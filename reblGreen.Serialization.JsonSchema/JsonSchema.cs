using reblGreen.Serialization.JsonSchemaClasses;
using reblGreen.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization
{
    public class JsonSchema
    {
        IJsonSchemaObjectParser Parser;

        /// <summary>
        /// 
        /// </summary>
        public JsonSchema(JsonSchemaOptions options)
        {
            Parser = new DefaultJsonSchemaObjectParser(options);
        }

        /// <summary>
        /// This option lets you add your own custom JsonSchemaObjectParser class. If this option is null, the default parser
        /// will be used.
        /// </summary>
        public JsonSchema(IJsonSchemaObjectParser parser)
        {
            if (parser == null)
            {
                Parser = new DefaultJsonSchemaObjectParser();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> FromType<T>(byte propertyDepth = 1)
        {
            return FromType(typeof(T), propertyDepth);
        }


        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> FromType(Type t, byte propertyDepth = 1)
        {
            var schemaObject = GetJsonSchemaObject(t, propertyDepth);
            return FromJsonSchemaObject(schemaObject);
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> FromJsonSchemaObject(JsonSchemaObject schemaObject)
        {
            return Parser.GetSchemaDictionaryFromJsonSchemaObject(schemaObject);
        }


        public JsonSchemaObject GetJsonSchemaObject(Type t, byte propertyDepth)
        {
            return JsonSchemaHelpers.GetSchemaObject(t, propertyDepth, 0);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //Dictionary<string, object> GetRecursiveSchema(Type t, byte depth)
        //{
        //    var schemaObject = GetJsonSchemaObject(t, depth, null);
        //    return GetJsonSchemaFromSchemaObject(schemaObject, false, parser);
        //}

        


        
    }
}
