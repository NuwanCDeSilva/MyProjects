using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Sales;
using FF.BusinessObjects;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Finance
{
    public partial class DebitNote : BasePage //System.Web.UI.Page
    {
        protected MasterBusinessEntity _masterBusinessCompany { get { return (MasterBusinessEntity)Session["_masterBusinessCompany"]; } set { Session["_masterBusinessCompany"] = value; } }
        protected List<PriceDefinitionRef> _PriceDefinitionRef { get { return (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"]; } set { Session["_PriceDefinitionRef"] = value; } }
        protected string DefaultInvoiceType { get { return (string)Session["DefaultInvoiceType"]; } set { Session["DefaultInvoiceType"] = value; } }
        protected MasterProfitCenter _MasterProfitCenter { get { return (MasterProfitCenter)Session["_MasterProfitCenter"]; } set { Session["_MasterProfitCenter"] = value; } }
        protected PriceBookLevelRef _priceBookLevelRef { get { return (PriceBookLevelRef)Session["_priceBookLevelRef"]; } set { Session["_priceBookLevelRef"] = value; } }
        protected List<PriceBookLevelRef> _priceBookLevelRefList { get { return (List<PriceBookLevelRef>)Session["_priceBookLevelRefList"]; } set { Session["_priceBookLevelRefList"] = value; } }
        protected DateTime _serverDt { get { return (DateTime)Session["_serverDt"]; } set { Session["_serverDt"] = value; } }
        protected bool _isStrucBaseTax { get { Session["_isStrucBaseTax "] = (Session["_isStrucBaseTax "] == null) ? false : (bool)Session["_isStrucBaseTax "]; ; return (bool)Session["_isStrucBaseTax "]; } set { Session["_isStrucBaseTax "] = value; } }

        private List<DebitNotes> _DebitNotes = new List<DebitNotes>();
        private List<DebitNoteHdr> _DebitNoteHdr = new List<DebitNoteHdr>();
        private decimal _totLineAmt = 0;
        decimal _totUnitAmt = 0;
        public decimal TotLineAmt
        {
            get
            {
                return _totLineAmt;
            }
            set
            {
                _totLineAmt = value;
            }
        }

        List<decimal> _totUnitAmt_list = new List<decimal>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["GlbModuleName"] = "m_Trans_Finance_DebitNote";
            if (!IsPostBack)
            {
                LoadCachedObjects();
                BindInvType();
                grdInvoices.DataSource = _DebitNotes;
                grdInvoices.DataBind();

                MasterCompany _masterComp = null;
                _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
                lblInvType.Text = "DEBT";
            }
        }

        //Customer Searching
        protected void lbtnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                result.Columns.RemoveAt(4);
                //result.Columns.RemoveAt(5);
                grdResult.DataSource = result;
                ViewState["SEARCH"] = result;
                grdResult.DataBind();
                lblvalue.Text = "CU";
                BindUCtrlDDLData(result);
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void DisplayMessage(String Msg, Int32 option)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", "");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
            else if (option == 5)
            {
                // WriteErrLog(Msg);
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

        protected void SaveDebitNote()
        {
            Int32 row_aff = 0;
            string _docNo = string.Empty;
            int lNo = 1;
            Int32 InvoiceSeqNo = (Int32)CHNLSVC.Inventory.GetSerialID();
            DataTable _dtl = CHNLSVC.General.SearchCustomerData(txtCus.Text);
            if (Convert.ToDateTime(txtDate.Text) != DateTime.Now.Date)
            {
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDate, lblH1, txtDate.Text, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                        {
                            txtDate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction!');", true);
                            //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        txtDate.Enabled = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction!');", true);
                        //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                        return;
                    }
                }
            }

            DebitNoteHdr _dbtNotehdr = new DebitNoteHdr();
            _dbtNotehdr.InvoiceSeqNo = InvoiceSeqNo;
            _dbtNotehdr.ComCode = Session["UserCompanyCode"].ToString();
            _dbtNotehdr.ProfitCenter = Session["UserDefProf"].ToString();
            _dbtNotehdr.InvoiceType = txtInvoice.Text != "" ? ViewState["InvType"].ToString() : cmbInvType.SelectedValue;
            _dbtNotehdr.InvSubType = txtInvoice.Text != "" ? ViewState["InvSubType"].ToString() : "";
            _dbtNotehdr.InvoiceNo = ViewState["HInvoiceNo"].ToString();
            _dbtNotehdr.sessionCreateBy = Session["UserID"].ToString();
            _dbtNotehdr.sessionCreateDate = DateTime.Now;
            _dbtNotehdr.sessionModBy = Session["UserID"].ToString();
            _dbtNotehdr.sessionModDate = DateTime.Now;
            _dbtNotehdr.SessionId = Session["SessionID"].ToString();
            _dbtNotehdr.Date = Convert.ToDateTime(txtDate.Text);
            _dbtNotehdr.CusCode = ViewState["HCusCode"] == null ? string.Empty : ViewState["HCusCode"].ToString();
            _dbtNotehdr.SalesExCode = ViewState["HExecutive"] == null ? string.Empty : ViewState["HExecutive"].ToString();
            _dbtNotehdr.CusName = ViewState["cusName"] == null ? string.Empty : ViewState["cusName"].ToString();
            _dbtNotehdr.CusAdd1 = ViewState["cusAdd1"] == null ? string.Empty : ViewState["cusAdd1"].ToString();
            _dbtNotehdr.CusAdd2 = ViewState["cusAdd2"] == null ? string.Empty : ViewState["cusAdd2"].ToString();

            _dbtNotehdr.InvAmt = Convert.ToDecimal(txtTotAmt.Text); //_InvAmt;
            _dbtNotehdr.InvAmtPaid = Convert.ToDecimal(txtPaidAmt.Text); //_InvAmtPaid;
            _dbtNotehdr.LineAmt = Convert.ToDecimal(lblLineAmt.Text); //_totLineAmt;

            _dbtNotehdr.ManagerCode = txtInvoice.Text != "" ? ViewState["managerCode"].ToString() : _MasterProfitCenter.Mpc_man;
            _dbtNotehdr.TaxInv = ViewState["sah_tax_inv"] != null ? Convert.ToDecimal(ViewState["sah_tax_inv"].ToString()) : Convert.ToDecimal(_dtl.AsEnumerable().FirstOrDefault().Field<Int16>("mbe_is_tax"));
            _dbtNotehdr.TaxExecpted = (lblVatExemptStatus.Text == "No" ? 0 : 1);//txtInvoice.Text != "" ? Convert.ToDecimal(ViewState["sah_tax_exempted"].ToString()) : (lblVatExemptStatus.Text == "No" ? 0 : 1);
            _dbtNotehdr.IsSvat = ViewState["sah_is_svat"] != null ? Convert.ToDecimal(ViewState["sah_is_svat"].ToString()) : Convert.ToDecimal(_dtl.AsEnumerable().FirstOrDefault().Field<Int16>("mbe_is_svat"));
            _dbtNotehdr.DCustName = ViewState["sah_d_cust_name"] != null ? ViewState["sah_d_cust_name"].ToString() : (ViewState["cusName"] == null ? string.Empty : ViewState["cusName"].ToString());

            _DebitNoteHdr.Add(_dbtNotehdr);

            foreach (GridViewRow row in grdInvoices.Rows)
            {
                DataTable _result = null;
                _result = CHNLSVC.Financial.getInvoiceDetailsValues((row.FindControl("col_Item") as Label).Text, (row.FindControl("col_InvoiceNo") as Label).Text);
                string autoGeReceiptNo = UpdateAutoNo("RECEIPT", 0, "VHREG", "PC", Session["UserDefProf"].ToString(), DateTime.Now, DateTime.Now.Year);
                DebitNotes _dbtNote = new DebitNotes();
                _dbtNote.Item = (row.FindControl("col_Item") as Label).Text;
                _dbtNote.Description = (row.FindControl("col_Description") as Label).Text;
                _dbtNote.Qty = Convert.ToDecimal((row.FindControl("col_Qty") as Label).Text);
                _dbtNote.UnitPrice = Convert.ToDecimal((row.FindControl("col_UnitPrice") as Label).Text);
                _dbtNote.Total = Convert.ToDecimal((row.FindControl("col_Total") as Label).Text);
                _dbtNote.DisRate = Convert.ToDecimal((row.FindControl("col_DisRate") as Label).Text);
                _dbtNote.DisAmt = Convert.ToDecimal((row.FindControl("col_DisAmt") as Label).Text);
                _dbtNote.Tax = Convert.ToDecimal((row.FindControl("col_TaxAmt") as Label).Text);
                _dbtNote.LineAmt = Convert.ToDecimal((row.FindControl("col_LineAmt") as Label).Text);

                _dbtNote.ComCode = Session["UserCompanyCode"].ToString();
                _dbtNote.ProfitCenter = Session["UserDefProf"].ToString();
                _dbtNote.CusCode = (row.FindControl("col_CusCode") as Label).Text;
                _dbtNote.SalesExCode = (row.FindControl("col_SalesExCode") as Label).Text;

                //_dbtNote.DebitNoteNo = (row.FindControl("col_DebitNoteNo") as Label).Text;
                _dbtNote.Date = Convert.ToDateTime((row.FindControl("col_Date") as Label).Text);
                _dbtNote.InvoiceNo = (row.FindControl("col_InvoiceNo") as Label).Text;
                _dbtNote.InvoiceType = (row.FindControl("col_InvoiceType") as Label).Text;
                _dbtNote.InvAmt = Convert.ToDecimal((row.FindControl("col_InvAmt") as Label).Text);
                _dbtNote.InvAmtPaid = Convert.ToDecimal((row.FindControl("col_InvAmtPaid") as Label).Text);
                _dbtNote.InvAmtBal = Convert.ToDecimal((row.FindControl("col_InvAmtBal") as Label).Text);

                _dbtNote.sessionCreateBy = Session["UserID"].ToString();
                _dbtNote.sessionCreateDate = DateTime.Now;
                _dbtNote.sessionModBy = Session["UserID"].ToString();
                _dbtNote.sessionModDate = DateTime.Now;
                _dbtNote.SessionId = Session["SessionID"].ToString();
                _dbtNote.InvoiceSeqNo = InvoiceSeqNo;
                _dbtNote.CusName = (row.FindControl("col_CusName") as Label).Text;
                _dbtNote.CusAdd1 = (row.FindControl("col_CusAdd1") as Label).Text;
                _dbtNote.CusAdd2 = (row.FindControl("col_CusAdd2") as Label).Text;
                _dbtNote.ReceiptNo = autoGeReceiptNo;
                _dbtNote.lineNo = lNo;
                _dbtNote.TaxRate = Convert.ToDecimal(ViewState["_taxRate"]);
                if (_result.Rows.Count > 0 && (txtInvoice.Text != null && txtInvoice.Text != ""))
                {
                    _dbtNote.sad_itm_stus = (from DataRow dr in _result.Rows select dr["sad_itm_stus"]).FirstOrDefault().ToString();
                    _dbtNote.sad_uom = (from DataRow dr in _result.Rows select dr["sad_uom"]).FirstOrDefault().ToString();
                    _dbtNote.ItemTp = (from DataRow dr in _result.Rows select dr["sad_itm_tp"]).FirstOrDefault().ToString();
                }
                else
                {
                    _dbtNote.sad_itm_stus = (from DataRow dr in _result.Rows select dr["mi_itm_stus"]).FirstOrDefault().ToString();
                    _dbtNote.sad_uom = (from DataRow dr in _result.Rows select dr["mi_itm_uom"]).FirstOrDefault().ToString();
                    _dbtNote.ItemTp = (from DataRow dr in _result.Rows select dr["mi_itm_tp"]).FirstOrDefault().ToString();
                }
                _DebitNotes.Add(_dbtNote);
                lNo++;
            }
            try
            {
                row_aff = (Int32)CHNLSVC.Financial.saveDebitNote(_DebitNoteHdr, _DebitNotes, GenerateMasterAutoNumber(), out _docNo);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            if (row_aff >= 1)
            {
                DisplayMessage("Successfully Saved The Debit Note " + _docNo + " Details...!", 3);
                try
                {
                    Session["documntNo"] = _docNo;
                    Label1.Text = "Do you want print now?";
                    popUpPrint.Show();
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message, 4);
                }
                ClearDebits(true, false);
            }
            else
            {
                DisplayMessage("Error In Process, Please contact IT...!", 4);
            }
            cleanViewState();
            txtCus.ReadOnly = false;
            txtExecutive.ReadOnly = false;
        }

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_cate_cd = Session["UserDefProf"].ToString(); // string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            masterAuto.Aut_direction = 0;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "REV";
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = "INDBN";
            masterAuto.Aut_year = null;
            return masterAuto;
        }

        protected void cleanViewState()
        {
            ViewState["StoreDN"] = string.Empty;
            ViewState["HCusCode"] = null;
            ViewState["HExecutive"] = null;
            ViewState["sah_d_cust_name"] = null;
            ViewState["sah_is_svat"] = null;
            ViewState["sah_tax_inv"] = null;
            ViewState["cusName"] = null;
            ViewState["cusAdd1"] = null;
            ViewState["cusAdd2"] = null;
            ViewState["_taxRate"] = null;
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
                //if (_bdt != null) _lblcontrol.Text = "This module is back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
                    //_dtpcontrol.Value = _bdt.Gad_act_to_dt.Date; //TEMPORAY BLOCK - CHAMAL 06-05-2014 , BLOCK REMOVE BY CHAMAL  31-12-2014
                    //Again removeed because POSTING time its gone 31 st or 30 date
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
                if (CHNLSVC.Security.IsSessionExpired(Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), out _expMsg) == true)
                {
                    // MessageBox.Show("Current Session is expired or has been closed by administrator!", "Fast Forward - SCM-II", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    BaseCls.GlbIsExit = true;
                    //Application.Exit();
                    GC.Collect();
                }
            }
        }

        //grdResult Searching
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            try
            {
                //lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
                lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Yes" : "No";//"Available" : "None";
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        //grdResult Searching Method
        protected void FilterData()
        {
            if (lblvalue.Text == "CU")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = result;//grdResult
                result.Columns.RemoveAt(4);
                //result.Columns.RemoveAt(5);
                grdResult.DataBind();
                lblvalue.Text = "CU";
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                //txtSearchbyword.Focus();
                //txtSearchbyword.Text = string.Empty;
            }
            else if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchInvoiceWeb(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim(), (txtSearchbywordD.Text.Trim() != null || txtSearchbywordD.Text.Trim() != null) ? DateTime.MinValue : Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                DataView dv = new DataView(result);
                dv.Sort = "Invoice No ASC";
                grdResultD.DataSource = dv.ToTable();
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            else if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.creditNoteVirtualItems(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim(), "S");
                grdResult.DataSource = result;//grdResult
                grdResult.DataBind();
                lblvalue.Text = "Item";
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
                //txtSearchbyword.Text = string.Empty;
            }
            else if (lblvalue.Text == "Sale_Ex")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Sale_Ex";
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                txtSearchbyword.Text = string.Empty;
            }
            if (lblvalue.Text == "srn")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchReversalInvoiceForDebit(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);//SearchReversalInvoice
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srn";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        //Invoice grid Searching
        protected void lbtnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                #region date validate
                //if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(17|20)\\d\\d$"))
                //{

                //}
                //else
                //{
                //    txtFDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                //    string _Msg = "Please enter valid date.";
                //    DisplayMessage(_Msg, 1);
                //}
                //if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(17|20)\\d\\d$"))
                //{

                //}
                //else
                //{
                //    txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                //    string _Msg = "Please enter valid date.";
                //    DisplayMessage(_Msg, 1);
                //}
                #endregion

                bool vDate = IsDateTime(txtDate.Text);
                if (!vDate)
                {
                    DisplayMessage("Please enter a valid Date");
                }
                txtFDate.Text = DateTime.Now.ToShortDateString();
                txtTDate.Text = DateTime.Now.ToShortDateString();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchInvoiceWeb(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date, Convert.ToDateTime(txtDate.Text).Date);
                DataView dv = new DataView(result);
                dv.Sort = "Invoice No DESC";

                grdResultD.DataSource = dv.ToTable();
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Text = "";
                txtSearchbywordD.Focus();

                //if (Convert.ToDecimal(txtBalAmt.Text) <= 0)
                //{
                //    creNote.visible = false;
                //}
                //else
                //    creNote.visible = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
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

        protected void btnCloseSearchMP_Click(object sender, EventArgs e)
        {
            Session["SIPopup"] = null;
            SIPopup.Hide();
            txtSearchbyword.Text = "";
        }

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
                DisplayMessage(ex.Message, 4);
            }
        }

        //Item and Customer Search Selected Index Change
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "CU")
            {
                txtCus.Text = grdResult.SelectedRow.Cells[1].Text;
                ViewState["cusName"] = grdResult.SelectedRow.Cells[2].Text;
                ViewState["cusAdd1"] = grdResult.SelectedRow.Cells[3].Text;
                ViewState["cusAdd2"] = grdResult.SelectedRow.Cells[4].Text;
                LoadCustomerDetailsByCustomer();
                txtSearchbyword.Text = string.Empty;
            }
            if (lblvalue.Text == "Item")
            {
                txtQty.Text = FormatToCurrency("0.00");
                txtUnitPrice.Text = FormatToCurrency("0.00");
                txtDisRate.Text = FormatToCurrency("0.00");
                txtUnitAmt.Text = FormatToCurrency("0.00");
                txtDisAmt.Text = FormatToCurrency("0.00");
                txtTaxAmt.Text = FormatToCurrency("0.00");
                txtLineTotAmt.Text = FormatToCurrency("0.00");

                txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                txtDescription.Text = grdResult.SelectedRow.Cells[2].Text.Replace("&quot;", "''").Replace("&amp;", "&");
                txtDescription.ToolTip = txtDescription.Text;
                txtSearchbyword.Text = string.Empty;
            }
            if (lblvalue.Text == "Sale_Ex")
            {
                txtExecutive.Text = grdResult.SelectedRow.Cells[2].Text;
            }
        }

        private void ClearDebits(bool invGrdClean, bool isHdr)
        {
            try
            {
                if (!isHdr)
                {
                    txtCus.Text = string.Empty;
                    lblVatExemptStatus.Text = string.Empty;
                    txtExecutive.Text = string.Empty;
                    txtInvoice.Text = string.Empty;
                    txtTotAmt.Text = FormatToCurrency("0.00");
                    txtPaidAmt.Text = FormatToCurrency("0.00");
                    txtBalAmt.Text = FormatToCurrency("0.00");
                }
                //txtCreNote.Text = string.Empty;
                txtDate.Text = DateTime.Now.ToShortDateString();

                txtUnitPrice.Text = FormatToCurrency("0.00");
                txtUnitAmt.Text = FormatToCurrency("0.00");



                txtItem.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtQty.Text = FormatToCurrency("0.00");
                txtDisRate.Text = FormatToCurrency("0.00");
                txtDisAmt.Text = FormatToCurrency("0.00");
                txtTaxAmt.Text = FormatToCurrency("0.00");
                txtLineTotAmt.Text = FormatToCurrency("0.00");
                if (invGrdClean)
                {
                    grdInvoices.DataSource = null;
                    grdInvoices.DataBind();

                    lbldisAmt.Text = string.Empty;
                    lbltaxAmt.Text = string.Empty;
                    lblLineAmt.Text = string.Empty;
                    lblTot.Text = string.Empty;
                }
                LoadCachedObjects();
                _serverDt = DateTime.Now;
                //bool _allowCurrentTrans = false;
                if (Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                txtDbtNte.Text = string.Empty;
                //IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, Session["GlbModuleName"].ToString(), txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        protected void LoadCustomerDetailsByCustomer()
        {
            if (string.IsNullOrEmpty(txtCus.Text)) return;
            try
            {
                if (cmbInvType.Text.Trim() == "CRED" && txtCus.Text.Trim() == "CASH")
                {
                    DisplayMessage("You cannot select customer as CASH, because your invoice type is " + cmbInvType.Text);
                    ClearDebits(false, false);
                    txtCus.Focus();
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();

                if (!string.IsNullOrEmpty(txtCus.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCus.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
                    ViewState["cusName"] = _masterBusinessCompany.Mbe_name;
                    ViewState["cusAdd1"] = _masterBusinessCompany.Mbe_add1;
                    ViewState["cusAdd2"] = _masterBusinessCompany.Mbe_add2;
                    txtCus.ToolTip = _masterBusinessCompany.Mbe_name;
                }
                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_tp != "C")
                    {
                        DisplayMessage("Selected Customer is not allow for enter transaction");
                        ClearDebits(false, false);
                        txtCus.Focus();
                        return;
                    }
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        DisplayMessage("This customer already inactive. Please contact IT dept");
                        ClearDebits(false, false);
                        txtCus.Focus();
                        return;
                    }
                    if (_masterBusinessCompany.Mbe_is_suspend == true)
                    {
                        DisplayMessageJS("This customer already suspended. Please contact IT dept");
                        ClearDebits(false, false);
                        txtCus.Focus();
                        return;
                    }
                    LoadTaxDetail(_masterBusinessCompany);
                }
                else
                {
                    DisplayMessage("Please enter a valid customer");
                    ClearDebits(false, false);
                    txtCus.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private Boolean IsInt(string stringToTest)
        {
            int result;
            return int.TryParse(stringToTest, out result);
        }
        private void LoadCachedObjects()
        {
            txtDate.Text = DateTime.Now.ToShortDateString();
            _PriceDefinitionRef = (List<PriceDefinitionRef>)Session["_PriceDefinitionRef_1"];
            _MasterProfitCenter = (MasterProfitCenter)Session["MasterProfitCenter_1"];
            _serverDt = CHNLSVC.Security.GetServerDateTime().Date;
        }

        protected void BindInvType()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    var _types = CHNLSVC.CommonSearch.creditNoteInvoiceType(SearchParams, null, null);
                    //_PriceDefinitionRef.Where(X => X.Sadd_com.Contains(Session["UserCompanyCode"].ToString())).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    //_PriceDefinitionRef.Where(X => X.Sadd_com.Contains(Session["UserCompanyCode"].ToString())).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    if (_types.Rows.Count > 0)
                    {
                        cmbInvType.DataSource = _types;
                        cmbInvType.DataTextField = "srtp_cd";
                        cmbInvType.DataValueField = "";
                        cmbInvType.DataBind();
                        cmbInvType.SelectedIndex = cmbInvType.Items.Count - 1;

                        if (cmbInvType.Text.Trim() == "CS")
                            txtCus.Text = "CASH";
                    }

                    if (!string.IsNullOrEmpty(DefaultInvoiceType))
                    {
                        cmbInvType.Text = DefaultInvoiceType;
                    }
                }
                else
                    cmbInvType.DataSource = null;
            else
                cmbInvType.DataSource = null;
        }

        //Invoice Popup Index Changing
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDbtNte.Text = string.Empty;
            txtInvoice.Text = string.Empty;
            lblInvType.Visible = true;

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            var InvCus = CHNLSVC.General.getInvoiceCustomer(Name);
            txtCus.Text = InvCus.FirstOrDefault().SAH_CUS_CD.ToString();
            ViewState["cusName"] = InvCus.FirstOrDefault().SAH_CUS_NAME.ToString();
            ViewState["cusAdd1"] = InvCus.FirstOrDefault().SAH_CUS_ADD1.ToString();
            ViewState["cusAdd2"] = InvCus.FirstOrDefault().SAH_CUS_ADD2.ToString();

            if (lblvalue.Text == "srn")
            {
                txtDbtNte.Text = grdResultD.SelectedRow.Cells[1].Text;
                List<DebitNotes> debtNte = CHNLSVC.Financial.DebitNoteDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), grdResultD.SelectedRow.Cells[1].Text == null ? string.Empty : grdResultD.SelectedRow.Cells[1].Text);
                if (debtNte.Count > 0)
                {
                    grdInvoices.DataSource = debtNte;
                    grdInvoices.DataBind();
                    gridTotCalculate();
                }
                DataTable _result = CHNLSVC.Financial.getInvoiceValues(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtDbtNte.Text);
                txtTotAmt.Text = (from DataRow dr in _result.Rows select Convert.ToDecimal(dr["TotalAmt"])).FirstOrDefault().ToString("N2"); //crdNte.FirstOrDefault().Total.ToString("N2");
                txtPaidAmt.Text = (from DataRow dr in _result.Rows select Convert.ToDecimal(dr["PaidAmt"])).FirstOrDefault().ToString("N2");//crdNte.FirstOrDefault().InvAmtPaid.ToString("N2");
                txtBalAmt.Text = (from DataRow dr in _result.Rows select Convert.ToDecimal(dr["ToBePaidAmt"])).FirstOrDefault().ToString("N2"); //crdNte.FirstOrDefault().InvAmtBal.ToString("N2");
                txtExecutive.Text = (from DataRow dr in _result.Rows select dr["SalesExCode"]).FirstOrDefault().ToString();
                txtInvoice.Text = (from DataRow dr in _result.Rows select dr["sah_inv_no"]).FirstOrDefault().ToString();
                lblVatExemptStatus.Text = (from DataRow dr in _result.Rows select dr["sah_tax_exempted"].ToString() == "0" ? "No" : "Yes").FirstOrDefault().ToString();
                cmbInvType.SelectedItem.Value = (from DataRow dr in _result.Rows select dr["sah_inv_tp"]).FirstOrDefault().ToString();
                txtDate.Text = (from DataRow dr in _result.Rows select Convert.ToDateTime(dr["sah_dt"]).ToShortDateString()).FirstOrDefault().ToString();
                lblvalue.Text = string.Empty;
            }
            if (lblvalue.Text == "InvoiceWithDate")
            {
                txtCus.ReadOnly = true;
                txtExecutive.ReadOnly = true;
                cmbInvType.Visible = false;
                lblInvType.Visible = true;
                lblInvType.Text = InvCus.FirstOrDefault().SAH_INV_TP.ToString();

                txtInvoice.Text = grdResultD.SelectedRow.Cells[1].Text;
                txtInvoiceNo_TextChanged(null, null);
                lblvalue.Text = "";
            }
            if (lblvalue.Text == "CreditWithDate")
            {
                txtDbtNte.Text = Name;
                lblvalue.Text = "";
                UserDPopoup.Hide();
                return;
            }
        }

        private Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            DataTable _result = null;
            try
            {
                _result = CHNLSVC.Financial.getInvoiceValues(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text);
                txtTotAmt.Text = (from DataRow dr in _result.Rows select Convert.ToDecimal(dr["TotalAmt"])).FirstOrDefault().ToString("N2");
                txtPaidAmt.Text = (from DataRow dr in _result.Rows select Convert.ToDecimal(dr["PaidAmt"])).FirstOrDefault().ToString("N2");
                txtBalAmt.Text = (from DataRow dr in _result.Rows select Convert.ToDecimal(dr["ToBePaidAmt"])).FirstOrDefault().ToString("N2");
                txtExecutive.Text = (from DataRow dr in _result.Rows select dr["SalesExCode"]).FirstOrDefault().ToString();
                ViewState["InvType"] = (from DataRow dr in _result.Rows select dr["InvType"]).FirstOrDefault().ToString();
                ViewState["InvSubType"] = (from DataRow dr in _result.Rows select dr["InvSubType"]).FirstOrDefault().ToString();

                ViewState["managerCode"] = (from DataRow dr in _result.Rows select dr["managerCode"]).FirstOrDefault().ToString();
                ViewState["sah_tax_inv"] = (from DataRow dr in _result.Rows select dr["sah_tax_inv"]).FirstOrDefault().ToString();
                ViewState["sah_tax_exempted"] = (from DataRow dr in _result.Rows select dr["sah_tax_exempted"]).FirstOrDefault().ToString();
                ViewState["sah_is_svat"] = (from DataRow dr in _result.Rows select dr["sah_is_svat"]).FirstOrDefault().ToString();
                ViewState["sah_d_cust_name"] = (from DataRow dr in _result.Rows select dr["sah_d_cust_name"]).FirstOrDefault().ToString();
                LoadCustomerDetailsByCustomer();
            }
            catch (Exception ex)
            {
                txtInvoice.Text = "";
                DisplayMessage(ex.Message);
            }
        }

        //Invoice Popup Page Index Changing
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            try
            {
                grdResultD.DataSource = null;
                DataTable _tbl = (DataTable)ViewState["SEARCH"];
                if (lblvalue.Text == "InvoiceWithDate")
                {
                    grdResultD.DataSource = _tbl;
                }
                else
                {
                    grdResultD.DataSource = _tbl;

                }
                grdResultD.DataBind();
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "InvoiceWithDate")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchInvoiceWeb(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);

                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            if (lblvalue.Text == "srn")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.SearchReversalInvoiceForDebit(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text.ToString()), Convert.ToDateTime(txtTDate.Text.ToString()));//SearchReversalInvoice
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srn";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
        }

        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected string UpdateAutoNo(string _module, Int16? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year)
        {
            try
            {
                return CHNLSVC.Financial.GetAndUpdateAutoNo(_module, _direction, _startChar, _catType, _catCode, _modifyDate, _year);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
                return "";
            }
        }

        protected void gridTotCalculate()
        {
            decimal disAmt = 0;
            decimal taxAmt = 0;
            decimal lineAmt = 0;
            if (grdInvoices.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdInvoices.Rows)
                {
                    try
                    {
                        disAmt = Convert.ToDecimal((row.FindControl("col_DisAmt") as Label).Text) + disAmt;
                        taxAmt = Convert.ToDecimal((row.FindControl("col_TaxAmt") as Label).Text) + taxAmt;
                        lineAmt = Convert.ToDecimal((row.FindControl("col_LineAmt") as Label).Text) + lineAmt;
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.Message, 4);
                        return;
                    }
                }
            }
            lbldisAmt.Text = disAmt.ToString("N2");
            lbltaxAmt.Text = taxAmt.ToString("N2");
            lblLineAmt.Text = lineAmt.ToString("N2");
        }

        //cancel creadit allocation
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Int32 debitNoCan = 0;
            string invoiceNo = string.Empty;
            if (hdfCancel.Value == "Yes")
            {
                if (grdInvoices.Rows.Count > 0)
                {
                    foreach (GridViewRow row in grdInvoices.Rows)
                    {
                        try
                        {
                            invoiceNo = (row.FindControl("col_InvoiceNo") as Label).Text;
                            decimal amt = Convert.ToDecimal((row.FindControl("col_LineAmt") as Label).Text);
                            string sessiomModBy = Session["UserID"].ToString();
                            DateTime sessiomModDate = DateTime.Now;
                            string sessionId = Session["SessionID"].ToString();
                            debitNoCan = (Int32)CHNLSVC.Financial.DebitNoteCancel(invoiceNo, amt, sessiomModBy, sessiomModDate, sessionId);
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage(ex.Message, 4);
                            return;
                        }
                        if (debitNoCan >= 1)
                        {
                            DisplayMessage("Successfully canceled the selected " + invoiceNo + " Debit Note", 3);
                            ClearDebits(true, false);
                        }
                        else
                            DisplayMessage("Please select the debit note # inorder to cancel !", 1);
                    }
                }
                else
                    DisplayMessage("Please select the debit note # inorder to cancel !", 1);
            }
            else return;
        }

        //Item Searching
        protected void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Text = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.creditNoteVirtualItems(SearchParams, null, null, "V");//GetItemforInvoiceSearchData
                grdResult.DataSource = result; //grdResult
                grdResult.DataBind();
                lblvalue.Text = "Item";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate) ? true : false;
        }

        //Add Invoice to grid
        protected void lbtnadditems_Click(object sender, EventArgs e)
        {
            _totUnitAmt_list.Add(Convert.ToDecimal(txtLineTotAmt.Text));
            _totUnitAmt = _totUnitAmt_list.Sum(x => x) + Convert.ToDecimal(ViewState["_totUnitAmt"]);
            ViewState["_totUnitAmt"] = _totUnitAmt;
            if (txtInvoice.Text != "" && txtInvoice.Text != null)
            {
                //QA request
                //if (Convert.ToDecimal(txtBalAmt.Text) <= 0)
                //{
                //    DisplayMessage("Can not add debit note due to balance amount 0");
                //    return;
                //}
                //if (_totUnitAmt >= Convert.ToDecimal(txtBalAmt.Text))
                //{
                //    DisplayMessage("Balance amount should be greater than total debit amount...!!!");
                //    ViewState["_totUnitAmt"] = null;
                //    return;
                //}
            }

            if (txtUnitPrice.Text == "0.00")
            {
                DisplayMessage("Zero debit can not be add...!!!");
                return;
            }

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            bool result_cus = CHNLSVC.General.CheckCustomer(txtCus.Text);//CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            string SearchParams_item = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
            DataTable result_item = CHNLSVC.CommonSearch.creditNoteVirtualItems(SearchParams_item, null, null, "S");
            string SearchParams_salesEx = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
            List<EMP_SEARCH_HEAD_SCM> result_sEx = CHNLSVC.CommonSearch.getEmployeeDetailsSCM("1", "150", "Epf No", txtExecutive.Text, Session["UserCompanyCode"].ToString());//CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams_salesEx, null, null);
            bool vDate = IsDateTime(txtDate.Text);
            if (!vDate)
            {
                DisplayMessage("Please enter a valid Date");
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                DisplayMessage("Please Select Item", 4);
                return;
            }
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please enter a valid quantity");
                return;
            }
            if (Convert.ToDecimal(txtQty.Text) < 0)
            {
                DisplayMessage("Can not add minus quantity");
                return;
            }
            if (IsNumeric(txtUnitPrice.Text) == false)
            {
                DisplayMessage("Please enter a valid unit price");
                return;
            }
            if (Convert.ToDecimal(txtUnitPrice.Text) < 0)
            {
                DisplayMessage("Can not add minus unit price");
                return;
            }
            if (IsNumeric(txtDisRate.Text) == false)
            {
                DisplayMessage("Please enter a valid discount rate");
                return;
            }
            if (Convert.ToDecimal(txtDisRate.Text) < 0)
            {
                DisplayMessage("Can not add minus discount rate");
                return;
            }
            if (IsNumeric(txtUnitAmt.Text) == false)
            {
                DisplayMessage("Please enter a valid unit amount");
                return;
            }
            if (IsNumeric(txtDisAmt.Text) == false)
            {
                DisplayMessage("Please enter a valid discount amount");
                return;
            }
            if (IsNumeric(txtTaxAmt.Text) == false)
            {
                DisplayMessage("Please enter a valid tax amount");
                return;
            }
            if (IsNumeric(txtLineTotAmt.Text) == false)
            {
                DisplayMessage("Please enter a valid total amount");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtBalAmt.Text))
            {
                DisplayMessage("Please select a valid invoice #");
                return;
            }
            //if (txtInvoice.Text == "" && txtInvoice.Text == null)
            //{
            //    DisplayMessage("Please select invoice #");
            //    return;
            //}
            //if (Convert.ToDecimal(txtBalAmt.Text) <= 0)
            //{
            //    DisplayMessage("Can not add debit note due to balance amount 0");
            //    return;
            //}
            //var cusCode = from row in result_cus.AsEnumerable() where row.Field<string>("CODE").Trim() == txtCus.Text select row;
            //List<DataRow> cCode = cusCode.ToList();
            if (!result_cus)
            {
                DisplayMessage("Please enter a valid Customer Code", 1);
                return;
            }
            var itemCode = from row in result_item.AsEnumerable() where row.Field<string>("ITEM").Trim() == txtItem.Text select row;
            List<DataRow> iCode = itemCode.ToList();
            if (iCode.Count <= 0)
            {
                DisplayMessage("Please enter a Valid Item Code", 1);
                return;
            }
            //var salesEx = from row in result_sEx.AsEnumerable() where row.Field<string>("CODE").Trim() == txtExecutive.Text select row;
            //List<DataRow> Ex = salesEx.ToList();
            if (result_sEx == null || result_sEx.Count <= 0)
            {
                DisplayMessage("Please enter a Valid Sales Executive No", 1);
                return;
            }
            _DebitNotes = ViewState["StoreDN"] as List<DebitNotes>;
            if (ViewState["StoreDN"] == null || ViewState["StoreDN"].ToString() == "")
            {
                _DebitNotes = new List<DebitNotes>();
            }

            if (_DebitNotes.Count > 0 && ViewState["VSItem"] != null)
            {
                var rmvItem = _DebitNotes.Find(r => r.Item == ViewState["VSItem"].ToString());
                if (rmvItem != null)
                {
                    _DebitNotes.Remove(rmvItem);
                }
                ViewState["VSItem"] = null;
            }

            DebitNotes _DbtNotes = new DebitNotes();
            _DbtNotes.Item = txtItem.Text;
            _DbtNotes.Description = (txtDescription.Text == "" || txtDescription.Text == null) ? string.Empty : txtDescription.Text;
            _DbtNotes.Qty = txtQty.Text == null ? 0 : Convert.ToDecimal(txtQty.Text);
            _DbtNotes.UnitPrice = txtUnitPrice.Text == null ? 0 : Convert.ToDecimal(txtUnitPrice.Text);
            _DbtNotes.Total = txtUnitAmt.Text == null ? 0 : Convert.ToDecimal(txtUnitAmt.Text);
            _DbtNotes.DisRate = txtDisRate.Text == null ? 0 : Convert.ToDecimal(txtDisRate.Text);
            _DbtNotes.DisAmt = txtDisAmt.Text == null ? 0 : Convert.ToDecimal(txtDisAmt.Text);
            _DbtNotes.Tax = txtTaxAmt.Text == null ? 0 : Convert.ToDecimal(txtTaxAmt.Text);
            _DbtNotes.LineAmt = txtLineTotAmt.Text == null ? 0 : Convert.ToDecimal(txtLineTotAmt.Text);
            _DbtNotes.CusCode = (txtCus.Text == "" || txtCus.Text == null) ? string.Empty : txtCus.Text;
            ViewState["HCusCode"] = txtCus.Text;
            _DbtNotes.SalesExCode = (txtExecutive.Text == "" || txtExecutive.Text == null) ? string.Empty : txtExecutive.Text;
            ViewState["HExecutive"] = txtExecutive.Text;
            _DbtNotes.DebitNoteNo = txtDbtNte.Text == "" || txtDbtNte.Text == null ? "" : txtDbtNte.Text;
            _DbtNotes.Date = Convert.ToDateTime(txtDate.Text);
            _DbtNotes.InvoiceNo = txtInvoice.Text == "" || txtInvoice.Text == null ? "" : txtInvoice.Text;
            ViewState["HInvoiceNo"] = txtInvoice.Text;
            _DbtNotes.InvoiceType = cmbInvType.SelectedValue;

            _DbtNotes.InvAmt = txtTotAmt.Text == null ? 0 : Convert.ToDecimal(txtTotAmt.Text);
            _DbtNotes.InvAmtPaid = txtPaidAmt.Text == null ? 0 : Convert.ToDecimal(txtPaidAmt.Text);
            _DbtNotes.InvAmtBal = txtBalAmt.Text == null ? 0 : Convert.ToDecimal(txtBalAmt.Text);
            _DbtNotes.CusName = ViewState["cusName"].ToString();
            _DbtNotes.CusAdd1 = ViewState["cusAdd1"].ToString();
            _DbtNotes.CusAdd2 = ViewState["cusAdd2"].ToString();
            _DbtNotes.ReceiptNo = "na";

            var res = _DebitNotes.Where(c => c.Item == txtItem.Text && grdInvoices.Rows.Count >= 1).FirstOrDefault();
            if (res != null)
            {
                DisplayMessage("Can not add same item again...!!!", 1);
                return;
            }
            else
                _DebitNotes.Add(_DbtNotes);

            grdInvoices.DataSource = _DebitNotes;
            grdInvoices.DataBind();
            ViewState["StoreDN"] = _DebitNotes;
            TotLineAmt = _DebitNotes.Sum(x => x.LineAmt);
            //ViewState["cusName"] = string.Empty;
            //ViewState["cusAdd1"] = string.Empty;
            //ViewState["cusAdd2"] = string.Empty;
            gridTotCalculate();
            ClearDebits(false, true);
            _DebitNotes = null;
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if(txtExecutive.Text == "")
            {
                DisplayMessage("Invalid sales executive code", 1);
                return;
            }
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please enter a valid quantity");
                return;
            }

            if (Convert.ToDecimal(txtQty.Text) < 0)
            {
                DisplayMessage("Can not add minus quantity");
                return;
            }
            try
            {
                if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    DisplayMessage("Quantity should be positive value.");
                    return;
                }
                if ((!string.IsNullOrEmpty(txtQty.Text.Trim())) && (!string.IsNullOrEmpty(txtUnitPrice.Text.Trim())))
                {
                    CalCulateVal();
                }
                TaxProcess();
            }
            catch (Exception ex)
            {
                txtQty.Text = FormatToQty("1");
                DisplayMessage(ex.Message);
            }
        }

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            decimal unitPrice;
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please enter a valid qty");
                return;
            }
            if (IsNumeric(txtUnitPrice.Text) == false)
            {
                DisplayMessage("Please enter a valid unit price");
                return;
            }
            if (Convert.ToDecimal(txtUnitPrice.Text) < 0)
            {
                DisplayMessage("Can not add minus unit price");
                return;
            }
            unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            txtUnitPrice.Text = SetDecimalPoint(unitPrice.ToString("N2"));
            try
            {
                if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    DisplayMessage("Unit price should be positive value.");
                    return;
                }
                if ((!string.IsNullOrEmpty(txtQty.Text.Trim())) && (!string.IsNullOrEmpty(txtUnitPrice.Text.Trim())))
                {
                    CalCulateVal();
                }
                TaxProcess();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void CalCulateVal()
        {
            try
            {
                Decimal totval = Convert.ToDecimal(txtQty.Text.Trim()) * Convert.ToDecimal(txtUnitPrice.Text.Trim());
                txtUnitAmt.Text = SetDecimalPoint(totval.ToString("N2"));
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private string SetDecimalPoint(string amount)
        {
            decimal value = Convert.ToDecimal(amount);
            return value.ToString("N2");
        }

        private string SetDecimalPoint(decimal amount)
        {
            return amount.ToString("N2");
        }

        protected void TaxProcess()
        {
            string book = string.Empty;
            string level = string.Empty;
            bool vDate = IsDateTime(txtDate.Text);
            if (IsNumeric(txtUnitAmt.Text) == false)
            {
                DisplayMessage("Please enter a valid Unit Amount");
                return;
            }
            if (string.IsNullOrEmpty(txtCus.Text))
            {
                DisplayMessage("Please enter a valid Customer");
                return;
            }
            if (!vDate)
            {
                DisplayMessage("Please enter a valid Date");
                return;
            }
            try
            {
                //Calculate Discount
                decimal _unitAmt = Convert.ToDecimal(txtUnitAmt.Text);
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                decimal _disAmt = (_unitAmt * (_disRate / 100));
                txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));

                //Calculate Tax
                decimal _vatPortion = 0;
                try
                {
                    DataTable _result = CHNLSVC.Financial.getComDetails(Session["UserCompanyCode"].ToString());
                    if (_result.Rows.Count > 0)
                    {
                        book = (from DataRow dr in _result.Rows select dr["book"]).FirstOrDefault().ToString();
                        level = (from DataRow dr in _result.Rows select dr["level"]).FirstOrDefault().ToString();

                        _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), book, level);
                        txtUnitAmt.Text = SetDecimalPoint((Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true))));

                        if (lblVatExemptStatus.Text == "No")
                        {
                            _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), "GOD", Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
                        }
                        ViewState["vat"] = _vatPortion;
                        if (_vatPortion == 0)
                        {
                            txtTaxAmt.Text = FormatToCurrency("0");
                        }
                        else
                            txtTaxAmt.Text = SetDecimalPoint((Convert.ToString(_vatPortion)));
                    }
                    else
                        DisplayMessage("User Haven't Relavent Book and Level Data, Please Contact IT", 4);
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message, 4);
                }

                //Calculate Line Total
                decimal _totalAmount = Convert.ToDecimal(txtUnitAmt.Text);
                decimal _vatPortionAmt = Convert.ToDecimal(ViewState["vat"]);
                decimal _discAmt = Convert.ToDecimal(txtDisAmt.Text);
                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }
                txtLineTotAmt.Text = SetDecimalPoint(_totalAmount);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        //tax, dis amt, line amt calculation method
        protected void txtDisRate_TextChanged(object sender, EventArgs e)
        {
            string book = string.Empty;
            string level = string.Empty;
            bool vDate = IsDateTime(txtDate.Text);
            if (IsNumeric(txtDisRate.Text) == false)
            {
                DisplayMessage("Please enter a valid discount rate");
                return;
            }
            if (Convert.ToDecimal(txtDisRate.Text) < 0)
            {
                DisplayMessage("Can not add minus discount rate");
                return;
            }
            if (IsNumeric(txtUnitAmt.Text) == false)
            {
                DisplayMessage("Please enter a valid Unit Amount");
                return;
            }
            if (string.IsNullOrEmpty(txtCus.Text))
            {
                DisplayMessage("Please enter a valid Customer");
                return;
            }
            if (!vDate)
            {
                DisplayMessage("Please enter a valid Date");
                return;
            }
            try
            {
                //Calculate Discount
                decimal _unitAmt = Convert.ToDecimal(txtUnitAmt.Text);
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                decimal _disAmt = (_unitAmt * (_disRate / 100));
                txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));

                //Calculate Tax
                decimal _vatPortion = 0;
                try
                {
                    DataTable _result = CHNLSVC.Financial.getComDetails(Session["UserCompanyCode"].ToString());
                    if (_result.Rows.Count > 0)
                    {
                        book = (from DataRow dr in _result.Rows select dr["book"]).FirstOrDefault().ToString();
                        level = (from DataRow dr in _result.Rows select dr["level"]).FirstOrDefault().ToString();

                        _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), book, level);
                        txtUnitAmt.Text = SetDecimalPoint((Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true))));

                        if (lblVatExemptStatus.Text == "No")
                        {
                            _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), "GOD", Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
                        }
                        ViewState["vat"] = _vatPortion;
                        if (_vatPortion == 0)
                        {
                            txtTaxAmt.Text = FormatToCurrency("0");
                        }
                        else
                            txtTaxAmt.Text = SetDecimalPoint((Convert.ToString(_vatPortion)));
                    }
                    else
                        DisplayMessage("User Haven't Relavent Book and Level Data, Please Contact IT", 4);
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message, 4);
                }

                //Calculate Line Total
                decimal _totalAmount = Convert.ToDecimal(txtUnitAmt.Text);
                decimal _vatPortionAmt = Convert.ToDecimal(ViewState["vat"]);
                decimal _discAmt = Convert.ToDecimal(txtDisAmt.Text);
                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }
                txtLineTotAmt.Text = SetDecimalPoint(_totalAmount);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            return value;
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            decimal _TaxAmt = 0;
            decimal _TotVal = 0;
            decimal _TotDis = 0;
            _TotVal = _pbUnitPrice * _qty;
            _TotDis = _TotVal * _disRate / 100;

            if (cmbInvType.Text == "DEBT")
            {
                _pbUnitPrice = 0;
            }
            else
            {
                //added darshana 30-Dec-2015
                if (_MasterProfitCenter.Mpc_issp_tax == true)
                {
                    List<MasterPCTax> _masterPCTax = new List<MasterPCTax>();
                    _masterPCTax = CHNLSVC.Sales.GetPcTax(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), 1, Convert.ToDateTime(txtDate.Text));
                    var _pcTaxNBT = from _pcTaxs in _masterPCTax
                                    where _pcTaxs.Mpt_taxtp == "NBT"
                                    select _pcTaxs;

                    foreach (MasterPCTax _one in _pcTaxNBT)
                    {
                        if (lblVatExemptStatus.Text != "Yes")
                        {
                            _discount = _TotVal * _disRate / 100;
                            _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mpt_taxrt / 100);

                            _TotVal = _TotVal - _TotDis + _TaxAmt;
                        }
                        else
                        {
                            _pbUnitPrice = 0;
                        }
                        ViewState["_taxRate"] = _one.Mpt_taxrt;
                    }

                    var _pcTaxVAT = from _pcTaxs in _masterPCTax
                                    where _pcTaxs.Mpt_taxtp == "VAT"
                                    select _pcTaxs;

                    foreach (MasterPCTax _one in _pcTaxVAT)
                    {
                        if (lblVatExemptStatus.Text != "Yes")
                        {
                            _discount = _TotVal * _disRate / 100;
                            _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mpt_taxrt / 100);
                        }
                        else
                        {
                            _pbUnitPrice = 0;
                        }
                        ViewState["_taxRate"] = _one.Mpt_taxrt;
                    }
                    _pbUnitPrice = _TaxAmt;
                }
                else
                {
                    if (_priceBookLevelRef != null)
                        if (_priceBookLevelRef.Sapl_vat_calc)
                        {
                            bool _isVATInvoice = false;

                            if (lblVatExemptStatus.Text == "Yes") _isVATInvoice = true;//chkTaxPayable.Checked || 
                            else _isVATInvoice = false;

                            if (Convert.ToDateTime(txtDate.Text).ToShortDateString() == Convert.ToDateTime(_serverDt).ToShortDateString())
                            {
                                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                                _taxs = CHNLSVC.Sales.GetCustomerTax(txtCus.Text, Session["UserCompanyCode"].ToString());
                                if (_taxs.Count == 0)
                                {
                                    if (_isStrucBaseTax == true)       //kapila
                                    {
                                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                        _taxs = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), _item, _status, "NBT", string.Empty, _mstItem.Mi_anal1);
                                    }
                                    else
                                    {
                                        _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, "NBT", string.Empty);
                                    }

                                }
                                var _Tax = from _itm in _taxs
                                           select _itm;

                                foreach (MasterItemTax _one in _Tax)
                                {
                                    if (lblVatExemptStatus.Text != "Yes")
                                    {
                                        _discount = _TotVal * _disRate / 100;
                                        _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mict_tax_rate / 100);

                                        _TotVal = _TotVal - _TotDis + _TaxAmt;
                                    }
                                    else
                                    {
                                        _pbUnitPrice = 0;
                                    }
                                    ViewState["_taxRate"] = _one.Mict_tax_rate;
                                }

                                //vAT
                                if (_isStrucBaseTax == true)       //kapila
                                {
                                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                                }
                                else
                                {
                                    _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, "VAT", string.Empty);
                                }

                                var _Tax1 = from _itm in _taxs
                                            select _itm;
                                foreach (MasterItemTax _one in _Tax1)
                                {
                                    if (lblVatExemptStatus.Text != "Yes")
                                    {
                                        _discount = _TotVal * _disRate / 100;
                                        _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mict_tax_rate / 100);
                                    }
                                    else
                                    {
                                        _pbUnitPrice = 0;
                                    }
                                    ViewState["_taxRate"] = _one.Mict_tax_rate;
                                }
                            }
                            else
                            {
                                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                                _taxs = CHNLSVC.Sales.GetCustomerTax(txtCus.Text, Session["UserCompanyCode"].ToString());
                                if (_taxs.Count == 0)
                                {
                                    _taxs = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), _item, _status, "VAT", string.Empty, Convert.ToDateTime(txtDate.Text));
                                }
                                var _Tax = from _itm in _taxs
                                           select _itm;
                                if (_taxs.Count > 0)
                                {
                                    foreach (MasterItemTax _one in _Tax)
                                    {
                                        if (lblVatExemptStatus.Text != "Yes")
                                        {
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Mict_tax_rate / 100);

                                            _TotVal = _TotVal - _TotDis + _TaxAmt;
                                        }
                                        else
                                        {
                                            _pbUnitPrice = 0;
                                        }
                                        ViewState["_taxRate"] = _one.Mict_tax_rate;
                                    }

                                    _taxs = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), _item, _status, "VAT", string.Empty, Convert.ToDateTime(txtDate.Text));
                                    var _Tax1 = from _itm in _taxs
                                                select _itm;
                                    foreach (MasterItemTax _one in _Tax1)
                                    {
                                        if (lblVatExemptStatus.Text != "Yes")
                                        {
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal) * _one.Mict_tax_rate / 100);
                                        }
                                        else
                                        {
                                            _pbUnitPrice = 0;
                                        }
                                        ViewState["_taxRate"] = _one.Mict_tax_rate;
                                    }
                                }
                                else
                                {
                                    List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();

                                    _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, "NBT", string.Empty, Convert.ToDateTime(txtDate.Text));

                                    var _TaxEffDt = from _itm in _taxsEffDt
                                                    select _itm;
                                    foreach (LogMasterItemTax _one in _TaxEffDt)
                                    {
                                        if (lblVatExemptStatus.Text != "Yes")
                                        {
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal - _TotDis) * _one.Lict_tax_rate / 100);

                                            _TotVal = _TotVal - _TotDis + _TaxAmt;
                                        }
                                        else
                                        {
                                            _pbUnitPrice = 0;
                                        }
                                        ViewState["_taxRate"] = _one.Lict_tax_rate;
                                    }
                                    _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, "VAT", string.Empty, Convert.ToDateTime(txtDate.Text));
                                    var _TaxEffDt1 = from _itm in _taxsEffDt
                                                     select _itm;
                                    foreach (LogMasterItemTax _one in _TaxEffDt1)
                                    {
                                        if (lblVatExemptStatus.Text != "Yes")
                                        {
                                            _discount = _TotVal * _disRate / 100;
                                            _TaxAmt = _TaxAmt + ((_TotVal) * _one.Lict_tax_rate / 100);
                                        }
                                        else
                                        {
                                            _pbUnitPrice = 0;
                                        }
                                        ViewState["_taxRate"] = _one.Lict_tax_rate;
                                    }

                                }
                            }
                        }
                        else
                            if (_isTaxfaction) _pbUnitPrice = 0;
                    _pbUnitPrice = _TaxAmt;
                }
            }
            return _pbUnitPrice;
        }

        protected void grdInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            TextBox date = (TextBox)sender;
            txtDate.Text = date.Text;
        }
        //Item and Customer page index changeing
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            try
            {
                grdResult.DataSource = null;
                DataTable _tbl = (DataTable)ViewState["SEARCH"];
                if (lblvalue.Text == "CU")
                {
                    grdResult.DataSource = _tbl;
                }
                else
                {
                    grdResult.DataSource = _tbl;

                }
                grdResult.DataBind();
                SIPopup.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnEx_Click(object sender, EventArgs e)
        {
            try
            {
                DummyDataBind();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Sale_Ex";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void DummyDataBind()
        {
            DataTable dummy = new DataTable();
            System.Data.DataColumn code = new System.Data.DataColumn("CODE", typeof(System.String));
            System.Data.DataColumn desc = new System.Data.DataColumn("DESCRIPTION", typeof(System.String));
            System.Data.DataColumn epf = new System.Data.DataColumn("EPF", typeof(System.String));
            dummy.Columns.Add(code);
            dummy.Columns.Add(desc);
            dummy.Columns.Add(epf);
            BindUCtrlDDLData(dummy);
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "Yes")
            {
                try
                {
                    if (grdInvoices.Rows.Count > 0)
                    {
                        SaveDebitNote();
                    }
                    else
                        DisplayMessage("Please add item details before saving");
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message);
                }
            }
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
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
                ClearDebits(false, false);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        protected void lbtnDetaltecost_Click(object sender, EventArgs e)
        {
            try
            {
                List<DebitNotes> dbtNote = new List<DebitNotes>();
                if (txtDeleteconformmessageValue.Value == "Yes")
                {
                    dbtNote = (List<DebitNotes>)ViewState["StoreDN"];
                    if (dbtNote.Count > 0)
                    {
                        GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                        Int32 rowIndex = dr.RowIndex;
                        Label lblItem = grdInvoices.Rows[rowIndex].FindControl("col_Item") as Label;
                        ViewState["VSItem"] = lblItem.Text;
                        var rmvItm = dbtNote.Find(r => r.Item == lblItem.Text);//Single
                        dbtNote.Remove(rmvItm);
                        grdInvoices.DataSource = dbtNote;
                        grdInvoices.DataBind();
                    }
                }
                else
                {
                    return;
                }
                gridTotCalculate();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtCus_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerDetailsByCustomer();
        }

        //protected void lbtnDebitNoteNo_Click(object sender, EventArgs e)
        //{
        //    string CredNote = UpdateAutoNo("REV", 0, "INREV", "PC", Session["UserDefProf"].ToString(), null, DateTime.Now.Year);
        //    if (CredNote != "" && CredNote != null)
        //    {
        //        txtCreNote.Text = CredNote;
        //    }
        //    else
        //        DisplayMessage("Issue In Auto Generate No..!", 4);
        //}

        protected void lbtnDbtNte_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                //{
                //}
                //else
                //{
                //    txtFDate.Text = DateTime.Now.Date.AddDays(-7).ToString("dd/MMM/yyyy");
                //}
                //if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                //{
                //}
                //else
                //{
                //    txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                //}
                txtFDate.Text = DateTime.Now.ToShortDateString();
                txtTDate.Text = DateTime.Now.ToShortDateString();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate); //SearchReversalInvoiceForCreditDebit
                DataTable result = CHNLSVC.CommonSearch.SearchReversalInvoiceForDebit(SearchParams, null, null, Convert.ToDateTime(txtTDate.Text.ToString()), Convert.ToDateTime(txtFDate.Text.ToString()));
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(3);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "srn";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Text = "";
                txtSearchbywordD.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void commonPrint_PDF()
        {
            lblPrint.OnClientClick = "ConfirmPrint();";
            try
            {
                if (hdnprint.Value == "Yes")
                {

                    if (txtDbtNte.Text.ToString() != "")
                    {
                        Session["documntNo"] = txtDbtNte.Text;
                        _print();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Document');", true);
                        return;
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lblPrint_Click(object sender, EventArgs e)
        {
            commonPrint_PDF();
        }
        public void _print()
        {
            try
            {
                Session["GlbReportType"] = "SCM1_SRN";
                Session["GlbReportName"] = "SRN.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDF(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch(Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Debit Note Print", "DebitNote", ex.Message, Session["UserID"].ToString());
            }
        }
        public void PrintPDF(string targetFileName)
        {
            try
            {
                clsSales obj = new clsSales();
                obj.GetSRNdata(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["documntNo"].ToString(), Session["UserDefLoca"].ToString());
                ReportDocument rptDoc = (ReportDocument)obj._SRNreport;
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
                throw ex;
            }
        }

        protected void txtExecutive_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "EPF", txtExecutive.Text);
                if (result.Rows.Count == 1)
                {
                }
                else
                {
                    txtExecutive.Text = string.Empty;
                    DisplayMessage("Invalid sales executive code", 1);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
            DataTable result = CHNLSVC.CommonSearch.creditNoteVirtualItems(SearchParams, "ITEM", txtItem.Text.Trim(), "S");
            txtDescription.Text = grdResult.SelectedRow.Cells[2].Text.Replace("&quot;", "''").Replace("&amp;", "&");
            txtDescription.ToolTip = txtDescription.Text;
        }

        protected void lbtnRefreshDtl_Click(object sender, EventArgs e)
        {
            txtItem.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtQty.Text = FormatToCurrency("0.00");
            txtUnitAmt.Text = FormatToCurrency("0.00");
            txtUnitPrice.Text = FormatToCurrency("0.00");
            txtDisRate.Text = FormatToCurrency("0.00");
            txtDisAmt.Text = FormatToCurrency("0.00");
            txtTaxAmt.Text = FormatToCurrency("0.00");
            txtLineTotAmt.Text = FormatToCurrency("0.00");
        }

        protected void lbtnPrintOk_Click(object sender, EventArgs e)
        {
            _print();
        }

        protected void linkPrintNo_Click(object sender, EventArgs e)
        {
            return;
        }

        ///
        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtCusCode.Text))
            //{
            //    DisplayMessage("Please select the customer", 2);
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtRecType.Text))
            //{
            //    DisplayMessage("Please select rceipt type", 2);
            //    return;
            //}

            //   DisplayMessage("Please unselect blank cell/cells which you have selected in excel sheet", 1);
            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
            excelUpload.Show();
        }
        protected void btnClose3_Click(object sender, EventArgs e)
        {
            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
        }
        protected void btnprocess_Click(object sender, EventArgs e)
        {
            DataTable[] GetExecelTbl = LoadData2(Session["FilePath"].ToString());
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    for (int i = 0; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        DateTime date = DateTime.Now;
                        decimal amount = 0;
                        string customerCode = GetExecelTbl[0].Rows[i][0].ToString();
                        if (!(GetExecelTbl[0].Rows[i][2].ToString().Trim().Equals(string.Empty)))
                        {
                            date = Convert.ToDateTime(GetExecelTbl[0].Rows[i][1].ToString());
                        }
                        string refNumber = GetExecelTbl[0].Rows[i][2].ToString();
                        string remarks = GetExecelTbl[0].Rows[i][3].ToString();
                        string executiveCode = GetExecelTbl[0].Rows[i][4].ToString();
                        if (!(GetExecelTbl[0].Rows[i][5].ToString().Trim().Equals(string.Empty)))
                        {
                            amount = Convert.ToDecimal(GetExecelTbl[0].Rows[i][5].ToString());
                        }
                        string otherAccount = GetExecelTbl[0].Rows[i][6].ToString();
                        string type = GetExecelTbl[0].Rows[i][7].ToString();
                        if (type.Equals("DEBT"))
                        {
                            Int32 row_aff = 0;
                            string _docNo = string.Empty;
                            int lNo = 1;
                            Int32 InvoiceSeqNo = (Int32)CHNLSVC.Inventory.GetSerialID();
                            DataTable _dtl = CHNLSVC.General.SearchCustomerData(customerCode);

                            string customerName = "";
                            string address1 = "";
                            string address2 = "";

                            if (_dtl.Rows.Count > 0)
                            {
                                customerName = _dtl.Rows[0]["mbe_name"].ToString();
                                address1 = _dtl.Rows[0]["mbe_add1"].ToString();
                                address2 = _dtl.Rows[0]["mbe_add2"].ToString();
                            }

                            if (Convert.ToDateTime(txtDate.Text) != DateTime.Now.Date)
                            {
                                bool _allowCurrentTrans = false;
                                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDate, lblH1, txtDate.Text, out _allowCurrentTrans) == false)
                                {
                                    if (_allowCurrentTrans == true)
                                    {
                                        if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                                        {
                                            txtDate.Enabled = true;
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction!');", true);

                                            txtDate.Focus();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        txtDate.Enabled = true;
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction!');", true);
                                        //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtDate.Focus();
                                        return;
                                    }
                                }
                            }

                            DebitNoteHdr _dbtNotehdr = new DebitNoteHdr();
                            _dbtNotehdr.InvoiceSeqNo = InvoiceSeqNo;
                            _dbtNotehdr.ComCode = Session["UserCompanyCode"].ToString();
                            _dbtNotehdr.ProfitCenter = Session["UserDefProf"].ToString();
                            _dbtNotehdr.InvoiceType = type;
                            _dbtNotehdr.InvSubType = "";

                            _dbtNotehdr.sessionCreateBy = Session["UserID"].ToString();
                            _dbtNotehdr.sessionCreateDate = DateTime.Now;
                            _dbtNotehdr.sessionModBy = Session["UserID"].ToString();
                            _dbtNotehdr.sessionModDate = DateTime.Now;
                            _dbtNotehdr.SessionId = Session["SessionID"].ToString();
                            _dbtNotehdr.Date = date;
                            _dbtNotehdr.CusCode = customerCode;
                            _dbtNotehdr.SalesExCode = executiveCode;
                            _dbtNotehdr.CusName = customerName;
                            _dbtNotehdr.CusAdd1 = address1;
                            _dbtNotehdr.CusAdd2 = address2;

                            _dbtNotehdr.LineAmt = amount;
                            _dbtNotehdr.OtherAccount = otherAccount;


                            _DebitNoteHdr.Add(_dbtNotehdr);
                            DataTable _result = null;
                            //_result = CHNLSVC.Financial.getInvoiceDetailsValues("LGACSTS1862QC7",invoiceNo);
                            string autoGeReceiptNo = UpdateAutoNo("RECEIPT", 0, "VHREG", "PC", Session["UserDefProf"].ToString(),DateTime.Now, DateTime.Now.Year);
                            DebitNotes _debtNote = new DebitNotes();
                            _debtNote.Item = "LGACSTS1862QC7";
                            _debtNote.Description = "LG-18000BTU AIR CONDITIONER-SPLIT-COM";
                            _debtNote.Qty = 1;
                            _debtNote.UnitPrice = amount;
                            _debtNote.Total = amount;
                            _debtNote.DisRate = 0;
                            _debtNote.DisAmt = 0;
                            _debtNote.Tax = 0;
                            _debtNote.LineAmt = amount;

                            _debtNote.ComCode = Session["UserCompanyCode"].ToString();
                            _debtNote.ProfitCenter = Session["UserDefProf"].ToString();
                            _debtNote.CusCode = customerCode;
                            _debtNote.SalesExCode = executiveCode;


                            _debtNote.Date = date;

                            _debtNote.InvoiceType = type;
                            _debtNote.InvAmt = amount;
                            _debtNote.InvAmtPaid = 0;
                            _debtNote.InvAmtBal = 0;

                            _debtNote.sessionCreateBy = Session["UserID"].ToString();
                            _debtNote.sessionCreateDate = DateTime.Now;
                            _debtNote.sessionModBy = Session["UserID"].ToString();
                            _debtNote.sessionModDate = DateTime.Now;
                            _debtNote.SessionId = Session["SessionID"].ToString();
                            _debtNote.InvoiceSeqNo = InvoiceSeqNo;
                            _debtNote.CusName = customerName;
                            _debtNote.CusAdd1 = address1;
                            _debtNote.CusAdd2 = address2;
                            _debtNote.ReceiptNo = autoGeReceiptNo;
                            _debtNote.lineNo = lNo;
                            _debtNote.sad_itm_stus = "GOD";
                            _debtNote.sad_uom = "0";
                            _debtNote.ItemTp = "M";

                            _DebitNotes.Add(_debtNote);
                            lNo++;

                            try
                            {
                                row_aff = (Int32)CHNLSVC.Financial.saveDebitNote(_DebitNoteHdr, _DebitNotes, GenerateMasterAutoNumber(), out _docNo);
                            }
                            catch (Exception ex)
                            {
                                DisplayMessage(ex.Message, 4);
                            }
                            if (row_aff >= 1)
                            {
                                // DisplayMessage("Successfully Saved The Debit Note " + _docNo + " Details...!", 3);
                                try
                                {
                                    //lblsuccess.Text = "Successfully saved the uploaded excel sheet data...!";
                                    //lblsuccess.Visible = true;

                                    //lblalert.Visible = false;
                                    //divUpcompleted.Visible = false;
                                    //excelUpload.Show();
                                    DisplayMessage("Successfully saved the uploaded excel sheet data...!",3);
                                    _DebitNoteHdr = new List<DebitNoteHdr>();
                                    _DebitNotes = new List<DebitNotes>();
                                }
                                catch (Exception ex)
                                {
                                    lblsuccess.Text = "Error In Process, Please contact IT...!";
                                    lblsuccess.Visible = true;
                                    _DebitNoteHdr = new List<DebitNoteHdr>();
                                    _DebitNotes = new List<DebitNotes>();
                                }

                            }
                            else
                            {
                                lblsuccess.Text = "Error In Process, Please contact IT...!";
                                lblsuccess.Visible = true;
                                lblalert.Visible = false;
                                divUpcompleted.Visible = false;
                                excelUpload.Show();
                                _DebitNoteHdr = new List<DebitNoteHdr>();
                                _DebitNotes = new List<DebitNotes>();
                            }
                            //   cleanViewState();
                            //  txtCus.ReadOnly = false;
                            //  txtExecutive.ReadOnly = false;
                        }
                        else
                        {
                            lblsuccess.Text = "Invoice type is not correct!!!";
                            lblsuccess.Visible = true;
                            excelUpload.Show();
                        }
                    }
                }
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //if(string.IsNullOrEmpty(txtPayment.Text))
            //{
            //    DisplayMessage("Please add customer payment amount before excel upload", 1);
            //}
            string error = "";
            int row = 1;
            lblalert.Visible = false;
            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {
                    lblsuccess.Visible = false;
                    lblsuccess.Text = string.Empty;

                    lblalert.Visible = true;
                    lblalert.Text = "Please select a valid excel (.xls) file";
                    excelUpload.Show();
                    return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload.SaveAs(FilePath);
                Session["FilePath"] = FilePath;

                Session["FilePath"] = FilePath;
                DataTable[] GetExecelTbl = LoadData2(Session["FilePath"].ToString());
                if (GetExecelTbl != null)
                {
                    if (GetExecelTbl[0].Rows.Count > 0)
                    {
                        # region excel validation
                        for (int i = 0; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            try
                            {

                                DateTime date = DateTime.Now;
                                decimal amount = 0;
                                string customerCode = GetExecelTbl[0].Rows[i][0].ToString();

                                if (customerCode.Equals(string.Empty))
                                {
                                    error = "Customer code cannot be empty! row is :" + row.ToString();
                                    ExecelUploadErrors(error);
                                }

                                if (!(GetExecelTbl[0].Rows[i][2].ToString().Trim().Equals(string.Empty)))
                                {
                                    try
                                    {
                                        date = Convert.ToDateTime(GetExecelTbl[0].Rows[i][1].ToString());
                                    }
                                    catch (Exception eeee)
                                    {
                                        error = "Date format is not correct! row is :" + row.ToString();
                                        ExecelUploadErrors(error);
                                        return;
                                    }
                                }
                                else
                                {
                                    error = "Date cannot be empty! row is :" + row.ToString();
                                    ExecelUploadErrors(error);
                                }
                                if (date != DateTime.Now.Date)
                                {
                                    bool _allowCurrentTrans = false;
                                    if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDate, lblH1, date.ToString(), out _allowCurrentTrans) == false)
                                    {
                                        if (_allowCurrentTrans == true)
                                        {
                                            if (date != DateTime.Now.Date)
                                            {
                                                ///  txtDate.Enabled = true;
                                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction!');", true);
                                                //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //   txtDate.Focus();
                                                //  return;
                                                error = "Date is not allowd for the transaction! row is :" + row.ToString();
                                                ExecelUploadErrors(error);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            txtDate.Enabled = true;
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction!');", true);
                                            //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtDate.Focus();
                                            return;
                                        }
                                    }
                                }
                                string refNumber = GetExecelTbl[0].Rows[i][2].ToString();
                                string remarks = GetExecelTbl[0].Rows[i][3].ToString();
                                string executiveCode = GetExecelTbl[0].Rows[i][4].ToString();
                                string SearchParams = "9" + ":" + Session["UserCompanyCode"].ToString() + "|" + Session["UserDefProf"].ToString() + "|";
                                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "EPF", executiveCode);
                                if (result.Rows.Count<1)
                                {
                                    error = "Executive is not not valid:" + " row is" + row.ToString();
                                    ExecelUploadErrors(error);
                                }
                                if (!(GetExecelTbl[0].Rows[i][5].ToString().Trim().Equals(string.Empty)))
                                {
                                    try
                                    {
                                        amount = Convert.ToDecimal(GetExecelTbl[0].Rows[i][5].ToString());
                                        if (amount < 0)
                                        {
                                            error = "Amount Cannot be minus value" + " row is" + row.ToString();
                                            ExecelUploadErrors(error);
                                        }
                                    }
                                    catch (Exception eee)
                                    {
                                        error = eee.ToString() + " row is" + row.ToString();
                                        ExecelUploadErrors(error);
                                    }
                                }
                                string otherAccount = GetExecelTbl[0].Rows[i][6].ToString();
                                if (otherAccount.Equals(string.Empty))
                                {
                                    error = "OtherAccount cannot be empty! row is :" + row.ToString();
                                    ExecelUploadErrors(error);
                                }

                                string type = GetExecelTbl[0].Rows[i][7].ToString();

                                if ((type.Equals("DEBT")))
                                {

                                    DataTable _dtl = CHNLSVC.General.SearchCustomerData(customerCode);

                                    string customerName = "";
                                    string address1 = "";
                                    string address2 = "";

                                    if (_dtl.Rows.Count > 0)
                                    {
                                        customerName = _dtl.Rows[0]["mbe_name"].ToString();
                                        address1 = _dtl.Rows[0]["mbe_add1"].ToString();
                                        address2 = _dtl.Rows[0]["mbe_add2"].ToString();
                                    }
                                    else
                                    {
                                        error = "Customer Code is not available ! row is :" + row.ToString();
                                        ExecelUploadErrors(error);
                                    }

                                }
                                else
                                {
                                    ExecelUploadErrors("Invoice type cannot "+type+" debit note row number is " + row.ToString());
                                    excelUpload.Show();
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            row++;
                        }
                        #endregion
                    }
                }
                if ((error.Equals("")))
                {
                    divUpcompleted.Visible = true;
                    DisplayMessage("Excel file upload completed. Do you want to process?", 2);
                }
                excelUpload.Show();
                //Import_To_Grid(FilePath, Extension, null);
            }
            else
            {
                lblalert.Visible = true;
                lblalert.Text = "Please select the correct upload file path";
                DisplayMessage("Please select the correct upload file path", 2);
                excelUpload.Show();
            }
        }
        private void ExecelUploadErrors(string msg)
        {
            lblalert.Visible = true;
            lblalert.Text = msg;
            excelUpload.Show();
            lblsuccess.Visible = false;
            return;
        }
        public DataTable[] LoadData2(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();
            using (OleDbConnection cn2 = new OleDbConnection { ConnectionString = ConnectionString(FileName, "Yes") })
            {
                try
                {
                    cn2.Open();
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dtExcelSchema;
                    cmdExcel.Connection = cn2;

                    dtExcelSchema = cn2.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    cn2.Close();

                    //Read Data from First Sheet
                    cn2.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Tax);
                }
                catch (Exception ex)
                {

                    lblalert.Visible = true;
                    lblalert.Text = "Invalid data found from the excel sheet. Please check data...." + ex.Message;
                    excelUpload.Show();
                    return new DataTable[] { Tax };
                }
                return new DataTable[] { Tax };
            }
        }

        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }
    }
}