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
            this.btnOk = new System.Windows.Forms.Button();
            this.numPeriodicInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.GroupPopup = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton4_SystemTray = new System.Windows.Forms.RadioButton();
            this.radioButton3_SystemTray = new System.Windows.Forms.RadioButton();
            this.radioButton2_SystemTray = new System.Windows.Forms.RadioButton();
            this.radioButton1_SystemTray = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupConfirmationList = new System.Windows.Forms.GroupBox();
            this.checkBoxConfirmationListBtn = new System.Windows.Forms.CheckBox();
            this.groupUpdates = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoCheckForUpdates = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodicInterval)).BeginInit();
            this.GroupPopup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupConfirmationList.SuspendLayout();
            this.groupUpdates.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkPeriodicChecking
            // 
            this.chkPeriodicChecking.AutoSize = true;
            this.chkPeriodicChecking.Location = new System.Drawing.Point(14, 22);
            this.chkPeriodicChecking.Name = "chkPeriodicChecking";
            this.chkPeriodicChecking.Size = new System.Drawing.Size(445, 17);
            this.chkPeriodicChecking.TabIndex = 0;
            this.chkPeriodicChecking.Text = "Periodically check for new confirmations and show a popup when new one arrive";
            this.chkPeriodicChecking.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.btnOk.Location = new System.Drawing.Point(368, 376);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 30);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.SettingsOk_Click);
            // 
            // numPeriodicInterval
            // 
            this.numPeriodicInterval.Location = new System.Drawing.Point(14, 47);
            this.numPeriodicInterval.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numPeriodicInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPeriodicInterval.Name = "numPeriodicInterval";
            this.numPeriodicInterval.Size = new System.Drawing.Size(80, 22);
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
            this.label1.Location = new System.Drawing.Point(100, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seconds between checking for confirmations";
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Location = new System.Drawing.Point(14, 80);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(213, 17);
            this.chkCheckAll.TabIndex = 4;
            this.chkCheckAll.Text = "Check all accounts for confirmations";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            // 
            // GroupPopup
            // 
            this.GroupPopup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupPopup.Controls.Add(this.chkCheckAll);
            this.GroupPopup.Controls.Add(this.chkPeriodicChecking);
            this.GroupPopup.Controls.Add(this.numPeriodicInterval);
            this.GroupPopup.Controls.Add(this.label1);
            this.GroupPopup.Location = new System.Drawing.Point(12, 137);
            this.GroupPopup.Name = "GroupPopup";
            this.GroupPopup.Size = new System.Drawing.Size(548, 110);
            this.GroupPopup.TabIndex = 4;
            this.GroupPopup.TabStop = false;
            this.GroupPopup.Text = "Popup Notification";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButton4_SystemTray);
            this.groupBox1.Controls.Add(this.radioButton3_SystemTray);
            this.groupBox1.Controls.Add(this.radioButton2_SystemTray);
            this.groupBox1.Controls.Add(this.radioButton1_SystemTray);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 119);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System Tray";
            // 
            // radioButton4_SystemTray
            // 
            this.radioButton4_SystemTray.AutoSize = true;
            this.radioButton4_SystemTray.Location = new System.Drawing.Point(14, 90);
            this.radioButton4_SystemTray.Name = "radioButton4_SystemTray";
            this.radioButton4_SystemTray.Size = new System.Drawing.Size(132, 17);
            this.radioButton4_SystemTray.TabIndex = 9;
            this.radioButton4_SystemTray.TabStop = true;
            this.radioButton4_SystemTray.Text = "none - hide tray icon";
            this.radioButton4_SystemTray.UseVisualStyleBackColor = true;
            // 
            // radioButton3_SystemTray
            // 
            this.radioButton3_SystemTray.AutoSize = true;
            this.radioButton3_SystemTray.Location = new System.Drawing.Point(14, 67);
            this.radioButton3_SystemTray.Name = "radioButton3_SystemTray";
            this.radioButton3_SystemTray.Size = new System.Drawing.Size(131, 17);
            this.radioButton3_SystemTray.TabIndex = 8;
            this.radioButton3_SystemTray.TabStop = true;
            this.radioButton3_SystemTray.Text = "none - show try icon";
            this.radioButton3_SystemTray.UseVisualStyleBackColor = true;
            // 
            // radioButton2_SystemTray
            // 
            this.radioButton2_SystemTray.AutoSize = true;
            this.radioButton2_SystemTray.Location = new System.Drawing.Point(14, 44);
            this.radioButton2_SystemTray.Name = "radioButton2_SystemTray";
            this.radioButton2_SystemTray.Size = new System.Drawing.Size(242, 17);
            this.radioButton2_SystemTray.TabIndex = 7;
            this.radioButton2_SystemTray.TabStop = true;
            this.radioButton2_SystemTray.Text = "Minimise button minimizes the app to tray";
            this.radioButton2_SystemTray.UseVisualStyleBackColor = true;
            // 
            // radioButton1_SystemTray
            // 
            this.radioButton1_SystemTray.AutoSize = true;
            this.radioButton1_SystemTray.Location = new System.Drawing.Point(14, 21);
            this.radioButton1_SystemTray.Name = "radioButton1_SystemTray";
            this.radioButton1_SystemTray.Size = new System.Drawing.Size(224, 17);
            this.radioButton1_SystemTray.TabIndex = 6;
            this.radioButton1_SystemTray.TabStop = true;
            this.radioButton1_SystemTray.Text = "Close button minimizes the app to tray";
            this.radioButton1_SystemTray.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(468, 376);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.SettingsCancel_Click);
            // 
            // groupConfirmationList
            // 
            this.groupConfirmationList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupConfirmationList.Controls.Add(this.checkBoxConfirmationListBtn);
            this.groupConfirmationList.Location = new System.Drawing.Point(12, 253);
            this.groupConfirmationList.Name = "groupConfirmationList";
            this.groupConfirmationList.Size = new System.Drawing.Size(548, 53);
            this.groupConfirmationList.TabIndex = 5;
            this.groupConfirmationList.TabStop = false;
            this.groupConfirmationList.Text = "Confirmation List";
            // 
            // checkBoxConfirmationListBtn
            // 
            this.checkBoxConfirmationListBtn.AutoSize = true;
            this.checkBoxConfirmationListBtn.Location = new System.Drawing.Point(14, 22);
            this.checkBoxConfirmationListBtn.Name = "checkBoxConfirmationListBtn";
            this.checkBoxConfirmationListBtn.Size = new System.Drawing.Size(517, 17);
            this.checkBoxConfirmationListBtn.TabIndex = 0;
            this.checkBoxConfirmationListBtn.Text = "Show Confirmation List button (you may need to restart the app for the GUI to dis" +
    "play correctly)";
            this.checkBoxConfirmationListBtn.UseVisualStyleBackColor = true;
            // 
            // groupUpdates
            // 
            this.groupUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupUpdates.Controls.Add(this.checkBoxAutoCheckForUpdates);
            this.groupUpdates.Location = new System.Drawing.Point(12, 312);
            this.groupUpdates.Name = "groupUpdates";
            this.groupUpdates.Size = new System.Drawing.Size(548, 53);
            this.groupUpdates.TabIndex = 7;
            this.groupUpdates.TabStop = false;
            this.groupUpdates.Text = "Updates";
            // 
            // checkBoxAutoCheckForUpdates
            // 
            this.checkBoxAutoCheckForUpdates.AutoSize = true;
            this.checkBoxAutoCheckForUpdates.Location = new System.Drawing.Point(14, 22);
            this.checkBoxAutoCheckForUpdates.Name = "checkBoxAutoCheckForUpdates";
            this.checkBoxAutoCheckForUpdates.Size = new System.Drawing.Size(190, 17);
            this.checkBoxAutoCheckForUpdates.TabIndex = 0;
            this.checkBoxAutoCheckForUpdates.Text = "Automatically check for updates";
            this.checkBoxAutoCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 418);
            this.Controls.Add(this.groupUpdates);
            this.Controls.Add(this.groupConfirmationList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupPopup);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodicInterval)).EndInit();
            this.GroupPopup.ResumeLayout(false);
            this.GroupPopup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupConfirmationList.ResumeLayout(false);
            this.groupConfirmationList.PerformLayout();
            this.groupUpdates.ResumeLayout(false);
            this.groupUpdates.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPeriodicChecking;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.NumericUpDown numPeriodicInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GroupPopup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3_SystemTray;
        private System.Windows.Forms.RadioButton radioButton2_SystemTray;
        private System.Windows.Forms.RadioButton radioButton1_SystemTray;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupConfirmationList;
        private System.Windows.Forms.CheckBox checkBoxConfirmationListBtn;
        private System.Windows.Forms.GroupBox groupUpdates;
        private System.Windows.Forms.CheckBox checkBoxAutoCheckForUpdates;
        private System.Windows.Forms.RadioButton radioButton4_SystemTray;
        private System.Windows.Forms.CheckBox chkCheckAll;
    }
}
