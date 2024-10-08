using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace StatusChecker
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer

    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;

        public ProjectInstaller()
        {
            // Service Process Installer
            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            // Service Installer
            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "HealthCheckService",
                DisplayName = "Health Check Service",
                Description = "A service to perform periodic health checks.",
                StartType = ServiceStartMode.Automatic
            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
