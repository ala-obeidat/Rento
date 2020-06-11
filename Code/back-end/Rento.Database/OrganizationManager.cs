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
    public class OrganizationManager
    {
        public static async Task Save(int userId, Organization data)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.ORGANIZATION_SAVE,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@Id", data.Id);
                cmd.Parameters.AddWithValue("@Name", data.Name.ToSafe());
                cmd.Parameters.AddWithValue("@Date",DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                cmd.Parameters.AddWithValue("@Flag", data.Flag);
                cmd.Parameters.AddWithValue("@UserId", userId);
            });
        }

        public static async Task<Organization> Select(int userId, int Id)
        {
            Organization data = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.ORGANIZATION_SELECT,delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Id", Id);
             },
            async delegate (SqlDataReader reader)
            {
                if (await reader.ReadAsync())
                {
                    data = new Organization()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Flag = reader.GetInt32(2),
                        CreateDate = reader.GetDateTime(3)
                    };
                }
            });
            return data;
        }

        public static async Task<List<Organization>> List(int userId)
        {
            var organizations = new List<Organization>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.ORGANIZATION_LIST,
                 async delegate (SqlDataReader reader)
             {
                 while (await reader.ReadAsync())
                     organizations.Add(new Organization()
                     {
                         Id = reader.GetInt32(0),
                         Name = reader.GetString(1),
                         Flag = reader.GetInt32(2),
                         CreateDate = reader.GetDateTime(3),
                     });
             });
            return organizations;
        }

    }
}
