namespace FF.WindowsERPClient.CommonSearch
{
    partial class VIewManagerReminds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VIewManagerReminds));
            this.pnlReminder = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblPossition = new System.Windows.Forms.Label();
            this.pnlReminder.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlReminder
            // 
            this.pnlReminder.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlReminder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReminder.Controls.Add(this.lblPossition);
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
            this.pnlReminder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReminder.Location = new System.Drawing.Point(0, 0);
            this.pnlReminder.Name = "pnlReminder";
            this.pnlReminder.Size = new System.Drawing.Size(437, 303);
            this.pnlReminder.TabIndex = 141;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnRMClose);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 17);
            this.panel1.TabIndex = 434;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 432;
            this.label3.Text = "Reminds";
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
            this.btnRMClose.TabIndex = 0;
            this.btnRMClose.UseVisualStyleBackColor = false;
            this.btnRMClose.Click += new System.EventHandler(this.btnRMClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 433;
            this.label2.Text = "Seq Num :";
            // 
            // lblSeqNum
            // 
            this.lblSeqNum.AutoSize = true;
            this.lblSeqNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeqNum.Location = new System.Drawing.Point(76, 41);
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
            this.lblCount.Location = new System.Drawing.Point(8, 57);
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
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 431;
            this.label1.Text = "Account :";
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(76, 25);
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
            this.btnNext.Location = new System.Drawing.Point(44, 272);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 24);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = ">";
            this.toolTip1.SetToolTip(this.btnNext, "Next");
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnInactive
            // 
            this.btnInactive.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnInactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInactive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInactive.Location = new System.Drawing.Point(361, 272);
            this.btnInactive.Name = "btnInactive";
            this.btnInactive.Size = new System.Drawing.Size(68, 24);
            this.btnInactive.TabIndex = 0;
            this.btnInactive.Text = "Accept";
            this.toolTip1.SetToolTip(this.btnInactive, "Accept");
            this.btnInactive.UseVisualStyleBackColor = false;
            this.btnInactive.Click += new System.EventHandler(this.btnInactive_Click);
            this.btnInactive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnInactive_KeyDown);
            // 
            // btnPre
            // 
            this.btnPre.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPre.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPre.Location = new System.Drawing.Point(6, 272);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(32, 24);
            this.btnPre.TabIndex = 1;
            this.btnPre.Text = "<";
            this.toolTip1.SetToolTip(this.btnPre, "Previous");
            this.btnPre.UseVisualStyleBackColor = false;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // txtReminder
            // 
            this.txtReminder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReminder.Location = new System.Drawing.Point(6, 77);
            this.txtReminder.Name = "txtReminder";
            this.txtReminder.ReadOnly = true;
            this.txtReminder.Size = new System.Drawing.Size(423, 189);
            this.txtReminder.TabIndex = 3;
            this.txtReminder.Text = "";
            this.txtReminder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReminder_KeyDown);
            // 
            // lblPossition
            // 
            this.lblPossition.AutoSize = true;
            this.lblPossition.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPossition.Location = new System.Drawing.Point(84, 278);
            this.lblPossition.Name = "lblPossition";
            this.lblPossition.Size = new System.Drawing.Size(23, 13);
            this.lblPossition.TabIndex = 435;
            this.lblPossition.Text = "1/1";
            this.lblPossition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VIewManagerReminds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 303);
            this.Controls.Add(this.pnlReminder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VIewManagerReminds";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIewManagerReminds";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.VIewManagerReminds_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VIewManagerReminds_KeyDown);
            this.pnlReminder.ResumeLayout(false);
            this.pnlReminder.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlReminder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRMClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSeqNum;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnInactive;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.RichTextBox txtReminder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblPossition;
    }
}