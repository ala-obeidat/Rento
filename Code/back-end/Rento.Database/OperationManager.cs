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
    public class OperationManager : BaseManager
    {
        public static async Task<BaseEntity> Save(int userId, Car data)
        {
            BaseEntity baseEntity = null;
            await DataBaseHelper.Instance.ExecuteTransaction(async delegate (SqlCommand cmd)
           {
               cmd.CommandText = StoredProcedure.CAR_SAVE;
               cmd.Parameters.AddWithValue("@Id", data.Id);
               cmd.Parameters.AddWithValue("@CityId", data.CityId);
               cmd.Parameters.AddWithValue("@AdditinalKiloCost", data.AdditinalKiloCost);
               cmd.Parameters.AddWithValue("@Color", data.Color.ToSafe());
               cmd.Parameters.AddWithValue("@DayCost", data.DayCost);
               cmd.Parameters.AddWithValue("@Description", data.Description.ToSafe());
               cmd.Parameters.AddWithValue("@Flag", data.Flag);
               cmd.Parameters.AddWithValue("@KiloLimit", data.KiloLimit);
               cmd.Parameters.AddWithValue("@KiloNumber", data.KiloNumber);
               cmd.Parameters.AddWithValue("@Model", data.Model);
               cmd.Parameters.AddWithValue("@MonthCost", data.MonthCost);
               cmd.Parameters.AddWithValue("@Status", data.Status);
               cmd.Parameters.AddWithValue("@SubType", data.SubType);
               cmd.Parameters.AddWithValue("@Type", data.Type);
               cmd.Parameters.AddWithValue("@WeekCost", data.WeekCost);
               cmd.Parameters.AddWithValue("@UserId", userId);
               using (var reader = await cmd.ExecuteReaderAsync())
               {
                   if (await reader.ReadAsync())
                       baseEntity = new BaseEntity()
                       {
                           Id = reader.GetInt32(0),
                           CreateDate = reader.GetDateTime(1)
                       };
               }
               if (baseEntity != null)
               {

                   if (data.DeletedImages != null && data.DeletedImages.Length > 0)
                   {
                       cmd.CommandText = StoredProcedure.CAR_IMAGE_DELETE;
                       foreach (var id in data.DeletedImages)
                       {
                           cmd.Parameters.Clear();
                           cmd.Parameters.AddWithValue("@CarId", baseEntity.Id);
                           cmd.Parameters.AddWithValue("@ImageId", id);
                           await cmd.ExecuteNonQueryAsync();
                       }
                   }
                   if (data.Images != null && data.Images.Count > 0)
                   {
                       cmd.CommandText = StoredProcedure.CAR_IMAGE_SAVE;
                       foreach (var image in data.Images)
                       {
                           cmd.Parameters.Clear();
                           cmd.Parameters.AddWithValue("@CarId", baseEntity.Id);
                           cmd.Parameters.AddWithValue("@ImageData", image.Content);
                           cmd.Parameters.AddWithValue("@ImageSize", image.Content.Length);
                           cmd.Parameters.AddWithValue("@ImageFileName", image.FileName.ToSafe());
                           await cmd.ExecuteNonQueryAsync();
                       }
                   }
               }
           }, delegate (Exception ex)
           {
               Logger.Exception(ex, "SQL transaction");
           });
            return baseEntity;
        }

        public static async Task<int> Checkout(int userId, Checkout data)
        {
             return await DataBaseHelper.Instance.ExecuteScaler<int>(StoredProcedure.CAR_CHECKOUT,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CarId", data.CarId);
                cmd.Parameters.AddWithValue("@Flag", data.Flag);
                cmd.Parameters.AddWithValue("@Date",DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@From", data.From);
                cmd.Parameters.AddWithValue("@To", data.To);
                
                if (data.Location != null)
                {
                    cmd.Parameters.AddWithValue("@GPS_Degree", data.Location.Longitude);
                    cmd.Parameters.AddWithValue("@GPS_Minute", data.Location.Latitude);
                }

            });

        }

        public static async Task CloseBooking(int id, CloseBooking data)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.CAR_CLOSE_BOOKING,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@Id", data.CheckoutId);
                cmd.Parameters.AddWithValue("@Comment", data.Comment);
                cmd.Parameters.AddWithValue("@Flag", data.Flag);
                cmd.Parameters.AddWithValue("@Date",DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                cmd.Parameters.AddWithValue("@Star", data.Star);

            });
        }

        public static async Task< List<CarActionBaseInfo>> ListRequest(int userId, bool history)
        {
            var response = new List<CarActionBaseInfo>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.CUSTOMER_CAR_REQUEST_LIST,
                delegate (SqlCommand sqlCommand)
                {
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlCommand.Parameters.AddWithValue("@History", history);
                },
                async delegate (SqlDataReader reader)
                {
                    while (await reader.ReadAsync())
                        response.Add(new CarActionBaseInfo()
                        {
                            Type = reader.GetInt32(0),
                            SubType = reader.GetInt32(1),
                            Model = reader.GetInt32(2),
                            Price = reader.GetInt32(3),
                            OfficeName = reader.GetString(4),
                            Action = reader.GetInt32(5),
                            CreateDate = reader.GetDateTime(6)
                         
                        });
                });
            return response;
        }


        public static async Task Delete(int userId, int id)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.CAR_DELETE,delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Id", id);
                 cmd.Parameters.AddWithValue("@UserId", userId);
             });
        }
        

        public static async Task<Car> Select(int userId, int id)
        {
            Car car = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.CAR_SELECT,delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Id", id);
             }, async delegate (SqlDataReader reader)
              {
                  if (await reader.ReadAsync())
                  {
                      car = new Car()
                      {
                          Id = id,
                          Type = reader.GetInt32(0),
                          SubType = reader.GetInt32(1),
                          Model = reader.GetInt32(2),
                          CityId = reader.GetInt32(3),
                          Status = reader.GetInt32(5),
                          DayCost = reader.GetInt32(6),
                          Flag = reader.GetInt32(7),
                          Color = reader.GetString(8),
                          CreateDate = reader.GetDateTime(9),

                      };
                      if (reader.NextResult() && await reader.ReadAsync())
                      {
                          car.Description = reader.GetString(0);
                          car.AdditinalKiloCost = reader.GetInt32(1);
                          car.MonthCost = reader.GetInt32(2);
                          car.WeekCost = reader.GetInt32(3);
                          car.KiloLimit = reader.GetInt32(4);
                          car.KiloNumber = reader.GetInt32(5);
                      }
                      if (reader.NextResult())
                      {
                          if (await reader.ReadAsync())
                          {
                              car.ImageIds = new List<int>();
                              do
                              {
                                  car.ImageIds.Add(reader.GetInt32(0));
                              } while (await reader.ReadAsync());
                          }
                      }
                  }

              });
            return car;
        }
    }
}
