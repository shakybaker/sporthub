using HtmlAgilityPack;
using Sporthub.Models.Entities;
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
            var resorts = new List<Resort>();

            var months = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var days = new string[] { "","1st","2nd","3rd","4th","5th","6th","7th","8th","9th","10th","11th","12th","13th","14th","15th","16th","17th","18th","19th","20th","21st","22nd","23rd","24th","25th","26th","27th","28th","29th","30th","31st" };

            HtmlNodeCollection cells = null;
            string trailsGreen = string.Empty;
            string trailsBlue = string.Empty;
            string trailsRed = string.Empty;
            string trailsBlack = string.Empty;
            string liftsTotal = string.Empty;
            string elevationFrom = string.Empty;
            string elevationTo = string.Empty;
            string verticalDrop = string.Empty;
            string resortStatus = string.Empty;
            string seasonStart = string.Empty;
            string seasonEnd = string.Empty;
            string liftTram = string.Empty;
            string liftFast8= string.Empty;
            string liftFast6 = string.Empty;
            string liftFast4 = string.Empty;
            string liftQuad = string.Empty;
            string liftDouble = string.Empty;
            string liftSurface = string.Empty;
            string liftTriple = string.Empty;
            string runsLength = string.Empty;
            string terrainParks = string.Empty;
            string longestRun = string.Empty;
            string skiableTerrain = string.Empty;
            string touristAddress = string.Empty;
            string touristEmail = string.Empty;
            string akas = string.Empty;

            var htmlWeb = new HtmlWeb();
            var resortsPage = htmlWeb.Load("http://www.onthesnow.co.uk/france/skireport.html?startRow=1&numRows=10000");
            var links = resortsPage.DocumentNode.SelectNodes("//table[@id='resort_grid']/tbody/tr/td/div/div/a");

            foreach (var link in links)
            {
                if (!link.Attributes["href"].Value.EndsWith("skireport.html"))
                    continue;
                var doc = htmlWeb.Load(link.Attributes["href"].Value);
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
                elevationFrom = tmp[0].Replace("m", string.Empty);
                elevationTo = tmp[1].Replace("m", string.Empty);
                verticalDrop = (int.Parse(elevationTo) - int.Parse(elevationFrom)).ToString();

                //runs
                cells = rows[2].SelectNodes("./td");
                foreach (var cell in cells)
                {
                    string className = string.Empty;
                    if (cell.Attributes["class"] != null)
                        className = cell.Attributes["class"].Value.Trim();

                    if (className == "ovv_trails")
                    {
                        trailsGreen = cell.SelectSingleNode("./span[@class='ovv_t t1']").InnerText.Replace("%", string.Empty);
                        trailsBlue = cell.SelectSingleNode("./span[@class='ovv_t t2']").InnerText.Replace("%", string.Empty);
                        trailsRed = cell.SelectSingleNode("./span[@class='ovv_t t3']").InnerText.Replace("%", string.Empty);
                        trailsBlack = cell.SelectSingleNode("./span[@class='ovv_t t4']").InnerText.Replace("%", string.Empty);
                    }
                }

                //elevation
                cells = rows[3].SelectNodes("./td");
                liftsTotal = cells[0].InnerText;

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
                            liftTram = li.InnerText;
                            break;
                        case "fast_eight":
                            liftFast8 = li.InnerText;
                            break;
                        case "fast_sixes":
                            liftFast6 = li.InnerText;
                            break;
                        case "fast_quads":
                            liftFast4 = li.InnerText;
                            break;
                        case "quad":
                            liftQuad = li.InnerText;
                            break;
                        case "triple":
                            liftTriple = li.InnerText;
                            break;
                        case "double":
                            liftDouble = li.InnerText;
                            break;
                        case "surface":
                            liftSurface = li.InnerText;
                            break;
                    }
                }

                //terrain
                var uls = doc.DocumentNode.SelectNodes("//div[@id='resort_terrain']/ul");
                var lis2 = uls[1].SelectNodes("./li");
                var t = lis2[0].SelectNodes("./p");
                runsLength = t[1].InnerText.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("KM", string.Empty).Trim();
                var t2 = lis2[1].SelectNodes("./p");
                terrainParks = t2[1].InnerText.Replace("\n", string.Empty).Replace(" ", string.Empty).Trim();
                var t3 = lis2[2].SelectNodes("./p");
                longestRun = t3[1].InnerText.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("KM", string.Empty).Trim();
                var t4 = lis2[3].SelectNodes("./p");
                skiableTerrain = t4[1].InnerText.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("ha", string.Empty).Trim();



                //important dates
                //--years open
                //--days open last year
                //--projected days open
                //--average snowfall

                //tourist office
                var divs = doc.DocumentNode.SelectNodes("//div[@id='resort_contact']");

                var ps = divs[0].SelectNodes("./ul/li/p");
                foreach (var p in ps)
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
                var ps2 = divs[1].SelectNodes("./ul/li/p");
                akas = ps2[0].InnerText;


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
                resort.CountryId = 1;
                resort.CountryName = "France";
                resort.TrailsGreen = int.Parse(trailsGreen);
                resort.TrailsBlue = int.Parse(trailsBlue);
                resort.TrailsRed = int.Parse(trailsRed);
                resort.TrailsBlack = int.Parse(trailsBlack);
                resort.LiftsTotal = int.Parse(liftsTotal);
                resort.ElevationFrom = int.Parse(elevationFrom);
                resort.ElevationTo = int.Parse(elevationTo);
                resort.VerticalDrop = int.Parse(verticalDrop);
                resort.ResortStatus = resortStatus;
                resort.SeasonStart = seasonStart;
                resort.SeasonEnd = seasonEnd;
                resort.LiftTram = int.Parse(liftTram);
                resort.LiftFastEight = int.Parse(liftFast8);
                resort.LiftFastSix = int.Parse(liftFast6);
                resort.LiftFastQuad = int.Parse(liftFast4);
                resort.LiftQuad = int.Parse(liftQuad);
                resort.LiftDouble = int.Parse(liftDouble);
                resort.LiftSurface = int.Parse(liftSurface);
                resort.LiftTriple = int.Parse(liftTriple);
                resort.RunsLength = int.Parse(runsLength);
                resort.TerrainParks = int.Parse(terrainParks);
                resort.LongestRun = int.Parse(longestRun);
                resort.SkiableTerrain = int.Parse(skiableTerrain);
                resort.TouristAddress = touristAddress;
                resort.TouristEmail = touristEmail;
                resort.Aka = akas;

                resorts.Add(resort);
            }

        }
    }
}
