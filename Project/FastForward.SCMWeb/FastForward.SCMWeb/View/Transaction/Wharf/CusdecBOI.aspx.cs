using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Imports;
using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class CusdecBOI : BasePage
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
        private List<mst_proc> BoiProcCode { get { return (List<mst_proc>)Session["mst_proc"]; } set { Session["mst_proc"] = value; } }
        private List<mst_proc_ele> BoiProcCodeele { get { return (List<mst_proc_ele>)Session["mst_proc_ele"]; } set { Session["mst_proc_ele"] = value; } }
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
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                    DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeader(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
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
                else if (lblvalue.Text == "ProcedureCode2")
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
                else if (lblvalue.Text == "Customer")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "Customer2")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
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
                if (lblSerValue.Text == "CusdecEntries")
                {
                    //ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                    DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeaderNew(SearchParams, ddlSearchby.Text, txtSearchbywordNew.Text.Trim(), 0, DateTime.Today, DateTime.Today);
                    dgvSearch.DataSource = result;
                    dgvSearch.DataBind();
                    ViewState["SEARCH"] = result;
                    serPopup.Show(); Session["IsSearchNew"] = true;
                }

            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session["IsSearchNew"] = null;
            serPopup.Hide();
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
                    }
                    oSelectItemLine = 0;
                }
                else if (lblvalue.Text == "ProcedureCode")
                {
                    txtProceCode.Text = code;
                    txtProceCode_TextChanged(null, null);
                }
                else if (lblvalue.Text == "ProcedureCode2")
                {
                    txtproccodenew.Text = code;
                    popubprocsetup.Show();
                    //txtProceCode_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Customer")
                {
                    txtBLno.Text = code;
                    //txtBLno_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Customer2")
                {
                    txtsupcode.Text = code;
                    popubprocsetup.Show();
                    //txtBLno_TextChanged(null, null);
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
            string _cusdecSubType = Request.QueryString["CUSTSTYPE"];
            //List<CustomsProcedureCodes> _custProcCds = CHNLSVC.Financial.GetCustomsProcedureCodes(oMstCom.Mc_anal19, Convert.ToString(Session["UserCompanyCode"]), string.Empty, string.Empty, _cusdecType, string.Empty);
            //if (_custProcCds == null || _custProcCds.Count <= 0)
            //{
            //    DisplayMessage("Procedure codes are not setup!", 1);
            //    return;
            //}
            //CustomsProcedureCodes _custProcCd = _custProcCds.FindAll(x => x.Mph_act == 1 && x.Mph_def == 1)[0];
            //if (_custProcCd == null)
            //{
            //    DisplayMessage("Procedure code default settings not setup!", 1);
            //    return;
            //}

            CusdecTypes _cusdecTypeInfor = CHNLSVC.Financial.GetCusdecTypeInfor(oMstCom.Mc_anal19).FindAll(y => y.Rcut_tp == _cusdecType)[0];
            if (_cusdecTypeInfor == null)
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

                //ddlLocOfGoods.Items.add(0, new ListItem("OTHER", "OTH"));
                //ddlLocOfGoods.SelectedItem.Text = _def01;
                ddlLocOfGoods.SelectedIndex = 0;
            }
           
            txtProceCode.Text = "";// _custProcCd.Mph_proc_cd;
            txtDec1.Text = "";// _custProcCd.Mph_decl_1;
            txtDec2.Text = "";// _custProcCd.Mph_decl_2;
            txtDec3.Text = "";// _custProcCd.Mph_decl_3;
            txtDocDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

            txtCurrencyOwn.Text = oMstCom.Mc_cur_cd;
            txtProceCode1.Text = "";// _custProcCd.Mph_print_1;
            txtProceCode2.Text = "";// _custProcCd.Mph_print_2;
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
            }

            if (_cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
            {
                txtMarksNumbers.ReadOnly = true;
                txtMarksNumbers.CssClass = "form-control";
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
            oCusdecCost = null;
            BoiProcCode = null;
            BoiProcCodeele = null;
            dgvCostItems.DataSource = new int[] { };
            dgvCostItems.DataBind();

            dgvContainers.DataSource = new int[] { };
            dgvContainers.DataBind();

            dgvDutyElements.DataSource = new int[] { };
            dgvDutyElements.DataBind();

            dgvItems.DataSource = new int[] { };
            dgvItems.DataBind();

            dgvHSHistory.DataSource = new int[] { };
            dgvHSHistory.DataBind();

            LoadDefaultValues();
            LoadPkgUOM();
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
                //string _cusdecType = Request.QueryString["CUSTTYPE"];
                //if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
                //{
                //    //ValidateTrue();
                //    ViewState["SEARCH"] = null;
                //    txtSearchbyword.Text = string.Empty;
                //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                //    DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, null, null);
                //    grdResult.DataSource = result;
                //    grdResult.DataBind();
                //    lblvalue.Text = "BLHeader";
                //    BindUCtrlDDLData(result);
                //    ViewState["SEARCH"] = result;
                //    mpUserPopup.Show(); Session["IsSearch"] = true;
                //}

                //if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                //{
                //    //ValidateTrue();
                //    ViewState["SEARCH"] = null;
                //    txtSearchbyword.Text = string.Empty;
                //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecReq);
                //    DataTable result = CHNLSVC.CommonSearch.SearchCusdecReq(SearchParams, null, null);
                //    grdResult.DataSource = result;
                //    grdResult.DataBind();
                //    lblvalue.Text = "CusdecReq";
                //    BindUCtrlDDLData(result);
                //    ViewState["SEARCH"] = result;
                //    mpUserPopup.Show(); Session["IsSearch"] = true;
                //}

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Customer";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpUserPopup.Show(); //Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();

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
                //ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeaderNew(SearchParams, null, null, 0, DateTime.Today, DateTime.Today);
                //result = result.AsEnumerable()
                //       .Where(r1 => r1.Field<string>("DOC NO").Contains("BOI")).OrderByDescending(r2 => r2.Field<string>("DOC NO"))
                //       .CopyToDataTable();
                dgvSearch.DataSource = result;
                dgvSearch.DataBind();
                lblSerValue.Text = "CusdecEntries";
                DataTable dt = new DataTable();
                dt.Columns.Add("DOC NO");
                dt.Columns.Add("OTH DOC");
                dt.Columns.Add("BL NO");
                dt.Columns.Add("STATUS");
                dt.Columns.Add("CUSTOMER");
                BindUCtrlDDLDataNew(dt);
                ViewState["SEARCH"] = result;
                txtSerToDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                txtSeFromDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                serPopup.Show(); Session["IsSearchNew"] = true;
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
                    if (_cusdecType == "BOI" || _cusdecType=="EXP")
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
                    //string _cusdecType = Request.QueryString["CUSTTYPE"];
                    //if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
                    //{
                    //    LoadBLDetails();
                    //}
                    //else if (_cusdecType == "EX" || _cusdecType == "RE" || _cusdecType == "EXP")
                    //{
                    //    LoadCusdecReqDetails();
                    //}
                    //else
                    //{
                    //    DisplayMessage("Invalid BL No!", 2);
                    //    txtBLno.Focus();
                    //}

                    //lblTobondNo.Text = txtCustomer.Text.ToUpper();
                    //LoadCustomerDetailsByCustomer();


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
                    if (!String.IsNullOrEmpty(txtValue1.Text))
                    {
                        decimal _value1 = 0;
                        if (decimal.TryParse(txtValue1.Text.ToString(), out _value1))
                        {
                            Int32 lineNo;
                            if (Int32.TryParse(txtLineNo.Text.ToString(), out lineNo))
                            {
                                ImpCusdecItm oEditedItem = oCusdecItm.Find(x => x.Cui_line == lineNo);
                                if (chkIgnoreCountry.Checked == true)
                                {
                                    //oEditedItem.Cui_def_cnty = "DEF";
                                    oEditedItem.Cui_def_cnty = txtCtryOriginItem.Text.ToString();

                                }
                                string preff = txtPreference.Text.ToString();
                                oEditedItem.Cui_unit_rt = _value1 / oEditedItem.Cui_qty;
                                oEditedItem.Cui_unit_amt = _value1;
                                oEditedItem.Cui_net_mass = Convert.ToDecimal(txtNetMassItem.Text);
                                oEditedItem.Cui_gross_mass = Convert.ToDecimal(txtGrossMassItem.Text);
                                oEditedItem.Cui_anal_3 = txtPreviousDocument.Text;
                                txtNetMassItem.Text = string.Format("{0:#,##0.00}", double.Parse(txtNetMassItem.Text));
                                txtGrossMassItem.Text = string.Format("{0:#,##0.00}", double.Parse(txtGrossMassItem.Text));
                                CHNLSVC.Financial.UpdateBLItemHS(txtBLno.Text.ToString(), lineNo, txtHSCode.Text.ToString(), txtGrossMassItem.Text.ToString(), txtNetMassItem.Text.ToString(), preff, txtRefno.Text.ToString());
                                BindItems();
                                decimal _totCost = oCusdecItm.Sum(x => x.Cui_unit_amt);
                                oCusdecCost.Where(w => w.Cus_ele_cd == "COST").ToList().ForEach(s => s.Cus_amt = _totCost);
                                txtInvoiceAmt.Text = _totCost.ToString("N2");
                                txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                                txtValueDetails.Text = Convert.ToString(_totCost * Convert.ToDecimal(txtExRate.Text.ToString()));
                                txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));
                                oCusdecHdr.CUH_TOT_AMT = Math.Round(_totCost, 2);
                                BindCostItems();
                                DisplayMessage("Item updated!", 3);
                            }
                            else
                            {
                                DisplayMessage("Invalid Item Line No", 2);
                            }
                        }
                        else
                        {
                            DisplayMessage("Invalid Value 1", 2);
                        }

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
            //try

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
                oCusdecHdr.CUH_COM_CHG = Convert.ToDecimal(txtComChgAmount.Text.ToString());
                oCusdecHdr.CUH_EX_RT = Convert.ToDecimal(txtExRate.Text.ToString());
                oCusdecHdr.CUH_STUS = "A";
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
                oCusdecHdr.CUH_MARKS_AND_NO = txtMarksAndNos.Text.ToString();
                oCusdecHdr.CUH_FILE_NO = txtFileNo.Text.ToString();
                if (string.IsNullOrEmpty(txtItems.Text)) txtItems.Text = "0";
                oCusdecHdr.CUH_ITEMS_QTY = Convert.ToInt32(txtItems.Text.ToString());
                oCusdecHdr.CUH_NATURE_OF_TRANCE = txtNatuOfTranst.Text.ToString();
                oCusdecHdr.CUH_LISION_NO = txtLicenceNo.Text.ToString();
                oCusdecHdr.CUH_DECL_SEQ_NO = txtDecSeqNo.Text.ToString();
                oCusdecHdr.CUH_PPC_NO = txtFinSettlement.Text.ToString();
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
                oCusdecHdr.CUH_ACC_NO = txtAcNo.Text.ToString();
                oCusdecHdr.CUH_CONTAINER_FCL = txtContainerFCL.Text.ToString();
                oCusdecHdr.CUH_FCL = txtFCL.Text.ToString();
                oCusdecHdr.CUH_RMK = txtDesc.Text;
                oCusdecHdr.CUH_CUSDEC_ENTRY_NO = txtCusdecEntryNo.Text;
                oCusdecHdr.CUH_DOC_REC_DT = Convert.ToDateTime(txtDocRecDate.Text);
                oCusdecHdr.CUH_CUSTOM_LC_TP = txtTermsOfPaymentCustom.Text.ToString();
                oCusdecHdr.CUH_PAGE_1 = txtProceCode1.Text.ToString();
                oCusdecHdr.CUH_PAGE_2 = txtProceCode2.Text.ToString();
                oCusdecHdr.CUH_INSU_TEXT = ddlOfficeOfEntryIn.SelectedItem.Text.ToString();
                oCusdecHdr.CUH_DELIVERY_TERMS = "CIF";
                oCusdecHdr.CUH_CUSTOM_LC_TP = txtTermsOfPaymentCustom.Text;
                oCusdecHdr.CUH_BL_NO = txtPreviousDocument.Text.ToString();
                oCusdecHdr.CUH_CUS_ENTRY_DT = Convert.ToDateTime(txtDocRecDate.Text);
                oCusdecHdr.CUH_TOT_PKG = txtTotalPackages.Text.ToString();

                string doc = "";
                _result = CHNLSVC.Financial.SaveCusdec(oCusdecHdr, oCusdecItm, oCusdecCost, oIsUpdate, out _returnMsg, 1, out doc, true,false);
                if (_result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + _returnMsg + "');window.location.href ='CusdecBOI.aspx?CUSTTYPE=" + Request.QueryString["CUSTTYPE"] + "&CUSTSTYPE=" + Request.QueryString["CUSTSTYPE"] + "';", true);
                    //DisplayMessage("aDOO SAVE UNA!!!", 5);
                    // txtRefno.Text = doc;
                    //  PrintResultsInvoice(doc);
                }
                else
                {
                    DisplayMessage(_returnMsg, 4);
                }
            }
        }

        protected void PrintResultsInvoice(string bond)
        {
            try
            {
                if (!string.IsNullOrEmpty(bond))
                {
                    Session["bondnum"] = bond;
                    if (bond != "")
                    {
                        lblMssg.Text = "Successfully Saved " + bond + " Do you want to print now?";
                        Session["print_type"] = "bond";
                        PopupConfBox.Show();
                    }
                    else
                    {
                        DisplayMessage("Error", 4);
                    }


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
                string bond = Session["bondnum"].ToString();
                Session["RefDoc"] = bond;
                lbtnboiprint_Click(null, null);

            }

        }

        protected void btnalertNo_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void chkHSHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHSHistory.Checked == true)
            { pnlDutyElements.Visible = false; pnlHSHistory.Visible = true; }
            else
            { pnlDutyElements.Visible = true; pnlHSHistory.Visible = false; }
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
            BindCostItems();
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
        private void BindCostItems()
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
                txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                txtValueDetails.Text = Convert.ToString(oTotalValue * Convert.ToDecimal(txtExRate.Text.ToString()));
                txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));
                oCusdecHdr.CUH_TOT_AMT = Math.Round(oTotalValue, 2);
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

            grdreservations.DataSource = new int[] { };
            grdreservations.DataBind();
            //if (Session["oCusdecItm"] != null)
            //{
            //    oCusdecItm = (List<ImpCusdecItm>)Session["oCusdecItm"];
            //    dgvItems.DataSource = oCusdecItm;
            //}
            //else
            //{
            //    dgvItems.DataSource = new int[] { };
            //}
            dgvItems.DataSource = oCusdecItm;
            dgvItems.DataBind();

            var result = oCusdecItm.GroupBy(test => test.Cui_anal_2)
                   .Select(grp => grp.First())
                   .ToList();
            grdreservations.DataSource = result;
            grdreservations.DataBind();
        }


        protected void btnAddHS_Click(object sender, EventArgs e)
        {
            try
            {
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "TO" || _cusdecType == "LR" || _cusdecType == "MV" || _cusdecType == "AIR")
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
                        txtPreference.Text = oSelectItem.Cui_preferance;
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
                        ; txtItemPrice.Text = _itemPrice.ToString();
                        txtItemPrice.Text = string.Format("{0:#,##0.00}", double.Parse(txtItemPrice.Text));
                        decimal _ncy = Convert.ToDecimal(Math.Round(_itemPrice, 2)) * Convert.ToDecimal(txtExRate.Text.ToString());
                        _ncy = Math.Round(_ncy, 2);
                        txtValueNCY.Text = _ncy.ToString();
                        txtValueNCY.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueNCY.Text));

                        decimal _preTot = 0;
                        decimal _actTot = 0;
                        oCusdecHdr.CUH_DT = DateTime.Now.Date;
                        List<HsCode> _hsDuty = CHNLSVC.Financial.GetHSDutyCalculation(true, Convert.ToString(Session["UserCompanyCode"]), oCusdecHdr.CUH_DT.Date, _cusdecType, "C", oCusdecHdr.CUH_CONSI_CD, oCusdecHdr.CUH_PROC_CD, Request.QueryString["CUSTTYPE"], oCusdecHdr.CUH_CNTY, 0, _ncy, oSelectItem.Cui_itm_cd, oSelectItem.Cui_hs_cd, oSelectItem.Cui_qty, oSelectItem.Cui_net_mass, oSelectItem.Cui_def_cnty, out _actTot, out _preTot);
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
                if (_cusdecType == "BOI" || _cusdecType == "EXP")
                {
                    if (oCusdecItm != null && oCusdecItm.Count > 0)
                    {
                        CusdecTypes _cusdecTypeInfor = CHNLSVC.Financial.GetCusdecTypeInfor(oMstCom.Mc_anal19).FindAll(y => y.Rcut_tp == _cusdecType)[0];
                        Label lblCui_line = dgvItems.SelectedRow.FindControl("lblCui_line") as Label;
                        Label lblCui_orgin_cnty = dgvItems.SelectedRow.FindControl("lblCui_orgin_cnty") as Label;
                        Label lblCui_qty = dgvItems.SelectedRow.FindControl("lblCui_qty") as Label;
                        Label lblCui_unit_rt = dgvItems.SelectedRow.FindControl("lblCui_unit_rt") as Label;
                        //Add by Lakshan
                        txtCtryOriginItem.Text = "";
                        if (lblCui_orgin_cnty != null)
                        {
                            txtCtryOriginItem.Text = lblCui_orgin_cnty.Text;
                        }
                        //
                        oSelectItemLine = Convert.ToInt32(lblCui_line.Text.ToString());
                        ImpCusdecItm oSelectItem = oCusdecItm.Find(x => x.Cui_line == oSelectItemLine);
                        txtHSCode.Text = oSelectItem.Cui_hs_cd;
                        txtLineNo.Text = oSelectItemLine.ToString();
                        txtItemCode.Text = oSelectItem.Cui_itm_cd;
                        txtItemDesc.Text = oSelectItem.Cui_itm_desc;
                        txtGrossMassItem.Text = oSelectItem.Cui_gross_mass.ToString();
                        txtNetMassItem.Text = oSelectItem.Cui_net_mass.ToString();
                        txtPreviousDocument.Text = oSelectItem.Cui_anal_3;
                        txtQuota.Text = oSelectItem.Cui_quota;
                        txtPreference.Text = oSelectItem.Cui_preferance;
                        txtUOM1.Text = "NOS";
                        txtQty1.Text = oSelectItem.Cui_qty.ToString();
                        txtItemPrice.Text = oSelectItem.Cui_itm_price.ToString();
                        txtItemPrice.Text = string.Format("{0:#,##0.00}", double.Parse(txtItemPrice.Text));
                        txtValueNCY.Text = "0";
                        txtMarksAndNos.Text = oSelectItem.Cui_anal_3.ToString();
                        txtrealunitprice.Text = lblCui_unit_rt.Text.ToString();
                        txtitemsqty.Text = lblCui_qty.Text.ToString();
                        txtitemsqty.ReadOnly = true; ;
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
                        decimal _ncy = Convert.ToDecimal(Math.Round(_itemPrice, 2)) * Convert.ToDecimal(txtExRate.Text.ToString());
                        _ncy = Math.Round(_ncy, 2);
                        txtValueNCY.Text = _ncy.ToString();
                        txtValueNCY.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueNCY.Text));
                        txtValue1.Text = txtItemPrice.Text;

                        if (chkHSHistory.Checked == false)
                        {
                            pnlDutyElements.Visible = true;
                            pnlHSHistory.Visible = false;

                            decimal _preTot = 0;
                            decimal _actTot = 0;
                            oCusdecHdr.CUH_DT = DateTime.Now.Date;
                            List<HsCode> _hsDuty = new List<HsCode>();

                            if (chkCurrentDuty.Checked == true)
                            {
                                if (oSelectItem.Cui_def_cnty == "") oSelectItem.Cui_def_cnty = "DEF";
                                _hsDuty = CHNLSVC.Financial.GetHSDutyCalculation(true, Convert.ToString(Session["UserCompanyCode"]), oCusdecHdr.CUH_DT.Date, "C", oCusdecHdr.CUH_CONSI_CD, oCusdecHdr.CUH_PROC_CD, _cusdecType, "ALL", oCusdecHdr.CUH_CNTY, _cusdecTypeInfor.Rcuit_duty_mp, _ncy, oSelectItem.Cui_itm_cd, oSelectItem.Cui_hs_cd, oSelectItem.Cui_qty, oSelectItem.Cui_net_mass, oSelectItem.Cui_def_cnty, out _actTot, out _preTot);
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
                else if (_cusdecType == "BOI") oCusdecHdr.CUH_TP = "BOI";
                else if (_cusdecType == "EXP") oCusdecHdr.CUH_TP = "EXP";

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
                // txtPreviousDocument.Text = oC.ToString();
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

        private void LoadCusdecReqDetails(List<string> _reqList)
        {
            string _msg = string.Empty;
            ImpCusdecHdr oCusdecHdr0 = new ImpCusdecHdr();
            List<ImpCusdecItm> oCusdecItm0 = new List<ImpCusdecItm>();
            List<ImpCusdecItmCost> oCusdecItmCost0 = new List<ImpCusdecItmCost>();
            List<ImpCusdecCost> oCusdecCost0 = new List<ImpCusdecCost>();
            List<ImportsBLContainer> oCusdecContainer0 = new List<ImportsBLContainer>();

            // + Request.QueryString["CUSTTYPE"].ToString()
            int _custStatus = CHNLSVC.Financial.GetBOICusdecReqData(Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19.ToString(), Request.QueryString["CUSTTYPE"].ToString(), txtBLno.Text.ToString().ToUpper(), _reqList, out oCusdecHdr0, out oCusdecItm0, out oCusdecItmCost0, out oCusdecCost0, out oCusdecContainer0, out _msg);
            if (_custStatus == 1)
            {
                // oCusdecCost0 = oCusdecCost0.Take(1).ToList();

                if (Request.QueryString["CUSTTYPE"]=="BOI")
                {
                    oCusdecHdr0.CUH_VESSEL = "Local sales";
                    oCusdecHdr0.CUH_VOYAGE = "Local sales";
                    oCusdecHdr0.CUH_VOYAGE_DT = DateTime.Now.Date;
                    oCusdecHdr0.CUH_CONTAINER_FCL = "-";
                    oCusdecHdr0.CUH_PLACE_OF_LOADING = "-";
                    oCusdecHdr0.CUH_CNTY_OF_EXPORT = "LO";
                    oCusdecHdr0.CUH_EXP_CNTY_NAME = "Local sales";
                }
                oCusdecHdr = oCusdecHdr0;
                oCusdecItm = oCusdecItm0;
                if (oCusdecItm0 != null && oCusdecItm0.Count>0)
                {
                    decimal totpack = oCusdecItm0.Sum(a => a.Cui_qty);
                    txtTotalPackages.Text = totpack.ToString();
                    
                }
                oCusdecItmCost = oCusdecItmCost0;
                oCusdecCost = oCusdecCost0.OrderBy(x => x.Cus_line).ToList();
                oCusdecContainer = oCusdecContainer0;

                chkCurrentDuty.Checked = false;
                chkCurrentDuty.Visible = true;

                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "EX") oCusdecHdr.CUH_TP = "EX";
                if (_cusdecType == "RE") oCusdecHdr.CUH_TP = "RE";
                if (_cusdecType == "BOI") oCusdecHdr.CUH_TP = "BOI";
                if (_cusdecType == "EXP") oCusdecHdr.CUH_TP = "EXP";

                txtProceCode.Text = oCusdecHdr.CUH_PROC_CD.ToString();
                txtProceCode1.Text = oCusdecHdr.CUH_PROCE_CD_1.ToString();
                txtProceCode2.Text = oCusdecHdr.CUH_PROCE_CD_2.ToString();

                oCusdecHdr.CUH_DT = DateTime.Now.Date;
                txtDocDate.Text = oCusdecHdr.CUH_DT.Date.ToString("dd/MMM/yyyy");
                //txtBLno.Text = oCusdecHdr.CUH_OTH_NO;
                txtDocRecDate.Text = oCusdecHdr.CUH_DOC_REC_DT.Date.ToString("dd/MMM/yyyy");
                txtCusdecEntryNo.Text = "";

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
                txtModeOfTransAtBorder.ReadOnly = false;
                txtModeOfTransAtBorder.Text = "3";
                txtModeOfTransAtBorder.ReadOnly = true;
                txtLists.Text = "0";
                txtItems.Text = oCusdecHdr.CUH_ITEMS_QTY.ToString();
                //txtTotalPackages.Text = oCusdecHdr.CUH_TOT_PKG.ToString();
                ddlTotalPkgUOM.Text = "OT";
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
                txtTermsOfPayment.ReadOnly = false;
                txtDelTerms.Text = "CIF";
               // txtTermsOfPayment.ReadOnly = true;
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
                txtFinRefNo.Text = oCusdecHdr.CUH_SUPP_NAME;
                txtBankCode.ReadOnly = false;
                txtBankName.ReadOnly = false;
                txtBranch.ReadOnly = false;
                txtTermsOfPayment.ReadOnly = false;
                txtTermsOfPaymentCustom.ReadOnly = false;

                if (Request.QueryString["CUSTTYPE"] == "BOI")
                {
                    txtBankCode.Text = "9999";
                    txtBankName.Text = "Other banks (not registered)";
                    txtBranch.Text = "000";
                    txtTermsOfPayment.Text = "OA";
                    txtTermsOfPaymentCustom.Text = "70";
                }
                else
                {
                    txtBankCode.Text = "7010";
                    txtBankName.Text = "Bank Of Celon";
                    txtBranch.Text = "660";
                    txtTermsOfPayment.Text = "LC";
                    txtTermsOfPaymentCustom.Text = "61";
                }
              


                txtBankCode.ReadOnly = true;
                txtBankName.ReadOnly = true;
                txtBranch.ReadOnly = true;
              //  txtTermsOfPayment.ReadOnly = true;
                txtTermsOfPaymentCustom.ReadOnly = true;
                txtTotGrossMass.Text = oCusdecHdr.CUH_TOT_GROSS_MASS.ToString();
                txtTotNetMass.Text = oCusdecHdr.CUH_TOT_NET_MASS.ToString();
                txtMainHS.Text = oCusdecHdr.CUH_MAIN_HS.ToString();
                ddlOfficeOfEntryIn.Items.Clear();
                ddlOfficeOfEntry.Items.Clear();
                ddlLocOfGoods.Items.Clear();

                string offentryin = "";
                string offout = "";
                //CUSTORMER LOAD
                List<MasterBusinessOfficeEntry> _MstBusOffEntry = CHNLSVC.General.getCustomerOfficeofEntry(Session["UserCompanyCode"].ToString(), txtBLno.Text, "C");
                if (_MstBusOffEntry != null)
                {
                    offentryin = _MstBusOffEntry.Where(a => a._mbbo_direct == 1).First()._mbbo_off_cd;
                }
                else
                {
                    offentryin = "";
                }
                if (_MstBusOffEntry != null)
                {
                    offout = _MstBusOffEntry.Where(a => a._mbbo_direct == 0).First()._mbbo_off_cd;
                }
                else
                {
                    offout = "";
                }

                ddlOfficeOfEntryIn.Items.Add(offentryin.ToString());
                ddlOfficeOfEntryIn.SelectedItem.Text = offentryin.ToString();
                ddlOfficeOfEntry.Items.Add(offout.ToString());
                ddlOfficeOfEntry.SelectedItem.Text = offout.ToString();
                ddlLocOfGoods.Items.Add(new ListItem("OTHER", "OTH"));
                ddlLocOfGoods.Items.Add(new ListItem(oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString(), oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString()));
                // ddlLocOfGoods.SelectedItem.Text = oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString();
                //ddlLocOfGoods.SelectedIndex = ddlLocOfGoods.Items.IndexOf(ddlLocOfGoods.Items.FindByValue(oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString()));
                ddlLocOfGoods.SelectedIndex = 0;
                // ddlLocOfGoods.Items.Add(oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString());
                // ddlLocOfGoods.SelectedItem.Text = oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString();
                txtDesc.Text = oCusdecHdr.CUH_RMK;
                txtReference.Text = oCusdecHdr.CUH_REF_NO;
                txtContainerFCL.Text = oCusdecHdr.CUH_CONTAINER_FCL;
                txtFinSettlement.Text = txtExporterName.Text;
                txtFCL.Text = oCusdecHdr.CUH_FCL;

                if (Request.QueryString["CUSTTYPE"] == "BOI")
                {

                    txtLastConsi.Text = "LO";
                    txtLastConsiName.Text = "Local sales";
                    txtTradingCountry.Text = "LO";
                    txtTradingCountryName.Text = "Local sales";
                    txtTermsOfPaymentCustom.Text = "70";
                }
                else
                {
                    txtLastConsi.Text = oCusdecHdr.CUH_CONSI_CD;
                    txtLastConsiName.Text = oCusdecHdr.CUH_CONSI_NAME;
                    txtTradingCountry.Text = oCusdecHdr.CUH_TRADING_COUNTRY;
                    txtTradingCountryName.Text = oCusdecHdr.CUH_TRADING_COUNTRY;
                    txtTermsOfPaymentCustom.Text = "61";
                    txtModeOfTransAtBorder.Text = "1";
                }

                txtMarksAndNos.Text = oCusdecHdr.CUH_MARKS_AND_NO;
                txtNatuOfTranst.Text = oCusdecHdr.CUH_NATURE_OF_TRANCE;
                txtPreviousDocument.Text = oCusdecHdr.CUH_BL_NO;
        
                //txtMarksAndNos.Text = oCusdecHdr.CUH_MARKS_AND_NO.ToString();
                txtMarksNumbers.Text = oCusdecHdr.CUH_MARKS_AND_NO.ToString();

                BindCostItems();
                BindContainers();
                BindItems();

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
            txtDec1.Text = "I";
            txtDec2.Text = "M";
            txtDec3.Text = "7";

            string _msg = string.Empty;
            ImpCusdecHdr oCusdecHdr0 = new ImpCusdecHdr();
            List<ImpCusdecItm> oCusdecItm0 = new List<ImpCusdecItm>();
            List<ImpCusdecItmCost> oCusdecItmCost0 = new List<ImpCusdecItmCost>();
            List<ImpCusdecCost> oCusdecCost0 = new List<ImpCusdecCost>();
            List<ImportsBLContainer> oCusdecContainer0 = new List<ImportsBLContainer>();

            // + Request.QueryString["CUSTTYPE"].ToString()
            int _custStatus = CHNLSVC.Financial.GetCusdecData(Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19.ToString(), Request.QueryString["CUSTTYPE"].ToString(), txtRefno.Text.Trim(), out oCusdecHdr0, out oCusdecItm0, out oCusdecItmCost0, out oCusdecCost0, out oCusdecContainer0, out _msg);
            if (_custStatus == 1)
            {
                oCusdecHdr = oCusdecHdr0;
                oCusdecItm = oCusdecItm0;
                oCusdecItmCost = oCusdecItmCost0;
                oCusdecCost = oCusdecCost0;
                oCusdecContainer = oCusdecContainer0;

                chkCurrentDuty.Checked = false;
                chkCurrentDuty.Visible = true;

                txtProceCode.Text = oCusdecHdr.CUH_PROC_CD.ToString();
                txtProceCode1.Text = oCusdecHdr.CUH_PROCE_CD_1.ToString();
                txtProceCode2.Text = oCusdecHdr.CUH_PROCE_CD_2.ToString();

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

                txtProceCode1.Text = oCusdecHdr.CUH_PAGE_1;
                txtProceCode2.Text = oCusdecHdr.CUH_PAGE_2;
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
                ddlOfficeOfEntryIn.Items.Clear();
                ddlOfficeOfEntry.Items.Clear();
                ddlLocOfGoods.Items.Clear();
                string _cusdecType = Request.QueryString["CUSTTYPE"];
                if (_cusdecType == "BOI" || _cusdecType == "EXP")
                {
                    ddlOfficeOfEntry.Items.Add(oCusdecHdr.CUH_OFFICE_OF_ENTRY.ToString());
                    ddlOfficeOfEntryIn.Items.Add(oCusdecHdr.CUH_INSU_TEXT.ToString());
                    // ddlLocOfGoods.Items.Add(oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString());

                    ddlLocOfGoods.Items.Add(new ListItem("OTHER", "OTH"));
                    ddlLocOfGoods.Items.Add(new ListItem(oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString(), oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString()));

                }
                ddlOfficeOfEntry.SelectedItem.Text = oCusdecHdr.CUH_OFFICE_OF_ENTRY.ToString();
                //ddlLocOfGoods.SelectedItem.Text = oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString();
                //ddlLocOfGoods.SelectedIndex = ddlLocOfGoods.Items.IndexOf(ddlLocOfGoods.Items.FindByValue(oCusdecHdr.CUH_LOCATION_OF_GOODS.ToString()));
                ddlLocOfGoods.SelectedIndex = 0;
                txtFinSettlement.Text = oCusdecHdr.CUH_FIN_SETTLE_TEXT;
                txtDesc.Text = oCusdecHdr.CUH_RMK;
                txtReference.Text = oCusdecHdr.CUH_REF_NO;
                txtContainerFCL.Text = oCusdecHdr.CUH_CONTAINER_FCL;
                txtFCL.Text = oCusdecHdr.CUH_FCL;
                txtLastConsiName.Text = txtCrtyExport.Text;
                txtTradingCountry.Text = txtCrtyExportCode.Text;
                txtTradingCountry.Text = oCusdecHdr.CUH_CNTY_OF_EXPORT.ToString();
                txtTradingCountryName.Text = oCusdecHdr.CUH_EXP_CNTY_NAME.ToString();
                txtMarksAndNos.Text = oCusdecHdr.CUH_MARKS_AND_NO;
                txtNatuOfTranst.Text = oCusdecHdr.CUH_NATURE_OF_TRANCE;
                txtPreviousDocument.Text = oCusdecHdr.CUH_BL_NO;
                if (Request.QueryString["CUSTTYPE"] == "BOI")
                {

                    txtLastConsi.Text = "LO";
                    txtLastConsiName.Text = "Local sales";
                    txtTradingCountry.Text = "LO";
                    txtTradingCountryName.Text = "Local sales";
                    txtTermsOfPaymentCustom.Text = "70";
                }
                else
                {
                    txtLastConsi.Text = oCusdecHdr.CUH_CONSI_CD;
                    txtLastConsiName.Text = oCusdecHdr.CUH_CONSI_NAME;
                    txtTradingCountry.Text = oCusdecHdr.CUH_TRADING_COUNTRY;
                    txtTradingCountryName.Text = oCusdecHdr.CUH_TRADING_COUNTRY;
                    txtTermsOfPaymentCustom.Text = "61";
                    txtModeOfTransAtBorder.Text = "1";
                }


                txtFinSettlement.Text = oCusdecHdr.CUH_PPC_NO.ToString();
                txtFinRefNo.Text = txtRefno.Text;

                BindCostItems();
                BindContainers();
                BindItems();

                btnSave.Text = "Update";
                btnSave.CssClass = "floatRight";
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

                        oCusdecItm = CHNLSVC.Financial.UpdateHSUsingHistory(oCusdecItm, Convert.ToString(Session["UserCompanyCode"]), oMstCom.Mc_anal19, _cusdecType, txtBLno.Text.ToString(), Convert.ToDecimal(txtInvoiceAmt.Text), Convert.ToDecimal(txtTotNetMass.Text), Convert.ToDecimal(txtTotGrossMass.Text), txtRefno.Text.ToString());
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
                    txtInvoiceAmt.Text = string.Format("{0:#,##0.00}", double.Parse(txtInvoiceAmt.Text));
                    txtValueDetails.Text = Convert.ToString(oTotalValue * Convert.ToDecimal(txtExRate.Text.ToString()));
                    txtValueDetails.Text = string.Format("{0:#,##0.00}", double.Parse(txtValueDetails.Text));
                    oCusdecHdr.CUH_TOT_AMT = Math.Round(oTotalValue, 2);
                }
            }
        }

        protected void btnBOIRequest_Click(object sender, EventArgs e)
        {
            ViewState["BOIReq"] = null;
            DataTable dtReq = CHNLSVC.Financial.LoadBOIRequestByCustomer(Convert.ToString(Session["UserCompanyCode"]), txtBLno.Text.ToString(), Request.QueryString["CUSTTYPE"]);
            
            grdBOIRequests.DataSource = dtReq;
            ViewState["BOIReq"] = dtReq;
            grdBOIRequests.DataBind();
            mpBOIRequests.Show();
        }

        protected void btnReqClose_Click(object sender, EventArgs e)
        {

            mpBOIRequests.Hide();
        }

        protected void btnReqAdd_Click(object sender, EventArgs e)
        {
            bool _isSelect = false;
            List<string> _list = new List<string>();


            if (grdBOIRequests != null && grdBOIRequests.Rows.Count > 0)
            {
                for (int i = 0; i < grdBOIRequests.Rows.Count; i++)
                {
                    GridViewRow row = grdBOIRequests.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkReqNo")).Checked;
                    string reqno = ((Label)row.FindControl("itr_req_no")).Text.ToString();
                    if (isChecked)
                    {
                        _list.Add(reqno);
                        _isSelect = true;
                    }
                }
            }

            if (_isSelect == false)
            {
                DisplayMessage("Please select the request(s)!", 1);
                return;
            }
            else
            {
                //List<CustomsProcedureCodes> _custProcCds = CHNLSVC.Financial.GetCustomsProcedureCodes(oMstCom.Mc_anal19, Convert.ToString(Session["UserCompanyCode"]), "C", txtBLno.Text.ToString().ToUpper(), "BOI", string.Empty);
                //if (_custProcCds == null || _custProcCds.Count <= 0)
                //{
                //    DisplayMessage("Procedure codes are not setup!", 1);
                //    return;
                //}
                //CustomsProcedureCodes _custProcCd = _custProcCds.FindAll(x => x.Mph_act == 1 && x.Mph_def == 1)[0];
                //if (_custProcCd == null)
                //{
                //    DisplayMessage("Procedure code default settings not setup!", 1);
                //    return;
                //}
                //txtProceCode.Text = _custProcCd.Mph_proc_cd;
                //txtDec1.Text = _custProcCd.Mph_decl_1;
                //txtDec2.Text = _custProcCd.Mph_decl_2;
                //txtDec3.Text = _custProcCd.Mph_decl_3;
                //txtProceCode1.Text = _custProcCd.Mph_print_1.ToString();
                //txtProceCode2.Text = _custProcCd.Mph_print_2.ToString();

                txtDec1.Text = "I";
                txtDec2.Text = "M";
                txtDec3.Text = "7";

                LoadCusdecReqDetails(_list);
                mpBOIRequests.Hide();
            }

        }

        protected void dgvSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string code = dgvSearch.SelectedRow.Cells[1].Text;
                string desc = dgvSearch.SelectedRow.Cells[2].Text;
                Session["IsSearchNew"] = null;
                serPopup.Hide();

                if (lblSerValue.Text == "BLHeader" || lblSerValue.Text == "CusdecReq")
                {
                    txtBLno.Text = code;
                    txtBLno_TextChanged(null, null);
                }
                else if (lblSerValue.Text == "CusdecEntries")
                {
                    txtRefno.Text = code;
                    txtRefno_TextChanged(null, null);
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
                    if (lblSerValue.Text == "CusdecEntries")
                    {
                        //ValidateTrue();
                        ViewState["SEARCH"] = null;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CusdecEntries);
                        DataTable result = CHNLSVC.CommonSearch.SearchCusdecHeaderNew(SearchParams, null, null, 1, dtFrom, dtTo);
                        dgvSearch.DataSource = result;
                        dgvSearch.DataBind();
                        ViewState["SEARCH"] = result;
                        serPopup.Show(); Session["IsSearchNew"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
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

        protected void lbtnboiprint_Click(object sender, EventArgs e)
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

                    Session["GlbReportName"] = "CusDecEntryDetails.rpt";
                    BaseCls.GlbReportHeading = "CusDec Entry Details";
                    string COM = Session["UserCompanyCode"] as string;
                    string EntryNo = Session["RefDoc"] as string;
                    clsImports obj = new clsImports();

                    InvReportPara repotPara = new InvReportPara();
                    repotPara._GlbCusDecNo = EntryNo;
                    repotPara._GlbEntryType = Request.QueryString["CUSTTYPE"];
                    repotPara._GlbUserID = Session["UserID"].ToString();
                    obj.EntryDetails(repotPara);
                    PrintPDF(targetFileName, obj._cusDecEntryDetails);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog(Request.QueryString["CUSTTYPE"], "Whlf", "Run Ok", Session["UserID"].ToString());
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("BOI Print", "CusdecBOI", ex.Message, Session["UserID"].ToString());
            }
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
                    if (status == "C")
                    {
                        DisplayMessage("Selected document already cancelled!!", 1);
                        return;
                    }
                    if (entryno == "")
                    {
                        DisplayMessage("Please Enter Entry No!!", 1);
                        return;
                    }
                    if (custype == "BOI" || custype == "EXP")
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
                        int effect = CHNLSVC.Financial.CusdecEntryCancelation_exreboi(com, entryno, sino, user, oCusdecItm, "", "BOI", out err);
                        if (effect == -99)
                        {
                            DisplayMessage(err, 4);
                        }
                        else if (effect == -1)
                        {
                            DisplayMessage(err, 1);
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


            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
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
            txtConsigneeAddress.Text = "";

            txtExporterTin.Text = "";
            txtExporterCode.Text = "";
            txtExporterName.Text = "";
            txtExporterAddress.Text = "";

            txtDeclarantTin.Text = "";
            txtDeclarantCode.Text = "";
            txtDeclarantName.Text = "";
            txtDeclarantAddress.Text = "";

            txtPreviousDocument.Text = "";
            txtLists.Text = "0";
            txtItems.Text = "";
            txtTotalPackages.Text = "";
            txtVessleNo.Text = "";
            txtPlaceOfLoading.Text = "";
            txtVoyageNo.Text = "";
            txtCrtyExport.Text = "";
            txtCrtyExportCode.Text = "";
            txtCrtyDesti.Text = "";
            txtCrtyDestiCode.Text = "";
            txtCrtyOrigin.Text = "";
            txtCrtyOriginCode.Text = "";
            txtTermsOfPayment.Text = "";
            txtTermsOfPaymentCustom.Text = "";
            txtDelTerms.Text = "";
            txtCurrency.Text = "";
            txtInvoiceAmt.Text = "";
            txtInvoiceAmt.Text = "";
            txtExRate.Text = "";
            txtValueDetails.Text = "";
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

        protected void txtrealunitprice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string realprice = txtrealunitprice.Text.ToString();
                string qty = txtitemsqty.Text.ToString();

                decimal realval = Convert.ToDecimal(realprice) * Convert.ToDecimal(qty);
                txtValue1.Text = string.Format("{0:#,##0.00}", double.Parse(realval.ToString()));
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void lbtnconproc_Click(object sender, EventArgs e)
        {
            popubprocsetup.Show();
        }

        protected void lbtnprocsave_Click(object sender, EventArgs e)
        {
            try
            {
                List<mst_proc_ele> _ele_list = new List<mst_proc_ele>();
                if (txtsupcode.Text.ToString().Trim() == "")
                {
                    DisplayMessage("Please Select Suplier ", 2);
                    return;
                }

                foreach (GridViewRow row in grdeleproc.Rows)
                {
                    Label procd = row.FindControl("procd") as Label;
                    Label duty = row.FindControl("duty") as Label;
                    CheckBox mp = row.FindControl("mp") as CheckBox;
                    CheckBox act = row.FindControl("act") as CheckBox;

                    mst_proc_ele ob = new mst_proc_ele();

                    if (mp.Checked == true)
                    {
                        ob.Mphe_mp = 1;
                    }
                    else
                    {
                        ob.Mphe_mp = 0;
                    }

                    if (act.Checked == true)
                    {
                        ob.Mphe_act = 1;
                    }
                    else
                    {
                        ob.Mphe_act = 0;
                    }
                    ob.Mphe_cnty = "LK";
                    ob.Mphe_com = Session["UserCompanyCode"].ToString();
                    ob.Mphe_consin = txtsupcode.Text.ToString();
                    ob.Mphe_cost_cat = "CUSTM";
                    ob.Mphe_cost_ele = duty.Text.ToString();
                    ob.Mphe_cost_tp = "DUTY";
                    ob.Mphe_cre_by = Convert.ToString(Session["UserID"]);
                    ob.Mphe_cre_dt = DateTime.Now.Date;
                    ob.Mphe_cre_session = Convert.ToString(Session["SessionID"]);
                    ob.Mphe_doc_tp = Request.QueryString["CUSTTYPE"];
                    ob.Mphe_mod_by = Convert.ToString(Session["UserID"]);
                    ob.Mphe_mod_dt = DateTime.Now;
                    ob.Mphe_mod_session = Convert.ToString(Session["SessionID"]);
                    ob.Mphe_proc_cd = procd.Text.ToString();
                    _ele_list.Add(ob);
                }
                string err = "";
                int _effect = CHNLSVC.Financial.SaveProcElements(_ele_list, out err);
                if (_effect > 0)
                {
                    DisplayMessage("Successfully Saved", 3);
                }
                else
                {
                    DisplayMessage(err, 1);
                }


            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void txtsupcode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnsupsearch_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Customer2";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); //Session["SIPopup"] = "SIPopup";
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
        }

        protected void txtproccodenew_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnproccodenew_Click(object sender, EventArgs e)
        {
            //ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProcedureCodes);
            DataTable result = CHNLSVC.CommonSearch.SearchGetProcedureCode(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "ProcedureCode2";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void lbtnaddprocdata_Click(object sender, EventArgs e)
        {
            try
            {

                List<mst_proc> _proc_list = CHNLSVC.Financial.GetProcByConsignee(Session["UserCompanyCode"].ToString(), Request.QueryString["CUSTTYPE"], txtsupcode.Text.ToString());
                if (_proc_list != null)
                {
                    grdproccodes.DataSource = _proc_list;
                    grdproccodes.DataBind();
                    popubprocsetup.Show();

                    Session["mst_proc"] = _proc_list;
                }
                //else
                //{
                //    DisplayMessage("Please Setup Proc Code For :" + txtsupcode.Text.ToString(), 2);
                //    txtsupcode.Text = "";
                //}
                popubprocsetup.Show();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void lbtnaddnewproc_Click(object sender, EventArgs e)
        {
            try
            {
                List<mst_proc> _proc = Session["mst_proc"] as List<mst_proc>;
                if (_proc == null)
                {
                    _proc = new List<mst_proc>();
                }
                mst_proc ob = new mst_proc();
                ob.Mph_cogn_cd = txtsupcode.Text.ToString();
                ob.Mph_proc_cd = txtproccodenew.Text.ToString();
                _proc.Add(ob);
                grdproccodes.DataSource = _proc;
                grdproccodes.DataBind();
                popubprocsetup.Show();
                Session["mst_proc"] = _proc;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void btnprocdelete_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label proccd = (Label)row.FindControl("mph_proc_cd");
                string proc = proccd.Text.ToString();
                List<BOIProc> _produty = CHNLSVC.Financial.GetDutyProc(Session["UserCompanyCode"].ToString());
                if (_produty != null)
                {
                    List<mst_proc_ele> _proconduty = CHNLSVC.Financial.GetProcDutyByConsignee(Session["UserCompanyCode"].ToString(), proc, txtsupcode.Text.ToString());

                    foreach (var list in _produty)
                    {
                        if (_proconduty != null)
                        {
                            if (_proconduty.Count > 0)
                            {
                                var count = _proconduty.Where(a => a.Mphe_cost_ele == list.duty).Count();
                                if (count > 0)
                                {
                                    list.act = _proconduty.Where(a => a.Mphe_cost_ele == list.duty).First().Mphe_act;
                                    list.mp = _proconduty.Where(a => a.Mphe_cost_ele == list.duty).First().Mphe_mp;
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                    grdeleproc.DataSource = _produty;
                    grdeleproc.DataBind();
                    popubprocsetup.Show();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void btnaddcostele_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label proccd = (Label)row.FindControl("mph_proc_cd");
                string proc = proccd.Text.ToString();
                List<BOIProc> _produty = CHNLSVC.Financial.GetDutyProc(Session["UserCompanyCode"].ToString());
                if (_produty != null)
                {
                    List<mst_proc_ele> _proconduty = CHNLSVC.Financial.GetProcDutyByConsignee(Session["UserCompanyCode"].ToString(), proc, txtsupcode.Text.ToString());

                    foreach (var list in _produty)
                    {
                        if (_proconduty != null)
                        {
                            if (_proconduty.Count > 0)
                            {
                                var count = _proconduty.Where(a => a.Mphe_cost_ele == list.duty).Count();
                                if (count > 0)
                                {
                                    list.act = _proconduty.Where(a => a.Mphe_cost_ele == list.duty).First().Mphe_act;
                                    list.mp = _proconduty.Where(a => a.Mphe_cost_ele == list.duty).First().Mphe_mp;
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                        list.procd = proccd.Text.ToString();
                    }
                    grdeleproc.DataSource = _produty;
                    grdeleproc.DataBind();
                    popubprocsetup.Show();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }
        protected bool subProgram(string str)
        {
            if (str == "1")
                return true;
            else
                return false;
        }
    }
}