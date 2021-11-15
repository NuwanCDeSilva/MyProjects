using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Drawing.Drawing2D;
using FF.WindowsERPClient.HP;
using FF.WindowsERPClient.CommonSearch;
using FF.WindowsERPClient.Enquiries.Inventory;
using FF.WindowsERPClient.Sales;


//Web base system written by Prabhath (Original)
//Windows base system written by Prabhath on 07/12/2012 according to the web

namespace FF.WindowsERPClient.Enquiries
{
    public partial class CustomerMonitoring : Base
    {
        #region Column Names
        //Account
        //----------
        //Date
        //Type
        //Document
        //Manual No
        //Status

        //Guarantor Detail
        //----------------
        //Name
        //Address
        //Contact No
        //Mobile

        //Account Log
        //------------
        //Date
        //Description

        //--Payment--
        //Account Summary
        //---------------
        //date
        //Description
        //Manual Ref.
        //Dr Amt
        //Cr Amt
        //Balance

        //---Product---
        //product
        //---------
        //Invoice
        //Item
        //Description
        //Status
        //Qty
        //Do No
        //Date
        //Serial 1
        //Serial 2

        //Revert Detail
        //------------
        //Adj No
        //Date
        //Item
        //Rvt Amt

        //Revert Release Detail
        //-----------------------
        //Adj No
        //Date
        //Item
        //Rls Amt

        //Exchange Detail
        //---------------
        //Type
        //Invoice
        //Date
        //Item
        //Status
        //Qty
        //Tot Amt
        //Doc No
        //Serial 1
        //Serial 2

        //--Schedule--
        //Schedule Detail
        //---------------
        //Rental No
        //Account
        //Due Date
        //Rental Amt
        //Insurance
        //Veh Insurance
        //Total Rental

        //Re-Schedule Detail
        //-----------------
        //Resc Date
        //Rnt No
        //Account
        //Due Date
        //Rental
        //Insurance
        //Veh Insurance
        //Total Rental


        //--Transfer --
        //Location Transfer
        //--------------
        //Type
        //Account
        //Profit Center
        //Transfer Date
        //Transfer Balance

        //Customer Transfer
        //----------------
        //Account
        //Transfer Date
        //Name
        //Address
        //Contact
        //Mobile
        //Trasnfer Balance

        //--Insurance & registration --
        //HP Insurance Detail
        //----------
        //Ins Ref
        //Account
        //Date
        //Type
        //Insurance
        //tax Rate
        //Tax Amt
        //Comm. Rate
        //Comm. Amt
        //Pay @

        //Vehicle Insurance Detail
        //--------------------------



        //--Credit Sale--
        //Date
        //Type
        //Document
        //Manual no
        //Status
        //Due
        //Paid
        //Batch

        //Batch
        //Due
        //Paid

        //Receipt Detail
        //------------


        //--Black LIst--
        //account
        //Balance

        #endregion

        #region Grid View Names
        //gvAccount
        //gvGuarantor
        //gvAccountLog
        //gvAccountPaySummary
        //gvProduct
        //gvRevert
        //gvRevertRls
        //gvExchange
        //gvSchedule
        //gvScheduleHistory
        //gvLocationTransfer
        //gvCustomerTransfer
        //gvHpInsurance
        //gvCrOutStand
        //gvCrSummary
        //gvReceipt
        //gvAccountBalance
        #endregion

        #region Variables
        StringBuilder _p_keyword = new StringBuilder(string.Empty);
        StringBuilder _p_value = new StringBuilder(string.Empty);
        StringBuilder _p_relateddoctype = new StringBuilder(string.Empty);
        StringBuilder _p_relateddoc = new StringBuilder(string.Empty);
        StringBuilder _p_customer = new StringBuilder(string.Empty);
        List<HpAccount> AccountList = new List<HpAccount>();
        DataTable _BindDocs = new DataTable();
        Deposit_Bank_Pc_wise _objgrid;
        List<Deposit_Bank_Pc_wise> _lstgrid;
        string _accNumber = string.Empty;
        #endregion

        #region Ad-hoc Session
        private void Ad_hoc_Session()
        {
            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserDefLoca = "AAZPG";
            //BaseCls.GlbUserDefProf = "AAZPG";
            //BaseCls.GlbUserID = "ADMIN";

            //BaseCls.GlbUserComCode = "SGL";
            //BaseCls.GlbUserDefLoca = "SGMTR";
            //BaseCls.GlbUserDefProf = "SGMTR";
            //BaseCls.GlbUserID = "PRABHATH";

            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserDefLoca = "AAZPG";
            //BaseCls.GlbUserDefProf = "AAZPG";
            //BaseCls.GlbUserID = "PRABHATH";

        }
        #endregion

        #region Rooting for Search Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + string.Empty + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerId:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_Account_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccount;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtAccount.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoice;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtInvoice.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnSearch_Receipt_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Receipt);
                DataTable _result = CHNLSVC.CommonSearch.GetReceipts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReceipt;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtReceipt.Select();

                checkAvailability(txtReceipt.Text.Trim());
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustomer;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCustomer.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnCustomerId_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerId);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerId(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSSI;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtSSI.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearch_VehRegistration_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Rooting for Form Navigation
        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInvoice.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Account_Click(null, null);
        }

