using Elsa.Abstractions;
using Elsa.Common.Models;
using Elsa.Workflows;
using Elsa.Workflows.Management;
using Elsa.Workflows.Runtime;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Requests;
using JetBrains.Annotations;

namespace Ipe.Engines.Endpoints.StartWorkflow;

[UsedImplicitly]
internal class Endpoint(
    IWorkflowDefinitionService workflowDefinitionService,
    IWorkflowDispatcher workflowDispatcher,
    IIdentityGenerator identityGenerator) : ElsaEndpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/ipe/workflows/{definitionId}/start");
        ConfigurePermissions("exec:workflow-definitions");
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var definitionId = request.DefinitionId;
        var versionOptions = VersionOptions.Published;
        var workflowGraph = await workflowDefinitionService.FindWorkflowGraphAsync(definitionId, versionOptions, cancellationToken);

        if (workflowGraph == null)
        {
            await Send.NotFoundAsync(cancellationToken);
            return;
        }

        var input = new Dictionary<string, object>
        {
            ["IpeConnectionString"] = request.IpeConnectionString,
            ["AdmConnectionString"] = request.AdmConnectionString
        };

        var instanceId = request.InstanceId ?? identityGenerator.GenerateId();
        var dispatchRequest = new DispatchWorkflowDefinitionRequest(workflowGraph.Workflow.Identity.Id)
        {
            Input = input,
            InstanceId = instanceId,
            CorrelationId = request.CorrelationId
        };

        var result = await workflowDispatcher.DispatchAsync(dispatchRequest, cancellationToken: cancellationToken);

        if (!result.Succeeded)
        {
            var fault = result.Fault!;
            AddError(fault.Message, fault.Code);
            await Send.ErrorsAsync(cancellation: cancellationToken);
            return;
        }

        await Send.OkAsync(new Response(instanceId), cancellationToken);
    }
}
