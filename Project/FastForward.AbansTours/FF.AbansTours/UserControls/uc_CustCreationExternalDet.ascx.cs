using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;

namespace FF.AbansTours.UserControls
{
    public partial class uc_CustCreationExternalDet : System.Web.UI.UserControl
    {

        public string Addressline1
        {
            get { return txtAddresline1.Text.Trim(); }
            set { txtAddresline1.Text = value; }
        }
        public string Addressline2
        {
            get { return txtAddresline2.Text.Trim(); }
            set { txtAddresline2.Text = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmail.Attributes.Add("onblur", "return onblurFire(event,'" + btnEmail.ClientID + "')");
            txtWrEmail.Attributes.Add("onblur", "return onblurFire(event,'" + btnEmailWr.ClientID + "')");


            txtPhone.Attributes.Add("onblur", "return onblurFire(event,'" + btnPhn.ClientID + "')");
            txrCurPhone.Attributes.Add("onblur", "return onblurFire(event,'" + btnPhnCur.ClientID + "')");
            txtWrkPhone.Attributes.Add("onblur", "return onblurFire(event,'" + btnPhnWr.ClientID + "')");

            txtTown.Attributes.Add("onblur", "return onblurFire(event,'" + btnTown.ClientID + "')");
            txtCurTown.Attributes.Add("onblur", "return onblurFire(event,'" + btnCurTown.ClientID + "')");

        }
        #region Validations



        #endregion Validations
        public void EnableAddressPanel(bool command)
        {
            Panel_homeAddr.Enabled = command;
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            Boolean isValidEmail = CUST.IsValidEmail(txtEmail.Text.Trim());
            if (isValidEmail == false)
            {
                string Msg = "<script>alert('Invalid email address!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }

        }
        public MasterBusinessEntity GetExtraCustDet()
        {
            MasterBusinessEntity _businessEntity = new MasterBusinessEntity();

            //      _businessEntity.Mbe_acc_cd;
            //     _businessEntity.Mbe_cd;
            //     _businessEntity.Mbe_cre_pc;
            _businessEntity.Mbe_com = "ABL";
            //_businessEntity.Mbe_cre_by;
            _businessEntity.Mbe_act = true; ;
            _businessEntity.Mbe_add1 = txtAddresline1.Text.ToUpper() + txtAddresline2.Text.ToUpper();
            _businessEntity.Mbe_add2 = txtCurrentAddrline1.Text.ToUpper() + txtCurrentAddrline2.Text.ToUpper();
            _businessEntity.Mbe_country_cd = txtCountryCD.Text.Trim().ToUpper();
            _businessEntity.Mbe_email = txtEmail.Text.Trim();

            //    _businessEntity.Mbe_contact;

            _businessEntity.Mbe_distric_cd = txtDistrict.Text.Trim().ToUpper();
            _businessEntity.Mbe_town_cd = txtTown.Text.Trim().ToUpper();
            _businessEntity.Mbe_province_cd = txtProvince.Text.Trim().ToUpper();
            _businessEntity.Mbe_country_cd = txtCountryCD.Text.Trim().ToUpper();
            _businessEntity.Mbe_postal_cd = txtPostalCode.Text.Trim().ToUpper();
            //     _businessEntity.Mbe_fax;
            //     _businessEntity.Mbe_is_suspend;
            //----------vat/tax------------------------
            _businessEntity.Mbe_tax_no = txtVatRegNo.Text.Trim().ToUpper();
            _businessEntity.Mbe_is_tax = chkVATcustomer.Checked;
            _businessEntity.Mbe_tax_ex = chkVATextempted.Checked;
            //  _businessEntity.Mbe_tel = txtTelHome.Text.Trim();
            _businessEntity.Mbe_is_svat = chkSVATcustomer.Checked;
            _businessEntity.Mbe_svat_no = txtSVATno.Text.Trim().ToUpper();

            //----------current/recident-------------------------
            _businessEntity.Mbe_cr_add1 = txtCurrentAddrline1.Text.ToUpper();
            _businessEntity.Mbe_cr_add2 = txtCurrentAddrline2.Text.ToUpper();

            _businessEntity.Mbe_cr_distric_cd = txtCurDistrict.Text.Trim().ToUpper();
            _businessEntity.Mbe_cr_province_cd = txtCurProvince.Text.Trim().ToUpper();
            _businessEntity.Mbe_cr_town_cd = txtCurTown.Text.Trim().ToUpper();
            _businessEntity.Mbe_cr_country_cd = txtCurContryCD.Text.Trim().ToUpper();
            _businessEntity.Mbe_cr_postal_cd = txtCurPostalCode.Text.Trim().ToUpper();

            //   _businessEntity.Mbe_cr_tel = txtTelResidence.Text.Trim();
            // _businessEntity.Mbe_cr_fax


            //--------------working--------------------------
            _businessEntity.Mbe_wr_add1 = txtWrkAddrLine1.Text.ToUpper();
            _businessEntity.Mbe_wr_add2 = txtWrkAddrLine2.Text.ToUpper();
            //_businessEntity.Mbe_wr_country_cd=
            // _businessEntity.Mbe_wr_email=
            _businessEntity.Mbe_wr_fax = txtWrkFax.Text.Trim();
            _businessEntity.Mbe_wr_tel = txtWrkPhone.Text.Trim();
            _businessEntity.Mbe_wr_email = txtWrEmail.Text.Trim();
            _businessEntity.Mbe_town_cd = txtTown.Text.Trim().ToUpper();
            _businessEntity.Mbe_tp = "C";
            _businessEntity.Mbe_cre_dt = DateTime.Now.Date;

            _businessEntity.Mbe_wr_dept = txtWrkDept.Text.ToUpper();
            _businessEntity.Mbe_wr_designation = txtWrkDesignation.Text.ToUpper();
            _businessEntity.Mbe_wr_proffesion = txtWrkProfession.Text.ToUpper();
            _businessEntity.Mbe_wr_com_name = txtWorkComName.Text.ToUpper();

            return _businessEntity;

        }

        public void SetExtraValues(MasterBusinessEntity _businessEntity)
        {
            //      _businessEntity.Mbe_acc_cd;
            //     _businessEntity.Mbe_cd;
            //     _businessEntity.Mbe_cre_pc;
            // _businessEntity.Mbe_com = "ABL";
            //_businessEntity.Mbe_cre_by;
            // _businessEntity.Mbe_act = true; ;
            txtAddresline1.Text = _businessEntity.Mbe_add1;
            txtAddresline2.Text = _businessEntity.Mbe_add2;//txtCurrentAddrline1.Text + txtCurrentAddrline2.Text;

            txtEmail.Text = _businessEntity.Mbe_email;
            //    _businessEntity.Mbe_contact;
            //      _businessEntity.Mbe_country_cd =

            txtDistrict.Text = _businessEntity.Mbe_distric_cd;
            txtTown.Text = _businessEntity.Mbe_town_cd;
            txtProvince.Text = _businessEntity.Mbe_province_cd;
            txtCountryCD.Text = _businessEntity.Mbe_country_cd;

            txtPostalCode.Text = _businessEntity.Mbe_postal_cd;
            //     _businessEntity.Mbe_fax;
            //     _businessEntity.Mbe_is_suspend;
            //----------vat/tax------------------------
            txtVatRegNo.Text = _businessEntity.Mbe_tax_no;

            chkVATcustomer.Checked = _businessEntity.Mbe_is_tax;
            chkVATextempted.Checked = _businessEntity.Mbe_tax_ex;
            //  _businessEntity.Mbe_tel = txtTelHome.Text.Trim();
            chkSVATcustomer.Checked = _businessEntity.Mbe_is_svat;
            txtSVATno.Text = _businessEntity.Mbe_svat_no;

            //----------current/recident-------------------------
            txtCurrentAddrline1.Text = _businessEntity.Mbe_cr_add1;
            txtCurrentAddrline2.Text = _businessEntity.Mbe_cr_add2;
            //_businessEntity.Mbe_cr_country_cd=
            txtCurDistrict.Text = _businessEntity.Mbe_cr_distric_cd;
            txtCurProvince.Text = _businessEntity.Mbe_cr_province_cd;
            txtCurTown.Text = _businessEntity.Mbe_cr_town_cd;
            txtCurContryCD.Text = _businessEntity.Mbe_cr_country_cd;
            txtCurPostalCode.Text = _businessEntity.Mbe_cr_postal_cd;
            //   _businessEntity.Mbe_cr_tel = txtTelResidence.Text.Trim();
            // _businessEntity.Mbe_cr_fax
            //--------------working--------------------------
            txtWrkAddrLine1.Text = _businessEntity.Mbe_wr_add1;
            txtWrkAddrLine2.Text = _businessEntity.Mbe_wr_add2;
            //_businessEntity.Mbe_wr_country_cd=
            // _businessEntity.Mbe_wr_email=
            txtWrkFax.Text = _businessEntity.Mbe_wr_fax;
            txtWrkPhone.Text = _businessEntity.Mbe_wr_tel;
            txtWrEmail.Text = _businessEntity.Mbe_wr_email;

            txtWrkDept.Text = _businessEntity.Mbe_wr_dept;
            txtWrkDesignation.Text = _businessEntity.Mbe_wr_designation;
            txtWrkProfession.Text = _businessEntity.Mbe_wr_proffesion;
            txtWorkComName.Text = _businessEntity.Mbe_wr_com_name;
            // _businessEntity.Mbe_tp = "C";
            // _businessEntity.Mbe_cre_dt = DateTime.Now.Date;


            //Prevent from modifiying
            if (true)//TODO: according to permission enable/disable
            {
                Panel_homeAddr.Enabled = false;
            }
            if (_businessEntity.Mbe_cd == "" || _businessEntity.Mbe_cd==null)
            {
                Panel_homeAddr.Enabled = true;
            }
        }

        protected void btnPhn_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            Boolean isValid = CUST.IsValidTelephone(txtPhone.Text.Trim());
            if (isValid == false)
            {
                txtPhone.Text = "";
                string Msg = "<script>alert('Invalid Phone Number!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
        }

        protected void btnPhnCur_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            Boolean isValid = CUST.IsValidTelephone(txrCurPhone.Text.Trim());
            if (isValid == false)
            {
                txrCurPhone.Text = "";
                string Msg = "<script>alert('Invalid Mobile Number(Curent)!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }

        }

        protected void btnPhnWr_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            Boolean isValid = CUST.IsValidTelephone(txtWrkPhone.Text.Trim());
            if (isValid == false)
            {
                // txtWrkPhone.Focus();
                txtWrkPhone.Text = "";
                string Msg = "<script>alert('Invalid Phone Number(Work place)!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }

        }

        protected void btnEmailWr_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            Boolean isValidEmail = CUST.IsValidEmail(txtWrEmail.Text.Trim());
            if (isValidEmail == false)
            {
                string Msg = "<script>alert('Invalid email address!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
        }

        protected void btnTown_Click(object sender, EventArgs e)
        {
            txtDistrict.Text = "";
            txtProvince.Text = "";
            txtPostalCode.Text = "";
            txtCountryCD.Text = "";

            CustomerCreationUC CUST = new CustomerCreationUC();
            DataTable dt = new DataTable();
            dt = CUST.Get_DetBy_town(txtTown.Text.Trim());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string district = dt.Rows[0]["DISTRICT"].ToString();
                    string province = dt.Rows[0]["PROVINCE"].ToString();
                    string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                    string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();
                    txtDistrict.Text = district;
                    txtProvince.Text = province;
                    txtPostalCode.Text = postalCD;
                    txtCountryCD.Text = countryCD;
                }

            }
        }

