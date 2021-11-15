using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;

namespace FF.WebERPClient.UserControls
{

    public class BusinessEntityTYPE
    {
        //this object stores all types 
        private string typeCD_;

        public string TypeCD_
        {
            get { return typeCD_; }
            set { typeCD_ = value; }
        }
        private string typeDesctipt;

        public string TypeDesctipt
        {
            get { return typeDesctipt; }
            set { typeDesctipt = value; }
        }
        private Boolean isMandatory;

        public Boolean IsMandatory
        {
            get { return isMandatory; }
            set { isMandatory = value; }
        }
    }

    public class BusinessEntityVAL
    {
        //this object stores all the types and the values.(therefore for the same type there can be more than one values)
        private string type_;

        public string Type_
        {
            get { return type_; }
            set { type_ = value; }
        }
        private string val;

        public string Val
        {
            get { return val; }
            set { val = value; }
        }
    }

    public partial class uc_CustomerCreation : System.Web.UI.UserControl
    {
        private List<BusinessEntityVAL> bizEntVal_list = new List<BusinessEntityVAL>();

        public event EventHandler UserControlButtonClicked;
        private void OnUserControlButtonClick()
        {
            if (UserControlButtonClicked != null)
            {
                UserControlButtonClicked(this, EventArgs.Empty);
            }
        } 
       

        public List<BusinessEntityVAL> BizEntVal_list
        {
            get { return bizEntVal_list; }
            set { bizEntVal_list = value; }
        }

