# .NET Aspire with Azure Functions and Azure Queue Storage

A small **.NET 9 / C# 13** sample demonstrating how to run Azure Functions and Azure Queue Storage together using **.NET Aspire** and a local storage emulator.

The example implements an event-driven flow: an HTTP-triggered function writes a message to a storage queue, and a queue-triggered function processes it.

## Overview

The Aspire AppHost coordinates the local application:

- Starts Azure Storage using an emulator.
- Configures the Azure Storage Queues resource.
- Starts the Azure Functions project with the required storage references and dependencies.

This repository focuses on **local development and integration**, not on deploying a production Azure environment.

## Technologies

- .NET 9 and C# 13
- .NET Aspire 9
- Azure Functions v4 (isolated worker)
- Azure Queue Storage
- Azure Storage emulator (containerized with Docker)

## Message flow

1. `QueueWriterFunction` is invoked through an HTTP `GET` or `POST` request.
2. The function uses `QueueClient` to send a plain-text message to the `hami-queue` queue.
3. `QueueTriggerFunction` receives the queued message and writes its contents to the application log.

## Queue message encoding: a configuration issue

During development, the queue-triggered function encountered message decoding errors. The producer sends plain-text messages through the Azure Storage Queues client, while the Azure Functions queue trigger must be configured to interpret those messages using the same encoding.

The solution in this example is to set `messageEncoding` to `none` in `QueueFunctionApp/host.json`:

```json
{
  "version": "2.0",
  "extensions": {
    "queues": {
      "messageEncoding": "none"
    }
  }
}
```

This explicitly configures the queue trigger to consume the unencoded messages sent by `QueueWriterFunction`. An encoding mismatch can otherwise lead to failed processing attempts and messages being moved to a poison queue.

**Takeaway:** When producing and consuming Azure Queue Storage messages, ensure that the sender and the queue-trigger binding use compatible message encodings.

## Project structure

```text
AspireTriggerDemo/
├── AppHost/
│   ├── AppHost.csproj
│   └── Program.cs
├── QueueFunctionApp/
│   ├── QueueFunctionApp.csproj
│   ├── Program.cs
│   ├── QueueWriterFunction.cs
│   ├── QueueTriggerFunction.cs
│   └── host.json
└── AspireTriggerDemo.sln
```

- **`AppHost/`** — Aspire orchestration, storage emulator, queue resources and Azure Functions integration.
- **`QueueFunctionApp/`** — HTTP-triggered queue producer and queue-triggered message consumer.

## Prerequisites

- **.NET 9 SDK**
- **Docker** for the local storage emulator
- **Visual Studio 2022** or another compatible .NET development environment (optional)

## Running locally

1. Start Docker.
2. Clone this repository.
3. From the repository root, start the Aspire AppHost:

   ```bash
   dotnet run --project AppHost/AppHost.csproj
   ```

4. Use the local Azure Functions HTTP endpoint for `QueueWriter` to send a message.
5. Inspect the function logs to verify that `QueueTriggerFunction` receives and processes the message.

The Aspire dashboard and function endpoint addresses are displayed by the local development environment.

## License

Licensed under the [MIT License](LICENSE.txt).
