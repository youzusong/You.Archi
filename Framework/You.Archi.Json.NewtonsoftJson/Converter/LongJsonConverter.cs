using Newtonsoft.Json;

namespace You.Archi.Json.NewtonsoftJson.Converter
{
    /// <summary>
    /// long类型Json转换器
    /// </summary>
    public class LongJsonConverter : JsonConverter<long>
    {
        public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            long val;
            var str = reader.ReadAsString();
            if (Int64.TryParse(str, out val))
                return val;
            else
                throw new JsonReaderException($"字符串[{str}]无法转换为long类型");
        }

        public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
