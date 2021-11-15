using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;
using System.Text.RegularExpressions;
using System.Text;

namespace FF.WebERPClient.General_Modules
{

    public partial class VehicalRegistration : BasePage
    {

        protected string _RecNo
        {
            get { return (string)ViewState["_RecNo"]; }
            set { ViewState["_RecNo"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                LoadCombos();
                LoadDetails();
                BindDistrict(DropDownListDistrict);
                DropDownListDistrict_SelectedIndexChanged(null, null);
                _RecNo = "";
            }
            TextBoxRMVSend.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            TextBoxRegDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            TextBoxCustomerSend.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            TextBoxJobCloseDt.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        }

        protected void BindDistrict(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            List<DistrictProvince> _district = CHNLSVC.Sales.GetDistrict("");
            _ddl.DataSource = _district.OrderBy(items => items.Mds_district);
            _ddl.DataTextField = "Mds_district";
            _ddl.DataValueField = "Mds_district";
            _ddl.DataBind();

        }

        protected void DropDownListDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DropDownListDistrict.SelectedValue.ToString())) return;
            DistrictProvince _type = CHNLSVC.Sales.GetDistrict(DropDownListDistrict.SelectedValue.ToString())[0];
            if (_type.Mds_district == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid district."); return; }
            TextBoxProvince.Text = _type.Mds_province;
        }

        public  void LoadCombos()
        {
            //load registration ddl
            DropDownListReg.Items.Clear();
            DropDownListReg.Items.Add(new ListItem("--select--", "-1"));
            DropDownListReg.DataSource = CHNLSVC.General.GetVehicalreciept(TextBoxCompany.Text, TextBoxLocation.Text, "Registration");
            DropDownListReg.DataTextField = "SVRT_REF_NO";
            DropDownListReg.DataValueField = "SVRT_REF_NO";
            DropDownListReg.DataBind();

            if (((DataTable)DropDownListReg.DataSource).Rows.Count > 0) { HiddenFieldRegCount.Value = ((DataTable)DropDownListReg.DataSource).Rows.Count.ToString(); }

            //load send RMV ddl
            DropDownListRMVRegNum.Items.Clear();
            DropDownListRMVRegNum.Items.Add(new ListItem("--select--", "-1"));
            DropDownListRMVRegNum.DataSource = CHNLSVC.General.GetVehicalreciept(TextBoxCompany.Text, TextBoxLocation.Text, "SendRMV");
            DropDownListRMVRegNum.DataTextField = "SVRT_REF_NO";
            DropDownListRMVRegNum.DataValueField = "SVRT_REF_NO";
            DropDownListRMVRegNum.DataBind();

            if (((DataTable)DropDownListRMVRegNum.DataSource).Rows.Count > 0) { HiddenFieldRMVCount.Value = ((DataTable)DropDownListRMVRegNum.DataSource).Rows.Count.ToString(); }

            //load assign reg no ddl
            DropDownListAssRegNum.Items.Clear();
            DropDownListAssRegNum.Items.Add(new ListItem("--select--", "-1"));
            DropDownListAssRegNum.DataSource = CHNLSVC.General.GetVehicalreciept(TextBoxCompany.Text, TextBoxLocation.Text, "AssignReg");
            DropDownListAssRegNum.DataTextField = "SVRT_REF_NO";
            DropDownListAssRegNum.DataValueField = "SVRT_REF_NO";
            DropDownListAssRegNum.DataBind();

            if (((DataTable)DropDownListAssRegNum.DataSource).Rows.Count > 0) { HiddenFieldAssRegCount.Value = ((DataTable)DropDownListAssRegNum.DataSource).Rows.Count.ToString(); }

            //load asend cus ddl
            DropDownListCusRegNum.Items.Clear();
            DropDownListCusRegNum.Items.Add(new ListItem("--select--", "-1"));
            DropDownListCusRegNum.DataSource = CHNLSVC.General.GetVehicalreciept(TextBoxCompany.Text, TextBoxLocation.Text, "SendCus");
            DropDownListCusRegNum.DataTextField = "SVRT_REF_NO";
            DropDownListCusRegNum.DataValueField = "SVRT_REF_NO";
            DropDownListCusRegNum.DataBind();

            if (((DataTable)DropDownListCusRegNum.DataSource).Rows.Count > 0) { HiddenFieldScusCount.Value = ((DataTable)DropDownListCusRegNum.DataSource).Rows.Count.ToString(); }


            //load job close ddl
            DropDownListJCloseRegNum.Items.Clear();
            DropDownListJCloseRegNum.Items.Add(new ListItem("--select--", "-1"));
            DropDownListJCloseRegNum.DataSource = CHNLSVC.General.GetVehicalreciept(TextBoxCompany.Text, TextBoxLocation.Text, "JobClose");
            DropDownListJCloseRegNum.DataTextField = "SVRT_REF_NO";
            DropDownListJCloseRegNum.DataValueField = "SVRT_REF_NO";
            DropDownListJCloseRegNum.DataBind();

            if (((DataTable)DropDownListJCloseRegNum.DataSource).Rows.Count > 0) { HiddenFieldClsCount.Value = ((DataTable)DropDownListJCloseRegNum.DataSource).Rows.Count.ToString(); }
        }

