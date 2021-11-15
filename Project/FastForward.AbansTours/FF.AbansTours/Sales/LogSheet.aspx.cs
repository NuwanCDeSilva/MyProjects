using FF.AbansTours.UserControls;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.Sales
{
    public partial class LogSheet : BasePage
    {
        private BasePage _basePage;
        private List<TR_LOGSHEET_DET> oMainItems = new List<TR_LOGSHEET_DET>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserSubChannl"]))
                    )
                {
                    lbtColse_Click(null, null);
                    if (!IsPostBack)
                    {
                        Session["oMainItems"] = null;
                        ClearAll();
                    }
                }
                else
                {
                    string gotoURL = "~/login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        #region Main Buttons
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (validateHeader())
            {
                if (Session["oMainItems"] != null)
                {
                    oMainItems = (List<TR_LOGSHEET_DET>)Session["oMainItems"];
                }
                else
                {
                    DisplayMessage("please add items to save", 1);
                    return;
                }
                bool isNew = true;
                String err = string.Empty;
                String logNumber = string.Empty;

                TR_LOGSHEET_HDR oHeader = new TR_LOGSHEET_HDR();
                oHeader.TLH_SEQ = 0;
                oHeader.TLH_COM = txtCompany.Text.Trim();
                oHeader.TLH_PC = txtPC.Text.Trim();
                oHeader.TLH_LOG_NO = txtLogSheetNo.Text.Trim();
                oHeader.TLH_DT = Convert.ToDateTime(txtDate.Text);
                oHeader.TLH_REQ_NO = txtEnquiryID.Text.Trim();

                DateTime date = DateTime.Now;
                TimeSpan time = new TimeSpan(0, tmStartTime.Hour, tmStartTime.Minute, 0);
                oHeader.TLH_ST_DT = Convert.ToDateTime(txtStartDate.Text).Add(time);

                date = DateTime.Now;
                time = new TimeSpan(0, tmEndTime.Hour, tmEndTime.Minute, 0);
                oHeader.TLH_ED_DT = Convert.ToDateTime(txtEndDate.Text).Add(time);

                oHeader.TLH_CUS_CD = txtCustomerCode.Text.Trim();
                oHeader.TLH_DRI_CD = txtDriverCode.Text.Trim();
                oHeader.TLH_GUST = txtGuest.Text.Trim();
                oHeader.TLH_FLEET = txtFleet.Text.Trim();
                oHeader.TLH_RMK = txtRemark.Text.Trim();
                oHeader.TLH_INV_MIL = Convert.ToDecimal(txtInvoiceMileage.Text);
                oHeader.TLH_DRI_MIL = Convert.ToDecimal(txtDriverMileage.Text);
                oHeader.TLH_MET_IN = Convert.ToDecimal(txtMeterIn.Text);
                oHeader.TLH_MET_OUT = Convert.ToDecimal(txtMeterOut.Text);
                oHeader.TLH_CRE_BY = Session["userID"].ToString();
                oHeader.TLH_CRE_DT = DateTime.Now;
                oHeader.TLH_MOD_BY = Session["userID"].ToString();
                oHeader.TLH_MOD_DT = DateTime.Now;
                oHeader.TLH_ANAL1 = String.Empty;
                oHeader.TLH_ANAL2 = String.Empty;
                oHeader.TLH_ANAL3 = String.Empty;
                oHeader.TLH_ANAL4 = 0;
                oHeader.TLH_ANAL5 = 0;
                oHeader.TLH_PAY_DRI = 0;
                oHeader.TLH_INV = 0;

                MasterAutoNumber _Auto = new MasterAutoNumber();
                _Auto.Aut_cate_cd = Session["UserDefProf"].ToString();
                _Auto.Aut_cate_tp = "LOGSHT";
                _Auto.Aut_direction = 1;
                _Auto.Aut_modify_dt = null;
                _Auto.Aut_moduleid = "LOGSHT";
                _Auto.Aut_number = 0;
                _Auto.Aut_start_char = "LOGSHT";
                _Auto.Aut_year = DateTime.Today.Year;

                Int32 result = CHNLSVC.Tours.saveLogSheet(oHeader, oMainItems, isNew, _Auto, out err, out logNumber);

                if (result > 0)
                {
                    DisplayMessage("Successfully saved. Log sheet number : " + logNumber, 1);
                    ClearAll();
                }
                else
                {
                    DisplayMessage("Process Terminated. Log sheet number : " + logNumber, 2);
                }
            }
        }



        #endregion

        #region Methods

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ToursFacCompany:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "TNSPT" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Miscellaneous:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "MSCELNS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearchWithStage:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "0,1,2,3,4,5,6,7,8,9,10" + seperator + "TNSPT" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Drivers:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Vehicles:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LogSheetHEader:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void DisplayMessage(String message, Int16 options)
        {
            if (options == 1)
            {
                DivAlert.Attributes.Add("class", "alert alert-success");
            }
            else if (options == 2)
            {
                DivAlert.Attributes.Add("class", "alert alert-danger");
            }
            else if (options == 3)
            {
                DivAlert.Attributes.Add("class", "alert alert-info");
            }
            DivAlert.Visible = true;
            lblAsk.Text = message;
        }

        private void ClearAll()
        {
            txtCompany.Text = "";
            txtPC.Text = "";
            txtLogSheetNo.Text = "";
            txtDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtEnquiryID.Text = "";
            txtCustomerCode.Text = "";
            txtStartDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtEndDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtDriverCode.Text = "";
            txtFleet.Text = "";
            txtRemark.Text = "";
            txtInvoiceMileage.Text = "";
            txtDriverMileage.Text = "";
            txtMeterIn.Text = "";
            txtMeterOut.Text = "";

            txtChargeCode.Text = "";
            txtDescription.Text = "";
            txtQty.Text = "";
            txtRateType.Text = "";
            txtUnitRate.Text = "";
            txtUnitAmount.Text = "";
            txtTAX.Text = "";
            txtDisRate.Text = "";
            txtDisAmount.Text = "";
            txtTotal.Text = "";

            chkIsCustomer.Checked = false;
            chkIsDriver.Checked = false;
            Session["oMainItems"] = null;

            btnCreate.Enabled = true;
            dgvItems.DataSource = null;
            dgvItems.DataBind();
            txtGuest.Text = "";

            if (DateTime.Now.Hour > 11)
            {
                tmStartTime.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.PM);
                tmEndTime.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.PM);
            }
            else
            {
                tmStartTime.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
                tmEndTime.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
            }
        }

        private void clearEnryLine()
        {
            txtChargeCode.Text = "";
            txtDescription.Text = "";
            txtQty.Text = "";
            txtRateType.Text = "";
            txtUnitRate.Text = "";
            txtUnitAmount.Text = "";
            txtTAX.Text = "";
            txtDisRate.Text = "";
            txtDisAmount.Text = "";
            txtTotal.Text = "";

            chkIsCustomer.Checked = false;
            chkIsDriver.Checked = false;
        }

        private void CalculateLineTotal(string txtbox)
        {
            decimal TAX = 0;
            decimal DisRate = 0;
            decimal DisAmount = 0;

            try
            {
                if (!string.IsNullOrEmpty(txtUnitAmount.Text))
                {
                    decimal UnitAmount = Convert.ToDecimal(txtUnitAmount.Text);

                    TAX = (txtTAX.Text != "") ? Convert.ToDecimal(txtTAX.Text) : 0;
                    DisRate = (txtDisRate.Text != "") ? Convert.ToDecimal(txtDisRate.Text) : 0;
                    DisAmount = (txtDisAmount.Text != "") ? Convert.ToDecimal(txtDisAmount.Text) : 0;

                    if (txtbox == "DISRATE")
                    {
                        if (DisRate == 100)
                        {
                            DisplayMessage("Discount Cannot be 100%", 3);
                            txtDisAmount.Text = "";
                            txtDisRate.Text = "";
                            txtTotal.Text = "";
                            return;
                        }
                        txtDisAmount.Text = ((UnitAmount * DisRate) / 100).ToString();
                        txtTotal.Text = (UnitAmount - ((UnitAmount * DisRate) / 100)).ToString();
                    }
                    else if (txtbox == "DISAMOUNT")
                    {
                        txtDisRate.Text = (((UnitAmount - (UnitAmount - DisAmount)) * 100) / UnitAmount).ToString();
                        txtTotal.Text = (UnitAmount - DisAmount).ToString();
                    }
                    else if (txtbox == "QTY")
                    {
                        txtUnitAmount.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitRate.Text)).ToString();
                    }
                    else if (txtbox == "ALL")
                    {
                        if (DisRate == 0 && DisAmount == 0)
                        {
                            txtDisRate.Text = "0";
                            txtDisAmount.Text = "0";
                            txtUnitAmount.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitRate.Text)).ToString();
                            txtTotal.Text = txtUnitAmount.Text;
                        }
                        else if (DisRate > DisAmount)
                        {
                            txtDisAmount.Text = ((UnitAmount * DisRate) / 100).ToString();
                            txtTotal.Text = (UnitAmount - ((UnitAmount * DisRate) / 100)).ToString();
                        }
                        else
                        {
                            txtDisRate.Text = (((UnitAmount - (UnitAmount - DisAmount)) * 100) / UnitAmount).ToString();
                            txtTotal.Text = (UnitAmount - DisAmount).ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private bool ValidateItemAdd()
        {
            bool status = true;
            if (String.IsNullOrEmpty(txtChargeCode.Text))
            {
                DisplayMessage("Please select a charge code", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                DisplayMessage("Please enter a description", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                DisplayMessage("Please enter quantity", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtRateType.Text))
            {
                DisplayMessage("Please enter a rate type", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtUnitRate.Text))
            {
                DisplayMessage("Please enter a unit rate", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtUnitAmount.Text))
            {
                DisplayMessage("Please enter a unit amount", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtTotal.Text))
            {
                DisplayMessage("Please enter total value", 1);
                status = false;
                return status;
            }
            if (chkIsCustomer.Checked == false && chkIsDriver.Checked == false)
            {
                DisplayMessage("Please select is customer or is driver", 1);
                status = false;
                return status;
            }
            return status;
        }

        private void bindTOGrid()
        {
            if (Session["oMainItems"] != null)
            {
                oMainItems = (List<TR_LOGSHEET_DET>)Session["oMainItems"];
                Session["oMainItems"] = oMainItems;
                dgvItems.DataSource = oMainItems;
                dgvItems.DataBind();
                modifyGRD();
            }
        }

        private void modifyGRD()
        {
            for (int i = 0; i < dgvItems.Rows.Count; i++)
            {
                GridViewRow row = dgvItems.Rows[i];
                Label lblTLD_QTY = (Label)row.FindControl("lblTLD_QTY");
                Label lblTLD_U_RT = (Label)row.FindControl("lblTLD_U_RT");
                Label lblTLD_U_AMT = (Label)row.FindControl("lblTLD_U_AMT");
                Label lblTLD_TAX = (Label)row.FindControl("lblTLD_TAX");
                Label lblTLD_DIS_RT = (Label)row.FindControl("lblTLD_DIS_RT");
                Label lblTLD_DIS_AMT = (Label)row.FindControl("lblTLD_DIS_AMT");
                Label lblTLD_TOT = (Label)row.FindControl("lblTLD_TOT");

                Label lblTLD_IS_CUS = (Label)row.FindControl("lblTLD_IS_CUS");
                Label lblTLD_IS_DRI = (Label)row.FindControl("lblTLD_IS_DRI");

                Image imgCusFalse = (Image)row.FindControl("imgCusFalse");
                Image imgCusTrue = (Image)row.FindControl("imgCusTrue");

                Image imgDriverFalse = (Image)row.FindControl("imgDriverFalse");
                Image imgDriverTrue = (Image)row.FindControl("imgDriverTrue");

                lblTLD_QTY.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_QTY.Text));
                lblTLD_U_RT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_U_RT.Text));
                lblTLD_U_AMT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_U_AMT.Text));

                lblTLD_TAX.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_TAX.Text));
                lblTLD_DIS_RT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_DIS_RT.Text));
                lblTLD_DIS_AMT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_DIS_AMT.Text));
                lblTLD_TOT.Text = String.Format("{0:#,##0.00;($#,##0.00);0.00}", Convert.ToDecimal(lblTLD_TOT.Text));

                if (lblTLD_IS_CUS.Text == "1")
                {
                    imgCusTrue.Visible = true;
                }
                else
                {
                    imgCusFalse.Visible = true;
                }

                if (lblTLD_IS_DRI.Text == "1")
                {
                    imgDriverTrue.Visible = true;
                }
                else
                {
                    imgDriverFalse.Visible = true;
                }
            }
        }

        private bool validateHeader()
        {
            bool status = true;

            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                DisplayMessage("Please select a company", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtPC.Text))
            {
                DisplayMessage("Please select a profit center", 1);
                status = false;
                return status;
            }

            if (string.IsNullOrEmpty(txtDate.Text))
            {
                DisplayMessage("Please select a date", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtCustomerCode.Text))
            {
                DisplayMessage("Please select a customer", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtStartDate.Text))
            {
                DisplayMessage("Please select a start date", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtEndDate.Text))
            {
                DisplayMessage("Please select a finish date", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtDriverCode.Text))
            {
                DisplayMessage("Please select a driver", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFleet.Text))
            {
                DisplayMessage("Please select a vehicle", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                DisplayMessage("Please enter a remark", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtInvoiceMileage.Text))
            {
                DisplayMessage("Please enter invoiced mileage", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtDriverMileage.Text))
            {
                DisplayMessage("Please enter driver mileage", 1);
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtMeterIn.Text))
            {
                DisplayMessage("Please enter meter in value", 1);
                status = false;
                return status;
            } if (string.IsNullOrEmpty(txtMeterOut.Text))
            {
                DisplayMessage("Please enter meter out value", 1);
                status = false;
                return status;
            }

            if (Convert.ToDecimal(txtMeterIn.Text) > Convert.ToDecimal(txtMeterOut.Text))
            {
                DisplayMessage("Meter in value cannot be less than meter out valve", 1);
                status = false;
                return status;
            }
            if (Convert.ToDecimal(txtInvoiceMileage.Text) == 0)
            {
                DisplayMessage("Invoice mile age cannot be '0'", 1);
                status = false;
                return status;
            }
            if (Convert.ToDecimal(txtDriverMileage.Text) == 0)
            {
                DisplayMessage("Driver mile age cannot be '0'", 1);
                status = false;
                return status;
            }

            DateTime date = DateTime.Now;
            TimeSpan time = new TimeSpan(0, tmStartTime.Hour, tmStartTime.Minute, 0);

            date = DateTime.Now;
            time = new TimeSpan(0, tmEndTime.Hour, tmEndTime.Minute, 0);

            if (Convert.ToDateTime(txtStartDate.Text).Add(time) > Convert.ToDateTime(txtEndDate.Text).Add(time))
            {
                DisplayMessage("Please select a valid date ranges", 1);
                status = false;
                return status;
            }
            return status;
        }

        private void loadDetails(Int32 Seq)
        {
            List<TR_LOGSHEET_DET> oItems = CHNLSVC.Tours.GetLogSheetDetails(Seq);
            oMainItems = oItems;
            Session["oMainItems"] = oMainItems;
            bindTOGrid();
            clearEnryLine();
        }

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, Session["UserCompanyCode"].ToString());
        }

        #endregion

        #region Search btns

        protected void btnCompany_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ToursFacCompany);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_FAC_COM(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCompany.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnPC_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnLogSheetNo_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LogSheetHEader);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchLogHeader(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtLogSheetNo.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btntxtEnquiryID_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearchWithStage);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchEnquiryWithStage(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtEnquiryID.ClientID;
            ucc.UCModalPopupExtender.Show();
            txtEnquiryID.Focus();
        }

        protected void btnCustomerCode_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.GetCustomerCommon(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtCustomerCode.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnDriverCode_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Drivers);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchDrivers(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtDriverCode.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnGuest_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnFleet_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Vehicles);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchVehicle(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtFleet.ClientID;
            ucc.UCModalPopupExtender.Show();
        }

        protected void btnChargeCode_Click(object sender, ImageClickEventArgs e)
        {
            BasePage basepage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Miscellaneous);
            DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_Miscellaneous(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = txtChargeCode.ClientID;
            ucc.UCModalPopupExtender.Show();
        }


        #endregion

        #region Text Changed
        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            btnCompanyLoad_Click(null, null);
        }

        protected void txtPC_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtLogSheetNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogSheetNo.Text))
            {
                btnLogSheetNoLoad_Click(null, null);
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtEnquiryID_TextChanged(object sender, EventArgs e)
        {
            btntxtEnquiryIDLoad_Click(null, null);
        }

        protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerCode.Text))
            {
                MasterBusinessEntity custProf = GetbyCustCD(txtCustomerCode.Text.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    DisplayMessage("Customer is inactivated.Please contact accounts dept.", 2);
                }
                else
                {
                    DisplayMessage("Please enter a valid customer code.", 2);
                    txtCustomerCode.Text = txtCustomerCode.Text;
                }
            }
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtEndDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDriverCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtGuest_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtFleet_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtChargeCode_TextChanged(object sender, EventArgs e)
        {
            SR_SER_MISS oSR_SER_MISS = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(Session["UserCompanyCode"].ToString(), "MSCELNS", txtChargeCode.Text.Trim());
            if (oSR_SER_MISS != null && oSR_SER_MISS.SSM_CD != null)
            {
                txtDescription.Text = oSR_SER_MISS.SSM_DESC;
                txtRateType.Text = oSR_SER_MISS.SSM_RT_TP;
                txtUnitRate.Text = oSR_SER_MISS.SSM_RT.ToString();
                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    txtUnitAmount.Text = (oSR_SER_MISS.SSM_RT * Convert.ToDecimal(txtQty.Text)).ToString();
                }
                txtQty.Focus();
            }
            else
            {
                DisplayMessage("Please enter valid charge code", 3);
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                txtUnitAmount.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitRate.Text)).ToString();
                CalculateLineTotal("ALL");
            }
        }

        protected void txtTAX_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTAX.Text) && !string.IsNullOrEmpty(txtUnitAmount.Text))
            {
            }
        }

        protected void txtDisRate_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtDisRate.Text) > 100)
            {
                DisplayMessage("Please select a valid percentage", 3);
                return;
            }
            CalculateLineTotal("DISRATE");
        }

        protected void txtDisAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateLineTotal("DISAMOUNT");
        }

        protected void txtUnitRate_TextChanged(object sender, EventArgs e)
        {
            CalculateLineTotal("ALL");
        }

        protected void txtUnitAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateLineTotal("ALL");
        }

        #endregion

        #region Load Details
        protected void btnCompanyLoad_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCompany.Text.Trim()))
            {
                List<MST_FACBY> oMST_FACBY = CHNLSVC.Tours.GET_FACILITY_BY(txtCompany.Text.Trim(), "TNSPT");
                if (oMST_FACBY.Count > 0)
                {
                    txtCompany.Text = oMST_FACBY[0].MFB_FACCOM;
                    txtPC.Text = oMST_FACBY[0].MFB_FACPC;
                }
            }
        }

        protected void btntxtEnquiryIDLoad_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEnquiryID.Text))
            {
                DisplayMessage("Please select a enquiry ID", 3);
                return;
            }
            GEN_CUST_ENQ oItem = CHNLSVC.Tours.GET_CUST_ENQRY(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtEnquiryID.Text);
            if (oItem != null)
            {
                TR_LOGSHEET_HDR oHdr = CHNLSVC.Tours.GET_LOG_HDR_BY_ENQRY(txtEnquiryID.Text.Trim());
                if (oHdr.TLH_LOG_NO != null)
                {
                    DisplayMessage("This enquiry is already used for log sheet", 2);
                    return;
                }

                lblEnquirySeq.Text = oItem.GCE_SEQ.ToString();
                txtCustomerCode.Text = oItem.GCE_CUS_CD;
                txtCompany.Text = oItem.GCE_COM;
                txtPC.Text = oItem.GCE_PC;
                txtGuest.Text = oItem.GCE_GUESS;
                if (oItem.GCE_EXPECT_DT == DateTime.MinValue)
                {
                    txtStartDate.Text = DateTime.Now.ToLocalTime().ToString("dd/MMM/yyyy");
                }
                else
                {
                    txtStartDate.Text = oItem.GCE_EXPECT_DT.ToLocalTime().ToString("dd/MMM/yyyy");

                }
                if (oItem.GCE_RET_DT == DateTime.MinValue)
                {

                    txtEndDate.Text = DateTime.Now.ToLocalTime().ToString("dd/MMM/yyyy");
                }
                else
                {
                    txtEndDate.Text = oItem.GCE_RET_DT.ToLocalTime().ToString("dd/MMM/yyyy");
                }
            }
        }

        protected void btnLoad_Click(object sender, ImageClickEventArgs e)
        {
            txtChargeCode_TextChanged(null, null);
        }

        protected void btnLogSheetNoLoad_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogSheetNo.Text))
            {
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    DisplayMessage("please select a company code", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtPC.Text))
                {
                    DisplayMessage("please select a profit center", 2);
                    return;
                }

                TR_LOGSHEET_HDR oheader = CHNLSVC.Tours.GetLogSheetHeader(txtCompany.Text.Trim(), txtPC.Text.Trim(), txtLogSheetNo.Text.Trim());
                if (oheader != null && oheader.TLH_LOG_NO != null)
                {
                    btnCreate.Enabled = false;

                    txtCompany.Text = oheader.TLH_COM;
                    txtPC.Text = oheader.TLH_PC;
                    txtLogSheetNo.Text = oheader.TLH_LOG_NO;
                    txtDate.Text = oheader.TLH_DT.ToString("dd/MMM/yyyy");
                    txtEnquiryID.Text = oheader.TLH_REQ_NO;
                    txtStartDate.Text = oheader.TLH_ST_DT.ToString("dd/MMM/yyyy");
                    txtEndDate.Text = oheader.TLH_ED_DT.ToString("dd/MMM/yyyy");


                    MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                    if (oheader.TLH_ST_DT.ToString("tt") == "AM")
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    }
                    else
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    }
                    tmStartTime.SetTime(oheader.TLH_ST_DT.Hour, oheader.TLH_ST_DT.Minute, am_pm);

                    if (oheader.TLH_ED_DT.ToString("tt") == "AM")
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    }
                    else
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    }
                    tmEndTime.SetTime(oheader.TLH_ED_DT.Hour, oheader.TLH_ED_DT.Minute, am_pm);

                    //  return;
                    //if (oheader.TLH_ST_DT.TimeOfDay > 12)
                    //{
                    //    tmStartTime.SetTime(oheader.TLH_ST_DT.Hour, oheader.TLH_ST_DT.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.PM);
                    //}
                    //else
                    //{
                    //    tmStartTime.SetTime(oheader.TLH_ST_DT.Hour, oheader.TLH_ST_DT.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
                    //}
                    //if (oheader.TLH_ED_DT.Hour > 12)
                    //{
                    //    tmEndTime.SetTime(oheader.TLH_ED_DT.Hour, oheader.TLH_ST_DT.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.PM);
                    //}
                    //else
                    //{
                    //    tmEndTime.SetTime(oheader.TLH_ED_DT.Hour, oheader.TLH_ST_DT.Minute, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
                    //}

                    txtCustomerCode.Text = oheader.TLH_CUS_CD;
                    txtDriverCode.Text = oheader.TLH_DRI_CD;
                    txtGuest.Text = oheader.TLH_GUST;
                    txtFleet.Text = oheader.TLH_FLEET;
                    txtRemark.Text = oheader.TLH_RMK;
                    txtInvoiceMileage.Text = oheader.TLH_INV_MIL.ToString();
                    txtDriverMileage.Text = oheader.TLH_DRI_MIL.ToString();
                    txtMeterIn.Text = oheader.TLH_MET_IN.ToString();
                    txtMeterOut.Text = oheader.TLH_MET_OUT.ToString();


                    loadDetails(oheader.TLH_SEQ);
                }

            }
        }

        #endregion

        protected void lbtColse_Click(object sender, EventArgs e)
        {
            DivAlert.Visible = false;
        }

        protected void btnAddTogrid_Click(object sender, EventArgs e)
        {
            if (Session["oMainItems"] != null)
            {
                oMainItems = (List<TR_LOGSHEET_DET>)Session["oMainItems"];

            }
            else
            {
                oMainItems = new List<TR_LOGSHEET_DET>();
            }

            if (ValidateItemAdd())
            {

                if (oMainItems.FindAll(x => x.TLD_CHR_CD == txtChargeCode.Text).Count > 0)
                {
                    DisplayMessage("This charge code is already added", 1);
                    return;
                }

                TR_LOGSHEET_DET oItems = new TR_LOGSHEET_DET();
                oItems.TLD_SEQ = 0;
                oItems.TLD_LINE = oMainItems.Count + 1;
                oItems.TLD_CHR_CD = txtChargeCode.Text;
                oItems.TLD_CHR_DESC = txtDescription.Text;
                oItems.TLD_QTY = Convert.ToDecimal(txtQty.Text);
                oItems.TLD_RT_TP = txtRateType.Text;
                oItems.TLD_U_RT = Convert.ToDecimal(txtUnitRate.Text);
                oItems.TLD_U_AMT = Convert.ToDecimal(txtUnitAmount.Text);
                oItems.TLD_TAX = (txtTAX.Text != "") ? Convert.ToDecimal(txtTAX.Text) : 0;
                oItems.TLD_DIS_RT = (txtDisRate.Text != "") ? Convert.ToDecimal(txtDisRate.Text) : 0;
                oItems.TLD_DIS_AMT = (txtDisAmount.Text != "") ? Convert.ToDecimal(txtDisAmount.Text) : 0;
                oItems.TLD_TOT = Convert.ToDecimal(txtTotal.Text);
                oItems.TLD_IS_CUS = (chkIsCustomer.Checked == true) ? 1 : 0;
                oItems.TLD_IS_DRI = (chkIsDriver.Checked == true) ? 1 : 0;
                oItems.TLD_IS_ADD = 1;
                oMainItems.Add(oItems);
                Session["oMainItems"] = oMainItems;
                bindTOGrid();
                clearEnryLine();
            }
        }

        protected void dgvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "delete")
                {
                    GridViewRow row = dgvItems.Rows[Convert.ToInt32(e.CommandArgument)];
                    oMainItems = (List<TR_LOGSHEET_DET>)Session["oMainItems"];
                    oMainItems.RemoveAt(Convert.ToInt32(e.CommandArgument));
                    Session["oMainItems"] = oMainItems;
                    bindTOGrid();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void dgvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void dgvItems_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}