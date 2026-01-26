using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;

namespace Ipe.Engines.Activities;

[Activity("IPE", "Initialize Transactions", "Initialize Transactions", DisplayName = "Initialize Transactions", Kind = ActivityKind.Task)]
[Output(IsSerializable = false)]
public class InitializeTransactions : Activity
{
    [Input] public string AdmConnectionString { get; set; }
    [Input] public string IpeConnectionString { get; set; }

    protected override void Execute(ActivityExecutionContext context)
    {
        context.
        base.Execute(context);
    }
}