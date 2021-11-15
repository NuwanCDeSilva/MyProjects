using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Inventory
{
    public partial class Customs_Entry_Progress_Viewer : BasePage
    {
        MasterItem _mstItem = new MasterItem();
        public List<MasterStatus> _mstStsList
        {
            get
            {
                if (Session["_mstStsList"] != null)
                {
                    return (List<MasterStatus>)Session["_mstStsList"];
                }
                else
                {
                    return new List<MasterStatus>();
                }
            }
            set
            {
                Session["_mstStsList"] = value;
            }
        }

        string _showPop
        {
            get { if (Session["_showPop"] != null) { return (string)Session["_showPop"]; } else { return ""; } }
            set { Session["_showPop"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _mstStsList = CHNLSVC.General.GET_MST_STUS(new MasterStatus() { });
                pageClear();
                _showPop = "Hide";
               
            }
            else
            {
                txtReqFoDate.Text = Request[txtReqFoDate.UniqueID];
                txtReqToDate.Text = Request[txtReqToDate.UniqueID];
                txtExpectedFrom.Text = Request[txtExpectedFrom.UniqueID];
                txtExpectedTo.Text = Request[txtExpectedTo.UniqueID];

                if (_showPop == "Show")
                {
                    GRNUserPopoup.Show();
                }
                else
                {
                    GRNUserPopoup.Hide();
                }
            

            }
        }



        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GRNUserPopoup.Hide();
            grnItemDetail.DataSource = new int[] { };
            grnItemDetail.DataBind();
            _showPop = "Hide";
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
        private void pageClear()
        {
            grdDeatails.DataSource = new int[] { };
            grdDeatails.DataBind();
            gridDispach.DataSource = new int[] { };
            gridDispach.DataBind();
            grdreceipt.DataSource = new int[] { };
            grdreceipt.DataBind();
            
            grdGrnItemHdr.DataSource = new int[] { };
            grdGrnItemHdr.DataBind();

            grnItemDetail.DataSource = new int[] { };
            grnItemDetail.DataBind();

            txtCustomer.Text = string.Empty;
            txtProcenter.Text = string.Empty;
            DateTime date = DateTime.Now;
            DateTime fromdt = DateTime.Now.AddMonths(-1);
            txtReqFoDate.Text = date.AddDays(-7).ToString("dd/MMM/yyyy");
            txtReqToDate.Text = date.ToString("dd/MMM/yyyy");
           txtExpectedFrom.Text = DateTime.Today.AddDays(-7).ToString("dd/MMM/yyyy");
           txtExpectedTo.Text = date.ToString("dd/MMM/yyyy");
            txtloc.Text = string.Empty;
        }

        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Customer")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtCustomer.Text = ID;
                txtCustomer.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Proft-center")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtProcenter.Text = ID;
                txtProcenter.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtloc.Text = ID;
                txtloc.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtItem.Text = ID;
                txtItem.ToolTip = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();

                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                string Name = grdResult.SelectedRow.Cells[2].Text;
                txtCat1.Text = ID;
                txtCat1.ToolTip = Name;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Entry")
            {
                txtEntryno.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
               UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Proft-center")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Entry")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                DataTable _result = CHNLSVC.CommonSearch.SearchCusdec(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Proft-center")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Entry")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                DataTable _result = CHNLSVC.CommonSearch.SearchCusdec(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
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
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Proft-center")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Prefer_Loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Entry")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                DataTable _result = CHNLSVC.CommonSearch.SearchCusdec(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtSearchbyword.Text = "";
                UserPopoup.Show();
                return;
            }
        }
        #endregion
        #region Common Searching Area
        private void DisplayMessage(String Msg, Int32 option)
        {
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CusdecEntries:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
                txtSearchbyword.Text = "";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, "Code", txtCustomer.Text);
                if ((result != null)&&(result.Rows.Count>0))
                {
                    if (txtCustomer.Text != "")
                    {
                        txtCustomer.Text = result.Rows[0][0].ToString();
                        txtCustomer.ToolTip = result.Rows[0][1].ToString();
                    }
                  
                }
                else
                {
                    txtCustomer.Text = "";
                    string _Msg = "Invalid customer";
                    DisplayMessage(_Msg, 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnProcenter_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Proft-center";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtProcenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "CODE", txtProcenter.Text);
                if ((_result != null) && (_result.Rows.Count > 0))
                {
                    if (txtProcenter.Text!=""){
                         txtProcenter.Text = _result.Rows[0][0].ToString();
                    txtProcenter.ToolTip = _result.Rows[0][1].ToString();
                    }
                   
                }
                else
                {
                    txtProcenter.Text = string.Empty;
                    string _Msg = "Invalid profit center";
                    DisplayMessage(_Msg, 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search Profit Center";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Prefer_Loc";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtloc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtloc.Text);
                if ((_result != null) && (_result.Rows.Count > 0))
                {
                    if (txtloc.Text != "")
                    {
                        txtloc.Text = _result.Rows[0][0].ToString();
                        txtloc.ToolTip = _result.Rows[0][1].ToString();
                    }
                    
                }
                else
                {
                    txtloc.Text = string.Empty;
                    string _Msg = "Invalid location";
                    DisplayMessage(_Msg, 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnItem_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "Item";
            txtSearchbyword.Text = "";
            UserPopoup.Show();
            return;
        }

        protected void txtitem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, "Item", txtItem.Text);
                if ((_result != null) && (_result.Rows.Count > 0))
                {
                    if (txtItem.Text != "")
                    {
                        txtItem.Text = _result.Rows[0][0].ToString();
                        txtItem.ToolTip = _result.Rows[0][1].ToString();
                    }
                }
                else
                {
                    txtItem.Text = string.Empty;
                    string _Msg = "Invalid item";
                    DisplayMessage(_Msg, 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnCat1_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat1";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

            var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat1.Text.Trim()).ToList();
            if (_validate == null || _validate.Count <= 0)
            {
                DisplayMessage("Invalid category", 2);
                txtCat1.Text = string.Empty;
                txtCat1.Focus();
                return;
            }
        }

       
        protected void lbtn_recqty_Click(object sender, EventArgs e)
        {
            grdentrydetails.DataSource = new int[] { };
            grdentrydetails.DataBind();

            if (grdDeatails.Rows.Count == 0) return;
             var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _EntryNo = (row.FindControl("col_entryno") as Label).Text;
                DataTable _Entrynotable = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_ENTRYNO_DO_NEW(_EntryNo, 0,1);
                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (oItemStaus != null && oItemStaus.Count > 0)
                {
                    foreach (DataRow _row in _Entrynotable.Rows)
                    {
                        _row[5] = oItemStaus.Find(x => x.Mis_cd == _row[5].ToString()).Mis_desc;
                    }
                    
                }
                txtEntryno.Text = _EntryNo.ToString();
                // cusdec entry item and balance 
                DataTable entryitembal = CHNLSVC.Inventory.GetEntryProgDetails(_EntryNo.ToString());
                //grdentrydetails
                grdentrydetails.DataSource = entryitembal;
                grdentrydetails.DataBind();

                grdreceipt.DataSource = _Entrynotable;
                grdreceipt.DataBind();
                BindRecieptModel();


            }
        }
        protected void lbtndisqty_Click(object sender, EventArgs e)
        {
            if (grdDeatails.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _EntryNo = (row.FindControl("col_entryno") as Label).Text;
                DataTable _Entryno;
                //RSG2 added by Wimal @ 07/09/2018
                if (_EntryNo.Contains("RAB") || _EntryNo.Contains("R201") || _EntryNo.Contains("RSG2") || _EntryNo.Contains("RE2"))
                {
                     _Entryno = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_ENTRYNO_DO_NEW(_EntryNo, 0, 99);
                }
                else
                {
                     _Entryno = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_ENTRYNO_DO_NEW(_EntryNo, 0, 0);
                }
             
                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (oItemStaus != null && oItemStaus.Count > 0)
                {
                    foreach (DataRow _row in _Entryno.Rows)
                    {
                        _row[5] = oItemStaus.Find(x => x.Mis_cd == _row[5].ToString()).Mis_desc;
                    }

                }
                gridDispach.DataSource = _Entryno;
                gridDispach.DataBind();
                BindDispatchModel();
            }
        }

        protected void lbtnSerchoption1_Click(object sender, EventArgs e)
        {
            try
            {

                grdentrydetails.DataSource = new int[] { };
                grdentrydetails.DataBind();
                grdDeatails.DataSource = new int[] { };
                grdDeatails.DataBind();
                ViewState["SearchReult"] = null;
                gridDispach.DataSource = new Int32[] { };
                gridDispach.DataBind();

                grdreceipt.DataSource = new Int32[] { };
                grdreceipt.DataBind();
                if (txtCustomer.Text != "")
                {
                    DataTable _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(txtCustomer.Text, null, -1, Convert.ToDateTime(txtReqFoDate.Text),
                        Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), null, null, null, null);
                    DataView view = _ProgressResult.DefaultView;
                    view.Sort = "Req_App_Date DESC";
                    DataTable sortedDate = view.ToTable();

                    if (sortedDate.Rows.Count > 0)
                    {

                        grdDeatails.DataSource = BindStatus(sortedDate);
                        grdDeatails.DataBind();
                        ViewState["SearchReult"] = null;
                        ViewState["SearchReult"] = sortedDate;
                    }
                    else
                    {
                        DisplayMessage("No data found !!!", 2);
                    }
                }
                else if (txtEntryno.Text != "")
                {
                    DataTable _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(null, txtEntryno.Text, -1, Convert.ToDateTime(txtReqFoDate.Text),
                        Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), null, null, null, null);

                    DataView view = _ProgressResult.DefaultView;
                    view.Sort = "Req_App_Date DESC";
                    DataTable sortedDate = view.ToTable();
                    if (sortedDate.Rows.Count > 0)
                    {
                       
                        grdDeatails.DataSource = BindStatus(sortedDate);
                        grdDeatails.DataBind();
                        ViewState["SearchReult"] = null;
                        ViewState["SearchReult"] = sortedDate;

                        //if (sortedDate.Rows[0]["IS_VALUE_FOR_GRN"].ToString().Length <= 0)
                        //{

                        //    LinkButton btnView = FindControl("lbtnLoadGRN") as LinkButton;
                        //    lbtnLoadGRN.Enabled = false;
                        //}


                    }
                    else
                    {
                        DisplayMessage("No data found !!!", 2);
                    }
                }
                else
                {
                    DisplayMessage("Please enter entry number or customer number", 2);

                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtnSerchoption2_Click(object sender, EventArgs e)
        {
            try
            {
                grdentrydetails.DataSource = new int[] { };
                grdentrydetails.DataBind();
                grdDeatails.DataSource = new int[] { };
                grdDeatails.DataBind();
                gridDispach.DataSource = new Int32[] { };
                gridDispach.DataBind();

                grdreceipt.DataSource = new Int32[] { };
                grdreceipt.DataBind();

                ViewState["SearchReult"] = null;
                DataTable _ProgressResult = new DataTable();
                if (chkbonddate.Checked)
                {
                    _ProgressResult = CHNLSVC.Inventory.CusEnqEntryData(Convert.ToDateTime(txtReqFoDate.Text),Convert.ToDateTime(txtReqToDate.Text));
                }
                else
                {
                    _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(null, null, 1, Convert.ToDateTime(txtReqFoDate.Text),
                            Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), null, null, null, null);

                }


                DataView view = _ProgressResult.DefaultView;

               

                view.Sort = "Req_App_Date DESC";
                DataTable sortedDate = view.ToTable();
                if (sortedDate.Rows.Count > 0)
                {
                    
                    grdDeatails.DataSource = BindStatus(sortedDate);
                    grdDeatails.DataBind();
                    ViewState["SearchReult"] = null;
                    ViewState["SearchReult"] = sortedDate;
                }
                else
                {
                    DisplayMessage("No data found !!!", 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnSerchoption3_Click(object sender, EventArgs e)
        {
            try
            {
                grdentrydetails.DataSource = new int[] { };
                grdentrydetails.DataBind();
                grdDeatails.DataSource = new int[] { };
                grdDeatails.DataBind();
                ViewState["SearchReult"] = null;
                gridDispach.DataSource = new Int32[] { };
                gridDispach.DataBind();

                grdreceipt.DataSource = new Int32[] { };
                grdreceipt.DataBind();
                DataTable _ProgressResult = new DataTable();

                if (!chkentry.Checked)
                {
                    _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(null, null, 2, Convert.ToDateTime(txtReqFoDate.Text),
                          Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), null, null, null, null);
                }
                else
                {
                    _ProgressResult = CHNLSVC.Inventory.CusEnqCusdecData(Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text));
                }
                DataView view = _ProgressResult.DefaultView;
                view.Sort = "Req_App_No DESC";
                DataTable sortedDate = view.ToTable();

                if (sortedDate.Rows.Count > 0)
                {

                    grdDeatails.DataSource = BindStatus(sortedDate);
                    grdDeatails.DataBind();
                    ViewState["SearchReult"] = null;
                    ViewState["SearchReult"] = sortedDate;
                }
                else
                {
                    DisplayMessage("No data found !!!", 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtnSerchoption4_Click(object sender, EventArgs e)
        {
             try
            {
                grdentrydetails.DataSource = new int[] { };
                grdentrydetails.DataBind();
                grdDeatails.DataSource = new int[] { };
                grdDeatails.DataBind();
                ViewState["SearchReult"] = null;

                gridDispach.DataSource = new Int32[] { };
                gridDispach.DataBind();

                grdreceipt.DataSource = new Int32[] { };
                grdreceipt.DataBind();
            if (txtProcenter.Text != "")
            {
                DataTable _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(null, null, -1, Convert.ToDateTime(txtReqFoDate.Text),
            Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), txtProcenter.Text, null, null, null);
                DataView view = _ProgressResult.DefaultView;
                view.Sort = "Req_App_date DESC";
                DataTable sortedDate = view.ToTable();
                if (sortedDate.Rows.Count > 0)
                {

                    grdDeatails.DataSource = BindStatus(sortedDate);
                    grdDeatails.DataBind();
                    ViewState["SearchReult"] = null;
                    ViewState["SearchReult"] = sortedDate;
                }
                else
                {
                    DisplayMessage("No data found !!!", 2);
                }
            }
            else
            {
                DataTable _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(null, null, -1, Convert.ToDateTime(txtReqFoDate.Text),
            Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), null, txtloc.Text, null,null);
                DataView view = _ProgressResult.DefaultView;
                view.Sort = "Req_App_No DESC";
                DataTable sortedDate = view.ToTable();
                if (sortedDate.Rows.Count > 0)
                {

                    grdDeatails.DataSource = BindStatus(sortedDate);
                    grdDeatails.DataBind();
                    ViewState["SearchReult"] = null;
                    ViewState["SearchReult"] = sortedDate;
                }
                else
                {
                    DisplayMessage("No data found !!!", 2);
                }
            }
            }
             catch (Exception ex)
             {
                 string _Msg = "Error Occurred while processing..!";
                 DisplayMessage(_Msg, 4);
             }

        }

        protected void lbtnEntry_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                DataTable _result = CHNLSVC.CommonSearch.SearchCusdec(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Entry";
                txtSearchbyword.Text = "";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnSerchoption6_Click(object sender, EventArgs e)
        {
            try
            {
                grdDeatails.DataSource = new int[] { };
                grdDeatails.DataBind();
                ViewState["SearchReult"] = null;

                gridDispach.DataSource = new Int32[] { };
                gridDispach.DataBind();

                grdreceipt.DataSource = new Int32[] { };
                grdreceipt.DataBind();

                string status = ddlStatus.SelectedItem.Text == "Pending" ? "P" : null;
                DataTable _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(null, null, 1, Convert.ToDateTime(txtReqFoDate.Text),
                        Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), null, null, null, null, status);
                if (ddlStatus.Text == "All")
                {
                    DataView view = _ProgressResult.DefaultView;
                    view.Sort = "Req_date DESC";
                    DataTable sortedDate = view.ToTable();
                    if (sortedDate.Rows.Count > 0)
                    {

                        grdDeatails.DataSource = BindStatus(sortedDate);
                        grdDeatails.DataBind();
                        ViewState["SearchReult"] = null;
                        ViewState["SearchReult"] = sortedDate;
                    }
                    else
                    {
                        DisplayMessage("No data found !!!", 2);
                    }
                }
                else
                {
                    DataRow[] result = _ProgressResult.Select("Entry_No is null");
                    if (result.Length > 0)
                    {

                        DataTable dt1 = result.CopyToDataTable();
                        DataView view = dt1.DefaultView;
                        view.Sort = "Req_date ASC";
                        DataTable sortedDate = view.ToTable();

                        if (sortedDate.Rows.Count > 0)
                        {

                            grdDeatails.DataSource = BindStatus(sortedDate);
                            grdDeatails.DataBind();
                            ViewState["SearchReult"] = null;
                            ViewState["SearchReult"] = sortedDate;
                        }
                        else
                        {
                            DisplayMessage("No data found !!! ", 2);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }



        }

        protected void grdDeatails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDeatails.PageIndex = e.NewPageIndex;
            DataTable _result = ViewState["SearchReult"] as DataTable;
            grdDeatails.DataSource = _result;
            grdDeatails.DataBind();
        }

        protected void lbtnSerchoption5_Click(object sender, EventArgs e)
        {
            grdentrydetails.DataSource = new int[] { };
            grdentrydetails.DataBind();
            if (txtItem.Text != "")
            {
                string status = ddlStatus.SelectedItem.Text == "Pending" ? "P" : null;
                DataTable _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(null, null, -1, Convert.ToDateTime(txtReqFoDate.Text),
                Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), null, null, txtItem.Text, null, null);
                DataView view = _ProgressResult.DefaultView;
                view.Sort = "Req_App_Date,Entry_Date DESC";
                DataTable sortedDate = view.ToTable();
                if (sortedDate.Rows.Count > 0)
                {

                    grdDeatails.DataSource = BindStatus(sortedDate);
                    grdDeatails.DataBind();
                    ViewState["SearchReult"] = null;
                    ViewState["SearchReult"] = sortedDate;
                }
                else
                {
                    DisplayMessage("No data found !!! ", 2);
                }
            }
            else
            {
                string status = ddlStatus.SelectedItem.Text == "Pending" ? "P" : null;
                DataTable _ProgressResult = CHNLSVC.Inventory.GETREQ_TRACKER_DATA_BY_CUSTEMER(null, null, -1, Convert.ToDateTime(txtReqFoDate.Text),
               Convert.ToDateTime(txtReqToDate.Text), Convert.ToDateTime(txtExpectedFrom.Text), Convert.ToDateTime(txtExpectedTo.Text), null, null, null,txtCat1.Text,null);
                DataView view = _ProgressResult.DefaultView;
                view.Sort = "Req_App_Date,Entry_Date DESC";
                DataTable sortedDate = view.ToTable();

                if (sortedDate.Rows.Count > 0)
                {

                    grdDeatails.DataSource = BindStatus(sortedDate);
                    grdDeatails.DataBind();
                    ViewState["SearchReult"] = null;
                    ViewState["SearchReult"] = sortedDate;
                }
                else
                {
                    DisplayMessage("No data found !!! ", 2);
                }
            }
         
        }

        protected void txtEntryno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                DataTable _result = CHNLSVC.CommonSearch.SearchCusdec(SearchParams, "DOC NO", txtEntryno.Text);
                if ((_result != null) && (_result.Rows.Count > 0))
                {
                    if (txtEntryno.Text != "")
                    {
                        txtEntryno.Text = _result.Rows[0][0].ToString();
                        txtEntryno.ToolTip = _result.Rows[0][1].ToString();
                    }

                }
                else
                {
                    txtEntryno.Text = string.Empty;
                    string _Msg = "Invalid entry number";
                    DisplayMessage(_Msg, 2);
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search Profit Center";
                DisplayMessage(_Msg, 4);
            }
        }

        private DataTable BindStatus(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (_mstStsList != null)
                {
                    if (_mstStsList.Count > 0)
                    {
                        MasterStatus _mstSts = _mstStsList.Find(x => x.Mss_cd == item["ReqStatus"].ToString());
                        if (_mstSts!=null)
                        {
                            item["ReqStatus"] = _mstSts.Mss_desc;
                        }
                    }
                }
            }
            return dt;
        }
        private void BindDispatchModel()
        {
            foreach (GridViewRow item in gridDispach.Rows)
            {
                Label lbliti_Model = item.FindControl("lbliti_Model") as Label;
                Label lbliti_itm_cd = item.FindControl("lbliti_itm_cd") as Label;
                Label lblUom = item.FindControl("lblUom") as Label;
                _mstItem = CHNLSVC.General.GetItemMaster(lbliti_itm_cd.Text);
                if (_mstItem!=null)
                {
                    lbliti_Model.Text = _mstItem.Mi_model;
                    lblUom.Text = string.IsNullOrEmpty(_mstItem.Mi_itm_uom) ? "N/A" : _mstItem.Mi_itm_uom== "NULL"?"N/A": _mstItem.Mi_itm_uom;
                }
            }
        }
        private void BindRecieptModel()
        {
            foreach (GridViewRow item in grdreceipt.Rows)
            {
                Label lbliti_Model = item.FindControl("lbliti_Model") as Label;
                Label lbliti_itm_cd = item.FindControl("lbliti_itm_cd") as Label;
                Label lblUom = item.FindControl("lblUom") as Label;
                _mstItem = CHNLSVC.General.GetItemMaster(lbliti_itm_cd.Text);
                if (_mstItem != null)
                {
                    lbliti_Model.Text = _mstItem.Mi_model;
                    lblUom.Text = string.IsNullOrEmpty(_mstItem.Mi_itm_uom) ? "N/A" : _mstItem.Mi_itm_uom == "NULL" ? "N/A" : _mstItem.Mi_itm_uom;
                }
            }
        }

        protected void lbtnLoadGrid_Click(object sender, EventArgs e)
        {
            lbtn_recqty_Click(sender, e);
            lbtndisqty_Click(sender, e);
        }

        protected void lbtnLoadGRN_Click(object sender, EventArgs e)
        {
            _showPop = "Show";



            LinkButton btn = (LinkButton)(sender);
            string _reqNo = btn.CommandArgument;
            DataTable _result = CHNLSVC.Inventory.GetGRNDetailsByReqNo(_reqNo);


            grdGrnItemHdr.DataSource = _result;
            grdGrnItemHdr.DataBind();

            GRNUserPopoup.Show();
            
        }

        protected void grdDeatails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdDeatails, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[0].Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void grdGrnItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void chk_selected_grn_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox chk = (CheckBox)sender;
            //GridViewRow gr = (GridViewRow)chk.Parent.Parent;
            //lblmsg.Text = GridView1.DataKeys[gr.RowIndex].Value.ToString();

            ////lblmsg.Text = "Hello";

            var chk = (CheckBox)sender;
            var _seqNo = chk.Attributes["ITB_SEQ_NO"];


        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

            string seq_numbers = "";
            foreach (GridViewRow Item in grdGrnItemHdr.Rows)
            {
                Label lblLocation = (Label)Item.FindControl("lblITH_SEQ_NO");
                CheckBox chkGrn = (CheckBox)Item.FindControl("CheckBox1");

                if (chkGrn.Checked)
                {
                    string seq = lblLocation.Text;
                    if (seq_numbers != "")
                    {
                        seq_numbers = seq_numbers + "," + seq;
                    }
                    else
                    {
                        seq_numbers = seq;
                    }
                }
            }


            DataTable _result = CHNLSVC.Inventory.GetGRNItemsDetailsBySeqNo(seq_numbers);

            grnItemDetail.DataSource = _result;
            grnItemDetail.DataBind();

            _showPop = "Show";

            //string seq_numbers = "";
            //foreach (GridViewRow row in grdGrnItem.Rows)
            //{
            //    CheckBox chk = row.Cells[0].Controls[0] as CheckBox;
            //    if (chk != null && chk.Checked)
            //    {
            //        var _seqNo = chk.Attributes["CommandArgument"];
            //        if (seq_numbers != "")
            //        {
            //            seq_numbers = seq_numbers + "," + _seqNo;
            //        }
            //        else
            //        {
            //            seq_numbers = _seqNo;
            //        }
            //    }
            //}

           // var chk = (CheckBox)sender;
           // var _seqNo = chk.Attributes["CommandArgument"];

            //GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            //int index = row.RowIndex;
            //CheckBox cb1 = (CheckBox)grdGrnItem.Rows[index].FindControl("CheckBox1");
            //string checkboxstatus;
            //if (cb1.Checked == true)
            //    checkboxstatus = "YES";
            //else if (cb1.Checked == false)
            //    checkboxstatus = "NO";

            //Here Write the code to connect to your database and update the status by 
            //sending the checkboxstatus as variable and update in the database.
        }

        protected void lbtngrnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itm = (row.FindControl("lbliti_itm_cd") as Label).Text;
                    string entry = txtEntryno.Text.ToString();
                    string aodno = (row.FindControl("lblith_doc_no") as Label).Text;
                    DataTable grnnumbers = CHNLSVC.Inventory.GetEntryProgGRN(entry, _itm,aodno);
                    int i = 0;
                    string grnno = "";
                    if (grnnumbers.Rows.Count>0)
                    {
                        foreach (var grn in grnnumbers.Rows)
                        {
                            if (grnnumbers.Rows[i]["inb_doc_no"].ToString().Contains("GRN"))
                            {
                                grnno = grnno + grnnumbers.Rows[i]["inb_doc_no"].ToString() + " ,";
                            }
                           
                            i++;
                        }
                    }

                   // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + grnno + "')", true);
                    lb.ToolTip = grnno;
                }
            }catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('"+ex.Message+"')", true);
            }
        
        }
    }
}