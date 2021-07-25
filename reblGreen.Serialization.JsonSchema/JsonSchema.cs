using reblGreen.Serialization.JsonSchemaClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization
{
    public class JsonSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonSchema(string @schemaUrl)
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
        Dictionary<string, object> GetRecursiveSchema(Type t)
        {
            var schemaMembers = ReflectionHelpers.GetSchemaMembers(t);
            return null;
        }
    }
}
