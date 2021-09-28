//using reblGreen.NetCore.Modules.Interfaces;
using reblGreen.Serialization.Attributes;
using reblGreen.Serialization.JsonSchemaAttributes;
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
    [JsonSchema(Title = "Dummy Event",
        Description = "This is a dummy event")]
    [JsonSchemaRequired("Name"), JsonSchemaAdditionalProperties(false)]
    internal class DummyEvent //: IEvent
    {
        [JsonName("name")]
        [JsonSchema(Title = "Event Name")]
        [JsonSchema(Description = "Enter the event name", MaxLength = 128, MinLength = 10)]
        [JsonSchema(TypeOverride = typeof(string))]
        public EventName Name { get; set; }

        [JsonName("meta"), JsonIgnore, JsonSchemaRequired]
        public Dictionary<string, object> Meta { get; set; }

        [JsonIgnore]
        public bool Handled { get; }

        public TestEnum TestEnum { get; set; }
    }
}
