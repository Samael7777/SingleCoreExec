using System.Diagnostics;


namespace SingleCoreExec;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        EnableHiDpi();

        if (args.Length == 0)
        {
            ShowError("App name not specified.\r\nUse:\r\nSingleCoreExec App.exe");
            return;
        }

        var appName = args[0];
        var cmdLine = args.Length == 1 ? [""] : args[1..];
        
        StartAppOnSingleCore(appName, cmdLine);
    }

    private static void StartAppOnSingleCore(string appName, string[] cmdLine)
    {
        try
        {
            if (!File.Exists(appName))
                throw new FileNotFoundException($"{appName} not found.");

            var startInfo = new ProcessStartInfo(appName)
            {
                Arguments = string.Join(" ", cmdLine),
                UseShellExecute = true
            };

            using var process = new Process();
            process.StartInfo = startInfo;
            
            if (!process.Start())
                throw new ApplicationException("Can't create app process.");

            process.ProcessorAffinity = 0x01;
        }
        catch (Exception e)
        {
            ShowError(e.Message);
        }
    }

    private static void ShowError(string message)
    {
        const int mbError = 0x00000010;

        _ = WinApi.MessageBoxW(IntPtr.Zero, message, "Error", mbError);
    }

    private static void EnableHiDpi()
    {
        try
        {
            // Use System DPI Awareness
            _ = WinApi.SetProcessDpiAwareness(ProcessDpiAwareness.ProcessSystemDpiAware);
        }
        catch (DllNotFoundException)
        {
            // Fallback for older systems
            WinApi.SetProcessDPIAware();
        }
    }
}
