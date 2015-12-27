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
            this.chkPopupNewConfPeriodicChecking = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.numPeriodicInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.GroupPopupNewConf = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioBtnBorderConfPopup2 = new System.Windows.Forms.RadioButton();
            this.radioBtnBorderConfPopup1 = new System.Windows.Forms.RadioButton();
            this.groupSystemTray = new System.Windows.Forms.GroupBox();
            this.radioButton4_SystemTray = new System.Windows.Forms.RadioButton();
            this.radioButton3_SystemTray = new System.Windows.Forms.RadioButton();
            this.radioButton2_SystemTray = new System.Windows.Forms.RadioButton();
            this.radioButton1_SystemTray = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupConfirmationListBtn = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxConfirmationListBtn = new System.Windows.Forms.CheckBox();
            this.groupUpdates = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoCheckForUpdates = new System.Windows.Forms.CheckBox();
            this.groupBoxAppGUI = new System.Windows.Forms.GroupBox();
            this.radioBtnGui2 = new System.Windows.Forms.RadioButton();
            this.radioBtnGui1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodicInterval)).BeginInit();
            this.GroupPopupNewConf.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupSystemTray.SuspendLayout();
            this.groupConfirmationListBtn.SuspendLayout();
            this.groupUpdates.SuspendLayout();
            this.groupBoxAppGUI.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkPopupNewConfPeriodicChecking
            // 
            this.chkPopupNewConfPeriodicChecking.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkPopupNewConfPeriodicChecking.AutoSize = true;
            this.chkPopupNewConfPeriodicChecking.Location = new System.Drawing.Point(19, 22);
            this.chkPopupNewConfPeriodicChecking.Name = "chkPopupNewConfPeriodicChecking";
            this.chkPopupNewConfPeriodicChecking.Size = new System.Drawing.Size(445, 17);
            this.chkPopupNewConfPeriodicChecking.TabIndex = 0;
            this.chkPopupNewConfPeriodicChecking.Text = "Periodically check for new confirmations and show a popup when new one arrive";
            this.chkPopupNewConfPeriodicChecking.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.btnOk.Location = new System.Drawing.Point(315, 403);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(107, 30);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.SettingsOk_Click);
            // 
            // numPeriodicInterval
            // 
            this.numPeriodicInterval.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numPeriodicInterval.Location = new System.Drawing.Point(19, 47);
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
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seconds between checking for confirmations";
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Location = new System.Drawing.Point(19, 80);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(213, 17);
            this.chkCheckAll.TabIndex = 4;
            this.chkCheckAll.Text = "Check all accounts for confirmations";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            // 
            // GroupPopupNewConf
            // 
            this.GroupPopupNewConf.Controls.Add(this.groupBox2);
            this.GroupPopupNewConf.Controls.Add(this.chkCheckAll);
            this.GroupPopupNewConf.Controls.Add(this.chkPopupNewConfPeriodicChecking);
            this.GroupPopupNewConf.Controls.Add(this.numPeriodicInterval);
            this.GroupPopupNewConf.Controls.Add(this.label1);
            this.GroupPopupNewConf.Location = new System.Drawing.Point(12, 12);
            this.GroupPopupNewConf.Name = "GroupPopupNewConf";
            this.GroupPopupNewConf.Size = new System.Drawing.Size(522, 179);
            this.GroupPopupNewConf.TabIndex = 4;
            this.GroupPopupNewConf.TabStop = false;
            this.GroupPopupNewConf.Text = "Popup New Confirmation";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.radioBtnBorderConfPopup2);
            this.groupBox2.Controls.Add(this.radioBtnBorderConfPopup1);
            this.groupBox2.Location = new System.Drawing.Point(7, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(508, 69);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Border ( will take effect after you restart the app )";
            // 
            // radioBtnBorderConfPopup2
            // 
            this.radioBtnBorderConfPopup2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioBtnBorderConfPopup2.AutoSize = true;
            this.radioBtnBorderConfPopup2.Location = new System.Drawing.Point(14, 44);
            this.radioBtnBorderConfPopup2.Name = "radioBtnBorderConfPopup2";
            this.radioBtnBorderConfPopup2.Size = new System.Drawing.Size(110, 17);
            this.radioBtnBorderConfPopup2.TabIndex = 7;
            this.radioBtnBorderConfPopup2.TabStop = true;
            this.radioBtnBorderConfPopup2.Text = "windows border";
            this.radioBtnBorderConfPopup2.UseVisualStyleBackColor = true;
            // 
            // radioBtnBorderConfPopup1
            // 
            this.radioBtnBorderConfPopup1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioBtnBorderConfPopup1.AutoSize = true;
            this.radioBtnBorderConfPopup1.Location = new System.Drawing.Point(14, 21);
            this.radioBtnBorderConfPopup1.Name = "radioBtnBorderConfPopup1";
            this.radioBtnBorderConfPopup1.Size = new System.Drawing.Size(89, 17);
            this.radioBtnBorderConfPopup1.TabIndex = 6;
            this.radioBtnBorderConfPopup1.TabStop = true;
            this.radioBtnBorderConfPopup1.Text = "small border";
            this.radioBtnBorderConfPopup1.UseVisualStyleBackColor = true;
            // 
            // groupSystemTray
            // 
            this.groupSystemTray.Controls.Add(this.radioButton4_SystemTray);
            this.groupSystemTray.Controls.Add(this.radioButton3_SystemTray);
            this.groupSystemTray.Controls.Add(this.radioButton2_SystemTray);
            this.groupSystemTray.Controls.Add(this.radioButton1_SystemTray);
            this.groupSystemTray.Location = new System.Drawing.Point(12, 272);
            this.groupSystemTray.Name = "groupSystemTray";
            this.groupSystemTray.Size = new System.Drawing.Size(264, 119);
            this.groupSystemTray.TabIndex = 5;
            this.groupSystemTray.TabStop = false;
            this.groupSystemTray.Text = "System Tray";
            // 
            // radioButton4_SystemTray
            // 
            this.radioButton4_SystemTray.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.radioButton3_SystemTray.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.radioButton2_SystemTray.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.radioButton1_SystemTray.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.btnCancel.Location = new System.Drawing.Point(428, 403);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.SettingsCancel_Click);
            // 
            // groupConfirmationListBtn
            // 
            this.groupConfirmationListBtn.Controls.Add(this.label2);
            this.groupConfirmationListBtn.Controls.Add(this.checkBoxConfirmationListBtn);
            this.groupConfirmationListBtn.Location = new System.Drawing.Point(12, 197);
            this.groupConfirmationListBtn.Name = "groupConfirmationListBtn";
            this.groupConfirmationListBtn.Size = new System.Drawing.Size(523, 69);
            this.groupConfirmationListBtn.TabIndex = 5;
            this.groupConfirmationListBtn.TabStop = false;
            this.groupConfirmationListBtn.Text = "Confirmation List Button";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "You may need to restart the app for the GUI to display correctly";
            // 
            // checkBoxConfirmationListBtn
            // 
            this.checkBoxConfirmationListBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxConfirmationListBtn.AutoSize = true;
            this.checkBoxConfirmationListBtn.Location = new System.Drawing.Point(17, 22);
            this.checkBoxConfirmationListBtn.Name = "checkBoxConfirmationListBtn";
            this.checkBoxConfirmationListBtn.Size = new System.Drawing.Size(185, 17);
            this.checkBoxConfirmationListBtn.TabIndex = 0;
            this.checkBoxConfirmationListBtn.Text = "Show Confirmation List button";
            this.checkBoxConfirmationListBtn.UseVisualStyleBackColor = true;
            // 
            // groupUpdates
            // 
            this.groupUpdates.Controls.Add(this.checkBoxAutoCheckForUpdates);
            this.groupUpdates.Location = new System.Drawing.Point(282, 346);
            this.groupUpdates.Name = "groupUpdates";
            this.groupUpdates.Size = new System.Drawing.Size(252, 45);
            this.groupUpdates.TabIndex = 7;
            this.groupUpdates.TabStop = false;
            this.groupUpdates.Text = "Updates";
            // 
            // checkBoxAutoCheckForUpdates
            // 
            this.checkBoxAutoCheckForUpdates.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxAutoCheckForUpdates.AutoSize = true;
            this.checkBoxAutoCheckForUpdates.Location = new System.Drawing.Point(17, 19);
            this.checkBoxAutoCheckForUpdates.Name = "checkBoxAutoCheckForUpdates";
            this.checkBoxAutoCheckForUpdates.Size = new System.Drawing.Size(190, 17);
            this.checkBoxAutoCheckForUpdates.TabIndex = 0;
            this.checkBoxAutoCheckForUpdates.Text = "Automatically check for updates";
            this.checkBoxAutoCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // groupBoxAppGUI
            // 
            this.groupBoxAppGUI.Controls.Add(this.radioBtnGui2);
            this.groupBoxAppGUI.Controls.Add(this.radioBtnGui1);
            this.groupBoxAppGUI.Location = new System.Drawing.Point(282, 272);
            this.groupBoxAppGUI.Name = "groupBoxAppGUI";
            this.groupBoxAppGUI.Size = new System.Drawing.Size(253, 68);
            this.groupBoxAppGUI.TabIndex = 10;
            this.groupBoxAppGUI.TabStop = false;
            this.groupBoxAppGUI.Text = "App GUI ( will take after you restart the app )";
            // 
            // radioBtnGui2
            // 
            this.radioBtnGui2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioBtnGui2.AutoSize = true;
            this.radioBtnGui2.Location = new System.Drawing.Point(17, 41);
            this.radioBtnGui2.Name = "radioBtnGui2";
            this.radioBtnGui2.Size = new System.Drawing.Size(92, 17);
            this.radioBtnGui2.TabIndex = 7;
            this.radioBtnGui2.TabStop = true;
            this.radioBtnGui2.Text = "Compact GUI";
            this.radioBtnGui2.UseVisualStyleBackColor = true;
            // 
            // radioBtnGui1
            // 
            this.radioBtnGui1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioBtnGui1.AutoSize = true;
            this.radioBtnGui1.Location = new System.Drawing.Point(17, 19);
            this.radioBtnGui1.Name = "radioBtnGui1";
            this.radioBtnGui1.Size = new System.Drawing.Size(169, 17);
            this.radioBtnGui1.TabIndex = 6;
            this.radioBtnGui1.TabStop = true;
            this.radioBtnGui1.Text = "Simple GUI ( for beginners  )";
            this.radioBtnGui1.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 445);
            this.Controls.Add(this.groupBoxAppGUI);
            this.Controls.Add(this.groupUpdates);
            this.Controls.Add(this.groupConfirmationListBtn);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupSystemTray);
            this.Controls.Add(this.GroupPopupNewConf);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodicInterval)).EndInit();
            this.GroupPopupNewConf.ResumeLayout(false);
            this.GroupPopupNewConf.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupSystemTray.ResumeLayout(false);
            this.groupSystemTray.PerformLayout();
            this.groupConfirmationListBtn.ResumeLayout(false);
            this.groupConfirmationListBtn.PerformLayout();
            this.groupUpdates.ResumeLayout(false);
            this.groupUpdates.PerformLayout();
            this.groupBoxAppGUI.ResumeLayout(false);
            this.groupBoxAppGUI.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPopupNewConfPeriodicChecking;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.NumericUpDown numPeriodicInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GroupPopupNewConf;
        private System.Windows.Forms.GroupBox groupSystemTray;
        private System.Windows.Forms.RadioButton radioButton3_SystemTray;
        private System.Windows.Forms.RadioButton radioButton2_SystemTray;
        private System.Windows.Forms.RadioButton radioButton1_SystemTray;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupConfirmationListBtn;
        private System.Windows.Forms.CheckBox checkBoxConfirmationListBtn;
        private System.Windows.Forms.GroupBox groupUpdates;
        private System.Windows.Forms.CheckBox checkBoxAutoCheckForUpdates;
        private System.Windows.Forms.RadioButton radioButton4_SystemTray;
        private System.Windows.Forms.CheckBox chkCheckAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioBtnBorderConfPopup2;
        private System.Windows.Forms.RadioButton radioBtnBorderConfPopup1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxAppGUI;
        private System.Windows.Forms.RadioButton radioBtnGui2;
        private System.Windows.Forms.RadioButton radioBtnGui1;
    }
}
