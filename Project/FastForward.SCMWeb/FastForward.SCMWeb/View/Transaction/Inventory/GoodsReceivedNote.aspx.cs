using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class Goods_Received_Note___GRN : Base
    {
        List<MasterItemStatus> _statusList
        {
            get { if (Session["_statusList"] != null) { return (List<MasterItemStatus>)Session["_statusList"]; } else { return new List<MasterItemStatus>(); } }
            set { Session["_statusList"] = value; }
        }

        protected List<ExcelSerial> _ExcelSerial { get { return (List<ExcelSerial>)Session["_ExcelSerial"]; } set { Session["_ExcelSerial"] = value; } }

        protected List<ReptPickSerials> _excelReptPickSerials { get { return (List<ReptPickSerials>)Session["_excelReptPickSerials"]; } set { Session["_excelReptPickSerials"] = value; } }
        protected Boolean _checkbaseitem { get { return (Boolean)Session["_checkbaseitem"]; } set { Session["_checkbaseitem"] = value; } }
        protected Boolean _getallItemDo { get { return (Boolean)Session["_getallItemDo"]; } set { Session["_getallItemDo"] = value; } }
        protected Boolean _chkallItemDo { get { return (Boolean)Session["_chkallItemDo"]; } set { Session["_chkallItemDo"] = value; } }
        protected Boolean _chkPOQty { get { return (Boolean)Session["_chkPOQty"]; } set { Session["_chkPOQty"] = value; } }
        protected Boolean _itmPick
        {
            get
            {
                if (Session["_itmPick"] != null)
                {
                    return (Boolean)Session["_itmPick"];
                }
                else
                {
                    return false;
                }
            }
            set { Session["_itmPick"] = value; }
        }
        protected Boolean _gridEdit
        {
            get
            {
                if (Session["_gridEdit"] != null)
                {
                    return (Boolean)Session["_gridEdit"];
                }
                else
                {
                    return false;
                }
            }
            set { Session["_gridEdit"] = value; }
        }
        protected string _itmPickItemCode
        {
            get
            {
                if (Session["_itmPickItemCode"] != null)
                {
                    return (string)Session["_itmPickItemCode"];
                }
                else
                {
                    return "";
                }
            }
            set { Session["_itmPickItemCode"] = value; }
        }
        protected DataTable _dtTable
        {
            get
            {
                if (Session["_dtTable"] != null)
                {
                    return (DataTable)Session["_dtTable"];
                }
                else
                {
                    return new DataTable();
                }
            }
            set { Session["_dtTable"] = value; }
        }
        protected Int32 _itmPickLine
        {
            get
            {
                if (Session["_itmPickLine"] != null)
                {
                    return (Int32)Session["_itmPickLine"];
                }
                else
                {
                    return 0;
                }
            }
            set { Session["_itmPickLine"] = value; }
        }

        protected int _userSeqNo { get { return (int)Session["_userSeqNo"]; } set { Session["_userSeqNo"] = value; } }

        protected string _serialvalue { get { return (string)Session["_serialvalue"]; } set { Session["_serialvalue"] = value; } }

        protected MasterItem _itemdetail { get { return (MasterItem)Session["_itemdetail"]; } set { Session["_itemdetail"] = value; } }
        protected List<ReptPickSerials> _excelMisMatchReptPickSerials { get { return (List<ReptPickSerials>)Session["_excelMisMatchReptPickSerials"]; } set { Session["_excelMisMatchReptPickSerials"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChangeName();
                ClearPage();
                tstSubSerial.Focus();
                CheckCom();
            }
            else if (IsPostBack)
            {
                txtFromDate.Text = Request[txtFromDate.UniqueID];
                txtToDate.Text = Request[txtToDate.UniqueID];
                // txtDODate.Text = Request[txtDODate.UniqueID];
                txtSerial1.Text = Request[txtSerial1.UniqueID];
                txtSerial2.Text = Request[txtSerial2.UniqueID];
                if (Session["Doc"] != null)
                {
                    if (Session["Doc"].ToString() == "true")
                    {
                        UserDPopoup.Show();
                        Session["true"] = "";
                    }
                }
            }
        }
        //Added By Dulaj 2018-May-10
        private void ChangeName()
        {
            
            var id = Request.QueryString["id"];
            if (id != null)
            {
                AutosacnDiv.InnerHtml = "Auto Scan Non-Serialized Assets";
                GrnItemLabal.InnerText = "Add GRN Asset";
                asstetStatusDiv.InnerText = "Asset Status";
                NewItemDetails.InnerHtml = "New Asset Details";
                NewItemDiv.InnerHtml = "New Asset";
                grdDOItems.Columns[3].HeaderText = "Asset";
                grdDOSerials.Columns[4].HeaderText = "Asset";
                showPickedLable.Text = "Show picked asset on top";
                ListItem.InnerText = "Asset";
                ItemDiv.InnerHtml = "Asset";
            }


        }
        //Added By Dulaj 2018-Mar-21
        private void CheckExessQty()
        {
            string para = "";
            if (ddlMainType.SelectedValue.Equals("L"))
            {
                 para = "EXCESSRELO";
                
            }
            if (ddlMainType.SelectedValue.Equals("I"))
            {
                para = "EXCESSRES";
            }
            Int32 _checkExessQty = CHNLSVC.Inventory.CheckExessQty("COM", para, Convert.ToDateTime(txtDODate.Text), Session["UserCompanyCode"].ToString());
            //Int32 _checkExessQty = CHNLSVC.Inventory.CheckExessQty("COM", para, Convert.ToDateTime(txtDODate.Text), "ARL");
            Session["ChecExess"] = _checkExessQty;
        }
        //ViewState["po_items"]
        //ViewState["Tempitems"]
        //Session["UNIT_PRICE"]
        //Session["LineNO]
        //Session["Subserial"]
        bool _isDecimalAllow = false;
        Boolean _isFactBase = false;
        int _itemSerializedStatus = 0;
        int cRow;
        private List<InvoiceItem> invoice_items = null;
        private List<InvoiceItem> invoice_items_bind = null;
        private List<PurchaseOrderDelivery> podel_items = null;
        private string _profitCenter = "";
        private bool IsGrn = false;
        private bool _isInterCompanyGRN = false;
        private string _poType = string.Empty;
        MasterItem msitem = new MasterItem();
        List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
        List<MasterItemComponent> _MasterItemComponent = new List<MasterItemComponent>();
        List<ReptPickSerialsSub> _reptPickSerialsSub = new List<ReptPickSerialsSub>();
        private List<InventoryRequestItem> ScanItemList = null;
        private void ClearPage()
        {
            _statusList = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            txtDO.Text = string.Empty;
            _chkallItemDo = false;
            _getallItemDo = false;
            _chkPOQty = false;
            _ExcelSerial = new List<ExcelSerial>();
            Session["baseitem"] = "";
            _gridEdit = false;
            _itmPick = false;
            _itmPickItemCode = "";
            _itmPickLine = -1;
            _dtTable = new DataTable();
            int _userSeqNo = 0;
            _checkbaseitem = false;
            Session["_userSeqNo"] = -1;
            Session["ValidateFilePath"] = null;
            Session["print"] = 3;
            Session["documntNo"] = "";
            Session["Doc"] = "";
            Session["DocType"] = "";
            Session["GlbModuleName"] = "m_Trans_Inventory_GRN";
            grdPendingPo.DataSource = new int[] { };
            grdPendingPo.DataBind();
            grdDOItems.DataSource = new int[] { };
            grdDOItems.DataBind();
            DateTime date = DateTime.Now;
            CheckBoxQR.Checked = false;
            

            if (Session["GlbDefaultBin"] == null)
            {
                lblbinMssg.Text = "BIN  is not allocate for your location.";
                SbuPopup.Show();
                return;
            }
            txtBincode.Text = Session["GlbDefaultBin"].ToString();
            _poType = string.Empty;
            txtFromDate.Text = date.Date.AddMonths(-5).ToString("dd/MMM/yyyy");// DateTime.Now.Date.AddMonths(-5).Date.ToShortDateString();
            txtToDate.Text = date.ToString("dd/MMM/yyyy");
            txtDODate.Text = date.ToString("dd/MMM/yyyy");
            txtFDate.Text = date.ToString("dd/MMM/yyyy");
            // txtCompany.Text = string.Empty;
            //GetPendingPurchaseOrders(Convert.ToDateTime(txtFromDate.Text).ToShortDateString(), Convert.ToDateTime(txtToDate.Text).ToShortDateString(), txtFindSupplier.Text, txtFindPONo.Text, false);
            //chkManualRef.Checked = false;
            /////SetManualRefNo();

            lblBackDateInfor.Text = string.Empty;
            txtPONo.Text = string.Empty;
            txtPODate.Text = date.ToString("dd/MMM/yyyy");

            txtPORefNo.Text = string.Empty;
            txtEntry.Text = string.Empty;
            txtSuppCode.Text = string.Empty;
            txtSuppName.Text = string.Empty;
            txtFindSupplier.Text = string.Empty;
            txtFindPONo.Text = string.Empty;
            //txtVehicleNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;

            List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
            _emptyserList = null;
            grdDOSerials.AutoGenerateColumns = false;
            grdDOSerials.DataSource = new int[] { };
            grdDOSerials.DataBind();
            List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
            _emptyinvoiceitemList = null;
            grdDOItems.AutoGenerateColumns = false;
            grdDOItems.DataSource = new int[] { };
            grdDOItems.DataBind();
            bool _allowCurrentTrans = false;
            if (Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            MasterLocationNew _objloc_backdate = new MasterLocationNew();
            _objloc_backdate.Ml_loc_cd = Session["UserDefLoca"].ToString();
            _objloc_backdate.Ml_act = 1;
            List<MasterLocationNew> _LOC_backdate = CHNLSVC.General.GetMasterLocations(_objloc_backdate);
            Session["Ml_cate_1"] = _LOC_backdate.FirstOrDefault().Ml_cate_1.ToString();
            Session["GlbModuleName"] = Session["Ml_cate_1"].ToString() == "DFS" ? "m_Trans_InvBond_GRN" : Session["GlbModuleName"];

            IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, Session["GlbModuleName"].ToString(), txtDODate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            GetUserPermission();

            txtItemCode.Text = string.Empty;
            txtItemDes.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtpartNo.Text = string.Empty;
            //  ddlStatus.SelectedValue = "0";
            txtqty.Text = string.Empty;
            txtModel.Text = string.Empty;

            txtMdate.Text = date.ToString("dd/MMM/yyyy");
            txtEDate.Text = date.ToString("dd/MMM/yyyy");
            txtCDate.Text = date.ToString("dd/MMM/yyyy");

            txtSerial1.Text = string.Empty;
            txtSerial2.Text = string.Empty;
            txtSerial3.Text = string.Empty;
            txtSubproduct.Text = string.Empty;
            tstSubSerial.Text = string.Empty;
            txtStartPage.Text = string.Empty;
            txtlastPages.Text = string.Empty;
            Session["UNIT_PRICE"] = "";
            Session["_itemSerializedStatus"] = "";
            Session["Subserial"] = "";
            Session["DivVisible"] = "";
            Session["RowNo"] = "";
            Session["subItemCurrentRowCount"] = "0";
            Session["ItemPreFix"] = "false";
            Session["Defalt"] = "";
            txtSubproduct.Text = string.Empty;
            // ddlSIStatus.Text = string.Empty;
            tstSubSerial.Text = string.Empty;






            //CHECK SERIAL MAINTANCE LOCATION
            MasterLocationNew _objloc = new MasterLocationNew();
            _objloc.Ml_loc_cd = Session["UserDefLoca"].ToString();
            _objloc.Ml_act = 1;
            // _objloc.Ml_com_cd=Session["UserCompanyCode"].ToString();
            List<MasterLocationNew> _LOC = CHNLSVC.General.GetMasterLocations(_objloc);
            if (_LOC.Count > 0)
            {
                int _isserialMaintan = _LOC.First().Ml_is_serial;
                if (_isserialMaintan == 0)
                {
                    Session["_isserialMaintan"] = false;

                }
                else
                {
                    Session["_isserialMaintan"] = true;
                }

                int _isPDA = _LOC.First().Ml_is_pda;
                if (_isPDA == 1)
                {
                    chkpda.Enabled = true;
                    Session["WAREHOUSE_COM"] = _LOC.First().Ml_wh_com;
                    Session["WAREHOUSE_LOC"] = _LOC.First().Ml_wh_cd;
                }
                else
                {
                    chkpda.Enabled = false;
                }
            }
            PopulateLoadingBays();

        }
        #region Common Searching Area

        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, TextBox _dtpcontrol, Label _lblcontrol, string _date, out bool _allowCurrentDate)
        {
            BackDates _bdt = new BackDates();
            _dtpcontrol.Enabled = false;
            _allowCurrentDate = false;

            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
            if (_isAllow == true)
            {
                _dtpcontrol.Enabled = true;
                //if (_bdt != null) _lblcontrol.Text = "This module is back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
                    //_dtpcontrol.Value = _bdt.Gad_act_to_dt.Date; //TEMPORAY BLOCK - CHAMAL 06-05-2014 , BLOCK REMOVE BY CHAMAL  31-12-2014
                    //Again removeed because POSTING time its gone 31 st or 30 date
                    Information.Visible = true;
                }
            }

            if (_bdt == null)
            {
                _allowCurrentDate = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentDate = true;
                }
            }

            CheckSessionIsExpired();
            return _isAllow;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        string type;
                        if (ddlMainType.SelectedItem.Text == "Local") { type = "L"; } else { type = "I"; }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + type);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "GRN" + seperator + "1" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(txtSuppCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POByDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ddlMainType.SelectedValue + seperator + txtFromDate.Text + seperator + txtToDate.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ProductionPlan:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion Common Searching Area
        #region Pending Orders

        private void GetUserPermission()
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10066))
                _isInterCompanyGRN = true;
            else _isInterCompanyGRN = false;
        }
        private void GetPendingPurchaseOrders(DateTime _fromDate, DateTime _toDate, string _supCode, string _docNo, string _RefNo, string _type, Boolean _showErrMsg, string _blref)
        {
            try
            {
                PurchaseOrder _paramPurchaseOrder = new PurchaseOrder();

                _paramPurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
                _paramPurchaseOrder.MasterLocation = new MasterLocation() { Ml_loc_cd = Session["UserDefLoca"].ToString() };

                if (chkFPO.Checked == true)
                {
                    _paramPurchaseOrder.Poh_stus = "F";
                }
                else
                {
                    _paramPurchaseOrder.Poh_stus = "A";
                }
                _paramPurchaseOrder.MasterBusinessEntity = new MasterBusinessEntity() { Mbe_tp = "S" };
                _paramPurchaseOrder.FromDateNew = _fromDate;
                _paramPurchaseOrder.ToDateNew = _toDate;
                _paramPurchaseOrder.Poh_supp = _supCode;
                _paramPurchaseOrder.Poh_ref = _RefNo;
                _paramPurchaseOrder.Poh_sub_tp = _type;
                _paramPurchaseOrder.Poh_doc_no = _docNo;
                _paramPurchaseOrder.Poh_remarks = _blref;
                DataTable pending_list = new DataTable();
                if (ddlPType.SelectedValue == "5")
                {
                    _paramPurchaseOrder.Poh_job_no = txtFindPONo.Text.ToString();
                    pending_list = CHNLSVC.Inventory.GetAllPendingPOrderBank(_paramPurchaseOrder);
                }
                else
                {
                    pending_list = CHNLSVC.Inventory.GetAllPendingPOrder(_paramPurchaseOrder);
                }


                if (chkPendingDoc.Checked)
                {
                    #region Check Scan Completed
                    foreach (DataRow item in pending_list.Rows)
                    {
                        ReptPickHeader _tmpPickHdr = new ReptPickHeader();
                        _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                        {
                            Tuh_doc_no = item[1].ToString(),//invHdr.Ith_oth_docno,
                            Tuh_doc_tp = "GRN",
                            Tuh_direct = false,
                            Tuh_usr_com = Session["UserCompanyCode"].ToString()
                        }).FirstOrDefault();
                        if (_tmpPickHdr != null)
                        {
                            if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_loc) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com))
                            {
                                if (_tmpPickHdr.Tuh_fin_stus != 1)
                                {
                                    item.Delete();
                                }

                            }
                            else
                            {
                                item.Delete();
                            }
                        }
                        else
                        {
                            item.Delete();
                        }
                    }
                    #endregion
                    pending_list.AcceptChanges();
                }


                //_paramPurchaseOrder = new PurchaseOrder();

                //_paramPurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
                //_paramPurchaseOrder.MasterLocation = new MasterLocation() { Ml_loc_cd = Session["UserDefLoca"].ToString() };
                //_paramPurchaseOrder.Poh_stus = "A";
                //_paramPurchaseOrder.MasterBusinessEntity = new MasterBusinessEntity() { Mbe_tp = "S" };
                //_paramPurchaseOrder.FromDate = _fromDate;
                //_paramPurchaseOrder.ToDate = _toDate;
                //_paramPurchaseOrder.Poh_supp = _supCode;
                //_paramPurchaseOrder.Poh_doc_no = _docNo;
                //_paramPurchaseOrder.Poh_sub_tp = "S";
                //_paramPurchaseOrder.Poh_doc_no = txtFindPONo.Text;
                //DataTable pending_listSup = CHNLSVC.Inventory.GetAllPendingPOrder(_paramPurchaseOrder);

                //pending_list.Merge(pending_listSup);

                if (pending_list.Rows.Count >= 0)
                {
                    MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (_mstLoc.Ml_cate_1 != "DFS")
                    {
                        DataView dv = new DataView(pending_list);
                        string N = "N";
                        dv.RowFilter = "poh_sub_tp ='" + N + "'";
                        grdPendingPo.AutoGenerateColumns = false;
                        dv.Sort = "poh_dt DESC";
                        grdPendingPo.DataSource = dv;
                        grdPendingPo.DataBind();
                    }
                    else
                    {
                        DataView dv = new DataView(pending_list);
                        string T = "T";
                        dv.RowFilter = "poh_sub_tp ='" + T + "'";
                        dv.Sort = "poh_dt DESC";
                        grdPendingPo.AutoGenerateColumns = false;
                        grdPendingPo.DataSource = dv;
                        grdPendingPo.DataBind();
                    }

                }
                else
                {
                    if (_showErrMsg == true)
                    {
                        pending_list = null;
                        ErrorMsgGRN("No pending purchase orders found!");
                        grdPendingPo.AutoGenerateColumns = false;
                        grdPendingPo.DataSource = pending_list;
                        grdPendingPo.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion Pending Orders
        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/MasterFiles/Warehouse/BinSetup.aspx");
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void LoadCommonOutScan_ForScan(string DocumentType, string _scanDocument, int ItemLineNo)
        {
            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                ///Messager = new StringBuilder();
                //Messager.Append("Please select the item code.");
                //Exception(lblPopupItemCode, null);
                // this.Close();
            }
            if (string.IsNullOrEmpty(txtBincode.Text))
            {
                //Messager = new StringBuilder();
                //Messager.Append("Please select the bin code.");
                //Exception(lblPopupBinCode, null);
                //this.Close();
            }
            if (string.IsNullOrEmpty(lblDocQty.Text))
            {
                //Messager = new StringBuilder();
                //Messager.Append("Please select the requested qty.");
                //Exception(lblDocQty, null);
                //this.Close();
            }

            #region Get Item Details
            _isFactBase = false;

            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text);
            List<MasterCompanyItemStatus> _tempRevertlist = CHNLSVC.Inventory.GetAllCompanyStatuslist(Session["UserCompanyCode"].ToString());
            List<MasterCompanyItemStatus> _list = null;
            txtItemDes.Text = msitem.Mi_longdesc;
            txtModel.Text = msitem.Mi_model;
            txtBrand.Text = msitem.Mi_brand;

            if (msitem.Mi_fac_base == true)
            {
                _isFactBase = true;
            }
            else
            {
                _isFactBase = false;
            }


            if (Convert.ToBoolean(Session["_isserialMaintan"].ToString()))
            {
                if (msitem.Mi_is_ser1 == 1)
                {
                    _itemSerializedStatus = 1;
                    Session["_itemSerializedStatus"] = "1";
                    //lblSerialized1.BackColor = Color.Green;
                    divSer.Visible = true;
                    Qty.Visible = false;
                    txtSerial1.Enabled = true;
                    txtSerial2.Enabled = false;
                    txtSerial1.Focus();
                }
                else
                {
                    _itemSerializedStatus = 0;
                    Session["_itemSerializedStatus"] = "0";
                    divSer.Visible = false;
                    Qty.Visible = true;
                    txtqty.Focus();
                }
                if (msitem.Mi_is_ser1 == -1)
                {
                    _itemSerializedStatus = -1;
                    Session["_itemSerializedStatus"] = "-1";
                    _isDecimalAllow = true;
                    divSer.Visible = false;
                    Qty.Visible = true;
                    txtqty.Focus();
                }

                if (msitem.Mi_is_ser2 == 1)
                {
                    _itemSerializedStatus = 2;
                    Session["_itemSerializedStatus"] = "2";
                    txtSerial1.Enabled = true;
                    txtSerial2.Enabled = true;
                    txtSerial1.Focus();
                    divSer.Visible = true;
                    Qty.Visible = false;
                    //if (_isFactBase == true)
                    //{
                    //    lblSerialized2.Text = "Weight";
                    //    label10.Text = "Weight";
                    //}
                }
                txtSerial3.Enabled = false;
                if (msitem.Mi_is_ser3 == true)
                {
                    _itemSerializedStatus = 3;
                    Session["_itemSerializedStatus"] = "3";
                    txtSerial3.Enabled = true;
                    txtSerial3.Focus();

                    txtSerial3.Enabled = true;
                    divSer.Visible = true;
                    Qty.Visible = false;
                    //if (_isFactBase == true)
                    //{
                    //    lblSerialized3.Text = "Factor";
                    //    label8.Text = "Factor";
                    //}
                    txtSerial1.Focus();
                }
            }
            else if (!BaseCls.GlbMasterLocation.Ml_is_serial)
            {
                _itemSerializedStatus = -1;
                Session["_itemSerializedStatus"] = "-1";
                _isDecimalAllow = true;
                divSer.Visible = false;
                Qty.Visible = true;
                txtqty.Focus();
            }

            if (msitem.Mi_is_scansub == true)
            {
                //lblSubSerialized.Text = "YES";
                Session["Subserial"] = "true";
            }
            else
            {
                Session["Subserial"] = "false";
                //lblSubSerialized.Text = "NO";
            }

            // txtItemStatus.Text = ItemStatus;
            #endregion

            #region Get Current Scan List
            Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, Session["UserCompanyCode"].ToString(), _scanDocument, 1);
            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
            if (_ResultItemsSerialList != null)
            {
                //grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(txtItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                //grdDOSerials.DataBind();
                grdDOSerials.DataSource = _ResultItemsSerialList;
                grdDOSerials.DataBind();
            }
            #endregion



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
        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykey2.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey2.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey2.SelectedIndex = 0;
        }
        private void ClearErrorMsg()
        {
            WarningGRN.Visible = false;
            SuccessGRN.Visible = false;
        }
        private void ErrorMsgGRN(string _Msg)
        {
            WarningGRN.Visible = true;
            lblWGRN.Text = _Msg;
        }
        private void SuccessMsgGRN(string _Msg)
        {
            SuccessGRN.Visible = true;
            lblSGRN.Text = _Msg;
        }

        private void CheckItemAllreadyInPo(string _ItemCode, DataTable _item)
        {
            Session["Subserial"] = "false";
            // DataTable _new = ViewState["po_items"] as DataTable;
            DataView dv = new DataView(_item);
            dv.RowFilter = "PODI_ITM_CD ='" + _ItemCode + "'";

            //if (dv.Table.Rows.Count > 1)
            //{
            //   
            //}
            int deletedLOVcount = dv.Count;
            if (deletedLOVcount > 1)
            {
                //_item = dv.Table;

                grdCheckIItem.DataSource = dv;
                grdCheckIItem.DataBind();
                _item.Columns.RemoveAt(0);
                _item.Columns.RemoveAt(0);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(2);
                _item.Columns.RemoveAt(1);
                _item.Columns[0].ColumnName = "Item";
                BindUCtrlDDLData2(_item);

                ViewState["ReqItem"] = _item;
                lblvalue.Text = "CheckItem";

                UserCheckItem.Show();
                _item = null;
                ViewState["po_items"] = ViewState["Tempitems"];
                lblNotMsg.Text = "Purchase order contains '" + _ItemCode + "' twice.Please select one of these ";

                return;
            }
            else if (deletedLOVcount == 0)
            {
                if (chkNewItem.Checked == true)
                {
                    DataView dvnew = new DataView(_item);
                    // string Des = dvnew[0]["MI_LONGDESC"].ToString();
                    // string Model = dvnew[0]["MI_MODEL"].ToString();
                    // string Brand = dvnew[0]["MI_BRAND"].ToString();
                    Session["UNIT_PRICE"] = 0;//dvnew[0]["UNIT_PRICE"].ToString();
                    int LineNo = 0;//Convert.ToInt32(dvnew[0]["PODI_LINE_NO"].ToString());
                    //lblDocQty.Text = "0"; dvnew[0]["PODI_BAL_QTY"].ToString();
                    // txtItemCode.Text = dvnew[0]["PODI_ITM_CD"].ToString();
                    // txtModel.Text = Model;
                    //txtBrand.Text = Brand;
                    // txtpartNo.Text = Part;
                    //txtItemDes.Text = Des;
                    Session["LineNO"] = LineNo;
                    LoadCommonOutScan_ForScan("GRN", txtPONo.Text, Convert.ToInt32(Session["LineNO"]));
                    LoadPOItems(txtPONo.Text);
                    lblvalue.Text = "";
                    return;
                }
                DataTable _item2 = ViewState["po_items"] as DataTable;
                grdCheckIItem.DataSource = _item2;
                grdCheckIItem.DataBind();
                lblvalue.Text = "CheckItem";
                lblNotMsg.Text = "Selected item is not available in the purchase order. Please select which item do you want to model change.'" + _ItemCode + "'";

                UserCheckItem.Show();
                return;
            }
            else
            {

                string Des = dv[0]["MI_LONGDESC"].ToString();
                string Model = dv[0]["MI_MODEL"].ToString();
                string Brand = dv[0]["MI_BRAND"].ToString();
                Session["UNIT_PRICE"] = dv[0]["UNIT_PRICE"].ToString();
                int LineNo = Convert.ToInt32(dv[0]["PODI_LINE_NO"].ToString());
                // lblDocQty.Text = dv[0]["PODI_BAL_QTY"].ToString();
                lblDocQty.Text = dv[0]["PODI_QTY"].ToString();
                txtItemCode.Text = dv[0]["PODI_ITM_CD"].ToString();
                txtModel.Text = Model;
                txtBrand.Text = Brand;
                // txtpartNo.Text = Part;
                txtItemDes.Text = Des;
                Session["LineNO"] = LineNo;
                LoadCommonOutScan_ForScan("GRN", txtPONo.Text, Convert.ToInt32(Session["LineNO"]));
                _dtTable = LoadDefRowNo(_dtTable);
                grdDOItems.DataSource = _dtTable;
                grdDOItems.DataBind();
                //LoadPOItems(txtPONo.Text);
                lblvalue.Text = "";

            }

        }
        private bool IsItemSerial(string _Item)
        {
            string vlaue = "";
            DataTable _tbl = CHNLSVC.Inventory.Get_Item_Infor(_Item);
            if (_tbl.Rows.Count != 0)
            {
                vlaue = _tbl.Rows[0]["mi_is_ser1"].ToString();

            }
            if (vlaue == "1") { return true; }
            return false;
        }


        //#region Generate new user seq no

        //private Int32 GenerateNewUserSeqNo()
        //{
        //    Int32 generated_seq = 0;
        //    try
        //    {
        //        generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "GRN", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
        //        ReptPickHeader RPH = new ReptPickHeader();
        //        RPH.Tuh_doc_tp = "GRN";
        //        RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
        //        RPH.Tuh_ischek_itmstus = true;//might change
        //        RPH.Tuh_ischek_reqqty = true;//might change
        //        RPH.Tuh_ischek_simitm = true;//might change
        //        RPH.Tuh_session_id = Session["SessionID"].ToString();
        //        RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change
        //        RPH.Tuh_usr_id = Session["UserID"].ToString();
        //        RPH.Tuh_usrseq_no = generated_seq;

        //        RPH.Tuh_direct = true; //direction always (-) for change status
        //        RPH.Tuh_doc_no = txtPONo.Text;
        //        //write entry to TEMP_PICK_HDR
        //        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
        //        if (affected_rows == 1)
        //        {
        //            return generated_seq;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...\n');", true);
        //        CHNLSVC.CloseChannel();
        //        return 0;
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseAllChannels();
        //    }
        //}

        //#endregion Generate new user seq no

        private void LoadPOItems(string _poNo)
        {
            string _errMsg = "";
            try
            {
                DataTable po_items = new DataTable();
                // po_items = ViewState["po_items"] as DataTable;
                //  if(po_items==null){
                // po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), _poNo, 1);
                if (ddlMainType.SelectedValue == "L")
                {
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), _poNo, 1);
                }
                else
                {
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), _poNo, 2);
                    if (Session["UserCompanyCode"].ToString() != "AAL") //Asanka List :: 12-Mar-2018 :: 196-Need to activate finished GRN for excess item GRN
                    {
                        foreach (DataRow drValue in po_items.Rows)
                        {
                            if (drValue["podi_bal_qty"].ToString() == "0")
                            {
                                drValue.Delete();
                            }

                        }

                        po_items.AcceptChanges();
                    }
                }
                //Get Invoice Items Details

                po_items.Columns.Add("RowNo", typeof(Int32));
                po_items = LoadDefRowNo(po_items);
                _dtTable = po_items;
                if (po_items.Rows.Count > 0)
                {
                    grdDOItems.Enabled = true;
                    ReptPickHeader _tmpPickHdrPart = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA_PARTIAL(new ReptPickHeader()
                    {
                        Tuh_doc_no = _poNo,//invHdr.Ith_oth_docno,
                        Tuh_doc_tp = "GRN",
                        Tuh_direct = false,
                        Tuh_usr_com = Session["UserCompanyCode"].ToString()
                    }).FirstOrDefault();
                    if (_tmpPickHdrPart != null)
                    {
                        ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                        {
                            Tuh_doc_no = _poNo,//invHdr.Ith_oth_docno,
                            Tuh_doc_tp = "GRN",
                            Tuh_direct = false,
                            Tuh_usr_com = Session["UserCompanyCode"].ToString()
                        }).FirstOrDefault();
                        if (_tmpPickHdr == null)
                        {
                            if (_tmpPickHdr == null)
                            {
                                string _tmpErr = "";
                                Int32 _r = CHNLSVC.Inventory.SaveDocumentHdrFromPartially(_poNo, "GRN", out _tmpErr);
                                if (_r < 1)
                                {
                                    DispMsg(_tmpErr, "E"); return;
                                }
                            }
                        }
                    }
                    _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), _poNo, 1);
                    if (_userSeqNo == -1)
                    {
                        _userSeqNo = GenerateNewUserSeqNo("GRN", txtPONo.Text);

                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_direct = true;
                        _inputReptPickHeader.Tuh_doc_no = _poNo;
                        _inputReptPickHeader.Tuh_doc_tp = "GRN";
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;
                        _inputReptPickHeader.Is_doc_Partial_save = true;
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        //Save it to the scmrep.temp_pick_hdr header table. 
                        Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    // //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");

                    if (_serList != null)
                    {
                        //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_qty,x.Tus_itm_stus }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_qty, x.Tus_itm_stus }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataRow row in po_items.Rows)
                            {
                                //if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString()))
                                //{
                                if (itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString()))
                                {
                                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                    if (msitem == null)
                                    {
                                        _errMsg = "Invalid Item Code " + itm.Peo.Tus_itm_cd;
                                    }
                                    else
                                    {
                                        if (!msitem.Mi_act)
                                        {
                                            _errMsg = "Invalid Item Code " + itm.Peo.Tus_itm_cd;
                                        }
                                    }
                                    string _itemSerializedStatus = msitem.Mi_is_ser1.ToString();// string  _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                                    _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                                    decimal qty = Convert.ToDecimal(row["GRN_QTY"].ToString());
                                    decimal poqty = Convert.ToDecimal(row["PODI_QTY"].ToString());
                                    decimal pobalqty = Convert.ToDecimal(row["PODI_BAL_QTY"].ToString());
                                    if (qty == 0)
                                    {
                                        //qty = pobalqty;
                                    }
                                    if (msitem.Mi_is_ser1 == 0)//(_itemSerializedStatus == "0")
                                    {

                                        row["PODI_BAL_QTY"] = pobalqty - (itm.Peo.Tus_qty * itm.theCount);
                                        row["GRN_QTY"] = qty + (itm.Peo.Tus_qty * itm.theCount);//itm.theCount; // Current scan qty
                                    }
                                    else if (msitem.Mi_is_ser1 == 1)// (_itemSerializedStatus == "1")
                                    {
                                        bool _ismaintanserial = (bool)Session["_isserialMaintan"];
                                        if (_ismaintanserial == false)
                                        {
                                            row["PODI_BAL_QTY"] = pobalqty - itm.Peo.Tus_qty;
                                            row["GRN_QTY"] = itm.Peo.Tus_qty; // Current scan qty   
                                        }
                                        else
                                        {
                                            row["PODI_BAL_QTY"] = pobalqty - itm.theCount;
                                            row["GRN_QTY"] = itm.theCount; // Current scan qty   
                                        }

                                    }
                                    else
                                    {

                                        bool _ismaintanserial = (bool)Session["_isserialMaintan"];
                                        if (_ismaintanserial == false)
                                        {
                                            row["PODI_BAL_QTY"] = pobalqty - itm.Peo.Tus_qty;
                                            row["GRN_QTY"] = itm.Peo.Tus_qty;//itm.theCount; // Current scan qty
                                        }
                                        else
                                        {
                                            row["PODI_BAL_QTY"] = pobalqty - itm.Peo.Tus_qty;
                                            row["GRN_QTY"] = itm.Peo.Tus_qty;//itm.theCount; // Current scan qty
                                        }

                                    }
                                    //}

                                }
                            }
                        }
                        grdDOSerials.AutoGenerateColumns = false;
                        grdDOSerials.DataSource = _serList.OrderByDescending(x => x.Tus_cre_dt).ToList().Take(100);
                        grdDOSerials.DataBind();

                        grdDOSerials_2.AutoGenerateColumns = false;
                        grdDOSerials_2.DataSource = _serList.OrderByDescending(x => x.Tus_cre_dt).ToList();
                        grdDOSerials_2.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        grdDOSerials.AutoGenerateColumns = false;
                        grdDOSerials.DataSource = emptyGridList;
                        grdDOSerials.DataBind();

                        grdDOSerials_2.AutoGenerateColumns = false;
                        grdDOSerials_2.DataSource = emptyGridList;
                        grdDOSerials_2.DataBind();
                    }



                    grdDOItems.AutoGenerateColumns = false;
                    po_items = LoadDefRowNo(po_items);
                    _dtTable = po_items;
                    grdDOItems.DataSource = po_items;
                    grdDOItems.DataBind();
                    grdDOItems_2.AutoGenerateColumns = false;
                    grdDOItems_2.DataSource = po_items;
                    grdDOItems_2.DataBind();

                    ViewState["po_items"] = po_items;
                    ViewState["Tempitems"] = po_items;

                    object sumObject;
                    sumObject = po_items.Compute("Sum(PODI_BAL_QTY)", "");
                    decimal tmpDes = 0;
                    lblpototal.Text = decimal.TryParse(sumObject.ToString(), out tmpDes) ? Convert.ToDecimal(sumObject.ToString()).ToString("N2") : "";
                    object sumObjectpick;
                    sumObjectpick = po_items.Compute("Sum(GRN_QTY)", "");
                    lblpicktotal.Text = decimal.TryParse(sumObjectpick.ToString(), out tmpDes) ? Convert.ToDecimal(sumObjectpick.ToString()).ToString("N2") : "";
                    if (_serList != null)
                    {
                        if (_serList.Count > 0)
                        {
                            decimal _pickQty = _serList.Sum(c => c.Tus_qty);
                            lblpicktotal.Text = _pickQty.ToString("#,##0.####");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (_errMsg.Contains("Invalid Item Code"))
                {
                    string _Msg = _errMsg;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
                }

                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            //get all from sat_itm
        }






        private void FilterData()
        {
            DataTable _result = (DataTable)ViewState["po_items"];
            DataView dv = new DataView(_result);
            string searchParameter = ddlSearchbykey.SelectedItem.Text;
            /// dv.RowFilter = " itr_req_no = '" + _RNo + "'";
            dv.RowFilter = " '" + ddlSearchbykey.SelectedItem.Text + "' = '" + txtSearchbyword2.Text + "' ";
            if (dv.Count > 0)
            {
                _result = dv.ToTable();
            }
            grdCheckIItem.DataSource = _result;
            grdCheckIItem.DataBind();
        }

        #region Add new serial/ new qty
        private void AddItemQuantites(string DocumentType, string _scanDocument, string PopupItemCode, decimal _unitCost, decimal _unitPrice, int ItemLineNo, decimal ScanQty, bool _ISTemp = false)
        {
            try
            {

                if (_ISTemp == true)
                {

                    if (_userSeqNo == 0) //Create a new seq no & insert it to the scmrep.temp_pick_hdr header table.  
                    {
                        _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());

                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_direct = true;
                        _inputReptPickHeader.Tuh_doc_no = _scanDocument;
                        _inputReptPickHeader.Tuh_doc_tp = DocumentType;
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                        _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;

                        //Save it to the scmrep.temp_pick_hdr header table. 
                        Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
                    }
                }

                string _binCode = txtBincode.Text;
                WarehouseBin _BIN = CHNLSVC.Inventory.GET_BIN_BY_CODE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _binCode);
                if (_BIN == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check bin code');", true);
                    return;
                }
                ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                _itemSerializedStatus = Convert.ToInt32(Session["_itemSerializedStatus"].ToString());
                // _ResultItemsSerialList= ViewState["Serials"]
                if (Convert.ToDateTime(txtEDate.Text) < DateTime.Now.Date)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid expiry date!');", true);
                    //MessageBox.Show("Please enter valid expiry date!", "Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDateTime(txtMdate.Text) > DateTime.Now.Date)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid manufacture date!');", true);
                    //MessageBox.Show("Please enter valid manufacture date!", "Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // int _userSeqNo = 0;
                //Need to check Whether that is there any record in temp_pick_hdr table in SCMREP DB.
                //_userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), _scanDocument);

                _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, Session["UserCompanyCode"].ToString(), _scanDocument, 1);
                if (_userSeqNo == -1)
                {
                    _userSeqNo = GenerateNewUserSeqNo(DocumentType, _scanDocument);
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_doc_no = _scanDocument;
                    _inputReptPickHeader.Tuh_doc_tp = DocumentType;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;

                    //Save it to the scmrep.temp_pick_hdr header table. 
                    Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
                }

                //using (TransactionScope _tr = new TransactionScope())
                //{
                #region TransactionScope

                if (_userSeqNo == 0) //Create a new seq no & insert it to the scmrep.temp_pick_hdr header table.  
                {
                    _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());

                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_doc_no = _scanDocument;
                    _inputReptPickHeader.Tuh_doc_tp = DocumentType;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;

                    //Save it to the scmrep.temp_pick_hdr header table. 
                    Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
                }

                //Get the selected Item
                //MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), PopupItemCode);

                string _itemStatus = ddlStatus.SelectedValue;//txtItemStatus.Text;
                string expirdate = txtEDate.Text;
                DataTable _WARUSER = CHNLSVC.Sales.GetinvUser(Session["UserID"].ToString());
                string _userwarrid = "P01";
                if (_WARUSER != null)
                {
                    if (_WARUSER.Rows.Count > 0)
                    {
                        _userwarrid = _WARUSER.Rows[0][32].ToString();
                        if (string.IsNullOrEmpty(_userwarrid))
                        {
                            _userwarrid = "P01";
                        }
                    }

                }

                Session["userSeqNo"] = _userSeqNo;
                if (_itemSerializedStatus == 1 || _itemSerializedStatus == 2 || _itemSerializedStatus == 3)
                {
                    #region Serialized
                    string _serialNo1 = string.Empty;
                    if (Session["ItemPreFix"].ToString() == "true")
                    {
                        if (Session["Defalt"].ToString() == "true")
                        {
                            _serialNo1 = txtFix.Text + txtSerial1.Text.Trim();
                            _inputReptPickSerials.Tus_pgs_prefix = txtFix.Text;
                        }
                        else if (Session["Defalt"].ToString() == "false")
                        {
                            _serialNo1 = ddlPreFix.SelectedItem.Text + txtSerial1.Text.Trim();
                            _inputReptPickSerials.Tus_pgs_prefix = ddlPreFix.SelectedItem.Text;
                        }
                        _inputReptPickSerials.Tus_is_pgs = 1;
                        _inputReptPickSerials.Tus_pgs_count = Convert.ToInt32(txtNoOfPages.Text);

                        _inputReptPickSerials.Tus_st_pg = Convert.ToInt32(txtStartPage.Text);
                        _inputReptPickSerials.Tus_ed_pg = Convert.ToInt32(txtlastPages.Text);
                    }
                    else if (Session["ItemPreFix"].ToString() == "false")
                    {
                        _serialNo1 = txtSerial1.Text.Trim();
                    }
                    else
                    {
                        _serialNo1 = txtSerial1.Text.Trim();
                    }

                    string _serialNo2 = txtSerial2.Text.Trim();
                    string _serialNo3 = txtSerial3.Text.Trim();
                    string _warrantyno = string.Empty;
                    int _serID = CHNLSVC.Inventory.IsExistInSerialMaster("", PopupItemCode, _serialNo1);
                    InventorySerialMaster _serIDMst = new InventorySerialMaster();
                    _serIDMst = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serID);

                    DataTable _dtser1 = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", PopupItemCode, _serialNo1);

                    if (_dtser1 != null)
                    {
                        if (_dtser1.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Added serial number is already exist!');", true);
                            //MessageBox.Show("Serial no 1 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    _dtser1.Dispose();


                    if ((CHNLSVC.Inventory.IsExistInTempPickSerial(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), PopupItemCode, _serialNo1)) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Added serial number is already exist.!');", true);
                        // MessageBox.Show("Serial no 1 is already in use.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_itemSerializedStatus == 2)
                    {
                        DataTable _dtser2 = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL2", PopupItemCode, _serialNo2);
                        if (_dtser2 != null)
                        {
                            if (_dtser2.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial no 2 is already exist.!');", true);
                                // MessageBox.Show("Serial no 2 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        _dtser2.Dispose();

                        if ((CHNLSVC.Inventory.IsExistInTempPickSerial(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), "SER_2", _serialNo2)) > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial no 2 is already in use.!');", true);
                            //MessageBox.Show("Serial no 2 is already in use.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    _warrantyno = _serIDMst.Irsm_warr_no;
                    txtWNo.Text = _warrantyno;
                    //Write to the Picked items serial table.

                    #region Fill Pick Serial Object
                    _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                    _inputReptPickSerials.Tus_doc_no = _scanDocument;
                    _inputReptPickSerials.Tus_base_doc_no = _scanDocument;
                    _inputReptPickSerials.Tus_base_itm_line = ItemLineNo;
                    _inputReptPickSerials.Tus_seq_no = 0;
                    _inputReptPickSerials.Tus_itm_line = 0;
                    _inputReptPickSerials.Tus_batch_line = 0;
                    _inputReptPickSerials.Tus_ser_line = 0;
                    _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                    _inputReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
                    _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickSerials.Tus_bin = _binCode;
                    _inputReptPickSerials.Tus_itm_cd = PopupItemCode;
                    _inputReptPickSerials.Tus_itm_stus = _itemStatus;
                    _inputReptPickSerials.Tus_unit_cost = _unitCost;
                    _inputReptPickSerials.Tus_unit_price = _unitPrice;
                    if (Session["_itemSerializedStatus"].ToString() == "0")
                    {
                        _inputReptPickSerials.Tus_qty = Convert.ToDecimal(txtqty.Text);
                    }
                    else
                    {
                        _inputReptPickSerials.Tus_qty = 1;
                    }


                    if (_serID > 0)
                    { _inputReptPickSerials.Tus_ser_id = _serID; }
                    else
                    { _inputReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID(); }
                    _inputReptPickSerials.Tus_ser_1 = _serialNo1;
                    _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                    _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                    if (string.IsNullOrEmpty(_warrantyno))
                        _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + Session["UserDefLoca"].ToString() + "-" + _userwarrid + "-" + _inputReptPickSerials.Tus_ser_id.ToString();
                    _inputReptPickSerials.Tus_warr_no = _warrantyno;
                    _inputReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                    _inputReptPickSerials.Tus_itm_model = txtModel.Text;
                    _inputReptPickSerials.Tus_itm_brand = txtBrand.Text;
                    _inputReptPickSerials.Tus_itm_line = ItemLineNo;
                    _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                    _inputReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                    _inputReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                    _inputReptPickSerials.Tus_itm_model = txtModel.Text;
                    _inputReptPickSerials.Tus_itm_brand = txtBrand.Text;
                    _inputReptPickSerials.Tus_batch_no = txtBatchNo.Text.Trim();
                    MasterItem _itmExp = CHNLSVC.General.GetItemMasterNew(_inputReptPickSerials.Tus_itm_cd);
                    if (_itmExp != null)
                    {
                        if (_itmExp.Tmp_mi_is_exp_dt == 1)
                        {
                            DateTime _dtTemp = new DateTime();
                            _inputReptPickSerials.Tus_exp_dt = DateTime.TryParse(txtEDate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtEDate.Text.Trim()) : DateTime.MinValue;
                            _inputReptPickSerials.Tus_manufac_dt = DateTime.TryParse(txtMdate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtMdate.Text.Trim()) : DateTime.MinValue;
                        }
                    }
                    string _baseItem = Session["baseitem"].ToString();
                    if (string.IsNullOrEmpty(_baseItem))
                    {
                        _baseItem = PopupItemCode;
                    }
                    _inputReptPickSerials.Tus_new_itm_cd = _baseItem;

                    //_inputReptPickSerials.Tus_base_doc_no = Session["baseitem"].ToString();
                    _inputReptPickSerials.Tus_job_line = ItemLineNo;
                    _inputReptPickSerials.Tus_job_no = txtPORefNo.Text;
                    // _inputReptPickSerials.Tus_exp_dt = Convert.ToDateTime(expirdate);

                    ////kapila 3/7/2015
                    //_inputReptPickSerials.Tus_orig_supp = _supplier;
                    //_inputReptPickSerials.Tus_exist_supp = _supplier;
                    //_inputReptPickSerials.Tus_orig_grnno = _grnNo;
                    //_inputReptPickSerials.Tus_exist_grnno = _grnNo;
                    //_inputReptPickSerials.Tus_orig_grndt = _grnDate;
                    //_inputReptPickSerials.Tus_exist_grndt = _grnDate;
                    //_inputReptPickSerials.Tus_batch_no = txtBatch.Text;
                    //_inputReptPickSerials.Tus_exp_dt = dtExp.Value.Date;
                    //_inputReptPickSerials.Tus_manufac_dt = dtManufc.Value.Date;
                    //_inputReptPickSerials.Tus_new_status = IsNew.ToString();

                    //if (DocumentType == "ADJ")
                    //{
                    //    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
                    //    if (_period != null)
                    //        _inputReptPickSerials.Tus_warr_period = _period.Mwp_val;
                    //}

                    #endregion

                    //Write to the Picked Sub Serial .
                    if (Session["Subserial"].ToString() == "true")
                    {
                        int _rowno = 0;
                        DataTable _tblSub = CHNLSVC.Inventory.getSubitemComponent(txtItemCode.Text);
                        foreach (DataRow row in _tblSub.Rows)
                        {
                            if (Session["_itemSerializedStatus"].ToString() == "1")
                            {
                                ReptPickSerialsSub _ReptPickSerialsSub = new ReptPickSerialsSub();
                                #region Fill Pick Sub Serial Object
                                _ReptPickSerialsSub.Tpss_itm_brand = txtBrand.Text;
                                _ReptPickSerialsSub.Tpss_itm_cd = row[0].ToString();
                                _ReptPickSerialsSub.Tpss_itm_desc = "";
                                _ReptPickSerialsSub.Tpss_itm_model = txtModel.Text;
                                _ReptPickSerialsSub.Tpss_itm_stus = ddlStatus.SelectedValue;
                                _ReptPickSerialsSub.Tpss_m_itm_cd = txtItemCode.Text;
                                _ReptPickSerialsSub.Tpss_m_ser = txtSerial1.Text;
                                _ReptPickSerialsSub.Tpss_mfc = "";
                                _ReptPickSerialsSub.Tpss_ser_id = _inputReptPickSerials.Tus_ser_id;

                                if (_rowno == 0)
                                {
                                    _ReptPickSerialsSub.Tpss_sub_ser = txtSerial1.Text;
                                }
                                else { _ReptPickSerialsSub.Tpss_sub_ser = "N/A"; }


                                _ReptPickSerialsSub.Tpss_tp = row["micp_itm_tp"].ToString();
                                _ReptPickSerialsSub.Tpss_usrseq_no = _userSeqNo;
                                _ReptPickSerialsSub.Tpss_warr_no = _warrantyno;
                                //_ReptPickSerialsSub.Tpss_warr_period = "";
                                // _ReptPickSerialsSub.Tpss_warr_rem = "";  
                                _reptPickSerialsSub.Add(_ReptPickSerialsSub);
                                _rowno++;
                                #endregion
                            }
                            else if (Session["_itemSerializedStatus"].ToString() == "2")
                            {
                                ReptPickSerialsSub _ReptPickSerialsSub = new ReptPickSerialsSub();
                                #region Fill Pick Sub Serial Object
                                _ReptPickSerialsSub.Tpss_itm_brand = txtBrand.Text;
                                _ReptPickSerialsSub.Tpss_itm_cd = row[0].ToString();
                                _ReptPickSerialsSub.Tpss_itm_desc = "";
                                _ReptPickSerialsSub.Tpss_itm_model = txtModel.Text;
                                _ReptPickSerialsSub.Tpss_itm_stus = ddlStatus.SelectedValue;
                                _ReptPickSerialsSub.Tpss_m_itm_cd = txtItemCode.Text;
                                _ReptPickSerialsSub.Tpss_m_ser = txtSerial1.Text;
                                _ReptPickSerialsSub.Tpss_mfc = "";
                                _ReptPickSerialsSub.Tpss_ser_id = _inputReptPickSerials.Tus_ser_id;

                                if (_rowno == 0)
                                {
                                    _ReptPickSerialsSub.Tpss_sub_ser = txtSerial1.Text;
                                }
                                else { _ReptPickSerialsSub.Tpss_sub_ser = "N/A"; }


                                _ReptPickSerialsSub.Tpss_tp = row["micp_itm_tp"].ToString();
                                _ReptPickSerialsSub.Tpss_usrseq_no = _userSeqNo;
                                _ReptPickSerialsSub.Tpss_warr_no = _warrantyno;
                                //_ReptPickSerialsSub.Tpss_warr_period = "";
                                // _ReptPickSerialsSub.Tpss_warr_rem = "";  
                                _reptPickSerialsSub.Add(_ReptPickSerialsSub);
                                _rowno++;
                                #endregion
                            }
                            else if (Session["_itemSerializedStatus"].ToString() == "3")
                            {
                                ReptPickSerialsSub _ReptPickSerialsSub = new ReptPickSerialsSub();
                                #region Fill Pick Sub Serial Object
                                _ReptPickSerialsSub.Tpss_itm_brand = txtBrand.Text;
                                _ReptPickSerialsSub.Tpss_itm_cd = row[0].ToString();
                                _ReptPickSerialsSub.Tpss_itm_desc = "";
                                _ReptPickSerialsSub.Tpss_itm_model = txtModel.Text;
                                _ReptPickSerialsSub.Tpss_itm_stus = ddlStatus.SelectedValue;
                                _ReptPickSerialsSub.Tpss_m_itm_cd = txtItemCode.Text;
                                _ReptPickSerialsSub.Tpss_m_ser = txtSerial1.Text;
                                _ReptPickSerialsSub.Tpss_mfc = "";
                                _ReptPickSerialsSub.Tpss_ser_id = _inputReptPickSerials.Tus_ser_id;

                                if (_rowno == 0)
                                {
                                    _ReptPickSerialsSub.Tpss_sub_ser = txtSerial1.Text;
                                }
                                else { _ReptPickSerialsSub.Tpss_sub_ser = "N/A"; }


                                _ReptPickSerialsSub.Tpss_tp = row["micp_itm_tp"].ToString();
                                _ReptPickSerialsSub.Tpss_usrseq_no = _userSeqNo;
                                _ReptPickSerialsSub.Tpss_warr_no = _warrantyno;
                                //_ReptPickSerialsSub.Tpss_warr_period = "";
                                // _ReptPickSerialsSub.Tpss_warr_rem = "";  
                                _reptPickSerialsSub.Add(_ReptPickSerialsSub);
                                _rowno++;
                                #endregion
                            }
                        }




                    }


                    List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);

                    //-------added by Shani
                    var serCount = 0;
                    if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                    {
                        serCount = (from c in _resultItemsSerialList
                                    where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
                                    select c).Count();
                    }
                    //for non serials
                    //var serCount_2 = (from c in _resultItemsSerialList
                    //                  select c.Tus_qty).Sum();lblDocQty.Text


                    // if (serCount <= Convert.ToDecimal(lblDocQty.Text.Trim()))
                    // {
                    lblScanQty.Text = serCount.ToString();
                    if (CheckBoxExcess.Checked && serCount>0)
                    {
                        _inputReptPickSerials.Tus_unit_cost = 0;
                        _inputReptPickSerials.Tus_job_line = 0;
                        _inputReptPickSerials.Tus_ser_line = 0;
                        _inputReptPickSerials.Tus_itm_line = 0;
                        _inputReptPickSerials.Tus_unit_price = 0;
                    }
                    if (CheckBoxExcess.Checked && serCount < 1)
                    {
                        DispMsg("Please add serials with cost before add excess items");
                        return;
                    }
                    CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, _reptPickSerialsSub);
                    //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
                    ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                    // }
                    // else
                    // {
                    //     ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required Qty!');", true);
                    // MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //     return;
                    // }
                    //-------added by Shani
                    //Save to the temp table.


                    //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                    List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);


                    grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    grdDOSerials.DataBind();
                    ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    //SubmitItemSerialData(); pls do this Chamal

                    #endregion

                }
                else if (_itemSerializedStatus == 0)
                {
                    #region Non-serialized
                    int _actualQty = Convert.ToInt32(txtqty.Text.Trim());
                    string _warrantyno = string.Empty;


                    //Write to the Picked items serials table.
                    ReptPickSerials _newReptPickSerials = new ReptPickSerials();
                    #region Fill Pick Serial Object
                    _newReptPickSerials.Tus_usrseq_no = _userSeqNo;
                    _newReptPickSerials.Tus_doc_no = _scanDocument;
                    _newReptPickSerials.Tus_base_doc_no = _scanDocument;
                    _newReptPickSerials.Tus_base_itm_line = ItemLineNo;
                    _newReptPickSerials.Tus_seq_no = 0;
                    _newReptPickSerials.Tus_itm_line = 0;
                    _newReptPickSerials.Tus_batch_line = 0;
                    _newReptPickSerials.Tus_ser_line = 0;
                    _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                    _inputReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
                    _newReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                    _newReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                    _newReptPickSerials.Tus_bin = _binCode;
                    _newReptPickSerials.Tus_itm_cd = PopupItemCode;
                    _newReptPickSerials.Tus_itm_stus = _itemStatus;
                    _newReptPickSerials.Tus_unit_cost = _unitCost;
                    _newReptPickSerials.Tus_unit_price = _unitPrice;
                    _newReptPickSerials.Tus_qty = _actualQty;//1
                    _newReptPickSerials.Tus_ser_id = 0;//CHNLSVC.Inventory.GetSerialID();
                    _newReptPickSerials.Tus_ser_1 = "N/A";
                    _newReptPickSerials.Tus_ser_2 = "N/A";
                    _newReptPickSerials.Tus_ser_3 = "N/A";
                    _newReptPickSerials.Tus_warr_no = _warrantyno;
                    _newReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                    _newReptPickSerials.Tus_itm_model = txtModel.Text;
                    _newReptPickSerials.Tus_itm_brand = txtBrand.Text;
                    _newReptPickSerials.Tus_itm_line = ItemLineNo;
                    _newReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                    _newReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                    _newReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                    _newReptPickSerials.Tus_itm_model = txtModel.Text;
                    _newReptPickSerials.Tus_itm_brand = txtBrand.Text;
                    _newReptPickSerials.Tus_batch_no = txtBatchNo.Text.Trim();
                    #region add by lakshan 21Sep2017
                    MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (_mstLoc != null)
                    {
                        if (_mstLoc.Ml_cate_1 == "DFS")
                        {
                            _newReptPickSerials.Tus_job_no = txtPORefNo.Text.Trim();
                            _newReptPickSerials.Tus_job_line = ItemLineNo;
                        }
                    }
                    #endregion
                    string _baseItem = Session["baseitem"].ToString();
                    MasterItem _itmExp = CHNLSVC.General.GetItemMasterNew(_inputReptPickSerials.Tus_itm_cd);
                    if (_itmExp != null)
                    {
                        if (_itmExp.Tmp_mi_is_exp_dt == 1)
                        {
                            DateTime _dtTemp = new DateTime();
                            _newReptPickSerials.Tus_exp_dt = DateTime.TryParse(txtEDate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtEDate.Text.Trim()) : DateTime.MinValue;
                            _newReptPickSerials.Tus_manufac_dt = DateTime.TryParse(txtMdate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtMdate.Text.Trim()) : DateTime.MinValue;
                        }
                    }

                    if (string.IsNullOrEmpty(_baseItem))
                    {
                        _baseItem = PopupItemCode;
                    }
                    _newReptPickSerials.Tus_new_itm_cd = _baseItem;
                    // _newReptPickSerials.Tus_exp_dt = Convert.ToDateTime(expirdate);
                    ////kapila 3/7/2015
                    //_newReptPickSerials.Tus_orig_supp = _supplier;
                    //_newReptPickSerials.Tus_exist_supp = _supplier;
                    //_newReptPickSerials.Tus_orig_grnno = _grnNo;
                    //_newReptPickSerials.Tus_exist_grnno = _grnNo;
                    //_newReptPickSerials.Tus_orig_grndt = _grnDate;
                    //_newReptPickSerials.Tus_exist_grndt = _grnDate;
                    //_newReptPickSerials.Tus_batch_no = txtBatch.Text;
                    //_newReptPickSerials.Tus_exp_dt = dtExp.Value.Date;
                    //_newReptPickSerials.Tus_manufac_dt = dtManufc.Value.Date;

                    //if (DocumentType == "ADJ")
                    //{
                    //    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
                    //    if (_period != null)
                    //        _newReptPickSerials.Tus_warr_period = _period.Mwp_val;
                    //}

                    #endregion
                    //Save to the temp table.
                    //CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);//commented by Shani
                    //_newReptPickSerials = null; //commented by Shani

                    //-------added by Shani
                    List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                    var serCount = 0;
                    var serCount_2 = Convert.ToDecimal(0);
                    if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                    {
                        serCount = (from c in _resultItemsSerialList
                                    where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
                                    select c).Count();
                        //for non serial, decimal allowed
                        serCount_2 = (from c in _resultItemsSerialList
                                      where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
                                      select c.Tus_qty).Sum();

                    }


                    if (!chkNewItem.Checked)
                    {
                        if (Convert.ToDecimal(serCount) < Convert.ToDecimal(lblDocQty.Text.Trim()))
                        //if (Convert.ToDecimal(serCount_2) >= Convert.ToDecimal(lblDocQty.Text.Trim()))
                        {

                            //if (Convert.ToDecimal(serCount_2) >= Convert.ToDecimal(lblDocQty.Text.Trim()))
                            //{
                            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required Qty!');", true);
                            //    //MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            bool isExcess = CheckBoxExcess.Checked;
                            lblScanQty.Text = serCount.ToString();
                            if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                            {
                                if (isExcess)
                                {
                                    var _filter1 = _resultItemsSerialList.Where(x => x.Tus_itm_cd == PopupItemCode && x.Tus_unit_cost == 0).ToList();
                                    if (_filter1 != null)
                                    {
                                        if (_filter1.Count > 0)
                                        {
                                            _filter1[0].Tus_qty = Convert.ToDecimal(txtqty.Text) + _filter1[0].Tus_qty;
                                            Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter1[0]);
                                        }
                                        else
                                        {
                                            _newReptPickSerials.Tus_unit_cost = 0;
                                            _newReptPickSerials.Tus_itm_line = 0;
                                            _newReptPickSerials.Tus_unit_price = 0;
                                            _newReptPickSerials.Tus_base_itm_line = 0;
                                            _newReptPickSerials.Tus_job_line = 0;
                                            CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                                        }

                                    }
                                    else
                                    {
                                        _newReptPickSerials.Tus_unit_cost = 0;
                                        _newReptPickSerials.Tus_itm_line = 0;
                                        _newReptPickSerials.Tus_unit_price = 0;
                                        _newReptPickSerials.Tus_base_itm_line = 0;
                                        _newReptPickSerials.Tus_job_line = 0;
                                        CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                                    }

                                }
                                else if (_resultItemsSerialList == null && isExcess)
                                {
                                    _newReptPickSerials.Tus_unit_cost = 0;
                                    _newReptPickSerials.Tus_itm_line = 0;
                                    _newReptPickSerials.Tus_unit_price = 0;
                                    _newReptPickSerials.Tus_base_itm_line = 0;
                                    _newReptPickSerials.Tus_job_line = 0;
                                    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                                }
                                else
                                {
                                    var _filter = _resultItemsSerialList.SingleOrDefault(x => x.Tus_itm_cd == PopupItemCode && x.Tus_itm_line == ItemLineNo&&x.Tus_unit_cost!=0);
                                if (_filter != null)
                                {
                                    _filter.Tus_qty = Convert.ToDecimal(txtqty.Text) + _filter.Tus_qty;
                                    Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                                }
                                else
                                {
                                    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                                }
                            }
                            }
                            else
                            {
                                //Added By Dulaj 2018/Oct/09 to add zero cost fro same item code for excess qty
                                if (isExcess)
                                {
                                    DispMsg("Please select items before add excess items", "W");
                                    return;
                                }
                                else
                                { CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub); }
                                // CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                            }

                            //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
                            ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                            _newReptPickSerials = null;

                            //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                            grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                            grdDOSerials.DataBind();
                            ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                            //SubmitItemSerialData();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required Qty!');", true);
                            //MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        //var _filter = _resultItemsSerialList.SingleOrDefault(x => x.Tus_itm_cd == PopupItemCode && x.Tus_itm_line == ItemLineNo);
                        //if (_filter != null)
                        //{
                        //    _filter.Tus_qty = Convert.ToDecimal(txtqty.Text) ;
                        //    Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                        //}
                        //else
                        //{
                        //    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                        //}
                        #region chg by lakshan null reference error
                        if (_resultItemsSerialList != null)
                        {
                            var _filter = _resultItemsSerialList.SingleOrDefault(x => x.Tus_itm_cd == PopupItemCode && x.Tus_itm_line == ItemLineNo&&x.Tus_unit_cost!=0);
                            if (_filter != null)
                            {
                                _filter.Tus_qty = Convert.ToDecimal(txtqty.Text);
                                Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                            }
                            else
                            {
                                CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                            }
                        }
                        else
                        {
                            CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                        }
                        #endregion
                        ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                        _newReptPickSerials = null;

                        //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                        List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                        grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                        grdDOSerials.DataBind();
                        ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    }
                    //-------added by Shani
                    //Save to the temp table.

                    // }
                    #endregion
                    //txtItemCode.Text = "";
                    //txtModel.Text = "";
                    //txtItemDes.Text = "";
                    //txtBrand.Text = "";
                    //txtpartNo.Text = "";
                    //txtSerial1.Text = "";
                    //txtSerial2.Text = "";
                    //txtSerial3.Text = "";
                    //txtBincode.Text = "";
                    //txtqty.Text = "";
                }
                else if (_itemSerializedStatus == -1) //(Non serialize decimal Item = -1))
                {
                    #region Non-serialized Decimal Allow
                    decimal _actualQty = Convert.ToDecimal(txtqty.Text);
                    string _warrantyno = string.Empty;


                    //Write to the Picked items serials table.
                    ReptPickSerials _newReptPickSerials = new ReptPickSerials();
                    #region Fill Pick Serial Object
                    _newReptPickSerials.Tus_usrseq_no = _userSeqNo;
                    _newReptPickSerials.Tus_doc_no = _scanDocument;
                    _newReptPickSerials.Tus_seq_no = 0;
                    _newReptPickSerials.Tus_itm_line = 0;
                    _newReptPickSerials.Tus_batch_line = 0;
                    _newReptPickSerials.Tus_ser_line = 0;
                    _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                    _inputReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
                    _newReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                    _newReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                    _newReptPickSerials.Tus_bin = _binCode;
                    _newReptPickSerials.Tus_itm_cd = PopupItemCode;
                    _newReptPickSerials.Tus_itm_stus = _itemStatus;
                    _newReptPickSerials.Tus_unit_cost = _unitCost;
                    _newReptPickSerials.Tus_unit_price = _unitPrice;
                    _newReptPickSerials.Tus_qty = _actualQty;//1
                    _newReptPickSerials.Tus_ser_id = 0;
                    _newReptPickSerials.Tus_ser_1 = "N/A";
                    _newReptPickSerials.Tus_ser_2 = "N/A";
                    _newReptPickSerials.Tus_ser_3 = "N/A";
                    _newReptPickSerials.Tus_warr_no = _warrantyno;
                    _newReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                    _newReptPickSerials.Tus_itm_model = txtModel.Text;
                    _newReptPickSerials.Tus_itm_brand = txtBrand.Text;
                    _newReptPickSerials.Tus_itm_line = ItemLineNo;
                    _newReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                    _newReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                    _newReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                    _newReptPickSerials.Tus_itm_model = txtModel.Text;
                    _newReptPickSerials.Tus_itm_brand = txtBrand.Text;
                    _newReptPickSerials.Tus_batch_no = txtBatchNo.Text.Trim();
                    MasterItem _itmExp = CHNLSVC.General.GetItemMasterNew(_newReptPickSerials.Tus_itm_cd);
                    if (_itmExp != null)
                    {
                        if (_itmExp.Tmp_mi_is_exp_dt == 1)
                        {
                            DateTime _dtTemp = new DateTime();
                            _newReptPickSerials.Tus_exp_dt = DateTime.TryParse(txtEDate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtEDate.Text.Trim()) : DateTime.MinValue;
                            _newReptPickSerials.Tus_manufac_dt = DateTime.TryParse(txtMdate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtMdate.Text.Trim()) : DateTime.MinValue;
                        }
                    }
                    string _baseItem = Session["baseitem"].ToString();
                    if (string.IsNullOrEmpty(_baseItem))
                    {
                        _baseItem = PopupItemCode;
                    }
                    _newReptPickSerials.Tus_new_itm_cd = _baseItem;
                    // _newReptPickSerials.Tus_exp_dt = Convert.ToDateTime(expirdate);
                    ////kapila 3/7/2015
                    //_newReptPickSerials.Tus_orig_supp = _supplier;
                    //_newReptPickSerials.Tus_exist_supp = _supplier;
                    //_newReptPickSerials.Tus_orig_grnno = _grnNo;
                    //_newReptPickSerials.Tus_exist_grnno = _grnNo;
                    //_newReptPickSerials.Tus_orig_grndt = _grnDate;
                    //_newReptPickSerials.Tus_exist_grndt = _grnDate;
                    //_newReptPickSerials.Tus_batch_no = txtBatch.Text;
                    //_newReptPickSerials.Tus_exp_dt = dtExp.Value.Date;
                    //_newReptPickSerials.Tus_manufac_dt = dtManufc.Value.Date;

                    //if (DocumentType == "ADJ")
                    //{
                    //    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
                    //    if (_period != null)
                    //        _newReptPickSerials.Tus_warr_period = _period.Mwp_val;
                    //}

                    #endregion
                    //Save to the temp table.
                    //CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);//commented by Shani
                    //_newReptPickSerials = null; //commented by Shani

                    //-------added by Shani
                    List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                    var serCount = 0;
                    if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                    {
                        serCount = (from c in _resultItemsSerialList
                                    where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
                                    select c).Count();
                    }

                    if (!chkNewItem.Checked)
                    {
                        if (Convert.ToDecimal(serCount) < Convert.ToDecimal(lblDocQty.Text.Trim()))
                        //if (Convert.ToDecimal(serCount_2) >= Convert.ToDecimal(lblDocQty.Text.Trim()))
                        {

                            //if (Convert.ToDecimal(serCount_2) >= Convert.ToDecimal(lblDocQty.Text.Trim()))
                            //{
                            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required Qty!');", true);
                            //    //MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            bool isExcess = CheckBoxExcess.Checked;
                            lblScanQty.Text = serCount.ToString();
                            if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                            {
                                if (isExcess)
                                {
                                    var _filter1 = _resultItemsSerialList.Where(x => x.Tus_itm_cd == PopupItemCode&&x.Tus_unit_cost==0).ToList();
                                    if (_filter1 != null)
                                    {
                                        if (_filter1.Count > 0)
                                        {
                                            _filter1[0].Tus_qty = Convert.ToDecimal(txtqty.Text) + _filter1[0].Tus_qty;
                                            Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter1[0]);
                                        }
                                        else
                                        {
                                          
                                            _newReptPickSerials.Tus_unit_cost = 0;
                                            _newReptPickSerials.Tus_itm_line = 0;
                                            _newReptPickSerials.Tus_unit_price = 0;
                                            _newReptPickSerials.Tus_base_itm_line = 0;
                                            _newReptPickSerials.Tus_job_line = 0;
                                            CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                                        }

                                     }
                                        else
                                        {
                                           
                                            _newReptPickSerials.Tus_unit_cost = 0;
                                            _newReptPickSerials.Tus_itm_line = 0;
                                            _newReptPickSerials.Tus_unit_price = 0;
                                            _newReptPickSerials.Tus_base_itm_line = 0;
                                            _newReptPickSerials.Tus_job_line = 0;
                                            CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                                        }
                                    
                              }
                                if(_resultItemsSerialList==null&&isExcess)
                                {
                              
                                    _newReptPickSerials.Tus_unit_cost = 0;
                                    _newReptPickSerials.Tus_itm_line = 0;
                                    _newReptPickSerials.Tus_unit_price = 0;
                                    _newReptPickSerials.Tus_base_itm_line = 0;
                                    _newReptPickSerials.Tus_job_line = 0;
                                    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                                }
                                else
                                {
                                    var _filter = _resultItemsSerialList.SingleOrDefault(x => x.Tus_itm_cd == PopupItemCode && x.Tus_itm_line == ItemLineNo&&x.Tus_unit_cost!=0);
                                if (_filter != null)
                                {
                                    _filter.Tus_qty = Convert.ToDecimal(txtqty.Text) + _filter.Tus_qty;
                                    Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                                }
                                else
                                {
                                    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                                }
                            }
                            }
                            else
                            {
                                //Added By Dulaj 2018/Oct/09 to add zero cost fro same item code for excess qty
                                if (isExcess)
                                {
                                    DispMsg("Please select items before add excess items", "W");
                                    return;
                                }
                                else
                                { CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub); }
                                // CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                            }

                            //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
                            ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                            _newReptPickSerials = null;

                            //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                            grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                            grdDOSerials.DataBind();
                            ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                            //SubmitItemSerialData();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required Qty!');", true);
                            //MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        if (_resultItemsSerialList != null)
                        {
                            var _filter = _resultItemsSerialList.SingleOrDefault(x => x.Tus_itm_cd == PopupItemCode && x.Tus_itm_line == ItemLineNo&&x.Tus_unit_cost!=0);
                            if (_filter != null)
                            {
                                _filter.Tus_qty = Convert.ToDecimal(txtqty.Text);
                                Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                            }
                            else
                            {
                                CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                            }
                        }
                        else
                        {
                            CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                        }
                        ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                        _newReptPickSerials = null;

                        //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                        List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                        grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                        grdDOSerials.DataBind();
                        ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    }
                    //for non serial, decimal allowed
                    //var serCount_2 = (from c in _resultItemsSerialList
                    //                  select c.Tus_qty).Sum();

                    //if (Convert.ToDecimal(serCount) < Convert.ToDecimal(lblDocQty.Text.Trim()))
                    //{
                    //    lblScanQty.Text = serCount.ToString();
                    //    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                    //    //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
                    //    ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                    //    _newReptPickSerials = null;

                    //    //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                    //    List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                    //    grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    //    grdDOSerials.DataBind();
                    //    ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    //    //SubmitItemSerialData();

                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required Qty!');", true);
                    //    //MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    //-------added by Shani
                    //Save to the temp table.

                    // }
                    #endregion
                    //#region Non-serialized Decimal Allow
                    //decimal _actualQty = Convert.ToDecimal(txtqty.Text.Trim());

                    ////Write to the Picked items serials table.
                    //ReptPickSerials _decimalReptPickSerials = new ReptPickSerials();
                    //#region Fill Temp Pick Serial List
                    //_decimalReptPickSerials.Tus_usrseq_no = _userSeqNo;
                    //_decimalReptPickSerials.Tus_doc_no = _scanDocument;
                    //_decimalReptPickSerials.Tus_seq_no = 0;
                    //_decimalReptPickSerials.Tus_itm_line = 0;
                    //_decimalReptPickSerials.Tus_batch_line = 0;
                    //_decimalReptPickSerials.Tus_ser_line = 0;
                    //_decimalReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                    //_decimalReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                    //_decimalReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                    //_decimalReptPickSerials.Tus_bin = _binCode;
                    //_decimalReptPickSerials.Tus_itm_cd = PopupItemCode;
                    //_decimalReptPickSerials.Tus_itm_stus = _itemStatus;
                    //_decimalReptPickSerials.Tus_unit_cost = _unitCost;
                    //_decimalReptPickSerials.Tus_unit_price = _unitPrice;
                    //_decimalReptPickSerials.Tus_qty = _actualQty;
                    ////_decimalReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                    //_decimalReptPickSerials.Tus_ser_1 = "N/A";
                    //_decimalReptPickSerials.Tus_ser_2 = "N/A";
                    //_decimalReptPickSerials.Tus_ser_3 = "N/A";
                    //_decimalReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                    //_decimalReptPickSerials.Tus_itm_model = txtModel.Text;
                    //_decimalReptPickSerials.Tus_itm_brand = txtBrand.Text;
                    //_decimalReptPickSerials.Tus_itm_line = ItemLineNo;
                    //_decimalReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    //_decimalReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                    //_decimalReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                    //_decimalReptPickSerials.Tus_itm_desc = txtItemDes.Text;
                    //_decimalReptPickSerials.Tus_itm_model = txtModel.Text;
                    //_decimalReptPickSerials.Tus_itm_brand = txtBrand.Text;

                    //#endregion
                    //List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                    //if (_resultItemsSerialList == null)
                    //{
                    //    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_decimalReptPickSerials, _reptPickSerialsSub);
                    //    List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                    //    grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    //    grdDOSerials.DataBind();
                    //}
                    //else
                    //{
                    //    //for non serial, decimal allowed
                    //    var serCount_2 = (from c in _resultItemsSerialList
                    //                      where c.Tus_itm_cd == PopupItemCode
                    //                      select c.Tus_qty).Sum();

                    //    if (Convert.ToDecimal(serCount_2) < Convert.ToDecimal(lblDocQty.Text.Trim()))
                    //    {
                    //        lblScanQty.Text = serCount_2.ToString();
                    //        CHNLSVC.Inventory.SaveAllScanSerials(_decimalReptPickSerials, _reptPickSerialsSub);
                    //        ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                    //        _decimalReptPickSerials = null;

                    //        //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                    //        List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);
                    //        grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    //        grdDOSerials.DataBind();
                    //        ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    //        //SubmitItemSerialData();
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required Qty!');", true);
                    //        //MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }
                    //}
                    //#endregion

                }
                LoadPOItems(txtPONo.Text.ToString());
                //_tr.Complete();
                #endregion
                //}

            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo(string DocumentType, string _scanDocument)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = DocumentType;
            RPH.Tuh_cre_dt = Convert.ToDateTime(txtCDate.Text).Date;// DateTime.Today;//might change //Calendar-SelectedDate;
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
        #endregion


        #endregion
        private void Process(bool _IsTemp)
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtPONo.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Select the PO no');", true);
                    //MessageBox.Show("Select the PO no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ImportsCostHeader _cstHdr = CHNLSVC.Financial.GetImpCstHdrForGrn(txtPONo.Text.Trim().ToUpper());
                if (_cstHdr != null)
                {
                    if (_cstHdr.Ich_actl != 1)
                    {
                        DispMsg("Please check the cost data !"); return;
                    }
                }
                PurchaseOrder _purHdr = CHNLSVC.Inventory.GET_PUR_HDR_DATA(Session["UserCompanyCode"].ToString(), txtPONo.Text.Trim().ToUpper());
                if (_purHdr != null)
                {
                    if (_purHdr.Poh_wp == 1)
                    {
                        DispMsg("Document is progress by user : " + _purHdr.Poh_wp_usr); return;
                    }
                }
                if (string.IsNullOrEmpty(txtPORefNo.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter reference number');", true);
                    //MessageBox.Show("Select the PO no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtEntry.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter entry number');", true);
                    //MessageBox.Show("Select the PO no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtBincode.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter bin code');", true);
                    //MessageBox.Show("Select the PO no", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //if (chkManualRef.Checked)
                //    if (string.IsNullOrEmpty(txtManualRefNo.Text))
                //    {
                //        MessageBox.Show("You do not enter a valid manual document no.", "Save...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return;
                //    }

                if (string.IsNullOrEmpty(txtPORefNo.Text)) txtPORefNo.Text = "N/A";
                //if (string.IsNullOrEmpty(txtVehicleNo.Text)) txtVehicleNo.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                ////int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, dtpDODate.Value.Date);
                //int resultDate = DateTime.Compare(Convert.ToDateTime(txtPODate.Text).Date, Convert.ToDateTime(txtDODate.Text).Date);
                //if (resultDate > 0)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('GRN date should be greater than or equal to PO date.');", true);
                //    //MessageBox.Show("GRN date should be greater than or equal to PO date.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDODate, lblH1, txtDODate.Text, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtDODate.Text).Date != DateTime.Now.Date)
                        {
                            txtDODate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction!');", true);
                            //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDODate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        txtDODate.Enabled = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction!');", true);
                        //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDODate.Focus();
                        return;
                    }
                }



                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                InventoryHeader invHdr = new InventoryHeader();
                string documntNo = "";
                Int32 result = -99;

                Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");
                bool diffLoc = false;
                foreach(var serials in reptPickSerialsList)
                {
                    if(serials.Tus_loc!=Session["UserDefLoca"].ToString())
                    {
                        diffLoc = true;
                    }
                }
                if(diffLoc)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Scaned location and saving location is mismatch!');", true);
                    return;
                }
                //check new Item
                if (_checkbaseitem == false)
                {
                    _excelMisMatchReptPickSerials = new List<ReptPickSerials>();
                    _excelMisMatchReptPickSerials = reptPickSerialsList.Where(x => x.Tus_new_itm_cd == "").ToList();
                    if (_excelMisMatchReptPickSerials.Count > 0)
                    {
                        var groupedCustomerList = _excelMisMatchReptPickSerials.GroupBy(x => x.Tus_itm_cd).Select(y => y.First());
                        grdExcelItem.DataSource = groupedCustomerList;
                        grdExcelItem.DataBind();
                        popupexcelitem.Show();
                        return;
                    }
                }



                if (reptPickSerialsList == null)
                {
                    reptPickSerialsList = ViewState["reptPickSerialsList"] as List<ReptPickSerials>;
                }
                if (reptPickSerialsList == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found!');", true);
                    //MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "GRN");

                #region Check Reference Date and the Doc Date

                //if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(dtpDODate.Text).Date) == false)
                //{
                //    Cursor.Current = Cursors.Default;
                //    return;
                //}

                #endregion Check Reference Date and the Doc Date

                #region Check Duplicate Serials

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
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item serials are duplicating. Please remove the duplicated serials. ');", true);
                    //MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                #endregion Check Duplicate Serials

                reptPickSerialsList.ForEach(i => { i.Tus_exist_grncom = Session["UserCompanyCode"].ToString(); i.Tus_exist_grndt = Convert.ToDateTime(txtDODate.Text).Date; i.Tus_exist_supp = txtSuppCode.Text.ToString(); i.Tus_orig_grncom = Session["UserCompanyCode"].ToString(); i.Tus_orig_grndt = Convert.ToDateTime(txtDODate.Text).Date; i.Tus_orig_supp = txtSuppCode.Text.ToString(); });

                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(Session["UserCompanyCode"].ToString(), txtPONo.Text, Session["UserDefLoca"].ToString());
                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList_2 = new List<PurchaseOrderDelivery>();

                //Remove Location check to Popup Validate
                //if (_purchaseOrderDeliveryList == null)
                //{
                //    if (ddlMainType.SelectedValue == "L")
                //    {
                //        string _msg = "Cannot GRN .Please check your location !";
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                //        return;
                //    }
                //}

                if (reptPickSerialsList != null)
                {
                    var _scanItems = reptPickSerialsList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_base_itm_line, x.Tus_qty, x.Tus_itm_stus }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    bool _ismain = (bool)Session["_isserialMaintan"];
                    List<ExcelValidation> _excelValidationListPO = new List<ExcelValidation>();
                    int line = 1;
                    foreach (var itm in _scanItems)
                    {
                        if (_purchaseOrderDeliveryList != null)
                        {

                            _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                            foreach (PurchaseOrderDelivery _invItem in _purchaseOrderDeliveryList)
                            {
                                if (Session["DocType"].ToString() == "TempDoc")
                                {
                                    //if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd && itm.Peo.Tus_base_itm_line == _invItem.Podi_line_no)
                                    //{
                                    if (itm.Peo.Tus_itm_line == _invItem.Podi_line_no) //base item qty add to po qty
                                    {
                                        if (_itemdetail.Mi_is_ser1 == 0)
                                        {
                                            _invItem.Actual_qty = _invItem.Actual_qty + itm.Peo.Tus_qty;// Current scan qty
                                        }
                                        else if (_itemdetail.Mi_is_ser1 == -1)
                                        {
                                            _invItem.Actual_qty = _invItem.Actual_qty + itm.Peo.Tus_qty;// Current scan qty
                                        }
                                        else
                                        {
                                            _invItem.Actual_qty = itm.theCount; // Current scan qty
                                        }
                                    }
                                }
                                else
                                {
                                    //if (itm.Peo.Tus_itm_cd == _invItem.MasterItem.Mi_cd && itm.Peo.Tus_itm_line == _invItem.Podi_line_no)
                                    //{
                                    if (itm.Peo.Tus_itm_line == _invItem.Podi_line_no)//base item qty add to po qty
                                    {

                                        // _itemSerializedStatus = Convert.ToInt32(Session["_itemSerializedStatus"].ToString());

                                        if (_itemdetail.Mi_is_ser1 == 0)
                                        {
                                            _invItem.Actual_qty = _invItem.Actual_qty + itm.Peo.Tus_qty; // Current scan qty'
                                        }
                                        else if (_itemdetail.Mi_is_ser1 == -1)
                                        {
                                            _invItem.Actual_qty = _invItem.Actual_qty + itm.Peo.Tus_qty; // Current scan qty'
                                        }
                                        else
                                        {
                                            if (_ismain == false)
                                            {
                                                _invItem.Actual_qty = _invItem.Actual_qty + itm.Peo.Tus_qty;

                                            }
                                            else
                                            {
                                                _invItem.Actual_qty = itm.theCount;

                                            }
                                            // Current scan qty
                                        }
                                        if (_chkPOQty == false)
                                        {
                                            if (_invItem.Podi_bal_qty < _invItem.Actual_qty)
                                            {
                                                lblrow1.Text = "PO Item qty exceeded";
                                                lblrow2.Text = "PO Item :-" + itm.Peo.Tus_itm_cd + "QTY :-" + _invItem.Podi_bal_qty;
                                                lblrow3.Text = "Pick qty :-" + _invItem.Actual_qty;
                                                lblrow4.Text = "Do you want to continue ?";
                                                mdlconf.Show();
                                                return;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {

                            DataTable po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtPONo.Text, 2);
                            foreach (DataRow _invItem in po_items.Rows)
                            {
                                if (itm.Peo.Tus_itm_line == Convert.ToInt32(_invItem["PODI_LINE_NO"].ToString()))
                                {
                                    MasterItem _componentItem = new MasterItem();
                                    PurchaseOrderDelivery _DeList = new PurchaseOrderDelivery();
                                    _DeList.Podi_seq_no = Convert.ToInt32(_invItem["PODI_SEQ_NO"].ToString());
                                    _DeList.Podi_line_no = Convert.ToInt32(_invItem["PODI_LINE_NO"].ToString());
                                    _DeList.Podi_bal_qty = Convert.ToDecimal(_invItem["PODI_bal_qty"].ToString());
                                    _componentItem.Mi_cd = _invItem["PODI_ITM_CD"].ToString();
                                    _DeList.MasterItem = _componentItem;
                                    _DeList.Podi_itm_stus = _invItem["pod_itm_stus"].ToString();
                                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);
                                    if (_itemdetail.Mi_is_ser1 == 0)
                                    {
                                        _DeList.Actual_qty = _DeList.Actual_qty + itm.Peo.Tus_qty; // Current scan qty'
                                    }
                                    else if (_itemdetail.Mi_is_ser1 == -1)
                                    {
                                        _DeList.Actual_qty = _DeList.Actual_qty + itm.Peo.Tus_qty; // Current scan qty'
                                    }
                                    else
                                    {
                                        if (_ismain == false)
                                        {
                                            _DeList.Actual_qty = _DeList.Actual_qty + itm.Peo.Tus_qty; ;
                                        }
                                        else
                                        {
                                            _DeList.Actual_qty = itm.theCount;
                                        }
                                    }
                                    if (_chkPOQty == false)
                                    {
                                        if (_DeList.Podi_bal_qty < _DeList.Actual_qty)
                                        {
                                            ExcelValidation exl = new ExcelValidation();
                                           // lblrow1.Text = "PO Item qty exceeded";
                                           // lblrow2.Text = "PO Item :-" + itm.Peo.Tus_itm_cd + "QTY :-" + _DeList.Podi_bal_qty;
                                           // lblrow3.Text = "Pick qty :-" + _DeList.Actual_qty;
                                            //lblrow4.Text = "Do you want to continue ?";

                                           // lblerror.Text = "PO Item qty exceeded ";
                                           // lbldata.Text = "PO Item :-" + itm.Peo.Tus_itm_cd + "QTY :-" + _DeList.Podi_bal_qty;
                                           // Label21.Text = "Pick qty :-" + _DeList.Actual_qty;
                                            //lblmsg.Text = "Do you want to continue ?";
                                            // mdlconf.Show();
                                            //PoConfBox.Show();

                                           // lblExcHdr.Text = "PO Item qty exceeded ";
                                            //lblExcBody.Text = "PO Item :-" + itm.Peo.Tus_itm_cd + "QTY :-" + _DeList.Podi_bal_qty;
                                            //lblExcB1.Text = "Pick qty :-" + _DeList.Actual_qty;
                                            exl.Code = itm.Peo.Tus_itm_cd;
                                            exl.RowNo = line;
                                            exl.Error = " PO Remaining QTY :-" + _DeList.Podi_bal_qty + " " + "Pick qty :-" + _DeList.Actual_qty;
                                            lblExcMsg.Text = "Above item exceeded PO/SI Qty. Do you want to continue ?";
                                            line++;
                                            // mdlconf.Show();
                                           // popQtyExc.Show();
                                            _excelValidationListPO.Add(exl);
                                           // return;
                                        }
                                    }
                                  
                                    
                                    _purchaseOrderDeliveryList_2.Add(_DeList);
                                }
                            }
                        }

                        }
                    if (_excelValidationListPO.Count > 0)
                    {
                        GridViewPoExceed.DataSource = _excelValidationListPO;
                        GridViewPoExceed.DataBind();
                        popQtyExc.Show();
                        //  mdlconf.Show();
                        return;
                    }

                }
                if (_purchaseOrderDeliveryList == null)
                {
                    _purchaseOrderDeliveryList = _purchaseOrderDeliveryList_2;
                }
                ////foreach (PurchaseOrderDelivery item in _purchaseOrderDeliveryList)
                ////{
                ////    Int32 _count = 0;

                ////    foreach (ReptPickSerials items in _reptPickSerialList)
                ////    {
                ////        if (item.Podi_line_no == items.Tus_itm_line)
                ////        {
                ////            _count++;

                ////        }

                ////        //_tempPickSerialList = _reptPickSerialList.Where(x => x.Tus_itm_cd.Equals(item.MasterItem.Mi_cd) && x.Tus_itm_line==items.Tus_itm_line ).ToList();
                ////        //item.Actual_qty = _tempPickSerialList.Count;
                ////    }
                ////    item.Actual_qty = Convert.ToDecimal(_count);
                ////    _purList.Add(item);
                ////}

                InventoryHeader _invHeader = new InventoryHeader();

                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    //// Get the value of the wanted column and cast it to string
                    _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"]) _invHeader.Ith_channel = (string)r["ML_CATE_2"]; else _invHeader.Ith_channel = string.Empty;
                }

                _invHeader.Ith_com = Session["UserCompanyCode"].ToString();
                _invHeader.Ith_loc = Session["UserDefLoca"].ToString();
                _invHeader.Ith_doc_date = Convert.ToDateTime(txtDODate.Text).Date;
                _invHeader.Ith_doc_year = Convert.ToDateTime(txtDODate.Text).Date.Year;
                _invHeader.Ith_direct = true;
                _invHeader.Ith_doc_tp = "GRN";
                if (_IsTemp == true)
                {
                    _invHeader.Ith_git_close_doc = _invHeader.Ith_entry_no;
                    _invHeader.Ith_entry_no = _userSeqNo.ToString();
                }
                else
                {
                    _invHeader.Ith_entry_no = txtEntry.Text;
                    _invHeader.Ith_git_close_date = Convert.ToDateTime(txtCDate.Text).Date; //Added by Chamal 08-Sep-2016
                    _invHeader.Ith_git_close_doc = _invHeader.Ith_entry_no;
                }

                if (ddlMainType.SelectedValue == "I")
                {
                    _invHeader.Ith_cate_tp = "IMPORTS";
                    _invHeader.Ith_sub_tp = "IMPORTS";
                }
                else
                {
                    _invHeader.Ith_cate_tp = "LOCAL";
                    _invHeader.Ith_sub_tp = "LOCAL";
                }
                _invHeader.Ith_bus_entity = txtSuppCode.Text;
                if (string.IsNullOrEmpty(txtPORefNo.Text))
                { _invHeader.Ith_is_manual = false; }
                else { _invHeader.Ith_is_manual = true; }
                // if (chkManualRef.Checked == true) _invHeader.Ith_is_manual = true; else _invHeader.Ith_is_manual = false;
                _invHeader.Ith_manual_ref = txtPORefNo.Text;
                _invHeader.Ith_remarks = txtRemarks.Text;
                _invHeader.Ith_stus = "A";
                _invHeader.Ith_cre_by = Session["UserID"].ToString();
                _invHeader.Ith_cre_when = DateTime.Now;
                _invHeader.Ith_mod_by = Session["UserID"].ToString();
                _invHeader.Ith_mod_when = DateTime.Now;
                _invHeader.Ith_session_id = Session["SessionID"].ToString();
                _invHeader.Ith_oth_docno = txtPONo.Text;
                if (_IsTemp == false)
                {
                    _invHeader.Ith_anal_10 = true;
                    _invHeader.Ith_anal_2 = txtDocumentNo.Text;
                }

                #region add by lakshan save supplier invoice no 05Feb2018
                _invHeader.ITH_SUP_INV_NO = txtSupInvNo.Text.Trim();

                DateTime _tmpDt = DateTime.MinValue;
                _invHeader.ITH_SUP_INV_DT = !string.IsNullOrEmpty(txtSupInvDt.Text) ? DateTime.TryParse(txtSupInvDt.Text.Trim(), out _tmpDt) ? Convert.ToDateTime(txtSupInvDt.Text.Trim()) : _tmpDt : _tmpDt;
                #endregion



                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString(); ;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "GRN";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "GRN";
                _masterAuto.Aut_year = _invHeader.Ith_doc_date.Date.Year;

                //Add by Chamal 23-May-2014
                int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(_invHeader.Ith_oth_com, _invHeader.Ith_com, _invHeader.Ith_doc_tp, txtPONo.Text, _invHeader.Ith_doc_date.Date, Session["UserID"].ToString());
                reptPickSerialsList.ForEach(x => x.Tus_doc_dt = _invHeader.Ith_doc_date.Date);
                if (_invHeader.Ith_doc_tp == "GRN")
                {
                    if (_invHeader.Ith_oth_com == "ABL" && _invHeader.Ith_com == "LRP")
                    {
                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                    }
                    if (_invHeader.Ith_oth_com == "SGL" && _invHeader.Ith_com == "SGD")
                    {
                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                    }
                }

                _invHeader.TMP_IS_ALLOCATION = false;
                if (_IsTemp == true)
                {

                    result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, reptPickSubSerialsList, _masterAuto, _purchaseOrderDeliveryList, out documntNo, true);

                    if (result != -99 && result >= 0)
                    {

                        string _msg = "GRN temporary saved ! Document No  : " + documntNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _msg + "');", true);

                        ClearPage();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + documntNo + "');", true);
                        // MessageBox.Show(documntNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!BaseCls.GlbMasterLocation.Ml_is_serial)
                    {
                        foreach (ReptPickSerials item in reptPickSerialsList)
                        {
                            item.Tus_ser_id = 0;
                        }
                        //reptPickSerialsList = new List<ReptPickSerials>();
                    }

                    string ib_doc_no = string.Empty;
                    string ib_bl_no = string.Empty;
                    DateTime? ib_bl_dt = null;
                    string ibi_fin_no = string.Empty;
                    string if_doc_no = string.Empty;
                    string if_file_no = string.Empty;
                    DateTime? if_doc_dt = null;
                    string To_bond = string.Empty;
                    DataTable dtbldata = CHNLSVC.Inventory.LoadBLData(txtPONo.Text.Trim());

                    if (dtbldata.Rows.Count > 0)
                    {
                        foreach (DataRow ddr in dtbldata.Rows)
                        {
                            ib_doc_no = ddr["ib_doc_no"].ToString();
                            ib_bl_no = ddr["ib_bl_no"].ToString();

                            if (!string.IsNullOrEmpty(ddr["ib_bl_dt"].ToString()))
                            {
                                ib_bl_dt = Convert.ToDateTime(ddr["ib_bl_dt"].ToString());
                            }
                            if (!string.IsNullOrEmpty(ddr["ib_ref_no"].ToString()))
                            {
                                To_bond = ddr["ib_ref_no"].ToString();
                            }
                        }
                    }

                    DataTable dtfinnum = CHNLSVC.Inventory.LoadFinDataNumber(ib_doc_no);
                    if (dtfinnum.Rows.Count > 0)
                    {
                        foreach (DataRow ddr1 in dtfinnum.Rows)
                        {
                            ibi_fin_no = ddr1["ibi_fin_no"].ToString();
                        }
                    }

                    DataTable dtfindata = CHNLSVC.Inventory.LoadFinData(ibi_fin_no);

                    if (dtfindata.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtfindata.Rows)
                        {
                            if_doc_no = ddr2["if_doc_no"].ToString();
                            if_file_no = ddr2["if_file_no"].ToString();

                            if (!string.IsNullOrEmpty(ddr2["if_doc_dt"].ToString()))
                            {
                                if_doc_dt = Convert.ToDateTime(ddr2["if_doc_dt"].ToString());
                            }
                        }
                    }
                    MasterLocation _MasterLocation = new MasterLocation();
                    List<PurchaseOrderDetail> _polist = CHNLSVC.Inventory.GetPOItemsList(_invHeader.Ith_oth_docno);

                    _MasterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    foreach (ReptPickSerials itemwarr in reptPickSerialsList)
                    {
                        itemwarr.Tus_orig_grndt = _invHeader.Ith_doc_date;

                        itemwarr.IRSM_SYS_BLNO = ib_doc_no;
                        itemwarr.IRSM_BLNO = ib_bl_no;

                        if (string.IsNullOrEmpty(ib_bl_dt.ToString()))
                        {
                            itemwarr.IRSM_BL_DT = Convert.ToDateTime(DateTime.MinValue);
                        }
                        else
                        {
                            itemwarr.IRSM_BL_DT = Convert.ToDateTime(ib_bl_dt.ToString());
                        }

                        itemwarr.IRSM_SYS_FIN_NO = ibi_fin_no;
                        itemwarr.IRSM_FIN_NO = if_file_no;

                        if (string.IsNullOrEmpty(if_doc_dt.ToString()))
                        {
                            itemwarr.IRSM_FIN_DT = Convert.ToDateTime(DateTime.MinValue);
                        }
                        else
                        {
                            itemwarr.IRSM_FIN_DT = Convert.ToDateTime(if_doc_dt.ToString());
                        }

                        itemwarr.Tus_orig_grnno = "";


                        if (_MasterLocation.Ml_cate_1 == "DFS")
                        {
                            _invHeader.Tobond = To_bond;
                        }

                        if (_polist != null)
                        {
                            if (_polist.Count > 0)
                            {
                                var _filtercost = _polist.Where(x => x.Pod_line_no == itemwarr.Tus_itm_line).ToList();
                                if (_filtercost.Count > 0)
                                {
                                    decimal _cost = _filtercost[0].Pod_act_unit_price;
                                    //if (itemwarr.Tus_unit_cost == 0)
                                    // {
                                    itemwarr.Tus_unit_cost = _cost;
                                    itemwarr.Tus_unit_price = _cost;
                                    //}
                                    //else if (_cost != itemwarr.Tus_unit_cost)
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('unit price missing');", true);
                                    //    return;
                                    //}
                                }
                            }
                        }
                    }
                    _invHeader.TMP_IS_BATCH_NO_NOT_UPDATE = true;
                    _invHeader.TMP_IS_ALLOCATION = true;
                    _invHeader.Ith_gen_frm = "SCMWEB";
                    _invHeader.TmpGrnValidate = true;
                    string _tmpErr = "";
                    CHNLSVC.Inventory.UpdateGrnIsInProgress(_invHeader.Ith_oth_docno, _invHeader.Ith_cre_by, 1, out _tmpErr);
                    result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, reptPickSubSerialsList, _masterAuto, _purchaseOrderDeliveryList, out documntNo);
                    CHNLSVC.Inventory.UpdateGrnIsInProgress(_invHeader.Ith_oth_docno, _invHeader.Ith_cre_by, 0, out _tmpErr);
                    SerialMasterLog SerialMaster = new SerialMasterLog();
                    SerialMaster.Irsm_orig_grn_no = documntNo;
                    SerialMaster.Irsm_doc_dt = DateTime.Now.Date;
                    SerialMaster.Irsm_com = Session["UserCompanyCode"].ToString();
                    SerialMaster.Irsm_loc = Session["UserDefLoca"].ToString();
                    SerialMaster.Irsm_doc_no = documntNo;

                    Int32 UpdateSerMaster = CHNLSVC.Inventory.UpdateSerialMaster(SerialMaster);

                    if (result != -99 && result >= 0)
                    {


                        //DialogResult _res = MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\n" + "Do you want print now?", "Goods Received Note", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        ////txtGRNno.Text = documntNo;
                        //if (_res == System.Windows.Forms.DialogResult.Yes)
                        //{
                        //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        //    BaseCls.GlbReportTp = "INWARD";
                        //    if (Session["UserCompanyCode"].ToString() == "SGL") //Sanjeewa 2014-01-07
                        //        _view.GlbReportName = "SInward_Docs.rpt";
                        //    else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                        //        _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                        //    else
                        //        _view.GlbReportName = "Inward_Docs.rpt";
                        //    _view.GlbReportDoc = documntNo;
                        //    _view.Show();
                        //    _view = null;
                        //}

                        string _msg = "Successfully Saved! Document No : " + documntNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _msg + "');", true);
                        //MessageBox.Show("Successfully Saved! Document No : " + documntNo, "Goods Received Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Session["documntNo"] = documntNo;
                        string _document = Session["documntNo"].ToString();
                        if (_document != "")
                        {
                            printdoc(1);
                        }
                        ClearPage();

                        //  lblMssg.Text = "Successfully Saved! Document No :" + documntNo;
                        //lblMssg.Text = "Do you want print now?";
                        //PopupConfBox.Show();
                        //Session["GlbReportType"] = "SCM1_GRN";
                        //BaseCls.GlbReportDoc = documntNo;
                        //BaseCls.GlbReportHeading = "INWARD DOC";
                        //Session["GlbReportName"] = "Inward_Docs_Full.rpt";
                        //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                    }
                    else
                    {
                        if (documntNo.Contains("EMS.FK_INBBIN"))
                        {
                            documntNo = "Scaned location and saving location is mismatch!";
                        }
                        DispMsg(documntNo);
                        //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + documntNo + "');", true);
                        // MessageBox.Show(documntNo, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + err + "');", true);
                // MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }
        #region Modalpopup

        protected void grdCheckIItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string Name = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Supplier")
            {
                txtFindSupplier.Text = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;

            }
            if (lblvalue.Text == "Po")
            {
                txtFindPONo.Text = Name;
                lblvalue.Text = "";
                UserPopoup.Hide();
                lbtnFilter_Click(null, null);
                return;
            }
            if (lblvalue.Text == "Item")
            {
                DataTable _test = ViewState["po_items"] as DataTable;
                txtItemCode.Text = Name;
                CheckItemAllreadyInPo(Name, _test);



                string Des = grdResult.SelectedRow.Cells[2].Text;
                string Model = grdResult.SelectedRow.Cells[3].Text;
                string Brand = grdResult.SelectedRow.Cells[4].Text;
                string Part = grdResult.SelectedRow.Cells[5].Text;
                txtItemCode.Text = Name;
                txtModel.Text = Model;
                txtBrand.Text = Brand;
                txtpartNo.Text = Part;
                txtItemDes.Text = Des;

                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Bin")
            {
                txtBincode.Text = Name;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "Item_2")
            {
                string Des = grdResult.SelectedRow.Cells[2].Text;
                string Model = grdResult.SelectedRow.Cells[3].Text;
                string Brand = grdResult.SelectedRow.Cells[4].Text;
                string Part = grdResult.SelectedRow.Cells[5].Text;
                txtitemcode_2.Text = Name;
                lbtnmodel_2.Text = Model;
                lbtnbrand_2.Text = Brand;
                lbtnpartno_2.Text = Part;
                lbtnDes_2.Text = Des;
                // lbtnselectitem.Text = "N/A";
                UserPopoup.Hide();
                return;
            }

        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Supplier")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Po")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrdersByType(SearchParams, null, null);
                DataView dv = new DataView(_result);
                string st = "APPROVED";
                dv.RowFilter = "Status ='" + st + "'";
                grdResult.DataSource = dv;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Item") || (lblvalue.Text == "Item_2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "ProductionPlan")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Bin")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable _result = CHNLSVC.General.GetWarehouseBinByLoc(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "baseItem")
            {
                DataTable _result = ViewState["po_items"] as DataTable;
                DataView dv = new DataView(_result);
                dv.RowFilter = "CODE ='" + txtSearchbyword.Text.ToString() + "'";
                grdResult.DataSource = dv;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Supplier")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Po")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrdersByType(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                if (ddlPType.SelectedValue == "1")
                {
                    DataView dv = new DataView(_result);
                    string st = "APPROVED";
                    dv.RowFilter = "Status ='" + st + "'";
                    grdResult.DataSource = dv;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    return;
                }
                else
                {
                    _result.Columns["Ref No"].SetOrdinal(0);
                    DataView dv = new DataView(_result);
                    string st = "APPROVED";
                    dv.RowFilter = "Status ='" + st + "'";
                    grdResult.DataSource = dv;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    return;
                }
            }
            if ((lblvalue.Text == "Item") || (lblvalue.Text == "Item_2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "ProductionPlan")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlan);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Bin")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable _result = CHNLSVC.General.GetWarehouseBinByLoc(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "CheckItem")
            {
                FilterData();
                UserCheckItem.Show();
                return;
            }

        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "baseItem")
            {
                DataTable _result = ViewState["po_items"] as DataTable;
                DataView dv = new DataView(_result);
                dv.RowFilter = "CODE ='" + txtSearchbyword.Text.ToString() + "'";
                grdResult.DataSource = dv;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Supplier")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Po")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrdersByType(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                if (ddlPType.SelectedValue == "1")
                {
                    DataView dv = new DataView(_result);
                    string st = "APPROVED";
                    dv.RowFilter = "Status ='" + st + "'";
                    grdResult.DataSource = dv;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    return;
                }
                else
                {
                    _result.Columns["Ref No"].SetOrdinal(0);
                    DataView dv = new DataView(_result);
                    string st = "APPROVED";
                    dv.RowFilter = "Status ='" + st + "'";
                    grdResult.DataSource = dv;
                    grdResult.DataBind();
                    UserPopoup.Show();
                    return;
                }


            }
            if ((lblvalue.Text == "Item") || (lblvalue.Text == "Item"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Bin")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable _result = CHNLSVC.General.GetWarehouseBinByLoc(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "CheckItem")
            {
                FilterData();
                UserCheckItem.Show();
                return;
            }
        }
        protected void lbtnReplace_Click(object sender, EventArgs e)
        {
            if (grdCheckIItem.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string Name = (row.FindControl("Pop_PODI_ITM_CD") as Label).Text;
                if (lblvalue.Text == "CheckItem")
                {
                    //txtItemCode.Focus();
                    //bool IsSerial = IsItemSerial(Name);
                    //if (IsSerial == true)
                    //{
                    //    divSer.Visible = true;
                    //    Qty.Visible = false;
                    //}
                    //else
                    //{
                    //    divSer.Visible = false;
                    //    Qty.Visible = true;
                    //}
                    string Des = (row.FindControl("Pop_MI_LONGDESC") as Label).Text;
                    string Model = (row.FindControl("Pop_MI_MODEL") as Label).Text;
                    string Brand = (row.FindControl("Pop_MI_BRAND") as Label).Text;
                    Session["UNIT_PRICE"] = (row.FindControl("Pop_UNIT_PRICE") as Label).Text;
                    Session["LineNO"] = (row.FindControl("Pop_PODI_LINE_NO") as Label).Text;
                    lblDocQty.Text = (row.FindControl("Pop_PODI_BAL_QTY") as Label).Text;
                    // txtItemCode.Text = Name;
                    Session["baseitem"] = Name;
                    //LoadPOItems(txtPONo.Text);
                    LoadCommonOutScan_ForScan("GRN", txtPONo.Text, Convert.ToInt32(Session["LineNO"]));

                    // string Part = (row.FindControl("MI_BRAND") as Label).Text;

                    txtModel.Text = Model;
                    txtBrand.Text = Brand;
                    // txtpartNo.Text = Part;
                    //txtItemDes.Text = Des;
                    lblvalue.Text = "";
                    UserCheckItem.Hide();
                }
            }

        }
        #endregion

        #region Modal Popup 2
        public void BindUCtrlDDLData3(DataTable _dataSource)
        {
            this.ddlSearchbykeyDate.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyDate.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyDate.SelectedIndex = 0;
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            grdResultDate.DataSource = _result;
            grdResultDate.DataBind();
            UserDPopoup.Show();
        }
        protected void grdResultDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordDate.ClientID + "').value = '';", true);
            string Name = grdResultDate.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Doc")
            {
                txtDocumentNo.Text = Name;
                Session["documntNo"] = Name;
                lblvalue.Text = "";
                Session["Doc"] = "";
                Session["DocType"] = "Doc";
                // GetDocData(Name);
                //  GetDocDataNew(Name);
                // GetDocDataNew_2(Name);
                UserDPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                txtDocumentNo.Text = Name;
                Session["documntNo"] = Name;
                lblvalue.Text = "";
                Session["TempDoc"] = "";
                Session["DocType"] = "TempDoc";
                GetTempDocData(Name);

                UserDPopoup.Hide();
                return;
            }
        }
        protected void grdResultDate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultDate.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtTDate.Text));
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtTDate.Text));
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
                return;
            }
        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyDate.SelectedItem.Text, txtSearchbywordDate.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyDate.SelectedItem.Text, txtSearchbywordDate.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                lblvalue.Text = "TempDoc";

                UserDPopoup.Show();
                return;
            }
        }
        protected void txtSearchbywordDate_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyDate.SelectedItem.Text, txtSearchbywordDate.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                lblvalue.Text = "Doc";
                Session["Doc"] = "true";
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyDate.SelectedItem.Text, txtSearchbywordDate.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultDate.DataSource = _result;
                grdResultDate.DataBind();
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
                return;
            }
        }
        #endregion

        #region Modalpopup PO

        protected void checkbox_Changed(object sender, EventArgs e)
        {
            chkNewItem.Checked = false;
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16139))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You dont have permission to approve .Permission code : 16139');", true);
                CheckBoxExcess.Checked = false;
                return;
                
            }

        }
        protected void lbtngrdselect_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    string _item = (row.FindControl("lblPOPODI_ITM_CD") as Label).Text;
                    string _price = (row.FindControl("lblPOUNIT_PRICE") as Label).Text;
                    string _lineno = (row.FindControl("lblPOPODI_LINE_NO") as Label).Text;
                    var _fiilterItem = _excelMisMatchReptPickSerials.Where(x => x.Tus_itm_cd == Session["selectitem"].ToString()).ToList();
                    if (_fiilterItem != null)
                    {
                        foreach (ReptPickSerials _itm in _fiilterItem)
                        {
                            _itm.Tus_new_itm_cd = _item;
                            _itm.Tus_unit_cost = Convert.ToDecimal(_price.ToString());
                            _itm.Tus_unit_price = Convert.ToDecimal(_price.ToString());
                            _itm.Tus_job_line = Convert.ToInt32(_lineno.ToString());
                            _itm.Tus_itm_line = Convert.ToInt32(_lineno.ToString());
                        }

                    }


                    var groupedCustomerList = _excelMisMatchReptPickSerials.GroupBy(x => x.Tus_itm_cd).Select(y => y.First());
                    grdExcelItem.DataSource = groupedCustomerList;
                    grdExcelItem.DataBind();

                    lblvalue.Text = "";
                    POpopup.Hide();
                    popupexcelitem.Show();
                    return;

                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void grdpo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdpo.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "baseItem")
            {
                DataTable _result = ViewState["po_items"] as DataTable;
                grdpo.DataSource = _result;
                grdpo.DataBind();
                POpopup.Show();
                popupexcelitem.Show();
                return;
            }
        }
        protected void lbtnposearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "baseItem")
            {
                DataTable _result = ViewState["po_items"] as DataTable;
                if (txtposearch.Text != "")
                {

                    DataView dv = new DataView(_result);
                    dv.RowFilter = "PODI_ITM_CD ='" + txtposearch.Text.ToString() + "'";

                    grdpo.DataSource = dv;
                    grdpo.DataBind();
                    POpopup.Show();
                    popupexcelitem.Show();
                    return;
                }
                else
                {
                    grdpo.DataSource = _result;
                    grdpo.DataBind();
                    POpopup.Show();
                    popupexcelitem.Show();
                    return;
                }
            }
        }
        protected void txtposearch_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "baseItem")
            {
                DataTable _result = ViewState["po_items"] as DataTable;
                if (txtposearch.Text != "")
                {

                    DataView dv = new DataView(_result);
                    dv.RowFilter = "PODI_ITM_CD ='" + txtposearch.Text.ToString() + "'";

                    grdpo.DataSource = dv;
                    grdpo.DataBind();
                    POpopup.Show();
                    popupexcelitem.Show();
                    return;
                }
                else
                {
                    grdpo.DataSource = _result;
                    grdpo.DataBind();
                    POpopup.Show();
                    popupexcelitem.Show();
                    return;
                }
            }

        }

        protected void lbtnPOcolse_Click(object sender, EventArgs e)
        {
            POpopup.Hide();
            popupexcelitem.Show();
        }

        #endregion
        protected void lbtnWGRN_Click(object sender, EventArgs e)
        {
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(txtPONo.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Select the PO no');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtSupInvNo.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Select the invoice no');", true);
                return;
            }
            try
            {
                List<PurchaseOrderDelivery> PODeliveryList = new List<PurchaseOrderDelivery>();
                if (txtSavelconformmessageValue.Value == "Yes")
                {
                    bool _invalidLoc = false;
                    PODeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(Session["UserCompanyCode"].ToString(), txtPONo.Text, null);
                    if (PODeliveryList != null)
                    {
                        if (PODeliveryList.Count > 0)
                        {
                            var _po = PODeliveryList.Where(c => c.Podi_loca != Session["UserDefLoca"].ToString()).ToList();
                            if (_po != null)
                            {
                                if (_po.Count > 0)
                                {
                                    _invalidLoc = true;
                                }
                            }
                        }
                    }
                    if (_invalidLoc)
                    {
                        lblerror.Text = "Invalid location find...!!! ";
                        lbldata.Text = PODeliveryList.FirstOrDefault().Podi_loca;
                        lblmsg.Text = "Do you want to continue ?";
                        PoConfBox.Show();
                    }
                    else
                    {
                        Process(false);
                    }
                    //if (PODeliveryList != null || PODeliveryList.Count > 0)
                    //{
                    //    for (int i = 1; i <= PODeliveryList.Count; i++)
                    //    {
                    //        if (PODeliveryList.FirstOrDefault().Podi_loca != Session["UserDefLoca"].ToString())
                    //        {
                    //            lblerror.Text = "Invalid location find...!!! ";
                    //            lbldata.Text = PODeliveryList.FirstOrDefault().Podi_loca;
                    //            lblmsg.Text = "Do you want to continue ?";
                    //            PoConfBox.Show();
                    //        }
                    //    }
                    //}
                    //else 
                    //{
                    //    Process(false); 
                    //}
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnTempSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSavelconformmessageValue.Value == "Yes")
                {

                    Process(true);
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        protected void lbtnSearch_Supplier_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Supplier";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
                // ErrorMsgGRN("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnSearch_PO_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMainType.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the Type');", true);
                    return;
                }
                if (ddlPType.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the Order');", true);
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                //  DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(SearchParams, null, null);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrdersByType(SearchParams, null, null);
                if (ddlPType.SelectedValue == "1")
                {
                    DataView dv = new DataView(_result);
                    string st = "APPROVED";
                    dv.RowFilter = "Status ='" + st + "'";
                    grdResult.DataSource = dv;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "Po";
                    UserPopoup.Show();
                }
                else if (ddlPType.SelectedValue == "2")
                {
                    _result.Columns["Ref No"].SetOrdinal(0);
                    DataView dv = new DataView(_result);
                    string st = "APPROVED";
                    dv.RowFilter = "Status ='" + st + "'";

                    grdResult.DataSource = dv;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "Po";
                    UserPopoup.Show();
                }
                else
                {
                    _result.Columns["Job No"].SetOrdinal(0);
                    DataView dv = new DataView(_result);
                    string st = "APPROVED";
                    dv.RowFilter = "Status ='" + st + "'";

                    grdResult.DataSource = dv;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "Po";
                    UserPopoup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
                // ErrorMsgGRN("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMainType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the Type');", true);
                    return;
                }
                if (!string.IsNullOrEmpty(txtFindSupplier.Text))
                {
                    GetPendingPurchaseOrders(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtFindSupplier.Text, txtFindPONo.Text, null, ddlMainType.SelectedValue, true, null);
                }
                else if (!string.IsNullOrEmpty(txtFindPONo.Text))
                {
                    if (ddlPType.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the Order');", true);
                        return;
                    }
                    if (ddlPType.SelectedValue == "1")
                    {
                        GetPendingPurchaseOrders(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtFindSupplier.Text, txtFindPONo.Text, null, ddlMainType.SelectedValue, true, null);
                    }
                    else if (ddlPType.SelectedValue == "4")
                    {
                        GetPendingPurchaseOrders(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtFindSupplier.Text, null, null, ddlMainType.SelectedValue, true, txtFindPONo.Text);
                    }
                    else if (ddlPType.SelectedValue == "5")
                    {
                        GetPendingPurchaseOrders(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtFindSupplier.Text, null, null, ddlMainType.SelectedValue, true, txtFindPONo.Text);
                    }
                    else
                    {
                        GetPendingPurchaseOrders(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtFindSupplier.Text, null, txtFindPONo.Text, ddlMainType.SelectedValue, true, null);
                    }
                }
                else
                {
                    GetPendingPurchaseOrders(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtFindSupplier.Text, txtFindPONo.Text, null, ddlMainType.SelectedValue, true, null);
                }

                List<ReptPickSerials> _emptyserList = new List<ReptPickSerials>();
                _emptyserList = null;
                grdDOSerials.AutoGenerateColumns = false;
                grdDOSerials.DataSource = _emptyserList;
                grdDOSerials.DataBind();
                List<InvoiceItem> _emptyinvoiceitemList = new List<InvoiceItem>();
                _emptyinvoiceitemList = null;
                grdDOItems.AutoGenerateColumns = false;
                grdDOItems.DataSource = new int[] { };
                grdDOItems.DataBind();
                ddlMainType.Enabled = false;

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        protected void txtFindSupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindSupplier.Text)) return;

                if (!CHNLSVC.Inventory.IsValidSupplier(Session["UserCompanyCode"].ToString(), txtFindSupplier.Text, 1, "S"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid supplier');", true);
                    //MessageBox.Show("Please select the valid supplier", "Supplier Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindSupplier.Text = "";
                    txtFindSupplier.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                CHNLSVC.CloseChannel();
            }
            finally
            {

            }
        }

        protected void txtFindPONo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindPONo.Text)) return;

                if (ddlMainType.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the Type');", true);
                    return;
                }
                if (ddlPType.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the Order');", true);
                    return;
                }
                if (ddlPType.SelectedValue == "1")
                {
                    PurchaseOrder _hdr = CHNLSVC.Inventory.GetPOHeader(Session["UserCompanyCode"].ToString(), txtFindPONo.Text.Trim(), ddlMainType.SelectedValue);//L

                    if (_hdr == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid purchase order no');", true);

                        txtFindPONo.Text = string.Empty;
                        txtFindPONo.Focus();
                        return;
                    }
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                    DataTable _result = CHNLSVC.Inventory.GetPurchaseOrdersByType(SearchParams, "Ref No", "%" + txtSearchbyword.Text.ToString());
                    if (_result.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid Ref no');", true);

                        txtFindPONo.Text = string.Empty;
                        txtFindPONo.Focus();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        protected void lbtnPSelect_Click(object sender, EventArgs e)
        {
            //Added by Dulaj 2018 MAr 22
            CheckExessQty();
            //
            chkPickTop.Checked = false;
            _itmPick = false;
            _itmPickItemCode = "";
            _itmPickLine = -1;
            hfScrollPositionItem.Value = "0";
            if (grdPendingPo.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                grdPendingPo.BackColor = System.Drawing.Color.Transparent;
                row.BackColor = System.Drawing.Color.Transparent;
                string _line = (row.FindControl("POH_REMARKS") as Label).Text;
                txtPONo.Text = (row.FindControl("POH_DOC_NO") as Label).Text;
                txtPODate.Text = (row.FindControl("POH_DT") as Label).Text;
                txtPORefNo.Text = (row.FindControl("POH_REF") as Label).Text;
                txtSuppCode.Text = (row.FindControl("POH_SUPP") as Label).Text;
                txtSuppName.Text = (row.FindControl("MBE_NAME") as Label).Text;
                ////_profitCenter = dvPendingPO.Rows[e.RowIndex].Cells["POH_PROFIT_CD"].Value.ToString();
                _poType = (row.FindControl("POH_TP") as Label).Text; //Add by Chamal 31/07/2014
                txtRemarks.Text = (row.FindControl("POH_REMARKS") as Label).Text;// added by  Nadeeka 18-02-2015
                //Load Entry number
                ImpCusdecHdr _cusHdr = new ImpCusdecHdr();
                _cusHdr = CHNLSVC.Financial.GET_CUSTDEC_HDR_BY_DOC(txtPONo.Text);

                if (_cusHdr != null)
                {
                    if (_cusHdr.CUH_CUSDEC_ENTRY_NO != null)
                    {
                        txtEntry.Text = _cusHdr.CUH_CUSDEC_ENTRY_NO;
                    }
                    else
                    {
                        txtEntry.Text = "";
                    }
                }

                LoadPOItems(txtPONo.Text.ToString());
                //loadInterCompanyPOSerials(txtPONo.Text.Trim(), txtSuppCode.Text.Trim(), txtPORefNo.Text.Trim());

                bool _isserialMaintan = Convert.ToBoolean(Session["_isserialMaintan"]);
                #region add by lakshan 21Sep2017
                MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                bool _isBondLoc = false;
                if (_mstLoc != null)
                {
                    if (_mstLoc.Ml_is_serial && _mstLoc.Ml_cate_1 == "DFS")
                    {
                        //_isBondLoc = true;
                    }
                }
                #endregion
                if (_isserialMaintan == false)
                {
                    pnlserialMaintan_false.Visible = true;
                    pnlserialMaintan_true.Visible = false;
                    // return;
                }
                else if (_isBondLoc)
                {
                    pnlserialMaintan_false.Visible = true;
                    pnlserialMaintan_true.Visible = false;
                }
                else
                {

                    pnlserialMaintan_false.Visible = false;
                    pnlserialMaintan_true.Visible = true;
                }

                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                // //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");
                _serList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");

                if (_serList == null)
                {
                    if (chkAODoutserials.Checked)
                    {
                        DataTable po_items = new DataTable();
                        if (ddlMainType.SelectedValue == "L")
                        {
                            po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtPONo.Text.ToString(), 1);
                        }
                        else
                        {
                            po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtPONo.Text.ToString(), 2);
                            foreach (DataRow drValue in po_items.Rows)
                            {
                                if (drValue["podi_bal_qty"].ToString() == "0")
                                {
                                    drValue.Delete();
                                }

                            }

                            po_items.AcceptChanges();
                        }
                        #region option 3
                        string _docno = string.Empty;
                        foreach (DataRow drValue in po_items.Rows)
                        {
                            MasterItem msitem = new MasterItem();
                            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), drValue["PODI_ITM_CD"].ToString());
                            MasterLocation _MasterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                            if (_MasterLocation.Ml_cate_1 == "DFS")
                            {
                                msitem.Mi_is_ser1 = 0;
                            }
                            if (msitem.Mi_is_ser1 == 0)
                            {

                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_usrseq_no = _userSeqNo;
                                _reptPickSerial_.Tus_seq_no = _userSeqNo;
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_base_doc_no = txtPONo.Text.ToString();
                                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32((drValue["PODI_LINE_NO"].ToString()));
                                _reptPickSerial_.Tus_new_itm_cd = drValue["PODI_ITM_CD"].ToString();
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                                _reptPickSerial_.Tus_itm_cd = drValue["PODI_ITM_CD"].ToString();
                                _reptPickSerial_.Tus_itm_stus = drValue["POD_ITM_STUS"].ToString();
                                _reptPickSerial_.Tus_itm_line = Convert.ToInt32((drValue["PODI_LINE_NO"].ToString()));
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(drValue["PODI_BAL_QTY"].ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_unit_cost = Convert.ToDecimal(drValue["UNIT_PRICE"].ToString());
                                _reptPickSerial_.Tus_unit_price = Convert.ToDecimal(drValue["UNIT_PRICE"].ToString());
                                //_reptPickSerial_.Tus_unit_price = drValue["PODI_ITM_CD"].ToString();
                                // _reptPickSerial_.Tus_job_no = _itm.Sad_inv_no;
                                _reptPickSerial_.Tus_doc_no = txtPONo.Text;
                                _reptPickSerial_.Tus_job_line = Convert.ToInt32((drValue["PODI_LINE_NO"].ToString()));
                                _reptPickSerial_.Tus_job_no = txtPORefNo.Text;
                                _reptPickSerial_.Tus_batch_no = txtBatchNo.Text.Trim();
                                MasterItem _itmExp = CHNLSVC.General.GetItemMasterNew(_reptPickSerial_.Tus_itm_cd);
                                if (_itmExp != null)
                                {
                                    if (_itmExp.Tmp_mi_is_exp_dt == 1)
                                    {
                                        DateTime _dtTemp = new DateTime();
                                        _reptPickSerial_.Tus_exp_dt = DateTime.TryParse(txtEDate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtEDate.Text.Trim()) : DateTime.MinValue;
                                        _reptPickSerial_.Tus_manufac_dt = DateTime.TryParse(txtMdate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtMdate.Text.Trim()) : DateTime.MinValue;
                                    }
                                }
                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                            }
                        }

                        #endregion


                        LoadPOItems(txtPONo.Text.ToString());

                    }
                }

                // txtManualRefNo.;
                // txtVehicleNo.Clear();
            }
        }



        protected void lbtnIcode_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBincode.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Bin');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtSuppCode.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Purchase Order');", true);
                    return;
                }
                if (chkNewItem.Checked)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    ddlSearchbykey.Items.FindByText("Description").Enabled = false;
                    lblvalue.Text = "Item";
                    UserPopoup.Show();
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    ddlSearchbykey.Items.FindByText("Description").Enabled = false;
                    lblvalue.Text = "Item";
                    UserPopoup.Show();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing.');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        protected void lbtnBincode_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable _result = CHNLSVC.General.GetWarehouseBinByLoc(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Bin";
                UserPopoup.Show();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void ddlMainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPage();
            if (ddlMainType.SelectedItem.Text == "Imports")
            {
                DataTable _result = CHNLSVC.Inventory.GetOrderStatus("I");
                ddlStatus.DataSource = _result;
                ddlStatus.DataTextField = "mis_desc";
                ddlStatus.DataValueField = "mis_cd";
                ddlStatus.DataBind();
                ddlStatus.SelectedValue = "GOD";

                ddlStatus_2.DataSource = _result;
                ddlStatus_2.DataTextField = "mis_desc";
                ddlStatus_2.DataValueField = "mis_cd";
                ddlStatus_2.DataBind();
                ddlStatus_2.SelectedValue = "GOD";

                ddlSIStatus.DataSource = _result;
                ddlSIStatus.DataTextField = "mis_desc";
                ddlSIStatus.DataValueField = "mis_cd";
                ddlSIStatus.DataBind();
                ddlSIStatus.SelectedIndex = 5;
                ddlSIStatus.SelectedValue = "GOD";
                return;
            }
            DataTable _result1 = CHNLSVC.Inventory.GetOrderStatus("A");
            ddlStatus.DataSource = _result1;
            ddlStatus.DataTextField = "mis_desc";
            ddlStatus.DataValueField = "mis_cd";
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = "GDLP";

            ddlStatus_2.DataSource = _result1;
            ddlStatus_2.DataTextField = "mis_desc";
            ddlStatus_2.DataValueField = "mis_cd";
            ddlStatus_2.DataBind();
            ddlStatus_2.SelectedValue = "GDLP";


            ddlSIStatus.DataSource = _result1;
            ddlSIStatus.DataTextField = "mis_desc";
            ddlSIStatus.DataValueField = "mis_cd";
            ddlSIStatus.DataBind();
            ddlSIStatus.SelectedValue = "GDLP";

        }

        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            //DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + txtItemCode.Text.ToString());
            //if (_result.Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected item is not attach to supplier.');", true);
            //    txtqty.Text = "";
            //    txtItemCode.Focus();
            //    return;
            //}
            if (Session["_itemSerializedStatus"].ToString() == "0")
            {
                int integer;
                if (!Int32.TryParse(txtqty.Text, out integer))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This item not allow decimal value!');", true);
                    txtqty.Text = "";
                    return;
                }
                if (txtqty.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('can't add zero value!');", true);
                    txtqty.Text = "";
                    return;
                }
            }
            else if (Session["_itemSerializedStatus"].ToString() == "-1")
            {

            }
            //Added By Dulaj 2018/Mar/21     

            Int32 _checkExessQty = 0;
            if (Session["ChecExess"] != null)
            {
                _checkExessQty = Convert.ToInt32(Session["ChecExess"].ToString());
            }
            if (_checkExessQty == 1)
            {

                DataTable excces = ViewState["po_items"] as DataTable;
                foreach (DataRow dr in excces.Rows)
                {

                    string itemcode = dr["PODI_ITM_CD"].ToString().Trim();
                    if (itemcode.Trim().Equals(txtItemCode.Text.Trim()))
                    {
                        if (Convert.ToInt64(txtqty.Text) > Convert.ToInt64(dr["PODI_BAL_QTY"].ToString().Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Do not have permission to add excess quantity');", true);
                            txtqty.Text = string.Empty;
                            return;
                        }
                    }
                }
            }
            ///////
            lbtnItemAdd_Click(null, null);
        }
        protected void btnqty_Click(object sender, EventArgs e)
        {
            txtqty_TextChanged(null, null);
        }

        protected void lbtnItemAdd_Click(object sender, EventArgs e)
        {
            _gridEdit = false;
            if (string.IsNullOrEmpty(txtPONo.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select PO');", true);
                return;
            }
            if (string.IsNullOrEmpty(Session["UNIT_PRICE"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Item');", true);
                return;
            }
            if ((Session["GlbDefaultBin"] == "") || (Session["GlbDefaultBin"] == null))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('default bin not setup');", true);
                return;
            }
            _itemSerializedStatus = Convert.ToInt32(Session["_itemSerializedStatus"].ToString());
            if (_itemSerializedStatus == 0)
            {
                if (string.IsNullOrEmpty(txtqty.Text)) return;
                AddItemQuantites("GRN", txtPONo.Text, txtItemCode.Text, Convert.ToDecimal(Session["UNIT_PRICE"]), Convert.ToDecimal(Session["UNIT_PRICE"]), Convert.ToInt32(Session["LineNO"]), Convert.ToDecimal(Session["scanQty"]));

                _itmPick = true;
                _itmPickItemCode = txtItemCode.Text;
                _itmPickLine = Convert.ToInt32(Session["LineNO"]);
                txtqty.Text = "";
                txtqty.Focus();
                LoadPOItems(txtPONo.Text);
                return;


            }
            else if (_itemSerializedStatus == 1)
            {
                _itmPick = true;
                _itmPickItemCode = txtItemCode.Text;
                _itmPickLine = Convert.ToInt32(Session["LineNO"]);
                if (string.IsNullOrEmpty(txtSerial1.Text)) return;

                txtSerial1.Text = txtSerial1.Text.Replace("'", "").ToString();

                if (txtSerial2.Enabled == false)
                {
                    AddItemQuantites("GRN", txtPONo.Text, txtItemCode.Text, Convert.ToDecimal(Session["UNIT_PRICE"]), Convert.ToDecimal(Session["UNIT_PRICE"]), Convert.ToInt32(Session["LineNO"]), Convert.ToDecimal(Session["scanQty"]));
                    txtSerial1.Text = "";
                    txtSerial1.Focus();
                    return;
                }
                else
                {
                    txtSerial1.Enabled = false;
                    txtSerial2.Text = "";
                    txtSerial2.Focus();
                }

            }
            else if (_itemSerializedStatus == 2)
            {
                _itmPick = true;
                _itmPickItemCode = txtItemCode.Text;
                _itmPickLine = Convert.ToInt32(Session["LineNO"]);
                if (string.IsNullOrEmpty(txtSerial1.Text) || string.IsNullOrEmpty(txtSerial2.Text)) return;

                txtSerial1.Text = txtSerial1.Text.Replace("'", "").ToString();
                txtSerial2.Text = txtSerial2.Text.Replace("'", "").ToString();

                if (txtSerial3.Enabled == false)
                {
                    AddItemQuantites("GRN", txtPONo.Text, txtItemCode.Text, Convert.ToDecimal(Session["UNIT_PRICE"]), Convert.ToDecimal(Session["UNIT_PRICE"]), Convert.ToInt32(Session["LineNO"]), Convert.ToDecimal(Session["scanQty"]));
                    txtSerial1.Enabled = true;

                    if (Session["Subserial"].ToString() != "true")
                    {
                        //printwarranty();
                        printWarrentyNew();
                        txtSerial1.Text = "";
                    }
                    //txtSerial1.Text = "";//422 need to clear serial--subserial print need to comment
                    txtSerial2.Text = "";
                    txtSerial1.Focus();
                    _itmPick = true;
                    _itmPickItemCode = txtItemCode.Text;
                    _itmPickLine = Convert.ToInt32(Session["LineNO"]);

                }
                else
                {
                    txtSerial1.Enabled = false;
                    txtSerial3.Text = "";
                    txtSerial3.Focus();
                }

            }
            else
            {
                if (string.IsNullOrEmpty(txtqty.Text)) return;

                AddItemQuantites("GRN", txtPONo.Text, txtItemCode.Text, Convert.ToDecimal(Session["UNIT_PRICE"]), Convert.ToDecimal(Session["UNIT_PRICE"]), Convert.ToInt32(Session["LineNO"]), Convert.ToDecimal(Session["scanQty"]));
                txtqty.Text = "";
                txtqty.Focus();
                _itmPick = true;
                _itmPickItemCode = txtItemCode.Text;
                _itmPickLine = Convert.ToInt32(Session["LineNO"]);
                LoadPOItems(txtPONo.Text);
                return;
                //// if (string.IsNullOrEmpty(txtSerial1.Text) || string.IsNullOrEmpty(txtSerial2.Text) || string.IsNullOrEmpty(txtSerial3.Text)) return;

                // txtSerial1.Text = txtSerial1.Text.Replace("'", "").ToString();
                // txtSerial2.Text = txtSerial2.Text.Replace("'", "").ToString();
                // txtSerial3.Text = txtSerial3.Text.Replace("'", "").ToString();

                // if (_isFactBase == true)
                // {
                //     if (!IsNumeric(txtSerial2.Text))
                //     {
                //         ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid weight. Pls. check');", true);
                //         //MessageBox.Show("Invalid weight. Pls. check", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //         txtSerial2.Text = "";
                //         txtSerial2.Focus();
                //         return;
                //     }

                //     if (!IsNumeric(txtSerial3.Text))
                //     {
                //         ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid factor rate. Pls. check');", true);
                //         //MessageBox.Show("Invalid factor rate. Pls. check", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //         txtSerial3.Text = "";
                //         txtSerial3.Focus();
                //         return;
                //     }

                //     if (Convert.ToDecimal(txtSerial3.Text) > 100)
                //     {
                //         ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid factor rate. Pls. check');", true);
                //         //MessageBox.Show("Invalid factor rate. Pls. check", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //         txtSerial3.Text = "";
                //         txtSerial3.Focus();
                //         return;
                //     }

                //     if (Convert.ToDecimal(txtSerial3.Text) <= 0)
                //     {
                //         ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid factor rate. Pls. check');", true);
                //         //MessageBox.Show("Invalid factor rate. Pls. check", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //         txtSerial3.Text = "";
                //         txtSerial3.Focus();
                //         return;
                //     }

                // }
                // AddItemQuantites("GRN", txtPONo.Text, txtItemCode.Text, Convert.ToDecimal(Session["UNIT_PRICE"]), 0, Convert.ToInt32(Session["LineNO"]), Convert.ToDecimal(Session["scanQty"]));
                // txtSerial1.Enabled = true;
                // // txtSerial1.Text = "";
                // //txtSerial2.Text = "";
                // //txtSerial3.Text = "";
                // txtSerial1.Focus();

            }








        }
        protected void lbtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "No")
                {
                    return;
                }
                if (grdDOSerials.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string ItemCode = (row.FindControl("TUS_ITM_CD") as Label).Text;
                    if (string.IsNullOrEmpty(ItemCode)) return;

                    string _item = (row.FindControl("TUS_ITM_CD") as Label).Text; ;
                    string _status = (row.FindControl("TUS_ITM_STUS") as Label).Text; ;
                    Int32 _serialID = Convert.ToInt32((row.FindControl("TUS_SER_ID") as Label).Text);
                    string _bin = (row.FindControl("TUS_BIN") as Label).Text;
                    string serial_1 = (row.FindControl("TUS_SER_1") as Label).Text;
                    Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);

                    MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                    if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0 || _masterItem.Mi_is_ser1 == -1)
                    {
                        CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, Convert.ToInt32(_serialID), _item, serial_1);
                        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                    }
                    else
                    {
                        CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _item, _status);
                    }

                    LoadPOItems(txtPONo.Text);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtSerial1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSerial1.Text.Trim()))
            {
                DispMsg("Serial number cannot be empty", "W");
                return;
            }            
            if (CheckBoxQR.Checked)
            {
                int method = Convert.ToInt32(DropDownListQRCom.SelectedValue.ToString());
                if (method != 0)
                {
                    string newSerial = GetSerial(txtSerial1.Text, method);
                    if (!(string.IsNullOrEmpty(newSerial)))
                    {
                        txtSerial1.Text = newSerial;
                    }
                }
                else
                {
                    DispMsg("Please Select a company", "W");
                    txtSerial1.Text = string.Empty;
                    return;
                }
            }
            else
            {
                DataTable paraTB = CHNLSVC.Inventory.getMstSysPara(Session["UserCompanyCode"].ToString(), "COM", Session["UserCompanyCode"].ToString(), "SERLENRES", Session["UserCompanyCode"].ToString());
                if (paraTB != null)
                {
                    
                    if (paraTB.Rows.Count > 0)
                    {                        
                        if (!(string.IsNullOrEmpty(paraTB.Rows[0]["MSP_REST_VAL"].ToString())))
                        {
                            int lenght = 0;
                            lenght = Convert.ToInt32(paraTB.Rows[0]["MSP_REST_VAL"].ToString());
                            if (txtSerial1.Text.Trim().Length > lenght)
                            {
                                DispMsg("Serial length is more than "+lenght, "W");
                                txtSerial1.Text = string.Empty;
                                return;
                            }
                        }
                    }
                }               
            }
            Session["MainItemSerial"] = txtSerial1.Text;
            // _MasterItemComponent = CHNLSVC.General.getitemComponent(txtItemCode.Text);
            //_MasterItemComponent.OrderByDescending(x => x.Micp_itm_tp).ThenBy(n => n.Micp_comp_itm_cd);
            // GgdsubItem.DataSource=_MasterItemComponent;
            // GgdsubItem.DataBind();
            // userSubSerial.Show();
            // Session["Subserial"] = "true";
            // ViewState["SubItem"] = _MasterItemComponent;
        }
        //Dulaj 2018/Oct/12
        private string GetSerial(string scannedCode, int method)
        {
            string serial = scannedCode;
            //   if (serDet != null)
            //   {
            //   if (serDet.Rows.Count > 0)
            //{
            DataTable qrMethods = new DataTable();

            if (method != 0)
            {
                qrMethods = CHNLSVC.Inventory.GetQRMethod(Session["UserCompanyCode"].ToString(), method);
                if (qrMethods != null)
                {
                    if (qrMethods.Rows.Count > 0)
                    {
                        Int32 serialPosition = Convert.ToInt32(qrMethods.Rows[0]["MQM_SERIAL_POS"].ToString());
                        string separator = qrMethods.Rows[0]["MQM_CHAR"].ToString();
                        bool checkqr = scannedCode.Contains(separator);
                        if (checkqr)
                        {
                            string[] tokens = scannedCode.Split(new[] { separator }, StringSplitOptions.None);
                            serial = tokens[serialPosition];
                        }                       
                    }
                }
            }
            // }
            //    }
            return serial;
        }
        protected void lbtnAddItem_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            //DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + txtItemCode.Text.ToString());
            //if (_result.Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please type correct item code');", true);
            //    txtqty.Text = "";
            //    txtItemCode.Focus();
            //    return;
            //}
            if (Session["DivVisible"].ToString() == "true")
            {
                divAddItem.Visible = false;
                Session["DivVisible"] = "false";
            }
            else
            {
                divAddItem.Visible = true;
                Session["DivVisible"] = "true";

            }
            // Session["DivVisible"] = "true";

        }

        protected void test_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSerial1.Text.Trim()))
            {
                DispMsg("Serial number cannot be empty", "W");
                return;
            }
            if (!CheckBoxQR.Checked)
            {                DataTable paraTB = CHNLSVC.Inventory.getMstSysPara(Session["UserCompanyCode"].ToString(), "COM", Session["UserCompanyCode"].ToString(), "SERLENRES", Session["UserCompanyCode"].ToString());
                if (paraTB != null)
                {

                    if (paraTB.Rows.Count > 0)
                    {
                        if (!(string.IsNullOrEmpty(paraTB.Rows[0]["MSP_REST_VAL"].ToString())))
                        {
                            int lenght = 0;
                            lenght = Convert.ToInt32(paraTB.Rows[0]["MSP_REST_VAL"].ToString());
                            if (txtSerial1.Text.Trim().Length > lenght)
                            {
                                DispMsg("Serial length is more than " + lenght, "W");
                                txtSerial1.Text = string.Empty;
                                return;
                            }
                        }
                    }
                }             
            }else
            {

                int method = Convert.ToInt32(DropDownListQRCom.SelectedValue.ToString());
                if (method == 0)
                {
                    DispMsg("Please Select a company", "W");                   
                    txtSerial1.Text = string.Empty;
                    return;
               
                }              
                   
                
            }
            Session["MainItemSerial"] = txtSerial1.Text;

            if (!string.IsNullOrEmpty(txtSerial1.Text) && Session["ValidateFilePath"] != null)
            {
                DataTable[] GetExecelTbl = LoadValidateData(Session["ValidateFilePath"].ToString());
                if (GetExecelTbl == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Excel Data Invalid Please check Excel File and Upload');", true);
                    return;
                }
                if (GetExecelTbl[0].Rows.Count < 1 && GetExecelTbl[0].Columns.Count < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Excel Data Invalid Please check Excel File and Upload');", true);
                    return;
                }
                bool valid = false;
                for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                {
                    string _serial1 = GetExecelTbl[0].Rows[i][0].ToString();
                    if (txtSerial1.Text == _serial1)
                    {
                        valid = true;
                    }
                }
                if (!valid)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Scanned serial not exist in excel file ');", true);
                    return;
                }
            }


            if (Session["_itemSerializedStatus"].ToString() == "1")
            {
                //if (_ExcelSerial != null)
                //{
                //    if (_ExcelSerial.Count > 0)
                //    {
                //        var _filter = _ExcelSerial.Where(x => x.serial1 == txtSerial1.Text).ToList();
                //        if (_filter.Count == 0)
                //        {
                //            dis
                //            return;
                //        }
                //    }
                //}
                DataTable _AllowPreFix = CheckPreFix(txtItemCode.Text);
                if (_AllowPreFix.Rows.Count > 0)
                {
                    string AllowpreFix = (_AllowPreFix.Rows[0][1].ToString());
                    if ((AllowpreFix == "") || (AllowpreFix == "0"))
                    {
                        txtNoOfPages.Text = _AllowPreFix.Rows[0][2].ToString();
                        ddlPreFix.Visible = false;
                        txtFix.Visible = true;
                        txtFix.Text = _AllowPreFix.Rows[0][0].ToString();
                        Session["Defalt"] = "true";
                    }
                    else
                    {
                        txtNoOfPages.Text = _AllowPreFix.Rows[0][2].ToString();
                        txtFix.Text = _AllowPreFix.Rows[0][0].ToString();
                        DataTable _PreFix = CHNLSVC.Inventory.GetitemPreFix(txtItemCode.Text, Session["UserCompanyCode"].ToString());
                        ddlPreFix.DataSource = _PreFix;
                        ddlPreFix.DataTextField = "mi_prefix";
                        ddlPreFix.DataValueField = "mi_prefix";
                        ddlPreFix.DataBind();
                        ddlPreFix.Visible = true;
                        txtFix.Visible = false;
                        Session["Defalt"] = "false";
                    }
                    txtStartPage.Focus();
                    userPrefixSerial.Show();
                    return;
                }
                else if (Session["Subserial"].ToString() == "true")
                {
                    tstSubSerial.Focus();

                    lbtnItemAdd_Click(null, null);
                    // ClientScript.RegisterStartupScript(this.GetType(), "key", "userSubSerial();", true);
                    DataTable _result = CHNLSVC.Inventory.GetSubSerials(txtItemCode.Text, Convert.ToInt32(Session["userSeqNo"]), Session["MainItemSerial"].ToString());
                    SubItemList(_result);
                    getRowValue(1);
                    int count = 1;

                    foreach (DataRow row in _result.Rows)
                    {
                        // your index is in i
                        count = _result.Rows.IndexOf(row);
                    }
                    Session["subItemRowCount"] = count.ToString();
                    Session["subItemCurrentRowCount"] = "1";

                }
                else
                {
                    lbtnItemAdd_Click(null, null);
                }

            }
            if (Session["_itemSerializedStatus"].ToString() == "1")
            {
                txtSerial1.Focus();
            }
            else
            {
                txtSerial2.Focus();
            }


        }

        protected void btnSII_Click(object sender, EventArgs e)
        {
            if (Session["_itemSerializedStatus"].ToString() == "2")
            {
                if (!string.IsNullOrEmpty(txtSerial1.Text) && Session["ValidateFilePath"] != null)
                {
                    DataTable[] GetExecelTbl = LoadValidateData(Session["ValidateFilePath"].ToString());
                    if (GetExecelTbl == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Excel Data Invalid Please check Excel File and Upload');", true);
                        return;
                    }
                    if (GetExecelTbl[0].Rows.Count < 1 && GetExecelTbl[0].Columns.Count < 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Excel Data Invalid Please check Excel File and Upload');", true);
                        return;
                    }
                    bool valid = false;
                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        string _serial2 = GetExecelTbl[0].Rows[i][1].ToString();
                        if (txtSerial2.Text == _serial2)
                        {
                            valid = true;
                        }
                    }
                    if (!valid)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Scanned serial not exist in excel file ');", true);
                        return;
                    }
                }

                if (Session["Subserial"].ToString() == "true")
                {

                    ///ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "Focus();", true);
                    lbtnItemAdd_Click(null, null);
                    // ClientScript.RegisterStartupScript(this.GetType(), "key", "Focus();", true);
                    DataTable _result = CHNLSVC.Inventory.GetSubSerials(txtItemCode.Text, Convert.ToInt32(Session["userSeqNo"]), Session["MainItemSerial"].ToString());
                    // tstSubSerial.Focus();
                    SubItemList(_result);
                    getRowValue(1);
                    int count = 1;

                    foreach (DataRow row in _result.Rows)
                    {
                        // your index is in i
                        count = _result.Rows.IndexOf(row);
                    }
                    Session["subItemRowCount"] = count.ToString();
                    Session["subItemCurrentRowCount"] = "1";
                    //Session["MainItemSerial"] = txtSerial2.Text;
                    // tstSubSerial.Focus();
                }
                else
                {
                    lbtnItemAdd_Click(null, null);
                }
            }
            //txtSerial3.Focus();
        }

        protected void btnserail3_Click(object sender, EventArgs e)
        {
            if (Session["_itemSerializedStatus"].ToString() == "3")
            {
                if (Session["Subserial"].ToString() == "true")
                {

                    ///ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "Focus();", true);
                    lbtnItemAdd_Click(null, null);
                    // ClientScript.RegisterStartupScript(this.GetType(), "key", "Focus();", true);
                    DataTable _result = CHNLSVC.Inventory.GetSubSerials(txtItemCode.Text, Convert.ToInt32(Session["userSeqNo"]), Session["MainItemSerial"].ToString());
                    SubItemList(_result);
                    getRowValue(1);
                    int count = 1;

                    foreach (DataRow row in _result.Rows)
                    {
                        // your index is in i
                        count = _result.Rows.IndexOf(row);
                    }
                    Session["subItemRowCount"] = count.ToString();
                    Session["subItemCurrentRowCount"] = "1";
                    //Session["MainItemSerial"] = txtSerial2.Text;
                    tstSubSerial.Focus();
                }
                else
                {
                    lbtnItemAdd_Click(null, null);
                }
            }
            txtSerial3.Focus();
        }
        private void SubItemList(DataTable _tbl)
        {
            tstSubSerial.Focus();
            if (_tbl != null)
            {
                DataRow dr = null;
                DataTable _SubItemTbl = new DataTable();
                _SubItemTbl.Columns.Add(new DataColumn("ID", typeof(int)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_m_ser", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_itm_cd", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_tp", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("mis_desc", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_itm_stus", typeof(string)));
                _SubItemTbl.Columns.Add(new DataColumn("tpss_sub_ser", typeof(string)));
                int row = 0;
                foreach (DataRow tblr in _tbl.Rows)
                {
                    dr = _SubItemTbl.NewRow();
                    dr["ID"] = row;
                    dr["tpss_m_ser"] = tblr["tpss_m_ser"].ToString();// txtSerial1.Text;
                    dr["tpss_itm_cd"] = tblr["tpss_itm_cd"].ToString();
                    dr["tpss_tp"] = tblr["tpss_tp"].ToString();
                    dr["mis_desc"] = tblr["mis_desc"].ToString();
                    dr["tpss_itm_stus"] = tblr["tpss_itm_stus"].ToString();
                    dr["tpss_sub_ser"] = tblr["tpss_sub_ser"].ToString();
                    //if (row == 0)
                    //{
                    //    dr["tpss_sub_ser"] = txtSerial1.Text;
                    //}
                    //else
                    //{
                    //    string Serial =tblr["tpss_sub_ser"].ToString();
                    //    if(Serial == null){
                    //         dr["tpss_sub_ser"] = "N/A";
                    //    }
                    //    else { dr["tpss_sub_ser"] = Serial; }

                    //}

                    _SubItemTbl.Rows.Add(dr);
                    row++;
                }

                GgdsubItem.DataSource = _SubItemTbl;
                GgdsubItem.DataBind();
                tstSubSerial.Focus();
                userSubSerial.Show();
                // tstSubSerial.Focus();
                ViewState["SubItem"] = _SubItemTbl;

            }
        }

        protected void tstSubSerial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tstSubSerial.Text))
                {
                    userSubSerial.Show();
                    return;
                }
                List<ReptPickSerialsSub> _listSubSer = CHNLSVC.Inventory.GET_TEMP_PICK_SER_SUB(new ReptPickSerialsSub()
                {
                    Tpss_sub_ser = tstSubSerial.Text
                });
                if (_listSubSer != null)
                {
                    if (_listSubSer.Count > 0)
                    {
                        tstSubSerial.Text = "";
                        tstSubSerial.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Sub serial already exist !!! ');", true);
                        userSubSerial.Show();
                        return;
                    }
                }

                List<InventorySubSerialMaster> _subSerList = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster()
                {
                    Irsms_sub_ser = tstSubSerial.Text
                });

                if (_subSerList != null)
                {
                    if (_subSerList.Count > 0)
                    {
                        tstSubSerial.Text = "";
                        tstSubSerial.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Sub serial already exist !!! ');", true);
                        userSubSerial.Show();
                        return;
                    }
                }

                DataTable _tbl = ViewState["SubItem"] as DataTable;
                int RowCount = Convert.ToInt32(Session["subItemRowCount"].ToString());
                cRow = Convert.ToInt32(Session["subItemCurrentRowCount"].ToString());

                tstSubSerial.Focus();
                foreach (DataRow row in _tbl.Rows)
                {
                    if (row["tpss_sub_ser"].ToString() == tstSubSerial.Text.Trim())
                    {
                        tstSubSerial.Text = "";
                        tstSubSerial.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Sub serial already exist !!! ');", true);
                        userSubSerial.Show();
                        return;
                    }
                    if (row["tpss_itm_cd"].ToString() == txtSubproduct.Text)
                    {
                        row.SetField("tpss_itm_stus", ddlSIStatus.SelectedItem.Text);
                        row.SetField("tpss_sub_ser", tstSubSerial.Text);
                        row.AcceptChanges();
                        UpdateSubSerial();
                        GgdsubItem.DataSource = _tbl;
                        GgdsubItem.DataBind();
                        userSubSerial.Show();
                        ViewState["SubItem"] = _tbl;
                        cRow++;
                        getRowValue(cRow);
                        Session["subItemCurrentRowCount"] = cRow.ToString();
                        cRow = cRow - 1;
                        if (RowCount == cRow)
                        {

                            userSubSerial.Hide();


                            InvReportPara _invRepPara = new InvReportPara();
                            //Session["GlbReportType"] = "";                        
                            //Session["GlbReportName"] = "RepWarrantyCard_AOA.rpt";
                            //_invRepPara._GlbReportName = "RepWarrantyCard_AOA.rpt";                        
                            //_invRepPara._GlbReportDoc = Session["GlbReportDoc"].ToString();
                            //_invRepPara._GlbReportItemCode = txtItemCode.Text;//main itm LGMU-KP105                        
                            //_invRepPara._GlbReportMainSerial = txtSerial1.Text;//PO2                       

                            //_invRepPara._GlbReportHeading = "Warranty Print";
                            //Session["InvReportPara"] = _invRepPara;
                            //clsInventory obj = new clsInventory();
                            //obj.Print_AOA_Warra(_invRepPara);
                            ////CVInventory.ReportSource = obj._warraPrint;
                            ////obj._warraPrint.PrintOptions.PrinterName = GetDefaultPrinter();
                            ////obj._warraPrint.PrintToPrinter(1, false, 0, 0);
                            //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);GG
                            if (chkwaranty.Checked)
                            {

                                Session["GlbReportDoc"] = _userSeqNo;
                                Session["GlbReportType"] = "";
                                //Session["GlbReportName"] = "RepWarrantyCard_AOA1.rpt";
                                //_invRepPara._GlbReportName = "RepWarrantyCard_AOA1.rpt";
                                Session["GlbReportName"] = "WarrPrint_abl.rpt";
                                _invRepPara._GlbReportName = "WarrPrint_abl.rpt";
                                _invRepPara._GlbReportDoc = Session["GlbReportDoc"].ToString();// Session["userSeqNo"].ToString();
                                _invRepPara._GlbReportItemCode = txtItemCode.Text;//main itm LGMU-KP105                        
                                _invRepPara._GlbReportMainSerial = txtSerial1.Text;//PO2
                                _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                                _invRepPara._GlbReportHeading = "Warranty Print";
                                Session["InvReportPara"] = _invRepPara;

                                //clsInventory obj = new clsInventory();
                                //obj.Print_AOA_Warra(_invRepPara);
                                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                                //PrintPDFWarr(targetFileName);
                                PrintPDFWarrNew(targetFileName);

                                //obj = null;
                                //obj.Dispose(); 
                                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                            }
                            //ClientPrintJob cpj = new ClientPrintJob();
                            //PrintFile file = new PrintFile("/Temp/", Session["UserID"].ToString() + ".pdf");
                            ////set file to print...
                            //cpj.PrintFile = file;
                            ////set client printer...
                            //if (obj.useDefaultPrinter || obj.printerName == "null")
                            //    cpj.ClientPrinter = new DefaultPrinter();
                            //else
                            //    cpj.ClientPrinter = new InstalledPrinter(obj.printerName);
                            ////send it...
                            //cpj.SendToClient(Response);

                            //-------------------------------------------------------------------------------
                            txtSerial1.Text = "";
                            txtSerial2.Text = "";

                            lbtnTempSave.Focus();
                            txtSerial1.Focus();
                            //if (Session["_itemSerializedStatus"].ToString() == "2")
                            //{
                            //    txtSerial2.Focus();
                            //}

                        }
                        tstSubSerial.Text = "";
                        //txtSerial1.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("GRN Warr Print", "Goods Received Note - GRN", ex.Message, Session["UserID"].ToString());
                throw ex;
            }
        }

        public void PrintPDFWarrNew(string targetFileName)
        {
            try
            {
                clsInventory obj = new clsInventory();
                InvReportPara _objRepPara = new InvReportPara();
                _objRepPara = Session["InvReportPara"] as InvReportPara;
                DataTable _warrPrint = CHNLSVC.Inventory.getWarrantyPrintMobDetails(_objRepPara._GlbReportComp, _objRepPara._GlbReportDoc, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportMainSerial, 1);
                obj._warrprint.Database.Tables["WARR_PRINT"].SetDataSource(_warrPrint);

                ReportDocument rptDoc = (ReportDocument)obj._warraPrint;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                obj.Dispose();
                rptDoc.Close();
                rptDoc.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("GRN Warr Print - PDF", "Goods Received Note - GRN", ex.Message, Session["UserID"].ToString());
                throw ex;
            }
        }

        public void PrintPDFWarr(string targetFileName)
        {
            try
            {
                clsInventory obj = new clsInventory();
                InvReportPara _objRepoPara = new InvReportPara();
                _objRepoPara = Session["InvReportPara"] as InvReportPara;
                obj.Print_AOA_Warra(_objRepoPara);
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)obj._warraPrint;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                // obj = null;
                obj.Dispose();
                rptDoc.Close();
                rptDoc.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("GRN Warr Print - PDF", "Goods Received Note - GRN", ex.Message, Session["UserID"].ToString());
                throw ex;
            }
        }
        string GetDefaultPrinter()
        {
            string _printerName = string.Empty;
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                {
                    _printerName = printer;
                    break;
                }
            }
            return _printerName;
        }

        private void getRowValue(int _row)
        {
            DataTable _tbl = ViewState["SubItem"] as DataTable;
            var chosenRow = (from row in _tbl.AsEnumerable()
                             where row.Field<int>("ID") == _row
                             select row);

            if (chosenRow != null)
            {
                foreach (DataRow _one in chosenRow)
                {
                    txtSubproduct.Text = _one["tpss_itm_cd"].ToString();
                    ddlSIStatus.SelectedItem.Text = _one["mis_desc"].ToString();
                }
            }


        }

        private void UpdateSubSerial()
        {
            //DataTable _tbl = ViewState["SubItem"] as DataTable;
            //foreach (DataRow dr in _tbl.Rows)
            //{
            ReptPickSerialsSub _ReptPickSerialsSub = new ReptPickSerialsSub();
            #region Fill Pick Sub Serial Object
            _ReptPickSerialsSub.Tpss_itm_stus = ddlSIStatus.SelectedValue;
            _ReptPickSerialsSub.Tpss_m_itm_cd = txtItemCode.Text;
            _ReptPickSerialsSub.Tpss_m_ser = Session["MainItemSerial"].ToString();
            _ReptPickSerialsSub.Tpss_itm_cd = txtSubproduct.Text;
            _ReptPickSerialsSub.Tpss_sub_ser = tstSubSerial.Text;
            // _ReptPickSerialsSub.Tpss_usrseq_no = Convert.ToInt32(Session["userSeqNo"].ToString());
            _reptPickSerialsSub.Add(_ReptPickSerialsSub);
            Int32 Result = CHNLSVC.Inventory.UpdateAllScanSubSerials(_reptPickSerialsSub);
            if (Result == 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error');", true);
            }
            #endregion
            //  }



        }
        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (chkNewItem.Checked)
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + txtItemCode.Text.ToString());
                if (_result.Rows.Count > 0)
                {
                    string Code = _result.Rows[0][0].ToString();
                    DataTable _test = ViewState["po_items"] as DataTable;
                    CheckItemAllreadyInPo(Code, _test);
                    string Des = _result.Rows[0][1].ToString();
                    string Model = _result.Rows[0][2].ToString();
                    string Brand = _result.Rows[0][3].ToString();
                    string Part = _result.Rows[0][4].ToString();
                    txtModel.Text = Model;
                    txtBrand.Text = Brand;
                    txtpartNo.Text = Part;
                    txtItemDes.Text = Des;
                    _itemSerializedStatus = Convert.ToInt32(Session["_itemSerializedStatus"].ToString());
                    if (_itemSerializedStatus == 0)
                    {
                        txtqty.Focus();
                    }
                    return;
                }
                txtItemCode.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please type correct item code');", true);

            }
            else
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + txtItemCode.Text.ToString());
                if (_result.Rows.Count > 0)
                {
                    string Code = _result.Rows[0][0].ToString();
                    DataTable _test = ViewState["po_items"] as DataTable;
                    CheckItemAllreadyInPo(Code, _test);
                    string Des = _result.Rows[0][1].ToString();
                    string Model = _result.Rows[0][2].ToString();
                    string Brand = _result.Rows[0][3].ToString();
                    string Part = _result.Rows[0][4].ToString();
                    txtModel.Text = Model;
                    txtBrand.Text = Brand;
                    txtpartNo.Text = Part;
                    txtItemDes.Text = Des;
                    _itemSerializedStatus = Convert.ToInt32(Session["_itemSerializedStatus"].ToString());
                    if (_itemSerializedStatus == 0)
                    {
                        txtqty.Focus();
                    }
                    return;
                }
                txtItemCode.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please type correct item code');", true);
            }
        }

        protected void lbtnGet_Click(object sender, EventArgs e)
        {
            Session["DivVisible"] = "true";
            _gridEdit = true;
            if (grdDOItems.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {

                string _item = (row.FindControl("PODI_ITM_CD") as Label).Text; ;
                string _Qty = (row.FindControl("PODI_BAL_QTY") as Label).Text;
                // string _Qty = (row.FindControl("PODI_QTY") as Label).Text;
                string _pickQty = (row.FindControl("PickQty") as Label).Text;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                //DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + _item);
                msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (msitem != null)
                {
                    if (msitem.Mi_is_ser1 == 1)
                    {
                        Session["DivVisible"] = "true";
                    }

                    Session["baseitem"] = "";
                    // string Code = _result.Rows[0][0].ToString();
                    DataTable _test = ViewState["po_items"] as DataTable;
                    _test = LoadDefRowNo(_test);
                    CheckItemAllreadyInPo(_item, _test);
                    string Des = msitem.Mi_longdesc;//_result.Rows[0][1].ToString();
                    string Model = msitem.Mi_model;//_result.Rows[0][2].ToString();
                    string Brand = msitem.Mi_brand;//_result.Rows[0][3].ToString();
                    string Part = msitem.Mi_part_no;//_result.Rows[0][4].ToString();
                    txtItemCode.Text = _item;
                    txtModel.Text = Model;
                    txtBrand.Text = Brand;
                    txtpartNo.Text = Part;
                    txtItemDes.Text = Des;
                    // msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text);
                    if (msitem != null)
                    {
                        if ((msitem.Mi_is_ser1 == 1) && (msitem.Mi_is_pgs))
                        {

                            try
                            {
                                Int64 _serialNo = 0;
                                Int32 _lasetpage = 0;
                                _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
                                if (_userSeqNo == -1)
                                {
                                    _serialvalue = CHNLSVC.Inventory.GET_BOOKMAX_SERIAL(Session["UserCompanyCode"].ToString(), txtItemCode.Text, out  _lasetpage);
                                    if (_serialvalue != "")
                                    {
                                        string[] tokens = _serialvalue.Split('|');
                                        //      _serialvalue = _serialvalue.Remove(0, 1);
                                        _serialvalue = tokens[1].ToString();
                                        _serialNo = Convert.ToInt64(_serialvalue) + 1;
                                        txtStartPage.Text = (_lasetpage + 1).ToString();

                                    }
                                    else
                                    {
                                        _serialNo = 0;
                                    }
                                }
                                else
                                {
                                    _serialvalue = CHNLSVC.Inventory.GET_TEMPBOOKMAX_SERIAL(Session["UserCompanyCode"].ToString(), txtItemCode.Text, _userSeqNo, out  _lasetpage);
                                    if (_serialvalue != "")
                                    {
                                        string[] tokens = _serialvalue.Split('|');
                                        //      _serialvalue = _serialvalue.Remove(0, 1);
                                        _serialvalue = tokens[1].ToString();
                                        _serialNo = Convert.ToInt64(_serialvalue) + 1;
                                        txtStartPage.Text = (_lasetpage + 1).ToString();

                                    }
                                    else
                                    {
                                        _serialvalue = CHNLSVC.Inventory.GET_BOOKMAX_SERIAL(Session["UserCompanyCode"].ToString(), txtItemCode.Text, out  _lasetpage);
                                        if (_serialvalue != "")
                                        {
                                            string[] tokens = _serialvalue.Split('|');
                                            //      _serialvalue = _serialvalue.Remove(0, 1);
                                            _serialvalue = tokens[1].ToString();
                                            _serialNo = Convert.ToInt64(_serialvalue) + 1;
                                            txtStartPage.Text = (_lasetpage + 1).ToString();

                                        }
                                        else
                                        {
                                            _serialNo = 0;
                                        }
                                    }
                                }

                                txtSerial1.Text = _serialNo.ToString();
                                decimal remaiqty = Convert.ToDecimal(_Qty) - Convert.ToDecimal(_pickQty);
                                txtpickqty.Text = remaiqty.ToString();
                            }
                            catch (Exception ex)
                            {
                                string msg = "Please check book serial number";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                                return;
                            }


                        }
                    }
                    if (Session["DivVisible"].ToString() == "true")
                    {
                        divAddItem.Visible = true;
                        Session["DivVisible"] = "true";
                    }
                    else
                    {
                        divAddItem.Visible = false;
                        Session["DivVisible"] = "false";

                    }
                    // loadInterCompanyPOSerials(pending_list.Rows[0]["POH_DOC_NO"].ToString(), pending_list.Rows[0]["POH_SUPP"].ToString(), pending_list.Rows[0]["POH_REF"].ToString());



                    return;
                }
                else
                {
                    string msg = "Please type correct item code";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                }
            }
        }

        protected void lbtnupdate_Click(object sender, EventArgs e)
        {
            if (GgdsubItem.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string ItemCode = (row.FindControl("tpss_itm_cd") as Label).Text;
                string Status = (row.FindControl("mis_desc") as Label).Text;
                int ID = Convert.ToInt32((row.FindControl("ID") as Label).Text);
                Session["subItemCurrentRowCount"] = ID - 1;
                txtSubproduct.Text = ItemCode;
                ddlSIStatus.SelectedItem.Text = Status;
                userSubSerial.Show();
                tstSubSerial.Focus();
            }
        }

        private DataTable CheckPreFix(string _Icode)
        {
            DataTable _result = CHNLSVC.Inventory.CheckitemPreFix(_Icode);
            return _result;
        }

        protected void txtStartPage_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(txtStartPage.Text, out value))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Enter numeric value');", true);
                txtStartPage.Text = "";
                txtlastPages.Text = "";
                txtStartPage.Focus();
                userPrefixSerial.Show();
                return;
            }
            int Pages = (Convert.ToInt32(txtStartPage.Text) + Convert.ToInt32(txtNoOfPages.Text)) - 1;
            txtlastPages.Text = Pages.ToString();
            userPrefixSerial.Show();
        }

        protected void lbtnSavePreFix_Click(object sender, EventArgs e)
        {
            Session["ItemPreFix"] = "true";
            int _pickqty = Convert.ToInt32(txtpickqty.Text);
            int startpage = Convert.ToInt32(txtStartPage.Text);
            int lastpage = Convert.ToInt32(txtlastPages.Text);
            for (int i = 0; _pickqty - 1 >= i; i++)
            {
                Int64 _serialNo = 0;
                if (_serialvalue != "")
                {

                    _serialNo = Convert.ToInt64(_serialvalue) + i;
                }
                else
                {
                    _serialNo = 0 + i;
                }

                if (i > 1)
                {

                    txtStartPage.Text = (lastpage + 1).ToString();
                    int Pages = (Convert.ToInt32(txtStartPage.Text) + Convert.ToInt32(txtNoOfPages.Text)) - 1;
                    txtlastPages.Text = Pages.ToString();
                }

                txtSerial1.Text = _serialNo.ToString();
                //txtSerial1.Text 
                lbtnItemAdd_Click(null, null);
            }


            txtFix.Text = "";
            //ddlPreFix.SelectedValue = "0";
            txtNoOfPages.Text = "";
            txtStartPage.Text = "";
            txtlastPages.Text = "";
            //if (Session["Defalt"].ToString() == "true")
            //{
            //    txtSerial1.Text = txtFix.Text + txtSerial1.Text.Trim();
            //}
            //else if (Session["Defalt"].ToString() == "false") { lbtnSavePreFix.Text = ddlPreFix.SelectedItem.Text + txtSerial1.Text.Trim(); }
        }

        protected void lbtnDocNo_Click(object sender, EventArgs e)
        {
            //if (ddlMainType.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select Type.');", true);
            //    return;
            //}
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable _result;
            var sortedTable = new DataTable();
            if (chk_Temp.Checked == true)
            {
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtDODate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDODate.Text));
                lblvalue.Text = "TempDoc";
                Session["TempDoc"] = "true";

            }
            else
            {
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtDODate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDODate.Text));
                lblvalue.Text = "Doc";
                Session["Doc"] = "true";

            }
            grdResultDate.DataSource = _result;
            grdResultDate.DataBind();
            BindUCtrlDDLData3(_result);

            txtFDate.Text = Convert.ToDateTime(txtDODate.Text).Date.AddMonths(-1).ToShortDateString();
            txtTDate.Text = Convert.ToDateTime(txtDODate.Text).Date.ToShortDateString();

            UserDPopoup.Show();
        }

        private void GetTempDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {
                    Int32 affected_rows;
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    _direction = 1;
                    Int32 _userSeqNo;
                    #region Clear Data
                    // grdItems.ReadOnly = false;
                    //gvSerial.ReadOnly = false;

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdDOSerials.AutoGenerateColumns = false;
                    grdDOSerials.DataSource = _emptySer;
                    grdDOSerials.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdDOItems.AutoGenerateColumns = false;
                    grdDOItems.DataSource = new int[] { };
                    grdDOItems.DataBind();

                    // btnSave.Enabled = true;
                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "SaveConfirm();";
                    lbtnSave.CssClass = "buttonUndocolor";

                    lbtnTempSave.Enabled = true;
                    lbtnTempSave.OnClientClick = "TempSaveConfirm();";
                    lbtnTempSave.CssClass = "buttonUndocolor";

                    //txtManualRef.Text = string.Empty;
                    //txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;

                    #endregion
                    InventoryHeader _invHdr = new InventoryHeader();
                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr_Temp(DocNo);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "GRN")
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == true && _direction == 0)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == false && _direction == 1)
                    {
                        _invalidDoc = false;
                        goto err;
                    }

                err:
                    if (_invalidDoc == false)
                    {
                        // MessageBox.Show("Invalid Document No!", "PRN No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocumentNo.Text = "";
                        txtDocumentNo.Focus();
                        return;
                    }
                    else
                    {
                        //cmdPrint.Enabled = true;
                        //grdItems.ReadOnly = true;
                        //gvSerial.ReadOnly = true;
                        foreach (GridViewRow gvr in grdDOItems.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnGet") as LinkButton;


                            Addrow.Enabled = true;

                        }
                        foreach (GridViewRow gvr in grdDOSerials.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnRemove") as LinkButton;
                            Addrow.Enabled = true;
                            Addrow.OnClientClick = "return DeleteConfirm();";
                        }

                        //btnSave.Enabled = false;
                        lbtnSave.Enabled = true;
                        lbtnSave.OnClientClick = "SaveConfirm();";
                        lbtnSave.CssClass = "buttonUndocolor";

                        lbtnTempSave.Enabled = false;
                        lbtnTempSave.OnClientClick = "return Enable();";
                        lbtnTempSave.CssClass = "buttoncolor";

                        //ddlAdjSubType.SelectedItem.Text = _invHdr.Ith_sub_tp;
                        //txtManualRef.Text = _invHdr.Ith_manual_ref;
                        //txtOtherRef.Text = _invHdr.Ith_bus_entity;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        //txtUserSeqNo.Clear();
                        //ddlSeqNo.Text = string.Empty;
                    }
                    #endregion
                    //string Seq = _invHdr.Ith_entry_no;
                    _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), DocNo, 1);
                    if (_userSeqNo == -1)
                    {
                        _userSeqNo = GenerateNewUserSeqNo("GRN", _invHdr.Ith_oth_docno);
                        #region Get Serials
                        List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                        List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                        _serList = CHNLSVC.Inventory.Get_Int_Ser_Temp(DocNo);
                        List<InventoryBatchN> _serListT = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                        if ((_serList != null) && (_serList.Count != 0))
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            foreach (var itm in _scanItems)
                            {
                                _lineNo += 1;
                                InventoryRequestItem _itm = new InventoryRequestItem();
                                _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                                _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                                _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                                _itm.Itri_line_no = _lineNo;
                                _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                                _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                                _itm.Mi_model = itm.Peo.Tus_itm_model;
                                _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                                _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                                _itmList.Add(_itm);
                                ScanItemList = _itmList;
                            }


                            List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                            foreach (InventoryRequestItem _addedItem in ScanItemList)
                            {
                                ReptPickItems _reptitm = new ReptPickItems();
                                _reptitm.Tui_usrseq_no = _userSeqNo;
                                _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                                _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                                _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                                _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                                _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                                _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                                _saveonly.Add(_reptitm);
                            }

                            // CHNLSVC.Inventory.SavePickedItems(_saveonly);
                            List<ReptPickSerials> _TempserList = new List<ReptPickSerials>();
                            foreach (ReptPickSerials serial in _serList)
                            {
                                var _Batch = _serListT.Where(x => x.Inb_itm_cd == serial.Tus_itm_cd && x.Inb_itm_stus == serial.Tus_itm_stus).ToList();
                                foreach (InventoryBatchN _BItem in _Batch)
                                {
                                    serial.Tus_qty = _BItem.Inb_qty;
                                }
                                serial.Tus_usrseq_no = Convert.ToInt32(_userSeqNo);

                                //serial.Tus_qty = Session["Inb_qty"];
                                _TempserList.Add(serial);
                                //affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(serial, null);

                            }

                            CHNLSVC.Inventory.SaveAllScanSerials_Excel(_TempserList, _saveonly, null);
                        }
                        else
                        {
                            // List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);

                            List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                            foreach (InventoryBatchN _serialItem in _serListT)
                            {
                                ReptPickItems _reptitm = new ReptPickItems();
                                _reptitm.Tui_usrseq_no = Convert.ToInt32(_userSeqNo.ToString());
                                _reptitm.Tui_req_itm_qty = _serialItem.Inb_qty;
                                _reptitm.Tui_req_itm_cd = _serialItem.Inb_itm_cd;
                                _reptitm.Tui_req_itm_stus = _serialItem.Inb_itm_stus;
                                _reptitm.Tui_pic_itm_cd = _serialItem.Inb_itm_line.ToString();
                                _reptitm.Tui_pic_itm_stus = _serialItem.Inb_itm_stus;
                                _reptitm.Tui_pic_itm_qty = _serialItem.Inb_qty;
                                _saveonly.Add(_reptitm);
                            }
                            // CHNLSVC.Inventory.SavePickedItems(_saveonly);
                            List<ReptPickSerials> _TempserList = new List<ReptPickSerials>();
                            foreach (InventoryBatchN _serialItem in _serListT)
                            {
                                ReptPickSerials _serial = new ReptPickSerials();
                                MasterItem _itms = new MasterItem();
                                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _serialItem.Inb_itm_cd);
                                _serial.Tus_qty = _serialItem.Inb_qty;
                                _serial.Tus_com = _serialItem.Inb_com;
                                _serial.Tus_doc_dt = _serialItem.Inb_doc_dt;
                                _serial.Tus_itm_cd = _serialItem.Inb_itm_cd;
                                _serial.Tus_itm_stus = _serialItem.Inb_itm_stus;
                                _serial.Tus_loc = _serialItem.Inb_loc;
                                _serial.Tus_unit_price = _serialItem.Inb_unit_price;
                                _serial.Tus_bin = _serialItem.Inb_bin;
                                _serial.Tus_itm_desc = _itms.Mi_shortdesc;
                                _serial.Tus_itm_model = _itms.Mi_model;
                                _serial.Tus_itm_brand = _itms.Mi_brand;
                                _serial.Tus_doc_no = _serialItem.Inb_doc_no;
                                _serial.Tus_ser_1 = "N/A";
                                _serial.Tus_base_doc_no = _invHdr.Ith_oth_docno;
                                _serial.Tus_batch_no = txtBatchNo.Text.Trim();
                                MasterItem _itmExp = CHNLSVC.General.GetItemMasterNew(_serial.Tus_itm_cd);
                                if (_itmExp != null)
                                {
                                    if (_itmExp.Tmp_mi_is_exp_dt == 1)
                                    {
                                        DateTime _dtTemp = new DateTime();
                                        _serial.Tus_exp_dt = DateTime.TryParse(txtEDate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtEDate.Text.Trim()) : DateTime.MinValue;
                                        _serial.Tus_manufac_dt = DateTime.TryParse(txtMdate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtMdate.Text.Trim()) : DateTime.MinValue;
                                    }
                                }
                                _serial.Tus_usrseq_no = Convert.ToInt32(_userSeqNo.ToString());

                                //serial.Tus_qty = Session["Inb_qty"];
                                _TempserList.Add(_serial);
                                // affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_serial, null);
                            }
                            CHNLSVC.Inventory.SaveAllScanSerials_Excel(_TempserList, _saveonly, null);
                            LoadItems(_userSeqNo.ToString());
                            txtSuppCode.Text = _invHdr.Ith_bus_entity;
                            txtPONo.Text = _invHdr.Ith_oth_docno;
                            // List<InventoryBatchN> _serListT1 = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                            GetItemSave(_serListT, _invHdr.Ith_oth_docno);
                            return;
                        }




                        #endregion
                        LoadItems(_userSeqNo.ToString());
                        txtSuppCode.Text = _invHdr.Ith_bus_entity;
                        txtPONo.Text = _invHdr.Ith_oth_docno;

                        GetItemSave(_serListT, _invHdr.Ith_oth_docno);
                    }
                    else
                    {
                        LoadItems(_userSeqNo.ToString());
                        txtSuppCode.Text = _invHdr.Ith_bus_entity;
                        txtPONo.Text = _invHdr.Ith_oth_docno;
                        List<InventoryBatchN> _serListT = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                        GetItemSave(_serListT, _invHdr.Ith_oth_docno);
                    }



                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;
                //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                //{
                _direction = 1;
                //}

                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "GRN", Session["UserID"].ToString(), _direction, _seqNo);
                if (_seqNo == "")
                {
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }
                }
                else
                {
                    user_seq_num = Convert.ToInt32(_seqNo);
                }




                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    //_itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList();
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "GRN");

                if (_serList != null)
                {
                    grdDOSerials.AutoGenerateColumns = false;
                    grdDOSerials.DataSource = _serList;
                    grdDOSerials.DataBind();
                    ViewState["reptPickSerialsList"] = _serList;

                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    grdDOSerials.AutoGenerateColumns = false;
                    grdDOSerials.DataSource = emptyGridList;
                    grdDOSerials.DataBind();
                    ViewState["reptPickSerialsList"] = emptyGridList;

                }
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
            //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
            //{
            _direction = 0;
            //}
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "PRN", _direction, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "GRN";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change 
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            if (_direction == 1)//direction always (-) for change status
            {
                RPH.Tuh_direct = true;
            }
            else
            {
                RPH.Tuh_direct = false;
            }
            RPH.Tuh_doc_no = generated_seq.ToString();
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                //txtUserSeqNo.Text = generated_seq.ToString();
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        protected void AddItem(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial)
        {
            try
            {
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                if (ScanItemList != null)
                {
                    var _maxline = (from _ls in ScanItemList
                                    select _ls.Itri_line_no).Max();
                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    //Added by Prabhath on 17/12/2013 ************* start **************
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    //Added by Prabhath on 17/12/2013 ************* end **************
                    ScanItemList.Add(_itm);

                }
                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(_UserSeqNo);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _saveonly.Add(_reptitm);
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);

                //grdDOItems.DataSource = null;
                //grdDOItems.DataSource = ScanItemList;
                //grdDOItems.DataBind();
                //ViewState["ScanItemList"] = ScanItemList;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);

                CHNLSVC.CloseChannel();
                return;
            }


        }

        private void GetDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    _direction = 1;

                    #region Clear Data
                    // grdItems.ReadOnly = false;
                    //gvSerial.ReadOnly = false;

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdDOSerials.AutoGenerateColumns = false;
                    grdDOSerials.DataSource = _emptySer;
                    grdDOSerials.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdDOItems.AutoGenerateColumns = false;
                    grdDOItems.DataSource = new int[] { };
                    grdDOItems.DataBind();

                    // btnSave.Enabled = true;
                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "SaveConfirm();";
                    lbtnSave.CssClass = "buttonUndocolor";

                    lbtnTempSave.Enabled = true;
                    lbtnTempSave.OnClientClick = "SaveConfirm();";
                    lbtnTempSave.CssClass = "buttonUndocolor";


                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(DocNo);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "GRN")
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == true && _direction == 0)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_direct == false && _direction == 1)
                    {
                        _invalidDoc = false;
                        goto err;
                    }

                err:
                    if (_invalidDoc == false)
                    {
                        // MessageBox.Show("Invalid Document No!", "PRN No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocumentNo.Text = "";
                        txtDocumentNo.Focus();
                        return;
                    }
                    else
                    {
                        //cmdPrint.Enabled = true;
                        //grdItems.ReadOnly = true;
                        //gvSerial.ReadOnly = true;
                        lbtnSave.Enabled = false;
                        lbtnSave.OnClientClick = "return Enable();";
                        lbtnSave.CssClass = "buttoncolor";

                        lbtnTempSave.Enabled = false;
                        lbtnTempSave.OnClientClick = "return Enable();";
                        lbtnTempSave.CssClass = "buttoncolor";

                    }
                    #endregion

                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost, x.Tus_base_doc_no, x.Tus_base_itm_line, x.Tus_seq_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            _lineNo += 1;
                            InventoryRequestItem _itm = new InventoryRequestItem();
                            _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                            _itm.Itri_line_no = _lineNo;
                            _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                            _itm.Mi_model = itm.Peo.Tus_itm_model;
                            _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                            _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                            _itm.Itri_res_no = itm.Peo.Tus_base_doc_no;
                            _itm.Itri_job_line = itm.Peo.Tus_base_itm_line;
                            _itm.Itri_seq_no = itm.Peo.Tus_seq_no;
                            _itmList.Add(_itm);

                        }
                        ScanItemList = _itmList;
                        DataTable __Itemtbl = new DataTable();
                        DataRow dr = null;
                        __Itemtbl.Columns.Add(new DataColumn("PODI_SEQ_NO", typeof(int)));
                        __Itemtbl.Columns.Add(new DataColumn("PODI_LINE_NO", typeof(int)));
                        __Itemtbl.Columns.Add(new DataColumn("PODI_ITM_CD", typeof(string)));
                        __Itemtbl.Columns.Add(new DataColumn("MI_LONGDESC", typeof(string)));
                        __Itemtbl.Columns.Add(new DataColumn("MI_MODEL", typeof(string)));
                        __Itemtbl.Columns.Add(new DataColumn("MI_BRAND", typeof(string)));
                        __Itemtbl.Columns.Add(new DataColumn("POD_ITM_STUS", typeof(string)));
                        __Itemtbl.Columns.Add(new DataColumn("PODI_QTY", typeof(decimal)));
                        __Itemtbl.Columns.Add(new DataColumn("PODI_BAL_QTY", typeof(decimal)));
                        __Itemtbl.Columns.Add(new DataColumn("GRN_QTY", typeof(decimal)));
                        __Itemtbl.Columns.Add(new DataColumn("UNIT_PRICE", typeof(decimal)));


                        foreach (InventoryRequestItem _addedItem in ScanItemList)
                        {
                            dr = __Itemtbl.NewRow();
                            dr["PODI_SEQ_NO"] = _addedItem.Itri_seq_no;
                            dr["PODI_LINE_NO"] = _addedItem.Itri_line_no;
                            dr["PODI_ITM_CD"] = _addedItem.Itri_itm_cd;
                            dr["GRN_QTY"] = _addedItem.Itri_qty;
                            DataTable _result = CHNLSVC.Inventory.Get_Item_Infor(_addedItem.Itri_itm_cd);
                            if (_result.Rows.Count > 0)
                            {
                                dr["MI_LONGDESC"] = _result.Rows[0][2].ToString();
                                dr["MI_MODEL"] = _result.Rows[0][8].ToString();
                                dr["MI_BRAND"] = _result.Rows[0][9].ToString();
                            }
                            DataTable _resultNew = CHNLSVC.Inventory.GetPoQty(_addedItem.Itri_res_no, _addedItem.Itri_job_line);
                            if (_resultNew.Rows.Count > 0)
                            {
                                dr["POD_ITM_STUS"] = _addedItem.Itri_itm_stus;
                                dr["PODI_QTY"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_qty"]);
                                dr["PODI_BAL_QTY"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_grn_bal"]);
                                dr["UNIT_PRICE"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_act_unit_price"]);
                            }
                            _itmPick = true;
                            _itmPickItemCode = _addedItem.Itri_itm_cd;
                            _itmPickLine = _addedItem.Itri_line_no;
                            __Itemtbl.Rows.Add(dr);
                            //Session["Inb_qty"] = _addedItem.Inb_qty;
                        }

                        DataTable po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), _invHdr.Ith_oth_docno, 2);
                        po_items.Columns.Add("RowNo", typeof(Int32));
                        _dtTable = po_items;
                        grdDOItems.DataSource = LoadDefRowNo(po_items);
                        grdDOItems.DataBind();
                        //grdItems.AutoGenerateColumns = false;
                        //grdItems.DataSource = ScanItemList;
                        //grdItems.DataBind();
                        grdDOSerials.AutoGenerateColumns = false;
                        grdDOSerials.DataSource = _serList;
                        grdDOSerials.DataBind();
                    }
                    else
                    {
                        //MessageBox.Show("Item not found!", "PRN No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocumentNo.Text = "";
                        txtDocumentNo.Focus();
                        return;
                    }
                    #endregion

                    foreach (GridViewRow gvr in grdDOSerials.Rows)
                    {

                        LinkButton Delrow = gvr.FindControl("lbtnRemove") as LinkButton;
                        Delrow.Enabled = false;
                        Delrow.OnClientClick = "return Enable();";

                    }
                    foreach (GridViewRow gvr in grdDOItems.Rows)
                    {
                        LinkButton Addrow = gvr.FindControl("lbtnGet") as LinkButton;
                        Addrow.Enabled = false;
                        Addrow.OnClientClick = "return Enable();";
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GetDocDataNew(string DocNo)
        {
            if (!string.IsNullOrEmpty(DocNo))
            {
                bool _invalidDoc = true;
                int _direction = 0;
                int _lineNo = 0;
                _direction = 1;

                #region Clear Data
                // grdItems.ReadOnly = false;
                //gvSerial.ReadOnly = false;

                List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                grdDOSerials.AutoGenerateColumns = false;
                grdDOSerials.DataSource = _emptySer;
                grdDOSerials.DataBind();

                List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                grdDOItems.AutoGenerateColumns = false;
                grdDOItems.DataSource = _emptyItm;
                grdDOItems.DataBind();

                // btnSave.Enabled = true;
                lbtnSave.Enabled = true;
                lbtnSave.OnClientClick = "SaveConfirm();";
                lbtnSave.CssClass = "buttonUndocolor";

                lbtnTempSave.Enabled = true;
                lbtnTempSave.OnClientClick = "SaveConfirm();";
                lbtnTempSave.CssClass = "buttonUndocolor";


                #endregion

                InventoryHeader _invHdr = new InventoryHeader();
                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();

                _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(DocNo);
                if (_invHdr != null)
                {
                    txtRemarks.Text = _invHdr.Ith_remarks;
                    txtPORefNo.Text = _invHdr.Ith_manual_ref;
                    txtEntry.Text = _invHdr.Ith_entry_no;
                    txtPONo.Text = _invHdr.Ith_oth_docno;
                }
                _serList = CHNLSVC.Inventory.Get_Int_Ser(DocNo);
                DataTable po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), _invHdr.Ith_oth_docno, 2);
                po_items.Columns.Add("RowNo", typeof(Int32));
                if (_serList != null)
                {
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });

                    foreach (var itm in _scanItems)
                    {


                        foreach (DataRow row in po_items.Rows)
                        {
                            if (itm.Peo.Tus_base_itm_line == Convert.ToInt32(row["PODI_LINE_No"].ToString()))
                            {

                                decimal qty = Convert.ToDecimal(row["GRN_QTY"].ToString());

                                row["GRN_QTY"] = itm.theCount;//itm.theCount; // Current scan qty



                            }
                        }
                        // var checgeqty = _serList.First(x => x.Tus_itm_cd == itm.Peo.Tus_itm_cd);
                        // checgeqty.Tus_qty = itm.theCount;
                    }

                }

                grdDOItems.DataSource = LoadDefRowNo(po_items);
                grdDOItems.DataBind();


                //var itemToRemove = _serList.FirstOrDefault(r => r.Tus_qty == 1);
                // _serList.RemoveAll(r => r.Tus_qty == 1);


                foreach (ReptPickSerials _ser in _serList)
                {

                    foreach (DataRow row in po_items.Rows)
                    {
                        if (_ser.Tus_base_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString()))
                        {
                            _ser.Tus_new_itm_cd = row["PODI_ITM_CD"].ToString();
                        }
                    }
                }

                grdDOSerials.AutoGenerateColumns = false;
                grdDOSerials.DataSource = _serList;
                grdDOSerials.DataBind();
                foreach (GridViewRow gvr in grdDOSerials.Rows)
                {

                    LinkButton Delrow = gvr.FindControl("lbtnRemove") as LinkButton;
                    Delrow.Enabled = false;
                    Delrow.OnClientClick = "return Enable();";

                }
                foreach (GridViewRow gvr in grdDOItems.Rows)
                {
                    LinkButton Addrow = gvr.FindControl("lbtnGet") as LinkButton;
                    Addrow.Enabled = false;
                    Addrow.OnClientClick = "return Enable();";
                }

            }
        }
        private void GetDocDataNew_2(string DocNo)
        {
            if (!string.IsNullOrEmpty(DocNo))
            {
                bool _invalidDoc = true;
                int _direction = 0;
                int _lineNo = 0;
                _direction = 1;

                #region Clear Data
                // grdItems.ReadOnly = false;
                //gvSerial.ReadOnly = false;

                List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                grdDOSerials.AutoGenerateColumns = false;
                grdDOSerials.DataSource = _emptySer;
                grdDOSerials.DataBind();

                List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                grdDOItems.AutoGenerateColumns = false;
                grdDOItems.DataSource = _emptyItm;
                grdDOItems.DataBind();

                // btnSave.Enabled = true;
                lbtnSave.Enabled = true;
                lbtnSave.OnClientClick = "SaveConfirm();";
                lbtnSave.CssClass = "buttonUndocolor";

                lbtnTempSave.Enabled = true;
                lbtnTempSave.OnClientClick = "SaveConfirm();";
                lbtnTempSave.CssClass = "buttonUndocolor";


                #endregion
                List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(DocNo);
                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                if (_serList != null)
                {
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost, x.Tus_base_doc_no, x.Tus_base_itm_line, x.Tus_seq_no }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        _lineNo += 1;
                        InventoryRequestItem _itm = new InventoryRequestItem();
                        _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                        _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                        _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                        _itm.Itri_line_no = _lineNo;
                        _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                        _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                        _itm.Mi_model = itm.Peo.Tus_itm_model;
                        _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                        _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                        _itm.Itri_res_no = itm.Peo.Tus_base_doc_no;
                        _itm.Itri_job_line = itm.Peo.Tus_base_itm_line;
                        _itm.Itri_seq_no = itm.Peo.Tus_seq_no;
                        _itmList.Add(_itm);

                    }
                    ScanItemList = _itmList;
                    DataTable __Itemtbl = new DataTable();
                    DataRow dr = null;
                    __Itemtbl.Columns.Add(new DataColumn("PODI_SEQ_NO", typeof(int)));
                    __Itemtbl.Columns.Add(new DataColumn("PODI_LINE_NO", typeof(int)));
                    __Itemtbl.Columns.Add(new DataColumn("PODI_ITM_CD", typeof(string)));
                    __Itemtbl.Columns.Add(new DataColumn("MI_LONGDESC", typeof(string)));
                    __Itemtbl.Columns.Add(new DataColumn("MI_MODEL", typeof(string)));
                    __Itemtbl.Columns.Add(new DataColumn("MI_BRAND", typeof(string)));
                    __Itemtbl.Columns.Add(new DataColumn("POD_ITM_STUS", typeof(string)));
                    __Itemtbl.Columns.Add(new DataColumn("PODI_QTY", typeof(decimal)));
                    __Itemtbl.Columns.Add(new DataColumn("PODI_BAL_QTY", typeof(decimal)));
                    __Itemtbl.Columns.Add(new DataColumn("GRN_QTY", typeof(decimal)));
                    __Itemtbl.Columns.Add(new DataColumn("UNIT_PRICE", typeof(decimal)));
                    __Itemtbl.Columns.Add("RowNo", typeof(Int32));

                    foreach (InventoryBatchN _addedItem in _nonserial)
                    {
                        dr = __Itemtbl.NewRow();
                        dr["PODI_SEQ_NO"] = 0;
                        dr["PODI_LINE_NO"] = _addedItem.Inb_itm_line;
                        dr["PODI_ITM_CD"] = _addedItem.Inb_itm_cd;
                        dr["GRN_QTY"] = _addedItem.Inb_qty;
                        DataTable _result = CHNLSVC.Inventory.Get_Item_Infor(_addedItem.Inb_itm_cd);
                        if (_result.Rows.Count > 0)
                        {
                            dr["MI_LONGDESC"] = _result.Rows[0][2].ToString();
                            dr["MI_MODEL"] = _result.Rows[0][8].ToString();
                            dr["MI_BRAND"] = _result.Rows[0][9].ToString();
                        }
                        //DataTable _resultNew = CHNLSVC.Inventory.GetPoQty(_addedItem.Itri_res_no, _addedItem.Itri_job_line);
                        //if (_resultNew.Rows.Count > 0)
                        //{
                        //    dr["POD_ITM_STUS"] = _addedItem.Itri_itm_stus;
                        //    dr["PODI_QTY"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_qty"]);
                        //    dr["PODI_BAL_QTY"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_grn_bal"]);
                        //    dr["UNIT_PRICE"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_unit_price"]);
                        //}

                        __Itemtbl.Rows.Add(dr);
                        //Session["Inb_qty"] = _addedItem.Inb_qty;
                    }
                    __Itemtbl = LoadDefRowNo(__Itemtbl);
                    _dtTable = __Itemtbl;
                    grdDOItems.DataSource = __Itemtbl;
                    grdDOItems.DataBind();
                    grdDOSerials.AutoGenerateColumns = false;
                    grdDOSerials.DataSource = _serList;
                    grdDOSerials.DataBind();
                }
                foreach (GridViewRow gvr in grdDOItems.Rows)
                {

                    Label qty = gvr.FindControl("PODI_QTY") as Label;
                    Label qty_b = gvr.FindControl("PODI_BAL_QTY") as Label;
                    qty.Visible = false;
                    qty_b.Visible = false;

                }
                grdDOItems.Columns[8].Visible = false;
                grdDOItems.Columns[9].Visible = false;
            }
        }
        private void GetItemSave(List<InventoryBatchN> _serListT, string _basedoc = null)
        {
            DataTable __Itemtbl = new DataTable();
            DataRow dr = null;
            __Itemtbl.Columns.Add(new DataColumn("PODI_SEQ_NO", typeof(int)));
            __Itemtbl.Columns.Add(new DataColumn("PODI_LINE_NO", typeof(int)));
            __Itemtbl.Columns.Add(new DataColumn("PODI_ITM_CD", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("MI_LONGDESC", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("MI_MODEL", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("MI_BRAND", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("POD_ITM_STUS", typeof(string)));
            __Itemtbl.Columns.Add(new DataColumn("PODI_QTY", typeof(decimal)));
            __Itemtbl.Columns.Add(new DataColumn("PODI_BAL_QTY", typeof(decimal)));
            __Itemtbl.Columns.Add(new DataColumn("GRN_QTY", typeof(decimal)));
            __Itemtbl.Columns.Add(new DataColumn("UNIT_PRICE", typeof(decimal)));
            __Itemtbl.Columns.Add("RowNo", typeof(Int32));
            List<PurchaseOrderDetail> polist = new List<PurchaseOrderDetail>();
            polist = CHNLSVC.Inventory.GetPOItemsList(_basedoc);
            foreach (InventoryBatchN _addedItem in _serListT)
            {
                dr = __Itemtbl.NewRow();
                dr["PODI_SEQ_NO"] = _addedItem.Inb_batch_line;
                dr["PODI_LINE_NO"] = _addedItem.Inb_base_itmline;
                dr["PODI_ITM_CD"] = _addedItem.Inb_base_itmcd;

                DataTable _result = CHNLSVC.Inventory.Get_Item_Infor(_addedItem.Inb_base_itmcd);
                if (_result.Rows.Count > 0)
                {
                    dr["MI_LONGDESC"] = _result.Rows[0][2].ToString();
                    dr["MI_MODEL"] = _result.Rows[0][8].ToString();
                    dr["MI_BRAND"] = _result.Rows[0][9].ToString();
                }
                if (_basedoc == null)
                {
                    _basedoc = _addedItem.Inb_base_doc_no;
                }
                //DataTable _resultNew = CHNLSVC.Inventory.GetPoQty(_basedoc, _addedItem.Inb_base_itmline);
                //dr["POD_ITM_STUS"] = _addedItem.Inb_base_itmstus;
                //dr["PODI_QTY"] = Convert.ToDecimal(_resultNew.Rows[0]["POD_QTY"]);
                //dr["PODI_BAL_QTY"] = Convert.ToDecimal(_resultNew.Rows[0]["POD_GRN_BAL"]);
                //dr["GRN_QTY"] = _addedItem.Inb_qty;
                //dr["UNIT_PRICE"] = Convert.ToDecimal(_resultNew.Rows[0]["pod_unit_price"]);
                if (polist.Count > 0)
                {
                    var _filter = polist.Find(x => x.Pod_line_no == _addedItem.Inb_base_itmline);
                    if (_filter != null)
                    {
                        dr["POD_ITM_STUS"] = _addedItem.Inb_base_itmstus;
                        dr["PODI_QTY"] = Convert.ToDecimal(_filter.Pod_qty);
                        dr["PODI_BAL_QTY"] = Convert.ToDecimal(_filter.Pod_grn_bal);
                        dr["GRN_QTY"] = _addedItem.Inb_qty;
                        dr["UNIT_PRICE"] = Convert.ToDecimal(_filter.Pod_unit_price);
                        __Itemtbl.Rows.Add(dr);
                        Session["Inb_qty"] = _addedItem.Inb_qty;
                    }
                }
                _itmPick = true;
                _itmPickItemCode = _addedItem.Inb_base_itmcd;
                _itmPickLine = _addedItem.Inb_base_itmline;
            }
            __Itemtbl = LoadDefRowNo(__Itemtbl);
            grdDOItems.DataSource = __Itemtbl;
            grdDOItems.DataBind();
            object sumObject;
            decimal tmpDes = 0;
            sumObject = __Itemtbl.Compute("Sum(PODI_QTY)", "");
            lblpototal.Text = decimal.TryParse(sumObject.ToString(), out tmpDes) ? Convert.ToDecimal(sumObject.ToString()).ToString("N2") : "";
            object sumObjectpick;
            sumObjectpick = __Itemtbl.Compute("Sum(GRN_QTY)", "");
            lblpicktotal.Text = decimal.TryParse(sumObjectpick.ToString(), out tmpDes) ? Convert.ToDecimal(sumObjectpick.ToString()).ToString("N2") : "";
            return;
        }

        //Tharaka 2015-12-04
        private void loadInterCompanyPOSerials(String PONumber, String SuppCode, string InvoiceNumber)
        {
            if (!string.IsNullOrEmpty(PONumber) && !string.IsNullOrEmpty(SuppCode))
            {
                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(SuppCode, null, null, null, null, Session["UserCompanyCode"].ToString());
                if (_masterBusinessCompany != null && _masterBusinessCompany.Mbe_intr_com && _masterBusinessCompany.Mbe_tp == "C")
                {
                    List<ReptPickSerials> _serList = CHNLSVC.Sales.GetServicesForPO(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), InvoiceNumber);

                    grdDOSerials.AutoGenerateColumns = false;
                    grdDOSerials.DataSource = _serList;
                    grdDOSerials.DataBind();

                    grdDOSerials_2.AutoGenerateColumns = false;
                    grdDOSerials_2.DataSource = _serList;
                    grdDOSerials_2.DataBind();
                }
            }
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            if (txtpdasend.Value == "Yes")
            {
                try
                {
                    SaveData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                    CHNLSVC.CloseChannel();
                }
                finally
                {
                    CHNLSVC.CloseAllChannels();
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
        protected void grdPendingPo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string Pono = (grdPendingPo.SelectedRow.FindControl("lbloutwrdnopending") as Label).Text;

            //DataTable dtdoccheck1 = CHNLSVC.Inventory.IsDocNoAvailable(Pono, "AOD", 1, Session["UserCompanyCode"].ToString());

            MPPDA.Show();
        }
        protected void lbtnPDA_Click(object sender, EventArgs e)
        {
            if (grdPendingPo.Rows.Count == 0) return;
            //Added By Dulaj 2018 Mar 22
            CheckExessQty();
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                if (chkpda.Checked == true)
                {
                    string Pono = (row.FindControl("POH_DOC_NO") as Label).Text;
                    DataTable dtdoccheck1 = CHNLSVC.Inventory.IsDocNoAvailable(Pono, "GRN", 1, Session["UserCompanyCode"].ToString());
                    if (dtdoccheck1.Rows.Count > 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + Pono + " !!!')", true);
                        return;
                    }
                    DataTable _headerchk2 = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), Pono);
                    Int64 _seqno = 0;
                    bool _pickItmAva = false;
                    bool _pickSerAva = false;
                    ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                    {
                        Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                        Tuh_doc_no = Pono,
                        Tuh_direct = true
                        //Tuh_doc_tp = ddlType.SelectedValue
                    }).FirstOrDefault();
                    if (_tmpPickHdr != null)
                    {
                        List<ReptPickItems> _tmpItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_tmpItmList != null)
                        {
                            if (_tmpItmList.Count > 0)
                            {
                                _pickItmAva = true;
                            }
                        }
                        List<ReptPickSerials> _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                _pickSerAva = true;
                            }
                        }
                    }
                    if (_pickItmAva)
                    {
                        if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA !!!')", true);
                            return;
                        }
                        else
                        {
                            string msg2 = "Document " + Pono + " had been already scanned by the user " + _tmpPickHdr.Tuh_usr_id + " on " + _tmpPickHdr.Tuh_cre_dt.ToString("dd/MMM/yyyy");
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                            return;
                        }
                    }
                    if (_pickSerAva)
                    {
                        if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_load_bay))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA !!!')", true);
                            return;
                        }
                        else
                        {
                            string msg2 = "Document " + Pono + " had been already scanned by the user " + _tmpPickHdr.Tuh_usr_id + " on " + _tmpPickHdr.Tuh_cre_dt.ToString("dd/MMM/yyyy");
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                            return;
                        }
                    }
                    //if (_headerchk2 != null && _headerchk2.Rows.Count > 0)
                    //{
                    //    string _headerUser = _headerchk2.Rows[0].Field<string>("tuh_usr_id");
                    //    string _scanDate = Convert.ToString(_headerchk2.Rows[0].Field<DateTime>("tuh_cre_dt"));
                    //    _seqno = _headerchk2.Rows[0].Field<Int64>("TUH_USRSEQ_NO");
                    //    if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                    //        {

                    //            if (_pickItmAva)
                    //            {
                    //                string msg2 = "Document " + Pono + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                    //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                    //                return;
                    //            }
                    //        }
                    //}
                    //DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(Convert.ToInt32(_seqno));

                    //if (dtchkitm.Rows.Count > 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                    //    return;
                    //}
                    txtdocname.Text = Pono;
                    MPPDA.Show();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No permission to add PDA')", true);
            }
        }

        private void SaveData()
        {
            Int32 val = 0;
            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];
            if (string.IsNullOrEmpty(txtdocname.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the document no !!!')", true);
                txtdocname.Focus();
                MPPDA.Show();
                return;
            }

            if (ddlloadingbay.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the loading bay !!!')", true);
                ddlloadingbay.Focus();
                MPPDA.Show();
                return;
            }
            // Int32 _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "GRN", 1, Session["UserCompanyCode"].ToString());
            DataTable po_items = new DataTable();
            // po_items = ViewState["po_items"] as DataTable;
            //  if(po_items==null){
            if (ddlMainType.SelectedValue == "L")
            {
                po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtdocname.Text, 1);
            }
            else
            {
                po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtdocname.Text, 2);
            }
            //po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtdocname.Text, 2);
            //if (po_items.Rows.Count == 0)
            //{
            //    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtdocname.Text, 1);
            //}
            foreach (DataRow drValue in po_items.Rows)
            {
                if (drValue["podi_bal_qty"].ToString() == "0")
                {
                    drValue.Delete();
                }

            }

            po_items.AcceptChanges();
            ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA_PARTIAL(new ReptPickHeader()
                        {
                            Tuh_doc_no = txtdocname.Text.Trim(),//invHdr.Ith_oth_docno,
                            Tuh_doc_tp = "GRN",
                            Tuh_direct = false,
                            Tuh_usr_com = Session["UserCompanyCode"].ToString()
                        }).FirstOrDefault();
            if (_tmpPickHdr != null)
            {
                List<ReptPickItems> _repItmListPart = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA_PARTIAL(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                if (_repItmListPart != null)
                {
                    if (_repItmListPart.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                        return;
                    }
                }
            }
            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtdocname.Text, 1);
            if (user_seq_num == -1)
            {
                user_seq_num = GenerateNewUserSeqNo("GRN", txtdocname.Text);
                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                _inputReptPickHeader.Tuh_doc_tp = "GRN";
                _inputReptPickHeader.Tuh_direct = true;
                _inputReptPickHeader.Tuh_ischek_itmstus = false;
                _inputReptPickHeader.Tuh_ischek_simitm = false;
                _inputReptPickHeader.Tuh_ischek_reqqty = false;
                _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                _inputReptPickHeader.Is_doc_Partial_save = true;
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

                _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                _inputReptPickHeader.Tuh_doc_tp = "GRN";
                _inputReptPickHeader.Tuh_direct = true;
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                _inputReptPickHeader.Is_doc_Partial_save = true;
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
                if (po_items.Rows.Count > 0)
                {
                    //06/Apr/2016 comment by Rukshan (chamal request) 

                    List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                    foreach (DataRow _row in po_items.Rows)
                    {
                        string _item = _row["PODI_ITM_CD"].ToString();
                        string _cost = _row["UNIT_PRICE"].ToString();
                        string _qty = _row["podi_bal_qty"].ToString();
                        string _line = _row["PODI_LINE_NO"].ToString();
                        string _STATUS = _row["POD_ITM_STUS"].ToString();
                        //AddItem(_item, _cost, null, null, user_seq_num.ToString(), null);

                        ReptPickItems _reptitm = new ReptPickItems();
                        _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                        _reptitm.Tui_req_itm_qty = Convert.ToDecimal(_qty);
                        _reptitm.Tui_req_itm_cd = _item;
                        _reptitm.Tui_req_itm_stus = _STATUS;
                        _reptitm.Tui_pic_itm_cd = _line;
                        // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                        _reptitm.Tui_pic_itm_qty = 0;
                        _reptitm.ls_par_save = true;
                        _saveonly.Add(_reptitm);


                    }
                    val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                }

            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                MPPDA.Hide();
            }
        }


        protected void btnClose3_Click(object sender, EventArgs e)
        {
            lblalert.Text = "";
            lblsuccess2.Text = "";
            lblsuccess2.Visible = false;
            lblalert.Visible = false;
            // divUpcompleted.Visible = false;
        }
        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                grdDOSerials.DataSource = null;
                grdDOSerials.DataBind();

                //ExcelUpPanelView(false);
                lblalert.Visible = false;
                lblalert.Text = "";
                if (fileupexcelupload.HasFile)
                {
                    string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                    if (Extension != ".xls" && Extension != ".xlsx" && Extension != ".XLS" && Extension != ".XLSX") //change with xlsx
                    {

                        lblalert.Visible = true;
                        lblalert.Text = "Please select a valid excel (.xls) file";
                        excelUpload.Show();
                        //ExcelUpPanelView(true);
                        return;
                    }
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    //LoadData(FolderPath + FileName);
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fileupexcelupload.SaveAs(FilePath);
                    Session["FilePath"] = FilePath;
                    int value = 0;
                    List<ReptPickSerials> _misMatchSerials = new List<ReptPickSerials>();
                    ExcelProcess(FilePath, 1, out value, out _misMatchSerials);
                    if (value == 1)
                    {
                        //if (_excelReptPickSerials != null)
                        //{
                        //    value = CHNLSVC.Inventory.SaveAllScanSerialsList(_excelReptPickSerials, _reptPickSerialsSub);
                        //    LoadPOItems(txtPONo.Text.ToString());
                        //}


                        if (_misMatchSerials != null) // _excelMisMatchReptPickSerials
                        {
                            if (_misMatchSerials.Count > 0)
                            {
                                value = CHNLSVC.Inventory.SaveAllScanSerialsList(_misMatchSerials, null);
                                var groupedCustomerList = _misMatchSerials.GroupBy(x => x.Tus_itm_cd).Select(y => y.First());
                                grdExcelItem.DataSource = groupedCustomerList;
                                grdExcelItem.DataBind();
                                popupexcelitem.Show();
                            }
                        }

                        lblalert.Visible = false;
                        lblsuccess2.Visible = true;
                        lblsuccess2.Text = "Suucessfully save excel data";
                        pnlupload2.Visible = false;
                        excelUpload.Show();
                        //ExcelUpPanelView(true);
                        return;
                    }
                    else if (value == 2)
                    {

                        lblsuccess2.Visible = false;
                        lblalert.Visible = true;
                        lblalert.Text = "Added serial number is already exist!";
                        excelUpload.Show();
                        //ExcelUpPanelView(true);
                        return;

                    }
                    else if (value == 3)
                    {

                        lblsuccess2.Visible = false;
                        lblalert.Visible = true;
                        lblalert.Text = "Excel  Data Invalid Please check Excel File and Upload";
                        excelUpload.Show();
                        //ExcelUpPanelView(true);
                        return;
                    }
                    else if (value == 4)
                    {
                        // DisplayMessage("You can not upload minus and zero values", 1);
                        lblsuccess2.Visible = false;
                        lblalert.Visible = true;
                        lblalert.Text = "You can not upload minus and zero values";
                        excelUpload.Show();
                        //ExcelUpPanelView(true);
                        return;
                    }
                    else if (value == 5)
                    {
                        // DisplayMessage("Excel contains duplicate records please check", 2);
                        lblalert.Visible = true;
                        lblalert.Text = "Excel contains duplicate records please check";
                        excelUpload.Show();
                        //ExcelUpPanelView(true);
                        return;
                    }
                    else if(value==40)
                    {
                        ModalPopupExtenderExcelValidation.Show();                       
                        excelUpload.Hide();                     
                        return;
                    }
                    else if (value == 41)
                    {
                        //ModalPopupExtenderExcelValidation.Show();
                        excelUpload.Hide();
                        lblalert.Visible = true;
                        lblalert.Text = "Decimal items should be in same bin";
                        excelUpload.Show();
                        return;
                    }
                    else
                    {
                        // DisplayMessage("Excel contains duplicate records please check", 2);
                        lblalert.Visible = true;
                        lblalert.Text = "Error in process";
                        excelUpload.Show();
                        //ExcelUpPanelView(true);
                        return;
                    }
                    // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Excel file upload completed. Do you want to process')", true);

                    excelUpload.Show();
                    //ExcelUpPanelView(true);
                    //Import_To_Grid(FilePath, Extension);
                }
                else
                {
                    lblalert.Visible = true;
                    lblalert.Text = "Please select the correct upload file path";
                    // DisplayMessage("Please select the correct upload file path", 2);
                    excelUpload.Show();
                    //ExcelUpPanelView(true);
                    // divalert.Visible = true;
                    // lblalert.Text = "Please select an excel file";
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                //ExcelUpPanelView(true);
            }
        }

        protected void ExcelUpPanelView(bool check)
        {
            //fileupexcelupload.Visible = check;
            //btnUpload.Visible = check;
            //btnClose3.Visible = check;
            pnlupload2.Visible = check;
            if (check == false)
            {
                string msg = check == false ? "Upload Progress Please Wait...!!!" : "";
                lblalert.Text = msg;
            }
        }

        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPONo.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select puchase order')", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Please unselect blank cell/cells which you have selected in excel sheet')", true);
            lblalert.Text = "";
            lblsuccess2.Text = "";
            lblsuccess2.Visible = false;
            lblalert.Visible = false;
            pnlupload2.Visible = true; ;
            excelUpload.Show();
        }
        public DataTable[] LoadData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                cn.Open();
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dtExcelSchema;
                cmdExcel.Connection = cn;

                dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cn.Close();

                //Read Data from First Sheet
                cn.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(Tax);


                return new DataTable[] { Tax };
            }
        }

        private void ExcelProcess(string FilePath, int option, out int value, out List<ReptPickSerials> _misMatchSerials)
        {
            value = 0;
            _misMatchSerials = new List<ReptPickSerials>();
            int _userSeqNo = 0;
            string msg = string.Empty;
            string statauscd = string.Empty;
            bool lblView = false;
            _excelReptPickSerials = new List<ReptPickSerials>();
            _excelMisMatchReptPickSerials = new List<ReptPickSerials>();
            DataTable[] GetExecelTbl = LoadData(FilePath);
            DataTable tblExcel = GetExecelTbl[0];
            tblExcel.TableName = "ExcelD";
            DataView dv = new DataView(tblExcel);
            dv.RowFilter = " F3 <> 'N/A'";
            DataTable tblchkSerDup = dv.ToTable();

            //Validate Duplicate Serial For Decimal
            DataTable decimalTable = new DataTable();
            DataTable decimalTableRemain = new DataTable();
            decimalTable = tblExcel;
           // decimalTable.Rows.RemoveAt(0);
            int count = decimalTable.Rows.Count;
            bool duplicates = false;
            for (int i = 1; i < count; i++)
            {
                MasterItem _itemdetail = CHNLSVC.General.GetItemMaster(tblExcel.Rows[i][0].ToString());
                string serial = tblExcel.Rows[i][2].ToString();
                if (_itemdetail != null)
                {
                    if (_itemdetail.Mi_is_ser1 == 1)
                    {
                        DataTable temp = new DataTable();
                        var result = decimalTable.AsEnumerable().Where(r => r.Field<string>("F3").Equals(serial));
                        if (result.Any())
                        {
                            decimalTableRemain = result.CopyToDataTable();
                            if (decimalTableRemain.Rows.Count > 1)
                            {
                                duplicates = true;
                            }
                        }
                    }
                }
            }
            
            //List<string> allDuplicates = decimalTable.AsEnumerable()
            //    .GroupBy(dr => dr.Field<string>("F3"), dr => dr.Field<string>("F1"))
            //    .Where(g => g.Count() > 1)
            //    .SelectMany(g => g)
            //    .ToList();

            if (duplicates)
            {
                msg = "Serial 1 can not be duplicate...!!!";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                value = 5;
                return;
            }


            //for (int i = 0; i < grdDOSerials.Rows.Count; i++)
            //{
            //    Label _ser1= grdDOSerials.Rows[i].FindControl("TUS_SER_1") as Label;
            //    Label _item= grdDOSerials.Rows[i].FindControl("TUS_ITM_CD") as Label;
            //    var serial = tblExcel.AsEnumerable().Where(r => r.Field<string>("F4") == _ser1.Text && _item.Text == r.Field<string>("F1"));
            //    if(serial.Any())
            //    {
            //        msg = "Item " + _item.Text + " and " + "Serial 1 " + _ser1.Text + "already exists!!!";
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
            //        value = 6;
            //        return;
            //    }
            //}

            DataTable _WARUSER = CHNLSVC.Sales.GetinvUser(Session["UserID"].ToString());
            string _userwarrid = "-P01-";
            if (_WARUSER != null)
            {
                if (_WARUSER.Rows.Count > 0)
                {
                    _userwarrid = "-" + _WARUSER.Rows[0][32].ToString() + "-";
                    if (string.IsNullOrEmpty(_userwarrid))
                    {
                        _userwarrid = "-P01-";
                    }
                }
            }

            //-------------------------------added by Wimal - 02/Aug/2018 - Start

            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        // GetExecelTbl[0].Rows[i][1] = "";
                        //DataTable _test = ViewState["po_items"] as DataTable;
                        string item = "";
                        if (GetExecelTbl[0].Rows[0][0].ToString() == "MODEL")
                        {
                            string MODEL = GetExecelTbl[0].Rows[i][0].ToString();
                            List<MasterItem> itemlist = CHNLSVC.General.GetItemFromModel(MODEL);
                            if (itemlist.Count == 1)
                            {
                                item = itemlist[0].Mi_cd;
                            }
                            else if (itemlist.Count > 1)
                            {
                                msg = MODEL + " Model assign to more than one item...!!!";
                                value = 6;
                                return;
                            }
                            else
                            {
                                msg = MODEL + " NO item assign to Model...!!!";
                                value = 6;
                                return;
                            }
                        }
                        else
                        {
                            item = GetExecelTbl[0].Rows[i][0].ToString();
                        }

                        MasterItem _mstItm = CHNLSVC.General.GetItemMaster(item);
                        //bool exists = _test.AsEnumerable().Any(r => item == r.Field<string>("PODI_ITM_CD"));
                        //if (exists == false)
                        //{
                        //    msg = item + " item not allocate to selected PO no...!!!";
                        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                        //    return;
                        //}
                        if (_mstItm == null)
                        {
                            msg = item + " item no invalid please check...!!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                            value = 6;
                            return;
                        }

                        string status = GetExecelTbl[0].Rows[i][1].ToString();
                        string qty = GetExecelTbl[0].Rows[i][4].ToString();

                        string serial1 = GetExecelTbl[0].Rows[i][2].ToString();
                        string serial2 = GetExecelTbl[0].Rows[i][3].ToString();
                        string serial3 = string.Empty; //GetExecelTbl[0].Rows[i][5].ToString(); //Remove from Naveen request

                        //if ((_mstItm.Mi_is_ser1 == 1) && ((qty =="") && Convert.ToInt16(qty) != 1))
                        //{
                        //    msg = serial1 + " serial can not be duplicate quantity should be 1...!!!";
                        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                        //    value = 6;
                        //    return;
                        //}

                        if ((_mstItm.Mi_is_ser1 != 1) && (serial1 == ""))
                        {
                            msg = item + " None Serial or Decimal item  enter Serial1 as N/A...!!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                            value = 6;
                            return;
                        }

                        if ((_mstItm.Mi_is_ser1 != 1) && ((qty == "") || Convert.ToInt16(qty) < 1))
                        {
                            msg = item + " None Serial or Decimal item  should have qty...!!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                            value = 6;
                            return;
                        }

                        if ((_mstItm.Mi_is_ser1 == 1) && (serial1 == "" || serial1 == "N/A"))
                        {
                            msg = "In item no " + item + " Serial no 1 is empty or N/A please check excel sheet...!!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                            value = 6;
                            return;
                }

                        if ((_mstItm.Mi_is_ser2 == 1) && (serial2 == "" || serial1 == "N/A"))
                        {
                            msg = "In item no " + item + " Serial no 2 is or N/A empty please check excel sheet...!!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                            value = 6;
                            return;
            }

                        if ((_mstItm.Mi_is_ser1 == 0) && (serial1 != null && serial1 != "" && serial1 != "N/A"))
                        {
                            msg = "In item no " + item + " is non serialize but excel have serial 1 value - " + serial1 + " please check it...!!!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                            value = 6;
                            return;
                        }

                        var statusVal = _statusList.Where(c => c.Mis_desc == status).FirstOrDefault();
                        if (statusVal != null)
                        {
                            statauscd = statusVal.Mis_cd;
                        }

                    }
                    // ViewState["_lstTaxDet"] = _lstTaxDet;
                }
            }

            //-------------------------------added by Wimal - 02/Aug/2018 - End         

                     List<string> allDuplicates = tblchkSerDup.AsEnumerable()
             .GroupBy(dr => dr.Field<string>("F3"), dr => dr.Field<string>("F1"))
             .Where(g => g.Count() > 1)
             .SelectMany(g => g)
             .ToList();

            //List<string> allDuplicates = tblExcel.AsEnumerable()
            //    .GroupBy(dr => dr.Field<string>("F3"), dr => dr.Field<string>("F1"))
            //    .Where(g => g.Count(d => d.Field<string>("F3")!="N/A") > 1)
            //    .SelectMany(g => g)
            //    .ToList();

            if (allDuplicates.Any())
            {
                msg = "Serial 1 can not be duplicate...!!!";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                value = 5;
                return;
            }

            _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
            List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");
            DataTable _test = new DataTable { TableName = "ExistData" };
            _test = ViewState["po_items"] as DataTable;
            //Dulaj 2018/Nov/01
            List<ExcelValidation> _excelValidationList = new List<ExcelValidation>();
            List<ExcelValidation> _excelNonSerialItmeList = new List<ExcelValidation>();
            List<DecimalItemBin> _decimalList = new List<DecimalItemBin>();
            List<ItemDecimal> _itemDecimals = new List<ItemDecimal>();           

            if (tblExcel != null)
            {
                if (tblExcel.Rows.Count > 0)
                {
                    for (int i = 1; i < tblExcel.Rows.Count; i++)
                    {
                        try
                        {ExcelValidation _excelValidation = new ExcelValidation(); 
                             
                            string otherBin = tblExcel.Rows[i][5].ToString();
                            string item = tblExcel.Rows[i][0].ToString();
                            MasterItem _itemdetail = CHNLSVC.General.GetItemMaster(item);
                            if(_itemdetail!=null)
                            {
                                if(_itemdetail.Mi_is_ser1==-1)
                                {
                                    ItemDecimal itemDecimal = new ItemDecimal();
                                    itemDecimal.ItmCode = item;
                                    itemDecimal.Serial = tblExcel.Rows[i][2].ToString(); 
                                    _itemDecimals.Add(itemDecimal);
                                }
                                if(_itemdetail.Mi_is_ser1==0||_itemdetail.Mi_is_ser1==-1)
                                {
                                    _excelValidation.RowNo = i + 1;
                                    _excelValidation.Code = item;
                                    _excelValidation.Error = "Same non-serialized item(s) available in more than one row.";
                                    _excelNonSerialItmeList.Add(_excelValidation);    
                                }
                            }
                            if (!(string.IsNullOrEmpty(otherBin)))
                            {
                                DataTable dt = CHNLSVC.General.GetBinLocGRN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), otherBin, "");
                                if (dt.Rows.Count < 1)
                                {
                                    _excelValidation = new ExcelValidation();
                                    _excelValidation.RowNo = i + 1;
                                    _excelValidation.Code = otherBin;
                                    _excelValidation.Error = "Invalid Bin code";
                                    _excelValidationList.Add(_excelValidation);
                                }
                                
                                if (!(string.IsNullOrEmpty(item)))
                                {
                                    
                                    if (_itemdetail != null)
                                    {
                                        if (_itemdetail.Mi_is_ser1 == -1)
                                        {
                                            DecimalItemBin dItmbin = new DecimalItemBin();
                                            dItmbin.ItmCode = item;
                                            dItmbin.Bin = otherBin;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                
                                if (_itemdetail != null)
                                {
                                    if (_itemdetail.Mi_is_ser1 == -1)
                                    {
                                        DecimalItemBin dItmbin = new DecimalItemBin();
                                        dItmbin.ItmCode = item;
                                        dItmbin.Bin = otherBin;
                                        _decimalList.Add(dItmbin);
                                    }
                                }
                            }
                                                     
                        }
                        catch (Exception ex)
                        {
                           
                        }
                    }
                }
            }
            if (_decimalList.Count>0)
            {               
                _decimalList = _decimalList.Where(x=>x.Bin!=_decimalList[0].Bin).ToList(); 
                if(_decimalList.Count>0)
                {
                    value = 41;
                    return;
                }
            }
            //NonSerial ItemDuplicate Validation Added By Dulaj 2018/Dec/28
            List<ExcelValidation> nonserialDuplicatesList = new List<ExcelValidation>();
            foreach(ExcelValidation excelNonSerItm in _excelNonSerialItmeList)
            {
                var someCount = _excelNonSerialItmeList.Count(y => y.Code == excelNonSerItm.Code);
                if(someCount>1)
                {
                    nonserialDuplicatesList.Add(excelNonSerItm);
                }
            }
            //

            if (_excelValidationList.Count > 0 || nonserialDuplicatesList.Count>0)
            {
                _excelValidationList.AddRange(nonserialDuplicatesList);
                GridViewExcelValidation.DataSource = _excelValidationList;
                GridViewExcelValidation.DataBind();
                value = 40;
                return;
            }
            else
            {
                GridViewExcelValidation.DataSource = null;
                GridViewExcelValidation.DataBind();
            }
            ///
            CHNLSVC.Inventory.GRNExcelUploadValidation(tblExcel, _userSeqNo, _resultItemsSerialList, _userwarrid, txtPONo.Text, Session["SessionID"].ToString(), Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString(), _test, txtCDate.Text, txtPORefNo.Text, txtBincode.Text, txtEDate.Text, txtMdate.Text, out lblView, out msg, out value, out _misMatchSerials);
            if (msg != null && msg != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                if (lblView == true)
                {
                    excelUpload.Show();
                }
                else
                {
                    excelUpload.Dispose();
                    return;
                }
            }
            #region excel validation
            //if (GetExecelTbl != null)
            //{
            //    if (GetExecelTbl[0].Rows.Count > 0)
            //    {
            //        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
            //        {
            //            try
            //            {
            //                // GetExecelTbl[0].Rows[i][1] = "";
            //                //DataTable _test = ViewState["po_items"] as DataTable;
            //                string item = GetExecelTbl[0].Rows[i][0].ToString();
            //                MasterItem _mstItm = CHNLSVC.General.GetItemMaster(item);
            //                bool exists = _test.AsEnumerable().Any(r => item == r.Field<string>("PODI_ITM_CD"));
            //                if (exists == false)
            //                {
            //                    msg = item + " item not allocate to selected PO no...!!!";
            //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
            //                    return;
            //                }
            //                if (_mstItm == null)
            //                {
            //                    msg = item + " item no invalid please check...!!!";
            //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
            //                    value = 6;
            //                    return;
            //                }

            //                string status = GetExecelTbl[0].Rows[i][1].ToString();
            //                string qty = "1";//GetExecelTbl[0].Rows[i][2].ToString(); //Remove from Naveen request

            //                string serial1 = GetExecelTbl[0].Rows[i][2].ToString();
            //                string serial2 = GetExecelTbl[0].Rows[i][3].ToString();
            //                string serial3 = string.Empty; //GetExecelTbl[0].Rows[i][5].ToString(); //Remove from Naveen request

            //                if ((_mstItm.Mi_is_ser1 == 1) && Convert.ToInt16(qty) != 1)
            //                {
            //                    msg = serial1 + " serial can not be duplicate quantity should be 1...!!!";
            //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
            //                    value = 6;
            //                    return;
            //                }

            //                if ((_mstItm.Mi_is_ser1 == 1) && (serial1 == "" || serial1 == "N/A"))
            //                {
            //                    msg = "In item no " + item + " Serial no 1 is empty please check excel sheet...!!!";
            //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
            //                    value = 6;
            //                    return;
            //                }

            //                if ((_mstItm.Mi_is_ser2 == 1) && (serial2 == "" || serial1 == "N/A"))
            //                {
            //                    msg = "In item no " + item + " Serial no 2 is empty please check excel sheet...!!!";
            //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
            //                    value = 6;
            //                    return;
            //                }

            //                if ((_mstItm.Mi_is_ser1 == 0) && (serial1 != null && serial1 != "" && serial1 != "N/A"))
            //                {
            //                    msg = "In item no " + item + " is non serialize but excel have serial 1 value - " + serial1 + " please check it...!!!";
            //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
            //                    value = 6;
            //                    return;
            //                }
            //                var statusVal = _statusList.Where(c => c.Mis_desc == status).FirstOrDefault();
            //                if (statusVal != null)
            //                {
            //                    statauscd = statusVal.Mis_cd;
            //                }
            //                saveexcel("GRN", txtPONo.Text, item, serial1, serial2, serial3, _test, statauscd, Convert.ToDecimal(qty), _userSeqNo, _resultItemsSerialList, _userwarrid, out value);
            //            }
            //            catch (Exception ex)
            //            {
            //                Label3.Visible = true;
            //                Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
            //                excelUpload.Show();
            //                value = 3;
            //                return;
            //            }
            //        }
            //        // ViewState["_lstTaxDet"] = _lstTaxDet;
            //    }
            //}
            #endregion excel validation
        }


        protected void grdDOSerials_2_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var base_itm = e.Row.FindControl("tus_new_itm_cd") as Label;
            var itm = e.Row.FindControl("TUS_ITM_CD") as Label;
            if (itm != null)
            {
                if (itm.Text != base_itm.Text)
                    e.Row.ForeColor = System.Drawing.Color.Red;


            }

        }
        protected void grdDOSerials_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var base_itm = e.Row.FindControl("tus_new_itm_cd") as Label;
            var itm = e.Row.FindControl("TUS_ITM_CD") as Label;
            if (itm != null)
            {
                if (itm.Text != base_itm.Text)
                    e.Row.ForeColor = System.Drawing.Color.Red;


            }

        }
        protected void lbtnGet_2_Click(object sender, EventArgs e)
        {
            if (grdDOItems_2.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _item = (row.FindControl("PODI_ITM_CD") as Label).Text; ;
                string _lineno = (row.FindControl("PODI_LINE_NO") as Label).Text; ;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (msitem != null)
                {
                    // string Code = _result.Rows[0][0].ToString();
                    DataTable _test = ViewState["po_items"] as DataTable;
                    CheckItemAllreadyInPo_2(_item, _test, _lineno);
                    string Des = msitem.Mi_longdesc;//_result.Rows[0][1].ToString();
                    string Model = msitem.Mi_model;//_result.Rows[0][2].ToString();
                    string Brand = msitem.Mi_brand;//_result.Rows[0][3].ToString();
                    string Part = msitem.Mi_part_no;//_result.Rows[0][4].ToString();
                    txtitemcode_2.Text = _item;
                    lbtnmodel_2.Text = Model;
                    lbtnbrand_2.Text = Brand;
                    lbtnpartno_2.Text = Part;
                    lbtnDes_2.Text = Des;
                    lbtnselectitem.Text = _item;
                    txtitemcode_2.Focus();
                    Session["LineNO"] = _lineno;
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please type correct item code');", true);
                }
            }
        }

        protected void lbtnadd_2_Click(object sender, EventArgs e)
        {


            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            //DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + txtItemCode.Text.ToString());
            //if (_result.Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please type correct item code');", true);
            //    txtqty.Text = "";
            //    txtItemCode.Focus();
            //    return;
            //}
            txtqty.Text = txtQty_2.Text;
            decimal integer;
            if (!decimal.TryParse(txtqty.Text, out integer))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This item not allow decimal value!');", true);
                txtqty.Text = "";
                return;
            }
            if (txtqty.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('can't add zero value!');", true);
                txtqty.Text = "";
                return;
            }

            Session["_itemSerializedStatus"] = "0";
            //lbtnItemAdd_Click(null, null);
            bindItem(txtQty_2.Text);
            lbtnselectitem.Text = "N/A";
            txtitemcode_2.Text = string.Empty;
            if (ddlMainType.SelectedValue == "I")
            {
                ddlStatus_2.SelectedValue = "GOD";
            }
            else
            {
                ddlStatus_2.SelectedValue = "GDLP";
            }
            txtQty_2.Text = string.Empty;
            lbtnDes_2.Text = "N/A";
            lbtnmodel_2.Text = "N/A";
            lbtnbrand_2.Text = "N/A";
            lbtnpartno_2.Text = "N/A";

        }

        private void bindItem(string qty)
        {
            int lineno = Convert.ToInt32(Session["LineNO"]);
            int _userSeqNo = 0;
            string _status = ddlStatus_2.SelectedValue;
            _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
            if (_userSeqNo == -1)
            {
                _userSeqNo = GenerateNewUserSeqNo("GRN", txtPONo.Text);
                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                _inputReptPickHeader.Tuh_doc_tp = "GRN";
                _inputReptPickHeader.Tuh_direct = true;
                _inputReptPickHeader.Tuh_ischek_itmstus = false;
                _inputReptPickHeader.Tuh_ischek_simitm = false;
                _inputReptPickHeader.Tuh_ischek_reqqty = false;
                _inputReptPickHeader.Tuh_doc_no = txtPONo.Text.ToString();
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
            }

            string _binCode = txtBincode.Text;//lblPopupBinCode.Text;
            Session["userSeqNo"] = _userSeqNo;

            decimal _actualQty = Convert.ToDecimal(qty);
            string _warrantyno = string.Empty;

            if (string.IsNullOrEmpty(Session["UNIT_PRICE"].ToString()))
            {
                Session["UNIT_PRICE"] = "0";
            }
            //Write to the Picked items serials table.
            ReptPickSerials _newReptPickSerials = new ReptPickSerials();
            #region Fill Pick Serial Object
            _newReptPickSerials.Tus_usrseq_no = _userSeqNo;
            if (lbtnselectitem.Text == "N/A")
            {
                _newReptPickSerials.Tus_doc_no = "N/A";
                _newReptPickSerials.Tus_itm_line = 0;
                _newReptPickSerials.Tus_unit_price = 0;
                _newReptPickSerials.Tus_unit_cost = 0;
                _newReptPickSerials.Tus_job_line = 0;
            }
            else
            {
                _newReptPickSerials.Tus_doc_no = txtPONo.Text;
                _newReptPickSerials.Tus_itm_line = lineno;
                _newReptPickSerials.Tus_unit_price = Convert.ToDecimal(Session["UNIT_PRICE"].ToString());
                _newReptPickSerials.Tus_unit_cost = Convert.ToDecimal(Session["UNIT_PRICE"].ToString());
                _newReptPickSerials.Tus_job_line = lineno;
            }

            _newReptPickSerials.Tus_job_no = txtPORefNo.Text;

            _newReptPickSerials.Tus_seq_no = 0;
            // _newReptPickSerials.Tus_itm_line = 0;
            _newReptPickSerials.Tus_batch_line = 0;
            _newReptPickSerials.Tus_ser_line = 0;
            _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
            _newReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
            _newReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
            _newReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
            _newReptPickSerials.Tus_bin = _binCode;
            _newReptPickSerials.Tus_itm_cd = txtitemcode_2.Text;
            _newReptPickSerials.Tus_itm_stus = _status;

            _newReptPickSerials.Tus_qty = _actualQty;//1
            _newReptPickSerials.Tus_ser_id = 0;//CHNLSVC.Inventory.GetSerialID();
            _newReptPickSerials.Tus_ser_1 = "N/A";
            _newReptPickSerials.Tus_ser_2 = "N/A";
            _newReptPickSerials.Tus_ser_3 = "N/A";
            _newReptPickSerials.Tus_warr_no = _warrantyno;
            _newReptPickSerials.Tus_itm_desc = lbtnDes_2.Text;
            _newReptPickSerials.Tus_itm_model = lbtnmodel_2.Text;
            _newReptPickSerials.Tus_itm_brand = lbtnbrand_2.Text;
            _newReptPickSerials.Tus_batch_no = txtBatchNo.Text.Trim();
            _newReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
            _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
            _newReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
            _newReptPickSerials.Tus_new_itm_cd = lbtnselectitem.Text;
            _newReptPickSerials.Tus_batch_no = txtBatchNo.Text.Trim();
            MasterItem _itmExp = CHNLSVC.General.GetItemMasterNew(_newReptPickSerials.Tus_itm_cd);
            if (_itmExp != null)
            {
                if (_itmExp.Tmp_mi_is_exp_dt == 1)
                {
                    DateTime _dtTemp = new DateTime();
                    _newReptPickSerials.Tus_exp_dt = DateTime.TryParse(txtEDate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtEDate.Text.Trim()) : DateTime.MinValue;
                    _newReptPickSerials.Tus_manufac_dt = DateTime.TryParse(txtMdate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtMdate.Text.Trim()) : DateTime.MinValue;
                }
            }
            #endregion

            DataTable _test = ViewState["po_items"] as DataTable;
            DataView dv = new DataView(_test);
            dv.RowFilter = "PODI_ITM_CD ='" + txtitemcode_2.Text + "'";
            //if (dv.Count > 0)
            //{
            if (string.IsNullOrEmpty(txtitemcode_2.Text))
            {
                return;
            }
            List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");
            var serCount = 0;
            if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
            {
                var _filter = _resultItemsSerialList.SingleOrDefault(x => x.Tus_itm_cd == txtitemcode_2.Text
                    && x.Tus_itm_stus == _status && x.Tus_itm_line == lineno&&x.Tus_unit_cost!=0);
                if (_filter != null)
                {
                    _filter.Tus_qty = Convert.ToDecimal(qty);//_filter.Tus_qty+ 
                    Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                }
                else
                {
                    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                }
            }
            else
            {
                CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);

            }

            //}
            //else
            //{

            //}
            //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");
            if (_ResultItemsSerialList != null)
            {
                grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(txtitemcode_2.Text) && x.Tus_itm_line == lineno).ToList();
                ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(txtitemcode_2.Text) && x.Tus_itm_line == lineno).ToList();
            }
            else
            {
                grdDOSerials.DataSource = null;
                ViewState["Serials"] = null;
            }

            grdDOSerials.DataBind();

            LoadPOItems(txtPONo.Text.ToString());
        }


        private void LoadPOItems_2(string _poNo)
        {
            try
            {
                DataTable po_items = new DataTable();

                if (ddlMainType.SelectedValue == "L")
                {
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), _poNo, 1);
                }
                else
                {
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), _poNo, 2);
                    foreach (DataRow drValue in po_items.Rows)
                    {
                        if (drValue["podi_bal_qty"].ToString() == "0")
                        {
                            drValue.Delete();
                        }

                    }

                    po_items.AcceptChanges();
                }
                //Get Invoice Items Details
                po_items.Columns.Add("RowNo", typeof(Int32)); LoadDefRowNo(po_items);
                if (po_items.Rows.Count > 0)
                {
                    grdDOItems.Enabled = true;

                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), _poNo, 1);
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo("GRN", txtPONo.Text);
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    // //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "GRN");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_qty, x.Tus_itm_stus }).Select(group => new { Peo = group.Key, theCount = group.Count() });

                        foreach (var itm in _scanItems)
                        {


                            foreach (DataRow row in po_items.Rows)
                            {
                                int poqty = Convert.ToInt32(row["PODI_BAL_QTY"].ToString());
                                if (lbtnselectitem.Text != "N/A")
                                {
                                    if (lbtnselectitem.Text == txtitemcode_2.Text)
                                    {
                                        if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString())
                                            && itm.Peo.Tus_itm_stus == row["POD_ITM_STUS"].ToString())
                                        {
                                            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);

                                            string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                                            if (_itemSerializedStatus == "0")
                                            {
                                                decimal qty = Convert.ToDecimal(row["GRN_QTY"].ToString());
                                                row["PODI_BAL_QTY"] = poqty - (qty + itm.Peo.Tus_qty);
                                                row["GRN_QTY"] = qty + itm.Peo.Tus_qty;//itm.theCount; // Current scan qty
                                            }


                                        }
                                    }
                                    else
                                    {
                                        if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString())
                                                && itm.Peo.Tus_itm_stus == row["POD_ITM_STUS"].ToString())
                                        {
                                            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);

                                            string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                                            if (_itemSerializedStatus == "0")
                                            {
                                                decimal qty = Convert.ToDecimal(row["GRN_QTY"].ToString());
                                                row["PODI_BAL_QTY"] = poqty - (qty + itm.Peo.Tus_qty);
                                                row["GRN_QTY"] = qty + Convert.ToDecimal(txtQty_2.Text);//itm.theCount; // Current scan qty
                                            }


                                        }
                                    }
                                }

                                else
                                {
                                    if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString())
                                                && itm.Peo.Tus_itm_stus == row["POD_ITM_STUS"].ToString())
                                    {
                                        msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);

                                        string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                                        if (_itemSerializedStatus == "0")
                                        {
                                            decimal qty = Convert.ToDecimal(row["GRN_QTY"].ToString());
                                            row["PODI_BAL_QTY"] = poqty - (qty + itm.Peo.Tus_qty);
                                            row["GRN_QTY"] = qty + itm.Peo.Tus_qty;//itm.theCount; // Current scan qty
                                        }


                                    }
                                }
                            }




                        }
                        grdDOSerials.AutoGenerateColumns = false;
                        grdDOSerials.DataSource = _serList;
                        grdDOSerials.DataBind();

                        grdDOSerials_2.AutoGenerateColumns = false;
                        grdDOSerials_2.DataSource = _serList;
                        grdDOSerials_2.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        grdDOSerials.AutoGenerateColumns = false;
                        grdDOSerials.DataSource = emptyGridList;
                        grdDOSerials.DataBind();

                        grdDOSerials_2.AutoGenerateColumns = false;
                        grdDOSerials_2.DataSource = emptyGridList;
                        grdDOSerials_2.DataBind();
                        foreach (DataRow row in po_items.Rows)
                        {
                            row["GRN_QTY"] = "0.0";
                        }
                    }



                    grdDOItems.AutoGenerateColumns = false;
                    grdDOItems.DataSource = po_items;
                    grdDOItems.DataBind();
                    grdDOItems_2.AutoGenerateColumns = false;
                    grdDOItems_2.DataSource = po_items;
                    grdDOItems_2.DataBind();

                    ViewState["po_items"] = po_items;
                    ViewState["Tempitems"] = po_items;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);

            }

        }


        protected void lbtnitemcode_2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBincode.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Bin');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtSuppCode.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Purchase Order');", true);
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                ddlSearchbykey.Items.FindByText("Description").Enabled = false;
                lblvalue.Text = "Item_2";
                UserPopoup.Show();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing.');", true);
                CHNLSVC.CloseChannel();
            }

        }

        private void CheckItemAllreadyInPo_2(string _ItemCode, DataTable _item, string line)
        {
            DataView dv = new DataView(_item);
            dv.RowFilter = "PODI_LINE_NO ='" + line + "'";
            if (dv.Count > 0)
            {
                string Des = dv[0]["MI_LONGDESC"].ToString();
                string Model = dv[0]["MI_MODEL"].ToString();
                string Brand = dv[0]["MI_BRAND"].ToString();
                Session["UNIT_PRICE"] = dv[0]["UNIT_PRICE"].ToString();
                int LineNo = Convert.ToInt32(dv[0]["PODI_LINE_NO"].ToString());
                lblDocQty.Text = dv[0]["PODI_BAL_QTY"].ToString();
                txtItemCode.Text = dv[0]["PODI_ITM_CD"].ToString();
                txtModel.Text = Model;
                txtBrand.Text = Brand;
                // txtpartNo.Text = Part;
                txtItemDes.Text = Des;
                // Session["LineNO"] = LineNo;              
                // LoadPOItems_2(txtPONo.Text);
                lblvalue.Text = "";
            }
        }

        protected void lbtnRemove_2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "No")
                {
                    return;
                }
                if (grdDOSerials_2.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string ItemCode = (row.FindControl("TUS_ITM_CD") as Label).Text;
                    if (string.IsNullOrEmpty(ItemCode)) return;

                    string _item = (row.FindControl("TUS_ITM_CD") as Label).Text; ;
                    string _status = (row.FindControl("TUS_ITM_STUS") as Label).Text; ;
                    Int32 _serialID = Convert.ToInt32((row.FindControl("TUS_SER_ID") as Label).Text);
                    string _bin = (row.FindControl("TUS_BIN") as Label).Text;
                    string serial_1 = (row.FindControl("TUS_SER_1") as Label).Text;
                    Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);

                    MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);


                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), UserSeqNo, _item, _status);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);

                    LoadPOItems_2(txtPONo.Text);
                    // RemoveTem_load(txtPONo.Text);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

        }

        private void RemoveTem_load(string _poNo)
        {
            try
            {
                DataTable po_items = new DataTable();

                po_items = ViewState["po_items"] as DataTable;

                //Get Invoice Items Details

                if (po_items.Rows.Count > 0)
                {
                    grdDOItems.Enabled = true;

                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), _poNo, 1);
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo("GRN", txtPONo.Text);
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    // //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "GRN");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_qty, x.Tus_itm_stus, x.Tus_new_itm_cd }).Select(group => new { Peo = group.Key, theCount = group.Count() });

                        foreach (var itm in _scanItems)
                        {


                            foreach (DataRow row in po_items.Rows)
                            {
                                int poqty = Convert.ToInt32(row["PODI_BAL_QTY"].ToString());
                                if (itm.Peo.Tus_itm_cd == itm.Peo.Tus_new_itm_cd)
                                {
                                    if (itm.Peo.Tus_itm_cd == row["PODI_ITM_CD"].ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row["PODI_LINE_NO"].ToString())
                                               && itm.Peo.Tus_itm_stus == row["POD_ITM_STUS"].ToString())
                                    {
                                        msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Peo.Tus_itm_cd);

                                        string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                                        if (_itemSerializedStatus == "0")
                                        {
                                            decimal qty = Convert.ToDecimal(row["GRN_QTY"].ToString());
                                            row["PODI_BAL_QTY"] = poqty + (itm.Peo.Tus_qty);
                                            row["GRN_QTY"] = itm.Peo.Tus_qty;//itm.theCount; // Current scan qty
                                        }


                                    }
                                }
                                else if (itm.Peo.Tus_new_itm_cd == "N/A")
                                {
                                    row["GRN_QTY"] = "0.0";
                                }
                                else
                                {
                                    decimal qty = Convert.ToDecimal(row["GRN_QTY"].ToString());
                                    row["PODI_BAL_QTY"] = poqty + (qty + itm.Peo.Tus_qty);
                                    row["GRN_QTY"] = qty + itm.Peo.Tus_qty;//itm.theCount; // Current scan qty



                                }
                            }

                        }
                        grdDOSerials_2.AutoGenerateColumns = false;
                        grdDOSerials_2.DataSource = _serList;
                        grdDOSerials_2.DataBind();
                    }
                    else
                    {
                        grdDOSerials_2.AutoGenerateColumns = false;
                        grdDOSerials_2.DataSource = _serList;
                        grdDOSerials_2.DataBind();
                        foreach (DataRow row in po_items.Rows)
                        {
                            row["PODI_BAL_QTY"] = row["PODI_QTY"];
                            row["GRN_QTY"] = "0.0";
                        }
                    }



                    grdDOItems.AutoGenerateColumns = false;
                    grdDOItems.DataSource = po_items;
                    grdDOItems.DataBind();
                    grdDOItems_2.AutoGenerateColumns = false;
                    grdDOItems_2.DataSource = po_items;
                    grdDOItems_2.DataBind();

                    ViewState["po_items"] = po_items;
                    ViewState["Tempitems"] = po_items;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);

            }
        }

        protected void lbtngrdDOItemstEdit_Click(object sender, EventArgs e)
        {
            try
            {
                #region add by lakshan 21Sep2017
                MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                bool _isBondLoc = false;
                if (_mstLoc != null)
                {
                    if (_mstLoc.Ml_cate_1 == "DFS")
                    {
                        _isBondLoc = true;
                    }
                }
                #endregion
                if (!_isBondLoc)
                {
                    _gridEdit = true;
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    var row = (GridViewRow)btn.NamingContainer;
                    if (row != null)
                    {
                        string _item = (row.FindControl("PODI_ITM_CD") as Label).Text; ;
                        string _line = (row.FindControl("PODI_LINE_NO") as Label).Text; ;
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                        DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + _item);
                        if (_result.Rows.Count > 0)
                        {
                            // string Code = _result.Rows[0][0].ToString();
                            DataTable _test = ViewState["po_items"] as DataTable;
                            CheckItemAllreadyInPo_2(_item, _test, _line);
                            string Des = _result.Rows[0][1].ToString();
                            string Model = _result.Rows[0][2].ToString();
                            string Brand = _result.Rows[0][3].ToString();
                            string Part = _result.Rows[0][4].ToString();
                            txtitemcode_2.Text = _item;
                            lbtnmodel_2.Text = Model;
                            lbtnbrand_2.Text = Brand;
                            lbtnpartno_2.Text = Part;
                            lbtnDes_2.Text = Des;
                            lbtnselectitem.Text = _item;
                            txtitemcode_2.Focus();
                            grdDOItems_2.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                            LoadPOItems(txtPONo.Text.ToString());
                            return;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add Item to supplier');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This functionality is disabled !');", true);
                }
                //grdDOItems_2.DataSource = (DataTable)ViewState["OrderItem"];
                //grdDOItems_2.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtngrdDOItemstUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                double qty = 0;
                double unitrate = 0;
                _gridEdit = false;
                if (!double.TryParse(((TextBox)grdDOItems_2.Rows[grdr.RowIndex].FindControl("txtGRN_QTY")).Text, out qty))
                {
                    LoadPOItems(txtPONo.Text.ToString());
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Enter valid Item Qty');", true);


                    return;
                }


                //dt.Rows[grdr.RowIndex]["IOI_BAL_QTY"] = qty;
                //dt.Rows[grdr.RowIndex]["IOI_UNIT_RT"] = unitrate;
                //dt.Rows[grdr.RowIndex]["ITEMVLUE"] = qty * unitrate;
                //if (Convert.ToInt32(dt.Rows[grdr.RowIndex]["OP_LINE"]) == 0)
                //    dt.Rows[grdr.RowIndex]["OP_LINE"] = 1;
                //grdDOItems_2.EditIndex = -1;
                //ViewState["OrderItem"] = dt;
                //grdDOItems_2.DataSource = dt;
                //grdDOItems_2.DataBind();

                bindItem(qty.ToString());

                grdDOItems_2.EditIndex = -1;
                LoadPOItems(txtPONo.Text.ToString());

            }
            catch (Exception ex)
            {

            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int value = (int)Session["print"];
        //    if (value == 1 | value == 3)
        //    {
        //        Session["GlbReportType"] = "SCM1_GRN";
        //        BaseCls.GlbReportDoc = Session["documntNo"].ToString();
        //        BaseCls.GlbReportHeading = "INWARD DOC";
        //        //BaseCls.GlbReportName = "Inward_Docs_Full_GRN.rpt";
        //        Session["GlbReportName"] = "Inward_Docs_Full_GRN.rpt";
        //        string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //        //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
        //    }
        //    else if (value == 2)
        //    {
        //        Session["GlbReportType"] = "";
        //        BaseCls.GlbReportDoc = Session["documntNo"].ToString();
        //        Session["GlbReportName"] = "serial_items.rpt";
        //        BaseCls.GlbReportHeading = "Item Serials Report";
        //        Session["GlbReportName"] = "serial_items.rpt";
        //        Session["GlbReportName"] = "serial_items.rpt";
        //        Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
        //    }          

        //}

        private void printdoc(int _doctp)
        {
            if (_doctp == 1)
            {
                try
                {
                    Session["GlbReportType"] = "SCM1_GRN";
                    //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                    //BaseCls.GlbReportHeading = "INWARD DOC";

                    if (Session["UserCompanyCode"].ToString() == "ARL")
                    {
                        Session["GlbReportName"] = "Inward_Docs_Full_GRN_ARL.rpt";
                    }
                    else if (Session["UserCompanyCode"].ToString() == "SGL")
                    {
                        Session["GlbReportName"] = "Inward_Docs_Full_GRN_SGL.rpt";
                    }
                    else if (Session["UserCompanyCode"].ToString() == "AAL")
                    {
                        Session["GlbReportName"] = "Inward_Docs_Full_GRN_AAL.rpt";
                    }
                    else if (Session["UserCompanyCode"].ToString() == "AOA")
                    {
                        Session["GlbReportName"] = "Inward_Docs_Full_GRN_AOA.rpt";
                    }
                    else if (Session["UserCompanyCode"].ToString() == "ABE")
                    {
                        Session["GlbReportName"] = "Inward_Docs_Full_GRN_ABE.rpt";
                    }
                    else
                    {
                        Session["GlbReportName"] = "Inward_Docs_Full_GRN.rpt";
                    }
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    PrintPDF(targetFileName);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    //string targetFileName = "\\Download_excel\\" + Session["UserID"].ToString() + ".pdf";
                    //PrintPDF(targetFileName);
                    //string url = "<script>window.open(('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                catch (Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("GRN Print", "Goods Received Note - GRN", ex.Message, Session["UserID"].ToString());
                }
            }
            else if (_doctp == 2)
            {
                try
                {
                    Session["GlbReportType"] = "";
                    BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                    Session["GlbReportName"] = "serial_items.rpt";
                    BaseCls.GlbReportHeading = "Item Serials Report";

                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.get_Item_Serials(Session["documntNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString());
                    PrintPDF(targetFileName, obj._serialItems);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //  Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                    //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                catch (Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("GRN Serial Print", "Goods Received Note - GRN", ex.Message, Session["UserID"].ToString());
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
            int value = (int)Session["print"];
            if (value == 1)
            {
                Session["GlbReportType"] = "SCM1_GRN";
                //BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                //BaseCls.GlbReportHeading = "INWARD DOC";
                Session["GlbReportName"] = "Inward_Docs_Full_GRN.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDF(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                //string targetFileName = "\\Download_excel\\" + Session["UserID"].ToString() + ".pdf";
                //PrintPDF(targetFileName);
                //string url = "<script>window.open(('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                PopupConfBox.Hide();
            }
            else if (value == 2)
            {
                Session["GlbReportType"] = "";
                BaseCls.GlbReportDoc = Session["documntNo"].ToString();
                Session["GlbReportName"] = "serial_items.rpt";
                BaseCls.GlbReportHeading = "Item Serials Report";

                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                clsInventory obj = new clsInventory();
                obj.get_Item_Serials(Session["documntNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString());
                PrintPDF(targetFileName, obj._serialItems);

                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //  Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                //string url = "<script>window.open('/View/Reports/Inventory/InventoryReportViewer.aspx','_blank');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

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

        public void PrintPDF(string targetFileName)
        {
            try
            {
                clsInventory obj = new clsInventory();
                obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                if (Session["GlbReportName"] == "Inward_Docs_Full_GRN_ARL.rpt")
                {
                    ReportDocument rptDoc = (ReportDocument)obj._indocfullGRN_ARL;
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                    rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    diskOpts.DiskFileName = targetFileName;
                    rptDoc.ExportOptions.DestinationOptions = diskOpts;
                    rptDoc.Export();

                    rptDoc.Close();
                    rptDoc.Dispose();
                }
                else if (Session["GlbReportName"] == "Inward_Docs_Full_GRN_SGL.rpt")
                {
                    ReportDocument rptDoc = (ReportDocument)obj._indocfullGRN_SGL;
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                    rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    diskOpts.DiskFileName = targetFileName;
                    rptDoc.ExportOptions.DestinationOptions = diskOpts;
                    rptDoc.Export();

                    rptDoc.Close();
                    rptDoc.Dispose();
                }
                else if (Session["GlbReportName"] == "Inward_Docs_Full_GRN_AAL.rpt")
                {
                    ReportDocument rptDoc = (ReportDocument)obj._indocfullGRN_AAL;
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                    rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    diskOpts.DiskFileName = targetFileName;
                    rptDoc.ExportOptions.DestinationOptions = diskOpts;
                    rptDoc.Export();

                    rptDoc.Close();
                    rptDoc.Dispose();
                }
                else if (Session["GlbReportName"] == "Inward_Docs_Full_GRN_ABE.rpt")
                {
                    ReportDocument rptDoc = (ReportDocument)obj._indocfullGRN_ABE;
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                    rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    diskOpts.DiskFileName = targetFileName;
                    rptDoc.ExportOptions.DestinationOptions = diskOpts;
                    rptDoc.Export();

                    rptDoc.Close();
                    rptDoc.Dispose();
                }
                else if (Session["GlbReportName"] == "Inward_Docs_Full_GRN_AOA.rpt")
                {
                    ReportDocument rptDoc = (ReportDocument)obj._indocfullGRN_AOA;
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                    rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    diskOpts.DiskFileName = targetFileName;
                    rptDoc.ExportOptions.DestinationOptions = diskOpts;
                    rptDoc.Export();

                    rptDoc.Close();
                    rptDoc.Dispose();
                }
                else
                {
                    ReportDocument rptDoc = (ReportDocument)obj._indocfullGRN;
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                    rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    diskOpts.DiskFileName = targetFileName;
                    rptDoc.ExportOptions.DestinationOptions = diskOpts;
                    rptDoc.Export();

                    rptDoc.Close();
                    rptDoc.Dispose();

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnUpload_Click(object sender, EventArgs e)
        {
            lbllAlert.Visible = false;
            lbllAlert.Text = "";
            lbllError.Visible = false;
            lbllError.Text = "";
            if (fileupexceluploadValidate.HasFile)
            {
                string FileName = Path.GetFileName(fileupexceluploadValidate.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexceluploadValidate.PostedFile.FileName);

                if (Extension == ".xls" || Extension == ".xlsx")
                {

                }
                else
                {
                    lbllAlert.Visible = true;
                    lbllAlert.Text = "Please select a valid excel (.xls or .xlsx) file";
                    //pnlUpload.Visible = true;

                    fileupexceluploadValidate.Dispose();
                    excelValidate.Show();
                    return;
                }

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //LoadData(FolderPath + FileName);
                string ValidateFilePath = Server.MapPath(FolderPath + FileName);
                fileupexceluploadValidate.SaveAs(ValidateFilePath);
                Session["ValidateFilePath"] = ValidateFilePath;
                lbllAlert.Visible = true;
                lbllAlert.Text = "Excel file upload completed. Do you want to process ? ";
                lbtnProcess.Visible = true;
                pnlProcess.Visible = true;
                excelValidate.Show();
                //Import_To_Grid(FilePath, Extension);
            }
            else
            {
                lbllAlert.Visible = true;
                lbllAlert.Text = "Please select the correct upload file path";
                //pnlUpload.Visible = true;
                //pnlProcess.Visible = false; 
                excelValidate.Show();
            }
        }

        protected void lbtnProcess_Click(object sender, EventArgs e)
        {
            List<InventorySerialMaster> _invSerialMst = new List<InventorySerialMaster>();
            DataTable[] GetExecelTbl = LoadValidateData(Session["ValidateFilePath"].ToString());
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0 && GetExecelTbl[0].Columns.Count > 1)
                {
                    // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        try
                        {
                            string _serial = GetExecelTbl[0].Rows[i][1].ToString();
                            string _item = GetExecelTbl[0].Rows[i][0].ToString();
                            foreach (GridViewRow row in grdDOSerials.Rows)
                            {
                                Label TUS_SER_1 = row.FindControl("TUS_SER_1") as Label;
                                if (TUS_SER_1.Text == _serial)
                                {
                                    _invSerialMst.Add(new InventorySerialMaster()
                                        {
                                            Irsm_itm_cd = _item,
                                            Irsm_ser_1 = _serial
                                        });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lbllError.Visible = true;
                            lbllError.Text = "Excel Data Invalid Please check Excel File and Upload";
                            //pnlUpload.Visible = true;
                            //pnlProcess.Visible = false;
                            excelValidate.Show();
                        }
                    }
                    lbtnProcess.Visible = false;
                    if (_invSerialMst.Count > 0)
                    {
                        //pnlUpload.Visible = true;
                        //pnlProcess.Visible = false;
                        lbllError.Visible = false;
                        excelValidate.Hide();
                        //dgvSer1Validate.DataSource = new int[] { };
                        //dgvSer1Validate.DataSource = _invSerialMst;
                        //dgvSer1Validate.DataBind();
                        popupValidateData.Show();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Excel data successfully validated !!!')", true);
                    }
                }
                else
                {
                    //pnlUpload.Visible = true;
                    //pnlProcess.Visible = false;
                    lbllError.Visible = true;
                    lbllError.Text = "Excel Data Invalid Please check Excel File and Upload";
                    excelValidate.Show();
                }
            }
            else
            {
                //pnlUpload.Visible = true;
                //pnlProcess.Visible = false;
                lbllError.Visible = true;
                lbllError.Text = "Excel Data Invalid Please check Excel File and Upload";
                excelValidate.Show();
            }
        }

        public DataTable[] LoadValidateData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();
            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dtExcelSchema;
                    cmdExcel.Connection = cn;

                    dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    cn.Close();

                    //Read Data from First Sheet
                    cn.Open();
                    cmdExcel.CommandText = "SELECT F1,F2 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Tax);
                }
                catch (Exception ex)
                {

                    lbllAlert.Visible = true;
                    lbllAlert.Text = "Invalid data found from the excel sheet. Please check data ...";
                    DispMsg(ex.Message + lbllAlert.Text, "E");
                    //pnlUpload.Visible = true;
                    //pnlProcess.Visible = false;
                    excelValidate.Show();
                    return new DataTable[] { Tax };

                }
                return new DataTable[] { Tax };
            }
        }

        protected void lbtnValidateSerial_Click(object sender, EventArgs e)
        {
            ClearPopUp();
            //if (grdDOSerials.Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add serial # ')", true);
            //    return;
            //}
            lblExcelProccesInfo.Visible = true;
            lblExcelProccesInfo.Text = "Please unselect blank cell/cells which you have selected in excel sheet";
            Session["ValidateFilePath"] = "";
            popUpUpload.Show();
        }
        protected void lbtnUploadExcel_Click(object sender, EventArgs e)
        {
            ClearPopUp();
            if (fileUploadExcel.HasFile)
            {
                string FileName = Path.GetFileName(fileUploadExcel.PostedFile.FileName);
                string Extension = Path.GetExtension(fileUploadExcel.PostedFile.FileName);

                if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX")
                {

                }
                else
                {
                    lblExcelProccesError.Visible = true;
                    lblExcelProccesError.Text = "Please select a valid excel (.xls or .xlsx) file";
                    popUpUpload.Show();
                    return;
                }

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string ValidateFilePath = Server.MapPath(FolderPath + FileName);
                fileUploadExcel.SaveAs(ValidateFilePath);
                Session["ValidateFilePath"] = ValidateFilePath;
                lblProcess.Visible = true;
                lblProcess.Text = "Excel file upload completed. Do you want to process ? ";
                //popUpUpload.Hide();
                popUpProcess.Show();
            }
            else
            {
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = "Please select the correct upload file path";
                popUpUpload.Show();
            }
        }

        protected void lbtnExcelProcess_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPopUp();
                List<InventorySerialMaster> _invSerInExcNotInGrid = new List<InventorySerialMaster>();
                List<InventorySerialMaster> _invSerInGrdNotInExc = new List<InventorySerialMaster>();
                if (Session["ValidateFilePath"] == null)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "File Path Invalid Please Upload ";
                    popUpUpload.Show();
                    return;
                }
                DataTable[] GetExecelTbl = LoadValidateData(Session["ValidateFilePath"].ToString());
                if (GetExecelTbl == null)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Excel Data Invalid Please check Excel File and Upload";
                    popUpUpload.Show();
                    return;
                }
                if (GetExecelTbl[0].Rows.Count < 1 && GetExecelTbl[0].Columns.Count < 0)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Excel Data Invalid Please check Excel File and Upload";
                    popUpUpload.Show();
                    return;
                }

                for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                {
                    bool ser1Have = false, ser2Have = false;
                    string _serial1 = GetExecelTbl[0].Rows[i][0].ToString();
                    string _serial2 = GetExecelTbl[0].Rows[i][1].ToString();
                    ExcelSerial _list = new ExcelSerial();
                    _list.serial1 = _serial1;
                    _list.serial2 = _serial2;
                    _ExcelSerial.Add(_list);


                    foreach (GridViewRow row in grdDOSerials.Rows)
                    {
                        Label TUS_SER_1 = row.FindControl("TUS_SER_1") as Label;
                        if (TUS_SER_1.Text == _serial1)
                        {
                            ser1Have = true;
                            break;
                        }
                    }
                    foreach (GridViewRow row in grdDOSerials.Rows)
                    {
                        Label TUS_SER_2 = row.FindControl("TUS_SER_2") as Label;
                        if (TUS_SER_2.Text == _serial2)
                        {
                            ser2Have = true;
                            break;
                        }
                    }
                    if (!ser1Have)
                    {
                        var v = _invSerInExcNotInGrid.Where(c => c.Irsm_ser_1 == _serial1 && c.Irsm_ser_2 == _serial2).ToList();
                        if (v != null)
                        {
                            if (v.Count == 0)
                            {
                                _invSerInExcNotInGrid.Add(new InventorySerialMaster()
                                {
                                    Irsm_ser_1 = _serial1,
                                    Irsm_ser_2 = _serial2
                                });
                            }
                        }
                    }
                    if (!ser2Have)
                    {
                        var v = _invSerInExcNotInGrid.Where(c => c.Irsm_ser_1 == _serial1 && c.Irsm_ser_2 == _serial2).ToList();
                        if (v != null)
                        {
                            if (v.Count == 0)
                            {
                                _invSerInExcNotInGrid.Add(new InventorySerialMaster()
                                {
                                    Irsm_ser_1 = _serial1,
                                    Irsm_ser_2 = _serial2
                                });
                            }
                        }
                    }
                }

                DgvSerInExcNotInGrid.DataSource = new int[] { };
                DgvSerInExcNotInGrid.DataBind();
                if (_invSerInExcNotInGrid.Count > 0)
                {
                    lblSerHed.Text = "Serials already exist in excel file and not scanned";
                    popUpProcess.Hide();
                    DgvSerInExcNotInGrid.DataSource = _invSerInExcNotInGrid;
                    DgvSerInExcNotInGrid.DataBind();
                    popupValidateData.Show();
                }
                else
                {
                    foreach (GridViewRow row in grdDOSerials.Rows)
                    {
                        Label TUS_SER_1 = row.FindControl("TUS_SER_1") as Label;
                        Label TUS_SER_2 = row.FindControl("TUS_SER_2") as Label;

                        bool F1 = GetExecelTbl[0].AsEnumerable().Any(c => TUS_SER_1.Text == c.Field<String>("F1"));
                        bool F2 = GetExecelTbl[0].AsEnumerable().Any(c => TUS_SER_2.Text == c.Field<String>("F2"));

                        if (!F1)
                        {
                            var v = _invSerInGrdNotInExc.Where(c => c.Irsm_ser_1 == TUS_SER_1.Text && c.Irsm_ser_2 == TUS_SER_2.Text).ToList();
                            if (v != null)
                            {
                                if (v.Count == 0)
                                {
                                    _invSerInGrdNotInExc.Add(new InventorySerialMaster()
                                    {
                                        Irsm_ser_1 = TUS_SER_1.Text,
                                        Irsm_ser_2 = TUS_SER_2.Text
                                    });
                                }
                            }
                        }
                        if (!F2)
                        {
                            var v = _invSerInGrdNotInExc.Where(c => c.Irsm_ser_1 == TUS_SER_1.Text && c.Irsm_ser_2 == TUS_SER_2.Text).ToList();
                            if (v != null)
                            {
                                if (v.Count == 0)
                                {
                                    _invSerInGrdNotInExc.Add(new InventorySerialMaster()
                                    {
                                        Irsm_ser_1 = TUS_SER_1.Text,
                                        Irsm_ser_2 = TUS_SER_2.Text
                                    });
                                }
                            }
                        }
                    }
                    if (_invSerInGrdNotInExc.Count > 0)
                    {
                        popUpProcess.Hide();
                        lblSerHed.Text = "Serials already scanned and not exist in excel file";
                        DgvSerInExcNotInGrid.DataSource = _invSerInGrdNotInExc;
                        DgvSerInExcNotInGrid.DataBind();
                        popupValidateData.Show();
                    }
                    else
                    {
                        popUpProcess.Hide();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Excel data successfully validated !!!')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
            }
        }

        private void ClearPopUp()
        {
            lblExcelUploadSuccess.Visible = false;
            lblExcelUploadSuccess.Text = "";
            lblExcelUploadError.Visible = false;
            lblExcelUploadError.Text = "";
            lblExcelUploadInfo.Visible = true;
            lblExcelUploadInfo.Text = "";

            lblExcelProccesSuccess.Visible = false;
            lblExcelProccesSuccess.Text = "";
            lblExcelProccesError.Visible = false;
            lblExcelProccesError.Text = "";
            lblExcelProccesInfo.Visible = false;
            lblExcelProccesInfo.Text = "";
        }

        //public DataTable[] LoadValidateDataNew(string FileName, bool hasHeader = true)
        //{
        //    using (var pck = new OfficeOpenXml.ExcelPackage())
        //    {
        //        using (var stream = File.OpenRead(FileName))
        //        {
        //            pck.Load(stream);
        //        }
        //        var ws = pck.Workbook.Worksheets.First();
        //        DataTable tbl = new DataTable();
        //        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        //        {
        //            tbl.Columns.Add(hasHeader ? firstRowCell.Value.ToString() : string.Format("Column {0}", firstRowCell.Start.Column));
        //        }
        //        var startRow = hasHeader ? 2 : 1;
        //        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        //        {
        //            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
        //            DataRow row = tbl.Rows.Add();
        //            foreach (var cell in wsRow)
        //            {
        //                row[cell.Start.Column - 1] = cell.Value.ToString();
        //            }
        //        }
        //      return   new DataTable[] { tbl };
        //    }
        //}
        protected void lbtnViewSerial_Click(object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            Label TUS_SER_ID = row.FindControl("TUS_SER_ID") as Label;
            //  lblserid.Text = "32811936";
            Int32 serId = TUS_SER_ID != null ? Convert.ToInt32(TUS_SER_ID.Text) : 0;
            if (serId == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check the serial id !!! ')", true);
                return;
            }
            dgvSubSerial.DataSource = new int[] { };
            List<InventorySubSerialMaster> _subSer = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster() { Irsms_ser_id = serId, Irsms_act = true });
            if (_subSer != null)
            {
                if (_subSer.Count > 0)
                {
                    dgvSubSerial.DataSource = _subSer;
                }
            }
            dgvSubSerial.DataBind();
            PopupDocument.Show();
        }

        protected void lbtPopDocClose_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnprint.Value == "Yes")
                {
                    string _document = Session["documntNo"].ToString();
                    if (_document != "")
                    {
                        printdoc(1);
                        //Session["print"] = 1;
                        //lblMssg.Text = "Do you want print now?";
                        //PopupConfBox.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Document');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnprintserial_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnprint.Value == "Yes")
                {
                    string _document = Session["documntNo"].ToString();
                    if (_document != "")
                    {
                        printdoc(2);
                        //Session["print"] = 2;
                        //lblMssg.Text = "Do you want print now?";
                        //PopupConfBox.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Document');", true);
                        return;
                    }


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }


        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;
            // MasterItem _itemdetail = new MasterItem();
            if (!string.IsNullOrEmpty(_item))
            {
                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            }
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;

                Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
            }
            else _isValid = false;
            return _isValid;

        }
        private void saveexcel(string DocumentType, string _scanDocument, string PopupItemCode,
            string _serialNo1, string _serialNo2, string _serialNo3, DataTable _PO, string status, decimal qty, int _userSeqNo, List<ReptPickSerials> _resultItemsSerialList, string _userwarrid, out int value)
        {
            //int _userSeqNo = 0;
            string _warrantyno = string.Empty;
            value = 0;
            if (!LoadItemDetail(PopupItemCode))
            {
                value = 2;
                return;
            }

            //_userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, Session["UserCompanyCode"].ToString(), _scanDocument, 1);
            if (_userSeqNo == -1)
            {
                _userSeqNo = GenerateNewUserSeqNo(DocumentType, _scanDocument);
            }

            if (_userSeqNo == 0) //Create a new seq no & insert it to the scmrep.temp_pick_hdr header table.  
            {
                _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 1, Session["UserCompanyCode"].ToString());

                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_direct = true;
                _inputReptPickHeader.Tuh_doc_no = _scanDocument;
                _inputReptPickHeader.Tuh_doc_tp = DocumentType;
                _inputReptPickHeader.Tuh_ischek_itmstus = false;
                _inputReptPickHeader.Tuh_ischek_reqqty = false;
                _inputReptPickHeader.Tuh_ischek_simitm = false;
                _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;

                //Save it to the scmrep.temp_pick_hdr header table. 
                Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
            }

            DataView dv = new DataView(_PO);
            dv.RowFilter = "PODI_ITM_CD ='" + PopupItemCode + "'";

            int deletedLOVcount = dv.Count;

            if (deletedLOVcount == 1)
            {
                #region Fill Pick Serial Object

                ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                _inputReptPickSerials.Tus_doc_no = _scanDocument;
                _inputReptPickSerials.Tus_seq_no = 0;
                _inputReptPickSerials.Tus_itm_line = 0;
                _inputReptPickSerials.Tus_batch_line = 0;
                _inputReptPickSerials.Tus_ser_line = 0;
                _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                _inputReptPickSerials.Tus_bin = txtBincode.Text;
                _inputReptPickSerials.Tus_itm_cd = PopupItemCode;
                _inputReptPickSerials.Tus_new_itm_cd = PopupItemCode;
                _inputReptPickSerials.Tus_itm_stus = status;
                _inputReptPickSerials.Tus_unit_cost = Convert.ToDecimal(dv[0]["UNIT_PRICE"].ToString());
                _inputReptPickSerials.Tus_unit_price = Convert.ToDecimal(dv[0]["UNIT_PRICE"].ToString());
                _inputReptPickSerials.Tus_qty = qty;
                if (_itemdetail.Mi_is_ser1 == 1)
                {
                    DataTable _dtser1 = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", PopupItemCode, _serialNo1);

                    if (_dtser1 != null)
                    {
                        if (_dtser1.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Added serial number is already exist!');", true);
                            value = 2;
                            return;
                        }
                    }
                    _dtser1.Dispose();

                    if ((CHNLSVC.Inventory.IsExistInTempPickSerial(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), PopupItemCode, _serialNo1)) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Added serial number is already exist.!');", true);
                        value = 2;
                        return;
                    }

                    if (string.IsNullOrEmpty(_serialNo1))
                    {
                        value = 2;
                        return;
                    }
                    if (string.IsNullOrEmpty(_serialNo2))
                    {
                        _serialNo2 = "N/A";
                    }
                    if (string.IsNullOrEmpty(_serialNo3))
                    {
                        _serialNo3 = "N/A";
                    }
                    _inputReptPickSerials.Tus_ser_1 = _serialNo1;
                    _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                    _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                }
                else
                {
                    _inputReptPickSerials.Tus_ser_1 = "N/A";
                    _inputReptPickSerials.Tus_ser_2 = "N/A";
                    _inputReptPickSerials.Tus_ser_3 = "N/A";
                }


                _inputReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();

                if (string.IsNullOrEmpty(_warrantyno)) _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + Session["UserDefLoca"].ToString() + _userwarrid + _inputReptPickSerials.Tus_ser_id.ToString(); //"-P01-"
                _inputReptPickSerials.Tus_warr_no = _warrantyno;
                _inputReptPickSerials.Tus_itm_desc = dv[0]["MI_LONGDESC"].ToString();
                _inputReptPickSerials.Tus_itm_model = dv[0]["MI_MODEL"].ToString();
                _inputReptPickSerials.Tus_itm_brand = dv[0]["MI_BRAND"].ToString();
                _inputReptPickSerials.Tus_itm_line = Convert.ToInt32(dv[0]["PODI_LINE_NO"].ToString());
                _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                _inputReptPickSerials.Tus_job_line = Convert.ToInt32(dv[0]["PODI_LINE_NO"].ToString());
                _inputReptPickSerials.Tus_job_no = txtPORefNo.Text;
                MasterItem _itmExp = CHNLSVC.General.GetItemMasterNew(_inputReptPickSerials.Tus_itm_cd);
                if (_itmExp != null)
                {
                    if (_itmExp.Tmp_mi_is_exp_dt == 1)
                    {
                        DateTime _dtTemp = new DateTime();
                        _inputReptPickSerials.Tus_exp_dt = DateTime.TryParse(txtEDate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtEDate.Text.Trim()) : DateTime.MinValue;
                        _inputReptPickSerials.Tus_manufac_dt = DateTime.TryParse(txtMdate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtMdate.Text.Trim()) : DateTime.MinValue;
                    }
                }
                #endregion

                //List<ReptPickSerials> _resultItemsSerialList_1 = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, DocumentType);

                var serCount = 0;
                if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                {
                    serCount = (from c in _resultItemsSerialList
                                where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == Convert.ToInt32(dv[0]["PODI_LINE_NO"].ToString())
                                select c).Count();
                }
                if (serCount <= Convert.ToDecimal(dv[0]["PODI_QTY"].ToString()))
                {
                    _excelReptPickSerials.Add(_inputReptPickSerials);
                    value = 1;
                    //lblScanQty.Text = serCount.ToString();
                    //CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, _reptPickSerialsSub);
                    //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
                    // ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;

                }
                else
                {

                }

            }
            else
            {
                #region Fill Pick Serial Object

                ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                _inputReptPickSerials.Tus_doc_no = _scanDocument;
                _inputReptPickSerials.Tus_seq_no = 0;
                _inputReptPickSerials.Tus_itm_line = 0;
                _inputReptPickSerials.Tus_batch_line = 0;
                _inputReptPickSerials.Tus_ser_line = 0;
                _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                _inputReptPickSerials.Tus_bin = txtBincode.Text;
                _inputReptPickSerials.Tus_itm_cd = PopupItemCode;
                // _inputReptPickSerials.Tus_new_itm_cd = PopupItemCode;
                _inputReptPickSerials.Tus_itm_stus = status;
                _inputReptPickSerials.Tus_unit_cost = 0;
                _inputReptPickSerials.Tus_unit_price = 0;
                _inputReptPickSerials.Tus_qty = qty;
                if (_itemdetail.Mi_is_ser1 == 1)
                {
                    if (string.IsNullOrEmpty(_serialNo1))
                    {
                        value = 2;
                        return;
                    }
                    if (string.IsNullOrEmpty(_serialNo2))
                    {
                        _serialNo2 = "N/A";
                    }
                    if (string.IsNullOrEmpty(_serialNo3))
                    {
                        _serialNo3 = "N/A";
                    }
                    _inputReptPickSerials.Tus_ser_1 = _serialNo1;
                    _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                    _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                }
                else
                {
                    _inputReptPickSerials.Tus_ser_1 = "N/A";
                    _inputReptPickSerials.Tus_ser_2 = "N/A";
                    _inputReptPickSerials.Tus_ser_3 = "N/A";
                }


                _inputReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();

                if (string.IsNullOrEmpty(_warrantyno)) _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + Session["UserDefLoca"].ToString() + _userwarrid + _inputReptPickSerials.Tus_ser_id.ToString(); //"-P01-"
                _inputReptPickSerials.Tus_warr_no = _warrantyno;
                _inputReptPickSerials.Tus_itm_desc = _itemdetail.Mi_longdesc;
                _inputReptPickSerials.Tus_itm_model = _itemdetail.Mi_model;
                _inputReptPickSerials.Tus_itm_brand = _itemdetail.Mi_brand;
                _inputReptPickSerials.Tus_itm_line = 0;
                _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                _inputReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
                _inputReptPickSerials.Tus_job_line = 0;
                _inputReptPickSerials.Tus_job_no = txtPORefNo.Text;

                #endregion

                _excelMisMatchReptPickSerials.Add(_inputReptPickSerials);

                value = 1;
            }

        }

        protected void lbtngrdRevenuEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    grdExcelItem.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                    var groupedCustomerList = _excelMisMatchReptPickSerials.GroupBy(x => x.Tus_itm_cd).Select(y => y.First());
                    grdExcelItem.DataSource = groupedCustomerList;
                    grdExcelItem.DataBind();
                    popupexcelitem.Show();
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtngrdRevenueUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    string _item = (row.FindControl("TUS_ITM_CD") as Label).Text;
                    string baseitem = (row.FindControl("txttus_new_itm_cd") as TextBox).Text;
                    if (_excelMisMatchReptPickSerials.Count > 0)
                    {
                        grdExcelItem.EditIndex = -1;
                        foreach (var item in _excelMisMatchReptPickSerials.Where(x => x.Tus_itm_cd == _item))
                        {
                            item.Tus_new_itm_cd = baseitem;
                        }


                        var groupedCustomerList = _excelMisMatchReptPickSerials.GroupBy(x => x.Tus_itm_cd).Select(y => y.First());
                        grdExcelItem.DataSource = groupedCustomerList;
                        grdExcelItem.DataBind();

                        popupexcelitem.Show();
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtnbaseItem_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                var row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    string _item = (row.FindControl("TUS_ITM_CD") as Label).Text;
                    Session["selectitem"] = _item;
                    DataTable _result = ViewState["po_items"] as DataTable;
                    grdpo.DataSource = _result;
                    grdpo.DataBind();
                    lblvalue.Text = "baseItem";
                    POpopup.Show();
                    popupexcelitem.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);
                // ErrorMsgGRN("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
        }


        protected void btnexcelsavebaseItem_Click(object sender, EventArgs e)
        {
            if (_excelMisMatchReptPickSerials != null)
            {
                int value = 0;
                // string seq = _excelMisMatchReptPickSerials[0].Tus_usrseq_no.ToString();
                // List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                // reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(seq), "GRN");

                //List<ReptPickSerials> difference = _excelMisMatchReptPickSerials.Except(reptPickSerialsList).ToList();
                value = CHNLSVC.Inventory.UPDATEPICKSERIAL_BASEITM(_excelMisMatchReptPickSerials);
                // value = CHNLSVC.Inventory.SaveAllScanSerialsList(_excelMisMatchReptPickSerials, _reptPickSerialsSub);
                if (value > 0)
                {
                    _checkbaseitem = true;
                    POpopup.Hide();
                    string _msg = "Successfully Pick excel sheet ";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _msg + "');", true);
                    LoadPOItems(txtPONo.Text.ToString());

                }

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


        protected void lbtngrdDOItemstEdit_1_Click(object sender, EventArgs e)
        {
            try
            {
                #region add by lakshan 21Sep2017
                MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                bool _isBondLoc = false;
                if (_mstLoc != null)
                {
                    if (_mstLoc.Ml_cate_1 == "DFS")
                    {
                        _isBondLoc = true;
                    }
                }
                #endregion
                if (!_isBondLoc)
                {
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    Label PODI_ITM_CD = grdr.FindControl("PODI_ITM_CD") as Label;
                    Label PODI_LINE_NO = grdr.FindControl("PODI_LINE_NO") as Label;
                    //_gridEdit = true;

                    //_itmPick = true;
                    //_itmPickItemCode = PODI_LINE_NO.Text;
                    //_itmPickLine =Convert.ToInt32(PODI_LINE_NO.Text);

                    DataTable dt = _dtTable;
                    if (dt.Rows.Count > 0)
                    {
                        DataView dv = dt.DefaultView;
                        dv.Sort = "RowNo DESC";
                        dt = dv.ToTable();
                    }
                    _dtTable = dt;
                    var row = (GridViewRow)btn.NamingContainer;
                    if (row != null)
                    {
                        grdDOItems.EditIndex = grdr.RowIndex;
                        grdDOItems.DataSource = dt;
                        grdDOItems.DataBind();
                        //LoadPOItems(txtPONo.Text.ToString());
                    }
                    //grdDOItems_2.DataSource = (DataTable)ViewState["OrderItem"];
                    //grdDOItems_2.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This functionality is disabled !');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lbtngrdDOItemstUpdate_1_Click(object sender, EventArgs e)
        {
            try
            {
                _gridEdit = false;
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                decimal qty = 0;
                decimal unitrate = 0;

                if (!decimal.TryParse(((TextBox)grdDOItems.Rows[grdr.RowIndex].FindControl("txtPickQty")).Text, out qty))
                {
                    LoadPOItems(txtPONo.Text.ToString());
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Enter valid Item Qty');", true);


                    return;
                }

                string _itm = (grdr.FindControl("PODI_ITM_CD") as Label).Text;
                Label UNIT_PRICE = grdr.FindControl("UNIT_PRICE") as Label;
                decimal _unitCost = Convert.ToDecimal(UNIT_PRICE.Text);
                string _status = (grdr.FindControl("POD_ITM_STUS") as Label).Text;
                string _rqty = (grdr.FindControl("PODI_BAL_QTY") as Label).Text;
                Int32 itmLine = Convert.ToInt32((grdr.FindControl("PODI_LINE_NO") as Label).Text);
                //dt.Rows[grdr.RowIndex]["IOI_BAL_QTY"] = qty;
                //dt.Rows[grdr.RowIndex]["IOI_UNIT_RT"] = unitrate;
                //dt.Rows[grdr.RowIndex]["ITEMVLUE"] = qty * unitrate;
                //if (Convert.ToInt32(dt.Rows[grdr.RowIndex]["OP_LINE"]) == 0)
                //    dt.Rows[grdr.RowIndex]["OP_LINE"] = 1;
                //grdDOItems_2.EditIndex = -1;
                //ViewState["OrderItem"] = dt;
                //grdDOItems_2.DataSource = dt;
                //grdDOItems_2.DataBind();
                decimal _Remaingqty = Convert.ToDecimal(_rqty);
                if (_Remaingqty < qty)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Enter valid Item Qty');", true);
                    return;
                }
                msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm);
                if (msitem != null)
                {
                    if (msitem.Mi_is_ser1 == 0)
                    {
                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");
                        var serCount = 0;
                        if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                        {
                            //Edited By Dulaj 2018/Dec/19
                            //Add Unit Cost to the filter to igonre zero cost 
                            var _filter = _resultItemsSerialList.SingleOrDefault(x => x.Tus_itm_cd == _itm && x.Tus_itm_stus == _status&&x.Tus_unit_cost>0);
                            if (_filter != null)
                            {
                                _filter.Tus_qty = Convert.ToDecimal(qty);
                                Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                            }

                        }

                    }
                    if (msitem.Mi_is_ser1 == -1 && Session["UserCompanyCode"].ToString() == "ARL")
                    {
                        bindItemNew(qty.ToString(), _itm, _unitCost, itmLine);
                    }
                }

                _itmPick = true;
                _itmPickItemCode = _itm;
                _itmPickLine = itmLine;
                grdDOItems.EditIndex = -1;
                LoadPOItems(txtPONo.Text.ToString());
                DataTable dt = _dtTable;
            }
            catch (Exception ex)
            {

            }
        }

        private DataTable LoadDefRowNo(DataTable _dt)
        {
            if (chkPickTop.Checked)
            {

                if (!_itmPick)
                {
                    int numberOfRecords = _dt.AsEnumerable().Where(x => x["GRN_QTY"].ToString() != "0").ToList().Count;
                    foreach (DataRow dr in _dt.Rows)
                    {
                        if (dr["GRN_QTY"].ToString() == "0")
                        {
                            dr["RowNo"] = 0;
                        }
                        else
                        {
                            dr["RowNo"] = numberOfRecords; numberOfRecords--;
                        }

                    }
                }
                else
                {
                    int numberOfRecords = _dt.AsEnumerable().Where(x => x["GRN_QTY"].ToString() != "0").ToList().Count + 1;
                    foreach (DataRow dr in _dt.Rows)
                    {
                        if (dr["PODI_ITM_CD"].ToString() == _itmPickItemCode && Convert.ToInt32(dr["PODI_LINE_NO"].ToString()) == _itmPickLine)
                        {
                            dr["RowNo"] = numberOfRecords; numberOfRecords--;
                            break;
                        }
                    }
                    foreach (DataRow dr in _dt.Rows)
                    {
                        if (dr["PODI_ITM_CD"].ToString() != _itmPickItemCode && Convert.ToInt32(dr["PODI_LINE_NO"].ToString()) != _itmPickLine)
                        {
                            if (dr["GRN_QTY"].ToString() == "0")
                            {
                                dr["RowNo"] = 0;
                            }
                            else
                            {
                                dr["RowNo"] = numberOfRecords; numberOfRecords--;
                            }
                        }
                    }
                }
                if (chkPickTop.Checked)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        DataView dv = _dt.DefaultView;
                        dv.Sort = "RowNo DESC";
                        _dt = dv.ToTable();
                    }
                }
            }
            return _dt;
        }

        protected void chkPickTop_CheckedChanged(object sender, EventArgs e)
        {
            //if (grdDOItems.DataSource!=null)
            //{
            // _dtTable =(DataTable) grdDOItems.DataSource;
            grdDOItems.DataSource = LoadDefRowNo(_dtTable);
            grdDOItems.DataBind();
            //}
        }
        protected void chkqr_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxQR.Checked)
            {
                lblcomQR.Visible = false;
                DropDownListQRCom.Visible = false;
            }
            else
            {

                lblcomQR.Visible = true;
                DropDownListQRCom.Visible = true;
            }
        }
        private void clearQR()
        {
            CheckBoxQR.Checked = false;
            DropDownListQRCom.Visible = false;
        }
        private void CheckCom()
        {
            if (Session["UserCompanyCode"].ToString().Equals("AAL"))
            {
                CheckBoxQR.Checked = false;
                CheckBoxQR.Enabled = true;
            }
            else { CheckBoxQR.Checked = false; CheckBoxQR.Enabled = false; }
            lblcomQR.Visible = false;
            DropDownListQRCom.Visible = false;
        }
        public string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
        private void printWarrentyNew()
        {
            try
            {
                if (chkwaranty.Checked)
                {
                    //Session["GlbReportName"] = "WarrPrint_abl.rpt";
                    //_invRepPara._GlbReportName = "WarrPrint_abl.rpt";
                    //DataTable _warrPrint = CHNLSVC.Inventory.getWarrantyPrintMobDetails(_objRepPara._GlbReportComp, _objRepPara._GlbReportDoc, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportMainSerial, 1);
                    //obj._warrprint.Database.Tables["WARR_PRINT"].SetDataSource(_warrPrint);

                    //ReportDocument rptDoc = (ReportDocument)obj._warraPrint;

                    WarrPrint_abl _warraPrint = new WarrPrint_abl();
                    ReportDocument rptDoc = new ReportDocument();
                    DataTable MST_COM = new DataTable("com");
                    DataTable param = new DataTable("param");
                    DataRow dr;
                    MST_COM = CHNLSVC.General.GetCompanyByCode("ABL");

                    param.Columns.Add("user", typeof(string));

                    param.Rows.Add(Session["UserID"].ToString());
                    DataTable tmp_Table = new DataTable("scm_warranty_movemnet");
                    tmp_Table = CHNLSVC.Inventory.getWarrantyPrintMobDetails(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), txtItemCode.Text.ToString(), txtSerial1.Text.ToString(), 1);

                    rptDoc.Load(ReportPath + "\\Inventory\\WarrPrint_abl.rpt");
                    rptDoc.Database.Tables["WARR_PRINT"].SetDataSource(tmp_Table);

                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, targetFileName);
                    rptDoc.Close();
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //RepWarrantyCard_AOA2 _warraPrint = new RepWarrantyCard_AOA2();
                    //ReportDocument rptDoc = new ReportDocument();
                    //DataTable MST_COM = new DataTable("com");
                    //DataTable purchaseOrderSumm = new DataTable("orderSum");
                    //DataTable param = new DataTable("param");
                    //DataRow dr;
                    //MST_COM = CHNLSVC.General.GetCompanyByCode("ABL");

                    //param.Columns.Add("user", typeof(string));

                    //param.Rows.Add(Session["UserID"].ToString());
                    //DataTable tmp_Table = new DataTable("scm_warranty_movemnet");
                    //tmp_Table = CHNLSVC.MsgPortal.get_WaraPrint_Main(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), txtItemCode.Text.ToString(), txtSerial1.Text.ToString());
                    //_warraPrint.Database.Tables["scm_warranty_movemnet"].SetDataSource(tmp_Table);
                    //_warraPrint.Database.Tables["param"].SetDataSource(param);
                    //DataTable tmp_Table1 = new DataTable("tbl");
                    //tmp_Table1 = CHNLSVC.MsgPortal.get_WaraPrint_Sub(Session["UserCompanyCode"].ToString(), _userSeqNo.ToString(), txtItemCode.Text.ToString(), txtSerial1.Text.ToString());


                    //foreach (object repOp in _warraPrint.ReportDefinition.ReportObjects)
                    //{
                    //    string _s = repOp.GetType().ToString();
                    //    if (_s.ToLower().Contains("subreport"))
                    //    {
                    //        SubreportObject _cs = (SubreportObject)repOp;
                    //        if (_cs.SubreportName == "rptSubSerial")
                    //        {
                    //            ReportDocument subRepDoc = _warraPrint.Subreports[_cs.SubreportName];
                    //            subRepDoc.Database.Tables["scm_warranty_movemnet_sub"].SetDataSource(tmp_Table1);
                    //            subRepDoc.Close();
                    //            subRepDoc.Dispose();
                    //        }
                    //        _cs.Dispose();
                    //    }
                    //}
                    //rptDoc.Load(ReportPath + "\\Inventory\\RepWarrantyCard_AOA2.rpt");
                    //string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    //_warraPrint.ExportToDisk(ExportFormatType.PortableDocFormat, targetFileName);
                    //_warraPrint.Close();
                    //rptDoc.Close();
                    //string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("GRN Warr Print - PDF", "Goods Received Note - GRN", ex.Message, Session["UserID"].ToString());
                throw ex;
            }
        }
        private void printwarranty()
        {
            if (chkwaranty.Checked)
            {
                InvReportPara _invRepPara = new InvReportPara();

                Session["GlbReportDoc"] = _userSeqNo;
                Session["GlbReportType"] = "";
                Session["GlbReportName"] = "RepWarrantyCard_AOA.rpt";
                _invRepPara._GlbReportName = "RepWarrantyCard_AOA.rpt";
                _invRepPara._GlbReportDoc = Session["GlbReportDoc"].ToString();// Session["userSeqNo"].ToString();
                _invRepPara._GlbReportItemCode = txtItemCode.Text;//main itm LGMU-KP105                        
                _invRepPara._GlbReportMainSerial = txtSerial1.Text;//PO2
                _invRepPara._GlbReportComp = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbReportHeading = "Warranty Print";
                Session["InvReportPara"] = _invRepPara;

                clsInventory obj = new clsInventory();
                obj.Print_AOA_Warra(_invRepPara);

                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDFWarr(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


            }
        }

        public class ExcelSerial
        {
            public string serial1 { get; set; }
            public string serial2 { get; set; }
        }

        protected void btnSentScan_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 val = 0;
                DataTable po_items = new DataTable();
                // po_items = ViewState["po_items"] as DataTable;
                //  if(po_items==null){
                if (ddlMainType.SelectedValue == "L")
                {
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
                }
                else
                {
                    po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtPONo.Text, 2);
                    foreach (DataRow drValue in po_items.Rows)
                    {
                        if (drValue["podi_bal_qty"].ToString() == "0")
                        {
                            drValue.Delete();
                        }

                    }

                    po_items.AcceptChanges();
                }
                ReptPickHeader _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA_PARTIAL(new ReptPickHeader()
                {
                    Tuh_doc_no = txtPONo.Text.Trim(),//invHdr.Ith_oth_docno,
                    Tuh_doc_tp = "GRN",
                    Tuh_direct = false,
                    Tuh_usr_com = Session["UserCompanyCode"].ToString()
                }).FirstOrDefault();
                if (_tmpPickHdr != null)
                {
                    if (_tmpPickHdr != null)
                    {
                        List<ReptPickItems> _repItmListPart = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA_PARTIAL(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                        if (_repItmListPart != null)
                        {
                            if (_repItmListPart.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                                return;
                            }
                        }
                    }
                }
                Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo("GRN", txtPONo.Text);
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "GRN";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtPONo.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    // _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    // _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                    _inputReptPickHeader.Is_doc_Partial_save = true;
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
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

                    _inputReptPickHeader.Tuh_doc_no = txtPONo.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = "GRN";
                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    //_inputReptPickHeader.Tuh_wh_com = warehousecom;
                    //_inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    _inputReptPickHeader.Is_doc_Partial_save = true;
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
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
                    if (po_items.Rows.Count > 0)
                    {
                        //06/Apr/2016 comment by Rukshan (chamal request) 

                        List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                        foreach (DataRow _row in po_items.Rows)
                        {
                            string _item = _row["PODI_ITM_CD"].ToString();
                            string _cost = _row["UNIT_PRICE"].ToString();
                            string _qty = _row["PODI_QTY"].ToString();
                            string _line = _row["PODI_LINE_NO"].ToString();
                            string _STATUS = _row["POD_ITM_STUS"].ToString();
                            //AddItem(_item, _cost, null, null, user_seq_num.ToString(), null);

                            ReptPickItems _reptitm = new ReptPickItems();
                            _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                            _reptitm.Tui_req_itm_qty = Convert.ToDecimal(_qty);
                            _reptitm.Tui_req_itm_cd = _item;
                            _reptitm.Tui_req_itm_stus = _STATUS;
                            _reptitm.Tui_pic_itm_cd = _line;
                            // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                            _reptitm.Tui_pic_itm_qty = 0;
                            _reptitm.ls_par_save = true;
                            _saveonly.Add(_reptitm);


                        }
                        val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                    }

                }
                if (val == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                    MPPDA.Hide();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
        }

        protected void lbtnDo_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtPONo.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select PO number')", true);
                return;
            }
            txtDO.Text = txtDO.Text.Trim();
            if (!string.IsNullOrEmpty(txtDO.Text))
            {
                int val = 0;
                InventoryHeader _hdr = new InventoryHeader();
                _hdr = CHNLSVC.Inventory.GetINTHDRByDocnO(Session["UserCompanyCode"].ToString(), "DO", txtDO.Text);
                if (_hdr != null)
                {
                    List<ReptPickSerials> _serial = new List<ReptPickSerials>();

                    _serial = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");
                    if (_serial != null)
                    {
                        if (_serial.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial has been already picked !')", true);

                            return;

                        }
                    }
                    _serial = CHNLSVC.Inventory.Get_Int_Ser(txtDO.Text);
                    if (_serial.Count > 0)
                    {
                        DataTable po_items = new DataTable();
                        if (ddlMainType.SelectedValue == "L")
                        {
                            po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
                        }
                        else
                        {
                            po_items = CHNLSVC.Inventory.GetPOItemsDataTable(Session["UserCompanyCode"].ToString(), txtPONo.Text, 2);
                            foreach (DataRow drValue in po_items.Rows)
                            {
                                if (drValue["podi_bal_qty"].ToString() == "0")
                                {
                                    drValue.Delete();
                                }
                            }
                            po_items.AcceptChanges();
                        }

                        _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
                        if (_userSeqNo == -1)
                        {
                            _userSeqNo = GenerateNewUserSeqNo("GRN", txtdocname.Text);
                            ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                            _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                            _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                            _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                            _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                            _inputReptPickHeader.Tuh_doc_tp = "GRN";
                            _inputReptPickHeader.Tuh_direct = true;
                            _inputReptPickHeader.Tuh_ischek_itmstus = false;
                            _inputReptPickHeader.Tuh_ischek_simitm = false;
                            _inputReptPickHeader.Tuh_ischek_reqqty = false;
                            _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                            _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();

                            val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                            if (val == -1)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Tempory save fail')", true);
                                CHNLSVC.CloseChannel();
                                return;
                            }
                        }

                        if (_serial.Count > 0)
                        {
                            // if (checkItem(po_items,_serial))
                            //{
                            _serial.ForEach(s => s.Tus_usrseq_no = _userSeqNo);
                            foreach (DataRow drValue in po_items.Rows)
                            {
                                string _itm = drValue["PODI_ITM_CD"].ToString();
                                string _status = drValue["POD_ITM_STUS"].ToString();
                                Int32 itmLine = Convert.ToInt32(drValue["PODI_LINE_NO"].ToString());
                                var _filter = _serial.FindAll(x => x.Tus_itm_cd == _itm && x.Tus_itm_stus == _status);
                                if (_filter != null)
                                {
                                    foreach (ReptPickSerials _seri in _filter)
                                    {
                                        // if (_itm == _seri.Tus_itm_cd)
                                        // {
                                        _seri.Tus_base_doc_no = txtPONo.Text;
                                        _seri.Tus_base_itm_line = itmLine;
                                        _seri.Tus_itm_line = itmLine;
                                        _seri.Tus_new_itm_cd = _seri.Tus_itm_cd;
                                        _seri.Tus_loc = Session["UserDefLoca"].ToString();
                                        _seri.Tus_doc_no = txtPONo.Text;
                                        // }
                                    }
                                }

                            }
                            List<ReptPickSerials> _Newserial = new List<ReptPickSerials>();
                            List<ReptPickSerials> _POserial = new List<ReptPickSerials>();

                            if (_chkallItemDo == false)
                            {
                                _Newserial = _serial.Where(x => x.Tus_base_doc_no != txtPONo.Text).ToList();
                                if (_Newserial.Count > 0)
                                {
                                    // var query = _Newserial.Distinct(p => p.);
                                    grdDOItem.DataSource = _Newserial;
                                    grdDOItem.DataBind();
                                    MDPDOConf.Show();
                                    return;
                                }
                            }
                            if (_getallItemDo == true)
                            {
                                foreach (DataRow drValue in po_items.Rows)
                                {
                                    string _itm = drValue["PODI_ITM_CD"].ToString();
                                    string _status = drValue["POD_ITM_STUS"].ToString();
                                    Int32 itmLine = Convert.ToInt32(drValue["PODI_LINE_NO"].ToString());
                                    //var _filter = _serial.FindAll(x => x.Tus_itm_cd == _itm && x.Tus_itm_stus == _status);
                                    if (_serial != null)
                                    {
                                        foreach (ReptPickSerials _seri in _serial)
                                        {
                                            if (_itm == _seri.Tus_itm_cd)
                                            {
                                                _seri.Tus_base_doc_no = txtPONo.Text;
                                                _seri.Tus_base_itm_line = itmLine;
                                                _seri.Tus_itm_line = itmLine;
                                                _seri.Tus_new_itm_cd = _seri.Tus_itm_cd;
                                                _seri.Tus_loc = Session["UserDefLoca"].ToString();
                                                _seri.Tus_doc_no = txtPONo.Text;
                                                _seri.Tus_bin = Session["GlbDefaultBin"].ToString();
                                                _seri.Tus_com = Session["UserCompanyCode"].ToString();
                                            }
                                            else
                                            {
                                                // _seri.Tus_base_doc_no = txtPONo.Text;
                                                //_seri.Tus_base_itm_line = itmLine;
                                                _seri.Tus_itm_line = 0;
                                                // _seri.Tus_new_itm_cd = _seri.Tus_itm_cd;
                                                _seri.Tus_loc = Session["UserDefLoca"].ToString();
                                                _seri.Tus_doc_no = txtPONo.Text;

                                            }

                                        }
                                    }

                                }
                                val = CHNLSVC.Inventory.SaveAllScanSerialsList(_serial, null);
                            }
                            else
                            {
                                _serial = _serial.Where(x => x.Tus_base_doc_no == txtPONo.Text).ToList();
                                val = CHNLSVC.Inventory.SaveAllScanSerialsList(_serial, null);
                                if (val == 0)
                                {
                                    val = 2;
                                }
                            }


                            // }
                        }


                        if (val > 0)
                        {
                            if (val == 2)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('DO does not contain the selected items !')", true);
                                // return;
                            }
                            _getallItemDo = false;
                            _chkallItemDo = false;
                            LoadPOItems(txtPONo.Text.ToString());
                        }
                        else
                        {
                            _getallItemDo = false;
                            _chkallItemDo = false;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Tempory save fail')", true);
                            CHNLSVC.CloseChannel();
                            return;
                        }
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Enter valid DO number')", true);
                    return;
                }
            }
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            _getallItemDo = true;
            _chkallItemDo = true;
            lbtnDo_Click(null, null);
        }
        protected void btnno_Click(object sender, EventArgs e)
        {
            _getallItemDo = false;
            _chkallItemDo = true;
            lbtnDo_Click(null, null);
        }
        //private bool checkItem(DataTable _tbl, List<ReptPickSerials> _serial)
        //{
        //    var tableIds = _tbl.Rows.Cast<DataRow>().Select(row => row["PODI_ITM_CD"].ToString());
        //    _serial.Except(tableIds).ToList();
        //}

        protected void btnokexceedqty_Click(object sender, EventArgs e)
        {
            _chkPOQty = true;
            Process(false);
        }

        protected void btnnOexceedqty_Click(object sender, EventArgs e)
        {
            _chkPOQty = false;

        }
        private void bindItemNew(string qty, string _itmCd, decimal _unitCst, Int32 itmLine)
        {
            int lineno = Convert.ToInt32(Session["LineNO"]);
            int _userSeqNo = 0;
            string _status = ddlStatus_2.SelectedValue;
            _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", Session["UserCompanyCode"].ToString(), txtPONo.Text, 1);
            if (_userSeqNo == -1)
            {
                _userSeqNo = GenerateNewUserSeqNo("GRN", txtPONo.Text);
                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                _inputReptPickHeader.Tuh_doc_tp = "GRN";
                _inputReptPickHeader.Tuh_direct = true;
                _inputReptPickHeader.Tuh_ischek_itmstus = false;
                _inputReptPickHeader.Tuh_ischek_simitm = false;
                _inputReptPickHeader.Tuh_ischek_reqqty = false;
                _inputReptPickHeader.Tuh_doc_no = txtPONo.Text.ToString();
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
            }

            string _binCode = txtBincode.Text;//lblPopupBinCode.Text;
            Session["userSeqNo"] = _userSeqNo;

            decimal _actualQty = Convert.ToDecimal(qty);
            string _warrantyno = string.Empty;

            if (string.IsNullOrEmpty(Session["UNIT_PRICE"].ToString()))
            {
                Session["UNIT_PRICE"] = "0";
            }
            //Write to the Picked items serials table.
            ReptPickSerials _newReptPickSerials = new ReptPickSerials();
            #region Fill Pick Serial Object
            _newReptPickSerials.Tus_usrseq_no = _userSeqNo;
            if (lbtnselectitem.Text == "N/A")
            {
                _newReptPickSerials.Tus_doc_no = txtPONo.Text;
                _newReptPickSerials.Tus_itm_line = itmLine;
                _newReptPickSerials.Tus_unit_price = _unitCst;
                _newReptPickSerials.Tus_unit_cost = _unitCst;
                _newReptPickSerials.Tus_job_line = 0;
            }
            else
            {
                _newReptPickSerials.Tus_doc_no = txtPONo.Text;
                _newReptPickSerials.Tus_itm_line = lineno;
                _newReptPickSerials.Tus_unit_price = _unitCst;
                _newReptPickSerials.Tus_unit_cost = _unitCst;
                _newReptPickSerials.Tus_job_line = lineno;
            }

            _newReptPickSerials.Tus_job_no = txtPORefNo.Text;

            _newReptPickSerials.Tus_seq_no = 0;
            // _newReptPickSerials.Tus_itm_line = 0;
            _newReptPickSerials.Tus_batch_line = 0;
            _newReptPickSerials.Tus_ser_line = 0;
            _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
            _newReptPickSerials.Tus_orig_grndt = DateTime.Now.Date;
            _newReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
            _newReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
            _newReptPickSerials.Tus_bin = _binCode;
            _newReptPickSerials.Tus_itm_cd = _itmCd;// txtitemcode_2.Text;
            _newReptPickSerials.Tus_itm_stus = _status;

            _newReptPickSerials.Tus_qty = _actualQty;//1
            _newReptPickSerials.Tus_ser_id = 0;//CHNLSVC.Inventory.GetSerialID();
            _newReptPickSerials.Tus_ser_1 = "N/A";
            _newReptPickSerials.Tus_ser_2 = "N/A";
            _newReptPickSerials.Tus_ser_3 = "N/A";
            _newReptPickSerials.Tus_warr_no = _warrantyno;
            _newReptPickSerials.Tus_itm_desc = lbtnDes_2.Text;
            _newReptPickSerials.Tus_itm_model = lbtnmodel_2.Text;
            _newReptPickSerials.Tus_itm_brand = lbtnbrand_2.Text;
            _newReptPickSerials.Tus_batch_no = txtBatchNo.Text.Trim();
            _newReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
            _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
            _newReptPickSerials.Tus_session_id = Session["SessionID"].ToString();
            _newReptPickSerials.Tus_new_itm_cd = _newReptPickSerials.Tus_itm_cd;
            _newReptPickSerials.Tus_batch_no = txtBatchNo.Text.Trim();
            MasterItem _itmExp = CHNLSVC.General.GetItemMasterNew(_newReptPickSerials.Tus_itm_cd);
            if (_itmExp != null)
            {
                _newReptPickSerials.Tus_itm_desc = _itmExp.Mi_shortdesc;
                _newReptPickSerials.Tus_itm_model = _itmExp.Mi_model;
                _newReptPickSerials.Tus_itm_brand = _itmExp.Mi_brand;
                if (_itmExp.Tmp_mi_is_exp_dt == 1)
                {
                    DateTime _dtTemp = new DateTime();
                    _newReptPickSerials.Tus_exp_dt = DateTime.TryParse(txtEDate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtEDate.Text.Trim()) : DateTime.MinValue;
                    _newReptPickSerials.Tus_manufac_dt = DateTime.TryParse(txtMdate.Text.Trim(), out _dtTemp) ? Convert.ToDateTime(txtMdate.Text.Trim()) : DateTime.MinValue;
                }
            }
            #endregion

            DataTable _test = ViewState["po_items"] as DataTable;
            DataView dv = new DataView(_test);
            dv.RowFilter = "PODI_ITM_CD ='" + _itmCd + "'";
            //if (dv.Count > 0)
            //{
            if (string.IsNullOrEmpty(_itmCd))
            {
                return;
            }
            List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");
            var serCount = 0;
            if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
            {
                var _filter = _resultItemsSerialList.SingleOrDefault(x => x.Tus_itm_cd == _itmCd
                    && x.Tus_itm_stus == _status && x.Tus_itm_line == lineno&&x.Tus_unit_cost!=0);
                if (_filter != null)
                {
                    _filter.Tus_qty = Convert.ToDecimal(qty);//_filter.Tus_qty+ 
                    Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                }
                else
                {
                    CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
                }
            }
            else
            {
                CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, _reptPickSerialsSub);
            }

            //}
            //else
            //{

            //}
            //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsListNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");
            if (_ResultItemsSerialList != null)
            {
                grdDOSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_itmCd) && x.Tus_itm_line == lineno).ToList();
                ViewState["Serials"] = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_itmCd) && x.Tus_itm_line == lineno).ToList();
            }
            else
            {
                grdDOSerials.DataSource = null;
                ViewState["Serials"] = null;
            }

            grdDOSerials.DataBind();

            LoadPOItems(txtPONo.Text.ToString());
        }

        protected void btnlocOK_Click(object sender, EventArgs e)
        {
            Process(false);
        }

        protected void btnlocNO_Click(object sender, EventArgs e)
        {

            if (ddlMainType.SelectedValue == "L")
            {
                string _msg = "Cannot GRN .Please check your location !";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                return;
            }
            return;
        }

        protected void btnQtyExcOk_Click(object sender, EventArgs e)
        {
            _chkPOQty = true;
            Process(false);
        }

        protected void btnQtyExcNo_Click(object sender, EventArgs e)
        {
            _chkPOQty = false;
        }

        protected void grdCheckIItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdCheckIItem.PageIndex = e.NewPageIndex;
                DataTable _dt = ViewState["po_items"] as DataTable;
                if (_dt != null)
                {
                    grdCheckIItem.DataSource = _dt;
                }
                else
                {
                    grdCheckIItem.DataSource = new int[] { };
                }
                grdCheckIItem.DataBind();
                UserCheckItem.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
    }
    public class ExcelValidation
    {
        public int RowNo { get; set; }
        public string Code { get; set; }
        public string Error { get; set; }

    }
    public class DecimalItemBin
    {
        //public int RowNo { get; set; }
        public string ItmCode { get; set; }
        public string Bin { get; set; }

        }
    public class ItemDecimal
    {        
        public string ItmCode { get; set; }
        public string Serial { get; set; }

    }
}