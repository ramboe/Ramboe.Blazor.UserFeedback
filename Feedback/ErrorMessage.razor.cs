using Microsoft.AspNetCore.Components;
using Ramboe.Blazor.UserFeedback.Models;

namespace Ramboe.Blazor.UserFeedback.Feedback;

public partial class ErrorMessage
{
    [Parameter] public string Text { get; set; }

    [Parameter] public BlazorExceptionModel Status { get; set; }
}
