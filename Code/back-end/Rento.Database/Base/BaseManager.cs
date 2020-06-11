using Rento.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Database.Base
{
    public class BaseManager
    {
        protected static async Task<RentoImage> GetRentoImage(SqlDataReader reader)
        {
            RentoImage image = null;
            if (await reader.ReadAsync())
            {
                image = new RentoImage()
                {
                    FileName = reader.GetString(1),
                    Content = new byte[reader.GetInt32(2)]
                };
                reader.GetStream(0).Read(image.Content, 0, image.Content.Length);
            }
            return image;
        }
        protected static async Task<Base64RentoImage> GetRentoImageBase64(SqlDataReader reader)
        {
            Base64RentoImage image = null;
            if (await reader.ReadAsync())
            {
                image = new Base64RentoImage()
                {
                    FileName = reader.GetString(1),
                };
                var array = new byte[reader.GetInt32(2)];
                reader.GetStream(0).Read(array, 0, array.Length);
                image.Content = Convert.ToBase64String(array);
            }
            return image;
        }
        protected static async Task<List<RentoImage>> GetRentoImages(SqlDataReader reader)
        {
            List<RentoImage> images = null;
            do
            {
                var image=new RentoImage()
                {
                    FileName = reader.GetString(1),
                    Content = new byte[reader.GetInt32(2)]
                };
                reader.GetStream(0).Read(image.Content, 0, image.Content.Length);
                images.Add(image);
            } while (await reader.ReadAsync());
            
            return images;
        }
    }
}
