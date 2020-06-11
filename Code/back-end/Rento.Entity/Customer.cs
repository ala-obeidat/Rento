using System;

namespace Rento.Entity
{
    public class CustomerOptinal : BaseEntity
    {
        public DateTime DOP { get; set; }
        public string BirthDate { get; set; }
        public DateTime IdExpireDate { get; set; }
        public int Flag { get; set; }
        public string IdentifierId { get; set; }
        public int Gender { get; set; }
        public Base64RentoImage Identifier { get; set; }
        public Base64RentoImage Licence { get; set; }
    }
    public enum Gender
    {
        Male = 0,
        Female= 1,
        NonSpecific = -2,
        
    }
    public enum CustomerFlag
    {
        UnComplete = 0,
        CompleteBySignUp = 2,
        CompleteFromUpdate = 4
    }
    public class CustomerBase : CustomerOptinal
    {
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public long LicenceId { get; set; }
        public int IdentifierImageId { get; set; }
        public int LicenceImageId { get; set; }
    }

    public class Customer : CustomerBase
    {
        public int CityId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool NotificationType { get; set; }
    }
}