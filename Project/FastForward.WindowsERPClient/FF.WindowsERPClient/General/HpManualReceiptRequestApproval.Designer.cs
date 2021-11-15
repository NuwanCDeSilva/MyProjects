namespace FF.WindowsERPClient.General
{
    partial class HpManualReceiptRequestApproval
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HpManualReceiptRequestApproval));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRequest = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtReqLoc = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.chkApp = new System.Windows.Forms.CheckBox();
            this.btnRej = new System.Windows.Forms.Button();
            this.btnApp = new System.Windows.Forms.Button();
            this.dgvPendings = new System.Windows.Forms.DataGridView();
            this.col_reqNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_pc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Inv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_reqDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SubType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_OthPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label53 = new System.Windows.Forms.Label();
            this.lblReqPC = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblReq = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnAddRec = new System.Windows.Forms.Button();
            this.txtManRec = new System.Windows.Forms.TextBox();
            this.ddlPrefix = new System.Windows.Forms.ComboBox();
            this.label81 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.gvReceipts = new System.Windows.Forms.DataGridView();
            this.col_recPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_recMannual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDeleteLast = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReceipts)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.toolStripSeparator3,
            this.btnRequest});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(749, 25);
            this.toolStrip1.TabIndex = 200;
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
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // btnRequest
            // 
            this.btnRequest.AutoSize = false;
            this.btnRequest.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRequest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRequest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRequest.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Padding = new System.Windows.Forms.Padding(2);
            this.btnRequest.Size = new System.Drawing.Size(60, 22);
            this.btnRequest.Text = "&Request";
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtReqLoc);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.chkApp);
            this.groupBox1.Controls.Add(this.btnRej);
            this.groupBox1.Controls.Add(this.btnApp);
            this.groupBox1.Controls.Add(this.dgvPendings);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Location = new System.Drawing.Point(1, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(747, 178);
            this.groupBox1.TabIndex = 201;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pending Request(s)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(200, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(19, 13);
            this.label19.TabIndex = 73;
            this.label19.Text = "F2";
            // 
            // txtReqLoc
            // 
            this.txtReqLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReqLoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReqLoc.Location = new System.Drawing.Point(82, 17);
            this.txtReqLoc.Name = "txtReqLoc";
            this.txtReqLoc.Size = new System.Drawing.Size(115, 20);
            this.txtReqLoc.TabIndex = 72;
            this.txtReqLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReqLoc_KeyDown);
            this.txtReqLoc.Leave += new System.EventHandler(this.txtReqLoc_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 13);
            this.label18.TabIndex = 71;
            this.label18.Text = "Profit center :";
            // 
            // chkApp
            // 
            this.chkApp.AutoSize = true;
            this.chkApp.Location = new System.Drawing.Point(5, 155);
            this.chkApp.Name = "chkApp";
            this.chkApp.Size = new System.Drawing.Size(114, 17);
            this.chkApp.TabIndex = 70;
            this.chkApp.Text = "Approval pendings";
            this.chkApp.UseVisualStyleBackColor = true;
            // 
            // btnRej
            // 
            this.btnRej.Location = new System.Drawing.Point(686, 150);
            this.btnRej.Name = "btnRej";
            this.btnRej.Size = new System.Drawing.Size(56, 23);
            this.btnRej.TabIndex = 69;
            this.btnRej.Text = "Reject";
            this.btnRej.UseVisualStyleBackColor = true;
            this.btnRej.Click += new System.EventHandler(this.btnRej_Click);
            // 
            // btnApp
            // 
            this.btnApp.Location = new System.Drawing.Point(631, 150);
            this.btnApp.Name = "btnApp";
            this.btnApp.Size = new System.Drawing.Size(56, 23);
            this.btnApp.TabIndex = 68;
            this.btnApp.Text = "Approve";
            this.btnApp.UseVisualStyleBackColor = true;
            this.btnApp.Click += new System.EventHandler(this.btnApp_Click);
            // 
            // dgvPendings
            // 
            this.dgvPendings.AllowUserToAddRows = false;
            this.dgvPendings.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.dgvPendings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPendings.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPendings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPendings.ColumnHeadersHeight = 20;
            this.dgvPendings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_reqNo,
            this.col_pc,
            this.col_Inv,
            this.col_reqDate,
            this.col_Status,
            this.col_remarks,
            this.col_Type,
            this.col_SubType,
            this.col_OthPC});
            this.dgvPendings.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvPendings.EnableHeadersVisualStyles = false;
            this.dgvPendings.Location = new System.Drawing.Point(3, 41);
            this.dgvPendings.Name = "dgvPendings";
            this.dgvPendings.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPendings.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPendings.RowHeadersVisible = false;
            this.dgvPendings.RowTemplate.Height = 18;
            this.dgvPendings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPendings.Size = new System.Drawing.Size(740, 108);
            this.dgvPendings.TabIndex = 67;
            this.dgvPendings.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPendings_CellContentDoubleClick);
            // 
            // col_reqNo
            // 
            this.col_reqNo.DataPropertyName = "grah_ref";
            this.col_reqNo.HeaderText = "Req. No";
            this.col_reqNo.Name = "col_reqNo";
            this.col_reqNo.ReadOnly = true;
            this.col_reqNo.Width = 150;
            // 
            // col_pc
            // 
            this.col_pc.DataPropertyName = "grah_loc";
            this.col_pc.HeaderText = "Profit center";
            this.col_pc.Name = "col_pc";
            this.col_pc.ReadOnly = true;
            this.col_pc.Width = 85;
            // 
            // col_Inv
            // 
            this.col_Inv.DataPropertyName = "grah_fuc_cd";
            this.col_Inv.HeaderText = "Invoice #";
            this.col_Inv.Name = "col_Inv";
            this.col_Inv.ReadOnly = true;
            this.col_Inv.Visible = false;
            // 
            // col_reqDate
            // 
            this.col_reqDate.DataPropertyName = "grah_cre_dt";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.col_reqDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_reqDate.HeaderText = "Req. Date";
            this.col_reqDate.Name = "col_reqDate";
            this.col_reqDate.ReadOnly = true;
            this.col_reqDate.Width = 85;
            // 
            // col_Status
            // 
            this.col_Status.DataPropertyName = "grah_app_stus";
            this.col_Status.HeaderText = "Status";
            this.col_Status.Name = "col_Status";
            this.col_Status.ReadOnly = true;
            // 
            // col_remarks
            // 
            this.col_remarks.DataPropertyName = "grah_remaks";
            this.col_remarks.HeaderText = "Remarks";
            this.col_remarks.Name = "col_remarks";
            this.col_remarks.ReadOnly = true;
            this.col_remarks.Width = 200;
            // 
            // col_Type
            // 
            this.col_Type.DataPropertyName = "grah_oth_loc";
            this.col_Type.HeaderText = "Type";
            this.col_Type.Name = "col_Type";
            this.col_Type.ReadOnly = true;
            this.col_Type.Visible = false;
            // 
            // col_SubType
            // 
            this.col_SubType.DataPropertyName = "grah_sub_type";
            this.col_SubType.HeaderText = "Sub Type";
            this.col_SubType.Name = "col_SubType";
            this.col_SubType.ReadOnly = true;
            this.col_SubType.Visible = false;
            // 
            // col_OthPC
            // 
            this.col_OthPC.DataPropertyName = "grah_oth_pc";
            this.col_OthPC.HeaderText = "Original Sales PC";
            this.col_OthPC.Name = "col_OthPC";
            this.col_OthPC.ReadOnly = true;
            this.col_OthPC.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(576, 150);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(56, 23);
            this.btnRefresh.TabIndex = 66;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.Navy;
            this.label53.ForeColor = System.Drawing.Color.Transparent;
            this.label53.Location = new System.Drawing.Point(1, 26);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(747, 19);
            this.label53.TabIndex = 202;
            this.label53.Text = "Request / Approval Details";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReqPC
            // 
            this.lblReqPC.BackColor = System.Drawing.Color.Navy;
            this.lblReqPC.ForeColor = System.Drawing.Color.White;
            this.lblReqPC.Location = new System.Drawing.Point(479, 29);
            this.lblReqPC.Name = "lblReqPC";
            this.lblReqPC.Size = new System.Drawing.Size(80, 13);
            this.lblReqPC.TabIndex = 208;
            this.lblReqPC.Text = "Profit Center";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Navy;
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(404, 29);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(70, 13);
            this.label24.TabIndex = 207;
            this.label24.Text = "Request PC :";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Navy;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(656, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(90, 13);
            this.lblStatus.TabIndex = 206;
            this.lblStatus.Text = "Status";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Navy;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(605, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 205;
            this.label12.Text = "Status :";
            // 
            // lblReq
            // 
            this.lblReq.BackColor = System.Drawing.Color.Navy;
            this.lblReq.ForeColor = System.Drawing.Color.White;
            this.lblReq.Location = new System.Drawing.Point(234, 29);
            this.lblReq.Name = "lblReq";
            this.lblReq.Size = new System.Drawing.Size(129, 13);
            this.lblReq.TabIndex = 204;
            this.lblReq.Text = "Request # :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Navy;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(167, 29);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 13);
            this.label15.TabIndex = 203;
            this.label15.Text = "Request # :";
            // 
            // btnAddRec
            // 
            this.btnAddRec.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddRec.BackColor = System.Drawing.Color.White;
            this.btnAddRec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddRec.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnAddRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRec.ForeColor = System.Drawing.Color.White;
            this.btnAddRec.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRec.Image")));
            this.btnAddRec.Location = new System.Drawing.Point(723, 250);
            this.btnAddRec.Name = "btnAddRec";
            this.btnAddRec.Size = new System.Drawing.Size(22, 21);
            this.btnAddRec.TabIndex = 241;
            this.btnAddRec.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnAddRec.UseVisualStyleBackColor = false;
            this.btnAddRec.Click += new System.EventHandler(this.btnAddRec_Click);
            // 
            // txtManRec
            // 
            this.txtManRec.Location = new System.Drawing.Point(605, 250);
            this.txtManRec.Name = "txtManRec";
            this.txtManRec.Size = new System.Drawing.Size(117, 20);
            this.txtManRec.TabIndex = 240;
            this.txtManRec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtManRec_KeyDown);
            // 
            // ddlPrefix
            // 
            this.ddlPrefix.FormattingEnabled = true;
            this.ddlPrefix.Location = new System.Drawing.Point(482, 250);
            this.ddlPrefix.Name = "ddlPrefix";
            this.ddlPrefix.Size = new System.Drawing.Size(122, 21);
            this.ddlPrefix.TabIndex = 239;
            // 
            // label81
            // 
            this.label81.BackColor = System.Drawing.Color.SteelBlue;
            this.label81.ForeColor = System.Drawing.Color.White;
            this.label81.Location = new System.Drawing.Point(606, 233);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(139, 16);
            this.label81.TabIndex = 238;
            this.label81.Text = "Receipt #";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.SteelBlue;
            this.label79.ForeColor = System.Drawing.Color.White;
            this.label79.Location = new System.Drawing.Point(483, 233);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(122, 16);
            this.label79.TabIndex = 237;
            this.label79.Text = "Prefix";
            this.label79.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvReceipts
            // 
            this.gvReceipts.AllowUserToAddRows = false;
            this.gvReceipts.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.SkyBlue;
            this.gvReceipts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvReceipts.BackgroundColor = System.Drawing.Color.White;
            this.gvReceipts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvReceipts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvReceipts.ColumnHeadersHeight = 20;
            this.gvReceipts.ColumnHeadersVisible = false;
            this.gvReceipts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_recPrefix,
            this.col_recMannual});
            this.gvReceipts.Cursor = System.Windows.Forms.Cursors.Default;
            this.gvReceipts.EnableHeadersVisualStyles = false;
            this.gvReceipts.Location = new System.Drawing.Point(483, 272);
            this.gvReceipts.Name = "gvReceipts";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvReceipts.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvReceipts.RowHeadersVisible = false;
            this.gvReceipts.RowTemplate.Height = 18;
            this.gvReceipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvReceipts.Size = new System.Drawing.Size(262, 134);
            this.gvReceipts.TabIndex = 242;
            // 
            // col_recPrefix
            // 
            this.col_recPrefix.DataPropertyName = "gras_anal2";
            this.col_recPrefix.HeaderText = "Prefix";
            this.col_recPrefix.Name = "col_recPrefix";
            this.col_recPrefix.ReadOnly = true;
            this.col_recPrefix.Width = 75;
            // 
            // col_recMannual
            // 
            this.col_recMannual.DataPropertyName = "gras_anal4";
            this.col_recMannual.HeaderText = "Mannual Ref";
            this.col_recMannual.Name = "col_recMannual";
            this.col_recMannual.ReadOnly = true;
            this.col_recMannual.Width = 80;
            // 
            // btnDeleteLast
            // 
            this.btnDeleteLast.Location = new System.Drawing.Point(675, 407);
            this.btnDeleteLast.Name = "btnDeleteLast";
            this.btnDeleteLast.Size = new System.Drawing.Size(70, 22);
            this.btnDeleteLast.TabIndex = 243;
            this.btnDeleteLast.Text = "Delete Last";
            this.btnDeleteLast.UseVisualStyleBackColor = true;
            this.btnDeleteLast.Click += new System.EventHandler(this.btnDeleteLast_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(53, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(10, 13);
            this.label11.TabIndex = 246;
            this.label11.Text = ":";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(66, 234);
            this.txtRemarks.MaxLength = 200;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(408, 20);
            this.txtRemarks.TabIndex = 245;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 244;
            this.label5.Text = "Remarks";
            // 
            // HpManualReceiptRequestApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 430);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDeleteLast);
            this.Controls.Add(this.gvReceipts);
            this.Controls.Add(this.btnAddRec);
            this.Controls.Add(this.txtManRec);
            this.Controls.Add(this.ddlPrefix);
            this.Controls.Add(this.label81);
            this.Controls.Add(this.label79);
            this.Controls.Add(this.lblReqPC);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblReq);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label53);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HpManualReceiptRequestApproval";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "HpManualReceiptRequestApproval";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HpManualReceiptRequestApproval_FormClosing);
            this.Load += new System.EventHandler(this.HpManualReceiptRequestApproval_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReceipts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnRequest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtReqLoc;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox chkApp;
        private System.Windows.Forms.Button btnRej;
        private System.Windows.Forms.Button btnApp;
        private System.Windows.Forms.DataGridView dgvPendings;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_reqNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_pc;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Inv;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_reqDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SubType;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_OthPC;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label lblReqPC;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblReq;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Button btnAddRec;
        private System.Windows.Forms.TextBox txtManRec;
        private System.Windows.Forms.ComboBox ddlPrefix;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.DataGridView gvReceipts;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_recPrefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_recMannual;
        private System.Windows.Forms.Button btnDeleteLast;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label5;
    }
}