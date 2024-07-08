namespace You.Archi.Json.Abstractions
{
    /// <summary>
    /// JSON序列化器接口
    /// </summary>
    public interface IArcJsonSerializer
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        string Serialize(object obj);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="jsonString">JSON字符串</param>
        /// <returns></returns>
        object Deserialize(Type type, string jsonString);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonString">JSON字符串</param>
        /// <returns></returns>
        T Deserialize<T>(string jsonString);
    }
}
