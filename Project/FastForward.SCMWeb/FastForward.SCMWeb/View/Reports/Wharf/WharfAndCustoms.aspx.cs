using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using FastForward.SCMWeb.UserControls;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System.Diagnostics;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Mail;
using System.Configuration;
using FastForward.SCMWeb.View.Reports.Imports;


namespace FastForward.SCMWeb.View.Reports.Wharf
{
    public partial class WharfAndCustoms : BasePage
    {
        //Created By Dulaj 2018/Dec/10
        public List<DropDownList> _ddlList
        {
            get { if (Session["_ddlList"] == null) { return new List<DropDownList>(); } else { return (List<DropDownList>)Session["_ddlList"]; } }
            set { Session["_ddlList"] = value; }
        }
        public List<TextBox> _txtList
        {
            get { if (Session["_txtList"] == null) { return new List<TextBox>(); } else { return (List<TextBox>)Session["_txtList"]; } }
            set { Session["_txtList"] = value; }
        }
        public List<LinkButton> _lbtnList
        {
            get { if (Session["_lbtnList"] == null) { return new List<LinkButton>(); } else { return (List<LinkButton>)Session["_lbtnList"]; } }
            set { Session["_lbtnList"] = value; }
        }
        public List<RadioButton> _radioList
        {
            get { if (Session["_radioList"] == null) { return new List<RadioButton>(); } else { return (List<RadioButton>)Session["_radioList"]; } }
            set { Session["_radioList"] = value; }
        }
        public List<CheckBox> _chkList
        {
            get { if (Session["_chkList"] == null) { return new List<CheckBox>(); } else { return (List<CheckBox>)Session["_chkList"]; } }
            set { Session["_chkList"] = value; }
        }
        public List<ListBox> _lstList
        {
            get { if (Session["_lstList"] == null) { return new List<ListBox>(); } else { return (List<ListBox>)Session["_lstList"]; } }
            set { Session["_lstList"] = value; }
        }

        public List<LinkButton> _enableLbtnList
        {
            get { if (Session["_enableLbtnList"] == null) { return new List<LinkButton>(); } else { return (List<LinkButton>)Session["_enableLbtnList"]; } }
            set { Session["_enableLbtnList"] = value; }
        }
        public List<RadioButton> _enbleRadioList
        {
            get { if (Session["_enbleRadioList"] == null) { return new List<RadioButton>(); } else { return (List<RadioButton>)Session["_enbleRadioList"]; } }
            set { Session["_enbleRadioList"] = value; }
        }

        public List<CheckBox> _enbleChkList
        {
            get { if (Session["_enbleChkList"] == null) { return new List<CheckBox>(); } else { return (List<CheckBox>)Session["_enbleChkList"]; } }
            set { Session["_enbleChkList"] = value; }
        }

        public List<TextBox> _enbleTxtList
        {
            get { if (Session["_enbleTxtList"] == null) { return new List<TextBox>(); } else { return (List<TextBox>)Session["_enbleTxtList"]; } }
            set { Session["_enbleTxtList"] = value; }
        }

        public List<DropDownList> _enbleDdlList
        {
            get { if (Session["_enbleDdlList"] == null) { return new List<DropDownList>(); } else { return (List<DropDownList>)Session["_enbleDdlList"]; } }
            set { Session["_enbleDdlList"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DATAADMIN"] = null;
                Session["DATACOMPANY"] = null;
                BindYear();
                LoadEmptyGrid();
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                BindCompany(Session["UserID"].ToString());
                txtFromDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtAsAt.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                dgvAdminTeam.Columns[1].Visible = false;
                AddEnableCntr();
                DesableAllControler();
                GetRequesttype();
                ddlPaymentTermsDataPopulate();
            }
            else
            {

            }
        }
        private void ddlPaymentTermsDataPopulate()
        {

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentTerms);
            DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, null, null);
            ddlPaymentTerms.DataSource = result;
            ddlPaymentTerms.DataTextField = "CODE";
            ddlPaymentTerms.DataValueField = "CODE";
            ddlPaymentTerms.DataBind();

        }
        private void GetRequesttype()
        {
            List<MasterSubType> _MasterSubType = new List<MasterSubType>();
            _MasterSubType = CHNLSVC.General.GetAllSubTypes("CUSA");
            if (_MasterSubType != null)
            {
                ddlRequestType.DataSource = _MasterSubType;
                ddlRequestType.DataTextField = "mstp_desc";
                ddlRequestType.DataValueField = "mstp_cd";
                ddlRequestType.DataBind();
            }

        }

