using System.Security.Cryptography;
using System.Text;

namespace TripleT.Application.Common.Utilities
{
    public static class HashUtility
    {
        public static byte[] CreateSHA256Hash(string value)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();

            using (SHA256 sHA256 = SHA256.Create())
            {
                var unicodeBytes = encoding.GetBytes(value);

                return sHA256.ComputeHash(unicodeBytes);
            }
        }
    }
}
