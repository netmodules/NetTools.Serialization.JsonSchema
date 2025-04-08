using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on all types: This allows you to quickly set the string format of the property that will
    /// be used by the json-schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaFormat : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on all types: This allows you to quickly set the string format of the property that will
        /// be used by the json-schema generator.
        /// </summary>
        public JsonSchemaFormat(StringFormat format)
        {
            Format = format;
        }
    }
}
