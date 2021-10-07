using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using reblGreen.Serialization.JsonSchemaAttributes.Internal;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: Must be of type JsonSchemaAttribute. The current schema must be valid against all of the subschemas. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaAllOf : JsonSchemaAttribute
    {
        public JsonSchemaAllOf(params object[] attributes)
        {
            throw new NotImplementedException("JsonSchemaAllOf is currently not supported.");
        }

        public JsonSchemaAllOf(List<JsonSchemaAttribute> allOf)
        {
            throw new NotImplementedException("JsonSchemaAllOf is currently not supported.");
        }
    }
}
