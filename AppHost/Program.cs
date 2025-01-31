var builder = DistributedApplication.CreateBuilder(args);

// Adds Azure Storage to the builder and configures it to run as an emulator.
var storage = builder.AddAzureStorage("mystorage").RunAsEmulator();

// Adds a queue to the Azure Storage.
var queue = storage.AddQueues("queues");

// Adds an Azure Functions project to the builder with references and dependencies.
builder.AddAzureFunctionsProject<Projects.QueueFunctionApp>("queuefunctionapp")
    .WithReference(queue)
    .WaitFor(queue)
    .WithHostStorage(storage)
    .WaitFor(storage);

// Builds and runs the distributed application.
builder.Build().Run();