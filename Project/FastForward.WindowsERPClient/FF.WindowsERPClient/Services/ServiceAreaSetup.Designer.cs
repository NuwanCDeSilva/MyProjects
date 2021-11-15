namespace FF.WindowsERPClient.Services
{
    partial class ServiceAreaSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceAreaSetup));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtbrows = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.grdServiceDetails = new System.Windows.Forms.DataGridView();
            this.lblServiceCenter = new System.Windows.Forms.Label();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtTown = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.txtSeviceCenter = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.lblTown = new System.Windows.Forms.Label();
            this.btnUploadFile_spv = new System.Windows.Forms.Button();
            this.btnSearchFile_spv = new System.Windows.Forms.Button();
            this.btnSerchTown = new System.Windows.Forms.Button();
            this.btnSearch_ServiceCeneter = new System.Windows.Forms.Button();
            this.chkInactive = new System.Windows.Forms.CheckBox();
            this.Service_Center_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Service_Center = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Town_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Town = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.States = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SVC_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiceDetails)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtbrows
            // 
            this.txtbrows.BackColor = System.Drawing.Color.White;
            this.txtbrows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbrows.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtbrows.Location = new System.Drawing.Point(94, 99);
            this.txtbrows.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtbrows.MaxLength = 10;
            this.txtbrows.Name = "txtbrows";
            this.txtbrows.Size = new System.Drawing.Size(199, 21);
            this.txtbrows.TabIndex = 265;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(5, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 264;
            this.label1.Text = "Browse";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // grdServiceDetails
            // 
            this.grdServiceDetails.AllowUserToAddRows = false;
            this.grdServiceDetails.AllowUserToDeleteRows = false;
            this.grdServiceDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdServiceDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Service_Center_code,
            this.Service_Center,
            this.Town_Code,
            this.Town,
            this.States,
            this.SVC_ID});
            this.grdServiceDetails.Location = new System.Drawing.Point(8, 139);
            this.grdServiceDetails.Name = "grdServiceDetails";
            this.grdServiceDetails.ReadOnly = true;
            this.grdServiceDetails.Size = new System.Drawing.Size(545, 202);
            this.grdServiceDetails.TabIndex = 262;
            this.grdServiceDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdServiceDetails_CellClick);
            this.grdServiceDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdServiceDetails_CellContentClick);
            // 
            // lblServiceCenter
            // 
            this.lblServiceCenter.AutoSize = true;
            this.lblServiceCenter.BackColor = System.Drawing.Color.Transparent;
            this.lblServiceCenter.Location = new System.Drawing.Point(216, 42);
            this.lblServiceCenter.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblServiceCenter.Name = "lblServiceCenter";
            this.lblServiceCenter.Size = new System.Drawing.Size(11, 13);
            this.lblServiceCenter.TabIndex = 257;
            this.lblServiceCenter.Text = "-";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // txtTown
            // 
            this.txtTown.BackColor = System.Drawing.Color.White;
            this.txtTown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTown.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTown.Location = new System.Drawing.Point(94, 68);
            this.txtTown.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtTown.MaxLength = 10;
            this.txtTown.Name = "txtTown";
            this.txtTown.Size = new System.Drawing.Size(91, 21);
            this.txtTown.TabIndex = 2;
            this.txtTown.TextChanged += new System.EventHandler(this.txtTown_TextChanged);
            this.txtTown.DoubleClick += new System.EventHandler(this.txtTown_DoubleClick);
            this.txtTown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTown_KeyDown);
            this.txtTown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTown_KeyPress);
            this.txtTown.Leave += new System.EventHandler(this.txtTown_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(5, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 258;
            this.label2.Text = "Town";
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
            this.toolStrip1.Size = new System.Drawing.Size(559, 23);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
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
            // txtSeviceCenter
            // 
            this.txtSeviceCenter.BackColor = System.Drawing.Color.White;
            this.txtSeviceCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSeviceCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSeviceCenter.Location = new System.Drawing.Point(94, 35);
            this.txtSeviceCenter.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtSeviceCenter.MaxLength = 10;
            this.txtSeviceCenter.Name = "txtSeviceCenter";
            this.txtSeviceCenter.Size = new System.Drawing.Size(91, 21);
            this.txtSeviceCenter.TabIndex = 0;
            this.txtSeviceCenter.TextChanged += new System.EventHandler(this.txtSeviceCenter_TextChanged);
            this.txtSeviceCenter.DoubleClick += new System.EventHandler(this.txtSeviceCenter_DoubleClick);
            this.txtSeviceCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeviceCenter_KeyDown);
            this.txtSeviceCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSeviceCenter_KeyPress);
            this.txtSeviceCenter.Leave += new System.EventHandler(this.txtSeviceCenter_Leave);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Location = new System.Drawing.Point(5, 39);
            this.label35.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(78, 13);
            this.label35.TabIndex = 253;
            this.label35.Text = "Service Center";
            // 
            // lblTown
            // 
            this.lblTown.AutoSize = true;
            this.lblTown.BackColor = System.Drawing.Color.Transparent;
            this.lblTown.Location = new System.Drawing.Point(216, 75);
            this.lblTown.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblTown.Name = "lblTown";
            this.lblTown.Size = new System.Drawing.Size(11, 13);
            this.lblTown.TabIndex = 260;
            this.lblTown.Text = "-";
            // 
            // btnUploadFile_spv
            // 
            this.btnUploadFile_spv.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.Circle_swith_icon;
            this.btnUploadFile_spv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUploadFile_spv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadFile_spv.ForeColor = System.Drawing.Color.Lavender;
            this.btnUploadFile_spv.Location = new System.Drawing.Point(338, 99);
            this.btnUploadFile_spv.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnUploadFile_spv.Name = "btnUploadFile_spv";
            this.btnUploadFile_spv.Size = new System.Drawing.Size(26, 24);
            this.btnUploadFile_spv.TabIndex = 5;
            this.btnUploadFile_spv.UseVisualStyleBackColor = true;
            this.btnUploadFile_spv.Click += new System.EventHandler(this.btnUploadFile_spv_Click);
            // 
            // btnSearchFile_spv
            // 
            this.btnSearchFile_spv.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.specialsearch2icon;
            this.btnSearchFile_spv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearchFile_spv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchFile_spv.ForeColor = System.Drawing.Color.Lavender;
            this.btnSearchFile_spv.Location = new System.Drawing.Point(303, 99);
            this.btnSearchFile_spv.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearchFile_spv.Name = "btnSearchFile_spv";
            this.btnSearchFile_spv.Size = new System.Drawing.Size(25, 23);
            this.btnSearchFile_spv.TabIndex = 4;
            this.btnSearchFile_spv.UseVisualStyleBackColor = true;
            this.btnSearchFile_spv.Click += new System.EventHandler(this.btnSearchFile_spv_Click);
            // 
            // btnSerchTown
            // 
            this.btnSerchTown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSerchTown.BackgroundImage")));
            this.btnSerchTown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSerchTown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerchTown.ForeColor = System.Drawing.Color.White;
            this.btnSerchTown.Location = new System.Drawing.Point(186, 69);
            this.btnSerchTown.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSerchTown.Name = "btnSerchTown";
            this.btnSerchTown.Size = new System.Drawing.Size(20, 19);
            this.btnSerchTown.TabIndex = 3;
            this.btnSerchTown.UseVisualStyleBackColor = true;
            this.btnSerchTown.Click += new System.EventHandler(this.btnSerchTown_Click);
            // 
            // btnSearch_ServiceCeneter
            // 
            this.btnSearch_ServiceCeneter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_ServiceCeneter.BackgroundImage")));
            this.btnSearch_ServiceCeneter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_ServiceCeneter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_ServiceCeneter.ForeColor = System.Drawing.Color.White;
            this.btnSearch_ServiceCeneter.Location = new System.Drawing.Point(186, 36);
            this.btnSearch_ServiceCeneter.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_ServiceCeneter.Name = "btnSearch_ServiceCeneter";
            this.btnSearch_ServiceCeneter.Size = new System.Drawing.Size(20, 19);
            this.btnSearch_ServiceCeneter.TabIndex = 1;
            this.btnSearch_ServiceCeneter.UseVisualStyleBackColor = true;
            this.btnSearch_ServiceCeneter.Click += new System.EventHandler(this.btnSearch_ServiceCeneter_Click);
            // 
            // chkInactive
            // 
            this.chkInactive.AutoSize = true;
            this.chkInactive.Checked = true;
            this.chkInactive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInactive.Location = new System.Drawing.Point(368, 75);
            this.chkInactive.Name = "chkInactive";
            this.chkInactive.Size = new System.Drawing.Size(56, 17);
            this.chkInactive.TabIndex = 6;
            this.chkInactive.Text = "Active";
            this.chkInactive.UseVisualStyleBackColor = true;
            // 
            // Service_Center_code
            // 
            this.Service_Center_code.HeaderText = "Service Center Code";
            this.Service_Center_code.Name = "Service_Center_code";
            this.Service_Center_code.ReadOnly = true;
            // 
            // Service_Center
            // 
            this.Service_Center.HeaderText = "Service description";
            this.Service_Center.Name = "Service_Center";
            this.Service_Center.ReadOnly = true;
            // 
            // Town_Code
            // 
            this.Town_Code.HeaderText = "Town Code";
            this.Town_Code.Name = "Town_Code";
            this.Town_Code.ReadOnly = true;
            // 
            // Town
            // 
            this.Town.HeaderText = "Town";
            this.Town.Name = "Town";
            this.Town.ReadOnly = true;
            // 
            // States
            // 
            this.States.HeaderText = "States";
            this.States.Name = "States";
            this.States.ReadOnly = true;
            // 
            // SVC_ID
            // 
            this.SVC_ID.HeaderText = "SVC_ID";
            this.SVC_ID.Name = "SVC_ID";
            this.SVC_ID.ReadOnly = true;
            this.SVC_ID.Visible = false;
            // 
            // ServiceAreaSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(559, 350);
            this.Controls.Add(this.chkInactive);
            this.Controls.Add(this.btnUploadFile_spv);
            this.Controls.Add(this.txtbrows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearchFile_spv);
            this.Controls.Add(this.grdServiceDetails);
            this.Controls.Add(this.lblServiceCenter);
            this.Controls.Add(this.btnSerchTown);
            this.Controls.Add(this.txtTown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnSearch_ServiceCeneter);
            this.Controls.Add(this.txtSeviceCenter);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.lblTown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ServiceAreaSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Service Area Setup";
            this.Load += new System.EventHandler(this.ServiceAreaSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdServiceDetails)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadFile_spv;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtbrows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchFile_spv;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView grdServiceDetails;
        private System.Windows.Forms.Label lblServiceCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnSerchTown;
        private System.Windows.Forms.TextBox txtTown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Button btnSearch_ServiceCeneter;
        private System.Windows.Forms.TextBox txtSeviceCenter;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lblTown;
        private System.Windows.Forms.CheckBox chkInactive;
        private System.Windows.Forms.DataGridViewTextBoxColumn Service_Center_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Service_Center;
        private System.Windows.Forms.DataGridViewTextBoxColumn Town_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Town;
        private System.Windows.Forms.DataGridViewTextBoxColumn States;
        private System.Windows.Forms.DataGridViewTextBoxColumn SVC_ID;
    }
}