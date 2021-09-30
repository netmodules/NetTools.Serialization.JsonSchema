using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on object: By default, the properties defined by the properties keyword are not required. However, one can provide a list of required
    /// properties using the required keyword. The required keyword takes an array of one or more strings and these strings should be unique. This
    /// field should be set at class level or could be ignored by the schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaMinMaxLength : JsonSchemaAttribute
    {
        public JsonSchemaMinMaxLength(uint min, uint max)
        {
            MinLength = min;
            MaxLength = max;
        }

        public JsonSchemaMinMaxLength(ulong min, ulong max)
        {
            MinLength = min;
            MaxLength = max;
        }

        public JsonSchemaMinMaxLength(ushort min, ushort max)
        {
            MinLength = min;
            MaxLength = max;
        }

        public JsonSchemaMinMaxLength(byte min, byte max)
        {
            MinLength = min;
            MaxLength = max;
        }
    }
}
