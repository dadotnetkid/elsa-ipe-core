using System.Net.Http.Json;
using Elsa.WorkflowEngine.Models;

namespace Elsa.WorkflowEngine.Services;

public class WorkflowEngineClient : IWorkflowEngineClient
{
    private readonly HttpClient _httpClient;

    public WorkflowEngineClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<StartWorkflowResponse?> StartWorkflowAsync(StartWorkflowRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"ipe/workflows/{request.DefinitionId}/start", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<StartWorkflowResponse>(cancellationToken);
    }

    public async Task<WorkflowInstanceResponse?> GetWorkflowInstanceAsync(string instanceId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"ipe/workflow-instances/{instanceId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<WorkflowInstanceResponse>(cancellationToken);
    }
}
