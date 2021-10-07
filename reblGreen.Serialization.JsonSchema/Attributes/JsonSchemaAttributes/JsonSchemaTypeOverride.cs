using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; using reblGreen.Serialization.JsonSchemaAttributes.Internal;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    public class JsonSchemaTypeOverride : JsonSchemaAttribute
    {
        public JsonSchemaTypeOverride(Type type)
        {
            TypeOverride = type;
        }
    }
}
