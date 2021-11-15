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
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.General;
using System.IO;
using System.Configuration;
using System.Data.OleDb;


//Written By kapila on 31/3/2014
namespace FF.WindowsERPClient.General
{
    public partial class LoyaltyVoucherGen : Base
    {
        private string _cardSer = "";
        private List<GiftVoucherPages> _lstgvPages = new List<GiftVoucherPages>();

        public LoyaltyVoucherGen()
        {
            InitializeComponent();
            load_vou_types();
        }

        private void load_vou_types()
        {
            DataTable _dt = CHNLSVC.Financial.getMyAbVouTypes();
            gvVouTp.AutoGenerateColumns = false;
            gvVouTp.DataSource = _dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            lblAdd1.Text = "";
            lblCus.Text = "";
            lblCusName.Text = "";
            lblMobile.Text = "";
            txtCardNo.Text = "";
            //Add by Akila 2016/11/22 to clear the entered gv no and gridviews
            txtVouNo.Text = "";
            gvDet.DataSource = null;
            _lstgvPages = new List<GiftVoucherPages>();
            load_vou_types();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCardNo:
                    {

                        DateTime _date = CHNLSVC.Security.GetServerDateTime();

                        paramsText.Append("MYAB" + seperator + _date.Date.ToString("d") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PartyType:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        paramsText.Append(null + seperator + 0 + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void ClearScreen()
        {

            lblAdd1.Text = "";
            lblCus.Text = "";
            lblCusName.Text = "";
            lblMobile.Text = "";

        }

        private void btnCardNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCardNo);
            DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCardNo(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCardNo;
            _CommonSearch.ShowDialog();
            txtCardNo.Select();
            load_cus_details();

        }

        private void load_cus_details()
        {
            //Commented by Akila 2016/11/22
            //if (!string.IsNullOrEmpty(lblCus.Text))
            //{
            lblCus.Text = "";
            lblCusName.Text = "";
            lblAdd1.Text = "";
            lblMobile.Text = "";

            if (!string.IsNullOrEmpty(txtCardNo.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetLoyaltyMemberByCardNo(txtCardNo.Text);
                if (_dt.Rows.Count > 0)
                {
                    MasterBusinessEntity custProf = CHNLSVC.Sales.GetCustomerProfileByCom(_dt.Rows[0]["salcm_cus_cd"].ToString(), null, null, null, null, BaseCls.GlbUserComCode);
                    if (custProf.Mbe_cd != null)
                    {
                        lblCus.Text = custProf.Mbe_cd;
                        lblCusName.Text = custProf.Mbe_name;
                        lblAdd1.Text = custProf.Mbe_add1 + ' ' + custProf.Mbe_add2;
                        lblMobile.Text = custProf.Mbe_mob;
                    }
                }
            }
            //}
        }

        private void checkBox_HIERCHY_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gvVouTp.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                gvVouTp.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Add by Akila - 2016/11/22 Vaidate controls. check for data type and null values
                ValidateControls();

                Int32 _page = Convert.ToInt32(txtVouNo.Text);
                Int32 _i = 1;


                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                int _selectedVoucherCount = 0;
                foreach (DataGridViewRow row in gvVouTp.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _selectedVoucherCount += 1;

                        DataTable _dtVouDet = CHNLSVC.Financial.getVouDetByCode(row.Cells["mmvd_cd"].Value.ToString());

                        if (_dtVouDet.Rows.Count > 0)
                        {
                            Boolean _isFnd = CHNLSVC.Financial.isGVFound(_dtVouDet.Rows[0]["spd_seq"].ToString(), lblCus.Text);
                            if (_isFnd == true)
                            {
                                MessageBox.Show("This voucher type is already issued for this customer", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            GiftVoucherPages _gvou = new GiftVoucherPages();
                            _gvou.Gvp_gv_tp = "VALUE";
                            _gvou.Gvp_amt = Convert.ToDecimal(_dtVouDet.Rows[0]["spd_disc"]);
                            _gvou.Gvp_app_by = BaseCls.GlbUserID;
                            _gvou.Gvp_bal_amt = Convert.ToDecimal(_dtVouDet.Rows[0]["spd_disc"]);
                            _gvou.Gvp_book = _page;
                            _gvou.Gvp_can_by = "PRO_VOU";
                            _gvou.Gvp_can_dt = DateTime.Now.Date;
                            _gvou.Gvp_com = BaseCls.GlbUserComCode;
                            _gvou.Gvp_cre_by = BaseCls.GlbUserID;
                            _gvou.Gvp_cre_dt = DateTime.Now.Date;
                            _gvou.Gvp_cus_add1 = lblAdd1.Text;
                            _gvou.Gvp_cus_cd = lblCus.Text;
                            _gvou.Gvp_cus_mob = lblMobile.Text;
                            _gvou.Gvp_cus_name = lblCusName.Text;
                            _gvou.Gvp_gv_cd = row.Cells["mmvd_cd"].Value.ToString();
                            _gvou.Gvp_gv_prefix = "P_GV";
                            _gvou.Gvp_is_allow_promo = false;
                            _gvou.Gvp_issu_itm = 0;
                            _gvou.Gvp_issue_by = "";
                            _gvou.Gvp_issue_dt = DateTime.Now.Date;
                            _gvou.Gvp_line = _i;
                            _gvou.Gvp_mod_by = "";
                            _gvou.Gvp_mod_dt = DateTime.Now.Date;
                            _gvou.Gvp_noof_itm = 1;
                            _gvou.Gvp_oth_ref = "MYAB";
                            _gvou.Gvp_page = _page;
                            _gvou.Gvp_pc = "HO";
                            _gvou.Gvp_ref = _dtVouDet.Rows[0]["spd_seq"].ToString();
                            _gvou.Gvp_stus = "A";
                            _gvou.Gvp_valid_from = Convert.ToDateTime(DateTime.Now.Date);
                            _gvou.Gvp_valid_to = Convert.ToDateTime(DateTime.Now.Date.AddYears(1));
                            _gvou.Gvp_cus_nic = lblNIC.Text;
                            _gvou.Gvp_from = row.Cells["mmvd_desc"].Value.ToString(); // add by Akila 2016/11/22 to show the description of selected gv type.

                            _page = _page + 1;
                            _i = _i + 1;

                            _lstgvPages.Add(_gvou);
                        }
                        else 
                        {
                            throw new Exception("Records not found for selected voucher types");
                        }
                    }
                }

                if (_selectedVoucherCount == 0)
                {
                    throw new Exception("Please select a voucher type.");
                }
                else
                {
                    gvDet.AutoGenerateColumns = false;
                    gvDet.DataSource = new List<GiftVoucherPages>();
                    gvDet.DataSource = _lstgvPages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ///////------------------Commented by Akila - 2016/11/22 -----------------------------
            //if (string.IsNullOrEmpty(txtVouNo.Text))
            //{
            //    MessageBox.Show("Please enter the starting voucher #", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            //foreach (DataGridViewRow row in gvVouTp.Rows)
            //{

            //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
            //    if (Convert.ToBoolean(chk.Value) == true)
            //    {

            //        //DataTable _dtVouDet = CHNLSVC.Financial.getVouDetByCode(row.Cells["mmvd_cd"].Value.ToString());

            //        //Boolean _isFnd = CHNLSVC.Financial.isGVFound(_dtVouDet.Rows[0]["spd_seq"].ToString(), lblCus.Text);
            //        //if (_isFnd == true)
            //        //{
            //        //    MessageBox.Show("This voucher type is already issued for this customer", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //    return;
            //        //}

            //        //GiftVoucherPages _gvou = new GiftVoucherPages();
            //        //_gvou.Gvp_gv_tp = "VALUE";
            //        //_gvou.Gvp_amt = Convert.ToDecimal(_dtVouDet.Rows[0]["spd_disc"]);
            //        //_gvou.Gvp_app_by = BaseCls.GlbUserID;
            //        //_gvou.Gvp_bal_amt = Convert.ToDecimal(_dtVouDet.Rows[0]["spd_disc"]);
            //        //_gvou.Gvp_book = _page;
            //        //_gvou.Gvp_can_by = "PRO_VOU";
            //        //_gvou.Gvp_can_dt = DateTime.Now.Date;
            //        //_gvou.Gvp_com = BaseCls.GlbUserComCode;
            //        //_gvou.Gvp_cre_by = BaseCls.GlbUserID;
            //        //_gvou.Gvp_cre_dt = DateTime.Now.Date;
            //        //_gvou.Gvp_cus_add1 = lblAdd1.Text;
            //        //// _gvou.Gvp_cus_add2 = txtCusAdd2.Text;
            //        //_gvou.Gvp_cus_cd = lblCus.Text;
            //        //_gvou.Gvp_cus_mob = lblMobile.Text;
            //        //_gvou.Gvp_cus_name = lblCusName.Text;
            //        //_gvou.Gvp_gv_cd = row.Cells["mmvd_cd"].Value.ToString();
            //        //_gvou.Gvp_gv_prefix = "P_GV";
            //        //_gvou.Gvp_is_allow_promo = false;
            //        //_gvou.Gvp_issu_itm = 0;
            //        //_gvou.Gvp_issue_by = "";
            //        //_gvou.Gvp_issue_dt = DateTime.Now.Date;
            //        //_gvou.Gvp_line = _i;
            //        //_gvou.Gvp_mod_by = "";
            //        //_gvou.Gvp_mod_dt = DateTime.Now.Date;
            //        //_gvou.Gvp_noof_itm = 1;
            //        //_gvou.Gvp_oth_ref = "MYAB";
            //        //_gvou.Gvp_page = _page;
            //        //_gvou.Gvp_pc = "HO";
            //        //_gvou.Gvp_ref = _dtVouDet.Rows[0]["spd_seq"].ToString();
            //        //_gvou.Gvp_stus = "A";
            //        //_gvou.Gvp_valid_from = Convert.ToDateTime(DateTime.Now.Date);
            //        //_gvou.Gvp_valid_to = Convert.ToDateTime(DateTime.Now.Date.AddYears(1));
            //        //_gvou.Gvp_cus_nic = lblNIC.Text;

            //        //_page = _page + 1;
            //        //_i = _i + 1;

            //        //_lstgvPages.Add(_gvou);
            //    }
            //}
            //gvDet.AutoGenerateColumns = false;
            //gvDet.DataSource = new List<GiftVoucherPages>();
            //gvDet.DataSource = _lstgvPages;


        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            Int32 _eff = 0;

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            _eff = CHNLSVC.Sales.SavePVoucherPages(_lstgvPages);

            MessageBox.Show("Successfully saved !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoyaltyVoucherGen formnew = new LoyaltyVoucherGen();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void txtVouNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtVouNo_Leave(object sender, EventArgs e)
        {
            ///////------------Commented by Akila --------------------
            //if ((!string.IsNullOrEmpty(txtVouNo.Text)) && (IsNumeric(txtVouNo.Text.Trim()) == false))
            //{
            //    MessageBox.Show("Please enter a valid voucher no. Voucher no shoulbe a numeric value", "Voucher No", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtVouNo.Text = "";
            //    txtVouNo.Focus();
            //    return;
            //}

        }

        private void txtCardNo_Leave(object sender, EventArgs e)
        {
            //Updated by Akila 2016/11/22
            if (!string.IsNullOrEmpty(txtCardNo.Text))
            {
                load_cus_details();
            }
            else if (string.IsNullOrEmpty(txtCardNo.Text))
            {
                lblAdd1.Text = "";
                lblCus.Text = "";
                lblCusName.Text = "";
                lblMobile.Text = "";
                return;
            }
        }

        //Updated by Akila 2016/11/22 - modefied to select customer for mouse dbl
        private void txtCardNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCardNo_Click(null, null);
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            //Updated by Akila 2016/11/22 - modefied to select customer for mouse dbl click and enter key
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtCardNo.Text))
                {
                    load_cus_details();
                }
                else
                {
                    lblAdd1.Text = "";
                    lblCus.Text = "";
                    lblCusName.Text = "";
                    lblMobile.Text = "";
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnCardNo_Click(null, null);
            }
        }

        // Add by Akila 2016/11/22 Browse for excel fill path
        private string GetFilePath()
        {
            string _filePath = null;

            try
            {
                OpenFileDialog _openFileDialog = new OpenFileDialog();

                _openFileDialog.InitialDirectory = @"C:\";
                _openFileDialog.Title = "Select the files";

                _openFileDialog.Filter = "Excel File (XLSX, XLS)|*.xlsx;*.xls";

                _openFileDialog.FilterIndex = 2;
                _openFileDialog.RestoreDirectory = true;


                if (_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = _openFileDialog.FileName;
                    if (!((Path.GetExtension(_filePath) == ".xls") || (Path.GetExtension(_filePath) == ".xlsx")))
                    {
                        MessageBox.Show("Invalid file format", "File Browser", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _filePath = null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return _filePath;
        }

        // Add by Akila 2016/11/22 load the data in excel file to a datatable
        private DataTable GetExcelData()
        {
            DataTable _voucherTb = new DataTable();
            try
            {
                string _excelFilePath = GetFilePath();
                FileInfo _fileInfo = new FileInfo(_excelFilePath);

                if (_fileInfo.Exists)
                {
                    string _excelConnection = null;
                    if (Path.GetExtension(_excelFilePath) == ".xls")
                    {
                        //get the connection string from app config file
                        _excelConnection = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _excelFilePath + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";
                    }
                    else if (Path.GetExtension(_excelFilePath) == ".xlsx")
                    {
                        _excelConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _excelFilePath + ";Extended Properties='Excel 12.0;HDR=NO';";
                    }

                    using (OleDbConnection _oleConnection = new OleDbConnection(_excelConnection))
                    {
                        OleDbDataAdapter _oleAdpter = new OleDbDataAdapter("select * from [Sheet1$]", _oleConnection);
                        _oleAdpter.Fill(_voucherTb);
                    }
                }
                else
                {
                    MessageBox.Show("File not exists. Please check the file path", "File Browser", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return _voucherTb;
        }

        // Add by Akila 2016/11/22 
        private void PrepareForUplaod()
        {
            DataTable _dataTb = new DataTable();

            try
            {
                _dataTb = GetExcelData();

                DataView _dataView = _dataTb.DefaultView;
                _dataView.Sort = "Card No";
                _dataTb = _dataView.ToTable();

                _lstgvPages = new List<GiftVoucherPages>();

                if (_dataTb.Rows.Count > 0)
                {
                    foreach (DataRow _row in _dataTb.Rows)
                    {
                        //Get loyalty card informetion
                        if (!DBNull.Value.Equals(_row["Card No"]))
                        {
                            DataTable _dt = CHNLSVC.Sales.GetLoyaltyMemberByCardNo(_row["Card No"].ToString());
                            if (_dt.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(_dt.Rows[0]["salcm_cus_cd"].ToString()))
                                {
                                    //Get customer profile details
                                    MasterBusinessEntity custProf = CHNLSVC.Sales.GetCustomerProfileByCom(_dt.Rows[0]["salcm_cus_cd"].ToString(), null, null, null, null, BaseCls.GlbUserComCode);

                                    //Load auto generated vouchers to grid
                                    LoadDataToGridGvDet(Convert.ToInt32(_row["Starting No"]), Convert.ToInt32(_row["Ending No"]), custProf);
                                }
                            }
                        }
                    }
                }
                else
                {
                    btnUploadExcel.Focus();
                    throw new Exception("File is empty");
                    //MessageBox.Show("File is empty", "Voucher Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message.ToString(), "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }

        //Add by Akila - 2016/11/22 Vaidate controls. check for data type and null values
        private void ValidateControls()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCardNo.Text))
                {
                    txtCardNo.Focus();
                    throw new Exception("Card number cannot be blank. Please select a cutomer");
                }
                else if (string.IsNullOrEmpty(txtVouNo.Text))
                {
                    txtVouNo.Focus();
                    throw new Exception("Voucher number cannot be blank. Please enter starting voucher number");
                }
                else if (IsNumeric(txtVouNo.Text.Trim()) == false)
                {
                    txtVouNo.Focus();
                    throw new Exception("Invalid voucher number. Voucher number should be a numeric value");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // Add by Akila 2016/11/22 load the data to voucher grid dynamicaly by using excel data
        private void LoadDataToGridGvDet(int _startingNo, int _endingNo, MasterBusinessEntity _customerProfile)
        {
            try
            {
                Int32 _i = 1;
                Int32 _page = _startingNo;

                // if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                int _selectedVoucherCount = 0;
                foreach (DataGridViewRow row in gvVouTp.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _selectedVoucherCount += 1;

                        DataTable _dtVouDet = CHNLSVC.Financial.getVouDetByCode(row.Cells["mmvd_cd"].Value.ToString());
                        if (_dtVouDet.Rows.Count > 0)
                        {
                            Boolean _isFnd = CHNLSVC.Financial.isGVFound(_dtVouDet.Rows[0]["spd_seq"].ToString(), lblCus.Text);
                            if (_isFnd == false)
                            {
                                GiftVoucherPages _gvou = new GiftVoucherPages();
                                _gvou.Gvp_gv_tp = "VALUE";
                                _gvou.Gvp_amt = Convert.ToDecimal(_dtVouDet.Rows[0]["spd_disc"]);
                                _gvou.Gvp_app_by = BaseCls.GlbUserID;
                                _gvou.Gvp_bal_amt = Convert.ToDecimal(_dtVouDet.Rows[0]["spd_disc"]);
                                _gvou.Gvp_book = _page;
                                _gvou.Gvp_can_by = "PRO_VOU";
                                _gvou.Gvp_can_dt = DateTime.Now.Date;
                                _gvou.Gvp_com = BaseCls.GlbUserComCode;
                                _gvou.Gvp_cre_by = BaseCls.GlbUserID;
                                _gvou.Gvp_cre_dt = DateTime.Now.Date;
                                _gvou.Gvp_cus_add1 = _customerProfile.Mbe_add1 + ' ' + _customerProfile.Mbe_add2;
                                _gvou.Gvp_cus_cd = _customerProfile.Mbe_cd;
                                _gvou.Gvp_cus_mob = _customerProfile.Mbe_mob;
                                _gvou.Gvp_cus_name = _customerProfile.Mbe_name;
                                _gvou.Gvp_gv_cd = row.Cells["mmvd_cd"].Value.ToString();
                                _gvou.Gvp_gv_prefix = "P_GV";
                                _gvou.Gvp_is_allow_promo = false;
                                _gvou.Gvp_issu_itm = 0;
                                _gvou.Gvp_issue_by = "";
                                _gvou.Gvp_issue_dt = DateTime.Now.Date;
                                _gvou.Gvp_line = _i;
                                _gvou.Gvp_mod_by = "";
                                _gvou.Gvp_mod_dt = DateTime.Now.Date;
                                _gvou.Gvp_noof_itm = 1;
                                _gvou.Gvp_oth_ref = "MYAB";
                                _gvou.Gvp_page = _page;
                                _gvou.Gvp_pc = "HO";
                                _gvou.Gvp_ref = _dtVouDet.Rows[0]["spd_seq"].ToString();
                                _gvou.Gvp_stus = "A";
                                _gvou.Gvp_valid_from = Convert.ToDateTime(DateTime.Now.Date);
                                _gvou.Gvp_valid_to = Convert.ToDateTime(DateTime.Now.Date.AddYears(1));
                                _gvou.Gvp_cus_nic = _customerProfile.Mbe_nic;
                                _gvou.Gvp_from = row.Cells["mmvd_desc"].Value.ToString();
                                _page = _page + 1;
                                _i = _i + 1;

                                _lstgvPages.Add(_gvou);

                                //MessageBox.Show("This voucher type is already issued for this customer", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //return;
                            }
                        }
                    }
                }

                if (_selectedVoucherCount == 0)
                {
                    throw new Exception("Please select a voucher type.");
                }
                else if (_selectedVoucherCount != (_endingNo + 1 - _startingNo))
                {
                    throw new Exception("Selected voucher count is incorrect.");
                }
                else
                {
                    gvDet.AutoGenerateColumns = false;
                    gvDet.DataSource = new List<GiftVoucherPages>();
                    gvDet.DataSource = _lstgvPages;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message.ToString(), "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }

        //Add by akila 2016-11-22 To upload voucher details from a excel file
        private void btnUploadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                PrepareForUplaod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


    }
}

