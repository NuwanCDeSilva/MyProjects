namespace FF.WindowsERPClient.CommonSearch
{
    partial class CommonSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonSearch));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dvResult = new System.Windows.Forms.DataGridView();
            this.grupByWord = new System.Windows.Forms.GroupBox();
            this.txtSearchbyword = new System.Windows.Forms.TextBox();
            this.grupByKey = new System.Windows.Forms.GroupBox();
            this.cmbSearchbykey = new System.Windows.Forms.ComboBox();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvResult)).BeginInit();
            this.grupByWord.SuspendLayout();
            this.grupByKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dvResult);
            this.pnlMain.Controls.Add(this.grupByWord);
            this.pnlMain.Controls.Add(this.grupByKey);
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
            this.dvResult.AllowUserToResizeColumns = false;
            this.dvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dvResult.BackgroundColor = System.Drawing.Color.LightGray;
            this.dvResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvResult.Location = new System.Drawing.Point(5, 61);
            this.dvResult.Name = "dvResult";
            this.dvResult.ReadOnly = true;
            this.dvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvResult.Size = new System.Drawing.Size(692, 338);
            this.dvResult.TabIndex = 3;
            this.dvResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvResult_CellDoubleClick);
            this.dvResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dvResult_KeyDown);
            // 
            // grupByWord
            // 
            this.grupByWord.Controls.Add(this.txtSearchbyword);
            this.grupByWord.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupByWord.ForeColor = System.Drawing.Color.Black;
            this.grupByWord.Location = new System.Drawing.Point(351, 2);
            this.grupByWord.Name = "grupByWord";
            this.grupByWord.Size = new System.Drawing.Size(344, 53);
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
            this.txtSearchbyword.Size = new System.Drawing.Size(330, 21);
            this.txtSearchbyword.TabIndex = 0;
            this.txtSearchbyword.TextChanged += new System.EventHandler(this.txtSearchbyword_TextChanged);
            this.txtSearchbyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchbyword_KeyDown);
            // 
            // grupByKey
            // 
            this.grupByKey.Controls.Add(this.cmbSearchbykey);
            this.grupByKey.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupByKey.ForeColor = System.Drawing.Color.Black;
            this.grupByKey.Location = new System.Drawing.Point(5, 2);
            this.grupByKey.Name = "grupByKey";
            this.grupByKey.Size = new System.Drawing.Size(344, 53);
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
            this.cmbSearchbykey.Location = new System.Drawing.Point(6, 24);
            this.cmbSearchbykey.Name = "cmbSearchbykey";
            this.cmbSearchbykey.Size = new System.Drawing.Size(330, 21);
            this.cmbSearchbykey.TabIndex = 1;
            this.cmbSearchbykey.SelectedIndexChanged += new System.EventHandler(this.cmbSearchbykey_SelectedIndexChanged);
            this.cmbSearchbykey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSearchbykey_KeyDown);
            // 
            // CommonSearch
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
            this.Name = "CommonSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.CommonSearch_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvResult)).EndInit();
            this.grupByWord.ResumeLayout(false);
            this.grupByWord.PerformLayout();
            this.grupByKey.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox grupByKey;
        private System.Windows.Forms.GroupBox grupByWord;
        public System.Windows.Forms.TextBox txtSearchbyword;
        public System.Windows.Forms.DataGridView dvResult;
        public System.Windows.Forms.ComboBox cmbSearchbykey;
    }
}
