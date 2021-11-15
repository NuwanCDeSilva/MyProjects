namespace FF.WindowsERPClient.Finance
{
    partial class FineChargesExeclUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FineChargesExeclUpload));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvFinesetup = new System.Windows.Forms.DataGridView();
            this.CompanyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pccode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FineChargeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FineRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExeclUpload = new System.Windows.Forms.TextBox();
            this.btnSearchExclpath = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinesetup)).BeginInit();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvFinesetup);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(723, 308);
            this.panel1.TabIndex = 0;
            // 
            // dgvFinesetup
            // 
            this.dgvFinesetup.AllowUserToAddRows = false;
            this.dgvFinesetup.AllowUserToDeleteRows = false;
            this.dgvFinesetup.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFinesetup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFinesetup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFinesetup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompanyCode,
            this.Pccode,
            this.FineChargeCode,
            this.FineRemark,
            this.colAmount,
            this.UserID,
            this.colDate});
            this.dgvFinesetup.EnableHeadersVisualStyles = false;
            this.dgvFinesetup.Location = new System.Drawing.Point(13, 86);
            this.dgvFinesetup.Name = "dgvFinesetup";
            this.dgvFinesetup.ReadOnly = true;
            this.dgvFinesetup.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvFinesetup.RowHeadersVisible = false;
            this.dgvFinesetup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFinesetup.Size = new System.Drawing.Size(704, 187);
            this.dgvFinesetup.TabIndex = 146;
            // 
            // CompanyCode
            // 
            this.CompanyCode.DataPropertyName = "comcode";
            this.CompanyCode.HeaderText = "CompanyCode";
            this.CompanyCode.Name = "CompanyCode";
            this.CompanyCode.ReadOnly = true;
            // 
            // Pccode
            // 
            this.Pccode.DataPropertyName = "pccode";
            this.Pccode.HeaderText = "PC";
            this.Pccode.Name = "Pccode";
            this.Pccode.ReadOnly = true;
            // 
            // FineChargeCode
            // 
            this.FineChargeCode.DataPropertyName = "finecode";
            this.FineChargeCode.HeaderText = "FineChargeCode";
            this.FineChargeCode.Name = "FineChargeCode";
            this.FineChargeCode.ReadOnly = true;
            // 
            // FineRemark
            // 
            this.FineRemark.DataPropertyName = "remarks";
            this.FineRemark.HeaderText = "FineRemark";
            this.FineRemark.Name = "FineRemark";
            this.FineRemark.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "amount";
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "createby";
            this.UserID.HeaderText = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            // 
            // colDate
            // 
            this.colDate.DataPropertyName = "finedate";
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUpload);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtExeclUpload);
            this.panel2.Controls.Add(this.btnSearchExclpath);
            this.panel2.Location = new System.Drawing.Point(13, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 58);
            this.panel2.TabIndex = 145;
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(743, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = false;
            this.btnClear.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 22);
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 22);
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FineChargesExeclUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 361);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "FineChargesExeclUpload";
            this.Text = "FineChargesExeclUpload";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinesetup)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExeclUpload;
        private System.Windows.Forms.Button btnSearchExclpath;
        private System.Windows.Forms.DataGridView dgvFinesetup;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pccode;
        private System.Windows.Forms.DataGridViewTextBoxColumn FineChargeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn FineRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
    }
}