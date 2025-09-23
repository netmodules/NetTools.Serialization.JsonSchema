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
                    //{
                    //    typeof(TestClass),
                    //    new JsonSchemaAttributeGroup(
                    //        new JsonSchemaType(BasicType.String),
                    //        new JsonSchemaMinMaxLength(0, 20)
                    //    )
                    //},
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

            dummySchema = jsonSchema.FromType<DummyEvent>(10);
            dummySchemaString = Json.ToJson(dummySchema).BeautifyJson();

            var dummyEvent = new DummyEvent()
            {
                // Required
                Meta = new Dictionary<string, object>(),
            };

            // Should be false as required fields aren't present...
            var validates = jsonSchema.ValidateSchema(dummyEvent.ToDictionary(), dummySchema, out var details);

            // Required minimum of 1 item...
            dummyEvent.TestArrayOfStrings = new List<string>()
            {
                "Hello World!"
            };

            // Should be false as required fields are present but testInt1 is less than the minimum value of 1...
            validates = jsonSchema.ValidateSchema(dummyEvent.ToDictionary(), dummySchema, out details);

            dummyEvent.TestInt1 = 1;
            
            // Should be false as required fields are present but testInt2 is less than the minimum value of 1000...
            validates = jsonSchema.ValidateSchema(dummyEvent.ToDictionary(), dummySchema, out details);

            dummyEvent.TestInt2 = 1000;

            // Should be valid...
            validates = jsonSchema.ValidateSchema(dummyEvent.ToDictionary(), dummySchema, out details);

            dummyEvent.TestString = "d";

            // Should be invalid as testString does not meet the minLength requirement of 2...
            validates = jsonSchema.ValidateSchema(dummyEvent.ToDictionary(), dummySchema, out details);

            dummyEvent.TestString = "do";

            var dummyDictionary = dummyEvent.ToDictionary();
            
            // Should be valid...
            validates = jsonSchema.ValidateSchema(dummyDictionary, dummySchema, out details);

            dummyDictionary["testEnum"] = "blahblah";
            validates = jsonSchema.ValidateSchema(dummyDictionary, dummySchema, out details);

            var testByteArrayValidation = new Dictionary<string, object>()
            {
                {
                    "bytes",
                    new int[]
                    {
                        256,
                        257,
                        258
                    }
                }
            };

            // Should be invalid as 256 is not a valid byte value...
            validates = jsonSchema.ValidateSchema<TestByteArrayValidation>(testByteArrayValidation, out details);

            var testByteArrayValidation2 = new Dictionary<string, object>()
            {
                {
                    "bytes",
                    new int[]
                    {
                        0,
                        1,
                        255
                    }
                }
            };

            // Should be valid as 255 is a valid byte value...
            validates = jsonSchema.ValidateSchema<TestByteArrayValidation>(testByteArrayValidation2, out details);

            System.Diagnostics.Debugger.Break();
        }
    }

    public class TestByteArrayValidation
    {
        public byte[] Bytes { get; set; }
    }
}
