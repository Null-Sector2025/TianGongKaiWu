using System.Text.RegularExpressions;
namespace TianGongKaiWu.Core;
public class PinyinInterpreter
{
    private Dictionary<string, object> vars = new();
    private Dictionary<string, string> grammar;
    public event Action<string> OnOutput;
    public PinyinInterpreter(Dictionary<string, string> grammarDict)
    {
        grammar = grammarDict;
    }
    public void Run(string code)
    {
        var lines = code.Split('\n');
        foreach (var line in lines)
        {
            ExecuteLine(line.Trim());
        }
    }
    private void ExecuteLine(string line)
    {
        if (string.IsNullOrEmpty(line)) return;
        if (line.StartsWith("dayin")) HandlePrint(line);
        else if (line.StartsWith("bianliang")) HandleVar(line);
        else if (line.StartsWith("ruguo")) OnOutput?.Invoke("(条件语句暂未实现，请使用盘古转换)");
    }
    private void HandlePrint(string line)
    {
        var match = Regex.Match(line, @"dayin\s*\(\s*""?(.*?)""?\s*\)");
        if (match.Success)
        {
            string content = match.Groups[1].Value;
            foreach (var v in vars)
                content = content.Replace($"${v.Key}", v.Value.ToString());
            OnOutput?.Invoke(content);
        }
    }
    private void HandleVar(string line)
    {
        var match = Regex.Match(line, @"bianliang\s+(\w+)\s*=\s*(.*);");
        if (match.Success)
        {
            string name = match.Groups[1].Value;
            string value = match.Groups[2].Value.Trim('"');
            vars[name] = value;
        }
    }
}
