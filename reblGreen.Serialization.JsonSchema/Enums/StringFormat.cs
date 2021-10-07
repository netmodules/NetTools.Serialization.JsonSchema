using System;
using System.Collections.Generic;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaEnums
{
    /// <summary>
    /// The format keyword allows for basic semantic validation on certain kinds of string values that are commonly used.
    /// This allows values to be constrained beyond what the other tools in JSON Schema, including Regular Expressions can do.
    /// </summary>
    public enum StringFormat
    {
        /// <summary>
        /// The none format is a custom format added by reblGreen.Serialization.JsonSchemaAttributes to allow a default if the attribute field is not set
        /// by the declarer.
        /// </summary>
        None = 0,

        /// <summary>
        /// Custom format added by reblGreen.Serialization.JsonSchemaAttributes: // ISO 8601 Duration should be formatted as P[n]Y[n]M[n]DT[n]H[n]M[n]S.
        /// See <see href="https://en.wikipedia.org/wiki/ISO_8601">Durations</see>
        /// </summary>
        Duration,

        /// <summary>
        /// Custom format added by reblGreen.Serialization.JsonSchemaAttributes: This tells the formatter that this string should span across multiple lines
        /// and should be displayed in a textarea rather than a text field.
        /// </summary>
        Multiline,


        /// <summary>
        /// Built-in: Date representation, as defined by <see href="http://tools.ietf.org/html/rfc3339"/> RFC 3339, section 5.6.
        /// </summary>
        DateTime,

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
