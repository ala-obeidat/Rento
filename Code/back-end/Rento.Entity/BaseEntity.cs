using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class BaseNameJust
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class BaseNameEntity : BaseNameJust
    {
        public string NameEn { get; set; }
    }

    public class BaseNameEntity<T> : BaseNameEntity
    {
        public T ExternalData { get; set; }
    }
}
