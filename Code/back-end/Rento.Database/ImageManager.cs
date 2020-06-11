using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rento.Helper;
using Rento.Entity;
using Rento.Database.Base;

namespace Rento.Database
{
    public class ImageManager : BaseManager
    {
        public static async Task<RentoImage> Select(int userId, int id)
        {
            RentoImage image = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.IMAGEDATA_SELECT,
               delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                }, async delegate (SqlDataReader reader)
              {
                  image = await GetRentoImage(reader);

              });
            return image;
        }


    }
}
