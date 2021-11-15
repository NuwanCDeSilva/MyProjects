namespace FF.WindowsERPClient.CommonSearch
{
    partial class CommonSearchDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonSearchDate));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dvResult = new System.Windows.Forms.DataGridView();
            this.grupByWord = new System.Windows.Forms.GroupBox();
            this.txtSearchbyword = new System.Windows.Forms.TextBox();
            this.grupByKey = new System.Windows.Forms.GroupBox();
            this.cmbSearchbykey = new System.Windows.Forms.ComboBox();
            this.grupDateSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvResult)).BeginInit();
            this.grupByWord.SuspendLayout();
            this.grupByKey.SuspendLayout();
            this.grupDateSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dvResult);
            this.pnlMain.Controls.Add(this.grupByWord);
            this.pnlMain.Controls.Add(this.grupByKey);
            this.pnlMain.Controls.Add(this.grupDateSearch);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(701, 402);
            this.pnlMain.TabIndex = 0;
            // 
            // dvResult
            // 
            this.dvResult.AllowUserToAddRows = false;
            this.dvResult.AllowUserToDeleteRows = false;
            this.dvResult.AllowUserToResizeRows = false;
            this.dvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dvResult.BackgroundColor = System.Drawing.Color.LightGray;
            this.dvResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvResult.Location = new System.Drawing.Point(5, 117);
            this.dvResult.Name = "dvResult";
            this.dvResult.ReadOnly = true;
            this.dvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvResult.Size = new System.Drawing.Size(692, 281);
            this.dvResult.TabIndex = 3;
            this.dvResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvResult_CellDoubleClick);
            this.dvResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dvResult_KeyDown);
            // 
            // grupByWord
            // 
            this.grupByWord.Controls.Add(this.txtSearchbyword);
            this.grupByWord.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupByWord.ForeColor = System.Drawing.Color.Black;
            this.grupByWord.Location = new System.Drawing.Point(169, 55);
            this.grupByWord.Name = "grupByWord";
            this.grupByWord.Size = new System.Drawing.Size(528, 55);
            this.grupByWord.TabIndex = 2;
            this.grupByWord.TabStop = false;
            this.grupByWord.Text = "Search by word";
            // 
            // txtSearchbyword
            // 
            this.txtSearchbyword.BackColor = System.Drawing.Color.White;
            this.txtSearchbyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchbyword.ForeColor = System.Drawing.Color.RoyalBlue;
            this.txtSearchbyword.Location = new System.Drawing.Point(6, 24);
            this.txtSearchbyword.Name = "txtSearchbyword";
            this.txtSearchbyword.Size = new System.Drawing.Size(516, 21);
            this.txtSearchbyword.TabIndex = 0;
            this.txtSearchbyword.TextChanged += new System.EventHandler(this.txtSearchbyword_TextChanged);
            this.txtSearchbyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchbyword_KeyDown);
            // 
            // grupByKey
            // 
            this.grupByKey.Controls.Add(this.cmbSearchbykey);
            this.grupByKey.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupByKey.ForeColor = System.Drawing.Color.Black;
            this.grupByKey.Location = new System.Drawing.Point(169, 2);
            this.grupByKey.Name = "grupByKey";
            this.grupByKey.Size = new System.Drawing.Size(528, 53);
            this.grupByKey.TabIndex = 1;
            this.grupByKey.TabStop = false;
            this.grupByKey.Text = "Search by key";
            // 
            // cmbSearchbykey
            // 
            this.cmbSearchbykey.BackColor = System.Drawing.Color.White;
            this.cmbSearchbykey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchbykey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSearchbykey.ForeColor = System.Drawing.Color.RoyalBlue;
            this.cmbSearchbykey.FormattingEnabled = true;
            this.cmbSearchbykey.Location = new System.Drawing.Point(6, 25);
            this.cmbSearchbykey.Name = "cmbSearchbykey";
            this.cmbSearchbykey.Size = new System.Drawing.Size(516, 21);
            this.cmbSearchbykey.TabIndex = 1;
            this.cmbSearchbykey.SelectedIndexChanged += new System.EventHandler(this.cmbSearchbykey_SelectedIndexChanged);
            this.cmbSearchbykey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSearchbykey_KeyDown);
            // 
            // grupDateSearch
            // 
            this.grupDateSearch.Controls.Add(this.btnSearch);
            this.grupDateSearch.Controls.Add(this.dtpTo);
            this.grupDateSearch.Controls.Add(this.label2);
            this.grupDateSearch.Controls.Add(this.dtpFrom);
            this.grupDateSearch.Controls.Add(this.label1);
            this.grupDateSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupDateSearch.ForeColor = System.Drawing.Color.Black;
            this.grupDateSearch.Location = new System.Drawing.Point(5, 2);
            this.grupDateSearch.Name = "grupDateSearch";
            this.grupDateSearch.Size = new System.Drawing.Size(161, 108);
            this.grupDateSearch.TabIndex = 0;
            this.grupDateSearch.TabStop = false;
            this.grupDateSearch.Text = "Search by period";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = global::FF.WindowsERPClient.Properties.Resources.specialsearch2icon;
            this.btnSearch.Location = new System.Drawing.Point(46, 67);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(104, 37);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.CustomFormat = "dd/MMM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(46, 44);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(104, 21);
            this.dtpTo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.CustomFormat = "dd/MMM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(46, 20);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(104, 21);
            this.dtpFrom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // CommonSearchDate
            // 
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(701, 402);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommonSearchDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.CommonSearchDate_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvResult)).EndInit();
            this.grupByWord.ResumeLayout(false);
            this.grupByWord.PerformLayout();
            this.grupByKey.ResumeLayout(false);
            this.grupDateSearch.ResumeLayout(false);
            this.grupDateSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox grupDateSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grupByKey;
        private System.Windows.Forms.ComboBox cmbSearchbykey;
        private System.Windows.Forms.GroupBox grupByWord;
        public System.Windows.Forms.TextBox txtSearchbyword;
        private System.Windows.Forms.Button btnSearch;
        public System.Windows.Forms.DataGridView dvResult;
        public System.Windows.Forms.DateTimePicker dtpTo;
        public System.Windows.Forms.DateTimePicker dtpFrom;
    }
}
