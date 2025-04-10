using NetTools.Serialization.JsonSchemaAttributes;
using NetTools.Serialization.JsonSchemaEnums;
using NetTools.Serialization.JsonSchemaInterfaces;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace NetTools.Serialization.JsonSchemaClasses
{
    internal class DefaultJsonSchemaStringValidators : IJsonSchemaStringValidators
    {
        /// <summary>
        /// The default assigned string validator will check to see if a string contains only alpha characters
        /// and return true that the property validates if it is either null (empty) or conversion is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Alpha { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || property.All(x => char.IsLetter(x));
            }
            catch { }
            return false;
        };

        /// <summary>
        /// The default assigned string validator will check to see if a string contains only alpha and numeric characters
        /// and return true that the property validates if it is either null (empty) or conversion is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Alphanumeric { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || property.All(x => char.IsLetterOrDigit(x));
            }
            catch { }
            return false;
        };

        /// <summary>
        /// The default assigned string validator will check to see if a string contains alpha, numeric, and symbol characters
        /// and return true that the property validates if it is either null (empty) or conversion is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Password { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || (property.Length > 7
                    && property.Any(x => char.IsLetter(x))
                    && property.Any(x => char.IsDigit(x))
                    && property.Any(x => char.IsSymbol(x)));
            }
            catch { }
            return false;
        };
        /// <summary>
        /// The default assigned string validator will check to see if a string contains only lowercase characters
        /// and return true that the property validates if it is either null (empty) or conversion is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Lowercase { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || property.All(x => char.IsLower(x));
            }
            catch { }
            return false;
        };

        /// <summary>
        /// The default assigned string validator will check to see if a string contains only uppercase characters
        /// and return true that the property validates if it is either null (empty) or conversion is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Uppercase { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || property.All(x => char.IsUpper(x));
            }
            catch { }
            return false;
        };

        /// <summary>
        /// The default assigned string validator will check to see if a string contains only numeric characters
        /// and return true that the property validates if it is either null (empty) or conversion is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Numeric { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || property.All(x => char.IsNumber(x));
            }
            catch { }
            return false;
        };

        /// <summary>
        /// The default assigned string validator will check to see if a string contains only symbol characters
        /// and return true that the property validates if it is either null (empty) or conversion is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Symbol { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || property.All(x => char.IsSymbol(x));
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will attempt to convert the property value from a Base64 string into an array
        /// and return true that the property validates if it is either null (empty) or conversion is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Base64 { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || Convert.FromBase64String(property.Split(',').LastOrDefault()) != null;
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will attempt to convert the property value from a color name and return true
        /// that the property validates if it is either null (empty) or conversion is successful. You can override this method
        /// with your own function(string, bool) and parser using such as the "ColorTranslator.FromHtml" method from package
        /// System.Drawing.Common to invoke a more complex validation method where required when validation is requested.
        /// </summary>
        public Func<string, bool> Color { get; set; } = (property) =>
        {
            try
            {
                if (string.IsNullOrEmpty(property) || property.Equals("black", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (System.Drawing.Color.FromName(property) != System.Drawing.Color.Black)
                {
                    return true;
                }
                // ColorTranslator.FromHtml
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will run a basic Regex check on the property value for commas and quotes and
        /// will return true that the property validates if it is either null (empty) or the basic check is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested. The default Regex may have performance issues and may be better replaces with your own tokenization
        /// or 3rd party library validation method.
        /// </summary>
        public Func<string, bool> Csv { get; set; } = (property) =>
        {
            try
            {
                var csv = new Regex(
                    "^\\s*(?:'[^'\\\\]*(?:\\\\[\\S\\s][^'\\\\]*)*'|\"[^\"\\\\]*(?:\\\\[\\S\\s][^\"\\\\]*)*\"|[^,'\"\\s\\\\]*(?:\\s+[^,'\"\\s\\\\]+)*)\\s*(?:,\\s*(?:'[^'\\\\]*(?:\\\\[\\S\\s][^'\\\\]*)*'|\"[^\"\\\\]*(?:\\\\[\\S\\s][^\"\\\\]*)*\"|[^,'\"\\s\\\\]*(?:\\s+[^,'\"\\s\\\\]+)*)\\s*)*$"
                , RegexOptions.Compiled);
                return string.IsNullOrEmpty(property) || csv.IsMatch(property.Replace("\r", "").Replace("\n", ""));
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will attempt to parse the string value using the DateTime.TryParse method with
        /// no formats or styles overrides used and return true if property is null (empty) or the parse is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Date { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || System.DateTime.TryParse($"{property}T00:00:00Z", out _);
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will attempt to parse the string value using the DateTime.TryParse method with
        /// no formats or styles overrides used and return true if property is null (empty) or the parse is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> DateTime { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || System.DateTime.TryParse(property, out _);
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will attempt to parse the string value using the DateTime.TryParse method with
        /// no formats or styles overrides used and return true if property is null (empty) or the parse is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Time { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || System.DateTime.TryParse($"0001-01-01T{property}", out _);
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will attempt to parse the string value using the TimeSpan.TryParse method with
        /// no formats or styles overrides used and return true if property is null (empty) or the parse is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        public Func<string, bool> Duration { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || TimeSpan.TryParse(property, out _);
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will attempt to initialize the property into a new MailAddress object type
        /// and return true that the property validates if it is either null (empty) or initialization is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Email { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || new MailAddress(property) != default;
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator will attempt to initialize a new Uri object type using the property value and
        /// return true that the property validates if it is either null (empty) or initialization is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Hostname { get; set; } = (property) =>
        {
            return string.IsNullOrEmpty(property) || System.Uri.TryCreate($"http://{property}", UriKind.Absolute, out _);
        };


        /// <summary>
        /// The default assigned string validator will run a basic check to ensure that the property value starts with a tag opener
        /// and ends with a tag closer, and return true that the property validates if it is either null (empty) or the basic check
        /// is successful. You can override this method with your own function(string, bool) to invoke a more complex validation
        /// method when validation is requested.
        /// </summary>
        public Func<string, bool> Html { get; set; } = (property) =>
        {
            return string.IsNullOrEmpty(property) || property.StartsWith('<') && property.EndsWith('>');
        };


        /// <summary>
        /// The default assigned string validator will attempt to split the property value into an array of 4 segments and
        /// validate that each segment is a numeric value less than 256, and return true that the property validates if it is
        /// either null (empty) or validation is successful. You can override this method with your own function(string, bool)
        /// to invoke a more complex validation method when validation is requested.
        /// </summary>
        public Func<string, bool> IPV4 { get; set; } = (property) =>
        {
            if (string.IsNullOrEmpty(property))
            {
                return true;
            }

            var split = property.Split('.');
            return split.Length == 4 && IPAddress.TryParse(property, out _);
        };


        /// <summary>
        /// The default assigned string validator will attempt to split the property value into an array of 4 segments and
        /// validate that each segment is a numeric value less than 256, and return true that the property validates if it is
        /// either null (empty) or validation is successful. You can override this method with your own function(string, bool)
        /// to invoke a more complex validation method when validation is requested.
        /// </summary>
        public Func<string, bool> IPV6 { get; set; } = (property) =>
        {
            if (string.IsNullOrEmpty(property))
            {
                return true;
            }

            var split = property.Split(':');
            return split.Length > 2 && IPAddress.TryParse(property, out _);
        };


        /// <summary>
        /// The default assigned string validator will run a basic check to ensure that the property value starts with a tag opener
        /// of either "{" or "]" and ends with a tag closer of either "}" or "]", and return true that the property validates if it
        /// is either null (empty) or the basic check is successful. You can override this method with your own function(string, bool)
        /// to invoke a more complex validation method when validation is requested.
        /// </summary>
        public Func<string, bool> Json { get; set; }


        /// <summary>
        /// The default assigned string validator returns true. You can override this method with your own function(string, bool)
        /// to invoke a more complex validation method when validation is requested.
        /// </summary>
        public Func<string, bool> Multiline { get; set; } = (property) =>
        {
            return true;
        };


        /// <summary>
        /// The default assigned string validator uses the same validation method as the Base64 method. This only validates that
        /// the value is null (empty) or a Base64 encoded string, and does not validate that it is a PNG image. You can override
        /// this method with your own function(string, bool) to invoke a more complex validation method when validation is
        /// requested.
        /// </summary>
        public Func<string, bool> PngImageBase64 { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || Convert.FromBase64String(property.Split(',').LastOrDefault()) != null;
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator uses the same validation method as the Base64 method. This only validates that
        /// the value is null (empty) or a Base64 encoded string, and does not validate that it is a PNG image. You can override
        /// this method with your own function(string, bool) to invoke a more complex validation method when validation is
        /// requested. Note that <see cref="StringFormat.FileBytes"/> may be defined on byte array types to inform the formatter
        /// to render a file upload option instead of a byte array and in this case, this validation method will not trigger if
        /// the property type is not of type <see cref="string"/>.
        /// </summary>
        public Func<string, bool> FileBytes { get; set; } = (property) =>
        {
            try
            {
                return string.IsNullOrEmpty(property) || Convert.FromBase64String(property.Split(',').LastOrDefault()) != null;
            }
            catch { }
            return false;
        };


        /// <summary>
        /// The default assigned string validator returns true. You can override this method with your own function(string, bool)
        /// to invoke a more complex validation method when validation is requested.
        /// </summary>
        public Func<string, bool> RichText { get; set; } = (property) =>
        {
            return true;
        };


        /// <summary>
        /// The default assigned string validator will attempt to initialize a new Uri object type using the property value and
        /// return true that the property validates if it is either null (empty) or initialization is successful. You can
        /// override this method with your own function(string, bool) to invoke a more complex validation method when validation
        /// is requested.
        /// </summary>
        public Func<string, bool> Uri { get; set; } = (property) =>
        {
            return string.IsNullOrEmpty(property) || System.Uri.TryCreate(property, UriKind.RelativeOrAbsolute, out _);
        };


        /// <summary>
        /// The default assigned string validator will run a basic check to ensure that the property value starts with a tag opener
        /// and ends with a tag closer, and return true that the property validates if it is either null (empty) or the basic check
        /// is successful. You can override this method with your own function(string, bool) to invoke a more complex validation
        /// method when validation is requested.
        /// </summary>
        public Func<string, bool> Xml { get; set; } = (property) =>
        {
            return string.IsNullOrEmpty(property) || property.StartsWith('<') && property.EndsWith('>');
        };
    }
}
