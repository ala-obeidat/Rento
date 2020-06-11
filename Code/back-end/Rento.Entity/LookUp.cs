using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class LookUp : BaseNameEntity
    {
        public string LookUpName { get; set; }
    }
    public class LookUp<T>: BaseNameEntity<T>
    {
        public string LookUpName { get; set; }
    }
    public class LookUpDelete
    {
        public string LookUpName { get; set; }
        public int Id { get; set; }
    }
}
