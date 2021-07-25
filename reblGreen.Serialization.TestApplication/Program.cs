using System;
using System.Collections.Generic;

namespace reblGreen.Serialization.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonSchema = new JsonSchema(null);
            jsonSchema.GenerateJsonSchemaFromObject<DummyEvent>();
            Console.WriteLine("Hello World!");
        }
    }
}
