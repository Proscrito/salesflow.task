using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity;

public class MessagingMember : Node
{
    private MiniProfile? _miniProfile;

    [JsonProperty("nameInitials")]
    public string? NameInitials { get; set; }
    [JsonProperty("miniProfile")]
    [JsonConverter(typeof(WrappedNodeConverter))]
    public string? MiniProfileId { get; set; }

    [JsonIgnore]
    public MiniProfile? MiniProfile => _miniProfile ??= GetNode<MiniProfile>(MiniProfileId);
}