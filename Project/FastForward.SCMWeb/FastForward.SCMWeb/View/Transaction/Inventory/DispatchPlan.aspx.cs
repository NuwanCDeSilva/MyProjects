using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects.InventoryNew;
using FastForward.SCMWeb.View.Reports.Warehouse;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastMember;
using System.Security.Principal;
using System.Diagnostics;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class DispatchPlan : Base
    {
        ReptPickHeader _tmpPickHdr = new ReptPickHeader();
        List<ReptPickSerials> _repSerList = new List<ReptPickSerials>();
        List<ReptPickItems> _repItmList = new List<ReptPickItems>();
        //private MasterLocationNew _mstLocNew = new MasterLocationNew();
        private List<InventoryRequestItem> _itmChgReqList
        {
            get { if (Session["_itmChgReqList"] != null) { return (List<InventoryRequestItem>)Session["_itmChgReqList"]; } else { return new List<InventoryRequestItem>(); } }
            set { Session["_itmChgReqList"] = value; }
        }
        private bool _showPopSendToPda
        {
            get { if (Session["_showPopSendToPda"] != null) { return (bool)Session["_showPopSendToPda"]; } else { return false; } }
            set { Session["_showPopSendToPda"] = value; }
        }
        private Int32 _chgItmCdLine
        {
            get { if (Session["_chgItmCdLine"] != null) { return (Int32)Session["_chgItmCdLine"]; } else { return 0; } }
            set { Session["_chgItmCdLine"] = value; }
        }
        private bool _showItmChg
        {
            get { if (Session["_showItmChg"] != null) { return (bool)Session["_showItmChg"]; } else { return false; } }
            set { Session["_showItmChg"] = value; }
        }
        private bool _dataSaveProcess
        {
            get { if (Session["_dataSaveProcess"] != null) { return (bool)Session["_dataSaveProcess"]; } else { return false; } }
            set { Session["_dataSaveProcess"] = value; }
        }
        protected Int32 IndexHeader { get { return (Int32)Session["IndexHeader"]; } set { Session["IndexHeader"] = value; } }
        protected Int32 IndexDetails { get { return (Int32)Session["IndexDetails"]; } set { Session["IndexDetails"] = value; } }
        protected Int32 IndexStock { get { return (Int32)Session["IndexStock"]; } set { Session["IndexStock"] = value; } }
        protected List<InventoryRequest> oRequestHeaders { get { return (List<InventoryRequest>)Session["oRequestHeaders"]; } set { Session["oRequestHeaders"] = value; } }
        protected List<InventoryLocation> oLocItems { get { return (List<InventoryLocation>)Session["oLocItems"]; } set { Session["oLocItems"] = value; } }
        protected List<InventoryRequestItem> oRequesItems { get { return (List<InventoryRequestItem>)Session["oRequesItems"]; } set { Session["oRequesItems"] = value; } }
        protected List<InventoryRequestItem> oAppredItems { get { return (List<InventoryRequestItem>)Session["oAppredItems"]; } set { Session["oAppredItems"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //checkAllcancel();
            if (!IsPostBack)
            {
                _dataSaveProcess = false;
                _showPopSendToPda = false;
                LoadRequestTypes(1);
                clear();
                LoadAppLevelStatus();
                PopulateLoadingBays();
                ViewState["sortOrder"] = "desc";
                Session["ISRESNO"] = "";
                Session["ApproveAllSuccess"] = "";
            }
            else
            {
                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];

                string[] objectNames = ctrlName.ToString().Split('$');

                if (Session["SY"] == null)
                {
                    mpSearch.Hide();
                }
                else
                {
                   // mpSearch.Show();
                }
                if (_showPopSendToPda)
                {
                    popSendToPda.Show();
                }
                else
                {
                    popSendToPda.Hide();
                }
                if (_showItmChg)
                {
                    popItemChg.Show();
                }
                else
                {
                    popItemChg.Hide();
                }
            }
        }

        string _sortDirection;
        string SortDireaction;
        DataTable dataTable;
        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }
                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }
        private void clear()
        {
            _itmChgReqList = new List<InventoryRequestItem>();
            _showItmChg = false;
            BindLodingBay();
            chkAppDoc.Enabled = true;
            chkAppDoc.Checked = false;
            btnSave.Visible = true;
            btnshow.Visible = true;
            btnApproveAll.Visible = true;
            _chgItmCdLine = 0;
            DataTable dtchk_warehouse_availability = CHNLSVC.Inventory.CheckWareHouseAvailability(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

            if (dtchk_warehouse_availability.Rows.Count > 0)
            {
                chkpda.Enabled = true;

                foreach (DataRow ddrware in dtchk_warehouse_availability.Rows)
                {
                    Session["WAREHOUSE_COM"] = ddrware["ml_wh_com"].ToString();
                    Session["WAREHOUSE_LOC"] = ddrware["ml_wh_cd"].ToString();
                }
            }
            else
            {
                chkpda.Enabled = false;
            }
            Session["loadingbay"] = "";
            Session["_status2"] = "";
            oRequestHeaders = new List<InventoryRequest>();
            oLocItems = new List<InventoryLocation>();
            oRequesItems = new List<InventoryRequestItem>();

            oAppredItems = new List<InventoryRequestItem>();

            IndexHeader = -1;
            IndexDetails = -1;
            IndexStock = -1;

            txtRoute.Text = "All";
            txtRoute.ReadOnly = true;
            chkRouteAll.Checked = true;

            txtMainCategory.Text = "All";
            txtMainCategory.ReadOnly = true;
            chkMainCategoryAll.Checked = true;

            txtItemCode.Text = "All";
            txtItemCode.ReadOnly = true;
            chkItemCode.Checked = true;

            txtLocation.Text = "";

            txtLocation.Text = Session["UserDefLoca"].ToString();

            chkSubCategoryAll.Checked = true;
            txtSubCategory.Text = "All";
            txtSubCategory.ReadOnly = true;

            txtModel.Text = "All";
            chkModelAll.Checked = true;
            txtModel.ReadOnly = true;

            txtDate.Text = DateTime.Now.AddDays(-5).ToString("dd/MMM/yyyy hh:mm tt");
            txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt");

            dgvRequestHeader.DataSource = new int[] { };
            dgvRequestHeader.DataBind();

            dgvCurrentStockDetails.DataSource = new int[] { };
            dgvCurrentStockDetails.DataBind();

            dgvRequestItems.DataSource = new int[] { };
            dgvRequestItems.DataBind();

            dgvApprovedItems.DataSource = new int[] { };
            dgvApprovedItems.DataBind();

            if (ddlReqType.Items.Count > 0)
            {
                if (!_dataSaveProcess)
                {
                    ddlReqType.SelectedIndex = 0;
                }
            }

            dgvRequestHeader.Enabled = true;

            txtAmount.Text = "";
            txtRequestNumber.Text = "";
            txtSearchbyword.Text = "";
            _dataSaveProcess = false;
        }

        private void clearnew()
        {
            chkAppDoc.Enabled = true;
            chkAppDoc.Checked = false;
            btnSave.Visible = true;
            btnshow.Visible = true;
            btnApproveAll.Visible = true;
            Session["ISRESNO"] = "";
            DataTable dtchk_warehouse_availability = CHNLSVC.Inventory.CheckWareHouseAvailability(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

            if (dtchk_warehouse_availability.Rows.Count > 0)
            {
                chkpda.Enabled = true;

                foreach (DataRow ddrware in dtchk_warehouse_availability.Rows)
                {
                    Session["WAREHOUSE_COM"] = ddrware["ml_wh_com"].ToString();
                    Session["WAREHOUSE_LOC"] = ddrware["ml_wh_cd"].ToString();
                }
            }
            else
            {
                chkpda.Enabled = false;
            }
            Session["loadingbay"] = "";
            Session["_status2"] = "";
            //oRequestHeaders = new List<InventoryRequest>();
            oLocItems = new List<InventoryLocation>();
            oRequesItems = new List<InventoryRequestItem>();

            oAppredItems = new List<InventoryRequestItem>();

          //  IndexHeader = -1;
            IndexDetails = -1;
            IndexStock = -1;

            txtRoute.Text = "All";
            txtRoute.ReadOnly = true;
            chkRouteAll.Checked = true;

            txtMainCategory.Text = "All";
            txtMainCategory.ReadOnly = true;
            chkMainCategoryAll.Checked = true;

            txtItemCode.Text = "All";
            txtItemCode.ReadOnly = true;
            chkItemCode.Checked = true;

            txtLocation.Text = "";

            txtLocation.Text = Session["UserDefLoca"].ToString();

            chkSubCategoryAll.Checked = true;
            txtSubCategory.Text = "All";
            txtSubCategory.ReadOnly = true;

            txtModel.Text = "All";
            chkModelAll.Checked = true;
            txtModel.ReadOnly = true;

            //txtDate.Text = DateTime.Now.AddDays(-5).ToString("dd/MMM/yyyy hh:mm tt");
            //txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy hh:mm tt");

            //dgvRequestHeader.DataSource = new int[] { };
            //dgvRequestHeader.DataBind();

            dgvCurrentStockDetails.DataSource = new int[] { };
            dgvCurrentStockDetails.DataBind();

            dgvRequestItems.DataSource = new int[] { };
            dgvRequestItems.DataBind();

            dgvApprovedItems.DataSource = new int[] { };
            dgvApprovedItems.DataBind();

            if (ddlReqType.Items.Count > 0)
            {
                if (!_dataSaveProcess)
                {
                    ddlReqType.SelectedIndex = 0;
                }
            }

          //  dgvRequestHeader.Enabled = true;

            txtAmount.Text = "";
            txtRequestNumber.Text = "";
            txtSearchbyword.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (hdfSave.Value == "1")
            {
                btnSave.Enabled = false;
                btnSave.CssClass = "buttoncolorleft";
                btnSave.OnClientClick = "return Enable();";
                _dataSaveProcess = true;
                if (dgvRequestHeader.Rows.Count == 0)
                {
                    DisplayMessage("Please select the pending request doc number");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "confSave();";
                    return;
                }
                if (IndexHeader == -1)
                {
                    DisplayMessage("Please select the pending request doc number");
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "confSave();";
                    return;
                }

                GridViewRow drHeader = dgvRequestHeader.Rows[IndexHeader];
                Label lblITR_SEQ_NO = drHeader.FindControl("lblITR_SEQ_NO") as Label;
                Label lblITR_REQ_NO = drHeader.FindControl("lblITR_REQ_NO") as Label;
                Label lblitr_dt = drHeader.FindControl("lblitr_dt") as Label;
                Label lblITR_LOC = drHeader.FindControl("lblITR_LOC") as Label;
                Label lblITR_ISSUE_FROM = drHeader.FindControl("lblITR_ISSUE_FROM") as Label;
                Label lblITR_REC_TO = drHeader.FindControl("lblITR_REC_TO") as Label;
                Label lblItr_tp = drHeader.FindControl("lblItr_tp") as Label;

                string DocType = string.Empty;
                string SubDocType = string.Empty;
                string DocNumber = string.Empty;

                DocNumber = lblITR_REQ_NO.Text.Trim();

                DocType = setDocumentType(lblItr_tp.Text.Trim());

                List<InventoryRequest> oHeaders = new List<InventoryRequest>();
                List<TmpValidation> _errList = new List<TmpValidation>();
                if (oAppredItems != null && oAppredItems.Count > 0)
                {
                    string[] oAppLocations = oAppredItems.Select(x => x.Itri_loc).Distinct().ToArray();
                    if (oAppLocations != null && oAppLocations.Length > 0)
                    {
                        foreach (string oAppLoc in oAppLocations)
                        {
                            #region supplier valdation add by lakshan 30 Jun 2017
                            InventoryRequest _invreqSup =  CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = DocNumber }).FirstOrDefault();
                            if (_invreqSup != null)
                            {
                                if (_invreqSup.Itr_tp=="SO")
                                {
                                    MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileByCom(_invreqSup.Itr_bus_code, null, null, null, null, 
                                        Session["UserCompanyCode"].ToString());
                                    if (_mstBusEntity != null)
                                    {
                                        if (_mstBusEntity.Mbe_is_suspend == true)
                                        {
                                            _errList.Add(new TmpValidation() { _isErr = true, ErrTP = _mstBusEntity.Mbe_cd});
                                        }
                                    }
                                }
                            }
                            #endregion
                            InventoryRequest oHeader = new InventoryRequest();
                            oHeader.TMP_IS_RES_UPDATE = true;
                            oHeader.Itr_com = Session["UserCompanyCode"].ToString();
                            oHeader.Itr_req_no = string.Empty;
                            oHeader.Itr_tp = DocType;
                            //set sub type subodana
                            InventoryRequest _tmpReq = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = lblITR_REQ_NO.Text.ToString() }).FirstOrDefault();
                            if (_tmpReq!=null)
                            {
                                oHeader.Itr_sub_tp = _tmpReq.Itr_sub_tp;
                                oHeader.Itr_job_no = _tmpReq.Itr_job_no;
                                oHeader.Itr_job_line = _tmpReq.Itr_job_line;
                            }
                            oHeader.Itr_loc = oAppLoc;  
                            oHeader.Itr_ref = DocNumber;
                            oHeader.Itr_dt = DateTime.Now.Date;
                            oHeader.Itr_exp_dt = DateTime.Now.Date;
                            oHeader.Itr_stus = "A";
                            oHeader.Itr_bus_code = string.Empty;
                            oHeader.Itr_note = string.Empty;
                            oHeader.Itr_issue_from = oAppLoc;
                            oHeader.Itr_rec_to = lblITR_REC_TO.Text;
                            oHeader.Itr_direct = 0;
                            oHeader.Itr_country_cd = string.Empty;
                            oHeader.Itr_town_cd = string.Empty;
                            oHeader.Itr_cur_code = string.Empty;
                            oHeader.Itr_exg_rate = 0;
                            oHeader.Itr_collector_id = string.Empty;
                            oHeader.Itr_collector_name = string.Empty;
                            oHeader.Itr_act = 1;
                            oHeader.Itr_cre_by = Session["UserID"].ToString();
                            oHeader.Itr_session_id = Session["SessionID"].ToString();
                            oHeader.Itr_issue_com = Session["UserCompanyCode"].ToString();

                            MasterAutoNumber masterAuto = new MasterAutoNumber();
                            masterAuto.Aut_cate_tp = "LOC";
                            masterAuto.Aut_cate_cd = oAppLoc;
                            masterAuto.Aut_direction = null;
                            masterAuto.Aut_modify_dt = null;
                            masterAuto.Aut_moduleid = DocType;
                            masterAuto.Aut_number = 0;
                            masterAuto.Aut_start_char = DocType;
                            masterAuto.Aut_year = null;

                            oHeader._mastAutoNo = masterAuto;

                            List<InventoryRequestItem> oSaveItems = oAppredItems.FindAll(x => x.Itri_loc == oAppLoc);
                            Int32 lineNum = 0;
                            foreach (InventoryRequestItem oSaveItem in oSaveItems)
                            {
                                lineNum = lineNum + 1;

                                decimal Qty = oSaveItem.Itri_qty;

                                oSaveItem.Itri_seq_no = 0;
                                oSaveItem.Itri_line_no = lineNum;
                                oSaveItem.Itri_qty = Qty;
                                oSaveItem.Itri_unit_price = 0;
                                oSaveItem.Itri_app_qty = Qty;
                                oSaveItem.Itri_mqty = Qty;
                                oSaveItem.Itri_bqty = Qty;
                                oSaveItem.Itri_res_qty = Qty;
                                oSaveItem.Itri_res_line = 0;
                                oSaveItem.Itri_job_no = oHeader.Itr_job_no;
                                oSaveItem.Itri_job_line = oHeader.Itr_job_line;
                            }

                            oHeader.InventoryRequestItemList = oSaveItems;

                            oHeaders.Add(oHeader);
                        }
                        string err = string.Empty;
                        string GenDoc = string.Empty;
                        ViewState["oRequesItems"] = oRequesItems;
                        string warehousecom = (string)Session["WAREHOUSE_COM"];
                        string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                        string loadingbay = (string)Session["loadingbay"];
                        bool PDA = false;
                        if (chkpda.Checked == true)
                        {
                            PDA = true;
                        }
                        foreach (var item in oHeaders)
                        {
                            item.TMP_Val_Itms = true;
                        }
                        #region add validation 30 Jun 2017
                        var _isErrAva = _errList.Where(c => c._isErr == true).ToList();
                        if (_isErrAva!=null)
                        {
                            if (_errList.Count > 0)
                            {
                                DispMsg("This customer already suspended : "+_errList[0].ErrTP);
                                btnSave.Enabled = true;
                                btnSave.CssClass = "buttonUndocolorLeft floatRight";
                                btnSave.OnClientClick = "confSave();";
                                return;
                            }
                        }
                        #endregion

                        Int32 result = CHNLSVC.Inventory.SaveDispatchPlan(oHeaders, oRequesItems, DocNumber, false, false, PDA, warehousecom, warehouseloc, loadingbay, out GenDoc, out err, Session["ISRESNO"].ToString(), false);
                        if (result > 0)
                        {

                            DisplayMessage("Successfully Approved. Document Number : " + GenDoc, 3);
                            clearnew();
                            btnSave.Enabled = true;
                            btnSave.CssClass = "buttonUndocolorLeft floatRight";
                            btnSave.OnClientClick = "confSave();";
                            //btnSearch_Click(null, null); 
                            #region Clear Data
                            oRequestHeaders = new List<InventoryRequest>();
                            oLocItems = new List<InventoryLocation>();
                            oRequesItems = new List<InventoryRequestItem>();

                            oAppredItems = new List<InventoryRequestItem>();

                            IndexHeader = -1;
                            IndexDetails = -1;
                            IndexStock = -1;

                            dgvRequestHeader.DataSource = new int[] { };
                            dgvRequestHeader.DataBind();

                            dgvCurrentStockDetails.DataSource = new int[] { };
                            dgvCurrentStockDetails.DataBind();

                            dgvRequestItems.DataSource = new int[] { };
                            dgvRequestItems.DataBind();

                            dgvApprovedItems.DataSource = new int[] { };
                            dgvApprovedItems.DataBind();

                            dgvRequestHeader.Enabled = true;
                            #endregion
                            lblMssg.Text = "Do you want to load the pending requests ? ";
                            SbuPopup.Show();
                            lbtnPopOk.Focus();
                            return;
                        }
                        else
                        {
                            if (err.Contains("Reservation"))
                            {
                                DispMsg(err);
                            }
                            else
                            {
                                DispMsg(err,"E");
                            }
                            btnSave.Enabled = true;
                            btnSave.CssClass = "buttonUndocolorLeft floatRight";
                            btnSave.OnClientClick = "confSave();";
                        }
                    }
                }
                else
                {
                    DisplayMessage("Please add approved stock details");
                    return;
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (hdfClear.Value == "1")
            {
                //clear();
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
        }
        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            //if (dgvApprovedItems.Rows.Count == 0) return;
            //var lb = (TextBox)sender;
            //var row = (GridViewRow)lb.NamingContainer;

            //string value = (row.FindControl("lblitri_app_qty") as TextBox).Text;
            //decimal number;

            if (hdfCancel.Value == "1")
            {
                string err = string.Empty;
                InventoryRequest oHeaders = Session["oHeaders"] as InventoryRequest;
                if (oHeaders == null)
                {
                    DisplayMessage("Please select the doc no.", 2);
                    return;
                }
                if (oRequesItems != null)
                {
                    if (oRequesItems.Count == 0)
                    {
                        DisplayMessage("Approved Items not found", 2);
                        return;
                    }
                }

                //dilshan
                //if ((row.FindControl("lblitri_app_qty") as TextBox).Text == "")
                //{
                //    DispMsg("Please enter the quantity");
                //    return;
                //}

                //if (Decimal.TryParse(value, out number))
                //{
                //    //Console.WriteLine(number);
                //}
                //else
                //{
                //    DispMsg("Please enter the valid quantity");
                //    return;
                //}
                cancel();
                // Int32 result = CHNLSVC.Inventory.Cancel_DispatchPlan(oHeaders, oRequesItems, out err);
                //if (result > 0)
                //{
                //    DisplayMessage("Successfully Cancelled", 3);
                //    clear();


                //    return;
                //}
                //else
                //{
                //    DisplayMessage(err, 4);
                //}
            }
        }

        private void cancel()
        {
            GridViewRow drHeader = dgvRequestHeader.Rows[IndexHeader];
            Label lblITR_SEQ_NO = drHeader.FindControl("lblITR_SEQ_NO") as Label;
            Label lblITR_REQ_NO = drHeader.FindControl("lblITR_REQ_NO") as Label;
            Label lblITR_REF = drHeader.FindControl("lblITR_REF") as Label;
            
            Label lblitr_dt = drHeader.FindControl("lblitr_dt") as Label;
            Label lblITR_LOC = drHeader.FindControl("lblITR_LOC") as Label;
            Label lblITR_ISSUE_FROM = drHeader.FindControl("lblITR_ISSUE_FROM") as Label;
            Label lblITR_REC_TO = drHeader.FindControl("lblITR_REC_TO") as Label;
            Label lblItr_tp = drHeader.FindControl("lblItr_tp") as Label;

            _tmpPickHdr = new ReptPickHeader();
            _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
            {
                Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                Tuh_doc_no = lblITR_REQ_NO.Text
            }).FirstOrDefault();

            if (_tmpPickHdr != null)
            {
                List<ReptPickSerials> _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                if (_repSerList != null)
                {
                    if (_repSerList.Count > 0)
                    {
                        DisplayMessage("Selected Item already picked serial available...!!!" , 2);
                        return;
                    }
                }
            }

            string DocType = string.Empty;
            string SubDocType = string.Empty;
            string DocNumber = string.Empty;
            bool isfinish = false;
            bool ischeck=false;
            

            CheckBox chkHeaderAllApp = dgvApprovedItems.HeaderRow.FindControl("chkHeaderAllApp") as CheckBox;

            if (chkHeaderAllApp.Checked)
            {
                ischeck = true;
            }
            else
            {
                ischeck = false;
            }

            DocNumber = lblITR_REQ_NO.Text.Trim();

            DocType = setDocumentTypeForCancel(lblItr_tp.Text.Trim());

            List<InventoryRequest> oHeaders = new List<InventoryRequest>();

            if (oAppredItems != null && oAppredItems.Count > 0)
            {
                string[] oAppLocations = oAppredItems.Select(x => x.Itri_loc).Distinct().ToArray();
                if (oAppLocations != null && oAppLocations.Length > 0)
                {
                    foreach (string oAppLoc in oAppLocations)
                    {

                        InventoryRequest oHeader = new InventoryRequest();
                        oHeader.Itr_com = Session["UserCompanyCode"].ToString();
                        oHeader.Itr_req_no = DocNumber;
                        oHeader.Itr_tp = DocType;
                        oHeader.Tmp_res_base_doc_no = DocNumber;
                        oHeader.Tmp_res_base_doc_tp = lblItr_tp.Text.Trim();
                        oHeader.Itr_sub_tp = "NOR";
                        oHeader.Itr_loc = oAppLoc;
                        oHeader.Itr_ref = lblITR_REF.Text.ToString();
                        oHeader.Itr_dt = DateTime.Now.Date;
                        oHeader.Itr_exp_dt = DateTime.Now.Date;
                        oHeader.Itr_stus = "F";
                        oHeader.Itr_job_no = string.Empty;
                        oHeader.Itr_bus_code = string.Empty;
                        oHeader.Itr_note = string.Empty;
                        oHeader.Itr_issue_from = oAppLoc;
                        oHeader.Itr_rec_to = lblITR_REC_TO.Text;
                        oHeader.Itr_direct = 0;
                        oHeader.Itr_country_cd = string.Empty;
                        oHeader.Itr_town_cd = string.Empty;
                        oHeader.Itr_cur_code = string.Empty;
                        oHeader.Itr_exg_rate = 0;
                        oHeader.Itr_collector_id = string.Empty;
                        oHeader.Itr_collector_name = string.Empty;
                        oHeader.Itr_act = 1;
                        oHeader.Itr_cre_by = Session["UserID"].ToString();
                        oHeader.Itr_session_id = Session["SessionID"].ToString();
                        oHeader.Itr_issue_com = Session["UserCompanyCode"].ToString();
                        //Update quary to cancelation 26.08.2017 Udaya comment
                        oHeader.sad_res_line_no_udt = true;

                        MasterAutoNumber masterAuto = new MasterAutoNumber();
                        masterAuto.Aut_cate_tp = "LOC";
                        //masterAuto.Aut_cate_cd = string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                        masterAuto.Aut_cate_cd = oAppLoc;
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = DocType;
                        masterAuto.Aut_number = 0;
                        masterAuto.Aut_start_char = DocType;
                        masterAuto.Aut_year = null;

                        oHeader._mastAutoNo = masterAuto;
                       
                        List<InventoryRequestItem> oSaveItems = oAppredItems.FindAll(x => x.Itri_loc == oAppLoc);
                        Int32 lineNum = 0;
                        Int32 count = oAppredItems.Count;
                        //foreach (InventoryRequestItem oSaveItem in oSaveItems)
                        //{
                            lineNum = lineNum + 1;

                          //  decimal Qty = oSaveItem.Itri_qty;
                            int i = 0;
                            Decimal _app_qty = 0;
                            Decimal Qty= 0;
                            List<InventoryRequestItem> oSaveitemnew = new List<InventoryRequestItem>();
                            foreach (GridViewRow item in dgvApprovedItems.Rows)
                            {
                                Label lblitri_itm_cd = item.FindControl("lblitri_itm_cd") as Label;
                                Label lblitri_seq_no = item.FindControl("lblitri_seq_no") as Label;
                                Label lblItri_line_no = item.FindControl("lblItri_line_no") as Label;
                                Label lblitri_app_qty = item.FindControl("lblitri_app_qty") as Label;
                                
                             
                                string itmcd = "";
                                Int32 seqno = 0;
                                Int32 _lineno = 0;
                                CheckBox ChkselectItm = item.FindControl("ChkselectItm") as CheckBox;
                                if (ChkselectItm.Checked)
                                {
                                    itmcd = lblitri_itm_cd.Text.ToString();
                                    seqno = Convert.ToInt32(lblitri_seq_no.Text.ToString());
                                    _lineno = Convert.ToInt32(lblItri_line_no.Text.ToString());
                                    _app_qty = Convert.ToDecimal(lblitri_app_qty.Text.ToString());

                                    oSaveItems = oAppredItems.Where(a => a.Itri_itm_cd == itmcd && a.Itri_seq_no == seqno && a.Itri_line_no==_lineno).ToList();
                                    Qty = oSaveItems.FirstOrDefault().Itri_bqty;

                                    if (_app_qty != Qty)
                                    {
                                        if (Qty != null)
                                        {
                                            //Qty = _app_qty;
                                            oSaveItems.First().Itri_seq_no = 0;
                                            oSaveItems.First().Itri_line_no = Convert.ToInt32(lblItri_line_no.Text.ToString());
                                            oSaveItems.First().Itri_qty = _app_qty;
                                            oSaveItems.First().Itri_unit_price = 0;
                                            oSaveItems.First().Itri_app_qty = 0;
                                            oSaveItems.First().Itri_mqty = _app_qty;
                                            oSaveItems.First().Itri_bqty = _app_qty;
                                            oSaveItems.First().Itri_res_qty = _app_qty;
                                            oSaveItems.First().Itri_res_line = 0;
                                            oSaveItems.First().Itri_cncl_qty = _app_qty;
                                        }
                                    }
                                    else
                                    {
                                        if (Qty != null)
                                        {
                                            //oSaveitemnew.Itri_seq_no = 0;
                                            //oSaveitemnew.Itri_line_no = lineNum;
                                            //oSaveitemnew.Itri_qty = Qty;
                                            //oSaveitemnew.Itri_unit_price = 0;
                                            //oSaveitemnew.Itri_app_qty = Qty;
                                            //oSaveitemnew.Itri_mqty = Qty;
                                            //oSaveitemnew.Itri_bqty = Qty;
                                            //oSaveitemnew.Itri_res_qty = Qty;
                                            //oSaveitemnew.Itri_res_line = 0;

                                            oSaveItems.First().Itri_seq_no = 0;
                                            oSaveItems.First().Itri_line_no = Convert.ToInt32(lblItri_line_no.Text.ToString());
                                            oSaveItems.First().Itri_qty = Qty;
                                            oSaveItems.First().Itri_unit_price = 0;
                                            oSaveItems.First().Itri_app_qty = 0;
                                            oSaveItems.First().Itri_mqty = Qty;
                                            oSaveItems.First().Itri_bqty = Qty;
                                            oSaveItems.First().Itri_res_qty = Qty;
                                            oSaveItems.First().Itri_res_line = 0;
                                            oSaveItems.First().Itri_cncl_qty = Qty;
                                        }
                                    }
                                    foreach (InventoryRequestItem oSaveItemnews in oSaveItems)
                                    {
                                        oSaveitemnew.Add(oSaveItemnews);
                                    }
                                    i++;
                                }
                                oHeader.InventoryRequestItemList = oSaveitemnew;
                            }
                            if (i == count | ischeck == true && _app_qty == Qty)
                            {
                                isfinish =true;
                            }
                            else
                            {
                                isfinish = false;
                            }
                            

                        //    oSaveItem.Itri_seq_no = 0;
                        //    oSaveItem.Itri_line_no = lineNum;
                        //    oSaveItem.Itri_qty = Qty;
                        //    oSaveItem.Itri_unit_price = 0;
                        //    oSaveItem.Itri_app_qty = Qty;
                        //    oSaveItem.Itri_mqty = Qty;
                        //    oSaveItem.Itri_bqty = Qty;
                        //    oSaveItem.Itri_res_qty = Qty;
                        //    oSaveItem.Itri_res_line = 0;
                        //}

                      //  oHeader.InventoryRequestItemList = oSaveitemnew;

                        oHeaders.Add(oHeader);
                    }


                    string err = string.Empty;
                    Int32 result = CHNLSVC.Inventory.Cancel_DispatchPlan(oHeaders, oAppredItems, out err, isfinish);
                    if (result > 0 && isfinish==true)
                    {
                        DisplayMessage("Successfully Cancelled. Document Number : " + err, 3);
                        clear();
                        return;
                    }
                    if (result > 0 && isfinish == false)
                    {
                        DisplayMessage("Successfully Updated Approval Quantity. Document Number : " + err, 3);
                        clear();
                        return;
                    }
                    else if(result==-5)
                    {
                        DisplayMessage(err, 2);
                    }
                    else
                    {
                        DisplayMessage(err, 4);
                    }
                }
            }


        }


        #region Checkboxs
        protected void chkRouteAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRouteAll.Checked)
            {
                txtRoute.Text = "All";
                txtRoute.ReadOnly = true;
            }
            else
            {
                txtRoute.Text = "";
                txtRoute.ReadOnly = false;
            }
        }

        protected void chkMainCategoryAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMainCategoryAll.Checked)
            {
                txtMainCategory.Text = "All";
                txtMainCategory.ReadOnly = true;
            }
            else
            {
                txtMainCategory.Text = "";
                txtMainCategory.ReadOnly = false;
            }
        }

        protected void chkItemCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkItemCode.Checked)
            {
                txtItemCode.Text = "All";
                txtItemCode.ReadOnly = true;
            }
            else
            {
                txtItemCode.Text = "";
                txtItemCode.ReadOnly = false;
            }
        }

        protected void chkSubCategoryAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubCategoryAll.Checked)
            {
                txtSubCategory.Text = "All";
                txtSubCategory.ReadOnly = true;
            }
            else
            {
                txtSubCategory.Text = "";
                txtSubCategory.ReadOnly = false;
            }
        }

        protected void chkModelAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkModelAll.Checked)
            {
                txtModel.Text = "All";
                txtModel.ReadOnly = true;
            }
            else
            {
                txtModel.Text = "";
                txtModel.ReadOnly = false;
            }
        }


        protected void chkAppDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAppDoc.Checked)
            {
                LoadRequestTypes(2);
            }
            else
            {
                LoadRequestTypes(1);
            }
        }

        #endregion

        #region Search

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.DispatchRoute:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtMainCategory.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void btnSearchClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            Session["SY"] = null;
            mpSearch.Hide();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
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
                DisplayMessage(ex.Message);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                if (lblvalue.Text == "DispatchRoute")
                {
                    chkRouteAll.Checked = false;
                    txtRoute.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRoute.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    txtRoute.ReadOnly = false;
                }
                else if (lblvalue.Text == "CAT_Main")
                {
                    chkMainCategoryAll.Checked = false;
                    txtMainCategory.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtMainCategory.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    txtMainCategory.ReadOnly = false;
                }
                else if (lblvalue.Text == "InvoiceItemUnAssable")
                {
                    chkItemCode.Checked = false;
                    txtItemCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemCode.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    txtItemCode.ReadOnly = false;
                }
                else if (lblvalue.Text == "UserLocation")
                {
                    txtLocation.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtLocation.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    txtLocation.ReadOnly = false;
                }
                else if (lblvalue.Text == "CAT_Sub1")
                {
                    chkSubCategoryAll.Checked = false;
                    txtSubCategory.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSubCategory.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    txtSubCategory.ReadOnly = false;
                }
                else if (lblvalue.Text == "Model")
                {
                    chkModelAll.Checked = false;
                    txtModel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtModel.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    txtModel.ReadOnly = false;
                }
                else if (lblvalue.Text == "chgItmcd")
                {
                    string _itmCd = grdResult.SelectedRow.Cells[1].Text;
                    if (_itmChgReqList.Count > 0)
                    {
                        var _chgLine = _itmChgReqList.Where(c => c.Itri_line_no == _chgItmCdLine).FirstOrDefault();
                        if (_chgLine!=null)
                        {
                            string _tmpItmCd = _chgLine.Itri_itm_cd;
                            if (string.IsNullOrEmpty(_chgLine.Itri_old_itm_cd))
                            {
                                _chgLine.Itri_old_itm_cd = _tmpItmCd;
                            }
                            _chgLine.Itri_itm_cd = _itmCd;
                            foreach (GridViewRow _row in dgvItmChg.Rows)
                            {
                                Label lblitri_line_no = _row.FindControl("lblitri_line_no") as Label;
                                LinkButton lbtnitri_itm_cd = _row.FindControl("lbtnitri_itm_cd") as LinkButton;
                                LinkButton lblItri_old_itm_cd = _row.FindControl("lblItri_old_itm_cd") as LinkButton;
                                lbtnitri_itm_cd.Text = _chgLine.Itri_itm_cd;
                                lblItri_old_itm_cd.Text = _chgLine.Itri_old_itm_cd;
                            }
                        }
                    }
                   // popItemChg.Hide();
                    //_showItmChg = false;
                }
                ViewState["SEARCH"] = null;
                Session["SY"] = null;
                mpSearch.Hide();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "DispatchRoute")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                DataTable _result = CHNLSVC.CommonSearch.Search_Routes(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show(); Session["SY"] = "Y";
                return;
            }
            else if (lblvalue.Text == "CAT_Main")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show(); Session["SY"] = "Y";
                return;
            }
            else if (lblvalue.Text == "InvoiceItemUnAssable")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show(); Session["SY"] = "Y";
                return;
            }
            else if (lblvalue.Text == "UserLocation")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show(); Session["SY"] = "Y";
                return;
            }
            else if (lblvalue.Text == "CAT_Sub1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show(); Session["SY"] = "Y";
                return;
            }
            else if (lblvalue.Text == "Model")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                mpSearch.Show(); Session["SY"] = "Y";
                return;
            }
        }

        private void FilterData()
        {
            if (ViewState["SEARCH"] != null)
            {
                if (lblvalue.Text == "DispatchRoute")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                    DataTable result = CHNLSVC.CommonSearch.Search_Routes(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show(); Session["SY"] = "Y";
                }
                else if (lblvalue.Text == "CAT_Main")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show(); Session["SY"] = "Y";
                }
                else if (lblvalue.Text == "InvoiceItemUnAssable")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show(); Session["SY"] = "Y";
                }
                else if (lblvalue.Text == "UserLocation")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show(); Session["SY"] = "Y";
                }
                else if (lblvalue.Text == "CAT_Sub1")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show(); Session["SY"] = "Y";
                }
                else if (lblvalue.Text == "Model")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    DataTable result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show(); Session["SY"] = "Y";
                }
                else if (lblvalue.Text == "chgItmcd")
                {
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforBoqMrn(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearch.Show(); Session["SY"] = "Y";
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

        private void LoadRequestTypes(int value)
        {
            List<ComboBoxObject> oItems = CHNLSVC.Inventory.GET_REQ_TYPES(1, value);
            if (oItems != null && oItems.Count > 0)
            {
                ComboBoxObject oSelect = new ComboBoxObject();
                oSelect.Value = "";
                oSelect.Text = "-- All --";
                oItems.Insert(0, oSelect);

                ddlReqType.DataSource = oItems;
                ddlReqType.DataTextField = "Text";
                ddlReqType.DataValueField = "Value";
                ddlReqType.DataBind();
            }
        }

        private void DisplayMessage(String Msg, Int32 option)
        {
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

        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        private void SelectHeader(Int32 index, bool selectLower)
        {
            try
            {
                //Clear Current stock details
                dgvCurrentStockDetails.DataSource = new int[] { };
                dgvCurrentStockDetails.DataBind();

                IndexHeader = index;

               // if (chkAppDoc.Checked==false)
               // {
                    for (int i = 0; i < dgvRequestHeader.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvRequestHeader.Rows[i];
                        dr.BackColor = Color.Transparent;
                        CheckBox chkHeadertemp = dr.FindControl("chkHeader") as CheckBox;
                        chkHeadertemp.Checked = false;
                    }
                //}

                GridViewRow drSelect = dgvRequestHeader.Rows[index];
                drSelect.BackColor = Color.LightCyan;

                Label lblITR_SEQ_NO = drSelect.FindControl("lblITR_SEQ_NO") as Label;
                CheckBox chkHeader = drSelect.FindControl("chkHeader") as CheckBox;
                chkHeader.Checked = true;

                if (lblITR_SEQ_NO != null && lblITR_SEQ_NO.Text != "")
                {
                    //if (chkAppDoc.Checked)
                    //{
                    //    if (oRequesItems.Count >0)
                    //    {
                    //        var _CHKiTEM = oRequesItems.Where(x => x.Itri_seq_no == (Convert.ToInt32(lblITR_SEQ_NO.Text))).ToList();
                    //        if (_CHKiTEM.Count == 0)
                    //        {
                    //            List<InventoryRequestItem> _itm = new List<InventoryRequestItem>();
                    //            _itm = CHNLSVC.Inventory.GET_INT_REQ_ITM_BY_SEQ(Convert.ToInt32(lblITR_SEQ_NO.Text));
                    //            if (_itm.Count > 0)
                    //            {
                    //                oRequesItems.AddRange(_itm);
                    //            }
                    //        }
                          
                    //    }
                    //    else
                    //    {
                    //        oRequesItems = CHNLSVC.Inventory.GET_INT_REQ_ITM_BY_SEQ(Convert.ToInt32(lblITR_SEQ_NO.Text));
                    //    }
                    //}
                    //else
                    //{
                        oRequesItems = CHNLSVC.Inventory.GET_INT_REQ_ITM_BY_SEQ(Convert.ToInt32(lblITR_SEQ_NO.Text));
                    //}
                  
                    if (oRequesItems != null && oRequesItems.Count > 0)
                    {
                        if (chkAppDoc.Checked)
                        {


                            oAppredItems = oRequesItems;
                            foreach (InventoryRequestItem _itm in oAppredItems)
                            {
                                _itm.Itri_app_qty = _itm.Itri_bqty;
                            }
                            dgvApprovedItems.DataSource = oAppredItems;
                            dgvApprovedItems.DataBind();
                            dgvApprovedItems.Columns[0].Visible = false;
                         
                            btnSave.Visible = false;
                            btnshow.Visible = false;
                            btnApproveAll.Visible = false;
                     
                        }
                        else
                        {
                            dgvRequestItems.DataSource = oRequesItems;
                            dgvRequestItems.DataBind();
                            ViewState["oRequesItems"] = oRequesItems;
                        }

                    }
                    else
                    {
                        dgvRequestItems.DataSource = new int[] { };
                        dgvRequestItems.DataBind();

                        DisplayMessage("No request details found.");
                        return;
                    }
                }
                if (selectLower)
                {
                    selectItem(0, true);

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
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
        private void selectItem(Int32 index, bool selectLower)
        {
            try
            {
                IndexDetails = index;

                for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                {
                    GridViewRow dr = dgvRequestItems.Rows[i];
                    dr.BackColor = Color.Transparent;
                }

                GridViewRow drSelect = dgvRequestItems.Rows[index];
                drSelect.BackColor = Color.LightCyan;
                Label lblItri_seq_no = drSelect.FindControl("lblItri_seq_no") as Label;
                Label lblItri_line_no = drSelect.FindControl("lblItri_line_no") as Label;
                Label lblItri_itm_cd = drSelect.FindControl("lblItri_itm_cd") as Label;
                Label lblItri_itm_stus = drSelect.FindControl("lblItri_itm_stus") as Label;
                List<InventoryLocation> _tempInvLoc=new List<InventoryLocation>();
                oLocItems = CHNLSVC.Inventory.GET_LOC_ITEMS_FOR_DISPATCH_NEW(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), lblItri_itm_cd.Text.Trim(), null);
                if (oLocItems != null && oLocItems.Count > 0)
                {
                    SetApprovedQty();
                    oLocItems = oLocItems.OrderBy(c => c.Inl_loc).ToList();
                    List<SystemUserLoc> _tmp_secUser = CHNLSVC.General.GET_SEC_USER_LOC_DATA(new SystemUserLoc() {
                        SEL_USR_ID = Session["UserID"].ToString(),
                        SEL_COM_CD = Session["UserCompanyCode"].ToString()                        
                    });
                    if (_tmp_secUser!=null)
                    {
                        if (_tmp_secUser.Count>0)
                        {
                            foreach (var _itemData in oLocItems)
                            {
                                foreach (var _loc in _tmp_secUser)
                                {
                                    if (_loc.SEL_LOC_CD == _itemData.Inl_loc)
                                    {
                                        _tempInvLoc.Add(_itemData);
                                    }
                                }
                            }
                        }
                    }
                    _tempInvLoc = _tempInvLoc.Where(c => c.Inl_qty > 0 || c.Inl_res_qty > 0 || c.Inl_free_qty > 0).ToList();
                    oLocItems = _tempInvLoc;
                    dgvCurrentStockDetails.DataSource = oLocItems;
                    dgvCurrentStockDetails.DataBind();
                }
                else
                {
                    dgvCurrentStockDetails.DataSource = new int[] { };
                    dgvCurrentStockDetails.DataBind();
                    DisplayMessage("There is no stock available in assigned location !");
                    // btnResetHeaders_Click(null, null);

                    return;
                }
                if (selectLower)
                {
                    selectStockItem(0, true);

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void selectStockItem(Int32 index, bool selectLower)
        {
            try
            {
                if (dgvCurrentStockDetails.Rows.Count <= index)
                {
                    return;
                }

                IndexStock = index;

                for (int i = 0; i < dgvCurrentStockDetails.Rows.Count; i++)
                {
                    GridViewRow dr = dgvCurrentStockDetails.Rows[i];
                    dr.BackColor = Color.Transparent;
                }

                GridViewRow drSelect = dgvCurrentStockDetails.Rows[index];
                drSelect.BackColor = Color.LightCyan;

                //txtLocation.Text = IndexHeader.ToString() + " " + IndexDetails.ToString() + " " + IndexStock.ToString();

                txtAmount.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void SetApprovedQty()
        {
            if (oAppredItems != null && oAppredItems.Count > 0 && dgvApprovedItems.Rows.Count > 0)
            {
                foreach (InventoryRequestItem oApprovedItem in oAppredItems)
                {
                    InventoryLocation oLocItem = oLocItems.Find(x => x.Inl_loc == oApprovedItem.Itri_loc && x.Inl_itm_cd == oApprovedItem.Itri_itm_cd && x.Inl_itm_stus == oApprovedItem.Itri_itm_stus);
                    if (oLocItem != null)
                    {
                        oLocItem.Inl_free_qty = oLocItem.Inl_free_qty - oApprovedItem.Itri_app_qty;
                        oLocItem.Inl_res_qty = oLocItem.Inl_res_qty + oApprovedItem.Itri_app_qty;
                    }
                }
            }
        }

        private void SelectHeaderCheckBoxes()
        {
            try
            {
                //Clear Current stock details
                dgvCurrentStockDetails.DataSource = new int[] { };
                dgvCurrentStockDetails.DataBind();

                dgvRequestItems.DataSource = new int[] { };
                dgvRequestItems.DataBind();

                oRequesItems = new List<InventoryRequestItem>();

                for (int i = 0; i < dgvRequestHeader.Rows.Count; i++)
                {

                    GridViewRow dr = dgvRequestHeader.Rows[i];
                    dr.BackColor = Color.Transparent;

                    CheckBox cheHeader = dr.FindControl("chkHeader") as CheckBox;
                    if (cheHeader.Checked)
                    {
                        dr.BackColor = Color.LightCyan;

                        Label lblITR_SEQ_NO = dr.FindControl("lblITR_SEQ_NO") as Label;

                        if (lblITR_SEQ_NO != null && lblITR_SEQ_NO.Text != "")
                        {
                            oRequesItems.AddRange(CHNLSVC.Inventory.GET_INT_REQ_ITM_BY_SEQ(Convert.ToInt32(lblITR_SEQ_NO.Text)));
                            if (oRequesItems != null && oRequesItems.Count > 0)
                            {

                                if (chkAppDoc.Checked)
                                {
                                    oAppredItems = oRequesItems;
                                    foreach (InventoryRequestItem _itm in oAppredItems)
                                    {
                                        _itm.Itri_app_qty = _itm.Itri_bqty;
                                    }
                                    dgvApprovedItems.DataSource = oAppredItems;
                                    dgvApprovedItems.DataBind();
                                    dgvApprovedItems.Columns[0].Visible = false;

                                    btnSave.Visible = false;
                                    btnshow.Visible = false;
                                    btnApproveAll.Visible = false;

                                }
                                else
                                {
                                    oRequesItems.RemoveAll(r => r.Itri_bqty == 0);

                                    dgvRequestItems.DataSource = oRequesItems;
                                    dgvRequestItems.DataBind();
                                }

                                ViewState["oRequesItems"] = oRequesItems;
                            }
                            else
                            {
                                dgvRequestItems.DataSource = new int[] { };
                                dgvRequestItems.DataBind();

                                DisplayMessage("No request details found.");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void selectItemCheckBoxes()
        {
            try
            {
                dgvCurrentStockDetails.DataSource = new int[] { };
                dgvCurrentStockDetails.DataBind();

                oLocItems = new List<InventoryLocation>();
                for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                {
                    GridViewRow dr = dgvRequestItems.Rows[i];
                    dr.BackColor = Color.Transparent;

                    CheckBox cheHeader = dr.FindControl("chkDetail") as CheckBox;
                    if (cheHeader.Checked)
                    {
                        GridViewRow drSelect = dgvRequestItems.Rows[i];
                        drSelect.BackColor = Color.LightCyan;
                        Label lblItri_seq_no = drSelect.FindControl("lblItri_seq_no") as Label;
                        Label lblItri_line_no = drSelect.FindControl("lblItri_line_no") as Label;
                        Label lblItri_itm_cd = drSelect.FindControl("lblItri_itm_cd") as Label;
                        Label lblItri_itm_stus = drSelect.FindControl("lblItri_itm_stus") as Label;

                        oLocItems.AddRange(CHNLSVC.Inventory.GET_LOC_ITEMS_FOR_DISPATCH(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), lblItri_itm_cd.Text.Trim(), lblItri_itm_stus.Text.Trim()));
                        if (oLocItems != null && oLocItems.Count > 0)
                        {
                            SetApprovedQty();
                            oLocItems = oLocItems.OrderBy(c => c.Inl_loc).ToList();
                            dgvCurrentStockDetails.DataSource = oLocItems;
                            dgvCurrentStockDetails.DataBind();
                        }
                        else
                        {
                            dgvCurrentStockDetails.DataSource = new int[] { };
                            dgvCurrentStockDetails.DataBind();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private bool isAnyDetailsSelected()
        {
            bool result = false;

            for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
            {
                GridViewRow dr = dgvRequestItems.Rows[i];
                CheckBox chkDetail = dr.FindControl("chkDetail") as CheckBox;
                if (chkDetail.Checked)
                {
                    result = true;
                    break;
                }

            }

            return result;
        }

        private string setDocumentType(string DocType)
        {
            string AppDocType = string.Empty;
            switch (DocType)
            {
                case "REQD":
                    AppDocType = "MRNA";
                    break;
                case "CONSD":
                    AppDocType = "CONSA";
                    break;
                case "SO":
                    AppDocType = "SOA";
                    break;
                default:
                    AppDocType = "MRNA";
                    break;
            }

            return AppDocType;
        }

        private string setDocumentTypeForCancel(string DocType)
        {
            string AppDocType = string.Empty;
            switch (DocType)
            {
                case "MRNA":
                    AppDocType = "MRNC";
                    break;
                case "CONSA":
                    AppDocType = "CONSC";
                    break;
                case "SOA":
                    AppDocType = "SOC";
                    break;
                default:
                    AppDocType = "MRNC";
                    break;
            }

            return AppDocType;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------
        protected void btnRoute_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
            DataTable result = CHNLSVC.CommonSearch.Search_Routes(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "DispatchRoute";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearch.Show(); Session["SY"] = "Y";
        }

        protected void btnMainCategory_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "CAT_Main";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearch.Show(); Session["SY"] = "Y";
        }

        protected void btnItemCode_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
            DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "InvoiceItemUnAssable";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearch.Show(); Session["SY"] = "Y";
        }

        protected void btnLocation_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "UserLocation";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearch.Show(); Session["SY"] = "Y";
        }

        protected void btnSubCategory_Click(object sender, EventArgs e)
        {
            if (txtMainCategory.Text == string.Empty || txtMainCategory.Text.ToUpper() == "ALL")
            {
                DisplayMessage("Please select the main category");
                return;
            }

            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "CAT_Sub1";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearch.Show(); Session["SY"] = "Y";
        }

        protected void btnModel_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Model";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearch.Show(); Session["SY"] = "Y";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            _dataSaveProcess = false;
            oRequestHeaders = new List<InventoryRequest>();
            oLocItems = new List<InventoryLocation>();
            oRequesItems = new List<InventoryRequestItem>();

            oAppredItems = new List<InventoryRequestItem>();

            IndexHeader = -1;
            IndexDetails = -1;
            IndexStock = -1;

            dgvRequestHeader.DataSource = new int[] { };
            dgvRequestHeader.DataBind();

            dgvCurrentStockDetails.DataSource = new int[] { };
            dgvCurrentStockDetails.DataBind();

            dgvRequestItems.DataSource = new int[] { };
            dgvRequestItems.DataBind();

            dgvApprovedItems.DataSource = new int[] { };
            dgvApprovedItems.DataBind();

            dgvRequestHeader.Enabled = true;

            String Route = (txtRoute.Text.Trim().ToUpper() == "ALL") ? null : txtRoute.Text.Trim();
            String MainCate = (txtMainCategory.Text.Trim().ToUpper() == "ALL") ? string.Empty : txtMainCategory.Text.Trim();
            String Item = (txtItemCode.Text.Trim().ToUpper() == "ALL") ? string.Empty : txtItemCode.Text.Trim();
            String SubCate = (txtSubCategory.Text.Trim().ToUpper() == "ALL") ? string.Empty : txtSubCategory.Text.Trim();
            String Model = (txtModel.Text.Trim().ToUpper() == "ALL") ? string.Empty : txtModel.Text.Trim();
            int type = 1;
            if (chkAppDoc.Checked) { type = 2; }
            double validDateRange = 0;
            bool _chkValidDateRange = false;
            
            if (ddlReqType.SelectedValue=="")
            {
                HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISPSERCHA", DateTime.Now.Date);
                if (_sysPara != null)
                {
                    if (_sysPara.Hsy_val != 0)
                    {
                        _chkValidDateRange = true;
                        validDateRange = (double)_sysPara.Hsy_val;
                    }
                }
            }
            else
            {
                HpSystemParameters _sysPara = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DISPSERCH", DateTime.Now.Date);
                if (_sysPara != null)
                {
                    if (_sysPara.Hsy_val != 0)
                    {
                        _chkValidDateRange = true;
                        validDateRange = (double)_sysPara.Hsy_val;
                    }
                }
            }
            if (radCreateDate.Checked)
            {
                DateTime _dtFrom = Convert.ToDateTime(txtDate.Text);
                DateTime _dtTo = Convert.ToDateTime(txtToDate.Text);
                if (_chkValidDateRange)
                {
                    TimeSpan timeSpan = _dtTo - _dtFrom;
                    double NrOfDays = timeSpan.TotalDays;
                    if (NrOfDays > validDateRange)
                    {
                        DisplayMessage("Maximum date range allowed is " + validDateRange + " days !", 2); return;
                    }
                }
                oRequestHeaders = CHNLSVC.Inventory.GET_REQUEST_FOR_DISPATCHNEW(Session["UserCompanyCode"].ToString(), Route, MainCate, Item, ddlReqType.SelectedValue.ToString(), txtLocation.Text.Trim(), SubCate, Model, Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtToDate.Text.Trim()), type);

                if (oRequestHeaders.Count > 0)
                {
                    foreach (var oRequestHeaderslist in oRequestHeaders)
                    {
                        if (oRequestHeaderslist.Itr_req_no != "AD-HOC")
                        {
                            InventoryRequest _obj = new InventoryRequest();
                            _obj.Itr_req_no = oRequestHeaderslist.Itr_ref.ToString();
                            InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(_obj).FirstOrDefault();
                            if (_reqData != null)
                            {
                                if (_reqData.Itr_tp == "SO")
                                {
                                    SalesOrderHeader _soHdr = CHNLSVC.Inventory.GET_SAO_HDR_DATA(_reqData.Itr_ref);
                                    if (_soHdr != null)
                                    {
                                        oRequestHeaderslist.Itr_mod_dt = _soHdr.SOH_MOD_WHEN;
                                    }
                                }
                                else
                                {
                                    oRequestHeaderslist.Itr_mod_dt = oRequestHeaderslist.Itr_cre_dt;
                                }
                                oRequestHeaderslist.Itr_cre_dt = _reqData.Itr_cre_dt;
                                oRequestHeaderslist.tmpapprovedate_show = 1;
                            }
                        }
                        if (oRequestHeaderslist.Itr_ref == "AD-HOC")
                        {
                            InventoryRequest _obj = new InventoryRequest();
                            _obj.Itr_req_no = oRequestHeaderslist.Itr_req_no.ToString();
                            InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(_obj).FirstOrDefault();
                            if (oRequestHeaderslist.Itr_tp == "REQD")
                            {
                                oRequestHeaderslist.Itr_mod_dt = _reqData.Itr_cre_dt;
                            }
                        }
                    }
                }
            }
            else
            {
                DateTime _dtTmpFrom = Convert.ToDateTime(txtDate.Text);
                DateTime _dtFrom = Convert.ToDateTime(_dtTmpFrom.ToShortDateString());
                DateTime _dtTmpTo = Convert.ToDateTime(txtToDate.Text);
                DateTime _dtTo = Convert.ToDateTime(_dtTmpTo.ToShortDateString());
                if (_chkValidDateRange)
                {
                    TimeSpan timeSpan = _dtTo - _dtFrom;
                    double NrOfDays = timeSpan.TotalDays;
                    if (NrOfDays > validDateRange)
                    {
                        DisplayMessage("Maximum date range allowed is " + validDateRange + " days !", 2); return;
                    }
                }
                oRequestHeaders = CHNLSVC.Inventory.GET_REQUEST_FOR_DISPATCH_EXP_DT(Session["UserCompanyCode"].ToString(), Route, MainCate, Item, ddlReqType.SelectedValue.ToString(),
                    txtLocation.Text.Trim(), SubCate, Model, _dtFrom
                    , _dtTo, type);
                //if (oRequestHeaders.Count > 0)
                //{
                //    foreach (var oRequestHeaderslist in oRequestHeaders)
                //    {
                //        if (oRequestHeaderslist.Itr_req_no != "AD-HOC")
                //        {
                //            InventoryRequest _obj = new InventoryRequest();
                //            _obj.Itr_req_no = oRequestHeaderslist.Itr_ref.ToString();
                //            InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(_obj).FirstOrDefault();
                //            if (_reqData != null)
                //            {
                //                if (_reqData.Itr_tp=="SO")
                //                {
                //                    SalesOrderHeader _soHdr = CHNLSVC.Inventory.GET_SAO_HDR_DATA(_reqData.Itr_ref);
                //                    if (_soHdr != null)
                //                    {
                //                        oRequestHeaderslist.Itr_mod_dt = _soHdr.SOH_MOD_WHEN;
                //                    }
                //                }
                //                else
                //                {
                //                    oRequestHeaderslist.Itr_mod_dt = oRequestHeaderslist.Itr_cre_dt;
                //                }
                //                oRequestHeaderslist.Itr_cre_dt = _reqData.Itr_cre_dt;
                //                oRequestHeaderslist.tmpapprovedate_show = 1;
                //            }
                //        }
                //        if (oRequestHeaderslist.Itr_ref == "AD-HOC")
                //        {
                //            InventoryRequest _obj = new InventoryRequest();
                //            _obj.Itr_req_no = oRequestHeaderslist.Itr_req_no.ToString();
                //            InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(_obj).FirstOrDefault();
                //            if (oRequestHeaderslist.Itr_tp == "REQD")
                //            {
                //                oRequestHeaderslist.Itr_mod_dt = _reqData.Itr_cre_dt;
                //            }
                //        }
                //    }
                //}

            }
           // oRequestHeaders = oRequestHeaders.Where(a=>a.);

            if (oRequestHeaders != null && oRequestHeaders.Count > 0)
            {
                dgvRequestHeader.DataSource = oRequestHeaders;
                dgvRequestHeader.DataBind();
                Session["dataTable"] = oRequestHeaders;
                chkAppDoc.Enabled = false;

                //if (Session["UserCompanyCode"].ToString() == "AAL")
                //{
                    if (ddlReqType.SelectedValue.ToString() == "SO")
                    {
                        int rowcount = 0;

                        foreach (var _hreq in oRequestHeaders)
                        {

                            int a = _hreq.Itr_seq_no;
                            oRequesItems = CHNLSVC.Inventory.GET_INT_REQ_ITM_BY_SEQ(Convert.ToInt32(a));
                            foreach (var _loc in oRequesItems)
                            {
                                if (_loc.Itri_bqty == 0)
                                {
                                    if (_loc.Itri_bqty != _loc.Itri_app_qty)
                                    {
                                        selectBqtyItem(rowcount, true);
                                    }
                                }
                            }
                            rowcount++;
                        }
                    }
                //}

            }
            else
            {
                DisplayMessage("No Records Found.");
                dgvRequestHeader.DataSource = new int[] { };
                dgvRequestHeader.DataBind();
               
            }
        }
        //dilshan on 16/03/2018
        private void selectBqtyItem(Int32 index, bool selectLower)
        {
            try
            {
                if (dgvRequestHeader.Rows.Count <= index)
                {
                    return;
                }

                IndexStock = index;

                //for (int i = 0; i < dgvRequestHeader.Rows.Count; i++)
                //{
                //    GridViewRow dr = dgvRequestHeader.Rows[i];
                //    dr.BackColor = Color.Transparent;
                //}

                GridViewRow drSelect = dgvRequestHeader.Rows[index];
                drSelect.BackColor = Color.LightGreen;

                //txtLocation.Text = IndexHeader.ToString() + " " + IndexDetails.ToString() + " " + IndexStock.ToString();

                txtAmount.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        //*********************

        protected void dgvRequestHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvRequestHeader.PageIndex = e.NewPageIndex;
            dgvRequestHeader.DataSource = oRequestHeaders;
            Session["dataTable"] = oRequestHeaders;
            dgvRequestHeader.DataBind();
        }

        protected void dgvRequestHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void SelectRequest_Click(object sender, EventArgs e)
        {
            
            oAppredItems = new List<InventoryRequestItem>();
            dgvApprovedItems.DataSource = new int[]{};
            dgvApprovedItems.DataBind();
            GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
            #region supplier valdation add by lakshan 30 Jun 2017
            Label lblITR_REQ_NO = drSelect.FindControl("lblITR_REQ_NO") as Label;
            CheckBox chkHeader = drSelect.FindControl("chkHeader") as CheckBox;
            InventoryRequest _invreqSup = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = lblITR_REQ_NO.Text }).FirstOrDefault();
            if (_invreqSup != null)
            {
                if (_invreqSup.Itr_tp == "SO")
                {
                    MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileByCom(_invreqSup.Itr_bus_code, null, null, null, null,
                        Session["UserCompanyCode"].ToString());
                    if (_mstBusEntity != null)
                    {
                        if (_mstBusEntity.Mbe_is_suspend == true)
                        {
                            DispMsg("This customer already suspended !"); return;
                        }
                    }
                }
            }
            #endregion
            if (chkAppDoc.Checked)
            {

                Label reno = drSelect.FindControl("lblITR_REQ_NO") as Label;
                InventoryRequest oHeaders = new InventoryRequest();
                oHeaders.Itr_req_no = reno.Text;
                oHeaders.Itr_cre_by = Session["UserID"].ToString();
                oHeaders.Itr_com = Session["UserCompanyCode"].ToString();
                Session["oHeaders"] = oHeaders;
            }
            //Label renoo = drSelect.FindControl("lblITR_REQ_NO") as Label;
            //if (chkAppDoc.Checked == false)
            //{
            //    UpdateUserforReq(renoo.Text.ToString());
            //}

            SelectHeader(drSelect.RowIndex, true);
            checkAllcancel();




            //dgvRequestHeader.Enabled = false;
        }
        private void checkAllcancel()
        {
           // CheckBox chkHeaderAllApp = dgvApprovedItems.HeaderRow.FindControl("chkHeaderAllApp") as CheckBox;
           //  chkHeaderAllApp.Checked = true;
            bool deefault = true;
            foreach (GridViewRow item in dgvApprovedItems.Rows)
            {
              //  TextBox lblitri_app_qty = item.FindControl("lblitri_app_qty") as TextBox;
                Label lblitri_app_qty = item.FindControl("lblitri_app_qty") as Label;

                CheckBox ChkselectItm = item.FindControl("ChkselectItm") as CheckBox;
                if (deefault=true && lblitri_app_qty.Text.ToString() != "0")
                {
                    ChkselectItm.Checked = true;
                }
                else
                {
                    ChkselectItm.Checked = false;
                }
            }
        }
        protected void dgvRequestItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void dgvRequestItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void SelectReqItem_Click(object sender, EventArgs e)
        {
            GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
            selectItem(drSelect.RowIndex, true);
        }

        protected void SelectStickItem_Click(object sender, EventArgs e)
        {
            GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
            selectStockItem(drSelect.RowIndex, true);
        }

        protected void dgvCurrentStockDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCurrentStockDetails.PageIndex = e.NewPageIndex;
            oLocItems = oLocItems.OrderBy(c => c.Inl_loc).ToList();
            dgvCurrentStockDetails.DataSource = oLocItems;
            dgvCurrentStockDetails.DataBind();
        }

        protected void btnAddQty_Click(object sender, EventArgs e)
        {
            try
            {
                //InventoryRequestItem> oAppredItems
                if (hdfISValiedNumber.Value == "0")
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtAmount.Text))
                {
                    DisplayMessage("Please enter valid quantity.");
                    return;
                }
                decimal tmpQty = 0, addQty=0;
                addQty = decimal.TryParse(txtAmount.Text,out tmpQty) ? Convert.ToDecimal(txtAmount.Text) : 0;
                if (addQty==0)
                {
                    DispMsg("Please enter valid quantity.");
                    return;
                }

                if (IndexHeader != -1 && IndexDetails != -1 && IndexStock != -1)
                {
                    GridViewRow drHeader = dgvRequestHeader.Rows[IndexHeader];
                    GridViewRow drDetail = dgvRequestItems.Rows[IndexDetails];
                    GridViewRow drStock = dgvCurrentStockDetails.Rows[IndexStock];

                    Label lblITR_REQ_NO = drHeader.FindControl("lblITR_REQ_NO") as Label;

                    //Current Stock grid
                    Label lblINL_COM = drStock.FindControl("lblINL_COM") as Label;
                    Label lblINL_LOC = drStock.FindControl("lblINL_LOC") as Label;
                    Label lblINL_ITM_CD = drStock.FindControl("lblINL_ITM_CD") as Label;
                    Label lblINL_ITM_STUS = drStock.FindControl("lblINL_ITM_STUS") as Label;
                    Label lblMIS_DESC = drStock.FindControl("lblMIS_DESC") as Label;
                    Label lblINL_QTY = drStock.FindControl("lblINL_QTY") as Label;
                    Label lblinl_res_qty = drStock.FindControl("lblinl_res_qty") as Label;
                    Label lblINL_FREE_QTY = drStock.FindControl("lblINL_FREE_QTY") as Label;

                    // Request Details grid
                    Label lblItri_app_qty = drDetail.FindControl("lblItri_app_qty") as Label;
                    Label lblItri_bqty = drDetail.FindControl("lblItri_bqty") as Label;
                    Label lblItri_seq_no = drDetail.FindControl("lblItri_seq_no") as Label;
                    Label lblItri_line_no = drDetail.FindControl("lblItri_line_no") as Label;
                    Label lblItri_res_no = drDetail.FindControl("lblItri_res_no") as Label;

                    Int32 DetailSeq = Convert.ToInt32(lblItri_seq_no.Text);
                    Int32 DetailLine = Convert.ToInt32(lblItri_line_no.Text);


                    decimal QtyNewAmount = Convert.ToDecimal(txtAmount.Text);

                    decimal QtyApp = (lblItri_app_qty.Text == "") ? 0 : Convert.ToDecimal(lblItri_app_qty.Text);
                    decimal QtyBal = (lblItri_bqty.Text == "") ? 0 : Convert.ToDecimal(lblItri_bqty.Text);
                    decimal QtyFreeStock = (lblINL_FREE_QTY.Text == "") ? 0 : Convert.ToDecimal(lblINL_FREE_QTY.Text);
                    decimal QtyResve = (lblinl_res_qty.Text == "") ? 0 : Convert.ToDecimal(lblinl_res_qty.Text);

                    if (QtyApp == 0)
                    {
                        DisplayMessage("You have not selected any approved quantity ");
                        return;
                    }
                    if (QtyBal == 0)
                    {
                        DisplayMessage("You dont have balance quantity");
                        return;
                    }
                    if ((lblItri_res_no.Text != "") && (lblItri_res_no.Text != "N/A"))
                    {
                        if (QtyResve < QtyNewAmount)
                        {
                            DisplayMessage("Selected Item is reserved.");
                            txtAmount.Text = QtyBal.ToString();
                            txtAmount.Focus();
                            return;
                        }
                        //check res items
                      //  DataTable chkqty = CHNLSVC.Inventory.SP_GETRESQTYMRN(lblINL_COM.Text.ToString(),lblINL_LOC.Text.ToString(),lblINL_ITM_CD.Text.ToString(),lblINL_ITM_STUS.Text.ToString());
                    }
                    else
                    {
                        if (QtyFreeStock <= 0)
                        {
                            DisplayMessage("You dont have free quantity in " + lblINL_LOC.Text + " location");
                            return;
                        }
                        if (QtyFreeStock < QtyNewAmount)
                        {
                            //DisplayMessage("Location : " + lblINL_LOC.Text + " dont have " + QtyNewAmount.ToString("N2"));
                            DisplayMessage("Please check the inventory balance ");
                            txtAmount.Text = QtyFreeStock.ToString();
                            txtAmount.Focus();
                            return;
                        }
                        if (QtyBal < QtyNewAmount)
                        {
                            DisplayMessage("Cant exceed balance Qty");
                            txtAmount.Text = QtyBal.ToString();
                            txtAmount.Focus();
                            return;
                        }
                    }
                   


                    if (oAppredItems.FindAll(x =>x.Itri_line_no==DetailLine && x.Itri_loc == lblINL_LOC.Text.Trim() && x.Itri_itm_cd == lblINL_ITM_CD.Text && x.Itri_itm_stus == lblINL_ITM_STUS.Text.Trim()).Count > 0)
                    {
                        InventoryRequestItem oOldItem = oAppredItems.Find(x => x.Itri_loc == lblINL_LOC.Text.Trim() && x.Itri_itm_cd == lblINL_ITM_CD.Text && x.Itri_itm_stus == lblINL_ITM_STUS.Text.Trim());
                        oOldItem.Itri_bqty = oOldItem.Itri_bqty + QtyNewAmount;
                        oOldItem.Itri_qty = oOldItem.Itri_qty + QtyNewAmount;
                        oOldItem.Itri_app_qty = oOldItem.Itri_app_qty + QtyNewAmount;
                    }
                    else
                    {
                        InventoryRequestItem oNewItem = new InventoryRequestItem();
                        oNewItem.Itri_loc = lblINL_LOC.Text.Trim();
                        oNewItem.Itri_itm_cd = lblINL_ITM_CD.Text.Trim();
                        oNewItem.Itri_itm_stus = lblINL_ITM_STUS.Text.Trim();
                        oNewItem.Mis_desc = lblMIS_DESC.Text.Trim();
                        oNewItem.Itri_bqty = QtyNewAmount;
                        oNewItem.Itri_qty = QtyNewAmount;
                        oNewItem.Itri_app_qty = QtyNewAmount;
                        oNewItem.Itri_res_no = lblItri_res_no.Text;
                        oNewItem.Itr_req_no = lblITR_REQ_NO.Text;
                        oNewItem.Itri_base_req_line = DetailLine;
                        oAppredItems.Add(oNewItem);
                    }

                    dgvApprovedItems.DataSource = oAppredItems;
                    dgvApprovedItems.DataBind();

                    InventoryRequestItem oSelectDetail = oRequesItems.Find(x => x.Itri_seq_no == DetailSeq && x.Itri_line_no == DetailLine);
                    oSelectDetail.Itri_bqty = oSelectDetail.Itri_bqty - QtyNewAmount;

                    dgvRequestItems.DataSource = oRequesItems;
                    dgvRequestItems.DataBind();

                    for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvRequestItems.Rows[i];
                        dr.BackColor = Color.Transparent;
                    }

                    GridViewRow drSelect = dgvRequestItems.Rows[IndexDetails];
                    drSelect.BackColor = Color.LightCyan;

                    txtAmount.Text = "";
                    txtAmount.Focus();

                    //InventoryLocation oSelectedStock = oLocItems.Find(x => x.Inl_com == lblINL_COM.Text && x.Inl_loc == lblINL_LOC.Text && x.Inl_itm_cd == lblINL_ITM_CD.Text);
                    //Add by Lakshan 02 Nov 2016 current stock grid data update incorrecly
                    InventoryLocation oSelectedStock = oLocItems.Find(x => x.Inl_com == lblINL_COM.Text && x.Inl_loc == lblINL_LOC.Text && x.Inl_itm_cd == lblINL_ITM_CD.Text
                      && x.Inl_itm_stus == lblINL_ITM_STUS.Text);
                    if ((lblItri_res_no.Text != "") && (lblItri_res_no.Text != "N/A"))
                    {
                        //oSelectedStock.Inl_res_qty = oSelectedStock.Inl_res_qty - QtyNewAmount;
                        Session["ISRESNO"] = "TRUE";
                    }
                    else
                    {
                        oSelectedStock.Inl_res_qty = oSelectedStock.Inl_res_qty + QtyNewAmount;
                        //oSelectedStock.Inl_res_qty = oSelectedStock.Inl_res_qty - QtyNewAmount;

                        oSelectedStock.Inl_free_qty = oSelectedStock.Inl_free_qty - QtyNewAmount;
                        //oSelectedStock.Inl_free_qty = oSelectedStock.Inl_free_qty + QtyNewAmount;
                    }

                   

                    oLocItems = oLocItems.OrderBy(c => c.Inl_loc).ToList();

                    dgvCurrentStockDetails.DataSource = oLocItems;
                    dgvCurrentStockDetails.DataBind();

                    for (int i = 0; i < dgvCurrentStockDetails.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvCurrentStockDetails.Rows[i];
                        dr.BackColor = Color.Transparent;
                    }

                    drSelect = dgvCurrentStockDetails.Rows[IndexStock];
                    drSelect.BackColor = Color.LightCyan;

                    if (oSelectedStock.Inl_free_qty == 0 && oSelectDetail.Itri_bqty > 0)
                    {
                        IndexStock = IndexStock + 1;
                        if (dgvCurrentStockDetails.Rows.Count == IndexStock + 1)
                        {
                        }
                        else
                        {
                            selectStockItem(IndexStock, true);
                        }
                    }

                    if (oSelectDetail.Itri_bqty == 0)
                    {
                        IndexDetails = IndexDetails + 1;
                        if (dgvRequestItems.Rows.Count == IndexDetails)
                        {
                            //Selected Request is complete
                           // DisplayMessage("All items are approved");
                            DispMsg("All items are approved !", "N");
                        }
                        else
                        {
                            selectItem(IndexDetails, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnDelAppItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdfDelAppr.Value == "1")
                {
                    GridViewRow drDelect = (sender as LinkButton).NamingContainer as GridViewRow;
                    Label lblitri_loc = drDelect.FindControl("lblitri_loc") as Label;
                    Label lblitri_itm_cd = drDelect.FindControl("lblitri_itm_cd") as Label;
                    Label lblitri_itm_stus = drDelect.FindControl("lblitri_itm_stus") as Label;
                    Label lblitri_app_qty = drDelect.FindControl("lblitri_app_qty") as Label;
                    Label lblItri_base_req_line = drDelect.FindControl("lblItri_base_req_line") as Label;

                    InventoryRequestItem oAppredItem = oAppredItems.Find(x => x.Itri_base_req_line == Convert.ToInt32(lblItri_base_req_line.Text) && x.Itri_loc == lblitri_loc.Text && x.Itri_itm_cd == lblitri_itm_cd.Text && x.Itri_itm_stus == lblitri_itm_stus.Text);
                    decimal QtyDel = oAppredItem.Itri_app_qty;
                    InventoryRequestItem oRequesItem = oRequesItems.Find(x => x.Itri_line_no == Convert.ToInt32(lblItri_base_req_line.Text) && x.Itri_itm_cd == lblitri_itm_cd.Text && x.Itri_itm_stus == lblitri_itm_stus.Text);
                    oRequesItem.Itri_bqty = oRequesItem.Itri_bqty + QtyDel;

                    dgvRequestItems.DataSource = oRequesItems;
                    dgvRequestItems.DataBind();

                    IndexDetails = IndexDetails - 1;
                    IndexStock = -1;

                    for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvRequestItems.Rows[i];
                        dr.BackColor = Color.Transparent;
                    }

                   // GridViewRow drSelect = dgvRequestItems.Rows[IndexDetails];
                  //  drSelect.BackColor = Color.LightCyan;

                    InventoryLocation oLocItem = oLocItems.Find(x => x.Inl_itm_cd == lblitri_itm_cd.Text && x.Inl_loc == lblitri_loc.Text && x.Inl_itm_stus == lblitri_itm_stus.Text);
                    if (oLocItem != null && Session["ISRESNO"].ToString() !="TRUE")
                    {
                        oLocItem.Inl_free_qty = oLocItem.Inl_free_qty + QtyDel;
                        oLocItem.Inl_res_qty = oLocItem.Inl_res_qty + QtyDel;
                    }
                    oLocItems = oLocItems.OrderBy(c => c.Inl_loc).ToList();
                    dgvCurrentStockDetails.DataSource = oLocItems;
                    dgvCurrentStockDetails.DataBind();
                    Session["ISRESNO"] = "";
                    for (int i = 0; i < dgvCurrentStockDetails.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvCurrentStockDetails.Rows[i];
                        dr.BackColor = Color.Transparent;
                    }

                    oAppredItems.Remove(oAppredItem);

                    if (oAppredItems.Count > 0)
                    {
                        dgvApprovedItems.DataSource = oAppredItems;
                        dgvApprovedItems.DataBind();
                    }
                    else
                    {
                        dgvApprovedItems.DataSource = new int[] { };
                        dgvApprovedItems.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void dgvApprovedItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnResetHeaders_Click(object sender, EventArgs e)
        {
            oLocItems = new List<InventoryLocation>();
            oRequesItems = new List<InventoryRequestItem>();
            oAppredItems = new List<InventoryRequestItem>();

            IndexDetails = -1;
            IndexStock = -1;

            dgvCurrentStockDetails.DataSource = new int[] { };
            dgvCurrentStockDetails.DataBind();

            dgvRequestItems.DataSource = new int[] { };
            dgvRequestItems.DataBind();

            dgvApprovedItems.DataSource = new int[] { };
            dgvApprovedItems.DataBind();

            dgvRequestHeader.Enabled = true;
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {

            // SelectHeaderCheckBoxes();

            GridViewRow drSelect = (sender as CheckBox).NamingContainer as GridViewRow;
            if (chkAppDoc.Checked)
            {

                Label reno = drSelect.FindControl("lblITR_REQ_NO") as Label;
                InventoryRequest oHeaders = new InventoryRequest();
                oHeaders.Itr_req_no = reno.Text;
                oHeaders.Itr_cre_by = Session["UserID"].ToString();
                oHeaders.Itr_com = Session["UserCompanyCode"].ToString();
                Session["oHeaders"] = oHeaders;
            }
            //Label renoo = drSelect.FindControl("lblITR_REQ_NO") as Label;

            //if (chkAppDoc.Checked == false)
            //{
            //    UpdateUserforReq(renoo.Text.ToString());
            //}
           
            SelectHeader(drSelect.RowIndex, true);
            checkAllcancel();
            //GridViewRow dr = (sender as CheckBox).NamingContainer as GridViewRow;
            //CheckBox chkHeader = dr.FindControl("chkHeader") as CheckBox;
            //if (chkHeader.Checked)
            //{
            //}
        }
        private void UpdateUserforReq(string reqno)
        {
            string com = Session["UserCompanyCode"].ToString();
            string user = Session["UserID"].ToString();

            //check alredy user
            DataTable userdata = CHNLSVC.Inventory.SP_GETUPDATEUSER(com,reqno);
            if (userdata.Rows.Count>0)
            {
             string userup=   userdata.Rows[0][0].ToString();
             if (userup == user | userup=="")
                {
                    int effect = CHNLSVC.Inventory.UpdateReqUser(com,reqno,user);
                }
             else
             {
                 DisplayMessage("Request is being processed by another user !", 1);
             }
            }
        }
        private void UpdateUserforReqempy(string reqno)
        {
            string com = Session["UserCompanyCode"].ToString();
            string user = Session["UserID"].ToString();

            //check alredy user
            DataTable userdata = CHNLSVC.Inventory.SP_GETUPDATEUSER(com, reqno);
            if (userdata.Rows.Count > 0)
            {
                string userup = userdata.Rows[0][0].ToString();
                if (userup == user)
                {
                    int effect = CHNLSVC.Inventory.UpdateReqUser(com, reqno, "");
                }
                else
                {
                    DisplayMessage("Already use  this req no onother user", 1);
                }
            }


        }
        protected void chkHeaderAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkHeaderAll = (CheckBox)dgvRequestHeader.HeaderRow.FindControl("chkHeaderAll");
            if (chkHeaderAll.Checked)
            {
                if (dgvRequestHeader.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvRequestHeader.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvRequestHeader.Rows[i];
                        CheckBox chkHeader = dr.FindControl("chkHeader") as CheckBox;
                        chkHeader.Checked = true;
                    }
                }

                SelectHeaderCheckBoxes();
            }
            else
            {
                dgvRequestItems.DataSource = new int[] { };
                dgvRequestItems.DataBind();

                if (dgvRequestHeader.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvRequestHeader.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvRequestHeader.Rows[i];
                        CheckBox chkHeader = dr.FindControl("chkHeader") as CheckBox;
                        chkHeader.Checked = false;

                        dr.BackColor = Color.Transparent;
                    }
                }
            }

        }

        protected void chkDetailsAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkHeaderAll = (CheckBox)dgvRequestItems.HeaderRow.FindControl("chkDetailsAll");
            if (chkHeaderAll.Checked)
            {
                if (dgvRequestItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvRequestItems.Rows[i];
                        CheckBox chkHeader = dr.FindControl("chkDetail") as CheckBox;
                        chkHeader.Checked = true;
                    }
                }

                //selectItemCheckBoxes();
            }
            else
            {
                dgvCurrentStockDetails.DataSource = new int[] { };
                dgvCurrentStockDetails.DataBind();

                if (dgvRequestItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvRequestItems.Rows[i];
                        CheckBox chkHeader = dr.FindControl("chkDetail") as CheckBox;
                        chkHeader.Checked = false;

                        dr.BackColor = Color.Transparent;
                    }
                }
            }
        }

        protected void chkDetail_CheckedChanged(object sender, EventArgs e)
        {
            //selectItemCheckBoxes();
        }

        protected void btnApproveAll_Click(object sender, EventArgs e)
        {
            if (isAnyDetailsSelected())
            {
                for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                {
                    GridViewRow dr = dgvRequestItems.Rows[i];
                    CheckBox chkDetail = dr.FindControl("chkDetail") as CheckBox;
                    if (chkDetail.Checked)
                    {
                        Label lblItri_seq_no = dr.FindControl("lblItri_seq_no") as Label;
                        Label lblItri_line_no = dr.FindControl("lblItri_line_no") as Label;
                        Label lblItri_itm_cd = dr.FindControl("lblItri_itm_cd") as Label;
                        Label lblItri_itm_stus = dr.FindControl("lblItri_itm_stus") as Label;
                        oLocItems.AddRange(CHNLSVC.Inventory.GET_LOC_ITEMS_FOR_DISPATCH(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), lblItri_itm_cd.Text.Trim(), lblItri_itm_stus.Text.Trim()));
                    }
                }

                if (oLocItems.Count > 0)
                {
                    var Locs = oLocItems.Select(x => new { x.Inl_loc, x.Ml_loc_desc }).Distinct().ToList();
                    if (Locs != null)
                    {
                        ddlIsseLocation.DataSource = Locs;
                        ddlIsseLocation.DataTextField = "Ml_loc_desc";
                        ddlIsseLocation.DataValueField = "Inl_loc";
                        ddlIsseLocation.DataBind();
                    }

                    var ItemStatus = oLocItems.Select(x => new { x.Inl_itm_stus, x.Mis_desc }).Distinct().ToList();
                    if (ItemStatus != null)
                    {
                        ddlIssueItemStatus.DataSource = ItemStatus;
                        ddlIssueItemStatus.DataTextField = "Mis_desc";
                        ddlIssueItemStatus.DataValueField = "Inl_itm_stus";
                        ddlIssueItemStatus.DataBind();
                    }
                }

                mpApproveAll.Show();
              
            }
            else
            {
                DisplayMessage("Please select the requested items to approve", 1);
                return;
            }
        }

        private void ApproveAll()
        {
            oAppredItems = ViewState["oRequesItems"] as List<InventoryRequestItem>;
            List<InventoryRequest> oHeaders = new List<InventoryRequest>();
            List<TmpValidation> _errList = new List<TmpValidation>();
            for (int hi = 0; hi < dgvRequestHeader.Rows.Count; hi++)
            {
                GridViewRow drHeader = dgvRequestHeader.Rows[hi];
                CheckBox chkHeadertemp = drHeader.FindControl("chkHeader") as CheckBox;
                if (chkHeadertemp.Checked == false)
                {
                    continue;
                }
                Label lblITR_REQ_NO = drHeader.FindControl("lblITR_REQ_NO") as Label;

                Label lblITR_SEQ_NO = drHeader.FindControl("lblITR_SEQ_NO") as Label;
                Label lblitr_dt = drHeader.FindControl("lblitr_dt") as Label;
                Label lblITR_LOC = drHeader.FindControl("lblITR_LOC") as Label;
                Label lblITR_ISSUE_FROM = drHeader.FindControl("lblITR_ISSUE_FROM") as Label;
                Label lblITR_REC_TO = drHeader.FindControl("lblITR_REC_TO") as Label;
                Label lblItr_tp = drHeader.FindControl("lblItr_tp") as Label;

                string DocNumber = string.Empty;
                string DocType = string.Empty;
                string SubDocType = string.Empty;

                DocType = setDocumentType(lblItr_tp.Text.Trim());
                DocNumber = lblITR_REQ_NO.Text.Trim();

                //  DocType = setDocumentType(lblItr_tp.Text.Trim());

                if (oAppredItems != null && oAppredItems.Count > 0)
                {
                    var sum = oAppredItems.Where(d => d.Itr_req_no == lblITR_REQ_NO.Text).ToList();
                    //var sum = oAppredItems.GroupBy(d => d.Itr_req_no).Select(g => new
                    //{

                    //    Key = g.Key,
                    //    Value = g.Sum(s => s.Itri_bqty)
                    //}).ToList();

                    if (sum != null && sum.Count > 0)
                    {
                        #region supplier valdation add by lakshan 30 Jun 2017
                        InventoryRequest _invreqSup = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = DocNumber }).FirstOrDefault();
                        if (_invreqSup != null)
                        {
                            if (_invreqSup.Itr_tp == "SO")
                            {
                                MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileByCom(_invreqSup.Itr_bus_code, null, null, null, null,
                                    Session["UserCompanyCode"].ToString());
                                if (_mstBusEntity != null)
                                {
                                    if (_mstBusEntity.Mbe_is_suspend == true)
                                    {
                                        _errList.Add(new TmpValidation() { _isErr = true, ErrTP = _mstBusEntity.Mbe_cd });
                                    }
                                }
                            }
                        }
                        #endregion
                        InventoryRequest oHeader = new InventoryRequest();
                        oHeader.Itr_com = Session["UserCompanyCode"].ToString();
                        oHeader.Itr_req_no = string.Empty;
                        oHeader.Itr_tp = DocType;

                        //set sub type subodana
                        InventoryRequest _tmpReq = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = lblITR_REQ_NO.Text.ToString() }).FirstOrDefault();
                        if (_tmpReq != null)
                        {
                            oHeader.Itr_sub_tp = _tmpReq.Itr_sub_tp;
                            oHeader.Itr_job_no = _tmpReq.Itr_job_no;
                            oHeader.Itr_job_line = _tmpReq.Itr_job_line;
                        }
                        oHeader.Itr_loc = Session["UserDefLoca"].ToString();
                        oHeader.Itr_ref = DocNumber;
                        oHeader.Itr_dt = DateTime.Now.Date;
                        oHeader.Itr_exp_dt = DateTime.Now.Date;
                        oHeader.Itr_stus = "A";
                        oHeader.Itr_bus_code = string.Empty;
                        oHeader.Itr_note = string.Empty;
                        oHeader.Itr_issue_from = lblITR_ISSUE_FROM.Text;
                        oHeader.Itr_rec_to = lblITR_REC_TO.Text;
                        oHeader.Itr_direct = 0;
                        oHeader.Itr_country_cd = string.Empty;
                        oHeader.Itr_town_cd = string.Empty;
                        oHeader.Itr_cur_code = string.Empty;
                        oHeader.Itr_exg_rate = 0;
                        oHeader.Itr_collector_id = string.Empty;
                        oHeader.Itr_collector_name = string.Empty;
                        oHeader.Itr_act = 1;
                        oHeader.Itr_cre_by = Session["UserID"].ToString();
                        oHeader.Itr_session_id = Session["SessionID"].ToString();
                        oHeader.Itr_issue_com = Session["UserCompanyCode"].ToString();

                        MasterAutoNumber masterAuto = new MasterAutoNumber();
                        masterAuto.Aut_cate_tp = "LOC";
                        masterAuto.Aut_cate_cd = string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = DocType;
                        masterAuto.Aut_number = 0;
                        masterAuto.Aut_start_char = DocType;
                        masterAuto.Aut_year = null;

                        oHeader._mastAutoNo = masterAuto;

                        List<InventoryRequestItem> oSaveItems = oAppredItems.Where(x => x.Itr_req_no == oHeader.Itr_ref).ToList();
                        // foreach (InventoryRequestItem _itm in oSaveItems)
                        foreach (var item in oSaveItems)
                        {
                            item.Itri_job_no = oHeader.Itr_job_no;
                            item.Itri_job_line = oHeader.Itr_job_line;
                        }
                        oHeader.InventoryRequestItemList = oSaveItems;
                        oHeaders.Add(oHeader);
                        // }
                    }
                }
            }
            string GenDoc = string.Empty;
            string err = string.Empty;
            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];
            string loadingbay = (string)Session["loadingbay"];
            bool PDA = false;
            if (chkpda.Checked == true)
            {
                PDA = true;
            }
            #region add validation 30 Jun 2017
            var _isErrAva = _errList.Where(c => c._isErr == true).ToList();
            if (_isErrAva != null)
            {
                if (_errList.Count > 0)
                {
                    DispMsg("This customer already suspended : " + _errList[0].ErrTP);
                    btnSave.Enabled = true;
                    btnSave.CssClass = "buttonUndocolorLeft floatRight";
                    btnSave.OnClientClick = "confSave();";
                    return;
                }
            }
            #endregion
            Int32 result = CHNLSVC.Inventory.SaveDispatchPlanebyAllitem(oHeaders, txtLocation.Text, ddlStatus.SelectedValue, Session["_status2"].ToString(), false, PDA, warehousecom, warehouseloc, loadingbay, out GenDoc, out err);
            if (result != -1)
            {
                DisplayMessage("Successfully Approved. Document Number : " + GenDoc, 3);
                clear();
                if (chkpda.Checked == true)
                {
                    sebPDA(GenDoc);
                }
                return;
            }
            else
            {
                DisplayMessage(err, 4);
            }
        }
        protected void btnApproveSave_Click(object sender, EventArgs e)
        {
            if (hdfApproveAll.Value == "1")
            {
                string selectedLoc = ddlIsseLocation.SelectedValue.ToString();
                // InventoryLocation oSelectedLocItems = oLocItems.Find(x => x.Inl_loc == selectedLoc);

                // for (int Hi = 0; Hi < dgvRequestHeader.Rows.Count; Hi++)
                ////  {
                //   GridViewRow drHeader = dgvRequestHeader.Rows[Hi];
                //   CheckBox chkHeader = drHeader.FindControl("chkHeader") as CheckBox;
                //    if (chkHeader.Checked == false)
                //   {
                //   continue;
                //   }
                List<TmpValidation> _errList = new List<TmpValidation>();
                
                for (int Di = 0; Di < dgvRequestItems.Rows.Count; Di++)
                {
                    GridViewRow drDetail = dgvRequestItems.Rows[Di];
                    CheckBox chkDetail = drDetail.FindControl("chkDetail") as CheckBox;
                    //  string Item =dgvRequestItems.Rows[Di].Cells[''].ToString();
                    string STATUS = ddlIssueItemStatus.SelectedValue.ToString();
                    Label lblItri_itm_cd = drDetail.FindControl("lblItri_itm_cd") as Label;
                    InventoryLocation oSelectedLocItems = oLocItems.Find(x => x.Inl_loc == selectedLoc && x.Inl_itm_cd == lblItri_itm_cd.Text && x.Inl_itm_stus==STATUS);
                    if (chkDetail.Checked == false)
                    {
                        continue;
                    }

                    if (oSelectedLocItems == null)
                    {
                        continue;
                    }

                    //Current Stock grid
                    String lblINL_COM = oSelectedLocItems.Inl_com;
                    String lblINL_LOC = oSelectedLocItems.Inl_loc;
                    String lblINL_ITM_CD = oSelectedLocItems.Inl_itm_cd;
                    String lblINL_ITM_STUS = oSelectedLocItems.Inl_itm_stus;
                    String lblMIS_DESC = oSelectedLocItems.Mis_desc;
                    String lblINL_QTY = oSelectedLocItems.Inl_qty.ToString();
                    String lblinl_res_qty = oSelectedLocItems.Inl_res_qty.ToString();
                    String lblINL_FREE_QTY = oSelectedLocItems.Inl_free_qty.ToString();

                    // Request Details grid

                    Label lblItri_app_qty = drDetail.FindControl("lblItri_app_qty") as Label;
                    Label lblItri_bqty = drDetail.FindControl("lblItri_bqty") as Label;
                    Label lblItri_seq_no = drDetail.FindControl("lblItri_seq_no") as Label;
                    Label lblItri_line_no = drDetail.FindControl("lblItri_line_no") as Label;
                    Label lblItri_res_no = drDetail.FindControl("lblItri_res_no") as Label;

                    Label lblItr_req_no = drDetail.FindControl("lblItr_req_no") as Label;

                    Int32 DetailSeq = Convert.ToInt32(lblItri_seq_no.Text);
                    Int32 DetailLine = Convert.ToInt32(lblItri_line_no.Text);

                    decimal QtyNewAmount = Convert.ToDecimal(lblItri_app_qty.Text);

                    decimal QtyApp = (lblItri_app_qty.Text == "") ? 0 : Convert.ToDecimal(lblItri_app_qty.Text);
                    decimal QtyBal = (lblItri_bqty.Text == "") ? 0 : Convert.ToDecimal(lblItri_bqty.Text);
                    decimal QtyFreeStock = (lblINL_FREE_QTY == "") ? 0 : Convert.ToDecimal(lblINL_FREE_QTY);
                    decimal QtyResve = (lblinl_res_qty == "") ? 0 : Convert.ToDecimal(lblinl_res_qty);

                    if (QtyApp == 0)
                    {
                       // DisplayMessage("You dont have approved Qty");
                        continue;
                    }
                    if (QtyBal == 0)
                    {
                       // DisplayMessage("You dont have balance Qty");
                        continue;
                    }
                    if (QtyFreeStock <= 0)
                    {
                        //DisplayMessage("You dont have free Qty in " + lblINL_LOC + " location");
                        continue;
                    }

                    if (QtyFreeStock < QtyNewAmount)
                    {
                        //if location dont have required qty
                        QtyNewAmount = QtyFreeStock;
                    }

                    if (QtyBal < QtyNewAmount)
                    {
                        continue;

                    }
                    if (!string.IsNullOrEmpty(lblItri_res_no.Text.Trim()))
                    {
                        if (QtyResve < QtyNewAmount)
                        {
                            continue;
                        }
                    }

                    if (oAppredItems.FindAll(x => x.Itri_loc == lblINL_LOC.Trim() && x.Itri_itm_cd == lblINL_ITM_CD && x.Itri_itm_stus == lblINL_ITM_STUS.Trim() && x.Itri_res_no == lblItr_req_no.Text).Count > 0)
                    {
                        InventoryRequestItem oOldItem = oAppredItems.Find(x => x.Itri_loc == lblINL_LOC.Trim() && x.Itri_itm_cd == lblINL_ITM_CD && x.Itri_itm_stus == lblINL_ITM_STUS.Trim());
                        oOldItem.Itri_bqty = oOldItem.Itri_bqty + QtyNewAmount;
                        oOldItem.Itri_qty = oOldItem.Itri_qty + QtyNewAmount;
                        oOldItem.Itri_app_qty = oOldItem.Itri_app_qty + QtyNewAmount;
                    }
                    else
                    {
                        InventoryRequestItem oNewItem = new InventoryRequestItem();
                        oNewItem.Itri_loc = lblINL_LOC.Trim();
                        oNewItem.Itri_itm_cd = lblINL_ITM_CD.Trim();
                        oNewItem.Itri_itm_stus = lblINL_ITM_STUS.Trim();
                        oNewItem.Mis_desc = lblMIS_DESC.Trim();
                        oNewItem.Itri_bqty = QtyNewAmount;
                        oNewItem.Itri_qty = QtyNewAmount;
                        oNewItem.Itri_app_qty = QtyNewAmount;
                        oNewItem.Itr_req_no = lblItr_req_no.Text;
                        oNewItem.Itri_res_no = lblItri_res_no.Text;
                        oNewItem.Itri_base_req_line = DetailLine;
                        oAppredItems.Add(oNewItem);
                    }

                    oSelectedLocItems.Inl_free_qty = oSelectedLocItems.Inl_free_qty - QtyNewAmount;
                }
                // }

                                                                              if (oAppredItems == null || oAppredItems.Count == 0)
                {
                    DisplayMessage("Cannot find items in the selected location with selected item status");
                    return;
                }

                //=================================================================================================
                string DocType = string.Empty;
                string SubDocType = string.Empty;

                DocType = "MRNA";

                List<InventoryRequest> oHeaders = new List<InventoryRequest>();

                for (int hi = 0; hi < dgvRequestHeader.Rows.Count; hi++)
                {
                    GridViewRow drHeader = dgvRequestHeader.Rows[hi];
                    CheckBox chkHeadertemp = drHeader.FindControl("chkHeader") as CheckBox;
                    if (chkHeadertemp.Checked == false)
                    {
                        continue;
                    }
                    Label lblITR_SEQ_NO = drHeader.FindControl("lblITR_SEQ_NO") as Label;
                    Label lblITR_REQ_NO = drHeader.FindControl("lblITR_REQ_NO") as Label;
                    Label lblitr_dt = drHeader.FindControl("lblitr_dt") as Label;
                    Label lblITR_LOC = drHeader.FindControl("lblITR_LOC") as Label;
                    Label lblITR_ISSUE_FROM = drHeader.FindControl("lblITR_ISSUE_FROM") as Label;
                    Label lblITR_REC_TO = drHeader.FindControl("lblITR_REC_TO") as Label;
                    Label lblItr_tp = drHeader.FindControl("lblItr_tp") as Label;

                    string DocNumber = string.Empty;

                    DocNumber = lblITR_REQ_NO.Text.Trim();

                    DocType = setDocumentType(lblItr_tp.Text.Trim());

                    if (oAppredItems != null && oAppredItems.Count > 0)
                    {
                        string[] oAppLocations = oAppredItems.Select(x => x.Itri_loc).Distinct().ToArray();
                        if (oAppLocations != null && oAppLocations.Length > 0)
                        {
                            foreach (string oAppLoc in oAppLocations)
                            {
                                #region supplier valdation add by lakshan 30 Jun 2017
                                InventoryRequest _invreqSup = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = DocNumber }).FirstOrDefault();
                                if (_invreqSup != null)
                                {
                                    if (_invreqSup.Itr_tp == "SO")
                                    {
                                        MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileByCom(_invreqSup.Itr_bus_code, null, null, null, null,
                                            Session["UserCompanyCode"].ToString());
                                        if (_mstBusEntity != null)
                                        {
                                            if (_mstBusEntity.Mbe_is_suspend == true)
                                            {
                                                _errList.Add(new TmpValidation() { _isErr = true, ErrTP = _mstBusEntity.Mbe_cd });
                                            }
                                        }
                                    }
                                }
                                #endregion
                                InventoryRequest oHeader = new InventoryRequest();
                                oHeader.Itr_com = Session["UserCompanyCode"].ToString();
                                oHeader.Itr_req_no = string.Empty;
                                oHeader.Itr_tp = DocType;

                                //set sub type subodana
                                InventoryRequest _tmpReq = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = lblITR_REQ_NO.Text.ToString() }).FirstOrDefault();
                                if (_tmpReq != null)
                                {
                                    oHeader.Itr_sub_tp = _tmpReq.Itr_sub_tp;
                                    oHeader.Itr_job_no = _tmpReq.Itr_job_no;
                                    oHeader.Itr_job_line = _tmpReq.Itr_job_line;
                                }
                                oHeader.Itr_loc = Session["UserDefLoca"].ToString(); ;
                                oHeader.Itr_ref = DocNumber;
                                oHeader.Itr_dt = DateTime.Now.Date;
                                oHeader.Itr_exp_dt = DateTime.Now.Date;
                                oHeader.Itr_stus = "A";
                                oHeader.Itr_bus_code = string.Empty;
                                oHeader.Itr_note = string.Empty;
                                oHeader.Itr_issue_from = lblITR_ISSUE_FROM.Text;
                                oHeader.Itr_rec_to = lblITR_REC_TO.Text;
                                oHeader.Itr_direct = 0;
                                oHeader.Itr_country_cd = string.Empty;
                                oHeader.Itr_town_cd = string.Empty;
                                oHeader.Itr_cur_code = string.Empty;
                                oHeader.Itr_exg_rate = 0;
                                oHeader.Itr_collector_id = string.Empty;
                                oHeader.Itr_collector_name = string.Empty;
                                oHeader.Itr_act = 1;
                                oHeader.Itr_cre_by = Session["UserID"].ToString();
                                oHeader.Itr_session_id = Session["SessionID"].ToString();
                                oHeader.Itr_issue_com = Session["UserCompanyCode"].ToString();

                                MasterAutoNumber masterAuto = new MasterAutoNumber();
                                masterAuto.Aut_cate_tp = "LOC";
                                masterAuto.Aut_cate_cd = string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
                                masterAuto.Aut_direction = null;
                                masterAuto.Aut_modify_dt = null;
                                masterAuto.Aut_moduleid = DocType;
                                masterAuto.Aut_number = 0;
                                masterAuto.Aut_start_char = DocType;
                                masterAuto.Aut_year = null;

                                oHeader._mastAutoNo = masterAuto;

                                List<InventoryRequestItem> oSaveItems = oAppredItems.FindAll(x => x.Itri_loc == oAppLoc && x.Itr_req_no == oHeader.Itr_ref);
                                Int32 lineNum = 0;
                                foreach (InventoryRequestItem oSaveItem in oSaveItems)
                                {
                                    lineNum = lineNum + 1;

                                    decimal Qty = oSaveItem.Itri_qty;

                                    oSaveItem.Itri_seq_no = 0;
                                    oSaveItem.Itri_line_no = lineNum;
                                    oSaveItem.Itri_qty = Qty;
                                    oSaveItem.Itri_unit_price = 0;
                                    oSaveItem.Itri_app_qty = Qty;
                                    oSaveItem.Itri_mqty = Qty;
                                    oSaveItem.Itri_bqty = Qty;
                                    oSaveItem.Itri_res_qty = Qty;
                                    oSaveItem.Itri_res_line = 0;
                                    oSaveItem.Itri_job_no = oHeader.Itr_job_no;
                                    oSaveItem.Itri_job_line = oHeader.Itr_job_line;
                                }

                                oHeader.InventoryRequestItemList = oSaveItems;

                                oHeaders.Add(oHeader);
                            }
                        }
                    }
                }
                string err = string.Empty;
                string GenDoc = string.Empty;
                ViewState["oRequesItems"] = oRequesItems;
                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                string loadingbay = (string)Session["loadingbay"];
                bool PDA = false;
                if (chkpda.Checked == true)
                {
                    PDA = true;
                }
                #region add validation 30 Jun 2017
                var _isErrAva = _errList.Where(c => c._isErr == true).ToList();
                if (_isErrAva != null)
                {
                    if (_errList.Count > 0)
                    {
                        DispMsg("This customer already suspended : " + _errList[0].ErrTP);
                        btnSave.Enabled = true;
                        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                        btnSave.OnClientClick = "confSave();";
                        return;
                    }
                }
                #endregion
                Int32 result = CHNLSVC.Inventory.SaveDispatchPlan(oHeaders, oRequesItems, string.Empty, true, false, PDA, warehousecom, warehouseloc, loadingbay, out GenDoc, out err, Session["ISRESNO"].ToString(), true);
                if (result > 0)
                {
                    DisplayMessage("Successfully Approved. Document Number : " + GenDoc, 3);
                    Session["ApproveAllSuccess"] = "Successfully Approved. Document Number : " + GenDoc;
                    clear();
                    if (chkpda.Checked == true)
                    {
                        sebPDA(GenDoc);
                    }
                    return;
                }
                else
                {
                    if (err.Contains("Reservation"))
                    {
                        DispMsg(err);
                        Session["ApproveAllSuccess"] = err;
                    }
                    else
                    {
                        DisplayMessage(err, 4);
                        Session["ApproveAllSuccess"] = err;
                    }
                }
            }

            mpApproveAll.Show();
        }

        protected void dgvAutoApprove_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnDelAppItemAuto_Click(object sender, EventArgs e)
        {

        }

        protected void btnAUtoApproveAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedLoc = ddlIsseLocation.SelectedValue.ToString();
                InventoryLocation oSelectedLocItems = oLocItems.Find(x => x.Inl_loc == selectedLoc);

                for (int Hi = 0; Hi < dgvRequestHeader.Rows.Count; Hi++)
                {
                    GridViewRow drHeader = dgvRequestHeader.Rows[Hi];
                    CheckBox chkHeader = drHeader.FindControl("chkHeader") as CheckBox;
                    if (chkHeader.Checked == false)
                    {
                        continue;
                    }

                    for (int Di = 0; Di < dgvRequestItems.Rows.Count; Di++)
                    {
                        GridViewRow drDetail = dgvRequestItems.Rows[Di];
                        CheckBox chkDetail = drDetail.FindControl("chkDetail") as CheckBox;
                        if (chkDetail.Checked == false)
                        {
                            continue;
                        }

                        if (oSelectedLocItems == null)
                        {
                            continue;
                        }

                        //Current Stock grid
                        String lblINL_COM = oSelectedLocItems.Inl_com;
                        String lblINL_LOC = oSelectedLocItems.Inl_loc;
                        String lblINL_ITM_CD = oSelectedLocItems.Inl_itm_cd;
                        String lblINL_ITM_STUS = oSelectedLocItems.Inl_itm_stus;
                        String lblMIS_DESC = oSelectedLocItems.Mis_desc;
                        String lblINL_QTY = oSelectedLocItems.Inl_qty.ToString();
                        String lblinl_res_qty = oSelectedLocItems.Inl_res_qty.ToString();
                        String lblINL_FREE_QTY = oSelectedLocItems.Inl_free_qty.ToString();

                        // Request Details grid
                        Label lblItri_app_qty = drDetail.FindControl("lblItri_app_qty") as Label;
                        Label lblItri_bqty = drDetail.FindControl("lblItri_bqty") as Label;
                        Label lblItri_seq_no = drDetail.FindControl("lblItri_seq_no") as Label;
                        Label lblItri_line_no = drDetail.FindControl("lblItri_line_no") as Label;
                        Label lblItri_res_no = drDetail.FindControl("lblItri_res_no") as Label;

                        Label lblItr_req_no = drDetail.FindControl("lblItr_req_no") as Label;

                        Int32 DetailSeq = Convert.ToInt32(lblItri_seq_no.Text);
                        Int32 DetailLine = Convert.ToInt32(lblItri_line_no.Text);

                        decimal QtyNewAmount = Convert.ToDecimal(lblItri_app_qty.Text);

                        decimal QtyApp = (lblItri_app_qty.Text == "") ? 0 : Convert.ToDecimal(lblItri_app_qty.Text);
                        decimal QtyBal = (lblItri_bqty.Text == "") ? 0 : Convert.ToDecimal(lblItri_bqty.Text);
                        decimal QtyFreeStock = (lblINL_FREE_QTY == "") ? 0 : Convert.ToDecimal(lblINL_FREE_QTY);
                        decimal QtyResve = (lblinl_res_qty == "") ? 0 : Convert.ToDecimal(lblinl_res_qty);

                        if (QtyApp == 0)
                        {
                            DisplayMessage("You dont have approved Qty");
                            continue;
                        }
                        if (QtyBal == 0)
                        {
                            DisplayMessage("You dont have balance Qty");
                            continue;
                        }
                        if (QtyFreeStock <= 0)
                        {
                            DisplayMessage("You dont have free Qty in " + lblINL_LOC + " location");
                            continue;
                        }

                        if (QtyFreeStock < QtyNewAmount)
                        {
                            //if location dont have required qty
                            QtyNewAmount = QtyFreeStock;
                        }

                        if (QtyBal < QtyNewAmount)
                        {
                            continue;

                        }
                        if (!string.IsNullOrEmpty(lblItri_res_no.Text.Trim()))
                        {
                            if (QtyResve < QtyNewAmount)
                            {
                                continue;
                            }
                        }

                        if (oAppredItems.FindAll(x => x.Itri_loc == lblINL_LOC.Trim() && x.Itri_itm_cd == lblINL_ITM_CD && x.Itri_itm_stus == lblINL_ITM_STUS.Trim() && x.Itri_res_no == lblItr_req_no.Text).Count > 0)
                        {
                            InventoryRequestItem oOldItem = oAppredItems.Find(x => x.Itri_loc == lblINL_LOC.Trim() && x.Itri_itm_cd == lblINL_ITM_CD && x.Itri_itm_stus == lblINL_ITM_STUS.Trim());
                            oOldItem.Itri_bqty = oOldItem.Itri_bqty + QtyNewAmount;
                            oOldItem.Itri_qty = oOldItem.Itri_qty + QtyNewAmount;
                            oOldItem.Itri_app_qty = oOldItem.Itri_app_qty + QtyNewAmount;
                        }
                        else
                        {
                            InventoryRequestItem oNewItem = new InventoryRequestItem();
                            oNewItem.Itri_loc = lblINL_LOC.Trim();
                            oNewItem.Itri_itm_cd = lblINL_ITM_CD.Trim();
                            oNewItem.Itri_itm_stus = lblINL_ITM_STUS.Trim();
                            oNewItem.Mis_desc = lblMIS_DESC.Trim();
                            oNewItem.Itri_bqty = QtyNewAmount;
                            oNewItem.Itri_qty = QtyNewAmount;
                            oNewItem.Itri_app_qty = QtyNewAmount;
                            oNewItem.Itr_req_no = lblItr_req_no.Text;
                            oNewItem.Itri_res_no = lblItri_res_no.Text;
                            oAppredItems.Add(oNewItem);
                        }

                        oSelectedLocItems.Inl_free_qty = oSelectedLocItems.Inl_free_qty - QtyNewAmount;

                        dgvAutoApprove.DataSource = oAppredItems;
                        dgvAutoApprove.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            mpApproveAll.Show();
        }

        protected void txtRoute_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRoute.Text.Trim()))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DispatchRoute);
                DataTable result = CHNLSVC.CommonSearch.Search_Routes(SearchParams, "CODE", txtRoute.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {

                }
                else
                {

                    DisplayMessage("Please select a valid router code");
                    txtRoute.Text = "";
                    return;
                }
            }
        }

        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtLocation.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("Please select a valid location code");
                    txtLocation.Text = "";
                    return;
                }
            }
        }

        protected void txtMainCategory_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMainCategory.Text.Trim()))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, "CODE", txtMainCategory.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("Please select a valid main category code");
                    txtMainCategory.Text = "";
                    return;
                }
            }
        }

        protected void txtSubCategory_TextChanged(object sender, EventArgs e)
        {
            if (txtMainCategory.Text == string.Empty || txtMainCategory.Text.ToUpper() == "ALL")
            {
                DisplayMessage("Please select the main category");
                return;
            }

            if (!string.IsNullOrEmpty(txtSubCategory.Text.Trim()))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, "CODE", txtSubCategory.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("Please select a valid sub category code");
                    txtSubCategory.Text = "";
                    return;
                }
            }
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text.Trim()))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, "item", txtItemCode.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("Please select a valid item code");
                    txtItemCode.Text = "";
                    return;
                }
            }
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtModel.Text.Trim()))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, "Code", txtModel.Text.Trim());
                if (result != null && result.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("Please select a valid item model code");
                    txtModel.Text = "";
                    return;
                }
            }

        }

        protected void txtRequestNumber_TextChanged(object sender, EventArgs e)
        {
            btnSearchRequest_Click(null, null);
        }

        protected void btnSearchRequest_Click(object sender, EventArgs e)
        {
            oRequestHeaders = new List<InventoryRequest>();
            oLocItems = new List<InventoryLocation>();
            oRequesItems = new List<InventoryRequestItem>();

            oAppredItems = new List<InventoryRequestItem>();

            IndexHeader = -1;
            IndexDetails = -1;
            IndexStock = -1;

            dgvRequestHeader.DataSource = new int[] { };
            dgvRequestHeader.DataBind();

            dgvCurrentStockDetails.DataSource = new int[] { };
            dgvCurrentStockDetails.DataBind();

            dgvRequestItems.DataSource = new int[] { };
            dgvRequestItems.DataBind();

            dgvApprovedItems.DataSource = new int[] { };
            dgvApprovedItems.DataBind();

            dgvRequestHeader.Enabled = true;

            String Route = "";
            String MainCate = "";
            String Item = "";
            String SubCate = "";
            String Model = "";
            int type = 1;
            if (chkAppDoc.Checked) { type = 2; }

            if (!string.IsNullOrEmpty(txtRequestNumber.Text.Trim()))
            {
                //oRequestHeaders = CHNLSVC.Inventory.GET_REQUEST_FOR_DISPATCH_NO(Session["UserCompanyCode"].ToString(), Route, MainCate, Item, ddlReqType.SelectedValue.ToString(), txtLocation.Text.Trim(), SubCate, Model, Convert.ToDateTime("01-Jan-2000"), Convert.ToDateTime("31-Dec-2050"), type, txtRequestNumber.Text.ToString());
                oRequestHeaders = CHNLSVC.Inventory.GET_REQ_FOR_DISPATCH_BYNO(Session["UserCompanyCode"].ToString(), Route, MainCate, Item, ddlReqType.SelectedValue.ToString(), txtLocation.Text.Trim(), SubCate, Model, Convert.ToDateTime("01-Jan-2000"), Convert.ToDateTime("31-Dec-2050"), type, txtRequestNumber.Text.ToString());
            }
            else
            {
                oRequestHeaders = CHNLSVC.Inventory.GET_REQUEST_FOR_DISPATCH(Session["UserCompanyCode"].ToString(), Route, MainCate, Item, ddlReqType.SelectedValue.ToString(), txtLocation.Text.Trim(), SubCate, Model, Convert.ToDateTime("01-Jan-2000"), Convert.ToDateTime("31-Dec-2050"), type);
            }

            if (oRequestHeaders != null && oRequestHeaders.Count > 0)
            {
                //if (!string.IsNullOrEmpty(txtRequestNumber.Text.Trim()))
                //{
                //    oRequestHeaders = oRequestHeaders.FindAll(x => x.Itr_req_no.Contains(txtRequestNumber.Text.ToUpper().Trim()));
                //}
                dgvRequestHeader.DataSource = oRequestHeaders;
                dgvRequestHeader.DataBind();
                Session["dataTable"] = oRequestHeaders;
                chkAppDoc.Enabled = false;
            }
            else
            {
                DisplayMessage("No Records Found.");
                dgvRequestHeader.DataSource = new int[] { };
                dgvRequestHeader.DataBind();
            }

        }

        protected void dgvRequestHeader_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblItr_bus_code = e.Row.FindControl("LblItr_bus_code") as Label;
                Label lblItr_anal1 = e.Row.FindControl("lblItr_anal1") as Label;
                string strFullText = LblItr_bus_code.Text;
                LblItr_bus_code.ToolTip = lblItr_anal1.Text;
            }
        }
        private void LoadAppLevelStatus()
        {

            DataTable dt = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "MIS_DESC";
            ddlStatus.DataValueField = "MIC_CD";
            ddlStatus.DataBind();
            ddlStatus.SelectedIndex = 14;

        }

        private bool checkItem()
        {
            bool value = true; ;
            for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
            {
                GridViewRow drw = dgvRequestItems.Rows[i];
                CheckBox chkDetail = drw.FindControl("chkDetail") as CheckBox;
                if (chkDetail.Checked == false)
                {
                    value = false;
                    return value;
                }
            }
            return value;
        }
        protected void btnshow_Click(object sender, EventArgs e)
        {

            if (checkItem())
            {
                DataRow dr = null;
                DataTable _tbl = new DataTable();
                _tbl.Columns.Add(new DataColumn("Item", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Qty", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Stock", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Res", typeof(string)));
                _tbl.Columns.Add(new DataColumn("LQty", typeof(string)));


                List<InventoryRequestItem> _item = new List<InventoryRequestItem>();
                _item = ViewState["oRequesItems"] as List<InventoryRequestItem>;
                if (_item == null)
                {
                    DisplayMessage("Requested details cannot be empty", 2);
                    return;
                }
                var sum = _item.GroupBy(d => d.Itri_itm_cd).Select(g => new
                  {

                      Key = g.Key,
                      Value = g.Sum(s => s.Itri_bqty)
                  }).ToList();


                foreach (var _ck in sum)
                {
                    dr = _tbl.NewRow();
                    dr["Item"] = _ck.Key;
                    dr["Qty"] = _ck.Value;
                   // dr["line"] = _ck.;
                    DataTable _inventoryLocation = CHNLSVC.Inventory.GetINV_BAL_STUS_LPAND_IMP(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _ck.Key, ddlStatus.SelectedValue);
                    if (_inventoryLocation.Rows.Count > 0)
                    {
                        dr["Stock"] = _inventoryLocation.Rows[0]["INL_FREE_QTY"].ToString();
                        dr["LQty"] = _inventoryLocation.Rows[0]["Inl_qty"].ToString();
                        dr["Res"] = _inventoryLocation.Rows[0]["Inl_res_qty"].ToString();
                    }
                    else
                    {
                        dr["Stock"] = "0";
                    }
                    _tbl.Rows.Add(dr);
                }
                lbtnstatus.Text = "With LP Status";
                ddlStatus.SelectedIndex = 14;
                chkstatus.Checked = false;
                grdsummery.DataSource = _tbl;
                grdsummery.DataBind();
                Popupsummery.Show();
            }
            else
            {
                DisplayMessage("Please select all the items");
            }
        }


        protected void btnsummeryapproval_Click(object sender, EventArgs e)
        {
           
            if (isAnyDetailsSelected())
            {
                for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                {
                    GridViewRow dr = dgvRequestItems.Rows[i];
                    CheckBox chkDetail = dr.FindControl("chkDetail") as CheckBox;
                    if (chkDetail.Checked)
                    {
                        Label lblItri_seq_no = dr.FindControl("lblItri_seq_no") as Label;
                        Label lblItri_line_no = dr.FindControl("lblItri_line_no") as Label;
                        Label lblItri_itm_cd = dr.FindControl("lblItri_itm_cd") as Label;
                        string lblItri_itm_stus = ddlStatus.SelectedValue;
                        oLocItems.AddRange(CHNLSVC.Inventory.GET_LOC_ITEMS_FOR_DISPATCH(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), lblItri_itm_cd.Text.Trim(), lblItri_itm_stus.Trim()));
                    }
                }

                if (oLocItems.Count > 0)
                {
                    var Locs = oLocItems.Select(x => new { x.Inl_loc, x.Ml_loc_desc }).Distinct().ToList();
                    if (Locs != null)
                    {
                        ddlIsseLocation.DataSource = Locs;
                        ddlIsseLocation.DataTextField = "Ml_loc_desc";
                        ddlIsseLocation.DataValueField = "Inl_loc";
                        ddlIsseLocation.DataBind();
                    }

                    var ItemStatus = oLocItems.Select(x => new { x.Inl_itm_stus, x.Mis_desc }).Distinct().ToList();
                    if (ItemStatus != null)
                    {
                        ddlIssueItemStatus.DataSource = ItemStatus;
                        ddlIssueItemStatus.DataTextField = "Mis_desc";
                        ddlIssueItemStatus.DataValueField = "Inl_itm_stus";
                        ddlIssueItemStatus.DataBind();
                    }
                }
                ApproveAll();
                if (Session["ApproveAllSuccess"].ToString() !="")
                {
                    DisplayMessage(Session["ApproveAllSuccess"].ToString(), 3);
                }
              
                Session["ApproveAllSuccess"] = "";
                // mpApproveAll.Show();
            }
            else
            {
                DisplayMessage("Please select the requested items to approve", 1);
                return;
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkItem())
            {
                DataRow dr = null;
                DataTable _tbl = new DataTable();
                _tbl.Columns.Add(new DataColumn("Item", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Qty", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Stock", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Res", typeof(string)));
                _tbl.Columns.Add(new DataColumn("LQty", typeof(string)));

                List<InventoryRequestItem> _item = new List<InventoryRequestItem>();
                _item = ViewState["oRequesItems"] as List<InventoryRequestItem>;
                var sum = _item.GroupBy(d => d.Itri_itm_cd).Select(g => new
                {

                    Key = g.Key,
                    Value = g.Sum(s => s.Itri_bqty)
                }).ToList();

                string lblname = string.Empty;
                string statusNew = string.Empty;
                foreach (var _ck in sum)
                {
                    dr = _tbl.NewRow();
                    dr["Item"] = _ck.Key;
                    dr["Qty"] = _ck.Value;

                    DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatu_both_LP_IMP(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _ck.Key, ddlStatus.SelectedValue, chkstatus.Checked, out lblname, out statusNew);
                    if (_inventoryLocation.Rows.Count > 0)
                    {
                        lbtnstatus.Text = lblname;
                        dr["Stock"] = _inventoryLocation.Rows[0]["INL_FREE_QTY"].ToString();
                        dr["LQty"] = _inventoryLocation.Rows[0]["Inl_qty"].ToString();
                        dr["Res"] = _inventoryLocation.Rows[0]["Inl_res_qty"].ToString();
                        if (chkstatus.Checked == true)
                        {
                            Session["_status2"] = statusNew;
                        }
                    }
                    else
                    {
                        lbtnstatus.Text = lblname;
                        dr["Stock"] = "0";
                    }
                    _tbl.Rows.Add(dr);
                }

                grdsummery.DataSource = _tbl;
                grdsummery.DataBind();
                Popupsummery.Show();
            }
            else
            {
                DisplayMessage("Please select all item");
            }
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            if (txtpdasend.Value == "Yes")
            {
                try
                {
                    if (!chkAppDoc.Checked)
                    {
                        DispMsg("Please search the approved documents !"); return;
                    }
                    Session["loadingbay"] = ddlloadingbay.SelectedValue;
                    SaveData();
                    MPPDA.Hide();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                    CHNLSVC.CloseChannel();
                }

            }
        }
        protected void btncClose_Click(object sender, EventArgs e)
        {
            try
            {
                MPPDA.Hide();
                ddlloadingbay.SelectedIndex = 0;
                txtdocname.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
        }
        private void PopulateLoadingBays()
        {
            try
            {
                DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");

                if (dtbays.Rows.Count > 0)
                {
                    ddlloadingbay.DataSource = dtbays;
                    ddlloadingbay.DataTextField = "mws_res_name";
                    ddlloadingbay.DataValueField = "mws_res_cd";
                    ddlloadingbay.DataBind();
                }

                ddlloadingbay.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }

        }


        private void SaveData()
        {
            Int32 val = 0;
            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];
            //if (string.IsNullOrEmpty(txtdocname.Text.Trim()))
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the document no !!!')", true);
            //    txtdocname.Focus();
            //    MPPDA.Show();
            //    return;
            //}

            if (ddlloadingbay.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the loading bay !!!')", true);
                ddlloadingbay.Focus();
                MPPDA.Show();
                return;
            }
            // Int32 _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "GRN", 1, Session["UserCompanyCode"].ToString());

            oRequesItems = ViewState["oRequesItems"] as List<InventoryRequestItem>;
            for (int i = 0; i < dgvRequestHeader.Rows.Count; i++)
            {

                GridViewRow dr = dgvRequestHeader.Rows[i];

                CheckBox _chk = dr.FindControl("chkHeader") as CheckBox;
                if (_chk.Checked)
                {
                    Label type = dr.FindControl("lblItr_tp") as Label;
                    Label docno = dr.FindControl("lblITR_REQ_NO") as Label;
                    Label direction = dr.FindControl("lblitr_direct") as Label;

                    #region Add by Lakshan to chk doc already send or not 01 Oct 2016
                    if (   oAppredItems.Count < 1)
                    {
                        DispMsg("Approve items not found !"); return;
                    }
                    _tmpPickHdr = new ReptPickHeader();
                    bool _docAva = false;
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                        Tuh_doc_no = docno.Text
                       // Tuh_direct = Convert.ToBoolean(direction.Text)
                    }).FirstOrDefault();
                    if (_tmpPickHdr != null)
                    {
                        _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                _docAva = true;
                            }
                        }
                        _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repItmList != null)
                        {
                            if (_repItmList.Count > 0)
                            {
                                _docAva = true;
                            }
                        }

                    }
                    if (_docAva)
                    {
                        //string _msg = "Document has already sent to PDA or has alread processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay :" + _tmpPickHdr.Tuh_load_bay ;
                        string _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay " + _tmpPickHdr.Tuh_load_bay;
                        DisplayMessage(_msg);
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                        return;
                    }
                    #endregion

                   /*11 Oct 2016 Add by Lakshan */
                    InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest()
                    {
                        Itr_req_no = docno.Text,
                        Itr_stus = "A",
                        Itr_tp = type.Text
                    }).FirstOrDefault();

                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(type.Text, Session["UserCompanyCode"].ToString(), docno.Text, Convert.ToInt32(direction.Text));
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo(type.Text, docno.Text);
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_doc_tp = type.Text;
                        int dir = Convert.ToInt32(direction.Text);
                        _inputReptPickHeader.Tuh_direct = Convert.ToBoolean(dir);
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_doc_no = docno.Text;
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        MasterLocation _whLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        bool _isWhLoc = false;
                        if (_whLoc!=null)
                        {
                            if (!string.IsNullOrEmpty(_whLoc.Ml_wh_com) && !string.IsNullOrEmpty(_whLoc.Ml_wh_cd))
                            {
                                _isWhLoc = true;
                            }
                        }
                        if (_isWhLoc)
                        {
                            _inputReptPickHeader.Tuh_wh_com = _whLoc.Ml_wh_com;
                            _inputReptPickHeader.Tuh_wh_loc = _whLoc.Ml_wh_cd;
                        }
                        else
                        {
                            DispMsg("Warehouse company data invalid "); return;
                        }
                        _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                        _inputReptPickHeader.Tuh_is_take_res = true;
                        if (_reqData != null)
                        {
                            _inputReptPickHeader.Tuh_rec_loc = _reqData.Itr_rec_to;
                        }
                        val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                        if (val == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            CHNLSVC.CloseChannel();
                            return;
                        }
                    }
                    else
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_doc_no = docno.Text;
                        _inputReptPickHeader.Tuh_doc_tp = type.Text;
                        int direc = Convert.ToInt32(direction.Text);
                        _inputReptPickHeader.Tuh_direct = Convert.ToBoolean(direc);
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        MasterLocation _whLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        bool _isWhLoc = false;
                        if (_whLoc != null)
                        {
                            if (!string.IsNullOrEmpty(_whLoc.Ml_wh_com) && !string.IsNullOrEmpty(_whLoc.Ml_wh_cd))
                            {
                                _isWhLoc = true;
                            }
                        }
                        if (_isWhLoc)
                        {
                            _inputReptPickHeader.Tuh_wh_com = _whLoc.Ml_wh_com;
                            _inputReptPickHeader.Tuh_wh_loc = _whLoc.Ml_wh_cd;
                        }
                        else
                        {
                            DispMsg("Warehouse company data invalid "); return;
                        }
                       // _inputReptPickHeader.Tuh_wh_com = warehousecom;
                       // _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                        _inputReptPickHeader.Tuh_is_take_res = true;
                        val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                        if (Convert.ToInt32(val) == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            CHNLSVC.CloseChannel();
                            return;
                        }
                    }


                    DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(user_seq_num);
                   
                    if (dtchkitm.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                        return;
                    }
                    else
                    {
                        if (oAppredItems.Count > 0)
                        {
                            List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                            var _filteritem = oAppredItems.Where(x => x.Itr_req_no == docno.Text).ToList();
                            if (_filteritem.Count > 0)
                            {
                                foreach (InventoryRequestItem _row in _filteritem)
                                {

                                    //AddItem(_item, _cost, null, null, user_seq_num.ToString(), null);
                                    if (_row.Itr_req_no == docno.Text)
                                    {
                                        ReptPickItems _reptitm = new ReptPickItems();
                                        _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                                        _reptitm.Tui_req_itm_qty = _row.Itri_bqty;
                                        _reptitm.Tui_req_itm_cd = _row.Itri_itm_cd;
                                        _reptitm.Tui_req_itm_stus = _row.Itri_itm_stus;
                                        _reptitm.Tui_pic_itm_cd =type.Text=="SOA"?  Convert.ToString(_row.Itri_base_req_line):Convert.ToString(_row.Itri_line_no);//Darshana request add by rukshan
                                        // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                                        // _reptitm.Tui_pic_itm_qty = _row.Itri_bqty;
                                        _saveonly.Add(_reptitm);
                                    }

                                }
                                val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                            }
                        }

                    }

                }

                if (val == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                    MPPDA.Hide();
                    chkpda.Checked = false;
                }
            }

        }

        private Int32 GenerateNewUserSeqNo(string DocumentType, string _scanDocument)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = DocumentType;
            RPH.Tuh_cre_dt = DateTime.Now;// DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change 
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = true; //direction always (-) for change status
            RPH.Tuh_doc_no = _scanDocument;
            //write entry to TEMP_PICK_HDR
            //int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            //if (affected_rows == 1)
            //{
            return generated_seq;
            //}
            //else
            //{
            //    return 0;
            //}
        }

        private void sebPDA(string docno)
        {

            DataTable dtdoccheck1 = CHNLSVC.Inventory.IsDocNoAvailable(docno, "MRNA", 1, Session["UserCompanyCode"].ToString());
            if (dtdoccheck1.Rows.Count > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + docno + " !!!')", true);
                return;
            }
            DataTable _headerchk2 = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), docno);
            Int64 _seqno = 0;
            if (_headerchk2 != null && _headerchk2.Rows.Count > 0)
            {
                string _headerUser = _headerchk2.Rows[0].Field<string>("tuh_usr_id");
                string _scanDate = Convert.ToString(_headerchk2.Rows[0].Field<DateTime>("tuh_cre_dt"));
                _seqno = _headerchk2.Rows[0].Field<Int64>("TUH_USRSEQ_NO");
                if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                    {
                        string msg2 = "Document " + docno + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                        return;
                    }
            }
            DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(Convert.ToInt32(_seqno));

            if (dtchkitm.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                return;
            }
            txtdocname.Text = docno;
            MPPDA.Show();
        }

        protected void chkpda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpda.Checked)
            {
                if (!chkAppDoc.Checked)
                {
                    chkpda.Checked = false;
                    DispMsg("Please search the approved documents !"); return;
                }
                MPPDA.Show();
            }

        }

        protected void chkstatus_CheckedChanged(object sender, EventArgs e)
        {

            if (checkItem())
            {
                DataRow dr = null;
                DataTable _tbl = new DataTable();
                _tbl.Columns.Add(new DataColumn("Item", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Qty", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Stock", typeof(string)));
                _tbl.Columns.Add(new DataColumn("Res", typeof(string)));
                _tbl.Columns.Add(new DataColumn("LQty", typeof(string)));

                List<InventoryRequestItem> _item = new List<InventoryRequestItem>();
                _item = ViewState["oRequesItems"] as List<InventoryRequestItem>;
                var sum = _item.GroupBy(d => d.Itri_itm_cd).Select(g => new
                {

                    Key = g.Key,
                    Value = g.Sum(s => s.Itri_bqty)
                }).ToList();

                string lblname = string.Empty;
                string statusNew = string.Empty;
                foreach (var _ck in sum)
                {
                    dr = _tbl.NewRow();
                    dr["Item"] = _ck.Key;
                    dr["Qty"] = _ck.Value;

                    DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatu_both_LP_IMP(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _ck.Key, ddlStatus.SelectedValue, chkstatus.Checked, out lblname, out statusNew);
                    if (_inventoryLocation.Rows.Count > 0)
                    {
                        lbtnstatus.Text = lblname;
                        dr["Stock"] = _inventoryLocation.Rows[0]["INL_FREE_QTY"].ToString();
                        dr["LQty"] = _inventoryLocation.Rows[0]["Inl_qty"].ToString();
                        dr["Res"] = _inventoryLocation.Rows[0]["Inl_res_qty"].ToString();
                        if (chkstatus.Checked == true)
                        {
                            Session["_status2"] = statusNew;
                        }
                        else
                        {
                            Session["_status2"] = "";
                        }

                    }
                    else
                    {
                        lbtnstatus.Text = lblname;
                        dr["Stock"] = "0";
                    }
                    _tbl.Rows.Add(dr);
                }

                grdsummery.DataSource = _tbl;
                grdsummery.DataBind();
                Popupsummery.Show();
            }
            else
            {
                DisplayMessage("Please select all item");
            }


        }

        protected void lbnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string docno = "";

                foreach (GridViewRow gvr in this.dgvRequestHeader.Rows)
                {

                    CheckBox check = (CheckBox)gvr.FindControl("chkHeader");

                    Label lblLocation = (Label)gvr.FindControl("lblITR_REQ_NO");
                    if (check.Checked)
                    {
                        docno = lblLocation.Text.ToString();

                    }

                }

                Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), null, null);
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null);

                InvReportPara _invRepPara = new InvReportPara();
                _invRepPara._GlbReportFromDate = Convert.ToDateTime("01/JAN/2000").Date;
                _invRepPara._GlbReportToDate = Convert.ToDateTime("31/DEC/2199").Date;
                _invRepPara._GlbReportColor = "";
                _invRepPara._GlbReportwithBin = 0;
                _invRepPara._GlbReportRoute = "";
                _invRepPara._GlbReportDoc = docno;
                _invRepPara._GlbReportCheckRegDate = 1; // All Routes
                _invRepPara._GlbReportnoofDays = 1; //Group Req
                _invRepPara._GlbReportFromAge = 0; //Group Model
                _invRepPara._GlbReportReqNo = "";
                _invRepPara._GlbReportType = "pick";
                _invRepPara._GlbReportCompCode = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbUserID = Session["UserID"].ToString();
                Session["GlbReportType"] = "pick";
                _invRepPara._GlbReportName = "Item_Pick_Plan.rpt";
                Session["GlbReportName"] = "Item_Pick_Plan.rpt";
                //string x = Session["UserCompanyCode"].ToString();                     

                _invRepPara._GlbReportHeading = "ITEM PICK PLAN";
                Session["InvReportPara"] = _invRepPara;

                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                csWarehouse obj = new csWarehouse();
                obj.ItemPickPlanReport(_invRepPara);
                if (Session["UserCompanyCode"].ToString() == "AAL")
                {
                    PrintPDF(targetFileName, obj._PICK_PLAN_AAL);
                }
                else
                {
                    PrintPDF(targetFileName, obj._pickplansum);
                }

                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //string url = "<script>window.open('/View/Reports/Warehouse/WarehouseReportViewer.aspx','_blank');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch(Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Dispatch Plan Print", "DispatchPlan", ex.Message, Session["UserID"].ToString());
            }
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
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
                throw ex;
            }
        }

        protected void ChkselectItm_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow item in dgvApprovedItems.Rows)
            {
               // TextBox lblitri_app_qty = item.FindControl("lblitri_app_qty") as TextBox;
                Label lblitri_app_qty = item.FindControl("lblitri_app_qty") as Label;
                Label lblitri_itm_cd = item.FindControl("lblitri_itm_cd") as Label;

                CheckBox ChkselectItm = item.FindControl("ChkselectItm") as CheckBox;
                if (ChkselectItm.Checked && lblitri_app_qty.Text.ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('"+lblitri_itm_cd.Text.ToString()+ " is already been canceled !')", true);
                    ChkselectItm.Checked = false;
                    return;
                }
            }
        }


        protected void chkHeaderAllApp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkHeaderAllApp = dgvApprovedItems.HeaderRow.FindControl("chkHeaderAllApp") as CheckBox;
            foreach (GridViewRow item in dgvApprovedItems.Rows)
            {
                Label lblitri_app_qty = item.FindControl("lblitri_app_qty") as Label;
               
                CheckBox ChkselectItm = item.FindControl("ChkselectItm") as CheckBox;
                if (chkHeaderAllApp.Checked && lblitri_app_qty.Text.ToString()!="0")
                {
                    ChkselectItm.Checked = true;
                }
                else
                {
                    ChkselectItm.Checked = false;
                }
            }
        }

        protected void dgvRequestHeader_Sorting(object sender, GridViewSortEventArgs e)
        {
            string _sortDirection = sortOrder;
            //Sort the data.
            //dataTable = Session["dataTable"] as DataTable;
            IEnumerable<InventoryRequest> resultnew2 = oRequestHeaders;
            dataTable = new DataTable();
            using (var reader = ObjectReader.Create(resultnew2))
            {
                dataTable.Load(reader);
            }

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                    dgvRequestHeader.DataSource = dataTable;
                    dgvRequestHeader.DataBind();
                    SortDireaction = _sortDirection;
                }
            }
        }

        protected void txtinloc_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnsearchpend_Click(object sender, EventArgs e)
        {
            oRequestHeaders = oRequestHeaders.Where(a => a.Itr_rec_to == txtinloc.Text.ToString()).ToList();
            if (oRequestHeaders != null && oRequestHeaders.Count > 0)
            {
                //if (!string.IsNullOrEmpty(txtRequestNumber.Text.Trim()))
                //{
                //    oRequestHeaders = oRequestHeaders.FindAll(x => x.Itr_req_no.Contains(txtRequestNumber.Text.ToUpper().Trim()));
                //}
                dgvRequestHeader.DataSource = oRequestHeaders;
                dgvRequestHeader.DataBind();
                Session["dataTable"] = oRequestHeaders;
                chkAppDoc.Enabled = false;
            }
            else
            {
                DisplayMessage("No Records Found.");
                dgvRequestHeader.DataSource = new int[] { };
                dgvRequestHeader.DataBind();
            }
        }

        protected void ResetRequest_Click(object sender, EventArgs e)
        {
            GridViewRow drSelect = (sender as LinkButton).NamingContainer as GridViewRow;
            Label renoo = drSelect.FindControl("lblITR_REQ_NO") as Label;
            //if (chkAppDoc.Checked == false)
            //{
            //    UpdateUserforReqempy(renoo.Text.ToString());
            //}

           
        }
        #region Add by Lakshan Send all document to pda 23 Sep 2016
        private void BindLodingBay()
        {
            DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");
            if (dtbays.Rows.Count > 0)
            {
                ddlSendAllLoadingBay.DataSource = dtbays;
                ddlSendAllLoadingBay.DataTextField = "mws_res_name";
                ddlSendAllLoadingBay.DataValueField = "mws_res_cd";
                ddlSendAllLoadingBay.DataBind();
            }
        }
        protected void lbtnAllToPda_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chkAppDoc.Checked)
                {
                    DispMsg("Please search the approved documents !"); return;
                }
                //App
                List<InventoryRequest> _invReqData = new List<InventoryRequest>();
                List<InventoryRequest> _invReqDataInPda = new List<InventoryRequest>();
                if (oRequestHeaders.Count<1)
                {
                    DispMsg("No pending documents available !"); return;
                }
                #region Doc available 
               _tmpPickHdr = new ReptPickHeader();
                foreach (var item in oRequestHeaders)
                {
                    #region Remove unavailable data
                    InventoryRequest _invReq = new InventoryRequest();
                    _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() 
                    { 
                        Itr_req_no = item.Itr_req_no, Itr_stus = "A", Itr_tp = item.Itr_tp 
                    }).FirstOrDefault();
                    if (_invReq==null)
                    {
                        continue;
                    }
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                        Tuh_doc_no = item.Itr_req_no,
                    }).FirstOrDefault();


                    if (_tmpPickHdr != null)
                    {
                        _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                _invReqDataInPda.Add(item);
                                continue;
                            }
                        }
                        _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repItmList != null)
                        {
                            if (_repItmList.Count > 0)
                            {
                                _invReqDataInPda.Add(item);
                                continue;
                            }
                        }
                        
                    }
                    _invReqData.Add(item);
                    #endregion
                }
                #endregion
                if (_invReqData.Count > 0)
                {
                    dgvPopPendingDoc.DataSource = _invReqData;
                    dgvPopPendingDoc.DataBind();

                    dgvSendDoc.DataSource = _invReqDataInPda;
                    dgvSendDoc.DataBind();

                    _showPopSendToPda = true;
                    popSendToPda.Show();
                }
                else
                {
                    DispMsg("Document has been already sent/processed by PDA !"); return;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        protected void lbtnSendToPDA_Click(object sender, EventArgs e)
        {
            try
            {
                SendAllDocToPda();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void lbtnSPDaClose_Click(object sender, EventArgs e)
        {
            popSendToPda.Hide();
            _showPopSendToPda = false;
        }
        #endregion

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkSelectAll = dgvPopPendingDoc.HeaderRow.FindControl("chkSelectAll") as CheckBox;
                foreach (GridViewRow row in dgvPopPendingDoc.Rows)
                {
                    CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                    chkSelect.Checked = chkSelectAll.Checked;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void SendAllDocToPda()
        {
            try
            {
                List<ReptPickHeader> _tmpPickHdrList = new List<ReptPickHeader>();
                List<ReptPickHeader> _allReadyPdaSend = new List<ReptPickHeader>();
                ReptPickHeader _tempPickHdr = new ReptPickHeader();
                if (ddlSendAllLoadingBay.SelectedIndex == 0 || ddlSendAllLoadingBay.SelectedValue == "0" || string.IsNullOrEmpty(ddlSendAllLoadingBay.SelectedValue))
                {
                    DispMsg("Please select the loading bay !"); return;
                }
                foreach (GridViewRow row in dgvPopPendingDoc.Rows)
                {
                    CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                    Label lblITR_REQ_NO = row.FindControl("lblITR_REQ_NO") as Label;
                    Label lblItr_tp = row.FindControl("lblItr_tp") as Label;
                    Label lblitr_direct = row.FindControl("lblitr_direct") as Label;
                    if (chkSelect.Checked)
                    {
                        _tempPickHdr = new ReptPickHeader();
                        _tempPickHdr.Tuh_doc_no = lblITR_REQ_NO.Text.Trim();
                        _tempPickHdr.Tuh_doc_tp = lblItr_tp.Text.Trim();
                        _tempPickHdr.Tuh_direct = Convert.ToInt32(lblitr_direct.Text)==1?true:false;
                        _tmpPickHdrList.Add(_tempPickHdr);
                    }
                }

                if (_tmpPickHdrList.Count < 1)
                {
                    DispMsg("Please select a document !"); return;
                }
               
                foreach (var item in _tmpPickHdrList)
                {
                    _tmpPickHdr = new ReptPickHeader();
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                        Tuh_doc_no = item.Tuh_doc_no,
                    }).FirstOrDefault();

                    if (_tmpPickHdr != null)
                    {
                        _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                _allReadyPdaSend.Add(item);
                                continue;
                            }
                        }
                        _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repItmList != null)
                        {
                            if (_repItmList.Count > 0)
                            {
                                _allReadyPdaSend.Add(item);
                                continue;
                            }
                        }

                    }
                }
                if (_allReadyPdaSend.Count > 0)
                {
                    DispMsg("Selected documents has already been sent to PDA or has already been processed !"); return;
                }
                #region Header
                int _res = 0;
                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                Int32 _userSeqNo = 0;
                foreach (var item in _tmpPickHdrList)
                {
                    InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() {
                        Itr_req_no = item.Tuh_doc_no,
                        Itr_stus = "A",
                        Itr_tp = item.Tuh_doc_tp
                    }).FirstOrDefault();
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_doc_no = item.Tuh_doc_no,
                        Tuh_doc_tp = item.Tuh_doc_tp,
                        Tuh_direct = item.Tuh_direct,
                        Tuh_usr_com = Session["UserCompanyCode"].ToString()
                    }).FirstOrDefault();
                    if (_tmpPickHdr == null)
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                        _userSeqNo = GenerateNewUserSeqNo(item.Tuh_doc_tp, item.Tuh_doc_no);
                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_doc_tp = item.Tuh_doc_tp;
                        _inputReptPickHeader.Tuh_direct = item.Tuh_direct;
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_doc_no = item.Tuh_doc_no;
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                       // _inputReptPickHeader.Tuh_wh_com = warehousecom;
                       // _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        MasterLocation _whLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        bool _isWhLoc = false;
                        if (_whLoc != null)
                        {
                            if (!string.IsNullOrEmpty(_whLoc.Ml_wh_com) && !string.IsNullOrEmpty(_whLoc.Ml_wh_cd))
                            {
                                _isWhLoc = true;
                            }
                        }
                        if (_isWhLoc)
                        {
                            _inputReptPickHeader.Tuh_wh_com = _whLoc.Ml_wh_com;
                            _inputReptPickHeader.Tuh_wh_loc = _whLoc.Ml_wh_cd;
                        }
                        else
                        {
                            DispMsg("Warehouse company data invalid "); return;
                        }
                        _inputReptPickHeader.Tuh_load_bay = ddlSendAllLoadingBay.SelectedValue;
                        _inputReptPickHeader.Tuh_is_take_res = true;
                        if (_reqData != null)
                        {
                            //_inputReptPickHeader.Tuh_rec_com = _reqData.Itr_com;
                            _inputReptPickHeader.Tuh_rec_loc = _reqData.Itr_rec_to;
                        }
                        _res = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
                    }
                    else
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                        _userSeqNo = _tmpPickHdr.Tuh_usrseq_no;
                        _inputReptPickHeader.Tuh_doc_no = item.Tuh_doc_no;
                        _inputReptPickHeader.Tuh_doc_tp = item.Tuh_doc_tp;
                        _inputReptPickHeader.Tuh_direct = item.Tuh_direct;
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                       // _inputReptPickHeader.Tuh_wh_com = warehousecom;
                       // _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        MasterLocation _whLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        bool _isWhLoc = false;
                        if (_whLoc != null)
                        {
                            if (!string.IsNullOrEmpty(_whLoc.Ml_wh_com) && !string.IsNullOrEmpty(_whLoc.Ml_wh_cd))
                            {
                                _isWhLoc = true;
                            }
                        }
                        if (_isWhLoc)
                        {
                            _inputReptPickHeader.Tuh_wh_com = _whLoc.Ml_wh_com;
                            _inputReptPickHeader.Tuh_wh_loc = _whLoc.Ml_wh_cd;
                        }
                        else
                        {
                            DispMsg("Warehouse company data invalid "); return;
                        }
                        _inputReptPickHeader.Tuh_load_bay = ddlSendAllLoadingBay.SelectedValue;
                        _inputReptPickHeader.Tuh_is_take_res = true;
                        if (_reqData != null)
                        {
                           // _inputReptPickHeader.Tuh_rec_com = _reqData.Itr_com;
                            _inputReptPickHeader.Tuh_rec_loc = _reqData.Itr_rec_to;
                        }
                        _res = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);
                    }
                    if (_res < 1)
                    {
                        DispMsg("Error Occurred while processing !", "E"); return;
                    }
                    else
                    {
                        _res = 0;
                        List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                        InventoryRequest _invReq = new InventoryRequest();
                        _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() { Itr_req_no=item.Tuh_doc_no,Itr_stus="A", Itr_tp=item.Tuh_doc_tp }).FirstOrDefault();
                        if (_invReq!=null)
                        {
                            if (_invReq.Itr_seq_no>0)
                            {
                                List<InventoryRequestItem> _invReqItm = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA(new InventoryRequestItem() { Itri_seq_no=_invReq.Itr_seq_no });
                                if (_invReqItm != null)
                                {
                                    foreach (var _rItm in _invReqItm)
                                    {
                                        if (_rItm.Itri_bqty > 0)
                                        {
                                            ReptPickItems _reptitm = new ReptPickItems();
                                            _reptitm.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                                            _reptitm.Tui_req_itm_qty = _rItm.Itri_bqty;
                                            _reptitm.Tui_req_itm_cd = _rItm.Itri_itm_cd;
                                            _reptitm.Tui_req_itm_stus = _rItm.Itri_itm_stus;
                                            _reptitm.Tui_pic_itm_cd = item.Tuh_doc_tp == "SOA" ? Convert.ToString(_rItm.Itri_base_req_line) : Convert.ToString(_rItm.Itri_line_no);//Darshana request add by rukshan
                                            _saveonly.Add(_reptitm);
                                        }
                                    }
                                    _res = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                                }
                            }
                        }
                    }
                    DispMsg("Document sent to PDA Successfully !", "S");
                    _showPopSendToPda = false;
                    popSendToPda.Hide();
                    dgvPopPendingDoc.DataSource = new int[] { };
                    dgvPopPendingDoc.DataBind();
                    //ddlSendAllLoadingBay.SelectedIndex = 0;
                }
                #endregion
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }



        protected void btncontinue_Click(object sender, EventArgs e)
        {
            if (isAnyDetailsSelected())
            {
                for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                {
                    GridViewRow dr = dgvRequestItems.Rows[i];
                    CheckBox chkDetail = dr.FindControl("chkDetail") as CheckBox;
                    if (chkDetail.Checked)
                    {
                        Label lblItri_seq_no = dr.FindControl("lblItri_seq_no") as Label;
                        Label lblItri_line_no = dr.FindControl("lblItri_line_no") as Label;
                        Label lblItri_itm_cd = dr.FindControl("lblItri_itm_cd") as Label;
                        string lblItri_itm_stus = ddlStatus.SelectedValue;
                        oLocItems.AddRange(CHNLSVC.Inventory.GET_LOC_ITEMS_FOR_DISPATCH(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), lblItri_itm_cd.Text.Trim(), lblItri_itm_stus.Trim()));
                    }
                }

                if (oLocItems.Count > 0)
                {
                    var Locs = oLocItems.Select(x => new { x.Inl_loc, x.Ml_loc_desc }).Distinct().ToList();
                    if (Locs != null)
                    {
                        ddlIsseLocation.DataSource = Locs;
                        ddlIsseLocation.DataTextField = "Ml_loc_desc";
                        ddlIsseLocation.DataValueField = "Inl_loc";
                        ddlIsseLocation.DataBind();
                    }

                    var ItemStatus = oLocItems.Select(x => new { x.Inl_itm_stus, x.Mis_desc }).Distinct().ToList();
                    if (ItemStatus != null)
                    {
                        ddlIssueItemStatus.DataSource = ItemStatus;
                        ddlIssueItemStatus.DataTextField = "Mis_desc";
                        ddlIssueItemStatus.DataValueField = "Inl_itm_stus";
                        ddlIssueItemStatus.DataBind();
                    }
                }
                AddSummery();
                // mpApproveAll.Show();
            }
            else
            {
                DisplayMessage("Please select the requested items to approve", 1);
                return;
            }
        }


        private void AddSummery()
        {
            List<InventoryRequestItem> _request = new List<InventoryRequestItem>();
            _request = ViewState["oRequesItems"] as List<InventoryRequestItem>;
            List<InventoryRequest> oHeaders = new List<InventoryRequest>();

            for (int hi = 0; hi < grdsummery.Rows.Count; hi++)
            {
                GridViewRow drHeader = grdsummery.Rows[hi];
                CheckBox chkHeadertemp = drHeader.FindControl("chkHeadersummerys") as CheckBox;
                Label Item = drHeader.FindControl("col_Itri_itm_cd") as Label;
                Label qtys = drHeader.FindControl("col_Qty") as Label;
                if (chkHeadertemp.Checked)
                {
                    var _selectitem = _request.Where(x => x.Itri_itm_cd == Item.Text).ToList();
                    txtAmount.Text = qtys.Text;
                    btnAddQty_Click(null, null);

                }
            }
        }

        private void AddApproveitem(string com,string _loc,string _itm,string _status,string _des,decimal _qty,decimal _free,decimal _res)
        {
            try
            {
                //InventoryRequestItem> oAppredItems
                if (hdfISValiedNumber.Value == "0")
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtAmount.Text))
                {
                    DisplayMessage("Please enter valid quantity.");
                    return;
                }

                if (IndexHeader != -1 && IndexDetails != -1 && IndexStock != -1)
                {
                    GridViewRow drHeader = dgvRequestHeader.Rows[IndexHeader];
                    GridViewRow drDetail = dgvRequestItems.Rows[IndexDetails];
                    GridViewRow drStock = dgvCurrentStockDetails.Rows[IndexStock];

                    Label lblITR_REQ_NO = drHeader.FindControl("lblITR_REQ_NO") as Label;

                    //Current Stock grid
                    Label lblINL_COM = drStock.FindControl("lblINL_COM") as Label;
                    Label lblINL_LOC = drStock.FindControl("lblINL_LOC") as Label;
                    Label lblINL_ITM_CD = drStock.FindControl("lblINL_ITM_CD") as Label;
                    Label lblINL_ITM_STUS = drStock.FindControl("lblINL_ITM_STUS") as Label;
                    Label lblMIS_DESC = drStock.FindControl("lblMIS_DESC") as Label;
                    Label lblINL_QTY = drStock.FindControl("lblINL_QTY") as Label;
                    Label lblinl_res_qty = drStock.FindControl("lblinl_res_qty") as Label;
                    Label lblINL_FREE_QTY = drStock.FindControl("lblINL_FREE_QTY") as Label;

                    // Request Details grid
                    Label lblItri_app_qty = drDetail.FindControl("lblItri_app_qty") as Label;
                    Label lblItri_bqty = drDetail.FindControl("lblItri_bqty") as Label;
                    Label lblItri_seq_no = drDetail.FindControl("lblItri_seq_no") as Label;
                    Label lblItri_line_no = drDetail.FindControl("lblItri_line_no") as Label;
                    Label lblItri_res_no = drDetail.FindControl("lblItri_res_no") as Label;

                    Int32 DetailSeq = Convert.ToInt32(lblItri_seq_no.Text);
                    Int32 DetailLine = Convert.ToInt32(lblItri_line_no.Text);


                    decimal QtyNewAmount = Convert.ToDecimal(txtAmount.Text);

                    decimal QtyApp = (lblItri_app_qty.Text == "") ? 0 : Convert.ToDecimal(lblItri_app_qty.Text);
                    decimal QtyBal = (lblItri_bqty.Text == "") ? 0 : Convert.ToDecimal(lblItri_bqty.Text);
                    decimal QtyFreeStock = (lblINL_FREE_QTY.Text == "") ? 0 : Convert.ToDecimal(lblINL_FREE_QTY.Text);
                    decimal QtyResve = (lblinl_res_qty.Text == "") ? 0 : Convert.ToDecimal(lblinl_res_qty.Text);

                    if (QtyApp == 0)
                    {
                        DisplayMessage("You have not selected any approved quantity ");
                        return;
                    }
                    if (QtyBal == 0)
                    {
                        DisplayMessage("You dont have balance quantity");
                        return;
                    }
                    if ((lblItri_res_no.Text != "") && (lblItri_res_no.Text != "N/A"))
                    {
                        if (QtyResve < QtyNewAmount)
                        {
                            DisplayMessage("Selected Item is reserved.");
                            txtAmount.Text = QtyBal.ToString();
                            txtAmount.Focus();
                            return;
                        }
                        //check res items
                        DataTable chkqty = CHNLSVC.Inventory.SP_GETRESQTYMRN(lblINL_COM.Text.ToString(), lblINL_LOC.Text.ToString(), lblINL_ITM_CD.Text.ToString(), lblINL_ITM_STUS.Text.ToString());
                    }
                    else
                    {
                        if (QtyFreeStock <= 0)
                        {
                            DisplayMessage("You dont have free quantity in " + lblINL_LOC.Text + " location");
                            return;
                        }
                        if (QtyFreeStock < QtyNewAmount)
                        {
                            //DisplayMessage("Location : " + lblINL_LOC.Text + " dont have " + QtyNewAmount.ToString("N2"));
                            DisplayMessage("Please check the inventory balance ");
                            txtAmount.Text = QtyFreeStock.ToString();
                            txtAmount.Focus();
                            return;
                        }
                        if (QtyBal < QtyNewAmount)
                        {
                            DisplayMessage("Cant exceed balance Qty");
                            txtAmount.Text = QtyBal.ToString();
                            txtAmount.Focus();
                            return;
                        }
                    }



                    if (oAppredItems.FindAll(x => x.Itri_line_no == DetailLine && x.Itri_loc == lblINL_LOC.Text.Trim() && x.Itri_itm_cd == lblINL_ITM_CD.Text && x.Itri_itm_stus == lblINL_ITM_STUS.Text.Trim()).Count > 0)
                    {
                        InventoryRequestItem oOldItem = oAppredItems.Find(x => x.Itri_loc == lblINL_LOC.Text.Trim() && x.Itri_itm_cd == lblINL_ITM_CD.Text && x.Itri_itm_stus == lblINL_ITM_STUS.Text.Trim());
                        oOldItem.Itri_bqty = oOldItem.Itri_bqty + QtyNewAmount;
                        oOldItem.Itri_qty = oOldItem.Itri_qty + QtyNewAmount;
                        oOldItem.Itri_app_qty = oOldItem.Itri_app_qty + QtyNewAmount;
                    }
                    else
                    {
                        InventoryRequestItem oNewItem = new InventoryRequestItem();
                        oNewItem.Itri_loc = lblINL_LOC.Text.Trim();
                        oNewItem.Itri_itm_cd = lblINL_ITM_CD.Text.Trim();
                        oNewItem.Itri_itm_stus = lblINL_ITM_STUS.Text.Trim();
                        oNewItem.Mis_desc = lblMIS_DESC.Text.Trim();
                        oNewItem.Itri_bqty = QtyNewAmount;
                        oNewItem.Itri_qty = QtyNewAmount;
                        oNewItem.Itri_app_qty = QtyNewAmount;
                        oNewItem.Itri_res_no = lblItri_res_no.Text;
                        oNewItem.Itr_req_no = lblITR_REQ_NO.Text;
                        oNewItem.Itri_base_req_line = DetailLine;
                        oAppredItems.Add(oNewItem);
                    }

                    dgvApprovedItems.DataSource = oAppredItems;
                    dgvApprovedItems.DataBind();

                    InventoryRequestItem oSelectDetail = oRequesItems.Find(x => x.Itri_seq_no == DetailSeq && x.Itri_line_no == DetailLine);
                    oSelectDetail.Itri_bqty = oSelectDetail.Itri_bqty - QtyNewAmount;

                    dgvRequestItems.DataSource = oRequesItems;
                    dgvRequestItems.DataBind();

                    for (int i = 0; i < dgvRequestItems.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvRequestItems.Rows[i];
                        dr.BackColor = Color.Transparent;
                    }

                    GridViewRow drSelect = dgvRequestItems.Rows[IndexDetails];
                    drSelect.BackColor = Color.LightCyan;

                    txtAmount.Text = "";
                    txtAmount.Focus();

                    InventoryLocation oSelectedStock = oLocItems.Find(x => x.Inl_com == lblINL_COM.Text && x.Inl_loc == lblINL_LOC.Text && x.Inl_itm_cd == lblINL_ITM_CD.Text);
                    if ((lblItri_res_no.Text != "") && (lblItri_res_no.Text != "N/A"))
                    {
                        //oSelectedStock.Inl_res_qty = oSelectedStock.Inl_res_qty - QtyNewAmount;
                        Session["ISRESNO"] = "TRUE";
                    }
                    else
                    {
                        oSelectedStock.Inl_res_qty = oSelectedStock.Inl_res_qty + QtyNewAmount;
                        //oSelectedStock.Inl_res_qty = oSelectedStock.Inl_res_qty - QtyNewAmount;

                        oSelectedStock.Inl_free_qty = oSelectedStock.Inl_free_qty - QtyNewAmount;
                        //oSelectedStock.Inl_free_qty = oSelectedStock.Inl_free_qty + QtyNewAmount;
                    }



                    oLocItems = oLocItems.OrderBy(c => c.Inl_loc).ToList();

                    dgvCurrentStockDetails.DataSource = oLocItems;
                    dgvCurrentStockDetails.DataBind();

                    for (int i = 0; i < dgvCurrentStockDetails.Rows.Count; i++)
                    {
                        GridViewRow dr = dgvCurrentStockDetails.Rows[i];
                        dr.BackColor = Color.Transparent;
                    }

                    drSelect = dgvCurrentStockDetails.Rows[IndexStock];
                    drSelect.BackColor = Color.LightCyan;

                    if (oSelectedStock.Inl_free_qty == 0 && oSelectDetail.Itri_bqty > 0)
                    {
                        IndexStock = IndexStock + 1;
                        if (dgvCurrentStockDetails.Rows.Count == IndexStock + 1)
                        {
                        }
                        else
                        {
                            selectStockItem(IndexStock, true);
                        }
                    }

                    if (oSelectDetail.Itri_bqty == 0)
                    {
                        IndexDetails = IndexDetails + 1;
                        if (dgvRequestItems.Rows.Count == IndexDetails)
                        {
                            //Selected Request is complete
                            DisplayMessage("All items are approved");
                        }
                        else
                        {
                            selectItem(IndexDetails, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }


        protected void btnSentScan_Click(object sender, EventArgs e)
        {
            SaveDataPick();
            string docuno = "";
            string doctp = "";
            string direc = "";
            string userseq = "";
            string sesstionid = Session["SessionID"].ToString();
            string userid = Session["UserID"].ToString();
            string company=Session["UserCompanyCode"].ToString();
            string location =Session["UserDefLoca"].ToString();
            if (Session["UserCompanyCode"].ToString() == "AAL" || (Session["UserID"].ToString()=="ASANKA" ||  Session["UserID"].ToString()=="1800090" ||  Session["UserID"].ToString()=="ADMIN"))
            {
                for (int i = 0; i < dgvRequestHeader.Rows.Count; i++)
                {

                    GridViewRow dr = dgvRequestHeader.Rows[i];

                    CheckBox _chk = dr.FindControl("chkHeader") as CheckBox;
                    if (_chk.Checked)
                    {
                        Label type = dr.FindControl("lblItr_tp") as Label;
                        Label docno = dr.FindControl("lblITR_REQ_NO") as Label;
                        Label direction = dr.FindControl("lblitr_direct") as Label;
                        docuno = docno.Text;
                        doctp = type.Text;
                        if (direction.Text == "0")
                        {
                            direc = "OUT";
                        }
                        else
                        {
                            direc = "IN";
                        }
                    }
                }
                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocAvailable(company, docuno, location, null, null, null);

                //string []  _domuserarr = Convert.ToString(WindowsIdentity.GetCurrent().Name).Split('\\');
                //string _domuser = _domuserarr[1];
                //Process p = new Process();
                //p.StartInfo.WorkingDirectory = "C:\\Users\\"+_domuser+"\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Supply Chain Management System";
                //p.StartInfo.FileName = "Fast Forward - Serial Picker";
                //p.StartInfo.Arguments = "";
                //p.Start();
                //p.WaitForExit();
                if (dtdoccheck.Rows.Count > 0)
                {
                    try
                    {
                        string str_Path = @"C:\Users\nuwanc\Desktop\note.bat";

                        ProcessStartInfo processInfo = new ProcessStartInfo(str_Path);

                        processInfo.UseShellExecute = false;

                        Process batchProcess = new Process();

                        batchProcess.StartInfo = processInfo;
                        
                        batchProcess.Start();
                        //userseq = dtdoccheck.Rows[0]["tuh_usrseq_no"].ToString();
                        //Process p = new Process();
                        ////p.StartInfo.WorkingDirectory = @"D:\Collection\FastForwardERP.Project\UtilityApp\FastForward.Scaning\FastForward.Scaning\bin\Debug";
                        //p.StartInfo.WorkingDirectory = @"D:\Collection\FastForwardERP.Project\UtilityApp\FastForward.Scaning\FastForward.Scaning\bin\Debug";
                        //p.StartInfo.FileName = "FastForward.Scaning.exe";
                        ////p.StartInfo.FileName = "FastForward.Scaning.application";
                        //p.StartInfo.Arguments = doctp + "," + docuno + "," + userseq + "," + company + "," + location + "," + direc + "," + sesstionid + "," + userid;
                        ////p.StartInfo.Arguments = "SOA DPS32-SOA-17-02053 DPS32";
                        //p.Start();
                        //p.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.Message.ToString());
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Invalid document details.");
                    return;
                }


            }
        }

        private void SaveDataPick()
        {
            Int32 val = 0;
            string warehousecom = string.Empty;
            string warehouseloc = string.Empty;
   
          
            // Int32 _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "GRN", 1, Session["UserCompanyCode"].ToString());

            oRequesItems = ViewState["oRequesItems"] as List<InventoryRequestItem>;
            for (int i = 0; i < dgvRequestHeader.Rows.Count; i++)
            {

                GridViewRow dr = dgvRequestHeader.Rows[i];

                CheckBox _chk = dr.FindControl("chkHeader") as CheckBox;
                if (_chk.Checked)
                {
                    Label type = dr.FindControl("lblItr_tp") as Label;
                    Label docno = dr.FindControl("lblITR_REQ_NO") as Label;
                    Label direction = dr.FindControl("lblitr_direct") as Label;

                    #region Add by Lakshan to chk doc already send or not 01 Oct 2016
                    if (oAppredItems.Count < 1)
                    {
                        DispMsg("Approve items not found !"); return;
                    }
                    _tmpPickHdr = new ReptPickHeader();
                    bool _docAva = false;
                    _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                        Tuh_doc_no = docno.Text
                        // Tuh_direct = Convert.ToBoolean(direction.Text)
                    }).FirstOrDefault();
                    if (_tmpPickHdr != null)
                    {
                        _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                _docAva = true;
                            }
                        }
                        _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repItmList != null)
                        {
                            if (_repItmList.Count > 0)
                            {
                                _docAva = true;
                            }
                        }

                    }
                    if (_docAva)
                    {
                        //string _msg = "Document has already sent to PDA or has alread processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay :" + _tmpPickHdr.Tuh_load_bay ;
                        string _msg = "Document has already been sent to PDA or has already been processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay " + _tmpPickHdr.Tuh_load_bay;
                        DisplayMessage(_msg);
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                        return;
                    }
                    #endregion

                    /*11 Oct 2016 Add by Lakshan */
                    InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest()
                    {
                        Itr_req_no = docno.Text,
                        Itr_stus = "A",
                        Itr_tp = type.Text
                    }).FirstOrDefault();

                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(type.Text, Session["UserCompanyCode"].ToString(), docno.Text, Convert.ToInt32(direction.Text));
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo(type.Text, docno.Text);
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_doc_tp = type.Text;
                        int dir = Convert.ToInt32(direction.Text);
                        _inputReptPickHeader.Tuh_direct = Convert.ToBoolean(dir);
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_doc_no = docno.Text;
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_wh_com = warehousecom;
                       _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                       // _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                        _inputReptPickHeader.Tuh_is_take_res = true;
                        if (_reqData != null)
                        {
                            _inputReptPickHeader.Tuh_rec_loc = _reqData.Itr_rec_to;
                        }
                        val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                        if (val == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            CHNLSVC.CloseChannel();
                            return;
                        }
                    }
                    else
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_doc_no = docno.Text;
                        _inputReptPickHeader.Tuh_doc_tp = type.Text;
                        int direc = Convert.ToInt32(direction.Text);
                        _inputReptPickHeader.Tuh_direct = Convert.ToBoolean(direc);
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_wh_com = warehousecom;
                        _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                        _inputReptPickHeader.Tuh_is_take_res = true;
                        val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                        if (Convert.ToInt32(val) == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            CHNLSVC.CloseChannel();
                            return;
                        }
                    }


                    DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(user_seq_num);

                    if (dtchkitm.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                        return;
                    }
                    else
                    {
                        if (oAppredItems.Count > 0)
                        {
                            List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                            var _filteritem = oAppredItems.Where(x => x.Itr_req_no == docno.Text).ToList();
                            if (_filteritem.Count > 0)
                            {
                                foreach (InventoryRequestItem _row in _filteritem)
                                {

                                    //AddItem(_item, _cost, null, null, user_seq_num.ToString(), null);
                                    if (_row.Itr_req_no == docno.Text)
                                    {
                                        ReptPickItems _reptitm = new ReptPickItems();
                                        _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                                        _reptitm.Tui_req_itm_qty = _row.Itri_bqty;
                                        _reptitm.Tui_req_itm_cd = _row.Itri_itm_cd;
                                        _reptitm.Tui_req_itm_stus = _row.Itri_itm_stus;
                                        _reptitm.Tui_pic_itm_cd = type.Text == "SOA" ? Convert.ToString(_row.Itri_base_req_line) : Convert.ToString(_row.Itri_line_no);//Darshana request add by rukshan
                                        // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                                        // _reptitm.Tui_pic_itm_qty = _row.Itri_bqty;
                                        _saveonly.Add(_reptitm);
                                    }

                                }
                                val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                            }
                        }

                    }

                }

                if (val == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                    MPPDA.Hide();
                    chkpda.Checked = false;
                }
            }

        }

        protected void lbtnPopOk_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, null); 
        }

        protected void lbtnPopNo_Click(object sender, EventArgs e)
        {

        }

        protected void btnItemChange_Click(object sender, EventArgs e)
        {
            try
            {
                bool b10159 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10159);
                if (!b10159)
                {
                    DispMsg("Sorry, You have no permission! ( Advice: Required permission code : 10159)"); return;
                }
                GridViewRow _row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblITR_REQ_NO = _row.FindControl("lblITR_REQ_NO") as Label;
                txtItmChgReqNo.Text = lblITR_REQ_NO.Text.ToUpper().Trim();
                txtItmChgReqNo_TextChanged(null, null);
                _showItmChg = true;
                popItemChg.Show();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void lbtnItmCdChgCls_Click(object sender, EventArgs e)
        {
            popItemChg.Hide();
            _showItmChg = false;
        }

        protected void txtItmChgReqNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvItmChg.DataSource = new int[] {};
                _itmChgReqList = new List<InventoryRequestItem>();
                if (!string.IsNullOrEmpty(txtItmChgReqNo.Text))
                {
                    _itmChgReqList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA_BY_REQ_NO(txtItmChgReqNo.Text.Trim().ToUpper());
                    if (_itmChgReqList.Count>0)
                    {
                        dgvItmChg.DataSource = _itmChgReqList;
                    }
                }
                dgvItmChg.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnitri_itm_cd_Click(object sender, EventArgs e)
        {
            try
            {
                _chgItmCdLine = 0;
                 GridViewRow _row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblitri_line_no = _row.FindControl("lblitri_line_no") as Label;
                _chgItmCdLine = Convert.ToInt32(lblitri_line_no.Text);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.GetItemforBoqMrn(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "chgItmcd";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearch.Show(); Session["SY"] = "Y";
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void btnSaveItmChg_Click(object sender, EventArgs e)
        {
            try
            {
                mpSearch.Hide();
                Session["SY"] = null;
                bool b10159 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10159);
                if (!b10159)
                {
                    DispMsg("Sorry, You have no permission! ( Advice: Required permission code : 10159)"); return;
                }
                #region validation
                if (string.IsNullOrEmpty(txtItmChgReqNo.Text))
                {
                    DispMsg("Please select the request no !"); return;
                }
                if (_itmChgReqList.Count == 0)
                {
                    DispMsg("Request items not found !"); return;
                }
                var _chgList = _itmChgReqList.Where(c => !string.IsNullOrEmpty(c.Itri_old_itm_cd)).ToList();
                if (_chgList == null)
                {
                    DispMsg("Request items not found !"); return;
                }
                if (_chgList.Count==0)
                {
                    DispMsg("Request items not found !"); return;
                }
                #endregion
                string _err="";
                foreach (var item in _chgList)
                {
                    item.Itr_req_no = txtItmChgReqNo.Text.Trim().ToUpper();
                    item.Itri_itm_mod_by = Session["UserID"].ToString();
                    item.Itri_itm_mod_session = Session["SessionID"].ToString();
                }
                Int32 _res = CHNLSVC.Inventory.UpdateMrnItemCode(_chgList, out _err);
                if (string.IsNullOrEmpty(_err))
                {
                    DispMsg("Successfully Saved !","S");
                    _itmChgReqList = new List<InventoryRequestItem>();
                    txtItmChgReqNo.Text = "";
                    _showItmChg = false;
                    popItemChg.Hide();
                    mpSearch.Hide();
                    Session["SY"] = null;
                    btnSearch_Click(null,null);
                }
                else
                {
                    DispMsg(_err, "E");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        //dilshan
        protected void lbtngrdDOItemstEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    if (oRequesItems !=null)
                    {
                        dgvApprovedItems.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                        dgvApprovedItems.DataSource = oRequesItems;
                        dgvApprovedItems.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        //dilshan
        protected void lbtngrdDOItemstUpdate_Click(object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            Label lblitri_line_no = row.FindControl("lblitri_line_no") as Label;
            TextBox txtlblitri_app_qty = row.FindControl("txtlblitri_app_qty") as TextBox;
            Label lblitri_app_qty1 = row.FindControl("lblitri_app_qty1") as Label;
            Label lblitri_itm_cd = row.FindControl("lblitri_itm_cd") as Label;
            MasterItem _mstItm = CHNLSVC.General.GetItemMaster(lblitri_itm_cd.Text);


            //Validat
            Int32 _line = Convert.ToInt32(lblitri_line_no.Text);
            decimal _appQty = Convert.ToDecimal(txtlblitri_app_qty.Text);
            decimal _exQty = Convert.ToDecimal(lblitri_app_qty1.Text);

            //--------
            string value = (row.FindControl("txtlblitri_app_qty") as TextBox).Text;
            decimal number;
            decimal dec;
            if (Decimal.TryParse(value, out number))
            {
                dec = (decimal)number;
                if ((dec % 1) > 0)
                {
                    if (_mstItm.Mi_is_ser1 == -1)
                    {
                        if (row != null)
                        {
                            decimal _Rqty = Convert.ToDecimal((row.FindControl("txtlblitri_app_qty") as TextBox).Text);
                            //decimal _qty = Convert.ToDecimal(lblitri_app_qty1);
                            decimal _qty = Convert.ToDecimal((row.FindControl("lblitri_app_qty1") as Label).Text);

                            if (_Rqty > _qty)
                            {
                                //dgvApprovedItems.AutoGenerateColumns = false;
                                //dgvApprovedItems.DataSource = null;
                                //dgvApprovedItems.DataBind();
                                var _selectedRow = oRequesItems.Where(c => c.Itri_line_no == _line).FirstOrDefault();
                                if (_selectedRow != null)
                                {
                                    _selectedRow.Itri_app_qty = _exQty;
                                    dgvApprovedItems.EditIndex = -1;
                                    //  dgvApprovedItems.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                                    dgvApprovedItems.DataSource = oRequesItems;
                                    dgvApprovedItems.DataBind();
                                }
                                string _Msg = "Cannot exceed approved qty. Please select the document again.";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                return;
                            }
                            else
                            {
                                //if (oRequesItems != null)
                                //{
                                //    foreach (InventoryRequestItem _item in oRequesItems)
                                //    {
                                //        //{
                                //        //    _item.Itri_app_qty = _Rqty;
                                //        //}

                                //    }
                                //    for (int i = 0; i < dgvApprovedItems.Rows.Count; i++)
                                //    {
                                //        TextBox tb = ((TextBox)dgvApprovedItems.Rows[i].FindControl("txtlblitri_app_qty"));
                                //        tb.Enabled = true;
                                //    }
                                //}
                                //oAppredItems = oRequesItems;
                                //ViewState["oRequesItems"] = oRequesItems;
                            }

                        }
                        }
                        else
                        {
                            //dgvApprovedItems.AutoGenerateColumns = false;
                            //dgvApprovedItems.DataSource = null;
                            //dgvApprovedItems.DataBind();
                            var _selectedRow = oRequesItems.Where(c => c.Itri_line_no == _line).FirstOrDefault();
                            if (_selectedRow != null)
                            {
                                _selectedRow.Itri_app_qty = _exQty;
                                dgvApprovedItems.EditIndex = -1;
                                //  dgvApprovedItems.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                                dgvApprovedItems.DataSource = oRequesItems;
                                dgvApprovedItems.DataBind();
                            }
                            string _Msg = "Cannot approve decimal value for this item. Please select the document again.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                    }
                else
                    {

                        if (row != null)
                        {
                            decimal _Rqty = Convert.ToDecimal((row.FindControl("txtlblitri_app_qty") as TextBox).Text);
                            //decimal _qty = Convert.ToDecimal(lblitri_app_qty1);
                            decimal _qty = Convert.ToDecimal((row.FindControl("lblitri_app_qty1") as Label).Text);


                            if (_Rqty > _qty)
                            {
                                //dgvApprovedItems.AutoGenerateColumns = false;
                                //dgvApprovedItems.DataSource = null;
                                //dgvApprovedItems.DataBind();
                                var _selectedRow = oRequesItems.Where(c => c.Itri_line_no == _line).FirstOrDefault();
                                if (_selectedRow != null)
                                {
                                    _selectedRow.Itri_app_qty = _exQty;
                                    dgvApprovedItems.EditIndex = -1;
                                    //  dgvApprovedItems.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                                    dgvApprovedItems.DataSource = oRequesItems;
                                    dgvApprovedItems.DataBind();
                                }
                                string _Msg = "Cannot exceed approved qty. Please select the document again.";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                return;
                            }
                            else
                            {
                                //if (oRequesItems != null)
                                //{
                                //    foreach (InventoryRequestItem _item in oRequesItems)
                                //    {
                                //        //{
                                //        //    _item.Itri_app_qty = _Rqty;
                                //        //}

                                //    }
                                //    for (int i = 0; i < dgvApprovedItems.Rows.Count; i++)
                                //    {
                                //        TextBox tb = ((TextBox)dgvApprovedItems.Rows[i].FindControl("txtlblitri_app_qty"));
                                //        tb.Enabled = true;
                                //    }
                                //}
                                //oAppredItems = oRequesItems;
                                //ViewState["oRequesItems"] = oRequesItems;
                            }

                        }
                    }
                }
                else
                {
                    //dgvApprovedItems.AutoGenerateColumns = false;
                    //dgvApprovedItems.DataSource = null;
                    //dgvApprovedItems.DataBind();
                    var _selectedRow = oRequesItems.Where(c => c.Itri_line_no == _line).FirstOrDefault();
                    if (_selectedRow != null)
                    {
                        _selectedRow.Itri_app_qty = _exQty;
                        dgvApprovedItems.EditIndex = -1;
                        //  dgvApprovedItems.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                        dgvApprovedItems.DataSource = oRequesItems;
                        dgvApprovedItems.DataBind();
                    }
                    string _Msg = "Please enter the valid quantity. Please select the document again.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }

            //--------
            if (oRequesItems != null)
            {
                var _selectedRow = oRequesItems.Where(c => c.Itri_line_no == _line).FirstOrDefault();
                if (_selectedRow!=null)
                {
                    _selectedRow.Itri_app_qty = _appQty;
                    dgvApprovedItems.EditIndex = -1;
                  //  dgvApprovedItems.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                    dgvApprovedItems.DataSource = oRequesItems;
                    dgvApprovedItems.DataBind();
                }
            }
        }
        protected void lblitri_app_qty_TextChanged(object sender, EventArgs e)
        {
            if (dgvApprovedItems.Rows.Count == 0) return;
            var lb = (TextBox)sender;
            var row = (GridViewRow)lb.NamingContainer;

            decimal _qtydecimalchk = Convert.ToDecimal((row.FindControl("lblitri_app_qtydecimal") as Label).Text);

            if ((row.FindControl("lblitri_app_qty") as TextBox).Text == "")
            {
                //DispMsg("Please enter the quantity"); 
                dgvApprovedItems.AutoGenerateColumns = false;
                dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                dgvApprovedItems.DataBind();
                string _Msg = "Please enter the quantity";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                return;
            }
            else
            {
                string value = (row.FindControl("lblitri_app_qty") as TextBox).Text;
                decimal number;
                decimal dec;
                if (Decimal.TryParse(value, out number))
                {
                    //Console.WriteLine(number);
                    dec = (decimal)number;
                    if ((dec % 1) > 0)
                    {
                        //is decimal
                        if (_qtydecimalchk == -1)
                        {
                            if (row != null)
                            {
                                decimal _Rqty = Convert.ToDecimal((row.FindControl("lblitri_app_qty") as TextBox).Text);
                                decimal _qty = Convert.ToDecimal((row.FindControl("lblitri_app_qty1") as Label).Text);

                                //string _Itm = (row.FindControl("col_invItem") as Label).Text;
                                //string _line = (row.FindControl("col_invLine") as Label).Text;

                                if (_Rqty > _qty)
                                {
                                    //_Rqty = 0;
                                    dgvApprovedItems.AutoGenerateColumns = false;
                                    //dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                                    //dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                                    //dgvApprovedItems.DataBind();
                                    dgvApprovedItems.DataSource = null;
                                    dgvApprovedItems.DataBind();
                                    string _Msg = "Cannot exceed approved qty. Please select the document again.";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                }
                                else
                                {
                                    //    _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
                                    //    if (_doitemserials.Count > 0)
                                    //    {
                                    //        decimal _Rqty2 = Convert.ToDecimal((row.FindControl("col_invRevQty") as TextBox).Text);
                                    //        int itmcount = _doitemserials.Where(x => x.Tus_itm_cd == _Itm && x.Tus_itm_line == Convert.ToDecimal(_line)).Count();
                                    //        if (itmcount > 0)
                                    //        {
                                    //            List<ReptPickSerials> _reversItm = new List<ReptPickSerials>();
                                    //            _reversItm = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
                                    //            if (_reversItm != null)
                                    //            {
                                    //                int itmrevcount = _reversItm.Where(x => x.Tus_itm_cd == _Itm).Count();
                                    //                if (itmrevcount != _Rqty2)
                                    //                {
                                    //                    _Rqty = itmrevcount;
                                    //                    string _Msg = "Cannot change qty";
                                    //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                    //                }
                                    //            }
                                    //            else
                                    //            {
                                    //                decimal count = _doitemserials.Where(x => x.Tus_itm_cd == _Itm).Count();

                                    //                decimal focqty = _qty - count;
                                    //                if (focqty < _Rqty)
                                    //                {
                                    //                    _Rqty = 0;
                                    //                    string _Msg = "Cannot exceed qty";
                                    //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                                    //                }
                                    //            }

                                    //        }
                                    //    }
                                    //oRequesItems = ViewState["oRequesItems"] as List<InventoryRequestItem>;
                                    if (oRequesItems != null)
                                    {
                                        foreach (InventoryRequestItem _item in oRequesItems)
                                        {
                                            //if ((_item.Sad_itm_cd == _Itm) && (_item.Sad_itm_line == Convert.ToDecimal(_line)))
                                            {
                                                _item.Itri_app_qty = _Rqty;
                                            }

                                        }
                                        for (int i = 0; i < dgvApprovedItems.Rows.Count; i++)
                                        {
                                            TextBox tb = ((TextBox)dgvApprovedItems.Rows[i].FindControl("lblitri_app_qty"));
                                            tb.Enabled = true;
                                        }
                                    }
                                    oAppredItems = oRequesItems;
                                    //dgvApprovedItems.AutoGenerateColumns = false;
                                    //dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                                    //dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                                    //dgvApprovedItems.DataBind();
                                    ViewState["oRequesItems"] = oRequesItems;

                                    //dgvApprovedItems.AutoGenerateColumns = false;
                                    //dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                                    //dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                                    //dgvApprovedItems.DataBind();
                                    //protected List<InventoryRequestItem> oRequesItems { get { return (List<InventoryRequestItem>)Session["oRequesItems"]; } set { Session["oRequesItems"] = value; } }

                                }

                            }
                        }
                        else
                        {
                            dgvApprovedItems.AutoGenerateColumns = false;
                            //dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                            //dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                            //dgvApprovedItems.DataBind();
                            dgvApprovedItems.DataSource = null;
                            dgvApprovedItems.DataBind();
                            string _Msg = "Cannot approve decimal value for this item. Please select the document again.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            return;
                        }
                    }
                    else
                    {
                        //is int
                        if (row != null)
                        {
                            decimal _Rqty = Convert.ToDecimal((row.FindControl("lblitri_app_qty") as TextBox).Text);
                            decimal _qty = Convert.ToDecimal((row.FindControl("lblitri_app_qty1") as Label).Text);

                            //string _Itm = (row.FindControl("col_invItem") as Label).Text;
                            //string _line = (row.FindControl("col_invLine") as Label).Text;

                            if (_Rqty > _qty)
                            {
                                //_Rqty = 0;
                                dgvApprovedItems.AutoGenerateColumns = false;
                                //dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                                //dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                                //dgvApprovedItems.DataBind();
                                dgvApprovedItems.DataSource = null;
                                dgvApprovedItems.DataBind();
                                string _Msg = "Cannot exceed approved qty. Please select the document again.";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                            }
                            else
                            {
                                //    _doitemserials = ViewState["_doitemserials"] as List<ReptPickSerials>;
                                //    if (_doitemserials.Count > 0)
                                //    {
                                //        decimal _Rqty2 = Convert.ToDecimal((row.FindControl("col_invRevQty") as TextBox).Text);
                                //        int itmcount = _doitemserials.Where(x => x.Tus_itm_cd == _Itm && x.Tus_itm_line == Convert.ToDecimal(_line)).Count();
                                //        if (itmcount > 0)
                                //        {
                                //            List<ReptPickSerials> _reversItm = new List<ReptPickSerials>();
                                //            _reversItm = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
                                //            if (_reversItm != null)
                                //            {
                                //                int itmrevcount = _reversItm.Where(x => x.Tus_itm_cd == _Itm).Count();
                                //                if (itmrevcount != _Rqty2)
                                //                {
                                //                    _Rqty = itmrevcount;
                                //                    string _Msg = "Cannot change qty";
                                //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                                //                }
                                //            }
                                //            else
                                //            {
                                //                decimal count = _doitemserials.Where(x => x.Tus_itm_cd == _Itm).Count();

                                //                decimal focqty = _qty - count;
                                //                if (focqty < _Rqty)
                                //                {
                                //                    _Rqty = 0;
                                //                    string _Msg = "Cannot exceed qty";
                                //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                                //                }
                                //            }

                                //        }
                                //    }
                                //oRequesItems = ViewState["oRequesItems"] as List<InventoryRequestItem>;
                                if (oRequesItems != null)
                                {
                                    foreach (InventoryRequestItem _item in oRequesItems)
                                    {
                                        //if ((_item.Sad_itm_cd == _Itm) && (_item.Sad_itm_line == Convert.ToDecimal(_line)))
                                        {
                                            _item.Itri_app_qty = _Rqty;
                                        }

                                    }
                                    for (int i = 0; i < dgvApprovedItems.Rows.Count; i++)
                                    {
                                        TextBox tb = ((TextBox)dgvApprovedItems.Rows[i].FindControl("lblitri_app_qty"));
                                        tb.Enabled = true;
                                    }
                                }
                                oAppredItems = oRequesItems;
                                //dgvApprovedItems.AutoGenerateColumns = false;
                                //dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                                //dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                                //dgvApprovedItems.DataBind();
                                ViewState["oRequesItems"] = oRequesItems;

                                //dgvApprovedItems.AutoGenerateColumns = false;
                                //dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                                //dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                                //dgvApprovedItems.DataBind();
                                //protected List<InventoryRequestItem> oRequesItems { get { return (List<InventoryRequestItem>)Session["oRequesItems"]; } set { Session["oRequesItems"] = value; } }

                            }

                        }
                    }
                }
                else
                {
                    //DispMsg("Please enter the valid quantity");
                    dgvApprovedItems.AutoGenerateColumns = false;
                    //dgvApprovedItems.DataSource = new List<InventoryRequestItem>();
                    //dgvApprovedItems.DataSource = oRequesItems;//_InvDetailList;
                    //dgvApprovedItems.DataBind();
                    dgvApprovedItems.DataSource = null;
                    dgvApprovedItems.DataBind();
                    string _Msg = "Please enter the valid quantity. Please select the document again.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    return;
                }
            }
        }

    }
}