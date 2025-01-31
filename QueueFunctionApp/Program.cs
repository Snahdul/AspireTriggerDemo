using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Entry point for the Azure Functions application.
var builder = FunctionsApplication.CreateBuilder(args);

// Adds Azure Queue client to the builder.
builder.AddAzureQueueClient("queues");

// Adds an Azure Functions project to the builder with references and dependencies.
builder.ConfigureFunctionsWebApplication();

// Builds and runs the distributed application.
var host = builder.Build();

// Retrieves the QueueServiceClient from the host's services and creates a new queue if the client is available.    
var queueServiceClient = host.Services.GetService<QueueServiceClient>();
if (queueServiceClient != null)
{
    await queueServiceClient.CreateQueueAsync("hami-queue");
}

// Runs the Azure Functions application.
host.Run();
