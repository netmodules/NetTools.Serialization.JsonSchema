using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using NetTools.Serialization.JsonSchemaAttributes;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaInterfaces
{
    /// <summary>
    /// Provides a set of validators for the <see cref="JsonSchemaFormat">string format</see> attribute. You can use this interface to
    /// define your own string format validators and assign them using <see cref="JsonSchema.JsonSchema(JsonSchemaOptions, IJsonSchemaStringValidators)"/>.
    /// </summary>
    public interface IJsonSchemaStringValidators
    {
        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Alpha">Alpha</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Alpha { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Alphanumeric">Alphanumeric</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Alphanumeric { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Lowercase">Lowercase</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Lowercase { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Uppercase">Uppercase</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Uppercase { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Numeric">Numeric</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Numeric { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Symbol">Symbol</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Symbol { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Base64">Base64</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Base64 { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Color">Color</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Color { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Csv">Csv</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Csv { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.DateTime">DateTime</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Date { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.DateTime">DateTime</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> DateTime { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.DateTime">DateTime</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Time { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Duration">Duration</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Duration { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Email">Email</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Email { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.HostName">Hostname</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Hostname { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Html">Html</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Html { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.IPv4">IPv4</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> IPV4 { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.IPv6">IPv6</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> IPV6 { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Json">John</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Json { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Multiline">Miltiline</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Multiline { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.PngImageBase64">PngImageBase64</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> PngImageBase64 { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.FileBytes">FileBytes</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> FileBytes { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.RichText">RichText</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> RichText { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Uri">Uri</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Uri { get; set; }

        /// <summary>
        /// Provides a validator method for the <see cref="StringFormat.Xml">Xml</see> <see cref="JsonSchemaFormat">string format</see>
        /// </summary>
        public Func<string, bool> Xml { get; set; }
    }
}
