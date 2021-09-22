using System;
using System.Collections.Generic;

namespace reblGreen.Serialization.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonSchema = new JsonSchema(null, true);
            var stringSchema = jsonSchema.GenerateJsonSchemaFromObject<string>();
            var stringSchemaString = Json.ToJson(stringSchema).BeautifyJson();


            var dummySchema = jsonSchema.GenerateJsonSchemaFromObject<DummyEvent>();
            var dummySchemaString = Json.ToJson(dummySchema).BeautifyJson();

            var schema = jsonSchema.GenerateJsonSchemaFromObject<reblGreen.NetCore.Modules.Events.GetSettingEvent>();
            var schemaString = Json.ToJson(schema).BeautifyJson();

            Console.WriteLine("Hello World!");
        }
    }
}
