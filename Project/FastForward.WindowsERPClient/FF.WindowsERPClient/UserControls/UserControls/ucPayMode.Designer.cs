namespace FF.WindowsERPClient.UserControls
{
    partial class ucPayMode
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPayMode));
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblbalanceAmo = new System.Windows.Forms.Label();
            this.lblPaidAmo = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewPayments = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlOthers = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxRefAmo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxRefNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlCheque = new System.Windows.Forms.Panel();
            this.dateTimePickerExpire = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxPreiod = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxPromotion = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxCardType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxBranch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxBank = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxChequeNo = new System.Windows.Forms.TextBox();
            this.lalChecqueCard = new System.Windows.Forms.Label();
            this.textBoxRemark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPayModes = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayments)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.pnlOthers.SuspendLayout();
            this.pnlCheque.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(278, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Months";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(841, 216);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment Details";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlOthers);
            this.groupBox3.Controls.Add(this.lblbalanceAmo);
            this.groupBox3.Controls.Add(this.pnlCheque);
            this.groupBox3.Controls.Add(this.lblPaidAmo);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(423, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(414, 195);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // lblbalanceAmo
            // 
            this.lblbalanceAmo.AutoSize = true;
            this.lblbalanceAmo.Location = new System.Drawing.Point(334, 166);
            this.lblbalanceAmo.Name = "lblbalanceAmo";
            this.lblbalanceAmo.Size = new System.Drawing.Size(28, 13);
            this.lblbalanceAmo.TabIndex = 6;
            this.lblbalanceAmo.Text = "0.00";
            // 
            // lblPaidAmo
            // 
            this.lblPaidAmo.AutoSize = true;
            this.lblPaidAmo.Location = new System.Drawing.Point(116, 166);
            this.lblPaidAmo.Name = "lblPaidAmo";
            this.lblPaidAmo.Size = new System.Drawing.Size(28, 13);
            this.lblPaidAmo.TabIndex = 5;
            this.lblPaidAmo.Text = "0.00";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Indigo;
            this.label14.ForeColor = System.Drawing.Color.Azure;
            this.label14.Location = new System.Drawing.Point(234, 166);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 16);
            this.label14.TabIndex = 4;
            this.label14.Text = "Balance Amount";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Indigo;
            this.label4.ForeColor = System.Drawing.Color.Azure;
            this.label4.Location = new System.Drawing.Point(16, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Paid Amount";
            // 
            // dataGridViewPayments
            // 
            this.dataGridViewPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridViewPayments.Location = new System.Drawing.Point(6, 90);
            this.dataGridViewPayments.Name = "dataGridViewPayments";
            this.dataGridViewPayments.Size = new System.Drawing.Size(402, 96);
            this.dataGridViewPayments.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewPayments);
            this.groupBox2.Controls.Add(this.textBoxRemark);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxAmount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxPayModes);
            this.groupBox2.Location = new System.Drawing.Point(5, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 194);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // pnlOthers
            // 
            this.pnlOthers.Controls.Add(this.button4);
            this.pnlOthers.Controls.Add(this.textBoxRefAmo);
            this.pnlOthers.Controls.Add(this.label12);
            this.pnlOthers.Controls.Add(this.textBoxRefNo);
            this.pnlOthers.Controls.Add(this.label13);
            this.pnlOthers.Location = new System.Drawing.Point(6, 11);
            this.pnlOthers.Name = "pnlOthers";
            this.pnlOthers.Size = new System.Drawing.Size(402, 103);
            this.pnlOthers.TabIndex = 0;
            this.pnlOthers.Visible = false;
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(352, 3);
            this.button4.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(20, 20);
            this.button4.TabIndex = 19;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // textBoxRefAmo
            // 
            this.textBoxRefAmo.Location = new System.Drawing.Point(68, 27);
            this.textBoxRefAmo.Name = "textBoxRefAmo";
            this.textBoxRefAmo.Size = new System.Drawing.Size(158, 20);
            this.textBoxRefAmo.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Ref Amount";
            // 
            // textBoxRefNo
            // 
            this.textBoxRefNo.Location = new System.Drawing.Point(68, 2);
            this.textBoxRefNo.Name = "textBoxRefNo";
            this.textBoxRefNo.Size = new System.Drawing.Size(281, 20);
            this.textBoxRefNo.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Ref No";
            // 
            // pnlCheque
            // 
            this.pnlCheque.Controls.Add(this.dateTimePickerExpire);
            this.pnlCheque.Controls.Add(this.button3);
            this.pnlCheque.Controls.Add(this.button2);
            this.pnlCheque.Controls.Add(this.label11);
            this.pnlCheque.Controls.Add(this.textBoxPreiod);
            this.pnlCheque.Controls.Add(this.label10);
            this.pnlCheque.Controls.Add(this.checkBoxPromotion);
            this.pnlCheque.Controls.Add(this.label9);
            this.pnlCheque.Controls.Add(this.label8);
            this.pnlCheque.Controls.Add(this.comboBoxCardType);
            this.pnlCheque.Controls.Add(this.label7);
            this.pnlCheque.Controls.Add(this.textBoxBranch);
            this.pnlCheque.Controls.Add(this.label6);
            this.pnlCheque.Controls.Add(this.textBoxBank);
            this.pnlCheque.Controls.Add(this.label5);
            this.pnlCheque.Controls.Add(this.textBoxChequeNo);
            this.pnlCheque.Controls.Add(this.lalChecqueCard);
            this.pnlCheque.Location = new System.Drawing.Point(6, 11);
            this.pnlCheque.Name = "pnlCheque";
            this.pnlCheque.Size = new System.Drawing.Size(402, 103);
            this.pnlCheque.TabIndex = 5;
            this.pnlCheque.Visible = false;
            // 
            // dateTimePickerExpire
            // 
            this.dateTimePickerExpire.Checked = false;
            this.dateTimePickerExpire.CustomFormat = "dd/MMM/yyyy";
            this.dateTimePickerExpire.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerExpire.Location = new System.Drawing.Point(241, 48);
            this.dateTimePickerExpire.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.dateTimePickerExpire.Name = "dateTimePickerExpire";
            this.dateTimePickerExpire.Size = new System.Drawing.Size(125, 20);
            this.dateTimePickerExpire.TabIndex = 19;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(346, 20);
            this.button3.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 20);
            this.button3.TabIndex = 18;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(170, 25);
            this.button2.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 17;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBoxPreiod
            // 
            this.textBoxPreiod.Location = new System.Drawing.Point(170, 75);
            this.textBoxPreiod.Name = "textBoxPreiod";
            this.textBoxPreiod.Size = new System.Drawing.Size(102, 20);
            this.textBoxPreiod.TabIndex = 15;
            this.toolTip1.SetToolTip(this.textBoxPreiod, "Please enter Promation period in months");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(127, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Period";
            // 
            // checkBoxPromotion
            // 
            this.checkBoxPromotion.AutoSize = true;
            this.checkBoxPromotion.Location = new System.Drawing.Point(70, 78);
            this.checkBoxPromotion.Name = "checkBoxPromotion";
            this.checkBoxPromotion.Size = new System.Drawing.Size(15, 14);
            this.checkBoxPromotion.TabIndex = 13;
            this.checkBoxPromotion.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Pramotion";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(192, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Expire";
            // 
            // comboBoxCardType
            // 
            this.comboBoxCardType.FormattingEnabled = true;
            this.comboBoxCardType.Items.AddRange(new object[] {
            "",
            "",
            "AMEX",
            "VISA",
            "MASTER",
            "DISCOVER",
            "2CO",
            "SAGE",
            "DELTA",
            "CIRRUS"});
            this.comboBoxCardType.Location = new System.Drawing.Point(65, 51);
            this.comboBoxCardType.Name = "comboBoxCardType";
            this.comboBoxCardType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCardType.TabIndex = 9;
            this.toolTip1.SetToolTip(this.comboBoxCardType, "Please select Card Type");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Card Type";
            // 
            // textBoxBranch
            // 
            this.textBoxBranch.Location = new System.Drawing.Point(241, 21);
            this.textBoxBranch.Name = "textBoxBranch";
            this.textBoxBranch.Size = new System.Drawing.Size(102, 20);
            this.textBoxBranch.TabIndex = 7;
            this.toolTip1.SetToolTip(this.textBoxBranch, "Please enter branch");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Branch";
            // 
            // textBoxBank
            // 
            this.textBoxBank.Location = new System.Drawing.Point(65, 25);
            this.textBoxBank.Name = "textBoxBank";
            this.textBoxBank.Size = new System.Drawing.Size(102, 20);
            this.textBoxBank.TabIndex = 5;
            this.toolTip1.SetToolTip(this.textBoxBank, "Please enter Bank");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Bank";
            // 
            // textBoxChequeNo
            // 
            this.textBoxChequeNo.Location = new System.Drawing.Point(65, 0);
            this.textBoxChequeNo.Name = "textBoxChequeNo";
            this.textBoxChequeNo.Size = new System.Drawing.Size(314, 20);
            this.textBoxChequeNo.TabIndex = 3;
            // 
            // lalChecqueCard
            // 
            this.lalChecqueCard.AutoSize = true;
            this.lalChecqueCard.Location = new System.Drawing.Point(0, 2);
            this.lalChecqueCard.Name = "lalChecqueCard";
            this.lalChecqueCard.Size = new System.Drawing.Size(61, 13);
            this.lalChecqueCard.TabIndex = 2;
            this.lalChecqueCard.Text = "Cheque No";
            // 
            // textBoxRemark
            // 
            this.textBoxRemark.Location = new System.Drawing.Point(66, 36);
            this.textBoxRemark.Multiline = true;
            this.textBoxRemark.Name = "textBoxRemark";
            this.textBoxRemark.Size = new System.Drawing.Size(314, 47);
            this.textBoxRemark.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textBoxRemark, "Please enter remarks");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Remark";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(337, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add";
            this.toolTip1.SetToolTip(this.button1, "Press to add payment");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amount";
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Location = new System.Drawing.Point(221, 10);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(100, 20);
            this.textBoxAmount.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxAmount, "Please enterpay amount");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pay Mode";
            // 
            // comboBoxPayModes
            // 
            this.comboBoxPayModes.FormattingEnabled = true;
            this.comboBoxPayModes.Location = new System.Drawing.Point(66, 10);
            this.comboBoxPayModes.Name = "comboBoxPayModes";
            this.comboBoxPayModes.Size = new System.Drawing.Size(99, 21);
            this.comboBoxPayModes.TabIndex = 1;
            this.toolTip1.SetToolTip(this.comboBoxPayModes, "Please select Pay Mode");
            this.comboBoxPayModes.SelectedIndexChanged += new System.EventHandler(this.comboBoxPayModes_SelectedIndexChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "SARD_PAY_TP";
            this.Column2.HeaderText = "Payment Type";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "sard_chq_bank_cd";
            this.Column3.HeaderText = "Bank";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "sard_chq_branch";
            this.Column4.HeaderText = "Branch";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "sard_cc_tp";
            this.Column5.HeaderText = "CC Type";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "sard_anal_3";
            this.Column6.HeaderText = "Bank Charge";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "sard_settle_amt";
            this.Column7.HeaderText = "Amount";
            this.Column7.Name = "Column7";
            // 
            // ucPayMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucPayMode";
            this.Size = new System.Drawing.Size(847, 222);
            this.Load += new System.EventHandler(this.ucPayMode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayments)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlOthers.ResumeLayout(false);
            this.pnlOthers.PerformLayout();
            this.pnlCheque.ResumeLayout(false);
            this.pnlCheque.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnlOthers;
        private System.Windows.Forms.TextBox textBoxRefAmo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxRefNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlCheque;
        private System.Windows.Forms.TextBox textBoxPreiod;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxPromotion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxCardType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxBranch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxBank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxChequeNo;
        private System.Windows.Forms.Label lalChecqueCard;
        private System.Windows.Forms.TextBox textBoxRemark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPayModes;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePickerExpire;
        private System.Windows.Forms.DataGridView dataGridViewPayments;
        private System.Windows.Forms.Label lblbalanceAmo;
        private System.Windows.Forms.Label lblPaidAmo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}
