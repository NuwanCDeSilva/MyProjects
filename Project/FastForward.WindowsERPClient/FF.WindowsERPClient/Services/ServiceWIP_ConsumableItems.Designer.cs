namespace FF.WindowsERPClient.Services
{
    partial class ServiceWIP_ConsumableItems
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceWIP_ConsumableItems));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.chkReturn = new System.Windows.Forms.CheckBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnVouNo = new System.Windows.Forms.Button();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnRetun = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sti_seqno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inward_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_staus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serial_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERIAL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.job_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.job_line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyIssued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequiredQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlMain.Controls.Add(this.chkReturn);
            this.pnlMain.Controls.Add(this.dtpDate);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.btnVouNo);
            this.pnlMain.Controls.Add(this.txtItemCode);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.dgvItems);
            this.pnlMain.Controls.Add(this.toolStrip1);
            this.pnlMain.Controls.Add(this.btnCloseFrom);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(769, 469);
            this.pnlMain.TabIndex = 0;
            // 
            // chkReturn
            // 
            this.chkReturn.AutoSize = true;
            this.chkReturn.BackColor = System.Drawing.SystemColors.Control;
            this.chkReturn.Location = new System.Drawing.Point(331, 5);
            this.chkReturn.Name = "chkReturn";
            this.chkReturn.Size = new System.Drawing.Size(79, 17);
            this.chkReturn.TabIndex = 289;
            this.chkReturn.Text = "Used Items";
            this.chkReturn.UseVisualStyleBackColor = false;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(217, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(106, 20);
            this.dtpDate.TabIndex = 288;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(179, 6);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 287;
            this.label5.Text = "Date";
            // 
            // btnVouNo
            // 
            this.btnVouNo.BackColor = System.Drawing.SystemColors.Control;
            this.btnVouNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVouNo.BackgroundImage")));
            this.btnVouNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVouNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVouNo.ForeColor = System.Drawing.Color.White;
            this.btnVouNo.Location = new System.Drawing.Point(154, 2);
            this.btnVouNo.Name = "btnVouNo";
            this.btnVouNo.Size = new System.Drawing.Size(22, 20);
            this.btnVouNo.TabIndex = 286;
            this.btnVouNo.UseVisualStyleBackColor = false;
            this.btnVouNo.Click += new System.EventHandler(this.btnVouNo_Click);
            // 
            // txtItemCode
            // 
            this.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemCode.Location = new System.Drawing.Point(60, 2);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(93, 20);
            this.txtItemCode.TabIndex = 284;
            this.txtItemCode.DoubleClick += new System.EventHandler(this.txtItemCode_DoubleClick);
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Leave += new System.EventHandler(this.txtItemCode_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 285;
            this.label1.Text = "Item Code";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.sti_seqno,
            this.inward_doc,
            this.item_code,
            this.DESC,
            this.item_staus,
            this.STATUS_CODE,
            this.serial_no,
            this.SERIAL_ID,
            this.job_no,
            this.job_line,
            this.Qty,
            this.QtyIssued,
            this.QTYBalance,
            this.RequiredQty,
            this.Remark});
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.Location = new System.Drawing.Point(2, 27);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(764, 439);
            this.dgvItems.TabIndex = 283;
            this.dgvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellEndEdit);
            this.dgvItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvItems_CellFormatting);
            this.dgvItems.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellMouseEnter);
            this.dgvItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvItems_CurrentCellDirtyStateChanged);
            this.dgvItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItems_EditingControlShowing);
            this.dgvItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvItems_MouseMove);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnRetun,
            this.btnSave,
            this.btnView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(769, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
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
            this.btnClear.Size = new System.Drawing.Size(80, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRetun
            // 
            this.btnRetun.AutoSize = false;
            this.btnRetun.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRetun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRetun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRetun.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnRetun.Name = "btnRetun";
            this.btnRetun.Padding = new System.Windows.Forms.Padding(2);
            this.btnRetun.Size = new System.Drawing.Size(80, 22);
            this.btnRetun.Text = "Return";
            this.btnRetun.Click += new System.EventHandler(this.btnRetun_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(2);
            this.btnSave.Size = new System.Drawing.Size(80, 22);
            this.btnSave.Text = "Issue";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnView
            // 
            this.btnView.AutoSize = false;
            this.btnView.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnView.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnView.Name = "btnView";
            this.btnView.Padding = new System.Windows.Forms.Padding(2);
            this.btnView.Size = new System.Drawing.Size(80, 22);
            this.btnView.Text = "View";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnCloseFrom
            // 
            this.btnCloseFrom.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseFrom.Location = new System.Drawing.Point(379, 229);
            this.btnCloseFrom.Name = "btnCloseFrom";
            this.btnCloseFrom.Size = new System.Drawing.Size(10, 10);
            this.btnCloseFrom.TabIndex = 275;
            this.btnCloseFrom.Text = "  ";
            this.btnCloseFrom.UseVisualStyleBackColor = true;
            this.btnCloseFrom.Click += new System.EventHandler(this.btnCloseFrom_Click);
            // 
            // select
            // 
            this.select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select.Frozen = true;
            this.select.HeaderText = "  ";
            this.select.Name = "select";
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.select.Width = 30;
            // 
            // sti_seqno
            // 
            this.sti_seqno.DataPropertyName = "sti_seqno";
            this.sti_seqno.HeaderText = "Seq";
            this.sti_seqno.Name = "sti_seqno";
            this.sti_seqno.ReadOnly = true;
            this.sti_seqno.Visible = false;
            // 
            // inward_doc
            // 
            this.inward_doc.DataPropertyName = "inward_doc";
            this.inward_doc.HeaderText = "Inward Doc";
            this.inward_doc.Name = "inward_doc";
            this.inward_doc.ReadOnly = true;
            this.inward_doc.Visible = false;
            // 
            // item_code
            // 
            this.item_code.DataPropertyName = "item_code";
            this.item_code.HeaderText = "Item Code";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            // 
            // DESC
            // 
            this.DESC.DataPropertyName = "DESC";
            this.DESC.HeaderText = "Item Description";
            this.DESC.Name = "DESC";
            this.DESC.ReadOnly = true;
            this.DESC.Width = 120;
            // 
            // item_staus
            // 
            this.item_staus.DataPropertyName = "item_staus";
            this.item_staus.HeaderText = "Item Status";
            this.item_staus.Name = "item_staus";
            this.item_staus.ReadOnly = true;
            this.item_staus.Width = 80;
            // 
            // STATUS_CODE
            // 
            this.STATUS_CODE.DataPropertyName = "STATUS_CODE";
            this.STATUS_CODE.HeaderText = "STATUS_CODE";
            this.STATUS_CODE.Name = "STATUS_CODE";
            this.STATUS_CODE.ReadOnly = true;
            this.STATUS_CODE.Visible = false;
            // 
            // serial_no
            // 
            this.serial_no.DataPropertyName = "serial_no";
            this.serial_no.HeaderText = "Serial No";
            this.serial_no.Name = "serial_no";
            this.serial_no.ReadOnly = true;
            this.serial_no.Visible = false;
            // 
            // SERIAL_ID
            // 
            this.SERIAL_ID.DataPropertyName = "SERIAL_ID";
            this.SERIAL_ID.HeaderText = "SERIAL_ID";
            this.SERIAL_ID.Name = "SERIAL_ID";
            this.SERIAL_ID.ReadOnly = true;
            this.SERIAL_ID.Visible = false;
            // 
            // job_no
            // 
            this.job_no.DataPropertyName = "job_no";
            this.job_no.HeaderText = "Job No";
            this.job_no.Name = "job_no";
            this.job_no.ReadOnly = true;
            this.job_no.Visible = false;
            // 
            // job_line
            // 
            this.job_line.DataPropertyName = "job_line";
            this.job_line.HeaderText = "Job Line No";
            this.job_line.Name = "job_line";
            this.job_line.ReadOnly = true;
            this.job_line.Visible = false;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.Qty.DefaultCellStyle = dataGridViewCellStyle2;
            this.Qty.HeaderText = "Stock In Hand";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 90;
            // 
            // QtyIssued
            // 
            this.QtyIssued.DataPropertyName = "QtyIssued";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.QtyIssued.DefaultCellStyle = dataGridViewCellStyle3;
            this.QtyIssued.HeaderText = "Used Qty";
            this.QtyIssued.Name = "QtyIssued";
            this.QtyIssued.ReadOnly = true;
            // 
            // QTYBalance
            // 
            this.QTYBalance.DataPropertyName = "QTYBalance";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.QTYBalance.DefaultCellStyle = dataGridViewCellStyle4;
            this.QTYBalance.HeaderText = "Balance Qty";
            this.QTYBalance.Name = "QTYBalance";
            this.QTYBalance.ReadOnly = true;
            // 
            // RequiredQty
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0";
            this.RequiredQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.RequiredQty.HeaderText = "Required Qty";
            this.RequiredQty.Name = "RequiredQty";
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "Remark";
            this.Remark.Name = "Remark";
            this.Remark.Width = 180;
            // 
            // ServiceWIP_ConsumableItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(769, 469);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceWIP_ConsumableItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Service WIP Consumable Items";
            this.Load += new System.EventHandler(this.ServiceWIP_ConsumableItems_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnView;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVouNo;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkReturn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.ToolStripButton btnRetun;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn sti_seqno;
        private System.Windows.Forms.DataGridViewTextBoxColumn inward_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_staus;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn serial_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIAL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn job_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn job_line;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyIssued;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequiredQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
    }
}