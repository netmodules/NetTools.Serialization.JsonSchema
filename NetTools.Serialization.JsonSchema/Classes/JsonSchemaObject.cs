using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaClasses
{
    /// <summary>
    /// Represents a JSON schema object such as a type, property, field or class that hold JSON schema information assigned
    /// using decorations with the available attributes in the <see cref="NetTools.Serialization.JsonSchemaAttributes"/> namespace.
    /// </summary>
    public class JsonSchemaObject
    {
        /// <summary>
        /// The name of the object, type, property, or field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defines the 'Javascript' primitive type. I.e. A class would be considered to be a JSON object, while a <see cref="List{T}"/>
        /// would be considered to be a JSON array.
        /// </summary>
        public PrimitiveType PrimitiveType { get; set; }

        /// <summary>
        /// Holds the .NET type info relating to the object, property, or field.
        /// </summary>
        public TypeInfo TypeInfo { get; set; }

        /// <summary>
        /// This attribute holds all of the properties from each attribute from the <see cref="NetTools.Serialization.JsonSchemaAttributes"/> namespace that is applied
        /// to the object, property, or field. For example, if the <see cref="JsonSchemaAttributes.JsonSchemaMinMaxValue"/> attribute is applied to a property,
        /// this attribute will hold the min and max numerical values in the <see cref="JsonSchemaAttribute.Minimum"/> and <see cref="JsonSchemaAttribute.Maximum"/> properties.
        /// </summary>
        public JsonSchemaAttribute Attribute { get; set; }

        /// <summary>
        /// Any child members within the object, property, or field type will be defined here. 
        /// </summary>
        public List<JsonSchemaObject> Members { get; set; }
    }
}
