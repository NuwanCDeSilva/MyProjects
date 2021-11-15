using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using FF.BusinessObjects;
using System.Data;

namespace FF.WebERPClient.HP_Module
{
    public partial class HPReminders : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                TextBoxRemindDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                TextBoxDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                LoadGrid(TextBoxAccNo.Text, "%", GlbUserComCode, GlbUserDefProf);
            }
        }

        private void LoadGrid(string _ref, string _stus, string _com, string _pc)
        {

            List<HPReminder> rmdList = CHNLSVC.Sales.GetReminders(_ref, _stus, _com, _pc, "No_Date", DateTime.MinValue);
            if (rmdList != null)
            {
                GridViewDetails.DataSource = rmdList;
                GridViewDetails.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                GridViewDetails.DataSource = dt;
                GridViewDetails.DataBind();
            }
        }

        protected void LinkButtonTemp_Click(object sender, EventArgs e)
        {
            if (TextBoxAccNo.Text != "" )
            {
                string location = GlbUserDefProf;
                string acc_seq = TextBoxAccNo.Text.Trim();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
                if (accList != null)
                {
                    if (accList.Count == 1)
                    {
                        uc_HpAccountSummary1.set_all_values(accList[0], GlbUserDefLoca, DateTime.Now.Date, GlbUserDefLoca);
                        LoadGrid(accList[0].Hpa_acc_no, "%", GlbUserComCode, GlbUserDefProf);
                    }
                    else {
                        //show a pop up to select the account number
                        grvMpdalPopUp.DataSource = accList;
                        grvMpdalPopUp.DataBind();
                        ModalPopupExtItem.Show();
                    }
                }
                else
                {
                    uc_HpAccountSummary1.Clear();
                }
            }
        }

        protected void ImgBtnAcc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxAccNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            #region validation

            string location = GlbUserDefProf;
            string acc_seq = TextBoxAccNo.Text.Trim();
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
            if (TextBoxCustMob.Text != "" && TextBoxCustMob.Text.Length < 10) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid customer mobile no");
                return;
            }
            if (TextBoxManagerMob.Text != "" && TextBoxManagerMob.Text.Length < 10)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid manager mobile no");
                return;
            }
            if (TextBoxAccNo.Text != "" && TextBoxManagerMob.Text=="" && TextBoxCustMob.Text=="")
            {
                if (accList == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid account no");
                    return;
                }
            }
            if (TextBoxAccNo.Text == "" && TextBoxCustMob.Text=="" && TextBoxManagerMob.Text=="")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter account no, customer mobile or manager mobile");
                return;
            }

            #endregion

            HPReminder _rmd = new HPReminder();

            _rmd.Hra_ref = accList[0].Hpa_acc_no;
            _rmd.Hra_tp = "ACC";
            _rmd.Hra_stus = "A";
            _rmd.Hra_stus_dt = DateTime.Now;
            _rmd.Hra_cre_by = GlbUserName;
            _rmd.Hra_cre_dt = DateTime.Now;
            _rmd.Hra_mod_by = "";
            _rmd.Hra_dt = Convert.ToDateTime(TextBoxRemindDate.Text);
            _rmd.Hra_rmd = TextBoxMessage.Text;
            _rmd.Hra_cust_mob = TextBoxCustMob.Text;
            _rmd.Hra_mgr_mob = TextBoxManagerMob.Text;
            _rmd.Hra_pc = GlbUserDefProf;
            _rmd.Hra_com = GlbUserComCode;
            
            int result = CHNLSVC.Sales.SaveHPReminder(_rmd);
            if (result > 0) {
                string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'HPReminders.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HP_Module/HPReminders.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            HpAccount acc = CHNLSVC.Sales.GetHP_Account_onAccNo(accountNo);
            uc_HpAccountSummary1.set_all_values(acc, GlbUserDefLoca, DateTime.Now.Date, GlbUserDefLoca);
            LoadGrid(acc.Hpa_acc_no, "%", GlbUserComCode, GlbUserDefProf);
        }

        protected void GridViewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "A")
                {
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                }
                if (e.Row.Cells[3].Text == "I")
                {
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[3].Text == "C")
                {
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;
                }
            }
        }
    }
}