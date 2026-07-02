namespace TianGongKaiWu;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        try
        {
            MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            MainPage = new ContentPage
            {
                Content = new Label
                {
                    Text = $"启动失败: {ex.Message}",
                    TextColor = Colors.White,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                },
                BackgroundColor = Colors.DarkRed
            };
        }
    }
}
