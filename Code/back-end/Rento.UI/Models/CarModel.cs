using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rento.Entity;

namespace Rento.UI.Models
{
    public class CarModel : BaseEntity
    {
        public string TypeName { get; set; }
        public string SubTypeName { get; set; }
        public int Model { get; set; }
        public string Status { get; set; }
        public string ProviderName { get; set; }
    }
    
}