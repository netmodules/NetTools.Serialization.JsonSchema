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
    public class JsonSchemaMaxLength : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMaxLength(uint max)
        {
            MaxLength = max;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMaxLength(ulong max)
        {
            MaxLength = max;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMaxLength(ushort max)
        {
            MaxLength = max;
        }

        /// <summary>
        /// Valid on string: By default, there is no constraints on the length of a string.
        /// </summary>
        public JsonSchemaMaxLength(byte max)
        {
            MaxLength = max;
        }
    }
}
