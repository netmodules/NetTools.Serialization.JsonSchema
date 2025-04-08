using System;
using System.Collections.Generic;
using System.Text;
using NetTools.Serialization.JsonSchemaClasses;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaInterfaces
{
    /// <summary>
    /// This interface is used to parse a <see cref="JsonSchemaObject"/> into a dictionary and allows you to define your own
    /// JsonSchemaObject parser and assign it using <see cref="JsonSchema.JsonSchema(JsonSchemaOptions, IJsonSchemaStringValidators)"/> if required.
    /// If no custom parser is provided, the <see cref="DefaultJsonSchemaObjectParser"/> parser will be used.
    /// </summary>
    public interface IJsonSchemaObjectParser
    {
        /// <summary>
        /// Parses a <see cref="JsonSchemaObject"/> into a dictionary. Implement this method to provide your own parser.
        /// </summary>
        Dictionary<string, object> GetSchemaDictionaryFromJsonSchemaObject(JsonSchemaObject obj);
    }
}
