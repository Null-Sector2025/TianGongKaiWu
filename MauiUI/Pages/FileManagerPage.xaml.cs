using TianGongKaiWu.Core;
namespace TianGongKaiWu.Pages;
public partial class FileManagerPage : ContentPage
{
    public FileManagerPage()
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
        ProjectListView.ItemsSource = ProjectManager.Shared.GetProjects();
    }
    private async void OnProjectSelected(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is ProjectItem item)
        {
            ProjectManager.Shared.CurrentFile = item.Path;
            ProjectManager.Shared.CurrentContent = File.ReadAllText(item.Path);
            await Shell.Current.GoToAsync("//PanGuPage");
        }
    }
    private async void OnNewProject(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("开天辟地", "输入项目名称", "创建", "取消", "例如: 山河.pinyin");
        if (!string.IsNullOrEmpty(name))
        {
            string path = Path.Combine(ProjectManager.ProjectDir, name);
            File.WriteAllText(path, "");
            RefreshList();
        }
    }
}
