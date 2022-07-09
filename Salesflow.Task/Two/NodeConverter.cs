using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Salesflow.Task.Two.Entity;

namespace Salesflow.Task.Two;

public class NodeConverter : CustomCreationConverter<Node>
{
    public override Node Create(Type objectType)
    {
        return (Activator.CreateInstance(objectType) as Node)!;
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (typeof(Node) == objectType)
        {
            var jo = JObject.Load(reader);
            var type = Enum.Parse<NodeType>(jo["$type"]!.ToString());

            return type switch
            {
                NodeType.Event => jo.ToObject<Event>(),
                NodeType.MessageEvent => jo.ToObject<MessageEvent>(),
                NodeType.MessagingMember => jo.ToObject<MessagingMember>(),
                NodeType.MiniProfile => jo.ToObject<MiniProfile>(),
                NodeType.AttributedText => jo.ToObject<AttributedText>(),
                NodeType.VectorArtifact => jo.ToObject<VectorArtifact>(),
                NodeType.VectorImage => jo.ToObject<VectorImage>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return base.ReadJson(reader, objectType, existingValue, serializer);
    }
}