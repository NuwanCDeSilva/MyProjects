namespace FF.WindowsERPClient.Services
{
    partial class ServiceWIP_TempIssue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceWIP_TempIssue));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlVistiSelect = new System.Windows.Forms.Panel();
            this.dgvVisits = new System.Windows.Forms.DataGridView();
            this.selectVisit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.jtv_visit_line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jtv_visit_rmk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.inward_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_staus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serial_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERIAL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.job_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.job_line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyIssued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblvisitNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnVisit = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.chkReturnItems = new System.Windows.Forms.CheckBox();
            this.chkReturn = new System.Windows.Forms.CheckBox();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlVistiSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnSave,
            this.btnView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(769, 25);
            this.toolStrip1.TabIndex = 1;
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
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(2);
            this.btnSave.Size = new System.Drawing.Size(80, 22);
            this.btnSave.Text = "Issue";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnView
            // 
            this.btnView.AutoSize = false;
            this.btnView.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnView.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnView.Name = "btnView";
            this.btnView.Padding = new System.Windows.Forms.Padding(2);
            this.btnView.Size = new System.Drawing.Size(80, 22);
            this.btnView.Text = "View";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlMain.Controls.Add(this.pnlVistiSelect);
            this.pnlMain.Controls.Add(this.chkSelectAll);
            this.pnlMain.Controls.Add(this.dgvItems);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(769, 469);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlVistiSelect
            // 
            this.pnlVistiSelect.BackColor = System.Drawing.Color.Gray;
            this.pnlVistiSelect.Controls.Add(this.dgvVisits);
            this.pnlVistiSelect.Controls.Add(this.btnConfirm);
            this.pnlVistiSelect.Controls.Add(this.btnHide);
            this.pnlVistiSelect.Controls.Add(this.label20);
            this.pnlVistiSelect.Location = new System.Drawing.Point(126, 108);
            this.pnlVistiSelect.Name = "pnlVistiSelect";
            this.pnlVistiSelect.Size = new System.Drawing.Size(526, 312);
            this.pnlVistiSelect.TabIndex = 276;
            // 
            // dgvVisits
            // 
            this.dgvVisits.AllowUserToAddRows = false;
            this.dgvVisits.AllowUserToDeleteRows = false;
            this.dgvVisits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectVisit,
            this.jtv_visit_line,
            this.jtv_visit_rmk});
            this.dgvVisits.Location = new System.Drawing.Point(3, 24);
            this.dgvVisits.Name = "dgvVisits";
            this.dgvVisits.RowHeadersVisible = false;
            this.dgvVisits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVisits.Size = new System.Drawing.Size(520, 285);
            this.dgvVisits.TabIndex = 255;
            this.dgvVisits.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVisits_CellClick);
            // 
            // selectVisit
            // 
            this.selectVisit.HeaderText = "   ";
            this.selectVisit.Name = "selectVisit";
            this.selectVisit.Width = 30;
            // 
            // jtv_visit_line
            // 
            this.jtv_visit_line.DataPropertyName = "jtv_visit_line";
            this.jtv_visit_line.HeaderText = "Number";
            this.jtv_visit_line.Name = "jtv_visit_line";
            this.jtv_visit_line.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.jtv_visit_line.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.jtv_visit_line.Width = 50;
            // 
            // jtv_visit_rmk
            // 
            this.jtv_visit_rmk.DataPropertyName = "jtv_visit_rmk";
            this.jtv_visit_rmk.HeaderText = "Remark";
            this.jtv_visit_rmk.Name = "jtv_visit_rmk";
            this.jtv_visit_rmk.ReadOnly = true;
            this.jtv_visit_rmk.Width = 200;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(422, -1);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 254;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.panelcloseicon;
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.ForeColor = System.Drawing.Color.Lavender;
            this.btnHide.Location = new System.Drawing.Point(507, 0);
            this.btnHide.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(21, 21);
            this.btnHide.TabIndex = 252;
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(526, 21);
            this.label20.TabIndex = 0;
            this.label20.Text = "Select Visit number";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(12, 61);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chkSelectAll.TabIndex = 281;
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.inward_doc,
            this.item_code,
            this.item_staus,
            this.STATUS_CODE,
            this.serial_no,
            this.SERIAL_ID,
            this.job_no,
            this.job_line,
            this.Qty,
            this.QtyIssued,
            this.QTYBalance,
            this.Remark});
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.Location = new System.Drawing.Point(2, 58);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(764, 409);
            this.dgvItems.TabIndex = 282;
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellClick);
            this.dgvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellEndEdit);
            this.dgvItems.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItems_CellMouseUp);
            this.dgvItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvItems_CurrentCellDirtyStateChanged);
            this.dgvItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItems_DataError);
            this.dgvItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItems_EditingControlShowing);
            // 
            // select
            // 
            this.select.Frozen = true;
            this.select.HeaderText = "  ";
            this.select.Name = "select";
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.select.Width = 30;
            // 
            // inward_doc
            // 
            this.inward_doc.DataPropertyName = "inward_doc";
            this.inward_doc.HeaderText = "Inward Doc";
            this.inward_doc.Name = "inward_doc";
            this.inward_doc.ReadOnly = true;
            this.inward_doc.Visible = false;
            // 
            // item_code
            // 
            this.item_code.DataPropertyName = "item_code";
            this.item_code.HeaderText = "Item Code";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            // 
            // item_staus
            // 
            this.item_staus.DataPropertyName = "item_staus";
            this.item_staus.HeaderText = "Item Status";
            this.item_staus.Name = "item_staus";
            this.item_staus.ReadOnly = true;
            // 
            // STATUS_CODE
            // 
            this.STATUS_CODE.DataPropertyName = "STATUS_CODE";
            this.STATUS_CODE.HeaderText = "STATUS_CODE";
            this.STATUS_CODE.Name = "STATUS_CODE";
            this.STATUS_CODE.ReadOnly = true;
            this.STATUS_CODE.Visible = false;
            // 
            // serial_no
            // 
            this.serial_no.DataPropertyName = "serial_no";
            this.serial_no.HeaderText = "Serial No";
            this.serial_no.Name = "serial_no";
            this.serial_no.ReadOnly = true;
            // 
            // SERIAL_ID
            // 
            this.SERIAL_ID.DataPropertyName = "SERIAL_ID";
            this.SERIAL_ID.HeaderText = "SERIAL_ID";
            this.SERIAL_ID.Name = "SERIAL_ID";
            this.SERIAL_ID.ReadOnly = true;
            this.SERIAL_ID.Visible = false;
            // 
            // job_no
            // 
            this.job_no.DataPropertyName = "job_no";
            this.job_no.HeaderText = "Job No";
            this.job_no.Name = "job_no";
            this.job_no.ReadOnly = true;
            this.job_no.Visible = false;
            // 
            // job_line
            // 
            this.job_line.DataPropertyName = "job_line";
            this.job_line.HeaderText = "Job Line No";
            this.job_line.Name = "job_line";
            this.job_line.ReadOnly = true;
            this.job_line.Visible = false;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.Qty.DefaultCellStyle = dataGridViewCellStyle2;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // QtyIssued
            // 
            this.QtyIssued.DataPropertyName = "QtyIssued";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.QtyIssued.DefaultCellStyle = dataGridViewCellStyle3;
            this.QtyIssued.HeaderText = "Issued Qty";
            this.QtyIssued.Name = "QtyIssued";
            // 
            // QTYBalance
            // 
            this.QTYBalance.DataPropertyName = "QTYBalance";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.QTYBalance.DefaultCellStyle = dataGridViewCellStyle4;
            this.QTYBalance.HeaderText = "Balance Qty";
            this.QTYBalance.Name = "QTYBalance";
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "Remark";
            this.Remark.Name = "Remark";
            this.Remark.Width = 180;
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(393, 33);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(106, 20);
            this.dtpDate.TabIndex = 274;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(355, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 273;
            this.label5.Text = "Date";
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(266, 33);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(86, 20);
            this.txtSerial.TabIndex = 272;
            this.txtSerial.DoubleClick += new System.EventHandler(this.txtSerial_DoubleClick);
            this.txtSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerial_KeyDown);
            this.txtSerial.Leave += new System.EventHandler(this.txtSerial_Leave);
            // 
            // txtItemCode
            // 
            this.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemCode.Location = new System.Drawing.Point(64, 33);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(109, 20);
            this.txtItemCode.TabIndex = 269;
            this.txtItemCode.DoubleClick += new System.EventHandler(this.txtItemCode_DoubleClick);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Leave += new System.EventHandler(this.txtItemCode_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(218, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 270;
            this.label3.Text = "Serial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(5, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 271;
            this.label1.Text = "Item Code";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.lblvisitNum);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnVisit);
            this.panel1.Controls.Add(this.btnSearchItem);
            this.panel1.Controls.Add(this.chkReturnItems);
            this.panel1.Controls.Add(this.chkReturn);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 58);
            this.panel1.TabIndex = 5;
            // 
            // lblvisitNum
            // 
            this.lblvisitNum.AutoSize = true;
            this.lblvisitNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblvisitNum.ForeColor = System.Drawing.Color.Maroon;
            this.lblvisitNum.Location = new System.Drawing.Point(683, 35);
            this.lblvisitNum.Name = "lblvisitNum";
            this.lblvisitNum.Size = new System.Drawing.Size(0, 13);
            this.lblvisitNum.TabIndex = 277;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(617, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 276;
            this.label2.Text = "Visit Num :-";
            // 
            // btnVisit
            // 
            this.btnVisit.Location = new System.Drawing.Point(515, 30);
            this.btnVisit.Name = "btnVisit";
            this.btnVisit.Size = new System.Drawing.Size(96, 23);
            this.btnVisit.TabIndex = 271;
            this.btnVisit.Text = "Select A Visit";
            this.btnVisit.UseVisualStyleBackColor = true;
            this.btnVisit.Click += new System.EventHandler(this.btnVisit_Click);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearchItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchItem.BackgroundImage")));
            this.btnSearchItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearchItem.Location = new System.Drawing.Point(176, 31);
            this.btnSearchItem.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(20, 20);
            this.btnSearchItem.TabIndex = 270;
            this.btnSearchItem.UseVisualStyleBackColor = false;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearchItem_Click);
            // 
            // chkReturnItems
            // 
            this.chkReturnItems.AutoSize = true;
            this.chkReturnItems.Location = new System.Drawing.Point(126, 3);
            this.chkReturnItems.Name = "chkReturnItems";
            this.chkReturnItems.Size = new System.Drawing.Size(98, 17);
            this.chkReturnItems.TabIndex = 6;
            this.chkReturnItems.Text = "Returned Items";
            this.chkReturnItems.UseVisualStyleBackColor = true;
            this.chkReturnItems.CheckedChanged += new System.EventHandler(this.chkReturnItems_CheckedChanged);
            // 
            // chkReturn
            // 
            this.chkReturn.AutoSize = true;
            this.chkReturn.Location = new System.Drawing.Point(7, 3);
            this.chkReturn.Name = "chkReturn";
            this.chkReturn.Size = new System.Drawing.Size(85, 17);
            this.chkReturn.TabIndex = 5;
            this.chkReturn.Text = "Issued Items";
            this.chkReturn.UseVisualStyleBackColor = true;
            this.chkReturn.CheckedChanged += new System.EventHandler(this.chkReturn_CheckedChanged);
            // 
            // btnCloseFrom
            // 
            this.btnCloseFrom.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseFrom.Location = new System.Drawing.Point(379, 229);
            this.btnCloseFrom.Name = "btnCloseFrom";
            this.btnCloseFrom.Size = new System.Drawing.Size(10, 10);
            this.btnCloseFrom.TabIndex = 275;
            this.btnCloseFrom.Text = "  ";
            this.btnCloseFrom.UseVisualStyleBackColor = true;
            this.btnCloseFrom.Click += new System.EventHandler(this.btnCloseFrom_Click);
            // 
            // ServiceWIP_TempIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(769, 469);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnCloseFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceWIP_TempIssue";
            this.Text = "Service WIP Temp Issue";
            this.Load += new System.EventHandler(this.ServiceWIP_TempIssue_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlVistiSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnView;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkReturn;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.CheckBox chkReturnItems;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn inward_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_staus;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn serial_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIAL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn job_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn job_line;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyIssued;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnVisit;
        private System.Windows.Forms.Panel pnlVistiSelect;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DataGridView dgvVisits;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectVisit;
        private System.Windows.Forms.DataGridViewTextBoxColumn jtv_visit_line;
        private System.Windows.Forms.DataGridViewTextBoxColumn jtv_visit_rmk;
        private System.Windows.Forms.Label lblvisitNum;
    }
}