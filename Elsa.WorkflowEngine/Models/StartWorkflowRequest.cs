namespace Elsa.WorkflowEngine.Models;

public class StartWorkflowRequest
{
    public string DefinitionId { get; set; } = default!;
    public string IpeConnectionString { get; set; } = default!;
    public string AdmConnectionString { get; set; } = default!;
    public string? CorrelationId { get; set; }
    public string? InstanceId { get; set; }
}

public class StartWorkflowResponse
{
    public string WorkflowInstanceId { get; set; } = default!;
}
