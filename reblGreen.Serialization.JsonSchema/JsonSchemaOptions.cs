using reblGreen.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization
{
    public class JsonSchemaOptions
    {
        public enum JsonSchemaType
        {
            Shallow,
            Nested,
        }


        /// <summary>
        /// Sets the base url that is used for adding an $id or a $ref link to the JSON schema object. If this is null,
        /// $id and $ref fields will not be added to the output JSON schema dictionary. This option is used by the
        /// default JsonSchemaObjectParser. If you are using a custom IJsonSchemaObjectParser, this property will be ignored
        /// and preferred naming of properties must be implemented in the custom parser.
        /// </summary>
        public Uri SchemaRefUrl;
        

        /// <summary>
        /// Suggests to JsonSchema that automatic camel-casing of property names is preferred. This option is used by the
        /// default JsonSchemaObjectParser. If you are using a custom IJsonSchemaObjectParser, this property will be ignored
        /// and preferred naming of properties must be implemented in the custom parser.
        /// </summary>
        public bool AutoCamelCase;


        /// <summary>
        /// Tells the default JsonSchemaObjectParser if you want a nested JSON schema output or a shallow Json schema does not include
        /// nested property child schemas.
        /// </summary>
        public JsonSchemaType SchemaType;

    }
}
