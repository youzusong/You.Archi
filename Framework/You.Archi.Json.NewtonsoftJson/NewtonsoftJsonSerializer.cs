using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using You.Archi.DependencyInjection;
using You.Archi.Json.Abstractions;

namespace You.Archi.Json.NewtonsoftJson
{
    /// <summary>
    /// NewtonsoftJson序列化器
    /// </summary>
    public class NewtonsoftJsonSerializer : IJsonSerializer, ITransientDependency
    {
        private readonly IOptions<NewtonsoftJsonOptions> _options;

        public NewtonsoftJsonSerializer(IOptions<NewtonsoftJsonOptions> options)
        {
            _options = options;
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _options.Value.SerializerSettings);
        }

        public object Deserialize(Type type, string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString, type, _options.Value.SerializerSettings)!;
        }

        public T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, _options.Value.SerializerSettings)!;
        }
    }
}
