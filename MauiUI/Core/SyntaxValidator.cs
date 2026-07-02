using System.Text.RegularExpressions;
namespace TianGongKaiWu.Core;
public class SyntaxValidator
{
    private HashSet<string> ndkKeys;
    public SyntaxValidator(Dictionary<string, string> dict)
    {
        ndkKeys = dict.Where(kv => kv.Key.Contains("ndk") || kv.Value.Contains("jni") || kv.Value.Contains("native")).Select(kv => kv.Key).ToHashSet();
    }
    public ValidationResult Validate(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) return new ValidationResult { IsValid = false, ErrorMessage = "代码为空" };
        var lines = code.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (line.Length == 0) continue;
            if (!Regex.IsMatch(line, @"^\s*(bianliang|ruguo|fouze|xunhuan|dang|dayin|shuru|hanshu|fanhui|lei|chuangjian|anniu|bianji|xianshi)\b") && !Regex.IsMatch(line, @"^\s*\w+\s*(=|\.|\(|\)|;)") && !line.StartsWith("{") && !line.StartsWith("}"))
                return new ValidationResult { IsValid = false, ErrorLine = i + 1, ErrorMessage = "未知语句" };
        }
        return ValidationResult.Valid;
    }
    public bool ContainsNdkKeywords(string code) => Regex.Split(code, @"\s+|[();,{}""]+").Any(w => ndkKeys.Contains(w));
}
public class ValidationResult
{
    public bool IsValid { get; set; }
    public int ErrorLine { get; set; }
    public string ErrorMessage { get; set; }
    public static ValidationResult Valid => new ValidationResult { IsValid = true };
}
