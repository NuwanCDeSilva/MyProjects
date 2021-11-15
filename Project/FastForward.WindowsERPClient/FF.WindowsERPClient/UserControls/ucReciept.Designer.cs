namespace FF.WindowsERPClient.UserControls
{
    partial class ucReciept
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucReciept));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlManualSystem = new System.Windows.Forms.Panel();
            this.radioButtonManual = new System.Windows.Forms.RadioButton();
            this.radioButtonSystem = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInsu = new System.Windows.Forms.Label();
            this.label_INSU = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDiriya = new System.Windows.Forms.Label();
            this.lblCollection = new System.Windows.Forms.Label();
            this.pnlInsValues = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.textBoxRecNo = new System.Windows.Forms.TextBox();
            this.comboBoxPrefix = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewreciepts = new System.Windows.Forms.DataGridView();
            this.Prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reciept_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblbalanceAmo = new System.Windows.Forms.Label();
            this.lblPaidAmo = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlManualSystem.SuspendLayout();
            this.pnlInsValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewreciepts)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlManualSystem
            // 
            this.pnlManualSystem.Controls.Add(this.radioButtonManual);
            this.pnlManualSystem.Controls.Add(this.radioButtonSystem);
            this.pnlManualSystem.Controls.Add(this.label4);
            this.pnlManualSystem.Location = new System.Drawing.Point(0, 0);
            this.pnlManualSystem.Name = "pnlManualSystem";
            this.pnlManualSystem.Size = new System.Drawing.Size(356, 24);
            this.pnlManualSystem.TabIndex = 0;
            // 
            // radioButtonManual
            // 
            this.radioButtonManual.AutoSize = true;
            this.radioButtonManual.Checked = true;
            this.radioButtonManual.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonManual.Location = new System.Drawing.Point(179, 4);
            this.radioButtonManual.Name = "radioButtonManual";
            this.radioButtonManual.Size = new System.Drawing.Size(59, 17);
            this.radioButtonManual.TabIndex = 2;
            this.radioButtonManual.TabStop = true;
            this.radioButtonManual.Text = "Manual";
            this.radioButtonManual.UseVisualStyleBackColor = true;
            this.radioButtonManual.CheckedChanged += new System.EventHandler(this.radioButtonManual_CheckedChanged);
            // 
            // radioButtonSystem
            // 
            this.radioButtonSystem.AutoSize = true;
            this.radioButtonSystem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSystem.Location = new System.Drawing.Point(75, 4);
            this.radioButtonSystem.Name = "radioButtonSystem";
            this.radioButtonSystem.Size = new System.Drawing.Size(60, 17);
            this.radioButtonSystem.TabIndex = 1;
            this.radioButtonSystem.Text = "System";
            this.radioButtonSystem.UseVisualStyleBackColor = true;
            this.radioButtonSystem.CheckedChanged += new System.EventHandler(this.radioButtonSystem_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(363, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(57, 21);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.buttonCancel, "Click here to cancel");
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Visible = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Vehical Insu";
            // 
            // lblInsu
            // 
            this.lblInsu.Location = new System.Drawing.Point(52, 15);
            this.lblInsu.Name = "lblInsu";
            this.lblInsu.Size = new System.Drawing.Size(69, 13);
            this.lblInsu.TabIndex = 3;
            this.lblInsu.Text = "0.00";
            this.lblInsu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_INSU
            // 
            this.label_INSU.AutoSize = true;
            this.label_INSU.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_INSU.Location = new System.Drawing.Point(193, 0);
            this.label_INSU.Name = "label_INSU";
            this.label_INSU.Size = new System.Drawing.Size(71, 13);
            this.label_INSU.TabIndex = 4;
            this.label_INSU.Text = "HP Insurance";
            this.label_INSU.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(336, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Collection";
            // 
            // lblDiriya
            // 
            this.lblDiriya.Location = new System.Drawing.Point(180, 16);
            this.lblDiriya.Name = "lblDiriya";
            this.lblDiriya.Size = new System.Drawing.Size(84, 13);
            this.lblDiriya.TabIndex = 6;
            this.lblDiriya.Text = "0.00";
            this.lblDiriya.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCollection
            // 
            this.lblCollection.Location = new System.Drawing.Point(269, 15);
            this.lblCollection.Name = "lblCollection";
            this.lblCollection.Size = new System.Drawing.Size(120, 13);
            this.lblCollection.TabIndex = 7;
            this.lblCollection.Text = "0.00";
            this.lblCollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlInsValues
            // 
            this.pnlInsValues.Controls.Add(this.label8);
            this.pnlInsValues.Controls.Add(this.label3);
            this.pnlInsValues.Controls.Add(this.label1);
            this.pnlInsValues.Controls.Add(this.lblCollection);
            this.pnlInsValues.Controls.Add(this.lblInsu);
            this.pnlInsValues.Controls.Add(this.lblDiriya);
            this.pnlInsValues.Controls.Add(this.label_INSU);
            this.pnlInsValues.Location = new System.Drawing.Point(3, 163);
            this.pnlInsValues.Name = "pnlInsValues";
            this.pnlInsValues.Size = new System.Drawing.Size(419, 28);
            this.pnlInsValues.TabIndex = 8;
            this.pnlInsValues.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Arrears";
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAmount.Location = new System.Drawing.Point(282, 28);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(71, 20);
            this.textBoxAmount.TabIndex = 22;
            this.textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.textBoxAmount, "Please enter amount");
            this.textBoxAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAmount_KeyDown);
            // 
            // textBoxRecNo
            // 
            this.textBoxRecNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRecNo.Location = new System.Drawing.Point(165, 28);
            this.textBoxRecNo.Name = "textBoxRecNo";
            this.textBoxRecNo.Size = new System.Drawing.Size(71, 20);
            this.textBoxRecNo.TabIndex = 20;
            this.toolTip1.SetToolTip(this.textBoxRecNo, "Please enter reciept no");
            this.textBoxRecNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRecNo_KeyDown);
            this.textBoxRecNo.Leave += new System.EventHandler(this.textBoxRecNo_Leave);
            // 
            // comboBoxPrefix
            // 
            this.comboBoxPrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrefix.DropDownWidth = 100;
            this.comboBoxPrefix.FormattingEnabled = true;
            this.comboBoxPrefix.Location = new System.Drawing.Point(38, 28);
            this.comboBoxPrefix.Name = "comboBoxPrefix";
            this.comboBoxPrefix.Size = new System.Drawing.Size(66, 21);
            this.comboBoxPrefix.TabIndex = 17;
            this.toolTip1.SetToolTip(this.comboBoxPrefix, "Please select prefix");
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.White;
            this.buttonAdd.BackgroundImage = global::FF.WindowsERPClient.Properties.Resources.downloadarrowicon;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(353, 27);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(19, 21);
            this.buttonAdd.TabIndex = 24;
            this.toolTip1.SetToolTip(this.buttonAdd, "Click here to add reciept");
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.White;
            this.buttonDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDelete.BackgroundImage")));
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(380, 26);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(21, 22);
            this.buttonDelete.TabIndex = 23;
            this.buttonDelete.Text = "Delete";
            this.toolTip1.SetToolTip(this.buttonDelete, "Click here to delete last reciept");
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(238, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Amount";
            // 
            // dataGridViewreciepts
            // 
            this.dataGridViewreciepts.AllowUserToAddRows = false;
            this.dataGridViewreciepts.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.dataGridViewreciepts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewreciepts.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewreciepts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewreciepts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewreciepts.ColumnHeadersVisible = false;
            this.dataGridViewreciepts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Prefix,
            this.Reciept_No,
            this.Amount,
            this.Column1});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewreciepts.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewreciepts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewreciepts.Location = new System.Drawing.Point(3, 50);
            this.dataGridViewreciepts.Name = "dataGridViewreciepts";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewreciepts.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewreciepts.RowHeadersVisible = false;
            this.dataGridViewreciepts.RowTemplate.Height = 16;
            this.dataGridViewreciepts.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewreciepts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewreciepts.Size = new System.Drawing.Size(398, 138);
            this.dataGridViewreciepts.TabIndex = 18;
            // 
            // Prefix
            // 
            this.Prefix.DataPropertyName = "SAR_PREFIX";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prefix.DefaultCellStyle = dataGridViewCellStyle3;
            this.Prefix.HeaderText = "Prefix";
            this.Prefix.Name = "Prefix";
            this.Prefix.Width = 85;
            // 
            // Reciept_No
            // 
            this.Reciept_No.DataPropertyName = "SAR_MANUAL_REF_NO";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.NullValue = null;
            this.Reciept_No.DefaultCellStyle = dataGridViewCellStyle4;
            this.Reciept_No.HeaderText = "Reciept No";
            this.Reciept_No.Name = "Reciept_No";
            this.Reciept_No.Width = 85;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "SAR_TOT_SETTLE_AMT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle5;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "isMgr";
            this.Column1.HeaderText = "Mgr";
            this.Column1.Name = "Column1";
            this.Column1.Width = 110;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Prefix";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(110, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Reciept No";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblbalanceAmo);
            this.panel1.Controls.Add(this.lblPaidAmo);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(435, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 186);
            this.panel1.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.MidnightBlue;
            this.label9.ForeColor = System.Drawing.Color.Azure;
            this.label9.Location = new System.Drawing.Point(6, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 16);
            this.label9.TabIndex = 3;
            this.label9.Text = "Paid Amount";
            // 
            // lblbalanceAmo
            // 
            this.lblbalanceAmo.Location = new System.Drawing.Point(9, 110);
            this.lblbalanceAmo.Name = "lblbalanceAmo";
            this.lblbalanceAmo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblbalanceAmo.Size = new System.Drawing.Size(90, 13);
            this.lblbalanceAmo.TabIndex = 6;
            this.lblbalanceAmo.Text = "0.00";
            this.lblbalanceAmo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaidAmo
            // 
            this.lblPaidAmo.Location = new System.Drawing.Point(9, 66);
            this.lblPaidAmo.Name = "lblPaidAmo";
            this.lblPaidAmo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPaidAmo.Size = new System.Drawing.Size(90, 13);
            this.lblPaidAmo.TabIndex = 5;
            this.lblPaidAmo.Text = "0.00";
            this.lblPaidAmo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.MidnightBlue;
            this.label14.ForeColor = System.Drawing.Color.Azure;
            this.label14.Location = new System.Drawing.Point(6, 86);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 16);
            this.label14.TabIndex = 4;
            this.label14.Text = "Balance Amount";
            // 
            // ucReciept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxRecNo);
            this.Controls.Add(this.dataGridViewreciepts);
            this.Controls.Add(this.comboBoxPrefix);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pnlInsValues);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.pnlManualSystem);
            this.Name = "ucReciept";
            this.Size = new System.Drawing.Size(423, 194);
            this.Load += new System.EventHandler(this.ucReciept_Load);
            this.pnlManualSystem.ResumeLayout(false);
            this.pnlManualSystem.PerformLayout();
            this.pnlInsValues.ResumeLayout(false);
            this.pnlInsValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewreciepts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlManualSystem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInsu;
        private System.Windows.Forms.Label label_INSU;
        private System.Windows.Forms.RadioButton radioButtonManual;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDiriya;
        private System.Windows.Forms.Label lblCollection;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel pnlInsValues;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxRecNo;
        private System.Windows.Forms.DataGridView dataGridViewreciepts;
        private System.Windows.Forms.ComboBox comboBoxPrefix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblbalanceAmo;
        private System.Windows.Forms.Label lblPaidAmo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reciept_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        public System.Windows.Forms.RadioButton radioButtonSystem;
    }
}
