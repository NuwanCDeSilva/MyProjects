using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours.UserControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;

namespace FF.AbansTours
{
    public partial class ReceiptEnrty : BasePage
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

        private List<RecieptItemTBS> _recieptItem;
        private decimal _paidAmount;
        List<RecieptItemTBS> recieptItemList;
        List<PaymentType> _paymentTypeRef;
        public List<RecieptItemTBS> RecieptItemList
        {
            get { return recieptItemList; }
            set { recieptItemList = value; }
        }

        #region Event handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Load_District();

                    Clear_Data();
                    GetProvince();

                    loadPaymenttypes();
                    ReceiptTypeChange();
                }

                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MessageBoxButtons.YesNo", "alert('Successfully created.Receipt No: ');", true);
                //grdPaymentDetails.DataSource = new int[] { };
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void txtReceiptType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                loadPaymenttypes();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void ImgbtnReceiptType_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Clear_Data();

                txtCode.Text = string.Empty;

                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetReceiptTypes(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtReceiptType.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtReceiptType.Focus();

                loadPaymenttypes();

            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void txtReceiptNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                detailsbyreceiptno();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void ImgReceiptNo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReceiptType.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select receipt type.');", true);
                    txtReceiptType.Focus();
                    return;
                }

                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptDate);

                DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetReceiptsDateTBS(ucc.SearchParams, null, null, Convert.ToDateTime(txtDate.Text).AddMonths(-1), Convert.ToDateTime(txtDate.Text));

                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtReceiptNo.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = true;
                ucc.DateEnable(enable);
                txtReceiptNo.Focus();

                Clear_Data();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void ImgReceiptNoserch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                detailsbyreceiptno();
                btnPrint.Enabled = true;
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void ddlDistrict_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetProvince();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void imgbtnCode_Click(object sender, ImageClickEventArgs e)
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
                ucc.ReturnResultControl = txtCode.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtCode.Focus();

                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtNIC.Text = string.Empty;
                txtMobile.Text = string.Empty;
                ddlDistrict.SelectedIndex = 0;
                txtProvince.Text = string.Empty;
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void imgbtncodeseach_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LoadCustomerDetails();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetails();
                GetProvince();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void chkIsManual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIsManual.Checked == true)
                {
                    txtManualReferance.Text = string.Empty;
                    txtManualReferance.Enabled = true;
                }
                else
                {
                    txtManualReferance.Text = string.Empty;
                    txtManualReferance.Enabled = false;
                }
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void txtManualReferance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIsManual.Checked == true)
                {
                    if (IsNumeric(txtManualReferance.Text, System.Globalization.NumberStyles.Integer))
                    {
                        Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(base.GlbUserComCode, base.GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManualReferance.Text));
                        if (_IsValid == false)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid manual document number.');", true);
                            txtManualReferance.Text = "";
                            txtManualReferance.Focus();
                            return;
                        }
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid manual document number.');", true);
                    }
                }
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }

        }

        protected void btnCustomerPaymentAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoice.Text) && string.IsNullOrEmpty(txtInvoiceAmount.Text))
                {
                    if (Convert.ToDouble(txtInvoiceAmount.Text) < Convert.ToDouble(txtCustomerPayment.Text))
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Payment cannot exceed Invoice amount.');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtCustomerPayment.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter amount which customer is going to pay.');", true);
                    txtCustomerPayment.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select payment customer.');", true);
                    txtCode.Focus();
                    return;
                }

                if (txtReceiptType.Text == "ADVAN")
                {
                    if (grdPaymentDetails.Rows.Count > 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Payments are already added. Now you cannot add more details.');", true);
                        return;
                    }
                }
                //Add Pemil 
                if (txtReceiptType.Text == "DEBT" && txtInvoiceAmount.Text == "0.00")
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invoice Amount cannot be zero.');", true);
                }
                if (txtReceiptType.Text == "DEBT")
                {
                    if (Convert.ToDecimal(txtCustomerPayment.Text) > Convert.ToDecimal(txtInvoiceAmount.Text))
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Payment cannot exceed outstanding amount.');", true);
                        txtCustomerPayment.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtCustomerPayment.Text) <= 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Settle amount cannot be zero.');", true);
                        txtCustomerPayment.Text = "";
                        txtCustomerPayment.Focus();
                        return;
                    }
                }

                txtReceiptType.Enabled = false;
                ImgbtnReceiptType.Enabled = false;


                //if IsNumeric(txtBalanceAmount.Text)
                //{
                Double result;
                if (Double.TryParse(txtBalanceAmount.Text, out result))
                    //txtBalanceAmount.Text = (Convert.ToDouble(txtBalanceAmount.Text) + Convert.ToDouble(txtCustomerPayment.Text)).ToString("N2");
                    txtBalanceAmount.Text = (Convert.ToDouble(txtCustomerPayment.Text)).ToString("N2");
                else
                    txtBalanceAmount.Text = (Convert.ToDouble(txtCustomerPayment.Text)).ToString("N2");
                //}
                txtAmount.Text = txtBalanceAmount.Text;
                txtTotalReceiptAmount.Text = txtBalanceAmount.Text;
                txtCustomerPayment.Text = string.Empty;
                loadPaymenttypes();

            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }

        }

        protected void ImgAmount_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (ddlPayMode.SelectedItem.Text.ToUpper() == "CHEQUE" && string.IsNullOrEmpty(txtChequeDate.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cheque Date is required.');", true);
                    return;
                }


                AddPayment();
                recieptItemList = (List<RecieptItemTBS>)Session["_recieptItem"];
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

                btnCustomerPaymentAdd.Enabled = false;

            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
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
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
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
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
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
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
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
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
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
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void imgbtnInvoice_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable _result = null;
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");

                if (txtReceiptType.Text == "DEBT")
                {

                    if (string.IsNullOrEmpty(txtCode.Text))
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select customer.');", true);
                        txtCode.Focus();
                        return;
                    }

                    //ucc.ReturnIndex = 0;
                    if (chkIsManual.Checked == true)
                    {
                        ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInvOth);
                        _result = CHNLSVC.CommonSearch.GetOutstandingInvoiceTBS(ucc.SearchParams, null, null);
                    }
                    else
                    {
                        ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInv);
                        _result = CHNLSVC.CommonSearch.GetOutstandingInvoiceTBS(ucc.SearchParams, null, null);
                    }
                    //_CommonSearch.dvResult.DataSource = _result;
                    //_CommonSearch.BindUCtrlDDLData(_result);
                    //_CommonSearch.obj_TragetTextBox = txtInvoice;
                    //_CommonSearch.ShowDialog();
                    ucc.BindUCtrlDDLData(_result);
                    ucc.BindUCtrlGridData(_result);
                    ucc.ReturnResultControl = txtInvoice.ClientID;
                    ucc.UCModalPopupExtender.Show();
                    bool enable = false;
                    ucc.DateEnable(enable);
                    txtInvoice.Focus();
                }
                else if ((txtReceiptType.Text == "VHREG") || (txtReceiptType.Text == "VHINS") || (txtReceiptType.Text == "ADINS"))
                {
                    if (string.IsNullOrEmpty(txtCode.Text))
                    {
                        //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        //ucc.ReturnIndex = 0;
                        ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
                        _result = CHNLSVC.CommonSearch.GetAllInvoiceSearchData(ucc.SearchParams, null, null);
                        //_CommonSearch.dvResult.DataSource = _result;
                        //_CommonSearch.BindUCtrlDDLData(_result);
                        //_CommonSearch.obj_TragetTextBox = txtInvoice;
                        //_CommonSearch.ShowDialog();
                        ucc.BindUCtrlDDLData(_result);
                        ucc.BindUCtrlGridData(_result);
                        ucc.ReturnResultControl = txtReceiptNo.ClientID;
                        ucc.UCModalPopupExtender.Show();
                        bool enable = false;
                        ucc.DateEnable(enable);
                        txtInvoice.Focus();
                    }
                    else
                    {
                        //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        //_CommonSearch.ReturnIndex = 0;
                        ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceByCus);
                        _result = CHNLSVC.CommonSearch.GetInvoicebyCustomer(ucc.SearchParams, null, null);
                        //_CommonSearch.dvResult.DataSource = _result;
                        //_CommonSearch.BindUCtrlDDLData(_result);
                        //_CommonSearch.obj_TragetTextBox = txtInvoice;
                        //_CommonSearch.ShowDialog();
                        ucc.BindUCtrlDDLData(_result);
                        ucc.BindUCtrlGridData(_result);
                        ucc.ReturnResultControl = txtReceiptNo.ClientID;
                        ucc.UCModalPopupExtender.Show();
                        bool enable = false;
                        ucc.DateEnable(enable);
                        txtInvoice.Focus();
                    }

                    //BasePage basepage = new BasePage();
                    //Page page = (Page)this.Page;
                    //uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                    //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptDate);
                    //DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetReceiptsDate(ucc.SearchParams, null, null, Convert.ToDateTime(txtDate.Text).AddMonths(-1), Convert.ToDateTime(txtDate.Text));
                    //ucc.BindUCtrlDDLData(dataSource);
                    //ucc.BindUCtrlGridData(dataSource);
                    //ucc.ReturnResultControl = txtReceiptNo.ClientID;
                    //ucc.UCModalPopupExtender.Show();
                    //txtReceiptNo.Focus();                   

                }
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void imgbtnInvoicedetals_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                invoicedetails();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void imgbtnSalesExecutive_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

                //_CommonSearch.ReturnIndex = 0;
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(ucc.SearchParams, null, null);
                //if (_result == null || _result.Rows.Count <= 0)
                //{
                ////_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                ////_result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                //}
                //ucc.dvResult.DataSource = _result;
                //ucc.BindUCtrlDDLData(_result);
                //ucc.obj_TragetTextBox = txtSalesExecutive.ClientID;
                //ucc.ShowDialog();
                //txtSalesExecutive.Focus();
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtSalesExecutive.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtSalesExecutive.Focus();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Error Occurred while processing...\n');", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void imgbtnDepositBankCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 3;
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable _result = base.CHNLSVC.CommonSearch.searchDepositBankCode(ucc.SearchParams, null, null);
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BiUndCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = textBoxCCDepBank;
                //_CommonSearch.ShowDialog();
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtDepositBankCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBankCard.Focus();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert(' " + ex.Message + " ');", true);
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }

        protected void imgbtnDepositBranchCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 1;
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
                DataTable _result = base.CHNLSVC.CommonSearch.SearchBankBranchData(ucc.SearchParams, null, null);
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = textBoxCCDepBranch;
                //_CommonSearch.ShowDialog();
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtDepositBranchCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtDepositBranchCard.Focus();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert(' " + ex.Message + " ');", true);
            }
            finally
            {
                base.CHNLSVC.CloseAllChannels();
            }
        }

        protected void imgbtntxtBankCard_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 0;
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = base.CHNLSVC.CommonSearch.GetBusinessCompany(ucc.SearchParams, null, null);
                //ucc.IsSearchEnter = true;
                //ucc.dvResult.DataSource = _result;
                //ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlDDLData(_result);
                ucc.BindUCtrlGridData(_result);
                ucc.ReturnResultControl = txtBankCard.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                //ucc.ShowDialog();
                txtBankCard.Focus();
                LoadCardType(txtBankCard.Text);
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert(' " + ex.Message + " ');", true);
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
                if (!string.IsNullOrEmpty(txtBankCard.Text))
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
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Amount is required.');", true);
                        }
                    }
                }
                //LoadBankChg();
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert(' " + ex.Message + " ');", true);
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
                    recieptItemList = (List<RecieptItemTBS>)Session["_recieptItem"];
                    RecieptItemList.RemoveAt(Convert.ToInt32(e.CommandArgument));

                    Session["_recieptItem"] = recieptItemList;

                    _paidAmount = 0;
                    foreach (RecieptItemTBS _list in RecieptItemList)
                    {
                        _paidAmount += _list.Sird_settle_amt;
                    }

                    GridViewRow selectedRow = grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)];
                    //txtPaidAmount.Text = (Convert.ToDouble(txtPaidAmount.Text) - Convert.ToDouble((grdPaymentDetails.Rows[Convert.ToInt32(e.CommandArgument)]).Cells[19])).ToString();
                    txtPaidAmount.Text = FormatToCurrency((Convert.ToDouble(txtPaidAmount.Text) - Convert.ToDouble(selectedRow.Cells[18].Text)).ToString());
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

                    if (grdPaymentDetails.Rows.Count > 0)
                        btnCustomerPaymentAdd.Enabled = false;
                    else
                        btnCustomerPaymentAdd.Enabled = true;

                }
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadMIDno();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReceiptNo.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select receipt number.');", true);
                    txtReceiptNo.Focus();
                    return;
                }

                if (Convert.ToBoolean(ViewState["_RecStatus"]) == false)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('This receipt is already cancelled.');", true);
                    return;
                }

                if (_sunUpload == true)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot cancel.Already uploaded to accounts.');", true);
                    return;
                }

                if (txtReceiptType.Text == "ADVAN")
                {
                    DataTable _adv = CHNLSVC.Sales.CheckAdvanForIntr(base.GlbUserComCode, txtReceiptNo.Text.Trim());
                    if (_adv != null && _adv.Rows.Count > 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('This advance receipt is already picked for a inter-transfer. You are not allow to cancel.');", true);
                        txtReceiptNo.Text = "";
                        txtReceiptNo.Focus();
                        return;
                    }

                    if (_usedAmt > 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('This advance recipet is already utilized. You are not allow to cancel.');", true);
                        txtReceiptNo.Text = "";
                        txtReceiptNo.Focus();
                        return;
                    }

                }



                if (txtReceiptType.Text == "VHREG")
                {
                    List<VehicalRegistration> _tempList = new List<VehicalRegistration>();
                    _tempList = CHNLSVC.Sales.GetVehicalRegByRefNo(txtReceiptNo.Text.Trim());

                    foreach (VehicalRegistration temp in _tempList)
                    {
                        if (temp.P_srvt_rmv_stus == 1)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot cancel Receipt.Documents are already send to the RMV. Engine # : ');", true);
                            return;
                        }
                        else if (temp.P_svrt_prnt_stus == 2)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot cancel Receipt. Engine # : ');", true);
                            return;
                        }
                    }

                }

                if (txtReceiptType.Text == "VHINS")
                {
                    List<VehicleInsuarance> _tempInsuList = new List<VehicleInsuarance>();
                    _tempInsuList = CHNLSVC.Sales.GetVehicalInsByRefNo(txtReceiptNo.Text.Trim());

                    foreach (VehicleInsuarance temp in _tempInsuList)
                    {
                        if (temp.Svit_polc_stus == true)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot cancel Receipt.Policy is already issued. Engine # :');", true);
                            return;
                        }

                        else if (temp.Svit_cvnt_issue == 2)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot cancel Receipt. Engine # : ');", true);
                            return;
                        }

                    }
                }
                //bool _allowCurrentTrans = false;
                //if (IsAllowBackDateForModule(base.GlbUserComCode, string.Empty, base.GlbUserDefProf, this.GlbModuleName, dtpRecDate, lblBackDateInfor, dtpRecDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                //{
                //    if (_allowCurrentTrans == true)
                //    {
                //        if (dtpRecDate.Value.Date != DateTime.Now.Date)
                //        {
                //            //dtpRecDate.Enabled = true;
                //            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Back date not allow for selected date!');", true);
                //            //dtpRecDate.Focus();
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        //dtpRecDate.Enabled = true;
                //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Back date not allow for selected date!');", true);
                //        //dtpRecDate.Focus();
                //        return;
                //    }
                //}

                //if (Convert.ToDateTime(dtpRecDate.Value).Date != (DateTime.Now.Date))
                //{
                //    MessageBox.Show("Cannot cancel previous receipt.Please get a backdate.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}


                //this.Cursor = Cursors.WaitCursor;
                UpdateRecStatus(false);
                //this.Cursor = Cursors.Default;

                Clear_Data();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //Clear_Data();
                //crvReceiptPrint.Visible = false;
                Response.Redirect("~/ReceiptEnrty.aspx");
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BasePage basePage = new BasePage();

                //if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select customer.');", true);
                    return;
                }

                if (Convert.ToDouble(txtBalanceAmount.Text) != 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Payment not completed.');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtBalanceAmount.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('please select the valid pay amount.');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtReceiptType.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Receipt type is missing.');", true);
                    return;
                }

                //if (string.IsNullOrEmpty(txtDivision.Text))
                //{
                //    MessageBox.Show("Receipt division is missing.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtDivision.Focus();
                //    return;
                //}

                //if (!CHNLSVC.Sales.IsValidDivision(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDivision.Text.Trim()))
                //{
                //    MessageBox.Show("Invalid division.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtDivision.Text = "";
                //    txtDivision.Focus();
                //    return;
                //}

                if (chkIsManual.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtManualReferance.Text))
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter manual document number.');", true);
                        return;
                    }

                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(base.GlbUserComCode, base.GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManualReferance.Text));
                    if (_IsValid == false)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid manual document number.');", true);
                        txtManualReferance.Text = "";
                        txtManualReferance.Focus();
                        return;
                    }

                    RecieptHeaderTBS rh = new RecieptHeaderTBS();
                    rh = CHNLSVC.Sales.Check_ManRef_RecTBS(base.GlbUserComCode, base.GlbUserDefProf, txtReceiptType.Text.Trim(), txtManualReferance.Text.Trim());

                    if (rh != null)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Receipt number : " + txtManualReferance.Text + " already used.');", true);
                        txtManualReferance.Text = "";
                        txtManualReferance.Focus();
                        return;
                    }
                }

                if (txtReceiptType.Text == "ADVAN")
                {
                    //if (!string.IsNullOrEmpty(txtItem.Text))
                    //{
                    //    MessageBox.Show("Please add item before save receipt.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtItem.Focus();
                    //    return;
                    //}

                    List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", basePage.GlbUserComCode);

                    if (para.Count <= 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('system parameter not setup for Advance receipt valid period.');", true);
                        return;
                    }
                }

                //if (dgvItem.Rows.Count > 0)
                //{
                //    if (MessageBox.Show("Confirm the items which you select is correct.?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                //    {
                //        return;
                //    }
                //}

                if (grdPaymentDetails.Rows.Count == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Payments are not found.');", true);
                    return;
                }
                if (txtReceiptType.Text == "VHREG")
                {
                    if (grdPaymentDetails.Rows.Count <= 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Registration details are not found.');", true);
                        return;
                    }
                }
                else if (txtReceiptType.Text == "VHINS")
                {
                    if (grdPaymentDetails.Rows.Count <= 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('insuarance details are not found.');", true);
                        return;
                    }
                }
                else if (txtReceiptType.Text == "ADINS")
                {
                    if (grdPaymentDetails.Rows.Count <= 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('insuarance details are not found.');", true);
                        return;
                    }
                }

                decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text).Date, out _wkNo, basePage.GlbUserComCode);

                if (_weekNo == 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Week Definition is still not setup for current date.Please contact retail accounts dept.');", true);
                    return;
                }
                //bool _allowCurrentTrans = false;
                //if (IsAllowBackDateForModule(basePage.GlbUserComCode, string.Empty, basePage.GlbUserDefProf, basePage.GlbModuleName, txtDate.Text, txtDate.Text, txtDate.Text, out _allowCurrentTrans) == false)
                //{
                //    if (_allowCurrentTrans == true)
                //    {
                //        if (Convert.ToDateTime(txtDate.Text) != DateTime.Now.Date)
                //        {
                //            txtDate.Enabled = true;
                //            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Back date not allow for selected date!');", true);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        txtDate.Enabled = true;
                //        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Back date not allow for selected date!');", true);
                //        return;
                //    }
                //}

                //if (_isRes == true)
                //{
                //    if ((txtReceiptType.Text == "ADVAN") || (txtReceiptType.Text == "VHREG") || (txtReceiptType.Text == "VHINS"))
                //    {
                //        foreach (ReptPickSerials line in _ResList)
                //        {
                //            ReptPickSerials _tempItem = new ReptPickSerials();
                //            _tempItem = CHNLSVC.Inventory.GetAvailableSerIDInformation(basePage.GlbUserComCode, basePage.GlbUserDefLoca, line.Tus_itm_cd, line.Tus_ser_1, string.Empty, string.Empty);

                //            if (_tempItem.Tus_itm_cd == null)
                //            {
                //                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Selected serial not available in inventory.Please check.');", true);
                //                return;
                //            }
                //        }
                //    }
                //}

                btnSave.Enabled = false;
                SaveReceiptHeader();
                btnPrint.Enabled = true;
            }

            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Process Terminated');", true);
                CHNLSVC.CloseChannel();
                return;
            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable salesDetails = new DataTable();
                //DataTable ProfitCenter = new DataTable();
                //DataTable mst_rec_tp = new DataTable();

                //salesDetails = CHNLSVC.Sales.GetReceipt(txtReceiptNo.Text);
                //ProfitCenter = CHNLSVC.Sales.GetProfitCenterTable(base.GlbUserComCode, Session["UserDefProf"].ToString());
                //mst_rec_tp = CHNLSVC.Sales.GetReceiptType(salesDetails.Rows[0]["SAR_RECEIPT_TYPE"].ToString());

                //if ((salesDetails.Rows.Count > 0))
                //{
                //    ReportDocument crystalReport = new ReportDocument();
                //    crystalReport.Load(Server.MapPath("/Reports_Module/Financial_Rep/receiptPrint_Report.rpt"));

                //    crystalReport.Database.Tables["salesDetails"].SetDataSource(salesDetails);
                //    crystalReport.Database.Tables["ProfitCenter"].SetDataSource(ProfitCenter);
                //    crystalReport.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);

                //    crvReceiptPrint.ReportSource = crystalReport;

                //    crvReceiptPrint.ToolPanelView = ToolPanelViewType.None;
                //    //crystalReport.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                //    //crystalReport.PrintOptions.PaperSize = PaperSize.PaperA4;
                //    crystalReport.PrintToPrinter(1, false, 0, 1);

                //    Clear_Data();

                //}

                string receiptno = txtReceiptNo.Text;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Successfully saved. Invoice :" + receiptno +"');", true);
                //DisplayMessages("Successfully saved. Invoice : " + _invoiceNo);
                Clear_Data();
                Session["receiptno"] = receiptno;
                mpReceiptPrint.Show();

            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void Close_Click(object sender, EventArgs e)
        {
            mpReceiptPrint.Hide();
        }

        #endregion

        #region Data populate Method

        private void UpdateRecStatus(Boolean _RecUpdateStatus)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            RecieptHeaderTBS _UpdateReceipt = new RecieptHeaderTBS();
            _UpdateReceipt.Sir_receipt_no = txtReceiptNo.Text.Trim();
            _UpdateReceipt.Sir_act = _RecUpdateStatus;
            _UpdateReceipt.Sir_com_cd = base.GlbUserComCode;
            _UpdateReceipt.Sir_profit_center_cd = base.GlbUserDefProf;
            //_UpdateReceipt.Sar_mod_by = base.GlbUserID;
            _UpdateReceipt.Sir_mod_by = Convert.ToString(Session["UserID"]);
            _UpdateReceipt.Sir_receipt_type = txtReceiptType.Text.Trim();
            _UpdateReceipt.Sir_debtor_cd = txtCode.Text.Trim();
            _UpdateReceipt.Sir_debtor_name = txtName.Text.Trim();
            _UpdateReceipt.Sir_debtor_add_1 = txtAddress.Text.Trim();
            _UpdateReceipt.Sir_debtor_add_2 = txtAddress2.Text.Trim();
            _UpdateReceipt.Sir_mob_no = txtMobile.Text.Trim();
            _UpdateReceipt.Sir_anal_1 = ddlDistrict.SelectedItem.Text.Trim();
            _UpdateReceipt.Sir_anal_2 = txtProvince.Text.Trim();
            _UpdateReceipt.Sir_receipt_date = Convert.ToDateTime(txtDate.Text).Date;
            _UpdateReceipt.Sir_tot_settle_amt = Convert.ToDecimal(txtTotalReceiptAmount.Text);
            if (chkIsManual.Checked == true)
            {
                _UpdateReceipt.Sir_is_oth_shop = true;
                //_UpdateReceipt.Sar_oth_sr = txtOthSR.Text;
            }

            row_aff = (Int32)CHNLSVC.Sales.ReceiptCancelProcessTBS(_UpdateReceipt, _list, _regList, _insList, _gvDetails, _tmpRecItem);

            if (row_aff == 1)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Receipt cancelled succsufully.');", true);
                if (txtReceiptType.Text == "VHREG" || txtReceiptType.Text == "VHINS")
                {
                    row_aff = (Int32)CHNLSVC.Sales.ReceiptCancelInfoTBS(_UpdateReceipt, _regList, _insList);
                }
                Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert(' " + _msg + " ');", true);
                    //MessageBox.Show(_msg, "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Cursor = Cursors.Default;
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert(' Fail ');", true);
                    //MessageBox.Show("Fail", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Cursor = Cursors.Default;
                }
            }
        }

        private void invoicedetails()
        {
            if (!string.IsNullOrEmpty(txtInvoice.Text))
            {
                if (txtReceiptType.Text == "DEBT")
                {

                    if (string.IsNullOrEmpty(txtCode.Text))
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select customer first.');", true);
                        txtCode.Text = "";
                        txtCode.Focus();
                        return;
                    }

                    //check valid invoice
                    List<InvoiceHeaderTBS> _invHdr = new List<InvoiceHeaderTBS>();
                    if (chkIsManual.Checked == true)
                        _invHdr = CHNLSVC.Sales.GetPendingInvoicesTBS(base.GlbUserComCode, string.Empty, txtCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtDate.Text, txtDate.Text);
                    else
                        _invHdr = CHNLSVC.Sales.GetPendingInvoicesTBS(base.GlbUserComCode, base.GlbUserDefProf, txtCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtDate.Text, txtDate.Text);

                    if (_invHdr == null || _invHdr.Count == 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid invoice number.');", true);
                        txtInvoice.Text = "";
                        txtInvoice.Focus();
                        return;
                    }

                    foreach (InvoiceHeaderTBS _tmpInv in _invHdr)
                    {
                        if (_tmpInv.Sih_stus == "C" || _tmpInv.Sih_stus == "R")
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Selected invoice is cancelled or reversed.');", true);
                            txtInvoice.Text = "";
                            txtInvoice.Focus();
                            return;
                        }
                    }

                    if (chkIsManual.Checked == true)
                        txtInvoiceAmount.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmtTBS(base.GlbUserComCode, string.Empty, txtCode.Text, txtInvoice.Text)).ToString("n");
                    else
                        txtInvoiceAmount.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmtTBS(base.GlbUserComCode, base.GlbUserDefProf, txtCode.Text, txtInvoice.Text)).ToString("n");

                    if (Convert.ToDecimal(txtInvoiceAmount.Text) <= 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot find outstanding amount.');", true);
                        txtInvoice.Text = "";
                        txtInvoiceAmount.Text = "0.00";
                        txtInvoice.Focus();

                    }

                }
                else if ((txtReceiptType.Text == "VHREG") || (txtReceiptType.Text == "VHINS") || (txtReceiptType.Text == "ADINS"))
                {
                    txtInvoiceAmount.Text = "0.00";
                    //txtItem.Text = "";
                    //txtengine.Text = "";
                    //txtChasis.Text = "";
                    txtPaidAmount.Text = "0.00";
                    //check valid invoice
                    List<InvoiceHeaderTBS> _invHdr = new List<InvoiceHeaderTBS>();
                    _invHdr = CHNLSVC.Sales.GetPendingInvoicesTBS(base.GlbUserComCode, base.GlbUserDefProf, txtCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtDate.Text, txtDate.Text);

                    foreach (InvoiceHeaderTBS _tempInv in _invHdr)
                    {
                        if (_tempInv.Sih_inv_no == null)
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid invoice number.');", true);
                            txtInvoice.Text = "";
                            //_accNo = "";
                            //_invLine = 0;
                            txtInvoice.Focus();
                            return;
                        }

                        if (_tempInv.Sih_stus == "C" || _tempInv.Sih_stus == "R")
                        {
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Selected invoice is cancelled or reversed.');", true);
                            txtInvoice.Text = "";
                            txtInvoice.Focus();
                            return;
                        }


                        if (string.IsNullOrEmpty(txtCode.Text))
                        {
                            txtCode.Text = _tempInv.Sih_cus_cd;
                            LoadCustomerDetails();

                        }

                        //_invType = _tempInv.Sah_inv_tp;
                        //_accNo = _tempInv.Sah_anal_2;
                    }
                }
                else
                {
                    txtInvoiceAmount.Text = "0.00";
                }
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

        //private void LoadPromotions()
        //{
        //    //REMOVE COMMENT
        //    if (InvoiceItemList == null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        List<int> period = new List<int>();

        //        if (_paymentTypeRef == null)
        //        {
        //            List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
        //            _paymentTypeRef = _paymentTypeRef1;
        //        }
        //        if (_paymentTypeRef.Count <= 0)
        //        {
        //            List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
        //            _paymentTypeRef = _paymentTypeRef1;
        //        }
        //        _paymentTypeRef = (from ii in _paymentTypeRef where ii.Stp_pay_tp == "CRCD" select ii).ToList<PaymentType>();

        //        #region Old Method
        //        if (_LINQ_METHOD == false)
        //        {
        //            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
        //            {
        //                for (int i = 0; i < _paymentTypeRef.Count; i++)
        //                {
        //                    //CHECK for Bank
        //                    if (_paymentTypeRef[i].Stp_bank == textBoxCCBank.Text || string.IsNullOrEmpty(_paymentTypeRef[i].Stp_bank))
        //                    {

        //                        //check item/serail
        //                        if (SerialList != null) if (SerialList.Count > 0)
        //                                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                                    foreach (InvoiceItem _itm in InvoiceItemList)
        //                                    {
        //                                        var seriallist = SerialList.Where(x => x.Sap_itm_cd == _itm.Sad_itm_cd && x.Sap_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Sap_ser_1)).ToList();
        //                                        foreach (InvoiceSerial _serial in seriallist)
        //                                        {
        //                                            {
        //                                                if (_paymentTypeRef[i].Stp_ser == _serial.Sap_ser_1 && _paymentTypeRef[i].Stp_itm == _itm.Sad_itm_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
        //                                                {
        //                                                    period.Add(_paymentTypeRef[i].Stp_pd);
        //                                                    goto END;

        //                                                }
        //                                            }

        //                                        }
        //                                    }
        //                        //check promo
        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                        {
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                {
        //                                    if (_paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
        //                                    {
        //                                        period.Add(_paymentTypeRef[i].Stp_pd);
        //                                        goto END;
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        //check item
        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                        {
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                {
        //                                    if (_paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
        //                                    {
        //                                        period.Add(_paymentTypeRef[i].Stp_pd);
        //                                        goto END;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        //check brand/cat1/cat 2
        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                        {
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                {
        //                                    MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
        //                                    if (_ii == null)
        //                                        return;

        //                                    if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_main_cat == _ii.Mi_cate_1 && _paymentTypeRef[i].Stp_cat == _itm.Mi_cate_2)
        //                                    {
        //                                        period.Add(_paymentTypeRef[i].Stp_pd);
        //                                        goto END;
        //                                    }
        //                                }
        //                            }
        //                        }


        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                        {
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                {
        //                                    MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
        //                                    if (_ii == null)
        //                                        return;

        //                                    if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_main_cat == _ii.Mi_cate_1 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
        //                                    {
        //                                        period.Add(_paymentTypeRef[i].Stp_pd);
        //                                        goto END;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        //check brand/cat2
        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                        {
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                {
        //                                    MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
        //                                    if (_ii == null)
        //                                        return;

        //                                    if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_cat == _ii.Mi_cate_2 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
        //                                    {
        //                                        period.Add(_paymentTypeRef[i].Stp_pd);
        //                                        goto END;
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        //check brand
        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                        {
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                {
        //                                    MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
        //                                    if (_ii == null)
        //                                        return;
        //                                    if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
        //                                    {
        //                                        period.Add(_paymentTypeRef[i].Stp_pd);
        //                                        goto END;
        //                                    }
        //                                }

        //                            }
        //                        }

        //                        //pb plvl
        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                        {
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                {

        //                                    if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat)
        //                                        && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser) && _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl)
        //                                    {
        //                                        period.Add(_paymentTypeRef[i].Stp_pd);
        //                                        goto END;
        //                                    }
        //                                }

        //                            }
        //                        }
        //                    }
        //                END:
        //                    ;

        //                }

        //            }
        //        }
        //        #endregion

        //        #region New Method :: Done by Chamal 22/07/2014
        //        if (_LINQ_METHOD == true)
        //        {
        //            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
        //            {
        //                //check item/serail
        //                if (SerialList != null && SerialList.Count > 0)
        //                {
        //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                    {
        //                        var _promo1 = (from p in _paymentTypeRef
        //                                       from i in InvoiceItemList
        //                                       from s in SerialList
        //                                       where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
        //                                       (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                       (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                       (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                       (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
        //                                       (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
        //                                       select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                        foreach (var _type in _promo1)
        //                        {
        //                            period.Add(_type.Stp_pd);
        //                            //goto END;
        //                        }
        //                    }
        //                }

        //                //check promo
        //                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                {
        //                    var _promo1 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
        //                                   (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                   (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo1)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }
        //                }

        //                //check item + Specify bank
        //                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                {
        //                    var _promo1 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == textBoxCCBank.Text.ToString()) &&
        //                                   (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                   (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "")
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo1)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }
        //                }

        //                //check item
        //                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                {
        //                    var _promo1 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == null || p.Stp_bank == "") &&
        //                                   (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                   (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "")
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo1)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }
        //                }

        //                //check brand/cat1/cat2
        //                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                {
        //                    //check brand/cat1/cat2
        //                    var _promo1 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                   (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                   (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
        //                                   (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo1)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }

        //                    //check brand/cat1
        //                    var _promo2 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                   (p.Stp_cat == null || p.Stp_cat == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                   (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                   (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo2)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }

        //                    //check brand/cat2
        //                    var _promo3 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                   (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                   (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                   (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo3)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }

        //                    //check brand
        //                    var _promo4 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                   (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                   (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo4)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }
        //                }

        //                //check pb plvel with bank
        //                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                {
        //                    var _promo1 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                   (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                   (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "")
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo1)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }
        //                }

        //                //check pb plvel 
        //                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                {
        //                    var _promo1 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_bank == null || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                   (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                   (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
        //                                   (p.Stp_pb == i.Sad_pbook) &&
        //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl)
        //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
        //                    foreach (var _type in _promo1)
        //                    {
        //                        period.Add(_type.Stp_pd);
        //                        //goto END;
        //                    }
        //                }
        //            }
        //            //END:;
        //        }
        //        #endregion

        //        if (period.Count > 0)
        //        {

        //            //set period visible
        //            period.Sort();
        //            panelPermotion.Visible = true;
        //            comboBoxPermotion.DataSource = period;
        //        }
        //    }
        //}

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

        private void detailsbyreceiptno()
        {
            //if (!string.IsNullOrEmpty(txtReceiptNo.Text))
            //{
            //_IsRecall = false;
            ViewState["_RecStatus"] = false;
            _sunUpload = false;
            Boolean _isValidRec = false;

            RecieptHeaderTBS _ReceiptHeader = null;
            _ReceiptHeader = CHNLSVC.Tours.GetReceiptHeaderTBS(base.GlbUserComCode, base.GlbUserDefProf, txtReceiptNo.Text.Trim());

            if (_ReceiptHeader != null)
            {
                txtReceiptType.Text = _ReceiptHeader.Sir_receipt_type;

                _isValidRec = CHNLSVC.Sales.IsValidReceiptType(base.GlbUserComCode, _ReceiptHeader.Sir_receipt_type);

                if (_isValidRec == false)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Not allowed to view receipt type " + _ReceiptHeader.Sir_receipt_type + " in receipt module.');", true);
                    txtReceiptType.Text = "";
                    Clear_Data();
                    return;
                }

                txtReceiptNo.Text = _ReceiptHeader.Sir_receipt_no;
                txtDate.Text = (_ReceiptHeader.Sir_receipt_date).ToString("dd-MMM-yyyy");
                txtDate.Enabled = false;
                imgbtnDate.Enabled = false;

                if (_ReceiptHeader.Sir_anal_8 == 1)
                {
                    chkIsManual.Checked = true;
                }
                else
                {
                    chkIsManual.Checked = false;
                }

                txtManualReferance.Text = _ReceiptHeader.Sir_manual_ref_no;
                txtNote.Text = _ReceiptHeader.Sir_remarks;
                txtInvoiceAmount.Text = Convert.ToDecimal(_ReceiptHeader.Sir_tot_settle_amt).ToString("n");
                txtCode.Text = _ReceiptHeader.Sir_debtor_cd;
                txtCode.Enabled = false;

                txtAddress.Text = _ReceiptHeader.Sir_debtor_add_1;
                txtAddress2.Text = _ReceiptHeader.Sir_debtor_add_2;
                txtName.Text = _ReceiptHeader.Sir_debtor_name;
                txtMobile.Text = _ReceiptHeader.Sir_mob_no;
                txtNIC.Text = _ReceiptHeader.Sir_nic_no;
                txtProvince.Text = _ReceiptHeader.Sir_anal_2;
                txtPaidAmount.Text = Convert.ToDecimal(_ReceiptHeader.Sir_tot_settle_amt).ToString("n");
                _usedAmt = _ReceiptHeader.Sir_used_amt;
                txtTotalReceiptAmount.Text = Convert.ToDecimal(_ReceiptHeader.Sir_tot_settle_amt).ToString("n");
                txtSalesExecutive.Text = _ReceiptHeader.Sir_create_by;
                //if (_ReceiptHeader.Sir_is_oth_shop == true)
                //{
                //chkOth.Checked = true;
                //txtOthSR.Text = _ReceiptHeader.Sir_oth_sr;
                //chkOth.Enabled = false;
                //txtOthSR.Enabled = false;
                //}

                //if (string.IsNullOrEmpty(_ReceiptHeader.Sir_anal_1))
                //{
                //  ddlDistrict.SelectedValue = " ";
                //}
                //else
                //{
                //    ddlDistrict.Text = _ReceiptHeader.Sir_anal_1;
                //}
                ddlDistrict.SelectedItem.Text = _ReceiptHeader.Sir_anal_1;

                ViewState["_RecStatus"] = _ReceiptHeader.Sir_act;
                _sunUpload = _ReceiptHeader.Sir_uploaded_to_finance;

                //if (string.IsNullOrEmpty(_ReceiptHeader.Sir_anal_4))
                //{
                //    txtSalesExecutive.Text = "";
                //}
                //else
                //{
                //    //txtSalesEx.Text = _ReceiptHeader.Sir_anal_4;
                //}
                txtSalesExecutive.Text = _ReceiptHeader.Sir_anal_4;

                BindSaveReceiptDetails(_ReceiptHeader.Sir_receipt_no);
                //BindSaveVehicleReg(_ReceiptHeader.Sir_receipt_no);
                //BindSaveVehicleIns(_ReceiptHeader.Sir_receipt_no);

                _gvDetails = new List<GiftVoucherPages>();
                _gvDetails = CHNLSVC.Inventory.GetGiftVoucherByOthRef(_ReceiptHeader.Sir_com_cd, _ReceiptHeader.Sir_profit_center_cd, _ReceiptHeader.Sir_receipt_no);

                //dgvGv.AutoGenerateColumns = false;
                //dgvGv.DataSource = new List<GiftVoucherPages>();
                //dgvGv.DataSource = _gvDetails;

                if (txtReceiptType.Text == "VHREG")
                {
                    //tbOth.SelectedTab = tpReg;
                }
                else if (txtReceiptType.Text == "VHINS")
                {
                    //tbOth.SelectedTab = tpInsu;
                }
                else if (txtReceiptType.Text == "GVISU")
                {
                    //tbOth.SelectedTab = tbGv;
                }

                _tmpRecItem = new List<ReceiptItemDetails>();
                _tmpRecItem = CHNLSVC.Sales.GetAdvanReceiptItems(_ReceiptHeader.Sir_receipt_no);



                //dgvItem.Rows.Clear();

                //if (_tmpRecItem != null)
                //{
                //    foreach (ReceiptItemDetails ser in _tmpRecItem)
                //    {
                //        dgvItem.Rows.Add();
                //        dgvItem["col_itmItem", dgvItem.Rows.Count - 1].Value = ser.Sari_item;
                //        dgvItem["col_itmDesc", dgvItem.Rows.Count - 1].Value = ser.Sari_item_desc;
                //        dgvItem["col_itmModel", dgvItem.Rows.Count - 1].Value = ser.Sari_model;
                //        dgvItem["col_itmStatus", dgvItem.Rows.Count - 1].Value = null;
                //        dgvItem["col_itmSerial", dgvItem.Rows.Count - 1].Value = ser.Sari_serial;
                //        dgvItem["col_itmOthSerial", dgvItem.Rows.Count - 1].Value = ser.Sari_serial_1;
                //    }
                //}

                btnSave.Enabled = false;
                btnCancel.Enabled = true;
            }

            //}        
        }

        private void ReceiptTypeChange()
        {
            if (!string.IsNullOrEmpty(txtReceiptType.Text))
            {
                if (!CHNLSVC.Sales.IsValidReceiptType(base.GlbUserComCode, txtReceiptType.Text.Trim()))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid receipt type.');", true);
                    txtReceiptType.Text = "";
                    txtReceiptType.Focus();
                    return;
                }
                else
                {
                    //chkOth.Visible = false;
                    //txtOthSR.Visible = false;
                    //btnOthSR.Visible = false;
                    //txtengine.Enabled = true;
                    //txtChasis.Enabled = true;
                    if (txtReceiptType.Text == "DEBT")
                    {
                        txtInvoice.Text = "";
                        txtBalanceAmount.Text = "0.00";
                        txtPaidAmount.Text = "0.00";
                        txtInvoice.Enabled = true;
                        chkIsManual.Checked = false;
                        //chkIsMan.Enabled = true;
                        //chkDel.Checked = false;
                        //chkDel.Enabled = false;
                        //gbItem.Visible = false;
                        //gbInsu.Visible = false;
                        //gbsettle.Visible = true;
                        //chkOth.Visible = true;
                        //txtOthSR.Visible = true;
                        //btnOthSR.Visible = true;
                        //txtGVCode.Text = "";
                        //lblFrompg.Text = "";
                        //lblPageCount.Text = "";
                        grdPaymentDetails.DataSource = new int[] { };
                        //cmbTopg.DataSource = new DataTable();
                        //txtPgAmt.Text = "";
                        //txtTotGvAmt.Text = "";
                        //gbGVDet.Visible = false;
                        //ClearCus_Data();
                        //ClearSettle_Data();
                    }
                    else if (txtReceiptType.Text == "VHREG")
                    {
                        txtInvoice.Text = "";
                        txtBalanceAmount.Text = "0.00";
                        txtPaidAmount.Text = "0.00";
                        txtInvoice.Enabled = true;
                        chkIsManual.Checked = false;
                        //chkIsMan.Enabled = true;
                        //chkDel.Checked = false;
                        //chkDel.Enabled = true;
                        //gbItem.Visible = true;
                        //gbInsu.Visible = false;
                        //gbsettle.Visible = true;
                        //txtGVCode.Text = "";
                        //lblFrompg.Text = "";
                        //lblPageCount.Text = "";
                        //cmbGvBook.DataSource = new DataTable();
                        //cmbTopg.DataSource = new DataTable();
                        //txtPgAmt.Text = "";
                        //txtTotGvAmt.Text = "";
                        //gbGVDet.Visible = false;
                        //ClearCus_Data();
                        //ClearSettle_Data();
                    }
                    else if (txtReceiptType.Text == "VHINS")
                    {
                        txtInvoice.Text = "";
                        txtBalanceAmount.Text = "0.00";
                        txtPaidAmount.Text = "0.00";
                        txtInvoice.Enabled = true;
                        chkIsManual.Checked = false;
                        //chkIsMan.Enabled = true;
                        //chkDel.Checked = false;
                        //chkDel.Enabled = true;
                        //gbItem.Visible = true;
                        //gbInsu.Visible = true;
                        //gbsettle.Visible = true;
                        //txtGVCode.Text = "";
                        //lblFrompg.Text = "";
                        //lblPageCount.Text = "";
                        //cmbGvBook.DataSource = new DataTable();
                        //cmbTopg.DataSource = new DataTable();
                        //txtPgAmt.Text = "";
                        //txtTotGvAmt.Text = "";
                        //gbGVDet.Visible = false;
                        //ClearCus_Data();
                        //ClearSettle_Data();
                        MasterOutsideParty _insCom = CHNLSVC.Sales.GetOutSidePartyDetails(null, "INS");
                        if (_insCom.Mbi_cd != null)
                        {
                            //txtInsCom.Text = _insCom.Mbi_cd;
                        }
                        else
                        {
                            //txtInsCom.Text = "";
                        }

                        InsuarancePolicy _insPol = CHNLSVC.Sales.GetInusPolicy(null);
                        if (_insPol.Svip_polc_cd != null)
                        {
                            //txtInsPol.Text = _insPol.Svip_polc_cd;
                        }
                        else
                        {
                            //txtInsPol.Text = "";
                        }

                    }
                    else if (txtReceiptType.Text == "ADINS")
                    {
                        txtInvoice.Text = "";
                        txtBalanceAmount.Text = "0.00";
                        txtPaidAmount.Text = "0.00";
                        txtInvoice.Enabled = true;
                        chkIsManual.Checked = false;
                        //chkIsMan.Enabled = true;
                        //chkDel.Checked = false;
                        //chkDel.Enabled = false;
                        //gbItem.Visible = true;
                        //gbInsu.Visible = true;
                        //gbsettle.Visible = true;
                        //txtengine.Enabled = true;
                        //txtChasis.Enabled = true;
                        //txtGVCode.Text = "";
                        //lblFrompg.Text = "";
                        //lblPageCount.Text = "";
                        //cmbGvBook.DataSource = new DataTable();
                        //cmbTopg.DataSource = new DataTable();
                        //txtPgAmt.Text = "";
                        //txtTotGvAmt.Text = "";
                        //gbGVDet.Visible = false;
                        //ClearCus_Data();
                        //ClearSettle_Data();
                        MasterOutsideParty _insCom = CHNLSVC.Sales.GetOutSidePartyDetails(null, "INS");
                        if (_insCom.Mbi_cd != null)
                        {
                            //txtInsCom.Text = _insCom.Mbi_cd;
                        }
                        else
                        {
                            //txtInsCom.Text = "";
                        }

                        InsuarancePolicy _insPol = CHNLSVC.Sales.GetInusPolicy(null);
                        if (_insPol.Svip_polc_cd != null)
                        {
                            //txtInsPol.Text = _insPol.Svip_polc_cd;
                        }
                        else
                        {
                            //txtInsPol.Text = "";
                        }

                    }
                    else if (txtReceiptType.Text == "ADVAN")
                    {
                        txtInvoice.Text = "";
                        txtBalanceAmount.Text = "0.00";
                        txtPaidAmount.Text = "0.00";
                        txtInvoice.Enabled = false;
                        chkIsManual.Checked = false;
                        //chkDel.Checked = false;
                        //chkDel.Enabled = false;
                        //gbInsu.Visible = false;
                        //gbsettle.Visible = true;

                        if (base.GlbUserComCode == "AAL")
                        {
                            if (base.GlbUserDefProf != "500")
                            {
                                //chkIsMan.Checked = true;
                                //chkIsManual.Enabled = false;
                            }
                            else
                            {
                                //chkIsMan.Checked = false;
                                chkIsManual.Enabled = true;
                            }
                        }
                        else
                        {

                            MasterProfitCenter _ctn = CHNLSVC.Sales.GetProfitCenter(base.GlbUserComCode, base.GlbUserDefProf);
                            if (_ctn != null)
                            {
                                if (_ctn.Mpc_chnl != "ELITE" && _ctn.Mpc_chnl != "RMSR" && _ctn.Mpc_chnl != "AOA_CH" && _ctn.Mpc_chnl != "CLEARENCE_SALES" && _ctn.Mpc_chnl != "APPLE" && _ctn.Mpc_chnl != "RAPS" && _ctn.Mpc_chnl != "RCLS")
                                {
                                    //chkIsMan.Checked = true;
                                    //chkIsManual.Enabled = false;
                                    txtManualReferance.Text = "";        //kapila 6/4/2015
                                }

                            }

                        }
                        //txtGVCode.Text = "";
                        //lblFrompg.Text = "";
                        //lblPageCount.Text = "";
                        //cmbGvBook.DataSource = new DataTable();
                        //cmbTopg.DataSource = new DataTable();
                        //txtPgAmt.Text = "";
                        //txtTotGvAmt.Text = "";
                        //gbGVDet.Visible = false;
                        //ClearCus_Data();
                        //ClearSettle_Data();
                        //txtCusCode.Text = "CASH";
                    }
                    else if (txtReceiptType.Text == "GVISU")
                    {
                        //ClearCus_Data();
                        //ClearSettle_Data();
                        if (!CHNLSVC.Security.Is_OptionPerimitted(base.GlbUserComCode, string.Empty, 10061))
                        {
                            //chkAllowPromo.Checked = false;
                            //chkAllowPromo.Visible = false;
                            //chkGvFOC.Checked = false;
                            //chkGvFOC.Visible = false;
                            //dtGVExp.Visible = false;
                            //lblGVExp.Visible = false;
                        }
                        else
                        {
                            //chkAllowPromo.Checked = false;
                            //chkAllowPromo.Visible = true;
                            //chkGvFOC.Checked = false;
                            //chkGvFOC.Visible = true;
                            //dtGVExp.Visible = true;
                            //lblGVExp.Visible = true;

                        }
                        //txtGVCode.Text = "";
                        //lblFrompg.Text = "";
                        //lblPageCount.Text = "";
                        //cmbGvBook.DataSource = new DataTable();
                        //cmbTopg.DataSource = new DataTable();
                        //txtPgAmt.Text = "";
                        //txtTotGvAmt.Text = "";
                        //gbsettle.Visible = false;
                        //gbGVDet.Visible = true;
                        //txtCusCode.Text = "CASH";
                    }

                    else
                    {
                        //txtInvoice.Text = "";
                        //txtBalance.Text = "0.00";
                        //txtPayment.Text = "0.00";
                        //txtInvoice.Enabled = false;
                        //chkIsMan.Checked = false;
                        //chkIsMan.Enabled = true;
                        //chkDel.Checked = false;
                        //chkDel.Enabled = false;
                        //gbInsu.Visible = false;
                        //gbItem.Visible = false;
                        //gbsettle.Visible = true;
                        //txtGVCode.Text = "";
                        //lblFrompg.Text = "";
                        //lblPageCount.Text = "";
                        //cmbGvBook.DataSource = new DataTable();
                        //cmbTopg.DataSource = new DataTable();
                        //txtPgAmt.Text = "";
                        //txtTotGvAmt.Text = "";
                        //gbGVDet.Visible = false;
                        //ClearCus_Data();
                        //ClearSettle_Data();
                    }

                    //ucPayModes1.InvoiceType = txtRecType.Text.Trim();
                    //ucPayModes1.LoadData();
                }

                MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                _RecDiv = CHNLSVC.Sales.GetDefRecDivision(base.GlbUserComCode, base.GlbUserDefProf);
                //if (_RecDiv.Msrd_cd != null)
                //{
                //    txtDivision.Text = _RecDiv.Msrd_cd;
                //}
                //else
                //{
                //    txtDivision.Text = "";
                //}
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
                        List<PaymentType> _paymentTypeRef1 = base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(base.GlbUserComCode, Session["UserSubChannl"].ToString(), base.GlbUserDefProf, txtReceiptType.Text, DateTime.Now.Date);
                        _paymentTypeRef = _paymentTypeRef1;
                    }
                    if (_paymentTypeRef.Count <= 0)
                    {
                        List<PaymentType> _paymentTypeRef1 = base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(base.GlbUserComCode, Session["UserSubChannl"].ToString(), base.GlbUserDefProf, txtReceiptType.Text, DateTime.Now.Date);
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

        private void Clear_Data()
        {
            ImgbtnReceiptType.Enabled = true;

            btnCustomerPaymentAdd.Enabled = true;

            txtPaidAmount.Enabled = false;
            txtBalanceAmount.Enabled = false;

            _sheduleDetails = new List<HpSheduleDetails>();
            _regList = new List<VehicalRegistration>();
            _insList = new List<VehicleInsuarance>();
            _HpAccount = new HpAccount();
            _SchemeDetails = new HpSchemeDetails();
            _ResList = new List<ReptPickSerials>();
            //ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            _list = new List<RecieptItemTBS>();
            _gvDetails = new List<GiftVoucherPages>();
            _tmpRecItem = new List<ReceiptItemDetails>();

            txtChequeNo.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtDepositBankCard.Text = string.Empty;

            //ucPayModes1.PaidAmountLabel.Text = "0.00";
            //ucPayModes1.ClearControls();

            //dgvGv.AutoGenerateColumns = false;
            //dgvGv.DataSource = new List<GiftVoucherPages>();

            //gbInsu.Visible = false;
            //gbItem.Visible = false;
            //chkDel.Enabled = false;
            //chkAnnual.Visible = false;
            //lblBackDateInfor.Text = "";
            //_regAmt = 0;
            //_invType = "";
            //_accNo = "";
            //_invNo = "";
            //_invLine = 0;
            //_colTerm = 0;
            //_insuTerm = 0;
            //_insuAmt = 0;
            //_IsRecall = false;
            ViewState["_RecStatus"] = false;
            _sunUpload = false;
            //_isRes = false;
            _usedAmt = 0;
            //chkOth.Enabled = true;
            //txtOthSR.Enabled = true;
            //chkOth.Checked = false;
            //txtOthSR.Text = "";

            txtReceiptType.Text = "ADVAN";
            txtReceiptType.Enabled = false;
            txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtDate.Enabled = false;
            imgbtnDate.Enabled = false;

            txtReceiptNo.Text = string.Empty;
            txtReceiptNo.Enabled = false;
            chkIsManual.Checked = false;
            txtManualReferance.Text = string.Empty;
            txtManualReferance.Enabled = false;

            txtCode.Text = string.Empty;
            txtCode.Enabled = true;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtNIC.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtProvince.Text = string.Empty;
            ddlDistrict.SelectedIndex = 0;
            txtInvoice.Text = string.Empty;
            txtInvoiceAmount.Text = "0.00";

            Session["_recieptItem"] = null;

            btnSave.Enabled = true;

            txtCustomerPayment.Text = string.Empty;
            txtPaidAmount.Text = "0.00";
            txtBalanceAmount.Text = "0.00";

            if (ddlPayMode.Items.Count > 0)
                ddlPayMode.SelectedIndex = 0;

            txtAmount.Text = string.Empty;

            txtRemark.Text = string.Empty;

            txtDepositBank.Text = string.Empty;
            txtDepositBranch.Text = string.Empty;

            txtCardNo.Text = string.Empty;
            txtBranchCheque.Text = string.Empty;
            txtBankCheque.Text = string.Empty;
            txtDepositBankCheque.Text = string.Empty;
            txtDepositBranchCheque.Text = string.Empty;

            txtNote.Text = string.Empty;
            txtSalesExecutive.Text = string.Empty;
            txtTotalReceiptAmount.Text = string.Empty;

            grdPaymentDetails.DataSource = new int[] { };
            grdPaymentDetails.DataBind();

            lblbank.Text = string.Empty;
            lblPromotion.Text = string.Empty;
            txtDepositBranchCard.Text = string.Empty;
            ddlCardType.Items.Clear();
            ddlCardType.DataBind();


            mltPaymentDetails.ActiveViewIndex = 0;

            btnPrint.Enabled = false;

            txtRemark.Enabled = false;


            //    grdPaymentDetails.DataSource = new int[] { };            

            //    if (BaseCls.GlbUserComCode != "AAL")
            //    {
            //        lblSer1.Text = "Serial 1 :";
            //        lblSer2.Text = "Serial 2 :";
            //    }

            //    dgvItem.Rows.Clear();
            //    dgvIns.Rows.Clear();
            //    dgvReg.Rows.Clear();
            //    txtRecType.Text = "";
            //    txtRecNo.Text = "";
            //    txtManual.Text = "";
            //    txtDivision.Text = "";
            //    chkIsMan.Checked = false;
            //    chkIsMan.Enabled = true;
            //    chkIsMan.Checked = false;
            //    dtpRecDate.Value = Convert.ToDateTime(DateTime.Now).Date;
            //    txtGVCode.Text = "";
            //    lblFrompg.Text = "";
            //    lblPageCount.Text = "";
            //    cmbGvBook.DataSource = new DataTable();
            //    cmbTopg.DataSource = new DataTable();
            //    txtPgAmt.Text = "";
            //    txtTotGvAmt.Text = "";
            //    txtTotal.Text = "";
            //    txtSalesEx.Text = "";

            //    cmbDistrict.Enabled = true;
            //    txtCusCode.Enabled = true;
            //    txtRecType.Enabled = true;
            //    ClearSettle_Data();
            //    ClearCus_Data();
            //    Load_District();
            //    btnSave.Enabled = true;
            btnCancel.Enabled = false;
            //    gbGVDet.Enabled = true;
            //    gbGVDet.Visible = false;
            //    gbsettle.Visible = true;
            //    chkAllowPromo.Checked = true;
            //    chkAllowPromo.Visible = false;
            //    chkGvFOC.Checked = false;
            //    chkGvFOC.Visible = false;
            //    dtGVExp.Value = DateTime.Now.Date;
            //    dtGVExp.Visible = false;
            //    lblGVExp.Visible = false;
            //    bool _allowCurrentTrans = false;
            //    IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpRecDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            //    txtRecType.Text = "ADVAN";

            //    if (BaseCls.GlbIsManChkLoc == true)
            //    {
            //        txtManual.Text = "";
            //        txtManual.Enabled = false;
            //    }
            //    else
            //    {
            //        txtManual.Text = "";
            //        txtManual.Enabled = true;
            //    }

            //    txtRecType.Focus();
        }

        private void loadPaymenttypes()
        {
            ddlPayMode.Items.Clear();
            BasePage basePage = new BasePage();
            List<PaymentType> _paymentTypeRef = basePage.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), Session["UserDefProf"].ToString(), txtReceiptType.Text, DateTime.Now);
            List<string> payTypes = new List<string>();
            //payTypes.Add("");
            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            {
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    payTypes.Add(pt.Stp_pay_tp);
                }
            }
            ddlPayMode.DataSource = payTypes;
            ddlPayMode.DataBind();
            ddlPayMode.SelectedIndex = 0;

            if (payTypes[0].ToString().ToUpper() == "CASH")
            {
                mltPaymentDetails.ActiveViewIndex = 0;
            }
            else if (payTypes[0].ToString().ToUpper() == "CRCD")
            {
                mltPaymentDetails.ActiveViewIndex = 1;
            }
            else if (payTypes[0].ToString().ToUpper() == "CHEQUE")
            {
                mltPaymentDetails.ActiveViewIndex = 2;
            }
        }

        private void AddPayment()
        {
            string invoiceType = txtReceiptType.Text;

            if (Session["_recieptItem"] == null)
            {
                _recieptItem = new List<RecieptItemTBS>();
            }
            else
            {
                _recieptItem = (List<RecieptItemTBS>)Session["_recieptItem"];
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { DisplayMessages("Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtAmount.Text)) { DisplayMessages("Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtAmount.Text) <= 0) { DisplayMessages("Please select the valid pay amount"); return; }

            Decimal BankOrOtherCharge_ = 0;
            Decimal BankOrOther_Charges = 0;
            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                decimal _selectAmt = Convert.ToDecimal(txtAmount.Text);

                List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), GlbUserDefProf, txtReceiptType.Text, DateTime.Now.Date);
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
                DisplayMessages("Please select the valid pay amount");
                return;
            }

            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                if (string.IsNullOrEmpty(txtBankCard.Text))
                {
                    DisplayMessages("Please select the valid bank");
                    txtBankCard.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCardNo.Text))
                {
                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        DisplayMessages("Please select the card no");
                    //else
                    //{
                    //    DisplayMessages("Please select the cheque no");
                    //    txtCardNo.Focus();
                    //    return;
                    //}
                }
                if (string.IsNullOrEmpty(ddlCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select the card type');", true);
                    ddlCardType.Focus();
                    return;
                }
            }
            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
            {
                if (string.IsNullOrEmpty(txtBankCheque.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please select the valid bank');", true);
                    txtBankCheque.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtChequeNo.Text))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Please enter cheque no');", true);
                    ddlCardType.Focus();
                    return;
                }

            }

            //if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
            //{
            //if (string.IsNullOrEmpty(txtPayAdvReceiptNo.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the document no");
            //    txtPayAdvReceiptNo.Focus();
            //    return;
            //}
            //}

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPeriod.Text))
                {
                    DisplayMessages("Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtPeriod.Text) <= 0)
                        {
                            DisplayMessages("Please select the valid period");
                            return;
                        }
                    }
                    catch
                    {
                        _period = 0;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPeriod.Text)) _period = 0;
            else _period = Convert.ToInt32(txtPeriod.Text);

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                DisplayMessages("Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtAmount.Text) <= 0)
                    {
                        DisplayMessages("Please select the valid pay amount");
                        return;
                    }
                }
                catch
                {
                    _payAmount = 0;
                }
            }

            _payAmount = Convert.ToDecimal(txtAmount.Text);

            if (_recieptItem.Count <= 0)
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

                _item.Sird_inv_no = txtInvoice.Text;

                _item.Sird_pay_tp = ddlPayMode.SelectedValue.ToString();
                _item.Sird_settle_amt = Convert.ToDecimal(_payAmount);
                _item.Sird_anal_3 = Math.Round(BankOrOther_Charges, 2);
                _item.Sird_rmk = txtRemark.Text;
                _paidAmount += _payAmount;

                _recieptItem.Add(_item);
            }
            else
            {
                bool _isDuplicate = false;

                var _duplicate = from _dup in _recieptItem
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

                    _item.Sird_inv_no = txtInvoice.Text;

                    //_item.Sard_chq_bank_cd = txtBankCheque.Text;
                    //_item.Sard_chq_branch = txtBranchCheque.Text;
                    //_item.Sard_credit_card_bank = null;
                    _item.Sird_deposit_bank_cd = txtDepositBank.Text;
                    _item.Sird_deposit_branch = txtDepositBranch.Text;
                    _item.Sird_pay_tp = ddlPayMode.SelectedValue.ToString();
                    _item.Sird_settle_amt = Convert.ToDecimal(_payAmount);
                    _item.Sird_anal_3 = Math.Round(BankOrOther_Charges, 2);
                    _paidAmount += _payAmount;
                    _recieptItem.Add(_item);
                }
                else
                {
                    DisplayMessages("You can not add duplicate payments");
                    return;
                }
            }

            Session["_recieptItem"] = _recieptItem;
            grdPaymentDetails.DataSource = _recieptItem;
            grdPaymentDetails.DataBind();

            txtPaidAmount.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtPaidAmount.Text) + Convert.ToDecimal(_paidAmount)));
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Division:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutstandingInv:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator + txtCode.Text.Trim() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.OutstandingInvOth:
                //    {
                //        paramsText.Append(base.GlbUserComCode + seperator + txtOthSR.Text + seperator + txtCode.Text.Trim() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {

                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceByCus:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator + "INV" + seperator + txtCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(txtInvoice.Text.Trim() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                //    {
                //        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefLoca + seperator + txtItem.Text.Trim() + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.DeliverdSerials:
                //    {
                //        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefLoca + seperator + txtInvoice.Text.Trim() + seperator + txtItem.Text.Trim() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator + txtReceiptType.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetItmByType:
                    {
                        paramsText.Append("G" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptDate:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBank:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator + base.GlbUserDefProf + seperator + ddlPayMode.SelectedItem.Text + seperator);
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
                        //paramsText.Append(textBoxCCDepBank.Text.Trim() + seperator);
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
                        //if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        //{
                        //    MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxCCBank.Text.ToUpper().Trim());
                        //    if (_bankAccounts != null)
                        //    {
                        //        paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                        //    }
                        //    // paramsText.Append(textBoxCCBank.Text.Trim() + seperator);
                        //}
                        //if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                        //{
                        //    MasterOutsideParty _bankAccounts = base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxDbBank.Text.ToUpper().Trim());
                        //    if (_bankAccounts != null)
                        //    {
                        //        paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                        //    }
                        //    //paramsText.Append(textBoxDbBank.Text.Trim() + seperator);
                        //}

                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void LoadCustomerDetails()
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(string.Empty, txtCode.Text.Trim(), string.Empty, string.Empty, "C");

                if (_masterBusinessCompany.Mbe_cd != null)
                {

                    if (_masterBusinessCompany.Mbe_cd == string.Empty)
                    {
                        txtCode.Text = _masterBusinessCompany.Mbe_cd;
                        txtName.Text = "";
                        txtAddress.Text = "";
                        txtAddress2.Text = "";
                        txtNIC.Text = "";
                        txtMobile.Text = "";
                        txtCode.ReadOnly = false;
                        txtName.ReadOnly = false;
                    }
                    else
                    {
                        txtCode.Text = _masterBusinessCompany.Mbe_cd;
                        txtName.Text = _masterBusinessCompany.Mbe_name;
                        txtAddress.Text = _masterBusinessCompany.Mbe_add1;
                        txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
                        txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                        txtMobile.Text = _masterBusinessCompany.Mbe_mob;

                        if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_distric_cd))
                        {

                        }
                        else
                        {
                            ddlDistrict.Text = _masterBusinessCompany.Mbe_distric_cd;
                        }

                        txtProvince.Text = _masterBusinessCompany.Mbe_province_cd;
                        txtName.ReadOnly = true;
                    }
                }
                else
                {
                    //MessageBox.Show("Invalid customer.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Text = "";
                    txtName.Text = "";
                    txtAddress.Text = "";
                    txtAddress2.Text = "";
                    txtNIC.Text = "";
                    txtMobile.Text = "";
                    txtProvince.Text = "";
                    txtCode.Focus();
                    return;
                }
            }
            else
            {
                txtName.Text = "";
                txtAddress.Text = "";
                txtAddress2.Text = "";
                txtNIC.Text = "";
                txtMobile.Text = "";
                txtProvince.Text = "";
            }
        }

        private void Load_District()
        {

            ddlDistrict.DataSource = new List<DistrictProvince>();
            List<DistrictProvince> _district = CHNLSVC.Sales.GetDistrict("");
            var _final = (from _lst in _district
                          select _lst.Mds_district).ToList();

            ddlDistrict.DataSource = _final.ToList();
            ddlDistrict.DataBind();
        }

        protected void GetProvince()
        {
            if (string.IsNullOrEmpty(ddlDistrict.Text)) return;
            DistrictProvince _type = CHNLSVC.Sales.GetDistrict(ddlDistrict.Text.Trim())[0];
            if (_type.Mds_district == null)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Invalid district selected.');", true);
            }
            txtProvince.Text = _type.Mds_province;
            txtProvince.Enabled = false;
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

            recieptItemList = (List<RecieptItemTBS>)Session["_recieptItem"];

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

        private void SaveReceiptHeader()
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            decimal _valPd = 0;
            ReptPickHeader _SerHeader = new ReptPickHeader();
            List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempRegSave = new List<VehicalRegistration>();
            List<VehicleInsuarance> _tempInsSave = new List<VehicleInsuarance>();
            BasePage basePage = new BasePage();

            if (txtReceiptType.Text == "ADVAN")
            {
                List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", basePage.GlbUserComCode);
                if (para.Count > 0)
                {
                    _valPd = para[0].Hsy_val;
                }
            }
            else if (txtReceiptType.Text == "ADINS")
            {
                List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADINREMXDT", "COM", basePage.GlbUserComCode);
                if (para.Count > 0)
                {
                    _valPd = para[0].Hsy_val;
                }
            }


            RecieptHeaderTBS _ReceiptHeader = new RecieptHeaderTBS();
            _ReceiptHeader.Sir_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, BaseCls.GlbUserComCode);
            _ReceiptHeader.Sir_com_cd = basePage.GlbUserComCode;
            _ReceiptHeader.Sir_receipt_type = txtReceiptType.Text.Trim();
            _ReceiptHeader.Sir_receipt_no = _ReceiptHeader.Sir_seq_no.ToString();// txtReceiptNo.Text.Trim();//
            //_ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
            _ReceiptHeader.Sir_manual_ref_no = txtManualReferance.Text.Trim();
            _ReceiptHeader.Sir_receipt_date = Convert.ToDateTime(txtDate.Text).Date;
            _ReceiptHeader.Sir_direct = true;
            _ReceiptHeader.Sir_acc_no = "";
            if (chkIsManual.Checked == true)
            {
                _ReceiptHeader.Sir_is_oth_shop = true;
                //_ReceiptHeader.Sar_oth_sr = txtOthSR.Text;
            }
            else
            {
                _ReceiptHeader.Sir_is_oth_shop = false;
                _ReceiptHeader.Sir_oth_sr = "";
            }
            _ReceiptHeader.Sir_profit_center_cd = basePage.GlbUserDefProf;
            _ReceiptHeader.Sir_debtor_cd = txtCode.Text.Trim();
            _ReceiptHeader.Sir_debtor_name = txtName.Text.Trim();
            _ReceiptHeader.Sir_debtor_add_1 = txtAddress.Text.Trim();
            _ReceiptHeader.Sir_debtor_add_2 = txtAddress2.Text.Trim();
            _ReceiptHeader.Sir_tel_no = "";
            _ReceiptHeader.Sir_mob_no = txtMobile.Text.Trim();
            _ReceiptHeader.Sir_nic_no = txtNIC.Text.Trim();
            _ReceiptHeader.Sir_tot_settle_amt = Convert.ToDecimal(txtTotalReceiptAmount.Text);
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
            _ReceiptHeader.Sir_remarks = txtNote.Text.Trim();
            _ReceiptHeader.Sir_is_used = false;
            _ReceiptHeader.Sir_ref_doc = "";
            _ReceiptHeader.Sir_ser_job_no = "";
            _ReceiptHeader.Sir_used_amt = 0;
            _ReceiptHeader.Sir_create_by = Convert.ToString(Session["UserID"]);
            _ReceiptHeader.Sir_mod_by = Convert.ToString(Session["UserID"]);
            _ReceiptHeader.Sir_session_id = basePage.GlbUserSessionID;
            _ReceiptHeader.Sir_anal_1 = ddlDistrict.Text;
            _ReceiptHeader.Sir_anal_2 = txtProvince.Text.Trim();

            if (chkIsManual.Checked == true)
            {
                _ReceiptHeader.Sir_anal_3 = "MANUAL";
                _ReceiptHeader.Sir_anal_8 = 1;
            }
            else
            {
                _ReceiptHeader.Sir_anal_3 = "SYSTEM";
                _ReceiptHeader.Sir_anal_8 = 0;
            }

            //_ReceiptHeader.Sar_anal_4 = txtSalesEx.Text;
            _ReceiptHeader.Sir_anal_5 = 0;
            _ReceiptHeader.Sir_anal_6 = 0;
            _ReceiptHeader.Sir_anal_7 = 0;
            _ReceiptHeader.Sir_anal_9 = 0;
            _ReceiptHeader.Sir_VALID_TO = _ReceiptHeader.Sir_receipt_date.AddDays(Convert.ToDouble(_valPd));

            recieptItemList = (List<RecieptItemTBS>)Session["_recieptItem"];

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
            masterAutoRecTp.Aut_start_char = txtReceiptType.Text.Trim();
            masterAutoRecTp.Aut_year = null;

            if (grdPaymentDetails.Rows.Count > 0)
            {
                _SerHeader.Tuh_usrseq_no = CHNLSVC.Inventory.GetSerialID();  //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "RECEIPT", 0, BaseCls.GlbUserComCode);
                _SerHeader.Tuh_usr_id = Convert.ToString(Session["UserID"]);
                _SerHeader.Tuh_usr_com = basePage.GlbUserComCode;
                _SerHeader.Tuh_session_id = basePage.GlbUserSessionID;
                _SerHeader.Tuh_cre_dt = Convert.ToDateTime(txtDate.Text).Date;
                //if (_invType == "HS")
                //{
                //    _SerHeader.Tuh_doc_tp = "RECEIPT";
                //}
                //else
                //{
                //    _SerHeader.Tuh_doc_tp = "DO";
                //}
                //_SerHeader.Tuh_direct = false;
                //if (_isRes == false)
                //{
                //    _SerHeader.Tuh_ischek_itmstus = false;
                //}
                //else
                //{
                //    _SerHeader.Tuh_ischek_itmstus = true;
                //}
                _SerHeader.Tuh_ischek_simitm = true;
                _SerHeader.Tuh_ischek_reqqty = true;


                if (txtReceiptType.Text == "VHREG" || txtReceiptType.Text == "VHINS")
                {
                    _SerHeader.Tuh_doc_no = txtInvoice.Text;
                }
                else
                {
                    _SerHeader.Tuh_doc_no = "na";
                }

                foreach (ReptPickSerials line in _ResList)
                {
                    line.Tus_usrseq_no = _SerHeader.Tuh_usrseq_no;
                    line.Tus_cre_by = Convert.ToString(Session["UserID"]);
                    _tempSerialSave.Add(line);
                }
            }

            if (txtReceiptType.Text == "VHREG")
            {
                foreach (VehicalRegistration _reg in _regList)
                {
                    Int32 _vehSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "VHREG", 1, BaseCls.GlbUserComCode);
                    _reg.P_seq = _vehSeq;
                    _tempRegSave.Add(_reg);
                }
            }

            if (txtReceiptType.Text == "VHINS")
            {
                foreach (VehicleInsuarance _ins in _insList)
                {
                    Int32 _insSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "VHINS", 1, BaseCls.GlbUserComCode);
                    _ins.Svit_seq = _insSeq;
                    _ins.Svit_rec_tp = txtReceiptType.Text.Trim();
                    _tempInsSave.Add(_ins);
                }
            }

            if (txtReceiptType.Text == "ADINS")
            {
                foreach (VehicleInsuarance _ins in _insList)
                {
                    Int32 _insSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "VHINS", 1, BaseCls.GlbUserComCode);
                    _ins.Svit_seq = _insSeq;
                    _ins.Svit_rec_tp = txtReceiptType.Text.Trim();
                    _tempInsSave.Add(_ins);
                }
            }

            string QTNum;

            row_aff = (Int32)CHNLSVC.Sales.SaveNewReceiptTBS(_ReceiptHeader, _ReceiptDetailsSave, masterAuto, _SerHeader, _tempSerialSave, _tempRegSave, _tempInsSave, _sheduleDetails, masterAutoRecTp, _gvDetails, out QTNum);

            if (chkIsManual.Checked == true)
            {
                if (basePage.GlbUserDefLoca != basePage.GlbUserDefProf)
                {
                    CHNLSVC.Inventory.UpdateManualDocNo(basePage.GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManualReferance.Text), QTNum);
                }
            }

            if (row_aff == 1)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Successfully created.Receipt No: " + QTNum + "');", true);
                txtReceiptNo.Text = QTNum;
                //Clear_Data();
                //btnSave.Enabled = true;

                if (chkIsManual.Checked == true)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Successfully created.Receipt No: " + QTNum + "');", true);
                    //Clear_Data();
                    //btnSave.Enabled = true;
                    return;
                }
                else
                {
                    //if (MessageBox.Show("Successfully created.Receipt No: " + QTNum + " Do you want to get the print Now ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    //{
                    //    Clear_Data();
                    //    btnSave.Enabled = true;
                    //    return;
                    //}
                    //    else
                    //    {
                    //        string _cusCode = "";
                    //        _cusCode = txtCusCode.Text.Trim();
                    //        MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _cusCode, string.Empty, string.Empty, "C");

                    //        if (_itm.Mbe_sub_tp == "C.")
                    //        {
                    //            if (BaseCls.GlbDefChannel == "AUTO_DEL")
                    //            {
                    //                ReportViewer _view = new ReportViewer();
                    //                BaseCls.GlbReportName = string.Empty;
                    //                _view.GlbReportName = string.Empty;
                    //                //27-mar-2014 added by Nadeeka
                    //                GlbReportName = string.Empty;
                    //                //get permission
                    //                bool _permission = CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11055);
                    //                if (!_permission)
                    //                {// Done By Nadeeka 19-mar-2014
                    //                    _view.GlbReportName = "ConsignmentReceiptPrint.rpt";
                    //                }
                    //                else
                    //                {
                    //                    _view.GlbReportName = "DealerReceiptPrint.rpt";
                    //                }
                    //                _view.GlbReportDoc = QTNum;
                    //                _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    //                _view.Show();
                    //                _view = null;

                    //            }
                    //            else
                    //            {
                    //                ReportViewer _view = new ReportViewer();
                    //                BaseCls.GlbReportName = string.Empty;
                    //                _view.GlbReportName = string.Empty;
                    //                GlbReportName = string.Empty;
                    //                _view.GlbReportName = "ReceiptPrintDealers.rpt";
                    //                _view.GlbReportDoc = QTNum;
                    //                _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    //                _view.Show();
                    //                _view = null;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (chkIsMan.Checked == false)
                    //            {
                    //                if (BaseCls.GlbDefChannel == "AUTO_DEL")
                    //                {
                    //                    ReportViewer _view = new ReportViewer();
                    //                    BaseCls.GlbReportName = string.Empty;
                    //                    _view.GlbReportName = string.Empty;
                    //                    GlbReportName = string.Empty;
                    //                    _view.GlbReportName = "DealerReceiptPrint.rpt";
                    //                    _view.GlbReportDoc = QTNum;
                    //                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    //                    _view.Show();
                    //                    _view = null;

                    //                }
                    //                else
                    //                {
                    //                    ReportViewer _view = new ReportViewer();
                    //                    BaseCls.GlbReportName = string.Empty;
                    //                    _view.GlbReportName = string.Empty;
                    //                    GlbReportName = string.Empty;
                    //                    BaseCls.GlbReportTp = "REC";
                    //                    _view.GlbReportName = "ReceiptPrints.rpt";
                    //                    _view.GlbReportDoc = QTNum;
                    //                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    //                    _view.Show();
                    //                    _view = null;
                    //                }
                    //            }
                    //        }
                    //        Clear_Data();
                    //        btnSave.Enabled = true;
                    //    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(QTNum))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + QTNum + "');", true);
                    btnSave.Enabled = true;
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Creation Fail.');", true);
                    btnSave.Enabled = true;
                }
            }
        }

        #endregion
    }
}
