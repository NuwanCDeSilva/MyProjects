using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Imports;
using FastForward.SCMWeb.View.Reports.Wharf;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Wharf
{
    public partial class Cusdec : BasePage
    {
        #region variable
        bool oIsUpdate = false;
        string oSubType = "N/A";
        private MasterCompany oMstCom { get { return (MasterCompany)Session["oMstCom"]; } set { Session["oMstCom"] = value; } }
        private CusdecCommon oCusdecCommon { get { return (CusdecCommon)Session["oCusdecCommon"]; } set { Session["oCusdecCommon"] = value; } }
        private ImpCusdecHdr oCusdecHdr { get { return (ImpCusdecHdr)Session["oCusdecHdr"]; } set { Session["oCusdecHdr"] = value; } }
        private List<ImpCusdecItm> oCusdecItm { get { return (List<ImpCusdecItm>)Session["oCusdecItm"]; } set { Session["oCusdecItm"] = value; } }
        private List<ImpCusdecItmCost> oCusdecItmCost { get { return (List<ImpCusdecItmCost>)Session["oCusdecItmCost"]; } set { Session["oCusdecItmCost"] = value; } }
        private List<ImpCusdecCost> oCusdecCost { get { return (List<ImpCusdecCost>)Session["oCusdecCost"]; } set { Session["oCusdecCost"] = value; } }
        private List<ImportsBLContainer> oCusdecContainer { get { return (List<ImportsBLContainer>)Session["oCusdecContainer"]; } set { Session["oCusdecContainer"] = value; } }
        private List<HsCode> oCusdecDutyElement { get { return (List<HsCode>)Session["oCusdecDutyElement"]; } set { Session["oCusdecDutyElement"] = value; } }
        private List<HsCode> oCusdecDutyElementTot { get { return (List<HsCode>)ViewState["oCusdecDutyElementTot"]; } set { ViewState["oCusdecDutyElementTot"] = value; } }
        private Int32 oSelectItemLine { get { return (Int32)Session["oSelectItemLine"]; } set { Session["oSelectItemLine"] = value; } }

        #endregion

        #region Common Search functions
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.CusdecEntries:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator + oMstCom.Mc_anal19 + seperator + Request.QueryString["CUSTTYPE"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Declarant:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator + Convert.ToString(Session["UserSBU"]) + seperator + 0 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CusdecReq:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator + Request.QueryString["CUSTTYPE"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HsCode:
                    {
                        //string toCountry = "";
                        //MasterCompany COM = CHNLSVC.General.GetCompByCode(Convert.ToString(Session["UserCompanyCode"]));
                        //if (COM != null) toCountry = COM.Mc_anal19;

                        paramsText.Append(oMstCom.Mc_anal19);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ProcedureCodes:
                    {
                        if (Request.QueryString["CUSTTYPE"].ToString() == "RE")
                        {
                            paramsText.Append(oMstCom.Mc_anal19 + seperator + Convert.ToString(Session["UserCompanyCode"]) + seperator + "C" + seperator + txtConsigneeCode.Text.ToString() + seperator + Request.QueryString["CUSTTYPE"].ToString() + seperator);
                        }
                        else
                        {
                            paramsText.Append(oMstCom.Mc_anal19 + seperator + Convert.ToString(Session["UserCompanyCode"]) + seperator + "" + seperator + "" + seperator + Request.QueryString["CUSTTYPE"].ToString() + seperator);
                        }
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        public void BindUCtrlDDLDataNew(DataTable _dataSource)
        {
            this.ddlSearchby.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchby.Items.Add(col.ColumnName);
            }
            this.ddlSearchby.SelectedIndex = 0;
        }

        private void FilterData()
        {
            if (ViewState["SEARCH"] != null)
            {
                if (lblvalue.Text == "CusdecEntries")
                {
                    if (bondApprove.Checked)
                    {
                        string status = "A";
                        //ValidateTrue();
                        ViewState["SEARCH"] = null;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                        DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeaderWithStus(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.ToUpper().Trim(), status);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        ViewState["SEARCH"] = result;
                        mpUserPopup.Show(); Session["IsSearch"] = true;
                    }
                    if (bondPending.Checked)
                    {
                        string status = "P";
                        //ValidateTrue();
                        ViewState["SEARCH"] = null;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                        DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeaderWithStus(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.ToUpper().Trim(), status);
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        ViewState["SEARCH"] = result;
                        mpUserPopup.Show(); Session["IsSearch"] = true;

                    }
                    else
                    {

                        //ValidateTrue();
                        ViewState["SEARCH"] = null;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                        DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeader(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        ViewState["SEARCH"] = result;
                        mpUserPopup.Show(); Session["IsSearch"] = true;
                    }
                }
                else if (lblvalue.Text == "BLHeader")
                {
                    string _cusdecType = Request.QueryString["CUSTTYPE"];
                    if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
                    {
                        //ValidateTrue();
                        ViewState["SEARCH"] = null;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                        DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        ViewState["SEARCH"] = result;
                        mpUserPopup.Show(); Session["IsSearch"] = true;
                    }
                }
                else if (lblvalue.Text == "CusdecReq")
                {
                    string _cusdecType = Request.QueryString["CUSTTYPE"];
                    if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                    {
                        //ValidateTrue();
                        ViewState["SEARCH"] = null;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecReq);
                        DataTable result = CHNLSVC.CommonSearch.SearchCusdecReq(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        ViewState["SEARCH"] = result;
                        mpUserPopup.Show(); Session["IsSearch"] = true;
                    }
                }
                else if (lblvalue.Text == "Item")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "ProcedureCode")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProcedureCodes);
                    DataTable result = CHNLSVC.CommonSearch.SearchGetProcedureCode(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "HS")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                    DataTable result = CHNLSVC.CommonSearch.SearchGetHsCode(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "CountyOfOrigin")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
            }
        }

        private void FilterDataNew()
        {
            if (ViewState["SEARCH"] != null)
            {
                if (lblSerValue.Text == "BLHeader")
                {
                    string _cusdecType = Request.QueryString["CUSTTYPE"];
                    if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
                    {
                        //ValidateTrue();
                        ViewState["SEARCH"] = null;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                        DataTable result = CHNLSVC.CommonSearch.SearchBLHeaderNew(SearchParams, ddlSearchby.Text, txtSearchbywordNew.Text.Trim(), 0, DateTime.Today, DateTime.Today);
                        dgvSearch.DataSource = new int[] { };
                        if (result != null)
                        {
                            if (result.Rows.Count > 0)
                            {
                                dgvSearch.DataSource = result;
                            }
                        }
                        dgvSearch.DataBind();
                        ViewState["SEARCH"] = result;
                        serPopup.Show(); Session["IsSearchNew"] = true;
                    }
                }
                else if (lblSerValue.Text == "CusdecReq")
                {
                    string _cusdecType = Request.QueryString["CUSTTYPE"];
                    if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                    {
                        //ValidateTrue();
                        ViewState["SEARCH"] = null;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecReq);
                        DataTable result = CHNLSVC.CommonSearch.SearchCusdecReqNew(SearchParams, ddlSearchby.Text, txtSearchbywordNew.Text.Trim(), 0, DateTime.Today, DateTime.Today);
                        dgvSearch.DataSource = result;
                        dgvSearch.DataBind();
                        ViewState["SEARCH"] = result;
                        serPopup.Show(); Session["IsSearchNew"] = true;
                    }
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session["IsSearch"] = null;
            mpUserPopup.Hide();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = grdResult.SelectedRow.Cells[1].Text;
                string desc = grdResult.SelectedRow.Cells[2].Text;
                Session["IsSearch"] = null;
                mpUserPopup.Hide();

                if (lblvalue.Text == "BLHeader" || lblvalue.Text == "CusdecReq")
                {
                    txtBLno.Text = code;
                    txtBLno_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CusdecEntries")
                {
                    txtRefno.Text = code;
                    txtRefno_TextChanged(null, null);
                }
                else if (lblvalue.Text == "HS")
                {
                    if (oSelectItemLine > 0)
                    {
                        ImpCusdecItm oEditedItem = oCusdecItm.Find(x => x.Cui_line == oSelectItemLine);
                        oEditedItem.Cui_hs_cd = code;
                        BindItems();
                        btnUpdateItem_Click(null, null);
                    }
                    oSelectItemLine = 0;
                }
                else if (lblvalue.Text == "ProcedureCode")
                {
                    txtProceCode.Text = code;
                    txtProceCode_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CountyOfOrigin")
                {
                    if (orignctry.Checked)
                    {
                        txtCrtyOriginCode.Text = code;
                        txtCrtyOrigin.Text = desc;
                    }
                    else if (destctry.Checked)
                    {
                        txtCrtyDesti.Text = desc;
                        txtCrtyDestiCode.Text = code;
                    }
                    else if (expctry.Checked)
                    {
                        txtCrtyExport.Text = desc;
                        txtCrtyExportCode.Text = code;
                    }
                    else if (tradctry.Checked)
                    {
                        txtTradingCountry.Text = code;
                        txtTradingCountryName.Text = desc;
                    }
                    
                }

                ViewState["SEARCH"] = null;
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
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                mpUserPopup.Show(); Session["IsSearch"] = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private bool isdecimal(string txt)
        {
            decimal _deciVal;
            return decimal.TryParse(txt, out _deciVal);
        }
        #endregion

        #region Common Messages
        private void DisplayMessage(String Msg, Int32 option, Exception ex = null)
        {
            //Msg = Msg.Replace(@"\r", "").Replace(@"\n", "");
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
                //WriteErrLog(Msg, ex);
            }
            else if (option == 5) //With clear window
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');window.location.href ='Cusdec.aspx?CUSTTYPE=" + Request.QueryString["CUSTTYPE"] + "&CUSTSTYPE=" + Request.QueryString["CUSTSTYPE"] + "';", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');window.location = 'Cusdec.aspx?CUSTTYPE='" + Request.QueryString["CUSTTYPE"].ToString() + "&CUSTSTYPE='" + Request.QueryString["CUSTSTYPE"] + "';", true);
                //WriteErrLog(Msg, ex);
            }

        }

        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        private void WriteErrLog(string err, Exception ex)
        {
            try
            {
                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data.txt"), true))
                {
                    if (ex == null)
                    {
                        _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + " | " + err + "   || Exception : " + err);
                    }
                    else
                    {
                        _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + " | " + err + "   || Exception : " + ex.Message);

                    }
                }
            }
            catch (Exception _err)
            {
                DisplayMessage(_err.Message, 4);
            }
        }


        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        #endregion

        private void LoadCachedObjects()
        {
            //oMstCom = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
            oMstCom = CHNLSVC.General.GetCompByCode(Convert.ToString(Session["UserCompanyCode"]));
        }

        private void LoadDefaultValues()
        {
            string _cusdecType = Request.QueryString["CUSTTYPE"];
            VisbilbleToBondApproveStus(_cusdecType);
            string _cusdecSubType = Request.QueryString["CUSTSTYPE"];
            List<CustomsProcedureCodes> _custProcCds = CHNLSVC.Financial.GetCustomsProcedureCodes(oMstCom.Mc_anal19, Convert.ToString(Session["UserCompanyCode"]), string.Empty, string.Empty, _cusdecType, string.Empty);
            if (_custProcCds == null || _custProcCds.Count <= 0)
            {
                DisplayMessage("Procedure codes are not setup!", 1);
                return;
            }
            CustomsProcedureCodes _custProcCd = _custProcCds.FindAll(x => x.Mph_act == 1 && x.Mph_def == 1)[0];
            if (_custProcCd == null)
            {
                DisplayMessage("Procedure code default settings not setup!", 1);
                return;
            }

            CusdecTypes _cusdecTypeInfor = CHNLSVC.Financial.GetCusdecTypeInfor(oMstCom.Mc_anal19).FindAll(y => y.Rcut_tp == _cusdecType)[0];
            if (_custProcCd == null)
            {
                DisplayMessage("Cusdec type infor not setup!", 1);
                return;
            }
            oCusdecCommon = CHNLSVC.Financial.Get_CusdecCommon(oMstCom.Mc_anal19);

            if (_cusdecSubType != "N/A")
            {
                lblHeaderName.Text = _cusdecTypeInfor.Rcut_desc + " Entry [" + _cusdecSubType + "]";
            }
            else
            {
                lblHeaderName.Text = _cusdecTypeInfor.Rcut_desc + " Entry";
            }
            lblDocNo.Text = _cusdecTypeInfor.Rcut_tp + " Entry No";

            DataTable _dtOffEntry = CHNLSVC.Financial.Get_OFFICE_ENTRY(oMstCom.Mc_anal19, _cusdecType, _cusdecSubType);
            DataTable _dtlocGoods = CHNLSVC.Financial.Get_LOC_OF_GOODS(oMstCom.Mc_anal19, _cusdecType, _cusdecSubType);

            if (_dtOffEntry != null && _dtOffEntry.Rows.Count > 0)
            {
                var _def1 = _dtOffEntry.AsEnumerable().Where(x => x.Field<Int16>("RCUOE_DEF") == 1).Select(x => x.Field<string>("RCUOE_CD")).ToList();
                string _def01 = Convert.ToString(_def1[0]);

                ddlOfficeOfEntry.DataSource = _dtOffEntry;
                ddlOfficeOfEntry.DataTextField = "RCUOE_CD";
                ddlOfficeOfEntry.DataValueField = "RCUOE_DESC";
                ddlOfficeOfEntry.DataBind();
                //ddlOfficeOfEntry.Items.Insert(0, new ListItem("--Select-", ""));
                ddlOfficeOfEntry.SelectedItem.Text = _def01;
            }

            if (_dtlocGoods != null && _dtlocGoods.Rows.Count > 0)
            {
                var _def1 = _dtlocGoods.AsEnumerable().Where(x => x.Field<Int16>("rculg_def") == 1).Select(x => x.Field<string>("rculg_cd")).ToList();
                string _def01 = Convert.ToString(_def1[0]);
                ddlLocOfGoods.DataSource = _dtlocGoods;
                ddlLocOfGoods.DataTextField = "RCULG_CD";
                ddlLocOfGoods.DataValueField = "RCULG_DESC";
                ddlLocOfGoods.DataBind();
                //ddlLocOfGoods.Items.Insert(0, new ListItem("--Select-", ""));
                ddlLocOfGoods.SelectedItem.Text = _def01;
            }
            if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
            {
                lblBLNo.Text = "Request No";
                lblDocNo.Text = "Tobond No";
            }
            if (_cusdecType == "EXP")
            {
                txtDecSeqNo.ReadOnly = false; 
            }


            txtProceCode.Text = _custProcCd.Mph_proc_cd;
            txtDec1.Text = _custProcCd.Mph_decl_1;
            txtDec2.Text = _custProcCd.Mph_decl_2;
            txtDec3.Text = _custProcCd.Mph_decl_3;
            txtDocDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

            txtCurrencyOwn.Text = oMstCom.Mc_cur_cd;
            txtProceCode1.Text = _custProcCd.Mph_print_1;
            txtProceCode2.Text = _custProcCd.Mph_print_2;
            txtLicenceNo.Text = oCusdecCommon.Rcsc_licence_no;
            txtAcNo.Text = oCusdecCommon.Rcsc_acc_no;
            txtWarehouseID.Text = oCusdecCommon.Rcsc_wh_no;

            txtAuthorized.Text = oCusdecCommon.Rcsc_auth_by;
            txtSubmitted.Text = oCusdecCommon.Rcsc_submit_by;
            txtIDNo.Text = oCusdecCommon.Rcsc_id_no;
            txtDocRecDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

            if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
            {
                btnUpdateHS.Visible = true;
                btnupdatemass.Visible = true;
            }

            //if (_cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
            //{
            //    txtMarksNumbers.ReadOnly = true;
            //    txtMarksNumbers.CssClass = "form-control";
            //}

        }

        //Dulaj 2018/Dec/18
        private void VisbilbleToBondApproveStus(string bondType)
        {
            if(bondType=="TO")
            {
                bondApprove.Visible = true;
                bondPending.Visible = true;
                bondAll.Visible = true;
                lbtnapprove.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCachedObjects();

                if (!IsPostBack)
                { clearAll(); }
                else
                {
                    if (Session["IsSearch"] != null && Convert.ToBoolean(Session["IsSearch"]) == true)
                    {
                        Session["IsSearch"] = null;
                        mpUserPopup.Show();
                        txtSearchbyword.Focus();
                    }
                    if (Session["IsSearchNew"] != null && Convert.ToBoolean(Session["IsSearchNew"]) == true)
                    {
                        Session["IsSearchNew"] = null;
                        serPopup.Show();
                        txtSearchbywordNew.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void clearAll()
        {
            hdfNewRecord.Value = null;
            hdfClear.Value = null;

            oCusdecHdr = null;
            oCusdecItm = null;
            oCusdecContainer = null;
            oCusdecDutyElement = null;
            oCusdecDutyElementTot = null;
            oCusdecCost = null;

            dgvCostItems.DataSource = new int[] { };
            dgvCostItems.DataBind();

            dgvContainers.DataSource = new int[] { };
            dgvContainers.DataBind();

            dgvDutyElements.DataSource = new int[] { };
            dgvDutyElements.DataBind();

            dgvDutyElementsTot.DataSource = new int[] { };
            dgvDutyElementsTot.DataBind();

            dgvItems.DataSource = new int[] { };
            dgvItems.DataBind();

            dgvHSHistory.DataSource = new int[] { };
            dgvHSHistory.DataBind();

            LoadDefaultValues();
            LoadPkgUOM();
            LoadAggrRef();

            txtSerToDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
            txtSeFromDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
            bondAll.Checked = true;
            setStatus("");
           // Label1status.Text = "";
        }

        #region Events

        protected void btnProceSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProcedureCodes);
                DataTable result = CHNLSVC.CommonSearch.SearchGetProcedureCode(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ProcedureCode";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpUserPopup.Show(); Session["IsSearch"] = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void btnBLSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    txtSearchbywordNew.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    DataTable result = CHNLSVC.CommonSearch.SearchBLHeaderNew(SearchParams, null, null,0,DateTime.Today,DateTime.Today.AddMonths(-2));
                    dgvSearch.DataSource = result;
                    dgvSearch.DataBind();
                    lblSerValue.Text = "BLHeader";
                    DataTable dt = new DataTable();
                    dt.Columns.Add("DOC");
                    dt.Columns.Add("BL");
                    dt.Columns.Add("REFERENCE");
                    dt.Columns.Add("STATUS");
                    dt.Columns.Add("LOCATION");
                    //dt.Columns.Add("CUSTOMER");
                    BindUCtrlDDLDataNew(dt);
                    ViewState["SEARCH"] = result;
                    txtSerToDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    txtSeFromDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    serPopup.Show(); Session["IsSearchNew"] = true;
                }

                if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    txtSearchbywordNew.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecReq);
                    DataTable result = CHNLSVC.CommonSearch.SearchCusdecReqNew(SearchParams, null, null,0, DateTime.Today,DateTime.Today);
                    DataView dv = result.DefaultView;
                    dv.Sort = "REQ NO";
                    DataTable sortedDT = dv.ToTable();
                    dgvSearch.DataSource = sortedDT;
                    dgvSearch.DataBind();
                    lblSerValue.Text = "CusdecReq";
                    DataTable dt = new DataTable();
                    dt.Columns.Add("REQ NO");
                    dt.Columns.Add("TOBOND NO");
                    dt.Columns.Add("BL NO");
                    dt.Columns.Add("STATUS");
                    dt.Columns.Add("LOCATION");
                    dt.Columns.Add("CUSTOMER");
                    BindUCtrlDDLDataNew(dt);
                    ViewState["SEARCH"] = sortedDT;
                    txtSerToDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    txtSeFromDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                    serPopup.Show(); Session["IsSearchNew"] = true;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void btnRefSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                string _cusdecType2 = Request.QueryString["CUSTSTYPE"];
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
                    if (bondApprove.Checked)
                    {
                        string status = "A";
                        result = CHNLSVC.CommonSearch.SearchCusdecHeaderWithStus(SearchParams, null, null, status); 
                    }
                    if (bondPending.Checked)
                    {
                        string status = "P";
                        result = CHNLSVC.CommonSearch.SearchCusdecHeaderWithStus(SearchParams, null, null, status); 
                    }
                    

                   
                       
                    
                     
                    
                }
              
                if (result.Rows.Count>0)
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
              
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CusdecEntries";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpUserPopup.Show(); Session["IsSearch"] = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtRefno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtRefno.Text))
                {
                    string _cusdecType = Request.QueryString["CUSTTYPE"];
                    if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR" || _cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                    {
                        LoadCusdecDetails();                        
                    }
                    else
                    {
                        DisplayMessage("Invalid Entry No!", 2);
                        txtRefno.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtBLno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtBLno.Text))
                {
                    string _cusdecType = Request.QueryString["CUSTTYPE"];
                    if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
                    {
                        LoadBLDetails();
                    }
                    else if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                    {
                        LoadCusdecReqDetails();
                    }
                    else
                    {
                        DisplayMessage("Invalid BL No!", 2);
                        txtBLno.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtProceCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtProceCode.Text))
                {
                    string _cusdecType = Request.QueryString["CUSTTYPE"];
                    List<CustomsProcedureCodes> _custProcCds = CHNLSVC.Financial.GetCustomsProcedureCodes(oMstCom.Mc_anal19, Convert.ToString(Session["UserCompanyCode"]), string.Empty, string.Empty, _cusdecType, txtProceCode.Text.ToString()).FindAll(x => x.Mph_act == 1);
                    if (_custProcCds != null && _custProcCds.Count > 0)
                    {
                        CustomsProcedureCodes _custProcCd = _custProcCds.FindAll(x => x.Mph_act == 1)[0];
                        txtDec1.Text = _custProcCd.Mph_decl_1;
                        txtDec2.Text = _custProcCd.Mph_decl_2;
                        txtDec3.Text = _custProcCd.Mph_decl_3;
                        txtProceCode1.Text = _custProcCd.Mph_print_1;
                        txtProceCode2.Text = _custProcCd.Mph_print_2;
                    }
                    else
                    {
                        DisplayMessage("Invalid Procedure Code!", 2);
                        txtProceCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnUpdateItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtLineNo.Text))
                {
                    Int32 lineNo;
                    string desc = "";
                    if (Int32.TryParse(txtLineNo.Text.ToString(), out lineNo))
                    {
                        ImpCusdecItm oEditedItem = oCusdecItm.Find(x => x.Cui_line == lineNo);
                        if (chkIgnoreCountry.Checked == true)
                        {
                            //oEditedItem.Cui_def_cnty = "DEF";
                            oEditedItem.Cui_def_cnty = txtCtryOriginItem.Text.ToString();
                        }
                        desc = txtItemDesc.Text.ToString().Trim();
                        
                        oEditedItem.Cui_net_mass = Convert.ToDecimal(txtNetMassItem.Text);
                        oEditedItem.Cui_gross_mass = Convert.ToDecimal(txtGrossMassItem.Text);
                        txtNetMassItem.Text = string.Format("{0:#,##0.00}", double.Parse(txtNetMassItem.Text));
                        txtGrossMassItem.Text = string.Format("{0:#,##0.00}", double.Parse(txtGrossMassItem.Text));
                        string preff = txtPreference.Text.ToString();

                        if (!string.IsNullOrEmpty(txtItemDesc.Text))
                        {
                            oEditedItem.Cui_itm_desc = txtItemDesc.Text.ToString();
                            foreach (var item in oCusdecItm.Where(w => w.Cui_line == lineNo))
                            {
                                item.Cui_itm_desc = txtItemDesc.Text.ToString();
                                item.Cui_preferance = preff;
                            }
                        }
                       
                        CHNLSVC.Financial.UpdateBLItemHS(txtBLno.Text.ToString(), lineNo, oEditedItem.Cui_hs_cd.ToString(), txtGrossMassItem.Text.ToString(), txtNetMassItem.Text.ToString(), preff, txtRefno.Text.ToString());
                        if (chkallhschk.Checked == true)
                        {
                            foreach (var item in oCusdecItm.Where(w => w.Cui_itm_desc == desc))
                            {
                                item.Cui_hs_cd = txtHSCode.Text.ToString();
                                CHNLSVC.Financial.UpdateBLItemHS(txtBLno.Text.ToString(), item.Cui_line, item.Cui_hs_cd, item.Cui_gross_mass.ToString(), item.Cui_net_mass.ToString(), item.Cui_preferance, txtRefno.Text.ToString());
                            }
                        }else if(chkallcat1.Checked)
                        {
                           // lblcat2.Text
                            foreach (var item in oCusdecItm.Where(w => w.ItemCat1 == txtcat1.Text.ToString()))
                            {
                                item.Cui_hs_cd = txtHSCode.Text.ToString();
                                CHNLSVC.Financial.UpdateBLItemHS(txtBLno.Text.ToString(), item.Cui_line, item.Cui_hs_cd, item.Cui_gross_mass.ToString(), item.Cui_net_mass.ToString(), item.Cui_preferance, txtRefno.Text.ToString());
                            }
                        }
                        else if (Chkallcat2.Checked)
                        {
                            foreach (var item in oCusdecItm.Where(w => w.ItemCat2 == txtcat2.Text.ToString()))
                            {
                                item.Cui_hs_cd = txtHSCode.Text.ToString();
                                CHNLSVC.Financial.UpdateBLItemHS(txtBLno.Text.ToString(), item.Cui_line, item.Cui_hs_cd, item.Cui_gross_mass.ToString(), item.Cui_net_mass.ToString(), item.Cui_preferance, txtRefno.Text.ToString());
                            }
                        }
                        else if (chkmodalhs.Checked)
                        {
                            foreach (var item in oCusdecItm.Where(w => w.Cui_model == txtitemmodel.Text.ToString()))
                            {
                                item.Cui_hs_cd = txtHSCode.Text.ToString();
                                CHNLSVC.Financial.UpdateBLItemHS(txtBLno.Text.ToString(), item.Cui_line, item.Cui_hs_cd, item.Cui_gross_mass.ToString(), item.Cui_net_mass.ToString(), item.Cui_preferance, txtRefno.Text.ToString());
                            }
                        }
                        DisplayMessage("Item updated!", 3);
                    }
                    else
                    {
                        DisplayMessage("Invalid Item Line No", 2);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string _returnMsg = string.Empty;
            int _result = 0;
            try
            {
            if (validateHeader())
            {
                if (!string.IsNullOrEmpty(txtRefno.Text.ToString())) oIsUpdate = true;
                oCusdecHdr.CUH_PROCE_CD_1 = txtProceCode1.Text.ToString();
                oCusdecHdr.CUH_PROCE_CD_2 = txtProceCode2.Text.ToString(); 
                oCusdecHdr.CUH_PROC_CD = txtProceCode.Text.ToString().Trim().ToUpper();
                oCusdecHdr.CUH_DECL_1 = txtDec1.Text.ToString().Trim().ToUpper();
                oCusdecHdr.CUH_DECL_2 = txtDec2.Text.ToString().Trim().ToUpper();
                oCusdecHdr.CUH_DECL_3 = txtDec3.Text.ToString().Trim().ToUpper();
                oCusdecHdr.CUH_TOT_PKG_UNIT = ddlTotalPkgUOM.Text;
                oCusdecHdr.CUH_TOT_PKG = txtTotalPackages.Text.ToString();
                oCusdecHdr.CUH_COM_CHG = Convert.ToDecimal(txtComChgAmount.Text.ToString());
                oCusdecHdr.CUH_EX_RT = Convert.ToDecimal(txtExRate.Text.ToString());                
                oCusdecHdr.CUH_STUS = "A";
                if (Request.QueryString["CUSTTYPE"].ToString() == "TO")//Added By Dulaj 2018/Dec/18 to save pending for to bond
                {
                    oCusdecHdr.CUH_STUS = "P";
                }
                oCusdecHdr.CUH_CRE_BY = Convert.ToString(Session["UserID"]);
                oCusdecHdr.CUH_CRE_SESSION = Convert.ToString(Session["SessionID"]);
                oCusdecHdr.CUH_MOD_BY = Convert.ToString(Session["UserID"]);
                oCusdecHdr.CUH_MOD_SESSION = Convert.ToString(Session["SessionID"]);
                if (string.IsNullOrEmpty(txtTotGrossMass.Text)) txtTotGrossMass.Text = "0";
                if (string.IsNullOrEmpty(txtTotNetMass.Text)) txtTotNetMass.Text = "0";
                oCusdecHdr.CUH_TOT_GROSS_MASS = Convert.ToDecimal(txtTotGrossMass.Text.ToString());
                oCusdecHdr.CUH_TOT_NET_MASS = Convert.ToDecimal(txtTotNetMass.Text.ToString());
                oCusdecHdr.CUH_GROSS_MASS = Convert.ToDecimal(txtTotGrossMass.Text.ToString());
                oCusdecHdr.CUH_NET_MASS = Convert.ToDecimal(txtTotNetMass.Text.ToString());
                oCusdecHdr.CUH_MAIN_HS = txtMainHS.Text.ToString();
                oCusdecHdr.CUH_VOYAGE = txtVoyageNo.Text.ToString();
                oCusdecHdr.CUH_VOYAGE_DT = Convert.ToDateTime(txtVoyageDate.Text);
                oCusdecHdr.CUH_WH_AND_PERIOD = txtWarehouseID.Text.ToString();
                oCusdecHdr.CUH_MARKS_AND_NO = txtMarksNumbers.Text.ToString();
                oCusdecHdr.CUH_FILE_NO = txtFileNo.Text.ToString();
                if (string.IsNullOrEmpty(txtItems.Text)) txtItems.Text = "0";
                oCusdecHdr.CUH_ITEMS_QTY = Convert.ToInt32(txtItems.Text.ToString());
                oCusdecHdr.CUH_NATURE_OF_TRANCE = txtNatuOfTranst.Text.ToString();
                oCusdecHdr.CUH_LISION_NO = txtLicenceNo.Text.ToString();
                oCusdecHdr.CUH_DECL_SEQ_NO = txtDecSeqNo.Text.ToString();
                oCusdecHdr.CUH_PPC_NO = txtPreviousDocument.Text.ToString();
                oCusdecHdr.CUH_TRADING_COUNTRY = txtTradingCountry.Text.ToString();
                oCusdecHdr.CUH_REF_NO = txtReference.Text.ToString();
                oCusdecHdr.CUH_TERMS_OF_PAYMENT = txtTermsOfPayment.Text.ToString();
                oCusdecHdr.CUH_BANK_CD = txtBankCode.Text.ToString();
                oCusdecHdr.CUH_BANK_NAME = txtBankName.Text.ToString();
                oCusdecHdr.CUH_BANK_BRANCH = txtBranch.Text.ToString();
                if (string.IsNullOrEmpty(txtComChgAmount.Text)) txtComChgAmount.Text = "0";
                oCusdecHdr.CUH_COM_CHG = Convert.ToDecimal(txtComChgAmount.Text.ToString());
                oCusdecHdr.CUH_OFFICE_OF_ENTRY = ddlOfficeOfEntry.SelectedItem.Text.ToString();
                oCusdecHdr.CUH_LOCATION_OF_GOODS = ddlLocOfGoods.SelectedItem.Text.ToString();
                oCusdecHdr.CUH_PREFERENCE = txtPreference.Text.ToString();
               // oCusdecHdr.CUH_ACC_NO = txtAcNo.Text.ToString();
                oCusdecHdr.CUH_CONTAINER_FCL = txtContainerFCL.Text.ToString();
                oCusdecHdr.CUH_FCL = txtFCL.Text.ToString();
                oCusdecHdr.CUH_RMK = txtDesc.Text;
                oCusdecHdr.CUH_CUSDEC_ENTRY_NO = txtCusdecEntryNo.Text.ToString();
                oCusdecHdr.CUH_DOC_REC_DT = Convert.ToDateTime(txtDocRecDate.Text);
                oCusdecHdr.CUH_CUSTOM_LC_TP = txtTermsOfPaymentCustom.Text.ToString();
                oCusdecHdr.CUH_BANK_REF_CD = txtFinRefNo.Text.ToString();
                oCusdecHdr.CUH_FIN_SETTLE_TEXT = txtFinSettlement.Text.ToString();
                oCusdecHdr.CUH_VESSEL = txtVessleNo.Text.ToString();
                oCusdecHdr.CUH_PLACE_OF_LOADING = txtPlaceOfLoading.Text.ToString();
                oCusdecHdr.CUH_VOYAGE = txtVoyageNo.Text.ToString();
                oCusdecHdr.CUH_CONSI_TIN = txtConsigneeTin.Text.ToString();
                oCusdecHdr.CUH_CNTY_OF_ORIGIN = txtCrtyOriginCode.Text.ToString();
                oCusdecHdr.CUH_CNTY_OF_EXPORT = txtCrtyExportCode.Text.ToString();
                oCusdecHdr.CUH_EXP_CNTY_NAME = txtCrtyExport.Text.ToString();
                oCusdecHdr.CUH_DESTI_CNTY_NAME = txtCrtyDesti.Text.ToString();
                oCusdecHdr.CUH_CNTY_OF_DESTINATION = txtCrtyDestiCode.Text.ToString();
                oCusdecHdr.CUH_ORIGIN_CNTY_NAME = txtCrtyOrigin.Text.ToString();
                oCusdecHdr.CUH_TRADING_COUNTRY = txtTradingCountry.Text.ToString();             

                oCusdecHdr.CUH_CUSDEC_ENTRY_DT =Convert.ToDateTime( txtDocRecDate.Text.ToString());
                //Added by dulaj 2018/jul/16
                string accountNo = CHNLSVC.Sales.GetAccountNo(oCusdecHdr.CUH_CONSI_CD, Session["UserCompanyCode"].ToString());
                if (!(accountNo.Equals(string.Empty)))
                {
                    oCusdecHdr.CUH_ACC_NO = accountNo;
                }
                else
                {
                    oCusdecHdr.CUH_ACC_NO = oCusdecCommon.Rcsc_acc_no;
                }
                //

                if (chkIgnoreCountry.Checked == true)
                { oCusdecHdr.CUH_IGNORE = 1; }
                else
                { oCusdecHdr.CUH_IGNORE = 0; }

                string Docnum="";
                _result = CHNLSVC.Financial.SaveCusdec(oCusdecHdr, oCusdecItm, oCusdecCost, oIsUpdate, out _returnMsg, 1, out Docnum, true, false);
                if (_result > 0)
                {
                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + _returnMsg + "');window.location.href ='Cusdec.aspx?CUSTTYPE=" + Request.QueryString["CUSTTYPE"] + "&CUSTSTYPE=" + Request.QueryString["CUSTSTYPE"] + "';", true);
                    //DisplayMessage("aDOO SAVE UNA!!!", 5);
                   // DisplayMessage(_returnMsg, 3);
                    txtRefno.Text = Docnum;
                   // if (Request.QueryString["CUSTTYPE"].ToString() == "TO")//Added By Dulaj 2018/Dec/18 to save pending for to bond
                  //  {
                   //     DisplayMessage("Successfully Saved " + Docnum, 3);
                 //   }
                  //  else
                  //  {
                        PrintResultsInvoice(Docnum);
                  //  }
                }
                else if (_result==-99)
                {
                    DisplayMessage(_returnMsg, 4);
                }
                else
                {
                    DisplayMessage(_returnMsg, 1);
                }
            }
        }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message.ToString() , 4);
        }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void chkHSHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHSHistory.Checked == true)
            { pnlDutyElements.Visible = false; pnlHSHistory.Visible = true; pnlDutyElementsTotal.Visible = false; chkAllTax.Checked = false; txtElementTotal.Text = ""; }
            else
            { pnlDutyElements.Visible = true; pnlHSHistory.Visible = false; pnlDutyElementsTotal.Visible = false; chkAllTax.Checked = false; txtElementTotal.Text = ""; }
        }
        #endregion

        #region Methods
        private void LoadPkgUOM()
        {
            List<ComboBoxObject> oItem = CHNLSVC.Financial.GET_PKG_UOM();
            ddlTotalPkgUOM.DataSource = oItem;
            ddlTotalPkgUOM.DataTextField = "Text";
            ddlTotalPkgUOM.DataValueField = "Value";
            ddlTotalPkgUOM.DataBind();
        }
        private void LoadAggrRef()
        {
            List<ComboBoxObject> oItem = CHNLSVC.Financial.GET_AGGR_REF();
            txtPreference.DataSource = oItem;
            txtPreference.DataTextField = "Text";
            txtPreference.DataValueField = "Value";
            txtPreference.DataBind();
        }
        private bool validateHeader()
        {
            bool status = true;
            if (string.IsNullOrEmpty(txtBLno.Text))
            {
                DisplayMessage("Please select a reference number", 3);
                txtBLno.Focus();
                status = false;
                return status;
            }

            if (string.IsNullOrEmpty(txtComChgAmount.Text))
            {
                DisplayMessage("Please enter the computer charges", 3);
                txtComChgAmount.Focus();
                status = false;
                return status;
            }
            else
            {
                Decimal _num;
                if (!Decimal.TryParse(txtComChgAmount.Text.ToString(), out _num))
                {
                    DisplayMessage("Please enter the valid computer charges", 3);
                    txtComChgAmount.Focus();
                    status = false;
                    return status;
                }
            }

            if (string.IsNullOrEmpty(txtExRate.Text))
            {
                DisplayMessage("Exchange rate can't be blank", 3);
                txtExRate.Focus();
                status = false;
                return status;
            }
            else
            {
                Decimal _num;
                if (!Decimal.TryParse(txtExRate.Text.ToString(), out _num))
                {
                    DisplayMessage("Invalid exchange rate amount", 3);
                    txtExRate.Focus();
                    status = false;
                    return status;
                }
            }

            if (string.IsNullOrEmpty(txtTotalPackages.Text))
            {
                DisplayMessage("Total no of packages can't be blank", 3);
                txtTotalPackages.Focus();
                status = false;
                return status;
            }
            else
            {
                Decimal _num;
                if (!Decimal.TryParse(txtTotalPackages.Text.ToString(), out _num))
                {
                    DisplayMessage("Invalid total no of packages", 3);
                    txtTotalPackages.Focus();
                    status = false;
                    return status;
                }
            }

            if (oCusdecItm == null || oCusdecItm.Count == 0)
            {
                DisplayMessage("Items not found", 3);
                status = false;
                return status;
            }

            return status;
        }

        #region Cusdec Cost Data Grid
        protected void dgvCostItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvCostItems.EditIndex = e.NewEditIndex;
            GridViewRow dr = dgvCostItems.Rows[e.NewEditIndex];
            Label lblIbcs_ele_cd = dr.FindControl("lblCus_ele_cd") as Label;
            TextBox txtIbcs_amt = dr.FindControl("txtCus_amt") as TextBox;
            
            if (lblIbcs_ele_cd.Text.ToUpper() == "COST")
            {
                //txtIbcs_amt.ReadOnly = true;
                dgvCostItems.EditIndex = -1;
                return;
            }

            lblIbcs_ele_cd.Focus();
            BindCostItems(e.NewEditIndex);
        }

        protected void dgvCostItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            dgvCostItems.EditIndex = -1;
            BindCostItems();
        }
        protected void dgvCostItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                try
                {
                    dgvCostItems.EditIndex = -1;
                    BindCostItems();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        protected void dgvCostItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        private void BindCostItems(int index=99)
        {
            dgvCostItems.DataSource = new int[] { };
            dgvCostItems.DataBind();
            dgvCostItems.DataSource = oCusdecCost.FindAll(x => x.Cus_ele_cat == "TOT").ToList();
            dgvCostItems.DataBind();

            for (int i = 0; i < dgvCostItems.Rows.Count; i++)
            {
                GridViewRow row = dgvCostItems.Rows[i];
                Label lblcus_act = (Label)row.FindControl("lblCus_act");
                Label lblCus_ele_cd = (Label)row.FindControl("lblCus_ele_cd");
                LinkButton lbtnedititem = (LinkButton)row.FindControl("lbtnedititem");


                if (lblcus_act.Text == "0")
                {
                    row.Visible = false;
                }

                if (lblCus_ele_cd.Text == "COST")
                {
                    lbtnedititem.Visible = false;
                }
            }
            if (index < 99 && index < dgvCostItems.Rows.Count)
            {
                GridViewRow dr = dgvCostItems.Rows[index];
                TextBox txtIbcs_amt = dr.FindControl("txtCus_amt") as TextBox;
                txtIbcs_amt.Focus();
            }
        }
        protected void OnUpdateCost(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblCus_ele_cd = (Label)row.FindControl("lblCus_ele_cd");
                TextBox txtCus_amt = (TextBox)row.FindControl("txtCus_amt");

                //if (Session["oCusdecCost"] != null)
                //{
                //oCusdecCost = (List<ImpCusdecCost>)Session["oCusdecCost"];
                if (!isdecimal(txtCus_amt.Text))
                {
                    DisplayMessage("Please enter valid amount", 2);
                    return;
                }
                ImpCusdecCost oEditedItem = oCusdecCost.Find(x => x.Cus_ele_cd == lblCus_ele_cd.Text);
                oEditedItem.Cus_amt = (txtCus_amt.Text == "") ? 0 : Convert.ToDecimal(txtCus_amt.Text);
                //Session["oCusdecCost"] = oCusdecCost;
                BindCostItems();
                dgvCostItems.EditIndex = -1;
                Decimal oTotalValue = 0;
                oTotalValue = oCusdecCost.Where(z => z.Cus_act == 1).Sum(x => x.Cus_amt);
                txtInvoiceAmt.Text = oTotalValue.ToString("N2");
                oCusdecHdr.CUH_TOT_AMT = Math.Round(oTotalValue, 2);
                txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                txtValueDetails.Text = Convert.ToString(oTotalValue * Convert.ToDecimal(txtExRate.Text.ToString()));
                txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));
                //}
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            BindCostItems();
        }
        protected void OnCancelCost(object sender, EventArgs e)
        {
            try
            {
                dgvCostItems.EditIndex = -1;
                BindCostItems();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        #endregion

        #region Containers Data Grid
        private void BindContainers()
        {
            dgvContainers.DataSource = new int[] { };
            
            dgvContainers.DataBind();
            dgvContainers.DataSource = oCusdecContainer;
            dgvContainers.DataBind();

            for (int i = 0; i < dgvContainers.Rows.Count; i++)
            {
                GridViewRow row = dgvContainers.Rows[i];
                Label lblIbc_act = (Label)row.FindControl("lblIbc_act");
                if (lblIbc_act.Text == "0")
                {
                    row.Visible = false;
                }
            }
        }
        #endregion

        #region Items Data Grid

        private void BindItems()
        {
            dgvItems.DataSource = new int[] { };
            dgvItems.DataBind();
            //if (Session["oCusdecItm"] != null)
            //{
            //    oCusdecItm = (List<ImpCusdecItm>)Session["oCusdecItm"];
            //    dgvItems.DataSource = oCusdecItm;
            //}
            //else
            //{
            //    dgvItems.DataSource = new int[] { };
            //}
            if (chkitem.Checked)
            {
                oCusdecItm = oCusdecItm.OrderBy(a => a.Cui_itm_cd).ToList();
            }
            if (chkmodel.Checked)
            {
                oCusdecItm = oCusdecItm.OrderBy(a => a.Cui_model).ToList();
            }
            if (chkhscode.Checked)
            {
                oCusdecItm = oCusdecItm.OrderBy(a => a.Cui_hs_cd).ToList();
            }
            if (chkdesc.Checked)
            {
                oCusdecItm = oCusdecItm.OrderBy(a => a.Cui_itm_desc).ToList();
            }
            if (chkqty.Checked)
            {
                oCusdecItm = oCusdecItm.OrderBy(a => a.Cui_qty).ToList();
            }
            if (chkprice.Checked)
            {
                oCusdecItm = oCusdecItm.OrderBy(a => a.Cui_unit_amt).ToList();
            }
            if (chkcat1.Checked)
            {
                oCusdecItm = oCusdecItm.OrderBy(a => a.ItemCat1).ToList();
            }
            if (chkcat2.Checked)
            {
                oCusdecItm = oCusdecItm.OrderBy(a => a.ItemCat2).ToList();
            }
#region Get tobond no Dulaj 2018/Nov/13
            InventoryRequest _inputInventoryRequest = new InventoryRequest();
            _inputInventoryRequest.Itr_req_no = txtBLno.Text.Trim();
            InventoryRequest intreq =  CHNLSVC.Inventory.GetInventoryRequestDataByReqNo(_inputInventoryRequest);
            if (intreq!=null)
            {
                if (intreq.Itr_sub_tp.Equals("EX") || intreq.Itr_sub_tp.Equals("TO"))
                {
                    if (!(intreq.Itr_job_no.Equals(string.Empty)))
                    {
                        foreach (ImpCusdecItm cusdec in oCusdecItm)
                        {
                            cusdec.Cui_oth_doc_no = intreq.Itr_job_no;
                        }
                    }
                }
            }
#endregion
            dgvItems.DataSource = oCusdecItm;
            dgvItems.DataBind();
        }


        protected void btnAddHS_Click(object sender, EventArgs e)
        {
            try
            {
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR" || _cusdecType == "RE" || _cusdecType=="EX")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                    DataTable result = CHNLSVC.CommonSearch.SearchGetHsCode(para, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "HS";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;

                    GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
                    Label lblCui_line = drSelect.FindControl("lblCui_line") as Label;
                    oSelectItemLine = Convert.ToInt32(lblCui_line.Text.ToString());
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void btnSelectItem_Click(object sender, EventArgs e)
        {
            //Not in use
            try
            {
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
                {
                    if (oCusdecItm != null && oCusdecItm.Count > 0)
                    {
                        GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
                        Label lblCui_line = drSelect.FindControl("lblCui_line") as Label;
                        oSelectItemLine = Convert.ToInt32(lblCui_line.Text.ToString());
                        ImpCusdecItm oSelectItem = oCusdecItm.Find(x => x.Cui_line == oSelectItemLine);
                        txtHSCode.Text = oSelectItem.Cui_hs_cd;
                        txtLineNo.Text = oSelectItemLine.ToString();
                        txtItemCode.Text = oSelectItem.Cui_itm_cd;
                        txtItemDesc.Text = oSelectItem.Cui_itm_desc;
                        txtGrossMassItem.Text = oSelectItem.Cui_gross_mass.ToString();
                        txtNetMassItem.Text = oSelectItem.Cui_net_mass.ToString();
                        //txtPreviousDocument.Text = oSelectItem.Cui_bl_no;
                        txtQuota.Text = oSelectItem.Cui_quota;
                        if (oSelectItem.Cui_preferance != null && oSelectItem.Cui_preferance != "")
                        {
                            if (oSelectItem.Cui_preferance=="0")
                            {
                                txtPreference.Text = "N/A";
                            }
                            else
                            {
                                txtPreference.Text = oSelectItem.Cui_preferance;
                            }
                         
                        }
                       
                        txtUOM1.Text = "NOS";
                        txtQty1.Text = oSelectItem.Cui_qty.ToString();
                        oSelectItem.Cui_itm_price = Math.Round(oSelectItem.Cui_itm_price, 2);
                        txtItemPrice.Text = oSelectItem.Cui_itm_price.ToString();
                        txtItemPrice.Text = string.Format("{0:#,##0.00}", double.Parse(txtItemPrice.Text));
                        txtValueNCY.Text = "0";


                        decimal _itemPrice = 0;
                        decimal _totCost = 0;
                        foreach (ImpCusdecCost _eleCost in oCusdecCost)
                        {
                            if (_eleCost.Cus_ele_cd == "COST") _totCost = _eleCost.Cus_amt;
                            _itemPrice = _itemPrice + (oSelectItem.Cui_qty * oSelectItem.Cui_unit_rt / _totCost) * _eleCost.Cus_amt;
                        }
                        _itemPrice = Math.Round(_itemPrice, 2);
                        txtItemPrice.Text = _itemPrice.ToString();
                        txtItemPrice.Text = string.Format("{0:#,##0.00}", double.Parse(txtItemPrice.Text));
                        decimal _ncy = Convert.ToDecimal(Math.Round(_itemPrice, 2)) * Convert.ToDecimal(txtExRate.Text.ToString());
                        _ncy = Math.Round(_ncy, 2);
                        txtValueNCY.Text = _ncy.ToString();
                        txtValueNCY.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueNCY.Text));

                        decimal _preTot = 0;
                        decimal _actTot = 0;
                        oCusdecHdr.CUH_DT = DateTime.Now.Date;
                        List<HsCode> _hsDuty = CHNLSVC.Financial.GetHSDutyCalculation(true, Convert.ToString(Session["UserCompanyCode"]), oCusdecHdr.CUH_DT.Date,"C",oCusdecHdr.CUH_CONSI_CD,txtProceCode.Text.ToString().Trim().ToUpper(),  _cusdecType, "ALL", oCusdecHdr.CUH_CNTY, 0, _ncy, oSelectItem.Cui_itm_cd, oSelectItem.Cui_hs_cd, oSelectItem.Cui_qty, oSelectItem.Cui_net_mass, oSelectItem.Cui_def_cnty, out _actTot, out _preTot);
                        oCusdecDutyElement = _hsDuty;
                        BindDutyElements();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void dgvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OutwardNo = (gvPending.SelectedRow.FindControl("lbloutwrdnopending") as Label).Text;

            try
            {
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR" || _cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                {
                    if (oCusdecItm != null && oCusdecItm.Count > 0)
                    {
                        CusdecTypes _cusdecTypeInfor = CHNLSVC.Financial.GetCusdecTypeInfor(oMstCom.Mc_anal19).FindAll(y => y.Rcut_tp == _cusdecType)[0];
                        Label lblCui_line = dgvItems.SelectedRow.FindControl("lblCui_line") as Label;
                        oSelectItemLine = Convert.ToInt32(lblCui_line.Text.ToString());
                        ImpCusdecItm oSelectItem = oCusdecItm.Find(x => x.Cui_line == oSelectItemLine);
                        txtHSCode.Text = oSelectItem.Cui_hs_cd;
                        txtLineNo.Text = oSelectItemLine.ToString();
                        txtItemCode.Text = oSelectItem.Cui_itm_cd;
                        txtItemDesc.Text = oSelectItem.Cui_itm_desc;
                        txtGrossMassItem.Text = oSelectItem.Cui_gross_mass.ToString();
                        txtNetMassItem.Text = oSelectItem.Cui_net_mass.ToString();
                     
                        //cat lables
                        txtcat1.Text = oSelectItem.ItemCat1;
                        txtcat2.Text = oSelectItem.ItemCat2;
                        txtitemmodel.Text = oSelectItem.Cui_model;

                        //txtPreviousDocument.Text = oSelectItem.Cui_bl_no;
                        txtQuota.Text = oSelectItem.Cui_quota;
                        if (oSelectItem.Cui_preferance !="" && oSelectItem.Cui_preferance !=null)
                        {
                            txtPreference.Text = oSelectItem.Cui_preferance.ToUpper();
                        }
                       
                        txtUOM1.Text = "NOS";
                        txtQty1.Text = oSelectItem.Cui_qty.ToString();
                        txtItemPrice.Text = oSelectItem.Cui_itm_price.ToString();
                        txtItemPrice.Text = string.Format("{0:#,##0.00}", double.Parse(txtItemPrice.Text));
                        txtValueNCY.Text = "0";
                        txtCtryOriginItem.ReadOnly = false; 
                        txtCtryOriginItem.Text = oSelectItem.Cui_def_cnty;
                        txtCtryOriginItem.ReadOnly = true;
                        decimal _itemPrice = 0;
                        decimal _totCost = 0;
                        oCusdecCost = oCusdecCost.OrderBy(x => x.Cus_line).ToList();
                        foreach (ImpCusdecCost _eleCost in oCusdecCost)
                        {
                            if (_eleCost.Cus_ele_cd == "COST") _totCost = _eleCost.Cus_amt;
                            _itemPrice = _itemPrice + (oSelectItem.Cui_qty * oSelectItem.Cui_unit_rt / _totCost) * _eleCost.Cus_amt;
                        }
                        _itemPrice = Math.Round(_itemPrice, 2);
                        txtItemPrice.Text = _itemPrice.ToString();
                        txtItemPrice.Text = string.Format("{0:#,##0.00}", double.Parse(txtItemPrice.Text));
                        decimal _ncy = Convert.ToDecimal(Math.Round(_itemPrice, 2)) * Convert.ToDecimal(txtExRate.Text.ToString());
                        _ncy = Math.Round(_ncy, 2);
                        txtValueNCY.Text = _ncy.ToString();
                        txtValueNCY.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueNCY.Text));

                        if (chkHSHistory.Checked == false)
                        {
                            pnlDutyElements.Visible = true;
                            pnlHSHistory.Visible = false;
                            pnlDutyElementsTotal.Visible = false;
                            chkAllTax.Checked = false;
                            chkhstax.Checked = false;

                            decimal _preTot = 0;
                            decimal _actTot = 0;
                            oCusdecHdr.CUH_DT = DateTime.Now.Date;
                            List<HsCode> _hsDuty = new List<HsCode>();
                            if (chkCurrentDuty.Checked == true)
                            {
                                _hsDuty = CHNLSVC.Financial.GetHSDutyCalculation(true, Convert.ToString(Session["UserCompanyCode"]), oCusdecHdr.CUH_DT.Date,"C",oCusdecHdr.CUH_CONSI_CD,txtProceCode.Text.ToString().Trim().ToUpper(), _cusdecType, "ALL", oCusdecHdr.CUH_CNTY, _cusdecTypeInfor.Rcuit_duty_mp, _ncy, oSelectItem.Cui_itm_cd, oSelectItem.Cui_hs_cd, oSelectItem.Cui_qty, oSelectItem.Cui_net_mass, oSelectItem.Cui_def_cnty, out _actTot, out _preTot);
                            }
                            else
                            {
                                List<ImpCusdecItmCost> _tempItemCost = oCusdecItmCost.FindAll(x => x.Cuic_itm_line == oSelectItemLine && x.Cuic_anal_1 == "DUTY");
                                foreach (ImpCusdecItmCost itemCostOne in _tempItemCost)
                                {
                                    HsCode oHsCode = new HsCode();
                                    oHsCode.Mhc_cost_ele = itemCostOne.Cuic_ele_cd;
                                    oHsCode.Mhc_mp = itemCostOne.Cuic_ele_mp;
                                    oHsCode.Tax_rate = itemCostOne.Cuic_ele_rt;
                                    oHsCode.Tax_base = itemCostOne.Cuic_ele_base;
                                    oHsCode.Tax_amount = itemCostOne.Cuic_ele_amt;
                                    _hsDuty.Add(oHsCode);
                                }
                            }
                            oCusdecDutyElement = _hsDuty;
                            BindDutyElements();
                        }
                        else
                        {
                            pnlDutyElements.Visible = false;
                            pnlHSHistory.Visible = true;
                            dgvHSHistory.DataSource = new int[] { };
                            dgvHSHistory.DataBind();
                            DataTable _dtHS = CHNLSVC.CommonSearch.GetHSHistory(Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19.ToString(), _cusdecType, txtItemCode.Text);
                            dgvHSHistory.DataSource = _dtHS;
                            dgvHSHistory.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void dgvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvItems, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message.ToString() + "')", true);
            }
        }

        protected void dgvItems_SortClick(object sender, GridViewSortEventArgs e)
        {
            switch (e.SortExpression)
            {
                case "Cui_line":
                    {
                        dgvItems.DataSource = new int[] { };
                        dgvItems.DataBind();
                        dgvItems.DataSource = oCusdecItm.OrderBy(x => x.Cui_line).ToList();
                        dgvItems.DataBind();
                        break;
                    }
                case "Cui_itm_cd":
                    {
                        dgvItems.DataSource = new int[] { };
                        dgvItems.DataBind();
                        dgvItems.DataSource = oCusdecItm.OrderBy(x => x.Cui_itm_cd).ToList();
                        dgvItems.DataBind();
                        break;
                    }
                case "Cui_hs_cd":
                    {
                        dgvItems.DataSource = new int[] { };
                        dgvItems.DataBind();
                        dgvItems.DataSource = oCusdecItm.OrderBy(x => x.Cui_hs_cd).ToList();
                        dgvItems.DataBind();
                        break;
                    }
                case "Cui_model":
                    {
                        dgvItems.DataSource = new int[] { };
                        dgvItems.DataBind();
                        dgvItems.DataSource = oCusdecItm.OrderBy(x => x.Cui_model).ToList();
                        dgvItems.DataBind();
                        break;
                    }
                case "Cui_itm_desc":
                    {
                        dgvItems.DataSource = new int[] { };
                        dgvItems.DataBind();
                        dgvItems.DataSource = oCusdecItm.OrderBy(x => x.Cui_itm_desc).ToList();
                        dgvItems.DataBind();
                        break;
                    }
            }
        }

        #endregion

        #region Containers Data Grid
        private void BindDutyElements()
        {
            dgvDutyElements.DataSource = new int[] { };
            dgvDutyElements.DataBind();
            dgvDutyElements.DataSource = oCusdecDutyElement;
            dgvDutyElements.DataBind();

            txtElementTotal.Text = string.Format("{0:#,##0}", double.Parse(Convert.ToString(oCusdecDutyElement.Sum(x => x.Tax_amount))));
        }

        private void BindDutyElementsTotal()
        {
            dgvDutyElementsTot.DataSource = new int[] { };
            dgvDutyElementsTot.DataBind();
            dgvDutyElementsTot.DataSource = oCusdecDutyElementTot;
            dgvDutyElementsTot.DataBind();

            txtElementTotal.Text = string.Format("{0:#,##0}", double.Parse(Convert.ToString(oCusdecDutyElementTot.Sum(x => x.Tax_amount))));
        }
        #endregion

        private void LoadBLDetails()
        {
            string _msg = string.Empty;
            ImpCusdecHdr oCusdecHdr0 = new ImpCusdecHdr();
            List<ImpCusdecItm> oCusdecItm0 = new List<ImpCusdecItm>();
            List<ImpCusdecCost> oCusdecCost0 = new List<ImpCusdecCost>();
            List<ImportsBLContainer> oCusdecContainer0 = new List<ImportsBLContainer>();

            int _custStatus = CHNLSVC.Financial.GetBLData(Convert.ToString(Session["UserCompanyCode"]), txtBLno.Text.Trim(), out oCusdecHdr0, out oCusdecItm0, out oCusdecCost0, out oCusdecContainer0, out _msg);
            if (_custStatus == 1)
            {
               // oCusdecCost0 = oCusdecCost0.Take(1).ToList();
                oCusdecHdr = oCusdecHdr0;
                oCusdecItm = oCusdecItm0;
                oCusdecCost = oCusdecCost0.OrderBy(x => x.Cus_line).ToList();
                oCusdecContainer = oCusdecContainer0;
                
                //btnSave.Visible = false;
                //hdfHEaderStatus.Value = oCusdecHdr.Ib_stus;
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "TO") oCusdecHdr.CUH_TP = "TO";
                else if (_cusdecType == "LR") oCusdecHdr.CUH_TP = "LR";
                else if (_cusdecType == "MV") oCusdecHdr.CUH_TP = "MV";
                else if (_cusdecType == "AIR") oCusdecHdr.CUH_TP = "AIR";

                string _cusdecSubType = Request.QueryString["CUSTSTYPE"];
                oCusdecHdr.CUH_SUB_TP = Request.QueryString["CUSTSTYPE"].ToString();

                txtDocDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                oCusdecHdr.CUH_DT = DateTime.Now.Date;
                oCusdecHdr.CUH_OTH_NO = txtBLno.Text.Trim().ToUpper().ToString();
                oCusdecHdr.CUH_SUN_REQ_NO = txtBLno.Text.Trim().ToUpper().ToString();
                txtDocRecDate.Text = oCusdecHdr.CUH_DOC_REC_DT.Date.ToString("dd/MMM/yyyy");
                txtCusdecEntryNo.Text = oCusdecHdr.CUH_CUSDEC_ENTRY_NO;

                txtConsigneeTin.Text = oCusdecHdr.CUH_CONSI_TIN;
                txtConsigneeCode.Text = oCusdecHdr.CUH_CONSI_CD;
                txtConsigneeName.Text = oCusdecHdr.CUH_CONSI_NAME;
                txtConsigneeAddress.Text = oCusdecHdr.CUH_CONSI_ADDR;

                txtExporterTin.Text = oCusdecHdr.CUH_SUPP_TIN;
                txtExporterCode.Text = oCusdecHdr.CUH_SUPP_CD;
                txtExporterName.Text = oCusdecHdr.CUH_SUPP_NAME;
                txtExporterAddress.Text = oCusdecHdr.CUH_SUPP_ADDR;

                txtDeclarantTin.Text = oCusdecHdr.CUH_DECL_TIN;
                txtDeclarantCode.Text = oCusdecHdr.CUH_DECL_CD;
                txtDeclarantName.Text = oCusdecHdr.CUH_DECL_NAME;
                txtDeclarantAddress.Text = oCusdecHdr.CUH_DECL_ADDR;

                //txtPg1.Text = oCusdecHdr.CUH_PAGE_1;
                //txtPg2.Text = oCusdecHdr.CUH_PAGE_2;
                txtLists.Text = "0";
                txtItems.Text = oCusdecItm.Count.ToString();
                //txtTotalPackages.Text = oCusdecItm.Sum(t => t.Cui_qty).ToString();
                txtTotalPackages.Text = oCusdecHdr.CUH_TOT_PKG;
                ddlTotalPkgUOM.Text = oCusdecHdr.CUH_TOT_PKG_UNIT;
                txtBankCode.Text = oCusdecHdr.CUH_BANK_CD;
                txtBankName.Text = oCusdecHdr.CUH_BANK_NAME;
                txtFinRefNo.Text = oCusdecHdr.CUH_BANK_REF_CD;
                txtBranch.Text = oCusdecHdr.CUH_BANK_BRANCH;
                txtTermsOfPayment.Text = oCusdecHdr.CUH_TERMS_OF_PAYMENT;
                txtTermsOfPaymentCustom.Text = oCusdecHdr.CUH_CUSTOM_LC_TP;

                //txtWarehouseID.Text = oCusdecHdr.CUH_WH_AND_PERIOD; //"7200725"; 

                txtVessleNo.Text = oCusdecHdr.CUH_VESSEL;
                txtPlaceOfLoading.Text = oCusdecHdr.CUH_PLACE_OF_LOADING;
                txtVoyageNo.Text = oCusdecHdr.CUH_VOYAGE;
                txtVoyageDate.Text = oCusdecHdr.CUH_VOYAGE_DT.Date.ToString("dd/MMM/yyyy");
                txtFCL.Text = oCusdecHdr.CUH_FCL;
                txtCrtyExport.Text = oCusdecHdr.CUH_EXP_CNTY_NAME;
                txtCrtyExportCode.Text = oCusdecHdr.CUH_CNTY_OF_EXPORT;
                txtCrtyDesti.Text = oCusdecHdr.CUH_DESTI_CNTY_NAME;
                txtCrtyDestiCode.Text = oCusdecHdr.CUH_CNTY_OF_DESTINATION;
                txtCrtyOrigin.Text = oCusdecHdr.CUH_ORIGIN_CNTY_NAME;
                txtCrtyOriginCode.Text = oCusdecHdr.CUH_CNTY_OF_ORIGIN;
                txtCtryOriginItem.Text = oCusdecHdr.CUH_CNTY_OF_ORIGIN;
                txtDelTerms.Text = oCusdecHdr.CUH_DELIVERY_TERMS;
                txtCurrency.Text = oCusdecHdr.CUH_CUR_CD;
                oCusdecHdr.CUH_TOT_AMT = Math.Round(oCusdecHdr.CUH_TOT_AMT, 2);
                txtInvoiceAmt.Text = oCusdecHdr.CUH_TOT_AMT.ToString();
                txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                txtExRate.Text = oCusdecHdr.CUH_EX_RT.ToString();
                txtCurrencyOwn.Text = oCusdecHdr.OwnCurrencyCode;
                txtValueDetails.Text = (oCusdecCost.Sum(t => t.Cus_amt) * oCusdecHdr.CUH_EX_RT).ToString();
                txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));

                txtFinSettlement.Text = txtConsigneeName.Text;
                txtLastConsi.Text = txtCrtyExportCode.Text;
                txtLastConsiName.Text = txtCrtyExport.Text;
                txtTradingCountry.Text = txtCrtyExportCode.Text;
                txtTradingCountryName.Text = txtCrtyExport.Text;
                txtComChgAmount.Text = oCusdecCommon.Rcsc_comchg_0.ToString();
                txtPreviousDocument.Text = oCusdecHdr.CUH_BL_NO.ToString();
                txtReference.Text = oCusdecHdr.CUH_REF_NO;
                txtFileNo.Text = oCusdecHdr.CUH_FILE_NO;

                if (txtFCL.Text == "FCL")
                {
                    if (oCusdecContainer != null)
                    {
                        if (oCusdecContainer.Count == 1) txtComChgAmount.Text = oCusdecCommon.Rcsc_comchg_1.ToString();
                        if (oCusdecContainer.Count > 1) txtComChgAmount.Text = (oCusdecCommon.Rcsc_comchg_1 + Convert.ToDecimal(Convert.ToDecimal(oCusdecContainer.Count - 1) * oCusdecCommon.Rcsc_comchg_up)).ToString();
                    }
                }
                oCusdecItm = oCusdecItm.OrderBy(x => x.Cui_line).ToList();

                BindCostItems();
                BindContainers();
                BindItems();
                chkCurrentDuty.Checked = true;

                btnSave.Text = "Save";
            }
            else
            {
                DisplayMessage(_msg, 1);
            }
        }

        private void LoadAccountNumber(string customerCode,string companyCode)
        {
            string accountNo = CHNLSVC.Sales.GetAccountNo(customerCode, companyCode);
                if(!(accountNo.Equals(string.Empty)))
                {
                    txtAcNo.Text = accountNo;
                }
            else
                {
                    txtAcNo.Text=oCusdecCommon.Rcsc_acc_no;
                }
            
        }
        private void LoadCusdecReqDetails()
        {
            string _msg = string.Empty;
            ImpCusdecHdr oCusdecHdr0 = new ImpCusdecHdr();
            List<ImpCusdecItm> oCusdecItm0 = new List<ImpCusdecItm>();
            List<ImpCusdecItmCost> oCusdecItmCost0 = new List<ImpCusdecItmCost>();
            List<ImpCusdecCost> oCusdecCost0 = new List<ImpCusdecCost>();
            List<ImportsBLContainer> oCusdecContainer0 = new List<ImportsBLContainer>();

            // + Request.QueryString["CUSTTYPE"].ToString()
            int _custStatus = CHNLSVC.Financial.GetCusdecReqData(Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19.ToString(), Request.QueryString["CUSTTYPE"].ToString(), txtBLno.Text.Trim(), out oCusdecHdr0, out oCusdecItm0, out oCusdecItmCost0, out oCusdecCost0, out oCusdecContainer0, out _msg, true);
            if (_custStatus == 1)
            {
                oCusdecHdr = oCusdecHdr0;
                oCusdecItm = oCusdecItm0;
                oCusdecItmCost = oCusdecItmCost0;
                oCusdecCost = oCusdecCost0.OrderBy(x => x.Cus_line).ToList();
                oCusdecContainer = oCusdecContainer0;

                chkCurrentDuty.Checked = false;
                chkCurrentDuty.Visible = true;

                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "EX") oCusdecHdr.CUH_TP = "EX";
                if (_cusdecType == "RE") oCusdecHdr.CUH_TP = "RE";
                if (_cusdecType == "EXP") oCusdecHdr.CUH_TP = "EXP";
                if (oCusdecHdr.CUH_TP == "RE")
                {
                    txtProceCode.Text = oCusdecHdr.CUH_PROC_CD.ToString();
                    txtProceCode1.Text = oCusdecHdr.CUH_PROCE_CD_1.ToString();
                    txtProceCode2.Text = oCusdecHdr.CUH_PROCE_CD_2.ToString();
                }
                if (oCusdecHdr.CUH_TP == "EX")
                {
                    txtWarehouseID.Text = "7200725";
                    txtProceCode1.ReadOnly = false;
                    txtProceCode2.ReadOnly = false;
                    txtProceCode1.Text = oCusdecHdr.CUH_PROCE_CD_1.ToString();
                    txtProceCode2.Text = oCusdecHdr.CUH_PROCE_CD_2.ToString();
                    if(string.IsNullOrEmpty(oCusdecHdr.CUH_PROCE_CD_1.ToString()) )
                    {
                        txtProceCode1.Text = "4172";
                        txtProceCode2.Text = "000";
                    }
                }

                oCusdecHdr.CUH_DT = DateTime.Now.Date;
                txtDocDate.Text = oCusdecHdr.CUH_DT.Date.ToString("dd/MMM/yyyy");
                txtBLno.Text = oCusdecHdr.CUH_OTH_NO;
                txtPreviousDocument.Text = oCusdecHdr.CUH_CUSDEC_ENTRY_NO.ToString() + " OF " + oCusdecHdr.CUH_CUSDEC_ENTRY_DT.ToString("dd/MM/yyyy");
                txtDocRecDate.Text = oCusdecHdr.CUH_DOC_REC_DT.Date.ToString("dd/MMM/yyyy");
                txtCusdecEntryNo.Text = oCusdecHdr.CUH_CUSDEC_ENTRY_NO;
                txtConsigneeTin.Text = oCusdecHdr.CUH_CONSI_TIN;
                txtConsigneeCode.Text = oCusdecHdr.CUH_CONSI_CD;
                if (oCusdecHdr.CUH_TP == "RE")
                {
                    MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileNew(
                            new MasterBusinessEntity() { Mbe_com = Session["UserCompanyCode"].ToString(), Mbe_cd = oCusdecHdr.CUH_CONSI_CD, Mbe_tp = "C" }
                            ).FirstOrDefault();
                    if (_mstBusEntity != null)
                    {
                        txtConsigneeTin.Text = _mstBusEntity.Mbe_oth_id_no;
                    }
                }

                txtConsigneeName.Text = oCusdecHdr.CUH_CONSI_NAME;
                txtConsigneeAddress.Text = oCusdecHdr.CUH_CONSI_ADDR;

                txtExporterTin.Text = oCusdecHdr.CUH_SUPP_TIN;
                txtExporterCode.Text = oCusdecHdr.CUH_SUPP_CD;
                txtExporterName.Text = oCusdecHdr.CUH_SUPP_NAME;
                txtExporterAddress.Text = oCusdecHdr.CUH_SUPP_ADDR;

                txtDeclarantTin.Text = oCusdecHdr.CUH_DECL_TIN;
                txtDeclarantCode.Text = oCusdecHdr.CUH_DECL_CD;
                txtDeclarantName.Text = oCusdecHdr.CUH_DECL_NAME;
                txtDeclarantAddress.Text = oCusdecHdr.CUH_DECL_ADDR;

                //txtPg1.Text = oCusdecHdr.CUH_PAGE_1;
                //txtPg2.Text = oCusdecHdr.CUH_PAGE_2;
                txtLists.Text = "0";
                txtItems.Text = oCusdecHdr.CUH_ITEMS_QTY.ToString();
                txtTotalPackages.Text = oCusdecHdr.CUH_TOT_PKG;
                ddlTotalPkgUOM.Text = oCusdecHdr.CUH_TOT_PKG_UNIT;
                txtWarehouseID.Text = oCusdecHdr.CUH_WH_AND_PERIOD; //"7200725"; 
                txtVessleNo.Text = oCusdecHdr.CUH_VESSEL;
                txtPlaceOfLoading.Text = oCusdecHdr.CUH_PLACE_OF_LOADING;
                txtVoyageNo.Text = oCusdecHdr.CUH_VOYAGE;
                txtVoyageDate.Text = oCusdecHdr.CUH_VOYAGE_DT.Date.ToString("dd/MMM/yyyy");
                txtCrtyExport.Text = oCusdecHdr.CUH_EXP_CNTY_NAME;
                txtCrtyExportCode.Text = oCusdecHdr.CUH_CNTY_OF_EXPORT;
                txtCrtyDesti.Text = oCusdecHdr.CUH_DESTI_CNTY_NAME;
                txtCrtyDestiCode.Text = oCusdecHdr.CUH_CNTY_OF_DESTINATION;
                txtCrtyOrigin.Text = oCusdecHdr.CUH_ORIGIN_CNTY_NAME;
                txtCrtyOriginCode.Text = oCusdecHdr.CUH_CNTY_OF_ORIGIN;
                txtTermsOfPayment.Text = oCusdecHdr.CUH_DELIVERY_TERMS;
                txtDelTerms.Text = oCusdecHdr.CUH_DELIVERY_TERMS;
                txtCurrency.Text = oCusdecHdr.CUH_CUR_CD;
                oCusdecHdr.CUH_TOT_AMT = Math.Round(oCusdecHdr.CUH_TOT_AMT, 2);
                txtInvoiceAmt.Text = oCusdecHdr.CUH_TOT_AMT.ToString();
                txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                txtExRate.Text = oCusdecHdr.CUH_EX_RT.ToString();
                txtCurrencyOwn.Text = oMstCom.Mc_anal19.ToString();
                txtValueDetails.Text = (oCusdecCost.Sum(t => t.Cus_amt) * oCusdecHdr.CUH_EX_RT).ToString();
                txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));
                //oCusdecHdr.CUH_COM_CHG = Math.Round(oCusdecHdr.CUH_COM_CHG, 2);
                oCusdecHdr.CUH_COM_CHG = oCusdecCommon.Rcsc_comchg_0;
                txtComChgAmount.Text = oCusdecHdr.CUH_COM_CHG.ToString("#.##");
                txtFileNo.Text = oCusdecHdr.CUH_FILE_NO.ToString();
                txtFinRefNo.Text = oCusdecHdr.CUH_BANK_REF_CD.ToString();
                txtBankCode.Text = oCusdecHdr.CUH_BANK_CD.ToString();
                txtBankName.Text = oCusdecHdr.CUH_BANK_NAME.ToString();
                txtBranch.Text = oCusdecHdr.CUH_BANK_BRANCH.ToString();
                txtTermsOfPayment.Text = oCusdecHdr.CUH_TERMS_OF_PAYMENT;
                txtTermsOfPaymentCustom.Text = oCusdecHdr.CUH_CUSTOM_LC_TP;
                txtTotGrossMass.Text = oCusdecHdr.CUH_TOT_GROSS_MASS.ToString();
                txtTotNetMass.Text = oCusdecHdr.CUH_TOT_NET_MASS.ToString();
                txtMainHS.Text = oCusdecHdr.CUH_MAIN_HS.ToString();
                if (_cusdecType != "EXP")
                {
                    ddlOfficeOfEntry.Items.Add(oCusdecHdr.CUH_OFFICE_OF_ENTRY.ToString());
                    ddlOfficeOfEntry.SelectedItem.Text = oCusdecHdr.CUH_OFFICE_OF_ENTRY.ToString();
                    ddlLocOfGoods.Items.Add(oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString());
                    ddlLocOfGoods.SelectedItem.Text = oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString();
                }
                txtDesc.Text = oCusdecHdr.CUH_RMK;
                txtReference.Text = oCusdecHdr.CUH_REF_NO;
                txtContainerFCL.Text = oCusdecHdr.CUH_CONTAINER_FCL;
                txtFinSettlement.Text = txtConsigneeName.Text;
                txtFCL.Text = oCusdecHdr.CUH_FCL;
                txtLastConsi.Text = txtCrtyExportCode.Text;
                txtLastConsiName.Text = txtCrtyExport.Text;
                txtTradingCountry.Text = oCusdecHdr.CUH_CNTY_OF_EXPORT.ToString();
                txtTradingCountryName.Text = oCusdecHdr.CUH_EXP_CNTY_NAME.ToString();
                txtMarksAndNos.Text = oCusdecHdr.CUH_MARKS_AND_NO;
                txtNatuOfTranst.Text = oCusdecHdr.CUH_NATURE_OF_TRANCE;
                txtPreviousDocument.Text = oCusdecHdr.CUH_BL_NO;
                //txtTermsOfPaymentCustom.Text = "";
                //txtMarksAndNos.Text = oCusdecHdr.CUH_MARKS_AND_NO.ToString();
                txtMarksNumbers.Text = oCusdecHdr.CUH_MARKS_AND_NO.ToString();
               // txtPreference.Text = oCusdecHdr.CUH_PREFERENCE.ToString();
                //txtPreviousDocument.Text = txtCusdecEntryNo.Text+ " of " + txtDocRecDate.Text;
                BindCostItems();
                BindContainers();
                BindItems();
                LoadAccountNumber(oCusdecHdr.CUH_CONSI_CD, Convert.ToString(Session["UserCompanyCode"]));
                chkCurrentDuty.Checked = true;
                btnSave.Text = "Save";
            }
            else if (_custStatus == 0)
            {
                DisplayMessage(_msg, 2);
                txtBLno.Text = "";
                txtBLno.Focus();
                return;
            }
            else
            {
                DisplayMessage(_msg, 4);
                txtBLno.Text = "";
                txtBLno.Focus();
                return;
            }
        }

        private void LoadCusdecDetails()
        {
            string _msg = string.Empty;
            ImpCusdecHdr oCusdecHdr0 = new ImpCusdecHdr();
            List<ImpCusdecItm> oCusdecItm0 = new List<ImpCusdecItm>();
            List<ImpCusdecItmCost> oCusdecItmCost0 = new List<ImpCusdecItmCost>();
            List<ImpCusdecCost> oCusdecCost0 = new List<ImpCusdecCost>();
            List<ImportsBLContainer> oCusdecContainer0 = new List<ImportsBLContainer>();

            // + Request.QueryString["CUSTTYPE"].ToString()
            int _custStatus = CHNLSVC.Financial.GetCusdecData(Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19.ToString(), Request.QueryString["CUSTTYPE"].ToString(), txtRefno.Text.Trim(), out oCusdecHdr0, out oCusdecItm0, out oCusdecItmCost0, out oCusdecCost0, out oCusdecContainer0, out _msg);
            if (oCusdecHdr0 != null)
            {
                if (oCusdecHdr0.CUH_STUS == "C")
                {
                    _custStatus = 0;
                    _msg = "Entry Number is invalid";
                }
            }
            if (_custStatus == 1)
            {
                oCusdecHdr = oCusdecHdr0;
                oCusdecItm = oCusdecItm0;
                oCusdecItmCost = oCusdecItmCost0;
                oCusdecCost = oCusdecCost0.OrderBy(x => x.Cus_line).ToList();

                oCusdecContainer = oCusdecContainer0;
                
                chkCurrentDuty.Checked = false;
                chkCurrentDuty.Visible = true;

                txtProceCode.Text = oCusdecHdr.CUH_PROC_CD.ToString();
                txtProceCode1.Text = oCusdecHdr.CUH_PROCE_CD_1.ToString();
                txtProceCode2.Text = oCusdecHdr.CUH_PROCE_CD_2.ToString() ;
                txtMarksNumbers.Text = oCusdecHdr.CUH_MARKS_AND_NO.ToString();

                txtDocDate.Text = oCusdecHdr.CUH_DT.Date.ToString("dd/MMM/yyyy");
                txtBLno.Text = oCusdecHdr.CUH_OTH_NO;
                txtDocRecDate.Text = oCusdecHdr.CUH_DOC_REC_DT.Date.ToString("dd/MMM/yyyy");
                txtCusdecEntryNo.Text = oCusdecHdr.CUH_CUSDEC_ENTRY_NO;
                txtConsigneeTin.Text = oCusdecHdr.CUH_CONSI_TIN;
                txtConsigneeCode.Text = oCusdecHdr.CUH_CONSI_CD;
                txtConsigneeName.Text = oCusdecHdr.CUH_CONSI_NAME;
                txtConsigneeAddress.Text = oCusdecHdr.CUH_CONSI_ADDR;

                txtExporterTin.Text = oCusdecHdr.CUH_SUPP_TIN;
                txtExporterCode.Text = oCusdecHdr.CUH_SUPP_CD;
                txtExporterName.Text = oCusdecHdr.CUH_SUPP_NAME;
                txtExporterAddress.Text = oCusdecHdr.CUH_SUPP_ADDR;

                txtDeclarantTin.Text = oCusdecHdr.CUH_DECL_TIN;
                txtDeclarantCode.Text = oCusdecHdr.CUH_DECL_CD;
                txtDeclarantName.Text = oCusdecHdr.CUH_DECL_NAME;
                txtDeclarantAddress.Text = oCusdecHdr.CUH_DECL_ADDR;

                //txtPg1.Text = oCusdecHdr.CUH_PAGE_1;
                //txtPg2.Text = oCusdecHdr.CUH_PAGE_2;
                txtPreviousDocument.Text = oCusdecHdr.CUH_PPC_NO;
                txtLists.Text = "0";
                txtItems.Text = oCusdecHdr.CUH_ITEMS_QTY.ToString();
                txtTotalPackages.Text = oCusdecHdr.CUH_TOT_PKG.ToString();
                ddlTotalPkgUOM.Text = oCusdecHdr.CUH_TOT_PKG_UNIT.ToString();
                txtWarehouseID.Text = oCusdecHdr.CUH_WH_AND_PERIOD.ToString(); //"7200725"; 
                txtVessleNo.Text = oCusdecHdr.CUH_VESSEL.ToString();
                txtPlaceOfLoading.Text = oCusdecHdr.CUH_PLACE_OF_LOADING;
                txtVoyageNo.Text = oCusdecHdr.CUH_VOYAGE.ToString();
                txtVoyageDate.Text = oCusdecHdr.CUH_VOYAGE_DT.Date.ToString("dd/MMM/yyyy");
                txtCrtyExport.Text = oCusdecHdr.CUH_EXP_CNTY_NAME.ToString();
                txtCrtyExportCode.Text = oCusdecHdr.CUH_CNTY_OF_EXPORT.ToString();
                txtCrtyDesti.Text = oCusdecHdr.CUH_DESTI_CNTY_NAME.ToString();
                txtCrtyDestiCode.Text = oCusdecHdr.CUH_CNTY_OF_DESTINATION.ToString();
                txtCrtyOrigin.Text = oCusdecHdr.CUH_ORIGIN_CNTY_NAME.ToString();
                txtCrtyOriginCode.Text = oCusdecHdr.CUH_CNTY_OF_ORIGIN.ToString();
                txtTermsOfPayment.Text = oCusdecHdr.CUH_TERMS_OF_PAYMENT.ToString();
                txtTermsOfPaymentCustom.Text = oCusdecHdr.CUH_CUSTOM_LC_TP.ToString();
                txtDelTerms.Text = oCusdecHdr.CUH_DELIVERY_TERMS.ToString();
                txtCurrency.Text = oCusdecHdr.CUH_CUR_CD.ToString();
                oCusdecHdr.CUH_TOT_AMT = Math.Round(oCusdecHdr.CUH_TOT_AMT, 2);
                txtInvoiceAmt.Text = oCusdecHdr.CUH_TOT_AMT.ToString();
                txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                txtExRate.Text = oCusdecHdr.CUH_EX_RT.ToString();
                txtCurrencyOwn.Text = oMstCom.Mc_anal19.ToString();
                txtValueDetails.Text = (oCusdecCost.Sum(t => t.Cus_amt) * oCusdecHdr.CUH_EX_RT).ToString();
                txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));
                txtComChgAmount.Text = oCusdecHdr.CUH_COM_CHG.ToString("#.##");
                txtFileNo.Text = oCusdecHdr.CUH_FILE_NO.ToString();
                txtFinRefNo.Text = oCusdecHdr.CUH_BANK_REF_CD.ToString();
                txtBankCode.Text = oCusdecHdr.CUH_BANK_CD.ToString();
                txtBankName.Text = oCusdecHdr.CUH_BANK_NAME.ToString();
                txtBranch.Text = oCusdecHdr.CUH_BANK_BRANCH.ToString();
                txtTotGrossMass.Text = oCusdecHdr.CUH_TOT_GROSS_MASS.ToString();
                txtTotNetMass.Text = oCusdecHdr.CUH_TOT_NET_MASS.ToString();
                txtMainHS.Text = oCusdecHdr.CUH_MAIN_HS.ToString();                           

                //ddlOfficeOfEntry.Items.Clear();
                //ddlLocOfGoods.Items.Clear();
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                {
                    ddlOfficeOfEntry.Items.Clear();
                    ddlLocOfGoods.Items.Clear();
                    ddlOfficeOfEntry.Items.Add(oCusdecHdr.CUH_OFFICE_OF_ENTRY.ToString());
                    ddlLocOfGoods.Items.Add(oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString());
                }
                ddlOfficeOfEntry.SelectedItem.Text = oCusdecHdr.CUH_OFFICE_OF_ENTRY.ToString();
                ddlLocOfGoods.SelectedItem.Text = oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString();
                txtFinSettlement.Text = oCusdecHdr.CUH_FIN_SETTLE_TEXT;
                txtDesc.Text = oCusdecHdr.CUH_RMK;
                txtReference.Text = oCusdecHdr.CUH_REF_NO;
                txtContainerFCL.Text = oCusdecHdr.CUH_CONTAINER_FCL;
                txtFCL.Text = oCusdecHdr.CUH_FCL;
                txtLastConsi.Text = txtCrtyExportCode.Text;
                txtLastConsiName.Text = txtCrtyExport.Text;
                txtTradingCountry.Text = txtCrtyExportCode.Text;
                txtTradingCountry.Text = oCusdecHdr.CUH_CNTY_OF_EXPORT.ToString();
                txtTradingCountryName.Text = oCusdecHdr.CUH_EXP_CNTY_NAME.ToString();
                txtMarksAndNos.Text = oCusdecHdr.CUH_MARKS_AND_NO;
                txtNatuOfTranst.Text = oCusdecHdr.CUH_NATURE_OF_TRANCE;

               // LoadAccountNumber(oCusdecHdr.CUH_CONSI_CD);
                txtAcNo.Text = oCusdecHdr.CUH_ACC_NO;
                //txtPreviousDocument.Text = oCusdecHdr.CUH_BL_NO;
                //txtPreference.Text = oCusdecHdr.CUH_PREFERENCE.ToString();
                if (oCusdecHdr.CUH_IGNORE == 1)
                {
                    chkIgnoreCountry.Checked = true;
                }

                BindCostItems();
                BindContainers();
                BindItems();
                if (_cusdecType == "TO")
                { setStatus(oCusdecHdr.CUH_STUS); }                
                btnSave.Text = "Update";
                btnSave.CssClass = "floatRight";
            }
            else if (_custStatus == 0)
            {
                DisplayMessage(_msg, 2);
                txtBLno.Text = "";
                txtRefno.Text = "";
                txtBLno.Focus();
                return;
            }
            else
            {
                DisplayMessage(_msg, 4);
                txtBLno.Text = "";
                txtBLno.Focus();
                return;
            }

        }
        private void setStatus(string stus)
        {
            if (stus.Equals("P"))
            {
                Label1status.Text = Server.HtmlEncode("Status :Pending");
                Label1status.ForeColor = Color.Red;

            }
            if (stus.Equals("A"))
            {
                Label1status.Text = "Status :Approved";
                Label1status.ForeColor = Color.Red;
            }
            if (stus.Equals(""))
            {
                Label1status.Text = "";
                Label1status.ForeColor = Color.Red;
            } 
        }
        public decimal Cal_Item_Cost_Apportion(decimal _unitRate, decimal _qty, decimal _totElementValue, decimal _elementValue)
        {
            return (_qty * _unitRate / _totElementValue) * _elementValue;
        }

        #endregion

        protected void btnUpdateHS_Click(object sender, EventArgs e)
        {
            string _cusdecType = Request.QueryString["CUSTTYPE"];
            if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
            {

                if (oCusdecItm != null)
                {
                    if (oCusdecItm.Count > 0)
                    {
                        decimal _fob_val = 0;
                        foreach (ImpCusdecCost _eleCost in oCusdecCost)
                        {
                            if (_eleCost.Cus_ele_cd == "COST") _fob_val = _eleCost.Cus_amt;
                        }
                       // oCusdecItm = CHNLSVC.Financial.UpdateHSUsingHistory(oCusdecItm, Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19, _cusdecType, txtBLno.Text.ToString(), _fob_val, Convert.ToDecimal(txtTotNetMass.Text), Convert.ToDecimal(txtTotGrossMass.Text), txtRefno.Text.ToString());
                        oCusdecItm = CHNLSVC.Financial.UpdateHSHistoryOnly(oCusdecItm, Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19, _cusdecType, txtBLno.Text.ToString(), _fob_val, Convert.ToDecimal(0), Convert.ToDecimal(0), txtRefno.Text.ToString());
                    }
                }

                BindItems();

                DisplayMessage("Items Updated!", 3);
            }
        }

        protected void txtExRate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExRate.Text))
            {
                if (!isdecimal(txtExRate.Text))
                {
                    DisplayMessage("Please enter valid amount", 2);
                    return;
                }
                else
                {
                    Decimal oTotalValue = oCusdecCost.Where(z => z.Cus_act == 1).Sum(x => x.Cus_amt);
                    txtInvoiceAmt.Text = oTotalValue.ToString("N2");
                    oCusdecHdr.CUH_TOT_AMT = Math.Round(oTotalValue, 2);
                    txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                    txtValueDetails.Text = Convert.ToString(oTotalValue * Convert.ToDecimal(txtExRate.Text.ToString()));
                    txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));
                }
            }
        }

        protected void lbtnHSCode_Click(object sender, EventArgs e)
        {
            string _cusdecType = Request.QueryString["CUSTTYPE"];
            if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
            {

                if (string.IsNullOrEmpty(txtLineNo.Text))
                {
                    DisplayMessage("Please select item line", 2);
                    txtLineNo.Focus();
                    return;
                }

                if (oCusdecItm != null)
                {
                    GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
                    Label lblCui_hs_cd = drSelect.FindControl("lblHSCode") as Label;
                    if (oCusdecItm.Count > 0)
                    {
                        if (oSelectItemLine > 0)
                        {
                            ImpCusdecItm oEditedItem = oCusdecItm.Find(x => x.Cui_line == oSelectItemLine);
                            oEditedItem.Cui_hs_cd = lblCui_hs_cd.Text.ToString();
                            CHNLSVC.Financial.Update_BL_HSCode(txtBLno.Text.ToString(), oEditedItem.Cui_itm_cd.ToString(), lblCui_hs_cd.Text.ToString());
                            BindItems();
                        }
                    }

                    DisplayMessage("HS Updated!", 3);
                }

            }
        }

        protected void btnUpdateMainHS_Click(object sender, EventArgs e)
        {
            string _cusdecType = Request.QueryString["CUSTTYPE"];
            if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
            {
                if (string.IsNullOrEmpty(txtMainHS.Text.ToString()))
                {
                    txtMainHS.Focus();
                    DisplayMessage("Please enter the main HS code!", 1);
                    return;
                }

                if (oCusdecItm != null)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                    DataTable result = CHNLSVC.CommonSearch.SearchGetHsCode(SearchParams, "Code", txtMainHS.Text.Trim());
                    if (result != null && result.Rows.Count > 0)
                    {
                        if (oCusdecItm.Count > 0)
                        {
                            foreach (ImpCusdecItm cusItem in oCusdecItm)
                            {
                                cusItem.Cui_hs_cd = txtMainHS.Text.ToString();
                                //cusItem.Cui_itm_desc = txtItemDesc.Text.ToString();   
                                CHNLSVC.Financial.Update_BL_HSCode(txtBLno.Text.ToString(), cusItem.Cui_itm_cd.ToString(), txtMainHS.Text.ToString());
                            }
                        }
                        BindItems();
                        DisplayMessage("Main HS Updated!", 3);
                    }
                    else
                    {
                        txtMainHS.Focus();
                        DisplayMessage("Invalid main HS Code!", 1);
                    }
                }
            }
        }

        protected void chkIgnoreCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (oCusdecItm != null && oCusdecItm.Count > 0)
            {
                if (chkIgnoreCountry.Checked == true)
                {
                    foreach (ImpCusdecItm cusItem in oCusdecItm)
                    {
                        cusItem.Cui_def_cnty = txtCtryOriginItem.Text.ToString();
                    }
                }
                else
                {
                    foreach (ImpCusdecItm cusItem in oCusdecItm)
                    {
                        cusItem.Cui_def_cnty = string.Empty;
                    }
                }
            }
        }

        protected void chkAllTax_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRefno.Text))
            {
                if (chkAllTax.Checked == true)
                {
                    oCusdecDutyElementTot = new List<HsCode>();
                    List<ImpCusdecItmCost> _tempTot = CHNLSVC.Financial.GetDutyElementSummary(txtRefno.Text.ToString().ToUpper());
                    foreach (ImpCusdecItmCost itm in _tempTot)
                    {
                        HsCode _hs = new HsCode();
                        _hs.Mhc_cost_cat = itm.Cuic_ele_cat;
                        _hs.Mhc_cost_tp = itm.Cuic_ele_tp;
                        _hs.Mhc_hs_cd = itm.Cuic_ele_cd;
                        _hs.Mhc_cost_ele = itm.Cuic_ele_cd;
                        _hs.Mhc_mp = 0;
                        _hs.Tax_base = itm.Cuic_ele_base;
                        _hs.Tax_rate = itm.Cuic_ele_rt;
                        _hs.Tax_amount = itm.Cuic_ele_amt;
                        oCusdecDutyElementTot.Add(_hs);
                    }
                    BindDutyElementsTotal();
                    pnlDutyElements.Visible = false; pnlHSHistory.Visible = false; pnlDutyElementsTotal.Visible = true;
                }
                else
                { pnlDutyElements.Visible = true; pnlHSHistory.Visible = false; pnlDutyElementsTotal.Visible = false; }
            }
        }

       
        protected void btncustdec_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {
                Session["GlbReportName"] = "custom_val_declear.rpt";
                BaseCls.GlbReportHeading = "Values Declaration";
                Session["GlbReportName"] = "custom_val_declear.rpt";
               // Response.Redirect("~/View/Reports/Wharf/WharfReportsViewer.aspx", false);

                clswharf obj = new clswharf();
                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                DataTable Hdrdata = CHNLSVC.CustService.GetCusdecHDRData(EntryNo, COM);
                if (Hdrdata != null)
                {
                    if (Hdrdata.Rows.Count > 0)
                    {
                        obj.ValueDeclarationForm(EntryNo, COM, Hdrdata);
                        PrintPDF(targetFileName, obj._valueDeclarationReport);
                        url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                        //string url = "<script>window.open('/View/Reports/Wharf/WharfReportsViewer.aspx','_blank');</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        CHNLSVC.MsgPortal.SaveReportErrorLog("custom_val_declear", "Whlf", "Run Ok", Session["UserID"].ToString());
                    }
                    else
                    {
                        DisplayMessage("Please Check the Company", 1);
                    }
                }
                else
                {
                    DisplayMessage("Please Check the Company", 1);
                }
            }
        }

        protected void btncustele_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {
                Session["GlbReportName"] = "custom_ele.rpt";
                BaseCls.GlbReportHeading = "Values Declaration";
                Session["GlbReportName"] = "custom_ele.rpt";

                clswharf obj = new clswharf();
                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                string user = Session["UserID"].ToString();
                obj.CustomElereport(COM, EntryNo, "DUTY", user);
                PrintPDF(targetFileName, obj._cus_ele);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

               //url = "<script>window.open('/View/Reports/Wharf/WharfReportsViewer.aspx','_blank');</script>";
               // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("custom_ele", "Whlf", "Run Ok", Session["UserID"].ToString());
            }
        }

        protected void Custpanalsheet_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {
                Session["GlbReportName"] = "custom_workingsheet.rpt";
                BaseCls.GlbReportHeading = "Values Declaration";
                Session["GlbReportName"] = "custom_workingsheet.rpt";
                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                clswharf obj = new clswharf();
                obj.CreateWorkingSheet(EntryNo, "TOT", txtFileNo.Text.ToString(),txtExporterName.Text.ToString());
                PrintPDF(targetFileName, obj._cus_work);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("custom_workingsheet", "Whlf", "Run Ok", Session["UserID"].ToString());
            }
        }

        protected void Importscargo_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {
                Session["GlbReportName"] = "cargo_imports.rpt";
                BaseCls.GlbReportHeading = "Cargo Imports";

                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                clswharf obj = new clswharf();
                obj.CargoImports(EntryNo, COM);
                PrintPDF(targetFileName, obj._cargo_imp);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                url = "<script>window.open('/View/Reports/Wharf/WharfReportsViewer.aspx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("custom_workingsheet", "Whlf", "Run Ok", Session["UserID"].ToString());
            }
        }

        protected void DOWNLOAD_Click(object sender, EventArgs e)
        {
            string name = "C:/Users/subodanam/Desktop/subo1.xlsx";
            string filename = "sube.xlsx";
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
                Response.Write("This file does not exist.");
            }
 

      
        }

        protected void chkLicenseEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLicenseEdit.Checked == true)
            {
                txtLicenceNo.ReadOnly = false;
            }
            else
            {
                txtLicenceNo.ReadOnly = true; 
            }
        }

        protected void Goodsdeclaration_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {
                Session["GlbReportName"] = "GoodsDeclaration.rpt";
                BaseCls.GlbReportHeading = "Goods Declaration";
                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                clswharf obj = new clswharf();
                obj.GoodsDecarration(EntryNo, COM);
                PrintPDF(targetFileName, obj._goods_declaration);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("custom_workingsheet", "Whlf", "Run Ok", Session["UserID"].ToString());
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
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
                return;
            }
        }

        protected void dgvSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordNew.ClientID + "').value = '';", true);
                string code = dgvSearch.SelectedRow.Cells[1].Text;
                string desc = dgvSearch.SelectedRow.Cells[2].Text;
                Session["IsSearchNew"] = null;
                serPopup.Hide();

                if (lblSerValue.Text == "BLHeader" || lblSerValue.Text == "CusdecReq")
                {
                    txtBLno.Text = code;
                    txtBLno_TextChanged(null, null);
                }
            }
            catch (Exception)
            {
                DisplayMessage("Please enter the total net mass", 4); 
            }
        }

        protected void dgvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvSearch.PageIndex = e.NewPageIndex;
                dgvSearch.DataSource = null;
                dgvSearch.DataSource = (DataTable)ViewState["SEARCH"];
                dgvSearch.DataBind();
                serPopup.Show(); Session["IsSearchNew"] = true;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearchNew_Click(object sender, EventArgs e)
        {
            try
            {
                FilterDataNew();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Session["IsSearchNew"] = null;
            serPopup.Hide();
        }

        protected void txtSearchbywordNew_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterDataNew();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void lbtnSeDateRange_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtFrom = DateTime.Today, dtTo = DateTime.Today;
                dtFrom = DateTime.TryParse(txtSeFromDate.Text.Trim(), out dtFrom) ? Convert.ToDateTime(txtSeFromDate.Text.Trim()) : dtFrom;
                dtTo = DateTime.TryParse(txtSerToDate.Text.Trim(), out dtTo) ? Convert.ToDateTime(txtSerToDate.Text.Trim()) : dtTo;
                if (ViewState["SEARCH"] != null)
                {
                    if (lblSerValue.Text == "BLHeader")
                    {
                        string _cusdecType = Request.QueryString["CUSTTYPE"];
                        if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
                        {
                            //ValidateTrue();
                            ViewState["SEARCH"] = null;
                            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                            DataTable result = CHNLSVC.CommonSearch.SearchBLHeaderNew(SearchParams, ddlSearchby.Text, txtSearchbywordNew.Text.Trim(), 1, dtFrom, dtTo);
                            dgvSearch.DataSource = result;
                            dgvSearch.DataBind();
                            ViewState["SEARCH"] = result;
                            serPopup.Show(); Session["IsSearchNew"] = true;
                        }
                    }
                    if (lblSerValue.Text == "CusdecReq")
                    {
                        string _cusdecType = Request.QueryString["CUSTTYPE"];
                        if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                        {
                            //ValidateTrue();
                            ViewState["SEARCH"] = null;
                            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecReq);
                            DataTable result = CHNLSVC.CommonSearch.SearchCusdecReqNew(SearchParams, null, null, 1, dtFrom, dtTo);
                            dgvSearch.DataSource = result;
                            dgvSearch.DataBind();
                            ViewState["SEARCH"] = result;
                            serPopup.Show(); Session["IsSearchNew"] = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void Goodsdeclarationsheet_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                Session["GlbReportType"] = "";
                Session["RefDoc"] = txtRefno.Text.Trim();
                if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Entry  No", 1);
                }
                else
                {
                    Session["GlbReportName"] = "GoodsDeclarationSheet.rpt";
                    BaseCls.GlbReportHeading = "Goods Declaration Sheet";
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    clswharf obj = new clswharf();
                    obj.GoodsDecarrationSheet(EntryNo, COM);
                    PrintPDF(targetFileName, obj._goods_declarationsheet);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("GoodsDeclarationSheet", "Whlf", "Run Ok", Session["UserID"].ToString());

                }
            }catch(Exception ex)
            {
                DisplayMessage(ex.Message,2);
                CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", ex.Message, Session["UserID"].ToString());
                return;
            }
           
        }

        protected void Goodsdeclaration2_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {
                Session["GlbReportName"] = "GoodsDeclarationII.rpt";
                BaseCls.GlbReportHeading = "Goods Declaration2";
                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                clswharf obj = new clswharf();
                obj.GoodsDeclarationSchedule2(EntryNo, COM);
                PrintPDF(targetFileName, obj._goods_declarationII);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("GoodsDeclarationII", "Whlf", "Run Ok", Session["UserID"].ToString());
            }
        }

        protected void Goodsdeclaration3_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {

                Session["GlbReportName"] = "GoodsDeclarationIII.rpt";
                BaseCls.GlbReportHeading = "Goods Declaration3";
                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                clswharf obj = new clswharf();
                obj.GoodsDecarrationAsycuda(EntryNo, COM);
                PrintPDF(targetFileName, obj._goods_declarationIII);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("GoodsDeclarationIII", "Whlf", "Run Ok", Session["UserID"].ToString());
            }
        }

        protected void chkItemDesc_CheckedChanged(object sender, EventArgs e)
        {
            if (oCusdecItm != null && oCusdecItm.Count > 0)
            {
                if (chkItemDesc.Checked == true)
                {
                    txtItemDesc.ReadOnly = false;
                    txtItemDesc.Focus(); 
                }
            }
        }

        protected void Goodsdeclarationsheetother_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                Session["GlbReportType"] = "";
                Session["RefDoc"] = txtRefno.Text.Trim();
                if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Entry  No", 1);
                }
                else
                {
                    Session["GlbReportName"] = "GoodsDeclarationSheetother.rpt";
                    BaseCls.GlbReportHeading = "Goods Declaration Sheetother";
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    clswharf obj = new clswharf();
                    obj.GoodsDecarrationSheetother(EntryNo, COM);
                    PrintPDF(targetFileName, obj._goods_declarationsheetother);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("GoodsDeclarationSheet", "Whlf", "Run Ok", Session["UserID"].ToString());

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
                CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", ex.Message, Session["UserID"].ToString());
                return;
            }
        }

        protected void GoodsdeclarationsheetRebond_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                Session["GlbReportType"] = "";
                Session["RefDoc"] = txtRefno.Text.Trim();
                if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Entry  No", 1);
                }
                else
                {
                    Session["GlbReportName"] = "GoodsDeclarationSheetRebond.rpt";
                    BaseCls.GlbReportHeading = "Goods Declaration SheetRebond";
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    clswharf obj = new clswharf();
                    obj.GoodsDecarrationSheetRebond(EntryNo, COM);
                    PrintPDF(targetFileName, obj._goods_declarationsheetRebond);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("GoodsDeclarationSheet", "Whlf", "Run Ok", Session["UserID"].ToString());

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
                CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", ex.Message, Session["UserID"].ToString());
                return;
            }
        }

        protected void CusdecEntryDetails_Click(object sender, EventArgs e)
        {
            
        }

        protected void lbtnexbond2_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                Session["GlbReportType"] = "";
                Session["RefDoc"] = txtRefno.Text.Trim();
                if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Entry  No", 1);
                }
                else
                {
                    Session["GlbReportName"] = "Rebond2.rpt";
                    BaseCls.GlbReportHeading = "Rebond new";
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    clswharf obj = new clswharf();
                    obj.EXBOND2(EntryNo, COM);
                    PrintPDF(targetFileName, obj._cusdec_Exbond2);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("GoodsDeclarationSheet", "Whlf", "Run Ok", Session["UserID"].ToString());

                }

            }catch(Exception ex)
            {
                DisplayMessage(ex.Message,2);
                CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", ex.Message, Session["UserID"].ToString());
            }
        }

        protected void lbtntobond2_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                Session["GlbReportType"] = "";
                Session["RefDoc"] = txtRefno.Text.Trim();
                if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Entry  No", 1);
                }
                else
                {
                    Session["GlbReportName"] = "GoodsDeclarationSheet.rpt";
                    BaseCls.GlbReportHeading = "Goods Declaration Sheet";
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    clswharf obj = new clswharf();
                    obj.Tobond2(EntryNo, COM);
                    PrintPDF(targetFileName, obj._cusdec_Tobond2);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", "Run Ok", Session["UserID"].ToString());

                }
            }catch(Exception EX)
            {
                DisplayMessage(EX.Message, 2);
                CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", EX.Message, Session["UserID"].ToString());
            }

        }

        protected void lbtnrebond2_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                Session["GlbReportType"] = "";
                Session["RefDoc"] = txtRefno.Text.Trim();
                if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Entry  No", 1);
                }
                else
                {
                    Session["GlbReportName"] = "GoodsDeclarationSheet.rpt";
                    BaseCls.GlbReportHeading = "Goods Declaration Sheet";
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    clswharf obj = new clswharf();
                    obj.REBOND2(EntryNo, COM);
                    PrintPDF(targetFileName, obj._cusdec_Rebond2);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", "Run Ok", Session["UserID"].ToString());

                }
            }
            catch (Exception EX)
            {
                DisplayMessage(EX.Message, 2);
                CHNLSVC.MsgPortal.SaveReportErrorLog("ToBond 2", "Whlf", EX.Message, Session["UserID"].ToString());
            }

        }

        protected void lbtngrdContaiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;

                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    dgvContainers.EditIndex = grdr.RowIndex;
                    dgvContainers.DataSource = oCusdecContainer;
                    dgvContainers.DataBind();
                    //LoadPOItems(txtPONo.Text.ToString());
                }
                //grdDOItems_2.DataSource = (DataTable)ViewState["OrderItem"];
                //grdDOItems_2.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtngrdContaiUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                string oldno = "";
                string _con = (grdr.FindControl("lblIbc_tp") as Label).Text;
                //string _status = (grdr.FindControl("POD_ITM_STUS") as Label).Text;
                string _no = (grdr.FindControl("txtIbc_desc") as TextBox).Text;
                //Int32 itmLine = Convert.ToInt32((grdr.FindControl("PODI_LINE_NO") as Label).Text);

                if (oCusdecContainer.Count > 0)
                {
                   
                    var _filter = oCusdecContainer.FirstOrDefault(x => x.Ibc_tp == _con);
                    oldno = _filter.Ibc_desc;
                    if (_filter != null)
                    {
                        _filter.Ibc_desc = _no;
                    }
                    string doc = oCusdecHdr.CUH_SUN_REQ_NO.ToString();
                    List<ImportsBLContainer> blContainers = new List<ImportsBLContainer>();
                    ImportsBLContainer conob = new ImportsBLContainer();
                    blContainers =CHNLSVC.Financial.GetContainers(doc);
                    conob = blContainers.Where(a => a.Ibc_desc == oldno).First();
                    if (conob !=null)
                    {
                        conob.Ibc_cre_by = Session["UserID"].ToString();
                        int iffectnw = CHNLSVC.Financial.SAVE_BL_CONTAINERSLog(conob);

                        if (iffectnw >0)
                        {
                            conob.Ibc_desc = _no;
                            conob.Ibc_cre_by = Session["UserID"].ToString();
                            iffectnw = CHNLSVC.Financial.SAVE_BL_CONTAINERS(conob);

                            if (iffectnw>0)
                            {
                                DisplayMessage("Successfully Updated",3);
                            }
                        }
                    }

                }

                dgvContainers.EditIndex = -1;
                BindContainers();
               
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void btnCountyOfOrigin_Click(object sender, EventArgs e)
        {
           
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
            DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);

           // result.Columns["Country"].SetOrdinal(1);
            //result.Columns["Description"].SetOrdinal(0);

            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "CountyOfOrigin";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void PrintResultsInvoice(string bond)
        {
            try
            {
                if (!string.IsNullOrEmpty(bond))
                {
                    Session["bondnum"] = bond;

                    lblMssg.Text ="Successfully Saved "+bond+ " Do you want to print now?";
                    Session["print_type"] = "bond";
                    PopupConfBox.Show();

                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }
        protected void btnalertYes_Click(object sender, EventArgs e)
        {
            string type = Session["print_type"].ToString();

            if (type == "bond")
            {
                string invoice = Session["bondnum"].ToString();
                Session["RefDoc"] = invoice;

                string custype = Request.QueryString["CUSTTYPE"].ToString();
                if (custype=="EX")
                {
                    Goodsdeclarationsheetother_Click(null, null);
                } else if(custype=="RE")
                {
                    GoodsdeclarationsheetRebond_Click(null, null);
                }else if(custype=="TO")
                {
                    Goodsdeclarationsheet_Click(null, null);
                }
                else
                {
                    Goodsdeclaration3_Click(null,null);
                }
                
            }
           
        }

        protected void btnalertNo_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }

        protected void lbtncusdeccancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16064))
                {
                    string custype = Request.QueryString["CUSTTYPE"].ToString();
                    string entryno = txtRefno.Text.ToString();
                    string sino = txtBLno.Text.ToString();
                    string user = Session["UserID"].ToString();
                    string com = Session["UserCompanyCode"].ToString();
                    string status = oCusdecHdr.CUH_STUS;
                    if (status=="C")
                    {
                        DisplayMessage("Selected document has been already cancelled !", 1);
                        return;
                    }
                    if (entryno == "")
                    {
                        DisplayMessage("Please Enter Entry No!!", 1);
                        return;
                    }
                    if (custype == "TO" || custype == "LR" || custype == "AIR" || custype == "MV")
                    {
                        //VALIDATION
                        DataTable isgrn = CHNLSVC.Financial.CusdecCancelIsGRN(Session["UserCompanyCode"].ToString(), sino, 1);
                        DataTable iscts = CHNLSVC.Financial.CusdecCancelIsGRN(Session["UserCompanyCode"].ToString(), sino, 2);
                        if (isgrn.Rows.Count > 0)
                        {
                            DisplayMessage("Cannot Cancel! Already GRN has been made for this document ! ", 1);
                            return;
                        }
                        if (iscts.Rows.Count > 0)
                        {
                            DisplayMessage("Cannot Cancel! Already costing has been done for this document ! ", 1);
                            return;
                        }
                        if (custype == "TO")
                        {
                            DataTable iscusdec = CHNLSVC.Financial.CusdecCancelIsGRN(Session["UserCompanyCode"].ToString(), entryno, 3);
                            int entrycount = Convert.ToInt16(iscusdec.Rows[0][0].ToString());
                            DataTable pendingreq = CHNLSVC.Financial.CusdecCancelIsGRN(Session["UserCompanyCode"].ToString(), entryno, 4);
                            int pencount = Convert.ToInt16(pendingreq.Rows[0][0].ToString());
                            if (entrycount > 0)
                            {
                                DisplayMessage("Cannot Cancel! Already entry has been done for this document ! ", 1);
                                return;
                            }
                            if (pencount > 0)
                            {
                                DisplayMessage("Can't Cancel! Already Pending Request for this doc!!", 1);
                                return;
                            }

                        }
                        //Update Process
                        string err = "";
                        int effect = CHNLSVC.Financial.CusdecEntryCancelation(com, entryno, sino, user, out err);
                        if (effect == -1)
                        {
                            DisplayMessage(err, 4);
                        }
                        else
                        {
                            DisplayMessage("Successfully Cancelled!!", 3);
                            clearAll();
                            textboxclear();
                        }
                    }

                    if (custype == "EX" || custype == "RE" || custype == "EXP")
                    {
                        //VALIDATION
                        DataTable chkvalex = CHNLSVC.Financial.CusdecCancelIsGRN(Session["UserCompanyCode"].ToString(), entryno, 5);
                        if (chkvalex.Rows.Count > 0)
                        {
                            DisplayMessage("Can't Cancel! Already Use this doc!!", 1);
                            return;
                        }
                        //Update Process
                        string err = "";
                        int effect = CHNLSVC.Financial.CusdecEntryCancelation_exreboi(com, entryno, sino, user, oCusdecItm, oCusdecHdr.CUH_SUN_BOND_NO.ToString(),"", out err);
                        if (effect == -1)
                        {
                            DisplayMessage(err, 4);
                        }
                        else
                        {
                            DisplayMessage("Successfully Cancelled!!", 3);
                            clearAll();
                            textboxclear();
                        }
                    }
                   
                }
                else
                {
                    DisplayMessage("Pleade Active Permission!! 16064", 1);
                    return;
                }
             

            }catch(Exception ex)
            {
                DisplayMessage(ex.Message,4);
                return;
            }
        }

        private void textboxclear()
        {
            txtProceCode.Text = "";
            txtProceCode1.Text = "";
            txtProceCode2.Text = "";
            txtMarksNumbers.Text = "";

            txtBLno.Text = "";
            txtCusdecEntryNo.Text = "";

            txtConsigneeTin.Text = "";
            txtConsigneeCode.Text = "";
            txtConsigneeName.Text = "";
            txtConsigneeAddress.Text ="";

            txtExporterTin.Text ="";
            txtExporterCode.Text ="";
            txtExporterName.Text ="";
            txtExporterAddress.Text ="";

            txtDeclarantTin.Text ="";
            txtDeclarantCode.Text ="";
            txtDeclarantName.Text ="";
            txtDeclarantAddress.Text ="";

            txtPreviousDocument.Text ="";
            txtLists.Text = "0";
            txtItems.Text = "";
            txtTotalPackages.Text = "";
            txtVessleNo.Text ="";
            txtPlaceOfLoading.Text ="";
            txtVoyageNo.Text ="";
            txtCrtyExport.Text = "";
            txtCrtyExportCode.Text = "";
            txtCrtyDesti.Text = "";
            txtCrtyDestiCode.Text ="";
            txtCrtyOrigin.Text = "";
            txtCrtyOriginCode.Text = "";
            txtTermsOfPayment.Text = "";
            txtTermsOfPaymentCustom.Text = "";
            txtDelTerms.Text = "";
            txtCurrency.Text = "";
            txtInvoiceAmt.Text = "";
            txtInvoiceAmt.Text = "";
            txtExRate.Text = "";
            txtValueDetails.Text ="";
            txtValueDetails.Text = "";
            txtComChgAmount.Text = "";
            txtFileNo.Text = "";
            txtFinRefNo.Text = "";
            txtBankCode.Text = "";
            txtBankName.Text = "";
            txtBranch.Text = "";
            txtTotGrossMass.Text = "";
            txtTotNetMass.Text = "";
            txtMainHS.Text = "";
            txtRefno.Text = "";
            txtBLno.Text = "";
            txtVessleNo.Text = "";
            txtPlaceOfLoading.Text = "";
            txtVoyageNo.Text = "";
            txtFCL.Text = "";
            txtFileNo.Text = "";
            txtLists.Text = "";
            txtFinSettlement.Text = "";
            txtLastConsiName.Text = "";
            txtLastConsi.Text = "";
            txtTradingCountryName.Text = "";
            txtTradingCountry.Text = "";
            txtMarksNumbers.Text = "";
            txtCrtyExport.Text = "";
            txtCurrency.Text = "";
            txtInvoiceAmt.Text = "";
            txtFinRefNo.Text = "";
            txtTermsOfPayment.Text = "";
            txtCurrency.Text = "";
            txtFinRefNo.Text = "";
            txtTermsOfPayment.Text = "";
            txtTermsOfPaymentCustom.Text = "";
            txtBankCode.Text = "";
            txtTotGrossMass.Text = "";
            txtTotNetMass.Text = "";
            txtMainHS.Text = "";
            txtBankCode.Text = "";
            txtBankName.Text = "";
            txtBranch.Text = "";
        }

        protected void chkitem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkitem.Checked)
            {
                dgvItems.DataSource = oCusdecItm.OrderBy(a => a.Cui_itm_cd).ToList();
                dgvItems.DataBind();
            }
            else
            {
                dgvItems.DataSource = oCusdecItm.OrderByDescending(a => a.Cui_itm_cd).ToList();
                dgvItems.DataBind();
            }
           
        }

        protected void chkmodel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkmodel.Checked)
            {
                dgvItems.DataSource = oCusdecItm.OrderBy(a => a.Cui_model).ToList();
                dgvItems.DataBind();
            }
            else
            {
                dgvItems.DataSource = oCusdecItm.OrderByDescending(a => a.Cui_model).ToList();
                dgvItems.DataBind();
            }
        }

        protected void chkhscode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhscode.Checked)
            {
                dgvItems.DataSource = oCusdecItm.OrderBy(a => a.Cui_hs_cd).ToList();
                dgvItems.DataBind();
            }
            else
            {
                dgvItems.DataSource = oCusdecItm.OrderByDescending(a => a.Cui_hs_cd).ToList();
                dgvItems.DataBind();
            }
        }

        protected void chkdesc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdesc.Checked)
            {
                dgvItems.DataSource = oCusdecItm.OrderBy(a => a.Cui_itm_desc).ToList();
                dgvItems.DataBind();
            }
            else
            {
                dgvItems.DataSource = oCusdecItm.OrderByDescending(a => a.Cui_itm_desc).ToList();
                dgvItems.DataBind();
            }
        }

        protected void chkqty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkqty.Checked)
            {
                dgvItems.DataSource = oCusdecItm.OrderBy(a => a.Cui_qty).ToList();
                dgvItems.DataBind();
            }
            else
            {
                dgvItems.DataSource = oCusdecItm.OrderByDescending(a => a.Cui_qty).ToList();
                dgvItems.DataBind();
            }
        }

        protected void chkprice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkprice.Checked)
            {
                dgvItems.DataSource = oCusdecItm.OrderBy(a => a.Cui_unit_amt).ToList();
                dgvItems.DataBind();
            }
            else
            {
                dgvItems.DataSource = oCusdecItm.OrderByDescending(a => a.Cui_unit_amt).ToList();
                dgvItems.DataBind();
            }
        }

        protected void btnupdatemass_Click(object sender, EventArgs e)
        {
            string _cusdecType = Request.QueryString["CUSTTYPE"];
            if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
            {

                if (string.IsNullOrEmpty(txtTotNetMass.Text))
                {
                    DisplayMessage("Please enter the total net mass", 2);
                    txtTotNetMass.Focus();
                    return;
                }
                else
                {
                    Decimal _num;
                    if (!Decimal.TryParse(txtTotNetMass.Text.ToString(), out _num))
                    {
                        DisplayMessage("Invalid total net mass amount", 2);
                        txtTotNetMass.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtTotGrossMass.Text))
                {
                    DisplayMessage("Please enter the total gross mass", 2);
                    txtTotGrossMass.Focus();
                    return;
                }
                else
                {
                    Decimal _num;
                    if (!Decimal.TryParse(txtTotGrossMass.Text.ToString(), out _num))
                    {
                        DisplayMessage("Invalid total gross mass amount", 2);
                        txtTotGrossMass.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtInvoiceAmt.Text))
                {
                    DisplayMessage("Please enter the invoice amount", 2);
                    txtInvoiceAmt.Focus();
                    return;
                }
                else
                {
                    Decimal _num;
                    if (!Decimal.TryParse(txtInvoiceAmt.Text.ToString(), out _num))
                    {
                        DisplayMessage("Invalid invoice amount", 2);
                        txtInvoiceAmt.Focus();
                        return;
                    }
                }

                if (oCusdecItm != null)
                {
                    if (oCusdecItm.Count > 0)
                    {
                        decimal _fob_val = 0;
                        foreach (ImpCusdecCost _eleCost in oCusdecCost)
                        {
                            if (_eleCost.Cus_ele_cd == "COST") _fob_val = _eleCost.Cus_amt;
                        }
                        // oCusdecItm = CHNLSVC.Financial.UpdateHSUsingHistory(oCusdecItm, Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19, _cusdecType, txtBLno.Text.ToString(), _fob_val, Convert.ToDecimal(txtTotNetMass.Text), Convert.ToDecimal(txtTotGrossMass.Text), txtRefno.Text.ToString());
                        oCusdecItm = CHNLSVC.Financial.UpdateMassOnly(oCusdecItm, Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19, _cusdecType, txtBLno.Text.ToString(), _fob_val, Convert.ToDecimal(txtTotNetMass.Text), Convert.ToDecimal(txtTotGrossMass.Text), txtRefno.Text.ToString());
                    }
                }

                BindItems();

                DisplayMessage("Items Updated!", 3);
            }
        }

        protected void chkhstax_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRefno.Text))
            {
                if (chkhstax.Checked == true)
                {
                    if (txtHSCode.Text.ToString()=="")
                    {
                        DisplayMessage("Please Select HS Item!!!", 2);
                        return;
                    }
                    oCusdecDutyElementTot = new List<HsCode>();
                    List<ImpCusdecItmCost> _tempTot = CHNLSVC.Financial.GetDutyElementSummaryForHS(txtRefno.Text.ToString().ToUpper(), txtHSCode.Text.ToString().Trim());
                    foreach (ImpCusdecItmCost itm in _tempTot)
                    {
                        HsCode _hs = new HsCode();
                        _hs.Mhc_cost_cat = itm.Cuic_ele_cat;
                        _hs.Mhc_cost_tp = itm.Cuic_ele_tp;
                        _hs.Mhc_hs_cd = itm.Cuic_ele_cd;
                        _hs.Mhc_cost_ele = itm.Cuic_ele_cd;
                        _hs.Mhc_mp = 0;
                        _hs.Tax_base = itm.Cuic_ele_base;
                        _hs.Tax_rate = itm.Cuic_ele_rt;
                        _hs.Tax_amount = itm.Cuic_ele_amt;
                        oCusdecDutyElementTot.Add(_hs);
                    }
                    BindDutyElementsTotal();
                    pnlDutyElements.Visible = false; pnlHSHistory.Visible = false; pnlDutyElementsTotal.Visible = true;
                }
                else
                { pnlDutyElements.Visible = true; pnlHSHistory.Visible = false; pnlDutyElementsTotal.Visible = false; }
            }
        }

        protected void txtCus_amt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
                Label lblCus_ele_cd = (Label)row.FindControl("lblCus_ele_cd");
                TextBox txtCus_amt = (TextBox)row.FindControl("txtCus_amt");

                //if (Session["oCusdecCost"] != null)
                //{
                //oCusdecCost = (List<ImpCusdecCost>)Session["oCusdecCost"];
                if (!isdecimal(txtCus_amt.Text))
                {
                    DisplayMessage("Please enter valid amount", 2);
                    return;
                }
                ImpCusdecCost oEditedItem = oCusdecCost.Find(x => x.Cus_ele_cd == lblCus_ele_cd.Text);
                oEditedItem.Cus_amt = (txtCus_amt.Text == "") ? 0 : Convert.ToDecimal(txtCus_amt.Text);
                //Session["oCusdecCost"] = oCusdecCost;
                BindCostItems();
                dgvCostItems.EditIndex = -1;
                Decimal oTotalValue = 0;
                oTotalValue = oCusdecCost.Where(z => z.Cus_act == 1).Sum(x => x.Cus_amt);
                txtInvoiceAmt.Text = oTotalValue.ToString("N2");
                oCusdecHdr.CUH_TOT_AMT = Math.Round(oTotalValue, 2);
                txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                txtValueDetails.Text = Convert.ToString(oTotalValue * Convert.ToDecimal(txtExRate.Text.ToString()));
                txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));
                if (row.RowIndex + 1 < dgvCostItems.Rows.Count)
                {
                    GridViewRow dr = dgvCostItems.Rows[row.RowIndex + 1];
                    LinkButton lbtnedititem = dr.FindControl("lbtnedititem") as LinkButton;
                    lbtnedititem.Focus();
                }

                
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

            BindCostItems();
        }

        protected void printrange_Click(object sender, EventArgs e)
        {
            PrintRange2.Show();
            lbtnprintrange.Enabled = true;
            //\\10.12.6.102\Panasonic KX-P1131E
            string _defcusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "WALFDEFPRINTER");
            //lbldefprinter.Text = GetDefaultPrinter();
            lbldefprinter.Text = _defcusacc;
        }

        protected void lbtnPriceClose_Click(object sender, EventArgs e)
        {
            PrintRange2.Hide();

        }

        protected void lbtnprintrange_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtendentry.Text=="" || txtstentry.Text=="")
                {
                    DisplayMessage("Please Enter Entry Numbers");
                    return;
                }
                if (rdbchkexbond.Checked==false && rdbcheckrebond.Checked==false)
                {
                    DisplayMessage("Please Select Rebond Or ExBond");
                    return;
                }
                string _type = "";
                DateTime fromdate = CHNLSVC.Financial.GetEntryDatetime(Session["UserCompanyCode"].ToString(), txtstentry.Text.ToString().Trim());
                DateTime todate = CHNLSVC.Financial.GetEntryDatetime(Session["UserCompanyCode"].ToString(), txtendentry.Text.ToString().Trim());

                if (rdbchkexbond.Checked == true)
                {
                    _type = "EX";
                }
                else
                {
                    _type = "RE";
                }
                DataTable Dt = CHNLSVC.Financial.GetEntryNumbers(Session["UserCompanyCode"].ToString(), _type, fromdate, todate);
                if (Dt != null && Dt.Rows.Count>0)
                {
                    lbtnprintrange.Enabled = false;
                    clswharf obj = new clswharf();
                    int i = 0;
                    string _defcusacc = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "WALFDEFPRINTER");
                    foreach (var _row in Dt.Rows)
                    {
                        if (_type=="RE")
                        {
                            
                            obj.GoodsDecarrationSheetRebond(Dt.Rows[i][0].ToString(), Session["UserCompanyCode"].ToString());
                            obj._goods_declarationsheetRebond.PrintOptions.PrinterName = _defcusacc;
                            obj._goods_declarationsheetRebond.PrintToPrinter(1, true,0,0);
                        }
                        if (_type == "EX")
                        {

                            obj.GoodsDecarrationSheetother(Dt.Rows[i][0].ToString(), Session["UserCompanyCode"].ToString());
                            obj._goods_declarationsheetRebond.PrintOptions.PrinterName = _defcusacc;
                            obj._goods_declarationsheetother.PrintToPrinter(1, true, 0, 0);
                        }
                        i++;
                    }
                    obj._goods_declarationsheetRebond.Close();
                    obj._goods_declarationsheetRebond.Dispose();
                    obj._goods_declarationsheetother.Close();
                    obj._goods_declarationsheetother.Dispose();

                }

            }catch(Exception ex)
            {
                DisplayMessage(ex.Message, 4);
                CHNLSVC.MsgPortal.SaveReportErrorLog("TO BOND", "Cusdec", ex.Message, Session["UserID"].ToString());
            }
        }
        string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }

        protected void chkcat1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcat1.Checked)
            {
                dgvItems.DataSource = oCusdecItm.OrderBy(a => a.ItemCat1).ToList();
                dgvItems.DataBind();
            }
            else
            {
                dgvItems.DataSource = oCusdecItm.OrderByDescending(a => a.ItemCat1).ToList();
                dgvItems.DataBind();
            }
        }

        protected void chkcat2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcat2.Checked)
            {
                dgvItems.DataSource = oCusdecItm.OrderBy(a => a.ItemCat2).ToList();
                dgvItems.DataBind();
            }
            else
            {
                dgvItems.DataSource = oCusdecItm.OrderByDescending(a => a.ItemCat2).ToList();
                dgvItems.DataBind();
            }
        }

        protected void lbtnworkcat_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {
                Session["GlbReportName"] = "custom_workingsheetcat.rpt";
                BaseCls.GlbReportHeading = "Values Declaration";
                Session["GlbReportName"] = "custom_workingsheetcat.rpt";
                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                clswharf obj = new clswharf();
                obj.CreateWorkingSheetCat(EntryNo, "TOT", txtFileNo.Text.ToString(), txtExporterName.Text.ToString());
                PrintPDF(targetFileName, obj._cus_workcat);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("custom_workingsheet", "Whlf", "Run Ok", Session["UserID"].ToString());
            }
        }

        protected void lbtnModel_Click(object sender, EventArgs e)
        {
            string url = "";
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            Session["GlbReportType"] = "";
            Session["RefDoc"] = txtRefno.Text.Trim();
            if (txtRefno.Text.Trim() == null | txtRefno.Text.Trim() == "")
            {
                DisplayMessage("Please Select Entry  No", 1);
            }
            else
            {
                Session["GlbReportName"] = "custom_workingsheetitm.rpt";
                BaseCls.GlbReportHeading = "Values Declaration";
                Session["GlbReportName"] = "custom_workingsheetitm.rpt";
                string COM = Session["UserCompanyCode"] as string;
                string pc = Session["UserDefProf"] as string;
                string EntryNo = Session["RefDoc"] as string;
                clswharf obj = new clswharf();
                obj.CreateWorkingSheet(EntryNo, "TOT", txtFileNo.Text.ToString(), txtExporterName.Text.ToString());
                PrintPDF(targetFileName, obj._cus_workitm);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("custom_workingsheetitm", "Whlf", "Run Ok", Session["UserID"].ToString());
            }
        }

        //Dulaj 2018/Dec/18 Cusdec Approve 
        protected void lbtnapprove_Click(object sender, EventArgs e)
        {
            if (txtapprove.Value == "Yes")
            {
                try
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16136))
                    {
                        string msg = "Sorry, You have no permission to approve this! (Advice: Required permission code :16136)";
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);
                        return;
                    }
                    else
                    {
                        if (txtRefno.Text == "")
                        {
                            DisplayMessage("To bond number is empty", 2);
                            return;
                        }

                        Int32 effect = CHNLSVC.Inventory.UpdateCustDecHdr(Session["UserCompanyCode"].ToString(), txtRefno.Text, Session["UserID"].ToString(), Session["SessionID"].ToString());
                        if (effect > 0)
                        {
                            DisplayMessage("Successfully approved : " + txtRefno.Text, 3);
                            setStatus("A");
                            return;
                        }
                        else
                        {
                            DisplayMessage("Not Approved", 2);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

    }



}