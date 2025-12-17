//using NetTools.NetCore.Modules.Interfaces;
using NetTools.Serialization.Attributes;
using NetTools.Serialization.JsonSchemaAttributes;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;
using NetTools.Serialization.TestApplication.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetTools.Serialization.TestApplication
{
    /// <summary>
    /// This class acts as a dummy event so we can deserialize an incoming event request for the event name only. This is an optimisation as
    /// initial deserialization requires only the event name so we can perform the second deserialization of the complete event once we can
    /// identify the concrete object type
    /// </summary>
    [Serializable]
    [JsonSchemaTitle("Dummy Event")]
    [JsonSchemaDescription("This is a dummy event")]
    [JsonSchemaRequired("Name"), JsonSchemaAdditionalProperties(false)]
    [JsonSchemaMinMaxLength(2, 2)]
    [JsonSchemaCustom("customAttribute", true)]
    [JsonSchemaCustom("customAttribute2", 12345)]
    internal class DummyEvent //: IEvent
    {
        [JsonSchemaTitle("Event Name")]
        [JsonSchemaDescription("Enter the event name")]
        [JsonSchemaTypeOverride(typeof(string))]
        [JsonSchemaMinLength(10)]
        [JsonSchemaAdditionalFormat(StringFormat.NoAutoComplete)]
        public string Name => "Dummy.Event";


        [JsonName("meta"), /*JsonIgnore,*/ JsonSchemaRequired]
        public Dictionary<string, object> Meta { get; set; }

        public Dictionary<string, EventName> TestDictionaryOfType { get; set; }

        [JsonSchemaRequired, JsonSchemaMinItems(1)]
        [JsonSchemaFormat(StringFormat.FileBytes)]
        public List<string> TestArrayOfStrings { get; set; }

        public List<object> TestArrayOfPrimitiveObject { get; set; }


        [JsonSchemaItems(typeof(string)), JsonSchemaPattern("abc")]
        public List<object> TestArrayOfStringsWithJsonSchemaItems { get; set; }

        public List<TestArrayItem> TestArray { get; set; }

        [JsonIgnore]
        public bool Handled { get; private set; }

        [JsonSchemaReadOnly]
        public TestEnum TestEnum { get; set; }

        public TestClass TestClass { get; set; }

        public Uri TestUrl { get; set; }


        [JsonSchemaMinMaxValue(1, 10)]
        public int TestInt1 { get; set; }

        [JsonSchemaMinValue(1000)]
        public int TestInt2 { get; set; }

        [JsonSchemaMaxValue(1000)]
        [JsonSchemaMultipleOf(10)]
        public int TestInt3 { get; set; }


        [JsonSchemaMinMaxLength(2, 2)]
        [JsonSchemaCustom("customAttribute", "string")]
        [JsonSchemaCustom("customAttribute2", 1.2)]
        [JsonSchemaFormat(StringFormat.FileBase64)]
        public string TestString { get; set; }

        [JsonSchemaFormat(StringFormat.Base64)]
        public string TestBase64 { get; set; }

        [JsonSchemaFormat(StringFormat.Color)]
        public string TestColor { get; set; }

        [JsonSchemaFormat(StringFormat.Csv)]
        public string TestCsv { get; set; }

        [JsonSchemaFormat(StringFormat.DateTime)]
        public string TestDateTime { get; set; }

        [JsonSchemaFormat(StringFormat.Duration)]
        public string TestDuration { get; set; }

        [JsonSchemaFormat(StringFormat.Email)]
        public string TestEmail { get; set; }

        [JsonSchemaFormat(StringFormat.HostName)]
        public string TestHostname { get; set; }

        [JsonSchemaFormat(StringFormat.Html)]
        public string TestHtml { get; set; }

        [JsonSchemaFormat(StringFormat.IPv4)]
        public string TestIpAddressv4 { get; set; }

        [JsonSchemaFormat(StringFormat.IPv6)]
        public string TestIpAddressv6 { get; set; }

        [JsonSchemaFormat(StringFormat.Json)]
        public string TestJson { get; set; }

        [JsonSchemaFormat(StringFormat.Multiline)]
        public string TestMultiline { get; set; }

        [JsonSchemaFormat(StringFormat.None)]
        public string TestNone { get; set; }

        [JsonSchemaFormat(StringFormat.PngImageBase64)]
        public string TestPngBase64 { get; set; }

        [JsonSchemaFormat(StringFormat.RichText)]
        public string TestRichText { get; set; }

        [JsonSchemaFormat(StringFormat.Uri)]
        public string TestUri { get; set; }

        [JsonSchemaFormat(StringFormat.Xml)]
        public string TestXml { get; set; }
    }
}
