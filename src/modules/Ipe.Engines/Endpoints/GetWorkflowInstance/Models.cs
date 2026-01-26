using Elsa.Workflows;
using Elsa.Workflows.State;

namespace Ipe.Engines.Endpoints.GetWorkflowInstance;

public class Request
{
    public string InstanceId { get; set; } = default!;
}

public class Response
{
    public string Id { get; set; } = default!;
    public string DefinitionId { get; set; } = default!;
    public string DefinitionVersionId { get; set; } = default!;
    public int Version { get; set; }
    public string? ParentWorkflowInstanceId { get; set; }
    public WorkflowState WorkflowState { get; set; } = default!;
    public WorkflowStatus Status { get; set; }
    public WorkflowSubStatus SubStatus { get; set; }
    public bool IsExecuting { get; set; }
    public string? CorrelationId { get; set; }
    public string? Name { get; set; }
    public int IncidentCount { get; set; }
    public bool IsSystem { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }
}
