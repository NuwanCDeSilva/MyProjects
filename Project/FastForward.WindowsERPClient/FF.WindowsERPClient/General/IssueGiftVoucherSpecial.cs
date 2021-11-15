using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.General
{
    public partial class IssueGiftVoucherSpecial : Base
    {
        private int _CHK_CUST = 0;
        private MasterBusinessEntity _masterBusinessCompany = null;
        private List<GiftVoucherPages> _lstgvPages = new List<GiftVoucherPages>();

        private void SystemInformationMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }

        private void SystemErrorMessage(Exception ex)
        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        private void SystemWarnningMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        public IssueGiftVoucherSpecial()
        {
            InitializeComponent();
            cmbGvType.SelectedIndex = -1;
        }

        private void IssueGiftVoucherAmenment_Load(object sender, EventArgs e)
        {
            //  Clear_Data();
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetItmByType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "G" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GvCategory:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void btnSearchItmGv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbGvType.Text))
                {
                    MessageBox.Show("Please select type of gift voucher.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbGvType.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemGvCode;
                _CommonSearch.ShowDialog();
                txtItemGvCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.ShowDialog();
            txtUpload.Text = openFileDialog.FileName;
        }

        private void btnupPC_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            string _custCode = "";
            MasterItem _Mstitm = new MasterItem();
            List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();

            if (string.IsNullOrEmpty(txtUpload.Text))
            {
                MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUpload.Text = "";
                txtUpload.Focus();
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUpload.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBrowse.Focus();
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }


            conStr = String.Format(conStr, txtUpload.Text, "NO");
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

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            //DataTable tem = new DataTable();
            //tem.Columns.Add("PROFIT_CENTER");
            //tem.Columns.Add("PC_DESCRIPTION");
            if (dt.Rows.Count > 0)
            {
                #region validate excel
                foreach (DataRow _dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(_dr[1].ToString()))
                    {
                        MessageBox.Show("Process halted. Invalid book number found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(_dr[5].ToString()))
                    {
                        MessageBox.Show("Process halted. Invalid value found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(_dr[0].ToString()))
                    {
                        MessageBox.Show("Process halted. Empty GV code found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _Mstitm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString());
                    if (_Mstitm == null)
                    {
                        MessageBox.Show("Process halted. Invalid GV code found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (_Mstitm.MI_CHK_CUST == 1)
                            _CHK_CUST = 1;
                        else
                            _CHK_CUST = 0;
                    }

                    //check valid customer
                    if (_CHK_CUST == 1)
                    {
                        //mobile
                        _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, _dr[6].ToString(), "C");
                        if (_custList == null && _custList.Count == 0)
                        {
                            //NIC
                            _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, _dr[7].ToString(), string.Empty, "C");
                            if (_custList == null && _custList.Count == 0)
                            {
                                MessageBox.Show("Process halted. Registered customer not found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                        }
                    }
                }
                #endregion

                _lstgvPages = new List<GiftVoucherPages>();
                foreach (DataRow _dr in dt.Rows)
                {
                    _custCode = "";
                    Int32 _count = _lstgvPages.Where(X => X.Gvp_gv_cd == _dr[0].ToString() && X.Gvp_book == Convert.ToInt32(_dr[1]) && X.Gvp_page == Convert.ToInt32(_dr[4])).Count();
                    if (_count > 0)
                        break;

                    _Mstitm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString());
                    if (_Mstitm.MI_CHK_CUST == 1)
                        _CHK_CUST = 1;
                    else
                        _CHK_CUST = 0;


                    //check valid customer
                    if (_CHK_CUST == 1)
                    {
                        //mobile
                        _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, _dr[6].ToString(), "C");
                        if (_custList != null && _custList.Count > 0)
                            _custCode = _custList[0].Mbe_cd;
                        else
                        {
                            //NIC
                            _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, _dr[7].ToString(), string.Empty, "C");
                            if (_custList != null && _custList.Count > 0)
                                _custCode = _custList[0].Mbe_cd;
                        }
                    }

                    GiftVoucherPages _gvou = new GiftVoucherPages();
                    _gvou.Gvp_gv_tp = "VALUE";
                    _gvou.Gvp_amt = Convert.ToDecimal(_dr[5]);
                    _gvou.Gvp_app_by = BaseCls.GlbUserID;
                    _gvou.Gvp_bal_amt = Convert.ToDecimal(_dr[5]);
                    _gvou.Gvp_book = Convert.ToInt32(_dr[1]);
                    _gvou.Gvp_can_by = "";
                    _gvou.Gvp_can_dt = DateTime.Now.Date;
                    _gvou.Gvp_com = BaseCls.GlbUserComCode;
                    _gvou.Gvp_cre_by = BaseCls.GlbUserID;
                    _gvou.Gvp_cre_dt = DateTime.Now.Date;
                    _gvou.Gvp_cus_add1 = _dr[9].ToString();
                    _gvou.Gvp_cus_add2 = _dr[10].ToString();
                    _gvou.Gvp_cus_cd = _custCode;
                    _gvou.Gvp_cus_mob = _dr[6].ToString();
                    _gvou.Gvp_cus_name = _dr[8].ToString();
                    _gvou.Gvp_gv_cd = _dr[0].ToString();
                    _gvou.Gvp_gv_prefix = "GV";
                    _gvou.Gvp_is_allow_promo = chkAlwPromo.Checked;
                    _gvou.Gvp_issu_itm = 0;
                    _gvou.Gvp_issue_by = "";
                    _gvou.Gvp_issue_dt = DateTime.Now.Date;
                    //_gvou.Gvp_line = i;
                    _gvou.Gvp_mod_by = "";
                    _gvou.Gvp_mod_dt = DateTime.Now.Date;
                    _gvou.Gvp_noof_itm = 1;
                    _gvou.Gvp_oth_ref = "";
                    _gvou.Gvp_page = Convert.ToInt32(_dr[4]);
                    _gvou.Gvp_pc = "HO";
                    _gvou.Gvp_ref = "";
                    _gvou.Gvp_stus = "A";
                    _gvou.Gvp_valid_from = Convert.ToDateTime(dtpItemValidFrom.Value.Date);
                    _gvou.Gvp_valid_to = Convert.ToDateTime(dtpItemValidTo.Value.Date);
                    _gvou.Gvp_cus_nic = txtNIC.Text;

                    _lstgvPages.Add(_gvou);
                }
            }

            gvPages.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _lstgvPages;
            gvPages.DataSource = _source;
        }

        private void txtFirst_TextChanged(object sender, EventArgs e)
        {
            txtLast.Text = txtFirst.Text;
            if (!string.IsNullOrEmpty(txtFirst.Text))
                calc();
        }

        private void calc()
        {
            txtPages.Text = (Convert.ToInt32(txtLast.Text) - Convert.ToInt32(txtFirst.Text) + 1).ToString();
        }

        private void calcnew()
        {
            txtPagesn.Text = (Convert.ToInt32(txtLastn.Text) - Convert.ToInt32(txtFirstn.Text) + 1).ToString();
        }

        private void txtLast_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFirst.Text) && !string.IsNullOrEmpty(txtLast.Text))
                calc();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string _errList = "";

            if (string.IsNullOrEmpty(txtBook.Text))
            {
                MessageBox.Show("Please enter book number", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBook.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAmt.Text))
            {
                MessageBox.Show("Please enter value", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmt.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFirst.Text))
            {
                MessageBox.Show("Please enter first page number", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirst.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLast.Text))
            {
                MessageBox.Show("Please enter first page number", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLast.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtItemGvCode.Text))
            {
                MessageBox.Show("Please select GV code", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemGvCode.Focus();
                return;
            }
            if (_CHK_CUST == 1 && string.IsNullOrEmpty(txtCusCode.Text))
            {
                MessageBox.Show("Please select registered customer", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int32 _page = Convert.ToInt32(txtFirst.Text);
            for (int i = 1; i <= Convert.ToInt32(txtPages.Text); i++)
            {
                Int32 _count = _lstgvPages.Where(X => X.Gvp_gv_cd == txtItemGvCode.Text && X.Gvp_book == Convert.ToInt32(txtBook.Text) && X.Gvp_page == _page).Count();
                if (_count > 0)
                    break;


                GiftVoucherPages _gvou = new GiftVoucherPages();
                _gvou.Gvp_gv_tp = "VALUE";
                _gvou.Gvp_amt = Convert.ToDecimal(txtAmt.Text);
                _gvou.Gvp_app_by = BaseCls.GlbUserID;
                _gvou.Gvp_bal_amt = Convert.ToDecimal(txtAmt.Text);
                _gvou.Gvp_book = Convert.ToInt32(txtBook.Text);
                _gvou.Gvp_can_by = "";
                _gvou.Gvp_can_dt = DateTime.Now.Date;
                _gvou.Gvp_com = BaseCls.GlbUserComCode;
                _gvou.Gvp_cre_by = BaseCls.GlbUserID;
                _gvou.Gvp_cre_dt = DateTime.Now.Date;
                _gvou.Gvp_cus_add1 = txtCusAdd1.Text;
                _gvou.Gvp_cus_add2 = txtCusAdd2.Text;
                _gvou.Gvp_cus_cd = txtCusCode.Text;
                _gvou.Gvp_cus_mob = txtMobile.Text;
                _gvou.Gvp_cus_name = txtCusName.Text;
                _gvou.Gvp_gv_cd = txtItemGvCode.Text;
                _gvou.Gvp_gv_prefix = "GV";
                _gvou.Gvp_is_allow_promo = chkAlwPromo.Checked;
                _gvou.Gvp_issu_itm = 0;
                _gvou.Gvp_issue_by = "";
                _gvou.Gvp_issue_dt = DateTime.Now.Date;
                _gvou.Gvp_line = i;
                _gvou.Gvp_mod_by = "";
                _gvou.Gvp_mod_dt = DateTime.Now.Date;
                _gvou.Gvp_noof_itm = 1;
                _gvou.Gvp_oth_ref = "";
                _gvou.Gvp_page = _page;
                _gvou.Gvp_pc = "HO";
                _gvou.Gvp_ref = "";
                _gvou.Gvp_stus = "A";
                _gvou.Gvp_valid_from = Convert.ToDateTime(dtpItemValidFrom.Value.Date);
                _gvou.Gvp_valid_to = Convert.ToDateTime(dtpItemValidTo.Value.Date);
                _gvou.Gvp_cus_nic = txtNIC.Text;

                _lstgvPages.Add(_gvou);
                _page = _page + 1;
            }

            gvPages.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _lstgvPages;
            gvPages.DataSource = _source;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_lstgvPages.Count==0)
            {
                MessageBox.Show("Gift voucher details not found !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to save the gift voucher  ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No) return;
            Int32 _eff = 0;
            if (optTrans.Checked == false)
            {
                  _eff = CHNLSVC.Sales.SaveGiftVoucherPages(_lstgvPages);
            }
            else
            {
                _eff = CHNLSVC.Sales.UpdateVouTransfer(_lstgvPages);
            }


            MessageBox.Show("Successfully saved !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);

            IssueGiftVoucherSpecial formnew = new IssueGiftVoucherSpecial();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();

        }

        private void btnSearchCus_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch.ReturnIndex = 0;
            _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            _commonSearch.dvResult.DataSource = _result;
            _commonSearch.BindUCtrlDDLData(_result);
            _commonSearch.obj_TragetTextBox = txtCusCode;
            _commonSearch.IsSearchEnter = true;
            this.Cursor = Cursors.Default;
            _commonSearch.ShowDialog();
            txtCusCode.Select();
        }

        private void IssueGiftVoucherSpecial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            if (txtCusCode.Text == "CASH")
            {
                SystemInformationMessage("Please enter a valid customer code.", "Voucher");
                txtCusCode.Text = "";
                txtCusCode.Focus();
                return;
            }

            LoadCustomerDetailsByCustomer();
        }

        protected void LoadCustomerDetailsByCustomer()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    if (txtCusName.Enabled == false)
                    {
                        txtCusCode.Text = "CASH";
                        EnableDisableCustomer();
                    }
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCusCode.Text, null, null, null, null, BaseCls.GlbUserComCode);
                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        ClearCustomer(true);
                        txtCusCode.Focus();
                        return;
                    }

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        SetCustomerAndDeliveryDetails(false, null);
                        //ClearCustomer(false);
                    }
                    else
                    {
                        SetCustomerAndDeliveryDetails(false, null);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    ClearCustomer(true);
                    txtCusCode.Focus();
                    return;
                }
                EnableDisableCustomer();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
            txtCusName.Text = _masterBusinessCompany.Mbe_name;
            txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
            txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
            txtMobile.Text = _masterBusinessCompany.Mbe_mob;
            txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            //    cmbTitle.Text = _masterBusinessCompany.MBE_TIT;


            if (_isRecall == false)
            {

            }
            else
            {
                txtCusName.Text = _hdr.Sah_cus_name;
                txtCusAdd1.Text = _hdr.Sah_cus_add1;
                txtCusAdd2.Text = _hdr.Sah_cus_add2;

            }

        }

        private void ClearCustomer(bool _isCustomer)
        {
            if (_isCustomer) txtCusCode.Clear();
            txtCusName.Clear();
            txtCusAdd1.Clear();
            txtCusAdd2.Clear();
            txtMobile.Clear();
            txtNIC.Clear();
            // txtTown.Clear();
            txtCusName.Enabled = true;
            txtCusAdd1.Enabled = true;
            txtCusAdd2.Enabled = true;
            txtMobile.Enabled = true;
            txtNIC.Enabled = true;
            // txtTown.Enabled = true;
            //chkTaxPayable.Checked = false;
            //txtLoyalty.Clear();
        }
        private void EnableDisableCustomer()
        {
            if (txtCusCode.Text == "CASH")
            {
                txtCusCode.Enabled = true;
                txtCusName.Enabled = true;
                txtCusAdd1.Enabled = true;
                txtCusAdd2.Enabled = true;
                txtMobile.Enabled = true;
                txtNIC.Enabled = true;

                btnSearch_NIC.Enabled = true;
                btnSearchCus.Enabled = true;
                btnSearch_Mobile.Enabled = true;
            }
            else
            {
                //txtCustomer.Enabled = false;
                txtCusName.Enabled = false;
                txtCusAdd1.Enabled = false;
                txtCusAdd2.Enabled = false;
                txtMobile.Enabled = false;
                txtNIC.Enabled = false;

                //btnSearch_NIC.Enabled = false;
                //btnSearch_Customer.Enabled = false;
                //btnSearch_Mobile.Enabled = false;
            }
        }

        private void btnSearch_NIC_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtNIC;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtNIC.Select();
            }
            catch (Exception ex)
            { txtNIC.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnSearch_Mobile_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtMobile;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtMobile.Select();
                if (_commonSearch.GlbSelectData == null) return;
                string[] args = _commonSearch.GlbSelectData.Split('|');
                string _cuscode = args[4];
                if (string.IsNullOrEmpty(txtCusCode.Text) || txtCusCode.Text.Trim() == "CASH") txtCusCode.Text = _cuscode;
                else if (txtCusCode.Text.Trim() != _cuscode && txtCusCode.Text.Trim() != "CASH")
                {
                    DialogResult _res = MessageBox.Show("Currently selected customer code " + txtCusCode.Text + " is differ which selected (" + _cuscode + ") from here. Do you need to change current customer code from selected customer", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_res == System.Windows.Forms.DialogResult.Yes)
                    {
                        txtCusCode.Text = _cuscode;
                        txtCusCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void btnNewCust_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCusCode;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txtCusCode.Select();
                txtCusCode_Leave(null, null);
            }
            catch (Exception ex)
            { txtCusCode.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void txtItemGvCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemGvCode.Text)) return;

                //updated by akila 2018/01/13
                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemGvCode.Text.Trim());
                if (_itm != null && (!string.IsNullOrEmpty(_itm.Mi_cd)))
                {
                    if (_itm.Mi_itm_tp != "G")
                    {
                        MessageBox.Show("Please select the valid Gift voucher code", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemGvCode.Clear();
                        txtItemGvCode.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Gift voucher details not found !", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemGvCode.Clear();
                    txtItemGvCode.Focus();
                    return;
                }

                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                //DataTable _result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, null, null);

                //var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtItemGvCode.Text.Trim()).ToList();
                //if (_validate == null || _validate.Count <= 0)
                //{
                //    MessageBox.Show("Please select the valid Gift voucher code", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtItemGvCode.Clear();
                //    txtItemGvCode.Focus();
                //    return;
                //}

               // MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemGvCode.Text);
                if (_itm.MI_CHK_CUST == 1)
                    _CHK_CUST = 1;
                else
                    _CHK_CUST = 0;


            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen ?", "Confirmation ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

            IssueGiftVoucherSpecial formnew = new IssueGiftVoucherSpecial();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void clearAll()
        {

        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtFirst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtLast_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnSearchItmGvn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbGvTypen.Text))
                {
                    MessageBox.Show("Please select the type of gift voucher.", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbGvTypen.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemGvCoden;
                _CommonSearch.ShowDialog();
                txtItemGvCoden.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemGvCoden_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemGvCoden.Text)) return;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtItemGvCoden.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid Gift voucher code", "Voucher Amend", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemGvCoden.Clear();
                    txtItemGvCoden.Focus();
                    return;
                }

                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemGvCoden.Text);
                if (_itm.MI_CHK_CUST == 1)
                    _CHK_CUST = 1;
                else
                    _CHK_CUST = 0;


            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAppNew_Click(object sender, EventArgs e)
        {
            string _errList = "";

            if (string.IsNullOrEmpty(txtBookn.Text))
            {
                MessageBox.Show("Please enter book number", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookn.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAmtn.Text))
            {
                MessageBox.Show("Please enter value", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmtn.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFirstn.Text))
            {
                MessageBox.Show("Please enter first page number", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstn.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLastn.Text))
            {
                MessageBox.Show("Please enter first page number", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastn.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtItemGvCoden.Text))
            {
                MessageBox.Show("Please select the GV code", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemGvCoden.Focus();
                return;
            }

            if (optTrans.Checked)
            {
                if (string.IsNullOrEmpty(txtPc.Text))
                {
                    MessageBox.Show("Please select  the transfer pc.", "Gift Vouchar", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPc.Focus();
                    return;
                }
            }

            Int32 _bkno = 0;
            _bkno = Convert.ToInt32(txtBookn.Text);
            DataTable _dtlgvbokk = CHNLSVC.Inventory.GetAvailable_GV_books(_bkno, txtItemGvCoden.Text , BaseCls.GlbUserComCode);

            if (optnew.Checked)
            {
            if (_dtlgvbokk.Rows.Count > 0)
            {
                MessageBox.Show("Process halted. Vouchar book already exist !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            }
            if (optTrans.Checked)
            {
                if (_dtlgvbokk.Rows.Count == 0)
                {
                    MessageBox.Show("Process halted. Vouchar book is invalid !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                   Int32 _pagen = Convert.ToInt32(txtFirstn.Text);
                  for (int i = 1; i <= Convert.ToInt32(txtPagesn.Text); i++)
                      
                  {

                      List<GiftVoucherPages> _lstgb = CHNLSVC.Inventory.GetAvailableGvPagesRange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "VALUE", "P", Convert.ToInt32(txtBookn.Text), txtItemGvCoden.Text, _pagen, _pagen);
                      if (_lstgb.Count == 0)
                      {
                          MessageBox.Show("Process halted. Vouchar page is not available !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                          return;
                      }
                      _pagen=_pagen+1;
                  }
               
            }

            //if (_CHK_CUST == 1 && string.IsNullOrEmpty(txtCusCode.Text))
            //{
            //    MessageBox.Show("Please select registered customer", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            Int32 _page = Convert.ToInt32(txtFirstn.Text);
            for (int i = 1; i <= Convert.ToInt32(txtPagesn.Text); i++)
            {
                Int32 _count = _lstgvPages.Where(X => X.Gvp_gv_cd == txtItemGvCoden.Text && X.Gvp_book == Convert.ToInt32(txtBookn.Text) && X.Gvp_page == _page).Count();
                if (_count > 0)
                    break;


                GiftVoucherPages _gvou = new GiftVoucherPages();
                _gvou.Gvp_gv_tp = "VALUE";
                _gvou.Gvp_amt = Convert.ToDecimal(txtAmtn.Text);
                _gvou.Gvp_app_by = BaseCls.GlbUserID;
                _gvou.Gvp_bal_amt = Convert.ToDecimal(txtAmtn.Text);
                _gvou.Gvp_book = Convert.ToInt32(txtBookn.Text);
                _gvou.Gvp_can_by = "";
                _gvou.Gvp_can_dt = DateTime.Now.Date;
                _gvou.Gvp_com = BaseCls.GlbUserComCode;
                _gvou.Gvp_cre_by = BaseCls.GlbUserID;
                _gvou.Gvp_cre_dt = DateTime.Now.Date;
                _gvou.Gvp_cus_add1 = string.Empty ;
                _gvou.Gvp_cus_add2 = string.Empty;
                _gvou.Gvp_cus_cd = string.Empty;
                _gvou.Gvp_cus_mob = string.Empty;
                _gvou.Gvp_cus_name = string.Empty;
                _gvou.Gvp_gv_cd = txtItemGvCoden.Text;
                _gvou.Gvp_gv_prefix = "P_GV";
                _gvou.Gvp_is_allow_promo = false;
                _gvou.Gvp_issu_itm = 0;
                _gvou.Gvp_issue_by = "";
                _gvou.Gvp_issue_dt = DateTime.Now.Date;
                _gvou.Gvp_line = i;
                _gvou.Gvp_mod_by = "";
                _gvou.Gvp_mod_dt = DateTime.Now.Date;
                _gvou.Gvp_noof_itm = 1;
                _gvou.Gvp_oth_ref = "";
                _gvou.Gvp_page = _page;
              if ( optTrans.Checked )
              { _gvou.Gvp_pc = txtPc.Text ; }
              else
              { _gvou.Gvp_pc = BaseCls.GlbUserDefProf; }
              
                _gvou.Gvp_ref = "";
                _gvou.Gvp_stus = "P";
                _gvou.Gvp_valid_from = Convert.ToDateTime(dtpItemValidFromn.Value.Date);
                _gvou.Gvp_valid_to = Convert.ToDateTime(dtpItemValidTon.Value.Date);
                _gvou.Gvp_cus_nic = string.Empty;

                _lstgvPages.Add(_gvou);
                _page = _page + 1;
            }

            gvPagesn.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _lstgvPages;
            gvPagesn.DataSource = _source;
        }

        private void txtItemGvCoden_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtBookn.Focus();
            //}
        }

        private void btnSearch_Pc_Click(object sender, EventArgs e)
        {
            try
            {
                

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPc;
                _CommonSearch.ShowDialog();
                txtPc.Select();
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

        private void txtPc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPc_Leave(object sender, EventArgs e)
        {
            try
            {
                

                if (string.IsNullOrEmpty(txtPc.Text)) return;
                MasterProfitCenter _pc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, txtPc.Text.Trim());
                if (_pc == null || string.IsNullOrEmpty(_pc.Mpc_com))
                {
                    MessageBox.Show("Please select the valid profit center", "Invalid Profir Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPc.Clear();
                    txtPc.Focus();
                    return;
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

        private void cmbGvTypen_KeyDown(object sender, KeyEventArgs e)
        {if(e.KeyCode ==Keys.Enter)
        {
            txtItemGvCoden.Focus();
        }
        }

        private void txtBookn_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtItemGvCoden.Focus();
            //}
        }

        private void dtpItemValidFromn_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    dtpItemValidTon.Focus();
            //}
        }

        private void dtpItemValidTon_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtFirstn.Focus();
            //}
        }

        private void txtFirstn_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtLastn.Focus();
            //}

        }

        private void txtLastn_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtAmtn.Focus();
            //}
        }

        private void txtAmtn_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnAppNew.Focus();
            //}
        }

        private void txtFirstn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtLastn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtAmtn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtBookn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtLastn_TextChanged(object sender, EventArgs e)
        {
         
            if (!string.IsNullOrEmpty(txtFirstn.Text))
                calcnew();
        }

        private void txtItemGvCoden_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowsen_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.ShowDialog();
            txtUploadn.Text = openFileDialog.FileName;
        }

        private void btnupPCn_Click(object sender, EventArgs e)
        {
            try
            {

                string _msg = string.Empty;
                string _custCode = "";
                MasterItem _Mstitm = new MasterItem();
                List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();

                if (string.IsNullOrEmpty(txtUploadn.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Gift Vouchar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadn.Text = "";
                    txtUploadn.Focus();
                    return;
                }
                if (optTrans.Checked)
                {
                    if (string.IsNullOrEmpty(txtPc.Text))
                    {
                        MessageBox.Show("Please select transfer pc.", "Gift Vouchar", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPc.Focus();
                        return;
                    }
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadn.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Gift Vouchar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBrowsen.Focus();
                }

                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension.ToUpper() == ".XLS")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                }
                else if (Extension.ToUpper() == ".XLSX")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;

                }


                conStr = String.Format(conStr, txtUploadn.Text, "NO");
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

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                //DataTable tem = new DataTable();
                //tem.Columns.Add("PROFIT_CENTER");
                //tem.Columns.Add("PC_DESCRIPTION");
                if (dt.Rows.Count > 0)
                {
                    #region validate excel
                     DataRow row = dt.Rows[0];
                     foreach (DataRow _dr in dt.Rows)
                     {

                         if (row != _dr)
                         {


                             if (string.IsNullOrEmpty(_dr[1].ToString()))
                             {
                                 MessageBox.Show("Process halted. Invalid book number found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 return;
                             }
                             if (string.IsNullOrEmpty(_dr[5].ToString()))
                             {
                                 MessageBox.Show("Process halted. Invalid value found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 return;
                             }

                             if (string.IsNullOrEmpty(_dr[0].ToString()))
                             {
                                 MessageBox.Show("Process halted. Empty GV code found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 return;
                             }

                             Int32 _bkno = 0;
                             _bkno = Convert.ToInt32(_dr[1].ToString());
                             DataTable _dtlgvbokk = CHNLSVC.Inventory.GetAvailable_GV_books(_bkno, _dr[0].ToString(), BaseCls.GlbUserComCode);


                             if (optnew.Checked)
                             {
                                 if (_dtlgvbokk.Rows.Count > 0)
                                 {
                                     MessageBox.Show("Process halted. Vouchar book already exist !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                     return;
                                 }
                             }
                             if (optTrans.Checked)
                             {
                                 if (_dtlgvbokk.Rows.Count == 0)
                                 {
                                     MessageBox.Show("Process halted. Vouchar book is invalid !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                     return;
                                 }
                                 _bkno = Convert.ToInt32(_dr[1].ToString());
                                 Int32 _pageno = Convert.ToInt32(_dr[4].ToString());
                                 string _gvcode = _dr[0].ToString();
                                 List<GiftVoucherPages> _lstgb = CHNLSVC.Inventory.GetAvailableGvPagesRange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "VALUE", "P", _bkno, _gvcode, _pageno, _pageno);
                                 if (_lstgb.Count == 0)
                                 {
                                     MessageBox.Show("Process halted. Vouchar page is not available !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                     return;
                                 }
                             }



                             _Mstitm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString());
                             if (_Mstitm == null)
                             {
                                 MessageBox.Show("Process halted. Invalid GV code found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 return;
                             }
                             else
                             {
                                 if (_Mstitm.MI_CHK_CUST == 1)
                                     _CHK_CUST = 1;
                                 else
                                     _CHK_CUST = 0;
                             }

                             ////check valid customer
                             //if (_CHK_CUST == 1)
                             //{
                             //    //mobile
                             //    _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, _dr[6].ToString(), "C");
                             //    if (_custList == null && _custList.Count == 0)
                             //    {
                             //        //NIC
                             //        _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, _dr[7].ToString(), string.Empty, "C");
                             //        if (_custList == null && _custList.Count == 0)
                             //        {
                             //            MessageBox.Show("Process halted. Registered customer not found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                             //            return;
                             //        }

                             //    }
                             //}
                         }
                     }
                    #endregion

                    _lstgvPages = new List<GiftVoucherPages>();
                    DataRow row1 = dt.Rows[0];
                    foreach (DataRow _dr in dt.Rows)
                    {
                        if (row1 != _dr)
                        {
                            _custCode = "";
                            Int32 _count = _lstgvPages.Where(X => X.Gvp_gv_cd == _dr[0].ToString() && X.Gvp_book == Convert.ToInt32(_dr[1]) && X.Gvp_page == Convert.ToInt32(_dr[4])).Count();
                            if (_count > 0)
                                break;

                            _Mstitm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString());
                            if (_Mstitm.MI_CHK_CUST == 1)
                                _CHK_CUST = 1;
                            else
                                _CHK_CUST = 0;


                            //check valid customer
                            //if (_CHK_CUST == 1)
                            //{
                            //    //mobile
                            //    _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, _dr[6].ToString(), "C");
                            //    if (_custList != null && _custList.Count > 0)
                            //        _custCode = _custList[0].Mbe_cd;
                            //    else
                            //    {
                            //        //NIC
                            //        _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, _dr[7].ToString(), string.Empty, "C");
                            //        if (_custList != null && _custList.Count > 0)
                            //            _custCode = _custList[0].Mbe_cd;
                            //    }
                            //}

                            GiftVoucherPages _gvou = new GiftVoucherPages();
                            _gvou.Gvp_gv_tp = "VALUE";
                            _gvou.Gvp_amt = Convert.ToDecimal(_dr[5]);
                            _gvou.Gvp_app_by = BaseCls.GlbUserID;
                            _gvou.Gvp_bal_amt = Convert.ToDecimal(_dr[5]);
                            _gvou.Gvp_book = Convert.ToInt32(_dr[1]);
                            _gvou.Gvp_can_by = "";
                            _gvou.Gvp_can_dt = DateTime.Now.Date;
                            _gvou.Gvp_com = BaseCls.GlbUserComCode;
                            _gvou.Gvp_cre_by = BaseCls.GlbUserID;
                            _gvou.Gvp_cre_dt = DateTime.Now.Date;
                            _gvou.Gvp_cus_add1 = _dr[9].ToString();
                            _gvou.Gvp_cus_add2 = _dr[10].ToString();
                            _gvou.Gvp_cus_cd = _custCode;
                            _gvou.Gvp_cus_mob = _dr[6].ToString();
                            _gvou.Gvp_cus_name = _dr[8].ToString();
                            _gvou.Gvp_gv_cd = _dr[0].ToString();
                            _gvou.Gvp_gv_prefix = "P_GV";
                            _gvou.Gvp_is_allow_promo = false;
                            _gvou.Gvp_issu_itm = 0;
                            _gvou.Gvp_issue_by = "";
                            _gvou.Gvp_issue_dt = DateTime.Now.Date;
                            //_gvou.Gvp_line = i;
                            _gvou.Gvp_mod_by = "";
                            _gvou.Gvp_mod_dt = DateTime.Now.Date;
                            _gvou.Gvp_noof_itm = 1;
                            _gvou.Gvp_oth_ref = "";
                            _gvou.Gvp_page = Convert.ToInt32(_dr[4]);
                            if (optTrans.Checked)
                            { _gvou.Gvp_pc = txtPc.Text; }
                            else
                            {
                                _gvou.Gvp_pc = BaseCls.GlbUserDefProf;
                            }

                            _gvou.Gvp_ref = "";
                            _gvou.Gvp_stus = "P";
                            _gvou.Gvp_valid_from = Convert.ToDateTime(dtpItemValidFromn.Value.Date);
                            _gvou.Gvp_valid_to = Convert.ToDateTime(dtpItemValidTon.Value.Date);
                            _gvou.Gvp_cus_nic = txtNIC.Text;

                            _lstgvPages.Add(_gvou);
                        }
                    }

                }

                gvPagesn.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = _lstgvPages;
                gvPagesn.DataSource = _source;
            }

                        
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClearUpload_Click(object sender, EventArgs e)
        {
            txtUploadn.Text = "";
        }

        private void optTrans_CheckedChanged(object sender, EventArgs e)
        {
            if (optTrans.Checked)
            {
                txtPc.Enabled = true;
                btnSearch_Pc.Enabled = true;
            }
            else
            {
                 txtPc.Enabled = false;
                 btnSearch_Pc.Enabled = false;
            }
        }

        private void optnew_CheckedChanged(object sender, EventArgs e)
        {
            if (optnew.Checked)
            {
                txtPc.Enabled = false;
                btnSearch_Pc.Enabled = false;
                
            }
            else
            {
                txtPc.Enabled = true;
                btnSearch_Pc.Enabled = true;
            }
        }
    }
}
