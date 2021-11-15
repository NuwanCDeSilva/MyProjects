using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using FastForward.SCMWeb.UserControls;
using System.Web.UI;
using FastForward.SCMWeb.UserControls;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using FF.BusinessObjects.InventoryNew;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using FF.BusinessObjects.General;
namespace FastForward.SCMWeb.View.Reports.Warehouse
{
    public partial class warehouse : BasePage
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

        private List<TransportParty> _tPartys
        {
            get { if (Session["_tPartys"] != null) { return (List<TransportParty>)Session["_tPartys"]; } else { return new List<TransportParty>(); } }
            set { Session["_tPartys"] = value; }
        }

        private TransportParty _tParty
        {
            get { if (Session["_tParty"] != null) { return (TransportParty)Session["_tParty"]; } else { return new TransportParty(); } }
            set { Session["_tParty"] = value; }
        }
        //Added By Udaya 03.10.2017
        public DropDownList DdlServiceBy
        {
            get { return ddlServiceBy; }
            set { ddlServiceBy = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  ReadPageControlerID();
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                Session["DATAADMIN"] = null;
                Session["DATACOMPANY"] = null;

                LoadEmptyGrid();
                BindCompany(Session["UserID"].ToString());
                ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                txtFromDate.Text = (DateTime.Now.Date.AddMonths(-1)).ToString("dd/MMM/yyyy");
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtAsAt.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtExDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                // frmtime.Text = (DateTime.Now.TimeOfDay).ToString("hh:mm");
                hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                locationPanel.Enabled = !chkAllCompany.Checked;
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
                txtAgeFrom.Text = "0";
                txtAgeTo.Text = "9999";
                dgvAdminTeam.Columns[1].Visible = false;
                AddEnableCntr();
                DesableAllControler();
                TestEnablefield();
                BindYear();
                Loadselectdate();
                BindTransService();
                // Default();
            }
            else
            {
                // Default();
                // Loadselectdate();
            }
        }

        private void TestEnablefield()
        {

        }
        private void Default()
        {
            if (ucLoactionSearch.txtCompany.Text == "")
            {
                ucLoactionSearch.txtCompany.Text = Session["UserCompanyCode"].ToString();
            }
            if (ucLoactionSearch.txtLocation.Text == "")
            {
                ucLoactionSearch.txtLocation.Text = Session["UserDefLoca"].ToString();
            }

            foreach (GridViewRow _row in dgvCompany.Rows)
            {
                CheckBox ItemCode = (_row.FindControl("chkCompanyCode") as CheckBox);
                Label com = (_row.FindControl("lblCode") as Label);
                if (com.Text == Session["UserCompanyCode"].ToString())
                {
                    ItemCode.Checked = true;
                }
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
            _rList.Add(rbpdf);
            _rList.Add(rbexel);
            _rList.Add(rbexeldata);
            _rList.Add(radmodwise);
            _rList.Add(radmodwisesumm);
            _rList.Add(radrequestwise);
            _rList.Add(radreqwisesumm);

            _enbleRadioList = _rList;

            List<CheckBox> _lChk = new List<CheckBox>();
            _lChk.Add(chkAllCompany);
            _lChk.Add(chkAllAdmin);

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
            _lTxt.Add(frmtime);
            _lTxt.Add(totime);
            _lTxt.Add(txtSupplier);
            _lTxt.Add(txtroute);
            _lTxt.Add(txtColor);
            _lTxt.Add(txtappdocno);
            _lTxt.Add(txtdoctypenew);
            _lTxt.Add(txtDocNo);


            _enbleTxtList = _lTxt;

            List<LinkButton> _lBtn = new List<LinkButton>();
            _lBtn.Add(lbtnView);
            _lBtn.Add(lbtnClear);
            _lBtn.Add(lbtnSearch);
            _lBtn.Add(lbtnSeSupplier);
            _lBtn.Add(btnSupplier);
            _lBtn.Add(lbtnroute);
            _lBtn.Add(btnroute);
            _lBtn.Add(lbtnColor);
            _lBtn.Add(lbtnappdocno);
            _lBtn.Add(lbtndoctype);


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
            _lBtn.Add(lbtnrouteAll);
            _lBtn.Add(lbtnLocationNone);
            _lBtn.Add(lbtnrouteNone);
            _lBtn.Add(lbtnLocationClear);
            _lBtn.Add(lbtnrouteClear);
            _enableLbtnList = _lBtn;

            List<DropDownList> _lDdl = new List<DropDownList>();
            _lDdl.Add(ucLoactionSearch.cmbSearchby);
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

        string _opt = "";
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dgvLocation.Rows.Count == 0) && (ucLoactionSearch.txtLocation.Text == ""))
                {
                    displayMessage("Please add location deatils");
                    return;
                }
                if ((ucLoactionSearch.txtCompany.Text == ""))
                {
                    displayMessage("Please enter company");
                    return;
                }
                Boolean _isSelected = false;

                Session["GlbReportName"] = string.Empty;
                GlbReportName = string.Empty;

                lbtnView.Enabled = false;

                //kapila 23/12/2015 - to get the company name in the report header
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

                //BaseCls.GlbReportCompName = com_desc;
                //BaseCls.GlbReportCompanies = com_cds;

                ////set all common parameters
                //BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                //BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                //BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                //BaseCls.GlbReportSupplier = txtSupplier.Text.ToString();

                //BaseCls.GlbReportCompCode = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportComp = Session["UserCompanyCode"].ToString();
                //BaseCls.GlbReportProfit = Session["UserDefLoca"].ToString();

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
                //BaseCls.GlbReportDoc = txtDocNo.Text;

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
                Session["GlbReportType"] = "";
                Session["GlbReportDoc"] = txtDocNo.Text;

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
                _invRepPara._GlbReportDoc = Session["GlbReportDoc"].ToString();

                _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString();
                _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportProfit = ucLoactionSearch.txtLocation.Text == "" ? "All Locations based on user rights" : ucLoactionSearch.txtLocation.Text;
                _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
                _invRepPara._GlbUserID = Session["UserID"].ToString();
                _invRepPara._GlbReportWithJob = "N";
                _opt = "";

                string _export = "N";
                csWarehouse obj = new csWarehouse();
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

