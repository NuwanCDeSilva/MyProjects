namespace FF.WindowsERPClient.General
{
    partial class ServiceAgentDefinition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceAgentDefinition));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSearch_ServiceAgent = new System.Windows.Forms.Button();
            this.txtAgent = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAdd1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAdd2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTown = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtContPerson = new System.Windows.Forms.TextBox();
            this.txtCordinator = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtMappedLoc = new System.Windows.Forms.TextBox();
            this.btnSearch_Town = new System.Windows.Forms.Button();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblMapped = new System.Windows.Forms.Label();
            this.btnSearch_MappedLoc = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gvUserPC = new System.Windows.Forms.DataGridView();
            this.chkDelPc = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.p_SUP_PC_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ml_loc_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_DelPC = new System.Windows.Forms.Button();
            this.btn_AddPC = new System.Windows.Forms.Button();
            this.ucLoactionSearch1 = new FF.WindowsERPClient.UserControls.ucLoactionSearch();
            this.grvLocs = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LOCATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOC_DESCRIPTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddLocs = new System.Windows.Forms.Button();
            this.chkAll_Itm = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserPC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLocs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch_ServiceAgent
            // 
            this.btnSearch_ServiceAgent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_ServiceAgent.BackgroundImage")));
            this.btnSearch_ServiceAgent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ServiceAgent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_ServiceAgent.ForeColor = System.Drawing.Color.White;
            this.btnSearch_ServiceAgent.Location = new System.Drawing.Point(187, 54);
            this.btnSearch_ServiceAgent.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_ServiceAgent.Name = "btnSearch_ServiceAgent";
            this.btnSearch_ServiceAgent.Size = new System.Drawing.Size(20, 19);
            this.btnSearch_ServiceAgent.TabIndex = 46;
            this.btnSearch_ServiceAgent.UseVisualStyleBackColor = true;
            this.btnSearch_ServiceAgent.Click += new System.EventHandler(this.btnSearch_ServiceAgent_Click);
            // 
            // txtAgent
            // 
            this.txtAgent.BackColor = System.Drawing.Color.White;
            this.txtAgent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAgent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAgent.Location = new System.Drawing.Point(95, 53);
            this.txtAgent.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtAgent.MaxLength = 10;
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Size = new System.Drawing.Size(91, 21);
            this.txtAgent.TabIndex = 45;
            this.txtAgent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAgent_KeyUp);
            this.txtAgent.Leave += new System.EventHandler(this.txtAgent_Leave);
            this.txtAgent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtAgent_MouseDoubleClick);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Location = new System.Drawing.Point(6, 57);
            this.label35.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(36, 13);
            this.label35.TabIndex = 44;
            this.label35.Text = "Agent";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Location = new System.Drawing.Point(390, 53);
            this.txtName.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(382, 21);
            this.txtName.TabIndex = 48;
            this.txtName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(353, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Name";
            // 
            // txtAdd1
            // 
            this.txtAdd1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdd1.Location = new System.Drawing.Point(95, 77);
            this.txtAdd1.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtAdd1.MaxLength = 50;
            this.txtAdd1.Name = "txtAdd1";
            this.txtAdd1.Size = new System.Drawing.Size(677, 21);
            this.txtAdd1.TabIndex = 50;
            this.txtAdd1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAdd1_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Address I";
            // 
            // txtAdd2
            // 
            this.txtAdd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdd2.Location = new System.Drawing.Point(95, 101);
            this.txtAdd2.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtAdd2.MaxLength = 50;
            this.txtAdd2.Name = "txtAdd2";
            this.txtAdd2.Size = new System.Drawing.Size(677, 21);
            this.txtAdd2.TabIndex = 52;
            this.txtAdd2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAdd2_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(6, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Address II";
            // 
            // txtTel
            // 
            this.txtTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTel.Location = new System.Drawing.Point(95, 125);
            this.txtTel.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtTel.MaxLength = 10;
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(112, 21);
            this.txtTel.TabIndex = 56;
            this.txtTel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTel_KeyUp);
            this.txtTel.Leave += new System.EventHandler(this.txtTel_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(6, 129);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Telephone";
            // 
            // txtFax
            // 
            this.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFax.Location = new System.Drawing.Point(351, 125);
            this.txtFax.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtFax.MaxLength = 10;
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(127, 21);
            this.txtFax.TabIndex = 58;
            this.txtFax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFax_KeyUp);
            this.txtFax.Leave += new System.EventHandler(this.txtFax_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(321, 129);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Fax";
            // 
            // txtTown
            // 
            this.txtTown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTown.Location = new System.Drawing.Point(610, 125);
            this.txtTown.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtTown.MaxLength = 20;
            this.txtTown.Name = "txtTown";
            this.txtTown.Size = new System.Drawing.Size(137, 21);
            this.txtTown.TabIndex = 60;
            this.txtTown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTown_KeyUp);
            this.txtTown.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtTown_MouseDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(572, 129);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Town";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(95, 149);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(216, 21);
            this.cmbCategory.TabIndex = 61;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            this.cmbCategory.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbCategory_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(6, 153);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 62;
            this.label8.Text = "Category";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(6, 177);
            this.label9.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Contact Person";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(431, 177);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 64;
            this.label10.Text = "Cordinator";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(39, 199);
            this.label12.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 66;
            this.label12.Text = "Remarks";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(6, 243);
            this.label13.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 67;
            this.label13.Text = "Mapped Location";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.toolStripSeparator2,
            this.btnSave,
            this.toolStripSeparator1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(778, 23);
            this.toolStrip1.TabIndex = 68;
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
            this.btnClear.Size = new System.Drawing.Size(60, 20);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 20);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // txtContPerson
            // 
            this.txtContPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContPerson.Location = new System.Drawing.Point(95, 173);
            this.txtContPerson.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtContPerson.MaxLength = 50;
            this.txtContPerson.Name = "txtContPerson";
            this.txtContPerson.Size = new System.Drawing.Size(297, 21);
            this.txtContPerson.TabIndex = 69;
            this.txtContPerson.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtContPerson_KeyUp);
            // 
            // txtCordinator
            // 
            this.txtCordinator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCordinator.Location = new System.Drawing.Point(493, 173);
            this.txtCordinator.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtCordinator.MaxLength = 50;
            this.txtCordinator.Name = "txtCordinator";
            this.txtCordinator.Size = new System.Drawing.Size(279, 21);
            this.txtCordinator.TabIndex = 70;
            this.txtCordinator.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCordinator_KeyUp);
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Location = new System.Drawing.Point(95, 197);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtRemarks.MaxLength = 50;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(677, 39);
            this.txtRemarks.TabIndex = 71;
            this.txtRemarks.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRemarks_KeyUp);
            // 
            // txtMappedLoc
            // 
            this.txtMappedLoc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMappedLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMappedLoc.Location = new System.Drawing.Point(95, 239);
            this.txtMappedLoc.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtMappedLoc.MaxLength = 10;
            this.txtMappedLoc.Name = "txtMappedLoc";
            this.txtMappedLoc.ReadOnly = true;
            this.txtMappedLoc.Size = new System.Drawing.Size(120, 21);
            this.txtMappedLoc.TabIndex = 72;
            this.txtMappedLoc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMappedLoc_KeyUp);
            this.txtMappedLoc.Leave += new System.EventHandler(this.txtMappedLoc_Leave);
            this.txtMappedLoc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtMappedLoc_MouseDoubleClick);
            // 
            // btnSearch_Town
            // 
            this.btnSearch_Town.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_Town.BackgroundImage")));
            this.btnSearch_Town.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_Town.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_Town.ForeColor = System.Drawing.Color.White;
            this.btnSearch_Town.Location = new System.Drawing.Point(748, 127);
            this.btnSearch_Town.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_Town.Name = "btnSearch_Town";
            this.btnSearch_Town.Size = new System.Drawing.Size(20, 19);
            this.btnSearch_Town.TabIndex = 73;
            this.btnSearch_Town.UseVisualStyleBackColor = true;
            this.btnSearch_Town.Click += new System.EventHandler(this.btnSearch_Town_Click);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkActive.Location = new System.Drawing.Point(244, 55);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(53, 17);
            this.chkActive.TabIndex = 75;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // lblMapped
            // 
            this.lblMapped.BackColor = System.Drawing.Color.Transparent;
            this.lblMapped.Location = new System.Drawing.Point(246, 243);
            this.lblMapped.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblMapped.Name = "lblMapped";
            this.lblMapped.Size = new System.Drawing.Size(526, 13);
            this.lblMapped.TabIndex = 76;
            // 
            // btnSearch_MappedLoc
            // 
            this.btnSearch_MappedLoc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_MappedLoc.BackgroundImage")));
            this.btnSearch_MappedLoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_MappedLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_MappedLoc.ForeColor = System.Drawing.Color.White;
            this.btnSearch_MappedLoc.Location = new System.Drawing.Point(216, 240);
            this.btnSearch_MappedLoc.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_MappedLoc.Name = "btnSearch_MappedLoc";
            this.btnSearch_MappedLoc.Size = new System.Drawing.Size(20, 19);
            this.btnSearch_MappedLoc.TabIndex = 77;
            this.btnSearch_MappedLoc.UseVisualStyleBackColor = true;
            this.btnSearch_MappedLoc.Click += new System.EventHandler(this.btnSearch_MappedLoc_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Navy;
            this.label11.ForeColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(6, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(766, 18);
            this.label11.TabIndex = 416;
            this.label11.Text = "RCC Agent Details";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Navy;
            this.label4.ForeColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(6, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(766, 18);
            this.label4.TabIndex = 417;
            this.label4.Text = "Assign Locations";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvUserPC
            // 
            this.gvUserPC.AllowUserToAddRows = false;
            this.gvUserPC.AllowUserToResizeRows = false;
            this.gvUserPC.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvUserPC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvUserPC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvUserPC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkDelPc,
            this.p_SUP_PC_CD,
            this.ml_loc_desc});
            this.gvUserPC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gvUserPC.EnableHeadersVisualStyles = false;
            this.gvUserPC.Location = new System.Drawing.Point(401, 434);
            this.gvUserPC.Name = "gvUserPC";
            this.gvUserPC.RowHeadersVisible = false;
            this.gvUserPC.RowHeadersWidth = 15;
            this.gvUserPC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvUserPC.Size = new System.Drawing.Size(373, 174);
            this.gvUserPC.TabIndex = 418;
            // 
            // chkDelPc
            // 
            this.chkDelPc.HeaderText = "";
            this.chkDelPc.Name = "chkDelPc";
            this.chkDelPc.Width = 25;
            // 
            // p_SUP_PC_CD
            // 
            this.p_SUP_PC_CD.DataPropertyName = "ragl_loc";
            this.p_SUP_PC_CD.HeaderText = "Location";
            this.p_SUP_PC_CD.Name = "p_SUP_PC_CD";
            this.p_SUP_PC_CD.ReadOnly = true;
            // 
            // ml_loc_desc
            // 
            this.ml_loc_desc.DataPropertyName = "ml_loc_desc";
            this.ml_loc_desc.HeaderText = "Description";
            this.ml_loc_desc.Name = "ml_loc_desc";
            this.ml_loc_desc.ReadOnly = true;
            this.ml_loc_desc.Width = 225;
            // 
            // btn_DelPC
            // 
            this.btn_DelPC.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_DelPC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DelPC.Location = new System.Drawing.Point(713, 405);
            this.btn_DelPC.Name = "btn_DelPC";
            this.btn_DelPC.Size = new System.Drawing.Size(61, 23);
            this.btn_DelPC.TabIndex = 420;
            this.btn_DelPC.Text = "Delete";
            this.btn_DelPC.UseVisualStyleBackColor = false;
            this.btn_DelPC.Click += new System.EventHandler(this.btn_DelPC_Click);
            // 
            // btn_AddPC
            // 
            this.btn_AddPC.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_AddPC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddPC.Location = new System.Drawing.Point(646, 405);
            this.btn_AddPC.Name = "btn_AddPC";
            this.btn_AddPC.Size = new System.Drawing.Size(61, 23);
            this.btn_AddPC.TabIndex = 419;
            this.btn_AddPC.Text = "Add New";
            this.btn_AddPC.UseVisualStyleBackColor = false;
            this.btn_AddPC.Click += new System.EventHandler(this.btn_AddPC_Click);
            // 
            // ucLoactionSearch1
            // 
            this.ucLoactionSearch1.Area = "";
            this.ucLoactionSearch1.BackColor = System.Drawing.Color.White;
            this.ucLoactionSearch1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucLoactionSearch1.Channel = "";
            this.ucLoactionSearch1.Company = "";
            this.ucLoactionSearch1.CompanyDes = "";
            this.ucLoactionSearch1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucLoactionSearch1.Location = new System.Drawing.Point(6, 285);
            this.ucLoactionSearch1.Name = "ucLoactionSearch1";
            this.ucLoactionSearch1.ProfitCenter = "";
            this.ucLoactionSearch1.Regien = "";
            this.ucLoactionSearch1.Size = new System.Drawing.Size(381, 189);
            this.ucLoactionSearch1.SubChannel = "";
            this.ucLoactionSearch1.TabIndex = 421;
            this.ucLoactionSearch1.Zone = "";
            // 
            // grvLocs
            // 
            this.grvLocs.AllowUserToAddRows = false;
            this.grvLocs.AllowUserToDeleteRows = false;
            this.grvLocs.AllowUserToResizeRows = false;
            this.grvLocs.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvLocs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grvLocs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvLocs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.LOCATION,
            this.LOC_DESCRIPTION});
            this.grvLocs.EnableHeadersVisualStyles = false;
            this.grvLocs.Location = new System.Drawing.Point(457, 285);
            this.grvLocs.Name = "grvLocs";
            this.grvLocs.RowHeadersVisible = false;
            this.grvLocs.Size = new System.Drawing.Size(317, 114);
            this.grvLocs.TabIndex = 422;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FalseValue = "false";
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.TrueValue = "true";
            this.dataGridViewCheckBoxColumn1.Width = 25;
            // 
            // LOCATION
            // 
            this.LOCATION.DataPropertyName = "LOCATION";
            this.LOCATION.HeaderText = "Code";
            this.LOCATION.Name = "LOCATION";
            this.LOCATION.ReadOnly = true;
            this.LOCATION.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LOCATION.Width = 70;
            // 
            // LOC_DESCRIPTION
            // 
            this.LOC_DESCRIPTION.DataPropertyName = "LOC_DESCRIPTION";
            this.LOC_DESCRIPTION.HeaderText = "Description";
            this.LOC_DESCRIPTION.Name = "LOC_DESCRIPTION";
            this.LOC_DESCRIPTION.ReadOnly = true;
            this.LOC_DESCRIPTION.Width = 200;
            // 
            // btnAddLocs
            // 
            this.btnAddLocs.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.right_arrow_icon;
            this.btnAddLocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddLocs.Location = new System.Drawing.Point(390, 285);
            this.btnAddLocs.Name = "btnAddLocs";
            this.btnAddLocs.Size = new System.Drawing.Size(30, 30);
            this.btnAddLocs.TabIndex = 423;
            this.btnAddLocs.UseVisualStyleBackColor = true;
            this.btnAddLocs.Click += new System.EventHandler(this.btnAddLocs_Click);
            // 
            // chkAll_Itm
            // 
            this.chkAll_Itm.AutoSize = true;
            this.chkAll_Itm.Location = new System.Drawing.Point(463, 405);
            this.chkAll_Itm.Name = "chkAll_Itm";
            this.chkAll_Itm.Size = new System.Drawing.Size(69, 17);
            this.chkAll_Itm.TabIndex = 424;
            this.chkAll_Itm.Text = "Select All";
            this.chkAll_Itm.UseVisualStyleBackColor = true;
            this.chkAll_Itm.CheckedChanged += new System.EventHandler(this.chkAll_Itm_CheckedChanged);
            // 
            // ServiceAgentDefinition
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(778, 612);
            this.Controls.Add(this.chkAll_Itm);
            this.Controls.Add(this.btnAddLocs);
            this.Controls.Add(this.grvLocs);
            this.Controls.Add(this.ucLoactionSearch1);
            this.Controls.Add(this.btn_DelPC);
            this.Controls.Add(this.btn_AddPC);
            this.Controls.Add(this.gvUserPC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnSearch_MappedLoc);
            this.Controls.Add(this.lblMapped);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSearch_Town);
            this.Controls.Add(this.txtMappedLoc);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.txtCordinator);
            this.Controls.Add(this.txtContPerson);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.txtTown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAdd2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAdd1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch_ServiceAgent);
            this.Controls.Add(this.txtAgent);
            this.Controls.Add(this.label35);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ServiceAgentDefinition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Service Agent Definition";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserPC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLocs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch_ServiceAgent;
        private System.Windows.Forms.TextBox txtAgent;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdd1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAdd2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox txtContPerson;
        private System.Windows.Forms.TextBox txtCordinator;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TextBox txtMappedLoc;
        private System.Windows.Forms.Button btnSearch_Town;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblMapped;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnSearch_MappedLoc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gvUserPC;
        private System.Windows.Forms.Button btn_DelPC;
        private System.Windows.Forms.Button btn_AddPC;
        private UserControls.ucLoactionSearch ucLoactionSearch1;
        private System.Windows.Forms.DataGridView grvLocs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOC_DESCRIPTION;
        private System.Windows.Forms.Button btnAddLocs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkDelPc;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_SUP_PC_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ml_loc_desc;
        private System.Windows.Forms.CheckBox chkAll_Itm;
    }
}
