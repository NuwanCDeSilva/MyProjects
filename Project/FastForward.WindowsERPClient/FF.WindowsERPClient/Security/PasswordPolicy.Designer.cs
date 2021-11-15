namespace FF.WindowsERPClient.Security
{
    partial class PasswordPolicy
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnShowDic = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.pnlPwDic = new System.Windows.Forms.Panel();
            this.optDic0 = new System.Windows.Forms.RadioButton();
            this.optDic1 = new System.Windows.Forms.RadioButton();
            this.btnAddDicWord = new System.Windows.Forms.Button();
            this.txtDicWord = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkPwDictionary = new System.Windows.Forms.CheckBox();
            this.chkPwComplexity = new System.Windows.Forms.CheckBox();
            this.chkPwNotMatchUser = new System.Windows.Forms.CheckBox();
            this.txtIdenticalCharactors = new System.Windows.Forms.TextBox();
            this.txtLockUserAttemtps = new System.Windows.Forms.TextBox();
            this.txtMinPwLength = new System.Windows.Forms.TextBox();
            this.txtPwHist = new System.Windows.Forms.TextBox();
            this.txtMinPw = new System.Windows.Forms.TextBox();
            this.txtMaxPw = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlPwDic.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(570, 453);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnShowDic);
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.pnlPwDic);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.chkPwDictionary);
            this.tabPage1.Controls.Add(this.chkPwComplexity);
            this.tabPage1.Controls.Add(this.chkPwNotMatchUser);
            this.tabPage1.Controls.Add(this.txtIdenticalCharactors);
            this.tabPage1.Controls.Add(this.txtLockUserAttemtps);
            this.tabPage1.Controls.Add(this.txtMinPwLength);
            this.tabPage1.Controls.Add(this.txtPwHist);
            this.tabPage1.Controls.Add(this.txtMinPw);
            this.tabPage1.Controls.Add(this.txtMaxPw);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(562, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Password";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnShowDic
            // 
            this.btnShowDic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnShowDic.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.btnShowDic.Location = new System.Drawing.Point(380, 204);
            this.btnShowDic.Name = "btnShowDic";
            this.btnShowDic.Size = new System.Drawing.Size(24, 22);
            this.btnShowDic.TabIndex = 14;
            this.btnShowDic.UseVisualStyleBackColor = true;
            this.btnShowDic.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnUpdate.Location = new System.Drawing.Point(486, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(73, 34);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "&Apply";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // pnlPwDic
            // 
            this.pnlPwDic.Controls.Add(this.optDic0);
            this.pnlPwDic.Controls.Add(this.optDic1);
            this.pnlPwDic.Controls.Add(this.btnAddDicWord);
            this.pnlPwDic.Controls.Add(this.txtDicWord);
            this.pnlPwDic.Controls.Add(this.label16);
            this.pnlPwDic.Location = new System.Drawing.Point(9, 230);
            this.pnlPwDic.Name = "pnlPwDic";
            this.pnlPwDic.Size = new System.Drawing.Size(546, 194);
            this.pnlPwDic.TabIndex = 12;
            this.pnlPwDic.Visible = false;
            // 
            // optDic0
            // 
            this.optDic0.AutoSize = true;
            this.optDic0.Location = new System.Drawing.Point(376, 13);
            this.optDic0.Name = "optDic0";
            this.optDic0.Size = new System.Drawing.Size(65, 17);
            this.optDic0.TabIndex = 10;
            this.optDic0.TabStop = true;
            this.optDic0.Text = "Disabled";
            this.optDic0.UseVisualStyleBackColor = true;
            // 
            // optDic1
            // 
            this.optDic1.AutoSize = true;
            this.optDic1.Location = new System.Drawing.Point(304, 13);
            this.optDic1.Name = "optDic1";
            this.optDic1.Size = new System.Drawing.Size(63, 17);
            this.optDic1.TabIndex = 9;
            this.optDic1.TabStop = true;
            this.optDic1.Text = "Enabled";
            this.optDic1.UseVisualStyleBackColor = true;
            // 
            // btnAddDicWord
            // 
            this.btnAddDicWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddDicWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDicWord.Location = new System.Drawing.Point(219, 8);
            this.btnAddDicWord.Name = "btnAddDicWord";
            this.btnAddDicWord.Size = new System.Drawing.Size(62, 26);
            this.btnAddDicWord.TabIndex = 8;
            this.btnAddDicWord.Text = "&Add";
            this.btnAddDicWord.UseVisualStyleBackColor = false;
            // 
            // txtDicWord
            // 
            this.txtDicWord.BackColor = System.Drawing.Color.White;
            this.txtDicWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDicWord.ForeColor = System.Drawing.Color.Blue;
            this.txtDicWord.Location = new System.Drawing.Point(70, 11);
            this.txtDicWord.MaxLength = 10;
            this.txtDicWord.Name = "txtDicWord";
            this.txtDicWord.Size = new System.Drawing.Size(130, 21);
            this.txtDicWord.TabIndex = 7;
            this.txtDicWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Brown;
            this.label16.Location = new System.Drawing.Point(3, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Exact word";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Purple;
            this.label15.Location = new System.Drawing.Point(382, 142);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(160, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "Consecutive dentical characters";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Purple;
            this.label14.Location = new System.Drawing.Point(382, 118);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Attempts";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Purple;
            this.label13.Location = new System.Drawing.Point(382, 94);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Characters";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Purple;
            this.label12.Location = new System.Drawing.Point(382, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(121, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Passwords remembered";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Purple;
            this.label11.Location = new System.Drawing.Point(382, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Days";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Purple;
            this.label10.Location = new System.Drawing.Point(382, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Days";
            // 
            // chkPwDictionary
            // 
            this.chkPwDictionary.AutoSize = true;
            this.chkPwDictionary.BackColor = System.Drawing.SystemColors.Control;
            this.chkPwDictionary.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chkPwDictionary.Enabled = false;
            this.chkPwDictionary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPwDictionary.ForeColor = System.Drawing.Color.SlateGray;
            this.chkPwDictionary.Location = new System.Drawing.Point(311, 207);
            this.chkPwDictionary.Name = "chkPwDictionary";
            this.chkPwDictionary.Size = new System.Drawing.Size(63, 17);
            this.chkPwDictionary.TabIndex = 9;
            this.chkPwDictionary.Text = "Disabled";
            this.chkPwDictionary.UseVisualStyleBackColor = false;
            this.chkPwDictionary.Visible = false;
            this.chkPwDictionary.CheckedChanged += new System.EventHandler(this.chkPwDictionary_CheckedChanged);
            // 
            // chkPwComplexity
            // 
            this.chkPwComplexity.AutoSize = true;
            this.chkPwComplexity.BackColor = System.Drawing.SystemColors.Control;
            this.chkPwComplexity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPwComplexity.ForeColor = System.Drawing.Color.Blue;
            this.chkPwComplexity.Location = new System.Drawing.Point(311, 184);
            this.chkPwComplexity.Name = "chkPwComplexity";
            this.chkPwComplexity.Size = new System.Drawing.Size(63, 17);
            this.chkPwComplexity.TabIndex = 8;
            this.chkPwComplexity.Text = "Disabled";
            this.chkPwComplexity.UseVisualStyleBackColor = false;
            this.chkPwComplexity.CheckedChanged += new System.EventHandler(this.chkPwComplexity_CheckedChanged);
            // 
            // chkPwNotMatchUser
            // 
            this.chkPwNotMatchUser.AutoSize = true;
            this.chkPwNotMatchUser.BackColor = System.Drawing.SystemColors.Control;
            this.chkPwNotMatchUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPwNotMatchUser.ForeColor = System.Drawing.Color.Blue;
            this.chkPwNotMatchUser.Location = new System.Drawing.Point(311, 161);
            this.chkPwNotMatchUser.Name = "chkPwNotMatchUser";
            this.chkPwNotMatchUser.Size = new System.Drawing.Size(63, 17);
            this.chkPwNotMatchUser.TabIndex = 7;
            this.chkPwNotMatchUser.Text = "Disabled";
            this.chkPwNotMatchUser.UseVisualStyleBackColor = false;
            this.chkPwNotMatchUser.CheckedChanged += new System.EventHandler(this.chkPwNotMatchUser_CheckedChanged);
            // 
            // txtIdenticalCharactors
            // 
            this.txtIdenticalCharactors.BackColor = System.Drawing.Color.White;
            this.txtIdenticalCharactors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIdenticalCharactors.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdenticalCharactors.ForeColor = System.Drawing.Color.Blue;
            this.txtIdenticalCharactors.Location = new System.Drawing.Point(311, 138);
            this.txtIdenticalCharactors.MaxLength = 3;
            this.txtIdenticalCharactors.Name = "txtIdenticalCharactors";
            this.txtIdenticalCharactors.Size = new System.Drawing.Size(65, 21);
            this.txtIdenticalCharactors.TabIndex = 6;
            this.txtIdenticalCharactors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLockUserAttemtps
            // 
            this.txtLockUserAttemtps.BackColor = System.Drawing.Color.White;
            this.txtLockUserAttemtps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLockUserAttemtps.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLockUserAttemtps.ForeColor = System.Drawing.Color.Blue;
            this.txtLockUserAttemtps.Location = new System.Drawing.Point(311, 114);
            this.txtLockUserAttemtps.MaxLength = 3;
            this.txtLockUserAttemtps.Name = "txtLockUserAttemtps";
            this.txtLockUserAttemtps.Size = new System.Drawing.Size(65, 21);
            this.txtLockUserAttemtps.TabIndex = 5;
            this.txtLockUserAttemtps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMinPwLength
            // 
            this.txtMinPwLength.BackColor = System.Drawing.Color.White;
            this.txtMinPwLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinPwLength.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMinPwLength.ForeColor = System.Drawing.Color.Blue;
            this.txtMinPwLength.Location = new System.Drawing.Point(311, 90);
            this.txtMinPwLength.MaxLength = 3;
            this.txtMinPwLength.Name = "txtMinPwLength";
            this.txtMinPwLength.Size = new System.Drawing.Size(65, 21);
            this.txtMinPwLength.TabIndex = 4;
            this.txtMinPwLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPwHist
            // 
            this.txtPwHist.BackColor = System.Drawing.Color.White;
            this.txtPwHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPwHist.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPwHist.ForeColor = System.Drawing.Color.Blue;
            this.txtPwHist.Location = new System.Drawing.Point(311, 66);
            this.txtPwHist.MaxLength = 3;
            this.txtPwHist.Name = "txtPwHist";
            this.txtPwHist.Size = new System.Drawing.Size(65, 21);
            this.txtPwHist.TabIndex = 3;
            this.txtPwHist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMinPw
            // 
            this.txtMinPw.BackColor = System.Drawing.Color.White;
            this.txtMinPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinPw.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMinPw.ForeColor = System.Drawing.Color.Blue;
            this.txtMinPw.Location = new System.Drawing.Point(311, 42);
            this.txtMinPw.MaxLength = 3;
            this.txtMinPw.Name = "txtMinPw";
            this.txtMinPw.Size = new System.Drawing.Size(65, 21);
            this.txtMinPw.TabIndex = 1;
            this.txtMinPw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMaxPw
            // 
            this.txtMaxPw.BackColor = System.Drawing.Color.White;
            this.txtMaxPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxPw.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaxPw.ForeColor = System.Drawing.Color.Blue;
            this.txtMaxPw.Location = new System.Drawing.Point(311, 18);
            this.txtMaxPw.MaxLength = 3;
            this.txtMaxPw.Name = "txtMaxPw";
            this.txtMaxPw.Size = new System.Drawing.Size(65, 21);
            this.txtMaxPw.TabIndex = 0;
            this.txtMaxPw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Purple;
            this.label9.Location = new System.Drawing.Point(6, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(203, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "No exact word match from the dictionary";
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Purple;
            this.label8.Location = new System.Drawing.Point(6, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(231, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Passwords must meet complexity requirements";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Purple;
            this.label7.Location = new System.Drawing.Point(6, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Password must not match user name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Purple;
            this.label6.Location = new System.Drawing.Point(6, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Number of consecutive identical characters allowed";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Purple;
            this.label5.Location = new System.Drawing.Point(6, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Lock user after failed log in";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Purple;
            this.label4.Location = new System.Drawing.Point(6, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Minimum password length";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Purple;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Enforce password history";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Purple;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimum password age";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Purple;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maximum password age";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(562, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Expiration";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PasswordPolicy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 459);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PasswordPolicy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Security Policy";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnlPwDic.ResumeLayout(false);
            this.pnlPwDic.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkPwDictionary;
        private System.Windows.Forms.CheckBox chkPwComplexity;
        private System.Windows.Forms.CheckBox chkPwNotMatchUser;
        private System.Windows.Forms.TextBox txtIdenticalCharactors;
        private System.Windows.Forms.TextBox txtLockUserAttemtps;
        private System.Windows.Forms.TextBox txtMinPwLength;
        private System.Windows.Forms.TextBox txtPwHist;
        private System.Windows.Forms.TextBox txtMinPw;
        private System.Windows.Forms.TextBox txtMaxPw;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlPwDic;
        private System.Windows.Forms.TextBox txtDicWord;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnAddDicWord;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.RadioButton optDic0;
        private System.Windows.Forms.RadioButton optDic1;
        private System.Windows.Forms.Button btnShowDic;
    }
}