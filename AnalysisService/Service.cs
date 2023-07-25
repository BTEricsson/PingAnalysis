using System;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;
using Business;

namespace AnalysisService
{
    public partial class Service : ServiceBase
    {
        PingNode pingNode = new PingNode();
        readonly Timer timer = new Timer();

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            pingNode.Load();

            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
        }

        private void WriteToFile(string Message)
        {
            string path = pingNode.LogPath + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = pingNode.LogPath + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        public static string AssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

    }
}
