using System.Diagnostics;
using System.Text.Json;
using Ramboe.Blazor.UserFeedback.Models;

namespace Ramboe.Blazor.UserFeedback.Services;

public class FeedbackTarget
{
    public FeedbackTarget()
    {
        FeedbackModel = new FeedbackModel();
        TurnSpinOffAndDisplayContent();
    }

    public FeedbackModel FeedbackModel { get; set; }


    private void TurnAllMessagesOff()
    {
        FeedbackModel.ShowSuccessMessage = false;
        FeedbackModel.ShowErrorMessage = false;
        FeedbackModel.ShowAlertMessage = false;
    }

    [DebuggerHidden]
    public void DisplaySuccess(string Message, bool hideContent = false)
    {
        TurnSpinOffAndDisplayContent();

        FeedbackModel.ShowSuccessMessage = true;
        FeedbackModel.SuccessStatusText = Message;

        FeedbackModel.ShowErrorMessage = false;
        FeedbackModel.ShowAlertMessage = false;

        FeedbackModel.ShowContent = !hideContent;
    }

    [DebuggerHidden]
    public void DisplayAlert(string Message)
    {
        TurnSpinOffAndDisplayContent();

        FeedbackModel.ShowAlertMessage = true;
        FeedbackModel.AlertStatusText = Message;
    }

    [DebuggerHidden]
    public void HideContent()
    {
        FeedbackModel.ShowContent = false;
    }

    [DebuggerHidden]
    public void HideAlert()
    {
        TurnSpinOffAndDisplayContent();

        FeedbackModel.ShowAlertMessage = false;
    }

    [DebuggerHidden]
    public void HideSuccess()
    {
        TurnSpinOffAndDisplayContent();

        FeedbackModel.ShowSuccessMessage = false;
    }

    [DebuggerHidden]
    public void DisplayError(Exception exception, string additionalMessage = "", bool hideContent = false)
    {
        var message = new BlazorExceptionModel();

        try
        {
            message = JsonSerializer.Deserialize<BlazorExceptionModel>(exception.Message);

            if (!string.IsNullOrEmpty(additionalMessage))
            {
                message.Inner = additionalMessage + ": " + message.Inner;
            }
        }
        catch (Exception exc)
        {
            message.Inner = exception.Message;
        }

        setFeedbackUpForError(message);

        FeedbackModel.ShowContent = !hideContent;
    }


    [DebuggerHidden]
    public void DisplayError(string messageAsString, bool hideContent = false)
    {
        var message = new BlazorExceptionModel
        {
            Inner = messageAsString,
            Reference = "blazor"
        };

        setFeedbackUpForError(message);

        FeedbackModel.ShowContent = !hideContent;
    }

    private void setFeedbackUpForError(BlazorExceptionModel? message)
    {
        FeedbackModel.ShowContent = false;
        FeedbackModel.ShowSpinner = false;
        FeedbackModel.ShowErrorMessage = true;
        FeedbackModel.Error = message;
    }

    [DebuggerHidden]
    public void DisplayErrorWithContent(string messageAsString)
    {
        var message = new BlazorExceptionModel
        {
            Inner = messageAsString,
            Reference = "blazor"
        };

        SetupForErrorButDisplayContent(message);
    }

    private void SetupForErrorButDisplayContent(BlazorExceptionModel message)
    {
        FeedbackModel.ShowContent = true;
        FeedbackModel.ShowSpinner = false;
        FeedbackModel.ShowErrorMessage = true;
        FeedbackModel.Error = message;
    }

    [DebuggerHidden]
    public void DisplayErrorWithContent(Exception exception, string additionalMessage = "")
    {
        var message = new BlazorExceptionModel();

        try
        {
            message = JsonSerializer.Deserialize<BlazorExceptionModel>(exception.Message);

            if (!string.IsNullOrEmpty(additionalMessage))
            {
                message.Inner = additionalMessage + ": " + message.Inner;
            }
        }
        catch (Exception exc)
        {
        }

        SetupForErrorButDisplayContent(message);
    }

    [DebuggerHidden]
    public void TurnSpinOn()
    {
        FeedbackModel.LoadingMessage = string.Empty;

        TurnFeedbackOn();
    }

    [DebuggerHidden]
    public void TurnSpinOn(string loadingMessage)
    {
        if (!string.IsNullOrEmpty(loadingMessage))
        {
            FeedbackModel.LoadingMessage = loadingMessage;
        }

        TurnFeedbackOn();
    }

    [DebuggerHidden]
    private void TurnFeedbackOn()
    {
        FeedbackModel.ShowSuccessMessage = false;
        FeedbackModel.ShowContent = false;
        FeedbackModel.ShowSpinner = true;
    }


    /// <summary>
    ///     Displays a message to the user while loading content
    /// </summary>
    /// <param name="loadingMessage"></param>
    [DebuggerHidden]
    public void TurnSpinOnWithLoadingMessage(string loadingMessage)
    {
        TurnSpinOn(loadingMessage);
    }

    [DebuggerHidden]
    public void TurnSpinOffAndDisplayContent()
    {
        FeedbackModel.ShowContent = true;
        FeedbackModel.ShowSpinner = false;
    }
}