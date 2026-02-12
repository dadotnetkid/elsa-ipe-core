using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;

namespace Ipe.Engines.Activities;

[Activity("IPE", "Image Review", "IPE Image Review No hit", DisplayName = "IPE Image Review No hit", Kind = ActivityKind.Task)]
[Output(IsSerializable = false)]
public class ImageReviewNoHit : Activity
{
    [Input] public string AdmConnectionString { get; set; }
    [Input] public string IpeConnectionString { get; set; }
    
    protected override void Execute(ActivityExecutionContext context)
    {
        var bookmark = new CreateBookmarkArgs()
        {
            Callback = OnResume
        };
        
        context.AddBookmark(new Bookmark()
        {
            
        });

        //create taskspending insert context.id
        base.Execute(context);
    }

    private ValueTask OnResume(ActivityExecutionContext context)
    {
        throw new NotImplementedException();
    }
}