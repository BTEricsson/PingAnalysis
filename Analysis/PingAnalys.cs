using System;
using System.Windows.Forms;
using System.Timers;
using Business;
using System.IO;
using System.Linq;

namespace Analysis
{
    public partial class PingAnalysis :  Form
    {
        PingNode pingNode = new PingNode();

        public PingAnalysis()
        {
            InitializeComponent();
            pingNode.Load();

            timer1.Tick += UpdateTime;
            timer1.Interval = 10000;
            timer1.Enabled = true;

            UpdatePingLog();
            UpdateServiceStatus();
        }

        private void btnServiceAdmin_Click(object sender, EventArgs e)
        {
            ServiceAdmin ServiceAdmin = new ServiceAdmin();
            ServiceAdmin.Show();
        }

        private void btnTraceEdit_Click(object sender, EventArgs e)
        {
            Config Trace = new Config();
            Trace.Show();
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            UpdatePingLog();
            UpdateServiceStatus();
        }

        private void UpdateServiceStatus()
        {
            string ServiceName = "PingAnalysisService";
            string serviceStatus = ServiceHelper.GetServiceStatus(ServiceName, Environment.MachineName);
            if (serviceStatus == string.Empty)
                serviceStatus = "not installed";
            LaServiceStatus.Text = serviceStatus;
        }

        private void UpdatePingLog()
        {
            string filepath = pingNode.LogPath + "\\Logs\\PingLog_" + DateTime.Now.Date.ToString("yyyy-MM").Replace('/', '_') + ".txt";
            var allLines = File.ReadAllLines(filepath).ToList();

            var LinesToView = 15;
            var startIndex = allLines.Count <= LinesToView ? 0 : allLines.Count - LinesToView; 
            var newLines = allLines.GetRange(startIndex, allLines.Count - startIndex);

            RtbPingLog.Text = string.Empty;
            foreach(var line in newLines)
            {
                RtbPingLog.Text += line + Environment.NewLine;
            }
        }

    }
}
