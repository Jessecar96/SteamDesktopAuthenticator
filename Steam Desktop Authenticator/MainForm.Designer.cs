namespace Steam_Desktop_Authenticator
{

    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnSteamLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.pbTimeout = new System.Windows.Forms.ProgressBar();
            this.txtLoginToken = new System.Windows.Forms.TextBox();
            this.listAccounts = new System.Windows.Forms.ListBox();
            this.timerSteamGuard = new System.Windows.Forms.Timer(this.components);
            this.btnTradeConfirmations = new System.Windows.Forms.Button();
            this.btnManageEncryption = new System.Windows.Forms.Button();
            this.groupAccount = new System.Windows.Forms.GroupBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImportMaFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoginAgain = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefreshSession = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuRemoveAccountFromManifest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeactivateAuthenticator = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.trayAccountList = new System.Windows.Forms.ToolStripComboBox();
            this.trayTradeConfirmations = new System.Windows.Forms.ToolStripMenuItem();
            this.trayCopySteamGuard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.trayQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerTradesPopup = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtAccSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupAccount.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.menuStripTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSteamLogin
            // 
            this.btnSteamLogin.Location = new System.Drawing.Point(12, 27);
            this.btnSteamLogin.Name = "btnSteamLogin";
            this.btnSteamLogin.Size = new System.Drawing.Size(155, 31);
            this.btnSteamLogin.TabIndex = 1;
            this.btnSteamLogin.Text = "Добавить аккаунт";
            this.btnSteamLogin.UseVisualStyleBackColor = true;
            this.btnSteamLogin.Click += new System.EventHandler(this.btnSteamLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Controls.Add(this.pbTimeout);
            this.groupBox1.Controls.Add(this.txtLoginToken);
            this.groupBox1.Location = new System.Drawing.Point(12, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 85);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Guard код";
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(267, 19);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(54, 35);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // pbTimeout
            // 
            this.pbTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTimeout.Location = new System.Drawing.Point(6, 60);
            this.pbTimeout.Maximum = 30;
            this.pbTimeout.Name = "pbTimeout";
            this.pbTimeout.Size = new System.Drawing.Size(315, 19);
            this.pbTimeout.TabIndex = 1;
            this.pbTimeout.Value = 30;
            // 
            // txtLoginToken
            // 
            this.txtLoginToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoginToken.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoginToken.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginToken.Location = new System.Drawing.Point(6, 19);
            this.txtLoginToken.Name = "txtLoginToken";
            this.txtLoginToken.ReadOnly = true;
            this.txtLoginToken.Size = new System.Drawing.Size(255, 35);
            this.txtLoginToken.TabIndex = 0;
            this.txtLoginToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listAccounts
            // 
            this.listAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listAccounts.FormattingEnabled = true;
            this.listAccounts.Location = new System.Drawing.Point(12, 217);
            this.listAccounts.Name = "listAccounts";
            this.listAccounts.Size = new System.Drawing.Size(327, 147);
            this.listAccounts.TabIndex = 3;
            this.listAccounts.SelectedValueChanged += new System.EventHandler(this.listAccounts_SelectedValueChanged);
            this.listAccounts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listAccounts_KeyDown);
            // 
            // timerSteamGuard
            // 
            this.timerSteamGuard.Enabled = true;
            this.timerSteamGuard.Interval = 1000;
            this.timerSteamGuard.Tick += new System.EventHandler(this.timerSteamGuard_Tick);
            // 
            // btnTradeConfirmations
            // 
            this.btnTradeConfirmations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTradeConfirmations.Enabled = false;
            this.btnTradeConfirmations.Location = new System.Drawing.Point(6, 19);
            this.btnTradeConfirmations.Name = "btnTradeConfirmations";
            this.btnTradeConfirmations.Size = new System.Drawing.Size(315, 31);
            this.btnTradeConfirmations.TabIndex = 4;
            this.btnTradeConfirmations.Text = "Посмотреть предложения";
            this.btnTradeConfirmations.UseVisualStyleBackColor = true;
            this.btnTradeConfirmations.Click += new System.EventHandler(this.btnTradeConfirmations_Click);
            // 
            // btnManageEncryption
            // 
            this.btnManageEncryption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnManageEncryption.Location = new System.Drawing.Point(173, 27);
            this.btnManageEncryption.Name = "btnManageEncryption";
            this.btnManageEncryption.Size = new System.Drawing.Size(166, 31);
            this.btnManageEncryption.TabIndex = 6;
            this.btnManageEncryption.Text = "Настройки шифрования";
            this.btnManageEncryption.UseVisualStyleBackColor = true;
            this.btnManageEncryption.Click += new System.EventHandler(this.btnManageEncryption_Click);
            // 
            // groupAccount
            // 
            this.groupAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAccount.Controls.Add(this.btnTradeConfirmations);
            this.groupAccount.Location = new System.Drawing.Point(12, 155);
            this.groupAccount.Name = "groupAccount";
            this.groupAccount.Size = new System.Drawing.Size(327, 56);
            this.groupAccount.TabIndex = 7;
            this.groupAccount.TabStop = false;
            this.groupAccount.Text = "Аккаунт";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.accountToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(351, 24);
            this.menuStrip.TabIndex = 10;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importAccountToolStripMenuItem,
            this.toolStripSeparator1,
            this.menuSettings,
            this.menuQuit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // importAccountToolStripMenuItem
            // 
            this.importAccountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuImportMaFile});
            this.importAccountToolStripMenuItem.Name = "importAccountToolStripMenuItem";
            this.importAccountToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importAccountToolStripMenuItem.Text = "Импорт аккаунта";
            // 
            // menuImportMaFile
            // 
            this.menuImportMaFile.Name = "menuImportMaFile";
            this.menuImportMaFile.Size = new System.Drawing.Size(180, 22);
            this.menuImportMaFile.Text = "From maFile";
            this.menuImportMaFile.Click += new System.EventHandler(this.menuImportMaFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(180, 22);
            this.menuSettings.Text = "Настройки";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // menuQuit
            // 
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(180, 22);
            this.menuQuit.Text = "Выход";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLoginAgain,
            this.menuRefreshSession,
            this.toolStripSeparator4,
            this.menuRemoveAccountFromManifest,
            this.menuDeactivateAuthenticator});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.accountToolStripMenuItem.Text = "Выбранный аккаунт";
            // 
            // menuLoginAgain
            // 
            this.menuLoginAgain.Name = "menuLoginAgain";
            this.menuLoginAgain.Size = new System.Drawing.Size(254, 22);
            this.menuLoginAgain.Text = "Перезайти (Login again)";
            this.menuLoginAgain.Click += new System.EventHandler(this.menuLoginAgain_Click);
            // 
            // menuRefreshSession
            // 
            this.menuRefreshSession.Name = "menuRefreshSession";
            this.menuRefreshSession.Size = new System.Drawing.Size(254, 22);
            this.menuRefreshSession.Text = "Обновить сессию";
            this.menuRefreshSession.Click += new System.EventHandler(this.menuRefreshSession_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(251, 6);
            // 
            // menuRemoveAccountFromManifest
            // 
            this.menuRemoveAccountFromManifest.Name = "menuRemoveAccountFromManifest";
            this.menuRemoveAccountFromManifest.Size = new System.Drawing.Size(254, 22);
            this.menuRemoveAccountFromManifest.Text = "Удалить из manifest";
            this.menuRemoveAccountFromManifest.Click += new System.EventHandler(this.menuRemoveAccountFromManifest_Click);
            // 
            // menuDeactivateAuthenticator
            // 
            this.menuDeactivateAuthenticator.Name = "menuDeactivateAuthenticator";
            this.menuDeactivateAuthenticator.Size = new System.Drawing.Size(254, 22);
            this.menuDeactivateAuthenticator.Text = "Деактивировать аутентификатор";
            this.menuDeactivateAuthenticator.Click += new System.EventHandler(this.menuDeactivateAuthenticator_Click);
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
            // timerTradesPopup
            // 
            this.timerTradesPopup.Enabled = true;
            this.timerTradesPopup.Interval = 5000;
            this.timerTradesPopup.Tick += new System.EventHandler(this.timerTradesPopup_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(183, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(163, 18);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAccSearch
            // 
            this.txtAccSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccSearch.Location = new System.Drawing.Point(63, 370);
            this.txtAccSearch.Name = "txtAccSearch";
            this.txtAccSearch.Size = new System.Drawing.Size(276, 22);
            this.txtAccSearch.TabIndex = 12;
            this.txtAccSearch.TextChanged += new System.EventHandler(this.txtAccSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Фильтр:";
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnCopy;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 397);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAccSearch);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupAccount);
            this.Controls.Add(this.btnManageEncryption);
            this.Controls.Add(this.listAccounts);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSteamLogin);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steam Desktop Authenticator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupAccount.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.menuStripTray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSteamLogin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar pbTimeout;
        private System.Windows.Forms.TextBox txtLoginToken;
        private System.Windows.Forms.ListBox listAccounts;
        private System.Windows.Forms.Timer timerSteamGuard;
        private System.Windows.Forms.Button btnTradeConfirmations;
        private System.Windows.Forms.Button btnManageEncryption;
        private System.Windows.Forms.GroupBox groupAccount;
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
        private System.Windows.Forms.Timer timerTradesPopup;
        private System.Windows.Forms.ToolStripComboBox trayAccountList;
        private System.Windows.Forms.ToolStripMenuItem importAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuImportMaFile;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtAccSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuDeactivateAuthenticator;
        private System.Windows.Forms.ToolStripMenuItem menuRefreshSession;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button btnCopy;
    }
}

