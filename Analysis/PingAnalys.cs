using System;
using System.Windows.Forms;
using Business;
using System.IO;
using System.Linq;
using Analysis.Extentions;

namespace Analysis
{
    public partial class PingAnalysis :  Form
    {
        readonly Config config = new Config();

        public PingAnalysis()
        {
            InitializeComponent();
        }

        private void PingAnalysis_Load(object sender, EventArgs e)
        {
            config.Load();
            SetTimer();
            UpdateRitchTextBoxWithPingLog();
            UpdateServiceStatus();
        }

        #region Buttons
        private void BtnServiceAdmin_Click(object sender, EventArgs e)
        {
            ServiceAdmin ServiceAdmin = new ServiceAdmin();
            ServiceAdmin.Show();
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            ConfigWF Trace = new ConfigWF();
            Trace.Show();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Timer
        void SetTimer()
        {
            timer1.Tick += onTickEvent;
            timer1.Interval = 10000;
            timer1.Enabled = true;
        }

        private void onTickEvent(object sender, EventArgs e)
        {
            UpdateRitchTextBoxWithPingLog();
            UpdateServiceStatus();
        }

        #endregion Timer
        
        private void UpdateServiceStatus()
        {
            string serviceStatus = ServiceHelper.GetServiceStatus(Environment.MachineName);
            if (serviceStatus == string.Empty)
                serviceStatus = "not installed";
            LaServiceStatus.Text = serviceStatus;
            LaServiceStatus.ForeColor = serviceStatus.GetStatusColor();
        }

        private void UpdateRitchTextBoxWithPingLog()
        {
            if(!File.Exists(config.LogPathAndFile))
            {
                RtbPingView.AddTexLineWithStatusColor("No data!");
                return;
            }

            int linesToView = 20;
            var allReadLines = File.ReadAllLines(config.LogPathAndFile).ToList();
            int allLines = allReadLines.Count;
            int startLine = allLines <= linesToView ? 0 : allLines - linesToView; 

            var selectedLinesToView = allReadLines.GetRange(startLine, allLines - startLine);

            RtbPingView.Text = string.Empty;
            foreach(var line in selectedLinesToView)
            {
                RtbPingView.AddTexLineWithStatusColor(line);
            }
        }
    }
}
