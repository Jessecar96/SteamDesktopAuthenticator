namespace Steam_Desktop_Authenticator
{
    using MetroFramework;
    using MetroFramework.Forms;

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
            this.listConfirmations = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAcceptConfirmation = new System.Windows.Forms.Button();
            this.btnDenyConfirmation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listConfirmations
            // 
            this.listConfirmations.FormattingEnabled = true;
            this.listConfirmations.Items.AddRange(new object[] {
            "test",
            "test"});
            this.listConfirmations.Location = new System.Drawing.Point(23, 94);
            this.listConfirmations.Name = "listConfirmations";
            this.listConfirmations.Size = new System.Drawing.Size(305, 134);
            this.listConfirmations.TabIndex = 3;
            this.listConfirmations.SelectedValueChanged += new System.EventHandler(this.listAccounts_SelectedValueChanged);
            // 
            // btnAcceptConfirmation
            // 
            this.btnAcceptConfirmation.Enabled = false;
            this.btnAcceptConfirmation.Location = new System.Drawing.Point(23, 58);
            this.btnAcceptConfirmation.Name = "btnAcceptConfirmation";
            this.btnAcceptConfirmation.Size = new System.Drawing.Size(138, 31);
            this.btnAcceptConfirmation.TabIndex = 4;
            this.btnAcceptConfirmation.Text = "Accept";
            this.btnAcceptConfirmation.UseVisualStyleBackColor = true;
            this.btnAcceptConfirmation.Click += new System.EventHandler(this.btnAcceptConfirmation_Click);
            // 
            // btnDenyConfirmation
            // 
            this.btnDenyConfirmation.Location = new System.Drawing.Point(190, 58);
            this.btnDenyConfirmation.Name = "btnDenyConfirmation";
            this.btnDenyConfirmation.Size = new System.Drawing.Size(138, 31);
            this.btnDenyConfirmation.TabIndex = 5;
            this.btnDenyConfirmation.Text = "Deny";
            this.btnDenyConfirmation.UseVisualStyleBackColor = true;
            this.btnDenyConfirmation.Click += new System.EventHandler(this.btnDenyConfirmation_Click);
            // 
            // ConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 247);
            this.Controls.Add(this.btnDenyConfirmation);
            this.Controls.Add(this.btnAcceptConfirmation);
            this.Controls.Add(this.listConfirmations);
            this.MaximizeBox = false;
            this.Name = "ConfirmationForm";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.SystemShadow;
            this.Text = "Trade Confirmations";
            this.Load += new System.EventHandler(this.ConfirmationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listConfirmations;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAcceptConfirmation;
        private System.Windows.Forms.Button btnDenyConfirmation;
    }
}

