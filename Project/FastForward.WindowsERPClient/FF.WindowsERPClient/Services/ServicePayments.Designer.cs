namespace FF.WindowsERPClient.Services
{
    partial class ServicePayments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlActualDefects = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.grvPayment = new System.Windows.Forms.DataGridView();
            this.sar_receipt_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sard_pay_tp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sard_ref_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sard_settle_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sard_receipt_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtJobNo = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.grvPending = new System.Windows.Forms.DataGridView();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.sar_receipt_date1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sar_receipt_no1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sar_tot_settle_amt1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sar_used_amt1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sard_remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlActualDefects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPayment)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPending)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlActualDefects
            // 
            this.pnlActualDefects.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlActualDefects.Controls.Add(this.label1);
            this.pnlActualDefects.Controls.Add(this.label15);
            this.pnlActualDefects.Controls.Add(this.grvPayment);
            this.pnlActualDefects.Controls.Add(this.panel2);
            this.pnlActualDefects.Controls.Add(this.grvPending);
            this.pnlActualDefects.Location = new System.Drawing.Point(1, 2);
            this.pnlActualDefects.Name = "pnlActualDefects";
            this.pnlActualDefects.Size = new System.Drawing.Size(554, 404);
            this.pnlActualDefects.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MidnightBlue;
            this.label1.ForeColor = System.Drawing.Color.Azure;
            this.label1.Location = new System.Drawing.Point(4, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(549, 14);
            this.label1.TabIndex = 315;
            this.label1.Text = "Payment Details";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.MidnightBlue;
            this.label15.ForeColor = System.Drawing.Color.Azure;
            this.label15.Location = new System.Drawing.Point(3, 225);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(550, 14);
            this.label15.TabIndex = 314;
            this.label15.Text = "Settlements";
            // 
            // grvPayment
            // 
            this.grvPayment.AllowUserToAddRows = false;
            this.grvPayment.AllowUserToDeleteRows = false;
            this.grvPayment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvPayment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grvPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvPayment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sar_receipt_date,
            this.sard_pay_tp,
            this.sard_ref_no,
            this.sard_settle_amt,
            this.sard_receipt_no});
            this.grvPayment.EnableHeadersVisualStyles = false;
            this.grvPayment.Location = new System.Drawing.Point(3, 54);
            this.grvPayment.MultiSelect = false;
            this.grvPayment.Name = "grvPayment";
            this.grvPayment.RowHeadersVisible = false;
            this.grvPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvPayment.Size = new System.Drawing.Size(550, 166);
            this.grvPayment.TabIndex = 313;
            // 
            // sar_receipt_date
            // 
            this.sar_receipt_date.DataPropertyName = "sar_receipt_date";
            this.sar_receipt_date.HeaderText = "Date";
            this.sar_receipt_date.Name = "sar_receipt_date";
            this.sar_receipt_date.ReadOnly = true;
            this.sar_receipt_date.Width = 80;
            // 
            // sard_pay_tp
            // 
            this.sard_pay_tp.DataPropertyName = "sard_pay_tp";
            this.sard_pay_tp.HeaderText = "Pay Mode";
            this.sard_pay_tp.Name = "sard_pay_tp";
            this.sard_pay_tp.ReadOnly = true;
            this.sard_pay_tp.Width = 90;
            // 
            // sard_ref_no
            // 
            this.sard_ref_no.DataPropertyName = "sard_ref_no";
            this.sard_ref_no.HeaderText = "Ref no";
            this.sard_ref_no.Name = "sard_ref_no";
            this.sard_ref_no.ReadOnly = true;
            this.sard_ref_no.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sard_ref_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sard_ref_no.Width = 120;
            // 
            // sard_settle_amt
            // 
            this.sard_settle_amt.DataPropertyName = "sard_settle_amt";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.sard_settle_amt.DefaultCellStyle = dataGridViewCellStyle2;
            this.sard_settle_amt.HeaderText = "Amount";
            this.sard_settle_amt.Name = "sard_settle_amt";
            this.sard_settle_amt.ReadOnly = true;
            this.sard_settle_amt.Width = 120;
            // 
            // sard_receipt_no
            // 
            this.sard_receipt_no.DataPropertyName = "sard_receipt_no";
            this.sard_receipt_no.HeaderText = "Receipt No";
            this.sard_receipt_no.Name = "sard_receipt_no";
            this.sard_receipt_no.ReadOnly = true;
            this.sard_receipt_no.Width = 120;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(201)))), ((int)(((byte)(167)))));
            this.panel2.Controls.Add(this.txtJobNo);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(549, 31);
            this.panel2.TabIndex = 312;
            // 
            // txtJobNo
            // 
            this.txtJobNo.BackColor = System.Drawing.Color.White;
            this.txtJobNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJobNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJobNo.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobNo.Location = new System.Drawing.Point(77, 6);
            this.txtJobNo.MaxLength = 5;
            this.txtJobNo.Name = "txtJobNo";
            this.txtJobNo.ReadOnly = true;
            this.txtJobNo.Size = new System.Drawing.Size(182, 19);
            this.txtJobNo.TabIndex = 313;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(7, 9);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(64, 13);
            this.label42.TabIndex = 312;
            this.label42.Text = "Job Number";
            // 
            // grvPending
            // 
            this.grvPending.AllowUserToAddRows = false;
            this.grvPending.AllowUserToDeleteRows = false;
            this.grvPending.AllowUserToOrderColumns = true;
            this.grvPending.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvPending.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grvPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvPending.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sar_receipt_date1,
            this.sar_receipt_no1,
            this.sar_tot_settle_amt1,
            this.sar_used_amt1,
            this.Column1,
            this.sard_remarks});
            this.grvPending.EnableHeadersVisualStyles = false;
            this.grvPending.Location = new System.Drawing.Point(3, 238);
            this.grvPending.MultiSelect = false;
            this.grvPending.Name = "grvPending";
            this.grvPending.RowHeadersVisible = false;
            this.grvPending.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvPending.Size = new System.Drawing.Size(550, 165);
            this.grvPending.TabIndex = 5;
            this.grvPending.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvActualDefects_CellEndEdit);
            this.grvPending.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvActualDefects_EditingControlShowing);
            // 
            // btnCloseFrom
            // 
            this.btnCloseFrom.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseFrom.Location = new System.Drawing.Point(374, 224);
            this.btnCloseFrom.Name = "btnCloseFrom";
            this.btnCloseFrom.Size = new System.Drawing.Size(10, 10);
            this.btnCloseFrom.TabIndex = 275;
            this.btnCloseFrom.Text = "  ";
            this.btnCloseFrom.UseVisualStyleBackColor = true;
            this.btnCloseFrom.Click += new System.EventHandler(this.btnCloseFrom_Click);
            // 
            // sar_receipt_date1
            // 
            this.sar_receipt_date1.DataPropertyName = "sar_receipt_date";
            this.sar_receipt_date1.HeaderText = "Date";
            this.sar_receipt_date1.Name = "sar_receipt_date1";
            this.sar_receipt_date1.ReadOnly = true;
            this.sar_receipt_date1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sar_receipt_date1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sar_receipt_date1.Width = 80;
            // 
            // sar_receipt_no1
            // 
            this.sar_receipt_no1.DataPropertyName = "sar_receipt_no";
            this.sar_receipt_no1.HeaderText = "Receipt #";
            this.sar_receipt_no1.Name = "sar_receipt_no1";
            this.sar_receipt_no1.ReadOnly = true;
            // 
            // sar_tot_settle_amt1
            // 
            this.sar_tot_settle_amt1.DataPropertyName = "sar_tot_settle_amt";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.sar_tot_settle_amt1.DefaultCellStyle = dataGridViewCellStyle4;
            this.sar_tot_settle_amt1.HeaderText = "Receipt Amount";
            this.sar_tot_settle_amt1.Name = "sar_tot_settle_amt1";
            this.sar_tot_settle_amt1.ReadOnly = true;
            this.sar_tot_settle_amt1.Width = 120;
            // 
            // sar_used_amt1
            // 
            this.sar_used_amt1.DataPropertyName = "sar_used_amt";
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.sar_used_amt1.DefaultCellStyle = dataGridViewCellStyle5;
            this.sar_used_amt1.HeaderText = "Settle Amount";
            this.sar_used_amt1.Name = "sar_used_amt1";
            this.sar_used_amt1.ReadOnly = true;
            this.sar_used_amt1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sar_used_amt1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "settled";
            this.Column1.HeaderText = "Settled";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // sard_remarks
            // 
            this.sard_remarks.DataPropertyName = "sard_remarks";
            this.sard_remarks.HeaderText = "Remarks";
            this.sard_remarks.Name = "sard_remarks";
            this.sard_remarks.Width = 60;
            // 
            // ServicePayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(559, 406);
            this.Controls.Add(this.pnlActualDefects);
            this.Controls.Add(this.btnCloseFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServicePayments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Service Payments";
            this.Load += new System.EventHandler(this.ServicePayments_Load);
            this.pnlActualDefects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvPayment)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPending)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlActualDefects;
        private System.Windows.Forms.DataGridView grvPending;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grvPayment;
        private System.Windows.Forms.TextBox txtJobNo;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_receipt_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn sard_pay_tp;
        private System.Windows.Forms.DataGridViewTextBoxColumn sard_ref_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn sard_settle_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn sard_receipt_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_receipt_date1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_receipt_no1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_tot_settle_amt1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_used_amt1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sard_remarks;
    }
}