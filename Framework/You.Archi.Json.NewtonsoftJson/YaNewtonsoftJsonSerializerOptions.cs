using Newtonsoft.Json;

namespace You.Archi.Json.NewtonsoftJson
{
    /// <summary>
    /// NewtonsoftJson序列化器配置项
    /// </summary>
    public class YaNewtonsoftJsonSerializerOptions
    {
        public YaNewtonsoftJsonSerializerOptions()
        {
            JsonSerializerSettings = new JsonSerializerSettings();
        }

        /// <summary>
        /// 序列化设定
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; }

    }
}
