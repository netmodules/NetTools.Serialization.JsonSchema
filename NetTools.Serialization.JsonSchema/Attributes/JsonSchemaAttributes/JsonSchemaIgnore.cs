using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on field and property types: This allows you to tell the json-schema generator to exclude the current property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class JsonSchemaIgnore: JsonSchemaAttribute
    {
        public JsonSchemaIgnore(bool ignore = true)
        {
            Hidden = ignore;
        }
    }
}
