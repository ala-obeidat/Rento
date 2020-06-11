using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Database
{
    public class StoredProcedure
    {
        public const string USER_INSERT = "User_Insert";
        public const string USER_LIST = "User_List";

        public const string OFFER_LIST = "Offer_List";

        public const string LOGIN = "User_Login";
        public const string LOGIN_GET_USER_TYPE = "Login_GetUserType";
        public const string ADMIN_LOGIN = "Admin_Login";

        public const string PROVIDER_SELECT = "Provider_Select";
        public const string PROVIDER_SELECT_TIRMS = "Provider_SelectTirms";
        public const string PROVIDER_SAVE = "Provider_Save";
        public const string PROVIDER_LIST = "Provider_List";
        public const string PROVIDER_PLACE_SAVE = "ProviderPlace_Save";

        public const string IMAGEDATA_SELECT = "ImageData_Select";

        public const string CLIENT_LIST = "Client_List";


        public const string CAR_IMAGE_SAVE = "Car_ImageSave";
        public const string CAR_IMAGE_DELETE = "Car_ImageDelete";

        public const string CAR_SAVE = "Car_Save";
        public const string CAR_DELETE = "Car_Delete";
        public const string CAR_LIST = "Car_List";
        public const string CAR_SELECT = "Car_Select";

        public const string LOOKUP_LIST = "LookUp_List";
        public const string LOOKUP_SAVE = "LookUp_Save";
        public const string LOOKUP_DELETE = "LookUp_Delete";

        public const string MESSAGE_SELECT = "Message_Select";
        public const string MESSAGE_CREATE = "Message_Create";

        public const string MESSAGE_SELECT_DEVICE_TOKENS = "Message_SelectTokens";

        
        public const string USER_UPDATEFLAG = "User_UpdateFlag_new";

        public const string CUSTOMER_UPDATE = "Customer_Update";
        public const string CUSTOMER_SAVE = "Customer_Save";
        public const string CUSTOMER_VERIFY = "Customer_Verify";
        public const string CUSTOMER_RESET_PASSWORD = "Customer_ResetPassword";
        public const string CUSTOMER_SELECT_MOBILE = "Customer_SelectMobile";
        public const string CUSTOMER_CAR_REQUEST_LIST = "Customer_CarRequestList";
        public const string CUSTOMER_LIST = "Customer_List";

        public const string PROVIDER_CHANGE_PASSWORD = "Provider_ChangePassword";

        public const string CAR_CHECKOUT = "Car_Checkout";
        public const string CAR_CHANGE_STATUS = "Car_ChangeStatus";
        public const string CHECKOUT_LIST_PENDING = "Checkout_ListPending";
        public const string CAR_CLOSE_BOOKING = "Car_CloseBooking";

        public const string ORDER_VIEW = "Order_View";
        public const string ORDER_CLOSE = "Order_Close";
        public const string ORDER_ACTION = "Order_Action";
        public const string ORDER_LIST = "Order_List";

        public const string TOKEN_REFRESH = "Token_Refresh";

        public const string ORGANIZATION_SELECT = "Organization_Select";
        public const string ORGANIZATION_SAVE = "Organization_Save";
        public const string ORGANIZATION_LIST = "Organization_List";

    }
}
