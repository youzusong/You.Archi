namespace You.Archi.Crypto
{
    public struct RsaKeyPairPem
    {
        /// <summary>
        /// 私钥内容
        /// </summary>
        public string PrivateKeyPem { get; set; }

        /// <summary>
        /// 公钥内容
        /// </summary>
        public string PublicKeyPem { get; set; }
    }
}
