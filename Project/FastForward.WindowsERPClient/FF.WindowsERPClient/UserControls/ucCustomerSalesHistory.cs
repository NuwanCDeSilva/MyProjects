using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace FF.WindowsERPClient.UserControls
{
	public partial class ucCustomerSalesHistory: UserControl
	{
		public ucCustomerSalesHistory()
		{
			InitializeComponent();
		}
        string _uc_cus_code = "";

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        private void ucCustomerSalesHistory_Load(object sender, EventArgs e)
        {
            //UcHpAccountSummary_Resize();
        }
        
        
        public string Uc_cus_code
        {
            get { return _uc_cus_code; }
            set { _uc_cus_code = value; }
        }


        public void LoadDetails(string CusCode)
        {
            try
            {
                Initialized();
                DataTable cus_dt = new DataTable();
                cus_dt.Columns.Add(new DataColumn("Cus_Name", typeof(string)));
                lblCustomerCode.Text = CusCode.ToString();
                HpAccountSummary SUMMARY = new HpAccountSummary();
                grv_ucCusInvDetails.AutoGenerateColumns = false;
                //grv_ucCusInvDetails.Columns[8].DefaultCellStyle.Format = "#.###";
                DataTable dt = SUMMARY.getCusInvoiceDetails(CusCode);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lblCustomerName.Text = dr["SAH_CUS_NAME"].ToString();
                    
                    grv_ucCusInvDetails.Visible = true;
                    grv_ucCusInvDetails.DataSource = dt;
                }
                else
                {
                    lblCustomerCode.Text = "";
                    lblCustomerName.Text = "";
                    grv_ucCusInvDetails.Visible = false;
                    lblUsrMsg.Visible = true;
                }
            }
            catch
            {
                lblUsrMsg.Text = "::Some error has occured while processing";  
            }
        }

        private void Initialized()
        {
            lblUsrMsg.Visible = false;
            //if(grv_ucCusInvDetails.Rows.Count>0)
            //grv_ucCusInvDetails.Rows.Clear();
        }

       


       
    }
}
