using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using Newtonsoft.Json;
using StatusChecker.Models;
using StatusChecker.Services;
using StatusChecker.Utilities;
using StatusChecker.Utils;

namespace StatusChecker
{
    public partial class Service1 : ServiceBase
    {
        private Timer _timer;
        private ServiceConfig _serviceConfig; // Cache the configuration

        public Service1()
        {
            this.ServiceName = "StatusChecker";
        }
        public void TestStartStop()
        {
            Console.WriteLine("Service is starting...");
            OnStart(null);

            // Simulate periodic checks
            Console.WriteLine("Press any key to stop the service...");
            Console.ReadKey();

            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("StatusChecker service started.");

            // Load the config once on service start
            _serviceConfig = ConfigLoader.LoadConfig("src/Config/ServiceConfig.json");

            // Set up a timer to trigger health checks every 60 seconds
            _timer = new Timer(CheckHealth, null, 0, 10000);
        }


        protected override void OnStop()
        {
            Console.WriteLine("StatusChecker service stopped.");
            _timer?.Dispose();
        }

        private void CheckHealth(object state)
        {
            try
            {
                var healthCheckResults = new List<HealthCheckResult>();
                var healthCheck = new DynamicHealthCheck(_serviceConfig);
                var task = healthCheck.CheckAllServices(healthCheckResults);
                task.Wait();

                var customOrder = new[] { "SIT", "UAT", "PROD" };
                var orderedResults = healthCheckResults.OrderBy(r => Array.IndexOf(customOrder, r.Environment)).ToList();

                // Use the logging utility to log results
                LoggingUtility.LogHealthCheckResults(orderedResults);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in health check: {ex.Message}");
            }
        }
    }
}
