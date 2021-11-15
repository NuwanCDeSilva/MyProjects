namespace FF.WindowsERPClient.General
{
    partial class WeekDefinition
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFromDate = new System.Windows.Forms.DateTimePicker();
            this.txtToDate = new System.Windows.Forms.DateTimePicker();
            this.gvWeek = new System.Windows.Forms.DataGridView();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.cmbWeek = new System.Windows.Forms.ComboBox();
            this.gvCompany = new System.Windows.Forms.DataGridView();
            this.c_select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.c_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.w_com = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.w_week = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.w_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.w_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.w_year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.w_month = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompany)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(322, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Year";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(322, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Month";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.btnEdit);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(1, -6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(876, 34);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Lavender;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(796, 9);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(71, 21);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Lavender;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(724, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 21);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "To Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Week";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(322, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "From Date";
            // 
            // txtFromDate
            // 
            this.txtFromDate.Checked = false;
            this.txtFromDate.CustomFormat = "dd/MMM/yyyy";
            this.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFromDate.Location = new System.Drawing.Point(322, 167);
            this.txtFromDate.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(105, 21);
            this.txtFromDate.TabIndex = 79;
            // 
            // txtToDate
            // 
            this.txtToDate.Checked = false;
            this.txtToDate.CustomFormat = "dd/MMM/yyyy";
            this.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtToDate.Location = new System.Drawing.Point(322, 209);
            this.txtToDate.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(105, 21);
            this.txtToDate.TabIndex = 80;
            // 
            // gvWeek
            // 
            this.gvWeek.AllowUserToAddRows = false;
            this.gvWeek.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.gvWeek.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvWeek.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvWeek.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvWeek.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvWeek.ColumnHeadersHeight = 20;
            this.gvWeek.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.w_com,
            this.w_week,
            this.w_from,
            this.w_to,
            this.w_year,
            this.w_month});
            this.gvWeek.EnableHeadersVisualStyles = false;
            this.gvWeek.GridColor = System.Drawing.Color.MidnightBlue;
            this.gvWeek.Location = new System.Drawing.Point(471, 46);
            this.gvWeek.Name = "gvWeek";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvWeek.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvWeek.RowHeadersVisible = false;
            this.gvWeek.RowTemplate.Height = 16;
            this.gvWeek.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvWeek.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvWeek.Size = new System.Drawing.Size(404, 182);
            this.gvWeek.TabIndex = 132;
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(322, 46);
            this.cmbYear.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(134, 21);
            this.cmbYear.TabIndex = 133;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(322, 85);
            this.cmbMonth.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(134, 21);
            this.cmbMonth.TabIndex = 134;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // cmbWeek
            // 
            this.cmbWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeek.FormattingEnabled = true;
            this.cmbWeek.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbWeek.Location = new System.Drawing.Point(322, 125);
            this.cmbWeek.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.cmbWeek.Name = "cmbWeek";
            this.cmbWeek.Size = new System.Drawing.Size(134, 21);
            this.cmbWeek.TabIndex = 135;
            this.cmbWeek.SelectedIndexChanged += new System.EventHandler(this.cmbWeek_SelectedIndexChanged);
            // 
            // gvCompany
            // 
            this.gvCompany.AllowUserToAddRows = false;
            this.gvCompany.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.SkyBlue;
            this.gvCompany.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gvCompany.BackgroundColor = System.Drawing.Color.White;
            this.gvCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCompany.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvCompany.ColumnHeadersHeight = 20;
            this.gvCompany.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_select,
            this.c_code,
            this.c_desc});
            this.gvCompany.EnableHeadersVisualStyles = false;
            this.gvCompany.GridColor = System.Drawing.Color.Navy;
            this.gvCompany.Location = new System.Drawing.Point(3, 46);
            this.gvCompany.Name = "gvCompany";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCompany.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gvCompany.RowHeadersVisible = false;
            this.gvCompany.RowTemplate.Height = 16;
            this.gvCompany.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCompany.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCompany.Size = new System.Drawing.Size(306, 182);
            this.gvCompany.TabIndex = 136;
            this.gvCompany.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCompany_CellClick);
            // 
            // c_select
            // 
            this.c_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.c_select.HeaderText = "";
            this.c_select.Name = "c_select";
            this.c_select.ReadOnly = true;
            this.c_select.Width = 20;
            // 
            // c_code
            // 
            this.c_code.DataPropertyName = "mc_cd";
            this.c_code.HeaderText = "Company";
            this.c_code.Name = "c_code";
            this.c_code.ReadOnly = true;
            this.c_code.Width = 60;
            // 
            // c_desc
            // 
            this.c_desc.DataPropertyName = "mc_desc";
            this.c_desc.HeaderText = "Description";
            this.c_desc.Name = "c_desc";
            this.c_desc.ReadOnly = true;
            this.c_desc.Width = 200;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 137;
            this.label5.Text = "Company";
            // 
            // w_com
            // 
            this.w_com.DataPropertyName = "gw_com";
            this.w_com.HeaderText = "Company";
            this.w_com.Name = "w_com";
            this.w_com.Width = 60;
            // 
            // w_week
            // 
            this.w_week.DataPropertyName = "gw_week";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.w_week.DefaultCellStyle = dataGridViewCellStyle3;
            this.w_week.HeaderText = "Week";
            this.w_week.Name = "w_week";
            this.w_week.ReadOnly = true;
            this.w_week.Width = 80;
            // 
            // w_from
            // 
            this.w_from.DataPropertyName = "gw_from_dt";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.w_from.DefaultCellStyle = dataGridViewCellStyle4;
            this.w_from.HeaderText = "From";
            this.w_from.Name = "w_from";
            this.w_from.ReadOnly = true;
            this.w_from.Width = 120;
            // 
            // w_to
            // 
            this.w_to.DataPropertyName = "gw_to_dt";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            this.w_to.DefaultCellStyle = dataGridViewCellStyle5;
            this.w_to.HeaderText = "To";
            this.w_to.Name = "w_to";
            this.w_to.ReadOnly = true;
            this.w_to.Width = 120;
            // 
            // w_year
            // 
            this.w_year.DataPropertyName = "gw_year";
            this.w_year.HeaderText = "Year";
            this.w_year.Name = "w_year";
            this.w_year.Visible = false;
            // 
            // w_month
            // 
            this.w_month.DataPropertyName = "gw_month";
            this.w_month.HeaderText = "Month";
            this.w_month.Name = "w_month";
            this.w_month.Visible = false;
            // 
            // WeekDefinition
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(877, 278);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gvCompany);
            this.Controls.Add(this.cmbWeek);
            this.Controls.Add(this.cmbMonth);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.gvWeek);
            this.Controls.Add(this.txtToDate);
            this.Controls.Add(this.txtFromDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "WeekDefinition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Week Definition";
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompany)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker txtFromDate;
        private System.Windows.Forms.DateTimePicker txtToDate;
        private System.Windows.Forms.DataGridView gvWeek;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox cmbWeek;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridView gvCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn c_select;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn w_com;
        private System.Windows.Forms.DataGridViewTextBoxColumn w_week;
        private System.Windows.Forms.DataGridViewTextBoxColumn w_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn w_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn w_year;
        private System.Windows.Forms.DataGridViewTextBoxColumn w_month;
    }
}
