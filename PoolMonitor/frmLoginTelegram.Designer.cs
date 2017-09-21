namespace PoolMonitor
{
    partial class frmLoginTelegram
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbTelegram = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.btSendCode = new System.Windows.Forms.Button();
            this.btLogin = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbTelegram);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TelegramID";
            // 
            // tbTelegram
            // 
            this.tbTelegram.Location = new System.Drawing.Point(6, 19);
            this.tbTelegram.Name = "tbTelegram";
            this.tbTelegram.Size = new System.Drawing.Size(131, 20);
            this.tbTelegram.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbCode);
            this.groupBox2.Location = new System.Drawing.Point(155, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Code";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(6, 19);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(130, 20);
            this.tbCode.TabIndex = 0;
            // 
            // btSendCode
            // 
            this.btSendCode.Location = new System.Drawing.Point(54, 80);
            this.btSendCode.Name = "btSendCode";
            this.btSendCode.Size = new System.Drawing.Size(75, 23);
            this.btSendCode.TabIndex = 2;
            this.btSendCode.Text = "Send Code";
            this.btSendCode.UseVisualStyleBackColor = true;
            this.btSendCode.Click += new System.EventHandler(this.btSendCode_Click);
            // 
            // btLogin
            // 
            this.btLogin.Location = new System.Drawing.Point(182, 80);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(75, 23);
            this.btLogin.TabIndex = 3;
            this.btLogin.Text = "Login";
            this.btLogin.UseVisualStyleBackColor = true;
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // frmLoginTelegram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 115);
            this.Controls.Add(this.btLogin);
            this.Controls.Add(this.btSendCode);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoginTelegram";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLoginTelegram";
            this.Load += new System.EventHandler(this.frmLoginTelegram_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbTelegram;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Button btSendCode;
        private System.Windows.Forms.Button btLogin;
    }
}