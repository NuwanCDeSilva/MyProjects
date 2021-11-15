using FF.AbansTours.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours
{
    public partial class DriverInquiry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PageClear();
                }
                
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        protected void imgbtnDriverEPFNo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DriverTBS);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchDriverTBS(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtDriver.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDriver.Focus();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }
        protected void imgbtnCustomerCode_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetCustomerCommon(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtCustomer.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtCustomer.Focus();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }
        protected void imgbtnVehicleNo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Vehicles);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchVehicle(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtVehicleNo.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtVehicleNo.Focus();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        } 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date;
                DateTime? fromDate = null;
                DateTime? toDate = null;
                if (DateTime.TryParse(txtFromDate.Text, out date))
                {
                   fromDate = Convert.ToDateTime(txtFromDate.Text);
                }
                if (DateTime.TryParse(txtToDate.Text, out date))
                {
                    toDate = Convert.ToDateTime(txtToDate.Text);
                }
                
                DataTable driverInquiry = CHNLSVC.Tours.Get_gen_cust_enq(base.GlbUserComCode, base.GlbUserDefProf, "TNSPT", txtVehicleNo.Text, txtDriver.Text, txtCustomer.Text, fromDate, toDate);
                Session["driverInquiry"] = driverInquiry;
                if (driverInquiry.Rows.Count > 0)
                {
                    grdDriverInquiry.DataSource = driverInquiry;
                    grdDriverInquiry.DataBind();
                }
                else 
                {
                    PageClear();
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Search details not found.');", true);
                }
                                       
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        protected void grdDriverInquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDriverInquiry.DataSource = Session["driverInquiry"];
            grdDriverInquiry.DataBind();
            grdDriverInquiry.PageIndex = e.NewPageIndex;
            grdDriverInquiry.DataBind();
        }
        

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.DriverTBS:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Vehicles:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                PageClear();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }
        private void PageClear()
        {
            txtVehicleNo.Text = string.Empty;
            txtDriver.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = String.Empty;
            grdDriverInquiry.DataSource = new int[] { };
            grdDriverInquiry.DataBind();

            Session["driverInquiry"] = null;

            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        }    

    }
}