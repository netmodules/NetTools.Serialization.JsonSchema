using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any: By default, a custom attribute added by reblGreen.Serialization.JsonSchemaAttributes to inform the json-schema generator that the property is
    /// not used or its functionality has not yet been implemented and the property should be excluded from the json schema.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaNotImplemented : Attribute
    {
        public JsonSchemaNotImplemented()
        {
            throw new Exception("JsonSchemaNotImplemented is currently not supported.");
        }
    }
}
