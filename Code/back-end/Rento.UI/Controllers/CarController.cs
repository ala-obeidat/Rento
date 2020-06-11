using Rento.Entity;
using Rento.UI.Models;
using Rento.UI.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Rento.Helper;

namespace Rento.UI.Controllers
{
    public class CarController : BaseController
    {
        #region User

        public async Task<ActionResult> ChangeStatus(int id)
        {
            try
            {
                return RentoJson(await CallApiTask("Car/ChangeStatus", id));
           }
            catch(Exception ex)
            {
                Logger.Exception(ex);
            }
            return RentoJsonError();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var carsResponse = await CallApi<List<CarBaseInfo>>("Car/UserList");
                if (carsResponse.Data != null && carsResponse.Data.Count > 0)
                {
                    return View(carsResponse.Data.Select(c => new CarModel()
                    {
                        Id = c.Id,
                        CreateDate = c.CreateDate,
                        Model = c.Model,
                        Status = Resources.Resource.ResourceManager.GetString(c.Status.ToString()),
                        SubTypeName = GetName(Rento.UI.Shared.FixData.SYSTEM_SUB_TYPE, c.SubType, c.Type),
                        TypeName = GetName(Rento.UI.Shared.FixData.SYSTEM_TYPE, c.Type),
                    }));
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e, "Car Index");
                return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            RentoResponse<Car> carInfoResponse = null;
            try
            {
                carInfoResponse = await CallApi<int, Car>("Car/Get", id);
                if (carInfoResponse.ErrorCode != ErrorCode.Success)
                    return RedirectToAction("Index");

                ViewBag.Type = FixData.SYSTEM_TYPE;
                ViewBag.ModelSubType = FixData.SYSTEM_SUB_TYPE.Where(c => c.ExternalData == 1);
                ViewBag.SubType = FixData.SYSTEM_SUB_TYPE;
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return View(carInfoResponse.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Save(CarDetailModel carDetailModel)
        {
            try
            {
                if (carDetailModel.ImagesData != null && carDetailModel.ImagesData[0] != null)
                {
                    carDetailModel.Images = new List<RentoImage>();
                    foreach (var item in carDetailModel.ImagesData)
                    {
                        var content = new byte[item.ContentLength];
                        item.InputStream.Read(content, 0, item.ContentLength);
                        item.InputStream.Dispose();
                        carDetailModel.Images.Add(new RentoImage()
                        {
                            Content = content,
                            FileName = item.FileName
                        });
                    }
                    Array.Clear(carDetailModel.ImagesData, 0, carDetailModel.ImagesData.Length);
                    carDetailModel.ImagesData = null;
                }
                carDetailModel.DeletedImages = string.IsNullOrEmpty(carDetailModel.DeletedImageIds) ? new int[0] : carDetailModel.DeletedImageIds.Split(',').Select(Int32.Parse).ToArray();
                var carSaveResponse = await CallApi<BaseEntity, Car>("Car/Save", carDetailModel);
                return RentoJson(carSaveResponse);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            return RentoJsonError();
        }
        public async Task<ActionResult> Delete(int id)
        {
            var carDeleteResponse = await CallApiTask<int>("Car/Delete", id);
            return RentoJsonDelete(carDeleteResponse, "ListAll");
        }
        public async Task<ActionResult> Create(int id = 0)
        {
            CarDetailModel model = null;
            try
            {
                ViewBag.Type = FixData.SYSTEM_TYPE;
                ViewBag.SubType = FixData._rentoSerializer.Serialize(FixData.SYSTEM_SUB_TYPE).Replace("\"", "'");
                ViewBag.TypeJSON = FixData._rentoSerializer.Serialize(FixData.SYSTEM_TYPE).Replace("\"", "'");
                ViewBag.StatusOption = GetCarStatus(true);
                if (id == 0)
                {
                    ViewBag.ModelSubType = FixData.SYSTEM_SUB_TYPE.Where(c => c.ExternalData == 1);
                    return View();
                }
                else
                {
                    var carInfoResponse = await CallApi<int, Car>("Car/Get", id);
                    if (carInfoResponse.ErrorCode != ErrorCode.Success)
                        return RedirectToAction("Index");
                    ViewBag.ModelSubType = FixData.SYSTEM_SUB_TYPE.Where(c => c.ExternalData == carInfoResponse.Data.Type);
                    model = new CarDetailModel
                    {
                        AdditinalKiloCost = carInfoResponse.Data.AdditinalKiloCost,
                        Color = carInfoResponse.Data.Color,
                        DayCost = carInfoResponse.Data.DayCost,
                        Description = carInfoResponse.Data.Description,
                        Flag = carInfoResponse.Data.Flag,
                        Id = carInfoResponse.Data.Id,
                        ImageIds = carInfoResponse.Data.ImageIds,
                        KiloLimit = carInfoResponse.Data.KiloLimit,
                        KiloNumber = carInfoResponse.Data.KiloNumber,
                        Model = carInfoResponse.Data.Model,
                        MonthCost = carInfoResponse.Data.MonthCost,
                        Status = carInfoResponse.Data.Status,
                        SubType = carInfoResponse.Data.SubType,
                        Type = carInfoResponse.Data.Type,
                        WeekCost = carInfoResponse.Data.WeekCost,
                    };
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return View(model);

        }

        #endregion

        #region Admin

        #region Type

        [HttpGet]
        public ActionResult ListType()
        {
            return View(FixData.SYSTEM_TYPE);
        }

        [HttpGet]
        public ActionResult TypeDetails(int id)
        {
            if (id == 0)
                return View();
            return View(FixData.SYSTEM_TYPE.First(t => t.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult> TypeSave(BaseNameEntity model)
        {
            try
            {
                model.NameEn = model.NameEn.ToEnglishChar();
                
                    var carSaveResponse = await CallApi<LookUp, BaseEntity>("LookUp/Save", new LookUp()
                    {
                        Id = model.Id,
                        LookUpName = "Type",
                        Name = model.Name,
                        NameEn = model.NameEn
                    });
                    if (carSaveResponse.ErrorCode == ErrorCode.Success)
                    {
                        var newFolderPath = GetImagePath("cars", model.NameEn.ToLower());
                        var types = FixData.SYSTEM_TYPE;
                        if (model.Id == 0)
                        {
                            Directory.CreateDirectory(newFolderPath);
                            types.Add(new BaseNameEntity
                            {
                                Id = carSaveResponse.Data.Id,
                                Name = model.Name,
                                NameEn = model.NameEn
                            });
                        }
                        else
                        {
                            var oldData = FixData.SYSTEM_TYPE.First(t => t.Id == model.Id);
                            if (!oldData.NameEn.Equals(model.NameEn, StringComparison.OrdinalIgnoreCase))
                                Directory.Move(GetImagePath("cars", oldData.NameEn.ToLower()), newFolderPath);
                            types.Remove(oldData);
                            types.Add(model);
                        }
                        FixData.SYSTEM_TYPE = types;
                    }
                return RentoJson(carSaveResponse);

            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return RentoJsonError();
        }

        [HttpGet]
        public async Task<ActionResult> TypeDelete(int id)
        {
                var carDeleteResponse = await CallApiTask<LookUpDelete>("LookUp/Delete", new LookUpDelete()
                {
                    Id = id,
                    LookUpName = "Type"
                });
                if (carDeleteResponse.ErrorCode == ErrorCode.Success)
                    FixData.SYSTEM_TYPE = FixData.SYSTEM_TYPE.Where(t => t.Id != id).ToList();
            return RentoJsonDelete(carDeleteResponse, "ListType");
        }

        #endregion

        #region SubType

        [HttpGet]
        public ActionResult ListSubType(int id)
        {
            ViewBag.CarTypeId = id;
            var subTypes = FixData.SYSTEM_SUB_TYPE.Where(s => s.ExternalData == id);
            if (subTypes.Count() == 0)
                return View();
            var type = FixData.SYSTEM_TYPE.FirstOrDefault(t => t.Id == id);
            if (type != null)
                ViewBag.CarTypeName = FixData.IsRTL ? type.Name : type.NameEn;
            else
                ViewBag.CarTypeName = "";
            return View(subTypes);
        }

        [HttpGet]
        public ActionResult SubTypeDetails(int id, int typeId)
        {
            try
            {
                ViewBag.CarTypeId = typeId;
                if (id == 0)
                    return View();
                var subType = FixData.SYSTEM_SUB_TYPE.First(t => t.Id == id);
                var type = FixData.SYSTEM_TYPE.First(t => t.Id == typeId);
                ViewBag.CarType = type.NameEn.ToLower();
                ViewBag.CarTypeName = FixData.IsRTL ? type.Name : type.NameEn;
                return View(subType);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SubTypeSave(CarSubType model)
        {
            try
            {
                   var type = FixData.SYSTEM_TYPE.First(t => t.Id == model.ExternalData);
                    if (model.CarIcon != null)
                    {
                        var filePath = GetImagePath("cars", type.NameEn.ToLower(), model.NameEn.ToLower() + ".png");
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                        using (var fileStream = System.IO.File.Create(filePath))
                        {
                            model.CarIcon.InputStream.Seek(0, SeekOrigin.Begin);
                            model.CarIcon.InputStream.CopyTo(fileStream);
                        }
                        model.CarIcon.InputStream.Dispose();
                        model.CarIcon = null;
                    }
                    var carSaveResponse = await CallApi<LookUp<int>, BaseEntity>("LookUp/SaveExternal", new LookUp<int>()
                    {
                        Id = model.Id,
                        LookUpName = "SubType",
                        Name = model.Name,
                        NameEn = model.NameEn,
                        ExternalData = model.ExternalData
                    });
                    if (carSaveResponse.ErrorCode == ErrorCode.Success)
                    {
                        var subTypes = FixData.SYSTEM_SUB_TYPE;
                        if (model.Id == 0)
                        {
                            subTypes.Add(new BaseNameEntity<int>
                            {
                                Id = carSaveResponse.Data.Id,
                                Name = model.Name,
                                NameEn = model.NameEn,
                                ExternalData = model.ExternalData
                            });
                        }
                        else
                        {
                            var oldSubType = subTypes.First(s => s.Id == carSaveResponse.Data.Id);
                            subTypes.Remove(oldSubType);
                            subTypes.Add(model);
                        }
                        FixData.SYSTEM_SUB_TYPE = subTypes;
                }
                return RentoJson(carSaveResponse);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return RentoJsonError();
        }

        [HttpGet]
        public async Task<ActionResult> SubTypeDelete(int id, int externalData)
        {
            var carDeleteResponse = await CallApiTask<LookUpDelete>("LookUp/Delete", new LookUpDelete()
            {
                Id = id,
                LookUpName = "SubType"
            });
            if (carDeleteResponse.ErrorCode == ErrorCode.Success)
                FixData.SYSTEM_SUB_TYPE = FixData.SYSTEM_SUB_TYPE.Where(t => t.Id != id).ToList();
            return RentoJsonDelete(carDeleteResponse, "ListSubType?id=" + externalData);
        }

        #endregion

        #endregion

        #region Operation

        [HttpGet]
        public async Task<ActionResult> ListAll()
        {
            try
            {
                var providerReponse = await CallApi<List<SelectModel>>("User/ListProvider");
                return View(new ListAllCarModel()
                {
                    CarSubType = FixData._rentoSerializer.Serialize(FixData.SYSTEM_SUB_TYPE).Replace("\"", "'"),
                    CarType = FixData.SYSTEM_TYPE,
                    Cities = FixData.SYSTEM_CITY,
                    Providers = providerReponse.Data
                });
            }
            catch (Exception e)
            {
                Logger.Exception(e, "Car List All");
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> Search(GridModel model)
        {
            var search = FixData._rentoSerializer.Deserialize<CarListRequest>(model.Settings);

            return await CallApiGrid<CarListRequest, List<CarBaseInfo>>("Car/List", search, model
                , delegate (List<CarBaseInfo> order)
                {
                    return order.Select(o => new object[]
                    {
                            o.Id,
                            "false",
                            o.OfficeName,
                            GetCarName(o.Type, o.SubType, o.Model),
                            Resources.Resource.ResourceManager.GetString(o.Status.ToString()),
                            o.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                            string.Format("<a href='{0}' class='btn btn-success'>{1}</a> | <a href = '{2}' class='btn btn-danger'>{3}</a>",Url.Action("Create",new { id =o.Id}),Resources.Resource.Details,$"javaScript:sendGetRequest(\"Car\",\"Delete\",{o.Id});",Resources.Resource.Delete)
                    });
                });
        }
        #endregion
    }
}