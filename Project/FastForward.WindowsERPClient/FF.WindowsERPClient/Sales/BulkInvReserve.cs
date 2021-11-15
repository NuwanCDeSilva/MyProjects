using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.Sales
{
    public partial class BulkInvReserve : FF.WindowsERPClient.Base
    {
        private MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private CustomerAccountRef _account = new CustomerAccountRef();
        private List<MasterBusinessEntityInfo> _busInfoList = new List<MasterBusinessEntityInfo>();
        private Int32 _isFoundBOCProject = 0;
        private static string _regRecNo = "";
        private Boolean _isGroup = false;
        private Boolean _isExsit = false;

        public BulkInvReserve()
        {
            InitializeComponent();
            txtBatch.Text = BaseCls.GlbUserDefLoca + "-" + BaseCls.GlbUserID;
            dtDate.Value = Convert.ToDateTime(DateTime.Now.Date);
        }

        private void Bind_Title()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("MR.", "Mr.");
            PartyTypes.Add("MRS.", "Mrs.");
            PartyTypes.Add("MS.", "Ms.");
            PartyTypes.Add("REV.", "Rev.");
            PartyTypes.Add("DR.", "Dr.");
            PartyTypes.Add("PROF.", "Prof.");

            cmbTitle.DataSource = new BindingSource(PartyTypes, null);
            cmbTitle.DisplayMember = "Value";
            cmbTitle.ValueMember = "Key";
        }

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearScreen()
        {
            //Clear the screen
            txtName.Clear();
            txtMob.Text = "";
            txtCusCode.Text = "";
            //txtDept.Text = "";
            //txtDesig.Text = "";
            txtDivSec.Text = "";
            txtInitial.Text = "";
            txtItemCode.Text = "";
            txtItemDesc.Text = "";
            txtLast.Text = "";
            txtListRef.Text = "";
            txtModel.Text = "";
            txtOffice.Text = "";
            txtPerAdd1.Text = "";
            txtPerAdd2.Text = "";
            txtPerDistrict.Text = "";
            txtPerProvince.Text = "";
            txtPerTown.Text = "";
            txtPhone.Text = "";
            txtSer2.Text = "";
            txtSerial.Text = "JF16EHE";
            txtSeqNo.Text = "0";
            txtItmStus.Text = "";
            //lblDesig.Text = "";
            //lblDept.Text = "";
            lblOffic.Text = "";
            txtRem.Text = "";

            txtItemCode2.Text = "";
            txtItemDesc2.Text = "";
            txtModel2.Text = "";
            txtSer22.Text = "";
            txtSerial2.Text = "JF16EHE";

            chkBRNo.Checked = true;
            chkDL.Checked = true;
            chkIID.Checked = true;
            chkNIC.Checked = true;
            chkPID.Checked = true;
            chkPP.Checked = true;

            txtCRRecDt.Text = "";
            txtNPRecDt.Text = "";
            txtDocRecDt.Text = "";
            txtRmvSentDt.Text = "";
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Towns:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Office:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BusDesig:
                    {
                        paramsText.Append(txtDept.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BusDept:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialNICAll:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        public void LoadCustProf(MasterBusinessEntity cust)
        {
            txtNIC.Text = cust.Mbe_nic;

            txtCusCode.Text = cust.Mbe_cd;

            txtName.Text = cust.MBE_FNAME;
            txtLast.Text = cust.MBE_SNAME;

            txtPerAdd1.Text = cust.Mbe_add1;
            txtPerAdd2.Text = cust.Mbe_add2;

            txtPhone.Text = cust.Mbe_tel;
            txtMob.Text = cust.Mbe_mob;

            txtPerTown.Text = cust.Mbe_town_cd;
            txtPerDistrict.Text = cust.Mbe_distric_cd;
            txtPerProvince.Text = cust.Mbe_province_cd;

            txtDept.Text = cust.Mbe_wr_dept;
            txtDept_Leave(null, null);
            txtDesig.Text = cust.Mbe_wr_designation;
            txtDesig_Leave(null, null);

            txtInitial.Text = cust.MBE_INI;
            txtOffice.Text = cust.Mbe_wr_town_cd;
            load_office_town();


            String nic_ = txtNIC.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(nic_))
            {
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = (nicarray[2]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    cmbTitle.Text = "Ms.";
                }
                else
                {
                    cmbTitle.Text = "Mr.";
                }
            }
            txtCusCode.Enabled = false;
        }

        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByNIC(nic);
        }

        private void Collect_Cust()
        {
            Boolean _isSMS = false;
            Boolean _isSVAT = false;
            Boolean _isVAT = false;
            Boolean _TaxEx = false;

            _custProfile = new MasterBusinessEntity();
            if (string.IsNullOrEmpty(txtCusCode.Text))
                _custProfile.Mbe_cd = null;
            else
                _custProfile.Mbe_cd = txtCusCode.Text;
            _custProfile.Mbe_acc_cd = null;
            _custProfile.Mbe_act = true;
            _custProfile.Mbe_tel = txtPhone.Text.ToUpper();
            _custProfile.Mbe_add1 = txtPerAdd1.Text.Trim().ToUpper();
            _custProfile.Mbe_add2 = txtPerAdd2.Text.Trim().ToUpper();
            _custProfile.Mbe_town_cd = txtPerTown.Text.ToUpper();
            _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_province_cd = txtPerProvince.Text;
            _isSMS = false;
            _custProfile.Mbe_agre_send_sms = _isSMS;
            _custProfile.Mbe_br_no = "";
            _custProfile.Mbe_cate = "INDIVIDUAL";
            _custProfile.Mbe_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_contact = null;
            _custProfile.Mbe_country_cd = null;
            _custProfile.Mbe_cr_add1 = txtPerAdd1.Text.Trim().ToUpper();
            _custProfile.Mbe_cr_add2 = txtPerAdd2.Text.Trim().ToUpper();
            _custProfile.Mbe_cr_country_cd = "LK";
            _custProfile.Mbe_cr_distric_cd = "";
            _custProfile.Mbe_cr_email = null;
            _custProfile.Mbe_cr_fax = null;
            _custProfile.Mbe_cr_postal_cd = "";
            _custProfile.Mbe_cr_province_cd = "";
            _custProfile.Mbe_cr_tel = "";
            _custProfile.Mbe_cr_town_cd = txtOffice.Text.Trim().ToUpper();
            _custProfile.Mbe_cre_by = BaseCls.GlbUserID;
            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _custProfile.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            _custProfile.Mbe_cust_com = BaseCls.GlbUserComCode.ToUpper();
            _custProfile.Mbe_cust_loc = BaseCls.GlbUserDefLoca.ToUpper();
            _custProfile.Mbe_distric_cd = txtPerDistrict.Text;
            _custProfile.Mbe_dl_no = "";
            _custProfile.Mbe_dob = DateTime.Now.Date;
            _custProfile.Mbe_email = "";
            _custProfile.Mbe_fax = null;
            _custProfile.Mbe_ho_stus = "GOOD";
            _custProfile.Mbe_income_grup = null;
            _custProfile.Mbe_intr_com = false;
            _custProfile.Mbe_is_suspend = false;
            _custProfile.Mbe_cust_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_cust_loc = BaseCls.GlbUserDefProf;
            _isSVAT = false;
            _custProfile.Mbe_is_svat = _isSVAT;
            _isVAT = false;
            _custProfile.Mbe_is_tax = _isVAT;
            _custProfile.Mbe_mob = txtMob.Text.Trim();
            _custProfile.Mbe_name = txtName.Text.Trim().ToUpper() + " " + txtLast.Text.Trim().ToUpper();
            _custProfile.Mbe_nic = txtNIC.Text.Trim().ToUpper();
            _custProfile.Mbe_oth_id_no = null;
            _custProfile.Mbe_oth_id_tp = null;
            _custProfile.Mbe_pc_stus = "GOOD";
            _custProfile.Mbe_postal_cd = "";
            _custProfile.Mbe_pp_no = "";
            _custProfile.Mbe_province_cd = txtPerProvince.Text.Trim().ToUpper();
            _custProfile.Mbe_sex = cmbTitle.Text == "Mr." ? "MALE" : "FEMALE";
            _custProfile.Mbe_sub_tp = null;
            _custProfile.Mbe_svat_no = "";
            _TaxEx = false;
            _custProfile.Mbe_tax_ex = _TaxEx;
            _custProfile.Mbe_tax_no = "";
            _custProfile.Mbe_tp = "C";
            _custProfile.Mbe_wr_add1 = "";
            _custProfile.Mbe_wr_add2 = "";
            _custProfile.Mbe_wr_com_name = "";
            _custProfile.Mbe_wr_country_cd = null;
            _custProfile.Mbe_wr_dept = txtDept.Text;
            _custProfile.Mbe_wr_designation = txtDesig.Text;
            _custProfile.Mbe_wr_distric_cd = null;
            _custProfile.Mbe_wr_email = "";
            _custProfile.Mbe_wr_fax = "";
            _custProfile.Mbe_wr_proffesion = null;
            _custProfile.Mbe_wr_province_cd = null;
            _custProfile.Mbe_wr_tel = "";
            _custProfile.Mbe_wr_town_cd = txtOffice.Text;

            _custProfile.MBE_TIT = (cmbTitle.Text).ToUpper();
            _custProfile.MBE_INI = txtInitial.Text;
            _custProfile.MBE_FNAME = txtName.Text;
            _custProfile.MBE_SNAME = txtLast.Text;


        }

        private void load_office_town()
        {
            if (!string.IsNullOrEmpty(txtOffice.Text))
            {
                DataTable _dt = CHNLSVC.General.GetTownByCode(txtOffice.Text);
                if (_dt.Rows.Count > 0)
                {
                    lblOffic.Text = _dt.Rows[0]["mt_desc"].ToString();
                }
                else
                {
                    MessageBox.Show("Invalid Office Town", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtOffice.Focus();
                    lblOffic.Text = "";
                }
            }
        }

        private void get_deptCode()
        {
            DataTable _dt = CHNLSVC.General.GetBusDesigByCode(txtDesig.Text);
            if (_dt.Rows.Count > 0)
            {
                DataTable _dt1 = CHNLSVC.General.GetBusDeptByCode(_dt.Rows[0]["mbd_dept_cd"].ToString());
                if (_dt1.Rows.Count > 0)
                {
                    lblDept.Text = _dt1.Rows[0]["mbdt_desc"].ToString();
                    txtDept.Text = _dt1.Rows[0]["mbdt_cd"].ToString();
                }
                else
                {
                    txtDept.Text = "";
                    lblDept.Text = "";
                }
            }

        }

        private void txtDesig_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDesig.Text))
            {
                DataTable _dt = CHNLSVC.General.GetBusDesigByCode(txtDesig.Text);
                if (_dt.Rows.Count > 0)
                {
                    lblDesig.Text = _dt.Rows[0]["mbd_desc"].ToString() + " (" + _dt.Rows[0]["mbd_rmk"].ToString() + ")";
                    if (string.IsNullOrEmpty(txtDept.Text))
                        get_deptCode();
                }
                else
                {
                    MessageBox.Show("Invalid Designation", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDesig.Focus();
                    lblDesig.Text = "";
                }
            }
        }

        private void txtDept_Leave(object sender, EventArgs e)
        {
            txtDesig.Text = "";
            lblDesig.Text = "";
            lblDept.Text = "";

            if (!string.IsNullOrEmpty(txtDept.Text))
            {
                DataTable _dt = CHNLSVC.General.GetBusDeptByCode(txtDept.Text);
                if (_dt.Rows.Count > 0)
                    lblDept.Text = _dt.Rows[0]["mbdt_desc"].ToString();
                else
                {
                    MessageBox.Show("Invalid Department", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDept.Text = "";
                    txtDept.Focus();
                }
            }
        }

        private void txtOffice_Leave(object sender, EventArgs e)
        {
            load_office_town();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Int16 X = CHNLSVC.Sales.UpdateBusDesig(txtDesig.Text, textBox1.Text);
            //txtDesig.Text = "";
            //textBox1.Text = "";
            //MessageBox.Show("Done", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSearchSerial2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch.ReturnIndex = 0;
            _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
            DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_commonSearch.SearchParams, null, null);
            _commonSearch.dvResult.DataSource = _result;
            _commonSearch.BindUCtrlDDLData(_result);
            _commonSearch.obj_TragetTextBox = txtSerial2;
            _commonSearch.IsSearchEnter = true;
            this.Cursor = Cursors.Default;
            _commonSearch.ShowDialog();
            btnUpdate.Select();

            txtSerial2_Leave(null, null);
        }

        private void txtSerial2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerial.Text))
            {
                txtItemCode2.Text = "";
                txtItemDesc2.Text = "";
                txtModel2.Text = "";
                txtSeqNo2.Text = "0";
                txtSer22.Text = "";
                txtItmStus2.Text = "";

                DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtSerial2.Text.Trim(), string.Empty);
                if (_multiItemforSerial.Rows.Count > 0)
                {
                    string _item = _multiItemforSerial.Rows[0].Field<string>("Item");
                    txtItemCode2.Text = _item;

                    MasterItem _itemdetail = new MasterItem();
                    if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                    if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;

                        txtItemDesc2.Text = _description;
                        txtModel2.Text = _model;

                        DataTable _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode2.Text, txtSerial2.Text);
                        if (_dtSer.Rows.Count > 0)
                        {
                            txtSeqNo2.Text = _dtSer.Rows[0]["ins_seq_no"].ToString();
                            txtSer22.Text = _dtSer.Rows[0]["ins_ser_2"].ToString();
                            txtItmStus2.Text = _dtSer.Rows[0]["ins_itm_stus"].ToString();
                        }
                    }
                }
            }
        }

        private void txtSerial2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchSerial2_Click(null, null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string _receiptNo = "";
            string _invNo = "";
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10089))
            {
                MessageBox.Show("Sorry, You have no permission to update!\n( Advice: Required permission code :10089)");
                return;
            }

            List<VehicalRegistration> _tempList = new List<VehicalRegistration>();
            DataTable _dt = CHNLSVC.Sales.getReceiptByEngNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtSerial.Text);
            if (_dt.Rows.Count > 0)
            {
                _receiptNo = _dt.Rows[0]["svrt_ref_no"].ToString();
                _invNo = _dt.Rows[0]["svrt_inv_no"].ToString();
                _tempList = CHNLSVC.Sales.GetVehicalRegByRefNo(_receiptNo);

                foreach (VehicalRegistration temp in _tempList)
                {
                    if (temp.P_srvt_rmv_stus == 1)
                    {
                        MessageBox.Show("Cannot cancel Receipt.Documents are already send to the RMV. Engine # : " + temp.P_svrt_engine, "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (temp.P_svrt_prnt_stus == 2)
                    {
                        MessageBox.Show("Cannot cancel Receipt. Engine # : " + temp.P_svrt_engine + " already cancel.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot Find the Receipt.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtSerial2.Text))
            {
                MessageBox.Show("Please enter new serial #", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSerial2.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                MessageBox.Show("Please select the customer", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNIC.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            if (txtSerial2.Enabled == true)
            {
                DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtSerial2.Text.Trim(), string.Empty);
                Int32 _isAvailable = _multiItemforSerial.Rows.Count;

                if (_isAvailable <= 0)
                {
                    MessageBox.Show("Selected serial no is invalid or not available in your location.\nPlease check your inventory.", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItemCode2.Clear();
                    txtItemDesc2.Clear();
                    txtModel2.Clear();
                    txtSerial2.Focus();
                    this.Cursor = Cursors.Default;
                    return;
                }
            }

            if (MessageBox.Show("Are you sure ?", "Message", MessageBoxButtons.YesNo) == DialogResult.No) return;

            string _cusCode = "";
            Collect_Cust();

            Int32 _effect = CHNLSVC.Sales.UpdateBulkSaleInvReservation(_custProfile, Convert.ToInt32(txtSeqNo.Text), txtBatch.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text, txtItmStus.Text, txtSerial.Text, txtSer2.Text, txtListRef.Text, BaseCls.GlbUserID, Convert.ToInt32(txtSeqNo2.Text), txtItemCode2.Text, txtItmStus2.Text, txtSerial2.Text, txtSer22.Text, txtListRef.Text, txtRem.Text, Convert.ToDateTime(dtDate.Value), Convert.ToInt32(chkNIC.Checked), Convert.ToInt32(chkPP.Checked), Convert.ToInt32(chkPID.Checked), Convert.ToInt32(chkDL.Checked), Convert.ToInt32(chkBRNo.Checked), Convert.ToInt32(chkIID.Checked),txtRec.Text, out _cusCode);

            if (_effect == 1)
            {
                UpdateRecStatus(_receiptNo, _invNo);

                string _docNo;
                _docNo = _receiptNo;

                BaseCls.GlbReportDoc = _docNo;
                Reports.Reconciliation.clsRecon obj = new Reports.Reconciliation.clsRecon();
                obj.VehRegAppReport1();

                Reports.Sales.ReportViewer _view1 = new Reports.Sales.ReportViewer();
                BaseCls.GlbReportName = string.Empty;
                _view1.GlbReportName = string.Empty;
                _view1.GlbReportName = "VehicleRegistrationSlip.rpt";
                BaseCls.GlbReportName = "VehicleRegistrationSlip.rpt";
                _view1.GlbReportDoc = _docNo;
                BaseCls.GlbReportDoc = _docNo;
                _view1.Show();
                _view1 = null;
            }
            else
            {
                MessageBox.Show(_cusCode, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Cursor = Cursors.Default;
                return;
            }


            this.Cursor = Cursors.Default;

        }

        private void btnSrchDivSec_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDivSec;
            _CommonSearch.ShowDialog();
            txtDivSec.Select();
        }

        private void btnSrchNIC_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialNIC);
            DataTable _result = CHNLSVC.CommonSearch.GetSerialNIC(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtNIC;
            _CommonSearch.ShowDialog();

            btnSave.Enabled = true;
            btnPrint.Enabled = true;
            btnUpdate.Enabled = true;

            txtNIC.Select();
        }

        private void txtSerial_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerial.Text))
            {
                txtItemCode.Text = "";
                txtItemDesc.Text = "";
                txtModel.Text = "";
                txtSeqNo.Text = "0";
                txtSer2.Text = "";
                txtItmStus.Text = "";
                //txtColor.Text = "";

                DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtSerial.Text.Trim(), string.Empty);
                if (_multiItemforSerial.Rows.Count > 0)
                {
                    string _item = _multiItemforSerial.Rows[0].Field<string>("Item");
                    txtItemCode.Text = _item;

                    MasterItem _itemdetail = new MasterItem();
                    if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                    if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;

                        txtItemDesc.Text = _description;
                        txtModel.Text = _model;
                        //txtColor.Text=_itemdetail.M

                        DataTable _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text, txtSerial.Text);
                        if (_dtSer.Rows.Count > 0)
                        {
                            txtSeqNo.Text = _dtSer.Rows[0]["ins_seq_no"].ToString();
                            txtSer2.Text = _dtSer.Rows[0]["ins_ser_2"].ToString();
                            txtItmStus.Text = _dtSer.Rows[0]["ins_itm_stus"].ToString();
                        }
                    }
                }
            }

        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                Boolean _isvalid = IsValidMobileOrLandNo(txtPhone.Text.Trim());

                if (_isvalid == false)
                {
                    MessageBox.Show("Invalid phone number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }
            }
        }



        private void txtMob_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMob.Focus();
                    return;
                }
            }
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchSerial_Click(null, null);
        }

        private void btnSrchDesig_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtDept.Text))
                //{
                //    MessageBox.Show("Select the department", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    txtDept.Focus();
                //    return;
                //}
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BusDesig);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusDesigData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDesig; //txtBox;
                _CommonSearch.ShowDialog();
                txtDesig.Focus();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSrchDep_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BusDept);
                DataTable _result = CHNLSVC.CommonSearch.SearchBusDeptData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDept; //txtBox;
                _CommonSearch.ShowDialog();
                txtDept.Focus();
                txtDesig.Text = "";
                lblDesig.Text = "";
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchDep_Click(null, null);
        }

        private void txtDesig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchDesig_Click(null, null);
        }

        private void txtOffice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchOffic_Click(null, null);
        }

        private void txtPerTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSrchTown_Click(null, null);
        }

        private void btnSearchSerial_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch.ReturnIndex = 0;
            _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
            DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_commonSearch.SearchParams, null, null);
            _commonSearch.dvResult.DataSource = _result;
            _commonSearch.BindUCtrlDDLData(_result);
            _commonSearch.obj_TragetTextBox = txtSerial;
            _commonSearch.IsSearchEnter = true;
            this.Cursor = Cursors.Default;
            _commonSearch.ShowDialog();
            btnSave.Select();

            txtSerial_Leave(null, null);
        }

        private void txtSerial_Enter(object sender, EventArgs e)
        {
            txtSerial.SelectionStart = txtSerial.Text.Length + 1;
        }


        private void txtName_Leave(object sender, EventArgs e)
        {
            string name = txtName.Text;
            var parts = name.Split(' ');
            string initials = "";

            foreach (var part in parts)
            {
                initials += Regex.Match(part, "[A-Z]");
                initials = initials + "";
            }
            txtInitial.Text = initials;
        }

        private void btnSrchTown_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town_new);
            DataTable _result = CHNLSVC.CommonSearch.GetTown_new(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPerTown;
            _CommonSearch.ShowDialog();
            txtPerTown.Select();
        }



        private void txtPerTown_Leave(object sender, EventArgs e)
        {
            txtPerDistrict.Text = "";
            txtPerProvince.Text = "";
            txtDivSec.Text = "";

            if (!string.IsNullOrEmpty(txtPerTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtPerTown.Text.Trim());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string district = dt.Rows[0]["DISTRICT"].ToString();
                        string province = dt.Rows[0]["PROVINCE"].ToString();

                        txtPerDistrict.Text = district;
                        txtPerProvince.Text = province;
                        DataTable _dt1 = CHNLSVC.General.GetTownByCode(dt.Rows[0]["DS"].ToString());
                        if (_dt1.Rows.Count > 0)
                        {
                            txtDivSec.Text = _dt1.Rows[0]["mt_desc"].ToString();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPerTown.Text = "";
                        txtPerTown.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPerTown.Text = "";
                    txtPerTown.Focus();
                }
            }
        }

        private void btnSrchOffic_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Office);
            DataTable _result = CHNLSVC.CommonSearch.searchOfficeTownData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtOffice;
            _CommonSearch.ShowDialog();
            txtOffice.Select();

            load_office_town();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNIC.Text))
            {
                MessageBox.Show("Please enter NIC #", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNIC.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSerial.Text))
            {
                MessageBox.Show("Please enter serial #", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSerial.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPerTown.Text))
            {
                MessageBox.Show("Please select the town code", "Invalid Town", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPerTown.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDesig.Text))
            {
                MessageBox.Show("Please select the designation", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDesig.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Please enter phone number", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPhone.Focus();
                return;
            }
            //if (string.IsNullOrEmpty(txtDivSec.Text))
            //{
            //    MessageBox.Show("Please select the divisional secretariat", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txtDivSec.Focus();
            //    return;
            //}
            if (string.IsNullOrEmpty(txtOffice.Text))
            {
                MessageBox.Show("Please select the office", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtOffice.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDept.Text))
            {
                MessageBox.Show("Please select the Department/Ministry", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDept.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter first name", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLast.Text))
            {
                MessageBox.Show("Please enter last name", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLast.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRec.Text))
            {
                MessageBox.Show("Please enter receipt #", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRec.Focus();
                return;
            }


            this.Cursor = Cursors.WaitCursor;
            if (txtSerial.Enabled == true)
            {
                DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtSerial.Text.Trim(), string.Empty);
                Int32 _isAvailable = _multiItemforSerial.Rows.Count;

                if (_isAvailable <= 0)
                {
                    MessageBox.Show("Selected serial no is invalid or not available in your location.\nPlease check your inventory.", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItemCode.Clear();
                    txtItemDesc.Clear();
                    txtModel.Clear();
                    txtSerial.Focus();
                    this.Cursor = Cursors.Default;
                    return;
                }
            }

            if (MessageBox.Show("Are you sure ?", "Message", MessageBoxButtons.YesNo) == DialogResult.No) return;

            string _cusCode = "";
            string _cusTMCode = "";
            Collect_Cust();
            Collect_GroupCust();

            Int32 _effct = 0;
            if (_isExsit == false)
            {
                List<BusEntityItem> _lstBusItm = new List<BusEntityItem>();
                _effct = CHNLSVC.Sales.SaveBusinessEntityDetailWithGroup(_custProfile, _account, _busInfoList,_lstBusItm, out _cusCode, null, _isExsit, _isGroup, _custGroup);
            }
            else
            {
                Boolean _alwCustChange = CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "ALWCUST");
                _cusCode = txtCusCode.Text.Trim();
                if (_alwCustChange == true)
                {
                    List<BusEntityItem> _lstBusItm = new List<BusEntityItem>();
                    _effct = CHNLSVC.Sales.UpdateBusinessEntityProfileWithGroup(_custProfile, BaseCls.GlbUserID, Convert.ToDateTime(DateTime.Today).Date, 0, _busInfoList, null,_lstBusItm, _custGroup);
                }
                else
                {
                    if (MessageBox.Show("Access denied to change the customer details.(ALWCUST) ! \n Do you want to continue without updating customer details ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
            }
            _custProfile.Mbe_cd = _cusCode;
            Int32 _effect = CHNLSVC.Sales.SaveBulkSaleInvReservation(_custProfile, Convert.ToInt32(txtSeqNo.Text), txtBatch.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text, txtItmStus.Text, txtSerial.Text, txtSer2.Text, txtListRef.Text, BaseCls.GlbUserID, txtRem.Text, Convert.ToDateTime(dtDate.Value), Convert.ToInt32(chkNIC.Checked), Convert.ToInt32(chkPP.Checked), Convert.ToInt32(chkPID.Checked), Convert.ToInt32(chkDL.Checked), Convert.ToInt32(chkBRNo.Checked), Convert.ToInt32(chkIID.Checked), _isFoundBOCProject,txtRec.Text, out _cusTMCode);

            if (_effect == 1)
            {
                SaveReceiptHeader(true, _cusCode);
            }
            else
            {
                MessageBox.Show(_cusTMCode, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Cursor = Cursors.Default;
                return;
            }

        }

        private void Collect_GroupCust()
        {
            _custGroup = new GroupBussinessEntity();
            _custGroup.Mbg_cd = txtCusCode.Text.Trim();
            _custGroup.Mbg_name = txtName.Text.Trim();
            _custGroup.Mbg_tit = cmbTitle.Text;
            _custGroup.Mbg_ini = txtInitial.Text.Trim();
            _custGroup.Mbg_fname = txtName.Text.Trim();
            _custGroup.Mbg_sname = txtLast.Text.Trim();
            _custGroup.Mbg_nationality = "SL";
            _custGroup.Mbg_add1 = txtPerAdd1.Text.Trim();
            _custGroup.Mbg_add2 = txtPerAdd2.Text.Trim();
            _custGroup.Mbg_town_cd = txtPerTown.Text.Trim();
            _custGroup.Mbg_distric_cd = txtPerDistrict.Text.Trim();
            _custGroup.Mbg_province_cd = txtPerProvince.Text.Trim();
            //_custGroup.Mbg_country_cd = txtPerCountry.Text.Trim();
            _custGroup.Mbg_tel = txtPhone.Text.Trim();
            _custGroup.Mbg_fax = "";
            //_custGroup.Mbg_postal_cd = txtPerPostal.Text.Trim();
            _custGroup.Mbg_mob = txtMob.Text.Trim();
            _custGroup.Mbg_nic = txtNIC.Text.Trim();
            //_custGroup.Mbg_pp_no = txtPP.Text.Trim();
            //_custGroup.Mbg_dl_no = txtDL.Text.Trim();
            //_custGroup.Mbg_br_no = txtBR.Text.Trim();
            //_custGroup.Mbg_email = txtPerEmail.Text.Trim();
            _custGroup.Mbg_contact = "";
            _custGroup.Mbg_act = true;
            _custGroup.Mbg_is_suspend = false;
            _custGroup.Mbg_sex = cmbTitle.Text == "Mr." ? "MALE" : "FEMALE";
            _custGroup.Mbg_dob = DateTime.Now.Date;
            _custGroup.Mbg_cre_by = BaseCls.GlbUserID;
            _custGroup.Mbg_mod_by = BaseCls.GlbUserID;

        }

        private void SaveReceiptHeader(Boolean _isNew, string _custCode)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            List<VehicalRegistration> _tempRegSave = new List<VehicalRegistration>();
            List<VehicleInsuarance> _tempInsSave = new List<VehicleInsuarance>();

            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.GetSerialID();
            _ReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;
            _ReceiptHeader.Sar_receipt_type = "VHREG";
            _ReceiptHeader.Sar_receipt_no = _ReceiptHeader.Sar_seq_no.ToString();
            _ReceiptHeader.Sar_prefix = "AUTO";
            _ReceiptHeader.Sar_manual_ref_no = "0";
            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(DateTime.Now.Date).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
            _ReceiptHeader.Sar_debtor_cd = txtCusCode.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = txtName.Text + ' ' + txtLast.Text;
            _ReceiptHeader.Sar_debtor_add_1 = txtPerAdd1.Text;
            _ReceiptHeader.Sar_debtor_add_2 = txtPerAdd2.Text;
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = txtMob.Text.Trim();
            _ReceiptHeader.Sar_nic_no = txtNIC.Text.Trim();
            _ReceiptHeader.Sar_tot_settle_amt = 0;
            _ReceiptHeader.Sar_comm_amt = 0;
            _ReceiptHeader.Sar_is_mgr_iss = false;
            _ReceiptHeader.Sar_esd_rate = 0;
            _ReceiptHeader.Sar_wht_rate = 0;
            _ReceiptHeader.Sar_epf_rate = 0;
            _ReceiptHeader.Sar_currency_cd = "LKR";
            _ReceiptHeader.Sar_uploaded_to_finance = false;
            _ReceiptHeader.Sar_act = true;
            _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sar_direct_deposit_branch = "";
            _ReceiptHeader.Sar_remarks = txtRem.Text.Trim();
            _ReceiptHeader.Sar_is_used = false;
            _ReceiptHeader.Sar_ref_doc = "";
            _ReceiptHeader.Sar_ser_job_no = "";
            _ReceiptHeader.Sar_used_amt = 0;
            _ReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
            _ReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
            _ReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
            _ReceiptHeader.Sar_anal_1 = txtPerDistrict.Text;
            _ReceiptHeader.Sar_anal_2 = txtPerProvince.Text.Trim();
            _ReceiptHeader.Sar_anal_3 = "MANUAL";
            _ReceiptHeader.Sar_anal_8 = 0;
            _ReceiptHeader.Sar_anal_4 = "";
            _ReceiptHeader.Sar_anal_5 = 0;
            _ReceiptHeader.Sar_anal_6 = 0;
            _ReceiptHeader.Sar_anal_7 = 0;
            _ReceiptHeader.Sar_anal_9 = 0;

            List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
            RecieptItem line = new RecieptItem();
            Int32 _line = 0;
            line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
            _line = _line + 1;
            line.Sard_line_no = _line;
            line.Sard_pay_tp = "CASH";
            line.Sard_settle_amt = 0;
            _ReceiptDetailsSave.Add(line);

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "RECEIPT";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "AUTO";
            masterAuto.Aut_year = null;

            DataTable _pcInfo = new DataTable();
            _pcInfo = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);


            MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
            masterAutoRecTp.Aut_cate_cd = BaseCls.GlbUserDefProf;
            masterAutoRecTp.Aut_cate_tp = "PC";
            masterAutoRecTp.Aut_direction = null;
            masterAutoRecTp.Aut_modify_dt = null;

            if (_pcInfo.Rows[0]["mpc_ope_cd"].ToString() == "INV_LRP" && BaseCls.GlbUserComCode == "LRP")
            {
                masterAutoRecTp.Aut_moduleid = "REC_LRP";
            }
            else
            {
                masterAutoRecTp.Aut_moduleid = "RECEIPT";
            }
            masterAutoRecTp.Aut_number = 5;//what is Aut_number
            masterAutoRecTp.Aut_start_char = "VHREG";
            masterAutoRecTp.Aut_year = null;

            List<VehicalRegistration> _regList = new List<VehicalRegistration>();
            _regList.Add(AssingRegDetails("na", null, _isNew));

            foreach (VehicalRegistration _reg in _regList)
            {
                Int32 _vehSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "VHREG", 1, BaseCls.GlbUserComCode);
                _reg.P_seq = _vehSeq;
                _reg.P_svrt_cust_cd = _custCode;
                _tempRegSave.Add(_reg);
            }

            string QTNum = "";

            if (_isFoundBOCProject == 0)
            {
                row_aff = (Int32)CHNLSVC.Sales.SaveNewReceipt(_ReceiptHeader, _ReceiptDetailsSave, masterAuto, null, null, _tempRegSave, _tempInsSave, null,null, masterAutoRecTp, null, out QTNum);
            }
            else
            {
                row_aff = 1;
            }

            if (row_aff == 1)
            {
                MessageBox.Show("Successfully saved", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearScreen();
                txtNIC.Text = "";

                if (_isNew == true)
                {
                    string _docNo;
                    _docNo = QTNum;

                    BaseCls.GlbReportDoc = _docNo;
                    Reports.Reconciliation.clsRecon obj = new Reports.Reconciliation.clsRecon();
                    obj.VehRegAppReport1();


                    BaseCls.GlbReportDoc = _docNo;
                    Reports.Reconciliation.clsRecon obj1 = new Reports.Reconciliation.clsRecon();
                    obj1.VehicleRegistrationSlip();
                }
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show(QTNum, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Cursor = Cursors.Default;
                return;
            }
        }

        private VehicalRegistration AssingRegDetails(string _recNo, string _invoiceNo, Boolean _is_New)
        {
            VehicalRegistration _tempReg = new VehicalRegistration();
            MasterItem _itemList = new MasterItem();

            if (_is_New == true)
                _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);
            else
                _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode2.Text);

            _tempReg.P_seq = 1;
            _tempReg.P_srvt_ref_no = _recNo;
            _tempReg.P_svrt_com = BaseCls.GlbUserComCode;
            _tempReg.P_svrt_pc = BaseCls.GlbUserDefProf;
            _tempReg.P_svrt_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _tempReg.P_svrt_inv_no = _invoiceNo;
            _tempReg.P_svrt_sales_tp = "";
            _tempReg.P_svrt_reg_val = 0;
            _tempReg.P_svrt_claim_val = 0;
            _tempReg.P_svrt_id_tp = "NIC";
            _tempReg.P_svrt_id_ref = txtNIC.Text.Trim();
            _tempReg.P_svrt_cust_cd = txtCusCode.Text.Trim();
            _tempReg.P_svrt_cust_title = cmbTitle.Text;
            _tempReg.P_svrt_last_name = txtLast.Text;
            _tempReg.P_svrt_full_name = txtName.Text.Trim();
            _tempReg.P_svrt_initial = txtInitial.Text.Trim();
            _tempReg.P_svrt_add01 = txtPerAdd1.Text;
            _tempReg.P_svrt_add02 = txtPerAdd2.Text.Trim();
            _tempReg.P_svrt_city = txtDivSec.Text;
            _tempReg.P_svrt_district = txtPerDistrict.Text;
            _tempReg.P_svrt_province = txtPerProvince.Text;
            _tempReg.P_svrt_contact = txtMob.Text.Trim();
            _tempReg.P_svrt_model = "PLEASURE";  // _itemList.Mi_model;
            _tempReg.P_svrt_brd = _itemList.Mi_brand;
            if (_is_New == true)
            {
                _tempReg.P_svrt_chassis = txtSer2.Text.Trim();
                _tempReg.P_svrt_engine = txtSerial.Text.Trim();
                _tempReg.P_srvt_itm_cd = txtItemCode.Text.Trim();
                _tempReg.P_svrt_color = txtColor.Text;
            }
            else
            {
                _tempReg.P_svrt_chassis = txtSer22.Text.Trim();
                _tempReg.P_svrt_engine = txtSerial2.Text.Trim();
                _tempReg.P_srvt_itm_cd = txtItemCode2.Text.Trim();
                _tempReg.P_svrt_color = txtColor2.Text;
            }

            _tempReg.P_svrt_fuel = "PETROL";
            _tempReg.P_svrt_capacity = 102;
            _tempReg.P_svrt_unit = "";
            _tempReg.P_svrt_horse_power = 0;
            _tempReg.P_svrt_wheel_base = 0;
            _tempReg.P_svrt_tire_front = "";
            _tempReg.P_svrt_tire_rear = "";
            _tempReg.P_svrt_weight = 0;
            _tempReg.P_svrt_man_year = Convert.ToInt32(txtYOM.Text);
            _tempReg.P_svrt_import = "";
            _tempReg.P_svrt_authority = "";
            _tempReg.P_svrt_country = "INDIA";
            _tempReg.P_svrt_custom_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_clear_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_declear_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_importer = "";
            _tempReg.P_svrt_importer_add01 = "";
            _tempReg.P_svrt_importer_add02 = "";
            _tempReg.P_svrt_cre_bt = BaseCls.GlbUserID;
            _tempReg.P_svrt_cre_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_prnt_stus = 1;
            _tempReg.P_svrt_prnt_by = "";
            _tempReg.P_svrt_prnt_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_srvt_rmv_stus = 0;
            _tempReg.P_srvt_rmv_by = "";
            _tempReg.P_srvt_rmv_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_veh_reg_no = "";
            _tempReg.P_svrt_reg_by = "";
            _tempReg.P_svrt_reg_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_image = "";
            _tempReg.P_srvt_cust_stus = 0;
            _tempReg.P_srvt_cust_by = "";
            _tempReg.P_srvt_cust_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_srvt_cls_stus = 0;
            _tempReg.P_srvt_cls_by = "";
            _tempReg.P_srvt_cls_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_srvt_insu_ref = "";

            return _tempReg;

        }

        private void UpdateRecStatus(string _recNo, string _invNo)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            List<VehicalRegistration> _regList = new List<VehicalRegistration>();
            _regList.Add(AssingRegDetails(_recNo, _invNo, false));

            row_aff = (Int32)CHNLSVC.Sales.VehRegReceiptCancelProcess(_regList);

            if (row_aff == 1)
            {
                MessageBox.Show("Successfully updated", "Customer creation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearScreen();
                txtNIC.Text = "";
                txtNIC.Focus();
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Not Updated.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Cursor = Cursors.Default;
                return;
            }
        }

        private void LoadCustProfByGrup(GroupBussinessEntity _cust)
        {

            txtNIC.Text = _cust.Mbg_nic;
            //txtPP.Text = _cust.Mbg_pp_no;
            //txtBR.Text = _cust.Mbg_br_no;
            txtCusCode.Text = _cust.Mbg_cd;
            //txtDL.Text = _cust.Mbg_dl_no;
            txtMob.Text = _cust.Mbg_mob;
            //cmbSex.Text = _cust.Mbg_sex;
            txtName.Text = _cust.Mbg_name;
            cmbTitle.Text = _cust.Mbg_tit;
            txtName.Text = _cust.Mbg_fname;
            txtLast.Text = _cust.Mbg_sname;
            txtInitial.Text = _cust.Mbg_ini;

            txtPerAdd1.Text = _cust.Mbg_add1;
            txtPerAdd2.Text = _cust.Mbg_add2;
            txtPerTown.Text = _cust.Mbg_town_cd;
            //txtPerPostal.Text = _cust.Mbg_postal_cd;
            //txtPerCountry.Text = _cust.Mbg_country_cd;
            txtPerDistrict.Text = _cust.Mbg_distric_cd;
            txtPerProvince.Text = _cust.Mbg_province_cd;
            txtPhone.Text = _cust.Mbg_tel;
            //txtPerEmail.Text = _cust.Mbg_email;

        }
        private void txtNIC_Leave(object sender, EventArgs e)
        {
            _isExsit = false;
            btnSearchSerial.Enabled = true;
            txtSerial.Enabled = true;
            _isFoundBOCProject = 0;
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid NIC.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearScreen();
                    txtDept.Text = "";
                    txtDesig.Text = "";
                    lblDesig.Text = "";
                    lblDept.Text = "";
                    txtNIC.Focus();
                    return;
                }
                else
                {
                    ClearScreen();
                    txtDept.Text = "";
                    lblDesig.Text = "";
                    lblDept.Text = "";
                    txtDesig.Text = "";
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, txtNIC.Text.Trim(), "", "", "", "", 1);
                    //if (_custList != null && _custList.Count > 1 && txtNIC.Text.ToUpper() != "N/A")
                    //{
                    //    string _custNIC = "Duplicate customers found!\n";
                    //    foreach (var _nicCust in _custList)
                    //    {
                    //        _custNIC = _custNIC + _nicCust.Mbe_cd.ToString() + " | " + _nicCust.Mbe_name.ToString() + "\n";
                    //    }
                    //    MessageBox.Show(_custNIC, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtNIC.Text = "";
                    //    txtNIC.Focus();
                    //    return;
                    //}

                    MasterBusinessEntity custProf = GetbyNIC(txtNIC.Text.Trim().ToUpper());
                    if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                    {
                        _isExsit = true;
                        LoadCustProf(custProf);
                    }
                    else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                    {
                        MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //btnCreate.Enabled = false;
                        LoadCustProf(custProf);
                    }
                    else

                        if (_isExsit == true)
                        {
                            string nic = txtNIC.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtNIC.Text = nic;
                        }
                        else
                        {
                            //Check the group level
                            GroupBussinessEntity _grupProf = CHNLSVC.Sales.GetCustomerProfileByGrup(null, txtNIC.Text.Trim().ToUpper(), null, null, null, null);
                            if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                            {
                                LoadCustProfByGrup(_grupProf);
                                _isGroup = true;
                                _isExsit = false;
                                //  btnCreate.Text = "Create";
                                return;
                            }
                            else
                            {
                                _isGroup = false;
                            }
                        }

                    if (_isExsit == true)
                    {
                        _isFoundBOCProject = 0;
                        //check already record in temp_boc_project for this customer
                        DataTable _dtCust = CHNLSVC.Sales.GetCustInBOCProject(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtCusCode.Text);
                        if (_dtCust.Rows.Count > 0)
                        {
                            _isFoundBOCProject = 1;
                            btnSearchSerial.Enabled = false;
                            btnUpdate.Enabled = true;
                            btnSave.Enabled = false;
                            txtSerial.Enabled = false;
                            txtSerial.Text = _dtCust.Rows[0]["ser1"].ToString();
                            txtListRef.Text = _dtCust.Rows[0]["ref_no"].ToString();
                            txtSer2.Text = _dtCust.Rows[0]["ser2"].ToString();
                            chkBRNo.Checked = Convert.ToBoolean(_dtCust.Rows[0]["IS_BR_NO"]);
                            txtRem.Text = _dtCust.Rows[0]["rem"].ToString();
                            txtEntredBy.Text = _dtCust.Rows[0]["cre_by"].ToString();
                            txtEnterOn.Text = _dtCust.Rows[0]["cre_dt"].ToString();
                            txtRec.Text = _dtCust.Rows[0]["rec_no"].ToString();

                            //get registration number
                            txtRegNo.Text = "";
                            _regRecNo = "";
                            DataTable _dt = CHNLSVC.Sales.GetInsuranceOnEngine(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "Reg1", txtSerial.Text);
                            if (_dt.Rows.Count > 0)
                            {
                                txtRegNo.Text = _dt.Rows[0]["SVRT_VEH_REG_NO"].ToString();
                                _regRecNo = _dt.Rows[0]["svrt_ref_no"].ToString();
                                txtDivSec.Text = _dt.Rows[0]["svrt_city"].ToString();
                                txtRmvSentDt.Text = _dt.Rows[0]["srvt_rmv_dt"].ToString();
                                txtCRRecDt.Text = _dt.Rows[0]["SVRT_REG_DT"].ToString();
                                txtDocRecDt.Text = _dt.Rows[0]["SVRT_CRE_DT"].ToString();
                                txtNPRecDt.Text = _dt.Rows[0]["srvt_plt_rec_dt"].ToString();
                            }

                            DataTable _dtSer = CHNLSVC.General.getDetail_on_serial2(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtSerial.Text);
                            if (_dtSer.Rows.Count > 0)
                            {
                                txtItemCode.Text = _dtSer.Rows[0]["Mi_cd"].ToString();
                                txtItemDesc.Text = _dtSer.Rows[0]["Mi_longdesc"].ToString();
                                txtModel.Text = _dtSer.Rows[0]["mi_model"].ToString();
                            }

                            DataTable _dtSer1 = CHNLSVC.Inventory.GetSerialDetailsBySerial1(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text, txtSerial.Text);
                            if (_dtSer1.Rows.Count > 0)
                            {
                                txtSeqNo.Text = _dtSer1.Rows[0]["ins_seq_no"].ToString();
                            }


                            if (Convert.ToInt32(_dtCust.Rows[0]["STUS"]) != 0)
                            {
                                btnSave.Enabled = false;
                                btnUpdate.Enabled = false;

                            }
                            else
                            {
                                btnSave.Enabled = true;
                                btnUpdate.Enabled = true;

                            }
                        }
                        else
                        {
                            _isFoundBOCProject = 0;
                            txtSerial.Text = "JF16EHE";
                            txtListRef.Text = "";
                            btnSearchSerial.Enabled = true;
                            btnUpdate.Enabled = false;
                            txtSerial.Enabled = true;
                            btnSave.Enabled = true;
                        }

                    }
                    else
                    {

                        if (_isExsit == true)
                        {
                            string nic = txtNIC.Text.Trim().ToUpper();
                            MasterBusinessEntity cust_null = new MasterBusinessEntity();
                            LoadCustProf(cust_null);
                            txtNIC.Text = nic;
                        }
                        _isExsit = false;

                        String nic_ = txtNIC.Text.Trim().ToUpper();
                        char[] nicarray = nic_.ToCharArray();
                        string thirdNum = (nicarray[2]).ToString();
                        if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                        {
                            cmbTitle.Text = "Miss.";
                        }
                        else
                        {
                            cmbTitle.Text = "Mr.";
                        }

                    }
                }
            }
        }

        private void BulkInvReserve_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTel_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtMob.Focus();
                    return;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
            txtNIC.Text = "";
            txtDept.Text = "";
            txtDesig.Text = "";
            lblDesig.Text = "";
            lblDept.Text = "";
            btnSave.Enabled = true;
            btnPrint.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            string _docNo;
            _docNo = _regRecNo;

            BaseCls.GlbReportDoc = _docNo;
            Reports.Reconciliation.clsRecon obj = new Reports.Reconciliation.clsRecon();
            obj.VehRegAppReport1();

            Reports.Reconciliation.clsRecon obj1 = new Reports.Reconciliation.clsRecon();
            obj1.VehicleRegistrationSlip();

            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
            BaseCls.GlbReportProfit = BaseCls.GlbUserDefLoca;
            BaseCls.GlbReportComp = "ABANS AUTO PVT LTD";
            BaseCls.GlbReportCompAddr = "NO: 498, GALLE ROAD, COLOMBO 03";
            BaseCls.GlbReportName = "BOCCusResReceipt.rpt";
            BaseCls.GlbReportDoc = txtSerial.Text;

            _view.Show();
            _view = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialNICAll);
            DataTable _result = CHNLSVC.CommonSearch.GetSerialNICAll(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtNIC;
            _CommonSearch.ShowDialog();
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnUpdate.Enabled = false;
            txtNIC.Select();
        }

        private void txtRec_Leave(object sender, EventArgs e)
        {
            int num;
            bool isNumeric = int.TryParse(txtRec.Text.ToString(), out num);
            if (isNumeric)
            {

            }
            else
            {
                MessageBox.Show("Please enter a valid receipt number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRec.Focus();
                return;
            }
         
        }

    }
}
