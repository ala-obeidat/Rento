using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class Checkout
    {
        public int CarId { get; set; }
        public int Flag { get; set; }
        public GPS Location { get; set; }
        public int Price { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
    public class CloseBooking
    {
        public int CheckoutId { get; set; }
        public int Star { get; set; }
        public int Flag { get; set; }
        public string Comment { get; set; }
    }
    public enum CheckoutFlag
    {
        GetFromOffice = 0,
        DeliverToMyLocation = 1,
        Done = 2,
        Rejected = 3,
        Approved = 4,
        CustomerRejected = 5,
        OfficeCutoff=6
    }
}