        public string CustCode
        {
            get { return txtCustCode.Text.Trim(); }
            set { txtCustCode.Text = value; }
        }
        public string CustType
        {
            get { return ddlCustType.SelectedValue; }
            set { ddlCustType.SelectedValue = value; }
        }
        public string DOB
        {
            get { return txtDateOfBirth.Text.Trim(); }
            set { txtDateOfBirth.Text = value; }
        }
        public string NIC
        {
            get { return txtNicNo.Text.Trim(); }
            set { txtNicNo.Text = value; }
        }
        public string DL
        {
            get { return txtDLno.Text.Trim(); }
            set { txtDLno.Text = value; }
        }
        public string PPNo
        {
            get { return txtPassportNo.Text.Trim(); }
            set { txtPassportNo.Text = value; }
        }
        public string BrNo
        {
            get { return txtBrNo.Text.Trim(); }
            set { txtBrNo.Text = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
           // CustomerCreationUC CUST = new CustomerCreationUC();
           // DataTable dt = new DataTable();
           // dt = CUST.GetBuizEntityTypes("C");
           // List<BusinessEntityTYPE> bindtypeList = new List<BusinessEntityTYPE>();
           // if (dt.Rows.Count > 0)
           // {
           //     foreach (DataRow r in dt.Rows)
           //     {
           //         // Get the value of the wanted column and cast it to string

           //         string TP = Convert.ToString(r["RBT_TP"]);
           //         string DESC = Convert.ToString(r["RBT_DESC"]); //rbv_val
           //         Boolean isMandetory = Convert.ToBoolean(r["RBT_MAD"]);
           //         BusinessEntityTYPE bizTP = new BusinessEntityTYPE();
           //         bizTP.IsMandatory = isMandetory;
           //         bizTP.TypeCD_ = TP;
           //         bizTP.TypeDesctipt = DESC;

           //         bindtypeList.Add(bizTP);

           //     }
           // }
           // //grvCustSegmentation.DataSource = bindtypeList;
           //// grvCustSegmentation.DataBind();

            if (!IsPostBack)
            {
                txtNicNo.Attributes.Add("onblur", "return onblurFire(event,'" + btnNIC.ClientID + "')");

                txtDLno.Attributes.Add("onblur", "return onblurFire(event,'" + btnDL.ClientID + "')");
                txtPassportNo.Attributes.Add("onblur", "return onblurFire(event,'" + btnPP.ClientID + "')");

                txtMobile.Attributes.Add("onblur", "return onblurFire(event,'" + btnPhone.ClientID + "')");
                

                txtCustCode.Attributes.Add("onkeypress", "return fun1(event,'" + btnGetbyCustCD.ClientID + "')");
                // txtEmail.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.ValidateNIC, ""));
                txtNicNo.Attributes.Add("onkeypress", "return fun1(event,'" + btnGetbyNIC.ClientID + "')");
                txtDLno.Attributes.Add("onkeypress", "return fun1(event,'" + btnGetbyDL.ClientID + "')");
                txtPassportNo.Attributes.Add("onkeypress", "return fun1(event,'" + btnGetbyPPno.ClientID + "')");
                txtBrNo.Attributes.Add("onkeypress", "return fun1(event,'" + btnGetbyBrNo.ClientID + "')");



                //divHide.Visible = false;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        { 
        //    if (txtNicNo.Text.Trim() == "" && txtPassportNo.Text.Trim() == "" && txtDLno.Text.Trim() == "")
        //    {
        //        //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "This is a cancelled receipt!");
        //        string Msg = "<script>alert('Please enter NIC or Passort or DL number!' );</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        //        return;
        //    }

        //    MasterBusinessEntity _businessEntity = new MasterBusinessEntity();

        //    //      _businessEntity.Mbe_acc_cd;
        //    //     _businessEntity.Mbe_cd;
        //    //     _businessEntity.Mbe_cre_pc;
        //    _businessEntity.Mbe_com = "ABL";
        //    //_businessEntity.Mbe_cre_by;
        //    _businessEntity.Mbe_act = true; ;
        //    _businessEntity.Mbe_add1 = txtAddresline1.Text + txtAddresline2.Text;
        //    _businessEntity.Mbe_add2 = txtCurrentAddrline1.Text + txtCurrentAddrline2.Text;

        //    _businessEntity.Mbe_email = txtEmail.Text.Trim();

        //    _businessEntity.Mbe_name = ddlTitle.SelectedValue + " " + txtFirstName.Text.Trim() + " " + txtMiddleName.Text.Trim() + " " + txtLastName.Text.Trim();
        //    _businessEntity.Mbe_nic = txtNicNo.Text.Trim();


        //    _businessEntity.Mbe_sub_tp = ddlCustSupType.SelectedValue.ToUpper();
        //    //    _businessEntity.Mbe_contact;
        //    //      _businessEntity.Mbe_country_cd =

        //    _businessEntity.Mbe_distric_cd = txtDistrict.Text.Trim();
        //    _businessEntity.Mbe_town_cd = txtTown.Text.Trim();
        //    _businessEntity.Mbe_province_cd = txtProvince.Text.Trim();

        //    //     _businessEntity.Mbe_fax;
        //    //     _businessEntity.Mbe_is_suspend;
        //    //----------vat/tax------------------------
        //    _businessEntity.Mbe_tax_no = txtVatRegNo.Text.Trim();
        //    _businessEntity.Mbe_is_tax = chkVATcustomer.Checked;
        //    _businessEntity.Mbe_tax_ex = chkVATextempted.Checked;
        //    _businessEntity.Mbe_tel = txtTelHome.Text.Trim();
        //    _businessEntity.Mbe_is_svat = chkSVATcustomer.Checked;
        //    _businessEntity.Mbe_svat_no = txtSVATno.Text.Trim();

        //    //----------current/recident-------------------------
        //    _businessEntity.Mbe_cr_add1 = txtCurrentAddrline1.Text;
        //    _businessEntity.Mbe_cr_add2 = txtCurrentAddrline2.Text;
        //    //_businessEntity.Mbe_cr_country_cd=
        //    _businessEntity.Mbe_cr_distric_cd = txtCurDistrict.Text.Trim();
        //    _businessEntity.Mbe_cr_province_cd = txtCurProvince.Text.Trim();
        //    _businessEntity.Mbe_cr_town_cd = txtCurTown.Text.Trim();
        //    _businessEntity.Mbe_cr_tel = txtTelResidence.Text.Trim();
        //    // _businessEntity.Mbe_cr_fax


        //    //--------------working--------------------------
        //    _businessEntity.Mbe_wr_add1 = txtWrkAddrLine1.Text;
        //    _businessEntity.Mbe_wr_add2 = txtWrkAddrLine2.Text;
        //    //_businessEntity.Mbe_wr_country_cd=
        //    // _businessEntity.Mbe_wr_email=
        //    _businessEntity.Mbe_wr_fax = txtWrkFax.Text.Trim();
        //    _businessEntity.Mbe_wr_tel = txtWrkPhone.Text.Trim();
        //    //_businessEntity.Mbe_wr_town_cd=
        //    //_businessEntity.Mbe_wr_province_cd=
        //    //_businessEntity.Mbe_wr_country_cd
        //    //_businessEntity.Mbe_wr_distric_cd
        //    //_businessEntity.Mbe_wr_town_cd        
        //    _businessEntity.Mbe_mob = txtMobile.Text.Trim();

        //    //     _businessEntity.Mbe_br_no;

        //    //  _businessEntity.Mbe_ho_stus;
        //    //      _businessEntity.Mbe_income_grup;
        //    //     _businessEntity.Mbe_oth_id_no;
        //    //     _businessEntity.Mbe_oth_id_tp;
        //    //      _businessEntity.Mbe_pc_stus;
        //    _businessEntity.Mbe_town_cd = txtTown.Text.Trim();
        //    _businessEntity.Mbe_tp = "C";

        //    _businessEntity.Mbe_pp_no = txtPassportNo.Text.Trim();
        //    _businessEntity.Mbe_sex = ddlSex.SelectedValue.ToUpper();
        //    _businessEntity.Mbe_cate = ddlCustType.SelectedValue.ToUpper();

        //    _businessEntity.Mbe_cre_dt = DateTime.Now.Date;

        //    _businessEntity.Mbe_dl_no = txtDLno.Text.Trim();
        //    try
        //    {
        //        _businessEntity.Mbe_dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim()).Date;
        //    }
        //    catch (Exception ex)
        //    {
        //        string Msg = "<script>alert('Date of birth is in incorrect format!' );</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        //        return;
        //    }
        //    Boolean isvalidemail = Validation();
        //    if (!isvalidemail)
        //    {
        //        string Msg = "<script>alert('Invalid email !' );</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        //        return;
        //    }

        //    //foreach (GridViewRow gvr in this.grvCustSegmentation.Rows)
        //    //{
        //    //    string isMand = Convert.ToString(gvr.Cells[1].Text.Trim());
        //    //    if (isMand.ToUpper() == "TRUE" && gvr.Cells[4].FindControl("ddlgrvTypeVal").ToString() == "")
        //    //    {
        //    //        string Msg = "<script>alert('Please select all Mandetory fields in Customer Segmentation !' );</script>";
        //    //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        //    //        return;
        //    //    }

        //    //}
        //    CustomerCreationUC CUST = new CustomerCreationUC();


        //    string custCD = CUST.SaveCustomer(_businessEntity);

        //    txtCustCode.Text = custCD;
        }
        //protected Boolean Validation()
        //{
        //    CustomerCreationUC CUST = new CustomerCreationUC();
        //    return CUST.IsValidEmail(txtEmail.Text.Trim());

        //}
        public MasterBusinessEntity GetMainCustInfor()
        {
            MasterBusinessEntity _businessEntity = new MasterBusinessEntity();
            _businessEntity.Mbe_com = "ABL";//this is changed in to the default company when saved
            _businessEntity.Mbe_act = true; ;

            _businessEntity.Mbe_name = ddlTitle.SelectedValue + " " + txtFirstName.Text.Trim().ToUpper();
            _businessEntity.Mbe_nic = txtNicNo.Text.Trim().ToUpper();
           // _businessEntity.Mbe_sub_tp = ddlCustSupType.SelectedValue.ToUpper();
   
            _businessEntity.Mbe_mob = txtMobile.Text.Trim();

            _businessEntity.Mbe_tp = "C";

            _businessEntity.Mbe_pp_no = txtPassportNo.Text.Trim().ToUpper();
            _businessEntity.Mbe_sex = ddlSex.SelectedValue.ToUpper();
            _businessEntity.Mbe_cate = ddlCustType.SelectedValue.ToUpper();

            _businessEntity.Mbe_cre_dt = DateTime.Now.Date;

            _businessEntity.Mbe_dl_no = txtDLno.Text.Trim().ToUpper();
            _businessEntity.Mbe_br_no = txtBrNo.Text.Trim().ToUpper();
            _businessEntity.Mbe_agre_send_sms = chekAgreSendSMS.Checked;
            if (txtDateOfBirth.Text.Trim()!=string.Empty)
            {
                _businessEntity.Mbe_dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim()).Date;
            }
            
            return _businessEntity;

        }
        //--------------------------------------------------------------------------
        #region Validations
        //protected void ValidateEMAIL(object sender, EventArgs e)
        //{
        //    CustomerCreationUC CUST = new CustomerCreationUC();
        //    Boolean isValidEmail= CUST.IsValidEmail(txtEmail.Text.Trim());
        //    if (isValidEmail==false)
        //    {
        //        string Msg = "<script>alert('Invalid email address!' );</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

        //    }
        //}
        protected void ValidatePP(object sender, EventArgs e)
        {
            btnGetbyPPno_Click(null, EventArgs.Empty); 
        }
        protected void ValidateDL(object sender, EventArgs e)
        {
            btnGetbyDL_Click(null, EventArgs.Empty); 
        }
       
        protected void ValidateNIC(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            Boolean isValid = CUST.IsValidNIC(txtNicNo.Text.Trim().ToUpper());
            if (isValid == false)
            {
                string Msg = "<script>alert('Invalid NIC number!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
            else
            {
                btnGetbyNIC_Click(null, EventArgs.Empty); 

                //-------------------------------
                String nic_ = txtNicNo.Text.Trim().ToUpper();
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = (nicarray[2]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    ddlSex.SelectedValue = "Female";
                    ddlTitle.SelectedValue = "Ms.";
                }
                else { ddlSex.SelectedValue = "Male";
                ddlTitle.SelectedValue = "Mr.";
                }


                //---------DOB generation----------------------
                string threechar = (nicarray[2]).ToString() + (nicarray[3]).ToString() + (nicarray[4]).ToString();
                Int32 DPBnum = Convert.ToInt32(threechar);
                if (DPBnum>500)
                {
                    DPBnum = DPBnum - 500;
                }

                

               // Int32 JAN = 1, FEF = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12;
                

                Dictionary<string, Int32> monthDict = new Dictionary<string, int>();
                monthDict.Add("JAN", 31);
                monthDict.Add("FEF", 29);
                monthDict.Add("MAR", 31);
                monthDict.Add("APR", 30);
                monthDict.Add("MAY", 31);
                monthDict.Add("JUN", 30);
                monthDict.Add("JUL", 31);
                monthDict.Add("AUG", 31);
                monthDict.Add("SEP", 30);
                monthDict.Add("OCT", 31);
                monthDict.Add("NOV", 30);
                monthDict.Add("DEC", 31);

                string bornMonth = string.Empty;
                Int32 bornDate = 0;

                Int32 leftval= DPBnum; 
                foreach (var itm in monthDict)
                {
                    bornDate = leftval;
                    
                    if (leftval<= itm.Value)
                    {
                        bornMonth = itm.Key;
                        
                        break;
                    }
                    leftval = leftval - itm.Value;
                }

                //-------------------------------
               // string bornMonth1 = bornMonth;
               // Int32 bornDate1 = bornDate;

                Dictionary<string, Int32> monthDict2 = new Dictionary<string, int>();
                monthDict2.Add("JAN", 1);
                monthDict2.Add("FEF", 2);
                monthDict2.Add("MAR", 3);
                monthDict2.Add("APR", 4);
                monthDict2.Add("MAY", 5);
                monthDict2.Add("JUN", 6);
                monthDict2.Add("JUL", 7);
                monthDict2.Add("AUG", 8);
                monthDict2.Add("SEP", 9);
                monthDict2.Add("OCT", 10);
                monthDict2.Add("NOV", 11);
                monthDict2.Add("DEC", 12);
                Int32 dobMon = 0;
                foreach (var itm in monthDict2)
                {
                    if (itm.Key == bornMonth)
                    {
                        dobMon = itm.Value;
                    }
                }
                Int32 dobYear = 1900 + Convert.ToInt32((nicarray[0].ToString())) * 10 + Convert.ToInt32((nicarray[1].ToString()));
                try
                {
                    DateTime dob = new DateTime(dobYear, dobMon, bornDate);
                    txtDateOfBirth.Text = dob.ToString("dd/MM/yyyy");
                }
                catch(Exception ex){
                
                }
               
            }
        }
        protected void ValidatePhoneNum(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            Boolean isValid = CUST.IsValidTelephone(txtMobile.Text.Trim());
            if (isValid == false)
            {
                txtMobile.Text = "";
                string Msg = "<script>alert('Invalid Phone Number!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
        }

        #endregion Validations
        //--------------------------------------------------------------------------
        protected void btnGetbyCustCD_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyCustCD(txtCustCode.Text.Trim());
            if (custProf.Mbe_cd != null)
            {
                LoadCustProf(custProf);
            }
            else//added on 01/10/2012
            {
                string custCd = txtCustCode.Text.Trim();
                MasterBusinessEntity cust_null = new MasterBusinessEntity();
                LoadCustProf(cust_null);
                txtCustCode.Text = custCd;
            }

           // SetExtraValues
        }
        protected void btnGetbyNIC_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyNIC(txtNicNo.Text.Trim().ToUpper());
            if (custProf.Mbe_cd != null)
            {
                LoadCustProf(custProf);
            }
            else//added on 01/10/2012
            {
                string nic = txtNicNo.Text.Trim().ToUpper();
                MasterBusinessEntity cust_null = new MasterBusinessEntity();
                LoadCustProf(cust_null);
                txtNicNo.Text = nic;
            }
            // SetExtraValues
        }
        protected void btnGetbyDL_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyDL(txtDLno.Text.Trim().ToUpper());
            if (custProf.Mbe_cd != null)
            {
                LoadCustProf(custProf);
            }
            else//added on 01/10/2012
            {
                string Dl = txtDLno.Text.Trim().ToUpper();
                MasterBusinessEntity cust_null = new MasterBusinessEntity();
                LoadCustProf(cust_null);
                txtDLno.Text = Dl;
            }
            // SetExtraValues
        }
        protected void btnGetbyPPno_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyPPno(txtPassportNo.Text.Trim().ToUpper());
            if (custProf.Mbe_cd != null)
            {
                LoadCustProf(custProf);
            }
            else//added on 01/10/2012
            {
                string pp = txtPassportNo.Text.Trim().ToUpper();
                MasterBusinessEntity cust_null = new MasterBusinessEntity();
                LoadCustProf(cust_null);
                txtPassportNo.Text = pp;
            }
            // SetExtraValues
        }
        protected void btnGetbyBrNo_Click(object sender, EventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyBrNo(txtBrNo.Text.Trim().ToUpper());
            if (custProf.Mbe_cd != null)
            {
                LoadCustProf(custProf);
            }
            else//added on 01/10/2012
            {
                string br = txtBrNo.Text.Trim().ToUpper();
                MasterBusinessEntity cust_null = new MasterBusinessEntity();
                LoadCustProf(cust_null);
                txtBrNo.Text = br;
            }
            // SetExtraValues
        }
        public void LoadCustProf(MasterBusinessEntity cust)
        {
            string typed_nic = txtNicNo.Text.Trim();
            string typed_ppno = txtPassportNo.Text.Trim().ToUpper();
            string typed_dl = txtDLno.Text.Trim().ToUpper();
            string typed_br = txtBrNo.Text.Trim().ToUpper();
            //------------------------------------------
           // ddlCustSupType.SelectedValue = cust.Mbe_sub_tp;
            txtNicNo.Text = cust.Mbe_nic;
            txtPassportNo.Text = cust.Mbe_pp_no;
            txtCustCode.Text = cust.Mbe_cd;
            txtDLno.Text = cust.Mbe_dl_no;
            //------------------------------------------
            //ddlTitle.SelectedValue=:TODO
            txtFirstName.Text = cust.Mbe_name;
            //if (!string.IsNullOrEmpty(cust.Mbe_cd))
            //{
            txtDateOfBirth.Text = ((DateTime)(cust.Mbe_dob.Date)).ToString("dd/MM/yyyy");// Convert.ToString(cust.Mbe_dob.Date); 
           // }
            //else if (txtDateOfBirth.Text!="")
            //{
                
            //}
                      
 
            txtBrNo.Text = cust.Mbe_br_no;
            chekAgreSendSMS.Checked = cust.Mbe_agre_send_sms;
            //------------------------------------------

            txtCustCode.Enabled = false;
            if (cust.Mbe_cd == "" || cust.Mbe_cd==null)
            {
                txtNicNo.Text = typed_nic;
                txtPassportNo.Text = typed_ppno;
                txtDLno.Text = typed_dl;
                txtBrNo.Text = typed_br;
            }

            OnUserControlButtonClick();
              
        }
        public void EnableMainButtons(bool isEnable)
        {
            if (isEnable)
            {
                divButtons.Visible = true;
            }
            else {
                divButtons.Visible = false;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}