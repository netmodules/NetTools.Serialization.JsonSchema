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
    public class JsonSchemaMinMaxLength : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMinMaxLength(uint min, uint max)
        {
            MinLength = min;
            MaxLength = max;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMinMaxLength(ulong min, ulong max)
        {
            MinLength = min;
            MaxLength = max;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMinMaxLength(ushort min, ushort max)
        {
            MinLength = min;
            MaxLength = max;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMinMaxLength(byte min, byte max)
        {
            MinLength = min;
            MaxLength = max;
        }
    }
}
