using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaMaxValue : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMaxValue(int max)
        {
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMaxValue(uint max)
        {
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMaxValue(long max)
        {
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMaxValue(ulong max)
        {
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMaxValue(float max)
        {
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMaxValue(double max)
        {
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMaxValue(decimal max)
        {
            Maximum = max;
        }
    }
}
