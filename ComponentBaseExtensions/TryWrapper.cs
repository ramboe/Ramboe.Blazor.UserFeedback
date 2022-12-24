using Microsoft.AspNetCore.Components;
using Ramboe.Blazor.UserFeedback.Services;

namespace Ramboe.Blazor.UserFeedback.ComponentBaseExtensions;

public class TryWrapper : ComponentBase
{
    protected FeedbackTarget DefaultTarget { get; } = new();

    protected void Try(Action methodCall, FeedbackTarget feedbackTarget, bool hideContent = false)
    {
        feedbackTarget.TurnSpinOn();

        StateHasChanged();

        try
        {
            methodCall();

            feedbackTarget.TurnSpinOffAndDisplayContent();
        }
        catch (Exception exc)
        {
            feedbackTarget.DisplayError(exc, null, hideContent);
        }

        StateHasChanged();
    }

    protected async Task TryAsync(Func<Task> asyncCall, FeedbackTarget feedbackTarget, bool hideContent = false)
    {
        feedbackTarget.TurnSpinOn();

        await InvokeAsync(StateHasChanged);

        try
        {
            await asyncCall();

            feedbackTarget.TurnSpinOffAndDisplayContent();
        }
        catch (Exception exc)
        {
            feedbackTarget.DisplayError(exc, null, hideContent);
        }

        await InvokeAsync(StateHasChanged);
    }

    protected async Task TryAsync(Func<Task> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, bool hideContent = false)
    {
        feedbackTarget.TurnSpinOnWithLoadingMessage(loadingMessage);

        await InvokeAsync(StateHasChanged);

        try
        {
            await asyncCall();

            feedbackTarget.TurnSpinOffAndDisplayContent();
        }
        catch (Exception exc)
        {
            feedbackTarget.DisplayError(exc, null, hideContent);
        }

        await InvokeAsync(StateHasChanged);
    }

    protected async Task<T> TryAsync<T>(Func<Task<T>> asyncCall, FeedbackTarget feedbackTarget, bool hideContent = false)
    {
        feedbackTarget.TurnSpinOn();

        await InvokeAsync(StateHasChanged);

        try
        {
            var result = await asyncCall();

            feedbackTarget.TurnSpinOffAndDisplayContent();

            await InvokeAsync(StateHasChanged);

            return result;
        }
        catch (TaskCanceledException e)
        {
            feedbackTarget.TurnSpinOffAndDisplayContent();

            await InvokeAsync(StateHasChanged);
        }
        catch (Exception exc)
        {
            feedbackTarget.DisplayError(exc, null, hideContent);
        }

        return default(T);
    }

    protected async Task<T> TryAsync<T>(Func<Task<T>> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, bool hideContent = false)
    {
        feedbackTarget.TurnSpinOnWithLoadingMessage(loadingMessage);

        await InvokeAsync(StateHasChanged);

        try
        {
            var result = await asyncCall();

            feedbackTarget.TurnSpinOffAndDisplayContent();
            await InvokeAsync(StateHasChanged);

            return result;
        }
        catch (Exception exc)
        {
            feedbackTarget.DisplayError(exc,
                additionalMessage: loadingMessage + "failed",
                hideContent);

            await InvokeAsync(StateHasChanged);

            return default(T);
        }
    }

    protected async Task<T> TryAsync<T>(Func<Task<T>> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, string successMessage, bool hideContent = false)
    {
        feedbackTarget.TurnSpinOnWithLoadingMessage(loadingMessage);

        await InvokeAsync(StateHasChanged);

        try
        {
            var result = await asyncCall();

            feedbackTarget.DisplaySuccess(successMessage);

            await InvokeAsync(StateHasChanged);

            return result;
        }
        catch (Exception exc)
        {
            feedbackTarget.DisplayError(exc,
                additionalMessage: loadingMessage + "failed",
                hideContent);

            await InvokeAsync(StateHasChanged);

            return default(T);
        }
    }
}