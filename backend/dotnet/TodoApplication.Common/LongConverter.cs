using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TodoApplication.Common;

public class LongConverter : JsonConverter<long>
{
    public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.Value == null)
            return default;

        return long.Parse(reader.Value.ToString());
    }
}