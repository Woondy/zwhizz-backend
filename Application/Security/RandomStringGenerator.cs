using System.Security.Cryptography;

namespace Application.Security
{
    public class RandomStringGenerator
    {
        public static byte[] GenerateRandomBytes(int byteSize = 32)
        {
            if (byteSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(byteSize), "Byte size must be positive.");
            }

            byte[] randomBytes = new byte[byteSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return randomBytes;
        }

        public static string GenerateRandomBase64UrlString(int byteSize = 32)
        {
            var randomBytes = GenerateRandomBytes(byteSize);
            var base64String = Convert.ToBase64String(randomBytes);
            return base64String.Replace('+', '-').Replace('/', '_');
        }
    }
}
