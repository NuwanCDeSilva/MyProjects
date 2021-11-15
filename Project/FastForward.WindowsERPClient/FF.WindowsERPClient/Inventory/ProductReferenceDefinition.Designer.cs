namespace FF.WindowsERPClient.Inventory
{
    partial class ProductReferenceDefinition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductReferenceDefinition));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvProductRef = new System.Windows.Forms.DataGridView();
            this.btnAddtogrid = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExeclUpload = new System.Windows.Forms.TextBox();
            this.btnSearchExclpath = new System.Windows.Forms.Button();
            this.btn_Srch_Cat3 = new System.Windows.Forms.Button();
            this.btn_Srch_Cat2 = new System.Windows.Forms.Button();
            this.btn_Srch_Cat1 = new System.Windows.Forms.Button();
            this.btn_Srch_Code = new System.Windows.Forms.Button();
            this.chkIntSetup = new System.Windows.Forms.CheckBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.txtCat3 = new System.Windows.Forms.TextBox();
            this.lblCat3 = new System.Windows.Forms.Label();
            this.txtCat2 = new System.Windows.Forms.TextBox();
            this.lblCat2 = new System.Windows.Forms.Label();
            this.txtCat1 = new System.Windows.Forms.TextBox();
            this.lblCat1 = new System.Windows.Forms.Label();
            this.txtCharge = new System.Windows.Forms.TextBox();
            this.lblCharge = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCharge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCat1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCat2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCat3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIntsetup = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductRef)).BeginInit();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvProductRef);
            this.panel1.Controls.Add(this.btnAddtogrid);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btn_Srch_Cat3);
            this.panel1.Controls.Add(this.btn_Srch_Cat2);
            this.panel1.Controls.Add(this.btn_Srch_Cat1);
            this.panel1.Controls.Add(this.btn_Srch_Code);
            this.panel1.Controls.Add(this.chkIntSetup);
            this.panel1.Controls.Add(this.chkIsActive);
            this.panel1.Controls.Add(this.txtCat3);
            this.panel1.Controls.Add(this.lblCat3);
            this.panel1.Controls.Add(this.txtCat2);
            this.panel1.Controls.Add(this.lblCat2);
            this.panel1.Controls.Add(this.txtCat1);
            this.panel1.Controls.Add(this.lblCat1);
            this.panel1.Controls.Add(this.txtCharge);
            this.panel1.Controls.Add(this.lblCharge);
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.lblCode);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 471);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dgvProductRef
            // 
            this.dgvProductRef.AllowUserToAddRows = false;
            this.dgvProductRef.AllowUserToDeleteRows = false;
            this.dgvProductRef.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductRef.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colDescription,
            this.colCharge,
            this.colCat1,
            this.colCat2,
            this.colCat3,
            this.ColActive,
            this.colIntsetup});
            this.dgvProductRef.Location = new System.Drawing.Point(29, 294);
            this.dgvProductRef.Name = "dgvProductRef";
            this.dgvProductRef.RowHeadersVisible = false;
            this.dgvProductRef.Size = new System.Drawing.Size(812, 163);
            this.dgvProductRef.TabIndex = 149;
            this.dgvProductRef.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductRef_CellContentClick);
            this.dgvProductRef.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductRef_CellValueChanged);
            this.dgvProductRef.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvProductRef_CurrentCellDirtyStateChanged);
            // 
            // btnAddtogrid
            // 
            this.btnAddtogrid.BackColor = System.Drawing.Color.Transparent;
            this.btnAddtogrid.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.downloadarrowicon;
            this.btnAddtogrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddtogrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddtogrid.Location = new System.Drawing.Point(442, 168);
            this.btnAddtogrid.Name = "btnAddtogrid";
            this.btnAddtogrid.Size = new System.Drawing.Size(75, 34);
            this.btnAddtogrid.TabIndex = 146;
            this.btnAddtogrid.UseVisualStyleBackColor = false;
            this.btnAddtogrid.Click += new System.EventHandler(this.btnAddtogrid_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUpload);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtExeclUpload);
            this.panel2.Controls.Add(this.btnSearchExclpath);
            this.panel2.Location = new System.Drawing.Point(104, 228);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 58);
            this.panel2.TabIndex = 144;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(338, 29);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 275;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 274;
            this.label2.Text = "File Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 273;
            this.label1.Text = "Upload Execl";
            // 
            // txtExeclUpload
            // 
            this.txtExeclUpload.Location = new System.Drawing.Point(57, 32);
            this.txtExeclUpload.Name = "txtExeclUpload";
            this.txtExeclUpload.Size = new System.Drawing.Size(230, 20);
            this.txtExeclUpload.TabIndex = 272;
            // 
            // btnSearchExclpath
            // 
            this.btnSearchExclpath.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.specialsearch2icon;
            this.btnSearchExclpath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchExclpath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchExclpath.ForeColor = System.Drawing.Color.Lavender;
            this.btnSearchExclpath.Location = new System.Drawing.Point(300, 30);
            this.btnSearchExclpath.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearchExclpath.Name = "btnSearchExclpath";
            this.btnSearchExclpath.Size = new System.Drawing.Size(25, 23);
            this.btnSearchExclpath.TabIndex = 271;
            this.btnSearchExclpath.UseVisualStyleBackColor = true;
            this.btnSearchExclpath.Click += new System.EventHandler(this.btnSearchExclpath_Click);
            // 
            // btn_Srch_Cat3
            // 
            this.btn_Srch_Cat3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Srch_Cat3.BackgroundImage")));
            this.btn_Srch_Cat3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Srch_Cat3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Srch_Cat3.ForeColor = System.Drawing.Color.White;
            this.btn_Srch_Cat3.Location = new System.Drawing.Point(287, 179);
            this.btn_Srch_Cat3.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_Srch_Cat3.Name = "btn_Srch_Cat3";
            this.btn_Srch_Cat3.Size = new System.Drawing.Size(20, 20);
            this.btn_Srch_Cat3.TabIndex = 143;
            this.btn_Srch_Cat3.UseVisualStyleBackColor = true;
            this.btn_Srch_Cat3.Click += new System.EventHandler(this.btn_Srch_Cat3_Click);
            // 
            // btn_Srch_Cat2
            // 
            this.btn_Srch_Cat2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Srch_Cat2.BackgroundImage")));
            this.btn_Srch_Cat2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Srch_Cat2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Srch_Cat2.ForeColor = System.Drawing.Color.White;
            this.btn_Srch_Cat2.Location = new System.Drawing.Point(287, 152);
            this.btn_Srch_Cat2.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_Srch_Cat2.Name = "btn_Srch_Cat2";
            this.btn_Srch_Cat2.Size = new System.Drawing.Size(20, 20);
            this.btn_Srch_Cat2.TabIndex = 142;
            this.btn_Srch_Cat2.UseVisualStyleBackColor = true;
            this.btn_Srch_Cat2.Click += new System.EventHandler(this.btn_Srch_Cat2_Click);
            // 
            // btn_Srch_Cat1
            // 
            this.btn_Srch_Cat1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Srch_Cat1.BackgroundImage")));
            this.btn_Srch_Cat1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Srch_Cat1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Srch_Cat1.ForeColor = System.Drawing.Color.White;
            this.btn_Srch_Cat1.Location = new System.Drawing.Point(287, 127);
            this.btn_Srch_Cat1.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_Srch_Cat1.Name = "btn_Srch_Cat1";
            this.btn_Srch_Cat1.Size = new System.Drawing.Size(20, 20);
            this.btn_Srch_Cat1.TabIndex = 141;
            this.btn_Srch_Cat1.UseVisualStyleBackColor = true;
            this.btn_Srch_Cat1.Click += new System.EventHandler(this.btn_Srch_Cat1_Click);
            // 
            // btn_Srch_Code
            // 
            this.btn_Srch_Code.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Srch_Code.BackgroundImage")));
            this.btn_Srch_Code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Srch_Code.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Srch_Code.ForeColor = System.Drawing.Color.White;
            this.btn_Srch_Code.Location = new System.Drawing.Point(287, 23);
            this.btn_Srch_Code.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btn_Srch_Code.Name = "btn_Srch_Code";
            this.btn_Srch_Code.Size = new System.Drawing.Size(20, 20);
            this.btn_Srch_Code.TabIndex = 140;
            this.btn_Srch_Code.UseVisualStyleBackColor = true;
            this.btn_Srch_Code.Click += new System.EventHandler(this.btn_Srch_Code_Click);
            // 
            // chkIntSetup
            // 
            this.chkIntSetup.AutoSize = true;
            this.chkIntSetup.Checked = true;
            this.chkIntSetup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntSetup.Location = new System.Drawing.Point(205, 205);
            this.chkIntSetup.Name = "chkIntSetup";
            this.chkIntSetup.Size = new System.Drawing.Size(79, 17);
            this.chkIntSetup.TabIndex = 13;
            this.chkIntSetup.Text = "Intial Setup";
            this.chkIntSetup.UseVisualStyleBackColor = true;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(104, 205);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(67, 17);
            this.chkIsActive.TabIndex = 12;
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtCat3
            // 
            this.txtCat3.Location = new System.Drawing.Point(104, 179);
            this.txtCat3.Name = "txtCat3";
            this.txtCat3.Size = new System.Drawing.Size(180, 20);
            this.txtCat3.TabIndex = 11;
            this.txtCat3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCat3_KeyDown);
            this.txtCat3.Leave += new System.EventHandler(this.txtCat3_Leave);
            this.txtCat3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtCat3_MouseDoubleClick);
            // 
            // lblCat3
            // 
            this.lblCat3.AutoSize = true;
            this.lblCat3.Location = new System.Drawing.Point(26, 179);
            this.lblCat3.Name = "lblCat3";
            this.lblCat3.Size = new System.Drawing.Size(58, 13);
            this.lblCat3.TabIndex = 10;
            this.lblCat3.Text = "Category 3";
            // 
            // txtCat2
            // 
            this.txtCat2.Location = new System.Drawing.Point(104, 153);
            this.txtCat2.Name = "txtCat2";
            this.txtCat2.Size = new System.Drawing.Size(180, 20);
            this.txtCat2.TabIndex = 9;
            this.txtCat2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCat2_KeyDown);
            this.txtCat2.Leave += new System.EventHandler(this.txtCat2_Leave);
            this.txtCat2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtCat2_MouseDoubleClick);
            // 
            // lblCat2
            // 
            this.lblCat2.AutoSize = true;
            this.lblCat2.Location = new System.Drawing.Point(26, 153);
            this.lblCat2.Name = "lblCat2";
            this.lblCat2.Size = new System.Drawing.Size(58, 13);
            this.lblCat2.TabIndex = 8;
            this.lblCat2.Text = "Category 2";
            // 
            // txtCat1
            // 
            this.txtCat1.Location = new System.Drawing.Point(104, 127);
            this.txtCat1.Name = "txtCat1";
            this.txtCat1.Size = new System.Drawing.Size(180, 20);
            this.txtCat1.TabIndex = 7;
            this.txtCat1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCat1_KeyDown);
            this.txtCat1.Leave += new System.EventHandler(this.txtCat1_Leave);
            this.txtCat1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtCat1_MouseDoubleClick);
            // 
            // lblCat1
            // 
            this.lblCat1.AutoSize = true;
            this.lblCat1.Location = new System.Drawing.Point(26, 127);
            this.lblCat1.Name = "lblCat1";
            this.lblCat1.Size = new System.Drawing.Size(58, 13);
            this.lblCat1.TabIndex = 6;
            this.lblCat1.Text = "Category 1";
            // 
            // txtCharge
            // 
            this.txtCharge.Location = new System.Drawing.Point(104, 101);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.Size = new System.Drawing.Size(180, 20);
            this.txtCharge.TabIndex = 5;
            this.txtCharge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCharge_KeyDown);
            this.txtCharge.Leave += new System.EventHandler(this.txtCharge_Leave);
            // 
            // lblCharge
            // 
            this.lblCharge.AutoSize = true;
            this.lblCharge.Location = new System.Drawing.Point(26, 101);
            this.lblCharge.Name = "lblCharge";
            this.lblCharge.Size = new System.Drawing.Size(41, 13);
            this.lblCharge.TabIndex = 4;
            this.lblCharge.Text = "Charge";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(104, 49);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(180, 46);
            this.txtDescription.TabIndex = 3;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(104, 23);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(180, 20);
            this.txtCode.TabIndex = 2;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            this.txtCode.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtCode_MouseDoubleClick);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(26, 52);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(26, 23);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(32, 13);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Code";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnView,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(925, 25);
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
            this.btnClear.Size = new System.Drawing.Size(60, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
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
            this.btnView.Size = new System.Drawing.Size(60, 22);
            this.btnView.Text = "View";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
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
            this.btnSave.Size = new System.Drawing.Size(60, 22);
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "Code";
            this.colCode.HeaderText = "Code";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colCharge
            // 
            this.colCharge.DataPropertyName = "Charge";
            this.colCharge.HeaderText = "Charge";
            this.colCharge.Name = "colCharge";
            this.colCharge.ReadOnly = true;
            // 
            // colCat1
            // 
            this.colCat1.DataPropertyName = "itemCat1";
            this.colCat1.HeaderText = "Category1";
            this.colCat1.Name = "colCat1";
            this.colCat1.ReadOnly = true;
            // 
            // colCat2
            // 
            this.colCat2.DataPropertyName = "itemCat2";
            this.colCat2.HeaderText = "Category2";
            this.colCat2.Name = "colCat2";
            this.colCat2.ReadOnly = true;
            // 
            // colCat3
            // 
            this.colCat3.DataPropertyName = "itemCat3";
            this.colCat3.HeaderText = "Category3";
            this.colCat3.Name = "colCat3";
            this.colCat3.ReadOnly = true;
            // 
            // ColActive
            // 
            this.ColActive.DataPropertyName = "isactive";
            this.ColActive.FalseValue = "0";
            this.ColActive.HeaderText = "Is Active";
            this.ColActive.Name = "ColActive";
            this.ColActive.TrueValue = "1";
            // 
            // colIntsetup
            // 
            this.colIntsetup.DataPropertyName = "intialsetup";
            this.colIntsetup.FalseValue = "0";
            this.colIntsetup.HeaderText = "Intial Setup";
            this.colIntsetup.Name = "colIntsetup";
            this.colIntsetup.TrueValue = "1";
            // 
            // ProductReferenceDefinition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 497);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "ProductReferenceDefinition";
            this.Text = "Product Condition Definitions";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductRef)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.CheckBox chkIntSetup;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.TextBox txtCat3;
        private System.Windows.Forms.Label lblCat3;
        private System.Windows.Forms.TextBox txtCat2;
        private System.Windows.Forms.Label lblCat2;
        private System.Windows.Forms.TextBox txtCat1;
        private System.Windows.Forms.Label lblCat1;
        private System.Windows.Forms.TextBox txtCharge;
        private System.Windows.Forms.Label lblCharge;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btn_Srch_Cat3;
        private System.Windows.Forms.Button btn_Srch_Cat2;
        private System.Windows.Forms.Button btn_Srch_Cat1;
        private System.Windows.Forms.Button btn_Srch_Code;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAddtogrid;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExeclUpload;
        private System.Windows.Forms.Button btnSearchExclpath;
        private System.Windows.Forms.DataGridView dgvProductRef;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCharge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCat1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCat2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCat3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIntsetup;
    }
}