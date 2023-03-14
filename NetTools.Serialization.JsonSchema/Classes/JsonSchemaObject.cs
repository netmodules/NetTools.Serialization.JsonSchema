using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaClasses
{
    public class JsonSchemaObject
    {
        public string Name { get; set; }

        public PrimitiveType PrimitiveType { get; set; }

        public TypeInfo TypeInfo { get; set; }

        public JsonSchemaAttribute Attribute { get; set; }

        public List<JsonSchemaObject> Members { get; set; }
    }
}
