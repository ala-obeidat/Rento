using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class RentoRequest
    {
        public string Token { get; set; }
        public string ApplicationKey { get; set; }
        public int PageNumber { get; set; }
        public int Language { get; set; }
    }

    public enum Language
    {
        Arabic = 0,
        English = 1
    }

    public class RentoRequest<T> : RentoRequest
    {
        public T Data { get; set; }
    }
}
