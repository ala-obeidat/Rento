using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rento.UI.Models
{
    public class UserInfo : Entity.User
    {
        public  HttpPostedFileBase LogoFile { get; set; }
        public  HttpPostedFileBase LicenceFile { get; set; }
        public  HttpPostedFileBase RefarmeCardFile { get; set; }
        public List<Entity.BaseNameEntity> Country { get; set; }
        public List<Entity.Organization> Organization { get; set; }
        public List<Entity.BaseNameEntity<int>> City { get; set; }
    }
}