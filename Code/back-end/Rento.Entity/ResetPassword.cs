using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class ResetPassword : Verification
    {
       public string NewPassword { get; set; }
    }
    public class ChangePassword 
    {
        public string OldPassword { get; set; }
        public string PasswordKey { get; set; }
        public string NewPassword { get; set; }
    }
}
