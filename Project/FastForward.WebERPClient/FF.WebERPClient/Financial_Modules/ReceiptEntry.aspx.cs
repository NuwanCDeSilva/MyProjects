using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using System.Globalization;
using System.Transactions;
using FF.WebERPClient.UserControls;

namespace FF.WebERPClient.Financial_Modules
{
    public partial class ReceiptEntry : BasePage
    {
        //protected static List<RecieptItem> _RecieptDetails = new List<RecieptItem>();
        protected static MasterBusinessCompany _businessEntity = new MasterBusinessCompany();
        protected static MasterBankAccount _bankAccounts = new MasterBankAccount();


        protected HpAccount _HpAccount
        {
            get { return (HpAccount)ViewState["_HpAccount"]; }
            set { ViewState["_HpAccount"] = value; }
        }

        protected Int32 _insuTerm
        {
            get { return (Int32)ViewState["_insuTerm"]; }
            set { ViewState["_insuTerm"] = value; }
        }

        protected Int32 _colTerm
        {
            get { return (Int32)ViewState["_colTerm"]; }
            set { ViewState["_colTerm"] = value; }
        }

        protected string _InvNo
        {
            get { return (string)ViewState["_InvNo"]; }
            set { ViewState["_InvNo"] = value; }
        }

        protected decimal _insuAmt
        {
            get { return (decimal)ViewState["_insuAmt"]; }
            set { ViewState["_insuAmt"] = value; }
        }

        protected decimal _regAmt
        {
            get { return (decimal)ViewState["_regAmt"]; }
            set { ViewState["_regAmt"] = value; }
        }

        protected string _invType
        {
            get { return (string)ViewState["_invType"]; }
            set { ViewState["_invType"] = value; }
        }

        protected string _accNo
        {
            get { return (string)ViewState["_accNo"]; }
            set { ViewState["_accNo"] = value; }
        }

        protected List<HpSheduleDetails> _sheduleDetails
        {
            get { return (List<HpSheduleDetails>)ViewState["_sheduleDetails"]; }
            set { ViewState["_sheduleDetails"] = value; }
        }

        protected HpSchemeDetails _SchemeDetails
        {
            get { return (HpSchemeDetails)ViewState["_SchemeDetails"]; }
            set { ViewState["_SchemeDetails"] = value; }
        }

        protected List<RecieptItem> _RecieptDetails
        {
            get { return (List<RecieptItem>)ViewState["_RecieptDetails"]; }
            set { ViewState["_RecieptDetails"] = value; }
        }

        //ReptPickSerials
        protected List<ReptPickSerials> _ResList
        {
            get { return (List<ReptPickSerials>)ViewState["_ResList"]; }
            set { ViewState["_ResList"] = value; }
        }

        protected List<VehicalRegistration> _regList
        {
            get { return (List<VehicalRegistration>)ViewState["_regList"]; }
            set { ViewState["_regList"] = value; }
        }

        protected List<VehicleInsuarance> _insList
        {
            get { return (List<VehicleInsuarance>)ViewState["_insList"]; }
            set { ViewState["_insList"] = value; }
        }


        protected decimal GrndTotal
        {
            get { return (decimal)ViewState["GrndTotal"]; }
            set { ViewState["GrndTotal"] = value; }
        }

        protected Int32 _lineNo
        {
            get { return (Int32)ViewState["_lineNo"]; }
            set { ViewState["_lineNo"] = value; }
        }

        protected static MasterBusinessEntity _masterBusinessCompany = null;


        //protected static Int32 _lineNo = 0;
        //protected static decimal GrndTotal = 0;
        protected static Boolean _IsRecall = false;
        protected static Boolean _RecStatus = false;
        protected static Boolean _sunUpload = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtType.Attributes.Add("onKeyup", "return clickButton(event,'" + imgtypesearch.ClientID + "')");
                txtCusCode.Attributes.Add("onKeyup", "return clickButton(event,'" + imgcusSearch.ClientID + "')");
                txtBank.Attributes.Add("onKeyup", "return clickButton(event,'" + ImgBankSearch.ClientID + "')");
                txtDBank.Attributes.Add("onKeyup", "return clickButton(event,'" + Img.ClientID + "')");
                txtInvoice.Attributes.Add("onKeyup", "return clickButton(event,'" + imgInvSearch.ClientID + "')");
                txtDivision.Attributes.Add("onKeyup", "return clickButton(event,'" + imgSearchDiv.ClientID + "')");

