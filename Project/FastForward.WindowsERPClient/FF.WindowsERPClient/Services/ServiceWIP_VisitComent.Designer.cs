namespace FF.WindowsERPClient.Services
{
    partial class ServiceWIP_VisitComent
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
            this.pnlActualDefects = new System.Windows.Forms.Panel();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtJobNo = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.delete2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.JTV_VISIT_LINE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JTV_VISIT_RMK2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JTV_VISIT_FROM2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JTV_VISIT_TO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addemp2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.selectPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlActualDefects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlActualDefects
            // 
            this.pnlActualDefects.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlActualDefects.Controls.Add(this.dgvRecords);
            this.pnlActualDefects.Controls.Add(this.label15);
            this.pnlActualDefects.Controls.Add(this.panel2);
            this.pnlActualDefects.Location = new System.Drawing.Point(1, 2);
            this.pnlActualDefects.Name = "pnlActualDefects";
            this.pnlActualDefects.Size = new System.Drawing.Size(747, 404);
            this.pnlActualDefects.TabIndex = 0;
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.delete2,
            this.JTV_VISIT_LINE2,
            this.JTV_VISIT_RMK2,
            this.JTV_VISIT_FROM2,
            this.JTV_VISIT_TO2,
            this.addemp2,
            this.selectPrint});
            this.dgvRecords.EnableHeadersVisualStyles = false;
            this.dgvRecords.Location = new System.Drawing.Point(3, 54);
            this.dgvRecords.MultiSelect = false;
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.RowHeadersVisible = false;
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(740, 347);
            this.dgvRecords.TabIndex = 315;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.MidnightBlue;
            this.label15.ForeColor = System.Drawing.Color.Azure;
            this.label15.Location = new System.Drawing.Point(3, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(741, 14);
            this.label15.TabIndex = 314;
            this.label15.Text = "Visit Comments";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(201)))), ((int)(((byte)(167)))));
            this.panel2.Controls.Add(this.txtJobNo);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(741, 31);
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
            // delete2
            // 
            this.delete2.HeaderText = "  ";
            this.delete2.Image = global::FF.WindowsERPClient.Properties.Resources.deleteicon;
            this.delete2.Name = "delete2";
            this.delete2.Visible = false;
            this.delete2.Width = 30;
            // 
            // JTV_VISIT_LINE2
            // 
            this.JTV_VISIT_LINE2.DataPropertyName = "JTV_VISIT_LINE";
            this.JTV_VISIT_LINE2.HeaderText = "line No";
            this.JTV_VISIT_LINE2.Name = "JTV_VISIT_LINE2";
            this.JTV_VISIT_LINE2.ReadOnly = true;
            this.JTV_VISIT_LINE2.Width = 70;
            // 
            // JTV_VISIT_RMK2
            // 
            this.JTV_VISIT_RMK2.DataPropertyName = "JTV_VISIT_RMK";
            this.JTV_VISIT_RMK2.HeaderText = "Remark";
            this.JTV_VISIT_RMK2.Name = "JTV_VISIT_RMK2";
            this.JTV_VISIT_RMK2.ReadOnly = true;
            this.JTV_VISIT_RMK2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.JTV_VISIT_RMK2.Width = 400;
            // 
            // JTV_VISIT_FROM2
            // 
            this.JTV_VISIT_FROM2.DataPropertyName = "JTV_VISIT_FROM";
            this.JTV_VISIT_FROM2.HeaderText = "From";
            this.JTV_VISIT_FROM2.Name = "JTV_VISIT_FROM2";
            this.JTV_VISIT_FROM2.ReadOnly = true;
            this.JTV_VISIT_FROM2.Width = 130;
            // 
            // JTV_VISIT_TO2
            // 
            this.JTV_VISIT_TO2.DataPropertyName = "JTV_VISIT_TO";
            this.JTV_VISIT_TO2.HeaderText = "To";
            this.JTV_VISIT_TO2.Name = "JTV_VISIT_TO2";
            this.JTV_VISIT_TO2.ReadOnly = true;
            this.JTV_VISIT_TO2.Width = 130;
            // 
            // addemp2
            // 
            this.addemp2.HeaderText = "Emp Code";
            this.addemp2.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.addemp2.Name = "addemp2";
            this.addemp2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.addemp2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.addemp2.Visible = false;
            // 
            // selectPrint
            // 
            this.selectPrint.HeaderText = "Print";
            this.selectPrint.Name = "selectPrint";
            this.selectPrint.ReadOnly = true;
            this.selectPrint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.selectPrint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.selectPrint.Visible = false;
            // 
            // ServiceWIP_VisitComent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(750, 406);
            this.Controls.Add(this.pnlActualDefects);
            this.Controls.Add(this.btnCloseFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceWIP_VisitComent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Job base Visit Comments";
            this.Load += new System.EventHandler(this.ServicePayments_Load);
            this.pnlActualDefects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlActualDefects;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtJobNo;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.DataGridViewImageColumn delete2;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTV_VISIT_LINE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTV_VISIT_RMK2;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTV_VISIT_FROM2;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTV_VISIT_TO2;
        private System.Windows.Forms.DataGridViewImageColumn addemp2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectPrint;
    }
}