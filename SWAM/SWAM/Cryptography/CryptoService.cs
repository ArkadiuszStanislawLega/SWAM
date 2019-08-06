using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Cryptography
{
    public class CryptoService
    {
        // 24 = 192 bits
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 14128;

        public static byte[] ComputeHash(string password, byte[] salt, int iterations = HasingIterationsCount, int hashByteSize = HashByteSize)
        {
            using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt))
            {
                hashGenerator.IterationCount = iterations;
                return hashGenerator.GetBytes(hashByteSize);
            }
        }

        public static byte[] GenerateSalt(int saltByteSize = SaltByteSize)
        {
            using (RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltByteSize];
                saltGenerator.GetBytes(salt);
                return salt;
            }
        }
    }
}
