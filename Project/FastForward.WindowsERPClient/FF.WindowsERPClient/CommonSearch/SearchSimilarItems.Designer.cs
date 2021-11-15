namespace FF.WindowsERPClient.CommonSearch
{
    partial class SearchSimilarItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchSimilarItems));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dvResult = new System.Windows.Forms.DataGridView();
            this.MISI_SEQ_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_TP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_COM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_ITM_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_SIM_ITM_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MI_LONGDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MI_MODEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MI_BRAND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_FROM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_TO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_LOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_DOC_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MISI_PROMO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dvResult);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(701, 291);
            this.pnlMain.TabIndex = 0;
            // 
            // dvResult
            // 
            this.dvResult.AllowUserToAddRows = false;
            this.dvResult.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PowderBlue;
            this.dvResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dvResult.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dvResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dvResult.ColumnHeadersHeight = 30;
            this.dvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MISI_SEQ_NO,
            this.MISI_TP,
            this.MISI_COM,
            this.MISI_ITM_CD,
            this.MISI_SIM_ITM_CD,
            this.MI_LONGDESC,
            this.MI_MODEL,
            this.MI_BRAND,
            this.MISI_FROM,
            this.MISI_TO,
            this.MISI_PC,
            this.MISI_LOC,
            this.MISI_DOC_NO,
            this.MISI_PROMO});
            this.dvResult.EnableHeadersVisualStyles = false;
            this.dvResult.Location = new System.Drawing.Point(5, 3);
            this.dvResult.Name = "dvResult";
            this.dvResult.ReadOnly = true;
            this.dvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvResult.Size = new System.Drawing.Size(692, 283);
            this.dvResult.TabIndex = 3;
            this.dvResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvResult_CellDoubleClick);
            this.dvResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dvResult_KeyDown);
            // 
            // MISI_SEQ_NO
            // 
            this.MISI_SEQ_NO.DataPropertyName = "MISI_SEQ_NO";
            this.MISI_SEQ_NO.HeaderText = "Seq No";
            this.MISI_SEQ_NO.Name = "MISI_SEQ_NO";
            this.MISI_SEQ_NO.ReadOnly = true;
            this.MISI_SEQ_NO.Visible = false;
            this.MISI_SEQ_NO.Width = 66;
            // 
            // MISI_TP
            // 
            this.MISI_TP.DataPropertyName = "MISI_TP";
            this.MISI_TP.HeaderText = "Type";
            this.MISI_TP.Name = "MISI_TP";
            this.MISI_TP.ReadOnly = true;
            this.MISI_TP.Visible = false;
            this.MISI_TP.Width = 56;
            // 
            // MISI_COM
            // 
            this.MISI_COM.DataPropertyName = "MISI_COM";
            this.MISI_COM.HeaderText = "Company";
            this.MISI_COM.Name = "MISI_COM";
            this.MISI_COM.ReadOnly = true;
            this.MISI_COM.Visible = false;
            this.MISI_COM.Width = 77;
            // 
            // MISI_ITM_CD
            // 
            this.MISI_ITM_CD.DataPropertyName = "MISI_ITM_CD";
            this.MISI_ITM_CD.HeaderText = "Item Code";
            this.MISI_ITM_CD.Name = "MISI_ITM_CD";
            this.MISI_ITM_CD.ReadOnly = true;
            this.MISI_ITM_CD.Visible = false;
            this.MISI_ITM_CD.Width = 82;
            // 
            // MISI_SIM_ITM_CD
            // 
            this.MISI_SIM_ITM_CD.DataPropertyName = "MISI_SIM_ITM_CD";
            this.MISI_SIM_ITM_CD.HeaderText = "Similar Item Code";
            this.MISI_SIM_ITM_CD.Name = "MISI_SIM_ITM_CD";
            this.MISI_SIM_ITM_CD.ReadOnly = true;
            this.MISI_SIM_ITM_CD.Width = 115;
            // 
            // MI_LONGDESC
            // 
            this.MI_LONGDESC.DataPropertyName = "MI_LONGDESC";
            this.MI_LONGDESC.HeaderText = "Description";
            this.MI_LONGDESC.Name = "MI_LONGDESC";
            this.MI_LONGDESC.ReadOnly = true;
            this.MI_LONGDESC.Width = 85;
            // 
            // MI_MODEL
            // 
            this.MI_MODEL.DataPropertyName = "MI_MODEL";
            this.MI_MODEL.HeaderText = "Model";
            this.MI_MODEL.Name = "MI_MODEL";
            this.MI_MODEL.ReadOnly = true;
            this.MI_MODEL.Width = 60;
            // 
            // MI_BRAND
            // 
            this.MI_BRAND.DataPropertyName = "MI_BRAND";
            this.MI_BRAND.HeaderText = "Brand";
            this.MI_BRAND.Name = "MI_BRAND";
            this.MI_BRAND.ReadOnly = true;
            this.MI_BRAND.Width = 60;
            // 
            // MISI_FROM
            // 
            this.MISI_FROM.DataPropertyName = "MISI_FROM";
            this.MISI_FROM.HeaderText = "Valid From";
            this.MISI_FROM.Name = "MISI_FROM";
            this.MISI_FROM.ReadOnly = true;
            this.MISI_FROM.Visible = false;
            this.MISI_FROM.Width = 81;
            // 
            // MISI_TO
            // 
            this.MISI_TO.DataPropertyName = "MISI_TO";
            this.MISI_TO.HeaderText = "Valid To";
            this.MISI_TO.Name = "MISI_TO";
            this.MISI_TO.ReadOnly = true;
            this.MISI_TO.Visible = false;
            this.MISI_TO.Width = 69;
            // 
            // MISI_PC
            // 
            this.MISI_PC.DataPropertyName = "MISI_PC";
            this.MISI_PC.HeaderText = "Profit Center";
            this.MISI_PC.Name = "MISI_PC";
            this.MISI_PC.ReadOnly = true;
            this.MISI_PC.Visible = false;
            this.MISI_PC.Width = 94;
            // 
            // MISI_LOC
            // 
            this.MISI_LOC.DataPropertyName = "MISI_LOC";
            this.MISI_LOC.HeaderText = "Location";
            this.MISI_LOC.Name = "MISI_LOC";
            this.MISI_LOC.ReadOnly = true;
            this.MISI_LOC.Visible = false;
            this.MISI_LOC.Width = 72;
            // 
            // MISI_DOC_NO
            // 
            this.MISI_DOC_NO.DataPropertyName = "MISI_DOC_NO";
            this.MISI_DOC_NO.HeaderText = "Document No";
            this.MISI_DOC_NO.Name = "MISI_DOC_NO";
            this.MISI_DOC_NO.ReadOnly = true;
            this.MISI_DOC_NO.Visible = false;
            this.MISI_DOC_NO.Width = 96;
            // 
            // MISI_PROMO
            // 
            this.MISI_PROMO.DataPropertyName = "MISI_PROMO";
            this.MISI_PROMO.HeaderText = "Promotion Code";
            this.MISI_PROMO.Name = "MISI_PROMO";
            this.MISI_PROMO.ReadOnly = true;
            this.MISI_PROMO.Visible = false;
            this.MISI_PROMO.Width = 108;
            // 
            // SearchSimilarItems
            // 
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(701, 291);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchSimilarItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Similar Items";
            this.Load += new System.EventHandler(this.SearchSimilarItems_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.DataGridView dvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_SEQ_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_TP;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_COM;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_ITM_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_SIM_ITM_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn MI_LONGDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MI_MODEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn MI_BRAND;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_FROM;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_TO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_PC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_LOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_DOC_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MISI_PROMO;
    }
}
