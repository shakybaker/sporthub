using HtmlAgilityPack;
using Sporthub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sporthub.Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SporthubContext())
            {
                foreach (var country in db.Countries)
                {
                    var slug = (country.Name == "United Kingdom") ? "scotland" : country.Slug;
                    var resorts = ScrapeResortsForCountry(slug, country.Id);
                    foreach (var resort in resorts)
                    {
                        db.Resorts.Add(resort);
                        Console.Write(string.Format("Saving country - {0} ...", resort.Name));
                        db.SaveChanges();
                        Console.WriteLine("DONE");
                    }
                }
            }

            Console.WriteLine("FINISHED");
            var tmp = Console.ReadLine();



        }

        private static List<Resort> ScrapeResortsForCountry(string slug, int countryId)
        {
            Console.WriteLine(string.Format("Scraping country - {0}", slug));
            var resorts = new List<Resort>();

            var months = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var days = new string[] { "", "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th", "13th", "14th", "15th", "16th", "17th", "18th", "19th", "20th", "21st", "22nd", "23rd", "24th", "25th", "26th", "27th", "28th", "29th", "30th", "31st" };

            HtmlNodeCollection cells = null;
            int? trailsGreen = null;
            int? trailsBlue = null;
            int? trailsRed = null;
            int? trailsBlack = null;
            int? liftsTotal = null;
            int? elevationFrom = null;
            int? elevationTo = null;
            int? verticalDrop = null;
            int? liftTram = null;
            int? liftFast8 = null;
            int? liftFast6 = null;
            int? liftFast4 = null;
            int? liftQuad = null;
            int? liftDouble = null;
            int? liftSurface = null;
            int? liftTriple = null;
            int? runsLength = null;
            int? terrainParks = null;
            int? longestRun = null;
            int? skiableTerrain = null;

            
            string resortStatus = string.Empty;
            string seasonStart = string.Empty;
            string seasonEnd = string.Empty;
            string touristAddress = string.Empty;
            string touristEmail = string.Empty;
            string akas = string.Empty;
            string href = "";

            var htmlWeb = new HtmlWeb();
            var resortsPage = htmlWeb.Load(string.Format("http://www.onthesnow.co.uk/{0}/skireport.html?startRow=1&numRows=10000", slug));
            var links = resortsPage.DocumentNode.SelectNodes("//div[@class='name']/a");

            int i = 0;
            foreach (var link in links.Where(x => x.Attributes["href"].Value.EndsWith("skireport.html")))
            {
                href = string.Format("http://www.onthesnow.co.uk{0}", link.Attributes["href"].Value.Replace("skireport", "ski-resort"));

                var doc = htmlWeb.Load(href);
                //var x = doc.DocumentNode.SelectSingleNode("./span[@class='resort_name']");
                var slugs = link.Attributes["href"].Value.Split('/');
                var slugname = slugs[2];
                var name = slugname;
                i++;
                //var name = "Resort " + i;
                //var slugname = "";



                Console.Write(string.Format("Scraping resort - {0} ...", name));
                var rows = doc.DocumentNode.SelectNodes("//table[@class='ovv_info']/tr");

                //resort & season status
                cells = rows[0].SelectNodes("./td");
                var stats = cells[0].SelectNodes("./span");
                resortStatus = stats[0].InnerText;
                var arr = stats[1].InnerText.Replace("Season Start/End:", string.Empty).Replace(" ", string.Empty).Split('-');
                var arr2 = arr[0].Split('/');
                var m1 = months[int.Parse(arr2[1])];
                var m2 = days[int.Parse(arr2[0])];
                seasonStart = string.Format("{0} {1}", m2, m1);
                var arr3 = arr[1].Split('/');
                var m3 = months[int.Parse(arr3[1])];
                var m4 = days[int.Parse(arr3[0])];
                seasonEnd = string.Format("{0} {1}", m4, m3);

                //elevation
                cells = rows[1].SelectNodes("./td");
                var tmp = cells[0].InnerText.Replace(" ", string.Empty).Split('-');
                elevationFrom = int.Parse(tmp[0].Replace("m", string.Empty));
                elevationTo = int.Parse(tmp[1].Replace("m", string.Empty));
                verticalDrop = elevationTo - elevationFrom;

                //runs
                cells = rows[2].SelectNodes("./td");
                foreach (var cell in cells)
                {
                    string className = string.Empty;
                    if (cell.Attributes["class"] != null)
                        className = cell.Attributes["class"].Value.Trim();

                    if (className == "ovv_trails")
                    {
                        if (cell.SelectSingleNode("./span[@class='ovv_t t1']") != null)
                            trailsGreen = int.Parse(cell.SelectSingleNode("./span[@class='ovv_t t1']").InnerText.Replace("%", string.Empty));
                        if (cell.SelectSingleNode("./span[@class='ovv_t t2']") != null)
                            trailsBlue = int.Parse(cell.SelectSingleNode("./span[@class='ovv_t t2']").InnerText.Replace("%", string.Empty));
                        if (cell.SelectSingleNode("./span[@class='ovv_t t3']") != null)
                            trailsRed = int.Parse(cell.SelectSingleNode("./span[@class='ovv_t t3']").InnerText.Replace("%", string.Empty));
                        if (cell.SelectSingleNode("./span[@class='ovv_t t4']") != null)
                            trailsBlack = int.Parse(cell.SelectSingleNode("./span[@class='ovv_t t4']").InnerText.Replace("%", string.Empty));
                    }
                }

                //elevation
                cells = rows[3].SelectNodes("./td");
                liftsTotal = int.Parse(cells[0].InnerText);

                //lifts
                var lis = doc.DocumentNode.SelectNodes("//div[@id='resort_lifts']/ul/li");
                foreach (var li in lis)
                {
                    string className = string.Empty;
                    if (li.Attributes["class"] != null)
                        className = li.Attributes["class"].Value.Trim();

                    switch (className)
                    {
                        case "trams":
                            liftTram = int.Parse(li.InnerText);
                            break;
                        case "fast_eight":
                            liftFast8 = int.Parse(li.InnerText);
                            break;
                        case "fast_sixes":
                            liftFast6 = int.Parse(li.InnerText);
                            break;
                        case "fast_quads":
                            liftFast4 = int.Parse(li.InnerText);
                            break;
                        case "quad":
                            liftQuad = int.Parse(li.InnerText);
                            break;
                        case "triple":
                            liftTriple = int.Parse(li.InnerText);
                            break;
                        case "double":
                            liftDouble = int.Parse(li.InnerText);
                            break;
                        case "surface":
                            liftSurface = int.Parse(li.InnerText);
                            break;
                    }
                }

                //terrain
                var uls = doc.DocumentNode.SelectNodes("//div[@id='resort_terrain']/ul");

                var lis2 = uls[0].SelectNodes("./li");
                foreach (var li in lis2)
                {
                    var ps = li.SelectNodes("./p");
                    foreach (var p in ps)
                    {
                        switch (p.InnerText.ToLower().Trim())
                        {
                            case "runs":
                                runsLength = int.Parse(p.NextSibling.InnerText.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("KM", string.Empty).Trim());
                                break;
                            case "terrain parks":
                                terrainParks = int.Parse(p.NextSibling.InnerText.Replace("\n", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty).Trim());
                                break;
                            case "longest run":
                                longestRun = int.Parse(p.NextSibling.InnerText.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("KM", string.Empty).Trim());
                                break;
                            case "skiable terrain":
                                skiableTerrain = int.Parse(p.NextSibling.InnerText.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("ha", string.Empty).Trim());
                                break;
                        }
                    }
                }


                //important dates
                //--years open
                //--days open last year
                //--projected days open
                //--average snowfall

                //tourist office
                var divs = doc.DocumentNode.SelectNodes("//div[@id='resort_contact']");

                var ps3 = divs[0].SelectNodes("./ul/li/p");
                foreach (var p in ps3)
                {
                    if (p.SelectSingleNode("./a") != null)
                    {
                        touristEmail = p.SelectSingleNode("./a").InnerText;
                    }
                    else
                    {
                        touristAddress += string.Format("{0}\n", p.InnerText);
                    }
                }

                //common misspellings
                if (divs.Count > 1)
                {
                    var ps2 = divs[1].SelectNodes("./ul/li/p");
                    akas = ps2[0].InnerText;
                }

                //Console.WriteLine(string.Format("Resort status - {0}", resortStatus));
                //Console.WriteLine(string.Format("Season Start - {0}", seasonStart));
                //Console.WriteLine(string.Format("Season End - {0}", seasonEnd));
                //Console.WriteLine(string.Format("Elevation - {0}m - {1}m", elevationFrom, elevationTo));
                //Console.WriteLine(string.Format("Vertical drop - {0}m", verticalDrop));
                //Console.WriteLine(string.Format("Lifts total - {0}", liftsTotal));
                //Console.WriteLine(string.Format("Runs - Green: {0}, Blue: {1}, Red: {2}, Black: {3}", trailsGreen, trailsBlue, trailsRed, trailsBlack));
                //Console.WriteLine(string.Format("Lifts - Trams: {0}, Fast Eights: {1}, Fast Sizes: {2}, Fast Quads: {3}, Quads: {4}, Triples: {5}, Doubles: {6}, Surface: {7}", liftTram, liftFast8, liftFast6, liftFast4, liftQuad, liftTriple, liftDouble, liftSurface));
                //Console.WriteLine(string.Format("Runs length - {0}km", runsLength));
                //Console.WriteLine(string.Format("Terrain parks - {0}", terrainParks));
                //Console.WriteLine(string.Format("Longest run - {0}km", longestRun));
                //Console.WriteLine(string.Format("Skiable terrain - {0}ha", skiableTerrain));
                //Console.WriteLine(string.Format("Tourist office address - {0}", touristAddress));
                //Console.WriteLine(string.Format("Tourist office email - {0}", touristEmail));
                //Console.WriteLine(string.Format("Also known as - {0}", akas));
                //string tmp2 = Console.ReadLine();

                var resort = new Resort();
                resort.Name = name;
                resort.Slug = slugname;
                resort.DummyCountryId = countryId;
                resort.TrailsGreen = trailsGreen;
                resort.TrailsBlue = trailsBlue;
                resort.TrailsRed = trailsRed;
                resort.TrailsBlack = trailsBlack;
                resort.LiftsTotal = liftsTotal;
                resort.ElevationFrom = elevationFrom;
                resort.ElevationTo = elevationTo;
                resort.VerticalDrop = verticalDrop;
                resort.ResortStatus = resortStatus;
                resort.SeasonStart = seasonStart;
                resort.SeasonEnd = seasonEnd;
                resort.LiftTram = liftTram;
                resort.LiftFastEight = liftFast8;
                resort.LiftFastSix = liftFast6;
                resort.LiftFastQuad = liftFast4;
                resort.LiftQuad = liftQuad;
                resort.LiftDouble = liftDouble;
                resort.LiftSurface = liftSurface;
                resort.LiftTriple = liftTriple;
                resort.RunsLength = runsLength;
                resort.TerrainParks = terrainParks;
                resort.LongestRun = longestRun;
                resort.SkiableTerrain = skiableTerrain;
                resort.TouristAddress = touristAddress;
                resort.TouristEmail = touristEmail;
                resort.Aka = akas;
                Console.WriteLine("DONE");

                resorts.Add(resort);
                    //                resorts.Add(resort);
            }

            return resorts;
        }
    }
}
