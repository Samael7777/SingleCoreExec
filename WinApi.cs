using System.Runtime.InteropServices;

namespace SingleCoreExec;

internal enum ProcessDpiAwareness
{
    ProcessDpiUnaware = 0,
    ProcessSystemDpiAware = 1,
    ProcessPerMonitorDpiAware = 2
}

internal static class WinApi
{
    [DllImport("user32.dll", EntryPoint="MessageBoxW")]
    internal static extern int MessageBoxW(IntPtr hWnd, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpText, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpCaption, int uType) ;

    

    [DllImport("Shcore.dll")]
    public static extern int SetProcessDpiAwareness(ProcessDpiAwareness awareness);

    [DllImport("User32.dll")]
    public static extern bool SetProcessDPIAware();

    [DllImport("User32.dll")]
    public static extern bool SetProcessDpiAwarenessContext(IntPtr dpiAwarenessContext);
}