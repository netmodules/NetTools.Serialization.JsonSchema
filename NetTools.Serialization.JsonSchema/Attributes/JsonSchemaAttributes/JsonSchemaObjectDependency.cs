using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on object: The dependencies keyword allows the schema of the object to change based on the presence of certain special properties.
    /// There are two forms of dependencies in JSON Schema: "Property" dependencies (represented as string[] value) declare that certain
    /// other properties must be present if a given property is present. "Schema" dependencies (represented as JsonSchemaAttribute value)
    /// declare that the schema changes when a given property is present.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaObjectDependency : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on object: The dependencies keyword allows the schema of the object to change based on the presence of certain special properties.
        /// There are two forms of dependencies in JSON Schema: "Property" dependencies (represented as string[] value) declare that certain
        /// other properties must be present if a given property is present. "Schema" dependencies (represented as JsonSchemaAttribute value)
        /// declare that the schema changes when a given property is present.
        /// </summary>
        public JsonSchemaObjectDependency(string key, params string[] keys)
        {
            throw new NotImplementedException("JsonSchemaObjectDependency is currently not supported.");
        }

        /// <summary>
        /// Valid on object: The dependencies keyword allows the schema of the object to change based on the presence of certain special properties.
        /// There are two forms of dependencies in JSON Schema: "Property" dependencies (represented as string[] value) declare that certain
        /// other properties must be present if a given property is present. "Schema" dependencies (represented as JsonSchemaAttribute value)
        /// declare that the schema changes when a given property is present.
        /// </summary>
        public JsonSchemaObjectDependency(string key, params object[] attributes)
        {
            throw new NotImplementedException("JsonSchemaObjectDependency is currently not supported.");
        }


        /// <summary>
        /// Valid on object: The dependencies keyword allows the schema of the object to change based on the presence of certain special properties.
        /// There are two forms of dependencies in JSON Schema: "Property" dependencies (represented as string[] value) declare that certain
        /// other properties must be present if a given property is present. "Schema" dependencies (represented as JsonSchemaAttribute value)
        /// declare that the schema changes when a given property is present.
        /// </summary>
        public JsonSchemaObjectDependency(string key, Type type)
        {
            throw new NotImplementedException("JsonSchemaObjectDependency is currently not supported.");
        }

        /// <summary>
        /// Valid on object: The dependencies keyword allows the schema of the object to change based on the presence of certain special properties.
        /// There are two forms of dependencies in JSON Schema: "Property" dependencies (represented as string[] value) declare that certain
        /// other properties must be present if a given property is present. "Schema" dependencies (represented as JsonSchemaAttribute value)
        /// declare that the schema changes when a given property is present.
        /// </summary>
        public JsonSchemaObjectDependency(string key)
        {
            throw new NotImplementedException("JsonSchemaObjectDependency is currently not supported.");
        }
    }
}
