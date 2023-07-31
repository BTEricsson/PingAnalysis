using System;
using System.Windows.Forms;
using Business;
using System.IO;
using System.Linq;

namespace Analysis
{
    public partial class PingAnalysis :  Form
    {
        PingConfig pingNode = new PingConfig();

        public PingAnalysis()
        {
            InitializeComponent();
            pingNode.Load();

            SetTimer();

            UpdatePingLog();
            UpdateServiceStatus();   
        }

        private void BtnServiceAdmin_Click(object sender, EventArgs e)
        {
            ServiceAdmin ServiceAdmin = new ServiceAdmin();
            ServiceAdmin.Show();
        }

        private void BtnPingConfig_Click(object sender, EventArgs e)
        {
            ConfigWF Trace = new ConfigWF();
            Trace.Show();
        }

        void SetTimer()
        {
            timer1.Tick += UpdateTime;
            timer1.Interval = 10000;
            timer1.Enabled = true;
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
