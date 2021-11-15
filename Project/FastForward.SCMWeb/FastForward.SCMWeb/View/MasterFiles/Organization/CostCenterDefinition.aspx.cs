
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;


namespace FastForward.SCMWeb.View.MasterFiles.Organization
{
    public partial class CostCenterDefinition : Base
    {
        // ucHirerachyDetails userhirachy = new ucHirerachyDetails();
        Base _basePage;
        protected void Page_Load(object sender, EventArgs e)
        {
            //   ucHirerachyDetails fg = new ucHirerachyDetails();
            if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyName"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserIP"])) &&
                    !String.IsNullOrEmpty(Convert.ToString(Session["UserComputer"])))
            {
                if (!IsPostBack)
                {
                    Session["SearchData"] = null;
                    dtOpenDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    dtHOvr.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    dtJoined.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    dtFwdSale.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    bindData();

                    dtFwdSale.Enabled = false;
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10124))
                    {
                        lbtnSaveRoleOpt.Visible = true;
                    }
                    else if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10124))
                    {
                        lbtnSaveRoleOpt.Visible = false;
                    }

                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        //clear
        private void clear()
        {
            txtPC.Text = "";
            txtEmail.Text = "";
            txtDesc.Text = "";
            txtOthRef.Text = "";
            cmbDel.SelectedIndex = -1;
            cmbType.SelectedIndex = -1;
            chkManDoc.Checked = false;
            chkHPRec.Checked = false;
            txtMaxFwdSale.Text = "0";
            dtFwdSale.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtDefDept.Text = "";
            chkMultiDept.Checked = false;
            lbtn_Srch_dep.Visible = false;
            chkSMS.Checked = false;
            txtExtWar.Text = "0";
            chkOrdRest.Checked = false;
            txtValidPrd.Text = "0";            
            chkAllowPrice.Checked = false;
            chkManCash.Checked = false;
            chkChkPay.Checked = false;
            chkPrintPay.Checked = false;
            chkPrintDisc.Checked = false;
            chkInterCom.Checked = false;
            chkPrintWarRem.Checked = false;
            // txtDefDisc.Text = "0";
            // txtEditRate.Text = "0";
            chkAct.Checked = true;
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtAddHours.Text = "";
            txtDefCustomer.Text = "CASH";

            // chk_edit_price.Checked = false;
            txtFax.Text = "";
            txtEpf.Text = "";
            txtOprTeam.Text = "";
            txtPB.Text = "";
            txtPhone.Text = "";
            txtDistrict.Text = "";
            txtProvince.Text = "";
            dtOpenDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtSquareFeet.Text = "0";
            txtManName.Text = "";
            dtJoined.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            dtHOvr.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtstaff.Text = "0";
            txtGrade.Text = "";
            txtDefLoc.Text = "";
            //txtDefExRt.Text = "0";
            chkCredBal.Checked = false;
            ucLoc1.Channel = "";
            ucLoc1.ChannelDes = "";
            ucLoc1.SubChannel = "";
            ucLoc1.Area = "";
            ucLoc1.Regien = "";
            ucLoc1.Zone = "";
            ucLoc1.Channel = "";
            ucLoc1.SubChannelDes = "";
            ucLoc1.AreaDes = "";
            ucLoc1.RegienDes = "";
            ucLoc1.ZoneDes = "";

            txtMainPC.Text = "";
            txtTown.Text = "";

            //Enable Hierachy Details  : Modified by Kelum : 2016-May-30
            ucLoc1.EnableHierachyDetails();

            //Modified by Kelum : 2016-May-30
            //Load Company Based Currency
            CompanyBasedCurrency();
        }

        //

        //load data
        private void Load_PC_Details(string _com, string _code)
        {
            MasterProfitCenter Prof = CHNLSVC.General.GetPCByPCCode(_com, _code);
            if (Prof == null)
            {
                clear();
                return;
            }
            cmbDel.SelectedIndex = Prof.Mpc_is_do_now;
            chkManDoc.Checked = Prof.Mpc_is_chk_man_doc;
            chkHPRec.Checked = Prof.Mpc_hp_sys_rec;
            txtMaxFwdSale.Text = Convert.ToString(Prof.Mpc_max_fwdsale);
            DateTime dateAndTime = Prof.Mpc_fwd_sale_st;
            string date = dateAndTime.ToString("dd/MMM/yyyy");
            if (Prof.Mpc_fwd_sale_st != Convert.ToDateTime("01/Jan/0001")) dtFwdSale.Text = date;

            //Console.WriteLine(dateAndTime.ToString("dd/MM/yyyy")); 

            //  TextBoxday.Text = Prof.Mpc_fwd_sale_st.ToString();
            txtDefDept.Text = Prof.Mpc_def_dept;
            chkMultiDept.Checked = Prof.Mpc_multi_dept;
            chkSMS.Checked = Prof.Mpc_so_sms;
            txtExtWar.Text = Convert.ToString(Prof.Mpc_wara_extend);
            chkOrdRest.Checked = Prof.Mpc_order_restric;
            txtValidPrd.Text = Prof.Mpc_order_valid_pd.ToString();
            //Load Currency Date
            LoadCurrency(Prof.Mpc_def_exrate.Trim().ToUpper());
            // txtValidPrd.Text = "90";
            chkAllowPrice.Checked = Prof.Mpc_without_price;
            chkManCash.Checked = Prof.Mpc_check_cm;
            chkChkPay.Checked = Prof.Mpc_check_pay;
            chkPrintPay.Checked = Prof.Mpc_print_payment;
            chkPrintDisc.Checked = Prof.Mpc_print_dis;
            chkInterCom.Checked = Prof.Mpc_inter_com;
            chkPrintWarRem.Checked = Prof.Mpc_print_wara_remarks;
            //  txtDefDisc.Text = Convert.ToString(Prof.Mpc_def_dis_rate);
            //txtEditRate.Text = Convert.ToString(Prof.Mpc_edit_rate);
            chkCredBal.Checked = Prof.Mpc_chk_credit;
            //txtDefExRt.Text = Prof.Mpc_def_exrate;
            txtDesc.Text = Prof.Mpc_desc;
            txtDesc.ToolTip = Prof.Mpc_desc;
            chkAct.Checked = Prof.Mpc_act;
            txtAdd1.Text = Prof.Mpc_add_1;
            txtAdd1.ToolTip = Prof.Mpc_add_1;
            txtAdd2.Text = Prof.Mpc_add_2;
            txtAdd2.ToolTip = Prof.Mpc_add_2;
            txtAddHours.Text = Prof.Mpc_add_hours.ToString();
            txtDefCustomer.Text = Prof.Mpc_def_customer;
            txtDefCustomer.ToolTip = Prof.Mpc_def_customer;
            txtPB.Text = Prof.Mpc_def_pb;
            txtPB.ToolTip = Prof.Mpc_def_pb;
            //  chk_edit_price.Checked = Prof.Mpc_edit_price;
            txtFax.Text = Prof.Mpc_fax;
            txtEpf.Text = Prof.Mpc_man;
            txtOprTeam.Text = Prof.Mpc_ope_cd;

            txtPhone.Text = Prof.Mpc_tel;
            txtDistrict.Text = Prof.MPC_DIST;
            txtProvince.Text = Prof.MPC_PROV;
            //dilshan 20/10/2017
            txtMainPC.Text = Prof.Mpc_main_pc;
            txtTown.Text = Prof.Mpc_town;

            if (Prof.Mpc_tp == "P") 
            {
                cmbType.SelectedValue = "P";
            }
            if (Prof.Mpc_tp == "C")
            {
                cmbType.SelectedValue = "C";
            }
            if (Prof.MPC_OPN_DT != Convert.ToDateTime("01/Jan/0001")) dtOpenDate.Text = Prof.MPC_OPN_DT.Date.ToString("dd/MMM/yyyy");
            txtSquareFeet.Text = Prof.MPC_SQ_FT.ToString();
            txtManName.Text = Prof.MPC_MAN_NAME;
            if (Prof.MPC_JOINED_DT != Convert.ToDateTime("01/Jan/0001")) dtJoined.Text = Prof.MPC_JOINED_DT.Date.ToString("dd/MMM/yyyy");
            if (Prof.MPC_HOVR_DT != Convert.ToDateTime("01/Jan/0001")) dtHOvr.Text = Prof.MPC_HOVR_DT.Date.ToString("dd/MMM/yyyy");
            txtstaff.Text = Prof.MPC_NO_OF_STAFF.ToString();
            txtGrade.Text = Prof.MPC_GRADE;
            txtDefLoc.Text = Prof.Mpc_def_loc;
            txtEmail.Text = Prof.Mpc_email;
            txtEmail.ToolTip = Prof.Mpc_email;
            txtOthRef.Text = Prof.Mpc_oth_ref;
            try
            {
                //ddlPcType.SelectedValue = Prof.Mpc_tp;
            }
            catch (Exception ex)
            {
                return;
            }
            string Com = Session["UserCompanyCode"].ToString();
            List<MasterSalesPriorityHierarchy> _lstPCInfor = new List<MasterSalesPriorityHierarchy>();
            _lstPCInfor = CHNLSVC.General.GetPCHeirachy(txtPC.Text.Trim().ToUpper(),Com);

            if (_lstPCInfor != null)
            {
                string _var = "";
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "CHNL").Select(X => X.Mpi_val).ToList()[0];
                if (_var != null)
                {
                    ucLoc1.Channel = _var;
                    ucLoc1.ChannelDes = GetLoc_HIRC_SearchDesc(36, _var);
                }

                _var = _lstPCInfor.Where(X => X.Mpi_cd == "SCHNL" ).Select(X => X.Mpi_val).ToList()[0];
                if (_var != null)
                {
                    ucLoc1.SubChannel = _var;
                    ucLoc1.SubChannelDes = GetLoc_HIRC_SearchDesc(37, _var);
                }
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "AREA" ).Select(X => X.Mpi_val).ToList()[0];
                if (_var != null)
                {
                    ucLoc1.Area = _var;
                    ucLoc1.AreaDes = GetLoc_HIRC_SearchDesc(38, _var);
                }
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "REGION" ).Select(X => X.Mpi_val).ToList()[0];
                if (_var != null)
                {
                    ucLoc1.Regien = _var;
                    ucLoc1.RegienDes = GetLoc_HIRC_SearchDesc(39, _var);
                }
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "ZONE").Select(X => X.Mpi_val).ToList()[0];
                if (_var != null)
                {
                    ucLoc1.Zone = _var;
                    ucLoc1.ZoneDes = GetLoc_HIRC_SearchDesc(40, _var);
                }

                //Disable Hierachy Details  : Modified by Kelum : 2016-May-27
                ucLoc1.DisableHierachyDetails();
            }



        }

        //

        public string GetLoc_HIRC_SearchDesc(int i, string _code)
        {
            if (i > 41 || i < 35)
            {
                return null;
            }
            ChannelOperator chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;

            Base _basePage = new Base();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }


            DataTable dt = chnlOpt.CommonSearch.Getloc_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
            if (dt == null)
            {
                return null;
            }
            if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                return null;
            else
                return dt.Rows[0][1].ToString();
        }
        protected void bindData()
        {
            ucLoc1.Company = Session["UserCompanyCode"].ToString();
            MasterCompany mstCom = CHNLSVC.General.GetCompByCode(ucLoc1.Company);
            ucLoc1.CompanyDes = mstCom != null ? mstCom.Mc_desc : "";
            DataTable Grpdt = CHNLSVC.General.Get_GET_GPC(mstCom.Mc_grup,null);
            string grdesc = "";
            if (Grpdt != null)
            {
                if(Grpdt.Rows.Count>0)
                {
                    grdesc = Grpdt.Rows[0]["Description"].ToString();
                }
            }
            ucLoc1.CompanyGrop = mstCom.Mc_grup;
            ucLoc1.CompanyGropDes = grdesc;
            cmbType.SelectedIndex = 0;

            //Modified by Kelum : 2016-May-25
            chkMultiDept.Checked = false;

            //Modified by Kelum : 2016-May-27
            txtPB.Text = "";

            txtDefCustomer.ToolTip = "CASH";
            txtDefCustomer.Text = "CASH";

            //Modified by Kelum : 2016-May-30
            //Load Company Based Currency
            CompanyBasedCurrency();

            // Modified by Kelum : 2016-May-30
            //keep the text of a Read only textbox after post-back
            //http://stackoverflow.com/questions/7570652/how-to-keep-the-text-of-a-read-only-textbox-after-post-back
            dtOpenDate.Attributes.Add("readonly", "readonly");

            //

            if (chkMultiDept.Checked == true)
            {
                lbtn_Srch_dep.Visible = true;
            }
            else
            {
                lbtn_Srch_dep.Visible = false;
            }
        }

        protected void lbtnSaveRoleOpt_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validateinputString(txtMainPC.Text))
                {
                    displayMessage("Invalid charactor found in main PC code.");
                    txtMainPC.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtManName.Text))
                {
                    displayMessage("Invalid charactor found in manager name.");
                    txtManName.Focus();
                    return;
                }
                if (!validateinputString(txtSquareFeet.Text))
                {
                    displayMessage("Invalid charactor found in square feet.");
                    txtSquareFeet.Focus();
                    return;
                }
                if (!validateinputString(txtPC.Text))
                {
                    displayMessage("Invalid charactor found in profit center code.");
                    txtPC.Focus();
                    return;
                }
                if (!validateinputString(txtProvince.Text))
                {
                    displayMessage("Invalid charactor found in province code.");
                    txtProvince.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtTown.Text))
                {
                    displayMessage("Invalid charactor found in town code.");
                    txtTown.Focus();
                    return;
                }
                if (!validateinputString(txtDistrict.Text))
                {
                    displayMessage("Invalid charactor found in district code.");
                    txtDistrict.Focus();
                    return;
                }
                if (!validateinputString(txtEpf.Text))
                {
                    displayMessage("Invalid charactor found in manager epf no.");
                    txtEpf.Focus();
                    return;
                }
                if (!validateinputString(txtFax.Text))
                {
                    displayMessage("Invalid charactor found in fax no.");
                    txtFax.Focus();
                    return;
                }
                if (!validateinputString(txtPhone.Text))
                {
                    displayMessage("Invalid charactor found in phone no.");
                    txtPhone.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtOthRef.Text))
                {
                    displayMessage("Invalid charactor found in other ref no.");
                    txtOthRef.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtAdd2.Text))
                {
                    displayMessage("Invalid charactor found in address line 2.");
                    txtAdd2.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtAdd1.Text))
                {
                    displayMessage("Invalid charactor found in address line 1.");
                    txtAdd1.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtDesc.Text))
                {
                    displayMessage("Invalid charactor found in profit center description.");
                    txtDesc.Focus();
                    return;
                }

                // TextBoxDescription.Text = userhirachy.Company;
                // txtDesc.Text = ucLoc1.Company;
                string errMsg = "";
                /* if (txtDesc.Text != "") 
                 {
                     displayMessage("You don't have the permission.\nPermission Code :- 10105");
                }*/


                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10105))
                {

                   // displayMessage("You don't have the permission.\nPermission Code :- 10105");
                    string msg = "You don't have the permission.\nPermission Code :- 10105";
                    DispMsg(msg,"E");
                   // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }

                if (string.IsNullOrEmpty(ucLoc1.Channel) || string.IsNullOrEmpty(ucLoc1.Channel) || string.IsNullOrEmpty(ucLoc1.Area) || string.IsNullOrEmpty(ucLoc1.Regien) || string.IsNullOrEmpty(ucLoc1.Zone))
                {

                    displayMessage("Please select the hierarchy !");
                    return;
                }

                if (string.IsNullOrEmpty(txtPC.Text))
                {

                    displayMessage("Please enter profit / cost center code !");
                    return;
                }
                if (string.IsNullOrEmpty(txtMainPC.Text))
                {

                    displayMessage("Please enter Main profit / cost center code !");
                    return;
                }

                if (chkMultiDept.Checked == true && string.IsNullOrEmpty(txtDefDept.Text))
                {

                    displayMessage("Please select the default department !");
                    return;
                }
                //Modified by kelum 
                //if (string.IsNullOrEmpty(txtDistrict.Text) || string.IsNullOrEmpty(txtProvince.Text) || string.IsNullOrEmpty(txtSquareFeet.Text) || string.IsNullOrEmpty(txtGrade.Text) || string.IsNullOrEmpty(txtEpf.Text) || string.IsNullOrEmpty(txtManName.Text))
                //{

                //    displayMessage("Please enter District/Province/Grade/Square feet/EPF/Manager name !");
                //    return;
                //}
                if (string.IsNullOrEmpty(txtDistrict.Text) || string.IsNullOrEmpty(txtProvince.Text) || string.IsNullOrEmpty(txtTown.Text))
                {

                    displayMessage("Please enter District/Province/Town !");
                    return;
                }
                //Modified by kelum 
                if (!(IsValidEmail(txtEmail.Text.Trim())))
                {
                    displayMessage("Please enter a valid email address !!!");
                    return;
                }

                if (string.IsNullOrEmpty(txtDesc.Text))
                {

                    displayMessage("Please enter description !");
                    return;
                }

                if (string.IsNullOrEmpty(txtAdd1.Text))
                {

                    displayMessage("Please enter address !");
                    return;
                }

                if (string.IsNullOrEmpty(txtAdd1.Text))
                {

                    displayMessage("Please enter address !");
                    return;
                }


                if (cmbType.SelectedValue == "0")
                {

                    displayMessage("Please select type !");
                    return;
                }

                if (string.IsNullOrEmpty(txtPhone.Text))
                {

                    displayMessage("Please enter phone # !");
                    return;
                }
                //if (string.IsNullOrEmpty(txtFax.Text))
                //{

                //    displayMessage("Please enter fax !");
                //    return;
                //}

                if (string.IsNullOrEmpty(txtEmail.Text))
                {

                    displayMessage("Please enter email !");
                    return;
                }

                if (string.IsNullOrEmpty(txtOprTeam.Text))
                {

                    displayMessage("Please enter Opr. Admin Team !");
                    return;
                }

                //if (string.IsNullOrEmpty(txtPB.Text))
                //{

                //    displayMessage("Please enter default price book !");
                //    return;
                //}
                if (string.IsNullOrEmpty(txtDefCustomer.Text))
                {

                    displayMessage("Please enter def. Customer !");
                    return;
                }

                MasterProfitCenter _pcenter = new MasterProfitCenter();
                _pcenter.Mpc_com = ucLoc1.Company;
                _pcenter.Mpc_cd = txtPC.Text.ToUpper().Trim();
                _pcenter.Mpc_desc = txtDesc.Text;
                if (cmbType.SelectedValue == "P") 
                {
                    _pcenter.Mpc_tp = "P"; 
                }
                if (cmbType.SelectedValue == "C") 
                { 
                    _pcenter.Mpc_tp = "C"; 
                }
                _pcenter.Mpc_oth_ref = txtOthRef.Text;
                _pcenter.Mpc_add_1 = txtAdd1.Text;
                _pcenter.Mpc_add_2 = txtAdd2.Text;
                _pcenter.Mpc_tel = txtPhone.Text;
                _pcenter.Mpc_fax = txtFax.Text;
                _pcenter.Mpc_act = chkAct.Checked;
                _pcenter.Mpc_chnl = ucLoc1.Channel;
                _pcenter.Mpc_ope_cd = txtOprTeam.Text;
                _pcenter.Mpc_def_pb = txtPB.Text;
                // _pcenter.Mpc_edit_price = chk_edit_price.Checked;
                _pcenter.Mpc_chk_credit = chkCredBal.Checked;
                //_pcenter.Mpc_edit_rate = Convert.ToInt32(txtEditRate.Text);
                // _pcenter.Mpc_def_dis_rate = Convert.ToInt32(txtDefDisc.Text);
                _pcenter.Mpc_print_wara_remarks = chkPrintWarRem.Checked;
                _pcenter.Mpc_inter_com = chkInterCom.Checked;
                _pcenter.Mpc_print_dis = chkPrintDisc.Checked;
                _pcenter.Mpc_print_payment = chkPrintPay.Checked;
                _pcenter.Mpc_check_pay = chkChkPay.Checked;
                _pcenter.Mpc_check_cm = chkManCash.Checked;
                _pcenter.Mpc_without_price = chkAllowPrice.Checked;
                _pcenter.Mpc_order_valid_pd = string.IsNullOrEmpty(txtValidPrd.Text) ? 0 : Convert.ToInt32(txtValidPrd.Text);
                _pcenter.Mpc_def_exrate = txtDefExRt.Text;
                _pcenter.Mpc_order_restric = chkOrdRest.Checked;
                _pcenter.Mpc_wara_extend = string.IsNullOrEmpty(txtExtWar.Text) ? 0 : Convert.ToInt32(txtExtWar.Text);
                _pcenter.Mpc_so_sms = chkSMS.Checked;
                _pcenter.Mpc_multi_dept = chkMultiDept.Checked;
                _pcenter.Mpc_def_dept = txtDefDept.Text;
                _pcenter.Mpc_def_loc = txtDefLoc.Text;
                _pcenter.Mpc_man = txtEpf.Text;
                //_pcenter.Mpc_def_exrate = txtDefExRt.Text;
                _pcenter.Mpc_def_customer = txtDefCustomer.Text;
                _pcenter.Mpc_add_hours = string.IsNullOrEmpty(txtAddHours.Text) ? 0 : Convert.ToInt32(txtAddHours.Text);
                _pcenter.Mpc_email = txtEmail.Text;
                if (chkFwdSale.Checked == true)
                {
                    _pcenter.Mpc_fwd_sale_st = Convert.ToDateTime(dtFwdSale.Text);
                }
                if (chkFwdSale.Checked == true)
                {
                    _pcenter.Mpc_fwd_sale_st = Convert.ToDateTime(null);
                }
                
                //dtFwdSale.Value.Date;
                _pcenter.Mpc_max_fwdsale = string.IsNullOrEmpty(txtMaxFwdSale.Text) ? 0 : Convert.ToInt32(txtMaxFwdSale.Text);
                _pcenter.Mpc_hp_sys_rec = chkHPRec.Checked;
                _pcenter.Mpc_is_chk_man_doc = chkManDoc.Checked;
                _pcenter.Mpc_is_do_now = cmbDel.SelectedIndex;
                _pcenter.MPC_DIST = txtDistrict.Text;
                _pcenter.MPC_PROV = txtProvince.Text;

                if (dtOpenDate.Text != "") _pcenter.MPC_OPN_DT = Convert.ToDateTime(dtOpenDate.Text); // DateTime.ParseExact(dtOpenDate.Text, "{0:d}", CultureInfo.CurrentCulture);//dtOpenDate.Text;
                _pcenter.MPC_SQ_FT = string.IsNullOrEmpty(txtSquareFeet.Text) ? 0 : Convert.ToInt32(txtSquareFeet.Text);
                _pcenter.MPC_MAN_NAME = txtManName.Text;
                if (dtJoined.Text != "") _pcenter.MPC_JOINED_DT = Convert.ToDateTime(dtJoined.Text); //DateTime.ParseExact(dtJoined.Text, "{0:d}", CultureInfo.CurrentCulture);//dtJoined.Text;
                if (dtHOvr.Text != "") _pcenter.MPC_HOVR_DT = Convert.ToDateTime(dtHOvr.Text);//DateTime.ParseExact(dtHOvr.Text, "{0:d}", CultureInfo.CurrentCulture);//dtHOvr.Value.Date;
                _pcenter.MPC_NO_OF_STAFF = string.IsNullOrEmpty(txtstaff.Text) ? 0 : Convert.ToInt32(txtstaff.Text);
                _pcenter.MPC_GRADE = txtGrade.Text;
                _pcenter.MPC_NUM_FWDSALE = 0;
                _pcenter.Mpc_main_pc = txtMainPC.Text;
                _pcenter.Mpc_town = txtTown.Text;

                //_pcenter.Mpc_

                //save employee details to databse 
                if (txtSaveconformmessageValue.Value == "Yes")
                {
                    int row_aff = CHNLSVC.General.Save_profit_center_new(_pcenter, ucLoc1.Channel, ucLoc1.SubChannel, ucLoc1.Area, ucLoc1.Regien, ucLoc1.Zone, Session["UserID"].ToString(), out errMsg);

                    //int row_aff = _basePage.CHNLSVC.General.Save_profit_center(_pcenter, Session["UserID"].ToString());
                    //  int row_aff =CHNLSVC.General.Update_profit_center(_pcenter);

                    if (row_aff != -99 && row_aff >= 0)
                    {
                        //MessageBox.Show("Successfully Updated.", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        displayMessage_Success("Successfully Updated ! Code:" + txtPC.Text.ToUpper().Trim());
                        clear();
                        txtPC.Text = "";
                    }
                    else
                    {
                        displayMessage("Process Terminated" + errMsg);
                    }
                }

                txtSaveconformmessageValue.Value = "";
            }
            catch (Exception ex)
            {

                CHNLSVC.CloseChannel();
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }







            //   Load_PC_Details(ucLoc1.Company, txtPC.Text);

            // Load_PC_Details(ucLoc1.Company, "102");


        }



        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

        }
        private void displayMessage_Success(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickySuccessToast", "showStickySuccessToast('" + msg + "');", true);

        }


        //
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(ucLoc1.Company + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(ucLoc1.Company + seperator + ucLoc1.Channel + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(ucLoc1.Company + seperator + ucLoc1.Channel + seperator + ucLoc1.SubChannel + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(ucLoc1.Company + seperator + ucLoc1.Channel + seperator + ucLoc1.SubChannel + seperator + ucLoc1.Area + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(ucLoc1.Company + seperator + ucLoc1.Channel + seperator + ucLoc1.SubChannel + seperator + ucLoc1.Area + seperator + ucLoc1.Regien + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(ucLoc1.Company + seperator + ucLoc1.Channel + seperator + ucLoc1.SubChannel + seperator + ucLoc1.Area + seperator + ucLoc1.Regien + seperator + ucLoc1.Zone + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        //break;
                        paramsText.Append(ucLoc1.Company + seperator + '%' + seperator + '%' + seperator + '%' + seperator + '%' + seperator + '%' + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Province:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.District:
                    {
                        paramsText.Append(txtProvince.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(txtDistrict.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Grade:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerAll:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdminTeam:
                    {
                        string com_cds = ucLoc1.Company + seperator + "1" + seperator;
                        paramsText.Append(com_cds);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        //

        //
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.DropDownList1.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.DropDownList1.Items.Add(col.ColumnName);
            }

            this.DropDownList1.SelectedIndex = 0;
        }

        public void BindUCtrlSeDDlData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }



        public void BindUCtrlDDLData_district(DataTable _dataSource)
        {
            this.DropDownList3.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.DropDownList3.Items.Add(col.ColumnName);
            }

            this.DropDownList3.SelectedIndex = 0;
        }


        public void BindUCtrlDDLDatas(DataTable _dataSource)
        {
            this.DropDownList2.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.DropDownList2.Items.Add(col.ColumnName);
            }

            this.DropDownList2.SelectedIndex = 0;
        }


        //



        protected void LinkButtonCode_Click(object sender, EventArgs e)
        {
            if (ucLoc1.Company == "")
            {
                displayMessage("Please select the Company");
                return;
            }

            //  CommonSearch _CommonSearch = new CommonSearch();
            string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(searchparams, null, null);
            int x = 0;
            foreach (DataRow r in _result.Rows)
            {

                if (r["Code"].ToString() == "R")
                {

                    x = 0;
                }
            }
            BLLoad.DataSource = _result;
            BLLoad.DataBind();
            BindUCtrlDDLData(_result);
            TextBox1.Text = "";
            TextBox1.Focus();
            Label1.Text = "ProfitCenterCode";
            UserBL.Show();
            //ViewState["searchdata"]=_result;
        }

        //dilshan 20/10/2017
        protected void LinkButtonMainPC_Click(object sender, EventArgs e)
        {
            if (ucLoc1.Company == "")
            {
                displayMessage("Please select the Company");
                return;
            }

            //  CommonSearch _CommonSearch = new CommonSearch();
            string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(searchparams, null, null);
            int x = 0;
            foreach (DataRow r in _result.Rows)
            {

                if (r["Code"].ToString() == "R")
                {

                    x = 0;
                }
            }
            BLLoad.DataSource = _result;
            BLLoad.DataBind();
            BindUCtrlDDLData(_result);
            TextBox1.Text = "";
            TextBox1.Focus();
            Label1.Text = "MainProfitCenterCode";
            UserBL.Show();
            //ViewState["searchdata"]=_result;
        }

        protected void BLLoad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Modified by Kelum : 2016-May-27 

            //BLLoad.PageIndex = e.NewPageIndex;
            //BLLoad.DataSource = null;
            //BLLoad.DataSource = (DataTable)ViewState["searchdata"];
            //BLLoad.DataBind();
            //UserBL.Show();

            BLLoad.PageIndex = e.NewPageIndex;

            if (Label1.Text == "ProfitCenterCode")
            {
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(searchparams, null, null);
                BLLoad.DataSource = _result;
                BLLoad.DataBind();
                BLLoad.PageIndex = 0;
                UserBL.Show();
                return;
            }

            if (Label1.Text == "MainProfitCenterCode")
            {
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(searchparams, null, null);
                BLLoad.DataSource = _result;
                BLLoad.DataBind();
                BLLoad.PageIndex = 0;
                UserBL.Show();
                return;
            }

        }

        //protected void BLLoadMPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    BLLoadMPC.PageIndex = e.NewPageIndex;

        //    if (Label1.Text == "MainProfitCenterCode")
        //    {
        //        string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
        //        DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(searchparams, null, null);
        //        BLLoadMPC.DataSource = _result;
        //        BLLoadMPC.DataBind();
        //        BLLoadMPC.PageIndex = 0;
        //        UserBLMPC.Show();
        //        return;
        //    }

        //}

        DataTable _result;
        protected void LinkButtonBLNOS_Click(object sender, EventArgs e)
        {
            _basePage = new Base();

            //Modified by Kelum: 2016-May-27

            //ViewState["result"] = null;
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, DropDownList1.SelectedValue, TextBox1.Text.ToUpper().Trim());
            //TextBox1.Text = "";
            //BLLoad.DataSource = _result;
            //ViewState["result"] = _result;
            //BLLoad.DataBind();

            //UserBL.Show();

            //}
            //else if (DropDownList1.SelectedValue == "DESCRIPTION") 
            //{
            //    ViewState["result"] = null;
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, DropDownList1.SelectedValue, TextBox1.Text.Trim());
            //    BLLoad.DataSource = _result;
            //    ViewState["result"] = _result;
            //    BLLoad.DataBind();

            //    UserBL.Show();
            //}

            if (Label1.Text == "ProfitCenterCode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, DropDownList1.SelectedValue, TextBox1.Text.ToUpper().Trim());
                BLLoad.DataSource = _result;
                BLLoad.DataBind();
                UserBL.Show();
            }

            if (Label1.Text == "MainProfitCenterCode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, DropDownList1.SelectedValue, TextBox1.Text.ToUpper().Trim());
                BLLoad.DataSource = _result;
                BLLoad.DataBind();
                UserBL.Show();
            }


        }

        protected void LinkButtonprovince_Click(object sender, EventArgs e)
        {
            //Modified by Kelum : 2016-May-30

            //string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
            //DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(searchparams, null, null);
            //GridView1.DataSource = _result;
            //GridView1.DataBind();
            //BindUCtrlDDLDatas(_result);
            //ViewState["province"] = _result;
            //TextBox2.Text = "";
            //ModalPopupExtenderProvince.Show();


            string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
            DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(searchparams, null, null);
            GridView1.DataSource = _result;
            GridView1.DataBind();
            BindUCtrlDDLDatas(_result);
            TextBox2.Text = "";
            TextBox2.Focus();
            Label2.Text = "Province";
            ModalPopupExtenderProvince.Show();

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Modified by Kelum : 2016-May-30

            //GridView1.PageIndex = e.NewPageIndex;
            //GridView1.DataSource = null;
            //GridView1.DataSource = (DataTable)ViewState["province"];
            //GridView1.DataBind();
            //ModalPopupExtenderProvince.Show();

            GridView1.PageIndex = e.NewPageIndex;

            if (Label2.Text == "Province")
            {
                string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(searchparams, null, null);
                GridView1.DataSource = _result;
                GridView1.DataBind();
                GridView1.PageIndex = 0;
                ModalPopupExtenderProvince.Show();
                return;
            }

            if (Label2.Text == "Currency")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(para, null, null);
                GridView1.DataSource = _result;
                GridView1.DataBind();
                GridView1.PageIndex = 0;
                ModalPopupExtenderProvince.Show();
                return;
            }


        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {

            _basePage = new Base();
            //if (DropDownList2.SelectedValue == "CODE")
            //{
            //    ViewState["province"] = null;
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
            //    _result =CHNLSVC.CommonSearch.GetProvinceData(SearchParams, DropDownList2.SelectedValue, TextBox2.Text.Trim());
            //    GridView1.DataSource = _result;
            //    ViewState["province"] = _result;
            //    GridView1.DataBind();

            //    ModalPopupExtenderProvince.Show();

            //}
            //else 
            //{
            //    ViewState["province"] = null;
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
            //    _result = CHNLSVC.CommonSearch.GetProvinceData(SearchParams, DropDownList2.SelectedValue, TextBox2.Text.Trim());
            //    GridView1.DataSource = _result;
            //    ViewState["province"] = _result;
            //    GridView1.DataBind();

            //    ModalPopupExtenderProvince.Show();


            //}

            if (Label2.Text == "Province")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                _result = CHNLSVC.CommonSearch.GetProvinceData(SearchParams, DropDownList2.SelectedValue, TextBox2.Text.Trim());
                GridView1.DataSource = _result;
                GridView1.DataBind();
                ModalPopupExtenderProvince.Show();
            }

            if (Label2.Text == "Currency")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                _result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, DropDownList2.SelectedValue, TextBox2.Text.Trim());
                GridView1.DataSource = _result;
                GridView1.DataBind();
                ModalPopupExtenderProvince.Show();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Label2.Text == "Province")
            {
                txtProvince.Text = GridView1.SelectedRow.Cells[1].Text;
                txtProvince.ToolTip = GridView1.SelectedRow.Cells[2].Text;
            }

            if (Label2.Text == "Currency")
            {
                txtDefExRt.Text = GridView1.SelectedRow.Cells[1].Text;
                txtDefExRt.ToolTip = GridView1.SelectedRow.Cells[2].Text;
            }

        }

        protected void BLLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Label1.Text == "ProfitCenterCode")
            {
                txtPC.Text = BLLoad.SelectedRow.Cells[1].Text.Trim();
                Load_PC_Details(ucLoc1.Company, txtPC.Text.Trim());
            }
            if (Label1.Text == "MainProfitCenterCode")
            {
                if (txtPC.Text == "")
                {
                    displayMessage("Please enter Profit Center Code !");
                    return;
                }
                else
                {

                    MasterProfitCenter Profnew = CHNLSVC.General.GetPCByPCCode(ucLoc1.Company, txtPC.Text.Trim());
                    if (Profnew == null)
                    {
                        if (txtPC.Text.Trim() == BLLoad.SelectedRow.Cells[1].Text.Trim())
                        {
                            txtMainPC.Text = BLLoad.SelectedRow.Cells[1].Text.Trim();                
                        }
                        else
                        {
                            displayMessage("Please enter the correct main profit center !");
                            return;
                        }
                    }
                    else
                    {
                        txtMainPC.Text = BLLoad.SelectedRow.Cells[1].Text.Trim();
                    }
                }
            }

        }

        //protected void BLLoadMPC_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtMainPC.Text = BLLoadMPC.SelectedRow.Cells[1].Text.Trim();

        //    //Load_PC_Details(ucLoc1.Company, txtPC.Text.Trim());

        //}

        protected void LinkButtonDistrict_Click(object sender, EventArgs e)
        {
            if (txtProvince.Text == "")
            {

                displayMessage("Please select the province");
                return;
            }

            string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
            DataTable dt = CHNLSVC.CommonSearch.GetDistrictByProvinceData(Searchparams, null, null);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            BindUCtrlDDLData_district(dt);
            ViewState["District"] = dt;
            Label3.Text = "District";
            TextBox3.Text = "";
            ModalPopupExtenderDistrict.Show();

        }

        protected void LinkButtonTown_Click(object sender, EventArgs e)
        {
            if (txtDistrict.Text == "")
            {

                displayMessage("Please select the District");
                return;
            }

            string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
            DataTable dt = CHNLSVC.CommonSearch.GetTownByDistrictData(Searchparams, null, null);
            //DataTable dt = CHNLSVC.CommonSearch.GetDistrictByProvinceData(Searchparams, null, null);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            BindUCtrlDDLData_district(dt);
            ViewState["Town"] = dt;
            Label3.Text = "Town";
            TextBox3.Text = "";
            ModalPopupExtenderDistrict.Show();

        }

        protected void LinkButtonGrade_Click(object sender, EventArgs e)
        {
            string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Grade);
            DataTable dt = CHNLSVC.CommonSearch.GetGradeByComData(Searchparams, null, null);
            //DataTable dt = CHNLSVC.CommonSearch.GetDistrictByProvinceData(Searchparams, null, null);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            BindUCtrlDDLData_district(dt);
            ViewState["Grade"] = dt;
            Label3.Text = "Grade";
            TextBox3.Text = "";
            ModalPopupExtenderDistrict.Show();

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Label3.Text == "District")
            {
                txtDistrict.Text = GridView2.SelectedRow.Cells[1].Text;
                txtDistrict.ToolTip = GridView2.SelectedRow.Cells[2].Text;
            }
            if(Label3.Text == "Town")
            {
                txtTown.Text = GridView2.SelectedRow.Cells[1].Text;
                txtTown.ToolTip = GridView2.SelectedRow.Cells[2].Text;
            }
            if (Label3.Text == "Grade")
            {
                txtGrade.Text = GridView2.SelectedRow.Cells[1].Text;
                txtGrade.ToolTip = GridView2.SelectedRow.Cells[2].Text;
            }
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataSource = null;
            if (Label3.Text == "District")
            {
                GridView2.DataSource = (DataTable)ViewState["District"];
            }
            if (Label3.Text == "Town")
            {
                GridView2.DataSource = (DataTable)ViewState["Town"];
            }
            if (Label3.Text == "Grade")
            {
                GridView2.DataSource = (DataTable)ViewState["Grade"];
            }
            GridView2.DataBind();
            ModalPopupExtenderDistrict.Show();
        }

        protected void linkClear_Click(object sender, EventArgs e)
        {
            // Response.Redirect(Request.RawUrl);
            clear();
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            if (Label3.Text == "District")
            {
                _basePage = new Base();
                if (DropDownList3.SelectedValue == "CODE")
                {

                    ViewState["District"] = null;
                    string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
                    DataTable _result = CHNLSVC.CommonSearch.GetDistrictByProvinceData(Searchparams, DropDownList3.SelectedValue, TextBox3.Text.Trim());
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, DropDownList1.SelectedValue, TextBox1.Text.Trim());
                    GridView2.DataSource = _result;
                    ViewState["District"] = _result;
                    GridView2.DataBind();

                    ModalPopupExtenderDistrict.Show();

                }
                else if (DropDownList3.SelectedValue == "DESCRIPTION")
                {
                    ViewState["District"] = null;
                    string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
                    DataTable _result = CHNLSVC.CommonSearch.GetDistrictByProvinceData(Searchparams, DropDownList3.SelectedValue, TextBox3.Text.Trim());

                    GridView2.DataSource = _result;
                    ViewState["District"] = _result;
                    GridView2.DataBind();

                    ModalPopupExtenderDistrict.Show();
                }
            }
            if (Label3.Text == "Town")
            {
                _basePage = new Base();
                if (DropDownList3.SelectedValue == "CODE")
                {

                    ViewState["Town"] = null;
                    string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                    DataTable _result = CHNLSVC.CommonSearch.GetTownByDistrictData(Searchparams, DropDownList3.SelectedValue, TextBox3.Text.Trim());
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, DropDownList1.SelectedValue, TextBox1.Text.Trim());
                    GridView2.DataSource = _result;
                    ViewState["Town"] = _result;
                    GridView2.DataBind();

                    ModalPopupExtenderDistrict.Show();

                }
                else if (DropDownList3.SelectedValue == "DESCRIPTION")
                {
                    ViewState["Town"] = null;
                    string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                    DataTable _result = CHNLSVC.CommonSearch.GetTownByDistrictData(Searchparams, DropDownList3.SelectedValue, TextBox3.Text.Trim());

                    GridView2.DataSource = _result;
                    ViewState["Town"] = _result;
                    GridView2.DataBind();

                    ModalPopupExtenderDistrict.Show();
                }
            }
            if (Label3.Text == "Grade")
            {
                _basePage = new Base();
                if (DropDownList3.SelectedValue == "CODE")
                {

                    ViewState["Grade"] = null;
                    string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Grade);
                    DataTable _result = CHNLSVC.CommonSearch.GetGradeByComData(Searchparams, DropDownList3.SelectedValue, TextBox3.Text.Trim());
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, DropDownList1.SelectedValue, TextBox1.Text.Trim());
                    GridView2.DataSource = _result;
                    ViewState["Grade"] = _result;
                    GridView2.DataBind();

                    ModalPopupExtenderDistrict.Show();

                }
                else if (DropDownList3.SelectedValue == "DESCRIPTION")
                {
                    ViewState["Grade"] = null;
                    string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Grade);
                    DataTable _result = CHNLSVC.CommonSearch.GetGradeByComData(Searchparams, DropDownList3.SelectedValue, TextBox3.Text.Trim());

                    GridView2.DataSource = _result;
                    ViewState["Grade"] = _result;
                    GridView2.DataBind();

                    ModalPopupExtenderDistrict.Show();
                }
            }

        }

        protected void chkFwdSale_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFwdSale.Checked == true)
            {
                dtFwdSale.Enabled = true;
            }
        }

        protected void txtPC_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPC.Text))
            {
                displayMessage("Invalid charactor found in profit center code.");
                txtPC.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtPC.Text))
            {
                if (string.IsNullOrEmpty(ucLoc1.Company))
                {
                    displayMessage("Please select a company !!!"); return;
                }
                MasterProfitCenter Prof = CHNLSVC.General.GetPCByPCCode(ucLoc1.Company, txtPC.Text.ToUpper().Trim());
                if (Prof != null)
                {
                    Load_PC_Details(ucLoc1.Company, txtPC.Text.ToUpper().Trim());
                }
                //Modified by Kelum : stop the clear once no record found
                //else
                //{
                //    clear();
                //}
            }
            else
            {
                clear();
            }

        }

        protected void txtMainPC_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtMainPC.Text))
            {
                displayMessage("Invalid charactor found in main PC code.");
                txtMainPC.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtPC.Text))
            {
                if (string.IsNullOrEmpty(ucLoc1.Company))
                {
                    displayMessage("Please select a company !!!"); return;
                }
                MasterProfitCenter Prof = CHNLSVC.General.GetPCByPCCode(ucLoc1.Company, txtPC.Text.ToUpper().Trim());
                if (Prof != null)
                {
                    //Load_PC_Details(ucLoc1.Company, txtPC.Text.ToUpper().Trim());
                }
                else
                {
                    if (txtPC.Text.Trim() == txtMainPC.Text.Trim())
                    {
                        //txtMainPC.Text = BLLoad.SelectedRow.Cells[1].Text.Trim();
                    }
                    else
                    {
                        if (txtPC.Text.Trim() == "")
                        {
                            txtMainPC.Text = "";
                            displayMessage("Please enter the profit center code!");
                            return;
                        }
                        else
                        {
                            txtMainPC.Text = "";
                            displayMessage("Please enter the correct main profit center !");
                            return;
                        }
                    }
                }
                //Modified by Kelum : stop the clear once no record found
                //else
                //{
                //    clear();
                //}
            }
            else
            {
                clear();
            }

        }

        protected void txtProvince_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtProvince.Text))
            {
                displayMessage("Invalid charactor found in province code.");
                txtProvince.Focus();
                return;
            }
            txtDistrict.Text = "";
            try
            {
                if (!string.IsNullOrEmpty(txtProvince.Text))
                {
                    if (txtProvince.Text != "")
                    {
                        bool b2 = false;
                        string toolTip = "";
                        string searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                        DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(searchparams, "CODE", txtProvince.Text.ToUpper());
                        foreach (DataRow row in _result.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Code"].ToString()))
                            {
                                if (txtProvince.Text.ToUpper() == row["Code"].ToString())
                                {
                                    b2 = true;
                                    toolTip = row["Description"].ToString();
                                    break;
                                }
                            }
                        }
                        if (b2)
                        {
                            txtProvince.ToolTip = toolTip;
                            txtDistrict.Focus();
                        }
                        else
                        {
                            txtProvince.ToolTip = "";
                            txtProvince.Text = "";
                            txtProvince.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid province !!!')", true);
                            return;
                        }
                    }
                    else
                    {
                        txtProvince.ToolTip = "";
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }
        }

        private void DispMsg(string msgText, string msgType)
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }

        protected void txtDistrict_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtDistrict.Text))
            {
                displayMessage("Invalid charactor found in district code.");
                txtDistrict.Focus();
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(txtProvince.Text))
                {
                    DispMsg("Please select a province!!!", "W"); txtDistrict.Text = ""; return;
                }

                if (!string.IsNullOrEmpty(txtDistrict.Text))
                {
                    if (txtDistrict.Text != "")
                    {
                        bool b2 = false;
                        string toolTip = "";
                        string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
                        DataTable dt = CHNLSVC.CommonSearch.GetDistrictByProvinceData(Searchparams, "CODE", txtDistrict.Text.ToUpper());
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Code"].ToString()))
                            {
                                if (txtDistrict.Text.ToUpper() == row["Code"].ToString())
                                {
                                    b2 = true;
                                    toolTip = row["Description"].ToString();
                                    break;
                                }
                            }
                        }
                        if (b2)
                        {
                            txtDistrict.ToolTip = toolTip;
                            txtTown.Focus();
                        }
                        else
                        {
                            txtDistrict.ToolTip = "";
                            txtDistrict.Text = "";
                            txtDistrict.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid distict')", true);
                            return;
                        }
                    }
                    else
                    {
                        txtDistrict.ToolTip = "";
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }
        }

        protected void txtTown_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDistrict.Text))
                {
                    DispMsg("Please select a district!!!", "W"); txtTown.Text = ""; return;
                }

                if (!string.IsNullOrEmpty(txtTown.Text))
                {
                    if (txtTown.Text != "")
                    {
                        bool b2 = false;
                        string toolTip = "";
                        string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                        DataTable dt = CHNLSVC.CommonSearch.GetTownByDistrictData(Searchparams, "CODE", txtTown.Text.ToUpper());
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Code"].ToString()))
                            {
                                if (txtTown.Text.ToUpper() == row["Code"].ToString())
                                {
                                    b2 = true;
                                    toolTip = row["Description"].ToString();
                                    break;
                                }
                            }
                        }
                        if (b2)
                        {
                            txtTown.ToolTip = toolTip;
                            txtOthRef.Focus();
                        }
                        else
                        {
                            txtTown.ToolTip = "";
                            txtTown.Text = "";
                            txtTown.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Town')", true);
                            return;
                        }
                    }
                    else
                    {
                        txtTown.ToolTip = "";
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }
        }

        protected void txtGrade_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtGrade.Text))
                {
                    if (txtGrade.Text != "")
                    {
                        bool b2 = false;
                        string toolTip = "";
                        string Searchparams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Grade);
                        DataTable dt = CHNLSVC.CommonSearch.GetGradeByComData(Searchparams, "CODE", txtGrade.Text.ToUpper());
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Code"].ToString()))
                            {
                                if (txtGrade.Text.ToUpper() == row["Code"].ToString())
                                {
                                    b2 = true;
                                    toolTip = row["Description"].ToString();
                                    break;
                                }
                            }
                        }
                        if (b2)
                        {
                            txtGrade.ToolTip = toolTip;
                            //txtOthRef.Focus();
                            txtMaxFwdSale.Focus();
                        }
                        else
                        {
                            txtGrade.ToolTip = "";
                            txtGrade.Text = "";
                            txtGrade.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Grade')", true);
                            return;
                        }
                    }
                    else
                    {
                        txtGrade.ToolTip = "";
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }
        }


        protected void lbtnSeCust_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Customer";
                Session["SearchData"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(para, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _result.Columns.Remove("Code1");


                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["SearchData"] = _result;
                    BindUCtrlSeDDlData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = new int[] { };
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSeAdminTm_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "AdminTeam";
                Session["SearchData"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
                DataTable _result = CHNLSVC.CommonSearch.SearchAdminTeam(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["SearchData"] = _result;
                    BindUCtrlSeDDlData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = new int[] { };
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtOprTeam_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOprTeam.Text))
                {
                    if (txtOprTeam.Text != "")
                    {
                        bool b2 = false;
                        string toolTip = "";
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
                        DataTable _result = CHNLSVC.CommonSearch.SearchAdminTeam(para, "CODE", txtOprTeam.Text.ToUpper());
                        foreach (DataRow row in _result.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Code"].ToString()))
                            {
                                if (txtOprTeam.Text.ToUpper() == row["Code"].ToString())
                                {
                                    b2 = true;
                                    toolTip = row["Description"].ToString();
                                    break;
                                }
                            }
                        }
                        if (b2)
                        {
                            txtOprTeam.ToolTip = toolTip;
                            txtDefLoc.Focus();
                        }
                        else
                        {
                            txtOprTeam.ToolTip = "";
                            txtOprTeam.Text = "";
                            txtOprTeam.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid admin team')", true);
                            return;
                        }
                    }
                    else
                    {
                        txtOprTeam.ToolTip = "";
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _result = new DataTable();

                if (lblSearchType.Text == "AdminTeam")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
                    _result = CHNLSVC.CommonSearch.SearchAdminTeam(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (lblSearchType.Text == "UserLocation")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                //else if (lblSearchType.Text == "SupplierCommon")
                //{
                //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierCommon);
                //    _result = CHNLSVC.CommonSearch.GetSupplierCommon(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                //}
                //Modified by Kelum : 2016-May-25
                else if (lblSearchType.Text == "Customer")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    _result = CHNLSVC.CommonSearch.GetCustomerGenaral(para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    _result.Columns.Remove("Code1");
                }
                else if (lblSearchType.Text == "PriceBookByCompany")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                Session["SearchData"] = _result;
                dgvResultItem.DataSource = new int[] { };
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResultItem.DataBind();
                ItemPopup.Show();
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dgvResultItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultItem.PageIndex = e.NewPageIndex;
                DataTable _result = new DataTable();
                if (Session["SearchData"] != null)
                {
                    _result = (DataTable)Session["SearchData"];
                }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                }
                else
                {
                    dgvResultItem.DataSource = new int[] { };
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                ItemPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dgvResultItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResultItem.SelectedRow.Cells[1].Text;
                if (lblSearchType.Text == "AdminTeam")
                {
                    txtOprTeam.Text = code;
                    txtOprTeam_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "UserLocation")
                {
                    txtDefLoc.Text = code;
                    txtDefLoc_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Customer")
                {
                    txtDefCustomer.Text = code;
                    txtDefCustomer_TextChanged(null, null);
                }
                //Modified by Kelum : 2016-May-25
                else if (lblSearchType.Text == "Department")
                {
                    txtDefDept.Text = code;
                    //txtDefDept_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "PriceBookByCompany")
                {
                    txtPB.Text = code;
                    //txtDefDept_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSeDefLocation_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "UserLocation";
                Session["SearchData"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["SearchData"] = _result;
                    BindUCtrlSeDDlData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = new int[] { };
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtDefLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDefLoc.Text))
                {
                    if (txtDefLoc.Text != "")
                    {
                        bool b2 = false;
                        string toolTip = "";
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                        DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(para, "CODE", txtDefLoc.Text.ToUpper());
                        foreach (DataRow row in _result.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Code"].ToString()))
                            {
                                if (txtDefLoc.Text.ToUpper() == row["Code"].ToString())
                                {
                                    b2 = true;
                                    toolTip = row["Description"].ToString();
                                    break;
                                }
                            }
                        }
                        if (b2)
                        {
                            txtDefLoc.ToolTip = toolTip;
                            cmbDel.Focus();
                        }
                        else
                        {
                            txtDefLoc.ToolTip = "";
                            txtDefLoc.Text = "";
                            txtDefLoc.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid location ')", true);
                            return;
                        }
                    }
                    else
                    {
                        txtDefLoc.ToolTip = "";
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }
        }

        protected void txtDefCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDefCustomer.Text))
                {
                    if (txtDefCustomer.Text != "")
                    {
                        bool b2 = false;
                        string toolTip = "";
                        //Modified by Kelum: 201-May-30

                        //string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierCommon);
                        //DataTable _result = CHNLSVC.CommonSearch.GetSupplierCommon(para, "CODE", txtDefCustomer.Text.ToUpper());
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                        DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(para, "CODE", txtDefCustomer.Text.ToUpper(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                        _result.Columns.Remove("Code1");
                        foreach (DataRow row in _result.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Code"].ToString()))
                            {
                                if (txtDefCustomer.Text.ToUpper() == row["Code"].ToString())
                                {
                                    b2 = true;
                                    toolTip = row["Name"].ToString();
                                    break;
                                }
                            }
                        }
                        if (b2)
                        {
                            txtDefCustomer.ToolTip = toolTip;
                            txtValidPrd.Focus();
                        }
                        else
                        {
                            txtDefCustomer.ToolTip = "CASH";
                            txtDefCustomer.Text = "CASH";
                            txtDefCustomer.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid customer ')", true);
                            return;
                        }
                    }
                    else
                    {
                        txtDefCustomer.ToolTip = "CASH";
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }
        }

        //Modified by Kelum : 2016-May-25
        protected void lbtn_Srch_dep_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Department";
                Session["SearchData"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["SearchData"] = _result;
                    BindUCtrlSeDDlData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = new int[] { };
                }

                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();

                if (dgvResultItem.PageIndex > 0)
                {
                    dgvResultItem.SetPageIndex(0);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void chkMultiDept_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMultiDept.Checked == true)
            {
                lbtn_Srch_dep.Visible = true;
            }
            else
            {
                lbtn_Srch_dep.Visible = false;
            }
        }

        protected void lbtn_Srch_PB_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "PriceBookByCompany";
                Session["SearchData"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["SearchData"] = _result;
                    BindUCtrlSeDDlData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = new int[] { };
                }

                dgvResultItem.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                ItemPopup.Show();

                if (dgvResultItem.PageIndex > 0)
                {
                    dgvResultItem.SetPageIndex(0);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dtOpenDate_TextChange(object sender, EventArgs e)
        {
            try 
            {
                Int32 _days = 0;
                _days = (Convert.ToDateTime(dtHOvr.Text).Date - Convert.ToDateTime(dtOpenDate.Text).Date).Days;
                if (_days < 0)
                {
                    string msg = "Open date cannot exceed handover date";
                    displayMessage(msg);
                    dtOpenDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    dtHOvr.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    dtOpenDate.Focus();
                    return;
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }

        }

        protected void CompanyBasedCurrency()
        {
            try
            { 
                MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_mstComp != null)
                {                    
                    if (_mstComp.Mc_cur_cd != null)
                    {                        
                        LoadCurrency(_mstComp.Mc_cur_cd.Trim().ToUpper());
                    }                    
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }

        }

        protected void lbtnSeToCurr_Click(object sender, EventArgs e)
        {
            try
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(para, null, null);

                if (_result.Rows.Count > 0)
                {
                    GridView1.DataSource = _result;
                    BindUCtrlDDLDatas(_result);
                }
                else
                {
                    GridView1.DataSource = null;
                }

                GridView1.DataBind();
                TextBox2.Text = "";
                TextBox2.Focus();
                Label2.Text = "Currency";
                ModalPopupExtenderProvince.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtDefExRt_TextChanged(object sender, EventArgs e)
        {
            LoadCurrency(txtDefExRt.Text.Trim().ToUpper());           
        }

        protected void LoadCurrency(string currencycode) 
        {
            try
            {
                if (!string.IsNullOrEmpty(currencycode))
                {                    
                  
                    DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(currencycode, null, null);
                    if (_result != null)
                    {
                        //txtDefExRt.Text = _result.;
                        DataView dv = new DataView(_result);
                        dv.RowFilter = "Code ='" + currencycode + "'";                                              

                        if (dv.Count > 0)
                        {
                            txtDefExRt.Text = dv[0]["Code"].ToString();
                            txtDefExRt.ToolTip = dv[0]["Description"].ToString();
                        }
                        else
                        {
                            CompanyBasedCurrency();
                            displayMessage("Please select a valid exchange rate !!!");
                            txtDefExRt.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing", "E");
            }
        
        }
        public bool validateinputString(string input)
        {
            var regexItem = new Regex("^[a-zA-Z0-9-/|]*$");
            if (!regexItem.IsMatch(input))
            {
                return false;   
            }
            return true;
        }
        public bool validateinputStringWithSpace(string input)
        {
            var regexItem = new Regex("^[a-zA-Z0-9-/|]*$");
            if (!(regexItem.IsMatch(input) || input.Contains(" ")))
            {
                return false;
            }
            return true;
        }

        protected void txtDesc_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtDesc.Text))
            {
                displayMessage("Invalid charactor found in profit center description.");
                txtDesc.Focus();
                return;
            }
        }

        protected void txtAdd1_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtAdd1.Text))
            {
                displayMessage("Invalid charactor found in address line 1.");
                txtAdd1.Focus();
                return;
            }
        }

        protected void txtAdd2_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtAdd2.Text))
            {
                displayMessage("Invalid charactor found in address line 2.");
                txtAdd2.Focus();
                return;
            }
        }

        protected void txtOthRef_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtOthRef.Text))
            {
                displayMessage("Invalid charactor found in other ref no.");
                txtOthRef.Focus();
                return;
            }
        }

        protected void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtPhone.Text))
            {
                displayMessage("Invalid charactor found in phone no.");
                txtPhone.Focus();
                return;
            }
        }

        protected void txtFax_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtFax.Text))
            {
                displayMessage("Invalid charactor found in fax no.");
                txtFax.Focus();
                return;
            }
        }

        protected void txtEpf_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtEpf.Text))
            {
                displayMessage("Invalid charactor found in manager epf no.");
                txtEpf.Focus();
                return;
            }
        }

        protected void txtManName_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(txtManName.Text))
            {
                displayMessage("Invalid charactor found in manager name.");
                txtManName.Focus();
                return;
            }
            //try
            //{
            //    if (!string.IsNullOrEmpty(txtManName.Text))
            //    {
            //        Regex r = new Regex("^[a-zA-Z]*$");
            //        if (r.IsMatch(txtManName.Text))
            //        {
            //            txtManName.Text = "";
            //            txtManName.Focus();
            //            displayMessage("Manager name cannot include alphanumric chars or numbers !!!");
            //            return;
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    DispMsg("Error Occurred while processing", "E");
            //}
        }

        protected void txtSquareFeet_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtSquareFeet.Text))
            {
                displayMessage("Invalid charactor found in square feet.");
                txtSquareFeet.Focus();
                return;
            }
        }
    }
}