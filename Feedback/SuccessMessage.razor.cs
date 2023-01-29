using Microsoft.AspNetCore.Components;

namespace Ramboe.Blazor.UserFeedback.Feedback;

public partial class SuccessMessage
{
    [Parameter]
    public string Text { get; set; }
}