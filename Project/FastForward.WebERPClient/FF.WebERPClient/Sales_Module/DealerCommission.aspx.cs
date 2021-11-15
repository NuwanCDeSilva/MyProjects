using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;

namespace FF.WebERPClient.Sales_Module
{
    public partial class DealerCommission : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmpty();
            }
        }

        protected void ImageButtonAddPC_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearchLoyaltyType.Company.ToUpper();
            string chanel = uc_ProfitCenterSearchLoyaltyType.Channel.ToUpper();
            string subChanel = uc_ProfitCenterSearchLoyaltyType.SubChannel.ToUpper();
            string area = uc_ProfitCenterSearchLoyaltyType.Area.ToUpper();
            string region = uc_ProfitCenterSearchLoyaltyType.Region.ToUpper();
            string zone = uc_ProfitCenterSearchLoyaltyType.Zone.ToUpper();
            string pc = uc_ProfitCenterSearchLoyaltyType.ProfitCenter.ToUpper();

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            GridViewPC.DataSource = dt;
            GridViewPC.DataBind();
        }

        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonNone_Click(object sender, EventArgs e)
        {
            GridViewPC.DataSource = null;
            GridViewPC.DataBind();
        }

        protected void ButtonClearPc_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }

        private void LoadEmpty()
        {
            DataTable dt = CHNLSVC.Sales.GetDelaerCommissionDetails(GlbUserComCode, GlbUserDefProf, DateTime.MaxValue, DateTime.MaxValue);
            GridViewItemDetails.DataSource = dt;
            GridViewItemDetails.DataBind();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private DataTable Search()
        {
            #region validation

            if (TextBoxFromDate.Text == "") {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select from date");
                return null;
            }
            if (TextBoxToDate.Text == "") {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select to date");
                return null;
            }


            DateTime _date;
            if (!DateTime.TryParse(TextBoxFromDate.Text, out _date))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from date correctly");
                return null;
            }
            if (!DateTime.TryParse(TextBoxToDate.Text, out _date))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to date correctly");
                return null;
            }
            if (GridViewPC.Rows.Count <= 0) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select profit centers");
                return null;
            }

            #endregion

            DataTable _dt = new DataTable();
            foreach (GridViewRow gr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gr.Cells[1].FindControl("chekPc");
                if (chkSelect.Checked)
                {
                    DataTable _tem = CHNLSVC.Sales.GetDelaerCommissionDetails(uc_ProfitCenterSearchLoyaltyType.Company.ToUpper(), gr.Cells[1].Text, Convert.ToDateTime(TextBoxFromDate.Text), Convert.ToDateTime(TextBoxToDate.Text));
                    _dt.Merge(_tem);
                }
            }
            GridViewItemDetails.DataSource = _dt;
            GridViewItemDetails.DataBind();

            return _dt;
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Sales_Module/DealerCommission.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void GridViewItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewItemDetails.PageIndex = e.NewPageIndex;

            //load
            ButtonSearch_Click(null, null);
        }

        protected void ButtonApply_Click(object sender, EventArgs e)
        {
           
            try
            {
                List<InvoiceItem> _itmList = new List<InvoiceItem>();
                DataTable _dt = Search();
                if (_dt != null)
                {
                    foreach (DataRow dr in _dt.Rows)
                    {
                        InvoiceItem _ii = new InvoiceItem();
                        _ii.Sad_itm_cd = dr["SAD_ITM_CD"].ToString();
                        _ii.Mi_cd = dr["SAH_PC"].ToString();
                        _ii.Sad_inv_no = dr["SAH_INV_NO"].ToString();
                        _ii.Sad_pbook = dr["SAD_PBOOK"].ToString();
                        _ii.Sad_pb_lvl = dr["SAD_PB_LVL"].ToString();
                        _ii.Mi_cre_dt =Convert.ToDateTime( dr["SAH_DT"]);
                        _ii.Mi_itm_tp = dr["SAH_INV_TP"].ToString();
                        _ii.Sad_qty =Convert.ToDecimal( dr["SAD_QTY"]);
                        _ii.Sad_unit_amt = Convert.ToDecimal(dr["SAD_UNIT_AMT"]);
                        _itmList.Add(_ii);
                    }
                }

                int result = CHNLSVC.Sales.UpdateItemCommission(_itmList);
                if (result > 0)
                {
                    string Msg = "<script>alert('Record updated Sucessfully!!');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    string Msg = "<script>alert('Nothing Updated!!');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                ButtonSearch_Click(null, null);
            }
            catch (Exception) {
                string Msg = "<script>alert('Error occured!!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }
    }
}