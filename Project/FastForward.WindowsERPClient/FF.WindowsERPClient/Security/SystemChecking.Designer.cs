namespace FF.WindowsERPClient.Security
{
    partial class SystemChecking
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrint1 = new System.Windows.Forms.Button();
            this.btnExpotExcel = new System.Windows.Forms.Button();
            this.btnAutoTransfer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnPrint.Location = new System.Drawing.Point(12, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 37);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Test Printing 1/2 Letter";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrint1
            // 
            this.btnPrint1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnPrint1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnPrint1.Location = new System.Drawing.Point(12, 55);
            this.btnPrint1.Name = "btnPrint1";
            this.btnPrint1.Size = new System.Drawing.Size(93, 37);
            this.btnPrint1.TabIndex = 1;
            this.btnPrint1.Text = "Test Printing Letter";
            this.btnPrint1.UseVisualStyleBackColor = false;
            this.btnPrint1.Click += new System.EventHandler(this.btnPrint1_Click);
            // 
            // btnExpotExcel
            // 
            this.btnExpotExcel.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnExpotExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpotExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnExpotExcel.Location = new System.Drawing.Point(12, 98);
            this.btnExpotExcel.Name = "btnExpotExcel";
            this.btnExpotExcel.Size = new System.Drawing.Size(93, 37);
            this.btnExpotExcel.TabIndex = 2;
            this.btnExpotExcel.Text = "Export Excel 2007";
            this.btnExpotExcel.UseVisualStyleBackColor = false;
            this.btnExpotExcel.Click += new System.EventHandler(this.btnExpotExcel_Click);
            // 
            // btnAutoTransfer
            // 
            this.btnAutoTransfer.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnAutoTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoTransfer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAutoTransfer.Location = new System.Drawing.Point(12, 141);
            this.btnAutoTransfer.Name = "btnAutoTransfer";
            this.btnAutoTransfer.Size = new System.Drawing.Size(93, 37);
            this.btnAutoTransfer.TabIndex = 3;
            this.btnAutoTransfer.Text = "New Company Transfer";
            this.btnAutoTransfer.UseVisualStyleBackColor = false;
            this.btnAutoTransfer.Click += new System.EventHandler(this.btnAutoTransfer_Click);
            // 
            // SystemChecking
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(413, 206);
            this.Controls.Add(this.btnAutoTransfer);
            this.Controls.Add(this.btnExpotExcel);
            this.Controls.Add(this.btnPrint1);
            this.Controls.Add(this.btnPrint);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemChecking";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "System Checking . . . . . . . .";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrint1;
        private System.Windows.Forms.Button btnExpotExcel;
        private System.Windows.Forms.Button btnAutoTransfer;

    }
}
