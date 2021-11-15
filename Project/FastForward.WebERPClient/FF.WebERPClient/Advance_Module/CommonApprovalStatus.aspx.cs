using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;


namespace FF.WebERPClient.Advance_Module
{
    public partial class CommonApprovalStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTabHeaderText(4, 3, 5);
                this.BindMRNGridData();
                this.BindINTRGridData();
            }
        }

        private void LoadTabHeaderText(int _inventoryCount,int _salesCount,int _hpCount)
        {
            tbpInventory.HeaderText = "Inventory (" + _inventoryCount.ToString() + ")";
            tbpSales.HeaderText = "Sales (" + _salesCount.ToString() + ")";
            tbpHirePurchase.HeaderText = "Hire Purchase (" + _hpCount.ToString() + ")";
        }

        private void BindMRNGridData()
        {
            List<ApprovalStatus> _approvalStatusList = new List<ApprovalStatus>();
            _approvalStatusList.Add(new ApprovalStatus() { DocumentNo = "R2AM-MRN-0001", Remarks = "R2AM location request no of 10 Items from DPS32" });
            _approvalStatusList.Add(new ApprovalStatus() { DocumentNo = "R2AM-MRN-0002", Remarks = "R2AM location request no of 02 Items from DPS32" });

            if ((_approvalStatusList != null) && (_approvalStatusList.Count > 0))
            {
                lblMRN.Text = "MRN (" + _approvalStatusList.Count.ToString() + ")";
                gvMRN.DataSource = _approvalStatusList;
                gvMRN.DataBind();
            }
        }


        private void BindINTRGridData()
        {
            List<ApprovalStatus> _approvalStatusList = new List<ApprovalStatus>();
            _approvalStatusList.Add(new ApprovalStatus() { DocumentNo = "R2AM-INTR-0001", Remarks = "R2AM to RABT Item-LGRF242SLV (Stock Item)" });
            _approvalStatusList.Add(new ApprovalStatus() { DocumentNo = "R2AM-INTR-0002", Remarks = "R2AM to RABT Item-LGTV21FA20KE (Customer Item)" });

            if ((_approvalStatusList != null) && (_approvalStatusList.Count > 0))
            {
                lblINTR.Text = "INTER TRANSFER (" + _approvalStatusList.Count.ToString() + ")";
                gvINTR.DataSource = _approvalStatusList;
                gvINTR.DataBind();
            }          
        }

    }
}