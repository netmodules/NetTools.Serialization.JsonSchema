using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    public class JsonSchemaType : JsonSchemaAttribute
    {
        public JsonSchemaType(BasicType type)
        {
            Type = type;
        }
    }
}
