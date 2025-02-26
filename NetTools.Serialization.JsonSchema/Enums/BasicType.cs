using System;
using System.Collections.Generic;
using System.Text;

namespace NetTools.Serialization.JsonSchemaEnums
{
    /// <summary>
    /// The type keyword is fundamental to JSON Schema. It specifies the data type for a schema.
    /// At its core, JSON Schema defines the following basic types:
    /// </summary>
    public enum BasicType
    {
        /// <summary>
        /// The unknown type is a custom type added by NetTools.Serialization.JsonSchema to allow a default if the attribute field is not set
        /// by the declarer.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The string type is used for strings of text. It may contain Unicode characters.
        /// </summary>
        String,

        /// <summary>
        /// There are two numeric types in JSON Schema: integer and number. They share the same validation keywords.
        /// The integer type is used for integral numbers.
        /// </summary>
        Integer,

        /// <summary>
        /// There are two numeric types in JSON Schema: integer and number. They share the same validation keywords.
        /// The number type is used for any numeric type, either integers or floating point numbers.
        /// </summary>
        Number,

        /// <summary>
        /// Objects are the mapping type in JSON. They map “keys” to “values”. In JSON, the “keys” must always be strings.
        /// Each of these pairs is conventionally referred to as a “property”.
        /// </summary>
        Object,

        /// <summary>
        /// Arrays are used for ordered elements. In JSON, each element in an array may be of a different type.
        /// </summary>
        Array,

        /// <summary>
        /// The boolean type matches only two special values: true and false.
        /// Note that values that evaluate to true or false, such as 1 and 0, are not accepted by the schema.
        /// </summary>
        Boolean,

        /// <summary>
        /// The null type is generally used to represent a missing value.
        /// When a schema specifies a type of null, it has only one acceptable value: null.
        /// </summary>
        Null,

        /// <summary>
        /// Any is a custom type added by NetTools.Serialization.JsonSchema that specifies in the schema
        /// that a property can have its value set to any object type
        /// </summary>
        Any
    }
}
