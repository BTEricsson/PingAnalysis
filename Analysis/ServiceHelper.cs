using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Analysis
{
    public class ServiceHelper
    {
        static readonly string ServiceName = AnalysisService.Service.GetServiceName;

        public static void StartService(int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(ServiceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message: " + ex.Message.ToString() + Environment.NewLine + "InnerExeption " + ex.InnerException.ToString());
            }
        }

        public static void StopService(int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(ServiceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string GetServiceStatus(string machineName)
        {
            ServiceController controller = null;

            try
            {
                controller = new ServiceController(ServiceName, machineName);
                return controller.Status.ToString();
            }
            catch (InvalidOperationException Ex)
            {
                return string.Empty;
            }
            finally
            {
                if (controller != null)
                    controller.Dispose();
            }
        }
    }
}