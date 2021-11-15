using FF.AbansTours.UserControls;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.Sales
{
    public partial class Payment : BasePage
    {
        private List<HpSheduleDetails> _sheduleDetails = new List<HpSheduleDetails>();
        private List<VehicalRegistration> _regList = new List<VehicalRegistration>();
        private List<VehicleInsuarance> _insList = new List<VehicleInsuarance>();
        private HpAccount _HpAccount = new HpAccount();
        private HpSchemeDetails _SchemeDetails = new HpSchemeDetails();
        private List<ReptPickSerials> _ResList = new List<ReptPickSerials>();
        private List<RecieptItemTBS> _list = new List<RecieptItemTBS>();
        private List<GiftVoucherPages> _gvDetails = new List<GiftVoucherPages>();
        private List<ReceiptItemDetails> _tmpRecItem = new List<ReceiptItemDetails>();

        //private Boolean _RecStatus = false;
        private Boolean _sunUpload = false;
        private decimal _usedAmt = 0;

        private List<RecieptItemTBS> recieptItem;
        private decimal _paidAmount;
        List<RecieptItemTBS> recieptItemList;
        List<PaymentType> _paymentTypeRef;
        public List<RecieptItemTBS> RecieptItemList
        {
            get { return recieptItemList; }
            set { recieptItemList = value; }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ValidateTrue();
                    LoadPaymenttypes();
                    LoadPayMode();
                    PageClear();                    
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void imgbtnDriver_Click(object sender, ImageClickEventArgs e)
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
                ucc.ReturnResultControl = txtDriverCode.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDriverCode.Focus();
                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void imgbtnPaymentNo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentDate);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetPaymentDateTBS(ucc.SearchParams, null, null, Convert.ToDateTime(DateTime.Now.Date).AddMonths(-1), Convert.ToDateTime(DateTime.Now.Date));
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtPaymentNo.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = true;
                ucc.DateEnable(enable);
                txtPaymentNo.Focus();
                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            } 
        }
        protected void ImgPaymentNoserch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtPaymentNo.Text))
                {
                    divError.Visible = true;
                    lblError.Text = "Receipt No is required.";
                    return;
                }
                ViewState["_RecStatus"] = false;
                _sunUpload = false;
                Boolean _isValidRec = false;

                RecieptHeaderTBS _ReceiptHeader = null;
                _ReceiptHeader = CHNLSVC.Tours.GetReceiptHeaderTBS(base.GlbUserComCode, base.GlbUserDefProf, txtPaymentNo.Text.Trim());

                if (_ReceiptHeader != null)
                {
                    ddlPaymentType.SelectedValue = _ReceiptHeader.Sir_receipt_type;

                    _isValidRec = CHNLSVC.Sales.IsValidReceiptType(base.GlbUserComCode, _ReceiptHeader.Sir_receipt_type);

                    if (_isValidRec == false)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Not allowed to view receipt type " + _ReceiptHeader.Sir_receipt_type + " in receipt module.');", true);
                        return;
                    }

                    // = _ReceiptHeader.Sir_receipt_no;
                    
                    txtRemark.Text = _ReceiptHeader.Sir_remarks;
                    txtDriverCode.Text = _ReceiptHeader.Sir_debtor_cd;
                    //txtInvoiceAmount.Text = Convert.ToDecimal(_ReceiptHeader.Sir_tot_settle_amt).ToString("n");
                    //txtCode.Text = _ReceiptHeader.Sir_debtor_cd;
                    //txtCode.Enabled = false;

                    //txtAddress.Text = _ReceiptHeader.Sir_debtor_add_1;
                    //txtAddress2.Text = _ReceiptHeader.Sir_debtor_add_2;
                    //txtName.Text = _ReceiptHeader.Sir_debtor_name;
                    //txtMobile.Text = _ReceiptHeader.Sir_mob_no;
                    //txtNIC.Text = _ReceiptHeader.Sir_nic_no;
                    //txtProvince.Text = _ReceiptHeader.Sir_anal_2;
                    txtTotalAmount.Text = Convert.ToDecimal(_ReceiptHeader.Sir_tot_settle_amt).ToString("n");
                    txtBalanceAmount.Text = "0.00";
                    _usedAmt = _ReceiptHeader.Sir_used_amt;
                    //txtTotalReceiptAmount.Text = Convert.ToDecimal(_ReceiptHeader.Sir_tot_settle_amt).ToString("n");
                    //txtSalesExecutive.Text = _ReceiptHeader.Sir_create_by;
                    
                    //ddlDistrict.SelectedItem.Text = _ReceiptHeader.Sir_anal_1;
                    ViewState["seq_no"] = _ReceiptHeader.Sir_seq_no;
                    ViewState["_RecStatus"] = _ReceiptHeader.Sir_act;
                    _sunUpload = _ReceiptHeader.Sir_uploaded_to_finance;

                    BindSaveReceiptDetails(_ReceiptHeader.Sir_receipt_no);

                    _gvDetails = new List<GiftVoucherPages>();
                    _gvDetails = CHNLSVC.Inventory.GetGiftVoucherByOthRef(_ReceiptHeader.Sir_com_cd, _ReceiptHeader.Sir_profit_center_cd, _ReceiptHeader.Sir_receipt_no);   

                    _tmpRecItem = new List<ReceiptItemDetails>();
                    _tmpRecItem = CHNLSVC.Sales.GetAdvanReceiptItems(_ReceiptHeader.Sir_receipt_no);

                    grdLogSheet.DataSource = CHNLSVC.Tours.Get_sr_pay_log(_ReceiptHeader.Sir_receipt_no);
                    grdLogSheet.DataBind();

                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    txtPaymentNo.Enabled = false;
                    ddlPaymentType.Enabled = false;
                    txtDriverCode.Enabled = false;
                    btnSearch.Enabled = false;
                    ImgAmount.Enabled = false;
                    imgbtnPaymentNo.Enabled = false;
                }
           
                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            } 
        }
        protected void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal total = Convert.ToDecimal(txtTotalAmount.Text);
                txtTotalAmount.Text = total.ToString("N2");
                txtBalanceAmount.Text = total.ToString("N2");
                txtAmount.Text = total.ToString("N2");
                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            } 
        }
        protected void ddlPaymentType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPaymentType.SelectedItem.Value == "TPAY")
                {
                    btnSearch.Enabled = true;
                    txtDriverCode.Enabled = true;
                    txtDriverCode.Text = string.Empty;
                    txtTotalAmount.Enabled = false;

                    txtTotalAmount.Text = string.Empty;
                    txtBalanceAmount.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                }
                if (ddlPaymentType.SelectedItem.Value == "ADPAY")
                {
                    if (string.IsNullOrEmpty(txtDriverCode.Text))
                    {
                        ddlPaymentType.SelectedValue = "TPAY";
                        divError.Visible = true;
                        lblError.Text = "Please enter Driver Code.";
                        return;
                    }
                    btnSearch.Enabled = false;
                    txtDriverCode.Enabled = false;
                    txtTotalAmount.Enabled = true;
                    grdLogSheet.DataSource = new int[] { };
                    grdLogSheet.DataBind();
                }
                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        } 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDriverCode.Text))
                {
                    divError.Visible = true;
                    lblError.Text = "Please enter Driver Code.";
                }
                else
                {
                    DataTable datatable = CHNLSVC.Tours.Get_tour_logsheet(base.GlbUserComCode, base.GlbUserDefProf, txtDriverCode.Text, Convert.ToDateTime(txtPaymentFromDate.Text), Convert.ToDateTime(txtPaymentToDate.Text));
                    Session["logsheet"] = datatable;
                    if (datatable.Rows.Count < 1)
                    {
                        divError.Visible = true;
                        lblError.Text = "Search details are not found.";
                        return;
                    }
                    decimal totalAmount;
                    Decimal.TryParse(datatable.Compute("sum(TLD_TOT)", "").ToString(), out totalAmount);
                    txtTotalAmount.Text = totalAmount.ToString("N2");
                    txtAmount.Text = txtTotalAmount.Text;
                    txtBalanceAmount.Text = txtTotalAmount.Text;
                    grdLogSheet.DataSource = datatable;
                    grdLogSheet.DataBind();

                    ValidateTrue();
                    txtPaymentNo.Enabled = false;
                    ddlPaymentType.Enabled = false;
                    txtDriverCode.Enabled = false;
                    txtPaymentNo.Text = string.Empty;
                    ImgPaymentNoserch.Enabled = false;
                    imgbtnPaymentNo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }            
        }
        

        protected void lbtColse_Click(object sender, EventArgs e)
        {
            divSuccess.Visible = false;
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            divError.Visible = false;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DivInfo.Visible = false;
        }

        protected void ImgAmount_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (String.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString()))
                {
                    divError.Visible = true;
                    lblError.Text = "Please select the valid payment type.";
                    return;
                }
                if (String.IsNullOrEmpty(txtAmount.Text))
                {
                    divError.Visible = true;
                    lblError.Text = "Please select the valid pay amount.";
                    return;
                }
                if (String.IsNullOrEmpty(txtBalanceAmount.Text))
                {
                    divError.Visible = true;
                    lblError.Text = "Please select the valid pay amount.";
                    return;
                }
                if (Convert.ToDecimal(txtBalanceAmount.Text) <= 0)
                {
                    divError.Visible = true;
                    lblError.Text = "Please select the valid pay amount.";
                    return;
                }
                if (ddlPayMode.SelectedItem.Text.ToUpper() == CommonUIDefiniton.PayMode.CASH.ToString())
                {
                    if (String.IsNullOrEmpty(txtDepositBank.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Bank is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBranch.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Branch is required.";
                        return;
                    }
                }
                if (ddlPayMode.SelectedItem.Text.ToUpper() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    if (String.IsNullOrEmpty(txtChequeNo.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "ChequeNo is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBankCheque.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Bank is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBranchCheque.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Branch is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtChequeDate.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Cheque Date is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBankCheque.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Deposit Bank is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBranchCheque.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Deposit Branch is required.";
                        return;
                    }                   
                }
                if (ddlPayMode.SelectedItem.Text.ToUpper() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    if (String.IsNullOrEmpty(txtCardNo.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Card No is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBankCard.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Bank is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtBranchCard.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Branch is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBankCard.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Deposit Bank is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDepositBranchCard.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Branch Card is required.";
                        return;
                    }
                    if (String.IsNullOrEmpty(txtPeriod.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Branch is required.";
                        return;
                    }
                }
                
                AddPayment();
                recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];
                LoadRecieptGrid();

                txtChequeNo.Text = string.Empty;
                txtBranchCheque.Text = String.Empty;
                txtBankCheque.Text = String.Empty;
                txtChequeDate.Text = String.Empty;
                txtDepositBranchCheque.Text = string.Empty;
                txtDepositBankCheque.Text = string.Empty;

                lblPromotion.Text = string.Empty;
                lblbank.Text = string.Empty;
                txtDepositBranchCard.Text = string.Empty;
                txtDepositBankCard.Text = string.Empty;
                ddlCardType.Items.Clear();
                ddlCardType.DataBind();

                ValidateTrue();

                txtTotalAmount.Enabled = false;
                txtDriverCode.Enabled = false;
                ddlPaymentType.Enabled = false;
                btnSearch.Enabled = false;

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void ddlPayMode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mltPaymentDetails.ActiveViewIndex = 0;

                if (ddlPayMode.SelectedValue.ToString() == "CRCD")
                {
                    mltPaymentDetails.ActiveViewIndex = 1;
                }
                else if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
                {
                    mltPaymentDetails.ActiveViewIndex = 2;
                }
                else if (ddlPayMode.SelectedValue.ToString() == "ADVAN")
                {
                    mltPaymentDetails.ActiveViewIndex = 3;
                }

                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void imgbtntxtBankCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = base.CHNLSVC.CommonSearch.GetBusinessCompany(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtBankCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtBankCard.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void imgbtnbankcard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBankCard.Text))
                {
                    divError.Visible = true;
                    lblError.Text = "Bank is required.";
                    txtBankCard.Focus();
                    return;
                }
                else
                {
                    if (!CheckBank(txtBankCard.Text, lblbank))
                    {
                        txtBankCard.Text = string.Empty;
                        txtBankCard.Focus();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) > 0)
                        {
                            //LoadBankChg();
                            LoadCardType(txtBankCard.Text);
                            //PROMOTION
                            //LoadPromotions();
                            // comboBoxPayModes_SelectionChangeCommitted(null, null);
                        }
                        else
                        {
                            divError.Visible = true;
                            lblError.Text = "Amount is required";
                            return;
                        }
                    }                    
                }
                //LoadBankChg();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void imgbtnDepositBankCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable _result = base.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtDepositBankCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBankCard.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void imgbtnBankCheque_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetBusinessCompany(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtBankCheque.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtBankCheque.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void imgbtnBranchCheque_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchBankBranchData(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtBranchCheque.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtBranchCheque.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void imgbtnDepositBankCheque_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtDepositBankCheque.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBankCheque.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void imgbtnDepositBranchCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
                DataTable _result = base.CHNLSVC.CommonSearch.SearchBankBranchData(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtDepositBranchCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBranchCard.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }        
        protected void grdPaymentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteAmount")
                {
                    recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];
                    RecieptItemList.RemoveAt(Convert.ToInt32(e.CommandArgument));

                    Session["recieptItem"] = recieptItemList;

                    _paidAmount = 0;
                    foreach (RecieptItemTBS _list in RecieptItemList)
                    {
                        _paidAmount += _list.Sird_settle_amt;
                    }

                    GridViewRow selectedRow = grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)];
                    //txtPaidAmount.Text = (Convert.ToDouble(txtPaidAmount.Text) - Convert.ToDouble((grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)]).Cells[19])).ToString();
                    txtAmount.Text = FormatToCurrency((Convert.ToDouble(txtAmount.Text) - Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());
                    txtBalanceAmount.Text = FormatToCurrency((Convert.ToDouble(txtBalanceAmount.Text) + Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());

                    //lblPaidAmo.Text = Convert.ToString(_paidAmount);
                    //lblbalanceAmo.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                    //textBoxAmount.Text = lblbalanceAmo.Text;

                    if (RecieptItemList.Count > 0)
                    {
                        LoadRecieptGrid();
                    }
                    else
                    {
                        LoadRecieptGrid();
                    }

                    //ItemAdded(sender, e);
                    //calculateBankChg = false;

                    base.CHNLSVC.CloseAllChannels();

                    txtAmount.Text = (txtBalanceAmount.Text);

                    //if (grdPaymentDetails.Rows.Count > 0)
                    //    btnCustomerPaymentAdd.Enabled = false;
                    //else
                    //    btnCustomerPaymentAdd.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void ImagebtnDepositBank_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetBankAccounts(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtDepositBank.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBank.Focus();

                ValidateTrue();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void txtBankCard_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //DataTable _dt = base.CHNLSVC.Sales.GetBankCC(comboBoxCCBank.SelectedValue.ToString());
                //if (_dt.Rows.Count > 0)
                //{
                //    comboBoxCardType.DataSource = _dt;
                //    comboBoxCardType.DisplayMember = "mbct_cc_tp";
                //    comboBoxCardType.ValueMember = "mbct_cc_tp";
                //}
                //else
                //{
                //    comboBoxCardType.DataSource = null;
                //}
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //LoadMIDno();
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (string.IsNullOrEmpty(txtBalanceAmount.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "please select the valid pay amount.";
                        return;
                    }
                    if (Convert.ToDouble(txtBalanceAmount.Text) != 0)
                    {
                        divError.Visible = true;
                        lblError.Text = "Payment not completed.";
                        return;
                    }
                    if (string.IsNullOrEmpty(txtDriverCode.Text))
                    {
                        divError.Visible = true;
                        lblError.Text = "Please enter Driver Code.";
                        return;
                    }
                    if (grdPaymentDetails.Rows.Count == 0)
                    {
                        divError.Visible = true;
                        lblError.Text = "Payments are not found.";
                        return;
                    }

                    Int32 row_aff = 0;
                    string _msg = string.Empty;
                    decimal _valPd = 0;
                    ReptPickHeader _SerHeader = new ReptPickHeader();
                    List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();
                    List<VehicalRegistration> _tempRegSave = new List<VehicalRegistration>();
                    List<VehicleInsuarance> _tempInsSave = new List<VehicleInsuarance>();
                    BasePage basePage = new BasePage();

                    if (ddlPaymentType.SelectedItem.Value == "ADVAN")
                    {
                        List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", basePage.GlbUserComCode);
                        if (para.Count > 0)
                        {
                            _valPd = para[0].Hsy_val;
                        }
                    }
                    else if (ddlPaymentType.SelectedItem.Value == "ADINS")
                    {
                        List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADINREMXDT", "COM", basePage.GlbUserComCode);
                        if (para.Count > 0)
                        {
                            _valPd = para[0].Hsy_val;
                        }
                    }

                    //(com_cd, ep, cat_subcd2, first_name2, last_name, nic, tou_lic)
                    DataTable driverTBS = CHNLSVC.Tours.Get_tour_searchDriverTBS(basePage.GlbUserComCode, txtDriverCode.Text, "", "", "", "", "");

                    RecieptHeaderTBS _ReceiptHeader = new RecieptHeaderTBS();
                    _ReceiptHeader.Sir_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
                    _ReceiptHeader.Sir_com_cd = basePage.GlbUserComCode;
                    _ReceiptHeader.Sir_receipt_type = ddlPaymentType.SelectedItem.Value.Trim();
                    _ReceiptHeader.Sir_receipt_no = _ReceiptHeader.Sir_seq_no.ToString();// txtReceiptNo.Text.Trim();//
                    _ReceiptHeader.Sir_prefix = "";//txtDivision.Text.Trim();
                    _ReceiptHeader.Sir_manual_ref_no = "";// txtManualReferance.Text.Trim();
                    _ReceiptHeader.Sir_receipt_date = DateTime.Now.Date;//Convert.ToDateTime(txtDate.Text).Date;
                    _ReceiptHeader.Sir_direct = false;
                    _ReceiptHeader.Sir_acc_no = "";
                    _ReceiptHeader.Sir_is_oth_shop = false;
                    _ReceiptHeader.Sir_oth_sr = "";
                    _ReceiptHeader.Sir_profit_center_cd = basePage.GlbUserDefProf;
                    _ReceiptHeader.Sir_debtor_cd = txtDriverCode.Text.Trim();
                    _ReceiptHeader.Sir_debtor_name = driverTBS.Rows[0]["FIRST NAME"].ToString() + " " + driverTBS.Rows[0]["LAST NAME"].ToString();// txtName.Text.Trim();
                    _ReceiptHeader.Sir_debtor_add_1 = "";// txtAddress.Text.Trim();
                    _ReceiptHeader.Sir_debtor_add_2 = "";// txtAddress2.Text.Trim();
                    _ReceiptHeader.Sir_tel_no = "";
                    _ReceiptHeader.Sir_mob_no = "";// txtMobile.Text.Trim();
                    _ReceiptHeader.Sir_nic_no = driverTBS.Rows[0]["NIC"].ToString();// txtNIC.Text.Trim();
                    _ReceiptHeader.Sir_tot_settle_amt = Convert.ToDecimal(txtTotalAmount.Text);
                    _ReceiptHeader.Sir_comm_amt = 0;
                    _ReceiptHeader.Sir_is_mgr_iss = false;
                    _ReceiptHeader.Sir_esd_rate = 0;
                    _ReceiptHeader.Sir_wht_rate = 0;
                    _ReceiptHeader.Sir_epf_rate = 0;
                    _ReceiptHeader.Sir_currency_cd = "LKR";
                    _ReceiptHeader.Sir_uploaded_to_finance = false;
                    _ReceiptHeader.Sir_act = true;
                    _ReceiptHeader.Sir_direct_deposit_bank_cd = "";
                    _ReceiptHeader.Sir_direct_deposit_branch = "";
                    _ReceiptHeader.Sir_remarks = txtRemark.Text.Trim();
                    _ReceiptHeader.Sir_is_used = false;
                    _ReceiptHeader.Sir_ref_doc = "";
                    _ReceiptHeader.Sir_ser_job_no = "";
                    _ReceiptHeader.Sir_used_amt = 0;
                    _ReceiptHeader.Sir_create_by = Convert.ToString(Session["UserID"]);
                    _ReceiptHeader.Sir_mod_by = "";// Convert.ToString(Session["UserID"]);
                    _ReceiptHeader.Sir_session_id = basePage.GlbUserSessionID;
                    _ReceiptHeader.Sir_anal_1 = "";// ddlDistrict.Text;
                    _ReceiptHeader.Sir_anal_2 = "";// txtProvince.Text.Trim();
                    _ReceiptHeader.Sir_anal_3 = "";
                    _ReceiptHeader.Sir_anal_8 = 0;
                    _ReceiptHeader.Sir_anal_5 = 0;
                    _ReceiptHeader.Sir_anal_6 = 0;
                    _ReceiptHeader.Sir_anal_7 = 0;
                    _ReceiptHeader.Sir_anal_9 = 0;
                    _ReceiptHeader.Sir_VALID_TO = _ReceiptHeader.Sir_receipt_date.AddDays(Convert.ToDouble(_valPd));

                    recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];

                    List<RecieptItemTBS> _ReceiptDetailsSave = new List<RecieptItemTBS>();
                    Int32 _line = 0;
                    foreach (RecieptItemTBS line in RecieptItemList)
                    {
                        line.Sird_seq_no = _ReceiptHeader.Sir_seq_no;
                        _line = _line + 1;
                        line.Sird_line_no = _line;
                        _ReceiptDetailsSave.Add(line);
                    }

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = basePage.GlbUserDefProf;
                    masterAuto.Aut_cate_tp = "PC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "RECEIPT";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "REC";
                    masterAuto.Aut_year = null;

                    DataTable _pcInfo = new DataTable();
                    _pcInfo = CHNLSVC.Sales.GetProfitCenterTable(basePage.GlbUserComCode, basePage.GlbUserDefProf);


                    MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
                    masterAutoRecTp.Aut_cate_cd = basePage.GlbUserDefProf;
                    masterAutoRecTp.Aut_cate_tp = "PC";
                    masterAutoRecTp.Aut_direction = null;
                    masterAutoRecTp.Aut_modify_dt = null;

                    if (_pcInfo.Rows[0]["mpc_ope_cd"].ToString() == "INV_LRP" && basePage.GlbUserComCode == "LRP")
                    {
                        masterAutoRecTp.Aut_moduleid = "REC_LRP";
                    }
                    else
                    {
                        masterAutoRecTp.Aut_moduleid = "RECEIPT";
                    }
                    masterAutoRecTp.Aut_number = 5;//what is Aut_number
                    masterAutoRecTp.Aut_start_char = ddlPaymentType.SelectedItem.Value.Trim();
                    masterAutoRecTp.Aut_year = null;

                    if (grdPaymentDetails.Rows.Count > 0)
                    {
                        _SerHeader.Tuh_usrseq_no = CHNLSVC.Inventory.GetSerialID();  //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "RECEIPT", 0, BaseCls.GlbUserComCode);
                        _SerHeader.Tuh_usr_id = Convert.ToString(Session["UserID"]);
                        _SerHeader.Tuh_usr_com = basePage.GlbUserComCode;
                        _SerHeader.Tuh_session_id = basePage.GlbUserSessionID;
                        _SerHeader.Tuh_cre_dt = DateTime.Now;//Convert.ToDateTime(txtDate.Text).Date;
                        _SerHeader.Tuh_ischek_simitm = true;
                        _SerHeader.Tuh_ischek_reqqty = true;
                        _SerHeader.Tuh_doc_no = "";
                        _SerHeader.Tuh_doc_no = "na";
                        foreach (ReptPickSerials line in _ResList)
                        {
                            line.Tus_usrseq_no = _SerHeader.Tuh_usrseq_no;
                            line.Tus_cre_by = Convert.ToString(Session["UserID"]);
                            _tempSerialSave.Add(line);
                        }
                    }

                    if (ddlPaymentType.SelectedItem.Value == "VHREG")
                    {
                        foreach (VehicalRegistration _reg in _regList)
                        {
                            Int32 _vehSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "VHREG", 1, BaseCls.GlbUserComCode);
                            _reg.P_seq = _vehSeq;
                            _tempRegSave.Add(_reg);
                        }
                    }

                    if (ddlPaymentType.SelectedItem.Value == "VHINS")
                    {
                        foreach (VehicleInsuarance _ins in _insList)
                        {
                            Int32 _insSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "VHINS", 1, BaseCls.GlbUserComCode);
                            _ins.Svit_seq = _insSeq;
                            _ins.Svit_rec_tp = ddlPaymentType.SelectedItem.Value.Trim();
                            _tempInsSave.Add(_ins);
                        }
                    }

                    if (ddlPaymentType.SelectedItem.Value == "ADINS")
                    {
                        foreach (VehicleInsuarance _ins in _insList)
                        {
                            Int32 _insSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "VHINS", 1, BaseCls.GlbUserComCode);
                            _ins.Svit_seq = _insSeq;
                            _ins.Svit_rec_tp = ddlPaymentType.SelectedItem.Value.Trim();
                            _tempInsSave.Add(_ins);
                        }
                    }

                    List<SR_PAY_LOG> sr_pay_logList = new List<SR_PAY_LOG>();
                    SR_PAY_LOG sr_pay_log = new SR_PAY_LOG();
                    DataTable datatable = (DataTable)Session["logsheet"];
                    int count = 0;
                    Int16 pay_dri = 0;
                    string lohNo = "";
                    while (datatable.Rows.Count > count)
                    {

                        sr_pay_log.SPL_SEQ = CHNLSVC.Inventory.GetSerialID();
                        sr_pay_log.SPL_COM = basePage.GlbUserComCode;
                        sr_pay_log.SPL_PC = basePage.GlbUserDefProf;
                        sr_pay_log.SPL_CUS = txtDriverCode.Text;
                        sr_pay_log.SPL_DT = DateTime.Now;
                        sr_pay_log.SPL_REF_NO = datatable.Rows[count]["TLH_LOG_NO"].ToString();
                        sr_pay_log.SPL_AMT = Convert.ToDecimal(datatable.Rows[count]["TLD_TOT"].ToString());
                        sr_pay_log.SPL_ACT = 1;
                        sr_pay_log.SPL_CRE_BY = Convert.ToString(Session["UserID"]); ;
                        sr_pay_log.SPL_CRE_DT = DateTime.Now;
                        //sr_pay_log.SPL_MOD_BY = "";
                        //sr_pay_log.SPL_MOD_DT = "":
                        sr_pay_log.SPL_REC_NO = _ReceiptHeader.Sir_seq_no.ToString();

                        lohNo = datatable.Rows[count]["TLH_LOG_NO"].ToString();
                        pay_dri = 1;

                        sr_pay_logList.Add(sr_pay_log);

                        count = count + 1;
                    }                  
                    string QTNum;
                    row_aff = (Int32)CHNLSVC.Tours.SaveNewReceiptTBS(_ReceiptHeader, _ReceiptDetailsSave, sr_pay_logList, masterAuto, _SerHeader, _tempSerialSave, _tempRegSave, _tempInsSave, _sheduleDetails, masterAutoRecTp, _gvDetails, lohNo, pay_dri, out QTNum);

                    if (row_aff == 1)
                    {
                        PageClear();
                        divSuccess.Visible = true;
                        lblSuccess.Text = "Successfully created.Receipt No: '" + QTNum + "'";
                        txtPaymentNo.Text = QTNum;
                        btnSave.Enabled = false;
                        txtPaymentNo.Enabled = false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(QTNum))
                        {
                            divError.Visible = true;
                            lblError.Text = QTNum;
                            return;
                        }
                        else
                        {
                            divError.Visible = true;
                            lblError.Text = "Creation Fail.";
                            return;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    divError.Visible = true;
                    lblError.Text = "Process Terminated.";
                    return;
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                int seq = (Int32)ViewState["seq_no"];
                string logNo = "";
                if (grdLogSheet.Rows.Count > 0)
                    logNo = grdLogSheet.Rows[0].Cells[0].Text.ToString();
                string receiptNo = txtPaymentNo.Text;
                int eff = CHNLSVC.Tours.Cancel_paymentTBS(seq, logNo, receiptNo);

                if (eff > 0)
                {
                    PageClear();
                    divSuccess.Visible = true;
                    lblSuccess.Text = "Successfully canceled Receipt No: '" + receiptNo + "'";
                }
                else
                {
                    divError.Visible = true;
                    lblError.Text = "Cannot cancel Receipt.";
                    return;                              
                }
                
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblError.Text = ex.Message;
            }
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


        private void LoadPaymenttypes()
        {
            ddlPaymentType.DataSource = CHNLSVC.Tours.Get_tour_searchreceipttype(base.GlbUserComCode, 1);
            ddlPaymentType.DataTextField = "MSRT_DESC";
            ddlPaymentType.DataValueField = "MSRT_CD";
            ddlPaymentType.DataBind();

            if (ddlPaymentType.SelectedItem.Value == "TPAY")
            {
                btnSearch.Enabled = true;
            }
            if (ddlPaymentType.SelectedItem.Value == "ADPAY")
            {
                btnSearch.Enabled = false; 
                grdLogSheet.DataSource = new int[] { };
                grdLogSheet.DataBind();
            }
        }
        private void LoadPayMode()
        {
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(base.GlbUserComCode, base.GlbChannel, GlbUserDefProf, ddlPaymentType.SelectedItem.Value, DateTime.Now.Date);
            ddlPayMode.DataSource = _paymentTypeRef;
            ddlPayMode.DataTextField = "Stp_pay_tp";
            ddlPayMode.DataValueField = "Stp_pay_tp";
            ddlPayMode.DataBind();
        }
        private void AddPayment()
        {
            string invoiceType = ddlPaymentType.SelectedItem.Value;

            if (Session["recieptItem"] == null)
            {
                recieptItem = new List<RecieptItemTBS>();
            }
            else
            {
                recieptItem = (List<RecieptItemTBS>)Session["recieptItem"];
            }          

            Decimal BankOrOtherCharge_ = 0;
            Decimal BankOrOther_Charges = 0;
            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                decimal _selectAmt = Convert.ToDecimal(txtAmount.Text);

                List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), GlbUserDefProf, ddlPaymentType.SelectedItem.Value, DateTime.Now.Date);
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                    {
                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge_ = (Convert.ToDecimal(txtAmount.Text) - BCV) * BCR / (BCR + 100);
                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                        BankOrOther_Charges = BankOrOtherCharge_;
                        txtAmount.Text = FormatToCurrency(Convert.ToString(_selectAmt - BankOrOther_Charges));
                    }
                }
            }

            if (Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtAmount.Text) < 0)
            {
                divError.Visible = true;
                lblError.Text = "Please select the valid pay amount.";
                return;
            }

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPeriod.Text))
                {
                    divError.Visible = true;
                    lblError.Text = "Please select the valid period.";
                    return;
                }
                if (Convert.ToInt32(txtPeriod.Text) <= 0)
                {
                    divError.Visible = true;
                    lblError.Text = "Please select the valid period.";
                    return;                    
                }
            }

            if (string.IsNullOrEmpty(txtPeriod.Text))
                _period = 0;
            else 
                _period = Convert.ToInt32(txtPeriod.Text);

            _payAmount = Convert.ToDecimal(txtAmount.Text);

            if (recieptItem.Count <= 0)
            {
                RecieptItemTBS _item = new RecieptItemTBS();

                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                    _cardno = txtCardNo.Text;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    //_cardno = txtPayAdvReceiptNo.Text;
                    //chkPayCrPromotion.Checked = false;
                    //_period = 0;
                    //txtPayCrCardType.Text = string.Empty;
                    //txtPayCrBranch.Text = string.Empty;
                    //txtPayCrBank.Text = string.Empty;
                }

                _item.Sird_cc_is_promo = chkPromotion.Checked ? true : false;
                _item.Sird_cc_period = _period;

                _item.Sird_deposit_bank_cd = txtDepositBank.Text;
                _item.Sird_deposit_branch = txtDepositBranch.Text;

                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    _item.Sird_credit_card_bank = txtBankCard.Text;
                    _item.Sird_chq_branch = txtBranchCard.Text;
                    _item.Sird_ref_no = txtCardNo.Text;
                    _item.Sird_cc_tp = ddlCardType.SelectedItem.Text;

                    if (!string.IsNullOrEmpty(txtExpireCard.Text))
                    {
                        _item.Sird_cc_expiry_dt = Convert.ToDateTime(txtExpireCard.Text).Date;
                    }
                }
                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    _item.Sird_chq_bank_cd = txtBankCheque.Text;
                    _item.Sird_chq_branch = txtBranchCheque.Text;
                    _item.Sird_ref_no = txtChequeNo.Text;
                }
                if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                    if (_bankAccounts == null)
                    {
                        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Bank not found for code');", true);
                        //return;
                        divError.Visible = true;
                        lblError.Text = "Bank not found for code.";
                        return; 
                    }

                    if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        if (string.IsNullOrEmpty(txtBranchCheque.Text))
                        {
                            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter cheque branch');", true);
                            //txtBranchCheque.Focus();
                            //return;
                            divError.Visible = true;
                            lblError.Text = "Please enter cheque branch.";
                            return; 
                        }

                        if (txtChequeNo.Text.Length != 6)
                        {
                            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter correct cheque number. [Cheque number should be 6 numbers.]');", true);
                            //txtChequeNo.Focus();
                            //return;
                            divError.Visible = true;
                            lblError.Text = "Please enter correct cheque number. [Cheque number should be 6 numbers.].";
                            return; 
                        }

                        _item.Sird_chq_dt = Convert.ToDateTime(txtChequeDate.Text);
                        _item.Sird_anal_5 = Convert.ToDateTime(txtChequeDate.Text);
                    }

                    _item.Sird_chq_bank_cd = txtBankCheque.Text;
                    _item.Sird_chq_branch = txtBranchCheque.Text;


                    //_item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd; //comboBoxChqBank.SelectedValue.ToString();
                    //_item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                    _item.Sird_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                    _item.Sird_deposit_branch = txtDepositBranchCheque.Text;
                    _item.Sird_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNo.Text;
                    //_item.Sard_anal_5 = dateTimePickerExpire.Value.Date;

                    //bank = textBoxChqBank.Text;
                    //branch = textBoxChqBranch.Text;
                    //depBank = textBoxChqDepBank.Text; ;
                    //depBranch = textBoxChqDepBranch.Text;
                    //chqNo = textBoxChequeNo.Text;
                    //chqExpire = dateTimePickerExpire.Value.Date;
                    //NEED CHEQUE DATE

                    //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                    //SARD_CHQ_DT NOT IN BO

                }

                //_item.Sard_credit_card_bank = txtBankCard.Text;
                //_item.Sard_chq_branch = txtBranchCard.Text;
                //_item.Sard_ref_no = txtCardNo.Text;    

                //_item.Sird_inv_no = txtInvoice.Text;
                _item.Sird_inv_no = "";

                _item.Sird_pay_tp = ddlPayMode.SelectedValue.ToString();
                _item.Sird_settle_amt = Convert.ToDecimal(_payAmount);
                _item.Sird_anal_3 = Math.Round(BankOrOther_Charges, 2);
                _item.Sird_rmk = txtRemark.Text;
                _paidAmount += _payAmount;

                recieptItem.Add(_item);
            }
            else
            {
                bool _isDuplicate = false;

                var _duplicate = from _dup in recieptItem
                                 where _dup.Sird_pay_tp == ddlPayMode.SelectedValue.ToString()
                                 select _dup;
                if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    //var _dup_crcd = from _dup in _duplicate
                    //                where _dup.Sard_cc_tp == ddlCardType.Text && _dup.Sard_ref_no == txtCardNo.Text
                    //                select _dup;
                    //if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    //var _dup_chq = from _dup in _duplicate
                    //               where _dup.Sard_chq_bank_cd == txtBankCard.Text && _dup.Sard_ref_no == txtCardNo.Text
                    //               select _dup;
                    //if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {
                    //var _dup_adv = from _dup in _duplicate
                    //               where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                    //               select _dup;
                    //if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    //string _loyalyno = "";
                    //if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();

                    //var _dup_lore = from _dup in _duplicate
                    //                where _dup.Sard_ref_no == _loyalyno
                    //                select _dup;
                    //if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    //var _dup_adv = from _dup in _duplicate
                    //               where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                    //               select _dup;
                    //if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (_isDuplicate == false)
                {
                    //No Duplicates
                    RecieptItemTBS _item = new RecieptItemTBS();


                    if (string.IsNullOrEmpty(txtPeriod.Text.Trim()))
                        txtPeriod.Text = "0";

                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                    {
                        //chkPayCrPromotion.Checked = false;
                        //_period = 0;
                        //txtPayCrCardType.Text = string.Empty;
                        //txtPayCrBranch.Text = string.Empty;
                        //txtPayCrBank.Text = string.Empty;
                    }
                    if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                        if (_bankAccounts == null)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Bank not found for code');", true);
                            return;
                        }

                        if (ddlPayMode.SelectedItem.Text == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            if (string.IsNullOrEmpty(txtBranchCheque.Text))
                            {
                                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter cheque branch');", true);
                                txtBranchCheque.Focus();
                                return;
                            }

                            if (txtChequeNo.Text.Length != 6)
                            {
                                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter correct cheque number. [Cheque number should be 6 numbers.]');", true);
                                txtChequeNo.Focus();
                                return;
                            }


                            _item.Sird_chq_bank_cd = txtBankCheque.Text;
                            _item.Sird_chq_branch = txtBranchCheque.Text;


                            _item.Sird_chq_dt = Convert.ToDateTime(txtChequeDate.Text);
                            //_item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd; //comboBoxChqBank.SelectedValue.ToString();
                            //_item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                            _item.Sird_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                            _item.Sird_deposit_branch = txtDepositBranchCheque.Text;
                            _item.Sird_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNo.Text;
                            _item.Sird_anal_5 = Convert.ToDateTime(txtChequeDate.Text);

                            //bank = textBoxChqBank.Text;
                            //branch = textBoxChqBranch.Text;
                            //depBank = textBoxChqDepBank.Text; ;
                            //depBranch = textBoxChqDepBranch.Text;
                            //chqNo = textBoxChequeNo.Text;
                            //chqExpire = dateTimePickerExpire.Value.Date;
                            //NEED CHEQUE DATE

                            //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                            //SARD_CHQ_DT NOT IN BO
                        }

                    }
                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        _item.Sird_credit_card_bank = txtBankCard.Text;
                        _item.Sird_chq_branch = txtBranchCard.Text;
                        _item.Sird_ref_no = txtCardNo.Text;
                        _item.Sird_cc_tp = ddlCardType.SelectedItem.Text;

                        if (!string.IsNullOrEmpty(txtExpireCard.Text))
                        { _item.Sird_cc_expiry_dt = Convert.ToDateTime(txtExpireCard.Text).Date; }

                    }
                    _item.Sird_cc_is_promo = chkPromotion.Checked ? true : false;
                    _item.Sird_cc_period = Convert.ToInt32(txtPeriod.Text);
                    _item.Sird_cc_period = _period;

                    //_item.Sird_inv_no = txtInvoice.Text;
                    _item.Sird_inv_no = "";

                    //_item.Sard_chq_bank_cd = txtBankCheque.Text;
                    //_item.Sard_chq_branch = txtBranchCheque.Text;
                    //_item.Sard_credit_card_bank = null;
                    _item.Sird_deposit_bank_cd = txtDepositBank.Text;
                    _item.Sird_deposit_branch = txtDepositBranch.Text;
                    _item.Sird_pay_tp = ddlPayMode.SelectedValue.ToString();
                    _item.Sird_settle_amt = Convert.ToDecimal(_payAmount);
                    _item.Sird_anal_3 = Math.Round(BankOrOther_Charges, 2);
                    _paidAmount += _payAmount;
                    recieptItem.Add(_item);
                }
                else
                {
                    divError.Visible = true;
                    lblError.Text = "You can not add duplicate payments.";
                    return; 
                    //DisplayMessages("You can not add duplicate payments");
                    //return;
                }
            }

            Session["recieptItem"] = recieptItem;
            grdPaymentDetails.DataSource = recieptItem;
            grdPaymentDetails.DataBind();

            //txtPaidAmount.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtPaidAmount.Text) + Convert.ToDecimal(_paidAmount)));
            //txtBalanceAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(_paidAmount))));
            //txtAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(_paidAmount))));
            txtBalanceAmount.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(_paidAmount)));
            txtAmount.Text = txtBalanceAmount.Text;

            txtRemark.Text = "";
            txtCardNo.Text = "";
            txtBankCard.Text = "";
            txtBranchCard.Text = "";
            //ddlCardType.SelectedIndex = 0;
            txtExpireCard.Text = "";
            chkPromotion.Checked = false;
            //txtPayAdvReceiptNo.Text = "";
            //txtPayAdvRefAmount.Text = "";
            txtPeriod.Text = "";
            // txtPayCrPeriod.Enabled = false;

            txtDepositBank.Text = string.Empty;
            txtDepositBranch.Text = string.Empty;
        }
        public void LoadRecieptGrid()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("SIRD_PAY_TP");
            dt.Columns.Add("SIRD_INV_NO");
            dt.Columns.Add("sird_chq_bank_cd");
            dt.Columns.Add("sird_chq_branch");
            dt.Columns.Add("sird_cc_tp");
            dt.Columns.Add("sird_anal_3");
            dt.Columns.Add("sird_settle_amt", typeof(decimal));
            dt.Columns.Add("Sird_ref_no");
            dt.Columns.Add("sird_anal_1");
            dt.Columns.Add("sird_anal_4");
            dt.Columns.Add("Sird_cc_period");

            recieptItemList = (List<RecieptItemTBS>)Session["recieptItem"];

            if (recieptItemList != null)
            {
                foreach (RecieptItemTBS ri in RecieptItemList)
                {
                    DataRow dr = dt.NewRow();
                    if (ri.Sird_pay_tp == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {

                        dr[0] = ri.Sird_pay_tp;
                        dr[1] = ri.Sird_inv_no;
                        dr[2] = ri.Sird_chq_bank_cd;
                        dr[3] = ri.Sird_chq_branch;
                        dr[4] = ri.Sird_cc_tp;
                        dr[5] = ri.Sird_anal_3;
                        dr[6] = ri.Sird_settle_amt;
                        dr[7] = ri.Sird_ref_no;
                        dr[8] = ri.Sird_anal_1;
                        dr[9] = ri.Sird_anal_4;
                    }
                    else if (ri.Sird_pay_tp == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        dr[0] = ri.Sird_pay_tp;
                        dr[1] = ri.Sird_inv_no;
                        dr[2] = ri.Sird_credit_card_bank;
                        dr[3] = ri.Sird_chq_branch;
                        dr[4] = ri.Sird_cc_tp;
                        dr[5] = ri.Sird_anal_3;
                        dr[6] = ri.Sird_settle_amt;
                        dr[7] = ri.Sird_ref_no;
                        dr[8] = ri.Sird_anal_1;
                        dr[9] = ri.Sird_anal_4;
                        dr[10] = ri.Sird_cc_period;
                    }
                    else
                    {
                        dr[0] = ri.Sird_pay_tp;
                        dr[1] = ri.Sird_inv_no;
                        dr[2] = ri.Sird_deposit_bank_cd;
                        dr[3] = ri.Sird_deposit_branch;
                        dr[4] = ri.Sird_cc_tp;
                        dr[5] = ri.Sird_anal_3;
                        dr[6] = ri.Sird_settle_amt;
                        dr[7] = ri.Sird_ref_no;
                        dr[8] = ri.Sird_anal_1;
                        dr[9] = ri.Sird_anal_4;
                    }
                    dt.Rows.Add(dr);
                }
            }

            grdPaymentDetails.AutoGenerateColumns = false;
            grdPaymentDetails.DataSource = dt;
            grdPaymentDetails.DataBind();

        }
        private void DisplayMessages(string message)
        {
            try
            {
                divError.Visible = true;
                lblError.Text = message;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }
        private bool CheckBank(string bank, Label lbl)
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(bank))
            {
                _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());

                if (_bankAccounts.Mbi_cd != null)
                {
                    if (_paymentTypeRef == null)
                    {
                        List<PaymentType> _paymentTypeRef1 = base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(base.GlbUserComCode, Session["UserSubChannl"].ToString(), base.GlbUserDefProf, ddlPaymentType.SelectedItem.Value, DateTime.Now.Date);
                        _paymentTypeRef = _paymentTypeRef1;
                    }
                    if (_paymentTypeRef == null)
                    {
                        divError.Visible = true;
                        lblError.Text = "invalid pay mode.";
                        return false;
                    }
                    if (_paymentTypeRef.Count <= 0)
                    {
                        List<PaymentType> _paymentTypeRef1 = base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(base.GlbUserComCode, Session["UserSubChannl"].ToString(), base.GlbUserDefProf, ddlPaymentType.SelectedItem.Value, DateTime.Now.Date);
                        _paymentTypeRef = _paymentTypeRef1;
                    }

                    var _promo = (from _prom in _paymentTypeRef
                                  where _prom.Stp_pay_tp == ddlPayMode.SelectedValue.ToString()
                                  select _prom).ToList();

                    foreach (PaymentType _type in _promo)
                    {
                        if (_type.Stp_pd != null && _type.Stp_pd > 0 && _type.Stp_bank == txtBankCard.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
                        {
                            pnlPermotion.Visible = true;
                            chkPromotion.Checked = false;
                        }
                    }
                    lbl.Text = _bankAccounts.Mbi_desc;
                    return true;
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select the valid bank.');", true);
                    return false;
                }
            }
            return false;

        }
        protected void LoadCardType(string bank)
        {
            MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());
            if (_bankAccounts != null)
            {
                DataTable _dt = base.CHNLSVC.Sales.GetBankCC(_bankAccounts.Mbi_cd);
                if (_dt.Rows.Count > 0)
                {
                    ddlCardType.DataSource = _dt;
                    ddlCardType.DataTextField = "mbct_cc_tp";
                    ddlCardType.DataValueField = "mbct_cc_tp";
                    ddlCardType.DataBind();
                }
                else
                {
                    ddlCardType.DataSource = null;
                }

                var dr = _dt.AsEnumerable().Where(x => x["MBCT_CC_TP"].ToString() == "VISA");

                if (dr.Count() > 0)
                    ddlCardType.SelectedValue = "VISA";
            }
        }
        private void LoadMIDno()
        {
            int mode = 0;
            lblPromotion.ForeColor = System.Drawing.Color.Black;
            string branch_code = "";
            string pc = base.GlbUserDefProf;
            //string MIDcode = "";
            int period = 0;
            //if (rdooffline.Checked == true) mode = 0;
            //if (rdoonline.Checked == true) mode = 1;
            //if (textBoxCCBank.Text.Length > 0) branch_code = textBoxCCBank.Text;
            //if (txtPromo.Text.Length > 0) period = Convert.ToInt32(txtPromo.Text);
            DataTable MID = base.CHNLSVC.Sales.get_bank_mid_code(branch_code, pc, mode, period);
            if (MID.Rows.Count > 0)
            {
                DataRow dr;

                dr = MID.Rows[0];
                lblPromotion.Text = dr["MPM_MID_NO"].ToString();
                lblPromotion.Visible = true;
            }
            else
            {
                lblPromotion.Visible = false;
                lblPromotion.Text = "";
                lblPromotion.Text = "No MID code";
                lblPromotion.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void BindSaveReceiptDetails(string _RecNo)
        {
            RecieptItemTBS _paramRecDetails = new RecieptItemTBS();

            _paramRecDetails.Sird_receipt_no = _RecNo;
            _list = new List<RecieptItemTBS>();
            _list = CHNLSVC.Sales.GetReceiptDetailsTBS(_paramRecDetails);
            grdPaymentDetails.DataSource = _list;
            grdPaymentDetails.DataBind();

        }

        private void LoadPaymenttypesddd()
        {
            //List<PaymentType> _paymentTypeRef = basePage.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), Session["UserDefProf"].ToString(), txtReceiptType.Text, DateTime.Now);
            //List<string> payTypes = new List<string>();
            ////payTypes.Add("");
            //if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            //{
            //    foreach (PaymentType pt in _paymentTypeRef)
            //    {
            //        payTypes.Add(pt.Stp_pay_tp);
            //    }
            //}
            //ddlPayMode.DataSource = payTypes;
            //ddlPayMode.DataBind();
            //ddlPayMode.SelectedIndex = 0;

            //if (payTypes[0].ToString().ToUpper() == "CASH")
            //{
            //    mltPaymentDetails.ActiveViewIndex = 0;
            //}
            //else if (payTypes[0].ToString().ToUpper() == "CRCD")
            //{
            //    mltPaymentDetails.ActiveViewIndex = 1;
            //}
            //else if (payTypes[0].ToString().ToUpper() == "CHEQUE")
            //{
            //    mltPaymentDetails.ActiveViewIndex = 2;
            //}
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
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBank:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator + ddlPayMode.SelectedItem.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                            if (_bankAccounts != null)
                            {
                                paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                            }
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBankBranch:
                    {
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            paramsText.Append(txtBankCheque.Text.Trim() + seperator);
                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            paramsText.Append(txtBankCard.Text.Trim() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentDate:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf);
                        break;
                    }
            }

            return paramsText.ToString();
        }
        private bool ValidateSave()
        {
            if (string.IsNullOrEmpty(txtDriverCode.Text))
            {
                divError.Visible = true;
                lblError.Text = "Please enter Driver Code.";
                return false;
            }
            else
            {
                return true;    
            }            
        }
        private void ValidateTrue()
        {
            divError.Visible = false;
            lblError.Text = "";
            divSuccess.Visible = false;
            lblSuccess.Text = "";
        }
        private void PageClear()
        {
            txtProcessDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtPaymentFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtPaymentToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtDriverCode.Text = string.Empty;
            txtDriverCode.Enabled = true;
            txtPaymentNo.Text = string.Empty;
            txtPaymentNo.Enabled = true;
            txtTotalAmount.Text = string.Empty;
            txtTotalAmount.Enabled = false;
            txtBalanceAmount.Text = string.Empty;
            txtAmount.Text = string.Empty;

            ImgPaymentNoserch.Enabled = true;
            imgbtnPaymentNo.Enabled = true;

            ddlPaymentType.Enabled = true;
            ddlPaymentType.SelectedItem.Value = "TPAY";
            btnSearch.Enabled = true;

            Session["logsheet"] = null;

            btnCancel.Enabled = false;

            txtRemarkDetails.Enabled = false;

            grdLogSheet.DataSource = new int[] { };
            grdLogSheet.DataBind();  

            ddlPayMode.SelectedIndex = 0;
            txtAmount.Text = string.Empty;
            txtDepositBank.Text = string.Empty;
            txtDepositBranch.Text = string.Empty;

            grdPaymentDetails.DataSource = new int[] { };
            grdPaymentDetails.DataBind();            

            txtCardNo.Text = string.Empty;
            txtBankCard.Text = string.Empty;
            txtBranchCard.Text = string.Empty;
            lblbank.Text = string.Empty;
            if (ddlCardType.Items.Count > 0)
                ddlCardType.SelectedIndex = 0;
            txtExpireCard.Text = string.Empty;
            txtDepositBankCard.Text = string.Empty;
            txtDepositBranchCard.Text = string.Empty;
            chkPromotion.Checked = false;
            txtPeriod.Text = string.Empty;

            txtChequeNo.Text = string.Empty;
            txtBankCheque.Text = string.Empty;
            txtBranchCheque.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtDepositBankCheque.Text = string.Empty;
            txtDepositBranchCheque.Text = string.Empty;

            ValidateTrue();

        }
 
    }
}