using Rento.Entity;
using Rento.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rento.UI.Controllers
{
    public class OrganizationController : BaseController
    {
        // GET: Organization
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await CallApi<List<Organization>>("Organization/List");
            if (response.Data != null && response.Data.Count > 0)
                return View(response.Data);
            else
                return View();
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
                return View();
            var carInfoResponse = await CallApi<int, Organization>("Organization/Get", id);
            if (carInfoResponse.ErrorCode != ErrorCode.Success)
                return RedirectToAction("Index");
            return View(carInfoResponse.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Save(Organization model)
        {
            var respose=await CallApiTask("Organization/Save", model);
            return RentoJson(respose);
        }
    }
}