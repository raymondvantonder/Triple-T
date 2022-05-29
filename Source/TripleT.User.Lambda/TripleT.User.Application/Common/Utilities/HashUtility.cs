using System;
using System.Security.Cryptography;
using System.Text;

namespace TripleT.User.Application.Common.Utilities
{
    public static class HashUtility
    {
        public static string CreateSha256HashBase64String(string value)
        {
            var encoding = new UnicodeEncoding();

            using var sHa256 = SHA256.Create();
            
            var unicodeBytes = encoding.GetBytes(value);

            return Convert.ToBase64String(sHa256.ComputeHash(unicodeBytes));
        }
    }
}
