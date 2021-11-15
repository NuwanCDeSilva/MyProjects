using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using System.Globalization;
using System.Transactions;
using FF.WebERPClient.UserControls;

namespace FF.WebERPClient.Sales_Module
{
    public partial class SalesReversal : BasePage
    {
        protected static List<InvoiceHeader> _InvHeaderList = new List<InvoiceHeader>();
        protected static List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
        protected static MasterBusinessEntity _masterBusinessCompany = null;
        protected static InvoiceHeader _masterInvoiceHeader = null;

        protected List<InvoiceHeader> _invoicebyaccount
        {
            get { return (List<InvoiceHeader>)ViewState["_invoicebyaccount"]; }
            set { ViewState["_invoicebyaccount"] = value; }
        }

        protected List<ReptPickSerials> _doitemserials
        {
            get { return (List<ReptPickSerials>)ViewState["_doitemserials"]; }
            set { ViewState["_doitemserials"] = value; }
        }

        protected List<ReptPickSerialsSub> _doitemSubSerials
        {
            get { return (List<ReptPickSerialsSub>)ViewState["_doitemSubSerials"]; }
            set { ViewState["_doitemSubSerials"] = value; }
        }

        public HpAccount AccountsList
        {
            get { return (HpAccount)ViewState["AccountsList"]; }
            set { ViewState["AccountsList"] = value; }
        }

