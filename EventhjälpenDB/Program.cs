using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Eventhjälpen.Models;

namespace EventhjälpenDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Hasher hs = new Hasher();

            Console.WriteLine(hs.GetHashPw("felkan"));
            Console.ReadLine();
        }
    }

    public class Hasher
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        protected int saltLength = 32;


        private static byte[] GetSalt(int maxSaltLength)
        {
            var salt = new byte[maxSaltLength];
            using (rngCsp)
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            return salt;
        }

        private string HashPw(string input)
        {
            using (SHA256 shaHash = SHA256.Create())
            {
                byte[] bytes = shaHash.ComputeHash(Encoding.UTF8.GetBytes(input + GetSalt(saltLength)));

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

        public string GetHashPw(string input)
        {
            var pw = HashPw(input);
            return pw;
        }

    }
}

