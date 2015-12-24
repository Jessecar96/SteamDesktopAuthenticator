namespace Steam_Desktop_Authenticator
{
    partial class WelcomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnImportConfig = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAndroidImport = new System.Windows.Forms.Button();
            this.btnJustStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to\r\nSteam Desktop Authenticator";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnImportConfig
            // 
            this.btnImportConfig.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportConfig.Location = new System.Drawing.Point(12, 241);
            this.btnImportConfig.Name = "btnImportConfig";
            this.btnImportConfig.Size = new System.Drawing.Size(366, 51);
            this.btnImportConfig.TabIndex = 1;
            this.btnImportConfig.Text = "I already setup Steam Desktop Authenticator in another location on this PC and I " +
    "want to import its account(s).\r\n";
            this.btnImportConfig.UseVisualStyleBackColor = true;
            this.btnImportConfig.Click += new System.EventHandler(this.btnImportConfig_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select an item to get started:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAndroidImport
            // 
            this.btnAndroidImport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAndroidImport.Location = new System.Drawing.Point(12, 298);
            this.btnAndroidImport.Name = "btnAndroidImport";
            this.btnAndroidImport.Size = new System.Drawing.Size(366, 51);
            this.btnAndroidImport.TabIndex = 3;
            this.btnAndroidImport.Text = "I have an Android device and want to \r\nimport my Steam account(s) from the Steam " +
    "app.";
            this.btnAndroidImport.UseVisualStyleBackColor = true;
            this.btnAndroidImport.Click += new System.EventHandler(this.btnAndroidImport_Click);
            // 
            // btnJustStart
            // 
            this.btnJustStart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJustStart.Location = new System.Drawing.Point(12, 355);
            this.btnJustStart.Name = "btnJustStart";
            this.btnJustStart.Size = new System.Drawing.Size(366, 51);
            this.btnJustStart.TabIndex = 4;
            this.btnJustStart.Text = "This is my first time and \r\nI just want to sign into my Steam Account(s).";
            this.btnJustStart.UseVisualStyleBackColor = true;
            this.btnJustStart.Click += new System.EventHandler(this.btnJustStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(480, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "If you are using old .maFile\'s you will need to import them using the";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(415, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "File Import maFile menu. Now press: This is my first time ...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(77, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(371, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "This app now uses .saFiles instead of .maFiles !!!";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(384, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 21);
            this.label7.TabIndex = 9;
            this.label7.Text = "for .saFiles";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(384, 314);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 21);
            this.label8.TabIndex = 10;
            this.label8.Text = "for Android";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(384, 369);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 21);
            this.label9.TabIndex = 11;
            this.label9.Text = "for All";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(381, 266);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 21);
            this.label10.TabIndex = 12;
            this.label10.Text = "(fast import all data)";
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 417);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnJustStart);
            this.Controls.Add(this.btnAndroidImport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnImportConfig);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steam Desktop Authenticator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImportConfig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAndroidImport;
        private System.Windows.Forms.Button btnJustStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}
