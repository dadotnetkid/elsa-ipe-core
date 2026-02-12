using Dapper;
using Elsa.Abstractions;
using JetBrains.Annotations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Ipe.Engines.Endpoints.GetExternalInstances;

[PublicAPI]
internal class Endpoint(IConfiguration configuration) : ElsaEndpoint<Request, Response>
{
    private const string DefaultConnectionName = "Adm";

    public override void Configure()
    {
        Get("/ipe/external-instances");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var connectionName = request.ConnectionName ?? DefaultConnectionName;
        var connectionString = configuration.GetConnectionString(connectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            AddError($"Connection string '{connectionName}' not found in configuration.");
            await Send.ErrorsAsync(cancellation: cancellationToken);
            return;
        }

        await using var connection = new SqlConnection(connectionString);
        var instances = await connection.QueryAsync<InstanceRecord>("SELECT * FROM ADM_E_Instances");

        var response = new Response
        {
            Instances = instances.ToList()
        };

        await Send.OkAsync(response, cancellationToken);
    }
}