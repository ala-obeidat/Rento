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
    public class ImageController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetImage(int id)
        {
            try
            {
                if (id > 0)
                {
                    var imageReposnse = await CallApi<int, RentoImage>("Image/Get", id);
                    if (imageReposnse.ErrorCode == ErrorCode.Success && imageReposnse.Data != null)
                    {
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + imageReposnse.Data.FileName);
                        Response.OutputStream.Write(imageReposnse.Data.Content, 0, imageReposnse.Data.Content.Length);
                        Response.Flush();
                        Response.Close();
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return base.File(GetImagePath("default.png"), "image/png");
        }
    }
}