        //protected void RadioButtonListOption_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //    if (RadioButtonListOption.SelectedValue == "OP1")
        //    {

        //    }
        //    else if (RadioButtonListOption.SelectedValue == "OP2")
        //    {

        //    }
        //    else if (RadioButtonListOption.SelectedValue == "OP3")
        //    {

        //    }
        //    else if (RadioButtonListOption.SelectedValue == "OP4")
        //    {

               
        //    }
        //    else
        //    {

        //    }
            
        //}

        protected void LinkButtonFillApp_Click(object sender, EventArgs e)
        {
            PanelApplicationDetails.Visible = true;
            PanelAssignNumber.Visible = false;
            PanelJobClose.Visible = false;
            PanelSendCustomer.Visible = false;
            PanelSendRMV.Visible = false;
            ButtonPrint.Enabled = true;
            HiddenFieldOP.Value = "OP1";

            LoadCombos();
            LoadDetails();
        }

        protected void LinkButtonSenRMV_Click(object sender, EventArgs e)
        {
            PanelSendRMV.Visible = true;
            PanelApplicationDetails.Visible = false;
            PanelAssignNumber.Visible = false;
            PanelJobClose.Visible = false;
            PanelSendCustomer.Visible = false;
            ButtonPrint.Enabled = false;
            HiddenFieldOP.Value = "OP2";

            LoadCombos();
            LoadDetails();
        }

        protected void LinkButtonAssReg_Click(object sender, EventArgs e)
        {
            PanelAssignNumber.Visible = true;
            PanelSendRMV.Visible = false;
            PanelApplicationDetails.Visible = false;
            PanelJobClose.Visible = false;
            PanelSendCustomer.Visible = false;
            ButtonPrint.Enabled = false;
            HiddenFieldOP.Value = "OP3";

            LoadCombos();
            LoadDetails();
        }

        protected void LinkButtonSenCus_Click(object sender, EventArgs e)
        {
            PanelSendCustomer.Visible = true;
            PanelAssignNumber.Visible = false;
            PanelSendRMV.Visible = false;
            PanelApplicationDetails.Visible = false;
            PanelJobClose.Visible = false;
            ButtonPrint.Enabled = false;
            HiddenFieldOP.Value = "OP4";

            LoadCombos();
            LoadDetails();
        }

        protected void LinkButtonJobCls_Click(object sender, EventArgs e)
        {
            PanelJobClose.Visible = true;
            PanelSendCustomer.Visible = false;
            PanelAssignNumber.Visible = false;
            PanelSendRMV.Visible = false;
            PanelApplicationDetails.Visible = false;
            ButtonPrint.Enabled = false;
            HiddenFieldOP.Value = "OP5";

            LoadCombos();
            LoadDetails();
        }

