using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using You.Archi.DependencyInjection;
using You.Archi.Json.Abstractions;

namespace You.Archi.Json.NewtonsoftJson
{
    /// <summary>
    /// NewtonsoftJson序列化器
    /// </summary>
    public class ArcNewtonsoftJsonSerializer : IArcJsonSerializer, IArcTransientDependency
    {
        private readonly IOptions<ArcNewtonsoftJsonOptions> _options;

        public ArcNewtonsoftJsonSerializer(IOptions<ArcNewtonsoftJsonOptions> options)
        {
            _options = options;
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _options.Value.OriginSerializerSettings);
        }

        public object Deserialize(Type type, string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString, type, _options.Value.OriginSerializerSettings)!;
        }

        public T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, _options.Value.OriginSerializerSettings)!;
        }
    }
}
