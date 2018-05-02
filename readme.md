# Infusio. 
**A Functional Infusionsoft REST Api Client**

Infusio is a new .net Infusionsoft client.

The majority of the code is generated from the [official Infusionsoft API documentation](https://developer.infusionsoft.com/docs/rest/infusion.json).

Benefits:

- All operations are coposable. This means that you can chain all operations together to form one 'master' operation using LINQ syntax. [LINQ Query](https://github.com/trbngr/infusio/blob/dev/src/Demo/CustomOperations.cs#L12). [LINQ Methods](https://github.com/trbngr/infusio/blob/dev/src/Demo/CustomOperations.cs#L35).
- All operations are completely lazy. Nothing actually executes until you call `RunWith`. [Example](https://github.com/trbngr/infusio/blob/dev/src/Demo/Program.cs#L84).
- For easy debugging, all operations have the ability to output logs. 
- Speaking of logging, you can create your own log messages. `Log` is an `InfusioOp`. [Example](https://github.com/trbngr/infusio/blob/dev/src/Demo/CustomOperations.cs#L13).
- All operations have XML documentation that comes from the API documentation.
- All optional operation parameters are actually typed as optional (ie `Operation(long? id = default)`) so you don't have to provide a value.
- All data models, and their properties, are immutable with `Copy` methods to change values. Makes for a nice API and thread-safe code. [Example](https://github.com/trbngr/infusio/blob/dev/src/Demo/CustomOperations.cs#L17).
- Authentication is configured up-front and not littered throughout the calls. I still need to make some authentication helpers around token management. But that's a pretty easy task.

Tasks to complete:

- [ ] Library Documentation
- [ ] Authentication token helper packages. These will allow auto-refreshing of tokens so you don't have to worry about it.
- [ ] Create a test kit for easy unit testing.

Please create issues if you run into any problems or just want a feature.

## Installation
`dotnet add package infusio`

or

`Install-Package infusio`

or

`paket add infusio`

## Usage
See the [demo project](src/Demo/Program.cs#L25)
