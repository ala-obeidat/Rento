using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class OfferBase : BaseEntity
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Period { get; set; }
        public int Cost { get; set; }
        public int Discount { get; set; }
    }

    public class OfferCreate : OfferBase
    {
        public int ProviderId { get; set; }
        public int CarId { get; set; }
    }
    
    public class Offer : OfferBase
    {
        public string ProviderName { get; set; }
        public int CarType { get; set; }
        public int CarSubType { get; set; }
        public int CarModel { get; set; }

        public string TypeNameAr { get; set; }
        public string TypeNameEn { get; set; }
        public string SubTypeNameAr { get; set; }
        public string SubTypeNameEn { get; set; }
    }
}
