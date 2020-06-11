using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rento.Helper;
using Rento.Entity;

namespace Rento.Database
{
    public class OfferManager
    {
        public static async Task<BaseEntity> Create(int userId, OfferCreate data)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.USER_INSERT,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@Name", data.CarId);
                cmd.Parameters.AddWithValue("@Age", data.Cost);
            });
            return new BaseEntity();
        }
        

        public static async Task<List<Offer>> List(int userId)
        {
            var offers = new List<Offer>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.OFFER_LIST, async delegate (SqlDataReader reader)
             {
                 while (await reader.ReadAsync())
                     offers.Add(new Offer()
                     {
                         Id = reader.GetInt32(0),
                         CarType = reader.GetInt32(1),
                         CarSubType = reader.GetInt32(2),
                         CarModel = reader.GetInt32(3),
                         Cost = reader.GetInt32(4),
                         Discount = reader.GetInt32(5),
                         Period = reader.GetInt32(6),
                         ProviderName = reader.GetString(7),
                         From = reader.GetDateTime(8),
                         To = reader.GetDateTime(9),
                         CreateDate = reader.GetDateTime(10),
                         TypeNameAr = reader.GetString(11),
                         TypeNameEn = reader.GetString(12),
                         SubTypeNameAr = reader.GetString(13),
                         SubTypeNameEn = reader.GetString(14)
                     });
             });
            return offers;
        }

        public static Offer Select(int userId)
        {
            throw new NotImplementedException();
        }
        
    }
}
