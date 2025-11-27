using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetTools.Serialization.JsonSchemaExtensions
{
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Returns the requested Dictionary item by key (if it exists) as the requested type. If the item is not of type T
        /// conversion is attempted with value types. If the item does not exist or it cannot be converted, the @default
        /// value is returned. This method is used by <see cref="GetDictionaryValueRecursive{T}(IDictionary{string, object}, T, string[])"/>
        /// </summary>
        public static T GetDictionaryValue<T>(this IDictionary<string, object> dict, T @default, string key)
        {
            try
            {
                if (dict != null && dict.TryGetValue(key, out object value))
                {
                    return value is T ? (T)value : (T)Convert.ChangeType(value, typeof(T));
                }
            }
            catch { }
            return @default;
        }
            
        /// <summary>
        /// Gets the dictionary value recursive.
        /// Returns the dictionary key value as T from either a single or nested dictionaries. If the value is unable to cast
        /// or is not found then the object assigned to @default is returned.
        /// </summary>
        /// <returns>The single or recursive dictionary value.</returns>
        /// <param name="dict">Dictionary to get the value from.</param>
        /// <param name="default">Default to return if returning the value fails.</param>
        /// <param name="keys">Single or recursive keys to look for in the dictionary or nested dictionaries.</param>
        /// <typeparam name="T">Type to cast and return.</typeparam>
        public static T GetDictionaryValueRecursive<T>(this IDictionary<string, object> dict, T @default, params string[] keys)
        {
            if (dict != null && keys.Length > 1)
            {
                for (var i = 0; i < keys.Length - 1; i++)
                {
                    dict = dict[keys[i]] as IDictionary<string, object>;

                    if (dict == null)
                    {
                        return @default;
                    }
                }
            }

            return GetDictionaryValue(dict, @default, keys[keys.Length - 1]);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            return new T().ToObject(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToObject<T>(this T obj, IDictionary<string, object> source)
            where T : class
        {
            var type = typeof(T);

            foreach (var item in source)
            {
                try
                {
                    type.GetProperty(item.Key).SetValue(obj, item.Value, null);
                }
                catch { }
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="includeFields"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public static IDictionary<string, object> AsDictionary<T>(this T source, bool includeFields = false, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
            where T : class
        {
            var props = source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

            if (includeFields)
            {
                var fields = source.GetType().GetFields(bindingAttr).ToDictionary
                (
                    fieldInfo => fieldInfo.Name,
                    fieldInfo => fieldInfo.GetValue(source)
                );

                return props.Concat(fields).ToDictionary(k => k.Key, v => v.Value);
            }

            return props;
        }
    }
}
