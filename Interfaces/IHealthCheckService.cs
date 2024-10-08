using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusChecker.Interfaces
{
    // Interface for health check service
    public interface IHealthCheckService
    {
        bool CheckServiceHealth(); // Simple health check method
    }

    // Enum for different environments
    public enum EnvironmentType
    {
        SIT,
        UAT,
        PROD
    }

    // Interface for environment setup and checking
    public interface IEnvironment
    {
        string Name { get; }
        string EndpointUrl { get; }
        IHealthCheckService HealthCheckService { get; }
    }
}
