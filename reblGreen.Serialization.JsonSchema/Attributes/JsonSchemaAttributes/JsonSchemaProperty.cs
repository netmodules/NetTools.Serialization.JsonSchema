using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on object: The properties (key-value pairs) on an object are defined using the properties keyword. The value of properties
    /// is an object, where each key is the name of a property and each value is a JSON schema used to validate that property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class JsonSchemaPropertyAttribute : JsonSchemaAttribute
    {
        public JsonSchemaPropertyAttribute(string key, params object[] attributes)
        {

        }

        /// <summary>
        /// JsonSchemaPropertyAttribute can be added to a type of object multiple times to define properties
        /// which must exist in the JSON schema for this property or field.
        /// </summary>
        /// <param name="key">The name of the property</param>
        /// <param name="value">a reference type to help identify the JSON schema used to validate that property</param>
        public JsonSchemaPropertyAttribute(string key, Type value)
        {

        }
    }
}
