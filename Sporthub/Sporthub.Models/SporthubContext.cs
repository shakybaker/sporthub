using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporthub.Models
{
    public class SporthubContext : DbContext
    {
        public DbSet<Resort> Resorts { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