                if (rad01.Checked == true)  // Item Pick Plan - Sanjeewa 2016-05-25
                {
                    _opt = "rad01";
                    _isSelected = true;

                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;
                    _invRepPara._GlbReportColor = txtColor.Text;
                    _invRepPara._GlbReportwithBin = chkwithbincd.Checked == true ? 1 : 0;
                    _invRepPara._GlbReportRoute = txtroute.Text;
                    _invRepPara._GlbReportCheckRegDate = chkallroute.Checked == true ? 1 : 0; // All Routes
                    _invRepPara._GlbReportnoofDays = radrequestwise.Checked == true ? 1 : 0; //Group Req
                    _invRepPara._GlbReportFromAge = radmodwise.Checked == true ? 1 : 0; //Group Model
                    _invRepPara._GlbReportReqNo = txtdoctypenew.Text;//Req Type
                    _invRepPara._GlbReportGetTimeWise = chkistime.Checked == true ? 1 : 0;

                    DateTime _tmpDtFrom = Convert.ToDateTime(txtFromDate.Text);
                    DateTime _tmpTmFrom = Convert.ToDateTime(frmtime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : frmtime.Text);
                    DateTime _tmpActFrom = new DateTime(_tmpDtFrom.Year, _tmpDtFrom.Month, _tmpDtFrom.Day, _tmpTmFrom.Hour, _tmpTmFrom.Minute, _tmpTmFrom.Second);

                    DateTime _tmpDtTo = Convert.ToDateTime(txtToDate.Text);
                    DateTime _tmpTmTo = Convert.ToDateTime(totime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : totime.Text);
                    DateTime _tmpActTo = new DateTime(_tmpDtTo.Year, _tmpDtTo.Month, _tmpDtTo.Day, _tmpTmTo.Hour, _tmpTmTo.Minute, _tmpTmTo.Second);

                    _invRepPara._GlbReportFromDate = chkistime.Checked ? _tmpActFrom : Convert.ToDateTime(txtFromDate.Text);
                    _invRepPara._GlbReportToDate = chkistime.Checked ? _tmpActTo : Convert.ToDateTime(txtToDate.Text);

                    if (Session["UserCompanyCode"].ToString() == "AAL")
                    {
                        _invRepPara._GlbReportName = "_PICK_PLAN_AAL.rpt";  //Item_Pick_Plan_sum_AAL
                        Session["GlbReportName"] = "_PICK_PLAN_AAL.rpt";
                    }
                    else
                    {
                        if (_invRepPara._GlbReportwithBin == 1)
                        {
                            _invRepPara._GlbReportName = "Item_Pick_Plan.rpt";
                            Session["GlbReportName"] = "Item_Pick_Plan.rpt";
                        }
                        else
                        {
                            _invRepPara._GlbReportName = "Item_Pick_Plan_sum.rpt";
                            Session["GlbReportName"] = "Item_Pick_Plan_sum.rpt";
                        }
                    }
                    Session["UserCompanyCode"] = _invRepPara._GlbReportCompCode;
                    update_PC_List_RPTDB();
                    _invRepPara._GlbReportHeading = "ITEM PICK PLAN";
                    Session["InvReportPara"] = _invRepPara;
                    obj.ItemPickPlanReport(_invRepPara);

                    if (Session["UserCompanyCode"].ToString() == "AAL")
                    {
                        PrintPDF(targetFileName, obj._PICK_PLAN_AAL); //_pickplansum_AAL
                    }
                    else
                    {
                        if (_invRepPara._GlbReportwithBin == 1)
                        {
                            PrintPDF(targetFileName, obj._pickplan);
                        }
                        else
                        {
                            PrintPDF(targetFileName, obj._pickplansum);
                        }
                    }
                    //string url = "<script>window.open('/View/Reports/Warehouse/WarehouseReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad02.Checked == true)
                {
                    _opt = "rad02";
                    _isSelected = true;
                    Session["docNo"] = txtDocNo.Text.Trim();
                    Session["ItemCode"] = txtItemCode.Text.Trim();
                    Session["GlbReportName"] = "Stock_Verification.rpt";
                    Session["GlbReportType"] = "";
                    Session["InvReportPara"] = _invRepPara;

                    obj.StockVerificationnew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["ItemCode"].ToString(), Session["docNo"].ToString());
                    PrintPDF(targetFileName, obj.stock_verify);

                    //string url = "<script>window.open('/View/Reports/Warehouse/WarehouseReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                //Dispatch Summary Report
                if (rad03.Checked == true)
                {
                    _opt = "rad03";
                    _isSelected = true;
                    Session["docNo"] = txtDocNo.Text.Trim();
                    Session["ItemCode"] = txtItemCode.Text.Trim();
                    Session["GlbReportName"] = "Dispatch_Req_Report.rpt";
                    Session["GlbReportType"] = "";
                    Session["InvReportPara"] = _invRepPara;

                    _invRepPara._GlbCompany = Session["GlbReportCompanies"].ToString();//Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;

                    DateTime _tmpDtFrom = Convert.ToDateTime(txtFromDate.Text);
                    DateTime _tmpTmFrom = Convert.ToDateTime(frmtime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : frmtime.Text);
                    DateTime _tmpActFrom = new DateTime(_tmpDtFrom.Year, _tmpDtFrom.Month, _tmpDtFrom.Day, _tmpTmFrom.Hour, _tmpTmFrom.Minute, _tmpTmFrom.Second);

                    DateTime _tmpDtTo = Convert.ToDateTime(txtToDate.Text);
                    DateTime _tmpTmTo = Convert.ToDateTime(totime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : totime.Text);
                    DateTime _tmpActTo = new DateTime(_tmpDtTo.Year, _tmpDtTo.Month, _tmpDtTo.Day, _tmpTmTo.Hour, _tmpTmTo.Minute, _tmpTmTo.Second);


                    _invRepPara._GlbReportFromDate = rbtnExpDte.Checked ? Convert.ToDateTime(txtFromDate.Text) : _tmpActFrom;
                    _invRepPara._GlbReportToDate = rbtnExpDte.Checked ? Convert.ToDateTime(txtToDate.Text) : _tmpActTo;
                    _invRepPara._GlbReportRoute = txtroute.Text;
                    _invRepPara._GlbLocation = "";

                    _invRepPara._GlbDispatchStatus = ddlStatus.SelectedValue.ToString();
                    _invRepPara._GlbReportGroupfrmtime = Convert.ToDateTime(frmtime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : frmtime.Text);
                    _invRepPara._GlbReportGrouptotime = Convert.ToDateTime(totime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : totime.Text);
                    _invRepPara._GlbReportExDteWise = rbtnExpDte.Checked ? 1 : 0;
                    _invRepPara._GlbReportprintMark = chkprintRecord.Checked ? 1 : 0;
                    if (txtroute.Text.Length <= 0)
                    {
                        update_Location_List_RPTDB(); //save locations tempory

                        string locationList = "";
                        //foreach (GridViewRow Item in dgvLocation.Rows)
                        //{
                        //    Label lblLocation = (Label)Item.FindControl("lblLocation");
                        //    CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");

                        //    var loc = lblLocation.Text;
                        //    if (chkLocation.Checked)
                        //    {
                        //        if (locationList.Length > 0)
                        //            locationList = locationList + "," + loc;
                        //        else
                        //            locationList = loc;
                        //    }
                        //}
                        int aa = locationList.Length;
                        var array = locationList.Split(',');
                        _invRepPara._GlbLocation = locationList;
                    }
                    Int32 effect = 0;
                    DataTable dispatchSummary = new DataTable("DispatchSummary");
                    dispatchSummary = obj.DispatchRequestReport(_invRepPara);
                    if (dispatchSummary != null && dispatchSummary.Rows.Count > 0)
                    {
                        if (hdfmarkPrinted.Value == "1")
                        {
                            effect = CHNLSVC.Inventory.DispatchRecordUpdate(dispatchSummary, Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), Session["SessionID"].ToString(), DateTime.Now, rad03.Checked ? 1 : 0);
                        }
                    }
                    else
                    {
                        //_export = "N/A";
                        DisplayMessage("No any data for selected filer option...!!!", 1);
                    }
                    if (effect == 1)
                    {
                        DisplayMessage("Successfully Update", 3);
                    }
                    PrintPDF(targetFileName, obj._dispatchReqSummary);
                }

