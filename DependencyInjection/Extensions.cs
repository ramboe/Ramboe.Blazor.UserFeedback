using Microsoft.Extensions.DependencyInjection;

namespace Ramboe.Blazor.UserFeedback.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddFeedbackAreas(this IServiceCollection services)
    {
        services.AddScoped<FeedbackConfiguration>(c => new FeedbackConfiguration
        {
            SpinnerSizeMode = SpinnerSizeMode.Medium,
            ColorMode = ColorMode.LightGrey,
            SpinnerType = SpinnerType.BorderSpinner
        });

        return services;
    }
}
