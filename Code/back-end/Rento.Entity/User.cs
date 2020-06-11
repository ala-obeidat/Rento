using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class User : BaseEntity
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int OrganizationId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string TermsAndCondition { get; set; }
        public int WorkingTimeStart { get; set; }
        public int WorkingTimeEnd { get; set; }
        public int WorkingTimeDays { get; set; }

        /// <summary>
        /// UserFlag
        /// </summary>
        public int Flag { get; set; }
        public int LogoId { get; set; }
        public int LicenceId { get; set; }
        public int RefarmeCardId { get; set; }
        public RentoImage Logo { get; set; }
        public RentoImage Licence { get; set; }
        public RentoImage RefarmeCard { get; set; }
    }

    public class UpdateUserFlagRequest
    {
        public int UserId { get; set; }
        /// <summary>
        /// UserType
        /// </summary>
        public int Flag { get; set; }
        public string Message { get; set; }
    }
    public enum UserFlag
    {
        Normal = 0,
        DELIVER_TO_HOME = 2,
    }
}
