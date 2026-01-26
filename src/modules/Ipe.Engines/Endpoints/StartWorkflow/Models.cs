namespace Ipe.Engines.Endpoints.StartWorkflow;

public class Request
{
    public string DefinitionId { get; set; } = default!;
    public string IpeConnectionString { get; set; } = default!;
    public string AdmConnectionString { get; set; } = default!;
    public string? CorrelationId { get; set; }
    public string? InstanceId { get; set; }
}

public record Response(string WorkflowInstanceId);
