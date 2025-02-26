using System;
using System.Collections.Generic;
using System.Text;

namespace NetTools.Serialization.JsonSchemaEnums
{
    /// <summary>
    /// The format keyword allows for basic semantic validation on certain kinds of string values that are commonly used.
    /// This allows values to be constrained beyond what the other tools in JSON Schema, including Regular Expressions can do.
    /// </summary>
    public enum StringFormat
    {
        /// <summary>
        /// The (none) format is a custom format added by NetTools.Serialization.JsonSchema to allow a default if the attribute field is not set
        /// by the declarer.
        /// </summary>
        None = 0,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: // ISO 8601 Duration should be formatted as P[n]Y[n]M[n]DT[n]H[n]M[n]S.
        /// See <see href="https://en.wikipedia.org/wiki/ISO_8601">Durations</see>
        /// </summary>
        Duration,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string should span across multiple lines
        /// and should be displayed in a textarea rather than a text field.
        /// </summary>
        Multiline,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is a Base 64 string representation
        /// of an object. This may allow the formatter to display a file upload system, for example.
        /// </summary>
        Base64,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is a Base 64 string representation
        /// of an object, but hints that a file may be expected. This may allow the formatter to display a file upload option instead of an
        /// inputbox, for example. It depends on implementation, or a fallback of inputbox or text field.
        /// </summary>
        FileBase64,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is a Base 64 string representation
        /// of a PNG image. This may allow the formatter to display a render of the png image instead of the string, for example.
        /// </summary>
        PngImageBase64,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is a Base 64 string representation
        /// of a PNG image, but hints that a file may be expected. This may allow the formatter to display a file upload option instead of an
        /// inputbox, for example. It depends on implementation, or a fallback of inputbox or text field.
        /// </summary>
        FilePngImageBase64,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is a CSV string representation of
        /// an object. This may allow the formatter to display a multi string formatter or container to display or input the comma separated values, for example.
        /// </summary>
        Csv,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is an XML representation of
        /// an object. This may allow the formatter to display an XML formatter or validator view, for example.
        /// </summary>
        Xml,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is an HTML representation of
        /// an object. This may allow the formatter to display an HTML formatter or validator view, for example.
        /// </summary>
        Html,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is a JSON representation of
        /// an object. This may allow the formatter to display a JSON formatter or validator view, for example.
        /// </summary>
        Json,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string is a rich text string representation of
        /// an object. This may allow the formatter to display a rich text formatter or editor to display or input the string value, for example.
        /// </summary>
        RichText,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string represents an HTML Color. This
        /// may allow the formatter to display a color picker view, for example.
        /// </summary>
        Color,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string must only contain alpha
        /// characters (no numbers or symbols).
        /// </summary>
        Alpha,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string must only contain alpha
        /// characters and/or numbers (no symbols).
        /// </summary>
        Alphanumeric,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string must only contain lowercase
        /// characters.
        /// </summary>
        Lowercase,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string must only contain uppercase
        /// characters.
        /// </summary>
        Uppercase,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string must only contain numbers
        /// characters.
        /// </summary>
        Numeric,

        /// <summary>
        /// Custom format added by NetTools.Serialization.JsonSchema: This tells the formatter that this string must only contain symbol
        /// characters (no alpha characters or numbers).
        /// </summary>
        Symbol,

        /// <summary>
        /// Built-in: Date representation, as defined by <see href="http://tools.ietf.org/html/rfc3339"/> RFC 3339, section 5.6.
        /// </summary>
        Date,

        /// <summary>
        /// Built-in: Date representation, as defined by <see href="http://tools.ietf.org/html/rfc3339"/> RFC 3339, section 5.6.
        /// </summary>
        DateTime,

        /// <summary>
        /// Built-in: Date representation, as defined by <see href="http://tools.ietf.org/html/rfc3339"/> RFC 3339, section 5.6.
        /// </summary>
        Time,

        /// <summary>
        /// Built-in: Internet email address, <see href="http://tools.ietf.org/html/rfc5322"/> RFC 5322, section 3.4.1.
        /// </summary>
        Email,

        /// <summary>
        /// Built-in: Internet host name, <see href="http://tools.ietf.org/html/rfc1034"/> RFC 1034, section 3.1.
        /// </summary>
        HostName,

        /// <summary>
        /// Built-in: IPv4 address, according to dotted-quad ABNF syntax as defined in <see href="http://tools.ietf.org/html/rfc2673"/> RFC 2673, section 3.2.
        /// </summary>
        IPv4,

        /// <summary>
        /// Built-in: IPv6 address, as defined in <see href="http://tools.ietf.org/html/rfc2373"/> RFC 2373, section 2.2.
        /// </summary>
        IPv6,

        /// <summary>
        /// Built-in: A universal resource identifier (URI), according to <see href="http://tools.ietf.org/html/rfc3986"/> RFC3986.
        /// </summary>
        Uri
    }
}
