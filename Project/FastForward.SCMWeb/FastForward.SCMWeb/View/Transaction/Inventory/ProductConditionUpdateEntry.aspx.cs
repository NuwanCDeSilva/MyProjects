using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class ProductConditionUpdateEntry : BasePage
    {
        Int32 check = 0;
        private string OutwardNo = string.Empty;
        string _userid = string.Empty;
        List<int> successItems = new List<int>();
        List<int> successItemsAnal4 = new List<int>();
        List<int> hasstuschangelist = new List<int>();
        List<InventoryRequestItem> ScanItemList = new List<InventoryRequestItem>();
        protected PriceBookLevelRef _priceBookLevelRef { get { return (PriceBookLevelRef)Session["_priceBookLevelRef"]; } set { Session["_priceBookLevelRef"] = value; } }
        Int32 SeqNo = 0;

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

        protected List<ItemConditionSetup> _repConSetups
        {
            get { if (Session["_repConSetups"] != null) { return (List<ItemConditionSetup>)Session["_repConSetups"]; } else { return new List<ItemConditionSetup>(); } }
            set { Session["_repConSetups"] = value; }
        }
        protected List<ItemConditionSetup> _repMainConSetups
        {
            get { if (Session["_repMainConSetups"] != null) { return (List<ItemConditionSetup>)Session["_repMainConSetups"]; } else { return new List<ItemConditionSetup>(); } }
            set { Session["_repMainConSetups"] = value; }
        }
        protected List<RepConditionType> _repConTps = new List<RepConditionType>();
        protected RepConditionType _repConTp = new RepConditionType();
        protected ItemConditionSetup _repConSetup = new ItemConditionSetup();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //dilshan on 05/07/2018*****
                    Session["CHECK"] = "0";
                    //***********************
                    bool b16051 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16051);
                    txtMainConPrice.ReadOnly = true;
                    txtSubCharg.ReadOnly = true;
                    if (b16051)
                    {
                        txtMainConPrice.ReadOnly = false;
                        txtSubCharg.ReadOnly = false;
                    }
                    else
                    {
                        txtMainConPrice.ReadOnly = true;
                        txtSubCharg.ReadOnly = true;
                    }
                    txtSubConCat.Visible = false;
                    Session["CurrCode"] = "";
                    dvPendingPO.DataSource = new int[] { };
                    dvPendingPO.DataBind();

                    gvselecteditems.DataSource = new int[] { };
                    gvselecteditems.DataBind();

                    gvconditions.DataSource = new int[] { };
                    gvconditions.DataBind();

                    gvaddconditions.DataSource = new int[] { };
                    gvaddconditions.DataBind();

                    dgvCond.DataSource = new int[] { };
                    dgvCond.DataBind();

                    List<MasterCompany> ListCom = CHNLSVC.General.GetALLMasterCompaniesData();
                    if (ListCom != null)
                    {
                        var company = ListCom.Where(c => c.Mc_cd == Session["UserCompanyCode"].ToString()).FirstOrDefault();
                        if (company != null)
                        {
                            Session["CurrCode"] = company.Mc_cur_cd;
                            lbltotChgAmount.Text = lbltotChgAmount.Text + " (" + company.Mc_cur_cd + ")";
                            lblMainChg.Text = lblMainChg.Text + " (" + company.Mc_cur_cd + ") :";
                        }
                    }


                    DateTime orddate = DateTime.Now.AddDays(-7);
                    dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");

                    DateTime orddate1 = DateTime.Now;
                    dtpToDate.Text = orddate1.ToString("dd/MMM/yyyy");

                    BindRequestTypesDDLData();

                    Session["BASEDOC"] = null;

                    if ((CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10116)) && (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10117)))
                    {
                        Session["USER_TYPE"] = "HO";
                    }
                    else if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10116))
                    {
                        Session["USER_TYPE"] = "WH";
                    }
                    else if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10117))
                    {
                        Session["USER_TYPE"] = "HO";
                    }

                    ViewState["Conditions"] = null;

                    DataTable dtconditions = new DataTable();
                    //dtconditions.Columns.AddRange(new DataColumn[11] { new DataColumn("its_itm_cd"), new DataColumn("irsc_ser_id"), new DataColumn("irsc_cat"), new DataColumn("irsc_tp"), new DataColumn("rct_desc"), new DataColumn("irsc_rmk"), new DataColumn("irsc_cre_by"), new DataColumn("irsc_stus"), new DataColumn("rct_cat1"), new DataColumn("irsc_cnl_by"), new DataColumn("irsc_cnl_dt") });
                    dtconditions.Columns.AddRange(new DataColumn[12] { new DataColumn("its_itm_cd"), new DataColumn("irsc_ser_id"), new DataColumn("irsc_cat"), new DataColumn("irsc_tp"), new DataColumn("irsc_rmk"), new DataColumn("irsc_stus"), new DataColumn("irsc_cre_by"), new DataColumn("irsc_cnl_dt"), new DataColumn("irsc_cnl_by"), new DataColumn("rct_desc"), new DataColumn("rct_cat1"), new DataColumn("status") });
                    ViewState["Conditions"] = dtconditions;
                    this.BindGrid();
                    btnGetPO_Click(null, null);
                    _repConSetups = new List<ItemConditionSetup>();
                    BindConditionTp();
                    LoadTotal();
                    isConventionalCheckBox.Checked = true;//Added by dulaj 2018/mar/26
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

        protected void BindGrid()
        {
            try
            {
                gvconditions.DataSource = (DataTable)ViewState["Conditions"];
                gvconditions.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void BindRequestTypesDDLData()
        {
            try
            {
                DataTable dttypes = CHNLSVC.Inventory.LoadProductConditionUpdateDocs();
                if (dttypes.Rows.Count > 0)
                {
                    ddlType.DataSource = dttypes;
                    ddlType.DataTextField = "mtp_desc";
                    ddlType.DataValueField = "mtp_map_doc_tp";
                    ddlType.DataBind();
                }
                ddlType.Items.Insert(0, new ListItem("Select", "0"));
                ddlType.SelectedIndex = 0;
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

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "429")
                {
                    string filter = (string)Session["USER_TYPE"];

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate);
                    DataTable result = CHNLSVC.Inventory.LoadProductConditionNew(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, filter);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "429";
                    ViewState["SEARCH"] = result;
                    grdResult.PageIndex = 0;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "1000")
                {

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
                grdResult.PageIndex = 0;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "429")
                {
                    string docno = grdResult.SelectedRow.Cells[1].Text;

                    txtAODNumber.Text = docno;

                    InventoryHeader _invHdr = new InventoryHeader();
                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(docno);

                    DataTable dtresultcopyIntr = new DataTable();
                    dtresultcopyIntr.Columns.AddRange(new DataColumn[4] { new DataColumn("ith_doc_no"), new DataColumn("ith_doc_date"), new DataColumn("ith_oth_docno"), new DataColumn("ith_oth_loc") });

                    DateTime date = _invHdr.Ith_doc_date;

                    string formatteddate = ((DateTime)date).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                    dtresultcopyIntr.Rows.Add(_invHdr.Ith_doc_no, formatteddate, _invHdr.Ith_oth_docno, _invHdr.Ith_oth_loc);

                    dvPendingPO.DataSource = null;
                    dvPendingPO.DataBind();

                    dvPendingPO.DataSource = dtresultcopyIntr;
                    dvPendingPO.DataBind();
                    LoadGridStatus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                ViewState["SEARCH"] = null;
            }
            catch
            {

            }
        }

        protected void lbtnaddinvitems_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtconditions1 = new DataTable();

                string catetype = (string)Session["ITEM_CATE"];

                gvaddconditions.DataSource = null;
                gvaddconditions.DataBind();

                dtconditions1.Clear();
                dtconditions1 = CHNLSVC.Inventory.LoadConditionsForCategory(Session["UserCompanyCode"].ToString(), "CT003", catetype);

                if (dtconditions1.Rows.Count > 0)
                {
                    gvaddconditions.DataSource = dtconditions1;
                    gvaddconditions.DataBind();
                    MpDelivery.Show();
                }
                else
                {
                    dtconditions1.Clear();
                    dtconditions1 = CHNLSVC.Inventory.LoadConditionsForAllCategory(Session["UserCompanyCode"].ToString(), "CT003");
                    if (dtconditions1.Rows.Count > 0)
                    {
                        gvaddconditions.DataSource = dtconditions1;
                        gvaddconditions.DataBind();

                        MpDelivery.Show();
                    }
                    else
                    {
                        gvaddconditions.DataSource = null;
                        gvaddconditions.DataBind();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No further conditions found for the item category " + catetype + " !!!')", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void chkUpdatatedType_CheckedChanged(object sender, EventArgs e)
        {
            btnGetPO_Click(null, null);
        }
        protected void btnGetPO_Click(object sender, EventArgs e)
        {
            try
            {
                gvselecteditems.DataSource = new int[] { };
                gvselecteditems.DataBind();
                bool b10116 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10116);
                if (!b10116)
                {
                    bool b10117 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10117);
                    if (!b10117)
                    {
                        DispMsg("Sorry, You have no permission! ( Advice: Required permission code : 10116/10117)"); return;
                    }
                }
                if (!string.IsNullOrEmpty(txtdirectserial.Text.Trim()))
                {
                    PopulateItemsGridDirectlyBySerial(txtdirectserial.Text.Trim(), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                }
                else
                {
                    string filter = (string)Session["USER_TYPE"];

                    string type = ddlType.SelectedValue;

                    if (type == "0")
                    {
                        type = string.Empty;
                    }

                    DateTime fromdate = Convert.ToDateTime(dtpFromDate.Text.Trim());
                    DateTime todate = Convert.ToDateTime(dtpToDate.Text.Trim());
                    Int32 isUpdated = (chkUpdatedType.Checked) ? 1 : 0;
                    if (fromdate > todate)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid date range !!!')", true);
                        dvPendingPO.DataSource = null;
                        dvPendingPO.DataBind();
                        return;
                    }

                    DataTable dtheader = new DataTable();

                    if (filter == "WH")
                    {
                        dtheader = CHNLSVC.Inventory.LoadHeaderPCUpdateDocWareHouseUserNew(type, Convert.ToDateTime(dtpFromDate.Text.Trim()), Convert.ToDateTime(dtpToDate.Text.Trim()), Session["UserDefLoca"].ToString(), isUpdated,
                            string.IsNullOrEmpty(txtCat1.Text) ? null : txtCat1.Text.Trim().ToUpper(),
                            string.IsNullOrEmpty(txtCat2.Text) ? null : txtCat2.Text.Trim().ToUpper(),
                            string.IsNullOrEmpty(txtCat3.Text) ? null : txtCat3.Text.Trim().ToUpper(),
                            txtAODNumber.Text.Trim());


                    }
                    else if (filter == "HO")
                    {
                        dtheader = CHNLSVC.Inventory.LoadHeaderPCUpdateDocHeadOfficeUserNew(type, Convert.ToDateTime(dtpFromDate.Text.Trim()), Convert.ToDateTime(dtpToDate.Text.Trim()), Session["UserDefLoca"].ToString(), isUpdated,
                            string.IsNullOrEmpty(txtCat1.Text) ? null : txtCat1.Text.Trim().ToUpper(),
                            string.IsNullOrEmpty(txtCat2.Text) ? null : txtCat2.Text.Trim().ToUpper(),
                            string.IsNullOrEmpty(txtCat3.Text) ? null : txtCat3.Text.Trim().ToUpper(),
                            txtAODNumber.Text.Trim());
                    }
                    if (dtheader != null)
                    {
                        if (dtheader.Rows.Count > 0)
                        {
                            DataView dv = dtheader.DefaultView;
                            dv.Sort = "ith_doc_date DESC";
                            dtheader = dv.ToTable();
                        }
                    }
                    dvPendingPO.DataSource = null;
                    dvPendingPO.DataBind();

                    dvPendingPO.DataSource = dtheader;
                    dvPendingPO.DataBind();
                    LoadGridStatus();
                    for (int i = 0; i < dvPendingPO.Rows.Count; i++)
                    {
                        dvPendingPO.Rows[i].BackColor = Color.White;
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

        protected void lbtnsearchrec_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                string filter = (string)Session["USER_TYPE"];

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate);
                DataTable result = CHNLSVC.Inventory.LoadProductConditionNew(SearchParams, null, null, filter);
                //if (result.Rows.Count > 0)
                //{
                //    DataView dv = result.DefaultView;
                //    dv.Sort = "ith_doc_date DESC";
                //    result = dv.ToTable();
                //}
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "429";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                grdResult.PageIndex = 0;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate:
                    {
                        string type = ddlType.SelectedValue;

                        if (type == "0")
                        {
                            type = string.Empty;
                        }
                        paramsText.Append(type + seperator + Session["UserDefLoca"].ToString() + seperator + dtpFromDate.Text.ToString() + seperator + dtpToDate.Text.ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCat1.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCat1.Text.ToUpper() + seperator + txtCat2.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        if (_serType == "Promotion")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "CT003" + seperator + "1" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "CT003" + seperator + "0" + seperator + lblCat1.Text + seperator + lblCat2.Text + seperator + lblCat3.Text + seperator);
                            break;
                        }
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void PopulateItemsGrid(string docno, Int32 isserialized, string serialno)
        {
            try
            {
                DataTable dtitemsdate = CHNLSVC.Inventory.LoadDocumnetItems(docno, isserialized, serialno);

                gvselecteditems.DataSource = null;
                gvselecteditems.DataBind();
                gvselecteditems.DataSource = dtitemsdate;
                gvselecteditems.DataBind();
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

        private void PopulateItemsGridDirectlyBySerial(string serial, string company, string location)
        {
            try
            {
                DataTable dtitemsgriddirectly = CHNLSVC.Inventory.LoadItemDataBySerial(serial, company, location,
                     string.IsNullOrEmpty(txtCat1.Text) ? null : txtCat1.Text.Trim().ToUpper(),
                            string.IsNullOrEmpty(txtCat2.Text) ? null : txtCat2.Text.Trim().ToUpper(),
                            string.IsNullOrEmpty(txtCat3.Text) ? null : txtCat3.Text.Trim().ToUpper());

                if (dtitemsgriddirectly.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid serial or serial is not available at the current location !!!')", true);
                    txtdirectserial.Text = string.Empty;
                    return;
                }

                DataTable uniqueColscurrency = RemoveDuplicateRows(dtitemsgriddirectly, "its_itm_stus");

                gvselecteditems.DataSource = null;
                gvselecteditems.DataBind();

                gvselecteditems.DataSource = uniqueColscurrency;
                gvselecteditems.DataBind();

                dvPendingPO.DataSource = null;
                dvPendingPO.DataBind();
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
        protected void dvPendingPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvselecteditems.Rows.Count > 0)
                {
                    for (int i = 0; i < gvselecteditems.Rows.Count; i++)
                    {
                        gvselecteditems.Rows[i].BackColor = Color.White;
                    }
                }
                _repConSetups = new List<ItemConditionSetup>();
                if (dvPendingPO.Rows.Count > 0)
                {
                    Int32 rowindex = dvPendingPO.SelectedRow.RowIndex;

                    if (txtconfirmselectnew.Value == "Yes")
                    {
                        gvconditions.DataSource = null;
                        gvconditions.DataBind();

                        chkser.Checked = false;
                        txtserial.Text = string.Empty;
                        txtItmCode.Text = string.Empty;
                        lblstus.Text = string.Empty;
                        txtserial.Text = string.Empty;
                        lblmodel.Text = string.Empty;
                        txtGrnNo.Text = "";
                        txtGrnDate.Text = "";
                        txtgranno.Text = "";
                        lblSerId.Text = "";
                        txtCondition.Text = "";
                        txtMainConPrice.Text = "";
                        lblCharge.Text = "";
                        TextDesc.Text = "";
                        txtConRemarks.Text = "";
                        txtGrnNo.Text = "";
                        txtGrnDate.Text = "";
                        txtNewStockSts.Text = "";
                        lblTotal.Text = "";
                        pnlCondition.Enabled = false;
                        pnlInitialFeedBack.Enabled = true;
                        _repConSetups = new List<ItemConditionSetup>();
                        BindConditionTp();

                        OutwardNo = (dvPendingPO.SelectedRow.FindControl("lbldocno") as Label).Text;
                        Session["BASEDOC"] = OutwardNo;
                        string OtherLoc = (dvPendingPO.SelectedRow.FindControl("lblothloc") as Label).Text;
                        Session["OtherLoc"] = OtherLoc;
                        Int32 isserializedcheck = 0;

                        if (chkser.Checked == true)
                        {
                            isserializedcheck = 1;
                        }

                        PopulateItemsGrid(OutwardNo, isserializedcheck, txtserial.Text.Trim());
                        txtconfirmselectnew.Value = "";

                        for (int i = 0; i < dvPendingPO.Rows.Count; i++)
                        {
                            dvPendingPO.Rows[i].BackColor = Color.White;
                        }

                        dvPendingPO.Rows[rowindex].BackColor = Color.Silver;
                    }
                    else if (txtconfirmselectnew.Value == "No")
                    {
                        dvPendingPO.Rows[rowindex].BackColor = Color.White;
                        txtconfirmselectnew.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void Clear()
        {
            try
            {
                Session["ITEM_CODE"] = null;
                Session["ITEM_CATE"] = null;
                Session["SERIAL"] = null;
                Session["SEQNO"] = null;
                Session["CHECK"] = "0";
                ddlType.SelectedIndex = 0;

                DateTime orddate = DateTime.Now.AddMonths(-1);
                dtpFromDate.Text = orddate.ToString("dd/MMM/yyyy");

                DateTime orddate1 = DateTime.Now;
                dtpToDate.Text = orddate1.ToString("dd/MMM/yyyy");

                txtAODNumber.Text = string.Empty;

                dvPendingPO.DataSource = null;
                dvPendingPO.DataBind();

                gvselecteditems.DataSource = null;
                gvselecteditems.DataBind();

                gvconditions.DataSource = null;
                gvconditions.DataBind();

                gvaddconditions.DataSource = null;
                gvaddconditions.DataBind();

                chkser.Checked = false;
                txtserial.Text = string.Empty;
                txtItmCode.Text = string.Empty;
                lblstus.Text = string.Empty;
                txtserial.Text = string.Empty;
                txtGrnNo.Text = "";
                txtGrnDate.Text = "";
                lblmodel.Text = string.Empty;
                txtdirectserial.Text = string.Empty;
                ScanItemList.Clear();
                successItems.Clear();
                successItemsAnal4.Clear();
                ViewState["Conditions"] = null;
                ViewState["SerialList"] = null;
                hasstuschangelist.Clear();
                txtgranno.Text = "";
                DataTable dtconditions = new DataTable();
                dtconditions.Columns.AddRange(new DataColumn[12] { new DataColumn("its_itm_cd"), new DataColumn("irsc_ser_id"), new DataColumn("irsc_cat"), new DataColumn("irsc_tp"), new DataColumn("irsc_rmk"), new DataColumn("irsc_stus"), new DataColumn("irsc_cre_by"), new DataColumn("irsc_cnl_dt"), new DataColumn("irsc_cnl_by"), new DataColumn("rct_desc"), new DataColumn("rct_cat1"), new DataColumn("status") });
                ViewState["Conditions"] = dtconditions;
                this.BindGrid();

                txtCondition.Text = "";
                txtMainConPrice.Text = "";
                lblCharge.Text = "";
                txtConRemarks.Text = "";
                TextDesc.Text = "";

                txtSubCon.Text = "";
                txtSubConCat.Text = "";
                txtSubCharg.Text = "";
                txtSubRemark.Text = "";

                txtCat1.Text = "";
                txtCat2.Text = "";
                txtCat3.Text = "";

                lblTotal.Text = "";
                txtNewStockSts.Text = "";

                _repConSetups = new List<ItemConditionSetup>();
                BindConditionTp();
                LoadTotal();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
                return;
            }
        }
        protected void dvPendingPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dvPendingPO, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[0].Attributes["style"] = "cursor:pointer";

                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    e.Row.Cells[0].Attributes["onclick"] = "ConfirmSelect();";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
        }

        protected void txtserial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OutwardNo = (string)Session["BASEDOC"];

                if (string.IsNullOrEmpty(OutwardNo))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a document !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtserial.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a serial # !!!')", true);
                    txtserial.Focus();
                    return;
                }

                Int32 isserializedcheck = 0;

                if (chkser.Checked == true)
                {
                    isserializedcheck = 1;
                }

                PopulateItemsGrid(OutwardNo, isserializedcheck, txtserial.Text.Trim());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
        }
        protected void gvselecteditems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvselecteditems, "Select$" + e.Row.RowIndex);
                    // e.Row.Cells[0].Attributes["style"] = "cursor:pointer";
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblpick = e.Row.FindControl("lblitspick") as Label;

                    if ((lblpick.Text == "1") || (lblpick.Text == "2"))
                    {
                        // e.Row.BackColor = System.Drawing.Color.LightPink;
                    }

                    DropDownList ddlnewstatus = (e.Row.FindControl("ddlnewstus") as DropDownList);
                    DataTable _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
                    if (_CompanyItemStatus.Rows.Count > 0)
                    {
                        ddlnewstatus.DataSource = _CompanyItemStatus;
                        ddlnewstatus.DataTextField = "MIS_DESC";
                        ddlnewstatus.DataValueField = "MIC_CD";
                        ddlnewstatus.DataBind();
                    }
                    string status = (e.Row.FindControl("lblstatusvalue") as Label).Text;
                    ddlnewstatus.Items.FindByValue(status).Selected = true;
                }

                string filter = (string)Session["USER_TYPE"];

                if (filter == "WH")
                {
                    foreach (GridViewRow hiderowbtn in this.gvselecteditems.Rows)
                    {
                        DropDownList ddlnewstus = (DropDownList)hiderowbtn.FindControl("ddlnewstus");
                        ddlnewstus.Enabled = false;
                        //gvselecteditems.Columns[8].Visible = false;
                    }
                }
                else if (filter == "HO")
                {
                    foreach (GridViewRow hiderowbtn in this.gvselecteditems.Rows)
                    {
                        DropDownList ddlnewstus = (DropDownList)hiderowbtn.FindControl("ddlnewstus");
                        ddlnewstus.Enabled = true;
                        //gvselecteditems.Columns[8].Visible = true;
                    }
                }
                else
                {
                    foreach (GridViewRow hiderowbtn in this.gvselecteditems.Rows)
                    {
                        DropDownList ddlnewstus = (DropDownList)hiderowbtn.FindControl("ddlnewstus");
                        ddlnewstus.Enabled = false;
                        //gvselecteditems.Columns[8].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
        }

        protected void gvselecteditems_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvselecteditems.Rows.Count; i++)
            {
                gvselecteditems.Rows[i].BackColor = Color.White;
            }
            try
            {
                if (gvselecteditems.Rows.Count > 0)
                {
                    gvselecteditems.Rows[gvselecteditems.SelectedRow.RowIndex].BackColor = Color.Silver;
                    string itemcode = (gvselecteditems.SelectedRow.FindControl("lblitmcode") as Label).Text;
                    string itemstatus = (gvselecteditems.SelectedRow.FindControl("lblstus") as Label).Text;
                    string itemser = (gvselecteditems.SelectedRow.FindControl("lblser1") as Label).Text;
                    string itemmodel = (gvselecteditems.SelectedRow.FindControl("lblmodelselect") as Label).Text;
                    string serialid = (gvselecteditems.SelectedRow.FindControl("lblserialid") as Label).Text;
                    string itemcate = (gvselecteditems.SelectedRow.FindControl("lblcate") as Label).Text;
                    //string _grnNo = (gvselecteditems.SelectedRow.FindControl("lblITS_ORIG_GRNNO") as Label).Text;

                    Session["ITEM_CATE"] = itemcate;
                    Session["ITEM_CODE"] = itemcode;
                    Session["SERIAL"] = serialid;
                    Session["CHECK"] = "0";

                    txtItmCode.Text = itemcode;
                    lblstus.Text = itemstatus;
                    txtserial.Text = itemser;
                    lblmodel.Text = itemmodel;

                    lblSerId.Text = serialid;
                    txtCondition.Text = "";
                    txtMainConPrice.Text = "";
                    TextDesc.Text = "";
                    txtConRemarks.Text = "";
                    txtSubCon.Text = "";
                    txtSubConCat.Text = "";
                    txtSubCharg.Text = "";
                    txtSubRemark.Text = "";
                    string docno = "";


                    txtNewStockSts.Enabled = true;
                    LinkButton2.Enabled = true;
                    txtNewStockSts.Text = "";

                    if (Session["BASEDOC"] != null)
                    {
                        docno = Session["BASEDOC"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select doxument number !!!')", true);
                        return;
                    }


                    //check inr loc bal and inr_batch bal

                    DataTable dtinrbatch = CHNLSVC.Inventory.GetFreqtyForLocation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), itemstatus, itemcode, docno, serialid);
                    //ataTable dtinrloc = CHNLSVC.Inventory.GetFreeoriginalqtyForLocation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), itemstatus, itemcode);
                    Int32 batchqty = 0;
                    Int32 locqty = 0;

                    if (dtinrbatch != null)
                    {
                        if (dtinrbatch.Rows.Count == 0)
                        {

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial not available in the system !!!')", true);
                            return;
                        }
                    }

                    //if (dtinrbatch != null && dtinrloc != null)
                    //{
                    //    if (dtinrbatch.Rows.Count > 0 && dtinrloc.Rows.Count > 0)
                    //    {
                    //        batchqty = Convert.ToInt32(dtinrbatch.Rows[0][0].ToString());
                    //        locqty = Convert.ToInt32(dtinrloc.Rows[0][0].ToString());

                    //        if (batchqty > locqty)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Check Location Balance !!!')", true);
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Check Location Balance !!!')", true);
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Check Location Balance !!!')", true);
                    //    return;
                    //}


                    InventorySerialN _ser = CHNLSVC.Inventory.Get_INT_SER_DATA(new InventorySerialN()
                        {
                            Ins_itm_cd = itemcode,
                            Ins_ser_1 = txtserial.Text.Trim(),
                            Ins_ser_id = Convert.ToInt32(lblSerId.Text.Trim()),
                            Ins_doc_no = Session["BASEDOC"].ToString()
                        }).FirstOrDefault();
                    InventorySerialN _ser2 = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                    {
                        Ins_itm_cd = itemcode,
                        Ins_ser_1 = txtserial.Text.Trim(),
                        Ins_ser_id = Convert.ToInt32(lblSerId.Text.Trim()),
                        Ins_doc_no = Session["BASEDOC"].ToString(),
                        Ins_available = 1
                    }).FirstOrDefault();
                    if (_ser != null)
                    {
                        txtGrnNo.Text = _ser.Ins_orig_grnno;
                        txtGrnDate.Text = _ser.Ins_orig_grndt.ToString("dd/MMM/yyyy");
                    }
                    if (_ser2 == null)
                    {
                        DispMsg("No Serial Available !!!");
                        return;
                    }
                    else
                    {
                        if (_ser2.Ins_available != 1)
                        {
                            DispMsg("No Serial Available !!!");
                            return;
                        }
                        else
                        {
                            if (_ser2.Ins_doc_no != Session["BASEDOC"].ToString())
                            {
                                DispMsg("No Serial Available !!!");
                                return;
                            }
                        }

                    }
                    //get grna num
                    InventoryHeader _invHdr1 = new InventoryHeader();
                    _invHdr1 = CHNLSVC.Inventory.Get_Int_Hdr(Session["BASEDOC"].ToString());
                    if (_invHdr1 != null)
                    {
                        if (_invHdr1.Ith_oth_docno.Contains("GRNA"))
                        {
                            txtGrnNo.Text = _invHdr1.Ith_oth_docno;
                        }
                        else
                        {
                            _invHdr1 = CHNLSVC.Inventory.Get_Int_Hdr(_invHdr1.Ith_oth_docno);
                            if (_invHdr1 != null)
                            {
                                if (_invHdr1.Ith_oth_docno.Contains("GRAN"))
                                {
                                    txtgranno.Text = _invHdr1.Ith_oth_docno;
                                }
                                else
                                {
                                    txtgranno.Text = "";

                                }
                            }

                        }
                    }


                    MasterItem _mstItem = CHNLSVC.General.GetItemMaster(itemcode);
                    if (_mstItem != null)
                    {
                        lblCat1.Text = string.IsNullOrEmpty(_mstItem.Mi_cate_1) ? "" : _mstItem.Mi_cate_1 == "N/A" ? "" : _mstItem.Mi_cate_1;
                        lblCat2.Text = string.IsNullOrEmpty(_mstItem.Mi_cate_2) ? "" : _mstItem.Mi_cate_2 == "N/A" ? "" : _mstItem.Mi_cate_2;
                        lblCat3.Text = string.IsNullOrEmpty(_mstItem.Mi_cate_3) ? "" : _mstItem.Mi_cate_3 == "N/A" ? "" : _mstItem.Mi_cate_3;
                    }
                    DataTable dt = (DataTable)ViewState["Conditions"];

                    DataTable dtitemconditions = CHNLSVC.Inventory.LoadItemConditions(serialid, itemcate);

                    if (dt.Rows.Count > 0)
                    {
                        dtitemconditions.Merge(dt);
                    }

                    DataTable dtColsUnique = new DataTable();

                    if (dtitemconditions.Rows.Count > 0)
                    {
                        dtColsUnique = dtitemconditions.AsEnumerable()
           .GroupBy(r => new { Col1 = r["its_itm_cd"], Col2 = r["irsc_tp"] })
           .Select(g => g.OrderBy(r => r["its_itm_cd"]).First())
           .CopyToDataTable();

                        gvconditions.DataSource = dtColsUnique;
                        gvconditions.DataBind();

                        ViewState["Conditions"] = dtColsUnique;
                    }
                    else
                    {
                        gvconditions.DataSource = dtColsUnique;
                        gvconditions.DataBind();

                        ViewState["Conditions"] = dtColsUnique;
                    }
                    BindConditionTp();
                    lbtnaddinvitems.Focus();

                }
                isConventionalCheckBox.Enabled = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
        }

        protected void gvconditions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlitmconditionstus = (e.Row.FindControl("ddlconditstus") as DropDownList);
                    string status = (e.Row.FindControl("lblstatusvalueitmconditions") as Label).Text;
                    ddlitmconditionstus.Items.FindByValue(status).Selected = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtsaveconfirm.Value == "Yes")
            {
                string adj = string.Empty;
                try
                {
                    if (_repConSetups.Count > 0)
                    {
                        for (int i = 0; i < _repConSetups.Count-1; i++)
                        {
                            if (!_repConSetups[i].StatusText.Trim().Equals(_repConSetups[i + 1].StatusText.Trim()))
                            {
                                txtNewStockSts.Text = "Changed";
                            }
                        }
                    }
                    else
                    {
                        DispMsg("Please Add Item Conditions !!!"); return;
                    }
                    
                    if (lblstus.Text == txtNewStockSts.Text.ToUpper().Trim())
                    {
                        DispMsg("New stock status cannot be similar to the item status !!!"); return;
                    }
                    #region Variables & Validations

                    _userid = (string)Session["UserID"];
                    string filter = (string)Session["USER_TYPE"];
                    string addedloc = string.Empty;
                    string doc = (string)Session["BASEDOC"];
                    string catetype = (string)Session["ITEM_CATE"];
                    string serialid = (string)Session["SERIAL"];
                    string okmsg = string.Empty;

                    Int32 results = 0;
                    Int32 ItsPick = 0;
                    string anal4 = string.Empty;

                    if (filter == "HO")
                    {
                        addedloc = "H";
                        ItsPick = 2;
                        anal4 = "H";
                    }
                    else if (filter == "WH")
                    {
                        addedloc = "W";
                        ItsPick = 1;
                        anal4 = "W";
                    }

                    if (string.IsNullOrEmpty(txtCondition.Text) && _repConSetups == null)
                    {
                        txtCondition.Focus(); DispMsg("Please select a condition "); return;
                    }

                    #endregion
                    //return;
                    #region Status Change

                    if (string.IsNullOrEmpty(txtNewStockSts.Text))
                    {
                        // DispMsg("Please select a new status "); txtNewStockSts.Focus(); return;
                    }
                    if (string.IsNullOrEmpty(txtItmCode.Text))
                    {
                        DispMsg("Please select a item "); return;
                    }
                    if (Session["SEQNO"] != null)
                    {
                        SeqNo = (Int32)Session["SEQNO"];
                    }
                    else
                    {
                        SeqNo = GenerateNewUserSeqNo();
                        Session["SEQNO"] = SeqNo;
                    }
                    List<ItemConditionSetup> _tmpList = new List<ItemConditionSetup>();
                    if (_repConSetups == null || _repConSetups.Count == 0)
                    {

                        _tmpList = _repConSetups;
                        if (string.IsNullOrEmpty(txtCondition.Text))
                        {
                            DispMsg("Please add condition !!!"); return;
                        }
                        if (string.IsNullOrEmpty(txtConRemarks.Text))
                        {
                            DispMsg("Please add remark !!!"); return;
                        }

                    }
                    bool isexits = _repConSetups.Exists(x => x.irsc_ser_id == Convert.ToInt32(lblSerId.Text) && x.ItemCode == txtItmCode.Text && x.irsc_tp == txtCondition.Text);
                    if (!isexits)
                    {
                        _repConSetup = new ItemConditionSetup();
                        _repConSetup.irsc_tp = txtCondition.Text.Trim();
                        _repConSetup.tmpCondescription = TextDesc.Text.Trim();
                        _repConSetup.irsc_ser_id = Convert.ToInt32(lblSerId.Text);
                        _repConSetup.irsc_rmk = txtConRemarks.Text;
                        _repConSetup.tmpRemark = _repConSetup.irsc_rmk.Length > 60 ? _repConSetup.irsc_rmk.Substring(0, 50) : _repConSetup.irsc_rmk;
                        _repConSetup.irsc_cha = Convert.ToDecimal(txtMainConPrice.Text.ToString());
                        _repConSetup.ItemCode = txtItmCode.Text.ToString();
                        _repConSetup.StatusText = lblstus.Text.ToString();
                        _repConSetup.ItemSearial = txtserial.Text.ToString();
                        _tmpList.Add(_repConSetup);
                        _repConSetups = _tmpList;
                    }

                    if (_repConSetups != null)
                    {
                        Int32 emptyItm = CHNLSVC.Inventory.emptyTempSerialItemsansSerials(SeqNo);
                        if (_repConSetups.Count > 0)
                        {
                            string nextserial = "";
                            _repConSetups = _repConSetups.OrderBy(a => a.ItemSearial).ToList();
                            int index = 0;
                            foreach (var item in _repConSetups)
                            {
                                string oldStatus = "";
                                foreach (GridViewRow serialList in gvselecteditems.Rows)
                                {
                                    Label serId = serialList.FindControl("lblserialid") as Label;
                                    if(serId.Text!=null || serId.Text != string.Empty)
                                    {
                                        if(item.irsc_ser_id== Int32.Parse(serId.Text.Trim()))
                                        {
                                            Label Oldstatus = serialList.FindControl("lblstatusvalue") as Label;
                                            oldStatus = Oldstatus.Text;
                                        }
                                    }                                   
                                   
                                }
                                AddItem(item.ItemCode, lblTotal.Text.Trim(),oldStatus, "1", SeqNo.ToString(), item.ItemSearial, item.StatusText);
                                if (item.ItemSearial != "N/A")
                                {
                                    if (nextserial == "")
                                    {
                                        AddSerials(item.ItemCode, item.ItemSearial, SeqNo.ToString(), oldStatus, 1, _repConSetups[index + 1].StatusText.ToUpper());

                                        nextserial = item.ItemSearial;
                                    }
                                    else
                                    {
                                        if (nextserial != item.ItemSearial)
                                        {
                                            AddSerials(item.ItemCode, item.ItemSearial, SeqNo.ToString(), oldStatus, 1, _repConSetups[index + 1].StatusText.ToUpper());
                                            nextserial = item.ItemSearial;
                                        }
                                    }
                                }
                                else
                                {

                                    AddSerials(item.ItemCode, item.ItemSearial, SeqNo.ToString(), oldStatus, 1, item.StatusText.Trim().ToUpper());
                                }
                                AddItem(item.ItemCode, lblTotal.Text.Trim(), oldStatus, "1", SeqNo.ToString(), item.ItemSearial, item.StatusText.Trim().ToUpper());
                                hasstuschangelist.Add(1);
                                index++;
                            }


                        }
                        else
                        {
                            //AddItem(txtItmCode.Text, lblTotal.Text.Trim(), lblstus.Text, "1", SeqNo.ToString(), txtserial.Text, txtNewStockSts.Text.Trim().ToUpper());
                            //AddSerials(txtItmCode.Text, txtserial.Text, SeqNo.ToString(), lblstus.Text, 1, txtNewStockSts.Text.Trim().ToUpper());
                            //hasstuschangelist.Add(1);
                        }
                    }
                    //else
                    //{
                    //    AddItem(txtItmCode.Text, lblTotal.Text.Trim(), lblstus.Text, "1", SeqNo.ToString(), txtserial.Text, txtNewStockSts.Text.Trim().ToUpper());
                    //    AddSerials(txtItmCode.Text, txtserial.Text, SeqNo.ToString(), lblstus.Text, 1, txtNewStockSts.Text.Trim().ToUpper());
                    //    hasstuschangelist.Add(1);
                    //}



                    //foreach (GridViewRow gvrow in gvselecteditems.Rows)
                    //{
                    //    Label lblstus = (Label)gvrow.FindControl("lblstus");
                    //    DropDownList ddlnewstus = (DropDownList)gvrow.FindControl("ddlnewstus");
                    //    Label lblitmcode = (Label)gvrow.FindControl("lblitmcode");
                    //    Label lblser1 = (Label)gvrow.FindControl("lblser1");

                    //    MasterItem _itms = new MasterItem();
                    //    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitmcode.Text);

                    //    if (lblstus.Text != ddlnewstus.SelectedValue)
                    //    {
                    //        if (Session["SEQNO"] != null)
                    //        {
                    //            SeqNo = (Int32)Session["SEQNO"];
                    //        }
                    //        else
                    //        {
                    //            SeqNo = GenerateNewUserSeqNo();
                    //            Session["SEQNO"] = SeqNo;
                    //        }
                    //        AddItem(lblitmcode.Text, _itms.Mi_itmtot_cost.ToString(), lblstus.Text, "1", SeqNo.ToString(), lblser1.Text, ddlnewstus.SelectedValue);
                    //        AddSerials(lblitmcode.Text, lblser1.Text, SeqNo.ToString(), lblstus.Text, 1, ddlnewstus.SelectedValue);
                    //        hasstuschangelist.Add(1);
                    //    }
                    //}
                    InventoryHeader _hdrMinus = new InventoryHeader();
                    InventoryHeader _hdrPlus = new InventoryHeader();
                    List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                    List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                    MasterAutoNumber _autonoMinus = new MasterAutoNumber();
                    MasterAutoNumber _autonoPlus = new MasterAutoNumber();
                    string documntNo_minus = "";
                    string documntNo_plus = "";
                    string error = string.Empty;
                    if (hasstuschangelist.Contains(1) && txtNewStockSts.Text != "")
                    {


                        Int32 _userSeqNo = Convert.ToInt32(SeqNo);

                        reptPickSerialsList = ViewState["SerialList"] as List<ReptPickSerials>;

                        #region Check Duplicate Serials
                        if (reptPickSerialsList != null)
                        {
                            var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                            string _duplicateItems = string.Empty;
                            bool _isDuplicate = false;
                            if (_dup != null)
                                if (_dup.Count > 0)
                                    foreach (Int32 _id in _dup)
                                    {
                                        Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                        if (_counts > 1)
                                        {
                                            _isDuplicate = true;
                                            var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                            foreach (string _str in _item)
                                                if (string.IsNullOrEmpty(_duplicateItems))
                                                    _duplicateItems = _str;
                                                else
                                                    _duplicateItems += "," + _str;
                                        }
                                    }
                            if (_isDuplicate)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item " + _duplicateItems + " serials are duplicating. Please remove the duplicated serials !!!')", true);
                                return;
                            }
                        }
                        #endregion

                        reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ-S");


                        #region Fill InventoryHeader
                        DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        foreach (DataRow r in dt_location.Rows)
                        {
                            _hdrMinus.Ith_sbu = (string)r["ML_OPE_CD"];
                            if (System.DBNull.Value != r["ML_CATE_2"])
                            {
                                _hdrMinus.Ith_channel = (string)r["ML_CATE_2"];
                            }
                            else
                            {
                                _hdrMinus.Ith_channel = string.Empty;
                            }
                        }

                        _hdrMinus.Ith_acc_no = "STATUS_CHANGE";
                        _hdrMinus.Ith_anal_1 = "";
                        _hdrMinus.Ith_anal_2 = "";
                        _hdrMinus.Ith_anal_3 = "";
                        _hdrMinus.Ith_anal_4 = "";
                        _hdrMinus.Ith_anal_5 = "";
                        _hdrMinus.Ith_anal_6 = _userSeqNo;
                        _hdrMinus.Ith_anal_7 = 0;
                        _hdrMinus.Ith_anal_8 = DateTime.MinValue;
                        _hdrMinus.Ith_anal_9 = DateTime.MinValue;
                        _hdrMinus.Ith_anal_10 = false;
                        _hdrMinus.Ith_anal_11 = false;
                        _hdrMinus.Ith_anal_12 = false;
                        _hdrMinus.Ith_bus_entity = "";
                        _hdrMinus.Ith_cate_tp = "STUS";
                        _hdrMinus.Ith_com = Session["UserCompanyCode"].ToString();
                        _hdrMinus.Ith_com_docno = "";
                        _hdrMinus.Ith_cre_by = Session["UserID"].ToString();
                        _hdrMinus.Ith_cre_when = DateTime.Now;
                        _hdrMinus.Ith_del_add1 = "";
                        _hdrMinus.Ith_del_add2 = "";
                        _hdrMinus.Ith_del_code = "";
                        _hdrMinus.Ith_del_party = "";
                        _hdrMinus.Ith_del_town = "";
                        _hdrMinus.Ith_direct = false;
                        _hdrMinus.Ith_doc_date = DateTime.Now.Date;//Convert.ToDateTime(dtpFromDate.Text);
                        _hdrMinus.Ith_doc_no = string.Empty;
                        _hdrMinus.Ith_doc_tp = "ADJ";
                        _hdrMinus.Ith_doc_year = DateTime.Now.Year;
                        _hdrMinus.Ith_entry_no = string.Empty;
                        _hdrMinus.Ith_entry_tp = "STTUS";
                        _hdrMinus.Ith_git_close = true;
                        _hdrMinus.Ith_git_close_date = DateTime.MinValue;
                        _hdrMinus.Ith_git_close_doc = BaseCls.GlbDefaultBin;
                        _hdrMinus.Ith_isprinted = false;
                        _hdrMinus.Ith_is_manual = false;
                        _hdrMinus.Ith_job_no = string.Empty;
                        _hdrMinus.Ith_loading_point = string.Empty;
                        _hdrMinus.Ith_loading_user = string.Empty;
                        _hdrMinus.Ith_loc = Session["UserDefLoca"].ToString();
                        _hdrMinus.Ith_manual_ref = string.Empty;
                        _hdrMinus.Ith_mod_by = Session["UserID"].ToString();
                        _hdrMinus.Ith_mod_when = DateTime.Now;
                        _hdrMinus.Ith_noofcopies = 0;
                        _hdrMinus.Ith_oth_loc = string.Empty;
                        _hdrMinus.Ith_oth_docno = "N/A";
                        _hdrMinus.Ith_remarks = string.Empty;
                        _hdrMinus.Ith_session_id = Session["SessionID"].ToString();
                        _hdrMinus.Ith_stus = "A";
                        _hdrMinus.Ith_sub_tp = "SYS";
                        _hdrMinus.Ith_vehi_no = string.Empty;
                        #endregion

                        #region Fill MasterAutoNumber
                        _autonoMinus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                        _autonoMinus.Aut_cate_tp = "LOC";
                        _autonoMinus.Aut_direction = null;
                        _autonoMinus.Aut_modify_dt = null;
                        _autonoMinus.Aut_moduleid = "ADJ";
                        _autonoMinus.Aut_number = 5;
                        _autonoMinus.Aut_start_char = "ADJ";
                        _autonoMinus.Aut_year = null;
                        #endregion

                        #region Fill InventoryHeader
                        _hdrPlus.Ith_channel = _hdrMinus.Ith_channel;
                        _hdrPlus.Ith_sbu = _hdrMinus.Ith_sbu;
                        _hdrPlus.Ith_acc_no = "STATUS_CHANGE";
                        _hdrPlus.Ith_anal_1 = "";
                        _hdrPlus.Ith_anal_2 = "";
                        _hdrPlus.Ith_anal_3 = "";
                        _hdrPlus.Ith_anal_4 = "";
                        _hdrPlus.Ith_anal_5 = "";
                        _hdrPlus.Ith_anal_6 = 0;
                        _hdrPlus.Ith_anal_7 = 0;
                        _hdrPlus.Ith_anal_8 = DateTime.MinValue;
                        _hdrPlus.Ith_anal_9 = DateTime.MinValue;
                        _hdrPlus.Ith_anal_10 = false;
                        _hdrPlus.Ith_anal_11 = false;
                        _hdrPlus.Ith_anal_12 = false;
                        _hdrPlus.Ith_bus_entity = "";
                        _hdrPlus.Ith_cate_tp = "STTUS";
                        _hdrPlus.Ith_com = Session["UserCompanyCode"].ToString();
                        _hdrPlus.Ith_com_docno = "";
                        _hdrPlus.Ith_cre_by = Session["UserID"].ToString();
                        _hdrPlus.Ith_cre_when = DateTime.Now;
                        _hdrPlus.Ith_del_add1 = "";
                        _hdrPlus.Ith_del_add2 = "";
                        _hdrPlus.Ith_del_code = "";
                        _hdrPlus.Ith_del_party = "";
                        _hdrPlus.Ith_del_town = "";
                        _hdrPlus.Ith_direct = true;
                        _hdrPlus.Ith_doc_date = DateTime.Now.Date;//Convert.ToDateTime(dtpFromDate.Text);
                        _hdrPlus.Ith_doc_no = string.Empty;
                        _hdrPlus.Ith_doc_tp = "ADJ";
                        _hdrPlus.Ith_doc_year = DateTime.Now.Year;
                        _hdrPlus.Ith_entry_no = string.Empty;
                        _hdrPlus.Ith_entry_tp = "STTUS";
                        _hdrPlus.Ith_git_close = true;
                        _hdrPlus.Ith_git_close_date = DateTime.MinValue;
                        _hdrPlus.Ith_git_close_doc = string.Empty;
                        _hdrPlus.Ith_isprinted = false;
                        _hdrPlus.Ith_is_manual = false;
                        _hdrPlus.Ith_job_no = string.Empty;
                        _hdrPlus.Ith_loading_point = string.Empty;
                        _hdrPlus.Ith_loading_user = string.Empty;
                        _hdrPlus.Ith_loc = Session["UserDefLoca"].ToString();
                        _hdrPlus.Ith_manual_ref = string.Empty;
                        _hdrPlus.Ith_mod_by = Session["UserID"].ToString();
                        _hdrPlus.Ith_mod_when = DateTime.Now;
                        _hdrPlus.Ith_noofcopies = 0;
                        _hdrPlus.Ith_oth_loc = string.Empty;
                        _hdrPlus.Ith_oth_docno = "N/A";
                        _hdrPlus.Ith_remarks = string.Empty;
                        _hdrPlus.Ith_session_id = Session["SessionID"].ToString();
                        _hdrPlus.Ith_stus = "A";
                        _hdrPlus.Ith_sub_tp = "STTUS";
                        _hdrPlus.Ith_vehi_no = string.Empty;
                        #endregion

                        #region Fill MasterAutoNumber
                        _autonoPlus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                        _autonoPlus.Aut_cate_tp = "LOC";
                        _autonoPlus.Aut_direction = null;
                        _autonoPlus.Aut_modify_dt = null;
                        _autonoPlus.Aut_moduleid = "ADJ";
                        _autonoPlus.Aut_number = 5;
                        _autonoPlus.Aut_start_char = "ADJ";
                        _autonoPlus.Aut_year = null;
                        #endregion

                        //Set Qty 0 for non serial reqitem- becouse status change GetNonSerializedItemRandomly function

                        //foreach (var ttm in ScanItemList)
                        //{
                        //   MasterItem  _itm = CHNLSVC.Inventory.GetItem(ttm.Itri_com,ttm.Itri_itm_cd);
                        //   if (_itm.Mi_is_ser1==0)
                        //    {
                        //        ttm.Itri_qty = 0;
                        //    }
                        //}
                        ScanItemList = new List<InventoryRequestItem>();

                        /*if (reptPickSerialsList != null && !string.IsNullOrEmpty(txtNewStockSts.Text))
                        {
                            error = CHNLSVC.Inventory.InventoryStatusChangeEntry(_hdrMinus, _hdrPlus, reptPickSerialsList, reptPickSubSerialsList, _autonoMinus, _autonoPlus, ScanItemList, out documntNo_minus, out documntNo_plus);
                        }

                        if (reptPickSerialsList != null && !string.IsNullOrEmpty(txtNewStockSts.Text))
                        {
                            if (string.IsNullOrEmpty(error))
                            {
                                okmsg = " Item Status change document no (-ADJ) : " + documntNo_minus + " and document no (+ADJ) :" + documntNo_plus;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('New stock status cannot be similar to the item status !!!')", true);
                                return;
                            }
                        }
                        if (reptPickSerialsList == null)
                        {
                            okmsg = " Unable to pick serials for item status change / changes !!!";
                        }*/
                    }

                    #endregion
                    #region Update main Condition

                    ItemConditionSetup _itemconditions = new ItemConditionSetup();
                    _itemconditions.irsc_ser_id = Convert.ToInt32(lblSerId.Text);
                    _itemconditions.irsc_com = Session["UserCompanyCode"].ToString();
                    _itemconditions.irsc_loc = Session["UserDefLoca"].ToString();
                    _itemconditions.irsc_cat = "CT003";
                    _itemconditions.irsc_tp = txtCondition.Text.Trim().ToUpper();
                    _itemconditions.irsc_rmk = txtConRemarks.Text.Trim();
                    if (_itemconditions.irsc_rmk == "")
                    {
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Please enter item condition remark.')", true);
                        // return;

                    }
                    _itemconditions.irsc_cre_by = Session["UserID"].ToString();
                    _itemconditions.irsc_cre_dt = DateTime.Now;
                    _itemconditions.irsc_stus = txtNewStockSts.Text.Trim().ToUpper();
                    _itemconditions.irsc_add_loc = Session["UserDefLoca"].ToString();
                    decimal _chg = 0;
                    _itemconditions.irsc_cha = decimal.TryParse(txtMainConPrice.Text, out _chg) ? _chg : 0;

                    //if (ddlconditstus.SelectedValue == "C")
                    //{
                    //  _itemconditions.irsc_cnl_by = _userid;
                    //_itemconditions.p_irsc_cnl_dt = Convert.ToDateTime(DateTime.Now);
                    //}
                    //else
                    //{
                    // _itemconditions.irsc_cnl_by = lblcancelledby.Text;
                    // if (!string.IsNullOrEmpty(lblcancelleddate.Text))
                    // {
                    // _itemconditions.p_irsc_cnl_dt = Convert.ToDateTime(lblcancelleddate.Text);
                    // }
                    //}
                    _itemconditions.irsc_stus = "A";
                    _itemconditions.irsc_add_loc = addedloc;
                    //_itemconditions.irsc_othloc = "	DPS43";
                    //foreach (GridViewRow myrow in this.dvPendingPO.Rows)
                    //{

                    //    Label docno = (Label)myrow.FindControl("lblothdocno");
                    
                    //    Label otherloc = (Label)myrow.FindControl("lblothloc");
                   
                    //}
                    if(Session["BASEDOC"]!=null&&Session["OtherLoc"]!=null)
                    {
                    _itemconditions.irsc_doc = Session["BASEDOC"].ToString();
                    _itemconditions.irsc_othloc = Session["OtherLoc"].ToString();
                    }
                    

                    //    _itemconditions.irsc_doc =  docno.Text;

                    //    _itemconditions.irsc_othloc = otherloc.Text;

                    /* results = CHNLSVC.Inventory.SaveItemConditions(_itemconditions, ItsPick, doc, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItmCode.Text, Convert.ToInt32(lblSerId.Text));
                     if (results > 0)
                     {
                         foreach (var item in _repConSetups)
                         {
                             item.irsc_ser_id = Convert.ToInt32(lblSerId.Text.Trim());
                             item.irsc_com = Session["UserCompanyCode"].ToString();
                             item.irsc_loc = Session["UserDefLoca"].ToString();
                             item.irsc_cat = "CT003";
                             //_itemconditions.irsc_tp = txtCondition.Text.Trim().ToUpper();
                             //_itemconditions.irsc_rmk = txtConRemarks.Text.Trim();
                             item.irsc_cre_by = Session["UserID"].ToString();
                             item.irsc_cre_dt = DateTime.Now;
                             item.irsc_stus = "A";
                             item.irsc_add_loc = addedloc;
                             results = CHNLSVC.Inventory.SaveItemConditions(item, ItsPick, doc, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItmCode.Text, Convert.ToInt32(lblSerId.Text));
                         }
                     }*/

                    //foreach (GridViewRow hiderowbtn in this.gvconditions.Rows)
                    //{
                    //    Label lbltype = (Label)hiderowbtn.FindControl("lbltype");
                    //    Label lblremarks = (Label)hiderowbtn.FindControl("lblremarks");
                    //    DropDownList ddlconditstus = (DropDownList)hiderowbtn.FindControl("ddlconditstus");
                    //    Label lblcancelledby = (Label)hiderowbtn.FindControl("lblcancelledby");
                    //    Label lblcancelleddate = (Label)hiderowbtn.FindControl("lblcancelleddate");
                    //    Label serialidori = (Label)hiderowbtn.FindControl("lblitmserialid");
                    //    Label selecteditem = (Label)hiderowbtn.FindControl("lblitmcodemyref");

                    //    ItemConditionSetup _itemconditions = new ItemConditionSetup();
                    //    _itemconditions.irsc_ser_id = Convert.ToInt32(serialidori.Text);
                    //    _itemconditions.irsc_com = Session["UserCompanyCode"].ToString();
                    //    _itemconditions.irsc_loc = Session["UserDefLoca"].ToString();
                    //    _itemconditions.irsc_cat = "CT003";
                    //    _itemconditions.irsc_tp = lbltype.Text;
                    //    _itemconditions.irsc_rmk = lblremarks.Text;
                    //    _itemconditions.irsc_cre_by = _userid;
                    //    _itemconditions.irsc_cre_dt = Convert.ToDateTime(DateTime.Now);
                    //    _itemconditions.irsc_stus = ddlconditstus.SelectedValue;
                    //    _itemconditions.irsc_add_loc = addedloc;

                    //    if (ddlconditstus.SelectedValue == "C")
                    //    {
                    //        _itemconditions.irsc_cnl_by = _userid;
                    //        _itemconditions.p_irsc_cnl_dt = Convert.ToDateTime(DateTime.Now);
                    //    }
                    //    else
                    //    {
                    //        _itemconditions.irsc_cnl_by = lblcancelledby.Text;
                    //        if (!string.IsNullOrEmpty(lblcancelleddate.Text))
                    //        {
                    //            _itemconditions.p_irsc_cnl_dt = Convert.ToDateTime(lblcancelleddate.Text);
                    //        }
                    //    }

                    //    results = CHNLSVC.Inventory.SaveItemConditions(_itemconditions, ItsPick, doc, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), selecteditem.Text, Convert.ToInt32(serialidori.Text));

                    //    successItems.Add(results);
                    //}

                    #endregion

                    #region Update ITH_ANAL_4

                    DataTable dtdocserials = CHNLSVC.Inventory.LoadDocSerials(doc);

                    if (dtdocserials.Rows.Count > 0)
                    {
                        foreach (DataRow ddritem in dtdocserials.Rows)
                        {
                            Int32 SerialId = Convert.ToInt32(ddritem["its_ser_id"].ToString());
                            DataTable dtconditionsperitem = CHNLSVC.Inventory.LoadItemConditionsPerItem(SerialId, catetype);

                            if (dtconditionsperitem.Rows.Count > 0)
                            {
                                successItemsAnal4.Add(1);
                            }
                            else
                            {
                                successItemsAnal4.Add(0);
                            }
                        }
                    }

                    InventoryHeader header1 = new InventoryHeader();
                    if (!successItemsAnal4.Contains(0))
                    {

                        header1.Ith_anal_4 = anal4;
                        header1.Ith_com = Session["UserCompanyCode"].ToString();
                        header1.Ith_loc = Session["UserDefLoca"].ToString();
                        header1.Ith_doc_no = doc;

                        //Int32 updateanal4 = CHNLSVC.Inventory.UpdateAnal4(header1);

                        //if (updateanal4 == -1)
                        //{
                        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while updating anal 4 !!!')", true);
                        //    return;
                        //}
                    }
                    List<InventoryHeader> updateAnl4Hdr = new List<InventoryHeader>();
                    foreach (GridViewRow myrow in this.dvPendingPO.Rows)
                    {
                        CheckBox cb = (CheckBox)myrow.FindControl("chkconfirm");
                        Label docno = (Label)myrow.FindControl("lbldocno");

                        if (doc == docno.Text)
                        {
                            if (cb.Checked == true)
                            {
                                InventoryHeader header = new InventoryHeader();
                                header.Ith_anal_4 = anal4;
                                header.Ith_com = Session["UserCompanyCode"].ToString();
                                header.Ith_loc = Session["UserDefLoca"].ToString();
                                header.Ith_doc_no = doc;
                                updateAnl4Hdr.Add(header);
                                //Int32 updateanal4 = CHNLSVC.Inventory.UpdateAnal4(header);

                                //if (updateanal4 == -1)
                                //{
                                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while updating anal 4 !!!')", true);
                                //    return;
                                //}
                            }
                        }
                    }
                    //string newADJ = string.Empty;
                    //foreach (InventoryRequestItem iRItem in ScanItemList)
                    //{
                    //    if (iRItem.Itri_itm_stus != null)
                    //    {
                    //        if (!iRItem.Itri_itm_stus.Equals("GOD"))
                    //        {
                    //            newADJ = "Allow";
                    //        }
                    //    }
                    //}
                    List<ReptPickSerials> checkStatusReptSerialsList = new List<ReptPickSerials>();
                    for(int i=0;i<reptPickSerialsList.Count;i++)
                    {
                        if(!reptPickSerialsList[i].Tus_itm_stus.Equals(reptPickSerialsList[i].Tus_new_status))
                        {
                            checkStatusReptSerialsList.Add(reptPickSerialsList[i]);
                        }
                    }
                    reptPickSerialsList = checkStatusReptSerialsList;                    
                    bool responce = CHNLSVC.Inventory.saveProductConditionUpdate(_hdrMinus, _hdrPlus, reptPickSerialsList, reptPickSubSerialsList, _autonoMinus, _autonoPlus, ScanItemList, out documntNo_minus, out documntNo_plus, out error, hasstuschangelist, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(),
                       _itemconditions, ItsPick, doc, txtItmCode.Text, _repConSetups, Convert.ToInt32(lblSerId.Text), addedloc,
                       header1, updateAnl4Hdr, successItemsAnal4, txtNewStockSts.Text);  
                    #endregion
                    if (responce == true)
                    {
                        txtserial.Text = "";
                        txtItmCode.Text = "";
                        txtGrnNo.Text = "";
                        txtGrnDate.Text = "";
                        txtgranno.Text = "";

                        txtCondition.Text = "";
                        txtMainConPrice.Text = "";
                        TextDesc.Text = "";
                        txtConRemarks.Text = "";
                        lblSerId.Text = "";
                        txtSubCon.Text = "";
                        txtSubConCat.Text = "";
                        txtSubCharg.Text = "";
                        txtSubRemark.Text = "";
                        txtNewStockSts.Text = "";
                        lblTotal.Text = "0.00";
                        dgvCond.DataSource = new int[] { };
                        dgvCond.DataBind();
                        gvselecteditems.DataSource = new int[] { };
                        gvselecteditems.DataBind();
                        Session["BASEDOC"] = null;
                        Session["SEQNO"] = null;
                        Session["_repConSetups"] = null;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + error + " " + okmsg + "')", true);
                        DispMsg(error, "S");
                        return;
                    }
                    else
                    {
                        DispMsg(error, "E");
                        return;
                    }

                    //if (!successItems.Contains(-1))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Product Condition Updated Successfully !!! " + okmsg + "')", true);
                    //    Clear();
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    //    return;
                    //}
                    return;
                }
                catch (Exception ex)
                {

                    DispMsg(ex.Message.ToString(), "E");
                    return;
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        private string SetNewItemStatus(List<ReptPickSerials> _serList, String ItemCode, String ItemStatus)
        {
            string NewItemStatus = string.Empty;

            if (_serList.FindAll(x => x.Tus_itm_cd == ItemCode && x.Tus_itm_stus == ItemStatus).Count > 0)
            {
                NewItemStatus = _serList.FindAll(x => x.Tus_itm_cd == ItemCode && x.Tus_itm_stus == ItemStatus)[0].Tus_new_status;
            }

            return NewItemStatus;
        }
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;

            _direction = 1;

            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ-S", _direction, Session["UserCompanyCode"].ToString());

            ReptPickHeader RPH = new ReptPickHeader();

            RPH.Tuh_doc_tp = "ADJ-S";
            RPH.Tuh_cre_dt = DateTime.Today;
            RPH.Tuh_ischek_itmstus = true;
            RPH.Tuh_ischek_reqqty = true;
            RPH.Tuh_ischek_simitm = true;
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            if (_direction == 1)
            {
                RPH.Tuh_direct = true;
            }
            else
            {
                RPH.Tuh_direct = false;
            }
            RPH.Tuh_doc_no = generated_seq.ToString();

            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

            if (affected_rows == 1)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        private void AddSerials(string _item, string _Serial, string _Seqno, string _stus, decimal _qty, string _newstus)
        {
            //Added By Dulaj 2018/Apr/19
            //string bin = CHNLSVC.Inventory.GetBinCodeForNonSeralizeItem(_item, Session["UserDefLoca"].ToString(), Session["UserCompanyCode"].ToString(), lblstus.Text.ToString());
            string bin = CHNLSVC.Inventory.GetBinCodeForNonSeralizeItem(_item, Session["UserDefLoca"].ToString(), Session["UserCompanyCode"].ToString(), _stus);
            
            Int32 generated_seq = -1;
            MasterItem msitem = new MasterItem();
            List<ReptPickSerials> Tempserial_list = new List<ReptPickSerials>();
            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            string basedoc = (string)Session["BASEDOC"];
            SeqNo = (Int32)Session["SEQNO"];

            if (msitem.Mi_is_ser1 == 1)
            {
                Tempserial_list = new List<ReptPickSerials>();
                Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);
                Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial && x.Tus_doc_no == basedoc).ToList();
                if (Tempserial_list.Count == 0)
                {
                    DispMsg("No serial available !!!"); return;
                }
            }
            else
            {
                List<ReptPickSerials> Tempserial_list2 = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);
                Tempserial_list2 = Tempserial_list2.Where(x => x.Tus_doc_no == basedoc && x.Tus_itm_cd == _item).ToList();
                List<ReptPickSerials> listnew = new List<ReptPickSerials>();
                if (Tempserial_list != null)
                {
                    if (Tempserial_list.Count > 0)
                    {
                        foreach (var li in Tempserial_list2)
                        {
                            var count = Tempserial_list.Where(a => a.Tus_ser_id == li.Tus_ser_id).Count();
                            if (count == 0)
                            {
                                listnew.Add(li);
                            }


                        }
                    }
                    else
                    {
                        listnew.AddRange(Tempserial_list2);
                    }
                }
                else
                {
                    listnew.AddRange(Tempserial_list2);
                }



                if (listnew == null)
                {
                    DispMsg("No serial available !!!"); return;
                }
                else
                {
                    if (listnew.Count == 0)
                    {
                        DispMsg("No serial available !!!"); return;
                    }
                }
                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                Tempserial_list = new List<ReptPickSerials>();
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_base_doc_no = basedoc;
                _reptPickSerial_.Tus_base_itm_line = 0;
                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();

                _reptPickSerial_.Tus_itm_cd = _item;
                _reptPickSerial_.Tus_itm_stus = _stus;
                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                _reptPickSerial_.Tus_ser_1 = "N/A";
                _reptPickSerial_.Tus_ser_2 = "N/A";
                _reptPickSerial_.Tus_ser_3 = "N/A";
                _reptPickSerial_.Tus_ser_4 = "N/A";
                _reptPickSerial_.Tus_ser_id = 0;
                _reptPickSerial_.Tus_serial_id = listnew.First().Tus_ser_id.ToString();
                _reptPickSerial_.Tus_unit_cost = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_job_no = string.Empty;
                _reptPickSerial_.Tus_pgs_prefix = _item;
                _reptPickSerial_.Tus_job_line = 0;
                _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                Tempserial_list.Add(_reptPickSerial_);
            }
            var serialLine = 0;
            if (ScanItemList.Count > 0)
            {
                serialLine = (from _ls in ScanItemList
                              where _ls.Itri_itm_cd == _item
                              select _ls.Itri_line_no).Max();
            }


            Int32 user_seq_num = -1;
            foreach (ReptPickSerials serial in Tempserial_list)
            {
                #region PRN

                user_seq_num = SeqNo;

                if (user_seq_num != -1)
                {
                    generated_seq = user_seq_num;
                }
                else
                {
                    generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ-S", 1, Session["UserCompanyCode"].ToString());
                    ReptPickHeader RPH = new ReptPickHeader();
                    RPH.Tuh_doc_tp = "ADJ-S";
                    RPH.Tuh_cre_dt = DateTime.Today;
                    RPH.Tuh_ischek_itmstus = true;
                    RPH.Tuh_ischek_reqqty = true;
                    RPH.Tuh_ischek_simitm = true;
                    RPH.Tuh_session_id = Session["SessionID"].ToString();
                    RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    RPH.Tuh_usr_id = Session["UserID"].ToString();
                    RPH.Tuh_usrseq_no = generated_seq;
                    RPH.Tuh_direct = false;
                    RPH.Tuh_doc_no = _Seqno;
                    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                }

                if (msitem.Mi_is_ser1 != -1)
                {
                    int rowCount = 0;

                    ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));
                    //Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_seq_no = generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_doc_no = (string)Session["BASEDOC"];
                    _reptPickSerial_.Tus_base_doc_no = basedoc;
                    _reptPickSerial_.Tus_itm_cd = _item;
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine);
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_job_no = serial.Tus_job_no;
                    _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                    _reptPickSerial_.Tus_itm_stus = _stus;
                    _reptPickSerial_.Tus_ser_1 = _Serial;
                    _reptPickSerial_.Tus_new_status = _newstus;
                    _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                    _reptPickSerial_.Tus_bin = bin;
                    _reptPickSerial_.Tus_bin_to = bin;
                    if (_reptPickSerial_.Tus_qty == 0 && msitem.Mi_is_ser1 == 0)
                    {
                        _reptPickSerial_.Tus_qty = 1;
                    }

                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                    rowCount++;
                }
                else
                {
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_seq_no = generated_seq;
                    _reptPickSerial_.Tus_base_doc_no = basedoc;
                    _reptPickSerial_.Tus_doc_no = _Seqno;
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine);
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_itm_cd = serial.Tus_itm_cd;
                    _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
                    _reptPickSerial_.Tus_ser_1 = _Serial;
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = 0;
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_unit_cost = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_job_no = serial.Tus_job_no;
                    _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                    _reptPickSerial_.Tus_new_status = _newstus;
                    _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();

                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                }

                #endregion
            }
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "ADJ-S");
            ViewState["SerialList"] = _serList;
        }

        protected void AddItem(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial, string _newstatus)
        {
            try
            {
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                int itemline = 0;
                if (ScanItemList != null)
                {
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == _item && _ls.Itri_itm_stus == _status
                                         select _ls;

                        if (_duplicate != null)
                            if (_duplicate.Count() > 0)
                            {
                                var line = from d in _duplicate
                                           select d.Itri_line_no;
                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_qty = x.Itri_qty + Convert.ToDecimal(_qty));

                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_app_qty = x.Itri_app_qty + Convert.ToDecimal(_qty));
                            }
                            else
                            {
                                var _maxline = (from _ls in ScanItemList
                                                select _ls.Itri_line_no).Max();
                                _itm.Itri_itm_cd = _item;
                                _itm.Itri_itm_stus = _status;
                                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                                _itm.Itri_qty = Convert.ToDecimal(_qty);
                                _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                                _itm.Itri_bqty = Convert.ToDecimal(_qty);
                                _itm.Mi_longdesc = _itms.Mi_longdesc;
                                _itm.Mi_model = _itms.Mi_model;
                                _itm.Mi_brand = _itms.Mi_brand;
                                _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                                _itm.Itri_note = _newstatus;
                                ScanItemList.Add(_itm);
                            }
                    }
                    else
                    {
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = 1;
                        _itm.Itri_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                        _itm.Itri_note = _newstatus;
                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }
                }
                else
                {
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = 1;
                    _itm.Itri_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_bqty = Convert.ToDecimal(_qty);
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    _itm.Itri_note = _newstatus;
                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }

                SeqNo = (Int32)Session["SEQNO"];
                List<ReptPickItems> _reptItemsTemp = new List<ReptPickItems>();
                _reptItemsTemp = CHNLSVC.Inventory.GetAllScanRequestItemsList(SeqNo);

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = SeqNo;
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;

                    if (_reptItemsTemp.Count > 0)
                    {
                        ReptPickItems oItem = _reptItemsTemp.Find(x => x.Tui_req_itm_qty == _addedItem.Itri_app_qty && x.Tui_req_itm_cd == _addedItem.Itri_itm_cd && _reptitm.Tui_req_itm_stus == _addedItem.Itri_itm_stus);
                        if (oItem != null)
                        {
                            _reptitm.Tui_pic_itm_cd = oItem.Tui_pic_itm_cd;
                            _reptitm.Tui_pic_itm_stus = oItem.Tui_pic_itm_stus;
                        }
                    }
                    _saveonly.Add(_reptitm);
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmclear.Value == "Yes")
                {
                    Session["BASEDOC"] = null;
                    Session["SEQNO"] = null;
                    Session["_repConSetups"] = null;
                    Response.Redirect(Request.RawUrl);
                }
                //Clear();
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
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                gvProdCondition.DataSource = new int[] { };
                gvProdCondition.DataBind();
                if (gvselecteditems.Rows.Count > 0)
                {
                    DataTable tot = new DataTable("tot");
                    foreach (GridViewRow item in gvselecteditems.Rows)
                    {
                        Label lblaodindate = item.FindControl("lblserialid") as Label;
                        DataTable res = CHNLSVC.Inventory.getConditionDetails(lblaodindate.Text);
                        tot.Merge(res);
                    }

                    gvProdCondition.DataSource = tot;
                    gvProdCondition.DataBind();
                    popProdCondition.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please seclect document for view!')", true);
                }

                //Clear();
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
        protected void lbtnClosePop_Click(object sender, EventArgs e)
        {
            try
            {
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
        protected void lbtndconfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 count = 0;
                foreach (GridViewRow rowCOunt in this.gvaddconditions.Rows)
                {
                    CheckBox chkId = (rowCOunt.FindControl("col_p_Get") as CheckBox);
                    if (chkId.Checked)
                    {
                        count++;
                    }
                }

                string itemcode = (string)Session["ITEM_CODE"];
                string serialidori = (string)Session["SERIAL"];
                string itemcate = (string)Session["ITEM_CATE"];
                Int32 selectedserial = Convert.ToInt32(serialidori);

                if (count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select condition/conditions before adding !!!')", true);
                    MpDelivery.Show();
                    return;
                }

                DataTable dt = (DataTable)ViewState["Conditions"];

                if (dt.Rows.Count == 0)
                {
                    DataTable dtconditions = new DataTable();
                    dtconditions.Columns.AddRange(new DataColumn[12] { new DataColumn("its_itm_cd"), new DataColumn("irsc_ser_id"), new DataColumn("irsc_cat"), new DataColumn("irsc_tp"), new DataColumn("irsc_rmk"), new DataColumn("irsc_stus"), new DataColumn("irsc_cre_by"), new DataColumn("irsc_cnl_dt"), new DataColumn("irsc_cnl_by"), new DataColumn("rct_desc"), new DataColumn("rct_cat1"), new DataColumn("status") });
                    ViewState["Conditions"] = dtconditions;
                    this.BindGrid();
                    dt = dtconditions;
                }

                DataView dv = new DataView(dt);

                _userid = (string)Session["UserID"];

                foreach (GridViewRow gvrow in gvaddconditions.Rows)
                {
                    Label type = (Label)gvrow.FindControl("lbladdtype");
                    Label condition = (Label)gvrow.FindControl("lbladdconditions");
                    TextBox remark = (TextBox)gvrow.FindControl("txtremarks");
                    CheckBox col_p_Get = (CheckBox)gvrow.FindControl("col_p_Get");

                    if (col_p_Get.Checked == true)
                    {
                        if (string.IsNullOrEmpty(remark.Text))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter remarks for adding conditions !!!')", true);
                            MpDelivery.Show();
                            return;
                        }

                        if (dv.Count > 0)
                        {
                            dv.RowFilter = "irsc_tp = '" + type.Text + "' and rct_desc = '" + condition.Text + "' and irsc_rmk = '" + remark.Text + "'";
                        }

                        if (dv.Count == 0)
                        {
                            dt.Rows.Add(itemcode, selectedserial, "CT003", type.Text, remark.Text, "A", _userid, null, string.Empty, condition.Text, itemcate, "Active");
                        }
                    }
                }

                gvconditions.DataSource = null;
                gvconditions.DataBind();

                gvconditions.DataSource = dt;
                gvconditions.DataBind();

                MpDelivery.Hide();
                ViewState["Conditions"] = dt;
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

        protected void lbtndreset_Click(object sender, EventArgs e)
        {
            try
            {
                MpDelivery.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void gvconditions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        protected void lbtngrdInvoiceDetailsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                if (row != null)
                {
                    string remarks = (row.FindControl("txtremarkscondition") as TextBox).Text;
                    string type = (row.FindControl("lbltype") as Label).Text;
                    string desc = (row.FindControl("lbldesc") as Label).Text;

                    if (string.IsNullOrEmpty(remarks))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a remark !!!')", true);
                        return;
                    }

                    gvconditions.EditIndex = -1;

                    DataTable dt = (DataTable)ViewState["Conditions"];

                    dt.Rows[row.RowIndex]["irsc_rmk"] = remarks;

                    gvconditions.DataSource = dt;
                    gvconditions.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtngrdInvoiceDetailsEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                gvconditions.EditIndex = grdr.RowIndex;

                DataTable dt = (DataTable)ViewState["Conditions"];

                gvconditions.DataSource = dt;
                gvconditions.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void gvconditions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
                {
                    int Myindex = Convert.ToInt32(e.RowIndex);
                    DataTable dt = ViewState["Conditions"] as DataTable;
                    dt.Rows[Myindex].Delete();
                    dt.AcceptChanges();
                    ViewState["Conditions"] = dt;
                    BindGrid();
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

        protected void ddlconditstus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["Conditions"];
                foreach (GridViewRow hiderowbtn in this.gvconditions.Rows)
                {
                    DropDownList ddlconditstus = (DropDownList)hiderowbtn.FindControl("ddlconditstus");
                    string newstus = ddlconditstus.SelectedValue;

                    string stustxt = string.Empty;

                    if (newstus == "A")
                    {
                        stustxt = "Active";
                    }
                    else if (newstus == "C")
                    {
                        stustxt = "Inactive";
                    }
                    Label itm = (Label)hiderowbtn.FindControl("lblitmcodemyref");
                    Label ser = (Label)hiderowbtn.FindControl("lblitmserialid");
                    Label cat = (Label)hiderowbtn.FindControl("lblct003");
                    Label tp = (Label)hiderowbtn.FindControl("lbltype");
                    Label stusval = (Label)hiderowbtn.FindControl("lblstatusvalueitmconditions");
                    Label cate = (Label)hiderowbtn.FindControl("lblcategory");

                    foreach (DataRow DDRitem in dt.Rows)
                    {
                        if ((itm.Text == DDRitem["its_itm_cd"].ToString()) && (ser.Text == DDRitem["irsc_ser_id"].ToString()) && (cat.Text == DDRitem["irsc_cat"].ToString()) && (tp.Text == DDRitem["irsc_tp"].ToString()) && (cate.Text == DDRitem["rct_cat1"].ToString()))
                        {
                            DDRitem["irsc_stus"] = newstus;
                            DDRitem["Status"] = stustxt;
                        }
                    }
                }
                ViewState["Conditions"] = dt;
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

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName1)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[colName1])))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName1], string.Empty);
                }
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
            {
                dTable.Rows.Remove(dRow);
            }
            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        protected void txtdirectserial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtdirectserial.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a serial number !!!')", true);
                    return;
                }

                PopulateItemsGridDirectlyBySerial(txtdirectserial.Text.Trim(), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
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



        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat1.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    string _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_Para, "CODE", txtCat1.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["CODE"].ToString()))
                        {
                            if (txtCat1.Text.ToUpper() == row["CODE"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCat1.ToolTip = toolTip;
                    }
                    else
                    {
                        txtCat1.ToolTip = "";
                        txtCat1.Text = "";
                        txtCat1.Focus();
                        DispMsg("Please select a valid category 1 !!!");
                        return;
                    }
                }
                else
                {
                    txtCat1.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        public void BindCmbSearchbykey(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }
            this.cmbSearchbykey.SelectedIndex = 0;
        }
        protected void lbtnSeCat1_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main), null, null);
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    dv.Sort = "Code asc";
                    _serData = dv.ToTable();
                    dgvResult.DataSource = _serData;

                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchWord.Text = "";
                txtSearchWord.Focus();
                _serType = "CAT_Main";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void txtCat2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat1.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    string _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_Para, "CODE", txtCat1.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["CODE"].ToString()))
                        {
                            if (txtCat1.Text.ToUpper() == row["CODE"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCat1.ToolTip = toolTip;
                    }
                    else
                    {
                        txtCat1.ToolTip = "";
                        txtCat1.Text = "";
                        txtCat1.Focus();
                        DispMsg("Please select a valid category 1 !!!");
                        return;
                    }
                }
                else
                {
                    txtCat1.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeCat2_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1), null, null);
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchWord.Text = "";
                txtSearchWord.Focus();
                _serType = "CAT_Sub1";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtCat3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat1.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    string _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_Para, "CODE", txtCat1.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["CODE"].ToString()))
                        {
                            if (txtCat1.Text.ToUpper() == row["CODE"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCat1.ToolTip = toolTip;
                    }
                    else
                    {
                        txtCat1.ToolTip = "";
                        txtCat1.Text = "";
                        txtCat1.Focus();
                        DispMsg("Please select a valid category 1 !!!");
                        return;
                    }
                }
                else
                {
                    txtCat1.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeCat3_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _serData = CHNLSVC.General.GetItemSubCat4(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2), null, null);
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchWord.Text = "";
                txtSearchWord.Focus();
                _serType = "CAT_Sub2";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }


        protected void lbtnSearchNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType != "Status")
                {
                    _serData = new DataTable();
                }
                if (_serType == "CAT_Main")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, "%" + txtSearchWord.Text);
                    DataView dv = _serData.DefaultView;
                    dv.Sort = "Code asc";
                    _serData = dv.ToTable();
                }
                else if (_serType == "CAT_Sub1")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    string valuetest = txtSearchWord.Text;
                    valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                }
                else if (_serType == "CAT_Sub2")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    string valuetest = txtSearchWord.Text;
                    valuetest = (valuetest == "") ? valuetest : "%" + valuetest;
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(para, cmbSearchbykey.SelectedValue, valuetest);
                }
                else if (_serType == "Promotion")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                    _serData = CHNLSVC.CommonSearch.SER_REF_COND_TP(para, cmbSearchbykey.SelectedValue, "%" + txtSearchWord.Text);
                    _serData = RemoveDuplicateRows(_serData, "Description");
                }
                else if (_serType == "SubPromotion")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                    _serData = CHNLSVC.CommonSearch.SER_REF_COND_TP_NEW(para, cmbSearchbykey.SelectedValue, "%" + txtSearchWord.Text);
                    _serData = RemoveDuplicateRows(_serData, "Description");
                }
                else if (_serType == "Status")
                {
                    if (string.IsNullOrEmpty(txtSearchWord.Text))
                    {
                        _serData = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
                        DataTable _dt = new DataTable();
                        _dt.Columns.Add("Code");
                        _dt.Columns.Add("Description");
                        foreach (DataRow dr in _serData.Rows)
                        {
                            DataRow ddr = _dt.NewRow();
                            ddr["Code"] = dr["MIC_CD"].ToString();
                            ddr["Description"] = dr["MIS_DESC"].ToString();
                            _dt.Rows.Add(ddr);
                        }
                        DataView dv = _dt.DefaultView;
                        dv.Sort = "Code ASC";
                        DataTable sortedDT = dv.ToTable();
                        _serData = sortedDT;
                    }
                    else
                    {
                        if (_serData.Rows.Count > 0)
                        {
                            DataView dv = new DataView(_serData);
                            string searchParameter = cmbSearchbykey.Text;
                            dv.RowFilter = "" + cmbSearchbykey.Text + " like '%" + txtSearchWord.Text + "%'";

                            if (dv.Count > 0)
                            {
                                _serData = dv.ToTable();
                            }
                        }
                    }
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    if (_serType == "SubPromotion" || _serType == "Promotion")
                    {
                        _serData.Columns["Description"].SetOrdinal(0);
                        _serData.Columns["Code"].SetOrdinal(1);
                        dgvResult.DataSource = _serData; 
                    }
                    else
                    { 
                        dgvResult.DataSource = _serData; 
                    }
                }
                txtSearchWord.Text = "";
                txtSearchWord.Focus();
                dgvResult.DataBind();
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
                string cate = "";
                if (_serType == "SubPromotion")
                {
                    cate = dgvResult.SelectedRow.Cells[3].Text;
                }
                if (_serType == "CAT_Main")
                {
                    txtCat1.Text = code;
                    txtCat1_TextChanged(null, null);
                }
                else if (_serType == "CAT_Sub1")
                {
                    txtCat2.Text = code;
                    txtCat2_TextChanged(null, null);
                }
                else if (_serType == "CAT_Sub2")
                {
                    txtCat3.Text = code;
                    txtCat3_TextChanged(null, null);
                }
                else if (_serType == "Promotion")
                {
                    txtCondition.Text = dgvResult.SelectedRow.Cells[2].Text;
                    txtCondition_TextChanged(null, null);
                }
                else if (_serType == "SubPromotion")
                {
                    code = dgvResult.SelectedRow.Cells[2].Text;
                    txtSubCon.Text = code;
                    txtSubConCat.Text = cate;
                    txtSubCon_TextChanged(null, null);
                }
                else if (_serType == "Status")
                {
                    txtNewStockSts.Text = code;
                    txtNewStockSts_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
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
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
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
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }

        protected void lbtnSeCondition_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _serType = "Promotion";
                _serData = CHNLSVC.CommonSearch.SER_REF_COND_TP(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion), null, null);
                if (_serData.Rows.Count > 0)
                {
                    _serData.Columns["Description"].SetOrdinal(0);
                    _serData.Columns["Code"].SetOrdinal(1);
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchWord.Text = "";
                txtSearchWord.Focus();
                _serType = "Promotion";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtCondition_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool b16051 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16051);
                txtMainConPrice.ReadOnly = true;
                txtSubCharg.ReadOnly = true;
                if (b16051)
                {
                    txtMainConPrice.ReadOnly = false;
                    txtSubCharg.ReadOnly = false;
                }
                else
                {
                    txtMainConPrice.ReadOnly = true;
                    txtSubCharg.ReadOnly = true;
                }

                pnlCondition.Enabled = false;
                txtMainConPrice.Text = "0.00";
                // lblCondition.Text = "";
                if (!string.IsNullOrEmpty(txtCondition.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    bool _loadSubCon = false;
                    DataTable _result = CHNLSVC.CommonSearch.SER_REF_COND_TP(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion), "Code", txtCondition.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtCondition.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                TextDesc.Text = toolTip;
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCondition.ToolTip = toolTip;
                        _repConTp = new RepConditionType();
                        _repConTp.Rct_com = Session["UserCompanyCode"].ToString();
                        _repConTp.Rct_tp = txtCondition.Text.Trim().ToUpper();

                        _repConTp.Rct_cate = "CT003";
                        _repConTp.Rct_ini = 1;
                        _repConTps = CHNLSVC.Inventory.GET_REF_COND_TP(_repConTp);
                        if (_repConTps != null)
                        {
                            if (_repConTps.Count > 0)
                            {
                                txtMainConPrice.Text = _repConTps[0].Rct_cha.ToString("N2");
                                if (_repConTps[0].Rct_ini == 1 && _repConTps[0].Rct_comp == 0)
                                {
                                    pnlCondition.Enabled = true;
                                }
                            }
                        }
                        LoadTotal();
                    }
                    else
                    {
                        txtCondition.ToolTip = "";
                        txtCondition.Text = "";
                        txtCondition.Focus();
                        DispMsg("Please enter a valid condition !!!");
                        return;
                    }
                }
                else
                {
                    txtCondition.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnAddCon_Click(object sender, EventArgs e)
        {
            try
            {
                List<ItemConditionSetup> _tmpList = new List<ItemConditionSetup>();
                _tmpList = _repConSetups;
                if (string.IsNullOrEmpty(txtCondition.Text))
                {
                    DispMsg("Please add condition !!!"); return;
                }
                if (string.IsNullOrEmpty(txtConRemarks.Text))
                {
                    DispMsg("Please add remark !!!"); return;
                }
                if (string.IsNullOrEmpty(txtSubRemark.Text))
                {
                    txtSubCon.Focus(); DispMsg("Please add remark !!!"); return;
                }
                if (string.IsNullOrEmpty(txtSubCon.Text))
                {
                    txtSubCon.Text = ""; txtSubCon.Focus(); DispMsg("Please select a condition !!!"); return;
                }
                if (!string.IsNullOrEmpty(txtSubRemark.Text))
                {
                    if (txtSubRemark.Text.Length > 199)
                    {
                        txtSubRemark.Text = ""; txtSubRemark.Focus(); DispMsg("Remark length exceeded !!!"); return;
                    }
                }

                bool isexits = _repConSetups.Exists(x => x.irsc_ser_id == Convert.ToInt32(lblSerId.Text) && x.ItemCode == txtItmCode.Text && x.irsc_tp == txtCondition.Text);
                if (!isexits)
                {
                    _repConSetup = new ItemConditionSetup();
                    _repConSetup.irsc_tp = txtCondition.Text.Trim();
                    _repConSetup.tmpCondescription = TextDesc.Text.Trim();
                    _repConSetup.irsc_ser_id = Convert.ToInt32(lblSerId.Text);
                    _repConSetup.irsc_rmk = txtConRemarks.Text;
                    _repConSetup.tmpRemark = _repConSetup.irsc_rmk.Length > 60 ? _repConSetup.irsc_rmk.Substring(0, 50) : _repConSetup.irsc_rmk;
                    _repConSetup.irsc_cha = Convert.ToDecimal(txtMainConPrice.Text.ToString());
                    _repConSetup.ItemCode = txtItmCode.Text.ToString();
                    _repConSetup.StatusText = lblstus.Text.ToString();
                    _repConSetup.ItemSearial = txtserial.Text.ToString();
                    _tmpList.Add(_repConSetup);
                    _repConSetups = _tmpList;
                }

                decimal chg = 0;
                if (!Decimal.TryParse(txtSubCharg.Text, out chg))
                {
                    txtSubCharg.Text = ""; txtSubCharg.Focus(); DispMsg("Please select valid charge !!!"); return;
                }
                //var mainCon = _tmpList.Where(c => c.irsc_tp == txtCondition.Text.Trim().ToUpper()).FirstOrDefault();
                if (txtCondition.Text.Trim().ToUpper() == txtSubCon.Text.Trim().ToUpper())
                {
                    txtSubCon.Text = ""; txtSubCon.ToolTip = ""; txtSubCon.Focus();
                    DispMsg("Entered condition already exist in intial condition !!!"); return;
                }
                //var v = _tmpList.Where(c => c.irsc_tp == txtSubCon.Text.Trim().ToUpper() && c.ItemCode == txtItmCode.Text.ToString() && c.ItemSearial==txtserial.Text).FirstOrDefault();
                var v = _tmpList.Where(c => c.irsc_tp == txtSubCon.Text.Trim().ToUpper() && c.ItemCode == txtItmCode.Text.ToString() && c.irsc_ser_id == Convert.ToInt32(lblSerId.Text)).FirstOrDefault();
                if (v != null)
                {
                    txtSubCon.Text = ""; txtSubCon.ToolTip = ""; txtSubCon.Focus();
                    DispMsg("Entered condition already exist !!!"); return;
                }


                _repConSetup = new ItemConditionSetup();
                _repConSetup.irsc_tp = txtSubCon.Text.Trim().ToUpper();
                _repConSetup.tmpCondescription = txtSubCon.ToolTip.Trim().ToUpper();
                _repConSetup.irsc_rmk = txtSubRemark.Text;
                _repConSetup.tmpRemark = _repConSetup.irsc_rmk.Length > 60 ? _repConSetup.irsc_rmk.Substring(0, 50) : _repConSetup.irsc_rmk;
                _repConSetup.irsc_cha = chg;
                _repConSetup.ItemCode = txtItmCode.Text.ToString();
                if (txtNewStockSts.Text.Equals(string.Empty)) //Added by dulaj 2018-Mar-26
                {
                    _repConSetup.StatusText = lblstus.Text.ToString();
                }
                else
                {
                    if(txtNewStockSts.Text.Equals(lblstus.Text.ToString()))
                    {
                        DispMsg("Status is not changed!"); return;
                    }
                    else
                    {
                        _repConSetup.StatusText = txtNewStockSts.Text;//Added by Dulaj
                    }
                }
          //      _repConSetup.StatusText = lblstus.Text.ToString();
            
                _repConSetup.ItemSearial = txtserial.Text.ToString();
                _repConSetup.irsc_ser_id = Convert.ToInt32(lblSerId.Text);
                _tmpList.Add(_repConSetup);
                _repConSetups = _tmpList;
                BindConditionTp();
                LoadTotal();
                txtSubCon.Text = "";
                txtSubCharg.Text = "";
                txtSubRemark.Text = "";
             //   txtNewStockSts.Text = "";
                txtDescription.Text = "";
                //pnlInitialFeedBack.Enabled = false;
                txtNewStockSts.Enabled = false;
                LinkButton2.Enabled = false;
                isConventionalCheckBox.Enabled = false;
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private void BindConditionTp()
        {
            dgvCond.DataSource = new int[] { };
            if (_repConSetups != null)
            {
                if (_repConSetups.Count > 0)
                {
                    dgvCond.DataSource = _repConSetups;
                }
            }
            dgvCond.DataBind();

            Label lblChgHeder = dgvCond.HeaderRow.FindControl("lblChgHeder") as Label;
            lblChgHeder.Text = lblChgHeder.Text + " (" + (string)Session["CurrCode"] + ")";
        }

        private void LoadTotal()
        {
            decimal _initialChg = 0;
            decimal _subChg = 0;
            decimal _iniTot = decimal.TryParse(txtMainConPrice.Text.Trim(), out _initialChg) ? _initialChg : 0;
            if (_repConSetups != null)
            {
                if (_repConSetups.Count > 0)
                {
                    _subChg = _repConSetups.Sum(c => c.irsc_cha);

                }
            }
            //lblTotal.Text = (_iniTot + _subChg).ToString("N2");
            lblTotal.Text = (_subChg).ToString("N2");
        }
        protected void lbtnSeSubCon_Click(object sender, EventArgs e)
        {
            try
            {
             
                _serData = new DataTable();
                _serType = "SubPromotion";
                dgvResult.DataSource = new int[] { };
                _serData = CHNLSVC.CommonSearch.SER_REF_COND_TP_NEW(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion), null, null);
                if (_serData.Rows.Count > 0)
                {
                    _serData.Columns["Description"].SetOrdinal(0);
                    _serData.Columns["Code"].SetOrdinal(1);
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchWord.Text = "";
                txtSearchWord.Focus();
                _serType = "SubPromotion";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtSubCon_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool b16051 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16051);
                txtMainConPrice.ReadOnly = true;
                txtSubCharg.ReadOnly = true;
                if (b16051)
                {
                    txtMainConPrice.ReadOnly = false;
                    txtSubCharg.ReadOnly = false;
                }
                else
                {
                    txtMainConPrice.ReadOnly = true;
                    txtSubCharg.ReadOnly = true;
                }

                txtSubCharg.Text = "";
                if (!string.IsNullOrEmpty(txtSubCon.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = CHNLSVC.CommonSearch.SER_REF_COND_TP_NEW(SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion), "Code", txtSubCon.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtSubCon.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtSubCon.ToolTip = toolTip;
                        txtDescription.Text = toolTip;
                        _repConTp = new RepConditionType();
                        _repConTp.Rct_com = Session["UserCompanyCode"].ToString();
                        _repConTp.Rct_tp = txtSubCon.Text.Trim().ToUpper();
                        _repConTp.Rct_cat1 = txtSubConCat.Text.Trim().ToUpper();
                        _repConTp.Rct_ini = 0;
                        _repConTps = CHNLSVC.Inventory.GET_REF_COND_TP(_repConTp);
                        if (_repConTps != null)
                        {
                            if (_repConTps.Count > 0)
                            {
                                if (isConventionalCheckBox.Checked)
                                {
                                    txtSubCharg.Text = _repConTps[0].Rct_cha.ToString("N2");
                                }
                                else
                                {
                                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                                    decimal amount = 0;                                    
                                    DataTable prodCondTable = CHNLSVC.General.GetProductCondtionParameters(Session["UserCompanyCode"].ToString(), "PCONAGE",dt_location.Rows[0]["ml_sev_chnl"].ToString(), txtNewStockSts.Text);
                                    if (prodCondTable.Rows.Count ==0)
                                    {
                                        prodCondTable = CHNLSVC.General.GetProductCondtionParametersByCd(Session["UserCompanyCode"].ToString(), "PCONAGE", Session["UserCompanyCode"].ToString(), txtNewStockSts.Text);
                                    }                                    
                                    if (prodCondTable.Rows.Count > 0)
                                    {
                                        amount = CHNLSVC.Inventory.Get_def_price_from_pc(prodCondTable.Rows[0]["rpp_pb"].ToString(), prodCondTable.Rows[0]["rpp_pb_lvl"].ToString(), txtItmCode.Text, DateTime.Now);
                                        if (txtNewStockSts.Text.Equals("GOD"))
                                        {
                                            amount = 0;
                                        }
                                        else if (Session["CHECK"] != "0")
                                        {
                                            amount = 0; 
                                        }
                                        else
                                        {
                                            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), prodCondTable.Rows[0]["rpp_pb"].ToString(), prodCondTable.Rows[0]["rpp_pb_lvl"].ToString());
                                            decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItmCode.Text.Trim(), "GOD".Trim(), 1, _priceBookLevelRef, amount, 0, 0, true), true);
                                            decimal baseRate = Convert.ToDecimal(prodCondTable.Rows[0]["rpp_base_rate"].ToString())/100;
                                            decimal calRate = Convert.ToDecimal(prodCondTable.Rows[0]["rpp_cal_rate"].ToString())/100;
                                            amount = amount+_vatPortion;
                                            amount = (amount *baseRate ) *calRate ;

                                            Session["CHECK"] = "1";
                                        }
                                        txtSubCharg.Text = SetDecimalPoint((Convert.ToString(amount)));
                                    }
                                    else
                                    {
                                        DispMsg("Charge Condition is not applied for this status, Charge is not changed !!!", "N");
                                        txtSubCharg.Text = _repConTps[0].Rct_cha.ToString("N2");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        txtSubCon.ToolTip = "";
                        txtSubCon.Text = "";
                        txtDescription.Text = string.Empty;
                        txtSubCon.Focus();
                        DispMsg("Please enter a valid condition !!!");
                        return;
                    }
                }
                else
                {
                    txtSubCon.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _serData = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
                DataTable _dt = new DataTable();
                _dt.Columns.Add("Code");
                _dt.Columns.Add("Description");
                foreach (DataRow dr in _serData.Rows)
                {
                    DataRow ddr = _dt.NewRow();
                    ddr["Code"] = dr["MIC_CD"].ToString();
                    ddr["Description"] = dr["MIS_DESC"].ToString();
                    _dt.Rows.Add(ddr);
                }
                DataView dv = _dt.DefaultView;
                dv.Sort = "Code ASC";
                DataTable sortedDT = dv.ToTable();
                _serData = sortedDT;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "Status";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtNewStockSts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNewStockSts.Text))
                {
                    bool b10116 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10116);
                    bool b10117 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10117);
                    if (!b10117)
                    {
                        // if (!b10117)
                        //{
                        txtNewStockSts.Text = ""; DispMsg("Sorry, You have no permission ! (Advice: Required permission code : 10117)");
                        //txtNewStatus.Text = ""; DispMsg("Sorry, You have no permission ! (Advice: Required permission code : 10117)"); return;
                        //}
                    }
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["MIC_CD"].ToString()))
                        {
                            if (txtNewStockSts.Text.ToUpper() == row["MIC_CD"].ToString())
                            {
                                b2 = true;
                                toolTip = row["MIS_DESC"].ToString();
                                txtSubCon.Text = "";
                                txtSubCharg.Text = "";
                                txtDescription.Text = "";
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtNewStockSts.ToolTip = toolTip;
                    }
                    else
                    {
                        txtNewStockSts.ToolTip = "";
                        txtNewStockSts.Text = "";
                        txtNewStockSts.Focus();
                        DispMsg("Please enter a valid status !!!");
                        return;
                    }
                }
                else
                {
                    txtNewStockSts.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private void LoadGridStatus()
        {
            foreach (GridViewRow rw in dvPendingPO.Rows)
            {
                Label lblothloc = rw.FindControl("lblothloc") as Label;
                lblothloc.ToolTip = "";
                MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), lblothloc.Text.Trim().ToUpper());
                if (_mstLoc != null)
                {
                    lblothloc.ToolTip = _mstLoc.Ml_loc_desc;
                }
            }
        }

        protected void txtAODNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAODNumber.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    string filter = (string)Session["USER_TYPE"];
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate);
                    DataTable _result = CHNLSVC.Inventory.LoadProductConditionPopUp(SearchParams, "Doc No", txtAODNumber.Text.Trim(), filter);
                    DataTable _result2 = CHNLSVC.Inventory.LoadProductConditionPopUp(SearchParams, "OTHER DOC NO", txtAODNumber.Text.Trim(), filter);
                    DataTable dtAll;
                    dtAll = _result.Copy();
                    dtAll.Merge(_result2);
                    string origdoc = "";
                    foreach (DataRow row in dtAll.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Doc No"].ToString()) || !string.IsNullOrEmpty(row["OTHER DOC NO"].ToString()))
                        {
                            if (txtAODNumber.Text.ToUpper() == row["Doc No"].ToString() || txtAODNumber.Text.ToUpper() == row["OTHER DOC NO"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Doc No"].ToString();
                                origdoc = toolTip;
                                toolTip = row["OTHER DOC NO"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtAODNumber.ToolTip = toolTip;

                        InventoryHeader _invHdr = new InventoryHeader();
                        _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(origdoc);

                        DataTable dtresultcopyIntr = new DataTable();
                        dtresultcopyIntr.Columns.AddRange(new DataColumn[4] { new DataColumn("ith_doc_no"), new DataColumn("ith_doc_date"), new DataColumn("ith_oth_docno"), new DataColumn("ith_oth_loc") });

                        DateTime date = _invHdr.Ith_doc_date;

                        string formatteddate = ((DateTime)date).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                        dtresultcopyIntr.Rows.Add(_invHdr.Ith_doc_no, formatteddate, _invHdr.Ith_oth_docno, _invHdr.Ith_oth_loc);

                        dvPendingPO.DataSource = null;
                        dvPendingPO.DataBind();

                        dvPendingPO.DataSource = dtresultcopyIntr;
                        dvPendingPO.DataBind();
                        LoadGridStatus();
                    }
                    else
                    {
                        txtAODNumber.ToolTip = "";
                        txtAODNumber.Text = "";
                        txtAODNumber.Focus();
                        DispMsg("Please select a valid outward no !!!");
                        return;
                    }
                }
                else
                {
                    txtAODNumber.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }


        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedIndex > 0)
            {
                ddlType.ToolTip = ddlType.SelectedItem.Text;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;

            if (row != null)
            {
                Label lblirsc_tp = row.FindControl("lblirsc_tp") as Label;
                var v = _repConSetups.Where(c => c.irsc_tp == lblirsc_tp.Text).FirstOrDefault();
                if (v != null)
                {
                    _repConSetups.Remove(v);
                }
                //dilshan
                if (v.irsc_cha > 0)
                {
                    Session["CHECK"] = "0";
                }
            }
            BindConditionTp();
            LoadTotal();
        }
        //Added By Dulaj 2018-Mar-09
        protected void ConventionalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //btnGetPO_Click(null, null);
        }
        private string SetDecimalPoint(string amount)
        {
            decimal value = Convert.ToDecimal(amount);
            return value.ToString("N2");
        }
        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            
            return value;
        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    bool _isStrucBaseTax = false;
                    string lblVatExemptStatus = "Available";
                   // if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                   // else _isVATInvoice = false;                   
                    
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                        {
                            _taxs = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, _status);
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), _item, _status, null, null, _mstItem.Mi_anal1);
                            }
                            else
                            {
                                _taxs = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, _status);
                            }

                        }
                        else
                        {
                            //_taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(),_item, _status, string.Empty, "VAT");
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT");
                        }

                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            if (lblVatExemptStatus == "Available")
                            {
                                if (_isTaxfaction == false)
                                    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                    }
                                    else
                                        _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                            }
                            else
                            {
                                if (_isTaxfaction) _pbUnitPrice = 0;
                            }
                        }
                                        
                }
                else
                    if (_isTaxfaction) _pbUnitPrice = 0;
            return _pbUnitPrice;
        }
       
    }
}