using reblGreen.Serialization.JsonSchemaAttributes;
using reblGreen.Serialization.JsonSchemaAttributes.Internal;
using reblGreen.Serialization.JsonSchemaEnums;
using System;
using System.Collections.Generic;

namespace reblGreen.Serialization.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * If you're planning on implementing more JSON Schema specification in this project, here is the documentation:
             * https://json-schema.org/understanding-json-schema/UnderstandingJSONSchema.pdf
            */

            var jsonSchema = new JsonSchema(new JsonSchemaOptions()
            {
                AutoCamelCase = true,
                SchemaRefUrl = new Uri("https://reblgreen.com/json-schema/"),
                SchemaType = JsonSchemaOptions.JsonSchemaType.Nested,
                TypeOverrides = new Dictionary<Type, JsonSchemaAttribute> {
                    {
                        typeof(TestClass),
                        new JsonSchemaAttributeGroup(
                            new JsonSchemaType(BasicType.String),
                            new JsonSchemaMinMaxLength(0, 20)
                        ) 
                    }
                }
            });

            //var stringSchema = jsonSchema.GenerateJsonSchemaFromObject<string>();
            //var stringSchemaString = Json.ToJson(stringSchema).BeautifyJson();

            var dummySchema = jsonSchema.FromType<DummyEvent>(10);
            var dummySchemaString = Json.ToJson(dummySchema).BeautifyJson();

            jsonSchema = new JsonSchema(new JsonSchemaOptions()
            {
                AutoCamelCase = true,
                SchemaRefUrl = new Uri("https://reblgreen.com/json-schema/"),
                SchemaType = JsonSchemaOptions.JsonSchemaType.Shallow
            });

            var settingSchema = jsonSchema.FromType<reblGreen.NetCore.Modules.Events.GetSettingEvent>(10);
            var settingSchemaString = Json.ToJson(settingSchema).BeautifyJson();

            Console.WriteLine(dummySchemaString);
        }
    }
}
