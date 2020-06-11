using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    /// <summary>
    /// After add Error code, please do not forget to add message for it in ErrorMessageLan.InitiatMessages()
    /// </summary>
    public enum ErrorCode
    {
        Success = 0,
        AccessDenai = 1,
        UserDoesNotExist = 2,
        GeneralError = 3,
        InvalidCode = 4,
        RequirdField = 5,
        UsernameAlreadyExists = 6,
        InvalidPasswordFormat = 7,
        InActiveUser = 8,
        CarAlreadyReserved= 9,
        ProviderRejectRequest=10,
        OldPasswordNotMatch=11,
        CustomerNonComplete=12,
        CarHasCheckout=13,
        BlockedUser=14,
        CarLinkWithOrder = 15,
        InvalidImageFormat=16,
    }
    public class ErrorMessageLan
    {
        public string ArabicText { get; set; }
        public string EnglishText { get; set; }
        public static Dictionary<ErrorCode, ErrorMessageLan> InitiatMessages()
        {
            var tempDic = new Dictionary<ErrorCode, ErrorMessageLan>();
            tempDic.Add(ErrorCode.Success, new ErrorMessageLan()
            {
                ArabicText = "تمت العملية بنجاح",
                EnglishText = "Action done successful"
            });
            tempDic.Add(ErrorCode.AccessDenai, new ErrorMessageLan()
            {
                ArabicText = "تم انتهاء مدة الجلسة، الرجاء إعادة تسجيل الدخول",
                EnglishText = "You do not have access for this action or you did not login successfully"
            });

            tempDic.Add(ErrorCode.CarAlreadyReserved, new ErrorMessageLan()
            {
                ArabicText = "لا تستطيع حجز سيارة محجوزة مسبقاً",
                EnglishText = "You can not reserve car already reserved"
            });

            tempDic.Add(ErrorCode.GeneralError, new ErrorMessageLan()
            {
                ArabicText = "لا يمكن تنفيذ الاجراء حالياً، الرجاء مراجعة الدعم",
                EnglishText = "You can not do this action now, please contact support"
            });
             
            tempDic.Add(ErrorCode.InActiveUser, new ErrorMessageLan()
            {
                ArabicText = "المستخدم غير مفعل",
                EnglishText = "User not active"
            });

            tempDic.Add(ErrorCode.InvalidCode, new ErrorMessageLan()
            {
                ArabicText = "الرمز خاطئ",
                EnglishText = "Invalid code"
            });

            tempDic.Add(ErrorCode.InvalidPasswordFormat, new ErrorMessageLan()
            {
                ArabicText = "صيغة كلمة المرور يجب أن تحتوي حرف انجليزي كبير وآخر صغير وأرقام ويكون طولها من 5-16 حرف",
                EnglishText = "Password must contain Capital, small letter and numbers and it must be 5 - 16 long"
            });

            tempDic.Add(ErrorCode.OldPasswordNotMatch, new ErrorMessageLan()
            {
                ArabicText = "كلمة المرور القديمة غير صحيحة",
                EnglishText = "Old password does not match what you enterd"
            });

            tempDic.Add(ErrorCode.ProviderRejectRequest, new ErrorMessageLan()
            {
                ArabicText = "قام المكتب برفض العملية",
                EnglishText = "Provider office reject this action"
            });

            tempDic.Add(ErrorCode.RequirdField, new ErrorMessageLan()
            {
                ArabicText = "حقل إجباري مفقود",
                EnglishText = "Empty required field"
            });

            tempDic.Add(ErrorCode.UserDoesNotExist, new ErrorMessageLan()
            {
                ArabicText = "اسم المستخدم أو كلمة المرور خاطئة",
                EnglishText = "Username or password is incorrect"
            });

            tempDic.Add(ErrorCode.UsernameAlreadyExists, new ErrorMessageLan()
            {
                ArabicText = "اسم المستخدم أو رقم الجوال موجود مسبقاً",
                EnglishText = "This username or mobile number is already exist"
            });
            tempDic.Add(ErrorCode.CustomerNonComplete, new ErrorMessageLan()
            {
                ArabicText = "يجب تعبئة البيانات الضرورية لعملية التأجير",
                EnglishText = "You must complete fill requird information for book a car"
            });
            tempDic.Add(ErrorCode.CarHasCheckout, new ErrorMessageLan()
            {
                ArabicText = "لا يمكن حذف سيارة مرتبطة بطلب",
                EnglishText = "You can't delete car has order"
            });
            tempDic.Add(ErrorCode.BlockedUser, new ErrorMessageLan()
            {
                ArabicText = "المستخدم موقوف",
                EnglishText = "User is blocked"
            });
            tempDic.Add(ErrorCode.CarLinkWithOrder, new ErrorMessageLan()
            {
                ArabicText = "لا يمكن حذف سيارة  انشاء طلب عليها",
                EnglishText = "Can not delete car there is exist order for it"
            });
            tempDic.Add(ErrorCode.InvalidImageFormat, new ErrorMessageLan()
            {
                ArabicText = "صورة غير صالحة",
                EnglishText = "Invalid Image file"
            });

            

            return tempDic;
        }
    }
    
}
