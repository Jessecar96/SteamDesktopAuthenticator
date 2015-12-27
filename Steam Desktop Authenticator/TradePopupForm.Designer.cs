namespace Steam_Desktop_Authenticator
{
    partial class TradePopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradePopupForm));
            this.lblDesc = new System.Windows.Forms.Label();
            this.btnDeny = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.label_Info = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newConfirmationToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnPopupConfBack = new System.Windows.Forms.Button();
            this.BtnPopupConfNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelConfirmationNo = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDesc
            // 
            this.lblDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDesc.Location = new System.Drawing.Point(29, 71);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(226, 19);
            this.lblDesc.TabIndex = 1;
            this.lblDesc.Text = "UserLongestNameSupportedBySteam";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeny
            // 
            this.btnDeny.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDeny.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeny.Location = new System.Drawing.Point(27, 110);
            this.btnDeny.Name = "btnDeny";
            this.btnDeny.Size = new System.Drawing.Size(76, 32);
            this.btnDeny.TabIndex = 2;
            this.btnDeny.Text = "Deny";
            this.btnDeny.UseVisualStyleBackColor = false;
            this.btnDeny.Click += new System.EventHandler(this.btnDeny_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(182, 110);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(76, 32);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Location = new System.Drawing.Point(30, 88);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(225, 19);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAccount
            // 
            this.lblAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAccount.Location = new System.Drawing.Point(30, 31);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(225, 19);
            this.lblAccount.TabIndex = 4;
            this.lblAccount.Text = "account name";
            this.lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Info
            // 
            this.label_Info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Info.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Info.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_Info.Location = new System.Drawing.Point(27, 145);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(231, 14);
            this.label_Info.TabIndex = 5;
            this.label_Info.Text = "info total confirmations";
            this.label_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newConfirmationToolStripMenuItem,
            this.xToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(239, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3);
            this.menuStrip1.Size = new System.Drawing.Size(285, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newConfirmationToolStripMenuItem
            // 
            this.newConfirmationToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.newConfirmationToolStripMenuItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.newConfirmationToolStripMenuItem.CausesValidation = false;
            this.newConfirmationToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.newConfirmationToolStripMenuItem.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.newConfirmationToolStripMenuItem.Name = "newConfirmationToolStripMenuItem";
            this.newConfirmationToolStripMenuItem.ReadOnly = true;
            this.newConfirmationToolStripMenuItem.Size = new System.Drawing.Size(120, 16);
            this.newConfirmationToolStripMenuItem.Text = "New Confirmation:";
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(26, 18);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // BtnPopupConfBack
            // 
            this.BtnPopupConfBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnPopupConfBack.FlatAppearance.BorderSize = 0;
            this.BtnPopupConfBack.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnPopupConfBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPopupConfBack.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPopupConfBack.Location = new System.Drawing.Point(1, 25);
            this.BtnPopupConfBack.Name = "BtnPopupConfBack";
            this.BtnPopupConfBack.Padding = new System.Windows.Forms.Padding(2, 0, 0, 35);
            this.BtnPopupConfBack.Size = new System.Drawing.Size(23, 139);
            this.BtnPopupConfBack.TabIndex = 7;
            this.BtnPopupConfBack.Text = "<";
            this.BtnPopupConfBack.UseVisualStyleBackColor = true;
            this.BtnPopupConfBack.Click += new System.EventHandler(this.PopupConfBack_Click);
            // 
            // BtnPopupConfNext
            // 
            this.BtnPopupConfNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPopupConfNext.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnPopupConfNext.FlatAppearance.BorderSize = 0;
            this.BtnPopupConfNext.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnPopupConfNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPopupConfNext.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPopupConfNext.Location = new System.Drawing.Point(261, 25);
            this.BtnPopupConfNext.Name = "BtnPopupConfNext";
            this.BtnPopupConfNext.Padding = new System.Windows.Forms.Padding(2, 0, 0, 31);
            this.BtnPopupConfNext.Size = new System.Drawing.Size(23, 139);
            this.BtnPopupConfNext.TabIndex = 8;
            this.BtnPopupConfNext.Text = ">";
            this.BtnPopupConfNext.UseVisualStyleBackColor = true;
            this.BtnPopupConfNext.Click += new System.EventHandler(this.PopupConfNext_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(30, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "trade with:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelConfirmationNo
            // 
            this.labelConfirmationNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelConfirmationNo.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelConfirmationNo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelConfirmationNo.Location = new System.Drawing.Point(109, 117);
            this.labelConfirmationNo.Name = "labelConfirmationNo";
            this.labelConfirmationNo.Size = new System.Drawing.Size(67, 19);
            this.labelConfirmationNo.TabIndex = 10;
            this.labelConfirmationNo.Text = "no. 1";
            this.labelConfirmationNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TradePopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(285, 165);
            this.ControlBox = false;
            this.Controls.Add(this.labelConfirmationNo);
            this.Controls.Add(this.BtnPopupConfNext);
            this.Controls.Add(this.BtnPopupConfBack);
            this.Controls.Add(this.label_Info);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnDeny);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TradePopupForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "New Confirmation";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TradePopup_FormClosing);
            this.Load += new System.EventHandler(this.TradePopupForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Button btnDeny;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox newConfirmationToolStripMenuItem;
        private System.Windows.Forms.Button BtnPopupConfBack;
        private System.Windows.Forms.Button BtnPopupConfNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelConfirmationNo;
    }
}
