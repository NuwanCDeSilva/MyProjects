namespace FF.WindowsERPClient.MDINotification
{
    partial class frmNotification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotification));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new GradientPanel.GradientPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlCA = new System.Windows.Forms.Panel();
            this.lstControlAct = new System.Windows.Forms.ListView();
            this.discription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.val1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.val2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblPnlCAHeading = new System.Windows.Forms.Label();
            this.rectangleShape1 = new System.Windows.Forms.Panel();
            this.pnlBottom = new GradientPanel.GradientPanel();
            this.lblThoughtHeading = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.picLog = new System.Windows.Forms.PictureBox();
            this.pnlRM = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gvRec = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lstReminders = new System.Windows.Forms.ListView();
            this.rectangleShape2 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlCA.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLog)).BeginInit();
            this.pnlRM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRec)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTop.BackgroundImage")));
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Controls.Add(this.pictureBox1);
            this.pnlTop.Location = new System.Drawing.Point(5, 2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.PageEndColor = System.Drawing.Color.White;
            this.pnlTop.PageStartColor = System.Drawing.Color.White;
            this.pnlTop.Size = new System.Drawing.Size(189, 78);
            this.pnlTop.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(1, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 1);
            this.panel1.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(147, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // pnlCA
            // 
            this.pnlCA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlCA.Controls.Add(this.lstControlAct);
            this.pnlCA.Controls.Add(this.lblPnlCAHeading);
            this.pnlCA.Controls.Add(this.rectangleShape1);
            this.pnlCA.Location = new System.Drawing.Point(11, 54);
            this.pnlCA.Name = "pnlCA";
            this.pnlCA.Size = new System.Drawing.Size(589, 251);
            this.pnlCA.TabIndex = 20;
            // 
            // lstControlAct
            // 
            this.lstControlAct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstControlAct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lstControlAct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstControlAct.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.discription,
            this.val1,
            this.val2});
            this.lstControlAct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstControlAct.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstControlAct.Location = new System.Drawing.Point(7, 26);
            this.lstControlAct.Name = "lstControlAct";
            this.lstControlAct.Size = new System.Drawing.Size(579, 216);
            this.lstControlAct.TabIndex = 6;
            this.lstControlAct.UseCompatibleStateImageBehavior = false;
            this.lstControlAct.View = System.Windows.Forms.View.Details;
            // 
            // discription
            // 
            this.discription.Width = 120;
            // 
            // val1
            // 
            this.val1.Width = 180;
            // 
            // val2
            // 
            this.val2.Width = 200;
            // 
            // lblPnlCAHeading
            // 
            this.lblPnlCAHeading.AutoSize = true;
            this.lblPnlCAHeading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(25)))), ((int)(((byte)(116)))));
            this.lblPnlCAHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPnlCAHeading.ForeColor = System.Drawing.Color.White;
            this.lblPnlCAHeading.Location = new System.Drawing.Point(3, 1);
            this.lblPnlCAHeading.Name = "lblPnlCAHeading";
            this.lblPnlCAHeading.Size = new System.Drawing.Size(144, 16);
            this.lblPnlCAHeading.TabIndex = 3;
            this.lblPnlCAHeading.Text = "Your open statistics";
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(25)))), ((int)(((byte)(116)))));
            this.rectangleShape1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rectangleShape1.Location = new System.Drawing.Point(0, 0);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(589, 20);
            this.rectangleShape1.TabIndex = 7;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlBottom.BackgroundImage")));
            this.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlBottom.Controls.Add(this.lblThoughtHeading);
            this.pnlBottom.Controls.Add(this.monthCalendar1);
            this.pnlBottom.Controls.Add(this.richTextBox1);
            this.pnlBottom.Controls.Add(this.picLog);
            this.pnlBottom.Location = new System.Drawing.Point(7, 309);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.PageEndColor = System.Drawing.Color.White;
            this.pnlBottom.PageStartColor = System.Drawing.Color.White;
            this.pnlBottom.Size = new System.Drawing.Size(914, 208);
            this.pnlBottom.TabIndex = 21;
            // 
            // lblThoughtHeading
            // 
            this.lblThoughtHeading.AutoSize = true;
            this.lblThoughtHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblThoughtHeading.Font = new System.Drawing.Font("Californian FB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThoughtHeading.Location = new System.Drawing.Point(0, 0);
            this.lblThoughtHeading.Name = "lblThoughtHeading";
            this.lblThoughtHeading.Size = new System.Drawing.Size(301, 27);
            this.lblThoughtHeading.TabIndex = 23;
            this.lblThoughtHeading.Text = "Thought for the week ......... . . .";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.monthCalendar1.Location = new System.Drawing.Point(406, 27);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 13;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.richTextBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.richTextBox1.Location = new System.Drawing.Point(63, 32);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ShortcutsEnabled = false;
            this.richTextBox1.Size = new System.Drawing.Size(377, 157);
            this.richTextBox1.TabIndex = 24;
            this.richTextBox1.Text = "";
            this.richTextBox1.Click += new System.EventHandler(this.richTextBox1_Click);
            // 
            // picLog
            // 
            this.picLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picLog.BackColor = System.Drawing.Color.Maroon;
            this.picLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLog.Location = new System.Drawing.Point(681, 62);
            this.picLog.Name = "picLog";
            this.picLog.Size = new System.Drawing.Size(200, 120);
            this.picLog.TabIndex = 25;
            this.picLog.TabStop = false;
            // 
            // pnlRM
            // 
            this.pnlRM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlRM.Controls.Add(this.label2);
            this.pnlRM.Controls.Add(this.panel2);
            this.pnlRM.Controls.Add(this.gvRec);
            this.pnlRM.Controls.Add(this.label1);
            this.pnlRM.Controls.Add(this.lstReminders);
            this.pnlRM.Controls.Add(this.rectangleShape2);
            this.pnlRM.Location = new System.Drawing.Point(606, 54);
            this.pnlRM.Name = "pnlRM";
            this.pnlRM.Size = new System.Drawing.Size(589, 251);
            this.pnlRM.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(25)))), ((int)(((byte)(116)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Past Log on Information";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(25)))), ((int)(((byte)(116)))));
            this.panel2.Location = new System.Drawing.Point(0, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1205, 20);
            this.panel2.TabIndex = 105;
            // 
            // gvRec
            // 
            this.gvRec.AllowUserToAddRows = false;
            this.gvRec.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.gvRec.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvRec.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvRec.ColumnHeadersHeight = 20;
            this.gvRec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.Column1,
            this.Column2,
            this.Column3});
            this.gvRec.Location = new System.Drawing.Point(2, 145);
            this.gvRec.Name = "gvRec";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRec.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvRec.RowHeadersVisible = false;
            this.gvRec.RowTemplate.Height = 16;
            this.gvRec.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvRec.Size = new System.Drawing.Size(585, 103);
            this.gvRec.TabIndex = 104;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(25)))), ((int)(((byte)(116)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Reminders";
            // 
            // lstReminders
            // 
            this.lstReminders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lstReminders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstReminders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lstReminders.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lstReminders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstReminders.Location = new System.Drawing.Point(7, 26);
            this.lstReminders.MultiSelect = false;
            this.lstReminders.Name = "lstReminders";
            this.lstReminders.Size = new System.Drawing.Size(579, 89);
            this.lstReminders.TabIndex = 6;
            this.lstReminders.UseCompatibleStateImageBehavior = false;
            this.lstReminders.View = System.Windows.Forms.View.Details;
            this.lstReminders.DoubleClick += new System.EventHandler(this.lstReminders_DoubleClick);
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(25)))), ((int)(((byte)(116)))));
            this.rectangleShape2.Location = new System.Drawing.Point(0, 0);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(1205, 20);
            this.rectangleShape2.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Log On";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn1.HeaderText = "Log On";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 130;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Log Off";
            this.dataGridViewTextBoxColumn2.HeaderText = "Log Off";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Login Company";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn4.HeaderText = "Company";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Login IP";
            this.Column1.HeaderText = "IP";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Login PC";
            this.Column2.HeaderText = "PC";
            this.Column2.Name = "Column2";
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Login Domain";
            this.Column3.HeaderText = "Domain";
            this.Column3.Name = "Column3";
            this.Column3.Width = 135;
            // 
            // frmNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1206, 529);
            this.Controls.Add(this.pnlRM);
            this.Controls.Add(this.pnlCA);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNotification";
            this.Text = "Notifications";
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmNotification_FormClosed);
            this.Load += new System.EventHandler(this.frmNotification_Load);
            this.Resize += new System.EventHandler(this.frmNotification_Resize);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlCA.ResumeLayout(false);
            this.pnlCA.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLog)).EndInit();
            this.pnlRM.ResumeLayout(false);
            this.pnlRM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRec)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GradientPanel.GradientPanel pnlTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlCA;
        private System.Windows.Forms.ListView lstControlAct;
        private System.Windows.Forms.Label lblPnlCAHeading;
        private GradientPanel.GradientPanel pnlBottom;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblThoughtHeading;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel pnlRM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstReminders;
        private System.Windows.Forms.PictureBox picLog;
        private System.Windows.Forms.ColumnHeader discription;
        private System.Windows.Forms.ColumnHeader val1;
        private System.Windows.Forms.ColumnHeader val2;
        private System.Windows.Forms.Panel rectangleShape1;
        private System.Windows.Forms.Panel rectangleShape2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gvRec;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}