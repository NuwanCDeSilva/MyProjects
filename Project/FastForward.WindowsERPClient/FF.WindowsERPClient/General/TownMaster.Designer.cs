namespace FF.WindowsERPClient.General
{
    partial class TownMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TownMaster));
            this.txtDivSec = new System.Windows.Forms.TextBox();
            this.txtDiv = new System.Windows.Forms.Label();
            this.txtDist = new System.Windows.Forms.TextBox();
            this.txtProv = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnSrchTown = new System.Windows.Forms.Button();
            this.txtTown = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSrchDist = new System.Windows.Forms.Button();
            this.btnSrchProv = new System.Windows.Forms.Button();
            this.btnSrchDiv = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPostal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSrchDistFrom = new System.Windows.Forms.Button();
            this.txtDistFrom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSrchDistUOM = new System.Windows.Forms.Button();
            this.txtDistUOM = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDistnce = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLong = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSrchHeightUOM = new System.Windows.Forms.Button();
            this.txtHeightUOM = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkAct = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDist = new System.Windows.Forms.TextBox();
            this.lblProv = new System.Windows.Forms.TextBox();
            this.lblDiv = new System.Windows.Forms.TextBox();
            this.lblDistFrom = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDivSec
            // 
            this.txtDivSec.BackColor = System.Drawing.Color.Beige;
            this.txtDivSec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDivSec.Location = new System.Drawing.Point(124, 311);
            this.txtDivSec.MaxLength = 10;
            this.txtDivSec.Name = "txtDivSec";
            this.txtDivSec.ReadOnly = true;
            this.txtDivSec.Size = new System.Drawing.Size(194, 21);
            this.txtDivSec.TabIndex = 13;
            this.txtDivSec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDivSec_KeyDown);
            // 
            // txtDiv
            // 
            this.txtDiv.AutoSize = true;
            this.txtDiv.Location = new System.Drawing.Point(9, 315);
            this.txtDiv.Name = "txtDiv";
            this.txtDiv.Size = new System.Drawing.Size(114, 13);
            this.txtDiv.TabIndex = 509;
            this.txtDiv.Text = "Divisional Secretariat :";
            // 
            // txtDist
            // 
            this.txtDist.BackColor = System.Drawing.Color.Beige;
            this.txtDist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDist.Location = new System.Drawing.Point(444, 101);
            this.txtDist.MaxLength = 50;
            this.txtDist.Name = "txtDist";
            this.txtDist.ReadOnly = true;
            this.txtDist.Size = new System.Drawing.Size(134, 21);
            this.txtDist.TabIndex = 4;
            this.txtDist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDist_KeyDown);
            // 
            // txtProv
            // 
            this.txtProv.BackColor = System.Drawing.Color.Beige;
            this.txtProv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProv.Location = new System.Drawing.Point(124, 101);
            this.txtProv.MaxLength = 50;
            this.txtProv.Name = "txtProv";
            this.txtProv.ReadOnly = true;
            this.txtProv.Size = new System.Drawing.Size(115, 21);
            this.txtProv.TabIndex = 3;
            this.txtProv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProv_KeyDown);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(383, 105);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(47, 13);
            this.label23.TabIndex = 506;
            this.label23.Text = "District :";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(9, 105);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(55, 13);
            this.label24.TabIndex = 505;
            this.label24.Text = "Province :";
            // 
            // txtCode
            // 
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.Location = new System.Drawing.Point(124, 11);
            this.txtCode.MaxLength = 10;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(96, 21);
            this.txtCode.TabIndex = 0;
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(9, 15);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(68, 13);
            this.label21.TabIndex = 503;
            this.label21.Text = "Town Code :";
            // 
            // btnSrchTown
            // 
            this.btnSrchTown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSrchTown.BackgroundImage")));
            this.btnSrchTown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchTown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchTown.ForeColor = System.Drawing.Color.White;
            this.btnSrchTown.Location = new System.Drawing.Point(227, 12);
            this.btnSrchTown.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSrchTown.Name = "btnSrchTown";
            this.btnSrchTown.Size = new System.Drawing.Size(20, 19);
            this.btnSrchTown.TabIndex = 504;
            this.btnSrchTown.UseVisualStyleBackColor = true;
            this.btnSrchTown.Click += new System.EventHandler(this.btnSrchTown_Click);
            // 
            // txtTown
            // 
            this.txtTown.BackColor = System.Drawing.Color.Beige;
            this.txtTown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTown.Location = new System.Drawing.Point(124, 41);
            this.txtTown.MaxLength = 50;
            this.txtTown.Name = "txtTown";
            this.txtTown.Size = new System.Drawing.Size(464, 21);
            this.txtTown.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 511;
            this.label1.Text = "Town :";
            // 
            // btnSrchDist
            // 
            this.btnSrchDist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSrchDist.BackgroundImage")));
            this.btnSrchDist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchDist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchDist.ForeColor = System.Drawing.Color.White;
            this.btnSrchDist.Location = new System.Drawing.Point(585, 102);
            this.btnSrchDist.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSrchDist.Name = "btnSrchDist";
            this.btnSrchDist.Size = new System.Drawing.Size(20, 19);
            this.btnSrchDist.TabIndex = 512;
            this.btnSrchDist.UseVisualStyleBackColor = true;
            this.btnSrchDist.Click += new System.EventHandler(this.btnSrchDist_Click);
            // 
            // btnSrchProv
            // 
            this.btnSrchProv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSrchProv.BackgroundImage")));
            this.btnSrchProv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchProv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchProv.ForeColor = System.Drawing.Color.White;
            this.btnSrchProv.Location = new System.Drawing.Point(245, 102);
            this.btnSrchProv.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSrchProv.Name = "btnSrchProv";
            this.btnSrchProv.Size = new System.Drawing.Size(20, 19);
            this.btnSrchProv.TabIndex = 513;
            this.btnSrchProv.UseVisualStyleBackColor = true;
            this.btnSrchProv.Click += new System.EventHandler(this.btnSrchProv_Click);
            // 
            // btnSrchDiv
            // 
            this.btnSrchDiv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSrchDiv.BackgroundImage")));
            this.btnSrchDiv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchDiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchDiv.ForeColor = System.Drawing.Color.White;
            this.btnSrchDiv.Location = new System.Drawing.Point(326, 312);
            this.btnSrchDiv.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSrchDiv.Name = "btnSrchDiv";
            this.btnSrchDiv.Size = new System.Drawing.Size(20, 19);
            this.btnSrchDiv.TabIndex = 514;
            this.btnSrchDiv.UseVisualStyleBackColor = true;
            this.btnSrchDiv.Click += new System.EventHandler(this.btnSrchDiv_Click);
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Main City",
            "City",
            "Big Town",
            "Other Town"});
            this.cmbType.Location = new System.Drawing.Point(124, 71);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(162, 21);
            this.cmbType.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 516;
            this.label2.Text = "Type :";
            // 
            // txtPostal
            // 
            this.txtPostal.BackColor = System.Drawing.Color.White;
            this.txtPostal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPostal.Location = new System.Drawing.Point(124, 131);
            this.txtPostal.MaxLength = 5;
            this.txtPostal.Name = "txtPostal";
            this.txtPostal.Size = new System.Drawing.Size(134, 21);
            this.txtPostal.TabIndex = 5;
            this.txtPostal.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 518;
            this.label3.Text = "Postal Code :";
            // 
            // btnSrchDistFrom
            // 
            this.btnSrchDistFrom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSrchDistFrom.BackgroundImage")));
            this.btnSrchDistFrom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchDistFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchDistFrom.ForeColor = System.Drawing.Color.White;
            this.btnSrchDistFrom.Location = new System.Drawing.Point(326, 162);
            this.btnSrchDistFrom.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSrchDistFrom.Name = "btnSrchDistFrom";
            this.btnSrchDistFrom.Size = new System.Drawing.Size(20, 19);
            this.btnSrchDistFrom.TabIndex = 521;
            this.btnSrchDistFrom.UseVisualStyleBackColor = true;
            this.btnSrchDistFrom.Click += new System.EventHandler(this.btnSrchDistFrom_Click);
            // 
            // txtDistFrom
            // 
            this.txtDistFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDistFrom.Location = new System.Drawing.Point(124, 161);
            this.txtDistFrom.MaxLength = 10;
            this.txtDistFrom.Name = "txtDistFrom";
            this.txtDistFrom.ReadOnly = true;
            this.txtDistFrom.Size = new System.Drawing.Size(194, 21);
            this.txtDistFrom.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 520;
            this.label4.Text = "Distance From :";
            // 
            // btnSrchDistUOM
            // 
            this.btnSrchDistUOM.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSrchDistUOM.BackgroundImage")));
            this.btnSrchDistUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchDistUOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchDistUOM.ForeColor = System.Drawing.Color.White;
            this.btnSrchDistUOM.Location = new System.Drawing.Point(251, 192);
            this.btnSrchDistUOM.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSrchDistUOM.Name = "btnSrchDistUOM";
            this.btnSrchDistUOM.Size = new System.Drawing.Size(20, 19);
            this.btnSrchDistUOM.TabIndex = 524;
            this.btnSrchDistUOM.UseVisualStyleBackColor = true;
            this.btnSrchDistUOM.Click += new System.EventHandler(this.btnSrchDistUOM_Click);
            // 
            // txtDistUOM
            // 
            this.txtDistUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDistUOM.Location = new System.Drawing.Point(124, 191);
            this.txtDistUOM.MaxLength = 10;
            this.txtDistUOM.Name = "txtDistUOM";
            this.txtDistUOM.Size = new System.Drawing.Size(119, 21);
            this.txtDistUOM.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 523;
            this.label5.Text = "Distance UOM :";
            // 
            // txtDistnce
            // 
            this.txtDistnce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDistnce.Location = new System.Drawing.Point(124, 221);
            this.txtDistnce.MaxLength = 10;
            this.txtDistnce.Name = "txtDistnce";
            this.txtDistnce.Size = new System.Drawing.Size(119, 21);
            this.txtDistnce.TabIndex = 8;
            this.txtDistnce.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 526;
            this.label6.Text = "Distance :";
            // 
            // txtLat
            // 
            this.txtLat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLat.Location = new System.Drawing.Point(124, 251);
            this.txtLat.MaxLength = 10;
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(96, 21);
            this.txtLat.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 528;
            this.label7.Text = "Latitude :";
            // 
            // txtLong
            // 
            this.txtLong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLong.Location = new System.Drawing.Point(444, 251);
            this.txtLong.MaxLength = 10;
            this.txtLong.Name = "txtLong";
            this.txtLong.Size = new System.Drawing.Size(96, 21);
            this.txtLong.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(383, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 530;
            this.label8.Text = "Longitude :";
            // 
            // btnSrchHeightUOM
            // 
            this.btnSrchHeightUOM.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSrchHeightUOM.BackgroundImage")));
            this.btnSrchHeightUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchHeightUOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchHeightUOM.ForeColor = System.Drawing.Color.White;
            this.btnSrchHeightUOM.Location = new System.Drawing.Point(228, 282);
            this.btnSrchHeightUOM.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSrchHeightUOM.Name = "btnSrchHeightUOM";
            this.btnSrchHeightUOM.Size = new System.Drawing.Size(20, 19);
            this.btnSrchHeightUOM.TabIndex = 533;
            this.btnSrchHeightUOM.UseVisualStyleBackColor = true;
            // 
            // txtHeightUOM
            // 
            this.txtHeightUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHeightUOM.Location = new System.Drawing.Point(124, 281);
            this.txtHeightUOM.MaxLength = 10;
            this.txtHeightUOM.Name = "txtHeightUOM";
            this.txtHeightUOM.Size = new System.Drawing.Size(96, 21);
            this.txtHeightUOM.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 532;
            this.label9.Text = "Height UOM :";
            // 
            // txtHeight
            // 
            this.txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHeight.Location = new System.Drawing.Point(444, 281);
            this.txtHeight.MaxLength = 10;
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(96, 21);
            this.txtHeight.TabIndex = 12;
            this.txtHeight.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(383, 285);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 535;
            this.label10.Text = "Height :";
            // 
            // chkAct
            // 
            this.chkAct.AutoSize = true;
            this.chkAct.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAct.Checked = true;
            this.chkAct.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAct.Location = new System.Drawing.Point(484, 315);
            this.chkAct.Name = "chkAct";
            this.chkAct.Size = new System.Drawing.Size(56, 17);
            this.chkAct.TabIndex = 536;
            this.chkAct.Text = "Active";
            this.chkAct.UseVisualStyleBackColor = true;
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
            this.btnClear.Location = new System.Drawing.Point(526, 349);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(66, 23);
            this.btnClear.TabIndex = 538;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClear.UseVisualStyleBackColor = false;
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
            this.btnSave.Location = new System.Drawing.Point(455, 349);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(66, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblDist
            // 
            this.lblDist.BackColor = System.Drawing.Color.DarkGray;
            this.lblDist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDist.Location = new System.Drawing.Point(502, 131);
            this.lblDist.MaxLength = 50;
            this.lblDist.Name = "lblDist";
            this.lblDist.ReadOnly = true;
            this.lblDist.Size = new System.Drawing.Size(38, 21);
            this.lblDist.TabIndex = 543;
            this.lblDist.Visible = false;
            // 
            // lblProv
            // 
            this.lblProv.BackColor = System.Drawing.Color.DarkGray;
            this.lblProv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProv.Location = new System.Drawing.Point(502, 155);
            this.lblProv.MaxLength = 50;
            this.lblProv.Name = "lblProv";
            this.lblProv.ReadOnly = true;
            this.lblProv.Size = new System.Drawing.Size(38, 21);
            this.lblProv.TabIndex = 544;
            this.lblProv.Visible = false;
            // 
            // lblDiv
            // 
            this.lblDiv.BackColor = System.Drawing.Color.DarkGray;
            this.lblDiv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDiv.Location = new System.Drawing.Point(245, 338);
            this.lblDiv.MaxLength = 50;
            this.lblDiv.Name = "lblDiv";
            this.lblDiv.ReadOnly = true;
            this.lblDiv.Size = new System.Drawing.Size(38, 21);
            this.lblDiv.TabIndex = 545;
            this.lblDiv.Visible = false;
            // 
            // lblDistFrom
            // 
            this.lblDistFrom.BackColor = System.Drawing.Color.DarkGray;
            this.lblDistFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDistFrom.Location = new System.Drawing.Point(502, 180);
            this.lblDistFrom.MaxLength = 50;
            this.lblDistFrom.Name = "lblDistFrom";
            this.lblDistFrom.ReadOnly = true;
            this.lblDistFrom.Size = new System.Drawing.Size(38, 21);
            this.lblDistFrom.TabIndex = 546;
            this.lblDistFrom.Visible = false;
            // 
            // TownMaster
            // 
            this.ClientSize = new System.Drawing.Size(610, 382);
            this.Controls.Add(this.lblDistFrom);
            this.Controls.Add(this.lblDiv);
            this.Controls.Add(this.lblProv);
            this.Controls.Add(this.lblDist);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkAct);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnSrchHeightUOM);
            this.Controls.Add(this.txtHeightUOM);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtLong);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDistnce);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSrchDistUOM);
            this.Controls.Add(this.txtDistUOM);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSrchDistFrom);
            this.Controls.Add(this.txtDistFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPostal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.btnSrchDiv);
            this.Controls.Add(this.btnSrchProv);
            this.Controls.Add(this.btnSrchDist);
            this.Controls.Add(this.txtTown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDivSec);
            this.Controls.Add(this.txtDiv);
            this.Controls.Add(this.txtDist);
            this.Controls.Add(this.txtProv);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.btnSrchTown);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label21);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "TownMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Town Master Details";
            this.Load += new System.EventHandler(this.CustomerCreation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TownMaster_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDivSec;
        private System.Windows.Forms.Label txtDiv;
        private System.Windows.Forms.TextBox txtDist;
        private System.Windows.Forms.TextBox txtProv;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnSrchTown;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSrchDist;
        private System.Windows.Forms.Button btnSrchProv;
        private System.Windows.Forms.Button btnSrchDiv;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPostal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSrchDistFrom;
        private System.Windows.Forms.TextBox txtDistFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSrchDistUOM;
        private System.Windows.Forms.TextBox txtDistUOM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDistnce;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLong;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSrchHeightUOM;
        private System.Windows.Forms.TextBox txtHeightUOM;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkAct;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox lblDist;
        private System.Windows.Forms.TextBox lblProv;
        private System.Windows.Forms.TextBox lblDiv;
        private System.Windows.Forms.TextBox lblDistFrom;

    }
}
