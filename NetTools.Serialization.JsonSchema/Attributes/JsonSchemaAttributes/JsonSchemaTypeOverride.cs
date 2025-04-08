using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on all types: You can use this to tell JsonSchema to use a different .NET <see cref="Type"/> a reference when creating the JSON Schema for this specific target.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaTypeOverride : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on all types: You can use this to tell JsonSchema to use a different .NET <see cref="Type"/> a reference when creating the JSON Schema for this specific target.
        /// </summary>
        public JsonSchemaTypeOverride(Type type)
        {
            TypeOverride = type;
        }
    }
}
