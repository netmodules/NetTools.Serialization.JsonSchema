using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on all types: You can use this to tell JsonSchema to output your preferred <see cref="BasicType"/> to the dictionary when the JSON Schema dictionary is generated. By default type is automatically discovered when serializing.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaType : JsonSchemaAttribute
    {
        public JsonSchemaType(BasicType type)
        {
            Type = type;
        }
    }
}
