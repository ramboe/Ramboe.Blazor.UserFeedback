namespace Ramboe.Blazor.UserFeedback.DependencyInjection;

public class FeedbackConfiguration
{
    public SpinnerSizeMode SpinnerSizeMode { get; set; }

    public SpinnerColorMode SpinnerColorMode { get; set; }

    public SpinnerTypeMode SpinnerTypeMode { get; set; }

    public string SpinnerSizeInRem { get; set; }
    public string ColorHex { get; set; }
}
