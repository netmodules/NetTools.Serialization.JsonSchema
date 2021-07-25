//using reblGreen.NetCore.Modules.Interfaces;
using reblGreen.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization.TestApplication
{
    /// <summary>
    /// This class acts as a dummy event so we can deserialize an incomming event request for the event name only. This is an optimisation as
    /// initial deserialization requires only the event name so we can perform the second deserialization of the complete event once we can
    /// identify the concrete object type
    /// </summary>
    [Serializable]
    internal class DummyEvent //: IEvent
    {
        [JsonName("name")]
        [JsonSchema(Description = "Enter a password length between 8 and 128", Maximum = 128, Minimum = 8)]
        public EventName Name { get; set; }

        [JsonName("meta"), JsonIgnore]
        public Dictionary<string, object> Meta { get; set; }

        [JsonIgnore]
        public bool Handled { get; }
    }
}
