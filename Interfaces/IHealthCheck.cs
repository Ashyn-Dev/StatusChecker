using System.Threading.Tasks;
using StatusChecker.Models;

namespace StatusChecker.Interfaces
{
    public interface IHealthCheck
    {
        string ServiceName { get; }
        Task<HealthCheckResult> CheckHealthWithRetryAsync(int retryCount);
    }
}