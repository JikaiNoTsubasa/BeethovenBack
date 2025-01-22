using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beethoven_api.Global;

public class UtcDateTimeJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var datetime = value as DateTime?;

        if(datetime is not null)
        {
            var iso = datetime.GetValueOrDefault().ToUniversalTime().ToString("u").Replace(" ", "T");
            var dateToken = JValue.CreateString(iso);
            dateToken.WriteTo(writer);
        }
        else
        {
            var nullToken = JValue.CreateNull();
            nullToken.WriteTo(writer);
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
    }

    public override bool CanRead
    {
        get { return false; }
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DateTime);
    }
}
