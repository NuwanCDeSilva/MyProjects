using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;

namespace FF.AbansTours.UserControls
{
    public partial class uc_ViewApprovedRequests : System.Web.UI.UserControl
    {   //uc_ddlReqNo
       // private 
       // private string selectedReqNum;

        public string SelectedReqNum
        {
            get { return uc_lblSelectedReqNum.Text.Trim(); }
            set { uc_lblSelectedReqNum.Text = value.Trim(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void bindReqNums(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            ApproveRequestUC APPROVE=new ApproveRequestUC();
            List<string> reqNumList = new List<string>();
            
            reqNumList.Add("");
            reqNumList = APPROVE.getApprovedReqNumbersList(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
            reqNumList.Add("");
            uc_ddlReqNo.DataSource = reqNumList;
            uc_ddlReqNo.DataBind();
            uc_ddlReqNo.SelectedValue = "";
            if (reqNumList != null)
            {
               // SelectedReqNum = reqNumList[0];
            }
        }

        protected void bindDetaisToGrid(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            ApproveRequestUC APPROVE = new ApproveRequestUC();
            DataTable bindTable = new DataTable();
            bindTable = APPROVE.getRequestDetails(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
            if( bindTable!=null)
            {
                grv_ViewReqDet.DataSource= bindTable;
                grv_ViewReqDet.DataBind();
            }
           
        }

        public void LoadUserControl(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            //appType: eg. Account number
            uc_lblSelectedReqNum.Text = "";
            bindReqNums(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
            bindDetaisToGrid(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
        }

        protected void uc_ddlReqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
           // SelectedReqNum
            uc_lblSelectedReqNum.Text = uc_ddlReqNo.SelectedValue.ToString();
            SelectedReqNum = uc_ddlReqNo.SelectedValue.ToString();
        }
        
    }
}