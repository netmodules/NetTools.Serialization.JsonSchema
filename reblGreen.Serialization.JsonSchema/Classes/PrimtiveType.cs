using System;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaClasses
{
    public class PrimitiveType
    {
        public JsonSchemaAttribute.BasicType Name;
        public JsonSchemaAttribute Constraints;
        public Type[] Children;

        internal PrimitiveType(JsonSchemaAttribute.BasicType name, JsonSchemaAttribute constraints = null, Type[] childTypes = null)
        {
            if (constraints == null)
            {
                constraints = new JsonSchemaAttribute() { Type = name };
            }
            else
            {
                constraints.Type = name;
            }

            Name = name;
            Constraints = constraints;
        }

        internal PrimitiveType(JsonSchemaAttribute.BasicType name, Type[] children)
        {
            Constraints = new JsonSchemaAttribute() { Type = name };
            Name = name;
            Children = children;
        }

        public string GetName()
        {
            return Name.ToString().ToLowerInvariant();
        }
    }
}
