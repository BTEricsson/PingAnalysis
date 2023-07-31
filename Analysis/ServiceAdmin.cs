using System;
using System.Configuration.Install;
using System.Security.Principal;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Analysis
{
    public partial class ServiceAdmin : Form
    {
        static readonly int Timeout = 10000;
        private bool serviceInstalled;

        public ServiceAdmin()
        {
            InitializeComponent();
            SetButtonStatus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ServiceHelper.StartService(Timeout);
            SetButtonStatus();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ServiceHelper.StopService(Timeout);
            SetButtonStatus();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            string serviceStatus = ServiceHelper.GetServiceStatus(Environment.MachineName);
            string serviceLocation = AnalysisService.Service.AssemblyLocation();

            if (serviceStatus == string.Empty)
                ManagedInstallerClass.InstallHelper(new string[] { serviceLocation });

            SetButtonStatus();
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            string serviceStatus = ServiceHelper.GetServiceStatus(Environment.MachineName);
            string serviceLocation = AnalysisService.Service.AssemblyLocation();

            if (serviceStatus != string.Empty)
                ManagedInstallerClass.InstallHelper(new string[] { "/u", serviceLocation });

            SetButtonStatus();
        }

        private void SetButtonStatus()
        {
            string serviceStatus = ServiceHelper.GetServiceStatus(Environment.MachineName);
            serviceInstalled = serviceStatus != string.Empty;
            
            if (serviceStatus == string.Empty)
                serviceStatus = "Service not installed";
            LaServiceStatus.Text = serviceStatus;

            bool ServiceRunning = serviceInstalled && 
                serviceStatus == ServiceControllerStatus.Running.ToString();
            
            bool ServiceStopped = serviceInstalled && 
                serviceStatus == ServiceControllerStatus.Stopped.ToString();
           
            btnUninstall.Enabled = serviceInstalled && IsUserAdmin();
            btnInstall.Enabled = !serviceInstalled && IsUserAdmin();
            btnStart.Enabled = ServiceStopped && IsUserAdmin();
            btnStop.Enabled = ServiceRunning && IsUserAdmin();

            if(IsUserAdmin())
                LaUserStatus.Visible = false;
        }

        private bool IsUserAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
