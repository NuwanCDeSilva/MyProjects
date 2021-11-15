using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using CrystalDecisions.Web;

namespace FF.WebERPClient.HP_Module
{
    public partial class CustomerAcknowledgement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            LoadGrid(GridViewDetails,GlbUserComCode,GlbUserDefProf,DateTime.Now,DateTime.Now);
        }

        private void LoadGrid(GridView GridViewDetails,string _com,string _pc,DateTime _from,DateTime _to)
        {
            GridViewDetails.DataSource = CHNLSVC.Sales.GetCustomerAcknowledgment(_com, _pc, _from, _to);
            GridViewDetails.DataBind();
        }

        protected void imgbtnSearchLocation_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxLocation.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void LinkButtonTemp_Click(object sender, EventArgs e)
        {
            DateTime _from=DateTime.Now;
            DateTime _to=DateTime.MinValue;
            if(DateTime.TryParse(TextBoxCreationDateFrom.Text,out _from))
                _from=Convert.ToDateTime(TextBoxCreationDateFrom.Text);
            if(DateTime.TryParse(TextBoxCreationDateTo.Text, out _to))
                _to=Convert.ToDateTime(TextBoxCreationDateTo.Text);

            LoadGrid(GridViewDetails,GlbUserComCode,TextBoxLocation.Text.ToUpper() ,_from,_to);
        }

        //protected void GridViewDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    if (e.NewPageIndex != -1)
        //    {
        //        LinkButtonTemp_Click(null, null);
        //        GridViewDetails.PageIndex = e.NewPageIndex;
        //        GridViewDetails.DataBind();
        //    }
        //}

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (RadioButtonListType.SelectedIndex == -1) {
                return;
            }

            string type = "";
            if (RadioButtonListType.SelectedIndex == 0) {
                type = "C";
            }
            else if (RadioButtonListType.SelectedIndex == 1) {
                type = "G1";
            }
            else if (RadioButtonListType.SelectedIndex == 2) {
                type = "G2";
            }

            string accNoList = "";
            foreach (GridViewRow gr in GridViewDetails.Rows)
            {
                CheckBox ck = (CheckBox)gr.FindControl("chkSelect");
                if (ck.Checked) {
                    string accNo = ((Label)gr.FindControl("lblAcc")).Text;
                    accNoList = accNoList+"'" + accNo + "'"+" , ";
                    int result = CHNLSVC.Sales.UpdateHPPrintStus(GlbUserComCode, GlbUserDefProf, 1, accNo);
                }
            }
            //user not selected any accno
            if (accNoList != "")
            {
                accNoList = accNoList.Substring(0, accNoList.Length - 3);

                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\CustomerAcknowledgment.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/CustomerAcknowledgment.rpt";
                GlbSelectionFormula = "{HPT_ACC.HPA_ACC_NO} in [" + accNoList + "]";
                HPCustType = type;
                string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/CustomerAcknoledgmentPrint.aspx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
            }

        }

        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            //LinkButtonTemp_Click(null, null);
            foreach (GridViewRow gr in GridViewDetails.Rows) {
                CheckBox ck = (CheckBox)gr.FindControl("chkSelect");
                ck.Checked = true;
            }
        }

        protected void ButtonNone_Click(object sender, EventArgs e)
        {
           // LinkButtonTemp_Click(null, null);
            foreach (GridViewRow gr in GridViewDetails.Rows)
            {
                CheckBox ck = (CheckBox)gr.FindControl("chkSelect");
                ck.Checked = false;
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HP_Module/CustomerAcknowledgement.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}