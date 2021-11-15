namespace FF.WindowsERPClient.Services
{
    partial class ServiceTasks
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
            this.pnlActualDefects = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkCollapse = new System.Windows.Forms.CheckBox();
            this.chkExpandAll = new System.Windows.Forms.CheckBox();
            this.txtJobNo = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.grvPending = new System.Windows.Forms.DataGridView();
            this.Task = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Task_Ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Task_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Task_UpdBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Task_UpdTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCloseFrom = new System.Windows.Forms.Button();
            this.pnlActualDefects.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvPending)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlActualDefects
            // 
            this.pnlActualDefects.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlActualDefects.Controls.Add(this.label15);
            this.pnlActualDefects.Controls.Add(this.panel2);
            this.pnlActualDefects.Controls.Add(this.grvPending);
            this.pnlActualDefects.Location = new System.Drawing.Point(1, 2);
            this.pnlActualDefects.Name = "pnlActualDefects";
            this.pnlActualDefects.Size = new System.Drawing.Size(916, 473);
            this.pnlActualDefects.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.MidnightBlue;
            this.label15.ForeColor = System.Drawing.Color.Azure;
            this.label15.Location = new System.Drawing.Point(3, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(890, 14);
            this.label15.TabIndex = 314;
            this.label15.Text = "Task Summary";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(201)))), ((int)(((byte)(167)))));
            this.panel2.Controls.Add(this.chkCollapse);
            this.panel2.Controls.Add(this.chkExpandAll);
            this.panel2.Controls.Add(this.txtJobNo);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(890, 31);
            this.panel2.TabIndex = 312;
            // 
            // chkCollapse
            // 
            this.chkCollapse.AutoSize = true;
            this.chkCollapse.Location = new System.Drawing.Point(401, 8);
            this.chkCollapse.Name = "chkCollapse";
            this.chkCollapse.Size = new System.Drawing.Size(80, 17);
            this.chkCollapse.TabIndex = 315;
            this.chkCollapse.Text = "Collapse All";
            this.chkCollapse.UseVisualStyleBackColor = true;
            this.chkCollapse.CheckedChanged += new System.EventHandler(this.chkCollapse_CheckedChanged);
            // 
            // chkExpandAll
            // 
            this.chkExpandAll.AutoSize = true;
            this.chkExpandAll.Checked = true;
            this.chkExpandAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExpandAll.Location = new System.Drawing.Point(314, 9);
            this.chkExpandAll.Name = "chkExpandAll";
            this.chkExpandAll.Size = new System.Drawing.Size(76, 17);
            this.chkExpandAll.TabIndex = 314;
            this.chkExpandAll.Text = "Expand All";
            this.chkExpandAll.UseVisualStyleBackColor = true;
            this.chkExpandAll.CheckedChanged += new System.EventHandler(this.chkExpandAll_CheckedChanged);
            // 
            // txtJobNo
            // 
            this.txtJobNo.BackColor = System.Drawing.Color.White;
            this.txtJobNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJobNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJobNo.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobNo.Location = new System.Drawing.Point(77, 6);
            this.txtJobNo.MaxLength = 5;
            this.txtJobNo.Name = "txtJobNo";
            this.txtJobNo.ReadOnly = true;
            this.txtJobNo.Size = new System.Drawing.Size(182, 19);
            this.txtJobNo.TabIndex = 313;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(7, 9);
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
            this.Task,
            this.Task_Ref,
            this.Task_Date,
            this.Task_UpdBy,
            this.Task_UpdTime,
            this.User_Name,
            this.STUS});
            this.grvPending.EnableHeadersVisualStyles = false;
            this.grvPending.Location = new System.Drawing.Point(3, 50);
            this.grvPending.MultiSelect = false;
            this.grvPending.Name = "grvPending";
            this.grvPending.RowHeadersVisible = false;
            this.grvPending.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvPending.Size = new System.Drawing.Size(890, 420);
            this.grvPending.TabIndex = 5;
            this.grvPending.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvPending_CellContentDoubleClick);
            // 
            // Task
            // 
            this.Task.DataPropertyName = "Task_Desc";
            this.Task.HeaderText = "Task";
            this.Task.Name = "Task";
            this.Task.ReadOnly = true;
            this.Task.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Task.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Task.Width = 200;
            // 
            // Task_Ref
            // 
            this.Task_Ref.DataPropertyName = "Task_Ref";
            this.Task_Ref.HeaderText = "Task Ref";
            this.Task_Ref.Name = "Task_Ref";
            this.Task_Ref.ReadOnly = true;
            this.Task_Ref.Width = 130;
            // 
            // Task_Date
            // 
            this.Task_Date.DataPropertyName = "Task_Date";
            this.Task_Date.HeaderText = "Task Date";
            this.Task_Date.Name = "Task_Date";
            this.Task_Date.ReadOnly = true;
            this.Task_Date.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Task_Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Task_Date.Width = 130;
            // 
            // Task_UpdBy
            // 
            this.Task_UpdBy.DataPropertyName = "Task_UpdBy";
            this.Task_UpdBy.HeaderText = "Task Updated By";
            this.Task_UpdBy.Name = "Task_UpdBy";
            // 
            // Task_UpdTime
            // 
            this.Task_UpdTime.DataPropertyName = "Task_UpdTime";
            this.Task_UpdTime.HeaderText = "Task Updated Time";
            this.Task_UpdTime.Name = "Task_UpdTime";
            this.Task_UpdTime.Width = 130;
            // 
            // User_Name
            // 
            this.User_Name.DataPropertyName = "Task_userName";
            this.User_Name.HeaderText = "Done by";
            this.User_Name.Name = "User_Name";
            // 
            // STUS
            // 
            this.STUS.DataPropertyName = "Taskstutes";
            this.STUS.HeaderText = "status";
            this.STUS.Name = "STUS";
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
            // ServiceTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseFrom;
            this.ClientSize = new System.Drawing.Size(895, 478);
            this.Controls.Add(this.pnlActualDefects);
            this.Controls.Add(this.btnCloseFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceTasks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Job base Task Summary";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Task;
        private System.Windows.Forms.DataGridViewTextBoxColumn Task_Ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn Task_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Task_UpdBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Task_UpdTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn STUS;
        private System.Windows.Forms.CheckBox chkCollapse;
        private System.Windows.Forms.CheckBox chkExpandAll;
    }
}