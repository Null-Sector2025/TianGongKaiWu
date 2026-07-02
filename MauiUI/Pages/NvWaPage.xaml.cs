using System;
using TianGongKaiWu.Core;

namespace TianGongKaiWu.Pages;
public partial class NvWaPage : ContentPage
{
    public NvWaPage()
    {
        InitializeComponent();
        ModeControl.EnableLightMode();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        this.Opacity = 0;
        await this.FadeTo(1, 600, Easing.SpringOut);
    }

    private async void OnRun(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        await btn.ScaleTo(0.9, 100, Easing.CubicIn);
        await btn.ScaleTo(1, 100, Easing.CubicOut);
        RunOutput.Text = "";
        string code = PlaygroundEditor.Text;
        if (string.IsNullOrWhiteSpace(code)) { RunOutput.Text = "代码为空"; return; }
        var validator = new SyntaxValidator(GrammarManager.GrammarDict);
        if (validator.ContainsNdkKeywords(code))
        {
            RunOutput.Text = "❌ 女娲模式禁止使用 NDK 关键字";
            return;
        }
        var interpreter = new PinyinInterpreter(GrammarManager.GrammarDict);
        interpreter.OnOutput += (output) => MainThread.BeginInvokeOnMainThread(() => RunOutput.Text += output + "\n");
        await Task.Run(() => interpreter.Run(code));
    }
}
