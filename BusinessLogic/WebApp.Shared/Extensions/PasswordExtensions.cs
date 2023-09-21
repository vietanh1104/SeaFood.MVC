using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Shared.Extensions
{
    public static class PasswordExtensions
    {
        public static string GetPasswordHash(string password, string? salt = "demo")
        {
            // using md5 hash
            return md5_hash($"{password}_{salt}");
        }
        
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        public static String md5_hash(String value)
        {
            string result = "";
            using (MD5 hash = MD5.Create())
            {
                result = String.Join
                (
                    "",
                    from ba in hash.ComputeHash
                    (
                        Encoding.UTF8.GetBytes(value)
                    )
                    select ba.ToString("x2")
                );
            }
            return result;
        }

    }
}
