using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public enum UserType
    {
        Pending = 0,
        Active = 1,
        Blocked = 2,
        Administrator = 3,
        Operation=4,

        Customer_Pending=10,
        Customer_Active = 11,
        Customer_Blocked = 12,
    }
}
