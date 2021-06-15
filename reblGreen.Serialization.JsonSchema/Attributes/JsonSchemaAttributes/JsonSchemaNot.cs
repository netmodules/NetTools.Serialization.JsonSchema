using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: Must be of type JsonSchemaAttribute. The current schema must not be valid against the given schema. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaNot : JsonSchemaAttribute
    {
        public JsonSchemaNot(params object[] attributes)
        {

        }
    }
}
