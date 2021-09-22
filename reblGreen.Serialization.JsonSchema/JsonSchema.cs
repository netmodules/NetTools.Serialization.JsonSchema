using reblGreen.Serialization.JsonSchemaClasses;
using reblGreen.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization
{
    public class JsonSchema
    {
        Uri SchemaUrl;
        bool CamelCasing;

        /// <summary>
        /// 
        /// </summary>
        public JsonSchema(Uri @schemaUrl, bool camelCasing)
        {
            SchemaUrl = schemaUrl;
            CamelCasing = camelCasing;
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
            if (t == typeof(string) || t.IsPrimitive)
            {
                throw new NotSupportedException($"Type {t.Name} is not supported as a top level JSON Schema object.");
            }

            return GetRecursiveSchema(t);
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> GenerateJsonSchemaFromObject(JsonSchemaObject obj)
        {
            

            //return GetRecursiveSchema(obj);
            return null;
        }

        Dictionary<string, object> GetShallowSchema(Type t, IJsonSchemaObjectParser parser = null)
        {
            var schemaObject = GetJsonSchemaObject(t, 0, SchemaUrl);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> GetRecursiveSchema(Type t, IJsonSchemaObjectParser parser = null)
        {
            var schemaObject = GetJsonSchemaObject(t, 15, null);
            return GetJsonSchemaFromSchemaObject(schemaObject, parser);
        }

        public JsonSchemaObject GetJsonSchemaObject(Type t, byte maxDepth, Uri schemaUrl)
        {
            return JsonSchemaHelpers.GetSchemaObject(t, 0, maxDepth);
        }


        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> GetJsonSchemaFromSchemaObject(JsonSchemaObject o, IJsonSchemaObjectParser parser = null)
        {
            var schema = new Dictionary<string, object>();
            PopulateSchemaDictionary(schema, o);

            if (schema.TryGetValue("type", out var type) && type.ToString() == "object" && o.Members != null)
            {
                var properties = new Dictionary<string, object>();


                foreach (var m in o.Members)
                {
                    if (m.Attribute != null && m.Attribute.Hidden)
                    {
                        continue;
                    }

                    string name = null;

                    if (CamelCasing)
                    {
                        name = char.ToLowerInvariant(m.Name[0]) + m.Name.Substring(1);
                    }
                    else
                    {
                        name = m.Name;
                    }

                    properties.Add(name, GetJsonSchemaFromSchemaObject(m));
                }

                if (properties.Count > 0)
                {
                    schema.Add("properties", properties);
                }
            }

            return schema;
        }

        void PopulateSchemaDictionary(Dictionary<string, object> schema, JsonSchemaObject o)
        {
            if (o.PrimitiveType != null)
            {
                schema.Add("type", o.PrimitiveType.Name.ToString().ToLowerInvariant());
            }
            else if (o.Attribute == null)
            {
                schema.Add("type", "object");
                return;
            }
            else if (o.Attribute.Type != JsonSchemaAttribute.BasicType.Null)
            {
                schema.Add("type", o.Attribute.Type.ToString().ToLowerInvariant());
            }

            if (o.Attribute.Title != null)
                schema.Add("title", o.Attribute.Title.ToString());

            if (o.Attribute.Description != null)
                schema.Add("description", o.Attribute.Description.ToString());

            if (o.Attribute.Default != null)
                schema.Add("default", o.Attribute.Default.ToString());

            if (o.Attribute.AdditionalItems)
                schema.Add("additionalItems", true);

            if (o.Attribute.ExclusiveMaximum)
                schema.Add("exclusiveMaximum", true);

            if (o.Attribute.ExclusiveMinimum)
                schema.Add("exclusiveMinimum", true);

            if (o.Attribute.Format != JsonSchemaAttribute.StringFormat.None)
                schema.Add("format", o.Attribute.Format.ToString().ToLowerInvariant());

            if (o.Attribute.Maximum != null)
                schema.Add("maximum", o.Attribute.Maximum);

            if (o.Attribute.MaxItems != null)
                schema.Add("maxItems", o.Attribute.MaxItems);

            if (o.Attribute.MaxLength != null)
                schema.Add("maxLength", o.Attribute.MaxLength);

            if (o.Attribute.MaxProperties != null)
                schema.Add("maxProperties", o.Attribute.MaxProperties);

            if (o.Attribute.Minimum != null)
                schema.Add("minimum", o.Attribute.Minimum);

            if (o.Attribute.MinItems != null)
                schema.Add("minItems", o.Attribute.MinItems);

            if (o.Attribute.MinLength != null)
                schema.Add("minLength", o.Attribute.MinLength);

            if (o.Attribute.MinProperties != null)
                schema.Add("minProperties", o.Attribute.MinProperties);

            if (o.Attribute.MultipleOf != null)
                schema.Add("multipleOf", o.Attribute.MultipleOf);

            if (o.Attribute.Pattern != null)
                schema.Add("pattern", o.Attribute.Pattern.ToString());

            if (o.Attribute.ReadOnly)
                schema.Add("readOnly", true);

            //if (o.Attribute.TypeOverride != null)
            // Do nothing...

            if (o.Attribute.UniqueItems)
                schema.Add("uniqueItems", true);
        }
    }
}
