namespace FF.WindowsERPClient.Services
{
    partial class ServiceWIP_VisitComment
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlAddNewRecord = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.dgvTechnicians = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.STH_EMP_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESEP_FIRST_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MT_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnHide = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.txtComment = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSaveActualDefects = new System.Windows.Forms.ToolStripButton();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.delete2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.JTV_VISIT_LINE2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JTV_VISIT_RMK2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JTV_VISIT_FROM2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JTV_VISIT_TO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addemp2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.selectPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlAddNewRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTechnicians)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlMain.Controls.Add(this.button1);
            this.pnlMain.Controls.Add(this.pnlAddNewRecord);
            this.pnlMain.Controls.Add(this.dgvRecords);
            this.pnlMain.Controls.Add(this.dtpTo);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.dtpFrom);
            this.pnlMain.Controls.Add(this.txtComment);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.toolStrip1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(759, 459);
            this.pnlMain.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Image = global::FF.WindowsERPClient.Properties.Resources.dwnarrowgridicon;
            this.button1.Location = new System.Drawing.Point(718, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 26);
            this.button1.TabIndex = 23;
            this.button1.Text = " ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnlAddNewRecord
            // 
            this.pnlAddNewRecord.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAddNewRecord.Controls.Add(this.btnCancel);
            this.pnlAddNewRecord.Controls.Add(this.btnAddItem);
            this.pnlAddNewRecord.Controls.Add(this.dgvTechnicians);
            this.pnlAddNewRecord.Controls.Add(this.btnHide);
            this.pnlAddNewRecord.Controls.Add(this.label10);
            this.pnlAddNewRecord.Location = new System.Drawing.Point(166, 100);
            this.pnlAddNewRecord.Name = "pnlAddNewRecord";
            this.pnlAddNewRecord.Size = new System.Drawing.Size(111, 67);
            this.pnlAddNewRecord.TabIndex = 21;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(419, 258);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 29);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddItem.Location = new System.Drawing.Point(323, 258);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(90, 29);
            this.btnAddItem.TabIndex = 6;
            this.btnAddItem.Text = "Add";
            this.btnAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // dgvTechnicians
            // 
            this.dgvTechnicians.AllowUserToAddRows = false;
            this.dgvTechnicians.AllowUserToDeleteRows = false;
            this.dgvTechnicians.AllowUserToOrderColumns = true;
            this.dgvTechnicians.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTechnicians.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTechnicians.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTechnicians.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.STH_EMP_CD,
            this.ESEP_FIRST_NAME,
            this.MT_DESC});
            this.dgvTechnicians.EnableHeadersVisualStyles = false;
            this.dgvTechnicians.Location = new System.Drawing.Point(9, 23);
            this.dgvTechnicians.MultiSelect = false;
            this.dgvTechnicians.Name = "dgvTechnicians";
            this.dgvTechnicians.RowHeadersVisible = false;
            this.dgvTechnicians.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTechnicians.Size = new System.Drawing.Size(500, 226);
            this.dgvTechnicians.TabIndex = 19;
            // 
            // select
            // 
            this.select.Frozen = true;
            this.select.HeaderText = "  ";
            this.select.Name = "select";
            this.select.Width = 30;
            // 
            // STH_EMP_CD
            // 
            this.STH_EMP_CD.DataPropertyName = "STH_EMP_CD";
            this.STH_EMP_CD.HeaderText = "Employee code";
            this.STH_EMP_CD.Name = "STH_EMP_CD";
            this.STH_EMP_CD.ReadOnly = true;
            this.STH_EMP_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.STH_EMP_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ESEP_FIRST_NAME
            // 
            this.ESEP_FIRST_NAME.DataPropertyName = "ESEP_FIRST_NAME";
            this.ESEP_FIRST_NAME.HeaderText = "Name";
            this.ESEP_FIRST_NAME.Name = "ESEP_FIRST_NAME";
            this.ESEP_FIRST_NAME.ReadOnly = true;
            this.ESEP_FIRST_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ESEP_FIRST_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ESEP_FIRST_NAME.Width = 250;
            // 
            // MT_DESC
            // 
            this.MT_DESC.DataPropertyName = "MT_DESC";
            this.MT_DESC.HeaderText = "Town";
            this.MT_DESC.Name = "MT_DESC";
            this.MT_DESC.ReadOnly = true;
            this.MT_DESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MT_DESC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MT_DESC.Width = 150;
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.panelcloseicon;
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.ForeColor = System.Drawing.Color.Lavender;
            this.btnHide.Location = new System.Drawing.Point(90, 0);
            this.btnHide.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(21, 21);
            this.btnHide.TabIndex = 8;
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Teal;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 20);
            this.label10.TabIndex = 10;
            this.label10.Text = "Allocated Technicians";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label10_MouseDown);
            this.label10.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label10_MouseMove);
            this.label10.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label10_MouseUp);
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            this.dgvRecords.Location = new System.Drawing.Point(3, 117);
            this.dgvRecords.MultiSelect = false;
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.RowHeadersVisible = false;
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(753, 339);
            this.dgvRecords.TabIndex = 22;
            this.dgvRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecords_CellClick);
            this.dgvRecords.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecords_CellEndEdit);
            this.dgvRecords.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvRecords_CurrentCellDirtyStateChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MMM/yyyy  hh:mm:ss tt";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(307, 96);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "To";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MMM/yyyy  hh:mm:ss tt";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(48, 96);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 16;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(2, 27);
            this.txtComment.MaxLength = 200;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtComment.Size = new System.Drawing.Size(751, 58);
            this.txtComment.TabIndex = 2;
            this.txtComment.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Comment";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnPrint,
            this.btnSaveActualDefects});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(759, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = false;
            this.btnClear.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(2);
            this.btnClear.Size = new System.Drawing.Size(80, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AutoSize = false;
            this.btnPrint.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(2);
            this.btnPrint.Size = new System.Drawing.Size(80, 22);
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSaveActualDefects
            // 
            this.btnSaveActualDefects.AutoSize = false;
            this.btnSaveActualDefects.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSaveActualDefects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSaveActualDefects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveActualDefects.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnSaveActualDefects.Name = "btnSaveActualDefects";
            this.btnSaveActualDefects.Padding = new System.Windows.Forms.Padding(2);
            this.btnSaveActualDefects.Size = new System.Drawing.Size(80, 22);
            this.btnSaveActualDefects.Text = "Save";
            this.btnSaveActualDefects.Click += new System.EventHandler(this.btnSaveActualDefects_Click);
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
            this.JTV_VISIT_RMK2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.JTV_VISIT_RMK2.Width = 250;
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
            // 
            // selectPrint
            // 
            this.selectPrint.HeaderText = "Print";
            this.selectPrint.Name = "selectPrint";
            this.selectPrint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.selectPrint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ServiceWIP_VisitComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(759, 459);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnCloseFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceWIP_VisitComment";
            this.Text = "Service WIP Visit Comment";
            this.Load += new System.EventHandler(this.ServiceWIP_VisitComment_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlAddNewRecord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTechnicians)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveActualDefects;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.RichTextBox txtComment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvTechnicians;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn STH_EMP_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESEP_FIRST_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MT_DESC;
        private System.Windows.Forms.Panel pnlAddNewRecord;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.DataGridViewImageColumn delete2;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTV_VISIT_LINE2;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTV_VISIT_RMK2;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTV_VISIT_FROM2;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTV_VISIT_TO2;
        private System.Windows.Forms.DataGridViewImageColumn addemp2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectPrint;
    }
}