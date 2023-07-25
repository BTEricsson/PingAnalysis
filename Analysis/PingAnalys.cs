using System;
using System.Windows.Forms;

namespace Analysis
{
    public partial class PingAnalysis :  Form
    {
        public PingAnalysis()
        {
            InitializeComponent();
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
    }
}
