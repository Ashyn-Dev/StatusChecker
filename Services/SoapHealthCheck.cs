using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using StatusChecker.Models;
using System.Collections.Generic;

namespace StatusChecker.Services
{
    public class SoapHealthCheck
    {
        private readonly string _serviceName;
        private readonly string _url;
        private readonly string _environment;
        private readonly string _soapAction;
        private readonly Dictionary<string, string> _parameters;

        public SoapHealthCheck(string serviceName, string url, string environment, string soapAction, Dictionary<string, string> parameters)
        {
            _serviceName = serviceName;
            _url = url;
            _environment = environment;
            _soapAction = soapAction;
            _parameters = parameters;
        }

        public async Task<HealthCheckResult> CheckHealthAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var requestContent = BuildSoapRequest();
                    var request = new HttpRequestMessage(HttpMethod.Post, _url)
                    {
                        Content = new StringContent(requestContent, Encoding.UTF8, "text/xml")
                    };
                    request.Headers.Add("SOAPAction", _soapAction);

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return new HealthCheckResult(_serviceName, true, $"Status Code: {response.StatusCode}", _environment, _url);
                    }
                    else
                    {
                        return new HealthCheckResult(_serviceName, false, $"Error: {response.StatusCode} - {response.ReasonPhrase}", _environment, _url);
                    }
                }
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(_serviceName, false, $"Error: {ex.Message}", _environment, _url);
            }
        }

        private string BuildSoapRequest()
        {
            // Building SOAP envelope dynamically
            var soapEnvelope = new StringBuilder();
            soapEnvelope.AppendLine("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            soapEnvelope.AppendLine("  <s:Body>");
            soapEnvelope.AppendLine($"    <Add xmlns=\"http://tempuri.org/\">");

            foreach (var param in _parameters)
            {
                soapEnvelope.AppendLine($"      <{param.Key}>{param.Value}</{param.Key}>");
            }

            soapEnvelope.AppendLine("    </Add>");
            soapEnvelope.AppendLine("  </s:Body>");
            soapEnvelope.AppendLine("</s:Envelope>");

            return soapEnvelope.ToString();
        }
    }
}
