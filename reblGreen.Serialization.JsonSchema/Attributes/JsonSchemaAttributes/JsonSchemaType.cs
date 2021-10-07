using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using reblGreen.Serialization.JsonSchemaAttributes.Internal;
using reblGreen.Serialization.JsonSchemaEnums;

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
