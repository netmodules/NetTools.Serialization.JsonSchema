# NetTools.Serialization.JsonSchema

**NetTools.Serialization.JsonSchema** is a simple yet robust .NET Standard 2.1 class library for automatic JSON Schema generation from .NET objects and types. With zero dependencies and a lightweight design, this library offers an easy-to-use solution for developers needing seamless JSON Schema generation and validation.

For more information on the official JSON Schema draft documentation and specification, visit [json-schema.org](http://json-schema.org/).

## Features

- **Zero Configuration:** Automatically generate JSON Schema from any .NET object or type without additional setup.
- **Attribute-Driven Customization:** Decorate your class-level types and properties with attributes to build detailed JSON Schemas.
- **JSON Schema Validation:** Built-in validation functionality, extendable for more specific use cases, closely aligned with the JSON Schema draft.
- **Rendering Enhancements:** Includes additions to assist with type validation for .NET and tools to generate layout elements for web UIs like [JSON Form](https://github.com/jsonform/jsonform).
- **Flexible Output:** Returns a .NET `Dictionary` object, which can be serialized into a JSON string using your preferred JSON serializer.

## Getting Started

### Installation

To include **NetTools.Serialization.JsonSchema** in your project, use NuGet Package Manager:

```bash
Install-Package NetTools.Serialization.JsonSchema
```

### Quick Examples

#### Generate JSON Schema
Here's a simple example to generate a JSON Schema for a .NET object:
```csharp
using NetTools.Serialization.JsonSchema;

// A sample class.
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsEmployed { get; set; }
}

// Generate JSON Schema.
var schema = JsonSchema.FromType<Person>();

// Serialize the schema dictionary to JSON string.
string jsonSchema = NetTools.Serialization.Json.ToJson(schema);
Console.WriteLine(jsonSchema);
```
#### Attribute-Driven Schema Customization
Leverage attributes to customize schema generation:
```csharp
using NetTools.Serialization.JsonSchema;
using NetTools.Serialization.JsonSchemaEnums;
using NetTools.Serialization.JsonSchemaAttributes;

public class Product
{
    [JsonSchemaRequired]
    [JsonSchemaTitle("ID")]
    [JsonSchemaDescription("The unique identifier for a product.")]
    public int Id { get; set; }

    [JsonSchemaRequired]
    [JsonSchemaTitle("Name")]
    [JsonSchemaMinLength(5)]
    [JsonSchemaDescription("The name of the product.")]
    public string Name { get; set; }

    [JsonSchemaRequired]
    [JsonSchemaMinValue(0)]
    [JsonSchemaTitle("Price")]
    [JsonSchemaDescription("The price of the product.")]
    public double Price { get; set; }

    [JsonSchemaTitle("Description")]
    [JsonSchemaMinMaxLength(0, 1000)]
    [JsonSchemaFormat(StringFormat.Multiline)]
    [JsonSchemaDescription("The description of the product.")]
    public string Description { get; set; }
}

// Generate JSON Schema with custom attributes
var schema = JsonSchema.FromType<Product>();
string jsonSchema = NetTools.Serialization.Json.ToJson(schema);
Console.WriteLine(jsonSchema);
```
#### Validate JSON Data Against Schema
You can also validate JSON data against a generated schema:
```csharp
using NetTools.Serialization.JsonSchema;

// Generate the JSON Schema for the Product class above.
var schema = JsonSchema.FromType<Product>();

// Define the object to validate, here we use a dictionary because NetTools.Serialization.JsonSchema,
// but this can be just a valid JSON object string. 
var product = new Dictionary<string, object>
{
	{ "Id", 1 },
	{ "Name", "Example Product" },
	{ "Price", 19.99 }
};

// Validate the object against our JSON schema.
var valid = JsonSchemaValidation.ValidateSchema(product, schema, out var validationDetails);

// Output results
Console.WriteLine(valid ? "Valid Product JSON!" : "Invalid Product JSON!");

if (!valid)
{
	Console.WriteLine($"Validation Errors: {string.Join('\n', validationDetails)}");
}
```

`[JsonSchemaFormat]` attribute validation is also supported and all `NetTools.Serialization.JsonSchemaEnums.StringFormat` types can be validated, either with zero configuration, or by applying and passing your own implementation of `IJsonSchemaStringValidators`.  

## Contributing

We welcome contributions! To get involved:
1. Fork [NetTools.Serialization.JsonSchema](https://github.com/netmodules/NetTools.Serialization.JsonSchema), make improvements, and submit a pull request.
2. Code will be reviewed upon submission.
3. Join discussions via the [issues board](https://github.com/netmodules/NetTools.Serialization.JsonSchema/issues).

## License

NetTools.Serialization.JsonSchema is licensed under the [MIT License](https://tldrlegal.com/license/mit-license), allowing unrestricted use, modification, and distribution. If you use NetTools.Serialization.JsonSchema in your own project, we’d love to hear about your experience, and possibly feature you on our website!

Full documentation coming soon!

[NetModules Foundation](https://netmodules.net/)
