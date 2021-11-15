using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Imports.CostSheet
{
    public partial class CostSheet : Base
    {
        string _para = "";
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }
        bool _isNewItem
        {
            get { if (Session["_isNewItem"] != null) { return (bool)Session["_isNewItem"]; } else { return false; } }
            set { Session["_isNewItem"] = value; }
        }
        bool _showCostItem
        {
            get { if (Session["_showCostItem"] != null) { return (bool)Session["_showCostItem"]; } else { return false; } }
            set { Session["_showCostItem"] = value; }
        }
        private List<ImportsCostElement> oImportsCostElements;
        protected List<imp_cst_ele_ref> oimp_cst_ele_refs { get { return (List<imp_cst_ele_ref>)Session["oimp_cst_ele_refs"]; } set { Session["oimp_cst_ele_refs"] = value; } }
        Int32 ApplyCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            dbtnActualPost.Items[0].Attributes.CssStyle.Add("margin-right", "35px");
            ValidateTrue();
            if (!IsPostBack)
            {
                txtFrom.Text = Request[txtFrom.UniqueID];
                LoadStatus();
                clear();
                loadItemStatus();
                _serData = new DataTable();
                _serType = "";
                _serPopShow = false;
                _showCostItem = false;
                _isNewItem = false;
            }
            else
            {
                if (Request[txtFrom.UniqueID] != null)
                {
                    if (Request[txtFrom.UniqueID].Length > 0)
                    {
                        txtFrom.Text = Request[txtFrom.UniqueID];
                    }
                }
                if (_serPopShow)
                {
                    PopupSearch.Show();
                }
                else
                {
                    _serPopShow = false;
                    PopupSearch.Hide();
                }
                if (_showCostItem)
                {
                    mpAddCostItems.Show();
                    _showCostItem = true;
                }
                else
                {
                    _showCostItem = false;
                    mpAddCostItems.Hide();
                }
            }
        }

        #region Search

        protected void btnClose_Click(object sender, EventArgs e)
        {
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
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
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
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                if (lblvalue.Text == "TradeTerms")
                {
                    txtCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDescription.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                if (lblvalue.Text == "BLHeader")
                {
                    txtBlNumer.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "Container")
                {
                    txtContainerNo.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "LcNo")
                {
                    txtbndreff.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
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
                mpUserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }

        private void FilterData()
        {
            if (ViewState["SEARCH"] != null)
            {
                if (lblvalue.Text == "TradeTerms")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
                    DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show();
                }
                else if (lblvalue.Text == "BLHeader")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show();
                }
                else if (lblvalue.Text == "Container")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Container);
                    DataTable result = CHNLSVC.CommonSearch.GetContainerNo(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show();
                }
            }
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

        #endregion

        #region Methods
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PINO:
                    {
                        paramsText.Append(BaseCls.GlbPINo + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TradeTerms:
                    {
                        paramsText.Append("OTH" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentTerms:
                    {
                        paramsText.Append("IPM" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OrderPlanNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("BANK" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "A" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Declarant:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + 1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ContainerType:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LcNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "");
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void clear()
        {
            Session["oImpCusdecHdr"] = "";
            txtBlNumer.Text = "";
            txtContainerNo.Text = "";
            txtBond.Text = "";
            ddlStatus.SelectedIndex = 0;
            txtAdminteam.Text = "";
            txtFrom.Text = DateTime.Now.AddMonths(-3).ToString("dd/MMM/yyyy");
            txtTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtActualClearDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            bypasscusdec.Checked = false;
            chkAll.Checked = true;
            chkAll_CheckedChanged(null, null);

            txtTradeTerm.Text = "";
            txtSupplier.Text = "";
            txtSupplierDesc.Text = "";
            txtBLNumber.Text = "";
            txtBlDate.Text = "";
            txtBLRef.Text = "";
            txtETA.Text = "";
            txtTradeTerm.Text = "";
            txtActualBLClearDate.Text = "";
            txtCustDecEntry.Text = "";
            txtTotAct.Text = "";
            txtTotActOther.Text = "";
            txtwopal.Text = "";
            txtTotPost.Text = "";
            txtTotPostOther.Text = "";
            txtTotPre.Text = "";

            txtCode.Text = "";
            txtDescription.Text = "";
            txtActual.Text = "";
            txtPost.Text = "";

            txtEntryDate.Text = "";
            txtEntryNum.Text = "";

            dbtnActualPost.SelectedIndex = 0;

            dgvCostSheetHeader.DataSource = new int[] { };
            dgvCostSheetHeader.DataBind();

            dgvCostDetails.DataSource = new int[] { };
            dgvCostDetails.DataBind();

            dgvOtherCostDetails.DataSource = new int[] { };
            dgvOtherCostDetails.DataBind();

            dgvDuty.DataSource = new int[] { };
            dgvDuty.DataBind();

            dgvitems.DataSource = new int[] { };
            dgvitems.DataBind();

            dgvItemCosts.DataSource = new int[] { };
            dgvItemCosts.DataBind();

            //grdelesubcost
            grdelesubcost.DataSource = new int[] { };
            grdelesubcost.DataBind();

            Session["SelectedDoc"] = null;
            Session["oCostHeader"] = null;
            Session["oImportsCostElements"] = null;
            Session["oImportsCostHeader"] = null;
            Session["oImportsCostItems"] = null;
            Session["IMP_CST_ELEREF_DET"] = null;

            dbtnActualPost.Items[1].Text = "Post";
            btnSave.Enabled = true;
            btnApply.Enabled = true;

            txtActual.ReadOnly = false;
            txtPost.ReadOnly = false;
            ApplyCount = 0;
            Session["ApplyCount"] = ApplyCount;
            oimp_cst_ele_refs = new List<imp_cst_ele_ref>();

            btnSearch_Click(null, null);

            loadCarryTyps();

            ddlCaringType.SelectedIndex = 0;
            ddlModeofShipment.SelectedIndex = 0;

            chkAll.Checked = true;
            chkAll_CheckedChanged(null, null);
        }

        private void clearByHeader()
        {
            dbtnActualPost.Items[1].Text = "Post";
            btnSave.Enabled = true;
            btnApply.Enabled = true;

            dgvCostDetails.DataSource = new int[] { };
            txtTotAct.Text = "";
            txtTotActOther.Text = "";
            txtwopal.Text = "";
            txtTotPost.Text = "";
            txtTotPostOther.Text = "";
            txtTotPre.Text = "";

            dgvOtherCostDetails.DataSource = new int[] { };
            dgvOtherCostDetails.DataBind();

            dgvDuty.DataSource = new int[] { };
            dgvDuty.DataBind();

            dgvitems.DataSource = new int[] { };
            dgvitems.DataBind();

            dgvItemCosts.DataSource = new int[] { };
            dgvItemCosts.DataBind();

            Session["SelectedDoc"] = null;
            Session["oCostHeader"] = null;
            Session["oImportsCostHeader"] = null;
            Session["oImportsCostItems"] = null;
        }

        private void loadCostSheetHeaders()
        {
            string bypass = "";
            if (bypasscusdec.Checked == true)
            {
                bypass = "1";
            }

            List<ImportsBLContainer> _cont = new List<ImportsBLContainer>();
            if (txtContainerNo.Text.ToString() != "")
            {
                _cont = CHNLSVC.Financial.GetSIByContainer(txtContainerNo.Text.ToString());
            }
            List<ImportsCostHeader> oHeaders = new List<ImportsCostHeader>();
            if (txtBlNumer.Text.ToString() == "" && txtBond.Text.Trim() == "" && txtContainerNo.Text.ToString() == "")
            {
                oHeaders = CHNLSVC.Financial.GET_IMP_CST_HDR_FOR_CS(Session["UserCompanyCode"].ToString(), txtBlNumer.Text.Trim(), txtBond.Text.Trim(), ddlStatus.SelectedValue.ToString(), Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), bypass);
            }
            else if (txtBlNumer.Text.ToString() != "" || txtBond.Text.Trim() != "")
            {
                oHeaders = CHNLSVC.Financial.GET_IMP_CST_HDR_FOR_CS(Session["UserCompanyCode"].ToString(), txtBlNumer.Text.Trim(), txtBond.Text.Trim(), ddlStatus.SelectedValue.ToString(), Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), bypass);
            }
            else if (txtContainerNo.Text.ToString() != "")
            {
                if (_cont != null && _cont.Count > 0)
                {
                    oHeaders = CHNLSVC.Financial.GET_IMP_CST_HDR_FOR_CS(Session["UserCompanyCode"].ToString(), _cont.First().Ibc_doc_no, txtBond.Text.Trim(), ddlStatus.SelectedValue.ToString(), Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), bypass);
                }
            }
            if (oHeaders != null)
            {
                if (oHeaders.Count > 0)
                {
                    oHeaders = oHeaders.Distinct().ToList();
                }
            }
            if (oHeaders == null)
            {
                oHeaders = new List<ImportsCostHeader>();
            }

            if (ddlModeofShipment.SelectedIndex != 0)
            {
                oHeaders = oHeaders.FindAll(x => x.Ib_tos == ddlModeofShipment.SelectedValue.ToString());
            }
            if (ddlCaringType.SelectedIndex != 0)
            {
                oHeaders = oHeaders.FindAll(x => x.Ib_carry_tp == ddlCaringType.SelectedValue.ToString());
            }
            if (!chkAll.Checked)
            {
                oHeaders = oHeaders.FindAll(x => x.Ib_doc_clear_dt.Date == Convert.ToDateTime(txtActualClearDate.Text).Date);
            }
            //if (txtActualClearDate.Text != "" && txtActualClearDate.Text != "01/Jan/1901")
            if (txtActualClearDate.Text != "")
            {
                oHeaders = oHeaders.Where(x => x.Ib_doc_clear_dt == Convert.ToDateTime(txtActualClearDate.Text.ToString())).ToList();
            }
            if (txtbndreff.Text != "")
            {
                oHeaders = oHeaders.Where(x => x.Cuh_bank_ref_cd.Contains(txtbndreff.Text.ToString())).ToList();
            }
            //if (!string.IsNullOrEmpty(txtContainerNo.Text))
            //{
            //    oHeaders = oHeaders.Where(x => x.Tmp_cont_no.Contains(txtContainerNo.Text.Trim())).ToList();
            //}
            dgvCostSheetHeader.DataSource = new int[] { };
            if (oHeaders != null)
            {
                foreach (var newohdr in oHeaders)
                {
                    if (newohdr.Ich_is_grn == 1)
                    {
                        newohdr.Ich_anal_5 = "YES";
                    }
                    else
                    {
                        newohdr.Ich_anal_5 = "NO";
                    }
                }

                Session["oHeaders"] = oHeaders;
                dgvCostSheetHeader.DataSource = oHeaders;
            }
            dgvCostSheetHeader.DataBind();
        }

        private void LoadStatus()
        {

            List<ComboBoxObject> oItems = new List<ComboBoxObject>();
            ComboBoxObject o1 = new ComboBoxObject();
            o1.Text = "Pending Costing";
            o1.Value = "P";
            oItems.Add(o1);

            ComboBoxObject o2 = new ComboBoxObject();
            o2.Text = "Costing Complete";
            o2.Value = "A";
            oItems.Add(o2);

            ComboBoxObject o3 = new ComboBoxObject();
            o3.Text = "Finalize";
            o3.Value = "F";
            oItems.Add(o3);


            ddlStatus.DataSource = oItems;
            ddlStatus.DataValueField = "Value";
            ddlStatus.DataTextField = "Text";
            ddlStatus.DataBind();
        }

        private void loadCostElements(String BL)
        {
            List<ImportsCostElement> oImportsCostElements = CHNLSVC.Financial.GET_IMP_CST_ELE_BY_DOC(BL);
            ImportsCostHeader oImportsCostHeader = CHNLSVC.Financial.GET_IMP_CST_HDR_BY_DOC(BL, "I");
            ImportsBLHeader oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), BL, "A");
            oimp_cst_ele_refs = CHNLSVC.Financial.GET_IMP_CST_ELE_REF_BY_SEQ(oImportsCostHeader.Ich_seq_no);
            List<IMP_CST_ELEREF_DET> _subdata = CHNLSVC.Financial.GetCostEleSubDetailsAll(oImportsCostHeader.Ich_seq_no);
            Session["IMP_CST_ELEREF_DET"] = _subdata;
            Session["Loc"] = oHeader.Ib_loc_cd;
            if (oImportsCostHeader != null && oImportsCostHeader.Ich_doc_no != null)
            {
                if (oImportsCostHeader.Ich_pre == 1 && oImportsCostHeader.Ich_actl == 0)
                {
                    dbtnActualPost.SelectedIndex = 0;
                    txtPost.ReadOnly = true;
                    txtActual.ReadOnly = false;
                    dbtnActualPost.Enabled = false;
                }
                else if (oImportsCostHeader.Ich_pre == 1 && oImportsCostHeader.Ich_actl == 1 && oImportsCostHeader.Ich_finl == 0)
                {
                    dbtnActualPost.SelectedIndex = 1;
                    txtActual.ReadOnly = true;
                    txtPost.ReadOnly = false;
                    dbtnActualPost.Enabled = false;
                }
                else
                {
                    dbtnActualPost.Enabled = true;
                }

                if (oImportsCostHeader.Ich_pre == 1 && oImportsCostHeader.Ich_actl == 1 && oImportsCostHeader.Ich_finl == 1)
                {
                    dbtnActualPost.Items[1].Text = "Post Completed";
                    //btnSave.Enabled = false;
                    // btnApply.Enabled = false;
                    dbtnActualPost.SelectedIndex = 1;
                }
            }

            txtSupplier.Text = oHeader.Ib_supp_cd;
            txtFileNo.Text = oHeader.Ib_mfile_no;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, "CODE", txtSupplier.Text.Trim());
            if (result != null && result.Rows.Count > 0)
            {
                txtSupplierDesc.Text = result.Rows[0][1].ToString();
            }
            txtBLNumber.Text = BL;
            txtBlDate.Text = oHeader.Ib_bl_dt.ToString("dd/MMM/yyyy");
            //  txtBLRef.Text = oHeader.re;
            txtETA.Text = oHeader.Ib_eta.ToString("dd/MMM/yyyy");
            txtActualBLClearDate.Text = oHeader.Ib_doc_clear_dt.ToString("dd/MMM/yyyy");
            txtEntryNum.Text = oImportsCostHeader.Ich_ref_no;
            txtEntryNum.Text = oImportsCostHeader.Ich_ref_no;
            //  txtActualClearDate.Text = oHeader.Ib_doc_clear_dt.ToString("dd/MMM/yyyy");
            // txtCustDecEntry.Text = oHeader.Ib_cusdec_entryno;
            txtActualClearDate.Text = oHeader.Ib_act_eta.ToString("dd/MMM/yyyy");



            ImpCusdecHdr oImpCusdecHdr = CHNLSVC.Financial.GET_CUSTDEC_HDR_BY_DOC(BL);
            if (oImpCusdecHdr != null)
            {
                txtEntryDate.Text = oImpCusdecHdr.CUH_DT.ToString("dd/MMM/yyyy");
                txtCustDecEntry.Text = oImpCusdecHdr.CUH_CUSDEC_ENTRY_NO.ToString();
                txtcusdecentrydate.Text = oImpCusdecHdr.CUH_CUSDEC_ENTRY_DT.ToString("dd/MMM/yyyy");
            }
            txtBLRef.Text = oHeader.Ib_bl_ref_no.ToString();
            Session["oImpCusdecHdr"] = oImpCusdecHdr;
            if (oImportsCostElements != null && oImportsCostElements.Count > 0)
            {
                ImportsCostElement oCostEle = oImportsCostElements.Find(x => x.Icet_ele_cd == "COST");
                if (oCostEle != null)
                {
                    if (oCostEle.Icet_actl_rt == 0)
                    {
                        oCostEle.Icet_actl_rt = oCostEle.Icet_actl_rt;
                    }
                    if (oCostEle.Icet_finl_rt == 0)
                    {
                        oCostEle.Icet_finl_rt = oCostEle.Icet_finl_rt;
                    }
                    foreach (ImportsCostElement item in oImportsCostElements)
                    {
                        if (dbtnActualPost.SelectedIndex == 0)
                        {
                            if (item.Icet_actl_rt == 0)
                            {
                                if (item.Icet_actl_rt == 0)
                                {
                                    item.Icet_actl_rt = item.Icet_actl_rt;
                                }
                                if (item.Icet_finl_rt == 0)
                                {
                                    item.Icet_finl_rt = item.Icet_finl_rt;
                                }
                            }
                        }
                        if (dbtnActualPost.SelectedIndex == 1)
                        {
                            if (item.Icet_finl_rt == 1)
                            {
                                item.Icet_finl_rt = item.Icet_finl_rt;
                            }
                        }
                    }
                }




                Session["oImportsCostHeader"] = oImportsCostHeader;
                if (oImportsCostHeader != null)
                {
                    if (!(oImportsCostHeader.Ich_doc_no.Equals(string.Empty)))
                    {
                        BindRates(oImportsCostHeader.Ich_doc_no);
                    }
                }
                loadAllOtherCharges(oImportsCostElements);
                Session["oImportsCostElements"] = oImportsCostElements;
                dgvCostDetails.DataSource = oImportsCostElements;

                DataTable dtTemp = CHNLSVC.Financial.GET_TRADTERMDESC_BY_DOC(BL, "TOT");
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    txtTradeTerm.Text = dtTemp.Rows[0][0].ToString() + " [ " + dtTemp.Rows[0][1].ToString() + " ]";
                }
                else
                {
                    txtTradeTerm.Text = "";
                }

                BindCostElements();
                BindOtherCostElements();
                bindCostBrackValus();
                BindDutyCostElements();
            }
            else
            {
                dgvCostDetails.DataSource = new int[] { };
                dgvCostDetails.DataBind();
            }
        }

        private void BindRates(string docno)
        {
            DataTable para = CHNLSVC.MsgPortal.getCostingParms(docno);
            if (para != null && para.Rows.Count>0)
            {
                txtsgexrate.Text = para.Rows[0]["USDRate"].ToString();
                txtbuyingrt.Text = para.Rows[0]["Buy Rate"].ToString();
                txtFreightRate.Text = para.Rows[0]["Freight Rate"].ToString();
                txtRemarks.Text = para.Rows[0]["Remarks"].ToString();
                txtDemurrageDays.Text = para.Rows[0]["RentDays"].ToString();
                txtExRate.Text = para.Rows[0]["ExRate"].ToString();
            }
          
        }
        private void BindCostElements()
        {
            //dgvCostDetails.DataSource = new int[] { };
            //if (Session["oImportsCostElements"] != null)
            //{
            //    oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
            //    dgvCostDetails.DataSource = oImportsCostElements;
            //}
            //dgvCostDetails.DataBind();
            //calculateElementTotal();
            dgvCostDetails.DataSource = new int[] { };
            if (Session["oImportsCostElements"] != null)
            {
                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                if (oImportsCostElements.FindAll(x => x.Icet_ele_cat != "OTH").Count > 0)
                {
                    dgvCostDetails.DataSource = oImportsCostElements.FindAll(x => x.Icet_ele_cat == "TOT").OrderBy(x => x.Icet_ele_cd);// oImportsCostElements.FindAll(x => x.Icet_ele_cat != "OTH");
                }
            }

            dgvCostDetails.DataBind();
            calculateElementTotal();

            for (int i = 0; i < dgvCostDetails.Rows.Count; i++)
            {
                GridViewRow row = dgvCostDetails.Rows[i];
                Label lblicet_ele_cd = (Label)row.FindControl("lblicet_ele_cd");
                Label lblicet_pre_rt = (Label)row.FindControl("lblicet_pre_rt");
                Label lblicet_actl_rt = (Label)row.FindControl("lblicet_actl_rt");
                Label lblRateVal = (Label)row.FindControl("lblRateVal");

                if (lblicet_actl_rt != null)
                {
                    double _per = (Convert.ToDouble(lblicet_pre_rt.Text) - Convert.ToDouble(lblicet_actl_rt.Text)) / Convert.ToDouble(lblicet_pre_rt.Text) * 100;

                    if (_per > 1.5 || _per < -1.5)
                    {
                        lblicet_actl_rt.ForeColor = Color.Red;
                        string _tmpStr = "(" + _per.ToString("N2") + "%)";
                        //lblRateVal.Text = _tmpStr;
                    }
                    else
                    {
                        lblicet_actl_rt.ForeColor = Color.Black;
                        //lblRateVal.Text = "";
                    }
                }
                if (lblicet_actl_rt != null)
                {
                    double _per = (Convert.ToDouble(lblicet_pre_rt.Text) - Convert.ToDouble(lblicet_actl_rt.Text)) / Convert.ToDouble(lblicet_pre_rt.Text) * 100;

                    if (_per > 0 || _per < 0)
                    {
                        string _tmpStr = "(" + _per.ToString("N2") + "%)";
                        lblRateVal.Text = _tmpStr;
                    }
                    else
                    {
                        lblRateVal.Text = "";
                    }
                }
                //if (lblicet_ele_cd.Text == "COST")
                //{
                //    LinkButton lbtnedititem2 = (LinkButton)row.FindControl("lbtnedititem2");
                //    lbtnedititem2.Visible = false;
                //}
            }


        }

        private void BindOtherCostElements()
        {
            dgvOtherCostDetails.DataSource = new int[] { };
            if (Session["oImportsCostElements"] != null)
            {
                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                if (oImportsCostElements.FindAll(x => x.Icet_ele_cat == "OTH").Count > 0)
                {
                    foreach (var IODATA in oImportsCostElements)
                    {
                        if (IODATA.ORDERNO == null)
                        {
                            IODATA.ORDERNO = "999999";
                        }
                    }
                    // dgvOtherCostDetails.DataSource = oImportsCostElements.FindAll(x => x.Icet_ele_cat == "OTH").OrderBy(x=>x.ORDERNO);
                    oImportsCostElements = oImportsCostElements.FindAll(x => x.Icet_ele_cat == "OTH").ToList();
                    oImportsCostElements = oImportsCostElements.OrderBy(x => int.Parse(x.ORDERNO)).ToList();
                    dgvOtherCostDetails.DataSource = oImportsCostElements;
                }
            }
            dgvOtherCostDetails.DataBind();
            calculateOtherElementTotal();
        }

        private void BindDutyCostElements()
        {
            dgvOtherCostDetails.DataSource = new int[] { };
            if (Session["oImportsCostElements"] != null)
            {
                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                if (oImportsCostElements.FindAll(x => x.Icet_ele_cat == "CUSTM").Count > 0)
                {
                    dgvDuty.DataSource = oImportsCostElements.FindAll(x => x.Icet_ele_cat == "CUSTM");
                }
            }
            dgvDuty.DataBind();
            calculateOtherElementTotal();
        }

        private void ValidateTrue()
        {
            divWarning.Visible = false;
            lblWarning.Text = "";
            divSuccess.Visible = false;
            lblSuccess.Text = "";
            divAlert.Visible = false;
            lblAlert.Text = "";
        }

        private bool isdecimal(string txt)
        {
            decimal asdasd;
            return decimal.TryParse(txt, out asdasd);
        }

        private void calculateElementTotal()
        {
            if (Session["oImportsCostElements"] != null)
            {
                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];

                txtTotPre.Text = oImportsCostElements.Where(z => z.Icet_stus == 1 && z.Icet_ele_cat == "TOT").Sum(x => x.Icet_pre_rt).ToString("N2");
                txtTotAct.Text = oImportsCostElements.Where(z => z.Icet_stus == 1 && z.Icet_ele_cat == "TOT").Sum(x => x.Icet_actl_rt).ToString("N2");
                txtTotPost.Text = oImportsCostElements.Where(z => z.Icet_stus == 1 && z.Icet_ele_cat == "TOT").Sum(x => x.Icet_finl_rt).ToString("N2");

                txtwopal.Text = (Convert.ToInt64(oImportsCostElements.Where(z => z.Icet_stus == 1 && (z.Icet_ele_tp == "OTH" | z.Icet_ele_cat == "TOT")).Sum(x => x.Icet_actl_rt)) +
    Convert.ToInt64(oImportsCostElements.Where(z => z.Icet_stus == 1 && (z.Icet_ele_tp == "OTH" | z.Icet_ele_cat == "TOT")).Sum(x => x.Icet_finl_rt))).ToString("N2");
            }
        }

        private void calculateOtherElementTotal()
        {
            if (Session["oImportsCostElements"] != null)
            {
                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                txtTotActOther.Text = oImportsCostElements.Where(z => z.Icet_stus == 1 && z.Icet_ele_tp == "OTH").Sum(x => x.Icet_actl_rt).ToString("N2");
                txtTotPostOther.Text = oImportsCostElements.Where(z => z.Icet_stus == 1 && z.Icet_ele_tp == "OTH").Sum(x => x.Icet_finl_rt).ToString("N2");

                txtwopal.Text = (Convert.ToInt64(oImportsCostElements.Where(z => z.Icet_stus == 1 && (z.Icet_ele_tp == "OTH" | z.Icet_ele_cat == "TOT")).Sum(x => x.Icet_actl_rt)) +
                    Convert.ToInt64(oImportsCostElements.Where(z => z.Icet_stus == 1 && (z.Icet_ele_tp == "OTH" | z.Icet_ele_cat == "TOT")).Sum(x => x.Icet_finl_rt))).ToString("N2");
            }

            for (int i = 0; i < dgvOtherCostDetails.Rows.Count; i++)
            {
                GridViewRow row = (GridViewRow)dgvOtherCostDetails.Rows[i];
                Label lblIcet_stus = (Label)row.FindControl("lblIcet_stus");
                if (lblIcet_stus.Text == "0")
                {
                    row.Visible = false;
                }

            }
        }

        private void DispalyMessages(string msg, Int32 option)
        {
            msg = msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", "");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msg + "');", true);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "scroll", "scrollTop();", true);
        }

        private void clearOtherEntryLine()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            txtActual.Text = "";
            txtPost.Text = "";
            txtCode.Focus();
        }

        private Int32 savePurchaseOrder(out string PONumber)
        {
            PONumber = string.Empty;
            Int32 result = 0;
            string BLNumber = Session["SelectedDoc"].ToString();
            ImportsBLHeader oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), BLNumber, "A");

            PurchaseOrder _PurchaseOrder = new PurchaseOrder();
            _PurchaseOrder.Poh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "PO", 1, Session["UserCompanyCode"].ToString());
            _PurchaseOrder.Poh_tp = "I";
            ImpCusdecHdr _ImpCusdecHdr = Session["oImpCusdecHdr"] as ImpCusdecHdr;

            if (_ImpCusdecHdr == null)
            {
                _PurchaseOrder.Poh_sub_tp = "N";
            }
            else if (_ImpCusdecHdr.CUH_TP == "TO")
            {
                _PurchaseOrder.Poh_sub_tp = "T";
            }
            else
            {
                _PurchaseOrder.Poh_sub_tp = "N";
            }

            _PurchaseOrder.Poh_doc_no = BLNumber;
            _PurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
            _PurchaseOrder.Poh_ope = "IMPORTS";
            _PurchaseOrder.Poh_profit_cd = Session["UserDefProf"].ToString();
            _PurchaseOrder.Poh_dt = DateTime.Now.Date;
            _PurchaseOrder.Poh_ref = oHeader.Ib_ref_no;
            _PurchaseOrder.Poh_job_no = oHeader.Ib_bl_ref_no;
            _PurchaseOrder.Poh_job_no = "IMPORTS_PO";
            _PurchaseOrder.Poh_pay_term = "CASH";
            _PurchaseOrder.Poh_supp = oHeader.Ib_supp_cd;
            _PurchaseOrder.Poh_cur_cd = oHeader.Ib_cur_cd;
            _PurchaseOrder.Poh_ex_rt = oHeader.Ib_ex_rt;
            _PurchaseOrder.Poh_trans_term = "";
            _PurchaseOrder.Poh_port_of_orig = "";
            //_PurchaseOrder.Poh_cre_period = txtCreditPeriod.Text;
            //_PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(txtPoDate.Text).Year;
            //_PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(txtPoDate.Text).Month;
            //_PurchaseOrder.Poh_to_yer = Convert.ToDateTime(txtPoDate.Text).Year;
            //_PurchaseOrder.Poh_to_mon = Convert.ToDateTime(txtPoDate.Text).Month;
            _PurchaseOrder.Poh_preferd_eta = oHeader.Ib_eta;
            _PurchaseOrder.Poh_contain_kit = false;
            _PurchaseOrder.Poh_sent_to_vendor = false;
            _PurchaseOrder.Poh_sent_by = "";
            _PurchaseOrder.Poh_sent_via = "";
            _PurchaseOrder.Poh_sent_add = "";
            _PurchaseOrder.Poh_stus = "A";
            //_PurchaseOrder.Poh_remarks = txtRemarks.Text;
            _PurchaseOrder.Poh_sub_tot = oHeader.Ib_tot_bl_amt;
            _PurchaseOrder.Poh_tax_tot = 0;
            _PurchaseOrder.Poh_dis_rt = 0;
            _PurchaseOrder.Poh_dis_amt = 0;
            _PurchaseOrder.Poh_oth_tot = 0;
            _PurchaseOrder.Poh_tot = oHeader.Ib_tot_bl_amt; ;
            _PurchaseOrder.Poh_reprint = false;
            _PurchaseOrder.Poh_tax_chg = false;
            _PurchaseOrder.poh_is_conspo = 0;
            _PurchaseOrder.Poh_cre_by = Session["UserID"].ToString();

            List<PurchaseOrderDetail> oPurchaseOrderDetails = new List<PurchaseOrderDetail>();
            List<ImportsBLItems> oImportsBLItems = CHNLSVC.Financial.GET_BL_ITMS_BY_SEQ(oHeader.Ib_seq_no);
            if (oImportsBLItems != null && oImportsBLItems.Count > 0)
            {
                Int32 lineNum = 0;
                foreach (ImportsBLItems oImportsBLItem in oImportsBLItems)
                {
                    lineNum = lineNum + 1;
                    MasterItem oMasterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), oImportsBLItem.Ibi_itm_cd);
                    PurchaseOrderDetail oPurchaseOrderDetail = new PurchaseOrderDetail();

                    if (oMasterItem != null)
                    {
                        oPurchaseOrderDetail.Pod_item_desc = oMasterItem.Mi_shortdesc;
                        oPurchaseOrderDetail.Pod_itm_tp = oMasterItem.Mi_itm_tp;
                        oPurchaseOrderDetail.Pod_uom = oMasterItem.Mi_itm_uom;
                    }
                    //Get actual costing finalize value
                    oPurchaseOrderDetail.Pod_act_unit_price = oImportsBLItem.Ibi_pi_unit_rt;
                    DataTable _costVal = new DataTable();
                    _costVal = CHNLSVC.Financial.Get_PerItemCostVal(oHeader.Ib_doc_no, oImportsBLItem.Ibi_itm_cd, oImportsBLItem.Ibi_line);
                    if (_costVal != null && _costVal.Rows.Count > 0)
                    {
                        oPurchaseOrderDetail.Pod_act_unit_price = Convert.ToDecimal(_costVal.Rows[0]["Tot"]);
                    }

                    oPurchaseOrderDetail.Pod_dis_amt = 0;
                    oPurchaseOrderDetail.Pod_dis_rt = 0;
                    oPurchaseOrderDetail.Pod_grn_bal = 0;
                    oPurchaseOrderDetail.Pod_itm_cd = oImportsBLItem.Ibi_itm_cd;
                    oPurchaseOrderDetail.Pod_itm_stus = oImportsBLItem.Ibi_itm_stus;
                    oPurchaseOrderDetail.Pod_kit_itm_cd = string.Empty;
                    oPurchaseOrderDetail.Pod_kit_line_no = 0;
                    oPurchaseOrderDetail.Pod_lc_bal = oImportsBLItem.Ibi_bal_qty;
                    oPurchaseOrderDetail.Pod_line_amt = oImportsBLItem.Ibi_unit_rt * oImportsBLItem.Ibi_qty;
                    oPurchaseOrderDetail.Pod_line_no = oImportsBLItem.Ibi_line; //lineNum;
                    oPurchaseOrderDetail.Pod_line_tax = 0;
                    oPurchaseOrderDetail.Pod_line_val = oImportsBLItem.Ibi_unit_rt * oImportsBLItem.Ibi_qty;
                    oPurchaseOrderDetail.Pod_nbt = 0;
                    oPurchaseOrderDetail.Pod_nbt_before = 0;
                    oPurchaseOrderDetail.Pod_pi_bal = oImportsBLItem.Ibi_qty;
                    oPurchaseOrderDetail.Pod_qty = oImportsBLItem.Ibi_qty;
                    oPurchaseOrderDetail.Pod_grn_bal = oImportsBLItem.Ibi_qty;
                    oPurchaseOrderDetail.Pod_ref_no = string.Empty;
                    oPurchaseOrderDetail.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                    oPurchaseOrderDetail.Pod_si_bal = oImportsBLItem.Ibi_bal_qty;
                    oPurchaseOrderDetail.Pod_tot_tax_before = 0;
                    oPurchaseOrderDetail.Pod_unit_price = oImportsBLItem.Ibi_unit_rt;
                    oPurchaseOrderDetail.Pod_vat = 0;
                    oPurchaseOrderDetail.Pod_vat_before = 0;
                    oPurchaseOrderDetails.Add(oPurchaseOrderDetail);
                }

                List<PurchaseOrderDelivery> _NewPODel = new List<PurchaseOrderDelivery>();
                MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();

                string err;
                Int32 row_aff = CHNLSVC.Inventory.SavePurchaseOrderNew(_PurchaseOrder, oPurchaseOrderDetails, _NewPODel, _masterAutoNumber, out PONumber, out err);
                //Int32 row_aff = (Int32)CHNLSVC.Inventory.SaveNewPO(_PurchaseOrder, oPurchaseOrderDetails, _NewPODel, _masterAutoNumber, out PONumber);
                if (row_aff > 0)
                {
                    result = row_aff;
                }
            }

            return result;
        }

        private void loadCarryTyps()
        {
            List<ComboBoxObject> oItem = CHNLSVC.Financial.GET_REF_CARY_TYPE();
            ddlCaringType.DataSource = oItem;
            ddlCaringType.DataTextField = "Text";
            ddlCaringType.DataValueField = "Value";
            ddlCaringType.DataBind();
        }


        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtConfirmSave.Value == "Yes")
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16016))
                {
                    DispalyMessages("Sorry, You have no permission.( Advice: Required permission code : 16016) !!!", 3);
                    return;
                }

                ApplyCount = Convert.ToInt32(Session["ApplyCount"]);
                if (ApplyCount == 0)
                {
                    DispalyMessages("Please apply values before finalize", 3);
                    return;
                }
                ImpCusdecHdr oImpCusdecHdr = CHNLSVC.Financial.GET_CUSTDEC_HDR_BY_DOC(txtBLNumber.Text);
                if (oImpCusdecHdr != null)
                {
                    if (oImpCusdecHdr.CUH_CUSDEC_ENTRY_NO.ToString().Trim() == "")
                    {
                        DispalyMessages("Please Update Cusdec Entry No and Date", 3);
                        return;
                    }
                }
                string PONumber = string.Empty;
                string err = string.Empty;
                Int32 result = 0;
                if (Session["oImportsCostHeader"] != null)
                {
                    ImportsCostHeader oImportsCostHeader = (ImportsCostHeader)Session["oImportsCostHeader"];
                    if (dbtnActualPost.SelectedIndex == 0)
                    {
                        Int32 POResult = savePurchaseOrder(out PONumber);
                        if (POResult > 0)
                        {
                            result = CHNLSVC.Financial.UPDATE_IMP_CST_HDR_STAGE(1, 1, Session["UserID"].ToString(), oImportsCostHeader.Ich_seq_no, Session["UserCompanyCode"].ToString(), oImportsCostHeader.Ich_doc_no, DateTime.Now, out err);
                        }
                    }
                    else if (dbtnActualPost.SelectedIndex == 1)
                    {
                        result = CHNLSVC.Financial.UPDATE_IMP_CST_HDR_STAGE(2, 1, Session["UserID"].ToString(), oImportsCostHeader.Ich_seq_no, Session["UserCompanyCode"].ToString(), oImportsCostHeader.Ich_doc_no, DateTime.Now, out err);
                    }

                    if (result == 1 || result == 2)
                    {
                        if (!String.IsNullOrEmpty(PONumber))
                        {
                            DispalyMessages("Successfully saved. Generated Purchase order : " + PONumber, 2);
                        }
                        else
                        {
                            DispalyMessages("Successfully saved", 2);
                        }

                        ApplyCount = 0;
                        Session["ApplyCount"] = ApplyCount;
                    }
                    else
                    {
                        DispalyMessages("Err :" + err, 3);
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadCostSheetHeaders();
        }

        protected void lblClear_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value.ToString() == "Yes")
            {
                clear();
            }
        }

        protected void dgvCostSheetHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvCostSheetHeader.PageIndex = e.NewPageIndex;
                dgvCostSheetHeader.DataSource = null;
                dgvCostSheetHeader.DataSource = (List<ImportsCostHeader>)Session["oHeaders"];
                dgvCostSheetHeader.DataBind();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }

        protected void dgvCostSheetHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void lbtnedititem_Click(object Sender, EventArgs e)
        {
            //if (dbtnActualPost.SelectedIndex == -1)
            //{
            //    DispalyMessages("Please select Costing type(Actual/Post)", 1);
            //    return;
            //}

            //dbtnActualPost.Enabled = false;

            clearByHeader();

            for (int i = 0; i < dgvCostSheetHeader.Rows.Count; i++)
            {
                dgvCostSheetHeader.Rows[i].BackColor = Color.FromName("0");
            }
            GridViewRow row = (Sender as LinkButton).NamingContainer as GridViewRow;
            row.BackColor = Color.FromName("PALETURQUOISE");
            Label lblich_doc_no = (Label)row.FindControl("lblich_doc_no");
            Label lblIch_seq_no = (Label)row.FindControl("lblIch_seq_no");
            Session["SelectedDoc"] = lblich_doc_no.Text;

            List<ImportsCostHeader> oHeaders = (List<ImportsCostHeader>)Session["oHeaders"];
            ImportsCostHeader oCostHeader = oHeaders.Find(x => x.Ich_seq_no == Convert.ToInt32(lblIch_seq_no.Text) && x.Ich_doc_no == lblich_doc_no.Text);
            Session["oCostHeader"] = oCostHeader;
            loadCostElements(lblich_doc_no.Text);
        }

        protected void lblMsgClose_Click(object sender, EventArgs e)
        {

        }

        protected void dbtnActualPost_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnOtherEleSearch_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
            DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "TradeTerms";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show();
        }

        protected void btnAddOtherCost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                DispalyMessages("please enter other cost code.", 1);
                return;
            }
            if (dbtnActualPost.SelectedIndex == 0 && string.IsNullOrEmpty(txtActual.Text))
            {
                DispalyMessages("Please enter actual cost value", 1);
                return;
            }

            if (!string.IsNullOrEmpty(txtActual.Text) && !isdecimal(txtActual.Text))
            {
                DispalyMessages("Please enter valid actual value", 1);
                return;
            }
            if (!string.IsNullOrEmpty(txtPost.Text) && !isdecimal(txtPost.Text))
            {
                DispalyMessages("Please enter valid post value", 1);
                return;
            }


            if (dbtnActualPost.SelectedIndex == 1 && string.IsNullOrEmpty(txtPost.Text))
            {
                DispalyMessages("Please enter post cost value", 1);
                return;
            }
            if (Session["SelectedDoc"] == null)
            {
                DispalyMessages("Please select a document", 1);
                return;
            }

            if (Session["oImportsCostElements"] != null)
            {
                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];

                if (oImportsCostElements.FindAll(x => x.Icet_ele_cd == txtCode.Text.Trim() && x.Icet_ele_cat == "OTH" && x.Icet_ele_tp == "OTH" && x.Icet_stus == 1).Count > 0)
                {
                    DispalyMessages("Selected code is already added.", 1);
                    return;
                }

                ImportsCostElement oNewItem = new ImportsCostElement();
                oNewItem.Icet_seq_no = 0;
                oNewItem.Icet_line = 0;
                oNewItem.Icet_doc_no = Session["SelectedDoc"].ToString().Trim();
                oNewItem.Icet_stus = 1;
                oNewItem.Icet_ele_cat = "OTH";
                oNewItem.Icet_ele_tp = "OTH";
                oNewItem.Icet_ele_cd = txtCode.Text.Trim();
                oNewItem.Icet_ele_rt = 0;
                oNewItem.Icet_pre_rt = 0;
                oNewItem.Icet_actl_rt = (txtActual.Text == "") ? 0 : Convert.ToDecimal(txtActual.Text);
                oNewItem.Icet_finl_rt = (txtPost.Text == "") ? 0 : Convert.ToDecimal(txtPost.Text);
                oNewItem.Icet_anal_1 = string.Empty;
                oNewItem.Icet_anal_2 = string.Empty;
                oNewItem.Icet_anal_3 = string.Empty;
                oNewItem.Icet_anal_4 = string.Empty;
                oNewItem.Icet_anal_5 = string.Empty;
                oNewItem.Icet_cre_by = Session["UserID"].ToString();
                oNewItem.Icet_cre_dt = DateTime.Now;
                oNewItem.Icet_mod_by = Session["UserID"].ToString();
                oNewItem.Icet_mod_dt = DateTime.Now;
                oNewItem.Icet_session_id = Session["SessionID"].ToString();
                oNewItem.Icet_ele_cd_desc = txtDescription.Text;
                oNewItem.isNewRecord = 1;
                oImportsCostElements.Add(oNewItem);
                BindOtherCostElements();
                clearOtherEntryLine();

                BindDutyCostElements();
            }
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
                DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, "CODE", txtCode.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {
                    txtCode.Text = result.Rows[0]["CODE"].ToString();
                    txtDescription.Text = result.Rows[0][1].ToString();
                    txtActual.Focus();
                }
                else
                {
                    DispalyMessages("Please enter a correct other cost code", 1);
                    return;
                }
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (txtConfirmApply.Value.ToString() == "Yes")
            {
                if (Session["oCostHeader"] != null)
                {
                    ImportsCostHeader oHeaders = (ImportsCostHeader)Session["oCostHeader"];
                    List<ImportsCostItem> oCostItems = new List<ImportsCostItem>();
                    List<ImportsCostElementItem> oImportsCostElementItems2 = new List<ImportsCostElementItem>();

                    if (Session["oImportsCostElements"] != null)
                    {
                        oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                        string err = string.Empty;

                        //if (dbtnActualPost.SelectedIndex == 0)
                        //{
                        //    oHeaders.Ich_actl = 1;
                        //}
                        //else if (dbtnActualPost.SelectedIndex == 1)
                        //{
                        //    oHeaders.Ich_actl = 1;
                        //    oHeaders.Ich_finl = 1;
                        //}
                        string isact = "";
                        string ispost = "";
                        oHeaders.Ich_finl = 0;
                        oHeaders.Ich_actl = 0;
                        if (dbtnActualPost.SelectedValue == "Actual")
                        {
                            oHeaders.Ich_actl = 1;
                            isact = "1";

                        }
                        else
                        {
                            ispost = "1";
                            oHeaders.Ich_finl = 1;
                        }
                        if (oimp_cst_ele_refs == null)
                        {
                            oimp_cst_ele_refs = new List<imp_cst_ele_ref>();
                        }

                        oHeaders.Ich_anal_4 = txtExRate.Text.Trim().ToString();
                        oHeaders.Ich_anal_5 = txtDemurrageDays.Text.Trim().ToString();

                        //get cst hdr by doc

                        DataTable csthdr = CHNLSVC.Sales.GET_CST_DATA_BYDOC(oHeaders.Ich_doc_no);
                        if (csthdr.Rows.Count > 0)
                        {
                            string getact = csthdr.Rows[0]["ich_actl"].ToString();


                            string getpost = csthdr.Rows[0]["ich_finl"].ToString();

                            if (getact == "0" && ispost == "1")
                            {
                                DispalyMessages("Can't  Process Post First!!!", 1);
                                return;
                            }
                            //if (getpost == ispost)
                            //{
                            //    DispalyMessages("Already Done Post Process!!!", 1);
                            //    return;
                            //}



                        }

                        Int32 result = CHNLSVC.Financial.CostSheetApply(oHeaders, oCostItems, oImportsCostElements, oImportsCostElementItems2, Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Session["UserDefLoca"].ToString(), Session["SessionID"].ToString(), oimp_cst_ele_refs, out err);
                        if (result == 1)
                        {
                            ApplyCount = Convert.ToInt32(Session["ApplyCount"]);
                            ApplyCount = ApplyCount + 1;
                            Session["ApplyCount"] = ApplyCount;
                            DispalyMessages("Successfully saved", 2);
                            loadCostElements(Session["SelectedDoc"].ToString());
                        }
                        else if (result == -1)
                        {
                            DispalyMessages("Err :" + err, 3);
                        }
                    }
                    else
                    {
                        DispalyMessages("Please select a document", 1);
                    }
                }
                else
                {
                    DispalyMessages("Please select a document", 1);
                }
            }
        }

        protected void btnViewItemDetials_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["oImportsCostHeader"] != null)
                {
                    ImportsCostHeader oImportsCostHeader = (ImportsCostHeader)Session["oImportsCostHeader"];
                    List<ImportsCostItem> oImportsCostItems = CHNLSVC.Financial.GET_IMP_CST_ITM_BY_SEQ(oImportsCostHeader.Ich_seq_no, 1);
                    if (oImportsCostItems != null)
                    {
                        List<ImportsBLItems> oImportsBLItems = CHNLSVC.Financial.GET_BL_ITM_BY_DOC_ITM(oImportsCostHeader.Ich_doc_no, "", "");
                        foreach (ImportsCostItem item in oImportsCostItems)
                        {
                            item.Ici_anal_5 = getItemStatusDesc(item.Ici_itm_stus);
                            if (oImportsBLItems != null && oImportsBLItems.Count > 0 && oImportsBLItems.FindAll(x => x.Ibi_itm_cd == item.Ici_itm_cd && x.Ibi_stus == item.Ici_stus).Count > 0)
                            {
                                item.Ref_FinNumber = oImportsBLItems.Find(x => x.Ibi_itm_cd == item.Ici_itm_cd && x.Ibi_stus == item.Ici_stus).Ibi_fin_no;
                                item.Ici_unit_rt = oImportsBLItems.Find(x => x.Ibi_itm_cd == item.Ici_itm_cd && x.Ibi_line == item.Ici_ref_line).Ibi_unit_rt;
                                item.Ici_unit_amt = item.Ici_unit_rt * item.Ici_qty;
                            }
                        }

                        Session["oImportsCostItems"] = oImportsCostItems;
                        dgvitems.DataSource = oImportsCostItems;
                        dgvitems.DataBind();

                        txtTotalActualAmount.Text = "";
                        txtTotalFinalAmount.Text = "";
                        txtTotalPreAmount.Text = "";
                        dbtnActualPost2.SelectedIndex = dbtnActualPost.SelectedIndex;
                        mpItemDetails.Show();
                    }
                    else
                    {
                        DispalyMessages("No items to show", 1);
                    }
                }
                else
                {
                    DispalyMessages("Please select the B/L document", 1);
                }
            }
            catch (Exception ex)
            {
                DispalyMessages(ex.Message, 1);
            }

        }

        protected void btnDgvItemSelect_Click(object Sender, EventArgs e)
        {
            GridViewRow row = (Sender as LinkButton).NamingContainer as GridViewRow;
            Label lblici_seq_no = (Label)row.FindControl("lblici_seq_no");
            Label lblici_itm_cd = (Label)row.FindControl("lblici_itm_cd");
            Label lblici_ref_line = (Label)row.FindControl("lblici_ref_line");

            for (int i = 0; i < dgvitems.Rows.Count; i++)
            {
                GridViewRow rowTemp = dgvitems.Rows[i];
                rowTemp.BackColor = Color.Transparent;
            }

            row.BackColor = Color.Cyan;

            List<ImportsCostElementItem> oImportsCostElementItems = CHNLSVC.Financial.GET_IMP_CST_ELE_ITM_BY_ITM(Convert.ToInt32(lblici_seq_no.Text), Session["SelectedDoc"].ToString(), lblici_itm_cd.Text.Trim());
            Session["oImportsCostElementItems"] = oImportsCostElementItems;
            dgvItemCosts.DataSource = oImportsCostElementItems.FindAll(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(lblici_ref_line.Text));
            dgvItemCosts.DataBind();
            mpItemDetails.Show();

            txtTotalPreAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1).Sum(z => z.Ice_pre_amnt).ToString("N2");
            txtTotalActualAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1).Sum(z => z.Ice_actl_amnt).ToString("N2");
            txtTotalFinalAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1).Sum(z => z.Ice_finl_amnt).ToString("N2");

            ImportsCostItem oPrivousItems = CHNLSVC.MsgPortal.GET_PREV_ITEM_COST_FOR_COSTSHEET(Session["UserCompanyCode"].ToString(), lblici_itm_cd.Text.Trim());
            if (oPrivousItems != null)
            {
                txtPreviousValues.Text = oPrivousItems.Ici_unit_amt.ToString("N2");
            }
            else
            {
                txtPreviousValues.Text = "0.00";
            }

            decimal RATE = ((Convert.ToDecimal(txtTotalActualAmount.Text) / 100) * 5);
            decimal Diff = Convert.ToDecimal(txtPreviousValues.Text) - Convert.ToDecimal(txtTotalActualAmount.Text);

            if (Diff != 0)
            {
                if ((RATE <= Diff) || (RATE >= Diff))
                {
                    txtTotalActualAmount.ForeColor = Color.Red;
                }
                else
                {
                    txtTotalActualAmount.ForeColor = Color.Black;
                }
            }
            else
            {
                txtTotalActualAmount.ForeColor = Color.Black;
            }

            //            If Diff <> "0" Then
            //    If (Val(RATE) <= Val(Diff)) Or (Val(RATE) >= Val(Diff)) Then
            //        txt_ActualItemCost.ForeColor = Color.Red
            //    Else
            //        txt_ActualItemCost.ForeColor = Color.Black
            //    End If
            //Else
            //    txt_ActualItemCost.ForeColor = Color.Black
            //End If

            //If txt_ActualItemCost.ForeColor <> Color.Red Then
            //    If Val(txt_ActualItemCost.Text) <> Val(txt_ActualItemCost.Text) Then
            //        txt_ActualItemCost.ForeColor = Color.Red
            //    Else
            //        txt_ActualItemCost.ForeColor = Color.Black
            //    End If
            //End If
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (txtConfirmReset.Value == "Yes")
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16017))
                {
                    DispalyMessages("Sorry, You have no permission to approve this order.( Advice: Required permission code : 16017) !!!", 3);
                    return;
                }

                if (Session["SelectedDoc"] == null || string.IsNullOrEmpty(Session["SelectedDoc"].ToString()))
                {
                    DispalyMessages("Please select the B/L document number", 3);
                    return;
                }

                string err;
                Int32 result = 0;
                ImportsCostHeader oImportsCostHeader = CHNLSVC.Financial.GET_IMP_CST_HDR_BY_DOC(Session["SelectedDoc"].ToString(), "I");


                if (oImportsCostHeader.Ich_is_grn == 1)
                {
                    if (dbtnActualPost.SelectedValue == "Actual")
                    {
                        DispalyMessages("GRN process is completed", 3);
                        return;
                    }

                }
                else
                {
                    if (oImportsCostHeader.Ich_actl == 1 && oImportsCostHeader.Ich_finl == 0)
                    {
                        PurchaseOrder oPurchaseOrder = CHNLSVC.Inventory.GetPOHeader(Session["UserCompanyCode"].ToString(), Session["SelectedDoc"].ToString(), "I");
                        if (oPurchaseOrder != null && oPurchaseOrder.Poh_doc_no != null)
                        {
                            result = CHNLSVC.Financial.UPDATE_PUR_HDR_BY_COSTSHEET(oPurchaseOrder.Poh_seq_no, "C", Session["UserID"].ToString(), out err);
                            if (result > 0)
                            {
                                result = CHNLSVC.Financial.UPDATE_IMP_CST_HDR_STAGE(1, 0, Session["UserID"].ToString(), oImportsCostHeader.Ich_seq_no, Session["UserCompanyCode"].ToString(), oImportsCostHeader.Ich_doc_no, DateTime.Now, out err);
                                if (result > 0)
                                {
                                    DispalyMessages("Actual cost reset succeeded.", 2);
                                    loadCostElements(Session["SelectedDoc"].ToString());
                                    return;
                                }
                            }
                        }
                    }
                    else if (oImportsCostHeader.Ich_actl == 1 && oImportsCostHeader.Ich_finl == 1)
                    {
                        result = CHNLSVC.Financial.UPDATE_IMP_CST_HDR_STAGE(2, 0, Session["UserID"].ToString(), oImportsCostHeader.Ich_seq_no, Session["UserCompanyCode"].ToString(), oImportsCostHeader.Ich_doc_no, DateTime.Now, out err);
                        if (result > 0)
                        {
                            DispalyMessages("Post cost reset succeeded.", 2);
                            loadCostElements(Session["SelectedDoc"].ToString());
                            return;
                        }
                    }
                    else
                    {
                        DispalyMessages("Cost sheet is in the pre stage.", 3);
                        return;
                    }
                }
            }
        }

        protected void btnDocNumber_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
            DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "BLHeader";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show();
        }

        #region Cost Element Grid

        protected void lbtnedititem2_Click(object sender, EventArgs e)
        {

        }

        protected void btndgvCostDetailsUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblicet_pre_rt = (Label)row.FindControl("lblicet_pre_rt");
            TextBox txticet_actl_rt = (TextBox)row.FindControl("txticet_actl_rt");
            TextBox txticet_finl_rt = (TextBox)row.FindControl("txticet_finl_rt");
            Label lblicet_ele_cd = (Label)row.FindControl("lblicet_ele_cd");
            if (Session["oImportsCostElements"] != null)
            {
                ImportsBLHeader oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), txtBLNumber.Text.ToString(), "A");
                if (oHeader != null)
                {
                    if (oHeader.Ib_anal_1 == "C&F" || oHeader.Ib_anal_1 == "CPT")
                    {
                        if (lblicet_ele_cd.Text.ToString() == "FRGT")
                        {
                            DispalyMessages("FREIGHT Value Block For C&F/CPT", 1);
                            // return;
                        }
                    }
                    if (oHeader.Ib_anal_1 == "CIF" || oHeader.Ib_anal_1 == "CIP")
                    {
                        if (lblicet_ele_cd.Text.ToString() == "FRGT" || lblicet_ele_cd.Text.ToString() == "INSU")
                        {
                            DispalyMessages("FREIGHT & INSURANCE  Block For CIF/CIP", 1);
                            // return;
                        }
                    }
                }


                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                //if (oImportsCostElements.FindAll(x => x.Icet_ele_cat != "OTH").Count > 0)
                //{
                //    var _filter = oImportsCostElements.FindAll(x => x.Icet_ele_cat == "TOT").ToList();// oImportsCostElements.FindAll(x => x.Icet_ele_cat != "OTH");
                //    oImportsCostElements = _filter;
                //} 

                ImportsCostElement oEditItem = oImportsCostElements.Find(x => x.Icet_ele_cd == lblicet_ele_cd.Text);
                if (dbtnActualPost.SelectedIndex == 0)
                {
                    if (isdecimal(txticet_actl_rt.Text))
                    {
                        oEditItem.Icet_actl_rt = Convert.ToDecimal(txticet_actl_rt.Text);
                    }
                    else
                    {
                        DispalyMessages("Please enter a valid actual cost value", 1);
                        return;
                    }
                }
                else if (dbtnActualPost.SelectedIndex == 1)
                {
                    if (isdecimal(txticet_finl_rt.Text))
                    {
                        oEditItem.Icet_finl_rt = Convert.ToDecimal(txticet_finl_rt.Text);
                    }
                    else
                    {
                        DispalyMessages("Please enter a valid post cost value", 1);
                        return;
                    }
                }
                Session["oImportsCostElements"] = oImportsCostElements;
                dgvCostDetails.EditIndex = -1;
                BindCostElements();
                BindCostElements();
            }
        }

        protected void btndgvCostDetailsCancel_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    dgvCostDetails.EditIndex = -1;
                    BindCostElements();
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

        protected void dgvCostDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvCostDetails.EditIndex = e.NewEditIndex;
            BindCostElements();

            int ActualOrPost = 0; //1-Actual 2-Post

            if (dbtnActualPost.SelectedIndex == 0)
            {
                ActualOrPost = 1;
            }
            else if (dbtnActualPost.SelectedIndex == 1)
            {
                ActualOrPost = 2;
            }

            for (int i = 0; i < dgvCostDetails.Rows.Count; i++)
            {
                GridViewRow row2 = dgvCostDetails.Rows[i];
                Label lblMCAE_IS_EDIT = (Label)row2.FindControl("lblMCAE_IS_EDIT");
                TextBox txticet_actl_rt = (TextBox)row2.FindControl("txticet_actl_rt");
                TextBox tb = (TextBox)dgvCostDetails.Rows[i].Cells[1].FindControl("txticet_actl_rt");
                TextBox txticet_finl_rt = (TextBox)row2.FindControl("txticet_finl_rt");
                // string remark = (dgvCostDetails.Rows[i].FindControl("txticet_actl_rt") as TextBox).Text;
                if (ActualOrPost == 1 && txticet_finl_rt != null)
                {
                    txticet_finl_rt.ReadOnly = true;
                }
                if (ActualOrPost == 2 && txticet_actl_rt != null)
                {
                    txticet_actl_rt.ReadOnly = true;
                }
                if (lblMCAE_IS_EDIT.Text == "0")
                {
                    //if (txticet_actl_rt != null) txticet_actl_rt.ReadOnly = true;
                    //if (txticet_finl_rt != null) txticet_finl_rt.ReadOnly = true;
                    // txticet_actl_rt.ReadOnly = true;
                    //  txticet_finl_rt.ReadOnly = true;
                    DispalyMessages("You don't have editable permision", 3);
                }
                else
                {
                    // txticet_actl_rt.ReadOnly = false;
                    // txticet_actl_rt.ReadOnly = false;
                    // txticet_finl_rt.ReadOnly = false;
                }
            }
        }

        protected void dgvCostDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void dgvCostDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        #endregion

        #region Other cost Region
        protected void btndgvOthCstEdit_Click(object sender, EventArgs e)
        {


        }

        protected void dgvOtherCostDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void dgvOtherCostDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvOtherCostDetails.EditIndex = e.NewEditIndex;
            BindOtherCostElements();

            int ActualOrPost = 0; //1-Actual 2-Post

            if (dbtnActualPost.SelectedIndex == 0)
            {
                ActualOrPost = 1;
            }
            else if (dbtnActualPost.SelectedIndex == 1)
            {
                ActualOrPost = 2;
            }

            for (int i = 0; i < dgvOtherCostDetails.Rows.Count; i++)
            {
                GridViewRow row2 = dgvOtherCostDetails.Rows[i];
                //Label lblMCAE_IS_EDIT = (Label)row2.FindControl("lblMCAE_IS_EDIT");
                TextBox txticet_actl_rt = (TextBox)row2.FindControl("txtIcet_actl_rt");
                TextBox txticet_finl_rt = (TextBox)row2.FindControl("txticet_finl_rt");

                if (ActualOrPost == 1 && txticet_finl_rt != null)
                {
                    txticet_finl_rt.ReadOnly = true;
                }
                if (ActualOrPost == 2 && txticet_actl_rt != null)
                {
                    txticet_actl_rt.ReadOnly = true;
                }
                //if (lblMCAE_IS_EDIT.Text == "0")
                //{
                //    if (txticet_actl_rt != null) txticet_actl_rt.ReadOnly = true;
                //    if (txticet_finl_rt != null) txticet_finl_rt.ReadOnly = true;
                //}
            }
        }

        protected void dgvOtherCostDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btndgvOthCstUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            TextBox txtIcet_actl_rt = (TextBox)row.FindControl("txticet_actl_rt");
            TextBox txticet_finl_rt = (TextBox)row.FindControl("txticet_finl_rt");
            Label lblicet_ele_cd = (Label)row.FindControl("lblicet_ele_cd");

            if (txtIcet_actl_rt.Text != null && !isdecimal(txtIcet_actl_rt.Text))
            {
                DispalyMessages("Please enter a valid amount", 1);
                return;
            }

            if (Session["oImportsCostElements"] != null)
            {
                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                ImportsCostElement oEditItem = oImportsCostElements.Find(x => x.Icet_ele_cd == lblicet_ele_cd.Text.Trim());
                if (oEditItem != null)
                {
                    if (dbtnActualPost.SelectedIndex == 0)
                    {
                        oEditItem.Icet_actl_rt = Convert.ToDecimal(txtIcet_actl_rt.Text);
                    }
                    else if (dbtnActualPost.SelectedIndex == 1)
                    {
                        oEditItem.Icet_finl_rt = Convert.ToDecimal(txticet_finl_rt.Text);
                    }
                    Session["oImportsCostElements"] = oImportsCostElements;
                    dgvOtherCostDetails.EditIndex = -1;
                    BindOtherCostElements();
                    BindDutyCostElements();
                }
            }

        }

        protected void btndgvOthCstCancel_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    dgvOtherCostDetails.EditIndex = -1;
                    BindOtherCostElements();
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
        #endregion

        protected void dgvitems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvitems.PageIndex = e.NewPageIndex;
                dgvitems.DataSource = null;
                dgvitems.DataSource = (List<ImportsCostItem>)Session["oImportsCostItems"];
                dgvitems.DataBind();
                mpItemDetails.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }

        protected void dgvOtherCostDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (txtConfDelOther.Value == "No")
                {
                    return;
                }

                if (dbtnActualPost.SelectedIndex == 0)
                {
                    GridViewRow row = dgvOtherCostDetails.Rows[e.RowIndex];
                    Label lblIch_seq_no = (Label)row.FindControl("lblIch_seq_no");
                    Label lblicet_ele_cd = (Label)row.FindControl("lblicet_ele_cd");
                    if (Session["oImportsCostElements"] != null)
                    {
                        oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                        ImportsCostElement oEdiedItem = oImportsCostElements.Find(x => x.Icet_seq_no == Convert.ToInt32(lblIch_seq_no.Text) && x.Icet_ele_cd == lblicet_ele_cd.Text.Trim());
                        if (oEdiedItem != null)
                        {
                            oEdiedItem.Icet_stus = 0;
                            Session["oImportsCostElements"] = oImportsCostElements;

                        }
                        dgvOtherCostDetails.EditIndex = -1;
                        BindOtherCostElements();
                    }
                    else
                    {
                        divWarning.Visible = true;
                        lblAlert.Text = "Error";
                    }
                }
                else if (dbtnActualPost.SelectedIndex == 1)
                {
                    GridViewRow row = dgvOtherCostDetails.Rows[e.RowIndex];
                    Label lblIch_seq_no = (Label)row.FindControl("lblIch_seq_no");
                    Label lblicet_ele_cd = (Label)row.FindControl("lblicet_ele_cd");
                    Label lblicet_actl_rt = (Label)row.FindControl("lblicet_actl_rt");

                    if (!string.IsNullOrEmpty(lblicet_actl_rt.Text) && Convert.ToDecimal(lblicet_actl_rt.Text) == 0)
                    {
                        if (Session["oImportsCostElements"] != null)
                        {
                            oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                            ImportsCostElement oEdiedItem = oImportsCostElements.Find(x => x.Icet_seq_no == Convert.ToInt32(lblIch_seq_no.Text) && x.Icet_ele_cd == lblicet_ele_cd.Text.Trim());
                            if (oEdiedItem != null)
                            {
                                oEdiedItem.Icet_stus = 0;
                                Session["oImportsCostElements"] = oImportsCostElements;

                            }
                            dgvOtherCostDetails.EditIndex = -1;
                            BindOtherCostElements();
                        }
                        else
                        {
                            divWarning.Visible = true;
                            lblAlert.Text = "Error";
                        }
                    }
                    else
                    {
                        DispalyMessages("Cannot delete items in this stage", 3);
                        return;
                    }
                }
                else
                {
                    DispalyMessages("Cannot delete items in this stage", 3);
                    return;
                }
            }
            catch (Exception ex)
            {
                DispalyMessages(ex.Message, 4);
            }
        }



        protected void btnAddCostDetails_Click(object sender, EventArgs e)
        {
            if (dgvOtherCostDetails.Rows.Count > 0)
            {
                dgvOtherChargeSelect.DataSource = new int[] { };
                if (Session["oImportsCostElements"] != null)
                {
                    oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];



                    var names = new string[] { "OTH", "TOT" };

                    //var matches = (from Item in oImportsCostElements
                    //              where names.Contains(Item.Icet_ele_cat)
                    //              select Item).ToList();
                    dgvOtherChargeSelect.DataSource = (from Item in oImportsCostElements
                                                       where names.Contains(Item.Icet_ele_cat)
                                                       select Item).ToList();

                    //if (oImportsCostElements.FindAll(x => x.Icet_ele_cat == "OTH").Count > 0)
                    //{
                    //    dgvOtherChargeSelect.DataSource = oImportsCostElements.FindAll(x => x.Icet_ele_cat == "OTH");
                    //}
                }
                dgvOtherChargeSelect.DataBind();
                mpAddCostItems.Show();
                _showCostItem = true;
                bindCostBrackValus();
                clearCostBValus();
            }
            else
            {
                DispalyMessages("please add other charges", 1);
            }
        }

        protected void btnSelectOtherCharge_Click(object sender, EventArgs e)
        {
            mpAddCostItems.Show();
            _showCostItem = true;
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            for (int i = 0; i < dgvOtherChargeSelect.Rows.Count; i++)
            {
                GridViewRow drTemp = dgvOtherChargeSelect.Rows[i];
                drTemp.BackColor = Color.Transparent;
            }
            dr.BackColor = Color.Cyan;

            Label lblicet_ele_cd = dr.FindControl("lblicet_ele_cd") as Label;
            Label lblIcet_ele_cd_desc = dr.FindControl("lblIcet_ele_cd_desc") as Label;
            Label lblIcet_line = dr.FindControl("lblIcet_line") as Label;
            Label lblIch_seq_no = dr.FindControl("lblIch_seq_no") as Label;

            txticer_doc_no.Text = lblicet_ele_cd.Text.Trim();
            lblicet_ele_cd.ToolTip = lblIcet_ele_cd_desc.Text.Trim();
            LblSelectedLine.Text = lblIcet_line.Text.Trim();
            lblSelectedSeq.Text = lblIch_seq_no.Text.Trim();
            txtSerProv.Text = "";
        }

        protected void btnDeleteChar_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblICER_DOC_NO = dr.FindControl("lblICER_DOC_NO") as Label;
            Label lblICER_LINE = dr.FindControl("lblICER_LINE") as Label;
            Label lblICER_REF_AMT = dr.FindControl("lblICER_REF_AMT") as Label;
            Label LBLICER_ELE_LINE = dr.FindControl("LBLICER_ELE_LINE") as Label;

            if (oimp_cst_ele_refs.Count > 0 && oimp_cst_ele_refs.FindAll(x => x.Icer_doc_no == lblICER_DOC_NO.Text.Trim() && x.Icer_line == Convert.ToInt32(lblICER_LINE.Text.Trim())).Count > 0)
            {
                oimp_cst_ele_refs.RemoveAll(x => x.Icer_doc_no == lblICER_DOC_NO.Text.Trim() && x.Icer_line == Convert.ToInt32(lblICER_LINE.Text));
                clearCostBValus();
                bindCostBrackValus();
                mpAddCostItems.Show();
                _showCostItem = true;

                List<IMP_CST_ELEREF_DET> _subdata = Session["IMP_CST_ELEREF_DET"] as List<IMP_CST_ELEREF_DET>;
                if (_subdata != null && _subdata.Count > 0)
                {
                    _subdata.RemoveAll(a => a.iced_ref_line == Convert.ToInt32(lblICER_LINE.Text.Trim()));
                }
                Session["IMP_CST_ELEREF_DET"] = _subdata;
                grdelesubcost.DataSource = null;
                grdelesubcost.DataBind();
            }
            oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
            if (oImportsCostElements.Where(a => a.Icet_line == Convert.ToInt32(LBLICER_ELE_LINE.Text.Trim())).ToList() != null && oImportsCostElements.Where(a => a.Icet_line == Convert.ToInt32(LBLICER_ELE_LINE.Text.Trim())).ToList().Count > 0)
            {
                if (dbtnActualPost.SelectedValue == "Actual")
                {
                    oImportsCostElements.Where(a => a.Icet_line == Convert.ToInt32(LBLICER_ELE_LINE.Text.Trim()) && a.Icet_ele_cat == "OTH").First().Icet_actl_rt -= Convert.ToDecimal(lblICER_REF_AMT.Text.Trim());
                }
                else
                {
                    oImportsCostElements.Where(a => a.Icet_line == Convert.ToInt32(LBLICER_ELE_LINE.Text.Trim()) && a.Icet_ele_cat == "OTH").First().Icet_finl_rt -= Convert.ToDecimal(lblICER_REF_AMT.Text.Trim());
                }
                Session["oImportsCostElements"] = oImportsCostElements;
                BindOtherCostElements();
            }


        }

        protected void btnAddITems_Click(object sender, EventArgs e)
        {
            mpAddCostItems.Show();
            _showCostItem = true;
            if (string.IsNullOrEmpty(txticer_doc_no.Text))
            {
                DispalyMessages("Please enter document number", 1);
                return;
            }
            if (string.IsNullOrEmpty(txticer_ref_no.Text))
            {
                DispalyMessages("Please enter reference", 1);
                return;
            }
            if (string.IsNullOrEmpty(txticer_ref_amt.Text))
            {
                DispalyMessages("Please enter amount", 1);
                return;
            }

            if (oimp_cst_ele_refs == null)
            {
                oimp_cst_ele_refs = new List<imp_cst_ele_ref>();
            }
            oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
            if (oImportsCostElements.Where(a => a.Icet_line == Convert.ToInt32(LblSelectedLine.Text.Trim())).ToList() != null && oImportsCostElements.Where(a => a.Icet_line == Convert.ToInt32(LblSelectedLine.Text.Trim())).ToList().Count > 0)
            {
                if (dbtnActualPost.SelectedValue == "Actual")
                {
                    oImportsCostElements.Where(a => a.Icet_line == Convert.ToInt32(LblSelectedLine.Text.Trim()) && a.Icet_ele_cat == "OTH").First().Icet_actl_rt += Convert.ToDecimal(txticer_ref_amt.Text.Trim());
                }
                else
                {
                    oImportsCostElements.Where(a => a.Icet_line == Convert.ToInt32(LblSelectedLine.Text.Trim()) && a.Icet_ele_cat == "OTH").First().Icet_finl_rt += Convert.ToDecimal(txticer_ref_amt.Text.Trim());
                }
                Session["oImportsCostElements"] = oImportsCostElements;
                BindOtherCostElements();
            }

            imp_cst_ele_ref oNewItem = new imp_cst_ele_ref();
            oNewItem.Icer_seq_no = Convert.ToInt32(lblSelectedSeq.Text.Trim());
            oNewItem.Icer_doc_no = txticer_doc_no.Text.Trim();
            oNewItem.Icer_ele_line = Convert.ToInt32(LblSelectedLine.Text.Trim());
            oNewItem.Icer_ref_no = txticer_ref_no.Text.Trim();
            oNewItem.Icer_line = oimp_cst_ele_refs.Count() + 1;
            oNewItem.Icer_ref_dt = Convert.ToDateTime(txticer_ref_dt.Text.Trim());
            oNewItem.Icer_ref_amt = Convert.ToDecimal(txticer_ref_amt.Text.Trim());
            oNewItem.Icer_ref_rmk = txticer_ref_rmk.Text.Trim();
            oNewItem.Icer_cre_by = Session["UserID"].ToString();
            oNewItem.Icer_cre_dt = DateTime.Now;
            oNewItem.Icer_mod_by = Session["UserID"].ToString();
            oNewItem.Icer_mod_dt = DateTime.Now;
            oNewItem.Icer_anal_1 = string.Empty;
            oNewItem.Icer_anal_2 = string.Empty;
            oNewItem.Icer_anal_3 = string.Empty;
            oNewItem.Icer_anal_4 = string.Empty;
            oNewItem.Icer_anal_5 = string.Empty;
            oNewItem.Icer_session_id = Session["SessionID"].ToString();
            oNewItem.Icer_ser_provider = txtSerProv.Text.Trim();
            oNewItem.Tmp_is_New = 1;
            oimp_cst_ele_refs.Add(oNewItem);
            _isNewItem = true;
            clearCostBValus();
            bindCostBrackValus();
            if (txticer_doc_no.Text.ToString() == "TRA")
            {
                dpcontainer.Visible = true;
                //GET BL CONTAINER
                List<ImportsBLContainer> _blcontai = CHNLSVC.Financial.GetCostContainers(txtBLNumber.Text.ToString());
                dpcontainer.DataSource = _blcontai;
                dpcontainer.DataTextField = "Ibc_desc";
                dpcontainer.DataValueField = "Ibc_desc";
                dpcontainer.DataBind();
            }
            else
            {
                dpcontainer.Visible = false;
                dpcontainer.DataSource = null;
                dpcontainer.DataBind();
            }


        }

        private void bindCostBrackValus()
        {
            if (oimp_cst_ele_refs != null && oimp_cst_ele_refs.Count > 0)
            {
                foreach (var item in oimp_cst_ele_refs)
                {
                    if (!_isNewItem)
                    {
                        item.Tmp_is_New = 0;
                    }
                }
                dgvCostBrackup.DataSource = oimp_cst_ele_refs;
                dgvCostBrackup.DataBind();
            }
            else
            {
                dgvCostBrackup.DataSource = new int[] { };
                dgvCostBrackup.DataBind();
            }
        }

        private void clearCostBValus(bool isClearDocNLine = false)
        {
            if (isClearDocNLine)
            {
                txticer_doc_no.Text = String.Empty;
                LblSelectedLine.Text = "";
                lblSelectedSeq.Text = "";
                for (int i = 0; i < dgvOtherChargeSelect.Rows.Count; i++)
                {
                    GridViewRow drTemp = dgvOtherChargeSelect.Rows[i];
                    drTemp.BackColor = Color.Transparent;
                }
            }
            txticer_ref_no.Text = String.Empty;
            txticer_ref_dt.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txticer_ref_amt.Text = String.Empty;
            txticer_ref_rmk.Text = String.Empty;
            txtSerProv.Text = String.Empty;
        }

        private void loadItemStatus()
        {
            List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            Session["ItemStatus"] = oMasterItemStatuss;
        }

        private string getItemStatusDesc(string stis)
        {
            List<MasterItemStatus> oStatuss = (List<MasterItemStatus>)Session["ItemStatus"];
            if (oStatuss.FindAll(x => x.Mis_cd == stis).Count > 0)
            {
                stis = oStatuss.Find(x => x.Mis_cd == stis).Mis_desc;
            }
            return stis;
        }

        protected void dgvCostSheetHeader_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvCostSheetHeader, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dgvCostSheetHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isNewItem = false;
            clearByHeader();

            for (int i = 0; i < dgvCostSheetHeader.Rows.Count; i++)
            {
                dgvCostSheetHeader.Rows[i].BackColor = Color.FromName("0");
            }
            GridViewRow row = dgvCostSheetHeader.SelectedRow;
            row.BackColor = Color.FromName("PALETURQUOISE");
            Label lblich_doc_no = (Label)row.FindControl("lblich_doc_no");
            Label lblIch_seq_no = (Label)row.FindControl("lblIch_seq_no");
            Session["SelectedDoc"] = lblich_doc_no.Text;

            List<ImportsCostHeader> oHeaders = (List<ImportsCostHeader>)Session["oHeaders"];
            ImportsCostHeader oCostHeader = oHeaders.Find(x => x.Ich_seq_no == Convert.ToInt32(lblIch_seq_no.Text) && x.Ich_doc_no == lblich_doc_no.Text);
            Session["oCostHeader"] = oCostHeader;
            loadCostElements(lblich_doc_no.Text);
        }

        protected void dgvitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = dgvitems.SelectedRow;
            Label lblici_seq_no = (Label)row.FindControl("lblici_seq_no");
            Label lblici_itm_cd = (Label)row.FindControl("lblici_itm_cd");
            Label lblici_ref_line = (Label)row.FindControl("lblici_ref_line");

            for (int i = 0; i < dgvitems.Rows.Count; i++)
            {
                GridViewRow rowTemp = dgvitems.Rows[i];
                rowTemp.BackColor = Color.Transparent;
            }

            row.BackColor = Color.Cyan;

            List<ImportsCostElementItem> oImportsCostElementItems = CHNLSVC.Financial.GET_IMP_CST_ELE_ITM_BY_ITM(Convert.ToInt32(lblici_seq_no.Text), Session["SelectedDoc"].ToString(), lblici_itm_cd.Text.Trim());
            Session["oImportsCostElementItems"] = oImportsCostElementItems;

            // 2016-01-20 Set Bond values

            List<ImportsCostElementItem> oFilterd = oImportsCostElementItems.FindAll(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(lblici_ref_line.Text));
            //oFilterd.Where(x => x.Ice_ele_cat == "CUSTM").

            DataTable dtTemp = GlobalMethod.ToDataTable(oFilterd);

            if (dbtnActualPost2.SelectedIndex == 0)
            {
                dbtnActualPost.SelectedIndex = 0;
            }
            if (dbtnActualPost2.SelectedIndex == 1)
            {
                dbtnActualPost.SelectedIndex = 1;
            }
            foreach (ImportsCostElementItem oitm in oFilterd)
            {
                oitm.Ice_pre_rt = oitm.Ice_pre_rt - oitm.Ice_pre_amt_claim;
                oitm.Ice_actl_rt = oitm.Ice_actl_rt - oitm.Ice_actl_amt_claim;
                oitm.Ice_finl_rt = oitm.Ice_finl_rt - oitm.Ice_finl_amt_claim;
            }

            foreach (ImportsCostElementItem item in oFilterd)
            {
                //Calculate tobond values
                if (item.Ice_ele_tp != "DUTY")
                {
                    if (dbtnActualPost.SelectedIndex == 0)
                    {
                        item.Ice_pre_rt = item.Ice_actl_rt;
                        item.Ice_actl_rt = item.Ice_actl_rt;
                        item.Ice_finl_rt = item.Ice_actl_rt;
                    }
                    else
                    {
                        item.Ice_pre_rt = item.Ice_finl_rt;
                        item.Ice_actl_rt = item.Ice_finl_rt;
                        item.Ice_finl_rt = item.Ice_finl_rt;
                    }
                }
                else if (item.Ice_ele_tp == "DUTY" && item.Ice_ele_cd == "PAL")
                {
                    if (dbtnActualPost.SelectedIndex == 0)
                    {
                        item.Ice_actl_rt = item.Ice_actl_rt;
                        item.Ice_pre_rt = 0;
                    }
                    else
                    {
                        item.Ice_finl_rt = item.Ice_finl_rt;
                        item.Ice_pre_rt = 0;
                    }

                }
                else
                {
                    item.Ice_pre_rt = 0;
                    item.Ice_actl_rt = 0;
                }

                //if (item.Ice_anal_2 == "1")
                //{
                //    //item.Ice_pre_amnt = 0;

                //    if (dbtnActualPost.SelectedIndex == 0)
                //    {
                //        item.Ice_pre_rt = item.Ice_actl_rt;
                //        item.Ice_actl_rt = item.Ice_actl_rt;
                //        item.Ice_finl_rt = item.Ice_actl_rt;
                //    }
                //    else
                //    {
                //        item.Ice_pre_rt = item.Ice_finl_rt;
                //        item.Ice_actl_rt = item.Ice_finl_rt;
                //        item.Ice_finl_rt = item.Ice_finl_rt;
                //    }
                //}
                //else
                //{

                //    if (dbtnActualPost.SelectedIndex == 0)
                //    {
                //        if (item.Ice_ele_cd =="PAL")
                //        {
                //            item.Ice_actl_rt = item.Ice_actl_rt;
                //        }
                //        else
                //        {
                //            item.Ice_pre_rt = 0;
                //            item.Ice_actl_rt = 0;
                //        }
                //    }
                //    else
                //    {
                //        if (item.Ice_ele_cd == "PAL")
                //        {
                //            item.Ice_finl_rt = item.Ice_finl_rt;
                //        }
                //        else
                //        {
                //            item.Ice_pre_rt = 0;
                //            item.Ice_actl_rt = 0;
                //        }
                //    }
                //item.Ice_pre_rt = 0;
                // item.Ice_actl_rt = 0;
                //}
            }


            dgvItemCosts.DataSource = oFilterd;
            dgvItemCosts.DataBind();
            mpItemDetails.Show();

            //txtTotalPreAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1).Sum(z => z.Ice_pre_amnt).ToString("N2");
            //txtTotalActualAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1).Sum(z => z.Ice_actl_amnt).ToString("N2");
            //txtTotalFinalAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1).Sum(z => z.Ice_finl_amnt).ToString("N2");

            txtTotalPreAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(lblici_ref_line.Text)).Sum(z => z.Ice_pre_rt).ToString("N2");
            txtTotalActualAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(lblici_ref_line.Text)).Sum(z => z.Ice_actl_rt).ToString("N2");
            txtTotalFinalAmount.Text = oImportsCostElementItems.Where(x => x.Ice_stus == 1 && x.Ice_ref_line == Convert.ToInt32(lblici_ref_line.Text)).Sum(z => z.Ice_finl_rt).ToString("N2");

            //  ImportsCostItem oPrivousItems = CHNLSVC.Financial.GET_PRIVIOUS_VALUES(Session["SelectedDoc"].ToString(), lblici_itm_cd.Text.Trim());
            ImportsCostItem oPrivousItems = CHNLSVC.MsgPortal.GET_PREV_ITEM_COST_FOR_COSTSHEET(lblici_itm_cd.Text.Trim(), Session["UserCompanyCode"].ToString());
            //oPrivousItems = oPrivousItems.OrderByDescending(c => c.Ici_mod_dt).ToList();
            if (oPrivousItems != null)
            {
                txtPreviousValues.Text = "0.00";
                txtPrvDocDt.Text = string.Empty;
                txtPrvDocNo.Text = string.Empty;

                txtPreviousValues.Text = oPrivousItems.Ici_unit_amt.ToString("N2");
                txtPrvDocNo.Text = oPrivousItems.Ici_doc_no;
                txtPrvDocDt.Text = !string.IsNullOrEmpty(oPrivousItems.Ici_doc_no) ? oPrivousItems.Ici_cre_dt.ToString("dd/MMM/yyyy") : "";

            }
            else
            {
                txtPreviousValues.Text = "0.00";
                txtPrvDocDt.Text = string.Empty;
                txtPrvDocNo.Text = string.Empty;
            }

            if (dbtnActualPost.SelectedIndex == 0)
            {
                decimal RATE = ((Convert.ToDecimal(txtTotalActualAmount.Text) / 100) * 5);
                decimal Diff = Convert.ToDecimal(txtPreviousValues.Text) - Convert.ToDecimal(txtTotalActualAmount.Text);

                if (Diff != 0)
                {
                    if ((RATE <= Diff) || (RATE >= Diff))
                    {
                        txtTotalActualAmount.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtTotalActualAmount.ForeColor = Color.Black;
                    }
                }
                else
                {
                    txtTotalActualAmount.ForeColor = Color.Black;
                }
            }
            else if (dbtnActualPost.SelectedIndex == 1)
            {
                decimal RATE = ((Convert.ToDecimal(txtTotalFinalAmount.Text) / 100) * 5);
                decimal Diff = Convert.ToDecimal(txtPreviousValues.Text) - Convert.ToDecimal(txtTotalFinalAmount.Text);

                if (Diff != 0)
                {
                    if ((RATE <= Diff) || (RATE >= Diff))
                    {
                        txtTotalFinalAmount.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtTotalFinalAmount.ForeColor = Color.Black;
                    }
                }
                else
                {
                    txtTotalFinalAmount.ForeColor = Color.Black;
                }
            }
        }

        protected void dgvitems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvitems, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private List<ImportsCostElement> loadAllOtherCharges(List<ImportsCostElement> oImportsCostElements)
        {
            DataTable result = CHNLSVC.Financial.GET_IMP_OTHER_CST("OTH");
            if (result != null && result.Rows.Count > 0)
            {
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (oImportsCostElements.FindAll(x => x.Icet_ele_cd == result.Rows[i]["CODE"].ToString()).Count == 0)
                    {
                        //var ordno = oImportsCostElements.Where(x => x.Icet_ele_cd == result.Rows[i]["CODE"].ToString()).Select(a => a.ORDERNO).First();
                        ImportsCostElement oNewItem = new ImportsCostElement();
                        oNewItem.Icet_ele_cd = result.Rows[i]["CODE"].ToString();
                        oNewItem.Icet_seq_no = 0;
                        oNewItem.Icet_line = 0;
                        oNewItem.Icet_doc_no = Session["SelectedDoc"].ToString().Trim();
                        oNewItem.Icet_stus = 1;
                        oNewItem.Icet_ele_cat = "OTH";
                        oNewItem.Icet_ele_tp = "OTH";
                        oNewItem.Icet_ele_rt = 0;
                        oNewItem.Icet_pre_rt = 0;
                        oNewItem.Icet_actl_rt = 0;
                        oNewItem.Icet_finl_rt = 0;
                        oNewItem.Icet_anal_1 = string.Empty;
                        oNewItem.Icet_anal_2 = result.Rows[i]["ORDERNO"].ToString();
                        oNewItem.Icet_anal_3 = string.Empty;
                        oNewItem.Icet_anal_4 = string.Empty;
                        oNewItem.Icet_anal_5 = result.Rows[i]["MCAT_FORMULA_ID"].ToString();
                        oNewItem.Icet_cre_by = Session["UserID"].ToString();
                        oNewItem.Icet_cre_dt = DateTime.Now;
                        oNewItem.Icet_mod_by = Session["UserID"].ToString();
                        oNewItem.Icet_mod_dt = DateTime.Now;
                        oNewItem.Icet_session_id = Session["SessionID"].ToString();
                        oNewItem.Icet_ele_cd_desc = result.Rows[i]["DESCRIPTION"].ToString();
                        oNewItem.isNewRecord = 1;
                        oNewItem.ORDERNO = result.Rows[i]["ORDERNO"].ToString();
                        oImportsCostElements.Add(oNewItem);
                    }
                }

                //if (oImportsCostElements.FindAll(x => x.Icet_ele_cat == "OTH").Count > 0)
                //{
                //    dgvOtherCostDetails.DataSource = oImportsCostElements.FindAll(x => x.Icet_ele_cat == "OTH");
                //}
            }
            return oImportsCostElements;
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDemurrageDays.Text))
            {
                DispalyMessages("Please enter demurrage days", 1);
                return;
            }
            if (string.IsNullOrEmpty(txtExRate.Text))
            {
                DispalyMessages("Please enter exchange rate", 1);
                return;
            }
            if (string.IsNullOrEmpty(txtsgexrate.Text))
            {
                DispalyMessages("Please enter exchange rate", 1);
                return;
            }
            bool is_shipgarr = CHNLSVC.Financial.IsShipGrnty(txtBLNumber.Text.ToString());
            if (Session["oImportsCostElements"] != null)
            {
                oImportsCostElements = (List<ImportsCostElement>)Session["oImportsCostElements"];
                ImportsCostHeader oImportsCostHeader = (ImportsCostHeader)Session["oImportsCostHeader"];

                foreach (ImportsCostElement item in oImportsCostElements)
                {
                    //alredy aded
                    if (!string.IsNullOrEmpty(item.Icet_anal_5))
                    {
                        if (item.Icet_anal_5.ToUpper() == "Cal_SLPA".ToUpper() && item.Icet_ele_cd != "TRA")
                        {
                            item.Icet_stus = 1;
                            item.Icet_actl_rt = CHNLSVC.Financial.Cal_SLPA(txtBLNumber.Text.Trim(), Convert.ToInt32(txtDemurrageDays.Text), Convert.ToDecimal(txtsgexrate.Text));
                        }
                        else if (item.Icet_anal_5.ToUpper() == "Cal_SLPA_RENT".ToUpper() && item.Icet_ele_cd != "TRA" && item.Icet_ele_cd != "WRFE")
                        {
                            item.Icet_stus = 1;
                            item.Icet_actl_rt = CHNLSVC.Financial.Cal_SLPA_RENT(txtBLNumber.Text.Trim(), Convert.ToInt32(txtDemurrageDays.Text), Convert.ToDecimal(txtsgexrate.Text));
                        }
                        else if (item.Icet_anal_5.ToUpper() == "Cal_SLPA_RENT".ToUpper()  && item.Icet_ele_cd == "WRFE")
                        {
                            item.Icet_stus = 1;
                            item.Icet_actl_rt = CHNLSVC.Financial.WalfExsCalc(txtBLNumber.Text.Trim(), Convert.ToInt32(txtDemurrageDays.Text), Convert.ToDecimal(txtsgexrate.Text));

                            if (Session["UserCompanyCode"].ToString() == "AAL")
                            {
                                item.Icet_actl_rt = CHNLSVC.Financial.WalfExsCalcAAL(txtBLNumber.Text.Trim(), Convert.ToInt32(txtDemurrageDays.Text), Convert.ToDecimal(txtsgexrate.Text));
                            }
                        }
                    }
                    if (item.Icet_ele_cd == "CDEM")
                    {
                        item.Icet_stus = 1;
                        item.Icet_actl_rt = CHNLSVC.Financial.Cal_CDEM_RENT(txtBLNumber.Text.Trim(), Convert.ToInt32(txtDemurrageDays.Text), Convert.ToDecimal(txtsgexrate.Text));
                    }
                    if (item.Icet_ele_cd == "TRA" && item.Icet_ele_cat == "OTH")
                    {
                        //GET BL CONTAINER
                        List<ImportsBLContainer> _blcontai = CHNLSVC.Financial.GetCostContainers(txtBLNumber.Text.ToString());
                        decimal _trnscost = 0;
                        foreach (var _con in _blcontai)
                        {
                            _trnscost = _trnscost + CHNLSVC.Financial.CostTransCost(Session["UserCompanyCode"].ToString(), _con.Ibc_tp, "TRAN", Session["Loc"].ToString());
                        }
                        item.Icet_actl_rt =_trnscost;
                    }
                    else if (item.Icet_ele_cd == "DOC" && item.Icet_ele_cat == "OTH")
                    {
                        decimal _trnscost = 0;
                        _trnscost = _trnscost + CHNLSVC.Financial.CostTransCost(Session["UserCompanyCode"].ToString(), "DOC", "DOC", oImportsCostHeader.Ib_loc_cd);
                        item.Icet_actl_rt =_trnscost;
                    }
                    else if (item.Icet_ele_cd == "BC" && item.Icet_ele_cat == "OTH")
                    {
                        decimal totamt = 0;
                        Int32 foc = 0;
                        Int32 shipgrrnt = 0;
                        decimal othercost = CHNLSVC.Financial.GetBLCostVal(txtBLNumber.Text.ToString());
                        if (CHNLSVC.Financial.IsfocSI(txtBLNumber.Text.ToString()))
                        {
                            foc = 1;
                        }
                        if (CHNLSVC.Financial.IsShipGrnty(txtBLNumber.Text.ToString()))
                        {
                            shipgrrnt = 1;
                        }
                        decimal _trnscost = 0;
                        string paytype = CHNLSVC.Financial.GetFinPaytype(txtBLNumber.Text.ToString());
                        string paysubtype = CHNLSVC.Financial.GetFinPaySubtype(txtBLNumber.Text.ToString());
                        if (paysubtype == "") paysubtype = "N/A";
                        string _bankcd = CHNLSVC.Financial.GetFinBankCode(txtBLNumber.Text.ToString());
                        decimal _lcval = CHNLSVC.Financial.GetLCVal(txtBLNumber.Text.ToString()) + othercost;
                        decimal exch2 = 0;
                        if (CHNLSVC.Financial.GetSICurrency(txtBLNumber.Text.ToString()) == "USD")
                        {
                            exch2 = Convert.ToDecimal(txtExRate.Text.ToString());
                        }
                        else
                        {
                            //exch2 = CHNLSVC.Financial.GetCostExchangeRAte(txtBLNumber.Text.ToString());
                            exch2 = Convert.ToDecimal(txtExRate.Text.ToString());
                        }
                                                //expire date
                        Int32 months = CHNLSVC.Financial.GetExpiryMonths(txtBLNumber.Text.ToString());

                        List<imp_cst_shp_bnk> _allbanckchg = CHNLSVC.Financial.GetAllBankData(Session["UserCompanyCode"].ToString(), _bankcd, foc, shipgrrnt, "BANK", paytype, paysubtype);
                        if (_allbanckchg != null && _allbanckchg.Count >0)
                        {
                            decimal chg1 = 0;
                            if (_allbanckchg.First().icsb_comm > 100)
                            {
                                chg1 = _allbanckchg.First().icsb_comm;
                            }
                            else
                            {
                                chg1 = _allbanckchg.First().icsb_comm * _lcval * exch2 / 100;
                            }
                           
                           
                            totamt = totamt + chg1;
                            if (months > _allbanckchg.First().icsc_expir_months*30)
                            {
                                decimal chg2=0;
                                if (_allbanckchg.First().icsc_add_month_chg > 100)
                                {
                                    chg2 = _allbanckchg.First().icsc_add_month_chg;
                                }
                                else
                                {
                                    chg2 = _allbanckchg.First().icsc_add_month_chg * _lcval * exch2 / 100;
                                }
                                totamt = totamt + chg2;
                            }
                            if(_allbanckchg.First().icsb_cost > 0)
                            {
                                decimal exch1 = Exchngerat(_allbanckchg.First().icsb_curr);
                                decimal chg3 = exch1 * _allbanckchg.First().icsb_cost;
                                totamt = totamt+chg3;
                            }
                            if (_allbanckchg.First().icsb_swift_chg_of_bllpay>0)
                            {
                                decimal exch3 = Exchngerat(_allbanckchg.First().icsb_anal2);

                                decimal chg4 = exch3 * _allbanckchg.First().icsb_swift_chg_of_bllpay;
                                totamt = totamt + chg4;
                            }
                            if (_allbanckchg.First().icsb_bill_pay_comm>100)
                            {
                                decimal chg5 = _allbanckchg.First().icsb_bill_pay_comm;
                                totamt = totamt + chg5;
                            }
                            else
                            {
                                decimal chg5 = _allbanckchg.First().icsb_bill_pay_comm * _lcval * exch2/100;
                                totamt = totamt + chg5;
                            }

                            if (totamt < _allbanckchg.First().icsb_min_bank_chg)
                            {
                                totamt = _allbanckchg.First().icsb_min_bank_chg;
                            }
                        }

                        if (Session["UserCompanyCode"].ToString()=="AAL")
                        {
                            decimal heromotobike = CHNLSVC.Financial.HeroMotobikeCharge(txtBLNumber.Text.ToString());
                            totamt = totamt + heromotobike;
                        }

                      
                        _trnscost = _trnscost + totamt;
                        item.Icet_actl_rt = _trnscost;
                    }
                    else if (item.Icet_ele_cd == "SG" && item.Icet_ele_cat == "OTH" && is_shipgarr==true)
                    {
                        Int32 foc = 0;
                        Int32 shipgrrnt = 0;
                        decimal othercost = CHNLSVC.Financial.GetBLCostVal(txtBLNumber.Text.ToString());
                        decimal bcexchange = Convert.ToDecimal(txtExRate.Text.ToString());
                        if (CHNLSVC.Financial.IsfocSI(txtBLNumber.Text.ToString()))
                        {
                            foc = 1;
                        }
                        if (CHNLSVC.Financial.IsShipGrnty(txtBLNumber.Text.ToString()))
                        {
                            shipgrrnt = 1;
                        }
                        string paytype = CHNLSVC.Financial.GetFinPaytype(txtBLNumber.Text.ToString());
                        string paysubtype = CHNLSVC.Financial.GetFinPaySubtype(txtBLNumber.Text.ToString());
                        ImpCusdecHdr oImpCusdecHdr = CHNLSVC.Financial.GET_CUSTDEC_HDR_BY_DOC(txtBLNumber.Text.ToString());
                        if (paysubtype == "") paysubtype = "N/A";
                        //expire date
                        Int32 months = CHNLSVC.Financial.GetExpiryMonths(txtBLNumber.Text.ToString());
                        decimal _trnscost = 0;
                        string _bankcd = CHNLSVC.Financial.GetFinBankCode(txtBLNumber.Text.ToString());
                        decimal _lcval = CHNLSVC.Financial.GetLCVal(txtBLNumber.Text.ToString()) + othercost;
                        string btype = "SHIPPING";
                        if (oImpCusdecHdr.CUH_TP=="AIR")
                        {
                            btype = "AIR";
                        }
                        List<imp_cst_shp_bnk> _allbanckchg = CHNLSVC.Financial.GetAllBankData(Session["UserCompanyCode"].ToString(), _bankcd, foc, shipgrrnt, btype, paytype, paysubtype);
                        if (_allbanckchg != null && _allbanckchg.Count>0)
                        {

                        }
                        else
                        {
                            _allbanckchg = CHNLSVC.Financial.GetAllBankData(Session["UserCompanyCode"].ToString(), _bankcd, foc, shipgrrnt, "SHIPPING", paytype, paysubtype);
                        }
                        if (_allbanckchg !=null && _allbanckchg.Count>0 && _allbanckchg.First().icsb_min_bank_chg>=0)
                        {
                            decimal chg2 = 0;
                         
                             chg2 = _allbanckchg.First().icsb_shpp_grr_comm * _lcval * bcexchange/100;
                            if (chg2 < _allbanckchg.First().icsb_min_shp_grnty)
                            {
                                chg2 = _allbanckchg.First().icsb_min_shp_grnty;
                            }

                            _trnscost = _trnscost + chg2;
                        }
                        item.Icet_actl_rt =_trnscost;
                    }
                    else if (item.Icet_ele_cd == "INSU")
                    {
                        decimal othercost = CHNLSVC.Financial.GetBLCostVal(txtBLNumber.Text.ToString());
                        decimal _lcval = CHNLSVC.Financial.GetLCValFin(txtBLNumber.Text.ToString()) ;
                        decimal _prevval = CHNLSVC.Financial.GetFinINSVal(txtBLNumber.Text.ToString());
                        decimal _wofocval = CHNLSVC.Financial.GetFinWOFOCVal(txtBLNumber.Text.ToString()) + othercost;
                       
                        string _vat = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "CSTVAT");
                        decimal vat = _prevval * Convert.ToDecimal(_vat) / (100 + Convert.ToDecimal(_vat));
                        item.Icet_actl_rt = (_prevval - vat) * _wofocval / _lcval;
                        if (txtTradeTerm.Text.ToString().Contains("CIF"))
                        {
                            item.Icet_actl_rt = 0;
                        }

                    }
                    else if (item.Icet_ele_cd == "COST")
                    {
                        decimal _wofocval = CHNLSVC.Financial.GetLCVal(txtBLNumber.Text.ToString());
                        decimal bcexchange = Convert.ToDecimal(txtExRate.Text.ToString());
                        item.Icet_actl_rt = _wofocval * bcexchange;

                        if (txtTradeTerm.Text.ToString().Contains("CIF") && Session["UserCompanyCode"].ToString() != "AEC")
                        {
                            decimal othercost = CHNLSVC.Financial.GetBLCostVal(txtBLNumber.Text.ToString());
                            decimal _lcval = CHNLSVC.Financial.GetLCValFin(txtBLNumber.Text.ToString());
                            decimal _prevval = CHNLSVC.Financial.GetFinINSVal(txtBLNumber.Text.ToString());
                            decimal _wofocval2 = CHNLSVC.Financial.GetFinWOFOCVal(txtBLNumber.Text.ToString()) + othercost;

                            string _vat = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "CSTVAT");
                            decimal vat = _prevval * Convert.ToDecimal(_vat) / (100 + Convert.ToDecimal(_vat));
                           // item.Icet_actl_rt = (_prevval - vat) * _wofocval2 / _lcval;
                            item.Icet_actl_rt = item.Icet_actl_rt + (_prevval - vat) * _wofocval2 / _lcval + (othercost * bcexchange);

                        }
                        else if (txtTradeTerm.Text.ToString().Contains("CIF") && Session["UserCompanyCode"].ToString() == "AEC")
                        {
                            decimal othercost = CHNLSVC.Financial.GetBLCostVal(txtBLNumber.Text.ToString());
                            decimal _lcval = CHNLSVC.Financial.GetLCValFin(txtBLNumber.Text.ToString());
                            decimal _prevval = CHNLSVC.Financial.GetFinINSVal(txtBLNumber.Text.ToString());
                            decimal _wofocval2 = CHNLSVC.Financial.GetFinWOFOCVal(txtBLNumber.Text.ToString()) + othercost;

                            string _vat = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "CSTVAT");
                            decimal vat = _prevval * Convert.ToDecimal(_vat) / (100 + Convert.ToDecimal(_vat));
                            // item.Icet_actl_rt = (_prevval - vat) * _wofocval2 / _lcval;
                            item.Icet_actl_rt = item.Icet_actl_rt + (othercost * bcexchange);
                        }
                        if (txtTradeTerm.Text.ToString().Contains("C&F"))
                        {
                            decimal frtval = CHNLSVC.Financial.GetBLInsuVal(txtBLNumber.Text.ToString());
                            item.Icet_actl_rt = item.Icet_actl_rt + frtval * bcexchange;
                        }
                    }
                    else if (item.Icet_ele_cd == "OTH")
                    {
                        decimal bcexchange = Convert.ToDecimal(txtExRate.Text.ToString());
                        decimal _othamt = CHNLSVC.Financial.GetBLOTHAmtVal(txtBLNumber.Text.ToString());
                        item.Icet_actl_rt = _othamt * bcexchange;
                      
                    }
                    else if (item.Icet_ele_cd == "QC")
                    {
                        item.Icet_actl_rt = 0;
                    }
                    else if (item.Icet_ele_cd == "OT 1" && Session["UserCompanyCode"].ToString() == "AEC")
                    {
                        decimal othercost = CHNLSVC.Financial.GetBLCostVal(txtBLNumber.Text.ToString());
                        decimal _lcval = CHNLSVC.Financial.GetLCValFin(txtBLNumber.Text.ToString());
                        decimal _prevval = CHNLSVC.Financial.GetFinINSVal(txtBLNumber.Text.ToString());
                        decimal _wofocval = CHNLSVC.Financial.GetFinWOFOCVal(txtBLNumber.Text.ToString()) + othercost;

                        string _vat = CHNLSVC.Financial.GetAccountCodeByTp(Session["UserCompanyCode"].ToString(), "CSTVAT");
                        decimal vat = _prevval * Convert.ToDecimal(_vat) / (100 + Convert.ToDecimal(_vat));
                        item.Icet_actl_rt = (_prevval - vat) * _wofocval / _lcval;
                        if (txtTradeTerm.Text.ToString().Contains("CIF") && Session["UserCompanyCode"].ToString() != "AEC")
                        {
                            item.Icet_actl_rt = 0;
                        }
                    }
                    else if (item.Icet_ele_cd == "FRGT")
                    {
                        if (txtTradeTerm.Text.ToString().Contains("CIF"))
                        {
                            item.Icet_actl_rt = 0;
                        }
                        if (txtTradeTerm.Text.ToString().Contains("C&F"))
                        {
                            item.Icet_actl_rt = 0;
                        }
                    }
                }

                Session["oImportsCostElements"] = oImportsCostElements;
                BindOtherCostElements();
                BindCostElements();
            }
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                txtActualClearDate.Text = "";
            }
            else
            {
                txtActualClearDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            }
        }
        public decimal Exchngerat(string curr )
        {
                string SearchParams1 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable CompanyCurrancytbl = CHNLSVC.CommonSearch.SearchCompanyCurrancy(SearchParams1);
                DataTable ERateTbl = new DataTable();
                decimal exrt = 1;
                if (CompanyCurrancytbl.Rows.Count > 0)
                {
                  ERateTbl = CHNLSVC.Financial.GetExchangeRate(Session["UserCompanyCode"].ToString(), curr, CompanyCurrancytbl.Rows[0]["CURRENCY"].ToString());
                }
           
            if (ERateTbl != null)
            {
                if (ERateTbl.Rows.Count > 0)
                {
                    exrt =Convert.ToDecimal( ERateTbl.Rows[0][5].ToString());
                    
                }
               
            }

            return exrt;
        }
        protected void lbtnfileno_Click(object sender, EventArgs e)
        {
            try
            {
                string docno = txtBLNumber.Text.ToString();
                if (docno == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Please select record')", true);
                    return;
                }
                string com = Session["UserCompanyCode"].ToString();
                string fileno = txtFileNo.Text.ToString();

                Int32 effect = CHNLSVC.Sales.Update_filenoforCostsheet(com, docno, fileno);

                if (effect == 1)
                {
                    DispalyMessages("Successfully saved file no ", 2);
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnCostdetSave_Click(object sender, EventArgs e)
        {

            if (txtConfirmSave.Value == "Yes")
            {
                string _error = "";
                List<IMP_CST_ELEREF_DET> _subdata = Session["IMP_CST_ELEREF_DET"] as List<IMP_CST_ELEREF_DET>;
                int result = CHNLSVC.Financial.SaveIMP_CST_ELENew(oimp_cst_ele_refs, 5, _subdata, out _error);
                if (result == -1)
                {
                    DispalyMessages(_error, 1);
                    return;
                }
                else
                {
                    try
                    {

                        ImportsCostHeader oCostHeaderTmp = (ImportsCostHeader)Session["oCostHeader"];
                        if (oCostHeaderTmp != null)
                        {
                            ImportsCostHeader _obj = new ImportsCostHeader();
                            _obj.Ich_doc_no = oCostHeaderTmp.Ich_doc_no;
                            _obj.Ich_ref_stus = "S";
                            _obj.Ich_app_by = Session["UserID"].ToString();
                            _obj.Ich_app_dt = DateTime.Now;
                            _obj.Ich_fin_by = Session["UserID"].ToString();
                            _obj.Ich_fin_dt = DateTime.Now;
                            _obj.Ich_sav_by = Session["UserID"].ToString();
                            _obj.Ich_sav_dt = DateTime.Now;
                            int res = CHNLSVC.Financial.UpdateImportsCostHeaderRefSave(_obj, "SAVE", out _error);
                            if (res > 0)
                            {
                                btnCloseCostItem_Click(null, null);
                                // DispMsg("Finalized Successfully !", "S");
                            }
                            else
                            {
                                DispMsg(_error, "E");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DispMsg(ex.Message, "E");
                    }
                    DispalyMessages("Successfully saved  ", 2);
                    var _mailList = oimp_cst_ele_refs.Where(c => c.Tmp_is_New == 1).ToList();
                    if (dbtnActualPost.SelectedValue == "Actual")
                    {
                        SendCostDataMail(_mailList);
                        mpAddCostItems.Hide();
                    }
                    //  oimp_cst_ele_refs = null;
                }
            }
            else
            {
                return;
            }
        }

        protected void dbtnActualPost2_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpItemDetails.Show();
            // dgvitems_SelectedIndexChanged(null,null);
        }

        private void SendCostDataMail(List<imp_cst_ele_ref> _list)
        {
            string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string _cstData = "";
            ImportsCostHeader oCostHeaderTmp = (ImportsCostHeader)Session["oCostHeader"];
            foreach (var item in _list)
            {
                string tmpCstItm = "<tr style='padding: 0px'>" +
                "<td  style='padding border :1px solid black; text-align:left; padding-left: 10px; padding-right: 10px;'>" + item.Icer_doc_no + "</td>" +
                "<td  style='border :1px solid black; text-align:left; padding-left: 10px; padding-right: 10px;'>" + item.Icer_ref_no + "</td>" +
                "<td style='border :1px solid black; text-align:left; padding-left: 10px; padding-right: 10px;'>" + item.Icer_ref_dt.ToString("dd/MMM/yyyy") + "</td>" +
                "<td style='border :1px solid black; text-align:right; padding-left: 10px; padding-right: 10px;'>" + item.Icer_ref_amt.ToString("N2") + "</td>" +
                "<td style='border :1px solid black; text-align:left; padding-left: 10px; padding-right: 10px;'>" + item.Icer_ref_rmk + "</td>" +
                "</tr>";
                _cstData = _cstData + tmpCstItm;
            }
            string userName = "";
            DataTable dt = CHNLSVC.Inventory.GetUserNameByUserID(Session["UserID"].ToString());
            if (dt.Rows.Count > 0)
            {
                userName = dt.Rows[0]["se_usr_name"].ToString() + " ";
            }
            string emailBody = "<html style='width: 590px;'>" +
                "<body style='width: 450px; border: 2px solid rgb(156, 38, 204); padding: 15px; color: rgb(156, 38, 204);'>" +
                    "<h3 style='margin-bottom: 0px;'>Abans Infor Portal</h3>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<div style='color:#07000A; padding: 3px;'>" +
                            "<div><u>Cost Item Details</u></div><br/>" +
                            "<div>" +
                            "<div>Bond # : " + oCostHeaderTmp.Ich_ref_no + "</div>" +
                            "<div>BL # : " + oCostHeaderTmp.Ich_doc_no + "</div>" +
                            "<div>LC # : " + oCostHeaderTmp.Cuh_bank_ref_cd + "</div>" +
                           "<table style='border-collapse: collapse; padding: 1px; font-family:Arial;font-size:10pt'>" +
                           "<tr style='padding: 0px'>" +
                           "<th style='border :1px solid black; text-align:left; padding-left: 10px; padding-right: 10px;'>Charge</th>" +
                           "<th style='border :1px solid black; text-align:left; padding-left: 10px; padding-right: 10px;'>Reference</th>" +
                           "<th style='border :1px solid black; text-align:left; padding-left: 10px; padding-right: 10px;'>Date</th>" +
                           "<th style='border :1px solid black; text-align:right; padding-left: 10px; padding-right: 10px;'>Amount</th>" +
                           "<th style='border :1px solid black;text-align:left; padding-left: 10px; padding-right: 10px;'>Remark</th>" +
                           "</tr>" +
                           _cstData +
                           "</table>" +
                            "</div><br/>" +
                            "<div style='font-family:Arial;font-size:10pt'>User ID : " + Session["UserID"].ToString() + "</div>" +
                            "<div style='font-family:Arial;font-size:10pt' >User Name : " + userName + "</div>" +
                            "<div>** This is an auto generated mail from Abans infor portal. Please don't Reply **</div>" +
                        "</div><br/>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<span style='font-family:Arial;font-size:10pt'> </span></body>" +
                "</html>";
            emailBody = emailBody.Replace("@reportname", "Please find 'Miss match items");

            // CHNLSVC.Security.SendCostSheetEmail("Inoshi@abansgroup.com", "", emailBody, host);
            // CHNLSVC.Security.SendCostSheetEmail("asanka@abansgroup.com", "", emailBody, host);
            CHNLSVC.Security.SendCostSheetEmail("lakshan@abansgroup.com", "", emailBody, host);
            emailBody = "";
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                bool b10150 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10150);
                if (!b10150)
                {
                    DispMsg("Sorry, You have no permission to approve this order.( Advice: Required permission code : 10150) !!!", "N");
                    return;
                }
                string _error = "";
                ImportsCostHeader oCostHeaderTmp = (ImportsCostHeader)Session["oCostHeader"];
                if (oCostHeaderTmp != null)
                {
                    ImportsCostHeader _obj = new ImportsCostHeader();
                    _obj.Ich_doc_no = oCostHeaderTmp.Ich_doc_no;
                    _obj.Ich_ref_stus = "A";
                    _obj.Ich_app_by = Session["UserID"].ToString();
                    _obj.Ich_app_dt = DateTime.Now;
                    _obj.Ich_fin_by = Session["UserID"].ToString();
                    _obj.Ich_fin_dt = DateTime.Now;
                    _obj.Ich_sav_by = Session["UserID"].ToString();
                    _obj.Ich_sav_dt = DateTime.Now;
                    int res = CHNLSVC.Financial.UpdateImportsCostHeaderRefSave(_obj, "APPROVE", out _error);
                    if (res > 0)
                    {
                        btnCloseCostItem_Click(null, null);
                        DispMsg("Approved Successfully !", "S");
                    }
                    else
                    {
                        DispMsg(_error, "E");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void btnfinalize_Click(object sender, EventArgs e)
        {
            try
            {
                bool b10151 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10151);
                if (!b10151)
                {
                    DispMsg("Sorry, You have no permission.( Advice: Required permission code : 10151) !!!", "N");
                    return;
                }
                string _error = "";
                ImportsCostHeader oCostHeaderTmp = (ImportsCostHeader)Session["oCostHeader"];
                if (oCostHeaderTmp != null)
                {
                    ImportsCostHeader _obj = new ImportsCostHeader();
                    _obj.Ich_doc_no = oCostHeaderTmp.Ich_doc_no;
                    _obj.Ich_ref_stus = "F";
                    _obj.Ich_app_by = Session["UserID"].ToString();
                    _obj.Ich_app_dt = DateTime.Now;
                    _obj.Ich_fin_by = Session["UserID"].ToString();
                    _obj.Ich_fin_dt = DateTime.Now;
                    _obj.Ich_sav_by = Session["UserID"].ToString();
                    _obj.Ich_sav_dt = DateTime.Now;
                    int res = CHNLSVC.Financial.UpdateImportsCostHeaderRefSave(_obj, "FINALIZE", out _error);
                    if (res > 0)
                    {
                        btnCloseCostItem_Click(null, null);
                        DispMsg("Finalized Successfully !", "S");
                    }
                    else
                    {
                        DispMsg(_error, "E");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
        }

        protected void txtSerProv_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSea_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "Company")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    _serData = CHNLSVC.CommonSearch.SearchBusEntity(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim().ToUpper());
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                //txtSerByKey.Text = "";
                txtSerByKey.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
                _serPopShow = true;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
                }
                dgvResult.DataBind();
                txtSerByKey.Text = string.Empty;
                txtSerByKey.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "Company")
                {
                    txtSerProv.Text = code;
                    txtSerProv_TextChanged(null, null);
                }
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }


        private void OrderSearchGridData(string _colName, string _ordTp)
        {
            try
            {
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    string dvSortStr = _colName + " " + _ordTp;
                    dv.Sort = dvSortStr;
                    _serData = dv.ToTable();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        public void BindDdlSerByKey(DataTable _dataSource)
        {
            this.ddlSerByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey.SelectedIndex = 0;
        }
        private void LoadSearchPopup(string serType, string _colName, string _ordTp)
        {
            OrderSearchGridData(_colName, _ordTp);
            try
            {
                dgvResult.DataSource = new int[] { };
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        BindDdlSerByKey(_serData);
                    }
                }
                dgvResult.DataBind();
                txtSerByKey.Text = "";
                txtSerByKey.Focus();
                _serType = serType;
                PopupSearch.Show();
                _serPopShow = true;
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeSerProv_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                _serData = CHNLSVC.CommonSearch.SearchBusEntity(_para, null, null);
                LoadSearchPopup("Company", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSerClose_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Hide();
        }
        protected void btnCloseCostItem_Click(object sender, EventArgs e)
        {
            _showCostItem = false;
            mpAddCostItems.Hide();
        }

        private decimal GetPrevCostVal()
        {
            //decimal _cost = 
            return 0;
        }

        protected void lbtnSeContainer_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Container);
            DataTable result = CHNLSVC.CommonSearch.GetContainerNo(para, null, null);
            if (result.Rows.Count > 0)
            {
                DataView dv = result.DefaultView;
                dv.Sort = "CODE ASC";
                result = dv.ToTable();
            }
            Int32 count = result.Rows.Count;
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Container";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show();
        }

        protected void lbtnSeLcNo_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LcNo);
            DataTable result = CHNLSVC.CommonSearch.SEARCH_FIN_HDR(para, null, null);
            if (result.Rows.Count > 0)
            {
                for (int x = 0; x < result.Rows.Count; x++)
                {
                    DataRow dr = result.Rows[x];
                    if (x > 100)
                    {
                        dr.Delete();
                    }
                }
                result.AcceptChanges();
            }
            Int32 count = result.Rows.Count;
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "LcNo";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show();
        }

        protected void btnupdatecusdec_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirncusdecentry.Value.ToString() == "Yes")
                {
                    string entryno = txtEntryNum.Text.ToString();
                    string cusdecentryno = txtCustDecEntry.Text.ToString();
                    if (entryno == "")
                    {
                        DispMsg("Please Select Entry No");
                        return;
                    }
                    if (cusdecentryno == "")
                    {
                        DispMsg("Please Enter Cusdec Entry No");
                        return;
                    }
                    if (txtcusdecentrydate.Text == "")
                    {
                        DispMsg("Please Select Cusdec Entry Date");
                        return;
                    }
                    DateTime cusdecentrydate = Convert.ToDateTime(txtcusdecentrydate.Text.ToString());

                    //Update Function
                    int effect = CHNLSVC.Financial.UpdateCusdecEntryData(entryno, cusdecentryno, cusdecentrydate);
                    if (effect > 0)
                    {
                        DispMsg("Successfully Updated", "S");
                        return;
                    }
                }



            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
                return;
            }
        }

        protected void txtcusdecentrydate_TextChanged(object sender, EventArgs e)
        {
            DateTime temp;
            if (DateTime.TryParse(txtcusdecentrydate.Text, out temp))
            {
            }
            else
            {

                DispMsg("Please Enter Valid Cusdec Entry Date", "N");
                txtcusdecentrydate.Text = "";
                return;
            }
        }

        protected void btnelesub_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblICER_DOC_NO = dr.FindControl("lblICER_DOC_NO") as Label;
                Label lblICER_LINE = dr.FindControl("lblICER_LINE") as Label;
                Label LBLICER_ELE_LINE = dr.FindControl("LBLICER_ELE_LINE") as Label;
                Label lblICER_REF_AMT = dr.FindControl("lblICER_REF_AMT") as Label;
                TextBox txtvehreg = dr.FindControl("txtvehreg") as TextBox;
                string _cont = "";
                if (dpcontainer != null)
                {
                    _cont = dpcontainer.SelectedValue.ToString();
                }
                List<MST_COST_ELE> _subele = CHNLSVC.Financial.GetCostEleSubType(Session["UserCompanyCode"].ToString(), lblICER_DOC_NO.Text.ToString());
                List<ImportsBLContainer> _blcontai = new List<ImportsBLContainer>();
                if (_subele == null)
                {
                    _subele = new List<MST_COST_ELE>();
                }
                List<ImportsCostHeader> oHeaders = (List<ImportsCostHeader>)Session["oHeaders"];
                ImportsCostHeader oHeadersOB = oHeaders.Where(a => a.Ich_doc_no == txtBLNumber.Text.ToString()).ToList()[0];
                List<IMP_CST_ELEREF_DET> _subdata = new List<IMP_CST_ELEREF_DET>();
                if (Session["IMP_CST_ELEREF_DET"] == null)
                {
                    _subdata = new List<IMP_CST_ELEREF_DET>();
                }
                else
                {
                    _subdata = Session["IMP_CST_ELEREF_DET"] as List<IMP_CST_ELEREF_DET>;
                }

                List<IMP_CST_ELEREF_DET> _subdataold = CHNLSVC.Financial.GetCostEleSubDetails(oHeadersOB.Ich_seq_no, Convert.ToInt32(LBLICER_ELE_LINE.Text.ToString()));
                if (_subdata.Where(a => a.iced_ele_line == Convert.ToInt32(LBLICER_ELE_LINE.Text.ToString()) && a.iced_ref_line == Convert.ToInt32(lblICER_LINE.Text.ToString()) && a.iced_ref == _cont).ToList() != null && _subdata.Where(a => a.iced_ele_line == Convert.ToInt32(LBLICER_ELE_LINE.Text.ToString()) && a.iced_ref_line == Convert.ToInt32(lblICER_LINE.Text.ToString()) && a.iced_ref == _cont).ToList().Count > 0)
                {
                    DispMsg("Already Added", "N");
                    return;
                }
                foreach (var _list in _subele)
                {
                    IMP_CST_ELEREF_DET ob = new IMP_CST_ELEREF_DET();
                    ob.iced_cd = _list.Mcae_cd;
                    ob.totval = Convert.ToDecimal(lblICER_REF_AMT.Text.ToString());
                    ob.iced_ele_line = Convert.ToInt32(LBLICER_ELE_LINE.Text.ToString());
                    ob.iced_seq = oHeadersOB.Ich_seq_no;
                    ob.iced_ref_line = Convert.ToInt32(lblICER_LINE.Text.ToString());
                    ob.iced_act = 1;
                    ob.iced_cre_by = Session["UserID"].ToString();
                    ob.iced_cre_dt = DateTime.Now;
                    ob.iced_mod_by = Session["UserID"].ToString();
                    ob.iced_mod_dt = DateTime.Now;
                    ob.iced_veh_reg = txtvehreg.Text;

                    if (dpcontainer != null)
                    {
                        ob.container = dpcontainer.SelectedValue.ToString();
                        ob.iced_ref = dpcontainer.SelectedValue.ToString();
                        _cont = ob.iced_ref;
                    }

                    if (_subdataold == null)
                    {
                        _subdataold = new List<IMP_CST_ELEREF_DET>();
                    }
                    var _subdataold_ob = _subdataold.Where(a => a.iced_cd == _list.Mcae_cd && a.iced_ref_line == ob.iced_ref_line).ToList();
                    if (_subdataold_ob != null && _subdataold_ob.Count > 0)
                    {
                        ob.iced_amt = _subdataold_ob.First().iced_amt;
                    }


                    Int32 maxline = 0;
                    if (_subdata != null && _subdata.Count > 0)
                    {
                        maxline = _subdata.Max(a => a.iced_det_line);

                    }
                    ob.iced_det_line = maxline + 1;
                    _subdata.Add(ob);
                }

                Session["IMP_CST_ELEREF_DET"] = _subdata;
                grdelesubcost.DataSource = _subdata.Where(a => a.iced_ele_line == Convert.ToInt32(LBLICER_ELE_LINE.Text.ToString()) && a.iced_ref_line == Convert.ToInt32(lblICER_LINE.Text.ToString()) && a.iced_ref == _cont).ToList();
                grdelesubcost.DataBind();

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
                return;
            }
        }

        protected void txt_sub_amt_TextChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow dr = (sender as TextBox).NamingContainer as GridViewRow;
                TextBox txt_sub_amt = dr.FindControl("txt_sub_amt") as TextBox;
                Label lbliced_det_line = dr.FindControl("lbliced_det_line") as Label;
                Label lbliced_ele_line = dr.FindControl("lbliced_ele_line") as Label;
                Label lbiced_ref_line = dr.FindControl("lbiced_ref_line") as Label;

                decimal n;
                bool isNumeric = decimal.TryParse(txt_sub_amt.Text.ToString(), out n);
                if (isNumeric == false)
                {
                    DispMsg("Please Enter Valid Amount", "N");
                    txt_sub_amt.Text = "";
                    txt_sub_amt.Focus();
                    txt_sub_amt.ReadOnly = false;
                    return;
                }
                else
                {
                    List<IMP_CST_ELEREF_DET> _subdata = Session["IMP_CST_ELEREF_DET"] as List<IMP_CST_ELEREF_DET>;
                    var _total = _subdata.Where(a => a.iced_ele_line == Convert.ToInt32(lbliced_ele_line.Text.ToString()) && a.iced_ref_line == Convert.ToInt32(lbiced_ref_line.Text.ToString()) && a.iced_det_line == Convert.ToInt32(lbliced_det_line.Text.ToString())).Sum(a => a.iced_amt);
                    if (_total + Convert.ToDecimal(txt_sub_amt.Text.ToString()) > _subdata.Where(a => a.iced_ref_line == Convert.ToInt32(lbiced_ref_line.Text.ToString())).First().totval)
                    {
                        DispMsg("Cant Exceed Invoice Amount", "N");
                        txt_sub_amt.Text = "0";
                        txt_sub_amt.Focus();
                        return;
                    }
                    _subdata.Where(a => a.iced_det_line == Convert.ToInt32(lbliced_det_line.Text.ToString())).FirstOrDefault().iced_amt = Convert.ToDecimal(txt_sub_amt.Text.ToString());

                    Session["IMP_CST_ELEREF_DET"] = _subdata;
                    grdelesubcost.DataSource = _subdata.Where(a => a.iced_ele_line == Convert.ToInt32(lbliced_ele_line.Text.ToString()) && a.iced_ref_line == Convert.ToInt32(lbiced_ref_line.Text.ToString())).ToList(); ;
                    grdelesubcost.DataBind();

                }

            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
                return;
            }
        }

        protected void btnupdaterates_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtExRate.Text == "" || txtbuyingrt.Text == "")
                {
                    DispMsg("Please Enter Rates", "N");
                    return;
                }
                if (txtBLNumber.Text=="")
                {
                    DispMsg("Please Select SI No", "N");
                    return;
                }
                if(txtsgexrate.Text=="")
                {
                    txtsgexrate.Text = "0";
                }
                if (txtFreightRate.Text == "")
                {
                    txtFreightRate.Text = "0";
                }
                int effect = CHNLSVC.Financial.UpdateBuyingRates(txtBLNumber.Text, Convert.ToDecimal(txtbuyingrt.Text), Convert.ToDecimal(txtExRate.Text), Convert.ToDecimal(txtFreightRate.Text), txtRemarks.Text, Convert.ToDecimal(txtsgexrate.Text));
                if (effect > 0)
                {
                    DispMsg("Successfully Updated", "S");
                    return;
                }

            }catch(Exception ex)
            {
                DispMsg(ex.Message, "E");
                return;
            }
        }

        protected void txtsgexrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal n;
                bool isNumeric = decimal.TryParse(txtsgexrate.Text.ToString(), out n);
                if (isNumeric == false)
                {
                    DispMsg("Please Enter Valid Amount", "N");
                    return;
                }
                //else
                //{
                //    txtcostingrate.Text = txtsgexrate.Text.ToString();
                //}
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
                return;
            }
        }
    }
}