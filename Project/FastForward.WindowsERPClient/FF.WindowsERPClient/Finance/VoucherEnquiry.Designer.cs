namespace FF.WindowsERPClient.Finance
{
    partial class VoucherEnquiry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoucherEnquiry));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grvDetails = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnVouNo = new System.Windows.Forms.Button();
            this.txtRef = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.rdoNonPrinted = new System.Windows.Forms.RadioButton();
            this.rdoPrinted = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.rdoCancel = new System.Windows.Forms.RadioButton();
            this.rdoActive = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.grvHeader = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetails)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnl2.SuspendLayout();
            this.pnl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.MidnightBlue;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(5, 123);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(780, 15);
            this.label4.TabIndex = 96;
            this.label4.Text = "Vouchers";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MidnightBlue;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(86, 300);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(520, 15);
            this.label1.TabIndex = 97;
            this.label1.Text = "Voucher Details";
            // 
            // grvDetails
            // 
            this.grvDetails.AllowUserToAddRows = false;
            this.grvDetails.AllowUserToDeleteRows = false;
            this.grvDetails.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.grvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column10,
            this.Column9});
            this.grvDetails.Location = new System.Drawing.Point(85, 315);
            this.grvDetails.Name = "grvDetails";
            this.grvDetails.ReadOnly = true;
            this.grvDetails.RowHeadersVisible = false;
            this.grvDetails.RowTemplate.Height = 18;
            this.grvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvDetails.Size = new System.Drawing.Size(521, 142);
            this.grvDetails.TabIndex = 148;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Givd_expe_cd";
            this.Column5.HeaderText = "Expense Code";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "givd_expe_desc";
            this.Column10.HeaderText = "Description";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 250;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "givd_expe_val";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column9.HeaderText = "Amount";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnVouNo);
            this.groupBox1.Controls.Add(this.txtRef);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chkPrint);
            this.groupBox1.Controls.Add(this.chkStatus);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.dateTimePickerTo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dateTimePickerFrom);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pnl2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pnl1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(773, 113);
            this.groupBox1.TabIndex = 149;
            this.groupBox1.TabStop = false;
            // 
            // btnVouNo
            // 
            this.btnVouNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVouNo.BackgroundImage")));
            this.btnVouNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVouNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVouNo.ForeColor = System.Drawing.Color.White;
            this.btnVouNo.Location = new System.Drawing.Point(175, 68);
            this.btnVouNo.Name = "btnVouNo";
            this.btnVouNo.Size = new System.Drawing.Size(17, 20);
            this.btnVouNo.TabIndex = 118;
            this.btnVouNo.UseVisualStyleBackColor = true;
            this.btnVouNo.Click += new System.EventHandler(this.btnVouNo_Click);
            // 
            // txtRef
            // 
            this.txtRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRef.Location = new System.Drawing.Point(77, 68);
            this.txtRef.MaxLength = 10;
            this.txtRef.Name = "txtRef";
            this.txtRef.Size = new System.Drawing.Size(94, 21);
            this.txtRef.TabIndex = 117;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 116;
            this.label7.Text = "Voucher No";
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Checked = true;
            this.chkPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrint.Location = new System.Drawing.Point(412, 18);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(15, 14);
            this.chkPrint.TabIndex = 115;
            this.chkPrint.UseVisualStyleBackColor = true;
            this.chkPrint.CheckedChanged += new System.EventHandler(this.chkPrint_CheckedChanged);
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Checked = true;
            this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStatus.Location = new System.Drawing.Point(14, 19);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(15, 14);
            this.chkStatus.TabIndex = 114;
            this.chkStatus.UseVisualStyleBackColor = true;
            this.chkStatus.CheckedChanged += new System.EventHandler(this.chkStatus_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(649, 73);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 113;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(568, 73);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 112;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTo.Location = new System.Drawing.Point(301, 41);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(105, 21);
            this.dateTimePickerTo.TabIndex = 111;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(241, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 110;
            this.label6.Text = "To";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(77, 41);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(105, 21);
            this.dateTimePickerFrom.TabIndex = 109;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "From";
            // 
            // pnl2
            // 
            this.pnl2.Controls.Add(this.rdoNonPrinted);
            this.pnl2.Controls.Add(this.rdoPrinted);
            this.pnl2.Location = new System.Drawing.Point(512, 14);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(218, 23);
            this.pnl2.TabIndex = 2;
            // 
            // rdoNonPrinted
            // 
            this.rdoNonPrinted.AutoSize = true;
            this.rdoNonPrinted.Location = new System.Drawing.Point(109, 3);
            this.rdoNonPrinted.Name = "rdoNonPrinted";
            this.rdoNonPrinted.Size = new System.Drawing.Size(69, 17);
            this.rdoNonPrinted.TabIndex = 1;
            this.rdoNonPrinted.Text = "Non Print";
            this.rdoNonPrinted.UseVisualStyleBackColor = true;
            // 
            // rdoPrinted
            // 
            this.rdoPrinted.AutoSize = true;
            this.rdoPrinted.Checked = true;
            this.rdoPrinted.Location = new System.Drawing.Point(4, 2);
            this.rdoPrinted.Name = "rdoPrinted";
            this.rdoPrinted.Size = new System.Drawing.Size(47, 17);
            this.rdoPrinted.TabIndex = 0;
            this.rdoPrinted.TabStop = true;
            this.rdoPrinted.Text = "Print";
            this.rdoPrinted.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(428, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Print Status";
            // 
            // pnl1
            // 
            this.pnl1.Controls.Add(this.rdoCancel);
            this.pnl1.Controls.Add(this.rdoActive);
            this.pnl1.Location = new System.Drawing.Point(77, 12);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(218, 23);
            this.pnl1.TabIndex = 1;
            // 
            // rdoCancel
            // 
            this.rdoCancel.AutoSize = true;
            this.rdoCancel.Location = new System.Drawing.Point(109, 3);
            this.rdoCancel.Name = "rdoCancel";
            this.rdoCancel.Size = new System.Drawing.Size(57, 17);
            this.rdoCancel.TabIndex = 1;
            this.rdoCancel.Text = "Cancel";
            this.rdoCancel.UseVisualStyleBackColor = true;
            // 
            // rdoActive
            // 
            this.rdoActive.AutoSize = true;
            this.rdoActive.Checked = true;
            this.rdoActive.Location = new System.Drawing.Point(4, 2);
            this.rdoActive.Name = "rdoActive";
            this.rdoActive.Size = new System.Drawing.Size(55, 17);
            this.rdoActive.TabIndex = 0;
            this.rdoActive.TabStop = true;
            this.rdoActive.Text = "Active";
            this.rdoActive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Status";
            // 
            // grvHeader
            // 
            this.grvHeader.AllowUserToAddRows = false;
            this.grvHeader.AllowUserToDeleteRows = false;
            this.grvHeader.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.grvHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvHeader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column6,
            this.Column7,
            this.Column14});
            this.grvHeader.Location = new System.Drawing.Point(4, 138);
            this.grvHeader.Name = "grvHeader";
            this.grvHeader.RowHeadersVisible = false;
            this.grvHeader.RowTemplate.Height = 18;
            this.grvHeader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvHeader.Size = new System.Drawing.Size(781, 152);
            this.grvHeader.TabIndex = 150;
            this.grvHeader.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvHeader_CellContentClick);
            // 
            // Column8
            // 
            this.Column8.HeaderText = "";
            this.Column8.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.Column8.Name = "Column8";
            this.Column8.Width = 25;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "GIVH_VOU_NO";
            this.Column1.HeaderText = "Voucher No";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "GIVH_VALID_FROM";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "From";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 70;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "GIVH_VALID_TO";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.HeaderText = "To";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 70;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "GIVH_VAL";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "Value";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 70;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "GIVH_CLAIM_STUS";
            this.Column11.HeaderText = "Claim Status";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 90;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "GIVH_CLAIM_COM";
            this.Column12.HeaderText = "Claim Com";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 85;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "GIVH_CLAIM_PC";
            this.Column13.HeaderText = "Claim PC";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 85;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "GIVD_EMP_NAME";
            this.Column6.HeaderText = "Employee";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 120;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "GIVH_DT";
            this.Column7.HeaderText = "Date";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 70;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "GIVH_PRINT_STUS";
            this.Column14.HeaderText = "Print";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 70;
            // 
            // VoucherEnquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(791, 476);
            this.Controls.Add(this.grvHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grvDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "VoucherEnquiry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Voucher Enquiry";
            this.Load += new System.EventHandler(this.VoucherEnquiry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvDetails)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnl2.ResumeLayout(false);
            this.pnl2.PerformLayout();
            this.pnl1.ResumeLayout(false);
            this.pnl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grvDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.RadioButton rdoNonPrinted;
        private System.Windows.Forms.RadioButton rdoPrinted;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.RadioButton rdoCancel;
        private System.Windows.Forms.RadioButton rdoActive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView grvHeader;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnVouNo;
        private System.Windows.Forms.TextBox txtRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewImageColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
    }
}