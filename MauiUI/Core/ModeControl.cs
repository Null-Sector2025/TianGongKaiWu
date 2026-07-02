namespace TianGongKaiWu.Core;
public static class ModeControl
{
    public static bool IsLightMode { get; set; }
    public static void EnableFullMode() => IsLightMode = false;
    public static void EnableLightMode() => IsLightMode = true;
}
