using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Ramboe.Blazor.UserFeedback.Services;

namespace Ramboe.Blazor.UserFeedback.ComponentBaseExtensions;

/// <summary>
///     Make sure to inherit from this class in order to use the feedback system.
/// </summary>
public class TryWrapper : ComponentBase
{
    protected FeedbackTarget DefaultTarget { get; } = new();

    /// <summary>
    ///     This method takes in three parameters: a method call, a feedback target, and a boolean value for hiding content. It
    ///     turns on a spinning animation on the feedback target, updates the state, and then attempts to execute the method
    ///     call. If the method call is successful, it turns off the spinning animation and displays the content. If an
    ///     exception is caught, it displays an error message on the feedback target and, if the hideContent boolean is set to
    ///     true, hides the content. The state is updated again before the method ends.
    /// </summary>
    /// <param name="methodCall">action to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    [DebuggerHidden]
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

    /// <summary>
    ///     Async Version of Try. Takes in three parameters: a method call, a feedback target, and a boolean value for hiding
    ///     content. It turns on a spinning animation on the feedback target, updates the state, and then attempts to execute
    ///     the method call. If the method call is successful, it turns off the spinning animation and displays the content. If
    ///     an exception is caught, it displays an error message on the feedback target and, if the hideContent boolean is set
    ///     to true, hides the content. The state is updated again before the method ends.
    /// </summary>
    /// <param name="asyncCall">Task to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    [DebuggerHidden]
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

    /// <summary>
    ///     Method that is used to perform an asynchronous call and handle any errors that may occur. It takes in three
    ///     parameters: an asyncCall (which is a function that returns a task that returns a generic type T), a feedbackTarget
    ///     (which is an object that has methods for handling feedback during the async call), and an optional hideContent
    ///     boolean. The method starts by turning on a "spin" on the feedbackTarget and then calls the asyncCall. If the call
    ///     is successful, it turns off the spin, displays the content, and returns the result. If an exception of type
    ///     TaskCanceledException is caught, the method turns off the spin, displays the content, and returns the default value
    ///     of T. If any other exception is caught, the method calls the DisplayError method on the feedbackTarget and passes
    ///     in the exception and an optional hideContent boolean. The method also calls StateHasChanged twice, once before the
    ///     asyncCall and once after.
    /// </summary>
    /// <param name="asyncCall">Task to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    /// <typeparam name="T">Type of the object that is returned by asyncCall</typeparam>
    /// <returns>result of asyncCall</returns>
    [DebuggerHidden]
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