                txtCusCode.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCustomer, ""));
                txtType.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnType, ""));
                txtBank.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnBank, ""));
                txtDBank.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnAcc, ""));
                txtRecNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnRecNo, ""));
                txtInvoice.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnOutInv, ""));
                txtDivision.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnDiv, ""));
                txtAmount.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnAmount, ""));
                ddlPayMode.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPayType, ""));
                ddlDistrict.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnDistrict, ""));
                txtMob.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnMob, ""));
                txtNIC.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnNIC, ""));
                txtItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItem, ""));
                txtEngine.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnSerial, ""));
                txtchasis.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnChasis, ""));
                txtInvItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInvItem, ""));
                txtInvEngine.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInvEngine, ""));
                txtInsCom.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnInsuCom, ""));
                txtPolicy.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPolicy, ""));
                txtRefNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnManual, ""));


                txtDate.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtRefNo.ClientID + "').focus();return false;}} else {return true}; ");
                txtRefNo.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtCusCode.ClientID + "').focus();return false;}} else {return true}; ");
                txtCusName.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtCusAdd1.ClientID + "').focus();return false;}} else {return true}; ");
                txtCusAdd1.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtCusAdd2.ClientID + "').focus();return false;}} else {return true}; ");
                txtCusAdd2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtNIC.ClientID + "').focus();return false;}} else {return true}; ");
                txtInvoice.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtInvAmt.ClientID + "').focus();return false;}} else {return true}; ");
                txtInvAmt.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + ddlPayMode.ClientID + "').focus();return false;}} else {return true}; ");
                ddlPayMode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAmount.ClientID + "').focus();return false;}} else {return true}; ");
                ddlRefType.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtRef.ClientID + "').focus();return false;}} else {return true}; ");
                txtRef.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtBank.ClientID + "').focus();return false;}} else {return true}; ");
                txtDBranch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnAdd.ClientID + "').focus();return false;}} else {return true}; ");
                txtAmount.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtAmount.ClientID + "').focus();return false;}} else {return true}; ");
                txtBranch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtBranch.ClientID + "').focus();return false;}} else {return true}; ");
                ddlDistrict.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtProvince.ClientID + "').focus();return false;}} else {return true}; ");
                txtProvince.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtProvince.ClientID + "').focus();return false;}} else {return true}; ");

                txtType.Attributes.Add("onkeypress", "uppercase();");
                txtCusCode.Attributes.Add("onkeypress", "uppercase();");
                txtInvoice.Attributes.Add("onkeypress", "uppercase();");
                txtBank.Attributes.Add("onkeypress", "uppercase();");
                txtDBank.Attributes.Add("onkeypress", "uppercase();");
                txtDivision.Attributes.Add("onkeypress", "uppercase();");
                txtItem.Attributes.Add("onkeypress", "uppercase();");
                txtInsCom.Attributes.Add("onkeypress", "uppercase();");
                txtPolicy.Attributes.Add("onkeypress", "uppercase();");

                this.Clear_Data();

                IsAllowBackDateForModule(GlbUserComCode, string.Empty,GlbUserDefProf, Page, string.Empty, imgRequestDate,lblDispalyInfor);
                _ResList = new List<ReptPickSerials>();
                _regList = new List<VehicalRegistration>();
                _RecieptDetails = new List<RecieptItem>();
                _insList = new List<VehicleInsuarance>();
                _HpAccount = new HpAccount();
                _SchemeDetails = new HpSchemeDetails();
                _sheduleDetails = new List<HpSheduleDetails>();
                _accNo = "";
                _invType = "";
                _insuAmt = 0;
                _insuTerm = 0;
                _regAmt = 0;
                _colTerm = 0;
                GrndTotal = 0;
                _lineNo = 0;
                _InvNo = "";
                
            }
        }


        private void Clear_Data()
        {
            imgRequestDate.Visible = false;
            txtType.Text = "";
            txtDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            txtDate.Enabled = false;
            txtRef.Text = "";
            txtRecNo.Text = "";
            lblBankName.Text = "";
            lblDBankDesc.Text = "";
            txtDBank.Text = "";
            txtDBranch.Text = "";
            txtRemarks.Text = "";
            txtDivision.Text = "";
            txtCusCode.Text = "";
            txtCusName.Text = "";
            txtCusAdd1.Text = "";
            txtCusAdd2.Text = "";
            txtInvoice.Text = "";
            txtInvAmt.Text = "";
            txtRefNo.Text = "";
            txtNIC.Text = "";
            txtMob.Text = "";
            txtProvince.Text = "";
            txtTotal.Text = "";
            txtInvEngine.Text = "";
            txtInvChasis.Text = "";
            txtInvItem.Text = "";
            txtInvoice.Text = "";
            txtEngine.Visible = false;
            txtchasis.Visible = false;
            lblEngine.Visible = false;
            lblChasis.Visible = false;
            txtInvoice.Enabled = false;
            imgEngine.Visible = false;
            imgChasis.Visible = false;
            txtInvAmt.Enabled = false;
            lblModel.Visible = false;
            lblDesc.Visible = false;
            Label10.Visible = false;
            Label11.Visible = false;
            btnAddSerial.Visible = false;
            imgInvSearch.Enabled = false;
            lblInvItem.Visible = false;
            txtInvItem.Visible = false;
            imgInvItem.Visible = false;
            lblInvEngine.Visible = false;
            txtInvEngine.Visible = false;
            imgInvEngine.Visible = false;
            lblInvChasis.Visible = false;
            txtInvChasis.Visible = false;
            imgInvChasis.Visible = false;
            txtPolicy.Visible = false;
            lblPolicy.Visible = false;
            imgPol.Visible = false;
            lblInsCom.Visible = false;
            txtInsCom.Visible = false;
            imgInsCom.Visible = false;
            chkIsManual.Visible = true;
            chkIsManual.Checked = false;
            chkIsManual.Enabled = true;
            chkAnnual.Checked = false;
            chkAnnual.Visible = false;
            txtItem.Text = "";
            lblModel.Text = "";
            lblDesc.Text = "";
            _invType = "";
            _accNo = "";
            _insuTerm = 0;
            _insuAmt = 0;
            GrndTotal = 0;
            _lineNo = 0;
            _InvNo = "";
            btnSave.Enabled = true;
            btnCancel.Enabled = false;
            BankDetailsDisable();
            BindDistrict(ddlDistrict);
            DataTable _Itemtable = new DataTable();
            gvRecDetails.DataSource = _Itemtable;
            gvRecDetails.DataBind();

            gvSerial.DataSource = _Itemtable;
            gvSerial.DataBind();

            gvReg.DataSource = _Itemtable;
            gvReg.DataBind();

            gvInsu.DataSource = _Itemtable;
            gvInsu.DataBind();
            
            txtType.Focus();
        }

        private void BankDetailsDisable()
        {
            //  ddlRefType.Text = "";
            txtRef.Text = "";
            txtBank.Text = "";
            txtBranch.Text = "";

            ddlRefType.Enabled = false;
            txtRef.Enabled = false;
            txtBank.Enabled = false;
            txtBranch.Enabled = false;
            ImgBankSearch.Enabled = false;
        }

        private void BankDetailsEnable()
        {
            // ddlRefType.Text = "";
            txtRef.Text = "";
            txtBank.Text = "";
            txtBranch.Text = "";


            ddlRefType.Enabled = true;
            txtRef.Enabled = true;
            txtBank.Enabled = true;
            txtBranch.Enabled = true;
            ImgBankSearch.Enabled = true;
        }

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutstandingInv:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + txtCusCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        if (string.IsNullOrEmpty(txtCusCode.Text))
                        {
                            paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator + txtCusCode.Text.Trim() + seperator);
                            break;
                        }
                    }

                case CommonUIDefiniton.SearchUserControlType.Division:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        if (txtType.Text == "VHREG")
                        {
                            paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtInvItem.Text.Trim() + seperator);
                            break;
                        }
                        else if (txtType.Text == "VHINS")
                        {
                            paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtInvItem.Text.Trim() + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtItem.Text.Trim() + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.DeliverdSerials:
                    {

                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtInvoice.Text.Trim() + seperator + txtInvItem.Text.Trim() + seperator);
                        break;

                    }

                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(txtInvoice.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion


        protected void imgPol_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInsuPolicy(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPolicy.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }


        protected void imgInsCom_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInsuCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInsCom.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgcusSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCusCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgInvItem_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoice.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select related invoice #.");
                txtInvItem.Text = "";
                txtInvoice.Focus();
                return;
            }

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceItem(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void CheckValidDivision(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDivision.Text))
            {
                if (!CHNLSVC.Sales.IsValidDivision(GlbUserComCode, GlbUserDefProf, txtDivision.Text.Trim()))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid division.");
                    txtDivision.Text = "";
                    txtDivision.Focus();
                    return;
                }
            }
            txtDate.Focus();
        }

        protected void GetOutInvAmt(object sender, EventArgs e)
        {
            if (txtType.Text == "DEBT")
            {
                if (!string.IsNullOrEmpty(txtInvoice.Text))
                {
                    if (string.IsNullOrEmpty(txtInvoice.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select invoice #.");
                        txtInvoice.Text = "";
                        txtInvoice.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select customer.");
                        txtCusCode.Text = "";
                        txtCusCode.Focus();
                        return;
                    }

                    //check valid invoice
                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(GlbUserComCode, GlbUserDefProf, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtDate.Text, txtDate.Text);

                    foreach (InvoiceHeader _tempInv in _invHdr)
                    {
                        if (_tempInv.Sah_inv_no == null)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid invoice.");
                            txtInvoice.Text = "";
                            txtInvoice.Focus();
                            return;
                        }
                    }

                    txtInvAmt.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmt(GlbUserComCode, GlbUserDefProf, txtCusCode.Text, txtInvoice.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);
                }
            }
            else if ((txtType.Text == "VHREG") || (txtType.Text == "VHINS"))
            {
                txtInvAmt.Text = "0.00";
                //check valid invoice
                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(GlbUserComCode, GlbUserDefProf, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", txtDate.Text, txtDate.Text);

                foreach (InvoiceHeader _tempInv in _invHdr)
                {
                    if (_tempInv.Sah_inv_no == null)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid invoice.");
                        txtInvoice.Text = "";
                        _invType = "";
                        txtInvoice.Focus();
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtCusCode.Text))
                        {
                            txtCusCode.Text = _tempInv.Sah_cus_cd;
                            LoadCustomerDetails();
                            //this.Page.ClientScript.GetPostBackEventReference(this.btnCustomer, "");

                        }

                        _invType = _tempInv.Sah_inv_tp;
                        _accNo = _tempInv.Sah_anal_2;
                    }
                }

            }
            else
            {
                txtInvAmt.Text = "0.00";
            }
        }

        protected void CheckValidType(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtType.Text))
            {
                if (!CHNLSVC.Sales.IsValidReceiptType(GlbUserComCode, txtType.Text.Trim()))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid receipt type.");
                    txtType.Text = "";
                    txtType.Focus();
                    return;
                }

                MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                _RecDiv = CHNLSVC.Sales.GetDefRecDivision(GlbUserComCode, GlbUserDefProf);
                if (_RecDiv.Msrd_cd != null)
                {
                    txtDivision.Text = _RecDiv.Msrd_cd;
                }
                else
                {
                    txtDivision.Text = "";
                }

                if (txtType.Text == "DEBT")
                {
                    txtInvoice.Enabled = true;
                    txtInvAmt.Enabled = true;
                    imgInvSearch.Enabled = true;
                    lblInvItem.Visible = false;
                    txtInvItem.Visible = false;
                    imgInvItem.Visible = false;
                    lblInvEngine.Visible = false;
                    txtInvEngine.Visible = false;
                    imgInvEngine.Visible = false;
                    lblInvChasis.Visible = false;
                    txtInvChasis.Visible = false;
                    lblInsCom.Visible = false;
                    lblPolicy.Visible = false;
                    txtInsCom.Visible = false;
                    txtPolicy.Visible = false;
                    imgInsCom.Visible = false;
                    imgPol.Visible = false;
                    chkAnnual.Visible = false;
                }
                else
                {
                    chkIsManual.Visible = true;
                    txtInvoice.Enabled = false;
                    txtInvAmt.Enabled = false;
                    imgInvSearch.Enabled = false;
                    lblInvItem.Visible = false;
                    txtInvItem.Visible = false;
                    imgInvItem.Visible = false;
                    lblInvEngine.Visible = false;
                    txtInvEngine.Visible = false;
                    imgInvEngine.Visible = false;
                    lblInvChasis.Visible = false;
                    txtInvChasis.Visible = false;
                    lblInsCom.Visible = false;
                    lblPolicy.Visible = false;
                    txtInsCom.Visible = false;
                    txtPolicy.Visible = false;
                    imgInsCom.Visible = false;
                    imgPol.Visible = false;
                    chkAnnual.Visible = false;
                }

                if (txtType.Text == "VHREG")
                {
                    txtInvoice.Enabled = true;
                    txtInvAmt.Enabled = true;
                    imgInvSearch.Enabled = true;
                    lblInvItem.Visible = true;
                    txtInvItem.Visible = true;
                    imgInvItem.Visible = true;
                    lblInvEngine.Visible = true;
                    txtInvEngine.Visible = true;
                    imgInvEngine.Visible = true;
                    lblInvChasis.Visible = true;
                    txtInvChasis.Visible = true;
                    lblInsCom.Visible = false;
                    lblPolicy.Visible = false;
                    txtInsCom.Visible = false;
                    txtPolicy.Visible = false;
                    imgInsCom.Visible = false;
                    imgPol.Visible = false;
                    chkDeliverItem.Visible = true;
                    chkAnnual.Visible = false;
                    //imgInvChasis.Visible = true;
                    //txtEngine.Visible = true;
                    //txtchasis.Visible = true;
                    //txtItem.Visible = true;
                    //Label7.Visible = true;
                    //Label8.Visible = true;
                    //Label9.Visible = true;
                    //imgEngine.Visible = true;
                }
                else if (txtType.Text == "VHINS")
                {
                    txtInvoice.Enabled = true;
                    txtInvAmt.Enabled = true;
                    imgInvSearch.Enabled = true;
                    lblInvItem.Visible = true;
                    txtInvItem.Visible = true;
                    imgInvItem.Visible = true;
                    lblInvEngine.Visible = true;
                    txtInvEngine.Visible = true;
                    imgInvEngine.Visible = true;
                    lblInvChasis.Visible = true;
                    txtInvChasis.Visible = true;
                    lblInsCom.Visible = true;
                    lblPolicy.Visible = true;
                    txtInsCom.Visible = true;
                    txtPolicy.Visible = true;
                    imgInsCom.Visible = true;
                    imgPol.Visible = true;
                    chkDeliverItem.Visible = true;
                    
                }
                else if (txtType.Text == "ADVAN")
                {
                    chkIsManual.Visible = true;
                    chkIsManual.Checked = true;
                    chkIsManual.Enabled = false;
                    txtCusCode.Text = "CASH";
                }
                //else
                //{
                //    txtInvoice.Enabled = false;
                //    txtInvAmt.Enabled = false;
                //    imgInvSearch.Enabled = false;
                //    lblInvItem.Visible = false;
                //    txtInvItem.Visible = false;
                //    imgInvItem.Visible = false;
                //    lblInvEngine.Visible = false;
                //    txtInvEngine.Visible = false;
                //    imgInvEngine.Visible = false;
                //    lblInvChasis.Visible = false;
                //    txtInvChasis.Visible = false;
                //    lblInsCom.Visible = false;
                //    lblPolicy.Visible = false;
                //    txtInsCom.Visible = false;
                //    txtPolicy.Visible = false;
                //    imgInsCom.Visible = false;
                //    imgPol.Visible = false;
                //    //txtEngine.Visible = false;
                //    //txtchasis.Visible = false;
                //    //txtItem.Visible = false;
                //    //Label7.Visible = false;
                //    //Label8.Visible = false;
                //    //Label9.Visible = false;
                //    //imgEngine.Visible = false;
                //}

                BindPaymentType(ddlPayMode);
            }
            txtDivision.Focus();
        }


        protected void GetAccDetails(object sender, EventArgs e)
        {
            _bankAccounts = new MasterBankAccount();
            if (!string.IsNullOrEmpty(txtDBank.Text))
            {
                _bankAccounts = CHNLSVC.Sales.GetBankDetails(GlbUserComCode, "", txtDBank.Text);

                if (_bankAccounts.Msba_acc_cd != null)
                {
                    txtDBank.Text = _bankAccounts.Msba_acc_cd;
                    lblDBankDesc.Text = _bankAccounts.Msba_acc_desc;
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid account.");
                    txtDBank.Text = "";
                    lblDBankDesc.Text = "";
                    txtDBank.Focus();
                    return;
                }
            }
            txtDBranch.Focus();
        }

        protected void GetSaveReceipt(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRecNo.Text))
            {
                LoadSaveReceipt();
            }

        }

        protected void ValidateAmount(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAmount.Text))
            {
                txtAmount.Text = Convert.ToDecimal(txtAmount.Text).ToString("0,0.00", CultureInfo.InvariantCulture);


                //if (Convert.ToDecimal(txtInvAmt.Text) > 0)
                if (!string.IsNullOrEmpty(txtInvAmt.Text))
                {
                    if (Convert.ToDecimal(txtAmount.Text) > Convert.ToDecimal(txtInvAmt.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Amount cannot be greter than to invoice amount.");
                        txtAmount.Text = "";
                        txtAmount.Focus();
                        return;
                    }
                }
            }
        }

        protected void CheckValidNIC(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                if (!IsValidNIC(txtNIC.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid NIC"); txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
            }
            txtMob.Focus();
        }

        protected void CheckValidMob(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                if (!IsValidMobileOrLandNo(txtMob.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid mobile"); txtMob.Text = "";
                    txtMob.Focus();
                    return;
                }
            }
            ddlDistrict.Focus();
        }

        protected void GetBankDetails(object sender, EventArgs e)
        {
            //_bankAccounts = new MasterBankAccount();
            //if (!string.IsNullOrEmpty(txtBank.Text))
            //{
            //    _bankAccounts = CHNLSVC.Sales.GetBankDetails(GlbUserComCode, txtBank.Text, "");

            //    if (_bankAccounts.Msba_cd != null)
            //    {
            //        txtBank.Text = _bankAccounts.Msba_cd;
            //        lblBankName.Text = _bankAccounts.Msba_desc;
            //    }
            //    else
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank.");
            //        txtBank.Text = "";
            //        lblBankName.Text = "";
            //        txtBank.Focus();
            //        return;
            //    }
            //}
            MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
            _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtBank.Text.Trim(), "BANK");

            if (_OutPartyDetails.Mbi_cd != null)
            {
                txtBank.Text = _OutPartyDetails.Mbi_cd;
                lblBankName.Text = _OutPartyDetails.Mbi_desc;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank.");
                txtBank.Text = "";
                lblBankName.Text = "";
                txtBank.Focus();
                return;
            }

            txtBranch.Focus();
        }

        private void LoadCustomerDetails()
        {
            _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCusCode.Text.Trim(), string.Empty, string.Empty, "C");


                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        txtCusName.Text = "";
                        txtCusAdd1.Text = "";
                        txtCusAdd2.Text = "";
                        txtNIC.Text = "";
                        txtMob.Text = "";
                        txtCusName.ReadOnly = false;

                    }
                    else
                    {
                        txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        txtCusName.Text = _masterBusinessCompany.Mbe_name;
                        txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                        txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                        txtMob.Text = _masterBusinessCompany.Mbe_mob;
                        txtNIC.Text = _masterBusinessCompany.Mbe_nic;

                        if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_distric_cd))
                        {

                        }
                        else
                        {
                            ddlDistrict.SelectedValue = _masterBusinessCompany.Mbe_distric_cd;
                        }

                        txtProvince.Text = _masterBusinessCompany.Mbe_province_cd;
                        txtCusName.ReadOnly = true;

                    }
                }
                else
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, txtCusCode.Text.Trim(), string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd != null)
                    {
                        if (_masterBusinessCompany.Mbe_cd == "CASH")
                        {
                            txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                            txtCusName.Text = "";
                            txtCusAdd1.Text = "";
                            txtCusAdd2.Text = "";
                            txtNIC.Text = "";
                            txtMob.Text = "";
                            txtCusName.ReadOnly = false;

                        }
                        else
                        {

                            txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                            txtCusName.Text = _masterBusinessCompany.Mbe_name;
                            txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                            txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                            txtMob.Text = _masterBusinessCompany.Mbe_mob;
                            txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                            if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_distric_cd))
                            {

                            }
                            else
                            {
                                ddlDistrict.SelectedValue = _masterBusinessCompany.Mbe_distric_cd;
                            }
                            txtProvince.Text = _masterBusinessCompany.Mbe_province_cd;
                            txtCusName.ReadOnly = true;

                        }
                    }

                    else
                    {
                        _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, string.Empty, txtCusCode.Text.Trim(), "C");

                        if (_masterBusinessCompany.Mbe_cd != null)
                        {
                            if (_masterBusinessCompany.Mbe_cd == "CASH")
                            {
                                txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                                txtCusName.Text = "";
                                txtCusAdd1.Text = "";
                                txtCusAdd2.Text = "";
                                txtNIC.Text = "";
                                txtMob.Text = "";
                                txtCusName.ReadOnly = false;

                            }
                            else
                            {

                                txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                                txtCusName.Text = _masterBusinessCompany.Mbe_name;
                                txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                                txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                                txtMob.Text = _masterBusinessCompany.Mbe_mob;
                                txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                                if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_distric_cd))
                                {

                                }
                                else
                                {
                                    ddlDistrict.SelectedValue = _masterBusinessCompany.Mbe_distric_cd;
                                }
                                txtProvince.Text = _masterBusinessCompany.Mbe_province_cd;
                                txtCusName.ReadOnly = true;

                            }
                        }
                        else
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid customer");
                            txtCusCode.Text = "";
                            txtCusName.Text = "";
                            txtCusAdd1.Text = "";
                            txtCusAdd2.Text = "";
                            txtNIC.Text = "";
                            txtMob.Text = "";
                            txtProvince.Text = "";
                            txtCusName.ReadOnly = true;
                            txtCusCode.Focus();
                            return;
                        }
                    }
                }
            }
        }

        protected void LoadCustomerDetailsByCustomer(object sender, EventArgs e)
        {
            LoadCustomerDetails();
            //_masterBusinessCompany = new MasterBusinessCompany();
            //if (!string.IsNullOrEmpty(txtCusCode.Text))
            //{
            //    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCusCode.Text.Trim(), string.Empty, string.Empty, "C");


            //    if (_masterBusinessCompany.Mbe_cd != null)
            //    {
            //        if (_masterBusinessCompany.Mbe_cd == "CASH")
            //        {
            //            txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
            //            txtCusName.Text = "";
            //            txtCusAdd1.Text = "";
            //            txtCusAdd2.Text = "";
            //            txtNIC.Text = "";
            //            txtMob.Text = "";
            //            txtCusName.ReadOnly = false;

            //        }
            //        else
            //        {
            //            txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
            //            txtCusName.Text = _masterBusinessCompany.Mbe_name;
            //            txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
            //            txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
            //            txtMob.Text = _masterBusinessCompany.Mbe_mob;
            //            txtNIC.Text = _masterBusinessCompany.Mbe_nic;

            //            if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_distric_cd))
            //            {

            //            }
            //            else
            //            {
            //                ddlDistrict.SelectedValue = _masterBusinessCompany.Mbe_distric_cd;
            //            }

            //            txtProvince.Text = _masterBusinessCompany.Mbe_province_cd;
            //            txtCusName.ReadOnly = true;

            //        }
            //    }
            //    else
            //    {
            //        _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, txtCusCode.Text.Trim(), string.Empty, "C");

            //        if (_masterBusinessCompany.Mbe_cd != null)
            //        {
            //            if (_masterBusinessCompany.Mbe_cd == "CASH")
            //            {
            //                txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
            //                txtCusName.Text = "";
            //                txtCusAdd1.Text = "";
            //                txtCusAdd2.Text = "";
            //                txtNIC.Text = "";
            //                txtMob.Text = "";
            //                txtCusName.ReadOnly = false;

            //            }
            //            else
            //            {

            //                txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
            //                txtCusName.Text = _masterBusinessCompany.Mbe_name;
            //                txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
            //                txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
            //                txtMob.Text = _masterBusinessCompany.Mbe_mob;
            //                txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            //                if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_distric_cd))
            //                {

            //                }
            //                else
            //                {
            //                    ddlDistrict.SelectedValue = _masterBusinessCompany.Mbe_distric_cd;
            //                }
            //                txtProvince.Text = _masterBusinessCompany.Mbe_province_cd;
            //                txtCusName.ReadOnly = true;

            //            }
            //        }

            //        else
            //        {
            //            _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, string.Empty, string.Empty, txtCusCode.Text.Trim(), "C");

            //            if (_masterBusinessCompany.Mbe_cd != null)
            //            {
            //                if (_masterBusinessCompany.Mbe_cd == "CASH")
            //                {
            //                    txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
            //                    txtCusName.Text = "";
            //                    txtCusAdd1.Text = "";
            //                    txtCusAdd2.Text = "";
            //                    txtNIC.Text = "";
            //                    txtMob.Text = "";
            //                    txtCusName.ReadOnly = false;

            //                }
            //                else
            //                {

            //                    txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
            //                    txtCusName.Text = _masterBusinessCompany.Mbe_name;
            //                    txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
            //                    txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
            //                    txtMob.Text = _masterBusinessCompany.Mbe_mob;
            //                    txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            //                    if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_distric_cd))
            //                    {

            //                    }
            //                    else
            //                    {
            //                        ddlDistrict.SelectedValue = _masterBusinessCompany.Mbe_distric_cd;
            //                    }
            //                    txtProvince.Text = _masterBusinessCompany.Mbe_province_cd;
            //                    txtCusName.ReadOnly = true;

            //                }
            //            }
            //            else
            //            {
            //                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid customer");
            //                txtCusCode.Text = "";
            //                txtCusName.Text = "";
            //                txtCusAdd1.Text = "";
            //                txtCusAdd2.Text = "";
            //                txtNIC.Text = "";
            //                txtMob.Text = "";
            //                txtProvince.Text = "";
            //                txtCusName.ReadOnly = true;
            //                txtCusCode.Focus();
            //                return;
            //            }
            //        }
            //    }
            //}
            txtCusName.Focus();
        }

        protected void imgtypesearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
            DataTable dataSource = CHNLSVC.CommonSearch.GetReceiptTypes(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void BindPaymentType(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            //List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(string.Empty);
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, txtType.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date);
            if (_paymentTypeRef != null)
            {
                _ddl.DataSource = _paymentTypeRef.OrderBy(items => items.Stp_pay_tp);
                _ddl.DataTextField = "Stp_pay_tp";
                _ddl.DataValueField = "Stp_pay_tp";
                _ddl.DataBind();
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

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                BankDetailsEnable();
                if (_type.Sapt_cd != "CRCD")
                {
                    ddlRefType.Enabled = false;
                }
            }
            else
            {
                BankDetailsDisable();
            }
            txtAmount.Focus();
        }

        protected void CheckPayType(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                BankDetailsEnable();
                if (_type.Sapt_cd != "CRCD")
                {
                    ddlRefType.Enabled = false;
                }
            }
            else
            {
                BankDetailsDisable();
            }
            txtAmount.Focus();
        }

        protected void GetProvince(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlDistrict.SelectedValue.ToString())) return;
            DistrictProvince _type = CHNLSVC.Sales.GetDistrict(ddlDistrict.SelectedValue.ToString())[0];
            if (_type.Mds_district == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid district."); return; }
            txtProvince.Text = _type.Mds_province;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRecNo.Text)) return;
            if (txtType.Text == "ADVAN") return;
            string _cusCode = "";
            _cusCode = txtCusCode.Text.Trim();
            MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, _cusCode, string.Empty, string.Empty, "C");


            string Msg = "";
            GlbMainPage = "~/Reports_Module/Sales_Rep/Sales_Rep.aspx";
            clearReportParameters();
            GlbDocNosList = txtRecNo.Text.Trim();


            if (_itm.Mbe_sub_tp != "C.")
            {
                GlbReportName = "ReceiptPrint";
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ReceiptPrint.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";

                Msg = "<script>window.open('../../Reports_Module/Sales_Rep/ReceiptEntryPrint.aspx','_blank');</script>";
            }
            else
            {
                GlbReportName = "ReceiptPrintDealer";
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ReceiptPrintDealer.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrintDealer.rpt";

                Msg = "<script>window.open('../../Reports_Module/Sales_Rep/ReceiptEntryPrint.aspx','_blank');</script>";
            }

            //GlbMainPage = "~/Financial_Modules/ReceiptEntry.aspx";
            // Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();
            _RecieptDetails = new List<RecieptItem>();
            _ResList = new List<ReptPickSerials>();
            _regList = new List<VehicalRegistration>();
            _insList = new List<VehicleInsuarance>();
            GrndTotal = 0;
            _IsRecall = false;
            _RecStatus = false;
            _lineNo = 0;
            _sunUpload = false;
            txtCusCode.ReadOnly = false;
            //BindPaymentType(ddlPayMode);
            btnSave.Enabled = true;
        }

        protected void BindAddItem()
        {
            gvRecDetails.DataSource = _RecieptDetails;
            gvRecDetails.DataBind();

        }

        private VehicleInsuarance AssingInsDetails()
        {
            VehicleInsuarance _tempIns = new VehicleInsuarance();
            MasterItem _itemList = new MasterItem();
            _itemList = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtInvItem.Text);

            _tempIns.Svit_seq = 1;
            _tempIns.Svit_ref_no = "na";
            _tempIns.Svit_com = GlbUserComCode;
            _tempIns.Svit_pc = GlbUserDefProf;
            _tempIns.Svit_dt = Convert.ToDateTime(txtDate.Text);
            _tempIns.Svit_inv_no = txtInvoice.Text.Trim();
            _tempIns.Svit_sales_tp = _invType;
            _tempIns.Svit_ins_com = txtInsCom.Text.Trim();
            _tempIns.Svit_ins_polc = txtPolicy.Text.Trim();
            _tempIns.Svit_ins_val = Convert.ToDecimal(txtInvAmt.Text);
            _tempIns.Svit_cust_cd = txtCusCode.Text.Trim();
            _tempIns.Svit_cust_title = "Mr.";
            _tempIns.Svit_last_name = txtCusName.Text.Trim();
            _tempIns.Svit_full_name = txtCusName.Text.Trim();
            _tempIns.Svit_initial = "";
            _tempIns.Svit_add01 = txtCusAdd1.Text.Trim();
            _tempIns.Svit_add02 = txtCusAdd2.Text.Trim();
            _tempIns.Svit_city = "";
            _tempIns.Svit_district = ddlDistrict.Text;
            _tempIns.Svit_province = txtProvince.Text.Trim();
            _tempIns.Svit_contact = txtMob.Text.Trim();
            _tempIns.Svit_model = _itemList.Mi_model;
            _tempIns.Svit_brd = _itemList.Mi_brand;
            _tempIns.Svit_chassis = txtInvChasis.Text.Trim();
            _tempIns.Svit_engine = txtInvEngine.Text.Trim();
            _tempIns.Svit_capacity = "0";
            _tempIns.Svit_cre_by = GlbUserName;
            _tempIns.Svit_cre_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_mod_by = GlbUserName;
            _tempIns.Svit_mod_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_issue = false;
            _tempIns.Svit_cvnt_no = "";
            _tempIns.Svit_cvnt_days = 0;
            _tempIns.Svit_cvnt_from_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_to_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_by = "";
            _tempIns.Svit_cvnt_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_issue = false;
            _tempIns.Svit_ext_no = "";
            _tempIns.Svit_ext_days = 0;
            _tempIns.Svit_ext_from_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_to_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_by = "";
            _tempIns.Svit_ext_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_veh_reg_no = "";
            _tempIns.Svit_reg_by = "";
            _tempIns.Svit_reg_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_polc_stus = false;
            _tempIns.Svit_polc_no = "";
            _tempIns.Svit_eff_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_expi_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_net_prem = 0;
            _tempIns.Svit_srcc_prem = 0;
            _tempIns.Svit_oth_val = 0;
            _tempIns.Svit_tot_val = 0;
            _tempIns.Svit_polc_by = "";
            _tempIns.Svit_polc_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_dbt_no = "";
            _tempIns.Svit_dbt_set_stus = false;
            _tempIns.Svit_dbt_by = "";
            _tempIns.Svit_dbt_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_veg_ref = "";
            _tempIns.Svit_itm_cd = txtInvItem.Text.Trim();
            return _tempIns;

        }


        private VehicalRegistration AssingRegDetails()
        {
            VehicalRegistration _tempReg = new VehicalRegistration();
            MasterItem _itemList = new MasterItem();
            _itemList = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtInvItem.Text);

            _tempReg.P_seq = 1;
            _tempReg.P_srvt_ref_no = "na";
            _tempReg.P_svrt_com = GlbUserComCode;
            _tempReg.P_svrt_pc = GlbUserDefProf;
            _tempReg.P_svrt_dt = Convert.ToDateTime(txtDate.Text);
            _tempReg.P_svrt_inv_no = txtInvoice.Text.Trim();
            _tempReg.P_svrt_sales_tp = _invType;
            _tempReg.P_svrt_reg_val = Convert.ToDecimal(txtInvAmt.Text);
            _tempReg.P_svrt_claim_val = _regAmt;
            _tempReg.P_svrt_id_tp = "NIC";
            _tempReg.P_svrt_id_ref = txtNIC.Text.Trim();
            _tempReg.P_svrt_cust_cd = txtCusCode.Text.Trim();
            _tempReg.P_svrt_cust_title = "Mr.";
            _tempReg.P_svrt_last_name = txtCusName.Text.Trim();
            _tempReg.P_svrt_full_name = txtCusName.Text.Trim();
            _tempReg.P_svrt_initial = "";
            _tempReg.P_svrt_add01 = txtCusAdd1.Text.Trim();
            _tempReg.P_svrt_add02 = txtCusAdd2.Text.Trim();
            _tempReg.P_svrt_city = "";
            _tempReg.P_svrt_district = ddlDistrict.Text;
            _tempReg.P_svrt_province = txtProvince.Text.Trim();
            _tempReg.P_svrt_contact = txtMob.Text.Trim();
            _tempReg.P_svrt_model = _itemList.Mi_model;
            _tempReg.P_svrt_brd = _itemList.Mi_brand;
            _tempReg.P_svrt_chassis = txtInvChasis.Text.Trim();
            _tempReg.P_svrt_engine = txtInvEngine.Text.Trim();
            _tempReg.P_svrt_color = _itemList.Mi_color_ext;
            _tempReg.P_svrt_fuel = "";
            _tempReg.P_svrt_capacity = 0;
            _tempReg.P_svrt_unit = "";
            _tempReg.P_svrt_horse_power = 0;
            _tempReg.P_svrt_wheel_base = 0;
            _tempReg.P_svrt_tire_front = "";
            _tempReg.P_svrt_tire_rear = "";
            _tempReg.P_svrt_weight = 0;
            _tempReg.P_svrt_man_year = 0;
            _tempReg.P_svrt_import = "";
            _tempReg.P_svrt_authority = "";
            _tempReg.P_svrt_country = _itemList.Mi_country_cd;
            _tempReg.P_svrt_custom_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_clear_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_declear_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_importer = "";
            _tempReg.P_svrt_importer_add01 = "";
            _tempReg.P_svrt_importer_add02 = "";
            _tempReg.P_svrt_cre_bt = GlbUserName;
            _tempReg.P_svrt_cre_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_prnt_stus = 0;
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
            _tempReg.P_srvt_itm_cd = txtInvItem.Text.Trim();
            return _tempReg;

        }

        private RecieptItem AssignDataToObject()
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            string _Amt = "";
            RecieptItem _tempItem = new RecieptItem();

            _tempItem.Sard_seq_no = 1;
            _tempItem.Sard_line_no = _lineNo;
            _tempItem.Sard_receipt_no = "1";
            _tempItem.Sard_inv_no = txtInvoice.Text;
            _tempItem.Sard_pay_tp = ddlPayMode.Text;
            _tempItem.Sard_ref_no = txtRef.Text;
            _tempItem.Sard_chq_bank_cd = txtBank.Text;
            _tempItem.Sard_chq_branch = txtBranch.Text;
            _tempItem.Sard_deposit_bank_cd = txtDBank.Text;
            _tempItem.Sard_deposit_branch = txtDBranch.Text;
            _tempItem.Sard_credit_card_bank = txtBank.Text;
            _tempItem.Sard_cc_tp = ddlRefType.Text;
            _tempItem.Sard_cc_expiry_dt = Convert.ToDateTime(txtDate.Text);
            _tempItem.Sard_cc_is_promo = false;
            _tempItem.Sard_cc_period = 0;
            _tempItem.Sard_gv_issue_loc = "";
            _tempItem.Sard_gv_issue_dt = Convert.ToDateTime(txtDate.Text);

            _Amt = Convert.ToDecimal(txtAmount.Text).ToString("0.00", CultureInfo.InvariantCulture);
            _tempItem.Sard_settle_amt = Convert.ToDecimal(_Amt);
            //Convert.ToDecimal(txtRevQty.Text)).ToString("0.00", CultureInfo.InvariantCulture)
            _tempItem.Sard_sim_ser = "";
            _tempItem.Sard_anal_1 = "";
            _tempItem.Sard_anal_2 = "";
            _tempItem.Sard_anal_3 = 0;
            _tempItem.Sard_anal_4 = 0;
            _tempItem.Sard_anal_5 = Convert.ToDateTime(txtDate.Text);

            return _tempItem;


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string _Htype = "";
            string _Hvalue = "";
            Int16 i = 0;

       

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                if (string.IsNullOrEmpty(txtRef.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter payment reference.");
                    txtRef.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtBank.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter payment bank.");
                    txtBank.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtBranch.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter payment branch.");
                    txtBranch.Focus();
                    return;
                }

                if (ddlPayMode.Text == "CRCD")
                {
                    if (string.IsNullOrEmpty(ddlRefType.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter card type.");
                        ddlRefType.Focus();
                        return;
                    }
                }

            }

            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer is missing.");
                txtCusCode.Focus();
                return;
            }

            //if (string.IsNullOrEmpty(txtDBank.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Deposit bank is missing.");
            //    txtDBank.Focus();
            //    return;
            //}

            if ((txtType.Text == "VHREG") || (txtType.Text == "DEBT") || (txtType.Text == "VHINS"))
            {


                if (Convert.ToDecimal(txtAmount.Text) > Convert.ToDecimal(txtInvAmt.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Settle amount is exceed.");
                    txtAmount.Text = "";
                    txtAmount.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtProvince.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select district & province.");
                    txtProvince.Focus();
                    return;
                }
            }

            if ((txtType.Text == "VHREG") || (txtType.Text == "VHINS"))
            {
                if (string.IsNullOrEmpty(txtInvEngine.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select engine #.");
                    txtInvEngine.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtInvChasis.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select chassis #.");
                    txtInvChasis.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtInvItem.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select item.");
                    txtInvItem.Focus();
                    return;
                }
            }

            if (txtType.Text == "VHINS")
            {
                if (string.IsNullOrEmpty(txtInsCom.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select insuarance company.");
                    txtInsCom.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPolicy.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select insuarance policy.");
                    txtPolicy.Focus();
                    return;
                }
            }

            if (Convert.ToDecimal(txtAmount.Text) <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Settle amount cannot be zero.");
                txtAmount.Text = "";
                txtAmount.Focus();
                return;
            }


            if ((txtType.Text != "VHREG") || (txtType.Text != "VHINS"))
            {
                foreach (RecieptItem item in _RecieptDetails)
                {
                    if (item.Sard_pay_tp == ddlPayMode.Text && item.Sard_inv_no == txtInvoice.Text && item.Sard_ref_no == txtRef.Text)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Related payment details already exsist.");
                        ddlPayMode.Focus();
                        return;
                    }
                }
            }


            //check whether selected serial is generated registration receipt
            //if (txtType.Text == "VHREG")
            //{
            //   // VehicalRegistration _isExsist = new VehicalRegistration();

            //    //_isExsist = CHNLSVC.Sales.GetRegRefBySeria
            //}

            txtInvAmt.ReadOnly = true;
            txtInvAmt.Enabled = false;

            _lineNo += 1;
            _RecieptDetails.Add(AssignDataToObject());
            BindAddItem();

            CalculateGrandTotal(Convert.ToDecimal(txtAmount.Text), true);
            Boolean _isExsist = false;

            ReptPickSerials _tempItem = new ReptPickSerials();
            if (txtType.Text == "VHREG")
            {

                foreach (VehicalRegistration temp in _regList)
                {
                    if (temp.P_srvt_itm_cd == txtInvItem.Text.Trim() && temp.P_svrt_engine == txtInvEngine.Text.Trim())
                    {
                        _isExsist = true;
                        goto L1;
                    }
                }
            L1:
                if (_isExsist == false)
                {
                    _regList.Add(AssingRegDetails());
                    gvReg.DataSource = _regList;
                    gvReg.DataBind();
                    _InvNo = txtInvoice.Text.Trim();
                    _tempItem = CHNLSVC.Inventory.GetAvailableSerIDInformation(GlbUserComCode, GlbUserDefLoca, txtInvItem.Text.Trim(), txtInvEngine.Text.Trim(), string.Empty, string.Empty);
                    _tempItem.Tus_itm_desc = "";
                    _tempItem.Tus_itm_model = "";
                    _tempItem.Tus_base_doc_no = txtInvoice.Text.Trim();
                    _ResList.Add(_tempItem);
                    gvSerial.DataSource = _ResList;
                    gvSerial.DataBind();


                }

                txtInvAmt.Text = Convert.ToDecimal(Convert.ToDecimal(txtInvAmt.Text) - Convert.ToDecimal(txtAmount.Text)).ToString("0.00");

                if (Convert.ToDecimal(txtInvAmt.Text) == 0)
                {
                    txtInvoice.Text = "";
                    txtInvAmt.Text = "";
                    BindPaymentType(ddlPayMode);
                    txtAmount.Text = "";
                    ddlRefType.Text = "";
                    txtRef.Text = "";
                    txtBank.Text = "";
                    lblBankName.Text = "";
                    txtBranch.Text = "";
                    txtDBank.Text = "";
                    txtDBranch.Text = "";
                    lblDBankDesc.Text = "";
                    txtInvItem.Text = "";
                    txtInvEngine.Text = "";
                    txtInvChasis.Text = "";
                    txtCusCode.ReadOnly = true;
                    txtInvoice.ReadOnly = false;
                    if (txtInvoice.Enabled == true)
                    {
                        txtInvoice.Focus();
                    }
                    else
                    {
                        ddlPayMode.Focus();
                    }
                }
                else
                {
                    BindPaymentType(ddlPayMode);
                    txtAmount.Text = "";
                    ddlRefType.Text = "";
                    txtRef.Text = "";
                    txtBank.Text = "";
                    lblBankName.Text = "";
                    txtBranch.Text = "";
                    txtDBank.Text = "";
                    txtDBranch.Text = "";
                    lblDBankDesc.Text = "";
                    txtInvoice.ReadOnly = true;
                    txtCusCode.ReadOnly = true;
                }
            }
            else if (txtType.Text == "VHINS")
            {

                foreach (VehicleInsuarance temp in _insList)
                {
                    if (temp.Svit_itm_cd == txtInvItem.Text.Trim() && temp.Svit_engine == txtInvEngine.Text.Trim())
                    {
                        _isExsist = true;
                        goto L2;
                    }
                }
            L2:
                if (_isExsist == false)
                {
                    _insList.Add(AssingInsDetails());
                    gvInsu.DataSource = _insList;
                    gvInsu.DataBind();
                }


                //calculate insurarance future rental
                if (_accNo != null && _accNo != "")
                {
                    Int32 _MainInsTerm = 0;
                    Int32 _SubInsTerm = 0;
                    _HpAccount = new HpAccount();
                    _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);
                    List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                    if (_Saleshir.Count > 0)
                    {
                        foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                        {
                            _Htype = _one.Mpi_cd;
                            _Hvalue = _one.Mpi_val;

                            _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_Htype, _Hvalue, 1, _HpAccount.Hpa_sch_cd);

                            if (_SchemeDetails.Hsd_cd != null)
                            {
                                if (_SchemeDetails.Hsd_veh_insu_term != null)
                                {

                                    _insuTerm = _SchemeDetails.Hsd_veh_insu_term;

                                    if (_SchemeDetails.Hsd_veh_insu_col_term != null)
                                    {
                                        _colTerm = _SchemeDetails.Hsd_veh_insu_col_term;
                                    }
                                    else
                                    {
                                        _colTerm = _insuTerm;
                                    }


                                    _MainInsTerm = _insuTerm / 12;

                                    if (_MainInsTerm > 0)
                                    {
                                        MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                        _vehIns = CHNLSVC.Sales.GetVehInsDef(GlbUserComCode, txtInvoice.Text.Trim(), txtInvItem.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf, txtInsCom.Text.Trim(), txtPolicy.Text.Trim(), 12);

                                        if (_vehIns.Ins_com_cd != null)
                                        {
                                            _insuAmt = _vehIns.Value * _MainInsTerm;
                                        }
                                        else
                                        {
                                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Insuarance definition not found for term 12.");
                                            return;
                                        }
                                    }

                                    _SubInsTerm = _insuTerm % 12;

                                    if (_SubInsTerm > 0)
                                    {
                                        if ((_SubInsTerm) <= 3)
                                        {
                                            _SubInsTerm = 3;
                                        }
                                        else if ((_SubInsTerm) <= 6)
                                        {
                                            _SubInsTerm = 6;
                                        }
                                        else if ((_SubInsTerm) <= 9)
                                        {
                                            _SubInsTerm = 9;
                                        }
                                        else
                                        {
                                            _SubInsTerm = 12;
                                        }

                                        MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                        _vehIns = CHNLSVC.Sales.GetVehInsDef(GlbUserComCode, txtInvoice.Text.Trim(), txtInvItem.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf, txtInsCom.Text.Trim(), txtPolicy.Text.Trim(), _SubInsTerm);

                                        if (_vehIns.Ins_com_cd != null)
                                        {
                                            _insuAmt = _insuAmt + _vehIns.Value;
                                        }
                                        else
                                        {
                                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Insuarance definition not found for term." + _SubInsTerm);
                                            return;
                                        }

                                    }


                                    for (int x = 1; x <= _colTerm; x++)
                                    {

                                        HpSheduleDetails _tempShedule = new HpSheduleDetails();
                                        _tempShedule.Hts_seq = 0;
                                        _tempShedule.Hts_acc_no = _accNo;
                                        _tempShedule.Hts_rnt_no = x;
                                        _tempShedule.Hts_due_dt = DateTime.Today.Date;
                                        _tempShedule.Hts_rnt_val = 0;
                                        _tempShedule.Hts_intr = 0; //double.Parse(number.ToString("####0.00"));
                                        _tempShedule.Hts_vat = 0;
                                        _tempShedule.Hts_ser = 0;
                                        _tempShedule.Hts_ins = 0;
                                        _tempShedule.Hts_sdt = 0;
                                        _tempShedule.Hts_cre_by = GlbUserName;
                                        _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                                        _tempShedule.Hts_mod_by = GlbUserName;
                                        _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                                        _tempShedule.Hts_upload = false;
                                        _tempShedule.Hts_veh_insu = _insuAmt / _colTerm;
                                        _tempShedule.Hts_tot_val = _insuAmt / _colTerm;
                                        _sheduleDetails.Add(_tempShedule);
                                    }

                                    goto L6;
                                }
                                else
                                {

                                    goto L6;
                                }

                            }
                        }
                    L6: i = 2;
                    }
                }

                txtInvAmt.Text = Convert.ToDecimal(Convert.ToDecimal(txtInvAmt.Text) - Convert.ToDecimal(txtAmount.Text)).ToString("0.00");

                if (Convert.ToDecimal(txtInvAmt.Text) == 0)
                {
                    txtInvoice.Text = "";
                    txtInvAmt.Text = "";
                    BindPaymentType(ddlPayMode);
                    txtAmount.Text = "";
                    ddlRefType.Text = "";
                    txtRef.Text = "";
                    txtBank.Text = "";
                    lblBankName.Text = "";
                    txtBranch.Text = "";
                    txtDBank.Text = "";
                    txtDBranch.Text = "";
                    lblDBankDesc.Text = "";
                    txtInvItem.Text = "";
                    txtInvEngine.Text = "";
                    txtInvChasis.Text = "";
                    txtInsCom.Text = "";
                    txtPolicy.Text = "";
                    txtCusCode.ReadOnly = true;
                    txtInvoice.ReadOnly = false;
                    if (txtInvoice.Enabled == true)
                    {
                        txtInvoice.Focus();
                    }
                    else
                    {
                        ddlPayMode.Focus();
                    }
                }
                else
                {
                    BindPaymentType(ddlPayMode);
                    txtAmount.Text = "";
                    ddlRefType.Text = "";
                    txtRef.Text = "";
                    txtBank.Text = "";
                    lblBankName.Text = "";
                    txtBranch.Text = "";
                    txtDBank.Text = "";
                    txtDBranch.Text = "";
                    lblDBankDesc.Text = "";
                    txtInvoice.ReadOnly = true;
                    txtCusCode.ReadOnly = true;
                }
            }
            else
            {

                txtInvoice.Text = "";
                txtInvAmt.Text = "";
                BindPaymentType(ddlPayMode);
                txtAmount.Text = "";
                ddlRefType.Text = "";
                txtRef.Text = "";
                txtBank.Text = "";
                lblBankName.Text = "";
                txtBranch.Text = "";
                txtDBank.Text = "";
                txtDBranch.Text = "";
                lblDBankDesc.Text = "";
                txtInvItem.Text = "";
                txtInvEngine.Text = "";
                txtInvChasis.Text = "";
                txtInsCom.Text = "";
                txtPolicy.Text = "";
                txtCusCode.ReadOnly = true;
                txtInvoice.ReadOnly = false;
                if (txtInvoice.Enabled == true)
                {
                    txtInvoice.Focus();
                }
                else
                {
                    ddlPayMode.Focus();
                }
            }
        }

        protected void ImgBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void Img_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBankAccounts(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtDBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private void CalculateGrandTotal(decimal _amt, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndTotal = GrndTotal + Convert.ToDecimal(_amt);

                txtTotal.Text = (GrndTotal).ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            else//--
            {
                GrndTotal = GrndTotal - Convert.ToDecimal(_amt);

                txtTotal.Text = (GrndTotal).ToString("0,0.00", CultureInfo.InvariantCulture);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (chkIsManual.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtRefNo.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Manual receipt no is required.");
                        txtRefNo.Focus();
                        return;
                    }
                    else
                    {
                        Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(GlbUserComCode, GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtRefNo.Text));
                        if (_IsValid == false)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                            txtRefNo.Text = "";
                            txtRefNo.Focus();
                            return;
                        }
                    }
                }

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer is missing.");
                    txtCusCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusName.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer name is missing.");
                    txtCusName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusAdd1.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Customer address is missing.");
                    txtCusAdd1.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDivision.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Division is missing.");
                    txtDivision.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtType.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Type is missing.");
                    txtType.Focus();
                    return;
                }

                if (gvRecDetails.Rows.Count == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter payment details.");
                    ddlPayMode.Focus();
                    return;
                }

                //if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtDate.Text, imgRequestDate, lblDispalyInfor) == true)
                //{
                //    throw new UIValidationException("Back date not allow for selected date!");
                //}

                if (IsAllowBackDateForModule(GlbUserComCode, string.Empty, GlbUserDefProf, Page, txtDate.Text, imgRequestDate, lblDispalyInfor) == false)
                {
                    if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Back date not allow for selected date!");
                        return;
                    }
                }



                if ((txtType.Text == "VHREG") || (txtType.Text == "VHINS"))
                {
                    if (!string.IsNullOrEmpty(txtInvAmt.Text))
                    {
                        if (Convert.ToDecimal(txtInvAmt.Text) > 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Settle amounts is missmatch with define amounts.");
                            ddlPayMode.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(txtProvince.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select district & province.");
                        txtProvince.Focus();
                        return;
                    }

                    if (txtType.Text == "VHREG")
                    {
                        if (gvReg.Rows.Count <= 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find registration details.Pls. re-process");
                            return;
                        }
                    }

                    if (txtType.Text == "VHINS")
                    {
                        if (gvInsu.Rows.Count <= 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find insuarance details.Pls. re-process");
                            return;
                        }
                    }
                }

                btnSave.Enabled = false;
                SaveReceiptHeader();
                btnSave.Enabled = true;

                _RecieptDetails = new List<RecieptItem>();
                _ResList = new List<ReptPickSerials>();
                _regList = new List<VehicalRegistration>();
                _insList = new List<VehicleInsuarance>();
                GrndTotal = 0;
                _lineNo = 0;
                _sunUpload = false;
                txtCusCode.ReadOnly = false;
                //BindPaymentType(ddlPayMode);
                this.Clear_Data();
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }
        }



        private void SaveReceiptHeader()
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            ReptPickHeader _SerHeader = new ReptPickHeader();
            List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempRegSave = new List<VehicalRegistration>();
            List<VehicleInsuarance> _tempInsSave = new List<VehicleInsuarance>();

            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, GlbUserComCode);
            _ReceiptHeader.Sar_com_cd = GlbUserComCode;
            _ReceiptHeader.Sar_receipt_type = txtType.Text.Trim();
            _ReceiptHeader.Sar_receipt_no = txtRecNo.Text.Trim();
            _ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
            _ReceiptHeader.Sar_manual_ref_no = txtRefNo.Text.Trim();
            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = GlbUserDefProf;
            _ReceiptHeader.Sar_debtor_cd = txtCusCode.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = txtCusName.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_1 = txtCusAdd1.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_2 = txtCusAdd2.Text.Trim();
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = txtMob.Text.Trim();
            _ReceiptHeader.Sar_nic_no = txtNIC.Text.Trim();
            _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(txtTotal.Text);
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
            _ReceiptHeader.Sar_remarks = txtRemarks.Text.Trim();
            _ReceiptHeader.Sar_is_used = false;
            _ReceiptHeader.Sar_ref_doc = "";
            _ReceiptHeader.Sar_ser_job_no = "";
            _ReceiptHeader.Sar_used_amt = 0;
            _ReceiptHeader.Sar_create_by = GlbUserName;
            _ReceiptHeader.Sar_mod_by = GlbUserName;
            _ReceiptHeader.Sar_session_id = GlbUserSessionID;
            _ReceiptHeader.Sar_anal_1 = ddlDistrict.Text;
            _ReceiptHeader.Sar_anal_2 = txtProvince.Text.Trim();
            if (chkIsManual.Checked == true)
            {
                _ReceiptHeader.Sar_anal_3 = "MANUAL";
            }
            else
            {
                _ReceiptHeader.Sar_anal_3 = "SYSTEM";
            }
            _ReceiptHeader.Sar_anal_4 = "";
            _ReceiptHeader.Sar_anal_5 = 0;
            _ReceiptHeader.Sar_anal_6 = 0;
            _ReceiptHeader.Sar_anal_7 = 0;
            _ReceiptHeader.Sar_anal_8 = 0;
            _ReceiptHeader.Sar_anal_9 = 0;


            List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
            foreach (RecieptItem line in _RecieptDetails)
            {
                line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
                _ReceiptDetailsSave.Add(line);
            }

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = GlbUserDefProf;
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "RECEIPT";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = txtDivision.Text.Trim();
            masterAuto.Aut_year = null;

            MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
            masterAutoRecTp.Aut_cate_cd = GlbUserDefProf;
            masterAutoRecTp.Aut_cate_tp = "PC";
            masterAutoRecTp.Aut_direction = null;
            masterAutoRecTp.Aut_modify_dt = null;
            masterAutoRecTp.Aut_moduleid = "RECEIPT";
            masterAutoRecTp.Aut_number = 5;//what is Aut_number
            masterAutoRecTp.Aut_start_char = txtType.Text.Trim();
            masterAutoRecTp.Aut_year = null;

            if (gvSerial.Rows.Count > 0)
            {
                _SerHeader.Tuh_usrseq_no = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 0, GlbUserComCode);
                _SerHeader.Tuh_usr_id = GlbUserName;
                _SerHeader.Tuh_usr_com = GlbUserComCode;
                _SerHeader.Tuh_session_id = GlbUserSessionID;
                _SerHeader.Tuh_cre_dt = Convert.ToDateTime(txtDate.Text).Date;
                _SerHeader.Tuh_doc_tp = "RECEIPT";
                _SerHeader.Tuh_direct = false;
                _SerHeader.Tuh_ischek_itmstus = true;
                _SerHeader.Tuh_ischek_simitm = true;
                _SerHeader.Tuh_ischek_reqqty = true;

                if (txtType.Text == "VHREG")
                {
                    _SerHeader.Tuh_doc_no = _InvNo;
                }
                else
                {
                    _SerHeader.Tuh_doc_no = "na";
                }


                foreach (ReptPickSerials line in _ResList)
                {
                    line.Tus_usrseq_no = _SerHeader.Tuh_usrseq_no;
                    line.Tus_cre_by = GlbUserName;
                    _tempSerialSave.Add(line);
                }
            }

            if (txtType.Text == "VHREG")
            {
                foreach (VehicalRegistration _reg in _regList)
                {
                    Int32 _vehSeq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "VHREG", 1, GlbUserComCode);
                    _reg.P_seq = _vehSeq;
                    _tempRegSave.Add(_reg);
                }
            }

            if (txtType.Text == "VHINS")
            {
                foreach (VehicleInsuarance _ins in _insList)
                {
                    Int32 _insSeq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "VHINS", 1, GlbUserComCode);
                    _ins.Svit_seq = _insSeq;
                    _tempInsSave.Add(_ins);
                }
            }

            string QTNum;

            row_aff = (Int32)CHNLSVC.Sales.SaveNewReceipt(_ReceiptHeader, _ReceiptDetailsSave, masterAuto, _SerHeader, _tempSerialSave, _tempRegSave, _tempInsSave, _sheduleDetails, masterAutoRecTp, out QTNum);

            if (chkIsManual.Checked == true)
            {
                CHNLSVC.Inventory.UpdateManualDocNo(GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtRefNo.Text));
            }

            if (row_aff == 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created.Receipt No: " + QTNum);
                if (string.IsNullOrEmpty(QTNum)) return;
                MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(GlbUserComCode, txtCusCode.Text, string.Empty, string.Empty, "C");

                if (chkIsManual.Checked == false)
                {
                    string Msg = "";
                    GlbMainPage = "~/Reports_Module/Sales_Rep/Sales_Rep.aspx";
                    clearReportParameters();
                    GlbDocNosList = QTNum.Trim();


                    if (_itm.Mbe_sub_tp != "C.")
                    {
                        GlbReportName = "ReceiptPrint";
                        GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ReceiptPrint.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";

                        Msg = "<script>window.open('../../Reports_Module/Sales_Rep/ReceiptEntryPrint.aspx','_blank');</script>";
                    }
                    else
                    {
                        GlbReportName = "ReceiptPrintDealer";
                        GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ReceiptPrintDealer.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrintDealer.rpt";

                        Msg = "<script>window.open('../../Reports_Module/Sales_Rep/ReceiptEntryPrint.aspx','_blank');</script>";
                    }


                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                //if (_itm.Mbe_sub_tp != "C.")
                //{
                //    GlbReportPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";
                //    GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";
                //}
                //else
                //{
                //    GlbReportPath = "~/Reports_Module/Sales_Rep/ReceiptPrintDealer.rpt";
                //    GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrintDealer.rpt";
                //}

                ////GlbReportPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";
                ////GlbReportMapPath = "~/Reports_Module/Sales_Rep/ReceiptPrint.rpt";

                //GlbMainPage = "~/Financial_Modules/ReceiptEntry.aspx";
                //Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
                Clear_Data();
                btnSave.Enabled = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    btnSave.Enabled = true;
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                    
                }
                else
                {
                    btnSave.Enabled = true;
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");

                }
            }
        }

        protected void imgInvSearch_Click(object sender, ImageClickEventArgs e)
        {

            if ((txtType.Text == "VHREG") || (txtType.Text == "VHINS"))
            {
                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
                    DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

                    MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                    MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                    MasterCommonSearchUCtrl.ReturnResultControl = txtInvoice.ClientID;
                    MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
                }
                else
                {
                    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
                    DataTable dataSource = CHNLSVC.CommonSearch.GetInvoicebyCustomer(MasterCommonSearchUCtrl.SearchParams, null, null);

                    MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                    MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                    MasterCommonSearchUCtrl.ReturnResultControl = txtInvoice.ClientID;
                    MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
                }
            }

            else
            {
                if (txtType.Text == "DEBT")
                {

                    if (string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select customer.");
                        txtCusCode.Focus();
                        return;
                    }
                }

                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInv);
                DataTable dataSource = CHNLSVC.CommonSearch.GetOutstandingInvoice(MasterCommonSearchUCtrl.SearchParams, null, null);

                MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                MasterCommonSearchUCtrl.ReturnResultControl = txtInvoice.ClientID;
                MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            }
        }

        protected void imgSearchDiv_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Division);
            DataTable dataSource = CHNLSVC.CommonSearch.GetDivision(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtDivision.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgrecSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Receipt);
            DataTable dataSource = CHNLSVC.CommonSearch.GetReceipts(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtRecNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private void LoadSaveReceipt()
        {

            _IsRecall = false;
            _RecStatus = false;
            _sunUpload = false;

            RecieptHeader _ReceiptHeader = null;
            _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeader(GlbUserComCode, GlbUserDefProf, txtRecNo.Text.Trim());
            if (_ReceiptHeader != null)
            {
                txtType.Text = _ReceiptHeader.Sar_receipt_type;
                txtRecNo.Text = _ReceiptHeader.Sar_receipt_no;
                txtDate.Text = Convert.ToDateTime(_ReceiptHeader.Sar_receipt_date).ToShortDateString();
                txtRefNo.Text = _ReceiptHeader.Sar_manual_ref_no;
                txtRemarks.Text = _ReceiptHeader.Sar_remarks;
                txtTotal.Text = Convert.ToDecimal(_ReceiptHeader.Sar_tot_settle_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
                txtCusCode.Text = _ReceiptHeader.Sar_debtor_cd;
                txtCusAdd1.Text = _ReceiptHeader.Sar_debtor_add_1;
                txtCusAdd2.Text = _ReceiptHeader.Sar_debtor_add_2;
                txtCusName.Text = _ReceiptHeader.Sar_debtor_name;
                txtMob.Text = _ReceiptHeader.Sar_mob_no;
                txtNIC.Text = _ReceiptHeader.Sar_nic_no;
                txtProvince.Text = _ReceiptHeader.Sar_anal_2;
                if (string.IsNullOrEmpty(_ReceiptHeader.Sar_anal_1))
                {
                    //  ddlDistrict.SelectedValue = " ";
                }
                else
                {
                    ddlDistrict.SelectedValue = _ReceiptHeader.Sar_anal_1;
                }
                _RecStatus = _ReceiptHeader.Sar_act;
                _sunUpload = _ReceiptHeader.Sar_uploaded_to_finance;

            }

            BindSaveReceiptDetails(_ReceiptHeader.Sar_receipt_no);
            BindSaveResevationDetails(_ReceiptHeader.Sar_com_cd, _ReceiptHeader.Sar_receipt_no);
            BindSaveVehicleReg(_ReceiptHeader.Sar_receipt_no);
            BindSaveVehicleIns(_ReceiptHeader.Sar_receipt_no);
            _IsRecall = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = true;
        }

        private void BindSaveVehicleIns(string _RecNo)
        {
            //List<VehicleInsuarance> _list = CHNLSVC.General.GetVehicalInsuarance(_RecNo);
            //_insList = new List<VehicleInsuarance>();
            //_insList = _list;
            //gvInsu.DataSource = _insList;
            //gvInsu.DataBind();
        }

        private void BindSaveVehicleReg(string _RecNo)
        {
            List<VehicalRegistration> _list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
            _regList = new List<VehicalRegistration>();
            _regList = _list;
            gvReg.DataSource = _regList;
            gvReg.DataBind();
        }

        private void BindSaveReceiptDetails(string _RecNo)
        {
            RecieptItem _paramRecDetails = new RecieptItem();

            _paramRecDetails.Sard_receipt_no = _RecNo;

            List<RecieptItem> _list = CHNLSVC.Sales.GetReceiptDetails(_paramRecDetails);
            _RecieptDetails = new List<RecieptItem>();
            _RecieptDetails = _list;
            gvRecDetails.DataSource = _RecieptDetails;
            gvRecDetails.DataBind();

        }

        private void BindSaveResevationDetails(string _com, string _doc)
        {
            List<ReptPickSerials> _list = CHNLSVC.Sales.GetSerialByBaseDoc(_com, _doc);
            _ResList = new List<ReptPickSerials>();
            _ResList = _list;
            gvSerial.DataSource = _ResList;
            gvSerial.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtRecNo.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select receipt #.");
                txtRecNo.Focus();
                return;
            }

            if (_RecStatus == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select receipt is already cancelled.");
                txtRecNo.Focus();
                return;
            }

            if (_sunUpload == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot cancel.Already uploaded to accounts.");
                return;
            }

            if (IsAllowBackDateForModule(GlbUserComCode, string.Empty, GlbUserDefProf, Page, txtDate.Text, imgRequestDate, lblDispalyInfor) == false)
            {
                if (Convert.ToDateTime(txtDate.Text).Date != (DateTime.Now.Date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot cancel previous receipt.Please get a backdate.");
                    return;
                }
            }


            UpdateRecStatus(false);
            _RecieptDetails = new List<RecieptItem>();
            _ResList = new List<ReptPickSerials>();
            _regList = new List<VehicalRegistration>();
            _insList = new List<VehicleInsuarance>();
            GrndTotal = 0;
            _IsRecall = false;
            _RecStatus = false;
            //BindPaymentType(ddlPayMode);
            _lineNo = 0;
            _sunUpload = false;
            txtCusCode.ReadOnly = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = true;
        }

        private void UpdateRecStatus(Boolean _RecUpdateStatus)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            RecieptHeader _UpdateReceipt = new RecieptHeader();
            _UpdateReceipt.Sar_receipt_no = txtRecNo.Text.Trim();
            _UpdateReceipt.Sar_act = _RecUpdateStatus;
            _UpdateReceipt.Sar_com_cd = GlbUserComCode;
            _UpdateReceipt.Sar_profit_center_cd = GlbUserDefProf;
            _UpdateReceipt.Sar_mod_by = GlbUserName;

            row_aff = (Int32)CHNLSVC.Sales.UpdateRecStatus(_UpdateReceipt);

            if (row_aff == 1)
            {

                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully cancelled. ");
                }
                Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                }
            }
        }

        protected void OnRemoveFromRecDetails(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (_RecieptDetails != null)
                if (_RecieptDetails.Count > 0)
                {

                    if (txtType.Text == "VHREG" || txtType.Text == "VHINS")
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot edit enter payment details.Pls. clear and re-process.");
                        return;
                    }
                    //if (txtType.Text == "VHREG")
                    //{
                    //    if (gvReg.Rows.Count > 0)
                    //    {
                    //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are registration details.Please remove those first and process.");
                    //        return;
                    //    }
                    //}

                    //if (txtType.Text == "VHINS")
                    //{
                    //    if (gvInsu.Rows.Count > 0)
                    //    {
                    //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are insuarance details.Please remove those first and process.");
                    //        return;
                    //    }
                    //}

                    decimal _uprice = (decimal)gvRecDetails.DataKeys[row_id][0];

                    CalculateGrandTotal(_uprice, false);
                    List<RecieptItem> _tempList = _RecieptDetails;
                    _lineNo = _lineNo - 1;
                    _tempList.RemoveAt(row_id);
                    _RecieptDetails = _tempList;
                    BindAddItem();
                }

        }

        protected void OnRemoveFromResDetails(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (_ResList != null)
                if (_ResList.Count > 0)
                {

                    List<ReptPickSerials> _tempList = _ResList;
                    _tempList.RemoveAt(row_id);
                    _ResList = _tempList;
                    gvSerial.DataSource = _ResList;
                    gvSerial.DataBind();
                }

        }

        protected void OnRemoveFromRegDetails(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (_regList != null)
                if (_regList.Count > 0)
                {

                    List<VehicalRegistration> _tempList = _regList;
                    _tempList.RemoveAt(row_id);
                    _regList = _tempList;
                    gvReg.DataSource = _regList;
                    gvReg.DataBind();
                }

        }

        protected void OnRemoveFromInsDetails(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (_insList != null)
                if (_insList.Count > 0)
                {

                    List<VehicleInsuarance> _tempList = _insList;
                    _tempList.RemoveAt(row_id);
                    _insList = _tempList;
                    gvInsu.DataSource = _insList;
                    gvInsu.DataBind();
                }

        }


        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlDistrict.SelectedValue.ToString())) return;
            DistrictProvince _type = CHNLSVC.Sales.GetDistrict(ddlDistrict.SelectedValue.ToString())[0];
            if (_type.Mds_district == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid district."); return; }
            txtProvince.Text = _type.Mds_province;
        }

        protected void imgItmSearch_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgInvEngine_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvItem.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select related invoice item.");
                txtInvItem.Focus();
                return;
            }

            if (chkDeliverItem.Checked == false)
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

                MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                MasterCommonSearchUCtrl.ReturnResultControl = txtInvEngine.ClientID;
                MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            }
            else
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DeliverdSerials);
                DataTable dataSource = CHNLSVC.CommonSearch.GetDeliverdInvoiceItemSerials(MasterCommonSearchUCtrl.SearchParams, null, null);

                MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                MasterCommonSearchUCtrl.ReturnResultControl = txtInvEngine.ClientID;
                MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            }
        }

        protected void imgEngine_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtEngine.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void imgChasis_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtchasis.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgInvChasis_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvEngine.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }


        protected void btnAddSerial_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtItem.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select item.");
            //    txtItem.Text = "";
            //    txtItem.Focus();
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtEngine.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select serial.");
            //    txtEngine.Text = "";
            //    txtEngine.Focus();
            //    return;
            //}




            ReptPickSerials _tempItem = new ReptPickSerials();
            foreach (ReptPickSerials _serial in _ResList)
            {
                if (_serial.Tus_itm_cd == txtItem.Text.Trim() && _serial.Tus_ser_1 == txtEngine.Text.Trim())
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected serial is already exsist.");
                    txtItem.Focus();
                    return;
                }
            }

            _tempItem = CHNLSVC.Inventory.GetAvailableSerIDInformation(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), txtEngine.Text.Trim(), string.Empty, string.Empty);
            _tempItem.Tus_itm_desc = lblDesc.Text;
            _tempItem.Tus_itm_model = lblModel.Text;

            //ADDED BY SACHITH
            //2012/12/24
            //if no serial
            _tempItem.Tus_com = GlbUserComCode;
            _tempItem.Tus_loc = GlbUserDefLoca;
            if (_tempItem.Tus_itm_cd == "" || _tempItem.Tus_itm_cd == null)
            {
                _tempItem.Tus_itm_cd = txtItem.Text;
            }

            //END


            _ResList.Add(_tempItem);
            gvSerial.DataSource = _ResList;
            gvSerial.DataBind();

            txtItem.Text = "";
            lblModel.Text = "";
            lblDesc.Text = "";
            txtEngine.Text = "";
            txtchasis.Text = "";
            lblEngine.Visible = false;
            lblChasis.Visible = false;
            imgChasis.Visible = false;
            imgEngine.Visible = false;
            txtchasis.Visible = false;
            txtEngine.Visible = false;
            txtItem.Focus();

        }


        protected void CheckValidChasis(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtchasis.Text))
            {
                ReptPickSerials _serialList = new ReptPickSerials();
                _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), string.Empty, txtchasis.Text.Trim(), string.Empty);

                if (_serialList.Tus_ser_1 != null)
                {
                    txtEngine.Text = _serialList.Tus_ser_1;
                    if (txtchasis.Visible == true)
                    {
                        txtchasis.Text = _serialList.Tus_ser_2;
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Chasis #.");
                    txtchasis.Text = "";
                    txtchasis.Focus();
                    return;
                }
            }
        }

        protected void CheckValidInvEngine(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvEngine.Text))
            {

                if (txtType.Text == "VHREG")
                {
                    VehicalRegistration _RegPrvDetails = new VehicalRegistration();
                    _RegPrvDetails = CHNLSVC.Sales.CheckPrvRegDetails(txtInvEngine.Text.Trim(), txtInvItem.Text.Trim(), GlbUserComCode, 2);

                    if (_RegPrvDetails.P_srvt_ref_no != null)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Already generated registration for this engine.Invoice # :" + _RegPrvDetails.P_svrt_inv_no);
                        txtInvEngine.Text = "";
                        txtInvChasis.Text = "";
                        txtInvEngine.Focus();
                        return;
                    }
                }
                else if (txtType.Text == "VHINS")
                {
                    VehicleInsuarance _InsPrvDetails = new VehicleInsuarance();
                    _InsPrvDetails = CHNLSVC.Sales.CheckPrvInsDetails(txtInvEngine.Text.Trim(), txtInvItem.Text.Trim(), GlbUserComCode, 2);

                    if (_InsPrvDetails.Svit_ref_no != null)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Already generated insuarance collection for this engine.Invoice # :" + _InsPrvDetails.Svit_inv_no);
                        txtInvEngine.Text = "";
                        txtInvChasis.Text = "";
                        txtInvEngine.Focus();
                        return;
                    }
                }


                if (chkDeliverItem.Checked == false)
                {
                    ReptPickSerials _serialList = new ReptPickSerials();
                    _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(GlbUserComCode, GlbUserDefLoca, txtInvItem.Text.Trim(), txtInvEngine.Text.Trim(), string.Empty, string.Empty);

                    if (_serialList.Tus_ser_1 != null)
                    {
                        txtInvEngine.Text = _serialList.Tus_ser_1;
                        if (txtInvChasis.Visible == true)
                        {
                            txtInvChasis.Text = _serialList.Tus_ser_2;
                        }
                    }
                    else
                    {
                        //ADDED BY SACHITH
                        //2012/11/15

                        if (txtType.Text == "VHINS")
                        {
                            DataTable _dt = CHNLSVC.Sales.GetInsuranceOnEngine(GlbUserComCode, GlbUserDefProf, "Reg", txtInvEngine.Text);
                            if (_dt.Rows.Count > 0) { 
                            //get chassis
                                txtInvChasis.Text = _dt.Rows[0]["SVRT_CHASSIS"].ToString();
                            }
                            else
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid engine #.");
                                txtInvEngine.Text = "";
                                txtInvEngine.Focus();
                                return;
                            }
                        }
                        else if (txtType.Text == "VHREG") {
                            DataTable _dt = CHNLSVC.Sales.GetInsuranceOnEngine(GlbUserComCode, GlbUserDefProf, "Ins", txtInvEngine.Text);
                            if (_dt.Rows.Count > 0)
                            {
                                //get chassis
                                txtInvChasis.Text = _dt.Rows[0]["SVIT_CHASSIS"].ToString();
                            }
                            else
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid engine #.");
                                txtInvEngine.Text = "";
                                txtInvEngine.Focus();
                                return;
                            }

                        }

                        //END
                    }
                }
                else
                {
                    InventorySerialN _delList = new InventorySerialN();
                    _delList = CHNLSVC.Inventory.GetDeliveredSerialForItem(GlbUserComCode, txtInvoice.Text.Trim(), txtInvItem.Text.Trim(),txtInvEngine.Text.Trim());

                    if (_delList.Ins_doc_no != null)
                    {
                        txtInvEngine.Text = _delList.Ins_ser_1;
                        if (txtInvChasis.Visible == true)
                        {
                            txtInvChasis.Text = _delList.Ins_ser_2;
                        }
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid engine #.");
                        txtInvEngine.Text = "";
                        txtInvEngine.Focus();
                        return;
                    }

                }
            }
        }

        protected void CheckValidSerial(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEngine.Text))
            {
                ReptPickSerials _serialList = new ReptPickSerials();
                _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), txtEngine.Text.Trim(), string.Empty, string.Empty);

                if (_serialList.Tus_ser_1 != null)
                {
                    txtEngine.Text = _serialList.Tus_ser_1;
                    if (txtchasis.Visible == true)
                    {
                        txtchasis.Text = _serialList.Tus_ser_2;
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid serial #.");
                    txtEngine.Text = "";
                    txtEngine.Focus();
                    return;
                }
            }
        }


        protected void CheckValidPolicy(object sender, EventArgs e)
        {
            Int32 _HpTerm = 0;

            if (!string.IsNullOrEmpty(txtPolicy.Text))
            {
                if (!string.IsNullOrEmpty(txtInvItem.Text))
                {
                    InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                    _insuPolicy = CHNLSVC.Sales.GetInusPolicy(txtPolicy.Text.Trim());

                    if (_insuPolicy.Svip_polc_cd == null)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid insuarance policy.");
                        txtPolicy.Text = "";
                        txtPolicy.Focus();
                        return;
                    }

                    else
                    {
                        txtPolicy.Text = _insuPolicy.Svip_polc_cd;
                        MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();

                        if (_accNo != null && _accNo != "")
                        {
                            _HpAccount = new HpAccount();
                            _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);

                            _HpTerm = _HpAccount.Hpa_term;

                            if (_HpTerm < 12)
                            {
                                chkAnnual.Checked = false; 
                                chkAnnual.Visible = true;
                            }
                            else
                            {
                                chkAnnual.Checked = false;
                                chkAnnual.Visible = false;
                            }


                            if ((_HpTerm + 3) <= 3)
                            {
                                _HpTerm = 3;
                            }
                            else if ((_HpTerm + 3) <= 6)
                            {
                                _HpTerm = 6;
                            }
                            else if ((_HpTerm + 3) <= 9)
                            {
                                _HpTerm = 9;
                            }
                            else
                            {
                                _HpTerm = 12;
                            }
                            _vehIns = CHNLSVC.Sales.GetVehInsDef(GlbUserComCode, txtInvoice.Text.Trim(), txtInvItem.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf, txtInsCom.Text.Trim(), txtPolicy.Text.Trim(), _HpTerm);
                        }

                        else
                        {
                            _HpTerm = 12;
                            _vehIns = CHNLSVC.Sales.GetVehInsDef(GlbUserComCode, txtInvoice.Text.Trim(), txtInvItem.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf, txtInsCom.Text.Trim(), txtPolicy.Text.Trim(), _HpTerm);
                        }

                        if (_vehIns.Ins_com_cd != null)
                        {
                            txtInvAmt.Text = _vehIns.Value.ToString("0.00");
                            txtAmount.Text = _vehIns.Value.ToString("0.00");
                        }
                        else
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Insuarance amount definitions not set.");
                            txtInvAmt.Text = "0.00";
                            txtAmount.Text = "0.00";
                            txtPolicy.Text = "";
                            txtPolicy.Focus();
                            return;
                        }

                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invoice item is missing.");
                    txtPolicy.Text = "";
                    txtInvItem.Focus();
                    return;
                }

            }
            txtAmount.Focus();
        }


        protected void CheckValidInsuCom(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInsCom.Text))
            {
                MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtInsCom.Text.Trim(), "INS");

                if (_OutPartyDetails.Mbi_cd == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid insuarance company.");
                    txtInsCom.Text = "";
                    txtInsCom.Focus();
                    return;
                }
                else
                {
                    txtInsCom.Text = _OutPartyDetails.Mbi_cd;
                }
            }
            txtPolicy.Focus();
        }

        protected void CheckValidInvItem(object sender, EventArgs e)
        {
            Boolean _isLease = false;
            Boolean _isAllowEdit = false;
            if (!string.IsNullOrEmpty(txtInvItem.Text))
            {
                MasterItem _itemList = new MasterItem();
                _itemList = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtInvItem.Text);

                InvoiceItem _invItem = new InvoiceItem();
                _invItem = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoice.Text.Trim(), txtInvItem.Text.Trim());

                if (_invItem.Sad_inv_no != null)
                {
                    if (_invItem.Sad_do_qty > 0)
                    {
                        chkDeliverItem.Checked = true;
                    }
                    else
                    {
                        chkDeliverItem.Checked = false;
                    }
                }

                if (_itemList.Mi_cd != null)
                {
                    if (txtType.Text == "VHREG")
                    {
                        if (_itemList.Mi_need_reg == true)
                        {
                            VehicalRegistrationDefnition _vehDef = new VehicalRegistrationDefnition();
                            _isLease = CHNLSVC.Sales.IsCheckLeaseCom(txtInvoice.Text.Trim(), GlbUserComCode, GlbUserDefProf, "LEASE");

                            if (_isLease == false)
                            {
                                
                                _vehDef = CHNLSVC.Sales.GetVehRegDef(GlbUserComCode, txtInvoice.Text.Trim(), txtInvItem.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date);

                                if (_vehDef.Svrd_itm != null)
                                {
                                    txtInvAmt.Text = _vehDef.Svrd_val.ToString("0.00");
                                    txtAmount.Text = _vehDef.Svrd_val.ToString("0.00");
                                    _regAmt = _vehDef.Svrd_claim_val;
                                }
                                else
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Registration amount definitions not set.");
                                    txtInvItem.Text = "";
                                    txtInvAmt.Text = "0.00";
                                    txtAmount.Text = "0.00";
                                    _regAmt = 0;
                                    txtInvItem.Focus();
                                    return;
                                }
                            }
                            else if (_isLease == true)
                            {
                                _vehDef = CHNLSVC.Sales.GetVehRegAmtDirect(GlbUserComCode, GlbUserDefProf, "LEASE", txtInvItem.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date);

                                if (_vehDef.Svrd_itm != null)
                                {
                                    txtInvAmt.Text = _vehDef.Svrd_val.ToString("0.00");
                                    txtAmount.Text = _vehDef.Svrd_val.ToString("0.00");
                                    _regAmt = _vehDef.Svrd_claim_val;
                                }
                                else
                                {
                                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Registration amount definitions not set for leasing company.");
                                    txtInvItem.Text = "";
                                    txtInvAmt.Text = "0.00";
                                    txtAmount.Text = "0.00";
                                    _regAmt = 0;
                                    txtInvItem.Focus();
                                    return;
                                }
                            }
                            //check registartion amount edit profit center
                            _isAllowEdit = CHNLSVC.Sales.IsCheckAllowFunction(GlbUserComCode, GlbUserDefProf, txtType.Text.Trim(), "ALWEDIT");
                            if (_isAllowEdit == true)
                            {
                                txtInvAmt.ReadOnly = false;
                                txtInvAmt.Enabled = true;
                            }
                            else
                            {
                                txtInvAmt.ReadOnly = true;
                                txtInvAmt.Enabled = false;
                            }
                        }
                        else
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This item is not allow to regiration process.");
                            txtInvItem.Text = "";
                            txtInvItem.Focus();
                            return;
                        }
                    }
                    else if (txtType.Text == "VHINS")
                    {
                        if (_itemList.Mi_need_insu == false)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This item is not allow to insuarance process.");
                            txtInvItem.Text = "";
                            txtInvItem.Focus();
                            return;
                        }
                    }
                }
            }
        }

        protected void CheckValidItem(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                if (!CHNLSVC.Inventory.IsValidCompanyItem(GlbUserComCode, txtItem.Text, 1))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item Not allow to company or invalid item.");
                    txtItem.Text = "";
                    txtItem.Focus();
                    return;
                }
                else
                {
                    MasterItem _itemList = new MasterItem();
                    _itemList = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text);

                    if (_itemList.Mi_cd != null)
                    {
                        txtItem.Text = _itemList.Mi_cd;
                        lblModel.Text = _itemList.Mi_model;
                        lblDesc.Text = _itemList.Mi_shortdesc;
                        lblEngine.Visible = true;
                        lblModel.Visible = true;
                        lblDesc.Visible = true;
                        lblChasis.Visible = true;
                        txtEngine.Visible = true;
                        txtchasis.Visible = true;
                        imgEngine.Visible = true;
                        //imgChasis.Visible = true;
                        btnAddSerial.Visible = true;
                        Label10.Visible = true;
                        Label11.Visible = true;
                        if (_itemList.Mi_need_reg == true)
                        {
                            lblEngine.Text = "Engine # :";
                            lblChasis.Text = "Chasis # :";
                        }
                        else
                        {
                            lblEngine.Text = "Serial # :";
                            txtEngine.Visible = true;
                            lblChasis.Visible = false;
                            txtchasis.Visible = false;
                            imgChasis.Visible = false;
                            imgEngine.Visible = true;
                        }
                        txtEngine.Focus();
                    }

                }
            }
        }

        protected void CheckValidManualRef(object sender, EventArgs e)
        {
            if (chkIsManual.Checked == true)
            {
                if (txtRefNo.Text != "")
                {
                    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(GlbUserComCode, GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtRefNo.Text));
                    if (_IsValid == false)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                        txtRefNo.Text = "";
                        txtRefNo.Focus();
                    }
                }
                else
                {
                    if (chkIsManual.Checked == true)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                        txtRefNo.Focus();
                    }
                }
            }
        }

        protected void chkIsManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsManual.Checked == true)
            {
                txtRefNo.Text = "";
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(GlbUserComCode, GlbUserDefLoca, "MDOC_AVREC");
                if (_NextNo != 0)
                {
                    txtRefNo.Text = _NextNo.ToString();
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find any available advance receipts.");
                    txtRefNo.Text = "";
                    chkIsManual.Checked = false;
                    txtRefNo.Focus();
                    return;

                }
            }

            else
            {
                txtRefNo.Text = string.Empty;

            }
        }

        protected void chkAnnual_CheckedChanged(object sender, EventArgs e)
        {
            Int32 _HpTerm = 0;
            MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();

            if (_accNo != null && _accNo != "")
            {
                _HpAccount = new HpAccount();
                _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);

                _HpTerm = _HpAccount.Hpa_term;

                if (_HpTerm < 12)
                {
                    _HpTerm = 12;
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This option is only valid for under 12 months accounts.");
                    txtInvAmt.Text = "0.00";
                    txtAmount.Text = "0.00";
                    return;
                }

               
                _vehIns = CHNLSVC.Sales.GetVehInsDef(GlbUserComCode, txtInvoice.Text.Trim(), txtInvItem.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf, txtInsCom.Text.Trim(), txtPolicy.Text.Trim(), _HpTerm);
            }

            if (_vehIns.Ins_com_cd != null)
            {
                txtInvAmt.Text = _vehIns.Value.ToString("0.00");
                txtAmount.Text = _vehIns.Value.ToString("0.00");
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Insuarance amount definitions not set.");
                txtInvAmt.Text = "0.00";
                txtAmount.Text = "0.00";
                txtPolicy.Text = "";
                txtPolicy.Focus();
                return;
            }
        }

    }
}
