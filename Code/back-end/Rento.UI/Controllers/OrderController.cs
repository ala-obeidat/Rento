using Rento.Entity;
using Rento.Helper;
using Rento.UI.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using Rento.UI.Models;
using Resources;

namespace Rento.UI.Controllers
{
    public class OrderController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Action(int id)
        {

            var orderInfo = await CallApi<int, OrderAction>("Car/ListOrderAction", id);
            if (orderInfo.Data != null)
            {
                orderInfo.Data.CarName = GetCarName(orderInfo.Data.CarType, orderInfo.Data.CarSubType, orderInfo.Data.CarYear);
                foreach (var item in orderInfo.Data.Actions)
                {
                    if (item.Action == CustomerCarAction.Pending)
                        item.Name = Resources.Resource.OrderCreated;
                    else
                        item.Name = Resources.Resource.ResourceManager.GetString(item.Action.ToString());
                }
                return View(orderInfo.Data);
            }
            return View();
        }

       


        [HttpGet]
        public async Task<ActionResult> List(GridModel model)
        {
            var search = FixData._rentoSerializer.Deserialize<GridSetting>(model.Settings);

            return await CallApiGrid<int, List<Order>>("Car/ListOrder", search.Key, model
                , delegate (List<Order> order)
                {
                    return order.Select(o => new object[]
                    {
                            o.Id,
                            "false",
                            GetCarName(o.Type, o.SubType, o.Model),
                            GetStatsColor(o.CheckoutFlag),
                            o.From.ToString("dd/MM/yyyy"),
                            o.To.ToString("dd/MM/yyyy"),
                            o.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                });
                });
        }

        private string GetStatsColor(CheckoutFlag checkoutFlag)
        {
            var name=Resource.ResourceManager.GetString(checkoutFlag.ToString());
            var className = "danger";
            switch (checkoutFlag)
            {
                case Rento.Entity.CheckoutFlag.GetFromOffice:
                case Rento.Entity.CheckoutFlag.DeliverToMyLocation:
                    className = "default";
                    break;
                case Rento.Entity.CheckoutFlag.Done:
                    className = "success";
                    break;
                case Rento.Entity.CheckoutFlag.Approved:
                    className = "primary";
                    break;
            }
            return $"<button title='{name}' class='form-control btn-{className}' >{name}</button>";
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var carInfoResponse = await CallApi<int, OrderItem>("Car/ViewOrder", id);
                if (carInfoResponse.ErrorCode != ErrorCode.Success)
                    return RedirectToAction("Index");
                carInfoResponse.Data.CarName = GetCarName(carInfoResponse.Data.Type, carInfoResponse.Data.SubType, carInfoResponse.Data.Model);
                return View(carInfoResponse.Data);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Close(OrderClose model)
        {
            try
            {
                var response=   await CallApiTask("Car/CloseOrder", model);
                return RentoJson(response);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return RentoJsonError();
        }

        [HttpGet]
        public async Task<ActionResult> ListPending()
        {
            try
            {
                var urlParameter = string.Format("key={0}&token={1}&lang={2}", "UI", Session["Token"], FixData.IsRTL ? "0" : "1");
                var response = await CallApiGet<PendngOrder>("Car/ListPendingOrder?" + urlParameter);
                return Json(new
                {
                    ErrorCode = response.ErrorCode,
                    Data = response.Data,
                    Message = Resources.Resource.ListPendingMessage
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return Json(new
            {
                ErrorCode = ErrorCode.GeneralError,
                Message = Resources.Resource.ListPendingMessage
            }, JsonRequestBehavior.AllowGet);
        }

    }
}