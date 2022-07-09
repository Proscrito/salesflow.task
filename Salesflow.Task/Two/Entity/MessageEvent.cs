using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity;

public class MessageEvent : Node
{
    private AttributedText? _attributedBody;

    [JsonProperty("messageBodyRenderFormat")]
    public string? MessageBodyRenderFormat { get; set; }
    [JsonProperty("body")]
    public string? Body { get; set; }
    [JsonProperty("attributedBody")]
    [JsonConverter(typeof(WrappedNodeConverter))]
    public string? AttributedBodyId { get; set; }

    [JsonIgnore]
    public AttributedText? AttributedBody => _attributedBody ??= GetNode<AttributedText>(AttributedBodyId);
}