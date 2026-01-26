using Elsa.WorkflowEngine.Models;

namespace Elsa.WorkflowEngine.Services;

public interface IWorkflowEngineClient
{
    Task<StartWorkflowResponse?> StartWorkflowAsync(StartWorkflowRequest request, CancellationToken cancellationToken = default);
    Task<WorkflowInstanceResponse?> GetWorkflowInstanceAsync(string instanceId, CancellationToken cancellationToken = default);
}
