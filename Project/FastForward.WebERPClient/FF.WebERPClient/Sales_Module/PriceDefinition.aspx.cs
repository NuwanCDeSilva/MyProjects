using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Data.OleDb;


namespace FF.WebERPClient.Sales_Module
{
    public partial class PriceDefinition : BasePage
    {
        protected static List<PriceBookRef> _priceBookRefCreation = null;
        protected static List<MasterProfitCenter> _masterProfitCenter = null;
        protected static List<PriceDefinitionRef> _priceDefinitionRef = null;

        //AD-HOC SESSION ASSIGNMENT FOR CHECKING PURPOSE ONLY
        private void Ad_hoc_sessions()
        {
            Session["UserID"] = "ADMIN";
            Session["UserCompanyCode"] = "ABL";
            Session["SessionID"] = "666";
            Session["UserDefLoca"] = "MSR16";
            Session["UserDefProf"] = "39";

        }

        private void BindAll()
        {
            BindBook(ddlPABook);
            BindBook(ddlLCBook);
            BindBook(ddlPPBook);
            BindInvoiceType(ddlPPInvType);
            BindProfitCenters(ddlPPProfitCenter);
            BindLevel(ddlPPLevel, string.Empty, string.Empty, false);
            BindBookGrid(string.Empty);
            BindItemStatus(ddlLCStatus);
            BindCurrency(ddlLCCurrency, string.Empty);
            BindLevelGrid(string.Empty, string.Empty);
            BindProfitCentersToGrid(string.Empty, string.Empty, false);
            BindPriceDefinitionToGrid();
            BindPriceCategory(ddlPriceCat);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ad_hoc_sessions();
            if (!IsPostBack)
            {
                _priceBookRefCreation = new List<PriceBookRef>();
                _masterProfitCenter = new List<MasterProfitCenter>();
                _priceDefinitionRef = new List<PriceDefinitionRef>();
                BindAll();
            }

            FileUpload1.Attributes.Add("onchange", "return checkFileExtension(this);");

            txtLCLevel.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnLCLevel.ClientID + "')");
            txtBCBook.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnPriceBook.ClientID + "')");

