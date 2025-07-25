﻿using NetTools.Serialization.JsonSchemaAttributes;
using NetTools.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections.Generic;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization
{
    /// <summary>
    /// Allows you to configure the default JSON Schema Object parser.The default parser is enabled when a custom parser
    /// is not used.
    /// </summary>
    public class JsonSchemaOptions
    {
        /// <summary>
        /// Tells the default JsonSchemaObjectParser if you want a nested JSON schema output or a shallow Json schema does not include
        /// nested property child schemas.
        /// </summary>
        public enum JsonSchemaType
        {
            /// <summary>
            /// A shallow JSON schema will only include the top-level properties and their types. This will also include the JSON schema
            /// $ref endpoints for each property if <see cref="SchemaRefUrl"/> has been set to a valid base Uri.
            /// </summary>
            Shallow,

            /// <summary>
            /// A nested JSON schema will include both a top-level and sub-level JSON schema for object properties that are nested
            /// to the chosen property depth. This will also include the JSON schema $ref endpoints for each property if
            /// <see cref="SchemaRefUrl"/> has been set to a valid base Uri.
            /// </summary>
            Nested,
        }


        /// <summary>
        /// Sets the base url that is used for adding an $id or a $ref link to the JSON schema object. If this is null,
        /// $id and $ref fields will not be added to the output JSON schema dictionary. This option is used by the
        /// default JsonSchemaObjectParser. If you are using a custom IJsonSchemaObjectParser, this property will be ignored
        /// and preferred naming of properties must be implemented in the custom parser. PLEASE NOTE: It is the application
        /// or person who is using NetTools.Serialization.JsonSchema who is responsible for ensuring that any $ref or $id
        /// urls return the correct JSON Schema object reference.
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


        /// <summary>
        /// If true and the legacy schema type is "type": "any", this tells the default JsonSchemaObjectParser to set type to
        /// "type":["number","string","boolean","object","array", "null"] - Multiple types is not supported in older schema drafts.
        /// </summary>
        public bool UseAnyTypeArray;

        /// <summary>
        /// This enables you to override the schema type that is generated by the default JsonSchemaObjectParser. This option is
        /// provided so that you can change the output schema on fields, properties and types in which you don't have access to
        /// modify the code to support the NetTools.Serialization.JsonSchema class library.
        /// </summary>
        public Dictionary<Type, JsonSchemaAttribute> TypeOverrides;
    }
}
