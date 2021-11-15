namespace FF.WindowsERPClient.CommonSearch
{
    partial class RegistrationCollection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.ucPayModes1 = new FF.WindowsERPClient.UserControls.ucPayModes();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotCharge = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gvItem = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.InvItm_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvItm_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvItm_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvItm_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvItm_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvItm_Charge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvItm_Serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvItm_Search = new System.Windows.Forms.DataGridViewImageColumn();
            this.InvItm_Serial2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvItm_Warranty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Navy;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(997, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registration Collection";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(154)))), ((int)(((byte)(205)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(928, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 22);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(178)))), ((int)(((byte)(238)))));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(862, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(67, 22);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Location = new System.Drawing.Point(796, 0);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(67, 22);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSubmit.UseVisualStyleBackColor = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(997, 442);
            this.pnlMain.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.ucPayModes1);
            this.panel3.Location = new System.Drawing.Point(-1, 232);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(997, 209);
            this.panel3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(78)))), ((int)(((byte)(139)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(997, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Payment Detail";
            // 
            // ucPayModes1
            // 
            this.ucPayModes1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ucPayModes1.CurrancyAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucPayModes1.CurrancyCode = "";
            this.ucPayModes1.Customer_Code = null;
            this.ucPayModes1.Date = new System.DateTime(((long)(0)));
            this.ucPayModes1.ExchangeRate = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucPayModes1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucPayModes1.HavePayModes = true;
            this.ucPayModes1.InvoiceItemList = null;
            this.ucPayModes1.InvoiceNo = null;
            this.ucPayModes1.IsDutyFree = false;
            this.ucPayModes1.IsZeroAllow = false;
            this.ucPayModes1.ItemList = null;
            this.ucPayModes1.Location = new System.Drawing.Point(-3, 16);
            this.ucPayModes1.Mobile = null;
            this.ucPayModes1.Name = "ucPayModes1";
            this.ucPayModes1.SerialList = null;
            this.ucPayModes1.Size = new System.Drawing.Size(998, 191);
            this.ucPayModes1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.gvItem);
            this.panel1.Location = new System.Drawing.Point(1, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(995, 209);
            this.panel1.TabIndex = 0;
            // 
            // lblTotCharge
            // 
            this.lblTotCharge.BackColor = System.Drawing.Color.Gold;
            this.lblTotCharge.Location = new System.Drawing.Point(128, 0);
            this.lblTotCharge.Name = "lblTotCharge";
            this.lblTotCharge.Size = new System.Drawing.Size(139, 16);
            this.lblTotCharge.TabIndex = 5;
            this.lblTotCharge.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DarkBlue;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Total Payable Charge";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(78)))), ((int)(((byte)(139)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(995, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Allow Item Detail";
            // 
            // gvItem
            // 
            this.gvItem.AllowUserToAddRows = false;
            this.gvItem.AllowUserToDeleteRows = false;
            this.gvItem.AllowUserToOrderColumns = true;
            this.gvItem.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvItem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvItem.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(224)))));
            this.gvItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvItem.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvItem.ColumnHeadersHeight = 20;
            this.gvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvItm_No,
            this.InvItm_Item,
            this.InvItm_Description,
            this.InvItm_Status,
            this.InvItm_Qty,
            this.InvItm_Charge,
            this.InvItm_Serial,
            this.InvItm_Search,
            this.InvItm_Serial2,
            this.InvItm_Warranty});
            this.gvItem.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvItem.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvItem.EnableHeadersVisualStyles = false;
            this.gvItem.GridColor = System.Drawing.Color.White;
            this.gvItem.Location = new System.Drawing.Point(0, 15);
            this.gvItem.MultiSelect = false;
            this.gvItem.Name = "gvItem";
            this.gvItem.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvItem.RowHeadersVisible = false;
            this.gvItem.RowTemplate.Height = 20;
            this.gvItem.RowTemplate.ReadOnly = true;
            this.gvItem.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvItem.Size = new System.Drawing.Size(993, 192);
            this.gvItem.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblTotCharge);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(722, 189);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 18);
            this.panel2.TabIndex = 6;
            // 
            // InvItm_No
            // 
            this.InvItm_No.DataPropertyName = "sad_itm_line";
            this.InvItm_No.HeaderText = "No";
            this.InvItm_No.Name = "InvItm_No";
            this.InvItm_No.Width = 25;
            // 
            // InvItm_Item
            // 
            this.InvItm_Item.DataPropertyName = "sad_itm_cd";
            this.InvItm_Item.HeaderText = "Item";
            this.InvItm_Item.Name = "InvItm_Item";
            this.InvItm_Item.Width = 90;
            // 
            // InvItm_Description
            // 
            this.InvItm_Description.DataPropertyName = "mi_longdesc";
            this.InvItm_Description.HeaderText = "Description";
            this.InvItm_Description.Name = "InvItm_Description";
            this.InvItm_Description.Width = 150;
            // 
            // InvItm_Status
            // 
            this.InvItm_Status.DataPropertyName = "sad_itm_stus";
            this.InvItm_Status.HeaderText = "Status";
            this.InvItm_Status.Name = "InvItm_Status";
            this.InvItm_Status.Width = 105;
            // 
            // InvItm_Qty
            // 
            this.InvItm_Qty.DataPropertyName = "sad_qty";
            this.InvItm_Qty.HeaderText = "Qty";
            this.InvItm_Qty.Name = "InvItm_Qty";
            this.InvItm_Qty.Width = 60;
            // 
            // InvItm_Charge
            // 
            this.InvItm_Charge.HeaderText = "Charge";
            this.InvItm_Charge.Name = "InvItm_Charge";
            this.InvItm_Charge.Width = 70;
            // 
            // InvItm_Serial
            // 
            this.InvItm_Serial.HeaderText = "Serial 1";
            this.InvItm_Serial.Name = "InvItm_Serial";
            this.InvItm_Serial.Width = 150;
            // 
            // InvItm_Search
            // 
            this.InvItm_Search.HeaderText = "";
            this.InvItm_Search.Image = global::FF.WindowsERPClient.Properties.Resources.addicon;
            this.InvItm_Search.Name = "InvItm_Search";
            this.InvItm_Search.Width = 25;
            // 
            // InvItm_Serial2
            // 
            this.InvItm_Serial2.HeaderText = "Serial 2";
            this.InvItm_Serial2.Name = "InvItm_Serial2";
            this.InvItm_Serial2.Width = 150;
            // 
            // InvItm_Warranty
            // 
            this.InvItm_Warranty.HeaderText = "Warranty";
            this.InvItm_Warranty.Name = "InvItm_Warranty";
            this.InvItm_Warranty.Width = 150;
            // 
            // RegistrationCollection
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(997, 442);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistrationCollection";
            this.Text = "Registration Collection";
            this.pnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gvItem;
        private UserControls.ucPayModes ucPayModes1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotCharge;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_Charge;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_Serial;
        private System.Windows.Forms.DataGridViewImageColumn InvItm_Search;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_Serial2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvItm_Warranty;
    }
}
