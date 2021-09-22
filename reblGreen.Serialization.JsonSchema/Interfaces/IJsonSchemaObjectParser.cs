using reblGreen.Serialization.JsonSchemaClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaInterfaces
{
    public interface IJsonSchemaObjectParser
    {
        Dictionary<string, object> ParseSchemaObject(JsonSchemaObject obj);
    }
}
