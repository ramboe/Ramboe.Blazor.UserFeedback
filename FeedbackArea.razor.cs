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

    [Parameter] public string ColorString { get; set; }

    [Parameter] public string SpinnerTypeString { get; set; }
    #endregion

    #region enums
    [Parameter] public SpinnerSizeMode? SpinnerSizeFromParams { get; set; }

    [Parameter] public ColorMode? ColorModeFromParams { get; set; }

    [Parameter] public SpinnerType? SpinnerTypeFromParams { get; set; }

    [Parameter] public RenderFragment ChildContent { get; set; }
    #endregion

    [Parameter] public FeedbackTarget Target { get; set; }

    protected override void OnInitialized()
    {
        SpinnerSizeInRem = setSpinnerSize();
        SpinnerTypeString = setSpinnerType();
        ColorString = setColorString();

        string setColorString()
        {
            if (string.IsNullOrEmpty(ColorString) is false)
            {
                return ColorString;
            }

            var colormode = FeedbackConfiguration.ColorMode;

            if (ColorModeFromParams is not null)
            {
                colormode = (ColorMode) ColorModeFromParams;
            }

            return colormode switch
            {
                ColorMode.LightGrey => "#D3D3D3FF",
                ColorMode.DarkGrey => "#2C2C2CFF",
                ColorMode.Black => "#000000",
                var _ => "#4c4c4c"
            };
        }

        string setSpinnerType()
        {
            if (string.IsNullOrEmpty(SpinnerTypeString) is false)
            {
                return SpinnerTypeString;
            }

            var spinnertype = FeedbackConfiguration.SpinnerType;

            if (SpinnerTypeFromParams is not null)
            {
                spinnertype = (SpinnerType) SpinnerTypeFromParams;
            }

            return spinnertype switch
            {
                SpinnerType.BorderSpinner => "spinner-border",
                SpinnerType.GrowingSpiner => "spinner-grow",
                var _ => SpinnerTypeString
            };
        }

        string setSpinnerSize()
        {
            if (string.IsNullOrEmpty(SpinnerSizeInRem) is false)
            {
                return SpinnerSizeInRem;
            }

            var spinnertype = FeedbackConfiguration.SpinnerSizeMode;

            if (SpinnerSizeFromParams is not null)
            {
                spinnertype = (SpinnerSizeMode) SpinnerSizeFromParams;
            }

            return spinnertype switch
            {
                SpinnerSizeMode.Small => "2rem",
                SpinnerSizeMode.Medium => "4rem",
                SpinnerSizeMode.Large => "8rem",
                var _ => SpinnerSizeInRem
            };
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        Target.FeedbackModel.ShowErrorMessage = false;
    }
}
