using Newtonsoft.Json;
using Salesflow.Task.Two.Entity;

namespace Salesflow.Task.Test
{
    [TestClass]
    public class MysticalMessageTest
    {
        [TestMethod]
        public void CanDeserialize()
        {
            var content = File.ReadAllText("Data\\test_data.json");
            var message = JsonConvert.DeserializeObject<MysticalMessage>(content);

            Assert.IsNotNull(message);
            Assert.AreEqual(message.RootEvents!.Count, 2);
            Assert.AreEqual(message.Nodes!.Count, 17);
            var rootEvent = message.RootEvents.Single(x => x.Id == "urn:li:fs_event:(6569262996214943744,S6573697598492819456_500)");
            Assert.IsNotNull(rootEvent);
            Assert.AreEqual(rootEvent.From!.MiniProfile!.Picture!.Artifacts!.Count, 4);
        }
    }
}
