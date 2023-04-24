using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any fields and properties: This is a custom schema field added by NetTools.Serialization.JsonSchemaAttributes
    /// that advises the schema creator to use a specific naming convention for the value names. Currently this is only
    /// implemented on enum values in the default JSON Schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class JsonSchemaValueNamingConvention : JsonSchemaAttribute
    {
        public JsonSchemaValueNamingConvention(NamingConvention naming)
        {
            ValueNamingConvention = naming;
        }
    }
}
