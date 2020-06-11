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
    public class MessageManager
    {
        public static async Task Create(int userId, string body)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.MESSAGE_CREATE,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@Body", body.ToSafe());
                cmd.Parameters.AddWithValue("@Date",DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                cmd.Parameters.AddWithValue("@UserId", userId);
            });
        }
        

        public static async Task<List<Message>> List(int userId)
        {
            var messages = new List<Message>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.MESSAGE_SELECT,
                delegate (SqlCommand command)
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                },
                async delegate (SqlDataReader reader)
             {
                 while (await reader.ReadAsync())
                     messages.Add(new Message()
                     {
                         Id = reader.GetInt32(0),
                         Body = reader.GetString(1),
                         CreateDate = reader.GetDateTime(2),
                     });
             });
            return messages;
        }

        public static async Task<List<string>> GetDeviceTokens()
        {
            var ids = new List<string>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.MESSAGE_SELECT_DEVICE_TOKENS, async delegate (SqlDataReader reader) 
            {
                while (await reader.ReadAsync())
                    ids.Add(reader.GetString(0));
            });
            return ids;
        }
    }
}
