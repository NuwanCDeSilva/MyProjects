namespace FF.WindowsERPClient.MDINotification
{
    partial class frmOtherShopCollections
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.SAR_ACC_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAR_PREFIX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAR_MANUAL_REF_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAR_PROFIT_CENTER_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAR_TOT_SETTLE_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDetails
            // 
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            this.dgvDetails.AllowUserToOrderColumns = true;
            this.dgvDetails.AllowUserToResizeRows = false;
            this.dgvDetails.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SAR_ACC_NO,
            this.SAR_PREFIX,
            this.SAR_MANUAL_REF_NO,
            this.SAR_PROFIT_CENTER_CD,
            this.SAR_TOT_SETTLE_AMT});
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.EnableHeadersVisualStyles = false;
            this.dgvDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetails.Size = new System.Drawing.Size(648, 382);
            this.dgvDetails.TabIndex = 0;
            // 
            // SAR_ACC_NO
            // 
            this.SAR_ACC_NO.DataPropertyName = "SAR_ACC_NO";
            this.SAR_ACC_NO.HeaderText = "Account Num";
            this.SAR_ACC_NO.Name = "SAR_ACC_NO";
            this.SAR_ACC_NO.ReadOnly = true;
            this.SAR_ACC_NO.Width = 130;
            // 
            // SAR_PREFIX
            // 
            this.SAR_PREFIX.DataPropertyName = "SAR_PREFIX";
            this.SAR_PREFIX.HeaderText = "Perfix";
            this.SAR_PREFIX.Name = "SAR_PREFIX";
            this.SAR_PREFIX.ReadOnly = true;
            // 
            // SAR_MANUAL_REF_NO
            // 
            this.SAR_MANUAL_REF_NO.DataPropertyName = "SAR_MANUAL_REF_NO";
            this.SAR_MANUAL_REF_NO.HeaderText = "Mannual Ref.";
            this.SAR_MANUAL_REF_NO.Name = "SAR_MANUAL_REF_NO";
            this.SAR_MANUAL_REF_NO.ReadOnly = true;
            // 
            // SAR_PROFIT_CENTER_CD
            // 
            this.SAR_PROFIT_CENTER_CD.DataPropertyName = "SAR_PROFIT_CENTER_CD";
            this.SAR_PROFIT_CENTER_CD.HeaderText = "Collected Profit Center";
            this.SAR_PROFIT_CENTER_CD.Name = "SAR_PROFIT_CENTER_CD";
            this.SAR_PROFIT_CENTER_CD.ReadOnly = true;
            this.SAR_PROFIT_CENTER_CD.Width = 140;
            // 
            // SAR_TOT_SETTLE_AMT
            // 
            this.SAR_TOT_SETTLE_AMT.DataPropertyName = "SAR_TOT_SETTLE_AMT";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.SAR_TOT_SETTLE_AMT.DefaultCellStyle = dataGridViewCellStyle2;
            this.SAR_TOT_SETTLE_AMT.HeaderText = "Amount";
            this.SAR_TOT_SETTLE_AMT.Name = "SAR_TOT_SETTLE_AMT";
            this.SAR_TOT_SETTLE_AMT.ReadOnly = true;
            // 
            // frmOtherShopCollections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 382);
            this.Controls.Add(this.dgvDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOtherShopCollections";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Other Shop Collection Details";
            this.Load += new System.EventHandler(this.frmOtherShopCollections_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAR_ACC_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAR_PREFIX;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAR_MANUAL_REF_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAR_PROFIT_CENTER_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAR_TOT_SETTLE_AMT;
    }
}