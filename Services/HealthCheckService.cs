using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatusChecker.Interfaces;
using StatusChecker.Utils;
using System.Configuration;

namespace StatusChecker.Services
{
    public class HealthCheckService
    {
        private readonly List<IHealthCheck> _healthChecks;

        public HealthCheckService()
        {
            _healthChecks = new List<IHealthCheck>();
            InitializeHealthChecks();
        }

        private void InitializeHealthChecks()
        {
            var healthCheckUrls = ConfigurationManager.AppSettings["HealthCheckUrlsTEST"].Split(',');
            foreach (var url in healthCheckUrls)
            {
                var parts = url.Split('|');
                if (parts.Length == 3)
                {
                    _healthChecks.Add(new HttpHealthCheck(parts[0].Trim(), parts[1].Trim(), parts[2].Trim(), parts[3].Trim()));
                }
            }
        }

        public async Task PerformHealthChecksAsync()
        {
            int retryCount = 3;
            foreach (var healthCheck in _healthChecks)
            {
                try
                {
                    var result = await healthCheck.CheckHealthWithRetryAsync(retryCount);
                    Logger.Log(result.ToString());
                }
                catch (Exception ex)
                {
                    Logger.Log($"Error performing health check for {healthCheck.ServiceName}: {ex.Message}");
                }
            }
        }
    }
}
