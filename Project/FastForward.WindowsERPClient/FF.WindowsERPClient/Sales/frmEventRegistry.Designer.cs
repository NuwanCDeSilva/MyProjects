namespace FF.WindowsERPClient.Sales
{
    partial class frmEventRegistry
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtEventId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchEvent = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEvent = new System.Windows.Forms.Panel();
            this.txtEventDesciption = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEventType = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDeliveryDate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEventDate = new System.Windows.Forms.TextBox();
            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDeliAddress = new System.Windows.Forms.TextBox();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.txtCustCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.ItemSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoldQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PickQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddItem = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlEvent.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.pnlItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEventId
            // 
            this.txtEventId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEventId.Location = new System.Drawing.Point(88, 5);
            this.txtEventId.Name = "txtEventId";
            this.txtEventId.Size = new System.Drawing.Size(200, 21);
            this.txtEventId.TabIndex = 1;
            this.txtEventId.DoubleClick += new System.EventHandler(this.txtEventId_DoubleClick);
            this.txtEventId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEventId_KeyDown);
            this.txtEventId.Leave += new System.EventHandler(this.txtEventId_Leave);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Event ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearchEvent
            // 
            this.btnSearchEvent.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.specialsearch1icon;
            this.btnSearchEvent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchEvent.FlatAppearance.BorderSize = 0;
            this.btnSearchEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchEvent.Location = new System.Drawing.Point(294, 3);
            this.btnSearchEvent.Name = "btnSearchEvent";
            this.btnSearchEvent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSearchEvent.Size = new System.Drawing.Size(25, 25);
            this.btnSearchEvent.TabIndex = 3;
            this.btnSearchEvent.UseVisualStyleBackColor = true;
            this.btnSearchEvent.Click += new System.EventHandler(this.btnSearchEvent_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pnlEvent, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlCustomer, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlItem, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 326);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // pnlEvent
            // 
            this.pnlEvent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEvent.Controls.Add(this.txtEventDesciption);
            this.pnlEvent.Controls.Add(this.label11);
            this.pnlEvent.Controls.Add(this.txtEventType);
            this.pnlEvent.Controls.Add(this.label10);
            this.pnlEvent.Controls.Add(this.txtDeliveryDate);
            this.pnlEvent.Controls.Add(this.label6);
            this.pnlEvent.Controls.Add(this.label7);
            this.pnlEvent.Controls.Add(this.txtEventDate);
            this.pnlEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEvent.Location = new System.Drawing.Point(3, 3);
            this.pnlEvent.Name = "pnlEvent";
            this.pnlEvent.Size = new System.Drawing.Size(538, 68);
            this.pnlEvent.TabIndex = 3;
            // 
            // txtEventDesciption
            // 
            this.txtEventDesciption.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEventDesciption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEventDesciption.Location = new System.Drawing.Point(85, 35);
            this.txtEventDesciption.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.txtEventDesciption.Name = "txtEventDesciption";
            this.txtEventDesciption.ReadOnly = true;
            this.txtEventDesciption.Size = new System.Drawing.Size(200, 21);
            this.txtEventDesciption.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.SteelBlue;
            this.label11.Location = new System.Drawing.Point(2, 35);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 21);
            this.label11.TabIndex = 12;
            this.label11.Text = "Description";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEventType
            // 
            this.txtEventType.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEventType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEventType.Location = new System.Drawing.Point(85, 5);
            this.txtEventType.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.txtEventType.Name = "txtEventType";
            this.txtEventType.ReadOnly = true;
            this.txtEventType.Size = new System.Drawing.Size(200, 21);
            this.txtEventType.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.SteelBlue;
            this.label10.Location = new System.Drawing.Point(2, 5);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 21);
            this.label10.TabIndex = 2;
            this.label10.Text = "Event Type";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDeliveryDate
            // 
            this.txtDeliveryDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDeliveryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeliveryDate.Location = new System.Drawing.Point(445, 35);
            this.txtDeliveryDate.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.txtDeliveryDate.Name = "txtDeliveryDate";
            this.txtDeliveryDate.ReadOnly = true;
            this.txtDeliveryDate.Size = new System.Drawing.Size(85, 21);
            this.txtDeliveryDate.TabIndex = 11;
            this.txtDeliveryDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.SteelBlue;
            this.label6.Location = new System.Drawing.Point(370, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 21);
            this.label6.TabIndex = 8;
            this.label6.Text = "Event Date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(370, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 21);
            this.label7.TabIndex = 10;
            this.label7.Text = "Delivery Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEventDate
            // 
            this.txtEventDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEventDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEventDate.Location = new System.Drawing.Point(445, 5);
            this.txtEventDate.Name = "txtEventDate";
            this.txtEventDate.ReadOnly = true;
            this.txtEventDate.Size = new System.Drawing.Size(85, 21);
            this.txtEventDate.TabIndex = 9;
            this.txtEventDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomer.Controls.Add(this.label9);
            this.pnlCustomer.Controls.Add(this.txtDeliAddress);
            this.pnlCustomer.Controls.Add(this.txtCustName);
            this.pnlCustomer.Controls.Add(this.txtCustCode);
            this.pnlCustomer.Controls.Add(this.label5);
            this.pnlCustomer.Controls.Add(this.label3);
            this.pnlCustomer.Controls.Add(this.label2);
            this.pnlCustomer.Location = new System.Drawing.Point(3, 77);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(538, 80);
            this.pnlCustomer.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.DarkBlue;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(536, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Cutomer Details";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDeliAddress
            // 
            this.txtDeliAddress.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDeliAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeliAddress.Location = new System.Drawing.Point(85, 50);
            this.txtDeliAddress.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.txtDeliAddress.Name = "txtDeliAddress";
            this.txtDeliAddress.ReadOnly = true;
            this.txtDeliAddress.Size = new System.Drawing.Size(440, 21);
            this.txtDeliAddress.TabIndex = 7;
            // 
            // txtCustName
            // 
            this.txtCustName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustName.Location = new System.Drawing.Point(305, 20);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.ReadOnly = true;
            this.txtCustName.Size = new System.Drawing.Size(220, 21);
            this.txtCustName.TabIndex = 3;
            // 
            // txtCustCode
            // 
            this.txtCustCode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustCode.Location = new System.Drawing.Point(85, 20);
            this.txtCustCode.Name = "txtCustCode";
            this.txtCustCode.ReadOnly = true;
            this.txtCustCode.Size = new System.Drawing.Size(115, 21);
            this.txtCustCode.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(2, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "Address";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(222, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Customer Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(2, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Customer Code";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlItem
            // 
            this.pnlItem.Controls.Add(this.dgvItem);
            this.pnlItem.Controls.Add(this.label8);
            this.pnlItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlItem.Location = new System.Drawing.Point(3, 163);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(538, 160);
            this.pnlItem.TabIndex = 4;
            this.pnlItem.Visible = false;
            // 
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToResizeColumns = false;
            this.dgvItem.AllowUserToResizeRows = false;
            this.dgvItem.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvItem.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvItem.ColumnHeadersHeight = 25;
            this.dgvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemSelected,
            this.ItemLine,
            this.ItemCode,
            this.ItemStatus,
            this.ItemQty,
            this.SoldQty,
            this.PickQty,
            this.PriceBook,
            this.PriceLevel,
            this.UnitPrice});
            this.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItem.GridColor = System.Drawing.Color.DimGray;
            this.dgvItem.Location = new System.Drawing.Point(0, 16);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvItem.RowHeadersVisible = false;
            this.dgvItem.Size = new System.Drawing.Size(538, 144);
            this.dgvItem.TabIndex = 1;
            this.dgvItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItem_CellContentClick);
            // 
            // ItemSelected
            // 
            this.ItemSelected.HeaderText = "";
            this.ItemSelected.Name = "ItemSelected";
            this.ItemSelected.Width = 30;
            // 
            // ItemLine
            // 
            this.ItemLine.DataPropertyName = "SERE_LINE";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemLine.DefaultCellStyle = dataGridViewCellStyle1;
            this.ItemLine.HeaderText = "Line #";
            this.ItemLine.Name = "ItemLine";
            this.ItemLine.Width = 45;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "SERE_ITM_CD";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.ItemCode.FillWeight = 150F;
            this.ItemCode.HeaderText = "Item Code";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.Width = 150;
            // 
            // ItemStatus
            // 
            this.ItemStatus.DataPropertyName = "SERE_ITM_STUS";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemStatus.DefaultCellStyle = dataGridViewCellStyle3;
            this.ItemStatus.FillWeight = 285F;
            this.ItemStatus.HeaderText = "Status";
            this.ItemStatus.Name = "ItemStatus";
            this.ItemStatus.Width = 80;
            // 
            // ItemQty
            // 
            this.ItemQty.DataPropertyName = "SERE_ITM_QTY";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.ItemQty.HeaderText = "Item Qty";
            this.ItemQty.Name = "ItemQty";
            this.ItemQty.Width = 65;
            // 
            // SoldQty
            // 
            this.SoldQty.DataPropertyName = "SERE_ITM_SOLD";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SoldQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.SoldQty.HeaderText = "Sold Qty";
            this.SoldQty.Name = "SoldQty";
            this.SoldQty.Width = 65;
            // 
            // PickQty
            // 
            this.PickQty.HeaderText = "Pick Qty";
            this.PickQty.Name = "PickQty";
            this.PickQty.Width = 65;
            // 
            // PriceBook
            // 
            this.PriceBook.DataPropertyName = "SERE_PB";
            this.PriceBook.HeaderText = "Book";
            this.PriceBook.Name = "PriceBook";
            // 
            // PriceLevel
            // 
            this.PriceLevel.DataPropertyName = "SERE_PB_LVL";
            this.PriceLevel.HeaderText = "Level";
            this.PriceLevel.Name = "PriceLevel";
            // 
            // UnitPrice
            // 
            this.UnitPrice.DataPropertyName = "SERE_ITM_PRICE";
            this.UnitPrice.HeaderText = "Price";
            this.UnitPrice.Name = "UnitPrice";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DarkBlue;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(538, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Item Details";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.toolStripSeparator1,
            this.btnAddItem});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(2);
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(544, 35);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(2);
            this.btnClear.Size = new System.Drawing.Size(41, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.btnAddItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Padding = new System.Windows.Forms.Padding(2);
            this.btnAddItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnAddItem.Size = new System.Drawing.Size(67, 22);
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // frmEventRegistry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(544, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEventId);
            this.Controls.Add(this.btnSearchEvent);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(560, 400);
            this.MinimizeBox = false;
            this.Name = "frmEventRegistry";
            this.ShowIcon = false;
            this.Text = "Add Event Items";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlEvent.ResumeLayout(false);
            this.pnlEvent.PerformLayout();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.pnlItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEventId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchEvent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlCustomer;
        private System.Windows.Forms.TextBox txtDeliAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCustCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeliveryDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEventDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnlEvent;
        private System.Windows.Forms.TextBox txtEventType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAddItem;
        private System.Windows.Forms.TextBox txtEventDesciption;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ItemSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoldQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PickQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;

    }
}