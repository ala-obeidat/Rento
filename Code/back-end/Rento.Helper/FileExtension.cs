using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Helper
{
    public static class FileExtension
    {

        [HandleProcessCorruptedStateExceptions]
        public static bool ScanImage(byte[] content)
        {

            Dictionary<string, List<byte[]>> ArrFileSignatures = GetFileSignatures();
            List<byte[]> tmp;
            if (ArrFileSignatures.TryGetValue("IMAGE", out tmp))
            {
                return ComparyHeaders(tmp, content);
            }
            else
            {
                return false;
            }
        }
        

        private static bool ComparyHeaders(List<byte[]> targets, byte[] content)
        {
            byte[] source = new byte[8];
            source = content.Take(8).ToArray();
            bool result = false;

            foreach (var target in targets)
            {
                bool innerResult = false;
                for (int i = 0; i < target.Length; i++)
                {
                    if (target[i] != source[i])
                    {
                        innerResult = false;
                        break;
                    }
                    else
                        innerResult = true;
                }

                if (innerResult)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private static Dictionary<string, List<byte[]>> GetFileSignatures()
        {
            Dictionary<string, List<byte[]>> ArrFileSignatures = new Dictionary<string, List<byte[]>>();

            ArrFileSignatures.Add("IMAGE", new List<byte[]>()
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
                new byte[] { 0x49, 0x20, 0x49 },
                new byte[] { 0x49, 0x49, 0x2A, 0x00  },
                new byte[] { 0x4D, 0x4D, 0x00, 0x2A  },
                new byte[] { 0x4D, 0x4D, 0x00, 0x2B },
                new byte[] { 0x49, 0x20, 0x49 },
                new byte[] { 0x49, 0x49, 0x2A, 0x00  },
                new byte[] { 0x4D, 0x4D, 0x00, 0x2A  },
                new byte[] { 0x4D, 0x4D, 0x00, 0x2B },
                new byte[] { 0x42, 0x4D },
                new byte[] { 0x47, 0x49, 0x46, 0x38 }
            });

            return ArrFileSignatures;

        }




    }
}
