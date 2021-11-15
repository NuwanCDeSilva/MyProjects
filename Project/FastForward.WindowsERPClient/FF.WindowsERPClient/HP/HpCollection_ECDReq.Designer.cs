namespace FF.WindowsERPClient.HP
{
    partial class HpCollection_ECDReq
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.panel_ReqApp = new System.Windows.Forms.Panel();
            this.grv_ViewReqDet = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.uc_ddlReqNo = new System.Windows.Forms.ComboBox();
            this.panel_accountSelect = new System.Windows.Forms.Panel();
            this.grvMpdalPopUp = new System.Windows.Forms.DataGridView();
            this.col_ACCNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_ReqApp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_ViewReqDet)).BeginInit();
            this.panel_accountSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvMpdalPopUp)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(468, 3);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(59, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "OK";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // panel_ReqApp
            // 
            this.panel_ReqApp.Controls.Add(this.grv_ViewReqDet);
            this.panel_ReqApp.Controls.Add(this.label1);
            this.panel_ReqApp.Controls.Add(this.uc_ddlReqNo);
            this.panel_ReqApp.Controls.Add(this.btnSelect);
            this.panel_ReqApp.Location = new System.Drawing.Point(3, 12);
            this.panel_ReqApp.Name = "panel_ReqApp";
            this.panel_ReqApp.Size = new System.Drawing.Size(542, 189);
            this.panel_ReqApp.TabIndex = 1;
            // 
            // grv_ViewReqDet
            // 
            this.grv_ViewReqDet.AllowUserToAddRows = false;
            this.grv_ViewReqDet.AllowUserToDeleteRows = false;
            this.grv_ViewReqDet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_ViewReqDet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.grv_ViewReqDet.Location = new System.Drawing.Point(7, 32);
            this.grv_ViewReqDet.Name = "grv_ViewReqDet";
            this.grv_ViewReqDet.Size = new System.Drawing.Size(520, 139);
            this.grv_ViewReqDet.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "grah_ref";
            this.Column1.HeaderText = "Request#";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "grad_line";
            this.Column2.HeaderText = "Line#";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "grad_req_param";
            this.Column3.HeaderText = "Req. Parameter";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "grah_fuc_cd";
            this.Column4.HeaderText = "Reference to";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "grah_app_stus";
            this.Column5.HeaderText = "Status";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "grah_app_lvl";
            this.Column6.HeaderText = "Approved level";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "grah_remaks";
            this.Column7.HeaderText = "Remarks";
            this.Column7.Name = "Column7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Approved Reques:";
            // 
            // uc_ddlReqNo
            // 
            this.uc_ddlReqNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uc_ddlReqNo.FormattingEnabled = true;
            this.uc_ddlReqNo.Location = new System.Drawing.Point(108, 5);
            this.uc_ddlReqNo.Name = "uc_ddlReqNo";
            this.uc_ddlReqNo.Size = new System.Drawing.Size(203, 21);
            this.uc_ddlReqNo.TabIndex = 1;
            this.uc_ddlReqNo.SelectedIndexChanged += new System.EventHandler(this.uc_ddlReqNo_SelectedIndexChanged);
            // 
            // panel_accountSelect
            // 
            this.panel_accountSelect.Controls.Add(this.grvMpdalPopUp);
            this.panel_accountSelect.Location = new System.Drawing.Point(39, 25);
            this.panel_accountSelect.Name = "panel_accountSelect";
            this.panel_accountSelect.Size = new System.Drawing.Size(506, 178);
            this.panel_accountSelect.TabIndex = 2;
            this.panel_accountSelect.Visible = false;
            // 
            // grvMpdalPopUp
            // 
            this.grvMpdalPopUp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvMpdalPopUp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_ACCNo,
            this.Column10,
            this.Column11});
            this.grvMpdalPopUp.Location = new System.Drawing.Point(13, 3);
            this.grvMpdalPopUp.Name = "grvMpdalPopUp";
            this.grvMpdalPopUp.Size = new System.Drawing.Size(471, 151);
            this.grvMpdalPopUp.TabIndex = 0;
            this.grvMpdalPopUp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grvMpdalPopUp_MouseDoubleClick);
            // 
            // col_ACCNo
            // 
            this.col_ACCNo.DataPropertyName = "HPA_ACC_NO";
            this.col_ACCNo.HeaderText = "Account No.";
            this.col_ACCNo.Name = "col_ACCNo";
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "HPA_ACC_CRE_DT";
            this.Column10.HeaderText = "Created Date";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Customer Name";
            this.Column11.Name = "Column11";
            // 
            // HpCollection_ECDReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 431);
            this.Controls.Add(this.panel_accountSelect);
            this.Controls.Add(this.panel_ReqApp);
            this.Name = "HpCollection_ECDReq";
            this.panel_ReqApp.ResumeLayout(false);
            this.panel_ReqApp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_ViewReqDet)).EndInit();
            this.panel_accountSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvMpdalPopUp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Panel panel_ReqApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox uc_ddlReqNo;
        private System.Windows.Forms.DataGridView grv_ViewReqDet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Panel panel_accountSelect;
        private System.Windows.Forms.DataGridView grvMpdalPopUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ACCNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}