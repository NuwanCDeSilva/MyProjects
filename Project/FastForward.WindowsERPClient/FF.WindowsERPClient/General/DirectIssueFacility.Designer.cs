namespace FF.WindowsERPClient.General
{
    partial class DirectIssueFacility
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectIssueFacility));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGrant = new System.Windows.Forms.ToolStripButton();
            this.btnSearch_FromLocation = new System.Windows.Forms.Button();
            this.txtFromLoc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch_ToLocation = new System.Windows.Forms.Button();
            this.txtToLoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch_ToCat3 = new System.Windows.Forms.Button();
            this.txtToCat3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gvCurrent = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gvAssign = new System.Windows.Forms.DataGridView();
            this.sclt_to_com = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sclt_to_cat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rlc3_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sclt_allow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstView = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSearch_ToCompany = new System.Windows.Forms.Button();
            this.txtToCompany = new System.Windows.Forms.TextBox();
            this.radLocWise = new System.Windows.Forms.RadioButton();
            this.radCatWise = new System.Windows.Forms.RadioButton();
            this.sclt_frm_com = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sclt_frm_loc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sclt_to_com1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sclt_to_cat1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sclt_allow1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssign)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.toolStripSeparator2,
            this.btnGrant});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(901, 23);
            this.toolStrip1.TabIndex = 94;
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
            this.btnClear.Size = new System.Drawing.Size(60, 18);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnGrant
            // 
            this.btnGrant.AutoSize = false;
            this.btnGrant.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGrant.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGrant.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGrant.Name = "btnGrant";
            this.btnGrant.Size = new System.Drawing.Size(60, 18);
            this.btnGrant.Text = "Grant";
            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
            // 
            // btnSearch_FromLocation
            // 
            this.btnSearch_FromLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_FromLocation.BackgroundImage")));
            this.btnSearch_FromLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_FromLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_FromLocation.ForeColor = System.Drawing.Color.White;
            this.btnSearch_FromLocation.Location = new System.Drawing.Point(51, 43);
            this.btnSearch_FromLocation.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_FromLocation.Name = "btnSearch_FromLocation";
            this.btnSearch_FromLocation.Size = new System.Drawing.Size(20, 20);
            this.btnSearch_FromLocation.TabIndex = 31;
            this.btnSearch_FromLocation.UseVisualStyleBackColor = true;
            this.btnSearch_FromLocation.Click += new System.EventHandler(this.btnSearch_FromLocation_Click);
            // 
            // txtFromLoc
            // 
            this.txtFromLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromLoc.Location = new System.Drawing.Point(3, 43);
            this.txtFromLoc.Name = "txtFromLoc";
            this.txtFromLoc.Size = new System.Drawing.Size(47, 21);
            this.txtFromLoc.TabIndex = 15;
            this.txtFromLoc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFromLoc_KeyUp);
            this.txtFromLoc.Leave += new System.EventHandler(this.txtFromLoc_Leave);
            this.txtFromLoc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtFromLoc_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "From Location";
            // 
            // btnSearch_ToLocation
            // 
            this.btnSearch_ToLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_ToLocation.BackgroundImage")));
            this.btnSearch_ToLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ToLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_ToLocation.ForeColor = System.Drawing.Color.White;
            this.btnSearch_ToLocation.Location = new System.Drawing.Point(188, 43);
            this.btnSearch_ToLocation.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_ToLocation.Name = "btnSearch_ToLocation";
            this.btnSearch_ToLocation.Size = new System.Drawing.Size(20, 20);
            this.btnSearch_ToLocation.TabIndex = 97;
            this.btnSearch_ToLocation.UseVisualStyleBackColor = true;
            this.btnSearch_ToLocation.Click += new System.EventHandler(this.btnSearch_ToLocation_Click);
            // 
            // txtToLoc
            // 
            this.txtToLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToLoc.Location = new System.Drawing.Point(146, 43);
            this.txtToLoc.Name = "txtToLoc";
            this.txtToLoc.Size = new System.Drawing.Size(41, 21);
            this.txtToLoc.TabIndex = 96;
            this.txtToLoc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtToLoc_KeyUp);
            this.txtToLoc.Leave += new System.EventHandler(this.txtToLoc_Leave);
            this.txtToLoc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtToLoc_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 95;
            this.label1.Text = "To Location";
            // 
            // btnSearch_ToCat3
            // 
            this.btnSearch_ToCat3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_ToCat3.BackgroundImage")));
            this.btnSearch_ToCat3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ToCat3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_ToCat3.ForeColor = System.Drawing.Color.White;
            this.btnSearch_ToCat3.Location = new System.Drawing.Point(260, 43);
            this.btnSearch_ToCat3.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_ToCat3.Name = "btnSearch_ToCat3";
            this.btnSearch_ToCat3.Size = new System.Drawing.Size(20, 20);
            this.btnSearch_ToCat3.TabIndex = 100;
            this.btnSearch_ToCat3.UseVisualStyleBackColor = true;
            this.btnSearch_ToCat3.Click += new System.EventHandler(this.btnSearch_ToCat3_Click);
            // 
            // txtToCat3
            // 
            this.txtToCat3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToCat3.Location = new System.Drawing.Point(213, 43);
            this.txtToCat3.Name = "txtToCat3";
            this.txtToCat3.Size = new System.Drawing.Size(46, 21);
            this.txtToCat3.TabIndex = 99;
            this.txtToCat3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtToCat3_KeyUp);
            this.txtToCat3.Leave += new System.EventHandler(this.txtToCat3_Leave);
            this.txtToCat3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtToCat3_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 98;
            this.label2.Text = "To Category";
            // 
            // gvCurrent
            // 
            this.gvCurrent.AllowUserToAddRows = false;
            this.gvCurrent.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.gvCurrent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCurrent.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCurrent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvCurrent.ColumnHeadersHeight = 20;
            this.gvCurrent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sclt_frm_com,
            this.sclt_frm_loc,
            this.sclt_to_com1,
            this.sclt_to_cat1,
            this.sclt_allow1});
            this.gvCurrent.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvCurrent.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvCurrent.EnableHeadersVisualStyles = false;
            this.gvCurrent.GridColor = System.Drawing.Color.White;
            this.gvCurrent.Location = new System.Drawing.Point(455, 39);
            this.gvCurrent.MultiSelect = false;
            this.gvCurrent.Name = "gvCurrent";
            this.gvCurrent.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCurrent.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvCurrent.RowHeadersVisible = false;
            this.gvCurrent.RowTemplate.Height = 18;
            this.gvCurrent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCurrent.Size = new System.Drawing.Size(442, 393);
            this.gvCurrent.TabIndex = 107;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.right_arrow_icon;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Location = new System.Drawing.Point(425, 33);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(29, 27);
            this.btnAdd.TabIndex = 108;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.MidnightBlue;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(4, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(444, 14);
            this.label5.TabIndex = 109;
            this.label5.Text = "Category Allocated Location";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.MidnightBlue;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(448, 14);
            this.label4.TabIndex = 110;
            this.label4.Text = "Assigned Permission";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.MidnightBlue;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(455, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(444, 14);
            this.label6.TabIndex = 112;
            this.label6.Text = "Current Selection";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvAssign
            // 
            this.gvAssign.AllowUserToAddRows = false;
            this.gvAssign.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.SkyBlue;
            this.gvAssign.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvAssign.BackgroundColor = System.Drawing.Color.White;
            this.gvAssign.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAssign.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvAssign.ColumnHeadersHeight = 20;
            this.gvAssign.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sclt_to_com,
            this.sclt_to_cat,
            this.rlc3_desc,
            this.sclt_allow});
            this.gvAssign.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvAssign.DefaultCellStyle = dataGridViewCellStyle7;
            this.gvAssign.EnableHeadersVisualStyles = false;
            this.gvAssign.GridColor = System.Drawing.Color.White;
            this.gvAssign.Location = new System.Drawing.Point(4, 278);
            this.gvAssign.MultiSelect = false;
            this.gvAssign.Name = "gvAssign";
            this.gvAssign.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAssign.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvAssign.RowHeadersVisible = false;
            this.gvAssign.RowTemplate.Height = 18;
            this.gvAssign.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvAssign.Size = new System.Drawing.Size(446, 152);
            this.gvAssign.TabIndex = 111;
            // 
            // sclt_to_com
            // 
            this.sclt_to_com.DataPropertyName = "sclt_to_com";
            this.sclt_to_com.HeaderText = "To Company";
            this.sclt_to_com.Name = "sclt_to_com";
            this.sclt_to_com.ReadOnly = true;
            this.sclt_to_com.Width = 75;
            // 
            // sclt_to_cat
            // 
            this.sclt_to_cat.DataPropertyName = "sclt_to_cat";
            this.sclt_to_cat.HeaderText = "Category";
            this.sclt_to_cat.Name = "sclt_to_cat";
            this.sclt_to_cat.ReadOnly = true;
            this.sclt_to_cat.Width = 75;
            // 
            // rlc3_desc
            // 
            this.rlc3_desc.DataPropertyName = "rlc3_desc";
            this.rlc3_desc.HeaderText = "Description";
            this.rlc3_desc.Name = "rlc3_desc";
            this.rlc3_desc.ReadOnly = true;
            this.rlc3_desc.Width = 175;
            // 
            // sclt_allow
            // 
            this.sclt_allow.DataPropertyName = "sclt_allow";
            this.sclt_allow.HeaderText = "Status";
            this.sclt_allow.Name = "sclt_allow";
            this.sclt_allow.ReadOnly = true;
            // 
            // lstView
            // 
            this.lstView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstView.LargeImageList = this.imageList1;
            this.lstView.Location = new System.Drawing.Point(4, 82);
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(444, 173);
            this.lstView.TabIndex = 113;
            this.lstView.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "deliveryicon.png");
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkActive.Location = new System.Drawing.Point(282, 45);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(53, 17);
            this.chkActive.TabIndex = 114;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 115;
            this.label7.Text = "To Company";
            // 
            // btnSearch_ToCompany
            // 
            this.btnSearch_ToCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_ToCompany.BackgroundImage")));
            this.btnSearch_ToCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ToCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_ToCompany.ForeColor = System.Drawing.Color.White;
            this.btnSearch_ToCompany.Location = new System.Drawing.Point(122, 43);
            this.btnSearch_ToCompany.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_ToCompany.Name = "btnSearch_ToCompany";
            this.btnSearch_ToCompany.Size = new System.Drawing.Size(20, 20);
            this.btnSearch_ToCompany.TabIndex = 117;
            this.btnSearch_ToCompany.UseVisualStyleBackColor = true;
            this.btnSearch_ToCompany.Click += new System.EventHandler(this.btnSearch_ToCompany_Click);
            // 
            // txtToCompany
            // 
            this.txtToCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToCompany.Location = new System.Drawing.Point(77, 43);
            this.txtToCompany.Name = "txtToCompany";
            this.txtToCompany.Size = new System.Drawing.Size(44, 21);
            this.txtToCompany.TabIndex = 116;
            this.txtToCompany.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtToCompany_KeyUp);
            this.txtToCompany.Leave += new System.EventHandler(this.txtToCompany_Leave);
            this.txtToCompany.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtToCompany_MouseDoubleClick);
            // 
            // radLocWise
            // 
            this.radLocWise.AutoSize = true;
            this.radLocWise.BackColor = System.Drawing.Color.White;
            this.radLocWise.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.radLocWise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radLocWise.ForeColor = System.Drawing.Color.Purple;
            this.radLocWise.Location = new System.Drawing.Point(332, 25);
            this.radLocWise.Name = "radLocWise";
            this.radLocWise.Size = new System.Drawing.Size(93, 17);
            this.radLocWise.TabIndex = 118;
            this.radLocWise.Text = "LocationWise  ";
            this.radLocWise.UseVisualStyleBackColor = false;
            // 
            // radCatWise
            // 
            this.radCatWise.AutoSize = true;
            this.radCatWise.BackColor = System.Drawing.Color.White;
            this.radCatWise.Checked = true;
            this.radCatWise.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.radCatWise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radCatWise.ForeColor = System.Drawing.Color.Indigo;
            this.radCatWise.Location = new System.Drawing.Point(332, 48);
            this.radCatWise.Name = "radCatWise";
            this.radCatWise.Size = new System.Drawing.Size(92, 17);
            this.radCatWise.TabIndex = 119;
            this.radCatWise.TabStop = true;
            this.radCatWise.Text = "CategoryWise";
            this.radCatWise.UseVisualStyleBackColor = false;
            // 
            // sclt_frm_com
            // 
            this.sclt_frm_com.DataPropertyName = "sclt_frm_com";
            this.sclt_frm_com.HeaderText = "sclt_frm_com";
            this.sclt_frm_com.Name = "sclt_frm_com";
            this.sclt_frm_com.ReadOnly = true;
            this.sclt_frm_com.Visible = false;
            // 
            // sclt_frm_loc
            // 
            this.sclt_frm_loc.DataPropertyName = "sclt_frm_loc";
            this.sclt_frm_loc.HeaderText = "From Location";
            this.sclt_frm_loc.Name = "sclt_frm_loc";
            this.sclt_frm_loc.ReadOnly = true;
            // 
            // sclt_to_com1
            // 
            this.sclt_to_com1.DataPropertyName = "sclt_to_com";
            this.sclt_to_com1.HeaderText = "To Company";
            this.sclt_to_com1.Name = "sclt_to_com1";
            this.sclt_to_com1.ReadOnly = true;
            // 
            // sclt_to_cat1
            // 
            this.sclt_to_cat1.DataPropertyName = "sclt_to_cat";
            this.sclt_to_cat1.HeaderText = "To Cat/ Loc";
            this.sclt_to_cat1.Name = "sclt_to_cat1";
            this.sclt_to_cat1.ReadOnly = true;
            // 
            // sclt_allow1
            // 
            this.sclt_allow1.DataPropertyName = "sclt_allow";
            this.sclt_allow1.HeaderText = "Status";
            this.sclt_allow1.Name = "sclt_allow1";
            this.sclt_allow1.ReadOnly = true;
            this.sclt_allow1.Width = 120;
            // 
            // DirectIssueFacility
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(901, 436);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.radCatWise);
            this.Controls.Add(this.radLocWise);
            this.Controls.Add(this.btnSearch_ToCompany);
            this.Controls.Add(this.txtToCompany);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.lstView);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gvAssign);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSearch_ToCat3);
            this.Controls.Add(this.txtToCat3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch_ToLocation);
            this.Controls.Add(this.txtToLoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch_FromLocation);
            this.Controls.Add(this.txtFromLoc);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gvCurrent);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DirectIssueFacility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Direct Issue Permission";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssign)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnGrant;
        private System.Windows.Forms.Button btnSearch_FromLocation;
        private System.Windows.Forms.TextBox txtFromLoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch_ToLocation;
        private System.Windows.Forms.TextBox txtToLoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch_ToCat3;
        private System.Windows.Forms.TextBox txtToCat3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gvCurrent;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView gvAssign;
        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSearch_ToCompany;
        private System.Windows.Forms.TextBox txtToCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn sclt_to_com;
        private System.Windows.Forms.DataGridViewTextBoxColumn sclt_to_cat;
        private System.Windows.Forms.DataGridViewTextBoxColumn rlc3_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn sclt_allow;
        private System.Windows.Forms.RadioButton radLocWise;
        private System.Windows.Forms.RadioButton radCatWise;
        private System.Windows.Forms.DataGridViewTextBoxColumn sclt_frm_com;
        private System.Windows.Forms.DataGridViewTextBoxColumn sclt_frm_loc;
        private System.Windows.Forms.DataGridViewTextBoxColumn sclt_to_com1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sclt_to_cat1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sclt_allow1;
    }
}
