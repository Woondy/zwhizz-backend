using Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace Application.Security
{
    public static class Hasher
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string plainText, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, hashedPassword);
        }

        public static string HashData(string data, HashAlgorithmType algorithmType)
        {
            ArgumentNullException.ThrowIfNull(data);

            return algorithmType switch
            {
                HashAlgorithmType.SHA256 => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(data))),
                HashAlgorithmType.MD5 => Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(data))),
                _ => throw new ArgumentOutOfRangeException(nameof(algorithmType), algorithmType, "Unsupported hashing algorithm"),
            };
        }
    }
}
