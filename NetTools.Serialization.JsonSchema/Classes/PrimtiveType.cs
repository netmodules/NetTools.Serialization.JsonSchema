using System;
using System.Collections.Generic;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaClasses
{
    /// <summary>
    /// Holds information about a 'Javascript' primitive type used to generate a JSON schema property values.
    /// </summary>
    public class PrimitiveType
    {
        /// <summary>
        /// The name of the defined primitive type.
        /// </summary>
        public BasicType Name;

        /// <summary>
        /// Any constraints that are applied to the primitive type. For example, a <see cref="JsonSchemaMinMaxValue"/> attribute would be applied to a number type.
        /// </summary>
        public JsonSchemaAttribute Constraints;

        /// <summary>
        /// Any child types that are defined within the primitive type. For example, a <see cref="List{T}"/> would be considered to be a JSON array.
        /// </summary>
        public Type[] Children;

        internal PrimitiveType(BasicType name, JsonSchemaAttribute constraints = null, Type[] childTypes = null)
        {
            if (constraints == null)
            {
                constraints = new JsonSchemaAttribute() { Type = name };
            }
            else
            {
                constraints.Type = name;
            }

            Name = name;
            Constraints = constraints;
        }

        internal PrimitiveType(BasicType name, Type[] children)
        {
            Constraints = new JsonSchemaAttribute() { Type = name };
            Name = name;
            Children = children;
        }


        /// <summary>
        /// Returns the name of the primitive type in lower case. This is used to generate the JSON schema property name.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return Name.ToString().ToLowerInvariant();
        }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
