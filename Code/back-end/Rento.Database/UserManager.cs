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
    public class UserManager
    {
        public static async Task<int> Create(UserLogin data)
        {
            return await DataBaseHelper.Instance.ExecuteScaler<int>(StoredProcedure.USER_INSERT,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@Username", data.Username.ToSafe());
                cmd.Parameters.AddWithValue("@Password", data.Password.ToSafe());
                cmd.Parameters.AddWithValue("@IsOperation", data.IsOperation);
            });
        }

        public static async Task<string> GetGetTermsAndCondition(int id)
        {
            return await DataBaseHelper.Instance.ExecuteScaler<string>(StoredProcedure.PROVIDER_SELECT_TIRMS,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@Id", id);
            });
        }

        public static async Task<List<User>> List(int userId)
        {
            var users = new List<User>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.USER_LIST, async delegate (SqlDataReader reader)
             {
                 while (await reader.ReadAsync())
                     users.Add(new User()
                     {
                         Id = reader.GetInt32(0),
                         Name = reader.GetString(1),
                     });
             });
            return users;
        }
        public static async Task<List<SelectModel>> ListProvider(int userId)
        {
            var users = new List<SelectModel>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.PROVIDER_LIST,
                delegate (SqlCommand command)
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                },
                async delegate (SqlDataReader reader)
            {
                while (await reader.ReadAsync())
                    users.Add(new SelectModel()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                    });
            });
            return users;
        }


        public static async Task<int> UpdateFlag(UpdateUserFlagRequest data)
        {
            return await DataBaseHelper.Instance.ExecuteScaler<int>(StoredProcedure.USER_UPDATEFLAG,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@UserId", data.UserId);
                cmd.Parameters.AddWithValue("@Flag", data.Flag);
                if (!string.IsNullOrEmpty(data.Message))
                    cmd.Parameters.AddWithValue("@Message", data.Message);
            });
        }
    }
}
