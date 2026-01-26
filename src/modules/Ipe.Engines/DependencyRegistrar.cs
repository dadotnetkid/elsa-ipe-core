using Elsa.Features.Services;
using Elsa.Workflows.Management.Features;
using Ipe.Engines.Activities;

namespace Ipe.Engines;

public static class DependencyRegistrar
{
    public static IModule AddIPEModule(this IModule module)
    {
        module.Configure<WorkflowManagementFeature>(management =>
        {
            management.AddActivity<ImageReviewNoHit>();
        });

        return module;
    }
}