            txtBCBook.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnBCBook, ""));
            ddlLCBook.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnLCBook, ""));
            txtLCLevel.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnLCLevel, ""));
            ddlLCStatus.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnLCStatus, ""));

            txtPPCode.Attributes.Add("onKeyup", this.Page.ClientScript.GetPostBackEventReference(this.btnPPCode, ""));
            txtPPDescription.Attributes.Add("onKeyup", this.Page.ClientScript.GetPostBackEventReference(this.btnPPDescription, ""));
        }

        #region  Search Area

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(GlbUserComCode + seperator + ddlLCBook.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {

                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void imgBtnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            // MasterCommonSearchUCtrl.ReturnResultControl = txtCustomer.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnInvType_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceTypeData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            // MasterCommonSearchUCtrl.ReturnResultControl = txtInvType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnPriceBook_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtBCBook.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnPriceLevel_Click(object sender, ImageClickEventArgs e)
        {

            if (string.IsNullOrEmpty(ddlLCBook.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price book!");
                ddlLCBook.Focus();
                return;
            }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelByBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtLCLevel.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnStatus_Click(object sender, ImageClickEventArgs e)
        {

            //if (string.IsNullOrEmpty(txtPriceBook.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price book!");
            //    txtPriceBook.Focus();
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtPriceLevel.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price book!");
            //    txtPriceLevel.Focus();
            //    return;
            //}

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelItemStatusData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //MasterCommonSearchUCtrl.ReturnResultControl = txtStatus.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #endregion

        #region Data Bind Area
        private void BindBook(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            List<PriceBookRef> _list = CHNLSVC.Sales.GetPriceBooklist(GlbUserComCode);
            _list.Add(new PriceBookRef { Sapb_pb = "" });
            _ddl.DataSource = _list.OrderBy(items => items.Sapb_pb);
            _ddl.DataTextField = "Sapb_pb";
            _ddl.DataValueField = "Sapb_pb";
            _ddl.DataBind();

        }

        private void BindLevel(DropDownList _ddl, string _book, string _level, bool _isDistinct)
        {
            if (_isDistinct == false)
            {
                _ddl.Items.Clear();
                List<PriceBookLevelRef> _list = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _book, _level);
                _list.Add(new PriceBookLevelRef { Sapl_pb_lvl_cd = "" });
                _ddl.DataSource = _list.OrderBy(items => items.Sapl_pb_lvl_cd);
                _ddl.DataTextField = "Sapl_pb_lvl_cd";
                _ddl.DataValueField = "Sapl_pb_lvl_cd";
                _ddl.DataBind();
            }
            else
            {
                _ddl.Items.Clear();
                List<PriceBookLevelRef> _list = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _book, _level);
                List<PriceBookLevelRef> _lists = new List<PriceBookLevelRef>();
                var _load = _list.Select(item => new { Sapl_pb_lvl_cd = item.Sapl_pb_lvl_cd }).Distinct();
                _lists.Add(new PriceBookLevelRef { Sapl_pb_lvl_cd = "" });
                foreach (var _new in _load) { PriceBookLevelRef _set = new PriceBookLevelRef(); _set.Sapl_pb_lvl_cd = _new.Sapl_pb_lvl_cd; _lists.Add(_set); }
                _ddl.DataSource = _lists;
                _ddl.DataTextField = "Sapl_pb_lvl_cd";
                _ddl.DataValueField = "Sapl_pb_lvl_cd";
                _ddl.DataBind();
            }
        }

        private void BindItemStatus(DropDownList _ddl)
        {

            _ddl.Items.Clear();
            DataTable _list = CHNLSVC.Inventory.GetAllCompanyStatus(GlbUserComCode);
            DataRow _r = _list.NewRow();
            _list.Rows.InsertAt(_r, 0);
            _ddl.DataSource = _list;
            _ddl.DataTextField = "Mic_cd";
            _ddl.DataValueField = "Mic_cd";
            _ddl.DataBind();
        }

        private void BindCurrency(DropDownList _ddl, string _code)
        {
            _ddl.Items.Clear();
            List<MasterCurrency> _list = CHNLSVC.General.GetAllCurrency(_code);
            _list.Add(new MasterCurrency { Mcr_cd = "" });
            _ddl.DataSource = _list.OrderBy(items => items.Mcr_cd);
            _ddl.DataTextField = "Mcr_cd";
            _ddl.DataValueField = "Mcr_cd";
            _ddl.DataBind();
        }

        private void BindBookGrid(string _book)
        {
            _priceBookRefCreation = CHNLSVC.Sales.GetPriceBooklist(GlbUserComCode);
            gvBCBook.DataSource = _priceBookRefCreation;
            gvBCBook.DataBind();
        }

        private void BindLevelGrid(string _book, string _level)
        {
            DataTable _table = CHNLSVC.Sales.GetPriceLevelTable(GlbUserComCode, _book, _level);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvLCLevel.DataSource = _table;
            }
            else
            {
                gvLCLevel.DataSource = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _book, _level);

            }
            gvLCLevel.DataBind();
        }

        protected void LoadPriceLevels(object sender, EventArgs e)
        {
            string _book = ((DropDownList)sender).SelectedValue.ToString();
            BindLevel(ddlPALevel, _book, string.Empty, false);
        }

        protected void BookRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "True")
                {
                    e.Row.Cells[2].Text = "Active";
                }
                else
                {
                    e.Row.Cells[2].Text = "Inactive";
                }
            }
        }

        protected void BindProfitCenters(DropDownList _ddl)
        {

            _ddl.Items.Clear();
            List<MasterProfitCenter> _list = CHNLSVC.Sales.GetProfitCenterList(GlbUserComCode, string.Empty);
            _list.Add(new MasterProfitCenter { Mpc_cd = "" });
            _ddl.DataSource = _list.OrderBy(items => items.Mpc_cd);
            _ddl.DataTextField = "Mpc_cd";
            _ddl.DataValueField = "Mpc_cd";
            _ddl.DataBind();
        }

        protected void BindProfitCentersToGrid(string _pcenter, string _description, bool _isSearch)
        {
            DataTable _table = CHNLSVC.Sales.GetProfitCenterTable(GlbUserComCode, _pcenter);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvPPProfits.DataSource = _table;
            }
            else
            {
                if (_isSearch == false)
                {
                    _masterProfitCenter = CHNLSVC.Sales.GetProfitCenterList(GlbUserComCode, _pcenter);

                    gvPPProfits.DataSource = _masterProfitCenter;
                }
                else
                {
                    _masterProfitCenter = CHNLSVC.Sales.GetProfitCenterListbyLike(GlbUserComCode, _pcenter, _description);
                    gvPPProfits.DataSource = _masterProfitCenter;
                }

            }
            gvPPProfits.DataBind();
        }

        protected void gvPPProfits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox c = (CheckBox)e.Row.FindControl("chkPPSelectAll");
                c.Attributes.Add("onclick", "SelectAll('" + c.ClientID + "')");
            }
        }

        protected void BindPriceDefinitionToGrid()
        {
            DataTable _table = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevelTable(GlbUserComCode, ddlPPBook.SelectedValue.ToString(), ddlPPLevel.SelectedValue.ToString(),string.Empty,string.Empty);
            if (_table.Rows.Count <= 0)
            {
                _table = SetEmptyRow(_table);
                gvPPPCAssign.DataSource = _table;

            }
            else
            {
                gvPPPCAssign.DataSource = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(GlbUserComCode, ddlPPBook.SelectedValue.ToString(), ddlPPLevel.SelectedValue.ToString(),string.Empty,string.Empty);
            }
            pnlPPPCAssign.DataBind();

            if (_table.Rows.Count > 0)
            {
                _table.Rows.Clear();
                _table = SetEmptyRow(_table);
            }
            gvPPEntry.DataSource = _table;
            gvPPEntry.DataBind();
        }

        protected void BindInvoiceType(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            List<MasterInvoiceType> _list = CHNLSVC.Sales.GetAllInvoiceType();
            _list.Add(new MasterInvoiceType { Srtp_cd = "" });
            _ddl.DataSource = _list.OrderBy(items => items.Srtp_cd);
            _ddl.DataTextField = "Srtp_cd";
            _ddl.DataValueField = "Srtp_cd";
            _ddl.DataBind();
        }

        protected void BindPriceCategory(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            List<PriceCategoryRef> _list = CHNLSVC.Sales.GetAllPriceCategory(string.Empty);
            _list.Add(new PriceCategoryRef { Sarpc_cd = "" });
            _ddl.DataSource = _list.OrderBy(items => items.Sarpc_cd);
            _ddl.DataTextField = "Sarpc_cd";
            _ddl.DataValueField = "Sarpc_cd";
            _ddl.DataBind();
        }

        protected void BindPriceType(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            _list.Add(new PriceTypeRef { Sarpt_cd = "", Sarpt_indi = -1 });
            _ddl.DataSource = _list.OrderBy(items => items.Sarpt_cd);
            _ddl.DataTextField = "Sarpt_cd";
            _ddl.DataValueField = "Sarpt_indi";
            _ddl.DataBind();
        }

        #endregion

        #region  Price Book Creation

        private void CheckBCBook()
        {
            if (string.IsNullOrEmpty(txtBCBook.Text)) return;
            PriceBookRef _pbook = CHNLSVC.Sales.GetPriceBook(GlbUserComCode, txtBCBook.Text.Trim().ToUpper());
            if (_pbook.Sapb_pb != null)
            {
                txtBCDescription.Text = _pbook.Sapb_desc;
                ddlBCStatus.SelectedValue = _pbook.Sapb_act == true ? "Active" : "Inactive";
            }
            else
            {
                txtBCDescription.Text = "";
                ddlBCStatus.SelectedValue = "Active";
            }
        }

        protected void CheckBCBook(object sender, EventArgs e)
        {
            CheckBCBook();
        }

        protected void AddBook(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBCBook.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(txtBCDescription.Text))
            {
                return;
            }


            var _exist = from _lists in _priceBookRefCreation
                         where _lists.Sapb_com == GlbUserComCode && _lists.Sapb_pb == txtBCBook.Text.Trim().ToUpper()
                         select _lists;

            if (_exist.Count() <= 0)
            {
                PriceBookRef _list = new PriceBookRef();
                _list.Sapb_pb = txtBCBook.Text.Trim();
                _list.Sapb_desc = txtBCDescription.Text.Trim();
                _list.Sapb_act = ddlBCStatus.SelectedValue == "Active" ? true : false;
                _list.Sapb_com = GlbUserComCode;
                _list.Sapb_cre_by = GlbUserName;
                _list.Sapb_cre_when = DateTime.Now;
                _list.Sapb_hierachy_lvl = 0;
                _list.Sapb_mod_by = GlbUserName;
                _list.Sapb_mod_when = DateTime.Now;
                _priceBookRefCreation.Add(_list);
            }
            else
            {
                foreach (PriceBookRef l in _exist)
                {
                    l.Sapb_desc = txtBCDescription.Text.Trim();
                    l.Sapb_act = ddlBCStatus.SelectedValue == "Active" ? true : false;

                }
            }

            gvBCBook.DataSource = _priceBookRefCreation;
            gvBCBook.DataBind();

            txtBCBook.Text = "";
            txtBCDescription.Text = "";
            txtBCBook.Focus();
        }

        protected void SavePriceBook(object sender, EventArgs e)
        {
            if (_priceBookRefCreation.Count > 0)
            {
                CHNLSVC.Sales.SavePriceBook(_priceBookRefCreation);
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed successfully!");
            }
        }

        #endregion

        #region Price Book/Level Creation

        #region Check Levek Create - Book
        private void CheckLCBook()
        {
            if (string.IsNullOrEmpty(ddlLCBook.SelectedValue.ToString())) return;
            BindLevelGrid(ddlLCBook.SelectedValue.ToString(), string.Empty);
        }
        protected void CheckLCBook(object sender, EventArgs e)
        {
            CheckLCBook();
        }
        #endregion

        #region Check Levek Create - Level
        private void CheckLCLevel()
        {
            if (string.IsNullOrEmpty(txtLCLevel.Text.ToString()))
            {
                BindLevelGrid(ddlLCBook.SelectedValue.ToString(), string.Empty);
            }
            BindLevelGrid(ddlLCBook.SelectedValue.ToString(), txtLCLevel.Text.Trim());
        }
        protected void CheckLCLevel(object sender, EventArgs e)
        {
            CheckLCLevel();
        }
        #endregion

        protected void ClearLevelCreation(object sender, EventArgs e)
        {
            ClearLevelCreation();
        }

        private void ClearLevelCreation()
        {
            BindAll();

            txtLCCreditPeriod.Text = "";
            txtLCLevel.Text = "";
            txtLCWaraPeriod.Text = "";

            chkLCIsCheckStatus.Checked = false;
            chkLCIsSerialzed.Checked = false;
            chkLCIsWarranty.Checked = false;
            chkLCTotalQty.Checked = false;
            chkLCTransfer.Checked = false;
            chkLCVat.Checked = false;
            chkLCWOPrice.Checked = false;
        }

        private PriceBookLevelRef FillLevel()
        {
            PriceBookLevelRef _list = new PriceBookLevelRef();
            _list.Sapl_act = true;
            _list.Sapl_base_on_tot_inv_qty = chkLCTotalQty.Checked ? true : false;
            _list.Sapl_chk_st_tp = chkLCIsCheckStatus.Checked ? true : false;
            _list.Sapl_com_cd = GlbUserComCode;
            _list.Sapl_cre_by = GlbUserName;
            _list.Sapl_cre_when = DateTime.Now;
            _list.Sapl_credit_period = Convert.ToInt32(txtLCCreditPeriod.Text);
            _list.Sapl_currency_cd = ddlLCCurrency.SelectedValue.ToString();
            _list.Sapl_erp_ref = txtLCLevel.Text.Trim();
            _list.Sapl_grn_com = null;
            _list.Sapl_is_def = false;
            _list.Sapl_is_for_po = false;
            _list.Sapl_is_pos = false;
            _list.Sapl_is_print = false;
            _list.Sapl_is_sales = false;
            _list.Sapl_is_serialized = chkLCIsSerialzed.Checked ? true : false;
            _list.Sapl_is_transfer = chkLCTransfer.Checked ? true : false;
            _list.Sapl_is_valid = true;
            _list.Sapl_is_without_p = chkLCWOPrice.Checked ? true : false;
            _list.Sapl_itm_stuts = ddlLCStatus.SelectedValue.ToString();
            _list.Sapl_mod_by = GlbUserName;
            _list.Sapl_mod_when = DateTime.Now;
            _list.Sapl_pb = ddlLCBook.SelectedValue.ToString();
            _list.Sapl_pb_lvl_cd = txtLCLevel.Text.Trim();
            _list.Sapl_pb_lvl_desc = "Price Level " + txtLCLevel.Text;
            _list.Sapl_set_warr = chkLCIsWarranty.Checked ? true : false;
            _list.Sapl_vat_calc = chkLCVat.Checked ? true : false;
            _list.Sapl_warr_period = Convert.ToInt32(txtLCWaraPeriod.Text.Trim());
            return _list;
        }

        protected void SaveLevel(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlLCBook.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price book");
                ddlLCBook.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtLCLevel.Text.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price level");
                txtLCLevel.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlLCStatus.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price status");
                ddlLCStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlLCCurrency.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price currency");
                ddlLCCurrency.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtLCCreditPeriod.Text.ToString()))
            {
                txtLCCreditPeriod.Text = "0";
            }

            if (chkLCIsWarranty.Checked == true)
            {
                if (string.IsNullOrEmpty(txtLCWaraPeriod.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the warranty period");
                    txtLCWaraPeriod.Focus();
                    return;
                }
            }
            else
            {
                txtLCWaraPeriod.Text = "0";
            }

            PriceBookLevelRef _list = new PriceBookLevelRef();
            _list = FillLevel();

            Int32 _effect = CHNLSVC.Sales.SavePriceLevel(_list);
            ClearLevelCreation();

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed successfully!");




        }

        private void AssignValue(PriceBookLevelRef _list)
        {
            ddlLCBook.SelectedValue = _list.Sapl_pb;
            txtLCLevel.Text = _list.Sapl_pb_lvl_cd;
            ddlLCStatus.SelectedValue = _list.Sapl_itm_stuts;
            chkLCIsCheckStatus.Checked = _list.Sapl_chk_st_tp ? true : false;
            ddlLCCurrency.SelectedValue = _list.Sapl_currency_cd;
            txtLCCreditPeriod.Text = _list.Sapl_credit_period.ToString();
            chkLCIsWarranty.Checked = _list.Sapl_set_warr ? true : false;
            txtLCWaraPeriod.Text = _list.Sapl_warr_period.ToString();
            chkLCIsSerialzed.Checked = _list.Sapl_is_serialized ? true : false;
            chkLCWOPrice.Checked = _list.Sapl_is_without_p ? true : false;
            chkLCTransfer.Checked = _list.Sapl_is_transfer ? true : false;
            chkLCVat.Checked = _list.Sapl_vat_calc ? true : false;
            chkLCTotalQty.Checked = _list.Sapl_base_on_tot_inv_qty ? true : false;
        }
        private void LoadDetailOnStatus(string _book, string _level, string status)
        {
            List<PriceBookLevelRef> _list = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _book, _level);
            var _loaded = from _listn in _list where _listn.Sapl_itm_stuts == status select _listn;
            if (_loaded != null) { foreach (var _single in _loaded) { AssignValue(_single); } }
        }

        protected void CheckLCStatus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlLCBook.SelectedValue.ToString())) return; if (string.IsNullOrEmpty(txtLCLevel.Text.Trim())) return; if (string.IsNullOrEmpty(ddlLCStatus.SelectedValue.ToString())) return;
            LoadDetailOnStatus(ddlLCBook.SelectedValue.ToString(), txtLCLevel.Text.Trim(), ddlLCStatus.SelectedValue.ToString());
        }

        #endregion

        #region  Pricing Parameter

        protected void LoadPriceDeinitionAtBook(object sender, EventArgs e)
        {
            BindPriceDefinitionToGrid();
        }

        protected void LoadPPLevels(object sender, EventArgs e)
        {
            string _book = ((DropDownList)sender).SelectedValue.ToString();
            BindLevel(ddlPPLevel, _book, string.Empty, true);
            BindPriceDefinitionToGrid();
        }

        protected void LoadPPCentersByCodeLike(object sender, EventArgs e)
        {
            Button _searchText = (Button)(sender);

            if (string.IsNullOrEmpty(txtPPCode.Text) && _searchText.ID == "btnPPCode")
            {
                BindProfitCentersToGrid(string.Empty, string.Empty, false);
                txtPPCode.Focus();
            }

            else if (string.IsNullOrEmpty(txtPPDescription.Text) && _searchText.ID == "btnPPDescription")
            {
                BindProfitCentersToGrid(string.Empty, string.Empty, false);
                txtPPDescription.Focus();
            }
            else
            {

                if (_searchText.ID == "btnPPCode")
                {
                    BindProfitCentersToGrid("%" + txtPPCode.Text.Trim() + "%", string.Empty, true);
                    txtPPCode.Focus();
                }

                else if (_searchText.ID == "btnPPDescription")
                {
                    BindProfitCentersToGrid("%" + txtPPCode.Text.Trim() + "%", "%" + txtPPDescription.Text.Trim() + "%", true);
                    txtPPDescription.Focus();
                }

            }

        }

        protected void AddItem(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ddlPPBook.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price book");
                ddlPPBook.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPPLevel.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price level");
                ddlPPLevel.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPPInvType.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the invoice type");
                ddlPPInvType.Focus();
                return;
            }

            if (_masterProfitCenter.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the profit center(s)");
                return;
            }


            if (_masterProfitCenter.Count > 0)
            {
                foreach (MasterProfitCenter _list in _masterProfitCenter)
                {

                    PriceDefinitionRef _single = new PriceDefinitionRef();
                    _single.Sadd_chk_credit_bal = false;
                    _single.Sadd_com = GlbUserComCode;
                    _single.Sadd_cre_by = GlbUserName;
                    _single.Sadd_cre_when = DateTime.Now;
                    _single.Sadd_disc_rt = 0;
                    _single.Sadd_doc_tp = ddlPPInvType.SelectedValue.ToString();
                    _single.Sadd_is_bank_ex_rt = false;
                    _single.Sadd_is_disc = false;
                    _single.Sadd_mod_by = GlbUserName;
                    _single.Sadd_mod_when = DateTime.Now;
                    _single.Sadd_p_lvl = ddlPPLevel.SelectedValue.ToString();
                    _single.Sadd_pb = ddlPPBook.SelectedValue.ToString();
                    _single.Sadd_pc = _list.Mpc_cd;

                    _priceDefinitionRef.Add(_single);

                }

                gvPPEntry.DataSource = _priceDefinitionRef;
                gvPPEntry.DataBind();


            }
        }

        protected void SavePriceDefinition(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPPBook.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price book");
                ddlPPBook.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPPLevel.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price level");
                ddlPPLevel.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPPInvType.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the invoice type");
                ddlPPInvType.Focus();
                return;
            }

            if (_priceDefinitionRef.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the entry for save");
                return;
            }

            Int32 effect = CHNLSVC.Sales.SavePriceDefinition(_priceDefinitionRef);
            if (effect == 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed successfully!");
            }

        }
        #endregion

        #region Price Upload

        protected void LoadPriceTypeFromPriceCategory(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPriceCat.SelectedValue.ToString())) return;

            List<PriceCategoryRef> _list = CHNLSVC.Sales.GetAllPriceCategory(ddlPriceCat.SelectedValue.ToString());
            PriceCategoryRef _category = new PriceCategoryRef();
            var _single = from _lists in _list
                          select _lists;

            foreach (var _one in _single)
            {
                _category = _one;
            }

            if (_category.Sarpc_is_type == true)
            {
                BindPriceType(ddlPriceType);
            }
            else
            {
                ddlPriceType.Items.Clear();
            }


        }

        public void AddPricedItem(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFrom.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid date range");
                txtFrom.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtTo.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid date range");
                txtTo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPABook.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price book");
                ddlPABook.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPALevel.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price level");
                ddlPALevel.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCirculer.Text.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the circuler");
                txtCirculer.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPriceCat.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the category");
                ddlPriceCat.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlPriceCat.SelectedValue.ToString())) return;

            List<PriceCategoryRef> _list = CHNLSVC.Sales.GetAllPriceCategory(ddlPriceCat.SelectedValue.ToString());
            PriceCategoryRef _category = new PriceCategoryRef();
            var _single = from _lists in _list
                          select _lists;

            foreach (var _one in _single)
            {
                _category = _one;
            }

            if (_category.Sarpc_is_type == true)
            {
                if (string.IsNullOrEmpty(ddlPriceType.SelectedValue.ToString()))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price type");
                    ddlPriceType.Focus();
                    return;
                }
            }
        }


        public string FilePath
        {
            get { return Session["FilePath"].ToString(); }
            set { Session["FilePath"] = value; }
        }


        protected void UploadPrice(object sender, EventArgs e)
        {
            FilePath = FileUpload1.PostedFile.FileName;

            if (FileUpload1.HasFile == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the file");
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(FilePath);

            if (fileObj.Exists == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected file does not exist at the following path");
                return;
            }

            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            string conStr = "";

            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;
                    break;
            }

            conStr = String.Format(conStr, FilePath, 0);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();


            List<PriceDetailRef> _list = new List<PriceDetailRef>();


            Int32 _currentRow = 1;

            foreach (DataRow _row in dt.Rows)
            {
                string _combineItem = "";
                Int32 _combineQty = 0;
                decimal _combinePrice = 0;

                PriceDetailRef _one = new PriceDetailRef();
                //Check Item Code
                //Check Price Book
                //Check Price Level
                //Check Back Date

                string _book = Convert.ToString(_row["A"]);
                string _level = Convert.ToString(_row["B"]);
                string _mainItem = Convert.ToString(_row["C"]);
                if (_row["D"] == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid 'from date'. please check the row - " + _currentRow.ToString());
                    return;
                }
                DateTime _fromDate = Convert.ToDateTime(_row["D"]);
                DateTime _toDate;
                if (_row["E"] == DBNull.Value) _toDate = DateTime.MaxValue;
                else _toDate = Convert.ToDateTime(_row["E"]);
                Int32 _fromQty = Convert.ToInt32(_row["F"]);
                Int32 _toQty = Convert.ToInt32(_row["G"]);
                Decimal _UPrice = Convert.ToDecimal(_row["H"]);
                string _serial = Convert.ToString(_row["I"]);
                string _warrantyRemarks = Convert.ToString(_row["S"]);

                bool _HPAllow;
                if (_row["T"] == DBNull.Value) _HPAllow = false;
                else _HPAllow = Convert.ToBoolean(_row["T"]);
                
                string _status = Convert.ToString(_row["U"]);

                bool _isValidItem = CHNLSVC.Inventory.IsValidItem(GlbUserComCode, _mainItem);
                bool _isValidBook = CHNLSVC.Sales.IsValidBook(GlbUserComCode, _book);
                bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(GlbUserComCode, _book, _level);

                if (_isValidItem==false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item code in the document is invalid. please check the row - " + _currentRow.ToString());
                    return;
                }

                if (_isValidBook == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Book code in the document is invalid. please check the row - " + _currentRow.ToString());
                    return;
                }

                if (_isValidLevel == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Level code in the document is invalid. please check the row - " + _currentRow.ToString());
                    return;
                }

                MasterItem _oneItem = new MasterItem();
                _oneItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _mainItem);


                PriceBookLevelRef _priceLevel = new PriceBookLevelRef();
                _priceLevel = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, _book, _level);

                if (_priceLevel.Sapl_is_serialized && string.IsNullOrEmpty(_serial))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Serialized price level found without seriali no. please check the row - " + _currentRow.ToString());
                    return;
                }


                _one.Sapd_warr_remarks = _warrantyRemarks;
                _one.Sapd_upload_dt = DateTime.Now;
                _one.Sapd_update_dt = DateTime.Now;
                _one.Sapd_to_date = DateTime.Now;
                _one.Sapd_session_id = GlbUserSessionID;
                _one.Sapd_seq_no = 1;
                _one.Sapd_qty_to = _toQty;
                _one.Sapd_qty_from = _fromQty;
                _one.Sapd_price_type = 0;
                _one.Sapd_price_stus = _status;
                _one.Sapd_pbk_lvl_cd = _level;
                _one.Sapd_pb_tp_cd = "";
                _one.Sapd_pb_seq = 1;
                _one.Sapd_no_of_use_times = 0;
                _one.Sapd_no_of_times = 0;
                _one.Sapd_model = _oneItem.Mi_model;
                _one.Sapd_mod_when = DateTime.Now;
                _one.Sapd_mod_by = GlbUserName;
                _one.Sapd_margin = 0;
                _one.Sapd_lst_cost = 0;
                _one.Sapd_itm_price = _UPrice;
                _one.Sapd_itm_cd = _mainItem;
                _one.Sapd_is_cancel = false;
                _one.Sapd_is_allow_individual = false;
                _one.Sapd_from_date = _fromDate;
                _one.Sapd_erp_ref = "";
                _one.Sapd_dp_ex_cost = 0;
                _one.Sapd_day_attempt = 0;
                _one.Sapd_customer_cd = "";
                _one.Sapd_cre_when = DateTime.Now;
                _one.Sapd_cre_by = GlbUserName;
                _one.Sapd_circular_no = "";
                _one.Sapd_cancel_dt = DateTime.MinValue; ;
                _one.Sapd_avg_cost = 0;
                _one.Sapd_apply_on = "0";



                _list.Add(_one);
                _currentRow += 1;
            }



            //Bind Data to GridView
            gvPUEntry.DataSource = _list;
            gvPUEntry.DataBind();






        }

        #endregion

        protected void Close(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
    }
}