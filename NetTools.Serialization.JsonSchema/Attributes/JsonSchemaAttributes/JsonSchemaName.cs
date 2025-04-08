using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on object: This keyword can be used override the property name when generating a schema.
    /// Be aware that a property name should match a serialized object counterpart when using JSON
    /// schema validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class JsonSchemaName : Attribute
    {
        public readonly string Name;

        /// <summary>
        /// Valid on object: This keyword can be used override the property name when generating a schema.
        /// Be aware that a property name should match a serialized object counterpart when using JSON
        /// schema validation.
        /// </summary>
        public JsonSchemaName(string name)
        {
            Name = name;
        }
    }
}
