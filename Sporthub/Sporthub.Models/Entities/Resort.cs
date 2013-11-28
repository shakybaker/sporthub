using Sporthub.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text;

namespace Sporthub.Models.Entities
{
    public class Resort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DbGeography Location { get; set; }
        public string Slug { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public int TrailsGreen { get; set; }
        public int TrailsBlue { get; set; }
        public int TrailsRed { get; set; }
        public int TrailsBlack { get; set; }
        public int LiftsTotal { get; set; }
        public int ElevationFrom { get; set; }
        public int ElevationTo { get; set; }
        public int VerticalDrop { get; set; }
        public int LiftTram { get; set; }
        public int LiftFastEight { get; set; }
        public int LiftFastSix { get; set; }
        public int LiftFastQuad { get; set; }
        public int LiftQuad { get; set; }
        public int LiftDouble { get; set; }
        public int LiftSurface { get; set; }
        public int LiftTriple { get; set; }
        public int RunsLength { get; set; }
        public int TerrainParks { get; set; }
        public int LongestRun { get; set; }
        public int SkiableTerrain { get; set; }

        public string ResortStatus { get; set; }
        public string SeasonStart { get; set; }
        public string SeasonEnd { get; set; }
        public string TouristAddress { get; set; }
        public string TouristEmail { get; set; }
        public string Aka { get; set; }    
    }
}
