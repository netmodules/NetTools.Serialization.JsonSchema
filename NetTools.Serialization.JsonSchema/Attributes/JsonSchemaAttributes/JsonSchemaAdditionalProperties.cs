using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on object: This keyword is used to control the handling of extra stuff, that is, properties whose names are not listed in the
    /// properties keyword. By default any additional properties are allowed. If the keyword is a boolean and set to false, no additional
    /// properties will be allowed.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaAdditionalProperties : JsonSchemaAttribute
    {
        public JsonSchemaAdditionalProperties(Type type)
        {
            AdditionalProperties = type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allowed"></param>
        public JsonSchemaAdditionalProperties(bool allowed)
        {
            AdditionalProperties = allowed;
        }
    }
}
