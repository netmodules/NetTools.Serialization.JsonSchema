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
    public class JsonSchemaMaxItems : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMaxItems(uint max)
        {
            MaxItems = max;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMaxItems(ulong max)
        {
            MaxItems = max;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMaxItems(ushort max)
        {
            MaxItems = max;
        }

        /// <summary>
        /// Valid on array: By default, there is no constraints on the number of items in an array.
        /// </summary>
        public JsonSchemaMaxItems(byte max)
        {
            MaxItems = max;
        }
    }
}
