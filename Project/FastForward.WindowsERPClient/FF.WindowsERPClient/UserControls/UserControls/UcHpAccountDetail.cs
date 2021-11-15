using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.UserControls
{
    public partial class UcHpAccountDetail : UserControl
    {
        public UcHpAccountDetail()
        {
            InitializeComponent();
        }

        string _uc_hpa_acc_no = "";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Uc_hpa_acc_no
        {
            get { return _uc_hpa_acc_no; }
            set { _uc_hpa_acc_no = value; LoadDetails(); }
        }

        //Get_hpAcc_TransactionDet 
        protected void LoadDetails()
        {           
            HpAccountSummary SUMMARY = new HpAccountSummary();
            grv_ucAccDetails.AutoGenerateColumns = false;
            grv_ucAccDetails.DataSource = SUMMARY.getAccDetails(Uc_hpa_acc_no);
            //grv_ucAccDetails.DataBind();
        }
        public void Clear()
        {
            DataTable dt = new DataTable();
            grv_ucAccDetails.DataSource = dt;
            Uc_hpa_acc_no = "";
           // InitializeComponent();
            //grv_ucAccDetails.DataBind();
        }

        private void grv_ucAccDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grv_ucAccDetails_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try {
                Int32 row = e.RowIndex;
                // grv_ucAccDetails.Rows(row) = true;
                grv_ucAccDetails.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception ex) { }
           
        }
        
    }
}
