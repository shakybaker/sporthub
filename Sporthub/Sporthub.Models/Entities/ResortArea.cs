using Sporthub.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text;

namespace Sporthub.Models.Entities
{
    public class ResortArea
    {
        public ResortArea()
        {
            Resorts = new List<Resort>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DbGeography Location { get; set; }

        public IList<Resort> Resorts { get; set; }
    }
}
