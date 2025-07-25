﻿using NetTools.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;
using System.Reflection;

namespace NetTools.Serialization.JsonSchemaClasses
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
            if (Options.TypeOverrides != null)
            {
                if (Options.TypeOverrides.TryGetValue(o.TypeInfo.AsType(), out var overrideSchema) && overrideSchema != null)
                {
                    o.Attribute = JsonSchemaHelpers.MergeJsonSchemaAttributes(overrideSchema, o.Attribute);
                }
            }

            var schema = new Dictionary<string, object>();

            // If the schema is top-level we add the schema draft property to the schema object.
            if (!isRef)
            {
                schema.Add("$schema", SchemaDraft);
            }

            // Deciding whether to add a schema reference or id... Id is used for top-level schema and ref is used for sub-schemas.
            if (Options.SchemaRefUrl != null)
            {
                string refKey;

                if (isRef)
                {
                    refKey = "$ref";
                }
                else
                {
                    refKey = "$id";
                }

                schema.Add(refKey, GetPrettySchemaRefString(o));
            }


            // Adding of all class-level JsonSchemaAttribute flags...
            PopulateSchemaDictionary(schema, o);


            // Do sub-schemas for members (properties and fields)
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

                        if (Options.SchemaType == JsonSchemaOptions.JsonSchemaType.Nested)
                        {
                            if (properties.ContainsKey(name))
                            {
                                properties[name] = GetSchemaDictionaryFromJsonSchemaObject(m, true);
                            }
                            else
                            {
                                properties.Add(name, GetSchemaDictionaryFromJsonSchemaObject(m, true));
                            }
                        }
                        else
                        {
                            if (Options.SchemaRefUrl != null)
                            {
                                var subSchemaRef = new Dictionary<string, object>() { { "$ref", GetPrettySchemaRefString(m) } };
                                if (properties.ContainsKey(name))
                                {
                                    properties[name] = subSchemaRef;
                                }
                                else
                                {
                                    properties.Add(name, subSchemaRef);
                                }
                            }
                        }

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
            var isNullable = false;
            var nullable = Nullable.GetUnderlyingType(o.TypeInfo.AsType());

            if (nullable != null)
            {
                isNullable = true;
                o.TypeInfo = nullable.GetTypeInfo();
            }

            if (o.TypeInfo.IsEnum && o.PrimitiveType == null && o.Attribute == null)
            {
                var names = Enum.GetNames(o.TypeInfo.AsType());

                if (isNullable)
                {
                    var nameList = names.ToList();
                    nameList.Insert(0, null);
                    names = nameList.ToArray();
                }

                if (Options.AutoCamelCase)
                {
                    schema.Add("enum", names.Select(n => char.ToLowerInvariant(n[0])
                    + (n.Length > 1 ? n.Substring(1) : "")).ToArray());
                }
                else
                {
                    schema.Add("enum", names);
                }

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
                var names = Enum.GetNames(o.TypeInfo.AsType());

                if (isNullable)
                {
                    var nameList = names.ToList();
                    nameList.Insert(0, null);
                    names = nameList.ToArray();
                }

                if (o.Attribute.ValueNamingConvention == NamingConvention.Uppercase)
                {
                    schema.Add("enum", names.Select(n => n.ToUpperInvariant()).ToArray());
                }
                else if (o.Attribute.ValueNamingConvention == NamingConvention.Lowercase)
                {
                    schema.Add("enum", names.Select(n => n.ToLowerInvariant()).ToArray());
                }
                else if (o.Attribute.ValueNamingConvention == NamingConvention.Automatic && Options.AutoCamelCase)
                {
                    schema.Add("enum", names.Select(n => char.ToLowerInvariant(n[0])
                    + (n.Length > 1 ? n.Substring(1) : "")).ToArray());
                }
                else
                {
                    schema.Add("enum", names);
                }
            }
            else if (o.Attribute.TypeOverride != null && o.Attribute.TypeOverride.IsEnum)
            {
                var names = Enum.GetNames(o.Attribute.TypeOverride);

                if (isNullable)
                {
                    var nameList = names.ToList();
                    nameList.Insert(0, null);
                    names = nameList.ToArray();
                }

                if (o.Attribute.ValueNamingConvention == NamingConvention.Uppercase)
                {
                    schema.Add("enum", names.Select(n => n.ToUpperInvariant()).ToArray());
                }
                else if (o.Attribute.ValueNamingConvention == NamingConvention.Lowercase)
                {
                    schema.Add("enum", names.Select(n => n.ToLowerInvariant()).ToArray());
                }
                else if (o.Attribute.ValueNamingConvention == NamingConvention.Automatic && Options.AutoCamelCase)
                {
                    schema.Add("enum", names.Select(n => char.ToLowerInvariant(n[0])
                    + (n.Length > 1 ? n.Substring(1) : "")).ToArray());
                }
                else
                {
                    schema.Add("enum", names);
                }
            }
            else if (o.Attribute.Type != BasicType.Null)
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

            if (o.Attribute.Items != null)
            {
                if (o.Attribute.Items is string items && items == "any")
                {
                    if (Options.UseAnyTypeArray)
                    {
                        schema.Add("items", new Dictionary<string, object> { { "type", new List<string> { "number", "string", "boolean", "object", "array", "null" } } });
                    }
                    else
                    {
                        schema.Add("items", new Dictionary<string, object> { { "type", "any" } });
                    }
                }
                else
                {
                    // Do something in the future with support for setting additionalItems as another JSON schema...
                    var genericItem = JsonSchemaHelpers.GetSchemaObject(o.Attribute.Items, 5, 0);
                    schema.Add("items", GetSchemaDictionaryFromJsonSchemaObject(genericItem, true));
                }
            }

            // By default, additional items and additional properties are allowed in JSON schema unless strictly set to false or another
            // JSON schema.
            if (o.Attribute.AdditionalItems != null)
            {
                if (o.Attribute.AdditionalItems is bool additionalItems)
                {
                    if (!additionalItems)
                    {
                        schema.Add("additionalItems", false);
                    }
                }
                else
                {
                    // Do something in the future with support for setting additionalItems as another JSON schema...
                    var genericItem = JsonSchemaHelpers.GetSchemaObject(o.Attribute.AdditionalItems, 5, 0);
                    schema.Add("additionalItems", GetSchemaDictionaryFromJsonSchemaObject(genericItem, true));
                }
            }

            if (o.Attribute.AdditionalProperties != null)
            {
                if (o.Attribute.AdditionalProperties is bool additionalProperties)
                {
                    if (!additionalProperties)
                    {
                        schema.Add("additionalProperties", false);
                    }
                }
                else
                {
                    // Do something in the future with support for setting additionalProperties as another JSON schema...
                    var genericProperty = JsonSchemaHelpers.GetSchemaObject(o.Attribute.AdditionalProperties, 5, 0);
                    schema.Add("additionalProperties", GetSchemaDictionaryFromJsonSchemaObject(genericProperty, true));
                }
            }

            if (o.Attribute.ExclusiveMaximum)
                schema.Add("exclusiveMaximum", true);

            if (o.Attribute.ExclusiveMinimum)
                schema.Add("exclusiveMinimum", true);

            if (o.Attribute.Format != StringFormat.None)
                schema.Add("format", o.Attribute.Format.ToString().ToLowerInvariant() == "datetime"
                    ? "date-time" : o.Attribute.Format.ToString().ToLowerInvariant());

            if (o.Attribute.AdditionalFormats != null)
                schema.Add("additionalFormats", o.Attribute.AdditionalFormats.Select(x=> x.ToString().ToLowerInvariant() == "datetime"
                    ? "date-time" : o.Attribute.Format.ToString().ToLowerInvariant()));

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

            if (o.Attribute.CustomAttributes != null)
            {
                foreach (var att in o.Attribute.CustomAttributes)
                {
                    schema[att.Key] = att.Value;
                }
            }
        }


        string GetPrettySchemaRefString(JsonSchemaObject o)
        {
            var schemaRef = Options.SchemaRefUrl.ToString();
            string typeName = string.Empty;
            List<string> generics = new List<string>();

            //if (o.PrimitiveType != null)
            //{
            //    typeName = "." + o.PrimitiveType.Name.ToString();

            //    if (o.PrimitiveType.Children != null)
            //    {
            //        foreach (var c in o.PrimitiveType.Children)
            //        {
            //            var generic = c.FullName;
            //            var systemGeneric = generic.IndexOf("System.");

            //            if (systemGeneric == 0)
            //            {
            //                generic = generic.Substring(6);
            //                generics.Add(generic);
            //            }
            //        }
            //    }
            //}
            //else
            if (o.Attribute != null && o.Attribute.TypeOverride != null)
            {
                typeName = o.Attribute.TypeOverride.FullName;
            }
            else if (o.TypeInfo.IsGenericType)
            {
                typeName = o.TypeInfo.FullName;

                foreach (var c in o.TypeInfo.GenericTypeArguments)
                {
                    var generic = c.FullName;
                    var systemGeneric = generic.IndexOf("System.");

                    if (systemGeneric == 0)
                    {
                        generic = generic.Substring(6);
                    }

                    generics.Add(generic);
                }
            }
            else
            {
                typeName = o.TypeInfo.FullName;
            }

            typeName = typeName.Split('`')[0];
            var system = typeName.IndexOf("System.");

            if (system == 0)
            {
                typeName = typeName.Substring(6); 
            }

            return schemaRef + typeName + (generics.Count > 0 ? "+" : "") + string.Join("+", generics.Select(g=>g.Split('`')[0])) + ".json";
        }
    }
}
