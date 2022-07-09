using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity;

public class Event : Node
{
    private MessageEvent? _eventContent;
    private MessagingMember? _from;
    private Event? _previousEventInConversation;

    [JsonProperty("createdAt")]
    public long CreatedAt { get; set; }
    [JsonProperty("subType")]
    public string? SubType { get; set; }
    [JsonProperty("eventContent")]
    [JsonConverter(typeof(WrappedNodeConverter))]
    public string? EventContentId { get; set; }
    [JsonProperty("from")]
    [JsonConverter(typeof(WrappedNodeConverter))]
    public string? FromId { get; set; }
    [JsonProperty("previousEventInConversation")]
    [JsonConverter(typeof(WrappedNodeConverter))]
    public string? PreviousEventInConversationId { get; set; }

    [JsonIgnore]
    public Event? PreviousEventInConversation => _previousEventInConversation ??= GetNode<Event>(PreviousEventInConversationId);
    [JsonIgnore]
    public MessageEvent? EventContent => _eventContent ??= GetNode<MessageEvent>(EventContentId);
    [JsonIgnore]
    public MessagingMember? From => _from ??= GetNode<MessagingMember>(FromId);
}