using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: Must be of type JsonSchemaAttribute.The current schema must be valid against exactly one of the subschemas. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaOneOf : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on any type: Must be of type JsonSchemaAttribute.The current schema must be valid against exactly one of the subschemas. 
        /// </summary>
        public JsonSchemaOneOf(Type type)
        {
            throw new NotImplementedException("JsonSchemaOneOf is currently not supported.");
        }
    }
}
