using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blurr.Encryption
{
    public class Hash
    {
        private Hash() {  }

        /// <summary>
        /// Calculate hash from string
        /// </summary>
        /// <param name="input">The string to calculate hash from</param>
        /// <returns>The hash string</returns>
        public static string CalculateHash(string input, HashType hashType)
        {
            HashAlgorithm hashAlgorithm;
            switch (hashType)
            {
                case HashType.MD5:
                    hashAlgorithm = MD5.Create();
                    break;
                case HashType.SHA1:
                    hashAlgorithm = SHA1.Create();
                    break;
                case HashType.SHA256:
                    hashAlgorithm = SHA256.Create();
                    break;
                case HashType.SHA384:
                    hashAlgorithm = SHA384.Create();
                    break;
                case HashType.SHA512:
                    hashAlgorithm = SHA512.Create();
                    break;
                default:
                    hashAlgorithm = MD5.Create();
                    break;
            }

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = hashAlgorithm.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Verify that the hash matches
        /// </summary>
        /// <param name="input">The string to compare</param>
        /// <param name="hash">The hash to compare</param>
        /// <param name="hashType">The algorithm used for hash</param>
        /// <returns>Returns true if the two hashes match</returns>
        public static bool VerifyHash(string input, string hash, HashType hashType)
        {
            string testHash = CalculateHash(input, hashType);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(testHash, hash) == 0;
        }
    }

    public enum HashType
    {
        MD5, SHA1, SHA256, SHA384, SHA512
    }
}
