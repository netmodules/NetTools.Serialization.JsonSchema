using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reblGreen.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on object: This keyword is used to control the handling of extra stuff, that is, properties whose names are not listed in the
    /// properties keyword. By default any additional properties are allowed. If the keyword is a boolean and set to false, no additional
    /// properties will be allowed.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaName : Attribute
    {
        string Name;
        
        public JsonSchemaName(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