        protected static Int32 _itmLine = 0;
        protected static decimal _InvQty = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtfDate.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txttDate.ClientID + "').focus();return false;}} else {return true}; ");
                txttDate.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtCus.ClientID + "').focus();return false;}} else {return true}; ");


                txtCus.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCustomer, ""));
                txthCus.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCustomer, ""));
                txtInv.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInvoice, ""));
                txtHInv.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnHSales, ""));
                txtHAcc.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnHAcc, ""));

                txtRevQty.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.txtRevQty, ""));


                txtCus.Attributes.Add("onkeypress", "uppercase();");
                txtInv.Attributes.Add("onkeypress", "uppercase();");
                txthCus.Attributes.Add("onkeypress", "uppercase();");
                txtHInv.Attributes.Add("onkeypress", "uppercase();");
                txtHAcc.Attributes.Add("onkeypress", "uppercase();");

                String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
                if (!string.IsNullOrEmpty(_defBin))
                {
                    hdnAllowBin.Value = _defBin;
                }
                else
                {
                    hdnAllowBin.Value = "0";
                }

                _invoicebyaccount = new List<InvoiceHeader>();
                _doitemserials = new List<ReptPickSerials>();
                _doitemSubSerials = new List<ReptPickSerialsSub>();
                AccountsList = new HpAccount();
                this.Clear_Data();
            }
        }

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HireSalesInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "HS" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.HireSalesAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void Clear_Data()
        {
            txtCus.Text = "";
            txtInv.Text = "";
            txthCus.Text = "";
            txtHInv.Text = "";
            txtHAcc.Text = "";
            lblHAmt.Text = "0.00";
            lblHdis.Text = "0.00";
            lblHTax.Text = "0.00";
            lblHtot.Text = "0.00";
            txthfDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            txthtDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            txtfDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            txttDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());

            uc_HpAccountSummary1.Clear();
            uc_HpAccountDetail1.Clear();

            DataTable _Itemtable = new DataTable();
            gvInvoice.DataSource = _Itemtable;
            gvInvoice.DataBind();

            DataTable _Detailtable = new DataTable();
            gvInvItems.DataSource = _Detailtable;
            gvInvItems.DataBind();

            DataTable _HireSaletable = new DataTable();
            gvHInv.DataSource = _HireSaletable;
            gvHInv.DataBind();

            DataTable _HireItems = new DataTable();
            gvHItems.DataSource = _HireItems;
            gvHItems.DataBind();

            DataTable _DelDetails = new DataTable();
            gvDelDetails.DataSource = _DelDetails;
            gvDelDetails.DataBind();

            btnClear.Focus();

        }

        protected void imgcus_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCus.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void imgHAcc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HireSalesAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceByAcc(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtHAcc.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }


        protected void imgHCus_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txthCus.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void CheckAccount(object sender, EventArgs e)
        {
            _invoicebyaccount = new List<InvoiceHeader>();
            if (!string.IsNullOrEmpty(txtHAcc.Text))
            {
                _invoicebyaccount = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, txtHAcc.Text.Trim());
                if (_invoicebyaccount != null)
                {

                    foreach (InvoiceHeader Acc in _invoicebyaccount)
                    {
                        if (Acc.Sah_stus == "C")
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select invoice is cancelled.");
                            txtHAcc.Text = "";
                            txtHAcc.Focus();
                            return;
                        }
                        else if (Acc.Sah_stus == "R")
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select invoice is reversd.");
                            txtHAcc.Text = "";
                            txtHAcc.Focus();
                            return;
                        }
                        else
                        {
                            txtHAcc.Text = Acc.Sah_anal_2;
                            return;
                        }
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid account.");
                    txtHAcc.Text = "";
                    txtHAcc.Focus();
                    return;
                }
            }
        }

        protected void LoadInvoice(object sender, EventArgs e)
        {
            _masterInvoiceHeader = new InvoiceHeader();
            if (!string.IsNullOrEmpty(txtInv.Text))
            {
                _masterInvoiceHeader = CHNLSVC.Sales.GetPendingInvoiceHeader(GlbUserComCode, GlbUserDefProf, "INV", txtInv.Text.Trim(), "A");

                if (_masterInvoiceHeader != null)
                {
                    if (!string.IsNullOrEmpty(txtCus.Text))
                    {
                        if (_masterInvoiceHeader.Sah_cus_cd != txtCus.Text.Trim())
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected invoice not belongs to above customer.");
                            txtInv.Text = "";
                            txtInv.Focus();
                            return;
                        }
                    }
                    else
                    {
                        txtInv.Text = _masterInvoiceHeader.Sah_inv_no;
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid invoice.");
                    txtInv.Text = "";
                    txtInv.Focus();
                    return;
                }
            }
            btnSearch.Focus();
        }

        protected void LoadHireSales(object sender, EventArgs e)
        {
            _masterInvoiceHeader = new InvoiceHeader();
            if (!string.IsNullOrEmpty(txtHInv.Text))
            {
                _masterInvoiceHeader = CHNLSVC.Sales.GetPendingInvoiceHeader(GlbUserComCode, GlbUserDefProf, "HS", txtHInv.Text.Trim(), "A");

                if (_masterInvoiceHeader != null)
                {
                    if (!string.IsNullOrEmpty(txthCus.Text))
                    {
                        if (_masterInvoiceHeader.Sah_cus_cd != txthCus.Text.Trim())
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected invoice not belongs to above customer.");
                            txtHInv.Text = "";
                            txtHInv.Focus();
                            return;
                        }
                    }
                    else
                    {
                        txtHInv.Text = _masterInvoiceHeader.Sah_inv_no;
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid hire sales invoice.");
                    txtHInv.Text = "";
                    txtHInv.Focus();
                    return;
                }
            }
            btnHSearch.Focus();
        }

        protected void LoadCustomerDetailsByCustomer(object sender, EventArgs e)
        {
            _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCus.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCus.Text, string.Empty, string.Empty, "C");

                if (_masterBusinessCompany.Mbe_acc_cd != null)
                {

                    txtCus.Text = _masterBusinessCompany.Mbe_cd;

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid customer");
                    txtCus.Text = "";
                    txtCus.Focus();
                    return;
                }
            }
            txtInv.Focus();

        }


        protected void imgHInv_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HireSalesInvoice);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtHInv.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void imginv_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInv.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();


        }

        protected void btnHSearch_Click(object sender, EventArgs e)
        {
            List<InvoiceHeader> _paramInvoice = new List<InvoiceHeader>();

            _paramInvoice = CHNLSVC.Sales.GetHireSaleInvoiceForReverse(GlbUserComCode, GlbUserDefProf, txthCus.Text.Trim(), txtHInv.Text.Trim(), txthfDate.Text.Trim(), txthtDate.Text.Trim(), txtHAcc.Text.Trim());
            _InvHeaderList = _paramInvoice;
            gvHInv.DataSource = _InvHeaderList;
            gvHInv.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            List<InvoiceHeader> _paramInvoice = new List<InvoiceHeader>();

            _paramInvoice = CHNLSVC.Sales.GetPendingInvoices(GlbUserComCode, GlbUserDefProf, txtCus.Text.Trim(), txtInv.Text.Trim(), "A", txtfDate.Text.Trim(), txttDate.Text.Trim());
            _InvHeaderList = _paramInvoice;
            gvInvoice.DataSource = _InvHeaderList;
            gvInvoice.DataBind();

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();

        }

        #region Close Button
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnHClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        #endregion

        protected void btnHClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();
        }

        protected void gvHInv_Rowcommand(object sender, GridViewCommandEventArgs e)
        {
            string _HInv = "";
            string _HAcc = "";
            decimal _Amt = 0;
            decimal _dis = 0;
            decimal _tax = 0;
            decimal _tot = 0;

            switch (e.CommandName.ToUpper())
            {
                case "HSELECT":
                    {
                        for (int i = 0; i < gvHInv.Rows.Count; i++)
                        {
                            CheckBox chk = (CheckBox)gvHInv.Rows[i].FindControl("chkHSelect");

                            if (chk.Checked == true)
                            {
                                chk.Checked = false;
                            }
                        }

                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        CheckBox check = (CheckBox)row.FindControl("chkHSelect");
                        check.Checked = true;

                        _HInv = row.Cells[7].Text.ToString();
                        _HAcc = row.Cells[10].Text.ToString();

                        _doitemserials = new List<ReptPickSerials>();
                        List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
                        List<InvoiceItem> _InvList = new List<InvoiceItem>();
                        List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                        _InvDetailList = new List<InvoiceItem>();

                        _paramInvoiceItems = CHNLSVC.Sales.GetAllInvoiceItems(_HInv);
                        foreach (InvoiceItem item in _paramInvoiceItems)
                        {
                            _Amt = _Amt + (Convert.ToDecimal(item.Sad_unit_amt));
                            _dis = _dis + (Convert.ToDecimal(item.Sad_disc_amt));
                            _tax = _tax + (Convert.ToDecimal(item.Sad_itm_tax_amt));
                            _tot = _tot + (Convert.ToDecimal(item.Sad_tot_amt));

                            _InvList.Add(item);


                            _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerial(GlbUserComCode, GlbUserDefLoca, GlbUserName, GlbUserSessionID, hdnAllowBin.Value, item.Sad_inv_no, item.Sad_itm_line);
                            _doitemserials.AddRange(_tempDOSerials);
                        }

                        _InvDetailList = _InvList;
                        gvHItems.DataSource = _InvDetailList;
                        gvHItems.DataBind();

                        gvDelDetails.DataSource = _doitemserials;
                        gvDelDetails.DataBind();


                        HpAccount accList = new HpAccount();
                        accList = CHNLSVC.Sales.GetHP_Account_onAccNo(_HAcc);
                        AccountsList = accList;//save in veiw statement
                        if (accList == null)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                            return;
                        }
                       
                        uc_HpAccountSummary1.set_all_values(AccountsList, GlbUserDefProf, DateTime.Now.Date, GlbUserDefProf);
                        uc_HpAccountDetail1.Uc_hpa_acc_no = _HAcc;

                        lblHAmt.Text = _Amt.ToString("0.00");
                        lblHdis.Text = _dis.ToString("0.00");
                        lblHTax.Text = _tax.ToString("0.00");
                        lblHtot.Text = _tot.ToString("0.00");
                        break;
                    }
            }
        }

        protected void gvInvItems_Rowcommand(object sender, GridViewCommandEventArgs e)
        {
            string _Inv = "";
            string _item = "";
            decimal _balQty = 0;
            decimal _revQty = 0;
            decimal _totAmount = 0;
            string _unitAmt = "";
            string _disAmt = "";
            string _taxAmt = "";
            string _totAmt = "";

            _InvQty = 0;

            switch (e.CommandName.ToUpper())
            {
                case "SELECT":
                    {
                        //gvInvoice.DataSource = _InvHeaderList;
                        //gvInvoice.DataBind();
                        for (int i = 0; i < gvInvoice.Rows.Count; i++)
                        {
                            CheckBox chk = (CheckBox)gvInvoice.Rows[i].FindControl("chkselect");

                            if (chk.Checked == true)
                            {
                                chk.Checked = false;
                            }
                        }


                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        CheckBox check = (CheckBox)row.FindControl("chkSelect");
                        check.Checked = true;

                        _Inv = row.Cells[7].Text.ToString();
                        List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
                        List<InvoiceItem> _InvList = new List<InvoiceItem>();
                        _paramInvoiceItems = CHNLSVC.Sales.GetPendingInvoiceItems(_Inv);

                        _doitemserials = new List<ReptPickSerials>();
                        List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();

                        foreach (InvoiceItem item in _paramInvoiceItems)
                        {
                            _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty)).ToString("0.00", CultureInfo.InvariantCulture);
                            _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty)).ToString("0.00", CultureInfo.InvariantCulture);
                            _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty)).ToString("0.00", CultureInfo.InvariantCulture);
                            _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty)).ToString("0.00", CultureInfo.InvariantCulture);

                            item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
                            item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
                            item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
                            item.Sad_tot_amt = Convert.ToDecimal(_totAmt);

                            _InvList.Add(item);
                            _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerial(GlbUserComCode, GlbUserDefLoca, GlbUserName, GlbUserSessionID, hdnAllowBin.Value, item.Sad_inv_no, item.Sad_itm_line);
                            _doitemserials.AddRange(_tempDOSerials);
                        }

                        _InvDetailList = _InvList;
                        gvInvItems.DataSource = _InvDetailList;
                        gvInvItems.DataBind();


                        foreach (InvoiceItem calc in _InvDetailList)
                        {
                            _totAmount = _totAmount + calc.Sad_tot_amt;
                        }

                        txttotal.Text = _totAmount.ToString("0.00", CultureInfo.InvariantCulture);
                        _totAmount = 0;
                        break;
                    }
                case "CHANGE":
                    {
                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        _item = row.Cells[3].Text.ToString();
                        _InvQty = Convert.ToDecimal(row.Cells[7].Text);
                        _balQty = Convert.ToDecimal(row.Cells[8].Text);
                        _revQty = Convert.ToDecimal(row.Cells[8].Text);
                        _itmLine = Convert.ToInt32(row.Cells[1].Text);
                        txtEditItem.Text = _item;
                        txtEditQty.Text = _balQty.ToString();
                        txtRevQty.Text = _revQty.ToString();
                        txtRevQty.Focus();
                        break;
                    }
            }
        }

        protected void OnRemoveFromInvoiceItem(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;
            decimal _totAmount = 0;

            if (_InvDetailList != null)
                if (_InvDetailList.Count > 0)
                {

                    List<InvoiceItem> _tempList = _InvDetailList;
                    _tempList.RemoveAt(row_id);
                    _InvDetailList = _tempList;
                    gvInvItems.DataSource = _InvDetailList;
                    gvInvItems.DataBind();

                }

            foreach (InvoiceItem calc in _InvDetailList)
            {
                _totAmount = _totAmount + calc.Sad_tot_amt;
            }

            txttotal.Text = _totAmount.ToString("0.00", CultureInfo.InvariantCulture);
            _totAmount = 0;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEditItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select reverse item.");
                txtEditItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtEditQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Balance qty is invalid.");
                txtEditQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtRevQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid reverse qty.");
                txtRevQty.Focus();
                return;
            }

            if (Convert.ToDecimal(txtEditQty.Text) < Convert.ToDecimal(txtRevQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Reverse qty exceed balance qty.");
                txtRevQty.Focus();
                return;
            }

            if (Convert.ToDecimal(_InvQty) == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid invoice qty.Please re-try.");
                txtRevQty.Focus();
                return;
            }

            List<InvoiceItem> _InvList = new List<InvoiceItem>();
            string _unitAmt = "";
            string _disAmt = "";
            string _taxAmt = "";
            string _totAmt = "";
            decimal _totAmount = 0;

            foreach (InvoiceItem item in _InvDetailList)
            {
                if (item.Sad_itm_cd.Equals(txtEditItem.Text) && item.Sad_itm_line == Convert.ToInt32(_itmLine))
                {
                    item.Sad_srn_qty = Convert.ToDecimal(txtRevQty.Text);
                    _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(_InvQty) * Convert.ToDecimal(txtRevQty.Text)).ToString("0.00", CultureInfo.InvariantCulture);
                    _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(_InvQty) * Convert.ToDecimal(txtRevQty.Text)).ToString("0.00", CultureInfo.InvariantCulture);
                    _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(_InvQty) * Convert.ToDecimal(txtRevQty.Text)).ToString("0.00", CultureInfo.InvariantCulture);
                    _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(_InvQty) * Convert.ToDecimal(txtRevQty.Text)).ToString("0.00", CultureInfo.InvariantCulture);

                    item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
                    item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
                    item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
                    item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                }
                _InvList.Add(item);
            }

            _InvDetailList = _InvList;
            gvInvItems.DataSource = _InvDetailList;
            gvInvItems.DataBind();

            foreach (InvoiceItem calc in _InvDetailList)
            {
                _totAmount = _totAmount + calc.Sad_tot_amt;
            }

            txtEditItem.Text = "";
            txtEditQty.Text = "";
            txtRevQty.Text = "";
            _InvQty = 0;
            txttotal.Text = _totAmount.ToString("0.00", CultureInfo.InvariantCulture);
            _totAmount = 0;
            txtEditItem.Focus();

        }

        protected void btnHSave_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            string _ReversNo = "";
            string _docNo = "";
            if (gvHItems.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Reversal details cannot found.");
                return;
            }

            InvoiceHeader _invheader = new InvoiceHeader();

            for (int i = 0; i < gvHInv.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvHInv.Rows[i].FindControl("chkHselect");

                if (chk.Checked == true)
                {
                    //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
                    _invheader.Sah_com = gvHInv.DataKeys[i][0].ToString();
                    _invheader.Sah_cre_by = GlbUserName;
                    _invheader.Sah_cre_when = DateTime.Now;
                    _invheader.Sah_currency = gvHInv.Rows[i].Cells[17].Text;
                    _invheader.Sah_cus_add1 = gvHInv.DataKeys[i][6].ToString();
                    _invheader.Sah_cus_add2 = gvHInv.DataKeys[i][7].ToString();
                    _invheader.Sah_cus_cd = gvHInv.Rows[i].Cells[13].Text;
                    _invheader.Sah_cus_name = gvHInv.DataKeys[i][5].ToString();
                    _invheader.Sah_d_cust_add1 = gvHInv.DataKeys[i][9].ToString();
                    _invheader.Sah_d_cust_add2 = gvHInv.DataKeys[i][10].ToString();
                    _invheader.Sah_d_cust_cd = gvHInv.DataKeys[i][8].ToString();
                    _invheader.Sah_direct = false;
                    _invheader.Sah_dt = Convert.ToDateTime(DateTime.Now).Date;
                    _invheader.Sah_epf_rt = 0;
                    _invheader.Sah_esd_rt = 0;
                    _invheader.Sah_ex_rt = Convert.ToInt32(gvHInv.DataKeys[i][11]);
                    _invheader.Sah_inv_no = "na";
                    _invheader.Sah_inv_sub_tp = "REV";
                    _invheader.Sah_inv_tp = gvHInv.DataKeys[i][3].ToString();
                    _invheader.Sah_is_acc_upload = false;
                    _invheader.Sah_man_cd = "";
                    _invheader.Sah_man_ref = gvHInv.DataKeys[i][13].ToString();
                    _invheader.Sah_manual = false;
                    _invheader.Sah_mod_by = GlbUserName;
                    _invheader.Sah_mod_when = DateTime.Now;
                    _invheader.Sah_pc = GlbUserDefProf;
                    _invheader.Sah_pdi_req = 0;
                    _invheader.Sah_ref_doc = gvHInv.Rows[i].Cells[7].Text;
                    _invheader.Sah_remarks = txtRemarks.Text;
                    _invheader.Sah_sales_chn_cd = "";
                    _invheader.Sah_sales_chn_man = "";
                    _invheader.Sah_sales_ex_cd = gvHInv.Rows[i].Cells[11].Text;
                    _invheader.Sah_sales_region_cd = "";
                    _invheader.Sah_sales_region_man = "";
                    _invheader.Sah_sales_sbu_cd = "";
                    _invheader.Sah_sales_sbu_man = "";
                    _invheader.Sah_sales_str_cd = "";
                    _invheader.Sah_sales_zone_cd = "";
                    _invheader.Sah_sales_zone_man = "";
                    _invheader.Sah_seq_no = 1;
                    _invheader.Sah_session_id = GlbUserSessionID;
                    _invheader.Sah_structure_seq = "";
                    _invheader.Sah_stus = "A";
                    _invheader.Sah_town_cd = "";
                    _invheader.Sah_tp = "INV";
                    _invheader.Sah_wht_rt = 0;
                    _invheader.Sah_tax_inv = Convert.ToBoolean(gvHInv.DataKeys[i][12]);
                    _invheader.Sah_anal_2 = gvHInv.Rows[i].Cells[10].Text;


                    MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                    _invoiceAuto.Aut_cate_cd = GlbUserDefProf;
                    _invoiceAuto.Aut_cate_tp = "PC";
                    _invoiceAuto.Aut_direction = 0;
                    _invoiceAuto.Aut_modify_dt = null;
                    _invoiceAuto.Aut_moduleid = "REV";
                    _invoiceAuto.Aut_number = 0;
                    _invoiceAuto.Aut_start_char = "HPREV";
                    _invoiceAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;

                    
                    //inventory document
                    InventoryHeader _inventoryHeader = new InventoryHeader();
                    _inventoryHeader.Ith_com = GlbUserComCode;
                    _inventoryHeader.Ith_loc = GlbUserDefLoca;
                    DateTime _docDate = Convert.ToDateTime(DateTime.Now).Date;
                    _inventoryHeader.Ith_doc_date = _docDate;
                    _inventoryHeader.Ith_doc_year = _docDate.Year;
                    _inventoryHeader.Ith_direct = true;
                    _inventoryHeader.Ith_doc_tp = "SRN";
                    _inventoryHeader.Ith_cate_tp = "";
                    _inventoryHeader.Ith_bus_entity = "";
                    _inventoryHeader.Ith_is_manual = false;
                    _inventoryHeader.Ith_manual_ref = "";
                    _inventoryHeader.Ith_remarks = "";
                    _inventoryHeader.Ith_stus = "A";
                    _inventoryHeader.Ith_cre_by = GlbUserName;
                    _inventoryHeader.Ith_cre_when = DateTime.Now;
                    _inventoryHeader.Ith_mod_by = GlbUserName;
                    _inventoryHeader.Ith_mod_when = DateTime.Now;
                    _inventoryHeader.Ith_session_id = GlbUserSessionID;
                
                       MasterAutoNumber _SRNAuto = new MasterAutoNumber();
                    _SRNAuto.Aut_cate_cd = GlbUserDefLoca;
                    _SRNAuto.Aut_cate_tp = "LOC";
                    _SRNAuto.Aut_direction = 1;
                    _SRNAuto.Aut_modify_dt = null;
                    _SRNAuto.Aut_moduleid = "SRN";
                    _SRNAuto.Aut_number = 0;
                    _SRNAuto.Aut_start_char = "SRN";
                    _SRNAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;

                    int effect = CHNLSVC.Sales.SaveReversal(_invheader, _InvDetailList, _invoiceAuto, true, out _ReversNo,_inventoryHeader,_doitemserials,_doitemSubSerials,_SRNAuto,out _docNo);

                    
                    if (effect == 1)
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully reverse.Reversal No: " + _ReversNo);
                        Clear_Data();
                        //string Msg = "<script>alert('Request Successfully Saved!');window.location = 'HpReceiptReversal.aspx';</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        
                        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created.Reversal No: " + _ReversNo + _docNo);
                        Clear_Data();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                        }
                        else
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                        }
                    }


                }
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;

            if (gvInvItems.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Reversal details cannot found.");
                return;
            }

            InvoiceHeader _invheader = new InvoiceHeader();

            for (int i = 0; i < gvInvoice.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvInvoice.Rows[i].FindControl("chkselect");

                if (chk.Checked == true)
                {
                    //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
                    _invheader.Sah_com = gvInvoice.DataKeys[i][0].ToString();
                    _invheader.Sah_cre_by = GlbUserName;
                    _invheader.Sah_cre_when = DateTime.Now;
                    _invheader.Sah_currency = gvInvoice.Rows[i].Cells[17].Text;
                    _invheader.Sah_cus_add1 = gvInvoice.DataKeys[i][6].ToString();
                    _invheader.Sah_cus_add2 = gvInvoice.DataKeys[i][7].ToString();
                    _invheader.Sah_cus_cd = gvInvoice.Rows[i].Cells[13].Text;
                    _invheader.Sah_cus_name = gvInvoice.DataKeys[i][5].ToString();
                    _invheader.Sah_d_cust_add1 = gvInvoice.DataKeys[i][9].ToString();
                    _invheader.Sah_d_cust_add2 = gvInvoice.DataKeys[i][10].ToString();
                    _invheader.Sah_d_cust_cd = gvInvoice.DataKeys[i][8].ToString();
                    _invheader.Sah_direct = false;
                    _invheader.Sah_dt = Convert.ToDateTime(DateTime.Now).Date;
                    _invheader.Sah_epf_rt = 0;
                    _invheader.Sah_esd_rt = 0;
                    _invheader.Sah_ex_rt = Convert.ToInt32(gvInvoice.DataKeys[i][11]);
                    _invheader.Sah_inv_no = "na";
                    _invheader.Sah_inv_sub_tp = "REV";
                    _invheader.Sah_inv_tp = gvInvoice.DataKeys[i][3].ToString();
                    _invheader.Sah_is_acc_upload = false;
                    _invheader.Sah_man_cd = "";
                    _invheader.Sah_man_ref = gvInvoice.Rows[i].Cells[10].Text;
                    _invheader.Sah_manual = false;
                    _invheader.Sah_mod_by = GlbUserName;
                    _invheader.Sah_mod_when = DateTime.Now;
                    _invheader.Sah_pc = GlbUserDefProf;
                    _invheader.Sah_pdi_req = 0;
                    _invheader.Sah_ref_doc = gvInvoice.Rows[i].Cells[7].Text;
                    _invheader.Sah_remarks = txtRemarks.Text;
                    _invheader.Sah_sales_chn_cd = "";
                    _invheader.Sah_sales_chn_man = "";
                    _invheader.Sah_sales_ex_cd = gvInvoice.Rows[i].Cells[11].Text;
                    _invheader.Sah_sales_region_cd = "";
                    _invheader.Sah_sales_region_man = "";
                    _invheader.Sah_sales_sbu_cd = "";
                    _invheader.Sah_sales_sbu_man = "";
                    _invheader.Sah_sales_str_cd = "";
                    _invheader.Sah_sales_zone_cd = "";
                    _invheader.Sah_sales_zone_man = "";
                    _invheader.Sah_seq_no = 1;
                    _invheader.Sah_session_id = GlbUserSessionID;
                    _invheader.Sah_structure_seq = "";
                    _invheader.Sah_stus = "A";
                    _invheader.Sah_town_cd = "";
                    _invheader.Sah_tp = "INV";
                    _invheader.Sah_wht_rt = 0;
                    _invheader.Sah_tax_inv = Convert.ToBoolean(gvInvoice.DataKeys[i][12]);


                    MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                    _invoiceAuto.Aut_cate_cd = GlbUserDefProf;
                    _invoiceAuto.Aut_cate_tp = "PC";
                    _invoiceAuto.Aut_direction = 0;
                    _invoiceAuto.Aut_modify_dt = null;
                    _invoiceAuto.Aut_moduleid = "REV";
                    _invoiceAuto.Aut_number = 0;
                    _invoiceAuto.Aut_start_char = "INREV";
                    _invoiceAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;


                    //inventory document
                    InventoryHeader _inventoryHeader = new InventoryHeader();
                    _inventoryHeader.Ith_com = GlbUserComCode;
                    _inventoryHeader.Ith_loc = GlbUserDefLoca;
                    DateTime _docDate = Convert.ToDateTime(DateTime.Now).Date;
                    _inventoryHeader.Ith_doc_date = _docDate;
                    _inventoryHeader.Ith_doc_year = _docDate.Year;
                    _inventoryHeader.Ith_direct = true;
                    _inventoryHeader.Ith_doc_tp = "SRN";
                    _inventoryHeader.Ith_cate_tp = "";
                    _inventoryHeader.Ith_bus_entity = "";
                    _inventoryHeader.Ith_is_manual = false;
                    _inventoryHeader.Ith_manual_ref = "";
                    _inventoryHeader.Ith_remarks = "";
                    _inventoryHeader.Ith_stus = "A";
                    _inventoryHeader.Ith_cre_by = GlbUserName;
                    _inventoryHeader.Ith_cre_when = DateTime.Now;
                    _inventoryHeader.Ith_mod_by = GlbUserName;
                    _inventoryHeader.Ith_mod_when = DateTime.Now;
                    _inventoryHeader.Ith_session_id = GlbUserSessionID;

                    MasterAutoNumber _SRNAuto = new MasterAutoNumber();
                    _SRNAuto.Aut_cate_cd = GlbUserDefLoca;
                    _SRNAuto.Aut_cate_tp = "LOC";
                    _SRNAuto.Aut_direction = 1;
                    _SRNAuto.Aut_modify_dt = null;
                    _SRNAuto.Aut_moduleid = "SRN";
                    _SRNAuto.Aut_number = 0;
                    _SRNAuto.Aut_start_char = "SRN";
                    _SRNAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;


                    string _ReversNo = "";
                    string _crednoteNo=""; //add by chamal 05-12-2012

                    int effect = CHNLSVC.Sales.SaveReversal(_invheader, _InvDetailList, _invoiceAuto, false, out _ReversNo, _inventoryHeader,_doitemserials, _doitemSubSerials, _SRNAuto, out _crednoteNo);
                    
                    
                    if (effect == 1)
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created.Reversal No: " + _ReversNo);
                        Clear_Data();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                        }
                        else
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                        }
                    }


                }
            }

        }


    }
}