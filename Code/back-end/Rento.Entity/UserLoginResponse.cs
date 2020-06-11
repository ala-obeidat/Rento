using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class UserLoginResponse : UserSession
    {
        public string Token { get; set; }
    }
}
