using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Globalization;

namespace FF.WindowsERPClient.HP
{
    public partial class HpCollection_ECDReq : Base
    {
        //Created by shani : this form is used for HpCollection
       

        private HpCollection parent;
        private AccountSettlement parent1;
        private HpRevert parent_Revert;
        private HpExchange parent_exchange;
        private HpReceiptReversal parent_HpReceiptReversal;
        private AccountReschedule account_reschedule;

        private FF.WindowsERPClient.HP.HPAdjustment parent_Adjustment;

        public HpCollection Parent1
        {
            get { return parent; }
            set { parent = value; }
        }
        public AccountSettlement Parent2
        {
            get { return parent1; }
            set { parent1 = value; }
        }
        public HpRevert Parent_Revert
        {
            get { return parent_Revert; }
            set { parent_Revert = value; }
        }

        public HpExchange Parent_Exchange {
            get { return parent_exchange; }
            set { parent_exchange = value; }
        
        }
       
        public FF.WindowsERPClient.HP.HPAdjustment Parent_Adjustment
        {
            get { return parent_Adjustment; }
            set { parent_Adjustment = value; }
        }
        public HpReceiptReversal Parent_HpReceiptReversal
        {
            get { return parent_HpReceiptReversal; }
            set { parent_HpReceiptReversal = value; }

        }
        public AccountReschedule Parent_AccountReschedule {
            get { return account_reschedule; }
            set { account_reschedule = value; }
        }

        ///////////////////////////////////////       
        public HpCollection_ECDReq()
        {
            InitializeComponent();

        }
        public HpCollection_ECDReq(HpCollection parent)
        {
            InitializeComponent();
            grvMpdalPopUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Parent1 = parent;
        }
        public HpCollection_ECDReq(AccountSettlement parent)
        {
            InitializeComponent();
            grvMpdalPopUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Parent2 = parent;
        }
        public HpCollection_ECDReq(HpRevert parent)
        {
            InitializeComponent();
            grvMpdalPopUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Parent_Revert = parent;
        }
        public HpCollection_ECDReq(HpExchange parent)
        {
            InitializeComponent();
            grvMpdalPopUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Parent_Exchange = parent;
        }

        public HpCollection_ECDReq(FF.WindowsERPClient.HP.HPAdjustment parent)
        {
            InitializeComponent();
            grvMpdalPopUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Parent_Adjustment = parent;
        }
        public HpCollection_ECDReq(HpReceiptReversal parent)
        {
            InitializeComponent();
            grvMpdalPopUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            parent_HpReceiptReversal = parent;
        }
        public HpCollection_ECDReq(AccountReschedule parent)
        {
            InitializeComponent();
            grvMpdalPopUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Parent_AccountReschedule = parent;
        }
        ///////////////////////////////////////       
        public void visible_panel_accountSelect(bool visibility)
        {
            panel_accountSelect.Visible = visibility;
        }
        public void visible_panel_ReqApp(bool visibility)
        {
            panel_ReqApp.Visible = visibility;
        }

        public void fill_AccountGrid(List<HpAccount> accList)
        {
            grvMpdalPopUp.AutoGenerateColumns = false;
            grvMpdalPopUp.DataSource = accList;
            
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (uc_ddlReqNo.SelectedValue == null)
            {
                Parent1.ApprReqNo = "";// "Yes";
            }
            else
            {
                Parent1.ApprReqNo = uc_ddlReqNo.SelectedValue.ToString();// "Yes";
            }
           
            
            this.Close();
        }             

        private void uc_ddlReqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SelectedReqNum
            //uc_lblSelectedReqNum.Text = uc_ddlReqNo.SelectedValue.ToString();
            //SelectedReqNum = uc_ddlReqNo.SelectedValue.ToString();
        }

        private void grvMpdalPopUp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow dr = grvMpdalPopUp.SelectedRows[0];           
            // or simply use column name instead of index
            //dr.Cells["id"].Value.ToString();
            if(Parent1!=null)
            Parent1.AccountNo = dr.Cells["col_ACCNo"].Value.ToString();
            else if(Parent2!=null)
                Parent2.AccountNo = dr.Cells["col_ACCNo"].Value.ToString();
            else if (Parent_Revert!=null)
            {                
                Parent_Revert.AccountNo = dr.Cells["col_ACCNo"].Value.ToString();
                Parent_Revert.LoadAccountDetail(Parent_Revert.AccountNo, CHNLSVC.Security.GetServerDateTime().Date);
            }
               
            else if(Parent_Exchange!=null)
                Parent_Exchange.AccountNo = dr.Cells["col_ACCNo"].Value.ToString();

            else if (Parent_Adjustment != null)
            {
                Parent_Adjustment.AccountNo = dr.Cells["col_ACCNo"].Value.ToString();
            }
            else if (Parent_AccountReschedule != null) {
                Parent_AccountReschedule.AccountNo = dr.Cells["col_ACCNo"].Value.ToString();
            }
            else if (Parent_HpReceiptReversal != null)
            {
                Parent_HpReceiptReversal.AccountNo = dr.Cells["col_ACCNo"].Value.ToString();
                Parent_HpReceiptReversal.LoadAccountDetail(Parent_HpReceiptReversal.AccountNo, CHNLSVC.Security.GetServerDateTime().Date);
            }
            this.Close();

        }
        public void LoadUserControl(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            //appType: eg. Account number
           // uc_lblSelectedReqNum.Text = "";
            bindReqNums(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
            bindDetaisToGrid(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
        }

        protected void bindReqNums(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        {
            ApproveRequestUC APPROVE = new ApproveRequestUC();
            List<string> reqNumList = new List<string>();

            reqNumList.Add("none");
            reqNumList = APPROVE.getApprovedReqNumbersList(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
            reqNumList.Add("none");
            uc_ddlReqNo.DataSource = reqNumList;
            //uc_ddlReqNo.DataBind();
            //uc_ddlReqNo.SelectedValue = "none";
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
            if (bindTable != null)
            {
                grv_ViewReqDet.AutoGenerateColumns = false;
                grv_ViewReqDet.DataSource = bindTable;
               // grv_ViewReqDet.DataBind();
            }

        }

    }
}
