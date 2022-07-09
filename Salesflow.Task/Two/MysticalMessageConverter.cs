using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Salesflow.Task.Two.Entity;

namespace Salesflow.Task.Two;

public class MysticalMessageConverter : CustomCreationConverter<MysticalMessage>
{
    public override MysticalMessage Create(Type objectType)
    {
        return new MysticalMessage();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (base.ReadJson(reader, objectType, existingValue, serializer) is not MysticalMessage result)
        {
            throw new JsonSerializationException("Cannot serialize message");
        }

        if (result.Nodes is not null)
        {
            foreach (var node in result.Nodes)
            {
                node.ParentMysticalMessage = result;
            }
        }

        return result;
    }
}