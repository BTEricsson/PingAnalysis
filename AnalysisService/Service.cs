using System;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;

namespace AnalysisService
{
    public partial class Service : ServiceBase
    {
        readonly Timer timer = new Timer();
        readonly PingHelper serviceHelper = new PingHelper();

        public static string GetServiceName = "PingAnalysisService";

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            serviceHelper.InitPingNode();

            LogFileBase.WriteToFile(serviceHelper.GetLogPathAndFile, $"------------------------------------------------------");
            LogFileBase.WriteToFile(serviceHelper.GetLogPathAndFile, $"Service is started at " + DateTime.Now);

            SetTimer();
            serviceHelper.PingHost();

        }

        protected override void OnStop()
        {
            LogFileBase.WriteToFile(serviceHelper.GetLogPathAndFile, $"Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            serviceHelper.PingHost();
        }

        private void SetTimer()
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000 * serviceHelper.GetTimer;
            timer.Enabled = true;
        }

        public static string AssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location;
        }
    }
}
