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
    public class JsonSchemaMinMaxValue : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinMaxValue(int min, int max)
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinMaxValue(uint min, uint max)
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinMaxValue(long min, long max)
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinMaxValue(ulong min, ulong max)
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinMaxValue(float min, float max)
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinMaxValue(double min, double max)
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinMaxValue(decimal min, decimal max)
        {
            Minimum = min;
            Maximum = max;
        }
    }
}
