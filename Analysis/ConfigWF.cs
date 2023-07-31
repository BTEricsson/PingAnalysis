using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Business;

namespace Analysis
{
    public partial class ConfigWF : Form
    {
        private Config pingData = new Config();
        private BindingSource bindingSource1 = new BindingSource();

        public ConfigWF()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            LoadData();
            InitView();
        }

        private void btnTrace_Click(object sender, EventArgs e)
        {
            if (cbURL.Text == string.Empty)
                return;

            SetGridPreloadProperty();
  
            pingData.Nodes = (IList<Node>)TraceHelpers.GetTraceRoute(cbURL.Text);
            pingData.Name = cbURL.Text;

            InitView();
        }

        private void InitView()
        {
            UpdateGridSource();
            SetGridLoadFinishProperty();

            cbURL.Text = pingData.Name;

            LaHost.Text = pingData.Name;
            LaLogPath.Text = pingData.LogPath;

            if (pingData.PingTimer == 0)
                pingData.PingTimer = 5;
            CbPingTimer.Text = pingData.PingTimer.ToString();
        }

        private void UpdateGridSource()
        {
            bindingSource1.DataSource = pingData.Nodes;
            dataGridView.DataSource = bindingSource1;
        }

        private void SetGridPreloadProperty()
        {
            dataGridView.ClearSelection();
            dataGridView.ForeColor = Color.LightGray;
            dataGridView.Refresh();
        }

        private void SetGridLoadFinishProperty()
        {
            if (dataGridView.Columns.Count < 1)
                return;
            dataGridView.Columns[0].ReadOnly = false;
            dataGridView.Columns[0].Width = 50;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].ReadOnly = true;
            dataGridView.Columns[1].Width = 40;
            dataGridView.Columns[2].ReadOnly = true;
            dataGridView.Columns[3].ReadOnly = true;
            dataGridView.Columns[3].Width = 200;
            dataGridView.Columns[4].ReadOnly = false;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[5].ReadOnly = false;
            dataGridView.Columns[5].Width = 50;
            dataGridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ForeColor = Color.Black;
            dataGridView.Refresh();
        }

        private void LoadData()
        {  
            pingData.Load();

            if (pingData.LogPath == null || pingData.LogPath == string.Empty)
                pingData.LogPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            pingData.PingTimer = int.Parse(CbPingTimer.Text);
            pingData.SaveOrUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = pingData.LogPath;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pingData.LogPath = folderBrowserDialog1.SelectedPath;
                LaLogPath.Text = pingData.LogPath;
            }
        }
    }
}
