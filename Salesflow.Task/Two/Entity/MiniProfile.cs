using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity;

public class MiniProfile : Node
{
    private VectorImage? _picture;

    [JsonProperty("firstName")]
    public string? FirstName { get; set; }
    [JsonProperty("lastName")]
    public string? LastName { get; set; }
    [JsonProperty("occupation")]
    public string? Occupation { get; set; }
    [JsonProperty("publicIdentifier")]
    public string? PublicIdentifier { get; set; }
    [JsonProperty("trackingId")]
    public string? TrackingId { get; set; }
    //TODO: seems like missing reference 
    [JsonProperty("objectUrn")]
    public string? ObjectUrn { get; set; }
    [JsonProperty("picture")]
    [JsonConverter(typeof(WrappedNodeConverter))]
    public string? PictureId { get; set; }

    [JsonIgnore]
    public VectorImage? Picture => _picture ??= GetNode<VectorImage>(PictureId);
}