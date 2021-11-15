using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.WebERPClient.UserControls;
using FF.BusinessObjects;
using System.Text;
using System.Data;

namespace FF.WebERPClient.Sales_Module
{
    public partial class LoyaltyMembership : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cusCreP1.EnableMainButtons(false);
            if (!IsPostBack)
            {
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd =GlbUserDefProf;
                _receiptAuto.Aut_cate_tp = GlbUserDefProf;
                _receiptAuto.Aut_start_char = "LO";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "LOTY";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = 2012;
                TextBoxLoyaltyNumber.Text = CHNLSVC.General.GetCoverNoteNo(_receiptAuto,"Loyalty");

                
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Sales_Module/LoyaltyMembership.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
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
            TextBoxCusCode.Text = custCD;

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

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {

                #region validation

                //customer code
                if (TextBoxCusCode.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Customer Code");
                    return;
                }
                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, TextBoxCusCode.Text.Trim(), string.Empty, string.Empty, "C");
                if (_masterBusinessCompany.Mbe_cd == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Customer Code");
                    return;
                }
                if (TextBoxCusCode.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Customer Code");
                    return;
                }
                if (TextBoxValidFrom.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select from date");
                    return;
                }
                if (TextBoxValidTo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select to date");
                    return;
                }
                if (Convert.ToDateTime(TextBoxValidFrom.Text) > Convert.ToDateTime(TextBoxValidTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has tobe smaller than to date");
                    return;
                }


                #endregion

                LoyaltyMemeber _loyal = new LoyaltyMemeber();
                _loyal.Salcm_app_by = GlbUserName;
                _loyal.Salcm_app_dt = DateTime.Now;
                _loyal.Salcm_bal_pt = 0;
                _loyal.Salcm_cd_ser = TextBoxCardSerial.Text;
                _loyal.Salcm_col_pt = 0;
                _loyal.Salcm_contact = TextBoxContactNo.Text;
                _loyal.Salcm_cre_by = GlbUserName;
                _loyal.Salcm_cre_dt = DateTime.Now;
                _loyal.Salcm_cus_cd = TextBoxCusCode.Text.ToUpper();
                _loyal.Salcm_dis_rt = 0;
                _loyal.Salcm_email = TextBoxEmail.Text;
                _loyal.Salcm_exp_pt = 0;
                _loyal.Salcm_loty_tp = TextBoxLoyaltyType.Text.ToUpper();
                _loyal.Salcm_red_pt = 0;
                _loyal.Salcm_val_frm = Convert.ToDateTime(TextBoxValidFrom.Text);
                _loyal.Salcm_val_to = Convert.ToDateTime(TextBoxValidTo.Text);

                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = GlbUserDefProf;
                _receiptAuto.Aut_cate_tp = GlbUserDefProf;
                _receiptAuto.Aut_start_char = "LO";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "LOTY";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = 2012;


                CHNLSVC.Sales.SaveLoyaltyMembership(_loyal, _receiptAuto);
                string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'LoyaltyMembership.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }

            else if (TabContainer1.ActiveTabIndex == 1) {

                #region validation

                
                if (TextBoxTHCardNo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Card No");
                    return;
                }
                if (TextBoxTHCusCd.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Customer Code");
                    return;
                }
                if (TextBoxNewSerial.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter New serial");
                    return;
                }

                #endregion

                LoyaltyMemeber _tem=new LoyaltyMemeber();
                _tem.Salcm_loty_tp = DropDownListLoyaltyType.SelectedValue;
                _tem.Salcm_no = TextBoxTHCardNo.Text;
                _tem.Salcm_cus_cd = TextBoxTHCusCd.Text;
                _tem.Salcm_cd_ser = TextBoxNewSerial.Text;

                LoyaltyMemeber _loyal = CHNLSVC.Sales.GetLoyaltyMember(_tem, DateTime.Now);
                if (_loyal != null)
                {

                    MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                    _receiptAuto.Aut_cate_cd = GlbUserDefProf;
                    _receiptAuto.Aut_cate_tp = GlbUserDefProf;
                    _receiptAuto.Aut_start_char = "LO";
                    _receiptAuto.Aut_direction = 0;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "LOTY";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_year = 2012;

                    _loyal.Salcm_cd_ser = TextBoxNewSerial.Text;

                    LoyaltyMemberLog _loyalLog = new LoyaltyMemberLog();
                    _loyalLog.Sacml_bal_pt = _loyal.Salcm_bal_pt;
                    //get old card serial
                    _loyalLog.Sacml_cd_ser = _tem.Salcm_cd_ser;
                    _loyalLog.Sacml_col_pt = _loyal.Salcm_col_pt;
                    _loyalLog.Sacml_cus_cd = _loyal.Salcm_cus_cd.ToUpper();
                    _loyalLog.Sacml_cus_spec = _loyal.Salcm_cus_spec.ToUpper();
                    _loyalLog.Sacml_dis_rt = _loyal.Salcm_dis_rt;
                    _loyalLog.Sacml_exp_pt = _loyal.Salcm_exp_pt;
                    _loyalLog.Sacml_loty_tp = _loyal.Salcm_loty_tp;
                    _loyalLog.Sacml_no = _loyal.Salcm_no;
                    _loyalLog.Sacml_red_pt = _loyal.Salcm_red_pt;
                    _loyalLog.Sacml_cre_by = GlbUserName;
                    _loyalLog.Sacml_cre_dt = DateTime.Now;

                    //call update method
                    bool result = CHNLSVC.Sales.UpdateLoyaltyMembership(_loyal,_receiptAuto,_loyalLog);



                    if (result)
                    {
                        string Msg = "<script>alert('Record Updated Sucessfully!!');window.location = 'LoyaltyMembership.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                    else {
                        string Msg = "<script>alert('Nothing Updated!!');window.location = 'LoyaltyMembership.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                }
                else {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Information");
                    return;
                }
            }
        }

        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxTHCusCd.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter identification.");
                TextBoxTHCusCd.Focus();
                return;
            }

            checkCustomer(TextBoxTHCusCd.Text.Trim());
        }

        private void checkCustomer(string _identification)
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            string _cusCode = "";


            if (!string.IsNullOrEmpty(TextBoxTHCusCd.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, _identification.Trim(), string.Empty, string.Empty, "C");

                if (_masterBusinessCompany.Mbe_cd != null)
                {

                    _cusCode = _masterBusinessCompany.Mbe_cd;
                }
                else{
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, _identification.Trim(), string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd != null)
                    {
                        _cusCode = _masterBusinessCompany.Mbe_cd;
                    }
                    else
                    {
                        _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, string.Empty, _identification.Trim(), "C");

                        if (_masterBusinessCompany.Mbe_cd != null)
                        {
                            _cusCode = _masterBusinessCompany.Mbe_cd;
                        }
                        else
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Customer Code");

                    }
                }
            }
            else
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Customer Code");

            LoadLoyaltyTypes(DropDownListLoyaltyType, _cusCode, null);
        }


        private void LoadLoyaltyTypes(DropDownList DropDownListLoyaltyType, string _code, string _cardNo)
        {
            DropDownListLoyaltyType.Items.Clear();
            List<LoyaltyMemeber> _list = CHNLSVC.Sales.GetLoyaltyMemberList(_code, _cardNo, DateTime.Now, null);
            DropDownListLoyaltyType.DataSource = _list;
            DropDownListLoyaltyType.DataTextField = "SALCM_LOTY_TP";
            DropDownListLoyaltyType.DataValueField = "SALCM_LOTY_TP";
            DropDownListLoyaltyType.DataBind();
            if (_list.Count <= 0) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Customer code or Card Number");
            }
        }

        protected void TextBoxTHCardNo_TextChanged(object sender, EventArgs e)
        {
            LoadLoyaltyTypes(DropDownListLoyaltyType, TextBoxTHCusCd.Text, TextBoxTHCardNo.Text);
        }

        protected void ImageButtonLoyalty_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
            DataTable dataSource = CHNLSVC.CommonSearch.GetLoyaltyTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxLoyaltyType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loyalty_Type:
                    {
                        paramsText.Append(GlbUserDefProf + seperator + DateTime.Now.ToString("MM-dd-yyyy") + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
    }
}