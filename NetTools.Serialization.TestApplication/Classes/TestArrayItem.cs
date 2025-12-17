using NetTools.Serialization.Attributes;
using NetTools.Serialization.JsonSchemaAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTools.Serialization.TestApplication.Classes
{
    internal class TestArrayItem
    {
        [JsonSchemaIgnore]
        [JsonName("tool_call_id")]
        [JsonSchemaDescription("Used to find a tool_call_result with matching tool_call_id in message history.")]
        public int Id { get; set; }

        [JsonSchemaRequired]
        [JsonSchemaDescription("Strictly only the correct name for the tool you wish to use and nothing else.")]
        public string Name { get; set; }

        [JsonSchemaDescription("Any tool parameters should only be included here and must match the tool's specification and requirements.")]
        public Dictionary<string, object> Parameters { get; set; }
    }
}
