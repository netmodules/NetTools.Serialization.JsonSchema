using NetTools.Serialization.JsonSchemaAttributes;
using NetTools.Serialization.JsonSchemaAttributes.Internal;
using NetTools.Serialization.JsonSchemaEnums;
using System;
using System.Collections.Generic;

namespace NetTools.Serialization.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * If you're planning on implementing more JSON Schema specification in this project, here is the documentation:
             * https://json-schema.org/understanding-json-schema/UnderstandingJSONSchema.pdf
            */

            // Test shallow schema with only links...
            var jsonSchema = new JsonSchema(new JsonSchemaOptions()
            {
                AutoCamelCase = true,
                SchemaRefUrl = new Uri("https://NetTools.com/json-schema/"),
                SchemaType = JsonSchemaOptions.JsonSchemaType.Shallow,
                TypeOverrides = new Dictionary<Type, JsonSchemaAttribute> {
                    {
                        typeof(TestClass),
                        new JsonSchemaAttributeGroup(
                            new JsonSchemaType(BasicType.String),
                            new JsonSchemaMinMaxLength(0, 20)
                        )
                    },
                    {
                        typeof(TimeSpan),
                        new JsonSchemaAttributeGroup(new JsonSchemaType(BasicType.String),
                        new JsonSchemaFormat(StringFormat.Duration))
                    }
                }
            });

            //var stringSchema = jsonSchema.GenerateJsonSchemaFromObject<string>();
            //var stringSchemaString = Json.ToJson(stringSchema).BeautifyJson();

            var dummySchema = jsonSchema.FromType<DummyEvent>(10);
            var dummySchemaString = Json.ToJson(dummySchema).BeautifyJson();

            // Test nested schemas...
            jsonSchema = new JsonSchema(new JsonSchemaOptions()
            {
                AutoCamelCase = true,
                SchemaRefUrl = new Uri("https://NetTools.com/json-schema/"),
                SchemaType = JsonSchemaOptions.JsonSchemaType.Nested,
                TypeOverrides = new Dictionary<Type, JsonSchemaAttribute> {
                    {
                        typeof(TestClass),
                        new JsonSchemaAttributeGroup(
                            new JsonSchemaType(BasicType.String),
                            new JsonSchemaMinMaxLength(0, 20)
                        )
                    },
                    {
                        typeof(TimeSpan),
                        new JsonSchemaAttributeGroup(new JsonSchemaType(BasicType.String),
                        new JsonSchemaFormat(StringFormat.Duration))
                    },
                    {
                        typeof(DateTime),
                        new JsonSchemaAttributeGroup(new JsonSchemaType(BasicType.String),
                        new JsonSchemaFormat(StringFormat.DateTime))
                    },
                    {
                        typeof(Uri),
                        new JsonSchemaAttributeGroup(new JsonSchemaType(BasicType.String),
                        new JsonSchemaFormat(StringFormat.Uri))
                    }
                }
            });

            var settingSchema = jsonSchema.FromType<NetModules.Events.GetSettingEvent>(10);
            var settingSchemaString = Json.ToJson(settingSchema).BeautifyJson();

            //var openNlpSchema = jsonSchema.FromType<Modules.Nlp.OpenNlp.Events.DateTimeParserEvent>(10);
            //var openNlpSchemaString = Json.ToJson(openNlpSchema).BeautifyJson();

            dummySchema = jsonSchema.FromType<DummyEvent>(10);
            dummySchemaString = Json.ToJson(dummySchema).BeautifyJson();

            var googleSchema = jsonSchema.FromType<Modules.Web.GoogleSearch.Events.GoogleSearchLocalMapsEvent>(10);
            var googleString = googleSchema.ToJson().BeautifyJson();
            Console.WriteLine(dummySchemaString);
        }
    }
}
