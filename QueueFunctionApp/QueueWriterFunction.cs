using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace QueueFunctionApp;

/// <summary>
/// Azure Function to write messages to an Azure Queue.
/// </summary>
/// <summary>
/// Initializes a new instance of the <see cref="QueueWriterFunction"/> class.
/// </summary>
/// <param name="logger">The logger instance.</param>
/// <param name="queueServiceClient">The queue service client.</param>
public class QueueWriterFunction(ILogger<QueueWriterFunction> logger, QueueServiceClient queueServiceClient)
{
    private readonly QueueClient _queueClient = queueServiceClient.GetQueueClient("hami-queue");

    /// <summary>
    /// HTTP trigger function to write a message to the queue.
    /// </summary>
    /// <param name="req">The HTTP request.</param>
    /// <returns>The result of the function execution.</returns>
    [Function("QueueWriter")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        logger.LogInformation("QueueWriter trigger fired.");

        var messageText = $"The message was sent at {DateTime.Now.ToLongTimeString()}";
        await _queueClient.SendMessageAsync(messageText);

        return new OkObjectResult(messageText);
    }
}