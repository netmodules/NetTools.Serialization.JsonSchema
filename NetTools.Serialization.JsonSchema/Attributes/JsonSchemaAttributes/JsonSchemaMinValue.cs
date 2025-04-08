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
    public class JsonSchemaMinValue : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinValue(int min)
        {
            Minimum = min;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinValue(uint min)
        {
            Minimum = min;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinValue(long min)
        {
            Minimum = min;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinValue(ulong min)
        {
            Minimum = min;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinValue(float min)
        {
            Minimum = min;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinValue(double min)
        {
            Minimum = min;
        }

        /// <summary>
        /// Valid on numeric: By default, there is no constraints on the value of a number or integer.
        /// </summary>
        public JsonSchemaMinValue(decimal min)
        {
            Minimum = min;
        }
    }
}
