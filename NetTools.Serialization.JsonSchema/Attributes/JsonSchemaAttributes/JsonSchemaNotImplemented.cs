﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools.Serialization.JsonSchemaAttributes.Internal;

namespace NetTools.Serialization.JsonSchemaAttributes
{
    /// <summary>
    /// Valid on any type: A custom attribute added by NetTools.Serialization.JsonSchema to inform the json-schema generator that the property is
    /// not used or its functionality has not yet been implemented and the property should be excluded from the json schema.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JsonSchemaNotImplemented : Attribute
    {
        public JsonSchemaNotImplemented()
        {
            throw new NotImplementedException("JsonSchemaNotImplemented is currently not supported.");
        }
    }
}
