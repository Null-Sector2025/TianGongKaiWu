using TianGongKaiWu.Core;

namespace TianGongKaiWu.Pages;
public partial class ErrorPage : ContentPage
{
    public ErrorPage()
    {
        InitializeComponent();
        RefreshList();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshList();
    }

    private void RefreshList()
    {
        ErrorListView.ItemsSource = ErrorLogger.Errors;
    }

    private void OnClear(object sender, EventArgs e)
    {
        ErrorLogger.Clear();
        RefreshList();
    }
}
