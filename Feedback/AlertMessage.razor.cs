using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace Ramboe.Blazor.UserFeedback.Feedback;

public partial class AlertMessage
{
    [Parameter]
    public string Text { get; set; }

}