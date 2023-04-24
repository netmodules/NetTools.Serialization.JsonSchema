using System;
using System.Collections.Generic;
using System.Text;

namespace NetTools.Serialization.JsonSchemaEnums
{
    /// <summary>
    /// Suggests to the JSON Schema generator what type of naming convention is prefered for the property name and/or its values.
    /// </summary>
    public enum NamingConvention
    {
        /// <summary>
        /// Allow the JSON Schema generator to decide what naming convention to use.
        /// </summary>
        Automatic,

        /// <summary>
        /// Use a name that matches the character casing of the .NET property.
        /// </summary>
        None,

        /// <summary>
        /// Convert the JSON Schema name to a lowercase value.
        /// </summary>
        Lowercase,

        /// <summary>
        /// Convert the JSON Schema name to a n uppercase value.
        /// </summary>
        Uppercase,
    }
}
