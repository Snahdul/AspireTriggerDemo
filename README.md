# Distributed Application with Azure Functions and Azure Storage

This project demonstrates a distributed application built using .NET 9 and C# 13.0. It leverages Azure Functions and Azure Storage, configured to run with an emulator for local development.

Just solved an interesting challenge while working with Azure Functions in an Aspire project targeting .NET 9 and C# 13. I discovered that custom hosting environments might not inherit all default configurations, which can lead to unexpected issues. Specifically, I had to explicitly set "messageEncoding": "none" in the host.json file to resolve a message decoding error with Azure Queue storage bindings.

## Project Structure

- **AppHost**: The main application host that configures and runs the distributed application.
- **QueueFunctionApp**: An Azure Functions project that processes messages from an Azure Storage queue.

## Prerequisites

- .NET 9 SDK
- Visual Studio 2022
- Docker
