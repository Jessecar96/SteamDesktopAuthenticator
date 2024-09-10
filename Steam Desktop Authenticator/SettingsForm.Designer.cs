namespace Steam_Desktop_Authenticator
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.chkPeriodicChecking = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.numPeriodicInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.chkConfirmMarket = new System.Windows.Forms.CheckBox();
            this.chkConfirmTrades = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.webAPIAddress = new System.Windows.Forms.TextBox();
            this.communityAddress = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodicInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // chkPeriodicChecking
            // 
            this.chkPeriodicChecking.AutoSize = true;
            this.chkPeriodicChecking.Location = new System.Drawing.Point(12, 12);
            this.chkPeriodicChecking.Name = "chkPeriodicChecking";
            this.chkPeriodicChecking.Size = new System.Drawing.Size(233, 30);
            this.chkPeriodicChecking.TabIndex = 0;
            this.chkPeriodicChecking.Text = "Periodically check for new confirmations\r\nand show a popup when they arrive";
            this.chkPeriodicChecking.UseVisualStyleBackColor = true;
            this.chkPeriodicChecking.CheckedChanged += new System.EventHandler(this.chkPeriodicChecking_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(12, 237);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(352, 38);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // numPeriodicInterval
            // 
            this.numPeriodicInterval.Location = new System.Drawing.Point(12, 51);
            this.numPeriodicInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPeriodicInterval.Name = "numPeriodicInterval";
            this.numPeriodicInterval.Size = new System.Drawing.Size(41, 22);
            this.numPeriodicInterval.TabIndex = 2;
            this.numPeriodicInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seconds between checking \r\nfor confirmations";
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Location = new System.Drawing.Point(12, 81);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(213, 17);
            this.chkCheckAll.TabIndex = 4;
            this.chkCheckAll.Text = "Check all accounts for confirmations";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            // 
            // chkConfirmMarket
            // 
            this.chkConfirmMarket.AutoSize = true;
            this.chkConfirmMarket.Location = new System.Drawing.Point(12, 104);
            this.chkConfirmMarket.Name = "chkConfirmMarket";
            this.chkConfirmMarket.Size = new System.Drawing.Size(198, 17);
            this.chkConfirmMarket.TabIndex = 5;
            this.chkConfirmMarket.Text = "Auto-confirm market transactions";
            this.chkConfirmMarket.UseVisualStyleBackColor = true;
            this.chkConfirmMarket.CheckedChanged += new System.EventHandler(this.chkConfirmMarket_CheckedChanged);
            // 
            // chkConfirmTrades
            // 
            this.chkConfirmTrades.AutoSize = true;
            this.chkConfirmTrades.Location = new System.Drawing.Point(12, 127);
            this.chkConfirmTrades.Name = "chkConfirmTrades";
            this.chkConfirmTrades.Size = new System.Drawing.Size(129, 17);
            this.chkConfirmTrades.TabIndex = 6;
            this.chkConfirmTrades.Text = "Auto-confirm trades";
            this.chkConfirmTrades.UseVisualStyleBackColor = true;
            this.chkConfirmTrades.CheckedChanged += new System.EventHandler(this.chkConfirmTrades_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "WebAPI Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Community Address";
            // 
            // webAPIAddress
            // 
            this.webAPIAddress.Location = new System.Drawing.Point(125, 161);
            this.webAPIAddress.Name = "webAPIAddress";
            this.webAPIAddress.Size = new System.Drawing.Size(238, 22);
            this.webAPIAddress.TabIndex = 9;
            this.webAPIAddress.Text = "https://api.steampowered.com";
            // 
            // communityAddress
            // 
            this.communityAddress.Location = new System.Drawing.Point(125, 193);
            this.communityAddress.Name = "communityAddress";
            this.communityAddress.Size = new System.Drawing.Size(238, 22);
            this.communityAddress.TabIndex = 10;
            this.communityAddress.Text = "https://steamcommunity.com";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 287);
            this.Controls.Add(this.communityAddress);
            this.Controls.Add(this.webAPIAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkConfirmTrades);
            this.Controls.Add(this.chkConfirmMarket);
            this.Controls.Add(this.chkCheckAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numPeriodicInterval);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkPeriodicChecking);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodicInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPeriodicChecking;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown numPeriodicInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCheckAll;
        private System.Windows.Forms.CheckBox chkConfirmMarket;
        private System.Windows.Forms.CheckBox chkConfirmTrades;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox webAPIAddress;
        private System.Windows.Forms.TextBox communityAddress;
    }
}