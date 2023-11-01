using System.Diagnostics;

namespace SingleCoreExec;

public class AppContext : ApplicationContext
{
    public AppContext(string[] args)
    {
        if (args.Length == 0)
            throw new ArgumentException("App name not specified.\r\nUse:\r\nSingleCoreExec App.exe");

        var appName = args[0];
        var cmdLine = args.Length == 1 
            ? ""
            : string.Join(" ", args[1..]);

        Task.Run(() => StartAppOnSingleCore(appName, cmdLine));
    }

    private void StartAppOnSingleCore(string appName, string cmdLine)
    {
        if (!File.Exists(appName))
            throw new FileNotFoundException($"{appName} not found.");

        var startInfo = new ProcessStartInfo(appName)
        {
            Arguments = string.Join(" ", cmdLine),
            UseShellExecute = true
        };
        var process = Process.Start(startInfo) ?? 
                      throw new ApplicationException("Can't create app process.");

        process.ProcessorAffinity = (IntPtr)0x01;
        
        while(!Application.AllowQuit){}
        Application.Exit();
    }
}