using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: Must be of type JsonSchemaAttribute. The current schema must be valid against any of the subschemas. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaAnyOf : JsonSchemaAttribute
    {
        public JsonSchemaAnyOf(params object[] attributes)
        {
            throw new NotImplementedException("JsonSchemaAnyOf is currently not supported.");
        }

        public JsonSchemaAnyOf(List<JsonSchemaAttribute> anyOf)
        {
            throw new NotImplementedException("JsonSchemaAnyOf is currently not supported.");
        }
    }
}
