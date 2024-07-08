using Newtonsoft.Json;

namespace You.Archi.Json.NewtonsoftJson
{
    /// <summary>
    /// NewtonsoftJson配置项
    /// </summary>
    public class NewtonsoftJsonOptions
    {

        public NewtonsoftJsonOptions()
        {
            this.SerializerSettings = new JsonSerializerSettings();
        }

        /// <summary>
        /// 
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; }

    }
}
