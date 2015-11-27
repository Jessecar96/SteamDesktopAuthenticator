namespace Steam_Desktop_Authenticator
{
    using MetroFramework;
    using MetroFramework.Forms;

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
            this.btnSteamLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbTimeout = new System.Windows.Forms.ProgressBar();
            this.txtLoginToken = new System.Windows.Forms.TextBox();
            this.listAccounts = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnTradeConfirmations = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSteamLogin
            // 
            this.btnSteamLogin.Location = new System.Drawing.Point(23, 63);
            this.btnSteamLogin.Name = "btnSteamLogin";
            this.btnSteamLogin.Size = new System.Drawing.Size(305, 31);
            this.btnSteamLogin.TabIndex = 1;
            this.btnSteamLogin.Text = "Setup new account";
            this.btnSteamLogin.UseVisualStyleBackColor = true;
            this.btnSteamLogin.Click += new System.EventHandler(this.btnSteamLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbTimeout);
            this.groupBox1.Controls.Add(this.txtLoginToken);
            this.groupBox1.Location = new System.Drawing.Point(23, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 85);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login Token";
            // 
            // pbTimeout
            // 
            this.pbTimeout.Location = new System.Drawing.Point(6, 60);
            this.pbTimeout.Name = "pbTimeout";
            this.pbTimeout.Size = new System.Drawing.Size(293, 19);
            this.pbTimeout.TabIndex = 1;
            // 
            // txtLoginToken
            // 
            this.txtLoginToken.BackColor = System.Drawing.SystemColors.Window;
            this.txtLoginToken.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginToken.Location = new System.Drawing.Point(6, 19);
            this.txtLoginToken.Name = "txtLoginToken";
            this.txtLoginToken.ReadOnly = true;
            this.txtLoginToken.Size = new System.Drawing.Size(293, 35);
            this.txtLoginToken.TabIndex = 0;
            this.txtLoginToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listAccounts
            // 
            this.listAccounts.FormattingEnabled = true;
            this.listAccounts.Items.AddRange(new object[] {
            "test",
            "test"});
            this.listAccounts.Location = new System.Drawing.Point(23, 221);
            this.listAccounts.Name = "listAccounts";
            this.listAccounts.Size = new System.Drawing.Size(305, 134);
            this.listAccounts.TabIndex = 3;
            this.listAccounts.SelectedValueChanged += new System.EventHandler(this.listAccounts_SelectedValueChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnTradeConfirmations
            // 
            this.btnTradeConfirmations.Enabled = false;
            this.btnTradeConfirmations.Location = new System.Drawing.Point(23, 185);
            this.btnTradeConfirmations.Name = "btnTradeConfirmations";
            this.btnTradeConfirmations.Size = new System.Drawing.Size(138, 31);
            this.btnTradeConfirmations.TabIndex = 4;
            this.btnTradeConfirmations.Text = "Trade Confirmations";
            this.btnTradeConfirmations.UseVisualStyleBackColor = true;
            this.btnTradeConfirmations.Click += new System.EventHandler(this.btnTradeConfirmations_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(190, 185);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(138, 31);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Remove Authenticator";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnSteamLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 378);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnTradeConfirmations);
            this.Controls.Add(this.listAccounts);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSteamLogin);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.SystemShadow;
            this.Text = "Steam Desktop Authenticator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSteamLogin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar pbTimeout;
        private System.Windows.Forms.TextBox txtLoginToken;
        private System.Windows.Forms.ListBox listAccounts;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTradeConfirmations;
        private System.Windows.Forms.Button btnDelete;
    }
}

