using System;
namespace StatusChecker.Models
{
    public class HealthCheckResult
    {
        public string ServiceName { get; set; }
        public string Environment { get; set; }
        public string Endpoint { get; set; }
        public bool Status { get; set; }  // Healthy = true, Unhealthy = false
        public string Details { get; set; }  // Additional info or error message

        // Constructor to initialize all properties
        public HealthCheckResult(string serviceName, bool status, string details, string environment, string endpoint)
        {
            ServiceName = serviceName;
            Status = status;
            Details = details;
            Environment = environment;
            Endpoint = endpoint;
        }

        // Override the ToString method for readable output
        public override string ToString()
        {
            return $"{ServiceName} is {(Status ? "Healthy" : "Unhealthy")}. Endpoint: {Endpoint} - Environment: {Environment}. Details: {Details}";
        }
    }
}
