using System.Runtime.CompilerServices;

namespace EmployeeManagement.Tools
{
    public interface IWebAppLogger
    {
        void LogAudit(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string path = null,
            [CallerMemberName] string caller = null);

        void LogInfo(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string path = null,
            [CallerMemberName] string caller = null);

        void LogWarning(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string path = null,
            [CallerMemberName] string caller = null);

        void LogError(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string path = null,
            [CallerMemberName] string caller = null);
    }

    public class WebAppLogger : IWebAppLogger
    {
        public void LogAudit(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string path = null,
            [CallerMemberName] string caller = null)
        {
            Console.WriteLine(
                $"{DateTime.UtcNow.ToString("O")} | Audit | {caller}(){Environment.NewLine}{message}");
        }

        public void LogInfo(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string path = null,
            [CallerMemberName] string caller = null)
        {
            Console.WriteLine(
                $"{DateTime.UtcNow.ToString("O")} | Info | TID: {Thread.CurrentThread.ManagedThreadId} | {Path.GetFileNameWithoutExtension(path)} | {caller}() | Line: {lineNumber} | {message}");
        }

        public void LogWarning(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string path = null,
            [CallerMemberName] string caller = null)
        {
            Console.WriteLine(
                $"{DateTime.UtcNow.ToString("O")} | Warning | TID: {Thread.CurrentThread.ManagedThreadId} | {Path.GetFileNameWithoutExtension(path)} | {caller}() | Line: {lineNumber} | {message}");
        }

        public void LogError(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string path = null,
            [CallerMemberName] string caller = null)
        {
            Console.WriteLine(
                $"{DateTime.UtcNow.ToString("O")} | Error | TID: {Thread.CurrentThread.ManagedThreadId} | {Path.GetFileNameWithoutExtension(path)} | {caller}() | Line: {lineNumber} | {message}");
        }
    }
}
