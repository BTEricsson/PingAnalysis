using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Business;

namespace Analysis
{
    public partial class ConfigWF : Form
    {
        private Config Config = new Config();
        private BindingSource bindingSource = new BindingSource();

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

            Config.Nodes = TraceHelpers.GetTraceRoute(cbURL.Text);
            Config.Name = cbURL.Text;

            InitView();
        }

        private void InitView()
        {
            UpdateGridSource();
            SetGridLoadFinishProperty();

            cbURL.Text = Config.Name;

            LaHost.Text = Config.Name;
            LaLogPath.Text = Config.LogPath;

            InitCBPingTimer();
        }



        private void InitCBPingTimer()
        {
            var PingTimer = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("1 min", 60000),
                new KeyValuePair<string, int>("5 min", 60000*5),
                new KeyValuePair<string, int>("10 min", 60000*10),
                new KeyValuePair<string, int>("15 min", 60000*15),
                new KeyValuePair<string, int>("30 min", 60000*30),
                new KeyValuePair<string, int>("60 min", 60000*60)
            };

            if (Config.PingTimer == 0)
                Config.PingTimer = 60000 * 5;

            CbPingTimer.DataSource = PingTimer;
            CbPingTimer.ValueMember = "value";
            CbPingTimer.DisplayMember = "key";
            CbPingTimer.SelectedValue = Config.PingTimer;
        }

        private void UpdateGridSource()
        {
            bindingSource.DataSource = Config.Nodes;
            dataGridView.DataSource = bindingSource;
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
            Config.Load();

            if (Config.LogPath == null || Config.LogPath == string.Empty)
                Config.LogPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Config.PingTimer = (int)CbPingTimer.SelectedValue;
            Config.SaveOrUpdate();
        }

        private void BtnLogPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Config.LogPath;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Config.LogPath = folderBrowserDialog1.SelectedPath;
                LaLogPath.Text = Config.LogPath;
            }
        }
    }
}
