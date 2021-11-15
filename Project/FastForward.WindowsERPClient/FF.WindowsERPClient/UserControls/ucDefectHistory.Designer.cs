namespace FF.WindowsERPClient.UserControls
{
    partial class ucDefectHistory
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlDefectHistory = new System.Windows.Forms.Panel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.pnlAdancedDetails = new System.Windows.Forms.Panel();
            this.dgvADActualDefecsD10 = new System.Windows.Forms.DataGridView();
            this.SDT_DESCD10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRD_DEF_RMKD10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.dgvSerialDetailsD9 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Jbd_jobnoD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jbd_joblineD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_ITM_CDD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jbd_itm_descD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jbd_modelD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jbd_ser1D9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jbd_warrD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jbd_warr_stusD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_WARRPERIODD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_SUPP_CDD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_INVC_NOD9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_REGND9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pnlDefectHistory.SuspendLayout();
            this.pnlAdancedDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvADActualDefecsD10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialDetailsD9)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDefectHistory
            // 
            this.pnlDefectHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlDefectHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDefectHistory.Controls.Add(this.linkLabel5);
            this.pnlDefectHistory.Controls.Add(this.pnlAdancedDetails);
            this.pnlDefectHistory.Controls.Add(this.linkLabel4);
            this.pnlDefectHistory.Controls.Add(this.dgvSerialDetailsD9);
            this.pnlDefectHistory.Controls.Add(this.linkLabel3);
            this.pnlDefectHistory.Controls.Add(this.linkLabel2);
            this.pnlDefectHistory.Controls.Add(this.linkLabel1);
            this.pnlDefectHistory.Location = new System.Drawing.Point(3, 2);
            this.pnlDefectHistory.Name = "pnlDefectHistory";
            this.pnlDefectHistory.Size = new System.Drawing.Size(768, 483);
            this.pnlDefectHistory.TabIndex = 3;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(593, 196);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(105, 13);
            this.linkLabel5.TabIndex = 4;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Technician Remarks";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // pnlAdancedDetails
            // 
            this.pnlAdancedDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.pnlAdancedDetails.Controls.Add(this.dgvADActualDefecsD10);
            this.pnlAdancedDetails.Location = new System.Drawing.Point(1, 212);
            this.pnlAdancedDetails.Name = "pnlAdancedDetails";
            this.pnlAdancedDetails.Size = new System.Drawing.Size(762, 267);
            this.pnlAdancedDetails.TabIndex = 257;
            // 
            // dgvADActualDefecsD10
            // 
            this.dgvADActualDefecsD10.AllowUserToAddRows = false;
            this.dgvADActualDefecsD10.AllowUserToDeleteRows = false;
            this.dgvADActualDefecsD10.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvADActualDefecsD10.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvADActualDefecsD10.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvADActualDefecsD10.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvADActualDefecsD10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvADActualDefecsD10.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SDT_DESCD10,
            this.SRD_DEF_RMKD10});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvADActualDefecsD10.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvADActualDefecsD10.EnableHeadersVisualStyles = false;
            this.dgvADActualDefecsD10.Location = new System.Drawing.Point(3, 3);
            this.dgvADActualDefecsD10.MultiSelect = false;
            this.dgvADActualDefecsD10.Name = "dgvADActualDefecsD10";
            this.dgvADActualDefecsD10.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvADActualDefecsD10.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvADActualDefecsD10.RowHeadersVisible = false;
            this.dgvADActualDefecsD10.RowHeadersWidth = 20;
            this.dgvADActualDefecsD10.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvADActualDefecsD10.Size = new System.Drawing.Size(754, 261);
            this.dgvADActualDefecsD10.TabIndex = 124;
            // 
            // SDT_DESCD10
            // 
            this.SDT_DESCD10.DataPropertyName = "SDT_DESC";
            this.SDT_DESCD10.HeaderText = "Defects";
            this.SDT_DESCD10.Name = "SDT_DESCD10";
            this.SDT_DESCD10.ReadOnly = true;
            // 
            // SRD_DEF_RMKD10
            // 
            this.SRD_DEF_RMKD10.DataPropertyName = "SRD_DEF_RMK";
            this.SRD_DEF_RMKD10.HeaderText = "Remark";
            this.SRD_DEF_RMKD10.Name = "SRD_DEF_RMKD10";
            this.SRD_DEF_RMKD10.ReadOnly = true;
            this.SRD_DEF_RMKD10.Width = 250;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(419, 196);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(110, 13);
            this.linkLabel4.TabIndex = 3;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Confirmation Remarks";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // dgvSerialDetailsD9
            // 
            this.dgvSerialDetailsD9.AllowUserToAddRows = false;
            this.dgvSerialDetailsD9.AllowUserToDeleteRows = false;
            this.dgvSerialDetailsD9.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvSerialDetailsD9.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSerialDetailsD9.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSerialDetailsD9.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSerialDetailsD9.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSerialDetailsD9.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.Jbd_jobnoD9,
            this.Jbd_joblineD9,
            this.JBD_ITM_CDD9,
            this.Jbd_itm_descD9,
            this.Jbd_modelD9,
            this.Jbd_ser1D9,
            this.Jbd_warrD9,
            this.Jbd_warr_stusD9,
            this.JBD_WARRPERIODD9,
            this.JBD_SUPP_CDD9,
            this.JBD_INVC_NOD9,
            this.JBD_REGND9});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSerialDetailsD9.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSerialDetailsD9.EnableHeadersVisualStyles = false;
            this.dgvSerialDetailsD9.Location = new System.Drawing.Point(2, 1);
            this.dgvSerialDetailsD9.MultiSelect = false;
            this.dgvSerialDetailsD9.Name = "dgvSerialDetailsD9";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSerialDetailsD9.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSerialDetailsD9.RowHeadersVisible = false;
            this.dgvSerialDetailsD9.RowHeadersWidth = 18;
            this.dgvSerialDetailsD9.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSerialDetailsD9.Size = new System.Drawing.Size(759, 191);
            this.dgvSerialDetailsD9.TabIndex = 256;
            this.dgvSerialDetailsD9.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSerialDetailsD9_CellClick);
            this.dgvSerialDetailsD9.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSerialDetailsD9_CellContentClick);
            this.dgvSerialDetailsD9.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvSerialDetailsD9_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "   ";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 25;
            // 
            // Jbd_jobnoD9
            // 
            this.Jbd_jobnoD9.DataPropertyName = "Jbd_jobno";
            this.Jbd_jobnoD9.HeaderText = "Job Number";
            this.Jbd_jobnoD9.Name = "Jbd_jobnoD9";
            this.Jbd_jobnoD9.ReadOnly = true;
            // 
            // Jbd_joblineD9
            // 
            this.Jbd_joblineD9.DataPropertyName = "Jbd_jobline";
            this.Jbd_joblineD9.HeaderText = "Job Line";
            this.Jbd_joblineD9.Name = "Jbd_joblineD9";
            this.Jbd_joblineD9.ReadOnly = true;
            // 
            // JBD_ITM_CDD9
            // 
            this.JBD_ITM_CDD9.DataPropertyName = "JBD_ITM_CD";
            this.JBD_ITM_CDD9.HeaderText = "Item Code";
            this.JBD_ITM_CDD9.Name = "JBD_ITM_CDD9";
            this.JBD_ITM_CDD9.ReadOnly = true;
            // 
            // Jbd_itm_descD9
            // 
            this.Jbd_itm_descD9.DataPropertyName = "Jbd_itm_desc";
            this.Jbd_itm_descD9.HeaderText = "Description";
            this.Jbd_itm_descD9.Name = "Jbd_itm_descD9";
            this.Jbd_itm_descD9.ReadOnly = true;
            // 
            // Jbd_modelD9
            // 
            this.Jbd_modelD9.DataPropertyName = "Jbd_model";
            this.Jbd_modelD9.HeaderText = "Model";
            this.Jbd_modelD9.Name = "Jbd_modelD9";
            this.Jbd_modelD9.ReadOnly = true;
            // 
            // Jbd_ser1D9
            // 
            this.Jbd_ser1D9.DataPropertyName = "Jbd_ser1";
            this.Jbd_ser1D9.HeaderText = "Serial Num";
            this.Jbd_ser1D9.Name = "Jbd_ser1D9";
            this.Jbd_ser1D9.ReadOnly = true;
            // 
            // Jbd_warrD9
            // 
            this.Jbd_warrD9.DataPropertyName = "Jbd_warr";
            this.Jbd_warrD9.HeaderText = "Warr. Num";
            this.Jbd_warrD9.Name = "Jbd_warrD9";
            this.Jbd_warrD9.ReadOnly = true;
            // 
            // Jbd_warr_stusD9
            // 
            this.Jbd_warr_stusD9.DataPropertyName = "Jbd_warr_stus_text";
            this.Jbd_warr_stusD9.HeaderText = "Warr. Status";
            this.Jbd_warr_stusD9.Name = "Jbd_warr_stusD9";
            this.Jbd_warr_stusD9.ReadOnly = true;
            // 
            // JBD_WARRPERIODD9
            // 
            this.JBD_WARRPERIODD9.DataPropertyName = "JBD_WARRPERIOD";
            this.JBD_WARRPERIODD9.HeaderText = "Warr. Period";
            this.JBD_WARRPERIODD9.Name = "JBD_WARRPERIODD9";
            this.JBD_WARRPERIODD9.ReadOnly = true;
            // 
            // JBD_SUPP_CDD9
            // 
            this.JBD_SUPP_CDD9.DataPropertyName = "JBD_SUPP_CD";
            this.JBD_SUPP_CDD9.HeaderText = "Supplier";
            this.JBD_SUPP_CDD9.Name = "JBD_SUPP_CDD9";
            this.JBD_SUPP_CDD9.ReadOnly = true;
            // 
            // JBD_INVC_NOD9
            // 
            this.JBD_INVC_NOD9.DataPropertyName = "JBD_INVC_NO";
            this.JBD_INVC_NOD9.HeaderText = "Invoice Num";
            this.JBD_INVC_NOD9.Name = "JBD_INVC_NOD9";
            this.JBD_INVC_NOD9.ReadOnly = true;
            // 
            // JBD_REGND9
            // 
            this.JBD_REGND9.DataPropertyName = "JBD_REGN";
            this.JBD_REGND9.HeaderText = "Reg. Num";
            this.JBD_REGND9.Name = "JBD_REGND9";
            this.JBD_REGND9.ReadOnly = true;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(263, 196);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(114, 13);
            this.linkLabel3.TabIndex = 2;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Technician Allocations";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(167, 196);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(60, 13);
            this.linkLabel2.TabIndex = 1;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Used Items";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(60, 196);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(77, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Defects details";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ucDefectHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDefectHistory);
            this.Name = "ucDefectHistory";
            this.Size = new System.Drawing.Size(771, 484);
            this.pnlDefectHistory.ResumeLayout(false);
            this.pnlDefectHistory.PerformLayout();
            this.pnlAdancedDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvADActualDefecsD10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialDetailsD9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDefectHistory;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.Panel pnlAdancedDetails;
        private System.Windows.Forms.DataGridView dgvADActualDefecsD10;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.DataGridView dgvSerialDetailsD9;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT_DESCD10;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRD_DEF_RMKD10;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jbd_jobnoD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jbd_joblineD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_ITM_CDD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jbd_itm_descD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jbd_modelD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jbd_ser1D9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jbd_warrD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jbd_warr_stusD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_WARRPERIODD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_SUPP_CDD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_INVC_NOD9;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_REGND9;
    }
}
