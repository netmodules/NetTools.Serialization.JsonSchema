using NetTools.Serialization.JsonSchemaExtensions;
using NetTools.Serialization.JsonSchemaInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace NetTools.Serialization.JsonSchemaClasses
{
    internal static class JsonSchemaValidation
    {
        internal static bool ValidateField(object obj, Dictionary<string, object> schema, IJsonSchemaStringValidators validators, out List<string> details, bool ignoreEnumCase = false, bool ignoreEnumSpaces = false, bool allowNumericStrings = false, string name = "property")
        {
            details = new List<string>();
            
            if (schema == null)
            {
                details.Add("The reference schema to validate against is null.");
                return false;
            }

            if (schema.TryGetValue("required", out var requiredProperties) && requiredProperties is IEnumerable<string> required && required.Count() > 0)
            {
                var dictionary = obj as Dictionary<string, object>;

                foreach (var property in required)
                {
                    if (dictionary == null || !dictionary.ContainsKey(property) || dictionary[property] == null
                        || (dictionary[property] is string prop) && string.IsNullOrEmpty(prop))
                    {
                        details.Add($"The following property is required: {property}");
                        return false;
                    }
                }
            }

            var type = schema.GetDictionaryValueRecursive<string>(null, "type");
            
            if (type == "object")
            {
                var properties = schema.GetDictionaryValueRecursive<Dictionary<string, object>>(null, "properties");
                var dictionary = obj as Dictionary<string, object>;

                if (properties != null)
                {
                    foreach (var property in properties)
                    {
                        var propertyDic = property.Value as Dictionary<string, object>;
                        object propertyValue = null;

                        dictionary?.TryGetValue(property.Key, out propertyValue);

                        if (propertyDic != null && propertyDic.GetDictionaryValueRecursive(false, "required")
                            && (dictionary == null || !dictionary.ContainsKey(property.Key)))
                        {
                            details.Add($"The following property is required: {property.Key}");
                            return false;
                        }

                        if (!ValidateField(propertyValue, propertyDic, validators, out details, ignoreEnumCase, ignoreEnumSpaces, allowNumericStrings, property.Key))
                        {
                            return false;
                        }
                    }

                    // need to handle additionalProperties anyOf oneOf...
                    //var additionalProperties = schema.GetDictionaryValueRecursive<Dictionary<string, object>>(null, "additionalProperties");
                }
            }
            else if (type == "array" && !ValidateArray(validators, name, obj, schema, out details, ignoreEnumCase, ignoreEnumSpaces))
            {
                return false;
            }
            else if (type == "boolean" && obj != null && !(obj is bool))
            {
                details.Add($"{name} must be a boolean value.");
                return false;
            }
            else if ((type == "integer" || type == "number") && !ValidateNumericValue(name, obj, schema, out details, type == "integer", allowNumericStrings))
            {
                return false;
            }
            else if (type == "string" && !ValidateString(validators, name, obj, schema, out details))
            {
                return false;
            }

            // Enums currently only supports .NET string named enums and NetTools.Serialization.JsonSchema generates a string[] array
            //
            var jsEnum = schema.GetDictionaryValueRecursive<ICollection>(null, "enum");

            if (jsEnum != null && !ValidateEnum(name, obj, jsEnum, out details, ignoreEnumCase, ignoreEnumSpaces))
            {
                return false;
            }

            return true;
        }

        private static bool ValidateEnum(string name, object obj, ICollection jsEnum, out List<string> details, bool ignoreStringCase = false, bool ignoreStringSpaces = false)
        {
            var str = "";
            details = new List<string>();

            if (obj == null || jsEnum == null)
            {
                return true;
            }

            if (ignoreStringSpaces && obj is string)
            {
                str = obj.ToString().Replace(" ", "").Replace(".", "").Replace("-", "");
            }
            else
            {
                str = obj.ToString();
            }

            foreach (var e in jsEnum)
            {
                if (ignoreStringCase && ignoreStringSpaces && e is string item
                    && (item.Equals(obj.ToString(), StringComparison.OrdinalIgnoreCase) || item.Equals(str, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
                else if (ignoreStringCase && e is string itm && itm.Equals(obj.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (ignoreStringSpaces && e is string tem
                    && (tem.Equals(obj.ToString(), StringComparison.Ordinal) || tem.Equals(str, StringComparison.Ordinal)))
                {
                    return true;
                }
                else if (e.Equals(obj))
                {
                    return true;
                }
            }

            var error = $"{name} must be equal to one of the following enum values: ";

            // For some reason string.Join(", " jsEnum) doesn't work and just outputs "System.String[]" so having
            // to iterate manually for the second time to generate output message...
            foreach (var e in jsEnum)
            {
                error += e.ToString() + ", ";
            }  

            // Try and work out what typeof(jsEnum) is (string[], List<object>, or List<string>)...
            details.Add(error.TrimEnd(' ', ',') + ".");
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private static bool ValidateArray(IJsonSchemaStringValidators validators, string name, object obj, Dictionary<string, object> schema, out List<string> details, bool ignoreEnumCase = false, bool ignoreEnumSpaces = false)
        {
            details = new List<string>();

            if (obj == null)
            {
                return true;
            }

            if (obj is IDictionary || !(obj is ICollection))
            {
                details.Add($"{name} must be an array value.");
                return false;
            }

            var enumerable = obj as ICollection;

            var items = schema.GetDictionaryValueRecursive(null as Dictionary<string, object>, "items");

            if (items != null)
            {
                var itemTypes = new List<string>();
                if (items.TryGetValue("type", out var type) && type != null)
                {
                    itemTypes.Add(type.ToString());
                }

                itemTypes.AddRange(items.Select(x => (x.Value as Dictionary<string, object>)?["type"] as string));

                // Additional check for nested enum array...
                var jsEnum = schema.GetDictionaryValueRecursive(null as string[], "enum");

                // Super inefficient but we must iterate each item and validate against the allowed items in items...
                foreach (var i in enumerable)
                {
                    if (jsEnum != null && !ValidateEnum($"Child element of {name}", i, jsEnum, out details, ignoreEnumCase, ignoreEnumSpaces))
                    {
                        return false;
                    }

                    if (i is string s)
                    {
                        if (!itemTypes.Contains("string"))
                        {
                            details.Add($"{name} must not contain any string values: {i}");
                            return false;
                        }

                        return ValidateString(validators, $"Child element of {name}", i, items, out details);
                    }
                    else if (i is bool && !itemTypes.Contains("boolean"))
                    {
                        details.Add($"{name} must not contain any boolean values: {i}");
                        return false;
                    }
                    else if (i is int || i is uint || i is long || i is ulong || i is short || i is ushort || i is float || i is double || i is decimal || i is byte)
                    {
                        if (!itemTypes.Contains("integer") && !itemTypes.Contains("number"))
                        {
                            details.Add($"{name} must not contain any numeric values: {i}");
                            return false;
                        }

                        return ValidateNumericValue($"Child element of {name}", i, items, out details, itemTypes.Contains("integer"), false);
                    }
                    else if (i is IDictionary)
                    {
                        if (!itemTypes.Contains("object"))
                        {
                            details.Add($"{name} must not contain any object values: {i}");
                            return false;
                        }

                        return ValidateField(i, items, validators, out details, ignoreEnumCase, ignoreEnumSpaces, false, $"Child element of {name}");
                    }
                    else if (!(i is IDictionary) && i is ICollection)
                    {
                        if (!itemTypes.Contains("array"))
                        {
                            details.Add($"{name} must not contain any array values: {i}");
                            return false;
                        }

                        return ValidateArray(validators, $"Child element of {name}", i, items, out details, ignoreEnumCase, ignoreEnumSpaces);
                    }
                }
            }

            var minItems = schema.GetDictionaryValueRecursive(0, "minItems");
            
            if (minItems > 0 && (enumerable == null || enumerable.Count < minItems))
            {
                details.Add($"{name} must contain a minimum of {minItems} items.");
                return false;
            }

            var maxItems = schema.GetDictionaryValueRecursive(int.MaxValue, "maxItems");

            if (maxItems > 0 && enumerable != null && enumerable.Count > maxItems)
            {
                details.Add($"{name} must contain a maximum of {minItems} items.");
                return false;
            }

            return true;
        }

        private static bool ValidateString(IJsonSchemaStringValidators validators, string name, object obj, Dictionary<string, object> schema, out List<string> details)
        {
            details = new List<string>();

            if (obj == null)
            {
                return true;
            }

            if (!(obj is string))
            {
                details.Add($"{name} must be a string value.");
                return false;
            }

            var str = obj as string;

            var minLength = schema.GetDictionaryValueRecursive(0, "minLength");

            if (minLength > 0 && str.Length < minLength)
            {
                details.Add($"{name} must be a minimum of {minLength} characters.");
                return false;
            }

            var maxLength = schema.GetDictionaryValueRecursive(int.MaxValue, "maxLength");

            if (maxLength > 0 && str.Length > maxLength)
            {
                details.Add($"{name} must be a maximum of {maxLength} items.");
                return false;
            }

            if (schema.TryGetValue("pattern", out var pStr) && pStr != null)
            {
                var pattern = pStr.ToString();

                if (!Regex.IsMatch(str, pattern))
                {
                    details.Add($"{name} must match the regular expression pattern: {pattern}");
                    return false;
                }
            }

            List<object> formats = new List<object>();

            if (schema.TryGetValue("format", out var f))
            {
                formats.Add(f);
            }

            if (schema.TryGetValue("additionalFormats", out var a) && a is ICollection af)
            {
                foreach (var fo  in af)
                {
                    formats.Add(fo);
                }
            }

            foreach (var format in formats)
            {    
                switch (format)
                {
                    case "alpha":
                        if (validators != null && validators.Alpha != null && !validators.Alpha(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "alphanumeric":
                        if (validators != null && validators.Alphanumeric != null && !validators.Alphanumeric(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "lowercase":
                        if (validators != null && validators.Lowercase != null && !validators.Lowercase(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "uppercase":
                        if (validators != null && validators.Uppercase != null && !validators.Uppercase(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "numeric":
                        if (validators != null && validators.Numeric != null && !validators.Numeric(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "symbol":
                        if (validators != null && validators.Symbol != null && !validators.Symbol(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "base64":
                    case "filebase64":
                        if (validators != null && validators.Base64 != null && !validators.Base64(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "color":
                        if (validators != null && validators.Color != null && !validators.Color(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "csv":
                        if (validators != null && validators.Csv != null && !validators.Csv(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "date":
                        if (validators != null && validators.Date != null && !validators.DateTime(str))
                        {
                            details.Add($"{name} must match an ISO 8601 {format} format.");
                            return false;
                        }
                        break;
                    case "date-time":
                        if(validators != null && validators.DateTime != null && !validators.DateTime(str))
                        {
                            details.Add($"{name} must match an ISO 8601 {format} format.");
                            return false;
                        }
                        break;
                    case "time":
                        if (validators != null && validators.Time != null && !validators.DateTime(str))
                        {
                            details.Add($"{name} must match an ISO 8601 {format} format.");
                            return false;
                        }
                        break;
                    case "duration":
                        if (validators != null && validators.Duration != null && !validators.Duration(str))
                        {
                            details.Add($"{name} must match an ISO 8601 {format} format.");
                            return false;
                        }
                        break;
                    case "email":
                        if (validators != null && validators.Email != null && !validators.Email(str))
                        {
                            details.Add($"{name} must match an {format} format.");
                            return false;
                        }
                        break;
                    case "hostname":
                        if (validators != null && validators.Hostname != null && !validators.Hostname(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "html":
                        if (validators != null && validators.Html != null && !validators.Html(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "ipv4":
                        if (validators != null && validators.IPV4 != null && !validators.IPV4(str))
                        {
                            details.Add($"{name} must match an {format} format.");
                            return false;
                        }
                        break;
                    case "ipv6":
                        if (validators != null && validators.IPV6 != null && !validators.IPV6(str))
                        {
                            details.Add($"{name} must match an {format} format.");
                            return false;
                        }
                        break;
                    case "json":
                        if (!validators.Json(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "multiline":
                        if (validators != null && validators.Multiline != null && !validators.Multiline(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "pngimagebase64":
                    case "filepngimagebase64":
                        if (validators != null && validators.PngImageBase64 != null && !validators.PngImageBase64(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "filebytes":
                        if (validators != null && validators.FileBytes != null && !validators.FileBytes(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "richtext":
                        if (validators != null && validators.RichText != null && !validators.RichText(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "uri":
                        if (validators != null && validators.Uri != null && !validators.Uri(str))
                        {
                            details.Add($"{name} must match a {format} format.");
                            return false;
                        }
                        break;
                    case "xml":
                        if (validators != null && validators.Xml != null && !validators.Xml(str))
                        {
                            details.Add($"{name} must match an {format} format.");
                            return false;
                        }
                        break;
                }
            }
            
            return true;
        }

        static bool ValidateNumericValue(string name, object value, Dictionary<string, object> schema, out List<string> details, bool integer = false, bool allowNumericStrings = false)
        {
            details = new List<string>();
            
            if (value == null)
            {
                return true;
            }

            if ((allowNumericStrings || !(value is string)) && decimal.TryParse(value.ToString(), out var numVal))
            {
                if (integer && numVal % 1 != 0)
                {
                    details.Add($"{name} must be an integer value and must not contain a decimal point.");
                    return false;
                }

                decimal min = 0;
                decimal max = decimal.MaxValue;
                decimal multipleOf = 0;

                if (schema.TryGetValue("minimum", out var minObj)
                    && minObj != null && decimal.TryParse(minObj.ToString(), out min) && numVal < min)
                {
                    details.Add($"{name} must be greater than or equal to {min}.");
                    return false;
                }

                if (schema.TryGetValue("exclusiveMinimum", out var exMinObj) && exMinObj != null)
                {
                    if (decimal.TryParse(exMinObj.ToString(), out min) && numVal <= min)
                    {
                        details.Add($"{name} must be greater than {min}.");
                        return false;
                    }
                    else if (bool.TryParse(exMinObj.ToString(), out var exBool) && exBool && numVal <= min)
                    {
                        details.Add($"{name} must be greater than {min}.");
                        return false;
                    }
                }

                if (schema.TryGetValue("maximum", out var maxObj)
                    && maxObj != null && decimal.TryParse(maxObj.ToString(), out max) && numVal > max)
                {
                    details.Add($"{name} must be less than or equal to {max}.");
                    return false;
                }

                if (schema.TryGetValue("exclusiveMaximum", out var exMaxObj) && exMaxObj != null)
                {
                    if (decimal.TryParse(exMaxObj.ToString(), out max) && numVal >= max)
                    {
                        details.Add($"{name} must be less than {max}.");
                        return false;
                    }
                    else if (bool.TryParse(exMaxObj.ToString(), out var exBool) && exBool && numVal >= min)
                    {
                        details.Add($"{name} must be less than {max}.");
                        return false;
                    }
                }

                if (schema.TryGetValue("multipleOf", out var multipleObj)
                    && multipleObj != null && decimal.TryParse(multipleObj.ToString(), out multipleOf) && numVal % multipleOf != 0)
                {
                    details.Add($"{name} must be multiples of {multipleOf}.");
                    return false;
                }
            }
            else
            {
                details.Add($"{name} must be a numeric value.");
                return false;
            }

            return true;
        }
    }
}