                //Dispatch Details Report
                if (rad04.Checked == true)
                {
                    _opt = "rad04";
                    _isSelected = true;
                    Session["docNo"] = txtDocNo.Text.Trim();
                    Session["ItemCode"] = txtItemCode.Text.Trim();
                    if (Session["GlbReportCompanies"].ToString() == "AAL")
                    {
                        Session["GlbReportName"] = "Dispatch_Req_Detail_Report_AAL.rpt";
                    }
                    else
                    {
                        Session["GlbReportName"] = "Dispatch_Req_Detail_Report.rpt";
                    }

                    Session["GlbReportType"] = "";
                    Session["InvReportPara"] = _invRepPara;
                    _invRepPara._GlbReportName = Session["GlbReportName"].ToString();

                    _invRepPara._GlbCompany = Session["GlbReportCompanies"].ToString();//Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;

                    DateTime _tmpDtFrom = Convert.ToDateTime(txtFromDate.Text);
                    DateTime _tmpTmFrom = Convert.ToDateTime(frmtime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : frmtime.Text);
                    DateTime _tmpActFrom = new DateTime(_tmpDtFrom.Year, _tmpDtFrom.Month, _tmpDtFrom.Day, _tmpTmFrom.Hour, _tmpTmFrom.Minute, _tmpTmFrom.Second);

                    DateTime _tmpDtTo = Convert.ToDateTime(txtToDate.Text);
                    DateTime _tmpTmTo = Convert.ToDateTime(totime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : totime.Text);
                    DateTime _tmpActTo = new DateTime(_tmpDtTo.Year, _tmpDtTo.Month, _tmpDtTo.Day, _tmpTmTo.Hour, _tmpTmTo.Minute, _tmpTmTo.Second);

                    _invRepPara._GlbReportFromDate = rbtnExpDte.Checked ? Convert.ToDateTime(txtFromDate.Text) : _tmpActFrom;
                    _invRepPara._GlbReportToDate = rbtnExpDte.Checked ? Convert.ToDateTime(txtToDate.Text) : _tmpActTo;
                    _invRepPara._GlbReportRoute = txtroute.Text;
                    _invRepPara._GlbLocation = "";

                    _invRepPara._GlbDispatchStatus = ddlStatus.SelectedValue.ToString();
                    _invRepPara._GlbReportGroupfrmtime = Convert.ToDateTime(frmtime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : frmtime.Text);
                    _invRepPara._GlbReportGrouptotime = Convert.ToDateTime(totime.Text == "" ? DateTime.Now.ToString("HH:mm:ss tt") : totime.Text);
                    _invRepPara._GlbReportExDteWise = rbtnExpDte.Checked ? 1 : 0;
                    _invRepPara._GlbReportprintMark = chkprintRecord.Checked ? 1 : 0;
                    if (txtroute.Text.Length <= 0)
                    {
                        update_Location_List_RPTDB(); //save locations tempory

                        string locationList = "";
                        //foreach (GridViewRow Item in dgvLocation.Rows)
                        //{
                        //    Label lblLocation = (Label)Item.FindControl("lblLocation");
                        //    CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");

                        //    var loc = lblLocation.Text;
                        //    if (chkLocation.Checked)
                        //    {
                        //        if (locationList.Length > 0)
                        //            locationList = locationList + "," + loc;
                        //        else
                        //            locationList = loc;
                        //    }
                        //}

                        _invRepPara._GlbLocation = locationList;
                    }
                    Int32 effect = 0;
                    DataTable dispatchDetails = new DataTable("DispatchDetails");
                    dispatchDetails = obj.DispatchRequestDtlReport(_invRepPara);
                    if (dispatchDetails != null && dispatchDetails.Rows.Count > 0)
                    {
                        if (hdfmarkPrinted.Value == "1")
                        {
                            effect = CHNLSVC.Inventory.DispatchRecordUpdate(dispatchDetails, Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), Session["SessionID"].ToString(), DateTime.Now, rad04.Checked ? 0 : 1);
                        }
                    }
                    else
                    {
                        //_export = "N/A";
                        DisplayMessage("No any data for selected filer option...!!!", 1);
                    }
                    if (effect == 1)
                    {
                        DisplayMessage("Successfully Update", 3);
                    }
                    if (Session["GlbReportCompanies"].ToString() == "AAL")
                    {
                        PrintPDF(targetFileName, obj._dispatchReqDetailsaal);
                    }
                    else
                    {
                        PrintPDF(targetFileName, obj._dispatchReqDetails);
                    }
                }
                if (rad05.Checked == true)
                {
                    _opt = "rad05";
                    _isSelected = true;
                    Session["docNo"] = txtDocNo.Text.Trim();
                    Session["ItemCode"] = txtItemCode.Text.Trim();
                    Session["GlbReportName"] = "Item_Dispatch_Detail_Report.rpt";
                    Session["GlbReportType"] = "";
                    Session["InvReportPara"] = _invRepPara;

                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();//UserCompanyCode
                    _invRepPara._GlbReportCompName = Session["GlbReportCompName"].ToString();
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;
                    _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    _invRepPara._GlbReportRoute = txtroute.Text;
                    _invRepPara._GlbLocation = "";

                    //DataTable rep_det = CHNLSVC.Inventory.GetReportParam(_invRepPara._GlbReportCompCode, _invRepPara._GlbUserID);
                    //foreach (DataRow row in rep_det.Rows)
                    //{
                    //    _invRepPara._GlbReportPoweredBy = row["MC_IT_POWERED"].ToString();
                    //}

                    //if (txtroute.Text.Length <= 0)
                    //{
                    //    update_Location_List_RPTDB(); //save locations tempory

                    //    string locationList = "";
                    //    foreach (GridViewRow Item in dgvLocation.Rows)
                    //    {
                    //        Label lblLocation = (Label)Item.FindControl("lblLocation");
                    //        CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");

                    //        var loc = lblLocation.Text;
                    //        if (chkLocation.Checked)
                    //        {
                    //            if (locationList.Length > 0)
                    //                locationList = locationList + "," + loc;
                    //            else
                    //                locationList = loc;
                    //        }
                    //    }

                    //    _invRepPara._GlbLocation = locationList;
                    //}

                    //obj.ItemDispatchDetailReport(_invRepPara);
                    //PrintPDF(targetFileName, obj._itemDispatchDetails);
                    int eff = CHNLSVC.MsgPortal.ItemDispatchDetailAutoMail(_invRepPara);

                    if (eff==1)
                    {
                        displayMessage("Excel File Alredy Sent...");
                        return;
                    }

                }

                if (rad06.Checked == true)  // Inter Transfer Request Report - Sanjeewa 2017-01-31
                {
                    _opt = "rad06";
                    _isSelected = true;
                    _export = "Y";

                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;
                    _invRepPara._GlbReportRoute = txtroute.Text;
                    _invRepPara._GlbReportCheckRegDate = chkallroute.Checked == true ? 1 : 0; // All Routes                    _
                    _invRepPara._GlbReportReqNo = txtdoctypenew.Text;//Req Type
                    _invRepPara._GlbReportSupplier = "";

                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportHeading = "Inter Transfer Request Report";
                    Session["InvReportPara"] = _invRepPara;

                    string _err = "";
                    string _filePath = CHNLSVC.MsgPortal.IntertransferDtlReport(Convert.ToDateTime(_invRepPara._GlbReportFromDate).Date,
                        Convert.ToDateTime(_invRepPara._GlbReportToDate).Date, _invRepPara._GlbReportCompCode, "", _invRepPara._GlbReportOtherLoc, _invRepPara._GlbUserID,
                        _invRepPara._GlbReportDoc, _invRepPara._GlbReportRoute, _invRepPara._GlbReportSupplier, out _err);

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
                    p.StartInfo = new ProcessStartInfo("C:/Download_excel/" + Session["UserID"].ToString() + ".xlsx");
                    p.Start();

                }

                if (rad07.Checked == true)
                {
                    _opt = "rad07";
                    _isSelected = true;
                    _invRepPara._GlbReportRoute = txtroute.Text;
                    _invRepPara._GlbReportCheckRegDate = chkallroute.Checked == true ? 1 : 0; // All Routes
                    _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    _invRepPara._GlbReportName = "Approved_MRN_Summary.rpt";
                    Session["UserCompanyCode"] = _invRepPara._GlbReportCompCode;
                    _invRepPara._GlbReportType = ddlReportType.SelectedValue;
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportHeading = "Approved_MRN";
                    Session["InvReportPara"] = _invRepPara;

                    obj.ApprovedMRN(_invRepPara);
                    PrintPDF(targetFileName, obj._approvedMRN);
                }

                if (rad08.Checked == true)
                {
                    _opt = "rad08";
                    _isSelected = true;
                    _invRepPara._GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    _invRepPara._GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    _invRepPara._GlbReportName = "Courier_Dailly_Summary.rpt";
                    Session["UserCompanyCode"] = _invRepPara._GlbReportCompCode;
                    _invRepPara._GlbReportType = ddlReportType.SelectedValue;
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportHeading = "Courier Dailly Summary";
                    Session["InvReportPara"] = _invRepPara;

                    obj.CourierDaillySummary(_invRepPara, ddlServiceBy.SelectedValue, txtFromNo.Text, txtToNo.Text);
                    PrintPDF(targetFileName, obj._courierDaillySum);
                }

                if (_isSelected == false)
                    displayMessage("Please select the report!");

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
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }

                lbtnView.Enabled = true;
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "warehouse", "Run Ok", Session["UserID"].ToString());
            }
            catch (Exception err)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "warehouse", err.Message, Session["UserID"].ToString());
                CHNLSVC.CloseChannel();
                Response.Redirect("~/Error.aspx?Error=" + err.Message + "");
                lbtnView.Enabled = true;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void update_PC_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, null, null);

            foreach (GridViewRow Item in dgvLocation.Rows)
            {
                Label lblLocation = (Label)Item.FindControl("lblLocation");
                CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                string pc = lblLocation.Text.ToUpper();

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
                BaseCls.GlbReportProfit = ucLoactionSearch.ProfitCenter.ToUpper();
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), ucLoactionSearch.Company, ucLoactionSearch.ProfitCenter.ToUpper(), null);
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
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub2.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + CommonUIDefiniton.SearchUserControlType.CAT_Sub3.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub4:
                    {
                        paramsText.Append(txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + txtCat4.Text + CommonUIDefiniton.SearchUserControlType.CAT_Sub4.ToString() + seperator + "" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.DocStockNo:
                    {
                        paramsText.Append(txtDocNo.Text.ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Color:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RoutDet:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ApprovedDoc:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReqAppType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CourierNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
                }
                else
                {
                    ucLoactionSearch.ClearText();
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
            string month = DateTime.Today.Month.ToString();
            ddlMonth.SelectedIndex = Convert.ToInt32(month) - 1;
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
                Session["DocStockNo"] = null;
                Session["InventoryDirection"] = null;
                Session["ItemStatus"] = null;
                Session["BrandManager"] = null;
                Session["Supplier"] = null;
                Session["Color"] = null;
                Session["Routdetails"] = null;
                Session["ApprovedDoc"] = null;
                Session["ReqAppType"] = null;
                Session["BondNumber"] = null;
                Session["GrnNumber"] = null;
                Session["ReqNumber"] = null;
                Session["OtherLoc"] = null;
                Session["OperationTeam"] = null;
                Session["CourierNos"] = null;
                Session["CourierNosTo"] = null;
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
                if (lblSearchType.Text == "DocStockNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocStockNo);
                    dt = CHNLSVC.CommonSearch.SearchStockDocNo(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["DocStockNo"] = dt;
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
                if (lblSearchType.Text == "Color")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Color);
                    dt = CHNLSVC.CommonSearch.Get_Color_All(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Color"] = dt;
                }
                if (lblSearchType.Text == "Routdetails")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RoutDet);
                    dt = CHNLSVC.CommonSearch.SearchRoutDetailsnew(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Routdetails"] = dt;
                }
                if (lblSearchType.Text == "ApprovedDoc")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovedDoc);
                    dt = CHNLSVC.CommonSearch.SearchApprovedDocNo(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ApprovedDoc"] = dt;
                }
                if (lblSearchType.Text == "ReqAppType")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReqAppType);
                    dt = CHNLSVC.CommonSearch.Get_ReqApprType(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ReqAppType"] = dt;
                }
                if (lblSearchType.Text == "BondNumber")
                {
                    string para = "";
                    dt = CHNLSVC.CommonSearch.Get_BondNumber(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["BondNumber"] = dt;
                }
                if (lblSearchType.Text == "GrnNumber")
                {
                    string para = "";
                    dt = CHNLSVC.CommonSearch.Get_GrnNumber(Session["UserCompanyCode"].ToString(), para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["GrnNumber"] = dt;
                }
                if (lblSearchType.Text == "ReqNumber")
                {
                    string para = "";
                    dt = CHNLSVC.CommonSearch.Get_ReqNumber(Session["UserCompanyCode"].ToString(), para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ReqNumber"] = dt;
                }
                if (lblSearchType.Text == "OtherLoc")
                {
                    string para = "";
                    dt = CHNLSVC.CommonSearch.Get_OtherLoc(Session["UserCompanyCode"].ToString(), para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["OtherLoc"] = dt;
                }
                if (lblSearchType.Text == "OperationTeam")
                {
                    string para = "";
                    dt = CHNLSVC.CommonSearch.Get_Operationteam(Session["UserCompanyCode"].ToString(), para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["OperationTeam"] = dt;
                }
                if (lblSearchType.Text == "CourierNos")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CourierNo);
                    dt = CHNLSVC.CommonSearch.GetCourierNos(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CourierNos"] = dt;
                }
                if (lblSearchType.Text == "CourierNosTo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CourierNo);
                    dt = CHNLSVC.CommonSearch.GetCourierNos(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["CourierNosTo"] = dt;
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
                if (lblSearchType.Text == "DocStockNo")
                {
                    _result = (DataTable)Session["DocStockNo"];
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
                if (lblSearchType.Text == "Color")
                {
                    _result = (DataTable)Session["Color"];
                }
                if (lblSearchType.Text == "Routdetails")
                {
                    _result = (DataTable)Session["Routdetails"];
                }
                if (lblSearchType.Text == "ApprovedDoc")
                {
                    _result = (DataTable)Session["ApprovedDoc"];
                }
                if (lblSearchType.Text == "ReqAppType")
                {
                    _result = (DataTable)Session["ReqAppType"];
                }
                if (lblSearchType.Text == "BondNumber")
                {
                    _result = (DataTable)Session["BondNumber"];
                }
                if (lblSearchType.Text == "GrnNumber")
                {
                    _result = (DataTable)Session["GrnNumber"];
                }
                if (lblSearchType.Text == "ReqNumber")
                {
                    _result = (DataTable)Session["ReqNumber"];
                }
                if (lblSearchType.Text == "OtherLoc")
                {
                    _result = (DataTable)Session["OtherLoc"];
                }
                if (lblSearchType.Text == "OperationTeam")
                {
                    _result = (DataTable)Session["OperationTeam"];
                }
                if (lblSearchType.Text == "CourierNos")
                {
                    _result = (DataTable)Session["CourierNos"];
                }
                if (lblSearchType.Text == "CourierNosTo")
                {
                    _result = (DataTable)Session["CourierNosTo"];
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
                if (lblSearchType.Text == "DocStockNo")
                {
                    txtDocNo.Text = code;
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
                if (lblSearchType.Text == "Color")
                {
                    txtColor.Text = code;
                }
                if (lblSearchType.Text == "Routdetails")
                {
                    txtroute.Text = code;
                }
                if (lblSearchType.Text == "ApprovedDoc")
                {
                    txtappdocno.Text = code;
                }
                if (lblSearchType.Text == "ReqAppType")
                {
                    txtdoctypenew.Text = code;
                }
                if (lblSearchType.Text == "BondNumber")
                {
                    txtBondNumber.Text = code;
                    chkAllSupplier.Checked = false;
                }
                if (lblSearchType.Text == "GrnNumber")
                {
                    txtGrnNo.Text = code;
                    chkAllSupplier.Checked = false;
                }
                if (lblSearchType.Text == "ReqNumber")
                {
                    txtRequestNo.Text = code;
                    chkAllSupplier.Checked = false;
                }
                if (lblSearchType.Text == "OtherLoc")
                {
                    txtOtherloc.Text = code;
                    chkAllSupplier.Checked = false;
                }
                if (lblSearchType.Text == "OperationTeam")
                {
                    txtadmintm.Text = code;
                    chkAllSupplier.Checked = false;
                }
                if (lblSearchType.Text == "CourierNos")
                {
                    txtFromNo.Text = code;
                    chkCouAll.Checked = false;
                }
                if (lblSearchType.Text == "CourierNosTo")
                {
                    txtToNo.Text = code;
                    chkCouAll.Checked = false;
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
            try
            {
                lblSearchType.Text = "DocStockNo";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocStockNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchStockDocNo(para, null, null);
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
            txtItemStat.Enabled = !chkAllStat.Checked;
            txtItemStat.Text = "";
        }

        protected void chkAllDocType_CheckedChanged(object sender, EventArgs e)
        {
            txtDocType.Text = "";
        }

        protected void chkAllDocSubType_CheckedChanged(object sender, EventArgs e)
        {
            txtDocSubType.Text = "";
        }

        protected void chkAllDocNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDocNo.Text = "";
        }

        protected void chkAllDirNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDirection.Text = "";
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
                    ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
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
                    ucLoactionSearch.ClearText();
                    locationPanel.Enabled = false;
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
                        //DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);
                        DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, adminteam);
                        dgvLocation.DataSource = dt;
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
                        dgvLocation.DataSource = dt;
                        dgvLocation.DataBind();
                        //foreach (DataRow drow in dt.Rows)
                        //{
                        //    chklstbox.Items.Add(drow["PROFIT_CENTER"].ToString());
                        //}
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
            string item = "ITEM";
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
                textBox.Text = "";
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
                listBox.Items.Clear();
                listBox.Enabled = false;
            }
            txtFromDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            txtAsAt.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            txtExDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            BindYear();
            Loadselectdate();
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

        protected void rad02_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
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
            lbtn.Add(lbtnSeDocNo);

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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            chk.Add(chkWithCostWIP);

            //With Job
            // chk.Add(chkWithJob);

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
            Default();

            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtOtherloc.Enabled = true;

            txtItemStat.Enabled = false;
            txtSupplier.Enabled = false;
            txtColor.Enabled = false;
            txtappdocno.Enabled = false;
            txtdoctypenew.Enabled = false;
            frmtime.Enabled = false;
            totime.Enabled = false;
            chkWithServiceWIP.Enabled = false;
            txtDocNo.Enabled = false;

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            chkAllCat1.Checked = false;
            chkAllCat2.Checked = false;
            chkAllCat3.Checked = false;
            chkAllCat4.Checked = false;
            chkAllCat5.Checked = false;
            chkAllItemCode.Checked = false;
            chkAllBrand.Checked = false;
            chkAllModel.Checked = false;

            //lbtn.Add(lbtnSeCat1);
            //lbtn.Add(lbtnSeCat2);
            //lbtn.Add(lbtnSeCat3);
            //lbtn.Add(lbtnSeCat4);
            //lbtn.Add(lbtnSeCat5);
            //lbtn.Add(lbtnSeItemCode);
            //lbtn.Add(lbtSeBrand);
            //lbtn.Add(lbtnSeModel);
            //lbtn.Add(lbtnSeDocNo);

            //chk.Add(chkAllCat1);
            //chk.Add(chkAllCat2);
            //chk.Add(chkAllCat3);
            //chk.Add(chkAllCat4);
            //chk.Add(chkAllCat5);
            //chk.Add(chkAllItemCode);
            //chk.Add(chkAllBrand);
            //chk.Add(chkAllModel);

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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            chk.Add(chkWithCostWIP);

            //With Job
            // chk.Add(chkWithJob);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();

            ddlStatus.Enabled = true;
            chkprintRecord.Enabled = true;
            frmtime.Enabled = true;
            totime.Enabled = true;
            rbtnExpDte.Enabled = true;
            rbtnCreDte.Enabled = true;
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
            Default();

            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtOtherloc.Enabled = true;

            txtItemStat.Enabled = false;
            txtSupplier.Enabled = false;
            txtColor.Enabled = false;
            txtappdocno.Enabled = false;
            txtdoctypenew.Enabled = false;
            frmtime.Enabled = false;
            totime.Enabled = false;
            chkWithServiceWIP.Enabled = false;
            txtDocNo.Enabled = false;

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            chkAllCat1.Checked = false;
            chkAllCat2.Checked = false;
            chkAllCat3.Checked = false;
            chkAllCat4.Checked = false;
            chkAllCat5.Checked = false;
            chkAllItemCode.Checked = false;
            chkAllBrand.Checked = false;
            chkAllModel.Checked = false;

            //lbtn.Add(lbtnSeCat1);
            //lbtn.Add(lbtnSeCat2);
            //lbtn.Add(lbtnSeCat3);
            //lbtn.Add(lbtnSeCat4);
            //lbtn.Add(lbtnSeCat5);
            //lbtn.Add(lbtnSeItemCode);
            //lbtn.Add(lbtSeBrand);
            //lbtn.Add(lbtnSeModel);
            //lbtn.Add(lbtnSeDocNo);

            //chk.Add(chkAllCat1);
            //chk.Add(chkAllCat2);
            //chk.Add(chkAllCat3);
            //chk.Add(chkAllCat4);
            //chk.Add(chkAllCat5);
            //chk.Add(chkAllItemCode);
            //chk.Add(chkAllBrand);
            //chk.Add(chkAllModel);

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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            chk.Add(chkWithCostWIP);

            //With Job
            // chk.Add(chkWithJob);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();

            ddlStatus.Enabled = true;
            chkprintRecord.Enabled = true;
            frmtime.Enabled = true;
            totime.Enabled = true;
            rbtnExpDte.Enabled = true;
            rbtnCreDte.Enabled = true;
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
            Default();

            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtOtherloc.Enabled = true;

            txtItemStat.Enabled = false;
            txtSupplier.Enabled = false;
            txtColor.Enabled = false;
            txtappdocno.Enabled = false;
            txtdoctypenew.Enabled = false;
            frmtime.Enabled = false;
            totime.Enabled = false;
            chkWithServiceWIP.Enabled = false;
            txtDocNo.Enabled = false;

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtCat4);
            //txt.Add(txtCat5);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            chkAllCat1.Checked = false;
            chkAllCat2.Checked = false;
            chkAllCat3.Checked = false;
            chkAllCat4.Checked = false;
            chkAllCat5.Checked = false;
            chkAllItemCode.Checked = false;
            chkAllBrand.Checked = false;
            chkAllModel.Checked = false;

            //lbtn.Add(lbtnSeCat1);
            //lbtn.Add(lbtnSeCat2);
            //lbtn.Add(lbtnSeCat3);
            //lbtn.Add(lbtnSeCat4);
            //lbtn.Add(lbtnSeCat5);
            //lbtn.Add(lbtnSeItemCode);
            //lbtn.Add(lbtSeBrand);
            //lbtn.Add(lbtnSeModel);
            //lbtn.Add(lbtnSeDocNo);

            //chk.Add(chkAllCat1);
            //chk.Add(chkAllCat2);
            //chk.Add(chkAllCat3);
            //chk.Add(chkAllCat4);
            //chk.Add(chkAllCat5);
            //chk.Add(chkAllItemCode);
            //chk.Add(chkAllBrand);
            //chk.Add(chkAllModel);

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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            chk.Add(chkWithCostWIP);

            //With Job
            // chk.Add(chkWithJob);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadselectdate();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMonth.Text))
            {
                displayMessage("Select the year");

                return;
            }

            int month = ddlMonth.SelectedIndex + 1;
            int year = Convert.ToInt32(ddlYear.Text);

            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);

            txtToDate.Text = lastDay.ToString("dd/MMM/yyyy");

            DateTime dtFrom = new DateTime(Convert.ToInt32(ddlYear.Text), month, 1);
            txtFromDate.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
            //Loadselectdate();
        }

        private void Loadselectdate()
        {
            DateTime now = DateTime.Now;

            int d = (int)System.DateTime.Now.Day;

            txtFromDate.Text = d + "/" + ddlMonth.SelectedValue + "/" + ddlYear.Text;
            txtToDate.Text = d + "/" + ddlMonth.SelectedValue + "/" + ddlYear.Text;

        }

        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            DataTable _result = CHNLSVC.General.GetMainCategoryDetail(txtCat1.Text);
            if (_result.Rows.Count == 0)
            {
                displayMessage("Please enter valid cat1.");
                txtCat1.Text = string.Empty;
                chkAllCat1.Checked = true;
                return;
            }
            chkAllCat1.Checked = false;
        }

        protected void txtCat2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                displayMessage("Please enter cat1.");
                txtCat1.Text = string.Empty;
                return;
            }
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.Sales.GetItemSubCate2(txtCat1.Text, txtCat2.Text);
            if (_result.Rows.Count == 0)
            {
                displayMessage("Please enter valid cat2.");
                txtCat2.Text = string.Empty;
                chkAllCat2.Checked = true;
                return;
            }
            chkAllCat2.Checked = false;
        }

        protected void txtCat3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                displayMessage("Please enter cat1.");
                txtCat3.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(txtCat2.Text))
            {
                displayMessage("Please enter cat2.");
                txtCat3.Text = string.Empty;
                return;
            }
            DataTable _result = CHNLSVC.Sales.GetItemSubCate3(txtCat1.Text, txtCat2.Text, txtCat3.Text);
            if (_result.Rows.Count == 0)
            {
                displayMessage("Please enter valid cat3.");
                txtCat3.Text = string.Empty;
                chkAllCat3.Checked = true;
                return;
            }
            chkAllCat3.Checked = false;
        }

        protected void txtCat4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat3.Text))
            {
                displayMessage("Please enter cat3.");
                txtCat4.Text = string.Empty;
                return;
            }
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, "CODE", txtCat4.Text);
            if (_result.Rows.Count == 0)
            {
                displayMessage("Please enter valid cat3.");
                txtCat4.Text = string.Empty;
                chkAllCat4.Checked = true;
                return;
            }
            chkAllCat4.Checked = false;
        }

        protected void txtCat5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCat4.Text))
            {
                displayMessage("Please enter cat4.");
                txtCat5.Text = string.Empty;
                return;
            }
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, "CODE", txtCat5.Text);
            if (_result.Rows.Count == 0)
            {
                displayMessage("Please enter valid cat4.");
                txtCat5.Text = string.Empty;
                chkAllCat5.Checked = true;
                return;
            }
            chkAllCat5.Checked = false;
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            MasterItem _itemdetail = new MasterItem();
            _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text);
            if (_itemdetail == null)
            {
                displayMessage("Please enter valid item.");
                txtItemCode.Text = string.Empty;
                chkAllItemCode.Checked = true;
                return;
            }
            chkAllItemCode.Checked = false;
        }

        protected void txtBrand_TextChanged(object sender, EventArgs e)
        {
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, "CODE", txtBrand.Text);
            if (_result.Rows.Count == 0)
            {
                displayMessage("Please enter valid Brand.");
                txtBrand.Text = string.Empty;
                chkAllBrand.Checked = true;
                return;
            }
            chkAllBrand.Checked = false;
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, "CODE", txtModel.Text);
            if (_result.Rows.Count == 0)
            {
                displayMessage("Please enter valid Model.");
                txtModel.Text = string.Empty;
                chkAllModel.Checked = true;
                return;
            }
            chkAllModel.Checked = false;
        }


        protected void btnBondNumber_Click(object sender, EventArgs e)
        {
            try
            {

                lblSearchType.Text = "BondNumber";
                DataTable _result = CHNLSVC.CommonSearch.Get_BondNumber("", null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["BondNumber"] = _result;
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

        protected void btnGrnNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "GrnNumber";
                DataTable _result = CHNLSVC.CommonSearch.Get_GrnNumber(Session["UserCompanyCode"].ToString(), "", null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["GrnNumber"] = _result;
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

        protected void btnRequestNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ReqNumber";
                DataTable _result = CHNLSVC.CommonSearch.Get_ReqNumber(Session["UserCompanyCode"].ToString(), "", null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["ReqNumber"] = _result;
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

        protected void lbtnOtherLocation_Click(object sender, EventArgs e)
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void btmadmintm_Click(object sender, EventArgs e)
        {

            try
            {
                lblSearchType.Text = "OperationTeam";
                DataTable _result = CHNLSVC.CommonSearch.Get_Operationteam(Session["UserCompanyCode"].ToString(), "", null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    BindUCtrlDDLData(_result);
                    Session["OperationTeam"] = _result;
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

        protected void lbtnroute_Click(object sender, EventArgs e)
        {

            if (ucLoactionSearch.txtCompany.Text != "")
            {
                try
                {
                    lblSearchType.Text = "Routdetails";
                    Session["Routdetails"] = null;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RoutDet);
                    DataTable _result = CHNLSVC.CommonSearch.SearchRoutDetailsnew(para, null, null);
                    dgvResultItem.DataSource = null;
                    if (_result.Rows.Count > 0)
                    {
                        dgvResultItem.DataSource = _result;
                        Session["Routdetails"] = _result;
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
            else
            {
                displayMessage("Please Select Company");
            }


        }

        protected void chkallroute_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnroute_Click(object sender, EventArgs e)
        {
            dgvRoute.DataSource = null;
            dgvRoute.DataBind();
            if (ucLoactionSearch.txtCompany.Text != "")
            {
                DataTable data = CHNLSVC.CustService.GetRouteDetails(ucLoactionSearch.txtCompany.Text);
                dgvRoute.DataSource = data;
                dgvRoute.DataBind();
            }
            else
            {
                displayMessage("Please Select Company");
            }
        }

        protected void lbtnrouteAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvRoute.Rows)
                {
                    // Label lblLocation = (Label)row.FindControl("lblLocation");
                    CheckBox chkRouteDet = (CheckBox)row.FindControl("chkRouteDet");
                    chkRouteDet.Checked = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnrouteNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvRoute.Rows)
                {
                    // Label lblLocation = (Label)row.FindControl("lblLocation");
                    CheckBox chkRouteDet = (CheckBox)row.FindControl("chkRouteDet");
                    chkRouteDet.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnrouteClear_Click(object sender, EventArgs e)
        {
            dgvRoute.DataSource = null;
            dgvRoute.DataBind();
        }

        protected void lbtnColor_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Color";
                Session["Color"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Color);
                DataTable _result = CHNLSVC.CommonSearch.Get_Color_All(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["Color"] = _result;
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

        protected void lbtnappdocno_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ApprovedDoc";
                Session["ApprovedDoc"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovedDoc);
                DataTable _result = CHNLSVC.CommonSearch.SearchApprovedDocNo(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ApprovedDoc"] = _result;
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

        protected void lbtndoctype_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ReqAppType";
                Session["ReqAppType"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReqAppType);
                DataTable _result = CHNLSVC.CommonSearch.Get_ReqApprType(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ReqAppType"] = _result;
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

            chkistime.Enabled = true;
            frmtime.Enabled = false;
            totime.Enabled = false;
            txt.Add(txtColor);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            if (txtDocNo.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocStockNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchStockDocNo(para, "STOCK DOC", txtDocNo.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["STOCK DOC"].ToString()))
                    {
                        if (txtDocNo.Text == row["STOCK DOC"].ToString())
                        {
                            b2 = true;
                            toolTip = row["STOCK DOC"].ToString();
                            return;
                        }
                    }
                }
                txtDocNo.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Loading Place !!!')", true);
                    txtDocNo.Text = "";
                    txtDocNo.Focus();
                    return;
                }
            }
            else
            {
                txtDocNo.ToolTip = "";
            }
        }



        protected void radmodwisesumm_CheckedChanged(object sender, EventArgs e)
        {
            chkwithbincd.Enabled = true;
        }

        protected void radreqwisesumm_CheckedChanged(object sender, EventArgs e)
        {
            chkwithbincd.Enabled = false;
        }

        protected void radrequestwise_CheckedChanged(object sender, EventArgs e)
        {
            chkwithbincd.Enabled = false;
        }

        protected void radmodwise_CheckedChanged(object sender, EventArgs e)
        {
            chkwithbincd.Enabled = false;
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(txtToDate.Text.ToString()), Convert.ToDateTime(txtFromDate.Text.ToString())) < 0)
            {
                displayMessage("From date cannot exceed to date");

                txtToDate.Text = "";
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

        protected void chkPending_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkPending.Checked)
            //{
            //    chkApproved.Checked = false;
            //}
            //else
            //{

            //}
        }

        protected void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void rad06_CheckedChanged(object sender, EventArgs e)
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
            txt.Add(txtOtherloc);
            lbtn.Add(btnOtherLocation);
            ddl.Add(ddlStatus);

            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllCat4.Checked = true;
            chkAllCat5.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;
            txt.Add(txtColor);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad07_CheckedChanged(object sender, EventArgs e)
        {
            ddlReportType.Enabled = true;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            chkallroute.Enabled = true;
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add(new ListItem("Select Type", ""));
            ddlReportType.Items.Add(new ListItem("MRNA", "M"));
            ddlReportType.Items.Add(new ListItem("SOA", "S"));
        }

        protected void chkistime_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkistime.Checked)
            {
                frmtime.Enabled = false;
                totime.Enabled = false;
            }
            else
            {
                frmtime.Enabled = true;
                totime.Enabled = true;
            }
        }

        protected void txtroute_TextChanged(object sender, EventArgs e)
        {
            chkallroute.Checked = false;
        }

        protected void rad08_CheckedChanged(object sender, EventArgs e)
        {
            BindTransService();
            lbtnSeDocNo.Visible = false;
            ddlServiceBy.Enabled = true;

            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;

            txtFromNo.Enabled = true;
            lknCourier.Enabled = true;
            txtToNo.Enabled = true;
            lknCourierTo.Enabled = true;
            chkCouAll.Enabled = true;
        }
        private void BindTransService()
        {
            _tPartys = new List<TransportParty>();
            _tParty = new TransportParty() { Rtnp_tnpt_seq = 2, Mbe_com = Session["UserCompanyCode"].ToString() };
            _tPartys = CHNLSVC.General.GET_TRANSPORT_PTY(_tParty);
            while (ddlServiceBy.Items.Count > 1)
            {
                ddlServiceBy.Items.RemoveAt(1);
            }
            ddlServiceBy.DataSource = _tPartys.Where(c => c.Mbe_name != string.Empty && c.Mbe_name != null).OrderBy(c => c.Mbe_name);
            ddlServiceBy.DataTextField = "mbe_name";
            ddlServiceBy.DataValueField = "rtnp_pty_cd";
            ddlServiceBy.DataBind();
            //if (_tPartys.Rtnp_tnpt_seq.Count = 0)
            //{
            //    ddlServiceBy.DataSource = _tPartys.Where(c => c.Mbe_name == string.Empty && c.Mbe_name == null).OrderBy(c => c.Mbe_name);
            //    ddlServiceBy.DataTextField = "mbe_name";
            //    ddlServiceBy.DataValueField = "rtnp_pty_cd";
            //    ddlServiceBy.DataBind();
            //}
            //else
            //{
            //    ddlServiceBy.DataSource = _tPartys.Where(c => c.Mbe_name != string.Empty && c.Mbe_name != null).OrderBy(c => c.Mbe_name);
            //    ddlServiceBy.DataTextField = "mbe_name";
            //    ddlServiceBy.DataValueField = "rtnp_pty_cd";
            //    ddlServiceBy.DataBind();
            //}
        }

        protected void lknCourier_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CourierNos";
                Session["CourierNos"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CourierNo);
                DataTable _result = CHNLSVC.CommonSearch.GetCourierNos(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CourierNos"] = _result;
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

        protected void lknCourierTo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CourierNosTo";
                Session["CourierNosTo"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CourierNo);
                DataTable _result = CHNLSVC.CommonSearch.GetCourierNos(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["CourierNosTo"] = _result;
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
    }
}