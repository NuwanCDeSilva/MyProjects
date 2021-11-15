using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class Cusdec_Entry_Request : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageClear();
            }
            else
            {
                txtFrom.Text = Request[txtFrom.UniqueID];
                txtto.Text = Request[txtto.UniqueID];

                txtgoodgivenDate.Text = Request[txtgoodgivenDate.UniqueID];
            }
        }
        List<MasterSubType> _MasterSubType = new List<MasterSubType>();
        List<InventoryRequestItem> _RequestItem = new List<InventoryRequestItem>();
        List<ImportsBLItems> oImportsBLItems = new List<ImportsBLItems>();
        MasterItem _itemdetail = new MasterItem();
        protected List<MasterItemStatus> oMasterItemStatuss { get { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } set { Session["oMasterItemStatuss"] = value; } }

        private void pageClear()
        {

            oMasterItemStatuss = new List<MasterItemStatus>();
            loadItemStatus();
            Session["GRN"] = "";
            Session["Reqno"] = "";
            Session["type"] = "";
            Session["usetobond"] = false;
            ViewState["_RequestItem"] = null;
            ViewState["oImportsBLItems"] = null;
            Session["item"] = "";
            lblStatus.Text = string.Empty;
            lbtnDeleteItem.Visible = true;
            txtRequest.Text = string.Empty;
            txtremark.Text = string.Empty;
            txtTobond.Text = string.Empty;
            DateTime orddate = DateTime.Now;
            txtgoodgivenDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtDate.Text = orddate.ToString("dd/MMM/yyyy");
            lblCustomerName.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            // txtplocation.Text = Session["UserDefProf"].ToString();
            txttransfertype.Text = string.Empty;
            txtref.Text = string.Empty;
            ddlRequestType.Items.Clear();

            oImportsBLItems = new List<ImportsBLItems>();
            _MasterSubType = new List<MasterSubType>();
            _RequestItem = new List<InventoryRequestItem>();
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16025))
            {
                pnlToBod.Visible = false;
                pnltoboneName.Enabled = false;
                pnltobonetxtbox.Enabled = false;
                pnlPermision.Enabled = false;
                HiddenFieldRequesttype.Value = "CUSR";
                GetRequesttype();
            }
            else
            {
                pnlToBod.Visible = true;
                pnltoboneName.Enabled = true;
                pnltobonetxtbox.Enabled = true;
                pnlPermision.Enabled = true;
                HiddenFieldRequesttype.Value = "CUSA";
                GetRequesttype();
            }
            grdItem.DataSource = new int[] { };
            grdItem.DataBind();
            grdrequest.DataSource = new int[] { };
            grdrequest.DataBind();
            grdrequestItem.DataSource = new int[] { };
            grdrequestItem.DataBind();
            txtItem.Text = string.Empty;
            txtqty.Text = string.Empty;
            txtRes.Text = string.Empty;
            txtTobond_Bl.Text = string.Empty;
            txtFrom.Text = orddate.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtto.Text = orddate.ToString("dd/MMM/yyyy");
            txtSearchCustomer.Text = string.Empty;
            txtSearchprofitcenter.Text = string.Empty;

            rdotobound.Checked = true;
            radpendingTobond.Checked = true;

            lbtnCancel.Enabled = false;
            lbtnCancel.OnClientClick = "return Enable();";
            lbtnCancel.CssClass = "buttoncolorleft";

            Session["BL"] = "";

            //added by kelum : 2016-June-15

            txtFDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
        }
        private void saveClear()
        {
            Session["BL"] = "";
            Session["item"] = "";
            lblStatus.Text = string.Empty;
            lbtnDeleteItem.Visible = true;
            txtRequest.Text = string.Empty;
            txtremark.Text = string.Empty;
            txtTobond.Text = string.Empty;
            DateTime orddate = DateTime.Now;
            txtgoodgivenDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtDate.Text = orddate.ToString("dd/MMM/yyyy");
            // txtplocation.Text = Session["UserDefProf"].ToString();
            txttransfertype.Text = string.Empty;
            txtref.Text = string.Empty;

            oImportsBLItems = new List<ImportsBLItems>();
            _MasterSubType = new List<MasterSubType>();
            _RequestItem = new List<InventoryRequestItem>();

            grdItem.DataSource = new int[] { };
            grdItem.DataBind();
            grdrequest.DataSource = new int[] { };
            grdrequest.DataBind();
            grdrequestItem.DataSource = new int[] { };
            grdrequestItem.DataBind();
            txtItem.Text = string.Empty;
            txtqty.Text = string.Empty;
            txtRes.Text = string.Empty;
            txtTobond_Bl.Text = string.Empty;
            txtFrom.Text = orddate.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtto.Text = orddate.ToString("dd/MMM/yyyy");
            txtSearchCustomer.Text = string.Empty;
            txtSearchprofitcenter.Text = string.Empty;

            rdotobound.Checked = true;
            radpendingTobond.Checked = true;

            lbtnCancel.Enabled = false;
            lbtnCancel.OnClientClick = "return Enable();";
            lbtnCancel.CssClass = "buttoncolorleft";

            Session["BL"] = "";
            ViewState["_RequestItem"] = null;
            ViewState["oImportsBLItems"] = null;
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CusDecBondNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtCustomer.Text + seperator + "A");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReservationNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + BaseCls.GlbDefChannel + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Tobond_bl:
                    {
                        int cusdec = 0;
                        string _tp = null;
                        _tp = ddlRequestType.SelectedValue == "EX" ? "TO" : ddlRequestType.SelectedValue;
                        if (rdotobound.Checked == true)
                        {
                            cusdec = 1;
                        }
                        string _datetype = "0";
                        if (ddlSearchbykeyRequest.Text == "Hand over Date")
                        {
                            _datetype = "1";
                        }

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + cusdec + seperator + txtItem.Text + seperator + _tp + seperator + _datetype);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Tobond_bl_2:
                    {
                        int cusdec = 0;
                        string _tp = null;
                        _tp = ddlRequestType.SelectedValue == "EX" ? "TO" : ddlRequestType.SelectedValue;
                        string _datetype = "0";
                        if (ddlSearchbykeyRequest.Text == "Hand over Date")
                        {
                            _datetype = "1";
                        }
                        if (radpendingTobond.Checked == true)
                        {
                            cusdec = 1;
                        }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + cusdec + seperator + Session["item"].ToString() + seperator + _tp + seperator + _datetype);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RequestNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "CUSA" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        #endregion

        private void GetRequesttype()
        {
            _MasterSubType = CHNLSVC.General.GetAllSubTypes(HiddenFieldRequesttype.Value);
            if (_MasterSubType != null)
            {
                ddlRequestType.DataSource = _MasterSubType;
                ddlRequestType.DataTextField = "mstp_desc";
                ddlRequestType.DataValueField = "mstp_cd";
                ddlRequestType.DataBind();
            }

        }

        #region error msg
        private void DisplayMessage(String Msg, Int32 option)
        {
            string _Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }
        #endregion
        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Customer")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtCustomer.Text = ID;
                lblCustomerName.Text = Name;
                txtCustomer.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Customer_2")
            {
                txtSearchCustomer.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc")
            {
                txtplocation.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "issue_Loc")
            {
                txtissueloc.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }

            if (lblvalue.Text == "Item")
            {
                txtRes.Text = string.Empty;
                txtTobond_Bl.Text = string.Empty;
                txtqty.Text = string.Empty;
                txtItem.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                ItemDetails(ID);
                return;
            }
            if (lblvalue.Text == "Proft-center")
            {
                txtSearchprofitcenter.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Tobond_bl")
            {

                HiddenFieldBQty.Value = grdResult.SelectedRow.Cells[3].Text;
                HiddenFieldReqQty.Value = grdResult.SelectedRow.Cells[4].Text;
                if (rdotobound.Checked == true)
                {

                    string Des = grdResult.SelectedRow.Cells[3].Text;
                    if (checkToBond_grn(Des, txtItem.Text, ""))
                    {
                        txtTobond_Bl.Text = Des;
                        string Bl = grdResult.SelectedRow.Cells[1].Text;
                        Session["BL"] = Bl;

                    }
                    else
                    {
                        //DisplayMessage("Please GRN this ToBond", 1);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('This document still not GRN');", true);
                        return;
                    }

                }
                else if (rdBl.Checked == true)
                {
                    string Des = grdResult.SelectedRow.Cells[1].Text;

                    txtTobond_Bl.Text = Des;
                    CheckBlTobond(Des);
                    Session["BL"] = Des;
                }


                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Tobond_bl_2")
            {
                if (radpendingTobond.Checked == true)
                {
                    string Des = grdResult.SelectedRow.Cells[3].Text;
                    txtaddbond.Text = Des;
                    string Bl = grdResult.SelectedRow.Cells[1].Text;
                    string ITM = "";
                    //GET SELLECTTED ITEM
                    foreach (GridViewRow dgvr in grdrequestItem.Rows)
                    {
                        CheckBox chk = (CheckBox)dgvr.FindControl("chk_ReqItem");

                        if (chk != null & chk.Checked)
                        {
                            Label _Itemcode = (Label)dgvr.FindControl("col_ITRI_ITM_CD");
                            ITM = _Itemcode.Text.ToString();

                        }
                    }


                    Session["BL"] = Bl;
                    if (grdResult.SelectedRow.Cells.Count > 7)
                    {
                        if (!string.IsNullOrEmpty(grdResult.SelectedRow.Cells[7].Text))
                        {
                            txtplocation.Text = grdResult.SelectedRow.Cells[7].Text;
                        }
                    }
                    bool _loc = false;
                    // checkToBond_grn( Des,ITM);
                    DataTable _result = CHNLSVC.Inventory.CHECK_TOBOND_GRN(Session["UserCompanyCode"].ToString(), Des, ITM, out _loc);
                    if (_result != null)
                    {
                        if (_result.Rows.Count > 0)
                        {
                            HiddenGRNloc.Value = _result.Rows[0][0].ToString();
                        }

                    }
                    else
                    {
                        HiddenGRNloc.Value = Session["UserDefLoca"].ToString();
                    }


                }
                else if (radpendingBL.Checked == true)
                {
                    string Des = grdResult.SelectedRow.Cells[1].Text;
                    txtaddbond.Text = Des;
                    Session["BL"] = Des;
                }
                //Session["GRN"] = grdResult.SelectedRow.Cells[13].Text;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Res")
            {
                txtRes.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "REQ_recoal")
            {
                string _status = grdResult.SelectedRow.Cells[3].Text;
                string refno = grdResult.SelectedRow.Cells[2].Text;
                if ((refno != "") && (refno != "&nbsp;"))
                {
                    txtref.Text = refno;
                }
                txtRequest.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                RecallDeatils();
                lblStatus.Text = _status;
                lbtnDeleteItem.Visible = false;
                return;
            }
        }


        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                UserPopoup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            //grdResult.PageIndex = e.NewPageIndex;
            //if (lblvalue.Text == "Customer")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
            //    grdResult.DataSource = _result;            
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Customer_2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Prefer_Loc")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            //    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}

            //if (lblvalue.Text == "Item")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            //   // DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
            //    DataTable result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
            //    grdResult.DataSource = result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Proft-center")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            //    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Tobond_bl")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
            //    DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, null, null);
            //    if (_result.Rows.Count > 0)
            //    {
            //        DataView dv = _result.DefaultView;
            //        dv.Sort = "ib_bl_dt ASC";
            //        _result = dv.ToTable();
            //    }
            //    _result.Columns.Remove("ib_bl_dt");
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "Tobond_bl_2")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl_2);
            //    DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, null, null);
            //    _result.Columns.Add("Location");
            //    _result.Columns["Location"].SetOrdinal(6);
            //    foreach (DataRow dr in _result.Rows)
            //    {
            //        string _loc = "";
            //        if (!string.IsNullOrEmpty(dr["DOC #"].ToString()))
            //        {
            //            InventoryHeader _invHdr = CHNLSVC.Inventory.GET_INT_HDR_DATA(new InventoryHeader()
            //            {
            //                Ith_com = Session["UserCompanyCode"].ToString(),
            //                Ith_loc = Session["UserDefLoca"].ToString(),
            //                Ith_oth_docno = dr["DOC #"].ToString()
            //            }).FirstOrDefault();
            //            if (_invHdr != null)
            //            {
            //                _loc = _invHdr.Ith_loc;
            //            }
            //        }
            //        dr["Location"] = _loc;
            //    }
            //    if (_result.Rows.Count > 0)
            //    {
            //        DataView dv = _result.DefaultView;
            //        dv.Sort = "ib_bl_dt ASC";
            //        _result = dv.ToTable();
            //    }
            //    _result.Columns.Remove("ib_bl_dt");
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
            //if (lblvalue.Text == "REQ_recoal")
            //{

            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
            //    DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text).Date);
            //    grdResult.DataSource = result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //}
            //if (lblvalue.Text == "Res")
            //{
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecBondNo);
            //    DataTable _result = CHNLSVC.Inventory.GETSEARCHRESERVATION(SearchParams, null, null);
            //    grdResult.DataSource = _result;
            //    grdResult.DataBind();
            //    grdResult.PageIndex = 0;
            //    UserPopoup.Show();
            //    return;
            //}
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer_2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "issue_Loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                //DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Proft-center")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Tobond_bl")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_Date(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (_result.Rows.Count > 0)
                {
                    DataView dv = _result.DefaultView;
                    dv.Sort = "ib_doc_rec_dt ASC";
                    _result = dv.ToTable();
                }

                _result.Columns.Remove("ib_doc_rec_dt");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Tobond_bl_2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl_2);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_Date(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                _result.Columns.Add("Location");
                _result.Columns["Location"].SetOrdinal(6);
                foreach (DataRow dr in _result.Rows)
                {
                    string _loc = "";
                    if (!string.IsNullOrEmpty(dr["DOC #"].ToString()))
                    {
                        InventoryHeader _invHdr = CHNLSVC.Inventory.GET_INT_HDR_DATA(new InventoryHeader()
                        {
                            Ith_com = Session["UserCompanyCode"].ToString(),
                            Ith_loc = Session["UserDefLoca"].ToString(),
                            Ith_oth_docno = dr["DOC #"].ToString()
                        }).FirstOrDefault();
                        if (_invHdr != null)
                        {
                            _loc = _invHdr.Ith_loc;
                        }
                    }
                    dr["Location"] = _loc;
                }
                if (_result.Rows.Count > 0)
                {
                    DataView dv = _result.DefaultView;
                    dv.Sort = "ib_doc_rec_dt ASC";
                    _result = dv.ToTable();
                }
                _result.Columns.Remove("ib_doc_rec_dt");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "REQ_recoal")
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text).Date);
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Res")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecBondNo);
                DataTable _result = CHNLSVC.Inventory.GETSEARCHRESERVATION(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Customer_2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "issue_Loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                // DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Proft-center")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Tobond_bl")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_Date(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (_result.Rows.Count > 0)
                {
                    DataView dv = _result.DefaultView;
                    dv.Sort = "ib_doc_rec_dt ASC";
                    _result = dv.ToTable();
                }
                _result.Columns.Remove("ib_doc_rec_dt");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Tobond_bl_2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl_2);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_Date(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "REQ_recoal")
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text).Date);
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Res")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecBondNo);
                DataTable _result = CHNLSVC.Inventory.GETSEARCHRESERVATION(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
        }
        #endregion

        private bool checkToBond_grn(string _doc, string _itm, string statusdesc)
        {
            DataTable _result;
            bool _loc = false;
            // _result = CHNLSVC.Inventory.CHECK_TOBOND_GRN(Session["UserCompanyCode"].ToString(), _doc,_itm,out _loc);
            _result = CHNLSVC.Inventory.CHECK_TOBOND_GRNLTST(Session["UserCompanyCode"].ToString(), _doc, _itm, out _loc);
            if (_result != null)
            {
                if (_result.Rows.Count > 0)
                {
                    DataTable _resulttobond = new DataTable();
                    foreach (DataRow _row in _result.Rows)
                    {

                        string _status = _row[5].ToString();
                        string _statusdesc = _row[5].ToString();

                        if (oMasterItemStatuss != null)
                        {
                            if (oMasterItemStatuss.Count > 0)
                            {
                                MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_desc == _status);
                                if (oStatus != null)
                                {
                                    _status = oStatus.Mis_cd;

                                }
                            }
                        }
                        decimal value = 0;
                        _resulttobond = CHNLSVC.Inventory.CHECK_TOBOND_GRN_status(Session["UserCompanyCode"].ToString(), _doc, _itm, _status);
                        if (_resulttobond != null)
                        {
                            if (_resulttobond.Rows.Count > 0)
                            {
                                value = Convert.ToDecimal(_resulttobond.Rows[0][0].ToString());
                            }

                        }
                        // decimal value = GetPendingno(searchParams[0].ToString(), bond, item);
                        // _row[12] = value.ToString();

                        decimal _grnfree = Convert.ToDecimal(_row[4].ToString());
                        decimal balnce = _grnfree - value;
                        _row[4] = balnce.ToString();

                        if (balnce <= 0)
                        {
                            _row.Delete();
                        }
                        if (statusdesc != "")
                        {
                            if (statusdesc != _statusdesc)
                            {
                                _row.Delete();
                            }
                        }

                    }
                    _result.AcceptChanges();
                    grdGRN.DataSource = _result;
                    grdGRN.DataBind();
                    mdlGRN.Show();
                    //HiddenGRNloc.Value = _loc;
                }
                //HiddenGRNloc.Value = _loc;
            }
            else
            {
                HiddenGRNloc.Value = Session["UserDefLoca"].ToString();
            }

            return _loc;
        }
        private void RecallDeatils()
        {
            InventoryRequest _inputInvReq = new InventoryRequest();
            _inputInvReq.Itr_req_no = txtRequest.Text;
            InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
            if (_selectedInventoryRequest != null)
            {
                txtplocation.Text = _selectedInventoryRequest.Itr_issue_from;
                txtissueloc.Text = _selectedInventoryRequest.Itr_rec_to;
                txtTobond.Text = _selectedInventoryRequest.Itr_job_no;
                txtremark.Text = _selectedInventoryRequest.Itr_note;
                txtCustomer.Text = _selectedInventoryRequest.Itr_bus_code;
                txtgoodgivenDate.Text = _selectedInventoryRequest.Itr_exp_dt.ToShortDateString();
                ddlRequestType.SelectedValue = _selectedInventoryRequest.Itr_sub_tp;
                txtref.Text = _selectedInventoryRequest.Itr_ref;
                txttransfertype.Text = _selectedInventoryRequest.Itr_collector_name;
                txtnewref.Text = _selectedInventoryRequest.Itr_anal1;
                //DataTable dt = CHNLSVC.Sales.Check_INT_REQ_RER(_selectedInventoryRequest.Itr_ref, "CUSR");
                //string SearchParamsRes = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReservationNo);
                //DataTable resultres = CHNLSVC.CommonSearch.Search_INT_RES(SearchParamsRes, "RESERVATION NO", _selectedInventoryRequest.Itr_ref);
                //if (resultres.Rows.Count > 0)
                //{
                //    string code = resultres.Rows[0][1].ToString();
                //    DataTable _type = CHNLSVC.Sales.Select_REF_REQ_SUBTP("RER");
                //    DataRow[] result = _type.Select("RRS_TP = '" + code + "'");
                //    foreach (DataRow row1 in result)
                //    {
                //        txttransfertype.Text = row1[7].ToString();
                //    }
                //}
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result2 = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, "Code", txtCustomer.Text);
                if (result2 != null && result2.Rows.Count > 0)
                {
                    lblCustomerName.Text = result2.Rows[0][1].ToString();
                    txtCustomer.ToolTip = lblCustomerName.Text;
                }
                else
                {
                    string _Msg = "Can't Found Customer.S";
                    DisplayMessage(_Msg, 2);
                }
                _RequestItem = CHNLSVC.Inventory.GetMRN_Req_item(txtRequest.Text);
                foreach (InventoryRequestItem _Item in _RequestItem)
                {
                    if (_Item.ITRI_ITM_COND == "0")
                    {
                        _Item.BL = _Item.Itri_job_no;
                    }
                    else
                    {
                        _Item.To_bond = _Item.Itri_job_no;
                    }
                }
                if (_selectedInventoryRequest.Itr_stus == "C")
                {
                    lblStatus.Text = "CANCELLED";

                    lbtnCancel.Enabled = false;
                    lbtnCancel.OnClientClick = "return Enable();";
                    lbtnCancel.CssClass = "buttoncolorleft";

                    lbtnupdate.Enabled = false;
                    lbtnupdate.OnClientClick = "return Enable();";
                    lbtnupdate.CssClass = "buttoncolorleft";
                }
                else if (_selectedInventoryRequest.Itr_stus == "P")
                {
                    lblStatus.Text = "PENDING";
                    lbtnCancel.Enabled = false;
                    lbtnCancel.OnClientClick = "return Enable();";
                    lbtnCancel.CssClass = "buttoncolorleft";

                    lbtnupdate.Enabled = false;
                    lbtnupdate.OnClientClick = "return Enable();";
                    lbtnupdate.CssClass = "buttoncolorleft";
                }
                else if (_selectedInventoryRequest.Itr_stus == "F")
                {
                    lblStatus.Text = "COMPLETED";
                    lbtnCancel.Enabled = false;
                    lbtnCancel.OnClientClick = "return Enable();";
                    lbtnCancel.CssClass = "buttoncolorleft";

                    lbtnupdate.Enabled = false;
                    lbtnupdate.OnClientClick = "return Enable();";
                    lbtnupdate.CssClass = "buttoncolorleft";
                }
                else
                {
                    lblStatus.Text = "APPROVED";

                    lbtnCancel.Enabled = true;
                    lbtnCancel.OnClientClick = "CancelConfirm();";
                    lbtnCancel.CssClass = "buttonUndocolor";

                    lbtnupdate.Enabled = true;
                    lbtnupdate.OnClientClick = "SaveConfirm();";
                    lbtnupdate.CssClass = "buttonUndocolor";
                }

                lbtnDeleteItem.Visible = false;
                grdItem.DataSource = _RequestItem;
                grdItem.DataBind();
                lbtnSave.Enabled = false;
                lbtnSave.OnClientClick = "return Enable();";
                lbtnSave.CssClass = "buttoncolorleft";
                ViewState["_RequestItem"] = _RequestItem;
            }
            else
            {
                string _Msg = "Please enter valid cusdec number";
                DisplayMessage(_Msg, 2);
            }
        }
        private void ItemDetails(string _item)
        {

            _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itemdetail != null)
            {
                lblItemModel.Text = _itemdetail.Mi_model;
                lblItemDes.Text = _itemdetail.Mi_longdesc;
                lblItemUom.Text = _itemdetail.Mi_itm_uom;
            }
            else
            {
                lblItemModel.Text = string.Empty;
                lblItemDes.Text = string.Empty;
                lblItemUom.Text = string.Empty;
            }
        }
        protected void lbtnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "Customer";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnplocation_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Prefer_Loc";
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void btnissueloc_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "issue_Loc";
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnitem_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "Item";
            ViewState["SEARCH"] = result;
            UserPopoup.Show();
            return;
        }

        protected void grdItem_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (HiddenFieldRequesttype.Value == "CUSR")
            {
                grdItem.Columns[3].Visible = false;
                grdItem.Columns[4].Visible = false;
            }

        }

        protected void lbtnAddItem_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "APPROVED")
            {
                if (HiddenFieldRequesttype.Value == "CUSA")
                {
                    _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
                    var item = _RequestItem.SingleOrDefault(x => x.Itri_itm_cd == txtItem.Text);
                    MasterItem _MstItem = new MasterItem();
                    if (item != null)
                    {
                        decimal BlTotal = CHNLSVC.Inventory.GET_SUM_USE_BL(txtTobond_Bl.Text, txtItem.Text);

                        int rowcount = 0;
                        decimal _remqty = Convert.ToDecimal(txtqty.Text);
                        decimal removecurqty = BlTotal - item.Itri_bqty;

                        oImportsBLItems = new List<ImportsBLItems>();




                        if (item.ITRI_ITM_COND == "1")
                        {
                            oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(txtTobond_Bl.Text, null, txtItem.Text);
                        }
                        else
                        {
                            oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(null, txtTobond_Bl.Text, txtItem.Text);
                        }



                        if (oImportsBLItems != null)
                        {
                            if (oImportsBLItems.Count > 1)
                            {
                                List<ImportsBLItems> _FilterBL = new List<ImportsBLItems>();
                                _FilterBL = ViewState["oImportsBLItems"] as List<ImportsBLItems>;
                                // DisplayMessage("Please contact IT Dep.", 3);
                                return;
                            }
                            else
                            {
                                foreach (ImportsBLItems _BlIterm in oImportsBLItems)
                                {
                                    decimal _BLQTY = _BlIterm.Ibi_bal_qty;
                                    decimal _total = removecurqty + Convert.ToDecimal(txtqty.Text);
                                    _BlIterm.Ibi_req_qty = _total;
                                    if (_total > _BLQTY)
                                    {
                                        DisplayMessage("Invalid quantity", 2);
                                        return;
                                    }
                                }
                            }
                        }

                        item.Itri_qty = Convert.ToDecimal(txtqty.Text);
                        item.Itri_bqty = Convert.ToDecimal(txtqty.Text);
                        _MstItem.Mi_cd = txtItem.Text;
                        item.MasterItem = _MstItem;
                    }


                    grdItem.DataSource = _RequestItem;
                    grdItem.DataBind();

                    ViewState["_RequestItem"] = _RequestItem;


                    List<ImportsBLItems> _oldBL = new List<ImportsBLItems>();
                    _oldBL = ViewState["oImportsBLItems"] as List<ImportsBLItems>;
                    if (_oldBL != null)
                    {
                        if (_oldBL.Count > 0)
                        {

                            _oldBL.AddRange(oImportsBLItems);
                            ViewState["oImportsBLItems"] = _oldBL;

                            txtItem.Text = string.Empty;
                            txtRes.Text = string.Empty;
                            txtqty.Text = string.Empty;
                            txtTobond_Bl.Text = string.Empty;
                            rdotobound.Checked = true;
                            rdBl.Checked = false;
                            return;
                        }
                    }
                    else
                    {
                        _oldBL = oImportsBLItems;
                    }

                    ViewState["oImportsBLItems"] = _oldBL;
                    txtItem.Text = string.Empty;
                    txtRes.Text = string.Empty;
                    txtqty.Text = string.Empty;
                    txtTobond_Bl.Text = string.Empty;
                    rdotobound.Checked = true;
                    rdBl.Checked = false;
                    return;
                }
            }


            bool _IsFindItem = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                DisplayMessage("Please insert the item code", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtqty.Text))
            {
                DisplayMessage("Please insert the quntity", 2);
                return;
            }
            if (!IsNumeric(txtqty.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid  quntity ", 2);
                txtqty.Focus();
                return;
            }
            if (HiddenFieldRequesttype.Value == "CUSR")
            {
                _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
                if (_RequestItem == null)
                {
                    _RequestItem = new List<InventoryRequestItem>();
                }
                InventoryRequestItem _Item = new InventoryRequestItem();
                MasterItem _MstItem = new MasterItem();
                //_Item.Itr_req_no 
                _Item.Itri_itm_cd = txtItem.Text;
                _Item.Itri_res_no = txtRes.Text;
                _Item.Itri_itm_stus = "GOD";
                _Item.Itri_qty = Convert.ToDecimal(txtqty.Text);
                _Item.Itri_bqty = Convert.ToDecimal(txtqty.Text);
                _MstItem.Mi_cd = txtItem.Text;
                _Item.MasterItem = _MstItem;


                var item = _RequestItem.Where(x => x.Itri_itm_cd == txtItem.Text).ToList();

                if (item.Count > 0)
                {
                    DisplayMessage("Item is already added", 2);
                    return;
                }
                _RequestItem.Add(_Item);

                grdItem.DataSource = _RequestItem;
                grdItem.DataBind();
                ViewState["_RequestItem"] = _RequestItem;
                txtItem.Text = string.Empty;
                txtRes.Text = string.Empty;
                txtqty.Text = string.Empty;
                txtTobond_Bl.Text = string.Empty;
                rdotobound.Checked = true;
                rdBl.Checked = false;
            }
            else if (HiddenFieldRequesttype.Value == "CUSA")
            {
                if (string.IsNullOrEmpty(txtTobond_Bl.Text))
                {
                    DisplayMessage("Please type To-Bond or BL number", 2);
                    return;
                }
                if ((rdotobound.Checked == false) && (rdBl.Checked == false))
                {
                    DisplayMessage("Please select To-Bond or BL number  ", 2);
                    return;
                }
                _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
                if (_RequestItem == null)
                {
                    _RequestItem = new List<InventoryRequestItem>();
                }
                InventoryRequestItem _Item = new InventoryRequestItem();
                MasterItem _MstItem = new MasterItem();
                //_Item.Itr_req_no 
                _Item.Itri_itm_cd = txtItem.Text;
                _Item.Itri_res_no = txtRes.Text;
                _Item.Itri_itm_stus = "GOD";
                _Item.Itri_qty = Convert.ToDecimal(txtqty.Text);
                _Item.Itri_bqty = Convert.ToDecimal(txtqty.Text);
                _Item.Itri_job_no = txtTobond_Bl.Text;
                _MstItem.Mi_cd = txtItem.Text;
                _Item.MasterItem = _MstItem;
                if (rdotobound.Checked == true)
                {
                    _Item.To_bond = txtTobond_Bl.Text;
                    _Item.ITRI_ITM_COND = "1";
                    _Item.Itri_job_no = txtTobond_Bl.Text;
                }
                else if (rdBl.Checked == true)
                {
                    _Item.BL = txtTobond_Bl.Text;
                    _Item.ITRI_ITM_COND = "0";
                    _Item.Itri_job_no = txtTobond_Bl.Text;
                }
                if (!string.IsNullOrEmpty(HiddenFieldBQty.Value))
                {
                    decimal _bqty = Convert.ToDecimal(HiddenFieldBQty.Value);
                    if (_bqty < _Item.Itri_bqty)
                    {
                        DisplayMessage("cannot exceed bl qty ", 2);
                        return;
                    }
                    else
                    {
                        bool _loc2 = false;
                        DataTable _result = CHNLSVC.Inventory.CHECK_TOBOND_GRNLTST(Session["UserCompanyCode"].ToString(), _Item.Itri_job_no, _Item.Itri_itm_cd, out _loc2);
                        if (_result != null && Hiddenstatus.Value != null)
                        {
                            if (_result.Rows.Count > 0)
                            {
                                decimal _totqty = 0;
                                int _i = 0;
                                foreach (var _row in _result.Rows)
                                {
                                    if (_result.Rows[_i][5].ToString() == Hiddenstatus.Value)
                                    {
                                        _totqty = _totqty + Convert.ToDecimal(_result.Rows[_i][4].ToString());
                                    }
                                    _i++;
                                }
                                if (_totqty < _Item.Itri_bqty)
                                {
                                    DisplayMessage("cannot exceed Grn qty ", 2);
                                    return;
                                }
                            }
                        }
                    }


                }



                #region  Check BL Balance and break Item Qty and set job_line by subo
                List<ImportsBLItems> _blitems = new List<ImportsBLItems>();
                if (rdotobound.Checked == true)
                {
                    _blitems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(_Item.Itri_job_no, "", _Item.Itri_itm_cd);
                }
                else if (rdBl.Checked == true)
                {
                    _blitems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm("", txtTobond_Bl.Text, _Item.Itri_itm_cd);
                }
                if (_blitems == null)
                {
                    _blitems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(txtTobond_Bl.Text, "", _Item.Itri_itm_cd);
                }
                List<ImportsBLItems> _blitems2 = _blitems.Where(a => (a.Ibi_qty - a.Ibi_req_qty) >= _Item.Itri_qty).ToList();

                if (_blitems2 != null)
                {
                    if (_blitems2.Count > 0)
                    {
                        _Item.Itri_job_line = _blitems2.First().Ibi_line;
                    }
                    else
                    {
                        var _sum = _blitems.Sum(a => (a.Ibi_qty - a.Ibi_req_qty));
                        if (_sum >= _Item.Itri_qty)
                        {
                            _blitems = _blitems.OrderByDescending(a => (a.Ibi_qty - a.Ibi_req_qty)).ToList();
                            DisplayMessage("Please Try Save Qty: " + (_blitems.First().Ibi_qty - _blitems.First().Ibi_req_qty) + " First", 2);
                            return;
                        }
                        else
                        {
                            DisplayMessage("cannot exceed bl qty ", 2);
                            return;
                        }
                    }
                }
                else
                {
                    var _sum = _blitems.Sum(a => (a.Ibi_qty - a.Ibi_req_qty));
                    if (_sum >= _Item.Itri_qty)
                    {
                        _blitems = _blitems.OrderByDescending(a => (a.Ibi_qty - a.Ibi_req_qty)).ToList();
                        DisplayMessage("Please Try Save Qty: " + (_blitems.First().Ibi_qty - _blitems.First().Ibi_req_qty) + " First", 2);
                        return;
                    }
                    else
                    {
                        DisplayMessage("cannot exceed bl qty ", 2);
                        return;
                    }
                }
                #endregion
                var item = _RequestItem.Where(x => x.Itri_itm_cd == txtItem.Text).ToList();

                if (item.Count > 0)
                {
                    DisplayMessage("Item is already added  ", 2);
                    return;
                }
                else if (item.Count == 0)
                {
                    _Item.Itri_line_no = 1;
                }
                else
                {
                    int maxline = item.Max(t => t.Itri_line_no);
                    _Item.Itri_line_no = maxline + 1;

                }
                if (txtTobond_Bl.Text != "")
                {
                    bool IsTobond = false;
                    oImportsBLItems.Clear();
                    string _ITM = txtItem.Text;
                    if (HiddenItem.Value != "")
                    {
                        _Item.Itri_mitm_cd = txtItem.Text;
                        txtItem.Text = HiddenItem.Value;

                        _Item.Itri_itm_cd = txtItem.Text;
                    }
                    if (Hiddenstatus.Value != "")
                    {
                        if (oMasterItemStatuss != null)
                        {
                            if (oMasterItemStatuss.Count > 0)
                            {
                                MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_desc == Hiddenstatus.Value);
                                if (oStatus != null)
                                {
                                    _Item.Itri_mitm_stus = oStatus.Mis_cd;
                                    _Item.Itri_itm_stus = oStatus.Mis_cd;
                                }
                            }
                        }

                    }

                    //Check grn status validation - req by lakshan
                    if (CHNLSVC.Financial.isstatusvalidation(Session["UserCompanyCode"].ToString(), _Item.Itri_itm_cd, _Item.Itri_itm_stus, _Item.Itri_job_no, _Item.Itri_qty) == false)
                    {
                        DisplayMessage("No Free Qty  " + _Item.Itri_itm_stus + " Status", 2);
                        return;
                    }

                    //else
                    //{
                    //    _Item.Itri_mitm_cd = txtItem.Text;
                    //}

                    if (rdotobound.Checked == true)
                    {
                        oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(txtTobond_Bl.Text, null, txtItem.Text);
                        IsTobond = true;
                    }
                    else if (rdBl.Checked == true)
                    {
                        oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(null, txtTobond_Bl.Text, txtItem.Text);
                        IsTobond = false;
                    }
                    if (oImportsBLItems == null)
                    {
                        oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(txtTobond_Bl.Text, null, txtItem.Text);
                        IsTobond = true;
                    }

                    decimal _remqty = Convert.ToDecimal(txtqty.Text);
                    if (oImportsBLItems != null)
                    {
                        if (IsTobond)
                        {
                            _Item.To_bond = txtTobond_Bl.Text;
                            _Item.BL = "";
                        }
                        if (oImportsBLItems.Count > 1)
                        {
                            List<ImportsBLItems> _FilterBL = new List<ImportsBLItems>();
                            _FilterBL = ViewState["oImportsBLItems"] as List<ImportsBLItems>;
                            if (_FilterBL != null)
                            {

                                int _seq = oImportsBLItems[0].Ibi_seq_no;
                                decimal _ToatalReq = _FilterBL.Where(x => x.Ibi_seq_no == _seq && x.Ibi_itm_cd == txtItem.Text).Sum(d => d.Ibi_req_qty);
                                if (_ToatalReq == 0)
                                {
                                    int _line = 0;
                                    foreach (ImportsBLItems _BlIterm in oImportsBLItems)
                                    {
                                        decimal qty = _BlIterm.Ibi_qty;
                                        decimal reqqty = _BlIterm.Ibi_req_qty;
                                        decimal total = qty - reqqty;
                                        _line = _BlIterm.Ibi_line;
                                        if (total >= _remqty)
                                        {
                                            break;
                                        }
                                    }

                                    oImportsBLItems = oImportsBLItems.Where(y => y.Ibi_line == _line).ToList();
                                    int rowcount = 0;
                                    #region calculation
                                    foreach (ImportsBLItems _BlIterm in oImportsBLItems)
                                    {
                                        decimal qty = _BlIterm.Ibi_qty;
                                        decimal reqqty = _BlIterm.Ibi_req_qty;
                                        decimal total = qty - reqqty;

                                        if (_IsFindItem == false)
                                        {
                                            _Item.Itri_job_line = _BlIterm.Ibi_line;
                                            if (oImportsBLItems.Count == 1)
                                            {
                                                if (total < _remqty)
                                                {
                                                    DisplayMessage("Invalid quantity", 2);
                                                    return;
                                                }
                                                else
                                                {

                                                    _BlIterm.Ibi_req_qty = _remqty;
                                                    _IsFindItem = true;
                                                }
                                            }
                                            else if (oImportsBLItems.Count > 1)
                                            {
                                                if (total >= _remqty)
                                                {
                                                    _BlIterm.Ibi_req_qty = _remqty;
                                                    _IsFindItem = true;
                                                }
                                                else if (total < _remqty)
                                                {
                                                    if (rowcount == oImportsBLItems.Count)
                                                    {
                                                        DisplayMessage("Invalid quantity", 2);
                                                        return;
                                                    }
                                                    //_remqty = _remqty - qty; comment by rukshan 11/apr/2016 reson minus request qty
                                                    _BlIterm.Ibi_req_qty = total;// _BlIterm.Ibi_req_qty + total;
                                                    _remqty = _remqty - total;

                                                }

                                            }


                                        }
                                        rowcount++;
                                    }
                                    #endregion

                                }
                                else
                                {
                                    DisplayMessage("same item line found please select item option", 3);
                                    return;
                                }

                            }
                            else
                            {
                                int _line = 0;
                                foreach (ImportsBLItems _BlIterm in oImportsBLItems)
                                {
                                    decimal qty = _BlIterm.Ibi_qty;
                                    decimal reqqty = _BlIterm.Ibi_req_qty;
                                    decimal total = qty - reqqty;
                                    _line = _BlIterm.Ibi_line;
                                    if (total >= _remqty)
                                    {
                                        break;
                                    }
                                }

                                oImportsBLItems = oImportsBLItems.Where(y => y.Ibi_line == _line).ToList();
                                int rowcount = 0;
                                #region calculation
                                foreach (ImportsBLItems _BlIterm in oImportsBLItems)
                                {
                                    decimal qty = _BlIterm.Ibi_qty;
                                    decimal reqqty = _BlIterm.Ibi_req_qty;
                                    decimal total = qty - reqqty;

                                    if (_IsFindItem == false)
                                    {
                                        _Item.Itri_job_line = _BlIterm.Ibi_line;
                                        if (oImportsBLItems.Count == 1)
                                        {
                                            if (total < _remqty)
                                            {
                                                DisplayMessage("Invalid quantity", 2);
                                                return;
                                            }
                                            else
                                            {

                                                _BlIterm.Ibi_req_qty = _remqty;
                                                _IsFindItem = true;
                                            }
                                        }
                                        else if (oImportsBLItems.Count > 1)
                                        {
                                            if (total >= _remqty)
                                            {
                                                _BlIterm.Ibi_req_qty = _remqty;
                                                _IsFindItem = true;
                                            }
                                            else if (total < _remqty)
                                            {
                                                if (rowcount == oImportsBLItems.Count)
                                                {
                                                    DisplayMessage("Invalid quantity", 2);
                                                    return;
                                                }
                                                //_remqty = _remqty - qty; comment by rukshan 11/apr/2016 reson minus request qty
                                                _BlIterm.Ibi_req_qty = total;// _BlIterm.Ibi_req_qty + total;
                                                _remqty = _remqty - total;

                                            }

                                        }


                                    }
                                    rowcount++;
                                }
                                #endregion

                            }
                            //  var _selectBL = _FilterBL.Where(x=>x.)
                            // DisplayMessage("Please contact IT Dep.", 3);
                            // return;
                        }
                        else
                        {
                            int rowcount = 0;

                            foreach (ImportsBLItems _BlIterm in oImportsBLItems)
                            {
                                decimal qty = _BlIterm.Ibi_qty;
                                decimal reqqty = _BlIterm.Ibi_req_qty;
                                decimal total = qty - reqqty;

                                if (_IsFindItem == false)
                                {
                                    _Item.Itri_job_line = _BlIterm.Ibi_line;
                                    if (oImportsBLItems.Count == 1)
                                    {
                                        if (total < _remqty)
                                        {
                                            DisplayMessage("Invalid quantity", 2);
                                            return;
                                        }
                                        else
                                        {

                                            _BlIterm.Ibi_req_qty = _remqty;
                                            _IsFindItem = true;
                                        }
                                    }
                                    else if (oImportsBLItems.Count > 1)
                                    {
                                        if (total >= _remqty)
                                        {
                                            _BlIterm.Ibi_req_qty = _remqty;
                                            _IsFindItem = true;
                                        }
                                        else if (total < _remqty)
                                        {
                                            if (rowcount == oImportsBLItems.Count)
                                            {
                                                DisplayMessage("Invalid quantity", 2);
                                                return;
                                            }
                                            //_remqty = _remqty - qty; comment by rukshan 11/apr/2016 reson minus request qty
                                            _BlIterm.Ibi_req_qty = total;// _BlIterm.Ibi_req_qty + total;
                                            _remqty = _remqty - total;

                                        }

                                    }


                                }
                                rowcount++;
                            }
                        }

                        txtItem.Text = string.Empty;
                        txtRes.Text = string.Empty;
                        txtqty.Text = string.Empty;
                        txtTobond_Bl.Text = string.Empty;
                        rdotobound.Checked = true;
                        rdBl.Checked = false;

                        radpendingTobond.Checked = false;
                        radpendingBL.Checked = false;
                        txtaddbond.Text = string.Empty;
                        txtbondqty.Text = string.Empty;
                    }
                    else
                    {
                        DisplayMessage("Invalid document number", 2);
                        return;
                    }
                }


                _RequestItem.Add(_Item);
                grdItem.DataSource = _RequestItem;
                grdItem.DataBind();
                ViewState["_RequestItem"] = _RequestItem;
                List<ImportsBLItems> _oldBL = new List<ImportsBLItems>();
                _oldBL = ViewState["oImportsBLItems"] as List<ImportsBLItems>;
                if (_oldBL != null)
                {
                    if (_oldBL.Count > 0)
                    {

                        _oldBL.AddRange(oImportsBLItems);
                        ViewState["oImportsBLItems"] = _oldBL;
                        txtItem.Text = string.Empty;
                        txtRes.Text = string.Empty;
                        txtqty.Text = string.Empty;
                        txtTobond_Bl.Text = string.Empty;
                        lblItemDes.Text = string.Empty;
                        lblItemModel.Text = string.Empty;
                        lblItemUom.Text = string.Empty;
                        rdotobound.Checked = true;
                        rdBl.Checked = false;
                        return;
                    }
                }
                else
                {
                    _oldBL = oImportsBLItems;
                }


                ViewState["oImportsBLItems"] = _oldBL;
                txtItem.Text = string.Empty;
                txtRes.Text = string.Empty;
                txtqty.Text = string.Empty;
                txtTobond_Bl.Text = string.Empty;
                lblItemDes.Text = string.Empty;
                lblItemModel.Text = string.Empty;
                lblItemUom.Text = string.Empty;

                rdotobound.Checked = true;
                rdBl.Checked = false;
            }
        }

        protected void lbtnDeleteItem_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            List<InventoryRequestItem> _old = new List<InventoryRequestItem>();
            List<InventoryRequestItem> _filterItem = new List<InventoryRequestItem>();
            List<ImportsBLItems> BLItemsList = new List<ImportsBLItems>();
            BLItemsList = ViewState["oImportsBLItems"] as List<ImportsBLItems>;

            string _type = Session["type"].ToString();
            if (_type == "CUSR")
            {

                _old = CHNLSVC.Inventory.GetMRN_Req_item(txtRequest.Text);
                _filterItem = _old.Where(x => x.Itri_bqty > 0).ToList();
            }
            else if (_type == "CUSA_BLITEM")
            {
                bool IsDeleted1 = false;
                _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
                if (_RequestItem == null)
                {
                    DisplayMessage("No item found to delete", 2);
                    return;
                }
                foreach (GridViewRow dgvr in grdItem.Rows)
                {
                    CheckBox chk = (CheckBox)dgvr.FindControl("chk_Items");

                    if (chk != null & chk.Checked)
                    {
                        Label _Itemcode = (Label)dgvr.FindControl("col_Itri_itm_cd");
                        _RequestItem.RemoveAll(x => x.Itri_itm_cd == _Itemcode.Text);
                        BLItemsList.RemoveAll(x => x.Ibi_itm_cd == _Itemcode.Text);
                        IsDeleted1 = true;
                    }
                }
                if (IsDeleted1 == false)
                {
                    DisplayMessage("No item found to delete", 2);
                    return;
                }
                grdItem.DataSource = _RequestItem;
                grdItem.DataBind();
                ViewState["_RequestItem"] = _RequestItem;
                ViewState["oImportsBLItems"] = BLItemsList;
                return;
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.Check_INT_REQ_RER(Session["Reqno"].ToString(), "RER");
                if (dt.Rows.Count > 0)
                {
                    string code = dt.Rows[0][28].ToString();
                    DataTable _type2 = CHNLSVC.Sales.Select_REF_REQ_SUBTP("RER");
                    DataRow[] result = _type2.Select("RRS_TP = '" + code + "'");
                    foreach (DataRow row1 in result)
                    {
                        txttransfertype.Text = row1[7].ToString();
                    }
                }

                List<InventoryRequestItem> _Item = new List<InventoryRequestItem>();
                INR_RES inr_res = new INR_RES();
                inr_res = CHNLSVC.Sales.GetReservationApproval(txtRequest.Text);
                List<INR_RES_DET> inr_res_det = new List<INR_RES_DET>();
                inr_res_det = CHNLSVC.Sales.GetGetReservationApprovalItem(inr_res.IRS_SEQ);
                foreach (INR_RES_DET _det in inr_res_det)
                {
                    InventoryRequestItem _req = new InventoryRequestItem();
                    _req.Itr_req_no = _det.IRD_RES_NO;
                    _req.Itri_itm_cd = _det.IRD_ITM_CD;
                    _req.Mi_longdesc = _det.MIS_DESC;
                    _req.Mst_item_model = _det.MI_MODEL;
                    _req.Itri_res_no = _det.IRD_RES_NO;
                    _req.Itri_qty = _det.IRD_RES_QTY;
                    _req.Itri_bqty = _det.IRD_RES_QTY;
                    _req.Itri_line_no = _det.IRD_LINE;
                    _Item.Add(_req);
                }
                _filterItem = _Item.Where(x => x.Itri_bqty > 0).ToList();
            }

            bool IsDeleted = false;
            _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
            if (_RequestItem == null)
            {
                DisplayMessage("No item found to delete", 2);
                return;
            }
            foreach (GridViewRow dgvr in grdItem.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("chk_Items");

                if (chk != null & chk.Checked)
                {
                    Label _Itemcode = (Label)dgvr.FindControl("col_Itri_itm_cd");
                    _RequestItem.RemoveAll(x => x.Itri_itm_cd == _Itemcode.Text);
                    BLItemsList.RemoveAll(x => x.Ibi_itm_cd == _Itemcode.Text);
                    IsDeleted = true;
                }
            }
            if (IsDeleted == false)
            {
                DisplayMessage("No item found to delete", 2);
                return;
            }

            grdItem.DataSource = _RequestItem;
            grdItem.DataBind();
            ViewState["_RequestItem"] = _RequestItem;


            if (_RequestItem.Count > 0)
            {

                foreach (InventoryRequestItem _itemsl in _RequestItem.ToList())
                {
                    if (_filterItem.Count > 0)
                    {
                        var itemToRemove = _filterItem.Single(r => r.Itri_itm_cd == _itemsl.Itri_itm_cd);
                        _filterItem.Remove(itemToRemove);

                    }

                }
                grdrequestItem.DataSource = _filterItem;
                grdrequestItem.DataBind();
                ViewState["_PendingRequestItem"] = _filterItem;

            }
            else
            {
                List<InventoryRequestItem> _old2 = new List<InventoryRequestItem>();
                _old2 = ViewState["OldItem_back"] as List<InventoryRequestItem>;
                grdrequestItem.DataSource = _old2;
                grdrequestItem.DataBind();
                ViewState["_PendingRequestItem"] = _old2;
            }
            //var itemToRemove = _old.Single(r => r.Id == 2);
            //resultList.Remove(itemToRemove);
            // _old = _old.Union(_RequestItem).ToList();

        }

        private void SaveRequest()
        {
            try
            {
                _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
                oImportsBLItems = ViewState["oImportsBLItems"] as List<ImportsBLItems>;
                int rowsAffected = 0;
                string _docNo = string.Empty;
                InventoryRequest _inventoryRequest = new InventoryRequest();
                _inventoryRequest.Itr_com = Session["UserCompanyCode"].ToString();
                // _inventoryRequest.Itr_req_no = reqno;
                //if (_RequestItem != null)
                //{
                //    string Msg = "Please add item";
                //    DisplayMessage(Msg, 2);
                //    return;
                //}
                if (ddlRequestType.SelectedValue == "RE")//add by rukshan-request chamal
                {
                    if (string.IsNullOrEmpty(txtCustomer.Text))
                    {
                        txtCustomer.Text = Session["UserCompanyCode"].ToString();
                    }

                }
                if (ddlRequestType.SelectedValue == "EXP")//add by rukshan-request chamal
                {
                    if (string.IsNullOrEmpty(txtCustomer.Text))
                    {
                        string Msg = "Please select customer";
                        DisplayMessage(Msg, 2);
                        return;
                    }

                }
                if (HiddenGRNloc.Value != "")
                {
                    _inventoryRequest.Itr_loc = HiddenGRNloc.Value;//UserDefProf
                    _inventoryRequest.Itr_issue_from = HiddenGRNloc.Value;
                }
                else
                {
                    _inventoryRequest.Itr_loc = Session["UserDefLoca"].ToString();//UserDefProf
                    _inventoryRequest.Itr_issue_from = txtplocation.Text;
                }


                //_inventoryRequest.Itr_issue_from = txtplocation.Text;

                _inventoryRequest.Itr_ref = txtRequest.Text;
                _inventoryRequest.Itr_dt = Convert.ToDateTime(txtDate.Text);
                //_inventoryRequest.Itr_exp_dt ="";
                if (HiddenFieldRequesttype.Value == "CUSR")
                {
                    _inventoryRequest.Itr_tp = "CUSR";
                    _inventoryRequest.Itr_sub_tp = ddlRequestType.SelectedValue;
                    _inventoryRequest.Itr_stus = "P";
                }
                else if (HiddenFieldRequesttype.Value == "CUSA")
                {
                    _inventoryRequest.Itr_tp = "CUSA";
                    _inventoryRequest.Itr_sub_tp = ddlRequestType.SelectedValue;
                    _inventoryRequest.Itr_stus = "A";
                }
                _inventoryRequest.Itr_job_no = txtTobond.Text;

                _inventoryRequest.Itr_bus_code = txtCustomer.Text;

                _inventoryRequest.Itr_note = txtremark.Text;





                _inventoryRequest.Itr_rec_to = txtissueloc.Text.ToString();
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtgoodgivenDate.Text);
                _inventoryRequest.Itr_country_cd = string.Empty;
                _inventoryRequest.Itr_town_cd = string.Empty;
                _inventoryRequest.Itr_cur_code = string.Empty;
                _inventoryRequest.Itr_exg_rate = 0;
                _inventoryRequest.Itr_collector_id = string.Empty;
                _inventoryRequest.Itr_collector_name = string.Empty;
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = Session["UserID"].ToString();
                _inventoryRequest.Itr_session_id = Session["SessionID"].ToString();
                _inventoryRequest.Itr_issue_com = Session["UserCompanyCode"].ToString();
                _inventoryRequest.Itr_collector_name = txttransfertype.Text;
                _inventoryRequest.InventoryRequestItemList = _RequestItem;
                _inventoryRequest.Itr_anal1 = txtnewref.Text;
                _inventoryRequest.Itr_anal2 = Session["BL"].ToString();
                _inventoryRequest._checkLineno = true;
                _inventoryRequest.UpdateResLog = true;
                _inventoryRequest.balancechek = true;


                //CHECK LONG ROOM
                DataTable _tbl = CHNLSVC.Financial.GetCusdechdrByBL(Session["UserCompanyCode"].ToString(), Session["BL"].ToString());
                if (_tbl != null)
                {
                    if (_tbl.Rows.Count > 0)
                    {
                        if (_tbl.Rows[0]["cuh_tp"].ToString() != "TO")
                        {
                            string Msg = "Cant Save Request! Please Check SI No";
                            DisplayMessage(Msg, 2);
                            return;
                        }
                    }
                }



                // rowsAffected = CHNLSVC.Inventory.SaveCusdecEntry(_inventoryRequest, GenerateMasterAutoNumber(), oImportsBLItems, out _docNo);
                rowsAffected = CHNLSVC.Financial.SaveCusdecEntry(_inventoryRequest, GenerateMasterAutoNumber(), oImportsBLItems, out _docNo);
                if (rowsAffected != -1)
                {
                    string Msg = "Successfully saved. " + _docNo;
                    DisplayMessage(Msg, 3);
                    lbtnSave.Visible = true;
                    //pageClear();
                    saveClear();
                }
                else
                {
                    //string Msg = "Invalid data found";
                    DisplayMessage(_docNo, 2);
                    lbtnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
                lbtnSave.Visible = true;
            }
        }
        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            string _Aut_moduleid = "";
            // string _Aut_start_char = "";
            if (!string.IsNullOrEmpty(ddlRequestType.SelectedValue))
            {
                _Aut_moduleid = ddlRequestType.SelectedValue;
                //   _Aut_start_char = ddlRequestType.SelectedValue;
            }

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;

            if (HiddenFieldRequesttype.Value == "CUSR")
            {
                masterAuto.Aut_moduleid = "CUSR";
                masterAuto.Aut_start_char = "CUSR";
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            }
            else if (HiddenFieldRequesttype.Value == "CUSA")
            {
                masterAuto.Aut_moduleid = _Aut_moduleid;
                masterAuto.Aut_start_char = "CUR";
                masterAuto.Aut_cate_tp = "COM";
                masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            }
            // masterAuto.Aut_moduleid = "REQD";
            masterAuto.Aut_number = 0;
            // masterAuto.Aut_start_char = "REQD";
            masterAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            return masterAuto;
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "Yes")
            {
                lbtnSave.Visible = false;
                SaveRequest();
            }
            else
            {
                lbtnSave.Visible = true;
            }
        }
        protected void lbtnupdate_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text != "APPROVED")
            {
                string Msg = "You can amend only approved request.";
                DisplayMessage(Msg, 2);
                return;
            }

            _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
            oImportsBLItems = ViewState["oImportsBLItems"] as List<ImportsBLItems>;

            InventoryRequest reqhdr = new InventoryRequest();
            reqhdr.Itr_bus_code = txtCustomer.Text.ToString();
            reqhdr.Itr_req_no = txtRequest.Text.ToString();
            reqhdr.Itr_com = Session["UserCompanyCode"].ToString();
            InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(reqhdr);
            _selectedInventoryRequest.Itr_bus_code = txtCustomer.Text.ToString();
            _selectedInventoryRequest.Itr_ref = txtref.Text.ToString();
            _selectedInventoryRequest.Itr_note = txtremark.Text.ToString();
            int rowsAffected = 0;
            string _docNo = string.Empty;
            _selectedInventoryRequest._checkLineno = true;
            rowsAffected = CHNLSVC.Inventory.SaveCUSA_amend(_RequestItem, oImportsBLItems, _selectedInventoryRequest, out _docNo);
            if (rowsAffected != -1)
            {
                string Msg = "Successfully Update. " + txtRequest.Text;
                DisplayMessage(Msg, 3);
                pageClear();
            }
            else
            {

                DisplayMessage(_docNo, 2);
            }

        }


        protected void lbtnSearchCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "Customer_2";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string _type = string.Empty;
            string _status = string.Empty;
            if (radpendingreq.Checked == true)
            {
                _type = "CUSR";
                _status = "P";
            }
            else if (radpendigres.Checked == true)
            {
                _type = "RER";
                _status = "F";
            }
            List<InventoryRequest> _reuest = new List<InventoryRequest>();
            if ((txtSearchCustomer.Text == "") && (txtSearchprofitcenter.Text == ""))
            {
                //List<InventoryRequest> _reuest = CHNLSVC.Inventory.GetCusdecEntryRequest(null, null, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtto.Text), _type, _status, Session["UserDefLoca"].ToString());
                _reuest = CHNLSVC.Inventory.GetCusdecEntryRequest(null, null, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtto.Text), _type, _status, null);
                //_reuest = _reuest.Distinct().ToList();
                //grdrequest.DataSource = _reuest;
                //grdrequest.DataBind();
            }
            else if (txtSearchprofitcenter.Text != "")
            {
                //List<InventoryRequest> _reuest = CHNLSVC.Inventory.GetCusdecEntryRequest(txtSearchprofitcenter.Text, null, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtto.Text), _type, _status, Session["UserDefLoca"].ToString());
                _reuest = CHNLSVC.Inventory.GetCusdecEntryRequest(txtSearchprofitcenter.Text, null, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtto.Text), _type, _status, null);
                //_reuest = _reuest.Distinct().ToList();
                //grdrequest.DataSource = _reuest;
                //grdrequest.DataBind();
            }
            else if (txtSearchCustomer.Text != "")
            {
                //List<InventoryRequest> _reuest = CHNLSVC.Inventory.GetCusdecEntryRequest(null, txtSearchCustomer.Text, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtto.Text), _type, _status, Session["UserDefLoca"].ToString());
                _reuest = CHNLSVC.Inventory.GetCusdecEntryRequest(null, txtSearchCustomer.Text, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtto.Text), _type, _status, null);
                //_reuest = _reuest.Distinct().ToList();
                //grdrequest.DataSource = _reuest;
                //grdrequest.DataBind();
            }
            List<InventoryRequest> tmpReq = new List<InventoryRequest>();
            if (_reuest != null && _reuest.Count > 0)
            {
                foreach (var item in _reuest)
                {
                    bool isAvailable = false;
                    if (tmpReq.Count > 0)
                    {
                        var v = tmpReq.Where(c => c.Ird_res_no == item.Ird_res_no).ToList();
                        if (v != null)
                        {
                            if (v.Count > 0)
                            {
                                isAvailable = true;
                            }
                        }
                    }
                    if (!isAvailable)
                    {
                        tmpReq.Add(item);
                    }
                }
            }
            grdrequest.DataSource = new int[] { };
            if (tmpReq.Count > 0)
            {
                tmpReq = tmpReq.OrderBy(c => c.Ird_res_no).ToList();
                grdrequest.DataSource = tmpReq;
                grdrequest.DataBind();
            }
            grdrequest.DataBind();
        }

        protected void chk_Req_CheckedChanged_Click(object sender, EventArgs e)
        {
            try
            {
                List<InventoryRequestItem> _Item = new List<InventoryRequestItem>();
                List<InventoryRequest> _hdd = new List<InventoryRequest>();
                if (grdrequest.Rows.Count == 0) return;
                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {
                        string Reqno = (row.FindControl("col_itr_req_no") as Label).Text;
                        Session["Reqno"] = Reqno;
                        string Appno = (row.FindControl("col_ird_res_no") as Label).Text;
                        // string Reqtype = (row.FindControl("col_itr_tp") as Label).Text;
                        string PreLoc = (row.FindControl("col_itr_loc") as Label).Text;
                        string Code = (row.FindControl("col_itr_bus_code") as Label).Text;
                        string cname = (row.FindControl("col_mbe_name") as Label).Text;
                        string type = (row.FindControl("col_itr_tp") as Label).Text;
                        string _ref = (row.FindControl("col_itr_ref") as Label).Text;
                        string _remark = (row.FindControl("col_itr_note") as Label).Text;
                        string _TransferType = (row.FindControl("col_itr_collector_name") as Label).Text;
                        Session["type"] = type;
                        string subtype = (row.FindControl("col_Itr_sub_tp") as Label).Text;
                        DateTime expir = Convert.ToDateTime((row.FindControl("col_itr_exp_dt") as Label).Text);
                        txtRequest.Text = Appno;
                        txtplocation.Text = PreLoc;
                        txtCustomer.Text = Code;
                        lblCustomerName.Text = cname;
                        txtCustomer.ToolTip = cname;
                        txtgoodgivenDate.Text = expir.ToString("dd/MMM/yyyy");
                        txtref.Text = _ref;
                        txtremark.Text = _remark;
                        txttransfertype.Text = _TransferType;
                        INR_RES inr_res = new INR_RES();
                        var _filterItem = new List<InventoryRequestItem>();
                        if (type == "CUSR")
                        {
                            ddlRequestType.SelectedValue = subtype;
                            _Item = CHNLSVC.Inventory.GetMRN_Req_item(Reqno);
                            _filterItem = _Item.Where(x => x.Itri_bqty > 0).ToList();

                        }
                        else
                        {
                            DataTable dt = CHNLSVC.Sales.Check_INT_REQ_RER(Reqno, "RER");
                            if (dt.Rows.Count > 0)
                            {
                                string code = dt.Rows[0][28].ToString();
                                DataTable _type = CHNLSVC.Sales.Select_REF_REQ_SUBTP("RER");
                                DataRow[] result = _type.Select("RRS_TP = '" + code + "'");
                                foreach (DataRow row1 in result)
                                {
                                    txttransfertype.Text = row1[7].ToString();
                                }
                            }



                            inr_res = CHNLSVC.Sales.GetReservationApproval(Appno);
                            MasterLocation _MasterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), inr_res.IRS_ANAL_2);
                            if (_MasterLocation != null)
                            {
                                if (_MasterLocation.Ml_cate_1 != "DFS")
                                {
                                    DisplayMessage("Please check resrvation no", 4);
                                    return;
                                }
                            }


                            List<INR_RES_DET> inr_res_det = new List<INR_RES_DET>();
                            inr_res_det = CHNLSVC.Sales.GetGetReservationApprovalItem(inr_res.IRS_SEQ);
                            foreach (INR_RES_DET _det in inr_res_det)
                            {
                                InventoryRequestItem _req = new InventoryRequestItem();
                                _req.Itr_req_no = _det.IRD_RES_NO;
                                _req.Itri_itm_cd = _det.IRD_ITM_CD;
                                _req.Mi_longdesc = _det.MIS_DESC;
                                _req.Mst_item_model = _det.MI_MODEL;
                                _req.Itri_res_no = _det.IRD_RES_NO;
                                _req.Itri_qty = _det.IRD_RES_QTY;
                                _req.Itri_bqty = _det.IRD_RES_BQTY;
                                _req.Itri_line_no = _det.IRD_LINE;
                                _req.Itri_job_line = _det.IRD_SI_LINE;
                                _Item.Add(_req);
                            }
                            _filterItem = _Item.Where(x => x.Itri_bqty > 0).ToList();
                        }

                        if (HiddenFieldRequesttype.Value == "CUSR")
                        {

                            txtTobond_Bl.Text = inr_res.IRS_ANAL_1;
                            _filterItem.ForEach(x => x.Itri_base_req_no = Reqno);
                            foreach (InventoryRequestItem _it in _filterItem)
                            {
                                MasterItem _MstItem = new MasterItem();
                                _MstItem.Mi_cd = _it.Itri_itm_cd;
                                _it.MasterItem = _MstItem;
                            }
                            grdItem.DataSource = _filterItem;
                            grdItem.DataBind();
                            ViewState["_RequestItem"] = _filterItem;
                            pnlItemadd.Enabled = false;
                            lbtnDeleteItem.Enabled = false;
                            lbtnDeleteItem.OnClientClick = "return Enable();";
                            lbtnDeleteItem.CssClass = "buttoncolorleft";
                        }
                        else
                        {
                            txtaddbond.Text = inr_res.IRS_ANAL_1;
                            _filterItem.ForEach(x => x.Itri_base_req_no = Reqno);
                            grdrequestItem.DataSource = _filterItem;
                            grdrequestItem.DataBind();

                            grdItem.DataSource = new int[] { };
                            grdItem.DataBind();

                        }
                        ViewState["OldItem"] = _filterItem;
                        ViewState["OldItem_back"] = _filterItem;
                        ViewState["_PendingRequestItem"] = _filterItem;
                        row.BackColor = System.Drawing.Color.LightCyan;
                        pnlItemadd.Enabled = false;
                    }
                    else
                    {
                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                }
            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }
        protected void chk_ReqItem_CheckedChanged_Click(object sender, EventArgs e)
        {
            if (grdrequestItem.Rows.Count == 0) return;
            var checkbox = (CheckBox)sender;
            var row = (GridViewRow)checkbox.NamingContainer;
            if (row != null)
            {
                if (checkbox.Checked == true)
                {
                    lblres.Text = "";
                    txtbondqty.Text = string.Empty;
                    //txtaddbond.Text = string.Empty;
                    txtbondqty.Text = (row.FindControl("col_itri_qty") as Label).Text;
                    Session["item"] = (row.FindControl("col_ITRI_ITM_CD") as Label).Text;
                    lblres.Text = (row.FindControl("col_itri_res_no") as Label).Text;
                }
            }
        }

        protected void lbtnAddbond_Click(object sender, EventArgs e)
        {
            List<ImportsBLItems> BLItemsList = new List<ImportsBLItems>();
            List<InventoryRequestItem> _PendingItem = new List<InventoryRequestItem>();
            _PendingItem = ViewState["_PendingRequestItem"] as List<InventoryRequestItem>;
            _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;

            if (_RequestItem == null)
            {
                _RequestItem = new List<InventoryRequestItem>();
            }

            if (string.IsNullOrEmpty(txtaddbond.Text))
            {
                string _Msg = "Please enter To-bond or BL number";
                DisplayMessage(_Msg, 2);
                return;
            }
            if (string.IsNullOrEmpty(txtbondqty.Text))
            {
                string _Msg = "Please enter quantity";
                DisplayMessage(_Msg, 2);
                return;
            }
            if (grdrequestItem.Rows.Count == 0)
            {
                string _Msg = "No item found";
                DisplayMessage(_Msg, 2);
                return;
            }


            foreach (GridViewRow _row in grdrequestItem.Rows)
            {
                BLItemsList = ViewState["oImportsBLItems"] as List<ImportsBLItems>;
                if (BLItemsList == null)
                {
                    BLItemsList = new List<ImportsBLItems>();
                }

                CheckBox checkbox = _row.FindControl("chk_ReqItem") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string _Itemcode = (_row.FindControl("col_ITRI_ITM_CD") as Label).Text;
                    string _Itemlineno = (_row.FindControl("col_Ritri_line_no") as Label).Text;
                    string _resno = (_row.FindControl("col_itri_res_no") as Label).Text;
                    string _qtyb = (_row.FindControl("col_itri_bqty") as Label).Text;
                    string _jobline = (_row.FindControl("col_itri_job_line") as Label).Text;

                    decimal balanceqty = Convert.ToDecimal(_qtyb);
                    decimal typeqty = Convert.ToDecimal(txtbondqty.Text.ToString());
                    if (balanceqty < typeqty)
                    {
                        DisplayMessage("Cannot exceed balance quantity ", 2);
                        txtbondqty.Focus();
                        return;
                    }

                    string _qty = string.Empty;
                    if (txtbondqty.Text != null)
                    {
                        if (!IsNumeric(txtbondqty.Text.Trim(), NumberStyles.Float))
                        {
                            DisplayMessage("Please enter valid  quntity ", 2);
                            txtbondqty.Focus();
                            return;
                        }
                        // _Item.Itri_qty = Convert.ToDecimal(txtbondqty.Text);
                        _qty = txtbondqty.Text;
                    }
                    string _grnbaseitm = "";
                    string _grnitemcode = _Itemcode;
                    if (Session["GRN"] == null || Session["GRN"].ToString() == "")
                    {
                        // get base item GET_INR_BATCH_ITMforJob
                        _grnbaseitm = CHNLSVC.Inventory.GET_INR_BATCH_ITMforJob(txtaddbond.Text, Convert.ToInt32(_jobline), Session["UserCompanyCode"].ToString());
                    }
                    else
                    {
                        _grnbaseitm = CHNLSVC.Inventory.GET_INR_BATCH_ITM(Session["GRN"].ToString(), _Itemcode, Session["UserCompanyCode"].ToString());
                    }


                    if (!string.IsNullOrEmpty(_grnbaseitm))
                    {
                        _Itemcode = _grnbaseitm;
                    }
                    if (radpendingTobond.Checked == true)
                    {
                        // oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(txtaddbond.Text, null, _Itemcode);
                        oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm_new(txtaddbond.Text, null, _Itemcode, Convert.ToInt32(_jobline));
                    }
                    else if (radpendingBL.Checked == true)
                    {
                        // oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(null, txtaddbond.Text, _Itemcode);
                        oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm_new(null, txtaddbond.Text, _Itemcode, Convert.ToInt32(_jobline));
                    }
                    if ((oImportsBLItems != null))
                    {
                        InventoryRequestItem _Item = new InventoryRequestItem();
                        MasterItem _MstItem = new MasterItem();

                        #region Update BItable
                        bool _IsFindItem = false;
                        int rowcount = 0;
                        decimal _remqty = Convert.ToDecimal(_qty);
                        foreach (ImportsBLItems _BlIterm in oImportsBLItems)
                        {
                            ImportsBLItems _bl = new ImportsBLItems();
                            decimal qty = _BlIterm.Ibi_qty;
                            decimal reqqty = _BlIterm.Ibi_req_qty;
                            decimal total = qty - reqqty;

                            if (_IsFindItem == false)
                            {
                                /*Check Correct Line No for balance */
                                //DataTable cusitem = CHNLSVC.Inventory.GET_CUSDEC_ITEM(txtaddbond.Text, _BlIterm.Ibi_itm_cd);
                                //Int32 joblinenew=0;
                                //int k = 0;
                                //foreach(var dt in cusitem.Rows)
                                //{
                                //    if (Convert.ToInt32(cusitem.Rows[k][1].ToString()) >= typeqty)
                                //    {
                                //        joblinenew = Convert.ToInt32(cusitem.Rows[k][0].ToString());
                                //    }
                                //    k++;
                                //}

                                //_Item.Itri_job_line = _BlIterm.Ibi_line;
                                _Item.Itri_job_line = Convert.ToInt16(_jobline);
                                if (oImportsBLItems.Count == 1)
                                {
                                    if (total < _remqty)
                                    {
                                        if (!string.IsNullOrEmpty(lblres.Text))
                                        {
                                            _bl.Ibi_req_qty = 0;
                                            _bl.Ibi_seq_no = _BlIterm.Ibi_seq_no;
                                            _bl.Ibi_itm_cd = _BlIterm.Ibi_itm_cd;
                                            _bl.Ibi_line = _BlIterm.Ibi_line;
                                            BLItemsList.Add(_bl);
                                            ViewState["oImportsBLItems"] = BLItemsList;
                                            _IsFindItem = true;
                                        }
                                        else
                                        {
                                            DisplayMessage("Invalid quantity or added quantity exceeds the BL quantity ", 2);
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        _bl.Ibi_req_qty = _remqty;// _BlIterm.Ibi_req_qty + _remqty;
                                        _bl.Ibi_seq_no = _BlIterm.Ibi_seq_no;
                                        _bl.Ibi_itm_cd = _BlIterm.Ibi_itm_cd;
                                        _bl.Ibi_line = _BlIterm.Ibi_line;
                                        BLItemsList.Add(_bl);
                                        ViewState["oImportsBLItems"] = BLItemsList;
                                        _IsFindItem = true;
                                    }
                                }
                                else if (oImportsBLItems.Count > 1)
                                {
                                    if (total >= _remqty)
                                    {
                                        _bl.Ibi_req_qty = _BlIterm.Ibi_req_qty + _remqty;
                                        _bl.Ibi_seq_no = _BlIterm.Ibi_seq_no;
                                        _bl.Ibi_itm_cd = _BlIterm.Ibi_itm_cd;
                                        _bl.Ibi_line = _BlIterm.Ibi_line;
                                        BLItemsList.Add(_bl);
                                        ViewState["oImportsBLItems"] = BLItemsList;
                                        _IsFindItem = true;
                                    }
                                    else if (total < _remqty)
                                    {
                                        if (rowcount == oImportsBLItems.Count)
                                        {
                                            DisplayMessage("last test error", 2);
                                            return;
                                        }
                                        _remqty = _remqty - qty;
                                        _bl.Ibi_req_qty = _BlIterm.Ibi_req_qty + total;
                                        _bl.Ibi_seq_no = _BlIterm.Ibi_seq_no;
                                        _bl.Ibi_itm_cd = _BlIterm.Ibi_itm_cd;
                                        _bl.Ibi_line = _BlIterm.Ibi_line;
                                        BLItemsList.Add(_bl);
                                        ViewState["oImportsBLItems"] = BLItemsList;
                                    }

                                }


                            }
                            rowcount++;

                        }
                        #endregion

                        //_Item.Itr_req_no 
                        _Item.Itri_itm_cd = _Itemcode;
                        _Item.Itri_res_no = _resno;
                        _Item.Itri_itm_stus = "GOD";
                        _Item.Itri_qty = Convert.ToDecimal(_qty);
                        _Item.Itri_res_qty = Convert.ToDecimal(_qty);
                        _Item.Itri_line_no = Convert.ToInt32(_Itemlineno);
                        _Item.Itri_res_line = Convert.ToInt32(_Itemlineno);
                        _Item.Itri_com = Session["UserCompanyCode"].ToString();
                        _Item.Itri_old_itm_cd = _grnitemcode;
                        if (radpendingTobond.Checked == true)
                        {
                            _Item.To_bond = txtaddbond.Text;
                            _Item.ITRI_ITM_COND = "1";
                            _Item.Itri_job_no = txtaddbond.Text;
                        }
                        else
                        {
                            _Item.BL = txtaddbond.Text;
                            _Item.ITRI_ITM_COND = "0";
                            _Item.Itri_job_no = txtaddbond.Text;
                        }
                        _MstItem.Mi_cd = _Itemcode;
                        _Item.MasterItem = _MstItem;

                        //if (txtbondqty.Text != null)
                        //{
                        //    if (!IsNumeric(txtbondqty.Text.Trim(), NumberStyles.Float))
                        //    {
                        //        DisplayMessage("Please enter valid  quntity ", 2);
                        //        txtbondqty.Focus();
                        //        return;
                        //    }
                        //    _Item.Itri_qty = Convert.ToDecimal(txtbondqty.Text);
                        //}


                        _Item.Itri_bqty = _Item.Itri_qty;
                        _Item.Itri_job_line = Convert.ToInt16(_jobline);
                        var _checkItemlist = _PendingItem.SingleOrDefault(x => x.Itri_itm_cd == _Itemcode);
                        _PendingItem.Remove(_checkItemlist);
                        _RequestItem.Add(_Item);
                        pnlItemadd.Enabled = false;
                        // lbtnDeleteItem.Enabled = false;
                        //lbtnDeleteItem.OnClientClick = "return Enable();";
                        //lbtnDeleteItem.CssClass = "buttoncolorleft";





                        Session["GRN"] = "";
                    }
                    else
                    {
                        string _Msg = "Check the To-Bond number or BL number";
                        DisplayMessage(_Msg, 2);
                        return;
                    }
                }
            }


            grdItem.DataSource = _RequestItem;
            grdItem.DataBind();
            ViewState["_RequestItem"] = _RequestItem;

            grdrequestItem.DataSource = _PendingItem;
            grdrequestItem.DataBind();
            ViewState["_PendingRequestItem"] = _PendingItem;


        }

        protected void lbtnres_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                DisplayMessage("Pleas select a customer.", 2);
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusDecBondNo);
            DataTable oINR_RESs = CHNLSVC.Inventory.GETSEARCHRESERVATION(SearchParams, null, null);
            if (oINR_RESs != null && oINR_RESs.Rows.Count > 0)
            {
                grdResult.DataSource = null;
                grdResult.DataSource = oINR_RESs;
                grdResult.DataBind();
                BindUCtrlDDLData(oINR_RESs);
                lblvalue.Text = "Res";
                ViewState["SEARCH"] = oINR_RESs;
                UserPopoup.Show();
            }
            else
            {
                DisplayMessage("No records found.", 2);
            }
        }

        protected void lbtnsearchproftcenter_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Proft-center";
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnTobond_bl_Click(object sender, EventArgs e)
        {
            try
            {
                if ((rdotobound.Checked == false) && (rdBl.Checked == false))
                {
                    DisplayMessage("Please select To-Bond or BL number", 2);
                    return;
                }
                DateTime orddate = DateTime.Now;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, null, null);

                // if (_result.Rows.Count > 0)
                // {
                //     if (rdotobound.Checked)
                //     {
                //         if(!string.IsNullOrEmpty(txtItem.Text))
                //         {
                //         _result = _result.AsEnumerable()
                //.OrderByDescending(r => r.Field<String>("GRN NO"))
                //.CopyToDataTable();

                //         if (txtItem.Text.ToString() != "")
                //         {
                //             DataView dv = _result.DefaultView;
                //             dv.Sort = "GRN NO";
                //             _result = dv.ToTable();
                //         }
                //         _result.Columns.Remove("GRN NO");
                //         }
                //     }





                // }



                _result.Columns.Remove("ib_doc_rec_dt");
                ViewState["SEARCH"] = _result;
                grdResultRequest.DataSource = _result;
                grdResultRequest.DataBind();
                _result.Columns.Remove("BL qty");
                _result.Columns.Remove("BL #");
                //_result.Columns.Remove("File #");
                _result.Columns.Remove("TO-BOND");

                // _result.Columns.Remove("LC #");

                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    _result.Columns.Remove("Request Qty");
                    _result.Columns.Remove("Balance Qty");
                }

                BindUCtrlDDLDataRequest(_result);
                lblvalueRequest.Text = "Tobond_bl";

                UserPopoupRequest.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }


        private void loadItemStatus()
        {
            oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
        }
        protected void lbtnAddbondsearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchbyword.Text = "";
                if ((radpendingTobond.Checked == false) && (radpendingBL.Checked == false))
                {
                    DisplayMessage("Please select To-Bond or BL number", 2);
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl_2);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, null, null);
                _result.Columns.Add("Location");
                _result.Columns["Location"].SetOrdinal(6);
                foreach (DataRow dr in _result.Rows)
                {
                    string _loc = "";
                    if (!string.IsNullOrEmpty(dr["DOC #"].ToString()))
                    {
                        InventoryHeader _invHdr = CHNLSVC.Inventory.GET_INT_HDR_DATA(new InventoryHeader()
                        {
                            Ith_com = Session["UserCompanyCode"].ToString(),
                            Ith_loc = Session["UserDefLoca"].ToString(),
                            Ith_oth_docno = dr["DOC #"].ToString()
                        }).FirstOrDefault();
                        if (_invHdr != null)
                        {
                            _loc = _invHdr.Ith_loc;
                        }
                    }
                    dr["Location"] = _loc;
                }
                if (_result.Rows.Count > 0)
                {
                    DataView dv = _result.DefaultView;
                    dv.Sort = "ib_doc_rec_dt ASC";
                    _result = dv.ToTable();
                }
                _result.Columns.Remove("ib_doc_rec_dt");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                _result.Columns.Remove("BL qty");
                _result.Columns.Remove("BL Req qty");
                _result.Columns.Remove("File #");
                _result.Columns.Remove("DOC DATE");
                _result.Columns.Remove("LC #");
                _result.Columns.Remove("Location");
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Tobond_bl_2";
                ViewState["SEARCH"] = _result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnreqno_Click(object sender, EventArgs e)
        {
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestNo);
            //DataTable result = CHNLSVC.CommonSearch.Search_INT_REQ_RER(SearchParams, null, null);

            //grdResult.DataSource = result;
            //grdResult.DataBind();
            //BindUCtrlDDLData(result);
            //lblvalue.Text = "REQ_recoal";
            //UserPopoup.Show();

            //Modified by Kelum : 2016-June-15

            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
            //DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, null, null);
            //grdResult.DataSource = result;
            //grdResult.DataBind();
            //BindUCtrlDDLData(result);
            //lblvalue.Text = "REQ_recoal";
            //UserPopoup.Show();

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
            DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text).Date);
            grdResultRequest.DataSource = result;
            grdResultRequest.DataBind();
            BindUCtrlDDLDataRequest(result);
            lblvalueRequest.Text = "REQ_recoal";
            UserPopoupRequest.Show();

        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            if (txtCancelconformmessageValue.Value == "Yes")
            {
                Cancel();
            }
        }

        private void Cancel()
        {
            bool _Iscancel = false;
            List<ImportsBLItems> _bllist = new List<ImportsBLItems>();
            string _error = "";
            foreach (GridViewRow _row in grdItem.Rows)
            {
                // CheckBox checkbox = _row.FindControl("chk_Items") as CheckBox;
                // if (checkbox.Checked == true)
                // {

                string _Itemcode = (_row.FindControl("col_Itri_itm_cd") as Label).Text;
                string _resno = (_row.FindControl("col_itri_itm_cond") as Label).Text;
                string _BL = (_row.FindControl("col_BL") as Label).Text;
                string _tobond = (_row.FindControl("col_tobond") as Label).Text;
                string _line = (_row.FindControl("col_itri_line_no") as Label).Text;
                string _qty = (_row.FindControl("col_Itri_qty") as Label).Text;
                string _lineno = (_row.FindControl("col_itri_job_line") as Label).Text;
                if (_resno == "1")
                {
                    oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm_latest(_tobond, null, Convert.ToInt32(_lineno));

                }
                else if (_resno == "0")
                {
                    oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm_latest(null, _BL, Convert.ToInt32(_lineno));
                    if (oImportsBLItems == null)
                    {
                        oImportsBLItems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm_latest(_BL, null, Convert.ToInt32(_lineno));
                    }

                }

                if (oImportsBLItems != null)
                {
                    foreach (ImportsBLItems _itm in oImportsBLItems)
                    {
                        if ((_itm.Ibi_line.ToString() == _lineno))
                        {
                            decimal _updateqty = _itm.Ibi_req_qty - Convert.ToDecimal(_qty);
                            if (_updateqty < 0)
                            {
                                _updateqty = 0;
                            }
                            if (_tobond == "") _tobond = _BL;
                            _itm._spc_itm = _Itemcode;
                            _itm._spc_line = Convert.ToInt32(_lineno);
                            _itm._spc_cancel_qty = Convert.ToDecimal(_qty);
                            _itm._spc_update_qty = _updateqty;
                            _itm._spc_req_no = txtRequest.Text;
                            _itm._spc_tobond = _tobond;
                            _bllist.Add(_itm);

                            // CHNLSVC.Inventory.Update_BI_Rqty(_itm.Ibi_seq_no, _Itemcode, Convert.ToInt32(_lineno), _cancelqty, txtRequest.Text, 0, Session["UserCompanyCode"].ToString(), _itm.Ibi_doc_no, out _error);
                            _Iscancel = true;
                        }
                    }
                }

                // }
            }

            Int32 _result = CHNLSVC.Inventory.Update_BI_RqtyNew(_bllist, Session["UserCompanyCode"].ToString(), out _error);

            if (_Iscancel == false)
            {
                string _Msg = "Please select Item to cancel";
                DisplayMessage(_Msg, 2);
                return;
            }
            else
            {
                string _Msg = "Successfully cancelled";
                DisplayMessage(_Msg, 3);
                pageClear();
            }
        }

        protected void radpendingreq_CheckedChanged(object sender, EventArgs e)
        {
            if (radpendingreq.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16025))
                {
                    pnlToBod.Visible = false;
                    pnltoboneName.Enabled = false;
                    pnltobonetxtbox.Enabled = false;
                    pnlPermision.Enabled = false;
                    pnlres.Enabled = false;
                    HiddenFieldRequesttype.Value = "CUSR";
                    GetRequesttype();
                    grdrequest.DataSource = new int[] { };
                    grdrequest.DataBind();
                    grdrequestItem.DataSource = new int[] { };
                    grdrequestItem.DataBind();
                    grdItem.DataSource = new int[] { };
                    grdItem.DataBind();
                    // ViewState["_RequestItem"] ="";
                    // ViewState["oImportsBLItems"] = "";
                    txtRequest.Text = string.Empty;
                    txtremark.Text = string.Empty;
                    txtTobond.Text = string.Empty;
                    txtgoodgivenDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    txtDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    lblCustomerName.Text = string.Empty;
                    txtCustomer.Text = string.Empty;
                    txtplocation.Text = string.Empty;
                    txttransfertype.Text = string.Empty;
                    txtref.Text = string.Empty;
                }
                else
                {
                    pnlToBod.Visible = true;
                    pnltoboneName.Enabled = true;
                    pnltobonetxtbox.Enabled = true;
                    pnlPermision.Enabled = true;
                    pnlres.Enabled = true;
                    HiddenFieldRequesttype.Value = "CUSA";
                    GetRequesttype();
                    grdrequest.DataSource = new int[] { };
                    grdrequest.DataBind();
                    grdrequestItem.DataSource = new int[] { };
                    grdrequestItem.DataBind();
                    grdItem.DataSource = new int[] { };
                    grdItem.DataBind();
                    //  ViewState["_RequestItem"] = "";
                    //  ViewState["oImportsBLItems"] = "";
                    txtRequest.Text = string.Empty;
                    txtremark.Text = string.Empty;
                    txtTobond.Text = string.Empty;
                    txtgoodgivenDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    txtDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    lblCustomerName.Text = string.Empty;
                    txtCustomer.Text = string.Empty;
                    txtplocation.Text = string.Empty;
                    txttransfertype.Text = string.Empty;
                    txtref.Text = string.Empty;
                }
            }
        }

        protected void radpendigres_CheckedChanged(object sender, EventArgs e)
        {
            if (radpendigres.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16025))
                {
                    pnlToBod.Visible = false;
                    pnltoboneName.Enabled = false;
                    pnltobonetxtbox.Enabled = false;
                    pnlPermision.Enabled = true;
                    pnlres.Enabled = false;
                    HiddenFieldRequesttype.Value = "CUSR";
                    GetRequesttype();
                }
                else
                {
                    pnlToBod.Visible = true;
                    pnltoboneName.Enabled = true;
                    pnltobonetxtbox.Enabled = true;
                    pnlPermision.Enabled = true;
                    pnlres.Enabled = true;
                    HiddenFieldRequesttype.Value = "CUSA";
                    GetRequesttype();
                }
            }
        }

        protected void rdotobound_CheckedChanged(object sender, EventArgs e)
        {
            txtTobond_Bl.Text = string.Empty;
        }

        protected void rdBl_CheckedChanged(object sender, EventArgs e)
        {
            txtTobond_Bl.Text = string.Empty;
        }

        protected void radpendingTobond_CheckedChanged(object sender, EventArgs e)
        {
            txtaddbond.Text = string.Empty;
        }

        protected void radpendingBL_CheckedChanged(object sender, EventArgs e)
        {
            txtaddbond.Text = string.Empty;
        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
            DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, "Code", txtCustomer.Text);
            if (result.Rows.Count == 0)
            {
                string _Msg = "Invalid customer";
                DisplayMessage(_Msg, 2);
                txtCustomer.Text = string.Empty;
            }
        }

        protected void txtplocation_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtplocation.Text);
            if (result.Rows.Count == 0)
            {
                string _Msg = "Please enter a correct preferred location";
                DisplayMessage(_Msg, 2);
                txtplocation.Text = string.Empty;
            }
        }
        protected void txtissueloc_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtissueloc.Text);
            if (result.Rows.Count == 0)
            {
                string _Msg = "Please enter a correct issue location";
                DisplayMessage(_Msg, 2);
                txtissueloc.Text = string.Empty;
            }
        }

        protected void txtSearchprofitcenter_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtSearchprofitcenter.Text);
            if (_result.Rows.Count == 0)
            {
                string _Msg = "Please enter a correct preferred profit center";
                DisplayMessage(_Msg, 2);
                txtSearchprofitcenter.Text = string.Empty;
            }
        }

        protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
            DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, "Code", txtSearchCustomer.Text);
            if (result.Rows.Count == 0)
            {
                string _Msg = "Invalid customer";
                DisplayMessage(_Msg, 2);
                txtSearchCustomer.Text = string.Empty;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            MasterItem _item = new MasterItem();
            _item = CHNLSVC.General.GetItemMaster(txtItem.Text);
            if (_item == null)
            {
                DisplayMessage("Please check the item code", 2);
                return;
            }

        }

        protected void lbtnblItem_Click(object sender, EventArgs e)
        {
            bool usetobond = (bool)Session["usetobond"];
            if (usetobond == false)
            {
                if (rdotobound.Checked)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                    DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, "TO-BOND", txtTobond_Bl.Text);
                    if (_result.Rows.Count > 0)
                    {
                        Session["BL"] = _result.Rows[0][0].ToString();
                        if (_result.Rows.Count > 1)
                        {
                            foreach (DataRow _row in _result.Rows)
                            {
                                if (_row["TO-BOND"].ToString() == txtTobond_Bl.Text)
                                {
                                    Session["BL"] = _row["DOC #"].ToString();
                                }
                            }
                        }
                    }
                }
                else if (rdBl.Checked)
                {
                    Session["BL"] = txtTobond_Bl.Text;
                }
            }
            else
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, "TO-BOND", txtTobond_Bl.Text);
                if (_result.Rows.Count > 0)
                {
                    Session["BL"] = _result.Rows[0][0].ToString();
                }
            }


            if ((rdotobound.Checked) || (rdBl.Checked))
            {
                ImportsBLHeader oHeader = new ImportsBLHeader();

                oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), Session["BL"].ToString(), "A");
                if (oHeader != null)
                {
                    List<ImportsBLItems> oImportsBLItems = CHNLSVC.Financial.GET_BL_ITMS_BY_SEQ(oHeader.Ib_seq_no);
                    foreach (ImportsBLItems _ITM in oImportsBLItems)
                    {
                        decimal balance = _ITM.Ibi_bal_qty - _ITM.Ibi_req_qty;
                        _ITM.Ibi_bl_qty_temp = _ITM.Ibi_qty;
                        _ITM.Ibi_bal_qty = balance;
                        _ITM.Ibi_qty = _ITM.Ibi_bal_qty;

                    }
                    var _filter = oImportsBLItems.Where(x => x.Ibi_bal_qty != 0).ToList();
                    grdBlItem.DataSource = _filter;
                    grdBlItem.DataBind();
                    PopupBLItems.Show();
                    Session["OImportsBLItems"] = _filter;
                }
                else
                {
                    DisplayMessage("Please check the item BL/TO-Bond #", 2);
                    return;
                }
            }
            else
            {
                DisplayMessage("Please check the item BL/TO-Bond #", 2);
                return;
            }


        }

        protected void btnBlItem_Click(object sender, EventArgs e)
        {
            _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
            bool checkSelected = false;
            if (_RequestItem == null)
            {
                _RequestItem = new List<InventoryRequestItem>();
            }
            foreach (GridViewRow dgvr in grdBlItem.Rows)
            {
                string _item = (dgvr.FindControl("col_ibi_itm_cd") as Label).Text;
                string _qty = (dgvr.FindControl("col_invRevQty") as TextBox).Text;
                string _balnce = (dgvr.FindControl("col_ibi_qty") as Label).Text;
                string _line = (dgvr.FindControl("col_ibi_line") as Label).Text;
                //checkSelected = false;
                CheckBox chk = (CheckBox)dgvr.FindControl("chk_BlItem");
                if (chk.Checked)
                {
                    checkSelected = true;
                    if (Convert.ToDecimal(_balnce) < Convert.ToDecimal(_qty))
                    {

                        DisplayMessage("Please check the item qty - '" + _item + "'", 2);
                        PopupBLItems.Show();
                        return;
                    }

                    oImportsBLItems = ViewState["oImportsBLItems"] as List<ImportsBLItems>;
                    if (oImportsBLItems == null)
                    {
                        oImportsBLItems = new List<ImportsBLItems>();
                    }
                    InventoryRequestItem _Item = new InventoryRequestItem();
                    MasterItem _MstItem = new MasterItem();
                    //_Item.Itr_req_no 
                    _Item.Itri_itm_cd = _item;
                    _Item.Itri_res_no = null;
                    _Item.Itri_itm_stus = "GOD";
                    _Item.Itri_qty = Convert.ToDecimal(_qty);
                    _Item.Itri_bqty = Convert.ToDecimal(_qty);
                    _MstItem.Mi_cd = _item;
                    _Item.MasterItem = _MstItem;

                    if (rdotobound.Checked == true)
                    {
                        _Item.To_bond = txtTobond_Bl.Text;
                        _Item.ITRI_ITM_COND = "1";
                        _Item.Itri_job_no = txtTobond_Bl.Text;

                    }
                    else if (rdBl.Checked == true)
                    {
                        _Item.BL = txtTobond_Bl.Text;
                        _Item.ITRI_ITM_COND = "0";
                        _Item.Itri_job_no = txtTobond_Bl.Text;
                    }
                    if (Hiddenstatus.Value != "")
                    {
                        if (oMasterItemStatuss != null)
                        {
                            if (oMasterItemStatuss.Count > 0)
                            {
                                MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_desc == Hiddenstatus.Value);
                                if (oStatus != null)
                                {
                                    _Item.Itri_mitm_stus = oStatus.Mis_cd;
                                    _Item.Itri_itm_stus = oStatus.Mis_cd;
                                }
                            }
                        }

                    }
                    var item = _RequestItem.Where(x => x.Itri_itm_cd == _item && x.Itri_line_no == Convert.ToInt32(_line)).ToList();

                    if (item.Count > 0)
                    {
                        DisplayMessage("Item is already added", 2);
                        return;
                    }
                    else if (item.Count == 0)
                    {
                        _Item.Itri_line_no = 1;
                    }
                    else
                    {
                        int maxline = item.Max(t => t.Itri_line_no);
                        _Item.Itri_line_no = maxline + 1;

                    }

                    if (txtTobond_Bl.Text != "")
                    {
                        List<ImportsBLItems> oImportsBLItems_1 = new List<ImportsBLItems>();
                        // oImportsBLItems.Clear();
                        if (rdotobound.Checked == true)
                        {
                            oImportsBLItems_1 = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(txtTobond_Bl.Text, null, _item);
                        }
                        else if (rdBl.Checked == true)
                        {
                            oImportsBLItems_1 = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(null, txtTobond_Bl.Text, _item);
                        }
                        bool usetobond = (bool)Session["usetobond"];
                        if (usetobond)
                        {
                            oImportsBLItems_1 = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(txtTobond_Bl.Text, null, _item);
                        }
                        if (oImportsBLItems_1 != null)
                        {
                            oImportsBLItems_1 = oImportsBLItems_1.Where(x => x.Ibi_line == Convert.ToInt32(_line)).ToList();
                            int rowcount = 0;
                            decimal _remqty = Convert.ToDecimal(_qty);
                            foreach (ImportsBLItems _BlIterm in oImportsBLItems_1)
                            {
                                decimal qty = _BlIterm.Ibi_qty;
                                decimal reqqty = _BlIterm.Ibi_req_qty;
                                decimal total = qty - reqqty;
                                _Item.Itri_job_line = _BlIterm.Ibi_line;
                                _BlIterm.Ibi_req_qty = _remqty;//_BlIterm.Ibi_req_qty + _remqty;  
                            }
                        }
                        if (oImportsBLItems_1 != null)
                        {
                            oImportsBLItems = oImportsBLItems.Concat(oImportsBLItems_1).ToList();
                            ViewState["oImportsBLItems"] = oImportsBLItems;
                        }

                    }
                    if (CHNLSVC.Financial.isstatusvalidation(Session["UserCompanyCode"].ToString(), _Item.Itri_itm_cd, _Item.Itri_itm_stus, _Item.Itri_job_no, _Item.Itri_qty) == false)
                    {
                        DisplayMessage("Already GRN  " + _Item.Itri_itm_cd, 2);
                        return;
                    }


                    _RequestItem.Add(_Item);


                }

            }            
            if (grdBlItem.Rows.Count<1)
            {
                DisplayMessage("No item(s) to add", 2);
                PopupBLItems.Show();

            }
            else
            {
                if (!checkSelected)
                {
                    DisplayMessage("Please select a item(s)", 2);
                    PopupBLItems.Show();
                }
            }
           
            oImportsBLItems = ViewState["oImportsBLItems"] as List<ImportsBLItems>;
            grdItem.DataSource = _RequestItem;
            grdItem.DataBind();
            ViewState["_RequestItem"] = _RequestItem;
            ViewState["oImportsBLItems"] = oImportsBLItems;
            Session["type"] = "CUSA_BLITEM";
        }

        protected void btnBlItemUpdate_Click(object sender, EventArgs e)
        {
            List<ImportsBLItems> importsBLItemList = new List<ImportsBLItems>();
            if (Session["OImportsBLItems"] != null)
            {
                importsBLItemList = Session["OImportsBLItems"] as List<ImportsBLItems>;
            }

            _RequestItem = ViewState["_RequestItem"] as List<InventoryRequestItem>;
            if (_RequestItem == null)
            {
                _RequestItem = new List<InventoryRequestItem>();
            }
            List<ImportsBLItems> updatedList = new List<ImportsBLItems>();
            foreach (GridViewRow dgvr in grdBlItem.Rows)
            {
                string _item = (dgvr.FindControl("col_ibi_itm_cd") as Label).Text;
                string _qty = (dgvr.FindControl("col_invRevQty") as TextBox).Text;
                string _balnce = (dgvr.FindControl("col_ibi_qty") as Label).Text;
                string _line = (dgvr.FindControl("col_ibi_line") as Label).Text;
                string _remarks = (dgvr.FindControl("col_invRemarks") as TextBox).Text;
                bool changeRemarks = true;
                ImportsBLItems blItem=importsBLItemList.Where(x => x.Ibi_itm_cd.Equals(_item) && x.Ibi_line == Convert.ToInt32(_line)).First();
                if (blItem != null)
                {
                    if (blItem.ibi_remarks.Equals(_remarks))
                    {
                        changeRemarks = false;
                    }
                    CheckBox chk = (CheckBox)dgvr.FindControl("chk_BlItem");
                    if (chk.Checked && changeRemarks)
                    {
                        ImportsBLItems importsBLItem = new ImportsBLItems();
                        importsBLItem.Ibi_doc_no = blItem.Ibi_doc_no;
                        importsBLItem.Ibi_itm_cd = _item;
                        importsBLItem.Ibi_line = Convert.ToInt32(_line);
                        importsBLItem.ibi_remarks = _remarks;
                        updatedList.Add(importsBLItem);
                        // int effect = CHNLSVC.Financial.UpdateBLItmRemarks(txtTobond_Bl.Text, _item, Convert.ToInt32(_line), _remarks);
                    }
                    if (changeRemarks && !chk.Checked)
                    {
                        DisplayMessage("Please select update item(s)", 2);
                        break;
                    }
                }
            }
            if(updatedList.Count>0)
            {
                int effect = 0;
                foreach (var blItm in updatedList)
                {
                    effect = CHNLSVC.Financial.UpdateBLItmRemarks(blItm.Ibi_doc_no, blItm.Ibi_itm_cd, blItm.Ibi_line, blItm.ibi_remarks);
                    if(effect<1)
                    {
                        DisplayMessage("Items not updated", 2);
                    }
                }
                if (effect > 0)
                {
                    DisplayMessage("Successfully Updated!!!", 3);
                }
            }
            else
            {
               DisplayMessage("Items not updated", 2);
            }

        }




        protected void chk_Items_Click(object sender, EventArgs e)
        {
            try
            {
                List<InventoryRequestItem> _Item = new List<InventoryRequestItem>();
                List<InventoryRequest> _hdd = new List<InventoryRequest>();
                if (grdItem.Rows.Count == 0) return;
                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {
                        string Itmcode = (row.FindControl("col_Itri_itm_cd") as Label).Text;
                        string reno = (row.FindControl("col_Itri_res_no") as Label).Text;
                        string tobone = (row.FindControl("col_tobond") as Label).Text;
                        string bl = (row.FindControl("col_BL") as Label).Text;
                        string qty = (row.FindControl("col_Itri_qty") as Label).Text;
                        txtItem.Text = Itmcode;
                        txtRes.Text = reno;
                        if (bl == "")
                        {
                            txtTobond_Bl.Text = tobone;
                        }
                        else { txtTobond_Bl.Text = bl; }
                        txtqty.Text = qty;
                        //txtItem.Enabled = false;
                        //txtRes.Enabled = false;
                        //txtTobond_Bl.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtRequest_TextChanged(object sender, EventArgs e)
        {
            RecallDeatils();
            if (lblStatus.Text == "CANCELLED")
            {
                lbtnCancel.Enabled = false;
                lbtnCancel.OnClientClick = "return Enable();";
                lbtnCancel.CssClass = "buttoncolorleft";

                lbtnupdate.Enabled = false;
                lbtnupdate.OnClientClick = "return Enable();";
                lbtnupdate.CssClass = "buttoncolorleft";
            }
            else if (lblStatus.Text == "PENDING")
            {
                lbtnCancel.Enabled = false;
                lbtnCancel.OnClientClick = "return Enable();";
                lbtnCancel.CssClass = "buttoncolorleft";

                lbtnupdate.Enabled = true;
                lbtnupdate.OnClientClick = "CancelConfirm();";
                lbtnupdate.CssClass = "buttonUndocolor";
            }
            else if (lblStatus.Text == "COMPLETED")
            {
                lblStatus.Text = "COMPLETED";
                lbtnCancel.Enabled = false;
                lbtnCancel.OnClientClick = "return Enable();";
                lbtnCancel.CssClass = "buttoncolorleft";

                lbtnupdate.Enabled = false;
                lbtnupdate.OnClientClick = "return Enable();";
                lbtnupdate.CssClass = "buttoncolorleft";
            }
            else
            {
                lbtnCancel.Enabled = true;
                lbtnCancel.OnClientClick = "CancelConfirm();";
                lbtnCancel.CssClass = "buttonUndocolor";

                lbtnupdate.Enabled = true;
                lbtnupdate.OnClientClick = "CancelConfirm();";
                lbtnupdate.CssClass = "buttonUndocolor";
            }
        }

        //search by FROM and TO dates : kelum : 2016-June-15
        protected void grdResultRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordRequest.ClientID + "').value = '';", true);
            string ID = grdResultRequest.SelectedRow.Cells[1].Text;
            if (lblvalueRequest.Text == "REQ_recoal")
            {
                string _status = grdResultRequest.SelectedRow.Cells[3].Text;
                string refno = grdResultRequest.SelectedRow.Cells[2].Text;
                if ((refno != "") && (refno != "&nbsp;"))
                {
                    txtref.Text = refno;
                }
                txtRequest.Text = ID;
                lblvalueRequest.Text = "";
                UserPopoupRequest.Hide();
                RecallDeatils();
                lblStatus.Text = _status;
                lbtnDeleteItem.Visible = false;
                return;
            }
            if (lblvalueRequest.Text == "Tobond_bl")
            {

                HiddenFieldBQty.Value = grdResultRequest.SelectedRow.Cells[10].Text;
                HiddenFieldReqQty.Value = grdResultRequest.SelectedRow.Cells[9].Text;
                //, grdResultRequest.SelectedRow.Cells[14].Text.ToString()
                if (rdotobound.Checked == true)
                {

                    string Des = grdResultRequest.SelectedRow.Cells[3].Text;
                    int cellcount = grdResultRequest.SelectedRow.Cells.Count;
                    if (cellcount < 14)
                    {
                        if (checkToBond_grn(Des, txtItem.Text, ""))
                        {
                            txtTobond_Bl.Text = Des;
                            string Bl = grdResultRequest.SelectedRow.Cells[1].Text;
                            Session["BL"] = Bl;

                        }
                        else
                        {
                            //DisplayMessage("Please GRN this ToBond", 1);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('This document still not GRN');", true);
                            txtTobond_Bl.Text = "";
                            Session["BL"] = null;
                            return;
                        }
                    }
                    else
                    {
                        string status = grdResultRequest.SelectedRow.Cells[14].Text;
                        if (checkToBond_grn(Des, txtItem.Text, status))
                        {
                            txtTobond_Bl.Text = Des;
                            string Bl = grdResultRequest.SelectedRow.Cells[1].Text;
                            Session["BL"] = Bl;

                        }
                        else
                        {
                            //DisplayMessage("Please GRN this ToBond", 1);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Wrong Item Status or not GRN');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Wrong Item Status or No Free GRN Qty available!');", true);
                            return;
                        }
                    }




                }
                else if (rdBl.Checked == true)
                {
                    string Des = grdResultRequest.SelectedRow.Cells[1].Text;

                    txtTobond_Bl.Text = Des;
                    CheckBlTobond(Des);
                    Session["BL"] = Des;
                }


                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }

        }

        protected void grdResultRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultRequest.PageIndex = e.NewPageIndex;

            if (lblvalueRequest.Text == "REQ_recoal")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultRequest.DataSource = result;
                grdResultRequest.DataBind();
                grdResultRequest.PageIndex = 0;
                UserPopoupRequest.Show();
            }
            if (lblvalue.Text == "Tobond_bl")
            {
                grdResultRequest.PageIndex = e.NewPageIndex;
                grdResultRequest.DataSource = null;
                grdResultRequest.DataSource = (DataTable)ViewState["SEARCH"];
                grdResultRequest.DataBind();
                UserPopoupRequest.Show();
                txtSearchbywordRequest.Focus();
            }
        }

        protected void lbtnSearchRequest_Click(object sender, EventArgs e)
        {
            if (lblvalueRequest.Text == "REQ_recoal")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, ddlSearchbykeyRequest.SelectedItem.ToString(), "%" + txtSearchbywordRequest.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultRequest.DataSource = result;
                grdResultRequest.DataBind();
                UserPopoupRequest.Show();
                return;
            }
            if (lblvalueRequest.Text == "Tobond_bl")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_Date(SearchParams, ddlSearchbykeyRequest.SelectedItem.ToString(), "%" + txtSearchbywordRequest.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (_result.Rows.Count > 0)
                {
                    DataView dv = _result.DefaultView;
                    dv.Sort = "ib_doc_rec_dt ASC";
                    _result = dv.ToTable();
                }

                _result.Columns.Remove("ib_doc_rec_dt");
                grdResultRequest.DataSource = _result;
                grdResultRequest.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoupRequest.Show();
                return;
            }
        }

        protected void txtSearchbywordRequest_TextChanged(object sender, EventArgs e)
        {
            if (lblvalueRequest.Text == "REQ_recoal")
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, ddlSearchbykeyRequest.SelectedItem.ToString(), txtSearchbywordRequest.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultRequest.DataSource = result;
                grdResultRequest.DataBind();
                UserPopoupRequest.Show();
                return;
            }
            if (lblvalue.Text == "Tobond_bl")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_Date(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (_result.Rows.Count > 0)
                {
                    DataView dv = _result.DefaultView;
                    dv.Sort = "ib_doc_rec_dt ASC";
                    _result = dv.ToTable();
                }

                _result.Columns.Remove("ib_doc_rec_dt");
                grdResultRequest.DataSource = _result;
                grdResultRequest.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoupRequest.Show();
                return;
            }
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalueRequest.Text == "Tobond_bl")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_Date(SearchParams, ddlSearchbykeyRequest.SelectedItem.ToString(), "%" + txtSearchbywordRequest.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                if (_result.Rows.Count > 0)
                {
                    DataView dv = _result.DefaultView;
                    dv.Sort = "ib_doc_rec_dt ASC";
                    _result = dv.ToTable();
                }

                _result.Columns.Remove("ib_doc_rec_dt");
                grdResultRequest.DataSource = _result;
                grdResultRequest.DataBind();
                ViewState["SEARCH"] = _result;
                UserPopoupRequest.Show();
                return;
            }
            else
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable result = CHNLSVC.Inventory.GetSearchRequest(SearchParams, ddlSearchbykeyRequest.SelectedItem.Text, txtSearchbywordRequest.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultRequest.DataSource = result;
                grdResultRequest.DataBind();
                lblvalueRequest.Text = "REQ_recoal";
                ViewState["SEARCH"] = result;
                UserPopoupRequest.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordRequest.Focus();
            }
        }

        public void BindUCtrlDDLDataRequest(DataTable _dataSource)
        {
            this.ddlSearchbykeyRequest.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyRequest.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyRequest.SelectedIndex = 0;
        }


        protected void grdGRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordRequest.ClientID + "').value = '';", true);
            string ID = grdGRN.SelectedRow.Cells[1].Text;
            HiddenGRNloc.Value = ID;
            HiddenItem.Value = grdGRN.SelectedRow.Cells[4].Text;
            Hiddenstatus.Value = grdGRN.SelectedRow.Cells[6].Text;
        }

        protected void txtTobond_Bl_TextChanged(object sender, EventArgs e)
        {
            //Dulaj 2018/Dec/28 Check Pending To Bond
            if (rdotobound.Checked)
            {
                
                DataTable _tbl = CHNLSVC.Financial.GetCusdechdrByBond(Session["UserCompanyCode"].ToString(), txtTobond_Bl.Text);
                {
                    if (_tbl != null)
                    {
                        if (_tbl.Rows.Count < 1)
                        {
                            txtTobond_Bl.Text = "";
                            DisplayMessage("To Bond Number is Invalid", 2);
                            return;
                        }
                    }
                }
            }
            //
            if ((rdotobound.Checked == false) && (rdBl.Checked == false))
            {
                DisplayMessage("Please select To-Bond or BL number", 2);
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
            DataTable _result = new DataTable();
            if (rdotobound.Checked)
            {
                _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, "TO-BOND", txtTobond_Bl.Text);
            }
            else if (rdBl.Checked)
            {
                _result = CHNLSVC.Inventory.GET_GetTobond_BL(SearchParams, "DOC #", txtTobond_Bl.Text);
            }
            if (_result.Rows.Count > 0)
            {
                if ((rdotobound.Checked == true))
                {
                    string Des = _result.Rows[0][2].ToString();
                    //14
                    if (checkToBond_grn(Des, txtItem.Text, ""))
                    {
                        txtTobond_Bl.Text = Des;
                        string Bl = _result.Rows[0][0].ToString();
                        Session["BL"] = Bl;

                    }
                    else
                    {
                        txtTobond_Bl.Text = "";
                        Session["BL"] = "";
                        DisplayMessage("Please GRN thi To-Bond", 2);
                    }
                }
                else
                {
                    CheckBlTobond(txtTobond_Bl.Text);
                }
            }
            else
            {
                DisplayMessage("Please select valid document", 2);
            }
        }

        private void CheckBlTobond(string _bldoc)
        {
            DataTable _tbl = CHNLSVC.Financial.GetCusdechdrByBL(Session["UserCompanyCode"].ToString(), _bldoc);

            var zeroDuties = _tbl.AsEnumerable()
                .Where(x => x.Field<String>("cuh_tp") == "TO")
                .ToList();

            if (zeroDuties.Count > 0)
            {
                var rowColl = zeroDuties.AsEnumerable();
                string name = (from r in rowColl
                               where r.Field<String>("cuh_tp") == "TO"
                               select r.Field<string>("cuh_doc_no")).First<string>();
                txtTobond_Bl.Text = name;
                if (!string.IsNullOrEmpty(name))
                {
                    Session["usetobond"] = true;
                }

            }
        }



    }
}