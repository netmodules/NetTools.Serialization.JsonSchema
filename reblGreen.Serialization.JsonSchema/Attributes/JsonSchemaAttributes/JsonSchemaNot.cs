using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: Must be of type JsonSchemaAttribute. The current schema must not be valid against the given schema. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaNot : JsonSchemaAttribute
    {
        public JsonSchemaNot(JsonSchemaAttribute[] not)
        {
            throw new Exception("JsonSchemaNot is currently not supported.");
        }
    }
}
