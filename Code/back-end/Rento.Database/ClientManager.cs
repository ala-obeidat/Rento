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
    public class ClientManager : BaseManager
    {
        public static List<Client> List()
        {
            List<Client> result = new List<Client>();
            DataBaseHelper.Instance.ExecuteReaderSync(StoredProcedure.CLIENT_LIST,
              async delegate (SqlDataReader reader)
              {
                  while (await reader.ReadAsync())
                  {
                      result.Add(new Client()
                      {
                          Id = reader.GetString(0),
                          Name = reader.GetString(1),
                          SecritKey = reader.GetString(2),
                      });
                  }
              });
            return result;
        }


    }
}
