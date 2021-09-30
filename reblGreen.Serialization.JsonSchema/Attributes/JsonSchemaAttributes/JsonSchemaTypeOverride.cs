using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
