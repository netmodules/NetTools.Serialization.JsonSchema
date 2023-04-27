using NetTools.Serialization.JsonSchemaAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaClasses
{
    internal static class PrimitiveTypes
    {
        internal static PrimitiveType GetPrimitiveType(Type type)
        {
            // If the type is nullable we need to get the underlying type and iterate with it instead...
            var nullable = Nullable.GetUnderlyingType(type);

            if (nullable != null)
            {
                type = nullable;
            }

            /*
             * The value of this keyword MUST be either a string or an array. If it is an array, elements of the array MUST be strings and MUST be unique.
             * String values MUST be one of the six primitive types ("null", "boolean", "object", "array", "number", or "string"), or "integer" which matches any number with a zero fractional part.
             * An instance validates if and only if the instance is in any of the sets listed for this keyword.
             */
            if (type.IsPointer || type.IsPrimitive || type == typeof(string))
            {
                // If the type is bool we simply check if json is "true" or "false". If it isn't either then we must assume false since we need to return a non-nullable object.
                if (type == typeof(string))
                    return new PrimitiveType(BasicType.String);
                if (type == typeof(bool))
                    return new PrimitiveType(BasicType.Boolean);
                if (type == typeof(int))
                    return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(int.MinValue, int.MaxValue));
                if (type == typeof(uint))
                    return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(uint.MinValue, uint.MaxValue));
                if (type == typeof(long))
                    return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(long.MinValue, long.MaxValue));
                if (type == typeof(ulong))
                    return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(ulong.MinValue, ulong.MaxValue));
                if (type == typeof(short))
                    return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(short.MinValue, short.MaxValue));
                if (type == typeof(ushort))
                    return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(ushort.MinValue, ushort.MaxValue));
                if (type == typeof(byte))
                    return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(byte.MinValue, byte.MaxValue));
                if (type == typeof(float))
                    return new PrimitiveType(BasicType.Number, new JsonSchemaMinMaxValue(float.MinValue, float.MaxValue));
                if (type == typeof(double))
                    return new PrimitiveType(BasicType.Number, new JsonSchemaMinMaxValue(double.MinValue, double.MaxValue));
                if (type == typeof(decimal))
                    return new PrimitiveType(BasicType.Number, new JsonSchemaMinMaxValue(decimal.MinValue, decimal.MaxValue));
                if (type == typeof(char))
                    return new PrimitiveType(BasicType.String, new JsonSchemaMinMaxLength(1,1));
            }

            // Speciall case for IntPtr, is IntPtr a built in type?
            if (type == typeof(IntPtr))
                return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(int.MinValue, int.MaxValue));

            if (type == typeof(UIntPtr))
                return new PrimitiveType(BasicType.Integer, new JsonSchemaMinMaxValue(uint.MinValue, uint.MaxValue));

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                var genericTypes = JsonSchemaHelpers.BaseGenericTypes(type);

                if (type.IsGenericType && genericTypes.Length > 1 && genericTypes[1] != typeof(object))
                {
                    return new PrimitiveType(BasicType.Object, new JsonSchemaAdditionalProperties(genericTypes[1]), genericTypes);
                }

                var elementType = type.GetElementType();

                if (elementType != null && elementType != typeof(object))
                {
                    return new PrimitiveType(BasicType.Object, new JsonSchemaAdditionalItems(elementType));
                }

                return new PrimitiveType(BasicType.Object);
            }

            if (typeof(ICollection).IsAssignableFrom(type))
            {
                var genericTypes = JsonSchemaHelpers.BaseGenericTypes(type);

                if (type.IsGenericType && genericTypes.Length > 0 && genericTypes[0] != typeof(object))
                {
                    return new PrimitiveType(BasicType.Array, new JsonSchemaItems(genericTypes[0]), genericTypes);
                }

                var elementType = type.GetElementType();

                if (elementType != null && elementType != typeof(object))
                {
                    return new PrimitiveType(BasicType.Array, new JsonSchemaItems(elementType));
                }

                return new PrimitiveType(BasicType.Array, new JsonSchemaItems());
            }

            if (type == typeof(object))
            {
                return new PrimitiveType(BasicType.Any);
            }

            return null;
        }
    }
}
