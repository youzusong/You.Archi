using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace You.Archi.Crypto.Utilities
{
    public static class CryptoUtility
    {
        private static readonly string[] RsaAlgorithmNames = new string[] { "RSA/ECB/PKCS1Padding", "RSA/ECB/OAEPWithSHA-256AndMGF1Padding" };

        static CryptoUtility()
        { }

        #region MD5

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="orgText">明文</param>
        /// <returns>密文</returns>
        public static string MD5Encrypt(string orgText)
        {
            byte[] orgData = Encoding.UTF8.GetBytes(orgText);
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashData = md5.ComputeHash(orgData);
                StringBuilder hashStr = new StringBuilder();
                foreach (byte b in hashData)
                {
                    hashStr.AppendFormat("{0:X2}", b);
                }
                return hashStr.ToString();
            }
        }

        #endregion

        #region AES



        #endregion

        #region RSA

        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <param name="length">位长</param>
        /// <returns></returns>
        public static RsaKeyPairPem GenRsaKeyPairPem(int strength)
        {
            RsaKeyPairPem keyPairText = new RsaKeyPairPem();

            RsaKeyPairGenerator rsaKeyPairGentor = new RsaKeyPairGenerator();
            rsaKeyPairGentor.Init(new KeyGenerationParameters(new SecureRandom(), strength));
            AsymmetricCipherKeyPair keyPair = rsaKeyPairGentor.GenerateKeyPair();

            using (var txtWriter = new StringWriter())
            {
                PemWriter permWriter = new PemWriter(txtWriter);
                permWriter.WriteObject(keyPair.Public);
                permWriter.Writer.Flush();
                keyPairText.PublicKeyPem = txtWriter.ToString();
            }

            using (var txtWriter = new StringWriter())
            {
                PemWriter permWriter = new PemWriter(txtWriter);
                permWriter.WriteObject(keyPair.Private);
                permWriter.Writer.Flush();
                keyPairText.PrivateKeyPem = txtWriter.ToString();
            }

            return keyPairText;
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="decText">明文</param>
        /// <param name="publicKeyPem">公钥</param>
        /// <returns>密文</returns>
        public static string RsaEncrypt(string decText, string publicKeyPem)
        {
            AsymmetricKeyParameter keyParams;
            using (PemReader reader = new PemReader(new StringReader(publicKeyPem)))
            {
                keyParams = (AsymmetricKeyParameter)reader.ReadObject();
            }

            IAsymmetricBlockCipher rsa = new RsaEngine();
            rsa.Init(true, keyParams);

            byte[] decData = Encoding.UTF8.GetBytes(decText);
            byte[] encData = rsa.ProcessBlock(decData, 0, decData.Length);
            string encText = Convert.ToBase64String(encData);
            return encText;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="encText">密文</param>
        /// <param name="privateKeyPem">私钥</param>
        /// <returns>明文</returns>
        public static string RsaDecrypt(string encText, string privateKeyPem)
        {
            AsymmetricKeyParameter keyParams;
            using (PemReader reader = new PemReader(new StringReader(privateKeyPem)))
            {
                keyParams = ((AsymmetricCipherKeyPair)reader.ReadObject()).Private;
            }

            IAsymmetricBlockCipher rsa = new RsaEngine();
            rsa.Init(false, keyParams);

            byte[] encData = Convert.FromBase64String(encText);
            byte[] decData = rsa.ProcessBlock(encData, 0, encData.Length);
            string decText = Encoding.UTF8.GetString(decData);
            return decText;
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="decText">明文</param>
        /// <param name="publicKeyPem">公钥</param>
        /// <param name="algorithmName">算法名称</param>
        /// <returns>密文</returns>
        public static string RsaEncrypt(string decText, string publicKeyPem, string algorithmName)
        {
            if (!RsaAlgorithmNames.Contains(algorithmName))
                throw new ArgumentOutOfRangeException(algorithmName, $"不支持的算法[{algorithmName}]");

            AsymmetricKeyParameter keyParams;
            using (PemReader reader = new PemReader(new StringReader(publicKeyPem)))
            {
                keyParams = (AsymmetricKeyParameter)reader.ReadObject();
            }

            IBufferedCipher cipher = CipherUtilities.GetCipher(algorithmName);
            cipher.Init(true, keyParams);

            byte[] decData = Encoding.UTF8.GetBytes(decText);
            byte[] encData = cipher.DoFinal(decData);
            string encText = Convert.ToBase64String(encData);
            return encText;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="encText">密文</param>
        /// <param name="privateKeyPem">私钥</param>
        /// <param name="algorithmName">算法名称</param>
        /// <returns>明文</returns>
        public static string RsaDecrypt(string encText, string privateKeyPem, string algorithmName)
        {
            if (!RsaAlgorithmNames.Contains(algorithmName))
                throw new ArgumentOutOfRangeException(algorithmName, $"不支持的算法[{algorithmName}]");

            AsymmetricKeyParameter keyParams;
            using (PemReader reader = new PemReader(new StringReader(privateKeyPem)))
            {
                keyParams = ((AsymmetricCipherKeyPair)reader.ReadObject()).Private;
            }

            IBufferedCipher cipher = CipherUtilities.GetCipher(algorithmName);
            cipher.Init(false, keyParams);

            byte[] encData = Convert.FromBase64String(encText);
            byte[] decData = cipher.DoFinal(encData);
            string decText = Encoding.UTF8.GetString(decData);
            return decText;
        }

        #endregion
    }
}
