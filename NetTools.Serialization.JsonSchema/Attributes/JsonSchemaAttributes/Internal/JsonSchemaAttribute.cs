using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaAttributes.Internal
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaAttribute : Attribute
    {
        /// <summary>
        /// Use named parameters to configure jsonSchemaAttribute.
        /// </summary>
        internal JsonSchemaAttribute()
        {

        }

        ///// <summary>
        ///// Allows you to set positional parameters in the constructor before any named parameters. Positional parameters will be used to configure
        ///// JsonSchemaAttribute fields based on the parameter types and order. Named parameters will override or configure unidentifiable fields.
        ///// </summary>
        ///// <param name="attributes"></param>
        //private JsonSchemaAttribute(params object[] attributes)
        //{

        //}


        #region custom keywords
        /// <summary>
        /// Valid on any type: This is a custom schema field added by NetTools.Serialization.JsonSchema that advises the schema creator to use a specific naming convention for the value names that apply to a JSON Schema property.
        /// </summary>
        public NamingConvention ValueNamingConvention { get; set; }

        /// <summary>
        /// Valid on any type: This is a custom schema field added by NetTools.Serialization.JsonSchema that advises the schema creator that the field should be read only.
        /// </summary>
        public bool ReadOnly { get; internal set; } = false;

        /// <summary>
        /// Valid on any type: This is a custom schema field added by NetTools.Serialization.JsonSchema that advises the schema creator that the field should not be visible.
        /// </summary>
        public bool Hidden { get; internal set; } = false;

        /// <summary>
        /// Valid on any type: This is a custom schema field added by NetTools.Serialization.JsonSchema that helps a schema creator by pointing it to an object type that can be used to
        /// create a schema fragment ($ref).
        /// </summary>
        public Type TypeOverride { get; internal set; } = null;

        /// <summary>
        /// Valid on any type: This is a custom schema field added by NetTools.Serialization.JsonSchema that adds the ability to set custom attributes on any property. This may be
        /// useful for extending the JSON Schema model, but standard JSON Schema Draft parsers and processors may ignore these attributes.
        /// </summary>
        public Dictionary<string, object> CustomAttributes { get; internal set; } = null;
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
        public BasicType Type { get; internal set; } = BasicType.Unknown;

        /// <summary>
        /// Valid on any type: JSON Schema includes a few keywords, title, description and default, that aren’t strictly used for validation, but are used to describe parts of a schema.
        /// The title and description keywords must be strings. A “title” will preferably be short, whereas a “description” will provide a more lengthy
        /// explanation about the purpose of the data described by the schema. Neither are required, but they are encouraged for good practice.
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Valid on any type: JSON Schema includes a few keywords, title, description and default, that aren’t strictly used for validation, but are used to describe parts of a schema.
        /// The title and description keywords must be strings. A “title” will preferably be short, whereas a “description” will provide a more lengthy
        /// explanation about the purpose of the data described by the schema. Neither are required, but they are encouraged for good practice.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Valid on any type: JSON Schema includes a few keywords, title, description and default, that aren’t strictly used for validation, but are used to describe parts of a schema.
        /// The default keyword specifies a default value for an item. JSON processing tools may use this information to provide a default value for a missing key/value pair,
        /// though many JSON schema validators simply ignore the default keyword. It should validate against the schema in which it resides, but that isn’t required.
        /// </summary>
        public object Default { get; internal set; }

        ///// <summary>
        ///// Valid on any type: The enum keyword is used to restrict a value to a fixed set of values. It must be an array with at least one element, where each element is unique.
        ///// You can use enum even without a type, to accept values of different types, however, in most cases, the elements in the enum array should also be valid
        ///// against the enclosing schema. The following is an example for validating street light colors: "enum": ["red", "amber", "green"]
        ///// </summary>
        //public object[] Enum;

        /// <summary>
        /// Valid on any type: By default, the properties defined by the properties keyword are not required. However, one can provide a list of required properties using the required
        /// keyword. The required keyword takes an array of zero or more strings. Each of these strings must be unique. If adding this attribute flag on an field rather than at
        /// class-level, you should use the empty constructor. This will tell JsonSchema to add the field to the Required Properties array when generating the schema.
        /// </summary>
        public object Required { get; internal set; }
        #endregion



        #region string

        /// <summary>
        /// Valid on string: The length of a string can be constrained using the MinLength and MaxLength keywords. For both keywords, the value must be a non-negative number.
        /// The value must be numeric or will be ignored.
        /// </summary>
        public object MinLength { get; internal set; }

        /// <summary>
        /// Valid on string: The length of a string can be constrained using the MinLength and MaxLength keywords. For both keywords, the value must be a non-negative number.
        /// The value must be numeric or will be ignored.
        /// </summary>
        public object MaxLength { get; internal set; }

        /// <summary>
        /// Valid on string: The pattern keyword is used to restrict a string to a particular regular expression.
        /// The regular expression syntax is the one defined in JavaScript (ECMA 262 specifically).
        /// See <see href="https://spacetelescope.github.io/understanding-json-schema/reference/regular_expressions.html#regular-expressions"/> Regular Expressions for more information.
        /// </summary>
        public string Pattern { get; internal set; }

        /// <summary>
        /// Valid on string: The format keyword allows for basic semantic validation on certain kinds of string values that are commonly used.
        /// This allows values to be constrained beyond what the other tools in JSON Schema, including Regular Expressions can do.
        /// </summary>
        public StringFormat Format { get; internal set; } = StringFormat.None;

        /// <summary>
        /// Valid on string: This is a custom schema field added by NetTools.Serialization.JsonSchema that helps a schema creator by specifying additional string formats.
        /// The format keyword allows for basic semantic validation on certain kinds of string values that are commonly used. This allows values to be constrained beyond what the
        /// other tools in JSON Schema, including Regular Expressions can do.
        /// </summary>
        public List<StringFormat> AdditionalFormats { get; internal set; } = null;

        #endregion


        #region numeric types

        /// <summary>
        /// Valid on numeric types: Clever use of the multipleOf keyword (<see href="https://spacetelescope.github.io/understanding-json-schema/reference/numeric.html#multiples"/> Multiples) can be used
        /// to get around this discrepancy. The value must be numeric.
        /// </summary>
        public object MultipleOf { get; internal set; }

        /// <summary>
        /// Valid on numeric type: Specifies a minimum numeric value. The value must be numeric or will be ignored.
        /// </summary>
        public object Minimum { get; internal set; }

        /// <summary>
        /// Valid on numeric type: When true, it indicates that the range excludes the minimum value, i.e., x > min.
        /// When false (or not included), it indicates that the range includes the minimum value, i.e., x >= min.
        /// </summary>
        public bool ExclusiveMinimum { get; internal set; } = false;

        /// <summary>
        /// Valid on numeric type: Specifies a maximum numeric value. The value must be numeric or will be ignored.
        /// </summary>
        public object Maximum { get; internal set; }

        /// <summary>
        /// Valid on numeric type: When true, it indicates that the range excludes the maximum value, i.e., x is less than max.
        /// When false (or not included), it indicates that the range includes the maximum value, i.e., x is less than or equal to max.
        /// </summary>
        public bool ExclusiveMaximum { get; internal set; } = false;

        #endregion

        #region object
        /// <summary>
        /// Valid on object: The additionalProperties keyword is used to control the handling of extra stuff, that is, properties whose names are not listed
        /// in the properties keyword or match any of the regular expressions in the patternProperties keyword. By default any additional properties are
        /// allowed. The value of the additionalProperties keyword is a schema that will be used to validate any properties in the instance that are not
        /// matched by properties or patternProperties.Setting the additionalProperties schema to false means no additional properties will be allowed.
        /// </summary>
        public object AdditionalProperties { get; internal set; }

        /// <summary>
        /// Valid on object: The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of these
        /// must be a non-negative integer.
        /// </summary>
        public object MinProperties { get; internal set; }

        /// <summary>
        /// Valid on object: The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of these
        /// must be a non-negative integer.
        /// </summary>
        public object MaxProperties { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Type Property { get; internal set; }
        #endregion


        #region array

        /// <summary>
        /// Valid on array: The items keyword defines what is a valid item in the array.
        /// When items is a single schema, the additionalItems keyword is meaningless, and it should not be used.
        /// </summary>
        public object Items { get; internal set; }

        /// <summary>
        /// Valid on array: The additionalItems keyword controls whether it’s valid to have additional items in the array beyond what is defined in the
        /// schema. Here, we’ll reuse the example schema above, but set additionalItems to false, which has the effect of disallowing extra
        /// items in the array. When items is a single schema, the additionalItems keyword is meaningless, and it should not be used.
        /// </summary>
        public object AdditionalItems { get; internal set; }

        /// <summary>
        /// Valid on array: The length of the array can be specified using the minItems and maxItems keywords. The value of each keyword must be a 
        /// non-negative number. These keywords work whether doing List validation or Tuple validation.
        /// </summary>
        public object MinItems { get; internal set; }

        /// <summary>
        /// Valid on array: The length of the array can be specified using the minItems and maxItems keywords. The value of each keyword must be a 
        /// non-negative number. These keywords work whether doing List validation or Tuple validation.
        /// </summary>
        public object MaxItems { get; internal set; }

        /// <summary>
        /// Valid on array: A schema can ensure that each of the items in an array is unique. Simply set the uniqueItems keyword to true.
        /// </summary>
        public bool UniqueItems { get; internal set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public Type Item { get; internal set; }

        #endregion
    }
}
