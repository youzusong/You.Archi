using You.Archi.Crypto;
using You.Archi.Crypto.Utilities;

namespace CryptoTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMD5()
        {
            string orgText = "test";
            string encText = CryptoUtility.MD5Encrypt(orgText);
            Console.WriteLine(encText);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string orgText = "123456";

            string rsaAlgorithmName1 = "RSA/ECB/PKCS1Padding";
            string rsaAlgorithmName2 = "RSA/ECB/OAEPWithSHA-256AndMGF1Padding";

            string privateKeyFile = @"E:\_temp\SSL\private_key.pem";
            string publicKeyFile = @"E:\_temp\SSL\public_key.pem";

            //var keyPairText = CryptoUtility.GenRsaKeyPairPem(2048);
            //Console.WriteLine("公钥：");
            //Console.WriteLine(keyPairText.PublicKeyPem);
            //Console.WriteLine("私钥：");
            //Console.WriteLine(keyPairText.PrivateKeyPem);
            //Console.WriteLine("========================================");

            var keyPairText = new RsaKeyPairPem();
            keyPairText.PrivateKeyPem = File.ReadAllText(privateKeyFile);
            keyPairText.PublicKeyPem = File.ReadAllText(publicKeyFile);


            string encText = CryptoUtility.RsaEncrypt(orgText, keyPairText.PublicKeyPem);
            string orgTextX = CryptoUtility.RsaDecrypt(encText, keyPairText.PrivateKeyPem);
            Console.Write("密文：");
            Console.WriteLine(encText);
            Console.Write("明文：");
            Console.WriteLine(orgTextX);
            Console.WriteLine("========================================");

            string encText2 = CryptoUtility.RsaEncrypt(orgText, keyPairText.PublicKeyPem, rsaAlgorithmName1);
            string orgTextX2 = CryptoUtility.RsaDecrypt(encText2, keyPairText.PrivateKeyPem, rsaAlgorithmName1);
            Console.WriteLine("算法：" + rsaAlgorithmName1);
            Console.Write("密文：");
            Console.WriteLine(encText2);
            Console.Write("明文：");
            Console.WriteLine(orgTextX2);
            Console.WriteLine("========================================");

            string encText3 = CryptoUtility.RsaEncrypt(orgText, keyPairText.PublicKeyPem, rsaAlgorithmName2);
            string orgTextX3 = CryptoUtility.RsaDecrypt(encText3, keyPairText.PrivateKeyPem, rsaAlgorithmName2);
            Console.WriteLine("算法：" + rsaAlgorithmName2);
            Console.Write("密文：");
            Console.WriteLine(encText3);
            Console.Write("明文：");
            Console.WriteLine(orgTextX3);
            Console.WriteLine("========================================");
        }


    }
}