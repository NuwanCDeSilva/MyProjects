using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Text.RegularExpressions;
namespace FF.WindowsERPClient.General
{
    public partial class ToursEnquiry : Base
    {
        private CommonSearch.CommonSearch _commonSearch = null;
        private MasterBusinessEntity _masterBusinessCompany = null;
        private GEN_CUST_ENQ _tourEnq = new GEN_CUST_ENQ();
        private MasterAutoNumber _enqAuto = null;
        public ToursEnquiry()
        {
            InitializeComponent();
            getEnquiry_types();
        }
         private class CenterWinDialog : IDisposable
        {
            private int mTries = 0;
            private Form mOwner;

            public CenterWinDialog(Form owner)
            {
                mOwner = owner;
                owner.BeginInvoke(new MethodInvoker(findDialog));
            }

            private void findDialog()
            {
                // Enumerate windows to find the message box
                if (mTries < 0) return;
                EnumThreadWndProc callback = new EnumThreadWndProc(checkWindow);
                if (EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero))
                {
                    if (++mTries < 10) mOwner.BeginInvoke(new MethodInvoker(findDialog));
                }
            }

            private bool checkWindow(IntPtr hWnd, IntPtr lp)
            {
                // Checks if <hWnd> is a dialog
                StringBuilder sb = new StringBuilder(260);
                GetClassName(hWnd, sb, sb.Capacity);
                if (sb.ToString() != "#32770") return true;
                // Got it
                Rectangle frmRect = new Rectangle(mOwner.Location, mOwner.Size);
                RECT dlgRect;
                GetWindowRect(hWnd, out dlgRect);
                MoveWindow(hWnd,
                    frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2,
                    frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2,
                    dlgRect.Right - dlgRect.Left,
                    dlgRect.Bottom - dlgRect.Top, true);
                return false;
            }

            public void Dispose()
            {
                mTries = -1;
            }

            // P/Invoke declarations
            private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);