        private void AddEnableCntr()
        {
            List<RadioButton> _rList = new List<RadioButton>();
            _rList.Add(rad01);
            _rList.Add(rad02);      
            _rList.Add(rbpdf);
            _rList.Add(rbexel);
            _rList.Add(rbexeldata);


            _enbleRadioList = _rList;

            List<LinkButton> _lList = new List<LinkButton>();
            _lList.Add(lbtnView);
            _lList.Add(lbtnClear);
            _lList.Add(lbtnSearch);
            _lList.Add(btnDownloadfile);
            _enableLbtnList = _lList;

            List<CheckBox> _lChk = new List<CheckBox>();
            //_lChk.Add(chkAllCompany);     //Commented by Udesh 09/Oct/2018
            //_lChk.Add(chkAllAdmin);       //Commented by Udesh 09/Oct/2018
            _lChk.Add(chkautomail);
            //_lChk.Add(chkblpending);
            //_lChk.Add(chknewall);
            foreach (GridViewRow grdRow in dgvCompany.Rows)
            {
                CheckBox chkCompanyCode = grdRow.FindControl("chkCompanyCode") as CheckBox;
                _lChk.Add(chkCompanyCode);
            }
            _enbleChkList = _lChk;

            List<TextBox> _lTxt = new List<TextBox>();
            _lTxt.Add(txtSearchbyword);
            _enbleTxtList = _lTxt;

            List<DropDownList> _lDdl = new List<DropDownList>();
            _lDdl.Add(cmbSearchbykey);
            _enbleDdlList = _lDdl;
        }
        private void setFormControls(Int32 _index)
        {
            txtLcNo.Enabled = false;
            txtBankNo.Enabled = false;

            switch (_index)
            {
                case 3:
                    txtLcNo.Enabled = true;
                    txtBankNo.Enabled = true;
                    break;
            }
        }

        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
        }
        private void DisplayMessage(String Msg, Int32 option)
        {
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }


        protected void lbtnUpd_Click(object sender, EventArgs e)
        {

        }
        string _opt = "";
        bool _aoutomail = false;
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (chkAllBankNo.Checked == false) { if (txtBankNo.Text == "") { DisplayMessage("Please select the bank code!", 2); return; } }
                if (chkAllLcNo.Checked == false) { if (txtLcNo.Text == "") { DisplayMessage("Please select the LC number!", 2); return; } }

                Boolean _isSelected = false;
                //BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                //BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                //BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                //BaseCls.GlbReportToBondNo = txtToBondNo.Text;
                //BaseCls.GlbReportBLNo = txtBlNo.Text;

                //BaseCls.GlbReportAgent = txtAgent.Text;
                //BaseCls.GlbReportPort = txtPort.Text;
                //BaseCls.GlbReportCountry = txtCountry.Text;
                //BaseCls.GlbReportSupplier = txtSupplier.Text;
                //BaseCls.GlbReportComp = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportProfit = Session["UserDefLoca"].ToString();
                string _export = "N";

            
                Session["GlbReportName"] = string.Empty;
                GlbReportName = string.Empty;
                InvReportPara _invRepPara = new InvReportPara();

                lbtnView.Enabled = false;

  
                string com_cds = "";
                string com_desc = "";
                Int32 _cnt = 0;
                foreach (GridViewRow row in dgvCompany.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkCompanyCode");
                    if (chk.Checked)
                    {
                        if (com_cds != "")
                        {
                            com_cds = com_cds + ",";
                        }
                        Label lblCode = (Label)row.FindControl("lblCode");

                        com_cds = com_cds + lblCode.Text;
                        _cnt = _cnt + 1;
                    }
                }

                MasterCompany _newCom = new MasterCompany();
                _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                com_desc = _newCom.Mc_desc;      

                _invRepPara._GlbReportCompName = com_desc;
                _invRepPara._GlbReportCompanies = com_cds;


               
                _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbUserComCode = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportProfit = Session["UserDefLoca"].ToString();
                _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                _invRepPara._GlbReportSupplier = txtSupplier.Text;
                _invRepPara._GlbReportAgent = txtAgent.Text;
                _invRepPara._GlbReportCountry = txtCountry.Text;
                _invRepPara._GlbReportPort = txtPort.Text;
                _invRepPara._GlbUserID = Session["UserID"].ToString();
                
                _opt = "";

               
                clswharf obj = new clswharf();
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = "";
                if (rbpdf.Checked)
                {
                    targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                }
                if (rbexel.Checked)
                {
                    targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".xls";
                }
                if (rbexeldata.Checked)
                {
                    targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".xls";
                }

                if (rad01.Checked == true)  
                {//Dulaj 2018/Dec/10
                    _opt = "rad01";
                    _isSelected = true;
                    _invRepPara._GlbReportName = "ShipmentTracker.rpt";
                    Session["GlbReportName"] = "ShipmentTracker.rpt";
                    _invRepPara._GlbReportHeading = _newCom.Mc_it_powered;
                    _invRepPara._GlbDocNo = txtToBondNo.Text;
                    Session["InvReportPara"] = _invRepPara;

                    obj.Shipment_Tracker(_invRepPara);
                    PrintPDF(targetFileName, obj._shipmentTracker);
                }
                if (rad02.Checked == true)  
                {//Dulaj 2018/Dec/11

                    int type = 0;
                    if (RadioButtonUser.Checked)
                    {
                        type = 1;
                    }
                    if(RadioButtonCompany.Checked)
                    {
                        type = 2;
                    }
                    if (RadioButtonShippingline.Checked)
                    {
                        type = 3;
                    }
                    if(type==0)
                    {
                        DisplayMessage("Please Select a Report!", 2);
                        lbtnView.Enabled = true;
                        return;
                    }
                    List<string> rptComlist = _invRepPara._GlbReportCompanies.Split(',').ToList<string>();
                    if (rptComlist[0]=="")
                    {
                        DisplayMessage("Please Select a Company(s)!", 2);
                        lbtnView.Enabled = true;
                        return;
                    }
                    _opt = "rad02";
                    _isSelected = true;
                    _invRepPara._GlbReportName = "EntryClearingClrUser.rpt";
                    Session["GlbReportName"] = "EntryClearingClrUser.rpt";
                    _invRepPara._GlbReportHeading = _newCom.Mc_it_powered;
                    _invRepPara._GlbDocNo = txtToBondNo.Text;
                    Session["InvReportPara"] = _invRepPara;
                    if (type == 1)
                    {
                        obj.ClearingEntry(_invRepPara, type);
                        PrintPDF(targetFileName, obj._entryClearingClrUser);
                    }
                    if(type==2)
                    {
                        obj.ClearingEntry(_invRepPara, type);
                        PrintPDF(targetFileName, obj._entryClearingClrcom);
                    }
                    if (type == 3)
                    {
                        obj.ClearingEntry(_invRepPara, type);
                        PrintPDF(targetFileName, obj._entryClearingClrshipping);
                    }
                }
                if (_isSelected == false)
                {
                    displayMessage("Please select the report!");
                    return;
                }

                //Response.Redirect("ImportReportViewer1.aspx", false);
                //Add by Chamal 11-05-2016
                if (_export == "N")
                {

                    #region send mails
                    if (_aoutomail && chkautomail.Checked)
                    {
                        string _path = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".xls";
                        System.IO.File.Copy(@_path, "\\\\\\192.168.1.20\\scm2rep\\" + txtBlNo.Text.ToString() + ".xls", true);

                        CHNLSVC.MsgPortal.SendBransMails("\\\\\\192.168.1.20\\scm2rep\\" + txtBlNo.Text.ToString() + ".xls", txtBlNo.Text, Session["UserCompanyCode"].ToString());
                        //
                    }
                    #endregion
                    //string url = "<script>window.open('/View/Reports/Imports/ImportReportViewer1.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    String url = "";
                    if (rbpdf.Checked)
                    {
                        url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    }
                    if (rbexel.Checked)
                    {
                        url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xls','_blank');</script>";
                    }
                    if (rbexeldata.Checked)
                    {
                        url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".xls','_blank');</script>";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                lbtnView.Enabled = true;
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "Wharf_Rep", "Run Ok", Session["UserID"].ToString());
               
            }
            catch (Exception err)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "Wharf_Rep", err.Message, Session["UserID"].ToString());
                CHNLSVC.CloseChannel();
                Response.Redirect("~/Error.aspx?Error=" + err.Message + "");
                lbtnView.Enabled = true;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        public string GetMailAddress()
        {
            return ConfigurationManager.ConnectionStrings["MailAdd"].ConnectionString.ToString();
        }
        public string GetMailDispalyName()
        {
            return ConfigurationManager.ConnectionStrings["MailDisp"].ConnectionString.ToString();
        }
        public string GetMailHost()
        {
            return ConfigurationManager.ConnectionStrings["MailHost"].ConnectionString.ToString();
        }

        public string GetMailFooterMsg()
        {
            return ConfigurationManager.ConnectionStrings["MailFooter"].ConnectionString.ToString();
        }
        public string GetMailFooterMsgColor()
        {
             return ConfigurationManager.ConnectionStrings["MailFooterColor"].ConnectionString.ToString();
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            ReportDocument rptDoc = (ReportDocument)_rpt;
            try
            {
                //ReportDocument Rel = new ReportDocument();

                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                if (rbpdf.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                }
                if (rbexel.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                }
                if (rbexeldata.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                }
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                rptDoc.Close();
                rptDoc.Dispose();
            }
        }

        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                System.IO.File.Copy(@"\\" + _filePath, "C:/Download_excel/" + filenamenew + ".xlsx", true);
            }
            else
            {
                DisplayMessage("This file does not exist.", 1);
            }
        }

        private void LoadEmptyGrid()
        {
            dgvCompany.DataSource = null;
            dgvCompany.DataBind();
            dgvAdminTeam.DataSource = null;
            dgvAdminTeam.DataBind();
        }

        private void BindAdminTeam()
        {
            Session["DATAADMIN"] = null;
            DataTable dt = new DataTable();
            dgvAdminTeam.DataSource = null;
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
            dt = CHNLSVC.CommonSearch.GetAdminTeamByCompany(para, null, null);
            if (dt.Rows.Count > 0)
            {
                System.Data.DataColumn newColumn = new System.Data.DataColumn("flag", typeof(System.Int32));
                newColumn.DefaultValue = "0";
                dt.Columns.Add(newColumn);
                Session["DATAADMIN"] = dt;
                dgvAdminTeam.DataSource = (DataTable)Session["DATAADMIN"];
            }
            dgvAdminTeam.DataBind();
        }
        //private void BindCompany()
        //{
        //    DataTable dt = new DataTable();
        //    dgvCompany.DataSource = null;
        //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
        //    dt = CHNLSVC.CommonSearch.GetCompanySearchData(para, null, null);

        //    if (dt.Rows.Count > 0)
        //    {
        //        System.Data.DataColumn newColumn = new System.Data.DataColumn("flag", typeof(System.Int32));
        //        newColumn.DefaultValue = "0";
        //        dt.Columns.Add(newColumn);
        //        Session["DATACOMPANY"] = dt;
        //        dgvCompany.DataSource = (DataTable)Session["DATACOMPANY"];
        //    }
        //    dgvCompany.DataBind();
        //}
        private void BindCompany(string userName)
        {
            dgvCompany.DataSource = null;
            if (!string.IsNullOrEmpty(userName))
            {
                string intime = DateTime.Now.ToString();
                List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(userName);

                if (_systemUserCompanyList != null)
                {
                    dgvCompany.DataSource = _systemUserCompanyList.OrderBy(c => c.SEC_COM_CD).ToList();
                }
            }
            dgvCompany.DataBind();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                //dilshan
                case CommonUIDefiniton.SearchUserControlType.CusdecEntries:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator + "LK" + seperator + ddlRequestType.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdminTeam:
                    {
                        string com_cds = "";
                        foreach (GridViewRow row in dgvCompany.Rows)
                        {
                            CheckBox chk = (CheckBox)row.FindControl("chkCompanyCode");
                            if (chk != null && chk.Checked)
                            {
                                if (com_cds != "")
                                {
                                    com_cds = com_cds + ",";
                                }
                                Label lblCode = (Label)row.FindControl("lblCode");
                                string com_cd = lblCode.Text;
                                com_cds = com_cds + com_cd;
                            }
                        }
                        paramsText.Append(com_cds);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text.Trim() + seperator + txtCat3.Text.Trim() + seperator + txtCat4.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub4:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text.Trim() + seperator + txtCat3.Text.Trim() + seperator + txtCat4.Text.Trim() + seperator + txtCat5.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OrderPlanNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + -1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString().ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GRNNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LoadingPlace:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["frmdate"].ToString() + seperator + Session["todate"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FileNoFrom:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FileNoTo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtpricebook.Text.Trim() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.PINO:
                    {
                        paramsText.Append(BaseCls.GlbPINo + seperator + Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LcNo:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EntryNo:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Tobond_bl:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Country.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Ports:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("BANK" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentTerms:
                    {
                        paramsText.Append("IPM" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void chkCompanyCode_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtCompany = (DataTable)Session["DATACOMPANY"];
            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
            Label compCode = (Label)Row.FindControl("lblCode");
            CheckBox chkSelect = (CheckBox)Row.FindControl("chkCompanyCode");
            bool select = chkSelect.Checked;
            string cellvalue = compCode.Text;

            Session["DATACOMPANY"] = dtCompany;
            BindAdminTeam();
            if (chkAllCompany.Checked)
            {
                //ucLoactionSearch.ClearText();
            }
            else
            {
                foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
                {
                    CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");

                    if (chkCompanyCode.Checked == true)
                    {
                        chkCompanyCode.Checked = false;
                    }
                }
                if (select)
                {
                    foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
                    {
                        CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");
                        Label code = (Label)hiderowbtn.FindControl("lblCode");
                        if (code.Text == cellvalue)
                        {
                            chkCompanyCode.Checked = true;
                        }
                    }
                }
                CheckBox chk = (CheckBox)sender;
                if (chk != null && chk.Checked)
                {
                    //ucLoactionSearch.Company = cellvalue;
                }
                else
                {
                    //ucLoactionSearch.ClearText();
                }
            }
        }
        protected void dgvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvCompany.PageIndex = e.NewPageIndex;
                DataTable dt = (DataTable)Session["DATACOMPANY"];
                if (dt.Rows.Count > 0)
                {
                    dgvCompany.DataSource = dt;
                    dgvCompany.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvAdminTeam_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //try
            //{
            //    dgvAdminTeam.PageIndex = e.NewPageIndex;
            //    DataTable dt = (DataTable)Session["DATAADMIN"];
            //    if (dt.Rows.Count > 0)
            //    {
            //        dgvAdminTeam.DataSource = dt;
            //        dgvAdminTeam.DataBind();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            //}
        }
        public void BindYear()
        {
            ddlYear.DataSource = null;
            int sysYear = 2012;
            int yearCount = DateTime.Today.Year - 2011;
            int[] yearList = new int[yearCount];
            for (int x = 0; x < yearCount; x++)
            {
                yearList[x] = sysYear;
                sysYear++;
            }
            ddlYear.DataSource = yearList;
            ddlYear.DataBind();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(DateTime.Today.Year.ToString()));
            ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(DateTime.Today.Month.ToString()));
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                Session["CAT_Main"] = null;
                Session["CAT_Sub1"] = null;
                Session["CAT_Sub2"] = null;
                Session["CAT_Sub3"] = null;
                Session["CAT_Sub4"] = null;
                Session["Item"] = null;
                Session["Brand"] = null;
                Session["Model"] = null;
                Session["OrderPlanNo"] = null;
                Session["BLHeader"] = null;
                //  Session["Bank"] = null;
                Session["GRNNo"] = null;
                Session["PINO"] = null;
                Session["LcNo"] = null;
                Session["EntryNo"] = null;
                Session["Supplier"] = null;
                Session["ImportAgent"] = null;
                Session["Country"] = null;
                Session["Ports"] = null;
                Session["Tobond_bl"] = null;
                Session["LoadingPlace"] = null;
                Session["FileNoFrom"] = null;
                Session["FileNoTo"] = null;
                Session["Pricebook"] = null;
                Session["Pricelevel"] = null;
                Session["EntryNo"] = null;
                Session["EntryNo01"] = null;
                if (lblSearchType.Text == "CAT_Main")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    dt = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Main"] = dt;
                }
                if (lblSearchType.Text == "CAT_Sub1")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    dt = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub1"] = dt;
                }
                if (lblSearchType.Text == "CAT_Sub2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    dt = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub2"] = dt;
                }
                if (lblSearchType.Text == "CAT_Sub3")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                    dt = CHNLSVC.General.GetItemSubCat4(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub3"] = dt;
                }
                if (lblSearchType.Text == "CAT_Sub4")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                    dt = CHNLSVC.General.GetItemSubCat5(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CAT_Sub4"] = dt;
                }
                if (lblSearchType.Text == "Item")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    dt = CHNLSVC.CommonSearch.GetItemSearchDataMaster(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Item"] = dt;
                }
                if (lblSearchType.Text == "Brand")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    dt = CHNLSVC.CommonSearch.GetItemBrands(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Brand"] = dt;
                }
                if (lblSearchType.Text == "Model")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    dt = CHNLSVC.CommonSearch.GetAllModels(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Model"] = dt;
                }
                if (lblSearchType.Text == "OrderPlanNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
                    dt = CHNLSVC.CommonSearch.SearchOrderPlanNoNew(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["OrderPlanNo"] = dt;
                }
                if (lblSearchType.Text == "BLHeader")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    dt = CHNLSVC.CommonSearch.SearchBLHeader(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BLHeader"] = dt;
                }
                if (lblSearchType.Text == "Bank")
                {
                    dt = (DataTable)Session["Bank"];
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "" + cmbSearchbykey.Text + " LIKE '%" + txtSearchbyword.Text + "%'";
                    if (dv.Count > 0)
                    {
                        dt = dv.ToTable();
                    }
                    //Session["Bank"] = dt;
                }
                if (lblSearchType.Text == "GRNNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNNo);
                    dt = CHNLSVC.CommonSearch.searchGRNDataNew(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["GRNNo"] = dt;
                }
                if (lblSearchType.Text == "PINO")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PINO);
                    dt = CHNLSVC.CommonSearch.Get_All_PINo(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["PINO"] = dt;
                }
                if (lblSearchType.Text == "LcNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LcNo);
                    dt = CHNLSVC.CommonSearch.SEARCH_LC_NO(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["LcNo"] = dt;
                }
                if (lblSearchType.Text == "EntryNo")
                {
                    if (rad01.Checked == false)
                    {
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                        dt = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        Session["EntryNo"] = dt;
                    }
                    else
                    {
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                        dt = CHNLSVC.CommonSearch.SearchCusdecHeader(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        Session["EntryNo"] = dt;
                    }

                }
                if (lblSearchType.Text == "Tobond_bl")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                    dt = CHNLSVC.CommonSearch.SEARCH_TO_BOND_NO(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Tobond_bl"] = dt;
                }
                if (lblSearchType.Text == "LoadingPlace")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoadingPlace);
                    dt = CHNLSVC.CommonSearch.SearchLoadingPlace(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["LoadingPlace"] = dt;
                }
                if (lblSearchType.Text == "FileNoFrom")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FileNoFrom);
                    dt = CHNLSVC.CommonSearch.SearchFileNo(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["FileNoFrom"] = dt;
                }
                if (lblSearchType.Text == "Pricebook")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    dt = CHNLSVC.CommonSearch.SearchPriceBookWeb(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Pricebook"] = dt;
                }
                if (lblSearchType.Text == "Pricelevel")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    dt = CHNLSVC.CommonSearch.GetPriceLevelByBookData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Pricelevel"] = dt;
                }
                if (lblSearchType.Text == "FileNoTo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FileNoTo);
                    dt = CHNLSVC.CommonSearch.SearchFileNo(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["FileNoTo"] = dt;
                }
                if (lblSearchType.Text == "Supplier")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    dt = CHNLSVC.CommonSearch.GetSupplierData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Supplier"] = dt;
                }
                if (lblSearchType.Text == "ImportAgent")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    dt = CHNLSVC.CommonSearch.GetContainerAgent(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ImportAgent"] = dt;
                }
                if (lblSearchType.Text == "Country")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    dt = CHNLSVC.CommonSearch.GetCountrySearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Country"] = dt;
                }
                if (lblSearchType.Text == "Ports")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ports);
                    dt = CHNLSVC.CommonSearch.SearchGetPort(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Ports"] = dt;
                }
                if (lblSearchType.Text == "EntryNo01")
                {
                    if (rad01.Checked == false)
                    {
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                        dt = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        Session["EntryNo01"] = dt;
                    }
                    else
                    {
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                        dt = CHNLSVC.CommonSearch.SearchCusdecHeader(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        Session["EntryNo01"] = dt;
                    }

                }
                if (lblSearchType.Text == "EntryNo02")
                {
                    if (rad01.Checked == false)
                    {
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                        dt = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        Session["EntryNo02"] = dt;
                    }
                    else
                    {
                        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                        dt = CHNLSVC.CommonSearch.SearchCusdecHeader(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                        Session["EntryNo02"] = dt;
                    }

                }

                dgvResultItem.DataSource = null;
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                if (dt.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = dt;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResultItem.DataBind();
                ItemPopup.Show();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvResultItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultItem.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "CAT_Main")
                {
                    _result = (DataTable)Session["CAT_Main"];
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    _result = (DataTable)Session["CAT_Sub1"];
                }
                else if (lblSearchType.Text == "CAT_Sub2")
                {
                    _result = (DataTable)Session["CAT_Sub2"];
                }
                else if (lblSearchType.Text == "CAT_Sub3")
                {
                    _result = (DataTable)Session["CAT_Sub3"];
                }
                else if (lblSearchType.Text == "CAT_Sub4")
                {
                    _result = (DataTable)Session["CAT_Sub4"];
                }
                else if (lblSearchType.Text == "Item")
                {
                    _result = (DataTable)Session["Item"];
                }
                else if (lblSearchType.Text == "Brand")
                {
                    _result = (DataTable)Session["Brand"];
                }
                else if (lblSearchType.Text == "Model")
                {
                    _result = (DataTable)Session["Model"];
                }
                else if (lblSearchType.Text == "OrderPlanNo")
                {
                    _result = (DataTable)Session["OrderPlanNo"];
                }
                else if (lblSearchType.Text == "BLHeader")
                {
                    _result = (DataTable)Session["BLHeader"];
                }
                else if (lblSearchType.Text == "Bank")
                {
                    _result = (DataTable)Session["Bank"];
                }
                else if (lblSearchType.Text == "GRNNo")
                {
                    _result = (DataTable)Session["GRNNo"];
                }
                else if (lblSearchType.Text == "PINO")
                {
                    _result = (DataTable)Session["PINO"];
                }
                else if (lblSearchType.Text == "LcNo")
                {
                    _result = (DataTable)Session["LcNo"];
                }
                else if (lblSearchType.Text == "EntryNo")
                {
                    _result = (DataTable)Session["EntryNo"];
                }
                else if (lblSearchType.Text == "Tobond_bl")
                {
                    _result = (DataTable)Session["Tobond_bl"];
                }
                else if (lblSearchType.Text == "LoadingPlace")
                {
                    _result = (DataTable)Session["LoadingPlace"];
                }
                else if (lblSearchType.Text == "FileNoFrom")
                {
                    _result = (DataTable)Session["FileNoFrom"];
                }
                else if (lblSearchType.Text == "Pricebook")
                {
                    _result = (DataTable)Session["Pricebook"];
                }
                else if (lblSearchType.Text == "Pricelevel")
                {
                    _result = (DataTable)Session["Pricelevel"];
                }
                else if (lblSearchType.Text == "FileNoTo")
                {
                    _result = (DataTable)Session["FileNoTo"];
                }
                else if (lblSearchType.Text == "Supplier")
                {
                    _result = (DataTable)Session["Supplier"];
                }
                else if (lblSearchType.Text == "ImportAgent")
                {
                    _result = (DataTable)Session["ImportAgent"];
                }
                else if (lblSearchType.Text == "Country")
                {
                    _result = (DataTable)Session["Country"];
                }
                else if (lblSearchType.Text == "Ports")
                {
                    _result = (DataTable)Session["Ports"];
                }
                else if (lblSearchType.Text == "EntryNo01")
                {
                    _result = (DataTable)Session["EntryNo01"];
                }
                else if (lblSearchType.Text == "EntryNo02")
                {
                    _result = (DataTable)Session["EntryNo02"];
                }
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;

                }

                else
                {
                    dgvResultItem.DataSource = null;
                }
                dgvResultItem.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                ItemPopup.Show();

            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvResultItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                     "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResultItem.SelectedRow.Cells[1].Text;
                string decs = "";
                if (dgvResultItem.SelectedRow.Cells.Count > 2)
                {
                    decs = dgvResultItem.SelectedRow.Cells[2].Text;
                }

                if (lblSearchType.Text == "CAT_Main")
                {
                    txtCat1.Text = code;
                    txtCat1.ToolTip = decs;
                    chkAllCat1.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    txtCat2.Text = code;
                    txtCat2.ToolTip = decs;
                    chkAllCat2.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub2")
                {
                    txtCat3.Text = code;
                    txtCat3.ToolTip = decs;
                    chkAllCat3.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub3")
                {
                    txtCat4.Text = code;
                    txtCat4.ToolTip = decs;
                    chkAllCat4.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub4")
                {
                    txtCat5.Text = code;
                    txtCat5.ToolTip = decs;
                    chkAllCat5.Checked = false;
                }
                else if (lblSearchType.Text == "Item")
                {
                    txtItemCode.Text = code;
                    txtItemCode.ToolTip = decs;
                    chkAllItemCode.Checked = false;
                }
                else if (lblSearchType.Text == "Brand")
                {
                    txtBrand.Text = code;
                    txtBrand.ToolTip = decs;
                    chkAllBrand.Checked = false;
                }
                else if (lblSearchType.Text == "Model")
                {
                    txtModel.Text = code;
                    txtModel.ToolTip = decs;
                    chkAllModel.Checked = false;
                }
                else if (lblSearchType.Text == "OrderPlanNo")
                {
                    txtOrderPlanNo.Text = code;
                    chkAllOrPlNo.Checked = false;
                }
                else if (lblSearchType.Text == "BLHeader")
                {
                    txtBlNo.Text = code;
                    chkAllBlNo.Checked = false;
                    txtBlNo_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Bank")
                {
                    txtBankNo.Text = code;
                    txtBankNo.ToolTip = decs;
                    chkAllBankNo.Checked = false;
                }
                else if (lblSearchType.Text == "GRNNo")
                {
                    txtGrnNo.Text = code;
                    chkAllGrnNo.Checked = false;
                }
                else if (lblSearchType.Text == "PINO")
                {
                    txtPlNo.Text = code;
                    chkAllPLNo.Checked = false;
                }
                else if (lblSearchType.Text == "EntryNo")
                {
                    txtEntryNo.Text = code;
                    chkAllEntryNo.Checked = false;
                }
                else if (lblSearchType.Text == "Tobond_bl")
                {
                    txtToBondNo.Text = code;
                    chkAllTobond.Checked = false;
                }
                else if (lblSearchType.Text == "LoadingPlace")
                {
                    txtloadingplace.Text = code;
                    //chkAllTobond.Checked = false;
                }
                else if (lblSearchType.Text == "FileNoFrom")
                {
                    txtfilenofrm.Text = code;
                    //chkAllTobond.Checked = false;
                }
                else if (lblSearchType.Text == "Pricebook")
                {
                    txtpricebook.Text = code;
                    //chkAllTobond.Checked = false;
                }
                else if (lblSearchType.Text == "Pricelevel")
                {
                    txtpricelevel.Text = code;
                    //chkAllTobond.Checked = false;
                }
                else if (lblSearchType.Text == "FileNoTo")
                {
                    txtfilenoto.Text = code;
                    //chkAllTobond.Checked = false;
                }
                else if (lblSearchType.Text == "LcNo")
                {
                    txtLcNo.Text = code;
                    chkAllLcNo.Checked = false;
                }

                else if (lblSearchType.Text == "Supplier")
                {
                    txtSupplier.Text = code;
                    chkAllSupplier.Checked = false;
                }
                else if (lblSearchType.Text == "ImportAgent")
                {
                    txtAgent.Text = code;
                    chkAllAgent.Checked = false;
                }
                else if (lblSearchType.Text == "Country")
                {
                    txtCountry.Text = code;
                    chkAllCountry.Checked = false;
                }
                else if (lblSearchType.Text == "Ports")
                {
                    txtPort.Text = code;
                    chkAllPort.Checked = false;
                }
                else if (lblSearchType.Text == "EntryNo01")
                {
                    EntryNumber01TextBox.Text = code;
                    chkAllEntryNo.Checked = false;
                }
                else if (lblSearchType.Text == "EntryNo02")
                {
                    EntryNumber02TextBox.Text = code;
                    chkAllEntryNo.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeCat1_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Main";
                Session["CAT_Main"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Main"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }



        protected void lbtnSeCat2_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub1";
                Session["CAT_Sub1"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub1"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeCat3_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub2";
                Session["CAT_Sub2"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub2"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeCat4_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub3";
                Session["CAT_Sub3"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _result = CHNLSVC.General.GetItemSubCat4(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub3"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeCat5_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub4";
                Session["CAT_Sub4"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                DataTable _result = CHNLSVC.General.GetItemSubCat5(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CAT_Sub4"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeItemCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item";
                Session["Item"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Item"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtSeBrand_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Brand";
                Session["Brand"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Brand"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeModel_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Model";
                Session["Model"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Model"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeOrPlNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "OrderPlanNo";
                Session["OrderPlanNo"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchOrderPlanNoNew(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["OrderPlanNo"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeBankNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Bank";
                Session["Bank"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.SearchBank(para);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Bank"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSePlNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "PINO";
                Session["PINO"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PINO);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_PINo(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["PINO"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeLcNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "LcNo";
                Session["LcNo"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LcNo);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_LC_NO(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["LcNo"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeBlNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BLHeader";
                Session["BLHeader"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable _result = CHNLSVC.CommonSearch.SearchBLHeader(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["BLHeader"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeGrnNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "GRNNo";
                Session["GRNNo"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNNo);
                DataTable _result = CHNLSVC.CommonSearch.searchGRNDataNew(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["GRNNo"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnEntryNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rad01.Checked == true)
                {
                    lblSearchType.Text = "EntryNo";
                    Session["EntryNo"] = null;
                    btnRefSearch();
                }
                else
                {
                    //btnRefSearch();
                    lblSearchType.Text = "EntryNo";
                    Session["EntryNo"] = null;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, null, null);
                    dgvResultItem.DataSource = null;
                    if (_result.Rows.Count > 0)
                    {
                        dgvResultItem.DataSource = _result;
                        Session["EntryNo"] = _result;
                        BindUCtrlDDLData(_result);
                    }
                    else
                    {
                        dgvResultItem.DataSource = null;
                    }
                    dgvResultItem.DataBind();
                    txtSearchbyword.Text = "";
                    txtSearchbyword.Focus();
                    ItemPopup.Show();
                    if (dgvResultItem.PageIndex > 0)
                    { dgvResultItem.SetPageIndex(0); }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnEntry01No_Click(object sender, EventArgs e)
        {
            try
            {
                if (rad01.Checked == true)
                {
                    lblSearchType.Text = "EntryNo01";
                    Session["EntryNo01"] = null;
                    btnRefSearchEntry01();
                }
                else
                {
                    //btnRefSearch();
                    lblSearchType.Text = "EntryNo01";
                    Session["EntryNo01"] = null;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, null, null);
                    dgvResultItem.DataSource = null;
                    if (_result.Rows.Count > 0)
                    {
                        dgvResultItem.DataSource = _result;
                        Session["EntryNo01"] = _result;
                        BindUCtrlDDLData(_result);
                    }
                    else
                    {
                        dgvResultItem.DataSource = null;
                    }
                    dgvResultItem.DataBind();
                    txtSearchbyword.Text = "";
                    txtSearchbyword.Focus();
                    ItemPopup.Show();
                    if (dgvResultItem.PageIndex > 0)
                    { dgvResultItem.SetPageIndex(0); }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void lbtnEntry02No_Click(object sender, EventArgs e)
        {
            try
            {
                if (rad01.Checked == true)
                {
                    lblSearchType.Text = "EntryNo02";
                    Session["EntryNo02"] = null;
                    btnRefSearchEntry02();
                }
                else
                {
                    //btnRefSearch();
                    lblSearchType.Text = "EntryNo02";
                    Session["EntryNo02"] = null;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, null, null);
                    dgvResultItem.DataSource = null;
                    if (_result.Rows.Count > 0)
                    {
                        dgvResultItem.DataSource = _result;
                        Session["EntryNo02"] = _result;
                        BindUCtrlDDLData(_result);
                    }
                    else
                    {
                        dgvResultItem.DataSource = null;
                    }
                    dgvResultItem.DataBind();
                    txtSearchbyword.Text = "";
                    txtSearchbyword.Focus();
                    ItemPopup.Show();
                    if (dgvResultItem.PageIndex > 0)
                    { dgvResultItem.SetPageIndex(0); }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void chkAllCat1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat1.Checked)
            {
                txtCat1.Enabled = false;
                txtCat1.Text = "";
                listCat1.Items.Clear();
            }
            else
            {
                txtCat1.Enabled = true;
            }
        }
        protected void chkAllCat2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat2.Checked)
            {
                txtCat2.Enabled = false;
                txtCat2.Text = "";
                listCat2.Items.Clear();
            }
            else
            {
                txtCat2.Enabled = true;
            }
        }

        protected void chkAllCat3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat3.Checked)
            {
                txtCat3.Enabled = false;
                txtCat3.Text = "";
                listCat3.Items.Clear();
            }
            else
            {
                txtCat3.Enabled = true;
            }
        }

        protected void chkAllCat4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat4.Checked)
            {
                txtCat4.Enabled = false;
                txtCat4.Text = "";
                listCat4.Items.Clear();
            }
            else
            {
                txtCat4.Enabled = true;
            }
        }

        protected void chkAllCat5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCat5.Checked)
            {
                txtCat5.Enabled = false;
                txtCat5.Text = "";
                listCat5.Items.Clear();
            }
            else
            {
                txtCat5.Enabled = true;
            }
        }

        protected void chkAllItemCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllItemCode.Checked)
            {
                txtItemCode.Enabled = false;
                txtItemCode.Text = "";
                listItemCode.Items.Clear();
            }
            else
            {
                txtItemCode.Enabled = true;
            }
        }

        protected void chkAllBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBrand.Checked)
            {
                txtBrand.Enabled = false;
                txtBrand.Text = "";
                listBrand.Items.Clear();
            }
            else
            {
                txtBrand.Enabled = true;
            }
        }

        protected void chkAllModel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllModel.Checked)
            {
                txtModel.Enabled = false;
                txtModel.Text = "";
                listModel.Items.Clear();
            }
            else
            {
                txtModel.Enabled = true;
            }
        }

        protected void chkAllOrPlNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllOrPlNo.Checked)
            {
                txtOrderPlanNo.Text = "";
            }
        }

        protected void chkAllBankNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBankNo.Checked)
            {
                txtBankNo.Text = "";
            }
        }

        protected void chkAllPLNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPLNo.Checked)
            {
                txtPlNo.Text = "";
            }
        }

        protected void chkAllLcNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllLcNo.Checked)
            {
                txtLcNo.Text = "";
            }
        }

        protected void chkAllBlNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBlNo.Checked)
            {
                txtBlNo.Text = "";
            }
        }

        protected void chkAllGrnNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllGrnNo.Checked)
            {
                txtGrnNo.Text = "";
            }
        }

        protected void chkAllEntryNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllEntryNo.Checked)
            {
                txtEntryNo.Text = "";
            }
        }

        protected void lbtnAddCat1_Click(object sender, EventArgs e)
        {
            if (txtCat1.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat1.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat1.Items)
                {
                    if (list.Text == txtCat1.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat1.Text);
                        listCat1.Items.Add(listItem);
                        chkAllCat1.Checked = false;
                        txtCat1.Text = "";
                    }
                    else
                    {
                        txtCat1.Focus();
                        displayMessage("Please enter a valid category 1 !!!");
                    }
                }
                else
                {
                    txtCat1.Focus();
                    displayMessage(txtCat1.Text + " is already added");
                }
            }
            else
            {
                txtCat1.Focus();
                displayMessage("Please select item category 1");
            }
        }

        protected void lbtnAddCat2_Click(object sender, EventArgs e)
        {
            if (txtCat2.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat2.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat2.Items)
                {
                    if (list.Text == txtCat2.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat2.Text);
                        listCat2.Items.Add(listItem);
                        chkAllCat2.Checked = false;
                        txtCat2.Text = "";
                    }
                    else
                    {
                        txtCat2.Focus(); displayMessage("Please enter a valid category 2 !!!");

                    }
                }
                else
                {
                    txtCat2.Focus(); displayMessage(txtCat2.Text + " is already added");

                }
            }
            else
            {
                txtCat2.Focus(); displayMessage("Please select category 2");

            }
        }

        protected void lbtnAddCat3_Click(object sender, EventArgs e)
        {
            if (txtCat3.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat3.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat3.Items)
                {
                    if (list.Text == txtCat3.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat3.Text);
                        listCat3.Items.Add(listItem);
                        chkAllCat3.Checked = false;
                        txtCat3.Text = "";
                    }
                    else
                    {
                        txtCat3.Focus(); displayMessage("Please enter a valid category 3 !!!");

                    }
                }
                else
                {
                    txtCat3.Focus(); displayMessage(txtCat3.Text + " is already added");

                }
            }
            else
            {
                txtCat3.Focus(); displayMessage("Please select category 3");

            }
        }

        protected void lbtnAddCat4_Click(object sender, EventArgs e)
        {
            if (txtCat4.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _result = CHNLSVC.General.GetItemSubCat4(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat4.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat4.Items)
                {
                    if (list.Text == txtCat4.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat4.Text);
                        listCat4.Items.Add(listItem);
                        chkAllCat4.Checked = false;
                        txtCat4.Text = "";
                    }
                    else
                    {
                        txtCat4.Focus(); displayMessage("Please enter a valid category 4 !!!");

                    }
                }
                else
                {
                    txtCat4.Focus(); displayMessage(txtCat4.Text + " is already added");

                }
            }
            else
            {
                txtCat4.Focus(); displayMessage("Please select category 4");

            }
        }

        protected void lbtnAddCat5_Click(object sender, EventArgs e)
        {
            if (txtCat5.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                DataTable _result = CHNLSVC.General.GetItemSubCat5(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat5.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCat5.Items)
                {
                    if (list.Text == txtCat5.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtCat5.Text);
                        listCat5.Items.Add(listItem);
                        chkAllCat5.Checked = false;
                        txtCat5.Text = "";
                    }
                    else
                    {
                        txtCat5.Focus(); displayMessage("Please enter a valid category 5 !!!");

                    }
                }
                else
                {
                    txtCat5.Focus(); displayMessage(txtCat5.Text + " is already added");

                }
            }
            else
            {
                txtCat5.Focus(); displayMessage("Please select category 5");

            }
        }

        protected void lbtnAddItemCode_Click(object sender, EventArgs e)
        {
            if (txtItemCode.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(para, "Item", txtItemCode.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Item"].ToString()))
                    {
                        if (txtItemCode.Text == row["Item"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listItemCode.Items)
                {
                    if (list.Text == txtItemCode.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtItemCode.Text);
                        listItemCode.Items.Add(listItem);
                        chkAllItemCode.Checked = false;
                        txtItemCode.Text = "";
                    }
                    else
                    {
                        displayMessage("Please enter a valid item code !!!");
                        txtItemCode.Focus();
                    }
                }
                else
                {
                    displayMessage(txtItemCode.Text + " is already added");
                    txtItemCode.Focus();
                }
            }
            else
            {
                displayMessage("Please select item code ");
                txtItemCode.Focus();
            }
        }

        protected void lbtnAddBrand_Click(object sender, EventArgs e)
        {
            if (txtBrand.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtBrand.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listBrand.Items)
                {
                    if (list.Text == txtBrand.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtBrand.Text);
                        listBrand.Items.Add(listItem);
                        chkAllBrand.Checked = false;
                        txtBrand.Text = "";
                    }
                    else
                    {
                        txtBrand.Focus(); displayMessage("Please enter a valid brand !!!");
                    }
                }
                else
                {
                    txtBrand.Focus(); displayMessage(txtBrand.Text + " is already added ");
                }
            }
            else
            {
                txtBrand.Focus(); displayMessage("Please select the brand");
            }
        }

        protected void lbtnAddModel_Click(object sender, EventArgs e)
        {
            if (txtModel.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtModel.Text == row["Code"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listModel.Items)
                {
                    if (list.Text == txtModel.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtModel.Text);
                        listModel.Items.Add(listItem);
                        chkAllModel.Checked = false;
                        txtModel.Text = "";
                    }
                    else
                    {
                        txtModel.Focus(); displayMessage("Please enter a valid model !!!");
                    }
                }
                else
                {
                    txtModel.Focus(); displayMessage(txtModel.Text + " is already added ");
                }
            }
            else
            {
                txtModel.Focus(); displayMessage("Please select the model");

            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            if (txtCat1.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat1.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtCat1.ToolTip = b2 ? toolTip : "";
            }
            else
            {
                txtCat1.ToolTip = "";
            }
        }

        protected void txtCat2_TextChanged(object sender, EventArgs e)
        {
            if (txtCat2.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat2.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtCat2.ToolTip = b2 ? toolTip : "";
            }
            else
            {
                txtCat2.ToolTip = "";
            }
        }

        protected void txtCat3_TextChanged(object sender, EventArgs e)
        {
            if (txtCat3.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat3.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtCat3.ToolTip = b2 ? toolTip : "";
            }
            else
            {
                txtCat3.ToolTip = "";
            }
        }

        protected void txtCat4_TextChanged(object sender, EventArgs e)
        {
            if (txtCat4.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _result = CHNLSVC.General.GetItemSubCat4(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat4.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtCat4.ToolTip = b2 ? toolTip : "";
            }
            else
            {
                txtCat4.ToolTip = "";
            }
        }

        protected void txtCat5_TextChanged(object sender, EventArgs e)
        {
            if (txtCat5.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
                DataTable _result = CHNLSVC.General.GetItemSubCat5(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCat5.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtCat5.ToolTip = b2 ? toolTip : "";
            }
            else
            {
                txtCat5.ToolTip = "";
            }
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (txtItemCode.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Item"].ToString()))
                    {
                        if (txtItemCode.Text == row["Item"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtItemCode.ToolTip = b2 ? toolTip : "";
            }
            else
            {
                txtItemCode.ToolTip = "";
            }
        }

        protected void txtBrand_TextChanged(object sender, EventArgs e)
        {
            if (txtBrand.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtBrand.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtBrand.ToolTip = b2 ? toolTip : "";
            }
            else
            {
                txtBrand.ToolTip = "";
            }
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            if (txtModel.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtModel.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtModel.ToolTip = b2 ? toolTip : "";
            }
            else
            {
                txtModel.ToolTip = "";
            }
        }

        protected void txtOrderPlanNo_TextChanged(object sender, EventArgs e)
        {
            if (txtOrderPlanNo.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchOrderPlanNoNew(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["ORDER PLAN NO"].ToString()))
                    {
                        if (txtOrderPlanNo.Text == row["ORDER PLAN NO"].ToString())
                        {
                            b2 = true;
                            toolTip = row["REFERENCE NO"].ToString();
                            return;
                        }
                    }
                }
                txtOrderPlanNo.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid order plan number !!!')", true);
                    txtOrderPlanNo.Text = "";
                    txtOrderPlanNo.Focus();
                    return;
                }
            }
            else
            {
                txtOrderPlanNo.ToolTip = "";
            }
        }

        protected void txtBankNo_TextChanged(object sender, EventArgs e)
        {
            if (txtBankNo.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtBankNo.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtBankNo.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid bank !!!')", true);
                    txtBankNo.Text = "";
                    txtBankNo.Focus();
                    return;
                }
            }
            else
            {
                txtBankNo.ToolTip = "";
            }
        }

        protected void txtPlNo_TextChanged(object sender, EventArgs e)
        {
            if (txtPlNo.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PINO);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_PINo(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["PINO"].ToString()))
                    {
                        if (txtPlNo.Text == row["PINO"].ToString())
                        {
                            b2 = true;
                            toolTip = row["REFERENCES"].ToString();
                            return;
                        }
                    }
                }
                txtPlNo.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid PI number !!!')", true);
                    txtPlNo.Text = "";
                    txtPlNo.Focus();
                    return;
                }
            }
            else
            {
                txtPlNo.ToolTip = "";
            }
        }

        protected void txtLcNo_TextChanged(object sender, EventArgs e)
        {
            if (txtLcNo.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LcNo);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_LC_NO(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["FACILITY NO"].ToString()))
                    {
                        if (txtLcNo.Text == row["FACILITY NO"].ToString())
                        {
                            b2 = true;
                            toolTip = row["REF NO"].ToString();
                            return;
                        }
                    }
                }
                txtLcNo.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid LC number !!!')", true);
                    txtLcNo.Text = "";
                    txtLcNo.Focus();
                    return;
                }
            }
            else
            {
                txtLcNo.ToolTip = "";
            }
        }

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
        }

            
        private void SetSparePartsAmt()
        {
            txtasscharg.Text = "0";
            txtoverhd.Text = "0";
            txtmarkValue.Text = "0";
            txtfcstRate.Text = "0";
            txtbuyingrate.Text = "0";
            txtcostingRate.Text = "0";
        }
        protected void txtGrnNo_TextChanged(object sender, EventArgs e)
        {
            if (txtGrnNo.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNNo);
                DataTable _result = CHNLSVC.CommonSearch.searchGRNDataNew(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["DOCUMENT"].ToString()))
                    {
                        if (txtGrnNo.Text == row["DOCUMENT"].ToString())
                        {
                            b2 = true;
                            toolTip = row["REFERENCE NO"].ToString();
                            return;
                        }
                    }
                }
                txtGrnNo.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid GRN number !!!')", true);
                    txtGrnNo.Text = "";
                    txtGrnNo.Focus();
                    return;
                }
            }
            else
            {
                txtGrnNo.ToolTip = "";
            }
        }

        protected void txtEntryNo_TextChanged(object sender, EventArgs e)
        {
            if (txtEntryNo.Text != "")
            {
                if (rad01.Checked == true)
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.GET_CUSDEC_NO(txtEntryNo.Text);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["CUSDEC NO"].ToString()))
                        {
                            if (txtEntryNo.Text == row["CUSDEC NO"].ToString())
                            {
                                b2 = true;
                                toolTip = row["CUSDEC NO"].ToString();
                                return;
                            }
                        }
                    }
                    txtEntryNo.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Cus Dec number !!!')", true);
                        txtEntryNo.Text = "";
                        txtEntryNo.Focus();
                        return;
                    }
                }
                else
                {

                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["ENTRY NO"].ToString()))
                        {
                            if (txtEntryNo.Text == row["ENTRY NO"].ToString())
                            {
                                b2 = true;
                                toolTip = row["ENTRY NO"].ToString();
                                return;
                            }
                        }
                    }
                    txtEntryNo.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Entry number !!!')", true);
                        txtEntryNo.Text = "";
                        txtEntryNo.Focus();
                        return;
                    }
                }
            }
            else
            {
                txtEntryNo.ToolTip = "";
            }
        }
        protected void txtEntryNo01_TextChanged(object sender, EventArgs e)
        {
            if (EntryNumber01TextBox.Text != "")
            {
                if (rad01.Checked == true)
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.GET_CUSDEC_NO(EntryNumber01TextBox.Text);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["CUSDEC NO"].ToString()))
                        {
                            if (txtEntryNo.Text == row["CUSDEC NO"].ToString())
                            {
                                b2 = true;
                                toolTip = row["CUSDEC NO"].ToString();
                                return;
                            }
                        }
                    }
                    EntryNumber01TextBox.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Cus Dec number !!!')", true);
                        EntryNumber01TextBox.Text = "";
                        EntryNumber01TextBox.Focus();
                        return;
                    }
                }
                else
                {

                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["ENTRY NO"].ToString()))
                        {
                            if (EntryNumber01TextBox.Text == row["ENTRY NO"].ToString())
                            {
                                b2 = true;
                                toolTip = row["ENTRY NO"].ToString();
                                return;
                            }
                        }
                    }
                    EntryNumber01TextBox.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Entry number !!!')", true);
                        //       EntryNumber01TextBox.Text = "";
                        //      EntryNumber01TextBox.Focus();
                        //     return;
                    }
                }
            }
            else
            {
                EntryNumber01TextBox.ToolTip = "";
            }
        }
        protected void txtEntryNo02_TextChanged(object sender, EventArgs e)
        {
            if (EntryNumber02TextBox.Text != "")
            {
                if (rad01.Checked == true)
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.GET_CUSDEC_NO(EntryNumber02TextBox.Text);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["CUSDEC NO"].ToString()))
                        {
                            if (EntryNumber02TextBox.Text == row["CUSDEC NO"].ToString())
                            {
                                b2 = true;
                                toolTip = row["CUSDEC NO"].ToString();
                                return;
                            }
                        }
                    }
                    EntryNumber02TextBox.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Cus Dec number !!!')", true);
                        EntryNumber02TextBox.Text = "";
                        EntryNumber02TextBox.Focus();
                        return;
                    }
                }
                else
                {

                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["ENTRY NO"].ToString()))
                        {
                            if (EntryNumber02TextBox.Text == row["ENTRY NO"].ToString())
                            {
                                b2 = true;
                                toolTip = row["ENTRY NO"].ToString();
                                return;
                            }
                        }
                    }
                    EntryNumber02TextBox.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Entry number !!!')", true);
                        //  EntryNumber02TextBox.Text = "";
                        //  EntryNumber02TextBox.Focus();
                        //   return;
                    }
                }
            }
            else
            {
                EntryNumber02TextBox.ToolTip = "";
            }
        }

        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            //if (txtSupplier.Text != "")
            //{
            //    bool b2 = false;
            //    string toolTip = "";
            //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            //    DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(para, null, null);
            //    foreach (DataRow row in _result.Rows)
            //    {
            //        if (!string.IsNullOrEmpty(row["Code"].ToString()))
            //        {
            //            if (txtSupplier.Text == row["Code"].ToString())
            //            {
            //                b2 = true;
            //                toolTip = row["Name"].ToString();
            //                return;
            //            }
            //        }
            //    }
            //    txtSupplier.ToolTip = b2 ? toolTip : "";
            //    if (!b2)
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid supplier !!!')", true);
            //        txtSupplier.Text = "";
            //        txtSupplier.Focus();
            //        return;
            //    }
            //}
            //else
            //{
            //    txtSupplier.ToolTip = "";
            //}
        }

        protected void txtAgent_TextChanged(object sender, EventArgs e)
        {
            if (txtAgent.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable _result = CHNLSVC.CommonSearch.GetContainerAgent(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtAgent.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtAgent.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid agent !!!')", true);
                    txtAgent.Text = "";
                    txtAgent.Focus();
                    return;
                }
            }
            else
            {
                txtAgent.ToolTip = "";
            }
        }

        protected void txtCountry_TextChanged(object sender, EventArgs e)
        {
            if (txtCountry.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtCountry.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtCountry.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid country !!!')", true);
                    txtCountry.Text = "";
                    txtCountry.Focus();
                    return;
                }
            }
            else
            {
                txtCountry.ToolTip = "";
            }
        }

        protected void txtPort_TextChanged(object sender, EventArgs e)
        {
            if (txtPort.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ports);
                DataTable _result = CHNLSVC.CommonSearch.SearchGetPort(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtPort.Text == row["Code"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            return;
                        }
                    }
                }
                txtPort.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid port !!!')", true);
                    txtPort.Text = "";
                    txtPort.Focus();
                    return;
                }
            }
            else
            {
                txtPort.ToolTip = "";
            }
        }

        protected void chkAllSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSupplier.Checked)
            {
                txtSupplier.Text = "";
            }
        }

        protected void chkAllAgent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllAgent.Checked)
            {
                txtAgent.Text = "";
            }
        }

        protected void chkAllCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllCountry.Checked)
            {
                txtCountry.Text = "";
            }
        }

        protected void chkAllPort_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPort.Checked)
            {
                txtPort.Text = "";
            }
        }

        protected void lbtnSeSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Supplier";
                Session["Supplier"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Supplier"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeAgent_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ImportAgent";
                Session["ImportAgent"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable _result = CHNLSVC.CommonSearch.GetContainerAgent(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ImportAgent"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSeCountry_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Country";
                Session["Country"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Country"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnSePort_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Ports";
                Session["Ports"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Ports);
                DataTable _result = CHNLSVC.CommonSearch.SearchGetPort(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Ports"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void chkAllCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16031))
            {
                chkAllCompany.Checked = false;
                displayMessage("You don't have the permission.\nPermission Code :- 16031");
                //return;
            }
            else
            {
                if (dgvAdminTeam.Rows.Count > 0)
                {
                    BindAdminTeam();
                }
                if (!chkAllCompany.Checked)
                {
                    dgvAdminTeam.Columns[1].Visible = false;
                    UnCheckAllCompanyGridCombo();
                    BindAdminTeam();
                    //ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                    //locationPanel.Enabled = true;
                }
                else
                {
                    dgvAdminTeam.Columns[1].Visible = true;
                    UnCheckAllCompanyGridCombo();
                    BindAdminTeam();
                    //ucLoactionSearch.ClearText();
                    //locationPanel.Enabled = false;
                }
                //chklstbox.Items.Clear();
            }
        }

        private void UnCheckAllCompanyGridCombo()
        {
            foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
            {
                CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");

                if (chkCompanyCode.Checked == true)
                {
                    chkCompanyCode.Checked = false;
                }
            }
        }

        private void EnableDisableAdminTeamGridView()
        {
            if (rad01.Checked)
            {
                dgvAdminTeam.Enabled = false;
            }
            else
            {
                dgvAdminTeam.Enabled = true;
            }
        }
        protected void chkAllAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16032))
                {
                    chkAllAdmin.Checked = false;
                    displayMessage("You don't have the permission.\nPermission Code :- 16032");
                    return;
                }
                else
                {
                    if (!chkAllAdmin.Checked)
                    {
                        // dgvAdminTeam.Columns[1].Visible = false;
                        foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                        {
                            CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");

                            if (chkAdminTeam.Checked == true)
                            {
                                chkAdminTeam.Checked = false;
                            }
                            Label lblAdminCode = (Label)hiderowbtn.FindControl("lblAdminCode");
                            //lblAdminCode.Visible = false;
                        }
                        //ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                        //locationPanel.Enabled = true;
                    }
                    else
                    {
                        // dgvAdminTeam.Columns[1].Visible = true;
                        foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                        {
                            CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");

                            if (chkAdminTeam.Checked == true)
                            {
                                chkAdminTeam.Checked = false;
                            }
                        }
                        //ucLoactionSearch.ClearText();
                        //locationPanel.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void chkAdminTeam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
                Label compCode = (Label)Row.FindControl("lblAdminCode");
                CheckBox chkSelect = (CheckBox)Row.FindControl("chkAdminTeam");
                bool select = chkSelect.Checked;
                string cellvalue = compCode.Text;
                if (chkAllAdmin.Checked)
                {
                    //ucLoactionSearch.ClearText();
                }
                else
                {
                    foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                    {
                        CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");
                        if (chkAdminTeam.Checked == true)
                        {
                            chkAdminTeam.Checked = false;
                        }
                    }
                    if (select)
                    {
                        foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                        {
                            CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");
                            Label code = (Label)hiderowbtn.FindControl("lblAdminCode");
                            if (code.Text == cellvalue)
                            {
                                chkAdminTeam.Checked = true;
                            }
                        }
                    }
                    CheckBox chk = (CheckBox)sender;
                    if (chk != null && chk.Checked)
                    {
                        //ucLoactionSearch.Company = cellvalue;
                    }
                    else
                    {
                        //ucLoactionSearch.ClearText();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void chkAllTobond_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllTobond.Checked)
            {
                txtToBondNo.Text = "";
            }
        }

        protected void txtToBondNo_TextChanged(object sender, EventArgs e)
        {
            if (txtToBondNo.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_TO_BOND_NO(para, "Bond No", txtToBondNo.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Bond No"].ToString()))
                    {
                        if (txtToBondNo.Text == row["Bond No"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Bond No"].ToString();
                            return;
                        }
                    }
                }
                txtToBondNo.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid to-bond # !!!')", true);
                    txtToBondNo.Text = "";
                    txtToBondNo.Focus();
                    return;
                }
            }
            else
            {
                txtToBondNo.ToolTip = "";
            }
        }

        protected void lbtnSeToBond_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Tobond_bl";
                Session["Tobond_bl"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_TO_BOND_NO(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Tobond_bl"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDateTime();
        }



        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDateTime();
        }
        protected void listdelete_Click(object sender, EventArgs e)
        {
            if (listPriceLevel.Items.Count > 0)
            {

                listPriceLevel.Items.Clear();
            }
        }

        private void BindDateTime()
        {
            DateTime _date = DateTime.Today;
            Int32 _month = _date.Month;
            Int32 _year = _date.Year;
            if (ddlYear.SelectedIndex > 0)
            {
                _year = Convert.ToInt32(ddlYear.SelectedValue);
            }

            if (ddlMonth.SelectedIndex > 0)
            {
                _month = Convert.ToInt32(ddlMonth.SelectedValue);
            }
            _date = new DateTime(_year, _month, _date.Day);
            txtFromDate.Text = (_date).ToString("dd/MMM/yyyy");
            txtToDate.Text = (_date).ToString("dd/MMM/yyyy");
        }

        private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
       where T : Control
        {
            foreach (Control control in controlCollection)
            {
                //if (control.GetType() == typeof(T))
                if (control is T) // This is cleaner
                    resultCollection.Add((T)control);

                if (control.HasControls())
                    GetControlList(control.Controls, resultCollection);
            }
        }

        private void DesableAllControler()
        {
            List<DropDownList> _dropDownList = new List<DropDownList>();
            GetControlList<DropDownList>(Page.Controls, _dropDownList);
            foreach (var dropDown in _dropDownList)
            {
                var enbCntr = _enbleDdlList.Where(c => c.ID == dropDown.ID).FirstOrDefault();
                if (enbCntr == null)
                {
                    dropDown.Enabled = false;
                }
                else
                {
                    dropDown.Enabled = true;
                }
            }

            List<TextBox> _textBoxList = new List<TextBox>();
            GetControlList<TextBox>(Page.Controls, _textBoxList);
            foreach (var textBox in _textBoxList)
            {
                var enbCntr = _enbleTxtList.Where(c => c.ID == textBox.ID).FirstOrDefault();
                if (enbCntr == null)
                {
                    textBox.Enabled = false;
                }
                else
                {
                    textBox.Enabled = true;
                }
            }

            List<LinkButton> _lbtnList = new List<LinkButton>();
            GetControlList<LinkButton>(Page.Controls, _lbtnList);
            foreach (var lbtn in _lbtnList)
            {
                var enbCntr = _enableLbtnList.Where(c => c.ID == lbtn.ID).FirstOrDefault();
                if (enbCntr == null)
                {
                    lbtn.Enabled = false;
                }
                else
                {
                    lbtn.Enabled = true;
                }
            }

            List<CheckBox> _chkBoxList = new List<CheckBox>();
            GetControlList<CheckBox>(Page.Controls, _chkBoxList);
            foreach (var chk in _chkBoxList)
            {
                var enbCntr = _enbleChkList.Where(c => c.ID == chk.ID).FirstOrDefault();
                if (enbCntr == null)
                {
                    chk.Enabled = false;
                    //chk.Checked = false;
                }
                else
                {
                    chk.Enabled = true;
                }
            }
            chkAllCompany.Checked = false;     //Added by Udesh 09/Oct/2018
            chkAllAdmin.Checked = false;      //Added by Udesh 09/Oct/2018

            List<RadioButton> _radList = new List<RadioButton>();
            GetControlList<RadioButton>(Page.Controls, _radList);
            foreach (var rad in _radList)
            {
                var enbCntr = _enbleRadioList.Where(c => c.ID == rad.ID).FirstOrDefault();
                if (enbCntr == null)
                {
                    rad.Enabled = false;
                    rad.Checked = false;// Udesh  05/Oct/2018
                }
                else
                {
                    rad.Enabled = true;
                }
            }
            List<ListBox> _listBoxList = new List<ListBox>();
            GetControlList<ListBox>(Page.Controls, _listBoxList);
            foreach (var listBox in _listBoxList)
            {
                listBox.Enabled = false;
            }
         

            UnCheckAllCompanyGridCombo();
            EnableDisableAdminTeamGridView();
            BindAdminTeam();
        }

        private void EnableControler()
        {
            foreach (var dropDown in _ddlList)
            {
                dropDown.Enabled = true;
            }
            foreach (var textBox in _txtList)
            {
                textBox.Enabled = true;
            }

            foreach (var lbtn in _lbtnList)
            {
                lbtn.Enabled = true;
            }

            foreach (var chk in _chkList)
            {
                chk.Enabled = true;
            }

            foreach (var rad in _radioList)
            {
                rad.Enabled = true;
            }

            foreach (var listBox in _lstList)
            {
                listBox.Enabled = true;
            }
        }

        protected void rad01_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16132))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16132)");
                rad01.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

        

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtToBondNo);     

      


            chk.Add(chkAllItemCode);
            ddl.Add(ddlRequestType);
            lbtn.Add(lbtnSeToBond);
            
           

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }

        protected void rad02_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16135))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16135)");
                rad02.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);
            rad.Add(RadioButtonUser);
            rad.Add(RadioButtonCompany);
            rad.Add(RadioButtonShippingline);
            chk.Add(chkAllCompany);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtToBondNo);




            chk.Add(chkAllItemCode);
            ddl.Add(ddlRequestType);
            lbtn.Add(lbtnSeToBond);



            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }


        protected void txtloadingplace_TextChanged(object sender, EventArgs e)
        {
            if (txtloadingplace.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoadingPlace);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoadingPlace(para, "LodingPlace", txtloadingplace.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["LodingPlace"].ToString()))
                    {
                        if (txtloadingplace.Text == row["LodingPlace"].ToString())
                        {
                            b2 = true;
                            toolTip = row["LodingPlace"].ToString();
                            return;
                        }
                    }
                }
                txtloadingplace.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Loading Place !!!')", true);
                    txtloadingplace.Text = "";
                    txtloadingplace.Focus();
                    return;
                }
            }
            else
            {
                txtloadingplace.ToolTip = "";
            }
        }

        protected void lbnloadingplace_Click(object sender, EventArgs e)
        {
            try
            {

                lblSearchType.Text = "LoadingPlace";
                Session["LoadingPlace"] = null;

                string frmdate = txtFromDate.Text;
                string todate = txtToDate.Text;

                if (frmdate == "" | todate == "")
                {
                    DisplayMessage("Please select Date", 2);
                }
                else
                {
                    Session["frmdate"] = frmdate;
                    Session["todate"] = todate;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoadingPlace);
                    DataTable _result = CHNLSVC.CommonSearch.SearchLoadingPlace(para, null, null);
                    dgvResultItem.DataSource = null;
                    if (_result.Rows.Count > 0)
                    {
                        dgvResultItem.DataSource = _result;
                        Session["LoadingPlace"] = _result;
                        BindUCtrlDDLData(_result);
                    }
                    else
                    {
                        dgvResultItem.DataSource = null;
                    }
                    dgvResultItem.DataBind();
                    txtSearchbyword.Text = "";
                    txtSearchbyword.Focus();
                    ItemPopup.Show();
                    if (dgvResultItem.PageIndex > 0)
                    { dgvResultItem.SetPageIndex(0); }
                }


            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(txtToDate.Text.ToString()), Convert.ToDateTime(txtFromDate.Text.ToString())) < 0)
            {
                displayMessage("From date cannot exceed to date");

                txtToDate.Text = "";
            }
        }

        //protected void rad13_CheckedChanged(object sender, EventArgs e)
        //{

        protected void txtfilenofrm_TextChanged(object sender, EventArgs e)
        {
            //if (txtfilenofrm.Text != "")
            //{
            //    bool b2 = false;
            //    string toolTip = "";
            //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FileNoFrom);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchFileNo(para, "FileNoFrom", txtfilenofrm.Text);
            //    foreach (DataRow row in _result.Rows)
            //    {
            //        if (!string.IsNullOrEmpty(row["fileno"].ToString()))
            //        {
            //            if (txtfilenofrm.Text == row["fileno"].ToString())
            //            {
            //                b2 = true;
            //                toolTip = row["fileno"].ToString();
            //                return;
            //            }
            //        }
            //    }
            //    txtfilenofrm.ToolTip = b2 ? toolTip : "";
            //    if (!b2)
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid From File No !!!')", true);
            //        txtfilenofrm.Text = "";
            //        txtfilenofrm.Focus();
            //        return;
            //    }
            //}
            //else
            //{
            //    txtfilenofrm.ToolTip = "";
            //}
        }

        protected void lbtnfilefrm_Click(object sender, EventArgs e)
        {
            try
            {

                lblSearchType.Text = "FileNoFrom";
                Session["FileNoFrom"] = null;




                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FileNoFrom);
                DataTable _result = CHNLSVC.CommonSearch.SearchFileNo(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["FileNoFrom"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void txtfilenoto_TextChanged(object sender, EventArgs e)
        {
            //if (txtfilenoto.Text != "")
            //{
            //    bool b2 = false;
            //    string toolTip = "";
            //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FileNoTo);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchFileNo(para, "FileNoTo", txtfilenoto.Text);
            //    foreach (DataRow row in _result.Rows)
            //    {
            //        if (!string.IsNullOrEmpty(row["fileno"].ToString()))
            //        {
            //            if (txtfilenoto.Text == row["fileno"].ToString())
            //            {
            //                b2 = true;
            //                toolTip = row["fileno"].ToString();
            //                return;
            //            }
            //        }
            //    }
            //    txtfilenoto.ToolTip = b2 ? toolTip : "";
            //    if (!b2)
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid From File No !!!')", true);
            //        txtfilenoto.Text = "";
            //        txtfilenoto.Focus();
            //        return;
            //    }
            //}
            //else
            //{
            //    txtfilenoto.ToolTip = "";
            //}
        }

        protected void lbtnfileto_Click(object sender, EventArgs e)
        {
            try
            {

                lblSearchType.Text = "FileNoTo";
                Session["FileNoTo"] = null;




                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FileNoTo);
                DataTable _result = CHNLSVC.CommonSearch.SearchFileNo(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["FileNoTo"] = _result;
                    BindUCtrlDDLData(_result);
                }
                else
                {
                    dgvResultItem.DataSource = null;
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
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void btnDownloadfile_Click(object sender, EventArgs e)
        {
            DownloadFileinSever();
        }
        private void DownloadFileinSever()
        {
            // string name = "C:/Users/subodanam/Desktop/subo1.xlsx";
            string filenamenew = Session["UserID"].ToString();

            string name = "C:/Download_excel/" + filenamenew + ".xlsx";
            string filename = filenamenew + ".xlsx";
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.AddHeader("Content-Type", "application/Excel");
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.End();
            }
            else
            {
                DisplayMessage("This file does not exist.", 1);
            }
        }

        protected void listentry_Click(object sender, EventArgs e)
        {
            if (txtEntryNo.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EntryNo);
                DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENTRY_NO(para, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["ENTRY NO"].ToString()))
                    {
                        if (txtEntryNo.Text == row["ENTRY NO"].ToString())
                        {
                            b2 = true;
                        }
                    }
                }

                foreach (ListItem list in listCommon.Items)
                {
                    if (list.Text == txtEntryNo.Text)
                    {
                        b1 = true;
                    }
                }

                if (!b1)
                {
                    if (b2)
                    {
                        ListItem listItem = new ListItem(txtEntryNo.Text);
                        listCommon.Items.Add(listItem);
                        chkAllEntryNo.Checked = false;
                        txtEntryNo.Text = "";
                    }
                    else
                    {
                        txtEntryNo.Focus();
                        displayMessage("Please enter a valid EntryNo !!!");
                    }
                }
                else
                {
                    txtEntryNo.Focus();
                    displayMessage(txtEntryNo.Text + " is already added");
                }
            }
            else
            {
                txtEntryNo.Focus();
                displayMessage("Please select item Entry No");
            }
        }

        protected void txtpricelevel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnpricelevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtpricebook.Text.ToString()))
                {
                    displayMessage("Please select Price Book");
                }
                else
                {
                    ViewState["Pricelevel"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                    dgvResultItem.DataSource = result;
                    dgvResultItem.DataBind();
                    lblSearchType.Text = "Pricelevel";
                    BindUCtrlDDLData(result);
                    ViewState["Pricelevel"] = result;
                    ItemPopup.Show();
                    txtSearchbyword.Focus();
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnaddpricelvl_Click(object sender, EventArgs e)
        {
            Boolean _insert = true;
            if (txtpricelevel.Text != "" && txtpricebook.Text != "")
            {
                if (listPriceLevel.Items.Count > 0)
                {
                    foreach (ListItem Item in listPriceLevel.Items)
                    {
                        string[] arr = Item.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
                        if (arr[1].ToString().Trim() == txtpricebook.Text && arr[0].ToString().Trim() == txtpricelevel.Text)
                        {
                            _insert = false;
                        }
                    }
                }
                else
                {
                    _insert = true;
                }

                if (_insert == true)
                {
                    ListItem listItem = new ListItem(txtpricelevel.Text + " | " + txtpricebook.Text);
                    listPriceLevel.Items.Add(listItem);
                }
                else
                {
                    displayMessage("Selected Price Book & Level Already Added");
                }
                txtpricelevel.Text = "";
                txtpricebook.Text = "";

                //  ListItem listItem = new ListItem(txtpricelevel.Text+ " | " + txtpricebook.Text);
                //  listPriceLevel.Items.Add(listItem);
                ////  chkAllCat1.Checked = false;
                //  txtpricelevel.Text = "";
                //  txtpricebook.Text = "";


            }
            else
            {
                txtpricelevel.Focus();
                displayMessage("Please select item PriceLevel AND Price Book");
            }
        }

        protected void lbtaddpricebook_Click(object sender, EventArgs e)
        {
            //if (txtpricebook.Text != "")
            //{
            //    bool b1 = false;
            //    bool b2 = false;
            //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchPriceBookWeb(para, null, null);
            //    foreach (DataRow row in _result.Rows)
            //    {
            //        if (!string.IsNullOrEmpty(row["Book"].ToString()))
            //        {
            //            if (txtpricebook.Text == row["Book"].ToString())
            //            {
            //                b2 = true;
            //            }
            //        }
            //    }

            //    foreach (ListItem list in listPriceLevel.Items)
            //    {
            //        if (list.Text == txtpricebook.Text)
            //        {
            //            b1 = true;
            //        }
            //    }

            //    if (!b1)
            //    {
            //        if (b2)
            //        {
            //            ListItem listItem = new ListItem(txtpricebook.Text);
            //            listPriceLevel.Items.Add(listItem);
            //            //  chkAllCat1.Checked = false;
            //            txtpricebook.Text = "";
            //        }
            //        else
            //        {
            //            txtpricebook.Focus();
            //            displayMessage("Please enter a valid PriceBook !!!");
            //        }
            //    }
            //    else
            //    {
            //        txtpricebook.Focus();
            //        displayMessage(txtpricebook.Text + " is already added");
            //    }
            //}
            //else
            //{
            //    txtpricebook.Focus();
            //    displayMessage("Please select item Price Book");
            //}
        }

        protected void lbtnpricebook_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                lblSearchType.Text = "Pricebook";
                ViewState["Pricebook"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable result = CHNLSVC.CommonSearch.SearchPriceBookWeb(SearchParams, null, null);
                dgvResultItem.DataSource = result;
                dgvResultItem.DataBind();
                lblSearchType.Text = "Pricebook";
                BindUCtrlDDLData(result);
                ViewState["Pricebook"] = result;
                ItemPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void txtpricebook_TextChanged(object sender, EventArgs e)
        {

        }

    
        protected void btnRefSearch()
        {
            try
            {
                string _cusdecType = ddlRequestType.SelectedValue; //Request.QueryString["CUSTTYPE"];
                string _cusdecType2 = "N/A";//Request.QueryString["CUSTSTYPE"];
                //ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                DataTable result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                if (_cusdecType2 == "DHL")
                {
                    result = CHNLSVC.CommonSearch.SearchCusdecHeader(SearchParams, "DOC NO", "DAB");
                }
                else
                {
                    result = CHNLSVC.CommonSearch.SearchCusdecHeader(SearchParams, null, null);
                }

                if (result.Rows.Count > 0)
                {
                    if (_cusdecType == "AIR" && _cusdecType2 == "N/A")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("AAB") || r1.Field<string>("DOC NO").Contains("AEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "LR" && _cusdecType2 == "N/A")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("LAB") || r1.Field<string>("DOC NO").Contains("LEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "CCO")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("CAB") | r1.Field<string>("DOC NO").Contains("CCO") | r1.Field<string>("DOC NO").Contains("CEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "PST")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("PAB") || r1.Field<string>("DOC NO").Contains("PEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "DHL")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("DAB") || r1.Field<string>("DOC NO").Contains("DEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                }

                dgvResultItem.DataSource = result;
                dgvResultItem.DataBind();
                //lblvalue.Text = "CusdecEntries";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                Session["EntryNo"] = result;
                BindUCtrlDDLData(result);
                //ViewState["SEARCH"] = result;
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                //Session["IsSearch"] = true;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void btnRefSearchEntry01()
        {
            try
            {
                string _cusdecType = ddlRequestType.SelectedValue; //Request.QueryString["CUSTTYPE"];
                string _cusdecType2 = "N/A";//Request.QueryString["CUSTSTYPE"];
                //ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                DataTable result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                if (_cusdecType2 == "DHL")
                {
                    result = CHNLSVC.CommonSearch.SearchCusdecHeader(SearchParams, "DOC NO", "DAB");
                }
                else
                {
                    result = CHNLSVC.CommonSearch.SearchCusdecHeader(SearchParams, null, null);
                }

                if (result.Rows.Count > 0)
                {
                    if (_cusdecType == "AIR" && _cusdecType2 == "N/A")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("AAB") || r1.Field<string>("DOC NO").Contains("AEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "LR" && _cusdecType2 == "N/A")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("LAB") || r1.Field<string>("DOC NO").Contains("LEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "CCO")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("CAB") | r1.Field<string>("DOC NO").Contains("CCO") | r1.Field<string>("DOC NO").Contains("CEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "PST")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("PAB") || r1.Field<string>("DOC NO").Contains("PEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "DHL")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("DAB") || r1.Field<string>("DOC NO").Contains("DEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                }

                dgvResultItem.DataSource = result;
                dgvResultItem.DataBind();
                //lblvalue.Text = "CusdecEntries";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                Session["EntryNo01"] = result;
                BindUCtrlDDLData(result);
                //ViewState["SEARCH"] = result;
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                //Session["IsSearch"] = true;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void btnRefSearchEntry02()
        {
            try
            {
                string _cusdecType = ddlRequestType.SelectedValue; //Request.QueryString["CUSTTYPE"];
                string _cusdecType2 = "N/A";//Request.QueryString["CUSTSTYPE"];
                //ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                DataTable result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                if (_cusdecType2 == "DHL")
                {
                    result = CHNLSVC.CommonSearch.SearchCusdecHeader(SearchParams, "DOC NO", "DAB");
                }
                else
                {
                    result = CHNLSVC.CommonSearch.SearchCusdecHeader(SearchParams, null, null);
                }

                if (result.Rows.Count > 0)
                {
                    if (_cusdecType == "AIR" && _cusdecType2 == "N/A")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("AAB") || r1.Field<string>("DOC NO").Contains("AEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "LR" && _cusdecType2 == "N/A")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("LAB") || r1.Field<string>("DOC NO").Contains("LEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "CCO")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("CAB") | r1.Field<string>("DOC NO").Contains("CCO") | r1.Field<string>("DOC NO").Contains("CEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "PST")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("PAB") || r1.Field<string>("DOC NO").Contains("PEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                    if (_cusdecType == "AIR" && _cusdecType2 == "DHL")
                    {
                        result = result.AsEnumerable()
                                             .Where(r1 => r1.Field<string>("DOC NO").Contains("DAB") || r1.Field<string>("DOC NO").Contains("DEN")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                                             .CopyToDataTable();
                    }
                }

                dgvResultItem.DataSource = result;
                dgvResultItem.DataBind();
                //lblvalue.Text = "CusdecEntries";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                Session["EntryNo02"] = result;
                BindUCtrlDDLData(result);
                //ViewState["SEARCH"] = result;
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
                //Session["IsSearch"] = true;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResultItem.PageIndex = e.NewPageIndex;
                dgvResultItem.DataSource = null;
                dgvResultItem.DataSource = (DataTable)ViewState["SEARCH"];
                dgvResultItem.DataBind();
                //mpUserPopup.Show(); 
                Session["IsSearch"] = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void radioDetailRep_CheckedChanged(object sender, EventArgs e)
        {
            //Added by Udesh 09/Oct/2018
            if (radioDetailRep.Checked)
            {
                txtPlNo.Enabled = true;
                lbtnSePlNo.Enabled = true;
                chkAllPLNo.Enabled = true;
            }
        }

        protected void radioSummaryRep_CheckedChanged(object sender, EventArgs e)
        {
            //Added by Udesh 09/Oct/2018
            if (radioSummaryRep.Checked)
            {
                txtPlNo.Enabled = false;
                txtPlNo.Text = string.Empty;
                lbtnSePlNo.Enabled = false;
                chkAllPLNo.Enabled = false;
            }
        }

    }
}