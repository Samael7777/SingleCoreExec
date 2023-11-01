namespace SingleCoreExec
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnDomainException;
            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.ThreadException += OnThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.Run(new AppContext(args));
        }

        private static void OnDomainException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler(e.ExceptionObject as Exception);
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionHandler(e.Exception);
        }

        private static void ExceptionHandler(Exception? ex)
        {
            MessageBox.Show(ex?.Message ?? "Unknown error.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.ExitThread();
        }
    }
}