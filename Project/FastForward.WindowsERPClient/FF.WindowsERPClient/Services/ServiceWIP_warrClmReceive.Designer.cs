namespace FF.WindowsERPClient.Services
{
    partial class ServiceWIP_warrClmReceive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceWIP_warrClmReceive));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlAddNewRecord = new System.Windows.Forms.Panel();
            this.btnVouNo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.txtCaseID = new System.Windows.Forms.TextBox();
            this.txtOEM = new System.Windows.Forms.TextBox();
            this.txtPartID = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSerialPnl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHide = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.swd_seq_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_doc_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_suppitmcd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_itmcd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_itm_stus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_itm_stusText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_ser1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_othdocno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swd_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlAddNewRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlMain.Controls.Add(this.pnlAddNewRecord);
            this.pnlMain.Controls.Add(this.chkSelectAll);
            this.pnlMain.Controls.Add(this.dgvItems);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(759, 459);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlAddNewRecord
            // 
            this.pnlAddNewRecord.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAddNewRecord.Controls.Add(this.btnVouNo);
            this.pnlAddNewRecord.Controls.Add(this.btnCancel);
            this.pnlAddNewRecord.Controls.Add(this.btnAddItem);
            this.pnlAddNewRecord.Controls.Add(this.txtCaseID);
            this.pnlAddNewRecord.Controls.Add(this.txtOEM);
            this.pnlAddNewRecord.Controls.Add(this.txtPartID);
            this.pnlAddNewRecord.Controls.Add(this.txtQty);
            this.pnlAddNewRecord.Controls.Add(this.txtSerial);
            this.pnlAddNewRecord.Controls.Add(this.txtItemCode);
            this.pnlAddNewRecord.Controls.Add(this.label6);
            this.pnlAddNewRecord.Controls.Add(this.label3);
            this.pnlAddNewRecord.Controls.Add(this.label5);
            this.pnlAddNewRecord.Controls.Add(this.lblSerialPnl);
            this.pnlAddNewRecord.Controls.Add(this.label4);
            this.pnlAddNewRecord.Controls.Add(this.label2);
            this.pnlAddNewRecord.Controls.Add(this.btnHide);
            this.pnlAddNewRecord.Controls.Add(this.label1);
            this.pnlAddNewRecord.Location = new System.Drawing.Point(270, 101);
            this.pnlAddNewRecord.Name = "pnlAddNewRecord";
            this.pnlAddNewRecord.Size = new System.Drawing.Size(361, 232);
            this.pnlAddNewRecord.TabIndex = 1;
            // 
            // btnVouNo
            // 
            this.btnVouNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVouNo.BackgroundImage")));
            this.btnVouNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVouNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVouNo.ForeColor = System.Drawing.Color.White;
            this.btnVouNo.Location = new System.Drawing.Point(318, 34);
            this.btnVouNo.Name = "btnVouNo";
            this.btnVouNo.Size = new System.Drawing.Size(17, 20);
            this.btnVouNo.TabIndex = 258;
            this.btnVouNo.UseVisualStyleBackColor = true;
            this.btnVouNo.Click += new System.EventHandler(this.btnVouNo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(232, 190);
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
            this.btnAddItem.Location = new System.Drawing.Point(136, 190);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(90, 29);
            this.btnAddItem.TabIndex = 6;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // txtCaseID
            // 
            this.txtCaseID.Location = new System.Drawing.Point(98, 164);
            this.txtCaseID.Name = "txtCaseID";
            this.txtCaseID.Size = new System.Drawing.Size(238, 20);
            this.txtCaseID.TabIndex = 5;
            this.txtCaseID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCaseID_KeyDown);
            // 
            // txtOEM
            // 
            this.txtOEM.Location = new System.Drawing.Point(98, 138);
            this.txtOEM.Name = "txtOEM";
            this.txtOEM.Size = new System.Drawing.Size(238, 20);
            this.txtOEM.TabIndex = 4;
            this.txtOEM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOEM_KeyDown);
            // 
            // txtPartID
            // 
            this.txtPartID.Location = new System.Drawing.Point(98, 112);
            this.txtPartID.Name = "txtPartID";
            this.txtPartID.Size = new System.Drawing.Size(238, 20);
            this.txtPartID.TabIndex = 3;
            this.txtPartID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartID_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(98, 86);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(238, 20);
            this.txtQty.TabIndex = 2;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(98, 60);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(238, 20);
            this.txtSerial.TabIndex = 1;
            this.txtSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerial_KeyDown);
            // 
            // txtItemCode
            // 
            this.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemCode.Location = new System.Drawing.Point(98, 34);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(214, 20);
            this.txtItemCode.TabIndex = 0;
            this.txtItemCode.DoubleClick += new System.EventHandler(this.txtItemCode_DoubleClick);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Leave += new System.EventHandler(this.txtItemCode_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 255;
            this.label6.Text = "Case ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 255;
            this.label3.Text = "Qty";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 255;
            this.label5.Text = "OEM";
            // 
            // lblSerialPnl
            // 
            this.lblSerialPnl.AutoSize = true;
            this.lblSerialPnl.Location = new System.Drawing.Point(17, 63);
            this.lblSerialPnl.Name = "lblSerialPnl";
            this.lblSerialPnl.Size = new System.Drawing.Size(33, 13);
            this.lblSerialPnl.TabIndex = 255;
            this.lblSerialPnl.Text = "Serial";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 255;
            this.label4.Text = "Part ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 255;
            this.label2.Text = "Item";
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.panelcloseicon;
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.ForeColor = System.Drawing.Color.Lavender;
            this.btnHide.Location = new System.Drawing.Point(340, 0);
            this.btnHide.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(21, 21);
            this.btnHide.TabIndex = 8;
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Add New Record";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(10, 30);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chkSelectAll.TabIndex = 284;
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
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
            this.select,
            this.swd_seq_no,
            this.swd_line,
            this.swd_doc_no,
            this.swd_suppitmcd,
            this.swd_itmcd,
            this.swd_itm_stus,
            this.swd_itm_stusText,
            this.swd_ser1,
            this.swd_othdocno,
            this.swd_qty});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.Location = new System.Drawing.Point(0, 26);
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
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvItems.Size = new System.Drawing.Size(756, 430);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvItems_CurrentCellDirtyStateChanged);
            // 
            // select
            // 
            this.select.DataPropertyName = "select";
            this.select.HeaderText = "  ";
            this.select.Name = "select";
            this.select.Width = 30;
            // 
            // swd_seq_no
            // 
            this.swd_seq_no.DataPropertyName = "swd_seq_no";
            this.swd_seq_no.HeaderText = "SEQ";
            this.swd_seq_no.Name = "swd_seq_no";
            this.swd_seq_no.ReadOnly = true;
            this.swd_seq_no.Visible = false;
            this.swd_seq_no.Width = 30;
            // 
            // swd_line
            // 
            this.swd_line.DataPropertyName = "swd_line";
            this.swd_line.HeaderText = "Line Num";
            this.swd_line.Name = "swd_line";
            this.swd_line.ReadOnly = true;
            this.swd_line.Visible = false;
            // 
            // swd_doc_no
            // 
            this.swd_doc_no.DataPropertyName = "swd_doc_no";
            this.swd_doc_no.HeaderText = "Doc Num";
            this.swd_doc_no.Name = "swd_doc_no";
            this.swd_doc_no.ReadOnly = true;
            this.swd_doc_no.Visible = false;
            // 
            // swd_suppitmcd
            // 
            this.swd_suppitmcd.DataPropertyName = "swd_suppitmcd";
            this.swd_suppitmcd.HeaderText = "Supplier";
            this.swd_suppitmcd.Name = "swd_suppitmcd";
            this.swd_suppitmcd.ReadOnly = true;
            this.swd_suppitmcd.Visible = false;
            // 
            // swd_itmcd
            // 
            this.swd_itmcd.DataPropertyName = "swd_itmcd";
            this.swd_itmcd.HeaderText = "Item";
            this.swd_itmcd.Name = "swd_itmcd";
            this.swd_itmcd.ReadOnly = true;
            this.swd_itmcd.Width = 150;
            // 
            // swd_itm_stus
            // 
            this.swd_itm_stus.DataPropertyName = "swd_itm_stus";
            this.swd_itm_stus.HeaderText = "ItemStatus";
            this.swd_itm_stus.Name = "swd_itm_stus";
            this.swd_itm_stus.ReadOnly = true;
            this.swd_itm_stus.Visible = false;
            // 
            // swd_itm_stusText
            // 
            this.swd_itm_stusText.DataPropertyName = "swd_itm_stusText";
            this.swd_itm_stusText.HeaderText = "Item Status";
            this.swd_itm_stusText.Name = "swd_itm_stusText";
            this.swd_itm_stusText.ReadOnly = true;
            this.swd_itm_stusText.Width = 130;
            // 
            // swd_ser1
            // 
            this.swd_ser1.DataPropertyName = "swd_ser1";
            this.swd_ser1.HeaderText = "Serial";
            this.swd_ser1.Name = "swd_ser1";
            this.swd_ser1.ReadOnly = true;
            this.swd_ser1.Width = 130;
            // 
            // swd_othdocno
            // 
            this.swd_othdocno.DataPropertyName = "swd_othdocno";
            this.swd_othdocno.HeaderText = "Other Doc";
            this.swd_othdocno.Name = "swd_othdocno";
            this.swd_othdocno.ReadOnly = true;
            this.swd_othdocno.Visible = false;
            // 
            // swd_qty
            // 
            this.swd_qty.DataPropertyName = "swd_qty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.swd_qty.DefaultCellStyle = dataGridViewCellStyle3;
            this.swd_qty.HeaderText = "Qty";
            this.swd_qty.Name = "swd_qty";
            this.swd_qty.ReadOnly = true;
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
            this.btnSave.Text = "Receive";
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
            // button1
            // 
            this.button1.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(6, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add New Part";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // ServiceWIP_warrClmReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(759, 459);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnCloseFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceWIP_warrClmReceive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Service WIP Warranty Claim Receive";
            this.Load += new System.EventHandler(this.ServiceWIP_warrClmReceive_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlAddNewRecord.ResumeLayout(false);
            this.pnlAddNewRecord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnView;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlAddNewRecord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lblSerialPnl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCaseID;
        private System.Windows.Forms.TextBox txtOEM;
        private System.Windows.Forms.TextBox txtPartID;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnVouNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_seq_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_line;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_doc_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_suppitmcd;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_itmcd;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_itm_stus;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_itm_stusText;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_ser1;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_othdocno;
        private System.Windows.Forms.DataGridViewTextBoxColumn swd_qty;
        private System.Windows.Forms.Button btnCloseFrom;
    }
}