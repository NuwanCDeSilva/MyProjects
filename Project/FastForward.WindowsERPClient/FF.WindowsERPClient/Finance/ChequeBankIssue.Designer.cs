namespace FF.WindowsERPClient.Finance
{
    partial class ChequeBankIssue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChequeBankIssue));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.lblbook = new System.Windows.Forms.Label();
            this.txtBookNo = new System.Windows.Forms.TextBox();
            this.txtStartingNo = new System.Windows.Forms.TextBox();
            this.lblStartingNo = new System.Windows.Forms.Label();
            this.txtnumofpage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEndingNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddToGrid = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvChqBankDets = new System.Windows.Forms.DataGridView();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.ImgBtnAcc = new System.Windows.Forms.Button();
            this.clmlineno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBankAccNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBookNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmpages = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEndingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmdelete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChqBankDets)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 444;
            this.label2.Text = "Bank Account No:";
            // 
            // lblbook
            // 
            this.lblbook.AutoSize = true;
            this.lblbook.Location = new System.Drawing.Point(62, 51);
            this.lblbook.Name = "lblbook";
            this.lblbook.Size = new System.Drawing.Size(52, 13);
            this.lblbook.TabIndex = 446;
            this.lblbook.Text = "Book No:";
            // 
            // txtBookNo
            // 
            this.txtBookNo.Location = new System.Drawing.Point(118, 44);
            this.txtBookNo.Name = "txtBookNo";
            this.txtBookNo.Size = new System.Drawing.Size(100, 20);
            this.txtBookNo.TabIndex = 1;
            this.txtBookNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookNo_KeyDown);
            this.txtBookNo.Leave += new System.EventHandler(this.txtBookNo_Leave);
            // 
            // txtStartingNo
            // 
            this.txtStartingNo.Location = new System.Drawing.Point(118, 71);
            this.txtStartingNo.Name = "txtStartingNo";
            this.txtStartingNo.Size = new System.Drawing.Size(100, 20);
            this.txtStartingNo.TabIndex = 2;
            this.txtStartingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStartingNo_KeyDown);
            this.txtStartingNo.Leave += new System.EventHandler(this.txtStartingNo_Leave);
            // 
            // lblStartingNo
            // 
            this.lblStartingNo.AutoSize = true;
            this.lblStartingNo.Location = new System.Drawing.Point(51, 78);
            this.lblStartingNo.Name = "lblStartingNo";
            this.lblStartingNo.Size = new System.Drawing.Size(63, 13);
            this.lblStartingNo.TabIndex = 448;
            this.lblStartingNo.Text = "Starting No:";
            // 
            // txtnumofpage
            // 
            this.txtnumofpage.Location = new System.Drawing.Point(118, 98);
            this.txtnumofpage.Name = "txtnumofpage";
            this.txtnumofpage.Size = new System.Drawing.Size(100, 20);
            this.txtnumofpage.TabIndex = 3;
            this.txtnumofpage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnumofpage_KeyDown);
            this.txtnumofpage.Leave += new System.EventHandler(this.txtnumofpage_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 450;
            this.label1.Text = "Num of pages:";
            // 
            // txtEndingNo
            // 
            this.txtEndingNo.Location = new System.Drawing.Point(118, 123);
            this.txtEndingNo.Name = "txtEndingNo";
            this.txtEndingNo.ReadOnly = true;
            this.txtEndingNo.Size = new System.Drawing.Size(100, 20);
            this.txtEndingNo.TabIndex = 4;
            this.txtEndingNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEndingNo_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 452;
            this.label3.Text = "Ending No:";
            // 
            // btnAddToGrid
            // 
            this.btnAddToGrid.BackColor = System.Drawing.Color.Transparent;
            this.btnAddToGrid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddToGrid.BackgroundImage")));
            this.btnAddToGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddToGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddToGrid.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnAddToGrid.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnAddToGrid.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnAddToGrid.Location = new System.Drawing.Point(224, 109);
            this.btnAddToGrid.Name = "btnAddToGrid";
            this.btnAddToGrid.Size = new System.Drawing.Size(41, 34);
            this.btnAddToGrid.TabIndex = 5;
            this.btnAddToGrid.UseVisualStyleBackColor = false;
            this.btnAddToGrid.Click += new System.EventHandler(this.btnAddToGrid_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(696, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 22);
            this.btnClose.TabIndex = 458;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClear.Location = new System.Drawing.Point(628, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(66, 22);
            this.btnClear.TabIndex = 457;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(559, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(66, 22);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvChqBankDets
            // 
            this.dgvChqBankDets.AllowUserToAddRows = false;
            this.dgvChqBankDets.AllowUserToResizeColumns = false;
            this.dgvChqBankDets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChqBankDets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmlineno,
            this.clmBankAccNo,
            this.clmBookNo,
            this.clmStartingNo,
            this.clmpages,
            this.clmEndingNo,
            this.clmdelete});
            this.dgvChqBankDets.Location = new System.Drawing.Point(41, 159);
            this.dgvChqBankDets.Name = "dgvChqBankDets";
            this.dgvChqBankDets.ReadOnly = true;
            this.dgvChqBankDets.Size = new System.Drawing.Size(663, 150);
            this.dgvChqBankDets.TabIndex = 459;
            this.dgvChqBankDets.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChqBankDets_CellClick);
            this.dgvChqBankDets.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvChqBankDets_RowHeaderMouseDoubleClick);
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAccountNo.Location = new System.Drawing.Point(118, 12);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(128, 20);
            this.txtAccountNo.TabIndex = 0;
            this.txtAccountNo.DoubleClick += new System.EventHandler(this.txtAccountNo_DoubleClick);
            this.txtAccountNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccountNo_KeyDown);
            this.txtAccountNo.Leave += new System.EventHandler(this.txtAccountNo_Leave);
            // 
            // ImgBtnAcc
            // 
            this.ImgBtnAcc.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.searchicon;
            this.ImgBtnAcc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ImgBtnAcc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImgBtnAcc.ForeColor = System.Drawing.Color.White;
            this.ImgBtnAcc.Location = new System.Drawing.Point(252, 12);
            this.ImgBtnAcc.Name = "ImgBtnAcc";
            this.ImgBtnAcc.Size = new System.Drawing.Size(20, 19);
            this.ImgBtnAcc.TabIndex = 461;
            this.ImgBtnAcc.UseVisualStyleBackColor = true;
            this.ImgBtnAcc.Click += new System.EventHandler(this.ImgBtnAcc_Click);
            // 
            // clmlineno
            // 
            this.clmlineno.HeaderText = "Line No";
            this.clmlineno.Name = "clmlineno";
            this.clmlineno.ReadOnly = true;
            this.clmlineno.Width = 50;
            // 
            // clmBankAccNo
            // 
            dataGridViewCellStyle1.NullValue = null;
            this.clmBankAccNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.clmBankAccNo.HeaderText = "Bank Acc No";
            this.clmBankAccNo.Name = "clmBankAccNo";
            this.clmBankAccNo.ReadOnly = true;
            // 
            // clmBookNo
            // 
            dataGridViewCellStyle2.NullValue = null;
            this.clmBookNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmBookNo.HeaderText = "Book No";
            this.clmBookNo.Name = "clmBookNo";
            this.clmBookNo.ReadOnly = true;
            // 
            // clmStartingNo
            // 
            this.clmStartingNo.HeaderText = "Starting No";
            this.clmStartingNo.Name = "clmStartingNo";
            this.clmStartingNo.ReadOnly = true;
            // 
            // clmpages
            // 
            this.clmpages.HeaderText = "Num of Pages";
            this.clmpages.Name = "clmpages";
            this.clmpages.ReadOnly = true;
            // 
            // clmEndingNo
            // 
            this.clmEndingNo.HeaderText = "Ending No";
            this.clmEndingNo.Name = "clmEndingNo";
            this.clmEndingNo.ReadOnly = true;
            // 
            // clmdelete
            // 
            this.clmdelete.HeaderText = "Delete";
            this.clmdelete.Image = global::FF.WindowsERPClient.Properties.Resources.deleteicon;
            this.clmdelete.Name = "clmdelete";
            this.clmdelete.ReadOnly = true;
            // 
            // ChequeBankIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(766, 472);
            this.Controls.Add(this.ImgBtnAcc);
            this.Controls.Add(this.txtAccountNo);
            this.Controls.Add(this.dgvChqBankDets);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddToGrid);
            this.Controls.Add(this.txtEndingNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtnumofpage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStartingNo);
            this.Controls.Add(this.lblStartingNo);
            this.Controls.Add(this.txtBookNo);
            this.Controls.Add(this.lblbook);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ChequeBankIssue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cheque book assign";
            this.Load += new System.EventHandler(this.ChequeBankIssue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChqBankDets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblbook;
        private System.Windows.Forms.TextBox txtBookNo;
        private System.Windows.Forms.TextBox txtStartingNo;
        private System.Windows.Forms.Label lblStartingNo;
        private System.Windows.Forms.TextBox txtnumofpage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEndingNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddToGrid;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvChqBankDets;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.Button ImgBtnAcc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmlineno;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBankAccNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBookNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmpages;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEndingNo;
        private System.Windows.Forms.DataGridViewImageColumn clmdelete;
    }
}