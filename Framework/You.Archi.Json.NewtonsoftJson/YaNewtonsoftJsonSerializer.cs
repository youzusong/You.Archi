using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using You.Archi.DependencyInjection;
using You.Archi.Json.Abstractions;

namespace You.Archi.Json.NewtonsoftJson
{
    /// <summary>
    /// NewtonsoftJson序列化器
    /// </summary>
    public class YaNewtonsoftJsonSerializer : IYaJsonSerializer, IYaTransientDependency
    {
        private readonly IOptions<YaNewtonsoftJsonSerializerOptions> _options;

        public YaNewtonsoftJsonSerializer(IOptions<YaNewtonsoftJsonSerializerOptions> options)
        {
            _options = options;
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _options.Value.JsonSerializerSettings);
        }

        public object Deserialize(Type type, string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString, type, _options.Value.JsonSerializerSettings)!;
        }

        public T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, _options.Value.JsonSerializerSettings)!;
        }
    }
}
