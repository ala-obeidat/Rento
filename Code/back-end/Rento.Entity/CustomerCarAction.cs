using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public enum CustomerCarAction
    {
        Pending = 0,
        Processing = 1,
        Approved = 2,
        OnTheWay = 3,
        Delivered = 4,
        Done = 5,
        Rejected = 6,
        CustomerRejected = 7,
        OfficeCutoff = 8
    }
}