        private void LoadDetails()
        {
            List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
            if (list != null)
            {
                //reciept details
                DropDownListIdType.SelectedValue = list[0].P_svrt_id_tp;
                TextBoxRecieptAmo.Text = list[0].P_svrt_reg_val.ToString();
                TextBoxClaimAmo.Text = list[0].P_svrt_claim_val.ToString();
                List<InvoiceItem> Inolist = CHNLSVC.Sales.GetInvoiceDetailByInvoice(list[0].P_svrt_inv_no);
                TextBoxSaleAmount.Text = Inolist[0].Sad_unit_amt.ToString();
                TextBoxInvoiceNo.Text = list[0].P_svrt_inv_no;
                TextBoxSType.Text = list[0].P_svrt_sales_tp;
                TextBoxRecieptDate.Text = list[0].P_svrt_dt.ToShortDateString();

                //cus details
                DropDownListCusTitle.SelectedValue = list[0].P_svrt_cust_title;
                TextBoxSType.Text = list[0].P_svrt_sales_tp;
                TextBoxIDNo.Text = list[0].P_svrt_id_ref;
                TextBoxLastName.Text = list[0].P_svrt_last_name;
                TextBoxFullName.Text = list[0].P_svrt_full_name;
                TextBoxInitials.Text = list[0].P_svrt_initial;
                TextBoxAdd1.Text = list[0].P_svrt_add01;
                TextBoxAdd2.Text = list[0].P_svrt_add02;
                TextBoxCity.Text = list[0].P_svrt_city;
                DropDownListDistrict.SelectedValue = list[0].P_svrt_district;
                TextBoxProvince.Text = list[0].P_svrt_province;
                TextBoxContact.Text = list[0].P_svrt_contact;

                //vehical details
                TextBoxCusCode.Text = list[0].P_svrt_cust_cd;
                TextBoxModel.Text = list[0].P_svrt_model;
                TextBoxBrand.Text = list[0].P_svrt_brd;
                TextBoxChassie.Text = list[0].P_svrt_chassis;
                TextBoxEngine.Text = list[0].P_svrt_engine;
                TextBoxColor.Text = list[0].P_svrt_color;
                TextBoxFeul.Text = list[0].P_svrt_fuel;
                TextBoxCapasity.Text = list[0].P_svrt_capacity.ToString();
                TextBoxUnit.Text = list[0].P_svrt_unit;
                TextBoxHP.Text = list[0].P_svrt_horse_power.ToString();
                TextBoxWheelBase.Text = list[0].P_svrt_wheel_base.ToString();
                TextBoxFroTire.Text = list[0].P_svrt_tire_front;
                TextBoxRearTire.Text = list[0].P_svrt_tire_rear;
                TextBoxWeight.Text = list[0].P_svrt_weight.ToString();
                TextBoxManf.Text = list[0].P_svrt_man_year.ToString();
                TextBoxImporter.Text = list[0].P_svrt_importer;
                TextBoxImportLie.Text = list[0].P_svrt_import;
                TextBoxAuthority.Text = list[0].P_svrt_authority;
                TextBoxCountry.Text = list[0].P_svrt_country;
                TextBoxCustomDate.Text = list[0].P_srvt_cust_dt.ToShortDateString();
                TextBoxClearenceDate.Text = list[0].P_svrt_clear_dt.ToShortDateString();
                TextBoxDeclareDate.Text = list[0].P_svrt_declear_dt.ToShortDateString();

                //importer details
                TextBoxImporter.Text = list[0].P_svrt_importer;
                TextBoxImpAdd1.Text = list[0].P_svrt_importer_add01;
                TextBoxImpAdd2.Text = list[0].P_svrt_importer_add02;

                CheckBoxRMVst.Checked = Convert.ToBoolean(list[0].P_srvt_rmv_stus);
                CheckBoxJCst.Checked = Convert.ToBoolean(list[0].P_srvt_cls_stus);
                CheckBoxSeXCusSt.Checked = Convert.ToBoolean(list[0].P_srvt_cust_stus);
                if (list[0].P_svrt_veh_reg_no != "" && list[0].P_svrt_veh_reg_no != null)
                    CheckBoxAssRegst.Checked = true;
                else
                    CheckBoxAssRegst.Checked = false;

                if (list[0].P_srvt_rmv_stus == 1)
                {
                   
                    DropDownListIdType.Enabled = false;
                    TextBoxRecieptAmo.Enabled = false;
                    TextBoxClaimAmo.Enabled = false;
                    TextBoxInvoiceNo.Enabled = false;
                    TextBoxSaleAmount.Enabled = false;
                    TextBoxSType.Enabled = false;
                    TextBoxRecieptDate.Enabled = false;

                    //cus details
                    DropDownListCusTitle.Enabled = false;
                    TextBoxSType.Enabled = false;
                    TextBoxIDNo.Enabled = false;
                    TextBoxLastName.Enabled = false;
                    TextBoxFullName.Enabled = false;
                    TextBoxInitials.Enabled = false;
                    TextBoxAdd1.Enabled = false;
                    TextBoxAdd2.Enabled = false;
                    TextBoxCity.Enabled = false;
                    DropDownListDistrict.Enabled = false;
                    TextBoxProvince.Enabled = false;
                    TextBoxContact.Enabled = false;

                    //vehical details
                    TextBoxCusCode.Enabled = false;
                    TextBoxModel.Enabled = false;
                    TextBoxBrand.Enabled = false;
                    TextBoxChassie.Enabled = false;
                    TextBoxEngine.Enabled = false;
                    TextBoxColor.Enabled = false;
                    TextBoxFeul.Enabled = false;
                    TextBoxCapasity.Enabled = false;
                    TextBoxUnit.Enabled = false;
                    TextBoxHP.Enabled = false;
                    TextBoxWheelBase.Enabled = false;
                    TextBoxFroTire.Enabled = false;
                    TextBoxRearTire.Enabled = false;
                    TextBoxWeight.Enabled = false;
                    TextBoxManf.Enabled = false;
                    TextBoxImporter.Enabled = false;
                    TextBoxImportLie.Enabled = false;
                    TextBoxAuthority.Enabled = false;
                    TextBoxCountry.Enabled = false;
                    TextBoxCustomDate.Enabled = false;
                    TextBoxClearenceDate.Enabled = false;
                    TextBoxDeclareDate.Enabled = false;

                    //importer details
                    TextBoxImporter.Enabled = false;
                    TextBoxImpAdd1.Enabled = false;
                    TextBoxImpAdd2.Enabled = false;
                }
                else {
                    DropDownListIdType.Enabled = true;


                    //cus details
                    DropDownListCusTitle.Enabled = true;
                    TextBoxSType.Enabled = true;
                    TextBoxIDNo.Enabled = true;
                    TextBoxLastName.Enabled = true;
                    TextBoxFullName.Enabled = true;
                    TextBoxInitials.Enabled = true;
                    TextBoxAdd1.Enabled = true;
                    TextBoxAdd2.Enabled = true;
                    TextBoxCity.Enabled = true;
                    DropDownListDistrict.Enabled = true;
                    TextBoxProvince.Enabled = true;
                    TextBoxContact.Enabled = true;

                    //vehical details
                    TextBoxCusCode.Enabled = true;
                    TextBoxModel.Enabled = true;
                    TextBoxBrand.Enabled = true;
                    TextBoxChassie.Enabled = false;
                    TextBoxEngine.Enabled = false;
                    TextBoxColor.Enabled = true;
                    TextBoxFeul.Enabled = true;
                    TextBoxCapasity.Enabled = true;
                    TextBoxUnit.Enabled = true;
                    TextBoxHP.Enabled = true;
                    TextBoxWheelBase.Enabled = true;
                    TextBoxFroTire.Enabled = true;
                    TextBoxRearTire.Enabled = true;
                    TextBoxWeight.Enabled = true;
                    TextBoxManf.Enabled = true;
                    TextBoxImporter.Enabled = true;
                    TextBoxImportLie.Enabled = true;
                    TextBoxAuthority.Enabled = true;
                    TextBoxCountry.Enabled = true;
                    TextBoxCustomDate.Enabled = true;
                    TextBoxClearenceDate.Enabled = true;
                    TextBoxDeclareDate.Enabled = true;

                    //importer details
                    TextBoxImporter.Enabled = true;
                    TextBoxImpAdd1.Enabled = true;
                    TextBoxImpAdd2.Enabled = true;
                }
            }
            else
            {
                DropDownListReg.SelectedValue = "-1";
                DropDownListIdType.SelectedIndex = 0;
                TextBoxRecieptAmo.Text = "";
                TextBoxClaimAmo.Text = "";
                TextBoxInvoiceNo.Text = "";
                TextBoxSType.Text = "";
                TextBoxRecieptDate.Text = "";
                TextBoxSaleAmount.Text = "";

                //cus details
                DropDownListCusTitle.SelectedIndex = 0;
                TextBoxSType.Text = "";
                TextBoxIDNo.Text = "";
                TextBoxLastName.Text = "";
                TextBoxFullName.Text = "";
                TextBoxInitials.Text = "";
                TextBoxAdd1.Text = "";
                TextBoxAdd2.Text = "";
                TextBoxCity.Text = "";
                DropDownListDistrict.SelectedIndex = 0;
                TextBoxProvince.Text = "";
                TextBoxContact.Text = "";

                //vehical details
                TextBoxCusCode.Text = "";
                TextBoxModel.Text = "";
                TextBoxBrand.Text = "";
                TextBoxChassie.Text = "";
                TextBoxEngine.Text = "";
                TextBoxColor.Text = "";
                TextBoxFeul.Text = "";
                TextBoxCapasity.Text = "";
                TextBoxUnit.Text = "";
                TextBoxHP.Text = "";
                TextBoxWheelBase.Text = "";
                TextBoxFroTire.Text = "";
                TextBoxRearTire.Text = "";
                TextBoxWeight.Text = "";
                TextBoxManf.Text = "";
                TextBoxImporter.Text = "";
                TextBoxImportLie.Text = "";
                TextBoxAuthority.Text = "";
                TextBoxCountry.Text = "";
                TextBoxCustomDate.Text = "";
                TextBoxClearenceDate.Text = "";
                TextBoxDeclareDate.Text = "";

                //importer details
                TextBoxImporter.Text = "";
                TextBoxImpAdd1.Text = "";
                TextBoxImpAdd2.Text = "";
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/VehicalRegistration.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void ImageButtonCompany_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanySearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxCompany.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonLocation_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxLocation.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void ImageButtonCusCode_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxCusCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
           
            if (PanelApplicationDetails.Visible)
            {
                List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
                if (list != null)
                {
                    int capacity;
                    int hp;
                    int wheelBase;
                    int weight;
                    int manf;
                    if (!Int32.TryParse(TextBoxCapasity.Text, out capacity))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter capacity in number");
                        return;
                    }
                    if (!Int32.TryParse(TextBoxHP.Text, out hp))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter horse power in number");
                        return;
                    }
                    if (!Int32.TryParse(TextBoxWheelBase.Text, out wheelBase))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter wheel base in number");
                        return;
                    }
                    if (!Int32.TryParse(TextBoxWeight.Text, out weight))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter weight in number");
                        return;
                    }
                    if (!Int32.TryParse(TextBoxManf.Text, out manf))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter manf. year in number");
                        return;
                    }
                    if(!IsValidNIC(TextBoxIDNo.Text)&& DropDownListIdType.SelectedValue=="NIC"){
                       MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "NIC format invalid.");
                        return;
                    }
                    list[0].P_svrt_id_tp = DropDownListIdType.SelectedValue;
                    list[0].P_svrt_id_ref = TextBoxIDNo.Text;
                    list[0].P_svrt_cust_title = DropDownListCusTitle.SelectedValue;
                    list[0].P_svrt_last_name = TextBoxLastName.Text.ToUpper();
                    list[0].P_svrt_full_name = TextBoxFullName.Text.ToUpper();
                    list[0].P_svrt_initial = TextBoxInitials.Text.ToUpper();
                    list[0].P_svrt_add01 = TextBoxAdd1.Text.ToUpper();
                    list[0].P_svrt_add02 = TextBoxAdd2.Text.ToUpper();
                    list[0].P_svrt_city = TextBoxCity.Text.ToUpper();
                    list[0].P_svrt_district = DropDownListDistrict.SelectedValue.ToUpper();
                    list[0].P_svrt_cust_cd = TextBoxCusCode.Text;
                    list[0].P_svrt_province = TextBoxProvince.Text.ToUpper();
                    list[0].P_svrt_contact = TextBoxContact.Text;
                    list[0].P_svrt_model = TextBoxModel.Text.ToUpper();
                    list[0].P_svrt_brd = TextBoxBrand.Text.ToUpper();
                    //list[0].P_svrt_chassis = TextBoxChassie.Text;
                    //list[0].P_svrt_engine = TextBoxEngine.Text;
                    list[0].P_svrt_color = TextBoxColor.Text.ToUpper();
                    list[0].P_svrt_fuel = TextBoxFeul.Text.ToUpper();
                    list[0].P_svrt_capacity = Convert.ToDecimal(TextBoxCapasity.Text);
                    list[0].P_svrt_unit = TextBoxUnit.Text.ToUpper();
                    list[0].P_svrt_horse_power = Convert.ToDecimal(TextBoxHP.Text);
                    list[0].P_svrt_wheel_base = Convert.ToDecimal(TextBoxWheelBase.Text);
                    list[0].P_svrt_tire_front = TextBoxFroTire.Text.ToUpper();
                    list[0].P_svrt_tire_rear = TextBoxRearTire.Text.ToUpper();
                    list[0].P_svrt_weight = Convert.ToDecimal(TextBoxWeight.Text);
                    list[0].P_svrt_man_year = Convert.ToDecimal(TextBoxManf.Text);
                    list[0].P_svrt_import = TextBoxImportLie.Text;
                    list[0].P_svrt_importer = TextBoxImporter.Text.ToUpper();
                    list[0].P_svrt_authority = TextBoxAuthority.Text.ToUpper();
                    list[0].P_svrt_country = TextBoxCountry.Text.ToUpper();
                    list[0].P_srvt_cust_dt = Convert.ToDateTime(TextBoxCustomDate.Text);
                    list[0].P_svrt_clear_dt = Convert.ToDateTime(TextBoxClearenceDate.Text);
                    list[0].P_svrt_declear_dt = Convert.ToDateTime(TextBoxDeclareDate.Text);
                    list[0].P_svrt_importer = TextBoxImporter.Text.ToUpper();
                    list[0].P_svrt_importer_add01 = TextBoxImpAdd1.Text.ToUpper();
                    list[0].P_svrt_importer_add02 = TextBoxImpAdd2.Text.ToUpper();
                    CHNLSVC.General.SaveVehicalRegistration(list[0]);
                    HiddenFieldRegPrint.Value = "1";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records updated sucessfully!');", true);
                    //clear controls
                    DropDownListReg.SelectedValue = "-1";
                    DropDownListIdType.SelectedIndex = 0;
                    TextBoxRecieptAmo.Text = "";
                    TextBoxClaimAmo.Text = "";
                    TextBoxInvoiceNo.Text ="";
                    TextBoxSType.Text = "";
                    TextBoxRecieptDate.Text ="";
                    TextBoxSaleAmount.Text = "";

                    //cus details
                    DropDownListCusTitle.SelectedIndex = 0;
                    TextBoxSType.Text = "";
                    TextBoxIDNo.Text = "";
                    TextBoxLastName.Text ="";
                    TextBoxFullName.Text = "";
                    TextBoxInitials.Text = "";
                    TextBoxAdd1.Text = "";
                    TextBoxAdd2.Text = "";
                    TextBoxCity.Text = "";
                    DropDownListDistrict.SelectedIndex = 0;
                    TextBoxProvince.Text = "";
                    TextBoxContact.Text = "";

                    //vehical details
                    TextBoxCusCode.Text = "";
                    TextBoxModel.Text = "";
                    TextBoxBrand.Text = "";
                    TextBoxChassie.Text = "";
                    TextBoxEngine.Text = "";
                    TextBoxColor.Text = "";
                    TextBoxFeul.Text = "";
                    TextBoxCapasity.Text = "";
                    TextBoxUnit.Text = "";
                    TextBoxHP.Text = "";
                    TextBoxWheelBase.Text = "";
                    TextBoxFroTire.Text = "";
                    TextBoxRearTire.Text = "";
                    TextBoxWeight.Text = "";
                    TextBoxManf.Text = "";
                    TextBoxImporter.Text = "";
                    TextBoxImportLie.Text = "";
                    TextBoxAuthority.Text = "";
                    TextBoxCountry.Text = "";
                    TextBoxCustomDate.Text = "";
                    TextBoxClearenceDate.Text = "";
                    TextBoxDeclareDate.Text = "";

                    //importer details
                    TextBoxImporter.Text ="";
                    TextBoxImpAdd1.Text = "";
                    TextBoxImpAdd2.Text = "";
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select valid reciept");
                    return;
                }
            }
            else if (PanelSendRMV.Visible)
            {
                List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
                if (list != null)
                {
                    //status updates
                    list[0].P_srvt_rmv_stus = 1;
                    list[0].P_srvt_rmv_by = GlbUserName;
                    list[0].P_srvt_rmv_dt = Convert.ToDateTime(TextBoxRMVSend.Text);
                    CHNLSVC.General.SaveVehicalRegistration(list[0]);
                    TextBoxRMVSend.Text = "";
                    HiddenFieldRMVPrint.Value = "1";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalRegistration.aspx';", true);
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select valid reciept");
                    return;
                }
            }
            else if (PanelAssignNumber.Visible)
            {
                List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
                if (list != null)
                {
                    //status updates
                    list[0].P_svrt_veh_reg_no = TextBoxAssRegNumber.Text.Remove(2,1);
                    list[0].P_svrt_reg_by = GlbUserName;
                    list[0].P_svrt_reg_dt = Convert.ToDateTime(TextBoxRegDate.Text);
                    if (HiddenFieldFilename.Value != "0")
                    {
                        string[] ext = (HiddenFieldFilename.Value.Split('\\')[HiddenFieldFilename.Value.Split('\\').Length-1]).Split('.');
                        list[0].P_svrt_image =DateTime.Now.ToString().Replace('/',' ').Replace(':',' ')+"." +ext[ext.Length - 1];
                        System.IO.File.Copy(HiddenFieldFilename.Value, "\\\\192.168.1.20\\Public\\Image Upload\\" + DateTime.Now.ToString().Replace('/', ' ').Replace(':', ' ') + "." + ext[ext.Length - 1]);
                    }
                    //ADDED BY SACHITH 2012/11/08
                    //UPDATE INR_SERMST

                    CHNLSVC.General.UpdateVehReg(list[0]);


                    //END

                    CHNLSVC.General.SaveVehicalRegistration(list[0]);
                    //update insurance
                    if (list[0].P_srvt_insu_ref != null && list[0].P_srvt_insu_ref != "")
                    {
                        CHNLSVC.General.UpdateInsuranceFromReg(TextBoxAssRegNumber.Text.Remove(2, 1), GlbUserName, Convert.ToDateTime(TextBoxRegDate.Text), list[0].P_srvt_insu_ref);
                    }
                    TextBoxAssRegNumber.Text = "";
                    TextBoxRegDate.Text = "";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalRegistration.aspx';", true);
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select valid reciept");
                    return;
                }
            }
            else if (PanelSendCustomer.Visible)
            {
                List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
                if (list != null)
                {
                    //status updates

                    list[0].P_srvt_cust_by = GlbUserName;
                    list[0].P_srvt_cust_dt = Convert.ToDateTime(TextBoxCustomerSend.Text);
                    list[0].Srvt_cr_pod_ref=TextBox6.Text;
                    list[0].Srvt_no_plt_dt=Convert.ToDateTime(TextBoxNumPlaCou.Text);
                    list[0].Srvt_no_plt_pod_ref=TextBoxNumPODNum.Text;
                    if(TextBoxNumPODNum.Text!="")
                        list[0].P_srvt_cust_stus = 1;


                    //job close if sales type != HP
                    if (list[0].P_svrt_sales_tp == "CS") {
                        list[0].P_srvt_cls_dt = DateTime.Now;
                        list[0].P_srvt_cls_by = GlbUserName;
                        list[0].P_srvt_cls_stus = 1;
                    }
                    CHNLSVC.General.SaveVehicalRegistration(list[0]);
                    TextBoxCustomerSend.Text = "";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalRegistration.aspx';", true);
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select valid reciept");
                    return;
                }
            }
            else
            {
                List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
                if (list != null)
                {
                    //status updates
                    list[0].P_srvt_cls_dt = Convert.ToDateTime(TextBoxJobCloseDt.Text);
                    list[0].P_srvt_cls_by = GlbUserName;
                    list[0].P_srvt_cls_stus = 1;
                    CHNLSVC.General.SaveVehicalRegistration(list[0]);

                    TextBoxJobCloseDt.Text = "";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalRegistration.aspx';", true);
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select valid reciept");
                    return;
                }
            }
        }

        protected void TextBoxCompany_TextChanged(object sender, EventArgs e)
        {
            LoadCombos();
        }

        protected void TextBoxLocation_TextChanged(object sender, EventArgs e)
        {
            LoadCombos();
        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
            if (PanelApplicationDetails.Visible)
            {
                list[0].P_svrt_prnt_stus = 1;
                list[0].P_svrt_prnt_by = GlbUserName;
                list[0].P_svrt_prnt_dt = DateTime.Now;
                CHNLSVC.General.SaveVehicalRegistration(list[0]);

                //send to report
                //TODO:REPORT
                //print method
                GlbVehRegRecNo = list[0].P_srvt_ref_no;
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\Vehicle_Reg_Form.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/Vehicle_Reg_Form.rpt";

                GlbMainPage = "~/General_Modules/VehicalRegistration.aspx";
                string Msg = "<script>window.open('../Reports_Module/Sales_Rep/VehicalRegistrationPrint.aspx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }

        protected void DropDownListReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDetails();
        }

        protected void LinkButtonTem_Click(object sender, EventArgs e)
        {
            LoadCombos();
        }

        protected void LinkButtonRMVView_Click(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.General.GetVehicalSearch(TextBoxCompany.Text, TextBoxLocation.Text, "SendRMV", TextBoxRMVVehNo.Text, TextBoxRMVChassisNo.Text, TextBoxRMVEngNo.Text, TextBoxRMVInvNo.Text, TextBoxRMVRecNo.Text,TextBoxRMVAccNo.Text);
            GridViewRMVSearch.DataSource = dt;
            GridViewRMVSearch.DataBind();
        }

        protected void LinkButtonARView_Click(object sender, EventArgs e)
        {
           

            DataTable dt = CHNLSVC.General.GetVehicalSearch(TextBoxCompany.Text, TextBoxLocation.Text, "AssignReg", TextBoxARVehNo.Text, TextBoxARChassisNo.Text, TextBoxAREngNo.Text, TextBoxARInvNo.Text, TextBoxARRecNo.Text,TextBoxARAccNo.Text);
            GridViewSRSearch.DataSource = dt;
            GridViewSRSearch.DataBind();
        }

        protected void LinkButtonView_Click(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.General.GetVehicalSearch(TextBoxCompany.Text, TextBoxLocation.Text, "Registration", TextBoxVehNo.Text, TextBoxChassisNo.Text, TextBoxEngNo.Text, TextBoxInvNo.Text, TextBoRecNo.Text,TextBoxAccNo.Text);
            GridViewSearch.DataSource = dt;
            GridViewSearch.DataBind();


        }

        protected void LinkButtonSCView_Click(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.General.GetVehicalSearch(TextBoxCompany.Text, TextBoxLocation.Text, "SendCus", TextBoxScVehNo.Text, TextBoxSCChassisNo.Text, TextBoxSCEngNo.Text, TextBoxScInvNo.Text, TextBoxSCRecNo.Text,TextBoxScAccNo.Text);
            GridViewSCSearch.DataSource = dt;
            GridViewSCSearch.DataBind();
        }

        protected void LinkButtonViewJS_Click(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.General.GetVehicalSearch(TextBoxCompany.Text, TextBoxLocation.Text, "JobClose", TextBoxJCVehNo.Text, TextBoxJCChassisNo.Text, TextBoxJCEngNo.Text, TextBoxJCInvNo.Text, TextBoxJCRecNo.Text,TextBoxJCAccNo.Text);
            GridViewJCSearch.DataSource = dt;
            GridViewJCSearch.DataBind();
        }

        protected void GridViewJCSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RecNo = GridViewJCSearch.Rows[GridViewJCSearch.SelectedIndex].Cells[1].Text;
            HiddenFieldRegCount.Value = "1";
        }

        protected void GridViewSCSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RecNo = GridViewSCSearch.Rows[GridViewSCSearch.SelectedIndex].Cells[1].Text;
            LoadCus();
            HiddenFieldRegCount.Value = "1";
        }

        protected void GridViewSRSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RecNo = GridViewSRSearch.Rows[GridViewSRSearch.SelectedIndex].Cells[1].Text;
            HiddenFieldRegCount.Value = "1";
        }


        protected void GridViewRMVSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RecNo = GridViewRMVSearch.Rows[GridViewRMVSearch.SelectedIndex].Cells[1].Text;
            HiddenFieldRegCount.Value = "1";
        }

        protected void GridViewSearch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void GridViewSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RecNo = GridViewSearch.Rows[GridViewSearch.SelectedIndex].Cells[1].Text;
            LoadDetails();
            HiddenFieldRegCount.Value = "1";
        }

        private void LoadCus() { 
         List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
         if (list != null)
         {
             TextBoxCustomerSend.Text = list[0].P_srvt_cust_dt.ToShortDateString();
             TextBox6.Text = list[0].Srvt_cr_pod_ref;
             TextBoxNumPlaCou.Text = list[0].Srvt_no_plt_dt.ToShortDateString(); ;
             TextBoxNumPODNum.Text = list[0].Srvt_no_plt_pod_ref;
         }
        
        }


    }
}