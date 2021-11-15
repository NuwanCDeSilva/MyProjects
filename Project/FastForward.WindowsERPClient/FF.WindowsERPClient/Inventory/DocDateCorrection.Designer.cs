namespace FF.WindowsERPClient.Inventory
{
    partial class DocDateCorrection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocDateCorrection));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcess = new System.Windows.Forms.ToolStripButton();
            this.lblBackDateInfor = new System.Windows.Forms.ToolStripLabel();
            this.dtpCorrectDate = new System.Windows.Forms.DateTimePicker();
            this.txtDocNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSavePermission = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearch_RecLocation = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlCorrection = new System.Windows.Forms.Panel();
            this.btnCor = new System.Windows.Forms.Button();
            this.gvCor = new System.Windows.Forms.DataGridView();
            this.btnCorF = new System.Windows.Forms.Button();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlCorrection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCor)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.toolStripSeparator2,
            this.btnProcess,
            this.lblBackDateInfor});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(592, 25);
            this.toolStrip1.TabIndex = 10;
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
            this.btnClear.Size = new System.Drawing.Size(60, 22);
            this.btnClear.Text = "Clear";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnProcess
            // 
            this.btnProcess.AutoSize = false;
            this.btnProcess.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcess.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Padding = new System.Windows.Forms.Padding(2);
            this.btnProcess.Size = new System.Drawing.Size(60, 22);
            this.btnProcess.Text = "Process";
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // lblBackDateInfor
            // 
            this.lblBackDateInfor.Name = "lblBackDateInfor";
            this.lblBackDateInfor.Size = new System.Drawing.Size(0, 0);
            // 
            // dtpCorrectDate
            // 
            this.dtpCorrectDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpCorrectDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCorrectDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCorrectDate.Location = new System.Drawing.Point(3, 4);
            this.dtpCorrectDate.Name = "dtpCorrectDate";
            this.dtpCorrectDate.Size = new System.Drawing.Size(123, 21);
            this.dtpCorrectDate.TabIndex = 11;
            // 
            // txtDocNo
            // 
            this.txtDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocNo.Location = new System.Drawing.Point(132, 4);
            this.txtDocNo.Name = "txtDocNo";
            this.txtDocNo.Size = new System.Drawing.Size(219, 20);
            this.txtDocNo.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Company";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(61, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 20);
            this.textBox1.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Process";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(0, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(592, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Discount Update";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSavePermission);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnSearch_RecLocation);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(3, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 10);
            this.panel1.TabIndex = 17;
            this.panel1.Visible = false;
            // 
            // btnSavePermission
            // 
            this.btnSavePermission.Location = new System.Drawing.Point(511, 4);
            this.btnSavePermission.Name = "btnSavePermission";
            this.btnSavePermission.Size = new System.Drawing.Size(75, 23);
            this.btnSavePermission.TabIndex = 37;
            this.btnSavePermission.Text = "Process";
            this.btnSavePermission.UseVisualStyleBackColor = true;
            this.btnSavePermission.Click += new System.EventHandler(this.btnSavePermission_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(234, 4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(44, 23);
            this.btnView.TabIndex = 36;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(131, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(102, 20);
            this.textBox3.TabIndex = 35;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(131, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(446, 342);
            this.listBox1.TabIndex = 34;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(5, 55);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 319);
            this.checkedListBox1.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Category";
            // 
            // btnSearch_RecLocation
            // 
            this.btnSearch_RecLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch_RecLocation.BackgroundImage")));
            this.btnSearch_RecLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch_RecLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch_RecLocation.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch_RecLocation.Location = new System.Drawing.Point(71, 20);
            this.btnSearch_RecLocation.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSearch_RecLocation.Name = "btnSearch_RecLocation";
            this.btnSearch_RecLocation.Size = new System.Drawing.Size(20, 20);
            this.btnSearch_RecLocation.TabIndex = 31;
            this.btnSearch_RecLocation.UseVisualStyleBackColor = true;
            this.btnSearch_RecLocation.Click += new System.EventHandler(this.btnSearch_RecLocation_Click);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(5, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(65, 20);
            this.textBox2.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Location";
            // 
            // pnlCorrection
            // 
            this.pnlCorrection.Controls.Add(this.btnCorF);
            this.pnlCorrection.Controls.Add(this.gvCor);
            this.pnlCorrection.Controls.Add(this.btnCor);
            this.pnlCorrection.Location = new System.Drawing.Point(0, 315);
            this.pnlCorrection.Name = "pnlCorrection";
            this.pnlCorrection.Size = new System.Drawing.Size(589, 144);
            this.pnlCorrection.TabIndex = 18;
            this.pnlCorrection.Visible = false;
            // 
            // btnCor
            // 
            this.btnCor.Location = new System.Drawing.Point(511, 3);
            this.btnCor.Name = "btnCor";
            this.btnCor.Size = new System.Drawing.Size(75, 23);
            this.btnCor.TabIndex = 16;
            this.btnCor.Text = "Process";
            this.btnCor.UseVisualStyleBackColor = true;
            this.btnCor.Click += new System.EventHandler(this.btnCor_Click);
            // 
            // gvCor
            // 
            this.gvCor.AllowUserToAddRows = false;
            this.gvCor.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.gvCor.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCor.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvCor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvCor.ColumnHeadersHeight = 20;
            this.gvCor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column5,
            this.Column6});
            this.gvCor.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvCor.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvCor.EnableHeadersVisualStyles = false;
            this.gvCor.GridColor = System.Drawing.Color.White;
            this.gvCor.Location = new System.Drawing.Point(6, 4);
            this.gvCor.MultiSelect = false;
            this.gvCor.Name = "gvCor";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCor.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvCor.RowHeadersVisible = false;
            this.gvCor.RowTemplate.Height = 18;
            this.gvCor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCor.Size = new System.Drawing.Size(502, 136);
            this.gvCor.TabIndex = 106;
            // 
            // btnCorF
            // 
            this.btnCorF.Location = new System.Drawing.Point(511, 26);
            this.btnCorF.Name = "btnCorF";
            this.btnCorF.Size = new System.Drawing.Size(75, 23);
            this.btnCorF.TabIndex = 107;
            this.btnCorF.Text = "Find";
            this.btnCorF.UseVisualStyleBackColor = true;
            this.btnCorF.Click += new System.EventHandler(this.btnCorF_Click);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "";
            this.Column3.Name = "Column3";
            this.Column3.Width = 20;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ith_doc_date";
            this.Column1.HeaderText = "date";
            this.Column1.Name = "Column1";
            this.Column1.Width = 75;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ith_com";
            this.Column4.HeaderText = "com";
            this.Column4.Name = "Column4";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ith_doc_no";
            this.Column2.HeaderText = "doc";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "ith_oth_com";
            this.Column5.HeaderText = "oth com";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "ith_loc";
            this.Column6.HeaderText = "do loc";
            this.Column6.Name = "Column6";
            // 
            // DocDateCorrection
            // 
            this.ClientSize = new System.Drawing.Size(592, 461);
            this.Controls.Add(this.pnlCorrection);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDocNo);
            this.Controls.Add(this.dtpCorrectDate);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DocDateCorrection";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Document Date Correction Entry";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlCorrection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvCor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnProcess;
        private System.Windows.Forms.ToolStripLabel lblBackDateInfor;
        private System.Windows.Forms.DateTimePicker dtpCorrectDate;
        private System.Windows.Forms.TextBox txtDocNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearch_RecLocation;
        private System.Windows.Forms.Button btnSavePermission;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel pnlCorrection;
        private System.Windows.Forms.Button btnCor;
        private System.Windows.Forms.DataGridView gvCor;
        private System.Windows.Forms.Button btnCorF;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}
