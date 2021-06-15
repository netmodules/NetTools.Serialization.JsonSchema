using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on array: By default, the elements of the array may be anything at all.
    /// However, it’s often useful to validate the items of the array against some schema as well. This is done using the items
    /// and additionalItems keywords. When items is a single schema, the additionalItems keyword is meaningless, and it should not be used.
    /// You must set this to a an array or it will be ignored.
    /// </summary>
    public class JsonSchemaItem : JsonSchemaAttribute
    {
        public JsonSchemaItem(params object[] attributes)
        {

        }

        public JsonSchemaItem(Type type)
        {

        }
    }
}
