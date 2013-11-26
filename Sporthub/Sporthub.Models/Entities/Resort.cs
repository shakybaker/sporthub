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
        public Resort()
        {
            Aka = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DbGeography Location { get; set; }
        public string Slug { get; set; }
        public IList<string> Aka { get; set; }
    }
}
