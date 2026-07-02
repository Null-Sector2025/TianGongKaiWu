using Microsoft.Extensions.Logging;
namespace TianGongKaiWu;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        builder.Services.AddSingleton<Pages.PanGuPage>();
        builder.Services.AddSingleton<Pages.NvWaPage>();
        builder.Services.AddSingleton<Pages.FileManagerPage>();
        builder.Services.AddSingleton<Pages.GrammarPage>();
        return builder.Build();
    }
}
