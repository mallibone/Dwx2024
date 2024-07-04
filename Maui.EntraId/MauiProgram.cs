using Maui.EntraId.Services;
using Maui.EntraId.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.Maui.LifecycleEvents;

namespace Maui.EntraId;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(platform => platform.OnActivityResult((activity, rc, result, data) =>
                {
                    AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(rc, result, data);
                }));
#endif
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Services
            .ConfigureIdentity()
            .AddSingleton<IEntraService, EntraService>()
            .AddScoped<MainPage>()
            .AddScoped<MainViewModel>()
            .AddScoped<LoginPage>()
            .AddScoped<LoginViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}