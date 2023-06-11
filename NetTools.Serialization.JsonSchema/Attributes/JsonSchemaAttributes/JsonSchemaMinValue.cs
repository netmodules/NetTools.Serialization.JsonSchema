using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on object: By default, the properties defined by the properties keyword are not required. However, one can provide a list of required
    /// properties using the required keyword. The required keyword takes an array of one or more strings and these strings should be unique. This
    /// field should be set at class level or could be ignored by the schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaMinValue : JsonSchemaAttribute
    {
        public JsonSchemaMinValue(int min)
        {
            Minimum = min;
        }

        public JsonSchemaMinValue(uint min)
        {
            Minimum = min;
        }

        public JsonSchemaMinValue(long min)
        {
            Minimum = min;
        }

        public JsonSchemaMinValue(ulong min)
        {
            Minimum = min;
        }

        public JsonSchemaMinValue(float min)
        {
            Minimum = min;
        }

        public JsonSchemaMinValue(double min)
        {
            Minimum = min;
        }

        public JsonSchemaMinValue(decimal min)
        {
            Minimum = min;
        }
    }
}
