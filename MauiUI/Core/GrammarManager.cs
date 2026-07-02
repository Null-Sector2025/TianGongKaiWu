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
        catch { }
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
        string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Download/grammar_map.txt");
        string content = string.Join("\n", GrammarDict.Select(kv => $"{kv.Key}|{kv.Value}"));
        File.WriteAllText(path, content);
    }
    public static void ImportGrammarFromTxt(string filePath)
    {
        if (File.Exists(filePath)) Parse(File.ReadAllText(filePath));
    }
}
