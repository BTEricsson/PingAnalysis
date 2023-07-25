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
            this.btnServiceAdmin = new System.Windows.Forms.Button();
            this.btnPingConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnServiceAdmin
            // 
            this.btnServiceAdmin.Location = new System.Drawing.Point(612, 24);
            this.btnServiceAdmin.Name = "btnServiceAdmin";
            this.btnServiceAdmin.Size = new System.Drawing.Size(121, 23);
            this.btnServiceAdmin.TabIndex = 5;
            this.btnServiceAdmin.Text = "Service Admin";
            this.btnServiceAdmin.UseVisualStyleBackColor = true;
            this.btnServiceAdmin.Click += new System.EventHandler(this.btnServiceAdmin_Click);
            // 
            // btnPingConfig
            // 
            this.btnPingConfig.Location = new System.Drawing.Point(612, 62);
            this.btnPingConfig.Name = "btnPingConfig";
            this.btnPingConfig.Size = new System.Drawing.Size(121, 23);
            this.btnPingConfig.TabIndex = 6;
            this.btnPingConfig.Text = "Ping Config";
            this.btnPingConfig.UseVisualStyleBackColor = true;
            this.btnPingConfig.Click += new System.EventHandler(this.btnTraceEdit_Click);
            // 
            // PingAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPingConfig);
            this.Controls.Add(this.btnServiceAdmin);
            this.Name = "PingAnalysis";
            this.Text = "Ping Analysis";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnServiceAdmin;
        private System.Windows.Forms.Button btnPingConfig;
    }
}

