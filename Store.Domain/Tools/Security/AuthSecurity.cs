using System.Security.Cryptography;
using System.Text;

namespace Store.Domain.Tools.Security
{
    public class AuthSecurity
    {
        public static string GenerateHash(string password, byte[] salt)
        {
            byte[] hashedPassword;
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];
            passwordBytes.CopyTo(saltedPassword, 0);
            salt.CopyTo(saltedPassword, passwordBytes.Length);
            using var hash = new SHA256CryptoServiceProvider();
            {
                hash.ComputeHash(saltedPassword);
                hashedPassword = hash.Hash;
                return BytesToString(hashedPassword);
            }
        }
        public static byte[] GenerateSalt()
        {
            Random random = new Random();
            int SaltLength = random.Next(30, 45);
            byte[] salt = new byte[SaltLength];
            var rngRandom = RandomNumberGenerator.Create();
            rngRandom.GetBytes(salt);
            return salt;
        }

        public static string BytesToString(byte[] bytes)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                str.AppendFormat("{0:X2}", bytes[i]);
            }
            return str.ToString();
        }
        public static byte[] StringToByte(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
