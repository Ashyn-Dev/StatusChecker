using StatusChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusChecker.Utilities
{
    public static class DynamicSoapRequestBuilder
    {
        public static string BuildSoapRequest(Service service)
        {
            StringBuilder soapEnvelope = new StringBuilder();

            soapEnvelope.AppendLine("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            soapEnvelope.AppendLine("  <s:Body>");
            soapEnvelope.AppendLine($"    <{service.SoapAction.Split('/').Last()} xmlns=\"http://tempuri.org/\">");

            // Add dynamic parameters
            foreach (var param in service.Parameters)
            {
                soapEnvelope.AppendLine($"      <{param.Key}>{param.Value}</{param.Key}>");
            }

            soapEnvelope.AppendLine("    </Add>");
            soapEnvelope.AppendLine("  </s:Body>");
            soapEnvelope.AppendLine("</s:Envelope>");
            Console.WriteLine(soapEnvelope.ToString());

            return soapEnvelope.ToString();
        }
    }
}