        protected void btnCurTown_Click(object sender, EventArgs e)
        {
            txtCurDistrict.Text = "";
            txtCurProvince.Text = "";
            txtCurPostalCode.Text = "";
            txtCountryCD.Text = "";

            CustomerCreationUC CUST = new CustomerCreationUC();
            DataTable dt = new DataTable();
            
            dt = CUST.Get_DetBy_town(txtCurTown.Text.Trim());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string district = dt.Rows[0]["DISTRICT"].ToString();
                    string province = dt.Rows[0]["PROVINCE"].ToString();
                    string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                    string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();
                    
                    txtCurDistrict.Text = district;
                    txtCurProvince.Text = province;
                    txtCurPostalCode.Text = postalCD;
                    txtCurContryCD.Text = countryCD;

                }

            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            BasePage Base_page = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserID:
                    {
                        paramsText.Append(Base_page.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Base_page.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void imgBtnTownSearch_Click(object sender, ImageClickEventArgs e)
        {
            BasePage Base_page = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable dataSource = Base_page.CHNLSVC.CommonSearch.GetTown(ucc.SearchParams, null, null);

            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtTown.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void imgBtnSearchCurTown_Click(object sender, ImageClickEventArgs e)
        {
            BasePage Base_page = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable dataSource = Base_page.CHNLSVC.CommonSearch.GetTown(ucc.SearchParams, null, null);

            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCurTown.ClientID;
            ucc.UCModalPopupExtender.Show();
        }
    }
}