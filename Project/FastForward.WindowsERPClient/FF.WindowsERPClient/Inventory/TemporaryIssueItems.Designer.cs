namespace FF.WindowsERPClient.Inventory
{
    partial class TemporaryIssueItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemporaryIssueItems));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.chkSelAll = new System.Windows.Forms.CheckBox();
            this.grvRecItems = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.STIISSUEITMCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STIISSUEITMSTUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STIISSUESERIALNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STIISSUESERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STISUB_LOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STIRMK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.STI_ISSUEITMCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_ISSUEITMSTUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_ISSUESERIALNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_ISSUESERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_SUB_LOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STI_RMK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtStus = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSerId = new System.Windows.Forms.TextBox();
            this.txtRem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_srch_sub_loc = new System.Windows.Forms.Button();
            this.txtSubLoc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btn_srch_serial = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.chkReturn = new System.Windows.Forms.CheckBox();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvRecItems)).BeginInit();
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
            this.btnView.Visible = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlMain.Controls.Add(this.grvRecItems);
            this.pnlMain.Controls.Add(this.chkSelectAll);
            this.pnlMain.Controls.Add(this.dgvItems);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(769, 469);
            this.pnlMain.TabIndex = 2;
            // 
            // chkSelAll
            // 
            this.chkSelAll.AutoSize = true;
            this.chkSelAll.Location = new System.Drawing.Point(6, 111);
            this.chkSelAll.Name = "chkSelAll";
            this.chkSelAll.Size = new System.Drawing.Size(15, 14);
            this.chkSelAll.TabIndex = 284;
            this.chkSelAll.UseVisualStyleBackColor = true;
            this.chkSelAll.Visible = false;
            this.chkSelAll.CheckedChanged += new System.EventHandler(this.chkSelAll_CheckedChanged);
            // 
            // grvRecItems
            // 
            this.grvRecItems.AllowUserToAddRows = false;
            this.grvRecItems.AllowUserToDeleteRows = false;
            this.grvRecItems.AllowUserToResizeRows = false;
            this.grvRecItems.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvRecItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grvRecItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvRecItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.STIISSUEITMCD,
            this.STIISSUEITMSTUS,
            this.STIISSUESERIALNO,
            this.STIISSUESERID,
            this.STISUB_LOC,
            this.STIRMK});
            this.grvRecItems.EnableHeadersVisualStyles = false;
            this.grvRecItems.Location = new System.Drawing.Point(2, 102);
            this.grvRecItems.MultiSelect = false;
            this.grvRecItems.Name = "grvRecItems";
            this.grvRecItems.RowHeadersVisible = false;
            this.grvRecItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvRecItems.Size = new System.Drawing.Size(764, 363);
            this.grvRecItems.TabIndex = 283;
            this.grvRecItems.Visible = false;
            this.grvRecItems.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grvRecItems_CellBeginEdit);
            this.grvRecItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.grvRecItems_CurrentCellDirtyStateChanged);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.Width = 20;
            // 
            // STIISSUEITMCD
            // 
            this.STIISSUEITMCD.DataPropertyName = "STI_ISSUEITMCD";
            this.STIISSUEITMCD.HeaderText = "Item Code";
            this.STIISSUEITMCD.Name = "STIISSUEITMCD";
            this.STIISSUEITMCD.ReadOnly = true;
            this.STIISSUEITMCD.Width = 150;
            // 
            // STIISSUEITMSTUS
            // 
            this.STIISSUEITMSTUS.DataPropertyName = "STI_ISSUEITMSTUS";
            this.STIISSUEITMSTUS.HeaderText = "Item Status";
            this.STIISSUEITMSTUS.Name = "STIISSUEITMSTUS";
            this.STIISSUEITMSTUS.ReadOnly = true;
            this.STIISSUEITMSTUS.Width = 80;
            // 
            // STIISSUESERIALNO
            // 
            this.STIISSUESERIALNO.DataPropertyName = "STI_ISSUESERIALNO";
            this.STIISSUESERIALNO.HeaderText = "Serial No";
            this.STIISSUESERIALNO.Name = "STIISSUESERIALNO";
            this.STIISSUESERIALNO.ReadOnly = true;
            this.STIISSUESERIALNO.Width = 200;
            // 
            // STIISSUESERID
            // 
            this.STIISSUESERID.DataPropertyName = "STI_ISSUESERID";
            this.STIISSUESERID.HeaderText = "SERIAL_ID";
            this.STIISSUESERID.Name = "STIISSUESERID";
            this.STIISSUESERID.ReadOnly = true;
            this.STIISSUESERID.Visible = false;
            // 
            // STISUB_LOC
            // 
            this.STISUB_LOC.DataPropertyName = "STI_SUB_LOC";
            this.STISUB_LOC.HeaderText = "Sub Location";
            this.STISUB_LOC.Name = "STISUB_LOC";
            this.STISUB_LOC.Width = 70;
            // 
            // STIRMK
            // 
            this.STIRMK.DataPropertyName = "STI_RMK";
            this.STIRMK.HeaderText = "Remark";
            this.STIRMK.Name = "STIRMK";
            this.STIRMK.Width = 220;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(33, 182);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chkSelectAll.TabIndex = 281;
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.Visible = false;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.STI_ISSUEITMCD,
            this.STI_ISSUEITMSTUS,
            this.STI_ISSUESERIALNO,
            this.STI_ISSUESERID,
            this.STI_SUB_LOC,
            this.STI_RMK});
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.Location = new System.Drawing.Point(2, 102);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(764, 363);
            this.dgvItems.TabIndex = 282;
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellClick);
            this.dgvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellEndEdit);
            this.dgvItems.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItems_CellMouseUp);
            this.dgvItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvItems_CurrentCellDirtyStateChanged);
            this.dgvItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItems_DataError);
            this.dgvItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItems_EditingControlShowing);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Image = global::FF.WindowsERPClient.Properties.Resources.deleteicon;
            this.Column1.Name = "Column1";
            this.Column1.Width = 30;
            // 
            // STI_ISSUEITMCD
            // 
            this.STI_ISSUEITMCD.DataPropertyName = "STI_ISSUEITMCD";
            this.STI_ISSUEITMCD.HeaderText = "Item Code";
            this.STI_ISSUEITMCD.Name = "STI_ISSUEITMCD";
            this.STI_ISSUEITMCD.ReadOnly = true;
            this.STI_ISSUEITMCD.Width = 150;
            // 
            // STI_ISSUEITMSTUS
            // 
            this.STI_ISSUEITMSTUS.DataPropertyName = "STI_ISSUEITMSTUS";
            this.STI_ISSUEITMSTUS.HeaderText = "Item Status";
            this.STI_ISSUEITMSTUS.Name = "STI_ISSUEITMSTUS";
            this.STI_ISSUEITMSTUS.ReadOnly = true;
            this.STI_ISSUEITMSTUS.Width = 80;
            // 
            // STI_ISSUESERIALNO
            // 
            this.STI_ISSUESERIALNO.DataPropertyName = "STI_ISSUESERIALNO";
            this.STI_ISSUESERIALNO.HeaderText = "Serial No";
            this.STI_ISSUESERIALNO.Name = "STI_ISSUESERIALNO";
            this.STI_ISSUESERIALNO.ReadOnly = true;
            this.STI_ISSUESERIALNO.Width = 200;
            // 
            // STI_ISSUESERID
            // 
            this.STI_ISSUESERID.DataPropertyName = "STI_ISSUESERID";
            this.STI_ISSUESERID.HeaderText = "SERIAL_ID";
            this.STI_ISSUESERID.Name = "STI_ISSUESERID";
            this.STI_ISSUESERID.ReadOnly = true;
            this.STI_ISSUESERID.Visible = false;
            // 
            // STI_SUB_LOC
            // 
            this.STI_SUB_LOC.DataPropertyName = "STI_SUB_LOC";
            this.STI_SUB_LOC.HeaderText = "Sub Location";
            this.STI_SUB_LOC.Name = "STI_SUB_LOC";
            this.STI_SUB_LOC.Width = 70;
            // 
            // STI_RMK
            // 
            this.STI_RMK.DataPropertyName = "STI_RMK";
            this.STI_RMK.HeaderText = "Remark";
            this.STI_RMK.Name = "STI_RMK";
            this.STI_RMK.Width = 220;
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(598, 32);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(106, 20);
            this.dtpDate.TabIndex = 274;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(560, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 273;
            this.label5.Text = "Date";
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(304, 32);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(220, 20);
            this.txtSerial.TabIndex = 272;
            this.txtSerial.DoubleClick += new System.EventHandler(this.txtSerial_DoubleClick);
            this.txtSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerial_KeyDown);
            this.txtSerial.Leave += new System.EventHandler(this.txtSerial_Leave);
            // 
            // txtItemCode
            // 
            this.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemCode.Location = new System.Drawing.Point(79, 32);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(136, 20);
            this.txtItemCode.TabIndex = 269;
            this.txtItemCode.DoubleClick += new System.EventHandler(this.txtItemCode_DoubleClick);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Leave += new System.EventHandler(this.txtItemCode_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(254, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 270;
            this.label3.Text = "Serial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 271;
            this.label1.Text = "Item Code";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.txtStus);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtSerId);
            this.panel1.Controls.Add(this.txtRem);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btn_srch_sub_loc);
            this.panel1.Controls.Add(this.txtSubLoc);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btn_srch_serial);
            this.panel1.Controls.Add(this.btnSearchItem);
            this.panel1.Controls.Add(this.chkReturn);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 100);
            this.panel1.TabIndex = 5;
            // 
            // txtStus
            // 
            this.txtStus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStus.Location = new System.Drawing.Point(304, 54);
            this.txtStus.Name = "txtStus";
            this.txtStus.ReadOnly = true;
            this.txtStus.Size = new System.Drawing.Size(94, 20);
            this.txtStus.TabIndex = 279;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(254, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 280;
            this.label6.Text = "Status";
            // 
            // txtSerId
            // 
            this.txtSerId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSerId.Location = new System.Drawing.Point(706, 26);
            this.txtSerId.Name = "txtSerId";
            this.txtSerId.Size = new System.Drawing.Size(58, 20);
            this.txtSerId.TabIndex = 278;
            this.txtSerId.Visible = false;
            this.txtSerId.Leave += new System.EventHandler(this.txtSerId_Leave);
            // 
            // txtRem
            // 
            this.txtRem.Location = new System.Drawing.Point(79, 76);
            this.txtRem.MaxLength = 200;
            this.txtRem.Name = "txtRem";
            this.txtRem.Size = new System.Drawing.Size(633, 20);
            this.txtRem.TabIndex = 277;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(29, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 276;
            this.label4.Text = "Remarks";
            // 
            // btn_srch_sub_loc
            // 
            this.btn_srch_sub_loc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_srch_sub_loc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_srch_sub_loc.BackgroundImage")));
            this.btn_srch_sub_loc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_srch_sub_loc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_srch_sub_loc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_srch_sub_loc.Location = new System.Drawing.Point(178, 54);
            this.btn_srch_sub_loc.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_srch_sub_loc.Name = "btn_srch_sub_loc";
            this.btn_srch_sub_loc.Size = new System.Drawing.Size(20, 20);
            this.btn_srch_sub_loc.TabIndex = 274;
            this.btn_srch_sub_loc.UseVisualStyleBackColor = false;
            this.btn_srch_sub_loc.Click += new System.EventHandler(this.btn_srch_sub_loc_Click);
            // 
            // txtSubLoc
            // 
            this.txtSubLoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSubLoc.Location = new System.Drawing.Point(79, 54);
            this.txtSubLoc.Name = "txtSubLoc";
            this.txtSubLoc.Size = new System.Drawing.Size(94, 20);
            this.txtSubLoc.TabIndex = 273;
            this.txtSubLoc.DoubleClick += new System.EventHandler(this.txtSubLoc_DoubleClick);
            this.txtSubLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubLoc_KeyDown);
            this.txtSubLoc.Leave += new System.EventHandler(this.txtSubLoc_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 275;
            this.label2.Text = "Sub Location";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAdd.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.dwnarrowgridicon;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.Location = new System.Drawing.Point(722, 71);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(30, 25);
            this.btnAdd.TabIndex = 272;
            this.btnAdd.UseCompatibleTextRendering = true;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btn_srch_serial
            // 
            this.btn_srch_serial.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_srch_serial.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_srch_serial.BackgroundImage")));
            this.btn_srch_serial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_srch_serial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_srch_serial.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_srch_serial.Location = new System.Drawing.Point(527, 32);
            this.btn_srch_serial.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_srch_serial.Name = "btn_srch_serial";
            this.btn_srch_serial.Size = new System.Drawing.Size(20, 20);
            this.btn_srch_serial.TabIndex = 271;
            this.btn_srch_serial.UseVisualStyleBackColor = false;
            this.btn_srch_serial.Click += new System.EventHandler(this.btn_srch_serial_Click);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearchItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchItem.BackgroundImage")));
            this.btnSearchItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearchItem.Location = new System.Drawing.Point(222, 32);
            this.btnSearchItem.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(20, 20);
            this.btnSearchItem.TabIndex = 270;
            this.btnSearchItem.UseVisualStyleBackColor = false;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearchItem_Click);
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
            // TemporaryIssueItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(769, 469);
            this.Controls.Add(this.chkSelAll);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnCloseFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "TemporaryIssueItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Temporary Issue Items";
            this.Load += new System.EventHandler(this.ServiceWIP_TempIssue_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvRecItems)).EndInit();
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
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.Button btn_srch_serial;
        private System.Windows.Forms.TextBox txtRem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_srch_sub_loc;
        private System.Windows.Forms.TextBox txtSubLoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtSerId;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_ISSUEITMCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_ISSUEITMSTUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_ISSUESERIALNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_ISSUESERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_SUB_LOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn STI_RMK;
        private System.Windows.Forms.TextBox txtStus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView grvRecItems;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn STIISSUEITMCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn STIISSUEITMSTUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn STIISSUESERIALNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STIISSUESERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn STISUB_LOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn STIRMK;
        private System.Windows.Forms.CheckBox chkSelAll;
    }
}