            [DllImport("user32.dll")]
            private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);

            [DllImport("kernel32.dll")]
            private static extern int GetCurrentThreadId();

            [DllImport("user32.dll")]
            private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);

            [DllImport("user32.dll")]
            private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);

            [DllImport("user32.dll")]
            private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

            private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtEnquiry_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                clear();
            }
        }

        private void clear()
        {
         txtCustomer.Text ="";
         txtemail.Text ="";
         txtAdd2.Text ="";
         txtAdd1.Text ="";
         txtName.Text ="";
         txtNIC.Text =""; 
         txtEnquiry.Text ="";
         txtFBl.Text ="";
         txtRef.Text ="";
         txtFacilityId.Text ="";
         txtFacilityCom.Text ="";
         txtMobile.Text = "";
         dtpExpected.Value = Convert.ToDateTime(DateTime.Today).Date;
        }
        private void getEnquiry_types()
           
        {   List<MST_ENQTP>  _qnyType =new   List<MST_ENQTP>();
        _qnyType = CHNLSVC.Tours.GET_ENQUIRY_TYPE(BaseCls.GlbUserComCode);

        cmbFacility.DataSource = _qnyType;
        cmbFacility.DisplayMember = "met_desc";
        cmbFacility.ValueMember = "met_cd";
    
        }
        private void getEnquiry_status()
        {
   
           DataTable  _qnyStatus = CHNLSVC.Tours.GET_ENQUIRY_STATUS(BaseCls.GlbUserComCode);

           comStatus.DataSource = _qnyStatus;
           comStatus.DisplayMember = "mes_desc";
           comStatus.ValueMember = "mes_id";
           comStatus.SelectedValue = 1;

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to save this ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                List<MST_FACBY> _objFacBy = new List<MST_FACBY>();
                string _err = string.Empty;
                _enqAuto = new MasterAutoNumber();

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Please enter name of customer", "Tours Enquiry .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtMobile.Text))
                {
                    MessageBox.Show("Please enter mobile #", "Tours Enquiry .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMobile.Text))
                {
                    MessageBox.Show("Please enter mobile #", "Tours Enquiry .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMobile.Focus();
                    return;
                }



                if (dtpExpected.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Expected date can't be back date", "Tours Enquiry .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMobile.Focus();
                    return;
                }
                try
                {
                    

                    _objFacBy = CHNLSVC.Tours.GET_FACILITY_BY(BaseCls.GlbUserComCode, Convert.ToString(cmbFacility.SelectedValue));
                    _tourEnq = new GEN_CUST_ENQ();
                    _tourEnq.GCE_ENQ_ID = txtFacilityId.Text;
                    _tourEnq.GCE_REF = txtRef.Text;
                    _tourEnq.GCE_ENQ_TP = Convert.ToString(cmbFacility.SelectedValue);
                    _tourEnq.GCE_COM = BaseCls.GlbUserComCode;
                    _tourEnq.GCE_PC = BaseCls.GlbUserDefProf;
                    _tourEnq.GCE_DT = DateTime.Today;
                    _tourEnq.GCE_EXPECT_DT = Convert.ToDateTime(dtpExpected.Value).Date;
                    _tourEnq.GCE_CUS_CD = txtCustomer.Text;
                    _tourEnq.GCE_NAME = txtName.Text;
                    _tourEnq.GCE_ADD1 = txtAdd1.Text;
                    _tourEnq.GCE_ADD2 = txtAdd2.Text;
                    _tourEnq.GCE_MOB = txtMobile.Text;
                    _tourEnq.GCE_EMAIL = txtemail.Text;
                    _tourEnq.GCE_NIC = txtNIC.Text;
                    _tourEnq.GCE_SER_LVL = string.Empty;
                    _tourEnq.GCE_ENQ = txtEnquiry.Text;
                    _tourEnq.GCE_ENQ_COM = txtFacilityCom.Text;


                    _tourEnq.GCE_ENQ_PC = _objFacBy[0].MFB_FACPC;
                    _tourEnq.GCE_ENQ_PC_DESC = string.Empty;
                    _tourEnq.GCE_STUS = 1;
                    _tourEnq.GCE_CRE_BY = BaseCls.GlbUserID;
                    _tourEnq.GCE_MOD_BY = BaseCls.GlbUserID;
                    _tourEnq.GCE_CRE_DT=DateTime.Today;
                    _tourEnq.GCE_MOD_DT = DateTime.Today;
          
                    if (string.IsNullOrEmpty(txtFacilityId.Text) == false)
                    {
                        GEN_CUST_ENQ _tourEnq1 = CHNLSVC.Tours.GET_CUST_ENQRY(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtFacilityId.Text);
                        if (_tourEnq1 != null)
                        {
                            _tourEnq.GCE_SEQ = _tourEnq1.GCE_SEQ;
                        }

                    }


                    _enqAuto = new MasterAutoNumber();
                  //  _enqAuto.Aut_cate_cd = _objFacBy[0].MFB_FACPC;
                    _enqAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                    _enqAuto.Aut_cate_tp = "PC";
                    _enqAuto.Aut_direction = null;
                    _enqAuto.Aut_modify_dt = null;
                    _enqAuto.Aut_moduleid = "AT";
                    _enqAuto.Aut_number = 0;
                    _enqAuto.Aut_start_char = "AT";
                    _enqAuto.Aut_year = Convert.ToDateTime(DateTime.Today).Year;

                    int _effect = CHNLSVC.Tours.Save_GEN_CUST_ENQ(_tourEnq, _enqAuto, out _err);
                    if (_effect == 1)
                    {
                        MessageBox.Show(_err, "Enquiry .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show(" Process Terminated : ", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception err)
                {
                    Cursor.Current = Cursors.Default;
                    CHNLSVC.CloseChannel();
                    MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtFacilityId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFacilityId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbFacility.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearch_Click(null, null);
            }
          
        }

        private void cmbFacility_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFacilityCom.Focus();
            }

        }

        private void txtFacilityCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRef.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearchCom_Click(null, null);
            }
        }

        private void txtRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFBl.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearch_Click(null, null);
            }
        }

        private void txtFBl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCustomer.Focus();
            }
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMobile.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSrhLocation_Click(null, null);
            }
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNIC.Focus();
            }
         
        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtName.Focus();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAdd1.Focus();
            }
        }

        private void txtAdd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAdd2.Focus();
            }
        }

        private void txtAdd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEnquiry.Focus();
            }
        }

        private void txtEnquiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCreate.Focus();
            }
        }

         
        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TourFacCom:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + cmbFacility.SelectedValue);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TourEnquiry:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator +  BaseCls.GlbUserDefProf);
                        break;
                    }
 
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        #region Regular Expressions


        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool IsValidNIC(string nic)
        {
            string pattern = @"^[0-9]{9}[V,X]{1}$";

            System.Text.RegularExpressions.Match match = Regex.Match(nic.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static bool IsValidMobileOrLandNo(string mobile)
        {
            string pattern = @"^[0-9]{10}$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        #endregion
      
   private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } }


        private void btnSrhLocation_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCustomer;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCustomer.Select();
                loadCustomer();
            }
            catch (Exception ex) { txtCustomer.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {

        }
        private void loadCustomer()
        {
            _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCustomer.Text))
                //_masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, BaseCls.GlbUserComCode);



            if (_masterBusinessCompany.Mbe_cd != null)
            {
                if (_masterBusinessCompany.Mbe_act == false)
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    ClearCustomer(true);
                    txtCustomer.Focus();
                    return;


                }
                txtName.Text = _masterBusinessCompany.Mbe_name;
                txtAdd1.Text = _masterBusinessCompany.Mbe_add1;
                txtAdd2.Text = _masterBusinessCompany.Mbe_add2;
                txtMobile.Text = _masterBusinessCompany.Mbe_mob;
                txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                txtemail.Text = _masterBusinessCompany.Mbe_email;
            }
            else
            {
                txtName.Clear();
                txtAdd1.Clear();
                txtAdd2.Clear();
                txtMobile.Clear();
                txtNIC.Clear();
                txtemail.Clear();
            }
        }
        private void txtCustomer_Leave(object sender, EventArgs e)
        {
            loadCustomer();
        }
        private void ClearCustomer(bool _isCustomer)
        {
            if (_isCustomer) txtCustomer.Clear();
            txtName.Clear();
            txtAdd1.Clear();
            txtAdd2.Clear();
            txtMobile.Clear();
            txtNIC.Clear();
            txtemail.Clear();
        }

        private void txtFacilityId_Leave(object sender, EventArgs e)
        {
            LoadEnquiry();
        }
        private void LoadEnquiry()
        {
            if (string.IsNullOrEmpty(txtFacilityId.Text) == false)
            {
                _tourEnq = CHNLSVC.Tours.GET_CUST_ENQRY(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtFacilityId.Text);
                if (_tourEnq != null)
                {
                    txtFacilityId.Text = _tourEnq.GCE_ENQ_ID;
                    txtRef.Text = _tourEnq.GCE_REF;
                    cmbFacility.SelectedValue = _tourEnq.GCE_ENQ_TP;
                    dtpExpected.Value = Convert.ToDateTime(_tourEnq.GCE_EXPECT_DT).Date;
                    txtCustomer.Text = _tourEnq.GCE_CUS_CD;
                    txtName.Text = _tourEnq.GCE_NAME;
                    txtAdd1.Text = _tourEnq.GCE_ADD1;
                    txtAdd2.Text = _tourEnq.GCE_ADD2;
                    txtMobile.Text = _tourEnq.GCE_MOB;
                    txtemail.Text = _tourEnq.GCE_EMAIL;
                    txtNIC.Text = _tourEnq.GCE_NIC;
                    _tourEnq.GCE_SER_LVL = string.Empty;
                    txtEnquiry.Text = _tourEnq.GCE_ENQ;
                    txtFacilityCom.Text = _tourEnq.GCE_ENQ_COM;
                    txtStatus.Text = _tourEnq.MES_DESC;
                    txtFBl.Text = _tourEnq.GCE_ENQ_PC;

                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TourEnquiry);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENQUIRY(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFacilityId;
                _CommonSearch.ShowDialog();
                txtFacilityId.Select();
                if (!string.IsNullOrEmpty(txtFacilityId.Text))
                {
                    LoadEnquiry();
                }
                txtFacilityId.Focus();
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TourFacCom);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_FAC_COM(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFacilityCom;
                _CommonSearch.ShowDialog();
                txtFacilityCom.Select();

                txtFacilityCom.Focus();
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

        private void btnView_Click(object sender, EventArgs e)
        {
            pnlView.Visible = true;
            pnlView.Height = 463;
            pnlView.Width  = 739;
            List<GEN_CUST_ENQ> _tourEnqList  ;
            getEnquiry_status();
            string _sts= Convert.ToString("1") ;
            _tourEnqList = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _sts, BaseCls.GlbUserID,15001);
            dgvEnquiry.AutoGenerateColumns = false;
            dgvEnquiry.DataSource = _tourEnqList;
        }

        private void btn_srch_req_Click(object sender, EventArgs e)
        {
            List<GEN_CUST_ENQ> _tourEnqList;
            string _sts = Convert.ToString(comStatus.SelectedValue);
            _tourEnqList = CHNLSVC.Tours.GET_ENQRY_BY_PC_STATUS(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _sts, BaseCls.GlbUserID, 15001);
            dgvEnquiry.AutoGenerateColumns = false;
            dgvEnquiry.DataSource = _tourEnqList;
        }

        private void btnPopupLastClose_Click(object sender, EventArgs e)
        {
            pnlView.Visible = false;
        }

        private void txtMobile_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMobile.Text))
            {
                btnCreate.Enabled = true;
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                Boolean _isValid = IsValidMobileOrLandNo(txtMobile.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid mobile number.", "Tours Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobile.Text = "";
                    txtMobile.Focus();
                    return;
                }
            }
        }

        private void txtNIC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                
                Boolean _isValid = IsValidNIC(txtNIC.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid NIC.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
            }
        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtemail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtemail.Text))
            {
                Boolean _isValid = IsValidEmail(txtemail.Text.Trim());

                if (_isValid == false)
                {
                    MessageBox.Show("Invalid email address.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtemail.Text = "";
                    txtemail.Focus();
                    return;
                }
            }
        }

        private void btnNewCust_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustomer;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txtCustomer.Select();
            }
            catch (Exception ex)
            { txtCustomer.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
        }

        private void txtFacilityId_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void txtFacilityCom_DoubleClick(object sender, EventArgs e)
        {
            btnSearchCom_Click(null, null);
        }

        private void txtCustomer_DoubleClick(object sender, EventArgs e)
        {
            btnSrhLocation_Click(null, null);
        }

        private void txtFacilityCom_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}
