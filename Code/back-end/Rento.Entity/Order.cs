using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class OrderBase : BaseEntity
    {
        public int Type { get; set; }
        public int SubType { get; set; }
        public int Model { get; set; }
        public string CarName { get; set; }
    }
    public class OrderCloseResponse
    {
        public string NotificationToken { get; set; }
        public bool IsAndroid { get; set; }
        public int CustomerId { get; set; }
    }
    public class OrderClose
    {
        public int Id { get; set; }
        public int Star { get; set; }
        public bool Approve { get; set; }
        public string Comment { get; set; }
    }
    public class PendngOrder
    {
        public OrderBase FirstOrder { get; set; }

        public int OrdersCount { get; set; }
    }
    public class Order : OrderBase
    {
        public CheckoutFlag CheckoutFlag { get; set; }



        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }

    public class OrderItem : Order
    {
        public int Price { get; set; }
        public GPS Location { get; set; }
        public CustomerBase CustomerInfo { get; set; }
        public int Star { get; set; }
        public int CarId { get; set; }
        public string Comment { get; set; }
    }
}
