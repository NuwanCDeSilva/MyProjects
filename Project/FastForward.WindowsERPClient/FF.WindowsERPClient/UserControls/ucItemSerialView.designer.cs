namespace FF.WindowsERPClient.UserControls
{
    partial class ucItemSerialView
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

        #region Component Designer generated code

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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.GridViewSerials = new System.Windows.Forms.DataGridView();
            this._ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Serial_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Serial_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Serial_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._ItemStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._DocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Warranty_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Warranty_Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._avail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._available = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GridViewDocuments = new System.Windows.Forms.DataGridView();
            this.Batch_Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Batch_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Doc_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit_Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grvTempIssue = new System.Windows.Forms.DataGridView();
            this.STI_ISSUESERIALNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_ISSUEITMSTUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_DT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_SUB_LOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_RMK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewSerials)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDocuments)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvTempIssue)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(875, 183);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GridViewSerials);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(867, 157);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Serials";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GridViewSerials
            // 
            this.GridViewSerials.AllowUserToAddRows = false;
            this.GridViewSerials.AllowUserToDeleteRows = false;
            this.GridViewSerials.AllowUserToResizeRows = false;
            this.GridViewSerials.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewSerials.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridViewSerials.ColumnHeadersHeight = 20;
            this.GridViewSerials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridViewSerials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._ItemCode,
            this._Serial,
            this._Serial_1,
            this._Serial_2,
            this._Serial_3,
            this._ItemStatus,
            this._DocNo,
            this._Date,
            this._Warranty_No,
            this._Warranty_Period,
            this._avail,
            this._cost,
            this._available});
            this.GridViewSerials.EnableHeadersVisualStyles = false;
            this.GridViewSerials.Location = new System.Drawing.Point(3, 6);
            this.GridViewSerials.Name = "GridViewSerials";
            this.GridViewSerials.ReadOnly = true;
            this.GridViewSerials.RowHeadersVisible = false;
            this.GridViewSerials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewSerials.Size = new System.Drawing.Size(858, 145);
            this.GridViewSerials.TabIndex = 0;
            // 
            // _ItemCode
            // 
            this._ItemCode.DataPropertyName = "INS_ITM_CD";
            this._ItemCode.HeaderText = "Item Code";
            this._ItemCode.Name = "_ItemCode";
            this._ItemCode.ReadOnly = true;
            this._ItemCode.Width = 120;
            // 
            // _Serial
            // 
            this._Serial.DataPropertyName = "INS_SER_1";
            this._Serial.HeaderText = "Serial";
            this._Serial.Name = "_Serial";
            this._Serial.ReadOnly = true;
            this._Serial.Visible = false;
            this._Serial.Width = 120;
            // 
            // _Serial_1
            // 
            this._Serial_1.DataPropertyName = "INS_SER_1";
            this._Serial_1.HeaderText = "Serial 1";
            this._Serial_1.Name = "_Serial_1";
            this._Serial_1.ReadOnly = true;
            this._Serial_1.Width = 150;
            // 
            // _Serial_2
            // 
            this._Serial_2.DataPropertyName = "INS_SER_2";
            this._Serial_2.HeaderText = "Serial 2";
            this._Serial_2.Name = "_Serial_2";
            this._Serial_2.ReadOnly = true;
            this._Serial_2.Width = 80;
            // 
            // _Serial_3
            // 
            this._Serial_3.DataPropertyName = "INS_SER_3";
            this._Serial_3.HeaderText = "Serial 3";
            this._Serial_3.Name = "_Serial_3";
            this._Serial_3.ReadOnly = true;
            this._Serial_3.Width = 80;
            // 
            // _ItemStatus
            // 
            this._ItemStatus.DataPropertyName = "INS_ITM_STUS";
            this._ItemStatus.HeaderText = "Item Status";
            this._ItemStatus.Name = "_ItemStatus";
            this._ItemStatus.ReadOnly = true;
            this._ItemStatus.Width = 90;
            // 
            // _DocNo
            // 
            this._DocNo.DataPropertyName = "INS_DOC_NO";
            this._DocNo.HeaderText = "Doc No";
            this._DocNo.Name = "_DocNo";
            this._DocNo.ReadOnly = true;
            this._DocNo.Width = 130;
            // 
            // _Date
            // 
            this._Date.DataPropertyName = "INS_DOC_DT";
            this._Date.HeaderText = "Date";
            this._Date.Name = "_Date";
            this._Date.ReadOnly = true;
            // 
            // _Warranty_No
            // 
            this._Warranty_No.DataPropertyName = "INS_WARR_NO";
            this._Warranty_No.HeaderText = "Warranty No";
            this._Warranty_No.Name = "_Warranty_No";
            this._Warranty_No.ReadOnly = true;
            // 
            // _Warranty_Period
            // 
            this._Warranty_Period.DataPropertyName = "INS_WARR_PERIOD";
            this._Warranty_Period.HeaderText = "Warranty Period";
            this._Warranty_Period.Name = "_Warranty_Period";
            this._Warranty_Period.ReadOnly = true;
            this._Warranty_Period.Visible = false;
            // 
            // _avail
            // 
            this._avail.HeaderText = "Reserved";
            this._avail.Name = "_avail";
            this._avail.ReadOnly = true;
            this._avail.Width = 50;
            // 
            // _cost
            // 
            this._cost.DataPropertyName = "ins_unit_cost";
            this._cost.HeaderText = "Unit Cost";
            this._cost.Name = "_cost";
            this._cost.ReadOnly = true;
            // 
            // _available
            // 
            this._available.DataPropertyName = "INS_AVAILABLE";
            this._available.HeaderText = "Column1";
            this._available.Name = "_available";
            this._available.ReadOnly = true;
            this._available.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GridViewDocuments);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(867, 157);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Documents";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GridViewDocuments
            // 
            this.GridViewDocuments.AllowUserToAddRows = false;
            this.GridViewDocuments.AllowUserToResizeRows = false;
            this.GridViewDocuments.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewDocuments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridViewDocuments.ColumnHeadersHeight = 20;
            this.GridViewDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridViewDocuments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Batch_Line,
            this.Batch_No,
            this.Doc_No,
            this.Date,
            this.Qty,
            this.Unit_Cost});
            this.GridViewDocuments.EnableHeadersVisualStyles = false;
            this.GridViewDocuments.Location = new System.Drawing.Point(3, 6);
            this.GridViewDocuments.Name = "GridViewDocuments";
            this.GridViewDocuments.ReadOnly = true;
            this.GridViewDocuments.RowHeadersVisible = false;
            this.GridViewDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewDocuments.Size = new System.Drawing.Size(823, 145);
            this.GridViewDocuments.TabIndex = 0;
            // 
            // Batch_Line
            // 
            this.Batch_Line.DataPropertyName = "INB_BATCH_LINE";
            this.Batch_Line.HeaderText = "Batch Line";
            this.Batch_Line.Name = "Batch_Line";
            this.Batch_Line.ReadOnly = true;
            this.Batch_Line.Visible = false;
            // 
            // Batch_No
            // 
            this.Batch_No.DataPropertyName = "INB_BATCH_NO";
            this.Batch_No.HeaderText = "Batch No";
            this.Batch_No.Name = "Batch_No";
            this.Batch_No.ReadOnly = true;
            this.Batch_No.Visible = false;
            // 
            // Doc_No
            // 
            this.Doc_No.DataPropertyName = "INB_DOC_NO";
            this.Doc_No.HeaderText = "Doc No";
            this.Doc_No.Name = "Doc_No";
            this.Doc_No.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "INB_DOC_DT";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "INB_QTY";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // Unit_Cost
            // 
            this.Unit_Cost.DataPropertyName = "INB_UNIT_COST";
            this.Unit_Cost.HeaderText = "Unit Cost";
            this.Unit_Cost.Name = "Unit_Cost";
            this.Unit_Cost.ReadOnly = true;
            this.Unit_Cost.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grvTempIssue);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(867, 157);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Temporary Issue";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // grvTempIssue
            // 
            this.grvTempIssue.AllowUserToAddRows = false;
            this.grvTempIssue.AllowUserToResizeRows = false;
            this.grvTempIssue.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvTempIssue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grvTempIssue.ColumnHeadersHeight = 20;
            this.grvTempIssue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grvTempIssue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STI_ISSUESERIALNO,
            this.STI_ISSUEITMSTUS,
            this.STI_DT,
            this.STI_SUB_LOC,
            this.STI_RMK});
            this.grvTempIssue.EnableHeadersVisualStyles = false;
            this.grvTempIssue.Location = new System.Drawing.Point(3, 6);
            this.grvTempIssue.Name = "grvTempIssue";
            this.grvTempIssue.ReadOnly = true;
            this.grvTempIssue.RowHeadersVisible = false;
            this.grvTempIssue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvTempIssue.Size = new System.Drawing.Size(823, 145);
            this.grvTempIssue.TabIndex = 1;
            // 
            // STI_ISSUESERIALNO
            // 
            this.STI_ISSUESERIALNO.DataPropertyName = "STI_ISSUESERIALNO";
            this.STI_ISSUESERIALNO.HeaderText = "Serial No";
            this.STI_ISSUESERIALNO.Name = "STI_ISSUESERIALNO";
            this.STI_ISSUESERIALNO.ReadOnly = true;
            this.STI_ISSUESERIALNO.Width = 225;
            // 
            // STI_ISSUEITMSTUS
            // 
            this.STI_ISSUEITMSTUS.DataPropertyName = "STI_ISSUEITMSTUS";
            this.STI_ISSUEITMSTUS.HeaderText = "Status";
            this.STI_ISSUEITMSTUS.Name = "STI_ISSUEITMSTUS";
            this.STI_ISSUEITMSTUS.ReadOnly = true;
            // 
            // STI_DT
            // 
            this.STI_DT.DataPropertyName = "STI_DT";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.STI_DT.DefaultCellStyle = dataGridViewCellStyle4;
            this.STI_DT.HeaderText = "Issue Date";
            this.STI_DT.Name = "STI_DT";
            this.STI_DT.ReadOnly = true;
            // 
            // STI_SUB_LOC
            // 
            this.STI_SUB_LOC.DataPropertyName = "STI_SUB_LOC";
            this.STI_SUB_LOC.HeaderText = "Sub Location";
            this.STI_SUB_LOC.Name = "STI_SUB_LOC";
            this.STI_SUB_LOC.ReadOnly = true;
            // 
            // STI_RMK
            // 
            this.STI_RMK.DataPropertyName = "STI_RMK";
            this.STI_RMK.HeaderText = "Remark";
            this.STI_RMK.Name = "STI_RMK";
            this.STI_RMK.ReadOnly = true;
            this.STI_RMK.Width = 225;
            // 
            // ucItemSerialView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucItemSerialView";
            this.Size = new System.Drawing.Size(881, 194);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewSerials)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDocuments)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvTempIssue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView GridViewDocuments;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView GridViewSerials;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Serial;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Serial_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Serial_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Serial_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ItemStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn _DocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Warranty_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Warranty_Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn _avail;
        private System.Windows.Forms.DataGridViewTextBoxColumn _cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn _available;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch_Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Doc_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit_Cost;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView grvTempIssue;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_ISSUESERIALNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_ISSUEITMSTUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_DT;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_SUB_LOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_RMK;
    }
}
