namespace FF.WindowsERPClient.Sales
{
    partial class CusmobileNo
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
            this.dgvMobilenos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMobilenos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMobilenos
            // 
            this.dgvMobilenos.AllowUserToAddRows = false;
            this.dgvMobilenos.AllowUserToDeleteRows = false;
            this.dgvMobilenos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMobilenos.Location = new System.Drawing.Point(6, 12);
            this.dgvMobilenos.Name = "dgvMobilenos";
            this.dgvMobilenos.ReadOnly = true;
            this.dgvMobilenos.Size = new System.Drawing.Size(364, 181);
            this.dgvMobilenos.TabIndex = 0;
            this.dgvMobilenos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMobilenos_CellDoubleClick);
            // 
            // CusmobileNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 200);
            this.Controls.Add(this.dgvMobilenos);
            this.Name = "CusmobileNo";
            this.Text = "CusmobileNo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMobilenos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMobilenos;
    }
}