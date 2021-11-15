namespace FF.WindowsERPClient.Finance
{
    partial class Elite_Upload
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPC = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.optDInv = new System.Windows.Forms.RadioButton();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlLoc = new System.Windows.Forms.Panel();
            this.txtCompAddr = new System.Windows.Forms.TextBox();
            this.lstPC = new System.Windows.Forms.ListView();
            this.btnNone = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.txtPCDesn = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.txtCompDesc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPC1 = new System.Windows.Forms.TextBox();
            this.txtZone = new System.Windows.Forms.TextBox();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.txtSChanel = new System.Windows.Forms.TextBox();
            this.txtChanel = new System.Windows.Forms.TextBox();
            this.txtComp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.DateTimePicker();
            this.txtTo = new System.Windows.Forms.DateTimePicker();
            this.groupBox3.SuspendLayout();
            this.pnlLoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profit Center :";
            // 
            // txtPC
            // 
            this.txtPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPC.Location = new System.Drawing.Point(127, 55);
            this.txtPC.Name = "txtPC";
            this.txtPC.Size = new System.Drawing.Size(134, 21);
            this.txtPC.TabIndex = 1;
            this.txtPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPC_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(265, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "F2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCreate);
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Location = new System.Drawing.Point(3, 366);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(722, 65);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(573, 14);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(71, 45);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Process";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(643, 14);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(71, 45);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // optDInv
            // 
            this.optDInv.AutoSize = true;
            this.optDInv.Location = new System.Drawing.Point(52, 21);
            this.optDInv.Name = "optDInv";
            this.optDInv.Size = new System.Drawing.Size(99, 17);
            this.optDInv.TabIndex = 29;
            this.optDInv.Text = "Dealer Invoices";
            this.optDInv.UseVisualStyleBackColor = true;
            this.optDInv.Visible = false;
            // 
            // txtFile
            // 
            this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFile.Location = new System.Drawing.Point(127, 162);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(261, 21);
            this.txtFile.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "File Name :";
            // 
            // pnlLoc
            // 
            this.pnlLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLoc.Controls.Add(this.txtCompAddr);
            this.pnlLoc.Controls.Add(this.lstPC);
            this.pnlLoc.Controls.Add(this.btnNone);
            this.pnlLoc.Controls.Add(this.button1);
            this.pnlLoc.Controls.Add(this.btnAll);
            this.pnlLoc.Controls.Add(this.btnAddItem);
            this.pnlLoc.Controls.Add(this.txtPCDesn);
            this.pnlLoc.Controls.Add(this.textBox11);
            this.pnlLoc.Controls.Add(this.textBox12);
            this.pnlLoc.Controls.Add(this.textBox13);
            this.pnlLoc.Controls.Add(this.textBox14);
            this.pnlLoc.Controls.Add(this.textBox15);
            this.pnlLoc.Controls.Add(this.txtCompDesc);
            this.pnlLoc.Controls.Add(this.label7);
            this.pnlLoc.Controls.Add(this.label8);
            this.pnlLoc.Controls.Add(this.label9);
            this.pnlLoc.Controls.Add(this.label10);
            this.pnlLoc.Controls.Add(this.label11);
            this.pnlLoc.Controls.Add(this.txtPC1);
            this.pnlLoc.Controls.Add(this.txtZone);
            this.pnlLoc.Controls.Add(this.txtRegion);
            this.pnlLoc.Controls.Add(this.txtArea);
            this.pnlLoc.Controls.Add(this.txtSChanel);
            this.pnlLoc.Controls.Add(this.txtChanel);
            this.pnlLoc.Controls.Add(this.txtComp);
            this.pnlLoc.Controls.Add(this.label12);
            this.pnlLoc.Controls.Add(this.label14);
            this.pnlLoc.Enabled = false;
            this.pnlLoc.Location = new System.Drawing.Point(5, 190);
            this.pnlLoc.Name = "pnlLoc";
            this.pnlLoc.Size = new System.Drawing.Size(720, 178);
            this.pnlLoc.TabIndex = 41;
            // 
            // txtCompAddr
            // 
            this.txtCompAddr.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtCompAddr.Enabled = false;
            this.txtCompAddr.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompAddr.Location = new System.Drawing.Point(480, 5);
            this.txtCompAddr.Name = "txtCompAddr";
            this.txtCompAddr.Size = new System.Drawing.Size(26, 19);
            this.txtCompAddr.TabIndex = 80;
            this.txtCompAddr.Visible = false;
            // 
            // lstPC
            // 
            this.lstPC.CheckBoxes = true;
            this.lstPC.Location = new System.Drawing.Point(518, 7);
            this.lstPC.Name = "lstPC";
            this.lstPC.Size = new System.Drawing.Size(192, 139);
            this.lstPC.TabIndex = 79;
            this.lstPC.UseCompatibleStateImageBehavior = false;
            this.lstPC.View = System.Windows.Forms.View.List;
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(587, 153);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(54, 21);
            this.btnNone.TabIndex = 77;
            this.btnNone.Text = "None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(647, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 21);
            this.button1.TabIndex = 76;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(527, 153);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(54, 21);
            this.btnAll.TabIndex = 75;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.right_arrow_icon;
            this.btnAddItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddItem.Location = new System.Drawing.Point(481, 67);
            this.btnAddItem.MaximumSize = new System.Drawing.Size(32, 36);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(31, 36);
            this.btnAddItem.TabIndex = 73;
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // txtPCDesn
            // 
            this.txtPCDesn.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtPCDesn.Enabled = false;
            this.txtPCDesn.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPCDesn.Location = new System.Drawing.Point(150, 146);
            this.txtPCDesn.Name = "txtPCDesn";
            this.txtPCDesn.Size = new System.Drawing.Size(324, 19);
            this.txtPCDesn.TabIndex = 71;
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox11.Enabled = false;
            this.textBox11.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox11.Location = new System.Drawing.Point(150, 123);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(324, 19);
            this.textBox11.TabIndex = 70;
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox12.Enabled = false;
            this.textBox12.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.Location = new System.Drawing.Point(150, 99);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(324, 19);
            this.textBox12.TabIndex = 69;
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox13.Enabled = false;
            this.textBox13.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox13.Location = new System.Drawing.Point(150, 76);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(324, 19);
            this.textBox13.TabIndex = 68;
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox14.Enabled = false;
            this.textBox14.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.Location = new System.Drawing.Point(150, 52);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(324, 19);
            this.textBox14.TabIndex = 67;
            // 
            // textBox15
            // 
            this.textBox15.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox15.Enabled = false;
            this.textBox15.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox15.Location = new System.Drawing.Point(150, 29);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(324, 19);
            this.textBox15.TabIndex = 66;
            // 
            // txtCompDesc
            // 
            this.txtCompDesc.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtCompDesc.Enabled = false;
            this.txtCompDesc.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompDesc.Location = new System.Drawing.Point(150, 5);
            this.txtCompDesc.Name = "txtCompDesc";
            this.txtCompDesc.Size = new System.Drawing.Size(324, 19);
            this.txtCompDesc.TabIndex = 65;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 149);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Profit Center";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 126);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 63;
            this.label8.Text = "Zone";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 102);
            this.label9.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 62;
            this.label9.Text = "Region";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 79);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "Area";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 55);
            this.label11.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 60;
            this.label11.Text = "Sub Channel";
            // 
            // txtPC1
            // 
            this.txtPC1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPC1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPC1.Location = new System.Drawing.Point(80, 146);
            this.txtPC1.Name = "txtPC1";
            this.txtPC1.Size = new System.Drawing.Size(65, 19);
            this.txtPC1.TabIndex = 58;
            // 
            // txtZone
            // 
            this.txtZone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtZone.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZone.Location = new System.Drawing.Point(80, 123);
            this.txtZone.Name = "txtZone";
            this.txtZone.Size = new System.Drawing.Size(65, 19);
            this.txtZone.TabIndex = 57;
            // 
            // txtRegion
            // 
            this.txtRegion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRegion.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegion.Location = new System.Drawing.Point(80, 99);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.Size = new System.Drawing.Size(65, 19);
            this.txtRegion.TabIndex = 56;
            // 
            // txtArea
            // 
            this.txtArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtArea.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.Location = new System.Drawing.Point(80, 76);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(65, 19);
            this.txtArea.TabIndex = 55;
            // 
            // txtSChanel
            // 
            this.txtSChanel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSChanel.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSChanel.Location = new System.Drawing.Point(80, 52);
            this.txtSChanel.Name = "txtSChanel";
            this.txtSChanel.Size = new System.Drawing.Size(65, 19);
            this.txtSChanel.TabIndex = 54;
            // 
            // txtChanel
            // 
            this.txtChanel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtChanel.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanel.Location = new System.Drawing.Point(80, 29);
            this.txtChanel.Name = "txtChanel";
            this.txtChanel.Size = new System.Drawing.Size(65, 19);
            this.txtChanel.TabIndex = 53;
            // 
            // txtComp
            // 
            this.txtComp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComp.Enabled = false;
            this.txtComp.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComp.Location = new System.Drawing.Point(80, 5);
            this.txtComp.Name = "txtComp";
            this.txtComp.Size = new System.Drawing.Size(65, 19);
            this.txtComp.TabIndex = 52;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 32);
            this.label12.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 51;
            this.label12.Text = "Channel";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 8);
            this.label14.Margin = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 50;
            this.label14.Text = "Company";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "From Date :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "To Date :";
            // 
            // txtFrom
            // 
            this.txtFrom.Checked = false;
            this.txtFrom.CustomFormat = "dd/MMM/yyyy";
            this.txtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFrom.Location = new System.Drawing.Point(127, 79);
            this.txtFrom.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(104, 21);
            this.txtFrom.TabIndex = 218;
            // 
            // txtTo
            // 
            this.txtTo.Checked = false;
            this.txtTo.CustomFormat = "dd/MMM/yyyy";
            this.txtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtTo.Location = new System.Drawing.Point(127, 100);
            this.txtTo.Margin = new System.Windows.Forms.Padding(0, 1, 10, 0);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(104, 21);
            this.txtTo.TabIndex = 219;
            // 
            // Elite_Upload
            // 
            this.ClientSize = new System.Drawing.Size(729, 436);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.pnlLoc);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.optDInv);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "Elite_Upload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SUN Upload Elite";
            this.groupBox3.ResumeLayout(false);
            this.pnlLoc.ResumeLayout(false);
            this.pnlLoc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPC;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RadioButton optDInv;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlLoc;
        private System.Windows.Forms.TextBox txtCompAddr;
        private System.Windows.Forms.ListView lstPC;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.TextBox txtPCDesn;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox txtCompDesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPC1;
        private System.Windows.Forms.TextBox txtZone;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.TextBox txtSChanel;
        private System.Windows.Forms.TextBox txtChanel;
        private System.Windows.Forms.TextBox txtComp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker txtFrom;
        private System.Windows.Forms.DateTimePicker txtTo;
    }
}
