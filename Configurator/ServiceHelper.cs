using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Analysis
{
    public class ServiceHelper
    {

        public static void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
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

        public static void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
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

        public static string GetServiceStatus(string name, string machineName)
        {
            ServiceController controller = null;

            try
            {
                controller = new ServiceController(name, machineName);
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