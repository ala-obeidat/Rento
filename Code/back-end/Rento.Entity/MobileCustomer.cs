using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class MobileCustomerRequest
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }
    public class MobileCustomerResponse:BaseEntity
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public int Type { get; set; }

        /// <summary>
        /// UserType
        /// </summary>
        public int Status { get; set; }
    }
}
