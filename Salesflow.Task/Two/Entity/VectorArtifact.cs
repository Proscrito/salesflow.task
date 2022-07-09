using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity;

public class VectorArtifact : Node
{
    [JsonProperty("width")]
    public int Width { get; set; }
    [JsonProperty("fileIdentifyingUrlPathSegment")]
    public string? FileIdentifyingUrlPathSegment { get; set; }
    [JsonProperty("expiresAt")]
    public long ExpiresAt { get; set; }
    [JsonProperty("height")]
    public int Height { get; set; }
}