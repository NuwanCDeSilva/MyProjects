using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

namespace FF.WebERPClient.General_Modules
{
    public partial class VehicalInsurance : BasePage
    {

        #region pay mode prop

        public Decimal PaidAmount
        {
            get { return Convert.ToDecimal(ViewState["PaidAmount"]); }
            set { ViewState["PaidAmount"] = Math.Round(value, 2); }
        }

        public Decimal BalanceAmount
        {
            get { return Convert.ToDecimal(ViewState["BalanceAmount"]); }
            set { ViewState["BalanceAmount"] = Math.Round(value, 2); }
        }
        public List<RecieptHeader> Receipt_List
        {
            get { return (List<RecieptHeader>)ViewState["Receipt_List"]; }
            set { ViewState["Receipt_List"] = value; }
        }

        public List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)ViewState["RecieptItemList"]; }
            set { ViewState["RecieptItemList"] = value; }
        }
        public List<PaymentType> PaymentTypes
        {
            get { return (List<PaymentType>)ViewState["PaymentTypes"]; }
            set { ViewState["PaymentTypes"] = value; }
        }

        public Decimal BankOrOther_Charges
        {
            get { return Convert.ToDecimal(ViewState["BankOrOther_Charges"]); }
            set { ViewState["BankOrOther_Charges"] = Math.Round(value, 2); }
        }


        public string IsValidVoucher
        {
            get { return Convert.ToString(ViewState["IsValidVoucher"]); }
            set { ViewState["IsValidVoucher"] = value; }
        }

        public Decimal EditReceiptOriginalAmt
        {
            get { return Convert.ToDecimal(ViewState["EditReceiptOriginalAmt"]); }
            set { ViewState["EditReceiptOriginalAmt"] = Math.Round(value, 2); }
        }

        public Decimal AmtToPayForFinishPayment
        {
            get { return Convert.ToDecimal(ViewState["AmtToPayForFinishPayment"]); }
            set { ViewState["AmtToPayForFinishPayment"] = Math.Round(value, 2); }
        }


        #endregion

        public DateTime AccDate {
            get { return Convert.ToDateTime(ViewState["AccDate"]); }
            set { ViewState["AccDate"] = value; }
        }

        public string VehNo
        {
            get { return (string)ViewState["VehNo"]; }
            set { ViewState["VehNo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack) {
                DropDownListInsCom.DataSource = CHNLSVC.General.GetInsuranceCompanies();
                DropDownListInsCom.DataTextField = "MBI_DESC";
                DropDownListInsCom.DataValueField = "MBI_CD";
                DropDownListInsCom.DataBind();

                DropDownListInsPol.DataSource = CHNLSVC.General.GetInsurancePolicies();
                DropDownListInsPol.DataTextField = "SVIP_POLC_DESC";
                DropDownListInsPol.DataValueField = "SVIP_POLC_CD";
                DropDownListInsPol.DataBind();

                Receipt_List = new List<RecieptHeader>();
                _recieptItem = new List<RecieptItem>();
                BalanceAmount = 0;
                PaidAmount = 0;
                BindPaymentType(ddlPayMode);

                PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "VHINS", DateTime.Now.Date);
                BankOrOther_Charges = 0;
                AmtToPayForFinishPayment = 0;
                IsValidVoucher = "N/A";

                Button1_Click(null, null);
                ButtonSDSearch_Click(null, null);
                BindDistrict(DropDownListDistrict);
                DropDownListDistrict_SelectedIndexChanged(null, null);
                AccDate = new DateTime();
                VehNo = "";
                LinkButtonIssCov_Click(null, null);
            }
            if (hdnTextStyle.Value == "1")
            {
                ChangeCss(Panel3.Controls, true);
                ChangeCss(PanelCusDe.Controls, true);
            }
            else {
                ChangeCss(Panel3.Controls, false);
                ChangeCss(PanelCusDe.Controls, false);
            }
            TextBoxCom.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButton1.ClientID + "')");
            TextBoxPC.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButton2.ClientID + "')");
            TextBoxSDCom.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonSDC.ClientID + "')");
            TextBoxSDPc.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonSDPc.ClientID + "')");
            txtPayCrBank.Attributes.Add("onKeyup", "return clickButton(event,'" + ImgBtnBankSearch.ClientID + "')");
        }

        public  void ChangeCss(ControlCollection pageControls,bool des)
        {
            foreach (Control contl in pageControls)
            {
                var strCntName = (contl.GetType()).Name; switch (strCntName)
                {
                    case "TextBox":
                        if (des)
                        {
                            var txtSource = (TextBox)contl;
                            txtSource.CssClass = "Label";
                            ImageButton3.Visible = false;       
                            break;
                        }
                        else {
                            var txtSource = (TextBox)contl;
                            txtSource.CssClass = "TextBox";
                            ImageButton3.Visible = true;
                            break;
                        }
                } ResetFields(contl.Controls);
            }
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

        protected void CheckBoxICCom_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxICCom.Checked)
                DivComSearch.Visible = true;
            else
                DivComSearch.Visible = false;
        }

        protected void CheckBoxICDate_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxICDate.Checked)
                DivDate.Visible = true;
            else
                DivDate.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string type = "";
            if (PanelCoverNoteDetails.Visible)
                type = "Cover";
            if (PanelExtendCoverNote.Visible)
                type = "Extend";
            if (PanelIssueCretificate.Visible)
                type = "Cer";



            DataTable datasource = new DataTable();
            //recent checked
            if (CheckBoxICMrec.Checked)
            {
                //search com checked
                if (CheckBoxICCom.Checked)
                {
                    

                        //has date has com
                    if (CheckBoxICDate.Checked)
                    {


                        try
                        {
                            DateTime _from = Convert.ToDateTime(TextBoxDFrom.Text);
                            DateTime _to = Convert.ToDateTime(TextBoxDTo.Text);

                            if (_from > _to)
                            { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From has to be smaler than to date"); }

                            datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, _from, _to, "Recent" + type, null, TextBoxAcc.Text.ToUpper(),"","","");
                            if (CheckBoxAcc.Checked)
                            {
                                datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, _from, _to, "Recent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                            }
                        }
                        catch (Exception)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From and To has to be dates");
                        }

                    }

                //no date has com
                    else
                    {
                        datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, DateTime.MinValue, DateTime.MaxValue, "Recent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                    }
                }
                //no com
                else
                {
                    //no com has date
                    if (CheckBoxICDate.Checked)
                    {
                        
                            try
                            {
                                DateTime _from = Convert.ToDateTime(TextBoxDFrom.Text);
                                DateTime _to = Convert.ToDateTime(TextBoxDTo.Text);

                                if (_from > _to)
                                { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From has to be smaler than to date"); }

                                datasource = CHNLSVC.General.GetInsuranceInvoice("", "", _from, _to, "Recent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                                if (CheckBoxAcc.Checked)
                                {
                                    datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, _from, _to, "Recent" + type, null, TextBoxAcc.Text, "", "", "");
                                }
                            }
                            catch (Exception)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From and To has to be dates");
                            }
                        
                    }
                    //no com no date
                    else
                    {
                        datasource = CHNLSVC.General.GetInsuranceInvoice("", "", DateTime.MinValue, DateTime.MaxValue, "Recent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                    }
                }
            }
            else
            {
                // no rec has com
                if (CheckBoxICCom.Checked)
                {
                    //has date
                    if (CheckBoxICDate.Checked)
                    {
                        
                            try
                            {
                                DateTime _from = Convert.ToDateTime(TextBoxDFrom.Text);
                                DateTime _to = Convert.ToDateTime(TextBoxDTo.Text);

                                if (_from > _to)
                                { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From has to be smaler than to date"); }

                                datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, _from, _to, "NRecent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                                if (CheckBoxAcc.Checked)
                                {
                                    datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, _from, _to, "Recent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                                }
                            }
                            catch (Exception)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From and To has to be dates");
                            }
                        
                    }
                    //no date
                    else
                    {
                        datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, DateTime.MinValue, DateTime.MaxValue, "NRecent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                    }
                }
                //no com
                else
                {
                    //has date
                    if (CheckBoxICDate.Checked)
                    {

                            try
                            {
                                DateTime _from = Convert.ToDateTime(TextBoxDFrom.Text);
                                DateTime _to = Convert.ToDateTime(TextBoxDTo.Text);

                                if (_from > _to)
                                { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From has to be smaler than to date"); }

                                datasource = CHNLSVC.General.GetInsuranceInvoice("", "", _from, _to, "NRecent" + type, null, "", "", "", "");
                                if (CheckBoxAcc.Checked)
                                {
                                    datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, _from, _to, "Recent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                                }
                            }
                            catch (Exception)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From and To has to be dates");
                            }
                        }
                    
                    else
                    {
                        datasource = CHNLSVC.General.GetInsuranceInvoice("", "", DateTime.MinValue, DateTime.MaxValue, "NRecent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                    }
                }
            }

            //grid data bind
            GridViewSearch.DataSource = datasource;
            GridViewSearch.DataBind();
            if (datasource.Rows.Count > 1 ) { 

            }
            if (PanelIssueCretificate.Visible) {

            }

            if (!CheckBoxICMrec.Checked && !CheckBoxICDate.Checked && !CheckBoxICCom.Checked && !CheckBoxAcc.Checked)
            {
                GridViewSearch.DataSource = CHNLSVC.General.GetInsuranceInvoice("", "", DateTime.MaxValue, DateTime.MinValue, "NRecent" + type, null, TextBoxAcc.Text.ToUpper(), "", "", "");
                GridViewSearch.DataBind();

            }
        }

        protected void GridViewSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxRNo.Text = GridViewSearch.Rows[GridViewSearch.SelectedIndex].Cells[1].Text;
            CheckRecieptAvailability(null);
        }

        protected void imgComSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanySearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxCom.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgPCsearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxPC.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(TextBoxCom.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void LinkButtonIssCovNot_Click(object sender, EventArgs e)
        {
            ResetFields(PanelSettleDebitNote.Controls);
            ResetFields(PanelClaimProcess.Controls);
            PanelInsReciept.Visible = true;
            PanelSettleDebitNote.Visible = false;
            PanelClaimProcess.Visible = false;

            DropDownListInsCom.DataSource = CHNLSVC.General.GetInsuranceCompanies();
            DropDownListInsCom.DataTextField = "MBI_DESC";
            DropDownListInsCom.DataValueField = "MBI_CD";
            DropDownListInsCom.DataBind();

            DropDownListInsPol.DataSource = CHNLSVC.General.GetInsurancePolicies();
            DropDownListInsPol.DataTextField = "SVIP_POLC_DESC";
            DropDownListInsPol.DataValueField = "SVIP_POLC_CD";
            DropDownListInsPol.DataBind();
            ButtonPrint.Enabled = false;
        }

        protected void LinkButtonSDN_Click(object sender, EventArgs e)
        {
            ResetFields(PanelInsReciept.Controls);
            ResetFields(PanelClaimProcess.Controls);
            PanelInsReciept.Visible = false;
            PanelSettleDebitNote.Visible = true;
            PanelClaimProcess.Visible = false;
            ButtonPrint.Enabled = false;
        }

        protected void LinkButtonCP_Click(object sender, EventArgs e)
        {
            ResetFields(PanelInsReciept.Controls);
            ResetFields(PanelSettleDebitNote.Controls);
            PanelInsReciept.Visible = false;
            PanelSettleDebitNote.Visible = false;
            PanelClaimProcess.Visible = true;
            ButtonPrint.Enabled = false;
        }

        //reset fields in container(page/uc)
        public static void ResetFields(ControlCollection pageControls)
        {
            foreach (Control contl in pageControls)
            {
                var strCntName = (contl.GetType()).Name; switch (strCntName)
                {
                    case "TextBox": 
                        var txtSource = (TextBox)contl; 
                        txtSource.Text = ""; 
                        break;
                    case "ListBox":
                        var lstSource = (ListBox)contl;
                        lstSource.Items.Clear(); 
                        lstSource.Enabled = true; 
                        break;
                    case "DropDownList": 
                        var cmbSource = (DropDownList)contl; 
                        cmbSource.SelectedIndex = -1; 
                        cmbSource.Enabled = true; 
                        break;
                    case "GridView": 
                        var dgvSource = (GridView)contl; 
                        dgvSource.DataSource = null; 
                        dgvSource.DataBind(); 
                        break;
                    case "CheckBox": 
                        var chkSource = (CheckBox)contl; 
                        chkSource.Checked = false; 
                        chkSource.Enabled = true; 
                        break;
                } ResetFields(contl.Controls);
            }
        }

        protected void LinkButtonIssCov_Click(object sender, EventArgs e)
        {

            DivISCP1.Visible = true;
            DivISCP2.Visible = true;
            PanelExtendCoverNote.Visible = false;
            PanelCoverNoteDetails.Visible = true;
            PanelCusDe.Enabled = true;
            PanelIssueCretificate.Visible = false;
            Panel3.Enabled = true;
            Panel4.Enabled = true;
            ResetFields(PanelIssueCretificate.Controls);
            ResetFields(PanelExtendCoverNote.Controls);
            //TextBoxRNo.Text = string.Empty;
            CheckRecieptAvailability(null);
            Button1_Click(null, null);
            DivVehical.Visible = false;
            GridViewSearch.Columns[GridViewSearch.Columns.Count - 1].Visible = false;
            ButtonPrint.Enabled = false;
        }

        protected void LinkButtonECN_Click(object sender, EventArgs e)
        {
            DivISCP1.Visible = true;
            DivISCP2.Visible = true;
            PanelExtendCoverNote.Visible = true;
            PanelCoverNoteDetails.Visible = false;
            PanelIssueCretificate.Visible = false;
            PanelCusDe.Enabled = false;
            Panel3.Enabled = false;
            Panel4.Enabled = false;
            ResetFields(PanelIssueCretificate.Controls);
            ResetFields(PanelCoverNoteDetails.Controls);
            //TextBoxRNo.Text = string.Empty;
            CheckRecieptAvailability(null);
            GridViewSearch.Columns[GridViewSearch.Columns.Count - 1].Visible = true;
            Button1_Click(null, null);
            DivVehical.Visible = false;
            ButtonPrint.Enabled = false;
        }

        protected void LinkButtonIssCer_Click(object sender, EventArgs e)
        {
            DivISCP1.Visible = true;
            DivISCP2.Visible = true;
            PanelExtendCoverNote.Visible = false;
            PanelCoverNoteDetails.Visible = false;
            PanelIssueCretificate.Visible = true;
            PanelCusDe.Enabled = false;
            Panel3.Enabled = false;
            Panel4.Enabled = false;
            ResetFields(PanelExtendCoverNote.Controls);
            ResetFields(PanelCoverNoteDetails.Controls);
            //TextBoxRNo.Text = string.Empty;
            CheckRecieptAvailability(null);
            GridViewSearch.Columns[GridViewSearch.Columns.Count - 1].Visible = true;
            Button1_Click(null, null);
            DivVehical.Visible = true;

            ButtonPrint.Enabled = true;
        }

        protected void CheckBoxSDCom_CheckedChanged(object sender, EventArgs e)
        {
           if(CheckBoxSDCom.Checked)
               DivSDCom.Visible=true;
            else
               DivSDCom.Visible = false;
        }

        protected void CheckBoxSDDate_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxSDDate.Checked)
                DivSDDate.Visible = true;
            else
                DivSDDate.Visible = false;
        }

        protected void ButtonSDSearch_Click(object sender, EventArgs e)
        {

            DataTable datasource = new DataTable();
            //recent checked
            if (CheckBoxSDMre.Checked)
            {
                //search com checked
                if (CheckBoxSDCom.Checked)
                {
                    //has date has com
                    if (CheckBoxSDDate.Checked)
                    {

                            try
                            {
                                DateTime _from = Convert.ToDateTime(TextBoxSDFrom.Text);
                                DateTime _to = Convert.ToDateTime(TextBoxSDTo.Text);

                                if (_from > _to)
                                { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From has to be smaler than to date"); }

                                datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxSDCom.Text, TextBoxSDPc.Text, _from, _to, "RecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                                if (CheckBoxDAcc.Checked)
                                {
                                    datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxSDCom.Text, TextBoxSDPc.Text, _from, _to, "RecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                                }
                            }
                            catch (Exception)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From and To has to be dates");
                            }
                        
                    }
                    //no date has com
                    else
                    {
                        datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxSDCom.Text, TextBoxSDPc.Text, DateTime.MinValue, DateTime.MaxValue, "RecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                    }
                }
                //no com
                else
                {
                    //no com has date
                    if (CheckBoxSDDate.Checked)
                    {
                       
                            try
                            {
                                DateTime _from = Convert.ToDateTime(TextBoxSDFrom.Text);
                                DateTime _to = Convert.ToDateTime(TextBoxSDTo.Text);

                                if (_from > _to)
                                { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From has to be smaler than to date"); }

                                datasource = CHNLSVC.General.GetInsuranceInvoice("", "", _from, _to, "RecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                                if (CheckBoxDAcc.Checked)
                                {
                                    datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxSDCom.Text, TextBoxSDPc.Text, _from, _to, "RecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                                }
                            }
                            catch (Exception)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From and To has to be dates");
                            }
                        
                    }
                    //no com no date
                    else
                    {
                        datasource = CHNLSVC.General.GetInsuranceInvoice("", "", DateTime.MinValue, DateTime.MaxValue, "RecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                    }
                }
            }
            else
            {
                // no rec has com
                if (CheckBoxICCom.Checked)
                {
                    //has date
                    if (CheckBoxSDDate.Checked)
                    {
                      
                            try
                            {
                                DateTime _from = Convert.ToDateTime(TextBoxSDFrom.Text);
                                DateTime _to = Convert.ToDateTime(TextBoxSDTo.Text);

                                if (_from > _to)
                                { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From has to be smaler than to date"); }

                                datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxCom.Text, TextBoxPC.Text, _from, _to, "NRecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                                if (CheckBoxDAcc.Checked)
                                {
                                    datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxSDCom.Text, TextBoxSDPc.Text, _from, _to, "RecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                                }
                            }
                            catch (Exception)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From and To has to be dates");
                            }
                        
                    }
                    //no date
                    else
                    {
                        datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxSDCom.Text, TextBoxSDPc.Text, DateTime.MinValue, DateTime.MaxValue, "NRecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                    }
                }
                //no com
                else
                {
                    //has date
                    if (CheckBoxSDDate.Checked)
                    {
                       
                            try
                            {
                                DateTime _from = Convert.ToDateTime(TextBoxSDFrom.Text);
                                DateTime _to = Convert.ToDateTime(TextBoxSDTo.Text);

                                if (_from > _to)
                                { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From has to be smaler than to date"); }

                                datasource = CHNLSVC.General.GetInsuranceInvoice("", "", _from, _to, "NRecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                                if (CheckBoxDAcc.Checked)
                                {
                                    datasource = CHNLSVC.General.GetInsuranceInvoice(TextBoxSDCom.Text, TextBoxSDPc.Text, _from, _to, "RecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                                }
                            }
                            catch (Exception)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From and To has to be dates");
                            }
                        }
                    
                    else
                    {
                        datasource = CHNLSVC.General.GetInsuranceInvoice("", "", DateTime.MinValue, DateTime.MaxValue, "NRecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                    }
                }
            }
            //grid data bind
            GridViewSettleDebit.DataSource = datasource;
            GridViewSettleDebit.DataBind();
            if (datasource.Rows.Count > 1) {
                SePanel.Height = 100;
            }

            if (!CheckBoxSDMre.Checked && !CheckBoxSDDate.Checked && !CheckBoxSDCom.Checked && !CheckBoxAcc.Checked)
            {
                GridViewSettleDebit.DataSource = CHNLSVC.General.GetInsuranceInvoice("", "", DateTime.MaxValue, DateTime.MinValue, "NRecentDE", TextBoxVehNo.Text, TextBoxDAcc.Text.ToUpper(), "", "", "");
                GridViewSettleDebit.DataBind();
                SePanel.Height = 50;
            }
        }

        protected void checkReciept_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow=((GridViewRow)((CheckBox)sender).NamingContainer);
            string debitNo = GridViewSettleDebit.Rows[gvRow.RowIndex].Cells[1].Text;
            string recNo = GridViewSettleDebit.DataKeys[gvRow.RowIndex].Value.ToString();
            if (((CheckBox)sender).Checked)
            {
                //check name
                ListItem li = ListBoxRecieptList.Items.FindByText(debitNo);
                if (li == null)
                {
                    ListItem debit = new ListItem(debitNo, recNo);
                    CheckRecieptAvailability(debit);
                }
               // ListBoxRecieptList.Items.Add(st);
            }
            else
            {
                List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("SetDebNot", recNo);
                //load details
                if (_vehins != null)
                {
                    hdnSetlDebAmo.Value = (Convert.ToDecimal(hdnSetlDebAmo.Value) - _vehins[0].Svit_tot_val).ToString();
                    TextBoxSeDeTot.Text = hdnSetlDebAmo.Value;
                    BalanceAmount = BalanceAmount - _vehins[0].Svit_tot_val;
                    lblPayBalance.Text = TextBoxSeDeTot.Text;
                    ListBoxRecieptList.Items.Remove(new ListItem(debitNo,recNo));
                    if (ListBoxRecieptList.Items.Count <= 0) {
                        hdnGetConfirm.Value = "0";
                    }
                }
            }
        }

        private void CheckRecieptAvailability(ListItem li)
        {
            try
            {
                if (PanelInsReciept.Visible)
                {

                    if (PanelCoverNoteDetails.Visible)
                    {
                        List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssInsCovNot", TextBoxRNo.Text);
                        //load details
                        if (_vehins != null)
                        {
                            DropDownListInsCom.SelectedValue = _vehins[0].Svit_ins_com;
                            DropDownListInsPol.SelectedValue = _vehins[0].Svit_ins_polc;

                            TextBoxCusCode.Text = _vehins[0].Svit_cust_cd;
                            DropDownListCusTitle.SelectedValue = _vehins[0].Svit_cust_title;
                            TextBoxLastName.Text = _vehins[0].Svit_last_name;
                            TextBoxFullName.Text = _vehins[0].Svit_full_name;
                            TextBoxInitials.Text = _vehins[0].Svit_initial;
                            TextBoxAdd1.Text = _vehins[0].Svit_add01;
                            TextBoxAdd2.Text = _vehins[0].Svit_add02;
                            TextBoxCity.Text = _vehins[0].Svit_city;
                            DropDownListDistrict.SelectedValue = _vehins[0].Svit_district;
                            TextBoxProvince.Text = _vehins[0].Svit_province;
                            TextBoxContact.Text = _vehins[0].Svit_contact;
                            //TextBoxMake.Text=_vehins.m
                            TextBoxModel.Text = _vehins[0].Svit_model;
                            TextBoxCC.Text = _vehins[0].Svit_capacity;
                            TextBoxEngine.Text = _vehins[0].Svit_engine;
                            TextBoxChassis.Text = _vehins[0].Svit_chassis;
                            TextBoxInsValue.Text = _vehins[0].Svit_ins_val.ToString();
                            List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);
                            DataTable dt = CHNLSVC.General.GetHpSch(_vehins[0].Svit_inv_no);
                            if (dt.Rows.Count > 0)
                            {
                                
                                TextBoxSchemeCode.Text = dt.Rows[0]["HPA_SCH_CD"].ToString();
                                TextBoxVAcc.Text = dt.Rows[0]["HPA_ACC_NO"].ToString();
                            }
                            else {
                                TextBoxSchemeCode.Text = "";
                                TextBoxVAcc.Text = "";
                            }
                            TextBoxSalesPrice.Text = list[0].Sad_unit_amt.ToString();
                            TextBoxCNFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                            string _cusNo = "";
                            lock (this)
                            {
                                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                                _receiptAuto.Aut_cate_cd = "COVNT";
                                _receiptAuto.Aut_cate_tp = "COVNT";
                                _receiptAuto.Aut_start_char = "ABN";
                                _receiptAuto.Aut_direction = 0;
                                _receiptAuto.Aut_modify_dt = null;
                                _receiptAuto.Aut_moduleid = "COVNT";
                                _receiptAuto.Aut_number = 0;
                                _receiptAuto.Aut_year = 2012;
                                _cusNo = CHNLSVC.General.GetCoverNoteNo(_receiptAuto, "Cover");
                            }
                            TextBoxCNNo.Text = _cusNo;
                            hdnGetConfirm.Value = "1";
                            DropDownListInsCom.Enabled = false;
                            DropDownListInsPol.Enabled = false;
                        }
                        else
                        {
                            if (TextBoxRNo.Text != "")
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Invalid reciept');", true);
                            ResetFields(Panel4.Controls);
                            ResetFields(DivISCP1.Controls);
                            // ResetFields(PanelCusDe.Controls);
                            ResetFields(Panel3.Controls); hdnGetConfirm.Value = "0";
                            DropDownListInsCom.Enabled = false;
                            DropDownListInsPol.Enabled = false;
                        }
                    }
                    else if (PanelExtendCoverNote.Visible)
                    {
                        List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("ExtCovNot", TextBoxRNo.Text);
                        //load details
                        if (_vehins != null)
                        {
                            DropDownListInsCom.SelectedValue = _vehins[0].Svit_ins_com;
                            DropDownListInsPol.SelectedValue = _vehins[0].Svit_ins_polc;

                            TextBoxCusCode.Text = _vehins[0].Svit_cust_cd;
                            DropDownListCusTitle.SelectedValue = _vehins[0].Svit_cust_title;
                            TextBoxLastName.Text = _vehins[0].Svit_last_name;
                            TextBoxFullName.Text = _vehins[0].Svit_full_name;
                            TextBoxInitials.Text = _vehins[0].Svit_initial;
                            TextBoxAdd1.Text = _vehins[0].Svit_add01;
                            TextBoxAdd2.Text = _vehins[0].Svit_add02;
                            TextBoxCity.Text = _vehins[0].Svit_city;
                            DropDownListDistrict.SelectedValue = _vehins[0].Svit_district;
                            TextBoxProvince.Text = _vehins[0].Svit_province;
                            TextBoxContact.Text = _vehins[0].Svit_contact;
                            //TextBoxMake.Text=_vehins.m
                            TextBoxModel.Text = _vehins[0].Svit_model;
                            TextBoxCC.Text = _vehins[0].Svit_capacity;
                            TextBoxEngine.Text = _vehins[0].Svit_engine;
                            TextBoxChassis.Text = _vehins[0].Svit_chassis;
                            if (_vehins[0].Svit_ext_to_dt.ToShortDateString() == "31/12/2999")
                                TextBoxECNFrom.Text = _vehins[0].Svit_cvnt_to_dt.ToShortDateString();
                            else
                                TextBoxECNFrom.Text = _vehins[0].Svit_ext_to_dt.ToShortDateString();
                            List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);
                            DataTable dt = CHNLSVC.General.GetHpSch(_vehins[0].Svit_inv_no);
                            if (dt.Rows.Count > 0)
                            {
                                TextBoxSchemeCode.Text = dt.Rows[0]["HPA_SCH_CD"].ToString();
                                TextBoxVAcc.Text = dt.Rows[0]["HPA_ACC_NO"].ToString();
                            }
                            else
                            {
                                TextBoxSchemeCode.Text = "";
                                TextBoxVAcc.Text = "";
                            }
                            TextBoxSalesPrice.Text = list[0].Sad_unit_amt.ToString();
                            hdnGetConfirm.Value = "1";
                            DropDownListInsCom.Enabled = false;
                            DropDownListInsPol.Enabled = false;
                        }
                        else
                        {
                            if (TextBoxRNo.Text != "")
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Invalid reciept');", true);
                            ResetFields(Panel4.Controls);
                            ResetFields(DivISCP1.Controls);
                            //ResetFields(PanelCusDe.Controls);
                            ResetFields(Panel3.Controls); hdnGetConfirm.Value = "0";
                            DropDownListInsCom.Enabled = false;
                            DropDownListInsPol.Enabled = false;
                        }
                    }
                    else
                    {
                        List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssCer", TextBoxRNo.Text);
                        //load details
                        if (_vehins != null)
                        {
                            DropDownListInsCom.SelectedValue = _vehins[0].Svit_ins_com;
                            DropDownListInsPol.SelectedValue = _vehins[0].Svit_ins_polc;

                            TextBoxCusCode.Text = _vehins[0].Svit_cust_cd;
                            DropDownListCusTitle.SelectedValue = _vehins[0].Svit_cust_title;
                            TextBoxLastName.Text = _vehins[0].Svit_last_name;
                            TextBoxFullName.Text = _vehins[0].Svit_full_name;
                            TextBoxInitials.Text = _vehins[0].Svit_initial;
                            TextBoxAdd1.Text = _vehins[0].Svit_add01;
                            TextBoxAdd2.Text = _vehins[0].Svit_add02;
                            TextBoxCity.Text = _vehins[0].Svit_city;
                            DropDownListDistrict.SelectedValue = _vehins[0].Svit_district;
                            TextBoxProvince.Text = _vehins[0].Svit_province;
                            TextBoxContact.Text = _vehins[0].Svit_contact;
                            //TextBoxMake.Text=_vehins.m
                            TextBoxModel.Text = _vehins[0].Svit_model;
                            TextBoxCC.Text = _vehins[0].Svit_capacity;
                            TextBoxEngine.Text = _vehins[0].Svit_engine;
                            TextBoxChassis.Text = _vehins[0].Svit_chassis;
                            TextBoxIssCreTot.Text = _vehins[0].Svit_ins_val.ToString();
                            TextBoxIssCerEff.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                            //ADDED 2012/10/27
                            TextBoxIssCerPol.Text = _vehins[0].Svit_polc_no;
                            TextBoxIssCerExp.Text = _vehins[0].Svit_expi_dt.ToString("dd/MMM/yyyy");
                            TextBoxIssCerDebNo.Text = _vehins[0].Svit_dbt_no;
                            TextBoxIssCerNet.Text = _vehins[0].Svit_net_prem.ToString();
                            TextBoxIssCerSRCC.Text = _vehins[0].Svit_srcc_prem.ToString();
                            TextBoxFileNo.Text = _vehins[0].Svit_file_no;
                            TextBoxIssCerSRCC_TextChanged(null, null);
                            //END

                            List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);
                            DataTable dt = CHNLSVC.General.GetHpSch(_vehins[0].Svit_inv_no);
                            if (dt.Rows.Count > 0)
                            {
                                TextBoxSchemeCode.Text = dt.Rows[0]["HPA_SCH_CD"].ToString();
                                TextBoxVAcc.Text = dt.Rows[0]["HPA_ACC_NO"].ToString();
                            }
                            else
                            {
                                TextBoxSchemeCode.Text = "";
                                TextBoxVAcc.Text = "";
                            }
                            TextBoxSalesPrice.Text = list[0].Sad_unit_amt.ToString();
                            LabelVehNo.Text = _vehins[0].Svit_veh_reg_no;
                            //TextBoxVehicalNo.Text = _vehins[0].Svit_veh_reg_no;
                            if (_vehins[0].Svit_reg_dt.ToShortDateString() == "31/12/2999")
                            {
                                //TextBoxRegDate.Text = "";
                                LabelRegDate.Text = "";
                            }
                            else
                            {
                                //TextBoxRegDate.Text = _vehins[0].Svit_reg_dt.ToShortDateString();
                                LabelRegDate.Text = _vehins[0].Svit_reg_dt.ToShortDateString();
                            }
                            hdnGetConfirm.Value = "1";
                            DropDownListInsCom.Enabled = false;
                            DropDownListInsPol.Enabled = false;
                        }
                        else
                        {
                            if (TextBoxRNo.Text != "")
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Invalid reciept');", true);
                            ResetFields(Panel4.Controls);
                            ResetFields(DivISCP1.Controls);
                            //ResetFields(PanelCusDe.Controls);
                            ResetFields(Panel3.Controls);
                            hdnGetConfirm.Value = "0";
                            DropDownListInsCom.Enabled = false;
                            DropDownListInsPol.Enabled = false;
                        }
                    }
                }
                if (PanelSettleDebitNote.Visible)
                {
                    List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("SetDebNot", li.Value);
                    //load details
                    if (_vehins != null)
                    {
                        hdnSetlDebAmo.Value = (Convert.ToDecimal(hdnSetlDebAmo.Value) + _vehins[0].Svit_tot_val).ToString();
                        ListBoxRecieptList.Items.Add(li);
                        TextBoxSeDeTot.Text = hdnSetlDebAmo.Value;
                        BalanceAmount = Convert.ToDecimal(TextBoxSeDeTot.Text);
                        lblPayBalance.Text = TextBoxSeDeTot.Text;
                        hdnGetConfirm.Value = "1";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Invalid reciept');", true);
                    }
                }
            }
            catch (Exception er) {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }

        protected void TextBoxRNo_TextChanged(object sender, EventArgs e)
        {
            CheckRecieptAvailability(null);
        }

        protected void TextBoxCNFrom_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxCNDays.Text != string.Empty) {
                TextBoxCNTo.Text = ReturnToDate(Convert.ToDateTime(TextBoxCNFrom.Text), TextBoxCNDays.Text);
            }
        }

        protected void TextBoxCNDays_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxCNFrom.Text != string.Empty)
            {
                TextBoxCNTo.Text =ReturnToDate(Convert.ToDateTime(TextBoxCNFrom.Text), TextBoxCNDays.Text);
            }
        }

        [System.Web.Services.WebMethod]
        public static string ReturnToDate(DateTime from,string no) {
            int tem;
            if (!int.TryParse(no, out tem))
            {
                return "";
            }
            else
            return from.AddDays(Convert.ToInt32(no)).ToString("dd/MMM/yyyy");
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (PanelInsReciept.Visible)
            {
                if (PanelCoverNoteDetails.Visible)
                {
                    List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssInsCovNot", TextBoxRNo.Text);
                    if (_vehins != null)
                    {
                        if (hdnGetConfirm.Value == "1")
                        {

                            //permission
                             //string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                             //if (!CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _masterLocation, "INV2"))
                             //{ 
                             //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "You do not have permission to add cover note");
                             //   return;
                             //}

                            //validation
                            int tem;
                            decimal temVal;
                            DateTime temDate;
                            if (!int.TryParse(TextBoxCNDays.Text, out tem))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No of days required and has to be number");
                                return;
                            }
                            if (!DateTime.TryParse(TextBoxCNFrom.Text, out temDate))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select From date");
                                return;
                            }
                            if (!DateTime.TryParse(TextBoxCNTo.Text, out temDate))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select to date");
                                return;

                            }
                            if (TextBoxCNNo.Text == "") {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter cover note no");
                                return;
                            }
                            if (!Decimal.TryParse(TextBoxInsValue.Text, out temVal))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Insurance value is required and has to be a number");
                                return;
                            }
                            try
                            {
                                _vehins[0].Svit_ins_com = DropDownListInsCom.SelectedValue;
                                _vehins[0].Svit_ins_polc = DropDownListInsPol.SelectedValue;

                                _vehins[0].Svit_cust_cd = TextBoxCusCode.Text;
                                _vehins[0].Svit_cust_title = DropDownListCusTitle.SelectedValue;
                                _vehins[0].Svit_last_name = TextBoxLastName.Text;
                                _vehins[0].Svit_full_name = TextBoxFullName.Text;
                                _vehins[0].Svit_initial = TextBoxInitials.Text;
                                _vehins[0].Svit_add01 = TextBoxAdd1.Text;
                                _vehins[0].Svit_add02 = TextBoxAdd2.Text;
                                _vehins[0].Svit_city = TextBoxCity.Text;
                                _vehins[0].Svit_district = DropDownListDistrict.SelectedValue;
                                _vehins[0].Svit_province = TextBoxProvince.Text;
                                _vehins[0].Svit_contact = TextBoxContact.Text;
                                //TextBoxMake.Text=_vehins.m
                                _vehins[0].Svit_model = TextBoxModel.Text;
                                _vehins[0].Svit_capacity = TextBoxCC.Text;

                                _vehins[0].Svit_cvnt_no = TextBoxCNNo.Text;
                                _vehins[0].Svit_ins_val = Convert.ToDecimal(TextBoxInsValue.Text);
                                _vehins[0].Svit_cvnt_issue = true;
                                _vehins[0].Svit_cvnt_by = GlbUserName;
                                _vehins[0].Svit_cvnt_days = Convert.ToInt32(TextBoxCNDays.Text);
                                _vehins[0].Svit_cvnt_from_dt = Convert.ToDateTime(TextBoxCNFrom.Text);
                                _vehins[0].Svit_cvnt_to_dt = Convert.ToDateTime(TextBoxCNTo.Text);
                                _vehins[0].Svit_cvnt_dt = DateTime.Now;
                                _vehins[0].Svit_no_of_prnt = _vehins[0].Svit_no_of_prnt+1;
                                List<InvoiceItem> list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_vehins[0].Svit_inv_no);

                                //_vehins[0].Svit_ext_no=ECN
                                CHNLSVC.General.SaveVehicalInsurance(_vehins[0]);

                                //update status
                                CHNLSVC.General.UpdateCoverNote(_vehins[0].Svit_inv_no, list[0].Sad_itm_cd, GlbUserComCode);


                                //print method
                                GlbCoverNoteNo = TextBoxCNNo.Text;
                                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\VehInsCoverNote.rpt";
                                GlbReportMapPath = "~/Reports_Module/Sales_Rep/VehInsCoverNote.rpt";

                                GlbMainPage = "~/General_Modules/VehicalInsurance.aspx";
                                string Msg = "<script>window.open('../Reports_Module/Sales_Rep/CoverNotePrint.aspx','_blank');</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                                //Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");


                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');", true);
                                TextBoxCNNo.Text = "";
                                TextBoxCNFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                                TextBoxCNTo.Text = "";
                                TextBoxCNDays.Text = "";
                                TextBoxInsValue.Text = "";
                            }
                            catch (Exception er) {
                                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                            }
                            
                        }
                        hdnGetConfirm.Value = "0";
                    }
                }
                else if (PanelExtendCoverNote.Visible)
                {
                    List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("ExtCovNot", TextBoxRNo.Text);
                    if (_vehins != null)
                    {
                        if (hdnGetConfirm.Value == "1")
                        {
                            //validation
                            int tem;
                            DateTime temDate;
                            if (!int.TryParse(TextBoxECNDays.Text, out tem))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No of days required and has to be number");
                                return;
                            }
                            if (!DateTime.TryParse(TextBoxECNFrom.Text, out temDate))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select From date");
                                return;
                            }
                            if (!DateTime.TryParse(TextBoxECNTo.Text, out temDate))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select to date");
                                return;
                            }
                            try
                            {

                                _vehins[0].Svit_ext_issue = true;
                                _vehins[0].Svit_ext_by = GlbUserName;
                                _vehins[0].Svit_ext_days = Convert.ToInt32(TextBoxECNDays.Text);
                                _vehins[0].Svit_ext_from_dt = Convert.ToDateTime(TextBoxECNFrom.Text);
                                _vehins[0].Svit_ext_to_dt = Convert.ToDateTime(TextBoxECNTo.Text);
                                _vehins[0].Svit_ext_dt = DateTime.Now;
                                //_vehins[0].Svit_ext_no=ECN
                                CHNLSVC.General.SaveVehicalInsurance(_vehins[0]);
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalInsurance.aspx';", true);
                            }
                            catch (Exception er)
                            {
                                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                            }
                        }
                        hdnGetConfirm.Value = "0";
                    }
                }
                else
                {
                    List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssCer", TextBoxRNo.Text);
                    if (_vehins != null)
                    {
                        if (LabelVehNo.Text == "")
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Can not save without veh. no");
                            return;
                        }

                        if (LabelVehNo.Text != "")
                        {
                            if (hdnGetConfirm.Value == "1")
                            {

                                //validation
                                decimal temVal;
                                DateTime temDate;
                                if (TextBoxIssCerPol.Text == "")
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter policy no");
                                    return;
                                }
                                if (!DateTime.TryParse(TextBoxIssCerEff.Text, out temDate))
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select effictive date");
                                    return;
                                }
                                if (!DateTime.TryParse(TextBoxIssCerExp.Text, out temDate))
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select expire date");
                                    return;
                                }
                                if (TextBoxIssCerDebNo.Text == "")
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter debit no");
                                    return;
                                }
                                if (!Decimal.TryParse(TextBoxIssCerNet.Text, out temVal))
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Net Premium is required and has to be a number");
                                    return;
                                }
                                if (!Decimal.TryParse(TextBoxIssCerSRCC.Text, out temVal))
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "SRCC Premium is required and has to be a number");
                                    return;
                                }
                                if (!Decimal.TryParse(TextBoxIssCerOther.Text, out temVal))
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Other value is required and has to be a number");
                                    return;
                                }
                                //if (TextBoxRegDate.Text == "") {
                                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter reg date");
                                //    return;
                                //}
                                try
                                {
                                    _vehins[0].Svit_net_prem = Convert.ToDecimal(TextBoxIssCerNet.Text);
                                    _vehins[0].Svit_oth_val = Convert.ToDecimal(TextBoxIssCerOther.Text);
                                    _vehins[0].Svit_srcc_prem = Convert.ToDecimal(TextBoxIssCerSRCC.Text);
                                    _vehins[0].Svit_tot_val = Convert.ToDecimal(TextBoxIssCreTot.Text);
                                    _vehins[0].Svit_polc_by = GlbUserName;
                                    _vehins[0].Svit_polc_dt = DateTime.Now;
                                    _vehins[0].Svit_polc_no = TextBoxIssCerPol.Text;
                                    _vehins[0].Svit_expi_dt = Convert.ToDateTime(TextBoxIssCerExp.Text);
                                    _vehins[0].Svit_eff_dt = Convert.ToDateTime(TextBoxIssCerEff.Text);
                                    _vehins[0].Svit_file_no = TextBoxFileNo.Text;
                                    _vehins[0].Svit_dbt_no = TextBoxIssCerDebNo.Text;
                                    _vehins[0].Svit_polc_stus = true;
                                    _vehins[0].Svit_reg_dt = Convert.ToDateTime(LabelRegDate.Text);
                                    _vehins[0].Svit_veh_reg_no = LabelVehNo.Text;
                                    CHNLSVC.General.SaveVehicalInsurance(_vehins[0]);
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalInsurance.aspx';", true);
                                }
                                catch (Exception er)
                                {
                                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                                    string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                                }
                            }
                            hdnGetConfirm.Value = "0";
                        }
                    }
                }
            }
            if (PanelSettleDebitNote.Visible) {

                decimal temVal;
               
                if (!Decimal.TryParse(TextBoxSeDeTot.Text, out temVal))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select reciept");
                    return;
                }
                if(Convert.ToDecimal(lblPayBalance.Text)!=0){
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "You have to pay full amount");
                    return;
                }
                try
                {
                    MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                    _receiptAuto.Aut_cate_cd = GlbUserDefLoca;
                    _receiptAuto.Aut_cate_tp = "HO";
                    _receiptAuto.Aut_start_char = "DBSET";
                    _receiptAuto.Aut_direction = 0;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "DBSET";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_year = 2012;
                    string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                    //ins pay table
                    int cou = 0;
                    foreach (RecieptItem ri in _recieptItem)
                    {
                        cou++;
                        VehicalInsurancePay vehInsPay = new VehicalInsurancePay();
                        vehInsPay.Pay_ref_no = _cusNo;
                        vehInsPay.Ref_line = cou;
                        vehInsPay.Cre_by = GlbUserName;
                        vehInsPay.Value = ri.Sard_settle_amt;
                        vehInsPay.Pay_tp = ri.Sard_pay_tp;
                        if (ri.Sard_pay_tp == "CHEQUE")
                        {
                            vehInsPay.Ref_no = ri.Sard_ref_no;
                            vehInsPay.Bank = ri.Sard_chq_bank_cd;
                            vehInsPay.Bank_branch = ri.Sard_chq_branch;
                        }
                        else if (ri.Sard_pay_tp == "CRCD")
                        {
                            vehInsPay.Ref_no = ri.Sard_ref_no;
                            vehInsPay.Bank = ri.Sard_credit_card_bank;
                            vehInsPay.Bank_branch = ri.Sard_chq_branch;
                        }
                        CHNLSVC.General.SaveInsurancePay(vehInsPay);
                    }
                    foreach (ListItem li in ListBoxRecieptList.Items)
                    {
                        List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("SetDebNot", li.Text);
                        if (_vehins != null)
                        {

                            _vehins[0].Svit_dbt_set_stus = true;
                            _vehins[0].Svit_dbt_by = GlbUserName;
                            _vehins[0].Svit_dbt_dt = DateTime.Now;
                            _vehins[0].Pay_ref_no = _cusNo;
                            CHNLSVC.General.SaveVehicalInsurance(_vehins[0]);
                        }
                    }
                    Panel5.Enabled = true;
                    hdnGetConfirm.Value = "0";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalInsurance.aspx';", true);
                }
                catch (Exception er)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                    string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                }
            }
            else 
            { 
               if (PanelClaimInimated.Visible) {
                   if (hdnGetConfirm.Value == "1")
                   {

                       decimal trnVal;
                       DateTime temDate;
                       if (!DateTime.TryParse(TextBoxAccDate.Text, out temDate))
                       {
                           MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select Accident date");
                           return;
                       }
                       try
                       {
                           List<VehicalInsuranceClaim> vehClList = CHNLSVC.General.GetClaimDetails("RecieveDocumentSpecific", VehNo, AccDate);
                           if (vehClList != null)
                           {

                               vehClList[0].Reg_no = VehNo;
                               vehClList[0].Polic_rep_sts = CheckBoxPoliceReport.Checked;
                               vehClList[0].Dri_name = TextBoxDriverName.Text;
                               vehClList[0].Dl_lic = TextBoxLicenceNo.Text;
                               vehClList[0].Init_by = GlbUserName;
                               vehClList[0].Init_date = DateTime.Now;
                               CHNLSVC.General.SaveInsuranceClaim(vehClList[0]);
                               ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalInsurance.aspx';", true);
                           }
                           else
                           {
                               VehicalInsuranceClaim _vehinsClaim = new VehicalInsuranceClaim();
                               _vehinsClaim.Reg_no = TextBoxCPRegNo.Text.ToUpper();
                               _vehinsClaim.Acc_date = Convert.ToDateTime(TextBoxAccDate.Text);
                               _vehinsClaim.Polic_rep_sts = CheckBoxPoliceReport.Checked;
                               _vehinsClaim.Dri_name = TextBoxDriverName.Text;
                               _vehinsClaim.Dl_lic = TextBoxLicenceNo.Text;
                               _vehinsClaim.Init_by = GlbUserName;
                               _vehinsClaim.Init_date = DateTime.Now;
                               CHNLSVC.General.SaveInsuranceClaim(_vehinsClaim);
                               ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record added sucessfully!');window.location='VehicalInsurance.aspx';", true);
                           }

                           hdnGetConfirm.Value = "0";
                       }
                       catch (Exception er)
                       {
                           //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                           string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                           ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                       }
                      
                   }
                }
                else if (PanelRecieveDocument.Visible) {
                    if (hdnGetConfirm.Value == "1")
                    {
                        List<VehicalInsuranceClaim> vehClList = CHNLSVC.General.GetClaimDetails("RecieveDocumentSpecific",VehNo ,AccDate);
                        if (vehClList != null) {
                            decimal trnVal;
                            DateTime temDate;
                            if (!DateTime.TryParse(TextBoxClaimFormRec.Text, out temDate))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select Claim form recieve date");
                                return;
                            }
                            try
                            {
                                if (TextBoxClaimFormRec.Text != "")
                                    vehClList[0].Clamin_form_rec = Convert.ToDateTime(TextBoxClaimFormRec.Text);
                                if (TextBoxLicenceRecieve.Text != "")
                                    vehClList[0].Dl_rec = Convert.ToDateTime(TextBoxLicenceRecieve.Text);
                                if (TextBoxRapirEstiRec.Text != "")
                                    vehClList[0].Repi_esti_rec = Convert.ToDateTime(TextBoxRapirEstiRec.Text);
                                if (TextBoxPoliceRepRecieve.Text != "")
                                    vehClList[0].Police_rep_rec = Convert.ToDateTime(TextBoxPoliceRepRecieve.Text);
                                if (TextBoxFowadinApp.Text != "")
                                    vehClList[0].App_forw = Convert.ToDateTime(TextBoxFowadinApp.Text);
                                if (TextBoxFinalApp.Text != "")
                                    vehClList[0].Final_invo = Convert.ToDateTime(TextBoxFinalApp.Text);
                                if (TextBoxRepaireEsti.Text != "")
                                    vehClList[0].Repi_esti_val = Convert.ToDecimal(TextBoxRepaireEsti.Text);
                                vehClList[0].Doc_stus = true;
                                vehClList[0].Rec_by = GlbUserName;
                                vehClList[0].Rec_dt = DateTime.Now;
                                CHNLSVC.General.SaveInsuranceClaim(vehClList[0]);
                                hdnGetConfirm.Value = "0";
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalInsurance.aspx';", true);
                            }
                            catch (Exception er)
                            {
                                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                            }
                        }
                    }
                }
                else {
                    if (hdnGetConfirm.Value == "1")
                    {
                        List<VehicalInsuranceClaim> vehClList = CHNLSVC.General.GetClaimDetails("CustomerSettlementSpecific", VehNo, AccDate);
                        if (vehClList != null)
                        {
                            decimal trnVal;
                            DateTime temDate;
                            if (!decimal.TryParse(TextBoxClaimAmo.Text, out trnVal))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter Claim amount");
                                return;
                            }
                            if (!decimal.TryParse(TextBoxPolicyExcess.Text, out trnVal))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter policy excess amount");
                                return;
                            }
                            if (!decimal.TryParse(TextBoxOtherDeduction.Text, out trnVal))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter other deduction amount");
                                return;
                            }
                            if (!decimal.TryParse(TextBoxBalValue.Text, out trnVal))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter balance amount");
                                return;
                            }
                            if (!decimal.TryParse(TextBoxCheVal.Text, out trnVal))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter checque amount");
                                return;
                            }
                            if (!DateTime.TryParse(TextBoxCheDate.Text, out temDate))
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please select cheque date");
                                return;
                            }
                            if (TextBoxAccNo.Text == "") {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter account no");
                                return;
                            }
                            if (TextBoxCheNo.Text == "")
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter cheque no");
                                return;
                            }
                            if (TextBoxCheBank.Text == "")
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter cheque bank");
                                return;
                            }
                            if (TextBoxCheBranch.Text == "")
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter cheque branch");
                                return;
                            }
                            try
                            {
                                vehClList[0].Claim_val = Convert.ToDecimal(TextBoxClaimAmo.Text);
                                vehClList[0].Policy_excess = Convert.ToDecimal(TextBoxPolicyExcess.Text);
                                vehClList[0].Other_dedection = Convert.ToDecimal(TextBoxOtherDeduction.Text);
                                vehClList[0].Bal_val = Convert.ToDecimal(TextBoxBalValue.Text);
                                vehClList[0].Acc_no = TextBoxAccNo.Text;
                                vehClList[0].Cheq_no = TextBoxCheNo.Text;
                                vehClList[0].Cheq_bank = TextBoxCheBank.Text;
                                vehClList[0].Cheq_branch = TextBoxCheBranch.Text;
                                vehClList[0].Cheq_dt = Convert.ToDateTime(TextBoxCheDate.Text);
                                vehClList[0].Cheq_val = Convert.ToDecimal(TextBoxCheVal.Text);
                                vehClList[0].Sett_dt = DateTime.Now;
                                vehClList[0].Set_by = GlbUserName;
                                vehClList[0].Set_dt = DateTime.Now;
                                CHNLSVC.General.SaveInsuranceClaim(vehClList[0]);
                                hdnGetConfirm.Value = "0";
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Record updated sucessfully!');window.location='VehicalInsurance.aspx';", true);
                            }
                            catch (Exception er)
                            {
                                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                            }
                        }
                    }
                }
            }
        }

        protected void TextBoxECNDays_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxECNFrom.Text != string.Empty)
            {
                TextBoxECNTo.Text = ReturnToDate(Convert.ToDateTime(TextBoxECNFrom.Text), TextBoxECNDays.Text);
            }
        }

        protected void TextBoxIssCerNet_TextChanged(object sender, EventArgs e)
        {
            CalcTotalPremium();
        }

        private void CalcTotalPremium()
        {
            try
            {
                decimal net = Convert.ToDecimal(TextBoxIssCerNet.Text);
                decimal srcc = Convert.ToDecimal(TextBoxIssCerSRCC.Text);
                decimal total = Convert.ToDecimal(TextBoxIssCreTot.Text);

                TextBoxIssCerOther.Text = (total-(net + srcc)).ToString();
            }
            catch (Exception) { }
        }

        protected void TextBoxIssCerSRCC_TextChanged(object sender, EventArgs e)
        {
            CalcTotalPremium();
        }

        protected void TextBoxIssCerOther_TextChanged(object sender, EventArgs e)
        {
            CalcTotalPremium();
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

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/VehicalInsurance.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void LinkButtonSDAdd_Click(object sender, EventArgs e)
        {

            List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetVehicalInsuarance(null,TextBoxSDRec.Text);
            if (_vehins != null)
            {
                ListItem li = ListBoxRecieptList.Items.FindByText(TextBoxSDRec.Text);
                if (li == null)
                {
                    CheckRecieptAvailability(new ListItem(TextBoxSDRec.Text, _vehins[0].Svit_ref_no));
                }
            }
        }

        protected void DropDownListDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DropDownListDistrict.SelectedValue.ToString())) return;
            DistrictProvince _type = CHNLSVC.Sales.GetDistrict(DropDownListDistrict.SelectedValue.ToString())[0];
            if (_type.Mds_district == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid district."); return; }
            TextBoxProvince.Text = _type.Mds_province;
        }

        protected void LinkButtonCPCI_Click(object sender, EventArgs e)
        {
            PanelClaimInimated.Visible = true;
            PanelRecieveDocument.Visible = false;
            PanelCustomerSettlement.Visible = false;
            ResetFields(Panel11.Controls);
            TextBoxCPRegNo.Text = "";
            ClearClaimProcess();
        }

        protected void LinkButtonCPRD_Click(object sender, EventArgs e)
        {
            PanelClaimInimated.Visible = false;
            PanelRecieveDocument.Visible = true;
            PanelCustomerSettlement.Visible = false;
            LoadRecDocDdl();
            ResetFields(Panel12.Controls);
            if(DropDownListRDRegNo.DataSource!=null)
            DropDownListRDRegNo.SelectedIndex = 0;
            ClearClaimProcess();
            DropDownListRDRegNo_SelectedIndexChanged(null, null);
        }

        private void LoadRecDocDdl()
        {
           List<VehicalInsuranceClaim> claLi= CHNLSVC.General.GetClaimDetails("RecieveDocumentDestinct", null, DateTime.MinValue);
            if(claLi!=null)
                foreach (VehicalInsuranceClaim veCl in claLi) {
                    if(DropDownListRDRegNo.Items.FindByText(veCl.Reg_no)==null)
                    DropDownListRDRegNo.Items.Add(new ListItem(veCl.Reg_no, veCl.Reg_no));
                }
            DropDownListRDRegNo.DataBind();
        }

        private void LoadCusSetDdl()
        {
            List<VehicalInsuranceClaim> claLi = CHNLSVC.General.GetClaimDetails("CustomerSettlement", null, DateTime.MinValue);
            if (claLi != null)
                foreach (VehicalInsuranceClaim veCl in claLi)
                {
                    if (DropDownListCSRegNo.Items.FindByText(veCl.Reg_no) == null)
                        DropDownListCSRegNo.Items.Add(new ListItem(veCl.Reg_no, veCl.Reg_no));
                }
            DropDownListRDRegNo.DataBind();
        }

        private string AddHtmlSpaces(int length)
        {
            string space = "";
            for (int i = 0; i < length; i++)
            {
                space = space + " &nbsp;";
            }
            return space;
        }

        protected void LinkButtonCPCS_Click(object sender, EventArgs e)
        {
            PanelClaimInimated.Visible = false;
            PanelRecieveDocument.Visible = false;
            PanelCustomerSettlement.Visible = true;
            ResetFields(Panel13.Controls);
            if (DropDownListCSRegNo.DataSource != null)
            DropDownListCSRegNo.SelectedIndex = 0;
            ClearClaimProcess();
            LoadCusSetDdl();
            DropDownListCSRegNo_SelectedIndexChanged(null, null);
        }

        protected void LinkButtonSDRemove_Click(object sender, EventArgs e)
        {
            List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("SetDebNot", ListBoxRecieptList.SelectedValue);
            //load details
            if (_vehins != null)
            {
                hdnSetlDebAmo.Value = (Convert.ToDecimal(hdnSetlDebAmo.Value) - _vehins[0].Svit_tot_val).ToString();
                TextBoxSeDeTot.Text = hdnSetlDebAmo.Value;
                BalanceAmount = BalanceAmount - _vehins[0].Svit_tot_val;
                lblPayBalance.Text = TextBoxSeDeTot.Text;
                ListBoxRecieptList.Items.RemoveAt(ListBoxRecieptList.SelectedIndex);
                if (ListBoxRecieptList.Items.Count <= 0)
                {
                    hdnGetConfirm.Value = "0";
                }
            }
        }

        protected void TextBoxCPRegNo_TextChanged(object sender, EventArgs e)
        {
            List<FF.BusinessObjects.VehicleInsuarance> _vehinsList = CHNLSVC.General.GetClaimCusDetails(TextBoxCPRegNo.Text.ToUpper());
            if (_vehinsList != null)
            {
                LabelCusTi.Text = _vehinsList[0].Svit_cust_title;
                LabelFullName.Text = _vehinsList[0].Svit_full_name;
                LabelLastName.Text = _vehinsList[0].Svit_last_name;
                LabelInitials.Text = _vehinsList[0].Svit_initial;
                LabelAddLin1.Text = _vehinsList[0].Svit_add01;
                LabelAddlin2.Text = _vehinsList[0].Svit_add02;
                LabelDistrict.Text = _vehinsList[0].Svit_district;
                LabelCity.Text = _vehinsList[0].Svit_city;
                LabelProvince.Text = _vehinsList[0].Svit_province;
                LabelContact.Text = _vehinsList[0].Svit_contact;
                hdnGetConfirm.Value = "1";

                GridViewPreviousClaims.DataSource = CHNLSVC.General.GetClaimDetails("REGISTRATION", TextBoxCPRegNo.Text.ToUpper(), DateTime.MinValue);
                GridViewPreviousClaims.DataBind();
            }
            else
            {
                ClearClaimProcess();
                hdnGetConfirm.Value = "0";
                GridViewPreviousClaims.DataSource = CHNLSVC.General.GetClaimDetails("REGISTRATION", TextBoxCPRegNo.Text.ToUpper(), DateTime.MinValue);
                GridViewPreviousClaims.DataBind();
            }
        }

        private void ClearClaimProcess()
        {
            LabelCusTi.Text = "";
            LabelFullName.Text = "";
            LabelLastName.Text = "";
            LabelInitials.Text = "";
            LabelAddLin1.Text = "";
            LabelAddlin2.Text = "";
            LabelDistrict.Text = "";
            LabelCity.Text = "";
            LabelProvince.Text = "";
            LabelContact.Text = "";

            LabelRDCusTi.Text = "";
            LabelRDFullName.Text = "";
            LabelRDLastName.Text = "";
            LabelRDInitials.Text = "";
            LabelRDAddLin1.Text = "";
            LabelRDAddlin2.Text = "";
            LabelRDDistrict.Text = "";
            LabelRDCity.Text = "";
            LabelRDProvince.Text = "";
            LabelRDContact.Text = "";

            LabelCSCusTi.Text = "";
            LabelCSFullName.Text = "";
            LabelCSLastName.Text = "";
            LabelCSInitials.Text = "";
            LabelCSAddLin1.Text = "";
            LabelCSAddlin2.Text = "";
            LabelCSDistrict.Text = "";
            LabelCSCity.Text = "";
            LabelCSProvince.Text = "";
            LabelCSContact.Text = "";
        }

        protected void DropDownListRDRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRDPreviousClaim.DataSource = CHNLSVC.General.GetClaimDetails("RecieveDocument", DropDownListRDRegNo.SelectedValue, DateTime.MinValue);
            GridViewRDPreviousClaim.DataBind();
        }

        protected void GridViewRDPreviousClaim_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime accdate = Convert.ToDateTime(GridViewRDPreviousClaim.Rows[GridViewRDPreviousClaim.SelectedIndex].Cells[1].Text);
            string vehNo = GridViewRDPreviousClaim.Rows[GridViewRDPreviousClaim.SelectedIndex].Cells[2].Text;
            List<VehicalInsuranceClaim> claimList = CHNLSVC.General.GetClaimDetails("RecieveDocumentSpecific", vehNo, accdate);
            if (claimList != null)
            {
                AccDate = accdate;
                VehNo = vehNo;
                List<FF.BusinessObjects.VehicleInsuarance> _vehinsList = CHNLSVC.General.GetClaimCusDetails(vehNo);
                if (_vehinsList != null)
                {
                    LabelRDCusTi.Text = _vehinsList[0].Svit_cust_title;
                    LabelRDFullName.Text = _vehinsList[0].Svit_full_name;
                    LabelRDLastName.Text = _vehinsList[0].Svit_last_name;
                    LabelRDInitials.Text = _vehinsList[0].Svit_initial;
                    LabelRDAddLin1.Text = _vehinsList[0].Svit_add01;
                    LabelRDAddlin2.Text = _vehinsList[0].Svit_add02;
                    LabelRDDistrict.Text = _vehinsList[0].Svit_district;
                    LabelRDCity.Text = _vehinsList[0].Svit_city;
                    LabelRDProvince.Text = _vehinsList[0].Svit_province;
                    LabelRDContact.Text = _vehinsList[0].Svit_contact;
                }
                hdnGetConfirm.Value = "1";
            }
        }

        protected void GridViewCSPreviousClaim_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime accdate = Convert.ToDateTime(GridViewCSPreviousClaim.Rows[GridViewCSPreviousClaim.SelectedIndex].Cells[1].Text);
            string vehNo = GridViewCSPreviousClaim.Rows[GridViewCSPreviousClaim.SelectedIndex].Cells[2].Text;
            List<VehicalInsuranceClaim> claimList = CHNLSVC.General.GetClaimDetails("CustomerSettlementSpecific", vehNo, accdate);
            if (claimList != null)
            {
                AccDate = accdate;
                VehNo = vehNo;
                List<FF.BusinessObjects.VehicleInsuarance> _vehinsList = CHNLSVC.General.GetClaimCusDetails(vehNo);
                if (_vehinsList != null)
                {
                    LabelCSCusTi.Text = _vehinsList[0].Svit_cust_title;
                    LabelCSFullName.Text = _vehinsList[0].Svit_full_name;
                    LabelCSLastName.Text = _vehinsList[0].Svit_last_name;
                    LabelCSInitials.Text = _vehinsList[0].Svit_initial;
                    LabelCSAddLin1.Text = _vehinsList[0].Svit_add01;
                    LabelCSAddlin2.Text = _vehinsList[0].Svit_add02;
                    LabelCSDistrict.Text = _vehinsList[0].Svit_district;
                    LabelCSCity.Text = _vehinsList[0].Svit_city;
                    LabelCSProvince.Text = _vehinsList[0].Svit_province;
                    LabelCSContact.Text = _vehinsList[0].Svit_contact;
                }
                hdnGetConfirm.Value = "1";
            }
        }

        protected void DropDownListCSRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewCSPreviousClaim.DataSource = CHNLSVC.General.GetClaimDetails("CustomerSettlementGrid", DropDownListCSRegNo.SelectedValue, DateTime.MinValue);
            GridViewCSPreviousClaim.DataBind();
        }

        protected void GridViewPreviousClaims_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool b = Convert.ToBoolean(((Label)e.Row.FindControl("LabelStatus")).Text);
                if (b)
                {
                    ((ImageButton)e.Row.FindControl("imgSelect")).Visible = false;
                }
            }
        }

        protected void GridViewPreviousClaims_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime accdate = Convert.ToDateTime(GridViewPreviousClaims.Rows[GridViewPreviousClaims.SelectedIndex].Cells[2].Text);
            string vehNo = GridViewPreviousClaims.Rows[GridViewPreviousClaims.SelectedIndex].Cells[3].Text;
            List<VehicalInsuranceClaim> claimList = CHNLSVC.General.GetClaimDetails("RecieveDocument", vehNo, accdate);
            if (claimList != null)
            {
                AccDate = accdate;
                VehNo = vehNo;
                TextBoxDriverName.Text = claimList[0].Dri_name;
                TextBoxLicenceNo.Text = claimList[0].Dl_lic;
                TextBoxAccDate.Text = claimList[0].Acc_date.ToShortDateString();
                hdnGetConfirm.Value = "1";
            }
        }

        protected void CheckBoxRegNo_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxRegNo.Checked)
                DivVeh.Visible = true;
            else
                DivVeh.Visible = false;
        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceDetails("IssCer", TextBoxRNo.Text);
            if (_vehins != null)
            {
                _vehins[0].Svit_no_of_prnt = _vehins[0].Svit_no_of_prnt + 1;
                CHNLSVC.General.SaveVehicalInsurance(_vehins[0]);

                //print method
                GlbCoverNoteNo = GridViewSearch.Rows[GridViewSearch.SelectedIndex].Cells[4].Text;
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\VehInsCoverNote.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/VehInsCoverNote.rpt";

                GlbMainPage = "~/General_Modules/VehicalInsurance.aspx";
                string Msg = "<script>window.open('../Reports_Module/Sales_Rep/CoverNotePrint.aspx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }


        //private string GethashCode(string sourceData)
        //{

        //    string Output = "";
        //    string hashCode;
        //    byte[] tmpSource;
        //    byte[] tmpHash;

        //    tmpSource = Encoding.UTF8.GetBytes(sourceData);//using ASCIIEncoding
        //    MD5 m=MD5.Create();
        //    tmpHash = m.ComputeHash(tmpSource);//using MD5 hash key Generater

        //    //Output(Length of tmpHash)    //size if the varible equal to length of the        tmpHash

        //    for (int i = 0; i < tmpHash.Length; i++)
        //    {
        //        Output = Output + "" + tmpHash[i];//Every New arry Row will Apend to end of the string 
        //    }


        //    hashCode = Output;

        //    return hashCode;
        //}

        //private string GenerateHashKey(string sorce)
        //{
        //    string hashKey="";

        //    double outNumber = 0;
        //    string HashCode = GethashCode(sorce);
        //    for (int i = 0; i < HashCode.Length; i++)
        //    {
        //        outNumber = outNumber + Convert.ToDouble(HashCode[i]);
        //    }

        //    return hashKey;

        //}

        protected void ImageButtonSDC_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanySearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxCom.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonSDPc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxPC.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }




        #region pay mode

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            //-------------------------------
            divCardDet.Visible = false;
            divCRDno.Visible = false;
            divChequeNum.Visible = false;
            divCredit.Visible = false;
            divAdvReceipt.Visible = false;
            divCreditCard.Visible = false;
            divBankDet.Visible = false;
            //-------------------------------

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;


            List<PaymentTypeRef> _case = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString());
            PaymentTypeRef _type = null;
            if (_case != null)
            {
                if (_case.Count > 0)
                    _type = _case[0];
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Payment types are not properly setup!");
                return;
            }
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true; divAdvReceipt.Visible = false;
            }
            else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
            {
                //divCredit.Visible = false; 
                divAdvReceipt.Visible = true;
            }
            else
            {
                //divCredit.Visible = false; 
                //divAdvReceipt.Visible = false;

            }
            if (ddlPayMode.SelectedValue == "CHEQUE")
            {
                //divCRDno.Visible = false;
                divChequeNum.Visible = true;
                divBankDet.Visible = true;
            }
            else
            {
                //divChequeNum.Visible = false;
                //  divCRDno.Visible = true;
            }
            if (ddlPayMode.SelectedValue == "CRCD")
            {
                divCRDno.Visible = true;
                divCardDet.Visible = true;
                divCreditCard.Visible = true;
                divBankDet.Visible = true;
            }
            if (ddlPayMode.SelectedValue == "DEBT")
            {
                divCRDno.Visible = true;
                divCardDet.Visible = false;
                divCreditCard.Visible = true;
                divBankDet.Visible = true;
            }
            Decimal BankOrOtherCharge = 0;
            BankOrOther_Charges = 0;
            AmtToPayForFinishPayment = 0;

            foreach (PaymentType pt in PaymentTypes)
            {
                if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                {
                    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    BankOrOtherCharge = BalanceAmount * BCR / 100;

                    Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                    BankOrOtherCharge = BankOrOtherCharge + BCV;

                    BankOrOther_Charges = BankOrOtherCharge;
                }
            }

            //-----------------**********
            AmtToPayForFinishPayment = (BankOrOtherCharge + BalanceAmount);
            txtPayAmount.Text = AmtToPayForFinishPayment.ToString();

            //-----------------**********
            txtPayAmount.Focus();

            //---------------

            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayCrPeriod.Text = "";
            txtPayAdvReceiptNo.Text = "";
            txtPayCrBatchNo.Text = "";
        }

        protected void AddPayment(object sender, EventArgs e)
        {
            try
            {
                Decimal d = Convert.ToDecimal(txtPayAmount.Text.Trim());
            }
            catch (Exception)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid amount!");
                return;
            }
            Decimal sum_receipt_amt = Convert.ToDecimal(TextBoxSeDeTot.Text);
            
            Decimal BankOrOtherCharge_ = 0;
            if (AmtToPayForFinishPayment != Convert.ToDecimal(txtPayAmount.Text))
            {
                foreach (PaymentType pt in PaymentTypes)
                {
                    if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                    {
                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        //   Convert.ToDecimal(txtPayAmount.Text) - BCV;
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge_ = (Convert.ToDecimal(txtPayAmount.Text) - BCV) * BCR / (BCR + 100);
                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                        BankOrOther_Charges = BankOrOtherCharge_;
                    }
                }
            }

            if ((PaidAmount + Convert.ToDecimal(txtPayAmount.Text.Trim()) - BankOrOther_Charges) > Math.Round(sum_receipt_amt, 2))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot Exceed Receipt Total Amount ");
                return;
            }
            Decimal bankorother = BankOrOther_Charges;
            AddPayment();
            set_PaidAmount();
            set_BalanceAmount();
        }

        private void AddPayment()
        {
            //ADDED BY SACHITH
            //TO BLOCK RECIRT ADD AND REMOVE
            Panel5.Enabled = false;

            //END


            if (_recieptItem == null || _recieptItem.Count == 0)
            {
                _recieptItem = new List<RecieptItem>();
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }



            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPayCrPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtPayCrPeriod.Text) <= 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                            return;
                        }
                    }
                    catch
                    {
                        _period = 0;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPayCrPeriod.Text)) _period = 0;
            else _period = Convert.ToInt32(txtPayCrPeriod.Text);


            if (string.IsNullOrEmpty(txtPayAmount.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                        return;
                    }
                }
                catch
                {
                    _payAmount = 0;
                }
            }


            //  _payAmount = Convert.ToDecimal(txtPayAmount.Text);
            _payAmount = Convert.ToDecimal(txtPayAmount.Text) - BankOrOther_Charges;

            //if (_recieptItem.Count <= 0)
            //{
            RecieptItem _item = new RecieptItem();
            if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
            { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

            string _cardno = string.Empty;
            //if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE") _cardno = txtPayCrCardNo.Text.Trim();
            if (ddlPayMode.SelectedValue.ToString() == "CRCD")
            {
                if (txtPayCrCardNo.Text.Trim() == "" || txtPayCrCardType.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Card Details.");
                    return;
                }
                _cardno = txtPayCrCardNo.Text.Trim();
                _item.Sard_chq_bank_cd = _cardno;


            }
            if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
            { _cardno = txtPayAdvReceiptNo.Text; _item.Sard_chq_bank_cd = txtPayCrBank.Text; }
            if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (txtChequeNo.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Cheque Details.");
                    return;
                }
                _cardno = txtChequeNo.Text.Trim();
                //_item.Sard_chq_bank_cd = _cardno;
                _item.Sard_ref_no = _cardno;
            }

            if (ddlPayMode.SelectedValue.ToString() == "DEBT" || ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (txtPayCrBank.Text.Trim() == "" || txtPayCrBranch.Text.Trim() == "")
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Bank Details.");
                    return;
                }
                //validate bank and branch.
                Boolean valid = CHNLSVC.Sales.validateBank_and_Branch(txtPayCrBank.Text.Trim(), txtPayCrBranch.Text.Trim(), "BANK");
                if (valid == false)
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "INVALID BANK DETAILS!");
                    return;
                }
            }
            _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
            _item.Sard_cc_period = _period;
            _item.Sard_cc_tp = txtPayCrCardType.Text;
            _item.Sard_chq_bank_cd = txtPayCrBank.Text;
            _item.Sard_chq_branch = txtPayCrBranch.Text;
            _item.Sard_credit_card_bank = null;
            _item.Sard_deposit_bank_cd = null;
            _item.Sard_deposit_branch = null;
            _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
            _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
            _item.Sard_anal_3 = BankOrOther_Charges;
            // _paidAmount += _payAmount;

            _item.Sard_receipt_no = "";//To be filled when saving.
            _item.Sard_ref_no = _cardno;
            _recieptItem.Add(_item);


            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            clearPaymetnScreen();
        }

        private void set_PaidAmount()
        {
            PaidAmount = 0;
            if (gvPayment.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPayment.Rows)
                {
                    //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    Decimal amt = Convert.ToDecimal(gvr.Cells[18].Text.Trim());
                    PaidAmount = PaidAmount + amt;
                }
            }
            lblPayPaid.Text = PaidAmount.ToString();
        }

        private void set_BalanceAmount()
        {
            BalanceAmount = 0;
            BalanceAmount = Convert.ToDecimal(TextBoxSeDeTot.Text) - Math.Abs(PaidAmount);
            lblPayBalance.Text = BalanceAmount.ToString();
        }

        private void clearPaymetnScreen()
        {
            txtPayAmount.Text = "";
            ddlPayMode.SelectedIndex = 0;
            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayCrPeriod.Text = "";
            txtPayAdvReceiptNo.Text = "";
            txtPayCrBatchNo.Text = "";
        }

        protected void ImgBtnBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void gvPayment_OnDelete(object sender, GridViewDeleteEventArgs e)
        {

            if (_recieptItem == null || _recieptItem.Count == 0) return;

            int row_id = e.RowIndex;
            string _payType = (string)gvPayment.DataKeys[row_id][0];
            decimal _settleAmount = (decimal)gvPayment.DataKeys[row_id][1];

            List<RecieptItem> _temp = new List<RecieptItem>();
            _temp = _recieptItem;


            _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
            _recieptItem = _temp;

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            //ADDED BY SACHITH
            //IF NO RECORD SET ENABILITY
            if (_recieptItem.Count <= 0)
                Panel5.Enabled = true;

            set_PaidAmount();
            set_BalanceAmount();
        }

        protected void BindPaymentType(DropDownList _ddl)
        {
            //try {
            //   DateTime receiptDT= Convert.ToDateTime(txtReceiptDate.Text).Date;
            //}
            //catch(Exception ex){
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Receipt date!");
            //    return;
            //}
            _ddl.Items.Clear();
            //  List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(string.Empty);
            //   List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", Convert.ToDateTime(txtReceiptDate.Text).Date);
            //TODO:SET PAY TYPE FOR VEHICAL INSURANCE
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "VHINS", DateTime.Now.Date);
            // _ddl.DataSource = _paymentTypeRef.OrderBy(items => items.Sapt_cd);
            //_ddl.DataTextField = "Sapt_cd";
            //_ddl.DataValueField = "Sapt_cd";
            List<string> payTypes = new List<string>();
            payTypes.Add("");
            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            {
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    payTypes.Add(pt.Stp_pay_tp);
                }
            }
            _ddl.DataSource = payTypes;
            _ddl.DataBind();
        }

        #endregion  

        protected void CheckBoxAcc_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAcc.Checked)
            {
                DivAcc.Visible = true;
                TextBoxAcc.Text = "";
            }
            else {
                DivAcc.Visible = false;
                TextBoxAcc.Text = "";
            
            }
        }

        protected void CheckBoxDAcc_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxDAcc.Checked)
            {
                DivDAcc.Visible = true;
                TextBoxDAcc.Text = "";
            }
            else
            {
                DivDAcc.Visible = false;
                TextBoxDAcc.Text = "";

            }
        }



        
    }
}
