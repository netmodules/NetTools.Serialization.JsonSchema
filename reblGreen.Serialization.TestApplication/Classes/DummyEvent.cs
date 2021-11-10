//using reblGreen.NetCore.Modules.Interfaces;
using reblGreen.Serialization.Attributes;
using reblGreen.Serialization.JsonSchemaAttributes;
using System;
using System.Collections.Generic;
using System.Text; using reblGreen.Serialization.JsonSchemaAttributes.Internal;

namespace reblGreen.Serialization.TestApplication
{
    /// <summary>
    /// This class acts as a dummy event so we can deserialize an incomming event request for the event name only. This is an optimisation as
    /// initial deserialization requires only the event name so we can perform the second deserialization of the complete event once we can
    /// identify the concrete object type
    /// </summary>
    [Serializable]
    [JsonSchemaTitle("Dummy Event")]
    [JsonSchemaDescription("This is a dummy event")]
    [JsonSchemaRequired("Name"), JsonSchemaAdditionalProperties(false)]
    internal class DummyEvent //: IEvent
    {
        [JsonSchemaTitle("Event Name")]
        [JsonSchemaDescription("Enter the event name")]
        [JsonSchemaTypeOverride(typeof(string))]
        public EventName Name { get; set; }

        [JsonName("meta"), JsonIgnore, JsonSchemaRequired]
        public Dictionary<string, object> Meta { get; set; }

        public Dictionary<string, EventName> TestDictionaryOfType { get; set; }

        [JsonIgnore]
        public bool Handled { get; }

        public TestEnum TestEnum { get; set; }

        public TestClass TestClass { get; set; }

        public Uri TestUrl { get; set; }
    }
}
