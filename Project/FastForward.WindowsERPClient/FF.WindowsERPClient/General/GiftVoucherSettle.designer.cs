namespace FF.WindowsERPClient.General
{

    partial class GiftVoucherSettle
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiftVoucherSettle));
            this.cmsInvoiceItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsInvoiceItem_Description = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_UnitAmt = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_DisRate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_DisAmt = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_TaxAmt = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_Book = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_Level = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.gvMultipleItem = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.MuItm_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MuItm_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MuItm_Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRef = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHead = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.lblCd = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lblBook = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblAdd1 = new System.Windows.Forms.Label();
            this.lblCusName = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblCusCode = new System.Windows.Forms.Label();
            this.btnGiftVoucher = new System.Windows.Forms.Button();
            this.txtGiftVoucher = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.lblBackDateInfor = new System.Windows.Forms.Label();
            this.cmsMultiItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsMuItem_Description = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMuItem_Brand = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMuItem_Model = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmsInvoiceItem.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultipleItem)).BeginInit();
            this.cmsMultiItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsInvoiceItem
            // 
            this.cmsInvoiceItem.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmsInvoiceItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsInvoiceItem_Description,
            this.cmsInvoiceItem_UnitAmt,
            this.cmsInvoiceItem_DisRate,
            this.cmsInvoiceItem_DisAmt,
            this.cmsInvoiceItem_TaxAmt,
            this.cmsInvoiceItem_Book,
            this.cmsInvoiceItem_Level});
            this.cmsInvoiceItem.Name = "cmsInvoiceItem";
            this.cmsInvoiceItem.Size = new System.Drawing.Size(135, 158);
            // 
            // cmsInvoiceItem_Description
            // 
            this.cmsInvoiceItem_Description.Checked = true;
            this.cmsInvoiceItem_Description.CheckOnClick = true;
            this.cmsInvoiceItem_Description.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsInvoiceItem_Description.Name = "cmsInvoiceItem_Description";
            this.cmsInvoiceItem_Description.Size = new System.Drawing.Size(134, 22);
            this.cmsInvoiceItem_Description.Text = "Description";
            // 
            // cmsInvoiceItem_UnitAmt
            // 
            this.cmsInvoiceItem_UnitAmt.Checked = true;
            this.cmsInvoiceItem_UnitAmt.CheckOnClick = true;
            this.cmsInvoiceItem_UnitAmt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsInvoiceItem_UnitAmt.Name = "cmsInvoiceItem_UnitAmt";
            this.cmsInvoiceItem_UnitAmt.Size = new System.Drawing.Size(134, 22);
            this.cmsInvoiceItem_UnitAmt.Text = "Unit Amt";
            // 
            // cmsInvoiceItem_DisRate
            // 
            this.cmsInvoiceItem_DisRate.Checked = true;
            this.cmsInvoiceItem_DisRate.CheckOnClick = true;
            this.cmsInvoiceItem_DisRate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsInvoiceItem_DisRate.Name = "cmsInvoiceItem_DisRate";
            this.cmsInvoiceItem_DisRate.Size = new System.Drawing.Size(134, 22);
            this.cmsInvoiceItem_DisRate.Text = "Dis. Rate";
            // 
            // cmsInvoiceItem_DisAmt
            // 
            this.cmsInvoiceItem_DisAmt.Checked = true;
            this.cmsInvoiceItem_DisAmt.CheckOnClick = true;
            this.cmsInvoiceItem_DisAmt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsInvoiceItem_DisAmt.Name = "cmsInvoiceItem_DisAmt";
            this.cmsInvoiceItem_DisAmt.Size = new System.Drawing.Size(134, 22);
            this.cmsInvoiceItem_DisAmt.Text = "Dis. Amt";
            // 
            // cmsInvoiceItem_TaxAmt
            // 
            this.cmsInvoiceItem_TaxAmt.Checked = true;
            this.cmsInvoiceItem_TaxAmt.CheckOnClick = true;
            this.cmsInvoiceItem_TaxAmt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsInvoiceItem_TaxAmt.Name = "cmsInvoiceItem_TaxAmt";
            this.cmsInvoiceItem_TaxAmt.Size = new System.Drawing.Size(134, 22);
            this.cmsInvoiceItem_TaxAmt.Text = "Tax Amt";
            // 
            // cmsInvoiceItem_Book
            // 
            this.cmsInvoiceItem_Book.Checked = true;
            this.cmsInvoiceItem_Book.CheckOnClick = true;
            this.cmsInvoiceItem_Book.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsInvoiceItem_Book.Name = "cmsInvoiceItem_Book";
            this.cmsInvoiceItem_Book.Size = new System.Drawing.Size(134, 22);
            this.cmsInvoiceItem_Book.Text = "Book";
            // 
            // cmsInvoiceItem_Level
            // 
            this.cmsInvoiceItem_Level.Checked = true;
            this.cmsInvoiceItem_Level.CheckOnClick = true;
            this.cmsInvoiceItem_Level.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsInvoiceItem_Level.Name = "cmsInvoiceItem_Level";
            this.cmsInvoiceItem_Level.Size = new System.Drawing.Size(134, 22);
            this.cmsInvoiceItem_Level.Text = "Level";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.toolStripSeparator2,
            this.btnUpd,
            this.toolStripSeparator4});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(1010, 25);
            this.toolStrip1.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = false;
            this.btnClose.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(2);
            this.btnClose.Size = new System.Drawing.Size(60, 22);
            this.btnClose.Text = "Clear";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnUpd
            // 
            this.btnUpd.AutoSize = false;
            this.btnUpd.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnUpd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnUpd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpd.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnUpd.Name = "btnUpd";
            this.btnUpd.Padding = new System.Windows.Forms.Padding(2);
            this.btnUpd.Size = new System.Drawing.Size(60, 22);
            this.btnUpd.Text = "Save";
            this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gvMultipleItem);
            this.pnlMain.Controls.Add(this.txtDate);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.txtRef);
            this.pnlMain.Controls.Add(this.label40);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblHead);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.textBoxAmount);
            this.pnlMain.Controls.Add(this.lblCd);
            this.pnlMain.Controls.Add(this.label46);
            this.pnlMain.Controls.Add(this.lblPrefix);
            this.pnlMain.Controls.Add(this.label44);
            this.pnlMain.Controls.Add(this.lblBook);
            this.pnlMain.Controls.Add(this.label42);
            this.pnlMain.Controls.Add(this.lblMobile);
            this.pnlMain.Controls.Add(this.label38);
            this.pnlMain.Controls.Add(this.label36);
            this.pnlMain.Controls.Add(this.lblAdd1);
            this.pnlMain.Controls.Add(this.lblCusName);
            this.pnlMain.Controls.Add(this.label34);
            this.pnlMain.Controls.Add(this.label35);
            this.pnlMain.Controls.Add(this.lblCusCode);
            this.pnlMain.Controls.Add(this.btnGiftVoucher);
            this.pnlMain.Controls.Add(this.txtGiftVoucher);
            this.pnlMain.Controls.Add(this.label37);
            this.pnlMain.Controls.Add(this.lblBackDateInfor);
            this.pnlMain.Location = new System.Drawing.Point(0, 27);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1000, 414);
            this.pnlMain.TabIndex = 17;
            // 
            // gvMultipleItem
            // 
            this.gvMultipleItem.AllowUserToAddRows = false;
            this.gvMultipleItem.AllowUserToDeleteRows = false;
            this.gvMultipleItem.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvMultipleItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMultipleItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn3,
            this.MuItm_Item,
            this.MuItm_Description,
            this.Column12,
            this.MuItm_Model,
            this.Column11,
            this.Column14,
            this.Column15});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMultipleItem.DefaultCellStyle = dataGridViewCellStyle11;
            this.gvMultipleItem.GridColor = System.Drawing.Color.White;
            this.gvMultipleItem.Location = new System.Drawing.Point(443, 25);
            this.gvMultipleItem.Name = "gvMultipleItem";
            this.gvMultipleItem.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMultipleItem.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.gvMultipleItem.RowHeadersVisible = false;
            this.gvMultipleItem.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMultipleItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvMultipleItem.Size = new System.Drawing.Size(424, 125);
            this.gvMultipleItem.TabIndex = 124;
            this.gvMultipleItem.Visible = false;
            this.gvMultipleItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMultipleItem_CellContentClick);
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.HeaderText = "";
            this.dataGridViewImageColumn3.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.ReadOnly = true;
            this.dataGridViewImageColumn3.Width = 20;
            // 
            // MuItm_Item
            // 
            this.MuItm_Item.DataPropertyName = "GVP_BOOK";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            this.MuItm_Item.DefaultCellStyle = dataGridViewCellStyle7;
            this.MuItm_Item.HeaderText = "Book";
            this.MuItm_Item.Name = "MuItm_Item";
            this.MuItm_Item.ReadOnly = true;
            this.MuItm_Item.Width = 50;
            // 
            // MuItm_Description
            // 
            this.MuItm_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MuItm_Description.DataPropertyName = "GVP_PAGE";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.MuItm_Description.DefaultCellStyle = dataGridViewCellStyle8;
            this.MuItm_Description.HeaderText = "Page";
            this.MuItm_Description.Name = "MuItm_Description";
            this.MuItm_Description.ReadOnly = true;
            this.MuItm_Description.Width = 56;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "GVP_BAL_AMT";
            this.Column12.HeaderText = "Amount";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 50;
            // 
            // MuItm_Model
            // 
            this.MuItm_Model.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MuItm_Model.DataPropertyName = "GVP_GV_CD";
            this.MuItm_Model.HeaderText = "Code";
            this.MuItm_Model.Name = "MuItm_Model";
            this.MuItm_Model.ReadOnly = true;
            this.MuItm_Model.Width = 57;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "gvp_gv_prefix";
            this.Column11.HeaderText = "Prefix";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 60;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "Gvp_valid_from";
            dataGridViewCellStyle9.Format = "d";
            dataGridViewCellStyle9.NullValue = null;
            this.Column14.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column14.HeaderText = "From";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 50;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "Gvp_valid_to";
            dataGridViewCellStyle10.Format = "d";
            dataGridViewCellStyle10.NullValue = null;
            this.Column15.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column15.HeaderText = "To";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 50;
            // 
            // txtDate
            // 
            this.txtDate.Checked = false;
            this.txtDate.CustomFormat = "dd/MMM/yyyy";
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDate.Location = new System.Drawing.Point(111, 348);
            this.txtDate.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(129, 21);
            this.txtDate.TabIndex = 122;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 352);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 123;
            this.label10.Text = "Settle Date";
            // 
            // txtRef
            // 
            this.txtRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRef.Location = new System.Drawing.Point(111, 320);
            this.txtRef.MaxLength = 50;
            this.txtRef.Name = "txtRef";
            this.txtRef.Size = new System.Drawing.Size(232, 21);
            this.txtRef.TabIndex = 121;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(16, 325);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(97, 13);
            this.label40.TabIndex = 120;
            this.label40.Text = "Reference Number";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MidnightBlue;
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(1, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(996, 14);
            this.label1.TabIndex = 119;
            this.label1.Text = "Gift Voucher Settlement Details";
            // 
            // lblHead
            // 
            this.lblHead.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblHead.ForeColor = System.Drawing.Color.Transparent;
            this.lblHead.Location = new System.Drawing.Point(2, 0);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(996, 14);
            this.lblHead.TabIndex = 118;
            this.lblHead.Text = "Gift Voucher Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 117;
            this.label2.Text = "Amount";
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAmount.Location = new System.Drawing.Point(111, 249);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.ReadOnly = true;
            this.textBoxAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxAmount.Size = new System.Drawing.Size(123, 21);
            this.textBoxAmount.TabIndex = 116;
            this.textBoxAmount.Text = "0.00";
            this.textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCd
            // 
            this.lblCd.ForeColor = System.Drawing.Color.Blue;
            this.lblCd.Location = new System.Drawing.Point(111, 217);
            this.lblCd.Name = "lblCd";
            this.lblCd.Size = new System.Drawing.Size(54, 14);
            this.lblCd.TabIndex = 115;
            this.lblCd.Text = "-";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(16, 191);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(35, 13);
            this.label46.TabIndex = 114;
            this.label46.Text = "Prefix";
            // 
            // lblPrefix
            // 
            this.lblPrefix.ForeColor = System.Drawing.Color.Blue;
            this.lblPrefix.Location = new System.Drawing.Point(111, 191);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(40, 13);
            this.lblPrefix.TabIndex = 113;
            this.lblPrefix.Text = "-";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(16, 218);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(32, 13);
            this.label44.TabIndex = 112;
            this.label44.Text = "Code";
            // 
            // lblBook
            // 
            this.lblBook.ForeColor = System.Drawing.Color.Blue;
            this.lblBook.Location = new System.Drawing.Point(111, 164);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(54, 13);
            this.lblBook.TabIndex = 111;
            this.lblBook.Text = "-";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(16, 164);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(30, 13);
            this.label42.TabIndex = 110;
            this.label42.Text = "Book";
            // 
            // lblMobile
            // 
            this.lblMobile.ForeColor = System.Drawing.Color.Blue;
            this.lblMobile.Location = new System.Drawing.Point(111, 137);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(123, 13);
            this.lblMobile.TabIndex = 108;
            this.lblMobile.Text = "-";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(16, 137);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(37, 13);
            this.label38.TabIndex = 107;
            this.label38.Text = "Mobile";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(16, 110);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(46, 13);
            this.label36.TabIndex = 106;
            this.label36.Text = "Address";
            // 
            // lblAdd1
            // 
            this.lblAdd1.ForeColor = System.Drawing.Color.Blue;
            this.lblAdd1.Location = new System.Drawing.Point(111, 110);
            this.lblAdd1.Name = "lblAdd1";
            this.lblAdd1.Size = new System.Drawing.Size(123, 13);
            this.lblAdd1.TabIndex = 105;
            this.lblAdd1.Text = "-";
            // 
            // lblCusName
            // 
            this.lblCusName.ForeColor = System.Drawing.Color.Blue;
            this.lblCusName.Location = new System.Drawing.Point(111, 80);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(123, 18);
            this.lblCusName.TabIndex = 104;
            this.lblCusName.Text = "-";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(16, 83);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(83, 13);
            this.label34.TabIndex = 103;
            this.label34.Text = "Customer Name";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(16, 56);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(81, 13);
            this.label35.TabIndex = 102;
            this.label35.Text = "Customer Code";
            // 
            // lblCusCode
            // 
            this.lblCusCode.ForeColor = System.Drawing.Color.Blue;
            this.lblCusCode.Location = new System.Drawing.Point(111, 56);
            this.lblCusCode.Name = "lblCusCode";
            this.lblCusCode.Size = new System.Drawing.Size(123, 13);
            this.lblCusCode.TabIndex = 101;
            this.lblCusCode.Text = "-";
            // 
            // btnGiftVoucher
            // 
            this.btnGiftVoucher.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.searchicon;
            this.btnGiftVoucher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGiftVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGiftVoucher.ForeColor = System.Drawing.Color.White;
            this.btnGiftVoucher.Location = new System.Drawing.Point(347, 24);
            this.btnGiftVoucher.Name = "btnGiftVoucher";
            this.btnGiftVoucher.Size = new System.Drawing.Size(20, 24);
            this.btnGiftVoucher.TabIndex = 100;
            this.btnGiftVoucher.UseVisualStyleBackColor = true;
            this.btnGiftVoucher.Click += new System.EventHandler(this.btnGiftVoucher_Click);
            // 
            // txtGiftVoucher
            // 
            this.txtGiftVoucher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGiftVoucher.Location = new System.Drawing.Point(111, 25);
            this.txtGiftVoucher.MaxLength = 10;
            this.txtGiftVoucher.Name = "txtGiftVoucher";
            this.txtGiftVoucher.Size = new System.Drawing.Size(232, 21);
            this.txtGiftVoucher.TabIndex = 99;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(16, 29);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(82, 13);
            this.label37.TabIndex = 98;
            this.label37.Text = "Gift Voucher No";
            // 
            // lblBackDateInfor
            // 
            this.lblBackDateInfor.AutoSize = true;
            this.lblBackDateInfor.BackColor = System.Drawing.Color.SteelBlue;
            this.lblBackDateInfor.ForeColor = System.Drawing.Color.White;
            this.lblBackDateInfor.Location = new System.Drawing.Point(756, 4);
            this.lblBackDateInfor.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblBackDateInfor.Name = "lblBackDateInfor";
            this.lblBackDateInfor.Size = new System.Drawing.Size(0, 13);
            this.lblBackDateInfor.TabIndex = 97;
            // 
            // cmsMultiItem
            // 
            this.cmsMultiItem.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmsMultiItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsMuItem_Description,
            this.cmsMuItem_Brand,
            this.cmsMuItem_Model});
            this.cmsMultiItem.Name = "cmsMultiItem";
            this.cmsMultiItem.Size = new System.Drawing.Size(135, 70);
            this.cmsMultiItem.Text = "Description";
            // 
            // cmsMuItem_Description
            // 
            this.cmsMuItem_Description.Checked = true;
            this.cmsMuItem_Description.CheckOnClick = true;
            this.cmsMuItem_Description.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsMuItem_Description.Name = "cmsMuItem_Description";
            this.cmsMuItem_Description.Size = new System.Drawing.Size(134, 22);
            this.cmsMuItem_Description.Text = "Description";
            // 
            // cmsMuItem_Brand
            // 
            this.cmsMuItem_Brand.Checked = true;
            this.cmsMuItem_Brand.CheckOnClick = true;
            this.cmsMuItem_Brand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsMuItem_Brand.Name = "cmsMuItem_Brand";
            this.cmsMuItem_Brand.Size = new System.Drawing.Size(134, 22);
            this.cmsMuItem_Brand.Text = "Brand";
            // 
            // cmsMuItem_Model
            // 
            this.cmsMuItem_Model.Checked = true;
            this.cmsMuItem_Model.CheckOnClick = true;
            this.cmsMuItem_Model.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmsMuItem_Model.Name = "cmsMuItem_Model";
            this.cmsMuItem_Model.Size = new System.Drawing.Size(134, 22);
            this.cmsMuItem_Model.Text = "Model";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.Width = 5;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.Width = 5;
            // 
            // GiftVoucherSettle
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1010, 638);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1016, 666);
            this.MinimumSize = new System.Drawing.Size(1016, 666);
            this.Name = "GiftVoucherSettle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Gift Voucher Settlement";
            this.cmsInvoiceItem.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultipleItem)).EndInit();
            this.cmsMultiItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnUpd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ContextMenuStrip cmsMultiItem;
        private System.Windows.Forms.ToolStripMenuItem cmsMuItem_Description;
        private System.Windows.Forms.ToolStripMenuItem cmsMuItem_Brand;
        private System.Windows.Forms.ToolStripMenuItem cmsMuItem_Model;
        // private UserControls.MultiColumnCombo multiColumnCombo1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.ContextMenuStrip cmsInvoiceItem;
        private System.Windows.Forms.ToolStripMenuItem cmsInvoiceItem_Description;
        private System.Windows.Forms.ToolStripMenuItem cmsInvoiceItem_UnitAmt;
        private System.Windows.Forms.ToolStripMenuItem cmsInvoiceItem_DisRate;
        private System.Windows.Forms.ToolStripMenuItem cmsInvoiceItem_DisAmt;
        private System.Windows.Forms.ToolStripMenuItem cmsInvoiceItem_TaxAmt;
        private System.Windows.Forms.ToolStripMenuItem cmsInvoiceItem_Book;
        private System.Windows.Forms.ToolStripMenuItem cmsInvoiceItem_Level;
        private System.Windows.Forms.Label lblBackDateInfor;
        private System.Windows.Forms.Label lblCd;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblBook;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblAdd1;
        private System.Windows.Forms.Label lblCusName;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lblCusCode;
        private System.Windows.Forms.Button btnGiftVoucher;
        private System.Windows.Forms.TextBox txtGiftVoucher;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.TextBox txtRef;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView gvMultipleItem;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn MuItm_Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn MuItm_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn MuItm_Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
 
     
    }
}
