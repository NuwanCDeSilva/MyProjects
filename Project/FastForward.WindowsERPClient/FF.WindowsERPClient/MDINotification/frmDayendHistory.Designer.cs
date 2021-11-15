namespace FF.WindowsERPClient.MDINotification
{
    partial class frmDayendHistory
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
            this.gvHistory = new System.Windows.Forms.DataGridView();
            this.Upd_com = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.upd_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.upd_pc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.upd_cre_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // gvHistory
            // 
            this.gvHistory.AllowUserToAddRows = false;
            this.gvHistory.AllowUserToDeleteRows = false;
            this.gvHistory.AllowUserToOrderColumns = true;
            this.gvHistory.AllowUserToResizeColumns = false;
            this.gvHistory.AllowUserToResizeRows = false;
            this.gvHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Upd_com,
            this.upd_dt,
            this.upd_pc,
            this.upd_cre_dt});
            this.gvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvHistory.Location = new System.Drawing.Point(0, 0);
            this.gvHistory.MultiSelect = false;
            this.gvHistory.Name = "gvHistory";
            this.gvHistory.ReadOnly = true;
            this.gvHistory.RowHeadersVisible = false;
            this.gvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvHistory.Size = new System.Drawing.Size(548, 306);
            this.gvHistory.TabIndex = 0;
            // 
            // Upd_com
            // 
            this.Upd_com.DataPropertyName = "Upd_com";
            this.Upd_com.HeaderText = "Company";
            this.Upd_com.Name = "Upd_com";
            this.Upd_com.ReadOnly = true;
            this.Upd_com.Visible = false;
            // 
            // upd_dt
            // 
            this.upd_dt.DataPropertyName = "upd_dt";
            this.upd_dt.HeaderText = "Day-end Date";
            this.upd_dt.Name = "upd_dt";
            this.upd_dt.ReadOnly = true;
            this.upd_dt.Width = 150;
            // 
            // upd_pc
            // 
            this.upd_pc.DataPropertyName = "upd_pc";
            this.upd_pc.HeaderText = "Profit Center";
            this.upd_pc.Name = "upd_pc";
            this.upd_pc.ReadOnly = true;
            this.upd_pc.Visible = false;
            // 
            // upd_cre_dt
            // 
            this.upd_cre_dt.DataPropertyName = "upd_cre_dt";
            this.upd_cre_dt.HeaderText = "Created Date/Time";
            this.upd_cre_dt.Name = "upd_cre_dt";
            this.upd_cre_dt.ReadOnly = true;
            this.upd_cre_dt.Width = 220;
            // 
            // frmDayendHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 306);
            this.Controls.Add(this.gvHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDayendHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Day-end History within 10 days";
            this.Load += new System.EventHandler(this.frmDayendHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Upd_com;
        private System.Windows.Forms.DataGridViewTextBoxColumn upd_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn upd_pc;
        private System.Windows.Forms.DataGridViewTextBoxColumn upd_cre_dt;
    }
}