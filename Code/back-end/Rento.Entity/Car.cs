using System.Collections.Generic;

namespace Rento.Entity
{
    public class CarActionBaseInfo : BaseEntity
    {
        public int Type { get; set; }
        public string TypeNameAr { get; set; }
        public string TypeNameEn { get; set; }
        public string OfficeName { get; set; }

        public int SubType { get; set; }

        public string SubTypeNameAr { get; set; }
        public string SubTypeNameEn { get; set; }
        public int Model { get; set; }
        public int Action { get; set; }
        public int Price { get; set; }
    }

    public class CarBaseInfo : CarActionBaseInfo
    {
        public int Status { get; set; }
        public string Color { get; set; }
        public int DayCost { get; set; }
        public string OfficeMobile { get; set; }
        public int OfficeFlag { get; set; }

    }

    public class CarBase : CarBaseInfo
    {
        public int Flag { get; set; }


        public int CityId { get; set; }
        public int CountryId { get; set; }
    }

    public class CarListRequest
    {
        public string Color { get; set; }
     
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public bool Ascending { get; set; }
        public bool Sort { get; set; }

        public int CityId { get; set; }
        public int Model { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public string ProviderId { get; set; }
    }

    public class Car : CarBase
    {
        public int KiloNumber { get; set; }
        public int AdditinalKiloCost { get; set; }
        public int KiloLimit { get; set; }
        public string Description { get; set; }
        public int WeekCost { get; set; }
        public int MonthCost { get; set; }
        public List<RentoImage> Images { get; set; }
        public List<int> ImageIds { get; set; }
        public int[] DeletedImages { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public enum CarStatus
    {
        Available = 0,
        Reserved = 1,
        Fixing = 2
    }
}
