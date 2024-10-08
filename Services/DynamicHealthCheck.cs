using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using StatusChecker.Models;

namespace StatusChecker.Services
{
    public class DynamicHealthCheck
    {
        private readonly ServiceConfig _config;

        public DynamicHealthCheck(ServiceConfig config)
        {
            _config = config;
        }

        //public async Task CheckAllServices(List<HealthCheckResult> healthCheckResults)
        //{
        //    foreach (var service in _config.Services)
        //    {
        //        bool isHealthy = false;
        //        string details = string.Empty;

        //        try
        //        {
        //            // Check if service is HTTP or SOAP based on the "Type" property
        //            if (service.Type == "SOAP")
        //            {
        //                // SOAP health check
        //                var soapCheck = new SoapHealthCheck(service.Name, service.Endpoint, service.Environment, service.SoapAction, service.Parameters);
        //                var result = await soapCheck.CheckHealthAsync();
        //                isHealthy = result.Status;
        //                details = result.Details;
        //            }
        //            else if (service.Type == "HTTP")
        //            {
        //                // HTTP health check
        //                var httpCheck = new HttpHealthCheck(service.Name, service.Endpoint, service.Type, service.Environment);
        //                var result = await httpCheck.CheckHealthWithRetryAsync();
        //                isHealthy = result.Status;
        //                details = result.Details;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            details = $"Error: {ex.Message}";
        //        }

        //        // Store the result
        //        healthCheckResults.Add(new HealthCheckResult(
        //            service.Name,
        //            isHealthy,
        //            details,
        //            service.Environment,
        //            service.Endpoint
        //        ));
        //    }
        //}
        //public async Task CheckAllServices(List<HealthCheckResult> healthCheckResults)
        //{
        //    foreach (var service in _config.Services)
        //    {
        //        bool isHealthy = false;
        //        string details = string.Empty;
        //        string environment = service.Environment;

        //        try
        //        {
        //            // Perform the health check for each service
        //            if (service.Type == "SOAP")
        //            {
        //                var soapCheck = new SoapHealthCheck(service.Name, service.Endpoint, service.Environment, service.SoapAction, service.Parameters);
        //                var result = await soapCheck.CheckHealthAsync();
        //                isHealthy = result.Status;
        //                details = result.Details;
        //            }
        //            else if (service.Type == "HTTP")
        //            {
        //                var httpCheck = new HttpHealthCheck(service.Name, service.Endpoint, service.Type, service.Environment);
        //                var result = await httpCheck.CheckHealthAsync();

        //                // Check if the HTTP response was successful
        //                if (result.Status)
        //                {
        //                    isHealthy = true;
        //                    details = result.Details;
        //                }
        //                else
        //                {
        //                    isHealthy = false;
        //                    details = $"HTTP check failed. Details: {result.Details}";
        //                }
        //            }
        //        }
        //        catch (HttpRequestException ex)
        //        {
        //            // Catch specific HTTP-related exceptions
        //            details = $"HTTP Request Error: {ex.Message}";
        //        }
        //        catch (Exception ex)
        //        {
        //            // Catch all other errors but continue to the next service
        //            details = $"General Error: {ex.Message}";
        //        }

        //        // Log or store the result for this specific service
        //        healthCheckResults.Add(new HealthCheckResult(
        //            service.Name,
        //            isHealthy,
        //            details,
        //            service.Environment,
        //            service.Endpoint
        //        ));
        //    }
        //}




        //public async Task CheckAllServices(List<HealthCheckResult> healthCheckResults)
        //{
        //    foreach (var service in _config.Services)
        //    {
        //        bool isHealthy = false;
        //        string details = string.Empty;
        //        string environment = service.Environment;

        //        try
        //        {
        //            // Perform the health check for each service
        //            if (service.Type == "SOAP")
        //            {
        //                var soapCheck = new SoapHealthCheck(service.Name, service.Endpoint, service.Environment, service.SoapAction, service.Parameters);
        //                var result = await soapCheck.CheckHealthAsync();
        //                isHealthy = result.Status;
        //                details = result.Details;
        //            }
        //            else if (service.Type == "HTTP")
        //            {
        //                // Create a new HttpHealthCheck for each service to avoid shared state
        //                var httpCheck = new HttpHealthCheck(service.Name, service.Endpoint, service.Type, service.Environment);
        //                var result = await httpCheck.CheckHealthAsync();
        //                isHealthy = result.Status;
        //                details = result.Details;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log exception details for individual services
        //            details = $"Service check failed for {service.Name} in {service.Environment}: {ex.Message}";
        //        }

        //        // Log or store the result for this specific service
        //        healthCheckResults.Add(new HealthCheckResult(
        //            service.Name,
        //            isHealthy,
        //            details,
        //            service.Environment,
        //            service.Endpoint
        //        ));
        //    }
        //}



        public async Task CheckAllServices(List<HealthCheckResult> healthCheckResults)
        {
            foreach (var service in _config.Services)
            {
                bool isHealthy = false;
                string details = string.Empty;
                string environment = service.Environment;

                try
                {
                    Console.WriteLine($"Starting health check for {service.Name} in {service.Environment}...");

                    // Perform the health check for each service
                    if (service.Type == "SOAP")
                    {
                        var soapCheck = new SoapHealthCheck(service.Name, service.Endpoint, service.Environment, service.SoapAction, service.Parameters);
                        var result = await soapCheck.CheckHealthAsync();
                        isHealthy = result.Status;
                        details = result.Details;
                    }
                    else if (service.Type == "HTTP")
                    {
                        var httpCheck = new HttpHealthCheck(service.Name, service.Endpoint, service.Type, service.Environment);
                        var result = await httpCheck.CheckHealthAsync();
                        isHealthy = result.Status;
                        details = result.Details;
                    }

                    Console.WriteLine($"Completed health check for {service.Name} in {service.Environment}: Status = {isHealthy}");
                }
                catch (Exception ex)
                {
                    // Log exception details for individual services
                    details = $"Service check failed for {service.Name} in {service.Environment}: {ex.Message}";
                    Console.WriteLine(details);
                }

                // Log or store the result for this specific service
                healthCheckResults.Add(new HealthCheckResult(
                    service.Name,
                    isHealthy,
                    details,
                    service.Environment,
                    service.Endpoint
                ));
            }
        }







    }
}
