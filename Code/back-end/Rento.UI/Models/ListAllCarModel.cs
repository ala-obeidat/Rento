using Rento.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rento.UI.Models
{
    public class ListAllCarModel
    {
        public List<BaseNameEntity> CarType { get; set; }
        public string CarSubType { get; set; }
        public List<BaseNameEntity<int>> Cities { get; set; }
        public List<SelectModel> Providers { get; set; }

    }
}