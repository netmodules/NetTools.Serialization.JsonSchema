using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on all types: This allows you to quickly set the string format of the property which will
    /// be used by the json-schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaAdditionalFormat : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on all types: This allows you to quickly set the string format of the property which will
        /// be used by the json-schema generator.
        /// </summary>
        public JsonSchemaAdditionalFormat(StringFormat format)
        {
            if (AdditionalFormats == null)
            {
                AdditionalFormats = new List<StringFormat>();
            }

            if (!AdditionalFormats.Contains(format))
            {
                AdditionalFormats.Add(format);
            }
        }
    }
}
