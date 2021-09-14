using reblGreen.Serialization.JsonSchemaClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization
{
    public class JsonSchema
    {
        Uri SchemaUrl;

        /// <summary>
        /// 
        /// </summary>
        public JsonSchema(Uri @schemaUrl)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> GenerateJsonSchemaFromObject<T>()
        {
            return GenerateJsonSchemaFromObject(typeof(T));
        }


        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> GenerateJsonSchemaFromObject(Type t)
        {
            if (t.IsPrimitive)
            {
                throw new NotSupportedException($"Type {t} is not supported!");
            }

            return GetRecursiveSchema(t);
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> GenerateJsonSchemaFromObject(JsonSchemaObject obj)
        {
            var schemaAttributes = new List<JsonSchemaAttribute>();

            if (obj.Attributes != null)
            {
                schemaAttributes.AddRange(obj.Attributes);
            }

            if (obj.PrimitiveType != null && obj.PrimitiveType.Constraints != null)
            {
                schemaAttributes.Add(obj.PrimitiveType.Constraints);
            }

            if (schemaAttributes.Count > 0)
            {
                // Generate schema from attributes...
                var mergedAttribute = JsonSchemaHelpers.MergeJsonSchemaAttributes(schemaAttributes.ToArray());

            }
            else
            {
                // Parse as standard object type???
            }

            //return GetRecursiveSchema(obj);
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> GetRecursiveSchema(Type t)
        {
            var schemaObject = GetJsonSchemaObject(t);

            return null;
        }

        public JsonSchemaObject GetJsonSchemaObject(Type t)
        {
            return JsonSchemaHelpers.GetSchemaObject(t);
        }
    }
}
