using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace reblGreen.Serialization.JsonSchema.Classes
{
    class ObjectMap
    {
        List<JsonSchemaAttribute> Attributes { get; set; }

        List<FieldInfo> Fields { get; set; }


    }
}
