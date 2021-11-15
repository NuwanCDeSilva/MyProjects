namespace FF.WindowsERPClient.General
{
    partial class ItemTaxStructure
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddstatus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTaxCode = new System.Windows.Forms.ComboBox();
            this.label88 = new System.Windows.Forms.Label();
            this.cmbwStatus = new System.Windows.Forms.ComboBox();
            this.btnSearchreCom = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.gvTax = new System.Windows.Forms.DataGridView();
            this.coldel = new System.Windows.Forms.DataGridViewImageColumn();
            this.colmCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaxtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coltaxrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmStataus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtStruc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnsrhStuc = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTax)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddstatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTax);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbTaxCode);
            this.groupBox1.Controls.Add(this.label88);
            this.groupBox1.Controls.Add(this.cmbwStatus);
            this.groupBox1.Controls.Add(this.btnSearchreCom);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.txtCompany);
            this.groupBox1.Controls.Add(this.gvTax);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(794, 243);
            this.groupBox1.TabIndex = 124;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tax";
            // 
            // btnAddstatus
            // 
            this.btnAddstatus.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.right_arrow_icon;
            this.btnAddstatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddstatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddstatus.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddstatus.Location = new System.Drawing.Point(291, 58);
            this.btnAddstatus.MaximumSize = new System.Drawing.Size(32, 36);
            this.btnAddstatus.Name = "btnAddstatus";
            this.btnAddstatus.Size = new System.Drawing.Size(27, 36);
            this.btnAddstatus.TabIndex = 537;
            this.btnAddstatus.UseVisualStyleBackColor = true;
            this.btnAddstatus.Click += new System.EventHandler(this.btnAddstatus_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 536;
            this.label2.Text = "Tax Rate";
            // 
            // txtTax
            // 
            this.txtTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTax.Location = new System.Drawing.Point(104, 112);
            this.txtTax.MaxLength = 20;
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(112, 21);
            this.txtTax.TabIndex = 535;
            this.txtTax.TextChanged += new System.EventHandler(this.txtTax_TextChanged);
            this.txtTax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTax_KeyPress);
            this.txtTax.Leave += new System.EventHandler(this.txtTax_Leave);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 534;
            this.label1.Text = "Tax Type";
            // 
            // cmbTaxCode
            // 
            this.cmbTaxCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxCode.FormattingEnabled = true;
            this.cmbTaxCode.Items.AddRange(new object[] {
            "Profit",
            "Cost"});
            this.cmbTaxCode.Location = new System.Drawing.Point(104, 85);
            this.cmbTaxCode.Name = "cmbTaxCode";
            this.cmbTaxCode.Size = new System.Drawing.Size(161, 21);
            this.cmbTaxCode.TabIndex = 533;
            this.cmbTaxCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTaxCode_KeyDown);
            // 
            // label88
            // 
            this.label88.Location = new System.Drawing.Point(20, 61);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(78, 13);
            this.label88.TabIndex = 532;
            this.label88.Text = "Item Status";
            // 
            // cmbwStatus
            // 
            this.cmbwStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbwStatus.FormattingEnabled = true;
            this.cmbwStatus.Items.AddRange(new object[] {
            "Profit",
            "Cost"});
            this.cmbwStatus.Location = new System.Drawing.Point(104, 58);
            this.cmbwStatus.Name = "cmbwStatus";
            this.cmbwStatus.Size = new System.Drawing.Size(161, 21);
            this.cmbwStatus.TabIndex = 531;
            this.cmbwStatus.SelectedIndexChanged += new System.EventHandler(this.cmbwStatus_SelectedIndexChanged);
            this.cmbwStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbwStatus_KeyDown);
            // 
            // btnSearchreCom
            // 
            this.btnSearchreCom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchreCom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchreCom.ForeColor = System.Drawing.Color.White;
            this.btnSearchreCom.Image = global::FF.WindowsERPClient.Properties.Resources.searchicon;
            this.btnSearchreCom.Location = new System.Drawing.Point(217, 31);
            this.btnSearchreCom.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearchreCom.Name = "btnSearchreCom";
            this.btnSearchreCom.Size = new System.Drawing.Size(20, 20);
            this.btnSearchreCom.TabIndex = 530;
            this.btnSearchreCom.UseVisualStyleBackColor = true;
            this.btnSearchreCom.Click += new System.EventHandler(this.btnSearchreCom_Click);
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(20, 31);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(78, 13);
            this.label37.TabIndex = 529;
            this.label37.Text = "Company";
            // 
            // txtCompany
            // 
            this.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCompany.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompany.Location = new System.Drawing.Point(104, 31);
            this.txtCompany.MaxLength = 20;
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(112, 21);
            this.txtCompany.TabIndex = 528;
            this.txtCompany.TextChanged += new System.EventHandler(this.txtCompany_TextChanged);
            this.txtCompany.DoubleClick += new System.EventHandler(this.txtCompany_DoubleClick);
            this.txtCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompany_KeyDown);
            this.txtCompany.Leave += new System.EventHandler(this.txtCompany_Leave);
            // 
            // gvTax
            // 
            this.gvTax.AllowUserToAddRows = false;
            this.gvTax.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.SkyBlue;
            this.gvTax.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvTax.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvTax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTax.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvTax.ColumnHeadersHeight = 20;
            this.gvTax.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coldel,
            this.colmCode,
            this.colmDes,
            this.colTaxtype,
            this.coltaxrate,
            this.colmStataus});
            this.gvTax.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTax.DefaultCellStyle = dataGridViewCellStyle7;
            this.gvTax.EnableHeadersVisualStyles = false;
            this.gvTax.GridColor = System.Drawing.Color.White;
            this.gvTax.Location = new System.Drawing.Point(337, 15);
            this.gvTax.MultiSelect = false;
            this.gvTax.Name = "gvTax";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTax.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvTax.RowHeadersVisible = false;
            this.gvTax.RowTemplate.Height = 18;
            this.gvTax.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTax.Size = new System.Drawing.Size(450, 221);
            this.gvTax.TabIndex = 113;
            this.gvTax.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTax_CellClick);
            this.gvTax.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTax_CellContentClick);
            this.gvTax.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTax_CellDoubleClick);
            // 
            // coldel
            // 
            this.coldel.HeaderText = "";
            this.coldel.Image = global::FF.WindowsERPClient.Properties.Resources.deleteicon;
            this.coldel.Name = "coldel";
            this.coldel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.coldel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.coldel.Width = 20;
            // 
            // colmCode
            // 
            this.colmCode.DataPropertyName = "ITS_COM";
            this.colmCode.HeaderText = "Company";
            this.colmCode.Name = "colmCode";
            this.colmCode.ReadOnly = true;
            // 
            // colmDes
            // 
            this.colmDes.DataPropertyName = "ITS_STUS";
            this.colmDes.HeaderText = "Item Status";
            this.colmDes.Name = "colmDes";
            this.colmDes.ReadOnly = true;
            // 
            // colTaxtype
            // 
            this.colTaxtype.DataPropertyName = "ITS_TAX_CD";
            this.colTaxtype.HeaderText = "Tax Type";
            this.colTaxtype.Name = "colTaxtype";
            this.colTaxtype.ReadOnly = true;
            this.colTaxtype.Width = 70;
            // 
            // coltaxrate
            // 
            this.coltaxrate.DataPropertyName = "ITS_TAX_RATE";
            this.coltaxrate.HeaderText = "Tax Rate";
            this.coltaxrate.Name = "coltaxrate";
            this.coltaxrate.ReadOnly = true;
            this.coltaxrate.Width = 70;
            // 
            // colmStataus
            // 
            this.colmStataus.DataPropertyName = "ITS_ACT";
            this.colmStataus.HeaderText = "Status";
            this.colmStataus.Name = "colmStataus";
            this.colmStataus.Width = 50;
            // 
            // txtStruc
            // 
            this.txtStruc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStruc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStruc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStruc.Location = new System.Drawing.Point(116, 6);
            this.txtStruc.MaxLength = 20;
            this.txtStruc.Name = "txtStruc";
            this.txtStruc.Size = new System.Drawing.Size(112, 21);
            this.txtStruc.TabIndex = 529;
            this.txtStruc.TextChanged += new System.EventHandler(this.txtStruc_TextChanged);
            this.txtStruc.DoubleClick += new System.EventHandler(this.txtStruc_DoubleClick);
            this.txtStruc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStruc_KeyDown);
            this.txtStruc.Leave += new System.EventHandler(this.txtStruc_Leave);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(26, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 537;
            this.label3.Text = "Structure Code";
            // 
            // txtDes
            // 
            this.txtDes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDes.Location = new System.Drawing.Point(348, 6);
            this.txtDes.MaxLength = 20;
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new System.Drawing.Size(327, 21);
            this.txtDes.TabIndex = 538;
            this.txtDes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDes_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 539;
            this.label4.Text = "Description";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(747, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(61, 22);
            this.btnClear.TabIndex = 541;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(681, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 22);
            this.btnSave.TabIndex = 540;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnsrhStuc
            // 
            this.btnsrhStuc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsrhStuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsrhStuc.ForeColor = System.Drawing.Color.White;
            this.btnsrhStuc.Image = global::FF.WindowsERPClient.Properties.Resources.searchicon;
            this.btnsrhStuc.Location = new System.Drawing.Point(229, 6);
            this.btnsrhStuc.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnsrhStuc.Name = "btnsrhStuc";
            this.btnsrhStuc.Size = new System.Drawing.Size(20, 20);
            this.btnsrhStuc.TabIndex = 542;
            this.btnsrhStuc.UseVisualStyleBackColor = true;
            this.btnsrhStuc.Click += new System.EventHandler(this.btnsrhStuc_Click);
            // 
            // ItemTaxStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 288);
            this.Controls.Add(this.btnsrhStuc);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStruc);
            this.Controls.Add(this.groupBox1);
            this.Name = "ItemTaxStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Item Tax Structure";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gvTax;
        private System.Windows.Forms.Button btnSearchreCom;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTaxCode;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.ComboBox cmbwStatus;
        private System.Windows.Forms.TextBox txtStruc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnsrhStuc;
        private System.Windows.Forms.Button btnAddstatus;
        private System.Windows.Forms.DataGridViewImageColumn coldel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaxtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn coltaxrate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colmStataus;
    }
}