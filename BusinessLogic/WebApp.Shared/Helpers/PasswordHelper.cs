﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Shared.Helpers
{
    public static class PasswordHelper
    {
        public static string GetPasswordHash(string password, string? salt = "demo")
        {
            // using md5 hash
            return md5_hash($"{password}_{salt}");
        }

        public static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        public static string md5_hash(string value)
        {
            string result = "";
            using (MD5 hash = MD5.Create())
            {
                result = string.Join
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
