using TianGongKaiWu.Core;
namespace TianGongKaiWu.Pages;
public partial class PanGuPage : ContentPage
{
    private JavaTranslatorService _translator;
    private string _currentFilePath;

    public PanGuPage()
    {
        InitializeComponent();
        GrammarManager.Initialize();
        _translator = new JavaTranslatorService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // 进场动画
        this.Opacity = 0;
        await this.FadeTo(1, 500, Easing.CubicInOut);
        if (!string.IsNullOrEmpty(ProjectManager.Shared.CurrentFile))
            CodeEditor.Text = ProjectManager.Shared.CurrentContent;
    }

    private async void AnimateButton(Button btn)
    {
        await btn.ScaleTo(0.95, 100, Easing.CubicIn);
        await btn.ScaleTo(1, 100, Easing.CubicOut);
    }

    private async void OnConvertJava(object s, EventArgs e) { AnimateButton((Button)s); OutputEditor.Text = await Task.Run(() => Translate(CodeEditor.Text, "java")); }
    private async void OnConvertCSharp(object s, EventArgs e) { AnimateButton((Button)s); OutputEditor.Text = await Task.Run(() => Translate(CodeEditor.Text, "csharp")); }
    private async void OnConvertPython(object s, EventArgs e) { AnimateButton((Button)s); OutputEditor.Text = await Task.Run(() => Translate(CodeEditor.Text, "python")); }
    private async void OnConvertJS(object s, EventArgs e) { AnimateButton((Button)s); OutputEditor.Text = await Task.Run(() => Translate(CodeEditor.Text, "javascript")); }

    private async void OnExecuteCode(object s, EventArgs e)
    {
        AnimateButton((Button)s);
        var interpreter = new PinyinInterpreter(GrammarManager.GrammarDict);
        interpreter.OnOutput += (output) => MainThread.BeginInvokeOnMainThread(() => OutputEditor.Text = output);
        await Task.Run(() => interpreter.Run(CodeEditor.Text));
    }

    private string Translate(string code, string lang)
    {
        if (string.IsNullOrWhiteSpace(code)) return "代码为空";
        var validator = new SyntaxValidator(GrammarManager.GrammarDict);
        var res = validator.Validate(code);
        if (!res.IsValid) return $"语法错误 行{res.ErrorLine}: {res.ErrorMessage}";
        return _translator.Translate(code, lang);
    }

    private async void OnOpenProject(object s, EventArgs e)
    {
        var file = await FilePicker.PickAsync(new PickOptions { FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> { { DevicePlatform.Android, new[] { ".pinyin" } } }) });
        if (file != null)
        {
            _currentFilePath = file.FullPath;
            CodeEditor.Text = File.ReadAllText(file.FullPath);
            ProjectManager.Shared.CurrentFile = file.FullPath;
            ProjectManager.Shared.CurrentContent = CodeEditor.Text;
            await DisplayAlert("已打开", Path.GetFileName(file.FullPath), "OK");
        }
    }

    private async void OnSaveProject(object s, EventArgs e)
    {
        if (string.IsNullOrEmpty(_currentFilePath))
            _currentFilePath = Path.Combine(ProjectManager.ProjectDir, "未命名.pinyin");
        File.WriteAllText(_currentFilePath, CodeEditor.Text);
        ProjectManager.Shared.CurrentFile = _currentFilePath;
        ProjectManager.Shared.CurrentContent = CodeEditor.Text;
        await DisplayAlert("保存成功", $"已保存到 {_currentFilePath}", "OK");
    }
}
