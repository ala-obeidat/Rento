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
    public class LookUpManager : BaseManager
    {
        public static async Task<List<BaseNameEntity>> List(int userId, string tableName)
        {
            var list = new List<BaseNameEntity>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.LOOKUP_LIST + tableName, delegate (SqlCommand cmd)
               {
               }, async delegate (SqlDataReader reader)
                {
                    while (await reader.ReadAsync())
                        list.Add(new BaseNameEntity()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            NameEn = reader.GetString(2),
                        });
                });
            return list;
        }
        public static async Task<List<BaseNameEntity<T>>> List<T>(int userId, string tableName)
        {
            var list = new List<BaseNameEntity<T>>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.LOOKUP_LIST + tableName, delegate (SqlCommand cmd)
             {
             }, async delegate (SqlDataReader reader)
             {
                 while (await reader.ReadAsync())
                     list.Add(new BaseNameEntity<T>()
                     {
                         Id = reader.GetInt32(0),
                         Name = reader.GetString(1),
                         NameEn = reader.GetString(2),
                         ExternalData = reader.GetFieldValue<T>(3)
                     });
             });
            return list;
        }

        public static async Task<BaseEntity> Save(int id, LookUp data)
        {
            BaseEntity baseEntity = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.LOOKUP_SAVE + data.LookUpName, delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Id", data.Id);
                 cmd.Parameters.AddWithValue("@Name", data.Name.ToSafe());
                 cmd.Parameters.AddWithValue("@NameEn", data.NameEn.ToSafe());
             }, async delegate (SqlDataReader reader)
             {
                 if (await reader.ReadAsync())
                     baseEntity = new BaseEntity()
                     {
                         Id = reader.GetInt32(0),
                         CreateDate = reader.GetDateTime(1)
                     };
             });
            return baseEntity;
        }

        public static async Task<BaseEntity> Save<T>(int id, LookUp<T> data)
        {
            BaseEntity baseEntity = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.LOOKUP_SAVE + data.LookUpName, delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Id", data.Id);
                 cmd.Parameters.AddWithValue("@Name", data.Name.ToSafe());
                 cmd.Parameters.AddWithValue("@NameEn", data.NameEn.ToSafe());
                 cmd.Parameters.AddWithValue("@ExternalData", data.ExternalData);
             }, async delegate (SqlDataReader reader)
             {
                 if (await reader.ReadAsync())
                     baseEntity = new BaseEntity()
                     {
                         Id = reader.GetInt32(0),
                         CreateDate = reader.GetDateTime(1)
                     };
             });
            return baseEntity;
        }

        public static async Task Delete(int id, LookUpDelete data)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.LOOKUP_DELETE + data.LookUpName,
               delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@Id", data.Id);
                });
        }
    }
}
