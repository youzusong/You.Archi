namespace You.Archi.Moment
{
    public class MomentOptions
    {
        public MomentOptions() { }

        /// <summary>
        /// 日期时间格式
        /// </summary>
        public string DateTimeFormat { get; set; } = "yyyy/MM/dd HH时mm分ss秒";

        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateFormat { get; set; } = "yyyy/MM/dd";

        /// <summary>
        /// 时间格式
        /// </summary>
        public string TimeFormat { get; set; } = "HH:mm";
    }
}
