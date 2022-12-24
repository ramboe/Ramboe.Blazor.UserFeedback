using Microsoft.AspNetCore.Components;
using Ramboe.Blazor.UserFeedback.Models;

namespace Ramboe.Blazor.UserFeedback.Feedback;

public partial class ErrorMessage
{
    #region injections
    #endregion

    #region params
    [Parameter] public string StatusText { get; set; }
    [Parameter] public BlazorExceptionModel Status { get; set; }

    [Parameter] public string MaxWith { get; set; }

    [Parameter] public string MarginBottom { get; set; }

    [Parameter] public string MarginTop { get; set; }
    #endregion

    #region props
    string _marginbottom = string.Empty;
    string _margintop = string.Empty;
    #endregion

    protected override void OnInitialized()
    {
        if (!string.IsNullOrEmpty(MarginBottom))
        {
            _marginbottom = $"margin-bottom: {MarginBottom};";
        }

        if (!string.IsNullOrEmpty(MarginTop))
        {
            _margintop = $"margin-bottom: {MarginTop};";
        }
    }
}