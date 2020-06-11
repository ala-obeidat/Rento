using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class OrderAction
    {
        public OrderAction()
        {
            Actions = new List<OrderCustomerActions>();
        }
        public void AddAction(int action,DateTime date)
        {
            Actions.Add(new OrderCustomerActions()
            {
                Action=(CustomerCarAction) action,
                Date=date
            });
        }
        public string OfficeName { get; set; }
        public string OfficeMobile { get; set; }
        public string OfficeEmail { get; set; }
        public string CustomerUsername { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerFullName { get; set; }
        public int CarType { get; set; }
        public int CarSubType { get; set; }
        public int CarYear { get; set; }
        public int CarId { get; set; }
        public int OfficeId { get; set; }
        public int CheckoutId { get; set; }
        public CheckoutFlag Status { get; set; }
        public int OfficeStar { get; set; }
        public int CustomerStar { get; set; }
        public string CustomerComment { get; set; }
        public Gender CustomerGender { get; set; }
        public string OfficeComment { get; set; }
        public int Price { get; set; }
        public List<OrderCustomerActions> Actions { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string CarName { get; set; }
    }
    public class OrderCustomerActions
    {
        public CustomerCarAction Action { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
