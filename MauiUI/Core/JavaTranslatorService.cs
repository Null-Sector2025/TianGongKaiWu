using System;
using Android.Runtime;
using Java.Interop;

namespace TianGongKaiWu.Core;
public class JavaTranslatorService : IDisposable
{
    private JniObjectReference lexer;
    private IntPtr method;
    private bool ready;
    public JavaTranslatorService()
    {
        try
        {
            var cls = JNIEnv.FindClass("com/tgk/parse/Lexer");
            var ctor = JNIEnv.GetMethodID(cls, "<init>", "(Ljava/lang/String;)V");
            using var g = new JniObjectReference(GrammarManager.GetGrammarRawText());
            lexer = JNIEnv.NewObject(cls, ctor, new JValue(g));
            method = JNIEnv.GetMethodID(cls, "translateToLang", "(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;");
            ready = true;
        }
        catch { ready = false; }
    }
    public string Translate(string code, string lang)
    {
        if (!ready) return "翻译服务不可用";
        using var c = new JniObjectReference(code);
        using var l = new JniObjectReference(lang);
        var r = JNIEnv.CallObjectMethod(lexer, method, new JValue(c), new JValue(l));
        return JNIEnv.GetString(r, JniHandleOwnership.TransferLocalRef);
    }
    public void Dispose()
    {
        if (lexer != IntPtr.Zero) JNIEnv.DeleteLocalRef(lexer);
    }
}
