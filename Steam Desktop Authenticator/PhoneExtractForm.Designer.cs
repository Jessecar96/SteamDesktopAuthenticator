namespace Steam_Desktop_Authenticator
{
    partial class PhoneExtractForm
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
            this.lblLog = new System.Windows.Forms.ListBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.tCheckDevice = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblLog
            // 
            this.lblLog.FormattingEnabled = true;
            this.lblLog.Location = new System.Drawing.Point(23, 63);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(238, 147);
            this.lblLog.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(23, 216);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(107, 35);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect over WiFi";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(201, 216);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(60, 35);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // tCheckDevice
            // 
            this.tCheckDevice.Enabled = true;
            this.tCheckDevice.Interval = 5000;
            this.tCheckDevice.Tick += new System.EventHandler(this.tCheckDevice_Tick);
            // 
            // PhoneExtractForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 270);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblLog);
            this.Name = "PhoneExtractForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Extract from phone";
            this.Load += new System.EventHandler(this.PhoneExtractForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lblLog;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Timer tCheckDevice;
    }
}