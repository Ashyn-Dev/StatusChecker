using Newtonsoft.Json;
using StatusChecker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusChecker.Utilities
{
    public static class ConfigLoader
    {
        public static ServiceConfig LoadConfig(string filePath)
        {
            // Use the base directory of the application (either for development or after deployment)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Dynamically construct the path to the Config folder
            string configPath = Path.Combine(baseDirectory, "..", "..", "Config", "ServiceConfig.json");

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"The configuration file was not found at {configPath}");
            }

            // Load and parse the config file
            var jsonContent = File.ReadAllText(configPath);
            var config = JsonConvert.DeserializeObject<ServiceConfig>(jsonContent);
            return config;
        }
        //{
        //    // Use the base directory of the application instead of a hardcoded path
        //    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    string configPath = Path.Combine(baseDirectory, "src", "Config", "ServiceConfig.json");

        //    // Load and parse the config file
        //    var jsonContent = File.ReadAllText(configPath);
        //    var config = JsonConvert.DeserializeObject<ServiceConfig>(jsonContent);
        //    return config;
        //}
    }
}