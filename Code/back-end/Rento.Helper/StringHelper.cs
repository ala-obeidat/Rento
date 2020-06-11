namespace Rento.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringHelper
    {
        private const string Key = " jJ9eutBMue44KhPGZp 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";
        private static System.Random Random = new System.Random();

        public static string GetUniqeKey()
        {
            return (Guid.NewGuid().ToString()
                + "علاءعبيدات ّ!@#$%^&*)(ًٌَُلإ|*؟:"
                + GenerateRandomNumber(6).ToString().ToSafeEncrepted()).ToHashString();
        }

        public static string ToRentoBase64(this string str, string key)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new List<string>();
            var tempCode = str;
            while (tempCode.Length > 0)
            {
                if (tempCode.Length > 2)
                {
                    result.Add(tempCode.Substring(0, 3));
                    tempCode = tempCode.Substring(3, tempCode.Length - 3);
                }
                else
                {
                    result.Add(tempCode.Substring(0, tempCode.Length));
                    tempCode = string.Empty;
                }
            }
            return ReverseString(Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(key, result))));
        }

        public static string FromRentoBase64(this string str, string key)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return string.Join("", Encoding.UTF8.GetString(Convert.FromBase64String(ReverseString(str))).Replace(key, ""));
        }

        public static string ToFlatString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            str = Encoding.UTF8.GetString(Convert.FromBase64String(str));
            int num = (str.Length - 4) / 2;
            char[] chArray = new char[] { (char)(str[0] ^ 'a'), (char)(str[1] ^ 's'), (char)(str[2] ^ 'e'), (char)(str[3] ^ 'l') };
            char[] chArray2 = new char[num];
            int num2 = Convert.ToInt32(new string(chArray));
            for (int i = 0; i < num; i++)
            {
                chArray2[i] = (char)(str[(i * 2) + 4] ^ Key[num2 + i]);
            }
            return new string(chArray2).ToSafe();
        }

        /// <summary>
        /// Get String as Reverse.
        /// </summary>
        /// <param name="input">String to be reverse</param>
        /// <returns>[input] Reversed</returns>
        public static string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string ToSafe(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            return Regex.Replace(source, "<.*?>", string.Empty).Trim();
        }

        public static bool CheckRegexValid(this string source, string pattern)
        {
            return Regex.IsMatch(source, pattern);
        }

        public static string ToEnglishChar(this string source)
        {
            return Regex.Replace(source, "[^0-9a-zA-Z]+", "");
        }
        public static string ToSafeEncrepted(this string source)
        {
            return ToEncreptedString(source.ToSafe());
        }

        public static string ToEncreptedString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            string str2 = Guid.NewGuid().ToString().Replace("-", "");
            char[] chArray = new char[(str.Length * 2) + 4];
            string str3 = string.Empty;
            int num = Random.Next(0, 2502 - str.Length);
            if (num < 10)
            {
                str3 = "000" + num;
            }
            else if (num < 100)
            {
                str3 = "00" + num;
            }
            else if (num < 1000)
            {
                str3 = "0" + num;
            }
            else
            {
                str3 = num.ToString();
            }
            chArray[0] = (char)(str3[0] ^ 'a');
            chArray[1] = (char)(str3[1] ^ 's');
            chArray[2] = (char)(str3[2] ^ 'e');
            chArray[3] = (char)(str3[3] ^ 'l');
            for (int i = 0; i < str.Length; i++)
            {
                chArray[4 + (i * 2)] = (char)(str[i] ^ Key[num + i]);
                chArray[5 + (i * 2)] = str2[i % 30];
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(chArray));
        }
        /// <summary>
        /// Get Hash of string.
        /// </summary>
        /// <param name="input">String to get hash for it</param>
        /// <returns>[input] hash</returns>
        public static string ToHashString(this string input)
        {
            var bytes = Encoding.Unicode.GetBytes(input);
            byte[] hash = null;
            var result = new StringBuilder();
            using (var hashstring = new SHA256Managed())
                hash = hashstring.ComputeHash(bytes);
            foreach (byte x in hash)
                result.AppendFormat("{0:x2}", x);
            return result.ToString();
        }
        public static int GenerateRandomNumber(int digit)
        {
            return Random.Next(Convert.ToInt32(Math.Pow(10, digit)), Convert.ToInt32(Math.Pow(10, digit + 1) - 1));
        }
    }
}
