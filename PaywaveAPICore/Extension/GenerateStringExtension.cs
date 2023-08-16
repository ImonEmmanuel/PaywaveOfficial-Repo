using System;
using System.Security.Cryptography;

namespace PaywaveAPICore.Extension
{
    public class GenerateStringExtension
    {
        public static string GenerateRandomBase64(int length = 32)
        {
            byte[] buffer = new byte[Convert.ToInt32(length)];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }

            return Convert.ToBase64String(buffer);
        }
        public static string GenerateRandomNumber(int lenght)
        {
            Random random = new Random();
            string number ="";
            for (int i = 0; i < lenght; i++)
            {
                number = $"{number}{random.Next(9).ToString()}";
            }
            return number;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
