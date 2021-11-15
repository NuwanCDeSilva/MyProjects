using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.CommonSearch
{
    public partial class ViewSubSerialItems : Base
    {
        public TextBox obj_TragetTextBox;
        public Boolean IsDateSearch = false;
        public string SearchParams = "";
        public int ReturnIndex = 0;

        #region Properties
        private string _mainItemCode = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MainItemCode
        {
            get { return _mainItemCode; }
            set { _mainItemCode = value; }
        }

        private string _mainSerialNo = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MainSerialNo
        {
            get { return _mainSerialNo; }
            set { _mainSerialNo = value; }
        }

        private int _SerialID = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SerialID
        {
            get { return _SerialID; }
            set { _SerialID = value; }
        }
        #endregion

        #region Form Events
        public ViewSubSerialItems()
        {
            InitializeComponent();
        }

        public void dvResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dvResult.ColumnCount > 0)
                {
                Cursor.Current = Cursors.WaitCursor;
                //obj_TragetTextBox.Text = dvResult.Rows[dvResult.CurrentCell.RowIndex].Cells["MISI_SIM_ITM_CD"].Value.ToString();
                obj_TragetTextBox.Text = "";
                Cursor.Current = Cursors.Default;
                this.Close();
            }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvResult.ColumnCount > 0 && e.RowIndex >= 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                //obj_TragetTextBox.Text = dvResult.Rows[e.RowIndex].Cells["MISI_SIM_ITM_CD"].Value.ToString();
                obj_TragetTextBox.Text = "";
                Cursor.Current = Cursors.Default;
                this.Close();
            }
        }
        #endregion

        private void ViewSubSerialItems_Load(object sender, EventArgs e)
        {
            List<InventoryWarrantySubDetail> dt = CHNLSVC.Inventory.GetSubItemSerials(MainItemCode, MainSerialNo, _SerialID);
            if (dt != null)
            {
                if (dt.Count > 0)
                {
                    dvResult.AutoGenerateColumns = false;
                    dvResult.DataSource = dt;
                }
            }
        }

    }
}
