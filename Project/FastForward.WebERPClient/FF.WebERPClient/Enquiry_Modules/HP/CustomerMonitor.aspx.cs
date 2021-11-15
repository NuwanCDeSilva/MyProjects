using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text;
using System.Data;

namespace FF.WebERPClient.Enquiry_Modules.HP
{
    public partial class CustomerMonitor : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
 

            }
        }

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator);
                        break;
                    }
                 default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void AccountSearch(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtAccount.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnInvNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvoice.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgrecSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Receipt);
            DataTable dataSource = CHNLSVC.CommonSearch.GetReceipts(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtReceipt.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }



        #endregion



        public List<HpAccount> AccountsList
        {
            get { return (List<HpAccount>)ViewState["AccountsList"]; }
            set { ViewState["AccountsList"] = value; }
        }
        protected void GetHpPayment(object sender, EventArgs e)
        {
     
            HpAccount account = new HpAccount();
            if (hdnDocumentType.Value.Trim() == "HS")
            {
                string[] _accseq = hdnDocumentNo.Value.Split('-');
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, GlbUserDefProf,  Convert.ToString(Convert.ToInt32(_accseq[1])), string.Empty);
                AccountsList = accList;//save in veiw state


                if (AccountsList == null)
                {
                    uc_HpAccountSummary1.Clear();
                    return;
                }

                foreach (HpAccount acc in AccountsList)
                {
                    if (hdnDocumentNo.Value == acc.Hpa_acc_no)
                    {
                        account = acc;
                    }
                }
                uc_HpAccountSummary1.set_all_values(account, GlbUserDefProf, DateTime.Now.Date, GlbUserDefProf);
               
            }
            else
            {
                uc_HpAccountSummary1 = new UserControls.uc_HpAccountSummary();
            }

          string  Msg = "return false;";
          ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "InvoiceMsg", Msg, true);
        }

        protected void btnPaymentClick(object sender, EventArgs e)
        {
         
            divPayment.Visible=true;
            divProduct.Visible=false;
            divSchedule.Visible=false;
            divTransfers.Visible=false;
            divDiriya.Visible=false;
            divCredit.Visible=false;
            divBlackList.Visible = false;
        }
        protected void btnProductClick(object sender, EventArgs e)
        {
         
            divPayment.Visible = false;
            divProduct.Visible = true;
            divSchedule.Visible = false;
            divTransfers.Visible = false;
            divDiriya.Visible = false;
            divCredit.Visible = false;
            divBlackList.Visible = false;
        }
        protected void btnScheduleClick(object sender, EventArgs e)
        {
        
            divPayment.Visible = false;
            divProduct.Visible = false;
            divSchedule.Visible = true;
            divTransfers.Visible = false;
            divDiriya.Visible = false;
            divCredit.Visible = false;
            divBlackList.Visible = false;
        }
        protected void btnTransfersClick(object sender, EventArgs e)
        {
            divPayment.Visible = false;
            divProduct.Visible = false;
            divSchedule.Visible = false;
            divTransfers.Visible = true;
            divDiriya.Visible = false;
            divCredit.Visible = false;
            divBlackList.Visible = false;
        }
        protected void btnDiriyaClick(object sender, EventArgs e)
        {
            divPayment.Visible = false;
            divProduct.Visible = false;
            divSchedule.Visible = false;
            divTransfers.Visible = false;
            divDiriya.Visible = true;
            divCredit.Visible = false;
            divBlackList.Visible = false;
        }
        protected void btnCreditClick(object sender, EventArgs e)
        { 
            divPayment.Visible = false;
            divProduct.Visible = false;
            divSchedule.Visible = false;
            divTransfers.Visible = false;
            divDiriya.Visible = false;
            divCredit.Visible = true;
            divBlackList.Visible = false;
        }
        protected void btnBlackListClick(object sender, EventArgs e)
        { 
            divPayment.Visible = false;
            divProduct.Visible = false;
            divSchedule.Visible = false;
            divTransfers.Visible = false;
            divDiriya.Visible = false;
            divCredit.Visible = false;
            divBlackList.Visible = true;
        }


        protected void Close(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        protected void Clear(object sender, EventArgs e)
        {
            Response.Redirect("~/Enquiry_Modules/HP/CustomerMonitor.aspx", false);
        }
    }
}