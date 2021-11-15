using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using FF.BusinessObjects;
using FF.WebERPClient.UserControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace FF.WebERPClient.HP_Module
{
    public partial class GroupSale : BasePage
    {

        protected List<GroupSaleCustomer> GroupSaleCustomerL
        {
            get { return (List<GroupSaleCustomer>)ViewState["GroupSaleCustomerL"]; }
            set { ViewState["GroupSaleCustomerL"] = value; }
        }
        protected Int32 NoOfProducts
        {
            get { return (Int32)ViewState["NoOfProducts"]; }
            set { ViewState["NoOfProducts"] = value; }
        }
        protected Int32 NoOfAccounts
        {
            get { return (Int32)ViewState["NoOfAccounts"]; }
            set { ViewState["NoOfAccounts"] = value; }
        }
        protected Int32 NoOfCustomers
        {
            get { return (Int32)ViewState["NoOfCustomers"]; }
            set { ViewState["NoOfCustomers"] = value; }
        }
        protected Decimal TotalValue
        {
            get { return (Decimal)ViewState["TotalValue"]; }
            set { ViewState["TotalValue"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                GroupSaleCustomerL = new List<GroupSaleCustomer>();
                txtCompCode.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnComp, ""));
                txtCustCode.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCustomer, ""));
                txtGroupSaleCode.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnGroupSaleCode, ""));

                txtCompCode.Attributes.Add("onkeypress", "uppercase();");
                txtCustCode.Attributes.Add("onkeypress", "uppercase();");

                IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, string.Empty, imgDate, lblDispalyInfor);

                BindEmptyCustomerTable();
            }
            //ADDED BY SACHITH 2012/08/16
            //TO SHOW POPUP AFTER VALIDATION POSTBACK
            if (HiddenFieldCusCrePopUpStats.Value == "1")
                ModalPopupExtender1.Show();

            cusCreP1.UserControlButtonClicked += new
                   EventHandler(txtHiddenCustCD_TextChanged);

        }

        protected void txtHiddenCustCD_TextChanged(object sender, EventArgs e)
        {
            //SET VALUES IN THE PAGE
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyCustCD(cusCreP1.CustCode);

            cusCreP2.SetExtraValues(custProf);


        }


        #region user defined functions
        protected void Close(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + -1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutsideParty:
                    {
                        paramsText.Append("HP" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }


                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void update_Summary(Int32 _noofprod, Int32 _noofAcc, Int32 _noofCust, Decimal _val)
        {
            Int32 _XX = (lbl_items.Text == string.Empty) ? 0 : Convert.ToInt32(lbl_items.Text);
            Int32 _X = (_XX + Convert.ToInt32(_noofprod));
            lbl_items.Text = (_X).ToString();

            Int32 _ZZ = (lbl_acc.Text == string.Empty) ? 0 : Convert.ToInt32(lbl_acc.Text);
            Int32 _Z = (_ZZ + Convert.ToInt32(_noofAcc));
            lbl_acc.Text = (_Z).ToString();

            Int32 _AA = (lbl_Cust.Text == string.Empty) ? 0 : Convert.ToInt32(lbl_Cust.Text);
            Int32 _A = (_AA + _noofCust);
            lbl_Cust.Text = (_A).ToString();

            Decimal _YY = (lbl_value.Text == string.Empty) ? 0 : Convert.ToDecimal(lbl_value.Text);
            Decimal _Y = _YY + Convert.ToDecimal(_val);
            lbl_value.Text = (_Y).ToString();

            pnl_Comp.Update();
        }

        private void calc()
        {
            if (gvCustProd.Rows.Count > 0)
            {

                for (int i = 0; i < gvCustProd.Rows.Count; i++)
                {
                    Int32 _noofprod = Convert.ToInt32(((Label)gvCustProd.Rows[i].FindControl("lblNoOfProd")).Text);
                    Int32 _noofacc = Convert.ToInt32(((Label)gvCustProd.Rows[i].FindControl("lblNoOfAcc")).Text);
                    Decimal _value = Convert.ToInt32(((Label)gvCustProd.Rows[i].FindControl("lblValue")).Text);
                    update_Summary(_noofprod, _noofacc, 1, _value);

                }
            }

        }

        private void BindGroupSaleCustomerGridData()
        {
            try
            {
                if (Convert.ToInt32(txtNoOfAcc.Text) == 0)
                {
                    throw new UIValidationException("Number of A/Cs cannot be zero");
                }
                if (Convert.ToInt32(txtNoOfProd.Text) == 0)
                {
                    throw new UIValidationException("Number of items cannot be zero");
                }
                if (Convert.ToDecimal(txtValue.Text) == 0)
                {
                    throw new UIValidationException("Value cannot be zero");
                }
                if (lblCustName.Text == "")
                {
                    throw new UIValidationException("Please select the customer");
                }
                if (txtNoOfProd.Text == "")
                {
                    throw new UIValidationException("Please enter no of products");
                }
                if (txtNoOfAcc.Text == "")
                {
                    throw new UIValidationException("Please enter no of accounts");
                }
                if (txtValue.Text == "")
                {
                    throw new UIValidationException("Please enter the value");
                }
                if (Convert.ToInt32(txtNoOfProd.Text) < Convert.ToInt32(txtNoOfAcc.Text))
                {
                    throw new UIValidationException("No of products cannot exceed no of accounts");
                }
                if (IsExistingCust(txtCustCode.Text, GroupSaleCustomerL))
                {
                    throw new UIValidationException("Customer already added.");
                }

                //summary update
                update_Summary(Convert.ToInt32(txtNoOfProd.Text), Convert.ToInt32(txtNoOfAcc.Text), 1, Convert.ToDecimal(txtValue.Text));

                GroupSaleCustomer _grpSaleCust = new GroupSaleCustomer();
                MasterBusinessCompany _masterBusComp = new MasterBusinessCompany();
                _masterBusComp.Mbe_name = lblCustName.Text;
                _grpSaleCust.MasterBusinessCompany = _masterBusComp;

                _grpSaleCust.Hgc_no_itm = Convert.ToInt32(txtNoOfProd.Text);
                _grpSaleCust.Hgc_no_acc = Convert.ToInt32(txtNoOfAcc.Text);
                _grpSaleCust.Hgc_cust_cd = txtCustCode.Text;
                _grpSaleCust.Hgc_val = Convert.ToDecimal(txtValue.Text);

                GroupSaleCustomerL.Add(_grpSaleCust);

                ClearCustomer();

                //Bind the updated list to grid.
                gvCustProd.DataSource = GroupSaleCustomerL;
                gvCustProd.DataBind();

            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }
        }

        private bool IsExistingCust(string _custCode, List<GroupSaleCustomer> _groupSaleCustList)
        {
            bool result = false;
            List<GroupSaleCustomer> _resultList = null;

            if (!string.IsNullOrEmpty(_custCode))
            {
                _resultList = _groupSaleCustList.Where(x => x.Hgc_cust_cd.Equals(_custCode)).ToList();
            }

            if (_resultList.Count > 0)
                result = true;

            return result;
        }

        protected void GetGroupSaleData(object sender, EventArgs e)
        {
            GroupSaleHeader _groupSaleHeader = new GroupSaleHeader();
            if (!string.IsNullOrEmpty(txtGroupSaleCode.Text))
            {
                _groupSaleHeader = CHNLSVC.Sales.GetGroupSaleHeaderDetails(txtGroupSaleCode.Text);

                if (_groupSaleHeader != null)
                {
                    lblGSaleDesn.Text = _groupSaleHeader.Hgr_desc;
                    txtValidFrom.Text = _groupSaleHeader.Hgr_from_dt.ToShortDateString(); ;
                    txtValidTo.Text = _groupSaleHeader.Hgr_to_dt.ToShortDateString();
                    txtVisitDate.Text = _groupSaleHeader.HGR_VISIT_DT.ToShortDateString();
                    txtFollow.Text = _groupSaleHeader.HGR_FOLLOW_UP;
                    txtContact.Text = _groupSaleHeader.Hgr_cont_cust;
                    txtContNo.Text = _groupSaleHeader.Hgr_cont_no;

                    if (_groupSaleHeader.Hgr_app_stus == true)
                    {
                        lbl_status.Text = "Approved";
                        btnApprove.Enabled = false;
                        //UpdatePanel1.Update();
                    }
                    else
                    {
                        lbl_status.Text = "Pending";
                        btnApprove.Enabled = true;
                        //UpdatePanel1.Update();
                    }

                    if (_groupSaleHeader.Hgr_tp == "HS")
                    {
                        optHire.Checked = true;
                    }
                    else if (_groupSaleHeader.Hgr_tp == "CRED")
                    {
                        optCredit.Checked = true;
                    }

                    txtCompCode.Text = _groupSaleHeader.Hgr_Grup_com;
                    GetCompanyData(null, null);

                    GroupSaleCustomerL = CHNLSVC.Sales.GetGroupSaleCustomers(txtGroupSaleCode.Text);
                    gvCustProd.DataSource = GroupSaleCustomerL;
                    gvCustProd.DataBind();

                    lbl_acc.Text = "";
                    lbl_Cust.Text = "";
                    lbl_items.Text = "";
                    lbl_value.Text = "";
                    //calc();
                    Int32 _NP = 0;
                    Int32 _NC = 0;
                    Int32 _NA = 0;
                    Decimal _TV = 0;
                    int Y = CHNLSVC.Sales.GetGroupSaleDet(txtGroupSaleCode.Text, out _NA, out _NP, out _NC, out _TV);
                    update_Summary(_NP, _NA, _NC, _TV);
                }
                else
                {
                    GroupSaleCustomerL = new List<GroupSaleCustomer>();
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid group sale code");
                    Response.Redirect("~/HP_Module/GroupSale.aspx", false);
                    //ClearCompany();
                    //ClearCustomer();
                    //ClearGroupSale();
                    txtGroupSaleCode.Text = "";
                    txtGroupSaleCode.Focus();
                    return;
                }
            }
        }

        protected void GetCustomerData(object sender, EventArgs e)
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCustCode.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCustCode.Text.Trim(), string.Empty, string.Empty, "C");


                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
                        lblCustName.Text = "";
                        lblCustAddr.Text = "";
                    }
                    else
                    {
                        txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
                        lblCustName.Text = _masterBusinessCompany.Mbe_name;
                        lblCustAddr.Text = _masterBusinessCompany.Mbe_add1 + " " + _masterBusinessCompany.Mbe_add2;
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid customer");
                    txtCustCode.Text = "";
                    lblCustName.Text = "";
                    lblCustAddr.Text = "";
                    txtCustCode.Focus();
                    return;
                }
            }

        }

        private void BindEmptyCustomerTable()
        {
            DataTable _DT = CHNLSVC.Sales.GetCustomerDetByGroupCode("");
            gvCustProd.DataSource = _DT;
            gvCustProd.DataBind();

            lbl_value.Text = "";
            lbl_items.Text = "";
            lbl_acc.Text = "";
            lbl_Cust.Text = "";
        }

        protected void GetCountryData(object sender, EventArgs e)
        {
        }

        protected void GetCompanyData(object sender, EventArgs e)
        {
            MasterOutsideParty _outsideParty = null;
            Int32 _countGrpSale = 0;
            if (txtCompCode.Text == "")
            {
                lblCompName.Text = "";
                lblCountry.Text = "";
                lblEmail.Text = "";
                lblFax.Text = "";
                lblTel.Text = "";
                lblAddr1.Text = "";
                lblAddr2.Text = "";


                return;
            }
            _outsideParty = CHNLSVC.General.GetOutsideParty(txtCompCode.Text);
            if (_outsideParty != null)
            {
                lblCompName.Text = _outsideParty.Mbi_desc;
                lblCountry.Text = "SRI LANKA";
                lblEmail.Text = _outsideParty.Mbi_email;
                lblFax.Text = _outsideParty.Mbi_fax;
                lblTel.Text = _outsideParty.Mbi_tel;
                lblAddr1.Text = _outsideParty.Mbi_add1;
                lblAddr2.Text = _outsideParty.Mbi_add2;

                _countGrpSale = CHNLSVC.Sales.GetGroupSaleCountByCompany(txtCompCode.Text);
                if (_countGrpSale > 0)
                {
                    _countGrpSale = _countGrpSale + 1;
                    lblGSaleDesn.Text = _outsideParty.Mbi_desc + " - " + _countGrpSale.ToString("000");
                }
                else
                {
                    lblGSaleDesn.Text = _outsideParty.Mbi_desc;
                }

            }
            else
            {
                lblCompName.Text = "";
                lblCountry.Text = "";
                lblEmail.Text = "";
                lblFax.Text = "";
                lblTel.Text = "";
                lblAddr1.Text = "";
                lblAddr2.Text = "";
            }

        }

        private void Save_Group_Sale()
        {
            try
            {
                if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtDate.Text, imgDate, lblDispalyInfor) == true)
                {
                    throw new UIValidationException("Back date not allow for selected date!");
                }


                string _groupSaleCode = "";

                if (lbl_status.Text == "Approved")
                {
                    throw new UIValidationException("Cannot save. Already Approved.");
                }

                if (optCredit.Checked == false && optHire.Checked == false)
                {
                    throw new UIValidationException("Please select the sale type");
                }
                if (lblCompName.Text == "")
                {
                    throw new UIValidationException("Please select the company");
                }
                if (txtVisitDate.Text == "")
                {
                    throw new UIValidationException("Please enter the visit date");
                }
                if (txtValidFrom.Text == "")
                {
                    throw new UIValidationException("Please enter the valid from date");
                }
                if (txtValidTo.Text == "")
                {
                    throw new UIValidationException("Please enter the valid to date");
                }
                if (txtFollow.Text == "")
                {
                    throw new UIValidationException("Please enter the follow up officer");
                }
                if (txtContact.Text == "")
                {
                    throw new UIValidationException("Please enter the contact person");
                }
                if (txtContNo.Text == "")
                {
                    throw new UIValidationException("Please enter the contact number");
                }

                if ((GroupSaleCustomerL == null) || (GroupSaleCustomerL.Count == 0))
                {
                    throw new UIValidationException("Please add customers to List.");
                }

                GroupSaleHeader _groupSale = new GroupSaleHeader();
                _groupSale.Hgr_grup_cd = txtGroupSaleCode.Text;
                _groupSale.Hgr_app_by = "";
                _groupSale.Hgr_app_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _groupSale.Hgr_app_stus = true;
                _groupSale.Hgr_Grup_com = txtCompCode.Text;
                _groupSale.Hgr_com = GlbUserComCode;
                _groupSale.Hgr_cont_cust = txtContact.Text;
                _groupSale.Hgr_cont_no = txtContNo.Text;
                _groupSale.Hgr_cre_by = GlbUserName;
                _groupSale.Hgr_cre_dt = Convert.ToDateTime(txtDate.Text);
                _groupSale.Hgr_desc = lblGSaleDesn.Text;
                _groupSale.Hgr_from_dt = Convert.ToDateTime(txtValidFrom.Text);
                _groupSale.Hgr_no_acc = Convert.ToInt32(lbl_acc.Text);
                _groupSale.Hgr_no_cust = Convert.ToInt32(lbl_Cust.Text);
                _groupSale.Hgr_no_itm = Convert.ToInt32(lbl_items.Text);
                _groupSale.Hgr_pc = GlbUserDefProf;
                _groupSale.Hgr_to_dt = Convert.ToDateTime(txtValidTo.Text);
                _groupSale.Hgr_tot_val = Convert.ToInt32(lbl_value.Text);
                _groupSale.Hgr_tp = (optHire.Checked == true ? "HS" : "CRED");
                _groupSale.HGR_FOLLOW_UP = txtFollow.Text;
                _groupSale.HGR_VISIT_DT = Convert.ToDateTime(txtVisitDate.Text);

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = GlbUserDefLoca;
                masterAuto.Aut_cate_tp = "GS";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "GS";
                masterAuto.Aut_start_char = "GS";
                masterAuto.Aut_year = null;

                _groupSale.GroupSaleCustomerList = GroupSaleCustomerL;

                int row_aff = CHNLSVC.Sales.SaveGroupSaleData(_groupSale, masterAuto, out _groupSaleCode);
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully updated. Group sale code is: " + _groupSaleCode);

                string Msg = "<script>alert('Successfully Saved! Document No. : " + _groupSaleCode + "');window.location = 'GroupSale.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                GroupSaleCustomerL = new List<GroupSaleCustomer>();
                //Response.Redirect("~/HP_Module/GroupSale.aspx", false);
                //ClearCompany();
                //ClearCustomer();
                //ClearGroupSale();
                //BindEmptyCustomerTable();
            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }
        }

        private void SaveCompany()
        {
            try
            {
                if (txtCompName.Text == "")
                {
                    throw new UIValidationException("Please enter company name");
                }


                MasterOutsideParty _outsideParty = new MasterOutsideParty();
                string _CompCode = "";

                _outsideParty.Mbi_country_cd = "LK";
                _outsideParty.Mbi_desc = (txtCompName.Text).ToUpper();
                _outsideParty.Mbi_tp = "HP";
                _outsideParty.Mbi_id = "0";
                //_outsideParty.Mbi_issub = "";
                _outsideParty.Mbi_add1 = txtAddr1.Text;
                _outsideParty.Mbi_add2 = txtAddr2.Text;
                _outsideParty.Mbi_tel = txtTel.Text;
                _outsideParty.Mbi_fax = txtFax.Text;
                _outsideParty.Mbi_email = txtemail.Text;
                _outsideParty.Mbi_web = "";
                _outsideParty.Mbi_town_cd = "";
                _outsideParty.Mbi_tax1 = "";
                _outsideParty.Mbi_tax2 = "";
                _outsideParty.Mbi_tax3 = "";
                _outsideParty.Mbi_act = true;
                _outsideParty.Mbi_cre_by = GlbUserName;
                _outsideParty.Mbi_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                _outsideParty.Mbi_mod_by = GlbUserName;
                _outsideParty.Mbi_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _outsideParty.Mbi_session_id = GlbUserSessionID;

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = GlbUserDefLoca;
                masterAuto.Aut_cate_tp = "COM";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "COM";
                masterAuto.Aut_start_char = "COM";
                masterAuto.Aut_year = null;

                int row_aff = CHNLSVC.General.SaveOutsideParty(_outsideParty, masterAuto, out _CompCode);

                //save company details as customer in busentity
                MasterBusinessEntity _customer = new MasterBusinessEntity();
                _customer.Mbe_com = GlbUserComCode;
                _customer.Mbe_cd = _CompCode;
                _customer.Mbe_tp = "C";
                _customer.Mbe_sub_tp = "C";
                _customer.Mbe_acc_cd = "";
                _customer.Mbe_name = (txtCompName.Text).ToUpper();
                _customer.Mbe_add1 = txtAddr1.Text;
                _customer.Mbe_add2 = txtAddr2.Text;
                _customer.Mbe_country_cd = "";
                _customer.Mbe_province_cd = "";
                _customer.Mbe_distric_cd = "";
                _customer.Mbe_town_cd = "";
                _customer.Mbe_tel = txtTel.Text;
                _customer.Mbe_fax = txtFax.Text;
                _customer.Mbe_mob = "";
                _customer.Mbe_nic = "";
                _customer.Mbe_email = txtemail.Text;
                _customer.Mbe_contact = "";
                _customer.Mbe_act = true;
                _customer.Mbe_tax_no = "";

                int effect = CHNLSVC.Sales.SaveGrpCompAsCustomer(_customer);


                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully updated. Company code is: " + _CompCode);
                txtCompCode.Text = _CompCode;
                ClearCompany();
                GetCompanyData(null, null);
            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }
        }

        protected void gvCustProd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustProd.PageIndex = e.NewPageIndex;
            gvCustProd.DataSource = GroupSaleCustomerL;
            gvCustProd.DataBind();

        }

        private void RemoveItemFromGrid(int rowIndex, int delIndex)
        {

            update_Summary(-Convert.ToInt32(((Label)gvCustProd.Rows[delIndex].FindControl("lblNoOfProd")).Text), -Convert.ToInt32(((Label)gvCustProd.Rows[delIndex].FindControl("lblNoOfAcc")).Text), -1, -Convert.ToDecimal(((Label)gvCustProd.Rows[delIndex].FindControl("lblValue")).Text));

            GroupSaleCustomerL.RemoveAt(rowIndex);

            gvCustProd.DataSource = GroupSaleCustomerL;

            gvCustProd.DataBind();
        }

        #endregion

        #region UI events
        protected void txtemail_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtemail.Text))
            {
                //lblMsg.Text = "";
                if (IsEmailAdress(txtemail.Text) == false)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid email address");
                    //lblMsg.Text = "Invalid email address";
                    //txtemail.Text = "";
                    //txtemail.Focus();
                }
            }
        }

        public static bool IsEmailAdress(string sEmail)
        {
            if (sEmail != "")
            {
                var sRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                return sRegex.IsMatch(sEmail) ? true : false;
            }
            else
            {
                return false;
            }
        }

        protected void btnClr_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/GroupSale.aspx", false);
            //ClearCompany();
            //ClearCustomer();
            //ClearGroupSale();
            //BindEmptyCustomerTable();
            //txtGroupSaleCode.Text = "";
        }

        protected void imgbtnSearchComp_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutsideParty);
            DataTable dataSource = CHNLSVC.CommonSearch.GetOutsidePartySearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCompCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgbtnSearchGrpCode_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Group_Sale);
            DataTable dataSource = CHNLSVC.CommonSearch.GetGroupSaleSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtGroupSaleCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCustCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgbtnCountry_Click(object sender, ImageClickEventArgs e)
        {
            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetCountrySearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            //MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            //MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //MasterCommonSearchUCtrl.ReturnResultControl = txtCountry.ClientID;
            //MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BindGroupSaleCustomerGridData();
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (txtGroupSaleCode.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select the group sale code");
                return;
            }

            Int16 row_aff = CHNLSVC.Sales.Approve_group_Sale(txtGroupSaleCode.Text);

            string Msg = "<script>alert('Successfully Approved!');window.location = 'GroupSale.aspx';</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            //ClearCompany();
            //ClearCustomer();
            //ClearGroupSale();
            //GetCompanyData(null, null);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveCompany();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Save_Group_Sale();
        }

        protected void gvCustProd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DELETEITEM":
                    {
                        ImageButton imgbtndelete = (ImageButton)e.CommandSource;
                        GridViewRow gvr = (GridViewRow)imgbtndelete.NamingContainer;
                        int pageindex = gvCustProd.PageIndex;
                        int pagesize = gvCustProd.PageSize;
                        int rowIndex = (pageindex * pagesize) + gvr.RowIndex;
                        int delrowIndex = gvr.RowIndex;
                        RemoveItemFromGrid(rowIndex, delrowIndex);
                        break;
                    }
            }
        }

        protected void gvCustProd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnlineNo = (HiddenField)e.Row.FindControl("hdnlineNo");
                if (hdnlineNo != null)
                {
                    int rowIndex = e.Row.RowIndex + 1;
                    hdnlineNo.Value = rowIndex.ToString();
                }
            }
        }
        #endregion

        #region clear functions
        private void ClearCompany()
        {
            txtCompName.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtTel.Text = "";
            txtFax.Text = "";
            txtemail.Text = "";
            pnl_Comp.Update();


        }

        private void ClearCustomer()
        {
            txtCustCode.Text = "";
            lblCustName.Text = "";
            lblCustAddr.Text = "";
            txtNoOfProd.Text = "";
            txtNoOfAcc.Text = "";
            txtValue.Text = "";

        }

        private void ClearGroupSale()
        {
            lblGSaleDesn.Text = "";
            txtValidFrom.Text = "";
            txtValidTo.Text = "";
            txtVisitDate.Text = "";
            txtFollow.Text = "";
            txtContact.Text = "";
            txtContNo.Text = "";

        }

        #endregion

        //ADDED BY SACHITH
        //2012/08/16
        //START

        protected void ModalPopupExtender1_Load(object sender, EventArgs e)
        {
            //set visibility of town search buttons in user control
            ImageButton img = (ImageButton)cusCreP2.FindControl("Panel_Tabs").FindControl("TabContainer").FindControl("TabPanel1").FindControl("Panel_CurrentAddress").FindControl("imgBtnSearchCurTown");
            img.Visible = false;
            ImageButton img1 = (ImageButton)cusCreP2.FindControl("Panel_Tabs").FindControl("TabContainer").FindControl("TabPanel1").FindControl("Panel_CurrentAddress").FindControl("imgBtnTownSearch");
            img1.Visible = false;

            //set visibility of buttons in user control
            cusCreP1.EnableMainButtons(false);
        }

        protected void ButtonAddCus_Click(object sender, EventArgs e)
        {
            string nicNo = ((TextBox)cusCreP1.FindControl("txtNicNo")).Text;
            string name = ((TextBox)cusCreP1.FindControl("txtFirstName")).Text;

            if (nicNo == "" || name == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Fill all Mandatory fields");
                return;
            }
            MasterBusinessEntity custPart1 = new MasterBusinessEntity();
            custPart1 = cusCreP1.GetMainCustInfor();
            //----------------------------------------------------------
            MasterBusinessEntity custPart2 = new MasterBusinessEntity();
            custPart2 = cusCreP2.GetExtraCustDet(); //uc_CustomerCreation1.GetExtraCustDet();
            //----------------------------------------------------------
            MasterBusinessEntity finalCust = new MasterBusinessEntity();
            finalCust = FinalMasterBusinessEntity(custPart1, custPart2);
            //----------------------------------------------------------
            CustomerAccountRef _account = new CustomerAccountRef();
            //_account.Saca_acc_bal 
            _account.Saca_com_cd = GlbUserComCode;
            _account.Saca_cre_by = GlbUserName;
            _account.Saca_cre_when = DateTime.Now.Date;
            // _account.Saca_cust_cd = _invoiceHeader.Sah_cus_cd;
            _account.Saca_mod_by = GlbUserName;
            _account.Saca_mod_when = DateTime.Now.Date;
            _account.Saca_ord_bal = 0;
            _account.Saca_session_id = GlbUserSessionID;

            List<MasterBusinessEntityInfo> bisInfoList = new List<MasterBusinessEntityInfo>();
            CustomerCreationUC CUST = new CustomerCreationUC();
            string custCD = CUST.SaveCustomer(finalCust, _account, bisInfoList);
            txtCustCode.Text = custCD;

            ModalPopupExtender1.Hide();
            GetCustomerData(null, null);

            HiddenFieldCusCrePopUpStats.Value = "0";
            //clear controls
            ResetFields(cusCreP1.Controls);
            ResetFields(cusCreP2.Controls);
            cusCreP1 = new uc_CustomerCreation();
            cusCreP2 = new uc_CustCreationExternalDet();
        }

        //reset fields in container(page/uc)
        public static void ResetFields(ControlCollection pageControls)
        {
            foreach (Control contl in pageControls)
            {
                var strCntName = (contl.GetType()).Name; switch (strCntName)
                {

                    case "TextBox": var txtSource = (TextBox)contl; txtSource.Text = ""; break;
                    //case "ListBox": var lstSource = (ListBox)contl; lstSource.SelectedIndex = -1; lstSource.Enabled = true; break;
                    //case "ComboBox": var cmbSource = (ComboBox)contl; cmbSource.SelectedIndex = -1; cmbSource.Enabled = true; break;
                    //case "DataGridView": var dgvSource = (DataGridView)contl; dgvSource.Rows.Clear(); break;
                    //case "CheckBox": var chkSource = (CheckBox)contl; chkSource.Checked = false; chkSource.Enabled = true; break;
                } ResetFields(contl.Controls);
            }
        }

        protected MasterBusinessEntity FinalMasterBusinessEntity(MasterBusinessEntity custPart1, MasterBusinessEntity custPart2)
        {
            MasterBusinessEntity customer = new MasterBusinessEntity();
            customer = custPart2;
            customer.Mbe_com = custPart1.Mbe_com;
            customer.Mbe_act = custPart1.Mbe_act;
            customer.Mbe_name = custPart1.Mbe_name;
            customer.Mbe_nic = custPart1.Mbe_nic;
            customer.Mbe_sub_tp = custPart1.Mbe_sub_tp;
            customer.Mbe_mob = custPart1.Mbe_mob;
            customer.Mbe_tp = custPart1.Mbe_tp;
            customer.Mbe_pp_no = custPart1.Mbe_pp_no;
            customer.Mbe_sex = custPart1.Mbe_sex;
            customer.Mbe_cate = custPart1.Mbe_cate;
            customer.Mbe_cre_dt = custPart1.Mbe_cre_dt;
            customer.Mbe_dl_no = custPart1.Mbe_dl_no;

            customer.Mbe_agre_send_sms = custPart1.Mbe_agre_send_sms;
            customer.Mbe_br_no = custPart1.Mbe_br_no;
            //customer.Mbe_ho_stus = ddlHO_status.SelectedValue;
            //customer.Mbe_pc_stus = ddlSH_status.SelectedValue;

            //--------------------------------------

            return customer;
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            cusCreP1 = new uc_CustomerCreation();

        }

        //END
    }
}