using reblGreen.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaClasses
{
    internal class DefaultJsonSchemaObjectParser : IJsonSchemaObjectParser
    {
        string SchemaDraft = "https://json-schema.org/draft/2020-12/schema";
        private JsonSchemaOptions Options;

        public DefaultJsonSchemaObjectParser(JsonSchemaOptions options = null)
        {
            if (options == null)
            {
                options = new JsonSchemaOptions()
                {
                    AutoCamelCase = true
                };
            }

            this.Options = options;
        }

        public Dictionary<string, object> GetSchemaDictionaryFromJsonSchemaObject(JsonSchemaObject o)
        {
            return GetSchemaDictionaryFromJsonSchemaObject(o, false);
        }


        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> GetSchemaDictionaryFromJsonSchemaObject(JsonSchemaObject o, bool isRef = false)
        {
            var schema = new Dictionary<string, object>();

            if (Options.SchemaRefUrl != null)
            {
                var refKey = isRef ? "$ref" : "$id";
                schema.Add(refKey, Options.SchemaRefUrl.ToString() + o.TypeInfo.FullName);
            }

            PopulateSchemaDictionary(schema, o);

            if (schema.TryGetValue("type", out var type) && o.Members != null)
            {
                var strType = type.ToString();

                if (strType == "object" || strType == "unknown")
                {
                    schema["type"] = "object";
                    var properties = new Dictionary<string, object>();
                    var required = new List<string>();

                    foreach (var m in o.Members)
                    {
                        if (m.Attribute != null && m.Attribute.Hidden)
                        {
                            continue;
                        }

                        string name = null;

                        if (Options.AutoCamelCase)
                        {
                            name = char.ToLowerInvariant(m.Name[0]) + (m.Name.Length > 1 ? m.Name.Substring(1) : "");
                        }
                        else
                        {
                            name = m.Name;
                        }

                        properties.Add(name, GetSchemaDictionaryFromJsonSchemaObject(m, true));

                        if (m.Attribute != null && m.Attribute.Required != null)
                        {
                            if (m.Attribute.Required is bool b)
                            {
                                required.Add(name);
                            }
                        }
                    }

                    if (properties.Count > 0)
                    {
                        schema.Add("properties", properties);
                    }

                    if (required.Count > 0)
                    {
                        if (schema.TryGetValue("required", out var r) && r is List<string> req)
                        {
                            req.AddRange(required);
                        }
                        else
                        {
                            schema.Add("required", required);
                        }
                    }
                }
            }

            return schema;
        }

        void PopulateSchemaDictionary(Dictionary<string, object> schema, JsonSchemaObject o)
        {
            if (o.TypeInfo.IsEnum && o.PrimitiveType == null && o.Attribute == null)
            {
                schema.Add("enum", Enum.GetNames(o.TypeInfo.AsType()));
                return;
            }
            else if (o.PrimitiveType != null)
            {
                schema.Add("type", o.PrimitiveType.Name.ToString().ToLowerInvariant());
            }
            else if (o.Attribute == null)
            {
                schema.Add("type", "object");
                return;
            }
            else if (o.TypeInfo.IsEnum && o.Attribute.TypeOverride == null)
            {
                schema.Add("enum", Enum.GetNames(o.TypeInfo.AsType()));
            }
            else if (o.Attribute.TypeOverride != null && o.Attribute.TypeOverride.IsEnum)
            {
                schema.Add("enum", Enum.GetNames(o.Attribute.TypeOverride));
            }
            else if (o.Attribute.Type != JsonSchemaAttribute.BasicType.Null)
            {
                schema.Add("type", o.Attribute.Type.ToString().ToLowerInvariant());
            }

            if (o.Attribute.Required != null)
            {
                if (o.Attribute.Required is string[] arr)
                {
                    if (Options.AutoCamelCase)
                    {
                        arr = arr.Select(x => char.ToLowerInvariant(x[0]) + (x.Length > 1 ? x.Substring(1) : "")).ToArray();
                    }
                    if (schema.TryGetValue("required", out var required) && required is List<string> r)
                    {
                        r.AddRange(arr);
                    }
                    else
                    {
                        schema.Add("required", new List<string>(arr));
                    }
                }
            }

            if (o.Attribute.Title != null)
                schema.Add("title", o.Attribute.Title.ToString());

            if (o.Attribute.Description != null)
                schema.Add("description", o.Attribute.Description.ToString());

            if (o.Attribute.Default != null)
                schema.Add("default", o.Attribute.Default.ToString());

            // By default, additional items and additional properties are allowed in JSON schema unless strictly set to false or another
            // JSON schema. Currently we only support 
            if (o.Attribute.AdditionalItems is bool additionalItems && !additionalItems)
            {
                schema.Add("additionalItems", false);
            }
            else
            {
                // Do something in the future with support for setting additionalItems as another JSON schema...
            }

            if (o.Attribute.AdditionalProperties is bool additionalProperties && !additionalProperties)
            {
                schema.Add("additionalProperties", false);
            }
            else
            {
                // Do something in the future with support for setting additionalProperties as another JSON schema...
            }

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
