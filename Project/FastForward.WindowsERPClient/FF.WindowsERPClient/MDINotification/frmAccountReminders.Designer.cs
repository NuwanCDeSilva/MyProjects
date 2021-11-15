namespace FF.WindowsERPClient.MDINotification
{
    partial class frmAccountReminders
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccountReminders));
            this.gvView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hra_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlReminder = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRMClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSeqNum = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnInactive = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.txtReminder = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvView)).BeginInit();
            this.pnlReminder.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvView
            // 
            this.gvView.AllowUserToAddRows = false;
            this.gvView.AllowUserToDeleteRows = false;
            this.gvView.AllowUserToResizeColumns = false;
            this.gvView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.gvView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvView.ColumnHeadersHeight = 20;
            this.gvView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.Column1,
            this.hra_seq});
            this.gvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvView.Location = new System.Drawing.Point(0, 0);
            this.gvView.MultiSelect = false;
            this.gvView.Name = "gvView";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvView.RowHeadersVisible = false;
            this.gvView.RowTemplate.Height = 25;
            this.gvView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvView.Size = new System.Drawing.Size(476, 467);
            this.gvView.TabIndex = 139;
            this.gvView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvView_CellDoubleClick);
            this.gvView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvView_KeyDown);
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "HRA_REF";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn10.HeaderText = "Acc No";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 80;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "HRA_DT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn11.HeaderText = "Remind Date";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 80;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "HRA_RMD";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.NullValue = null;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn12.HeaderText = "Reminder";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 260;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "HRA_STUS";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column1.HeaderText = "Status";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // hra_seq
            // 
            this.hra_seq.DataPropertyName = "hra_seq";
            this.hra_seq.HeaderText = "hra_seq";
            this.hra_seq.Name = "hra_seq";
            this.hra_seq.Visible = false;
            this.hra_seq.Width = 5;
            // 
            // pnlReminder
            // 
            this.pnlReminder.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlReminder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReminder.Controls.Add(this.panel1);
            this.pnlReminder.Controls.Add(this.label2);
            this.pnlReminder.Controls.Add(this.lblSeqNum);
            this.pnlReminder.Controls.Add(this.lblCount);
            this.pnlReminder.Controls.Add(this.label1);
            this.pnlReminder.Controls.Add(this.lblAccount);
            this.pnlReminder.Controls.Add(this.btnNext);
            this.pnlReminder.Controls.Add(this.btnInactive);
            this.pnlReminder.Controls.Add(this.btnPre);
            this.pnlReminder.Controls.Add(this.txtReminder);
            this.pnlReminder.Location = new System.Drawing.Point(25, 50);
            this.pnlReminder.Name = "pnlReminder";
            this.pnlReminder.Size = new System.Drawing.Size(434, 302);
            this.pnlReminder.TabIndex = 140;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.btnRMClose);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 17);
            this.panel1.TabIndex = 434;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // btnRMClose
            // 
            this.btnRMClose.BackColor = System.Drawing.Color.SlateGray;
            this.btnRMClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRMClose.BackgroundImage")));
            this.btnRMClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRMClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRMClose.ForeColor = System.Drawing.Color.SlateGray;
            this.btnRMClose.Location = new System.Drawing.Point(414, -1);
            this.btnRMClose.Name = "btnRMClose";
            this.btnRMClose.Size = new System.Drawing.Size(20, 19);
            this.btnRMClose.TabIndex = 431;
            this.btnRMClose.UseVisualStyleBackColor = false;
            this.btnRMClose.Click += new System.EventHandler(this.btnRMClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 433;
            this.label2.Text = "Seq Num :";
            // 
            // lblSeqNum
            // 
            this.lblSeqNum.AutoSize = true;
            this.lblSeqNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeqNum.Location = new System.Drawing.Point(76, 49);
            this.lblSeqNum.Name = "lblSeqNum";
            this.lblSeqNum.Size = new System.Drawing.Size(49, 13);
            this.lblSeqNum.TabIndex = 432;
            this.lblSeqNum.Text = "Seq Num";
            this.lblSeqNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(8, 65);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(46, 13);
            this.lblCount.TabIndex = 432;
            this.lblCount.Text = "lblCount";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 431;
            this.label1.Text = "Account :";
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(76, 33);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(56, 13);
            this.lblAccount.TabIndex = 431;
            this.lblAccount.Text = "lblAccount";
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(41, 271);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 24);
            this.btnNext.TabIndex = 429;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnInactive
            // 
            this.btnInactive.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnInactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInactive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInactive.Location = new System.Drawing.Point(358, 272);
            this.btnInactive.Name = "btnInactive";
            this.btnInactive.Size = new System.Drawing.Size(68, 24);
            this.btnInactive.TabIndex = 429;
            this.btnInactive.Text = "Accept";
            this.btnInactive.UseVisualStyleBackColor = false;
            this.btnInactive.Click += new System.EventHandler(this.btnInactive_Click);
            // 
            // btnPre
            // 
            this.btnPre.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPre.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPre.Location = new System.Drawing.Point(3, 271);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(32, 24);
            this.btnPre.TabIndex = 429;
            this.btnPre.Text = "<";
            this.btnPre.UseVisualStyleBackColor = false;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // txtReminder
            // 
            this.txtReminder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReminder.Location = new System.Drawing.Point(3, 81);
            this.txtReminder.Name = "txtReminder";
            this.txtReminder.ReadOnly = true;
            this.txtReminder.Size = new System.Drawing.Size(423, 185);
            this.txtReminder.TabIndex = 0;
            this.txtReminder.Text = "";
            this.txtReminder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReminder_KeyDown);
            // 
            // frmAccountReminders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 467);
            this.Controls.Add(this.pnlReminder);
            this.Controls.Add(this.gvView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAccountReminders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account Reminders";
            this.Load += new System.EventHandler(this.frmAccountReminders_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAccountReminders_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gvView)).EndInit();
            this.pnlReminder.ResumeLayout(false);
            this.pnlReminder.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvView;
        private System.Windows.Forms.Panel pnlReminder;
        private System.Windows.Forms.RichTextBox txtReminder;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnInactive;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label lblSeqNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn hra_seq;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRMClose;
    }
}