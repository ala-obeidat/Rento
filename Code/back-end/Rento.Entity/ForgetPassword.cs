using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class ForgetPassword
    {
        public int UserId { get; set; }
        public Guid Token { get; set; }
        public string Mobile { get; set; }
    }
    public class ForgetPasswordCacheObject
    {
        public int UserId { get; set; }
        public int Code { get; set; }
    }
}
