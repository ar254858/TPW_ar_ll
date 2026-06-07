using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class ASCIILogger : ILogger
    {
        private readonly string _logFilePath;
        private readonly BlockingCollection<string> _logQueue;
        private readonly Task _loggingTask;

        private const int MaxLogFiles = 20;

        public ASCIILogger(string customLogDir = null)
        {
            _logFilePath = InitializeEnvironment();

            _logQueue = new BlockingCollection<string>();
            _loggingTask = Task.Run(ProcessQueue);
        }

        private string InitializeEnvironment()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string logDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\logs"));
            Directory.CreateDirectory(logDir);

            ApplyLogRotation(logDir);

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return Path.Combine(logDir, $"ball_log_{timestamp}.txt");
        }

        private void ApplyLogRotation(string logDir)
        {
            try
            {
                var dirInfo = new DirectoryInfo(logDir);
                var files = dirInfo.GetFiles("ball_log_*.txt").OrderBy(f => f.CreationTime).ToList();

                while (files.Count >= MaxLogFiles)
                {
                    files.First().Delete();
                    files.RemoveAt(0);
                }
            }
            catch (Exception) {}
        }

        public void LogData(object data)
        {
            string result = JsonSerializer.Serialize(data);
            if (!_logQueue.IsAddingCompleted)
            {
                string logEntry = $"[{DateTime.Now:HH:mm:ss.fff}] {result}";
                _logQueue.Add(logEntry);
            }
        }

        private void ProcessQueue()
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, append: true, System.Text.Encoding.ASCII))
            {
                foreach (var logEntry in _logQueue.GetConsumingEnumerable())
                {
                    writer.WriteLine(logEntry);
                }
            }
        }

        public void Stop()
        {
            _logQueue.CompleteAdding();
            _loggingTask.Wait();
        }
    }
}
