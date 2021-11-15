namespace FF.WindowsERPClient.General
{
    partial class ContactUs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactUs));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imgIT = new System.Windows.Forms.PictureBox();
            this.btnIT = new System.Windows.Forms.Label();
            this.btnAcc = new System.Windows.Forms.Label();
            this.grvDet = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCredit = new System.Windows.Forms.Label();
            this.btnInv = new System.Windows.Forms.Label();
            this.btnCost = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblInfor = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnIns = new System.Windows.Forms.Label();
            this.btnITOpr = new System.Windows.Forms.Label();
            this.SAR_RECEIPT_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sar_acc_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sar_receipt_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receiptTpDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sar_receipt_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sar_tot_settle_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.imgIT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDet)).BeginInit();
            this.SuspendLayout();
            // 
            // imgIT
            // 
            this.imgIT.Image = ((System.Drawing.Image)(resources.GetObject("imgIT.Image")));
            this.imgIT.Location = new System.Drawing.Point(924, 18);
            this.imgIT.Name = "imgIT";
            this.imgIT.Size = new System.Drawing.Size(207, 148);
            this.imgIT.TabIndex = 56;
            this.imgIT.TabStop = false;
            // 
            // btnIT
            // 
            this.btnIT.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnIT.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIT.Location = new System.Drawing.Point(12, 38);
            this.btnIT.Name = "btnIT";
            this.btnIT.Size = new System.Drawing.Size(83, 33);
            this.btnIT.TabIndex = 57;
            this.btnIT.Text = "IT (Project)";
            this.btnIT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnIT.Click += new System.EventHandler(this.btnIT_Click);
            // 
            // btnAcc
            // 
            this.btnAcc.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnAcc.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcc.Location = new System.Drawing.Point(190, 38);
            this.btnAcc.Name = "btnAcc";
            this.btnAcc.Size = new System.Drawing.Size(83, 33);
            this.btnAcc.TabIndex = 58;
            this.btnAcc.Text = "ACCOUNTS";
            this.btnAcc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAcc.Click += new System.EventHandler(this.btnAcc_Click);
            // 
            // grvDet
            // 
            this.grvDet.AllowUserToAddRows = false;
            this.grvDet.AllowUserToResizeRows = false;
            this.grvDet.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.grvDet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvDet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grvDet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SAR_RECEIPT_TYPE,
            this.sar_acc_no,
            this.sar_receipt_date,
            this.receiptTpDesc,
            this.sar_receipt_no,
            this.sar_tot_settle_amt});
            this.grvDet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grvDet.EnableHeadersVisualStyles = false;
            this.grvDet.Location = new System.Drawing.Point(12, 166);
            this.grvDet.Name = "grvDet";
            this.grvDet.ReadOnly = true;
            this.grvDet.RowHeadersVisible = false;
            this.grvDet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvDet.Size = new System.Drawing.Size(1119, 357);
            this.grvDet.TabIndex = 212;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(170)))), ((int)(((byte)(111)))));
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1089, 26);
            this.label2.TabIndex = 213;
            this.label2.Text = "CONTACT US";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCredit
            // 
            this.btnCredit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnCredit.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCredit.Location = new System.Drawing.Point(368, 38);
            this.btnCredit.Name = "btnCredit";
            this.btnCredit.Size = new System.Drawing.Size(83, 33);
            this.btnCredit.TabIndex = 215;
            this.btnCredit.Text = "CREDIT";
            this.btnCredit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCredit.Click += new System.EventHandler(this.btnCredit_Click);
            // 
            // btnInv
            // 
            this.btnInv.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnInv.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInv.Location = new System.Drawing.Point(279, 38);
            this.btnInv.Name = "btnInv";
            this.btnInv.Size = new System.Drawing.Size(83, 33);
            this.btnInv.TabIndex = 214;
            this.btnInv.Text = "INVENTORY";
            this.btnInv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInv.Click += new System.EventHandler(this.btnInv_Click);
            // 
            // btnCost
            // 
            this.btnCost.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnCost.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCost.Location = new System.Drawing.Point(457, 38);
            this.btnCost.Name = "btnCost";
            this.btnCost.Size = new System.Drawing.Size(83, 33);
            this.btnCost.TabIndex = 216;
            this.btnCost.Text = "COSTING";
            this.btnCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCost.Click += new System.EventHandler(this.btnCost_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.SteelBlue;
            this.label14.ForeColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(12, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(906, 14);
            this.label14.TabIndex = 217;
            this.label14.Text = "Information";
            // 
            // lblInfor
            // 
            this.lblInfor.AutoSize = true;
            this.lblInfor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfor.Location = new System.Drawing.Point(23, 119);
            this.lblInfor.Name = "lblInfor";
            this.lblInfor.Size = new System.Drawing.Size(0, 16);
            this.lblInfor.TabIndex = 218;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.panelcloseicon;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(1100, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 23);
            this.btnClose.TabIndex = 219;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnIns
            // 
            this.btnIns.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnIns.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIns.Location = new System.Drawing.Point(546, 38);
            this.btnIns.Name = "btnIns";
            this.btnIns.Size = new System.Drawing.Size(83, 33);
            this.btnIns.TabIndex = 220;
            this.btnIns.Text = "INSURANCE";
            this.btnIns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnIns.Click += new System.EventHandler(this.btnIns_Click);
            // 
            // btnITOpr
            // 
            this.btnITOpr.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnITOpr.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnITOpr.Location = new System.Drawing.Point(101, 38);
            this.btnITOpr.Name = "btnITOpr";
            this.btnITOpr.Size = new System.Drawing.Size(83, 33);
            this.btnITOpr.TabIndex = 221;
            this.btnITOpr.Text = "IT (Operation)";
            this.btnITOpr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnITOpr.Click += new System.EventHandler(this.btnITOpr_Click);
            // 
            // SAR_RECEIPT_TYPE
            // 
            this.SAR_RECEIPT_TYPE.DataPropertyName = "DCNT_NAME";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SAR_RECEIPT_TYPE.DefaultCellStyle = dataGridViewCellStyle2;
            this.SAR_RECEIPT_TYPE.HeaderText = "NAME";
            this.SAR_RECEIPT_TYPE.Name = "SAR_RECEIPT_TYPE";
            this.SAR_RECEIPT_TYPE.ReadOnly = true;
            this.SAR_RECEIPT_TYPE.Width = 180;
            // 
            // sar_acc_no
            // 
            this.sar_acc_no.DataPropertyName = "dcnt_desig";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.sar_acc_no.DefaultCellStyle = dataGridViewCellStyle3;
            this.sar_acc_no.HeaderText = "DESIGNATION";
            this.sar_acc_no.Name = "sar_acc_no";
            this.sar_acc_no.ReadOnly = true;
            this.sar_acc_no.Width = 170;
            // 
            // sar_receipt_date
            // 
            this.sar_receipt_date.DataPropertyName = "DCNT_FAX";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.sar_receipt_date.DefaultCellStyle = dataGridViewCellStyle4;
            this.sar_receipt_date.HeaderText = "RELATED WORKS";
            this.sar_receipt_date.Name = "sar_receipt_date";
            this.sar_receipt_date.ReadOnly = true;
            this.sar_receipt_date.Width = 410;
            // 
            // receiptTpDesc
            // 
            this.receiptTpDesc.DataPropertyName = "DCNT_TEL";
            this.receiptTpDesc.HeaderText = "CONTACT NO (OFFICE)";
            this.receiptTpDesc.Name = "receiptTpDesc";
            this.receiptTpDesc.ReadOnly = true;
            // 
            // sar_receipt_no
            // 
            this.sar_receipt_no.DataPropertyName = "DCNT_MOB";
            this.sar_receipt_no.HeaderText = "CONTACT NO (MOB)";
            this.sar_receipt_no.Name = "sar_receipt_no";
            this.sar_receipt_no.ReadOnly = true;
            this.sar_receipt_no.Width = 90;
            // 
            // sar_tot_settle_amt
            // 
            this.sar_tot_settle_amt.DataPropertyName = "DCNT_EMAIL";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.NullValue = null;
            this.sar_tot_settle_amt.DefaultCellStyle = dataGridViewCellStyle5;
            this.sar_tot_settle_amt.HeaderText = "EMAIL";
            this.sar_tot_settle_amt.Name = "sar_tot_settle_amt";
            this.sar_tot_settle_amt.ReadOnly = true;
            this.sar_tot_settle_amt.Width = 150;
            // 
            // ContactUs
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1129, 524);
            this.ControlBox = false;
            this.Controls.Add(this.btnITOpr);
            this.Controls.Add(this.btnIns);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblInfor);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnCost);
            this.Controls.Add(this.btnCredit);
            this.Controls.Add(this.btnInv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grvDet);
            this.Controls.Add(this.btnAcc);
            this.Controls.Add(this.btnIT);
            this.Controls.Add(this.imgIT);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ContactUs";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.ContactUs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgIT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgIT;
        private System.Windows.Forms.Label btnIT;
        private System.Windows.Forms.Label btnAcc;
        private System.Windows.Forms.DataGridView grvDet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label btnCredit;
        private System.Windows.Forms.Label btnInv;
        private System.Windows.Forms.Label btnCost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblInfor;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label btnIns;
        private System.Windows.Forms.Label btnITOpr;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAR_RECEIPT_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_acc_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_receipt_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn receiptTpDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_receipt_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn sar_tot_settle_amt;
    }
}
