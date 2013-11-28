using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text;

namespace Sporthub.Models
{
    public class MountainRange
    {
        public MountainRange()
        {
            Resorts = new List<Resort>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DbGeography Location { get; set; }
        public string Slug { get; set; }

        public IList<Resort> Resorts { get; set; }
    }
}
