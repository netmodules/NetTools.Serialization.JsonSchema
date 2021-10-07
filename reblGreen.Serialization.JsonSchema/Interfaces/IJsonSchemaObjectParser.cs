using reblGreen.Serialization.JsonSchemaClasses;
using System;
using System.Collections.Generic;
using System.Text; using reblGreen.Serialization.JsonSchemaAttributes.Internal;

namespace reblGreen.Serialization.JsonSchemaInterfaces
{
    public interface IJsonSchemaObjectParser
    {
        Dictionary<string, object> GetSchemaDictionaryFromJsonSchemaObject(JsonSchemaObject obj);
    }
}
