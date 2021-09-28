using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    public class JsonSchemaType : JsonSchemaAttribute
    {
        public JsonSchemaType(BasicType type)
        {
            Type = type;
        }
    }
}
