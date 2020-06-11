using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class Organization: BaseEntity
    {
        public string Name { get; set; }
        public int Flag { get; set; }
    }
    public enum OrganizationFlag
    {
        Normal = 0,
    }
}
