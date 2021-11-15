using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using FF.BusinessObjects;
using System.Globalization;

namespace FF.WebERPClient.General_Modules
{
    public partial class ReturnRequestApproval : BasePage
    {

        protected List<ReptPickSerials> _popUpList
        {
            get { return (List<ReptPickSerials>)Session["_popUpList"]; }
            set { Session["_popUpList"] = value; }
        }
        protected List<ReptPickSerials> _selectedItemList
        {
            get { return (List<ReptPickSerials>)Session["_selectedItemList"]; }
            set { Session["_selectedItemList"] = value; }
        }
        protected string InvoiceType
        {
            get { return Convert.ToString(Session["InvoiceType"]); }
            set { Session["InvoiceType"] = value; }
        }
        private bool IsPriceBookChanged
        {
            get { return Convert.ToBoolean(Session["IsPriceBookChanged"]); }
            set { Session["IsPriceBookChanged"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _invoiceItemList = new List<InvoiceItem>();
                _lineNo = 0;
                InvoiceType = "CS";
                _selectedItemList = new List<ReptPickSerials>();
                BindAccountItem(string.Empty, true);
                BindSelectedItems(null);

                txtAWarrantyNo.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnAWarranty.ClientID + "')");
                txtASerialNo.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnASerial.ClientID + "')");
                txtADocumentNo.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnAInvoice.ClientID + "')");

                txtADocumentNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnDocument, ""));

                txtOutItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnInvNo.ClientID + "')");
                txtOutQty.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnQty, ""));

                txtPriceBook.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnBook, ""));
                txtPriceLevel.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPriceLevel, ""));

                txtDate.Text = DateTime.Now.Date.ToShortDateString();

                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT010, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);

                GlbReqIsApprovalNeed = true;
                GlbReqUserPermissionLevel = 3;
                GlbReqIsFinalApprovalUser = true;
                GlbReqIsRequestGenerateUser = true;
            }
        }
        private void BindAccountItem(string _account, bool _isHireSale)
        {
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            InvoiceHeader _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_account);

            if (_isHireSale)
            {

                if (_hdrs != null)
                    _account = _hdrs.Sah_anal_2;
                List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, _account);

                DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(GlbUserComCode, GlbUserDefProf, _account);
                if (_table.Rows.Count <= 0)
                {
                    //_table = SetEmptyRow(_table);
                    gvATradeItem.DataSource = _table;
                }
                else if (_invoice != null)
                    if (_invoice.Count > 0)
                    {
                        _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                        #region New Method
                        var _sales = from _lst in _invoice
                                     where _lst.Sah_direct == true
                                     select _lst;

                        foreach (InvoiceHeader _hdr in _sales)
                        {
                            string _invoiceno = _hdr.Sah_inv_no;
                            List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                            var _forwardSale = from _lst in _invItem
                                               where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                               select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                            var _deliverdSale = from _lst in _invItem
                                                where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                                select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                            if (_forwardSale.Count() > 0)
                                _itemList.AddRange(_forwardSale);

                            if (_deliverdSale.Count() > 0)
                                _itemList.AddRange(_deliverdSale);
                        }
                        #endregion
                        gvATradeItem.DataSource = _itemList;
                    }

                gvATradeItem.DataBind();
            }
            else
            {
                if (ddlADocType.SelectedValue.ToString() == "Cash")
                {

                    List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_account);
                    var _forwardSale = from _lst in _invItem
                                       where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                       select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                    var _deliverdSale = from _lst in _invItem
                                        where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                        select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                    if (_forwardSale.Count() > 0)
                        _itemList.AddRange(_forwardSale);

                    if (_deliverdSale.Count() > 0)
                        _itemList.AddRange(_deliverdSale);

                }
                else if (ddlADocType.SelectedValue.ToString() == "Request")
                {
                    RequestApprovalHeader _hdrss = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(GlbUserComCode, GlbUserDefProf, txtADocumentNo.Text.Trim());
                    if (_hdrss.Grah_remaks == "Cash")
                    {
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_account);
                        var _forwardSale = from _lst in _invItem
                                           where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                           select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                        var _deliverdSale = from _lst in _invItem
                                            where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                            select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                        if (_forwardSale.Count() > 0)
                            _itemList.AddRange(_forwardSale);

                        if (_deliverdSale.Count() > 0)
                            _itemList.AddRange(_deliverdSale);
                    }
                    else if (_hdrss.Grah_remaks == "Hire")
                    {
                        if (_hdrs != null)
                            _account = _hdrs.Sah_anal_2;
                        List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, _account);

                        DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(GlbUserComCode, GlbUserDefProf, _account);
                        if (_table.Rows.Count <= 0)
                        {
                            //_table = SetEmptyRow(_table);
                            gvATradeItem.DataSource = _table;
                        }
                        else if (_invoice != null)
                            if (_invoice.Count > 0)
                            {
                                _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                                #region New Method
                                var _sales = from _lst in _invoice
                                             where _lst.Sah_direct == true
                                             select _lst;

                                foreach (InvoiceHeader _hdr in _sales)
                                {
                                    string _invoiceno = _hdr.Sah_inv_no;
                                    List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                                    var _forwardSale = from _lst in _invItem
                                                       where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                                       select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                                    var _deliverdSale = from _lst in _invItem
                                                        where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                                        select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                                    if (_forwardSale.Count() > 0)
                                        _itemList.AddRange(_forwardSale);

                                    if (_deliverdSale.Count() > 0)
                                        _itemList.AddRange(_deliverdSale);
                                }
                                #endregion
                                gvATradeItem.DataSource = _itemList;
                            }

                        gvATradeItem.DataBind();
                    }



                }

                gvATradeItem.DataSource = _itemList;
                gvATradeItem.DataBind();
            }

            if (_hdrs != null)
                BindCustomerDetails(_hdrs);

        }

        #region Search Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                //p_com in NVARCHAR2,p_pcenter IN NVARCHAR2,p_doctype in NVARCHAR2,p_invoicetype in NVARCHAR2,p_isHire in NUMBER
                case CommonUIDefiniton.SearchUserControlType.WarraWarrantyNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarraSerialNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GeneralRequest:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + CommonUIDefiniton.SalesType.INV.ToString() + seperator + CommonUIDefiniton.InvoiceType.HS.ToString() + seperator + 0 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HireInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + CommonUIDefiniton.SalesType.INV.ToString() + seperator + CommonUIDefiniton.InvoiceType.HS.ToString() + seperator + 1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + InvoiceType + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + InvoiceType + seperator + txtPriceBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceItem:
                    {
                        paramsText.Append(txtPriceBook.Text + seperator + txtPriceLevel.Text + seperator + 1 + seperator + DateTime.Now.Date.ToShortDateString() + seperator + string.Empty + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void imgBtnAWarranty_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarraWarrantyNo);
            DataTable dataSource = CHNLSVC.CommonSearch.GetWarrantySearchByWarrantyNoSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = imgBtnAWarranty.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnASerial_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarraSerialNo);
            DataTable dataSource = CHNLSVC.CommonSearch.GetWarrantySearchBySerialNoSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtASerialNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnAInvoice_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(ddlADocType.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the document type");
                ddlADocType.Focus();
                return;
            }

            DataTable dataSource = null;

            if (ddlADocType.SelectedValue.ToString().Equals(CommonUIDefiniton.ReturnRequestDocumentType.Cash.ToString()))
            {

                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoice);
                dataSource = CHNLSVC.CommonSearch.GetInvoiceByInvType(MasterCommonSearchUCtrl.SearchParams, null, null);
            }

            if (ddlADocType.SelectedValue.ToString().Equals(CommonUIDefiniton.ReturnRequestDocumentType.Hire.ToString()))
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HireInvoice);
                dataSource = CHNLSVC.CommonSearch.GetInvoiceByInvType(MasterCommonSearchUCtrl.SearchParams, null, null);
            }

            if (ddlADocType.SelectedValue.ToString().Equals(CommonUIDefiniton.ReturnRequestDocumentType.Request.ToString()))
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GeneralRequest);
                dataSource = CHNLSVC.CommonSearch.GetGeneralRequestSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtADocumentNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnPriceBook_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(InvoiceType))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the invoice type!");
                //txtInvType.Focus();
                return;
            }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPriceBook.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnPriceLevel_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(InvoiceType))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the invoice type!");
                //txtInvType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPriceBook.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the price book!");
                txtPriceBook.Focus();
                return;
            }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPriceLevel.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceItem);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtOutItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #endregion
        static int _count = 0;
        protected void AccountItem_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField _hdnIsForward = (HiddenField)e.Row.FindControl("hdnIsForwardSale");
                    HiddenField _hdnInvoiceNo = (HiddenField)e.Row.FindControl("hdnInvoiceNo");
                    HiddenField _hdnDONo = (HiddenField)e.Row.FindControl("hdnDONo");
                    HiddenField _hdnLineNo = (HiddenField)e.Row.FindControl("hdnlineNo");

                    if (Convert.ToBoolean(_hdnIsForward.Value.ToString()) == true)
                    {
                        e.Row.Style.Add("color", "#990000"); //e.Row.Style.Add("background-color", "#9DC2D5");
                        e.Row.Style.Add("font-weight", "bold");
                    }

                    e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                    e.Row.Attributes["ondblclick"] = ClientScript.GetPostBackClientHyperlink(
                                gvATradeItem,
                                String.Concat("Select$", e.Row.RowIndex),
                                true);

                    _count += 1;

                }

        }

        private void BindPopSerial(List<ReptPickSerials> _list, bool _isByRequest)
        {
            if (_isByRequest == false)
            {
                gvPopSerial.DataSource = _list;
                gvPopSerial.DataBind();
            }
            else
            {

            }
        }

        private void BindSelectedItems(List<ReptPickSerials> _list)
        {
            if (_list == null) { DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, -1, string.Empty); gvAReturnItem.DataSource = _table; }
            else if (_list.Count <= 0) { DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, -1, string.Empty); gvAReturnItem.DataSource = _table; }
            else { gvAReturnItem.DataSource = _list; }
            gvAReturnItem.DataBind();
        }

        private void BindItemsFromRequest(string _reqno)
        {
            RequestApprovalHeader _hdr = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(GlbUserComCode, GlbUserDefProf, _reqno);
            string _documentType = _hdr.Grah_remaks;
            List<RequestApprovalHeader> _lst = null;

            if (_documentType == "Cash")
            {
                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                _lst = CHNLSVC.General.GetApprovedRequestDetailsList(GlbUserComCode, GlbUserDefProf, _hdr.Grah_fuc_cd, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014.ToString(), 0, GlbReqUserPermissionLevel);
            }
            if (_documentType == "Hire")
                if (_hdr.Grah_app_tp == "ARQT013")
                {
                    RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _lst = CHNLSVC.General.GetApprovedRequestDetailsList(GlbUserComCode, GlbUserDefProf, _hdr.Grah_fuc_cd, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013.ToString(), 0, GlbReqUserPermissionLevel);
                }
                else if (_hdr.Grah_app_tp == "ARQT008")
                {
                    RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    GlbReqUserPermissionLevel = 3;
                    _lst = CHNLSVC.General.GetApprovedRequestDetailsList(GlbUserComCode, GlbUserDefProf, _hdr.Grah_fuc_cd, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008.ToString(), 0, GlbReqUserPermissionLevel);
                }

            if (_lst != null)
                if (_lst.Count > 0)
                {
                    var _in = (from _k in _lst
                               where _k.Grad_anal5 == "IN"
                               select _k).ToList();

                    var _out = (from _k in _lst
                                where _k.Grad_anal5 == "OUT"
                                select _k).ToList();

                    var _other = (from _k in _lst
                                  where _k.Grad_anal5 == "AMT"
                                  select _k).ToList();


                    List<ReptPickSerials> _ins = new List<ReptPickSerials>();
                    List<InvoiceItem> _outs = new List<InvoiceItem>();

                    string _usageDocType = string.Empty;
                    decimal _value = 0;

                    if (_in != null)
                        if (_in.Count > 0)
                            foreach (RequestApprovalHeader _one in _in)
                            {
                                ReptPickSerials _single = new ReptPickSerials();
                                //ra_det.Grad_ref = ra_hdr.Grah_ref;
                                //ra_det.Grad_line = _count;
                                //ra_det.Grad_req_param = _in.Tus_itm_cd;

                                //ra_det.Grad_val1 = _in.Tus_qty;
                                //ra_det.Grad_val2 = _in.Tus_unit_price;
                                //ra_det.Grad_val3 = _single.Tus_base_itm_line;
                                //ra_det.Grad_val4 = _single.Tus_ser_line;
                                //ra_det.Grad_val5 = _single.Tus_ser_id;

                                //ra_det.Grad_anal1 = txtADocumentNo.Text;
                                //ra_det.Grad_anal2 = _in.Tus_base_doc_no;
                                //ra_det.Grad_anal3 = _in.Tus_doc_no;
                                //ra_det.Grad_anal4 = _in.Tus_ser_1;
                                //ra_det.Grad_anal5 = "IN";
                                _single.Tus_itm_cd = _one.Grad_req_param;
                                _single.Tus_qty = _one.Grad_val1;
                                _single.Tus_unit_price = _one.Grad_val2;
                                _single.Tus_base_itm_line = Convert.ToInt32(_one.Grad_val3);
                                _single.Tus_ser_line = Convert.ToInt32(_one.Grad_val4);
                                _single.Tus_ser_id = Convert.ToInt32(_one.Grad_val5);
                                _single.Tus_base_doc_no = _one.Grad_anal2;
                                _single.Tus_doc_no = _one.Grad_anal3;
                                _single.Tus_ser_1 = _one.Grad_anal4;

                                _ins.Add(_single);
                            }

                    if (_out != null)
                        if (_out.Count > 0)
                            foreach (RequestApprovalHeader _one in _out)
                            {
                                InvoiceItem _single = new InvoiceItem();

                                _single.Sad_itm_cd = _one.Grad_req_param;
                                _single.Sad_qty = _one.Grad_val1;
                                _single.Sad_unit_amt = _one.Grad_val2;
                                _single.Sad_pb_price = _one.Grad_val3;
                                _single.Sad_itm_tax_amt = _one.Grad_val4;
                                _single.Sad_seq = Convert.ToInt32(_one.Grad_val5);

                                _single.Sad_itm_stus = _one.Grad_anal2;
                                _single.Sad_promo_cd = _one.Grad_anal3;
                                _single.Sad_seq_no = Convert.ToInt32(_one.Grad_anal4);

                                _outs.Add(_single);

                            }

                    if (_other != null)
                        if (_other.Count > 0)
                            foreach (RequestApprovalHeader _one in _other)
                            {
                                _usageDocType = _one.Grad_req_param;
                                _value = _one.Grad_val2;
                            }

                    if (_outs != null)
                        if (_outs.Count > 0)
                        {
                            gvAReturnItem.DataSource = _ins ;
                            gvAReturnItem.DataBind();
                        }

                    if (_ins != null)
                        if (_ins.Count > 0)
                        {
                            gvExchangeOutItm.DataSource = _outs;
                            gvExchangeOutItm.DataBind();
                        }
                    if (string.IsNullOrEmpty(_usageDocType))
                    {
                        ddlUsageType.SelectedValue = _usageDocType;
                        txtUsageAmount.Text = FormatToCurrency(_value.ToString());
                    }
                }
        }

        //Add double click item to IN area
        private void AddInItem(ReptPickSerials _ser)
        {
            _popUpList.Add(_ser);
        }
        protected void BindSelectedAccItemetail(Object sender, EventArgs e)
        {
            //DataKeyNames="Sad_itm_cd,Sad_qty,Sad_unit_rt,Sad_itm_line,Mi_act,sad_inv_no"
            string _sad_itm_cd = gvATradeItem.SelectedDataKey[0].ToString();
            decimal _sad_qty = Convert.ToDecimal(gvATradeItem.SelectedDataKey[1].ToString());
            decimal _sad_unit_rt = Convert.ToDecimal(gvATradeItem.SelectedDataKey[2].ToString());
            Int32 _sad_itm_line = Convert.ToInt32(gvATradeItem.SelectedDataKey[3].ToString());
            bool _isForwardSale = Convert.ToBoolean(gvATradeItem.SelectedDataKey[4].ToString());
            string _invoice = gvATradeItem.SelectedDataKey[5].ToString();
            string _status = gvATradeItem.SelectedDataKey[6].ToString();

            MasterItem _itms = CHNLSVC.Inventory.GetItem(GlbUserComCode, _sad_itm_cd);

            if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == false)
            {

                if (_itms.Mi_is_ser1 == -1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Exchange not processing for decimal allow items");
                    return;
                }
                List<InventoryBatchN> _lst = new List<InventoryBatchN>();
                _lst = CHNLSVC.Inventory.GetDeliveryOrderDetail(GlbUserComCode, _invoice, _sad_itm_line);

                string _docno = string.Empty;
                int _itm_line = -1;
                int _batch_line = -1;

                List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(GlbUserComCode, GlbUserDefLoca, GlbUserName, GlbUserSessionID, string.Empty, _invoice, _sad_itm_line);
                _popUpList = _serLst;
                if (_lst != null && _serLst != null)
                    if (_sad_qty > 1)
                    {
                        //More than one qty
                        BindPopSerial(_serLst, false);
                        MPESerial.Show();
                    }
                    else
                    {
                        //only one qty
                        _docno = _lst[0].Inb_doc_no;
                        _itm_line = _lst[0].Inb_itm_line;
                        _batch_line = _lst[0].Inb_batch_line;
                        AddInItem(_serLst[0]);
                        foreach (ReptPickSerials _lt in _popUpList)
                        {
                            string _item = _lt.Tus_itm_cd;
                            Int32 _serialID = _lt.Tus_ser_id;
                            MasterItem _items = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
                            if (_items.Mi_is_ser1 != -1)
                            {
                                var _match = (from _lsst in _popUpList
                                              where _lsst.Tus_ser_id == Convert.ToInt32(_serialID)
                                              select _lsst);
                                if (_match != null)
                                    foreach (ReptPickSerials _one in _match)
                                    {
                                        if (_selectedItemList != null)
                                            if (_selectedItemList.Count > 0)
                                            {
                                                var _duplicate = from _lssst in _selectedItemList
                                                                 where _lssst.Tus_ser_id == _one.Tus_ser_id
                                                                 select _lssst;

                                                if (_duplicate.Count() <= 0)
                                                {
                                                    _selectedItemList.Add(_one);
                                                }
                                            }
                                            else
                                            {
                                                _selectedItemList.Add(_one);
                                            }
                                    }
                            }
                            else
                            {

                                var _match = (from _lsst in _popUpList
                                              where _lsst.Tus_itm_cd == _item
                                              select _lsst);
                                if (_match != null)
                                    foreach (ReptPickSerials _one in _match)
                                    {
                                        if (_selectedItemList != null)
                                            if (_selectedItemList.Count > 0)
                                            {
                                                var _duplicate = from _lsst in _selectedItemList
                                                                 where _lsst.Tus_itm_cd == _one.Tus_itm_cd
                                                                 select _lsst;
                                                if (_duplicate.Count() <= 0)
                                                {
                                                    _selectedItemList.Add(_one);
                                                }

                                            }
                                            else
                                            {
                                                _selectedItemList.Add(_one);
                                            }
                                    }
                            }

                        }

                    }
                BindSelectedItems(_selectedItemList);
            }
            else if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == true)
            {
                if (_sad_qty > 1)
                {

                    var _duplicate = from _lsst in _selectedItemList
                                     where _lsst.Tus_itm_cd == _sad_itm_cd && _lsst.Tus_itm_stus == _status && _lsst.Tus_unit_price == _sad_unit_rt
                                     select _lsst;
                    if (_duplicate.Count() <= 0)
                    {

                        string Msg = "<script>var name = prompt('Please enter your name', " + _sad_qty + ");if (name!=null && name!='') { document.getElementById('" + hdnUserQty.ClientID + "').value=name; } else { document.getElementById('<%=hdnUserQty.ClientID%>').value='-1';}  </script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                        if (hdnUserQty.Value == "-1" || string.IsNullOrEmpty(hdnUserQty.Value.ToString())) return;

                        ReptPickSerials _one = new ReptPickSerials();
                        _one.Tus_base_doc_no = _invoice;
                        _one.Tus_base_itm_line = _sad_itm_line;
                        _one.Tus_bin = string.Empty;
                        _one.Tus_com = GlbUserComCode;
                        _one.Tus_cre_by = GlbUserName;
                        _one.Tus_cre_dt = DateTime.Now.Date;
                        _one.Tus_cross_batchline = -1;
                        _one.Tus_cross_itemline = -1;
                        _one.Tus_cross_seqno = -1;
                        _one.Tus_cross_serline = -1;
                        _one.Tus_doc_dt = DateTime.Now.Date;
                        _one.Tus_doc_no = _invoice;
                        _one.Tus_exist_grncom = string.Empty;
                        _one.Tus_exist_grndt = DateTime.Now.Date;
                        _one.Tus_exist_grnno = string.Empty;
                        _one.Tus_exist_supp = string.Empty;
                        _one.Tus_itm_brand = string.Empty;
                        _one.Tus_itm_cd = _sad_itm_cd;
                        _one.Tus_itm_desc = _itms.Mi_longdesc;
                        _one.Tus_itm_line = -1;
                        _one.Tus_itm_model = _itms.Mi_model;
                        _one.Tus_itm_stus = _status;
                        _one.Tus_loc = GlbUserDefLoca;
                        _one.Tus_new_remarks = string.Empty;
                        _one.Tus_new_status = string.Empty;
                        _one.Tus_orig_grncom = string.Empty;
                        _one.Tus_orig_grndt = DateTime.Now.Date;
                        _one.Tus_orig_grnno = string.Empty;
                        _one.Tus_orig_supp = string.Empty;
                        _one.Tus_out_date = DateTime.Now.Date;
                        _one.Tus_qty = Convert.ToDecimal(hdnUserQty.Value);
                        _one.Tus_seq_no = -1;
                        _one.Tus_ser_1 = string.Empty;
                        _one.Tus_ser_2 = string.Empty;
                        _one.Tus_ser_3 = string.Empty;
                        _one.Tus_ser_4 = string.Empty;
                        _one.Tus_ser_id = -1;
                        _one.Tus_ser_line = -1;
                        _one.Tus_serial_id = string.Empty;
                        _one.Tus_session_id = GlbUserSessionID;
                        _one.Tus_unit_cost = 0;
                        _one.Tus_unit_price = _sad_unit_rt;
                        _one.Tus_usrseq_no = -1;
                        _one.Tus_warr_no = string.Empty;
                        _one.Tus_warr_period = 0;

                        _selectedItemList.Add(_one);

                    }

                }
                else
                {

                    var _duplicate = from _lsst in _selectedItemList
                                     where _lsst.Tus_itm_cd == _sad_itm_cd && _lsst.Tus_itm_stus == _status && _lsst.Tus_unit_price == _sad_unit_rt
                                     select _lsst;

                    if (_duplicate.Count() <= 0)
                    {

                        ReptPickSerials _one = new ReptPickSerials();
                        _one.Tus_base_doc_no = _invoice;
                        _one.Tus_base_itm_line = _sad_itm_line;
                        _one.Tus_bin = string.Empty;
                        _one.Tus_com = GlbUserComCode;
                        _one.Tus_cre_by = GlbUserName;
                        _one.Tus_cre_dt = DateTime.Now.Date;
                        _one.Tus_cross_batchline = -1;
                        _one.Tus_cross_itemline = -1;
                        _one.Tus_cross_seqno = -1;
                        _one.Tus_cross_serline = -1;
                        _one.Tus_doc_dt = DateTime.Now.Date;
                        _one.Tus_doc_no = _invoice;
                        _one.Tus_exist_grncom = string.Empty;
                        _one.Tus_exist_grndt = DateTime.Now.Date;
                        _one.Tus_exist_grnno = string.Empty;
                        _one.Tus_exist_supp = string.Empty;
                        _one.Tus_itm_brand = string.Empty;
                        _one.Tus_itm_cd = _sad_itm_cd;
                        _one.Tus_itm_desc = _itms.Mi_longdesc;
                        _one.Tus_itm_line = -1;
                        _one.Tus_itm_model = _itms.Mi_model;
                        _one.Tus_itm_stus = _status;
                        _one.Tus_loc = GlbUserDefLoca;
                        _one.Tus_new_remarks = string.Empty;
                        _one.Tus_new_status = string.Empty;
                        _one.Tus_orig_grncom = string.Empty;
                        _one.Tus_orig_grndt = DateTime.Now.Date;
                        _one.Tus_orig_grnno = string.Empty;
                        _one.Tus_orig_supp = string.Empty;
                        _one.Tus_out_date = DateTime.Now.Date;
                        _one.Tus_qty = _sad_qty;
                        _one.Tus_seq_no = -1;
                        _one.Tus_ser_1 = string.Empty;
                        _one.Tus_ser_2 = string.Empty;
                        _one.Tus_ser_3 = string.Empty;
                        _one.Tus_ser_4 = string.Empty;
                        _one.Tus_ser_id = -1;
                        _one.Tus_ser_line = -1;
                        _one.Tus_serial_id = string.Empty;
                        _one.Tus_session_id = GlbUserSessionID;
                        _one.Tus_unit_cost = 0;
                        _one.Tus_unit_price = _sad_unit_rt;
                        _one.Tus_usrseq_no = -1;
                        _one.Tus_warr_no = string.Empty;
                        _one.Tus_warr_period = 0;

                        _selectedItemList.Add(_one);
                    }

                }
            }


        }
        protected bool MyFunction(string _ispick)
        {
            if (_ispick == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void ConfirmPopUpSerial_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gvPopSerial.Rows)
            {
                CheckBox _chk = (CheckBox)gvr.Cells[0].FindControl("chkPopSerSelect");

                if (_chk.Checked)
                {
                    HiddenField _serialId = (HiddenField)gvr.Cells[0].FindControl("hdnPopSerialID");
                    HiddenField _item = (HiddenField)gvr.Cells[0].FindControl("hdnPopItem");
                    MasterItem _items = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item.Value);
                    if (_items.Mi_is_ser1 != -1)
                    {
                        var _match = (from _lst in _popUpList
                                      where _lst.Tus_ser_id == Convert.ToInt32(_serialId.Value)
                                      select _lst);
                        if (_match != null)
                            foreach (ReptPickSerials _one in _match)
                            {
                                if (_selectedItemList != null)
                                    if (_selectedItemList.Count > 0)
                                    {
                                        var _duplicate = from _lst in _selectedItemList
                                                         where _lst.Tus_ser_id == _one.Tus_ser_id
                                                         select _lst;

                                        if (_duplicate.Count() <= 0)
                                        {
                                            _selectedItemList.Add(_one);
                                        }
                                    }
                                    else
                                    {
                                        _selectedItemList.Add(_one);
                                    }
                            }
                    }
                    else
                    {
                        var _match = (from _lst in _popUpList
                                      where _lst.Tus_itm_cd == _item.Value.ToString()
                                      select _lst);
                        if (_match != null)
                            foreach (ReptPickSerials _one in _match)
                            {
                                if (_selectedItemList != null)
                                    if (_selectedItemList.Count > 0)
                                    {
                                        var _duplicate = from _lst in _selectedItemList
                                                         where _lst.Tus_itm_cd == _one.Tus_itm_cd
                                                         select _lst;
                                        if (_duplicate.Count() <= 0)
                                        {
                                            _selectedItemList.Add(_one);
                                        }

                                    }
                                    else
                                    {
                                        _selectedItemList.Add(_one);
                                    }
                            }
                    }

                }
            }
            BindSelectedItems(_selectedItemList);
            CalculateTotalNewandOldAmount();
        }
        protected void txtDocument_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtADocumentNo.Text)) return;

            if (ddlADocType.SelectedValue.ToString() == CommonUIDefiniton.ReturnRequestDocumentType.Cash.ToString())
            {
                BindAccountItem(txtADocumentNo.Text.Trim(), false);
            }

            if (ddlADocType.SelectedValue.ToString() == CommonUIDefiniton.ReturnRequestDocumentType.Hire.ToString())
            {
                BindAccountItem(txtADocumentNo.Text.Trim(), true);
                List<InvoiceHeader> _hdr = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, txtADocumentNo.Text);
                if (_hdr != null)
                    if (_hdr.Count > 0)
                    {
                        InvoiceItem _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_hdr[0].Sah_inv_no)[0];
                        txtPriceBook.Text = _invItem.Sad_pbook;
                        txtPriceLevel.Text = _invItem.Sad_pb_lvl;
                        SSPriceBook = _invItem.Sad_pbook;
                        SSPriceLevel = _invItem.Sad_pb_lvl;
                    }
            }

            if (ddlADocType.SelectedValue.ToString() == CommonUIDefiniton.ReturnRequestDocumentType.Request.ToString())
            {
                BindAccountItem(txtADocumentNo.Text.Trim(), false);
                BindItemsFromRequest(txtADocumentNo.Text.Trim());
            }



        }
        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            lblACode.Text = _hdr.Sah_cus_cd;
            lblAName.Text = _hdr.Sah_cus_name;
            lblAAddress1.Text = _hdr.Sah_cus_add1;
            lblAAddress2.Text = _hdr.Sah_cus_add2;
        }

        protected void SelectedItem_OnDelete(object sender, GridViewDeleteEventArgs e)
        {
            if (_selectedItemList == null) return;
            if (_selectedItemList.Count <= 0) return;

            int row_id = e.RowIndex;
            Int32 _serialID = (Int32)gvAReturnItem.DataKeys[row_id][0];
            string _item = (string)gvAReturnItem.DataKeys[row_id][1];

            MasterItem _itm = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
            if (_itm.Mi_is_ser1 != -1)
            {
                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                _selectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
            }
            else
            {
                _selectedItemList.RemoveAll(x => x.Tus_itm_cd == _item);
            }

            BindSelectedItems(_selectedItemList);

        }

        #region Invoicing Area Function

        #region  Session Variables

        public decimal SSPriceBookPrice
        {
            get { return Convert.ToDecimal(Session["pb_price"]); }
            set { Session["pb_price"] = value.ToString(); }
        }
        public string SSPriceBookSequance
        {
            get { return Session["pb_seq"].ToString(); }
            set { Session["pb_seq"] = value; }
        }
        public string SSPriceBookItemSequance
        {
            get { return Session["pb_itm_seq"].ToString(); }
            set { Session["pb_itm_seq"] = value; }
        }
        public string SSIsLevelSerialized
        {
            get { return Session["IsLevelSerialized"].ToString(); }
            set { Session["IsLevelSerialized"] = value; }
        }

        public string SSPromotionCode
        {
            get { return Convert.ToString(Session["PromotionCode"]); }
            set { Session["PromotionCode"] = value; }
        }
        public Int32 SSPRomotionType
        {
            get { return Convert.ToInt32(Session["PromotionType"]); }
            set { Session["PromotionType"] = value; }
        }
        public Int32 SSCombineLine
        {
            get { return Convert.ToInt32(Session["CombineLine"]); }
            set { Session["CombineLine"] = value; }
        }

        protected string SSPriceBook
        {
            get { return Session["PriceBook"].ToString(); }
            set { Session["PriceBook"] = value; }
        }

        protected string SSPriceLevel
        {
            get { return Session["PriceLevel"].ToString(); }
            set { Session["PriceLevel"] = value; }
        }


        #endregion
        protected PriceDefinitionRef _priceDefinitionRef
        {
            get { return (PriceDefinitionRef)ViewState["_priceDefinitionRef"]; }
            set { ViewState["_priceDefinitionRef"] = value; }
        }
        protected void CheckPriceBook(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPriceBook.Text.Trim())) return;

            if (string.IsNullOrEmpty(InvoiceType)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the invoice type"); return; }

            List<PriceDefinitionRef> _def = new List<PriceDefinitionRef>();
            _def = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(GlbUserComCode, txtPriceBook.Text.Trim(), string.Empty, InvoiceType, GlbUserDefProf);

            if (_def.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid price book");
                txtPriceBook.Text = string.Empty;
                txtPriceBook.Focus();
                return;
            }
            SSPriceBook = txtPriceBook.Text.Trim();
            GetPriceDetail(txtADocumentNo.Text.Trim());

        }
        protected void CheckPriceLevel(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPriceLevel.Text)) { return; }


            if (string.IsNullOrEmpty(txtPriceBook.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Price book not select. It is set to profit center default");
                txtPriceBook.Text = _masterProfitCenter.Mpc_def_pb;
            }


            List<PriceDefinitionRef> _def = new List<PriceDefinitionRef>();
            _def = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(GlbUserComCode, txtPriceBook.Text.Trim(), txtPriceLevel.Text.Trim(), InvoiceType, GlbUserDefProf);
            if (_def.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid price level");
                txtPriceLevel.Text = string.Empty;
                txtPriceLevel.Focus();
                return;
            }


            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, txtPriceBook.Text, txtPriceLevel.Text);
            if (_priceBookLevelRef != null)
            {
                if (_priceBookLevelRef.Sapl_is_serialized) SSIsLevelSerialized = "1";
                else SSIsLevelSerialized = "0";

                _priceDefinitionRef = new PriceDefinitionRef();
                _priceDefinitionRef = CHNLSVC.Sales.GetPriceDefinition(GlbUserComCode, GlbUserDefProf, InvoiceType, txtPriceBook.Text, txtPriceLevel.Text);
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid level");
                txtPriceLevel.Text = "";
                txtPriceLevel.Focus();
                return;

            }

            SSPriceLevel = txtPriceLevel.Text.Trim();

            GetPriceDetail(txtADocumentNo.Text.Trim());

        }

        protected List<PriceCombinedItemRef> _MainPriceCombinItem
        {
            get { return (List<PriceCombinedItemRef>)ViewState["_MainPriceCombinItem"]; }
            set { ViewState["_MainPriceCombinItem"] = value; }
        }
        protected PriceBookLevelRef _priceBookLevelRef
        {
            get { return (PriceBookLevelRef)ViewState["_priceBookLevelRef"]; }
            set { ViewState["_priceBookLevelRef"] = value; }
        }
        protected MasterProfitCenter _masterProfitCenter
        {
            get { return (MasterProfitCenter)ViewState["_masterProfitCenter"]; }
            set { ViewState["_masterProfitCenter"] = value; }
        }
        protected List<PriceSerialRef> _tempPriceSerial
        {
            get { return (List<PriceSerialRef>)ViewState["_tempPriceSerial"]; }
            set { ViewState["_tempPriceSerial"] = value; }
        }
        protected bool IsNoPriceDefinition
        {
            get { return Convert.ToBoolean(Session["NoPrice"]); }
            set { Session["NoPrice"] = value; }
        }

        private void SetDecimalTextBoxForZero(bool _isUnit)
        {
            txtOutQty.Text = "Qty";
            txtOutAmount.Text = "Amount";
            if (_isUnit) txtOutUnitRate.Text = "Unit Rate";
        }

        protected void BindSerializedPriceSerial(List<PriceSerialRef> _list)
        {
            gvPopSerialPricePick.DataSource = _list;
            gvPopSerialPricePick.DataBind();
        }
        protected List<PriceDetailRef> _priceDetailRef
        {
            get { return (List<PriceDetailRef>)ViewState["_priceDetailRef"]; }
            set { ViewState["_priceDetailRef"] = value; }
        }
        protected void BindPriceSerial(List<PriceDetailRef> _list)
        {
            gvPopPricePick.DataSource = _list;
            gvPopPricePick.DataBind();
        }
        protected PriceTypeRef TakePromotion(Int32 _priceType)
        {
            List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            var _ptype = from _types in _type
                         where _types.Sarpt_indi == _priceType
                         select _types;
            PriceTypeRef _list = new PriceTypeRef();
            foreach (PriceTypeRef _ones in _ptype)
            {
                _list = _ones;

            }
            return _list;
        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, bool _isTaxPotion)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxPotion == false) _taxs = CHNLSVC.Sales.GetTax(GlbUserComCode, txtOutItem.Text.Trim(), txtOutStatus.Text.Trim()); else _taxs = CHNLSVC.Sales.GetItemTax(GlbUserComCode, _item, _status, string.Empty, string.Empty);
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (_isTaxPotion == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                    }
                }
                else
                    if (_isTaxPotion) _pbUnitPrice = 0;

            return _pbUnitPrice;
        }
        protected List<PriceCombinedItemRef> _tempPriceCombinItem
        {
            get { return (List<PriceCombinedItemRef>)ViewState["_tempPriceCombinItem"]; }
            set { ViewState["_tempPriceCombinItem"] = value; }
        }
        protected bool _isEditPrice
        {
            get { return (bool)ViewState["_isEditPrice"]; }
            set { ViewState["_isEditPrice"] = value; }
        }
        protected bool _isEditDiscount
        {
            get { return (bool)ViewState["_isEditDiscount"]; }
            set { ViewState["_isEditDiscount"] = value; }
        }
        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtOutQty.Text) && !string.IsNullOrEmpty(txtOutUnitRate.Text))
            {
                decimal _vatPortion = TaxCalculation(txtOutItem.Text.Trim(), txtOutStatus.Text.Trim(), Convert.ToDecimal(txtOutQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtOutUnitRate.Text.Trim()), true);
                decimal _totalAmount = Convert.ToDecimal(txtOutQty.Text) * Convert.ToDecimal(txtOutUnitRate.Text);
                decimal _disAmt = 0;

                _totalAmount = _totalAmount + _vatPortion - _disAmt;

                txtOutAmount.Text = FormatToCurrency(_totalAmount.ToString());
            }
        }

        protected List<PriceSerialRef> _MainPriceSerial
        {
            get { return (List<PriceSerialRef>)ViewState["_MainPriceSerial"]; }
            set { ViewState["_MainPriceSerial"] = value; }
        }
        protected List<PriceSerialRef> _tempPriceSerialItm
        {
            get { return (List<PriceSerialRef>)ViewState["_tempPriceSerialItm"]; }
            set { ViewState["_tempPriceSerialItm"] = value; }
        }

        protected void CheckQty(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOutItem.Text)) return;
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtOutQty.Text) || Convert.ToDecimal(txtOutQty.Text) <= 0) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; return; }



            if (string.IsNullOrEmpty(txtOutItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
                txtOutItem.Focus();
                return;
            }



            if (string.IsNullOrEmpty(txtOutStatus.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item status");
                txtOutStatus.Focus();
                return;
            }

            //Load Price Level Details
            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, SSPriceBook, SSPriceLevel);

            //Check for tax setup  - under darshana confirmation on 02/06/2012
            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(GlbUserComCode, txtOutItem.Text.Trim(), txtOutStatus.Text.Trim(), string.Empty, string.Empty);
            if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item does not have setup tax definition for the selected status. Please contact costing dept.");
                txtOutStatus.Focus();
                return;
            }

            bool _isMRP = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtOutItem.Text).Mi_anal3;

            if (_isMRP)
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Consumer products not allow here");
            }


            if (_priceBookLevelRef.Sapl_is_serialized)
            {
                //Serialized price level
                //User directing for select the serial from pick the serials,
                //It should fire after enter item code, and without enter qty.
                //After selecting serial, the selected serials will goes to DO grid and the items will add to the sales entry end.

                //The event should be performed in lostforcus of the item as same at the "Add Item"  button
                List<PriceSerialRef> _list = CHNLSVC.Sales.GetAllPriceSerial(SSPriceBook, SSPriceLevel, txtOutItem.Text, Convert.ToDateTime(txtDate.Text), string.Empty,GlbUserComCode,GlbUserDefProf);
                _tempPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerial = _list;
                if (_list.Count < Convert.ToDecimal(txtOutQty.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected qty is exceeds available serials!");
                    txtOutQty.Text = "Qty";
                    txtOutQty.Focus();
                    IsNoPriceDefinition = true;
                    return;
                }


                if (_list.Count > 0)
                {
                    lblPopSerialQty.Text = txtOutQty.Text;
                    BindSerializedPriceSerial(_list);
                    divPopSerialPriceList.Visible = true;
                    divPopSerialPriceList.Disabled = false;
                    divPopPriceItemCombination.Visible = false;
                    IsNoPriceDefinition = false;
                    MPEPopup.Show();
                    return;
                }
                return;
            }

            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice(GlbUserComCode, GlbUserDefProf, InvoiceType, SSPriceBook, SSPriceLevel, string.Empty, txtOutItem.Text, Convert.ToDecimal(txtOutQty.Text), Convert.ToDateTime(txtDate.Text));

            if (_priceDetailRef.Count <= 0)
            {
                //Msg for no price define
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no price for the selected item");
                IsNoPriceDefinition = true;
                return;
            }
            else
            {
                if (_priceDetailRef.Count > 1)
                {
                    //Find More than one price for the selected item
                    //Load price prices for the grid and popup for user confirmation
                    lblPopNonSerialQty.Text = txtOutQty.Text;

                    divPopPriceList.Visible = true;
                    divPopPriceList.Disabled = false;
                    divPopPriceItemCombination.Visible = false;
                    IsNoPriceDefinition = false;
                    MPEPopup.Show();
                    BindPriceSerial(_priceDetailRef);
                    return;
                }
                else if (_priceDetailRef.Count == 1)
                {

                    var _one = from _itm in _priceDetailRef
                               select _itm;
                    int _priceType = 0;
                    foreach (var _single in _one)
                    {
                        _priceType = _single.Sapd_price_type;

                        PriceTypeRef _promotion = TakePromotion(_priceType);

                        //Tax Calculation
                        decimal UnitPrice = TaxCalculation(txtOutItem.Text.Trim(), txtOutStatus.Text.Trim(), Convert.ToDecimal(txtOutQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, false);

                        txtOutUnitRate.Text = FormatToCurrency(UnitPrice.ToString());
                        SSPriceBookPrice = UnitPrice;
                        SSPriceBookSequance = _single.Sapd_pb_seq.ToString();
                        SSPriceBookItemSequance = _single.Sapd_seq_no.ToString();

                        Int32 _pbSq = _single.Sapd_pb_seq;
                        string _mItem = _single.Sapd_itm_cd;

                        //If Promotion Available
                        if (_promotion.Sarpt_is_com)
                        {
                            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                            _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbSq, _mItem, string.Empty);
                            if (_tempPriceCombinItem != null)
                            {
                                gvPriceItemCombine.DataSource = _tempPriceCombinItem;
                                gvPriceItemCombine.DataBind();
                                divPopPriceItemCombination.Visible = true;
                                MPEPopup.Show();
                                return;
                            }
                            else
                            {
                                divPopPriceItemCombination.Visible = false;
                                lblMsg.Text = "There is no such combine items pick";
                                MPEPopup.Show();
                                return;
                            }
                        }
                        else
                        {
                            txtOutUnitRate.Focus();
                        }

                    }

                }

            }

            _isEditPrice = false;
            _isEditDiscount = false;

            if (string.IsNullOrEmpty(txtOutQty.Text)) txtOutQty.Text = "Qty";

            txtOutQty.Text = FormatToQty(txtOutQty.Text);
            CalculateItem();
        }

        protected void btnPopConfirm_Click(object sender, EventArgs e)
        {

            #region Confirmation Serialized Price Level Price

            if (divPopSerialPriceList.Visible && divPopSerialPriceList.Disabled == false)
            {
                Int32 _counter = 0;
                if (_MainPriceSerial == null) _MainPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerialItm = new List<PriceSerialRef>();

                foreach (GridViewRow row in gvPopSerialPricePick.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox _chk = row.FindControl("chkPopPricePick") as CheckBox;
                        string _serial = row.Cells[3].Text;

                        if (_chk.Checked)
                        {
                            _counter++;

                            var _obj = from _list in _tempPriceSerial
                                       where _list.Sars_ser_no == _serial && _list.Sars_itm_cd == txtOutItem.Text
                                       select _list;
                            if (_obj != null)
                            {
                                foreach (PriceSerialRef _one in _obj)
                                {
                                    _tempPriceSerialItm.Add(_one);
                                }
                            }
                        }
                    }
                }

                if (_counter != Convert.ToDecimal(txtOutQty.Text))
                {
                    lblMsg.Text = "Select serial and qty mismatch!";
                    MPEPopup.Show();
                    return;
                }


                string _item = txtOutItem.Text;
                string _status = txtOutStatus.Text;
                string _duplicateSerials = "";

                foreach (PriceSerialRef _one in _tempPriceSerialItm)
                {

                    txtOutItem.Text = _item;
                    txtOutStatus.Text = _status;
                    txtOutQty.Text = "1.00";
                    txtOutUnitRate.Text = FormatToCurrency(_one.Sars_itm_price.ToString());

                    var _duplicate = from _dup in _MainPriceSerial
                                     where _dup.Sars_pb_seq == _one.Sars_pb_seq && _dup.Sars_pbook == _one.Sars_pbook && _dup.Sars_price_lvl == _one.Sars_price_lvl && _dup.Sars_itm_cd == _one.Sars_itm_cd && _dup.Sars_ser_no == _one.Sars_ser_no
                                     select _dup;

                    if (_duplicate.Count() <= 0)
                    {
                        _MainPriceSerial.Add(_one);

                        SSPriceBookPrice = _one.Sars_itm_price;
                        SSPriceBookSequance = _one.Sars_pb_seq.ToString();
                        SSPriceBookItemSequance = "1"; //TODO : Table does not having item line no

                        txtOutUnitRate.Focus();



                        txtOutAmount.Focus();
                        CalculateItem();
                        AddItem(true);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_duplicateSerials))
                            _duplicateSerials = _one.Sars_ser_no;
                        else
                            _duplicateSerials += ", " + _one.Sars_ser_no;

                    }

                }

                if (!string.IsNullOrEmpty(_duplicateSerials))
                {
                    lblMsg.Text = "Duplicate serial found - " + _duplicateSerials;
                    MPEPopup.Show();
                    return;
                }

                divPopSerialPriceList.Visible = false;
                divPopPriceItemCombination.Visible = false;
                txtOutUnitRate.Focus();

            }
            #endregion

            #region  Confirmation Serialized Price Promotion Item

            if (divPopSerialPriceList.Visible && divPopSerialPriceList.Disabled == true && divPopPriceItemCombination.Visible)
            {
                if (_tempPriceCombinItem != null)
                {
                    if (_MainPriceCombinItem == null) _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                    foreach (PriceCombinedItemRef _item in _tempPriceCombinItem)
                    {
                        _MainPriceCombinItem.Add(_item);
                    }
                }

                divPopSerialPriceList.Disabled = false;
                divPopPriceItemCombination.Visible = false;
                MPEPopup.Show();
                return;
            }

            #endregion

            #region Confirmation Non-Serialized Price Level Price
            if (divPopPriceList.Visible && divPopPriceList.Disabled == true && divPopPriceItemCombination.Visible || divPopPriceItemCombination.Visible)
            {
                txtOutUnitRate.Text = FormatToCurrency(SSPriceBookPrice.ToString());
                if (_tempPriceCombinItem != null)
                {
                    if (_MainPriceCombinItem == null) _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                    string _duplicateSerials = "";
                    foreach (PriceCombinedItemRef _item in _tempPriceCombinItem)
                    {
                        var _duplicate = from _list in _MainPriceCombinItem
                                         where _list.Sapc_main_itm_cd == _item.Sapc_main_itm_cd && _list.Sapc_itm_cd == _item.Sapc_itm_cd && _list.Sapc_pb_seq == _item.Sapc_pb_seq && _list.Sapc_price == _item.Sapc_price
                                         select _list;

                        if (_duplicate.Count() <= 0)
                        {
                            _MainPriceCombinItem.Add(_item);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(_duplicateSerials))
                                _duplicateSerials = _item.Sapc_itm_cd;
                            else
                                _duplicateSerials += ", " + _item.Sapc_itm_cd;
                        }
                    }

                    if (!string.IsNullOrEmpty(_duplicateSerials))
                    {
                        lblMsg.Text = "Duplicate serial found - " + _duplicateSerials;
                        MPEPopup.Show();
                        return;
                    }

                    divPopPriceList.Visible = false;
                    divPopPriceItemCombination.Visible = false;
                    txtOutUnitRate.Focus();
                }
            }
            #endregion

        }

        protected List<InvoiceItem> _invoiceItemList
        {
            get { return (List<InvoiceItem>)ViewState["_invoiceItemList"]; }
            set { ViewState["_invoiceItemList"] = value; }
        }
        protected Int32 _lineNo
        {
            get { return (Int32)ViewState["_lineNo"]; }
            set { ViewState["_lineNo"] = value; }
        }
        private bool _isCombineAdding = false;
        private InvoiceItem AssignDataToObject(bool _isPromotion)
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            InvoiceItem _tempItem = new InvoiceItem();
            _tempItem.Sad_alt_itm_cd = "";
            _tempItem.Sad_alt_itm_desc = "";
            _tempItem.Sad_comm_amt = 0;
            _tempItem.Sad_disc_amt = 0;
            _tempItem.Sad_disc_rt = 0;
            _tempItem.Sad_do_qty = 0;
            _tempItem.Sad_fws_ignore_qty = 0;
            _tempItem.Sad_inv_no = "";
            _tempItem.Sad_is_promo = _isPromotion;
            _tempItem.Sad_itm_cd = txtOutItem.Text;
            _tempItem.Sad_itm_line = _lineNo;
            _tempItem.Sad_itm_seq = 0;//Convert.ToInt32(SSPriceBookItemSequance);
            _tempItem.Sad_itm_stus = txtOutStatus.Text;
            //TODO: Vat Amount
            _tempItem.Sad_itm_tax_amt = 0;
            _tempItem.Sad_itm_tp = "";
            _tempItem.Sad_job_no = "";
            _tempItem.Sad_merge_itm = "";
            _tempItem.Sad_pb_lvl = SSPriceLevel;
            _tempItem.Sad_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            _tempItem.Sad_pbook = SSPriceBook;
            _tempItem.Sad_print_stus = false;
            _tempItem.Sad_promo_cd = SSPromotionCode;
            _tempItem.Sad_qty = Convert.ToDecimal(txtOutQty.Text);
            _tempItem.Sad_res_line_no = 0;
            _tempItem.Sad_res_no = "";
            _tempItem.Sad_seq = Convert.ToInt32(SSPriceBookSequance);
            _tempItem.Sad_seq_no = 0;
            _tempItem.Sad_srn_qty = 0;
            _tempItem.Sad_tot_amt = Convert.ToDecimal(txtOutAmount.Text);
            _tempItem.Sad_unit_amt = Convert.ToDecimal(txtOutUnitRate.Text) * Convert.ToDecimal(txtOutQty.Text);
            _tempItem.Sad_unit_rt = Convert.ToDecimal(txtOutUnitRate.Text);
            _tempItem.Sad_uom = "";
            _tempItem.Sad_warr_based = false;
            _tempItem.Sad_warr_period = 0;
            _tempItem.Sad_warr_remarks = "";
            //TODO: description
            _tempItem.Mi_longdesc = "";
            _tempItem.Sad_job_line = Convert.ToInt32(SSCombineLine);
            return _tempItem;
        }
        private int _combineCounter = 0;
        private void ClearAfterAddItem()
        {
            txtOutItem.Text = "Item";
            txtOutStatus.Text = "Status";
            txtOutQty.Text = "Qty";
            txtOutUnitRate.Text = "Unit Rate";
            txtOutAmount.Text = "Amount";

        }
        protected void BindAddItem()
        {
            //if (_invoiceItemList.Count > 0)
            //{
            gvExchangeOutItm.DataSource = _invoiceItemList;
            gvExchangeOutItm.DataBind();
            //}
        }

        private void AddItem(bool _isPromotion)
        {

            if (SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid price"); return; }
            if (string.IsNullOrEmpty(txtOutQty.Text.Trim())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid qty"); return; }
            if (Convert.ToDecimal(txtOutQty.Text) == 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid qty"); return; }
            if (Convert.ToDecimal(txtOutQty.Text.Trim()) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid qty"); return; }

            if (string.IsNullOrEmpty(txtOutUnitRate.Text.Trim())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid unit price"); return; }


            if (string.IsNullOrEmpty(txtOutItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
                txtOutItem.Focus();
                return;
            }



            if (string.IsNullOrEmpty(txtOutStatus.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item status");
                txtOutStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtOutQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the qty");
                txtOutQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtOutUnitRate.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the unit price");
                txtOutUnitRate.Focus();
                return;
            }



            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(GlbUserComCode, txtOutItem.Text.Trim(), txtOutStatus.Text.Trim(), string.Empty, string.Empty);
            if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item does not have setup tax definition for the selected status. Please contact costing dept.");
                txtOutStatus.Focus();
                return;
            }


            CalculateItem();



            if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
            //No Records
            {
                _lineNo += 1;
                if (!_isCombineAdding) SSCombineLine = _lineNo;
                _invoiceItemList.Add(AssignDataToObject(_isPromotion));
            }
            else
            //Having some records
            {
                var _similerItem = from _list in _invoiceItemList
                                   where _list.Sad_itm_cd == txtOutItem.Text && _list.Sad_itm_stus == txtOutStatus.Text && _list.Sad_pbook == SSPriceBook && _list.Sad_pb_lvl == SSPriceLevel && _list.Sad_unit_rt == Convert.ToDecimal(txtOutUnitRate.Text)
                                   select _list;

                if (_similerItem.Count() > 0)
                //Similer item available
                {
                    foreach (var _similerList in _similerItem)
                    {
                        _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(0);//txtDiscountAmt.Text
                        _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(0);//txtVATAmt.Text
                        _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtOutQty.Text);
                        _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtOutAmount.Text);

                    }
                }
                else
                //No similer item found
                {
                    _lineNo += 1;
                    if (!_isCombineAdding) SSCombineLine = _lineNo;
                    _invoiceItemList.Add(AssignDataToObject(_isPromotion));
                }

            }

            //TODO: VAT amount
            // CalculateGrandTotal(Convert.ToDecimal(txtOutQty.Text), Convert.ToDecimal(txtOutUnitRate.Text), 0, 0, true);


            if (_MainPriceCombinItem != null)
            {
                string _combineStatus = string.Empty;
                decimal _combineQty = 0;

                if (_MainPriceCombinItem.Count > 0 && _isCombineAdding == false)
                {
                    _isCombineAdding = true;
                    if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = txtOutStatus.Text;
                    if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtOutQty.Text);

                    foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                    {
                        txtOutItem.Text = _list.Sapc_itm_cd;
                        // txtDescription.Text = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text).Mi_longdesc;
                        txtOutStatus.Text = _combineStatus;
                        txtOutUnitRate.Text = _list.Sapc_price.ToString();
                        txtOutQty.Text = (_list.Sapc_qty * _combineQty).ToString();
                        txtOutAmount.Text = "";
                        CalculateItem();
                        AddItem(_isPromotion);
                        _combineCounter += 1;


                    }
                }

                if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); SSCombineLine = 0; }
            }
            //TODO: Check for delivery now! tag

            ClearAfterAddItem();
            txtOutItem.Focus();
            BindAddItem();
            SetDecimalTextBoxForZero(true);


        }

        public bool IsFixQty
        {
            get { return Convert.ToBoolean(Session["IsFixQty"]); }
            set { Session["IsFixQty"] = value; }
        }
        //When Select the checkbox of the combine items
        protected void CheckPopPriceListClick(object sender, EventArgs e)
        {
            CheckBox chkBx = sender as CheckBox;

            GridViewRow row = chkBx.NamingContainer as GridViewRow;
            HiddenField _priceTp = row.FindControl("hdnPriceType") as HiddenField;
            HiddenField _priceSeq = row.FindControl("hdnPbSeq") as HiddenField;
            HiddenField _mainItem = row.FindControl("hdnMainItem") as HiddenField;
            HiddenField _isFxQty = row.FindControl("hdnIsFixQty") as HiddenField;
            string _mainSerial = row.Cells[3].Text;

            Int32 _priceType = Convert.ToInt32(_priceTp.Value.ToString());
            Int32 _pbSq = Convert.ToInt32(_priceSeq.Value.ToString());
            string _mItem = Convert.ToString(_mainItem.Value.ToString());
            IsFixQty = Convert.ToBoolean(_isFxQty.Value);

            PriceTypeRef _list = TakePromotion(_priceType);
            SSPRomotionType = _priceType;
            SSPromotionCode = row.Cells[1].Text;

            if (chkBx.Checked)
            {

                if (_list.Sarpt_is_com)
                {
                    _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                    _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbSq, _mItem, _mainSerial);
                    if (_tempPriceCombinItem != null)
                    {
                        gvPriceItemCombine.DataSource = _tempPriceCombinItem;
                        gvPriceItemCombine.DataBind();
                        divPopSerialPriceList.Disabled = true;
                        divPopPriceItemCombination.Visible = true;
                        MPEPopup.Show();
                        return;
                    }
                    else
                    {
                        divPopSerialPriceList.Disabled = false;
                        divPopPriceItemCombination.Visible = false;
                        lblMsg.Text = "There is no such combine items pick";
                        MPEPopup.Show();
                        return;
                    }

                }

                divPopSerialPriceList.Disabled = false;
                divPopPriceItemCombination.Visible = false;
                MPEPopup.Show();
                return;

            }
            else
            {
                MPEPopup.Show();
                return;
            }

        }

        //PriceList -Non Serial Binding
        static int _counts = 0;
        protected void gvPopPricePick_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (divPopPriceList.Visible)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Attributes.Add("ID", "tab" + _counts.ToString());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                                    gvPopPricePick,
                                    String.Concat("Select$", e.Row.RowIndex),
                                    true);

                        _counts += 1;

                        decimal UnitPrice = TaxCalculation(txtOutItem.Text.Trim(), txtOutStatus.Text.Trim(), Convert.ToDecimal(txtOutQty.Text), _priceBookLevelRef, Convert.ToDecimal(e.Row.Cells[2].Text.Trim()), false);
                        e.Row.Cells[2].Text = FormatToCurrency(UnitPrice.ToString());
                    }
                }
            }
        }
        protected void LoadNonSerializedCombination(object sender, EventArgs e)
        {

            string _item = Convert.ToString(gvPopPricePick.SelectedDataKey[0]); //Item
            int _pbseq = Convert.ToInt32(gvPopPricePick.SelectedDataKey[1]);//Pb Seq No
            IsFixQty = Convert.ToBoolean(gvPopPricePick.SelectedDataKey[2]);//Is Fix Qty
            Int32 _priceType = Convert.ToInt32(gvPopPricePick.SelectedDataKey[3]);//Price Type - combine/Free/Normal
            decimal _unitPrice = Convert.ToDecimal(gvPopPricePick.SelectedDataKey[4]);//Price 
            Int32 _itmLine = Convert.ToInt32(gvPopPricePick.SelectedDataKey[5]);//item line no
            _unitPrice = TaxCalculation(txtOutItem.Text.Trim(), txtOutStatus.Text.Trim(), Convert.ToDecimal(txtOutQty.Text), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), false);
            String _promoCD = Convert.ToString(gvPopPricePick.SelectedDataKey[6]);

            PriceTypeRef _list = TakePromotion(_priceType);
            if (_list.Sarpt_is_com)
            {
                _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbseq, _item, string.Empty);
                if (_tempPriceCombinItem != null)
                {
                    gvPriceItemCombine.DataSource = _tempPriceCombinItem;
                    gvPriceItemCombine.DataBind();
                    divPopPriceList.Disabled = true;
                    divPopPriceItemCombination.Visible = true;

                    SSPriceBookPrice = _unitPrice;
                    SSPriceBookSequance = _pbseq.ToString();
                    SSPriceBookItemSequance = _itmLine.ToString();
                    SSPromotionCode = _promoCD;
                    SSPRomotionType = _priceType;


                    MPEPopup.Show();
                    return;
                }
                else
                {
                    divPopPriceList.Disabled = false;
                    divPopPriceItemCombination.Visible = false;
                    lblMsg.Text = "There is no such combine items pick";
                    MPEPopup.Show();
                    return;
                }
            }

            SSPriceBookPrice = _unitPrice;
            SSPriceBookSequance = _pbseq.ToString();
            SSPriceBookItemSequance = _itmLine.ToString();
            SSPromotionCode = _promoCD;
            SSPRomotionType = _priceType;

            txtOutUnitRate.Text = FormatToCurrency(_unitPrice.ToString());
            txtOutUnitRate.Focus();
            return;

        }

        protected void gvPopConsumPricePick_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (divConsumPricePick.Visible)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                                    gvPopConsumPricePick,
                                    String.Concat("Select$", e.Row.RowIndex),
                                    true);

                        _count += 1;
                    }

                }
            }
        }
        protected void LoadConsumablePriceList(object sender, EventArgs e)
        {
            decimal _unitPrice = Convert.ToDecimal(gvPopConsumPricePick.SelectedDataKey[0]); //Item
            txtOutUnitRate.Text = FormatToCurrency(Convert.ToDecimal(_unitPrice).ToString());
            divConsumPricePick.Visible = false;
            txtOutUnitRate.Focus();

        }
        protected void btnPopCancel_Click(object sender, EventArgs e)
        {
            if (divPopSerialPriceList.Visible)
            {
                divPopSerialPriceList.Disabled = false;
                divPopPriceItemCombination.Visible = false;
                MPEPopup.Show();
                return;
            }

            if (divPopPriceList.Visible || (divPopPriceItemCombination.Visible && divPopPriceList.Visible == false))
            {
                //ATN: Process
                //If user wont confirm the combine items, it will pick the price from selected list and forcus to the unit price txt box
                //if user confirm, then add the combnie item to the temp list and forcus the price to txt box

                txtOutUnitRate.Text = FormatToCurrency(SSPriceBookPrice.ToString());
                divPopPriceList.Disabled = false;
                divPopPriceItemCombination.Visible = false;
                txtOutUnitRate.Focus();
            }
        }

        //Combine Item Binding
        protected void gvPriceItemCombine_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox _txt = (TextBox)e.Row.FindControl("txtPopPriceItmComSelQty");
                if (IsFixQty)
                    _txt.Enabled = false;
                else
                {
                    _txt.Enabled = true;
                    //check for the user selection
                    _txt.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPopUpCombineItemQty, ""));
                }

                string _item = e.Row.Cells[0].Text.Trim();
                decimal _qty = Convert.ToDecimal(_txt.Text);

                decimal UnitPrice = TaxCalculation(_item, txtOutStatus.Text.Trim(), Convert.ToDecimal(txtOutQty.Text), _priceBookLevelRef, Convert.ToDecimal(e.Row.Cells[3].Text.Trim()), false);
                e.Row.Cells[3].Text = FormatToCurrency(UnitPrice.ToString());
                if (!divPopSerialPriceList.Visible)
                {
                    e.Row.Cells[5].Text = FormatToCurrency((_qty * Convert.ToDecimal(txtOutQty.Text)).ToString());
                    e.Row.Cells[4].Text = e.Row.Cells[5].Text;
                }
            }
        }

        //If the promotion is not fix item, user will select the qty as per the pointing system
        protected void CheckPopUpCombineItemQty(object sender, EventArgs e)
        {
            //Update temp object
            TextBox _txt = sender as TextBox;
            if (string.IsNullOrEmpty(_txt.Text)) { MPEPopup.Show(); return; }
            if (!IsNumeric(_txt.Text.Trim(), NumberStyles.Number)) { MPEPopup.Show(); return; }
            if (Convert.ToDecimal(_txt.Text) == 0) { MPEPopup.Show(); return; }

            decimal _assQty = Convert.ToDecimal(gvPriceItemCombine.SelectedDataKey[0].ToString());
            decimal _selQty = Convert.ToDecimal(_txt.Text.Trim());

            if (_assQty < _selQty) { lblMsg.Text = "Allocated Qty exceeds by selected qty!"; MPEPopup.Show(); return; }



            //TODO: need to show popup again in any circumstance
        }
        protected void AddItem(Object sender, EventArgs e)
        {
            AddItem(SSPromotionCode == "0" ? false : true);
            CalculateTotalNewandOldAmount();
        }

        protected void OnRemoveFromInvoiceItemGrid(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;
            Int32 _combineLine = Convert.ToInt32(gvExchangeOutItm.DataKeys[row_id][10]);

            if (_MainPriceSerial != null)
                if (_MainPriceSerial.Count > 0)
                {

                    //sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty
                    string _item = (string)gvExchangeOutItm.DataKeys[row_id][0];
                    decimal _uRate = (decimal)gvExchangeOutItm.DataKeys[row_id][2];
                    string _pbook = (string)gvExchangeOutItm.DataKeys[row_id][3];
                    string _level = (string)gvExchangeOutItm.DataKeys[row_id][4];

                    List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                    var _remove = from _list in _tempSerial
                                  where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                                  select _list;

                    foreach (PriceSerialRef _single in _remove)
                    {
                        _tempSerial.Remove(_single);
                    }

                    _MainPriceSerial = _tempSerial;
                }

            List<InvoiceItem> _tempList = _invoiceItemList;
            var _promo = (from _pro in _invoiceItemList
                          where _pro.Sad_job_line == _combineLine
                          select _pro).ToList();

            if (_promo.Count() > 0)
            {
                foreach (InvoiceItem code in _promo)
                {
                    //CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                    //_tempList.Remove(code);
                }
                _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
            }
            else
            {
                //CalculateGrandTotal(Convert.ToDecimal(gvExchangeOutItm.DataKeys[row_id][5]), (decimal)gvExchangeOutItm.DataKeys[row_id][2], (decimal)gvExchangeOutItm.DataKeys[row_id][6], (decimal)gvExchangeOutItm.DataKeys[row_id][7], false);
                _tempList.RemoveAt(row_id);
            }
            _invoiceItemList = _tempList;
            BindAddItem();
        }

        #endregion
        private void CalculateTotalNewandOldAmount()
        {
            decimal _inAmt = 0;
            decimal _outAmt = 0;
            if (_selectedItemList != null)
                if (_selectedItemList.Count > 0)
                {
                    var _sumReturn = (from _l in _selectedItemList
                                      select _l.Tus_unit_price * _l.Tus_qty).Sum();
                    _inAmt = _sumReturn;
                    lblOldAmount.Text = FormatToCurrency(_sumReturn.ToString());
                }
                else lblOldAmount.Text = "0.00";


            if (_invoiceItemList != null)
                if (_invoiceItemList.Count > 0)
                {
                    var _sumIssue = (from _l in _invoiceItemList
                                     select _l.Sad_unit_amt).Sum();
                    _outAmt = _sumIssue;
                    lblNewAmount.Text = FormatToCurrency(_sumIssue.ToString());
                }
                else lblNewAmount.Text = "0.00";

            lblDifference.Text = _outAmt - _inAmt <= 0 ? FormatToCurrency(((_outAmt - _inAmt) * -1).ToString()) : FormatToCurrency((_outAmt - _inAmt).ToString());

            if (_outAmt - _inAmt < 0)
            {
                ddlUsageType.SelectedValue = "Usage Charge";
                ddlUsageType.Enabled = false;
            }


        }
        private void GetPriceDetail(string _account)
        {

            List<InvoiceHeader> _hdr = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, _account);
            if (_hdr != null)
                if (_hdr.Count > 0 && ddlADocType.SelectedValue=="Hire")
                {
                    InvoiceItem _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_hdr[0].Sah_inv_no)[0];
                    if (_invItem.Sad_pbook != txtPriceBook.Text.Trim() && _invItem.Sad_pb_lvl != txtPriceLevel.Text.Trim()) IsPriceBookChanged = true;
                    else if (_invItem.Sad_pbook != txtPriceBook.Text.Trim()) IsPriceBookChanged = true;
                    else if (_invItem.Sad_pb_lvl != txtPriceLevel.Text.Trim()) IsPriceBookChanged = true;
                    else if (_invItem.Sad_pbook == txtPriceBook.Text.Trim() && _invItem.Sad_pb_lvl == txtPriceLevel.Text.Trim()) IsPriceBookChanged = false;
                }
        }
        protected void Process(object sender, EventArgs e)
        {
            if (_selectedItemList == null)
                if (_selectedItemList.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the return item");
                    return;
                }

            if (_invoiceItemList == null && (ddlADocType.SelectedValue.ToString() == "Hire" || ddlADocType.SelectedValue.ToString() == "Request"))
                if (_invoiceItemList.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the issue item");
                    return;
                }

            if (string.IsNullOrEmpty(txtUsageAmount.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the usage amount");
                return;
            }




            CommonUIDefiniton.HirePurchasModuleApprovalCode _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014;
            string _documentType = string.Empty;


            if (ddlADocType.SelectedValue.ToString() == "Cash")
            {
                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014;
                _documentType = "Cash";
            }

            if (ddlADocType.SelectedValue.ToString() == "Hire")
            {
                if (IsPriceBookChanged)
                {
                    RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;
                }
                else
                {
                    RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008;
                }
                _documentType = "Hire";
            }

            if (ddlADocType.SelectedValue.ToString() == "Request")
            {
                RequestApprovalHeader _hdr = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(GlbUserComCode, GlbUserDefProf, txtADocumentNo.Text.Trim());
                _documentType = _hdr.Grah_remaks;

                if (_documentType == "Cash")
                {
                    RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                    _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT014;
                }

                if (_documentType == "Hire")
                {
                    if (IsPriceBookChanged)
                    {
                        RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT013;
                    }
                    else
                    {
                        RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                        _approvalCode = CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008;
                    }
                }
            }

            GlbReqIsApprovalNeed = true;
            GlbReqUserPermissionLevel = 3;
            GlbReqIsFinalApprovalUser = true;
            GlbReqIsRequestGenerateUser = true;

            #region fill RequestApprovalHeader

            RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
            ra_hdr.Grah_app_by = GlbUserName;
            ra_hdr.Grah_app_dt = Convert.ToDateTime(txtDate.Text);
            ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
            ra_hdr.Grah_app_stus = "P";
            ra_hdr.Grah_app_tp = _approvalCode.ToString();
            ra_hdr.Grah_com = GlbUserComCode;
            ra_hdr.Grah_cre_by = GlbUserName;
            ra_hdr.Grah_cre_dt = Convert.ToDateTime(txtDate.Text);
            ra_hdr.Grah_fuc_cd = txtADocumentNo.Text;
            ra_hdr.Grah_loc = GlbUserDefProf;// GlbUserDefLoca;
            ra_hdr.Grah_mod_by = GlbUserName;
            ra_hdr.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);
            ra_hdr.Grah_oth_loc = GlbUserDefProf;
            ra_hdr.Grah_remaks = _documentType;

            if (ddlADocType.SelectedValue != "Request") ra_hdr.Grah_ref = txtADocumentNo.Text;
            else ra_hdr.Grah_ref = null;


            ra_hdr.Grah_remaks = "";

            #endregion

            #region fill List<RequestApprovalDetail> with Log
            List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
            List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();

            Int32 _count = 1;

            foreach (ReptPickSerials _in in _selectedItemList)
            {
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();

                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = _count;
                ra_det.Grad_req_param = _in.Tus_itm_cd; //Item

                ra_det.Grad_val1 = _in.Tus_qty; //Qty
                ra_det.Grad_val2 = _in.Tus_unit_price;//Unit Price
                ra_det.Grad_val3 = _in.Tus_base_itm_line;//invoice line
                ra_det.Grad_val4 = _in.Tus_ser_line; //serial line
                ra_det.Grad_val5 = _in.Tus_ser_id; //serial id

                ra_det.Grad_anal1 = txtADocumentNo.Text; //account no
                ra_det.Grad_anal2 = _in.Tus_base_doc_no;//invoice no
                ra_det.Grad_anal3 = _in.Tus_doc_no;//DO no
                ra_det.Grad_anal4 = _in.Tus_ser_1;//serial no
                ra_det.Grad_anal5 = "IN";

                ra_det_List.Add(ra_det);

                ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                ra_det_log.Grad_line = _count;
                ra_det_log.Grad_req_param = _in.Tus_itm_cd;//Item

                ra_det_log.Grad_val1 = _in.Tus_qty;//Qty
                ra_det_log.Grad_val2 = _in.Tus_unit_price;//Unit Price
                ra_det_log.Grad_val3 = _in.Tus_base_itm_line;//invoice line
                ra_det_log.Grad_val4 = _in.Tus_ser_line;//serial line
                ra_det_log.Grad_val5 = _in.Tus_ser_id;//serial id

                ra_det_log.Grad_anal1 = txtADocumentNo.Text;//account no
                ra_det_log.Grad_anal2 = _in.Tus_base_doc_no;//invoice no
                ra_det_log.Grad_anal3 = _in.Tus_doc_no;//DO no
                ra_det_log.Grad_anal4 = _in.Tus_ser_1;//serial no
                ra_det_log.Grad_anal5 = "IN";

                ra_det_log.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_det_log);
                _count += 1;
            }

            foreach (InvoiceItem _out in _invoiceItemList)
            {
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                RequestApprovalDetailLog ra_det_log = new RequestApprovalDetailLog();

                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = _count;

                ra_det.Grad_req_param = _out.Sad_itm_cd;//Item
                ra_det.Grad_val1 = _out.Sad_qty;//Qty
                ra_det.Grad_val2 = _out.Sad_unit_amt;//Unit Rate
                ra_det.Grad_val3 = _out.Sad_pb_price;//Price Book Price
                ra_det.Grad_val4 = _out.Sad_itm_tax_amt;//Tax amt
                ra_det.Grad_val5 = _out.Sad_seq;//PB SEQ

                ra_det.Grad_anal1 = txtADocumentNo.Text;//account no
                ra_det.Grad_anal2 = _out.Sad_itm_stus;//Item Status
                ra_det.Grad_anal3 = _out.Sad_promo_cd;//Promotion code
                ra_det.Grad_anal4 = _out.Sad_seq_no.ToString();//PB item Seq
                ra_det.Grad_anal5 = "OUT";

                ra_det_List.Add(ra_det);

                ra_det_log.Grad_ref = ra_hdr.Grah_ref;
                ra_det_log.Grad_line = _count;

                ra_det_log.Grad_req_param = _out.Sad_itm_cd;//Item
                ra_det_log.Grad_val1 = _out.Sad_qty;//Qty
                ra_det_log.Grad_val2 = _out.Sad_unit_amt;//Unit Rate
                ra_det_log.Grad_val3 = _out.Sad_pb_price;//Price Book Price
                ra_det_log.Grad_val4 = _out.Sad_itm_tax_amt;//Tax amt
                ra_det_log.Grad_val5 = _out.Sad_seq;//PB SEQ

                ra_det_log.Grad_anal1 = txtADocumentNo.Text;//account no
                ra_det_log.Grad_anal2 = _out.Sad_itm_stus;//Item Status
                ra_det_log.Grad_anal3 = _out.Sad_promo_cd;//Promotion code
                ra_det_log.Grad_anal4 = _out.Sad_seq_no.ToString();//PB item Seq
                ra_det_log.Grad_anal5 = "OUT";

                ra_det_log.Grad_lvl = GlbReqUserPermissionLevel;
                ra_detLog_List.Add(ra_det_log);

                _count += 1;
            }

            RequestApprovalDetail ra_det_one = new RequestApprovalDetail();
            ra_det_one.Grad_ref = ra_hdr.Grah_ref;
            ra_det_one.Grad_line = _count++;
            ra_det_one.Grad_anal5 = "AMT";
            ra_det_one.Grad_req_param = ddlUsageType.SelectedValue.ToString().ToUpper();
            ra_det_one.Grad_val1 = 1;
            ra_det_one.Grad_val2 = Convert.ToDecimal(txtUsageAmount.Text.Trim());
            ra_det_one.Grad_anal1 = txtADocumentNo.Text;
            ra_det_List.Add(ra_det_one);


            RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();
            ra_detLog.Grad_ref = ra_hdr.Grah_ref;
            ra_detLog.Grad_line = _count;
            ra_detLog.Grad_anal5 = "AMT";
            ra_detLog.Grad_req_param = ddlUsageType.SelectedValue.ToString().ToUpper();
            ra_detLog.Grad_val1 = 1;
            ra_detLog.Grad_val2 = Convert.ToDecimal(txtUsageAmount.Text.Trim());
            ra_detLog.Grad_anal1 = txtADocumentNo.Text;
            ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
            ra_detLog_List.Add(ra_detLog);


            #endregion

            #region fill RequestApprovalHeaderLog

            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
            ra_hdrLog.Grah_app_by = GlbUserName;
            ra_hdrLog.Grah_app_dt = Convert.ToDateTime(txtDate.Text);
            ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
            ra_hdrLog.Grah_app_stus = "P";
            ra_hdrLog.Grah_app_tp = _approvalCode.ToString();
            ra_hdrLog.Grah_com = GlbUserComCode;
            ra_hdrLog.Grah_cre_by = GlbUserName;
            ra_hdrLog.Grah_cre_dt = Convert.ToDateTime(txtDate.Text);
            ra_hdrLog.Grah_fuc_cd = txtADocumentNo.Text;
            ra_hdrLog.Grah_loc = GlbUserDefProf;

            ra_hdrLog.Grah_mod_by = GlbUserName;
            ra_hdrLog.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);

            ra_hdrLog.Grah_oth_loc = GlbUserDefProf;

            ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
            ra_hdrLog.Grah_remaks = _documentType;

            #endregion


            string referenceNo;
            Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);

            if (eff > 0)
            {
                string Msg = "<script>alert('Request sent!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                string Msg = "<script>alert('Request not sent!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }

        }

        protected void CastOff(object sender, EventArgs e)
        {
            if (ddlUsageType.SelectedValue.ToString() == "Cash") { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no request for castoff"); return; }
            if (ddlUsageType.SelectedValue.ToString() == "Hire") { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no request for castoff"); return; }

            if (string.IsNullOrEmpty(txtADocumentNo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the request no to castoff"); return; }

            RequestApprovalHeader _hdr = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(GlbUserComCode, GlbUserDefProf, txtADocumentNo.Text.Trim());

            _hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
            _hdr.Grah_app_stus = "C";
            _hdr.Grah_loc = GlbUserDefProf;
            _hdr.Grah_mod_by = GlbUserName;
            _hdr.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);

            Int32 _effect = CHNLSVC.General.UpdateApprovalStatus(_hdr);

            if (_effect > 0)
            {
                string Msg = "<script>alert('Request castoff!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                string Msg = "<script>alert('Request not castoff!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }


        }
        //checkin
    }
}