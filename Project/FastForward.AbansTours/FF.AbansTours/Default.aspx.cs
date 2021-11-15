using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.AbansTours.UserControls;
using System.Data;
using System.Text;

namespace FF.AbansTours
{
    public partial class _Default : BasePage
    {
        private List<QUO_COST_DET> oMainItems = null;
        private BasePage _basePage;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])))
                {
                    if (!IsPostBack)
                    {
                        loadData();
                    }
                }
                else
                {
                    string gotoURL = "login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void dgvHistry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewEnquiry")
                {
                    GridViewRow row = dgvHistry.Rows[Convert.ToInt32(e.CommandArgument)];
                    Label lblEnquiry = (Label)row.FindControl("lblEnquiry");
                    txtEnquiry.Text = lblEnquiry.Text;
                    mpEnquiry.Show();
                }
                else if (e.CommandName == "Costing")
                {
                    GridViewRow row = dgvHistry.Rows[Convert.ToInt32(e.CommandArgument)];
                    Label lblCustCode = (Label)row.FindControl("lblCustCode");
                    Label lblEnquiryID = (Label)row.FindControl("lblEnquiryID");
                    Label lblstatus = (Label)row.FindControl("lblstatus");
                    Label Type = (Label)row.FindControl("met_desc");
                    if (lblstatus.Text == "Cancelled")
                    {
                        DisplayMessages("Enquiry is Cancelled");
                        return;
                    }
                    Session["CustCode"] = lblCustCode.Text;
                    Session["EnquiryID"] = lblEnquiryID.Text;
                    Session["RedirectPage"] = "Default.aspx";
                    if (Type.Text == "Transport")
                    {
                        Response.Redirect("~/Sales/VehicleAllocation.aspx", false);
                    }
                    else
                    {
                       // Session["newCustomer"] = lblCustCode.Text;
                        Response.Redirect("CostingSheet.aspx" + "?htenus=Default");
                       
                    }
                   
                }
                else if (e.CommandName == "Invoice")
                {
                    GridViewRow row = dgvHistry.Rows[Convert.ToInt32(e.CommandArgument)];
                    Label lblEnquiryID = (Label)row.FindControl("lblEnquiryID");
                    Label lblstatus = (Label)row.FindControl("lblstatus");
                    if (lblstatus.Text == "Cancelled")
                    {
                        DisplayMessages("Enquiry is Cancelled");
                        return;
                    }
                    Session["EnquiryID"] = lblEnquiryID.Text;
                    
                    Session["RedirectPage"] = "Default.aspx";
                    Response.Redirect("Invoice.aspx" + "?htenus=Default");
                }
                else
                {
                    GridViewRow row = dgvHistry.Rows[Convert.ToInt32(e.CommandArgument)];
                    Label lblEnquiryID = (Label)row.FindControl("lblEnquiryID");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mpEnquiry.Hide();
        }

        private void loadData()
        {
            string Status = "0,1,2,3,4,5,6,7,8";

            List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Status, Session["UserID"].ToString(), 15001);
            if (oItems.Count > 0)
            {
                dgvHistry.DataSource = oItems;
                Session["Data"] = oItems;
                dgvHistry.DataBind();
                modifyGrid();
                ViewState["oItems"] = oItems;
            }
            else
            {
                divColors.Visible = false;
            }
        }

        private void modifyGrid()
        {
            if (dgvHistry.Rows.Count > 0)
            {
                for (int i = 0; i < dgvHistry.Rows.Count; i++)
                {
                    GridViewRow row = dgvHistry.Rows[i];
                    Label IsLateToNextStage = (Label)row.FindControl("IsLateToNextStage");
                    if (IsLateToNextStage.Text.ToUpper() == "TRUE")
                    {
                        row.BackColor = System.Drawing.Color.Lavender;
                    }
                    Label lblDate = (Label)row.FindControl("lblDate");
                    Label lblExpectedDate = (Label)row.FindControl("lblExpectedDate");
                    lblDate.Text = Convert.ToDateTime(lblDate.Text).Date.ToString("dd/MMM/yyyy");
                    lblExpectedDate.Text = Convert.ToDateTime(lblExpectedDate.Text).Date.ToString("dd/MMM/yyyy");
                }
            }
        }

        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFilter.Text))
            {
                string Status = "0,1,2,3,4,5,6,7,8";

                List<GEN_CUST_ENQ> oItems = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Status, Session["UserID"].ToString(), 15001);
                if (oItems.Count > 0)
                {
                    List<GEN_CUST_ENQ> oItems2 = new List<GEN_CUST_ENQ>();
                    if (ddlFilter.Text == "Customer Code")
                    {
                        oItems2 = oItems.FindAll(x => x.GCE_CUS_CD.ToUpper().Contains(txtFilter.Text.ToUpper().Trim()));
                    }
                    else if (ddlFilter.Text == "Enquiry ID")
                    {
                        oItems2 = oItems.FindAll(x => x.GCE_ENQ_ID.ToUpper().Contains(txtFilter.Text.ToUpper().Trim()));
                    }
                    else if (ddlFilter.Text == "Reference")
                    {
                        oItems2 = oItems.FindAll(x => x.GCE_REF.ToUpper().Contains(txtFilter.Text.ToUpper().Trim()));
                    }
                    else if (ddlFilter.Text == "PC")
                    {
                        oItems2 = oItems.FindAll(x => x.GCE_ENQ_PC_DESC.ToUpper().Contains(txtFilter.Text.ToUpper().Trim()));
                    }
                    else if (ddlFilter.Text == "Name")
                    {
                        oItems2 = oItems.FindAll(x => x.GCE_NAME.ToUpper().Contains(txtFilter.Text.ToUpper().Trim()));
                    }
                    else if (ddlFilter.Text == "Mobile")
                    {
                        oItems2 = oItems.FindAll(x => x.GCE_MOB.ToUpper().Contains(txtFilter.Text.ToUpper().Trim()));
                    }
                    else if (ddlFilter.Text == "Status")
                    {
                        oItems2 = oItems.FindAll(x => x.MES_DESC.ToUpper().Contains(txtFilter.Text.ToUpper().Trim()));
                    }
                    dgvHistry.DataSource = oItems2;
                    Session["Data"] = oItems2;
                    dgvHistry.DataBind();
                    modifyGrid();
                }
                else
                {
                    divColors.Visible = false;
                }
            }
            else
            {
                loadData();
            }
        }

        protected void dgvHistry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvHistry.PageIndex = e.NewPageIndex;
                dgvHistry.DataSource = null;
                dgvHistry.DataSource = (List<GEN_CUST_ENQ>)Session["Data"];
                dgvHistry.DataBind();
                modifyGrid();
            }
            catch (Exception ex)
            {

            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void btnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetCustomerCommon(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtFilter.ClientID;
            ucc.UCModalPopupExtender.Show();
        }
        protected void btnEnquiryID_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_ENQUIRY(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtFilter.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtFilter.Focus();
        }
        protected void lblPC_Click(object sender, EventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtFilter.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtFilter.Focus();
        }
        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFilter.SelectedValue == "C") 
            {
                lblvalue.Text = "Customer Code";
                btnCustomer.Visible = true;
                btnPc.Visible = false;
                btnEnquiryID.Visible = false;
            }
            else if (ddlFilter.SelectedValue == "E")
            {
                lblvalue.Text = "System ID";

                btnCustomer.Visible = false;
                btnPc.Visible = false;
                btnEnquiryID.Visible = true;
            }
            else if (ddlFilter.SelectedValue == "R")
            {
                lblvalue.Text = "Reference";

                btnCustomer.Visible = false;
                btnPc.Visible = false;
                btnEnquiryID.Visible = false;
            }
            else if (ddlFilter.SelectedValue == "P")
            {
                lblvalue.Text = "Profit Center";
                btnCustomer.Visible = false;
                btnPc.Visible = true;
                btnEnquiryID.Visible = false;
            }
            else if (ddlFilter.SelectedValue == "S")
            {
                lblvalue.Text = "Status";
                btnCustomer.Visible = false;
                btnPc.Visible = false;
                btnEnquiryID.Visible = false;
            }
        }

        protected void dgvHistry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvHistry, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dgvHistry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string EnquiryID = (dgvHistry.SelectedRow.FindControl("gce_mainreqid") as Label).Text;
            List<GEN_CUST_ENQ> _enqList = ViewState["oItems"] as List<GEN_CUST_ENQ>;

            if (_enqList.Count > 0)
            {
                if (EnquiryID != "")
                {
                    var Filter = _enqList.Where(x => x.Gce_mainreqid == EnquiryID).ToList();
                    grdstatus.DataSource = Filter;
                    grdstatus.DataBind();
                }
                else
                {
                    grdstatus.DataSource = new int[] { };
                    grdstatus.DataBind();

                }
                
            }
        }

       
    }
}