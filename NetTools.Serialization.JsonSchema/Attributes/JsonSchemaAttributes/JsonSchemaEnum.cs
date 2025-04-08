using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: The enum keyword is used to restrict a value to a fixed set of values. It must be an array with at least one element, where each element is unique.
    /// You can use enum even without a type, to accept values of different types, however, in most cases, the elements in the enum array should also be valid
    /// against the enclosing schema. The following is an example for validating street light colors: "enum": ["red", "amber", "green"]
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaEnum : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on any type: The enum keyword is used to restrict a value to a fixed set of values. It must be an array with at least one element, where each element is unique.
        /// You can use enum even without a type, to accept values of different types, however, in most cases, the elements in the enum array should also be valid
        /// against the enclosing schema. The following is an example for validating street light colors: "enum": ["red", "amber", "green"]
        /// </summary>
        public JsonSchemaEnum(params object[] attributes)
        {
            throw new NotImplementedException("JsonSchemaEnum is currently not supported. Enum properties and values are generated automatically.");
        }

        
    }
}
