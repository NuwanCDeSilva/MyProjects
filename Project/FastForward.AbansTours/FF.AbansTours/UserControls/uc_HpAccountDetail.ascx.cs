using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.UserControls
{
    public partial class uc_HpAccountDetail : System.Web.UI.UserControl
    {
        string _uc_hpa_acc_no = "";
        Unit _uc_panel_height = 91;

        public Unit Uc_panel_height
        {
            get { return _uc_panel_height; }
            set { _uc_panel_height = value; }
        }

 

        public string Uc_hpa_acc_no
        {
            get { return _uc_hpa_acc_no; }
            set { _uc_hpa_acc_no = value; LoadDetails(); }
        }

        //Get_hpAcc_TransactionDet 
        protected void LoadDetails()
        {
            Panel_grvDet.Height = Uc_panel_height;
            Hp_AccountSummary SUMMARY = new Hp_AccountSummary();

            grv_ucAccDetails.DataSource = SUMMARY.getAccDetails(Uc_hpa_acc_no);
            grv_ucAccDetails.DataBind();
        }
        public void Clear()
        {
            grv_ucAccDetails.DataSource = null;
            grv_ucAccDetails.DataBind();
        }
        
    }
}