namespace Analysis
{
    partial class Config
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbURL = new System.Windows.Forms.ComboBox();
            this.btnGetTraceRoutes = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.LaHost = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LaLogPath = new System.Windows.Forms.Label();
            this.btnLogPath = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.CbPingTimer = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cbURL
            // 
            this.cbURL.FormattingEnabled = true;
            this.cbURL.Items.AddRange(new object[] {
            "Google.se",
            "Google.com"});
            this.cbURL.Location = new System.Drawing.Point(47, 316);
            this.cbURL.Name = "cbURL";
            this.cbURL.Size = new System.Drawing.Size(634, 21);
            this.cbURL.TabIndex = 6;
            // 
            // btnGetTraceRoutes
            // 
            this.btnGetTraceRoutes.Location = new System.Drawing.Point(693, 314);
            this.btnGetTraceRoutes.Name = "btnGetTraceRoutes";
            this.btnGetTraceRoutes.Size = new System.Drawing.Size(95, 23);
            this.btnGetTraceRoutes.TabIndex = 5;
            this.btnGetTraceRoutes.Text = "Get Routes";
            this.btnGetTraceRoutes.UseVisualStyleBackColor = true;
            this.btnGetTraceRoutes.Click += new System.EventHandler(this.btnTrace_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(693, 423);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 28);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(776, 275);
            this.dataGridView.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Routes to";
            // 
            // LaHost
            // 
            this.LaHost.AutoSize = true;
            this.LaHost.Location = new System.Drawing.Point(77, 12);
            this.LaHost.Name = "LaHost";
            this.LaHost.Size = new System.Drawing.Size(86, 13);
            this.LaHost.TabIndex = 14;
            this.LaHost.Text = "Selected Host ...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Host";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Log path:";
            // 
            // LaLogPath
            // 
            this.LaLogPath.AutoSize = true;
            this.LaLogPath.Location = new System.Drawing.Point(77, 350);
            this.LaLogPath.Name = "LaLogPath";
            this.LaLogPath.Size = new System.Drawing.Size(54, 13);
            this.LaLogPath.TabIndex = 17;
            this.LaLogPath.Text = "logpath....";
            // 
            // btnLogPath
            // 
            this.btnLogPath.Location = new System.Drawing.Point(12, 375);
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.Size = new System.Drawing.Size(151, 23);
            this.btnLogPath.TabIndex = 18;
            this.btnLogPath.Text = "Select Path";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Success Timer";
            // 
            // CbPingTimer
            // 
            this.CbPingTimer.FormattingEnabled = true;
            this.CbPingTimer.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "30",
            "60"});
            this.CbPingTimer.Location = new System.Drawing.Point(560, 365);
            this.CbPingTimer.Name = "CbPingTimer";
            this.CbPingTimer.Size = new System.Drawing.Size(121, 21);
            this.CbPingTimer.TabIndex = 21;
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 458);
            this.Controls.Add(this.CbPingTimer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnLogPath);
            this.Controls.Add(this.LaLogPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LaHost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbURL);
            this.Controls.Add(this.btnGetTraceRoutes);
            this.Name = "Config";
            this.Text = "Ping Config";
            this.Load += new System.EventHandler(this.Config_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbURL;
        private System.Windows.Forms.Button btnGetTraceRoutes;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LaHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LaLogPath;
        private System.Windows.Forms.Button btnLogPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CbPingTimer;
    }
}