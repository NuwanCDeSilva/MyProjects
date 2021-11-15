namespace FF.WindowsERPClient.UserControls
{
    partial class ucServicePriority
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTaskLevels = new System.Windows.Forms.DataGridView();
            this.lblPriorityLevelText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.jbs_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spit_expt_dur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTaskLevels
            // 
            this.dgvTaskLevels.AllowUserToAddRows = false;
            this.dgvTaskLevels.AllowUserToDeleteRows = false;
            this.dgvTaskLevels.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvTaskLevels.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTaskLevels.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvTaskLevels.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaskLevels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTaskLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskLevels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.jbs_desc,
            this.Spit_expt_dur});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTaskLevels.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTaskLevels.EnableHeadersVisualStyles = false;
            this.dgvTaskLevels.Location = new System.Drawing.Point(-1, 54);
            this.dgvTaskLevels.MultiSelect = false;
            this.dgvTaskLevels.Name = "dgvTaskLevels";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaskLevels.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTaskLevels.RowHeadersVisible = false;
            this.dgvTaskLevels.RowHeadersWidth = 20;
            this.dgvTaskLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaskLevels.Size = new System.Drawing.Size(157, 86);
            this.dgvTaskLevels.TabIndex = 127;
            // 
            // lblPriorityLevelText
            // 
            this.lblPriorityLevelText.BackColor = System.Drawing.Color.Coral;
            this.lblPriorityLevelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriorityLevelText.Location = new System.Drawing.Point(0, 0);
            this.lblPriorityLevelText.Name = "lblPriorityLevelText";
            this.lblPriorityLevelText.Size = new System.Drawing.Size(156, 55);
            this.lblPriorityLevelText.TabIndex = 126;
            this.lblPriorityLevelText.Text = "Priority Level text";
            this.lblPriorityLevelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Coral;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 128;
            this.label1.Text = "Service Level";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // jbs_desc
            // 
            this.jbs_desc.DataPropertyName = "jbs_desc";
            this.jbs_desc.HeaderText = "Stage";
            this.jbs_desc.Name = "jbs_desc";
            this.jbs_desc.ReadOnly = true;
            // 
            // Spit_expt_dur
            // 
            this.Spit_expt_dur.DataPropertyName = "Spit_expt_durTxt";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Spit_expt_dur.DefaultCellStyle = dataGridViewCellStyle3;
            this.Spit_expt_dur.HeaderText = "Duration";
            this.Spit_expt_dur.Name = "Spit_expt_dur";
            this.Spit_expt_dur.ReadOnly = true;
            this.Spit_expt_dur.Width = 50;
            // 
            // ucServicePriority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTaskLevels);
            this.Controls.Add(this.lblPriorityLevelText);
            this.Name = "ucServicePriority";
            this.Size = new System.Drawing.Size(159, 143);
            this.Load += new System.EventHandler(this.ucServicePriority_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskLevels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTaskLevels;
        private System.Windows.Forms.Label lblPriorityLevelText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn jbs_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spit_expt_dur;
    }
}
