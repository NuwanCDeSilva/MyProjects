using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours.UserControls;
using FF.BusinessObjects;

namespace FF.AbansTours
{
    public partial class ChargeCodeDetails : BasePage
    {
        private BasePage _basePage;

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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlChargeType.SelectedValue.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.TransferCodes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlChargeType.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Miscellaneous:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlChargeType.SelectedValue.ToString() + seperator);
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
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserSubChannl"]))
                    )
                {
                    if (!IsPostBack)
                    {
                        loadCostCate();
                        ddlChargeType_SelectedIndexChanged(null, null);
                        grdresult.DataSource = new int[] { };
                        grdresult.DataBind();
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

        private void loadCostCate()
        {
            List<MST_COST_CATE> oCate = CHNLSVC.Tours.GET_COST_CATE(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            ddlChargeType.DataSource = oCate;
            ddlChargeType.DataTextField = "MCC_DESC";
            ddlChargeType.DataValueField = "MCC_CD";
            ddlChargeType.DataBind();
        }

        protected void ddlChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(DefaultView);
            if (ddlChargeType.SelectedValue.ToString() == "AIRTVL")
            {
                loadClasses();
                LoadCurrancyCodes();
                loadIschild();
                LoadProviders();
                MultiView1.SetActiveView(AriTravel);
                clearARI();
                grdresult.Visible = true;
                grdlvlresult.Visible = false;
                grdMisreult.Visible = false;
            }
            else if (ddlChargeType.SelectedValue.ToString() == "TRANS")
            {
                loadClassesTravel();
                LoadProvidersTravel();
                LoadCurrancyCodesTravl();
                MultiView1.SetActiveView(Travel);
                clearTravl();
                grdresult.Visible = false;
                grdlvlresult.Visible = true;
                grdMisreult.Visible = false;
            }
            else if (ddlChargeType.SelectedValue.ToString() == "MSCELNS")
            {
                loadServiceProvidersMiss();
                LoadCurrancyCodesTravlMiss();
                MultiView1.SetActiveView(Miscellaneous);
                clearMiscellaneous();
                grdresult.Visible = false;
                grdlvlresult.Visible = false;
                grdMisreult.Visible = true;
            }
            grdresult.DataSource = new int[] { };
            grdresult.DataBind();
            grdlvlresult.DataSource = new int[] { };
            grdlvlresult.DataBind();
            grdMisreult.DataSource = new int[] { };
            grdMisreult.DataBind();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (ddlChargeType.SelectedValue.ToString() == "AIRTVL")
            {
                clearARI();
            }
            else if (ddlChargeType.SelectedValue.ToString() == "TRANS")
            {
                clearTravl();
            }
            else if (ddlChargeType.SelectedValue.ToString() == "MSCELNS")
            {
                clearMiscellaneous();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (ddlChargeType.SelectedValue.ToString() == "AIRTVL")
            {
                if (validateAir())
                {
                    SR_AIR_CHARGE oItem = new SR_AIR_CHARGE();
                    oItem.SAC_SEQ = (hfSAC_SEQ.Value == "") ? 0 : Convert.ToInt32(hfSAC_SEQ.Value.ToString());
                    oItem.SAC_COM = Session["UserCompanyCode"].ToString();
                    oItem.SAC_SCV_BY = ddlServiceProvider.SelectedValue.ToString();
                    oItem.SAC_CATE = ddlChargeType.SelectedValue.ToString();
                    oItem.SAC_CD = txtCode.Text.Trim();
                    oItem.SAC_CLS = ddlClass.SelectedValue.ToString();
                    oItem.SAC_FRM_DT = Convert.ToDateTime(txtFromDate.Text).Date;
                    oItem.SAC_TO_DT = Convert.ToDateTime(txtToDate.Text).Date;
                    oItem.SAC_TIC_DESC = txtTicketDetails.Text;
                    oItem.SAC_ADD_DESC = txtAdditional.Text;
                    oItem.SAC_FROM = txtFrom.Text;
                    oItem.SAC_TO = txtTo.Text;
                    oItem.SAC_RT = Convert.ToDecimal(txtRate.Text);
                    oItem.SAC_CUR = ddlCurrency.SelectedValue.ToString();
                    oItem.SAC_IS_CHILD = Convert.ToInt32(ddlIsChild.SelectedValue.ToString());
                    oItem.SAC_TP = txtType.Text;
                    oItem.SAC_CRE_BY = Session["UserID"].ToString();
                    oItem.SAC_CRE_DT = DateTime.Now;
                    oItem.SAC_MOD_BY = Session["UserID"].ToString();
                    oItem.SAC_MOD_DT = DateTime.Now;
                    oItem.SAC_ACT = 1;

                    string err;
                    int result = CHNLSVC.Tours.SaveAirChageCodes(oItem, out err);
                    if (result > 0)
                    {
                        DisplayMessages("Successfully saved.");
                        clearARI();
                    }
                    else
                    {
                        DisplayMessages(err);
                    }
                }
            }
            else if (ddlChargeType.SelectedValue.ToString() == "TRANS")
            {
                if (validateTraval())
                {
                    SR_TRANS_CHA oItem = new SR_TRANS_CHA();
                    oItem.STC_SEQ = (hfTravlSeq.Value == "") ? 0 : Convert.ToInt32(hfTravlSeq.Value.ToString());
                    oItem.STC_COM = Session["UserCompanyCode"].ToString();
                    oItem.STC_SER_BY = ddlServiceByTvl.SelectedValue.ToString();
                    oItem.STC_CATE = ddlChargeType.SelectedValue.ToString();
                    oItem.STC_CD = txtCodeTvl.Text.Trim();
                    oItem.STC_FRM_DT = Convert.ToDateTime(txtFromDateTvl.Text);
                    oItem.STC_TO_DT = Convert.ToDateTime(txtTodateTvl.Text);
                    oItem.STC_FRM = txtFromTvl.Text;
                    oItem.STC_TO = txtToTvl.Text;
                    oItem.STC_CLS = ddlClassTvl.SelectedValue.ToString();
                    oItem.STC_VEH_TP = txtVehicalTVl.Text;
                    oItem.STC_FRM_KM = Convert.ToInt32(txtFromKmTvl.Text);
                    oItem.STC_TO_KM = Convert.ToInt32(txtToKmTvl.Text);
                    oItem.STC_RT = Convert.ToDecimal(txtRateTvl.Text);
                    oItem.STC_AD_RT = Convert.ToDecimal(txtAdditionalRateTvl.Text);
                    oItem.STC_RT_TP = txtRateTypeTvl.Text;
                    oItem.STC_CRE_BY = Session["UserID"].ToString();
                    oItem.STC_CRE_DT = DateTime.Now;
                    oItem.STC_MOD_BY = Session["UserID"].ToString();
                    oItem.STC_MOD_DT = DateTime.Now;
                    oItem.STC_CURR = ddlCurrancyTvl.SelectedValue.ToString();
                    oItem.STC_DESC = txtDescriptiobTvl.Text;
                    oItem.STC_ACT = 1;

                    string err;
                    int result = CHNLSVC.Tours.SaveTrasportChageCodes(oItem, out err);
                    if (result > 0)
                    {
                        DisplayMessages("Successfully saved.");
                        clearTravl();
                    }
                    else
                    {
                        DisplayMessages(err);
                    }
                }
            }
            else if (ddlChargeType.SelectedValue.ToString() == "MSCELNS")
            {
                if (validateMiscellaneous())
                {
                    SR_SER_MISS oItem = new SR_SER_MISS();

                    oItem.SSM_SEQ = (hfMiscellaneous.Value == "") ? 0 : Convert.ToInt32(hfMiscellaneous.Value.ToString());
                    oItem.SSM_COM = Session["UserCompanyCode"].ToString();
                    oItem.SSM_SER_PRO = ddlServiceProviderMis.SelectedValue.ToString();
                    oItem.SSM_CATE = ddlChargeType.SelectedValue.ToString();
                    oItem.SSM_CD = txtCodeMis.Text.Trim();
                    oItem.SSM_FRM_DT = Convert.ToDateTime(txtFromDateMis.Text);
                    oItem.SSM_TO_DT = Convert.ToDateTime(txtToDateMis.Text);
                    oItem.SSM_RT = Convert.ToDecimal(txtRateMis.Text);
                    oItem.SSM_CUR = ddlCurrancyMis.SelectedValue.ToString();
                    oItem.SSM_RT_TP = txtRateTypeMis.Text;
                    oItem.SSM_CRE_BY = Session["UserID"].ToString();
                    oItem.SSM_CRE_DT = DateTime.Now;
                    oItem.SSM_MOD_BY = Session["UserID"].ToString();
                    oItem.SSM_MOD_DT = DateTime.Now;
                    oItem.SSM_DESC = txtDescriptionMis.Text;
                    oItem.SSM_ACT = 1;

                    string err;
                    int result = CHNLSVC.Tours.SaveMiscellaneousChageCodes(oItem, out err);
                    if (result > 0)
                    {
                        DisplayMessages("Successfully saved.");
                        clearMiscellaneous();
                    }
                    else
                    {
                        DisplayMessages(err);
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        #region Air Tavel

        private void loadClasses()
        {
            List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceClasses(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString());
            ComboBoxObject oEmpty = new ComboBoxObject(); oEmpty.Text = string.Empty; oEmpty.Value = string.Empty; oItemsa.Insert(0, oEmpty);
            ddlClass.DataSource = oItemsa;
            ddlClass.DataTextField = "Text";
            ddlClass.DataValueField = "Value";
            ddlClass.DataBind();
        }

        private void LoadCurrancyCodes()
        {
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            if (_cur != null)
            {
                ddlCurrency.DataSource = _cur;
                ddlCurrency.DataTextField = "Mcr_cd";
                ddlCurrency.DataValueField = "Mcr_cd";
                ddlCurrency.DataBind();
                ddlCurrency.SelectedValue = "USD";
            }
        }

        private void loadIschild()
        {
            List<ComboBoxObject> oItems = new List<ComboBoxObject>();

            ComboBoxObject oNo1 = new ComboBoxObject();
            oNo1.Text = " ";
            oNo1.Value = "-1";
            oItems.Add(oNo1);

            ComboBoxObject oNo = new ComboBoxObject();
            oNo.Text = "No";
            oNo.Value = "1";
            oItems.Add(oNo);

            ComboBoxObject oYes = new ComboBoxObject();
            oYes.Text = "Yes";
            oYes.Value = "0";
            oItems.Add(oYes);

            ddlIsChild.DataSource = oItems;
            ddlIsChild.DataTextField = "Text";
            ddlIsChild.DataValueField = "Value";
            ddlIsChild.DataBind();
        }

        private void LoadProviders()
        {
            List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceProviders(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString());
            ComboBoxObject oEmpty = new ComboBoxObject(); oEmpty.Text = string.Empty; oEmpty.Value = string.Empty; oItemsa.Insert(0, oEmpty);
            ddlServiceProvider.DataSource = oItemsa;
            ddlServiceProvider.DataTextField = "Text";
            ddlServiceProvider.DataValueField = "Value";
            ddlServiceProvider.DataBind();
        }

        private bool validateAir()
        {
            bool status = true;

            if (string.IsNullOrEmpty(txtCode.Text))
            {
                DisplayMessages("Please enter a code");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(ddlClass.SelectedValue.ToString()))
            {
                DisplayMessages("Please select a class");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(ddlServiceProvider.SelectedValue.ToString()))
            {
                DisplayMessages("Please select a service provider");
                status = false;
                return status;
            }
            if (Convert.ToDateTime(txtFromDate.Text).Date < DateTime.Today.Date)
            {
                DisplayMessages("Please select a valid from date");
                status = false;
                return status;
            }

            if (Convert.ToDateTime(txtToDate.Text).Date < DateTime.Today.Date)
            {
                DisplayMessages("Please select a valid to date");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtTicketDetails.Text))
            {
                DisplayMessages("Please enter a ticket details");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFrom.Text))
            {
                DisplayMessages("Please enter a from");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtTo.Text))
            {
                DisplayMessages("Please enter a to");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(ddlCurrency.SelectedValue.ToString()))
            {
                DisplayMessages("Please select a currency ");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtRate.Text))
            {
                DisplayMessages("Please enter a rate");
                status = false;
                return status;
            }
            if (!isdecimal(txtRate.Text))
            {
                DisplayMessages("Please enter a valid rate");
                status = false;
                return status;
            }
            if (Convert.ToDecimal(txtRate.Text) < 0)
            {
                DisplayMessages("Please enter a valid rate");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtType.Text))
            {
                DisplayMessages("Please enter a type");
                status = false;
                return status;
            }
            if (ddlIsChild.SelectedIndex == 0 )
            {
                DisplayMessages("Please select is child");
                status = false;
                return status;
            }
            return status;
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                SR_AIR_CHARGE oAirChargeCode = CHNLSVC.Tours.GetChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString(), txtCode.Text.Trim());
                if (oAirChargeCode != null && oAirChargeCode.SAC_CD != null)
                {
                    hfSAC_SEQ.Value = oAirChargeCode.SAC_SEQ.ToString();
                    ddlServiceProvider.SelectedValue = oAirChargeCode.SAC_SCV_BY;
                    ddlChargeType.SelectedValue = oAirChargeCode.SAC_CATE;
                    txtCode.Text = oAirChargeCode.SAC_CD;
                    ddlClass.SelectedValue = oAirChargeCode.SAC_CLS;
                    txtDateExtender.SelectedDate = oAirChargeCode.SAC_FRM_DT;
                    dtTo.SelectedDate = oAirChargeCode.SAC_TO_DT;
                    txtTicketDetails.Text = oAirChargeCode.SAC_TIC_DESC;
                    txtAdditional.Text = oAirChargeCode.SAC_ADD_DESC;
                    txtFrom.Text = oAirChargeCode.SAC_FROM;
                    txtTo.Text = oAirChargeCode.SAC_TO;
                    txtRate.Text = oAirChargeCode.SAC_RT.ToString("N2");
                    ddlCurrency.SelectedValue = oAirChargeCode.SAC_CUR;
                    ddlIsChild.SelectedValue = oAirChargeCode.SAC_IS_CHILD.ToString();
                    txtType.Text = oAirChargeCode.SAC_TP;
                    txtCode.Enabled = false;
                    List<SR_AIR_CHARGE> _list_air_charge = new List<SR_AIR_CHARGE>();
                    _list_air_charge = CHNLSVC.Tours.GetALLChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString(), txtCode.Text.Trim(),DateTime.Now.Date); 
                    grdresult.DataSource = _list_air_charge;
                    grdresult.DataBind();
                }
            }
            else
            {
                DisplayMessages("please select a code");
            }
        }

        protected void btnCode_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlChargeType.SelectedValue.ToString() == "AIRTVL")
            {
                //BasePage basepage = new BasePage();
                //Page pp = (Page)this.Page;
                //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChargeCodeTours);
                //DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_ChargeCode(ucc.SearchParams, null, null);
                //ucc.BindUCtrlDDLData(dataSource);
                //ucc.BindUCtrlGridData(dataSource);
                //ucc.ReturnResultControl = txtCode.ClientID;
                //ucc.UCModalPopupExtender.Show();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChargeCodeTours);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_ChargeCode(SearchParams, null, null);
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "CodeAirTravel";
                UserPopoup.Show();
            }
        }

        private void clearARI()
        {
            hfSAC_SEQ.Value = "0";
            ddlServiceProvider.SelectedIndex = 0;
            txtCode.Text = "";
            ddlClass.SelectedIndex = 0;
            txtDateExtender.SelectedDate = DateTime.Now.Date;
            dtTo.SelectedDate = DateTime.Now.Date;
            txtTicketDetails.Text = "";
            txtAdditional.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
            txtRate.Text = "0.00";
            ddlCurrency.SelectedValue = "USD";
            ddlIsChild.SelectedIndex = 0;
            txtType.Text = "";
            txtCode.Enabled = true;
        }

        #endregion Air Tavel

        #region Travel

        private void clearTravl()
        {
            txtCodeTvl.Text = "";
            txtDescriptiobTvl.Text = "";
            ddlClassTvl.SelectedIndex = 0;
            ddlServiceByTvl.SelectedIndex = 0;
            dtFromDateTvl.SelectedDate = DateTime.Now.Date;
            dtTodateTvl.SelectedDate = DateTime.Now.Date;
            txtFromTvl.Text = "";
            txtToTvl.Text = "";
            txtVehicalTVl.Text = "";
            txtRateTypeTvl.Text = "";
            txtFromKmTvl.Text = "";
            txtToKmTvl.Text = "";
            txtRate.Text = "0.00";
            txtAdditionalRateTvl.Text = "0.00";
            ddlCurrancyTvl.SelectedValue = "USD";
            txtRateTvl.Text = "0.00";
            txtCodeTvl.Enabled = true;
        }

        private void loadClassesTravel()
        {
            List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceClasses(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString());
            ComboBoxObject oEmpty = new ComboBoxObject(); oEmpty.Text = string.Empty; oEmpty.Value = string.Empty; oItemsa.Insert(0, oEmpty);
            ddlClassTvl.DataSource = oItemsa;
            ddlClassTvl.DataTextField = "Text";
            ddlClassTvl.DataValueField = "Value";
            ddlClassTvl.DataBind();
        }

        private void LoadProvidersTravel()
        {
            List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceProviders(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString());
            ComboBoxObject oEmpty = new ComboBoxObject(); oEmpty.Text = string.Empty; oEmpty.Value = string.Empty; oItemsa.Insert(0, oEmpty);
            ddlServiceByTvl.DataSource = oItemsa;
            ddlServiceByTvl.DataTextField = "Text";
            ddlServiceByTvl.DataValueField = "Value";
            ddlServiceByTvl.DataBind();
        }

        private void LoadCurrancyCodesTravl()
        {
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            if (_cur != null)
            {
                ddlCurrancyTvl.DataSource = _cur;
                ddlCurrancyTvl.DataTextField = "Mcr_cd";
                ddlCurrancyTvl.DataValueField = "Mcr_cd";
                ddlCurrancyTvl.DataBind();
                ddlCurrancyTvl.SelectedValue = "USD";
            }
        }

        protected void btnCodeTvl_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlChargeType.SelectedValue.ToString() == "TRANS")
            {
                //BasePage basepage = new BasePage();
                //Page pp = (Page)this.Page;
                //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
                //DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_TransferCode(ucc.SearchParams, null, null);
                //ucc.BindUCtrlDDLData(dataSource);
                //ucc.BindUCtrlGridData(dataSource);
                //ucc.ReturnResultControl = txtCodeTvl.ClientID;
                //ucc.UCModalPopupExtender.Show();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_TransferCode(SearchParams, null, null);
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "CodeTravel";
                UserPopoup.Show();
            }
        }

        private bool validateTraval()
        {
            bool status = true;
            if (string.IsNullOrEmpty(txtCodeTvl.Text))
            {
                DisplayMessages("Please enter a code");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtDescriptiobTvl.Text))
            {
                DisplayMessages("Please enter a description");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(ddlClassTvl.SelectedValue.ToString()))
            {
                DisplayMessages("Please select a class");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(ddlServiceByTvl.SelectedValue.ToString()))
            {
                DisplayMessages("Please select a service provider");
                status = false;
                return status;
            }

            if (string.IsNullOrEmpty(txtFromDateTvl.Text))
            {
                DisplayMessages("Please enter a from date");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtTodateTvl.Text))
            {
                DisplayMessages("Please enter a to date");
                status = false;
                return status;
            }
            if (Convert.ToDateTime(txtFromDateTvl.Text).Date < DateTime.Now.Date)
            {
                DisplayMessages("Please enter a valid from date");
                status = false;
                return status;
            }

            if (Convert.ToDateTime(txtTodateTvl.Text).Date < DateTime.Now.Date)
            {
                DisplayMessages("Please enter a valid to date");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtVehicalTVl.Text))
            {
                DisplayMessages("Please enter a vehicle type");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtRateTypeTvl.Text))
            {
                DisplayMessages("Please enter a rate type");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFromKmTvl.Text))
            {
                DisplayMessages("Please enter a from Km");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtToKmTvl.Text))
            {
                DisplayMessages("Please enter a to Km");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtRateTvl.Text))
            {
                DisplayMessages("Please enter a rate");
                status = false;
                return status;
            }
            if (!isdecimal(txtRateTvl.Text))
            {
                DisplayMessages("Please enter a valid rate");
                status = false;
                return status;
            }

            if (Convert.ToDecimal(txtRateTvl.Text) < 0)
            {
                DisplayMessages("Please enter a valid rate");
                status = false;
                return status;
            }

            if (string.IsNullOrEmpty(txtAdditionalRateTvl.Text))
            {
                DisplayMessages("Please enter the additional rate");
                status = false;
                return status;
            }
            if (!isdecimal(txtAdditionalRateTvl.Text))
            {
                DisplayMessages("Please enter a valid additional rate");
                status = false;
                return status;
            }
            if (Convert.ToDecimal(txtAdditionalRateTvl.Text) < 0)
            {
                DisplayMessages("Please enter a valid additional rate");
                status = false;
                return status;
            }

            if (string.IsNullOrEmpty(ddlCurrancyTvl.SelectedValue.ToString()))
            {
                DisplayMessages("Please select a currency");
                status = false;
                return status;
            }
            return status;
        }

        protected void txtCodeTvl_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodeTvl.Text))
            {
                SR_TRANS_CHA oItem = CHNLSVC.Tours.GetTransferChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString(), txtCodeTvl.Text.Trim());
                if (oItem != null && oItem.STC_CD != null)
                {
                    hfTravlSeq.Value = oItem.STC_SEQ.ToString();
                    ddlServiceByTvl.SelectedValue = oItem.STC_SER_BY;
                    ddlChargeType.SelectedValue = oItem.STC_CATE;
                    txtCodeTvl.Text = oItem.STC_CD;
                    dtFromDateTvl.SelectedDate = oItem.STC_FRM_DT;
                    dtTodateTvl.SelectedDate = oItem.STC_TO_DT;
                    txtFromTvl.Text = oItem.STC_FRM;
                    txtToTvl.Text = oItem.STC_TO;
                    ddlClassTvl.SelectedValue = oItem.STC_CLS;
                    txtVehicalTVl.Text = oItem.STC_VEH_TP;
                    txtFromKmTvl.Text = oItem.STC_FRM_KM.ToString();
                    txtToKmTvl.Text = oItem.STC_TO_KM.ToString();
                    txtRateTvl.Text = oItem.STC_RT.ToString("N2");
                    txtAdditionalRateTvl.Text = oItem.STC_AD_RT.ToString("N2");
                    ddlCurrancyTvl.SelectedValue = oItem.STC_CURR;
                    txtDescriptiobTvl.Text = oItem.STC_DESC;
                    txtRateTypeTvl.Text = oItem.STC_RT_TP;
                    txtCodeTvl.Enabled = false;

                    List<SR_TRANS_CHA> _list_TVl_charge = new List<SR_TRANS_CHA>();
                    _list_TVl_charge = CHNLSVC.Tours.GetAllTransferChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString(), txtCodeTvl.Text.Trim(),DateTime.Now.Date);
                    grdlvlresult.DataSource = _list_TVl_charge;
                    grdlvlresult.DataBind();
                }
            }
            else
            {
                DisplayMessages("please select a code");
            }
        }

        protected void btnLoadCodeTvl_Click(object sender, ImageClickEventArgs e)
        {
            txtCodeTvl_TextChanged(null, null);
        }

        #endregion Travel

        #region Miscellaneous

        private void loadServiceProvidersMiss()
        {
            List<ComboBoxObject> oItemsa = CHNLSVC.Tours.GetServiceProviders(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString());
            ComboBoxObject oEmpty = new ComboBoxObject(); oEmpty.Text = string.Empty; oEmpty.Value = string.Empty; oItemsa.Insert(0, oEmpty);
            ddlServiceProviderMis.DataSource = oItemsa;
            ddlServiceProviderMis.DataTextField = "Text";
            ddlServiceProviderMis.DataValueField = "Value";
            ddlServiceProviderMis.DataBind();
        }

        private void LoadCurrancyCodesTravlMiss()
        {
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            if (_cur != null)
            {
                ddlCurrancyMis.DataSource = _cur;
                ddlCurrancyMis.DataTextField = "Mcr_cd";
                ddlCurrancyMis.DataValueField = "Mcr_cd";
                ddlCurrancyMis.DataBind();
                ddlCurrancyMis.SelectedValue = "USD";
            }
        }

        private void clearMiscellaneous()
        {
            txtCodeMis.Text = "";
            txtDescriptionMis.Text = "";
            ddlServiceProviderMis.SelectedIndex = 0;
            txtRateTypeMis.Text = "";
            dtFromDateMis.SelectedDate = DateTime.Now.Date;
            dtTodateMis.SelectedDate = DateTime.Now.Date;
            ddlCurrancyMis.SelectedValue = "USD";
            txtRateMis.Text = "0.00";
            txtCodeMis.Enabled = true;
        }

        private bool validateMiscellaneous()
        {
            bool status = true;
            if (String.IsNullOrEmpty(txtCodeMis.Text))
            {
                DisplayMessages("Please enter a code");
                status = false;
                return status;
            }

            if (String.IsNullOrEmpty(txtDescriptionMis.Text))
            {
                DisplayMessages("Please enter a description");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(ddlServiceProviderMis.SelectedValue.ToString()))
            {
                DisplayMessages("Please select a service provider");
                status = false;
                return status;
            }
            if (String.IsNullOrEmpty(txtRateTypeMis.Text))
            {
                DisplayMessages("Please enter a rate type");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtFromDateMis.Text))
            {
                DisplayMessages("Please enter a from date");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtToDateMis.Text))
            {
                DisplayMessages("Please enter a to date");
                status = false;
                return status;
            }
            if (Convert.ToDateTime(txtFromDateMis.Text).Date < DateTime.Now.Date)
            {
                DisplayMessages("Please enter a valid from date");
                status = false;
                return status;
            }

            if (Convert.ToDateTime(txtToDateMis.Text).Date < DateTime.Now.Date)
            {
                DisplayMessages("Please enter a valid to date");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(ddlCurrancyMis.SelectedValue.ToString()))
            {
                DisplayMessages("Please select a currency");
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtRateMis.Text))
            {
                DisplayMessages("Please enter a rate");
                status = false;
                return status;
            }
            if (!isdecimal(txtRateMis.Text))
            {
                DisplayMessages("Please enter a valid rate");
                status = false;
                return status;
            }

            if (Convert.ToDecimal(txtRateMis.Text) < 0)
            {
                DisplayMessages("Please enter a valid rate");
                status = false;
                return status;
            }


            return status;
        }

        protected void btnSearchCOdeMis_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlChargeType.SelectedValue.ToString() == "MSCELNS")
            {
                //BasePage basepage = new BasePage();
                //Page pp = (Page)this.Page;
                //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Miscellaneous);
                //DataTable dataSource = basepage.CHNLSVC.CommonSearch.SEARCH_Miscellaneous(ucc.SearchParams, null, null);
                //ucc.BindUCtrlDDLData(dataSource);
                //ucc.BindUCtrlGridData(dataSource);
                //ucc.ReturnResultControl = txtCodeMis.ClientID;
                //ucc.UCModalPopupExtender.Show();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Miscellaneous);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_Miscellaneous(SearchParams, null, null);
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "CodeMiscell";
                UserPopoup.Show();
            }
        }

        protected void txtCodeMis_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodeMis.Text))
            {
                SR_SER_MISS oItem = new SR_SER_MISS();
                oItem = CHNLSVC.Tours.GetMiscellaneousChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString(), txtCodeMis.Text);
                if (oItem != null && oItem.SSM_CD != null)
                {
                    hfMiscellaneous.Value = oItem.SSM_SEQ.ToString();
                    ddlServiceProviderMis.SelectedValue = oItem.SSM_SER_PRO;
                    ddlChargeType.SelectedValue = oItem.SSM_CATE;
                    txtCodeMis.Text = oItem.SSM_CD;
                    dtFromDateMis.SelectedDate = oItem.SSM_FRM_DT;
                    dtTodateMis.SelectedDate = oItem.SSM_TO_DT;
                    txtRateMis.Text = oItem.SSM_RT.ToString("N2");
                    ddlCurrancyMis.SelectedValue = oItem.SSM_CUR;
                    txtRateTypeMis.Text = oItem.SSM_RT_TP;
                    txtDescriptionMis.Text = oItem.SSM_DESC;
                    txtCodeMis.Enabled = false;

                    List<SR_SER_MISS> _list_MIS_charge = new List<SR_SER_MISS>();
                    _list_MIS_charge = CHNLSVC.Tours.GetALLMiscellaneousChargeDetailsByCode(Session["UserCompanyCode"].ToString(), ddlChargeType.SelectedValue.ToString(), txtCodeMis.Text, DateTime.Now.Date);
                    grdMisreult.DataSource = _list_MIS_charge;
                    grdMisreult.DataBind();
                }
            }
            else
            {
                DisplayMessages("please select a code");
            }
        }

        #endregion Miscellaneous
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        protected void Imgbtnfrom_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = "";
            DataTable result = CHNLSVC.CommonSearch.searchTownData(SearchParams, null, null);
            grdResultsearch.DataSource = result;
            grdResultsearch.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "TownFromAirTravel";
            UserPopoup.Show();
        }

        protected void ImgbtnTo_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = "";
            DataTable result = CHNLSVC.CommonSearch.searchTownData(SearchParams, null, null);
            grdResultsearch.DataSource = result;
            grdResultsearch.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "TownToAirTravel";
            UserPopoup.Show();
        }

   
        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResultsearch.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "TownFromAirTravel")
            {
                txtFrom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "TownToAirTravel")
            {
                txtFrom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "TownFromTvl")
            {
                txtFromTvl.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "TownToTvl")
            {
                txtToTvl.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "CodeAirTravel")
            {
                txtCode.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "CodeTravel")
            {
                txtCodeTvl.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "CodeMiscell")
            {
                txtCodeMis.Text = ID;
                lblvalue.Text = "";
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultsearch.PageIndex = e.NewPageIndex;
            if ((lblvalue.Text == "TownFromAirTravel") || (lblvalue.Text == "TownToAirTravel")
                 || (lblvalue.Text == "TownFromTvl") || (lblvalue.Text == "TownToTvl"))
            {              
                string SearchParams = "";
                DataTable result = CHNLSVC.CommonSearch.searchTownData(SearchParams, null, null);
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeAirTravel"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChargeCodeTours);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_ChargeCode(SearchParams, null, null);
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeTravel"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_TransferCode(SearchParams, null, null);
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeMiscell"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Miscellaneous);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_Miscellaneous(SearchParams, null, null);
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if ((lblvalue.Text == "TownFromAirTravel") || (lblvalue.Text == "TownToAirTravel")
                || (lblvalue.Text == "TownFromTvl") || (lblvalue.Text == "TownToTvl"))
            {
                string SearchParams = "";
                DataTable result = CHNLSVC.CommonSearch.searchTownData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeAirTravel"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChargeCodeTours);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_ChargeCode(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeTravel"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_TransferCode(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeMiscell"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Miscellaneous);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_Miscellaneous(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if ((lblvalue.Text == "TownFromAirTravel") || (lblvalue.Text == "TownToAirTravel")
                || (lblvalue.Text == "TownFromTvl") || (lblvalue.Text == "TownToTvl"))
            {
                string SearchParams = "";
                DataTable result = CHNLSVC.CommonSearch.searchTownData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeAirTravel"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ChargeCodeTours);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_ChargeCode(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeTravel"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TransferCodes);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_TransferCode(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "CodeMiscell"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Miscellaneous);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_Miscellaneous(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResultsearch.DataSource = result;
                grdResultsearch.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        #endregion


        protected void ImgbtnFromTvl_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = "";
            DataTable result = CHNLSVC.CommonSearch.searchTownData(SearchParams, null, null);
            grdResultsearch.DataSource = result;
            grdResultsearch.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "TownFromTvl";
            UserPopoup.Show();
        }

        protected void ImgbtnToTvl_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = "";
            DataTable result = CHNLSVC.CommonSearch.searchTownData(SearchParams, null, null);
            grdResultsearch.DataSource = result;
            grdResultsearch.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "TownToTvl";
            UserPopoup.Show();
        }
    }
}