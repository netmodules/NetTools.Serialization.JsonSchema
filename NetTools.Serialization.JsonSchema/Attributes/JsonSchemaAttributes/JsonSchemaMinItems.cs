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
    public class JsonSchemaMinItems : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMinItems(uint min)
        {
            MinItems = min;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMinItems(ulong min)
        {
            MinItems = min;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMinItems(ushort min)
        {
            MinItems = min;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMinItems(byte min)
        {
            MinItems = min;
        }
    }
}