        private void txtAccount_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Account_Click(null, null);
        }

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtReceipt.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Invoice_Click(null, null);
        }

        private void txtInvoice_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }

        private void txtReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCustomer.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Receipt_Click(null, null);
        }

        private void txtReceipt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Receipt_Click(null, null);
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSSI.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_Customer_Click(null, null);
        }

        private void txtCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Customer_Click(null, null);
        }

        private void txtSSI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnClear.Focus();
        }

        private void txtVehRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tabControl1.TabPages[0].Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_VehRegistration_Click(null, null);
        }

        private void txtVehRegistration_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_VehRegistration_Click(null, null);
        }
        #endregion

        #region Rooting for Loading Area
        public CustomerMonitoring()
        {
            try
            {
                InitializeComponent();
                tabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
                Ad_hoc_Session();
                gvReceipt.AutoGenerateColumns = false;
                gvAccountBalance.AutoGenerateColumns = false;
                LoadReceiptGrid();
                pnlBlkLst.Size = new Size(592, 279);
                BlackListPermission();
               // pnlMobno.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void BlackListPermission()
        {
            try
            {
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "BLKST"))
                {
                    radHd.Visible = true;
                    radHd.Enabled = true;
                    radSR.Visible = false;
                }
                else
                {
                    radSR.Visible = false;
                    radSR.Enabled = false;
                    radHd.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void CustomerMonitoring_Load(object sender, EventArgs e)
        {
            try
            {
                this.Location = new Point(0, 0);
                ClearScreen(true, false);
                ucHpAccountSummary1.BorderStyle = BorderStyle.None;
                ucHpAccountSummary1.Clear();
                MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                if (mst_com != null)
                    lblInsu.Text = mst_com.Mc_anal3 + " Payment Details";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        #region Rooting for Binding Area
        private void ClearScreen(bool _isClearParameters, bool _isClearGrid)
        {
            try
            {
                if (_isClearParameters)
                {
                    txtAccount.Clear();
                    txtCustomer.Clear();
                    txtInvoice.Clear();
                    txtSSI.Clear();
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16125))                    
                {
                    btnViewVou.Enabled = true;
                }
                else
                {
                    btnViewVou.Enabled = false;
                }
                lblCusDistrict.Text = string.Empty;
                lblCusDOB.Text = string.Empty;
                lblCusEmail.Text = string.Empty;
                lblCusMobile.Text = string.Empty;
                lblCusName.Text = string.Empty;
                lblCusNorAdd.Text = string.Empty;
                lblCusPostal.Text = string.Empty;
                lblCusPreAdd.Text = string.Empty;
                lblCusProvince.Text = string.Empty;
                lblCusSSI.Text = string.Empty;
                lblCusSTxRegistered.Text = string.Empty;
                lblCusSTxRegNo.Text = string.Empty;
                lblCusTown.Text = string.Empty;
                lblCusTxEx.Text = string.Empty;
                lblCusTxRegistered.Text = string.Empty;
                lblCusTxRegNo.Text = string.Empty;
                lblCusWrkAdd.Text = string.Empty;
                lblCusWrkDept.Text = string.Empty;
                lblCusWrkDesignation.Text = string.Empty;
                lblCusWrkFax.Text = string.Empty;
                lblCusWrkName.Text = string.Empty;
                lblCusWrkPhone.Text = string.Empty;
                lblCusWrkProf.Text = string.Empty;
                pnlBalance.Visible = false;
                txtInterest.Text = string.Empty;
                txtCapital.Text = string.Empty;
                _accNumber = string.Empty;
                if (_isClearGrid)
                {
                    //BindGuarantor(string.Empty);
                    DataTable _dt = new DataTable();
                    gvGuarantor.DataSource = _dt;
                    BindAccountSummary(string.Empty);
                    BindCustomerAccountSchedule(string.Empty);
                    BindAccountScheduleHistory(string.Empty);
                    BindRevertAccountDetail(string.Empty);
                    BindRevertReleaseAccountDetail(string.Empty);
                    BindExchangeDetail(string.Empty);
                    BindHireSaleAccountBalance(string.Empty, string.Empty);
                    BindCustomerAccountBalance(string.Empty);
                    BindAccountLocationTransfer(string.Empty);
                    BindAccountCustomerTrasnfer(string.Empty);
                    BindAccountVehicleInsurance(string.Empty);
                    //BindAccountRegistration(string.Empty);
                    _dt = new DataTable();
                    gvVhRegistration.DataSource = _dt;
                    BindAccountDiriyaDetail(string.Empty);
                    BindProductDetail(string.Empty);
                    BindCustomerDocumentWithSettlement(string.Empty);
                    BindCustomerPaymentSummary(string.Empty);
                    BindInvoiceReceipt(string.Empty);
                }
                MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                if (mst_com != null)
                    lblInsu.Text = mst_com.Mc_anal3 + " Payment Details";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        bool _isCustomerFocus = false;
        private void LoadCustomerDetail(string _customer)
        {
            try
            {
                MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _customer, string.Empty, string.Empty, "C");


                lblCusDistrict.Text = _entity.Mbe_distric_cd;
                lblCusDOB.Text = _entity.Mbe_dob.Date.ToShortDateString() == "01/01/0001" ? string.Empty : _entity.Mbe_dob.Date.ToShortDateString();
                lblCusEmail.Text = _entity.Mbe_email;
                lblCusMobile.Text = _entity.Mbe_mob;
                lblCusName.Text = _entity.Mbe_name;
                lblCusNorAdd.Text = _entity.Mbe_add1 + " " + _entity.Mbe_add2;
                lblCusPostal.Text = _entity.Mbe_postal_cd;
                lblCusPreAdd.Text = _entity.Mbe_cr_add1 + " " + _entity.Mbe_cr_add2;
                lblCusProvince.Text = _entity.Mbe_province_cd;
                lblCusSSI.Text = _entity.Mbe_nic + (string.IsNullOrEmpty(_entity.Mbe_pp_no) ? string.Empty : "/ ") + _entity.Mbe_pp_no + (string.IsNullOrEmpty(_entity.Mbe_dl_no) ? string.Empty : "/ ") + _entity.Mbe_dl_no;
                lblCusSTxRegistered.Text = _entity.Mbe_is_svat == true ? "Yes" : "No";
                lblCusSTxRegNo.Text = _entity.Mbe_svat_no;
                lblCusTown.Text = _entity.Mbe_town_cd;
                lblCusTxEx.Text = _entity.Mbe_tax_ex == true ? "Yes" : "No";
                lblCusTxRegistered.Text = _entity.Mbe_is_tax == true ? "Yes" : "No";
                lblCusTxRegNo.Text = _entity.Mbe_tax_no;
                lblCusWrkAdd.Text = _entity.Mbe_wr_add1 + " " + _entity.Mbe_wr_add2;
                lblCusWrkDept.Text = _entity.Mbe_wr_dept;
                lblCusWrkDesignation.Text = _entity.Mbe_wr_designation;
                lblCusWrkFax.Text = _entity.Mbe_wr_fax;
                lblCusWrkName.Text = _entity.Mbe_wr_com_name;
                lblCusWrkPhone.Text = _entity.Mbe_wr_tel;
                lblCusWrkProf.Text = _entity.Mbe_wr_proffesion;
                _isCustomerFocus = true;
                txtCustomer.Text = _customer;
                _isCustomerFocus = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }
        private void LoadEverything(string _customer, string _document)
        {

            try
            {


                BindProductDetail(_document);

                if (Convert.ToString(_p_relateddoctype) == "HS")
                {
                    BindRevertAccountDetail(_document);
                    BindRevertReleaseAccountDetail(_document);
                    BindExchangeDetail(_document);
                    _accNumber = _document;
                    BindAccountSummary(_document);
                    BindHireSaleAccountBalance(_document, _customer);
                }
                //if (tabControl1.SelectedTab.Name == tbpBlackList.Name)
                //{
                //    BindHireSaleAccountBalance(_document, _customer);
                //}
                if (tabControl1.SelectedTab.Name == tbpCreditSale.Name)
                {
                    BindCustomerDocumentWithSettlement(_customer);
                    BindCustomerPaymentSummary(_customer);
                    BindInvoiceReceipt(_document);
                }
                if (tabControl1.SelectedTab.Name == tbpInsurance.Name)
                {
                    BindAccountDiriyaDetail(_document);
                    BindAccountVehicleInsurance(_document);
                    BindAccountRegistration(_document);
                }
                if (tabControl1.SelectedTab.Name == tbpPayment.Name && Convert.ToString(_p_relateddoctype) == "HS")
                {
                    BindCustomerAccountBalance(_document);
                    BindAccountGeneralSummary(_document);
                }
                if (tabControl1.SelectedTab.Name == tbpProduct.Name)
                {
                    BindGuarantor(_document);
                }
                if (tabControl1.SelectedTab.Name == tbpSchedule.Name)
                {
                    BindCustomerAccountSchedule(_document);
                    BindAccountScheduleHistory(_document);
                }
                if (tabControl1.SelectedTab.Name == tbpTransfer.Name)
                {
                    BindAccountLocationTransfer(_document);
                    BindAccountCustomerTrasnfer(_document);
                }
                if (tabControl1.SelectedTab.Name == tbpLoyalaty.Name)
                {
                    BindLoyalatyDetails(_customer);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //by darshana on 07-05-2014
        private void LoadRelatedDocumentforDupCus(string _customer)
        {
            try
            {
                DataTable _docs = new DataTable();
                

                //GetMonitorByCustomerDocument
                if ((_customer == "CASH" || _customer == "N/A") && !string.IsNullOrEmpty(txtInvoice.Text))
                {
                    _docs = CHNLSVC.Sales.GetCusMonitorByDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text);
                    if (_docs != null && _docs.Rows.Count > 0)
                    {
                        _BindDocs.Merge(_docs);
                        gvAccount.DataSource = _BindDocs;
                    }

                    InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoice.Text);

                    if (_hdr != null)
                    {
                        lblCusName.Text = _hdr.Sah_cus_name;
                        lblCusNorAdd.Text = _hdr.Sah_cus_add1 + "," + _hdr.Sah_cus_add2;
                    }
                }
                else
                {
                    _docs = CHNLSVC.Sales.GetMonitorByCustomerDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customer);
                    if (_docs != null && _docs.Rows.Count > 0)
                    {
                        _BindDocs.Merge(_docs);
                        gvAccount.DataSource = _BindDocs;
                    }
                }

                //if (_docs == null || _docs.Rows.Count <= 0)
                //{
                //    List<HpAccount> cusList = CHNLSVC.Sales.GetAccByCustType(BaseCls.GlbUserComCode, _customer, "G");

                //    if (cusList != null && cusList.Count > 0)
                //    {
                //        string customer = CHNLSVC.Sales.GetMonitorCustomer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cusList[0].Hpa_acc_no, "ACCOUNT");

                //        _docs = CHNLSVC.Sales.GetMonitorByCustomerDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, customer);
                //        gvAccount.DataSource = _docs;
                //    }
                //}

                //if (_docs == null || _docs.Rows.Count <= 0)
                //{
                //    MessageBox.Show("This customer does not having any entry for the invoice.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (_docs != null)
                    if (_docs.Rows.Count == 1)
                    {
                        _p_relateddoctype = new StringBuilder(_docs.Rows[0].Field<string>("Type"));
                        _p_relateddoc = new StringBuilder(_docs.Rows[0].Field<string>("Document"));
                        _p_customer = new StringBuilder(_customer);
                        LoadEverything(_customer.ToString(), _p_relateddoc.ToString());
                    }
                    else if (_p_keyword.ToString().Contains("ACCOUNT"))//Added by Prabhath on 25/11/2013
                    {
                        DataTable _single = _docs.AsEnumerable().Where(x => x.Field<string>("Document").Contains(txtAccount.Text.Trim())).CopyToDataTable();
                        _p_relateddoctype = new StringBuilder(_single.Rows[0].Field<string>("Type"));
                        _p_relateddoc = new StringBuilder(_single.Rows[0].Field<string>("Document"));
                        _p_customer = new StringBuilder(_customer);
                        LoadEverything(_customer.ToString(), _p_relateddoc.ToString());
                    }
                    else if (_p_keyword.ToString().Contains("INVOICE"))//Added by Prabhath on 25/11/2013
                    {
                        DataTable _single = _docs.AsEnumerable().Where(x => x.Field<string>("Document").Contains(txtInvoice.Text.Trim())).CopyToDataTable();
                        _p_relateddoctype = new StringBuilder(_single.Rows[0].Field<string>("Type"));
                        _p_relateddoc = new StringBuilder(_single.Rows[0].Field<string>("Document"));
                        _p_customer = new StringBuilder(_customer);
                        LoadEverything(_customer.ToString(), _p_relateddoc.ToString());
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadRelatedDocument(string _customer)
        {
            try
            {
                DataTable _docs = new DataTable();
                //GetMonitorByCustomerDocument
                if ((_customer == "CASH" || _customer == "N/A") && !string.IsNullOrEmpty(txtInvoice.Text))
                {
                    _docs = CHNLSVC.Sales.GetCusMonitorByDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text);
                    gvAccount.DataSource = _docs;

                    InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoice.Text);

                    if (_hdr != null)
                    {
                        lblCusName.Text = _hdr.Sah_cus_name;
                        lblCusNorAdd.Text = _hdr.Sah_cus_add1 + "," + _hdr.Sah_cus_add2;
                    }
                }
                else
                {
                    _docs = CHNLSVC.Sales.GetMonitorByCustomerDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customer);
                    gvAccount.DataSource = _docs;
                }

                //Tharaka 2015-08-04
                if (AccountList != null && AccountList.Count > 0)
                {
                    DataTable dtByAcc = CHNLSVC.Sales.GETCUST_BY_ACC(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, AccountList[0].Hpa_acc_no);
                    _docs.Merge(dtByAcc);
                }

                //if (_docs == null || _docs.Rows.Count <= 0)
                //{
                //    List<HpAccount> cusList = CHNLSVC.Sales.GetAccByCustType(BaseCls.GlbUserComCode, _customer, "G");

                //    if (cusList != null && cusList.Count > 0)
                //    {
                //        string customer = CHNLSVC.Sales.GetMonitorCustomer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cusList[0].Hpa_acc_no, "ACCOUNT");

                //        _docs = CHNLSVC.Sales.GetMonitorByCustomerDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, customer);
                //        gvAccount.DataSource = _docs;
                //    }
                //}

                if (_docs == null || _docs.Rows.Count <= 0)
                {
                    MessageBox.Show("No transactions found for this customer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_docs != null)
                    if (_docs.Rows.Count == 1)
                    {
                        _p_relateddoctype = new StringBuilder(_docs.Rows[0].Field<string>("Type"));
                        _p_relateddoc = new StringBuilder(_docs.Rows[0].Field<string>("Document"));
                        _p_customer = new StringBuilder(_customer);
                        LoadEverything(_customer.ToString(), _p_relateddoc.ToString());
                    }
                    else if ( _p_keyword.ToString().Contains("ACCOUNT"))//Added by Prabhath on 25/11/2013
                    {
                        DataTable _single = _docs.AsEnumerable().Where(x => x.Field<string>("Document").Contains(txtAccount.Text.Trim())).CopyToDataTable();
                        _p_relateddoctype = new StringBuilder(_single.Rows[0].Field<string>("Type"));
                        _p_relateddoc = new StringBuilder(_single.Rows[0].Field<string>("Document"));
                        _p_customer = new StringBuilder(_customer);
                        LoadEverything(_customer.ToString(), _p_relateddoc.ToString());
                    }
                    else if (_p_keyword.ToString().Contains("INVOICE"))//Added by Prabhath on 25/11/2013
                    {
                        DataTable _single = _docs.AsEnumerable().Where(x => x.Field<string>("Document").Contains(txtInvoice.Text.Trim())).CopyToDataTable();
                        _p_relateddoctype = new StringBuilder(_single.Rows[0].Field<string>("Type"));
                        _p_relateddoc = new StringBuilder(_single.Rows[0].Field<string>("Document"));
                        _p_customer = new StringBuilder(_customer);
                        LoadEverything(_customer.ToString(), _p_relateddoc.ToString());
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //added by darshana on 09-09-2013
        private void BindLoyalatyDetails(string _customer)
        {
            try
            {
                List<LoyaltyMemeber> _loyalCus = new List<LoyaltyMemeber>();
                _loyalCus = CHNLSVC.Sales.GetCurrentLoyalByCus(_customer,null);

                gvLoyalaty.AutoGenerateColumns = false;
                gvLoyalaty.DataSource = new List<LoyaltyMemeber>();
                gvLoyalaty.DataSource = _loyalCus;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void BindGuarantor(string _account)
        {
            try
            {
                gvGuarantor.AutoGenerateColumns = false;
                DataTable _guarantor = CHNLSVC.Sales.GetHpGuarantor("G", _account);
                gvGuarantor.DataSource = _guarantor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void BindAccountSummary(string _account)
        {
            try
            {
                DataTable _summary = CHNLSVC.Sales.GetAccountSummary(_account);
                gvAccountLog.DataSource = _summary;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void BindHireSaleAccountBalance(string _account, string _customer)
        {
            try
            {
                DataTable _accbal = CHNLSVC.Sales.GetHireSaleAccountBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customer, DateTime.Now.Date);
                gvAccountBalance.DataSource = _accbal;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindCustomerDocumentWithSettlement(string _customer)
        {
            try
            {
                if (_customer != string.Empty)
                {
                    DataTable _settle = CHNLSVC.Sales.GetCustomerDocumentWithSettlement(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customer, 1);
                    gvCrOutStand.DataSource = _settle;
                }
                else
                {
                    gvCrOutStand.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindCustomerPaymentSummary(string _customer)
        {
            try
            {
                if (_customer != string.Empty)
                {
                    DataTable _paysum = CHNLSVC.Sales.GetCustomerPaymentSummary(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customer);
                    gvCrSummary.DataSource = _paysum;
                }
                else
                {
                    gvCrSummary.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindInvoiceReceipt(string _account)
        {
            try
            {
                if (_account != string.Empty)
                {
                    DataTable _receipt = CHNLSVC.Sales.GetInvoiceReceipt(_account);
                    gvReceipt.DataSource = _receipt;
                    //kapila 20/9/2016
                    DataTable _adj = CHNLSVC.Sales.GetInvoiceAdj(_account);
                    gvAdj.DataSource = _adj;
                }
                else
                {
                    gvReceipt.DataSource = null;
                    gvAdj.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindAccountDiriyaDetail(string _account)
        {
            try
            {
                if (_account != string.Empty)
                {
                    DataTable _diriya = CHNLSVC.Sales.GetAccountDiriyaDetail(_account);
                    gvHpInsurance.DataSource = _diriya;
                }
                else
                {
                    gvHpInsurance.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindCustomerAccountBalance(string _account)
        {
            try
            {
                DataTable _cusAccBal = CHNLSVC.Sales.GetCustomerAccountBalance(_account);
                gvAccountPaySummary.DataSource = _cusAccBal;


                if (gvAccountPaySummary.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in gvAccountPaySummary.Rows)
                    {
                        if (row.Cells["Column21"].Value != null)
                        {
                            if (row.Cells["Column21"].Value.ToString() == "1")
                            {
                                row.DefaultCellStyle.BackColor = Color.OrangeRed;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void BindAccountGeneralSummary(string _account)
        {
            try
            {
                string[] _nos = _account.Split('-');

                if (IsNumeric(_nos[1]) == false)
                {
                    MessageBox.Show("Account No is incorrect", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Int32 _no = Convert.ToInt32(_nos[1]);
                AccountList = CHNLSVC.Sales.GetHPAccount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _no, string.Empty);
                HpAccount account = new HpAccount();
                if (AccountList != null)
                {
                    bool _isAny = false;
                    foreach (HpAccount acc in AccountList)
                    {
                        if (_account == acc.Hpa_acc_no)
                        {
                            account = acc;
                            _isAny = true;
                        }
                    }

                    if (!_isAny)
                    {
                        MessageBox.Show("You are not allow to review other shop account(s) ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (account != null && !string.IsNullOrEmpty(account.Hpa_acc_no))
                        ucHpAccountSummary1.set_all_values(account, BaseCls.GlbUserDefProf, DateTime.Now.Date, BaseCls.GlbUserDefProf);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindProductDetail(string _account)
        {
            try
            {
                if (_account != string.Empty)
                {
                    //DataTable _product = CHNLSVC.Sales.GetInvoiceWithSerial(_account);
                    DataTable _product = CHNLSVC.Sales.GetInvoiceWithSerialCusMonitor(BaseCls.GlbUserComCode, _account);
                    gvProduct.DataSource = _product;
                }
                else
                {
                    gvProduct.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindRevertAccountDetail(string _account)
        {
            try
            {
                DataTable _revert = CHNLSVC.Sales.GetRevertAccountDetail(_account);
                gvRevert.DataSource = _revert;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindRevertReleaseAccountDetail(string _account)
        {
            try
            {
                DataTable _revertrls = CHNLSVC.Sales.GetRevertReleaseAccountDetail(_account);
                gvRevertRls.DataSource = _revertrls;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindExchangeDetail(string _account)
        {
            try
            {
                DataTable _exch = CHNLSVC.Sales.GetExchangeDetail(BaseCls.GlbUserComCode, _account);
                gvExchange.DataSource = _exch;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindCustomerAccountSchedule(string _account)
        {
            try
            {
                DataTable _sch = CHNLSVC.Sales.GetCustomerAccountSchedule(_account);
                gvSchedule.DataSource = _sch;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindAccountScheduleHistory(string _account)
        {
            try
            {
                //GetAccountScheduleHistory
                DataTable _his = CHNLSVC.Sales.GetAccountScheduleHistory(_account);
                gvScheduleHistory.DataSource = _his;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void BindAccountLocationTransfer(string _account)
        {
            try
            {
                DataTable _accTrf = CHNLSVC.Sales.GetAccountTransfer(_account);
                gvLocationTransfer.DataSource = _account;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindAccountCustomerTrasnfer(string _account)
        {
            try
            {
                DataTable _cusTrf = CHNLSVC.Sales.GetAccountCustomerTrasnfer(_account);
                gvCustomerTransfer.DataSource = _cusTrf;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }
        private void BindAccountVehicleInsurance(string _account)
        {
            try
            {
                DataTable _cusTrf = CHNLSVC.Sales.GetVehicleInsurance(_account);
                gvVhlInsurance.DataSource = _cusTrf;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }


        private void BindAccountRegistration(string _account)
        {
            try
            {
                DataTable _dt = CHNLSVC.Sales.GetVehicalRegistrationReciept(_account);
                gvVhRegistration.DataSource = _dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }



        #endregion

        #region Rooting for Grid Mouse Over
        int gvAccountOldRow = 0;
        private void gvAccount_MouseMove(object sender, MouseEventArgs e)
        {

            DataGridView.HitTestInfo hti = gvAccount.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvAccountOldRow)
            {
                gvAccount.Rows[gvAccountOldRow].Selected = false;
                gvAccount.Rows[hti.RowIndex].Selected = true;
                gvAccountOldRow = hti.RowIndex;
            }
        }
        int gvGuarantorOldRow = 0;
        private void gvGuarantor_MouseMove(object sender, MouseEventArgs e)
        {

            DataGridView.HitTestInfo hti = gvGuarantor.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvGuarantorOldRow)
            {
                gvGuarantor.Rows[gvGuarantorOldRow].Selected = false;
                gvGuarantor.Rows[hti.RowIndex].Selected = true;
                gvGuarantorOldRow = hti.RowIndex;
            }
        }
        int gvAccountLogOldRow = 0;
        private void gvAccountLog_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvAccountLog.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvAccountLogOldRow)
            {
                gvAccountLog.Rows[gvAccountLogOldRow].Selected = false;
                gvAccountLog.Rows[hti.RowIndex].Selected = true;
                gvAccountLogOldRow = hti.RowIndex;
            }
        }
        int gvScheduleOldRow = 0;
        private void gvSchedule_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvSchedule.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvScheduleOldRow)
            {
                gvSchedule.Rows[gvScheduleOldRow].Selected = false;
                gvSchedule.Rows[hti.RowIndex].Selected = true;
                gvScheduleOldRow = hti.RowIndex;
            }
        }
        int gvScheduleHistoryOldRow = 0;
        private void gvScheduleHistory_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvScheduleHistory.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvScheduleHistoryOldRow)
            {
                gvScheduleHistory.Rows[gvScheduleHistoryOldRow].Selected = false;
                gvScheduleHistory.Rows[hti.RowIndex].Selected = true;
                gvScheduleHistoryOldRow = hti.RowIndex;
            }
        }
        int gvRevertOldRow = 0;
        private void gvRevert_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvRevert.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvRevertOldRow)
            {
                gvRevert.Rows[gvRevertOldRow].Selected = false;
                gvRevert.Rows[hti.RowIndex].Selected = true;
                gvRevertOldRow = hti.RowIndex;
            }
        }
        int gvRevertRlsOldRow = 0;
        private void gvRevertRls_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvRevertRls.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvRevertRlsOldRow)
            {
                gvRevertRls.Rows[gvRevertRlsOldRow].Selected = false;
                gvRevertRls.Rows[hti.RowIndex].Selected = true;
                gvRevertRlsOldRow = hti.RowIndex;
            }
        }
        int gvExchangeOldRow = 0;
        private void gvExchange_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvExchange.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvExchangeOldRow)
            {
                gvExchange.Rows[gvExchangeOldRow].Selected = false;
                gvExchange.Rows[hti.RowIndex].Selected = true;
                gvExchangeOldRow = hti.RowIndex;
            }
        }
        int gvAccountBalanceOldRow = 0;
        private void gvAccountBalance_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvAccountBalance.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvAccountBalanceOldRow)
            {
                gvAccountBalance.Rows[gvAccountBalanceOldRow].Selected = false;
                gvAccountBalance.Rows[hti.RowIndex].Selected = true;
                gvAccountBalanceOldRow = hti.RowIndex;
            }
        }
        int gvAccountPaySummaryOldRow = 0;
        private void gvAccountPaySummary_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvAccountPaySummary.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvAccountPaySummaryOldRow)
            {
                gvAccountPaySummary.Rows[gvAccountPaySummaryOldRow].Selected = false;
                gvAccountPaySummary.Rows[hti.RowIndex].Selected = true;
                gvAccountPaySummaryOldRow = hti.RowIndex;
            }
        }
        int gvLocationTransferOldRow = 0;
        private void gvLocationTransfer_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvLocationTransfer.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvLocationTransferOldRow)
            {
                gvLocationTransfer.Rows[gvLocationTransferOldRow].Selected = false;
                gvLocationTransfer.Rows[hti.RowIndex].Selected = true;
                gvLocationTransferOldRow = hti.RowIndex;
            }
        }
        int gvCustomerTransferOldRow = 0;
        private void gvCustomerTransfer_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvCustomerTransfer.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvCustomerTransferOldRow)
            {
                gvCustomerTransfer.Rows[gvCustomerTransferOldRow].Selected = false;
                gvCustomerTransfer.Rows[hti.RowIndex].Selected = true;
                gvCustomerTransferOldRow = hti.RowIndex;
            }
        }
        int gvHpInsuranceOldRow = 0;
        private void gvHpInsurance_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvHpInsurance.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvHpInsuranceOldRow)
            {
                gvHpInsurance.Rows[gvHpInsuranceOldRow].Selected = false;
                gvHpInsurance.Rows[hti.RowIndex].Selected = true;
                gvHpInsuranceOldRow = hti.RowIndex;
            }
        }
        int gvProductOldRow = 0;
        private void gvProduct_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvProduct.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvProductOldRow)
            {
                gvProduct.Rows[gvProductOldRow].Selected = false;
                gvProduct.Rows[hti.RowIndex].Selected = true;
                gvProductOldRow = hti.RowIndex;
            }
        }
        int gvCrOutStandOldRow = 0;
        private void gvCrOutStand_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvCrOutStand.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvCrOutStandOldRow)
            {
                gvCrOutStand.Rows[gvCrOutStandOldRow].Selected = false;
                gvCrOutStand.Rows[hti.RowIndex].Selected = true;
                gvCrOutStandOldRow = hti.RowIndex;
            }
        }
        int gvCrSummaryOldRow = 0;
        private void gvCrSummary_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvCrSummary.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvCrSummaryOldRow)
            {
                gvCrSummary.Rows[gvCrSummaryOldRow].Selected = false;
                gvCrSummary.Rows[hti.RowIndex].Selected = true;
                gvCrSummaryOldRow = hti.RowIndex;
            }
        }
        int gvReceiptOldRow = 0;
        private void gvReceipt_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = gvReceipt.HitTest(e.X, e.Y);
            if (hti.RowIndex >= 0 && hti.RowIndex != gvReceiptOldRow)
            {
                gvReceipt.Rows[gvReceiptOldRow].Selected = false;
                gvReceipt.Rows[hti.RowIndex].Selected = true;
                gvReceiptOldRow = hti.RowIndex;
            }
        }

        #endregion

        #region Rooting for Event-Firing Area
        private void CheckBlackListCustomer(string _customer)
        {
            lblBlackList.Text = string.Empty;
            DataTable _tbl = CHNLSVC.Sales.CheckBlackListCustomer(_customer);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                MessageBox.Show("This customer is black listed!.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblBlackList.Text = "Black Listed Customer";
            }

        }
        private void gvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 _row = e.RowIndex;
                if (_row > -1)
                {
                    StringBuilder _document = new StringBuilder(gvAccount.Rows[_row].Cells[2].Value.ToString());
                    StringBuilder _type = new StringBuilder(gvAccount.Rows[_row].Cells[1].Value.ToString());
                    _p_relateddoctype = new StringBuilder(Convert.ToString(_type));
                    _p_relateddoc = new StringBuilder(Convert.ToString(_document));
                    // string _customer=


                    LoadEverything(_p_customer.ToString(), _p_relateddoc.ToString());
                    ucHpAccountSummary1.Uc_ins_balance = CHNLSVC.Financial.Isurance_balance(_p_relateddoc.ToString());//add by tharanga 2017/11/24
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtAccount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAccount.Text.Trim())) return;
                this.Cursor = Cursors.WaitCursor;
                if (IsNumeric(txtAccount.Text) == false)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Invalid account sequence no");
                    return;
                }

                bool _byinvoice = false;
                AccountList = CHNLSVC.Sales.GetHPAccount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(txtAccount.Text.Trim()), string.Empty);
                if (AccountList != null)
                    if (AccountList.Count > 0)
                    {
                        if (AccountList.Count > 1)
                        {
                            TextBox _selected = new TextBox();
                            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                            _CommonSearch.IsSearchEnter = false;
                            _CommonSearch.ReturnIndex = 1;
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                            _CommonSearch.dvResult.DataSource = _result;
                            _CommonSearch.BindUCtrlDDLData(_result);
                            _CommonSearch.obj_TragetTextBox = _selected;
                            this.Cursor = Cursors.Default;
                            _CommonSearch.ShowDialog();
                            if (string.IsNullOrEmpty(_selected.Text)) return;
                            if (!_selected.Text.Contains("-")) return;
                            AccountList = AccountList.Where(x => x.Hpa_acc_no == _selected.Text).ToList();
                        }


                        if (!_byinvoice) _p_keyword = new StringBuilder("ACCOUNT");
                        _p_value = new StringBuilder();
                        _p_value = new StringBuilder(AccountList[0].Hpa_acc_no);

                        ClearScreen(false, true);
                        StringBuilder _particularCustomer = new StringBuilder(CHNLSVC.Sales.GetMonitorCustomer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _p_value.ToString(), _p_keyword.ToString()));
                        if (string.IsNullOrEmpty(_particularCustomer.ToString())) 
                        {
                            MessageBox.Show("No data found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; 
                        }
                        _p_customer = new StringBuilder(Convert.ToString(_particularCustomer));
                        LoadCustomerDetail(_particularCustomer.ToString());
                        LoadRelatedDocument(_particularCustomer.ToString());
                        CheckBlackListCustomer(_particularCustomer.ToString());

                        viewReminds(AccountList[0].Hpa_acc_no);
                    
                    }
                this.Cursor = Cursors.Default;
                txtInvoice.Clear();
                txtReceipt.Clear();
                txtSSI.Clear();
                txtCustomer.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoice.Text.Trim())) return;
                this.Cursor = Cursors.WaitCursor;
                _p_keyword = new StringBuilder("INVOICE");
                _p_value = new StringBuilder(txtInvoice.Text.Trim());
                ClearScreen(false, true);
                StringBuilder _particularCustomer = new StringBuilder(CHNLSVC.Sales.GetMonitorCustomer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _p_value.ToString(), _p_keyword.ToString()));
                if (string.IsNullOrEmpty(_particularCustomer.ToString())) { MessageBox.Show("No data found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                _p_customer = new StringBuilder(Convert.ToString(_particularCustomer));
                LoadCustomerDetail(_particularCustomer.ToString());
                LoadRelatedDocument(_particularCustomer.ToString());
                CheckBlackListCustomer(_particularCustomer.ToString());
                this.Cursor = Cursors.Default;
                txtAccount.Clear();
                txtReceipt.Clear();
                txtSSI.Clear();
                txtCustomer.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtReceipt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReceipt.Text.Trim())) return;
                this.Cursor = Cursors.WaitCursor;
                _p_keyword = new StringBuilder("RECEIPT");
                _p_value = new StringBuilder(txtReceipt.Text.Trim());
                checkAvailability(_p_value.ToString());
                ClearScreen(false, true);

                StringBuilder _particularCustomer = new StringBuilder(CHNLSVC.Sales.GetMonitorCustomer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _p_value.ToString(), _p_keyword.ToString()));
                if (string.IsNullOrEmpty(_particularCustomer.ToString())) 
                { 
                    //MessageBox.Show("No data found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; 
                }
                _p_customer = new StringBuilder(Convert.ToString(_particularCustomer));
                LoadCustomerDetail(_particularCustomer.ToString());
                LoadRelatedDocument(_particularCustomer.ToString());
                CheckBlackListCustomer(_particularCustomer.ToString());
                txtAccount.Clear();
                txtInvoice.Clear();
                txtSSI.Clear();
                txtCustomer.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtSSI_Leave(object sender, EventArgs e)
        {

            try
            {
                DateTime start = DateTime.Now;
                if (string.IsNullOrEmpty(txtSSI.Text.Trim())) return;
                this.Cursor = Cursors.WaitCursor;
                _p_keyword = new StringBuilder("SSI");
                _p_value = new StringBuilder(txtSSI.Text.Trim());
                ClearScreen(false, true);
                DateTime clearend = DateTime.Now;
                TimeSpan clear = clearend - start;
                _BindDocs = new DataTable();
                gvAccount.DataSource = _BindDocs;

                //Test
                DataTable mobnos = new DataTable();
               
                mobnos = CHNLSVC.Sales.getcustomermobno(BaseCls.GlbUserComCode,_p_value.ToString().Trim(),"C");
                if(mobnos.Rows.Count > 1)
                {
                
                    CusmobileNo objcus = new CusmobileNo();
                  //  CusmobileNo objcus1 = new CusmobileNo(mobnos);
                    objcus.GetMobileNos(mobnos);
                    objcus.ShowDialog();
                    txtCustomer.Text = objcus.cusmobno;
                    //CusmobileNo objcus1 = new CusmobileNo(mobno
                                  
                                   
                }

                //check multiple Add By Chamal 24/04/2014
                //List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, _p_value.ToString().Trim(), "", "", "", "", 1);
                //ADDED BY DARSHANA ON 25-04-2014
                List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, _p_value.ToString().Trim(), string.Empty, "C");
                if (_custList != null && _custList.Count > 1 && _p_value.ToString().ToUpper() != "N/A")
                {
                    string _custNIC = "Duplicate customers found!\n";
                    foreach (var _nicCust in _custList)
                    {
                        _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                        if (_isCustomerFocus == true) return;
                        if (string.IsNullOrEmpty(_nicCust.Mbe_cd)) return;
                        this.Cursor = Cursors.WaitCursor;
                        _p_keyword = new StringBuilder("CUSTOMER");
                        _p_value = new StringBuilder(_nicCust.Mbe_cd.Trim());
                        //ClearScreen(false, true);
                        StringBuilder _particularCustomerNIC = new StringBuilder(CHNLSVC.Sales.GetMonitorCustomer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _p_value.ToString(), _p_keyword.ToString()));
                        if (string.IsNullOrEmpty(_particularCustomerNIC.ToString())) { MessageBox.Show("No data found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                        _p_customer = new StringBuilder(Convert.ToString(_particularCustomerNIC));
                        LoadCustomerDetail(_particularCustomerNIC.ToString());
                        LoadRelatedDocumentforDupCus(_particularCustomerNIC.ToString());
                        CheckBlackListCustomer(_particularCustomerNIC.ToString());
                        txtAccount.Clear();
                        txtInvoice.Clear();
                        txtReceipt.Clear();
                        txtCustomer.Clear();
                    }
                    _custNIC = _custNIC + "\nAll above customer codes related details are captured.";
                    MessageBox.Show(_custNIC, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                StringBuilder _particularCustomer = new StringBuilder(CHNLSVC.Sales.GetMonitorCustomer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _p_value.ToString(), _p_keyword.ToString()));
                DateTime ustomerend = DateTime.Now;
                TimeSpan clcustomerear = ustomerend - clearend;
                if (string.IsNullOrEmpty(_particularCustomer.ToString())) { MessageBox.Show("No data found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                _p_customer = new StringBuilder(Convert.ToString(_particularCustomer));
                LoadCustomerDetail(_particularCustomer.ToString());
                DateTime loadcusend = DateTime.Now;
                TimeSpan loadcus = loadcusend - ustomerend;
                LoadRelatedDocument(_particularCustomer.ToString());
                DateTime reletedload = DateTime.Now;
                TimeSpan related = reletedload - loadcusend;
                CheckBlackListCustomer(_particularCustomer.ToString());
                this.Cursor = Cursors.Default;
                txtAccount.Clear();
                txtInvoice.Clear();
                txtReceipt.Clear();
                txtCustomer.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtCustomer_Leave(object sender, EventArgs e)
        {

            try
            {
                if (_isCustomerFocus == true) return;
                if (string.IsNullOrEmpty(txtCustomer.Text.Trim())) return;
                this.Cursor = Cursors.WaitCursor;
                _p_keyword = new StringBuilder("CUSTOMER");
                _p_value = new StringBuilder(txtCustomer.Text.Trim());
                ClearScreen(false, true);
                StringBuilder _particularCustomer = new StringBuilder(CHNLSVC.Sales.GetMonitorCustomer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _p_value.ToString(), _p_keyword.ToString()));
                if (string.IsNullOrEmpty(_particularCustomer.ToString())) { MessageBox.Show("No data found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                _p_customer = new StringBuilder(Convert.ToString(_particularCustomer));
                LoadCustomerDetail(_particularCustomer.ToString());
                LoadRelatedDocument(_particularCustomer.ToString());
                CheckBlackListCustomer(_particularCustomer.ToString());
                txtAccount.Clear();
                txtInvoice.Clear();
                txtReceipt.Clear();
                txtSSI.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadEverything(_p_customer.ToString(), _p_relateddoc.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        #region Rooting for Various Events
        private void tabControl2_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            TabPage CurrentTab = tabControl2.TabPages[e.Index]; Rectangle ItemRect = tabControl2.GetTabRect(e.Index); Rectangle PageRect1 = new Rectangle(new Point(2, 25), new Size(3, 126)); SolidBrush FillBrush = new SolidBrush(Color.White); SolidBrush TextBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat(); sf.Alignment = StringAlignment.Center; sf.LineAlignment = StringAlignment.Center;
            Font TabFont;
            if (System.Convert.ToBoolean(e.State & DrawItemState.Selected)) { FillBrush.Color = Color.FromArgb(180, 169, 190); TextBrush.Color = Color.FromArgb(80, 69, 90); ItemRect.Inflate(3, 3); }
            if (e.Index == this.tabControl2.SelectedIndex) TabFont = new Font(e.Font, FontStyle.Bold);
            else TabFont = e.Font;
            PageRect1.Inflate(2, 2);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 169, 190)), PageRect1);
            e.Graphics.FillRectangle(FillBrush, ItemRect);
            e.Graphics.DrawString(CurrentTab.Text, TabFont, TextBrush, (RectangleF)ItemRect, sf);
            e.Graphics.ResetTransform();
            FillBrush.Dispose();
            TextBrush.Dispose();

            //private void GradentColor()
            //{
            //    //LinearGradientBrush br = new LinearGradientBrush(this.ClientRectangle, Color.Black, Color.Black, 0, false);
            //    //ColorBlend cb = new ColorBlend();
            //    //cb.Positions = new[] { 0, 1 / 6f, 2 / 6f, 3 / 6f, 4 / 6f, 5 / 6f, 1 };
            //    //cb.Colors = new[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
            //    //br.InterpolationColors = cb;
            //    //// rotate
            //    //br.RotateTransform(45);
            //    //// paint
            //    //e.Graphics.FillRectangle(br, this.ClientRectangle);

            //    //LinearGradientBrush lgb = new LinearGradientBrush(tabControl2.ClientRectangle, Color.IndianRed, Color.White, 0f, true);
            //    //e.Graphics.FillRectangle(lgb, tabControl2.ClientRectangle);
            //    //lgb.Dispose();
            //}
        }
        private void LoadReceiptGrid()
        {
            //this.gvReceipt.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            //this.gvReceipt.ColumnHeadersHeight = this.gvReceipt.ColumnHeadersHeight * 2;
            //this.gvReceipt.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            //this.gvReceipt.CellPainting += new DataGridViewCellPaintingEventHandler(gvReceipt_CellPainting);
            //this.gvReceipt.Paint += new PaintEventHandler(gvReceipt_Paint);
            //this.gvReceipt.Scroll += new ScrollEventHandler(gvReceipt_Scroll);
            //this.gvReceipt.ColumnWidthChanged += new DataGridViewColumnEventHandler(gvReceipt_ColumnWidthChanged);
        }
        private void gvReceipt_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Rectangle rtHeader = this.gvReceipt.DisplayRectangle;
            rtHeader.Height = this.gvReceipt.ColumnHeadersHeight / 2;
            this.gvReceipt.Invalidate(rtHeader);
        }
        private void gvReceipt_Scroll(object sender, ScrollEventArgs e)
        {
            Rectangle rtHeader = this.gvReceipt.DisplayRectangle;
            rtHeader.Height = this.gvReceipt.ColumnHeadersHeight / 2;
            this.gvReceipt.Invalidate(rtHeader);
        }
        private void gvReceipt_Paint(object sender, PaintEventArgs e)
        {
            ReceiptGridPaint(-1, 1, 1, e);
            ReceiptGridPaint(0, 1, 4, e);
            ReceiptGridPaint(1, 5, 9, e);
            ReceiptGridPaint(2, 10, 13, e);
        }
        private void ReceiptGridPaint(Int16 _name, Int32 _startIndex, Int32 _finishIndex, PaintEventArgs e)
        {
            string[] _heading = { "General Info.", "Cheque Info.", "CC Info." };

            Rectangle r1 = this.gvReceipt.GetCellDisplayRectangle(_startIndex, -1, true);

            int width = r1.Width;
            int height = r1.Height / 2 - 2;
            r1.X -= 1;
            r1.Y -= 1;
            for (Int32 x = _startIndex + 1; x <= _finishIndex; x++)
            {
                int w2 = this.gvReceipt.GetCellDisplayRectangle(x, -1, true).Width;
                width = width + w2;
            }
            r1.Width = width;
            r1.Height = height;


            e.Graphics.FillRectangle(new SolidBrush(this.gvReceipt.ColumnHeadersDefaultCellStyle.BackColor), r1);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            if (_name == -1)
                e.Graphics.DrawString(string.Empty, this.gvReceipt.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(this.gvReceipt.ColumnHeadersDefaultCellStyle.ForeColor), r1, format);
            else
                e.Graphics.DrawString(_heading[_name], this.gvReceipt.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(this.gvReceipt.ColumnHeadersDefaultCellStyle.ForeColor), r1, format);
        }
        private void gvReceipt_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                Rectangle r2 = e.CellBounds;
                r2.Y += e.CellBounds.Height / 2;
                r2.Height = e.CellBounds.Height / 2;
                e.PaintBackground(r2, true);
                e.PaintContent(r2);
                e.Handled = true;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                AccountList = new List<HpAccount>();
                _BindDocs = new DataTable();
                while (this.Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                InitializeComponent();
                tabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
                Ad_hoc_Session();
                gvReceipt.AutoGenerateColumns = false;
                gvAccountBalance.AutoGenerateColumns = false;
                _accNumber = string.Empty;
                pnlBalance.Visible = false;
                LoadReceiptGrid();
                ucHpAccountSummary1.Clear();

                _p_keyword = new StringBuilder(string.Empty);
                _p_value = new StringBuilder(string.Empty);
                _p_relateddoctype = new StringBuilder(string.Empty);
                _p_relateddoc = new StringBuilder(string.Empty);
                _p_customer = new StringBuilder(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_p_relateddoc.ToString())) return;
                if (_p_relateddoctype.ToString() != "HS") return;
                Reports.HP.ReportViewerHP _history = new Reports.HP.ReportViewerHP();
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                _history.GlbReportName = string.Empty;
                BaseCls.GlbReportDoc = _p_relateddoc.ToString();
                BaseCls.GlbReportCustomerCode = _p_customer.ToString(); ;
                BaseCls.GlbReportName = "Cust_Acc_History.rpt";
                if (chkWRedBal.Checked == true)
                {
                    BaseCls.GlbReportName = "Cust_Acc_History_W_Red_bal.rpt";      
                }
                          
                _history.Show();

              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void btnBlkLst_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you need to black list this customer?", "Save...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BlackListCustomers _obj = new BlackListCustomers();

                    if (string.IsNullOrEmpty(txtBValue.Text))
                    {
                        MessageBox.Show("Please select the value", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBValue.Focus();
                        return;
                    }

                    if (IsNumeric(txtBValue.Text) == false)
                    {
                        MessageBox.Show("Please select the valid value", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBValue.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtBValue.Text) <= 0)
                    {
                        MessageBox.Show("Please select the valid value", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBValue.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtBRemark.Text))
                    {
                        MessageBox.Show("Please select the remarks", "Remarks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBRemark.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(_p_customer.ToString()))
                    {
                        MessageBox.Show("Please select the customer", "Remarks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBRemark.Focus();
                        return;
                    }

                    //if (!radHd.Checked)
                    //{
                    //    MessageBox.Show("You don't have permission for black list this customer. Please contact credit dept.", "User Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}


                    _obj.Hbl_act = true;
                    _obj.Hbl_cls_bal = 0;
                    _obj.Hbl_cre_by = BaseCls.GlbUserID;
                    _obj.Hbl_cre_dt = DateTime.Now.Date;
                    _obj.Hbl_cust_cd = _p_customer.ToString();
                    _obj.Hbl_def_val = Convert.ToDecimal(txtBValue.Text.Trim());
                    _obj.Hbl_dt = DateTime.Now.Date;
                    _obj.Hbl_rmk = txtBRemark.Text.Trim();
                    _obj.Hbl_rmv_dt = Convert.ToDateTime("12/12/9999");
                    _obj.Hbl_com = BaseCls.GlbUserComCode;
                    _obj.Hbl_pc = BaseCls.GlbUserDefProf;
                    if (radSR.Checked == false)
                    {
                        _obj.Hbl_com = string.Empty;
                        _obj.Hbl_pc = string.Empty;
                    }

                    int effect = CHNLSVC.Sales.SaveBlackListCustomer(_obj);
                    if (effect == -1)
                        MessageBox.Show("Process terminated. Please try again", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    else
                        MessageBox.Show("Save successfully", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnBlackList_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10149))
            {
                MessageBox.Show("Sorry, You have no permission to view black list customers!\n( Advice: Required permission code :10149)");
                return;
            }

            if (pnlBlkLst.Visible)
                pnlBlkLst.Visible = false;
            else
            {
                pnlBlkLst.Visible = true;
                pnlBlkLst.Size = new Size(592, 279);
            }
        }
        private void btnRemoveBlkLst_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_p_customer.ToString()))
                {
                    MessageBox.Show("Please select the customer", "Remarks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBRemark.Focus();
                    return;
                }

                if (MessageBox.Show("Do you need to release black list status?", "Remove Black List...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int effect = CHNLSVC.Sales.ReleaseBlackListCustomer(_p_customer.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, radHd.Checked ? "1" : "0");
                    if (effect == -1)
                        MessageBox.Show("Process terminated. Please try again", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    else
                        MessageBox.Show("Update successfully", "Update...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        #endregion

        DataTable _serviceHistory = new DataTable();
        private void checkAvailability(string _manual_ref_no)
        {
            StringBuilder _errorlist = new StringBuilder();
           // List<string[]> _oklist = new List<string[]>();

            DataTable dt = CHNLSVC.Sales.Load_Recept_details(_manual_ref_no);
            if (dt.Rows.Count > 0)
            {

                _lstgrid = new List<Deposit_Bank_Pc_wise>();

                foreach (DataRow dr in dt.Rows)
                {
                    _objgrid = new Deposit_Bank_Pc_wise();
                    bool check_permision = CHNLSVC.Sales.Check_Available_ProfitCenters(BaseCls.GlbUserID, BaseCls.GlbUserComCode, dr["SAR_PROFIT_CENTER_CD"].ToString().Trim());
                    if (check_permision == false)
                    {
                        if (string.IsNullOrEmpty(_errorlist.ToString())) _errorlist.Append("Location - " + dr["SAR_PROFIT_CENTER_CD"].ToString());
                        else _errorlist.Append(" and Location - " + dr["SAR_PROFIT_CENTER_CD"].ToString());
                        continue;
                    }
                    else
                    {
                         _objgrid.Com = dr["SAR_COM_CD"].ToString();
                         _objgrid.Prof_center = dr["SAR_PROFIT_CENTER_CD"].ToString();
                         _objgrid.BankCode = dr["SAR_ACC_NO"].ToString();
                         _objgrid.Prifix = dr["SAR_PREFIX"].ToString();
                         _objgrid.Date = Convert.ToDateTime(dr["SAR_RECEIPT_DATE"]);
                         _objgrid.Amount = Convert.ToDecimal(dr["SAR_TOT_SETTLE_AMT"]);
                         _objgrid.BankName = dr["SAR_IS_MGR_ISS"].ToString();
                         _objgrid.Desc = dr["MSRT_DESC"].ToString();
                         _objgrid.Create_by = dr["SAR_CREATE_BY"].ToString();
                         _objgrid.Create_when = Convert.ToDateTime(dr["SAR_CREATE_WHEN"]);
                         _lstgrid.Add(_objgrid);
                    }


                }



                if (!string.IsNullOrEmpty(_errorlist.ToString()))
                {
                    MessageBox.Show("This receipt number is Available. But you dont have permission for those locations.\n" + _errorlist.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_lstgrid.Count > 0)
                    {
                        pnlGrid.Visible = true;
                        dgvReceptDetails.AutoGenerateColumns = false;
                        dgvReceptDetails.DataSource = _lstgrid;
                    }

                }
                else
                {
                    pnlGrid.Visible = true;
                    dgvReceptDetails.AutoGenerateColumns = false;
                   
                    dgvReceptDetails.DataSource = _lstgrid;
                  
                }


            }
            else
            {
                MessageBox.Show("This receipt number is not available in the system..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }





        }



        private void gvLoyalaty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 _row = e.RowIndex;
                if (_row > -1)
                {
                    string _cardNo = gvLoyalaty.Rows[_row].Cells[0].Value.ToString();
                    BindLoyalatyHistory(BaseCls.GlbUserComCode, _p_customer.ToString(), _cardNo);
                    BindLoyalatyRedeem(BaseCls.GlbUserComCode, _p_customer.ToString(), _cardNo, "LORE");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void BindLoyalatyRedeem(string _com, string _cus, string _ref, string _payTp)
        {
            try
            {

                DataTable _loyalRedeem = CHNLSVC.Sales.GetCustomerLoyalRedeemHis(_com, _cus, _ref, _payTp);

                dgvRedeemHis.AutoGenerateColumns = false;
                dgvRedeemHis.DataSource = new List<LoyaltyMemeber>();
                dgvRedeemHis.DataSource = _loyalRedeem;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void BindLoyalatyHistory(string _com, string _cus, string _card)
        {
            try
            {

                DataTable _loyalHis = CHNLSVC.Sales.GetCustomerLoyalEarnHis(_com, _cus, _card);

                dgvLoyalHis.AutoGenerateColumns = false;
                dgvLoyalHis.DataSource = new List<LoyaltyMemeber>();
                dgvLoyalHis.DataSource = _loyalHis;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void viewReminds(string accNo)
        {
            bool isReminderOpen = false;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "VIewManagerReminds")
                {
                    isReminderOpen = true;
                }
            }
            if (!isReminderOpen)
            {
                List<HPReminder> oHPReminder = new List<HPReminder>();
                oHPReminder = CHNLSVC.General.Notification_Get_AccountRemindersDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf).FindAll(x => x.Hra_ref == accNo); ;

                if (oHPReminder.Count > 0)
                {
                    VIewManagerReminds frm = new VIewManagerReminds(oHPReminder);
                    frm.ShowDialog();
                }
            }
        }

        private void btnClose_new_Click(object sender, EventArgs e)
        {
            dgvReceptDetails.DataSource = null;
            pnlGrid.Visible = false;
        }

        private void btnViewPay_Click(object sender, EventArgs e)
        {
            if (pnlBalance.Visible == true)
            {
                pnlBalance.Visible = false;
            }
            else
            {
              
                if (!string.IsNullOrEmpty(_accNumber))
                {
                    pnlBalance.Visible = true;
                    DataTable _accTbl = CHNLSVC.Sales.GetReduceBalInterestAccno(DateTime.Today ,BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _accNumber);
                 
                    object sumObject;
                    object sumObjectCap;
                    //sumObject = _accTbl.Compute("Sum(INTR)", "INTR >0");
                    //txtInterest.Text = sumObject.ToString();
                    //sumObjectCap = _accTbl.Compute("Sum(CAPITAL)", "CAPITAL >0");
                    //txtCapital.Text = sumObjectCap.ToString();
                    if (_accTbl.Rows.Count > 0)
                    {
                        foreach (DataRow drow in _accTbl.Rows)
                        {
                            //txtInterest.Text = drow["INTR"].ToString();
                            //txtCapital.Text = drow["CAPITAL"].ToString();
                            decimal intvalue = Convert.ToDecimal(drow["INTR"].ToString());
                            txtInterest.Text = intvalue.ToString("n2");
                            decimal capvalue = Convert.ToDecimal(drow["CAPITAL"].ToString());
                            txtCapital.Text = capvalue.ToString("n2");

                            
                        }  
                    }

                  
                }
        }
        }
   
        private void gvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtAccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrintInsu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_p_relateddoc.ToString())) return;
            if (_p_relateddoctype.ToString() != "HS") return;
            Reports.HP.ReportViewerHP _history = new Reports.HP.ReportViewerHP();
            BaseCls.GlbReportName = string.Empty;
            GlbReportName = string.Empty;
            _history.GlbReportName = string.Empty;
            BaseCls.GlbReportDoc = _p_relateddoc.ToString();
            BaseCls.GlbReportCustomerCode = _p_customer.ToString(); ;
            BaseCls.GlbReportName = "Cust_Acc_His_Insu.rpt";
            _history.Show();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            if(btnExpand.Text=="Expand >>")
            {
                btnExpand.Text = "Compress <<";
                pnlExchange.Size = new Size(987, 217);
                gvExchange.Size = new Size(982, 193);
            }
            else
            {
                btnExpand.Text = "Expand >>";
                pnlExchange.Size = new Size(397, 76);
                gvExchange.Size = new Size(395, 63);
            }
        }

        private void btnCloseVou_Click(object sender, EventArgs e)
        {
            pnlVou.Visible = false;
        }

        private void btnViewVou_Click(object sender, EventArgs e)
        {
            DataTable _dt = CHNLSVC.Financial.GetAccVoucher(_accNumber);
            grvVou.AutoGenerateColumns = false;
            grvVou.DataSource = _dt;
            pnlVou.Visible = true;

        }

        private void gvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                if (gvProduct.Rows.Count > 0)
                {
                    string _serialNo = gvProduct.Rows[e.RowIndex].Cells["pr_Serial1"].Value == DBNull.Value ? string.Empty : gvProduct.Rows[e.RowIndex].Cells["pr_Serial1"].Value.ToString().Trim();
                    string _itemCode = gvProduct.Rows[e.RowIndex].Cells["pr_Item"].Value == DBNull.Value ? string.Empty : gvProduct.Rows[e.RowIndex].Cells["pr_Item"].Value.ToString().Trim();
                    LoadServiceJobHistorty(_serialNo, _itemCode);
                }
            }
        }

        private void LoadServiceJobHistorty(string _serial, string item)
        {
            try
            {
                _serviceHistory = new DataTable();
                _serviceHistory = CHNLSVC.CustService.GetServiceJobHistoryBySerial(_serial, item);
                
                if (_serviceHistory.Rows.Count > 0)
                {
                    ServiceHistory _frmServiceHistory = new ServiceHistory();
                    _frmServiceHistory.ServiceJobHistory = _serviceHistory;
                    _frmServiceHistory.ShowDialog();
                }
                else { MessageBox.Show("Couldn't find service details for given serial number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching service details" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dgvMobileNo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
         //  txtCustomer.Text = dgvMobileNo.Rows[e.RowIndex].Cells[0].Value.ToString();
        //   pnlMobno.Visible = false;
        }


    }
}
//private void button2_Click(object sender, EventArgs e)
//{
//    //ReportViewer _view = new ReportViewer();

//    //_view.ReportPath = Application.ExecutablePath.Remove(Application.ExecutablePath.Length-8)+ "InvoiceHalfPrint.rpt";
//    //_view.ReportDisplayName = "Invoice";
//    //_view.InvoiceNo = txtInvoice.Text.Trim();
//    //_view.Show();

//    //ReportViewer _view2 = new ReportViewer();

//    //_view2.ReportPath = Application.ExecutablePath.Remove(Application.ExecutablePath.Length - 8) + "InvoiceHalfPrint.rpt";
//    //_view2.ReportDisplayName = "Invoice";
//    //_view2.InvoiceNo = txtInvoice.Text.Trim();
//    //_view2.Show();

//    //_view = null;
//    //_view2 = null;
//}
//void dataGridView1_Paint(object sender, PaintEventArgs e)
//{
//    string[] monthes = { "Điểm Hệ Số 1" };

//    for (int j = 0; j < 2; )
//    {

//        Rectangle r1 = this.dataGridView1.GetCellDisplayRectangle(j, -1, true);

//        int w2 = this.dataGridView1.GetCellDisplayRectangle(j + 1, -1, true).Width;

//        r1.X += 1;

//        r1.Y += 1;

//        r1.Width = r1.Width + w2 - 2;

//        r1.Height = r1.Height / 2 - 2;

//        e.Graphics.FillRectangle(new SolidBrush(this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);

//        StringFormat format = new StringFormat();

//        format.Alignment = StringAlignment.Center;

//        format.LineAlignment = StringAlignment.Center;

//        e.Graphics.DrawString(monthes[j / 2],

//            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font,

//            new SolidBrush(this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor),

//            r1,

//            format);

//        j += 2;

//    }

//}