using Elsa.WorkflowEngine.Extensions;
using Elsa.WorkflowEngine.Models;
using Elsa.WorkflowEngine.Services;
using Microsoft.Extensions.DependencyInjection;

// Setup DI container
var services = new ServiceCollection();

// Configure the workflow engine client with API key
services.AddWorkflowEngineClient(options =>
{
    options.BaseUrl = "https://localhost:5001"; // Your Elsa server URL
    options.ApiKey = "your-api-key-here"; // Your API key
});

var serviceProvider = services.BuildServiceProvider();

// Get the client
var workflowClient = serviceProvider.GetRequiredService<IWorkflowEngineClient>();

// Example: Start a workflow
var startRequest = new StartWorkflowRequest
{
    DefinitionId = "your-workflow-definition-id",
    IpeConnectionString = "Server=172.10.1.10;Database=IPE;User Id=sa;Password=Global@2024!;TrustServerCertificate=True",
    AdmConnectionString = "Server=172.10.1.10;Database=ADM;User Id=sa;Password=Global@2024!;TrustServerCertificate=True",
    CorrelationId = "my-correlation-id" // Optional
};

try
{
    Console.WriteLine("Starting workflow...");
    var startResponse = await workflowClient.StartWorkflowAsync(startRequest);
    Console.WriteLine($"Workflow started! Instance ID: {startResponse?.WorkflowInstanceId}");

    // Example: Get workflow instance status
    if (startResponse != null)
    {
        Console.WriteLine("Getting workflow instance...");
        var instance = await workflowClient.GetWorkflowInstanceAsync(startResponse.WorkflowInstanceId);
        Console.WriteLine($"Workflow Status: {instance?.Status}");
        Console.WriteLine($"Sub-Status: {instance?.SubStatus}");
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Error calling workflow API: {ex.Message}");
}
