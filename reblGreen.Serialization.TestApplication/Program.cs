using System;
using System.Collections.Generic;

namespace reblGreen.Serialization.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonSchema = new JsonSchema(new Uri("https://reblgreen.com/json-schema/"), true);

            //var stringSchema = jsonSchema.GenerateJsonSchemaFromObject<string>();
            //var stringSchemaString = Json.ToJson(stringSchema).BeautifyJson();

            var dummySchema = jsonSchema.GenerateJsonSchemaFromObject<DummyEvent>();
            var dummySchemaString = Json.ToJson(dummySchema).BeautifyJson();

            var settingSchema = jsonSchema.GenerateJsonSchemaFromObject<reblGreen.NetCore.Modules.Events.GetSettingEvent>();
            var settingSchemaString = Json.ToJson(settingSchema).BeautifyJson();

            Console.WriteLine(dummySchemaString);
        }
    }
}