        return default;
    }

    /// <summary>
    ///     Method to perform an asynchronous call and handle any errors that may occur. It takes in four parameters: an
    ///     asyncCall (which is a function that returns a task), a feedbackTarget (which is an object that has methods for
    ///     handling feedback during the async call), a loadingMessage (a string message to be displayed while loading), and an
    ///     optional hideContent boolean. The method starts by turning on a "spin" on the feedbackTarget with the loading
    ///     message provided and then calls the asyncCall. If the call is successful, it turns off the spin and displays the
    ///     content. If an exception is caught, the method calls the DisplayError method on the feedbackTarget and passes in
    ///     the exception and an optional hideContent boolean. The method also calls StateHasChanged twice, once before the
    ///     asyncCall and once after.
    /// </summary>
    /// <param name="asyncCall">Task to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="loadingMessage">message that is displayed while the spinner is busy</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    [DebuggerHidden]
    protected async Task TryWithLoadingMessageAsync(Func<Task> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, bool hideContent = false)
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

    /// <summary>
    ///     This overload is a wrapper around TryWithLoadingMessageAsync
    /// </summary>
    /// <param name="asyncCall">Task to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="loadingMessage">message that is displayed while the spinner is busy</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    [DebuggerHidden]
    protected async Task TryAsync(Func<Task> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, bool hideContent = false)
    {
        await TryWithLoadingMessageAsync(asyncCall, feedbackTarget, loadingMessage, hideContent);
    }

    /// <summary>
    ///     Method to perform an asynchronous call and handle any errors that may occur. It takes in four parameters: an
    ///     asyncCall (which is a function that returns a task), a feedbackTarget (which is an object that has methods for
    ///     handling feedback during the async call), a loadingMessage (a string message to be displayed while loading), and an
    ///     optional hideContent boolean. The method starts by turning on a "spin" on the feedbackTarget with the loading
    ///     message provided and then calls the asyncCall. If the call is successful, it turns off the spin and displays the
    ///     content. If an exception is caught, the method calls the DisplayError method on the feedbackTarget and passes in
    ///     the exception and an optional hideContent boolean. The method also calls StateHasChanged twice, once before the
    ///     asyncCall and once after.
    /// </summary>
    /// <param name="asyncCall">Task to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="loadingMessage">message that is displayed while the spinner is busy</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    /// <typeparam name="T">Type of the object that is returned by asyncCall</typeparam>
    /// <returns>result of asyncCall</returns>
    [DebuggerHidden]
    protected async Task<T> TryWithLoadingMessageAsync<T>(Func<Task<T>> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, bool hideContent = false)
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
            loadingMessage + "failed",
            hideContent);
            await InvokeAsync(StateHasChanged);

            return default;
        }
    }

    /// <summary>
    ///     This overload is a wrapper around TryWithLoadingMessageAsync
    /// </summary>
    /// <param name="asyncCall">Task to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="loadingMessage">message that is displayed while the spinner is busy</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    /// <typeparam name="T">Type of the object that is returned by asyncCall</typeparam>
    /// <returns>result of asyncCall</returns>
    [DebuggerHidden]
    protected async Task<T> TryAsync<T>(Func<Task<T>> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, bool hideContent = false) =>
        await TryWithLoadingMessageAsync(asyncCall, feedbackTarget, loadingMessage, hideContent);

    /// <summary>
    ///     This is a slightly modified version of the C# method that is used to perform an asynchronous call and handle any
    ///     errors that may occur. It also returns a result of a generic type T. It takes in five parameters: an asyncCall
    ///     (which is a function that returns a task of type T), a feedbackTarget (which is an object that has methods for
    ///     handling feedback during the async call), a loadingMessage and a successMessage string, and an optional hideContent
    ///     boolean. The method starts by turning on a "spin" with a loading message on the feedbackTarget and then calls the
    ///     asyncCall. If the call is successful, it displays a success message and return the result. If an exception is
    ///     caught, the method calls the DisplayError method on the feedbackTarget and passes in the exception, an additional
    ///     message and an optional hideContent boolean. The method also calls StateHasChanged twice, once before the asyncCall
    ///     and once after. If an error occurs, it returns the default value of T.
    /// </summary>
    /// <param name="asyncCall">Task to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="loadingMessage">message that is displayed while the spinner is busy</param>
    /// <param name="successMessage">message that is displayed after asyncCall was successful</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    /// <typeparam name="T">Type of the object that is returned by asyncCall</typeparam>
    /// <returns>result of asyncCall</returns>
    [DebuggerHidden]
    protected async Task<T> TryWithLoadingAndSuccessMessageAsync<T>(Func<Task<T>> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, string successMessage,
        bool hideContent = false)
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
            loadingMessage + "failed",
            hideContent);
            await InvokeAsync(StateHasChanged);

            return default;
        }
    }

    /// <summary>
    ///     This overload is a wrapper around TryWithLoadingAndSuccessMessageAsync
    /// </summary>
    /// <param name="asyncCall">Task to be executed</param>
    /// <param name="feedbackTarget">target feedback area to show spinner animation</param>
    /// <param name="loadingMessage">message that is displayed while the spinner is busy</param>
    /// <param name="successMessage">message that is displayed after asyncCall was successful</param>
    /// <param name="hideContent">determine whether content within feedback area shall be hidden or not</param>
    /// <typeparam name="T">Type of the object that is returned by asyncCall</typeparam>
    /// <returns>result of asyncCall</returns>
    [DebuggerHidden]
    protected async Task<T> TryAsync<T>(Func<Task<T>> asyncCall, FeedbackTarget feedbackTarget, string loadingMessage, string successMessage, bool hideContent = false) =>
        await TryWithLoadingAndSuccessMessageAsync(asyncCall, feedbackTarget, loadingMessage, successMessage, hideContent);
}
