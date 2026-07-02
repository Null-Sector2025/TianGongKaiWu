using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TianGongKaiWu.Core;
public class ProjectItem
{
    public string Name { get; set; } = "";
    public string Path { get; set; } = "";
}
public class ProjectManager
{
    // 使用应用内部存储，无需权限
    public static readonly string ProjectDir = Path.Combine(FileSystem.AppDataDirectory, "天工开物");
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
