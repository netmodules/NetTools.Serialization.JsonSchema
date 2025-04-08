using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on array: By default, there is no constraints on the number of items in an array.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaMinMaxItems : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMinMaxItems(uint min, uint max)
        {
            MinItems = min;
            MaxItems = max;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMinMaxItems(ulong min, ulong max)
        {
            MinItems = min;
            MaxItems = max;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMinMaxItems(ushort min, ushort max)
        {
            MinItems = min;
            MaxItems = max;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMinMaxItems(byte min, byte max)
        {
            MinItems = min;
            MaxItems = max;
        }
    }
}
