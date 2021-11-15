namespace FF.WindowsERPClient.Inventory
{
    partial class ItemComponentSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemComponentSetup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmsInvoiceItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsInvoiceItem_Description = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_UnitAmt = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_DisRate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_DisAmt = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_TaxAmt = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_Book = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsInvoiceItem_Level = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.cmsMultiItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsMuItem_Description = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMuItem_Brand = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMuItem_Model = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblHead = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvRec = new System.Windows.Forms.DataGridView();
            this.MCC_CAT1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCC_CAT2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCC_CAT3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCC_ITM_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCC_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCC_SUPP_WARR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCC_ISSER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MCC_ACT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkWar = new System.Windows.Forms.CheckBox();
            this.chkMan = new System.Windows.Forms.CheckBox();
            this.chkAct = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.btn_Srch_Cat1 = new System.Windows.Forms.Button();
            this.btn_Srch_Cat2 = new System.Windows.Forms.Button();
            this.btn_Srch_Cat3 = new System.Windows.Forms.Button();
            this.btn_Srch_Itm = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.txtIcat3 = new System.Windows.Forms.TextBox();
            this.txtIcat2 = new System.Windows.Forms.TextBox();
            this.txtIcat1 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lbl_comm_wdr = new System.Windows.Forms.TextBox();
            this.lbl_diff = new System.Windows.Forms.TextBox();
            this.lbl_TotRem = new System.Windows.Forms.TextBox();
            this.lbl_CIH = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cmsInvoiceItem.SuspendLayout();
            this.cmsMultiItem.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRec)).BeginInit();
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
            this.btnClose.Text = "Close";
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
            // lblHead
            // 
            this.lblHead.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblHead.ForeColor = System.Drawing.Color.Transparent;
            this.lblHead.Location = new System.Drawing.Point(6, 6);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(696, 14);
            this.lblHead.TabIndex = 13;
            this.lblHead.Text = "Item Criteria";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnl2);
            this.pnlMain.Controls.Add(this.lblHead);
            this.pnlMain.Location = new System.Drawing.Point(0, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(709, 510);
            this.pnlMain.TabIndex = 17;
            // 
            // pnl2
            // 
            this.pnl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl2.Controls.Add(this.label2);
            this.pnl2.Controls.Add(this.btnClear);
            this.pnl2.Controls.Add(this.btnSave);
            this.pnl2.Controls.Add(this.gvRec);
            this.pnl2.Controls.Add(this.chkWar);
            this.pnl2.Controls.Add(this.chkMan);
            this.pnl2.Controls.Add(this.chkAct);
            this.pnl2.Controls.Add(this.label1);
            this.pnl2.Controls.Add(this.txtQty);
            this.pnl2.Controls.Add(this.btn_Srch_Cat1);
            this.pnl2.Controls.Add(this.btn_Srch_Cat2);
            this.pnl2.Controls.Add(this.btn_Srch_Cat3);
            this.pnl2.Controls.Add(this.btn_Srch_Itm);
            this.pnl2.Controls.Add(this.label26);
            this.pnl2.Controls.Add(this.label27);
            this.pnl2.Controls.Add(this.txtItemCode);
            this.pnl2.Controls.Add(this.txtIcat3);
            this.pnl2.Controls.Add(this.txtIcat2);
            this.pnl2.Controls.Add(this.txtIcat1);
            this.pnl2.Controls.Add(this.label28);
            this.pnl2.Controls.Add(this.label29);
            this.pnl2.Controls.Add(this.lbl_comm_wdr);
            this.pnl2.Controls.Add(this.lbl_diff);
            this.pnl2.Controls.Add(this.lbl_TotRem);
            this.pnl2.Controls.Add(this.lbl_CIH);
            this.pnl2.Controls.Add(this.label22);
            this.pnl2.Controls.Add(this.label21);
            this.pnl2.Controls.Add(this.label20);
            this.pnl2.Controls.Add(this.label19);
            this.pnl2.Location = new System.Drawing.Point(7, 20);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(695, 487);
            this.pnl2.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.MidnightBlue;
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(671, 14);
            this.label2.TabIndex = 570;
            this.label2.Text = "Item Component Set up Details";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.NavajoWhite;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClear.Location = new System.Drawing.Point(599, 182);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(83, 23);
            this.btnClear.TabIndex = 569;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.NavajoWhite;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(512, 182);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 23);
            this.btnSave.TabIndex = 568;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvRec
            // 
            this.gvRec.AllowUserToAddRows = false;
            this.gvRec.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.gvRec.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvRec.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvRec.ColumnHeadersHeight = 20;
            this.gvRec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MCC_CAT1,
            this.MCC_CAT2,
            this.MCC_CAT3,
            this.MCC_ITM_CD,
            this.MCC_QTY,
            this.MCC_SUPP_WARR,
            this.MCC_ISSER,
            this.MCC_ACT});
            this.gvRec.Location = new System.Drawing.Point(11, 230);
            this.gvRec.Name = "gvRec";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRec.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvRec.RowHeadersVisible = false;
            this.gvRec.RowTemplate.Height = 16;
            this.gvRec.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvRec.Size = new System.Drawing.Size(673, 251);
            this.gvRec.TabIndex = 146;
            this.gvRec.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRec_CellDoubleClick);
            // 
            // MCC_CAT1
            // 
            this.MCC_CAT1.DataPropertyName = "MCC_CAT1";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MCC_CAT1.DefaultCellStyle = dataGridViewCellStyle3;
            this.MCC_CAT1.HeaderText = "CATEGORY 1";
            this.MCC_CAT1.Name = "MCC_CAT1";
            this.MCC_CAT1.ReadOnly = true;
            this.MCC_CAT1.Width = 80;
            // 
            // MCC_CAT2
            // 
            this.MCC_CAT2.DataPropertyName = "MCC_CAT2";
            this.MCC_CAT2.HeaderText = "CATEGORY 2";
            this.MCC_CAT2.Name = "MCC_CAT2";
            this.MCC_CAT2.ReadOnly = true;
            this.MCC_CAT2.Width = 80;
            // 
            // MCC_CAT3
            // 
            this.MCC_CAT3.DataPropertyName = "MCC_CAT3";
            this.MCC_CAT3.HeaderText = "CATEGORY 3";
            this.MCC_CAT3.Name = "MCC_CAT3";
            this.MCC_CAT3.ReadOnly = true;
            this.MCC_CAT3.Width = 80;
            // 
            // MCC_ITM_CD
            // 
            this.MCC_ITM_CD.DataPropertyName = "MCC_ITM_CD";
            this.MCC_ITM_CD.HeaderText = "ITEM CODE";
            this.MCC_ITM_CD.Name = "MCC_ITM_CD";
            this.MCC_ITM_CD.ReadOnly = true;
            // 
            // MCC_QTY
            // 
            this.MCC_QTY.DataPropertyName = "MCC_QTY";
            this.MCC_QTY.HeaderText = "QUANTITY";
            this.MCC_QTY.Name = "MCC_QTY";
            this.MCC_QTY.ReadOnly = true;
            this.MCC_QTY.Width = 60;
            // 
            // MCC_SUPP_WARR
            // 
            this.MCC_SUPP_WARR.DataPropertyName = "MCC_SUPP_WARR";
            this.MCC_SUPP_WARR.HeaderText = "ALLOW SUP. WAR.";
            this.MCC_SUPP_WARR.Name = "MCC_SUPP_WARR";
            this.MCC_SUPP_WARR.ReadOnly = true;
            // 
            // MCC_ISSER
            // 
            this.MCC_ISSER.DataPropertyName = "MCC_ISSER";
            this.MCC_ISSER.HeaderText = "IS SERIAL MAN.";
            this.MCC_ISSER.Name = "MCC_ISSER";
            this.MCC_ISSER.ReadOnly = true;
            // 
            // MCC_ACT
            // 
            this.MCC_ACT.DataPropertyName = "MCC_ACT";
            this.MCC_ACT.HeaderText = "ACTIVE";
            this.MCC_ACT.Name = "MCC_ACT";
            this.MCC_ACT.ReadOnly = true;
            this.MCC_ACT.Width = 50;
            // 
            // chkWar
            // 
            this.chkWar.AutoSize = true;
            this.chkWar.Checked = true;
            this.chkWar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWar.Location = new System.Drawing.Point(111, 142);
            this.chkWar.Name = "chkWar";
            this.chkWar.Size = new System.Drawing.Size(141, 17);
            this.chkWar.TabIndex = 145;
            this.chkWar.Text = "Allow Supplier Warranty";
            this.chkWar.UseVisualStyleBackColor = true;
            // 
            // chkMan
            // 
            this.chkMan.AutoSize = true;
            this.chkMan.Checked = true;
            this.chkMan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMan.Location = new System.Drawing.Point(111, 165);
            this.chkMan.Name = "chkMan";
            this.chkMan.Size = new System.Drawing.Size(159, 17);
            this.chkMan.TabIndex = 144;
            this.chkMan.Text = "Is Mandatory Serial Number";
            this.chkMan.UseVisualStyleBackColor = true;
            // 
            // chkAct
            // 
            this.chkAct.AutoSize = true;
            this.chkAct.Checked = true;
            this.chkAct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAct.Location = new System.Drawing.Point(111, 188);
            this.chkAct.Name = "chkAct";
            this.chkAct.Size = new System.Drawing.Size(56, 17);
            this.chkAct.TabIndex = 143;
            this.chkAct.Text = "Active";
            this.chkAct.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 140;
            this.label1.Text = "Quantity";
            // 
            // txtQty
            // 
            this.txtQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(111, 117);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(82, 20);
            this.txtQty.TabIndex = 139;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // btn_Srch_Cat1
            // 
            this.btn_Srch_Cat1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Srch_Cat1.BackgroundImage")));
            this.btn_Srch_Cat1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Srch_Cat1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Srch_Cat1.ForeColor = System.Drawing.Color.White;
            this.btn_Srch_Cat1.Location = new System.Drawing.Point(334, 12);
            this.btn_Srch_Cat1.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_Srch_Cat1.Name = "btn_Srch_Cat1";
            this.btn_Srch_Cat1.Size = new System.Drawing.Size(20, 20);
            this.btn_Srch_Cat1.TabIndex = 138;
            this.btn_Srch_Cat1.UseVisualStyleBackColor = true;
            this.btn_Srch_Cat1.Click += new System.EventHandler(this.btn_Srch_Cat1_Click);
            // 
            // btn_Srch_Cat2
            // 
            this.btn_Srch_Cat2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Srch_Cat2.BackgroundImage")));
            this.btn_Srch_Cat2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Srch_Cat2.Enabled = false;
            this.btn_Srch_Cat2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Srch_Cat2.ForeColor = System.Drawing.Color.White;
            this.btn_Srch_Cat2.Location = new System.Drawing.Point(334, 36);
            this.btn_Srch_Cat2.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_Srch_Cat2.Name = "btn_Srch_Cat2";
            this.btn_Srch_Cat2.Size = new System.Drawing.Size(20, 20);
            this.btn_Srch_Cat2.TabIndex = 137;
            this.btn_Srch_Cat2.UseVisualStyleBackColor = true;
            this.btn_Srch_Cat2.Click += new System.EventHandler(this.btn_Srch_Cat2_Click);
            // 
            // btn_Srch_Cat3
            // 
            this.btn_Srch_Cat3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Srch_Cat3.BackgroundImage")));
            this.btn_Srch_Cat3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Srch_Cat3.Enabled = false;
            this.btn_Srch_Cat3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Srch_Cat3.ForeColor = System.Drawing.Color.White;
            this.btn_Srch_Cat3.Location = new System.Drawing.Point(334, 58);
            this.btn_Srch_Cat3.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_Srch_Cat3.Name = "btn_Srch_Cat3";
            this.btn_Srch_Cat3.Size = new System.Drawing.Size(20, 20);
            this.btn_Srch_Cat3.TabIndex = 136;
            this.btn_Srch_Cat3.UseVisualStyleBackColor = true;
            this.btn_Srch_Cat3.Click += new System.EventHandler(this.btn_Srch_Cat3_Click);
            // 
            // btn_Srch_Itm
            // 
            this.btn_Srch_Itm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Srch_Itm.BackgroundImage")));
            this.btn_Srch_Itm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Srch_Itm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Srch_Itm.ForeColor = System.Drawing.Color.White;
            this.btn_Srch_Itm.Location = new System.Drawing.Point(334, 81);
            this.btn_Srch_Itm.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_Srch_Itm.Name = "btn_Srch_Itm";
            this.btn_Srch_Itm.Size = new System.Drawing.Size(20, 20);
            this.btn_Srch_Itm.TabIndex = 135;
            this.btn_Srch_Itm.UseVisualStyleBackColor = true;
            this.btn_Srch_Itm.Click += new System.EventHandler(this.btn_Srch_Itm_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(12, 85);
            this.label26.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(57, 13);
            this.label26.TabIndex = 134;
            this.label26.Text = "Item Code";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 62);
            this.label27.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(86, 13);
            this.label27.TabIndex = 133;
            this.label27.Text = "Item Category 3";
            // 
            // txtItemCode
            // 
            this.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemCode.Location = new System.Drawing.Point(111, 81);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(212, 20);
            this.txtItemCode.TabIndex = 132;
            this.txtItemCode.DoubleClick += new System.EventHandler(this.txtItemCode_DoubleClick);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            // 
            // txtIcat3
            // 
            this.txtIcat3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIcat3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIcat3.Location = new System.Drawing.Point(111, 58);
            this.txtIcat3.Name = "txtIcat3";
            this.txtIcat3.Size = new System.Drawing.Size(212, 20);
            this.txtIcat3.TabIndex = 131;
            this.txtIcat3.DoubleClick += new System.EventHandler(this.txtIcat3_DoubleClick);
            this.txtIcat3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIcat3_KeyDown);
            this.txtIcat3.Leave += new System.EventHandler(this.txtIcat3_Leave);
            // 
            // txtIcat2
            // 
            this.txtIcat2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIcat2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIcat2.Location = new System.Drawing.Point(111, 36);
            this.txtIcat2.Name = "txtIcat2";
            this.txtIcat2.Size = new System.Drawing.Size(212, 20);
            this.txtIcat2.TabIndex = 130;
            this.txtIcat2.DoubleClick += new System.EventHandler(this.txtIcat2_DoubleClick);
            this.txtIcat2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIcat2_KeyDown);
            this.txtIcat2.Leave += new System.EventHandler(this.txtIcat2_Leave);
            // 
            // txtIcat1
            // 
            this.txtIcat1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIcat1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIcat1.Location = new System.Drawing.Point(111, 12);
            this.txtIcat1.Name = "txtIcat1";
            this.txtIcat1.Size = new System.Drawing.Size(212, 20);
            this.txtIcat1.TabIndex = 129;
            this.txtIcat1.DoubleClick += new System.EventHandler(this.txtIcat1_DoubleClick);
            this.txtIcat1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIcat1_KeyDown);
            this.txtIcat1.Leave += new System.EventHandler(this.txtIcat1_Leave);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(12, 40);
            this.label28.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(86, 13);
            this.label28.TabIndex = 128;
            this.label28.Text = "Item Category 2";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(12, 16);
            this.label29.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(86, 13);
            this.label29.TabIndex = 127;
            this.label29.Text = "Item Category 1";
            // 
            // lbl_comm_wdr
            // 
            this.lbl_comm_wdr.BackColor = System.Drawing.SystemColors.Info;
            this.lbl_comm_wdr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_comm_wdr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lbl_comm_wdr.Enabled = false;
            this.lbl_comm_wdr.Location = new System.Drawing.Point(824, 411);
            this.lbl_comm_wdr.Name = "lbl_comm_wdr";
            this.lbl_comm_wdr.Size = new System.Drawing.Size(149, 21);
            this.lbl_comm_wdr.TabIndex = 126;
            this.lbl_comm_wdr.Text = "0.00";
            this.lbl_comm_wdr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_diff
            // 
            this.lbl_diff.BackColor = System.Drawing.SystemColors.Info;
            this.lbl_diff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_diff.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lbl_diff.Enabled = false;
            this.lbl_diff.Location = new System.Drawing.Point(824, 371);
            this.lbl_diff.Name = "lbl_diff";
            this.lbl_diff.Size = new System.Drawing.Size(149, 21);
            this.lbl_diff.TabIndex = 125;
            this.lbl_diff.Text = "0.00";
            this.lbl_diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_TotRem
            // 
            this.lbl_TotRem.BackColor = System.Drawing.SystemColors.Info;
            this.lbl_TotRem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TotRem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lbl_TotRem.Enabled = false;
            this.lbl_TotRem.Location = new System.Drawing.Point(824, 331);
            this.lbl_TotRem.Name = "lbl_TotRem";
            this.lbl_TotRem.Size = new System.Drawing.Size(149, 21);
            this.lbl_TotRem.TabIndex = 124;
            this.lbl_TotRem.Text = "0.00";
            this.lbl_TotRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_CIH
            // 
            this.lbl_CIH.BackColor = System.Drawing.SystemColors.Info;
            this.lbl_CIH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CIH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lbl_CIH.Enabled = false;
            this.lbl_CIH.Location = new System.Drawing.Point(824, 291);
            this.lbl_CIH.Name = "lbl_CIH";
            this.lbl_CIH.Size = new System.Drawing.Size(149, 21);
            this.lbl_CIH.TabIndex = 123;
            this.lbl_CIH.Text = "0.00";
            this.lbl_CIH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(901, 275);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(72, 13);
            this.label22.TabIndex = 119;
            this.label22.Text = "Cash In Hand";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(889, 315);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(84, 13);
            this.label21.TabIndex = 118;
            this.label21.Text = "Total Remitance";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(916, 355);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(57, 13);
            this.label20.TabIndex = 117;
            this.label20.Text = "Difference";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(856, 395);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(117, 13);
            this.label19.TabIndex = 116;
            this.label19.Text = "Commission Withdrawn";
            // 
            // ItemComponentSetup
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(708, 514);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1016, 666);
            this.Name = "ItemComponentSetup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Item Component Setup";
            this.cmsInvoiceItem.ResumeLayout(false);
            this.cmsMultiItem.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnl2.ResumeLayout(false);
            this.pnl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRec)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnClose;
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
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox lbl_comm_wdr;
        private System.Windows.Forms.TextBox lbl_diff;
        private System.Windows.Forms.TextBox lbl_TotRem;
        private System.Windows.Forms.TextBox lbl_CIH;
        private System.Windows.Forms.Button btn_Srch_Cat1;
        private System.Windows.Forms.Button btn_Srch_Cat2;
        private System.Windows.Forms.Button btn_Srch_Cat3;
        private System.Windows.Forms.Button btn_Srch_Itm;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TextBox txtIcat3;
        private System.Windows.Forms.TextBox txtIcat2;
        private System.Windows.Forms.TextBox txtIcat1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.CheckBox chkWar;
        private System.Windows.Forms.CheckBox chkMan;
        private System.Windows.Forms.CheckBox chkAct;
        private System.Windows.Forms.DataGridView gvRec;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCC_CAT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCC_CAT2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCC_CAT3;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCC_ITM_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCC_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCC_SUPP_WARR;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCC_ISSER;
        private System.Windows.Forms.DataGridViewTextBoxColumn MCC_ACT;
 
     
    }
}
