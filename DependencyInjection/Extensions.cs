using Microsoft.Extensions.DependencyInjection;

namespace Ramboe.Blazor.UserFeedback.DependencyInjection;

public static class Extensions
{
    public static IFeedbackConfigurator AddFeedbackAreas(this IServiceCollection services)
    {
        services.AddScoped<FeedbackConfiguration>(c => new FeedbackConfiguration
        {
            SpinnerSizeMode = SpinnerSizeMode.Medium,
            ColorMode = ColorMode.LightGrey,
            SpinnerType = SpinnerType.BorderSpinner
        });

        return new FeedbackConfigurator(services);
    }

    public static IFeedbackConfigurator WithType(this IFeedbackConfigurator configurator, SpinnerType spinnerType)
    {
        configurator.SetType(spinnerType);

        return configurator;
    }

    public static IFeedbackConfigurator WithSize(this IFeedbackConfigurator configurator, SpinnerSizeMode spinnerSizeMode)
    {
        configurator.SetSize(spinnerSizeMode);

        return configurator;
    }

    public static IFeedbackConfigurator WithColor(this IFeedbackConfigurator configurator, ColorMode colorMode)
    {
        configurator.SetColor(colorMode);

        return configurator;
    }
}
public class FeedbackConfigurator : IFeedbackConfigurator
{
    readonly IServiceCollection _services;

    SpinnerSizeMode _spinnerSizeMode;

    ColorMode _colorMode;

    SpinnerType _spinnerType;

    public FeedbackConfigurator(IServiceCollection services)
    {
        _services = services;
    }

    public FeedbackConfigurator(SpinnerSizeMode spinnerSizeMode)
    {
        _spinnerSizeMode = spinnerSizeMode;
    }

    public IServiceCollection ConfigureFeedbackAreas()
    {
        _services.AddScoped<FeedbackConfiguration>(c => new FeedbackConfiguration
        {
            SpinnerSizeMode = _spinnerSizeMode,
            ColorMode = _colorMode,
            SpinnerType = _spinnerType
        });

        return _services;
    }

    public void SetColor(ColorMode colorMode)
    {
        _colorMode = colorMode;
    }

    public void SetSize(SpinnerSizeMode spinnerSizeMode)
    {
        _spinnerSizeMode = spinnerSizeMode;
    }

    public void SetType(SpinnerType spinnerType)
    {
        _spinnerType = spinnerType;
    }
}
public interface IFeedbackConfigurator
{
    IServiceCollection ConfigureFeedbackAreas();

    void SetColor(ColorMode colorMode);

    void SetSize(SpinnerSizeMode spinnerSizeMode);

    void SetType(SpinnerType spinnerType);
}
