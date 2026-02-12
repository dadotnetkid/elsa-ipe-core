namespace Ipe.Engines.Endpoints.GetExternalInstances;

public class Request
{
    /// <summary>
    /// The name of the connection string to use from configuration.
    /// Defaults to "Adm" if not specified.
    /// </summary>
    public string? ConnectionName { get; set; }
}

public class Response
{
    public ICollection<InstanceRecord> Instances { get; set; } = new List<InstanceRecord>();
}

public class InstanceRecord
{
    public int Id { get; set; }
    public string? ConnectionString { get; set; }
    public string? Name { get; set; }
    public string? DataRoot { get; set; }
    public string? ContainerString { get; set; }
    public string? Container { get; set; }
    public string? WorkflowString { get; set; }
    public int Status { get; set; }
    public int Type { get; set; }
    public int CustomerID { get; set; }
}
