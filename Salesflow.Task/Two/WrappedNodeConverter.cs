using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Salesflow.Task.Two;

public class WrappedNodeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => throw new NotImplementedException();

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        try
        {
            string? urn;

            if (reader.TokenType == JsonToken.String)
            {
                urn = reader.Value?.ToString();
            }
            else
            {
                var jObject = JObject.Load(reader);
                var first = jObject.First;

                while (first is not null && !first.ToString().StartsWith("urn:li:"))
                {
                    first = first.First;
                }

                urn = first?.ToString();
            }

            return urn;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public override bool CanConvert(Type objectType)
    {
        return true;
    }
}