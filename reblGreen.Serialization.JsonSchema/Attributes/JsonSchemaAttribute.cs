using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaAttribute : Attribute
    {
        #region custom keywords
        /// <summary>
        /// Valid on any type: This is a custom schema field added by reblGreen.Serialization.JsonSchemaAttributes which advises the schema creator that the field should be read only.
        /// </summary>
        public bool ReadOnly = false;

        /// <summary>
        /// Valid on any type: This is a custom schema field added by reblGreen.Serialization.JsonSchemaAttributes which advises the schema creator that the field should not be visible.
        /// </summary>
        public bool Hidden = false;

        /// <summary>
        /// Valid on any type: This is a custom schema field added by reblGreen.Serialization.JsonSchemaAttributes which helps a schema creator by pointing it to an object type that can be used to
        /// create a schema fragment ($ref).
        /// </summary>
        public Type TypeOverride = null;
        #endregion


        #region generic keywords
        /*
        JSON Schema includes a few keywords, title, description and default, that aren’t strictly used for validation, but are used to describe parts of a schema.

        The title and description keywords must be strings. A “title” will preferably be short, whereas a “description” will provide a more lengthy explanation about
        the purpose of the data described by the schema. Neither are required, but they are encouraged for good practice.

        The default keyword specifies a default value for an item. JSON processing tools may use this information to provide a default value for a missing key/value
        pair, though many JSON schema validators simply ignore the default keyword. It should validate against the schema in which it resides, but that isn’t required.

        The enum keyword is used to restrict a value to a fixed set of values. It must be an array with at least one element, where each element is unique.
        You can use enum even without a type, to accept values of different types.

        */

        /// <summary>
        /// The type keyword is fundamental to JSON Schema. It specifies the data type for a schema. Some JsonSchema fields are only effective on types which
        /// they apply to. See <see href="https://spacetelescope.github.io/understanding-json-schema/reference/index.html"/> for more details on JSON Schema fields
        /// </summary>
        public BasicType Type;

        /// <summary>
        /// Valid on any type: JSON Schema includes a few keywords, title, description and default, that aren’t strictly used for validation, but are used to describe parts of a schema.
        /// The title and description keywords must be strings. A “title” will preferably be short, whereas a “description” will provide a more lengthy
        /// explanation about the purpose of the data described by the schema. Neither are required, but they are encouraged for good practice.
        /// </summary>
        public string Title;

        /// <summary>
        /// Valid on any type: JSON Schema includes a few keywords, title, description and default, that aren’t strictly used for validation, but are used to describe parts of a schema.
        /// The title and description keywords must be strings. A “title” will preferably be short, whereas a “description” will provide a more lengthy
        /// explanation about the purpose of the data described by the schema. Neither are required, but they are encouraged for good practice.
        /// </summary>
        public string Description;

        /// <summary>
        /// Valid on any type: JSON Schema includes a few keywords, title, description and default, that aren’t strictly used for validation, but are used to describe parts of a schema.
        /// The default keyword specifies a default value for an item. JSON processing tools may use this information to provide a default value for a missing key/value pair,
        /// though many JSON schema validators simply ignore the default keyword. It should validate against the schema in which it resides, but that isn’t required.
        /// </summary>
        public object Default;

        ///// <summary>
        ///// Valid on any type: The enum keyword is used to restrict a value to a fixed set of values. It must be an array with at least one element, where each element is unique.
        ///// You can use enum even without a type, to accept values of different types, however, in most cases, the elements in the enum array should also be valid
        ///// against the enclosing schema. The following is an example for validating street light colors: "enum": ["red", "amber", "green"]
        ///// </summary>
        //public object[] Enum;
        #endregion



        #region string

        /// <summary>
        /// Valid on string: The length of a string can be constrained using the MinLength and MaxLength keywords. For both keywords, the value must be a non-negative number.
        /// The value must be numeric or will be ignored.
        /// </summary>
        public object MinLength;

        /// <summary>
        /// Valid on string: The length of a string can be constrained using the MinLength and MaxLength keywords. For both keywords, the value must be a non-negative number.
        /// The value must be numeric or will be ignored.
        /// </summary>
        public object MaxLength;

        /// <summary>
        /// Valid on string: The pattern keyword is used to restrict a string to a particular regular expression.
        /// The regular expression syntax is the one defined in JavaScript (ECMA 262 specifically).
        /// See <see href="https://spacetelescope.github.io/understanding-json-schema/reference/regular_expressions.html#regular-expressions"/> Regular Expressions for more information.
        /// </summary>
        public string Pattern;

        /// <summary>
        /// Valid on string: The format keyword allows for basic semantic validation on certain kinds of string values that are commonly used.
        /// This allows values to be constrained beyond what the other tools in JSON Schema, including Regular Expressions can do.
        /// </summary>
        public StringFormat Format;

        #endregion


        #region numeric types

        /// <summary>
        /// Valid on numeric types: Clever use of the multipleOf keyword (<see href="https://spacetelescope.github.io/understanding-json-schema/reference/numeric.html#multiples"/> Multiples) can be used
        /// to get around this discrepancy. The value must be numeric.
        /// </summary>
        public object MultipleOf;

        /// <summary>
        /// Valid on numeric type: Specifies a minimum numeric value. The value must be numeric or will be ignored.
        /// </summary>
        public object Minimum;

        /// <summary>
        /// Valid on numeric type: When true, it indicates that the range excludes the minimum value, i.e., x > min.
        /// When false (or not included), it indicates that the range includes the minimum value, i.e., x >= min.
        /// </summary>
        public bool ExclusiveMinimum = false;

        /// <summary>
        /// Valid on numeric type: Specifies a maximum numeric value. The value must be numeric or will be ignored.
        /// </summary>
        public object Maximum;

        /// <summary>
        /// Valid on numeric type: When true, it indicates that the range excludes the maximum value, i.e., x is less than max.
        /// When false (or not included), it indicates that the range includes the maximum value, i.e., x is less than or equal to max.
        /// </summary>
        public bool ExclusiveMaximum = false;

        #endregion

        #region object

        /// <summary>
        /// Valid on object: The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of these
        /// must be a non-negative integer.
        /// </summary>
        public object MinProperties;

        /// <summary>
        /// Valid on object: The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of these
        /// must be a non-negative integer.
        /// </summary>
        public object MaxProperties;



        #endregion


        #region array

        /// <summary>
        /// Valid on array: The additionalItems keyword controls whether it’s valid to have additional items in the array beyond what is defined in the
        /// schema. Here, we’ll reuse the example schema above, but set additionalItems to false, which has the effect of disallowing extra
        /// items in the array. When items is a single schema, the additionalItems keyword is meaningless, and it should not be used.
        /// </summary>
        public bool AdditionalItems = false;

        /// <summary>
        /// Valid on array: The length of the array can be specified using the minItems and maxItems keywords. The value of each keyword must be a 
        /// non-negative number. These keywords work whether doing List validation or Tuple validation.
        /// </summary>
        public object MinItems;

        /// <summary>
        /// Valid on array: The length of the array can be specified using the minItems and maxItems keywords. The value of each keyword must be a 
        /// non-negative number. These keywords work whether doing List validation or Tuple validation.
        /// </summary>
        public object MaxItems;

        /// <summary>
        /// Valid on array: A schema can ensure that each of the items in an array is unique. Simply set the uniqueItems keyword to true.
        /// </summary>
        public bool UniqueItems = false;
        #endregion


        /// <summary>
        /// Use named parameters to configure jsonSchemaAttribute.
        /// </summary>
        public JsonSchemaAttribute()
        {

        }

        /// <summary>
        /// Allows you to set positional parameters in the constructor before any named parameters. Positional parameters will be used to configure
        /// JsonSchemaAttribute fields based on the parameter types and order. Named parameters will override or configure unidentifiable fields.
        /// </summary>
        /// <param name="attributes"></param>
        public JsonSchemaAttribute(params object[] attributes)
        {

        }


        /// <summary>
        /// The type keyword is fundamental to JSON Schema. It specifies the data type for a schema.
        /// At its core, JSON Schema defines the following basic types:
        /// </summary>
        public enum BasicType
        {
            /// <summary>
            /// The unknown type is a custom type added by reblGreen.Serialization.JsonSchemaAttributes.Events to allow a default if the attribute field is not set
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
            /// Any is a custom type added by reblGreen.Serialization.JsonSchemaAttributes.Events that specifies in the schema
            /// that a property can have its value set to any object type
            /// </summary>
            Any
        }

        /// <summary>
        /// The format keyword allows for basic semantic validation on certain kinds of string values that are commonly used.
        /// This allows values to be constrained beyond what the other tools in JSON Schema, including Regular Expressions can do.
        /// </summary>
        public enum StringFormat
        {
            /// <summary>
            /// The none format is a custom format added by reblGreen.Serialization.JsonSchemaAttributes to allow a default if the attribute field is not set
            /// by the declarer.
            /// </summary>
            None = 0,

            /// <summary>
            /// Custom format added by reblGreen.Serialization.JsonSchemaAttributes: // ISO 8601 Duration should be formatted as P[n]Y[n]M[n]DT[n]H[n]M[n]S.
            /// See <see href="https://en.wikipedia.org/wiki/ISO_8601">Durations</see>
            /// </summary>
            Duration,

            /// <summary>
            /// Custom format added by reblGreen.Serialization.JsonSchemaAttributes: This tells the formatter that this string should span across multiple lines
            /// and should be displayed in a textarea rather than a text field.
            /// </summary>
            Multiline,


            /// <summary>
            /// Built-in: Date representation, as defined by <see href="http://tools.ietf.org/html/rfc3339"/> RFC 3339, section 5.6.
            /// </summary>
            DateTime,

            /// <summary>
            /// Built-in: Internet email address, <see href="http://tools.ietf.org/html/rfc5322"/> RFC 5322, section 3.4.1.
            /// </summary>
            Email,

            /// <summary>
            /// Built-in: Internet host name, <see href="http://tools.ietf.org/html/rfc1034"/> RFC 1034, section 3.1.
            /// </summary>
            HostName,

            /// <summary>
            /// Built-in: IPv4 address, according to dotted-quad ABNF syntax as defined in <see href="http://tools.ietf.org/html/rfc2673"/> RFC 2673, section 3.2.
            /// </summary>
            IPv4,

            /// <summary>
            /// Built-in: IPv6 address, as defined in <see href="http://tools.ietf.org/html/rfc2373"/> RFC 2373, section 2.2.
            /// </summary>
            IPv6,

            /// <summary>
            /// Built-in: A universal resource identifier (URI), according to <see href="http://tools.ietf.org/html/rfc3986"/> RFC3986.
            /// </summary>
            Uri
        }
    }
}
