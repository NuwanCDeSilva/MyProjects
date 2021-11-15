namespace FF.WindowsERPClient.CommonSearch
{
    partial class RCCSerialSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCriteria = new System.Windows.Forms.ComboBox();
            this.btn_View = new System.Windows.Forms.Button();
            this.gvSerial = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeqNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn127 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cust_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_Stus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serial2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warrantystatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvSerial)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Text";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(89, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(390, 21);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(4, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Criteria";
            // 
            // cmbCriteria
            // 
            this.cmbCriteria.BackColor = System.Drawing.Color.Lavender;
            this.cmbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriteria.FormattingEnabled = true;
            this.cmbCriteria.Items.AddRange(new object[] {
            "SERIAL",
            "WARRANTY",
            "DOCUMENT",
            "INVOICE NO",
            "ACC NO"});
            this.cmbCriteria.Location = new System.Drawing.Point(89, 2);
            this.cmbCriteria.Name = "cmbCriteria";
            this.cmbCriteria.Size = new System.Drawing.Size(203, 21);
            this.cmbCriteria.TabIndex = 2;
            this.cmbCriteria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCriteria_KeyDown);
            // 
            // btn_View
            // 
            this.btn_View.Image = global::FF.WindowsERPClient.Properties.Resources.specialsearch2icon;
            this.btn_View.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_View.Location = new System.Drawing.Point(480, 2);
            this.btn_View.Name = "btn_View";
            this.btn_View.Size = new System.Drawing.Size(49, 45);
            this.btn_View.TabIndex = 1;
            this.btn_View.Text = "View";
            this.btn_View.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btn_View.UseVisualStyleBackColor = true;
            this.btn_View.Click += new System.EventHandler(this.btn_View_Click);
            // 
            // gvSerial
            // 
            this.gvSerial.AllowUserToAddRows = false;
            this.gvSerial.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.gvSerial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSerial.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvSerial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSerial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSerial.ColumnHeadersHeight = 20;
            this.gvSerial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.Column1,
            this.SeqNo,
            this.itemline,
            this.batchline,
            this.serline,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn127,
            this.Column2,
            this.Column3,
            this.Cust_Code,
            this.Item_Stus,
            this.Brand,
            this.serial2,
            this.warrantystatus});
            this.gvSerial.EnableHeadersVisualStyles = false;
            this.gvSerial.GridColor = System.Drawing.Color.White;
            this.gvSerial.Location = new System.Drawing.Point(0, 76);
            this.gvSerial.MultiSelect = false;
            this.gvSerial.Name = "gvSerial";
            this.gvSerial.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSerial.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvSerial.RowHeadersVisible = false;
            this.gvSerial.RowTemplate.Height = 16;
            this.gvSerial.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSerial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSerial.Size = new System.Drawing.Size(984, 353);
            this.gvSerial.TabIndex = 132;
            this.gvSerial.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSerial_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "serial_no";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn10.HeaderText = "Serial";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 160;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "warranty_no";
            this.dataGridViewTextBoxColumn11.HeaderText = "Warranty";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 160;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Item_Code";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn12.HeaderText = "Item";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 90;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Doc_No";
            this.Column1.HeaderText = "Document";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // SeqNo
            // 
            this.SeqNo.DataPropertyName = "ITS_SEQ_NO";
            this.SeqNo.HeaderText = "ITS_SEQ_NO";
            this.SeqNo.Name = "SeqNo";
            this.SeqNo.ReadOnly = true;
            this.SeqNo.Visible = false;
            this.SeqNo.Width = 5;
            // 
            // itemline
            // 
            this.itemline.DataPropertyName = "ITS_ITM_LINE";
            this.itemline.HeaderText = "ITS_ITM_LINE";
            this.itemline.Name = "itemline";
            this.itemline.ReadOnly = true;
            this.itemline.Visible = false;
            this.itemline.Width = 5;
            // 
            // batchline
            // 
            this.batchline.DataPropertyName = "ITS_BATCH_LINE";
            this.batchline.HeaderText = "ITS_BATCH_LINE";
            this.batchline.Name = "batchline";
            this.batchline.ReadOnly = true;
            this.batchline.Visible = false;
            this.batchline.Width = 5;
            // 
            // serline
            // 
            this.serline.DataPropertyName = "ITS_SER_LINE";
            this.serline.HeaderText = "ITS_SER_LINE";
            this.serline.Name = "serline";
            this.serline.ReadOnly = true;
            this.serline.Visible = false;
            this.serline.Width = 5;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "Inv_No";
            this.dataGridViewTextBoxColumn20.HeaderText = "Invoice";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Width = 120;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "Acc_No";
            this.dataGridViewTextBoxColumn21.HeaderText = "Account";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Width = 50;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.DataPropertyName = "War_Period";
            this.dataGridViewTextBoxColumn22.HeaderText = "Period";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Width = 50;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.DataPropertyName = "War_Rem";
            this.dataGridViewTextBoxColumn23.HeaderText = "Remarks";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.DataPropertyName = "Cust_Name";
            this.dataGridViewTextBoxColumn24.HeaderText = "Customer";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Width = 120;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "Cust_Addr";
            this.dataGridViewTextBoxColumn25.HeaderText = "Address";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "Inv_Date";
            this.dataGridViewTextBoxColumn26.HeaderText = "Inv. Date";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.Width = 60;
            // 
            // dataGridViewTextBoxColumn127
            // 
            this.dataGridViewTextBoxColumn127.DataPropertyName = "Tel";
            this.dataGridViewTextBoxColumn127.HeaderText = "Telephone";
            this.dataGridViewTextBoxColumn127.Name = "dataGridViewTextBoxColumn127";
            this.dataGridViewTextBoxColumn127.ReadOnly = true;
            this.dataGridViewTextBoxColumn127.Width = 60;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "its_ser_id";
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 5;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "SYS";
            this.Column3.HeaderText = "Sys";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // Cust_Code
            // 
            this.Cust_Code.DataPropertyName = "Cust_Code";
            this.Cust_Code.HeaderText = "Cust Code";
            this.Cust_Code.Name = "Cust_Code";
            this.Cust_Code.ReadOnly = true;
            this.Cust_Code.Visible = false;
            this.Cust_Code.Width = 10;
            // 
            // Item_Stus
            // 
            this.Item_Stus.DataPropertyName = "Item_Stus";
            this.Item_Stus.HeaderText = "Item Status";
            this.Item_Stus.Name = "Item_Stus";
            this.Item_Stus.ReadOnly = true;
            this.Item_Stus.Visible = false;
            this.Item_Stus.Width = 10;
            // 
            // Brand
            // 
            this.Brand.DataPropertyName = "Brand";
            this.Brand.HeaderText = "Brand";
            this.Brand.Name = "Brand";
            this.Brand.ReadOnly = true;
            this.Brand.Visible = false;
            this.Brand.Width = 10;
            // 
            // serial2
            // 
            this.serial2.DataPropertyName = "Serial2";
            this.serial2.HeaderText = "serial2";
            this.serial2.Name = "serial2";
            this.serial2.ReadOnly = true;
            this.serial2.Visible = false;
            this.serial2.Width = 10;
            // 
            // warrantystatus
            // 
            this.warrantystatus.DataPropertyName = "warrantystatus";
            this.warrantystatus.HeaderText = "warrantystatus";
            this.warrantystatus.Name = "warrantystatus";
            this.warrantystatus.ReadOnly = true;
            this.warrantystatus.Visible = false;
            this.warrantystatus.Width = 10;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.SteelBlue;
            this.label14.ForeColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(1, 62);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(982, 14);
            this.label14.TabIndex = 131;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(86, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 13);
            this.label2.TabIndex = 133;
            this.label2.Text = "Please enter minimum 5 characters";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // RCCSerialSearch
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(984, 431);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btn_View);
            this.Controls.Add(this.cmbCriteria);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gvSerial);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "RCCSerialSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Serial/Warranty Search - RCC";
            this.Load += new System.EventHandler(this.RCCSerialSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvSerial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCriteria;
        private System.Windows.Forms.Button btn_View;
        private System.Windows.Forms.DataGridView gvSerial;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeqNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemline;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchline;
        private System.Windows.Forms.DataGridViewTextBoxColumn serline;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn127;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cust_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_Stus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn serial2;
        private System.Windows.Forms.DataGridViewTextBoxColumn warrantystatus;
        private System.Windows.Forms.Label label2;
    }
}
