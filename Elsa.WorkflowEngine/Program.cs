using Elsa.WorkflowEngine.Extensions;
using Elsa.WorkflowEngine.Models;
using Elsa.WorkflowEngine.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Build configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Setup DI container
var services = new ServiceCollection();

// Configure the workflow engine client from appsettings.json
services.AddWorkflowEngineClient(options =>
{
    options.BaseUrl = configuration["WorkflowEngine:BaseUrl"] ?? "https://localhost:5001";
    options.ApiKey = configuration["WorkflowEngine:ApiKey"] ?? throw new InvalidOperationException("ApiKey is required");
});

var serviceProvider = services.BuildServiceProvider();

// Get the client
var workflowClient = serviceProvider.GetRequiredService<IWorkflowEngineClient>();

// Get connection strings from config
var ipeConnectionString = configuration.GetConnectionString("Ipe") ?? throw new InvalidOperationException("Ipe connection string is required");
var admConnectionString = configuration.GetConnectionString("Adm") ?? throw new InvalidOperationException("Adm connection string is required");

// Example: Start a workflow
var startRequest = new StartWorkflowRequest
{
    DefinitionId = "your-workflow-definition-id",
    IpeConnectionString = ipeConnectionString,
    AdmConnectionString = admConnectionString,
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
