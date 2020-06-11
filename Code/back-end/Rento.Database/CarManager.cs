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
    public class CarManager : BaseManager
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
               cmd.Parameters.AddWithValue("@Date", DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
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

        public static async Task ChangeStatus(int id, int carId)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.CAR_CHANGE_STATUS,
               delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    cmd.Parameters.AddWithValue("@CarId", carId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                });
        }

        public static async Task<int> Checkout(int userId, Checkout data)
        {
            return await DataBaseHelper.Instance.ExecuteScaler<int>(StoredProcedure.CAR_CHECKOUT,delegate (SqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CarId", data.CarId);
                cmd.Parameters.AddWithValue("@Flag", data.Flag);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
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
                cmd.Parameters.AddWithValue("@Comment", data.Comment.ToSafe());
                cmd.Parameters.AddWithValue("@Flag", data.Flag);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                cmd.Parameters.AddWithValue("@Star", data.Star);

            });
        }

        public static async Task<List<CarActionBaseInfo>> ListRequest(int userId, bool history)
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
                            CreateDate = reader.GetDateTime(6),
                            TypeNameAr = reader.GetString(7),
                            TypeNameEn = reader.GetString(8),
                            SubTypeNameAr = reader.GetString(9),
                            SubTypeNameEn = reader.GetString(10),
                            Id = reader.GetInt32(11)
                        });
                });
            return response;
        }

        public static async Task<OrderItem> ViewOrder(int id, int data)
        {
            var response = new OrderItem();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.ORDER_VIEW,
                delegate (SqlCommand sqlCommand)
                {
                    sqlCommand.Parameters.AddWithValue("@UserId", id);
                    sqlCommand.Parameters.AddWithValue("@OrderId", data);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                },
                async delegate (SqlDataReader reader)
                {
                    try
                    {
                        if (await reader.ReadAsync())
                        {
                            response = new OrderItem()
                            {
                                Id = reader.GetInt32(0),
                                CarId = reader.GetInt32(1),
                                Type = reader.GetInt32(2),
                                SubType = reader.GetInt32(3),
                                Model = reader.GetInt32(4),
                                Price = reader.GetInt32(5),
                                From = reader.GetDateTime(6),
                                To = reader.GetDateTime(7),
                                Comment = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                                CheckoutFlag = (CheckoutFlag)reader.GetInt32(9),
                                Location = reader.GetInt32(9) == 1 ? new GPS()
                                {
                                    Latitude = reader.GetString(10),
                                    Longitude = reader.GetString(11)
                                } : null,
                                Star = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                            };
                            if (await reader.NextResultAsync() && await reader.ReadAsync())
                            {
                                response.CustomerInfo = new CustomerBase()
                                {
                                    Id = reader.GetInt32(0),
                                    DOP = reader.GetDateTime(1),
                                    Flag = reader.GetInt32(2),
                                    IdentifierId = reader.GetString(3),
                                    Mobile = reader.GetString(4),
                                    Username = reader.GetString(5),
                                    IdentifierImageId = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                                    LicenceImageId = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                    Gender = reader.GetInt32(8),
                                    FullName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9)
                                };
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Logger.Exception(e);
                        throw e;
                    }
                });
            return response;
        }

        public static async Task<OrderAction> ListOrderAction(int data)
        {
            OrderAction response = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.ORDER_ACTION,
                delegate (SqlCommand sqlCommand)
                {
                    sqlCommand.Parameters.AddWithValue("@CheckoutId", data);
                },
                async delegate (SqlDataReader reader)
                {
                    if (await reader.ReadAsync())
                    {
                        response = new OrderAction()
                        {
                            CarType = reader.GetInt32(0),
                            CarSubType = reader.GetInt32(1),
                            CarYear = reader.GetInt32(2),
                            CheckoutId = reader.GetInt32(3),
                            Status = (CheckoutFlag)reader.GetInt32(4),
                            Price = reader.GetInt32(5),
                            OfficeStar = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                            CustomerStar = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                            OfficeComment = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            CustomerComment = reader.IsDBNull(9) ? "" : reader.GetString(9),
                            CustomerUsername = reader.GetString(10),
                            CustomerMobile = reader.GetString(11),
                            OfficeName = reader.GetString(12),
                            OfficeMobile = reader.GetString(13),
                            OfficeEmail = reader.IsDBNull(14) ? "" : reader.GetString(14),
                            From = reader.GetDateTime(15),
                            To = reader.GetDateTime(16),
                            CustomerGender = (Gender)reader.GetInt32(17),
                            CarId = reader.GetInt32(18),
                            OfficeId = reader.GetInt32(19),
                            CustomerFullName = reader.IsDBNull(20) ? string.Empty : reader.GetString(20)
                        };
                    }
                    if (await reader.NextResultAsync())
                        while (await reader.ReadAsync())
                            response.AddAction(reader.GetInt32(0), reader.GetDateTime(1));
                });
            return response;
        }

        public static async Task<OrderCloseResponse> CloseOrder(int id, OrderClose data)
        {
            OrderCloseResponse response = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.ORDER_CLOSE,
                delegate (SqlCommand sqlCommand)
                {
                    sqlCommand.Parameters.AddWithValue("@UserId", id);
                    sqlCommand.Parameters.AddWithValue("@OrderId", data.Id);
                    sqlCommand.Parameters.AddWithValue("@Approve", data.Approve);
                    sqlCommand.Parameters.AddWithValue("@Star", data.Star);
                    if (!string.IsNullOrEmpty(data.Comment))
                        sqlCommand.Parameters.AddWithValue("@Comment", data.Comment);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                },
                async delegate (SqlDataReader reader)
                {
                    if (await reader.ReadAsync())
                        response = new OrderCloseResponse()
                        {
                            NotificationToken =reader.IsDBNull(0) ? string.Empty: reader.GetString(0),
                            IsAndroid = reader.GetInt32(1) == 1,
                            CustomerId = reader.GetInt32(2)
                        };

                });
            return response;
        }
        public static async Task<RentoCountResponse<List<Order>>> ListOrder(int id, int pageNumber, int pageSize, int checkoutId)
        {
            var orders = new List<Order>();

            var tempRowsCount = 0;

            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.ORDER_LIST,
               delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    if (checkoutId != 0)
                        cmd.Parameters.AddWithValue("@CheckoutId", checkoutId);

                },
                async delegate (SqlDataReader reader)
                {
                    while (await reader.ReadAsync())
                    {
                        orders.Add(new Order()
                        {
                            Id = reader.GetInt32(0),
                            CheckoutFlag = (CheckoutFlag)reader.GetInt32(1),
                            Type = reader.GetInt32(2),
                            SubType = reader.GetInt32(3),
                            Model = reader.GetInt32(4),
                            From = reader.GetDateTime(5),
                            To = reader.GetDateTime(6),
                            CreateDate = reader.GetDateTime(7),
                        });
                    }
                    if (await reader.NextResultAsync() && await reader.ReadAsync())
                        tempRowsCount = reader.GetInt32(0);
                });
            return new RentoCountResponse<List<Order>>()
            {
                Data = orders,
                RowsCount = tempRowsCount
            };
        }

        public static async Task<PendngOrder> ListPendingOrder(int id)
        {
            PendngOrder order = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.CHECKOUT_LIST_PENDING,
               delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                },
                async delegate (SqlDataReader reader)
             {
                 if (await reader.ReadAsync())
                 {
                     order = new PendngOrder();
                     order.OrdersCount = reader.GetInt32(0);
                 }
             });
            return order;
        }

        public static async Task<int> Delete(int userId, int id)
        {
            return await DataBaseHelper.Instance.ExecuteScaler<int>(StoredProcedure.CAR_DELETE,delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Id", id);
                 cmd.Parameters.AddWithValue("@UserId", userId);
             });
        }

        public static async Task<RentoCountResponse<List<CarBaseInfo>>> List(int userId, CarListRequest data, int pageNumber = 1, int pageSize = 50, UserType type = UserType.Active)
        {
            int count = 0;
            var cars = new List<CarBaseInfo>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.CAR_LIST,delegate (SqlCommand cmd)
             {
                 if (pageNumber > 0)
                     cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                 if (pageSize > 0)
                     cmd.Parameters.AddWithValue("@PageSize", pageSize);

                 if (userId == 0)
                 {
                     if (type == UserType.Operation)
                         cmd.Parameters.AddWithValue("@Status", -1);
                     if (data.CityId > 0)
                         cmd.Parameters.AddWithValue("@CityId", data.CityId);
                     if (data.MaxPrice > 0)
                         cmd.Parameters.AddWithValue("@MaxPrice", data.MaxPrice);
                     if (data.MinPrice > 0)
                         cmd.Parameters.AddWithValue("@MinPrice", data.MinPrice);
                     if (data.Model > 0)
                         cmd.Parameters.AddWithValue("@Model", data.Model);
                     if (data.Type > 0)
                         cmd.Parameters.AddWithValue("@Type", data.Type);
                     if (data.SubType > 0)
                         cmd.Parameters.AddWithValue("@SubType", data.SubType);

                     if (!string.IsNullOrEmpty(data.ProviderId))
                         cmd.Parameters.AddWithValue("@ProviderId", Convert.ToInt32(data.ProviderId));

                     if (data.Sort)
                         cmd.Parameters.AddWithValue("@Ascending", data.Ascending);
                     if (!string.IsNullOrEmpty(data.Color))
                         cmd.Parameters.AddWithValue("@Color", data.Color);
                 }
                 else
                 {
                     cmd.Parameters.AddWithValue("@UserId", userId);
                 }
             }, async delegate (SqlDataReader reader)
              {
                  while (await reader.ReadAsync())
                      cars.Add(new CarBaseInfo()
                      {
                          Id = reader.GetInt32(0),
                          Type = reader.GetInt32(1),
                          SubType = reader.GetInt32(2),
                          Model = reader.GetInt32(3),
                          Status = reader.GetInt32(4),
                          CreateDate = reader.GetDateTime(5),
                          DayCost = reader.GetInt32(6),
                          Color = reader.GetString(7),
                          OfficeName = reader.GetString(8),
                          OfficeMobile = reader.GetString(9),
                          TypeNameAr = reader.GetString(10),
                          TypeNameEn = reader.GetString(11),
                          SubTypeNameAr = reader.GetString(12),
                          SubTypeNameEn = reader.GetString(13),
                      });
                  if (type == UserType.Operation && await reader.NextResultAsync() && await reader.ReadAsync())
                      count = reader.GetInt32(0);
              });
            return new RentoCountResponse<List<CarBaseInfo>>()
            {
                Data = cars,
                RowsCount = count
            };
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
                          TypeNameAr = reader.GetString(10),
                          TypeNameEn = reader.GetString(11),
                          SubTypeNameAr = reader.GetString(12),
                          SubTypeNameEn = reader.GetString(13),
                          OfficeName = reader.GetString(14),
                          OfficeMobile = reader.GetString(15),
                          Latitude = reader.IsDBNull(16) ? "0" : reader.GetString(16),
                          Longitude = reader.IsDBNull(17) ? "0" : reader.GetString(17),
                          OfficeFlag = reader.GetInt32(18),
                      };
                      if (await reader.NextResultAsync() && await reader.ReadAsync())
                      {
                          car.Description = reader.GetString(0);
                          car.AdditinalKiloCost = reader.GetInt32(1);
                          car.MonthCost = reader.GetInt32(2);
                          car.WeekCost = reader.GetInt32(3);
                          car.KiloLimit = reader.GetInt32(4);
                          car.KiloNumber = reader.GetInt32(5);
                      }
                      if (await reader.NextResultAsync())
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
