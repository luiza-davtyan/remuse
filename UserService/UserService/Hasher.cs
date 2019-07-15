using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserService
{
    /// <summary>
    /// Helper class for project functionality.
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// SHA256 Hash of your string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
