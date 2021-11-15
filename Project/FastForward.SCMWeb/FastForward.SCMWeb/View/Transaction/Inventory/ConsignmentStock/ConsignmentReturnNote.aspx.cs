using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory.Consignment_Stock
{
    public partial class ConsignmentReturnNote : BasePage
    {
        List<TempPopup> _popSerialList { 
            get 
            {
                if (Session["_popSerialList"] == null)
                {
                    return new List<TempPopup>();
                }
                else
                {
                    return (List<TempPopup>)Session["_popSerialList"];
                }
            }
            set
            {
                Session["_popSerialList"] = value;
            }
        }
       bool _popSerSearch
        {
            get
            {
                if (Session["_popSerSearch"] == null)
                {
                    return false;
                }
                else
                {
                    return (bool)Session["_popSerSearch"];
                }
            }
            set
            {
                Session["_popSerSearch"] = value;
            }
        }
        #region Varialbe
        private string _addTp
        {
            get { if (Session["_addTp"] != null) { return (string)Session["_addTp"]; } else { return ""; } }
            set { Session["_addTp"] = value; }
        }
        private List<InventoryRequestItem> ScanItemList = null;
        private string _receCompany = string.Empty;
        private string RequestNo = string.Empty;
        private string SelectedStatus = string.Empty;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        private List<ReptPickSerials> serial_list = null;
        private List<ReptPickSerials> SelectedSerialList = null;
        private string _chargeType = string.Empty;
        private List<string> SeqNumList = null;
        List<int> successItems = new List<int>();
        string _userid = string.Empty;
        List<int> successItemsPDA = new List<int>();
        public bool isConsReturnNote
        {
            get { if (ViewState["isConsReturnNote"] == null) return false; return Convert.ToBoolean(ViewState["isConsReturnNote"]); }
            set { ViewState["isConsReturnNote"] = value; }
        }
        #endregion

        #region Rooting for Form Initializing
        class DocumentType
        {
            string _displayMemener = string.Empty;
            string _valueMemeber = string.Empty;

            public string DisplayMemener
            {
                get { return _displayMemener; }
                set { _displayMemener = value; }
            }
            public string ValueMemeber
            {
                get { return _valueMemeber; }
                set { _valueMemeber = value; }
            }
        }
        private void BindUserCompanyItemStatusDDLData(DropDownList ddl)
        {
            try
            {
                DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString()); ;
                var _s = (from L in _tbl.AsEnumerable()
                          select new
                          {
                              MIS_DESC = L.Field<string>("MIS_DESC"),
                              MIC_CD = L.Field<string>("MIC_CD")
                          }).ToList();
                ddl.DataSource = _s;
                ddl.DataTextField = "MIS_DESC";
                ddl.DataValueField = "MIC_CD";
                ddl.DataBind();

                ddl.Items.Insert(0, new ListItem("Select", "0"));
                ddl.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region Rooting for Check Adjustment Sub Type
        private bool IsValidAdjustmentSubType()
        {
            bool status = false;
            try
            {
                txtAdjSubType.Text = txtAdjSubType.Text.Trim().ToUpper().ToString();
                DataTable _adjSubType = CHNLSVC.Inventory.GetMoveSubTypeAllTable("ADJ", txtAdjSubType.Text.ToString());
                if (_adjSubType.Rows.Count > 0)
                {
                    lblSubTypeDesc.Text = _adjSubType.Rows[0]["mmct_sdesc"].ToString();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            return status;
        }

        #endregion

        #region Rooting for Load Items/Serials
        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {
                    _direction = 1;
                }

                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "CRN", Session["UserID"].ToString(), _direction, _seqNo);
                if (user_seq_num == -1)
                {
                     user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "CRN", Session["UserID"].ToString(), _direction, txtDocumentNo.Text);
                }
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo();
                    txtUserSeqNo.Text = user_seq_num.ToString();
                }

                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                int line=0;
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    line++;
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_line_no = line;
                    _itm.Itri_qty = _reptitem.Tui_pic_itm_qty;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(0);
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "CRN");
                #region Cmt
                //if (_serList != null)
                //{
                //    if (_direction == 0)
                //    {
                //        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                //        foreach (var itm in _scanItems)
                //        {
                //            foreach (GridViewRow row in this.grdItems.Rows)
                //            {
                //                Label itemlabel = (Label)row.FindControl("lblitri_itm_cd");
                //                Label itemline = (Label)row.FindControl("lblitri_line_no");
                //                Label itempicqty = (Label)row.FindControl("lblitri_qty");

                //                if (itm.Peo.Tus_itm_cd == itemlabel.Text && itm.Peo.Tus_base_itm_line == Convert.ToInt32(itemline.Text))
                //                {
                //                    //itempicqty.Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_itm_stus, x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                //        foreach (var itm in _scanItems)
                //        {
                //            foreach (InventoryRequestItem dr in ScanItemList)
                //            {
                //                if ((itm.Peo.Tus_itm_cd == dr.Itri_itm_cd) && (itm.Peo.Tus_itm_stus == dr.Itri_itm_stus))
                //                {
                //                    if (itm.Peo.Tus_qty > 1)
                //                    {
                //                        dr.Itri_bqty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                //                      //  dr.Itri_app_qty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                //                    }
                //                    else
                //                    {
                //                        dr.Itri_bqty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                //                        //dr.Itri_app_qty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                //                    }


                //                }
                //                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                //                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                //                if (oItemStaus != null && oItemStaus.Count > 0)
                //                {
                //                    dr.Mis_desc = oItemStaus.Find(x => x.Mis_cd == itm.Peo.Tus_itm_stus).Mis_desc;
                //                }
                //            }
                //        }
                //    }
                //    grdSerial.AutoGenerateColumns = false;
                //    grdSerial.DataSource = _serList;
                //    grdSerial.DataBind();
                //}
                //else
                //{
                //    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                //    grdSerial.AutoGenerateColumns = false;
                //    grdSerial.DataSource = emptyGridList;
                //    grdSerial.DataBind();
                //}
                //ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                #endregion
                if (_serList != null)
                {
                    //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_itm_stus, x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line, x.Tus_itm_stus }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        foreach (InventoryRequestItem dr in ScanItemList)
                        {
                            if ((itm.Peo.Tus_itm_cd == dr.Itri_itm_cd) && (itm.Peo.Tus_itm_stus == dr.Itri_itm_stus))
                            {
                                dr.Itri_bqty = itm.theCount;
                            }
                        }
                    }
                    //foreach (var itm in _scanItems)
                    //{
                    //    foreach (InventoryRequestItem dr in ScanItemList)
                    //    {
                    //        if ((itm.Peo.Tus_itm_cd == dr.Itri_itm_cd) && (itm.Peo.Tus_itm_stus == dr.Itri_itm_stus))
                    //        {
                    //            if (itm.Peo.Tus_qty > 1)
                    //            {
                    //                dr.Itri_bqty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty   
                    //                if (dr.Itri_bqty > dr.Itri_app_qty)
                    //                {
                    //                    dr.Itri_app_qty = itm.theCount;
                    //                }
                    //               // dr.Itri_app_qty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                    //            }
                    //            else
                    //            {
                    //                dr.Itri_bqty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty   
                    //                if (dr.Itri_bqty>dr.Itri_app_qty)
                    //                {
                    //                    dr.Itri_app_qty = itm.theCount;
                    //                }
                    //                //itm.Peo.Tus_qty; // Current scan qty    
                    //            }


                    //        }
                    //        List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                    //        oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    //        if (oItemStaus != null && oItemStaus.Count > 0)
                    //        {
                    //            dr.Mis_desc = oItemStaus.Find(x => x.Mis_cd == itm.Peo.Tus_itm_stus).Mis_desc;
                    //        }
                    //    }
                    //}
                }
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                ViewState["ScanItemList"] = ScanItemList;
                if (_serList == null)
                {
                    grdSerial.DataSource = new int[] { };
                    grdSerial.DataBind();
                   // grdItems.DataSource = new int[] { };
                   // grdItems.DataBind();
                    return;
                }
                List<MasterItemStatus> oItemStaus2 = new List<MasterItemStatus>();
                oItemStaus2 = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                foreach (ReptPickSerials itemSer in _serList)
                {
                    if (oItemStaus2 != null && oItemStaus2.Count > 0)
                    {
                        itemSer.Tus_itm_stus_Desc = oItemStaus2.Find(x => x.Mis_cd == itemSer.Tus_itm_stus).Mis_desc;
                    }
                }

                grdSerial.DataSource = _serList;
                grdSerial.DataBind();
                ViewState["SerialList"] = _serList;
                Session["SERIALLIST"] = _serList;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo()
        {
            try
            {
                Int32 generated_seq = 0;
                Int16 _direction = 0;
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {
                    _direction = 1;
                }
                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "CRN", _direction, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
                ReptPickHeader RPH = new ReptPickHeader();
                RPH.Tuh_doc_tp = "CRN";
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
                RPH.Tuh_doc_no =string.IsNullOrEmpty(txtDocumentNo.Text)?generated_seq.ToString():txtDocumentNo.Text.Trim();
                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                if (affected_rows == 1)
                {
                    txtUserSeqNo.Text = generated_seq.ToString();
                    return generated_seq;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return 0;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    _popSerSearch = false;
                    _popSerialList = new List<TempPopup>();
                    ucOutScan.isConsReturnNote = true;
                    Session["SCAN_ITEM_LIST"] =new List<InventoryRequestItem>(); 
                    DateTime date = DateTime.Now;
                    dtpDate.Text = date.ToString("dd/MMM/yyyy");

                    DateTime fromdt = DateTime.Now.AddMonths(-1);
                    txtfromdate.Text = fromdt.ToString("dd/MMM/yyyy");

                    DateTime todate = DateTime.Now;
                    txttodate.Text = todate.ToString("dd/MMM/yyyy");

                    //--Need to think when calling
                    SelectedSerialList = new List<ReptPickSerials>();
                    ScanItemList = new List<InventoryRequestItem>();
                    _itemdetail = new MasterItem();
                    serial_list = new List<ReptPickSerials>();
                    //--

                    grdItems.DataSource = new int[] { };
                    grdItems.DataBind();

                    //gvBalance.DataSource = new int[] { };
                    //gvBalance.DataBind();

                    grdSerial.DataSource = new int[] { };
                    grdSerial.DataBind();

                    gvsearchdata.DataSource = new int[] { };
                    gvsearchdata.DataBind();

                    ddlAdjTypeSearch.SelectedIndex = 0;

                    BindUserCompanyItemStatusDDLData(ddlStatus);

                    ddlAdjType.SelectedItem.Text = "ADJ-";
                    txtAdjSubType.Text = "CONSIGN";
                    ddlStatus.SelectedValue = "CONS";

                    ddlAdjTypeSearch.Text = "ADJ-";
                    RequestNo = string.Empty;

                    bool _allowCurrentTrans = false;
                    IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_ConsStock_StockRtn", dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                    ddlAdjType_SelectedIndexChanged(null, null);

                    ucOutScan.adjustmentTypeValue = "-";
                    ucOutScan.doc_tp = "ADJ";
                    ucOutScan.isApprovalSend = false;
                    ucOutScan.PNLTobechange.Visible = false;

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

                    PopulateLoadingBays();

                    DataTable dtpda = CHNLSVC.Inventory.CheckIsPDALoc(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (dtpda.Rows.Count > 0)
                    {
                        foreach (DataRow ddrpda in dtpda.Rows)
                        {
                            Session["PDA_LOCATION"] = ddrpda["ML_IS_PDA"].ToString();
                            if (ddrpda["ML_IS_PDA"].ToString() == "1")
                            {
                                chkpda.Enabled = true;
                            }
                            else
                            {
                                chkpda.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    if (_popSerSearch)
                    {
                        UserAdPopup.Show();
                    }
                    else
                    {
                        UserAdPopup.Hide();
                        _popSerSearch = false;
                    }
                    string popup = (string)Session["POPUP_CRN_RETURN"];
                    if (!string.IsNullOrEmpty(popup))
                    {
                        MpDelivery.Show();
                    }

                    string sessionselecteditm = (string)Session["SELECTED_ITEM"];
                    string sessionselecteditm_qty = (string)Session["SELECTED_ITEM_QTY"];

                    if ((!string.IsNullOrEmpty(sessionselecteditm)) && (!string.IsNullOrEmpty(sessionselecteditm_qty)))
                    {
                        foreach (GridViewRow myrow in grdItems.Rows)
                        {
                            Decimal pickqty = 0;
                            Label lblitri_qty = (Label)myrow.FindControl("lblitri_qty");
                            Label lblitri_itm_cd = (Label)myrow.FindControl("lblitri_itm_cd");

                            if (lblitri_itm_cd.Text == sessionselecteditm)
                            {
                                pickqty = Convert.ToDecimal(lblitri_qty.Text.Trim());
                                pickqty = pickqty + Convert.ToDecimal(sessionselecteditm_qty);
                                lblitri_qty.Text = pickqty.ToString(); 
                            } 
                        }

                        foreach (GridViewRow myrow1 in grdSerial.Rows)
                        {
                            Label lblsup = (Label)myrow1.FindControl("lblsup");
                            lblsup.Text = txtSupplierCd.Text.Trim();
                        }


                        Session["SELECTED_ITEM"] = null;
                        Session["SELECTED_ITEM_QTY"] = null;

                        LoadStatusDescriptionItem();
                        LoadStatusDescriptionSerial();
                    }
                    else
                    {
                        Session["SELECTED_ITEM"] = null;
                        Session["SELECTED_ITEM_QTY"] = null;
                    }

                    string docsession = (string)Session["Doc"];
                    string tempdocsession = (string)Session["TempDoc"];

                    if (!string.IsNullOrEmpty(docsession))
                    {
                        if (Session["Doc"].ToString() == "true")
                        {
                            UserDPopoup.Show();
                            MpDelivery.Hide();
                        }
                    }

                    if (!string.IsNullOrEmpty(tempdocsession))
                    {
                        if (Session["TempDoc"].ToString() == "true")
                        {
                            UserDPopoup.Show();
                            MpDelivery.Hide();
                        }
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

        protected void txtAdjSubType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblSubTypeDesc.Text = string.Empty;

                if (string.IsNullOrEmpty(txtAdjSubType.Text)) return;
                if (IsValidAdjustmentSubType() == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid adjustment sub type !!!')", true);
                    lblSubTypeDesc.Text = string.Empty;
                    txtAdjSubType.Text = string.Empty;
                    txtAdjSubType.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtUnitCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUnitCost.Text))
                {
                    if (!IsNumeric(txtUnitCost.Text.Trim(), NumberStyles.Float))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid unit cost. Please enter the valid amount !!!')", true);
                        txtUnitCost.Text = string.Empty;
                        txtUnitCost.Focus();
                        return;
                    }
                }
                else
                {
                    if (ddlAdjType.SelectedItem.ToString() == "ADJ-")
                    {
                        txtUnitCost.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void ddlAdjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlAdjType.Enabled = false;
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {
                    SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                }
                else
                {
                    SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(Session["UserID"].ToString(), "ADJ", 0, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                }
                SeqNumList = SeqNumList.Where(c=> c.Substring(0)!="").OrderByDescending(p => p.Substring(0)).ToList();
                while (ddlSeqNo.Items.Count>1)
                {
                    ddlSeqNo.Items.RemoveAt(1);
                }
                ddlSeqNo.DataSource = SeqNumList;
                ddlSeqNo.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSupplierCd.Enabled = true;
                if (!string.IsNullOrEmpty(ddlSeqNo.Text))
                {
                    txtUserSeqNo.Text = ddlSeqNo.Text;
                    Int32 seqNo = 0;
                    if (Int32.TryParse(ddlSeqNo.Text, out seqNo))
                    {
                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(),
                         Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), seqNo, "ADJ");
                        lblBatchNo.Text = "";
                        if (_resultItemsSerialList != null)
                        {
                            if (_resultItemsSerialList != null)
                            {
                                if (_resultItemsSerialList.Count > 0)
                                {
                                    lblBatchNo.Text = _resultItemsSerialList[0].Tus_doc_no;
                                }
                            }
                        }
                    }
                
                    LoadItems(txtUserSeqNo.Text);

                    if (grdSerial.Rows.Count > 0)
                    {
                        foreach (GridViewRow myrow in grdSerial.Rows)
                        {
                            Label supplierlabel = (Label)myrow.FindControl("lblsup");
                            string supplier = supplierlabel.Text;

                            txtSupplierCd.Text = supplier;
                            if (txtSupplierCd.Text.Trim() != "")
                            {
                                txtSupplierCd.Enabled = false;
                                btnSearchSupp.Enabled = false;
                                LoadStatusDescriptionItem();
                                LoadStatusDescriptionSerial();
                                return;
                            }
                        }
                    }
                }
                LoadStatusDescriptionItem();
                LoadStatusDescriptionSerial();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(_receCompany + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItem.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("ADJ" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        if (ddlAdjTypeSearch.Text == "ADJ+")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "ADJ" + seperator + "1" + seperator);
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "ADJ" + seperator + "0" + seperator);
                        }

                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtSupplierCd.Text.ToUpper() + seperator + Session["itemstatus"].ToString()+ seperator+"Serial");
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "123")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                    DataTable result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "123";
                    ViewState["SEARCH"] = result;
                    txtSearchbyword.Text = "";
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "3";
                    ViewState["SEARCH"] = result;
                    txtSearchbyword.Text = "";

                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "16")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "16";
                    ViewState["SEARCH"] = result;
                    txtSearchbyword.Text = "";
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "16a")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "16a";
                    ViewState["SEARCH"] = result;
                    txtSearchbyword.Text = "";
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);
                    string searchParameter = ddlSearchbykey.Text;

                    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                    result = dv.ToTable();

                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    txtSearchbyword.Text = "";
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch 
            {
                
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                txtSearchbyword.Text = "";
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void ClearItemDetail()
        {
            txtItem.Text = string.Empty;
            txtUnitCost.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtfindreq.Text = string.Empty;
            txtSupplierCd.ReadOnly = false;
            txtSupplierCd.ToolTip = string.Empty;
            //gvBalance.DataSource = null;
            //gvBalance.AutoGenerateColumns = false;
            //gvBalance.DataSource = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, string.Empty);
            //gvBalance.DataBind();
            successItems.Clear();
            Session["ENTERED_DOC"] = null;
        }

        private void ClearItemDetail2()
        {
            txtItem.Text = string.Empty;
            txtUnitCost.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtfindreq.Text = string.Empty;
            txtSupplierCd.ReadOnly = true;
            txtSupplierCd.ToolTip = string.Empty;
            successItems.Clear();
            Session["ENTERED_DOC"] = null;
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "123")
                {
                    txtAdjSubType.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAdjSubType_TextChanged(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                if (lblvalue.Text == "3")
                {
                    ClearItemDetail();
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    CheckItem();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                if (lblvalue.Text == "16")
                {
                    txtSupplierCd.ToolTip = string.Empty;
                    txtSupplierCd.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSupplierCd_TextChanged(null, null);
                    txtSupplierCd.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                //if (lblvalue.Text == "16a")
                //{
                //    txtfindsup.ToolTip = string.Empty;
                //    txtfindsup.Text = grdResult.SelectedRow.Cells[1].Text;
                //    txtfindsup.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                //}
                ViewState["SEARCH"] = null;
            }
            catch 
            {
                
            }
        }

        protected void CheckItem()
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text)) return;

                if (string.IsNullOrEmpty(ddlAdjType.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the adjustment type !!!')", true);
                    txtItem.Text = string.Empty;
                    ddlAdjType.Focus();
                    return;
                }

                if (LoadItemDetail(txtItem.Text.Trim()) == false)
                {
                    txtItem.Focus();
                    return;
                }

                txtItem.Text = txtItem.Text.Trim();

                List<ReptPickSerials> Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text, string.Empty, string.Empty);

                if (Tempserial_list.Count <= 0)
                {
                    txtItem.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Item is not available.');", true);
                    return;
                }

                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());//STATUS="CONS"
                if (ddlAdjType.SelectedItem.ToString() == "ADJ-")
                {
                    if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No stock balance available !!!')", true);
                        txtItem.Text = "";
                        ddlStatus.DataSource = new List<InventoryLocation>();
                        txtItem.Focus();
                        return;
                    }
                    else
                    {
                        ddlStatus.DataSource = null;
                        ddlStatus.DataSource = _inventoryLocation;
                        ddlStatus.DataTextField = "mis_desc";
                        ddlStatus.DataValueField = "inl_itm_stus";
                        ddlStatus.DataBind();
                    }
                }

                //gvBalance.DataSource = null;
                //gvBalance.AutoGenerateColumns = false;
                //gvBalance.DataSource = _inventoryLocation;
                //gvBalance.DataBind();
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

        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }


        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date = ddr["Date"].ToString();
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                _result.Clear();
                _result = dtresultcopyMRN;

                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                Session["Doc"] = "true";
                UserDPopoup.Show();
            }
            else if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                

                
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
            }
            else
            {
                ddlAdjTypeSearch.SelectedItem.Text = "ADJ+";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                ddlAdjTypeSearch.SelectedItem.Text = "ADJ-";
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                DataView dv = new DataView(_result);

                dv.RowFilter = "Status <> 'C'";

                _result = dv.ToTable();

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date =Convert.ToDateTime(ddr["Date"].ToString()).Date.ToString("dd/MMM/yyyy");
                   // string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                _result.Clear();
                _result = dtresultcopyMRN;

                grdResultD.DataSource = null;
                grdResult.DataBind();

                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                UserDPopoup.Show();
            }
        }

        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date = ddr["Date"].ToString();
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                _result.Clear();
                _result = dtresultcopyMRN;

                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
            }
            else if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
            }
            else
            {
                ddlAdjTypeSearch.SelectedItem.Text = "ADJ+";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                ddlAdjTypeSearch.SelectedItem.Text = "ADJ-";
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;

                DataView dv = new DataView(_result);

                dv.RowFilter = "Status <> 'C'";

                _result = dv.ToTable();

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });
                DateTime dt = DateTime.Today;
                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date = DateTime.TryParse(ddr["Date"].ToString(), out dt) ? Convert.ToDateTime(ddr["Date"].ToString()).Date.ToString("dd/MMM/yyyy") : dt.ToString("dd/MMM/yyyy");
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                _result.Clear();
                _result = dtresultcopyMRN;

                grdResultD.DataSource = null;
                grdResult.DataBind();

                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                UserDPopoup.Show();
            }
        }

        private void GetTempDocData(string DocNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {

                    #region Clear Data

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = _emptyItm;
                    grdItems.DataBind();

                    lbtnsave.Enabled = true;
                    lbtnsave.OnClientClick = "SaveConfirm();";
                    lbtnsave.CssClass = "buttonUndocolor";

                    lbtntempsave.Enabled = false;
                    lbtntempsave.OnClientClick = "return Enable();";
                    lbtntempsave.CssClass = "buttoncolor";

                    #endregion

                    #region Get Serials
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser_Temp(txtDocumentNo.Text);

                    if (_serList != null)
                    {
                        DataTable dttemp2 = new DataTable();
                        dttemp2.Columns.AddRange(new DataColumn[11] { new DataColumn("itri_line_no"), new DataColumn("itri_itm_cd"), new DataColumn("mi_longdesc"), new DataColumn("mi_model"), new DataColumn("itri_itm_stus"), new DataColumn("itri_unit_price"), 
                            new DataColumn("itri_app_qty"), new DataColumn("itri_qty"), new DataColumn("itri_note"), new DataColumn("Mis_desc"), new DataColumn("Itri_bqty")});

                        List<InventoryBatchN> _itmlist = CHNLSVC.Inventory.Get_Int_Batch_Temp(txtDocumentNo.Text.Trim());

                        if (_serList.Count == 0)
                        {
                            foreach (InventoryBatchN binditem2 in _itmlist)
                            {
                                dttemp2.Rows.Add(binditem2.Inb_itm_line, binditem2.Inb_itm_cd, "", "", binditem2.Inb_itm_stus, binditem2.Inb_unit_cost, binditem2.Inb_qty, binditem2.Inb_qty, "", "CONSIGNMENT", binditem2.Inb_qty);
                                txtUserSeqNo.Text = binditem2.Inb_seq_no.ToString();
                                if (!string.IsNullOrEmpty(txtUserSeqNo.Text))
                                {
                                    Int32 SeqNo = 0;
                                    if (Int32.TryParse(txtUserSeqNo.Text, out  SeqNo))
                                    {
                                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(),
                                            Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), SeqNo, "ADJ");
                                        lblBatchNo.Text = "";
                                        if (_resultItemsSerialList != null)
                                        {
                                            if (_resultItemsSerialList.Count > 0)
                                            {
                                                lblBatchNo.Text = _resultItemsSerialList[0].Tus_doc_no;
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (InventoryBatchN binditem3 in _itmlist)
                            {
                                ReptPickSerials _serial = new ReptPickSerials();
                                MasterItem _itms = new MasterItem();
                                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), binditem3.Inb_itm_cd);
                                _serial.Tus_qty = binditem3.Inb_qty;
                                _serial.Tus_com = binditem3.Inb_com;
                                _serial.Tus_doc_dt = binditem3.Inb_doc_dt;
                                _serial.Tus_itm_cd = binditem3.Inb_itm_cd;
                                _serial.Tus_itm_stus = binditem3.Inb_itm_stus;
                                _serial.Tus_loc = binditem3.Inb_loc;
                                _serial.Tus_unit_price = binditem3.Inb_unit_price;
                                _serial.Tus_bin = binditem3.Inb_bin;
                                _serial.Tus_itm_desc = _itms.Mi_shortdesc;
                                _serial.Tus_itm_model = _itms.Mi_model;
                                _serial.Tus_itm_brand = _itms.Mi_brand;
                                _serial.Tus_doc_no = binditem3.Inb_doc_no;
                                _serial.Tus_ser_1 = "N/A";
                                _serial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                                _serList.Add(_serial);
                            }
                        }
                        else
                        {
                            foreach (InventoryBatchN binditem in _itmlist)
                            {
                                lblBatchNo.Text = "";
                                foreach (ReptPickSerials seritem in _serList)
                                {
                                    if ((binditem.Inb_itm_cd == seritem.Tus_itm_cd) && (binditem.Inb_itm_line == seritem.Tus_itm_line))
                                    {
                                        dttemp2.Rows.Add(seritem.Tus_itm_line, seritem.Tus_itm_cd, seritem.Tus_itm_desc, seritem.Tus_itm_model, seritem.Tus_itm_stus, seritem.Tus_unit_cost, seritem.Tus_qty, seritem.Tus_qty, "", "CONSIGNMENT", seritem.Tus_qty);

                                        txtUserSeqNo.Text = binditem.Inb_seq_no.ToString();
                                        if (!string.IsNullOrEmpty(txtUserSeqNo.Text))
                                        {
                                            Int32 SeqNo = 0;
                                            if (Int32.TryParse(txtUserSeqNo.Text, out  SeqNo))
                                            {
                                                List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(),
                                                    Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), SeqNo, "ADJ");
                                                lblBatchNo.Text = "";
                                                if (_resultItemsSerialList != null)
                                                {
                                                    if (_resultItemsSerialList.Count > 0)
                                                    {
                                                        lblBatchNo.Text = _resultItemsSerialList[0].Tus_doc_no;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        grdItems.DataSource = null;
                        grdItems.DataSource = dttemp2;
                        grdItems.DataBind();


                        foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                        {
                            Label lblapqtygrd = (Label)hiderowbtn.FindControl("lblapqtygrd");
                            Label lblitri_qty = (Label)hiderowbtn.FindControl("lblitri_qty");

                            lblapqtygrd.Text = DoFormat(Convert.ToDecimal(lblapqtygrd.Text.Trim()));
                            lblitri_qty.Text = DoFormat(Convert.ToDecimal(lblitri_qty.Text.Trim()));
                        }

                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _serList;
                        grdSerial.DataBind();
                    }
                    else
                    {
                        txtfindreq.Text = "";
                        txtfindreq.Focus();
                        return;
                    }
                    #endregion

                    Session["SERIALLIST"] = _serList;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }


        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }

       

        private void LoadHeader(bool istemp)
        {
            try
            {
                DataTable dtheader = new DataTable();
                txtSupplierCd.ToolTip = string.Empty;
                if (istemp == true)
                {
                    dtheader = CHNLSVC.Inventory.GetTempDocHeaderData(txtDocumentNo.Text.Trim());
                }
                else
                {
                    dtheader = CHNLSVC.Inventory.GetDocHeaderData(txtDocumentNo.Text.Trim());
                }

                if (dtheader.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid document no !!!')", true);
                    txtDocumentNo.Text = string.Empty;
                    return;
                }

                foreach (DataRow ddrdoc in dtheader.Rows)
                {
                    Session["LOADEDSEQNO"] = ddrdoc["ith_seq_no"].ToString();
                    txtSupplierCd.Text = ddrdoc["ith_bus_entity"].ToString();
                    Session["STATUS"] = ddrdoc["ith_stus"].ToString();
                    txtManualRef.Text = ddrdoc["ith_manual_ref"].ToString();
                    txtSupplierCd.ToolTip = ddrdoc["mbe_name"].ToString();
                    txtOtherRef.Text = ddrdoc["ith_entry_no"].ToString();
                    txtRemarks.Text = ddrdoc["ith_remarks"].ToString();
                }

                string reqstatus = (string)Session["STATUS"];

                if (reqstatus == "C")
                {
                    lbtncancel.Enabled = false;
                    lbtncancel.CssClass = "buttoncolor";
                    lbtncancel.OnClientClick = "return Enable();";
                }
                else
                {
                    lbtncancel.Enabled = true;
                    lbtncancel.CssClass = "buttonUndocolor";
                    lbtncancel.OnClientClick = "ConfirmCancel();";
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date=Convert.ToDateTime(ddr["Date"].ToString()).Date.ToString("dd/MMM/yyyy"); 
                    //string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                _result.Clear();
                _result = dtresultcopyMRN;
                
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "Doc";
                txtFDate.Text = Convert.ToDateTime(txtFDate.Text).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtTDate.Text).Date.ToShortDateString();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
            }
            else if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "TempDoc";
                txtFDate.Text = Convert.ToDateTime(txtFDate.Text).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtTDate.Text).Date.ToShortDateString();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
            }
            else
            {
                ddlAdjTypeSearch.SelectedItem.Text = "ADJ+";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                ddlAdjTypeSearch.SelectedItem.Text = "ADJ-";
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;

                DataView dv = new DataView(_result);

                dv.RowFilter = "Status <> 'C'";

                _result = dv.ToTable();

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in _result.Rows)
                {
                    DateTime dt = DateTime.Today;
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                    string date = DateTime.TryParse(ddr["Date"].ToString(), out dt) ? Convert.ToDateTime(ddr["Date"].ToString()).Date.ToString("dd/MMM/yyyy") : dt.ToString("dd/MMM/yyyy");
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                _result.Clear();
                _result = dtresultcopyMRN;

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                txtFDate.Text = Convert.ToDateTime(txtFDate.Text).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtTDate.Text).Date.ToShortDateString();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
            }
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));

            DataTable dtresultcopyMRN = new DataTable();
            dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

            foreach (DataRow ddr in _result.Rows)
            {
                string doc = ddr["Document"].ToString();
                string subtp = ddr["Sub Type"].ToString();
                string date = Convert.ToDateTime(ddr["Date"].ToString()).Date.ToString("dd/MMM/yyyy"); 
                //string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                string manref = ddr["Manual Ref No"].ToString();
                string otherdoc = ddr["Other Document"].ToString();
                string entry = ddr["Entry No"].ToString();
                string job = ddr["Job No"].ToString();
                string otherloc = ddr["Other Loc"].ToString();
                string stus = ddr["Status"].ToString();

                dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
            }

            _result.Clear();
            _result = dtresultcopyMRN;

            grdResultD.DataSource = _result;
            grdResultD.DataBind();
            UserDPopoup.Show();
        }

        protected void btnSearch_DocumentNo_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;

                grdResultD.DataSource = null;
                grdResultD.DataBind();

                if (chktemp.Checked == true)
                {
                    _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now));
                    lblvalue.Text = "TempDoc";
                    Session["TempDoc"] = "true";
                }
                else
                {
                    _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now));
                    lblvalue.Text = "Doc";
                    Session["Doc"] = "true";

                    DataTable dtresultcopyMRN = new DataTable();
                    dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                    foreach (DataRow ddr in _result.Rows)
                    {
                        string doc = ddr["Document"].ToString();
                        string subtp = ddr["Sub Type"].ToString();
                        string date = Convert.ToDateTime(ddr["Date"].ToString()).Date.ToString("dd/MMM/yyyy"); 
                        string manref = ddr["Manual Ref No"].ToString();
                        string otherdoc = ddr["Other Document"].ToString();
                        string entry = ddr["Entry No"].ToString();
                        string job = ddr["Job No"].ToString();
                        string otherloc = ddr["Other Loc"].ToString();
                        string stus = ddr["Status"].ToString();

                        dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                    }

                    _result.Clear();
                    _result = dtresultcopyMRN;
                }
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("Document");
                dtnew.Columns.Add("Sub Type");
                dtnew.Columns.Add("Manual Ref");
                dtnew.Columns.Add("Other Doc");
                dtnew.Columns.Add("Entry No");
                dtnew.Columns.Add("Job No");
                dtnew.Columns.Add("Other Loc");
                dtnew.Columns.Add("Status");
                BindUCtrlDDLData2(dtnew);

                txtFDate.Text = Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(DateTime.Now).Date.ToShortDateString();
                UserDPopoup.Show();
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

        #region Rooting for Load Item Detail
        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;
            try
            {
                _chargeType = string.Empty;
                lblItemDescription.Text = string.Empty;
                lblItemModel.Text = string.Empty;
                lblItemBrand.Text = string.Empty;
                lblItemSerialStatus.Text = string.Empty;
                _itemdetail = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_itemdetail != null)
                {
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        if (_itemdetail.Mi_itm_tp == "V")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Virtual item not allowed !!!')", true);
                            txtItem.Text = string.Empty;
                            lblItemDescription.Text = string.Empty;
                            lblItemModel.Text = string.Empty;
                            lblItemBrand.Text = string.Empty;
                            lblItemSerialStatus.Text = string.Empty;
                            txtUnitCost.Text = string.Empty;
                            txtQty.Text = string.Empty;
                            return false;
                        }

                        _isValid = true;
                        _chargeType = _itemdetail.Mi_chg_tp;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Serialized" : "Non-Serialized";

                        lblItemDescription.Text = _description;
                        lblItemModel.Text = _model;
                        lblItemBrand.Text = _brand;
                        lblItemSerialStatus.Text = _serialstatus;
                        _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid item code !!!')", true);
                    txtItem.Text = string.Empty;
                    lblItemDescription.Text = string.Empty;
                    lblItemModel.Text = string.Empty;
                    lblItemBrand.Text = string.Empty;
                    lblItemSerialStatus.Text = string.Empty;
                    txtUnitCost.Text = string.Empty;
                    txtQty.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _isValid = false;
                CHNLSVC.CloseChannel();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return _isValid;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
            return _isValid;
        }
        #endregion

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        protected void btnSearch_AdjSubType_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "123";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                txtSearchbyword.Text = "";
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

        protected void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                txtSearchbyword.Text = "";
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

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtUnitCost.Text = string.Empty;
                txtQty.Text = string.Empty;
                //gvBalance.DataSource = null;
                //gvBalance.AutoGenerateColumns = false;
                //gvBalance.DataSource = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, string.Empty);
                //gvBalance.DataBind();
                CheckItem();
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

        protected void CheckQty()
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text)) return;

                if (string.IsNullOrEmpty(txtItem.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the item !!!')", true);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the status !!!')", true);
                    ddlStatus.Focus();
                    return;
                }

                if (ddlAdjType.SelectedItem.ToString() == "ADJ-")
                {
                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());

                    if (_inventoryLocation != null)
                    {
                        if (_inventoryLocation.Count == 1)
                        {
                            foreach (InventoryLocation _loc in _inventoryLocation)
                            {
                                decimal _formQty = Convert.ToDecimal(txtQty.Text);
                                if (_formQty > _loc.Inl_free_qty)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot exceed the required qty !!!')", true);
                                    txtQty.Text = string.Empty;
                                    txtQty.Focus();
                                    return;
                                }
                            }
                        }
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

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckQty();
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

        protected void AddItem()
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserSeqNo.Text))
                {
                    GenerateNewUserSeqNo();
                }
                //var list = new List<InventoryRequestItem>();

                //list = (List<InventoryRequestItem>)Session["SCAN_ITEM_LIST"];

                //if (list != null)
                //{
                //    ScanItemList = list;
                //}

                CheckItem();
                if (string.IsNullOrEmpty(txtSupplierCd.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter supplier code !!!')", true);
                    btnSearchSupp.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the item !!!')", true);
                    txtItem.Focus();
                    return;
                }

                if ((string.IsNullOrEmpty(ddlStatus.Text.ToString())) || (ddlStatus.SelectedItem.Text == "Select"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the status !!!')", true);
                    ddlStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the qty !!!')", true);
                    txtQty.Focus();
                    return;
                }

                DataTable dtsupitem = CHNLSVC.Inventory.GetSuplierByItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());

                foreach (DataRow ddritem in dtsupitem.Rows)
                {
                    if (ddritem["mbii_cd"].ToString() == txtSupplierCd.Text.Trim())
                    {
                        successItems.Add(1);
                    }
                    else
                    {
                        successItems.Add(0);
                    }
                }

                if (!successItems.Contains(1))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serials which does not belong to the given suppler, cannot be added !!!')", true);
                    return;
                }

                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {
                    if (string.IsNullOrEmpty(txtUnitCost.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter unit cost !!!')", true);
                        txtUnitCost.Focus();
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtUnitCost.Text.ToString()) <= 0 && ddlStatus.Text.ToString().ToUpper() != "CONSIGNMENT")
                        {
                            if (_chargeType != "FOC")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Unit cost can't be less than or equal to zero amount !!!')", true);
                                txtUnitCost.Text = string.Empty;
                                txtUnitCost.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (ddlStatus.Text.ToString().ToUpper() == "CONSIGNMENT")
                            {
                                if (Convert.ToDecimal(txtUnitCost.Text.ToString()) != 0)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Unit cost should be zero(0.00) for CONSIGNMENT status !!!')", true);
                                    txtUnitCost.Text = string.Empty;
                                    txtUnitCost.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    txtUnitCost.Text = "0";
                }

                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                InventoryRequestItem _itm = new InventoryRequestItem();
                ScanItemList = new List<InventoryRequestItem>();
                if (ScanItemList != null)
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == txtItem.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString()
                                         select _ls;

                        if (_duplicate != null)
                            if (_duplicate.Count() > 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected item already available !!!')", true);
                                return;
                            }

                        var _maxline = (from _ls in ScanItemList
                                        select _ls.Itri_line_no).Max();
                        _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                        _itm.Itri_itm_cd = txtItem.Text.Trim();
                        _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                        _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                        _itm.Itri_qty = Convert.ToDecimal(0);
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(txtUnitCost.Text);
                    }
                    else
                    {
                        //var _maxline1 = (from _ls1 in ScanItemList
                        //                select _ls1.Itri_line_no).Max();

                        _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                        _itm.Itri_itm_cd = txtItem.Text.Trim();
                        _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                        _itm.Itri_line_no = 1;
                        //_itm.Itri_line_no = Convert.ToInt32(_maxline1) + 1;
                        _itm.Itri_qty = Convert.ToDecimal(0);
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(txtUnitCost.Text);
                    }

                ScanItemList.Add(_itm);

                //if (string.IsNullOrEmpty(txtUserSeqNo.Text))
                //{
                //    GenerateNewUserSeqNo();
                //}

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = "CONS";
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = 0;
                    _reptitm.Tui_sup = txtSupplierCd.Text.Trim();
                    _saveonly.Add(_reptitm);
                }
               Int32 i=  CHNLSVC.Inventory.SavePickedItems(_saveonly);

                //grdItems.DataSource = null;
                //grdItems.DataSource = ScanItemList;
                //grdItems.DataBind();
                //LoadItems(txtUserSeqNo.Text);

                //ClearItemDetail2();
               // LoadItemDetail(string.Empty);
               // txtItem.Focus();

               Session["SCAN_ITEM_LIST"] = ScanItemList;
             
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


        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplierCd.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter supplier code !!!')", true);
                    btnSearchSupp.Focus();
                    return;
                }
                AddItem();
                panelReqestAdd.Enabled = false;
                txtItem.Text = "";
                txtQty.Text = "";
                lblItemDescription.Text = "";
                lblItemModel.Text = "";
                lblItemBrand.Text = "";
                lblItemSerialStatus.Text = "";

                LoadItems(string.IsNullOrEmpty(txtDocumentNo.Text) ? txtUserSeqNo.Text : txtDocumentNo.Text);
                foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                {
                    Label lblapqtygrd = (Label)hiderowbtn.FindControl("lblapqtygrd");
                    Label lblitri_qty = (Label)hiderowbtn.FindControl("lblitri_qty");

                    lblapqtygrd.Text = DoFormat(Convert.ToDecimal(lblapqtygrd.Text.Trim()));
                    lblitri_qty.Text = DoFormat(Convert.ToDecimal(lblitri_qty.Text.Trim()));
                }
               

                foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                {
                    LinkButton lbtnadditem = (LinkButton)hiderowbtn.FindControl("lbtnadditem");
                    lbtnadditem.Visible = true;
                }

               
                LoadStatusDescriptionItem();
                LoadStatusDescriptionSerial();
                txtSupplierCd.Enabled = false;
                btnSearchSupp.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void Clear()
        {
            DateTime date = DateTime.Now;
            dtpDate.Text = date.ToString("dd/MMM/yyyy");

            DateTime fromdt = DateTime.Now.AddMonths(-1);
            txtfromdate.Text = fromdt.ToString("dd/MMM/yyyy");

            DateTime todate = DateTime.Now;
            txttodate.Text = todate.ToString("dd/MMM/yyyy");

            grdItems.DataSource = new int[] { };
            grdItems.DataBind();

            //gvBalance.DataSource = new int[] { };
            //gvBalance.DataBind();

            grdSerial.DataSource = new int[] { };
            grdSerial.DataBind();

            gvsearchdata.DataSource = new int[] { };
            gvsearchdata.DataBind();

            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "m_Trans_Inventory_ConsStock_StockRtn", dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);

            ClearItemDetail();

            txtDocumentNo.Text = string.Empty;
            chktemp.Checked = false;
            txtManualRef.Text = string.Empty;
            txtOtherRef.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtUserSeqNo.Text = string.Empty;
            lblItemDescription.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemSerialStatus.Text = string.Empty;

            lbtnsave.Enabled = true;
            lbtnsave.CssClass = "buttonUndocolor";
            lbtnsave.OnClientClick = "SaveConfirm();";

            lbtntempsave.Enabled = true;
            lbtntempsave.CssClass = "buttonUndocolor";
            lbtntempsave.OnClientClick = "TempSaveConfirm();";
            Session["SERIALLIST"] = null;
            Session["POPUP_CRN_RETURN"] = null;
            Session["GEN_SEQ"] = null;
            Session["SCAN_ITEM_LIST"] = null;
            Session["Doc"] = null;
            Session["TempDoc"] = null;
            Session["CRN_SUPP"] = null;
            Session["NOT_AVAILABLE_SERIALS"] = null;
            txtdocname.ReadOnly = false;
            UserDPopoup.Hide();
            txtSupplierCd.Text = string.Empty;
            PanelItemAdd.Enabled = true;
            panelReqestAdd.Enabled = true;
            lblBatchNo.Text = "";
            ddlSeqNo.SelectedIndex = 0;
            txtSupplierCd.Enabled = true;
            btnSearchSupp.Enabled = true;
            ucOutScan.ScanItemList = new List<InventoryRequestItem>();
            ViewState["ScanItemList"] = new List<InventoryRequestItem>();
            ViewState["SerialList"] = new List<ReptPickSerials>();
        }

        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, TextBox _dtpcontrol, Label _lblcontrol, string _date, out bool _allowCurrentDate)
        {
            BackDates _bdt = new BackDates();
            _dtpcontrol.Enabled = false;
            _allowCurrentDate = false;

            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
            if (_isAllow == true)
            {
                _dtpcontrol.Enabled = true;
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
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

        public void CheckSessionIsExpired()
        {
            if (Session["UserID"].ToString() != "ADMIN")
            {
                string _expMsg = "Current Session is expired or has been closed by administrator!";
                if (CHNLSVC.Security.IsSessionExpired(Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(),out _expMsg) == true)
                {
                    BaseCls.GlbIsExit = true;
                    GC.Collect();
                }
            }
        }

        protected void lbtnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSavelconformmessageValue.Value == "Yes")
                {
                    if ((chktemp.Checked == true) && (!string.IsNullOrEmpty(txtDocumentNo.Text.Trim())))
                    {
                        Session["DocType"] = "TempDoc";
                    }
                    else
                    {
                        Session["DocType"] = "Doc";
                    }
                    Process(false);
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

        public bool CheckServerDateTime()
        {
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine date conflict with the server date.please contact system administrator !!!')", true);
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine time zone conflict with the server time zone.please contact system administrator !!!')", true);
                return false;
            }
            return true;
        }

        public bool IsReferancedDocDateAppropriate(List<ReptPickSerials> _reptPrickSerialList, DateTime _processDate)
        {
            bool _appropriate = true;
            if (_reptPrickSerialList != null)
            {
                var _isInAppropriate = _reptPrickSerialList.Where(x => x.Tus_doc_dt.Date > _processDate.Date).ToList();
                if (_isInAppropriate == null || _isInAppropriate.Count <= 0) _appropriate = true;
                else _appropriate = false;
                if (_appropriate == false)
                {
                    StringBuilder _documents = new StringBuilder();
                    foreach (ReptPickSerials _one in _isInAppropriate)
                    {
                        if (string.IsNullOrEmpty(_documents.ToString()))
                            _documents.Append(_one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                        else
                            _documents.Append(" and " + _one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                    }
                    string msg = "The Inward documents " + _documents.ToString() + " equal or grater than to a this Outward document " + _processDate.Date.ToShortDateString() + " date!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                }
            }
            return _appropriate;
        }

        private void Process(bool _ISTEMP)
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(ddlAdjType.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Select the adjustment type !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtAdjSubType.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Select the adjustment sub type !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";
                if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                bool _allowCurrentTrans = false;

                DateTime mydate = DateTime.Now;

                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), "m_Trans_Inventory_ConsStock_StockRtn", dtpDate, lblH1, mydate.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (mydate.Date != DateTime.Now.Date)
                        {
                            dtpDate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction !!!')", true);
                            dtpDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpDate.Enabled = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction !!!')", true);
                        dtpDate.Focus();
                        return;
                    }
                }

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo = "";
                Int32 result = -99;
                Int32 _userSeqNo = 0;
                int _direction = 0;
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+") _direction = 1;

                var mylist = (List<ReptPickSerials>)Session["SERIALLIST"];

                if (chktemp.Checked == true)
                {
                    _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);
                    reptPickSerialsList = mylist;
                }
                else
                {
                    _userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "CRN", Session["UserID"].ToString(), _direction, txtUserSeqNo.Text);
                    reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "CRN");
                    reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "CRN");
                }

                if (reptPickSerialsList == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found !!!')", true);
                    return;
                }

                //------------only CONS should be scanned-----------add by shani 25-06-2013------------------------------------------------
                //-----------------------------------------------------------------------------
                if (txtSupplierCd.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter supplier code !!!')", true);
                    return;
                }
                //------------------------------------------------------------------------------
                if (reptPickSerialsList!=null)
                {
                    foreach (ReptPickSerials rps in reptPickSerialsList)
                    {
                        if (rps.Tus_itm_stus != ddlStatus.SelectedValue.ToString())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please remove non-consignment serials from the scanned serial list !!!')", true);
                            return;
                        }
                    }
                    foreach (ReptPickSerials rps in reptPickSerialsList)
                    {
                        if (rps.Tus_orig_supp != null)
                        {
                            if ((rps.Tus_orig_supp != txtSupplierCd.Text.Trim()) && (rps.Tus_orig_supp != "") && (rps.Tus_orig_supp.ToUpper() != "N/A"))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot save. items belong to a different supplier.Please check the supplier again !!!')", true);
                                return;
                            }
                        }
                    }
                }
                //-----------------------------------------------------------------------               

                
                //-------------------------------------------------------------------------------
                //--------------------------------------------------------------------------------------------------------------
                #region Check Referance Date and the Doc Date
                if (_direction == 0 && reptPickSerialsList!=null)
                {
                    if (IsReferancedDocDateAppropriate(reptPickSerialsList, mydate.Date) == false)
                    {
                        return;
                    }
                }
                #endregion

                #region Check Duplicate Serials
                if (reptPickSerialsList!=null)
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
                        string msg2 = "Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                        return;
                    }
                }

                
                #endregion
                InventoryHeader inHeader = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeader.Ith_channel = string.Empty;
                    }
                }
                if (txtSupplierCd.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter supplier code !!!')", true);
                    return;
                }
                inHeader.Ith_acc_no = "CONS_OUTS";
                inHeader.Ith_anal_1 = "";
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = txtSupplierCd.Text.Trim();
                inHeader.Ith_cate_tp = txtAdjSubType.Text.ToString().Trim(); //"CONSIGN";
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_cre_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";
                if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                {
                    inHeader.Ith_direct = true;
                }
                else
                {
                    inHeader.Ith_direct = false;
                }
                inHeader.Ith_doc_date = mydate.Date;
                inHeader.Ith_doc_no = string.Empty;
                inHeader.Ith_doc_tp = "ADJ";
                inHeader.Ith_doc_year = mydate.Year;
                inHeader.Ith_entry_no = txtOtherRef.Text.ToString().Trim();//_ISTEMP?txtUserSeqNo.Text:txtOtherRef.Text.ToString().Trim();
                inHeader.Ith_entry_tp = txtAdjSubType.Text.ToString().Trim();
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = string.Empty;
                inHeader.Ith_isprinted = false;//????
                inHeader.Ith_is_manual = false;//????
                inHeader.Ith_job_no = string.Empty;
                inHeader.Ith_loading_point = string.Empty;
                inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                inHeader.Ith_manual_ref = txtManualRef.Text.Trim();
                inHeader.Ith_mod_by = Session["UserID"].ToString();
                inHeader.Ith_mod_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_oth_loc = string.Empty;
                inHeader.Ith_remarks = txtRemarks.Text;
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = "CONS";
                inHeader.Ith_vehi_no = string.Empty;
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                if (Session["DocType"].ToString() == "TempDoc")
                {
                    inHeader.Ith_anal_10 = true;
                    inHeader.Ith_anal_2 = txtDocumentNo.Text;
                }
                else
                {
                    inHeader.Ith_anal_10 = false;
                    inHeader.Ith_anal_2 = "";
                }
                inHeader.Ith_sub_docno = txtDocumentNo.Text.Trim();
                inHeader.Ith_oth_docno = txtDocumentNo.Text.Trim();
                #endregion
                MasterAutoNumber masterAuto = new MasterAutoNumber();

                #region Fill MasterAutoNumber

                masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "CONS";
                masterAuto.Aut_number = 0;
                masterAuto.Aut_start_char = "CONS";
                masterAuto.Aut_year = null;

                #endregion

                #region Update some serial items
                if (_direction == 1 && reptPickSerialsList!=null)
                {
                    foreach (var _seritem in reptPickSerialsList)
                    {
                        _seritem.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                        _seritem.Tus_exist_grndt = mydate.Date;
                        _seritem.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                        _seritem.Tus_orig_grndt = mydate.Date;
                    }
                }
                #endregion
                if (reptPickSerialsList==null)
                {
                    reptPickSerialsList = new List<ReptPickSerials>();
                }
                if (reptPickSubSerialsList == null)
                {
                    reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                }
                _popSerialList = new List<TempPopup>();
                _popSerSearch = false;
                UserAdPopup.Hide();
                #region Save Adj+ / Adj-
                if (_direction == 1)
                {
                    result = CHNLSVC.Inventory.ADJPlus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo);
                }
                else
                {
                    result = CHNLSVC.Inventory.ConsignmentReturn(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo,_ISTEMP);
                }

                if (result != -99 && result >= 0)
                {
                    string Msg = "Successfully Saved! Document No : " + documntNo;
                    Session["documntNo"] = documntNo;

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);

                    UserDPopoup.Hide();

                    Session["GlbReportType"] = "SCM1_CONSOT";
                    Session["GlbReportName"] = "Inward_Docs_Consign.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    clsInventory obj = new clsInventory();
                    obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                    PrintPDF(targetFileName, obj._consIn);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    //if (MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\nDo you want to print this?", "Process Completed : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{
                    //    //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //    //if (_direction == 1) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                    //    //if (Session["UserCompanyCode"].ToString() == "SGL") //Sanjeewa 2014-01-07
                    //    //{
                    //    //    if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                    //    //    else _view.GlbReportName = "Outward_Docs.rpt";
                    //    //}
                    //    //else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                    //    //{
                    //    //    if (_direction == 1) _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                    //    //    else _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                    //    //}
                    //    //else
                    //    //{
                    //    //    if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                    //    //    else _view.GlbReportName = "Outward_Docs.rpt";
                    //    //}
                    //    //_view.GlbReportDoc = documntNo;
                    //    //_view.Show();
                    //    //_view = null;
                    //}
                    lbtnclear_Click(null, null);
                }
                else
                {
                    if (!string.IsNullOrEmpty(documntNo))
                    {
                        if (documntNo.Contains("CHK_INLFREEQTY"))
                        {
                            DispMsg("There is no free stock balance available. Please check the stock balances.", "E");
                        }
                        else if (documntNo.Contains("CHK_INLRESQTY"))
                        {
                            DispMsg("There is no reserved stock available. Please check the stock balances.", "E");
                        }
                        else if (documntNo.Contains("NO_STOCK_BALANCE"))
                        {
                            DispMsg("There is no free stock available. Please check the stock balances.[Batch]", "E");
                        }
                        else if (documntNo.Contains("CHK_INLQTY"))
                        {
                            DispMsg("There is no stock available. Please check the stock balances.", "E");
                        }
                        else if (documntNo.Contains("CHK_INBFREEQTY"))
                        {
                            DispMsg("There is no bin stock available !", "E");
                        }
                        else if (documntNo.Contains("CHK_ITRIBQTY"))
                        {
                            DispMsg("There is no request balance available. Please check the request balances. !", "E");
                        }
                        else if (documntNo.Contains("CHK_INBQTY"))
                        {
                            DispMsg("Please check the stock balances.[Batch]");
                        }
                        else
                        {
                            DispMsg(documntNo, "E");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }

                #endregion
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error')", true);
                return;
            }
        }

        protected void lbtndelser_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
                {
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    if (row != null)
                    {
                        string _item = (row.FindControl("lblitri_itm_cd") as Label).Text;
                        string _status = (row.FindControl("lblstatus") as Label).Text;
                        string _serialIDtext = (row.FindControl("lblserialid") as Label).Text;
                        string _bin = (row.FindControl("lblbin") as Label).Text;
                        string _serial1 = (row.FindControl("lblser1") as Label).Text;
                        string _requestno = (row.FindControl("lblrequest2") as Label).Text;

                        if (string.IsNullOrEmpty(_item))
                        {
                            return;
                        }
                        OnRemoveFromSerialGrid(_item, Convert.ToInt32(_serialIDtext), _status);
                        //MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                        //if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                        //{
                        //    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialIDtext), null, null);
                        //    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, Convert.ToInt32(_serialIDtext), 1);
                        //}
                        //else
                        //{
                        //    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _item, _status);

                        //}
                        //LoadItems(txtUserSeqNo.Text);
                    }
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void OnRemoveFromSerialGrid(string _item, int _serialID, string _status)
        {
            try
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), null, null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);

                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _item, _status);

                }
                LoadItems(string.IsNullOrEmpty(txtDocumentNo.Text.Trim())?txtUserSeqNo.Text:txtDocumentNo.Text);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtndeleteitm_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "No")
                {
                    return;
                }
                if (grdItems.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                    string lblstatus = (row.FindControl("lblstatus") as Label).Text;
                    int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                    if (string.IsNullOrEmpty(_itemCode)) return;
                    OnRemoveFromItemGrid(_itemCode, lblstatus, _lineNo);
                }

            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void OnRemoveFromItemGrid(string _itemCode, string _itemStatus, int _lineNo)
        {
            try
            {
                bool _Isdelete = false;
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "ADJ");
                if (_list != null)
                {
                    if (_list.Count > 0)
                    {
                        var _delete = (from _lst in _list
                                       where _lst.Tus_itm_cd == _itemCode && _lst.Tus_itm_stus == _itemStatus && _lst.Tus_base_itm_line == _lineNo
                                       select _lst).ToList();

                        foreach (ReptPickSerials _ser in _delete)
                        {
                            Delete_Serials(_itemCode, _itemStatus, _ser.Tus_ser_id);
                            _Isdelete = true;
                        }
                    }
                }
                    CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus, _lineNo, 2);
                    if (ScanItemList!=null)
                    {
                        if (ScanItemList.Count>0)
                        {
                            ScanItemList.RemoveAll(x => x.Itri_itm_cd == _itemCode && x.Itri_itm_stus == _itemStatus);
                        }
                    }   
                
                 LoadItems(txtUserSeqNo.Text);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

        }
        void Delete_Serials(string _itemCode, string _itemStatus, Int32 _serialID)
        {
            try
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), null, null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
                }

                grdSerial.DataSource = serial_list;
                grdSerial.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnSearchSupp_Click(object sender, EventArgs e)
        {
            try
            {
                UserDPopoup.Hide();
                //if (txtSupplierCd.Text != "")
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot change supplier now !!!')", true);
                //    return;
                //}

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                grdResult.PageIndex = 0;
                txtSearchbyword.Text = "";
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

        protected void txtSupplierCd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplierCd.Text)) return;

                if (!CHNLSVC.Inventory.IsValidSupplier(Session["UserCompanyCode"].ToString(), txtSupplierCd.Text, 1, "S"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid supplier !!!')", true);
                    txtSupplierCd.Text = "";
                    txtSupplierCd.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            try
            {
                UserDPopoup.Hide();
                if (txtcancenconfirm.Value == "Yes")
                {
                    if (txtDocumentNo.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the document number to cancel !!!')", true);
                        txtDocumentNo.Focus();
                        return;
                    }
                    Boolean eff = CHNLSVC.Inventory.CancelInventoryDocument(txtDocumentNo.Text.Trim(), Session["UserID"].ToString());
                    if (eff == true)
                    {
                        string Msg = "Successfully cancelled !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);
                        lbtnclear_Click(null, null);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
        protected void lbtnfindall_Click(object sender, EventArgs e)
        {
            try
            {
                UserDPopoup.Hide();
                if (string.IsNullOrEmpty(txtSupplierCd.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter supplier code !!!')", true);
                    txtSupplierCd.Focus();
                    return;
                }

                DataTable dtsearchdata = CHNLSVC.Inventory.SearchCRNDoc(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtfindreq.Text.Trim(), Convert.ToDateTime(txtfromdate.Text), Convert.ToDateTime(txttodate.Text), txtSupplierCd.Text.Trim());

                //DataTable uniquecols = RemoveDuplicateRows(dtsearchdata, "inb_doc_no");

                gvsearchdata.DataSource = dtsearchdata;
                gvsearchdata.DataBind();
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

        private void LoadStatusDescriptionItem()
        {
            try
            {
                foreach (GridViewRow row in this.grdItems.Rows)
                {
                    Label lblstatus = (Label)row.FindControl("lblstatus");
                    Label lblstatusdesc = (Label)row.FindControl("lblstatusdesc");

                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblstatus.Text.Trim());

                    if (dtstatustx.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatustx.Rows)
                        {
                            lblstatusdesc.Text = ddr2[0].ToString();
                        }
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

        private void LoadStatusDescriptionSerial()
        {
            try
            {
                foreach (GridViewRow row in this.grdSerial.Rows)
                {
                    Label lblstatus = (Label)row.FindControl("lblstatus");
                    Label lblstatusdesc = (Label)row.FindControl("lblserstus");

                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblstatus.Text.Trim());

                    if (dtstatustx.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatustx.Rows)
                        {
                            lblstatusdesc.Text = ddr2[0].ToString();
                        }
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
        protected void lbtnaddselected_Click(object sender, EventArgs e)
        {
            try
            {
                List<Int32> checkcount = new List<int>();
                foreach (GridViewRow row in this.gvsearchdata.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");

                    if (chkselect.Checked)
                    {
                        checkcount.Add(row.RowIndex);
                    }
                }

                if (checkcount.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select at least one document number !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtSupplierCd.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter supplier code !!!')", true);
                    btnSearchSupp.Focus();
                    return;
                }

                foreach (GridViewRow row in this.gvsearchdata.Rows)
                {
                    Label lblsupp = (Label)row.FindControl("lblsupplier");

                    if (txtSupplierCd.Text.Trim() != lblsupp.Text)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serials which does not belong to the given suppler, cannot be added !!!')", true);
                        return;
                    }
                }

                DataTable dtitems = new DataTable();
                dtitems.Columns.AddRange(new DataColumn[11] { new DataColumn("itri_line_no"), new DataColumn("itri_itm_cd"), 
                    new DataColumn("mi_longdesc"), new DataColumn("mi_model"), new DataColumn("itri_itm_stus"), new DataColumn("itri_unit_price"), 
                    new DataColumn("itri_app_qty"), new DataColumn("itri_qty"), new DataColumn("itri_note") , new DataColumn("Mis_desc"), new DataColumn("Itri_bqty")});

                DataTable dtserials = new DataTable();
                dtserials.Columns.AddRange(new DataColumn[13] { new DataColumn("tus_itm_cd"), new DataColumn("tus_itm_model"), new DataColumn("tus_itm_stus"), 
                    new DataColumn("tus_qty"), new DataColumn("tus_ser_1"), new DataColumn("tus_ser_2"), new DataColumn("tus_ser_3"), new DataColumn("tus_bin"), 
                    new DataColumn("tus_ser_id"), new DataColumn("tus_base_doc_no"), new DataColumn("tus_base_itm_line"),
                    new DataColumn("Tus_orig_supp"),new DataColumn("Tus_itm_stus_Desc")});
               // List<ReptPickItems> _msgItmList = new List<ReptPickItems>();
                List<ReptPickSerials> _MsgList = new List<ReptPickSerials>();
                foreach (GridViewRow row in this.gvsearchdata.Rows)
                {
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                    if (chkselect.Checked)
                    {
                        Label lbldocno = (Label)row.FindControl("lbldocno");
                        Label lblitri_itm_cd = (Label)row.FindControl("lblitemcode");
                        Label lbldescgrd = (Label)row.FindControl("lblshortdesc");
                        Label lblmodelgrd = (Label)row.FindControl("lblmodel");
                        Label lblstatus = (Label)row.FindControl("lblitmstatus");
                        Label lblcost = (Label)row.FindControl("lblunitprice");
                        Label lblapqtygrd = (Label)row.FindControl("lblfreeqty");
                        Label lblitri_qty = (Label)row.FindControl("lblfreeqty");
                        //Label lblserid = (Label)row.FindControl("lblserid");
                        //Label lblser1 = (Label)row.FindControl("lblser1");
                        //Label lblser2 = (Label)row.FindControl("lblser2");
                        Label lblsupplier = (Label)row.FindControl("lblsupplier");
                        Label lblbin = (Label)row.FindControl("lblbin");
                        Label lblComCd = (Label)row.FindControl("lblComCd");
                        Label lblLocCd = (Label)row.FindControl("lblLocCd");
                        List<ReptPickItems> _list = CHNLSVC.Inventory.GET_ReptPickItems(new ReptPickItems() { Tui_req_itm_cd = lblitri_itm_cd.Text });

                        dtitems.Rows.Add(dtitems.Rows.Count + 1, lblitri_itm_cd.Text, lbldescgrd.Text, lblmodelgrd.Text, lblstatus.Text, lblcost.Text, lblapqtygrd.Text, 0, "","CONSIGNMENT",0);
                        //bool _itmHave = false;
                        bool _serHave = false;
                       // _itmHave = _list == null ? false : _list.Count == 0 ? false : true;

                        List<InventorySerialMaster> _invSerList = CHNLSVC.Inventory.GetSerialMasterData(new InventorySerialMaster()
                        {
                            Irsm_com = lblComCd.Text.ToUpper(),
                            Irsm_doc_no = lbldocno.Text.ToUpper(),
                            Irsm_loc = lblLocCd.Text.ToUpper(),
                            Irsm_itm_cd = lblitri_itm_cd.Text.ToUpper(),
                            Irsm_ser_id = 0
                        });
                        if (_invSerList != null)
                        {
                            foreach (var _ser in _invSerList)
                            {
                                List<ReptPickSerials> _listSer = CHNLSVC.Inventory.GET_ReptPickSerials(new ReptPickSerials() { Tus_ser_id = _ser.Irsm_ser_id });
                                _serHave = _listSer == null ? false : _listSer.Count == 0 ? false : true;
                                if (!_serHave)
                                {
                                    
                                    dtserials.Rows.Add(lblitri_itm_cd.Text, lblmodelgrd.Text, lblstatus.Text, lblitri_qty.Text, _ser.Irsm_ser_1, _ser.Irsm_ser_2, "", lblbin.Text, _ser.Irsm_ser_id, lbldocno.Text,
                                        dtserials.Rows.Count + 1, lblsupplier.Text, "CONSIGNMENT");
                                }
                                else
                                {
                                    _MsgList.Add(_listSer[0]);
                                    //if (_itmHave)
                                    //{
                                    //    _list[0].Tui_req_itm_cd = lblitri_itm_cd.Text;
                                    //    Int32 _msgCount = _msgItmList.Where(c => c.Tui_req_itm_cd == _list[0].Tui_req_itm_cd).ToList().Count;
                                    //    if (_msgCount == 0)
                                    //    {
                                    //        _msgItmList.Add(_list[0]);
                                    //    }
                                    //}
                                    //if (_serHave)
                                    //{

                                        //Int32 _msgCount = _MsgList.Where(c => c.Tus_ser_id == _listSer[0].Tus_ser_id).ToList().Count;
                                        //if (_msgCount == 0)
                                        //{
                                        //    _MsgList.Add(_listSer[0]);
                                        //}
                                    //}
                                }
                            }
                        }
                    }
                }

                #region Sahan
                #endregion
                //DataTable uniquecols = RemoveDuplicateRows(dtitems, "itri_itm_cd");

                //grdItems.DataSource = uniquecols;
                //grdItems.DataBind();

                //grdSerial.DataSource = dtserials;
                //grdSerial.DataBind();
                if (string.IsNullOrEmpty(txtUserSeqNo.Text))
                {
                    GenerateNewUserSeqNo();
                }
                foreach (DataRow ddritm in dtitems.Rows)
                {
                    string serial = string.Empty;
                    foreach (DataRow ddrserdup in dtserials.Rows)
                    {
                        if (ddritm["itri_itm_cd"].ToString() == ddrserdup["tus_itm_cd"].ToString())
                        {
                            serial = ddrserdup["tus_ser_1"].ToString();
                            Int32 rowline = dtitems.Rows.IndexOf(ddritm);
                            AddItems(ddritm["itri_itm_cd"].ToString(), ddritm["itri_unit_price"].ToString(), ddritm["itri_itm_stus"].ToString(), ddritm["itri_app_qty"].ToString(), txtUserSeqNo.Text.Trim(), serial, rowline + 1);
                        }
                    }
                }

                foreach (DataRow ddrser in dtserials.Rows)
                {
                    foreach (DataRow ddritemdup in dtitems.Rows)
                    {
                        if (ddrser["tus_itm_cd"].ToString() == ddritemdup["itri_itm_cd"].ToString())
                        {
                            Int32 rowlinedup = Convert.ToInt32(ddritemdup["itri_line_no"].ToString());
                            AddSerials(ddrser["tus_itm_cd"].ToString(), ddrser["tus_ser_1"].ToString(), txtUserSeqNo.Text.Trim(), ddrser["tus_itm_stus"].ToString(), Convert.ToDecimal(ddrser["tus_qty"].ToString()), rowlinedup, ddrser["tus_bin"].ToString(), Convert.ToInt32(ddrser["tus_ser_id"].ToString()));
                        }
                    }
                }

                string notavailableser = (string)Session["NOT_AVAILABLE_SERIALS"];

                if (!string.IsNullOrEmpty(notavailableser))
                {
                    grdItems.DataSource = null;
                    grdItems.DataBind();

                    grdSerial.DataSource = null;
                    grdSerial.DataBind();

                    txtUserSeqNo.Text = string.Empty;
                    return;
                }

                LoadStatusDescriptionItem();
                LoadStatusDescriptionSerial();
                txtSupplierCd.ReadOnly = true;

                foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                {
                    Label lblapqtygrd = (Label)hiderowbtn.FindControl("lblapqtygrd");
                    Label lblitri_qty = (Label)hiderowbtn.FindControl("lblitri_qty");
                    LinkButton lbtnadditem = (LinkButton)hiderowbtn.FindControl("lbtnadditem");
                    LinkButton lbtnAddSerial = (LinkButton)hiderowbtn.FindControl("lbtnAddSerial");

                    lblapqtygrd.Text = DoFormat(Convert.ToDecimal(lblapqtygrd.Text.Trim()));
                    lblitri_qty.Text = DoFormat(Convert.ToDecimal(lblitri_qty.Text.Trim()));

                    if (lblapqtygrd.Text.Trim () == lblitri_qty.Text.Trim())
                    {
                        lbtnadditem.Visible = false;
                        lbtnAddSerial.Visible = false;
                    }
                    else
                    {
                        lbtnadditem.Visible = true;
                        lbtnAddSerial.Visible = true;
                    }
                }
                if (_MsgList.Count>0 )
                {
                    dgvSerSave.DataSource = new int[] { };
                    if (_MsgList.Count>0)
                    {
                        foreach (var item in _MsgList)
                        {
                            
                        }
                        dgvSerSave.DataSource = _MsgList;
                    }
                    dgvSerSave.DataBind();
                    BindSerSaveGridStatus();
                    popupSaveData.Show();
                }
                PanelItemAdd.Enabled = false;
                LoadItems(txtUserSeqNo.Text);
                txtSupplierCd.Enabled = false;
                btnSearchSupp.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            //finally
            //{
            //    CHNLSVC.CloseChannel();
            //}
        }

        private void BindSerSaveGridStatus()
        {
            try
            {
                foreach (GridViewRow row in this.dgvSerSave.Rows)
                {
                    Label lblSatus = (Label)row.FindControl("lblSatus");
                   // Label lblstatusdesc = (Label)row.FindControl("lblserstus");

                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblSatus.Text.Trim());
                    if (dtstatustx != null)
                    {
                        if (dtstatustx.Rows.Count > 0)
                        {
                            foreach (DataRow ddr2 in dtstatustx.Rows)
                            {
                                lblSatus.Text = ddr2[0].ToString();
                            }
                        }
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
        protected void lbtnfindreq_Click(object sender, EventArgs e)
        {
            try
            {
                ddlAdjTypeSearch.SelectedItem.Text = "ADJ+";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                ddlAdjTypeSearch.SelectedItem.Text = "ADJ-";

                DataTable _result;

                grdResultD.DataSource = null;
                grdResultD.DataBind();
                
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now));

                DataView dv = new DataView(_result);

                dv.RowFilter = "Status <> 'C'";

                _result = dv.ToTable();
                var sortedTable = _result.AsEnumerable()
        .OrderByDescending(r => r.Field<string>("Document"))
        .CopyToDataTable();

                DataTable dtresultcopyMRN = new DataTable();
                dtresultcopyMRN.Columns.AddRange(new DataColumn[9] { new DataColumn("Document"), new DataColumn("Sub Type"), new DataColumn("Date"), new DataColumn("Manual Ref No"), new DataColumn("Other Document"), new DataColumn("Entry No"), new DataColumn("Job No"), new DataColumn("Other Loc"), new DataColumn("Status") });

                foreach (DataRow ddr in sortedTable.Rows)
                {
                    string doc = ddr["Document"].ToString();
                    string subtp = ddr["Sub Type"].ToString();
                   // string date = ((DateTime)ddr["Date"]).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    string date = Convert.ToDateTime(ddr["Date"].ToString()).Date.ToString("dd/MMM/yyyy");
                    string manref = ddr["Manual Ref No"].ToString();
                    string otherdoc = ddr["Other Document"].ToString();
                    string entry = ddr["Entry No"].ToString();
                    string job = ddr["Job No"].ToString();
                    string otherloc = ddr["Other Loc"].ToString();
                    string stus = ddr["Status"].ToString();

                    dtresultcopyMRN.Rows.Add(doc, subtp, date, manref, otherdoc, entry, job, otherloc, stus);
                }

                sortedTable.Clear();
                sortedTable = dtresultcopyMRN;


                grdResultD.DataSource = null;
                grdResultD.DataBind();

                grdResultD.DataSource = sortedTable;
                grdResultD.DataBind();

                BindUCtrlDDLData2(sortedTable);

                txtFDate.Text = Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(DateTime.Now).Date.ToShortDateString();
                UserDPopoup.Show();
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

        protected void lbtnfindsup_Click(object sender, EventArgs e)
        {
            try
            {
                UserDPopoup.Hide();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                txtSearchbyword.Text = "";
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

        protected void AddItems(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial, Int32 itemline)
        {
            try
            {
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                ScanItemList = new List<InventoryRequestItem>();

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
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_qty = x.Itri_qty + Convert.ToDecimal(txtQty.Text.ToString()));

                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_app_qty = x.Itri_app_qty + Convert.ToDecimal(txtQty.Text.ToString()));
                            }
                            else
                            {
                                var _maxline = (from _ls in ScanItemList
                                                select _ls.Itri_line_no).Max();
                                _itm.Itri_itm_cd = _item;
                                _itm.Itri_itm_stus = _status;
                                _itm.Itri_line_no = itemline;
                                _itm.Itri_qty = Convert.ToDecimal(_qty);
                                _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                                _itm.Itri_bqty = Convert.ToDecimal(_qty);
                                _itm.Mi_longdesc = _itms.Mi_longdesc;
                                _itm.Mi_model = _itms.Mi_model;
                                _itm.Mi_brand = _itms.Mi_brand;
                                _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                                ScanItemList.Add(_itm);
                            }
                    }
                    else
                    {
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = itemline;
                        _itm.Itri_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }
                }
                else
                {
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = itemline;
                    _itm.Itri_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_bqty = Convert.ToDecimal(_qty);
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }

                if (string.IsNullOrEmpty(_UserSeqNo))
                {
                    Int32 Seq = GenerateNewUserSeqNo();
                    txtUserSeqNo.Text = Seq.ToString();
                }
                if (!string.IsNullOrEmpty(txtUserSeqNo.Text))
                {
                    Int32 SeqNo = 0;
                    if (Int32.TryParse(txtUserSeqNo.Text, out  SeqNo))
                    {
                        ReptPickHeader _repPH = CHNLSVC.Inventory.GetReptPickHeader(new ReptPickHeader() { Tuh_usrseq_no=SeqNo });
                        lblBatchNo.Text = "";
                        if (_repPH != null)
                        {

                            lblBatchNo.Text = _repPH.Tuh_doc_no;
                        }
                    }
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _reptitm.Tui_sup = txtSupplierCd.Text.Trim();
                    _saveonly.Add(_reptitm);
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);
                //ViewState["ScanItemList"] = ScanItemList;

                Session["SCAN_ITEM_LIST"] = ScanItemList;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void AddSerials(string _item, string _Serial, string _Seqno, string _stus, decimal _qty,Int32 lineno,string bin,Int32 serialid)
        {
            Int32 generated_seq = -1;
            MasterItem msitem = new MasterItem();
            List<ReptPickSerials> Tempserial_list = new List<ReptPickSerials>();
            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

            if (msitem.Mi_is_ser1 == 1)
            {
                Tempserial_list = new List<ReptPickSerials>();
                Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);

                Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial).ToList();

                if (Tempserial_list.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Some serial/serials are not available at your location !!!')", true);
                    Session["NOT_AVAILABLE_SERIALS"] = "Yes";
                    return;
                }
            }
            else
            {
                Session["NOT_AVAILABLE_SERIALS"] = null;
                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                Tempserial_list = new List<ReptPickSerials>();
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                _reptPickSerial_.Tus_seq_no = generated_seq;
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_doc_no = txtUserSeqNo.Text.Trim();
                _reptPickSerial_.Tus_base_doc_no = txtfindreq.Text.Trim();
                _reptPickSerial_.Tus_itm_line = lineno;
                _reptPickSerial_.Tus_base_itm_line = lineno;
                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                _reptPickSerial_.Tus_bin = bin;
                _reptPickSerial_.Tus_itm_cd = _item;
                _reptPickSerial_.Tus_itm_stus = _stus;
                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                _reptPickSerial_.Tus_ser_1 = _Serial;
                _reptPickSerial_.Tus_ser_2 = "N/A";
                _reptPickSerial_.Tus_ser_3 = "N/A";
                _reptPickSerial_.Tus_ser_4 = "N/A";
                _reptPickSerial_.Tus_ser_id = serialid;
                _reptPickSerial_.Tus_serial_id = serialid.ToString();
                _reptPickSerial_.Tus_unit_cost = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_job_no = "";
                _reptPickSerial_.Tus_job_line = lineno;
                _reptPickSerial_.Tus_orig_supp = txtSupplierCd.Text.Trim();
                Tempserial_list.Add(_reptPickSerial_);
            }

            //var serialLine = (from _ls in ScanItemList
            //                  where _ls.Itri_itm_cd == _item
            //                  select _ls.Itri_line_no).Max();

            var serialLine = lineno;

            Int32 user_seq_num = -1;

            foreach (ReptPickSerials serial in Tempserial_list)
            {
                #region PRN
                if (string.IsNullOrEmpty(txtUserSeqNo.Text.Trim()))
                {
                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", Session["UserCompanyCode"].ToString(), _Seqno, 0);
                }
                else
                {
                    user_seq_num = Convert.ToInt32(txtUserSeqNo.Text.Trim());
                }

                if (user_seq_num != -1)
                {
                    generated_seq = user_seq_num;
                }
                else
                {
                    generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString());
                    ReptPickHeader RPH = new ReptPickHeader();
                    RPH.Tuh_doc_tp = "ADJ";
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
                    Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_seq_no = generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_doc_no = txtUserSeqNo.Text.Trim();
                    _reptPickSerial_.Tus_base_doc_no = txtfindreq.Text.Trim();
                    _reptPickSerial_.Tus_itm_cd = _item;
                    _reptPickSerial_.Tus_itm_line = Convert.ToInt32(serialLine);
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine);
                    _reptPickSerial_.Tus_bin = bin;
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_job_no = "";
                    _reptPickSerial_.Tus_job_line = lineno;
                    _reptPickSerial_.Tus_itm_stus = _stus;
                    _reptPickSerial_.Tus_ser_1 = _Serial;
                    _reptPickSerial_.Tus_ser_id = serialid;
                    _reptPickSerial_.Tus_serial_id = serialid.ToString();
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
                    _reptPickSerial_.Tus_orig_supp = txtSupplierCd.Text.Trim();

                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                    rowCount++;
                }
                else
                {
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_seq_no = generated_seq;
                    _reptPickSerial_.Tus_doc_no = txtUserSeqNo.Text.Trim();
                    _reptPickSerial_.Tus_base_doc_no = txtfindreq.Text.Trim();
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine); 
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_bin = bin;
                    _reptPickSerial_.Tus_itm_cd = serial.Tus_itm_cd;
                    _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
                    _reptPickSerial_.Tus_ser_1 = _Serial;
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = serialid;
                    _reptPickSerial_.Tus_serial_id = serialid.ToString();
                    _reptPickSerial_.Tus_unit_cost = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_job_no = "";
                    _reptPickSerial_.Tus_job_line = lineno;
                    _reptPickSerial_.Tus_orig_supp = txtSupplierCd.Text.Trim();

                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                }
                
                #endregion
            }

        }

        protected void lbtntempsave_Click(object sender, EventArgs e)
        {
            try 
            {
                if (txtTempSavelconformmessageValue.Value == "Yes")
                {
                    Session["DocType"] = "TempDoc";
                    Process(true);
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

        

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            try
            {
                Session["POPUP_CRN_RETURN"] = null;
                Session["GEN_SEQ"] = null;
                Session["SCAN_ITEM_LIST"] = null;
                MpDelivery.Hide();
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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

        protected void ddlloadingbay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MPPDA.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
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
        private void SaveData()
        {
            try
            {
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


                string pdaloc = (string)Session["PDA_LOCATION"];

                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];

                foreach (GridViewRow row in this.gvsearchdata.Rows)
                {
                    Label OutwardNo = (Label)row.FindControl("lbldocno");
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");

                    if (chkselect.Checked == true)
                    {
                        DataTable dtdoccheck1 = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo.Text.Trim(), "AOD", 1, Session["UserCompanyCode"].ToString());

                        if (dtdoccheck1.Rows.Count > 1)
                        {
                            grdItems.DataSource = null;
                            grdItems.DataBind();
                            grdSerial.DataSource = null;
                            grdSerial.DataBind();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + OutwardNo.Text + " !!!')", true);
                            return;
                        }

                        DataTable _headerchk2 = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), OutwardNo.Text.Trim());
                        if (_headerchk2 != null && _headerchk2.Rows.Count > 0)
                        {
                            string _headerUser = _headerchk2.Rows[0].Field<string>("tuh_usr_id");
                            string _scanDate = Convert.ToString(_headerchk2.Rows[0].Field<DateTime>("tuh_cre_dt"));

                            if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                                {
                                    string msg2 = "Document " + OutwardNo.Text + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                                    return;
                                }
                        }

                        Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(Session["UserCompanyCode"].ToString(), OutwardNo.Text.Trim());
                        List<ReptPickSerials> PickSerialsPOPUP = CHNLSVC.Inventory.GetTempPickSerialBySeqNo(_seq, OutwardNo.Text.Trim(), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                        Session["POPUPSERIALS"] = PickSerialsPOPUP;

                        if (pdaloc == "1")
                        {
                            DataTable dtupdatedlb = CHNLSVC.Inventory.CheckHasLoadingBay(Session["UserCompanyCode"].ToString(), OutwardNo.Text.Trim(), Session["UserDefLoca"].ToString());

                            if (dtupdatedlb.Rows.Count == 0)
                            {
                                //Int32 deltempser = CHNLSVC.Inventory.DeleteRepSerials(_seq, OutwardNo.Text.Trim());
                                grdSerial.DataSource = null;
                                grdSerial.DataBind();
                                Session["SERIALLIST"] = null;
                            }
                            else
                            {

                                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo.Text.Trim(), "AOD", 1, Session["UserCompanyCode"].ToString());

                                if (dtdoccheck.Rows.Count > 1)
                                {
                                    grdItems.DataSource = null;
                                    grdItems.DataBind();
                                    grdSerial.DataSource = null;
                                    grdSerial.DataBind();
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('Multiple documents found for no " + OutwardNo + " !!!')", true);
                                    return;
                                }

                                DataTable _headerchk = CHNLSVC.Inventory.GetPickHeaderByDocument(Session["UserCompanyCode"].ToString(), OutwardNo.Text.Trim());
                                if (_headerchk != null && _headerchk.Rows.Count > 0)
                                {
                                    string _headerUser = _headerchk.Rows[0].Field<string>("tuh_usr_id");
                                    string _scanDate = Convert.ToString(_headerchk.Rows[0].Field<DateTime>("tuh_cre_dt"));

                                    if (!string.IsNullOrEmpty(_headerUser)) if (Session["UserID"].ToString().Trim() != _headerUser.Trim())
                                        {
                                            string msg2 = "Document " + OutwardNo.Text.Trim() + " had been already scanned by the user " + _headerUser + " on " + _scanDate;
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                                            return;
                                        }
                                }

                            }
                        }
                    }
                }


                foreach (GridViewRow row in this.gvsearchdata.Rows)
                {
                    Label OutwardNo = (Label)row.FindControl("lbldocno");
                    CheckBox chkselect = (CheckBox)row.FindControl("chkselect");

                    Int32 _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "AOD", 1, Session["UserCompanyCode"].ToString());
                    _userid = (string)Session["UserID"];
                    Int32 val = 0;

                    #region Header

                    DataTable dtmyref = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo.Text.Trim(), "AOD", 1, Session["UserCompanyCode"].ToString());

                    if (dtmyref.Rows.Count > 0)
                    {
                        foreach (DataRow ddr in dtmyref.Rows)
                        {
                            string seqNo = ddr["tuh_usrseq_no"].ToString();
                            _userSeqNo = Convert.ToInt32(seqNo);
                        }
                    }

                    DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(_userSeqNo);

                    if (dtchkitm.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already sent to PDA or has alread processed !!!')", true);
                        return;
                    }

                    if (dtmyref.Rows.Count == 0)
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                        _inputReptPickHeader.Tuh_usr_id = _userid;
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                        _inputReptPickHeader.Tuh_doc_tp = "AOD";
                        _inputReptPickHeader.Tuh_direct = true;
                        _inputReptPickHeader.Tuh_ischek_itmstus = false;
                        _inputReptPickHeader.Tuh_ischek_simitm = false;
                        _inputReptPickHeader.Tuh_ischek_reqqty = false;
                        _inputReptPickHeader.Tuh_doc_no = OutwardNo.Text.Trim();
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_wh_com = warehousecom;
                        _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                        val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                        successItemsPDA.Add(1);

                        if (Convert.ToInt32(val) == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            CHNLSVC.CloseChannel();
                            return;
                        }
                    }
                    else
                    {
                        ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        _inputReptPickHeader.Tuh_doc_no = OutwardNo.Text.Trim();
                        _inputReptPickHeader.Tuh_doc_tp = "AOD";
                        _inputReptPickHeader.Tuh_direct = true;
                        _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickHeader.Tuh_wh_com = warehousecom;
                        _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                        val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);
                        successItemsPDA.Add(2);

                        if (Convert.ToInt32(val) == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            CHNLSVC.CloseChannel();
                            return;
                        }
                    }


                    #endregion

                    //#region Items

                    //Int32 rownumber = 0;
                    //Int32 rowvalline = 0;

                    //foreach (GridViewRow row in gvItem.Rows)
                    //{
                    //    Label lblitem = (Label)row.FindControl("lblitem");
                    //    Label lblstatus = (Label)row.FindControl("lblstatus");
                    //    Label lblqty = (Label)row.FindControl("lblqty");

                    //    ReptPickItems _items = new ReptPickItems();

                    //    _items.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                    //    _items.Tui_req_itm_cd = lblitem.Text.Trim();
                    //    _items.Tui_req_itm_stus = lblstatus.Text.Trim();
                    //    _items.Tui_pic_itm_qty = 0;
                    //    _items.Tui_req_itm_qty = Convert.ToDecimal(lblqty.Text.Trim());
                    //    _items.Tui_pic_itm_cd = rownumber.ToString();
                    //    _items.Tui_pic_itm_stus = string.Empty;
                    //    _items.Tui_grn = string.Empty;
                    //    _items.Tui_batch = string.Empty;
                    //    _items.Tui_sup = string.Empty;

                    //    valitem = CHNLSVC.Inventory.UpdatePickItem(_items);

                    //    DataTable dtrownum = CHNLSVC.Inventory.LoadCurrentRowNumber(Convert.ToInt32(_userSeqNo), warehousecom, warehouseloc, ddlloadingbay.SelectedValue);
                    //    foreach (DataRow ddrrownum in dtrownum.Rows)
                    //    {
                    //        rownumber = Convert.ToInt32(ddrrownum["RowCount"].ToString());
                    //    }

                    //    ReptPickItems _itemslines = new ReptPickItems();

                    //    _itemslines.Tui_pic_itm_cd = rownumber.ToString();
                    //    _itemslines.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                    //    _itemslines.Tui_req_itm_cd = lblitem.Text.Trim();
                    //    _itemslines.Tui_req_itm_stus = lblstatus.Text.Trim();

                    //    rowvalline = CHNLSVC.Inventory.UpdatePickItemLine(_itemslines);

                    //    if (Convert.ToInt32(valitem) == -1)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    //        CHNLSVC.CloseChannel();
                    //        return;
                    //    }

                    //    if (Convert.ToInt32(rowvalline) == -1)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    //        CHNLSVC.CloseChannel();
                    //        return;
                    //    }
                    //}
                    //#endregion
                }


                if ((successItemsPDA.Contains(1)) && (successItemsPDA.Contains(2)))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Some documents were successfully sent & some were successfully updated !!!')", true);
                }
                else if (successItemsPDA.Contains(1))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully sent !!!')", true);
                }
                else if (successItemsPDA.Contains(2))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully updated !!!')", true);
                }

                Clear();
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

        protected void chkpda_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkpda.Checked)
                {
                    if (gvsearchdata.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Load consignement receive notes !!!')", true);
                        lbtnfindall.Focus();
                        chkpda.Checked = false;
                        return;
                    }

                    List<Int32> checkcount = new List<int>();
                    foreach (GridViewRow row in this.gvsearchdata.Rows)
                    {
                        CheckBox chkselect = (CheckBox)row.FindControl("chkselect");

                        if (chkselect.Checked)
                        {
                            checkcount.Add(row.RowIndex);
                        }
                    }

                    if (checkcount.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select at least one document number !!!')", true);
                        chkpda.Checked = false;
                        return;
                    }


                    if (chkpda.Checked == true)
                    {
                        txtdocname.Text = "Selected All Documents";
                        txtdocname.ReadOnly = true;
                        MPPDA.Show();
                    }
                    else
                    {
                        MPPDA.Hide();
                        txtdocname.Text = string.Empty;
                        ddlloadingbay.SelectedIndex = 0;
                        txtdocname.ReadOnly = false;
                    }
                }
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

        protected void txtfindreq_TextChanged(object sender, EventArgs e)
        {
            try
            {
                InventoryHeader _invHdr = new InventoryHeader();

                _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtfindreq.Text.Trim());

                if (_invHdr == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid request number !!!')", true);
                    return;
                }
                else
                {
                    DataTable dtsearchdata = CHNLSVC.Inventory.SearchCRNDoc(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtfindreq.Text.Trim(), Convert.ToDateTime(txtfromdate.Text), Convert.ToDateTime(txttodate.Text), txtSupplierCd.Text.Trim());

                    gvsearchdata.DataSource = null;
                    gvsearchdata.DataBind();

                    gvsearchdata.DataSource = dtsearchdata;
                    gvsearchdata.DataBind();
                }
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

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.gvsearchdata.Rows)
            {
                CheckBox chkHeader = (CheckBox)gvsearchdata.HeaderRow.FindControl("chkHeader");
                CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                if (chkHeader.Checked ==true)
                {
                    chkselect.Checked = true;
                }
                else
                {
                    chkselect.Checked = false;
                }
            }
        }

        protected void chktemp_CheckedChanged(object sender, EventArgs e)
        {
            UserDPopoup.Hide();
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            Session["Doc"] = null;
            Session["TempDoc"] = null;
            UserDPopoup.Hide();
        }

        protected void lbtnadditem_Click(object sender, EventArgs e)
        {
            //try
            //{
            Session["POPUP_CRN_RETURN"] = "Yes";

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            string _itamcode = string.Empty;

            if (row != null)
            {
                _itamcode = (row.FindControl("lblitri_itm_cd") as Label).Text;
            }

            ucOutScan.TXTItemCode.Text = _itamcode;
            ucOutScan.ItemCodeChange();
           

            Session["GEN_SEQ"] = txtUserSeqNo.Text.Trim();
            Session["CRN_SUPP"] = txtSupplierCd.Text.Trim();

            while (ucOutScan.DDLStatus.Items.Count > 0)
            {
                ucOutScan.DDLStatus.Items.RemoveAt(0);
            }
            ucOutScan.DDLStatus.Items.Add(new ListItem("CONSIGNMENT", "CONS"));
            Session["SupplierCode"] = txtSupplierCd.Text;
            List<InventoryRequestItem> _ItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
            Session["SCAN_ITEM_LIST"] = _ItemList;
            ucOutScan.isConsReturnNote = true;
            ucOutScan.ReqSupplier = true;
            ucOutScan.doc_tp = "CRN";
            int _direction = 0;
            if (ddlAdjTypeSearch.Text == "ADJ+") _direction = 1;
            ucOutScan.direction = _direction;
            ucOutScan.DOC_No =string.IsNullOrEmpty(txtDocumentNo.Text)?txtUserSeqNo.Text.Trim():txtDocumentNo.Text.Trim();
          //  ucOutScan.DDLStatus.Enabled = false;
            MpDelivery.Show();
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            //}
            //finally
            //{
            //    CHNLSVC.CloseChannel();
            //}
        }
        protected void lbtnAddSerial_Click(object sender, EventArgs e)
        {
            _popSerialList = new List<TempPopup>();
            if (txtSupplierCd.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select supplier code...');", true);
                return;
            }
            if (grdItems.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                string _itemStatus = (row.FindControl("lblstatus") as Label).Text;
                int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                Session["_lineNo"] = _lineNo;
                Session["_itemCode"] = _itemCode;
                Session["itemstatus"] = _itemStatus;
                if (string.IsNullOrEmpty(_itemCode)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                _popSerialList = CHNLSVC.Inventory.GetSupplierSerialForConsignmentReturn(SearchParams, "Item", _itemCode);
                #region MyRegion
                //DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, "Item", _itemCode);
                //Int32 rowIndex = 0;
                //DataTable _dt= new DataTable();

                //_dt.Columns.Add("Supplier");
                //_dt.Columns.Add("Serial1");
                //_dt.Columns.Add("Serial2");
                //_dt.Columns.Add("Serial3");
                //_dt.Columns.Add("Item Status");
                //_dt.Columns.Add("Item");
                //_dt.Columns.Add("inb_qty");
                //_dt.Columns.Add("ins_unit_cost");
                //_dt.Columns.Add("ins_itm_stus");
                //_dt.Columns.Add("Warranty No");
                //_dt.Columns.Add("GRN #");
                //_dt.Columns.Add("Doc Date");
                //_dt.Columns.Add("PO #");
               
                //foreach (DataRow item in _result.Rows)
                //{
                //    rowIndex++;
                //    if (item["Item Status"].ToString()=="CONS")
                //    {
                //        DataRow _dataRow = _dt.NewRow();
                //        _dataRow["Supplier"] = item["Supplier"].ToString();
                //        _dataRow["Serial1"] = item["Serial"].ToString();
                //        _dataRow["Serial2"] = item["Serial 2"].ToString();
                //        _dataRow["Serial3"] = item["Serial 3"].ToString();
                //        _dataRow["Item Status"] = item["Item Status"].ToString();
                //        _dataRow["Item"] = item["Item"].ToString();
                //        _dataRow["inb_qty"] = item["inb_qty"].ToString();
                //        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                //        _dataRow["ins_itm_stus"] = item["ins_itm_stus"].ToString();
                //        _dataRow["Warranty No"] = item["Warranty No"].ToString();
                //        _dataRow["GRN #"] = item["GRN #"].ToString();
                //        _dataRow["Doc Date"] = item["Doc Date"].ToString();
                //        _dataRow["PO #"] = item["PO #"].ToString();
                //        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                //        _dt.Rows.Add(_dataRow);
                //    }
                //}
                #endregion
                grdAdSearch.DataSource = _popSerialList;
                grdAdSearch.DataBind();
                btnAllItem.Visible = false;

                DataTable _dtDrop = new DataTable();
                _dtDrop.Columns.Add("Serial 1");
                _dtDrop.Columns.Add("Serial 2");
                BindUCtrlDDLData3(_dtDrop);
                lblAvalue.Text = "Serial";
                UserAdPopup.Show();
                _popSerSearch = true;
            }
        }

        protected void lbtnAddSerial_ClickOld(object sender, EventArgs e)
        {
            if (txtSupplierCd.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select supplier code...');", true);
                return;
            }
            if (grdItems.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                string _itemStatus = (row.FindControl("lblstatus") as Label).Text;
                int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                Session["_lineNo"] = _lineNo;
                Session["_itemCode"] = _itemCode;
                Session["itemstatus"] = _itemStatus;
                if (string.IsNullOrEmpty(_itemCode)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, "Item", _itemCode);
                Int32 rowIndex = 0;
                DataTable _dt = new DataTable();

                _dt.Columns.Add("Supplier");
                _dt.Columns.Add("Serial1");
                _dt.Columns.Add("Serial2");
                _dt.Columns.Add("Serial3");
                _dt.Columns.Add("Item Status");
                _dt.Columns.Add("Item");
                _dt.Columns.Add("inb_qty");
                _dt.Columns.Add("ins_unit_cost");
                _dt.Columns.Add("ins_itm_stus");
                _dt.Columns.Add("Warranty No");
                _dt.Columns.Add("GRN #");
                _dt.Columns.Add("Doc Date");
                _dt.Columns.Add("PO #");

                foreach (DataRow item in _result.Rows)
                {
                    rowIndex++;
                    if (item["Item Status"].ToString() == "CONS")
                    {
                        DataRow _dataRow = _dt.NewRow();
                        _dataRow["Supplier"] = item["Supplier"].ToString();
                        _dataRow["Serial1"] = item["Serial"].ToString();
                        _dataRow["Serial2"] = item["Serial 2"].ToString();
                        _dataRow["Serial3"] = item["Serial 3"].ToString();
                        _dataRow["Item Status"] = item["Item Status"].ToString();
                        _dataRow["Item"] = item["Item"].ToString();
                        _dataRow["inb_qty"] = item["inb_qty"].ToString();
                        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                        _dataRow["ins_itm_stus"] = item["ins_itm_stus"].ToString();
                        _dataRow["Warranty No"] = item["Warranty No"].ToString();
                        _dataRow["GRN #"] = item["GRN #"].ToString();
                        _dataRow["Doc Date"] = item["Doc Date"].ToString();
                        _dataRow["PO #"] = item["PO #"].ToString();
                        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                        _dt.Rows.Add(_dataRow);
                    }
                }
                grdAdSearch.DataSource = _dt;
                grdAdSearch.DataBind();
                btnAllItem.Visible = false;

                DataTable _dtDrop = new DataTable();
                _dtDrop.Columns.Add("Serial 1");
                _dtDrop.Columns.Add("Serial 2");
                BindUCtrlDDLData3(_dtDrop);
                lblAvalue.Text = "Serial";
                UserAdPopup.Show();
            }
        }

        public void BindUCtrlDDLData3(DataTable _dataSource)
        {
            this.ddlSearchbykeyA.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyA.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyA.SelectedIndex = 0;
        }
        protected void btnAClose_Click(object sender, EventArgs e)
        {
                UserAdPopup.Hide();
                _popSerSearch = false;
        }

        protected void txtSearchbywordA_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSearchA_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                lblAvalue.Text = "Adv";

                UserAdPopup.Show();
                return;
            }
            if (lblAvalue.Text == "Serial")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                _popSerialList = CHNLSVC.Inventory.GetSupplierSerialForConsignmentReturn(SearchParams, "Item", Session["_itemCode"].ToString());
                //DataTable _result = CHNLSVC.Inventory.GetSupplierSerialForConsignmentReturn(SearchParams, "Item", Session["_itemCode"].ToString());
                //_result.Columns["Serial 2"].ColumnName = "Serial2";
                //DataView dv = new DataView(_result);
                grdAdSearch.DataSource = _popSerialList;
                string _search = ddlSearchbykeyA.Text == "Serial 1" ? "Serial" : "Serial2";
                if (_popSerialList.Count > 0)
                {
                    if (_search == "Serial")
                    {
                        _popSerialList = _popSerialList.Where(c => c.Serial_1 == txtSearchbywordA.Text.Trim()).ToList();
                    }
                    if (_search == "Serial2")
                    {
                        _popSerialList = _popSerialList.Where(c => c.Serial_2 == txtSearchbywordA.Text.Trim()).ToList();
                    }
                }
                if (_popSerialList.Count > 0)
                {
                    grdAdSearch.DataSource = _popSerialList;
                }
                grdAdSearch.DataBind();
                UserAdPopup.Show();
                //dv.RowFilter = "" + _search + " LIKE '%" + txtSearchbywordA.Text + "%'";
                ////if (dv.Count > 0)
                ////{
                //    _result = dv.ToTable();
                ////}
                
                //Int32 rowIndex = 0;
                //DataTable _dt = new DataTable();

                //_dt.Columns.Add("Supplier");
                //_dt.Columns.Add("Serial1");
                //_dt.Columns.Add("Serial2");
                //_dt.Columns.Add("Serial3");
                //_dt.Columns.Add("Item Status");
                //_dt.Columns.Add("Item");
                //_dt.Columns.Add("inb_qty");
                //_dt.Columns.Add("ins_unit_cost");
                //_dt.Columns.Add("ins_itm_stus");
                //_dt.Columns.Add("Warranty No");
                //_dt.Columns.Add("GRN #");
                //_dt.Columns.Add("Doc Date");
                //_dt.Columns.Add("PO #");

                //foreach (DataRow item in _result.Rows)
                //{
                //    rowIndex++;
                //    if (item["Item Status"].ToString() == "CONS")
                //    {
                //        DataRow _dataRow = _dt.NewRow();
                //        _dataRow["Supplier"] = item["Supplier"].ToString();
                //        _dataRow["Serial1"] = item["Serial"].ToString();
                //        _dataRow["Serial2"] = item["Serial2"].ToString();
                //        _dataRow["Serial3"] = item["Serial 3"].ToString();
                //        _dataRow["Item Status"] = item["Item Status"].ToString();
                //        _dataRow["Item"] = item["Item"].ToString();
                //        _dataRow["inb_qty"] = item["inb_qty"].ToString();
                //        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                //        _dataRow["ins_itm_stus"] = item["ins_itm_stus"].ToString();
                //        _dataRow["Warranty No"] = item["Warranty No"].ToString();
                //        _dataRow["GRN #"] = item["GRN #"].ToString();
                //        _dataRow["Doc Date"] = item["Doc Date"].ToString();
                //        _dataRow["PO #"] = item["PO #"].ToString();
                //        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                //        _dt.Rows.Add(_dataRow);
                //    }
                //}
                //grdAdSearch.DataSource = _dt;
                //grdAdSearch.DataBind();
                //UserAdPopup.Show();
            }
        }

        protected void btnAllItem_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdvanceAddItem_Click(object sender, EventArgs e)
        {
            if (txtSupplierCd.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select supplier code...');", true);
                return;
            }
            bool _IsItemAvailable = false;

            decimal selectedQty = 0;
            decimal itemReturnQty = 0;
            decimal itemPickQty = 0;
            string _itemCode = "";
            //foreach (GridViewRow dgvr in grdAdSearch.Rows)
            //{
            //    CheckBox chkSelect = (CheckBox)dgvr.FindControl("selectchk");
            //    Label Col_ItemCode = (Label)dgvr.FindControl("Col_ItemCode");
            //    if (chkSelect.Checked)
            //    {
            //       selectedQty= selectedQty + 1;
            //        _itemCode = Col_ItemCode.Text;
            //    }
            //}
            if (_popSerialList.Count > 0)
            {
                selectedQty = _popSerialList.Where(c => c.Is_select == true).ToList().Count();
                _itemCode = _popSerialList[0].Item;
            }
            if (grdItems.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdItems.Rows)
                {
                    Label lblitri_itm_cd = row.FindControl("lblitri_itm_cd") as Label;
                    Label lblapqtygrd = row.FindControl("lblapqtygrd") as Label;
                    Label lblitri_qty = row.FindControl("lblitri_qty") as Label;//pick
                    if (lblitri_itm_cd.Text == _itemCode)
                    {
                        itemReturnQty = Convert.ToDecimal(lblapqtygrd.Text);
                        itemPickQty = Convert.ToDecimal(lblitri_qty.Text);
                        break;
                    }
                }
            }
            if ((selectedQty + itemPickQty) > itemReturnQty && !chktemp.Checked)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You cannot exceed the return qty ...!');", true);
                return;
            }

            // Add Item ...
            bool select = false;
            var _selectSerialList = _popSerialList.Where(c => c.Is_select == true).ToList();
            if (_selectSerialList != null)
            {
                if (_selectSerialList.Count > 0)
                {
                    select = true;
                    foreach (var _ser in _selectSerialList)
                    {
                        string _status = "CONS";
                        string _item  = _ser.Item;
                        bool _balnce = CheckItem(_item);
                        string _qty = "1";
                        if (_balnce == true)
                        {
                            if (Session["Advan"] == null)
                            {
                                _qty = "1";
                            }
                            AddSerials(_item, _ser.Serial_1, txtUserSeqNo.Text, _status, _qty);
                        }
                    }
                }
            }
            //foreach (GridViewRow dgvr in grdAdSearch.Rows)
            //{
            //    CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
            //    if (chk.Checked)
            //    {
            //        select = true;
            //        string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
            //        string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
            //        string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
            //        string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
            //        string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
            //        // txtOtherRef.Text = (dgvr.FindControl("Col_Supplier") as Label).Text;
            //        // txtOtherRef.Enabled = false;

            //        _status = "CONS";
            //        bool _balnce = CheckItem(_item);
            //        if (_balnce == true)
            //        {
            //            //DataTable _chkItem = CHNLSVC.Inventory.CheckItemTo_PRN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status,"ADJ", txtSupplierCd.Text);
            //            //if (_chkItem.Rows.Count > 0)
            //            //{
            //            //AddItem(_item, _UnitCost, _status, _qty, txtUserSeqNo.Text, _serial);
            //          //  AddSerials(_item, _serial, txtUserSeqNo.Text.Trim(), _status, _qty);

            //            _IsItemAvailable = true;
            //            //}
            //            // else
            //            // {
            //            //     ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You cannot return this item. Please check the item again...!');", true);
            //            //     return;
            //            // }
            //        }

            //    }
            //}
            //if (_IsItemAvailable == true)
            //{
            //    // Add serial ...Save TEMP_PICK_SER
            //    foreach (GridViewRow dgvr in grdAdSearch.Rows)
            //    {
            //        CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
            //        if (chk.Checked)
            //        {
            //            string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
            //            string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
            //            string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
            //            string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
            //            string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
            //            bool _balnce = CheckItem(_item);
            //            if (_balnce == true)
            //            {
            //                if (Session["Advan"] == null)
            //                {
            //                    _qty = "1";
            //                }
            //                AddSerials(_item, _serial,txtUserSeqNo.Text, _status, _qty);
            //                // Session["Advan"] = "";
            //            }

            //        }
            //    }
            //}
            if (select)
            {
                UserAdPopup.Hide();
                //LoadStatusDescriptionItem();
                //LoadStatusDescriptionSerial();
                LoadItems(txtUserSeqNo.Text);
            }
            else 
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a serial !!!');", true);
                UserAdPopup.Show(); 
            }
            
        }

        protected void AddItem(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial)
        {
            try
            {
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                if (ScanItemList == null)
                {
                    ScanItemList = ucOutScan.ScanItemList;
                }

                if (ScanItemList != null)
                {
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == _item && _ls.Itri_itm_stus == _status
                                         select _ls;

                        if (_duplicate != null)
                        {
                            if (_duplicate.Count() > 0)
                            {
                                ViewState["ScanItemList"] = ScanItemList;
                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected item already available.');", true);
                                return;
                            }
                            else
                            {
                                var _maxline = (from _ls in ScanItemList
                                                select _ls.Itri_line_no).Max();
                                _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                                _itm.Itri_itm_cd = _item;
                                _itm.Itri_itm_stus = _status;
                                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                                Session["_lineNo"] = _itm.Itri_line_no;
                                _itm.Itri_qty = 0;
                                _itm.Mi_longdesc = _itms.Mi_longdesc;
                                _itm.Mi_model = _itms.Mi_model;
                                _itm.Mi_brand = _itms.Mi_brand;
                                //Added by Prabhath on 17/12/2013 ************* start **************
                                _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                                //Added by Prabhath on 17/12/2013 ************* end **************
                                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                                if (oItemStaus != null && oItemStaus.Count > 0)
                                {
                                    _itm.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _itm.Itri_itm_stus).Mis_desc;
                                }
                                ScanItemList.Add(_itm);
                            }
                        }
                    }
                    else
                    {
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = 1;
                        Session["_lineNo"] = 1;
                        _itm.Itri_qty = 0;
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        //Added by Prabhath on 17/12/2013 ************* start **************
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                        //Added by Prabhath on 17/12/2013 ************* end **************
                        List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                        oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            _itm.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _itm.Itri_itm_stus).Mis_desc;
                        }
                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }

                }
                else
                {
                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = 1;
                    Session["_lineNo"] = 1;
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    //Added by Prabhath on 17/12/2013 ************* start **************
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    //Added by Prabhath on 17/12/2013 ************* end **************
                    List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                    oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        _itm.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _itm.Itri_itm_stus).Mis_desc;
                    }
                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }



                if (string.IsNullOrEmpty(_UserSeqNo))
                {
                    GenerateNewUserSeqNo();
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _saveonly.Add(_reptitm);
                }
              int res=   CHNLSVC.Inventory.SavePickedItems(_saveonly);


                grdItems.DataSource = null;
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                ViewState["ScanItemList"] = ScanItemList;
                ucOutScan.userSeqNo = txtUserSeqNo.Text;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);

                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void AddSerials(string _item, string _Serial, string _Seqno, string _status, string _qty)
        {
            Int32 generated_seq = -1;
            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            Int32 user_seq_num = -1;
             user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("CRN", Session["UserCompanyCode"].ToString(), _Seqno, 0);
             if (user_seq_num == -1)
             {
                 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("CRN", Session["UserCompanyCode"].ToString(), txtDocumentNo.Text, 0);
             }
            if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
            {
                generated_seq = user_seq_num;
            }
            else
            {
                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                //assign user_seqno
                ReptPickHeader RPH = new ReptPickHeader();
                RPH.Tuh_doc_tp = "ADJ";
                RPH.Tuh_cre_dt = DateTime.Today;
                RPH.Tuh_ischek_itmstus = true;
                RPH.Tuh_ischek_reqqty = true;
                RPH.Tuh_ischek_simitm = true;
                RPH.Tuh_session_id = Session["SessionID"].ToString();
                RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                RPH.Tuh_usr_id = Session["UserID"].ToString();
                RPH.Tuh_usrseq_no = generated_seq;

                RPH.Tuh_direct = false;
                RPH.Tuh_doc_no = generated_seq.ToString();
                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

            }
            if (msitem.Mi_is_ser1 == 1)
            {
                List<ReptPickSerials> Tempserial_list = CHNLSVC.Inventory.Search_serial_for_consignment(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, ddlStatus.SelectedValue.ToString(), txtSupplierCd.Text.ToString(), _Serial);

                Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial).ToList();

                if (Tempserial_list != null)
                {
                    if (Tempserial_list.Count == 1)
                    {
                        foreach (ReptPickSerials serial in Tempserial_list)
                        {
                            int rowCount = 0;
                            //-------------
                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));
                            //Update_inrser_INS_AVAILABLE
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_usrseq_no = generated_seq;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = _Seqno;
                            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(Session["_lineNo"]);//Convert.ToInt32(serial.Tus_itm_line);
                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_.Tus_job_no = serial.Tus_job_no;//JobNo;
                            _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                            _reptPickSerial_.Tus_exist_grnno = _reptPickSerial_.Tus_doc_no;
                            //enter row into TEMP_PICK_SER
                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                            rowCount++;
                            //isManualscan = true;
                        }
                    }
                }
                //if (ViewState["SerialList"] != null)
                //{
                //    serial_list = ViewState["SerialList"] as List<ReptPickSerials>;
                //    serial_list = serial_list.Concat(Tempserial_list).ToList();
                //    grdSerial.DataSource = serial_list;
                //    grdSerial.DataBind();
                //    ViewState["SerialList"] = serial_list;
                //    return;
                //}

            }
            else
            {


                List<ReptPickSerials> _serList2 = new List<ReptPickSerials>();
                _serList2 = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "CRN");
                if (_serList2 != null)
                {
                    if (_serList2.Count > 0)
                    {

                        var _filter = _serList2.SingleOrDefault(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _status);
                        if (_filter != null)
                        {
                            if (Session["GRNbase"] == "true")
                            {
                                return;
                            }
                            _filter.Tus_qty = _filter.Tus_qty + Convert.ToDecimal(_qty);
                            Int32 value = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                        }
                        else
                        {

                            ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                            _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                            _inputReptPickSerials.Tus_usrseq_no = generated_seq;
                            _inputReptPickSerials.Tus_doc_no = _Seqno;
                            _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                            _inputReptPickSerials.Tus_base_doc_no = _Seqno;
                            _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(Session["_lineNo"]);
                            _inputReptPickSerials.Tus_itm_desc = msitem.Mi_shortdesc;
                            _inputReptPickSerials.Tus_itm_model = msitem.Mi_model;
                            _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                            _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                            _inputReptPickSerials.Tus_bin =(string)Session["GlbDefaultBin"];
                            _inputReptPickSerials.Tus_itm_cd = _item;
                            _inputReptPickSerials.Tus_itm_stus = _status;
                            _inputReptPickSerials.Tus_qty = Convert.ToDecimal(_qty);
                            _inputReptPickSerials.Tus_ser_1 = "N/A";
                            _inputReptPickSerials.Tus_ser_2 = "N/A";
                            _inputReptPickSerials.Tus_ser_3 = "N/A";
                            _inputReptPickSerials.Tus_ser_4 = "N/A";
                            _inputReptPickSerials.Tus_ser_id = 0;
                            _inputReptPickSerials.Tus_serial_id = "0";
                            _inputReptPickSerials.Tus_unit_cost = 0;
                            _inputReptPickSerials.Tus_unit_price = 0;
                            _inputReptPickSerials.Tus_unit_price = 0;
                            _inputReptPickSerials.Tus_job_no = "";
                            _inputReptPickSerials.Tus_job_line = 0;
                            //enter row into TEMP_PICK_SER
                            Int32 value = CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                        }
                    }
                    else
                    {


                        ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                        _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                        _inputReptPickSerials.Tus_usrseq_no = generated_seq;
                        _inputReptPickSerials.Tus_doc_no = _Seqno;
                        _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                        _inputReptPickSerials.Tus_base_doc_no = _Seqno;
                        _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(Session["_lineNo"]);
                        _inputReptPickSerials.Tus_itm_desc = msitem.Mi_shortdesc;
                        _inputReptPickSerials.Tus_itm_model = msitem.Mi_model;
                        _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                        _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickSerials.Tus_bin = BaseCls.GlbDefaultBin;
                        _inputReptPickSerials.Tus_itm_cd = _item;
                        _inputReptPickSerials.Tus_itm_stus = _status;
                        _inputReptPickSerials.Tus_qty = Convert.ToDecimal(_qty);
                        _inputReptPickSerials.Tus_ser_1 = "N/A";
                        _inputReptPickSerials.Tus_ser_2 = "N/A";
                        _inputReptPickSerials.Tus_ser_3 = "N/A";
                        _inputReptPickSerials.Tus_ser_4 = "N/A";
                        _inputReptPickSerials.Tus_ser_id = 0;
                        _inputReptPickSerials.Tus_serial_id = "0";
                        _inputReptPickSerials.Tus_unit_cost = 0;
                        _inputReptPickSerials.Tus_unit_price = 0;
                        _inputReptPickSerials.Tus_unit_price = 0;
                        _inputReptPickSerials.Tus_job_no = "";
                        _inputReptPickSerials.Tus_job_line = 0;
                        //enter row into TEMP_PICK_SER
                        Int32 value = CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                        //Tempserial_list2.Add(_inputReptPickSerials);
                        //if (ViewState["SerialList"] != null)
                        //{
                        //    serial_list = ViewState["SerialList"] as List<ReptPickSerials>;
                        //    serial_list = serial_list.Concat(Tempserial_list2).ToList();
                        //    grdSerial.DataSource = serial_list;
                        //    grdSerial.DataBind();
                        //    ViewState["SerialList"] = serial_list;
                        //    return;
                        //}
                        //grdSerial.DataSource = Tempserial_list2;
                        //grdSerial.DataBind();

                    }
                }
                else
                {
                    ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                    _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    _inputReptPickSerials.Tus_usrseq_no = generated_seq;
                    _inputReptPickSerials.Tus_doc_no = _Seqno;
                    _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    _inputReptPickSerials.Tus_base_doc_no = _Seqno;
                    _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(Session["_lineNo"]);
                    _inputReptPickSerials.Tus_itm_desc = msitem.Mi_shortdesc;
                    _inputReptPickSerials.Tus_itm_model = msitem.Mi_model;
                    _inputReptPickSerials.Tus_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickSerials.Tus_bin = BaseCls.GlbDefaultBin;
                    _inputReptPickSerials.Tus_itm_cd = _item;
                    _inputReptPickSerials.Tus_itm_stus = _status;
                    _inputReptPickSerials.Tus_qty = Convert.ToDecimal(_qty);
                    _inputReptPickSerials.Tus_ser_1 = "N/A";
                    _inputReptPickSerials.Tus_ser_2 = "N/A";
                    _inputReptPickSerials.Tus_ser_3 = "N/A";
                    _inputReptPickSerials.Tus_ser_4 = "N/A";
                    _inputReptPickSerials.Tus_ser_id = 0;
                    _inputReptPickSerials.Tus_serial_id = "0";
                    _inputReptPickSerials.Tus_unit_cost = 0;
                    _inputReptPickSerials.Tus_unit_price = 0;
                    _inputReptPickSerials.Tus_unit_price = 0;
                    _inputReptPickSerials.Tus_job_no = "";
                    _inputReptPickSerials.Tus_job_line = 0;
                    //enter row into TEMP_PICK_SER
                    Int32 value = CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                }

            }
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            //_serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "CRN");
            //ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
           // LoadItems(string.IsNullOrEmpty(txtDocumentNo.Text) ? txtUserSeqNo.Text : txtDocumentNo.Text);
            //if (_serList != null)
            //{
            //    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Count() });
            //    foreach (var itm in _scanItems)
            //    {
            //        foreach (InventoryRequestItem dr in ScanItemList)
            //        {
            //            if ((itm.Peo.Tus_itm_cd == dr.Itri_itm_cd) && (itm.Peo.Tus_itm_stus == dr.Itri_itm_stus))
            //            {
            //                if (itm.Peo.Tus_qty > 1)
            //                {
            //                    dr.Itri_bqty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
            //                   // dr.Itri_app_qty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
            //                }
            //                else
            //                {
            //                    dr.Itri_bqty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
            //                   // dr.Itri_app_qty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty   
            //                }


            //            }
            //        }

            //    }

            //}
            //grdItems.DataSource = ScanItemList;
            //grdItems.DataBind();
            //ViewState["ScanItemList"] = ScanItemList;
            //ucOutScan.ScanItemList = ScanItemList;

            //List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
            //oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            //if (_serList != null)
            //{
            //    foreach (ReptPickSerials itemSer in _serList)
            //    {
            //        if (oItemStaus != null && oItemStaus.Count > 0)
            //        {
            //            itemSer.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == itemSer.Tus_itm_stus).Mis_desc;
            //        }
            //    }
            //}
            //grdSerial.DataSource = _serList;
            //grdSerial.DataBind();
            //ViewState["SerialList"] = _serList;
            //ucOutScan.PickSerial = _serList;
        }
        protected bool CheckItem(string _item)
        {
            try
            {

                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty);

                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No stock balance available...!');", true);
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }


        }

        protected void grdAdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdAdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAdSearch.PageIndex = e.NewPageIndex;

            if (lblAvalue.Text == "Adv-ser")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                Int32 rowIndex = 0;
                DataTable _dt = new DataTable();

                _dt.Columns.Add("Supplier");
                _dt.Columns.Add("Serial1");
                _dt.Columns.Add("Serial2");
                _dt.Columns.Add("Serial3");
                _dt.Columns.Add("Item Status");
                _dt.Columns.Add("Item");
                _dt.Columns.Add("inb_qty");
                _dt.Columns.Add("ins_unit_cost");
                _dt.Columns.Add("ins_itm_stus");
                _dt.Columns.Add("Warranty No");
                _dt.Columns.Add("GRN #");
                _dt.Columns.Add("Doc Date");
                _dt.Columns.Add("PO #");

                foreach (DataRow item in _result.Rows)
                {
                    rowIndex++;
                    if (item["Item Status"].ToString() == "CONS")
                    {
                        DataRow _dataRow = _dt.NewRow();
                        _dataRow["Supplier"] = item["Supplier"].ToString();
                        _dataRow["Serial1"] = item["Serial"].ToString();
                        _dataRow["Serial2"] = item["Serial 2"].ToString();
                        _dataRow["Serial3"] = item["Serial 3"].ToString();
                        _dataRow["Item Status"] = item["Item Status"].ToString();
                        _dataRow["Item"] = item["Item"].ToString();
                        _dataRow["inb_qty"] = item["inb_qty"].ToString();
                        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                        _dataRow["ins_itm_stus"] = item["ins_itm_stus"].ToString();
                        _dataRow["Warranty No"] = item["Warranty No"].ToString();
                        _dataRow["GRN #"] = item["GRN #"].ToString();
                        _dataRow["Doc Date"] = item["Doc Date"].ToString();
                        _dataRow["PO #"] = item["PO #"].ToString();
                        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                        _dt.Rows.Add(_dataRow);
                    }
                }
                grdAdSearch.DataSource = _dt;
                grdAdSearch.DataBind();
                btnAllItem.Visible = false;

                DataTable _dtDrop = new DataTable();
                _dtDrop.Columns.Add("Serial 1");
                _dtDrop.Columns.Add("Serial 2");
                BindUCtrlDDLData3(_dtDrop);
                lblAvalue.Text = "Serial";
                UserAdPopup.Show();
            }
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                Int32 rowIndex = 0;
                DataTable _dt = new DataTable();

                _dt.Columns.Add("Supplier");
                _dt.Columns.Add("Serial1");
                _dt.Columns.Add("Serial2");
                _dt.Columns.Add("Serial3");
                _dt.Columns.Add("Item Status");
                _dt.Columns.Add("Item");
                _dt.Columns.Add("inb_qty");
                _dt.Columns.Add("ins_unit_cost");
                _dt.Columns.Add("ins_itm_stus");
                _dt.Columns.Add("Warranty No");
                _dt.Columns.Add("GRN #");
                _dt.Columns.Add("Doc Date");
                _dt.Columns.Add("PO #");

                foreach (DataRow item in _result.Rows)
                {
                    rowIndex++;
                    if (item["Item Status"].ToString() == "CONS")
                    {
                        DataRow _dataRow = _dt.NewRow();
                        _dataRow["Supplier"] = item["Supplier"].ToString();
                        _dataRow["Serial1"] = item["Serial"].ToString();
                        _dataRow["Serial2"] = item["Serial 2"].ToString();
                        _dataRow["Serial3"] = item["Serial 3"].ToString();
                        _dataRow["Item Status"] = item["Item Status"].ToString();
                        _dataRow["Item"] = item["Item"].ToString();
                        _dataRow["inb_qty"] = item["inb_qty"].ToString();
                        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                        _dataRow["ins_itm_stus"] = item["ins_itm_stus"].ToString();
                        _dataRow["Warranty No"] = item["Warranty No"].ToString();
                        _dataRow["GRN #"] = item["GRN #"].ToString();
                        _dataRow["Doc Date"] = item["Doc Date"].ToString();
                        _dataRow["PO #"] = item["PO #"].ToString();
                        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                        _dt.Rows.Add(_dataRow);
                    }
                }
                grdAdSearch.DataSource = _dt;
                grdAdSearch.DataBind();
                btnAllItem.Visible = false;

                DataTable _dtDrop = new DataTable();
                _dtDrop.Columns.Add("Serial 1");
                _dtDrop.Columns.Add("Serial 2");
                BindUCtrlDDLData3(_dtDrop);
                lblAvalue.Text = "Serial";
                UserAdPopup.Show();
                return;
            }
            if (lblAvalue.Text == "Serial")
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, "Item", Session["_itemCode"].ToString());
                Int32 rowIndex = 0;
                DataTable _dt = new DataTable();

                _dt.Columns.Add("Supplier");
                _dt.Columns.Add("Serial1");
                _dt.Columns.Add("Serial2");
                _dt.Columns.Add("Serial3");
                _dt.Columns.Add("Item Status");
                _dt.Columns.Add("Item");
                _dt.Columns.Add("inb_qty");
                _dt.Columns.Add("ins_unit_cost");
                _dt.Columns.Add("ins_itm_stus");
                _dt.Columns.Add("Warranty No");
                _dt.Columns.Add("GRN #");
                _dt.Columns.Add("Doc Date");
                _dt.Columns.Add("PO #");

                foreach (DataRow item in _result.Rows)
                {
                    rowIndex++;
                    if (item["Item Status"].ToString() == "CONS")
                    {
                        DataRow _dataRow = _dt.NewRow();
                        _dataRow["Supplier"] = item["Supplier"].ToString();
                        _dataRow["Serial1"] = item["Serial"].ToString();
                        _dataRow["Serial2"] = item["Serial 2"].ToString();
                        _dataRow["Serial3"] = item["Serial 3"].ToString();
                        _dataRow["Item Status"] = item["Item Status"].ToString();
                        _dataRow["Item"] = item["Item"].ToString();
                        _dataRow["inb_qty"] = item["inb_qty"].ToString();
                        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                        _dataRow["ins_itm_stus"] = item["ins_itm_stus"].ToString();
                        _dataRow["Warranty No"] = item["Warranty No"].ToString();
                        _dataRow["GRN #"] = item["GRN #"].ToString();
                        _dataRow["Doc Date"] = item["Doc Date"].ToString();
                        _dataRow["PO #"] = item["PO #"].ToString();
                        _dataRow["ins_unit_cost"] = item["ins_unit_cost"].ToString();
                        _dt.Rows.Add(_dataRow);
                    }
                }
                grdAdSearch.DataSource = _dt;
                grdAdSearch.DataBind();
                btnAllItem.Visible = false;

                DataTable _dtDrop = new DataTable();
                _dtDrop.Columns.Add("Serial 1");
                _dtDrop.Columns.Add("Serial 2");
                BindUCtrlDDLData3(_dtDrop);
                lblAvalue.Text = "Serial";
                UserAdPopup.Show();
            }
        }

        
        protected void grdResultD_SelectedIndexChangedOld(object sender, EventArgs e)
        {
            string Name = string.Empty;

            string entereddoc = (string)Session["ENTERED_DOC"];

            if (string.IsNullOrEmpty(entereddoc))
            {
                Name = grdResultD.SelectedRow.Cells[1].Text;
            }
            else
            {
                Name = entereddoc;
                Session["ENTERED_DOC"] = null;
            }

            if (lblvalue.Text == "Doc")
            {
                txtDocumentNo.Text = Name;
                lblvalue.Text = "";
                Session["Doc"] = "";
                Session["DocType"] = "Doc";

                LoadHeader(false);

                if (!string.IsNullOrEmpty(txtDocumentNo.Text))
                {
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    if (ddlAdjTypeSearch.Text == "ADJ+") _direction = 1;

                    #region Clear Data

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = _emptyItm;

                    lbtnsave.Enabled = true;
                    lbtnsave.CssClass = "buttonUndocolor";
                    lbtnsave.OnClientClick = "SaveConfirm();";

                    txtAdjSubType.Text = string.Empty;
                    lblSubTypeDesc.Text = "";
                    txtManualRef.Text = string.Empty;
                    txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtUserSeqNo.Text = string.Empty;
                    lblBatchNo.Text = string.Empty;
                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "ADJ")
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
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid document no !!!')", true);
                        txtDocumentNo.Text = string.Empty;
                        txtDocumentNo.Focus();
                        return;
                    }
                    else
                    {
                        txtSupplierCd.Enabled = false;
                        btnSearchSupp.Enabled = false;
                        lbtnsave.Enabled = false;
                        lbtnsave.CssClass = "buttoncolor";
                        lbtnsave.OnClientClick = "return Enable();";

                        //cmdPrint.Enabled = true;
                        //cmdPrint.CssClass = "buttonUndocolor";

                        txtAdjSubType.Text = _invHdr.Ith_sub_tp;
                        txtManualRef.Text = _invHdr.Ith_manual_ref;
                        txtOtherRef.Text = _invHdr.Ith_entry_no;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        txtUserSeqNo.Text = string.Empty;
                        ddlSeqNo.Text = string.Empty;
                    }
                    #endregion

                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    if (_serList != null)
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

                        }

                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList = _itmList;

                        grdItems.AutoGenerateColumns = false;
                        grdItems.DataSource = ScanItemList;
                        grdItems.DataBind();

                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _serList;
                        grdSerial.DataBind();

                        foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                        {
                            LinkButton _addbtn = (LinkButton)hiderowbtn.FindControl("lbtnadditem");

                            LinkButton _delbtn = (LinkButton)hiderowbtn.FindControl("lbtndeleteitm");
                            LinkButton lbtnAddSerial = (LinkButton)hiderowbtn.FindControl("lbtnAddSerial");

                            _addbtn.Enabled = false;
                            _addbtn.CssClass = "buttoncolor";
                            _addbtn.OnClientClick = "return Enable();";

                            _delbtn.Enabled = false;
                            _delbtn.CssClass = "buttoncolor";
                            _delbtn.OnClientClick = "return Enable();";

                            lbtnAddSerial.Enabled = false;
                            lbtnAddSerial.CssClass = "buttoncolor";
                            lbtnAddSerial.OnClientClick = "return Enable();";
                        }

                        foreach (GridViewRow hiderowbtn2 in this.grdSerial.Rows)
                        {
                            LinkButton _delbutton = (LinkButton)hiderowbtn2.FindControl("lbtndelser");

                            _delbutton.Enabled = false;
                            _delbutton.CssClass = "buttoncolor";
                            _delbutton.OnClientClick = "return Enable();";
                        }

                        lbtntempsave.Enabled = false;
                        lbtntempsave.CssClass = "buttoncolor";
                        lbtntempsave.OnClientClick = "return Enable();";
                        LoadStatusDescriptionItem();
                        LoadStatusDescriptionSerial();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item not found !!!')", true);
                        txtDocumentNo.Text = string.Empty;
                        txtDocumentNo.Focus();
                        return;
                    }
                    Session["Doc"] = null;
                    #endregion
                }

                UserDPopoup.Hide();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
                return;
            }
            else if (lblvalue.Text == "TempDoc")
            {
                if (string.IsNullOrEmpty(entereddoc))
                {
                    Name = grdResultD.SelectedRow.Cells[1].Text;
                }
                else
                {
                    Name = entereddoc;
                    Session["ENTERED_DOC"] = null;
                }

                txtDocumentNo.Text = Name;
                lblvalue.Text = "";
                Session["TempDoc"] = "";
                Session["DocType"] = "TempDoc";

                LoadHeader(true);
                GetTempDocData(Name);

                foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                {
                    LinkButton _addbtn = (LinkButton)hiderowbtn.FindControl("lbtnadditem");

                    LinkButton _delbtn = (LinkButton)hiderowbtn.FindControl("lbtndeleteitm");
                    LinkButton lbtnAddSerial = (LinkButton)hiderowbtn.FindControl("lbtnAddSerial");

                    _addbtn.Enabled = false;
                    _addbtn.CssClass = "buttoncolor";
                    _addbtn.OnClientClick = "return Enable();";

                    _delbtn.Enabled = false;
                    _delbtn.CssClass = "buttoncolor";
                    _delbtn.OnClientClick = "return Enable();";

                    lbtnAddSerial.Enabled = false;
                    lbtnAddSerial.CssClass = "buttoncolor";
                    lbtnAddSerial.OnClientClick = "return Enable();";
                }

                foreach (GridViewRow hiderowbtn2 in this.grdSerial.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn2.FindControl("lbtndelser");

                    _delbutton.Enabled = false;
                    _delbutton.CssClass = "buttoncolor";
                    _delbutton.OnClientClick = "return Enable();";
                }

                lbtntempsave.Enabled = false;
                lbtntempsave.CssClass = "buttoncolor";
                lbtntempsave.OnClientClick = "return Enable();";

                LoadStatusDescriptionItem();
                LoadStatusDescriptionSerial();

                UserDPopoup.Hide();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
                Session["TempDoc"] = null;
                return;
            }
            else
            {
                txtfindreq.Text = grdResultD.SelectedRow.Cells[1].Text;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            }
        }
        
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDocumentNo.Text = grdResultD.SelectedRow.Cells[1].Text;
            txtDocumentNo_TextChanged(null, null);
            UserDPopoup.Hide();
            Session["TempDoc"] = "false";
        }

       

        protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {
           
            #region Clear Data

            List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
            grdSerial.AutoGenerateColumns = false;
            grdSerial.DataSource = _emptySer;
            grdSerial.DataBind();
            List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
            grdItems.AutoGenerateColumns = false;
            grdItems.DataSource = _emptyItm;
            grdItems.DataBind();

            lbtnsave.Enabled = true;
            lbtnsave.CssClass = "buttonUndocolor";
            lbtnsave.OnClientClick = "SaveConfirm();";

            txtAdjSubType.Text = string.Empty;
            lblSubTypeDesc.Text = "";
            txtManualRef.Text = string.Empty;
            txtOtherRef.Text = string.Empty;
            txtSupplierCd.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtUserSeqNo.Text = string.Empty;
            lblBatchNo.Text = string.Empty;
            ViewState["userSeqNo"] = txtUserSeqNo.Text;
            #endregion
            if (string.IsNullOrEmpty(txtDocumentNo.Text))
            {
                return;
            }
            if (chktemp.Checked)
            {
                LoadTempData(txtDocumentNo.Text);
            }
            else
            {
                LoadSavedData(txtDocumentNo.Text);
            }
        }


        protected void txtDocumentNo_TextChangedOld(object sender, EventArgs e)
        {
            string contain = txtDocumentNo.Text.Trim();
            if (!contain.Contains("TEMP"))
            {
                lblvalue.Text = "Doc";
                chktemp.Checked = false;
            }
            else
            {
                lblvalue.Text = "TempDoc";
                chktemp.Checked = true;
            }

            Session["ENTERED_DOC"] = contain;

            grdResultD_SelectedIndexChangedOld(null, null);

        }
        private void LoadTempData(string DocNo) 
        {
            try
            {
                if (!string.IsNullOrEmpty(DocNo))
                {
                    Int32 affected_rows;
                    bool _invalidDoc = true;
                    int _direction = 0;
                    int _lineNo = 0;
                    _direction = 0;

                    #region Clear Data
                    // grdItems.ReadOnly = false;
                    //gvSerial.ReadOnly = false;

                    List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _emptySer;
                    grdSerial.DataBind();

                    List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = _emptyItm;
                    grdItems.DataBind();

                    // btnSave.Enabled = true;
                    lbtnsave.Enabled = true;
                    lbtnsave.OnClientClick = "SaveConfirm();";
                    lbtnsave.CssClass = "buttonUndocolor";

                    lbtntempsave.Enabled = true;
                    lbtntempsave.OnClientClick = "TempSaveConfirm();";
                    lbtntempsave.CssClass = "buttonUndocolor";

                    // txtAdjSubType.Text = string.Empty;
                    //lblSubTypeDesc.Text = "";
                    txtManualRef.Text = string.Empty;
                    txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    //txtUserSeqNo.Clear();
                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr_Temp(DocNo);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "ADJ")
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
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid Document No');", true);
                        txtDocumentNo.Text = "";
                        txtDocumentNo.Focus();
                        return;
                    }
                    else
                    {
                        //cmdPrint.Enabled = true;
                        //grdItems.ReadOnly = true;
                        //gvSerial.ReadOnly = true;
                        foreach (GridViewRow gvr in grdItems.Rows)
                        {
                            LinkButton lbtnadditem = gvr.FindControl("lbtnadditem") as LinkButton;
                            LinkButton lbtnAddSerial = gvr.FindControl("lbtnAddSerial") as LinkButton;
                            LinkButton lbtndeleteitm = gvr.FindControl("lbtndeleteitm") as LinkButton;

                            lbtnadditem.Enabled = true;
                            lbtnAddSerial.Enabled = true;
                            lbtndeleteitm.Enabled = true;
                        }
                        foreach (GridViewRow gvr in grdSerial.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnser_Remove") as LinkButton;

                            Addrow.Enabled = true;
                        }

                        //btnSave.Enabled = false;
                        lbtnsave.Enabled = true;
                        lbtnsave.OnClientClick = "SaveConfirm();";
                        lbtnsave.CssClass = "buttonUndocolor";

                        lbtntempsave.Enabled = false;
                        lbtntempsave.OnClientClick = "return Enable();";
                        lbtntempsave.CssClass = "buttoncolor";

                        //ddlAdjSubType.DataTextField = _invHdr.Ith_sub_tp;
                        txtAdjSubType.Text = _invHdr.Ith_sub_tp;
                        txtManualRef.Text = _invHdr.Ith_manual_ref;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        txtOtherRef.Text = _invHdr.Ith_entry_no;
                        txtSupplierCd.Text = _invHdr.Ith_bus_entity;

                        MasterBusinessEntity _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupplierCd.Text.Trim(), null, null, "S");
                        //ddlSeqNo.Text = string.Empty;
                        //txtUserSeqNo.Clear();
                        ddlSeqNo.SelectedIndex = 0;
                    }
                    #endregion

                   // txtUserSeqNo.Text = _invHdr.Ith_entry_no;

                    #region loadSerial
                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("CRN", Session["UserCompanyCode"].ToString(), DocNo, _direction);
                    if (user_seq_num == -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                    {
                        GenerateNewUserSeqNo();
                        ViewState["userSeqNo"] = txtUserSeqNo.Text;
                        ucOutScan.userSeqNo = txtUserSeqNo.Text;
                        #region Get Serials
                        List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                        List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                        _serList = CHNLSVC.Inventory.Get_Int_Ser_Temp(txtDocumentNo.Text);
                        List<InventoryBatchN> _serListT = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);
                        if (_serList != null && _serList.Count != 0)
                        {
                            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            foreach (var itm in _scanItems)
                            {
                                _lineNo += 1;
                                InventoryRequestItem _itm = new InventoryRequestItem();
                                _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                                _itm.Itri_bqty = Convert.ToDecimal(itm.theCount);
                                _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                                _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                                _itm.Itri_line_no = _lineNo;
                                _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                                MasterItem _mstItem = new MasterItem();
                                _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Itri_itm_cd);
                                _itm.Mi_longdesc = _mstItem.Mi_longdesc;
                                _itm.Mi_model = _mstItem.Mi_model;
                                _itm.Mi_brand = _mstItem.Mi_brand;
                               // _itm.Itri_unit_price = itm.Peo.Inb_unit_cost;
                                _itm.Itri_unit_price = 0; 
                                _itmList.Add(_itm);
                                List<InventoryRequestItem> _ListItem = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                               // AddItem(itm.Peo.Tus_itm_cd, "", itm.Peo.Tus_itm_stus, itm.theCount.ToString(), txtUserSeqNo.Text, null);
                                AddItem(itm.Peo.Tus_itm_cd, _itm.Itri_unit_price.ToString(), itm.Peo.Tus_itm_stus, itm.theCount.ToString(), txtUserSeqNo.Text, null);
                            }

                            ScanItemList = _itmList;

                            foreach (ReptPickSerials serial in _serList)
                            {
                                var itemToRemove = _serListT.Find(r => r.Inb_itm_cd == serial.Tus_itm_cd);
                                _serListT.Remove(itemToRemove);

                                serial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                                // AddSerials(serial.Tus_itm_cd, serial.Tus_ser_1, txtUserSeqNo.Text);
                                serial.Tus_base_doc_no = serial.Tus_doc_no;
                                serial.Tus_doc_no = serial.Tus_exist_grnno;
                                // serial.Tus_base_itm_line = 1;
                                serial.Tus_batch_line = 1;
                                serial.Tus_job_no = null;
                                List<ReptPickSerials> _Listserial = ViewState["SerialList"] as List<ReptPickSerials>;
                                if ((_Listserial == null))
                                {
                                    _Listserial = new List<ReptPickSerials>();
                                    affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(serial, null);
                                }
                                if ((_Listserial.Count==0))
                                {
                                    _Listserial = new List<ReptPickSerials>();
                                    affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(serial, null);
                                }
                                //if (affected_rows == 0)
                                //{
                                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial Load Fail');", true);
                                //    return;
                                //}
                               // AddSerials(serial.Tus_itm_cd, serial.Tus_ser_1.ToString(), txtUserSeqNo.Text, serial.Tus_itm_stus, serial.Tus_qty.ToString());
                            }

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
                                _serial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                                //serial.Tus_qty = Session["Inb_qty"];
                                affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_serial, null);
                            }

                            //grdItems.AutoGenerateColumns = false;
                            //grdItems.DataSource = ScanItemList;
                            //grdItems.DataBind();
                            ViewState["TempScanItemList"] = ScanItemList;

                            //grdSerial.AutoGenerateColumns = false;
                            //grdSerial.DataSource = _serList;
                            //grdSerial.DataBind();
                            ViewState["TempserList"] = _serList;
                            //Get seq
                            LoadItems(txtDocumentNo.Text);
                            int xx = 0;
                        }
                        else
                        {

                            List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch_Temp(DocNo);

                            List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                            foreach (InventoryBatchN _serialItem in _nonserial)
                            {
                                ReptPickItems _reptitm = new ReptPickItems();
                                _reptitm.Tui_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                                _reptitm.Tui_req_itm_qty = _serialItem.Inb_qty;
                                _reptitm.Tui_req_itm_cd = _serialItem.Inb_itm_cd;
                                _reptitm.Tui_req_itm_stus = _serialItem.Inb_itm_stus;
                                _reptitm.Tui_pic_itm_cd = _serialItem.Inb_itm_cd;
                                _reptitm.Tui_pic_itm_stus = _serialItem.Inb_itm_stus;
                                _reptitm.Tui_pic_itm_qty = _serialItem.Inb_qty;
                                _saveonly.Add(_reptitm);
                            }
                            CHNLSVC.Inventory.SavePickedItems(_saveonly);

                            foreach (InventoryBatchN _serialItem in _nonserial)
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


                                _serial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);

                                //serial.Tus_qty = Session["Inb_qty"];
                                affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_serial, null);
                            }
                            //MessageBox.Show("Item not found!", "PRN No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // txtDocumentNo.Text = "";
                            //txtDocumentNo.Focus();
                            LoadItems(txtUserSeqNo.Text);
                            //ViewState["userSeqNo"] = txtUserSeqNo.Text;
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        txtUserSeqNo.Text = user_seq_num.ToString();
                        LoadItems(txtDocumentNo.Text);
                        ViewState["userSeqNo"] = txtUserSeqNo.Text;
                    }

                    #endregion
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        private void LoadSavedData(string DocNo) {
            lblvalue.Text = "";
            Session["Doc"] = "";
            Session["DocType"] = "Doc";

            LoadHeader(false);
            if (!string.IsNullOrEmpty(txtDocumentNo.Text))
            {
                bool _invalidDoc = true;
                int _direction = 0;
                int _lineNo = 0;
                if (ddlAdjTypeSearch.Text == "ADJ+") _direction = 1;

                #region Clear Data

                List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
                grdSerial.AutoGenerateColumns = false;
                grdSerial.DataSource = _emptySer;

                List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = _emptyItm;

                lbtnsave.Enabled = true;
                lbtnsave.CssClass = "buttonUndocolor";
                lbtnsave.OnClientClick = "SaveConfirm();";

                txtAdjSubType.Text = string.Empty;
                lblSubTypeDesc.Text = "";
                txtManualRef.Text = string.Empty;
                txtOtherRef.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtUserSeqNo.Text = string.Empty;
                lblBatchNo.Text = string.Empty;
                #endregion

                InventoryHeader _invHdr = new InventoryHeader();

                _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);
                #region Check Valid Document No
                if (_invHdr == null)
                {
                    _invalidDoc = false;
                    goto err;
                }
                if (_invHdr.Ith_doc_tp != "ADJ")
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
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid document no !!!')", true);
                    txtDocumentNo.Text = string.Empty;
                    txtDocumentNo.Focus();
                    return;
                }
                else
                {
                    txtSupplierCd.Enabled = false;
                    btnSearchSupp.Enabled = false;
                    lbtnsave.Enabled = false;
                    lbtnsave.CssClass = "buttoncolor";
                    lbtnsave.OnClientClick = "return Enable();";

                    //cmdPrint.Enabled = true;
                    //cmdPrint.CssClass = "buttonUndocolor";

                    txtAdjSubType.Text = _invHdr.Ith_sub_tp;
                    txtManualRef.Text = _invHdr.Ith_manual_ref;
                    txtOtherRef.Text = _invHdr.Ith_entry_no;
                    txtRemarks.Text = _invHdr.Ith_remarks;
                    txtUserSeqNo.Text = string.Empty;
                    ddlSeqNo.SelectedIndex = 0;
                }
                #endregion

                #region Get Serials
                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                if (_serList != null)
                {
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand}).Select(group => new { Peo = group.Key, theCount = group.Count() });
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
                        _itm.Itri_unit_price = 0;
                        _itm.Itri_bqty = Convert.ToDecimal(itm.theCount);
                       // _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                        _itmList.Add(_itm);

                    }

                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList = _itmList;

                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = ScanItemList;
                    grdItems.DataBind();

                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _serList;
                    grdSerial.DataBind();

                    foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                    {
                        LinkButton _addbtn = (LinkButton)hiderowbtn.FindControl("lbtnadditem");

                        LinkButton _delbtn = (LinkButton)hiderowbtn.FindControl("lbtndeleteitm");
                        LinkButton lbtnAddSerial = (LinkButton)hiderowbtn.FindControl("lbtnAddSerial");

                        _addbtn.Enabled = false;
                        _addbtn.CssClass = "buttoncolor";
                        _addbtn.OnClientClick = "return Enable();";

                        _delbtn.Enabled = false;
                        _delbtn.CssClass = "buttoncolor";
                        _delbtn.OnClientClick = "return Enable();";

                        lbtnAddSerial.Enabled = false;
                        lbtnAddSerial.CssClass = "buttoncolor";
                        lbtnAddSerial.OnClientClick = "return Enable();";
                    }

                    foreach (GridViewRow hiderowbtn2 in this.grdSerial.Rows)
                    {
                        LinkButton _delbutton = (LinkButton)hiderowbtn2.FindControl("lbtndelser");

                        _delbutton.Enabled = false;
                        _delbutton.CssClass = "buttoncolor";
                        _delbutton.OnClientClick = "return Enable();";
                    }

                    lbtntempsave.Enabled = false;
                    lbtntempsave.CssClass = "buttoncolor";
                    lbtntempsave.OnClientClick = "return Enable();";

                    LoadStatusDescriptionItem();
                    LoadStatusDescriptionSerial();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item not found !!!')", true);
                    txtDocumentNo.Text = string.Empty;
                    txtDocumentNo.Focus();
                    return;
                }
                Session["Doc"] = null;
                #endregion
            }

        }
        private void DispMsg(string msgText, string msgType = "")
        {
            //msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

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
        protected void allchk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox allchk = grdAdSearch.HeaderRow.FindControl("allchk") as CheckBox;
            if (_popSerialList.Count > 0)
            {
                foreach (var item in _popSerialList)
                {
                    item.Is_select = allchk.Checked;
                }
            }
            grdAdSearch.DataSource = _popSerialList;
            grdAdSearch.DataBind();
            CheckBox chkAll = grdAdSearch.HeaderRow.FindControl("allchk") as CheckBox;
            chkAll.Checked = allchk.Checked;
            UserAdPopup.Show();
            _popSerSearch = true;
        }

        protected void selectchk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var lb = (CheckBox)sender;
                var row = (GridViewRow)lb.NamingContainer;
                CheckBox selectchk = row.FindControl("selectchk") as CheckBox;
                Label Col_ItemCode = row.FindControl("Col_ItemCode") as Label;
                Label Col_Serial1 = row.FindControl("Col_Serial1") as Label;
                if (_popSerialList.Count > 0)
                {
                    var _selectRowList = _popSerialList.Where(c => c.Item == Col_ItemCode.Text.Trim() && c.Serial_1 == Col_Serial1.Text.Trim()).ToList();
                    if (_selectRowList.Count == 1)
                    {
                        _selectRowList[0].Is_select = selectchk.Checked;
                    }
                }
                grdAdSearch.DataSource = _popSerialList;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
                _popSerSearch = true;
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void lbtnprint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["documntNo"] = txtDocumentNo.Text;
                Session["GlbReportType"] = "SCM1_CONSOT";
                Session["GlbReportName"] = "Inward_Docs_Consign.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                clsInventory obj = new clsInventory();
                obj.printInwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                PrintPDF(targetFileName, obj._consIn);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch(Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Consignment Return Note Print", "ConsignmentReturnNote", ex.Message, Session["UserID"].ToString());
            }
        }
    }
}

    
