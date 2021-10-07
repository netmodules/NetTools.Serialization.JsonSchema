using System;
using System.Collections.Generic;
using System.Text;
using reblGreen.Serialization.JsonSchemaAttributes.Internal;
using reblGreen.Serialization.JsonSchemaEnums;

namespace reblGreen.Serialization.JsonSchemaClasses
{
    public class PrimitiveType
    {
        public BasicType Name;
        public JsonSchemaAttribute Constraints;
        public Type[] Children;

        internal PrimitiveType(BasicType name, JsonSchemaAttribute constraints = null, Type[] childTypes = null)
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

        internal PrimitiveType(BasicType name, Type[] children)
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
