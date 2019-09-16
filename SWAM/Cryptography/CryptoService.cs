using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Cryptography
{
    /// <summary>
    /// Creating password salt and calculates hashed password.
    /// </summary>
    public class CryptoService
    {
        // 24 = 192 bits

        /// <summary>
        /// Size of salt array.
        /// </summary>
        private const int SALT_BYTE_SIZE = 24;
        /// <summary>
        /// Size of hashed password array.
        /// </summary>
        private const int HASH_BYTE_SIZE = 24;
        /// <summary>
        /// Iteration for operation.
        /// </summary>
        private const int HASING_ITERATION_COUNT = 14128;

        /// <summary>
        /// Calculates the hashed password for an array byte using the password provided and the password salt.
        /// </summary>
        /// <param name="password">Inputed password</param>
        /// <param name="salt">Password salt from database or GenerateSalt method.</param>
        /// <param name="iterations">Iteration for operation. Minimum value is 1000.</param>
        /// <param name="hashByteSize">Size of the final hashed function it's should be the same as size of salt</param>
        /// <returns>If all calculations were correct returns hashed password in byte array. Else 0 array.</returns>
        public static byte[] ComputeHash(string password, byte[] salt, int iterations = HASING_ITERATION_COUNT, int hashByteSize = HASH_BYTE_SIZE)
        {
            if (salt != null && iterations >= 1000)
            {
                using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt))
                {
                    hashGenerator.IterationCount = iterations;
                    return hashGenerator.GetBytes(hashByteSize);
                }
            }
            else return new byte[0];
        }

        /// <summary>
        /// Generate password salt.
        /// </summary>
        /// <param name="saltByteSize">Size of the password salt it's should be the same as size of password in bytes</param>
        /// <returns>Password salt in byte array.</returns>
        public static byte[] GenerateSalt(int saltByteSize = SALT_BYTE_SIZE)
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
