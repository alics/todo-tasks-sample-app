﻿using Newtonsoft.Json;

namespace TodoApplication.Common;

public class NullableLongConverter : JsonConverter<long?>
{
    public override void WriteJson(JsonWriter writer, long? value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override long? ReadJson(JsonReader reader, Type objectType, long? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.Value == null ||
            string.IsNullOrEmpty(reader.Value?.ToString()))
            return null;

        return long.Parse(reader.Value.ToString());
    }
}