using reblGreen.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaClasses
{
    internal class DefaultJsonSchemaObjectParser : IJsonSchemaObjectParser
    {
        public Dictionary<string, object> ParseSchemaObject(JsonSchemaObject obj)
        {
            JsonSchemaAttribute attribute = null;

            if (obj.Attribute != null)
            {
                attribute = obj.Attribute;
            }
            else if (obj.PrimitiveType != null && obj.PrimitiveType.Constraints != null)
            {
                attribute = obj.PrimitiveType.Constraints;
            }

            if (attribute != null)
            {
                var schema = new Dictionary<string, object>();

                if (attribute.AdditionalItems)
                    schema.Add("additionalItems", true);

                // We need to check the object type to see if we need to parse another schema object??
                if (attribute.Default != null)
                {

                }
            }

            return null;
        }
    }
}
