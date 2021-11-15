using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects.InventoryNew;
using FastForward.SCMWeb.UserControls;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace FastForward.SCMWeb.View.Reports.Sales
{
    public partial class Sal_Rep : BasePage
    {
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

        public List<ListBox> _enblelSTlList
        {
            get { if (Session["_enblelSTlList"] == null) { return new List<ListBox>(); } else { return (List<ListBox>)Session["_enblelSTlList"]; } }
            set { Session["_enblelSTlList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                //ucProfitCenterSearch. LblLocation.Text = "Profit Center";
                //ucProfitCenterSearch.LblHeading.Text = "Profit Center Details";
                Session["DATAADMIN"] = null;
                Session["DATACOMPANY"] = null;
                BindYear();
                LoadEmptyGrid();
                BindCompany(Session["UserID"].ToString());

                if (Session["UserCompanyCode"] != null)
                {
                    foreach (GridViewRow item in dgvCompany.Rows)
                    {
                        Label lblCode = item.FindControl("lblCode") as Label;
                        CheckBox chkCompanyCode = item.FindControl("chkCompanyCode") as CheckBox;
                        if (lblCode.Text == Session["UserCompanyCode"].ToString())
                        {
                            chkCompanyCode.Checked = true;
                            BindAdminTeam();
                            break;
                        }
                    }
                }
                ucProfitCenterSearch.Company = Session["UserCompanyCode"].ToString();
                txtFromDate.Text = (DateTime.Now.Date.AddMonths(-1)).ToString("dd/MMM/yyyy");
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtAsAt.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtExDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                locationPanel.Enabled = !chkAllCompany.Checked;
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
                txtAgeFrom.Text = "0";
                txtAgeTo.Text = "9999";
                dgvAdminTeam.Columns[1].Visible = false;
                AddEnableCntr();
                DesableAllControler();
                rbRepPDF.Visible = false;
                rbRepExcel.Visible = false;
                rbInvWise.Visible = false;
                rbItemWise.Visible = false;
                rbInvoiceCurrency.Visible = false;
                rbCompanyCurrency.Visible = false;
                txtDiscountFrom.Visible = false;
                txtDiscountTo.Visible = false;
                lblDicountFrom.Visible = false;
                lblDicountTo.Visible = false;
            }
            else
            {

            }
        }

        private void AddEnableCntr()
        {
            List<RadioButton> _rList = new List<RadioButton>();
            _rList.Add(rad01);
            _rList.Add(rad02);
            _rList.Add(rad03);
            _rList.Add(rad04);
            _rList.Add(rad05);
            _rList.Add(rad06);
            _rList.Add(rad07);
            _rList.Add(rad08);
            _rList.Add(rad09);
            _rList.Add(rad10);
            _rList.Add(rad11);
            _rList.Add(rad12);
            _rList.Add(rad13);
            _rList.Add(rad14);
            _rList.Add(rad15);
            _rList.Add(rad16);
            _rList.Add(rad17);
            _rList.Add(rad18);
            _rList.Add(rad19);
            _rList.Add(rbpdf);
            _rList.Add(rbexel);
            _rList.Add(rbexeldata);
            _rList.Add(rbwithoutintercompny);
            _rList.Add(rbboth);
            _rList.Add(rbwithicom);
            _rList.Add(rbRepPDF);
            _rList.Add(rbRepExcel);
            _rList.Add(rbInvWise);
            _rList.Add(rbItemWise);
            _rList.Add(rbInvoiceCurrency);
            _rList.Add(rbCompanyCurrency);

            _enbleRadioList = _rList;

            List<CheckBox> _lChk = new List<CheckBox>();
            _lChk.Add(chkAllCompany);
            _lChk.Add(chkAllAdmin);


            List<TextBox> _lTxt = new List<TextBox>();
            _lTxt.Add(ucProfitCenterSearch.TxtCompany);
            _lTxt.Add(ucProfitCenterSearch.TxtChanel);
            _lTxt.Add(ucProfitCenterSearch.TxtSubChanel);
            _lTxt.Add(ucProfitCenterSearch.TxtAreya);
            _lTxt.Add(ucProfitCenterSearch.TxtRegion);
            _lTxt.Add(ucProfitCenterSearch.TxtZone);
            _lTxt.Add(ucProfitCenterSearch.TxtProfCenter);
            _lTxt.Add(ucProfitCenterSearch.txtSearch);
            _lTxt.Add(txtSearchbyword);
            _enbleTxtList = _lTxt;

            List<LinkButton> _lBtn = new List<LinkButton>();
            _lBtn.Add(lbtnView);
            _lBtn.Add(lbtnClear);
            _lBtn.Add(lbtnSearch);

            _lBtn.Add(ucProfitCenterSearch.lbtnCompany);
            _lBtn.Add(ucProfitCenterSearch.lbtnChanel);
            _lBtn.Add(ucProfitCenterSearch.lbtnSubChanel);
            _lBtn.Add(ucProfitCenterSearch.lbtnAreya);
            _lBtn.Add(ucProfitCenterSearch.lbtnRegion);
            _lBtn.Add(ucProfitCenterSearch.lbtnZone);
            _lBtn.Add(ucProfitCenterSearch.lbtnLocation);
            _lBtn.Add(ucProfitCenterSearch.lbtnImgSearch);
            _lBtn.Add(lbtnSearch);
            _lBtn.Add(lbtnAddLocation);
            _lBtn.Add(btnLocation);
            _lBtn.Add(lbtnLocationAll);
            _lBtn.Add(lbtnLocationNone);
            _lBtn.Add(lbtnLocationClear);
            _lBtn.Add(lbtnRemoveItem);
            _lBtn.Add(btnClearGroup);
            _lBtn.Add(btnDownloadfile);
            _enableLbtnList = _lBtn;

            List<DropDownList> _lDdl = new List<DropDownList>();
            _lDdl.Add(ucProfitCenterSearch.cmbSearchby);
            _lDdl.Add(cmbSearchbykey);
            _enbleDdlList = _lDdl;


            List<ListBox> _lList = new List<ListBox>();
            _lList.Add(listGroup);
            _enblelSTlList = _lList;

            foreach (GridViewRow grdRow in dgvCompany.Rows)
            {
                CheckBox chkCompanyCode = grdRow.FindControl("chkCompanyCode") as CheckBox;
                _lChk.Add(chkCompanyCode);
            }
            _enbleChkList = _lChk;

            foreach (GridViewRow grdRow in dgvAdminTeam.Rows)
            {
                CheckBox chkAdminTeam = grdRow.FindControl("chkAdminTeam") as CheckBox;
                _lChk.Add(chkAdminTeam);
            }
            _enbleChkList = _lChk;


        }
        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
        }

        string _opt = "";
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _isSelected = false;

                Session["GlbReportName"] = string.Empty;
                GlbReportName = string.Empty;

                lbtnView.Enabled = false;

                //kapila 23/12/2015 - to get the company name in the report header
                string com_cds = "";
                string com_desc = "";
                string com_add = "";
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
                com_add = _newCom.Mc_add1 + " " + _newCom.Mc_add2;

                //BaseCls.GlbReportCompName = com_desc;
                //BaseCls.GlbReportCompanies = com_cds;

                //set all common parameters
                //BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                //BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                //BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                //BaseCls.GlbReportSupplier = txtSupplier.Text.ToString();

                //BaseCls.GlbReportCompCode = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportComp = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportProfit = Session["UserDefLoca"].ToString();
                Session["GlbReportType"] = "";
                string _export = "N";

                string vItemCat1 = "";
                string vItemCat2 = "";
                string vItemCat3 = "";
                string vItemCat4 = "";
                string vItemCat5 = "";
                string vItemcode = "";
                string vBrand = "";
                string vModel = "";

                foreach (ListItem Item in listCat1.Items)
                {
                    vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listCat2.Items)
                {
                    vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listCat3.Items)
                {
                    vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listCat4.Items)
                {
                    vItemCat4 = vItemCat4 == "" ? "^" + Item.Text + "$" : vItemCat4 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listCat5.Items)
                {
                    vItemCat5 = vItemCat5 == "" ? "^" + Item.Text + "$" : vItemCat5 + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listItemCode.Items)
                {
                    vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listBrand.Items)
                {
                    vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                }
                foreach (ListItem Item in listModel.Items)
                {
                    vModel = vModel == "" ? "^" + Item.Text + "$" : vModel + "|" + "^" + Item.Text + "$";
                }

                //BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                //BaseCls.GlbReportModel = vModel == "" ? txtModel.Text == "" ? txtModel.Text : "^" + txtModel.Text + "$" : vModel;
                //BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                //BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtCat1.Text == "" ? txtCat1.Text : "^" + txtCat1.Text + "$" : vItemCat1;
                //BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtCat2.Text == "" ? txtCat2.Text : "^" + txtCat2.Text + "$" : vItemCat2;
                //BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtCat3.Text == "" ? txtCat3.Text : "^" + txtCat3.Text + "$" : vItemCat3;
                //BaseCls.GlbReportItemCat4 = vItemCat4 == "" ? txtCat4.Text == "" ? txtCat4.Text : "^" + txtCat4.Text + "$" : vItemCat4;
                //BaseCls.GlbReportItemCat5 = vItemCat5 == "" ? txtCat5.Text == "" ? txtCat5.Text : "^" + txtCat5.Text + "$" : vItemCat5;


                //BaseCls.GlbReportItemStatus = txtItemStat.Text;
                //BaseCls.GlbReportDoc2 = chkWithGit.Checked == true ? "Y" : "N";
                //BaseCls.GlbReportJobStatus = chkWithJob.Checked == true ? "Y" : "N";
                //BaseCls.GlbReportWithRCC = chkWithServiceWIP.Checked == true ? "Y" : "N";
                //BaseCls.GlbReportWithCost = chkWithCostWIP.Checked == true ? Convert.ToInt16(1) : Convert.ToInt16(0);

                //BaseCls.GlbReportDocType = txtDocType.Text;
                //BaseCls.GlbReportDirection = txtDirection.Text;
                //BaseCls.GlbReportDocSubType = txtDocSubType.Text;

                int x = 0;
                foreach (ListItem Item in listGroup.Items)
                {
                    x++;
                    if (Item.Text == "INV")
                    {
                        if (listGroup.Items.Count > x)
                        {
                            displayMessage("Document Number group should be the last group.");
                            lbtnView.Enabled = true;
                            return;
                        }
                    }
                }

                Session["GlbReportBrand"] = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                Session["GlbReportModel"] = vModel == "" ? txtModel.Text == "" ? txtModel.Text : "^" + txtModel.Text + "$" : vModel;
                Session["GlbReportItemCode"] = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                Session["GlbReportItemCat1"] = vItemCat1 == "" ? txtCat1.Text == "" ? txtCat1.Text : "^" + txtCat1.Text + "$" : vItemCat1;
                Session["GlbReportItemCat2"] = vItemCat2 == "" ? txtCat2.Text == "" ? txtCat2.Text : "^" + txtCat2.Text + "$" : vItemCat2;
                Session["GlbReportItemCat3"] = vItemCat3 == "" ? txtCat3.Text == "" ? txtCat3.Text : "^" + txtCat3.Text + "$" : vItemCat3;
                Session["GlbReportItemCat4"] = vItemCat4 == "" ? txtCat4.Text == "" ? txtCat4.Text : "^" + txtCat4.Text + "$" : vItemCat4;
                Session["GlbReportItemCat5"] = vItemCat5 == "" ? txtCat5.Text == "" ? txtCat5.Text : "^" + txtCat5.Text + "$" : vItemCat5;
                Session["GlbReportItemStatus"] = txtItemStat.Text;
                Session["GlbReportDoc2"] = chkWithGit.Checked == true ? "Y" : "N";
                Session["GlbReportJobStatus"] = chkWithJob.Checked == true ? "Y" : "N";
                Session["GlbReportWithRCC"] = chkWithServiceWIP.Checked == true ? "Y" : "N";
                Session["GlbReportWithCost"] = chkWithCostWIP.Checked == true ? Convert.ToInt16(1) : Convert.ToInt16(0);
                Session["GlbReportDocType"] = txtDocType.Text;
                Session["GlbReportDocSubType"] = txtDocSubType.Text;

                Session["GlbReportFromDate"] = Convert.ToDateTime(txtFromDate.Text).Date;
                Session["GlbReportToDate"] = Convert.ToDateTime(txtToDate.Text).Date;
                Session["GlbReportAsAtDate"] = Convert.ToDateTime(txtAsAt.Text).Date;
                Session["GlbReportSupplier"] = txtSupplier.Text.ToString();

                Session["GlbReportCompCode"] = Session["UserCompanyCode"].ToString();
                Session["GlbReportComp"] = Session["UserCompanyCode"].ToString();
                Session["GlbReportProfit"] = Session["UserDefLoca"].ToString();

                Session["GlbReportCompName"] = com_desc;
                Session["GlbReportCompanies"] = com_cds;

                Session["GlbReportDirection"] = string.Empty;
                if (txtDirection.Text == "IN")
                { BaseCls.GlbReportDirection = "1"; Session["GlbReportDirection"] = "1"; }

                if (txtDirection.Text == "OUT")
                { BaseCls.GlbReportDirection = "0"; Session["GlbReportDirection"] = "0"; }

                InvReportPara _invRepPara = new InvReportPara();
                _invRepPara._GlbReportBrand = Session["GlbReportBrand"].ToString();
                _invRepPara._GlbReportModel = Session["GlbReportModel"].ToString();
                _invRepPara._GlbReportItemCode = Session["GlbReportItemCode"].ToString();
                _invRepPara._GlbReportItemCat1 = Session["GlbReportItemCat1"].ToString();
                _invRepPara._GlbReportItemCat2 = Session["GlbReportItemCat2"].ToString();
                _invRepPara._GlbReportItemCat3 = Session["GlbReportItemCat3"].ToString();
                _invRepPara._GlbReportItemCat4 = Session["GlbReportItemCat4"].ToString();
                _invRepPara._GlbReportItemCat5 = Session["GlbReportItemCat5"].ToString();
                _invRepPara._GlbReportItemStatus = Session["GlbReportItemStatus"].ToString();
                _invRepPara._GlbReportDoc2 = Session["GlbReportDoc2"].ToString();
                _invRepPara._GlbReportJobStatus = Session["GlbReportJobStatus"].ToString();
                _invRepPara._GlbReportWithRCC = Session["GlbReportWithRCC"].ToString();
                _invRepPara._GlbReportWithCost = Convert.ToInt16(Session["GlbReportWithCost"]);
                _invRepPara._GlbReportDocType = Session["GlbReportDocType"].ToString();
                _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
                _invRepPara._GlbReportDocSubType = Session["GlbReportDocSubType"].ToString();
                _invRepPara._GlbReportCompName = Session["GlbReportCompName"].ToString();
                _invRepPara._GlbReportCompanies = Session["GlbReportCompanies"].ToString();

                _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString();
                _invRepPara._GlbReportExecutive = txtExecutive.Text.ToString();
                _invRepPara._GlbReportCustomer = txtCustomer.Text.ToString();
                _invRepPara._GlbReportPromotor = txtPromotor.Text.ToString();
                _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportProfit = ucProfitCenterSearch.TxtProfCenter.Text == "" ? "All Profit centers based on user rights" : ucProfitCenterSearch.TxtProfCenter.Text;
                _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
                _invRepPara._GlbUserID = Session["UserID"].ToString();
                _invRepPara._GlbReportWithJob = "N";
                _invRepPara._GlbReportCheckRegDate = 0;
                _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportComp = com_desc;
                _invRepPara._GlbReportCompAddr = com_add;

                string _exec = CHNLSVC.MsgPortal.getSalesRestrictbyUser(Session["UserID"].ToString());
                if (!(_exec == null | _exec == ""))
                {
                    _invRepPara._GlbReportExecutive = _exec;
                }

                int i = 1;
                int j = listGroup.Items.Count;
                _invRepPara._GlbReportGroupPC = 0;
                _invRepPara._GlbReportGroupDLoc = 0;
                _invRepPara._GlbReportGroupDocTp = 0;
                _invRepPara._GlbReportGroupCust = 0;
                _invRepPara._GlbReportGroupExec = 0;
                _invRepPara._GlbReportGroupItem = 0;
                _invRepPara._GlbReportGroupBrand = 0;
                _invRepPara._GlbReportGroupModel = 0;
                _invRepPara._GlbReportGroupCat1 = 0;
                _invRepPara._GlbReportGroupCat2 = 0;
                _invRepPara._GlbReportGroupCat3 = 0;
                _invRepPara._GlbReportGroupCat4 = 0;
                _invRepPara._GlbReportGroupCat5 = 0;
                _invRepPara._GlbReportGroupLastGroup = 0;
                _invRepPara._GlbReportGroupDocNo = 0;
                _invRepPara._GlbReportGroupStockTp = 0;
                _invRepPara._GlbReportGroupPromotor = 0;
                _invRepPara._GlbReportGroupJobNo = 0;
                _invRepPara._GlbReportGroupLastGroupCat = "";
                _invRepPara._GlbReportGroupCheque = 0;
                _invRepPara._GlbReportGroupaccountcode = 0;

                foreach (ListItem Item in listGroup.Items)
                {
                    if (Item.Text == "PC")
                    {
                        _invRepPara._GlbReportGroupPC = i;
                    }
                    if (Item.Text == "DLOC")
                    {
                        _invRepPara._GlbReportGroupDLoc = i;
                    }
                    if (Item.Text == "DTP")
                    {
                        _invRepPara._GlbReportGroupDocTp = i;
                    }
                    if (Item.Text == "CUST")
                    {
                        _invRepPara._GlbReportGroupCust = i;
                    }
                    if (Item.Text == "EXEC")
                    {
                        _invRepPara._GlbReportGroupExec = i;
                    }
                    if (Item.Text == "ITM")
                    {
                        _invRepPara._GlbReportGroupItem = i;
                    }
                    if (Item.Text == "BRND")
                    {
                        _invRepPara._GlbReportGroupBrand = i;
                    }
                    if (Item.Text == "MDL")
                    {
                        _invRepPara._GlbReportGroupModel = i;
                    }
                    if (Item.Text == "CAT1")
                    {
                        _invRepPara._GlbReportGroupCat1 = i;
                    }
                    if (Item.Text == "CAT2")
                    {
                        _invRepPara._GlbReportGroupCat2 = i;
                    }
                    if (Item.Text == "CAT3")
                    {
                        _invRepPara._GlbReportGroupCat3 = i;
                    }
                    if (Item.Text == "CAT4")
                    {
                        _invRepPara._GlbReportGroupCat4 = i;
                    }
                    if (Item.Text == "CAT5")
                    {
                        _invRepPara._GlbReportGroupCat5 = i;
                    }
                    if (Item.Text == "DOC")
                    {
                        _invRepPara._GlbReportGroupDocNo = i;
                    }
                    if (Item.Text == "STK")
                    {
                        _invRepPara._GlbReportGroupStockTp = i;
                    }
                    if (Item.Text == "PRM")
                    {
                        _invRepPara._GlbReportGroupPromotor = i;
                    }
                    if (Item.Text == "CHQ")
                    {
                        _invRepPara._GlbReportGroupCheque = i;
                    }
                    if (Item.Text == "ACC")
                    {
                        _invRepPara._GlbReportGroupaccountcode = i;
                    }
                    _invRepPara._GlbReportGroupLastGroup = j;
                    if (j == i)
                    {
                        _invRepPara._GlbReportGroupLastGroupCat = Item.Text;
                    }
                    i++;
                }
                if (j == 0)
                {
                    _invRepPara._GlbReportGroupPC = 1;
                    _invRepPara._GlbReportGroupItem = 2;
                    _invRepPara._GlbReportGroupLastGroup = 2;
                    _invRepPara._GlbReportGroupLastGroupCat = "ITM";
                }

                _opt = "";
                //if (BaseCls.GlbReportDirection == "IN") BaseCls.GlbReportDirection = "1";
                //if (BaseCls.GlbReportDirection == "OUT") BaseCls.GlbReportDirection = "0";


                update_PC_List_RPTDB();

                clsSales obj = new clsSales();
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
                if (rad01.Checked == true)  // Total Sales - Sanjeewa 2016-04-28
                {

                    int revOrSaleOrAll = 1;
                    //Sale
                    if (rbSale.Checked)
                    {
                        revOrSaleOrAll = 1;
                    }
                    else if (rbReversal.Checked)//Reversal
                    {
                        revOrSaleOrAll = 0;
                    }
                    else //Sale and Reversal 
                    {
                        revOrSaleOrAll = 2;
                    }
                    _invRepPara._GlbReportFromAge = revOrSaleOrAll;

                    if (rbRepPDF.Checked == true)
                    {
                        _opt = "rad01";
                        _isSelected = true;

                        Session["GlbReportType"] = "TSALE";
                        _invRepPara._GlbReportType = "TSALE";

                        _invRepPara._GlbReportName = "DeliveredSalesReport.rpt";
                        Session["GlbReportName"] = "DeliveredSalesReport.rpt";

                        _invRepPara._GlbReportHeading = "TOTAL SALES";
                        Session["InvReportPara"] = _invRepPara;
                        //    Response.Redirect("SalesReportViewer.aspx", false);

                        obj.DeliveredSalesReport(_invRepPara);
                        PrintPDF(targetFileName, obj._delSalesrptPC);
                    }
                    else if (rbRepExcel.Checked == true)
                    {
                        string rptType = "INVOICE";
                        if (rbInvWise.Checked == true)
                            rptType = "INVOICE";
                        else if (rbItemWise.Checked == true)
                            rptType = "ITEM";

                        // Currency Type 1 - Original Invoice Currency | 2 - Company Based Currency
                        int currencyType = 1;
                        if (rbInvoiceCurrency.Checked)
                            currencyType = 1; // Invoice Currency
                        else if (rbCompanyCurrency.Checked)
                            currencyType = 2; //Company Based Currency

                        _opt = "rad01";
                        _isSelected = true;
                        _export = "Y";

                        update_PC_List_RPTDB();

                        string _err = "";

                        string _filePath = CHNLSVC.MsgPortal.GetDeliveredSalesDetailsExcel1_old(Convert.ToDateTime(_invRepPara._GlbReportFromDate).Date,
                        Convert.ToDateTime(_invRepPara._GlbReportToDate).Date,
                        _invRepPara._GlbReportCustomer,
                        _invRepPara._GlbReportExecutive,
                        _invRepPara._GlbReportDocType,
                        _invRepPara._GlbReportItemCode,
                        _invRepPara._GlbReportBrand,
                        _invRepPara._GlbReportModel,
                        _invRepPara._GlbReportItemCat1,
                        _invRepPara._GlbReportItemCat2,
                        _invRepPara._GlbReportItemCat3,
                        _invRepPara._GlbUserID,
                        _invRepPara._GlbReportType,
                        _invRepPara._GlbReportItemStatus,
                        _invRepPara._GlbReportDoc,
                        _invRepPara._GlbReportCompCode,
                        _invRepPara._GlbReportPromotor,
                        _invRepPara._GlbReportIsFreeIssue,
                        rptType,
                        currencyType,
                        revOrSaleOrAll,
                        out _err);

                        if (!string.IsNullOrEmpty(_err))
                        {
                            lbtnView.Enabled = true;
                            displayMessage(_err);
                            return;
                        }
                        if (string.IsNullOrEmpty(_filePath))
                        {
                            displayMessage("The excel file path cannot identify. Please contact IT Dept");
                            return;
                        }
                        _copytoLocal(_filePath);
                        displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                    }

                }

                if (rad02.Checked == true)  // Delivered Sales - Sanjeewa 2016-04-28
                {
                    _opt = "rad02";
                    _isSelected = true;

                    int revOrSaleOrAll = 1;
                    //Sale
                    if (rbSale.Checked)
                    {
                        revOrSaleOrAll = 1;
                    }
                    else if (rbReversal.Checked)//Reversal
                    {
                        revOrSaleOrAll = 0;
                    }
                    else //Sale and Reversal 
                    {
                        revOrSaleOrAll = 2;
                    }
                    _invRepPara._GlbReportFromAge = revOrSaleOrAll;

                    if (chkExportExcel.Checked == false)
                    {
                        Session["GlbReportType"] = "DSALE";
                        _invRepPara._GlbReportType = "DSALE";

                        _invRepPara._GlbReportName = "DeliveredSalesReport.rpt";
                        Session["GlbReportName"] = "DeliveredSalesReport.rpt";

                        _invRepPara._GlbReportHeading = "DELIVERED SALES";
                        Session["InvReportPara"] = _invRepPara;
                        //   Response.Redirect("SalesReportViewer.aspx", false);

                        obj.DeliveredSalesReport(_invRepPara);
                        PrintPDF(targetFileName, obj._delSalesrptPC);
                    }
                    else
                    {
                        Session["GlbReportType"] = "DSALEEX";
                        _invRepPara._GlbReportType = "DSALEEX";

                        string rptType = "INVOICE";
                        if (rbInvWise.Checked == true)
                            rptType = "INVOICE";
                        else if (rbItemWise.Checked == true)
                            rptType = "ITEM";

                        // Currency Type 1 - Original Invoice Currency | 2 - Company Based Currency
                        int currencyType = 1;
                        if (rbInvoiceCurrency.Checked)
                            currencyType = 1; // Invoice Currency
                        else if (rbCompanyCurrency.Checked)
                            currencyType = 2; //Company Based Currency

                        _opt = "rad02";
                        _isSelected = true;
                        _export = "Y";

                        //update_PC_List_RPTDB();

                        string _err = "";

                        string _filePath = CHNLSVC.MsgPortal.GetDeliveredSalesDetailsExcel(Convert.ToDateTime(_invRepPara._GlbReportFromDate).Date,
                        Convert.ToDateTime(_invRepPara._GlbReportToDate).Date,
                        _invRepPara._GlbReportCustomer,
                        _invRepPara._GlbReportExecutive,
                        _invRepPara._GlbReportDocType,
                        _invRepPara._GlbReportItemCode,
                        _invRepPara._GlbReportBrand,
                        _invRepPara._GlbReportModel,
                        _invRepPara._GlbReportItemCat1,
                        _invRepPara._GlbReportItemCat2,
                        _invRepPara._GlbReportItemCat3,
                        "",
                        _invRepPara._GlbUserID,
                        _invRepPara._GlbReportType,
                        _invRepPara._GlbReportItemStatus,
                        _invRepPara._GlbReportDoc,
                        _invRepPara._GlbReportCompCode,
                        _invRepPara._GlbReportPromotor,
                        _invRepPara._GlbReportIsFreeIssue,
                        currencyType,
                        revOrSaleOrAll,
                        out _err);

                        if (!string.IsNullOrEmpty(_err))
                        {
                            lbtnView.Enabled = true;
                            displayMessage(_err);
                            return;
                        }
                        if (string.IsNullOrEmpty(_filePath))
                        {
                            displayMessage("The excel file path cannot identify. Please contact IT Dept");
                            return;
                        }
                        _copytoLocal(_filePath);
                        displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                    }

                }

                if (rad03.Checked == true)  // Debtors Sales & Settlements - Sanjeewa 2016-05-04
                {
                    _opt = "rad03";
                    _isSelected = true;

                    if (radOutOnly.Checked == true)//optOutsAll
                    {
                        _invRepPara._GlbReportOutstandingStatus = 0;
                        if (radProfitCenterWice.Checked == true)//optPC
                        {
                            _invRepPara._GlbReportName = "DebtorSettlement_PC.rpt";
                            Session["GlbReportName"] = _invRepPara._GlbReportName;
                        }
                        else
                        {
                            _invRepPara._GlbReportName = "DebtorSettlement1.rpt";
                            Session["GlbReportName"] = _invRepPara._GlbReportName;
                        }
                    }
                    else
                    {
                        _invRepPara._GlbReportOutstandingStatus = 1;
                        if (radProfitCenterWice.Checked == true)//optPC
                        {
                            _invRepPara._GlbReportName = "DebtorSettlement_Outs_PC.rpt";
                            Session["GlbReportName"] = _invRepPara._GlbReportName;
                        }
                        else
                        {
                            _invRepPara._GlbReportName = "DebtorSettlement_Outs.rpt";
                            Session["GlbReportName"] = _invRepPara._GlbReportName;
                        }
                    }
                    _invRepPara._GlbReportHeading = "DEBTORS SALES AND SETTLEMENTS";
                    Session["InvReportPara"] = _invRepPara;
                    //  Response.Redirect("SalesReportViewer.aspx", false);

                    obj.DebtorSettlementPrint(_invRepPara);
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement.rpt")
                    {
                        PrintPDF(targetFileName, obj._DebtSett);
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_PC.rpt")
                    {
                        PrintPDF(targetFileName, obj._DebtSettPC);
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_Outs_PC.rpt")
                    {
                        PrintPDF(targetFileName, obj._DebtSettOutPC);
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_Outs.rpt")
                    {
                        PrintPDF(targetFileName, obj._DebtSettOuts);
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_Outs_PC_Meeting.rpt")
                    {
                        PrintPDF(targetFileName, obj._DebtSettOutPCMeeting);
                    }
                    if (Session["GlbReportName"].ToString() == "DebtorSettlement_Outs_PC_with_comm.rpt")
                    {
                        PrintPDF(targetFileName, obj._DebtSettOutPCWithComm);
                    }

                }

                if (rad04.Checked == true)  // Debtors Sales & Settlements (Accounts Dept.) - Sanjeewa 2016-05-04
                {
                    _opt = "rad04";
                    _isSelected = true;

                    if (radOutOnly.Checked == true)//optOutsAll
                    {
                        if (chkWithCommition.Checked == false)//chkWithComm
                        {
                            displayMessage("Sorry.This report is allow to generate outstanding details only");
                            lbtnView.Enabled = true;
                            radProfitCenterWice.Checked = true;//optPC
                            return;
                        }

                        _invRepPara._GlbReportOutstandingStatus = 0;
                        if (radProfitCenterWice.Checked == true)//optPC
                        {
                            _invRepPara._GlbReportName = "DebtorSettlement_Outs_PC_with_comm.rpt";
                            Session["GlbReportName"] = _invRepPara._GlbReportName;
                        }
                        else
                        {
                            displayMessage("Sorry.This report is allow to generate profit center wise only");
                            lbtnView.Enabled = true;
                            radProfitCenterWice.Checked = true;//optPC
                            return;
                        }

                    }
                    else
                    {
                        _invRepPara._GlbReportOutstandingStatus = 1;
                        if (radProfitCenterWice.Checked == true)//optPC
                        {
                            if (chkWithCommition.Checked == false)//chkWithComm
                            {
                                _invRepPara._GlbReportName = "DebtorSettlement_Outs_PC_Meeting.rpt";
                                Session["GlbReportName"] = _invRepPara._GlbReportName;
                            }
                            else
                            {
                                _invRepPara._GlbReportName = "DebtorSettlement_Outs_PC_with_comm.rpt";
                                Session["GlbReportName"] = _invRepPara._GlbReportName;
                            }
                        }
                        else
                        {
                            displayMessage("Sorry.This report is allow to generate profit center wise only");
                            lbtnView.Enabled = true;
                            radProfitCenterWice.Checked = true;//optPC
                            return;
                        }
                    }
                    _invRepPara._GlbReportHeading = "DEBTORS SALES AND SETTLEMENTS (ACCOUNTS DEPT)";
                    Session["InvReportPara"] = _invRepPara;
                    //  Response.Redirect("SalesReportViewer.aspx", false);

                    obj.AgeAnalysisOfDebtorsOutstandingPrint(_invRepPara);
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding.rpt")
                    {
                        PrintPDF(targetFileName, obj._AgeDebtOuts);
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_PC.rpt")
                    {
                        PrintPDF(targetFileName, obj._AgeDebtOutsPC);
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_new.rpt")
                    {
                        PrintPDF(targetFileName, obj._AgeDebtOuts_new);
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_PC_new.rpt")
                    {
                        PrintPDF(targetFileName, obj._AgeDebtOutsPC_new);
                    }
                }

                if (rad05.Checked == true)  // Age Analysis of Debtors Outstanding - Sanjeewa 2016-05-04
                {
                    _opt = "rad05";
                    _isSelected = true;

                    lblMssg.Text = "Do you need to run the report with Credit Notes & Advance Receipts ?";
                    PopupConfBox.Show();
                    return;

                    //if (MessageBox.Show("Do you need to run the report register/unregister-wise?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //{
                    if (radProfitCenterWice.Checked == true)//optPC
                    {
                        _invRepPara._GlbReportName = "Age_Debtor_Outstanding_PC.rpt";
                        Session["GlbReportName"] = _invRepPara._GlbReportName;
                    }
                    else
                    {
                        _invRepPara._GlbReportName = "Age_Debtor_Outstanding.rpt";
                        Session["GlbReportName"] = _invRepPara._GlbReportName;
                    }
                   //}
                    //else
                    //{
                    //    if (radProfitCenterWice.Checked == true)//optPC
                    //    {
                    //        _invRepPara._GlbReportName = "Age_Debtor_Outstanding_PC_new.rpt";
                    //        Session["GlbReportName"] = _invRepPara._GlbReportName;
                    //    }
                    //    else
                    //    {
                    //        _invRepPara._GlbReportName = "Age_Debtor_Outstanding_new.rpt";
                    //        Session["GlbReportName"] = _invRepPara._GlbReportName;
                    //    }
                    //}
                    _invRepPara._GlbReportCustomer = chkAllCustomer.Checked ? "ALL" : txtCustomer.Text;
                    _invRepPara._GlbReportHeading = "Age Arrears of Debtors Outstanding";
                    Session["InvReportPara"] = _invRepPara;
                    // Response.Redirect("SalesReportViewer.aspx", false);

                    obj.AgeAnalysisOfDebtorsOutstandingPrint(_invRepPara);
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding.rpt")
                    {
                        PrintPDF(targetFileName, obj._AgeDebtOuts);
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_PC.rpt")
                    {
                        PrintPDF(targetFileName, obj._AgeDebtOutsPC);
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_new.rpt")
                    {
                        PrintPDF(targetFileName, obj._AgeDebtOuts_new);
                    }
                    if (Session["GlbReportName"].ToString() == "Age_Debtor_Outstanding_PC_new.rpt")
                    {
                        PrintPDF(targetFileName, obj._AgeDebtOutsPC_new);
                    }
                }
                if (rad06.Checked == true)  // Delivered Sales - Sanjeewa 2016-04-28
                {
                    _opt = "rad06";
                    _isSelected = true;


                    if (ucProfitCenterSearch.TxtCompany.Text == null | ucProfitCenterSearch.TxtCompany.Text.ToString() == "")
                    {
                        displayMessage("Please select Companay");
                    }
                    else
                    {

                        string locationsval = "";
                        foreach (GridViewRow Item in dgvLocation.Rows)
                        {
                            Label lblLocation = (Label)Item.FindControl("lblLocation");
                            CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                            // string pc = lblLocation.Text.ToUpper();

                            if (chkLocation.Checked == true)
                            {
                                locationsval = locationsval + lblLocation.Text + ",";
                            }
                        }


                        Session["LocationList"] = locationsval;
                        //  Session["Chanal"] = ucLoactionSearch.Channel;
                        //  Session["Location"] = ucLoactionSearch.ProfitCenter;
                        Session["GlbReportType"] = "";
                        Session["GlbReportName"] = "pcdetails.rpt";
                        _invRepPara._GlbReportName = "pcdetails.rpt";
                        _invRepPara._GlbReportHeading = "Profit Center Details";
                        Session["InvReportPara"] = _invRepPara;

                        string COM = Session["UserCompanyCode"] as string;
                        string pc = Session["UserDefProf"] as string;
                        string loc = Session["UserDefLoca"].ToString();
                        string pclist = Session["LocationList"] as string;
                        string user = Session["UserID"] as string;
                        obj.ProfitCenterDetails(COM, loc, pclist, user);
                        PrintPDF(targetFileName, obj._pcdetails);
                    };

                }

                if (rad07.Checked == true) //Intercompany Sales Report - Sanjeewa 2016-06-19
                {
                    _opt = "rad07";
                    _isSelected = true;
                    _export = "Y";
                    update_PC_List_RPTDB();

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.getIntercompanySalesDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, out _err);

                    if (!string.IsNullOrEmpty(_err))
                    {
                        lbtnView.Enabled = true;
                        displayMessage(_err);
                        return;
                    }
                    if (string.IsNullOrEmpty(_filePath))
                    {
                        displayMessage("The excel file path cannot identify. Please contact IT Dept");
                        return;
                    }
                    _copytoLocal(_filePath);
                    displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();
                }

                if (rad08.Checked == true) //Intercompany Reversal Report - Sanjeewa 2016-06-19
                {
                    _opt = "rad08";
                    _isSelected = true;
                    _export = "Y";

                    update_PC_List_RPTDB();

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.getIntercompanyReversalDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, out _err);

                    if (!string.IsNullOrEmpty(_err))
                    {
                        lbtnView.Enabled = true;
                        displayMessage(_err);
                        return;
                    }
                    if (string.IsNullOrEmpty(_filePath))
                    {
                        displayMessage("The excel file path cannot identify. Please contact IT Dept");
                        return;
                    }
                    _copytoLocal(_filePath);
                    displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                }

                if (rad09.Checked == true)  // Consignment Sales Details - Sanjeewa 2016-07-11
                {
                    _opt = "rad09";
                    _isSelected = true;

                    _invRepPara._GlbReportExport = 0;

                    _invRepPara._GlbReportCompCode = "";

                    if (_invRepPara._GlbReportExport == 1) { _export = "Y"; } else { _export = "N"; }

                    _invRepPara._GlbReportName = "Delivered_Sales_GRN.rpt";
                    Session["GlbReportName"] = "Delivered_Sales_GRN.rpt";

                    _invRepPara._GlbReportHeading = "CONSIGNMENT SALES";
                    Session["InvReportPara"] = _invRepPara;

                    obj.DeliveredSalesGRNReport(_invRepPara);
                    PrintPDF(targetFileName, obj._delSalesrptGRN);
                }
                if (rad10.Checked == true)  // Consignment Sales Details - Sanjeewa 2016-07-11
                {
                    _opt = "rad10";
                    _isSelected = true;

                    //_invRepPara._GlbReportExport = 0;

                    //_invRepPara._GlbReportCompCode = "";

                    //if (_invRepPara._GlbReportExport == 1) { _export = "Y"; } else { _export = "N"; }

                    _invRepPara._GlbReportName = "SalesReversalDetails.rpt";
                    Session["GlbReportName"] = "SalesReversalDetails.rpt";

                    _invRepPara._GlbReportHeading = "Sales Reversal Details";
                    Session["InvReportPara"] = _invRepPara;
                    string locationsval = "";
                    string CompanyCode = string.Empty;
                    string loginCompanyCode = Session["UserCompanyCode"] as string;
                    string CompanyDes = string.Empty;
                    string Admincode = string.Empty;
                    foreach (GridViewRow Item in dgvLocation.Rows)
                    {
                        Label lblLocation = (Label)Item.FindControl("lblLocation");
                        CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                        // string pc = lblLocation.Text.ToUpper();

                        if (chkLocation.Checked == true)
                        {
                            locationsval = locationsval + lblLocation.Text + ",";
                        }
                    }
                    foreach (GridViewRow Item in dgvCompany.Rows)
                    {
                        Label lblcode = (Label)Item.FindControl("lblCode");
                        Label lblDes = (Label)Item.FindControl("lblcodes");
                        CheckBox chkcom = (CheckBox)Item.FindControl("chkCompanyCode");
                        // string pc = lblLocation.Text.ToUpper();

                        if (chkcom.Checked == true)
                        {
                            CompanyCode = lblcode.Text;
                            CompanyDes = lblDes.Text;
                        }
                    }
                    foreach (GridViewRow Item in dgvAdminTeam.Rows)
                    {
                        Label lblcode = (Label)Item.FindControl("lblAdminCode");
                        CheckBox chkadmin = (CheckBox)Item.FindControl("chkAdminTeam");
                        // string pc = lblLocation.Text.ToUpper();

                        if (chkadmin.Checked == true)
                        {
                            Admincode = lblcode.Text;

                        }
                    }
                    obj.SRNDeatils(CompanyCode, _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, Admincode, locationsval, txtCustomer.Text, txtItemCode.Text, txtBrand.Text, txtModel.Text, CompanyDes,
                        Session["UserID"].ToString());
                    PrintPDF(targetFileName, obj._SalesReversalDetails);
                }

                if (rad11.Checked == true)  // Total Sales Month Wise - Lakshika 2016-10-12
                {
                    string rptType = "MONTH";

                    _opt = "rad11";
                    _isSelected = true;
                    _export = "Y";

                    update_PC_List_RPTDB();

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.GetDeliveredSalesDetailsExcel1_old(Convert.ToDateTime(_invRepPara._GlbReportFromDate).Date,
                    Convert.ToDateTime(_invRepPara._GlbReportToDate).Date,
                    _invRepPara._GlbReportCustomer,
                    _invRepPara._GlbReportExecutive,
                    _invRepPara._GlbReportDocType,
                    _invRepPara._GlbReportItemCode,
                    _invRepPara._GlbReportBrand,
                    _invRepPara._GlbReportModel,
                    _invRepPara._GlbReportItemCat1,
                    _invRepPara._GlbReportItemCat2,
                    _invRepPara._GlbReportItemCat3,
                    _invRepPara._GlbUserID,
                    _invRepPara._GlbReportType,
                    _invRepPara._GlbReportItemStatus,
                    _invRepPara._GlbReportDoc,
                    _invRepPara._GlbReportCompCode,
                    _invRepPara._GlbReportPromotor,
                    _invRepPara._GlbReportIsFreeIssue,
                    rptType,
                    2,//Company Currency
                    2,// All Sale Types (Sale and Reversal)
                    out _err);

                    if (!string.IsNullOrEmpty(_err))
                    {
                        lbtnView.Enabled = true;
                        displayMessage(_err);
                        return;
                    }
                    if (string.IsNullOrEmpty(_filePath))
                    {
                        displayMessage("The excel file path cannot identify. Please contact IT Dept");
                        return;
                    }
                    _copytoLocal(_filePath);
                    displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();


                }

                if (rad12.Checked == true)  // Discount Report 2016-10-14 Lakshika
                {

                    _opt = "rad12";
                    _isSelected = true;


                    if (ucProfitCenterSearch.TxtCompany.Text == null | ucProfitCenterSearch.TxtCompany.Text.ToString() == "")
                    {
                        displayMessage("Please select Companay");
                    }
                    else
                    {

                        //string locationsval = "";
                        //foreach (GridViewRow Item in dgvLocation.Rows)
                        //{
                        //    Label lblLocation = (Label)Item.FindControl("lblLocation");
                        //    CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                        //    // string pc = lblLocation.Text.ToUpper();

                        //    if (chkLocation.Checked == true)
                        //    {
                        //        locationsval = locationsval + lblLocation.Text + ",";
                        //    }
                        //}

                        update_PC_List_RPTDB();

                        //Session["LocationList"] = locationsval;
                        //  Session["Chanal"] = ucLoactionSearch.Channel;
                        //  Session["Location"] = ucLoactionSearch.ProfitCenter;
                        Session["GlbReportType"] = "";
                        Session["GlbReportName"] = "pcdetails.rpt";
                        _invRepPara._GlbReportName = "pcdetails.rpt";
                        _invRepPara._GlbReportHeading = "Discount Report";
                        Session["InvReportPara"] = _invRepPara;

                        string COM = Session["UserCompanyCode"] as string;
                        string pc = Session["UserDefProf"] as string;
                        string loc = Session["UserDefLoca"].ToString();
                        string pclist = Session["LocationList"] as string;
                        string user = Session["UserID"] as string;

                        int discountFrom = txtDiscountFrom.Text == "" ? 0 : Convert.ToInt32(txtDiscountFrom.Text);
                        int discountTo = txtDiscountTo.Text == "" ? 0 : Convert.ToInt32(txtDiscountTo.Text);

                        obj.GetDiscountReportDetails(_invRepPara._GlbReportCompCode,
                            "",
                            txtItemCode.Text,//_invRepPara._GlbReportItemCode,
                            txtCat1.Text,// _invRepPara._GlbReportItemCat1,
                            txtCat2.Text,//_invRepPara._GlbReportItemCat2,
                            txtCat3.Text,//_invRepPara._GlbReportItemCat3,
                            txtBrand.Text,//_invRepPara._GlbReportBrand,
                            txtModel.Text,// _invRepPara._GlbReportModel,
                            txtCustomer.Text,// _invRepPara._GlbReportCustomer,
                            txtExecutive.Text,// _invRepPara._GlbReportExecutive,
                            _invRepPara._GlbReportFromDate,
                            _invRepPara._GlbReportToDate,
                            discountFrom,
                            discountTo,
                            _invRepPara._GlbUserID,
                            _invRepPara._GlbReportCompName);



                        PrintPDF(targetFileName, obj._discountReport);
                    };



                    //string rptType = "MONTH";

                    //_opt = "rad12";
                    //_isSelected = true;
                    //_export = "Y";

                    //update_PC_List_RPTDB();

                    //string _err = "";

                    //string _filePath = CHNLSVC.MsgPortal.GetDeliveredSalesDetailsExcel(Convert.ToDateTime(_invRepPara._GlbReportFromDate).Date,
                    //Convert.ToDateTime(_invRepPara._GlbReportToDate).Date,
                    //_invRepPara._GlbReportCustomer,
                    //_invRepPara._GlbReportExecutive,
                    //_invRepPara._GlbReportDocType,
                    //_invRepPara._GlbReportItemCode,
                    //_invRepPara._GlbReportBrand,
                    //_invRepPara._GlbReportModel,
                    //_invRepPara._GlbReportItemCat1,
                    //_invRepPara._GlbReportItemCat2,
                    //_invRepPara._GlbReportItemCat3,
                    //_invRepPara._GlbUserID,
                    //_invRepPara._GlbReportType,
                    //_invRepPara._GlbReportItemStatus,
                    //_invRepPara._GlbReportDoc,
                    //_invRepPara._GlbReportCompCode,
                    //_invRepPara._GlbReportPromotor,
                    //_invRepPara._GlbReportIsFreeIssue,
                    //rptType,
                    //2,//Company Currency
                    //out _err);

                    //if (!string.IsNullOrEmpty(_err))
                    //{
                    //    lbtnView.Enabled = true;
                    //    displayMessage(_err);
                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(_filePath))
                    //{
                    //    displayMessage("The excel file path cannot identify. Please contact IT Dept");
                    //    return;
                    //}
                    //_copytoLocal(_filePath);
                    //displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                    //Process p = new Process();
                    //p.StartInfo = new ProcessStartInfo(_filePath);
                    //p.Start();


                }

                if (rad13.Checked == true)  // Excetutive Wise Sales - Wimal 27/10/2016
                {
                    _opt = "rad13";
                    _isSelected = true;

                    Session["GlbReportType"] = "ASALE";
                    _invRepPara._GlbReportType = "ASALE";

                    _invRepPara._GlbReportName = "excecutivewisesales.rpt";
                    Session["GlbReportName"] = "excecutivewisesales.rpt";

                    _invRepPara._GlbReportHeading = "EXECUTIVE WISE SALES";
                    Session["InvReportPara"] = _invRepPara;
                    //   Response.Redirect("SalesReportViewer.aspx", false);

                    obj.executiveWiseSalesReport(_invRepPara);
                    PrintPDF(targetFileName, obj._exectSales);
                }
                if (rad14.Checked == true)  // Customer Entery Report - Rukshan 23/11/2016
                {
                    _opt = "rad14";
                    _isSelected = true;

                    _invRepPara._GlbReportName = "CustomerEntryReport.rpt";
                    Session["GlbReportName"] = "CustomerEntryReport.rpt";

                    _invRepPara._GlbReportHeading = "Customer Entry Report";
                    Session["InvReportPara"] = _invRepPara;
                    string locationsval = "";
                    string CompanyCode = string.Empty;
                    string loginCompanyCode = Session["UserCompanyCode"] as string;
                    string CompanyDes = string.Empty;
                    string Admincode = string.Empty;
                    string user = Session["UserID"] as string;
                    foreach (GridViewRow Item in dgvLocation.Rows)
                    {
                        Label lblLocation = (Label)Item.FindControl("lblLocation");
                        CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                        // string pc = lblLocation.Text.ToUpper();

                        if (chkLocation.Checked == true)
                        {
                            locationsval = locationsval + lblLocation.Text + ",";
                        }
                    }
                    foreach (GridViewRow Item in dgvCompany.Rows)
                    {
                        Label lblcode = (Label)Item.FindControl("lblCode");
                        Label lblDes = (Label)Item.FindControl("lblcodes");
                        CheckBox chkcom = (CheckBox)Item.FindControl("chkCompanyCode");
                        // string pc = lblLocation.Text.ToUpper();

                        if (chkcom.Checked == true)
                        {
                            CompanyCode = lblcode.Text;
                            CompanyDes = lblDes.Text;
                        }
                    }

                    obj.CustomerEntry(CompanyCode, _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, CompanyDes, user);
                    PrintPDF(targetFileName, obj._CustomerEntryReport);
                }

                if (rad15.Checked == true)
                {
                    //Wimal - 16/12/2016

                    _opt = "rad15";
                    _isSelected = true;

                    _invRepPara._GlbReportName = "SelloutDetails.rpt";
                    Session["GlbReportName"] = "SelloutDetails.rpt";

                    _invRepPara._GlbReportHeading = "SellOut Report";
                    Session["InvReportPara"] = _invRepPara;
                    string locationsval = "";
                    string CompanyCode = string.Empty;
                    string loginCompanyCode = Session["UserCompanyCode"] as string;
                    string CompanyDes = string.Empty;
                    string Admincode = string.Empty;
                    string user = Session["UserID"] as string;
                    /* foreach (GridViewRow Item in dgvLocation.Rows)
                     {
                         Label lblLocation = (Label)Item.FindControl("lblLocation");
                         CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                         // string pc = lblLocation.Text.ToUpper();

                         if (chkLocation.Checked == true)
                         {
                             locationsval = locationsval + lblLocation.Text + ",";
                         }
                     }
                     foreach (GridViewRow Item in dgvCompany.Rows)
                     {
                         Label lblcode = (Label)Item.FindControl("lblCode");
                         Label lblDes = (Label)Item.FindControl("lblcodes");
                         CheckBox chkcom = (CheckBox)Item.FindControl("chkCompanyCode");
                         // string pc = lblLocation.Text.ToUpper();

                         if (chkcom.Checked == true)
                         {
                             CompanyCode = lblcode.Text;
                             CompanyDes = lblDes.Text;
                         }
                     }*/

                    obj.Get_Sellout_Report(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, user, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportPromotor, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive);
                    PrintPDF(targetFileName, obj._Sellout);
                }
                if (rad16.Checked == true)
                {
                    _isSelected = true;
                    //ISURU 2017/03/06
                    update_PC_List_RPTDB();
                    string customer = txtCustomer.Text.ToString();
                    string saletype = txtDocType.Text.ToString();
                    string invoiceno = txtDocNo.Text.ToString();
                    DateTime fromdate = Convert.ToDateTime(txtFromDate.Text.ToString());
                    DateTime todate = Convert.ToDateTime(txtToDate.Text.ToString());
                    string company = Session["UserCompanyCode"] as string;
                    string userid = Session["UserID"] as string;

                    obj.CollectionDetailRep(company, fromdate, todate, saletype, customer, invoiceno, userid);
                    PrintPDF(targetFileName, obj._Collectiondetail);
                }
                //Cheque Return
                if (rad17.Checked == true)
                {
                    _isSelected = true;
                    update_PC_List_RPTDB();
                    string customer = txtCustomer.Text.ToString();
                    DateTime fromdate = Convert.ToDateTime(txtFromDate.Text.ToString());
                    DateTime todate = Convert.ToDateTime(txtToDate.Text.ToString());
                    string company = Session["UserCompanyCode"] as string;
                    string userid = Session["UserID"] as string;
                    string cheque = txtcheque.Text.ToString();
                    string accountcode = txtaccountcode.Text.ToString();

                    string grntxt = "";
                    foreach (GridViewRow row in dgvLocation.Rows)
                    {
                        CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");
                        if (chkLocation != null && chkLocation.Checked)
                        {
                            if (grntxt != "")
                            {
                                grntxt = grntxt + ",";
                            }
                            Label lblLocation = (Label)row.FindControl("lblLocation");
                            string com_cd = lblLocation.Text;
                            grntxt = grntxt + com_cd;
                        }
                    }

                    obj.returnchequedet(company, fromdate, todate, customer, userid, cheque, accountcode, grntxt, ViewState["Name"] == null ? string.Empty : ViewState["Name"].ToString(), ViewState["Bank"] == null ? string.Empty : ViewState["Bank"].ToString());
                    PrintPDF(targetFileName, obj._chequedetail);
                    ViewState["Name"] = null;
                    ViewState["Bank"] = null;
                }

                if (rad18.Checked == true)
                {
                    int revOrSaleOrAll = 1;
                    //Sale
                    if (rbSale.Checked)
                    {
                        revOrSaleOrAll = 1;
                    }
                    else if (rbReversal.Checked)//Reversal
                    {
                        revOrSaleOrAll = 0;
                    }
                    else //Sale and Reversal 
                    {
                        revOrSaleOrAll = 2;
                    }
                    _invRepPara._GlbReportFromAge = revOrSaleOrAll;

                    string rptType = "INVOICE";
                    if (rbInvWise.Checked == true)
                        rptType = "INVOICE";
                    else if (rbItemWise.Checked == true)
                        rptType = "ITEM";

                    // Currency Type 1 - Original Invoice Currency | 2 - Company Based Currency
                    int currencyType = 1;
                    if (rbInvoiceCurrency.Checked)
                        currencyType = 1; // Invoice Currency
                    else if (rbCompanyCurrency.Checked)
                        currencyType = 2; //Company Based Currency

                    _opt = "rad18";
                    _isSelected = true;
                    _export = "Y";

                    update_PC_List_RPTDB();

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.GetTotalSaleswithInvType(Convert.ToDateTime(_invRepPara._GlbReportFromDate).Date,
                    Convert.ToDateTime(_invRepPara._GlbReportToDate).Date,
                    _invRepPara._GlbReportCustomer,
                    _invRepPara._GlbReportExecutive,
                    _invRepPara._GlbReportDocType,
                    _invRepPara._GlbReportItemCode,
                    _invRepPara._GlbReportBrand,
                    _invRepPara._GlbReportModel,
                    _invRepPara._GlbReportItemCat1,
                    _invRepPara._GlbReportItemCat2,
                    _invRepPara._GlbReportItemCat3,
                    "",
                    _invRepPara._GlbUserID,
                    _invRepPara._GlbReportType,
                    _invRepPara._GlbReportItemStatus,
                    _invRepPara._GlbReportDoc,
                    _invRepPara._GlbReportCompCode,
                    _invRepPara._GlbReportPromotor,
                    _invRepPara._GlbReportIsFreeIssue,
                    currencyType,
                    revOrSaleOrAll, "", "",
                    out _err);

                    if (!string.IsNullOrEmpty(_err))
                    {
                        lbtnView.Enabled = true;
                        displayMessage(_err);
                        return;
                    }
                    if (string.IsNullOrEmpty(_filePath))
                    {
                        displayMessage("The excel file path cannot identify. Please contact IT Dept");
                        return;
                    }
                    _copytoLocal(_filePath);
                    displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                }

                //Added By Udaya BOQ Details Report 23.09.2017
                if (rad19.Checked == true)
                {
                    _opt = "rad08";
                    _isSelected = true;
                    update_PC_List_RPTDB();
                    string customer = txtCustomer.Text.ToString();
                    _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    _invRepPara._GlbReportName = "BOQ_Details.rpt";
                    Session["UserCompanyCode"] = _invRepPara._GlbReportCompCode;
                    _invRepPara._GlbReportCustomer = customer;
                    string _boqNo = string.Empty;
                    string _subCordinator = string.Empty;

                    _invRepPara._GlbReportHeading = "BOQ_Details";
                    Session["InvReportPara"] = _invRepPara;

                    obj.BOQDetails(_invRepPara, _boqNo, _subCordinator);
                    PrintPDF(targetFileName, obj._boqDetails);
                }

                if (_isSelected == false)
                {
                    displayMessage("Please select the report!");

                    return;
                }

                if (_export == "N")
                {
                    //string url = "<script>window.open('/View/Reports/Sales/SalesReportViewer.aspx','_blank');</script>";
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
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "Sal_Rep", "Run Ok", Session["UserID"].ToString());
            }
            catch (Exception err)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "Sal_Rep", err.Message, Session["UserID"].ToString());
                CHNLSVC.CloseChannel();
                Response.Redirect("~/Error.aspx?Error=" + err.Message + "");
                lbtnView.Enabled = true;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)_rpt;
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
        }

        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);
            if (file.Exists)
            {
                System.IO.File.Copy(@"" + _filePath, "C:/Download_excel/" + filenamenew + ".xlsx", true);
            }
            else
            {
                DisplayMessage("This file does not exist.", 1);
            }
        }

        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
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

        private void update_PC_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucProfitCenterSearch.Company, null, null);

            foreach (GridViewRow Item in dgvLocation.Rows)
            {
                Label lblLocation = (Label)Item.FindControl("lblLocation");
                CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                string pc = lblLocation.Text;

                if (chkLocation.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucProfitCenterSearch.Company, pc, null);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = pc;
                    }
                    else
                    {
                        //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
                        BaseCls.GlbReportProfit = "All Locations Based on User Rights";
                    }
                }
            }

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = ucProfitCenterSearch.ProfitCenter.ToUpper();
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucProfitCenterSearch.Company, ucProfitCenterSearch.ProfitCenter.ToUpper(), null);
            }
        }
        private void LoadEmptyGrid()
        {
            dgvCompany.DataSource = null;
            dgvCompany.DataBind();
            dgvAdminTeam.DataSource = null;
            dgvAdminTeam.DataBind();
        }

        private void set_GroupOrder()
        {
            int i = 1;
            int j = listCat5.Items.Count;
            BaseCls.GlbReportGroupProfit = 0;
            BaseCls.GlbReportGroupDOLoc = 0;
            BaseCls.GlbReportGroupDocType = 0;
            BaseCls.GlbReportGroupCustomerCode = 0;
            BaseCls.GlbReportGroupExecCode = 0;
            BaseCls.GlbReportGroupItemCode = 0;
            BaseCls.GlbReportGroupBrand = 0;
            BaseCls.GlbReportGroupModel = 0;
            BaseCls.GlbReportGroupItemCat1 = 0;
            BaseCls.GlbReportGroupItemCat2 = 0;
            BaseCls.GlbReportGroupItemCat3 = 0;
            BaseCls.GlbReportGroupItemCat4 = 0;
            BaseCls.GlbReportGroupItemCat5 = 0;
            BaseCls.GlbReportGroupLastGroup = 0;
            BaseCls.GlbReportGroupInvoiceNo = 0;
            BaseCls.GlbReportGroupItemStatus = 0;
            BaseCls.GlbReportGroupPromotor = 0;
            //  BaseCls.GlbReportGroupJobNo = 0;
            BaseCls.GlbReportGroupLastGroupCat = "";
            //BaseCls.GlbReportGroupCheque = 0;
            //BaseCls.GlbReportGroupaccountcode = 0;

            foreach (ListItem Item in listCat5.Items)
            {
                if (Item.Text == "LOC")
                {
                    BaseCls.GlbReportGroupProfit = i;
                }
                if (Item.Text == "DLOC")
                {
                    BaseCls.GlbReportGroupDOLoc = i;
                }
                if (Item.Text == "DTP")
                {
                    BaseCls.GlbReportGroupDocType = i;
                }
                if (Item.Text == "CUST")
                {
                    BaseCls.GlbReportGroupCustomerCode = i;
                }
                if (Item.Text == "EXEC")
                {
                    BaseCls.GlbReportGroupExecCode = i;
                }
                if (Item.Text == "ITM")
                {
                    BaseCls.GlbReportGroupItemCode = i;
                }
                if (Item.Text == "BRND")
                {
                    BaseCls.GlbReportGroupBrand = i;
                }
                if (Item.Text == "MDL")
                {
                    BaseCls.GlbReportGroupModel = i;
                }
                if (Item.Text == "CAT1")
                {
                    BaseCls.GlbReportGroupItemCat1 = i;
                }
                if (Item.Text == "CAT2")
                {
                    BaseCls.GlbReportGroupItemCat2 = i;
                }
                if (Item.Text == "CAT3")
                {
                    BaseCls.GlbReportGroupItemCat3 = i;
                }
                if (Item.Text == "CAT4")
                {
                    BaseCls.GlbReportGroupItemCat4 = i;
                }
                if (Item.Text == "CAT5")
                {
                    BaseCls.GlbReportGroupItemCat5 = i;
                }
                if (Item.Text == "INV")
                {
                    BaseCls.GlbReportGroupInvoiceNo = i;
                }
                if (Item.Text == "STK")
                {
                    BaseCls.GlbReportGroupItemStatus = i;
                }
                if (Item.Text == "PRM")
                {
                    BaseCls.GlbReportGroupPromotor = i;
                }
                //if (Item.Text == "CHQ")
                //{
                //    BaseCls.GlbReportGroupCheque = i;
                //}
                //if (Item.Text == "ACC")
                //{
                //    BaseCls.GlbReportGroupaccountcode = i;
                //}
                BaseCls.GlbReportGroupLastGroup = j;
                if (j == i)
                {
                    BaseCls.GlbReportGroupLastGroupCat = Item.Text;
                }
                i++;
            }
            if (j == 0)
            {
                BaseCls.GlbReportGroupDOLoc = 1;
                BaseCls.GlbReportGroupItemCode = 2;
                BaseCls.GlbReportGroupLastGroup = 2;
                BaseCls.GlbReportGroupLastGroupCat = "ITM";
            }
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
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
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
                                string com_cd = row.Cells[1].Text;
                                com_cds = com_cds + com_cd;
                            }
                        }
                        paramsText.Append(com_cds);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(seperator);
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Promotor:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append(txtDocType.Text.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InventoryDirection:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BrandManager:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"] + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Chequedet:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.accountcode:
                    {
                        paramsText.Append(seperator);
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
            Label compDes = (Label)Row.FindControl("lblcodes");

            CheckBox chkSelect = (CheckBox)Row.FindControl("chkCompanyCode");
            bool select = chkSelect.Checked;
            string cellvalue = compCode.Text;
            string cellvalueDes = compDes.Text;
            Session["DATACOMPANY"] = dtCompany;
            BindAdminTeam();
            if (chkAllCompany.Checked)
            {
                ucProfitCenterSearch.ClearAllTextBoxs();
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
                    ucProfitCenterSearch.Company = cellvalue;
                    ucProfitCenterSearch.CompanyDes = cellvalueDes;
                }
                else
                {
                    ucProfitCenterSearch.ClearAllTextBoxs();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void dgvAdminTeam_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvAdminTeam.PageIndex = e.NewPageIndex;
                DataTable dt = (DataTable)Session["DATAADMIN"];
                if (dt.Rows.Count > 0)
                {
                    dgvAdminTeam.DataSource = dt;
                    dgvAdminTeam.DataBind();
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
        public void BindYear()
        {
            ddlYear.DataSource = null;
            int sysYear = DateTime.Today.Year;
            int yearCount = DateTime.Today.Year - 2011;
            int[] yearList = new int[yearCount];
            for (int x = 0; x < yearCount; x++)
            {
                yearList[x] = sysYear;
                sysYear--;
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
                Session["MovementTypes"] = null;
                Session["DocSubType"] = null;
                Session["InventoryDirection"] = null;
                Session["ItemStatus"] = null;
                Session["BrandManager"] = null;
                Session["Supplier"] = null;
                Session["Customer"] = null;
                Session["InvoiceExecutive"] = null;
                Session["Promotor"] = null;
                Session["Sales_Type"] = null;
                Session["Cheque"] = null;
                Session["accountcode"] = null;

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
                if (lblSearchType.Text == "MovementTypes")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                    dt = CHNLSVC.CommonSearch.GetMovementTypes(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["MovementTypes"] = dt;
                }
                if (lblSearchType.Text == "DocSubType")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                    dt = CHNLSVC.CommonSearch.GetMovementDocSubTypes(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["DocSubType"] = dt;
                }
                if (lblSearchType.Text == "InventoryDirection")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryDirection);
                    dt = CHNLSVC.CommonSearch.GetInventoryDirections(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["InventoryDirection"] = dt;
                }
                if (lblSearchType.Text == "ItemStatus")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                    dt = CHNLSVC.CommonSearch.GetCompanyItemStatusData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ItemStatus"] = dt;
                }
                if (lblSearchType.Text == "BrandManager")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BrandManager);
                    dt = CHNLSVC.CommonSearch.GetBrandManager(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BrandManager"] = dt;
                }
                if (lblSearchType.Text == "Supplier")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    dt = CHNLSVC.CommonSearch.GetSupplierData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Supplier"] = dt;
                }
                if (lblSearchType.Text == "Customer")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    dt = CHNLSVC.CommonSearch.GetCustomerGenaral(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    dt.Columns.Remove("Code1");
                    Session["Customer"] = dt;
                }
                if (lblSearchType.Text == "InvoiceExecutive")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                    dt = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["InvoiceExecutive"] = dt;
                }
                if (lblSearchType.Text == "Promotor")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotor);
                    dt = CHNLSVC.CommonSearch.SearchSalesPromotor(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Promotor"] = dt;
                }
                if (lblSearchType.Text == "Sales_Type")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                    dt = CHNLSVC.General.GetSalesTypes(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Sales_Type"] = dt;
                }
                if (lblSearchType.Text == "Chequedet")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Cheque);
                    dt = CHNLSVC.CommonSearch.SearchChequeCodes(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Chequedet"] = dt;
                }
                if (lblSearchType.Text == "accountcode")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.accountcode);
                    dt = CHNLSVC.CommonSearch.Searchaccountcode(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["accountcode"] = dt;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                if (lblSearchType.Text == "CAT_Sub1")
                {
                    _result = (DataTable)Session["CAT_Sub1"];
                }
                if (lblSearchType.Text == "CAT_Sub2")
                {
                    _result = (DataTable)Session["CAT_Sub2"];
                }
                if (lblSearchType.Text == "CAT_Sub3")
                {
                    _result = (DataTable)Session["CAT_Sub3"];
                }
                if (lblSearchType.Text == "CAT_Sub4")
                {
                    _result = (DataTable)Session["CAT_Sub4"];
                }
                if (lblSearchType.Text == "Item")
                {
                    _result = (DataTable)Session["Item"];
                }
                if (lblSearchType.Text == "Brand")
                {
                    _result = (DataTable)Session["Brand"];
                }
                if (lblSearchType.Text == "Model")
                {
                    _result = (DataTable)Session["Model"];
                }
                if (lblSearchType.Text == "MovementTypes")
                {
                    _result = (DataTable)Session["MovementTypes"];
                }
                if (lblSearchType.Text == "DocSubType")
                {
                    _result = (DataTable)Session["DocSubType"];
                }
                if (lblSearchType.Text == "InventoryDirection")
                {
                    _result = (DataTable)Session["InventoryDirection"];
                }
                if (lblSearchType.Text == "ItemStatus")
                {
                    _result = (DataTable)Session["ItemStatus"];
                }
                if (lblSearchType.Text == "BrandManager")
                {
                    _result = (DataTable)Session["BrandManager"];
                }
                if (lblSearchType.Text == "Supplier")
                {
                    _result = (DataTable)Session["Supplier"];
                }
                if (lblSearchType.Text == "InvoiceExecutive")
                {
                    _result = (DataTable)Session["InvoiceExecutive"];
                }
                if (lblSearchType.Text == "Promotor")
                {
                    _result = (DataTable)Session["Promotor"];
                }
                if (lblSearchType.Text == "Customer")
                {
                    _result = (DataTable)Session["Customer"];
                }
                if (lblSearchType.Text == "Sales_Type")
                {
                    _result = (DataTable)Session["Sales_Type"];
                }
                if (lblSearchType.Text == "Chequedet")
                {
                    _result = (DataTable)Session["Chequedet"];
                }
                if (lblSearchType.Text == "accountcode")
                {
                    _result = (DataTable)Session["accountcode"];
                }

                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;

                }
                else
                {
                    dgvResultItem.DataSource = null;
                } dgvResultItem.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                ItemPopup.Show();

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


        protected void dgvResultItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string description = string.Empty;
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript",
                     "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvResultItem.SelectedRow.Cells[1].Text;
                if (lblSearchType.Text == "CAT_Main")
                {
                    txtCat1.Text = code;
                    chkAllCat1.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    txtCat2.Text = code;
                    chkAllCat2.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub2")
                {
                    txtCat3.Text = code;
                    chkAllCat3.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub3")
                {
                    txtCat4.Text = code;
                    chkAllCat4.Checked = false;
                }
                else if (lblSearchType.Text == "CAT_Sub4")
                {
                    txtCat5.Text = code;
                    chkAllCat5.Checked = false;
                }
                else if (lblSearchType.Text == "Item")
                {
                    txtItemCode.Text = code;
                    chkAllItemCode.Checked = false;
                }
                else if (lblSearchType.Text == "Brand")
                {
                    txtBrand.Text = code;
                    chkAllBrand.Checked = false;
                }
                else if (lblSearchType.Text == "Model")
                {
                    txtModel.Text = code;
                    chkAllModel.Checked = false;
                }
                if (lblSearchType.Text == "MovementTypes")
                {
                    txtDocType.Text = code;
                    chkAllDocType.Checked = false;
                }
                if (lblSearchType.Text == "DocSubType")
                {
                    txtDocSubType.Text = code;
                    chkAllDocSubType.Checked = false;
                }
                if (lblSearchType.Text == "InventoryDirection")
                {
                    txtDirection.Text = code;
                    chkAllDirNo.Checked = false;
                }
                if (lblSearchType.Text == "ItemStatus")
                {
                    txtItemStat.Text = dgvResultItem.SelectedRow.Cells[2].Text; ;
                    chkAllStat.Checked = false;
                }
                if (lblSearchType.Text == "BrandManager")
                {
                    txtBrandMan.Text = code;
                    chkAllBrandMan.Checked = false;
                }
                if (lblSearchType.Text == "Supplier")
                {
                    txtSupplier.Text = code;
                    chkAllSupplier.Checked = false;
                }
                if (lblSearchType.Text == "Promotor")
                {
                    txtPromotor.Text = code;
                    chkAllPromotor.Checked = false;
                }
                if (lblSearchType.Text == "InvoiceExecutive")
                {
                    txtExecutive.Text = code;
                    chkAllExecutive.Checked = false;
                }
                if (lblSearchType.Text == "Customer")
                {
                    txtCustomer.Text = code;
                    description = dgvResultItem.SelectedRow.Cells[2].Text;
                    ViewState["Name"] = description.ToString();
                    chkAllCustomer.Checked = false;
                }
                if (lblSearchType.Text == "InvoiceNo")
                {
                    txtDocNo.Text = code;
                    chkAllCustomer.Checked = false;
                }
                if (lblSearchType.Text == "Chequedet")
                {
                    txtcheque.Text = code;
                    CheckALLCheque.Checked = false;
                }
                if (lblSearchType.Text == "accountcode")
                {
                    txtaccountcode.Text = code;
                    description = dgvResultItem.SelectedRow.Cells[2].Text;
                    ViewState["Bank"] = description;
                    CheckAllAccountCode.Checked = false;
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
        #region start
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeItemCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item";
                Session["Item"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
            //if (chkAllOrPlNo.Checked)
            //{
            //    txtOrderPlanNo.Text = "";
            //}
        }

        protected void chkAllBankNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllBankNo.Checked)
            //{
            //    txtBankNo.Text = "";
            //}
        }

        protected void chkAllPLNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllPLNo.Checked)
            //{
            //    txtPlNo.Text = "";
            //}
        }

        protected void chkAllLcNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllLcNo.Checked)
            //{
            //    txtLcNo.Text = "";
            //}
        }

        protected void chkAllBlNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllBlNo.Checked)
            //{
            //    txtBlNo.Text = "";
            //}
        }

        protected void chkAllGrnNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllGrnNo.Checked)
            //{
            //    txtGrnNo.Text = "";
            //}
        }

        protected void chkAllEntryNo_CheckedChanged(object sender, EventArgs e)
        {
            //    if (chkAllEntryNo.Checked)
            //    {
            //        txtEntryNo.Text = "";
            //    }
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
                        txtCat1.Text = "";
                        txtCat1.Focus();
                        displayMessage("Please select a valid item for category 1");
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
                displayMessage("Please select category 1");
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
                        txtCat2.Text = "";
                        txtCat2.Focus(); displayMessage("Please select a valid item for category 2");
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
                        txtCat3.Text = "";
                        txtCat3.Focus(); displayMessage("Please select a valid item for category 3");

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
                        txtCat4.Text = "";
                        txtCat4.Focus(); displayMessage("Please select a valid item for category 4");

                    }
                }
                else
                {
                    txtCat4.Text = "";
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
                        txtCat5.Text = "";
                        txtCat5.Focus(); displayMessage("Please select a valid item for category 5");

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
                        txtItemCode.Text = "";
                        txtItemCode.Focus();
                        displayMessage("Please select a valid item code");
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
                        txtBrand.Text = "";
                        txtBrand.Focus(); displayMessage("Please select a valid brand");

                    }
                }
                else
                {
                    txtBrand.Focus(); displayMessage(txtBrand.Text + " is already added");

                }
            }
            else
            {
                txtBrand.Focus(); displayMessage("Please select item brand");

            }
        }

        protected void lbtnAddModel_Click(object sender, EventArgs e)
        {
            if (txtModel.Text != "")
            {
                bool b1 = false;
                bool b2 = false;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, "Code", txtModel.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (txtModel.Text.ToUpper() == row["Code"].ToString())
                        {
                            b2 = true;
                            break;
                        }
                    }
                }

                foreach (ListItem list in listModel.Items)
                {
                    if (list.Text == txtModel.Text)
                    {
                        b1 = true;
                        break;
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
                        txtModel.Text = "";
                        txtModel.Focus(); displayMessage("Please select a valid model");

                    }
                }
                else
                {
                    txtModel.Focus(); displayMessage(txtModel.Text + " is already added");

                }
            }
            else
            {
                txtModel.Focus(); displayMessage("Please select item model");

            }
        }
        #endregion
        protected void lbtnSeDocType_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _result = new DataTable();
                lblSearchType.Text = "MovementTypes";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                if (rad16.Checked == true)
                {
                    _result = CHNLSVC.CommonSearch.Get_Rcpt_Types(para, null, null);
                }
                else
                {
                    _result = CHNLSVC.CommonSearch.GetInvoiceTypes(para, null, null);
                }

                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["MovementTypes"] = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeDocSubType_Click(object sender, EventArgs e)
        {

            try
            {
                lblSearchType.Text = "Sales_Type";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable _result = CHNLSVC.General.GetSalesTypes(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["DocSubType"] = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeDocNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "InvoiceNo";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceNo(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["InvoiceNo"] = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeDerection_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "InventoryDirection";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryDirection);
                DataTable _result = CHNLSVC.CommonSearch.GetInventoryDirections(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeEntry_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "InventoryDirection";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryDirection);
                DataTable _result = CHNLSVC.CommonSearch.GetInventoryDirections(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeItemSta_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ItemStatus";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnSeBrandMan_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "BrandManager";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BrandManager);
                DataTable _result = CHNLSVC.CommonSearch.GetBrandManager(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void chkAllStat_CheckedChanged(object sender, EventArgs e)
        {
            //txtItemStat.Enabled = !chkAllStat.Checked;
            txtItemStat.Text = "";
            if (chkAllStat.Checked)
            {
                txtItemStat.Enabled = false;
            }
            else
            {
                txtItemStat.Enabled = true;
            }
        }
        protected void chkAllBrandMan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBrandMan.Checked)
            {
                txtBrandMan.Text = "";
                txtBrandMan.Enabled = false;
            }
            else
            {
                txtBrandMan.Enabled = true;
            }
        }
        protected void chkAllSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSupplier.Checked)
            {
                txtSupplier.Text = "";
                txtSupplier.Enabled = false;
            }
            else
            {
                txtSupplier.Enabled = true;
            }
        }
        protected void chkAllDocType_CheckedChanged(object sender, EventArgs e)
        {
            txtDocType.Text = "";
            if (chkAllDocType.Checked)
            {
                txtDocType.Enabled = false;
            }
            else
            {
                txtDocType.Enabled = true;
            }

        }

        protected void chkAllDocSubType_CheckedChanged(object sender, EventArgs e)
        {
            txtDocSubType.Text = "";
            if (chkAllDocSubType.Checked)
            {
                txtDocSubType.Enabled = false;
            }
            else
            {
                txtDocSubType.Enabled = true;
            }
        }

        protected void chkAllDocNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDocNo.Text = "";
        }

        protected void chkAllDirNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDirection.Text = "";
            if (chkAllDirNo.Checked)
            {
                txtDirection.Enabled = false;
            }
            else
            {
                txtDirection.Enabled = true;
            }
        }

        protected void chkAllEntType_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void chkAllRecType_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void chkAllBatchNo_CheckedChanged(object sender, EventArgs e)
        {
            txtBatchNo.Text = "";
        }

        protected void dgvAdminTeam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                if (chkAllCompany.Checked)
                {
                    e.Row.Cells[1].Visible = true;
                }
                else
                {
                    e.Row.Cells[1].Visible = false;
                }
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
                    foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
                    {
                        CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");

                        if (chkCompanyCode.Checked == true)
                        {
                            chkCompanyCode.Checked = false;
                        }
                    }
                    BindAdminTeam();
                    ucProfitCenterSearch.Company = Session["UserCompanyCode"].ToString();
                    locationPanel.Enabled = true;
                }
                else
                {
                    dgvAdminTeam.Columns[1].Visible = true;
                    foreach (GridViewRow hiderowbtn in this.dgvCompany.Rows)
                    {
                        CheckBox chkCompanyCode = (CheckBox)hiderowbtn.FindControl("chkCompanyCode");

                        if (chkCompanyCode.Checked == true)
                        {
                            chkCompanyCode.Checked = false;
                        }
                    }
                    BindAdminTeam();
                    ucProfitCenterSearch.ClearAllTextBoxs();
                    locationPanel.Enabled = true;
                }
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
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
                        //ucProfitCenterSearch.Company = Session["UserCompanyCode"].ToString();
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
                        //ucProfitCenterSearch.ClearText();
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

        protected void lbtnAddLocation_Click(object sender, EventArgs e)
        {
            try
            {
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
                if (chkAllCompany.Checked)
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10044))
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
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                        DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                        //DataTable _result = CHNLSVC.General.GetLocationByComs(com_cds);
                        dgvLocation.DataSource = _result;
                        dgvLocation.DataBind();
                        //foreach (DataRow drow in dt.Rows)
                        //{
                        //    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                        //}
                    }
                }
                else
                {
                    if (ucProfitCenterSearch.Company == "")
                    {
                        displayMessage("Please select the company ");
                        return;
                    }
                    string opval = "";
                    foreach (GridViewRow Item in dgvAdminTeam.Rows)
                    {
                        Label lblAdminCode = (Label)Item.FindControl("lblAdminCode");
                        CheckBox chkAdminTeam = (CheckBox)Item.FindControl("chkAdminTeam");
                        // string pc = opcode.Text.ToUpper();

                        if (chkAdminTeam.Checked == true)
                        {
                            opval = opval + lblAdminCode.Text;
                        }
                    }
                    string adminteam = opval;
                    string com = ucProfitCenterSearch.Company;
                    string chanel = ucProfitCenterSearch.Channel;
                    string subChanel = ucProfitCenterSearch.SubChannel;
                    string area = ucProfitCenterSearch.Area;
                    string region = ucProfitCenterSearch.Regien;
                    string zone = ucProfitCenterSearch.Zone;
                    string pc = ucProfitCenterSearch.ProfitCenter;


                    string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();

                    //if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "REPI"))
                    //Add by Chamal 30-Aug-2013
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10045))
                    {
                        // DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                        List<string> chklstbox = new List<string>();
                        foreach (DataRow drow in dt.Rows)
                        {
                            chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        dgvLocation.DataSource = chklstbox;
                        dgvLocation.DataBind();

                    }
                    else
                    {
                        // DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                        List<string> chklstbox = new List<string>();
                        foreach (DataRow drow in dt.Rows)
                        {
                            chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        dgvLocation.DataSource = chklstbox;
                        dgvLocation.DataBind();
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

        protected void lbtnLocationClear_Click(object sender, EventArgs e)
        {
            dgvLocation.DataSource = null;
            dgvLocation.DataBind();
        }

        protected void lbtnLocationAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvLocation.Rows)
                {
                    // Label lblLocation = (Label)row.FindControl("lblLocation");
                    CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");
                    chkLocation.Checked = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnLocationNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvLocation.Rows)
                {
                    // Label lblLocation = (Label)row.FindControl("lblLocation");
                    CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");
                    chkLocation.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnClearGroup_Click(object sender, EventArgs e)
        {
            listGroup.Items.Clear();
        }
        protected void btnCat1_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT1";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 1 is already added");
                return;
            }
        }

        protected void btnCat2_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT2";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 2 is already added");
                return;
            }
        }

        protected void btnCat3_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT3";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 3 is already added");
                return;
            }
        }

        protected void btnCat4_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT4";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 4 is already added");
                return;
            }
        }

        protected void btnCat5_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT5";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Category 5 is already added");
                return;
            }
        }

        protected void btnItemCode_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "ITM";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Item is already added");
                return;
            }
        }

        protected void btnBrand_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "BRND";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Brand is already added");
                return;
            }
        }

        protected void btnModel_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "MDL";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Model is already added");
                return;
            }
        }
        protected void btnDocSubType_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DSUB";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Document sub type is already added");
                return;
            }
        }
        protected void btnDocType_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DTP";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Document type is already added");
                return;
            }
        }
        protected void btnBrandMan_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "BMAN";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Brand manager is already added");
                return;
            }
        }
        protected void btnItemStats_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "ITMS";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Item status is already added");
                return;
            }
        }
        protected void btnLocation_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "PC";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Location is already added");
                return;
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
                    //ucProfitCenterSearch.ClearText();
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
                        //ucProfitCenterSearch.Company = cellvalue;
                    }
                    else
                    {
                        //ucProfitCenterSearch.ClearText();
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
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplier.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(para, null, null);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtSupplier.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Name"].ToString();
                                return;
                            }
                        }
                    }
                    txtSupplier.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid supplier !!!')", true);
                        txtSupplier.Text = "";
                        txtSupplier.Focus();
                        return;
                    }
                }
                else
                {
                    txtSupplier.ToolTip = "";
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }



        protected void btnSupplier_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "SUPP";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Supplier is already added");
                return;
            }
        }

        protected void lbtnSeCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Customer";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(para, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _result.Columns.Remove("Code1");
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["Customer"] = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSeExecutive_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "InvoiceExecutive";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["InvoiceExecutive"] = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSePromotor_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Promotor";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotor);
                DataTable _result = CHNLSVC.CommonSearch.SearchSalesPromotor(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["Promotor"] = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (listGroup.Items.Count > 0)
            {
                if (listGroup.SelectedIndex != -1)
                {
                    listGroup.Items.RemoveAt(listGroup.SelectedIndex);
                }
            }
        }

        protected void btnDocument_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DOC";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Document is already added");
                return;
            }
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
                //dropDown.SelectedIndex = 0;
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
                    textBox.Text = "";
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
                }
                else
                {
                    chk.Enabled = true;
                }
            }

            List<RadioButton> _radList = new List<RadioButton>();
            GetControlList<RadioButton>(Page.Controls, _radList);
            foreach (var rad in _radList)
            {
                var enbCntr = _enbleRadioList.Where(c => c.ID == rad.ID).FirstOrDefault();
                if (enbCntr == null)
                {
                    rad.Enabled = false;
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
                var enbCntr = _enblelSTlList.Where(c => c.ID == listBox.ID).FirstOrDefault();
                if (enbCntr == null)
                {
                    listBox.Enabled = false;
                }
                else
                {
                    listBox.Enabled = true;
                }
                listBox.Items.Clear();
            }
            txtFromDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            txtAsAt.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            txtExDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
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

            //Item Criteria
            // txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //  txt.Add(txtCat3);
            //  txt.Add(txtCat4);
            //  txt.Add(txtCat5);
            //  txt.Add(txtItemCode);
            //  txt.Add(txtBrand);
            //  txt.Add(txtModel);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            //check
            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;

            lbtn.Add(lbtnAddCat1);

            lbtn.Add(lbtnAddCat2Remove);
            lbtn.Add(lbtnAddCat3Remove);
            lbtn.Add(lbtnAddCat4Remove);
            lbtn.Add(lbtnAddCat5Remove);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(lbtnAddItemCodeRemove);
            lbtn.Add(lbtnAddBrandRemove);
            lbtn.Add(lbtnAddModelRemove);

            lbtn.Add(btndelLocation);
            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnBrand);
            lbtn.Add(btnModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //DOc Type
            //txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //DOc sub Type
            txt.Add(txtDocSubType);
            lbtn.Add(lbtnSeDocSubType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);


            lbtn.Add(btnBrandMan);
            lbtn.Add(btnItemStats);
            lbtn.Add(btnSupplier);
            //Doc no
            txt.Add(txtDocNo);
            lbtn.Add(btnDocument);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnexective);
            lbtn.Add(btnpromoter);
            //Promoter
            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            //Invoice No
            txt.Add(txtDocNo);
            lbtn.Add(lbtnSeDocNo);
            chk.Add(chkAllDocNo);
            lbtn.Add(btnDocument);
            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            //Executive
            //txt.Add(txtExecutive);
            lbtn.Add(lbtnSeExecutive);
            chk.Add(chkAllExecutive);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            rad.Add(rbSale);
            rad.Add(rbReversal);
            rad.Add(rbAll);


            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();

            rbRepPDF.Visible = true;
            rbRepExcel.Visible = true;
            rbAll.Checked = true;
        }

        protected void rad02_CheckedChanged(object sender, EventArgs e)
        {
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

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);

            //checked
            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;


            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnBrand);
            lbtn.Add(btnModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //Doc no
            txt.Add(txtDocNo);
            lbtn.Add(btnDocument);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnexective);
            lbtn.Add(btnpromoter);

            //Promoter
            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);

            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            //Executive
            txt.Add(txtExecutive);
            lbtn.Add(lbtnSeExecutive);
            chk.Add(chkAllExecutive);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            chk.Add(chkExportExcel);

            rad.Add(rbSale);
            rad.Add(rbReversal);
            rad.Add(rbAll);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad13_CheckedChanged(object sender, EventArgs e)
        {
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

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);

            //checked
            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;


            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnBrand);
            lbtn.Add(btnModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //Doc no
            txt.Add(txtDocNo);
            lbtn.Add(btnDocument);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnexective);
            lbtn.Add(btnpromoter);

            //Promoter
            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);

            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            //Executive
            txt.Add(txtExecutive);
            lbtn.Add(lbtnSeExecutive);
            chk.Add(chkAllExecutive);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad03_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtAsAt);

            rad.Add(radOutOnly);
            rad.Add(radProfitCenterWice);
            rad.Add(radDebtorWice);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad04_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtAsAt);

            rad.Add(radOutOnly);
            rad.Add(radProfitCenterWice);
            rad.Add(radDebtorWice);

            chk.Add(chkWithCommition);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad05_CheckedChanged(object sender, EventArgs e)
        {
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

            chk.Add(chkConsiderAge);

            txt.Add(txtCustomer);
            chk.Add(chkAllCustomer);
            lbtn.Add(lbtnSeCustomer);

            rad.Add(radProfitCenterWice);
            rad.Add(radDebtorWice);


            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad11_CheckedChanged(object sender, EventArgs e)
        {
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

            //Item Criteria
            // txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //  txt.Add(txtCat3);
            //  txt.Add(txtCat4);
            //  txt.Add(txtCat5);
            //  txt.Add(txtItemCode);
            //  txt.Add(txtBrand);
            //  txt.Add(txtModel);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            //check
            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;

            lbtn.Add(lbtnAddCat1);

            lbtn.Add(lbtnAddCat2Remove);
            lbtn.Add(lbtnAddCat3Remove);
            lbtn.Add(lbtnAddCat4Remove);
            lbtn.Add(lbtnAddCat5Remove);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(lbtnAddItemCodeRemove);
            lbtn.Add(lbtnAddBrandRemove);
            lbtn.Add(lbtnAddModelRemove);

            lbtn.Add(btndelLocation);
            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnBrand);
            lbtn.Add(btnModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //DOc sub Type
            txt.Add(txtDocSubType);
            lbtn.Add(lbtnSeDocSubType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);


            lbtn.Add(btnBrandMan);
            lbtn.Add(btnItemStats);
            lbtn.Add(btnSupplier);
            //Doc no
            txt.Add(txtDocNo);
            lbtn.Add(btnDocument);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnexective);
            lbtn.Add(btnpromoter);
            //Promoter
            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            //Invoice No
            txt.Add(txtDocNo);
            lbtn.Add(lbtnSeDocNo);
            chk.Add(chkAllDocNo);
            lbtn.Add(btnDocument);
            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            //Executive
            txt.Add(txtExecutive);
            lbtn.Add(lbtnSeExecutive);
            chk.Add(chkAllExecutive);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);


            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad12_CheckedChanged(object sender, EventArgs e)
        {
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
            txt.Add(txtDiscountFrom);
            txt.Add(txtDiscountTo);

            //Item Criteria
            // txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //  txt.Add(txtCat3);
            //  txt.Add(txtCat4);
            //  txt.Add(txtCat5);
            //  txt.Add(txtItemCode);
            //  txt.Add(txtBrand);
            //  txt.Add(txtModel);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            //check
            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;

            lbtn.Add(lbtnAddCat1);

            lbtn.Add(lbtnAddCat2Remove);
            lbtn.Add(lbtnAddCat3Remove);
            lbtn.Add(lbtnAddCat4Remove);
            lbtn.Add(lbtnAddCat5Remove);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(lbtnAddItemCodeRemove);
            lbtn.Add(lbtnAddBrandRemove);
            lbtn.Add(lbtnAddModelRemove);

            lbtn.Add(btndelLocation);
            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnBrand);
            lbtn.Add(btnModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //DOc Type
            //txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //DOc sub Type
            txt.Add(txtDocSubType);
            lbtn.Add(lbtnSeDocSubType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);


            lbtn.Add(btnBrandMan);
            lbtn.Add(btnItemStats);
            lbtn.Add(btnSupplier);
            //Doc no
            txt.Add(txtDocNo);
            lbtn.Add(btnDocument);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnexective);
            lbtn.Add(btnpromoter);
            //Promoter
            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            //Invoice No
            txt.Add(txtDocNo);
            lbtn.Add(lbtnSeDocNo);
            chk.Add(chkAllDocNo);
            lbtn.Add(btnDocument);
            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            //Executive
            //txt.Add(txtExecutive);
            lbtn.Add(lbtnSeExecutive);
            chk.Add(chkAllExecutive);

            //rad.Add(radwith);
            //rad.Add(radwithout);
            //rad.Add(radfoc);


            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();

            txtDiscountFrom.Visible = true;
            txtDiscountTo.Visible = true;
            lblDicountFrom.Visible = true;
            lblDicountTo.Visible = true;

        }

        protected void rbRepExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRepExcel.Checked == true)
            {
                rbInvWise.Visible = true;
                rbItemWise.Visible = true;

                rbInvoiceCurrency.Visible = true;
                rbCompanyCurrency.Visible = true;
            }
            else
            {
                rbInvWise.Visible = false;
                rbItemWise.Visible = false;

                rbInvoiceCurrency.Visible = false;
                rbCompanyCurrency.Visible = false;
            }

        }

        protected void rbRepPDF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRepPDF.Checked == true)
            {
                rbInvWise.Visible = false;
                rbItemWise.Visible = false;

                rbInvoiceCurrency.Visible = false;
                rbCompanyCurrency.Visible = false;
            }

        }

        protected void btnCat1Remove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT1";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Category 1 is already Remove");
                return;
            }
        }

        protected void btnCat2Remove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT2";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Category 2 is already Remove");
                return;
            }
        }

        protected void btnCat3Remove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT3";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Category 3 is already Remove");
                return;
            }
        }

        protected void btnCat4Remove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT4";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Category 4 is already Remove");
                return;
            }
        }

        protected void btnCat5Remove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CAT5";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Category 5 is already Removed");
                return;
            }
        }

        protected void btnItemCodeRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "ITM";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Item is already Removed");
                return;
            }
        }

        protected void btnBrandRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "BRND";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Brand is already Removed");
                return;
            }
        }

        protected void btnModelRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "MDL";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Model is already Removed");
                return;
            }
        }

        protected void btnDocTypeRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DTP";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Document type is already Removed");
                return;
            }
        }

        protected void btnDocSubTypeRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DSUB";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Document sub type is already Removed");
                return;
            }
        }

        protected void btnDocumentRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DOC";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Document is already Removed");
                return;
            }
        }



        protected void btnBrandManRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "BMAN";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Brand manager is already Removed");
                return;
            }
        }

        protected void btnItemStatsRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "ITMS";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Item status is already Removed");
                return;
            }
        }

        protected void btnSupplierRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "SUPP";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Supplier is already Removed");
                return;
            }
        }

        protected void lbtnAddCat1Remove_Click(object sender, EventArgs e)
        {
            if (listCat1.SelectedValue != "")
            {
                bool b1 = false;
                foreach (ListItem list in listCat1.Items)
                {
                    if (list.Text == listCat1.SelectedValue)
                    {
                        b1 = true;
                    }
                }

                if (b1)
                {

                    ListItem listItem = new ListItem(listCat1.SelectedValue);
                    listCat1.Items.Remove(listItem);
                    chkAllCat1.Checked = false;
                    txtCat1.Text = "";
                }
                else
                {
                    txtCat1.Focus();
                    displayMessage(listCat1.SelectedValue + " is already Removed");
                }
            }
            else
            {
                txtCat1.Focus();
                displayMessage("Please select category 1");
            }
        }

        protected void lbtnAddCat2Remove_Click(object sender, EventArgs e)
        {
            if (listCat2.SelectedValue != "")
            {
                bool b1 = false;
                foreach (ListItem list in listCat2.Items)
                {
                    if (list.Text == listCat2.SelectedValue)
                    {
                        b1 = true;
                    }
                }

                if (b1)
                {

                    ListItem listItem = new ListItem(listCat2.SelectedValue);
                    listCat2.Items.Remove(listItem);
                    chkAllCat1.Checked = false;
                    txtCat1.Text = "";
                }
                else
                {
                    txtCat1.Focus();
                    displayMessage(listCat2.SelectedValue + " is already Removed");
                }
            }
            else
            {
                txtCat2.Focus();
                displayMessage("Please select category 2");
            }
        }

        protected void lbtnAddCat3Remove_Click(object sender, EventArgs e)
        {
            if (listCat3.SelectedValue != "")
            {
                bool b1 = false;
                foreach (ListItem list in listCat3.Items)
                {
                    if (list.Text == listCat3.SelectedValue)
                    {
                        b1 = true;
                    }
                }

                if (b1)
                {

                    ListItem listItem = new ListItem(listCat3.SelectedValue);
                    listCat3.Items.Remove(listItem);
                    chkAllCat3.Checked = false;
                    txtCat1.Text = "";
                }
                else
                {
                    txtCat1.Focus();
                    displayMessage(listCat3.SelectedValue + " is already Removed");
                }
            }
            else
            {
                txtCat3.Focus();
                displayMessage("Please select category 3");
            }
        }

        protected void lbtnAddCat4Remove_Click(object sender, EventArgs e)
        {
            if (listCat4.SelectedValue != "")
            {
                bool b1 = false;
                foreach (ListItem list in listCat4.Items)
                {
                    if (list.Text == listCat4.SelectedValue)
                    {
                        b1 = true;
                    }
                }

                if (b1)
                {

                    ListItem listItem = new ListItem(listCat4.SelectedValue);
                    listCat4.Items.Remove(listItem);
                    chkAllCat4.Checked = false;
                    txtCat4.Text = "";
                }
                else
                {
                    txtCat4.Focus();
                    displayMessage(listCat4.SelectedValue + " is already Removed");
                }
            }
            else
            {
                txtCat4.Focus();
                displayMessage("Please select category 4");
            }
        }

        protected void lbtnAddCat5Remove_Click(object sender, EventArgs e)
        {
            if (listCat5.SelectedValue != "")
            {
                bool b1 = false;
                foreach (ListItem list in listCat5.Items)
                {
                    if (list.Text == listCat5.SelectedValue)
                    {
                        b1 = true;
                    }
                }

                if (b1)
                {

                    ListItem listItem = new ListItem(listCat5.SelectedValue);
                    listCat5.Items.Remove(listItem);
                    chkAllCat5.Checked = false;
                    txtCat5.Text = "";
                }
                else
                {
                    txtCat5.Focus();
                    displayMessage(listCat5.SelectedValue + " is already Removed");
                }
            }
            else
            {
                txtCat5.Focus();
                displayMessage("Please select category 5");
            }
        }

        protected void lbtnAddBrandRemove_Click(object sender, EventArgs e)
        {
            if (listBrand.SelectedValue != "")
            {
                bool b1 = false;
                foreach (ListItem list in listBrand.Items)
                {
                    if (list.Text == listBrand.SelectedValue)
                    {
                        b1 = true;
                    }
                }

                if (b1)
                {

                    ListItem listItem = new ListItem(listBrand.SelectedValue);
                    listBrand.Items.Remove(listItem);
                    //  listBrand.Checked = false;
                    txtBrand.Text = "";
                }
                else
                {
                    txtBrand.Focus();
                    displayMessage(listBrand.SelectedValue + " is already Removed");
                }
            }
            else
            {
                txtBrand.Focus();
                displayMessage("Please select Brand");
            }
        }

        protected void lbtnAddModelRemove_Click(object sender, EventArgs e)
        {
            if (listModel.SelectedValue != "")
            {
                bool b1 = false;
                foreach (ListItem list in listModel.Items)
                {
                    if (list.Text == listModel.SelectedValue)
                    {
                        b1 = true;
                    }
                }

                if (b1)
                {

                    ListItem listItem = new ListItem(listModel.SelectedValue);
                    listModel.Items.Remove(listItem);
                    //  chkAllCat1.Checked = false;
                    txtModel.Text = "";
                }
                else
                {
                    txtModel.Focus();
                    displayMessage(listModel.SelectedValue + " is already Removed");
                }
            }
            else
            {
                txtModel.Focus();
                displayMessage("Please select Model");
            }
        }

        protected void lbtnAddItemCodeRemove_Click(object sender, EventArgs e)
        {
            if (listItemCode.SelectedValue != "")
            {
                bool b1 = false;
                foreach (ListItem list in listItemCode.Items)
                {
                    if (list.Text == listItemCode.SelectedValue)
                    {
                        b1 = true;
                    }
                }

                if (b1)
                {

                    ListItem listItem = new ListItem(listItemCode.SelectedValue);
                    listItemCode.Items.Remove(listItem);
                    //   chkAllCat1.Checked = false;
                    txtItemCode.Text = "";


                }
                else
                {
                    txtItemCode.Focus();
                    displayMessage(listItemCode.SelectedValue + " is already Removed");
                }
            }
            else
            {
                txtItemCode.Focus();
                displayMessage("Please select Item Code");
            }
        }

        protected void rad06_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnSeCustormer_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CUST";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Custormer is already added");
                return;
            }
        }

        protected void btnSeCustormerRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "CUST";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Custormer is already Removed");
                return;
            }
        }

        protected void btnpromoter_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "PRM";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Promoter is already added");
                return;
            }
        }

        protected void btnpromoterRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "PRM";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Promoter is already Removed");
                return;
            }
        }

        protected void btnexective_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "EXEC";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Executive is already added");
                return;
            }
        }

        protected void btnexectiveRemove_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "EXEC";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (b1)
            {
                listGroup.Items.Remove(new ListItem(item));
            }
            else
            {
                displayMessage("Executive is already Removed");
                return;
            }
        }

        protected void rad07_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //Date criteria
            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void chkAllCustomer_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomer.Text = "";
            if (chkAllCustomer.Checked)
            {
                txtCustomer.Enabled = false;
            }
            else
            {
                txtCustomer.Enabled = true;
            }
        }

        protected void chkAllExecutive_CheckedChanged(object sender, EventArgs e)
        {
            txtExecutive.Text = "";
            if (chkAllExecutive.Checked)
            {
                txtExecutive.Enabled = false;
            }
            else
            {
                txtExecutive.Enabled = true;
            }
        }

        protected void chkAllPromotor_CheckedChanged(object sender, EventArgs e)
        {
            txtPromotor.Text = "";
            if (chkAllPromotor.Checked)
            {
                txtPromotor.Enabled = false;
            }
            else
            {
                txtPromotor.Enabled = true;
            }

        }

        protected void btndelLocation_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "DLOC";
            foreach (ListItem list in listGroup.Items)
            {
                if (list.Text == item)
                {
                    b1 = true;
                    break;
                }
            }
            if (!b1)
            {
                listGroup.Items.Add(new ListItem(item));
            }
            else
            {
                displayMessage("Del Location is already added");
                return;
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

        protected void rad08_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //Date criteria
            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad09_CheckedChanged(object sender, EventArgs e)
        {
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

            //Item Criteria
            // txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //  txt.Add(txtCat3);
            //  txt.Add(txtCat4);
            //  txt.Add(txtCat5);
            //  txt.Add(txtItemCode);
            //  txt.Add(txtBrand);
            //  txt.Add(txtModel);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            //check
            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;

            lbtn.Add(lbtnAddCat1);

            lbtn.Add(lbtnAddCat2Remove);
            lbtn.Add(lbtnAddCat3Remove);
            lbtn.Add(lbtnAddCat4Remove);
            lbtn.Add(lbtnAddCat5Remove);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(lbtnAddItemCodeRemove);
            lbtn.Add(lbtnAddBrandRemove);
            lbtn.Add(lbtnAddModelRemove);

            lbtn.Add(btndelLocation);
            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnBrand);
            lbtn.Add(btnModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //DOc sub Type
            txt.Add(txtDocSubType);
            lbtn.Add(lbtnSeDocSubType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);

            lbtn.Add(btnBrandMan);
            lbtn.Add(btnItemStats);
            lbtn.Add(btnSupplier);
            //Doc no
            txt.Add(txtDocNo);
            lbtn.Add(btnDocument);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnexective);
            lbtn.Add(btnpromoter);

            //Invoice No
            txt.Add(txtDocNo);
            lbtn.Add(lbtnSeDocNo);
            chk.Add(chkAllDocNo);
            lbtn.Add(btnDocument);
            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            //Executive
            txt.Add(txtExecutive);
            lbtn.Add(lbtnSeExecutive);
            chk.Add(chkAllExecutive);
            //Supplier
            txt.Add(txtSupplier);
            lbtn.Add(lbtnSeSupplier);
            chk.Add(chkAllSupplier);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad10_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //Date criteria
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);
            //Customer
            txt.Add(txtCustomer);

            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();

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
        protected void btnDownloadfile_Click(object sender, EventArgs e)
        {
            DownloadFileinSever();
        }
        protected void rad14_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //Date criteria
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);
            //Customer
            // txt.Add(txtCustomer);

            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();

        }

        protected void rad15_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16063))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12500)");
                rad15.Checked = false;
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

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);

            //checked
            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;


            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnBrand);
            lbtn.Add(btnModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //Doc no
            txt.Add(txtDocNo);
            lbtn.Add(btnDocument);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnexective);
            lbtn.Add(btnpromoter);

            //Promoter
            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);

            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            //Executive
            txt.Add(txtExecutive);
            lbtn.Add(lbtnSeExecutive);
            chk.Add(chkAllExecutive);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            chk.Add(chkExportExcel);

            rad.Add(rbSale);
            rad.Add(rbReversal);
            rad.Add(rbAll);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();

        }

        protected void rad16_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16066))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16066)");
                rad15.Checked = false;
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
            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);
            lbtn.Add(btnSeCustormer);

            //Invoice No
            txt.Add(txtDocNo);
            lbtn.Add(lbtnSeDocNo);
            lbtn.Add(btnDocument);

            //Sale Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }


        // Isuru 2017/05/22 
        protected void rad17_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16077))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16077)");
                rad17.Checked = false;
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
            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);
            lbtn.Add(btnSeCustormer);

            //cheque no
            txt.Add(txtcheque);
            lbtn.Add(lbtnsecheque);
            chk.Add(CheckALLCheque);

            //aacntno

            txt.Add(txtaccountcode);
            lbtn.Add(lbtnseaccountcode);
            chk.Add(CheckAllAccountCode);


            //Invoice No
            //txt.Add(txtDocNo);
            //lbtn.Add(lbtnSeDocNo);
            //lbtn.Add(btnDocument);



            //
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void lbtnsecheque_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Chequedet";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Chequedet);
                DataTable _result = CHNLSVC.CommonSearch.SearchChequeCodes(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["Chequedet"] = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void CheckALLCheque_CheckedChanged(object sender, EventArgs e)
        {
            txtcheque.Text = "";
            if (CheckALLCheque.Checked)
            {
                txtcheque.Enabled = false;
            }
            else
            {
                txtcheque.Enabled = true;
            }
        }

        protected void lbtnseaccountcode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "accountcode";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.accountcode);
                DataTable _result = CHNLSVC.CommonSearch.Searchaccountcode(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["accountcode"] = _result;
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void CheckAllAccountCode_CheckedChanged(object sender, EventArgs e)
        {
            txtaccountcode.Text = "";
            if (CheckAllAccountCode.Checked)
            {
                txtaccountcode.Enabled = false;
            }
            else
            {
                txtaccountcode.Enabled = true;
            }
        }

        protected void rad18_CheckedChanged(object sender, EventArgs e)
        {
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

            //Item Criteria
            // txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //  txt.Add(txtCat3);
            //  txt.Add(txtCat4);
            //  txt.Add(txtCat5);
            //  txt.Add(txtItemCode);
            //  txt.Add(txtBrand);
            //  txt.Add(txtModel);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            //check
            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;

            lbtn.Add(lbtnAddCat1);

            lbtn.Add(lbtnAddCat2Remove);
            lbtn.Add(lbtnAddCat3Remove);
            lbtn.Add(lbtnAddCat4Remove);
            lbtn.Add(lbtnAddCat5Remove);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(lbtnAddItemCodeRemove);
            lbtn.Add(lbtnAddBrandRemove);
            lbtn.Add(lbtnAddModelRemove);

            lbtn.Add(btndelLocation);
            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnBrand);
            lbtn.Add(btnModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //DOc Type
            //txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //DOc sub Type
            txt.Add(txtDocSubType);
            lbtn.Add(lbtnSeDocSubType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);

            lbtn.Add(btnBrandMan);
            lbtn.Add(btnItemStats);
            lbtn.Add(btnSupplier);
            //Doc no
            txt.Add(txtDocNo);
            lbtn.Add(btnDocument);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnexective);
            lbtn.Add(btnpromoter);
            //Promoter
            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            //Invoice No
            txt.Add(txtDocNo);
            lbtn.Add(lbtnSeDocNo);
            chk.Add(chkAllDocNo);
            lbtn.Add(btnDocument);
            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            //Executive
            //txt.Add(txtExecutive);
            lbtn.Add(lbtnSeExecutive);
            chk.Add(chkAllExecutive);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            rad.Add(rbSale);
            rad.Add(rbReversal);
            rad.Add(rbAll);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();

            rbRepPDF.Visible = true;
            rbRepExcel.Visible = true;
            rbAll.Checked = true;
        }
        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
           DataTable dt = new DataTable();
           string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
           dt = CHNLSVC.CommonSearch.GetCustomerGenaral(para, "Code", "%" + txtCustomer.Text.Trim(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
           foreach(DataRow item in dt.Rows)
           {
               ViewState["Name"] = item.Field<string>("Name");
           }
        }

        protected void rad19_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            //Customer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);
            //Date
            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            EnableControler();
        }

        protected void lBtnDilogResultYes_Click(object sender, EventArgs e)
        {
            lblMssg1.Text = "Do you need to run the report register/unregister-wise ?";
            PopupConfBox1.Show();
            return;
        }

        protected void lBtnDilogResultNo_Click(object sender, EventArgs e)
        {
            clsSales obj = new clsSales();
            # region
            MasterCompany _newCom = new MasterCompany();
            _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
            string com_desc = _newCom.Mc_desc;
            string com_add = _newCom.Mc_add1 + " " + _newCom.Mc_add2;

            InvReportPara _invRepPara = new InvReportPara();
            _invRepPara._GlbReportBrand = Session["GlbReportBrand"].ToString();
            _invRepPara._GlbReportModel = Session["GlbReportModel"].ToString();
            _invRepPara._GlbReportItemCode = Session["GlbReportItemCode"].ToString();
            _invRepPara._GlbReportItemCat1 = Session["GlbReportItemCat1"].ToString();
            _invRepPara._GlbReportItemCat2 = Session["GlbReportItemCat2"].ToString();
            _invRepPara._GlbReportItemCat3 = Session["GlbReportItemCat3"].ToString();
            _invRepPara._GlbReportItemCat4 = Session["GlbReportItemCat4"].ToString();
            _invRepPara._GlbReportItemCat5 = Session["GlbReportItemCat5"].ToString();
            _invRepPara._GlbReportItemStatus = Session["GlbReportItemStatus"].ToString();
            _invRepPara._GlbReportDoc2 = Session["GlbReportDoc2"].ToString();
            _invRepPara._GlbReportJobStatus = Session["GlbReportJobStatus"].ToString();
            _invRepPara._GlbReportWithRCC = Session["GlbReportWithRCC"].ToString();
            _invRepPara._GlbReportWithCost = Convert.ToInt16(Session["GlbReportWithCost"]);
            _invRepPara._GlbReportDocType = Session["GlbReportDocType"].ToString();
            _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
            _invRepPara._GlbReportDocSubType = Session["GlbReportDocSubType"].ToString();
            _invRepPara._GlbReportCompName = Session["GlbReportCompName"].ToString();
            _invRepPara._GlbReportCompanies = Session["GlbReportCompanies"].ToString();

            _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
            _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
            _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
            _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString();
            _invRepPara._GlbReportExecutive = txtExecutive.Text.ToString();
            _invRepPara._GlbReportCustomer = txtCustomer.Text.ToString();
            _invRepPara._GlbReportPromotor = txtPromotor.Text.ToString();
            _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
            _invRepPara._GlbReportProfit = ucProfitCenterSearch.TxtProfCenter.Text == "" ? "All Profit centers based on user rights" : ucProfitCenterSearch.TxtProfCenter.Text;
            _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
            _invRepPara._GlbUserID = Session["UserID"].ToString();
            _invRepPara._GlbReportWithJob = "N";
            _invRepPara._GlbReportCheckRegDate = 0;
            _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
            _invRepPara._GlbReportComp = com_desc;
            _invRepPara._GlbReportCompAddr = com_add;

            string _exec = CHNLSVC.MsgPortal.getSalesRestrictbyUser(Session["UserID"].ToString());
            if (!(_exec == null | _exec == ""))
            {
                _invRepPara._GlbReportExecutive = _exec;
            }

            int i = 1;
            int j = listGroup.Items.Count;
            _invRepPara._GlbReportGroupPC = 0;
            _invRepPara._GlbReportGroupDLoc = 0;
            _invRepPara._GlbReportGroupDocTp = 0;
            _invRepPara._GlbReportGroupCust = 0;
            _invRepPara._GlbReportGroupExec = 0;
            _invRepPara._GlbReportGroupItem = 0;
            _invRepPara._GlbReportGroupBrand = 0;
            _invRepPara._GlbReportGroupModel = 0;
            _invRepPara._GlbReportGroupCat1 = 0;
            _invRepPara._GlbReportGroupCat2 = 0;
            _invRepPara._GlbReportGroupCat3 = 0;
            _invRepPara._GlbReportGroupCat4 = 0;
            _invRepPara._GlbReportGroupCat5 = 0;
            _invRepPara._GlbReportGroupLastGroup = 0;
            _invRepPara._GlbReportGroupDocNo = 0;
            _invRepPara._GlbReportGroupStockTp = 0;
            _invRepPara._GlbReportGroupPromotor = 0;
            _invRepPara._GlbReportGroupJobNo = 0;
            _invRepPara._GlbReportGroupLastGroupCat = "";
            _invRepPara._GlbReportGroupCheque = 0;
            _invRepPara._GlbReportGroupaccountcode = 0;

            foreach (ListItem Item in listGroup.Items)
            {
                if (Item.Text == "PC")
                {
                    _invRepPara._GlbReportGroupPC = i;
                }
                if (Item.Text == "DLOC")
                {
                    _invRepPara._GlbReportGroupDLoc = i;
                }
                if (Item.Text == "DTP")
                {
                    _invRepPara._GlbReportGroupDocTp = i;
                }
                if (Item.Text == "CUST")
                {
                    _invRepPara._GlbReportGroupCust = i;
                }
                if (Item.Text == "EXEC")
                {
                    _invRepPara._GlbReportGroupExec = i;
                }
                if (Item.Text == "ITM")
                {
                    _invRepPara._GlbReportGroupItem = i;
                }
                if (Item.Text == "BRND")
                {
                    _invRepPara._GlbReportGroupBrand = i;
                }
                if (Item.Text == "MDL")
                {
                    _invRepPara._GlbReportGroupModel = i;
                }
                if (Item.Text == "CAT1")
                {
                    _invRepPara._GlbReportGroupCat1 = i;
                }
                if (Item.Text == "CAT2")
                {
                    _invRepPara._GlbReportGroupCat2 = i;
                }
                if (Item.Text == "CAT3")
                {
                    _invRepPara._GlbReportGroupCat3 = i;
                }
                if (Item.Text == "CAT4")
                {
                    _invRepPara._GlbReportGroupCat4 = i;
                }
                if (Item.Text == "CAT5")
                {
                    _invRepPara._GlbReportGroupCat5 = i;
                }
                if (Item.Text == "DOC")
                {
                    _invRepPara._GlbReportGroupDocNo = i;
                }
                if (Item.Text == "STK")
                {
                    _invRepPara._GlbReportGroupStockTp = i;
                }
                if (Item.Text == "PRM")
                {
                    _invRepPara._GlbReportGroupPromotor = i;
                }
                if (Item.Text == "CHQ")
                {
                    _invRepPara._GlbReportGroupCheque = i;
                }
                if (Item.Text == "ACC")
                {
                    _invRepPara._GlbReportGroupaccountcode = i;
                }
                _invRepPara._GlbReportGroupLastGroup = j;
                if (j == i)
                {
                    _invRepPara._GlbReportGroupLastGroupCat = Item.Text;
                }
                i++;
            }
            if (j == 0)
            {
                _invRepPara._GlbReportGroupPC = 1;
                _invRepPara._GlbReportGroupItem = 2;
                _invRepPara._GlbReportGroupLastGroup = 2;
                _invRepPara._GlbReportGroupLastGroupCat = "ITM";
            }

            _opt = "";
            #endregion

            _invRepPara._GlbReportCustomer = chkAllCustomer.Checked ? "ALL" : txtCustomer.Text;
            _invRepPara._GlbReportHeading = "Age Arrears of Debtors Outstanding";
            Session["InvReportPara"] = _invRepPara;
            // Response.Redirect("SalesReportViewer.aspx", false);
            _invRepPara._GlbReportName = "Age_Debtor_Outstanding_adv.rpt";
            Session["GlbReportName"] = _invRepPara._GlbReportName;
        }

        protected void btnalertYes1_Click(object sender, EventArgs e)
        {
            clsSales obj = new clsSales();
            # region
            MasterCompany _newCom = new MasterCompany();
            _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
            string com_desc = _newCom.Mc_desc;
            string com_add = _newCom.Mc_add1 + " " + _newCom.Mc_add2;

            InvReportPara _invRepPara = new InvReportPara();
            _invRepPara._GlbReportBrand = Session["GlbReportBrand"].ToString();
            _invRepPara._GlbReportModel = Session["GlbReportModel"].ToString();
            _invRepPara._GlbReportItemCode = Session["GlbReportItemCode"].ToString();
            _invRepPara._GlbReportItemCat1 = Session["GlbReportItemCat1"].ToString();
            _invRepPara._GlbReportItemCat2 = Session["GlbReportItemCat2"].ToString();
            _invRepPara._GlbReportItemCat3 = Session["GlbReportItemCat3"].ToString();
            _invRepPara._GlbReportItemCat4 = Session["GlbReportItemCat4"].ToString();
            _invRepPara._GlbReportItemCat5 = Session["GlbReportItemCat5"].ToString();
            _invRepPara._GlbReportItemStatus = Session["GlbReportItemStatus"].ToString();
            _invRepPara._GlbReportDoc2 = Session["GlbReportDoc2"].ToString();
            _invRepPara._GlbReportJobStatus = Session["GlbReportJobStatus"].ToString();
            _invRepPara._GlbReportWithRCC = Session["GlbReportWithRCC"].ToString();
            _invRepPara._GlbReportWithCost = Convert.ToInt16(Session["GlbReportWithCost"]);
            _invRepPara._GlbReportDocType = Session["GlbReportDocType"].ToString();
            _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
            _invRepPara._GlbReportDocSubType = Session["GlbReportDocSubType"].ToString();
            _invRepPara._GlbReportCompName = Session["GlbReportCompName"].ToString();
            _invRepPara._GlbReportCompanies = Session["GlbReportCompanies"].ToString();

            _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
            _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
            _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
            _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString();
            _invRepPara._GlbReportExecutive = txtExecutive.Text.ToString();
            _invRepPara._GlbReportCustomer = txtCustomer.Text.ToString();
            _invRepPara._GlbReportPromotor = txtPromotor.Text.ToString();
            _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
            _invRepPara._GlbReportProfit = ucProfitCenterSearch.TxtProfCenter.Text == "" ? "All Profit centers based on user rights" : ucProfitCenterSearch.TxtProfCenter.Text;
            _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
            _invRepPara._GlbUserID = Session["UserID"].ToString();
            _invRepPara._GlbReportWithJob = "N";
            _invRepPara._GlbReportCheckRegDate = 0;
            _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
            _invRepPara._GlbReportComp = com_desc;
            _invRepPara._GlbReportCompAddr = com_add;

            string _exec = CHNLSVC.MsgPortal.getSalesRestrictbyUser(Session["UserID"].ToString());
            if (!(_exec == null | _exec == ""))
            {
                _invRepPara._GlbReportExecutive = _exec;
            }

            int i = 1;
            int j = listGroup.Items.Count;
            _invRepPara._GlbReportGroupPC = 0;
            _invRepPara._GlbReportGroupDLoc = 0;
            _invRepPara._GlbReportGroupDocTp = 0;
            _invRepPara._GlbReportGroupCust = 0;
            _invRepPara._GlbReportGroupExec = 0;
            _invRepPara._GlbReportGroupItem = 0;
            _invRepPara._GlbReportGroupBrand = 0;
            _invRepPara._GlbReportGroupModel = 0;
            _invRepPara._GlbReportGroupCat1 = 0;
            _invRepPara._GlbReportGroupCat2 = 0;
            _invRepPara._GlbReportGroupCat3 = 0;
            _invRepPara._GlbReportGroupCat4 = 0;
            _invRepPara._GlbReportGroupCat5 = 0;
            _invRepPara._GlbReportGroupLastGroup = 0;
            _invRepPara._GlbReportGroupDocNo = 0;
            _invRepPara._GlbReportGroupStockTp = 0;
            _invRepPara._GlbReportGroupPromotor = 0;
            _invRepPara._GlbReportGroupJobNo = 0;
            _invRepPara._GlbReportGroupLastGroupCat = "";
            _invRepPara._GlbReportGroupCheque = 0;
            _invRepPara._GlbReportGroupaccountcode = 0;

            foreach (ListItem Item in listGroup.Items)
            {
                if (Item.Text == "PC")
                {
                    _invRepPara._GlbReportGroupPC = i;
                }
                if (Item.Text == "DLOC")
                {
                    _invRepPara._GlbReportGroupDLoc = i;
                }
                if (Item.Text == "DTP")
                {
                    _invRepPara._GlbReportGroupDocTp = i;
                }
                if (Item.Text == "CUST")
                {
                    _invRepPara._GlbReportGroupCust = i;
                }
                if (Item.Text == "EXEC")
                {
                    _invRepPara._GlbReportGroupExec = i;
                }
                if (Item.Text == "ITM")
                {
                    _invRepPara._GlbReportGroupItem = i;
                }
                if (Item.Text == "BRND")
                {
                    _invRepPara._GlbReportGroupBrand = i;
                }
                if (Item.Text == "MDL")
                {
                    _invRepPara._GlbReportGroupModel = i;
                }
                if (Item.Text == "CAT1")
                {
                    _invRepPara._GlbReportGroupCat1 = i;
                }
                if (Item.Text == "CAT2")
                {
                    _invRepPara._GlbReportGroupCat2 = i;
                }
                if (Item.Text == "CAT3")
                {
                    _invRepPara._GlbReportGroupCat3 = i;
                }
                if (Item.Text == "CAT4")
                {
                    _invRepPara._GlbReportGroupCat4 = i;
                }
                if (Item.Text == "CAT5")
                {
                    _invRepPara._GlbReportGroupCat5 = i;
                }
                if (Item.Text == "DOC")
                {
                    _invRepPara._GlbReportGroupDocNo = i;
                }
                if (Item.Text == "STK")
                {
                    _invRepPara._GlbReportGroupStockTp = i;
                }
                if (Item.Text == "PRM")
                {
                    _invRepPara._GlbReportGroupPromotor = i;
                }
                if (Item.Text == "CHQ")
                {
                    _invRepPara._GlbReportGroupCheque = i;
                }
                if (Item.Text == "ACC")
                {
                    _invRepPara._GlbReportGroupaccountcode = i;
                }
                _invRepPara._GlbReportGroupLastGroup = j;
                if (j == i)
                {
                    _invRepPara._GlbReportGroupLastGroupCat = Item.Text;
                }
                i++;
            }
            if (j == 0)
            {
                _invRepPara._GlbReportGroupPC = 1;
                _invRepPara._GlbReportGroupItem = 2;
                _invRepPara._GlbReportGroupLastGroup = 2;
                _invRepPara._GlbReportGroupLastGroupCat = "ITM";
            }

            _opt = "";
            #endregion

            _invRepPara._GlbReportCustomer = chkAllCustomer.Checked ? "ALL" : txtCustomer.Text;
            _invRepPara._GlbReportHeading = "Age Arrears of Debtors Outstanding";
            Session["InvReportPara"] = _invRepPara;
            // Response.Redirect("SalesReportViewer.aspx", false);

            obj.AgeAnalysisOfDebtorsOutstandingPrint(_invRepPara);

            if (radProfitCenterWice.Checked == true)//optPC
            {
                _invRepPara._GlbReportName = "Age_Debtor_Outstanding_PC.rpt";
                Session["GlbReportName"] = _invRepPara._GlbReportName;
            }
            else
            {
                _invRepPara._GlbReportName = "Age_Debtor_Outstanding.rpt";
                Session["GlbReportName"] = _invRepPara._GlbReportName;
            }
        }

        protected void btnalertNo1_Click(object sender, EventArgs e)
        {
            clsSales obj = new clsSales();
            # region
            MasterCompany _newCom = new MasterCompany();
            _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
            string com_desc = _newCom.Mc_desc;
            string com_add = _newCom.Mc_add1 + " " + _newCom.Mc_add2;

            InvReportPara _invRepPara = new InvReportPara();
            _invRepPara._GlbReportBrand = Session["GlbReportBrand"].ToString();
            _invRepPara._GlbReportModel = Session["GlbReportModel"].ToString();
            _invRepPara._GlbReportItemCode = Session["GlbReportItemCode"].ToString();
            _invRepPara._GlbReportItemCat1 = Session["GlbReportItemCat1"].ToString();
            _invRepPara._GlbReportItemCat2 = Session["GlbReportItemCat2"].ToString();
            _invRepPara._GlbReportItemCat3 = Session["GlbReportItemCat3"].ToString();
            _invRepPara._GlbReportItemCat4 = Session["GlbReportItemCat4"].ToString();
            _invRepPara._GlbReportItemCat5 = Session["GlbReportItemCat5"].ToString();
            _invRepPara._GlbReportItemStatus = Session["GlbReportItemStatus"].ToString();
            _invRepPara._GlbReportDoc2 = Session["GlbReportDoc2"].ToString();
            _invRepPara._GlbReportJobStatus = Session["GlbReportJobStatus"].ToString();
            _invRepPara._GlbReportWithRCC = Session["GlbReportWithRCC"].ToString();
            _invRepPara._GlbReportWithCost = Convert.ToInt16(Session["GlbReportWithCost"]);
            _invRepPara._GlbReportDocType = Session["GlbReportDocType"].ToString();
            _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
            _invRepPara._GlbReportDocSubType = Session["GlbReportDocSubType"].ToString();
            _invRepPara._GlbReportCompName = Session["GlbReportCompName"].ToString();
            _invRepPara._GlbReportCompanies = Session["GlbReportCompanies"].ToString();

            _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
            _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
            _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
            _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString();
            _invRepPara._GlbReportExecutive = txtExecutive.Text.ToString();
            _invRepPara._GlbReportCustomer = txtCustomer.Text.ToString();
            _invRepPara._GlbReportPromotor = txtPromotor.Text.ToString();
            _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
            _invRepPara._GlbReportProfit = ucProfitCenterSearch.TxtProfCenter.Text == "" ? "All Profit centers based on user rights" : ucProfitCenterSearch.TxtProfCenter.Text;
            _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
            _invRepPara._GlbUserID = Session["UserID"].ToString();
            _invRepPara._GlbReportWithJob = "N";
            _invRepPara._GlbReportCheckRegDate = 0;
            _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
            _invRepPara._GlbReportComp = com_desc;
            _invRepPara._GlbReportCompAddr = com_add;

            string _exec = CHNLSVC.MsgPortal.getSalesRestrictbyUser(Session["UserID"].ToString());
            if (!(_exec == null | _exec == ""))
            {
                _invRepPara._GlbReportExecutive = _exec;
            }

            int i = 1;
            int j = listGroup.Items.Count;
            _invRepPara._GlbReportGroupPC = 0;
            _invRepPara._GlbReportGroupDLoc = 0;
            _invRepPara._GlbReportGroupDocTp = 0;
            _invRepPara._GlbReportGroupCust = 0;
            _invRepPara._GlbReportGroupExec = 0;
            _invRepPara._GlbReportGroupItem = 0;
            _invRepPara._GlbReportGroupBrand = 0;
            _invRepPara._GlbReportGroupModel = 0;
            _invRepPara._GlbReportGroupCat1 = 0;
            _invRepPara._GlbReportGroupCat2 = 0;
            _invRepPara._GlbReportGroupCat3 = 0;
            _invRepPara._GlbReportGroupCat4 = 0;
            _invRepPara._GlbReportGroupCat5 = 0;
            _invRepPara._GlbReportGroupLastGroup = 0;
            _invRepPara._GlbReportGroupDocNo = 0;
            _invRepPara._GlbReportGroupStockTp = 0;
            _invRepPara._GlbReportGroupPromotor = 0;
            _invRepPara._GlbReportGroupJobNo = 0;
            _invRepPara._GlbReportGroupLastGroupCat = "";
            _invRepPara._GlbReportGroupCheque = 0;
            _invRepPara._GlbReportGroupaccountcode = 0;

            foreach (ListItem Item in listGroup.Items)
            {
                if (Item.Text == "PC")
                {
                    _invRepPara._GlbReportGroupPC = i;
                }
                if (Item.Text == "DLOC")
                {
                    _invRepPara._GlbReportGroupDLoc = i;
                }
                if (Item.Text == "DTP")
                {
                    _invRepPara._GlbReportGroupDocTp = i;
                }
                if (Item.Text == "CUST")
                {
                    _invRepPara._GlbReportGroupCust = i;
                }
                if (Item.Text == "EXEC")
                {
                    _invRepPara._GlbReportGroupExec = i;
                }
                if (Item.Text == "ITM")
                {
                    _invRepPara._GlbReportGroupItem = i;
                }
                if (Item.Text == "BRND")
                {
                    _invRepPara._GlbReportGroupBrand = i;
                }
                if (Item.Text == "MDL")
                {
                    _invRepPara._GlbReportGroupModel = i;
                }
                if (Item.Text == "CAT1")
                {
                    _invRepPara._GlbReportGroupCat1 = i;
                }
                if (Item.Text == "CAT2")
                {
                    _invRepPara._GlbReportGroupCat2 = i;
                }
                if (Item.Text == "CAT3")
                {
                    _invRepPara._GlbReportGroupCat3 = i;
                }
                if (Item.Text == "CAT4")
                {
                    _invRepPara._GlbReportGroupCat4 = i;
                }
                if (Item.Text == "CAT5")
                {
                    _invRepPara._GlbReportGroupCat5 = i;
                }
                if (Item.Text == "DOC")
                {
                    _invRepPara._GlbReportGroupDocNo = i;
                }
                if (Item.Text == "STK")
                {
                    _invRepPara._GlbReportGroupStockTp = i;
                }
                if (Item.Text == "PRM")
                {
                    _invRepPara._GlbReportGroupPromotor = i;
                }
                if (Item.Text == "CHQ")
                {
                    _invRepPara._GlbReportGroupCheque = i;
                }
                if (Item.Text == "ACC")
                {
                    _invRepPara._GlbReportGroupaccountcode = i;
                }
                _invRepPara._GlbReportGroupLastGroup = j;
                if (j == i)
                {
                    _invRepPara._GlbReportGroupLastGroupCat = Item.Text;
                }
                i++;
            }
            if (j == 0)
            {
                _invRepPara._GlbReportGroupPC = 1;
                _invRepPara._GlbReportGroupItem = 2;
                _invRepPara._GlbReportGroupLastGroup = 2;
                _invRepPara._GlbReportGroupLastGroupCat = "ITM";
            }

            _opt = "";
            #endregion
            _invRepPara._GlbReportCustomer = chkAllCustomer.Checked ? "ALL" : txtCustomer.Text;
            _invRepPara._GlbReportHeading = "Age Arrears of Debtors Outstanding";
            Session["InvReportPara"] = _invRepPara;
            // Response.Redirect("SalesReportViewer.aspx", false);

            obj.AgeAnalysisOfDebtorsOutstandingPrint(_invRepPara);

            if (radProfitCenterWice.Checked == true)
            {
                _invRepPara._GlbReportName = "Age_Debtor_Outstanding_PC_new.rpt";
                Session["GlbReportName"] = _invRepPara._GlbReportName;
            }
            else
            {
                _invRepPara._GlbReportName = "Age_Debtor_Outstanding_new.rpt";
                Session["GlbReportName"]  = _invRepPara._GlbReportName;
            }

        }
    }
}