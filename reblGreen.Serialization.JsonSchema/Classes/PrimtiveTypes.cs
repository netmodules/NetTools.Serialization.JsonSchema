using reblGreen.Serialization.JsonSchemaAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaClasses
{
    internal static class PrimitiveTypes
    {
        internal static PrimitiveType GetPrimitiveType(Type type)
        {
            /*
             * The value of this keyword MUST be either a string or an array. If it is an array, elements of the array MUST be strings and MUST be unique.
             * String values MUST be one of the six primitive types ("null", "boolean", "object", "array", "number", or "string"), or "integer" which matches any number with a zero fractional part.
             * An instance validates if and only if the instance is in any of the sets listed for this keyword.
             */
            if (type.IsPointer || type.IsPrimitive || type == typeof(string))
            {
                // If the type is bool we simply check if json is "true" or "false". If it isn't either then we must assume false since we need to return a non-nullable object.
                if (type == typeof(string))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.String);
                if (type == typeof(bool))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Boolean);
                if (type == typeof(int))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(int.MinValue, int.MaxValue));
                if (type == typeof(uint))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(uint.MinValue, uint.MaxValue));
                if (type == typeof(long))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(long.MinValue, long.MaxValue));
                if (type == typeof(ulong))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(ulong.MinValue, ulong.MaxValue));
                if (type == typeof(short))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(short.MinValue, short.MaxValue));
                if (type == typeof(ushort))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(ushort.MinValue, ushort.MaxValue));
                if (type == typeof(byte))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(byte.MinValue, byte.MaxValue));
                if (type == typeof(float))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Number, new JsonSchemaMinMaxValue(float.MinValue, float.MaxValue));
                if (type == typeof(double))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Number, new JsonSchemaMinMaxValue(double.MinValue, double.MaxValue));
                if (type == typeof(decimal))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.Number, new JsonSchemaMinMaxValue(decimal.MinValue, decimal.MaxValue));
                if (type == typeof(char))
                    return new PrimitiveType(JsonSchemaAttribute.BasicType.String, new JsonSchemaMinMaxLength(1,1));
            }

            // Speciall case for IntPtr, is IntPtr a built in type?
            if (type == typeof(IntPtr))
                return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(int.MinValue, int.MaxValue));

            if (type == typeof(UIntPtr))
                return new PrimitiveType(JsonSchemaAttribute.BasicType.Integer, new JsonSchemaMinMaxValue(uint.MinValue, uint.MaxValue));

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                var genericTypes = JsonSchemaHelpers.BaseGenericTypes(type);
                return new PrimitiveType(JsonSchemaAttribute.BasicType.Object, new JsonSchemaAdditionalProperties(genericTypes[1]), genericTypes);
            }

            if (typeof(ICollection).IsAssignableFrom(type))
            {
                var genericTypes = JsonSchemaHelpers.BaseGenericTypes(type);
                return new PrimitiveType(JsonSchemaAttribute.BasicType.Array, new JsonSchemaAdditionalItems(genericTypes[0]), genericTypes);
            }

            return null;
        }
    }
}
