namespace FF.WindowsERPClient.Services
{
    partial class ServiceJobAccept
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceJobAccept));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlActualDefects = new System.Windows.Forms.Panel();
            this.btnAccept = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkAllJob = new System.Windows.Forms.CheckBox();
            this.chkAllSer = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btn_srch_ser = new System.Windows.Forms.Button();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_srch_job = new System.Windows.Forms.Button();
            this.txtJobNo = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.grvPending = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.JBD_JOBNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_JOBLINE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_ITM_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_ITM_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_SER1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JBD_WARR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.pnlActualDefects.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPending)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlActualDefects
            // 
            this.pnlActualDefects.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlActualDefects.Controls.Add(this.btnAccept);
            this.pnlActualDefects.Controls.Add(this.label15);
            this.pnlActualDefects.Controls.Add(this.panel2);
            this.pnlActualDefects.Controls.Add(this.grvPending);
            this.pnlActualDefects.Location = new System.Drawing.Point(1, 2);
            this.pnlActualDefects.Name = "pnlActualDefects";
            this.pnlActualDefects.Size = new System.Drawing.Size(819, 492);
            this.pnlActualDefects.TabIndex = 0;
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.NavajoWhite;
            this.btnAccept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnAccept.Location = new System.Drawing.Point(703, 465);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(106, 23);
            this.btnAccept.TabIndex = 315;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.DimGray;
            this.label15.ForeColor = System.Drawing.Color.Azure;
            this.label15.Location = new System.Drawing.Point(3, 56);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(813, 14);
            this.label15.TabIndex = 314;
            this.label15.Text = "Pending Jobs";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(201)))), ((int)(((byte)(167)))));
            this.panel2.Controls.Add(this.chkAllJob);
            this.panel2.Controls.Add(this.chkAllSer);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.btn_srch_ser);
            this.panel2.Controls.Add(this.txtSerial);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_srch_job);
            this.panel2.Controls.Add(this.txtJobNo);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 49);
            this.panel2.TabIndex = 312;
            // 
            // chkAllJob
            // 
            this.chkAllJob.AutoSize = true;
            this.chkAllJob.Checked = true;
            this.chkAllJob.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllJob.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkAllJob.Location = new System.Drawing.Point(673, 16);
            this.chkAllJob.Name = "chkAllJob";
            this.chkAllJob.Size = new System.Drawing.Size(43, 18);
            this.chkAllJob.TabIndex = 320;
            this.chkAllJob.Text = "All";
            this.chkAllJob.UseVisualStyleBackColor = true;
            this.chkAllJob.CheckedChanged += new System.EventHandler(this.chkAllJob_CheckedChanged);
            // 
            // chkAllSer
            // 
            this.chkAllSer.AutoSize = true;
            this.chkAllSer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkAllSer.Location = new System.Drawing.Point(298, 16);
            this.chkAllSer.Name = "chkAllSer";
            this.chkAllSer.Size = new System.Drawing.Size(43, 18);
            this.chkAllSer.TabIndex = 319;
            this.chkAllSer.Text = "All";
            this.chkAllSer.UseVisualStyleBackColor = true;
            this.chkAllSer.CheckedChanged += new System.EventHandler(this.chkAllSer_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.specialsearch2icon;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.Location = new System.Drawing.Point(763, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(43, 44);
            this.btnSearch.TabIndex = 318;
            this.btnSearch.UseCompatibleTextRendering = true;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btn_srch_ser
            // 
            this.btn_srch_ser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_srch_ser.BackgroundImage")));
            this.btn_srch_ser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_srch_ser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_srch_ser.ForeColor = System.Drawing.Color.Linen;
            this.btn_srch_ser.Location = new System.Drawing.Point(271, 14);
            this.btn_srch_ser.Name = "btn_srch_ser";
            this.btn_srch_ser.Size = new System.Drawing.Size(21, 20);
            this.btn_srch_ser.TabIndex = 317;
            this.btn_srch_ser.UseVisualStyleBackColor = true;
            this.btn_srch_ser.Click += new System.EventHandler(this.btn_srch_ser_Click);
            // 
            // txtSerial
            // 
            this.txtSerial.BackColor = System.Drawing.Color.White;
            this.txtSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSerial.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerial.Location = new System.Drawing.Point(83, 15);
            this.txtSerial.MaxLength = 100;
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(182, 20);
            this.txtSerial.TabIndex = 0;
            this.txtSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerial_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 315;
            this.label1.Text = "Serial #";
            // 
            // btn_srch_job
            // 
            this.btn_srch_job.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_srch_job.BackgroundImage")));
            this.btn_srch_job.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_srch_job.Enabled = false;
            this.btn_srch_job.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_srch_job.ForeColor = System.Drawing.Color.Linen;
            this.btn_srch_job.Location = new System.Drawing.Point(645, 14);
            this.btn_srch_job.Name = "btn_srch_job";
            this.btn_srch_job.Size = new System.Drawing.Size(21, 20);
            this.btn_srch_job.TabIndex = 314;
            this.btn_srch_job.UseVisualStyleBackColor = true;
            this.btn_srch_job.Click += new System.EventHandler(this.btn_srch_job_Click);
            // 
            // txtJobNo
            // 
            this.txtJobNo.BackColor = System.Drawing.Color.White;
            this.txtJobNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJobNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJobNo.Enabled = false;
            this.txtJobNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobNo.Location = new System.Drawing.Point(457, 15);
            this.txtJobNo.MaxLength = 100;
            this.txtJobNo.Name = "txtJobNo";
            this.txtJobNo.Size = new System.Drawing.Size(182, 20);
            this.txtJobNo.TabIndex = 1;
            this.txtJobNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJobNo_KeyDown);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(387, 18);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(64, 13);
            this.label42.TabIndex = 312;
            this.label42.Text = "Job Number";
            // 
            // grvPending
            // 
            this.grvPending.AllowUserToAddRows = false;
            this.grvPending.AllowUserToDeleteRows = false;
            this.grvPending.AllowUserToOrderColumns = true;
            this.grvPending.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvPending.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grvPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvPending.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.JBD_JOBNO,
            this.JBD_JOBLINE,
            this.JBD_ITM_CD,
            this.JBD_ITM_DESC,
            this.JBD_SER1,
            this.JBD_WARR});
            this.grvPending.EnableHeadersVisualStyles = false;
            this.grvPending.Location = new System.Drawing.Point(3, 72);
            this.grvPending.MultiSelect = false;
            this.grvPending.Name = "grvPending";
            this.grvPending.RowHeadersVisible = false;
            this.grvPending.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvPending.Size = new System.Drawing.Size(813, 390);
            this.grvPending.TabIndex = 5;
            this.grvPending.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvPending_CellContentDoubleClick);
            this.grvPending.CurrentCellDirtyStateChanged += new System.EventHandler(this.grvPending_CurrentCellDirtyStateChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 30;
            // 
            // JBD_JOBNO
            // 
            this.JBD_JOBNO.DataPropertyName = "JBD_JOBNO";
            this.JBD_JOBNO.HeaderText = "Job #";
            this.JBD_JOBNO.Name = "JBD_JOBNO";
            this.JBD_JOBNO.ReadOnly = true;
            this.JBD_JOBNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.JBD_JOBNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.JBD_JOBNO.Width = 130;
            // 
            // JBD_JOBLINE
            // 
            this.JBD_JOBLINE.DataPropertyName = "JBD_JOBLINE";
            this.JBD_JOBLINE.HeaderText = "Job Line";
            this.JBD_JOBLINE.Name = "JBD_JOBLINE";
            this.JBD_JOBLINE.ReadOnly = true;
            this.JBD_JOBLINE.Width = 50;
            // 
            // JBD_ITM_CD
            // 
            this.JBD_ITM_CD.DataPropertyName = "JBD_ITM_CD";
            this.JBD_ITM_CD.HeaderText = "Item";
            this.JBD_ITM_CD.Name = "JBD_ITM_CD";
            this.JBD_ITM_CD.ReadOnly = true;
            this.JBD_ITM_CD.Width = 130;
            // 
            // JBD_ITM_DESC
            // 
            this.JBD_ITM_DESC.DataPropertyName = "JBD_ITM_DESC";
            this.JBD_ITM_DESC.HeaderText = "Description";
            this.JBD_ITM_DESC.Name = "JBD_ITM_DESC";
            this.JBD_ITM_DESC.ReadOnly = true;
            this.JBD_ITM_DESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.JBD_ITM_DESC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.JBD_ITM_DESC.Width = 150;
            // 
            // JBD_SER1
            // 
            this.JBD_SER1.DataPropertyName = "JBD_SER1";
            this.JBD_SER1.HeaderText = "Serial #";
            this.JBD_SER1.Name = "JBD_SER1";
            this.JBD_SER1.ReadOnly = true;
            this.JBD_SER1.Width = 150;
            // 
            // JBD_WARR
            // 
            this.JBD_WARR.DataPropertyName = "JBD_WARR";
            this.JBD_WARR.HeaderText = "Warranty #";
            this.JBD_WARR.Name = "JBD_WARR";
            this.JBD_WARR.ReadOnly = true;
            this.JBD_WARR.Width = 170;
            // 
            // btnCloseFrom
            // 
            this.btnCloseFrom.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseFrom.Location = new System.Drawing.Point(374, 224);
            this.btnCloseFrom.Name = "btnCloseFrom";
            this.btnCloseFrom.Size = new System.Drawing.Size(10, 10);
            this.btnCloseFrom.TabIndex = 275;
            this.btnCloseFrom.Text = "  ";
            this.btnCloseFrom.UseVisualStyleBackColor = true;
            this.btnCloseFrom.Click += new System.EventHandler(this.btnCloseFrom_Click);
            // 
            // ServiceJobAccept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(822, 498);
            this.Controls.Add(this.pnlActualDefects);
            this.Controls.Add(this.btnCloseFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceJobAccept";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Pending Jobs Acceptance";
            this.Load += new System.EventHandler(this.ServicePayments_Load);
            this.pnlActualDefects.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPending)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlActualDefects;
        private System.Windows.Forms.DataGridView grvPending;
        private System.Windows.Forms.Button btnCloseFrom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtJobNo;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btn_srch_ser;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_srch_job;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkAllJob;
        private System.Windows.Forms.CheckBox chkAllSer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_JOBNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_JOBLINE;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_ITM_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_ITM_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_SER1;
        private System.Windows.Forms.DataGridViewTextBoxColumn JBD_WARR;
    }
}