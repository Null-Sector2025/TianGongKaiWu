using System;
using System.Collections.Generic;

namespace TianGongKaiWu.Core;
public class PinyinTranslator : IDisposable
{
    private readonly Dictionary<string, string> _grammar;
    public PinyinTranslator()
    {
        _grammar = GrammarManager.GrammarDict;
    }
    public string Translate(string pinyinCode, string targetLang)
    {
        if (string.IsNullOrEmpty(pinyinCode)) return string.Empty;
        string result = pinyinCode;
        foreach (var pair in _grammar)
        {
            string replacement = pair.Value;
            if (replacement.Contains(' '))
                replacement = replacement.Split(' ')[0];
            result = result.Replace(pair.Key, replacement);
        }
        // 按语言微调
        if (targetLang == "python")
        {
            result = result.Replace("zhen", "True").Replace("jia", "False").Replace("kong", "None");
        }
        else if (targetLang == "javascript")
        {
            result = result.Replace("dayin", "console.log");
        }
        return result;
    }
    public void Dispose() { }
}
