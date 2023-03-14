using NetTools.Serialization.JsonSchemaClasses;
using System;
using System.Collections.Generic;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaInterfaces
{
    public interface IJsonSchemaObjectParser
    {
        Dictionary<string, object> GetSchemaDictionaryFromJsonSchemaObject(JsonSchemaObject obj);
    }
}
