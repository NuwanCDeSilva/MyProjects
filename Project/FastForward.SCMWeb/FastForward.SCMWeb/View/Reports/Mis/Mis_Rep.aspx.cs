using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using FF.BusinessObjects.InventoryNew;
using System.Web.UI.WebControls;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Sales;
using FastForward.SCMWeb.View.Reports.Inventory;
using Excel = Microsoft.Office.Interop.Excel;
using FastForward.SCMWeb.UserControls;
using FastForward.SCMWeb.Services;

namespace FastForward.SCMWeb.View.Reports.Mis
{
    public partial class Mis_Rep : BasePage
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
        protected List<proflist> _pclist { get { return (List<proflist>)Session["_pclist"]; } set { Session["_pclist"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                _pclist = new List<proflist>();
                Session["DATAADMIN"] = null;
                Session["DATACOMPANY"] = null;
                LoadEmptyGrid();
                BindCompany(Session["UserID"].ToString());
                ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                ucLoactionSearch._allcom1 = 1;
                txtFromDate.Text = (DateTime.Now.Date.AddMonths(-1)).ToString("dd/MMM/yyyy");
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtFromDate2.Text = (DateTime.Now.Date.AddMonths(-1)).ToString("dd/MMM/yyyy");
                txtToDate2.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtAsAt.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtExDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                pnlLocation.Enabled = !chkAllCompany.Checked;
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
                BindYear();
                //txtAgeFrom.Text = "0";
                // txtAgeTo.Text = "9999";
                dgvAdminTeam.Columns[1].Visible = false;
                AddEnableCntr();
                DesableAllControler();
                pnlLocation.Visible = true;
                pnlProfitCenter.Visible = false;

                Session["AdminTMList"] = "";
                Session["delAdminTMList"] = "";

            }
            else
            {

            }
        }

        private void _showHeirarchy(string _type)
        {
            if (_type == "LOC")
            {
                List<TextBox> _lTxt = new List<TextBox>();
                _lTxt.Add(ucLoactionSearch.txtCompany);
                _lTxt.Add(ucLoactionSearch.txtChanel);
                _lTxt.Add(ucLoactionSearch.txtSubChanel);
                _lTxt.Add(ucLoactionSearch.txtAreya);
                _lTxt.Add(ucLoactionSearch.txtRegion);
                _lTxt.Add(ucLoactionSearch.txtZone);
                _lTxt.Add(ucLoactionSearch.txtLocation);
                _lTxt.Add(ucLoactionSearch.txtSearch);
                _lTxt.Add(txtSearchbyword);

                List<LinkButton> _lBtn = new List<LinkButton>();
                _lBtn.Add(ucLoactionSearch.lbtnCompany);
                _lBtn.Add(ucLoactionSearch.lbtnChanel);
                _lBtn.Add(ucLoactionSearch.lbtnSubChanel);
                _lBtn.Add(ucLoactionSearch.lbtnAreya);
                _lBtn.Add(ucLoactionSearch.lbtnRegion);
                _lBtn.Add(ucLoactionSearch.lbtnZone);
                _lBtn.Add(ucLoactionSearch.lbtnLocation);
                _lBtn.Add(ucLoactionSearch.lbtnImgSearch);
                _lBtn.Add(lbtnSearch);
                _lBtn.Add(lbtnAddLocation);
                _lBtn.Add(btnLocation);
                _lBtn.Add(lbtnLocationAll);
                _lBtn.Add(lbtnLocationNone);
                _lBtn.Add(lbtnLocationClear);
                _lBtn.Add(btnClose);
                _enableLbtnList = _lBtn;
            }
            else
            {
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
                _lBtn.Add(btnClearGroup);
                _enableLbtnList = _lBtn;
            }
        }

        private void AddEnableCntr()
        {
            List<RadioButton> _rList = new List<RadioButton>();
            _rList.Add(rad01);
            //_rList.Add(rad02);
            _rList.Add(rad03);
            _rList.Add(rad04);
            //_rList.Add(rad05);
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
            _rList.Add(rad21);
            _rList.Add(rad22);
            _rList.Add(rad23);
            _rList.Add(rad24);
            _rList.Add(rad20);
            _rList.Add(rad25);
            _rList.Add(rad26);
            _rList.Add(rad27);
            _rList.Add(rbpdf);
            _rList.Add(rbexel);
            _rList.Add(rbexeldata);
            _rList.Add(rbword);
            _enbleRadioList = _rList;

            List<CheckBox> _lChk = new List<CheckBox>();
            _lChk.Add(chkAllCompany);
            _lChk.Add(chkAllAdmin);

            List<TextBox> _lTxt = new List<TextBox>();


            _enbleTxtList = _lTxt;

            List<LinkButton> _lBtn = new List<LinkButton>();
            _lBtn.Add(lbtnView);
            _lBtn.Add(lbtnClear);
            _lBtn.Add(lbtnSearch);
            _lBtn.Add(btnDownloadfile);

            _lBtn.Add(lbtnSearch);
            _lBtn.Add(lbtnAddLocation);
            _lBtn.Add(btnLocation);
            _lBtn.Add(lbtnLocationAll);
            _lBtn.Add(lbtnLocationNone);
            _lBtn.Add(lbtnLocationClear);
            _lBtn.Add(btnClose);
            _enableLbtnList = _lBtn;

            List<DropDownList> _lDdl = new List<DropDownList>();
            _lDdl.Add(ucLoactionSearch.cmbSearchby);
            _lDdl.Add(cmbSearchbykey);
            _lDdl.Add(ddlMonth);
            _lDdl.Add(ddlYear);
            _enbleDdlList = _lDdl;

            _lTxt = new List<TextBox>();
            _lTxt.Add(ucProfitCenterSearch.TxtCompany);
            _lTxt.Add(ucProfitCenterSearch.TxtChanel);
            _lTxt.Add(ucProfitCenterSearch.TxtSubChanel);
            _lTxt.Add(ucProfitCenterSearch.TxtAreya);
            _lTxt.Add(ucProfitCenterSearch.TxtRegion);
            _lTxt.Add(ucProfitCenterSearch.TxtZone);
            _lTxt.Add(ucProfitCenterSearch.TxtProfCenter);
            _lTxt.Add(ucProfitCenterSearch.txtSearch);
            _lTxt.Add(txtSearchbyword);
            _lTxt.Add(ucLoactionSearch.txtCompany);
            _lTxt.Add(ucLoactionSearch.txtChanel);
            _lTxt.Add(ucLoactionSearch.txtSubChanel);
            _lTxt.Add(ucLoactionSearch.txtAreya);
            _lTxt.Add(ucLoactionSearch.txtRegion);
            _lTxt.Add(ucLoactionSearch.txtZone);
            _lTxt.Add(ucLoactionSearch.txtLocation);
            _lTxt.Add(ucLoactionSearch.txtSearch);

            _enbleTxtList = _lTxt;

            _lBtn = new List<LinkButton>();
            _lBtn.Add(lbtnView);
            _lBtn.Add(lbtnClear);
            _lBtn.Add(lbtnSearch);
            _lBtn.Add(btnDownloadfile);

            _lBtn.Add(ucProfitCenterSearch.lbtnCompany);
            _lBtn.Add(ucProfitCenterSearch.lbtnChanel);
            _lBtn.Add(ucProfitCenterSearch.lbtnSubChanel);
            _lBtn.Add(ucProfitCenterSearch.lbtnAreya);
            _lBtn.Add(ucProfitCenterSearch.lbtnRegion);
            _lBtn.Add(ucProfitCenterSearch.lbtnZone);
            _lBtn.Add(ucProfitCenterSearch.lbtnLocation);
            _lBtn.Add(ucProfitCenterSearch.lbtnImgSearch);
            _lBtn.Add(ucLoactionSearch.lbtnCompany);
            _lBtn.Add(ucLoactionSearch.lbtnChanel);
            _lBtn.Add(ucLoactionSearch.lbtnSubChanel);
            _lBtn.Add(ucLoactionSearch.lbtnAreya);
            _lBtn.Add(ucLoactionSearch.lbtnRegion);
            _lBtn.Add(ucLoactionSearch.lbtnZone);
            _lBtn.Add(ucLoactionSearch.lbtnLocation);
            _lBtn.Add(ucLoactionSearch.lbtnImgSearch);
            _lBtn.Add(lbtnSearch);
            _lBtn.Add(lbtnAddLocation);
            _lBtn.Add(btnLocation);
            _lBtn.Add(lbtnLocationAll);
            _lBtn.Add(lbtnLocationNone);
            _lBtn.Add(lbtnLocationClear);
            _lBtn.Add(btnClearGroup);
            _enableLbtnList = _lBtn;

            _lDdl = new List<DropDownList>();
            _lDdl.Add(ucProfitCenterSearch.cmbSearchby);
            _lDdl.Add(cmbSearchbykey);
            _enbleDdlList = _lDdl;

            foreach (GridViewRow grdRow in dgvCompany.Rows)
            {
                CheckBox chkCompanyCode = grdRow.FindControl("chkCompanyCode") as CheckBox;
                _lChk.Add(chkCompanyCode);
            }
            _enbleChkList = _lChk;
        }
        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
        }

