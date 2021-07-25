using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaClasses
{
    internal class ReflectionHelpers
    {
        internal static Dictionary<string, Type> GetSchemaMembers(Type t)
        {
            var attrs = t.GetCustomAttributes(typeof(JsonSchemaAttribute), true).Select(x=> x as JsonSchemaAttribute).ToList();
            var fields = t.GetFields().ToList();
            var props = t.GetProperties().ToList();

            return null;
        }
    }
}
