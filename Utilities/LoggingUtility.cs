using StatusChecker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusChecker.Utilities
{
    public static class LoggingUtility
    {
        public static void LogHealthCheckResults(List<HealthCheckResult> results, string logFilePath = "HealthCheckLog.txt")
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
            {
                writer.WriteLine("Health Check Results: " + DateTime.Now);
                foreach (var result in results)
                {
                    writer.WriteLine($"Service: {result.ServiceName}, Environment: {result.Environment}, Status: {result.Status}");
                }
                writer.WriteLine();
            }

            Console.WriteLine("Health check results logged.");
        }
    }
}
