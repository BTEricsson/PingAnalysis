using System;
using System.Windows.Forms;
using Business;
using System.IO;
using System.Linq;
using Analysis.Extentions;
using System.Drawing;

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
            UpdatePingLog();
            UpdateServiceStatus();
        }

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
            string serviceStatus = ServiceHelper.GetServiceStatus(Environment.MachineName);
            if (serviceStatus == string.Empty)
                serviceStatus = "not installed";
            LaServiceStatus.Text = serviceStatus;
            LaServiceStatus.ForeColor = serviceStatus.GetStatusColor();
        }

        private void UpdatePingLog()
        {
            var allLines = File.ReadAllLines(config.LogPathAndFile).ToList();
            var LinesToView = 15;
            var startIndex = allLines.Count <= LinesToView ? 0 : allLines.Count - LinesToView; 
            var newLines = allLines.GetRange(startIndex, allLines.Count - startIndex);

            RtbPingLog.Text = string.Empty;
            foreach(var line in newLines)
            {
                SetTextAndColor(RtbPingLog,line);
            }
        }

        public void SetTextAndColor(RichTextBox Rtb,string TextLine)
        {
            string Text = TextLine + Environment.NewLine;

            int StartPos = Rtb.TextLength;
            int Length = Text.Length;
            
            Rtb.SelectionColor = Text.GetLineColor();
            Rtb.SelectionStart = StartPos;
            Rtb.SelectionLength = Length;

            Rtb.AppendText(Text);
        }

    }
}
