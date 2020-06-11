using Rento.Entity;
using Rento.UI.Models;
using Rento.UI.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rento.UI.Controllers
{
    public class MobileCustomerController : BaseController
    {
        // GET: MobileCustomer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> List(GridModel model)
        {
            var search = FixData._rentoSerializer.Deserialize<MobileCustomerRequest>(model.Settings);

            return await CallApiGrid<MobileCustomerRequest, List<MobileCustomerResponse>>("User/ListMobileCustomer", search, model
                , delegate (List<MobileCustomerResponse> items)
                {
                    return items.Select(o => new object[]
                    {
                            o.Id,
                            "false",
                            o.Username,
                            o.FullName,
                            o.Mobile,
                            Resources.Resource.ResourceManager.GetString(o.Status.ToString()),
                            Resources.Resource.ResourceManager.GetString(o.Type.ToString()),
                            o.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                            $"<a href='javaScript:updateUserFlag({o.Id})' class='btn btn-primary'>{Resources.Resource.UpdateStats}</a>"
                    });
                });
        }
    }
}