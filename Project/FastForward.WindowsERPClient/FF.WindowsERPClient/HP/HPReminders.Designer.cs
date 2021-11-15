namespace FF.WindowsERPClient.HP
{
    partial class HPReminders
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtAccNo = new System.Windows.Forms.TextBox();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ucHpAccountSummary1 = new FF.WindowsERPClient.UserControls.UcHpAccountSummary();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnActive = new System.Windows.Forms.Button();
            this.txtCustMob = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtManagerMob = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemindDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.gvView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hra_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBackDateInfor = new System.Windows.Forms.Label();
            this.chkView = new System.Windows.Forms.CheckBox();
            this.btnInactive = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.ImgBtnAccountNo = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAccNo
            // 
            this.txtAccNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccNo.Location = new System.Drawing.Point(108, 8);
            this.txtAccNo.Name = "txtAccNo";
            this.txtAccNo.Size = new System.Drawing.Size(100, 21);
            this.txtAccNo.TabIndex = 0;
            this.txtAccNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccNo_KeyDown);
            this.txtAccNo.Leave += new System.EventHandler(this.txtAccNo_Leave);
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.Location = new System.Drawing.Point(237, 10);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(91, 16);
            this.lblAccountNo.TabIndex = 40;
            this.lblAccountNo.Text = "_";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Account No";
            // 
            // ucHpAccountSummary1
            // 
            this.ucHpAccountSummary1.BackColor = System.Drawing.Color.White;
            this.ucHpAccountSummary1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucHpAccountSummary1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucHpAccountSummary1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ucHpAccountSummary1.Location = new System.Drawing.Point(12, 229);
            this.ucHpAccountSummary1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucHpAccountSummary1.Name = "ucHpAccountSummary1";
            this.ucHpAccountSummary1.Size = new System.Drawing.Size(459, 249);
            this.ucHpAccountSummary1.TabIndex = 43;
            // 
            // txtDate
            // 
            this.txtDate.Checked = false;
            this.txtDate.CustomFormat = "dd/MMM/yyyy";
            this.txtDate.Enabled = false;
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDate.Location = new System.Drawing.Point(369, 8);
            this.txtDate.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(104, 21);
            this.txtDate.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(334, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Date";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtMessage);
            this.panel1.Controls.Add(this.btnActive);
            this.panel1.Controls.Add(this.txtCustMob);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtManagerMob);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtRemindDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(4, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 167);
            this.panel1.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Remind Message";
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Location = new System.Drawing.Point(10, 79);
            this.txtMessage.MaxLength = 200;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(387, 82);
            this.txtMessage.TabIndex = 4;
            // 
            // btnActive
            // 
            this.btnActive.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.btnActive.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnActive.Location = new System.Drawing.Point(401, 135);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(61, 26);
            this.btnActive.TabIndex = 5;
            this.btnActive.Text = "Add";
            this.btnActive.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActive.UseVisualStyleBackColor = true;
            this.btnActive.Click += new System.EventHandler(this.btnActive_Click);
            // 
            // txtCustMob
            // 
            this.txtCustMob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustMob.Location = new System.Drawing.Point(334, 29);
            this.txtCustMob.MaxLength = 10;
            this.txtCustMob.Name = "txtCustMob";
            this.txtCustMob.Size = new System.Drawing.Size(100, 21);
            this.txtCustMob.TabIndex = 3;
            this.txtCustMob.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustMob_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Manager Mobile";
            // 
            // txtManagerMob
            // 
            this.txtManagerMob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtManagerMob.Location = new System.Drawing.Point(103, 31);
            this.txtManagerMob.MaxLength = 10;
            this.txtManagerMob.Name = "txtManagerMob";
            this.txtManagerMob.Size = new System.Drawing.Size(100, 21);
            this.txtManagerMob.TabIndex = 2;
            this.txtManagerMob.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtManagerMob_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Customer Mobile";
            // 
            // txtRemindDate
            // 
            this.txtRemindDate.Checked = false;
            this.txtRemindDate.CustomFormat = "dd/MMM/yyyy";
            this.txtRemindDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtRemindDate.Location = new System.Drawing.Point(103, 7);
            this.txtRemindDate.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtRemindDate.Name = "txtRemindDate";
            this.txtRemindDate.Size = new System.Drawing.Size(104, 21);
            this.txtRemindDate.TabIndex = 1;
            this.txtRemindDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemindDate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Remind On";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.SteelBlue;
            this.label14.ForeColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(477, 37);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 14);
            this.label14.TabIndex = 109;
            this.label14.Text = "Reminder Details";
            // 
            // gvView
            // 
            this.gvView.AllowUserToAddRows = false;
            this.gvView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.gvView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvView.ColumnHeadersHeight = 20;
            this.gvView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.Column1,
            this.hra_seq});
            this.gvView.Location = new System.Drawing.Point(477, 54);
            this.gvView.Name = "gvView";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvView.RowHeadersVisible = false;
            this.gvView.RowTemplate.Height = 16;
            this.gvView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvView.Size = new System.Drawing.Size(521, 424);
            this.gvView.TabIndex = 131;
            this.gvView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvView_CellClick);
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "HRA_REF";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn10.HeaderText = "Acc No";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 80;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "HRA_DT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn11.HeaderText = "Remind Date";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 80;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "HRA_RMD";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.NullValue = null;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn12.HeaderText = "Reminder";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 260;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "HRA_STUS";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column1.HeaderText = "Status";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // hra_seq
            // 
            this.hra_seq.DataPropertyName = "hra_seq";
            this.hra_seq.HeaderText = "hra_seq";
            this.hra_seq.Name = "hra_seq";
            this.hra_seq.Visible = false;
            this.hra_seq.Width = 5;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(562, 8);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(117, 21);
            this.cmbStatus.TabIndex = 132;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(511, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 133;
            this.label5.Text = "Status";
            // 
            // lblBackDateInfor
            // 
            this.lblBackDateInfor.BackColor = System.Drawing.Color.SteelBlue;
            this.lblBackDateInfor.ForeColor = System.Drawing.Color.Transparent;
            this.lblBackDateInfor.Location = new System.Drawing.Point(576, 37);
            this.lblBackDateInfor.Name = "lblBackDateInfor";
            this.lblBackDateInfor.Size = new System.Drawing.Size(422, 14);
            this.lblBackDateInfor.TabIndex = 136;
            // 
            // chkView
            // 
            this.chkView.AutoSize = true;
            this.chkView.Location = new System.Drawing.Point(12, 209);
            this.chkView.Name = "chkView";
            this.chkView.Size = new System.Drawing.Size(137, 17);
            this.chkView.TabIndex = 137;
            this.chkView.Text = "Veiw Account Summary";
            this.chkView.UseVisualStyleBackColor = true;
            this.chkView.CheckedChanged += new System.EventHandler(this.chkView_CheckedChanged);
            // 
            // btnInactive
            // 
            this.btnInactive.Image = global::FF.WindowsERPClient.Properties.Resources.EditIcon;
            this.btnInactive.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnInactive.Location = new System.Drawing.Point(837, 6);
            this.btnInactive.Name = "btnInactive";
            this.btnInactive.Size = new System.Drawing.Size(71, 26);
            this.btnInactive.TabIndex = 135;
            this.btnInactive.Text = "Inactive";
            this.btnInactive.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInactive.UseVisualStyleBackColor = true;
            this.btnInactive.Click += new System.EventHandler(this.btnInactive_Click);
            // 
            // btnView
            // 
            this.btnView.Image = global::FF.WindowsERPClient.Properties.Resources.searchicon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnView.Location = new System.Drawing.Point(688, 6);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(61, 26);
            this.btnView.TabIndex = 133;
            this.btnView.Text = "View";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // ImgBtnAccountNo
            // 
            this.ImgBtnAccountNo.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.searchicon;
            this.ImgBtnAccountNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ImgBtnAccountNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImgBtnAccountNo.ForeColor = System.Drawing.Color.White;
            this.ImgBtnAccountNo.Location = new System.Drawing.Point(210, 9);
            this.ImgBtnAccountNo.Name = "ImgBtnAccountNo";
            this.ImgBtnAccountNo.Size = new System.Drawing.Size(20, 19);
            this.ImgBtnAccountNo.TabIndex = 42;
            this.ImgBtnAccountNo.UseVisualStyleBackColor = true;
            this.ImgBtnAccountNo.Click += new System.EventHandler(this.ImgBtnAccountNo_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Image = global::FF.WindowsERPClient.Properties.Resources.InveTrancker;
            this.btn_Clear.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btn_Clear.Location = new System.Drawing.Point(917, 6);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(71, 26);
            this.btn_Clear.TabIndex = 138;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // HPReminders
            // 
            this.ClientSize = new System.Drawing.Size(1000, 483);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.chkView);
            this.Controls.Add(this.lblBackDateInfor);
            this.Controls.Add(this.btnInactive);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.gvView);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucHpAccountSummary1);
            this.Controls.Add(this.ImgBtnAccountNo);
            this.Controls.Add(this.txtAccNo);
            this.Controls.Add(this.lblAccountNo);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HPReminders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Account Reminders";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ImgBtnAccountNo;
        private System.Windows.Forms.TextBox txtAccNo;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.Label label7;
        private UserControls.UcHpAccountSummary ucHpAccountSummary1;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker txtRemindDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnActive;
        private System.Windows.Forms.TextBox txtCustMob;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtManagerMob;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView gvView;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnInactive;
        private System.Windows.Forms.Label lblBackDateInfor;
        private System.Windows.Forms.CheckBox chkView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn hra_seq;
        private System.Windows.Forms.Button btn_Clear;
    }
}
