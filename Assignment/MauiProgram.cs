using Assignment.ViewModels;
using Assignment.Views;
using Microsoft.Extensions.Logging;
using Camera.MAUI;

namespace Assignment;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCameraView()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<RecipeViewModel>();
        builder.Services.AddTransient<BarcodeScanViewModel>();
        builder.Services.AddTransient<VoiceAssistantViewModel>();
        builder.Services.AddTransient<SensorsViewModel>();
        builder.Services.AddTransient<RecipeDetailViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<BarcodeScanPage>();
        builder.Services.AddTransient<AddEditRecipePage>();
        builder.Services.AddTransient<VoiceAssistantPage>();
        builder.Services.AddTransient<SensorsPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<RecipeDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}