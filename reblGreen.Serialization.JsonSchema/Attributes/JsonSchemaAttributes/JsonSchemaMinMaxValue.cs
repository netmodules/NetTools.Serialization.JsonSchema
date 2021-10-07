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
    public class JsonSchemaMinMaxValue : JsonSchemaAttribute
    {
        public JsonSchemaMinMaxValue(int min, int max)
        {
            Minimum = min;
            Maximum = max;
        }

        public JsonSchemaMinMaxValue(uint min, uint max)
        {
            Minimum = min;
            Maximum = max;
        }

        public JsonSchemaMinMaxValue(long min, long max)
        {
            Minimum = min;
            Maximum = max;
        }

        public JsonSchemaMinMaxValue(ulong min, ulong max)
        {
            Minimum = min;
            Maximum = max;
        }

        public JsonSchemaMinMaxValue(float min, float max)
        {
            Minimum = min;
            Maximum = max;
        }

        public JsonSchemaMinMaxValue(double min, double max)
        {
            Minimum = min;
            Maximum = max;
        }

        public JsonSchemaMinMaxValue(decimal min, decimal max)
        {
            Minimum = min;
            Maximum = max;
        }
    }
}
