using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Maui.Storage;

namespace TianGongKaiWu.Core;
public static class GrammarManager
{
    public static Dictionary<string, string> GrammarDict { get; private set; } = new();
    public static void Initialize()
    {
        try
        {
            using var stream = FileSystem.OpenAppPackageFileAsync("GrammarData/default_grammar.txt").Result;
            using var reader = new StreamReader(stream);
            Parse(reader.ReadToEnd());
        }
        catch (Exception ex)
        {
            ErrorLogger.Log("语法文件加载", ex);
        }
    }
    private static void Parse(string raw)
    {
        GrammarDict.Clear();
        foreach (var line in raw.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Trim().Split('|');
            if (parts.Length == 2) GrammarDict[parts[0].Trim()] = parts[1].Trim();
        }
    }
    public static void ExportGrammarToTxt()
    {
        // 导出到下载目录，但捕获异常
        try
        {
            string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Download/grammar_map.txt");
            string content = string.Join("\n", GrammarDict.Select(kv => $"{kv.Key}|{kv.Value}"));
            File.WriteAllText(path, content);
        }
        catch (Exception ex)
        {
            ErrorLogger.Log("导出语法", ex);
            // 备选：保存到内部
            string internalPath = Path.Combine(FileSystem.AppDataDirectory, "grammar_map.txt");
            File.WriteAllText(internalPath, string.Join("\n", GrammarDict.Select(kv => $"{kv.Key}|{kv.Value}")));
        }
    }
    public static void ImportGrammarFromTxt(string filePath)
    {
        if (File.Exists(filePath)) Parse(File.ReadAllText(filePath));
    }
}
