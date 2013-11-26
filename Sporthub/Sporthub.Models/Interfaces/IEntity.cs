using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporthub.Models.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
        int CreatedById { get; set; }
        int ModifiedById { get; set; }
    }
}
