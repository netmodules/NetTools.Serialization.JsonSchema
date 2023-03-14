using NetTools.Serialization.JsonSchemaClasses;
using NetTools.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization
{
    /// <summary>
    /// A basic implementation of JSON Schema generation from .Net objects and types. For JSON Schema documentation see
    /// <see href="https://json-schema.org/understanding-json-schema/UnderstandingJSONSchema.pdf"/>.
    /// </summary>
    public class JsonSchema
    {
        IJsonSchemaObjectParser Parser;

        /// <summary>
        /// A basic implementation of JSON Schema generation from .Net objects and types. For JSON Schema documentation see
        /// <see href="https://json-schema.org/understanding-json-schema/UnderstandingJSONSchema.pdf"/>.
        /// </summary>
        /// <param name="options">
        /// Allows you to configure the default JSONSchemaObject parser. The default parser is enabled when a custom parser
        /// is not used.
        /// </param>
        public JsonSchema(JsonSchemaOptions options)
        {
            Parser = new DefaultJsonSchemaObjectParser(options);
        }


        /// <summary>
        /// A basic implementation of JSON Schema generation from .Net objects and types. For JSON Schema documentation see
        /// <see href="https://json-schema.org/understanding-json-schema/UnderstandingJSONSchema.pdf"/>.
        /// </summary>
        /// <param name="parser">
        /// This option lets you add your own custom JsonSchemaObjectParser class. If this option is null, the default parser
        /// will be used.
        /// </param>
        public JsonSchema(IJsonSchemaObjectParser parser)
        {
            if (parser == null)
            {
                Parser = new DefaultJsonSchemaObjectParser();
            }
        }


        /// <summary>
        /// Returns a dictionary representation of a JSON Schema formatted object created from a .NET type, use your favorite
        /// JSON Serialization class library to convert this dictionary to a JSON Schema object string.
        /// </summary>
        public Dictionary<string, object> FromType<T>(byte propertyDepth = 1)
        {
            return FromType(typeof(T), propertyDepth);
        }


        /// <summary>
        /// Returns a dictionary representation of a JSON Schema formatted object created from a .NET type, use your favorite
        /// JSON Serialization class library to convert this dictionary to a JSON Schema object string.
        /// </summary>
        public Dictionary<string, object> FromType(Type t, byte propertyDepth = 1)
        {
            var schemaObject = GetJsonSchemaObject(t, propertyDepth);
            return FromJsonSchemaObject(schemaObject);
        }

        /// <summary>
        /// Returns a dictionary representation of a JSON Schema formatted object created from a <see cref="JsonSchemaObject"/>,
        /// use your favorite JSON Serialization class library to convert this dictionary to a JSON Schema object string.
        /// </summary>
        public Dictionary<string, object> FromJsonSchemaObject(JsonSchemaObject schemaObject)
        {
            return Parser.GetSchemaDictionaryFromJsonSchemaObject(schemaObject);
        }

        /// <summary>
        /// Returns a <see cref="JsonSchemaObject"/> that contains the required attributes and type information to generate a simple
        /// JSON Schema object. This method is used internally by <see cref="FromType(Type, byte)"/> and other methods, and is
        /// exposed as a public method as a helper for creating your own <see cref="IJsonSchemaObjectParser">custom parser</see>.
        /// </summary>
        public JsonSchemaObject GetJsonSchemaObject(Type t, byte propertyDepth)
        {
            return JsonSchemaHelpers.GetSchemaObject(t, propertyDepth, 0);
        }
    }
}
