using Elsa.Abstractions;
using Elsa.Workflows.Management;
using Elsa.Workflows.Management.Filters;
using JetBrains.Annotations;

namespace Ipe.Engines.Endpoints.GetWorkflowInstance;

[UsedImplicitly]
internal class Endpoint(IWorkflowInstanceStore store) : ElsaEndpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/ipe/workflow-instances/{instanceId}");
        ConfigurePermissions("read:workflow-instances");
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var filter = new WorkflowInstanceFilter { Id = request.InstanceId };
        var workflowInstance = await store.FindAsync(filter, cancellationToken);

        if (workflowInstance == null)
        {
            await Send.NotFoundAsync(cancellationToken);
            return;
        }

        var response = new Response
        {
            Id = workflowInstance.Id,
            DefinitionId = workflowInstance.DefinitionId,
            DefinitionVersionId = workflowInstance.DefinitionVersionId,
            Version = workflowInstance.Version,
            ParentWorkflowInstanceId = workflowInstance.ParentWorkflowInstanceId,
            WorkflowState = workflowInstance.WorkflowState,
            Status = workflowInstance.Status,
            SubStatus = workflowInstance.SubStatus,
            IsExecuting = workflowInstance.IsExecuting,
            CorrelationId = workflowInstance.CorrelationId,
            Name = workflowInstance.Name,
            IncidentCount = workflowInstance.IncidentCount,
            IsSystem = workflowInstance.IsSystem,
            CreatedAt = workflowInstance.CreatedAt,
            UpdatedAt = workflowInstance.UpdatedAt,
            FinishedAt = workflowInstance.FinishedAt
        };

        await Send.OkAsync(response, cancellationToken);
    }
}
