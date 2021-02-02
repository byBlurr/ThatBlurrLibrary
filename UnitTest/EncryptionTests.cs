using Blurr.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Tests for the encryption methods
    /// </summary>
    [TestClass]
    public class EncryptionTests
    {
        /// <summary>
        /// Test CalculateHash() and VerifyHash()
        /// </summary>
        [TestMethod]
        public void HashTest()
        {
            string input = "APasswordMaybe";
            string expectedHashMD5 = "12516856a659f9e8bb06b65e17d31259";
            string expectedHashSHA1 = "22d8013f906edc0b3dbfeffa79513dca0ea2f688";
            string expectedHashSHA256 = "2f39872bd00992bb84f8cd246a0523cca2ef263723dd00ef7366bc6c0522bf8a";
            string expectedHashSHA384 = "e243936613776aa1da53d9237ca0d849bb8e7d4fa5b32843fc7153a3b0a4d3cf5d3ab961ad1214dffeeb9bad612a797a";
            string expectedHashSHA512 = "7c122fae3d05a6dcd3769c8d4387cedb34898c8222cf7b749acd3e90d478ec84da4d06625850080e559a42780619e9b1de460e7cedbfc458c5fe3093dbe47dcc";

            Assert.AreEqual(expectedHashMD5, Hash.CalculateHash(input, HashType.MD5));
            Assert.IsTrue(Hash.VerifyHash(input, expectedHashMD5, HashType.MD5));

            Assert.AreEqual(expectedHashSHA1, Hash.CalculateHash(input, HashType.SHA1));
            Assert.IsTrue(Hash.VerifyHash(input, expectedHashSHA1, HashType.SHA1));

            Assert.AreEqual(expectedHashSHA256, Hash.CalculateHash(input, HashType.SHA256));
            Assert.IsTrue(Hash.VerifyHash(input, expectedHashSHA256, HashType.SHA256));

            Assert.AreEqual(expectedHashSHA384, Hash.CalculateHash(input, HashType.SHA384));
            Assert.IsTrue(Hash.VerifyHash(input, expectedHashSHA384, HashType.SHA384));

            Assert.AreEqual(expectedHashSHA512, Hash.CalculateHash(input, HashType.SHA512));
            Assert.IsTrue(Hash.VerifyHash(input, expectedHashSHA512, HashType.SHA512));
        }
    }
}
