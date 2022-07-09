using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity;

public class VectorImage : Node
{
    private List<VectorArtifact?>? _artifacts;

    [JsonProperty("rootUrl")]
    public string? RootUrl { get; set; }
    [JsonProperty("artifacts")]
    public List<string>? ArtifactIds { get; set; }

    [JsonIgnore]
    public List<VectorArtifact?>? Artifacts => _artifacts ??= ArtifactIds?.Select(GetNode<VectorArtifact>).ToList();
}