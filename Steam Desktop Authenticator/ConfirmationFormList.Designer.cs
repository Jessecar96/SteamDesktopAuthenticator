namespace Steam_Desktop_Authenticator
{

    partial class ConfirmationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmationForm));
            this.listConfirmations = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAcceptConfirmation = new System.Windows.Forms.Button();
            this.btnDenyConfirmation = new System.Windows.Forms.Button();
            this.btnRefreshConfirmation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listConfirmations
            // 
            this.listConfirmations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listConfirmations.FormattingEnabled = true;
            this.listConfirmations.Items.AddRange(new object[] {
            "test",
            "test"});
            this.listConfirmations.Location = new System.Drawing.Point(10, 49);
            this.listConfirmations.Name = "listConfirmations";
            this.listConfirmations.Size = new System.Drawing.Size(333, 186);
            this.listConfirmations.TabIndex = 3;
            this.listConfirmations.SelectedValueChanged += new System.EventHandler(this.listAccounts_SelectedValueChanged);
            // 
            // btnAcceptConfirmation
            // 
            this.btnAcceptConfirmation.Enabled = false;
            this.btnAcceptConfirmation.Location = new System.Drawing.Point(8, 9);
            this.btnAcceptConfirmation.Name = "btnAcceptConfirmation";
            this.btnAcceptConfirmation.Size = new System.Drawing.Size(119, 31);
            this.btnAcceptConfirmation.TabIndex = 4;
            this.btnAcceptConfirmation.Text = "Accept";
            this.btnAcceptConfirmation.UseVisualStyleBackColor = true;
            this.btnAcceptConfirmation.Click += new System.EventHandler(this.btnAcceptConfirmation_Click);
            // 
            // btnDenyConfirmation
            // 
            this.btnDenyConfirmation.Enabled = false;
            this.btnDenyConfirmation.Location = new System.Drawing.Point(132, 9);
            this.btnDenyConfirmation.Name = "btnDenyConfirmation";
            this.btnDenyConfirmation.Size = new System.Drawing.Size(118, 31);
            this.btnDenyConfirmation.TabIndex = 5;
            this.btnDenyConfirmation.Text = "Deny";
            this.btnDenyConfirmation.UseVisualStyleBackColor = true;
            this.btnDenyConfirmation.Click += new System.EventHandler(this.btnDenyConfirmation_Click);
            // 
            // btnRefreshConfirmation
            // 
            this.btnRefreshConfirmation.Location = new System.Drawing.Point(255, 9);
            this.btnRefreshConfirmation.Name = "btnRefreshConfirmation";
            this.btnRefreshConfirmation.Size = new System.Drawing.Size(88, 31);
            this.btnRefreshConfirmation.TabIndex = 6;
            this.btnRefreshConfirmation.Text = "Refresh";
            this.btnRefreshConfirmation.UseVisualStyleBackColor = true;
            this.btnRefreshConfirmation.Click += new System.EventHandler(this.btnRefreshConfirmation_Click);
            // 
            // ConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 243);
            this.Controls.Add(this.btnRefreshConfirmation);
            this.Controls.Add(this.btnDenyConfirmation);
            this.Controls.Add(this.btnAcceptConfirmation);
            this.Controls.Add(this.listConfirmations);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(367, 281);
            this.Name = "ConfirmationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trade Confirmations List";
            this.Load += new System.EventHandler(this.ConfirmationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listConfirmations;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAcceptConfirmation;
        private System.Windows.Forms.Button btnDenyConfirmation;
        private System.Windows.Forms.Button btnRefreshConfirmation;
    }
}

