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
            this.btnSave = new System.Windows.Forms.Button();
            this.numPeriodicInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.chkConfirmMarket = new System.Windows.Forms.CheckBox();
            this.chkConfirmTrades = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodicInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // chkPeriodicChecking
            // 
            this.chkPeriodicChecking.AutoSize = true;
            this.chkPeriodicChecking.Location = new System.Drawing.Point(12, 12);
            this.chkPeriodicChecking.Name = "chkPeriodicChecking";
            this.chkPeriodicChecking.Size = new System.Drawing.Size(224, 56);
            this.chkPeriodicChecking.TabIndex = 0;
            this.chkPeriodicChecking.Text = "Периодически проверяйте наличие \r\nновых подтверждений\r\nи показать всплывающее окн" +
    "о, \r\nкогда они приходят";
            this.chkPeriodicChecking.UseVisualStyleBackColor = true;
            this.chkPeriodicChecking.CheckedChanged += new System.EventHandler(this.chkPeriodicChecking_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(12, 248);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(224, 38);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // numPeriodicInterval
            // 
            this.numPeriodicInterval.Location = new System.Drawing.Point(12, 78);
            this.numPeriodicInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPeriodicInterval.Name = "numPeriodicInterval";
            this.numPeriodicInterval.Size = new System.Drawing.Size(41, 22);
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
            this.label1.Location = new System.Drawing.Point(59, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Секунды между проверкой \nдля подтверждения";
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Location = new System.Drawing.Point(12, 108);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(199, 30);
            this.chkCheckAll.TabIndex = 4;
            this.chkCheckAll.Text = "Проверка всех учетных записей \r\nна наличие предложений";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            // 
            // chkConfirmMarket
            // 
            this.chkConfirmMarket.AutoSize = true;
            this.chkConfirmMarket.Location = new System.Drawing.Point(12, 144);
            this.chkConfirmMarket.Name = "chkConfirmMarket";
            this.chkConfirmMarket.Size = new System.Drawing.Size(205, 30);
            this.chkConfirmMarket.TabIndex = 5;
            this.chkConfirmMarket.Text = "Автоматическое подтверждение \r\nSteam ТП сделок";
            this.chkConfirmMarket.UseVisualStyleBackColor = true;
            this.chkConfirmMarket.CheckedChanged += new System.EventHandler(this.chkConfirmMarket_CheckedChanged);
            // 
            // chkConfirmTrades
            // 
            this.chkConfirmTrades.AutoSize = true;
            this.chkConfirmTrades.Location = new System.Drawing.Point(12, 180);
            this.chkConfirmTrades.Name = "chkConfirmTrades";
            this.chkConfirmTrades.Size = new System.Drawing.Size(205, 30);
            this.chkConfirmTrades.TabIndex = 6;
            this.chkConfirmTrades.Text = "Автоматическое подтверждение \r\nсделок";
            this.chkConfirmTrades.UseVisualStyleBackColor = true;
            this.chkConfirmTrades.CheckedChanged += new System.EventHandler(this.chkConfirmTrades_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 298);
            this.Controls.Add(this.chkConfirmTrades);
            this.Controls.Add(this.chkConfirmMarket);
            this.Controls.Add(this.chkCheckAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numPeriodicInterval);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkPeriodicChecking);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodicInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPeriodicChecking;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown numPeriodicInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCheckAll;
        private System.Windows.Forms.CheckBox chkConfirmMarket;
        private System.Windows.Forms.CheckBox chkConfirmTrades;
    }
}