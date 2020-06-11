using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class SearchAPIRequest
    {
        public string Type { get; set; }
        public string Color { get; set; }
        public int Model { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }

    }
}
