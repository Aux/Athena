using System;
using System.IO;
using System.Threading.Tasks;

namespace Pugster
{
    public class BaseLoggingService
    {
        private string _logDirectory { get; }
        private string _logFile => Path.Combine(_logDirectory, $"{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt");

        public BaseLoggingService()
        {
            _logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
        }

        public Task LogAsync(object severity, string source, string message)
        {
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);
            if (!File.Exists(_logFile))
                File.Create(_logFile).Dispose();

            string logText = $"{DateTime.UtcNow.ToString("hh:mm:ss")} [{severity}] {source}: {message}";
            File.AppendAllText(_logFile, logText + "\n");

            return Console.Out.WriteLineAsync(logText);
        }
    }
}
