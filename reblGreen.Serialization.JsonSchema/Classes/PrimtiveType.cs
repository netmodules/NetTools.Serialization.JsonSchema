using System;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaClasses
{
    public class PrimitiveType
    {
        internal JsonSchemaAttribute.BasicType Name;
        internal JsonSchemaAttribute Constraints;

        internal PrimitiveType(JsonSchemaAttribute.BasicType name, JsonSchemaAttribute constraints = null)
        {
            Name = name;
            Constraints = constraints;
        }
    }
}
