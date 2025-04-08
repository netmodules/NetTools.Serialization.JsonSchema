using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on string: The pattern keyword is used to restrict a string to a particular regular expression.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaPattern : JsonSchemaAttribute
    {
        /// <summary>
        /// Valid on string: The pattern keyword is used to restrict a string to a particular regular expression.
        /// </summary>
        public JsonSchemaPattern(string pattern)
        {
            Pattern = pattern;
        }
    }
}
