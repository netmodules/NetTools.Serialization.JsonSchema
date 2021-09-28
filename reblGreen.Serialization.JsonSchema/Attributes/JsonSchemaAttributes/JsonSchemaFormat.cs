using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on all types: This allows you to quickly set the string format of the property which will
    /// be used by the json-schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaFormat : JsonSchemaAttribute
    {
        public JsonSchemaFormat(JsonSchemaAttribute.StringFormat format)
        {
            Format = format;
        }
    }
}
