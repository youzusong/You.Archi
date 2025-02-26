namespace You.Archi.Localization
{
    /// <summary>
    /// 本地化配置
    /// </summary>
    public class LocalizationOptions
    {
        public LocalizationOptions()
        {
            this.Language = "zh-CN";
            this.CurrencyCode = "CNY";
            this.CurrencySymbol = "￥";
        }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 货币代码
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// 货币符号
        /// </summary>
        public string CurrencySymbol { get; set; }
    }
}
