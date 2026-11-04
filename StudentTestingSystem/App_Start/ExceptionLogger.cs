using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace StudentTestingSystem
{
    /// <summary>
    /// Last-chance logging of unhandled exceptions.
    /// The report is written to App_Data\Logs, so it is possible to find out what terminated
    /// the IIS Express worker process (the "unhandled exception" JIT debugger dialog).
    /// </summary>
    public static class ExceptionLogger
    {
        private static readonly object SyncRoot = new object();

        public static void Register()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                Log(args.ExceptionObject as Exception, "AppDomain.UnhandledException");

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                Log(args.Exception, "TaskScheduler.UnobservedTaskException");
                args.SetObserved();
            };
        }

        public static void Log(Exception exception, string source)
        {
            if (exception == null)
            {
                return;
            }

            try
            {
                var logFolder = HostingEnvironment.MapPath("~/App_Data/Logs");
                if (string.IsNullOrEmpty(logFolder))
                {
                    logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Logs");
                }

                Directory.CreateDirectory(logFolder);

                var logFile = Path.Combine(
                    logFolder,
                    string.Format(CultureInfo.InvariantCulture, "errors-{0:yyyyMMdd}.log", DateTime.Now));

                var report = new StringBuilder()
                    .AppendLine(string.Format(CultureInfo.InvariantCulture, "===== {0:yyyy-MM-dd HH:mm:ss.fff} | {1}", DateTime.Now, source))
                    .AppendLine(exception.ToString())
                    .AppendLine();

                lock (SyncRoot)
                {
                    File.AppendAllText(logFile, report.ToString(), Encoding.UTF8);
                }
            }
            catch
            {
                // Logging must never break the application.
            }
        }
    }
}
