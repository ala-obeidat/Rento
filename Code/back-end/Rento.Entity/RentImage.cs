using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class BaseRentoImage
    {
        public string FileName { get; set; }
    }
    public class RentoImage : BaseRentoImage
    {
        public byte[] Content { get; set; }
    }
    public class Base64RentoImage : BaseRentoImage
    {
        public string Content { get; set; }
        public int ArrayLength { get; set; }
        public byte[] ContentArray
        {
            get
            {
                if (string.IsNullOrEmpty(Content))
                    return null;
                var array= System.Convert.FromBase64String(Content);
                ArrayLength = array.Length;
                return array;
            }
        }
    }
}
