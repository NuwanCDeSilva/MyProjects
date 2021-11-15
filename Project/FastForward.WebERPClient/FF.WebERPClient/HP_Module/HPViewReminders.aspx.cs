using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;

namespace FF.WebERPClient.HP_Module
{
    public partial class HPViewReminders : BasePage
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Request.QueryString["id"] != null)
                {
                    string location = GlbUserDefProf;
                    string acc_seq = Request.QueryString["id"].ToString();
                    TextBoxAccNo.Text = acc_seq;
                    List<HpAccount> accList = new List<HpAccount>();
                    accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
                    if (accList != null)
                    {
                        LoadGrid(accList[0].Hpa_acc_no, DropDownListReminderStatus.SelectedValue, GlbUserComCode, GlbUserDefProf);
                    }
                    else
                    {
                        LoadGrid("%", DropDownListReminderStatus.SelectedValue, GlbUserComCode, GlbUserDefProf);
                    }
                }
                else {
                    LoadGrid("%", DropDownListReminderStatus.SelectedValue, GlbUserComCode, GlbUserDefProf);
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



        private void LoadGrid(string _ref, string _stus, string _com, string _pc)
        {

            List<HPReminder> rmdList = CHNLSVC.Sales.GetReminders(_ref, _stus, _com, _pc, "No_Date",DateTime.MinValue);
            if (rmdList != null)
            {
                GridViewDetails.DataSource = rmdList;
                GridViewDetails.DataBind();
            }
            else {
                DataTable dt = new DataTable();
                GridViewDetails.DataSource = dt;
                GridViewDetails.DataBind();
            }
        }


        protected void ButtonInactive_Click(object sender, EventArgs e)
        {
            string location = GlbUserDefProf;
            string acc_seq = TextBoxAccNo.Text;
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
            if (accList != null)
            {
                if (accList.Count == 1)
                {
                    LoadGrid(accList[0].Hpa_acc_no, DropDownListReminderStatus.SelectedValue, GlbUserComCode, GlbUserDefProf);
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
                LoadGrid("%", DropDownListReminderStatus.SelectedValue, GlbUserComCode, GlbUserDefProf);
            }
        }

        protected void ButtonInactive_Click1(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewDetails.Rows) {
                CheckBox chk = (CheckBox)gr.FindControl("chekPc");
                if (chk.Checked) {
                    HPReminder _rmd = new HPReminder();
                    _rmd.Hra_seq = Convert.ToInt32(GridViewDetails.DataKeys[gr.RowIndex][0].ToString());
                    _rmd.Hra_stus = "I";
                    _rmd.Hra_stus_dt = DateTime.Now;
                    _rmd.Hra_mod_by = GlbUserName;

                    //update process
                    int result = CHNLSVC.Sales.UpdateHPReminder(_rmd);
                    if (result > 0)
                    {
                        string Msg = "<script>alert('Record Updated Sucessfully!!');window.location = 'HPViewReminders.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                }
            }
        }

        protected void GridViewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "A") {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                }
                if (e.Row.Cells[4].Text == "I")
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[4].Text == "C")
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Blue;
                }
            }
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
            LoadGrid(acc.Hpa_acc_no, DropDownListReminderStatus.SelectedValue, GlbUserComCode, GlbUserDefProf);
        }
    }
}