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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GridViewDocuments = new System.Windows.Forms.DataGridView();
            this.Batch_Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Batch_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Doc_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit_Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDocuments)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewSerials)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(536, 183);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GridViewDocuments);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(528, 157);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Documents";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GridViewDocuments
            // 
            this.GridViewDocuments.AllowUserToAddRows = false;
            this.GridViewDocuments.AllowUserToResizeRows = false;
            this.GridViewDocuments.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewDocuments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridViewDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewDocuments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Batch_Line,
            this.Item_Code,
            this.Item_Status,
            this.Batch_No,
            this.Doc_No,
            this.Date,
            this.Unit_Cost,
            this.Qty});
            this.GridViewDocuments.EnableHeadersVisualStyles = false;
            this.GridViewDocuments.Location = new System.Drawing.Point(3, 6);
            this.GridViewDocuments.Name = "GridViewDocuments";
            this.GridViewDocuments.ReadOnly = true;
            this.GridViewDocuments.RowHeadersVisible = false;
            this.GridViewDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewDocuments.Size = new System.Drawing.Size(515, 145);
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
            // Item_Code
            // 
            this.Item_Code.DataPropertyName = "INB_ITM_CD";
            this.Item_Code.HeaderText = "Item Code";
            this.Item_Code.Name = "Item_Code";
            this.Item_Code.ReadOnly = true;
            // 
            // Item_Status
            // 
            this.Item_Status.DataPropertyName = "INB_ITM_STUS";
            this.Item_Status.HeaderText = "Item Status";
            this.Item_Status.Name = "Item_Status";
            this.Item_Status.ReadOnly = true;
            // 
            // Batch_No
            // 
            this.Batch_No.DataPropertyName = "INB_BATCH_NO";
            this.Batch_No.HeaderText = "Batch No";
            this.Batch_No.Name = "Batch_No";
            this.Batch_No.ReadOnly = true;
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
            // Unit_Cost
            // 
            this.Unit_Cost.DataPropertyName = "INB_UNIT_COST";
            this.Unit_Cost.HeaderText = "Unit Cost";
            this.Unit_Cost.Name = "Unit_Cost";
            this.Unit_Cost.ReadOnly = true;
            this.Unit_Cost.Visible = false;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "INB_QTY";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GridViewSerials);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(528, 157);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Serials";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GridViewSerials
            // 
            this.GridViewSerials.AllowUserToAddRows = false;
            this.GridViewSerials.AllowUserToResizeRows = false;
            this.GridViewSerials.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewSerials.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridViewSerials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this._Warranty_Period});
            this.GridViewSerials.EnableHeadersVisualStyles = false;
            this.GridViewSerials.Location = new System.Drawing.Point(3, 6);
            this.GridViewSerials.Name = "GridViewSerials";
            this.GridViewSerials.ReadOnly = true;
            this.GridViewSerials.RowHeadersVisible = false;
            this.GridViewSerials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewSerials.Size = new System.Drawing.Size(519, 145);
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
            // 
            // _Serial_2
            // 
            this._Serial_2.DataPropertyName = "INS_SER_2";
            this._Serial_2.HeaderText = "Serial 2";
            this._Serial_2.Name = "_Serial_2";
            this._Serial_2.ReadOnly = true;
            // 
            // _Serial_3
            // 
            this._Serial_3.DataPropertyName = "INS_SER_3";
            this._Serial_3.HeaderText = "Serial 3";
            this._Serial_3.Name = "_Serial_3";
            this._Serial_3.ReadOnly = true;
            // 
            // _ItemStatus
            // 
            this._ItemStatus.DataPropertyName = "INS_ITM_STUS";
            this._ItemStatus.HeaderText = "Item Status";
            this._ItemStatus.Name = "_ItemStatus";
            this._ItemStatus.ReadOnly = true;
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
            // ucItemSerialView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucItemSerialView";
            this.Size = new System.Drawing.Size(543, 195);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDocuments)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewSerials)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView GridViewDocuments;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch_Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Batch_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Doc_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit_Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
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
    }
}
