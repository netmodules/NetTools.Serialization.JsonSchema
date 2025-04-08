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
        IJsonSchemaStringValidators Validators;

        /// <summary>
        /// A basic implementation of JSON Schema generation from .Net objects and types. For JSON Schema documentation see
        /// <see href="https://json-schema.org/understanding-json-schema/UnderstandingJSONSchema.pdf"/>.
        /// </summary>
        /// <param name="options">
        /// Allows you to configure the default JSON Schema Object parser. The default parser is enabled when a custom parser
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
        /// <param name="options">
        /// Allows you to configure the default JSONSchemaObject parser. The default parser is enabled when a custom parser
        /// is not used.
        /// </param>
        /// <param name="stringValidators">
        /// Allows you to assign the methods used for string validation. A default validator with basic validation is assigned
        /// if a stringValidator is not provided. This is used 
        /// </param>
        public JsonSchema(JsonSchemaOptions options, IJsonSchemaStringValidators stringValidators)
            : this(options)
        {
            Validators = stringValidators == null ? new DefaultJsonSchemaStringValidators() : stringValidators;
        }


        /// <summary>
        /// A basic implementation of JSON Schema generation from .Net objects and types. For JSON Schema documentation see
        /// <see href="https://json-schema.org/understanding-json-schema/UnderstandingJSONSchema.pdf"/>.
        /// </summary>
        /// <param name="parser">
        /// This option lets you add your own custom JsonSchemaObjectParser class. If this option is null, the default parser
        /// will be used.
        /// </param>
        /// <param name="stringValidators">
        /// Allows you to assign the methods used for string validation. A default validator with basic validation is assigned
        /// if a stringValidator is not provided.
        /// </param>
        public JsonSchema(IJsonSchemaObjectParser parser, IJsonSchemaStringValidators stringValidators = null)
        {
            Parser = parser == null ? new DefaultJsonSchemaObjectParser() : parser;
            Validators = stringValidators == null ? new DefaultJsonSchemaStringValidators() : stringValidators;
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


        /// <summary>
        /// This method will validate a dictionary representation of an object against the JSON Schema generated from a given type
        /// and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema<T>(IDictionary<string, object> obj, out List<string> details, JsonSchemaValidationOptions options = null, byte propertyDepth = 1)
        {
            return ValidateSchema(obj, typeof(T), out details, options, propertyDepth);
        }


        /// <summary>
        /// This method will validate a dictionary representation of an object against the JSON Schema generated from a given type
        /// and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema(IDictionary<string, object> obj, Type type, out List<string> details, JsonSchemaValidationOptions options = null, byte propertyDepth = 1)
        {
            var schema = FromType(type, propertyDepth);
            return JsonSchemaValidation.ValidateField(obj, schema, Validators, out details, options != null ? options.IgnoreEnumCasing : false, options != null ? options.IgnoreEnumSpaces : false, options != null ? options.AllowNumbersAsStrings : false);
        }


        /// <summary>
        /// This method will validate a dictionary representation of an object against the provided dictionary representation of a
        /// "object" JSON Schema and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema(IDictionary<string, object> obj, Dictionary<string, object> schema, out List<string> details, JsonSchemaValidationOptions options = null)
        {
            return JsonSchemaValidation.ValidateField(obj, schema, Validators, out details, options != null ? options.IgnoreEnumCasing : false, options != null ? options.IgnoreEnumSpaces : false, options != null ? options.AllowNumbersAsStrings : false);
        }


        /// <summary>
        /// This method will validate a list representation of an array against the provided dictionary representation of a
        /// "array" JSON Schema and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema(IList<object> obj, Dictionary<string, object> schema, out List<string> details, JsonSchemaValidationOptions options = null)
        {
            return JsonSchemaValidation.ValidateField(obj, schema, Validators, out details, options != null ? options.IgnoreEnumCasing : false, options != null ? options.IgnoreEnumSpaces : false, options != null ? options.AllowNumbersAsStrings : false);
        }


        /// <summary>
        /// This method will validate an array representation of an array against the provided dictionary representation of a
        /// "array" JSON Schema and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema(object[] obj, Dictionary<string, object> schema, out List<string> details, JsonSchemaValidationOptions options = null)
        {
            return JsonSchemaValidation.ValidateField(obj, schema, Validators, out details, options != null ? options.IgnoreEnumCasing : false, options != null ? options.IgnoreEnumSpaces : false, options != null ? options.AllowNumbersAsStrings : false);
        }


        /// <summary>
        /// This method will validate a boolean representation of a boolean against the provided dictionary representation of a
        /// "boolean" JSON Schema and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema(bool obj, Dictionary<string, object> schema, out List<string> details, JsonSchemaValidationOptions options = null)
        {
            return JsonSchemaValidation.ValidateField(obj, schema, Validators, out details, options != null ? options.IgnoreEnumCasing : false, options != null ? options.IgnoreEnumSpaces : false, options != null ? options.AllowNumbersAsStrings : false);
        }


        /// <summary>
        /// This method will validate an integer representation of an integer against the provided dictionary representation of a
        /// "integer" JSON Schema and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema(int obj, Dictionary<string, object> schema, out List<string> details, JsonSchemaValidationOptions options = null)
        {
            return JsonSchemaValidation.ValidateField(obj, schema, Validators, out details, options != null ? options.IgnoreEnumCasing : false, options != null ? options.IgnoreEnumSpaces : false, options != null ? options.AllowNumbersAsStrings : false);
        }


        /// <summary>
        /// This method will validate a double representation of a double against the provided dictionary representation of a
        /// "number" JSON Schema and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema(double obj, Dictionary<string, object> schema, out List<string> details, JsonSchemaValidationOptions options = null)
        {
            return JsonSchemaValidation.ValidateField(obj, schema, Validators, out details, options != null ? options.IgnoreEnumCasing : false, options != null ? options.IgnoreEnumSpaces : false, options != null ? options.AllowNumbersAsStrings : false);
        }


        /// <summary>
        /// This method will validate a string representation of a string against the provided dictionary representation of a
        /// "string" JSON Schema and return details if the object is invalid. For objects that are nested, the provided schema must
        /// contain nested schemas to validate against. Linked schemas in the form of URIs ($ref/$schema) for recursion are not
        /// currently supported. Currently very basic validation is supported and some JSON schema fields will not invalidate where
        /// invalid.
        /// </summary>
        public bool ValidateSchema(string obj, Dictionary<string, object> schema, out List<string> details, JsonSchemaValidationOptions options = null)
        {
            return JsonSchemaValidation.ValidateField(obj, schema, Validators, out details, options != null ? options.IgnoreEnumCasing : false, options != null ? options.IgnoreEnumSpaces : false, options != null ? options.AllowNumbersAsStrings : false);
        }
    }
}
