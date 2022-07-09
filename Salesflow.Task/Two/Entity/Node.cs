using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity;

[JsonConverter(typeof(NodeConverter))]
public class Node
{
    private string? _id;

    [JsonProperty("$id", NullValueHandling = NullValueHandling.Ignore)]
    public string? Id
    {
        get => _id;
        set => _id = value;
    }

    [JsonProperty("entityUrn", NullValueHandling = NullValueHandling.Ignore)]
    public string? EntityUrn
    {
        get => _id;
        set => _id = value;
    }

    [JsonProperty("$type")]
    public NodeType NodeType { get; set; }

    [JsonProperty("$deletedFields")]
    public List<string>? DeletedFields { get; set; }

    [JsonIgnore]
    public MysticalMessage? ParentMysticalMessage { get; set; }

    protected T? GetNode<T>(string? urn) where T : class
    {
        return ParentMysticalMessage?.Nodes?.FirstOrDefault(x => x.Id!.Equals(urn, StringComparison.Ordinal)) as T;
    }
}