using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: Must be of type JsonSchemaAttribute. The current schema must be valid against any of the subschemas. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaAnyOf : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on any type: Must be of type JsonSchemaAttribute. The current schema must be valid against any of the subschemas. 
        /// </summary>
        public JsonSchemaAnyOf(params object[] attributes)
        {
            throw new NotImplementedException("JsonSchemaAnyOf is currently not supported.");
        }

        public JsonSchemaAnyOf(List<JsonSchemaAttribute> anyOf)
        {
            /// <summary>
            /// Valid on any type: Must be of type JsonSchemaAttribute. The current schema must be valid against any of the subschemas. 
            /// </summary>
            throw new NotImplementedException("JsonSchemaAnyOf is currently not supported.");
        }
    }
}
