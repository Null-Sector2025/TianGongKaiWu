using System;
using System.Linq;
using TianGongKaiWu.Core;

namespace TianGongKaiWu.Pages;
public partial class GrammarPage : ContentPage
{
    public GrammarPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        this.Opacity = 0;
        await this.FadeTo(1, 400);
        RefreshList();
    }
    private void RefreshList()
    {
        GrammarListView.ItemsSource = GrammarManager.GrammarDict.Select(kv => new { Key = kv.Key, Value = kv.Value }).ToList();
    }
    private void OnExport(object sender, EventArgs e)
    {
        GrammarManager.ExportGrammarToTxt();
        DisplayAlert("完成", "已导出至 Download/grammar_map.txt", "确定");
    }
    private async void OnImport(object sender, EventArgs e)
    {
        var file = await FilePicker.PickAsync(new PickOptions { FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> { { DevicePlatform.Android, new[] { ".txt" } } }) });
        if (file != null)
        {
            GrammarManager.ImportGrammarFromTxt(file.FullPath);
            RefreshList();
            await DisplayAlert("成功", "字典已更新", "OK");
        }
    }
}
