using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; using reblGreen.Serialization.JsonSchemaAttributes.Internal;
using reblGreen.Serialization.JsonSchemaClasses;
using reblGreen.Serialization.JsonSchemaEnums;

namespace reblGreen.Serialization
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class JsonSchemaAttributeGroup : JsonSchemaAttribute
    {
        

        /// <summary>
        /// Allows you to set positional parameters in the constructor before any named parameters. Positional parameters will be used to configure
        /// JsonSchemaAttribute fields based on the parameter types and order. Named parameters will override or configure unidentifiable fields.
        /// </summary>
        /// <param name="attributes"></param>
        public JsonSchemaAttributeGroup(params JsonSchemaAttribute[] attributes)
        {
            if (attributes == null || attributes.Length == 0)
            {
                throw new Exception("The attributes paramerter must contain a minimum of 1 arguments to create a JsonSchemaAttribute group.");
            }

            var schemas = new List<JsonSchemaAttribute>() { this, new JsonSchemaAttribute() };
            schemas.AddRange(attributes);
            JsonSchemaHelpers.MergeJsonSchemaAttributes(schemas.ToArray());
        }
    }
}
