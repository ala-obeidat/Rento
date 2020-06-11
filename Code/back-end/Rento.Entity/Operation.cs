using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class Operation
    {
        public string CustomerName { get; set; }
        public string CarName { get; set; }
        public string OfficeName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Price { get; set; }
        public CustomerCarAction Action { get; set; }
    }

    public class OperationItemAssign
    {
        public int ItemId { get; set; }
    }
}
