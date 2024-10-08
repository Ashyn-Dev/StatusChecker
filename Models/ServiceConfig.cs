using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace StatusChecker.Models
//{
//    public class ServiceConfig
//    {
//        public List<Service> Services { get; set; }
//    }

//    public class Service
//    {
//        public string Name { get; set; }
//        public string Endpoint { get; set; }
//        public string Environment { get; set; }
//        public string SoapAction { get; set; }
//        public Dictionary<string, string> Parameters { get; set; } // Dynamic params
//    }

//}
namespace StatusChecker.Models
{
    public class Service
    {
        public string Name { get; set; }  // Service Name
        public string Endpoint { get; set; }  // Service URL
        public string Environment { get; set; }  // Environment (SIT, UAT, PROD)
        public string Type { get; set; }  // "HTTP" or "SOAP"
        public string SoapAction { get; set; }  // SOAP Action (if applicable)
        public Dictionary<string, string> Parameters { get; set; }  // SOAP Parameters (if applicable)
    }

    public class ServiceConfig
    {
        public List<Service> Services { get; set; }  // List of services to check
    }
}
