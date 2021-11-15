namespace FF.WindowsERPClient.CommonSearch
{
    partial class ViewSubSerialItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewSubSerialItems));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dvResult = new System.Windows.Forms.DataGridView();
            this.IRSMS_SER_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_SER_LINE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_WARR_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_ITM_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MI_LONGDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MI_MODEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MI_BRAND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_ITM_STUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_SUB_SER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_MFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_TP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_WARR_PERIOD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_WARR_REM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IRSMS_ACT = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.pnlMain.Size = new System.Drawing.Size(633, 157);
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
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.PaleVioletRed;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.PaleVioletRed;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dvResult.ColumnHeadersHeight = 30;
            this.dvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IRSMS_SER_ID,
            this.IRSMS_SER_LINE,
            this.IRSMS_WARR_NO,
            this.IRSMS_ITM_CD,
            this.MI_LONGDESC,
            this.MI_MODEL,
            this.MI_BRAND,
            this.IRSMS_ITM_STUS,
            this.IRSMS_SUB_SER,
            this.IRSMS_MFC,
            this.IRSMS_TP,
            this.IRSMS_WARR_PERIOD,
            this.IRSMS_WARR_REM,
            this.IRSMS_ACT});
            this.dvResult.EnableHeadersVisualStyles = false;
            this.dvResult.Location = new System.Drawing.Point(5, 3);
            this.dvResult.Name = "dvResult";
            this.dvResult.ReadOnly = true;
            this.dvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvResult.Size = new System.Drawing.Size(626, 152);
            this.dvResult.TabIndex = 3;
            this.dvResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvResult_CellDoubleClick);
            this.dvResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dvResult_KeyDown);
            // 
            // IRSMS_SER_ID
            // 
            this.IRSMS_SER_ID.DataPropertyName = "IRSMS_SER_ID";
            this.IRSMS_SER_ID.HeaderText = "Serial ID";
            this.IRSMS_SER_ID.Name = "IRSMS_SER_ID";
            this.IRSMS_SER_ID.ReadOnly = true;
            this.IRSMS_SER_ID.Visible = false;
            this.IRSMS_SER_ID.Width = 72;
            // 
            // IRSMS_SER_LINE
            // 
            this.IRSMS_SER_LINE.DataPropertyName = "IRSMS_SER_LINE";
            this.IRSMS_SER_LINE.HeaderText = "Line No";
            this.IRSMS_SER_LINE.Name = "IRSMS_SER_LINE";
            this.IRSMS_SER_LINE.ReadOnly = true;
            this.IRSMS_SER_LINE.Visible = false;
            this.IRSMS_SER_LINE.Width = 67;
            // 
            // IRSMS_WARR_NO
            // 
            this.IRSMS_WARR_NO.DataPropertyName = "IRSMS_WARR_NO";
            this.IRSMS_WARR_NO.HeaderText = "Warranty No";
            this.IRSMS_WARR_NO.Name = "IRSMS_WARR_NO";
            this.IRSMS_WARR_NO.ReadOnly = true;
            this.IRSMS_WARR_NO.Visible = false;
            this.IRSMS_WARR_NO.Width = 94;
            // 
            // IRSMS_ITM_CD
            // 
            this.IRSMS_ITM_CD.HeaderText = "Item Code";
            this.IRSMS_ITM_CD.Name = "IRSMS_ITM_CD";
            this.IRSMS_ITM_CD.ReadOnly = true;
            this.IRSMS_ITM_CD.Width = 82;
            // 
            // MI_LONGDESC
            // 
            this.MI_LONGDESC.HeaderText = "Description";
            this.MI_LONGDESC.Name = "MI_LONGDESC";
            this.MI_LONGDESC.ReadOnly = true;
            this.MI_LONGDESC.Width = 85;
            // 
            // MI_MODEL
            // 
            this.MI_MODEL.HeaderText = "Model";
            this.MI_MODEL.Name = "MI_MODEL";
            this.MI_MODEL.ReadOnly = true;
            this.MI_MODEL.Width = 60;
            // 
            // MI_BRAND
            // 
            this.MI_BRAND.HeaderText = "Brand";
            this.MI_BRAND.Name = "MI_BRAND";
            this.MI_BRAND.ReadOnly = true;
            this.MI_BRAND.Width = 60;
            // 
            // IRSMS_ITM_STUS
            // 
            this.IRSMS_ITM_STUS.HeaderText = "Item Status";
            this.IRSMS_ITM_STUS.Name = "IRSMS_ITM_STUS";
            this.IRSMS_ITM_STUS.ReadOnly = true;
            this.IRSMS_ITM_STUS.Visible = false;
            this.IRSMS_ITM_STUS.Width = 88;
            // 
            // IRSMS_SUB_SER
            // 
            this.IRSMS_SUB_SER.HeaderText = "Sub Serial No";
            this.IRSMS_SUB_SER.Name = "IRSMS_SUB_SER";
            this.IRSMS_SUB_SER.ReadOnly = true;
            this.IRSMS_SUB_SER.Width = 95;
            // 
            // IRSMS_MFC
            // 
            this.IRSMS_MFC.HeaderText = "MFC";
            this.IRSMS_MFC.Name = "IRSMS_MFC";
            this.IRSMS_MFC.ReadOnly = true;
            this.IRSMS_MFC.Width = 53;
            // 
            // IRSMS_TP
            // 
            this.IRSMS_TP.HeaderText = "Item Type";
            this.IRSMS_TP.Name = "IRSMS_TP";
            this.IRSMS_TP.ReadOnly = true;
            this.IRSMS_TP.Width = 81;
            // 
            // IRSMS_WARR_PERIOD
            // 
            this.IRSMS_WARR_PERIOD.HeaderText = "Warr Period";
            this.IRSMS_WARR_PERIOD.Name = "IRSMS_WARR_PERIOD";
            this.IRSMS_WARR_PERIOD.ReadOnly = true;
            this.IRSMS_WARR_PERIOD.Visible = false;
            this.IRSMS_WARR_PERIOD.Width = 89;
            // 
            // IRSMS_WARR_REM
            // 
            this.IRSMS_WARR_REM.HeaderText = "Warr Remarks";
            this.IRSMS_WARR_REM.Name = "IRSMS_WARR_REM";
            this.IRSMS_WARR_REM.ReadOnly = true;
            this.IRSMS_WARR_REM.Visible = false;
            // 
            // IRSMS_ACT
            // 
            this.IRSMS_ACT.HeaderText = "Active";
            this.IRSMS_ACT.Name = "IRSMS_ACT";
            this.IRSMS_ACT.ReadOnly = true;
            this.IRSMS_ACT.Visible = false;
            this.IRSMS_ACT.Width = 62;
            // 
            // ViewSubSerialItems
            // 
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(633, 157);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewSubSerialItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sub Item Serials";
            this.Load += new System.EventHandler(this.ViewSubSerialItems_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.DataGridView dvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_SER_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_SER_LINE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_WARR_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_ITM_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn MI_LONGDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MI_MODEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn MI_BRAND;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_ITM_STUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_SUB_SER;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_MFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_TP;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_WARR_PERIOD;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_WARR_REM;
        private System.Windows.Forms.DataGridViewTextBoxColumn IRSMS_ACT;
    }
}
