namespace Analysis
{
    partial class PingAnalysis
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
            this.components = new System.ComponentModel.Container();
            this.btnServiceAdmin = new System.Windows.Forms.Button();
            this.btnPingConfig = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.RtbPingLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LaServiceStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnServiceAdmin
            // 
            this.btnServiceAdmin.Location = new System.Drawing.Point(665, 38);
            this.btnServiceAdmin.Name = "btnServiceAdmin";
            this.btnServiceAdmin.Size = new System.Drawing.Size(121, 23);
            this.btnServiceAdmin.TabIndex = 5;
            this.btnServiceAdmin.Text = "Service Admin";
            this.btnServiceAdmin.UseVisualStyleBackColor = true;
            this.btnServiceAdmin.Click += new System.EventHandler(this.BtnServiceAdmin_Click);
            // 
            // btnPingConfig
            // 
            this.btnPingConfig.Location = new System.Drawing.Point(665, 76);
            this.btnPingConfig.Name = "btnPingConfig";
            this.btnPingConfig.Size = new System.Drawing.Size(121, 23);
            this.btnPingConfig.TabIndex = 6;
            this.btnPingConfig.Text = "Ping Config";
            this.btnPingConfig.UseVisualStyleBackColor = true;
            this.btnPingConfig.Click += new System.EventHandler(this.BtnPingConfig_Click);
            // 
            // RtbPingLog
            // 
            this.RtbPingLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RtbPingLog.Location = new System.Drawing.Point(10, 38);
            this.RtbPingLog.Name = "RtbPingLog";
            this.RtbPingLog.Size = new System.Drawing.Size(638, 317);
            this.RtbPingLog.TabIndex = 8;
            this.RtbPingLog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Service Status:";
            // 
            // LaServiceStatus
            // 
            this.LaServiceStatus.AutoSize = true;
            this.LaServiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaServiceStatus.Location = new System.Drawing.Point(97, 14);
            this.LaServiceStatus.Name = "LaServiceStatus";
            this.LaServiceStatus.Size = new System.Drawing.Size(41, 13);
            this.LaServiceStatus.TabIndex = 10;
            this.LaServiceStatus.Text = "label2";
            // 
            // PingAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 367);
            this.Controls.Add(this.LaServiceStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RtbPingLog);
            this.Controls.Add(this.btnPingConfig);
            this.Controls.Add(this.btnServiceAdmin);
            this.Name = "PingAnalysis";
            this.Text = "Ping Analysis";
            this.Load += new System.EventHandler(this.PingAnalysis_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnServiceAdmin;
        private System.Windows.Forms.Button btnPingConfig;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox RtbPingLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LaServiceStatus;
    }
}

