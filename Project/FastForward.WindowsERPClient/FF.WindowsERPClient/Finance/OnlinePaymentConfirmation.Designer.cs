namespace FF.WindowsERPClient.Finance
{
    partial class OnlinePaymentConfirmation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbDocTp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrmDt = new System.Windows.Forms.DateTimePicker();
            this.dtpToDt = new System.Windows.Forms.DateTimePicker();
            this.cmbTp = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApprove = new System.Windows.Forms.ToolStripButton();
            this.lblBackDateInfor = new System.Windows.Forms.ToolStripLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gvItem = new System.Windows.Forms.DataGridView();
            this.c_select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.c_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_webref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_accno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_pc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_com = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_paymode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_statusdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_trno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_paytp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_bkcharge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDocTp
            // 
            this.cmbDocTp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocTp.FormattingEnabled = true;
            this.cmbDocTp.Items.AddRange(new object[] {
            "WEBHP"});
            this.cmbDocTp.Location = new System.Drawing.Point(261, 34);
            this.cmbDocTp.Name = "cmbDocTp";
            this.cmbDocTp.Size = new System.Drawing.Size(121, 21);
            this.cmbDocTp.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(177, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Document Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(414, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Date from";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(574, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(719, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Type";
            // 
            // dtpFrmDt
            // 
            this.dtpFrmDt.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpFrmDt.Checked = false;
            this.dtpFrmDt.CustomFormat = "dd/MMM/yyyy";
            this.dtpFrmDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrmDt.Location = new System.Drawing.Point(472, 34);
            this.dtpFrmDt.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.dtpFrmDt.Name = "dtpFrmDt";
            this.dtpFrmDt.Size = new System.Drawing.Size(101, 21);
            this.dtpFrmDt.TabIndex = 51;
            // 
            // dtpToDt
            // 
            this.dtpToDt.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpToDt.Checked = false;
            this.dtpToDt.CustomFormat = "dd/MMM/yyyy";
            this.dtpToDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDt.Location = new System.Drawing.Point(592, 34);
            this.dtpToDt.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.dtpToDt.Name = "dtpToDt";
            this.dtpToDt.Size = new System.Drawing.Size(101, 21);
            this.dtpToDt.TabIndex = 52;
            // 
            // cmbTp
            // 
            this.cmbTp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTp.FormattingEnabled = true;
            this.cmbTp.Items.AddRange(new object[] {
            "PENDING",
            "APPROVED",
            "REJECTED"});
            this.cmbTp.Location = new System.Drawing.Point(751, 34);
            this.cmbTp.Name = "cmbTp";
            this.cmbTp.Size = new System.Drawing.Size(121, 21);
            this.cmbTp.TabIndex = 53;
            this.cmbTp.SelectedIndexChanged += new System.EventHandler(this.cmbTp_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.toolStripSeparator2,
            this.btnReject,
            this.toolStripSeparator5,
            this.btnApprove,
            this.lblBackDateInfor});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(915, 28);
            this.toolStrip1.TabIndex = 54;
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = false;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(2);
            this.btnClear.Size = new System.Drawing.Size(60, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnReject
            // 
            this.btnReject.AutoSize = false;
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btnReject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnReject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReject.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnReject.Name = "btnReject";
            this.btnReject.Padding = new System.Windows.Forms.Padding(2);
            this.btnReject.Size = new System.Drawing.Size(60, 22);
            this.btnReject.Text = "Reject";
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 23);
            // 
            // btnApprove
            // 
            this.btnApprove.AutoSize = false;
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btnApprove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprove.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Padding = new System.Windows.Forms.Padding(2);
            this.btnApprove.Size = new System.Drawing.Size(60, 22);
            this.btnApprove.Text = "Approve";
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // lblBackDateInfor
            // 
            this.lblBackDateInfor.AutoSize = false;
            this.lblBackDateInfor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblBackDateInfor.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackDateInfor.ForeColor = System.Drawing.Color.Blue;
            this.lblBackDateInfor.Name = "lblBackDateInfor";
            this.lblBackDateInfor.Size = new System.Drawing.Size(298, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources._1357119662_12576;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearch.Location = new System.Drawing.Point(874, 29);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 34);
            this.btnSearch.TabIndex = 55;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gvItem
            // 
            this.gvItem.AllowUserToAddRows = false;
            this.gvItem.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvItem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvItem.BackgroundColor = System.Drawing.Color.White;
            this.gvItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvItem.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvItem.ColumnHeadersHeight = 20;
            this.gvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_select,
            this.c_date,
            this.c_webref,
            this.c_accno,
            this.c_pc,
            this.c_com,
            this.c_paymode,
            this.c_amount,
            this.c_status,
            this.c_statusdesc,
            this.c_trno,
            this.c_paytp,
            this.c_bkcharge});
            this.gvItem.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvItem.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvItem.EnableHeadersVisualStyles = false;
            this.gvItem.GridColor = System.Drawing.Color.White;
            this.gvItem.Location = new System.Drawing.Point(7, 64);
            this.gvItem.Name = "gvItem";
            this.gvItem.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvItem.RowHeadersVisible = false;
            this.gvItem.RowTemplate.Height = 20;
            this.gvItem.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvItem.Size = new System.Drawing.Size(902, 429);
            this.gvItem.TabIndex = 56;
            // 
            // c_select
            // 
            this.c_select.HeaderText = "";
            this.c_select.Name = "c_select";
            this.c_select.Width = 20;
            // 
            // c_date
            // 
            this.c_date.DataPropertyName = "hwr_rct_dt";
            this.c_date.HeaderText = "Date";
            this.c_date.Name = "c_date";
            this.c_date.ReadOnly = true;
            this.c_date.Width = 75;
            // 
            // c_webref
            // 
            this.c_webref.DataPropertyName = "hwr_web_ref";
            this.c_webref.HeaderText = "Web Ref. No";
            this.c_webref.Name = "c_webref";
            this.c_webref.ReadOnly = true;
            this.c_webref.Width = 120;
            // 
            // c_accno
            // 
            this.c_accno.DataPropertyName = "hwr_acc_no";
            this.c_accno.HeaderText = "Account";
            this.c_accno.Name = "c_accno";
            this.c_accno.ReadOnly = true;
            this.c_accno.Width = 120;
            // 
            // c_pc
            // 
            this.c_pc.DataPropertyName = "hwr_pc";
            this.c_pc.HeaderText = "Profit Center";
            this.c_pc.Name = "c_pc";
            this.c_pc.ReadOnly = true;
            this.c_pc.Width = 75;
            // 
            // c_com
            // 
            this.c_com.DataPropertyName = "hwr_com";
            this.c_com.HeaderText = "Company";
            this.c_com.Name = "c_com";
            this.c_com.ReadOnly = true;
            this.c_com.Visible = false;
            this.c_com.Width = 75;
            // 
            // c_paymode
            // 
            this.c_paymode.DataPropertyName = "hwr_pay_mode";
            this.c_paymode.HeaderText = "Pay Mode";
            this.c_paymode.Name = "c_paymode";
            this.c_paymode.ReadOnly = true;
            this.c_paymode.Width = 110;
            // 
            // c_amount
            // 
            this.c_amount.DataPropertyName = "hwr_col_amt";
            this.c_amount.HeaderText = "Amount";
            this.c_amount.Name = "c_amount";
            this.c_amount.ReadOnly = true;
            // 
            // c_status
            // 
            this.c_status.DataPropertyName = "hwr_app_stus";
            this.c_status.HeaderText = "Status";
            this.c_status.Name = "c_status";
            this.c_status.ReadOnly = true;
            this.c_status.Visible = false;
            this.c_status.Width = 120;
            // 
            // c_statusdesc
            // 
            this.c_statusdesc.DataPropertyName = "statusdesc";
            this.c_statusdesc.HeaderText = "Status";
            this.c_statusdesc.Name = "c_statusdesc";
            this.c_statusdesc.ReadOnly = true;
            this.c_statusdesc.Width = 120;
            // 
            // c_trno
            // 
            this.c_trno.DataPropertyName = "hwr_rct_ref";
            this.c_trno.HeaderText = "Transaction No";
            this.c_trno.Name = "c_trno";
            this.c_trno.ReadOnly = true;
            this.c_trno.Width = 120;
            // 
            // c_paytp
            // 
            this.c_paytp.DataPropertyName = "HWR_PAY_MODE";
            this.c_paytp.HeaderText = "PayTp";
            this.c_paytp.Name = "c_paytp";
            this.c_paytp.ReadOnly = true;
            this.c_paytp.Visible = false;
            // 
            // c_bkcharge
            // 
            this.c_bkcharge.DataPropertyName = "HWR_BANK_CHG";
            this.c_bkcharge.HeaderText = "bkcharge";
            this.c_bkcharge.Name = "c_bkcharge";
            this.c_bkcharge.ReadOnly = true;
            this.c_bkcharge.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDate.Checked = false;
            this.dtpDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(41, 34);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(101, 21);
            this.dtpDate.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Date";
            // 
            // OnlinePaymentConfirmation
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(915, 499);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.gvItem);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cmbTp);
            this.Controls.Add(this.dtpToDt);
            this.Controls.Add(this.dtpFrmDt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDocTp);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OnlinePaymentConfirmation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Online Payment Confirmation";
            this.Load += new System.EventHandler(this.OnlinePaymentConfirmation_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDocTp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFrmDt;
        private System.Windows.Forms.DateTimePicker dtpToDt;
        private System.Windows.Forms.ComboBox cmbTp;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnReject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnApprove;
        private System.Windows.Forms.ToolStripLabel lblBackDateInfor;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView gvItem;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn c_select;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_webref;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_accno;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_pc;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_com;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_paymode;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_statusdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_trno;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_paytp;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_bkcharge;
    }
}
