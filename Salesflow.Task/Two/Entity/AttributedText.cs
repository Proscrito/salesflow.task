using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity;

public class AttributedText : Node
{
    [JsonProperty("text")]
    public string? Text { get; set; }
    [JsonProperty("attributes")]
    public List<string>? Attributes { get; set; }
}