using System.Collections.Generic;
using System.IO;
using System.Linq;
using AndroidEnv = Android.OS.Environment;

namespace TianGongKaiWu.Core;
public class ProjectItem
{
    public string Name { get; set; } = "";
    public string Path { get; set; } = "";
}
public class ProjectManager
{
    public static readonly string ProjectDir = Path.Combine(AndroidEnv.ExternalStorageDirectory.AbsolutePath, "天工开物");
    public static ProjectManager Shared { get; } = new();
    public string? CurrentFile { get; set; }
    public string? CurrentContent { get; set; }
    private ProjectManager()
    {
        if (!Directory.Exists(ProjectDir)) Directory.CreateDirectory(ProjectDir);
    }
    public List<ProjectItem> GetProjects()
    {
        return Directory.GetFiles(ProjectDir, "*.pinyin").Select(f => new ProjectItem { Name = Path.GetFileName(f), Path = f }).ToList();
    }
}
