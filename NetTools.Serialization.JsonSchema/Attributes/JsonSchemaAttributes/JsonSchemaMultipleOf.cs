using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on numeric: By default, there is no constraints on the multiple of a number or integer.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaMultipleOf : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the multiple of a number or integer.
        /// </summary>
        public JsonSchemaMultipleOf(int value)
        {
            MultipleOf = value;
        }
        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the multiple of a number or integer.
        /// </summary>

        public JsonSchemaMultipleOf(uint value)
        {
            MultipleOf = value;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the multiple of a number or integer.
        /// </summary>
        public JsonSchemaMultipleOf(long value)
        {
            MultipleOf = value;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the multiple of a number or integer.
        /// </summary>
        public JsonSchemaMultipleOf(ulong value)
        {
            MultipleOf = value;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the multiple of a number or integer.
        /// </summary>
        public JsonSchemaMultipleOf(float value)
        {
            MultipleOf = value;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the multiple of a number or integer.
        /// </summary>
        public JsonSchemaMultipleOf(double value)
        {
            MultipleOf = value;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the multiple of a number or integer.
        /// </summary>
        public JsonSchemaMultipleOf(decimal value)
        {
            MultipleOf = value;
        }
    }
}