        private void update_LOC_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), "ABL", null, null);

            foreach (GridViewRow Item in dgvLocation.Rows)
            {
                Label lblLocation = (Label)Item.FindControl("lblLocation");
                CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                string pc = lblLocation.Text;

                if (chkLocation.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, pc, null);

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
                BaseCls.GlbReportProfit = "";
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, ucLoactionSearch.ProfitCenter.ToUpper(), null);
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
                Label lblcm = (Label)Item.FindControl("lblCompany");
                if (chkLocation.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), lblcm.Text, pc, null);

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

        private DataTable GetTempPCLoc(string user)
        {

            return CHNLSVC.Sales.GetTempPCLoc(user);

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
                Session["GlbReportCompCode"] = com_cds;
                MasterCompany _newCom = new MasterCompany();
                _newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                com_desc = _newCom.Mc_desc;
                com_add = _newCom.Mc_add1 + " " + _newCom.Mc_add2;

                //BaseCls.GlbReportCompName = com_desc;
                //BaseCls.GlbReportCompanies = com_cds;

                ////set all common parameters
                //BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                //BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                //BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                ////BaseCls.GlbReportSupplier = txtSupplier.Text.ToString();

                //BaseCls.GlbReportCompCode = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportComp = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportProfit = Session["UserDefLoca"].ToString();
                Session["GlbReportType"] = "";

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
                Session["GlbReportDoc2"] = "";
                Session["GlbReportJobStatus"] = "";
                Session["GlbReportWithRCC"] = "";
                Session["GlbReportWithCost"] = 0;
                Session["GlbReportDocType"] = txtDocType.Text;
                //Session["GlbReportDocSubType"] = txtDocSubType.Text;

                Session["GlbReportFromDate"] = Convert.ToDateTime(txtFromDate.Text).Date;
                Session["GlbReportToDate"] = Convert.ToDateTime(txtToDate.Text).Date;
                Session["GlbReportAsAtDate"] = Convert.ToDateTime(txtAsAt.Text).Date;
                Session["GlbReportSupplier"] = txtSupplier.Text.ToString();

                //Session["GlbReportCompCode"] = Session["UserCompanyCode"].ToString();
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
                //_invRepPara._GlbReportDocSubType = Session["GlbReportDocSubType"].ToString();
                _invRepPara._GlbReportCompName = Session["GlbReportCompName"].ToString();
                _invRepPara._GlbReportCompanies = Session["GlbReportCompanies"].ToString();

                _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                _invRepPara._GlbReportFromDate2 = Convert.ToDateTime(txtFromDate2.Text).Date;
                _invRepPara._GlbReportToDate2 = Convert.ToDateTime(txtToDate2.Text).Date;
                _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString();
                _invRepPara._GlbReportExecutive = txtExecutive.Text.ToString();
                _invRepPara._GlbReportCustomer = txtCustomer.Text.ToString();
                _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportProfit = ucProfitCenterSearch.TxtProfCenter.Text == "" ? "All Profit centers based on user rights" : ucProfitCenterSearch.TxtProfCenter.Text;
                _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
                _invRepPara._GlbUserID = Session["UserID"].ToString();
                _invRepPara._GlbReportWithJob = "N";
                _invRepPara._GlbReportCheckRegDate = 0;
                if (Session["GlbReportCompCode"] != null)
                {
                    _invRepPara._GlbReportCompCode = Session["GlbReportCompCode"].ToString();
                }

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
                update_LOC_List_RPTDB();

                string _export = "N";
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
                if (rbword.Checked)
                {
                    targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".doc";
                }
                //if (BaseCls.GlbReportDirection == "IN") BaseCls.GlbReportDirection = "1";
                //if (BaseCls.GlbReportDirection == "OUT") BaseCls.GlbReportDirection = "0";                

                if (rad01.Checked == true)
                {
                    bool withReversal = false;
                    if (chkWithReversal.Checked)
                    {
                        withReversal = true;
                    }

                    foreach (ListItem Item in listGroup.Items)
                    {
                        if (Item.Text == "BRND" && listGroup.Items.Count > 1)
                        {
                            lbtnView.Enabled = true;
                            displayMessage("Other grouping options with Brand grouping not allowed ");
                            return;
                        }

                    }

                    _export = "Y";
                    _opt = "rad01";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    string _err = "";
                    string _cat = "INV";
                    Int16 _intercom = Convert.ToInt16(rbwithicom.Checked == true ? 1 : rbwithoutintercompny.Checked == true ? 2 : rbboth.Checked == true ? 0 : 0);
                    _invRepPara._GlbReportIsReplItem = 0;

                    

                    //_invRepPara._GlbReportIsFreeIssue=

                    foreach (ListItem Item in listGroup.Items)
                    {
                        _cat = Item.Text;
                        goto a;
                    }
                a:

                    string _filePath = CHNLSVC.MsgPortal.GetItemWiseGpExcel(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                        _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                        _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                        _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, withReversal, _invRepPara._GlbReportIsReplItem, _invRepPara._GlbReportFromDate2, _invRepPara._GlbReportToDate2, _intercom, out _err);

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

                //if (rad02.Checked == true)
                //{
                //    _opt = "rad02";
                //    _isSelected = true;

                //    string _cat = "ITM";

                //    foreach (ListItem Item in listGroup.Items)
                //    {
                //        _cat = Item.Text;
                //        goto a;
                //    }
                //a:
                //    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                //    BaseCls.GlbReportModel = txtModel.Text;
                //    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                //    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtCat1.Text == "" ? txtCat1.Text : "^" + txtCat1.Text + "$" : vItemCat1;
                //    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtCat2.Text == "" ? txtCat2.Text : "^" + txtCat2.Text + "$" : vItemCat2;
                //    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtCat3.Text == "" ? txtCat3.Text : "^" + txtCat3.Text + "$" : vItemCat3;
                //    BaseCls.GlbReportItemCat4 = vItemCat4 == "" ? txtCat4.Text == "" ? txtCat4.Text : "^" + txtCat4.Text + "$" : vItemCat4;
                //    BaseCls.GlbReportItemCat5 = vItemCat5 == "" ? txtCat5.Text == "" ? txtCat5.Text : "^" + txtCat5.Text + "$" : vItemCat5;

                //    DateTime in_fdate = BaseCls.GlbReportFromDate;
                //    DateTime in_tdate = BaseCls.GlbReportToDate;
                //    string in_clasif = "";
                //    string in_itemcode = BaseCls.GlbReportItemCode;
                //    string in_brand = BaseCls.GlbReportBrand;
                //    string in_model = BaseCls.GlbReportModel;
                //    string in_itemcat1 = BaseCls.GlbReportItemCat1;
                //    string in_itemcat2 = BaseCls.GlbReportItemCat2;
                //    string in_itemcat3 = BaseCls.GlbReportItemCat3;
                //    string in_itemcat4 = BaseCls.GlbReportItemCat4;
                //    string in_itemcat5 = BaseCls.GlbReportItemCat5;
                //    string in_stktype = "";
                //    string in_com = Session["UserCompanyCode"].ToString();
                //    string in_pc = "";
                //    string in_user = Session["UserID"].ToString();
                //    string _err = "";
                //    if (chkExportExcel.Checked)
                //    {
                //        string _filePath = CHNLSVC.MsgPortal.GetSalesValuation(in_fdate, in_tdate, in_clasif, in_itemcode, in_brand, in_model,
                //        in_itemcat1, in_itemcat2, in_itemcat3, in_itemcat4, in_itemcat5, in_stktype, in_com, in_pc, in_user,_cat, out _err);
                //        if (!string.IsNullOrEmpty(_err))
                //        {
                //            lbtnView.Enabled = true;
                //            displayMessage(_err);
                //            return;
                //        }
                //        if (string.IsNullOrEmpty(_filePath))
                //        {
                //            displayMessage("The excel file path cannot identify. Please contact IT Dept");
                //            return;
                //        }
                //        _copytoLocal(_filePath);
                //        displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                //        Process p = new Process();
                //        p.StartInfo = new ProcessStartInfo(_filePath);
                //        p.Start();
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select export excel !')", true);
                //    }
                //}


                if (rad03.Checked == true)
                {
                    _opt = "rad03";
                    _isSelected = true;

                    string _err = "";
                    _invRepPara._GlbReportType = "N";
                    _invRepPara._GlbReportDoc2 = "All";
                    _invRepPara._GlbReportToAge = 0;
                    _invRepPara._GlbReportOtherLoc = txtotherloc.Text;
                    _invRepPara._GlbReportCustomer = txtCustomer.Text;
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportHeading = "Pending Delivery Report";
                    Session["InvReportPara"] = _invRepPara;

                    if (_invRepPara._GlbReportType == "N")
                    {
                        if (_invRepPara._GlbReportWithCost == 1)
                            Session["GlbReportName"] = "Forward_Sales_Report_cost.rpt";
                        else
                            Session["GlbReportName"] = "Forward_Sales_Report1.rpt";
                    }
                    else
                    {
                        Session["GlbReportName"] = "Forward_Sales_Report2.rpt";
                    }

                    obj.PendingDeliveryReport(_invRepPara);
                    if (_invRepPara._GlbReportType == "N")
                    {
                        if (_invRepPara._GlbReportWithCost == 1)
                            PrintPDF(targetFileName, obj._Collectiondetail);
                        else
                            PrintPDF(targetFileName, obj._ForwardSalesrpt1);
                    }
                    else
                    {
                        PrintPDF(targetFileName, obj._ForwardSalesrpt2);
                    }

                    //Session["GlbReportName"] = "rpt_PendingDeliveryReportNew.rpt";
                    //Response.Redirect("~/View/Reports/Sales/SalesReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Sales/SalesReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }

                if (rad04.Checked == true)
                {
                    _opt = "rad04";
                    _isSelected = true;

                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtCat1.Text == "" ? txtCat1.Text : "^" + txtCat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtCat2.Text == "" ? txtCat2.Text : "^" + txtCat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtCat3.Text == "" ? txtCat3.Text : "^" + txtCat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportItemCat4 = vItemCat4 == "" ? txtCat4.Text == "" ? txtCat4.Text : "^" + txtCat4.Text + "$" : vItemCat4;
                    BaseCls.GlbReportItemCat5 = vItemCat5 == "" ? txtCat5.Text == "" ? txtCat5.Text : "^" + txtCat5.Text + "$" : vItemCat5;

                    DateTime in_fdate = BaseCls.GlbReportFromDate;
                    DateTime in_tdate = BaseCls.GlbReportToDate;
                    string in_clasif = "";
                    string in_itemcode = BaseCls.GlbReportItemCode;
                    string in_brand = BaseCls.GlbReportBrand;
                    string in_model = BaseCls.GlbReportModel;
                    string in_itemcat1 = BaseCls.GlbReportItemCat1;
                    string in_itemcat2 = BaseCls.GlbReportItemCat2;
                    string in_itemcat3 = BaseCls.GlbReportItemCat3;
                    string in_itemcat4 = BaseCls.GlbReportItemCat4;
                    string in_itemcat5 = BaseCls.GlbReportItemCat5;
                    string in_stktype = "";
                    string in_com = Session["UserCompanyCode"].ToString();
                    string in_pc = "";
                    string in_user = Session["UserID"].ToString();
                    string _err = "";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    // update_PC_List_RPTDB();
                    BaseCls.GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    BaseCls.GlbReportHeading = "Purchase Order Summery - Summery Report";
                    BaseCls.GlbReportComp = Session["UserCompanyCode"].ToString();
                    Session["GlbReportName"] = "rpt_PendingSalesReportNew.rpt";
                    string url = "<script>window.open('~/View/Reports/Sales/SalesReportViewer.aspx','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    //Response.Redirect("~/View/Reports/Sales/SalesReportViewer.aspx", false);
                }

                //if (rad05.Checked == true)
                //{
                //    _isSelected = true;
                //    Session["GlbReportType"] = "";
                //    //update_PC_List_RPTDB();
                //    //BaseCls.GlbReportDoc = txtDocNo.Text;
                //    BaseCls.GlbReportCompCode = Session["UserCompanyCode"].ToString();
                //    BaseCls.GlbReportHeading = "Sales Order Status Report";
                //    BaseCls.GlbReportComp = Session["UserCompanyCode"].ToString();
                //    Session["GlbReportName"] = "rptSaleOrderstatusReport.rpt";
                //    //invRepPara._GlbReportName = "DeliveredSalesReport.rpt";
                //    Session["GlbReportName"] = "rptSaleOrderstatusReport.rpt";
                //    string url = "<script>window.open('~/View/Reports/Sales/SalesReportViewer.aspx','_blank');</script>";
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                //    //Response.Redirect("~/View/Reports/Sales/SalesReportViewer.aspx", false);

                //}

                if (rad06.Checked == true) //Sales Order Details Report - Sanjeewa 2016-06-16
                {
                    _export = "Y";
                    _opt = "rad06";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportOtherLoc = txtotherloc.Text;

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.getSalesOrderDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportOtherLoc, _invRepPara._GlbReportExecutive, _invRepPara._GlbUserID, _invRepPara._GlbReportCustomer, out _err);

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
                //Dilshan on 16/10/2017
                if (rad24.Checked == true) //Sales Order Details Report - Sanjeewa 2016-06-16
                {
                    _export = "Y";
                    _opt = "rad24";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportOtherLoc = txtotherloc.Text;

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.getSalesOrderSummery(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportOtherLoc, _invRepPara._GlbReportExecutive, _invRepPara._GlbUserID, _invRepPara._GlbReportCustomer, out _err);

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

                //Tharindu on 10/03/2018
                if (rad26.Checked == true) //Sales Order Details Report New- 
                {
                    _export = "Y";
                    _opt = "rad26";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportOtherLoc = txtotherloc.Text;

                    string _err = "";
                
                    //Tharindu
                    DataTable results = CHNLSVC.MsgPortal.getSalesOrderSummaryDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportOtherLoc, _invRepPara._GlbReportExecutive, _invRepPara._GlbUserID, _invRepPara._GlbReportCustomer, out _err);

                    _invRepPara._GlbReportHeading = "Sales_Order_Summary ";
                    Session["InvReportPara"] = _invRepPara;

                    _invRepPara._GlbReportName = "Sales_Order_Summary.rpt";
                    Session["GlbReportName"] = "Sales_Order_Summary.rpt";

                    obj.GetSalesOrderDetails(_invRepPara);
                    PrintPDF(targetFileName, obj._sosumAAL);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }

                if (rad07.Checked == true) // Bond Balance 2016-05-28 Sanjeewa
                {
                    _export = "Y";
                    _opt = "rad07";
                    _isSelected = true;

                    string _err = "";

                    //string _filePath = CHNLSVC.MsgPortal.getBondBalanceDetails(_invRepPara._GlbReportAsAtDate, _invRepPara._GlbReportItemCode,
                    //    _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                    //    _invRepPara._GlbReportItemStatus,_invRepPara._GlbReportCompCode,_invRepPara._GlbUserID, out _err);

                    string _filePath = CHNLSVC.MsgPortal.getBondBalanceDetails1(_invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, out _err);

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

                if (rad08.Checked == true) // Valuation Report 2016-06-05 Sanjeewa
                {
                    _opt = "rad08";
                    _isSelected = true;

                    string _err = "";
                    string _cat = "ITM";
                    string _type = "BOTH";

                    _type = radwith.Checked ? "QTY" : radwithout.Checked ? "VAL" : "BOTH";

                    foreach (ListItem Item in listGroup.Items)
                    {
                        _cat = Item.Text;
                        goto a;
                    }
                a:
                    if (chkExportExcel.Checked == true || (chkExportExcel.Checked == false && _cat != "ITM"))
                    {
                        _export = "Y";
                        string _filePath = CHNLSVC.MsgPortal.getValuationDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportItmClasif,
                            _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                            _invRepPara._GlbReportItemStatus, _cat, _type, _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, out _err);

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
                    else
                    {
                        _type = "BOTH";

                        _invRepPara._GlbReportHeading = "Item wise Valuation Report";
                        Session["InvReportPara"] = _invRepPara;

                        obj.getValuationDetails(_invRepPara);
                        PrintPDF(targetFileName, obj._valdtl);
                    }

                }

                if (rad09.Checked == true) // Sale Details 2016-09-20
                {
                    _export = "Y";
                    _opt = "rad09";
                    _isSelected = true;

                    string _err = "";
                    //    string _cat = "ITM";
                    //    string _type = "BOTH";

                    //    _type = radwith.Checked ? "QTY" : radwithout.Checked ? "VAL" : "BOTH";

                    //    foreach (ListItem Item in listGroup.Items)
                    //    {
                    //        _cat = Item.Text;
                    //        goto a;
                    //    }
                    //a:
                    string _filePath = CHNLSVC.MsgPortal.GetSaleDetailsInvoiceMain(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, out _err);

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

                if (rad10.Checked == true) // GP Reconcilation 2016-09-23
                {
                    bool withReversal = false;
                    if (chkWithReversal.Checked)
                    {
                        withReversal = true;
                    }

                    _export = "Y";
                    _opt = "rad10";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    string _err = "";
                    string _cat = "INV";
                    Int16 _intercom = Convert.ToInt16(rbwithicom.Checked == true ? 1 : rbwithoutintercompny.Checked == true ? 2 : rbboth.Checked == true ? 0 : 0);
                    _invRepPara._GlbReportIsReplItem = 0;
                    string _fromAdmin = txtoperteamfrom.Text.ToString();
                    string _toAdmin = txtoperteamto.Text.ToString();

                    //_invRepPara._GlbReportIsFreeIssue=

                    foreach (ListItem Item in listGroup.Items)
                    {
                        _cat = Item.Text;
                        goto a;
                    }
                a:

                    string _filePath = CHNLSVC.MsgPortal.GetItemWiseGpExcelRecon(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                        _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                        _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                        _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, withReversal, _invRepPara._GlbReportIsReplItem, _invRepPara._GlbReportFromDate2, _invRepPara._GlbReportToDate2, _intercom, _fromAdmin, _toAdmin, out _err);

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


                //    DataTable dtReportData = new DataTable();
                //    _export = "Y";
                //    _opt = "rad10";
                //    _isSelected = true;
                //    update_PC_List_RPTDB();

                //    string _err = "";
                //    string _cat = "INV";

                //    foreach (ListItem Item in listGroup.Items)
                //    {
                //        _cat = Item.Text;
                //        goto a;
                //    }
                //a:

                //    List<DataTable> dtDpDetails = new List<DataTable>();

                //    dtDpDetails = CHNLSVC.MsgPortal.GetItemWiseGpExcel2(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                //        _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                //        _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                //        _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, Session["AdminTMList"].ToString(), Session["delAdminTMList"].ToString());



                //    foreach (var item in dtDpDetails)
                //    {
                //        dtReportData.Merge(item);
                //    }
                //    dtReportData.TableName = "tblReport";

                //    string _filePath = CHNLSVC.MsgPortal.GetGP_ReconciExcelReport(dtReportData, _invRepPara._GlbReportCompCode, "CC", _invRepPara._GlbUserID, out _err);


                //    if (string.IsNullOrEmpty(_filePath))
                //    {
                //        displayMessage("The excel file path cannot identify. Please contact IT Dept");
                //        return;
                //    }

                //    if (!string.IsNullOrEmpty(_err))
                //    {
                //        lbtnView.Enabled = true;
                //        displayMessage(_err);
                //        return;
                //    }

                //    _copytoLocal(_filePath);
                //    displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                //    Process p = new Process();
                //    p.StartInfo = new ProcessStartInfo(_filePath);
                //    p.Start();

                //}


                if (rad11.Checked == true) // Stock Adjestment 2016-09-26
                {
                    _export = "Y";
                    _opt = "rad11";
                    _isSelected = true;

                    string _err = "";

                    update_Location_List_RPTDB();

                    string _filePath = CHNLSVC.MsgPortal.GetStockAdjustmentSummary(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, Session["AdminTMList"].ToString(), _invRepPara._GlbUserID, out _err);

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

                if (rad12.Checked == true) // GIT Reconcilation AS AT 2016-09-28
                {
                    _opt = "rad12";

                    _export = "Y";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportOtherLoc = txtotherloc.Text;
                    //update_PC_List_RPTDB();
                    string _err = "";

                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportHeading = "GIT Report";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportType = "ASAT";
                    Session["GlbReportName"] = "rpt_GLB_Git_Document.rpt";
                    _invRepPara._GlbReportName = "rpt_GLB_Git_Document.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    string _filePath = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.getGITReport_ASAT_Recon(_invRepPara._GlbReportAsAtDate.Date, _invRepPara._GlbReportComp, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5, _invRepPara._GlbUserID, _invRepPara._GlbReportOtherLoc, out _err);
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

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();



                    //_isSelected = true;
                    //Session["GlbReportType"] = "";
                    //_invRepPara._GlbReportOtherLoc = txtotherloc.Text;//not used
                    //update_Location_List_RPTDB();
                    //string _err;

                    //_invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    //_invRepPara._GlbReportHeading = "GIT Reconcilation Report";
                    //_invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    //_invRepPara._GlbReportType = "ASAT";

                    //string _filePath = string.Empty;
                    //string _error = string.Empty;

                    //_filePath = CHNLSVC.MsgPortal.GetGIT_ReconcilationByGIT_AsAt(_invRepPara._GlbReportAsAtDate.Date, _invRepPara._GlbReportComp, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5, _invRepPara._GlbUserID, _invRepPara._GlbReportOtherLoc, out _err);
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

                    //Process p = new Process();
                    //p.StartInfo = new ProcessStartInfo(_filePath);
                    //p.Start();

                    //return;

                }

                if (rad13.Checked == true) // AOD Reconcilation by GIT AS AT 2016-09-30
                {
                    _opt = "rad13";

                    _export = "Y";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportOtherLoc = txtotherloc.Text;//not used
                    update_Location_List_RPTDB();
                    string _err;

                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportHeading = "AOD Reconcilation Report";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbUserID = Session["UserID"].ToString();
                    _invRepPara._GlbReportChannel = ucLoactionSearch.Channel;

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.GetAOD_ReconcilationDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportComp, "", Session["AdminTMList"].ToString(), _invRepPara._GlbReportChannel, _invRepPara._GlbUserID, out _err);
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

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    return;

                }

                if (rad14.Checked == true) // Sale Detail Report Category Wise 2016-10-03
                {
                    _export = "Y";
                    _opt = "rad14";
                    _isSelected = true;
                    string groupCat = "";
                    update_PC_List_RPTDB();

                    foreach (ListItem list in listGroup.Items)
                    {
                        if (list.Text == "CAT1")
                        {
                            groupCat = "mst_itm.mi_cate_1";
                        }
                        if (list.Text == "CAT2")
                        {
                            if (groupCat.Length > 0)
                                groupCat = groupCat + "," + "mst_itm.mi_cate_2";
                            else
                                groupCat = "mst_itm.mi_cate_2";
                        }
                        if (list.Text == "CAT3")
                        {
                            if (groupCat.Length > 0)
                                groupCat = groupCat + "," + "mst_itm.mi_cate_3";
                            else
                                groupCat = "mst_itm.mi_cate_3";
                        }
                        if (list.Text == "CAT4")
                        {
                            if (groupCat.Length > 0)
                                groupCat = groupCat + "," + "mst_itm.mi_cate_4";
                            else
                                groupCat = "mst_itm.mi_cate_4";
                        }
                        if (list.Text == "CAT5")
                        {
                            if (groupCat.Length > 0)
                                groupCat = groupCat + "," + "mst_itm.mi_cate_5";
                            else
                                groupCat = "mst_itm.mi_cate_5";
                        }
                    }

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.GetSaleRepDetailsCatWise(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, groupCat, out _err);

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

                if (rad15.Checked)
                {
                    try
                    {
                        DataTable GLOB_DataTable = new DataTable();
                        _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
                        _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text.ToString());
                        _invRepPara._GlbReportItemCode = txtItemCode.Text.ToString();
                        _invRepPara._GlbReportItemCat1 = txtCat1.Text.ToString();
                        _invRepPara._GlbReportItemCat2 = txtCat2.Text.ToString();
                        _invRepPara._GlbReportItemCat3 = txtCat3.Text.ToString();
                        _invRepPara._GlbUserID = Session["UserID"].ToString();

                        clsInventory invObj = new clsInventory();

                        update_PC_List_RPTDB();
                        DataTable dtPC = GetTempPCLoc(_invRepPara._GlbUserID);
                        string status = "Pending";
                        foreach (DataRow drow in dtPC.Rows)
                        {
                            DataTable dtRcc = new DataTable();
                            dtRcc = CHNLSVC.Financial.Print_RCC_Listing_Report(_invRepPara._GlbReportCompCode, drow["MPC_CD"].ToString(), Convert.ToDateTime(_invRepPara._GlbReportFromDate).Date, Convert.ToDateTime(_invRepPara._GlbReportToDate).Date, _invRepPara._GlbUserID, BaseCls.GlbRccType, BaseCls.GlbRccAgent, BaseCls.GlbRccColMethod, BaseCls.GlbRccCloseTp, status, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCode);
                            GLOB_DataTable.Merge(dtRcc);

                        }

                        invObj.RCC_Report_Data(GLOB_DataTable, _invRepPara);
                        PrintPDF(targetFileName, invObj._rccReport);

                    }
                    catch (Exception ex)
                    {
                        displayMessage(ex.Message);
                    }



                }

                if (rad16.Checked == true) // Age of Revert Reprot 2016/10/07
                {
                    _export = "Y";
                    _opt = "rad16";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
                    _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text.ToString());
                    _invRepPara._GlbReportBrand = txtBrand.Text;
                    _invRepPara._GlbReportItemCat1 = txtCat1.Text.ToString();
                    _invRepPara._GlbReportItemCat2 = txtCat2.Text.ToString();
                    _invRepPara._GlbReportItemCode = txtItemCode.Text;
                    _invRepPara._GlbUserID = Session["UserID"].ToString();

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.GetAgeOfRevertReportDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCode, _invRepPara._GlbUserID, out _err);

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

                if (rad17.Checked == true)
                {

                    bool withReversal = false;
                    if (chkWithReversal.Checked)
                    {
                        withReversal = true;
                    }

                    _export = "N";
                    _opt = "rad17";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    string _err = "";
                    string _cat = "INV";

                    foreach (ListItem Item in listGroup.Items)
                    {
                        _cat = Item.Text;
                        goto a;
                    }
                a:
                    clsSales saleobj = new clsSales();

                    List<DataTable> dtResults = CHNLSVC.MsgPortal.GetGP_AnalysisDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                        _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                        _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                        _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, withReversal, out _err);

                    saleobj.GP_AnalysisData(dtResults, _invRepPara);
                    PrintPDF(targetFileName, saleobj._gpAnalysisRpt);

                }

                if (rad18.Checked == true)
                {
                    bool withReversal = false;
                    if (chkWithReversal.Checked)
                    {
                        withReversal = true;
                    }

                    _opt = "rad18";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    string _err = "";
                    string _cat = "INV";
                    Int16 _intercom = Convert.ToInt16(rbwithicom.Checked == true ? 1 : rbwithoutintercompny.Checked == true ? 2 : rbboth.Checked == true ? 0 : 0);
                    if (chkWithReplItems.Checked)
                    {
                        _invRepPara._GlbReportIsReplItem = 1;
                    }
                    else
                    {
                        _invRepPara._GlbReportIsReplItem = 0;
                    }

                    foreach (ListItem Item in listGroup.Items)
                    {
                        _cat = Item.Text;
                        goto a;
                    }
                a:

                    if (chkExportExcel.Checked == true)
                    {
                        _export = "Y";
                        string _filePath = CHNLSVC.MsgPortal.GetItemWiseGpExcel(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                            _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                            _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                            _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, withReversal,
                            _invRepPara._GlbReportIsReplItem, _invRepPara._GlbReportFromDate2, _invRepPara._GlbReportToDate2, _intercom, out _err);

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
                    else
                    {
                        _export = "N";

                        _invRepPara._GlbReportName = "GP_Detail_Repl.rpt";
                        Session["GlbReportName"] = "GP_Detail_Repl.rpt";

                        if (chkWithReplItems.Checked)
                        {
                            _invRepPara._GlbReportHeading = "GP DETAIL WITH REPLACEMENT ITEMS";
                        }
                        else
                        {
                            _invRepPara._GlbReportHeading = "GP DETAIL REPORT";
                        }
                        Session["InvReportPara"] = _invRepPara;

                        obj.GpDetailwithReplacementReport(_invRepPara);
                        PrintPDF(targetFileName, obj._gpdtlrepl);
                    }

                }

                //-----------------------------------------
                if (rad19.Checked == true)
                {
                    bool withReversal = false;
                    if (chkWithReversal.Checked)
                    {
                        withReversal = true;
                    }

                    _export = "Y";
                    _opt = "rad19";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    string _err = "";
                    string _cat = "CAT1";
                    Int16 _intercom = Convert.ToInt16(rbwithicom.Checked == true ? 1 : rbwithoutintercompny.Checked == true ? 2 : rbboth.Checked == true ? 0 : 0);
                    //Int16 _intercom = 0;
                    _invRepPara._GlbReportIsReplItem = 0;

                    //_invRepPara._GlbReportIsFreeIssue=

                    foreach (ListItem Item in listGroup.Items)
                    {
                        _cat = Item.Text;
                        goto a;
                    }
                a:

                    //string _filePath = CHNLSVC.MsgPortal.GetFitchExcel(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                    //    _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                    //    _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                    //    _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, withReversal, _invRepPara._GlbReportIsReplItem, _invRepPara._GlbReportFromDate2, _invRepPara._GlbReportToDate2, _intercom, out _err);

                    string _filePath = CHNLSVC.MsgPortal.GetFitch_CatBrndExcel(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                       _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                       _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                       _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, withReversal, _invRepPara._GlbReportIsReplItem, _invRepPara._GlbReportFromDate2, _invRepPara._GlbReportToDate2, _intercom, out _err);


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

                //-----------------------------------------
                if (rad21.Checked == true)
                {
                    bool withReversal = false;
                    if (chkWithReversal.Checked)
                    {
                        withReversal = true;
                    }

                    _export = "Y";
                    _opt = "rad21";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    string _err = "";
                    string _cat = "CAT1";
                    Int16 _intercom = Convert.ToInt16(rbwithicom.Checked == true ? 1 : rbwithoutintercompny.Checked == true ? 2 : rbboth.Checked == true ? 0 : 0);
                    _invRepPara._GlbReportIsReplItem = 0;

                    //_invRepPara._GlbReportIsFreeIssue=

                    foreach (ListItem Item in listGroup.Items)
                    {
                        _cat = Item.Text;
                        goto a;
                    }
                a:

                    //string _filePath = CHNLSVC.MsgPortal.GetFitchExcel(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                    //    _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                    //    _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                    //    _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, withReversal, _invRepPara._GlbReportIsReplItem, _invRepPara._GlbReportFromDate2, _invRepPara._GlbReportToDate2, _intercom, out _err);

                    string _filePath = CHNLSVC.MsgPortal.GetFitchExcel(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCustomer, _invRepPara._GlbReportExecutive, _invRepPara._GlbReportDocType, _invRepPara._GlbReportItemCode,
                       _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                       _invRepPara._GlbUserID, _invRepPara._GlbReportType, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportDoc, _invRepPara._GlbReportCompCode,
                       _invRepPara._GlbReportPromotor, _invRepPara._GlbReportIsFreeIssue, _invRepPara._GlbReportItmClasif, _invRepPara._GlbReportBrandMgr, _cat, withReversal, _invRepPara._GlbReportIsReplItem, _invRepPara._GlbReportFromDate2, _invRepPara._GlbReportToDate2, _intercom, out _err);


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
                if (rad20.Checked == true)
                {

                    _export = "Y";
                    _opt = "rad19";
                    _isSelected = true;
                    update_PC_List_RPTDB();
                    string comadd = "";
                    //get Admin teams
                    foreach (GridViewRow Item in dgvAdminTeam.Rows)
                    {
                        Label lblAdminCode = (Label)Item.FindControl("lblAdminCode");
                        CheckBox chkAdminTeam = (CheckBox)Item.FindControl("chkAdminTeam");
                        Label chkCom = (Label)Item.FindControl("lblAdminCome");



                        if (chkAdminTeam.Checked == true)
                        {
                            comadd = comadd + chkCom.Text + "-" + lblAdminCode.Text + ",";
                        }
                    }




                    string _err = "";
                    string _filePath = CHNLSVC.MsgPortal.PSIReport(_invRepPara._GlbUserID, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, txtModel.Text, txtBrand.Text, txtCat1.Text, txtCat2.Text, txtCat3.Text, 0, txtBrandMan.Text, out _err, comadd);

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

                if (rad22.Checked == true)  // sales reconcilation - kapila 20/3/2017
                {

                    //if (rbRepPDF.Checked == true)
                    //{
                        _opt = "rad22";
                        _isSelected = true;

                        _invRepPara._GlbReportName = "DeliveredSalesReport.rpt";
                        Session["GlbReportName"] = "DeliveredSalesReport.rpt";

                        _invRepPara._GlbReportHeading = "SALES RECONCILATION";
                        Session["InvReportPara"] = _invRepPara;
                        //    Response.Redirect("SalesReportViewer.aspx", false);

                        obj.SalesReconcilationReport(_invRepPara);
                        PrintPDF(targetFileName, obj._salesRecon);
                    //}
                    //else if (rbRepExcel.Checked == true)
                    //{
                    //    string rptType = "INVOICE";
                    //    if (rbInvWise.Checked == true)
                    //        rptType = "INVOICE";
                    //    else if (rbItemWise.Checked == true)
                    //        rptType = "ITEM";

                    //    // Currency Type 1 - Original Invoice Currency | 2 - Company Based Currency
                    //    int currencyType = 1;
                    //    if (rbInvoiceCurrency.Checked)
                    //        currencyType = 1; // Invoice Currency
                    //    else if (rbCompanyCurrency.Checked)
                    //        currencyType = 2; //Company Based Currency

                    //    _opt = "rad01";
                    //    _isSelected = true;
                    //    _export = "Y";

                    //    update_PC_List_RPTDB();

                    //    string _err = "";

                    //    string _filePath = CHNLSVC.MsgPortal.GetDeliveredSalesDetailsExcel1_old(Convert.ToDateTime(_invRepPara._GlbReportFromDate).Date,
                    //    Convert.ToDateTime(_invRepPara._GlbReportToDate).Date,
                    //    _invRepPara._GlbReportCustomer,
                    //    _invRepPara._GlbReportExecutive,
                    //    _invRepPara._GlbReportDocType,
                    //    _invRepPara._GlbReportItemCode,
                    //    _invRepPara._GlbReportBrand,
                    //    _invRepPara._GlbReportModel,
                    //    _invRepPara._GlbReportItemCat1,
                    //    _invRepPara._GlbReportItemCat2,
                    //    _invRepPara._GlbReportItemCat3,
                    //    _invRepPara._GlbUserID,
                    //    _invRepPara._GlbReportType,
                    //    _invRepPara._GlbReportItemStatus,
                    //    _invRepPara._GlbReportDoc,
                    //    _invRepPara._GlbReportCompCode,
                    //    _invRepPara._GlbReportPromotor,
                    //    _invRepPara._GlbReportIsFreeIssue,
                    //    rptType,
                    //    currencyType,
                    //    revOrSaleOrAll,
                    //    out _err);

                    //    if (!string.IsNullOrEmpty(_err))
                    //    {
                    //        lbtnView.Enabled = true;
                    //        displayMessage(_err);
                    //        return;
                    //    }
                    //    if (string.IsNullOrEmpty(_filePath))
                    //    {
                    //        displayMessage("The excel file path cannot identify. Please contact IT Dept");
                    //        return;
                    //    }
                    //    _copytoLocal(_filePath);
                    //    displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                    //    Process p = new Process();
                    //    p.StartInfo = new ProcessStartInfo(_filePath);
                    //    p.Start();

                    //}

                }
                if (rad23.Checked == true) // Valuation Report insurance - kapila 30/5/2017
                {
                    _opt = "rad23";
                    _isSelected = true;

                    string _err = "";
                    string _cat = "ITM";
                    string _type = "BOTH";

                    _type = radwith.Checked ? "QTY" : radwithout.Checked ? "VAL" : "BOTH";

                    foreach (ListItem Item in listGroup.Items)
                    {
                        _cat = Item.Text;
                        goto a;
                    }
                a:
                    if (chkExportExcel.Checked == true || (chkExportExcel.Checked == false && _cat != "ITM"))
                    {
                        _export = "Y";
                        string _filePath =  CHNLSVC.MsgPortal.getValuationDetails_Insu(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportItmClasif,
                             _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                             _invRepPara._GlbReportItemStatus, _cat, _type, _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, out _err,0);

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
                    else
                    {
                        _type = "BOTH";

                        _invRepPara._GlbReportHeading = "Item wise Valuation Report";
                        Session["InvReportPara"] = _invRepPara;

                        obj.getValuationDetails(_invRepPara);

                        //kapila 24/6/2017
                        MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                        string _path = _MasterComp.Mc_anal6;
                        obj._valdtl.ExportToDisk(ExportFormatType.Excel, _path + "ItmWiseValuIns" + _invRepPara._GlbUserID + ".xls");

                        Excel.Application excelApp = new Excel.Application();
                        excelApp.Visible = true;
                        string workbookPath = _path + "ItmWiseValuIns" + _invRepPara._GlbUserID + ".xls";
                        Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                                0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                                true, false, 0, true, false, false);
                        //PrintPDF(targetFileName, obj._valdtl);
                    }

                }

                // Tharindu 2018-01-24
                if (rad25.Checked == true)
                {
                    _opt = "rad25";
                    _isSelected = true;
                     clsInventory objcrdtrpt = new clsInventory();

                     objcrdtrpt.CreditCardSummaryPrint (Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate,"","","", Session["UserID"].ToString());
                    
                     PrintPDF(targetFileName, objcrdtrpt._creditcardsummarydoc);
                }

                // Tharindu 2018-05-21
                if (rad27.Checked == true)
                {
                    _opt = "rad27";
                    _isSelected = true;
                    update_PC_List_RPTDB();
                    clsInventory objgprpt = new clsInventory();
                    string abc = ucLoactionSearch.txtChanel.Text.ToString();
                    string itemcat1 = txtCat1.Text.Trim();
                    string itmcd = txtItemCode.Text.Trim();
                    string pc = ucProfitCenterSearch.ProfitCenter;

                    Session["InvReportPara"] = _invRepPara;
                    objgprpt.CategoryWiseTradingGP(ucLoactionSearch.Company, _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, ucProfitCenterSearch.Channel, itemcat1, Session["UserID"].ToString(), itmcd);
                  

                  //  objgprpt.CategoryWiseTradingGP(_invRepPara);

                    PrintPDF(targetFileName, objgprpt._cattrading);
                }


                //-----------------------------------------

                if (_export == "N")
                {
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
                    if (rbword.Checked)
                    {
                        url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".doc','_blank');</script>";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }

                if (_isSelected == false)
                    displayMessage("Please select the report!");

                lbtnView.Enabled = true;
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "Mis_Rep", "Run Ok", Session["UserID"].ToString());
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "Mis_Rep", ex.Message, Session["UserID"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                lbtnView.Enabled = true;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void _copytoLocal(string _filePath)
        {
            string filenamenew = Session["UserID"].ToString();
            string name = _filePath;
            FileInfo file = new FileInfo(name);

            string path = @"C:\Download_excel";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (file.Exists)
            {


                System.IO.File.Copy(@"\\" + _filePath, "C:/Download_excel/" + filenamenew + ".xlsx", true);
            }
            else
            {
                DisplayMessage("This file does not exist.", 1);
            }
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //clsInventory obj = new clsInventory();
                //InvReportPara _objRepoPara = new InvReportPara();
                //_objRepoPara = Session["InvReportPara"] as InvReportPara;
                //obj.Print_AOA_Warra(_objRepoPara);
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
                if (rbword.Checked)
                {
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.WordForWindows;
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

        private void LoadEmptyGrid()
        {
            dgvCompany.DataSource = null;
            dgvCompany.DataBind();
            dgvAdminTeam.DataSource = null;
            dgvAdminTeam.DataBind();

            dgvDelAdminTeam.DataSource = null;
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
            dgvDelAdminTeam.DataSource = null;

            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
            dt = CHNLSVC.CommonSearch.GetAdminTeamByCompany(para, null, null);
            if (dt.Rows.Count > 0)
            {
                System.Data.DataColumn newColumn = new System.Data.DataColumn("flag", typeof(System.Int32));
                newColumn.DefaultValue = "0";
                dt.Columns.Add(newColumn);
                Session["DATAADMIN"] = dt;
                dgvAdminTeam.DataSource = (DataTable)Session["DATAADMIN"];
                dgvDelAdminTeam.DataSource = (DataTable)Session["DATAADMIN"];
            }
            dgvAdminTeam.DataBind();
            dgvDelAdminTeam.DataBind();
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
                case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                    {
                        paramsText.Append(seperator);
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
                case CommonUIDefiniton.SearchUserControlType.SalesOrderNew:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"] + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotor:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
                //case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                //    {
                //        paramsText.Append(seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.DocSubType:
                //    {
                //        paramsText.Append(txtDocType.Text.ToString() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.InventoryDirection:
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
            CheckBox chkSelect = (CheckBox)Row.FindControl("chkCompanyCode");
            bool select = chkSelect.Checked;
            string cellvalue = compCode.Text;

            Session["DATACOMPANY"] = dtCompany;
            BindAdminTeam();
            if (chkAllCompany.Checked)
            {
                ucLoactionSearch.ClearText();
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
                    ucLoactionSearch.Company = cellvalue;
                    ucProfitCenterSearch.Company = cellvalue;
                }
                else
                {
                    ucLoactionSearch.ClearText();
                }
            }
            ucLoactionSearch._allcom1 = 1;
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
        //public void BindYear()
        //{
        //    ddlYear.DataSource = null;
        //    int sysYear = 2012;
        //    int yearCount = DateTime.Today.Year - 2011;
        //    int[] yearList = new int[yearCount];
        //    for (int x = 0; x < yearCount; x++)
        //    {
        //        yearList[x] = sysYear;
        //        sysYear++;
        //    }
        //    ddlYear.DataSource = yearList;
        //    ddlYear.DataBind();
        //    ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(DateTime.Today.Year.ToString()));
        //    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(DateTime.Today.Month.ToString()));
        //}

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
                Session["SalesOrderNew"] = null;
                Session["Customer"] = null;
                Session["InvoiceExecutive"] = null;
                Session["Promotor"] = null;
                Session["Supplier"] = null;
                Session["OtherLoc"] = null;


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
                    dt = CHNLSVC.CommonSearch.GetItemSearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
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

                if (lblSearchType.Text == "SalesOrderNew")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                    dt = CHNLSVC.Sales.SearchSalesOrder(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["SalesOrderNew"] = dt;
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
                if (lblSearchType.Text == "OtherLoc")
                {
                    string para = "";
                    dt = CHNLSVC.CommonSearch.Get_OtherLoc(Session["UserCompanyCode"].ToString(), para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["OtherLoc"] = dt;
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
                if (lblSearchType.Text == "Supplier")
                {
                    _result = (DataTable)Session["Supplier"];
                }
                if (lblSearchType.Text == "BrandManager")
                {
                    _result = (DataTable)Session["BrandManager"];
                }
                if (lblSearchType.Text == "ItemStatus")
                {
                    _result = (DataTable)Session["ItemStatus"];
                }
                if (lblSearchType.Text == "SalesOrderNew")
                {
                    _result = (DataTable)Session["SalesOrderNew"];
                }
                if (lblSearchType.Text == "Customer")
                {
                    _result = (DataTable)Session["Customer"];
                }
                if (lblSearchType.Text == "InvoiceExecutive")
                {
                    _result = (DataTable)Session["InvoiceExecutive"];
                }
                if (lblSearchType.Text == "Promotor")
                {
                    _result = (DataTable)Session["Promotor"];
                }
                if (lblSearchType.Text == "OtherLoc")
                {
                    _result = (DataTable)Session["OtherLoc"];
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
                if (lblSearchType.Text == "SalesOrderNew")
                {
                    txtDocNo.Text = code;
                    chkAllDocNo.Checked = false;
                }
                if (lblSearchType.Text == "Customer")
                {
                    txtCustomer.Text = code;
                    chkAllCustomer.Checked = false;
                }
                if (lblSearchType.Text == "InvoiceExecutive")
                {
                    txtExecutive.Text = code;
                    chkAllExecutive.Checked = false;
                }
                if (lblSearchType.Text == "Promotor")
                {
                    txtPromotor.Text = code;
                    chkAllPromotor.Checked = false;
                }
                if (lblSearchType.Text == "OtherLoc")
                {
                    txtotherloc.Text = code;
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
                if (lblSearchType.Text == "ItemStatus")
                {
                    txtItemStat.Text = code;
                    chkAllStat.Checked = false;
                }
                //if (lblSearchType.Text == "DocSubType")
                //{
                //    txtDocSubType.Text = code;
                //    chkAllDocSubType.Checked = false;
                //}
                if (lblSearchType.Text == "InventoryDirection")
                {
                    txtDirection.Text = code;
                    chkAllDirNo.Checked = false;
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


        protected void chkAllDirNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDirection.Text = "";
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
        protected void chkAllBrandMan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllBrandMan.Checked)
            {
                txtBrandMan.Text = "";
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
                        displayMessage(txtCat1.Text + " is invalid ");
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
                        txtCat2.Focus(); displayMessage(txtCat2.Text + " is invalid ");

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
                        txtCat3.Focus(); displayMessage(txtCat3.Text + " is invalid ");

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
                        txtCat4.Focus(); displayMessage(txtCat4.Text + " is invalid ");

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
                        txtCat5.Focus(); displayMessage(txtCat5.Text + " is invalid ");

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
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
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
                        displayMessage(txtItemCode.Text + " is invalid ");
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
                        txtBrand.Focus(); displayMessage(txtBrand.Text + " is invalid ");

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
                        txtModel.Focus(); displayMessage(txtModel.Text + " is invalid ");

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
        //protected void lbtnSeDocType_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblSearchType.Text = "MovementTypes";
        //        string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
        //        DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(para, null, null);
        //        dgvResultItem.DataSource = null;
        //        if (_result.Rows.Count > 0)
        //        {
        //            dgvResultItem.DataSource = _result;
        //            BindUCtrlDDLData(_result);
        //        }
        //        else
        //        {
        //            dgvResultItem.DataSource = null;
        //        }
        //        dgvResultItem.DataBind();
        //        txtSearchbyword.Text = "";
        //        txtSearchbyword.Focus();
        //        ItemPopup.Show();
        //        if (dgvResultItem.PageIndex > 0)
        //        { dgvResultItem.SetPageIndex(0); }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseChannel();
        //    }
        //}

        protected void lbtnSeDocSubType_Click(object sender, EventArgs e)
        {

            try
            {
                lblSearchType.Text = "DocSubType";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(para, null, null);
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

        protected void lbtnSeDocNo_Click(object sender, EventArgs e)
        {

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





        protected void chkAllStat_CheckedChanged(object sender, EventArgs e)
        {
            txtItemStat.Enabled = !chkAllStat.Checked;
            txtItemStat.Text = "";
        }

        //protected void chkAllDocType_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtDocType.Text = "";
        //}

        //protected void chkAllDocSubType_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtDocSubType.Text = "";
        //}

        //protected void chkAllDocNo_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtDocNo.Text = "";
        //}



        protected void chkAllEntType_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void chkAllRecType_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void chkAllBatchNo_CheckedChanged(object sender, EventArgs e)
        {
            //txtBatchNo.Text = "";
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
                    ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                    pnlLocation.Enabled = true;
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
                    ucLoactionSearch.ClearText();
                    pnlLocation.Enabled = false;
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

        protected void lbtnAddLocation_Click(object sender, EventArgs e)
        {
            try
            {
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
                _pclist = new List<proflist>();
                if (Session["PlaceWise"].ToString() == "Location")
                {
                    ucLoactionSearch._allcom = 1;
                    if (chkAllCompany.Checked)
                    {

                        if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10045))
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
                            DataTable dt = CHNLSVC.General.GetLocationByComs(com_cds);
                            dgvLocation.DataSource = dt;
                            dgvLocation.DataBind();
                            //foreach (DataRow drow in dt.Rows)
                            //{
                            //    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                            //}
                        }
                    }
                    else
                    {
                        if (ucLoactionSearch.Company == "")
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
                        string com = ucLoactionSearch.Company;
                        string chanel = ucLoactionSearch.Channel;
                        string subChanel = ucLoactionSearch.SubChannel;
                        string area = ucLoactionSearch.Area;
                        string region = ucLoactionSearch.Regien;
                        string zone = ucLoactionSearch.Zone;
                        string pc = ucLoactionSearch.ProfitCenter;


                        string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();

                        //if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "REPI"))
                        //Add by Chamal 30-Aug-2013
                        if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10045))
                        {
                            // DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);
                            DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                            List<string> chklstbox = new List<string>();
                            foreach (DataRow drow in dt.Rows)
                            {
                                chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                                proflist _pc = new proflist();
                                _pc.pc = drow["PROFIT_CENTER"].ToString();
                                _pc.com = com;

                                _pclist.Add(_pc);
                            }
                            dgvLocation.DataSource = _pclist;
                            dgvLocation.DataBind();
                            //foreach (DataRow drow in dt.Rows)
                            //{
                            //    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                            //}
                        }
                        else
                        {
                            // DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep(Session["UserID"].ToString(), com, chanel, subChanel, area, region, zone, pc);
                            DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                            List<string> chklstbox = new List<string>();
                            foreach (DataRow drow in dt.Rows)
                            {
                                chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                                proflist _pc = new proflist();
                                _pc.pc = drow["PROFIT_CENTER"].ToString();
                                _pc.com = com;

                                _pclist.Add(_pc);
                            }
                            dgvLocation.DataSource = _pclist;
                            dgvLocation.DataBind();
                            //foreach (DataRow drow in dt.Rows)
                            //{
                            //    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                            //}
                        }
                    }
                }
                if (Session["PlaceWise"].ToString() == "ProfitCenter")
                {
                    if ((chkAllCompany.Checked) && (chkAllAdmin.Checked))
                    {
                        string com_cds = "";
                        if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10044))
                        {

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
                        }
                        if (com_cds == "")
                        {
                            displayMessage("Please select the company ");
                            return;
                        }
                        string opval = "";
                        string adminteam = string.Empty;
                        string com = string.Empty;
                        List<string> chklstbox = new List<string>();
                        foreach (GridViewRow Item in dgvAdminTeam.Rows)
                        {
                            Label lblAdminCode = (Label)Item.FindControl("lblAdminCode");
                            CheckBox chkAdminTeam = (CheckBox)Item.FindControl("chkAdminTeam");
                            // string pc = opcode.Text.ToUpper();
                            Label comcode = (Label)Item.FindControl("lblAdminCome");
                            // adminteam = lblAdminCode.Text;
                            if (chkAdminTeam.Checked == true)
                            {
                                // opval = opval + "," + lblAdminCode.Text;
                                adminteam = opval;
                                com = comcode.Text;

                                adminteam = lblAdminCode.Text;



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
                                    //  DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, adminteam);

                                    foreach (DataRow drow in dt.Rows)
                                    {
                                        chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                                        proflist _pc = new proflist();
                                        _pc.pc = drow["PROFIT_CENTER"].ToString();
                                        _pc.com = drow["COM"].ToString();

                                        _pclist.Add(_pc);
                                    }
                                    dgvLocation.DataSource = _pclist;
                                    dgvLocation.DataBind();

                                }
                                else
                                {
                                    // DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                                    //  List<string> chklstbox = new List<string>();
                                    foreach (DataRow drow in dt.Rows)
                                    {
                                        chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                                        proflist _pc = new proflist();
                                        _pc.pc = drow["PROFIT_CENTER"].ToString();
                                        _pc.com = com;

                                        _pclist.Add(_pc);
                                    }
                                    dgvLocation.DataSource = _pclist;
                                    dgvLocation.DataBind();
                                }
                            }
                        }
                    }
                    else if (chkAllCompany.Checked)
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

                                    //---------------

                                    //string opval = "";
                                    //foreach (GridViewRow Item in dgvAdminTeam.Rows)
                                    //{
                                    //    Label lblAdminCode = (Label)Item.FindControl("lblAdminCode");
                                    //    CheckBox chkAdminTeam = (CheckBox)Item.FindControl("chkAdminTeam");
                                    //    // string pc = opcode.Text.ToUpper();

                                    //    if (chkAdminTeam.Checked == true)
                                    //    {
                                    //        opval = opval + lblAdminCode.Text;
                                    //    }
                                    //}
                                    //string adminteam = opval;
                                    //string com = ucLoactionSearch.Company;
                                    //string chanel = ucLoactionSearch.Channel;
                                    //string subChanel = ucLoactionSearch.SubChannel;
                                    //string area = ucLoactionSearch.Area;
                                    //string region = ucLoactionSearch.Regien;
                                    //string zone = ucLoactionSearch.Zone;
                                    //string pc = ucLoactionSearch.ProfitCenter;


                                    //string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();

                                    ////if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "REPI"))
                                    ////Add by Chamal 30-Aug-2013
                                    //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10045))
                                    //{
                                    //    //  DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                                    //    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                                    //    List<string> chklstbox = new List<string>();
                                    //    foreach (DataRow drow in dt.Rows)
                                    //    {
                                    //        chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                                    //    }
                                    //    dgvLocation.DataSource = chklstbox;
                                    //    dgvLocation.DataBind();

                                    //}
                                    //else
                                    //{
                                    //    // DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                                    //    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                                    //    List<string> chklstbox = new List<string>();
                                    //    foreach (DataRow drow in dt.Rows)
                                    //    {
                                    //        chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                                    //    }
                                    //    dgvLocation.DataSource = chklstbox;
                                    //    dgvLocation.DataBind();
                                    //}

                                    //--------------------


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
                                opval = opval + "," + lblAdminCode.Text;
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
                            //  DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                            List<string> chklstbox = new List<string>();
                            foreach (DataRow drow in dt.Rows)
                            {
                                chklstbox.Add(drow["PROFIT_CENTER"].ToString());
                                proflist _pc = new proflist();
                                _pc.pc = drow["PROFIT_CENTER"].ToString();
                                _pc.com = com;

                                _pclist.Add(_pc);
                            }
                            dgvLocation.DataSource = _pclist;
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
                                proflist _pc = new proflist();
                                _pc.pc = drow["PROFIT_CENTER"].ToString();
                                _pc.com = com;

                                _pclist.Add(_pc);
                            }
                            dgvLocation.DataSource = _pclist;
                            dgvLocation.DataBind();
                        }
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
                displayMessage(item + " is already added");
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
                displayMessage(item + " is already added");
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
                displayMessage(item + " is already added");
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
                displayMessage(item + " is already added");
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
                displayMessage(item + " is already added");
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
                displayMessage(item + " is already added");
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
                displayMessage(item + " is already added");
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
                displayMessage(item + " is already added");
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
                displayMessage(item + " is already added");
                return;
            }
        }
        //protected void btnDocType_Click(object sender, EventArgs e)
        //{
        //    bool b1 = false;
        //    string item = "DOC";
        //    foreach (ListItem list in listGroup.Items)
        //    {
        //        if (list.Text == item)
        //        {
        //            b1 = true;
        //            break;
        //        }
        //    }
        //    if (!b1)
        //    {
        //        listGroup.Items.Add(new ListItem(item));
        //    }
        //    else
        //    {
        //        displayMessage(item + " is already added");
        //        return;
        //    }
        //}
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
        protected void btnLocation_Click(object sender, EventArgs e)
        {
            bool b1 = false;
            string item = "LOC";
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
                displayMessage(item + " is already added");
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

                string AdminTMList = "";
                foreach (GridViewRow Item in dgvAdminTeam.Rows)
                {
                    Label lblAdminTM = (Label)Item.FindControl("lblAdminCode");
                    CheckBox chkAdminTM = (CheckBox)Item.FindControl("chkAdminTeam");

                    var loc = lblAdminTM.Text;
                    if (chkAdminTM.Checked)
                    {
                        if (AdminTMList.Length > 0)
                            AdminTMList = AdminTMList + "," + loc;
                        else
                            AdminTMList = loc;
                    }
                }

                Session["AdminTMList"] = AdminTMList;


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

        protected void chkDelAdminTeam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
                Label compCode = (Label)Row.FindControl("lblDelAdminCode");
                CheckBox chkSelect = (CheckBox)Row.FindControl("chkDelAdminTeam");
                bool select = chkSelect.Checked;
                string cellvalue = compCode.Text;


                if (!string.IsNullOrEmpty(cellvalue))
                {

                }

                string delAdminTMList = "";
                foreach (GridViewRow Item in dgvDelAdminTeam.Rows)
                {
                    Label lblDelAdminTM = (Label)Item.FindControl("lblDelAdminCode");
                    CheckBox chkDelAdminTM = (CheckBox)Item.FindControl("chkDelAdminTeam");

                    var loc = lblDelAdminTM.Text;
                    if (chkDelAdminTM.Checked)
                    {
                        if (delAdminTMList.Length > 0)
                            delAdminTMList = delAdminTMList + "," + loc;
                        else
                            delAdminTMList = loc;
                    }
                }

                Session["delAdminTMList"] = delAdminTMList;

                // _invRepPara._GlbLocation = locationList;


                //if (chkDelAdminTeam.Checked)
                //{
                //    //ucLoactionSearch.ClearText();
                //}
                //else
                //{
                //    foreach (GridViewRow hiderowbtn in this.dgvDelAdminTeam.Rows)
                //    {
                //        CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkDelAdminTeam");
                //        if (chkDelAdminTeam.Checked == true)
                //        {
                //            chkDelAdminTeam.Checked = false;
                //        }
                //    }
                //    if (select)
                //    {
                //        foreach (GridViewRow hiderowbtn in this.dgvAdminTeam.Rows)
                //        {
                //            CheckBox chkAdminTeam = (CheckBox)hiderowbtn.FindControl("chkAdminTeam");
                //            Label code = (Label)hiderowbtn.FindControl("lblAdminCode");
                //            if (code.Text == cellvalue)
                //            {
                //                chkAdminTeam.Checked = true;
                //            }
                //        }
                //    }
                //    CheckBox chk = (CheckBox)sender;
                //    if (chk != null && chk.Checked)
                //    {
                //        //ucLoactionSearch.Company = cellvalue;
                //    }
                //    else
                //    {
                //        //ucLoactionSearch.ClearText();
                //    }
                //}

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
                listBox.Enabled = false;
            }
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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11500))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11500)");
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

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);
            lbtn.Add(btnBrand);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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
            chk.Add(chkWithReversal);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radwith1);
            rad.Add(radwithout1);
            rad.Add(radfoc1);

            rad.Add(rbwithicom);
            rad.Add(rbwithoutintercompny);
            rad.Add(rbboth);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
            dgvDelAdminTeam.Visible = false;
        }

        protected void rad10_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11500))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11500)");
            //    rad01.Checked = false;
            //    return;
            //}
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            txt.Add(txtoperteamfrom);
            txt.Add(txtoperteamto);
            lbtn.Add(lbtntoopteam);
            lbtn.Add(lbtnfromopteam);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            rad.Add(rbwithicom);
            rad.Add(rbwithoutintercompny);
            rad.Add(rbboth);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }

        protected void rad11_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11500))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11500)");
            //    rad01.Checked = false;
            //    return;
            //}
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //txt.Add(txtAsAt);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

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
            pnlProfitCenter.Visible = false;
            pnlLocation.Visible = true;
            Session["PlaceWise"] = "Location";
            //_showHeirarchy("PC");
        }

        protected void rad12_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11500))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11500)");
            //    rad01.Checked = false;
            //    return;
            //}
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            //txt.Add(txtFromDate);
            //txt.Add(txtToDate);
            txt.Add(txtAsAt);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

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
            pnlProfitCenter.Visible = false;
            pnlLocation.Visible = true;
            Session["PlaceWise"] = "Location";
            //_showHeirarchy("PC");
        }

        protected void rad13_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11500))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11500)");
            //    rad01.Checked = false;
            //    return;
            //}
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //txt.Add(txtAsAt);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

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
            pnlProfitCenter.Visible = false;
            pnlLocation.Visible = true;
            Session["PlaceWise"] = "Location";
            //_showHeirarchy("PC");
        }

        //protected void rad02_CheckedChanged(object sender, EventArgs e)
        //{
        //    DesableAllControler();
        //    List<DropDownList> ddl = new List<DropDownList>();
        //    List<RadioButton> rad = new List<RadioButton>();
        //    List<TextBox> txt = new List<TextBox>();
        //    List<CheckBox> chk = new List<CheckBox>();
        //    List<LinkButton> lbtn = new List<LinkButton>();
        //    List<ListBox> lst = new List<ListBox>();

        //    txt.Add(txtFromDate);
        //    txt.Add(txtToDate);
        //    chk.Add(chkExportExcel);

        //    _radioList = rad;
        //    _ddlList = ddl;
        //    _txtList = txt;
        //    _chkList = chk;
        //    _lbtnList = lbtn;
        //    _lstList = lst;
        //    EnableControler();
        //    pnlProfitCenter.Visible = true;
        //    pnlLocation.Visible = false;

        //}

        protected void rad03_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11501))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11501)");
                rad03.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);

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

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            txt.Add(txtotherloc);
            lbtn.Add(lbtnotherloc);

            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);

            txt.Add(txtAsAt);
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }

        protected void rad04_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11502))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11502)");
                rad04.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);

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

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);




            txt.Add(txtAsAt);
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
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
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtExDate);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);

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
            lbtn.Add(lbtnSedoNo);
            lbtn.Add(btnAddDoc);

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

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }

        protected void rad06_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11503))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11503)");
                rad06.Checked = false;
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
            txt.Add(txtExDate);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            txt.Add(txtDocNo);
            txt.Add(txtDirection);

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
            lbtn.Add(lbtnSedoNo);
            lbtn.Add(btnAddDoc);
            lbtn.Add(lbtnSeDerection);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkAllDocNo);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

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

            //custormer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnSeCustormerRemove);

            lbtn.Add(lbtnSeExecutive);
            txt.Add(txtExecutive);
            chk.Add(chkAllExecutive);

            lbtn.Add(lbtnSeBrandMan);
            txt.Add(txtBrandMan);

            lbtn.Add(lbtnSeItemSta);
            txt.Add(txtItemStat);

            lbtn.Add(lbtnSeSupplier);
            txt.Add(txtSupplier);

            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            txt.Add(txtotherloc);
            lbtn.Add(lbtnotherloc);
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }

        //dilshan on 16/10/2017
        protected void rad24_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11503))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11503)");
                rad24.Checked = false;
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
            txt.Add(txtExDate);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            txt.Add(txtDocNo);
            txt.Add(txtDirection);

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
            lbtn.Add(lbtnSedoNo);
            lbtn.Add(btnAddDoc);
            lbtn.Add(lbtnSeDerection);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkAllDocNo);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

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

            //custormer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnSeCustormerRemove);

            lbtn.Add(lbtnSeExecutive);
            txt.Add(txtExecutive);
            chk.Add(chkAllExecutive);

            lbtn.Add(lbtnSeBrandMan);
            txt.Add(txtBrandMan);

            lbtn.Add(lbtnSeItemSta);
            txt.Add(txtItemStat);

            lbtn.Add(lbtnSeSupplier);
            txt.Add(txtSupplier);

            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            txt.Add(txtotherloc);
            lbtn.Add(lbtnotherloc);
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }
        protected void rad17_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11500))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11500)");
            //    rad01.Checked = false;
            //    return;
            //}
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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

            chk.Add(chkWithReversal);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radwith1);
            rad.Add(radwithout1);
            rad.Add(radfoc1);

            //rad.Add(rbSale);
            //rad.Add(rbReversal);
            //rad.Add(rbAll);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }

        protected void rad19_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16065))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16065)");
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

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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
            chk.Add(chkWithReversal);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radwith1);
            rad.Add(radwithout1);
            rad.Add(radfoc1);

            rad.Add(rbwithicom);
            rad.Add(rbwithoutintercompny);
            rad.Add(rbboth);
            //rbboth.Checked = true; 

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
            dgvDelAdminTeam.Visible = false;
        }

        protected void rad21_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16065))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16065)");
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

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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
            chk.Add(chkWithReversal);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radwith1);
            rad.Add(radwithout1);
            rad.Add(radfoc1);

            //rad.Add(rbwithicom);
            //rad.Add(rbwithoutintercompny);
            //rad.Add(rbboth);
            rbboth.Checked = true;

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
            dgvDelAdminTeam.Visible = false;
        }

        protected void rad120_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11500))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11500)");
            //    rad01.Checked = false;
            //    return;
            //}
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //chk.Add(chkExportExcel);

            //lbtn.Add(btnCat1);
            //lbtn.Add(btnItemCode);
            //lbtn.Add(btnexective);
            ///lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            // txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtBrandMan);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            //lbtn.Add(lbtnSeCat4);
            //lbtn.Add(lbtnSeCat5);
            //lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);
            lbtn.Add(lbtnSeBrandMan);


            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            //chk.Add(chkAllCat4);
            //chk.Add(chkAllCat5);
            //chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkAllBrandMan);
            //chk.Add(chkWithReversal);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            //lbtn.Add(lbtnAddCat4);
            //lbtn.Add(lbtnAddCat5);
            //lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            //lst.Add(listCat4);
            //lst.Add(listCat5);
            //lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);



            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            // pnlProfitCenter.Visible = true;
            //pnlLocation.Visible = false;
            // Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
            // dgvDelAdminTeam.Visible = false;
        }

        //Tharindu 2018-01-24
        protected void rad25_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11503))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11503)");
                rad24.Checked = false;
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
            txt.Add(txtExDate);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            txt.Add(txtDocNo);
            txt.Add(txtDirection);

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
            lbtn.Add(lbtnSedoNo);
            lbtn.Add(btnAddDoc);
            lbtn.Add(lbtnSeDerection);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkAllDocNo);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

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

            //custormer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnSeCustormerRemove);

            lbtn.Add(lbtnSeExecutive);
            txt.Add(txtExecutive);
            chk.Add(chkAllExecutive);

            lbtn.Add(lbtnSeBrandMan);
            txt.Add(txtBrandMan);

            lbtn.Add(lbtnSeItemSta);
            txt.Add(txtItemStat);

            lbtn.Add(lbtnSeSupplier);
            txt.Add(txtSupplier);

            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            txt.Add(txtotherloc);
            lbtn.Add(lbtnotherloc);
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }

        //dilshan on 16/10/2017
        protected void rad26_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11503))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11503)");
                rad26.Checked = false;
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
            txt.Add(txtExDate);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            txt.Add(txtDocNo);
            txt.Add(txtDirection);

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
            lbtn.Add(lbtnSedoNo);
            lbtn.Add(btnAddDoc);
            lbtn.Add(lbtnSeDerection);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkAllDocNo);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

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

            //custormer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnSeCustormerRemove);

            lbtn.Add(lbtnSeExecutive);
            txt.Add(txtExecutive);
            chk.Add(chkAllExecutive);

            lbtn.Add(lbtnSeBrandMan);
            txt.Add(txtBrandMan);

            lbtn.Add(lbtnSeItemSta);
            txt.Add(txtItemStat);

            lbtn.Add(lbtnSeSupplier);
            txt.Add(txtSupplier);

            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            txt.Add(txtotherloc);
            lbtn.Add(lbtnotherloc);
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }

        //Tharindu 2018-05-21
        protected void rad27_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11503))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11503)");
                rad27.Checked = false;
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
            txt.Add(txtExDate);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            txt.Add(txtDocNo);
            txt.Add(txtDirection);

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
            lbtn.Add(lbtnSedoNo);
            lbtn.Add(btnAddDoc);
            lbtn.Add(lbtnSeDerection);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkAllDocNo);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

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

            //custormer
            txt.Add(txtCustomer);
            lbtn.Add(lbtnSeCustomer);
            chk.Add(chkAllCustomer);
            lbtn.Add(btnSeCustormer);
            lbtn.Add(btnSeCustormerRemove);

            lbtn.Add(lbtnSeExecutive);
            txt.Add(txtExecutive);
            chk.Add(chkAllExecutive);

            lbtn.Add(lbtnSeBrandMan);
            txt.Add(txtBrandMan);

            lbtn.Add(lbtnSeItemSta);
            txt.Add(txtItemStat);

            lbtn.Add(lbtnSeSupplier);
            txt.Add(txtSupplier);

            txt.Add(txtPromotor);
            lbtn.Add(lbtnSePromotor);
            chk.Add(chkAllPromotor);
            txt.Add(txtotherloc);
            lbtn.Add(lbtnotherloc);
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
        }

        protected void lbtnSeDocType_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "MovementTypes";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(para, null, null);
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

        protected void chkAllDocType_CheckedChanged(object sender, EventArgs e)
        {
            txtDocType.Text = "";
        }

        protected void btnDocType_Click(object sender, EventArgs e)
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
                displayMessage("Document type is already added");
                return;
            }
        }

        protected void lbtnSedoNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "SalesOrderNew";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                DataTable _result = CHNLSVC.Sales.SearchSalesOrder(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["SalesOrderNew"] = _result;
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

        protected void chkAllDocNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDocNo.Text = "";
        }

        protected void btnAddDoc_Click(object sender, EventArgs e)
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
                displayMessage("doc # is already added");
                return;
            }
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDocNo.Text.ToUpper().Trim() != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                    DataTable _result = CHNLSVC.Sales.SearchSalesOrder(para, "REFERENCE", txtDocNo.Text.ToUpper().Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Order No"].ToString()))
                        {
                            if (txtDocNo.Text.ToUpper().Trim() == row["Order No"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Order No"].ToString();
                                break;
                            }
                        }
                    }
                    txtDocNo.ToolTip = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid doc # !!!')", true);
                        txtDocNo.Text = "";
                        txtDocNo.Focus();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtDocType_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSeDocTypeNEW_Click(object sender, EventArgs e)
        {

        }

        protected void rad07_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11504))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11504)");
                rad07.Checked = false;
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
            //txt.Add(txtFromDate);
            //txt.Add(txtToDate);
            //txt.Add(txtExDate);
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtDocNo);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);
            lbtn.Add(lbtnSedoNo);
            lbtn.Add(btnAddDoc);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkAllDocNo);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

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
            //lbtn.Add(lbtnSeDocType);
            //chk.Add(chkAllDocType);
            //lbtn.Add(btnDocType);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
            pnlProfitCenter.Visible = false;
            pnlLocation.Visible = true;
            Session["PlaceWise"] = "Location";
            //_showHeirarchy("LOC");

        }

        protected void txtDirection_TextChanged(object sender, EventArgs e)
        {

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
        protected void chkAllSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSupplier.Checked)
            {
                txtSupplier.Text = "";
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

        protected void btnClassRemove_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnRemoveClass_Click(object sender, EventArgs e)
        {

        }

        protected void rad08_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11505))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11505)");
                rad08.Checked = false;
                return;
            }

            DesableAllControler();

            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            Session["PlaceWise"] = "Location";
            EnableControler();
            pnlProfitCenter.Visible = false;
            pnlLocation.Visible = true;
            //_showHeirarchy("LOC");
        }

        protected void rad09_CheckedChanged(object sender, EventArgs e)
        {
            //Session["PlaceWise"] = "Location";

            //txtFromDate.Enabled = true;
            //txtToDate.Enabled = true;


            DesableAllControler();

            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

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

            //lbtn.Add(lbtnSeCat1);
            //lbtn.Add(lbtnSeCat2);
            //lbtn.Add(lbtnSeCat3);
            //lbtn.Add(lbtnSeCat4);
            //lbtn.Add(lbtnSeCat5);
            //lbtn.Add(lbtnSeItemCode);
            //lbtn.Add(lbtSeBrand);
            //lbtn.Add(lbtnSeModel);

            //chk.Add(chkAllCat1);
            //chk.Add(chkAllCat2);
            //chk.Add(chkAllCat3);
            //chk.Add(chkAllCat4);
            //chk.Add(chkAllCat5);
            //chk.Add(chkAllItemCode);
            //chk.Add(chkAllBrand);
            //chk.Add(chkAllModel);

            //lbtn.Add(lbtnAddCat1);
            //lbtn.Add(lbtnAddCat2);
            //lbtn.Add(lbtnAddCat3);
            //lbtn.Add(lbtnAddCat4);
            //lbtn.Add(lbtnAddCat5);
            //lbtn.Add(lbtnAddItemCode);
            //lbtn.Add(lbtnAddBrand);
            //lbtn.Add(lbtnAddModel);

            //lst.Add(listCat1);
            //lst.Add(listCat2);
            //lst.Add(listCat3);
            //lst.Add(listCat4);
            //lst.Add(listCat5);
            //lst.Add(listItemCode);
            //lst.Add(listBrand);
            //lst.Add(listModel);

            //rad.Add(radwith);
            //rad.Add(radwithout);
            //rad.Add(radfoc);

            //lbtn.Add(btnCat1);
            //lbtn.Add(btnItemCode);

            //_radioList = rad;
            //_ddlList = ddl;
            _txtList = txt;
            //_chkList = chk;
            //_lbtnList = lbtn;
            //_lstList = lst;
            Session["PlaceWise"] = "Location";
            EnableControler();
            pnlProfitCenter.Visible = false;
            pnlLocation.Visible = true;

        }

        protected void rad14_CheckedChanged(object sender, EventArgs e)
        {
            //Session["PlaceWise"] = "Location";

            //txtFromDate.Enabled = true;
            //txtToDate.Enabled = true;


            DesableAllControler();

            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

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

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            //lbtn.Add(lbtnSeItemCode);
            //lbtn.Add(lbtSeBrand);
            //lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            //chk.Add(chkAllItemCode);
            //chk.Add(chkAllBrand);
            //chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            //lbtn.Add(lbtnAddItemCode);
            //lbtn.Add(lbtnAddBrand);
            //lbtn.Add(lbtnAddModel);

            //lst.Add(listCat1);
            //lst.Add(listCat2);
            //lst.Add(listCat3);
            //lst.Add(listCat4);
            //lst.Add(listCat5);
            //lst.Add(listItemCode);
            //lst.Add(listBrand);
            //lst.Add(listModel);

            //rad.Add(radwith);
            //rad.Add(radwithout);
            //rad.Add(radfoc);

            lbtn.Add(btnCat1);
            lbtn.Add(btnCat2);
            lbtn.Add(btnCat3);
            lbtn.Add(btnCat4);
            lbtn.Add(btnCat5);
            //lbtn.Add(btnItemCode);

            //_radioList = rad;
            //_ddlList = ddl;
            _txtList = txt;
            //_chkList = chk;
            _lbtnList = lbtn;
            //_lstList = lst;
            Session["PlaceWise"] = "ProfitCenter";
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;

        }

        protected void rad15_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();

            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            Session["PlaceWise"] = "ProfitCenter";
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            dgvDelAdminTeam.Visible = false;
            //_showHeirarchy("LOC");
        }

        protected void rad16_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();

            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            // txt.Add(txtCat3);
            // txt.Add(txtCat4);
            // txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            // txt.Add(txtModel);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            // lbtn.Add(lbtnSeCat3);
            // lbtn.Add(lbtnSeCat4);
            // lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            // lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            // chk.Add(chkAllCat3);
            // chk.Add(chkAllCat4);
            // chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            // chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            //lbtn.Add(lbtnAddCat3);
            //lbtn.Add(lbtnAddCat4);
            //lbtn.Add(lbtnAddCat5);
            //lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            // lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            // lst.Add(listCat3);
            // lst.Add(listCat4);
            // lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            // lst.Add(listModel);

            rad.Add(radwith);
            rad.Add(radwithout);
            rad.Add(radfoc);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            Session["PlaceWise"] = "ProfitCenter";
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;

            dgvAdminTeam.Enabled = false;
            dgvDelAdminTeam.Visible = false;
            //_showHeirarchy("LOC");
        }
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(txtToDate.Text.ToString()), Convert.ToDateTime(txtFromDate.Text.ToString())) < 0)
            {
                displayMessage("From date cannot exceed to date");

                txtToDate.Text = "";
            }
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

        protected void lbtnotherloc_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "OtherLoc";
                DataTable _result = CHNLSVC.CommonSearch.Get_OtherLoc(Session["UserCompanyCode"].ToString(), "", null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["OtherLoc"] = _result;
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
                DisplayMessage(ex.Message, 4);
            }
        }

        private void update_Location_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, null, null);

            foreach (GridViewRow Item in dgvLocation.Rows)
            {
                Label lblLocation = (Label)Item.FindControl("lblLocation");
                CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                string loc = lblLocation.Text.ToUpper();

                if (chkLocation.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, null, loc);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = loc;
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
                BaseCls.GlbReportProfit = ucLoactionSearch.ProfitCenter.ToUpper();
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, ucLoactionSearch.ProfitCenter.ToUpper(), null);
            }
        }

        protected void rad18_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11518))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11518)");
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

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtFromDate2);
            txt.Add(txtToDate2);
            //chk.Add(chkExportExcel);

            lbtn.Add(btnCat1);
            lbtn.Add(btnItemCode);
            lbtn.Add(btnexective);
            lbtn.Add(btnAddDoc);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

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
            chk.Add(chkWithReversal);
            chk.Add(chkWithReplItems);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radwith1);
            rad.Add(radwithout1);
            rad.Add(radfoc1);

            rad.Add(rbwithicom);
            rad.Add(rbwithoutintercompny);
            rad.Add(rbboth);

            radwith1.Checked = true;
            chkWithReversal.Checked = true;
            chkWithReplItems.Checked = true;

            chk.Add(chkExportExcel);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            pnlProfitCenter.Visible = true;
            pnlLocation.Visible = false;
            Session["PlaceWise"] = "ProfitCenter";
            //_showHeirarchy("PC");
            dgvDelAdminTeam.Visible = false;
        }

        protected void txtToDate2_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(txtToDate2.Text.ToString()), Convert.ToDateTime(txtFromDate2.Text.ToString())) < 0)
            {
                displayMessage("From date cannot exceed to date");

                txtToDate2.Text = "";
            }
        }

        protected void lbtnfromopteam_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "FromTeam";
                DataTable _result = CHNLSVC.CommonSearch.Get_Operationteam(Session["UserCompanyCode"].ToString(), "", null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["FromTeam"] = _result;
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

        protected void lbtntoopteam_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ToTeam";
                DataTable _result = CHNLSVC.CommonSearch.Get_Operationteam(Session["UserCompanyCode"].ToString(), "", null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["ToTeam"] = _result;
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

        public class proflist
        {
            public string pc { get; set; }
            public string com { get; set; }
        }
    }
}