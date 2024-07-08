namespace You.Archi.Moment
{
    /// <summary>
    /// 时刻接口
    /// </summary>
    public interface IMoment
    {
        /// <summary>
        /// 本地时间
        /// </summary>
        DateTime Now { get; }
    }
}
