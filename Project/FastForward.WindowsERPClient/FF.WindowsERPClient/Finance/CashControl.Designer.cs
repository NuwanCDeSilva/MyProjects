namespace FF.WindowsERPClient.Finance
{
    partial class CashControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProfitCenter = new System.Windows.Forms.Button();
            this.txtAdjPC = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.optDr = new System.Windows.Forms.RadioButton();
            this.optCr = new System.Windows.Forms.RadioButton();
            this.btnAddAdj = new System.Windows.Forms.Button();
            this.grvAdj = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.rem_pc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rem_sh_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rem_val_final_CR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rem_val_final_DR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rem_rmk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rem_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAdjRem = new System.Windows.Forms.TextBox();
            this.btn_srch_adj = new System.Windows.Forms.Button();
            this.txtAdjCode = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCompAddr = new System.Windows.Forms.TextBox();
            this.lstPC = new System.Windows.Forms.ListView();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnClearPC = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.txtPCDesn = new System.Windows.Forms.TextBox();
            this.txtZoneDesc = new System.Windows.Forms.TextBox();
            this.txtRegDesc = new System.Windows.Forms.TextBox();
            this.txtAreaDesc = new System.Windows.Forms.TextBox();
            this.txtSChnlDesc = new System.Windows.Forms.TextBox();
            this.txtChnlDesc = new System.Windows.Forms.TextBox();
            this.txtCompDesc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPC = new System.Windows.Forms.TextBox();
            this.txtZone = new System.Windows.Forms.TextBox();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.txtSChanel = new System.Windows.Forms.TextBox();
            this.txtChanel = new System.Windows.Forms.TextBox();
            this.txtComp = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblAdj = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtRem = new System.Windows.Forms.TextBox();
            this.btnFinalize = new System.Windows.Forms.Button();
            this.txtAmt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblStus = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grvAdj)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtDate
            // 
            this.dtDate.Checked = false;
            this.dtDate.CustomFormat = "MMM/yyyy";
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDate.Location = new System.Drawing.Point(106, 6);
            this.dtDate.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(96, 21);
            this.dtDate.TabIndex = 0;
            this.dtDate.ValueChanged += new System.EventHandler(this.dtDate_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 10);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 463;
            this.label8.Text = "Month";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 496;
            this.label1.Text = "Adjustment Type";
            // 
            // btnProfitCenter
            // 
            this.btnProfitCenter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProfitCenter.BackgroundImage")));
            this.btnProfitCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnProfitCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfitCenter.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnProfitCenter.Location = new System.Drawing.Point(532, 6);
            this.btnProfitCenter.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnProfitCenter.Name = "btnProfitCenter";
            this.btnProfitCenter.Size = new System.Drawing.Size(20, 20);
            this.btnProfitCenter.TabIndex = 500;
            this.btnProfitCenter.UseVisualStyleBackColor = true;
            this.btnProfitCenter.Click += new System.EventHandler(this.btnProfitCenter_Click);
            // 
            // txtAdjPC
            // 
            this.txtAdjPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdjPC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAdjPC.Location = new System.Drawing.Point(407, 6);
            this.txtAdjPC.Name = "txtAdjPC";
            this.txtAdjPC.Size = new System.Drawing.Size(118, 21);
            this.txtAdjPC.TabIndex = 1;
            this.txtAdjPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAdjPC_KeyDown);
            this.txtAdjPC.Leave += new System.EventHandler(this.txtAdjPC_Leave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(313, 10);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(67, 13);
            this.label25.TabIndex = 498;
            this.label25.Text = "Profit center";
            // 
            // optDr
            // 
            this.optDr.AutoSize = true;
            this.optDr.Checked = true;
            this.optDr.Location = new System.Drawing.Point(538, 34);
            this.optDr.Name = "optDr";
            this.optDr.Size = new System.Drawing.Size(50, 17);
            this.optDr.TabIndex = 549;
            this.optDr.TabStop = true;
            this.optDr.Text = "Debit";
            this.optDr.UseVisualStyleBackColor = true;
            // 
            // optCr
            // 
            this.optCr.AutoSize = true;
            this.optCr.Location = new System.Drawing.Point(595, 34);
            this.optCr.Name = "optCr";
            this.optCr.Size = new System.Drawing.Size(54, 17);
            this.optCr.TabIndex = 550;
            this.optCr.TabStop = true;
            this.optCr.Text = "Credit";
            this.optCr.UseVisualStyleBackColor = true;
            // 
            // btnAddAdj
            // 
            this.btnAddAdj.BackColor = System.Drawing.Color.Transparent;
            this.btnAddAdj.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.downloadarrowicon;
            this.btnAddAdj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddAdj.ForeColor = System.Drawing.Color.Transparent;
            this.btnAddAdj.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddAdj.Location = new System.Drawing.Point(592, 68);
            this.btnAddAdj.Name = "btnAddAdj";
            this.btnAddAdj.Size = new System.Drawing.Size(32, 31);
            this.btnAddAdj.TabIndex = 5;
            this.btnAddAdj.UseVisualStyleBackColor = false;
            this.btnAddAdj.Click += new System.EventHandler(this.btnAddAdj_Click);
            // 
            // grvAdj
            // 
            this.grvAdj.AllowUserToAddRows = false;
            this.grvAdj.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
            this.grvAdj.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grvAdj.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvAdj.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grvAdj.ColumnHeadersHeight = 20;
            this.grvAdj.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.rem_pc,
            this.rem_sh_desc,
            this.rem_val_final_CR,
            this.rem_val_final_DR,
            this.rem_rmk,
            this.rem_cd});
            this.grvAdj.EnableHeadersVisualStyles = false;
            this.grvAdj.Location = new System.Drawing.Point(3, 123);
            this.grvAdj.MultiSelect = false;
            this.grvAdj.Name = "grvAdj";
            this.grvAdj.RowHeadersVisible = false;
            this.grvAdj.RowTemplate.Height = 20;
            this.grvAdj.Size = new System.Drawing.Size(643, 150);
            this.grvAdj.TabIndex = 552;
            this.grvAdj.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvAdj_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Image = global::FF.WindowsERPClient.Properties.Resources.deleteicon;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 20;
            // 
            // rem_pc
            // 
            this.rem_pc.DataPropertyName = "rem_pc";
            this.rem_pc.HeaderText = "Showroom";
            this.rem_pc.Name = "rem_pc";
            this.rem_pc.ReadOnly = true;
            this.rem_pc.Width = 80;
            // 
            // rem_sh_desc
            // 
            this.rem_sh_desc.DataPropertyName = "rem_sh_desc";
            this.rem_sh_desc.HeaderText = "Description";
            this.rem_sh_desc.Name = "rem_sh_desc";
            this.rem_sh_desc.ReadOnly = true;
            this.rem_sh_desc.Width = 175;
            // 
            // rem_val_final_CR
            // 
            this.rem_val_final_CR.DataPropertyName = "rem_val_final_CR";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.rem_val_final_CR.DefaultCellStyle = dataGridViewCellStyle3;
            this.rem_val_final_CR.HeaderText = "Credit";
            this.rem_val_final_CR.Name = "rem_val_final_CR";
            this.rem_val_final_CR.ReadOnly = true;
            this.rem_val_final_CR.Width = 80;
            // 
            // rem_val_final_DR
            // 
            this.rem_val_final_DR.DataPropertyName = "rem_val_final_DR";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.rem_val_final_DR.DefaultCellStyle = dataGridViewCellStyle4;
            this.rem_val_final_DR.HeaderText = "Debit";
            this.rem_val_final_DR.Name = "rem_val_final_DR";
            this.rem_val_final_DR.Width = 80;
            // 
            // rem_rmk
            // 
            this.rem_rmk.DataPropertyName = "rem_rmk";
            this.rem_rmk.HeaderText = "Remarks";
            this.rem_rmk.Name = "rem_rmk";
            this.rem_rmk.ReadOnly = true;
            this.rem_rmk.Width = 180;
            // 
            // rem_cd
            // 
            this.rem_cd.DataPropertyName = "rem_cd";
            this.rem_cd.HeaderText = "Column1";
            this.rem_cd.Name = "rem_cd";
            this.rem_cd.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 554;
            this.label2.Text = "Remarks";
            // 
            // txtAdjRem
            // 
            this.txtAdjRem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdjRem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAdjRem.Location = new System.Drawing.Point(106, 74);
            this.txtAdjRem.MaxLength = 200;
            this.txtAdjRem.Name = "txtAdjRem";
            this.txtAdjRem.Size = new System.Drawing.Size(477, 21);
            this.txtAdjRem.TabIndex = 4;
            // 
            // btn_srch_adj
            // 
            this.btn_srch_adj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_srch_adj.BackgroundImage")));
            this.btn_srch_adj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_srch_adj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_srch_adj.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_srch_adj.Location = new System.Drawing.Point(268, 32);
            this.btn_srch_adj.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_srch_adj.Name = "btn_srch_adj";
            this.btn_srch_adj.Size = new System.Drawing.Size(20, 20);
            this.btn_srch_adj.TabIndex = 556;
            this.btn_srch_adj.UseVisualStyleBackColor = true;
            this.btn_srch_adj.Click += new System.EventHandler(this.btn_srch_adj_Click);
            // 
            // txtAdjCode
            // 
            this.txtAdjCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdjCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAdjCode.Location = new System.Drawing.Point(107, 32);
            this.txtAdjCode.Name = "txtAdjCode";
            this.txtAdjCode.Size = new System.Drawing.Size(154, 21);
            this.txtAdjCode.TabIndex = 2;
            this.txtAdjCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAdjCode_KeyDown);
            this.txtAdjCode.Leave += new System.EventHandler(this.txtAdjCode_Leave);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtCompAddr);
            this.panel3.Controls.Add(this.lstPC);
            this.panel3.Controls.Add(this.btnNone);
            this.panel3.Controls.Add(this.btnClearPC);
            this.panel3.Controls.Add(this.btnAll);
            this.panel3.Controls.Add(this.btnAddItem);
            this.panel3.Controls.Add(this.txtPCDesn);
            this.panel3.Controls.Add(this.txtZoneDesc);
            this.panel3.Controls.Add(this.txtRegDesc);
            this.panel3.Controls.Add(this.txtAreaDesc);
            this.panel3.Controls.Add(this.txtSChnlDesc);
            this.panel3.Controls.Add(this.txtChnlDesc);
            this.panel3.Controls.Add(this.txtCompDesc);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtPC);
            this.panel3.Controls.Add(this.txtZone);
            this.panel3.Controls.Add(this.txtRegion);
            this.panel3.Controls.Add(this.txtArea);
            this.panel3.Controls.Add(this.txtSChanel);
            this.panel3.Controls.Add(this.txtChanel);
            this.panel3.Controls.Add(this.txtComp);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Location = new System.Drawing.Point(2, 291);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(644, 155);
            this.panel3.TabIndex = 557;
            // 
            // txtCompAddr
            // 
            this.txtCompAddr.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtCompAddr.Enabled = false;
            this.txtCompAddr.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompAddr.Location = new System.Drawing.Point(407, 5);
            this.txtCompAddr.Name = "txtCompAddr";
            this.txtCompAddr.Size = new System.Drawing.Size(26, 19);
            this.txtCompAddr.TabIndex = 80;
            this.txtCompAddr.Visible = false;
            // 
            // lstPC
            // 
            this.lstPC.CheckBoxes = true;
            this.lstPC.Location = new System.Drawing.Point(445, 7);
            this.lstPC.Name = "lstPC";
            this.lstPC.Size = new System.Drawing.Size(192, 120);
            this.lstPC.TabIndex = 79;
            this.lstPC.UseCompatibleStateImageBehavior = false;
            this.lstPC.View = System.Windows.Forms.View.List;
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(514, 130);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(54, 21);
            this.btnNone.TabIndex = 77;
            this.btnNone.Text = "None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // btnClearPC
            // 
            this.btnClearPC.Location = new System.Drawing.Point(574, 130);
            this.btnClearPC.Name = "btnClearPC";
            this.btnClearPC.Size = new System.Drawing.Size(54, 21);
            this.btnClearPC.TabIndex = 76;
            this.btnClearPC.Text = "Clear";
            this.btnClearPC.UseVisualStyleBackColor = true;
            this.btnClearPC.Click += new System.EventHandler(this.btnClearPC_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(454, 130);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(54, 21);
            this.btnAll.TabIndex = 75;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.right_arrow_icon;
            this.btnAddItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddItem.Location = new System.Drawing.Point(408, 67);
            this.btnAddItem.MaximumSize = new System.Drawing.Size(32, 36);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(31, 36);
            this.btnAddItem.TabIndex = 73;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // txtPCDesn
            // 
            this.txtPCDesn.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtPCDesn.Enabled = false;
            this.txtPCDesn.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPCDesn.Location = new System.Drawing.Point(150, 125);
            this.txtPCDesn.Name = "txtPCDesn";
            this.txtPCDesn.Size = new System.Drawing.Size(252, 19);
            this.txtPCDesn.TabIndex = 71;
            // 
            // txtZoneDesc
            // 
            this.txtZoneDesc.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtZoneDesc.Enabled = false;
            this.txtZoneDesc.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZoneDesc.Location = new System.Drawing.Point(150, 105);
            this.txtZoneDesc.Name = "txtZoneDesc";
            this.txtZoneDesc.Size = new System.Drawing.Size(252, 19);
            this.txtZoneDesc.TabIndex = 70;
            // 
            // txtRegDesc
            // 
            this.txtRegDesc.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtRegDesc.Enabled = false;
            this.txtRegDesc.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegDesc.Location = new System.Drawing.Point(150, 85);
            this.txtRegDesc.Name = "txtRegDesc";
            this.txtRegDesc.Size = new System.Drawing.Size(252, 19);
            this.txtRegDesc.TabIndex = 69;
            // 
            // txtAreaDesc
            // 
            this.txtAreaDesc.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtAreaDesc.Enabled = false;
            this.txtAreaDesc.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAreaDesc.Location = new System.Drawing.Point(150, 65);
            this.txtAreaDesc.Name = "txtAreaDesc";
            this.txtAreaDesc.Size = new System.Drawing.Size(252, 19);
            this.txtAreaDesc.TabIndex = 68;
            // 
            // txtSChnlDesc
            // 
            this.txtSChnlDesc.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtSChnlDesc.Enabled = false;
            this.txtSChnlDesc.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSChnlDesc.Location = new System.Drawing.Point(150, 45);
            this.txtSChnlDesc.Name = "txtSChnlDesc";
            this.txtSChnlDesc.Size = new System.Drawing.Size(252, 19);
            this.txtSChnlDesc.TabIndex = 67;
            // 
            // txtChnlDesc
            // 
            this.txtChnlDesc.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtChnlDesc.Enabled = false;
            this.txtChnlDesc.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChnlDesc.Location = new System.Drawing.Point(150, 25);
            this.txtChnlDesc.Name = "txtChnlDesc";
            this.txtChnlDesc.Size = new System.Drawing.Size(252, 19);
            this.txtChnlDesc.TabIndex = 66;
            // 
            // txtCompDesc
            // 
            this.txtCompDesc.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtCompDesc.Enabled = false;
            this.txtCompDesc.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompDesc.Location = new System.Drawing.Point(150, 5);
            this.txtCompDesc.Name = "txtCompDesc";
            this.txtCompDesc.Size = new System.Drawing.Size(252, 19);
            this.txtCompDesc.TabIndex = 65;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Profit Center";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "Zone";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 88);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Region";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "Area";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 48);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Sub Channel";
            // 
            // txtPC
            // 
            this.txtPC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPC.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPC.Location = new System.Drawing.Point(80, 125);
            this.txtPC.Name = "txtPC";
            this.txtPC.Size = new System.Drawing.Size(65, 19);
            this.txtPC.TabIndex = 58;
            this.txtPC.DoubleClick += new System.EventHandler(this.txtPC_DoubleClick);
            this.txtPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPC_KeyDown);
            // 
            // txtZone
            // 
            this.txtZone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtZone.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZone.Location = new System.Drawing.Point(80, 105);
            this.txtZone.Name = "txtZone";
            this.txtZone.Size = new System.Drawing.Size(65, 19);
            this.txtZone.TabIndex = 57;
            this.txtZone.DoubleClick += new System.EventHandler(this.txtZone_DoubleClick);
            this.txtZone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZone_KeyDown);
            // 
            // txtRegion
            // 
            this.txtRegion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRegion.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegion.Location = new System.Drawing.Point(80, 85);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.Size = new System.Drawing.Size(65, 19);
            this.txtRegion.TabIndex = 56;
            this.txtRegion.DoubleClick += new System.EventHandler(this.txtRegion_DoubleClick);
            this.txtRegion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRegion_KeyDown);
            // 
            // txtArea
            // 
            this.txtArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtArea.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.Location = new System.Drawing.Point(80, 65);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(65, 19);
            this.txtArea.TabIndex = 55;
            this.txtArea.DoubleClick += new System.EventHandler(this.txtArea_DoubleClick);
            this.txtArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtArea_KeyDown);
            // 
            // txtSChanel
            // 
            this.txtSChanel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSChanel.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSChanel.Location = new System.Drawing.Point(80, 45);
            this.txtSChanel.Name = "txtSChanel";
            this.txtSChanel.Size = new System.Drawing.Size(65, 19);
            this.txtSChanel.TabIndex = 54;
            this.txtSChanel.DoubleClick += new System.EventHandler(this.txtSChanel_DoubleClick);
            this.txtSChanel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSChanel_KeyDown);
            // 
            // txtChanel
            // 
            this.txtChanel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtChanel.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanel.Location = new System.Drawing.Point(80, 25);
            this.txtChanel.Name = "txtChanel";
            this.txtChanel.Size = new System.Drawing.Size(65, 19);
            this.txtChanel.TabIndex = 53;
            this.txtChanel.DoubleClick += new System.EventHandler(this.txtChanel_DoubleClick);
            this.txtChanel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChanel_KeyDown);
            // 
            // txtComp
            // 
            this.txtComp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComp.Enabled = false;
            this.txtComp.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComp.Location = new System.Drawing.Point(80, 5);
            this.txtComp.Name = "txtComp";
            this.txtComp.Size = new System.Drawing.Size(65, 19);
            this.txtComp.TabIndex = 52;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 28);
            this.label13.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 51;
            this.label13.Text = "Channel";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 8);
            this.label14.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 50;
            this.label14.Text = "Company";
            // 
            // lblAdj
            // 
            this.lblAdj.AutoSize = true;
            this.lblAdj.ForeColor = System.Drawing.Color.Blue;
            this.lblAdj.Location = new System.Drawing.Point(107, 56);
            this.lblAdj.Name = "lblAdj";
            this.lblAdj.Size = new System.Drawing.Size(0, 13);
            this.lblAdj.TabIndex = 558;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.MidnightBlue;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(2, 276);
            this.label15.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(644, 15);
            this.label15.TabIndex = 559;
            this.label15.Text = "Finalization";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(10, 450);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(48, 13);
            this.label27.TabIndex = 561;
            this.label27.Text = "Remarks";
            // 
            // txtRem
            // 
            this.txtRem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRem.Location = new System.Drawing.Point(106, 448);
            this.txtRem.MaxLength = 100;
            this.txtRem.Name = "txtRem";
            this.txtRem.Size = new System.Drawing.Size(540, 21);
            this.txtRem.TabIndex = 560;
            // 
            // btnFinalize
            // 
            this.btnFinalize.BackColor = System.Drawing.Color.NavajoWhite;
            this.btnFinalize.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnFinalize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnFinalize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnFinalize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalize.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFinalize.Location = new System.Drawing.Point(414, 475);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(75, 23);
            this.btnFinalize.TabIndex = 562;
            this.btnFinalize.Text = "Finalize";
            this.btnFinalize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFinalize.UseVisualStyleBackColor = false;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // txtAmt
            // 
            this.txtAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAmt.Location = new System.Drawing.Point(407, 32);
            this.txtAmt.Name = "txtAmt";
            this.txtAmt.Size = new System.Drawing.Size(118, 21);
            this.txtAmt.TabIndex = 3;
            this.txtAmt.Text = "0";
            this.txtAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmt_KeyPress);
            this.txtAmt.Leave += new System.EventHandler(this.txtAmt_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(313, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 564;
            this.label9.Text = "Amount";
            // 
            // lblStus
            // 
            this.lblStus.AutoSize = true;
            this.lblStus.ForeColor = System.Drawing.Color.Blue;
            this.lblStus.Location = new System.Drawing.Point(215, 9);
            this.lblStus.Name = "lblStus";
            this.lblStus.Size = new System.Drawing.Size(51, 13);
            this.lblStus.TabIndex = 565;
            this.lblStus.Text = "PENDING";
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.NavajoWhite;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.Location = new System.Drawing.Point(450, 98);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 566;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            this.btnClear.Location = new System.Drawing.Point(570, 475);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 567;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.RosyBrown;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancel.Location = new System.Drawing.Point(106, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 22);
            this.btnCancel.TabIndex = 568;
            this.btnCancel.Text = "Cancel Finalize";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblNote
            // 
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(202, 100);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(235, 20);
            this.lblNote.TabIndex = 569;
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
            this.btnSave.Location = new System.Drawing.Point(492, 475);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 570;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CashControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(650, 501);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblStus);
            this.Controls.Add(this.txtAmt);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnFinalize);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.txtRem);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblAdj);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btn_srch_adj);
            this.Controls.Add(this.txtAdjCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAdjRem);
            this.Controls.Add(this.grvAdj);
            this.Controls.Add(this.btnAddAdj);
            this.Controls.Add(this.optCr);
            this.Controls.Add(this.optDr);
            this.Controls.Add(this.btnProfitCenter);
            this.Controls.Add(this.txtAdjPC);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "CashControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cash Control Statement";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CashControl_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grvAdj)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProfitCenter;
        private System.Windows.Forms.TextBox txtAdjPC;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.RadioButton optDr;
        private System.Windows.Forms.RadioButton optCr;
        private System.Windows.Forms.Button btnAddAdj;
        private System.Windows.Forms.DataGridView grvAdj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAdjRem;
        private System.Windows.Forms.Button btn_srch_adj;
        private System.Windows.Forms.TextBox txtAdjCode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtCompAddr;
        private System.Windows.Forms.ListView lstPC;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnClearPC;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.TextBox txtPCDesn;
        private System.Windows.Forms.TextBox txtZoneDesc;
        private System.Windows.Forms.TextBox txtRegDesc;
        private System.Windows.Forms.TextBox txtAreaDesc;
        private System.Windows.Forms.TextBox txtSChnlDesc;
        private System.Windows.Forms.TextBox txtChnlDesc;
        private System.Windows.Forms.TextBox txtCompDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPC;
        private System.Windows.Forms.TextBox txtZone;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.TextBox txtSChanel;
        private System.Windows.Forms.TextBox txtChanel;
        private System.Windows.Forms.TextBox txtComp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblAdj;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtRem;
        private System.Windows.Forms.Button btnFinalize;
        private System.Windows.Forms.TextBox txtAmt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblStus;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rem_pc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rem_sh_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rem_val_final_CR;
        private System.Windows.Forms.DataGridViewTextBoxColumn rem_val_final_DR;
        private System.Windows.Forms.DataGridViewTextBoxColumn rem_rmk;
        private System.Windows.Forms.DataGridViewTextBoxColumn rem_cd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Button btnSave;



    }
}