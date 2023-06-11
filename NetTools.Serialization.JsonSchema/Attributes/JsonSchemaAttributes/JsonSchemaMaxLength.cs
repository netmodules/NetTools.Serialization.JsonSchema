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
    public class JsonSchemaMaxLength : JsonSchemaAttribute
    {
        public JsonSchemaMaxLength(uint max)
        {
            MaxLength = max;
        }

        public JsonSchemaMaxLength(ulong max)
        {
            MaxLength = max;
        }

        public JsonSchemaMaxLength(ushort max)
        {
            MaxLength = max;
        }

        public JsonSchemaMaxLength(byte max)
        {
            MaxLength = max;
        }
    }
}
