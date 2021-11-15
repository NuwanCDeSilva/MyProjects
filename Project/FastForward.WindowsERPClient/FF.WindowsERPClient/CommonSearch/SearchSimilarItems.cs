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
    public partial class SearchSimilarItems : Base
    {
        public TextBox obj_TragetTextBox;
        public Boolean IsDateSearch = false;
        public string SearchParams = "";
        public int ReturnIndex = 0;

        #region Properties
        private string _documentType = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DocumentType
        {
            get { return _documentType; }
            set { _documentType = value; }
        }

        private string _itemCode = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ItemCode
        {
            get { return _itemCode; }
            set { _itemCode = value; }
        }

        private DateTime _functionDate = DateTime.Now.Date;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime FunctionDate
        {
            get { return _functionDate; }
            set { _functionDate = value; }
        }

        private string _documentNo = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DocumentNo
        {
            get { return _documentNo; }
            set { _documentNo = value; }
        }

        private string _promoCode = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PromotionCode
        {
            get { return _promoCode; }
            set { _promoCode = value; }
        }
        #endregion

        #region Form Events
        public SearchSimilarItems()
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
                obj_TragetTextBox.Text = dvResult.Rows[dvResult.CurrentCell.RowIndex].Cells["MISI_SIM_ITM_CD"].Value.ToString();
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
                obj_TragetTextBox.Text = dvResult.Rows[e.RowIndex].Cells["MISI_SIM_ITM_CD"].Value.ToString();
                Cursor.Current = Cursors.Default;
                this.Close();
            }
        }
        
        #endregion

        private void SearchSimilarItems_Load(object sender, EventArgs e)
        {
            List<MasterItemSimilar> dt = CHNLSVC.Inventory.GetSimilarItems(DocumentType, ItemCode, BaseCls.GlbUserComCode, FunctionDate.Date, DocumentNo, PromotionCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf);
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
