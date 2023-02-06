using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace Ramboe.Blazor.UserFeedback.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddStandardFeedbackAreas(this IServiceCollection services)
    {
        services.AddScoped<FeedbackConfiguration>(c => new FeedbackConfiguration
        {
            SpinnerSizeMode = SpinnerSizeMode.Vh5,
            SpinnerColorMode = SpinnerColorMode.Primary,
            SpinnerTypeMode = SpinnerTypeMode.BorderSpinner
        });

        return services;
    }

    public static IFeedbackConfigurator AddAndConfigureFeedbackAreas(this IServiceCollection services)
    {
        services.AddScoped<FeedbackConfiguration>(c => new FeedbackConfiguration
        {
            SpinnerSizeMode = SpinnerSizeMode.Vh5,
            SpinnerColorMode = SpinnerColorMode.Primary,
            SpinnerTypeMode = SpinnerTypeMode.BorderSpinner
        });

        return new FeedbackConfigurator(services);
    }

    public static IFeedbackConfigurator WithType(this IFeedbackConfigurator configurator, SpinnerTypeMode spinnerTypeMode)
    {
        configurator.SetType(spinnerTypeMode);

        return configurator;
    }

    public static IFeedbackConfigurator WithSize(this IFeedbackConfigurator configurator, SpinnerSizeMode spinnerSizeMode)
    {
        configurator.SetSize(spinnerSizeMode);

        return configurator;
    }

    public static IFeedbackConfigurator WithColor(this IFeedbackConfigurator configurator, SpinnerColorMode spinnerColorMode)
    {
        configurator.SetColorHex(null);
        configurator.SetColor(spinnerColorMode);

        return configurator;
    }

    public static IFeedbackConfigurator WithColor(this IFeedbackConfigurator configurator, string colorHex)
    {
        if (colorHex.Contains('#') is false)
        {
            colorHex = "#" + colorHex;
        }

        if (isValidHexColor(colorHex) is not true)
        {
            throw new Exception("given input is not a valid hexadecimal color: " + colorHex);
        }

        configurator.SetColorHex(colorHex);

        return configurator;
    }

    static bool isValidHexColor(string hexColor)
    {
        return Regex.IsMatch(hexColor, @"^#(?:[0-9a-fA-F]{3}){1,2}$");
    }
}
public class FeedbackConfigurator : IFeedbackConfigurator
{
    readonly IServiceCollection _services;
    string _colorHex;

    SpinnerColorMode _spinnerColorMode;

    SpinnerSizeMode _spinnerSizeMode;

    SpinnerTypeMode _spinnerTypeMode;

    public FeedbackConfigurator(IServiceCollection services)
    {
        _services = services;
    }

    public IServiceCollection ConfigureFeedbackAreas()
    {
        _services.AddScoped<FeedbackConfiguration>(c => new FeedbackConfiguration
        {
            SpinnerSizeMode = _spinnerSizeMode,
            SpinnerColorMode = _spinnerColorMode,
            SpinnerTypeMode = _spinnerTypeMode,
            ColorHex = _colorHex
        });

        return _services;
    }

    public void SetColor(SpinnerColorMode spinnerColorMode)
    {
        _spinnerColorMode = spinnerColorMode;
    }

    public void SetSize(SpinnerSizeMode spinnerSizeMode)
    {
        _spinnerSizeMode = spinnerSizeMode;
    }

    public void SetType(SpinnerTypeMode spinnerTypeMode)
    {
        _spinnerTypeMode = spinnerTypeMode;
    }

    public void SetColorHex(string hex)
    {
        _colorHex = hex;
    }
}
public interface IFeedbackConfigurator
{
    IServiceCollection ConfigureFeedbackAreas();

    void SetColor(SpinnerColorMode spinnerColorMode);

    void SetSize(SpinnerSizeMode spinnerSizeMode);

    void SetType(SpinnerTypeMode spinnerTypeMode);

    void SetColorHex(string hex);
}
