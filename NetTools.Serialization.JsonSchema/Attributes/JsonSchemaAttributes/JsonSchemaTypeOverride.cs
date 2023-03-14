using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    public class JsonSchemaTypeOverride : JsonSchemaAttribute
    {
        public JsonSchemaTypeOverride(Type type)
        {
            TypeOverride = type;
        }
    }
}
