namespace FF.WindowsERPClient
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.splitContLogin = new System.Windows.Forms.SplitContainer();
            this.lblTestVersion = new System.Windows.Forms.Label();
            this.lblVersionNo = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContChangePw = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.btnResetCancel = new System.Windows.Forms.Button();
            this.btnResetLogin = new System.Windows.Forms.Button();
            this.txtConfirmPw = new System.Windows.Forms.TextBox();
            this.txtNewPw = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContLogin)).BeginInit();
            this.splitContLogin.Panel2.SuspendLayout();
            this.splitContLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContChangePw)).BeginInit();
            this.splitContChangePw.Panel2.SuspendLayout();
            this.splitContChangePw.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContLogin
            // 
            this.splitContLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContLogin.Location = new System.Drawing.Point(0, 0);
            this.splitContLogin.Name = "splitContLogin";
            // 
            // splitContLogin.Panel1
            // 
            this.splitContLogin.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContLogin.Panel1.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources._1357118961_Login_Manager;
            this.splitContLogin.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            // 
            // splitContLogin.Panel2
            // 
            this.splitContLogin.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContLogin.Panel2.Controls.Add(this.lblTestVersion);
            this.splitContLogin.Panel2.Controls.Add(this.lblVersionNo);
            this.splitContLogin.Panel2.Controls.Add(this.btnCancel);
            this.splitContLogin.Panel2.Controls.Add(this.btnLogin);
            this.splitContLogin.Panel2.Controls.Add(this.cmbCompany);
            this.splitContLogin.Panel2.Controls.Add(this.txtPw);
            this.splitContLogin.Panel2.Controls.Add(this.txtUser);
            this.splitContLogin.Panel2.Controls.Add(this.label3);
            this.splitContLogin.Panel2.Controls.Add(this.label2);
            this.splitContLogin.Panel2.Controls.Add(this.label1);
            this.splitContLogin.Size = new System.Drawing.Size(410, 184);
            this.splitContLogin.SplitterDistance = 136;
            this.splitContLogin.TabIndex = 0;
            // 
            // lblTestVersion
            // 
            this.lblTestVersion.AutoSize = true;
            this.lblTestVersion.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestVersion.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTestVersion.Location = new System.Drawing.Point(5, 5);
            this.lblTestVersion.Name = "lblTestVersion";
            this.lblTestVersion.Size = new System.Drawing.Size(199, 18);
            this.lblTestVersion.TabIndex = 8;
            this.lblTestVersion.Text = "*** Test Version - II ***";
            this.lblTestVersion.Visible = false;
            // 
            // lblVersionNo
            // 
            this.lblVersionNo.AutoSize = true;
            this.lblVersionNo.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionNo.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lblVersionNo.Location = new System.Drawing.Point(177, 169);
            this.lblVersionNo.Name = "lblVersionNo";
            this.lblVersionNo.Size = new System.Drawing.Size(84, 11);
            this.lblVersionNo.TabIndex = 7;
            this.lblVersionNo.Text = "Version - 1:0:0:100";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(172, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Location = new System.Drawing.Point(88, 133);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "&Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(88, 106);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(159, 21);
            this.cmbCompany.TabIndex = 2;
            this.cmbCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCompany_KeyDown);
            // 
            // txtPw
            // 
            this.txtPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPw.Location = new System.Drawing.Point(88, 77);
            this.txtPw.MaxLength = 25;
            this.txtPw.Name = "txtPw";
            this.txtPw.PasswordChar = '*';
            this.txtPw.Size = new System.Drawing.Size(159, 21);
            this.txtPw.TabIndex = 1;
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPw_KeyDown);
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Location = new System.Drawing.Point(88, 48);
            this.txtUser.MaxLength = 25;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(159, 21);
            this.txtUser.TabIndex = 0;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            this.txtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUser_KeyDown);
            this.txtUser.Leave += new System.EventHandler(this.txtUser_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Company";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name";
            // 
            // splitContChangePw
            // 
            this.splitContChangePw.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContChangePw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContChangePw.Location = new System.Drawing.Point(0, 0);
            this.splitContChangePw.Name = "splitContChangePw";
            // 
            // splitContChangePw.Panel1
            // 
            this.splitContChangePw.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContChangePw.Panel1.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.resetpw_icon;
            this.splitContChangePw.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            // 
            // splitContChangePw.Panel2
            // 
            this.splitContChangePw.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContChangePw.Panel2.Controls.Add(this.label4);
            this.splitContChangePw.Panel2.Controls.Add(this.btnResetCancel);
            this.splitContChangePw.Panel2.Controls.Add(this.btnResetLogin);
            this.splitContChangePw.Panel2.Controls.Add(this.txtConfirmPw);
            this.splitContChangePw.Panel2.Controls.Add(this.txtNewPw);
            this.splitContChangePw.Panel2.Controls.Add(this.label7);
            this.splitContChangePw.Panel2.Controls.Add(this.label8);
            this.splitContChangePw.Size = new System.Drawing.Size(410, 184);
            this.splitContChangePw.SplitterDistance = 136;
            this.splitContChangePw.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Goldenrod;
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Change password . . . .";
            // 
            // btnResetCancel
            // 
            this.btnResetCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetCancel.Location = new System.Drawing.Point(172, 133);
            this.btnResetCancel.Name = "btnResetCancel";
            this.btnResetCancel.Size = new System.Drawing.Size(75, 23);
            this.btnResetCancel.TabIndex = 4;
            this.btnResetCancel.Text = "&Cancel";
            this.btnResetCancel.UseVisualStyleBackColor = true;
            this.btnResetCancel.Click += new System.EventHandler(this.btnResetCancel_Click);
            // 
            // btnResetLogin
            // 
            this.btnResetLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetLogin.Location = new System.Drawing.Point(88, 133);
            this.btnResetLogin.Name = "btnResetLogin";
            this.btnResetLogin.Size = new System.Drawing.Size(75, 23);
            this.btnResetLogin.TabIndex = 3;
            this.btnResetLogin.Text = "&Login";
            this.btnResetLogin.UseVisualStyleBackColor = true;
            this.btnResetLogin.Click += new System.EventHandler(this.btnResetLogin_Click);
            // 
            // txtConfirmPw
            // 
            this.txtConfirmPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPw.Location = new System.Drawing.Point(102, 77);
            this.txtConfirmPw.MaxLength = 25;
            this.txtConfirmPw.Name = "txtConfirmPw";
            this.txtConfirmPw.PasswordChar = '*';
            this.txtConfirmPw.Size = new System.Drawing.Size(145, 21);
            this.txtConfirmPw.TabIndex = 1;
            this.txtConfirmPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConfirmPw_KeyDown);
            // 
            // txtNewPw
            // 
            this.txtNewPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPw.Location = new System.Drawing.Point(102, 48);
            this.txtNewPw.MaxLength = 25;
            this.txtNewPw.Name = "txtNewPw";
            this.txtNewPw.PasswordChar = '*';
            this.txtNewPw.Size = new System.Drawing.Size(145, 21);
            this.txtNewPw.TabIndex = 0;
            this.txtNewPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPw_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Confirm password";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "New password";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(410, 184);
            this.Controls.Add(this.splitContLogin);
            this.Controls.Add(this.splitContChangePw);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Login_Load);
            this.splitContLogin.Panel2.ResumeLayout(false);
            this.splitContLogin.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContLogin)).EndInit();
            this.splitContLogin.ResumeLayout(false);
            this.splitContChangePw.Panel2.ResumeLayout(false);
            this.splitContChangePw.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContChangePw)).EndInit();
            this.splitContChangePw.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContLogin;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblVersionNo;
        private System.Windows.Forms.Label lblTestVersion;
        private System.Windows.Forms.SplitContainer splitContChangePw;
        private System.Windows.Forms.Button btnResetCancel;
        private System.Windows.Forms.Button btnResetLogin;
        private System.Windows.Forms.TextBox txtConfirmPw;
        private System.Windows.Forms.TextBox txtNewPw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
    }
}