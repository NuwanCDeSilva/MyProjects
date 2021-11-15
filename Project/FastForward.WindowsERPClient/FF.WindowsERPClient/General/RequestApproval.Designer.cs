namespace FF.WindowsERPClient.General
{
    partial class RequestApproval
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestApproval));
            this.cmbrequesttype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlgridrecord = new System.Windows.Forms.Panel();
            this.pnldiscountforcashcredit = new System.Windows.Forms.Panel();
            this.gridloadcustomerinfo = new System.Windows.Forms.DataGridView();
            this.Seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CusCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusProfitCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusPricebook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusPriceLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusDiscValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusDisRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusNoofItm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusFromDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusToDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusRequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusReqUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusSalesType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCustomerDetails = new System.Windows.Forms.Panel();
            this.btnReject = new System.Windows.Forms.Button();
            this.buttonApprove = new System.Windows.Forms.Button();
            this.btnprocessclose = new System.Windows.Forms.Button();
            this.lbladdress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCusName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gridloadDiscntCashCreditSales = new System.Windows.Forms.DataGridView();
            this.rowSelect = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblRefference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSeqNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CusCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoofItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.checkSelectAll = new System.Windows.Forms.CheckBox();
            this.dtdate = new System.Windows.Forms.DateTimePicker();
            this.btnSearchCusCode = new System.Windows.Forms.Button();
            this.textCustomerCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSearchLocation = new System.Windows.Forms.Button();
            this.textSearchLoc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUsrMsg = new System.Windows.Forms.Label();
            this.pnlgridrecord.SuspendLayout();
            this.pnldiscountforcashcredit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridloadcustomerinfo)).BeginInit();
            this.pnlCustomerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridloadDiscntCashCreditSales)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbrequesttype
            // 
            this.cmbrequesttype.FormattingEnabled = true;
            this.cmbrequesttype.Items.AddRange(new object[] {
            "DISCOUNT FOR CASH/CRD SALES"});
            this.cmbrequesttype.Location = new System.Drawing.Point(88, 15);
            this.cmbrequesttype.Name = "cmbrequesttype";
            this.cmbrequesttype.Size = new System.Drawing.Size(240, 21);
            this.cmbrequesttype.TabIndex = 0;
            this.cmbrequesttype.SelectedIndexChanged += new System.EventHandler(this.cmbrequesttype_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Request Type";
            // 
            // pnlgridrecord
            // 
            this.pnlgridrecord.Controls.Add(this.lblUsrMsg);
            this.pnlgridrecord.Controls.Add(this.pnldiscountforcashcredit);
            this.pnlgridrecord.Controls.Add(this.gridloadDiscntCashCreditSales);
            this.pnlgridrecord.Location = new System.Drawing.Point(4, 93);
            this.pnlgridrecord.Name = "pnlgridrecord";
            this.pnlgridrecord.Size = new System.Drawing.Size(749, 394);
            this.pnlgridrecord.TabIndex = 2;
            // 
            // pnldiscountforcashcredit
            // 
            this.pnldiscountforcashcredit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pnldiscountforcashcredit.Controls.Add(this.gridloadcustomerinfo);
            this.pnldiscountforcashcredit.Controls.Add(this.pnlCustomerDetails);
            this.pnldiscountforcashcredit.Location = new System.Drawing.Point(3, 123);
            this.pnldiscountforcashcredit.Name = "pnldiscountforcashcredit";
            this.pnldiscountforcashcredit.Size = new System.Drawing.Size(746, 268);
            this.pnldiscountforcashcredit.TabIndex = 3;
            this.pnldiscountforcashcredit.Visible = false;
            // 
            // gridloadcustomerinfo
            // 
            this.gridloadcustomerinfo.AllowUserToAddRows = false;
            this.gridloadcustomerinfo.AllowUserToDeleteRows = false;
            this.gridloadcustomerinfo.AllowUserToResizeRows = false;
            this.gridloadcustomerinfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridloadcustomerinfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridloadcustomerinfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridloadcustomerinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridloadcustomerinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seq,
            this.CusCompany,
            this.cusCompanyName,
            this.cusProfitCenter,
            this.cusPricebook,
            this.cusPriceLevel,
            this.cusItem,
            this.cusDiscValue,
            this.cusDisRate,
            this.cusNoofItm,
            this.Ref,
            this.cusFromDate,
            this.cusToDate,
            this.cusRequestDate,
            this.cusReqUser,
            this.cusSalesType});
            this.gridloadcustomerinfo.EnableHeadersVisualStyles = false;
            this.gridloadcustomerinfo.GridColor = System.Drawing.Color.White;
            this.gridloadcustomerinfo.Location = new System.Drawing.Point(3, 109);
            this.gridloadcustomerinfo.Name = "gridloadcustomerinfo";
            this.gridloadcustomerinfo.Size = new System.Drawing.Size(740, 86);
            this.gridloadcustomerinfo.TabIndex = 5;
            // 
            // Seq
            // 
            this.Seq.DataPropertyName = "sgdd_seq";
            this.Seq.HeaderText = "Seq";
            this.Seq.Name = "Seq";
            this.Seq.Visible = false;
            this.Seq.Width = 51;
            // 
            // CusCompany
            // 
            this.CusCompany.DataPropertyName = "sgdd_com";
            this.CusCompany.HeaderText = "Company";
            this.CusCompany.Name = "CusCompany";
            this.CusCompany.ReadOnly = true;
            this.CusCompany.Width = 76;
            // 
            // cusCompanyName
            // 
            this.cusCompanyName.DataPropertyName = "ComName";
            this.cusCompanyName.HeaderText = "Company Name";
            this.cusCompanyName.Name = "cusCompanyName";
            this.cusCompanyName.ReadOnly = true;
            this.cusCompanyName.Width = 98;
            // 
            // cusProfitCenter
            // 
            this.cusProfitCenter.DataPropertyName = "sgdd_pc";
            this.cusProfitCenter.HeaderText = "Profit Center";
            this.cusProfitCenter.Name = "cusProfitCenter";
            this.cusProfitCenter.ReadOnly = true;
            this.cusProfitCenter.Width = 83;
            // 
            // cusPricebook
            // 
            this.cusPricebook.DataPropertyName = "sgdd_pb";
            this.cusPricebook.HeaderText = "Price Book";
            this.cusPricebook.Name = "cusPricebook";
            this.cusPricebook.ReadOnly = true;
            this.cusPricebook.Width = 78;
            // 
            // cusPriceLevel
            // 
            this.cusPriceLevel.DataPropertyName = "sgdd_pb_lvl";
            this.cusPriceLevel.HeaderText = "Price Level";
            this.cusPriceLevel.Name = "cusPriceLevel";
            this.cusPriceLevel.ReadOnly = true;
            this.cusPriceLevel.Width = 78;
            // 
            // cusItem
            // 
            this.cusItem.DataPropertyName = "sgdd_itm";
            this.cusItem.HeaderText = "Item";
            this.cusItem.Name = "cusItem";
            this.cusItem.ReadOnly = true;
            this.cusItem.Width = 52;
            // 
            // cusDiscValue
            // 
            this.cusDiscValue.DataPropertyName = "sgdd_disc_val";
            this.cusDiscValue.HeaderText = "Discount Value";
            this.cusDiscValue.Name = "cusDiscValue";
            this.cusDiscValue.Width = 96;
            // 
            // cusDisRate
            // 
            this.cusDisRate.DataPropertyName = "sgdd_disc_rt";
            this.cusDisRate.HeaderText = "Discount Rate";
            this.cusDisRate.Name = "cusDisRate";
            this.cusDisRate.Width = 92;
            // 
            // cusNoofItm
            // 
            this.cusNoofItm.DataPropertyName = "sgdd_no_of_times";
            this.cusNoofItm.HeaderText = "# Of Items";
            this.cusNoofItm.Name = "cusNoofItm";
            this.cusNoofItm.ReadOnly = true;
            this.cusNoofItm.Width = 75;
            // 
            // Ref
            // 
            this.Ref.DataPropertyName = "sgdd_req_ref";
            this.Ref.HeaderText = "Reference #";
            this.Ref.Name = "Ref";
            this.Ref.ReadOnly = true;
            this.Ref.Width = 85;
            // 
            // cusFromDate
            // 
            this.cusFromDate.HeaderText = "FromDate";
            this.cusFromDate.Name = "cusFromDate";
            this.cusFromDate.ReadOnly = true;
            this.cusFromDate.Visible = false;
            this.cusFromDate.Width = 78;
            // 
            // cusToDate
            // 
            this.cusToDate.HeaderText = "ToDate";
            this.cusToDate.Name = "cusToDate";
            this.cusToDate.ReadOnly = true;
            this.cusToDate.Visible = false;
            this.cusToDate.Width = 68;
            // 
            // cusRequestDate
            // 
            this.cusRequestDate.HeaderText = "Request Date";
            this.cusRequestDate.Name = "cusRequestDate";
            this.cusRequestDate.ReadOnly = true;
            this.cusRequestDate.Width = 90;
            // 
            // cusReqUser
            // 
            this.cusReqUser.HeaderText = "Request User";
            this.cusReqUser.Name = "cusReqUser";
            this.cusReqUser.ReadOnly = true;
            this.cusReqUser.Width = 89;
            // 
            // cusSalesType
            // 
            this.cusSalesType.DataPropertyName = "sgdd_sale_tp";
            this.cusSalesType.HeaderText = "Sales Type";
            this.cusSalesType.Name = "cusSalesType";
            this.cusSalesType.ReadOnly = true;
            this.cusSalesType.Width = 78;
            // 
            // pnlCustomerDetails
            // 
            this.pnlCustomerDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomerDetails.Controls.Add(this.btnReject);
            this.pnlCustomerDetails.Controls.Add(this.buttonApprove);
            this.pnlCustomerDetails.Controls.Add(this.btnprocessclose);
            this.pnlCustomerDetails.Controls.Add(this.lbladdress);
            this.pnlCustomerDetails.Controls.Add(this.label4);
            this.pnlCustomerDetails.Controls.Add(this.lblCusName);
            this.pnlCustomerDetails.Controls.Add(this.label3);
            this.pnlCustomerDetails.Controls.Add(this.lblCustomerCode);
            this.pnlCustomerDetails.Controls.Add(this.label2);
            this.pnlCustomerDetails.Location = new System.Drawing.Point(3, 3);
            this.pnlCustomerDetails.Name = "pnlCustomerDetails";
            this.pnlCustomerDetails.Size = new System.Drawing.Size(740, 100);
            this.pnlCustomerDetails.TabIndex = 4;
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Location = new System.Drawing.Point(637, 1);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(74, 24);
            this.btnReject.TabIndex = 143;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click_1);
            // 
            // buttonApprove
            // 
            this.buttonApprove.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApprove.Location = new System.Drawing.Point(560, 1);
            this.buttonApprove.Name = "buttonApprove";
            this.buttonApprove.Size = new System.Drawing.Size(74, 24);
            this.buttonApprove.TabIndex = 142;
            this.buttonApprove.Text = "Approve";
            this.buttonApprove.UseVisualStyleBackColor = false;
            this.buttonApprove.Click += new System.EventHandler(this.buttonApprove_Click_1);
            // 
            // btnprocessclose
            // 
            this.btnprocessclose.BackColor = System.Drawing.Color.Lavender;
            this.btnprocessclose.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.ExitSCM2;
            this.btnprocessclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnprocessclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprocessclose.ForeColor = System.Drawing.Color.Lavender;
            this.btnprocessclose.Location = new System.Drawing.Point(710, 1);
            this.btnprocessclose.Name = "btnprocessclose";
            this.btnprocessclose.Size = new System.Drawing.Size(27, 24);
            this.btnprocessclose.TabIndex = 19;
            this.btnprocessclose.UseVisualStyleBackColor = false;
            this.btnprocessclose.Click += new System.EventHandler(this.btnPromoVouClose_Click);
            // 
            // lbladdress
            // 
            this.lbladdress.AutoSize = true;
            this.lbladdress.Location = new System.Drawing.Point(123, 53);
            this.lbladdress.Name = "lbladdress";
            this.lbladdress.Size = new System.Drawing.Size(35, 13);
            this.lbladdress.TabIndex = 1;
            this.lbladdress.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Address";
            // 
            // lblCusName
            // 
            this.lblCusName.AutoSize = true;
            this.lblCusName.Location = new System.Drawing.Point(123, 9);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(35, 13);
            this.lblCusName.TabIndex = 3;
            this.lblCusName.Text = "label5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Customer Code";
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.AutoSize = true;
            this.lblCustomerCode.Location = new System.Drawing.Point(123, 31);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(35, 13);
            this.lblCustomerCode.TabIndex = 0;
            this.lblCustomerCode.Text = "label5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Customer Name";
            // 
            // gridloadDiscntCashCreditSales
            // 
            this.gridloadDiscntCashCreditSales.AllowUserToAddRows = false;
            this.gridloadDiscntCashCreditSales.AllowUserToDeleteRows = false;
            this.gridloadDiscntCashCreditSales.AllowUserToOrderColumns = true;
            this.gridloadDiscntCashCreditSales.AllowUserToResizeRows = false;
            this.gridloadDiscntCashCreditSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridloadDiscntCashCreditSales.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridloadDiscntCashCreditSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridloadDiscntCashCreditSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridloadDiscntCashCreditSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rowSelect,
            this.lblRefference,
            this.lblSeqNo,
            this.Company,
            this.CompanyName,
            this.ProfitCenter,
            this.CusCode,
            this.Customer,
            this.PriceLevel,
            this.Item,
            this.DiscountValue,
            this.DiscountRate,
            this.NoofItems,
            this.SaleType});
            this.gridloadDiscntCashCreditSales.EnableHeadersVisualStyles = false;
            this.gridloadDiscntCashCreditSales.GridColor = System.Drawing.Color.White;
            this.gridloadDiscntCashCreditSales.Location = new System.Drawing.Point(2, 3);
            this.gridloadDiscntCashCreditSales.Name = "gridloadDiscntCashCreditSales";
            this.gridloadDiscntCashCreditSales.Size = new System.Drawing.Size(747, 388);
            this.gridloadDiscntCashCreditSales.TabIndex = 0;
            this.gridloadDiscntCashCreditSales.Visible = false;
            this.gridloadDiscntCashCreditSales.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridloadDiscntCashCreditSales_CellContentClick);
            // 
            // rowSelect
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            this.rowSelect.DefaultCellStyle = dataGridViewCellStyle3;
            this.rowSelect.HeaderText = "";
            this.rowSelect.Name = "rowSelect";
            this.rowSelect.Width = 5;
            // 
            // lblRefference
            // 
            this.lblRefference.DataPropertyName = "sgdd_req_ref";
            this.lblRefference.HeaderText = "lblRefference";
            this.lblRefference.Name = "lblRefference";
            this.lblRefference.Visible = false;
            this.lblRefference.Width = 95;
            // 
            // lblSeqNo
            // 
            this.lblSeqNo.DataPropertyName = "sgdd_seq";
            this.lblSeqNo.HeaderText = "lblSeqNo";
            this.lblSeqNo.Name = "lblSeqNo";
            this.lblSeqNo.Visible = false;
            this.lblSeqNo.Width = 75;
            // 
            // Company
            // 
            this.Company.DataPropertyName = "sgdd_com";
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            this.Company.ReadOnly = true;
            this.Company.Width = 76;
            // 
            // CompanyName
            // 
            this.CompanyName.DataPropertyName = "ComName";
            this.CompanyName.HeaderText = "Company Name";
            this.CompanyName.Name = "CompanyName";
            this.CompanyName.ReadOnly = true;
            this.CompanyName.Width = 98;
            // 
            // ProfitCenter
            // 
            this.ProfitCenter.DataPropertyName = "sgdd_pc";
            this.ProfitCenter.HeaderText = "Profit Center";
            this.ProfitCenter.Name = "ProfitCenter";
            this.ProfitCenter.ReadOnly = true;
            this.ProfitCenter.Width = 83;
            // 
            // CusCode
            // 
            this.CusCode.DataPropertyName = "sgdd_cust_cd";
            this.CusCode.HeaderText = "CusCode";
            this.CusCode.Name = "CusCode";
            this.CusCode.ReadOnly = true;
            this.CusCode.Width = 75;
            // 
            // Customer
            // 
            this.Customer.DataPropertyName = "mbe_name";
            this.Customer.HeaderText = "Customer";
            this.Customer.Name = "Customer";
            this.Customer.ReadOnly = true;
            this.Customer.Width = 76;
            // 
            // PriceLevel
            // 
            this.PriceLevel.DataPropertyName = "sgdd_pb_lvl";
            this.PriceLevel.HeaderText = "Price Level";
            this.PriceLevel.Name = "PriceLevel";
            this.PriceLevel.ReadOnly = true;
            this.PriceLevel.Width = 78;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "sgdd_itm";
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 52;
            // 
            // DiscountValue
            // 
            this.DiscountValue.DataPropertyName = "sgdd_disc_val";
            this.DiscountValue.HeaderText = "Discount Value";
            this.DiscountValue.Name = "DiscountValue";
            this.DiscountValue.ReadOnly = true;
            this.DiscountValue.Width = 96;
            // 
            // DiscountRate
            // 
            this.DiscountRate.DataPropertyName = "sgdd_disc_rt";
            this.DiscountRate.HeaderText = "Discount Rate";
            this.DiscountRate.Name = "DiscountRate";
            this.DiscountRate.Width = 92;
            // 
            // NoofItems
            // 
            this.NoofItems.DataPropertyName = "sgdd_no_of_times";
            this.NoofItems.HeaderText = "#of Items";
            this.NoofItems.Name = "NoofItems";
            this.NoofItems.ReadOnly = true;
            this.NoofItems.Width = 70;
            // 
            // SaleType
            // 
            this.SaleType.DataPropertyName = "sgdd_sale_tp";
            this.SaleType.HeaderText = "Sales Type";
            this.SaleType.Name = "SaleType";
            this.SaleType.ReadOnly = true;
            this.SaleType.Width = 78;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.checkSelectAll);
            this.panel1.Controls.Add(this.dtdate);
            this.panel1.Controls.Add(this.btnSearchCusCode);
            this.panel1.Controls.Add(this.textCustomerCode);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnSearchLocation);
            this.panel1.Controls.Add(this.textSearchLoc);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(7, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 43);
            this.panel1.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSearch.Enabled = false;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(507, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 24);
            this.btnSearch.TabIndex = 142;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // checkSelectAll
            // 
            this.checkSelectAll.AutoSize = true;
            this.checkSelectAll.Enabled = false;
            this.checkSelectAll.Location = new System.Drawing.Point(442, 15);
            this.checkSelectAll.Name = "checkSelectAll";
            this.checkSelectAll.Size = new System.Drawing.Size(67, 17);
            this.checkSelectAll.TabIndex = 122;
            this.checkSelectAll.Text = "SelectAll";
            this.checkSelectAll.UseVisualStyleBackColor = true;
            this.checkSelectAll.CheckedChanged += new System.EventHandler(this.checkSelectAll_CheckedChanged);
            // 
            // dtdate
            // 
            this.dtdate.Enabled = false;
            this.dtdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtdate.Location = new System.Drawing.Point(26, 12);
            this.dtdate.Name = "dtdate";
            this.dtdate.Size = new System.Drawing.Size(88, 20);
            this.dtdate.TabIndex = 5;
            // 
            // btnSearchCusCode
            // 
            this.btnSearchCusCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCusCode.BackgroundImage")));
            this.btnSearchCusCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchCusCode.Enabled = false;
            this.btnSearchCusCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchCusCode.ForeColor = System.Drawing.Color.White;
            this.btnSearchCusCode.Location = new System.Drawing.Point(403, 13);
            this.btnSearchCusCode.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearchCusCode.Name = "btnSearchCusCode";
            this.btnSearchCusCode.Size = new System.Drawing.Size(20, 20);
            this.btnSearchCusCode.TabIndex = 32;
            this.btnSearchCusCode.UseVisualStyleBackColor = true;
            this.btnSearchCusCode.Click += new System.EventHandler(this.btnSearchCusCode_Click);
            // 
            // textCustomerCode
            // 
            this.textCustomerCode.Enabled = false;
            this.textCustomerCode.Location = new System.Drawing.Point(325, 13);
            this.textCustomerCode.Name = "textCustomerCode";
            this.textCustomerCode.Size = new System.Drawing.Size(78, 20);
            this.textCustomerCode.TabIndex = 120;
            this.textCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textCustomerCode_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(249, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 119;
            this.label8.Text = "Customer Code";
            // 
            // btnSearchLocation
            // 
            this.btnSearchLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchLocation.BackgroundImage")));
            this.btnSearchLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchLocation.Enabled = false;
            this.btnSearchLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchLocation.ForeColor = System.Drawing.Color.White;
            this.btnSearchLocation.Location = new System.Drawing.Point(230, 11);
            this.btnSearchLocation.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearchLocation.Name = "btnSearchLocation";
            this.btnSearchLocation.Size = new System.Drawing.Size(20, 20);
            this.btnSearchLocation.TabIndex = 118;
            this.btnSearchLocation.UseVisualStyleBackColor = true;
            this.btnSearchLocation.Click += new System.EventHandler(this.btnSearchLocation_Click);
            // 
            // textSearchLoc
            // 
            this.textSearchLoc.Enabled = false;
            this.textSearchLoc.Location = new System.Drawing.Point(156, 12);
            this.textSearchLoc.Name = "textSearchLoc";
            this.textSearchLoc.Size = new System.Drawing.Size(73, 20);
            this.textSearchLoc.TabIndex = 3;
            this.textSearchLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSearchLoc_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(111, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Location";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-1, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Search By";
            // 
            // lblUsrMsg
            // 
            this.lblUsrMsg.AutoSize = true;
            this.lblUsrMsg.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsrMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblUsrMsg.Location = new System.Drawing.Point(222, 86);
            this.lblUsrMsg.Name = "lblUsrMsg";
            this.lblUsrMsg.Size = new System.Drawing.Size(208, 13);
            this.lblUsrMsg.TabIndex = 41;
            this.lblUsrMsg.Text = ":: No data found for selected criteria";
            this.lblUsrMsg.Visible = false;
            // 
            // RequestApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 527);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnlgridrecord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbrequesttype);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "RequestApproval";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RequestApproval";
            this.pnlgridrecord.ResumeLayout(false);
            this.pnlgridrecord.PerformLayout();
            this.pnldiscountforcashcredit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridloadcustomerinfo)).EndInit();
            this.pnlCustomerDetails.ResumeLayout(false);
            this.pnlCustomerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridloadDiscntCashCreditSales)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbrequesttype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlgridrecord;
        private System.Windows.Forms.DataGridView gridloadDiscntCashCreditSales;
        private System.Windows.Forms.Panel pnldiscountforcashcredit;
        private System.Windows.Forms.Label lblCusName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlCustomerDetails;
        private System.Windows.Forms.Label lbladdress;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.DataGridView gridloadcustomerinfo;
        private System.Windows.Forms.Button btnprocessclose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn CusCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusCompanyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusProfitCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusPricebook;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusPriceLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusDiscValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusDisRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusNoofItm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusFromDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusToDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusRequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusReqUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusSalesType;
        private System.Windows.Forms.DataGridViewButtonColumn rowSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblRefference;
        private System.Windows.Forms.DataGridViewTextBoxColumn lblSeqNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn CusCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoofItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textSearchLoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textCustomerCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSearchLocation;
        private System.Windows.Forms.Button btnSearchCusCode;
        private System.Windows.Forms.DateTimePicker dtdate;
        private System.Windows.Forms.CheckBox checkSelectAll;
        private System.Windows.Forms.Button buttonApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblUsrMsg;
    }
}