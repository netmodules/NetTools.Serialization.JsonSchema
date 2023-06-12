using NetTools.Serialization.JsonSchemaAttributes;
using NetTools.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections.Generic;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization
{
    public class JsonSchemaValidationOptions
    {
        /// <summary>
        /// Tells JSON Schema validation to validate enums as true if the enum is a matching string while ignoring
        /// the character-casing (ignore case).
        /// </summary>
        public bool IgnoreEnumCasing { get; set; }


        /// <summary>
        /// Tells JSON Schema validation to validate enums as true if the enum is a matching string while ignoring
        /// any spaces (" ") or decimal point (.) punctuation. This adds support for the NetTools.Serialization.Json 
        /// extended enum parsing.
        /// </summary>
        public bool IgnoreEnumSpaces { get; set; }


        /// <summary>
        /// Tells JSON Schema validation to validate integers/numbers as true if the value is a numeric string. This
        /// adds support for the NetTools.Serialization.Json extended integer/number parsing.
        /// </summary>
        public bool AllowNumbersAsStrings { get; set; }
    }
}
