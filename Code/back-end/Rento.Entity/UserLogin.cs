using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Customer { get; set; }
        public bool IsOperation { get; set; }
        /// <summary>
        /// Android=true,
        /// IOS=false
        /// </summary>
        public bool NotificationType { get; set; }
    }
    public enum NotificationType
    {
        Android = 0,
        IOS = 1
    }
    public class TokenRefresh
    {
        public string LoginToken { get; set; }
        public string NotificationToken { get; set; }
    }
    public class AdminUserLogin
    {
        public int RequestUserId { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
    }
}
