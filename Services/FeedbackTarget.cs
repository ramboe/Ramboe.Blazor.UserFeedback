using System.Text.Json;
using Ramboe.Blazor.UserFeedback.Models;

namespace Ramboe.Blazor.UserFeedback.Services;

public class FeedbackTarget
{
    public FeedbackModel FeedbackModel { get; set; }

    public FeedbackTarget()
    {
        FeedbackModel = new FeedbackModel();
        TurnSpinOffAndDisplayContent();
    }


    void TurnAllMessagesOff()
    {
        FeedbackModel.ShowSuccessMessage = false;
        FeedbackModel.ShowErrorMessage = false;
        FeedbackModel.ShowAlertMessage = false;
    }

    public void DisplaySuccess(string Message, bool hideContent = false)
    {
        TurnSpinOffAndDisplayContent();

        FeedbackModel.ShowSuccessMessage = true;
        FeedbackModel.SuccessStatusText = Message;

        FeedbackModel.ShowErrorMessage = false;
        FeedbackModel.ShowAlertMessage = false;

        FeedbackModel.ShowContent = !hideContent;
    }

    public void DisplayAlert(string Message)
    {
        TurnSpinOffAndDisplayContent();

        FeedbackModel.ShowAlertMessage = true;
        FeedbackModel.AlertStatusText = Message;
    }

    public void HideContent()
    {
        FeedbackModel.ShowContent = false;
    }

    public void HideAlert()
    {
        TurnSpinOffAndDisplayContent();

        FeedbackModel.ShowAlertMessage = false;
    }

    public void HideSuccess()
    {
        TurnSpinOffAndDisplayContent();

        FeedbackModel.ShowSuccessMessage = false;
    }

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

    void setFeedbackUpForError(BlazorExceptionModel? message)
    {
        FeedbackModel.ShowContent = false;
        FeedbackModel.ShowSpinner = false;
        FeedbackModel.ShowErrorMessage = true;
        FeedbackModel.Error = message;
    }

    public void DisplayErrorWithContent(string messageAsString)
    {
        var message = new BlazorExceptionModel
        {
            Inner = messageAsString,
            Reference = "blazor"
        };

        SetupForErrorButDisplayContent(message);
    }

    void SetupForErrorButDisplayContent(BlazorExceptionModel message)
    {
        FeedbackModel.ShowContent = true;
        FeedbackModel.ShowSpinner = false;
        FeedbackModel.ShowErrorMessage = true;
        FeedbackModel.Error = message;
    }

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

    public void TurnSpinOn()
    {
        FeedbackModel.LoadingMessage = string.Empty;

        TurnFeedbackOn();
    }

    public void TurnSpinOn(string loadingMessage)
    {
        if (!string.IsNullOrEmpty(loadingMessage))
        {
            FeedbackModel.LoadingMessage = loadingMessage;
        }

        TurnFeedbackOn();
    }

    void TurnFeedbackOn()
    {
        FeedbackModel.ShowSuccessMessage = false;
        FeedbackModel.ShowContent = false;
        FeedbackModel.ShowSpinner = true;
    }


    /// <summary>
    /// Displays a message to the user while loading content
    /// </summary>
    /// <param name="loadingMessage"></param>
    public void TurnSpinOnWithLoadingMessage(string loadingMessage)
    {
        TurnSpinOn(loadingMessage);
    }

    public void TurnSpinOffAndDisplayContent()
    {
        FeedbackModel.ShowContent = true;
        FeedbackModel.ShowSpinner = false;
    }

    #region utility
    void turnSpinOnInternal()
    {
        TurnAllMessagesOff();

        FeedbackModel.ShowSpinner = true;
    }
    #endregion
}