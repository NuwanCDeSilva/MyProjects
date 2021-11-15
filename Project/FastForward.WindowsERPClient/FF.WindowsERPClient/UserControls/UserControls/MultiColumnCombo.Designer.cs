﻿namespace FF.WindowsERPClient.UserControls
{
    partial class MultiColumnCombo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.gvDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // txtValue
            // 
            this.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValue.Location = new System.Drawing.Point(1, 1);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 21);
            this.txtValue.TabIndex = 0;
            this.txtValue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtValue_MouseClick);
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // gvDataGrid
            // 
            this.gvDataGrid.AllowUserToAddRows = false;
            this.gvDataGrid.AllowUserToDeleteRows = false;
            this.gvDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.gvDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvDataGrid.GridColor = System.Drawing.Color.White;
            this.gvDataGrid.Location = new System.Drawing.Point(1, 21);
            this.gvDataGrid.Name = "gvDataGrid";
            this.gvDataGrid.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvDataGrid.RowHeadersVisible = false;
            this.gvDataGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gvDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvDataGrid.Size = new System.Drawing.Size(379, 147);
            this.gvDataGrid.TabIndex = 4;
            this.gvDataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvDataGrid_CellMouseClick);
            this.gvDataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvDataGrid_ColumnHeaderMouseClick);
            this.gvDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvDataGrid_KeyDown);
            // 
            // MultiColumnCombo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.gvDataGrid);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MultiColumnCombo";
            this.Size = new System.Drawing.Size(101, 22);
            ((System.ComponentModel.ISupportInitialize)(this.gvDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.DataGridView gvDataGrid;
    }
}
