NetTools.Serialization.JsonSchema is a simple, robust, stand-alone, zero-dependency implementation of automatic JSON Schema generation from .Net objects and types.

For official JSON Schema draft documentation and specification, see [http://json-schema.org/](http://json-schema.org/)

This .NET Standard 2.1 class library will generate JSON schema from any .Net object type and requires no configuration, yet powerful attributes are available for decorating your class-level types and their properties to build detailed JSON schema perfect for developer, and usage documentation.

A generated JSON Schema object returned from invocation of the JsonSchema.FromType<T>() method will create and return a .NET Dictionary object containing the generated schema fields, you can simly serialize this dictionary to a string using your favorite flavor JSON serializer to get the final schema output.

A built in, extendable JSON Schema validation functionality is also available in this library, and, while closely following JSON Schema draft, many additions that can help with additional type validation for .NET, along with rendering aids to build layout elements for a web UI using tools such as the awesome [JSON Form](https://github.com/jsonform/jsonform)

Full documentation (and a better ReadMe) coming soon...
