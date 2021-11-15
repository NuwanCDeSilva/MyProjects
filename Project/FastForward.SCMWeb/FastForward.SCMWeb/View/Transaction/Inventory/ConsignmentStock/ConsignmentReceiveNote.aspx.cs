using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory.Consignment_Stock
{
    public partial class ConsignmentReceiveNote : BasePage
    {
        public string _mainModuleName
        {
            get { if (Session["_mainModuleName"] != null) { return (string)Session["_mainModuleName"]; } else { return ""; } }
            set { Session["_mainModuleName"] = value; }
        }
        private List<InvoiceItem> invoice_items = null;
        private List<InvoiceItem> invoice_items_bind = null;

        private List<PurchaseOrderDelivery> podel_items = null;
        protected int user_seq_num { get { return (int)Session["user_seq_num"]; } set { Session["user_seq_num"] = value; } }
        private string _profitCenter = "";
        private bool IsGrn = false;
        private List<InventoryRequestItem> ScanItemList = null;
        string _userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    dvPendingPO.DataSource = new int[] { };
                    dvPendingPO.DataBind();
                    user_seq_num = -1;
                    grdItems.DataSource = new int[] { };
                    grdItems.DataBind();

                    grdSerial.DataSource = new int[] { };
                    grdSerial.DataBind();

                    DateTime fromdt = DateTime.Now.AddMonths(-1);
                    dtpFromDate.Text = fromdt.ToString("dd/MMM/yyyy");

                    DateTime todate = DateTime.Now;
                    dtpToDate.Text = todate.ToString("dd/MMM/yyyy");

                    DateTime dodate = DateTime.Now;
                    dtpDODate.Text = dodate.ToString("dd/MMM/yyyy");

                    DateTime podate = DateTime.Now;
                    dtpPODate.Text = podate.ToString("dd/MMM/yyyy");

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16024))
                    {
                        lbtncancel.Enabled = false;
                        lbtncancel.CssClass = "buttoncolor";
                        lbtncancel.OnClientClick = "return Enable();";
                    }
                    else
                    {
                        lbtncancel.Enabled = true;
                        lbtncancel.CssClass = "buttonUndocolor";
                        lbtncancel.OnClientClick = "ConfirmCancel();";
                    }
                    this.btnGetPO_Click(null, null);
                    _mainModuleName = "StockADJ";
                    ucInScan.adjustmentTypeValue = "+";
                    ucInScan.doc_tp = "ADJ";
                    ucInScan.adjustmentType = "ADJ";
                    MpDelivery.Hide();
                    Session["DocType"] = null;
                    //added by nuwan for uc item clear option
                    Session["CLEARITEM"] = "True";

                    DataTable dtchk_warehouse_availability = CHNLSVC.Inventory.CheckWareHouseAvailability(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                    if (dtchk_warehouse_availability.Rows.Count > 0)
                    {
                        chkpda.Enabled = true;

                        foreach (DataRow ddrware in dtchk_warehouse_availability.Rows)
                        {
                            Session["WAREHOUSE_COM"] = ddrware["ml_wh_com"].ToString();
                            Session["WAREHOUSE_LOC"] = ddrware["ml_wh_cd"].ToString();
                        }
                    }
                    else
                    {
                        chkpda.Enabled = false;
                    }

                    PopulateLoadingBays();

                    DataTable dtpda = CHNLSVC.Inventory.CheckIsPDALoc(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (dtpda.Rows.Count > 0)
                    {
                        foreach (DataRow ddrpda in dtpda.Rows)
                        {
                            Session["PDA_LOCATION"] = ddrpda["ML_IS_PDA"].ToString();
                            if (ddrpda["ML_IS_PDA"].ToString() == "1")
                            {
                                chkpda.Enabled = true;
                            }
                            else
                            {
                                chkpda.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    string popup = (string)Session["POPUP"];
                    if (!string.IsNullOrEmpty(popup))
                    {
                        ucInScan.GrnNoRequired = true;
                        MpDelivery.Show();
                    }

                    string docsession = (string)Session["Doc"];
                    string tempdocsession = (string)Session["TempDoc"];

                    if (!string.IsNullOrEmpty(docsession))
                    {
                        if (Session["Doc"].ToString() == "true")
                        {
                            UserDPopoup.Show();
                            MpDelivery.Hide();
                        } 
                    }

                    if (!string.IsNullOrEmpty(tempdocsession))
                    {
                        if (Session["TempDoc"].ToString() == "true")
                        {
                            UserDPopoup.Show();
                            MpDelivery.Hide();
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void PopulateLoadingBays()
        {
            try
            {
                DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");

                if (dtbays.Rows.Count > 0)
                {
                    ddlloadingbay.DataSource = dtbays;
                    ddlloadingbay.DataTextField = "mws_res_name";
                    ddlloadingbay.DataValueField = "mws_res_cd";
                    ddlloadingbay.DataBind();
                }

                ddlloadingbay.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region Clear Screen

        private void ClearScreen()
        {
            try
            {
                DateTime fromdt = DateTime.Now.AddMonths(-1);
                dtpFromDate.Text = fromdt.ToString("dd/MMM/yyyy");

                DateTime todate = DateTime.Now;
                dtpToDate.Text = todate.ToString("dd/MMM/yyyy");

                DateTime dodate = DateTime.Now;
                dtpDODate.Text = dodate.ToString("dd/MMM/yyyy");

                DateTime podate = DateTime.Now;
                dtpPODate.Text = podate.ToString("dd/MMM/yyyy");

                GetPendingPurchaseOrders(dtpFromDate.Text, dtpToDate.Text, txtFindSupplier.Text, txtFindPONo.Text, false);
                chkManualRef.Checked = false;

                lblBackDateInfor.Text = string.Empty;
                txtPONo.Text = string.Empty;
                txtPORefNo.Text = string.Empty;

                txtSuppCode.Text = string.Empty;
                txtSuppName.Text = string.Empty;
                txtFindSupplier.Text = string.Empty;
                txtFindPONo.Text = string.Empty;
                txtVehicleNo.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtFindPONoDoc.Text = string.Empty;

                List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
                _emptyserList = null;
                grdSerial.AutoGenerateColumns = false;
                grdSerial.DataSource = new int[]{};
                grdSerial.DataBind();

                List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
                _emptyinvoiceitemList = null;
                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = new int[] { }; 
                grdItems.DataBind();

                dvPendingPO.DataSource = null;
                grdItems.DataBind();

                chkManualRef.Checked = false;

                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_ConsStock_StockRec", dtpDODate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);

                txtManualRefNo.Text = string.Empty;
                Session["DocType"] = null;
                Session["SERIALLIST"] = null;
                Session["LOADEDSEQNO"] = null;
                Session["POPUP"] = null;
                Session["STATUS"] = null;
                Session["PO_NO"] = null;
                Session["Doc"] = null;
                Session["TempDoc"] = null;
                Session["SENT_TO_CRN"] = null;

                LinkButton2.Enabled = true;
                LinkButton2.CssClass = "buttonUndocolor";
                LinkButton2.OnClientClick = "ConfirmPlaceOrder();";

                lbtnsave.Enabled = true;
                lbtnsave.CssClass = "buttonUndocolor";
                lbtnsave.OnClientClick = "ConfirmPlaceOrder();";

                lbtncancel.Enabled = true;
                lbtncancel.CssClass = "buttonUndocolor";
                lbtncancel.OnClientClick = "ConfirmCancel();";

                ucInScan.ListClear();
                ucInScan.PageClear();
                ucInScan.PageDataClear();
                Session["POPUPSERIALS"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, TextBox _dtpcontrol, Label _lblcontrol, string _date, out bool _allowCurrentDate)
        {
            BackDates _bdt = new BackDates();
            _dtpcontrol.Enabled = false;
            _allowCurrentDate = false;

            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
            if (_isAllow == true)
            {
                _dtpcontrol.Enabled = true;
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
                    Information.Visible = true;
                }
            }

            if (_bdt == null)
            {
                _allowCurrentDate = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentDate = true;
                }
            }

            CheckSessionIsExpired();
            return _isAllow;
        }

        public void CheckSessionIsExpired()
        {
            if (Session["UserID"].ToString() != "ADMIN")
            {
                string _expMsg = "Current Session is expired or has been closed by administrator!";
                if (CHNLSVC.Security.IsSessionExpired(Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(),out _expMsg) == true)
                {
                    BaseCls.GlbIsExit = true;
                    GC.Collect();
                }
            }
        }

        private void ClearBody()
        {
            chkManualRef.Checked = false;
            txtPONo.Text = string.Empty;

            DateTime fromdt = DateTime.Now.AddMonths(-1);
            dtpFromDate.Text = fromdt.ToString("dd/MMM/yyyy");

            DateTime todate = DateTime.Now;
            dtpToDate.Text = todate.ToString("dd/MMM/yyyy");

            DateTime dodate = DateTime.Now;
            dtpDODate.Text = dodate.ToString("dd/MMM/yyyy");

            DateTime podate = DateTime.Now;
            dtpPODate.Text = podate.ToString("dd/MMM/yyyy");

            txtPORefNo.Text = string.Empty;

            txtSuppCode.Text = string.Empty;
            txtSuppName.Text = string.Empty;
            txtFindSupplier.Text = string.Empty;
            txtFindPONo.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtFindPONoDoc.Text = string.Empty;

            List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
            _emptyserList = null;
            grdSerial.AutoGenerateColumns = false;
            grdSerial.DataSource = new int[]{};
            grdSerial.DataBind();

            List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
            _emptyinvoiceitemList = null;
            grdItems.AutoGenerateColumns = false;
            grdItems.DataSource = new int[]{};
            grdItems.DataBind();
        }

        #endregion

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "16")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "16";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "424")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POrderFastWeb);

                    string columnname = ddlSearchbykey.SelectedItem.Text;

                    if (columnname == "PO No")
                    {
                        columnname = "DOC NO";
                    }
                    else if (columnname == "Ref No")
                    {
                        columnname = "DOC REF";
                    }

                    DataTable result = CHNLSVC.CommonSearch.SearchPurchaseOrdersFast(SearchParams, columnname, txtSearchbyword.Text);

                    DataTable dtresultcopyMRN = new DataTable();
                    dtresultcopyMRN.Columns.AddRange(new DataColumn[4] { new DataColumn("PO No"), new DataColumn("Ref No"), new DataColumn("Date"), new DataColumn("Status")});

                    foreach (DataRow ddr in result.Rows)
                    {
                        string doc = ddr["PO No"].ToString();
                        string subtp = ddr["Ref No"].ToString();
                        string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                        string stus = ddr["Status"].ToString();

                        dtresultcopyMRN.Rows.Add(doc, subtp, date,stus);
                    }

                    grdResult.DataSource = dtresultcopyMRN;
                    grdResult.DataBind();
                    lblvalue.Text = "424";
                    ViewState["SEARCH"] = dtresultcopyMRN;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);
                    string searchParameter = ddlSearchbykey.Text;

                    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                    result = dv.ToTable();
                    
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POrder:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator + "A");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POrderFastWeb:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator + "A");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "ADJ" + seperator + "1" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }


        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                if (lblvalue.Text == "16")
                {
                    txtFindSupplier.Text = grdResult.SelectedRow.Cells[1].Text;

                    if (string.IsNullOrEmpty(txtFindSupplier.Text)) return;

                    if (!CHNLSVC.Inventory.IsValidSupplier(Session["UserCompanyCode"].ToString(), txtFindSupplier.Text, 1, "S"))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid supplier !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtFindSupplier.Text = "";
                        txtFindSupplier.Focus();
                        return;
                    }
                }
                else if (lblvalue.Text == "424")
                {
                    txtFindPONo.Text = grdResult.SelectedRow.Cells[1].Text;

                    if (string.IsNullOrEmpty(txtFindPONo.Text)) return;

                    PurchaseOrder _hdr = CHNLSVC.Inventory.GetPOHeader(Session["UserCompanyCode"].ToString(), txtFindPONo.Text.Trim(), "L");
                    if (_hdr == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid consignment request no !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtFindPONo.Text = string.Empty;
                        txtFindPONo.Focus();
                        return;
                    }
                    else
                    {
                        if (_hdr.Poh_sub_tp != "C")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid consignment request no !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                            txtFindPONo.Text = string.Empty;
                            txtFindPONo.Focus();
                            return;
                        }
                        if (_hdr.Poh_stus == "P")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Request has not approved yet !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                            return;
                        }
                        if (_hdr.Poh_stus == "F")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Request has completed !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                            return;
                        }
                        if (_hdr.Poh_stus == "C")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This is a cancelled request !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                            return;
                        }

                        this.btnGetPO_Click(null, null);
                    }
                }
                
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
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

        public void BindUCtrlDDLDataPOREQUEST(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if ((col.ColumnName != "Date")&&(col.ColumnName != "Status"))
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName); 
                }
                
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        protected void btnSearch_Supplier_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                UserDPopoup.Hide();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            grdResultD.DataSource = _result;
            grdResultD.DataBind();
            UserDPopoup.Show();
        }

        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                string soundExCode = SoundEx("CONS");

                EnumerableRowCollection<DataRow> query = from contact in _result.AsEnumerable()
                                                         where SoundEx(contact.Field<string>("Sub Type")) == soundExCode
                                                         select contact;

                DataView view = query.AsDataView();
                _result = view.ToTable();

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                grdResultD.DataSource = dtresultcopyMRN;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                Session["Doc"] = "true";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                string soundExCode = SoundEx("CONS");

                EnumerableRowCollection<DataRow> query = from contact in _result.AsEnumerable()
                                                         where SoundEx(contact.Field<string>("Sub Type")) == soundExCode
                                                         select contact;

                DataView view = query.AsDataView();
                _result = view.ToTable();

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
            }
        }

        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                string soundExCode = SoundEx("CONS");

                EnumerableRowCollection<DataRow> query = from contact in _result.AsEnumerable()
                                                         where SoundEx(contact.Field<string>("Sub Type")) == soundExCode
                                                         select contact;

                DataView view = query.AsDataView();
                _result = view.ToTable();

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                grdResultD.DataSource = dtresultcopyMRN;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                string soundExCode = SoundEx("CONS");

                EnumerableRowCollection<DataRow> query = from contact in _result.AsEnumerable()
                                                         where SoundEx(contact.Field<string>("Sub Type")) == soundExCode
                                                         select contact;

                DataView view = query.AsDataView();
                _result = view.ToTable();

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
            }
        }

        static private string SoundEx(string word)
        {
            // The length of the returned code.
            int length = 4;

            // Value to return.
            string value = "";

            // The size of the word to process.
            int size = word.Length;

            // The word must be at least two characters in length.
            if (size > 1)
            {
                // Convert the word to uppercase characters.
                word = word.ToUpper(System.Globalization.CultureInfo.InvariantCulture);

                // Convert the word to a character array.
                char[] chars = word.ToCharArray();

                // Buffer to hold the character codes.
                StringBuilder buffer = new StringBuilder();
                buffer.Length = 0;

                // The current and previous character codes.
                int prevCode = 0;
                int currCode = 0;

                // Add the first character to the buffer.
                buffer.Append(chars[0]);

                // Loop through all the characters and convert them to the proper character code.
                for (int i = 1; i < size; i++)
                {
                    switch (chars[i])
                    {
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U':
                        case 'H':
                        case 'W':
                        case 'Y':
                            currCode = 0;
                            break;
                        case 'B':
                        case 'F':
                        case 'P':
                        case 'V':
                            currCode = 1;
                            break;
                        case 'C':
                        case 'G':
                        case 'J':
                        case 'K':
                        case 'Q':
                        case 'S':
                        case 'X':
                        case 'Z':
                            currCode = 2;
                            break;
                        case 'D':
                        case 'T':
                            currCode = 3;
                            break;
                        case 'L':
                            currCode = 4;
                            break;
                        case 'M':
                        case 'N':
                            currCode = 5;
                            break;
                        case 'R':
                            currCode = 6;
                            break;
                    }

                    // Check if the current code is the same as the previous code.
                    if (currCode != prevCode)
                    {
                        // Check to see if the current code is 0 (a vowel); do not process vowels.
                        if (currCode != 0)
                            buffer.Append(currCode);
                    }
                    // Set the previous character code.
                    prevCode = currCode;

                    // If the buffer size meets the length limit, exit the loop.
                    if (buffer.Length == length)
                        break;
                }
                // Pad the buffer, if required.
                size = buffer.Length;
                if (size < length)
                    buffer.Append('0', (length - size));

                // Set the value to return.
                value = buffer.ToString();
            }
            // Return the value.
            return value;
        }
        protected void btnSearch_PO_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                if (chktemp.Checked == true)
                {
                    _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now));
                    lblvalue.Text = "TempDoc";
                    Session["TempDoc"] = "true";
                }
                else
                {
                    _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now));
                    lblvalue.Text = "Doc";
                    Session["Doc"] = "true";
                }

                string soundExCode = SoundEx("CONS");

                EnumerableRowCollection<DataRow> query = from contact in _result.AsEnumerable()
                                                         where SoundEx(contact.Field<string>("Sub Type")) == soundExCode
                                                         select contact;

                DataView view = query.AsDataView();
                _result = view.ToTable();

                grdResultD.DataSource = null;
                grdResultD.DataBind();


                if (chktemp.Checked == true)
                {
                    grdResultD.DataSource = _result;
                    grdResultD.DataBind();
                    BindUCtrlDDLData2(_result);

                    txtFDate.Text = Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1).ToShortDateString();
                    txtTDate.Text = Convert.ToDateTime(DateTime.Now).Date.ToShortDateString();
                    UserDPopoup.Show();
                }
                else
                {
                    DataTable dtresultcopyMRN = new DataTable();
                    dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                    foreach (DataRow ddr in _result.Rows)
                    {
                        string doc = ddr["Document"].ToString();
                        string subtp = ddr["Sub Type"].ToString();
                        string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                        string manref = ddr["Manual Ref No"].ToString();
                        string otherdoc = ddr["Other Document"].ToString();
                        string entry = ddr["Entry No"].ToString();
                        string job = ddr["Job No"].ToString();
                        string otherloc = ddr["Other Loc"].ToString();
                        string stus = ddr["Status"].ToString();

                        dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                    }

                    grdResultD.DataSource = dtresultcopyMRN;
                    grdResultD.DataBind();
                    BindUCtrlDDLData2(dtresultcopyMRN);

                    txtFDate.Text = Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1).ToShortDateString();
                    txtTDate.Text = Convert.ToDateTime(DateTime.Now).Date.ToShortDateString();
                    UserDPopoup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }

        protected void btnGetPO_Click(object sender, EventArgs e)
        {
            try
            {
                string reqNo = txtFindPONo.Text.Trim();
                DateTime FromDt = Convert.ToDateTime(dtpFromDate.Text);
                DateTime ToDt = Convert.ToDateTime(dtpToDate.Text);
                string supplier = txtFindSupplier.Text.Trim();

                ClearBody();
                dtpFromDate.Text = FromDt.ToString("dd/MMM/yyyy");
                dtpToDate.Text = ToDt.ToString("dd/MMM/yyyy"); 
                txtFindSupplier.Text = supplier;
                txtFindPONo.Text = reqNo;

                GetPendingPurchaseOrders(dtpFromDate.Text, dtpToDate.Text, supplier, reqNo, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void GetPendingPurchaseOrders(string _fromDate, string _toDate, string _supCode, string _docNo, Boolean _showErrMsg)
        {
            try
            {
                PurchaseOrder _paramPurchaseOrder = new PurchaseOrder();

                _paramPurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
                _paramPurchaseOrder.MasterLocation = new MasterLocation() { Ml_loc_cd = Session["UserDefLoca"].ToString() };
                _paramPurchaseOrder.Poh_stus = "A";
                _paramPurchaseOrder.MasterBusinessEntity = new MasterBusinessEntity() { Mbe_tp = "S" };
                _paramPurchaseOrder.FromDate = _fromDate;
                _paramPurchaseOrder.ToDate = _toDate;
                _paramPurchaseOrder.Poh_supp = _supCode;
                _paramPurchaseOrder.Poh_doc_no = _docNo;
                _paramPurchaseOrder.Poh_sub_tp = "C";

                DataTable pending_list = CHNLSVC.Inventory.GetAllPendingPurchaseOrderDataTable(_paramPurchaseOrder);

                if (pending_list.Rows.Count >= 0)
                {
                    dvPendingPO.AutoGenerateColumns = false;
                    dvPendingPO.DataSource = pending_list;
                    dvPendingPO.DataBind();
                }
                else
                {
                    if (_showErrMsg == true)
                    {
                        pending_list = null;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No pending requests found !!!')", true);
                        dvPendingPO.AutoGenerateColumns = false;
                        dvPendingPO.DataSource = pending_list;
                        dvPendingPO.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void dvPendingPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dvPendingPO, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dvPendingPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string OutwardNo = string.Empty;
            try
            {
                btnSearch_Supplier.Enabled = false;
                string pdaloc = (string)Session["PDA_LOCATION"];

                OutwardNo = (dvPendingPO.SelectedRow.FindControl("lblreqno") as Label).Text;

                DataTable dtdoccheck1 = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo, "AOD", 1, Session["UserCompanyCode"].ToString());

                if (dtdoccheck1.Rows.Count > 1)
                {
                    grdItems.DataSource = new int[]{};
                    grdItems.DataBind();
                    grdSerial.DataSource = new int[]{};
                    grdSerial.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + OutwardNo + " !!!')", true);
                    return;
                }

                DataTable _headerchk2 = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), OutwardNo);
                if (_headerchk2 != null && _headerchk2.Rows.Count > 0)
                {
                    string _headerUser = _headerchk2.Rows[0].Field<string>("tuh_usr_id");
                    string _scanDate = Convert.ToString(_headerchk2.Rows[0].Field<DateTime>("tuh_cre_dt"));

                    if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                        {
                            string msg2 = "Document " + OutwardNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                            return;
                        }
                }

                Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), OutwardNo);
                List<ReptPickSerials> PickSerialsPOPUP = CHNLSVC.Inventory.GetTempPickSerialBySeqNo(_seq, OutwardNo, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                Session["POPUPSERIALS"] = PickSerialsPOPUP;

                if (dvPendingPO.SelectedRow.RowIndex != -1)
                {
                    txtPONo.Text = (dvPendingPO.SelectedRow.FindControl("lblreqno") as Label).Text;
                    dtpPODate.Text = (dvPendingPO.SelectedRow.FindControl("lblreqdate") as Label).Text;
                    txtPORefNo.Text = (dvPendingPO.SelectedRow.FindControl("lblrefno") as Label).Text;
                    txtSuppCode.Text = (dvPendingPO.SelectedRow.FindControl("lblsupcode") as Label).Text;
                    txtSuppName.Text = (dvPendingPO.SelectedRow.FindControl("lblsupname") as Label).Text;
                }
                if (pdaloc == "1")
                {
                    DataTable dtupdatedlb = CHNLSVC.Inventory.CheckHasLoadingBay(Session["UserCompanyCode"].ToString(), OutwardNo, Session["UserDefLoca"].ToString());

                    if (dtupdatedlb.Rows.Count == 0)
                    {
                        grdSerial.DataSource = null;
                        grdSerial.DataBind();
                        Session["SERIALLIST"] = null;
                        LoadPOItems(OutwardNo);
                        //Int32 deltempser = CHNLSVC.Inventory.DeleteRepSerials(_seq, OutwardNo);
                        

                        if (chkpda.Checked == true)
                        {
                            txtdocname.Text = OutwardNo;
                            MPPDA.Show();
                        }
                        else
                        {
                            MPPDA.Hide();
                            txtdocname.Text = string.Empty;
                            ddlloadingbay.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if (dvPendingPO.Rows.Count > 0)
                        {
                            string warehousecom = (string)Session["WAREHOUSE_COM"];
                            string warehouseloc = (string)Session["WAREHOUSE_LOC"];

                            DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo, "AOD", 1, Session["UserCompanyCode"].ToString());

                            if (dtdoccheck.Rows.Count > 1)
                            {
                                grdItems.DataSource = new int[]{};
                                grdItems.DataBind();
                                grdSerial.DataSource = new int[]{};
                                grdSerial.DataBind();
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + OutwardNo + " !!!')", true);
                                return;
                            }

                            DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), OutwardNo);
                            if (_headerchk != null && _headerchk.Rows.Count > 0)
                            {
                                string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id");
                                string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt"));

                                if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                                    {
                                        string msg2 = "Document " + OutwardNo + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                                        return;
                                    }
                            }
                            LoadPOItems(OutwardNo);
                        }
                    }
                }
                else
                {

                    DataTable _resultNew = new DataTable();
                    if (dvPendingPO.Rows.Count > 0)
                    {
                        txtPONo.Text = (dvPendingPO.SelectedRow.FindControl("lblreqno") as Label).Text;
                        dtpPODate.Text = (dvPendingPO.SelectedRow.FindControl("lblreqdate") as Label).Text;
                        txtPORefNo.Text = (dvPendingPO.SelectedRow.FindControl("lblrefno") as Label).Text;
                        txtSuppCode.Text = (dvPendingPO.SelectedRow.FindControl("lblsupcode") as Label).Text;
                        txtSuppName.Text = (dvPendingPO.SelectedRow.FindControl("lblsupname") as Label).Text;

                        Session["PO_NO"] = txtPONo.Text.Trim();

                        string doctype = (string)Session["DocType"];

                        if ((doctype == "Doc") || (string.IsNullOrEmpty(doctype)))
                        {
                            DataTable dtrptdb = CHNLSVC.Inventory.CheckDocIsInRepDB(Session["UserCompanyCode"].ToString(), txtPONo.Text.ToString());

                            //if (dtrptdb.Rows.Count == 0)
                            //{
                            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No records in report db !!!')", true);
                            //    return;
                            //}

                            LoadPOItems(txtPONo.Text.ToString());
                        }

                        if (doctype == "TempDoc")
                        {
                            _resultNew = CHNLSVC.Inventory.GetPoQty(txtPONo.Text, 0);
                        }
                    }

                    foreach (GridViewRow gvrow in grdItems.Rows)
                    {
                        Label code = (Label)gvrow.FindControl("lblitri_itm_cd");
                        Label line = (Label)gvrow.FindControl("lblpodiline");
                        foreach (DataRow ddr22 in _resultNew.Rows)
                        {
                            if ((code.Text == ddr22["POD_ITM_CD"].ToString()) && (line.Text == ddr22["POD_LINE_NO"].ToString()))
                            {
                                Label poqty = (Label)gvrow.FindControl("lblitri_qty");
                                poqty.Text = ddr22["POD_QTY"].ToString();

                                Label picqty = (Label)gvrow.FindControl("lblpickqty");

                                Decimal POQTY = Convert.ToDecimal(poqty.Text);
                                Decimal PICQTY = Convert.ToDecimal(picqty.Text);
                                Decimal remainqty = POQTY - PICQTY;
                                Label remaqty = (Label)gvrow.FindControl("lblremainqty");
                                remaqty.Text = remainqty.ToString();
                            }
                        }
                    }

                    foreach (GridViewRow gvrow in grdItems.Rows)
                    {
                        Label q1 = (Label)gvrow.FindControl("lblitri_qty");
                        q1.Text = DoFormat(Convert.ToDecimal(q1.Text));

                        Label q2 = (Label)gvrow.FindControl("lblremainqty");
                        q2.Text = DoFormat(Convert.ToDecimal(q2.Text));

                        Label q3 = (Label)gvrow.FindControl("lblpickqty");
                        q3.Text = DoFormat(Convert.ToDecimal(q3.Text));
                    }
                    txtFindSupplier.Text = txtSuppCode.Text;
                    ucInScan.ListClear();
                    ucInScan.PageClear();
                    ucInScan.PageDataClear();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
        private void LoadPOItems(string _poNo)
        {
            try
            {
                DataTable po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), _poNo, 1);
                if (po_items.Rows.Count > 0)
                {
                    grdItems.Enabled = true;
                    if (user_seq_num == 0)
                    {
                        user_seq_num = -1;
                    }
                    try
                    {
                        user_seq_num = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), _poNo);
                        if (user_seq_num==0)
                        {
                            user_seq_num = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No records in report db !!!')", true);
                        return;
                    }


                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "ADJ");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataRow row in po_items.Rows)
                            {

                                MasterItem _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), row["PODI_ITM_CD"].ToString());
                                if (_mstItm.Mi_is_ser1 == -1)
                                {
                                    var _scanItems1 = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theSum = group.Sum(o => o.Tus_qty) });
                                    foreach (var itm1 in _scanItems1)
                                    {
                                        if (itm1.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString())
                                        {
                                            row["GRN_QTY"] = itm1.theSum;
                                        }
                                    }

                                }
                                else
                                {
                                    if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString()))
                                    {
                                        row["GRN_QTY"] = itm.theCount;
                                    }
                                }
                            }
                        }
                        Session["SERIALLIST"] = _serList;
                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _serList;
                        grdSerial.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = emptyGridList;
                        grdSerial.DataBind();
                    }

                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = po_items;
                    grdItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private Int32 GenerateNewUserSeqNo()
        {
            try
            {
                Int32 _userSeqNo = 0;
                _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString());

                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_direct = true;
                _inputReptPickHeader.Tuh_doc_no = txtPONo.Text; ;//_selectedReqNo;
                _inputReptPickHeader.Tuh_doc_tp = "ADJ";
                _inputReptPickHeader.Tuh_ischek_itmstus = true;//false; not sure
                _inputReptPickHeader.Tuh_ischek_reqqty = true;//false; not sure
                _inputReptPickHeader.Tuh_ischek_simitm = true;//false; not sure
                _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;
                _inputReptPickHeader.Tuh_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;

                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(_inputReptPickHeader);
                if (affected_rows == 1)
                {
                    return _userSeqNo;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return 0;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        protected void lbtnadd_Click(object sender, EventArgs e)
        {
            try
            {
                Session["POPUP"] = "Yes";

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                string _itamcode = string.Empty;
                string line = "0";
                if (row != null)
                {
                    _itamcode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                    line = (row.FindControl("lblpodiline") as Label).Text;
                }

                ucInScan.TXTItemCode.Text = _itamcode;
                ucInScan.ItemCodeChange();
                ucInScan.DDL_STATUS.Text = "CONS";
                ucInScan.LBtnItemCode.Visible = false;
                ucInScan.TXTItemCode.ReadOnly = true;
                ucInScan.GrnNoRequired = true;
                ucInScan.TXTUnitcost.Text = "0";
                ucInScan.userSeqNo = user_seq_num.ToString();
                ViewState["userSeqNo"] = user_seq_num.ToString();
                ucInScan.baseitemline = Convert.ToInt32(line);
                Session["baseitemline"] = Convert.ToInt32(line);
                while (ucInScan.DDL_STATUS.Items.Count > 0)
                {
                    ucInScan.DDL_STATUS.Items.RemoveAt(0);
                }
                ucInScan.DDL_STATUS.Items.Add(new ListItem("CONSIGNMENT", "CONS"));
                ucInScan.TXTSuppliercode.Text = txtSuppCode.Text;
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value == "Yes")
                {
                    if ((chktemp.Checked == true) && (!string.IsNullOrEmpty(txtFindPONoDoc.Text.Trim())))
                    {
                        Session["DocType"] = "TempDoc";
                    }
                    else
                    {
                        Session["DocType"] = "Doc";
                    }
                    Process(false);
                    Session["GlbReportType"] = "SCM1_CONSIN";
                    Session["GlbReportName"] = "Inward_Docs_Consign.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    clsInventory obj = new clsInventory();
                    obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    PrintPDF(targetFileName, obj._consIn);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //clsInventory obj = new clsInventory();
                //InvReportPara _objRepoPara = new InvReportPara();
                //_objRepoPara = Session["InvReportPara"] as InvReportPara;
                //obj.Print_AOA_Warra(_objRepoPara);
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error')", true);
                return;
            }
        }
        public bool CheckServerDateTime()
        {
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine date conflict with the server date.please contact system administrator !!!')", true);
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine time zone conflict with the server time zone.please contact system administrator !!!')", true);
                return false;
            }
            return true;
        }

        public bool IsReferancedDocDateAppropriate(List<ReptPickSerials> _reptPrickSerialList, DateTime _processDate)
        {
            bool _appropriate = true;
            if (_reptPrickSerialList != null)
            {
                var _isInAppropriate = _reptPrickSerialList.Where(x => x.Tus_doc_dt.Date > _processDate.Date).ToList();
                if (_isInAppropriate == null || _isInAppropriate.Count <= 0) _appropriate = true;
                else _appropriate = false;
                if (_appropriate == false)
                {
                    StringBuilder _documents = new StringBuilder();
                    foreach (ReptPickSerials _one in _isInAppropriate)
                    {
                        if (string.IsNullOrEmpty(_documents.ToString()))
                            _documents.Append(_one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                        else
                            _documents.Append(" and " + _one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                    }
                    string msg = "The Inward documents " + _documents.ToString() + " equal or grater than to a this Outward document " + _processDate.Date.ToShortDateString() + " date!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                }
            }
            return _appropriate;
        }

        private void Process(bool _IsTemp)
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtPONo.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select Request no !!!')", true);
                    return;
                }

                if (chkManualRef.Checked)
                    if (string.IsNullOrEmpty(txtManualRefNo.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have not entered a valid manual document no !!!')", true);
                        return;
                    }

                if (string.IsNullOrEmpty(txtManualRefNo.Text)) txtManualRefNo.Text = "N/A";
                if (string.IsNullOrEmpty(txtVehicleNo.Text)) txtVehicleNo.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                int resultDate = DateTime.Compare(Convert.ToDateTime(dtpPODate.Text), Convert.ToDateTime(dtpDODate.Text));
                if (resultDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Request date should be greater than or equal to Consignment date !!!')", true);
                    return;
                }

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), "m_Trans_Inventory_ConsStock_StockRec", dtpDODate, lblH1, dtpDODate.Text.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        DateTime d1 = Convert.ToDateTime(dtpDODate.Text);
                        DateTime d2 = DateTime.Now;

                        if (d1.Date != d2.Date)
                        {
                            dtpDODate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction !!!')", true);
                            dtpDODate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpDODate.Enabled = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction !!!')", true);
                        dtpDODate.Focus();
                        return;
                    }
                }

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                InventoryHeader invHdr = new InventoryHeader();
                string documntNo = "";
                Int32 result = -99;

                var list = (List<ReptPickSerials>)Session["SERIALLIST"];
                int _userSeqNo = 0;
                string docno = txtUserSeqNo.Text.Trim();

                if (chktemp.Checked == true)
                {
                    _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);
                    reptPickSerialsList = list;
                }
                else
                {
                    _userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), txtPONo.Text);
                    reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "ADJ");
                }

                if (reptPickSerialsList == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No serials found !!!')", true);
                    return;
                }

                #region Add By Lakshan 01/Sep/2016
                var v = reptPickSerialsList.Where(c => c.Tus_itm_stus != "CONS").ToList();
                if (v!=null)
                {
                    if (v.Count>0)
                    {
                         ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Picked serial status invalid !!!')", true);
                        return;
                    }
                }
                #endregion

                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ");

                #region Check Referance Date and the Doc Date
                if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                {
                    return;
                }
                #endregion

                #region Check Duplicate Serials
                var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                string _duplicateItems = string.Empty;
                bool _isDuplicate = false;
                if (_dup != null)
                    if (_dup.Count > 0)
                        foreach (Int32 _id in _dup)
                        {
                            Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                            if (_counts > 1)
                            {
                                _isDuplicate = true;
                                var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                foreach (string _str in _item)
                                    if (string.IsNullOrEmpty(_duplicateItems))
                                        _duplicateItems = _str;
                                    else
                                        _duplicateItems += "," + _str;
                            }
                        }
                if (_isDuplicate)
                {
                    string msg2 = "Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                    return;
                }
                #endregion


                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(Session["UserCompanyCode"].ToString(), txtPONo.Text, Session["UserDefLoca"].ToString());

                if (reptPickSerialsList != null)
                {
                    var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        if (_purchaseOrderDeliveryList != null)
                        {
                            foreach (PurchaseOrderDelivery _invItem in _purchaseOrderDeliveryList)

                                //if ((itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd) && (itm.Peo.Tus_itm_line == _invItem.Podi_line_no))
                                if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd)
                                {
                                    _invItem.Actual_qty = itm.theCount; // Current scan qty
                                }
                        }
                    }

                }

                if (_purchaseOrderDeliveryList != null)
                {
                    foreach (PurchaseOrderDelivery item in _purchaseOrderDeliveryList)
                    {
                        Int32 _count = 0;
                        List<ReptPickSerials> oSelectedItms = new List<ReptPickSerials>();
                        //oSelectedItms = reptPickSerialsList.FindAll(x => x.Tus_itm_cd == item.MasterItem.Mi_cd && x.Tus_itm_stus == item.Podi_itm_stus);

                        foreach (ReptPickSerials itemser in reptPickSerialsList)
                        {
                            if ((itemser.Tus_itm_cd == item.MasterItem.Mi_cd) && (itemser.Tus_itm_stus == item.Podi_itm_stus))
                            {
                                oSelectedItms.Add(itemser);
                            }
                        }

                        foreach (ReptPickSerials _itemSer in oSelectedItms)
                        {
                            _count++;
                        }

                        //if (item.Podi_bal_qty < _count || _count == 0)
                        //{
                        //    string msg3 = "Following item serials and item quantities not matching. " + item.Podi_itm_cd;
                        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg3 + "')", true);
                        //    return;
                        //}

                    }
                }

                InventoryHeader _invHeader = new InventoryHeader();

                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        _invHeader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        _invHeader.Ith_channel = string.Empty;
                    }
                }

                DateTime mydate = Convert.ToDateTime(dtpDODate.Text);

                if (Session["DocType"].ToString() == "TempDoc")
                {
                    _invHeader.Ith_anal_10 = true;
                    _invHeader.Ith_anal_2 = txtFindPONoDoc.Text;
                }
                else
                {
                    _invHeader.Ith_anal_10 = false;
                    _invHeader.Ith_anal_2 = "";
                }

                _invHeader.Ith_com = Session["UserCompanyCode"].ToString();//
                _invHeader.Ith_loc = Session["UserDefLoca"].ToString();//
                _invHeader.Ith_pc = Session["UserDefProf"].ToString();
                _invHeader.Ith_oth_com = Session["UserCompanyCode"].ToString();
                _invHeader.Ith_doc_date = Convert.ToDateTime(dtpDODate.Text);//
                _invHeader.Ith_doc_year = mydate.Year;//
                _invHeader.Ith_direct = true;//
                _invHeader.Ith_doc_tp = "ADJ";//"GRN";
                _invHeader.Ith_cate_tp = "CONSIGN";//"NOR";
                //  _invHeader.Ith_sub_tp = "LOCAL";  TODO:
                _invHeader.Ith_bus_entity = txtSuppCode.Text;//
                if (chkManualRef.Checked == true)
                {
                    _invHeader.Ith_is_manual = true;//
                }
                else
                {
                    _invHeader.Ith_is_manual = false;//
                }
                _invHeader.Ith_manual_ref = txtManualRefNo.Text;//
                _invHeader.Ith_remarks = txtRemarks.Text;//
                _invHeader.Ith_stus = "A";//
                _invHeader.Ith_cre_by = Session["UserID"].ToString();//
                _invHeader.Ith_cre_when = CHNLSVC.Security.GetServerDateTime().Date;// DateTime.Now;//
                _invHeader.Ith_mod_by = Session["UserID"].ToString();//
                _invHeader.Ith_mod_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;//
                _invHeader.Ith_session_id = Session["SessionID"].ToString();//
                _invHeader.Ith_oth_docno = txtPONo.Text;//
                _invHeader.Ith_acc_no = "CONS_INS";//?????
                _invHeader.Ith_sub_tp = "CONS"; //????
                _invHeader.Ith_vehi_no = txtVehicleNo.Text.Trim();
                _invHeader.Ith_anal_5 = txtPORefNo.Text.Trim();

                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "CONS";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "CONS";
                _masterAuto.Aut_year = null;
                #region add by lakshan 28Sep2017
                if (reptPickSerialsList != null)
                {
                    foreach (var item in reptPickSerialsList)
                    {
                        item.Tus_unit_cost = 0;
                        item.Tus_unit_price = 0;
                    }
                }
                #endregion
                result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, null, _masterAuto, _purchaseOrderDeliveryList, out documntNo, _IsTemp);
                if (result != -99 && result >= 0)
                {
                    string Msg = "Successfully Saved! Document No : " + documntNo;
                    Session["documntNo"] = documntNo;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);

                    this.btnGetPO_Click(null, null);

                    if (chkManualRef.Checked == false)
                    {
                        //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        Session["GlbReportName"] = string.Empty; //add on 16-Jul-2013
                        //_view.GlbReportName = string.Empty; //add on 16-Jul-2013

                        if (_invHeader.Ith_direct == true) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                        if (Session["UserCompanyCode"].ToString() == "SGL") //Sanjeewa 2014-01-07
                        {
                            //if (_invHeader.Ith_direct == true) _view.GlbReportName = "Inward_Docs.rpt";
                            //else _view.GlbReportName = "Outward_Docs.rpt";
                        }
                        else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                        {
                            //if (_invHeader.Ith_direct == true) _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                            //else _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                        }
                        else
                        {
                            //if (_invHeader.Ith_direct == true) _view.GlbReportName = "Inward_Docs.rpt";
                            //else _view.GlbReportName = "Outward_Docs.rpt";
                        }
                        //_view.GlbReportDoc = documntNo;
                        //_view.Show();
                        //_view = null;
                    }
                    ClearScreen();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    return;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ClearScreen();
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtManualRefNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtManualRefNo.Text != "" && chkManualRef.Checked)
                {
                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_DO", Convert.ToInt32(txtManualRefNo.Text));
                    if (_IsValid == false)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid Manual Document Number !!!')", true);
                        txtManualRefNo.Text = "";
                        txtManualRefNo.Focus();
                        return;
                    }
                }
                else
                {
                    if (chkManualRef.Checked == true)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid Manual Document Number !!!')", true);
                        txtManualRefNo.Focus();
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtManualRefNo.Text) && chkManualRef.Checked == false)
                {
                    //TODO: Check N Validate N Load Serials.
                    string _msg = string.Empty;
                    CHNLSVC.Inventory.GetSCMAOD(Convert.ToDateTime(dtpDODate.Text), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), BaseCls.GlbDefaultBin, txtSuppCode.Text.Trim(), txtManualRefNo.Text.Trim(), txtPONo.Text.Trim(), Session["UserID"].ToString(), out _msg);
                    if (string.IsNullOrEmpty(_msg))
                    {
                        LoadPOItems(txtPONo.Text.Trim());
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "')", true);
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            try
            {
                string ordstatus = (string)Session["STATUS"];

                if (txtcancenconfirm.Value == "Yes")
                {
                    if (string.IsNullOrEmpty(txtFindPONoDoc.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a document no !!!')", true);
                        btnSearch_PO.Focus();
                        return;
                    }

                    DataTable dtcheck = CHNLSVC.Inventory.CheckINBIssueQty(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtFindPONoDoc.Text.Trim());

                    foreach (DataRow item in dtcheck.Rows)
                    {
                        decimal issuqty = Convert.ToDecimal(item[0].ToString());
                        if (issuqty > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Request is in use.you are not allowed to cancel !!!')", true);
                            return;
                        }
                    }

                    if (ordstatus == "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This document has been already cancelled !!!')", true);
                        return;
                    }

                    _userid = (string)Session["UserID"];

                    InventoryHeader header = new InventoryHeader();

                    header.Ith_stus = "C";
                    header.Ith_doc_no = txtFindPONoDoc.Text.Trim();
                    header.Ith_com = Session["UserCompanyCode"].ToString();
                    header.Ith_loc = Session["UserDefLoca"].ToString();
                    header.Ith_mod_by = _userid;
                    header.Ith_mod_when = CHNLSVC.Security.GetServerDateTime();

                    Int32 outputresultapprove = CHNLSVC.Inventory.UpdateCRNStatus(header);

                    if (outputresultapprove == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully cancelled !!!')", true);
                        ClearScreen();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void SetManualRefNo()
        {
            try
            {
                if (chkManualRef.Checked == true)
                {
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_GRN");
                    if (_NextNo != 0)
                        txtManualRefNo.Text = _NextNo.ToString();
                    else
                        txtManualRefNo.Text = string.Empty;
                }
                else
                    txtManualRefNo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void chkManualRef_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkManualRef.Checked)
                {
                    SetManualRefNo();
                    txtManualRefNo.Enabled = false;
                }
                else
                {
                    txtManualRefNo.Enabled = true;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnadd_Click1(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
                {
                    
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    if (row != null)
                    {
                        string _item = (row.FindControl("lblitri_itm_cd") as Label).Text;
                        string _status = (row.FindControl("lblstatus") as Label).Text;
                        string _serialIDtext = (row.FindControl("lblserialid") as Label).Text;
                        string _bin = (row.FindControl("lblbin") as Label).Text;
                        string _serial1 = (row.FindControl("lblser1") as Label).Text;

                        if (string.IsNullOrEmpty(_item)) return;

                        //Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", Session["UserCompanyCode"].ToString(), txtUserSeqNo.Text.Trim(),1);
                        Int32 UserSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), txtPONo.Text);
                        MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                        if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                        {
                            //modify Rukshan 05/oct/2015 add two parameters
                            CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, Convert.ToInt32(_serialIDtext), null, null);
                            CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, Convert.ToInt32(_serialIDtext), 1);
                        }
                        else
                        {
                            CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _item, _status);
                        }

                        LoadPOItems(txtUserSeqNo.Text.Trim());
                    }
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtFindSupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindSupplier.Text)) return;

                if (!CHNLSVC.Inventory.IsValidSupplier(Session["UserCompanyCode"].ToString(), txtFindSupplier.Text, 1, "S"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid supplier !!!')", true);
                    txtFindSupplier.Text = "";
                    txtFindSupplier.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtFindPONo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindPONo.Text)) return;

                PurchaseOrder _hdr = CHNLSVC.Inventory.GetPOHeader(Session["UserCompanyCode"].ToString(), txtFindPONo.Text.Trim(), "L");
                if (_hdr == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid consignment request no !!!')", true);
                    txtFindPONo.Text = string.Empty;
                    txtFindPONo.Focus();
                    return;
                }
                else
                {
                    if (_hdr.Poh_sub_tp != "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid consignment request no !!!')", true);
                        txtFindPONo.Text = string.Empty;
                        txtFindPONo.Focus();
                        return;
                    }
                    else if (_hdr.Poh_stus == "P")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Request has not approved yet !!!')", true);
                        return;
                    }
                    else if (_hdr.Poh_stus == "F")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Request has been completed !!!')", true);
                        return;
                    }
                    else if (_hdr.Poh_stus == "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This is a cancelled request !!!')", true);
                        return;
                    }
                    else
                    {
                        btnGetPO_Click(null, null);
                    }
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value =="Yes")
                {
                    Session["DocType"] = "TempDoc";
                    Process(true); 
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                string soundExCode = SoundEx("CONS");

                EnumerableRowCollection<DataRow> query = from contact in _result.AsEnumerable()
                                                         where SoundEx(contact.Field<string>("Sub Type")) == soundExCode
                                                         select contact;

                DataView view = query.AsDataView();
                _result = view.ToTable();

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                grdResultD.DataSource = dtresultcopyMRN;
                grdResultD.DataBind();
                BindUCtrlDDLData2(dtresultcopyMRN);
                lblvalue.Text = "Doc";
                txtFDate.Text = Convert.ToDateTime(txtFDate.Text).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtTDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
            }
            else if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                string soundExCode = SoundEx("CONS");

                EnumerableRowCollection<DataRow> query = from contact in _result.AsEnumerable()
                                                         where SoundEx(contact.Field<string>("Sub Type")) == soundExCode
                                                         select contact;

                DataView view = query.AsDataView();
                _result = view.ToTable();

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "TempDoc";
                txtFDate.Text = Convert.ToDateTime(txtFDate.Text).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtTDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
            }
        }

        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            txtFindPONoDoc.Text = Name;
            RecallData(Name);

            Session["Doc"] = null;
            Session["TempDoc"] = null;
            Session["SENT_TO_CRN"] = null;
        }

        private void GetDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {

                    #region Clear Data

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = new int[]{};
                    grdItems.DataBind();

                    lbtnsave.Enabled = true;
                    lbtnsave.OnClientClick = "return Enable();";
                    lbtnsave.CssClass = "buttoncolor";

                    LinkButton2.Enabled = false;
                    LinkButton2.OnClientClick = "return Enable();";
                    LinkButton2.CssClass = "buttoncolor";

                    #endregion

                    #region Get Serials
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtFindPONoDoc.Text);

                    if (_serList != null)
                    {
                        DataTable dttemp2 = new DataTable();
                        dttemp2.Columns.AddRange(new DataColumn[12] { new DataColumn("PODI_SEQ_NO"), new DataColumn("PODI_LINE_NO"), new DataColumn("PODI_ITM_CD"), new DataColumn("MI_LONGDESC"), new DataColumn("MI_MODEL"), new DataColumn("MI_BRAND"), new DataColumn("PODI_QTY", System.Type.GetType("System.Decimal")), new DataColumn("PODI_BAL_QTY"), new DataColumn("GRN_QTY"), new DataColumn("PODI_LOCA"), new DataColumn("PODI_REMARKS"), new DataColumn("UNIT_PRICE") });

                        List<InventoryBatchN> _itmlist = CHNLSVC.Inventory.Get_Int_Batch(txtFindPONoDoc.Text.Trim());

                        foreach (InventoryBatchN binditem in _itmlist)
                        {
                            foreach (ReptPickSerials seritem in _serList)
                            {
                                if ((binditem.Inb_itm_cd == seritem.Tus_itm_cd) && (binditem.Inb_itm_line == seritem.Tus_itm_line))
                                {
                                    dttemp2.Rows.Add(binditem.Inb_seq_no, binditem.Inb_itm_line, binditem.Inb_itm_cd, seritem.Tus_itm_desc, seritem.Tus_itm_model, seritem.Tus_itm_brand, binditem.Inb_qty, binditem.Itb_bal_qty1, "", binditem.Inb_loc, "", binditem.Inb_unit_price);
                                }
                            } 
                        }

                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd}).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        Int32 count = 0;

                        grdItems.DataSource = new int[]{};
                        grdItems.DataBind();

                        DataTable uniqueCols = RemoveDuplicateRows(dttemp2, "PODI_ITM_CD");
                        DataTable _dt = new DataTable();
                        foreach (DataColumn column in uniqueCols.Columns)
                        {
                            _dt.Columns.Add(column.ColumnName, column.DataType); //better to have cell type
                        }
                        for (int i = 0; i < uniqueCols.Rows.Count; i++)
                        {
                            if (i < 500)
                            {
                                _dt.Rows.Add();
                                for (int j = 0; j < uniqueCols.Columns.Count; j++)
                                {
                                    _dt.Rows[i][j] = uniqueCols.Rows[i][j];
                                }
                            }
                        }
                        grdItems.DataSource = _dt;
                        grdItems.DataBind();
                        List<ReptPickSerials>  _tmpList = new List<ReptPickSerials>();
                        foreach (var item in _serList)
                        {
                            if (_tmpList.Count < 500)
                            {
                                _tmpList.Add(item);
                            }
                        }
                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _tmpList;
                        grdSerial.DataBind();

                        foreach (var item in _scanItems)
                        {
                            count = item.theCount;
                            string itemcode = item.Peo.Tus_itm_cd;

                            foreach (GridViewRow row in grdItems.Rows)
                            {
                                Label l1 = (Label)row.FindControl("lblitri_itm_cd");
                                Label l2 = (Label)row.FindControl("lblpickqty");
                                if (itemcode == l1.Text )
                                {
                                    l2.Text = count.ToString();
                                }
                            }
                        }

                        foreach (GridViewRow row1 in grdItems.Rows)
                        {
                            Label la = (Label)row1.FindControl("lblitri_qty");
                            Label lb = (Label)row1.FindControl("lblremainqty");
                            Label lc = (Label)row1.FindControl("lblpickqty");

                            la.Text = DoFormat(Convert.ToDecimal(la.Text));
                            lb.Text = DoFormat(Convert.ToDecimal(lb.Text));
                            lc.Text = DoFormat(Convert.ToDecimal(lc.Text));
                        }
                    }
                    else
                    {
                        txtFindPONoDoc.Text = "";
                        txtFindPONoDoc.Focus();
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void LoadHeader(bool istemp)
        {
            try
            {
                dvPendingPO.DataSource = null;
                dvPendingPO.DataBind();

                DataTable dtheader = new DataTable();

                if (istemp == true)
                {
                    dtheader = CHNLSVC.Inventory.GetTempDocHeaderData(txtFindPONoDoc.Text.Trim());

                    lbtncancel.Enabled = false;
                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else
                {
                    dtheader = CHNLSVC.Inventory.GetDocHeaderData(txtFindPONoDoc.Text.Trim());
                }

                foreach (DataRow ddrdoc in dtheader.Rows)
                {
                    Session["LOADEDSEQNO"] = ddrdoc["ith_seq_no"].ToString();
                    Session["STATUS"] = ddrdoc["ith_stus"].ToString();
                    txtFindSupplier.Text = ddrdoc["ith_bus_entity"].ToString();
                    txtPONo.Text = ddrdoc["ith_doc_no"].ToString();
                }

                string stus = (string)Session["STATUS"];

                if (stus == "C")
                {
                    lbtncancel.Enabled = false;
                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else
                {
                    lbtncancel.Enabled = true;
                    lbtncancel.CssClass = "buttonUndocolor";
                    lbtncancel.OnClientClick = "ConfirmCancel();";
                }

                DataTable dtitemtemp = new DataTable();
                dtitemtemp.Columns.AddRange(new DataColumn[6] { new DataColumn("POH_DOC_NO"), new DataColumn("POH_DT"), new DataColumn("POH_SUPP"), new DataColumn("MBE_NAME"), new DataColumn("POH_REF"), new DataColumn("POH_REMARKS") });

                foreach (DataRow ddr in dtheader.Rows)
                {
                    dtitemtemp.Rows.Add(ddr[1].ToString(), ddr[2].ToString(), ddr[7].ToString(), ddr[13].ToString(), ddr[18].ToString(), ddr[15].ToString());
                    txtManualRefNo.Text = ddr[3].ToString();
                    txtVehicleNo.Text = ddr[16].ToString();
                    txtRemarks.Text = ddr[15].ToString();
                    txtPORefNo.Text = ddr[18].ToString();
                }

                dvPendingPO.DataSource = dtitemtemp;
                dvPendingPO.DataBind();
                dvPendingPO.SelectedIndex = 0;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            try
            {
                Session["POPUP"] = null;
                MpDelivery.Hide();
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void chktemp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chktemp.Checked == true)
                {
                    lbtncancel.Enabled = false;
                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else
                {
                    lbtncancel.Enabled = true;
                    lbtncancel.CssClass = "buttonUndocolor";
                    lbtncancel.OnClientClick = "ConfirmCancel();";
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void GetTempDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {

                    #region Clear Data

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = new int[]{};
                    grdItems.DataBind();

                    lbtnsave.Enabled = true;
                    lbtnsave.OnClientClick = "ConfirmPlaceOrder();";
                    lbtnsave.CssClass = "buttonUndocolor";

                    LinkButton2.Enabled = false;
                    LinkButton2.OnClientClick = "return Enable();";
                    LinkButton2.CssClass = "buttoncolor";

                    #endregion

                    #region Get Serials
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser_Temp(txtFindPONoDoc.Text);

                    if (_serList != null)
                    {
                        DataTable dttemp2 = new DataTable();
                        dttemp2.Columns.AddRange(new DataColumn[12] { new DataColumn("PODI_SEQ_NO"), new DataColumn("PODI_LINE_NO"), new DataColumn("PODI_ITM_CD"), new DataColumn("MI_LONGDESC"), new DataColumn("MI_MODEL"), new DataColumn("MI_BRAND"), new DataColumn("PODI_QTY"), new DataColumn("PODI_BAL_QTY"), new DataColumn("GRN_QTY"), new DataColumn("PODI_LOCA"), new DataColumn("PODI_REMARKS"), new DataColumn("UNIT_PRICE") });

                        List<InventoryBatchN> _itmlist = CHNLSVC.Inventory.Get_Int_Batch_Temp(txtFindPONoDoc.Text.Trim());


                        if (_serList.Count == 0)
                        {
                            foreach (InventoryBatchN binditem2 in _itmlist)
                            {
                                dttemp2.Rows.Add(binditem2.Inb_seq_no, binditem2.Inb_itm_line, binditem2.Inb_itm_cd, "","","", binditem2.Inb_qty, binditem2.Itb_bal_qty1, "", binditem2.Inb_loc, "", binditem2.Inb_unit_price);
                                txtUserSeqNo.Text = binditem2.Inb_seq_no.ToString();
                            }

                            foreach (InventoryBatchN binditem3 in _itmlist)
                            {
                                ReptPickSerials _serial = new ReptPickSerials();
                                MasterItem _itms = new MasterItem();
                                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), binditem3.Inb_itm_cd);
                                _serial.Tus_qty = binditem3.Inb_qty;
                                _serial.Tus_com = binditem3.Inb_com;
                                _serial.Tus_doc_dt = binditem3.Inb_doc_dt;
                                _serial.Tus_itm_cd = binditem3.Inb_itm_cd;
                                _serial.Tus_itm_stus = binditem3.Inb_itm_stus;
                                _serial.Tus_loc = binditem3.Inb_loc;
                                _serial.Tus_unit_price = binditem3.Inb_unit_price;
                                _serial.Tus_bin = binditem3.Inb_bin;
                                _serial.Tus_itm_desc = _itms.Mi_shortdesc;
                                _serial.Tus_itm_model = _itms.Mi_model;
                                _serial.Tus_itm_brand = _itms.Mi_brand;
                                _serial.Tus_doc_no = binditem3.Inb_doc_no;
                                _serial.Tus_ser_1 = "N/A";
                                _serial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                                _serList.Add(_serial);
                            }
                        }
                        else
                        {
                            foreach (InventoryBatchN binditem in _itmlist)
                            {
                                foreach (ReptPickSerials seritem in _serList)
                                {
                                    if ((binditem.Inb_itm_cd == seritem.Tus_itm_cd) && (binditem.Inb_itm_line == seritem.Tus_itm_line))
                                    {
                                        dttemp2.Rows.Add(binditem.Inb_seq_no, binditem.Inb_itm_line, binditem.Inb_itm_cd, seritem.Tus_itm_desc, seritem.Tus_itm_model, seritem.Tus_itm_brand, binditem.Inb_qty, binditem.Itb_bal_qty1, "", binditem.Inb_loc, "", binditem.Inb_unit_price);

                                        txtUserSeqNo.Text = binditem.Inb_seq_no.ToString();
                                    }
                                }
                            }
                        }
              

                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        Int32 count = 0;

                        grdItems.DataSource = new int[]{};
                        grdItems.DataBind();

                        DataTable uniqueCols = RemoveDuplicateRows(dttemp2, "PODI_ITM_CD");

                        grdItems.DataSource = uniqueCols;
                        grdItems.DataBind();

                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _serList;
                        grdSerial.DataBind();

                        foreach (var item in _scanItems)
                        {
                            count = item.theCount;
                            string itemcode = item.Peo.Tus_itm_cd;

                            foreach (GridViewRow row in grdItems.Rows)
                            {
                                Label l1 = (Label)row.FindControl("lblitri_itm_cd");
                                Label l2 = (Label)row.FindControl("lblpickqty");
                                if (itemcode == l1.Text)
                                {
                                    l2.Text = count.ToString();
                                }
                            }
                        }

                        foreach (GridViewRow row1 in grdItems.Rows)
                        {
                            Label la = (Label)row1.FindControl("lblitri_qty");
                            Label lb = (Label)row1.FindControl("lblremainqty");
                            Label lc = (Label)row1.FindControl("lblpickqty");

                            la.Text = DoFormat(Convert.ToDecimal(la.Text));
                            lb.Text = DoFormat(Convert.ToDecimal(lb.Text));
                            lc.Text = DoFormat(Convert.ToDecimal(lc.Text));
                        }
                    }
                    else
                    {
                        txtFindPONoDoc.Text = "";
                        txtFindPONoDoc.Focus();
                        return;
                    }
                    #endregion

                    Session["SERIALLIST"] = _serList;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName1)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[colName1])))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName1], string.Empty);
                }
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
            {
                dTable.Rows.Remove(dRow);
            }
            //Datatable which contains unique records will be return as output.
            return dTable;
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                UserDPopoup.Hide();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POrderFastWeb);
                DataTable result = CHNLSVC.CommonSearch.SearchPurchaseOrdersFast(SearchParams, null, null);

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[4] { new DataColumn("PO No"), new DataColumn("Ref No"), new DataColumn("Date"), new DataColumn("Status")});

                foreach (DataRow ddr in result.Rows)
                {
                    string doc = ddr["PO No"].ToString();
                    string subtp = ddr["Ref No"].ToString();
                    string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, stus);
                }

                grdResult.DataSource = dtresultcopyMRN;
                grdResult.DataBind();
                lblvalue.Text = "424";
                BindUCtrlDDLDataPOREQUEST(dtresultcopyMRN);
                ViewState["SEARCH"] = dtresultcopyMRN;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void RecallData(string Name)
        {
            try
            {
                Name = txtFindPONoDoc.Text.Trim();
                if (lblvalue.Text == "Doc")
                {
                    txtFindPONoDoc.Text = Name;
                    lblvalue.Text = "";
                    Session["Doc"] = null;
                    Session["TempDoc"] = null;
                    Session["DocType"] = "Doc";
                    LoadHeader(false);
                    GetDocData(Name);

                    foreach (GridViewRow hiderowbtn in this.grdSerial.Rows)
                    {
                        LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtnadd");

                        _delbutton.Enabled = false;
                        _delbutton.CssClass = "buttoncolor";
                        _delbutton.OnClientClick = "return Enable();";
                    }

                    foreach (GridViewRow hiderowbtn2 in this.grdItems.Rows)
                    {
                        LinkButton _delbutton = (LinkButton)hiderowbtn2.FindControl("lbtnadd");

                        _delbutton.Enabled = false;
                        _delbutton.CssClass = "buttoncolor";
                        _delbutton.OnClientClick = "return Enable();";
                    }

                    UserDPopoup.Hide();

                    DataTable _resultNew = CHNLSVC.Inventory.GetPoQty(txtPONo.Text, 0);

                    foreach (GridViewRow gvrow in grdItems.Rows)
                    {
                        Label code = (Label)gvrow.FindControl("lblitri_itm_cd");
                        Label line = (Label)gvrow.FindControl("lblpodiline");
                        foreach (DataRow ddr22 in _resultNew.Rows)
                        {
                            if ((code.Text == ddr22["POD_ITM_CD"].ToString()))
                            {
                                Label poqty = (Label)gvrow.FindControl("lblitri_qty");
                                poqty.Text = ddr22["POD_QTY"].ToString();

                                Label picqty = (Label)gvrow.FindControl("lblpickqty");

                                Decimal POQTY = Convert.ToDecimal(poqty.Text);
                                Decimal PICQTY = Convert.ToDecimal(picqty.Text);
                                Decimal remainqty = POQTY - PICQTY;
                                Label remaqty = (Label)gvrow.FindControl("lblremainqty");
                                remaqty.Text = remainqty.ToString();
                            }
                        }
                    }

                    foreach (GridViewRow gvrow in grdItems.Rows)
                    {
                        Label q1 = (Label)gvrow.FindControl("lblitri_qty");
                        q1.Text = DoFormat(Convert.ToDecimal(q1.Text));

                        Label q2 = (Label)gvrow.FindControl("lblremainqty");
                        q2.Text = DoFormat(Convert.ToDecimal(q2.Text));

                        Label q3 = (Label)gvrow.FindControl("lblpickqty");
                        q3.Text = DoFormat(Convert.ToDecimal(q3.Text));
                    }
                    return;
                }
                if (lblvalue.Text == "TempDoc")
                {
                    txtFindPONoDoc.Text = Name;
                    lblvalue.Text = "";
                    Session["TempDoc"] = "";
                    Session["DocType"] = "TempDoc";
                    LoadHeader(true);

                    GetTempDocData(Name);

                    foreach (GridViewRow hiderowbtn in this.grdSerial.Rows)
                    {
                        LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtnadd");

                        _delbutton.Enabled = false;
                        _delbutton.CssClass = "buttoncolor";
                        _delbutton.OnClientClick = "return Enable();";
                    }

                    foreach (GridViewRow hiderowbtn2 in this.grdItems.Rows)
                    {
                        LinkButton _delbutton = (LinkButton)hiderowbtn2.FindControl("lbtnadd");

                        _delbutton.Enabled = false;
                        _delbutton.CssClass = "buttoncolor";
                        _delbutton.OnClientClick = "return Enable();";
                    }

                    dvPendingPO_SelectedIndexChanged(null, null);

                    DataTable _resultNew = CHNLSVC.Inventory.GetPoQty(txtPONo.Text, 0);

                    foreach (GridViewRow gvrow in grdItems.Rows)
                    {
                        Label code = (Label)gvrow.FindControl("lblitri_itm_cd");
                        Label line = (Label)gvrow.FindControl("lblpodiline");
                        foreach (DataRow ddr22 in _resultNew.Rows)
                        {
                            if ((code.Text == ddr22["POD_ITM_CD"].ToString()))
                            {
                                Label poqty = (Label)gvrow.FindControl("lblitri_qty");
                                poqty.Text = ddr22["POD_QTY"].ToString();

                                Label picqty = (Label)gvrow.FindControl("lblpickqty");

                                Decimal POQTY = Convert.ToDecimal(poqty.Text);
                                Decimal PICQTY = Convert.ToDecimal(picqty.Text);
                                Decimal remainqty = Convert.ToDecimal(ddr22["POD_GRN_BAL"].ToString());
                                Label remaqty = (Label)gvrow.FindControl("lblremainqty");
                                Decimal remainqtyactual = remainqty - PICQTY;
                                remaqty.Text = remainqtyactual.ToString();
                            }
                        }
                    }

                    foreach (GridViewRow gvrow in grdItems.Rows)
                    {
                        Label q1 = (Label)gvrow.FindControl("lblitri_qty");
                        q1.Text = DoFormat(Convert.ToDecimal(q1.Text));

                        Label q2 = (Label)gvrow.FindControl("lblremainqty");
                        q2.Text = DoFormat(Convert.ToDecimal(q2.Text));

                        Label q3 = (Label)gvrow.FindControl("lblpickqty");
                        q3.Text = DoFormat(Convert.ToDecimal(q3.Text));
                    }


                    UserDPopoup.Hide();
                    return;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void txtFindPONoDoc_TextChanged(object sender, EventArgs e)
        {
            string contain = txtFindPONoDoc.Text.Trim();
            if (!contain.Contains("TEMP"))
            {
                lblvalue.Text = "Doc";
                chktemp.Checked = false;
            }
            else
            {
                lblvalue.Text = "TempDoc";
                chktemp.Checked = true;
            }
            RecallData(txtFindPONoDoc.Text.Trim());
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            UserDPopoup.Hide();
            Session["Doc"] = null;
            Session["TempDoc"] = null;
            Session["SENT_TO_CRN"] = null;
        }

        protected void btncClose_Click(object sender, EventArgs e)
        {
            try
            {
                MPPDA.Hide();
                ddlloadingbay.SelectedIndex = 0;
                txtdocname.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
        }

        protected void ddlloadingbay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MPPDA.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            if (txtpdasend.Value == "Yes")
            {
                try
                {
                    SaveData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                    CHNLSVC.CloseChannel();
                }
                finally
                {
                    CHNLSVC.CloseAllChannels();
                }
            }
        }

        private void SaveData()
        {
            try
            {
                chkpda.Checked = false;
                Int32 val = 0;
                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                

                if (string.IsNullOrEmpty(txtdocname.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the document no !!!')", true);
                    txtdocname.Focus();
                    MPPDA.Show();
                    return;
                }

                if (ddlloadingbay.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the loading bay !!!')", true);
                    ddlloadingbay.Focus();
                    MPPDA.Show();
                    return;
                }

                Int32 _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString());
                _userid = (string)Session["UserID"];
               

                #region Header

                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(txtdocname.Text.Trim(), "ADJ", 1, Session["UserCompanyCode"].ToString());

                if (dtdoccheck.Rows.Count > 0)
                {
                    foreach (DataRow ddr in dtdoccheck.Rows)
                    {
                        string seqNo = ddr["tuh_usrseq_no"].ToString();
                        _userSeqNo = Convert.ToInt32(seqNo);
                    }
                }

                DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(_userSeqNo);

                if (dtchkitm.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                    return;
                }
               
                if (dtdoccheck.Rows.Count == 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _inputReptPickHeader.Tuh_usr_id = _userid;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "ADJ";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
                else
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = "ADJ";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }

                #endregion
                #region Add by Lakshan 01/Sep/2016
                DataTable _dtChkAlreadyEx = CHNLSVC.Inventory.CheckItemsScannedStatus(_userSeqNo);

                if (_dtChkAlreadyEx.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                    return;
                }
                else
                {
                    DataTable po_items = new DataTable();
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtdocname.Text, 2);
                    if (po_items.Rows.Count == 0)
                    {
                        po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtdocname.Text, 1);
                    }
                    if (po_items.Rows.Count > 0)
                    {
                        //06/Apr/2016 comment by Rukshan (chamal request) 

                        List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                        foreach (DataRow _row in po_items.Rows)
                        {
                            string _item = _row["PODI_ITM_CD"].ToString();
                            string _cost = _row["UNIT_PRICE"].ToString();
                            string _qty = _row["PODI_QTY"].ToString();
                            string _line = _row["PODI_LINE_NO"].ToString();
                            string _STATUS = _row["POD_ITM_STUS"].ToString();
                            //AddItem(_item, _cost, null, null, user_seq_num.ToString(), null);

                            ReptPickItems _reptitm = new ReptPickItems();
                            _reptitm.Tui_usrseq_no = _userSeqNo;
                            _reptitm.Tui_req_itm_qty = Convert.ToDecimal(_qty);
                            _reptitm.Tui_req_itm_cd = _item;
                            _reptitm.Tui_req_itm_stus = "CONS";
                            _reptitm.Tui_pic_itm_cd = _line;
                            // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                            _reptitm.Tui_pic_itm_qty = 0;
                            _saveonly.Add(_reptitm);


                        }
                        val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                    }
                }
                #endregion
                #region Items

                //Int32 rownumber = 0;
                //Int32 rowvalline = 0;

                //foreach (GridViewRow row in gvItem.Rows)
                //{
                //    Label lblitem = (Label)row.FindControl("lblitem");
                //    Label lblstatus = (Label)row.FindControl("lblstatus");
                //    Label lblqty = (Label)row.FindControl("lblqty");

                //    ReptPickItems _items = new ReptPickItems();

                //    _items.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                //    _items.Tui_req_itm_cd = lblitem.Text.Trim();
                //    _items.Tui_req_itm_stus = lblstatus.Text.Trim();
                //    _items.Tui_pic_itm_qty = 0;
                //    _items.Tui_req_itm_qty = Convert.ToDecimal(lblqty.Text.Trim());
                //    _items.Tui_pic_itm_cd = rownumber.ToString();
                //    _items.Tui_pic_itm_stus = string.Empty;
                //    _items.Tui_grn = string.Empty;
                //    _items.Tui_batch = string.Empty;
                //    _items.Tui_sup = string.Empty;

                //    valitem = CHNLSVC.Inventory.UpdatePickItem(_items);

                //    DataTable dtrownum = CHNLSVC.Inventory.LoadCurrentRowNumber(Convert.ToInt32(_userSeqNo), warehousecom, warehouseloc, ddlloadingbay.SelectedValue);
                //    foreach (DataRow ddrrownum in dtrownum.Rows)
                //    {
                //        rownumber = Convert.ToInt32(ddrrownum["RowCount"].ToString());
                //    }

                //    ReptPickItems _itemslines = new ReptPickItems();

                //    _itemslines.Tui_pic_itm_cd = rownumber.ToString();
                //    _itemslines.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                //    _itemslines.Tui_req_itm_cd = lblitem.Text.Trim();
                //    _itemslines.Tui_req_itm_stus = lblstatus.Text.Trim();

                //    rowvalline = CHNLSVC.Inventory.UpdatePickItemLine(_itemslines);

                //    if (Convert.ToInt32(valitem) == -1)
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                //        CHNLSVC.CloseChannel();
                //        return;
                //    }

                //    if (Convert.ToInt32(rowvalline) == -1)
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                //        CHNLSVC.CloseChannel();
                //        return;
                //    }
                //}
                #endregion

                if (dtdoccheck.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully updated !!!')", true);
                }
                ClearScreen();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnprint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["documntNo"] = txtFindPONoDoc.Text.ToString();
                Session["GlbReportType"] = "SCM1_CONSIN";
                Session["GlbReportName"] = "Inward_Docs_Consign.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                clsInventory obj = new clsInventory();
                obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                PrintPDF(targetFileName, obj._consIn);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch(Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Consignment Receive Note Print", "ConsignmentReceiveNote", ex.Message, Session["UserID"].ToString());
            }
        }

        protected void btnSentScan_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 val = 0;
                DataTable po_items = new DataTable();
                // po_items = ViewState["po_items"] as DataTable;
                //  if(po_items==null){
                
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
                
             
                    foreach (DataRow drValue in po_items.Rows)
                    {
                        if (drValue["podi_bal_qty"].ToString() == "0")
                        {
                            drValue.Delete();
                        }

                    }

                    po_items.AcceptChanges();
                
                Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo("ADJ", txtPONo.Text);
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "ADJ";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtPONo.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    // _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    // _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (val == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
                else
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_doc_no = txtPONo.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = "ADJ";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    //_inputReptPickHeader.Tuh_wh_com = warehousecom;
                    //_inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
                DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(user_seq_num);

                if (dtchkitm.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                    return;
                }
                else
                {
                    if (po_items.Rows.Count > 0)
                    {
                        //06/Apr/2016 comment by Rukshan (chamal request) 

                        List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                        foreach (DataRow _row in po_items.Rows)
                        {
                            string _item = _row["PODI_ITM_CD"].ToString();
                            string _cost = _row["UNIT_PRICE"].ToString();
                            string _qty = _row["PODI_QTY"].ToString();
                            string _line = _row["PODI_LINE_NO"].ToString();
                            string _STATUS = _row["POD_ITM_STUS"].ToString();
                            //AddItem(_item, _cost, null, null, user_seq_num.ToString(), null);

                            ReptPickItems _reptitm = new ReptPickItems();
                            _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                            _reptitm.Tui_req_itm_qty = Convert.ToDecimal(_qty);
                            _reptitm.Tui_req_itm_cd = _item;
                            _reptitm.Tui_req_itm_stus = _STATUS;
                            _reptitm.Tui_pic_itm_cd = _line;
                            // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                            _reptitm.Tui_pic_itm_qty = 0;
                            _saveonly.Add(_reptitm);


                        }
                        val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                    }

                }
                if (val == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                    MPPDA.Hide();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
        }


        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo(string DocumentType, string _scanDocument)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = DocumentType;
            RPH.Tuh_cre_dt = DateTime.Now;// DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change 
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = true; //direction always (-) for change status
            RPH.Tuh_doc_no = _scanDocument;
            //write entry to TEMP_PICK_HDR
            //int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            //if (affected_rows == 1)
            //{
            return generated_seq;
            //}
            //else
            //{
            //    return 0;
            //}
        }
        #endregion

    }
}