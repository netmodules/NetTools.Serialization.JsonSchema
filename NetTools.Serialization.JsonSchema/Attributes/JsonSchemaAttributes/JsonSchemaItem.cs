using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on array: By default, the elements of the array may be anything at all.
    /// However, it’s often useful to validate the items of the array against some schema as well. This is done using the items
    /// and additionalItems keywords. When items is a single schema, the additionalItems keyword is meaningless, and it should not be used.
    /// You must set this attribute on a collection type such as an array or List{T} or it will be ignored.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaItem : JsonSchemaAttribute
    {
        public JsonSchemaItem(Type type)
        {
            throw new Exception("JsonSchemaItem: Specifying custom array item objects is currently not supported.");
        }
    }
}
