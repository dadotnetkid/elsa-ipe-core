using Elsa.Workflows;
using Elsa.Workflows.Attributes;

namespace Ipe.Engines.Activities;

[Activity("IPE", "Image Review", "Supervisor", DisplayName = "Supervisor", Kind = ActivityKind.Task)]
[Output(IsSerializable = false)]
public class SupervisorActivity : Activity
{
    
}