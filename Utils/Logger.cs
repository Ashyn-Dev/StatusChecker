using System;
using System.IO;

namespace StatusChecker.Utils
{
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "healthcheck.log");

        public static void Log(string message)
        {
            // Implement logging logic here
            Console.WriteLine(message);
            // log to a file
            if (!Directory.Exists(Path.GetDirectoryName(LogFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
            }
            File.AppendAllText(LogFilePath, message);
            Console.WriteLine("Logged to file: " + LogFilePath);
            // Create it if it doesn't exist
        }

        internal static void Log(object value)
        {
            throw new NotImplementedException();
        }
    }
}