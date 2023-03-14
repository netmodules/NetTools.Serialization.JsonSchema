using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on all types: This allows you to quickly set the description of the property which will
    /// be used by the json-schema generator.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaReadOnly : JsonSchemaAttribute
    {
        public JsonSchemaReadOnly(bool readOnly = true)
        {
            ReadOnly = readOnly;
        }
    }
}
