using System;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;

namespace AnalysisService
{
    public partial class Service : ServiceBase
    {
        readonly Timer timer = new Timer();
        readonly PingHelper PingHelper = new PingHelper();

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            PingHelper.InitPingNode();

            LogFileBase.WriteToFile(PingHelper.GetLogPath, $"------------------------------------------------------");
            LogFileBase.WriteToFile(PingHelper.GetLogPath, $"Service is started at " + DateTime.Now);

            SetTimer();

            PingHelper.PingHost();
        }

        protected override void OnStop()
        {
            LogFileBase.WriteToFile(PingHelper.GetLogPath, $"Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            PingHelper.PingHost();
        }

        private void SetTimer()
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000 * PingHelper.GetTimer;
            timer.Enabled = true;
        }

        public static string AssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location;
        }
    }
}
