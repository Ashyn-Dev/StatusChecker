using System;
using System.Net.Http;
using System.Threading.Tasks;
using StatusChecker.Models;
using StatusChecker.Interfaces;

namespace StatusChecker.Services
{
    public class HttpHealthCheck : IHealthCheck
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public string ServiceName { get; }
        public string ServiceType { get; }
        public string Environment { get; }

        public HttpHealthCheck(string serviceName, string url, string serviceType, string environment)
        {
            ServiceName = serviceName;
            ServiceType = serviceType;
            Environment = environment;
            _url = url;
            _client = new HttpClient();
        }

        public async Task<HealthCheckResult> CheckHealthWithRetryAsync(int retryCount = 3)
        {
            for (int attempt = 0; attempt < retryCount; attempt++)
            {
                try
                {
                    var response = await _client.GetAsync(_url);
                    if (response.IsSuccessStatusCode)
                    {
                        return new HealthCheckResult(
                            ServiceName,
                            true,
                            $"Status Code: {response.StatusCode}, URL: {_url}",
                            Environment,
                            _url
                        );
                    }
                    else
                    {
                        return new HealthCheckResult(
                            ServiceName,
                            false,
                            $"Error: {response.StatusCode} {response.ReasonPhrase}",
                            Environment,
                            _url
                        );
                    }
                }
                catch (Exception ex)
                {
                    // On last attempt, log the failure
                    if (attempt == retryCount - 1)
                    {
                        return new HealthCheckResult(
                            ServiceName,
                            false,
                            $"Error after {retryCount} attempts: {ex.Message}",
                            Environment,
                            _url
                        );
                    }
                    // Optionally add a delay between retries
                    await Task.Delay(2000); // Wait 2 seconds before retry
                }
            }
            // If all retries fail, return a failure
            return new HealthCheckResult(ServiceName, false, "Health check failed after retries", Environment, _url);
        }

        //public async Task<HealthCheckResult> CheckHealthAsync()
        //{
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            // Optional: Increase timeout for long-running requests
        //            client.Timeout = TimeSpan.FromSeconds(30);

        //            var response = await client.GetAsync(_url);

        //            // Check if the response status code indicates success (2xx)
        //            if (response.IsSuccessStatusCode)
        //            {
        //                return new HealthCheckResult(
        //                    ServiceName,
        //                    true,
        //                    $"Success: Status Code: {response.StatusCode}, URL: {_url}",
        //                    Environment,
        //                    _url
        //                );
        //            }
        //            else
        //            {
        //                // Handle non-success status codes, like 404, 500, etc.
        //                return new HealthCheckResult(
        //                    ServiceName,
        //                    false,
        //                    $"HTTP Error: {response.StatusCode} - {response.ReasonPhrase}. URL: {_url}",
        //                    Environment,
        //                    _url
        //                );
        //            }
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        // Handle HTTP request exceptions
        //        return new HealthCheckResult(
        //            ServiceName,
        //            false,
        //            $"HttpRequestException: {ex.Message}. URL: {_url}",
        //            Environment,
        //            _url
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        // Catch any other unexpected exceptions
        //        return new HealthCheckResult(
        //            ServiceName,
        //            false,
        //            $"General Exception: {ex.Message}. URL: {_url}",
        //            Environment,
        //            _url
        //        );
        //    }
        //}





        public async Task<HealthCheckResult> CheckHealthAsync()
        {
            try
            {
                using (var client = new HttpClient())  // Independent HttpClient for each check
                {
                    // Optional: Increase timeout for long-running requests
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var response = await client.GetAsync(_url);

                    // Check if the response status code indicates success (2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        return new HealthCheckResult(
                            ServiceName,
                            true,
                            $"Success: Status Code: {response.StatusCode}, URL: {_url}",
                            Environment,
                            _url
                        );
                    }
                    else
                    {
                        // Handle non-success status codes, like 404, 500, etc.
                        return new HealthCheckResult(
                            ServiceName,
                            false,
                            $"HTTP Error: {response.StatusCode} - {response.ReasonPhrase}. URL: {_url}",
                            Environment,
                            _url
                        );
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exceptions
                return new HealthCheckResult(
                    ServiceName,
                    false,
                    $"HttpRequestException: {ex.Message}. URL: {_url}",
                    Environment,
                    _url
                );
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions
                return new HealthCheckResult(
                    ServiceName,
                    false,
                    $"General Exception: {ex.Message}. URL: {_url}",
                    Environment,
                    _url
                );
            }
        }









    }
}
