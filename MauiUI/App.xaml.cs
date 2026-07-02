using TianGongKaiWu.Core;

namespace TianGongKaiWu;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            ErrorLogger.Log("未处理异常", args.ExceptionObject as Exception ?? new Exception("未知错误"));
        };
        TaskScheduler.UnobservedTaskException += (sender, args) =>
        {
            ErrorLogger.Log("未观测任务异常", args.Exception);
        };

        try
        {
            MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            ErrorLogger.Log("应用启动", ex);
            MainPage = new ContentPage
            {
                BackgroundColor = Colors.DarkRed,
                Content = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label { Text = "启动失败", TextColor = Colors.White, FontSize = 24, HorizontalOptions = LayoutOptions.Center },
                        new Label { Text = ex.Message, TextColor = Colors.White, FontSize = 16, HorizontalOptions = LayoutOptions.Center },
                        new Button { Text = "查看错误日志", BackgroundColor = Colors.Gray, TextColor = Colors.White, Command = new Command(() => MainPage = new AppShell()) }
                    }
                }
            };
        }
    }
}
