using System;
using System.Collections.Generic;

namespace reblGreen.Serialization.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonSchema.GenerateJsonSchemaFromObject<DummyEvent>();
            Console.WriteLine("Hello World!");
        }
    }
}
