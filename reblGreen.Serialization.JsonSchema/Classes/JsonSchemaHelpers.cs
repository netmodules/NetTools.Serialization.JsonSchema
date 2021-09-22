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
        internal static JsonSchemaObject GetSchemaObject<T>(T t, byte depth = 0, byte maxDepth = 15)
        {
            Type type;
            JsonSchemaName name;
            List<JsonSchemaAttribute> attributes;

            string namedObject = null;

            if (t is MemberInfo memberInfo)
            {
                type = GetMemberType(memberInfo);
                name = memberInfo.GetCustomAttributes(typeof(JsonSchemaName), true).Select(x => x as JsonSchemaName).FirstOrDefault();
                attributes = memberInfo.GetCustomAttributes(typeof(JsonSchemaAttribute), true).Select(x => x as JsonSchemaAttribute).ToList();

                if (name != null)
                {
                    namedObject = name.GetName();
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
                    namedObject = name.GetName();
                }

                if (string.IsNullOrWhiteSpace(namedObject))
                {
                    namedObject = type.FullName;
                }
            }

            var schemaObject = new JsonSchemaObject()
            {
                Name = namedObject,
                TypeInfo = type.GetTypeInfo(),
            };

            JsonSchemaAttribute attribute = null;
            PrimitiveType primitiveType = null;
            JsonSchemaObject overrideSchema = null;

            if (attributes != null & attributes.Count > 0)
            {
                attribute = MergeJsonSchemaAttributes(attributes.ToArray());
                schemaObject.Attribute = attribute;

                if (attribute.TypeOverride != null)
                {
                    type = attribute.TypeOverride;
                    primitiveType = PrimitiveTypes.GetPrimitiveType(type);

                    overrideSchema = GetSchemaObject(type, depth, maxDepth);

                    if (overrideSchema.Attribute != null)
                    {
                        attributes = new List<JsonSchemaAttribute>() { overrideSchema.Attribute, attribute };
                    }

                    attribute = MergeJsonSchemaAttributes(attributes.ToArray());
                    schemaObject.Attribute = attribute;
                    schemaObject.PrimitiveType = primitiveType;
                }
                else
                {
                    primitiveType = PrimitiveTypes.GetPrimitiveType(type);

                    if (primitiveType != null && primitiveType.Constraints != null)
                    {
                        attributes.Add(primitiveType.Constraints);
                    }

                    attribute = MergeJsonSchemaAttributes(attributes.ToArray());
                    schemaObject.Attribute = attribute;
                    schemaObject.PrimitiveType = primitiveType;
                }
            }
            else
            {
                primitiveType = PrimitiveTypes.GetPrimitiveType(type);

                if (primitiveType != null)
                {
                    schemaObject.PrimitiveType = primitiveType;

                    if (primitiveType.Constraints != null)
                    {
                        schemaObject.Attribute = primitiveType.Constraints;
                    }

                    schemaObject.PrimitiveType = primitiveType;
                }
                else
                {
                    var fields = type.GetFields().ToList();
                    var properties = type.GetProperties().ToList();

                    if (fields.Count > 0 || properties.Count > 0)
                    {
                        schemaObject.Members = new List<JsonSchemaObject>();

                        while (depth < maxDepth)
                        {
                            depth++;

                            foreach (var field in fields)
                            {
                                schemaObject.Members.Add(GetSchemaObject(field, depth, maxDepth));
                            }

                            foreach (var prop in properties)
                            {
                                schemaObject.Members.Add(GetSchemaObject(prop, depth, maxDepth));
                            }

                            break;
                        }
                    }
                }
            }

            return schemaObject;
        }


        internal static Type GetMemberType(MemberInfo member)
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

        internal static JsonSchemaAttribute MergeJsonSchemaAttributes(params JsonSchemaAttribute[] attributes)
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

                attribute.AdditionalItems = attribute.AdditionalItems || other.AdditionalItems;
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

            }

            return attributes[0];
        }

        public static Dictionary<string, object> CreateSchemaDictionary(JsonSchemaObject obj, IJsonSchemaObjectParser parser)
        {
            Dictionary<string, object> schema = parser.ParseSchemaObject(obj);

            if (schema == null)
            {
                // Parse as standard object type???
                schema = new Dictionary<string, object>();
                schema.Add("type", "object");
            }

            return schema;
        }
    }
}
