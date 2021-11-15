using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours.UserControls;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

namespace FF.AbansTours
{
    public partial class CostingSheet : BasePage
    {
        private BasePage _basepage;
        private QUO_COST_HDR oHeader = null;
        private BasePage _basePage = null;
        private List<QUO_COST_DET> oMainItems = null;

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ChargeCodeTours:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlCostType.SelectedValue.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.TransferCodes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlCostType.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Miscellaneous:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlCostType.SelectedValue.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])))
                {
                    var ctrlName = Request.Params[Page.postEventSourceID];
                    var args = Request.Params[Page.postEventArgumentID];

                    if (ctrlName == txtCostSubType.UniqueID && args == "OnKeyPress")
                    {
                        MyTextBox_OnKeyPress(ctrlName, args);
                    }

                    if (!IsPostBack)
                    {
                        Session["CostSheetNumber"] = null;
                        Session["oMainItems"] = null;
                        _basepage = new BasePage();
                        loadCostCate();
                        LoadCurrancyCodes();
                        txtdate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                        lblClient.Text = Session["CustCode"].ToString();
                        lblEnquityID.Text = Session["EnquiryID"].ToString();
                        txtReffNum.Focus();
                        dgvCostSheet.DataSource = new int[] { };
                        dgvCostSheet.DataBind();
                        loadCostSheet();

                        btnAddtoGrid.Visible = false;
                    }
                }
                else
                {
                    //string gotoURL = "http://" + System.Web.HttpContext.Current.Request.Url.Host + @"/loginNew.aspx";
                    string gotoURL = "login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void MyTextBox_OnKeyPress(string ctrlName, string args)
        {
            DisplayMessages("test");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Session["oMainItems"] == null)
            {
                DisplayMessages("Please add records");
                return;
            }

            oMainItems = (List<QUO_COST_DET>)Session["oMainItems"];

            if (oMainItems.FindAll(x => x.QCD_SUB_CATE != "").Count == 0)
            {
                DisplayMessages("Please add records");
                return;
            }
            if (Session["oHeader"] != null)
            {
                oHeader = (QUO_COST_HDR)Session["oHeader"];
                oHeader.QCH_MARKUP = Convert.ToDecimal(txtMarkUpMain.Text);
            }
            else
            {
                oHeader = new QUO_COST_HDR();
                oHeader.QCH_SEQ = 0;
                oHeader.QCH_COM = Session["UserCompanyCode"].ToString();
                oHeader.QCH_SBU = Session["UserDefProf"].ToString();
                oHeader.QCH_COST_NO = string.Empty;
                oHeader.QCH_DT = Convert.ToDateTime(txtdate.Text);
                oHeader.QCH_OTH_DOC = lblEnquityID.Text;
                oHeader.QCH_REF = txtReffNum.Text;
                oHeader.QCH_CUS_CD = lblClient.Text;
                oHeader.QCH_CUS_NAME = "";
                oHeader.QCH_CUS_MOB = "";
                oHeader.QCH_TOT_PAX = Convert.ToInt32(txtTotalPAX.Text);
                oHeader.QCH_TOT_COST = Convert.ToDecimal(txtTotalUSD.Text);
                oHeader.QCH_TOT_COST_LOCAL = Convert.ToDecimal(txtTotalCostMain.Text);
                oHeader.QCH_MARKUP = Convert.ToDecimal(txtMarkUpMain.Text);
                oHeader.QCH_TOT_VALUE = Convert.ToDecimal(txtTotalMain.Text);
                oHeader.QCH_ACT = (Int32)ToursStatus.Pending;
                oHeader.QCH_SEND_CUS = 0;
                oHeader.QCH_CUS_SEND_DT = DateTime.MinValue.Date;
                oHeader.QCH_CUS_APP = 0;
                oHeader.QCH_CUS_APP_DT = DateTime.MinValue.Date;
                oHeader.QCH_ANAL1 = "";
                oHeader.QCH_ANAL2 = "";
                oHeader.QCH_ANAL3 = "";
                oHeader.QCH_ANAL4 = "";
                oHeader.QCH_ANAL5 = 0;
                oHeader.QCH_ANAL6 = 0;
                oHeader.QCH_ANAL7 = 0;
                oHeader.QCH_ANAL8 = DateTime.MinValue.Date;
                oHeader.QCH_CRE_BY = Session["UserID"].ToString();
                oHeader.QCH_CRE_DT = DateTime.Now;
                oHeader.QCH_MOD_BY = Session["UserID"].ToString();
                oHeader.QCH_MOD_DT = DateTime.Now;
            }

            MasterAutoNumber _Auto = new MasterAutoNumber();
            _Auto.Aut_cate_cd = Session["UserDefProf"].ToString();
            _Auto.Aut_cate_tp = "CTSHT";
            _Auto.Aut_direction = 1;
            _Auto.Aut_modify_dt = null;
            _Auto.Aut_moduleid = "CTSHT";
            _Auto.Aut_number = 0;
            _Auto.Aut_start_char = "CTSHT";
            _Auto.Aut_year = DateTime.Today.Year;

            string err;

            Int32 result = CHNLSVC.Tours.SaveCostingSheet(oHeader, oMainItems, _Auto, out err);
            if (result > 0)
            {
                string msg = "Costing sheet saved successfully. Cost sheet Num : " + err;
                DisplayMessages(msg);
                clearALl();
                btnBack_Click(null, null);
            }
            else
            {
                string msg = "Error Occur. Error : " + err;
                DisplayMessages(msg);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearALl();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Session["oMainItems"] = null;
            Session["oHeader"] = null;
            RedirectToBackPage();
        }

        protected void txtReffNum_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtReffNum.Text))
            {
                txtTotalPAX.Focus();
            }
        }

        protected void txtPAX_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotalPAX.Text))
            {
                if (Convert.ToInt32(txtTotalPAX.Text) < Convert.ToInt32(txtPAX.Text))
                {
                    DisplayMessages("PAX should be less than Total PAX");
                    txtPAX.Text = string.Empty;
                    txtUSD.Text = string.Empty;
                    txtTotal.Text = string.Empty;
                    txtTotalLKR.Text = string.Empty;
                    txtRemark.Text = string.Empty;
                    return;
                }
            }

            if (!String.IsNullOrEmpty(txtPAX.Text))
            {
                if (isdecimal(txtPAX.Text))
                {
                    if (Convert.ToDecimal(txtPAX.Text) <= 0)
                    {
                        DisplayMessages("Please enter valid PAX");
                        txtPAX.Text = "";
                        txtPAX.Focus();
                    }

                    ddlCostType.Focus();
                    if (!string.IsNullOrEmpty(txtCostSubType.Text))
                    {
                        getChargeCOdeDetails();
                        txtUSD.Focus();
                        return;
                    }
                }
                else
                {
                    DisplayMessages("Please enter valid PAX");
                    txtPAX.Text = "";
                    txtPAX.Focus();
                }
            }
        }

        protected void ddlCostType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCostSubType.Focus();
        }

        protected void txtCostSubType_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCostSubType.Text))
            {
                getChargeCodeDesc();
                txtPAX.Focus();
            }
        }

        protected void txtUSD_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPAX.Text))
            {
                DisplayMessages("Enter PAX");
                txtPAX.Focus();
                return;
            }
            if (!String.IsNullOrEmpty(txtUSD.Text))
            {
                if (isdecimal(txtUSD.Text) && Convert.ToDecimal(txtUSD.Text) > 0)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtPAX.Text) * Convert.ToDecimal(txtUSD.Text)).ToString("N2");
                    txtTotal.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(txtTotal.Text));
                    txtRemark.Focus();

                    if (ddlItmCurncy.SelectedValue != "")
                    {
                        MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                        decimal _exchangRate = 0;
                        MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), ddlItmCurncy.SelectedValue.ToString(), DateTime.Now, _pc.Mpc_def_exrate, Session["UserDefProf"].ToString());
                        if (_exc1 != null)
                        {
                            _exchangRate = _exc1.Mer_bnkbuy_rt;
                            txtTotalLKR.Text = (Convert.ToDecimal(txtTotal.Text) * _exchangRate).ToString();
                            txtTotalLKR.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(txtTotalLKR.Text));
                        }
                        else
                        {
                            DisplayMessages("please update exchange rates for selected currency");
                        }
                    }
                }
                else
                {
                    DisplayMessages("Please enter valid FARE USD");
                    txtUSD.Text = "";
                    txtUSD.Focus();
                }
            }
        }

        protected void txtTAX_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTAX.Text))
            {
                if (isdecimal(txtPAX.Text))
                {
                    txtTotal.Focus();
                }
                else
                {
                    DisplayMessages("Please enter valid TAX");
                    txtTAX.Text = "";
                    txtTAX.Focus();
                }
            }
        }

        protected void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTotal.Text))
            {
                if (isdecimal(txtPAX.Text))
                {
                    txtTotalLKR.Focus();
                }
                else
                {
                    DisplayMessages("Please enter valid total amount");
                    txtTotal.Text = "";
                    txtTotal.Focus();
                }
            }
        }

        protected void txtTotalLKR_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTotalLKR.Text))
            {
                txtRemark.Focus();
            }
        }

        protected void txtRemark_TextChanged(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(txtTotalLKR.Text))
            //{
            //    btnAddtoGrid.Focus();
            //}
        }

        protected void dgvCostSheet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "delete")
                {
                    GridViewRow row = dgvCostSheet.Rows[Convert.ToInt32(e.CommandArgument)];
                    Label lblQCD_CAT = (Label)row.FindControl("lblQCD_CAT");

                    oMainItems = (List<QUO_COST_DET>)Session["oMainItems"];
                    oMainItems.RemoveAt(Convert.ToInt32(e.CommandArgument));
                    Session["oMainItems"] = oMainItems;
                    calculateCategoryTotal(lblQCD_CAT.Text);
                    bindData();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void dgvCostSheet_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void dgvCostSheet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void txtMarkUpMain_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMarkUpMain.Text) && isdecimal(txtMarkUpMain.Text))
            {
                txtTotalMain.Text = (Convert.ToDecimal(txtTotalCostMain.Text) + (Convert.ToDecimal(txtTotalCostMain.Text) * Convert.ToDecimal(txtMarkUpMain.Text)) / 100).ToString("N2");
                txtPerPaxMain.Text = ((Convert.ToDecimal(txtTotalMain.Text)) / Convert.ToInt32(txtTotalPAX.Text)).ToString("N2");
            }
        }

        protected void btnAddtoGrid_Click(object sender, ImageClickEventArgs e)
        {
            if (validateADd())
            {
                if (Session["oMainItems"] == null)
                {
                    oMainItems = new List<QUO_COST_DET>();
                }
                else
                {
                    oMainItems = (List<QUO_COST_DET>)Session["oMainItems"];
                }
                QUO_COST_DET oItem = new QUO_COST_DET();
                oItem.QCD_SEQ = 0;
                oItem.QCD_COST_NO = string.Empty;
                oItem.QCD_CAT = ddlCostType.SelectedValue.ToString();
                oItem.QCD_SUB_CATE = txtCostSubType.Text;
                oItem.QCD_DESC = txtSubCostDesc.Text;
                oItem.QCD_CURR = ddlItmCurncy.SelectedValue.ToString();
                oItem.QCD_EX_RATE = Convert.ToDecimal(lblItemExRate.Text);
                oItem.QCD_QTY = Convert.ToInt32(txtPAX.Text);
                oItem.QCD_UNIT_COST = Convert.ToDecimal(txtUSD.Text);
                oItem.QCD_TAX = Convert.ToDecimal(txtTAX.Text);
                oItem.QCD_TOT_COST = Convert.ToDecimal(txtTotal.Text);
                oItem.QCD_TOT_LOCAL = Convert.ToDecimal(txtTotalLKR.Text);
                oItem.QCD_MARKUP = Convert.ToDecimal(txtItemMarkup.Text);
                oItem.QCD_AF_MARKUP = Convert.ToDecimal(txtTotalLKR.Text);
                oItem.QCD_RMK = txtRemark.Text;
                oItem.QCD_STATUS = 1;
                oItem.QCD_CRE_BY = Session["UserID"].ToString();
                oItem.QCD_CRE_DT = DateTime.Now;
                oItem.QCD_MOD_BY = Session["UserID"].ToString();
                oItem.QCD_MOD_DT = DateTime.Now;

                if (oMainItems.FindAll(x => x.QCD_SUB_CATE == "TOTAL" && x.QCD_CAT == oItem.QCD_CAT).Count > 0)
                {
                    List<QUO_COST_DET> oCatItems = oMainItems.FindAll(x => x.QCD_CAT == oItem.QCD_CAT);
                    int Index = oMainItems.IndexOf(oCatItems[oCatItems.Count - 1]);
                    //int Index = oMainItems.Count - 1;
                    oMainItems.Insert(Index, oItem);
                }
                else
                {
                    oMainItems.Add(oItem);
                }
                Session["oMainItems"] = oMainItems;
                calculateCategoryTotal(oItem.QCD_CAT);
                bindData();
                ClearEntryLine();

                //txtPAX.Enabled = false;
                ddlItmCurncy.Enabled = false;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            oHeader = (QUO_COST_HDR)Session["oHeader"];
            string err = string.Empty;
            Int32 stage = (int)EnquiryStages.Quotation_Approved;
            int result = CHNLSVC.Tours.UPDATE_COST_HDR_STATUS(stage, 1, oHeader.QCH_SEQ, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Session["UserID"].ToString(), lblEnquityID.Text.ToString(), out  err);

            if (result > 0)
            {
                if (chkSendSMS.Checked)
                {
                    OutSMS _out = new OutSMS();
                    String msg = "Dear Customer,\nYou cost sheet is finalized.\nEnquiry ID - " + lblEnquityID.Text + "\nTotal Value - " + txtTotalMain.Text + "\nReff Num :" + oHeader.QCH_SEQ.ToString() + " \nDo you want to approve?";

                    GEN_CUST_ENQ oEnquity = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), lblEnquityID.Text.Trim());
                    String mobi = "+94" + oEnquity.GCE_MOB.Substring(1, 9);
                    _out.Msgstatus = 0;
                    _out.Msgtype = "COST";
                    _out.Receivedtime = DateTime.Now;
                    _out.Receiver = mobi;
                    _out.Msg = msg;
                    _out.Receiverphno = mobi;
                    _out.Senderphno = mobi;
                    _out.Msgid = oHeader.QCH_SEQ.ToString();
                    _out.Refdocno = lblEnquityID.Text.Trim();
                    _out.Sender = "Abans Tours";
                    _out.Createtime = DateTime.Now;
                    result = CHNLSVC.Tours.SendSMS(_out, out err);

                    DisplayMessages("Cost sheet approved.");
                    clearALl();

                    return;
                }
                else
                {
                    DisplayMessages("Cost sheet approved.");
                    clearALl();
                }


            }
            else
            {
                DisplayMessages("Error occurred.");
            }
        }

        protected void ddlItmCurncy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItmCurncy.SelectedValue != "")
            {
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), ddlItmCurncy.SelectedValue.ToString(), DateTime.Now, _pc.Mpc_def_exrate, Session["UserDefProf"].ToString());
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                    lblItemExRate.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);

                    if (!String.IsNullOrEmpty(txtCostSubType.Text))
                    {
                        getChargeCOdeDetails();
                    }
                }
                else if (ddlItmCurncy.SelectedValue.ToString() == "LKR")
                {
                    _exchangRate = 1;
                    lblItemExRate.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", _exchangRate);

                    if (!String.IsNullOrEmpty(txtCostSubType.Text))
                    {
                        getChargeCOdeDetails();
                    }
                }
                else
                {
                    DisplayMessages("please update exchange rates for selected currency");
                }
            }
        }

        protected void txtItemMarkup_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemMarkup.Text) && isdecimal(txtItemMarkup.Text))
            {
                if (!string.IsNullOrEmpty(txtMarkUpMain.Text) && isdecimal(txtMarkUpMain.Text))
                {
                    //txtTotalLKR.Text = (Convert.ToDecimal(txtTotalLKR.Text) + (Convert.ToDecimal(txtTotalLKR.Text) * Convert.ToDecimal(txtItemMarkup.Text)) / 100).ToString("N2");
                    decimal TotalLocalCost = Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(lblItemExRate.Text);
                    decimal MarkupValue = TotalLocalCost * Convert.ToDecimal(txtItemMarkup.Text) / 100;
                    txtTotalLKR.Text = (TotalLocalCost + MarkupValue).ToString("N2");
                }
            }
        }

        protected void btnChargeType_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTotalPAX.Text) || Convert.ToDecimal(txtTotalPAX.Text) <= 0)
            {
                DisplayMessages("Please enter total PAX");
                txtTotalPAX.Focus();
                return;
            }

            if (ddlCostType.SelectedValue.ToString() == "AIRTVL")
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChargeCodeTours);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_ChargeCode(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtCostSubType.ClientID;
                ucc.UCModalPopupExtender.Show();
            }
            else if (ddlCostType.SelectedValue.ToString() == "TRANS")
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_TransferCode(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtCostSubType.ClientID;
                ucc.UCModalPopupExtender.Show();
            }
            else if (ddlCostType.SelectedValue.ToString() == "MSCELNS")
            {
                BasePage basepage = new BasePage();
                Page pp = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Miscellaneous);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_Miscellaneous(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtCostSubType.ClientID;
                ucc.UCModalPopupExtender.Show();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable salesDetails = new DataTable();
                //DataTable ProfitCenter = new DataTable();
                //DataTable mst_rec_tp = new DataTable();

                //salesDetails = CHNLSVC.Sales.GetReceipt(txtReceiptNo.Text);
                //ProfitCenter = CHNLSVC.Sales.GetProfitCenterTable(base.GlbUserComCode, Session["UserDefProf"].ToString());
                //mst_rec_tp = CHNLSVC.Sales.GetReceiptType(salesDetails.Rows[0]["SAR_RECEIPT_TYPE"].ToString());

                //if ((salesDetails.Rows.Count > 0))
                //{
                //    ReportDocument crystalReport = new ReportDocument();
                //    crystalReport.Load(Server.MapPath("/Reports_Module/Financial_Rep/receiptPrint_Report.rpt"));

                //    crystalReport.Database.Tables["salesDetails"].SetDataSource(salesDetails);
                //    crystalReport.Database.Tables["ProfitCenter"].SetDataSource(ProfitCenter);
                //    crystalReport.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);

                //    crvReceiptPrint.ReportSource = crystalReport;

                //    crvReceiptPrint.ToolPanelView = ToolPanelViewType.None;
                //    //crystalReport.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                //    //crystalReport.PrintOptions.PaperSize = PaperSize.PaperA4;
                //    crystalReport.PrintToPrinter(1, false, 0, 1);

                //    Clear_Data();

                //}

                //string receiptno = txtReceiptNo.Text;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Successfully saved. Invoice :" + receiptno +"');", true);
                //DisplayMessages("Successfully saved. Invoice : " + _invoiceNo);
                //Clear_Data();
                //Session["receiptno"] = receiptno;
                //mpReceiptPrint.Show();

                ////DataTable salesDetails = new DataTable();

                ////salesDetails = CHNLSVC.Sales.GetInvoiceDetailsForPrint(txtInoviceNo.Text);
                ////if ((salesDetails.Rows.Count > 0))
                ////{


                ////    ReportDocument crystalReport = new ReportDocument();
                ////    crystalReport.Load(Server.MapPath("Reports_Module\\Sales_Rep\\InvoiceCrystalReport.rpt"));
                ////    crystalReport.SetDataSource(salesDetails);
                ////    CrystalReportViewer1.ReportSource = crystalReport;
                ////    //CrystalReportViewer1.RefreshReport();
                ////    // CrystalReportViewer1.GroupTreeStyle=;
                ////}
                ////else
                ////{
                ////    DisplayMessages("Wrong Invoice No.");
                ////    CrystalReportViewer1.ReportSource = null;
                ////    HttpContext.Current.Response.Redirect("InvoicePrint.aspx");
                ////}

                //DataTable CostingReport = new DataTable();
                //CostingReport = CHNLSVC.Tours.Get_CostingFormat(lblCostSheetNumber.Text);

                //ReportDocument crystalReport = new ReportDocument();
                //crystalReport.Load(Server.MapPath("/Reports_Module/Financial_Rep/CostingFormat_Report.rpt"));
                //crystalReport.SetDataSource(CostingReport);
                //cryrCostingReport.ReportSource = crystalReport;

                //cryrCostingReport.ToolPanelView = ToolPanelViewType.None;


                ////PDF
                //if (string.IsNullOrEmpty(lblCostSheetNumber.Text) || (string.IsNullOrEmpty(lblStatus.Text)) || (lblStatus.Text != "Approved"))
                //{
                //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('CostSheetNumber requed');", true);
                //    return;
                //}
                //DataTable CostingReport = new DataTable();
                //BasePage basepage = new BasePage();

                //ReportDocument crystalReport = new ReportDocument();

                //CostingReport = basepage.CHNLSVC.Tours.Get_CostingFormat(lblCostSheetNumber.Text);

                ////crystalReport.Load(Server.MapPath("CostingFormat_Report.rpt"));
                ////crystalReport.Load(Server.MapPath("CostingFormat_Report.rpt"));
                ////crystalReport.SetDataSource(CostingReport);
                //CrystalReportSource1.ReportDocument.DataSourceConnections.Clear();
                //CrystalReportSource1.ReportDocument.Database.Tables["Costing_format"].SetDataSource(CostingReport);
                //CrystalReportSource1.ReportDocument.Refresh();
                ////CrystalReportSource1.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "CostingFormatReport");

                ////CrystalReport1 objRpt = new CrystalReport1();
                ////objRpt.SetDataSource(ds.Tables[0]);
                ////CrystalReportSource1.ReportDocument.DataSourceConnections.Clear();
                ////CrystalReportSource1.ReportDocument.Database.Tables["DataTable1"].SetDataSource(ds.Tables[0]);
                ////CrystalReportSource1.ReportDocument.Refresh();
                //// CrystalReportSource1.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "Report");
                //string date = DateTime.Now.ToString("ddMMMyyyy");
                //string time = DateTime.Now.ToString("hhmmss");
                //string ID = Convert.ToString(Session["UserID"]);
                //string FileName = time + ID;
                //string Path = Server.MapPath("css\\");
                //string NewPath = Path + date + "_" + time + "_" + ID + ".pdf";
                //CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, NewPath);

                //Response.Clear();
                //string filePath = NewPath;
                //Response.ContentType = "application/pdf";
                ////Response.TransmitFile(filePath);
                //Response.WriteFile(filePath);

                //Response.End();


                //popup, view and print
                if (string.IsNullOrEmpty(lblCostSheetNumber.Text) || (string.IsNullOrEmpty(lblStatus.Text)) || (lblStatus.Text != "Approved"))
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('CostSheet Number is required');", true);
                    return;
                }

                Session["CostSheetNumber"] = lblCostSheetNumber.Text;
                mpReceiptPrint.Show();

            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        private void loadCostCate()
        {
            List<MST_COST_CATE> oCate = _basepage.CHNLSVC.Tours.GET_COST_CATE(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            ddlCostType.DataSource = oCate;
            ddlCostType.DataTextField = "MCC_DESC";
            ddlCostType.DataValueField = "MCC_CD";
            ddlCostType.DataBind();
        }

        private void clearALl()
        {
            oHeader = new QUO_COST_HDR();
            oMainItems = new List<QUO_COST_DET>();

            Session["oHeader"] = null;
            Session["oMainItems"] = null;

            if (!string.IsNullOrEmpty(lblCostSheetNumber.Text))
            {
                RedirectToBackPage();
            }

            txtdate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
            txtReffNum.Text = "";
            txtPAX.Text = "";
            ddlItmCurncy.SelectedValue = "USD";
            ddlCostType.SelectedIndex = 0;
            txtCostSubType.Text = "";
            txtUSD.Text = "0.00";
            txtTAX.Text = "0.00";
            txtTotal.Text = "0.00";
            txtTotalLKR.Text = "0.00";
            txtRemark.Text = "";
            dgvCostSheet.DataSource = new int[] { };
            dgvCostSheet.DataBind();
            txtTotalUSD.Text = "0.00";
            txtTotalTax.Text = "0.00";
            txtTotalTotal.Text = "0.00";
            txtTotalLKR.Text = "0.00";
            txtTotalCostMain.Text = "0.00";
            TextBox6.Text = "";
            txtMarkUpMain.Text = "0.00";
            txtTotalMain.Text = "0.00";
            txtPerPaxMain.Text = "0.00";
            lblCostSheetNumber.Text = "";
            lblStatus.Text = "";
            hdfChargeDesc.Value = "";

            btnApprove.Visible = false;
            chkSendSMS.Visible = false;
            //txtPAX.Enabled = true;
            ddlItmCurncy.Enabled = true;

            txtSubCostDesc.Text = "";
            btnCreate.Enabled = true;

            txtTotalPAX.Text = "";

        }

        private void bindData()
        {
            Session["oMainItems"] = oMainItems;
            dgvCostSheet.DataSource = oMainItems;
            dgvCostSheet.DataBind();
            modifyGRD();
            calculateTotal();
        }

        private bool validateADd()
        {
            bool status = true;

            if (String.IsNullOrEmpty(txtPAX.Text))
            {
                DisplayMessages("Enter PAX");
                status = false;
                txtPAX.Focus();
                return status;
            }

            if (String.IsNullOrEmpty(txtReffNum.Text))
            {
                DisplayMessages("Enter Reference Number");
                status = false;
                txtReffNum.Focus();
                return status;
            }

            decimal asd1;
            if (String.IsNullOrEmpty(txtPAX.Text) && decimal.TryParse(txtPAX.Text, out asd1))
            {
                DisplayMessages("Enter Reference Number");
                status = false;
                txtPAX.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtCostSubType.Text))
            {
                DisplayMessages("Sub Type");
                status = false;
                txtCostSubType.Focus();
                return status;
            }

            if (String.IsNullOrEmpty(txtUSD.Text))
            {
                DisplayMessages("Enter Fare USD");
                status = false;
                txtUSD.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtTAX.Text))
            {
                DisplayMessages("Enter TAX");
                status = false;
                txtTAX.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtTotal.Text))
            {
                DisplayMessages("Enter Total");
                status = false;
                txtTotal.Focus();
                return status;
            }

            //getChargeCOdeDetails();

            if (String.IsNullOrEmpty(txtTotalLKR.Text))
            {
                DisplayMessages("Enter Total(LKR)");
                status = false;
                txtTotalLKR.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtRemark.Text))
            {
                DisplayMessages("Enter remark");
                status = false;
                txtRemark.Focus();
                return status;
            }
            if (Convert.ToDecimal(txtTotalPAX.Text) < Convert.ToDecimal(txtPAX.Text))
            {
                DisplayMessages("Total number of packs cannot be less than item line pax");
                status = false;
                txtTotalPAX.Focus();
                return status;
            }

            return status;
        }

        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }

        private bool isdecimal(string txt)
        {
            decimal asdasd;
            return decimal.TryParse(txt, out asdasd);
        }

        private void modifyGRD()
        {
            for (int i = 0; i < dgvCostSheet.Rows.Count; i++)
            {
                GridViewRow row = dgvCostSheet.Rows[i];
                Label lblQCD_UNIT_COST = (Label)row.FindControl("lblQCD_UNIT_COST");
                Label lblQCD_SEQLocal = (Label)row.FindControl("lblQCD_SEQLocal");
                Label lblQCD_TAX = (Label)row.FindControl("lblQCD_TAX");
                Label lblQCD_TOT_COST = (Label)row.FindControl("lblQCD_TOT_COST");
                Label lblQCD_TOT_LOCAL = (Label)row.FindControl("lblQCD_TOT_LOCAL");
                Label lblQCD_QTY = (Label)row.FindControl("lblQCD_QTY");
                Label lblQCD_MARKUP = (Label)row.FindControl("lblQCD_MARKUP");

                lblQCD_UNIT_COST.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblQCD_UNIT_COST.Text));
                lblQCD_SEQLocal.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblQCD_SEQLocal.Text));
                lblQCD_TAX.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblQCD_TAX.Text));
                lblQCD_TOT_COST.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblQCD_TOT_COST.Text));
                lblQCD_TOT_LOCAL.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblQCD_TOT_LOCAL.Text));

                Label lblQCD_SUB_CATE = (Label)row.FindControl("lblQCD_SUB_CATE");
                if (lblQCD_SUB_CATE.Text == "TOTAL")
                {
                    lblQCD_MARKUP.Text = string.Empty;
                    lblQCD_QTY.Text = string.Empty;
                    lblQCD_UNIT_COST.Text = string.Empty;
                    row.BackColor = System.Drawing.Color.LightPink;
                    ImageButton btn = (ImageButton)row.FindControl("btndelete");
                    btn.Visible = false;
                }
            }
        }

        private void calculateCategoryTotal(String category)
        {
            if (Session["oMainItems"] != null)
            {
                oMainItems = (List<QUO_COST_DET>)Session["oMainItems"];
                if (oMainItems.FindAll(x => x.QCD_SUB_CATE == "TOTAL" && x.QCD_CAT == category).Count > 0)
                {
                    List<QUO_COST_DET> oCatItems = oMainItems.FindAll(x => x.QCD_CAT == category && x.QCD_SUB_CATE != "TOTAL");
                    if (oCatItems.Count > 0)
                    {
                        QUO_COST_DET oTotalItem = oMainItems.Find(x => x.QCD_SUB_CATE == "TOTAL" && x.QCD_CAT == category);
                        //oTotalItem.QCD_UNIT_COST = oCatItems.Sum(x => x.QCD_UNIT_COST);
                        oTotalItem.QCD_TAX = oCatItems.Sum(x => x.QCD_TAX);
                        oTotalItem.QCD_TOT_COST = oCatItems.Sum(x => x.QCD_TOT_COST);
                        oTotalItem.QCD_TOT_LOCAL = oCatItems.Sum(x => x.QCD_TOT_LOCAL);
                        //oTotalItem.QCD_MARKUP = oCatItems.Sum(x => x.QCD_MARKUP);
                        oTotalItem.QCD_AF_MARKUP = oCatItems.Sum(x => x.QCD_AF_MARKUP);
                        Session["oMainItems"] = oMainItems;
                    }
                    else
                    {
                        Session["oMainItems"] = oMainItems.RemoveAll(x => x.QCD_SUB_CATE == "TOTAL" && x.QCD_CAT == category);
                    }
                }
                else
                {
                    List<QUO_COST_DET> oCatItems = oMainItems.FindAll(x => x.QCD_CAT == category);
                    QUO_COST_DET oTotalItem = new QUO_COST_DET();
                    oTotalItem.QCD_CAT = category;
                    oTotalItem.QCD_SUB_CATE = "TOTAL";
                    //oTotalItem.QCD_UNIT_COST = oCatItems.Sum(x => x.QCD_UNIT_COST);
                    oTotalItem.QCD_TAX = oCatItems.Sum(x => x.QCD_TAX);
                    oTotalItem.QCD_TOT_COST = oCatItems.Sum(x => x.QCD_TOT_COST);
                    oTotalItem.QCD_TOT_LOCAL = oCatItems.Sum(x => x.QCD_TOT_LOCAL);
                    //oTotalItem.QCD_MARKUP = oCatItems.Sum(x => x.QCD_MARKUP);
                    oTotalItem.QCD_AF_MARKUP = oCatItems.Sum(x => x.QCD_AF_MARKUP);
                    int CateLastIndex = oMainItems.IndexOf(oCatItems[oCatItems.Count - 1]) + 1;
                    oMainItems.Insert(CateLastIndex, oTotalItem);
                    Session["oMainItems"] = oMainItems;
                }

                if (oMainItems.Count == 0)
                {
                    //txtPAX.Enabled = true;
                    ddlItmCurncy.Enabled = true;

                }
            }
        }

        private void calculateTotal()
        {
            if (Session["oMainItems"] != null)
            {
                oMainItems = (List<QUO_COST_DET>)Session["oMainItems"];
                List<QUO_COST_DET> oCatItems = oMainItems.FindAll(x => x.QCD_SUB_CATE != "TOTAL");
                txtMarkUpMain.Text = "0.00";
                txtTotalMain.Text = oCatItems.Sum(x => x.QCD_TOT_LOCAL).ToString();

                txtTotalUSD.Text = oCatItems.Sum(x => x.QCD_UNIT_COST).ToString("N2");
                txtTotalTax.Text = oCatItems.Sum(x => x.QCD_TAX).ToString("N2");
                txtTotalTotal.Text = oCatItems.Sum(x => x.QCD_TOT_COST).ToString("N2");
                txtTotalCostMain.Text = oCatItems.Sum(x => x.QCD_TOT_LOCAL).ToString();

                txtPerPaxMain.Text = (oCatItems.Sum(x => x.QCD_TOT_LOCAL) / Convert.ToInt32(txtTotalPAX.Text)).ToString("N2");

                txtTotalCostMain.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(txtTotalCostMain.Text));
                txtMarkUpMain.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(txtMarkUpMain.Text));
                txtTotalMain.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(txtTotalMain.Text));
                txtTotalMain.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(txtTotalMain.Text));

                if (!string.IsNullOrEmpty(txtMarkUpMain.Text) && isdecimal(txtMarkUpMain.Text))
                {
                    txtTotalMain.Text = (Convert.ToDecimal(txtTotalCostMain.Text) - (Convert.ToDecimal(txtTotalCostMain.Text) * Convert.ToDecimal(txtMarkUpMain.Text)) / 100).ToString("N2");
                    txtPerPaxMain.Text = ((Convert.ToDecimal(txtTotalMain.Text)) / Convert.ToInt32(txtTotalPAX.Text)).ToString("N2");
                }
            }
        }

        private void RedirectToBackPage()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["RedirectPage"])))
            {
                Session["isComingBack"] = "1";
                Response.Redirect(Session["RedirectPage"].ToString() + "?htenus=customer");
            }
        }

        private void LoadCurrancyCodes()
        {
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            if (_cur != null)
            {
                ddlItmCurncy.DataSource = _cur;
                ddlItmCurncy.DataTextField = "Mcr_cd";
                ddlItmCurncy.DataValueField = "Mcr_cd";
                ddlItmCurncy.DataBind();
                ddlItmCurncy.SelectedValue = "USD";
                ddlItmCurncy_SelectedIndexChanged(null, null);
            }
        }

        private void getExchangeRate()
        {
        }

        private void loadCostSheet()
        {
            string err;
            Int32 result = CHNLSVC.Tours.getCostSheetDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Session["EnquiryID"].ToString(), "1,2", out oHeader, out oMainItems, out err);

            lblStatus.Text = "";
            lblCostSheetNumber.Text = "";

            if (oMainItems.Count > 0)
            {
                Session["oMainItems"] = oMainItems;
                Session["oHeader"] = oHeader;

                txtReffNum.Text = oHeader.QCH_REF;
                txtdate.Text = oHeader.QCH_DT.ToString("dd/MMM/yyyy");
                txtTotalPAX.Text = oHeader.QCH_TOT_PAX.ToString();
                lblCostSheetNumber.Text = oHeader.QCH_COST_NO;

                switch (oHeader.QCH_ACT)
                {
                    case 1:
                        lblStatus.Text = GetEnumDesc.GetEnumDescription(ToursStatus.Approved);
                        btnCreate.Enabled = false;
                        break;

                    case 2:
                        lblStatus.Text = GetEnumDesc.GetEnumDescription(ToursStatus.Pending);
                        break;

                    case 0:
                        lblStatus.Text = GetEnumDesc.GetEnumDescription(ToursStatus.Cancel);
                        break;
                }

                List<String> oCateLists = oMainItems.Select(x => x.QCD_CAT).Distinct().ToList();
                foreach (String oCateList in oCateLists)
                {
                    calculateCategoryTotal(oCateList);
                    bindData();
                }

                txtMarkUpMain.Text = oHeader.QCH_MARKUP.ToString();
                txtMarkUpMain_TextChanged(null, null);
                if (oHeader.QCH_ACT == 2)
                {
                    btnApprove.Visible = true;
                    chkSendSMS.Visible = true;
                }
            }
        }

        private void ClearEntryLine()
        {
            ddlCostType.SelectedIndex = 0;
            txtCostSubType.Text = "";
            txtUSD.Text = "";
            txtTAX.Text = "0.00";
            txtTotal.Text = "0.00";
            txtTotalLKR.Text = "0.00";
            txtRemark.Text = "";
            ddlCostType.Focus();
            txtItemMarkup.Text = "0.00";
            txtPAX.Text = "";
        }

        private void getChargeCOdeDetails()
        {
            if (ddlCostType.SelectedValue.ToString() == "AIRTVL")
            {
                SR_AIR_CHARGE oSR_AIR_CHARGE = CHNLSVC.Tours.GetChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text);
                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.SAC_CD != null)
                {
                    //ddlItmCurncy.SelectedValue = oSR_AIR_CHARGE.SAC_CUR;
                    //ddlItmCurncy_SelectedIndexChanged(null, null);
                    if (txtUSD.Text == "")
                    {
                        txtUSD.Text = oSR_AIR_CHARGE.SAC_RT.ToString();
                    }
                  
                    //hdfChargeDesc.Value = oSR_AIR_CHARGE.SAC_ADD_DESC;
                    //txtSubCostDesc.Text = oSR_AIR_CHARGE.SAC_ADD_DESC;

                    if (!string.IsNullOrEmpty(txtPAX.Text))
                    {
                        txtTotal.Text = (Convert.ToDecimal(txtUSD.Text) * Convert.ToDecimal(txtPAX.Text)).ToString();
                        txtTotalLKR.Text = (Convert.ToDecimal(txtUSD.Text) * Convert.ToDecimal(txtPAX.Text) * Convert.ToDecimal(lblItemExRate.Text)).ToString();

                        if (!string.IsNullOrEmpty(txtMarkUpMain.Text) && isdecimal(txtMarkUpMain.Text) && Convert.ToDecimal(txtMarkUpMain.Text) > 0)
                        {
                            //txtTotalLKR.Text = (Convert.ToDecimal(txtTotalLKR.Text) + (Convert.ToDecimal(txtTotalLKR.Text) * Convert.ToDecimal(txtItemMarkup.Text)) / 100).ToString("N2");
                            decimal TotalLocalCost = Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(lblItemExRate.Text);
                            decimal MarkupValue = TotalLocalCost * Convert.ToDecimal(txtItemMarkup.Text) / 100;
                            txtTotalLKR.Text = (TotalLocalCost + MarkupValue).ToString("N2");
                        }
                        txtRemark.Focus();
                    }
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtCostSubType.Text = "";
                    ClearEntryLine();
                    txtCostSubType.Focus();
                }
            }
            else if (ddlCostType.SelectedValue.ToString() == "TRANS")
            {
                SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text);
                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.STC_CD != null)
                {
                    //ddlItmCurncy.SelectedValue = oSR_AIR_CHARGE.STC_CURR;
                    //ddlItmCurncy_SelectedIndexChanged(null, null);
                    if (txtUSD.Text == "")
                    {
                        txtUSD.Text = oSR_AIR_CHARGE.STC_RT.ToString();
                    }
                 
                    //hdfChargeDesc.Value = oSR_AIR_CHARGE.STC_DESC;
                    //txtSubCostDesc.Text = oSR_AIR_CHARGE.STC_DESC;

                    if (!string.IsNullOrEmpty(txtPAX.Text))
                    {
                        txtTotal.Text = "";
                        txtTotal.Text = (Convert.ToDecimal(txtUSD.Text) * Convert.ToDecimal(txtPAX.Text)).ToString();
                        txtTotalLKR.Text = (Convert.ToDecimal(txtUSD.Text) * Convert.ToDecimal(lblItemExRate.Text)).ToString();
                        txtRemark.Focus();

                        if (!string.IsNullOrEmpty(txtMarkUpMain.Text) && isdecimal(txtMarkUpMain.Text))
                        {
                            //txtTotalLKR.Text = (Convert.ToDecimal(txtTotalLKR.Text) + (Convert.ToDecimal(txtTotalLKR.Text) * Convert.ToDecimal(txtItemMarkup.Text)) / 100).ToString("N2");
                            decimal TotalLocalCost = Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(lblItemExRate.Text);
                            decimal MarkupValue = TotalLocalCost * Convert.ToDecimal(txtItemMarkup.Text) / 100;
                            txtTotalLKR.Text = (TotalLocalCost + MarkupValue).ToString("N2");
                        }
                    }
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtCostSubType.Text = "";
                    ClearEntryLine();
                    txtCostSubType.Focus();
                }
            }
            else if (ddlCostType.SelectedValue.ToString() == "MSCELNS")
            {
                SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text);
                if (oSR_SER_MISS != null && oSR_SER_MISS.SSM_CD != null)
                {
                    //ddlItmCurncy.SelectedValue = oSR_SER_MISS.SSM_CUR;
                    //ddlItmCurncy_SelectedIndexChanged(null, null);
                    if (txtUSD.Text == "")
                    {
                        txtUSD.Text = oSR_SER_MISS.SSM_RT.ToString();
                    }
                  
                   
                    //hdfChargeDesc.Value = oSR_SER_MISS.SSM_DESC;
                    //txtSubCostDesc.Text = oSR_SER_MISS.SSM_DESC;

                    if (!string.IsNullOrEmpty(txtPAX.Text))
                    {
                        txtTotal.Text = (Convert.ToDecimal(txtUSD.Text) * Convert.ToDecimal(txtPAX.Text)).ToString();
                        txtTotalLKR.Text = (Convert.ToDecimal(txtUSD.Text) * Convert.ToDecimal(lblItemExRate.Text)).ToString();
                        txtRemark.Focus();

                        if (!string.IsNullOrEmpty(txtMarkUpMain.Text) && isdecimal(txtMarkUpMain.Text))
                        {
                            //txtTotalLKR.Text = (Convert.ToDecimal(txtTotalLKR.Text) + (Convert.ToDecimal(txtTotalLKR.Text) * Convert.ToDecimal(txtItemMarkup.Text)) / 100).ToString("N2");
                            decimal TotalLocalCost = Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(lblItemExRate.Text);
                            decimal MarkupValue = TotalLocalCost * Convert.ToDecimal(txtItemMarkup.Text) / 100;
                            txtTotalLKR.Text = (TotalLocalCost + MarkupValue).ToString("N2");
                        }
                    }
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtCostSubType.Text = "";
                    ClearEntryLine();
                    txtCostSubType.Focus();
                }
            }
            else
            {
                hdfChargeDesc.Value = "";
                hdfChargeDesc.Value = "";
                ClearEntryLine();
            }
        }

        private void getChargeCodeDesc()
        {
            if (ddlCostType.SelectedValue.ToString() == "AIRTVL")
            {
                SR_AIR_CHARGE oSR_AIR_CHARGE = CHNLSVC.Tours.GetChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text);
                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.SAC_CD != null)
                {
                    txtSubCostDesc.Text = oSR_AIR_CHARGE.SAC_ADD_DESC;
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtCostSubType.Text = "";
                    txtCostSubType.Focus();
                }
            }
            else if (ddlCostType.SelectedValue.ToString() == "TRANS")
            {
                SR_TRANS_CHA oSR_AIR_CHARGE = CHNLSVC.Tours.GetTransferChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text);
                if (oSR_AIR_CHARGE != null && oSR_AIR_CHARGE.STC_CD != null)
                {
                    txtSubCostDesc.Text = oSR_AIR_CHARGE.STC_DESC;
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtCostSubType.Text = "";
                    ClearEntryLine();
                    txtCostSubType.Focus();
                }
            }
            else if (ddlCostType.SelectedValue.ToString() == "MSCELNS")
            {
                SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text);
                if (oSR_SER_MISS != null && oSR_SER_MISS.SSM_CD != null)
                {
                    txtSubCostDesc.Text = oSR_SER_MISS.SSM_DESC;
                }
                else
                {
                    DisplayMessages("Please enter valid charge code");
                    txtCostSubType.Text = "";
                    ClearEntryLine();
                    txtCostSubType.Focus();
                }
            }
        }

        protected void Close_Click(object sender, EventArgs e)
        {
            mpReceiptPrint.Hide();
        }

        protected void btnAddtoGrid2_Click(object sender, EventArgs e)
        {
            btnAddtoGrid_Click(null, null);
        }

        protected void txtTotalPAX_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotalPAX.Text) && isdecimal(txtTotalPAX.Text))
            {
                ddlCostType.Focus();
            }
        }

        #region Modalpopup
        protected void grdResultsearch_SelectedIndexChanged(object sender, EventArgs e)
        {

            string ID = (grdResultsearch.SelectedRow.FindControl("col_stc_rt") as Label).Text;
            txtUSD.Text = ID;
            getChargeCOdeDetails();
        }
        protected void gridair_SelectedIndexChanged(object sender, EventArgs e)
        {

            string ID = grdResultsearch.SelectedRow.Cells[5].Text;
            txtUSD.Text = ID;

        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultsearch.PageIndex = e.NewPageIndex;
    
          
          
        }
        protected void gridair_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridair.PageIndex = e.NewPageIndex;



        }
     
        #endregion

        protected void Imgbtncost_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCostSubType.Text))
            {
                DisplayMessages("Plase enter Sub type");
                return;
            }
            if (string.IsNullOrEmpty(txtPAX.Text))
            {
                DisplayMessages("Plase enter pax");
                return;
            }
            if (ddlCostType.SelectedValue.ToString() == "AIRTVL")
            {
                List<SR_AIR_CHARGE> _list_air_charge = new List<SR_AIR_CHARGE>();
                _list_air_charge = CHNLSVC.Tours.GetALLChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text.Trim(), DateTime.Now.Date);
                gridair.DataSource = _list_air_charge;
                gridair.DataBind();

            }
            else if (ddlCostType.SelectedValue.ToString() == "TRANS")
            {
                List<SR_TRANS_CHA> _list_TVl_charge = new List<SR_TRANS_CHA>();
                _list_TVl_charge = CHNLSVC.Tours.GetAllTransferChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text.Trim(), DateTime.Now.Date);
                grdResultsearch.DataSource = _list_TVl_charge;
                grdResultsearch.DataBind();
                UserPopoup.Show();
            }
            else if (ddlCostType.SelectedValue.ToString() == "MSCELNS")
            {
                List<SR_SER_MISS> _list_MIS_charge = new List<SR_SER_MISS>();
                _list_MIS_charge = CHNLSVC.Tours.GetALLMiscellaneousChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlCostType.SelectedValue.ToString(), txtCostSubType.Text, DateTime.Now.Date);
                grdMiscellaneous.DataSource = _list_MIS_charge;
                grdMiscellaneous.DataBind();
                UserPopupMer.Show();
                UserPopoup.Hide();
            }
           
        }

    }
}
