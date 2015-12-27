namespace Steam_Desktop_Authenticator
{
    partial class MainFormGuiCompact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormGuiCompact));
            this.groupToken = new System.Windows.Forms.GroupBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.pbTimeout = new System.Windows.Forms.ProgressBar();
            this.txtLoginToken = new System.Windows.Forms.TextBox();
            this.btnTradeConfirmationsList = new System.Windows.Forms.Button();
            this.btnTradeConfirmations = new System.Windows.Forms.Button();
            this.listAccounts = new System.Windows.Forms.ListBox();
            this.timerSteamGuard = new System.Windows.Forms.Timer(this.components);
            this.groupAccount = new System.Windows.Forms.GroupBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSteamLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.importAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImportMaFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImportAndroid = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManageEncryption = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoginAgain = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefreshSession = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuRemoveAccountFromManifest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDeactivateAuthenticator = new System.Windows.Forms.ToolStripMenuItem();
            this.quitUnderXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.trayAccountList = new System.Windows.Forms.ToolStripComboBox();
            this.trayTradeConfirmations = new System.Windows.Forms.ToolStripMenuItem();
            this.trayCopySteamGuard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.trayQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings_PopupNewConf = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtAccSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDisableListClick = new System.Windows.Forms.Label();
            this.groupToken.SuspendLayout();
            this.groupAccount.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.menuStripTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupToken
            // 
            this.groupToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupToken.Controls.Add(this.btnCopy);
            this.groupToken.Controls.Add(this.pbTimeout);
            this.groupToken.Controls.Add(this.txtLoginToken);
            this.groupToken.Location = new System.Drawing.Point(9, 25);
            this.groupToken.Name = "groupToken";
            this.groupToken.Size = new System.Drawing.Size(294, 77);
            this.groupToken.TabIndex = 1;
            this.groupToken.TabStop = false;
            this.groupToken.Text = "Login Token";
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI", 8.6F);
            this.btnCopy.Location = new System.Drawing.Point(216, 16);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(71, 32);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // pbTimeout
            // 
            this.pbTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTimeout.Location = new System.Drawing.Point(7, 54);
            this.pbTimeout.Maximum = 30;
            this.pbTimeout.Name = "pbTimeout";
            this.pbTimeout.Size = new System.Drawing.Size(280, 16);
            this.pbTimeout.TabIndex = 4;
            this.pbTimeout.Value = 30;
            // 
            // txtLoginToken
            // 
            this.txtLoginToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoginToken.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoginToken.Font = new System.Drawing.Font("Segoe UI", 13.55F);
            this.txtLoginToken.Location = new System.Drawing.Point(7, 16);
            this.txtLoginToken.Name = "txtLoginToken";
            this.txtLoginToken.ReadOnly = true;
            this.txtLoginToken.Size = new System.Drawing.Size(203, 32);
            this.txtLoginToken.TabIndex = 3;
            this.txtLoginToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnTradeConfirmationsList
            // 
            this.btnTradeConfirmationsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTradeConfirmationsList.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.btnTradeConfirmationsList.Location = new System.Drawing.Point(6, 52);
            this.btnTradeConfirmationsList.Name = "btnTradeConfirmationsList";
            this.btnTradeConfirmationsList.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.btnTradeConfirmationsList.Size = new System.Drawing.Size(284, 32);
            this.btnTradeConfirmationsList.TabIndex = 7;
            this.btnTradeConfirmationsList.Text = "View Trade Confirmations List";
            this.btnTradeConfirmationsList.UseVisualStyleBackColor = true;
            this.btnTradeConfirmationsList.Visible = false;
            this.btnTradeConfirmationsList.Click += new System.EventHandler(this.btnTradeConfirmationsList_Click);
            // 
            // btnTradeConfirmations
            // 
            this.btnTradeConfirmations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTradeConfirmations.Enabled = false;
            this.btnTradeConfirmations.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.btnTradeConfirmations.Location = new System.Drawing.Point(6, 15);
            this.btnTradeConfirmations.Name = "btnTradeConfirmations";
            this.btnTradeConfirmations.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.btnTradeConfirmations.Size = new System.Drawing.Size(284, 32);
            this.btnTradeConfirmations.TabIndex = 6;
            this.btnTradeConfirmations.Text = "View Trade Confirmations";
            this.btnTradeConfirmations.UseVisualStyleBackColor = true;
            this.btnTradeConfirmations.Click += new System.EventHandler(this.btnTradeConfirmations_Click);
            // 
            // listAccounts
            // 
            this.listAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listAccounts.FormattingEnabled = true;
            this.listAccounts.Location = new System.Drawing.Point(7, 160);
            this.listAccounts.Name = "listAccounts";
            this.listAccounts.Size = new System.Drawing.Size(296, 186);
            this.listAccounts.TabIndex = 8;
            this.listAccounts.SelectedValueChanged += new System.EventHandler(this.listAccounts_SelectedValueChanged);
            this.listAccounts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listAccounts_KeyDown);
            // 
            // timerSteamGuard
            // 
            this.timerSteamGuard.Enabled = true;
            this.timerSteamGuard.Interval = 1000;
            this.timerSteamGuard.Tick += new System.EventHandler(this.timerSteamGuard_Tick);
            // 
            // groupAccount
            // 
            this.groupAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAccount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupAccount.Controls.Add(this.btnTradeConfirmations);
            this.groupAccount.Controls.Add(this.btnTradeConfirmationsList);
            this.groupAccount.Location = new System.Drawing.Point(7, 102);
            this.groupAccount.Name = "groupAccount";
            this.groupAccount.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.groupAccount.Size = new System.Drawing.Size(296, 53);
            this.groupAccount.TabIndex = 5;
            this.groupAccount.TabStop = false;
            this.groupAccount.Text = "Account";
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelVersion.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.labelVersion.Location = new System.Drawing.Point(260, 378);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(45, 12);
            this.labelVersion.TabIndex = 12;
            this.labelVersion.Text = "v0.0.0";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.accountToolStripMenuItem,
            this.quitUnderXToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(309, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSteamLogin,
            this.importAccountToolStripMenuItem,
            this.toolStripSeparator1,
            this.menuSettings,
            this.menuManageEncryption,
            this.toolStripSeparator5,
            this.menuCheckForUpdates,
            this.menuQuit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuSteamLogin
            // 
            this.menuSteamLogin.Name = "menuSteamLogin";
            this.menuSteamLogin.Size = new System.Drawing.Size(179, 22);
            this.menuSteamLogin.Text = "Setup New Account";
            this.menuSteamLogin.Click += new System.EventHandler(this.menuSteamLogin_Click);
            // 
            // importAccountToolStripMenuItem
            // 
            this.importAccountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuImportMaFile,
            this.menuImportAndroid});
            this.importAccountToolStripMenuItem.Name = "importAccountToolStripMenuItem";
            this.importAccountToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.importAccountToolStripMenuItem.Text = "Import Account";
            // 
            // menuImportMaFile
            // 
            this.menuImportMaFile.Name = "menuImportMaFile";
            this.menuImportMaFile.Size = new System.Drawing.Size(186, 22);
            this.menuImportMaFile.Text = "From maFile";
            this.menuImportMaFile.Click += new System.EventHandler(this.menuImportMaFile_Click);
            // 
            // menuImportAndroid
            // 
            this.menuImportAndroid.Name = "menuImportAndroid";
            this.menuImportAndroid.Size = new System.Drawing.Size(186, 22);
            this.menuImportAndroid.Text = "From Android Device";
            this.menuImportAndroid.Click += new System.EventHandler(this.menuImportAndroid_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(179, 22);
            this.menuSettings.Text = "Settings";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // menuManageEncryption
            // 
            this.menuManageEncryption.Name = "menuManageEncryption";
            this.menuManageEncryption.Size = new System.Drawing.Size(179, 22);
            this.menuManageEncryption.Text = "Manage Encryption";
            this.menuManageEncryption.Click += new System.EventHandler(this.menuManageEncryption_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(176, 6);
            // 
            // menuCheckForUpdates
            // 
            this.menuCheckForUpdates.Name = "menuCheckForUpdates";
            this.menuCheckForUpdates.Size = new System.Drawing.Size(179, 22);
            this.menuCheckForUpdates.Text = "Check for updates";
            this.menuCheckForUpdates.Click += new System.EventHandler(this.menuCheckForUpdates_Click);
            // 
            // menuQuit
            // 
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(179, 22);
            this.menuQuit.Text = "Quit";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLoginAgain,
            this.menuRefreshSession,
            this.toolStripSeparator4,
            this.menuRemoveAccountFromManifest,
            this.toolStripSeparator6,
            this.menuDeactivateAuthenticator});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.accountToolStripMenuItem.Text = "Selected Account";
            // 
            // menuLoginAgain
            // 
            this.menuLoginAgain.Name = "menuLoginAgain";
            this.menuLoginAgain.Size = new System.Drawing.Size(205, 22);
            this.menuLoginAgain.Text = "Login again";
            this.menuLoginAgain.Click += new System.EventHandler(this.menuLoginAgain_Click);
            // 
            // menuRefreshSession
            // 
            this.menuRefreshSession.Name = "menuRefreshSession";
            this.menuRefreshSession.Size = new System.Drawing.Size(205, 22);
            this.menuRefreshSession.Text = "Force session refresh";
            this.menuRefreshSession.Click += new System.EventHandler(this.menuRefreshSession_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(202, 6);
            // 
            // menuRemoveAccountFromManifest
            // 
            this.menuRemoveAccountFromManifest.Name = "menuRemoveAccountFromManifest";
            this.menuRemoveAccountFromManifest.Size = new System.Drawing.Size(205, 22);
            this.menuRemoveAccountFromManifest.Text = "Remove from manifest";
            this.menuRemoveAccountFromManifest.Click += new System.EventHandler(this.menuRemoveAccountFromManifest_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(202, 6);
            // 
            // menuDeactivateAuthenticator
            // 
            this.menuDeactivateAuthenticator.Name = "menuDeactivateAuthenticator";
            this.menuDeactivateAuthenticator.Size = new System.Drawing.Size(205, 22);
            this.menuDeactivateAuthenticator.Text = "Deactivate Authenticator";
            this.menuDeactivateAuthenticator.Click += new System.EventHandler(this.menuDeactivateAuthenticator_Click);
            // 
            // quitUnderXToolStripMenuItem
            // 
            this.quitUnderXToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.quitUnderXToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.quitUnderXToolStripMenuItem.Name = "quitUnderXToolStripMenuItem";
            this.quitUnderXToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.quitUnderXToolStripMenuItem.Text = "Quit";
            this.quitUnderXToolStripMenuItem.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.menuStripTray;
            this.trayIcon.Text = "Steam Desktop Authenticator";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // menuStripTray
            // 
            this.menuStripTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayRestore,
            this.toolStripSeparator2,
            this.trayAccountList,
            this.trayTradeConfirmations,
            this.trayCopySteamGuard,
            this.toolStripSeparator3,
            this.trayQuit});
            this.menuStripTray.Name = "contextMenuStripTray";
            this.menuStripTray.Size = new System.Drawing.Size(216, 131);
            // 
            // trayRestore
            // 
            this.trayRestore.Name = "trayRestore";
            this.trayRestore.Size = new System.Drawing.Size(215, 22);
            this.trayRestore.Text = "Restore";
            this.trayRestore.Click += new System.EventHandler(this.trayRestore_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // trayAccountList
            // 
            this.trayAccountList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trayAccountList.Items.AddRange(new object[] {
            "test1",
            "test2"});
            this.trayAccountList.Name = "trayAccountList";
            this.trayAccountList.Size = new System.Drawing.Size(121, 23);
            this.trayAccountList.SelectedIndexChanged += new System.EventHandler(this.trayAccountList_SelectedIndexChanged);
            // 
            // trayTradeConfirmations
            // 
            this.trayTradeConfirmations.Name = "trayTradeConfirmations";
            this.trayTradeConfirmations.Size = new System.Drawing.Size(215, 22);
            this.trayTradeConfirmations.Text = "Trade Confirmations";
            this.trayTradeConfirmations.Click += new System.EventHandler(this.trayTradeConfirmations_Click);
            // 
            // trayCopySteamGuard
            // 
            this.trayCopySteamGuard.Name = "trayCopySteamGuard";
            this.trayCopySteamGuard.Size = new System.Drawing.Size(215, 22);
            this.trayCopySteamGuard.Text = "Copy SG code to clipboard";
            this.trayCopySteamGuard.Click += new System.EventHandler(this.trayCopySteamGuard_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
            // 
            // trayQuit
            // 
            this.trayQuit.Name = "trayQuit";
            this.trayQuit.Size = new System.Drawing.Size(215, 22);
            this.trayQuit.Text = "Quit";
            this.trayQuit.Click += new System.EventHandler(this.trayQuit_Click);
            // 
            // Settings_PopupNewConf
            // 
            this.Settings_PopupNewConf.Enabled = true;
            this.Settings_PopupNewConf.Interval = 5000;
            this.Settings_PopupNewConf.Tick += new System.EventHandler(this.Settings_PopupNewConf_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 6.75F);
            this.lblStatus.Location = new System.Drawing.Point(10, 377);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(244, 13);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Console: status";
            // 
            // txtAccSearch
            // 
            this.txtAccSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccSearch.Location = new System.Drawing.Point(56, 352);
            this.txtAccSearch.Name = "txtAccSearch";
            this.txtAccSearch.Size = new System.Drawing.Size(247, 22);
            this.txtAccSearch.TabIndex = 10;
            this.txtAccSearch.TextChanged += new System.EventHandler(this.txtAccSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Search:";
            // 
            // labelDisableListClick
            // 
            this.labelDisableListClick.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDisableListClick.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelDisableListClick.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.labelDisableListClick.Location = new System.Drawing.Point(8, 161);
            this.labelDisableListClick.Name = "labelDisableListClick";
            this.labelDisableListClick.Size = new System.Drawing.Size(294, 184);
            this.labelDisableListClick.TabIndex = 18;
            this.labelDisableListClick.Text = "Refreshing session...";
            this.labelDisableListClick.Visible = false;
            // 
            // MainFormGuiCompact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 394);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelDisableListClick);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAccSearch);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupAccount);
            this.Controls.Add(this.listAccounts);
            this.Controls.Add(this.groupToken);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(325, 432);
            this.Name = "MainFormGuiCompact";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steam Desktop Authenticator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.groupToken.ResumeLayout(false);
            this.groupToken.PerformLayout();
            this.groupAccount.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.menuStripTray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupToken;
        private System.Windows.Forms.ProgressBar pbTimeout;
        private System.Windows.Forms.TextBox txtLoginToken;
        private System.Windows.Forms.ListBox listAccounts;
        private System.Windows.Forms.Timer timerSteamGuard;
        private System.Windows.Forms.Button btnTradeConfirmations;
        private System.Windows.Forms.GroupBox groupAccount;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuRemoveAccountFromManifest;
        private System.Windows.Forms.ToolStripMenuItem menuLoginAgain;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip menuStripTray;
        private System.Windows.Forms.ToolStripMenuItem trayRestore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem trayTradeConfirmations;
        private System.Windows.Forms.ToolStripMenuItem trayCopySteamGuard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem trayQuit;
        private System.Windows.Forms.Timer Settings_PopupNewConf;
        private System.Windows.Forms.ToolStripComboBox trayAccountList;
        private System.Windows.Forms.ToolStripMenuItem importAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuImportMaFile;
        private System.Windows.Forms.ToolStripMenuItem menuImportAndroid;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtAccSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuDeactivateAuthenticator;
        private System.Windows.Forms.ToolStripMenuItem menuRefreshSession;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolStripMenuItem quitUnderXToolStripMenuItem;
        private System.Windows.Forms.Button btnTradeConfirmationsList;
        private System.Windows.Forms.ToolStripMenuItem menuSteamLogin;
        private System.Windows.Forms.ToolStripMenuItem menuManageEncryption;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuCheckForUpdates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Label labelDisableListClick;
    }
}

