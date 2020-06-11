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
    public class AccountManager
    {
        public static async Task<UserSession> Login(UserLogin userLogin)
        {
            UserSession userSession = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.LOGIN, delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Username", userLogin.Username);
                 cmd.Parameters.AddWithValue("@Password", userLogin.Password);
                 cmd.Parameters.AddWithValue("@Customer", userLogin.Customer);
                 cmd.Parameters.AddWithValue("@NotificationType", userLogin.NotificationType);

             },async  delegate (SqlDataReader reader)
             {
                 if (await reader.ReadAsync())
                 {
                     userSession = new UserSession
                     {
                         Id = reader.GetInt32(0),
                         Type = reader.GetInt32(1),
                         Username = reader.GetString(2)
                     };
                 }
             });
            return userSession;
        }

        public static async Task<UserSession> AdminLogin(int userId, AdminUserLogin data)
        {
            UserSession userSession = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.ADMIN_LOGIN, delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@UserId", userId);
                 cmd.Parameters.AddWithValue("@Password", data.Password);
                 cmd.Parameters.AddWithValue("@RequestUserId", data.RequestUserId);
             }, async delegate (SqlDataReader reader)
             {
                 if (await reader.ReadAsync())
                     userSession = new UserSession
                     {
                         Id = reader.GetInt32(0),
                         Type = reader.GetInt32(1),
                         Username = reader.GetString(2)
                     };
             });
            return userSession;
        }

        public static async Task<byte> GetUserType(int userId)
        {
            return (byte)await DataBaseHelper.Instance.ExecuteScaler<int>(StoredProcedure.LOGIN_GET_USER_TYPE, (SqlCommand cmd) =>
              {
                  cmd.Parameters.AddWithValue("@UserId", userId);
              });
        }

        public static async Task<RentoCountResponse<List<MobileCustomerResponse>>> ListMobileCustomer(MobileCustomerRequest data, int pageNumber, int pageSize)
        {
            var tempRowsCount = 0;
            List<MobileCustomerResponse> list = new List<MobileCustomerResponse>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.CUSTOMER_LIST, delegate (SqlCommand cmd)
             {

                 cmd.Parameters.AddWithValue("@PageSize", pageSize);
                 cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                 cmd.Parameters.AddWithValue("@Type", data.Type);

                 if (!string.IsNullOrEmpty(data.Name))
                     cmd.Parameters.AddWithValue("@Name", data.Name);
                 if (!string.IsNullOrEmpty(data.Mobile))
                     cmd.Parameters.AddWithValue("@Mobile", Helper.SMSMessage.CheckMobileNumber(data.Mobile));
             }, async delegate (SqlDataReader reader)
             {
                 if (await reader.ReadAsync())
                     tempRowsCount = reader.GetInt32(0);
                 if (reader.NextResult())
                 {
                     while (await reader.ReadAsync())
                         list.Add(new MobileCustomerResponse
                         {
                             Id = reader.GetInt32(0),
                             Username = reader.GetString(1),
                             FullName = reader.GetString(2),
                             Mobile = reader.GetString(3),
                             Type = reader.GetBoolean(4) ? (int)MobileType.Android : (int)MobileType.iPhone,
                             Status = (reader.GetInt32(5) - 10),
                             CreateDate = reader.GetDateTime(6)
                         });
                 }
             });
            
            return new RentoCountResponse<List<MobileCustomerResponse>>() {Data=list,RowsCount=tempRowsCount };
        }

        public static async Task<UserSession> Login(string loginToken)
        {
            UserSession userSession = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.LOGIN, delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Customer", true);
                 cmd.Parameters.AddWithValue("@LoginToken", loginToken);

             }, async delegate (SqlDataReader reader)
             {
                 if (await reader.ReadAsync())
                     userSession = new UserSession
                     {
                         Id = reader.GetInt32(0),
                         Type = reader.GetInt32(1),
                         Username = reader.GetString(2)
                     };
             });
            return userSession;
        }

        public static async Task<List<BaseNameEntity<UserType>>> List(UserType type)
        {
            var users = new List<BaseNameEntity<UserType>>();
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.USER_LIST, async delegate (SqlDataReader reader)
             {
                 while (await reader.ReadAsync())
                     users.Add(new BaseNameEntity<UserType>()
                     {
                         Id = reader.GetInt32(0),
                         Name = reader.GetString(1),
                         ExternalData = (UserType)reader.GetInt32(2)
                     });
             });
            if (type == UserType.Operation)
                return users.Where(u => u.ExternalData != UserType.Administrator).ToList();
            return users;
        }

        public static async Task<User> Select(int userId)
        {
            User user = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.PROVIDER_SELECT, delegate (SqlCommand cmd)
              {
                  cmd.Parameters.AddWithValue("@Id", userId);
              }, async delegate (SqlDataReader reader)
              {
                  if (await reader.ReadAsync())
                  {
                      user = new User()
                      {
                          Name = reader["Name"].ToString(),
                          Latitude = reader["Latitude"].ToString(),
                          Longitude = reader["Longitude"].ToString(),
                          Mobile = reader["Mobile"].ToString(),
                          Phone = reader["Phone"].ToString(),
                          TermsAndCondition = reader["TermsAndCondition"].ToString(),
                          CityId = Convert.ToInt32(reader["City"]),
                          CountryId = Convert.ToInt32(reader["CountryId"]),
                          WorkingTimeStart = Convert.ToInt32(reader["WorkFrom"]),
                          WorkingTimeEnd = Convert.ToInt32(reader["WorkTo"]),
                          WorkingTimeDays = Convert.ToInt32(reader["WorkDay"]),
                          LogoId = reader.IsDBNull(9) ? 0 : Convert.ToInt32(reader["Logo"]),
                          LicenceId = reader.IsDBNull(11) ? 0 : Convert.ToInt32(reader["Licence"]),
                          RefarmeCardId = reader.IsDBNull(12) ? 0 : Convert.ToInt32(reader["Freamcard"]),
                          Flag = Convert.ToInt32(reader["Flag"]),
                          OrganizationId = Convert.ToInt32(reader["Organization"]),
                          CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                      };
                  }
              });
            return user;
        }

        public static async Task RefreshToken(TokenRefresh data)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.TOKEN_REFRESH, delegate (SqlCommand cmd)
             {
                 if (data.LoginToken.Contains('-'))
                 {
                     cmd.Parameters.AddWithValue("@LoginToken", data.LoginToken);
                 }
                 else
                 {
                     cmd.Parameters.AddWithValue("@UserId", data.LoginToken.ToFlatString().Split(',')[1]);
                 }
                 cmd.Parameters.AddWithValue("@NotificationToken", data.NotificationToken);
             });
        }

        public static async Task<ForgetPassword> SelectMobile(string username)
        {
            ForgetPassword forgetPasswordResponse = null;
            await DataBaseHelper.Instance.ExecuteReader(StoredProcedure.CUSTOMER_SELECT_MOBILE,
                delegate (SqlCommand sqlCommand)
                {
                    sqlCommand.Parameters.AddWithValue("@Username", username);
                }, async delegate (SqlDataReader reader)
                 {
                     if (await reader.ReadAsync())
                     {
                         forgetPasswordResponse = new ForgetPassword()
                         {
                             UserId = reader.GetInt32(0),
                             Mobile = reader.GetString(1),
                             Token = Guid.NewGuid()
                         };
                     }
                 });
            return forgetPasswordResponse;
        }

        public static async Task<string> SelectMobile(int userId)
        {
            return await DataBaseHelper.Instance.ExecuteScaler<string>(StoredProcedure.CUSTOMER_SELECT_MOBILE,
                delegate (SqlCommand sqlCommand)
                {
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                });
        }

        public static async Task ResetPassword(int userId, string newPassword)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.CUSTOMER_RESET_PASSWORD, delegate (SqlCommand sqlCommand)
             {
                 sqlCommand.Parameters.AddWithValue("@UserId", userId);
                 sqlCommand.Parameters.AddWithValue("@Password", newPassword);
             });
        }

        public static async Task<int> SignUp(Customer data)
        {
            return await DataBaseHelper.Instance.ExecuteScaler<int>(StoredProcedure.CUSTOMER_SAVE, delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Id", data.Id);
                 cmd.Parameters.AddWithValue("@Username", data.Username.ToSafe());
                 cmd.Parameters.AddWithValue("@Mobile", data.Mobile.ToSafe());
                 cmd.Parameters.AddWithValue("@Password", data.Password.ToSafe());
                 cmd.Parameters.AddWithValue("@Email", data.Email.ToSafe());
                 cmd.Parameters.AddWithValue("@FullName", data.FullName.ToSafe());
                 cmd.Parameters.AddWithValue("@City", data.CityId == 0 ? 1 : data.CityId);
                 cmd.Parameters.AddWithValue("@DOP", data.DOP == DateTime.MinValue ? DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME) : data.DOP);
                 cmd.Parameters.AddWithValue("@Flag", data.Flag);
                 cmd.Parameters.AddWithValue("@Identifier", data.IdentifierId);
                 if (data.Identifier != null)
                 {
                     cmd.Parameters.AddWithValue("@IdentifierImage", data.Identifier.ContentArray);
                     cmd.Parameters.AddWithValue("@IdentifierSize", data.Identifier.ArrayLength);
                     cmd.Parameters.AddWithValue("@IdentifierFileName", data.Identifier.FileName);
                 }
                 cmd.Parameters.AddWithValue("@Licence", data.LicenceId);
                 if (data.Licence != null)
                 {
                     cmd.Parameters.AddWithValue("@LicenceImage", data.Licence.ContentArray);
                     cmd.Parameters.AddWithValue("@LicenceSize", data.Licence.ArrayLength);
                     cmd.Parameters.AddWithValue("@LicenceFileName", data.Licence.FileName);
                 }
                 cmd.Parameters.AddWithValue("@NotificationType", data.NotificationType);
                 cmd.Parameters.AddWithValue("@Date", DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                 cmd.Parameters.AddWithValue("@Gender", data.Gender);
             });
        }

        public static async Task<bool> ChangePassword(int id, ChangePassword data)
        {
            var success = false;
            var sqlData = await DataBaseHelper.Instance.ExecuteScaler<int>(StoredProcedure.PROVIDER_CHANGE_PASSWORD, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    cmd.Parameters.AddWithValue("@OldPassword", data.OldPassword);
                    cmd.Parameters.AddWithValue("@NewPassword", data.NewPassword);
                });
            success = sqlData == 1;
            return success;
        }

        public static async Task Verify(int userId)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.CUSTOMER_VERIFY, delegate (SqlCommand sqlCommand)
            {
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
            });
        }

        public static async Task Update(User data)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.PROVIDER_SAVE, delegate (SqlCommand cmd)
                 {
                     cmd.Parameters.AddWithValue("@Id", data.Id);
                     cmd.Parameters.AddWithValue("@Date", DateTime.Now.AddHours(Entity.Constant.ADDITINAL_HOUT_TIME));
                     cmd.Parameters.AddWithValue("@Name", data.Name.ToSafe());
                     cmd.Parameters.AddWithValue("@Latitude", data.Latitude);
                     cmd.Parameters.AddWithValue("@Longitude", data.Longitude);
                     cmd.Parameters.AddWithValue("@Mobile", data.Mobile.ToSafe());
                     cmd.Parameters.AddWithValue("@Phone", data.Phone.ToSafe());
                     cmd.Parameters.AddWithValue("@City", data.CityId);
                     cmd.Parameters.AddWithValue("@WorkFrom", data.WorkingTimeStart);
                     cmd.Parameters.AddWithValue("@WorkTo", data.WorkingTimeEnd);
                     cmd.Parameters.AddWithValue("@WorkDay", data.WorkingTimeDays);
                     cmd.Parameters.AddWithValue("@IicenceNumber", data.LicenceId);
                     if (data.OrganizationId > 0)
                         cmd.Parameters.AddWithValue("@OrganizationId", data.OrganizationId);

                     cmd.Parameters.AddWithValue("@TermsAndCondition", "");
                     cmd.Parameters.AddWithValue("@Flag", data.Flag);
                     if (data.Logo != null)
                     {
                         cmd.Parameters.AddWithValue("@LogoImage", data.Logo.Content);
                         cmd.Parameters.AddWithValue("@LogoSize", data.Logo.Content.Length);
                         cmd.Parameters.AddWithValue("@LogoFileName", data.Logo.FileName.ToSafe());
                     }
                     if (data.Licence != null)
                     {
                         cmd.Parameters.AddWithValue("@LicenceImage", data.Licence.Content);
                         cmd.Parameters.AddWithValue("@LicenceSize", data.Licence.Content.Length);
                         cmd.Parameters.AddWithValue("@LicenceFileName", data.Licence.FileName.ToSafe());
                     }
                     if (data.RefarmeCard != null)
                     {
                         cmd.Parameters.AddWithValue("@FreamcardImage", data.RefarmeCard.Content);
                         cmd.Parameters.AddWithValue("@FreamcardSize", data.RefarmeCard.Content.Length);
                         cmd.Parameters.AddWithValue("@FreamcardFileName", data.RefarmeCard.FileName.ToSafe());
                     }
                 });
        }

        public static async Task UpdateCustomer(CustomerOptinal data)
        {
            await DataBaseHelper.Instance.ExecuteNonQuery(StoredProcedure.CUSTOMER_UPDATE, delegate (SqlCommand cmd)
             {
                 cmd.Parameters.AddWithValue("@Id", data.Id);
                 cmd.Parameters.AddWithValue("@Flag", data.Flag);
                 cmd.Parameters.AddWithValue("@DOP", data.DOP);
                 cmd.Parameters.AddWithValue("@IdentifierId", data.IdentifierId);
                 cmd.Parameters.AddWithValue("@Gender", data.Gender);
                 cmd.Parameters.AddWithValue("@IdentifierFileName", data.Identifier.FileName);
                 cmd.Parameters.AddWithValue("@IdentifierSize", data.Identifier.ArrayLength);
                 cmd.Parameters.AddWithValue("@IdentifierImage", data.Identifier.ContentArray);
                 cmd.Parameters.AddWithValue("@LicenceFileName", data.Licence.FileName);
                 cmd.Parameters.AddWithValue("@LicenceSize", data.Licence.ArrayLength);
                 cmd.Parameters.AddWithValue("@LicenceImage", data.Licence.ContentArray);
             });
        }
    }
}
