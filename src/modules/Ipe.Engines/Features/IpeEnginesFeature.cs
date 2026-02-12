using Elsa.Extensions;
using Elsa.Features.Abstractions;
using Elsa.Features.Services;

namespace Ipe.Engines.Features;

/// <summary>
/// Adds IPE Engines API endpoints.
/// </summary>
public class IpeEnginesFeature(IModule module) : FeatureBase(module)
{
    /// <inheritdoc />
    public override void Configure()
    {
        Module.AddFastEndpointsAssembly(GetType());
    }

    /// <inheritdoc />
    public override void Apply()
    {
        // Additional service registrations can go here if needed
    }
}
