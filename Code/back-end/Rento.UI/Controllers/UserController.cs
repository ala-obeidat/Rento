using Rento.Entity;
using Rento.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Rento.UI.Shared;
using Rento.Helper;

namespace Rento.UI.Controllers
{
    public class UserController : BaseController
    {

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                ViewBag.FromOthers = false;
                changePassword.NewPassword = changePassword.NewPassword.FromRentoBase64(changePassword.PasswordKey);
                changePassword.OldPassword = changePassword.OldPassword.FromRentoBase64(changePassword.PasswordKey);
                changePassword.PasswordKey = string.Empty;
                var userLoginResponse = await CallApiTask("Account/ChangePassword", changePassword);
                ViewBag.ChangePasswordError = userLoginResponse.Message;
                return RentoJson(userLoginResponse);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return RentoJsonError();
        }

        // GET: User
        public async Task<ActionResult> Index(int id = 0)
        {
            try
            {
                ViewBag.FromOthers = id > 0;
                var saveResponse = await CallApi<int, Entity.User>("User/GetUserInfo", id);
                var userInfo = saveResponse.Data;
                ViewBag.City = FixData.SYSTEM_CITY;
                ViewBag.ModelCity = FixData.SYSTEM_CITY.Where(c => c.ExternalData == 1);
                ViewBag.Country = FixData.SYSTEM_COUNTRY;
                var organizationsResponse = await CallApi<List<Organization>>("Organization/List");
                if (organizationsResponse.ErrorCode == ErrorCode.Success && organizationsResponse.Data != null)
                    ViewBag.Organization = organizationsResponse.Data;
                ViewBag.PasswordKey = StringHelper.GetUniqeKey();
                if (userInfo != null)
                {
                    return View(new UserInfo()
                    {
                        CityId = userInfo.CityId,
                        CountryId = userInfo.CountryId,
                        Flag = userInfo.Flag,
                        Latitude = userInfo.Latitude,
                        Longitude = userInfo.Longitude,
                        Mobile = userInfo.Mobile,
                        Name = userInfo.Name,
                        WorkingTimeDays = userInfo.WorkingTimeDays,
                        WorkingTimeEnd = userInfo.WorkingTimeEnd,
                        WorkingTimeStart = userInfo.WorkingTimeStart,
                        Phone = userInfo.Phone,
                        LogoId = userInfo.LogoId,
                        OrganizationId = userInfo.OrganizationId,
                        RefarmeCardId = userInfo.RefarmeCardId,
                        LicenceId = userInfo.LicenceId,
                        Country = FixData.SYSTEM_COUNTRY,
                        Organization = organizationsResponse.Data,
                        Id = userInfo.Id,

                        TermsAndCondition = userInfo.TermsAndCondition,
                        City = FixData.SYSTEM_CITY.Where(c => c.ExternalData == userInfo.CountryId).ToList()
                    });
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Save(UserInfo userInfo)
        {
            try
            {
                RentoImage logo = null;
                RentoImage licence = null;
                RentoImage refarmeCard = null;

                if (userInfo.LogoFile != null)
                {
                    logo = new Entity.RentoImage()
                    {
                        FileName = userInfo.LogoFile.FileName,
                        Content = new byte[userInfo.LogoFile.ContentLength],
                    };
                    userInfo.LogoFile.InputStream.Read(logo.Content, 0, userInfo.LogoFile.ContentLength);
                    userInfo.LogoFile.InputStream.Dispose();
                }
                if (userInfo.LicenceFile != null)
                {
                    licence = new RentoImage()
                    {
                        FileName = userInfo.LicenceFile.FileName,
                        Content = new byte[userInfo.LicenceFile.ContentLength],
                    };
                    userInfo.LicenceFile.InputStream.Read(licence.Content, 0, userInfo.LicenceFile.ContentLength);
                    userInfo.LicenceFile.InputStream.Dispose();
                }
                if (userInfo.RefarmeCardFile != null)
                {
                    refarmeCard = new Entity.RentoImage()
                    {
                        FileName = userInfo.RefarmeCardFile.FileName,
                        Content = new byte[userInfo.RefarmeCardFile.ContentLength],
                    };
                    userInfo.RefarmeCardFile.InputStream.Read(refarmeCard.Content, 0, userInfo.RefarmeCardFile.ContentLength);
                    userInfo.RefarmeCardFile.InputStream.Dispose();
                }

                var saveResponse = await CallApi<Entity.User, Task>("User/UpdateUserInfo", new Entity.User()
                {
                    CityId = userInfo.CityId,
                    CountryId = userInfo.CountryId,
                    Id = userInfo.Id,
                    Flag = userInfo.Flag,
                    Longitude = userInfo.Longitude,
                    Latitude = userInfo.Latitude,
                    Mobile = userInfo.Mobile,
                    Name = userInfo.Name,
                    WorkingTimeDays = userInfo.WorkingTimeDays,
                    WorkingTimeEnd = userInfo.WorkingTimeEnd,
                    WorkingTimeStart = userInfo.WorkingTimeStart,
                    Phone = userInfo.Phone,
                    Logo = logo,
                    TermsAndCondition = userInfo.TermsAndCondition,
                    Licence = licence,
                    RefarmeCard = refarmeCard,
                    OrganizationId = userInfo.OrganizationId
                });
                return RentoJson(saveResponse);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return RentoJsonError();
        }
    }
}