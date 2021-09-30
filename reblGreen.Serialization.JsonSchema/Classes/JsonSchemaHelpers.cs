using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using reblGreen.Serialization.JsonSchemaAttributes;
using reblGreen.Serialization.JsonSchemaInterfaces;

namespace reblGreen.Serialization.JsonSchemaClasses
{
    internal class JsonSchemaHelpers
    {
        internal static JsonSchemaObject GetSchemaObject<T>(T t, byte maxDepth = 1, byte currentDepth = 0)
        {
            var schemaObject = GetSchemaObjectFromType(t, maxDepth, currentDepth);
            var primitiveType = PrimitiveTypes.GetPrimitiveType(schemaObject.TypeInfo);
            List<JsonSchemaAttribute> attributes = new List<JsonSchemaAttribute>();

            if (schemaObject == null)
            {
                var typeInfo = t.GetType().GetTypeInfo();

                schemaObject = new JsonSchemaObject()
                {
                    Attribute = primitiveType.Constraints,
                    PrimitiveType = primitiveType,
                    TypeInfo = typeInfo,
                    Name = typeInfo.Name
                };
            }

            if (primitiveType != null)
            {
                schemaObject.PrimitiveType = primitiveType;

                if (primitiveType.Constraints != null)
                {
                    if (schemaObject.Attribute != null)
                    {
                        attributes.Add(schemaObject.Attribute);
                    }

                    attributes.Add(primitiveType.Constraints);
                    schemaObject.Attribute = MergeJsonSchemaAttributes(attributes.ToArray());
                }
            }
            else
            {
                if (!schemaObject.TypeInfo.IsEnum)
                {
                    var fields = schemaObject.TypeInfo.GetFields().ToList();
                    var properties = schemaObject.TypeInfo.GetProperties().ToList();

                    if (fields.Count > 0 || properties.Count > 0)
                    {
                        schemaObject.Members = new List<JsonSchemaObject>();

                        while (currentDepth < maxDepth)
                        {
                            currentDepth++;

                            foreach (var field in fields)
                            {
                                // Attempt to stop loops where a member is an instance of the current type.
                                if (field.FieldType == schemaObject.TypeInfo.AsType())
                                {
                                    continue;
                                }

                                schemaObject.Members.Add(GetSchemaObject(field, maxDepth, currentDepth));
                            }

                            foreach (var prop in properties)
                            {
                                // Attempt to stop loops where a member is an instance of the current type.
                                if (prop.PropertyType == schemaObject.TypeInfo.AsType())
                                {
                                    continue;
                                }

                                schemaObject.Members.Add(GetSchemaObject(prop, maxDepth, currentDepth));
                            }

                            break;
                        }
                    }
                }
            }

            return schemaObject;
        }


        static JsonSchemaObject GetSchemaObjectFromType<T>(T t, byte maxDepth = 1, byte currentDepth = 0)
        {
            Type type;
            JsonSchemaName name;
            List<JsonSchemaAttribute> attributes;
            JsonSchemaAttribute attribute = null;
            JsonSchemaObject overrideSchema = null;

            string namedObject = null;

            if (t is MemberInfo memberInfo)
            {
                type = GetMemberType(memberInfo);
                name = memberInfo.GetCustomAttributes(typeof(JsonSchemaName), true).Select(x => x as JsonSchemaName).FirstOrDefault();
                attributes = memberInfo.GetCustomAttributes(typeof(JsonSchemaAttribute), true).Select(x => x as JsonSchemaAttribute).ToList();

                if (name != null)
                {
                    namedObject = name.Name;
                }

                if (string.IsNullOrWhiteSpace(namedObject))
                {
                    namedObject = memberInfo.Name;
                }
            }
            else
            {
                type = t.GetType();
                name = type.GetCustomAttributes(typeof(JsonSchemaName), true).Select(x => x as JsonSchemaName).FirstOrDefault();
                attributes = type.GetCustomAttributes(typeof(JsonSchemaAttribute), true).Select(x => x as JsonSchemaAttribute).ToList();

                if (name != null)
                {
                    namedObject = name.Name;
                }

                if (string.IsNullOrWhiteSpace(namedObject))
                {
                    namedObject = type.FullName;
                }
            }

            if (attributes != null & attributes.Count > 0)
            {
                attribute = MergeJsonSchemaAttributes(attributes.ToArray());
                
                if (attribute.TypeOverride != null)
                {
                    type = attribute.TypeOverride;
                    overrideSchema = GetSchemaObject(type, maxDepth, currentDepth);

                    if (overrideSchema.Attribute != null)
                    {
                        attributes = new List<JsonSchemaAttribute>() { overrideSchema.Attribute, attribute };
                    }

                    attribute = MergeJsonSchemaAttributes(attributes.ToArray());
                }
                else
                {
                    attribute = MergeJsonSchemaAttributes(attributes.ToArray());
                }
            }

            return new JsonSchemaObject()
            {
                Name = namedObject,
                TypeInfo = type.GetTypeInfo(),
                Attribute = attribute,
            };
        }


