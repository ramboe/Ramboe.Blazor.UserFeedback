using Microsoft.AspNetCore.Components;
using Ramboe.Blazor.UserFeedback.DependencyInjection;
using Ramboe.Blazor.UserFeedback.Services;

namespace Ramboe.Blazor.UserFeedback;

public partial class FeedbackArea
{
    [Inject] public FeedbackConfiguration FeedbackConfiguration { get; set; }

    #region strings
    [Parameter] public string SpinnerMargin { get; set; } = "auto";

    [Parameter] public string ErrorMaxWidth { get; set; } = "100%";

    [Parameter] public string ErrorMarginBottom { get; set; } = string.Empty;

    [Parameter] public string SpinnerSizeInRem { get; set; }

    [Parameter] public string ColorHexFromParams { get; set; }

    [Parameter] public string SpinnerTypeString { get; set; }
    #endregion

    #region enums
    [Parameter] public SpinnerSizeMode? SpinnerSizeFromParams { get; set; }

    [Parameter] public SpinnerColorMode? ColorModeFromParams { get; set; }

    [Parameter] public SpinnerTypeMode? SpinnerTypeFromParams { get; set; }
    #endregion

    [Parameter] public RenderFragment ChildContent { get; set; }

    [Parameter] public FeedbackTarget Target { get; set; }

    public string ColorClass { get; set; }

    protected override void OnInitialized()
    {
        SpinnerSizeInRem = setSpinnerSize(SpinnerSizeInRem, FeedbackConfiguration.SpinnerSizeMode, SpinnerSizeFromParams);
        SpinnerTypeString = setSpinnerType(SpinnerTypeString, FeedbackConfiguration.SpinnerTypeMode, SpinnerTypeFromParams);
        ColorClass = setColorString(ColorClass, FeedbackConfiguration.SpinnerColorMode, ColorModeFromParams);

        string setColorString(string colorClass, SpinnerColorMode feedbackConfigurationColorMode, SpinnerColorMode? colorModeFromParams)
        {
            if (string.IsNullOrEmpty(ColorHexFromParams) is false)
            {
                return null;
            }

            if (string.IsNullOrEmpty(FeedbackConfiguration.ColorHex) is false && colorModeFromParams is null)
            {
                ColorHexFromParams = FeedbackConfiguration.ColorHex;

                return null;
            }

            if (string.IsNullOrEmpty(colorClass) is false)
            {
                return colorClass;
            }

            var colormode = feedbackConfigurationColorMode;

            if (colorModeFromParams is not null)
            {
                colormode = (SpinnerColorMode) colorModeFromParams;
            }

            return colormode switch
            {
                SpinnerColorMode.Primary => "text-primary",
                SpinnerColorMode.Secondary => "text-secondary",
                SpinnerColorMode.Success => "text-success",
                SpinnerColorMode.Danger => "text-danger",
                SpinnerColorMode.Warning => "text-warning",
                SpinnerColorMode.Info => "text-info",
                SpinnerColorMode.Light => "text-light",
                SpinnerColorMode.Dark => "text-dark",
                var _ => "text-secondary"
            };
        }

        string setSpinnerType(string spinnerTypeString, SpinnerTypeMode feedbackConfigurationSpinnerType, SpinnerTypeMode? spinnerTypeFromParams)
        {
            if (string.IsNullOrEmpty(spinnerTypeString) is false)
            {
                return spinnerTypeString;
            }

            var spinnertype = feedbackConfigurationSpinnerType;

            if (spinnerTypeFromParams is not null)
            {
                spinnertype = (SpinnerTypeMode) spinnerTypeFromParams;
            }

            return spinnertype switch
            {
                SpinnerTypeMode.BorderSpinner => "spinner-border",
                SpinnerTypeMode.GrowingSpiner => "spinner-grow",
                var _ => spinnerTypeString
            };
        }

        string setSpinnerSize(string spinnerSizeInRem, SpinnerSizeMode feedbackConfigurationSpinnerSizeMode, SpinnerSizeMode? spinnerSizeFromParams)
        {
            if (string.IsNullOrEmpty(spinnerSizeInRem) is false)
            {
                return spinnerSizeInRem;
            }

            if (string.IsNullOrEmpty(FeedbackConfiguration.SpinnerSizeInRem) is false)
            {
                return FeedbackConfiguration.SpinnerSizeInRem;
            }

            var spinnertype = feedbackConfigurationSpinnerSizeMode;

            if (spinnerSizeFromParams is not null)
            {
                spinnertype = (SpinnerSizeMode) spinnerSizeFromParams;
            }

            return spinnertype switch
            {
                SpinnerSizeMode.Vh90 => "90vh",
                SpinnerSizeMode.Vh80 => "80vh",
                SpinnerSizeMode.Vh70 => "70vh",
                SpinnerSizeMode.Vh60 => "60vh",
                SpinnerSizeMode.Vh50 => "50vh",
                SpinnerSizeMode.Vh40 => "40vh",
                SpinnerSizeMode.Vh30 => "30vh",
                SpinnerSizeMode.Vh20 => "20vh",
                SpinnerSizeMode.Vh10 => "10vh",
                SpinnerSizeMode.Vh5 => "5vh",
                SpinnerSizeMode.Vh3 => "3vh",
                SpinnerSizeMode.Vh2 => "2vh",
                var _ => spinnerSizeInRem
            };
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        Target.FeedbackModel.ShowErrorMessage = false;
    }
}
