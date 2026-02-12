using Elsa.Extensions;
using Elsa.Features.Services;
using Elsa.Workflows.Management.Features;
using Ipe.Engines.Activities;
using Ipe.Engines.Features;

namespace Ipe.Engines;

public static class DependencyRegistrar
{
    public static IModule AddIPEModule(this IModule module)
    {
        module.Configure<WorkflowManagementFeature>(management =>
        {
            management.AddActivity<ImageReviewNoHit>();
            management.AddActivity<SupervisorActivity>();
            management.AddActivity<InitializeTransactions>();
        });

        // Register IPE Engines endpoints
        module.Use<IpeEnginesFeature>();

        return module;
    }
}