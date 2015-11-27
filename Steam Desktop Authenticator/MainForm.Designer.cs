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
            this.btnSteamLogin = new System.Windows.Forms.Button();
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
            // MainForm
            // 
            this.AcceptButton = this.btnSteamLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 499);
            this.Controls.Add(this.btnSteamLogin);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.SystemShadow;
            this.Text = "Steam Desktop Authenticator";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSteamLogin;
    }
}

