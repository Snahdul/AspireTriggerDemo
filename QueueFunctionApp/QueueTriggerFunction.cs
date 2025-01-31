using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace QueueFunctionApp;

/// <summary>
/// Azure Function to process messages from an Azure Queue.
/// </summary>
/// <remarks>
/// When you see the error message "Message has reached MaxDequeueCount of 5. Moving message to queue 'hami-queue-poison'." in logs,
/// maybe you have to explicitly set "messageEncoding": "none" in the host.json file to resolve a message decoding error with Azure Queue storage bindings.
/// </remarks>
public class QueueTriggerFunction
{
    private readonly ILogger<QueueTriggerFunction> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueueTriggerFunction"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public QueueTriggerFunction(ILogger<QueueTriggerFunction> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Function triggered by messages in the specified Azure Queue.
    /// </summary>
    /// <param name="queueMessage">The message from the queue.</param>
    [Function(nameof(QueueTriggerFunction))]
    public void Run([QueueTrigger("hami-queue")] QueueMessage queueMessage)
    {
        if (queueMessage.MessageText is null)
        {
            _logger.LogInformation("C# Queue trigger fired ");
        }
        else
        {
            _logger.LogInformation("C# Queue trigger fired " + queueMessage.MessageText);
        }
    }
}