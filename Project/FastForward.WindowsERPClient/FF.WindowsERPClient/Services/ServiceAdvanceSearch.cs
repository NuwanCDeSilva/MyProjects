using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceAdvanceSearch : Base
    {
        private string _warrSearchtp = string.Empty;
        private string _warrSearchorder = string.Empty;
        private Service_Chanal_parameter _scvParam = null;
        MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
        public ServiceAdvanceSearch(string serial,Service_Chanal_parameter scvParam)
        {
            InitializeComponent();
            txtSerial.Text = serial;
            _scvParam = scvParam;
            this.dtgrdDetView.Columns[0].Visible = false;
            if (serial.Trim() != "")
            {
                txtSerial_Leave(null,null);
                btnService_Click(null,null);
                btnService.Focus();
            }
            
        }

        private void btn_srch_serial_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                if (_scvParam.SP_ISAUTOMOBI == 1)
                {
                    _warrSearchtp = "ENGINE #";
                    _warrSearchorder = "SER";
                }
                else
                {
                    _warrSearchtp = _scvParam.SP_DB_SERIAL;
                    _warrSearchorder = "SER";
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSerial);
                _result = CHNLSVC.CommonSearch.SearchWarranty(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.ShowDialog();
                txtSerial.Select();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { SystemErrorMessage(ex); }
        }

        private void btn_srch_custmer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCusCd;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCusCd.Select();
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 1;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                //DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtCustCode;
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.ShowDialog();
                //txtCustCode.Select();
            }
            catch (Exception ex) { txtCusCd.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCusCd.Text.Trim()) && txtCusCd.Text.Trim() !="CASH")
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.dtgrdDetView.Columns[0].Visible = false;
                    lblTblHdr.Text = "Customer Sales Details";
                    string error = "";
                    DataTable sales = CHNLSVC.Inventory.getCustomerInvoiceData(BaseCls.GlbUserComCode, txtCusCd.Text.Trim(),txtItem.Text.Trim(),txtModel.Text.Trim(),txtCategory.Text.Trim(),txtInvNo.Text.Trim(), out error);
                    dtgrdDetView.DataSource = null;
                    dtgrdDetView.DataSource = sales;
                    dtgrdDetView.Refresh();
                    this.dtgrdDetView.Columns["Amount"].DefaultCellStyle.Format = "#,##0.#0";
                    this.dtgrdDetView.Columns["Quantity"].DefaultCellStyle.Format = "#,##0.#0";
                    this.dtgrdDetView.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dtgrdDetView.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dtgrdDetView.Columns["Amount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dtgrdDetView.Columns["Quantity"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    if (txtCusCd.Text.Trim() == "CASH")
                    {
                        MessageBox.Show("Unable to load sales details to CASH customer.");
                        return;
                    }
                    MessageBox.Show("Please select customer.");
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCusCd.Text.Trim()) || !string.IsNullOrEmpty(txtSerial.Text.Trim()))
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.dtgrdDetView.Columns[0].Visible = true;
                    lblTblHdr.Text = "Customer Job Details";
                    string error = "";
                    DataTable sarvice = CHNLSVC.Inventory.getCustomerJobHistoryData(BaseCls.GlbUserComCode, txtCusCd.Text.Trim(), txtItem.Text.Trim(), txtModel.Text.Trim(), txtCategory.Text.Trim(),txtSerial.Text.Trim(),txtInvNo.Text.Trim(), out error);
                    dtgrdDetView.DataSource = null;
                    dtgrdDetView.DataSource = sarvice;
                    dtgrdDetView.Refresh();
                    //this.dtgrdDetView.Columns["Cost"].DefaultCellStyle.Format = "#,##0.#0";
                    //this.dtgrdDetView.Columns["Cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //this.dtgrdDetView.Columns["Cost"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    MessageBox.Show("Please select customer.");
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                
                
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceSerial:
                    {
                        paramsText.Append(_warrSearchtp + seperator + _warrSearchorder + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void SystemErrorMessage(Exception ex)
        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private void SystemInformationMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }

        private void btnClr_Click(object sender, EventArgs e)
        {
            txtMobile.Text = "";
            txtNic.Text = "";
            txtDL.Text = "";
            txtPP.Text = "";
            txtSerial.Text = "";
            txtCusCd.Text = "";
            dtgrdDetView.DataSource = null;
            lblCustomerCd.Text = "";
            lblName.Text = "";
            lblAdress.Text = "";
            lblAdress2.Text = "";
            lblMobile.Text = "";
            lblTown.Text = "";
            lblDistrict.Text = "";
            lblMessage.Text = "";
            txtModel.Text = "";
            txtCategory.Text = "";
            txtItem.Text = "";
            txtInvNo.Text = "";
        }

        private void txtCusCd_Leave(object sender, EventArgs e)
        {
            try
            {
                dtgrdDetView.DataSource = null; 
                dtgrdDetView.Refresh();
                if (txtCusCd.Text.Trim() != "")
                {
                    this.Cursor = Cursors.WaitCursor;
                    _masterBusinessCompany = new MasterBusinessEntity();
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCusCd.Text, null, null, null, null, null/*BaseCls.GlbUserComCode*/);
                    if (_masterBusinessCompany.Mbe_cd != null)
                    {
                        if (_masterBusinessCompany.Mbe_act == false)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("This customer already inactive.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            btnClr_Click(null, null);
                            return;
                        }
                        else
                        {
                            lblCustomerCd.Text = _masterBusinessCompany.Mbe_cd;
                            lblName.Text = _masterBusinessCompany.Mbe_name;
                            lblAdress.Text = _masterBusinessCompany.Mbe_add1;
                            lblAdress2.Text = _masterBusinessCompany.Mbe_add2;
                            lblMobile.Text = _masterBusinessCompany.Mbe_mob;
                            lblTown.Text = _masterBusinessCompany.Mbe_town_cd;
                            lblDistrict.Text = _masterBusinessCompany.Mbe_distric_cd;
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtCusCd.Text = "";
                        btnClr_Click(null, null);
                        txtCusCd.Focus();
                        return;
                    }

                    check_Blacklistcustomer();

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }
        private void check_Blacklistcustomer()
        {
            DataTable _blacklist = CHNLSVC.Sales.CheckBlackListCustomer(txtCusCd.Text);

            if (_blacklist.Rows.Count > 0)
            {
                foreach (DataRow row in _blacklist.Rows)
                {
                    lblMessage.Text = ":: Black listed Customer ::";
                    lblMessage.BackColor = System.Drawing.Color.Yellow;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    SystemInformationMessage("Black Listed Customer. Customer Code: " + row["hbl_cust_cd"].ToString() + " - [" + row["hbl_rmk"].ToString() + "]", "Black List Customer");
                }
            }
            else
            {
                lblMessage.Text = ":: Customer ::";
                lblMessage.BackColor = System.Drawing.Color.MidnightBlue;
                lblMessage.ForeColor = System.Drawing.Color.White;
            }
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            try
            {
                dtgrdDetView.DataSource = null;
                dtgrdDetView.Refresh();
                _masterBusinessCompany = new MasterBusinessEntity();
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (!IsValidMobileOrLandNo(txtMobile.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the valid mobile", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClr_Click(null, null);
                        txtMobile.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                    if (_custList != null && _custList.Count > 0)
                    {
                        if (_custList.Count >= 1)
                        {
                            txtCusCd.Text = _custList[0].Mbe_cd;
                            lblCustomerCd.Text = _custList[0].Mbe_cd;
                            lblName.Text = _custList[0].Mbe_name;
                            lblAdress.Text = _custList[0].Mbe_add1;
                            lblAdress2.Text = _custList[0].Mbe_add2;
                            lblMobile.Text = _custList[0].Mbe_mob;
                            lblTown.Text = _custList[0].Mbe_town_cd;
                            lblDistrict.Text = _custList[0].Mbe_distric_cd;
                            check_Blacklistcustomer();
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Please select the valid mobile.", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            Cursor = DefaultCursor;
                            txtMobile.Text = ""; 
                            txtMobile.Focus();
                            return;
                        }

                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please select the valid mobile.", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtMobile.Text = "";
                        txtMobile.Focus();
                        return;
                    }

                }
               
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtNic_Leave(object sender, EventArgs e)
            {
            try
            {
                dtgrdDetView.DataSource = null;
                dtgrdDetView.Refresh();
                if (txtNic.Text.Trim() != "")
                {
                    this.Cursor = Cursors.WaitCursor;
                    _masterBusinessCompany = new MasterBusinessEntity();
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, txtNic.Text.Trim(), null, null, null, null/*BaseCls.GlbUserComCode*/);
                    if (_masterBusinessCompany.Mbe_cd != null)
                    {
                        if (_masterBusinessCompany.Mbe_act == false)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("This customer already inactive.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            btnClr_Click(null, null);
                            return;
                        }
                        else
                        {
                            txtCusCd.Text = _masterBusinessCompany.Mbe_cd;
                            lblCustomerCd.Text = _masterBusinessCompany.Mbe_cd;
                            lblName.Text = _masterBusinessCompany.Mbe_name;
                            lblAdress.Text = _masterBusinessCompany.Mbe_add1;
                            lblAdress2.Text = _masterBusinessCompany.Mbe_add2;
                            lblMobile.Text = _masterBusinessCompany.Mbe_mob;
                            lblTown.Text = _masterBusinessCompany.Mbe_town_cd;
                            lblDistrict.Text = _masterBusinessCompany.Mbe_distric_cd;
                            check_Blacklistcustomer();
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please select the valid customer NIC", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtNic.Text = "";
                        btnClr_Click(null, null);
                        txtNic.Focus();
                        return;
                    }

                    check_Blacklistcustomer();

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtDL_Leave(object sender, EventArgs e)
        {
            try
            {
                dtgrdDetView.DataSource = null;
                dtgrdDetView.Refresh();
                if (txtDL.Text.Trim() != "")
                {
                    this.Cursor = Cursors.WaitCursor;
                    _masterBusinessCompany = new MasterBusinessEntity();
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, null, txtDL.Text.Trim(), null, null, null/*BaseCls.GlbUserComCode*/);
                    if (_masterBusinessCompany.Mbe_cd != null)
                    {
                        if (_masterBusinessCompany.Mbe_act == false)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("This customer already inactive.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            btnClr_Click(null, null);
                            return;
                        }
                        else
                        {
                            txtCusCd.Text = _masterBusinessCompany.Mbe_cd;
                            lblCustomerCd.Text = _masterBusinessCompany.Mbe_cd;
                            lblName.Text = _masterBusinessCompany.Mbe_name;
                            lblAdress.Text = _masterBusinessCompany.Mbe_add1;
                            lblAdress2.Text = _masterBusinessCompany.Mbe_add2;
                            lblMobile.Text = _masterBusinessCompany.Mbe_mob;
                            lblTown.Text = _masterBusinessCompany.Mbe_town_cd;
                            lblDistrict.Text = _masterBusinessCompany.Mbe_distric_cd;
                            check_Blacklistcustomer();
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please select the valid customer DL", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtDL.Text = "";
                        btnClr_Click(null, null);
                        txtDL.Focus();
                        return;
                    }

                    check_Blacklistcustomer();

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtPP_Leave(object sender, EventArgs e)
        {
            try
            {
                dtgrdDetView.DataSource = null;
                dtgrdDetView.Refresh();
                if (txtPP.Text.Trim() != "")
                {
                    this.Cursor = Cursors.WaitCursor;
                    _masterBusinessCompany = new MasterBusinessEntity();
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, null, null, txtPP.Text.Trim(), null, null/*BaseCls.GlbUserComCode*/);
                    if (_masterBusinessCompany.Mbe_cd != null)
                    {
                        if (_masterBusinessCompany.Mbe_act == false)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("This customer already inactive.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            btnClr_Click(null, null);
                            return;
                        }
                        else
                        {
                            txtCusCd.Text = _masterBusinessCompany.Mbe_cd;
                            lblCustomerCd.Text = _masterBusinessCompany.Mbe_cd;
                            lblName.Text = _masterBusinessCompany.Mbe_name;
                            lblAdress.Text = _masterBusinessCompany.Mbe_add1;
                            lblAdress2.Text = _masterBusinessCompany.Mbe_add2;
                            lblMobile.Text = _masterBusinessCompany.Mbe_mob;
                            lblTown.Text = _masterBusinessCompany.Mbe_town_cd;
                            lblDistrict.Text = _masterBusinessCompany.Mbe_distric_cd;
                            check_Blacklistcustomer();
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please select the valid customer passport", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtPP.Text = "";
                        btnClr_Click(null, null);
                        txtPP.Focus();
                        return;
                    }

                    check_Blacklistcustomer();

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void btnItmSrch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    _commonSearch.ReturnIndex = 0;
                    _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(_commonSearch.SearchParams, null, null);
                    _commonSearch.dvResult.DataSource = _result;
                    _commonSearch.BindUCtrlDDLData(_result);
                    _commonSearch.obj_TragetTextBox = txtItem;
                    _commonSearch.IsSearchEnter = true;
                    this.Cursor = Cursors.Default;
                    _commonSearch.ShowDialog();
                    txtItem.Select(); 
                }
                catch (Exception ex)
                { txtItem.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
                finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void btnModelSrch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtModel; 
                _CommonSearch.ShowDialog();
                txtModel.Focus();
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void btnCateSrch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCategory;
                _CommonSearch.ShowDialog();
                txtCategory.Focus();
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtSerial_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSerial.Text.Trim()))
                {
                    this.Cursor = Cursors.WaitCursor;
                    string error = "";
                    string serial = txtSerial.Text.Trim();
                    DataTable serdocus = CHNLSVC.Inventory.getSerialDoDetails(serial,BaseCls.GlbUserComCode,out error);
                    if (error != "")
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(error, "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (serdocus.Rows.Count > 0)
                    {
                        _masterBusinessCompany = new MasterBusinessEntity();
                        _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(serdocus.Rows[0]["ITH_BUS_ENTITY"].ToString(), null, null, null, null, null/*BaseCls.GlbUserComCode*/);
                        txtCusCd.Text = _masterBusinessCompany.Mbe_cd;
                        lblCustomerCd.Text = _masterBusinessCompany.Mbe_cd;
                        lblName.Text = _masterBusinessCompany.Mbe_name;
                        lblAdress.Text = _masterBusinessCompany.Mbe_add1;
                        lblAdress2.Text = _masterBusinessCompany.Mbe_add2;
                        lblMobile.Text = _masterBusinessCompany.Mbe_mob;
                        lblTown.Text = _masterBusinessCompany.Mbe_town_cd;
                        lblDistrict.Text = _masterBusinessCompany.Mbe_distric_cd;
                        check_Blacklistcustomer();
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Added serial does not have any DO details.", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtSerial.Text = "";
                        txtSerial.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void dtgrdDetView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                if (e.ColumnIndex == 0)
                {
                    Int32 indx = e.ColumnIndex;
                    string jobnum = dtgrdDetView.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if (string.IsNullOrEmpty(jobnum))
                    {
                        MessageBox.Show("Please select the job number.", "Job Tasks", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    ServiceTasks frm = new ServiceTasks(jobnum, 0);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtInvNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvNo.Text.Trim()))
                {
                    string error = "";
                    string serial = txtInvNo.Text.Trim();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable serdocus = CHNLSVC.Inventory.getInvoiceDetails(txtInvNo.Text.Trim(), BaseCls.GlbUserComCode, out error);
                    if (error != "")
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(error, "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (serdocus.Rows.Count > 0)
                    {
                        
                        _masterBusinessCompany = new MasterBusinessEntity();
                        _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(serdocus.Rows[0]["SAH_CUS_CD"].ToString(), null, null, null, null, null/*BaseCls.GlbUserComCode*/);
                        txtCusCd.Text = _masterBusinessCompany.Mbe_cd;
                        lblCustomerCd.Text = _masterBusinessCompany.Mbe_cd;
                        lblName.Text = _masterBusinessCompany.Mbe_name;
                        lblAdress.Text = _masterBusinessCompany.Mbe_add1;
                        lblAdress2.Text = _masterBusinessCompany.Mbe_add2;
                        lblMobile.Text = _masterBusinessCompany.Mbe_mob;
                        lblTown.Text = _masterBusinessCompany.Mbe_town_cd;
                        lblDistrict.Text = _masterBusinessCompany.Mbe_distric_cd;
                        check_Blacklistcustomer();
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Invalid invoice number", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtInvNo.Text = "";
                        txtInvNo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtMobile_Enter(object sender, EventArgs e)
        {
            //if (txtMobile.Text.Trim() != "")
            //{
            //    txtDL.Focus();
            //}
        }

        private void txtDL_Enter(object sender, EventArgs e)
        {
            //if (txtDL.Text.Trim() != "")
            //{
            //    txtSerial.Focus();
            //}
        }

        private void txtSerial_Enter(object sender, EventArgs e)
        {
            //if (txtSerial.Text.Trim() != "")
            //{
            //    txtItem.Focus();
            //}
        }

        private void txtItem_Enter(object sender, EventArgs e)
        {
            //if (txtItem.Text.Trim() != "")
            //{
            //    txtCategory.Focus();
            //}

        }

        private void txtCategory_Enter(object sender, EventArgs e)
        {
            //if (txtCategory.Text.Trim() != "")
            //{
            //    txtNic.Focus();
            //}
        }

        private void txtPP_Enter(object sender, EventArgs e)
        {
            //if (txtPP.Text.Trim() != "")
            //{
            //    txtCusCd.Focus();
            //}
        }

        private void txtCusCd_Enter(object sender, EventArgs e)
        {
            //if (txtCusCd.Text.Trim() != "")
            //{
            //    txtModel.Focus();
            //}
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (txtMobile.Text.Trim() != "")
                {
                    btnSale.Focus();
                }
            }
        }

        private void txtDL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDL.Text.Trim() != "")
                {
                    btnSale.Focus();
                }
            }
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSerial.Text.Trim() != "")
                {
                    btnSale.Focus();
                }
            }
        }

        private void txtNic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNic.Text.Trim() != "")
                {
                    btnSale.Focus();
                }
            }
        }

        private void txtPP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPP.Text.Trim() != "")
                {
                    btnSale.Focus();
                }
            }
        }

        private void txtCusCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCusCd.Text.Trim() != "")
                {
                    btnSale.Focus();
                }
            }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtItem.Text.Trim() != "")
                {
                    txtCategory.Focus();
                }
            }
        }

        private void txtCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCategory.Text.Trim() != "")
                {
                    txtNic.Focus();
                }
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtModel.Text.Trim() != "")
                {
                    txtInvNo.Focus();
                }
            }
        }

        private void txtInvNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtInvNo.Text.Trim() != "")
                {
                    txtCusCd.Focus();
                }
            }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtItem.Text.Trim() != "")
                {
                    MasterItem itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                    if (itm == null || itm.Mi_cd == null)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please enter valid item code.", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtItem.Text = "";
                        txtItem.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtModel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtModel.Text.Trim() != "")
                {
                    List<MasterItemModel>  mdl = CHNLSVC.General.GetItemModelNew(txtModel.Text.Trim());
                    if (mdl == null || mdl.Count==0)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please enter valid item model.", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtModel.Text = "";
                        txtModel.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void txtCategory_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.Text.Trim() != "")
                {
                    List<REF_ITM_CATE1> cat1 = CHNLSVC.General.GetItemCate1();
                    bool has = cat1.Any(x => x.Ric1_cd == txtCategory.Text.Trim());
                    if (!has)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please enter valid item model.", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        Cursor = DefaultCursor;
                        txtCategory.Text = "";
                        txtCategory.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }

        }
    }
}
