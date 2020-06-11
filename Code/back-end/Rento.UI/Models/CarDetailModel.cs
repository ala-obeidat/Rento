using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rento.Entity;
namespace Rento.UI.Models
{
    public class CarDetailModel : Car
    {
        public HttpPostedFileBase[] ImagesData { get; set; }
        public string DeletedImageIds { get; set; }
    }
}