using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on all types: This allows you to quickly set an additional string format of the property that will
    /// be used by the json-schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaAdditionalFormat : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on all types: This allows you to quickly set an additional string format of the property that will
        /// be used by the json-schema generator.
        /// </summary>
        public JsonSchemaAdditionalFormat(StringFormat format)
        {
            if (format == StringFormat.None)
            {
                return;
            }

            if (AdditionalFormats == null)
            {
                AdditionalFormats = new List<StringFormat>();
            }

            if (!AdditionalFormats.Contains(format))
            {
                AdditionalFormats.Add(format);
            }
        }


        /// <summary>
        /// Valid on all types: This allows you to quickly set multiple additional string formats of the property that will
        /// be used by the json-schema generator.
        /// </summary>
        public JsonSchemaAdditionalFormat(params StringFormat[] formats)
        {
            if (formats == null || formats.Length == 0)
            {
                return;
            }

            foreach (var format in formats)
            {
                if (format == StringFormat.None)
                {
                    continue;
                }

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
}
