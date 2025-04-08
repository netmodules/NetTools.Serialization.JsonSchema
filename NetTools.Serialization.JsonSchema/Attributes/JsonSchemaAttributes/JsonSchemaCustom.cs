using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: Allows you to add custom properties and values to any target property or class that will be added to the JsonSchema dictionary. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaCustom : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on any type: Allows you to add custom properties and values to any target property or class that will be added to the JsonSchema dictionary. 
        /// </summary>
        public JsonSchemaCustom(string name, object value)
        {
            if (CustomAttributes == null)
            {
                CustomAttributes = new Dictionary<string, object>();
            }

            CustomAttributes[name] = value is bool ? value.ToString().ToLowerInvariant() : value;
        }
    }
}
