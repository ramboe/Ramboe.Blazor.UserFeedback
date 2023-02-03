namespace Ramboe.Blazor.UserFeedback.Models;

/// <summary>
///     Represents the current feedback state
/// </summary>
public class FeedbackModel
{
    public bool ShowContent { get; set; }

    public bool ShowErrorMessage { get; set; }

    public bool ShowAlertMessage { get; set; }

    public bool ShowSpinner { get; set; }

    public bool ShowSuccessMessage { get; set; }

    public string LoadingMessage { get; set; }

    public BlazorExceptionModel Error { get; set; }

    public string SuccessStatusText { get; set; }

    public string AlertStatusText { get; set; }
}
public class BlazorExceptionModel
{
    public string Inner { get; set; }

    public string Reference { get; set; }
}
