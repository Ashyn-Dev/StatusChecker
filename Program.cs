using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace StatusChecker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Array for running multiple services, if necessary
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1() // Our main service
            };

            // Check if the application is running interactively (console mode)
            if (Environment.UserInteractive)
            {
                // Run in console mode for testing
                Console.WriteLine("Running in console mode...");

                var service1 = new Service1();
                service1.TestStartStop();  // Method to manually start and stop the service for testing
            }
            else
            {
                // Run as a Windows service
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
