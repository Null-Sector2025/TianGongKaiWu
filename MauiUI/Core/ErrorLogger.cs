using System;
using System.Collections.Generic;
using System.Linq;

namespace TianGongKaiWu.Core;
public static class ErrorLogger
{
    private static List<ErrorRecord> _errors = new();

    public static IReadOnlyList<ErrorRecord> Errors => _errors.AsReadOnly();

    public static void Log(string context, Exception ex)
    {
        _errors.Add(new ErrorRecord
        {
            Time = DateTime.Now,
            Context = context,
            Message = ex.Message,
            StackTrace = ex.StackTrace ?? ""
        });
        // 保留最近 100 条
        if (_errors.Count > 100)
            _errors = _errors.Skip(_errors.Count - 100).ToList();
    }

    public static void Log(string context, string message)
    {
        _errors.Add(new ErrorRecord
        {
            Time = DateTime.Now,
            Context = context,
            Message = message,
            StackTrace = ""
        });
    }

    public static void Clear() => _errors.Clear();
}

public class ErrorRecord
{
    public DateTime Time { get; set; }
    public string Context { get; set; } = "";
    public string Message { get; set; } = "";
    public string StackTrace { get; set; } = "";
    public string ShortInfo => $"[{Time:HH:mm:ss}] {Context}: {Message}";
}
