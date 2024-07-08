using Newtonsoft.Json;

namespace You.Archi.Json.NewtonsoftJson.Converter
{
    /// <summary>
    /// 可空long类型Json转换器
    /// </summary>
    public class NullableLongJsonConverter : JsonConverter<long?>
    {
        public override long? ReadJson(JsonReader reader, Type objectType, long? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            long val;
            var str = reader.ReadAsString();
            if (Int64.TryParse(str, out val))
                return val;
            else
                return null;
        }

        public override void WriteJson(JsonWriter writer, long? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value.ToString());
            else
                writer.WriteNull();
        }
    }
}
