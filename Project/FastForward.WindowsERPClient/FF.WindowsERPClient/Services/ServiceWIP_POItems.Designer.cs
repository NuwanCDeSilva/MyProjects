namespace FF.WindowsERPClient.Services
{
    partial class ServiceWIP_POItems
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnRetun = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.POSA_JOB_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POSA_JOB_ITM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POSA_PO_ITM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POSA_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POSA_UNIT_COST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlMain.Controls.Add(this.dgvItems);
            this.pnlMain.Controls.Add(this.toolStrip1);
            this.pnlMain.Controls.Add(this.btnCloseFrom);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(769, 469);
            this.pnlMain.TabIndex = 0;
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
            this.POSA_JOB_NO,
            this.POSA_JOB_ITM,
            this.POSA_PO_ITM,
            this.POSA_QTY,
            this.POSA_UNIT_COST});
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
            this.toolStrip1.Visible = false;
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
            // POSA_JOB_NO
            // 
            this.POSA_JOB_NO.DataPropertyName = "POSA_JOB_NO";
            this.POSA_JOB_NO.HeaderText = "Job No";
            this.POSA_JOB_NO.Name = "POSA_JOB_NO";
            this.POSA_JOB_NO.ReadOnly = true;
            this.POSA_JOB_NO.Width = 180;
            // 
            // POSA_JOB_ITM
            // 
            this.POSA_JOB_ITM.DataPropertyName = "POSA_JOB_ITM";
            this.POSA_JOB_ITM.HeaderText = "Job Item";
            this.POSA_JOB_ITM.Name = "POSA_JOB_ITM";
            this.POSA_JOB_ITM.ReadOnly = true;
            this.POSA_JOB_ITM.Width = 200;
            // 
            // POSA_PO_ITM
            // 
            this.POSA_PO_ITM.DataPropertyName = "POSA_PO_ITM";
            this.POSA_PO_ITM.HeaderText = "PO Item Code";
            this.POSA_PO_ITM.Name = "POSA_PO_ITM";
            this.POSA_PO_ITM.ReadOnly = true;
            this.POSA_PO_ITM.Width = 150;
            // 
            // POSA_QTY
            // 
            this.POSA_QTY.DataPropertyName = "POSA_QTY";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.POSA_QTY.DefaultCellStyle = dataGridViewCellStyle2;
            this.POSA_QTY.HeaderText = "Quantity";
            this.POSA_QTY.Name = "POSA_QTY";
            this.POSA_QTY.ReadOnly = true;
            this.POSA_QTY.Width = 80;
            // 
            // POSA_UNIT_COST
            // 
            this.POSA_UNIT_COST.DataPropertyName = "POSA_UNIT_COST";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.POSA_UNIT_COST.DefaultCellStyle = dataGridViewCellStyle3;
            this.POSA_UNIT_COST.HeaderText = "Unit Cost";
            this.POSA_UNIT_COST.Name = "POSA_UNIT_COST";
            this.POSA_UNIT_COST.ReadOnly = true;
            this.POSA_UNIT_COST.Width = 150;
            // 
            // ServiceWIP_POItems
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
            this.Name = "ServiceWIP_POItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Service Approved Purchase Order Items";
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
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.ToolStripButton btnRetun;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSA_JOB_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSA_JOB_ITM;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSA_PO_ITM;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSA_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSA_UNIT_COST;
    }
}