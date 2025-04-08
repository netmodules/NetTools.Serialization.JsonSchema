using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on string: By default, there is no constraints on the length of a string.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaMinLength : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMinLength(uint min)
        {
            MinLength = min;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMinLength(ulong min)
        {
            MinLength = min;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMinLength(ushort min)
        {
            MinLength = min;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMinLength(byte min)
        {
            MinLength = min;
        }
    }
}
