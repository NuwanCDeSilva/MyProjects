namespace FF.WindowsERPClient.Services
{
    partial class ServiceWIP_oldPartRemove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceWIP_oldPartRemove));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnRefix = new System.Windows.Forms.ToolStripButton();
            this.BtnRemove = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SOP_LINE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOP_OLDITMCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRIPTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOP_TP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOP_TP_text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOP_OLDITMSTUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOP_OLDITMSTUS_TEXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOP_OLDITMSER1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNewSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOP_OLDITMQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOP_RMK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblItemSerialStatus = new System.Windows.Forms.Label();
            this.lblItemBrand = new System.Windows.Forms.Label();
            this.lblItemModel = new System.Windows.Forms.Label();
            this.lblItemDescription = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.lblPartNo = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsPeri = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnRefix,
            this.btnSave,
            this.BtnRemove,
            this.btnView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(779, 25);
            this.toolStrip1.TabIndex = 263;
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
            // btnRefix
            // 
            this.btnRefix.AutoSize = false;
            this.btnRefix.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRefix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefix.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefix.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnRefix.Name = "btnRefix";
            this.btnRefix.Padding = new System.Windows.Forms.Padding(2);
            this.btnRefix.Size = new System.Drawing.Size(80, 22);
            this.btnRefix.Text = "Re-Fix";
            this.btnRefix.Click += new System.EventHandler(this.btnRefix_Click);
            // 
            // BtnRemove
            // 
            this.BtnRemove.AutoSize = false;
            this.BtnRemove.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnRemove.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.Padding = new System.Windows.Forms.Padding(2);
            this.BtnRemove.Size = new System.Drawing.Size(80, 22);
            this.BtnRemove.Text = "Remove";
            this.BtnRemove.Visible = false;
            this.BtnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
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
            this.btnSave.Text = "Remove";
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
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(437, 14);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(187, 20);
            this.txtSerial.TabIndex = 267;
            this.txtSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerial_KeyDown);
            this.txtSerial.Leave += new System.EventHandler(this.txtSerial_Leave);
            // 
            // txtItemCode
            // 
            this.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemCode.Location = new System.Drawing.Point(64, 14);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(122, 20);
            this.txtItemCode.TabIndex = 266;
            this.txtItemCode.TextChanged += new System.EventHandler(this.txtItemCode_TextChanged);
            this.txtItemCode.DoubleClick += new System.EventHandler(this.txtItemCode_DoubleClick);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Leave += new System.EventHandler(this.txtItemCode_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 264;
            this.label3.Text = "Serial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 265;
            this.label1.Text = "Item Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 269;
            this.label2.Text = "Item Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.DropDownWidth = 120;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(272, 14);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(87, 21);
            this.cmbStatus.TabIndex = 268;
            this.cmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbStatus_KeyDown);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.Transparent;
            this.btnAddItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddItem.BackgroundImage")));
            this.btnAddItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddItem.Location = new System.Drawing.Point(732, 15);
            this.btnAddItem.MaximumSize = new System.Drawing.Size(32, 36);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(32, 30);
            this.btnAddItem.TabIndex = 272;
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Visible = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(659, 15);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(66, 20);
            this.txtQty.TabIndex = 271;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(634, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 270;
            this.label4.Text = "Qty";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.delete,
            this.select,
            this.SOP_LINE,
            this.SOP_OLDITMCD,
            this.DESCRIPTION,
            this.PARTNO,
            this.SOP_TP,
            this.SOP_TP_text,
            this.SOP_OLDITMSTUS,
            this.SOP_OLDITMSTUS_TEXT,
            this.SOP_OLDITMSER1,
            this.colNewSerial,
            this.SOP_OLDITMQTY,
            this.SOP_RMK,
            this.serialId});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.Location = new System.Drawing.Point(1, 135);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(776, 342);
            this.dgvItems.TabIndex = 273;
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellClick);
            this.dgvItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellContentClick);
            this.dgvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellEndEdit);
            this.dgvItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvItems_CurrentCellDirtyStateChanged);
            this.dgvItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvItems_DataError_1);
            this.dgvItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItems_EditingControlShowing);
            // 
            // delete
            // 
            this.delete.HeaderText = "  ";
            this.delete.Image = global::FF.WindowsERPClient.Properties.Resources.deleteicon;
            this.delete.Name = "delete";
            this.delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.delete.Visible = false;
            this.delete.Width = 30;
            // 
            // select
            // 
            this.select.HeaderText = "  ";
            this.select.Name = "select";
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.select.Width = 30;
            // 
            // SOP_LINE
            // 
            this.SOP_LINE.DataPropertyName = "dgvLine";
            this.SOP_LINE.HeaderText = " # ";
            this.SOP_LINE.Name = "SOP_LINE";
            this.SOP_LINE.ReadOnly = true;
            this.SOP_LINE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SOP_LINE.Visible = false;
            this.SOP_LINE.Width = 35;
            // 
            // SOP_OLDITMCD
            // 
            this.SOP_OLDITMCD.DataPropertyName = "SOP_OLDITMCD";
            this.SOP_OLDITMCD.HeaderText = "Item Code";
            this.SOP_OLDITMCD.Name = "SOP_OLDITMCD";
            this.SOP_OLDITMCD.ReadOnly = true;
            this.SOP_OLDITMCD.Width = 130;
            // 
            // DESCRIPTION
            // 
            this.DESCRIPTION.DataPropertyName = "DESCRIPTION";
            this.DESCRIPTION.HeaderText = "Description";
            this.DESCRIPTION.Name = "DESCRIPTION";
            this.DESCRIPTION.ReadOnly = true;
            // 
            // PARTNO
            // 
            this.PARTNO.DataPropertyName = "PARTNO";
            this.PARTNO.HeaderText = "Part No";
            this.PARTNO.Name = "PARTNO";
            this.PARTNO.ReadOnly = true;
            // 
            // SOP_TP
            // 
            this.SOP_TP.DataPropertyName = "SOP_TP";
            this.SOP_TP.HeaderText = "Type";
            this.SOP_TP.Name = "SOP_TP";
            this.SOP_TP.ReadOnly = true;
            this.SOP_TP.Visible = false;
            // 
            // SOP_TP_text
            // 
            this.SOP_TP_text.DataPropertyName = "SOP_TP_text";
            this.SOP_TP_text.HeaderText = "Type";
            this.SOP_TP_text.Name = "SOP_TP_text";
            this.SOP_TP_text.ReadOnly = true;
            // 
            // SOP_OLDITMSTUS
            // 
            this.SOP_OLDITMSTUS.DataPropertyName = "SOP_OLDITMSTUS";
            this.SOP_OLDITMSTUS.HeaderText = "Status";
            this.SOP_OLDITMSTUS.Name = "SOP_OLDITMSTUS";
            this.SOP_OLDITMSTUS.ReadOnly = true;
            this.SOP_OLDITMSTUS.Visible = false;
            this.SOP_OLDITMSTUS.Width = 130;
            // 
            // SOP_OLDITMSTUS_TEXT
            // 
            this.SOP_OLDITMSTUS_TEXT.DataPropertyName = "SOP_OLDITMSTUS_TEXT";
            this.SOP_OLDITMSTUS_TEXT.HeaderText = "Status";
            this.SOP_OLDITMSTUS_TEXT.Name = "SOP_OLDITMSTUS_TEXT";
            this.SOP_OLDITMSTUS_TEXT.ReadOnly = true;
            // 
            // SOP_OLDITMSER1
            // 
            this.SOP_OLDITMSER1.DataPropertyName = "SOP_OLDITMSER1";
            this.SOP_OLDITMSER1.HeaderText = "Serial";
            this.SOP_OLDITMSER1.Name = "SOP_OLDITMSER1";
            this.SOP_OLDITMSER1.ReadOnly = true;
            this.SOP_OLDITMSER1.Width = 120;
            // 
            // colNewSerial
            // 
            this.colNewSerial.HeaderText = "New Serial";
            this.colNewSerial.Name = "colNewSerial";
            this.colNewSerial.Visible = false;
            // 
            // SOP_OLDITMQTY
            // 
            this.SOP_OLDITMQTY.DataPropertyName = "SOP_OLDITMQTY";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.NullValue = "-1";
            this.SOP_OLDITMQTY.DefaultCellStyle = dataGridViewCellStyle3;
            this.SOP_OLDITMQTY.HeaderText = "Qty";
            this.SOP_OLDITMQTY.Name = "SOP_OLDITMQTY";
            this.SOP_OLDITMQTY.ReadOnly = true;
            this.SOP_OLDITMQTY.Width = 80;
            // 
            // SOP_RMK
            // 
            this.SOP_RMK.DataPropertyName = "SOP_RMK";
            this.SOP_RMK.HeaderText = "Remark";
            this.SOP_RMK.Name = "SOP_RMK";
            this.SOP_RMK.ReadOnly = true;
            this.SOP_RMK.Width = 250;
            // 
            // serialId
            // 
            this.serialId.HeaderText = "Serial Id";
            this.serialId.Name = "serialId";
            this.serialId.Visible = false;
            // 
            // lblItemSerialStatus
            // 
            this.lblItemSerialStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemSerialStatus.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblItemSerialStatus.Location = new System.Drawing.Point(609, 39);
            this.lblItemSerialStatus.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblItemSerialStatus.Name = "lblItemSerialStatus";
            this.lblItemSerialStatus.Size = new System.Drawing.Size(174, 15);
            this.lblItemSerialStatus.TabIndex = 277;
            this.lblItemSerialStatus.Text = "Status : ";
            // 
            // lblItemBrand
            // 
            this.lblItemBrand.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemBrand.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblItemBrand.Location = new System.Drawing.Point(486, 39);
            this.lblItemBrand.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblItemBrand.Name = "lblItemBrand";
            this.lblItemBrand.Size = new System.Drawing.Size(120, 15);
            this.lblItemBrand.TabIndex = 276;
            this.lblItemBrand.Text = "Brand : ";
            // 
            // lblItemModel
            // 
            this.lblItemModel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemModel.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblItemModel.Location = new System.Drawing.Point(279, 39);
            this.lblItemModel.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblItemModel.Name = "lblItemModel";
            this.lblItemModel.Size = new System.Drawing.Size(202, 15);
            this.lblItemModel.TabIndex = 275;
            this.lblItemModel.Text = "Model : ";
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemDescription.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblItemDescription.Location = new System.Drawing.Point(5, 38);
            this.lblItemDescription.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(275, 16);
            this.lblItemDescription.TabIndex = 274;
            this.lblItemDescription.Text = "Description";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(274, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(106, 20);
            this.dtpDate.TabIndex = 279;
            this.dtpDate.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(236, 6);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 278;
            this.label5.Text = "Date";
            this.label5.Visible = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(11, 138);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chkSelectAll.TabIndex = 280;
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Image = global::FF.WindowsERPClient.Properties.Resources.dwnarrowgridicon;
            this.btnRemoveItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveItem.Location = new System.Drawing.Point(616, 104);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(81, 23);
            this.btnRemoveItem.TabIndex = 281;
            this.btnRemoveItem.Text = "Add";
            this.btnRemoveItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 282;
            this.label6.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(64, 106);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(546, 20);
            this.txtRemark.TabIndex = 283;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSearchItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchItem.BackgroundImage")));
            this.btnSearchItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchItem.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnSearchItem.Location = new System.Drawing.Point(187, 14);
            this.btnSearchItem.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(20, 20);
            this.btnSearchItem.TabIndex = 284;
            this.btnSearchItem.UseVisualStyleBackColor = false;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearchItem_Click);
            // 
            // lblPartNo
            // 
            this.lblPartNo.AutoSize = true;
            this.lblPartNo.Location = new System.Drawing.Point(788, 43);
            this.lblPartNo.Name = "lblPartNo";
            this.lblPartNo.Size = new System.Drawing.Size(39, 13);
            this.lblPartNo.TabIndex = 285;
            this.lblPartNo.Text = "partNo";
            this.lblPartNo.Visible = false;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(788, 56);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(58, 13);
            this.lblDesc.TabIndex = 285;
            this.lblDesc.Text = "description";
            this.lblDesc.Visible = false;
            // 
            // btnCloseFrom
            // 
            this.btnCloseFrom.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseFrom.Location = new System.Drawing.Point(492, 137);
            this.btnCloseFrom.Name = "btnCloseFrom";
            this.btnCloseFrom.Size = new System.Drawing.Size(10, 10);
            this.btnCloseFrom.TabIndex = 286;
            this.btnCloseFrom.Text = "  ";
            this.btnCloseFrom.UseVisualStyleBackColor = true;
            this.btnCloseFrom.Click += new System.EventHandler(this.btnCloseFrom_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.btnCloseFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnSearchItem);
            this.groupBox1.Controls.Add(this.txtItemCode);
            this.groupBox1.Controls.Add(this.txtSerial);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.btnAddItem);
            this.groupBox1.Controls.Add(this.lblItemDescription);
            this.groupBox1.Controls.Add(this.lblItemSerialStatus);
            this.groupBox1.Controls.Add(this.lblItemModel);
            this.groupBox1.Controls.Add(this.lblItemBrand);
            this.groupBox1.Location = new System.Drawing.Point(2, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 63);
            this.groupBox1.TabIndex = 287;
            this.groupBox1.TabStop = false;
            // 
            // chkIsPeri
            // 
            this.chkIsPeri.AutoSize = true;
            this.chkIsPeri.BackColor = System.Drawing.Color.White;
            this.chkIsPeri.Location = new System.Drawing.Point(3, 4);
            this.chkIsPeri.Name = "chkIsPeri";
            this.chkIsPeri.Size = new System.Drawing.Size(78, 17);
            this.chkIsPeri.TabIndex = 288;
            this.chkIsPeri.Text = "Peripharals";
            this.chkIsPeri.UseVisualStyleBackColor = false;
            this.chkIsPeri.CheckedChanged += new System.EventHandler(this.chkIsPeri_CheckedChanged);
            // 
            // ServiceWIP_oldPartRemove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(779, 479);
            this.Controls.Add(this.chkIsPeri);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblPartNo);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceWIP_oldPartRemove";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Service Old Part Remove";
            this.Load += new System.EventHandler(this.ServiceWIP_oldPartRemove_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnRefix;
        private System.Windows.Forms.ToolStripButton BtnRemove;
        private System.Windows.Forms.ToolStripButton btnView;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblItemSerialStatus;
        private System.Windows.Forms.Label lblItemBrand;
        private System.Windows.Forms.Label lblItemModel;
        private System.Windows.Forms.Label lblItemDescription;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.Label lblPartNo;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkIsPeri;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.DataGridViewImageColumn delete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_LINE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_OLDITMCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRIPTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn PARTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_TP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_TP_text;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_OLDITMSTUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_OLDITMSTUS_TEXT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_OLDITMSER1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNewSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_OLDITMQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOP_RMK;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialId;
    }
}