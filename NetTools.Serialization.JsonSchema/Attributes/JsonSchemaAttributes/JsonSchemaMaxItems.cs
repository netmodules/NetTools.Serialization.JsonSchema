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
    public class JsonSchemaMaxItems : JsonSchemaAttribute
    {
        public JsonSchemaMaxItems(uint max)
        {
            MaxItems = max;
        }

        public JsonSchemaMaxItems(ulong max)
        {
            MaxItems = max;
        }

        public JsonSchemaMaxItems(ushort max)
        {
            MaxItems = max;
        }

        public JsonSchemaMaxItems(byte max)
        {
            MaxItems = max;
        }
    }
}
