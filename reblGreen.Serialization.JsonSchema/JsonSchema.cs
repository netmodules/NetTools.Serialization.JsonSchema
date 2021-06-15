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
        public static Dictionary<string, object> GenerateJsonSchemaFromObject<T>()
        {
            return GenerateJsonSchemaFromObject(typeof(T));
        }


        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, object> GenerateJsonSchemaFromObject(Type t)
        {
            var type = typeof(T);

            if (type.IsPrimitive || type is IEnumerable)
            {
                throw new NotSupportedException($"Type {type.GetType()} is not supported!");
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> GetRecursiveSchema(Type t)
        {
            return null;
        }
    }
}
