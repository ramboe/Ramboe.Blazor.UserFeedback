using Microsoft.AspNetCore.Components;
using Ramboe.Blazor.UserFeedback.Services;

namespace Ramboe.Blazor.UserFeedback;

public partial class FeedbackArea
{
    [Parameter]
    public string SpinnerSizeInRem { get; set; }

    [Parameter]
    public SpinnerSizeMode SpinnerSize { get; set; } = SpinnerSizeMode.Medium;

    [Parameter]
    public ColorMode Color { get; set; } = ColorMode.DarkGrey;

    [Parameter]
    public string ColorString { get; set; }

    [Parameter]
    public string SpinnerMargin { get; set; } = "auto"; // "200px";

    [Parameter]
    public string ErrorMaxWidth { get; set; } = "100%";

    [Parameter]
    public string ErrorMarginBottom { get; set; } = string.Empty;

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public FeedbackTarget Target { get; set; }

    protected override void OnInitialized()
    {
        setSpinnerSize();

        setColorString();

        void setColorString()
        {
            if (Color == null) return;

            if (string.IsNullOrEmpty(ColorString)) return;

            ColorString = Color switch
            {
                ColorMode.LightGrey => "#D3D3D3FF",
                ColorMode.DarkGrey => "#2C2C2CFF",
                ColorMode.Black => "#000000",
                _ => "#4c4c4c"
            };
        }

        void setSpinnerSize()
        {
            if (!string.IsNullOrEmpty(SpinnerSizeInRem) || SpinnerSize == null) return;

            SpinnerSizeInRem = SpinnerSize switch
            {
                SpinnerSizeMode.Small => "2rem",
                SpinnerSizeMode.Medium => "4rem",
                SpinnerSizeMode.Large => "8rem",
                _ => SpinnerSizeInRem
            };
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        Target.FeedbackModel.ShowErrorMessage = false;
    }
}