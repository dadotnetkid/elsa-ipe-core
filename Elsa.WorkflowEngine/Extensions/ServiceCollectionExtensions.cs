using Elsa.WorkflowEngine.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.WorkflowEngine.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkflowEngineClient(this IServiceCollection services, Action<WorkflowEngineClientOptions> configure)
    {
        var options = new WorkflowEngineClientOptions();
        configure(options);

        services.AddHttpClient<IWorkflowEngineClient, WorkflowEngineClient>(client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
            client.DefaultRequestHeaders.Add("X-Api-Key", options.ApiKey);
        });

        return services;
    }
}
