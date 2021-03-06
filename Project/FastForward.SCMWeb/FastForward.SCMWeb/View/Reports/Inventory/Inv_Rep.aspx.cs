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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace FastForward.SCMWeb.View.Reports.Inventory 
{
    public partial class Inv_Rep : BasePage
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
                BindYear();
                LoadEmptyGrid();
                BindCompany(Session["UserID"].ToString());
                ucLoactionSearch.Company = Session["UserCompanyCode"].ToString();
                txtFromDate.Text = (DateTime.Now.Date.AddMonths(-1)).ToString("dd/MMM/yyyy");
                txtToDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtAsAt.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                txtExDate.Text = (DateTime.Now.Date).AddMonths(1).ToString("dd/MMM/yyyy");
                hdfCurrentDate.Value = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                locationPanel.Enabled = !chkAllCompany.Checked;
                dgvLocation.DataSource = null;
                dgvLocation.DataBind();
                txtAgeFrom.Text = "0";
                txtAgeTo.Text = "9999";
                dgvAdminTeam.Columns[1].Visible = false;
                AddEnableCntr();
                DesableAllControler();
                //download button
                //  btnDownloadfile.Visible = false;
                // Session["btnDownloadfile"] = "false";

                //  Loadselectdate();
                // Default();
                Session["AdminTMList"] = "";


            }
            else
            {


            }
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
            // _rList.Add(rad07);
            // _rList.Add(rad08);
            // _rList.Add(rad09);
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
        //    _rList.Add(rad20);
            _rList.Add(rad21);
            _rList.Add(rad51);
            _rList.Add(rad22);
            _rList.Add(rad23);
         //   _rList.Add(rad24);
            _rList.Add(rad25);
            _rList.Add(rad26);
            _rList.Add(rad27);
            //  _rList.Add(rad28);
            _rList.Add(rad29);
            _rList.Add(rad30);
            _rList.Add(rad31);
            _rList.Add(rad32);
            _rList.Add(rad33);
            _rList.Add(rad34);
            _rList.Add(rad35);
            _rList.Add(rad36);
            _rList.Add(rad38);
            _rList.Add(rad37);
            _rList.Add(rad39);
            _rList.Add(rad40);
            _rList.Add(rad41);
            _rList.Add(rad42);
            _rList.Add(rad43);
            _rList.Add(rad44);
            _rList.Add(rad45);
            _rList.Add(rad46);
            _rList.Add(rad47);
            _rList.Add(rad49);
            _rList.Add(rad52);
            _rList.Add(rad53);
            _rList.Add(rad54);
            _rList.Add(rad55);
            _rList.Add(rad56);
            _rList.Add(rad57);
            _rList.Add(rad58);
            _rList.Add(rad59);
            _rList.Add(rad60);
            _rList.Add(rad61);
            _rList.Add(rad62);           
            _rList.Add(rbpdf);
            _rList.Add(rbexel);
            _rList.Add(rbexeldata);
            _rList.Add(rbword);
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
            _enbleTxtList = _lTxt;

            List<LinkButton> _lBtn = new List<LinkButton>();
            _lBtn.Add(btnDownloadfile);
            _lBtn.Add(lbtnView);
            _lBtn.Add(lbtnClear);
            _lBtn.Add(lbtnSearch);

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

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        string _opt = "";
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (rad40.Checked | rad37.Checked | rad33.Checked | rad55.Checked | rad57.Checked)
                {

                }
                else
                {
                    if (rad32.Checked == false && rad31.Checked == false &&  rad47.Checked==false)
                        if ((dgvLocation.Rows.Count == 0) && (ucLoactionSearch.txtLocation.Text == ""))
                        {
                            displayMessage("Please add location deatils");
                            return;
                        }
                }


                if ((ucLoactionSearch.txtCompany.Text == "") && !chkAllCompany.Checked)
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

                BaseCls.GlbReportCompName = com_desc;
                BaseCls.GlbReportCompanies = com_cds;

                //set all common parameters
                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;
                BaseCls.GlbReportSupplier = txtSupplier.Text.ToString();

                BaseCls.GlbReportCompCode = Session["UserCompanyCode"].ToString();
                BaseCls.GlbReportComp = Session["UserCompanyCode"].ToString();
                BaseCls.GlbReportProfit = Session["UserDefLoca"].ToString();

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


                BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                BaseCls.GlbReportModel = vModel == "" ? txtModel.Text == "" ? txtModel.Text : "^" + txtModel.Text + "$" : vModel;
                BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtCat1.Text == "" ? txtCat1.Text : "^" + txtCat1.Text + "$" : vItemCat1;
                BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtCat2.Text == "" ? txtCat2.Text : "^" + txtCat2.Text + "$" : vItemCat2;
                BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtCat3.Text == "" ? txtCat3.Text : "^" + txtCat3.Text + "$" : vItemCat3;
                BaseCls.GlbReportItemCat4 = vItemCat4 == "" ? txtCat4.Text == "" ? txtCat4.Text : "^" + txtCat4.Text + "$" : vItemCat4;
                BaseCls.GlbReportItemCat5 = vItemCat5 == "" ? txtCat5.Text == "" ? txtCat5.Text : "^" + txtCat5.Text + "$" : vItemCat5;
                BaseCls.GlbReportItemStatus = txtItemStat.Text;
                BaseCls.GlbReportDoc2 = chkWithGit.Checked == true ? "Y" : "N";
                BaseCls.GlbReportJobStatus = chkWithJob.Checked == true ? "Y" : "N";
                BaseCls.GlbReportWithRCC = chkWithServiceWIP.Checked == true ? "Y" : "N";
                BaseCls.GlbReportWithCost = chkWithCostWIP.Checked == true ? Convert.ToInt16(1) : Convert.ToInt16(0);
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirection.Text;
                BaseCls.GlbReportDocSubType = txtDocSubType.Text;



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
                _invRepPara._GlbReportExpDate = Convert.ToDateTime(txtExDate.Text).Date;
                _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString();
                _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportProfit = ucLoactionSearch.txtLocation.Text == "" ? "All Locations based on user rights" : ucLoactionSearch.txtLocation.Text;
                _invRepPara._GlbReportDirection = Session["GlbReportDirection"].ToString();
                _invRepPara._GlbUserID = Session["UserID"].ToString();
                _invRepPara._GlbReportWithJob = "N";
                string _export = "N";
                _opt = "";

                string url = "";
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

                if (rad01.Checked == true)  // curent stock balance items
                {//kapila 12/12/2015
                    _opt = "rad01";
                    _isSelected = true;
                    _invRepPara._GlbReportWithCost = 0;
                    _invRepPara._GlbReportWithSerial = 0;
                    _invRepPara._GlbReportJobStatus = "N";
                    _invRepPara._GlbReportType = "CURR";
                    Session["GlbReportType"] = "CURR";
                    _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;

                    if (radcatwise.Checked == true)
                    {
                        _invRepPara._GlbReportName = "Stock_BalanceCatWise.rpt";
                        Session["GlbReportName"] = "Stock_BalanceCatWise.rpt";
                    }
                    else
                    {
                        _invRepPara._GlbReportName = "Stock_Balance.rpt";
                        Session["GlbReportName"] = "Stock_Balance.rpt";
                    }

                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportHeading = "CURRENT INVENTORY BALANCE (ITEMS)";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Current_Stock_Balance(_invRepPara);


                    if (_invRepPara._GlbReportName == "Stock_Balance.rpt")
                        PrintPDF(targetFileName, obj._curStkBal);
                    else
                        PrintPDF(targetFileName, obj._curStkBalCat);

                    //   string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //                  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


                }
                if (rad02.Checked == true)   // curent stock balance serials
                {//kapila 12/12/2015
                    _opt = "rad02";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportWithCost = 0;
                    _invRepPara._GlbReportWithSerial = 1;
                    _invRepPara._GlbReportJobStatus = "N";
                    _invRepPara._GlbReportType = "CURR";

                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;

                    _invRepPara._GlbReportHeading = "CURRENT INVENTORY BALANCE (WITH SERIAL)";
                    Session["GlbReportName"] = "Stock_Balance.rpt";
                    _invRepPara._GlbReportName = "Stock_Balance.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    if (rbpdf.Checked)
                    {
                        clsInventory obj = new clsInventory();
                        obj.Current_Stock_Balance(_invRepPara);
                        PrintPDF(targetFileName, obj._curStkBal);
                    }
                    else if (rbexel.Checked)
                    {
                        targetFileName = "";
                        string _err = "";
                        string _filePath = CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM_new(_invRepPara._GlbUserID, _invRepPara._GlbReportChannel, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5, _invRepPara._GlbReportWithCost, _invRepPara._GlbReportWithSerial, Session["UserCompanyCode"].ToString(), ucLoactionSearch.ProfitCenter, "N", "N", "N", out _err);

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
                    }

                }
                if (rad03.Checked == true)  // Stock Balance as at - Items (pkg_scm_reports.PROC_INVENTORY_BALANCE_ASAT)
                {//kapila 12/12/2015
                    _opt = "rad03";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    if (chkWithCostWIP.Checked == true)
                    {
                        _invRepPara._GlbReportHeading = "INVENTORY BALANCE AS AT DATE (WITH COST)";
                        _invRepPara._GlbReportWithCost = 1;
                    }
                    else
                    {
                        _invRepPara._GlbReportHeading = "INVENTORY BALANCE AS AT DATE";
                        _invRepPara._GlbReportWithCost = 0;
                    }
                    if (CheckBox1.Checked)
                        _invRepPara._GlbReportWithStatus = 1;
                    else
                        _invRepPara._GlbReportWithStatus = 0;

                    _invRepPara._GlbReportWithSerial = 0;
                    _invRepPara._GlbReportJobStatus = "N";
                    Session["GlbReportName"] = "Stock_BalanceCost.rpt";
                    _invRepPara._GlbReportName = "Stock_BalanceCost.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    if (rbpdf.Checked)
                    {
                        clsInventory obj = new clsInventory();
                        obj.inventoryBalanceWithCost(_invRepPara);
                        PrintPDF(targetFileName, obj._invBalCst);
                    }
                    else if (rbexel.Checked)
                    {
                        targetFileName = "";
                        string _err = "";
                        string _filePath = CHNLSVC.MsgPortal.inventoryBalance_Asat(_invRepPara._GlbUserID, _invRepPara._GlbReportChannel, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3,_invRepPara._GlbReportItemCat4,_invRepPara._GlbReportItemCat5, _invRepPara._GlbReportWithCost, _invRepPara._GlbReportAsAtDate, _invRepPara._GlbReportWithSerial, Session["UserCompanyCode"].ToString(), ucLoactionSearch.ProfitCenter, "N", "N", "N", out _err);

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
                    }

                    //clsInventory obj = new clsInventory();
                    //obj.inventoryBalanceWithCost(_invRepPara);
                    //PrintPDF(targetFileName, obj._invBalCst);
                    // Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad04.Checked == true)  // Stock Balance as at - serials
                {//kapila 12/12/2015
                    _opt = "rad04";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    if (chkWithCostWIP.Checked == true)
                        _invRepPara._GlbReportWithCost = 1;
                    else
                        _invRepPara._GlbReportWithCost = 0;

                    if (CheckBox1.Checked)
                        _invRepPara._GlbReportWithStatus = 1;
                    else
                        _invRepPara._GlbReportWithStatus = 0;

                    _invRepPara._GlbReportWithSerial = 1;
                    _invRepPara._GlbReportJobStatus = "N";
                    _invRepPara._GlbReportHeading = "INVENTORY BALANCE AS AT DATE (WITH SERIAL)";
                    Session["GlbReportName"] = "Stock_BalanceSerialAs_at.rpt";
                    _invRepPara._GlbReportName = "Stock_BalanceSerialAs_at.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    if (rbpdf.Checked)
                    {
                        clsInventory obj = new clsInventory();
                        obj.inventoryBalanceSerial_Asat(_invRepPara);
                        PrintPDF(targetFileName, obj._invBalSrlAsat);
                    }
                    else if (rbexel.Checked)
                    {
                        targetFileName = "";
                        string _err = "";
                        string _filePath = CHNLSVC.MsgPortal.inventoryBalanceSerial_Asat(_invRepPara._GlbUserID, _invRepPara._GlbReportChannel, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5, _invRepPara._GlbReportWithCost,_invRepPara._GlbReportAsAtDate, _invRepPara._GlbReportWithSerial, Session["UserCompanyCode"].ToString(), ucLoactionSearch.ProfitCenter, "N", "N", "N", out _err);

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
                    }

                }
                if (rad05.Checked == true)  // Movement Audit Trial Report - items
                {//kapila 12/12/2015
                    _opt = "rad05";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportIsCostPrmission = 0;
                    _invRepPara._GlbReportWithCost = chkWithCostWIP.Checked == true ? Convert.ToInt16(1) : Convert.ToInt16(0);
                    _invRepPara._GlbReportDoc = txtDocNo.Text;

                    if (chkNor.Checked == true)
                    {
                        _invRepPara._GlbReportHeading = "Movement Audit Trial Report (Items)";
                        if (chkWithCostWIP.Checked == true)
                        {
                            _invRepPara._GlbReportWithCost = Convert.ToInt16(1);
                            Session["GlbReportName"] = "Movement_audit_trial_cost.rpt";
                            _invRepPara._GlbReportName = "Movement_audit_trial_cost.rpt";
                        }
                        else
                        {
                            _invRepPara._GlbReportWithCost = Convert.ToInt16(0);
                            Session["GlbReportName"] = "Movement_audit_trial.rpt";
                            _invRepPara._GlbReportName = "Movement_audit_trial.rpt";
                        }
                    }

                    else if (chkSumm.Checked == true)
                    {
                        _invRepPara._GlbReportHeading = "Movement Audit Trial Summary Report";
                        Session["GlbReportName"] = "Movement_audit_trial_summary.rpt";
                        _invRepPara._GlbReportName = "Movement_audit_trial_summary.rpt";
                    }
                    else if (chkDet.Checked == true)
                    {
                        _invRepPara._GlbReportHeading = "Movement Audit Trial Detail Report";
                        Session["GlbReportName"] = "Movement_audit_trial_det.rpt";
                        _invRepPara._GlbReportName = "Movement_audit_trial_det.rpt";
                    }
                    else if (chklist.Checked == true)
                    {
                        _invRepPara._GlbReportHeading = "Movement Audit Trial Listing Report";
                        Session["GlbReportName"] = "Movement_audit_trial_sum.rpt";
                        _invRepPara._GlbReportName = "Movement_audit_trial_sum.rpt";
                    }
                    _invRepPara._GlbReportIsSummary = Convert.ToInt16(chkSumm.Checked);
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;

                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Movement_Audit_Trial(_invRepPara);

                    if (Session["GlbReportName"] == "Movement_audit_trial_sum.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialSum);
                    else if (Session["GlbReportName"] == "Movement_audit_trial_cost.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialCost);
                    else if (Session["GlbReportName"] == "Movement_audit_trial.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrial);
                    else if (Session["GlbReportName"] == "Movement_audit_trial_ser.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialSer);
                    else if (Session["GlbReportName"] == "Movement_audit_trial_ser_cost.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialSerCost);
                    else if (Session["GlbReportName"] == "Movement_audit_trial_summary.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialSummary);
                    else if (Session["GlbReportName"] == "Movement_audit_trial_det.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialDet);
                    else if (Session["GlbReportName"] == "Movement_audit_trial_sum.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialSum);
                    //   Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad06.Checked == true)  // Movement Audit Trial Report - Serials
                {//kapila 12/12/2015
                    _opt = "rad06";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();
                    _invRepPara._GlbReportDoc = txtDocNo.Text;

                    _invRepPara._GlbReportHeading = "Movement Audit Trial Report (Serial)";
                    if (chkWithCostWIP.Checked == true)
                    {
                        _invRepPara._GlbReportWithCost = Convert.ToInt16(1);
                        Session["GlbReportName"] = "Movement_audit_trial_ser_cost.rpt";
                        _invRepPara._GlbReportName = "Movement_audit_trial_ser_cost.rpt";
                    }
                    else
                    {
                        _invRepPara._GlbReportWithCost = Convert.ToInt16(0);
                        Session["GlbReportName"] = "Movement_audit_trial_ser.rpt";
                        _invRepPara._GlbReportName = "Movement_audit_trial_ser.rpt";
                    }

                    // _invRepPara._GlbReportIsSummary = Convert.ToInt16(chkSumm.Checked);
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;

                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Movement_Audit_Trial(_invRepPara);

                    if (Session["GlbReportName"] == "Movement_audit_trial_ser.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialSer);
                    else if (Session["GlbReportName"] == "Movement_audit_trial_ser_cost.rpt")
                        PrintPDF(targetFileName, obj._moveAuditTrialSerCost);

                    //   Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }

                //if (rad07.Checked == true)   // age report items
                //{//kapila 16/12/2015
                //    _isSelected = true;
                //    Session["GlbReportType"] = "";
                //    _invRepPara._GlbReportWithSerial = 0;

                //    set_GroupOrder();

                //    _invRepPara._GlbReportHeading = "AGE REPORT (ITEMS)";
                //    Session["GlbReportName"] = "StockAge.rpt";
                //    Response.Redirect("InventoryReportViewer.aspx", false);

                //}
                //if (rad08.Checked == true)   // age report serials
                //{//kapila 16/12/2015
                //    _isSelected = true;
                //    Session["GlbReportType"] = "";
                //    _invRepPara._GlbReportWithSerial = 1;

                //    set_GroupOrder();

                //    _invRepPara._GlbReportHeading = "AGE REPORT (SERIALS)";
                //    Session["GlbReportName"] = "StockBalanceWithSerialAge.rpt";
                //    Response.Redirect("InventoryReportViewer.aspx", false);

                //}

                if (rad10.Checked == true)
                {
                    //Response.Redirect("~/Error.aspx");
                    //return;
                    _opt = "rad10";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();
                    _invRepPara._GlbReportDoc = txtDocNo.Text;

                    //_invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString(); // Commented by Udesh - 19/Oct/2018
                    //_invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();     // Commented by Udesh - 19/Oct/2018

                    //==== Added by Udesh - 19/Oct/2018======
                    foreach (GridViewRow gr in this.dgvCompany.Rows)
                    {
                        CheckBox check = (CheckBox)gr.FindControl("chkCompanyCode");
                        if (check.Checked == true)
                        {
                            Label companyName = (Label)gr.FindControl("lblCode");
                            _invRepPara._GlbReportCompCode = companyName.Text;
                            _invRepPara._GlbReportCompName = this.dgvCompany.Rows[gr.RowIndex].Cells[2].Text;
                            _invRepPara._GlbReportComp = companyName.Text;
                            break;
                        }
                    }
                    //=========================================

                    _invRepPara._GlbReportHeading = "Purchase Order Summery - Summery Report";
                    if (radioLocal.Checked)
                    {
                        Session["PurchaseType"] = "L";
                    }
                    else if (radioImport.Checked)
                    {
                        Session["PurchaseType"] = "I";
                    }
                    else
                    {
                        Session["PurchaseType"] = "B";
                    }


                    _invRepPara._GlbEntryType = Session["PurchaseType"].ToString();
                    Session["GlbReportName"] = "rptPurchaseorderSummery_Summery.rpt";
                    _invRepPara._GlbReportName = "rptPurchaseorderSummery_Summery.rpt";
                    Session["InvReportPara"] = _invRepPara;


                    //Udesh 16-Oct-2018
                    if (chkExportExcel.Checked == true)
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;

                        _filePath = CHNLSVC.MsgPortal.getPurchaseOrderNewSummeryExcel(_invRepPara._GlbReportComp, "  ", _invRepPara._GlbEntryType, _invRepPara._GlbReportSupplier, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportDocType, _invRepPara._GlbReportDoc, _invRepPara._GlbUserID, out _error);
                        
                        if (!string.IsNullOrEmpty(_error))
                        {
                            lbtnView.Enabled = true;
                            if (_error.Contains("because it is being used by another process"))
                            {
                                displayMessage("Excel file has being used by another process. Please close the excel file.");
                                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alertMessage", "alert('Excel file has being used by another process. Please close the excel file.')", true);
                                
                                //Response.Redirect("~/Error.aspx?Error=" + "Excel file has being used by another process.Please close the excel file." + "");
                                return;
                            }
                            Response.Redirect("~/Error.aspx?Error=" + _error + "");
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            lbtnView.Enabled = true;
                            displayMessage("The excel file path cannot identify. Please contact IT Dept");
                            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alertMessage", "alert('The excel file path cannot identify. Please contact IT Dept')", true);
                            //Response.Redirect("~/Error.aspx?Error=" + "The excel file path cannot identify. Please contact IT Dept" + "");
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
                        clsInventory obj = new clsInventory();
                        obj.purchaseOrderSummery(_invRepPara);
                        PrintPDF(targetFileName, obj._purOrdSumm_summ);

                    // Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                }

                if (rad11.Checked == true)
                {
                    _opt = "rad11";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    update_PC_List_RPTDB();
                    _invRepPara._GlbReportDoc = txtDocNo.Text;
                    _invRepPara._GlbReportHeading = "Purchase Order Summery - Detail Report";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    Session["GlbReportName"] = "rptPurchaseorderSummery_Detail.rpt";
                    _invRepPara._GlbReportName = "rptPurchaseorderSummery_Detail.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    if (radioLocal.Checked)
                    {
                        Session["PurchaseType"] = "L";
                    }
                    else if (radioImport.Checked)
                    {
                        Session["PurchaseType"] = "I";
                    }
                    else
                    {
                        Session["PurchaseType"] = "B";
                    }


                    _invRepPara._GlbEntryType = Session["PurchaseType"].ToString();

                    if (chkExportExcel.Checked == true)
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;

                        _filePath = CHNLSVC.MsgPortal.getpurchaseOrderSummeryExcel(_invRepPara._GlbReportComp, "  ", _invRepPara._GlbEntryType, _invRepPara._GlbReportSupplier, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportDocType, _invRepPara._GlbReportDoc, _invRepPara._GlbUserID, out _error);
                        if (!string.IsNullOrEmpty(_error))
                        {
                            lbtnView.Enabled = true;
                            //Response.Redirect("~/Error.aspx?Error=" + _error + "");
                            displayMessage(_error);
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            lbtnView.Enabled = true;
                            // Response.Redirect("~/Error.aspx?Error=" + "The excel file path cannot identify. Please contact IT Dept" + "");
                            displayMessage("The excel file path cannot identify. Please contact IT Dept");
                            return;
                        }



                       _copytoLocal(_filePath);
                        displayMessage("Report Generated. Please Download the Excel File from <Download Excel> link.");
                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                        //Process p = new Process();
                        //p.StartInfo = new ProcessStartInfo(_filePath);
                        //p.Start();

                        //Response.Redirect("~/Error.aspx?" + "Export Completed" + "");
                        displayMessage("Export Completed");
                    }
                    else
                    {
                        clsInventory obj = new clsInventory();
                        obj.purchaseOrderSummery_Detail(_invRepPara);
                        PrintPDF(targetFileName, obj._purOrdSumm_dtl);
                    }
                    // Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }

                if (rad12.Checked == true)
                {//Sanjeewa 2016-06-09 LOPO Cost Detail Report
                    _opt = "rad12";
                    _isSelected = true;
                    update_PC_List_RPTDB();

                    string _err = "";
                    _invRepPara._GlbReportDoc = "ABANS";
                    _invRepPara._GlbReportDoc2 = "A";

                    string _filePath = CHNLSVC.MsgPortal.LOPOCostDetailsReport(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate,
                        _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, _invRepPara._GlbReportDoc, _invRepPara._GlbReportDoc2, out _err);

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
                }

                if (rad13.Checked == true)
                {
                    _opt = "rad13";
                    _isSelected = true;
                    update_PC_List_RPTDB();
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportDoc = txtDocNo.Text;
                    _invRepPara._GlbLocation = BaseCls.GlbReportProfit;
                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportHeading = "PO-GRN Pending";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    Session["GlbReportName"] = "rptPOGRNPending.rpt";
                    _invRepPara._GlbReportName = "rptPOGRNPending.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.pending_GRNPO(_invRepPara);
                    PrintPDF(targetFileName, obj._pendingGRN);
                    //   Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }

                if (rad14.Checked == true)
                {//Sanjeewa 2015-12-22  --Bond Balance Report
                    _opt = "rad14";
                    _isSelected = true;

                    string _err = "";

                    string _filePath = CHNLSVC.MsgPortal.getBondBalanceDetails(_invRepPara._GlbReportAsAtDate, _invRepPara._GlbReportItemCode,
                        _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                        _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, out _err);

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
                }

                if (rad15.Checked == true)
                {
                    //Wimal 23/12/2015
                    _opt = "rad15";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportHeading = "Local Purcahse Cost Detail";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    Session["GlbReportName"] = "rptLocalpurchaseCostDetail.rpt";
                    _invRepPara._GlbReportName = "rptLocalpurchaseCostDetail.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Local_Purchase_Costing(_invRepPara);
                    PrintPDF(targetFileName, obj._localPurcaseCost);
                    //  Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }

                if (rad16.Checked == true)
                {
                    //Wimal 29/12/2015
                    _opt = "rad16";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportHeading = "Value Addtion Report";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    Session["GlbReportName"] = "rptValueAddition.rpt";
                    _invRepPara._GlbReportName = "rptValueAddition.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Value_Addtion(_invRepPara);
                    PrintPDF(targetFileName, obj._valueAddition);

                    //   Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                if (rad17.Checked == true)  // item wise transaction detail listing
                {//kapila 30/12/2015
                    _opt = "rad17";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "InventoryStatementsTr3.rpt";
                    _invRepPara._GlbReportName = "InventoryStatementsTr3.rpt";

                    _invRepPara._GlbReportHeading = "Item-wise Transaction Detail Listing";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.ItemWiseTransDetListing(_invRepPara);
                    PrintPDF(targetFileName, obj._itmwiseTransList);

                    //    Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad18.Checked == true)  // stock ledger
                {//kapila 31/12/2015
                    _opt = "rad18";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "StockLedger.rpt";
                    _invRepPara._GlbReportName = "StockLedger.rpt";

                    _invRepPara._GlbReportHeading = "Stock Ledger Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.StockLedger(_invRepPara);
                    PrintPDF(targetFileName, obj._stkLedger);

                    //   Response.Redirect("InventoryReportViewer.aspx", false);
                    //  url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad19.Checked == true)  //
                {//kapila 
                    _opt = "rad19";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "MoveCostDetail.rpt";
                    _invRepPara._GlbReportName = "MoveCostDetail.rpt";
                    _invRepPara._GlbReportJobStatus = "N";
                    _invRepPara._GlbReportWithSerial = 0;
                    _invRepPara._GlbReportDoc = txtDocNo.Text;
                    _invRepPara._GlbReportDocType = txtDocType.Text;

                    _invRepPara._GlbReportHeading = "Movement - Cost Detail Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.movementCostDetail(_invRepPara);
                    PrintPDF(targetFileName, obj._movCostDet);
                    //   Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                //if (rad20.Checked == true)  // 
                //{//kapila 
                //    _opt = "rad20";
                //    _isSelected = true;
                //    Session["GlbReportType"] = "";
                //    update_PC_List_RPTDB();

                //    Session["GlbReportName"] = "SerialMovement.rpt";
                //    _invRepPara._GlbReportName = "SerialMovement.rpt";

                //    _invRepPara._GlbReportHeading = "Serial Movement Report";
                //    Session["InvReportPara"] = _invRepPara;

                //    clsInventory obj = new clsInventory();
                //    obj.Movement_Audit_Trial(_invRepPara);
                //    PrintPDF(targetFileName, obj._serMove);
                //    //     Response.Redirect("InventoryReportViewer.aspx", false);
                //    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                //    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //}
                if (rad21.Checked == true)  // current age report  (sp_get_curr_age)
                {//kapila 
                    _opt = "rad21";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    //Session["GlbReportName"] = "curr_age_report.rpt";
                    //_invRepPara._GlbReportName = "curr_age_report.rpt";
                    if (radlocwise.Checked)
                    {
                        Session["GlbReportName"] = "LocwiseItemAge.rpt";
                        _invRepPara._GlbReportName = "LocwiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Location-Wise Item Age Report";
                    }
                    if (radcatwise.Checked)
                    {
                        Session["GlbReportName"] = "CatwiseItemAge.rpt";
                        _invRepPara._GlbReportName = "CatwiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Category-Wise Item Age Report";
                    }
                    if (radsubcatwise.Checked)
                    {
                        Session["GlbReportName"] = "CatScatwiseItemAge.rpt";
                        _invRepPara._GlbReportName = "CatScatwiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Category/Sub Category-Wise Item Age Report";
                    }
                    if (radstatuswise.Checked)
                    {
                        Session["GlbReportName"] = "StuswiseItemAge.rpt";
                        _invRepPara._GlbReportName = "StuswiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Status-Wise Item Age Report";
                    }
                    if (raditembrandwise.Checked)
                    {
                        Session["GlbReportName"] = "ItmBrndwiseItemAge.rpt";
                        _invRepPara._GlbReportName = "ItmBrndwiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Item/Brand-Wise Item Age Report";
                    }

                    _invRepPara._GlbReportSupplier = txtSupplier.Text;
                    _invRepPara._GlbReportPromotor = txtBrandMan.Text;
                    _invRepPara._GlbReportType = "CUR";

                    _invRepPara._GlbReportHeading = "Current Age Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Loc_wise_item_age(_invRepPara);

                    if (radlocwise.Checked)
                        PrintPDF(targetFileName, obj._locAge);
                    if (radcatwise.Checked)
                        PrintPDF(targetFileName, obj._catAge);
                    if (radsubcatwise.Checked)
                        PrintPDF(targetFileName, obj._catScatAge);
                    if (radstatuswise.Checked)
                        PrintPDF(targetFileName, obj._StusAge);
                    if (raditembrandwise.Checked)
                        PrintPDF(targetFileName, obj._ItmBrndAge);
                    //    Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }

                //Randima 04-Oct-2016
                if (rad51.Checked == true)  // current age report  (comapany)
                {
                    _opt = "rad51";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportWithCost = 0;
                    _invRepPara._GlbReportWithSerial = 1;
                    _invRepPara._GlbReportJobStatus = "N";
                    _invRepPara._GlbReportType = "CURR";

                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;

                    _invRepPara._GlbReportHeading = "CURRENT AGE ANALYSIS (COMAPNY)";
                    Session["GlbReportName"] = "com_curr_age.rpt";
                    _invRepPara._GlbReportName = "com_curr_age.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    List<string> locList = new List<string>();

                    if (rbexel.Checked)
                    {
                        targetFileName = "";
                        string _err = "";

                        foreach (GridViewRow Item in dgvLocation.Rows)
                        {
                            Label lblLocation = (Label)Item.FindControl("lblLocation");
                            CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                            string pc = lblLocation.Text.ToUpper();
                           
                            if (chkLocation.Checked == true)
                            {
                                locList.Add(pc);
                            }
                        }
                        string _filePath = CHNLSVC.MsgPortal.getCurrentComAgeDetails(Session["UserCompanyCode"].ToString(), locList, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportBrandMgr, _invRepPara._GlbUserID);

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
                    }

                }
                if (rad25.Checked == true)
                {
                    _opt = "rad25";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();
                    Session["docNo"] = txtDocNo.Text;
                    if (txtDocNo.Text == "" | txtDocNo.Text == null)
                    {
                        displayMessage("Please select Document Number");
                    }
                    else
                    {
                        Session["GlbReportName"] = "serial_items.rpt";

                        _invRepPara._GlbReportSupplier = txtSupplier.Text;

                        _invRepPara._GlbReportHeading = "Item Serials Report";
                        Session["InvReportPara"] = _invRepPara;
                        //  Response.Redirect("InventoryReportViewer.aspx", false);
                        //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    }


                }

                if (rad22.Checked == true)  // Item Age Analysis with Serial
                {//kapila 
                    _opt = "rad22";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "StockBalanceWithSerialAge.rpt";
                    _invRepPara._GlbReportName = "StockBalanceWithSerialAge.rpt";

                    _invRepPara._GlbReportHeading = "Item Age Analysis with Serial";

                    if (chkWithCostWIP.Checked == true)
                        _invRepPara._GlbReportWithCost = 1;
                    else
                        _invRepPara._GlbReportWithCost = 0;

                    if (!string.IsNullOrEmpty(txtAgeFrom.Text))
                        _invRepPara._GlbReportFromAge = Convert.ToInt32(txtAgeFrom.Text);
                    if (!string.IsNullOrEmpty(txtAgeTo.Text))
                        _invRepPara._GlbReportToAge = Convert.ToInt32(txtAgeTo.Text);

                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.SerialAgeReport(_invRepPara);
                    //   PrintPDF(targetFileName, obj._serAge);

                    MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    string _path = _MasterComp.Mc_anal6;
                    obj._serAge.ExportToDisk(ExportFormatType.Excel, _path + "SerialAge" + _invRepPara._GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;
                    string workbookPath = _path + "SerialAge" + _invRepPara._GlbUserID + ".xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);


                    //  Response.Redirect("InventoryReportViewer.aspx", false);
                    //     url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //   ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad23.Checked == true)  // Inventory statement
                {//kapila 
                    _opt = "rad23";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "InventoryStatements.rpt";
                    _invRepPara._GlbReportName = "InventoryStatements.rpt";

                    _invRepPara._GlbReportHeading = "Inventory Statement";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.inventoryStatement(_invRepPara);
                    PrintPDF(targetFileName, obj._invSts);
                    //    Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                //if (rad24.Checked == true)  // movement summary
                //{//kapila 
                //    _opt = "rad24";
                //    _isSelected = true;
                //    Session["GlbReportType"] = "";
                //    update_PC_List_RPTDB();

                //    Session["GlbReportName"] = "MovementSummary.rpt";
                //    _invRepPara._GlbReportName = "MovementSummary.rpt";

                //    _invRepPara._GlbReportHeading = "Movement Summary";
                //    Session["InvReportPara"] = _invRepPara;
                //    //     Response.Redirect("InventoryReportViewer.aspx", false);
                //    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                //    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //}
                if (rad26.Checked == true)  // consignment movement
                {//kapila 
                    _opt = "rad26";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "ConsignmentMovement.rpt";
                    _invRepPara._GlbReportName = "ConsignmentMovement.rpt";

                    _invRepPara._GlbReportHeading = "Consignment Movement";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.ConsignmentMovement(_invRepPara);
                    PrintPDF(targetFileName, obj._consMove);

                    //     Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad27.Checked == true)  // location wise item age analysis   (sp_get_asat_age_scm_new)
                {//kapila 
                    _opt = "rad27";
                    _isSelected = true;

                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    if (radlocwise.Checked)
                    {
                        Session["GlbReportName"] = "LocwiseItemAge.rpt";
                        _invRepPara._GlbReportName = "LocwiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Location-Wise Item Age Report";
                    }
                    if (radcatwise.Checked)
                    {
                        Session["GlbReportName"] = "CatwiseItemAge.rpt";
                        _invRepPara._GlbReportName = "CatwiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Category-Wise Item Age Report";
                    }
                    if (radsubcatwise.Checked)
                    {
                        Session["GlbReportName"] = "CatScatwiseItemAge.rpt";
                        _invRepPara._GlbReportName = "CatScatwiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Category/Sub Category-Wise Item Age Report";
                    }
                    if (radstatuswise.Checked)
                    {
                        Session["GlbReportName"] = "StuswiseItemAge.rpt";
                        _invRepPara._GlbReportName = "StuswiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Status-Wise Item Age Report";
                    }
                    if (raditembrandwise.Checked)
                    {
                        Session["GlbReportName"] = "ItmBrndwiseItemAge.rpt";
                        _invRepPara._GlbReportName = "ItmBrndwiseItemAge.rpt";
                        _invRepPara._GlbReportHeading = "Item/Brand-Wise Item Age Report";
                    }

                    if (chkWithCostWIP.Checked == true)
                        _invRepPara._GlbReportWithCost = 1;
                    else
                        _invRepPara._GlbReportWithCost = 0;

                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Loc_wise_item_age(_invRepPara);

                    if (_invRepPara._GlbReportName == "LocwiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._locAge);
                    else if (_invRepPara._GlbReportName == "CatwiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._catAge);
                    else if (_invRepPara._GlbReportName == "CatScatwiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._catScatAge);
                    else if (_invRepPara._GlbReportName == "StuswiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._StusAge);
                    else if (_invRepPara._GlbReportName == "ItmBrndwiseItemAge.rpt")
                        PrintPDF(targetFileName, obj._ItmBrndAge);

                    //    Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                //if (rad28.Checked == true)  // age monitoring report
                //{//kapila 
                //    _isSelected = true;
                //    Session["GlbReportType"] = "";
                //    update_PC_List_RPTDB();

                //    Session["GlbReportName"] = "AgeMonitoring.rpt";
                //    _invRepPara._GlbReportName = "AgeMonitoring.rpt";

                //    _invRepPara._GlbReportHeading = "Age Monitoring Report";
                //    Session["InvReportPara"] = _invRepPara;
                ////    Response.Redirect("InventoryReportViewer.aspx", false);
                //    string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //}

                if (rad29.Checked == true)
                {
                    //Wimal 29/12/2015
                    _opt = "rad29";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportHeading = "GIT Report";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportType = "CURR";
                    Session["GlbReportName"] = "rpt_GLB_Git_Document.rpt";
                    _invRepPara._GlbReportName = "rpt_GLB_Git_Document.rpt";
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;
                    _invRepPara._GlbReportReqNo = ucLoactionSearch.txtLocation.Text;
                    Session["InvReportPara"] = _invRepPara;
                    _invRepPara._GlbGITWithSerials = Convert.ToInt32(chkWithSerial.Checked);

                    clsInventory obj = new clsInventory();
                    obj.currentGIT(_invRepPara);
                    PrintPDF(targetFileName, obj.rptgit);
                    // Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }

                if (rad30.Checked == true)
                {
                    //Wimal 29/12/2015
                    _opt = "rad30";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;
                    update_PC_List_RPTDB();
                    string _err;

                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportHeading = "GIT Report";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbReportType = "ASAT";
                    Session["GlbReportName"] = "rpt_GLB_Git_Document.rpt";
                    _invRepPara._GlbReportName = "rpt_GLB_Git_Document.rpt";
                    Session["InvReportPara"] = _invRepPara;                   


                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.getGITReport_Asat(_invRepPara._GlbReportAsAtDate.Date, _invRepPara._GlbReportComp, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5, _invRepPara._GlbUserID, _invRepPara._GlbReportOtherLoc,"N", out _err);                    
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

                    // Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }

                if (rad31.Checked == true)  // to bond status
                {//kapila 
                    if(string.IsNullOrEmpty( txtBondNumber.Text))
                    {
                        displayMessage("Please select Bond Number");
                        return;
                    }
                    _opt = "rad31";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    //  update_PC_List_RPTDB();

                    Session["GlbReportName"] = "ToBondStatus_Old.rpt";
                    _invRepPara._GlbReportName = "ToBondStatus_Old.rpt";

                    Session["GlbReportDoc"] = "";
                    _invRepPara._GlbReportToBondNo = txtBondNumber.Text;
                    _invRepPara._GlbReportGRNNo = txtGrnNo.Text;
                    _invRepPara._GlbReportReqNo = txtRequestNo.Text;


                    _invRepPara._GlbReportHeading = "To Bond Status Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.To_Bond_Status(_invRepPara);
                    PrintPDF(targetFileName, obj._toBondStusOld);
                    //   Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad32.Checked == true)  // item buffer status
                {//kapila 
                    _opt = "rad32";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "ItemBufferStatus.rpt";
                    _invRepPara._GlbReportName = "ItemBufferStatus.rpt";

                    if (radExcess.Checked == true)
                        _invRepPara._GlbReportDocType = "Excess";
                    else if (radShort.Checked == true)
                        _invRepPara._GlbReportDocType = "Short";
                    else
                        _invRepPara._GlbReportDocType = "Both";

                    _invRepPara._GlbReportHeading = "Item Buffer Status Report";
                    Session["InvReportPara"] = _invRepPara;

                    if (chkExportExcel.Checked == true)
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;

                        _filePath = CHNLSVC.MsgPortal.ItemBufferStatusReport_Excel(_invRepPara._GlbUserID, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5, _invRepPara._GlbReportModel, "", _invRepPara._GlbReportDocType, out _error);
                        if (!string.IsNullOrEmpty(_error))
                        {
                            lbtnView.Enabled = true;
                            Response.Redirect("~/Error.aspx?Error=" + _error + "");
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            lbtnView.Enabled = true;
                            Response.Redirect("~/Error.aspx?Error=" + "The excel file path cannot identify. Please contact IT Dept" + "");
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                        Response.Redirect("~/Error.aspx?" + "Export Completed" + "");
                    }
                    else
                    {
                        clsInventory obj = new clsInventory();
                        obj.Item_Buffer_Status(_invRepPara);
                        PrintPDF(targetFileName, obj._itmBufStus);
                        //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    }

                }
                if (rad33.Checked == true)
                {
                    _opt = "rad33";
                    Session["Company"] = ucLoactionSearch.Company;

                    if (Session["Company"] == null | Session["Company"].ToString() == "")
                    {
                        displayMessage("Please select Companay");
                    }
                    else
                    {
                        Session["Chanal"] = ucLoactionSearch.Channel;
                        Session["Location"] = ucLoactionSearch.ProfitCenter;
                        Session["Mcat"] = txtCat1.Text;
                        Session["Scat"] = txtCat2.Text;
                        Session["Range"] = txtCat3.Text;
                        Session["ItemCode"] = txtItemCode.Text;
                        Session["Model"] = txtBrand.Text;
                        Session["Brand"] = txtModel.Text;
                        Session["FrmDate"] = txtFromDate.Text;
                        Session["ToDt"] = txtToDate.Text;
                        Session["DocNo"] = txtDocNo.Text;
                        _isSelected = true;
                        Session["GlbReportType"] = "";
                        update_PC_List_RPTDB();

                        //INCLUDE REPORT VIEW FILE
                        string doc = Session["DocNo"].ToString();
                        string COM = Session["Company"] as string;
                        // string pc = Session["UserDefProf"] as string;
                        string Chanal = Session["Chanal"] as string;
                        string Location = Session["Location"] as string;
                        string Mcat = Session["Mcat"] as string;
                        string Scat = Session["Scat"] as string;
                        string range = Session["Range"] as string;
                        string ItemCode = Session["ItemCode"] as string;
                        string Model = Session["Model"] as string;
                        string Brand = Session["Brand"] as string;
                        string FrmDate = Session["FrmDate"] as string;
                        string ToDt = Session["ToDt"] as string;
                        string user = Session["UserID"] as string;

                        Session["GlbReportName"] = "AllocationDetails.rpt";
                        _invRepPara._GlbReportName = "AllocationDetails.rpt";

                        _invRepPara._GlbReportHeading = "Allocation Details";
                        Session["InvReportPara"] = _invRepPara;
                        clsInventory obj = new clsInventory();
                        obj.AllocationDetails(COM, Chanal, Location, Mcat, Scat, range, ItemCode, Model, Brand, FrmDate, ToDt, user, doc);
                        PrintPDF(targetFileName, obj._allocDet);
                        //// Response.Redirect("InventoryReportViewer.aspx", false);
                        //url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    }
                }

                if (rad34.Checked == true)  // mrn status
                {//kapila 
                    _opt = "rad34";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "MRN_Status.rpt";
                    _invRepPara._GlbReportName = "MRN_Status.rpt";

                    Session["GlbReportDoc"] = "";
                    _invRepPara._GlbReportReqFrom = txtReqfrom.Text;
                    _invRepPara._GlbReportReqTo = txtReqto.Text;

                    _invRepPara._GlbReportHeading = "Dispatch Request Status Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.MRN_Status(_invRepPara);
                    PrintPDF(targetFileName, obj._mrnStus);
                    //  Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad35.Checked == true)  // reservation details
                {//kapila 
                    _opt = "rad35";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "ReservationDetail.rpt";
                    _invRepPara._GlbReportName = "ReservationDetail.rpt";

                    Session["GlbReportDoc"] = "";

                    _invRepPara._GlbReportHeading = "Reservation Detail Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Reservation_Details(_invRepPara);
                    PrintPDF(targetFileName, obj._resDet);
                    //  Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad36.Checked == true)  // reservation summary
                {//kapila 
                    _opt = "rad36";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "ReservationSummary.rpt";
                    _invRepPara._GlbReportName = "ReservationSummary.rpt";

                    Session["GlbReportDoc"] = "";

                    _invRepPara._GlbReportHeading = "Reservation Summary Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Reservation_Summary(_invRepPara);
                    PrintPDF(targetFileName, obj._resSumm);

                    //  Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }

                if (rad37.Checked == true)//Location Details
                {//subodana
                    _opt = "rad37";
                    Session["Company"] = ucLoactionSearch.Company;
                    if (Session["Company"] == null | Session["Company"].ToString() == "")
                    {
                        displayMessage("Please select Companay");
                    }
                    else
                    {
                        //List<string> locval = new List<string>();
                        string locationsval = Session["UserID"].ToString();
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
                        Session["LocationList"] = locationsval;
                        Session["Chanal"] = ucLoactionSearch.Channel;
                        Session["Location"] = ucLoactionSearch.ProfitCenter;
                        Session["GlbReportType"] = "";
                        Session["GlbReportName"] = "cr_locationdetails.rpt";
                        _invRepPara._GlbReportName = "cr_locationdetails.rpt";
                        _invRepPara._GlbReportHeading = "Location Details";
                        Session["InvReportPara"] = _invRepPara;
                        clsInventory obj = new clsInventory();
                        string LOCVALUE = Session["LocationList"] as string;
                        string COM = Session["Company"] as string;
                        string Chanal = Session["Chanal"] as string;
                        string Location = Session["Location"] as string;
                        obj.LocationDetails(COM, Chanal, Location, LOCVALUE);
                        PrintPDF(targetFileName, obj._Locationdet);
                        // Response.Redirect("InventoryReportViewer.aspx", false);
                        // string url2 = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                        //  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url2, false);
                    }
                }

                if (rad38.Checked == true)  // liability report
                {
                    _opt = "rad38";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "rpt_Bond_Balance_Report1.rpt";
                    _invRepPara._GlbReportName = "rpt_Bond_Balance_Report1.rpt";

                    Session["GlbReportDoc"] = "";

                    _invRepPara._GlbReportHeading = "Liability Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.liabilityReport(_invRepPara);
                    PrintPDF(targetFileName, obj._LiableRep);
                    //  Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


                    ////-------------------------------------------------------------------------------
                    ////InvReportPara _invRepPara = new InvReportPara();
                    //_isSelected = true;
                    //Session["GlbReportType"] = "";
                    ////update_PC_List_RPTDB();
                    //Session["GlbReportName"] = "RepWarrantyCard_AOA.rpt";
                    //_invRepPara._GlbReportName = "RepWarrantyCard_AOA.rpt";
                    //Session["GlbReportDoc"] = "2125643";
                    //_invRepPara._GlbReportDoc = "2125643";
                    //_invRepPara._GlbReportItemCode = "LGPH-KP105";
                    //Session["GlbReportItemCode"] = "LGPH-KP105";
                    //_invRepPara._GlbReportMainSerial = "PH1";
                    //Session["GlbReportMainSerial"] = "PH1";

                    //_invRepPara._GlbReportHeading = "Warranty Print";
                    //Session["InvReportPara"] = _invRepPara;
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ////-------------------------------------------------------------------------------

                }
                if (rad39.Checked == true)  // last no sequence report
                {
                    _opt = "rad39";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "Last_No_Seq_Rep.rpt";
                    _invRepPara._GlbReportName = "Last_No_Seq_Rep.rpt";

                    Session["GlbReportDoc"] = "";
                    if (radInv.Checked == true)
                        _invRepPara._GlbReportDocType = "INV";
                    else
                        _invRepPara._GlbReportDocType = "PC";

                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();

                    _invRepPara._GlbReportHeading = "Last No Sequence Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.LastNoSeqReport(_invRepPara);
                    PrintPDF(targetFileName, obj._LastNoSeq);

                    //  Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
                if (rad40.Checked == true)//Item Details
                {//subodana
                    _opt = "rad40";
                    _isSelected = true;
                    Session["Company"] = ucLoactionSearch.Company;
                    if (Session["Company"] == null | Session["Company"].ToString() == "")
                    {
                        displayMessage("Please select Companay");
                    }
                    else
                    {
                        // btnDownloadfile.Visible = true;
                        Session["Location"] = ucLoactionSearch.ProfitCenter;
                        Session["Company"] = ucLoactionSearch.Company;
                        Session["Cat1"] = txtCat1.Text;
                        Session["Cat2"] = txtCat2.Text;
                        Session["Cat3"] = txtCat3.Text;
                        Session["Cat4"] = txtCat4.Text;
                        Session["Cat5"] = txtCat5.Text;
                        Session["Code"] = txtItemCode.Text;
                        Session["Brand"] = txtBrand.Text;
                        Session["Model"] = txtModel.Text;
                        Session["GlbReportType"] = "";
                        Session["GlbReportName"] = "item_profile.rpt";

                        Session["InvReportPara"] = _invRepPara;
                        clsInventory obj = new clsInventory();
                        string COM = Session["Company"] as string;
                        string Location = Session["Location"] as string;
                        string cat1 = Session["Cat1"] as string;
                        string cat2 = Session["Cat2"] as string;
                        string cat3 = Session["Cat3"] as string;
                        string cat4 = Session["Cat4"] as string;
                        string cat5 = Session["Cat5"] as string;
                        string code = Session["Code"] as string;
                        string brand = Session["Brand"] as string;
                        string model = Session["Model"] as string;
                        string user = Session["UserId"] as string;

                        string act = "";
                        if (raditmact.Checked) act = "act";
                        if (raditminact.Checked) act = "inact";
                        if (radallitm.Checked) act = "all";

                        obj.ItemProfileDetails(COM, user, Location, cat1, cat2, cat3, cat4, cat5, code, brand, model, act);
                        PrintPDF(targetFileName, obj._itemprofile);

                        //CVInventory.ReportSource = obj._itemprofile;
                        //CVInventory.RefreshReport();
                        //url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        // DisplayMessage("Successfull",3);

                        //  Session["btnDownloadfile"] = "true";

                        /*  string filenamenew = Session["UserID"].ToString();
                          string name = "\\\\192.168.1.16\\scm2rep\\" + filenamenew + ".xlsx";
                          FileInfo file = new FileInfo(name);
                          if (file.Exists)
                          {
                              System.IO.File.Copy(@"\\\\192.168.1.16\\scm2rep\\" + filenamenew + ".xlsx", "C:/Download_excel/" + filenamenew + ".xlsx", true);
                          }
                          else
                          {
                              DisplayMessage("This file does not exist.", 1);
                          }*/


                    }
                }

                if (rad41.Checked == true)
                {//Sanjeewa 2016-06-05  --Item Wise Warehouse Movements In Units
                    _opt = "rad41";
                    _isSelected = true;
                    _export = "Y";
                    update_PC_List_RPTDB();

                    string _err = "";
                    _invRepPara._GlbReportIsCostPrmission = 3;

                    string _filePath = CHNLSVC.MsgPortal.GetItemwiseWarehouseMovement(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate,
                        _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                         _invRepPara._GlbUserID, _invRepPara._GlbReportItemStatus, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportIsCostPrmission, out _err);

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

                if (rad42.Checked == true)
                {//Sanjeewa 2016-06-08  --Dispatch Approved Details
                    _opt = "rad42";
                    _isSelected = true;
                    _export = "Y";
                    update_PC_List_RPTDB();

                    string _err = "";
                    _invRepPara._GlbReportIsCostPrmission = 3;

                    string _filePath = CHNLSVC.MsgPortal.getDispatchAppDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate,
                        _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                        _invRepPara._GlbReportCompCode, _invRepPara._GlbUserID, out _err);

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
                    //  btnDownloadfile.Visible = true;
                }

                if (rad43.Checked == true)
                {//Sanjeewa 2016-06-08  --AOD Reconciliation Details
                    _opt = "rad43";
                    _isSelected = true;
                    _export = "Y";
                    update_PC_List_RPTDB();

                    string _err = "";
                    _invRepPara._GlbReportIsCostPrmission = 3;
                    _invRepPara._GlbReportDoc = txtoperteamfrom.Text;
                    _invRepPara._GlbReportDoc2 = txtoperteamto.Text;

                    string _filePath = CHNLSVC.MsgPortal.getAODReconciliationDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportDoc, _invRepPara._GlbReportDoc2, _invRepPara._GlbUserID, out _err);

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
                    //  btnDownloadfile.Visible = true;
                }

                if (rad44.Checked == true)  // Excess Short Stock Report
                {
                    _opt = "rad44";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "excess_stock_report.rpt";
                    _invRepPara._GlbReportName = "excess_stock_report.rpt";

                    Session["GlbReportDoc"] = "";
                    _invRepPara._GlbReportDocType = radExcess.Checked ? "EX" : radShort.Checked ? "SH" : "BT";

                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();

                    _invRepPara._GlbReportHeading = "Excess Short Stock Report";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.ExcessStockReport(_invRepPara);
                    PrintPDF(targetFileName, obj._Esxcessstk);

                    //  Response.Redirect("InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }

                if (rad45.Checked == true)
                {//Sanjeewa 2016-08-02 SR Request Report
                    _opt = "rad45";

                    _isSelected = true;
                    _export = "Y";
                    string _err = "";
                    Session["GlbReportType"] = "";
                    update_PC_List_RPTDB();

                    Session["GlbReportName"] = "MRN_Status.rpt";
                    _invRepPara._GlbReportName = "MRN_Status.rpt";

                    Session["GlbReportDoc"] = "";
                    _invRepPara._GlbReportReqFrom = txtReqfrom.Text;
                    _invRepPara._GlbReportReqTo = txtReqto.Text;
                    _invRepPara._GlbReportIsCostPrmission = 3;
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;

                    _invRepPara._GlbReportHeading = "MRN Status Report";
                    Session["InvReportPara"] = _invRepPara;

                    string _filePath = CHNLSVC.MsgPortal.SRMRNStatusReport(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate,
                        _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5,
                        _invRepPara._GlbReportCompCode, _invRepPara._GlbReportOtherLoc, _invRepPara._GlbUserID, out _err);

                    //clsInventory obj = new clsInventory();
                    //obj.MRN_Status(_invRepPara);
                    //PrintPDF(targetFileName, obj._mrnStus);

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
                }

                if (rad46.Checked == true)
                {//Wimal 2016-08-12 Daily bond balance
                    _opt = "rad46";
                    _isSelected = true;
                    _export = "Y";
                    update_PC_List_RPTDB();

                    string _err = "";
                    _invRepPara._GlbReportIsCostPrmission = 3;
                    _invRepPara._GlbReportDoc = txtoperteamfrom.Text;
                    _invRepPara._GlbReportDoc2 = txtoperteamto.Text;

                    string _filePath = CHNLSVC.MsgPortal.getdialyWHStock(_invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportModel, _invRepPara._GlbReportBrand, _invRepPara._GlbReportBrandMgr, _invRepPara._GlbUserID, out _err);

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
                    //  btnDownloadfile.Visible = true;
                }
                //subodana
                if (rad47.Checked)
                {
                    try
                    {
                        DateTime fromdate = Convert.ToDateTime(txtFromDate.Text.ToString());
                        DateTime todate = Convert.ToDateTime(txtToDate.Text.ToString());
                        string itemcode = txtItemCode.Text.ToString();
                        string category = txtCat1.Text.ToString();
                        string cat2 = txtCat2.Text.ToString();
                        string cat3 = txtCat3.Text.ToString();
                        string dispatchloc = ucLoactionSearch.txtLocation.Text.ToString(); //txtReqfrom.Text.ToString();
                        string customercode = txtcustormer.Text.ToString();
                        string status = "";

                        if (radrebond.Checked == true)
                        {
                            status = "RE";
                        }
                        else if (radexbond.Checked == true)
                        {
                            status = "EX";
                        }
                        else if (radBOI.Checked == true)
                        {
                            status = "BOI";
                        }
                        else if (radbondall.Checked == true)
                        {
                            status = "";
                        }
                        clsInventory obj = new clsInventory();
                        obj.ReservationItemsRep(fromdate, todate, itemcode, category, dispatchloc, customercode, status, txtRequestNo.Text.ToString(), cat2, cat3, Session["AdminTMList"].ToString());
                        PrintPDF(targetFileName, obj._resitems);

                    }
                    catch (Exception ex)
                    {
                        displayMessage(ex.Message);
                    }



                }

                //subodana
                if (rad49.Checked)
                {
                    _opt = "rad46";
                    _isSelected = true;
                    _export = "Y";
                    
                    string _err = "";                    
                    
                    DateTime fromdate = Convert.ToDateTime(txtFromDate.Text.ToString());
                    DateTime todate = Convert.ToDateTime(txtToDate.Text.ToString());
                    string chanal = ucLoactionSearch.txtChanel.Text.ToString();
                    string com = Session["UserCompanyCode"].ToString();
                    clsInventory obj = new clsInventory();
                    DataTable _dt= obj.ADJDetails(com, chanal, fromdate, todate);

                    //string _filePath = CHNLSVC.MsgPortal.getAdjustmentDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbUserID, com,chanal, out _err);

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

                if (rad48.Checked == true)  // cusdec entry req
                {//kapila
                    _opt = "rad48";
                    _isSelected = true;
                    _invRepPara._GlbReportHeading = "CusDec Entry Request Details";
                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    //if (chlAllReqTp.Checked)
                    //    _invRepPara._GlbReportType = null;
                    //else
                    //    _invRepPara._GlbReportType = ddlRequestType.SelectedValue;

                    //_invRepPara._GlbUserID = null;
                    //if (!chkUser.Checked)
                    //    _invRepPara._GlbUserID = Session["UserID"].ToString();

                    //_invRepPara._GlbReportParaLine1 = 0;
                    //if (chkViewAll.Checked)
                    //    _invRepPara._GlbReportParaLine1 = 1;

                    Session["InvReportPara"] = _invRepPara;
                    _invRepPara._GlbReportName = "CusDecEntryReq.rpt";
                    Session["GlbReportName"] = "CusDecEntryReq.rpt";

                    clsInventory obj = new clsInventory();
                    obj.CusDecEntryRequest(_invRepPara);
                    PrintPDF(targetFileName, obj._cusDecEntry);
                }

                if (rad52.Checked == true)  // Aod In-Out report
                {//Wimal - 24/10/2016

                    _opt = "rad52";
                    _isSelected = true;
                    _export = "Y";
                    update_PC_List_RPTDB();

                    string _err = "";
                    _invRepPara._GlbReportIsCostPrmission = 3;
                    _invRepPara._GlbReportDoc = txtoperteamfrom.Text;
                    _invRepPara._GlbReportDoc2 = txtoperteamto.Text;
                    //_invRepPara._GlbLocation = txtLocation.Text.ToString;
                    _invRepPara._GlbReportOtherLoc = txtOtherloc.Text;
                    _invRepPara._GlbDocNo = txtDocNo.Text;
                    if (radTotRec.Checked) { _invRepPara._GlbReportType = "TOT"; };
                    if (radPartRec.Checked) { _invRepPara._GlbReportType = "PART"; };
                    if (radNotRec.Checked) { _invRepPara._GlbReportType = "NOT"; };
                    //-txtDirection.Text 

                    string _filePath = CHNLSVC.MsgPortal.getAODInOutDetails(_invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportCompCode, _invRepPara._GlbReportDoc, _invRepPara._GlbReportDoc2, "", _invRepPara._GlbReportOtherLoc, txtDirection.Text == "IN" ? txtDocNo.Text : "", txtDirection.Text != "IN" ? "" : txtDocNo.Text, _invRepPara._GlbReportType, _invRepPara._GlbUserID, out _err);

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
                    //  btnDownloadfile.Visible = true;
                }

                if (rad53.Checked == true)  // current consignment report  (comapany)
                {
                    _opt = "rad53";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportWithCost = 0;
                    _invRepPara._GlbReportWithSerial = 1;
                    _invRepPara._GlbReportJobStatus = "N";
                    _invRepPara._GlbReportType = "CURR";

                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;

                    _invRepPara._GlbReportHeading = "CURRENT CONSIGNMENT REPORT";
                    //Session["GlbReportName"] = "com_curr_age.rpt";
                    //_invRepPara._GlbReportName = "com_curr_age.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    List<string> locList = new List<string>();
                    
                        targetFileName = "";
                        string _err = "";

                        foreach (GridViewRow Item in dgvLocation.Rows)
                        {
                            Label lblLocation = (Label)Item.FindControl("lblLocation");
                            CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                            string pc = lblLocation.Text.ToUpper();

                            if (chkLocation.Checked == true)
                            {
                                locList.Add(pc);
                            }
                        }
                        string _filePath = CHNLSVC.MsgPortal.getConsignmentStockWithValue(Session["UserCompanyCode"].ToString(), locList, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportBrandMgr, _invRepPara._GlbUserID, false, _invRepPara._GlbReportAsAtDate);

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

                }

                if (rad54.Checked == true)  // asat consignment report  (comapany)
                {
                    if (Convert.ToDateTime(txtAsAt.Text).Day != DateTime.DaysInMonth(Convert.ToDateTime(txtAsAt.Text).Year, Convert.ToDateTime(txtAsAt.Text).Month))
                    {
                        displayMessage("Asat Date Should be Month End Date");
                        return;
                    }

                    _opt = "rad54";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportWithCost = 0;
                    _invRepPara._GlbReportWithSerial = 1;
                    _invRepPara._GlbReportJobStatus = "N";
                    _invRepPara._GlbReportType = "CURR";

                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportAsAtDate = Convert.ToDateTime(txtAsAt.Text).Date;

                    _invRepPara._GlbReportHeading = "As At CONSIGNMENT REPORT";
                    //Session["GlbReportName"] = "com_curr_age.rpt";
                    //_invRepPara._GlbReportName = "com_curr_age.rpt";
                    Session["InvReportPara"] = _invRepPara;

                    List<string> locList = new List<string>();

                    targetFileName = "";
                    string _err = "";

                    foreach (GridViewRow Item in dgvLocation.Rows)
                    {
                        Label lblLocation = (Label)Item.FindControl("lblLocation");
                        CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                        string pc = lblLocation.Text.ToUpper();

                        if (chkLocation.Checked == true)
                        {
                            locList.Add(pc);
                        }
                    }
                    string _filePath = CHNLSVC.MsgPortal.getConsignmentStockWithValue(Session["UserCompanyCode"].ToString(), locList, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportBrandMgr, _invRepPara._GlbUserID, true, _invRepPara._GlbReportAsAtDate);

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

                }

                if (rad55.Checked == true)  // Daily Entry Details (Bond)
                {
                    if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                    {
                        displayMessage("From date must be greater than the to date");
                        return;
                    }
                    _opt = "rad55";
                    _isSelected = true;
                    //update_PC_List_RPTDB();
                    string _err = "";
                    Session["Company"] = ucLoactionSearch.Company;
                    string _filePath = CHNLSVC.MsgPortal.Bond_DaillyEntryDetails(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), com_cds, Session["UserID"].ToString(), Session["UserCompanyName"].ToString(), ddlReportType.SelectedValue, Session["UserCompanyCode"].ToString(), out _err);

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
                    //p.StartInfo = new ProcessStartInfo("C:/Download_excel/" + Session["UserID"].ToString() + ".xlsx");
                    p.Start();
                    targetFileName = string.Empty;
                }
                //Check SP ItemCondition
                if (rad56.Checked)
                {
                    _isSelected = true;
                    if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                    {
                        displayMessage("From date must be greater than the to date");
                        return;
                    }
                    //if (txtOtherloc.Text.Equals(string.Empty))
                    //{
                    //    displayMessage("Please select Other Location");
                    //    return;
                    //}
                    List<string> locList = new List<string>();
                    foreach (GridViewRow Item in dgvLocation.Rows)
                    {
                        Label lblLocation = (Label)Item.FindControl("lblLocation");
                        CheckBox chkLocation = (CheckBox)Item.FindControl("chkLocation");
                        string pc = lblLocation.Text.ToUpper();

                        if (chkLocation.Checked == true)
                        {
                            locList.Add(pc);
                        }
                    }
                                    
                   
                    
                    if(locList.Count<1)
                    {
                        displayMessage("Please select a location");
                        return;
                    }
                    DateTime fromdate = Convert.ToDateTime(txtFromDate.Text.ToString());
                    DateTime todate = Convert.ToDateTime(txtToDate.Text.ToString());                                  
                                       
                    try
                    {              
                     
                        clsInventory obj = new clsInventory();
                        obj.ProductItemUpdate(fromdate, todate, txtOtherloc.Text, locList[0], com_cds,txtUser.Text );
                        if(obj._itemconditionreport.Section3.ReportObjects.Count<1)
                        {
                            displayMessage("No Data Found");
                            return;
                        }
                        PrintPDF(targetFileName, obj._itemconditionreport);

                    }
                    catch (Exception ex)
                    {
                        displayMessage(ex.Message);
                    }

                }
                //

                //Tharindu 2018-03-22 Current Aginst BL
                if (rad57.Checked)
                {
                    _isSelected = true;
                    if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                    {
                        displayMessage("From date must be greater than the to date");
                        return;
                    }
                    DateTime fromdate = Convert.ToDateTime(txtFromDate.Text.ToString());
                    DateTime todate = Convert.ToDateTime(txtToDate.Text.ToString());
                    string itemcode = txtItemCode.Text.ToString();
                   
                    string cat2 = txtCat2.Text.ToString();
                    string cat3 = txtCat3.Text.ToString();
                    string dispatchloc = ucLoactionSearch.txtLocation.Text.ToString(); //txtReqfrom.Text.ToString();
                    string customercode = txtcustormer.Text.ToString();
                    string status = "";

                 
                    try
                    {

                        _opt = "rad57";
                        _isSelected = true;
                        _invRepPara._GlbReportHeading = "Current Availability Aginst BL";
                        _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                        _invRepPara._GlbDocNo =  txtDocNo.Text.ToString().Trim();
                        _invRepPara._GlbReportItemCode = txtItemCode.Text.ToString().Trim();
                        _invRepPara._GlbReportModel = txtModel.Text.ToString().Trim();
                        _invRepPara._GlbReportBrand =  txtBrand.Text.ToString().Trim();
                        _invRepPara._GlbReportItemCat1 =  txtCat1.Text.ToString().Trim();
                        _invRepPara._GlbReportItemCat2 = txtCat2.Text.ToString().Trim();
                        _invRepPara._GlbReportItemCat3 = txtCat3.Text.ToString().Trim();
                        _invRepPara._GlbReportSupplier = txtSupplier.Text.ToString().Trim();

  
                        Session["InvReportPara"] = _invRepPara;
                        _invRepPara._GlbReportName = "Current_Availability_Against_BL.rpt";
                        Session["GlbReportName"] = "Current_Availability_Against_BL.rpt";

                        clsInventory obj = new clsInventory(); 
                        obj.GetCurrentAvailabilityBL_rpt(_invRepPara);
                        PrintPDF(targetFileName, obj._curr_available_rpt);

                    }
                    catch (Exception ex)
                    {
                        displayMessage(ex.Message);
                    }

                }



                if (rad58.Checked) {
                    //string User = Session["UserID"].ToString();
                    int count = 0;
                    foreach (GridViewRow row in dgvLocation.Rows)
                    {
                        CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");

                        if (chkLocation.Checked) {
                            count++;
                        }
                    }

                    if (count > 0) { 
                    _opt = "rad58";
                    _isSelected = true;

                    update_PC_List_RPTDB();

                    _invRepPara._GlbReportGroupLastGroupCat = (DropDownListAging.SelectedIndex).ToString();
                    _invRepPara._GlbReportHeading = "Summarized Ageing Report for Provisioning";
                    Session["InvReportPara"] = _invRepPara;

                    clsInventory obj = new clsInventory();
                    obj.Ageing_Report_for_Provisioning(_invRepPara);
                     PrintPDF(targetFileName, obj._summarized_age_report);

                    //Session["GlbReportType"] = "";
                    //update_PC_List_RPTDB();

                    //Session["GlbReportName"] = "summarized_age_report.rpt";
                    //_invRepPara._GlbReportName = "summarized_age_report.rpt";

                    //Session["GlbReportDoc"] = "";
                    //_invRepPara._GlbReportDocType = radExcess.Checked ? "EX" : radShort.Checked ? "SH" : "BT";

                    //_invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                    //_invRepPara._GlbUserID = Session["UserID"].ToString();
                    //_invRepPara._GlbReportToDate = DateTime.Parse(txtToDate.Text);
                    //_invRepPara._GlbReportGroupLastGroupCat = DropDownListAging.SelectedValue.ToString();
                    //_invRepPara._GlbReportHeading = "Summarized age report";
                    //Session["InvReportPara"] = _invRepPara;
                    
                    }
                    else
                    {
                        displayMessage("Please select a location");
                        return;
                    }
                
                }

                if (rad59.Checked)
                {
                    //string User = Session["UserID"].ToString();
                    int count = 0;
                    foreach (GridViewRow row in dgvLocation.Rows)
                    {
                        CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");

                        if (chkLocation.Checked)
                        {
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        update_PC_List_RPTDB();
                        _opt = "rad11";
                        _isSelected = true;
                        Session["GlbReportType"] = "";
                        _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                        update_PC_List_RPTDB();
                        _invRepPara._GlbReportDoc = txtDocNo.Text;
                        _invRepPara._GlbReportHeading = "Fixed Asset Depreciation detail Report";
                        _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                        Session["GlbReportName"] = "FIXA_GrnDepre_details.rpt";
                        _invRepPara._GlbReportName = "FIXA_GrnDepre_details.rpt";
                        Session["InvReportPara"] = _invRepPara;

                        if (radioLocal.Checked)
                        {
                            Session["PurchaseType"] = "L";
                        }
                        else if (radioImport.Checked)
                        {
                            Session["PurchaseType"] = "I";
                        }
                        else
                        {
                            Session["PurchaseType"] = "B";
                        }


                        _invRepPara._GlbEntryType = Session["PurchaseType"].ToString();


                        clsInventory obj = new clsInventory();
                        obj.Fixa_dtl_WDepreciation(_invRepPara);
                        PrintPDF(targetFileName, obj._FIXA_DepreDtl);
                    }
                    else
                    {
                        lbtnView.Enabled = true;
                        displayMessage("Please select a location");
                        return;
                    }

                }


                if (rad60.Checked)
                {
                  
                    //_isSelected = true;
                    if (Convert.ToDateTime(DateTime.Now) < Convert.ToDateTime(txtAsAt.Text))
                    {
                        lbtnView.Enabled = true;
                       // displayMessage("From date must be greater than the to date");
                        displayMessage("Asat Date Can`t be grater than current Date");
                        return;
                    }
                    //string User = Session["UserID"].ToString();
                    update_PC_List_RPTDB();
                    int count = 0;
                    foreach (GridViewRow row in dgvLocation.Rows)
                    {
                        CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");

                        if (chkLocation.Checked)
                        {
                            count++;
                        }
                    }

                    _invRepPara._GlbReportItemCode = txtItemCode.Text.ToString().Trim();
                    _invRepPara._GlbReportModel = txtModel.Text.ToString().Trim();
                    _invRepPara._GlbReportBrand = txtBrand.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat1 = txtCat1.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat2 = txtCat2.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat3 = txtCat3.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat4 = txtCat4.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat5 = txtCat5.Text.ToString().Trim();

                    if (count > 0)
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;                       
                        _filePath = CHNLSVC.MsgPortal.get_depreciation_Summery(_invRepPara._GlbReportComp, "", _invRepPara._GlbReportAsAtDate, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCode, _invRepPara._GlbUserID, out _error);
                        if (!string.IsNullOrEmpty(_error))
                        {
                            lbtnView.Enabled = true;                           
                            displayMessage(_error);
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            lbtnView.Enabled = true;                           
                            displayMessage(_error);
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();
                        
                        lbtnView.Enabled = true;
                        displayMessage("Export Completed");
                        return;
                    }
                    else
                    {
                        lbtnView.Enabled = true;
                        displayMessage("Please select a location");
                        return;
                    }

                }

                if (rad61.Checked)
                {
                    _opt = "rad61";
                    _isSelected = true;
                    Session["GlbReportType"] = "";
                    _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                    update_PC_List_RPTDB();
                    _invRepPara._GlbReportHeading = "Fixed Assets PPNOTE";
                    _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();              
                    Session["InvReportPara"] = _invRepPara;
                    
                    int count = 0;
                    foreach (GridViewRow row in dgvLocation.Rows)
                    {
                        CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");

                        if (chkLocation.Checked)
                        {
                            count++;
                        }
                    }

                    _invRepPara._GlbReportItemCode = txtItemCode.Text.ToString().Trim();
                    _invRepPara._GlbReportModel = txtModel.Text.ToString().Trim();
                    _invRepPara._GlbReportBrand = txtBrand.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat1 = txtCat1.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat2 = txtCat2.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat3 = txtCat3.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat4 = txtCat4.Text.ToString().Trim();
                    _invRepPara._GlbReportItemCat5 = txtCat5.Text.ToString().Trim();

                    if (count > 0)
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;                      
                        _filePath = CHNLSVC.MsgPortal.get_depreciation_PPNote(_invRepPara._GlbReportComp, "", _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportItemCat3, _invRepPara._GlbReportItemCat4, _invRepPara._GlbReportItemCat5, _invRepPara._GlbReportBrand, _invRepPara._GlbReportModel, _invRepPara._GlbReportItemCode, _invRepPara._GlbUserID, out _error);
                        if (!string.IsNullOrEmpty(_error))
                        {                            
                            lbtnView.Enabled = true;
                            displayMessage(_error);
                            return;
                        }
                        if (string.IsNullOrEmpty(_filePath))
                        {
                            lbtnView.Enabled = true;                            
                            displayMessage("The excel file path cannot identify. Please contact IT Dept");
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();
                       
                        lbtnView.Enabled = true;
                        displayMessage("Export Completed");
                        return;
                    }
                    else
                    {
                        lbtnView.Enabled = true;
                        displayMessage("Please select a location");
                        return;
                    }

                }
                
                if (rad62.Checked)
                {
                    update_PC_List_RPTDB();
                    //string User = Session["UserID"].ToString();
                    int count = 0;
                    foreach (GridViewRow row in dgvLocation.Rows)
                    {
                        CheckBox chkLocation = (CheckBox)row.FindControl("chkLocation");

                        if (chkLocation.Checked)
                        {
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;

                        // _filePath = CHNLSVC.MsgPortal.getpurchaseOrderSummeryExcel(_invRepPara._GlbReportComp, "  ", _invRepPara._GlbEntryType, _invRepPara._GlbReportSupplier, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportDocType, _invRepPara._GlbReportDoc, _invRepPara._GlbUserID, out _error);
                        _filePath = CHNLSVC.MsgPortal.get_depreciation_Disposal(_invRepPara._GlbReportComp, "", _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbUserID, out _error);
                        if (!string.IsNullOrEmpty(_error))
                        {
                            lbtnView.Enabled = true;
                            //Response.Redirect("~/Error.aspx?Error=" + _error + "");
                            displayMessage(_error);
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            lbtnView.Enabled = true;
                            //Response.Redirect("~/Error.aspx?Error=" + "The excel file path cannot identify. Please contact IT Dept" + "");
                            displayMessage("The excel file path cannot identify. Please contact IT Dept");
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                        //Response.Redirect("~/Error.aspx?" + "Export Completed" + "");
                        lbtnView.Enabled = true;
                        displayMessage("Export Completed");
                        return;
                    }
                    else
                    {
                        lbtnView.Enabled = true;
                        displayMessage("Please select a location");
                        return;
                    }

                }             


                if (!chkExportExcel.Checked)// Add this Condition by Udesh 16-Oct-2018
                {
                if (!string.IsNullOrEmpty(targetFileName))
                {
                    if (_export == "N")
                    {
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
                }

                if (_isSelected == false)
                    displayMessage("Please select the report!");

                }

                lbtnView.Enabled = true;
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "Inv_Rep", "Run Ok", Session["UserID"].ToString());
            }
            catch (Exception err)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog(_opt, "Inv_Rep", err.Message, Session["UserID"].ToString());
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
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(txtDocType.Text + seperator + Session["UserCompanyCode"].ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.ReqFromLoc:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReqToLoc:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
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
                Session["BondNumber"] = null;
                Session["GrnNumber"] = null;
                Session["ReqNumber"] = null;
                Session["OtherLoc"] = null;
                Session["OperationTeam"] = null;
                Session["ReqFromLoc"] = null;
                Session["ReqToLoc"] = null;
                Session["InvoiceNo"] = null;
                Session["FromTeam"] = null;
                Session["ToTeam"] = null;
                Session["cussearch"] = null;


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
                if (lblSearchType.Text == "ReqFromLoc")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReqFromLoc);
                    dt = CHNLSVC.CommonSearch.SearchClompanyLocation(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ReqFromLoc"] = dt;
                }
                if (lblSearchType.Text == "ReqToLoc")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReqFromLoc);
                    dt = CHNLSVC.CommonSearch.SearchClompanyLocation(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ReqToLoc"] = dt;
                }
                if (lblSearchType.Text == "Supplier")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    dt = CHNLSVC.CommonSearch.GetSupplierData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["Supplier"] = dt;
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
                if (lblSearchType.Text == "FromTeam")
                {
                    string para = "";
                    dt = CHNLSVC.CommonSearch.Get_Operationteam(Session["UserCompanyCode"].ToString(), para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["FromTeam"] = dt;
                }
                if (lblSearchType.Text == "ToTeam")
                {
                    string para = "";
                    dt = CHNLSVC.CommonSearch.Get_Operationteam(Session["UserCompanyCode"].ToString(), para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["ToTeam"] = dt;
                }
                if (lblSearchType.Text == "InvoiceNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                    dt = CHNLSVC.CommonSearch.GetDocNumforTP(para, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                    Session["InvoiceNo"] = dt;
                }
                if (lblSearchType.Text == "cussearch")
                {
                    string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    dt = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams2, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    Session["cussearch"] = dt;
                }

                if (lblSearchType.Text == "empsearch")
                {
                    //string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    //dt = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams2, cmbSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                   // dt = CHNLSVC.CommonSearch.Get_All_Users(SearchParams2, null, null);
                    dt = CHNLSVC.CommonSearch.Get_All_SystemUsers("178:|", cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    Session["empsearch"] = dt;
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
                if (lblSearchType.Text == "ReqFromLoc")
                {
                    _result = (DataTable)Session["ReqFromLoc"];
                }
                if (lblSearchType.Text == "ReqToLoc")
                {
                    _result = (DataTable)Session["ReqToLoc"];
                }
                if (lblSearchType.Text == "Supplier")
                {
                    _result = (DataTable)Session["Supplier"];
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
                if (lblSearchType.Text == "FromTeam")
                {
                    _result = (DataTable)Session["FromTeam"];
                }
                if (lblSearchType.Text == "ToTeam")
                {
                    _result = (DataTable)Session["ToTeam"];
                }
                if (lblSearchType.Text == "InvoiceNo")
                {
                    _result = (DataTable)Session["InvoiceNo"];
                }
                if (lblSearchType.Text == "cussearch")
                {
                    _result = (DataTable)Session["cussearch"];
                }
                if (lblSearchType.Text == "empsearch")
                {
                    _result = (DataTable)Session["empsearch"];
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
                if (lblSearchType.Text == "ReqFromLoc")
                {
                    txtReqfrom.Text = code;
                    // chkAllBrandMan.Checked = false;
                }
                if (lblSearchType.Text == "ReqToLoc")
                {
                    txtReqto.Text = code;
                    // chkAllBrandMan.Checked = false;
                }
                if (lblSearchType.Text == "Supplier")
                {
                    txtSupplier.Text = code;
                    chkAllSupplier.Checked = false;
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
                if (lblSearchType.Text == "FromTeam")
                {
                    txtoperteamfrom.Text = code;
                    chkAllSupplier.Checked = false;
                }
                if (lblSearchType.Text == "ToTeam")
                {
                    txtoperteamto.Text = code;
                    chkAllSupplier.Checked = false;
                }
                if (lblSearchType.Text == "cussearch")
                {
                    txtcustormer.Text = code;

                }
                if (lblSearchType.Text == "InvoiceNo")
                {
                    txtDocNo.Text = code;
                    chkAllDocNo.Checked = false;
                }

                if (lblSearchType.Text == "empsearch")
                {
                    txtUser.Text = code;                  
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
                lblSearchType.Text = "InvoiceNo";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = CHNLSVC.CommonSearch.GetDocNumforTP(para, null, null);
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
            if (chkAllDocNo.Checked)
            {
                txtDocNo.Enabled = false;
            }
            else
            {
                txtDocNo.Enabled = true;
            }
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
                    string pc = ucLoactionSearch.ProfitCenter.ToUpper();


                    string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();

                    //if (CHNLSVC.Inventory.CheckUserPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), _masterLocation, "REPI"))
                    //Add by Chamal 30-Aug-2013
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10045))
                    {
                        // DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);
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
                    DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(para, "Code", txtSupplier.Text); //Added by Udesh 18/Oct/2018
                    //DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(para, null, null); //Commented by Udesh 18/Oct/2018
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
                dropDown.SelectedIndex = 0;
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
            chkExportExcel.Checked = false; // Udesh 15-Oct-2018

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
            txtExDate.Text = DateTime.Today.Date.AddMonths(1).ToString("dd/MMM/yyyy");
            BindYear();
            // Loadselectdate();
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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12500))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12500)");
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
            txt.Add(txtReqfrom);
            txt.Add(txtReqto);

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
            lbtn.Add(btnReqfrom);
            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(CheckBox1);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(btnReqto);
            lbtn.Add(lbtnAddCat1Remove);
            lbtn.Add(lbtnAddModelRemove);
            lbtn.Add(lbtnAddBrandRemove);
            lbtn.Add(lbtnAddItemCodeRemove);
            lbtn.Add(lbtnAddCat5Remove);
            lbtn.Add(lbtnAddCat4Remove);
            lbtn.Add(lbtnAddCat3Remove);
            lbtn.Add(lbtnAddCat2Remove);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radlocwise);
            rad.Add(radcatwise);
            rad.Add(radsubcatwise);
            rad.Add(radstatuswise);
            rad.Add(raditembrandwise);

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

        protected void rad02_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12501))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12501)");
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
            //  chk.Add(chkWithJob);

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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12502))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12502)");
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
            Default();
            txt.Add(txtAsAt);

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
            chk.Add(CheckBox1);

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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12503))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12503)");
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
            Default();
            txt.Add(txtAsAt);

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
            chk.Add(CheckBox1);

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

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }

        protected void rad53_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12501))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12501)");
            //    rad02.Checked = false;
            //    return;
            //}
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
            //  chk.Add(chkWithJob);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }

        protected void rad54_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12502))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12502)");
            //    rad03.Checked = false;
            //    return;
            //}
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

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
            chk.Add(CheckBox1);

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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12504))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12504)");
                rad05.Checked = false;
                return;
            }
            DesableAllControler();
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
            lbtn.Add(lbtnSeDocType);
            lbtn.Add(lbtnSeDocSubType);
            lbtn.Add(lbtnSeDerection);
            lbtn.Add(btnotherloc);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkWithCostWIP);
            chk.Add(chkAllDocNo);
            chk.Add(chkAllDocType);
            chk.Add(chkAllDocSubType);
            chk.Add(chkAllDirNo);
            chk.Add(chkSumm);
            chk.Add(chkNor);
            chk.Add(chkDet);
            chk.Add(chklist);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(lbtnSeDocNo);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);


            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtDocType);
            txt.Add(txtDocSubType);
            txt.Add(txtDocNo);
            txt.Add(txtDirection);
            txt.Add(txtBatchNo);
            txt.Add(txtOtherloc);
            //txt.Add(txtAsAt);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad06_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12505))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12505)");
                rad06.Checked = false;
                return;
            }
            DesableAllControler();
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
            lbtn.Add(btnotherloc);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkWithCostWIP);
            // chk.Add(chkSumm);

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

            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);


            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtDocNo);
            txt.Add(txtDirection);
            txt.Add(txtBatchNo);
            txt.Add(txtOtherloc);
            //txt.Add(txtAsAt);

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

            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);

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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12506))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12506)");
               // rad07.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //As at date
            txt.Add(txtAsAt);
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

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);


            //With Cost
            chk.Add(chkWithCostWIP);

            //Age Range
            txt.Add(txtAgeFrom);
            txt.Add(txtAgeTo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad08_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12507))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12507)");
              //  rad08.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //As at date
            txt.Add(txtAsAt);
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

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);


            //With Cost
            chk.Add(chkWithCostWIP);

            //Age Range
            txt.Add(txtAgeFrom);
            txt.Add(txtAgeTo);

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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12508))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12508)");
                //rad09.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //As at date
            txt.Add(txtAsAt);
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

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);


            //With Cost
            chk.Add(chkWithCostWIP);

            //Age Range
            txt.Add(txtAgeFrom);
            txt.Add(txtAgeTo);

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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12509))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12509)");
                rad10.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);
            rad.Add(radioBoth);
            rad.Add(radioLocal);
            rad.Add(radioImport);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtSupplier);
            //txt.Add(txtAsAt);

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

            //== Added Udesh 18/Oct/2018====
            chkAllSupplier.Checked = true;
            chkAllDocType.Checked = true;
            chkAllCompany.Enabled = false;
            //===========================

            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;
            radioBoth.Checked = true;

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
            chk.Add(chkExportExcel);//Udesh 16-Oct-2018

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddCat4);
            lbtn.Add(lbtnAddCat5);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(lbtnSeSupplier);

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
            chk.Add(chkAllSupplier);
            lbtn.Add(btnDocType);

            //Doc no
            //txt.Add(txtDocNo); // Commented by Udesh 18/Oct/2018

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            BindAdminTeam();

        }

        protected void rad11_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12510))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12510)");
                rad11.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);
            rad.Add(radioBoth);
            rad.Add(radioLocal);
            rad.Add(radioImport);


            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtSupplier);
            //txt.Add(txtAsAt);

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
            lbtn.Add(lbtnSeSupplier);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkExportExcel);


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

            //Doc no
            txt.Add(txtDocNo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
            BindAdminTeam();
        }

        protected void rad12_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12511))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12511)");
                rad12.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

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

        protected void rad13_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12512))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12512)");
                rad13.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);
            rad.Add(radioBoth);
            rad.Add(radioLocal);
            rad.Add(radioImport);


            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtSupplier);
            //txt.Add(txtAsAt);

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
            lbtn.Add(lbtnSeSupplier);

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

            //Doc no
            txt.Add(txtDocNo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad14_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rad15_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12514))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12514)");
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
            Default();
            txt.Add(txtAsAt);

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
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12515))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12515)");
                rad16.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);
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

        protected void rad17_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12516))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12516)");
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
            Default();
            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //txt.Add(txtAsAt);

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

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);


            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad18_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12517))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12517)");
                rad18.Checked = false;
                return;
            }
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

            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //txt.Add(txtAsAt);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);


            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }

        protected void rad19_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12518))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12518)");
                rad19.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //txt.Add(txtAsAt);

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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            chk.Add(chkWithCostWIP);
            //With sts
            //  chk.Add(chkWithJob);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad20_CheckedChanged(object sender, EventArgs e)
        {

        }

        //Current age analysis(company)
        protected void rad51_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12520))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12520)");
                rad51.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            // txt.Add(txtAsAt);
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

            //With Cost
            chk.Add(chkWithCostWIP);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);

            rad.Add(radlocwise);
            rad.Add(radcatwise);
            rad.Add(radsubcatwise);
            rad.Add(radstatuswise);
            rad.Add(raditembrandwise);


            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad21_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12520))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12520)");
                rad21.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            // txt.Add(txtAsAt);
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

            //With Cost
            chk.Add(chkWithCostWIP);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);

            rad.Add(radlocwise);
            rad.Add(radcatwise);
            rad.Add(radsubcatwise);
            rad.Add(radstatuswise);
            rad.Add(raditembrandwise);


            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        //item age analysis with serial
        protected void rad22_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12521))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12521)");
                rad22.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();

            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtAgeFrom);
            txt.Add(txtAgeTo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }

        protected void rad23_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12522))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12522)");
                rad23.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
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

        protected void rad24_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rad25_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12525))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12525)");
                rad25.Checked = false;
                return;
            }
            List<TextBox> txt = new List<TextBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            txt.Add(txtDocNo);
            txt.Add(txtDocSubType);

            lbtn.Add(lbtnSeDocSubType);
            lbtn.Add(lbtnSeDocNo);
            _txtList = txt;
            _lbtnList = lbtn;
            EnableControler();
        }

        protected void rad26_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12525))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12525)");
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

            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            rad.Add(radioEtaDate);
            rad.Add(radioEtdDate);
            rad.Add(radioClearDate);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //txt.Add(txtAsAt);

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocSubType);
            lbtn.Add(btnDocSubType);


            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);
            txt.Add(txtGrnNo);
            lbtn.Add(btnGrnNo);
            txt.Add(txtBondNumber);
            lbtn.Add(btnBondNumber);
            txt.Add(txtRequestNo);
            lbtn.Add(btnRequestNo);
            txt.Add(txtOtherloc);
            lbtn.Add(btnotherloc);
            txt.Add(txtadmintm);
            lbtn.Add(btmadmintm);
            lbtn.Add(lbtnfromopteam);
            lbtn.Add(lbtntoopteam);

            // chk.Add(chkAllDirNo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }

        //item age analysis
        protected void rad27_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12526))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12526)");
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
            Default();
            // txt.Add(txtAsAt);
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
            chk.Add(chkWithCostWIP);

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
            //With sts
            // chk.Add(chkWithst);

            rad.Add(radlocwise);
            rad.Add(radcatwise);
            rad.Add(radsubcatwise);
            rad.Add(radstatuswise);
            rad.Add(raditembrandwise);

            ddl.Add(ddlYear);
            ddl.Add(ddlMonth);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad28_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12527))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12527)");
                //rad28.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);
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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            chk.Add(chkWithCostWIP);
            //With sts
            // chk.Add(chkWithst);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad29_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12528))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12528)");
                rad29.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtOtherloc);
            txt.Add(txtAsAt);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;
            chkWithSerial.Checked = true;

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            chk.Add(chkWithSerial);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad30_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12529))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12529)");
                rad30.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            txt.Add(txtOtherloc);
            txt.Add(txtAsAt);

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

            chkAllCat1.Checked = true;
            chkAllCat2.Checked = true;
            chkAllCat3.Checked = true;
            chkAllItemCode.Checked = true;
            chkAllBrand.Checked = true;
            chkAllModel.Checked = true;

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad39_CheckedChanged(object sender, EventArgs e)
        {
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            //txt.Add(txtFromDate);
            //txt.Add(txtToDate);

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
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
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

            rad.Add(radInv);
            rad.Add(radSale);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();


        }
        protected void rad36_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12535))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12535)");
                rad36.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtExDate);
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            //txt.Add(txtFromDate);
            //txt.Add(txtToDate);

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
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
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

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();


        }
        protected void rad35_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12534))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12534)");
                rad35.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtExDate);
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            //Item Criteria
            //txt.Add(txtCat1);
            //txt.Add(txtCat2);
            //txt.Add(txtCat3);
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            //txt.Add(txtFromDate);
            //txt.Add(txtToDate);

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
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
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

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();


        }
        protected void rad34_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12533))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12533)");
                rad34.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtReqfrom);
            txt.Add(txtReqto);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(btnReqfrom);
            lbtn.Add(btnReqto);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
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

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();


        }
        protected void rad33_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12532))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12532)");
                rad33.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
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

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();


        }

        protected void rad31_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12530))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12530)");
                rad31.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtBondNumber);
            txt.Add(txtRequestNo);
            txt.Add(txtGrnNo);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);
            lbtn.Add(btnBondNumber);
            lbtn.Add(btnGrnNo);
            lbtn.Add(btnRequestNo);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
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
            chk.Add(chkExportExcel);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad32_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12531))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12531)");
                rad32.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            rad.Add(radExcess);
            rad.Add(radShort);
            rad.Add(radBoth);

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            chk.Add(chkWithCostWIP);
            chk.Add(chkExportExcel);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }


        //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(ddlMonth.Text))
        //    {
        //        displayMessage("Select the year");

        //        return;
        //    }

        //    int month = ddlMonth.SelectedIndex + 1;
        //    int year = Convert.ToInt32(ddlYear.Text);

        //    int numberOfDays = DateTime.DaysInMonth(year, month);
        //    DateTime lastDay = new DateTime(year, month, numberOfDays);

        //    txtToDate.Text = lastDay.ToString("dd/MMM/yyyy");

        //    DateTime dtFrom = new DateTime(Convert.ToInt32(ddlYear.Text), month, 1);
        //    txtFromDate.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
        //    //Loadselectdate();
        //}

        //private void Loadselectdate()
        //{
        //    DateTime now = DateTime.Now;

        //    int d = (int)System.DateTime.Now.Day;

        //    txtFromDate.Text = d + "/" + ddlMonth.SelectedValue + "/" + ddlYear.Text;
        //    txtToDate.Text = d + "/" + ddlMonth.SelectedValue + "/" + ddlYear.Text;

        //}

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

        protected void rad38_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12537))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12537)");
                rad38.Checked = false;
                return;
            }
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            txt.Add(txtBondNumber);
            lbtn.Add(btnBondNumber);
        }

        protected void rad37_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12536))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12536)");
                rad37.Checked = false;
                return;
            }
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            txt.Add(txtBondNumber);
            lbtn.Add(btnBondNumber);
        }

        protected void rad40_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12539))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12539)");
                rad40.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
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
            txt.Add(txtFromDate);
            txt.Add(txtToDate);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeCat4);
            lbtn.Add(lbtnSeCat5);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);
            lbtn.Add(btnDownloadfile);
            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            rad.Add(raditmact);
            rad.Add(raditminact);
            rad.Add(radallitm);
            
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
            chk.Add(chkExportExcel);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
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

        protected void btnotherloc_Click(object sender, EventArgs e)
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

        protected void btnReqfrom_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ReqFromLoc";
                Session["ReqFromLoc"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReqFromLoc);
                DataTable _result = CHNLSVC.CommonSearch.SearchClompanyLocation(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ReqFromLoc"] = _result;
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

        protected void btnReqto_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "ReqToLoc";
                Session["ReqToLoc"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReqFromLoc);
                DataTable _result = CHNLSVC.CommonSearch.SearchClompanyLocation(para, null, null);
                dgvResultItem.DataSource = null;
                if (_result.Rows.Count > 0)
                {
                    dgvResultItem.DataSource = _result;
                    Session["ReqToLoc"] = _result;
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

        protected void txtReqfrom_TextChanged(object sender, EventArgs e)
        {
            if (txtReqfrom.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReqFromLoc);
                DataTable _result = CHNLSVC.CommonSearch.SearchClompanyLocation(para, "Location", txtReqfrom.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Location"].ToString()))
                    {
                        if (txtReqfrom.Text == row["Location"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Location"].ToString();
                            return;
                        }
                    }
                }
                txtReqfrom.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Location !!!')", true);
                    txtReqfrom.Text = "";
                    txtReqfrom.Focus();
                    return;
                }
            }
            else
            {
                txtReqfrom.ToolTip = "";
            }
        }

        protected void txtReqto_TextChanged(object sender, EventArgs e)
        {
            if (txtReqto.Text != "")
            {
                bool b2 = false;
                string toolTip = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReqFromLoc);
                DataTable _result = CHNLSVC.CommonSearch.SearchClompanyLocation(para, "Location", txtReqto.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Location"].ToString()))
                    {
                        if (txtReqto.Text == row["Location"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Location"].ToString();
                            return;
                        }
                    }
                }
                txtReqto.ToolTip = b2 ? toolTip : "";
                if (!b2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Location !!!')", true);
                    txtReqto.Text = "";
                    txtReqto.Focus();
                    return;
                }
            }
            else
            {
                txtReqto.ToolTip = "";
            }
        }

        protected void chkWithCostWIP_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16050))
            {
                displayMessage("You don't have the permission.\nPermission Code :- 16050");
            }
        }

        protected void rad41_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12540))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12540)");
                rad41.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);

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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();

        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(txtToDate.Text.ToString()), Convert.ToDateTime(txtFromDate.Text.ToString())) < 0)
            {
                displayMessage("From date cannot exceed to date");

                txtToDate.Text = "";
            }
        }

        protected void rad42_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12541))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12541)");
                rad42.Checked = false;
                return;
            }

            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);

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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

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

        protected void rad43_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12542))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12542)");
                rad43.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtCat4);
            txt.Add(txtCat5);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);

            txt.Add(txtFromDate);
            txt.Add(txtToDate);

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

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            txt.Add(txtoperteamfrom);
            txt.Add(txtoperteamto);
            lbtn.Add(lbtntoopteam);
            lbtn.Add(lbtnfromopteam);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
        }

        protected void rad44_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12543))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12543)");
                rad44.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();

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

            rad.Add(radExcess);
            rad.Add(radShort);
            rad.Add(radBoth);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;

            EnableControler();
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


        protected void radSale_Click(object sender, EventArgs e)
        {
            ucLoactionSearch.IscheckLocationValidation = false;
        }
        protected void radInv_Click(object sender, EventArgs e)
        {
            ucLoactionSearch.IscheckLocationValidation = true;
        }

        protected void rad46_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12545))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12545)");
                rad46.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtReqfrom);
            txt.Add(txtReqto);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(btnReqfrom);
            lbtn.Add(btnReqto);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            //chk.Add(chkWithCostWIP);
            txt.Add(txtOtherloc);
            lbtn.Add(btnotherloc);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }
        protected void rad45_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12544))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12544)");
                rad45.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtReqfrom);
            txt.Add(txtReqto);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(btnReqfrom);
            lbtn.Add(btnReqto);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);

            //With Cost
            //chk.Add(chkWithCostWIP);
            txt.Add(txtOtherloc);
            lbtn.Add(btnotherloc);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        //protected void rad46_CheckedChanged(object sender, EventArgs e)
        //{

        //}

        protected void rad48_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void rad47_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12546))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12546)");
                rad47.Checked = false;
                return;
            }
            
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtReqfrom);
            txt.Add(txtReqto);
            txt.Add(txtcustormer);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);
            lbtn.Add(lbtcussearch);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(btnReqfrom);
            lbtn.Add(btnReqto);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);
            txt.Add(txtRequestNo);
            lbtn.Add(btnRequestNo);

            //With Cost
            chk.Add(chkWithCostWIP);
            rad.Add(radrebond);
            rad.Add(radexbond);
            rad.Add(radBOI);
            rad.Add(radbondall);
            radrebond.Checked = true;
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();

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

        protected void lbtcussearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams2, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                dgvResultItem.DataSource = result2;
                dgvResultItem.DataBind();
                lblSearchType.Text = "cussearch";
                BindUCtrlDDLData(result2);
                txtSearchbyword.Text = "";
                Session["cussearch"] = result2;
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }
            }
            catch (Exception ex)
            {

            }
        }

        protected void rad49_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11548))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11548)");
            //    rad49.Checked = false;
            //    return;
            //}
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
            txt.Add(txtAsAt);

            //Item Criteria
            txt.Add(txtCat1);
            txt.Add(txtCat2);
            txt.Add(txtCat3);
            txt.Add(txtItemCode);
            txt.Add(txtBrand);
            txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtReqfrom);
            txt.Add(txtReqto);
            txt.Add(txtcustormer);

            lbtn.Add(lbtnSeCat1);
            lbtn.Add(lbtnSeCat2);
            lbtn.Add(lbtnSeCat3);
            lbtn.Add(lbtnSeItemCode);
            lbtn.Add(lbtSeBrand);
            lbtn.Add(lbtnSeModel);
            lbtn.Add(lbtcussearch);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            lbtn.Add(lbtnAddCat1);
            lbtn.Add(lbtnAddCat2);
            lbtn.Add(lbtnAddCat3);
            lbtn.Add(lbtnAddItemCode);
            lbtn.Add(lbtnAddBrand);
            lbtn.Add(lbtnAddModel);
            lbtn.Add(btnReqfrom);
            lbtn.Add(btnReqto);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //Item Sts
            txt.Add(txtItemStat);
            lbtn.Add(lbtnSeItemSta);
            chk.Add(chkAllStat);
            lbtn.Add(btnItemStats);
            txt.Add(txtRequestNo);
            lbtn.Add(btnRequestNo);

            //With Cost
            chk.Add(chkWithCostWIP);
            rad.Add(radrebond);
            rad.Add(radexbond);
            rad.Add(radBOI);
            rad.Add(radbondall);
            radrebond.Checked = true;
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad52_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11548))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :11548)");
            //    rad49.Checked = false;
            //    return;
            //}
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
            //txt.Add(txtItemCode);
            //txt.Add(txtBrand);
            //txt.Add(txtModel);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            txt.Add(txtDocType);
            txt.Add(txtDirection);
            txt.Add(txtDocNo);
            txt.Add(txtOtherloc);
            txt.Add(txtadmintm);
            //txt.Add(txtReqfrom);
            //txt.Add(txtReqto);
            //txt.Add(txtcustormer);

            lbtn.Add(lbtnSeDocType);
            lbtn.Add(lbtnSeDerection);
            lbtn.Add(lbtnSeDocNo);
            lbtn.Add(btnotherloc);
            lbtn.Add(btmadmintm);


            //lbtn.Add(lbtnSeCat2);
            //lbtn.Add(lbtnSeCat3);
            //lbtn.Add(lbtnSeItemCode);
            //lbtn.Add(lbtSeBrand);
            //lbtn.Add(lbtnSeModel);
            //lbtn.Add(lbtcussearch);

            chk.Add(chkAllDocType);
            chk.Add(chkAllDirNo);
            chk.Add(chkAllDocNo);
            //chk.Add(btnotherloc );
            //chk.Add(chkAllCat2);
            //chk.Add(chkAllCat3);
            //chk.Add(chkAllItemCode);
            //chk.Add(chkAllBrand);
            //chk.Add(chkAllModel);

            //lbtn.Add(lbtnAddCat1);
            //lbtn.Add(lbtnAddCat2);
            //lbtn.Add(lbtnAddCat3);
            //lbtn.Add(lbtnAddItemCode);
            //lbtn.Add(lbtnAddBrand);
            //lbtn.Add(lbtnAddModel);
            //lbtn.Add(btnReqfrom);
            //lbtn.Add(btnReqto);

            //lst.Add(listCat1);
            //lst.Add(listCat2);
            //lst.Add(listCat3);
            //lst.Add(listItemCode);
            //lst.Add(listBrand);
            //lst.Add(listModel);

            ////Item Sts
            //txt.Add(txtItemStat);
            //lbtn.Add(lbtnSeItemSta);
            //chk.Add(chkAllStat);
            //lbtn.Add(btnItemStats);
            //txt.Add(txtRequestNo);
            //lbtn.Add(btnRequestNo);

            ////With Cost
            //chk.Add(chkWithCostWIP);
            rad.Add(radTotRec);
            rad.Add(radPartRec);
            rad.Add(radNotRec);
            //rad.Add(radexbond);
            //rad.Add(radBOI);
            //rad.Add(radbondall);
            //radrebond.Checked = true;
            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        //protected void rad53_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12520))
        //    {
        //        displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12520)");
        //        rad51.Checked = false;
        //        return;
        //    }
        //    DesableAllControler();
        //    List<DropDownList> ddl = new List<DropDownList>();
        //    List<RadioButton> rad = new List<RadioButton>();
        //    List<TextBox> txt = new List<TextBox>();
        //    List<CheckBox> chk = new List<CheckBox>();
        //    List<LinkButton> lbtn = new List<LinkButton>();
        //    List<ListBox> lst = new List<ListBox>();
        //    Default();
        //    // txt.Add(txtAsAt);
        //    //Item Criteria
        //    //txt.Add(txtCat1);
        //    //txt.Add(txtCat2);
        //    //txt.Add(txtCat3);
        //    //txt.Add(txtCat4);
        //    //txt.Add(txtCat5);
        //    //txt.Add(txtItemCode);
        //    //txt.Add(txtBrand);
        //    //txt.Add(txtModel);

        //    chkAllCat1.Checked = true;
        //    chkAllCat2.Checked = true;
        //    chkAllCat3.Checked = true;
        //    chkAllCat4.Checked = true;
        //    chkAllCat5.Checked = true;
        //    chkAllItemCode.Checked = true;
        //    chkAllBrand.Checked = true;
        //    chkAllModel.Checked = true;

        //    lbtn.Add(lbtnSeCat1);
        //    lbtn.Add(lbtnSeCat2);
        //    lbtn.Add(lbtnSeCat3);
        //    lbtn.Add(lbtnSeCat4);
        //    lbtn.Add(lbtnSeCat5);
        //    lbtn.Add(lbtnSeItemCode);
        //    lbtn.Add(lbtSeBrand);
        //    lbtn.Add(lbtnSeModel);

        //    chk.Add(chkAllCat1);
        //    chk.Add(chkAllCat2);
        //    chk.Add(chkAllCat3);
        //    chk.Add(chkAllCat4);
        //    chk.Add(chkAllCat5);
        //    chk.Add(chkAllItemCode);
        //    chk.Add(chkAllBrand);
        //    chk.Add(chkAllModel);

        //    lbtn.Add(lbtnAddCat1);
        //    lbtn.Add(lbtnAddCat2);
        //    lbtn.Add(lbtnAddCat3);
        //    lbtn.Add(lbtnAddCat4);
        //    lbtn.Add(lbtnAddCat5);
        //    lbtn.Add(lbtnAddItemCode);
        //    lbtn.Add(lbtnAddBrand);
        //    lbtn.Add(lbtnAddModel);

        //    lst.Add(listCat1);
        //    lst.Add(listCat2);
        //    lst.Add(listCat3);
        //    lst.Add(listCat4);
        //    lst.Add(listCat5);
        //    lst.Add(listItemCode);
        //    lst.Add(listBrand);
        //    lst.Add(listModel);

        //    //With Cost
        //    chk.Add(chkWithCostWIP);

        //    //DOc Type
        //    txt.Add(txtDocType);
        //    lbtn.Add(lbtnSeDocType);
        //    chk.Add(chkAllDocSubType);
        //    lbtn.Add(btnDocSubType);

        //    rad.Add(radlocwise);
        //    rad.Add(radcatwise);
        //    rad.Add(radsubcatwise);
        //    rad.Add(radstatuswise);
        //    rad.Add(raditembrandwise);


        //    //Derection
        //    txt.Add(txtDirection);
        //    lbtn.Add(lbtnSeDerection);
        //    chk.Add(chkAllDirNo);

        //    _radioList = rad;
        //    _ddlList = ddl;
        //    _txtList = txt;
        //    _chkList = chk;
        //    _lbtnList = lbtn;
        //    _lstList = lst;
        //    EnableControler();
        //}

        //protected void rad54_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12520))
        //    {
        //        displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12520)");
        //        rad51.Checked = false;
        //        return;
        //    }
        //    DesableAllControler();
        //    List<DropDownList> ddl = new List<DropDownList>();
        //    List<RadioButton> rad = new List<RadioButton>();
        //    List<TextBox> txt = new List<TextBox>();
        //    List<CheckBox> chk = new List<CheckBox>();
        //    List<LinkButton> lbtn = new List<LinkButton>();
        //    List<ListBox> lst = new List<ListBox>();
        //    Default();
        //    // txt.Add(txtAsAt);
        //    //Item Criteria
        //    //txt.Add(txtCat1);
        //    //txt.Add(txtCat2);
        //    //txt.Add(txtCat3);
        //    //txt.Add(txtCat4);
        //    //txt.Add(txtCat5);
        //    //txt.Add(txtItemCode);
        //    //txt.Add(txtBrand);
        //    //txt.Add(txtModel);

        //    chkAllCat1.Checked = true;
        //    chkAllCat2.Checked = true;
        //    chkAllCat3.Checked = true;
        //    chkAllCat4.Checked = true;
        //    chkAllCat5.Checked = true;
        //    chkAllItemCode.Checked = true;
        //    chkAllBrand.Checked = true;
        //    chkAllModel.Checked = true;

        //    lbtn.Add(lbtnSeCat1);
        //    lbtn.Add(lbtnSeCat2);
        //    lbtn.Add(lbtnSeCat3);
        //    lbtn.Add(lbtnSeCat4);
        //    lbtn.Add(lbtnSeCat5);
        //    lbtn.Add(lbtnSeItemCode);
        //    lbtn.Add(lbtSeBrand);
        //    lbtn.Add(lbtnSeModel);

        //    chk.Add(chkAllCat1);
        //    chk.Add(chkAllCat2);
        //    chk.Add(chkAllCat3);
        //    chk.Add(chkAllCat4);
        //    chk.Add(chkAllCat5);
        //    chk.Add(chkAllItemCode);
        //    chk.Add(chkAllBrand);
        //    chk.Add(chkAllModel);

        //    lbtn.Add(lbtnAddCat1);
        //    lbtn.Add(lbtnAddCat2);
        //    lbtn.Add(lbtnAddCat3);
        //    lbtn.Add(lbtnAddCat4);
        //    lbtn.Add(lbtnAddCat5);
        //    lbtn.Add(lbtnAddItemCode);
        //    lbtn.Add(lbtnAddBrand);
        //    lbtn.Add(lbtnAddModel);

        //    lst.Add(listCat1);
        //    lst.Add(listCat2);
        //    lst.Add(listCat3);
        //    lst.Add(listCat4);
        //    lst.Add(listCat5);
        //    lst.Add(listItemCode);
        //    lst.Add(listBrand);
        //    lst.Add(listModel);

        //    //With Cost
        //    chk.Add(chkWithCostWIP);

        //    //DOc Type
        //    txt.Add(txtDocType);
        //    lbtn.Add(lbtnSeDocType);
        //    chk.Add(chkAllDocSubType);
        //    lbtn.Add(btnDocSubType);

        //    rad.Add(radlocwise);
        //    rad.Add(radcatwise);
        //    rad.Add(radsubcatwise);
        //    rad.Add(radstatuswise);
        //    rad.Add(raditembrandwise);


        //    //Derection
        //    txt.Add(txtDirection);
        //    lbtn.Add(lbtnSeDerection);
        //    chk.Add(chkAllDirNo);

        //    _radioList = rad;
        //    _ddlList = ddl;
        //    _txtList = txt;
        //    _chkList = chk;
        //    _lbtnList = lbtn;
        //    _lstList = lst;
        //    EnableControler();
        //}

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["IserID"].ToString(), 10141))
            {
                displayMessage("Sorry, You have no permission to view with cost!\n( Advice: Required permission code :10141 )");
                return;
            }
        }

        protected void rad55_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16079))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16079)");
                rad01.Checked = false;
                return;
            }
            ddlReportType.Enabled = true;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            ddlReportType.Items.Clear();
            List<CusdecTypes> rptTypes = CHNLSVC.Financial.GetCusdecTypeInfor("LK");
            ddlReportType.DataTextField = "Rcut_desc";
            ddlReportType.DataValueField = "Rcut_tp"; 
            ddlReportType.DataSource = rptTypes;
            ddlReportType.DataBind();
        }
        protected void rad56_CheckedChanged(object sender, EventArgs e)
        {

            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16106))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16106)");
                rad01.Checked = false;
                return;
            }
            DesableAllControler();
           // ddlReportType.Enabled = true;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            btnotherloc.Enabled = true;
            txtOtherloc.Enabled = true;
           // ddlReportType.Items.Clear();
            //List<CusdecTypes> rptTypes = CHNLSVC.Financial.GetCusdecTypeInfor("LK");
            //ddlReportType.DataTextField = "Rcut_desc";
            //ddlReportType.DataValueField = "Rcut_tp";
            //ddlReportType.DataSource = rptTypes;
            //ddlReportType.DataBind();
            //lbtn.Add(lbtusrsearch);
            txtUser.Enabled = true;
            lbtusrsearch.Enabled = true; 
            //List<TextBox> txt = new List<TextBox>();
        }

        protected void rad57_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16105))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16105)");
                rad57.Checked = false;
                return;
            }
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
         //   Default();
            //As at date
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
        //    txt.Add(txtAsAt);
            txtDocNo.Enabled = true;
            txtSupplier.Enabled = true;
            txtBrandMan.Enabled = true;
     

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

            //DOc Type
            txt.Add(txtDocType);
            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);

            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);


            //With Cost
            chk.Add(chkWithCostWIP);

            //Age Range
            txt.Add(txtAgeFrom);
            txt.Add(txtAgeTo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }


        protected void rad58_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 12509))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :12509)");
                rad58.Checked = false;
                return;
            }

            string Usercom = Session["UserCompanyCode"].ToString();

            DataTable dt = CHNLSVC.General.GET_REF_AGE_SLOT(Usercom);

            foreach (DataRow drow in dt.Rows)
            {
                DropDownListAging.Items.Add(drow["rags_slot_l1"].ToString());
                DropDownListAging.Items.Add(drow["rags_slot_l2"].ToString());
                DropDownListAging.Items.Add(drow["rags_slot_l3"].ToString());
                DropDownListAging.Items.Add(drow["rags_slot_l4"].ToString());
                DropDownListAging.Items.Add(drow["rags_slot_l5"].ToString());
                DropDownListAging.Items.Add(drow["rags_slot_g1"].ToString());
            }




            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            //   Default();
            //As at date
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            //    txt.Add(txtAsAt);
            txtDocNo.Enabled = true;
            txtSupplier.Enabled = true;
            txtBrandMan.Enabled = true;


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

            //DOc Type

            lbtn.Add(lbtnSeDocType);
            chk.Add(chkAllDocType);
            lbtn.Add(btnDocType);
            
            //Dropdown list
            ddl.Add(DropDownListAging);
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);
            //Derection
            txt.Add(txtDirection);
            lbtn.Add(lbtnSeDerection);
            chk.Add(chkAllDirNo);


            //With Cost
            chk.Add(chkWithCostWIP);

            //Age Range
            txt.Add(txtAgeFrom);
            txt.Add(txtAgeTo);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad59_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rad60_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16129))
            {
                displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16129)");
                rad60.Checked = false;
                return;
            }
            DesableAllControler();
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();
       
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
            //lbtn.Add(lbtnSeDocType);
            //lbtn.Add(lbtnSeDocSubType);
            //lbtn.Add(lbtnSeDerection);
            //lbtn.Add(btnotherloc);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);
            //chk.Add(chkWithCostWIP);
            //chk.Add(chkAllDocNo);
            //chk.Add(chkAllDocType);
            //chk.Add(chkAllDocSubType);
            //chk.Add(chkAllDirNo);
            //chk.Add(chkSumm);
            //chk.Add(chkNor);
            //chk.Add(chkDet);
            //chk.Add(chklist);

            //lbtn.Add(lbtnAddCat1);
            //lbtn.Add(lbtnAddCat2);
            //lbtn.Add(lbtnAddCat3);
            //lbtn.Add(lbtnAddCat4);
            //lbtn.Add(lbtnAddCat5);
            //lbtn.Add(lbtnAddItemCode);
            //lbtn.Add(lbtnAddBrand);
            //lbtn.Add(lbtnAddModel);
         //   lbtn.Add(lbtnSeDocNo);

            //lst.Add(listCat1);
          //  lst.Add(listCat2);
          ///  lst.Add(listCat3);
           // lst.Add(listCat4);
            //lst.Add(listCat5);
           // lst.Add(listItemCode);
            //lst.Add(listBrand);
           // lst.Add(listModel);

            //Date criteria
           // ddl.Add(ddlMonth);
           // ddl.Add(ddlYear);

           // rad.Add(radioEtaDate);
            //rad.Add(radioEtdDate);
           //// rad.Add(radioClearDate);

            txt.Add(txtAsAt);
            //txt.Add(txtFromDate);
            //txt.Add(txtToDate);
            //txt.Add(txtDocType);
            //txt.Add(txtDocSubType);
            //txt.Add(txtDocNo);
            //txt.Add(txtDirection);
            //txt.Add(txtBatchNo);
            //txt.Add(txtOtherloc);
        

            //DOc Type
            //txt.Add(txtDocType);
            //lbtn.Add(lbtnSeDocType);
            //chk.Add(chkAllDocSubType);
            //lbtn.Add(btnDocSubType);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }

        protected void rad61_CheckedChanged(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16130))
            //{
            //    displayMessage("Sorry, You have no permission to view this report! (Advice: Required permission code :16130)");
            //    rad61.Checked = false;
            //    return;
            //}
            DesableAllControler();
            DesableAllControler();
            List<DropDownList> ddl = new List<DropDownList>();
            List<RadioButton> rad = new List<RadioButton>();
            List<TextBox> txt = new List<TextBox>();
            List<CheckBox> chk = new List<CheckBox>();
            List<LinkButton> lbtn = new List<LinkButton>();
            List<ListBox> lst = new List<ListBox>();
            Default();

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
            //lbtn.Add(lbtnSeDocType);
            //lbtn.Add(lbtnSeDocSubType);
            //lbtn.Add(lbtnSeDerection);
            //lbtn.Add(btnotherloc);

            chk.Add(chkAllCat1);
            chk.Add(chkAllCat2);
            chk.Add(chkAllCat3);
            chk.Add(chkAllCat4);
            chk.Add(chkAllCat5);
            chk.Add(chkAllItemCode);
            chk.Add(chkAllBrand);
            chk.Add(chkAllModel);

            //lbtn.Add(lbtnAddCat1);
            //lbtn.Add(lbtnAddCat2);
            //lbtn.Add(lbtnAddCat3);
            //lbtn.Add(lbtnAddCat4);
            //lbtn.Add(lbtnAddCat5);
            //lbtn.Add(lbtnAddItemCode);
            //lbtn.Add(lbtnAddBrand);
            //lbtn.Add(lbtnAddModel);
            //lbtn.Add(lbtnSeDocNo);

            lst.Add(listCat1);
            lst.Add(listCat2);
            lst.Add(listCat3);
            lst.Add(listCat4);
            lst.Add(listCat5);
            lst.Add(listItemCode);
            lst.Add(listBrand);
            lst.Add(listModel);

            //Date criteria
            ddl.Add(ddlMonth);
            ddl.Add(ddlYear);

            //rad.Add(radioEtaDate);
            //rad.Add(radioEtdDate);
            //rad.Add(radioClearDate);

            //txt.Add(txtAsAt);
            txt.Add(txtFromDate);
            txt.Add(txtToDate);
            //txt.Add(txtDocType);
            //txt.Add(txtDocSubType);
            //txt.Add(txtDocNo);
            //txt.Add(txtDirection);
            //txt.Add(txtBatchNo);
            //txt.Add(txtOtherloc);


            //DOc Type
            //txt.Add(txtDocType);
            //lbtn.Add(lbtnSeDocType);
            //chk.Add(chkAllDocSubType);
            //lbtn.Add(btnDocSubType);

            _radioList = rad;
            _ddlList = ddl;
            _txtList = txt;
            _chkList = chk;
            _lbtnList = lbtn;
            _lstList = lst;
            EnableControler();
        }
        

        protected void lbtusrsearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                //string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                //DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams2, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                //CommonUIDefiniton.SearchUserControlType.UserID
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable result2 = CHNLSVC.CommonSearch.Get_All_Users(SearchParams, null, null);

                dgvResultItem.DataSource = result2;
                dgvResultItem.DataBind();
                lblSearchType.Text = "empsearch";
                BindUCtrlDDLData(result2);
                txtSearchbyword.Text = "";
                Session["empsearch"] = result2;
                txtSearchbyword.Focus();
                ItemPopup.Show();
                if (dgvResultItem.PageIndex > 0)
                { dgvResultItem.SetPageIndex(0); }



                //WarningUser.Visible = false;
                //SuccessUser.Visible = false;
                //errorDiv.Visible = false;
                //successDiv.Visible = false;
                //dvResultUser.DataSource = null;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                //_result = CHNLSVC.CommonSearch.Get_All_Users(SearchParams, null, null);
                //dvResultUser.DataSource = _result;
                //dvResultUser.DataBind();
                //BindUCtrlDDLData(_result);
                //UserPopoup.Show();
                //Label1.Text = "178";
            }
            catch (Exception ex)
            {

            }
        }

    }
}