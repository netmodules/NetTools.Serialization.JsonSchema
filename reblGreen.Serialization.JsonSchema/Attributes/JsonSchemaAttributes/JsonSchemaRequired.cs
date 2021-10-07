using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; using reblGreen.Serialization.JsonSchemaAttributes.Internal;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on object: By default, the properties defined by the properties keyword are not required. However, one can provide a list of required
    /// properties using the required keyword. The required keyword takes an array of one or more strings and these strings should be unique. This
    /// field should be set at class level or could be ignored by the schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaRequired : JsonSchemaAttribute
    {
        public JsonSchemaRequired()
        {
            Required = true;
        }

        public JsonSchemaRequired(params string[] attributes) 
        {
            Required = attributes;
        }

        public JsonSchemaRequired(List<string> attributes)
        {
            Required = attributes.ToArray();
        }

        public JsonSchemaRequired(string attribute)
        {
            if (Required == null || Required is bool)
            {
                Required = new string[] { attribute };
            }
            else if (Required is string[] arr)
            {
                Array.Resize(ref arr, arr.Length + 1);
                arr[arr.Length] = attribute;
            }
        }
    }
}
