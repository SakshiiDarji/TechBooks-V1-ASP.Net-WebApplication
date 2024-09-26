using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace TechBooks.WebUI.Login
{
    public static class Security
    {
        public static string ComputePasswordHash(string userName, string password)
        {
            string pepper = ConfigurationManager.AppSettings["Pepper"];
            int keySize = 64;
            int iterations = 1000;
            var hashAlgorithName = HashAlgorithmName.SHA512;
            var salt = ComputeSalt(userName, pepper);
            var r = new Rfc2898DeriveBytes(password, salt, iterations, hashAlgorithName);
            string result = Convert.ToBase64String(r.GetBytes(keySize));
            return result;
        }

        public static byte[] ComputeSalt(string userName, string pepper)
        {
            byte[] result = Encoding.UTF8.GetBytes(userName + pepper);
            return result;
        }
    }
}