        static Type GetMemberType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.TypeInfo:
                    return ((TypeInfo)member).AsType();
                default:
                    throw new ArgumentException
                    (
                     "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }

        internal static Type[] BaseGenericTypes<T>(T instance)
        {
            Type type;
            Type objectType = typeof(object);//new object().GetType();

            if (instance is Type t)
            {
                type = t;
            }
            else
            {
                type = instance.GetType();
            }


            if (!type.IsGenericType)
            {
                while (type.BaseType != objectType)
                {
                    type = type.BaseType;

                    if (type.IsGenericType)
                    {
                        break;
                    }
                }
            }

            return type.GetGenericArguments();
        }

        static JsonSchemaAttribute MergeJsonSchemaAttributes(params JsonSchemaAttribute[] attributes)
        {
            if (attributes == null || attributes.Length == 0)
            {
                return null;
            }

            if (attributes.Length == 1)
            {
                return attributes[0];
            }

            for (var i = 1; i < attributes.Length; i++)
            {
                var attribute = attributes[0];
                var other = attributes[i];

                // Dirty manual merging time... We merge all attributes into the first attribute in the array and then return it.

                attribute.AdditionalItems = attribute.AdditionalItems != null ? attribute.AdditionalItems : other.AdditionalItems;
                attribute.AdditionalProperties = attribute.AdditionalProperties != null ? attribute.AdditionalProperties : other.AdditionalProperties;
                attribute.Default = attribute.Default != null ? attribute.Default : other.Default;
                attribute.Description = attribute.Description != null ? attribute.Description : other.Description;
                attribute.ExclusiveMaximum = attribute.ExclusiveMaximum || other.ExclusiveMaximum;
                attribute.ExclusiveMinimum = attribute.ExclusiveMinimum || other.ExclusiveMinimum;
                attribute.Format = attribute.Format != JsonSchemaAttribute.StringFormat.None ? attribute.Format : other.Format;
                attribute.Hidden = attribute.Hidden || other.Hidden;
                attribute.Maximum = attribute.Maximum != null ? attribute.Maximum : other.Maximum;
                attribute.MaxItems = attribute.MaxItems != null ? attribute.MaxItems : other.MaxItems;
                attribute.MaxLength = attribute.MaxLength != null ? attribute.MaxLength : other.MaxLength;
                attribute.MaxProperties = attribute.MaxProperties != null ? attribute.MaxProperties : other.MaxProperties;
                attribute.Minimum = attribute.Minimum != null ? attribute.Minimum : other.Minimum;
                attribute.MinItems = attribute.MinItems != null ? attribute.MinItems : other.MinItems;
                attribute.MinLength = attribute.MinLength != null ? attribute.MinLength : other.MinLength;
                attribute.MinProperties = attribute.MinProperties != null ? attribute.MinProperties : other.MinProperties;
                attribute.MultipleOf = attribute.MultipleOf != null ? attribute.MultipleOf : other.MultipleOf;
                attribute.Pattern = attribute.Pattern != null ? attribute.Pattern : other.Pattern;
                attribute.ReadOnly = attribute.ReadOnly || other.ReadOnly;
                attribute.Title = attribute.Title != null ? attribute.Title : other.Title;
                attribute.Type = attribute.Type != JsonSchemaAttribute.BasicType.Null && attribute.Type != JsonSchemaAttribute.BasicType.Unknown ? attribute.Type : other.Type;
                attribute.TypeOverride = attribute.TypeOverride != null ? attribute.TypeOverride : other.TypeOverride;
                attribute.UniqueItems = attribute.UniqueItems | other.UniqueItems;

                // Merge required properties array...
                if (attribute.Required != null && attribute.Required is string[] req1 && other.Required != null && other.Required is string[] req2)
                {
                    var len = req1.Length;

                    Array.Resize(ref req1, req1.Length + req2.Length);
                    
                    foreach (var s in req2)
                    {
                        req1[len] = s;
                        len++;
                    }
                }
                else if (attribute.Required != null && attribute.Required is bool b1 && other.Required != null && other.Required is bool b2)
                {
                    attribute.Required = b1 || b2;
                }
                else if (attribute.Required == null)
                {
                    attribute.Required = other.Required;
                }
            }

            return attributes[0];
        }
    }
}
