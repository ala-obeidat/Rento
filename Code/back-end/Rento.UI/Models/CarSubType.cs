using Rento.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rento.UI.Models
{
    public class CarSubType : BaseNameEntity<int>
    {
        public HttpPostedFileBase CarIcon { get; set; }
    }
}