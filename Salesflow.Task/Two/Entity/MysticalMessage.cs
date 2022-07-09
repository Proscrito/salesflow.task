using Newtonsoft.Json;

namespace Salesflow.Task.Two.Entity
{
    [JsonConverter(typeof(MysticalMessageConverter))]
    public class MysticalMessage
    {
        [JsonProperty("data")]
        public Header? Header { get; set; }
        [JsonProperty("included")]
        public List<Node>? Nodes { get; set; }

        [JsonIgnore]
        public List<Event>? RootEvents => Nodes?.OfType<Event>().ToList();
    }
}
