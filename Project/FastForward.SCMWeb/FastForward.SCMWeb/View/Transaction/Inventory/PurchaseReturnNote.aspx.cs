using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class PurchaseReturnNote : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearPage();
            }
            else
            {
                try
                {
                    if (chkpda.Checked == true)
                    {
                        ucOutScan._ispda = true;
                    }
                    else
                    {
                        ucOutScan._ispda = false;
                    }
                    
                    if (Session["Doc"].ToString() == "true")
                    {
                        UserDPopoup.Show();
                        UserPopoup.Hide();
                        Session["Doc"] = "";
                    }
                    else if (Session["Adv"].ToString() == "true")
                    {
                        UserAdPopup.Show();
                        UserDPopoup.Hide();
                        UserPopoup.Hide();
                        Session["Adv"] = "";
                    }
                    else if (Session["sup"].ToString() == "true")
                    {
                        UserAdPopup.Hide();
                        UserDPopoup.Hide();
                        UserPopoup.Show();
                        Session["sup"] = "";
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Please Reload Page..');", true);
                }

            }
        }
        #region Varialbe
        public string AdjType = string.Empty;
        private List<InventoryRequestItem> ScanItemList = null;
        private string _receCompany = string.Empty;
        private string RequestNo = string.Empty;
        private string SelectedStatus = string.Empty;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        private List<ReptPickSerials> serial_list = null;
        private List<ReptPickSerials> SelectedSerialList = null;
        //CommonSearch.CommonOutScan _commonOutScan = null;
        private string _chargeType = string.Empty;
        private List<string> SeqNumList = null;
        #endregion
        private void ClearPage()
        {
            Session["documntNo"] = "";
            PopulateLoadingBays();
            DateTime date = DateTime.Now;
            AdjType = string.Empty;
            ScanItemList = null;
            _receCompany = string.Empty;
            RequestNo = string.Empty;
            SelectedStatus = string.Empty;
            _itemdetail = null;
            _isDecimalAllow = false;
            serial_list = null;
            SelectedSerialList = null;
            _chargeType = string.Empty;
            SeqNumList = null;
            Session["sup"] = "";
            Session["GlbModuleName"] = "m_Trans_Inventory_PurchaseReturnNote";
            txtDate.Text = date.ToString("dd/MMM/yyyy");
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, Session["GlbModuleName"].ToString(), txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            //LoadAFSL();
            LoadAdjSubType();
            LoadDeliveroption();
            Session["Doc"] = "";
            ucOutScan.doc_tp = "PRN";
            ucOutScan.isApprovalSend = false;
            GetSeqNo();
            ucOutScan.adjustmentTypeValue = "-";
            ucOutScan.PNLTobechange.Visible = false;
            List<ReptPickSerials> _emptySer = new List<ReptPickSerials>();
            grdSerial.AutoGenerateColumns = false;
            grdSerial.DataSource = _emptySer;
            grdSerial.DataBind();

            List<InventoryRequestItem> _emptyItm = new List<InventoryRequestItem>();
            grdItems.AutoGenerateColumns = false;
            grdItems.DataSource = _emptyItm;
            grdItems.DataBind();
            Session["Adv"] = "";
            Session["DocType"] = "";
            ddlAdjSubType.SelectedIndex = -1;
            ddlMainType.SelectedIndex = -1;
            txtManualRef.Text = string.Empty;
            txtOtherRef.Text = string.Empty;
            txtDAdd1.Text = string.Empty;
            txtDAdd2.Text = string.Empty;
            txtRemarks.Text = string.Empty;
          //  ddlDeliver.SelectedIndex = 2;
            txtVehicle.Text = string.Empty;
            chk_Temp.Checked = false;
            txtDocumentNo.Text = string.Empty;
            txtUserSeqNo.Text = string.Empty;
            ddlSeqNo.SelectedIndex = -1;
            ucOutScan.PageClear();

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
        }

        #region Rooting for Additional Searching
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
                //case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItem.Text + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("PRN" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "PRN" + seperator + "0" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial:
                    {
                        //paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtOtherRef.Text);
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtOtherRef.Text + seperator + Session["itemstatus"].ToString() + seperator + "Serial");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        string type;
                        if (ddlMainType.SelectedItem.Text == "Local") { type = "L"; } else { type = "I"; }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + type);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void LoadAdjSubType()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
            DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, null, null);
            ddlAdjSubType.DataSource = _result;
            ddlAdjSubType.DataValueField = "Code";
            ddlAdjSubType.DataTextField = "Description";

            ddlAdjSubType.DataBind();
            ddlAdjSubType.Items.RemoveAt(0);
            //ddlAdjSubType.Items.Insert(0, "--Select--");

        }

        private void LoadDeliveroption()
        {
            DataTable _result = CHNLSVC.CommonSearch.GET_DeliverByOption(Session["UserCompanyCode"].ToString());
            ddlDeliver.DataSource = _result;
            ddlDeliver.DataValueField = "rtm_tp";
            ddlDeliver.DataTextField = "rtm_tp";
            ddlDeliver.DataBind();
            //ddlDeliver.Items.Insert(2, "--Select--");
           // ddlDeliver.SelectedItem.Text = "--Select--";


            if (ddlDeliver.Text == "OWN")
            {
                lblVehicle.Text = "Vehicle No";
            }
            else if (ddlDeliver.Text == "COURIER")
            {
                lblVehicle.Text = "POD Slip No";
            }
            else if (ddlDeliver.Text == "CUSTOMER")
            {
                lblVehicle.Text = "Vehicle No";
            }
            else if (ddlDeliver.Text == "FLEET JOB")
            {
                lblVehicle.Text = "Fleet Job";
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
        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
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

        protected void lbtnSearch_Supplier_Click(object sender, EventArgs e)
        {
             if (!(ddlMainType.SelectedIndex > 0))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select order type...!');", true);          
                return;
            }
            
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Supplier";
                UserPopoup.Show();
            

            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            //DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, null, null);
            //grdResult.DataSource = _result;
            //grdResult.DataBind();
            //BindUCtrlDDLData(_result);
            //lblvalue.Text = "Supplier";
            //UserPopoup.Show();
        }

        protected void lbtnDocNo_Click(object sender, EventArgs e)
        {
            Session["Adv"] = "";
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
            DataTable _result;
            var sortedTable = new DataTable();
            if (chk_Temp.Checked == true)
            {
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text));
                lblvalue.Text = "TempDoc";
                Session["TempDoc"] = "true";
        //        sortedTable = _result.AsEnumerable()
        //.OrderByDescending(r => r.Field<string>("Document"))
        //.CopyToDataTable();
                lblvalue.Text = "TempDoc";
                Session["TempDoc"] = "true";
            }
            else
            {
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1), Convert.ToDateTime(txtDate.Text));
                DataView dv = _result.DefaultView;
                dv.Sort = "Document desc";
                DataTable sortedDT = dv.ToTable();

                lblvalue.Text = "Doc";
                Session["Doc"] = "true";
                grdResultD.DataSource = sortedDT;
                grdResultD.DataBind();
                BindUCtrlDDLData2(sortedDT);

                txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
                return;
            }
            grdResultD.DataSource = _result;
            grdResultD.DataBind();
            BindUCtrlDDLData2(_result);

            txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
            txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
            UserDPopoup.Show();
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                UserDPopoup.Show();
                return;
            }
        }
        #endregion
        #region Generate new user seq no
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
            RPH.Tuh_doc_tp = "PRN";
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
                txtUserSeqNo.Text = generated_seq.ToString();
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #region Rooting for Load Items/Serials
        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;
                //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                //{
                _direction = 0;
                //}
                
                    Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "PRN", Session["UserID"].ToString(), _direction, _seqNo);
                    if (txtUserSeqNo.Text == "")
                    {
                        if (user_seq_num == -1)
                        {
                            user_seq_num = GenerateNewUserSeqNo();
                        }
                    }
                    else
                    {
                        user_seq_num = Convert.ToInt32(txtUserSeqNo.Text);
                    }
                   
                
              

                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
               // int i = 0;
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = 0;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    //_itm.Itri_line_no = _itms.
                    _itm.Itri_qty = 0;
                    _itm.Itri_bqty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = 0;
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                ViewState["ScanItemList"] = ScanItemList;

                ucOutScan.ScanItemList = ScanItemList;
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "PRN");
                ////if (_serList != null)
                ////{
                ////    if (_direction == 0)
                ////    {
                ////        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                ////        foreach (var itm in _scanItems)
                ////        {
                ////            foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
                ////            {
                ////                if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                ////                {
                ////                    ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                ////                }
                ////            }
                ////        }
                ////    }
                ////    else
                ////    {
                ////        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                ////        foreach (var itm in _scanItems)
                ////        {
                ////            foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
                ////            {
                ////                if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                ////                {
                ////                    ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                ////                }
                ////            }
                ////        }
                ////    }
           
                ////    grdSerial.AutoGenerateColumns = false;
                ////    grdSerial.DataSource = _serList;
                ////    grdSerial.DataBind();
                ////    ViewState["SerialList"] = _serList;
                ////    ucOutScan.PickSerial = _serList;
                ////}
                ////else
                ////{
                ////    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                ////    grdSerial.AutoGenerateColumns = false;
                ////    grdSerial.DataSource = emptyGridList;
                ////    grdSerial.DataBind();
                ////    ViewState["SerialList"] = emptyGridList;
                ////    ucOutScan.PickSerial = emptyGridList;
                ////}

                ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                if (_serList != null)
                {
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line, x.Tus_itm_stus, x.Tus_qty }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        foreach (InventoryRequestItem dr in ScanItemList)
                        {
                            if ((itm.Peo.Tus_itm_cd == dr.Itri_itm_cd) && (itm.Peo.Tus_itm_stus == dr.Itri_itm_stus))
                            {
                                if (itm.Peo.Tus_qty > 1)
                                {
                                    dr.Itri_bqty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                                    dr.Itri_app_qty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                                }
                                else
                                {
                                    dr.Itri_bqty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                                    dr.Itri_app_qty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                                }
                               

                            }
                            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                dr.Mis_desc = oItemStaus.Find(x => x.Mis_cd == itm.Peo.Tus_itm_stus).Mis_desc;
                            }
                        }
                     
                    }
                }
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                ViewState["ScanItemList"] = ScanItemList;
                if (_serList == null)
                {
                    grdSerial.DataSource = new int[] { };
                    grdSerial.DataBind();
                    grdItems.DataSource = new int[] { };
                    grdItems.DataBind();
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
          
            }
            catch (Exception err)
            {

                
                // MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        #endregion
        #region Rooting for Add Item

        private void AddSerials(string _item, string _Serial, string _Seqno,string _status,string _qty)
        {
            Int32 generated_seq = -1;
            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("PRN", Session["UserCompanyCode"].ToString(), _Seqno, 0);
            if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
            {
                generated_seq = user_seq_num;
            }
            else
            {
                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "PRN", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                //assign user_seqno
                ReptPickHeader RPH = new ReptPickHeader();
                RPH.Tuh_doc_tp = "PRN";
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
                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

            }
            if (msitem.Mi_is_ser1 ==1)
            {
                List<ReptPickSerials> Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);

                Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial).ToList();

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
                _serList2 = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "PRN");
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
            _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "PRN");
            ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
            if (_serList != null)
            {
                var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_qty}).Select(group => new { Peo = group.Key, theCount = group.Count() });
                foreach (var itm in _scanItems)
                {
                    foreach (InventoryRequestItem dr in ScanItemList)
                    {
                        if ((itm.Peo.Tus_itm_cd == dr.Itri_itm_cd) && (itm.Peo.Tus_itm_stus == dr.Itri_itm_stus))
                        {
                            if (itm.Peo.Tus_qty > 1)
                            {
                                dr.Itri_bqty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                                dr.Itri_app_qty = itm.Peo.Tus_qty;//itm.Peo.Tus_qty; // Current scan qty    
                            }
                            else
                            {
                                dr.Itri_bqty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                                dr.Itri_app_qty = itm.theCount;//itm.Peo.Tus_qty; // Current scan qty   
                            }
                         
                           
                        }
                    }
                   
                }

            }
            grdItems.DataSource = ScanItemList;
            grdItems.DataBind();
            ViewState["ScanItemList"] = ScanItemList;
            ucOutScan.ScanItemList = ScanItemList;

            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            if (_serList != null)
            {
                foreach (ReptPickSerials itemSer in _serList)
                {
                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        itemSer.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == itemSer.Tus_itm_stus).Mis_desc;
                    }
                }
            }
           

            grdSerial.DataSource = _serList;
            grdSerial.DataBind();
            ViewState["SerialList"] = _serList;
            ucOutScan.PickSerial = _serList;
           

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
                CHNLSVC.Inventory.SavePickedItems(_saveonly);
              

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

        #endregion
        #region Rooting for Delete items/Serials
        protected void OnRemoveFromSerialGrid(string _item, int _serialID, string _status)
        {
            try
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID),null,null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                  
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _item, _status);
                   
                }
                LoadItems(txtUserSeqNo.Text);

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

        protected void OnRemoveFromItemGrid(string _itemCode, string _itemStatus, int _lineNo)
        {
            try
            {
                bool _Isdelete = false;
                List<ReptPickSerials> _list = new List<ReptPickSerials>();
                _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "PRN");
                if (_list != null)
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
                      
                        //}
                    }
                //commented by dilshan on 23/04/2018 for PRN modification
                //if (_Isdelete==true)
                //{
                //    CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus, _lineNo, 2);
                   // txtUserSeqNo.Text = "";
                //}
                CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus, _lineNo, 2);
                //ScanItemList.RemoveAll(x => x.Itri_itm_cd == _itemCode && x.Itri_itm_stus == _itemStatus);
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
            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
            if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
            {
                CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID),null,null);
                CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _serialID, 1);
            }
            else
            {
                CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
            }
        }
        #endregion
        #region Rooting for Save
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkpda.Checked == true)
                {
                    return;
                }
                   bool value = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16018);
                   if (value == true)
                   {
                       Process(false);
                   }
                   else
                   {
                       string msg = "You dont have permission to save PRN...! Code : 16018";
                       ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('"+msg+"');", true);
                      // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You don't have permission to Save PRN');", true);
                   }
            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
          
        }
        private void Process(bool IsTemp)
        {
            try
            {
                if (IsTemp == false)
                {
                    if (txtVehicle.Text != "")
                    {
                        bool _vehicle = validateVehicle(txtVehicle.Text);
                        if (_vehicle == false)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please check vehicle number.');", true);
                            return;
                        }
                    }
                }
          
                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtOtherRef.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select supplier...!');", true);
                    return;
                }

                //if ((ddlAdjSubType.SelectedIndex == 0))
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Select the adjustment sub type...!');", true);
                //    // MessageBox.Show("Select the adjustment sub type!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";
                if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDate, lblH1, Convert.ToDateTime(txtDate.Text).ToShortDateString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                        {
                            txtDate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction');", true);
                            // MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        txtDate.Enabled = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction');", true);
                        //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                        return;
                    }
                }
                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo = "";
                Int32 result = -99;
                Int32 _userSeqNo = 0;
                int _direction = 0;
                _direction = 0;


                _userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "PRN", Session["UserID"].ToString(), _direction, txtUserSeqNo.Text);

                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "PRN");
                //reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "PRN");
                if (reptPickSerialsList == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found');", true);
                    // MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var _Nonserialfilter = reptPickSerialsList.Where(x => x.Tus_ser_id == 0).ToList();
                if (_Nonserialfilter.Count >0)
                {
                    foreach (ReptPickSerials _serial in _Nonserialfilter)
                    {
                        DataTable _chkItem = CHNLSVC.Inventory.CheckItemTo_PRN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _serial.Tus_itm_cd, _serial.Tus_itm_stus, "GRN", txtOtherRef.Text);
                        if (_chkItem.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            string _msg = "You cannot return this item" + _serial.Tus_itm_cd;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                            return;
                        }
                    }
                   
                }
                #region Check Referance Date and the Doc Date
                if (_direction == 0)
                {
                    if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(txtDate.Text).Date) == false)
                    {
                        return;
                    }
                }
                #endregion
                if (txtSavelconformmessageValue.Value == "No")
                {
                    return;
                }
                //#region Check Duplicate Serials
                //var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                //string _duplicateItems = string.Empty;
                //bool _isDuplicate = false;
                //if (_dup != null)
                //    if (_dup.Count > 0)
                //        foreach (Int32 _id in _dup)
                //        {
                //            Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                //            if (_counts > 1)
                //            {
                //                _isDuplicate = true;
                //                var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                //                foreach (string _str in _item)
                //                    if (string.IsNullOrEmpty(_duplicateItems))
                //                        _duplicateItems = _str;
                //                    else
                //                        _duplicateItems += "," + _str;
                //            }
                //        }
                //if (_isDuplicate)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item serials are duplicating. Please remove the duplicated serials. ');", true);
                //    // MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //#endregion
                InventoryHeader inHeader = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
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
                inHeader.Ith_acc_no = "PRN";
                inHeader.Ith_anal_1 = "";
                inHeader.Is_Suplierreturn = true;
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
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
              

                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = txtOtherRef.Text.Trim();
                inHeader.Ith_cate_tp = ddlAdjSubType.SelectedValue;// txtAdjSubType.Text.ToString().Trim();
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_cre_when = DateTime.Now;
                inHeader.Ith_del_add1 = txtDAdd1.Text.Trim();
                inHeader.Ith_del_add2 = txtDAdd2.Text.Trim();
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";

                //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                //{
                //    inHeader.Ith_direct = true;
                //}
                //else
                //{
                inHeader.Ith_direct = false;
                //}
                inHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                inHeader.Ith_doc_no = string.Empty;
                inHeader.Ith_doc_tp = "PRN";
                inHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                if (IsTemp == true)
                {
                    inHeader.Ith_entry_no = txtUserSeqNo.Text;
                }
                else
                {
                    inHeader.Ith_entry_no = "";
                }

                inHeader.Ith_entry_tp = ddlAdjSubType.SelectedValue;//txtAdjSubType.Text.ToString().Trim();
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = string.Empty;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_job_no = string.Empty;
                inHeader.Ith_loading_point = string.Empty;
                inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                inHeader.Ith_manual_ref = txtManualRef.Text.Trim();
                inHeader.Ith_mod_by = Session["UserID"].ToString();
                inHeader.Ith_mod_when = DateTime.Now;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_oth_loc = string.Empty;
                inHeader.Ith_oth_docno = "N/A";
                inHeader.Ith_remarks = txtRemarks.Text;
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                //inHeader.Ith_sub_tp = ddlAdjSubType.SelectedItem.Text; //txtAdjSubType.Text.ToString().Trim();
                inHeader.Ith_sub_tp = ddlAdjSubType.SelectedValue;
                inHeader.Ith_vehi_no = string.Empty;
                inHeader.Ith_vehi_no = txtVehicle.Text;
               // if (ddlDeliver.SelectedIndex > 0)
               // {
                    inHeader.Ith_anal_3 = ddlDeliver.SelectedItem.Text;
               // }
                
                #endregion
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "PRN";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "PRN";
                masterAuto.Aut_year = null;
                #endregion

                int _line = 0;
                #region Update some serial items
                if (_direction == 1)
                {
                    foreach (var _seritem in reptPickSerialsList)
                    {
                        _seritem.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                        _seritem.Tus_exist_grndt = Convert.ToDateTime(txtDate.Text).Date;
                        _seritem.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                        _seritem.Tus_orig_grndt = Convert.ToDateTime(txtDate.Text).Date;
                    }
                }
                else if (_direction == 0)
                {
                    foreach (var _prnSer in reptPickSerialsList)
                    {
                        _line = _line + 1;
                        _prnSer.Tus_base_itm_line = _line;
                    }
                }
                #endregion



                #region Save Adj+ / Adj-
                //if (_direction == 1) result = CHNLSVC.Inventory.ADJPlus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo);
                //else result = CHNLSVC.Inventory.ADJMinus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo);

                 result = CHNLSVC.Inventory.SavePRN(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, IsTemp, out documntNo);



                if (result != -99 && result >= 0)
                {
                    //if (MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\nDo you want to print this?", "Process Completed : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{
                    //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //    if (_direction == 1) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                    //    if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                    //    else _view.GlbReportName = "Outward_Docs.rpt";

                    //    _view.GlbReportDoc = documntNo;
                    //    _view.Show();
                    //    _view = null;
                    //}
                    //btnClear_Click(null, null);
                    //_inventoryRepDAL.DeleteTempPickObjs(txtUserSeqNo.Text);
                    string _msg = "Successfully saved the document number : " + documntNo;
                    Session["documntNo"] = documntNo;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _msg + "');", true);
                    ClearPage();

                    Session["print"] = 1;
                    _print();
                    //lblMssg.Text = "Do you want print now?";
                    //PopupConfBox.Show();
                    
                }
                else
                {
                    string Msg = documntNo.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                    string _msg = Msg ;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _msg + "');", true);
                    //MessageBox.Show(documntNo, "Process Terminated : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // ClearPage();
                }

                #endregion

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void Temp_After_Process()
        {
            if (CheckServerDateTime() == false) return;
            if ((ddlAdjSubType.SelectedIndex == 0))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Select the adjustment sub type...!');", true);
                // MessageBox.Show("Select the adjustment sub type!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";
            if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
            if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDate, lblH1, Convert.ToDateTime(txtDate.Text).ToShortDateString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction...!');", true);
                        // MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                        return;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction');", true);
                    //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDate.Focus();
                    return;
                }
            }
            List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
            List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
            string documntNo = "";
            Int32 result = -99;
            Int32 _userSeqNo = 0;
            int _direction = 0;
            _direction = 0;

            reptPickSerialsList = ViewState["TempserList"] as List<ReptPickSerials>;
            if (reptPickSerialsList == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found');", true);
                // MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region Check Referance Date and the Doc Date
            if (_direction == 0)
            {
                if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(txtDate.Text).Date) == false)
                {
                    return;
                }
            }
            #endregion
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
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
                // MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion


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
        protected void lbtnTempSave_Click(object sender, EventArgs e)
        {
            try
            {
                DispMsg("Please Contact  IT Department for further instructions."); return;
                if (chkpda.Checked == true)
                {
                    MasterAutoNumber _AutoNo = new MasterAutoNumber();
                    _AutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                    _AutoNo.Aut_cate_tp = "COM";
                    _AutoNo.Aut_direction = 0;
                    _AutoNo.Aut_modify_dt = null;
                    _AutoNo.Aut_moduleid = "PRN";
                    _AutoNo.Aut_start_char = "-PRN";
                    DateTime date = DateTime.Now;
                    _AutoNo.Aut_year = date.Year;
                    Int32 _autoNo = CHNLSVC.Inventory.GetAutoNumber(_AutoNo.Aut_moduleid, Convert.ToInt16(_AutoNo.Aut_direction), _AutoNo.Aut_start_char, _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;

                    string _documentNo = Session["UserDefLoca"].ToString() + "" + _AutoNo.Aut_start_char + "-" + "T" + "-" + Convert.ToString(date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                    Session["_AutoNo"] = _AutoNo;
                    txtdocname.Text = _documentNo;
                    MPPDA.Show();
                    return;
                }
                else
                {


                    Process(true);
                }
            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
           
        }
        #endregion
        #region Rooting for Check Adjustment Sub Type
        private bool IsValidAdjustmentSubType()
        {
            bool status = false;
            //ddlAdjSubType.SelectedItem.Text = ddlAdjSubType.SelectedItem.Text.ToUpper().ToString();
            DataTable _adjSubType = CHNLSVC.Inventory.GetMoveSubTypeAllTable("ADJ", ddlAdjSubType.SelectedValue);
            if (_adjSubType.Rows.Count > 0)
            {
                lblSubTypeDesc.Text = _adjSubType.Rows[0]["mmct_sdesc"].ToString();
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }
        #endregion
        #region Rooting for Check Item
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
        #endregion
        #region Modal Popup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string Name = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Supplier")
            {
                txtOtherRef.Text = Name;
                MasterBusinessEntity _supDet = new MasterBusinessEntity();
                _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtOtherRef.Text.Trim(), null, null, "S");
                txtDAdd1.Text = _supDet.Mbe_add1;
                txtDAdd2.Text = _supDet.Mbe_add2;
                lblvalue.Text = "";
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

        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Supplier")
            {           
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Supplier";
                UserPopoup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Supplier")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Supplier";
                UserPopoup.Show();
                Session["sup"]= "true";
                return;
            }
        }
        #endregion
        #region Modal Popup 2
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Doc")
            {
                txtDocumentNo.Text = Name;
                Session["documntNo"] = Name;
                lblvalue.Text = "";
                Session["Doc"] = "";
                Session["DocType"] = "Doc";
                GetDocData(Name);
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
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
               // txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                //txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
               // txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
               // txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
                return;
            }
        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                //txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                //txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
                return;
            }
        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "Doc";
                Session["Doc"] = "true";
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "TempDoc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                DataTable _result;
                _result = CHNLSVC.CommonSearch.Search_int_hdr_Temp_Infor(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "TempDoc";
                Session["Doc"] = "true";
                //txtFDate.Text = Convert.ToDateTime(txtDate.Text).Date.AddMonths(-1).ToShortDateString();
                //txtTDate.Text = Convert.ToDateTime(txtDate.Text).Date.ToShortDateString();
                UserDPopoup.Show();
                return;
            }
        }
        #endregion
        #region Modal Popup 3
        protected void grdAdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdAdSearch.SelectedRow.Cells[1].Text;

        }
        protected void grdAdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAdSearch.PageIndex = e.NewPageIndex;

            if (lblAvalue.Text == "Adv-ser")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
            }
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
                return;
            }
            if (lblAvalue.Text == "Serial")
            {
                
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerialWEB(SearchParams, "Item", Session["_itemCode"].ToString());
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
            }
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
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
            }
        }
        protected void txtSearchbywordA_TextChanged(object sender, EventArgs e)
        {
            Session["Adv"] = "";
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                lblAvalue.Text = "Adv";
                Session["Adv"] = "true";
                UserAdPopup.Show();
                return;
            }
            if (lblAvalue.Text == "Serial")
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                Session["Adv"] = "true";
                UserAdPopup.Show();
            }
        }
        #endregion
        private bool validateVehicle(string VNO)
        {

            if (ddlDeliver.Text == "OWN")
            {
                DataTable _Vehicle = CHNLSVC.CommonSearch.GET_Vehicle(Session["UserCompanyCode"].ToString(), VNO);
                if ((_Vehicle != null) && (_Vehicle.Rows.Count > 0))
                {
                    return true;
                }
                return false;
            }
            else if (ddlDeliver.Text == "FLEET JOB")
            {
                DataTable _Vehicle = CHNLSVC.CommonSearch.GET_TRANSPORT_JOB(Session["UserCompanyCode"].ToString(), VNO);
                if ((_Vehicle != null) && (_Vehicle.Rows.Count > 0))
                {
                    return true;
                }
                return false;
            }
            return true;
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

        protected void txtOtherRef_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOtherRef.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(Session["UserCompanyCode"].ToString(), txtOtherRef.Text, 1, "S"))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid supplier code...!');", true);
                        // MessageBox.Show("Invalid supplier code.", "Purchase Return", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOtherRef.Text = "";
                        txtOtherRef.Focus();
                        return;
                    }
                    else
                    {
                        MasterBusinessEntity _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtOtherRef.Text.Trim(), null, null, "S");

                        if (_supDet.Mbe_cd == null)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid supplier code...!');", true);
                            txtOtherRef.Text = "";
                            txtDAdd1.Text = "";
                            txtDAdd2.Text = "";
                            txtOtherRef.Focus();
                            return;
                        }
                        else
                        {
                            txtDAdd1.Text = _supDet.Mbe_add1;
                            txtDAdd2.Text = _supDet.Mbe_add2;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing..');", true);

            }
            
        }

        private void GetSeqNo()
        {
            SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(Session["UserID"].ToString(), "PRN", 0, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            ddlSeqNo.DataSource = SeqNumList;

            ddlSeqNo.DataBind();
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
                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "SaveConfirm();";
                    lbtnSave.CssClass = "buttonUndocolor";

                    lbtnTempSave.Enabled = true;
                    lbtnTempSave.OnClientClick = "SaveConfirm();";
                    lbtnTempSave.CssClass = "buttonUndocolor";

                    // txtAdjSubType.Text = string.Empty;
                    //lblSubTypeDesc.Text = "";
                    txtManualRef.Text = string.Empty;
                    txtOtherRef.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                   
                    //txtUserSeqNo.Clear();
                    #endregion

                    InventoryHeader _invHdr = new InventoryHeader();

                    _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(DocNo);
                    #region Check Valid Document No
                    if (_invHdr == null)
                    {
                        _invalidDoc = false;
                        goto err;
                    }
                    if (_invHdr.Ith_doc_tp != "PRN")
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
                        lbtnSave.Enabled = false;
                        lbtnSave.OnClientClick = "return Enable();";
                        lbtnSave.CssClass = "buttoncolor";

                        lbtnTempSave.Enabled = false;
                        lbtnTempSave.OnClientClick = "return Enable();";
                        lbtnTempSave.CssClass = "buttoncolor";

                        ddlAdjSubType.SelectedItem.Text = _invHdr.Ith_sub_tp;
                        txtManualRef.Text = _invHdr.Ith_manual_ref;
                        txtOtherRef.Text = _invHdr.Ith_bus_entity;
                        MasterBusinessEntity _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtOtherRef.Text.Trim(), null, null, "S");
                        txtDAdd1.Text = _supDet.Mbe_add1;
                        txtDAdd2.Text = _supDet.Mbe_add2;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                        ddlDeliver.Text = _invHdr.Ith_anal_3;
                        txtVehicle.Text = _invHdr.Ith_vehi_no;
                        //txtUserSeqNo.Clear();
                        ddlMainType.SelectedValue = _supDet.Mbe_sub_tp;
                        ddlSeqNo.Text = string.Empty;

                    }
                    #endregion

                    #region Get Serials
                    List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                    if (_serList != null && _serList.Count != 0)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus}).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            _lineNo += 1;
                            var _Model = _serList.First(x => x.Tus_itm_cd == itm.Peo.Tus_itm_cd);
                            InventoryRequestItem _itm = new InventoryRequestItem();
                            _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Itri_bqty = Convert.ToDecimal(itm.theCount);
                            _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                            _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                            _itm.Itri_line_no = _lineNo;
                            _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                            _itm.Mi_longdesc = _Model.Tus_itm_desc;
                            _itm.Mi_model = _Model.Tus_itm_model;
                            _itm.Mi_brand = _Model.Tus_itm_brand;
                            _itm.Itri_unit_price = _Model.Tus_unit_cost;
                            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                _itm.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _itm.Itri_itm_stus).Mis_desc;
                            }
                            _itmList.Add(_itm);

                        }
                        

                        ScanItemList = _itmList;
                       
                        grdItems.AutoGenerateColumns = false;
                        grdItems.DataSource = ScanItemList;
                        grdItems.DataBind();
                        List<ReptPickSerials> _NewserList = new List<ReptPickSerials>();

                        foreach (var itm2 in _serList)
                        {
                            var _Model = _serList.First(x => x.Tus_itm_cd == itm2.Tus_itm_cd);

                            ReptPickSerials serial = new ReptPickSerials();
                            serial.Tus_itm_cd = itm2.Tus_itm_cd;
                            serial.Tus_itm_stus = itm2.Tus_itm_stus;
                            serial.Tus_qty = 1;
                            serial.Tus_itm_model = _Model.Tus_itm_model;
                            serial.Tus_ser_1 = itm2.Tus_ser_1;
                            serial.Tus_ser_2 = itm2.Tus_ser_2;
                            serial.Tus_ser_3 = itm2.Tus_ser_3;
                            serial.Tus_ser_id = itm2.Tus_ser_id;
                            _NewserList.Add(serial);
                        }
                        foreach (ReptPickSerials _slist in _NewserList)
                        {

                            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                _slist.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == _slist.Tus_itm_stus).Mis_desc;
                            }
                        }
                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _NewserList;
                        grdSerial.DataBind();
                    }
                    else
                    {
                        //MessageBox.Show("Item not found!", "PRN No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       // txtDocumentNo.Text = "";
//txtDocumentNo.Focus();
                        List<InventoryBatchN> _nonserial = CHNLSVC.Inventory.Get_Int_Batch(DocNo);

                        List<InventoryRequestItem> _itmList2 = new List<InventoryRequestItem>();
                        foreach (InventoryBatchN _serialItem in _nonserial)
                        {
                            InventoryRequestItem _itm = new InventoryRequestItem();
                            MasterItem _mst = new MasterItem();
                            _mst = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _serialItem.Inb_itm_cd);
                            _itm.Itri_bqty = _serialItem.Inb_qty;
                            _itm.Itri_app_qty = _serialItem.Inb_qty;
                            _itm.Itri_itm_cd = _serialItem.Inb_itm_cd;
                            _itm.Itri_itm_stus = _serialItem.Inb_itm_stus;
                            _itm.Itri_line_no = _serialItem.Inb_itm_line;
                            _itm.Itri_qty = _serialItem.Inb_qty;
                            _itm.Mi_longdesc = _mst.Mi_longdesc;
                            _itm.Mi_model = _mst.Mi_model;
                            _itm.Mi_brand = _mst.Mi_brand;
                            _itm.Itri_unit_price = 0;
                            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                _itm.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _itm.Itri_itm_stus).Mis_desc;
                            }
                            _itmList2.Add(_itm);
                           
                        }
                       // CHNLSVC.Inventory.SavePickedItems(_saveonly);

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


                            _serial.Tus_usrseq_no = 0;
                            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                _serial.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _serial.Tus_itm_stus).Mis_desc;
                            }
                            _serList.Add(_serial);
                            //serial.Tus_qty = Session["Inb_qty"];
                           // affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_serial, null);
                        }
                        //MessageBox.Show("Item not found!", "PRN No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // txtDocumentNo.Text = "";
                        //txtDocumentNo.Focus();
                        grdItems.AutoGenerateColumns = false;
                        grdItems.DataSource = _itmList2;
                        grdItems.DataBind();
                        grdSerial.AutoGenerateColumns = false;
                        grdSerial.DataSource = _serList;
                        grdSerial.DataBind();
                        return;
                    }
                    #endregion
                    foreach (GridViewRow gvr in grdItems.Rows)
                    {
                        LinkButton Addrow = gvr.FindControl("lbtnitm_AddSerial") as LinkButton;
                        LinkButton Delrow = gvr.FindControl("lbtnitm_Remove") as LinkButton;
                        Addrow.Enabled = false;
                        Addrow.OnClientClick = "return Enable();";
                        Delrow.Enabled = false;
                        Delrow.OnClientClick = "return Enable();";

                    }
                    foreach (GridViewRow gvr in grdSerial.Rows)
                    {
                        LinkButton Addrow = gvr.FindControl("lbtnser_Remove") as LinkButton;
                        Addrow.Enabled = false;
                        Addrow.OnClientClick = "return Enable();";
                    }
                    ucOutScan.LBTNAdd.Enabled = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "SaveConfirm();";
                    lbtnSave.CssClass = "buttonUndocolor";

                    lbtnTempSave.Enabled = true;
                    lbtnTempSave.OnClientClick = "TempSaveConfirm();";
                    lbtnTempSave.CssClass = "buttonUndocolor";

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
                    if (_invHdr.Ith_doc_tp != "PRN")
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
                        foreach (GridViewRow gvr in grdItems.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnitm_AddSerial") as LinkButton;
                            LinkButton Delrow = gvr.FindControl("lbtnitm_Remove") as LinkButton;

                            Addrow.Enabled = true;
                            Delrow.Enabled = true;
                        }
                        foreach (GridViewRow gvr in grdSerial.Rows)
                        {
                            LinkButton Addrow = gvr.FindControl("lbtnser_Remove") as LinkButton;

                            Addrow.Enabled = true;
                        }

                        //btnSave.Enabled = false;
                        lbtnSave.Enabled = true;
                        lbtnSave.OnClientClick = "SaveConfirm();";
                        lbtnSave.CssClass = "buttonUndocolor";

                        lbtnTempSave.Enabled = false;
                        lbtnTempSave.OnClientClick = "return Enable();";
                        lbtnTempSave.CssClass = "buttoncolor";

                        ddlAdjSubType.DataTextField = _invHdr.Ith_sub_tp;
                        txtManualRef.Text = _invHdr.Ith_manual_ref;
                        txtOtherRef.Text = _invHdr.Ith_bus_entity;

                        MasterBusinessEntity _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtOtherRef.Text.Trim(), null, null, "S");
                        txtDAdd1.Text = _supDet.Mbe_add1;
                        txtDAdd2.Text = _supDet.Mbe_add2;
                        txtRemarks.Text = _invHdr.Ith_remarks;
                      // LoadDeliveroption();
                        ddlDeliver.Text= _invHdr.Ith_anal_3;
                        txtVehicle.Text = _invHdr.Ith_vehi_no;
                        //txtUserSeqNo.Clear();
                        ddlMainType.SelectedValue = _supDet.Mbe_sub_tp;
                        ddlSeqNo.Text = string.Empty;



                        //txtUserSeqNo.Clear();
                        ddlSeqNo.Text = string.Empty;
                    }
                    #endregion
                    txtUserSeqNo.Text = _invHdr.Ith_entry_no;
                  
                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("PRN", Session["UserCompanyCode"].ToString(), DocNo, 0);
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
                        if (_serList != null && _serList.Count !=0)
                        {
                            var _scanItems = _serListT.GroupBy(x => new { x.Inb_itm_cd, x.Inb_itm_stus, x.Inb_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                            foreach (var itm in _scanItems)
                            {
                                _lineNo += 1;
                                InventoryRequestItem _itm = new InventoryRequestItem();
                                _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                                _itm.Itri_bqty = Convert.ToDecimal(itm.theCount);
                                _itm.Itri_itm_cd = itm.Peo.Inb_itm_cd;
                                _itm.Itri_itm_stus = itm.Peo.Inb_itm_stus;
                                _itm.Itri_line_no = _lineNo;
                                _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                                MasterItem _mstItem = new MasterItem();
                                _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Itri_itm_cd);
                                _itm.Mi_longdesc = _mstItem.Mi_longdesc;
                                _itm.Mi_model = _mstItem.Mi_model;
                                _itm.Mi_brand = _mstItem.Mi_brand;
                                _itm.Itri_unit_price = itm.Peo.Inb_unit_cost;
                                _itmList.Add(_itm);


                                List<InventoryRequestItem> _ListItem = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                                //if ((_ListItem==null))
                               // {
                                  //  _ListItem = new List<InventoryRequestItem>();
                                AddItem(itm.Peo.Inb_itm_cd, itm.Peo.Inb_unit_cost.ToString(), itm.Peo.Inb_itm_stus, itm.theCount.ToString(), txtUserSeqNo.Text, null);
                               // }
                                ////if (_ListItem.Count == 0)
                                ////{
                                ////    // CHNLSVC.Inventory.SavePickedItems(;
                                ////    AddItem(itm.Peo.Tus_itm_cd, itm.Peo.Tus_unit_cost.ToString(), itm.Peo.Tus_itm_stus, itm.theCount.ToString(), txtUserSeqNo.Text, null);
                                ////}


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

                                //if (affected_rows == 0)
                                //{
                                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial Load Fail');", true);
                                //    return;
                                //}
                                // AddSerials(serial.Tus_itm_cd, serial.Tus_ser_1.ToString(), txtUserSeqNo.Text,true);
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


                            grdItems.AutoGenerateColumns = false;
                            grdItems.DataSource = ScanItemList;
                            grdItems.DataBind();
                            ViewState["TempScanItemList"] = ScanItemList;
                            grdSerial.AutoGenerateColumns = false;
                            grdSerial.DataSource = _serList;
                            grdSerial.DataBind();
                            ViewState["TempserList"] = _serList;
                            LoadItems(txtUserSeqNo.Text);
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
                            return;
                        }
                        #endregion
                       
                        //ReptPickHeader RPH = new ReptPickHeader();
                        //RPH.Tuh_doc_tp = "PRN";
                        //RPH.Tuh_cre_dt = DateTime.Today;
                        //RPH.Tuh_ischek_itmstus = true;
                        //RPH.Tuh_ischek_reqqty = true;
                        //RPH.Tuh_ischek_simitm = true;
                        //RPH.Tuh_session_id = Session["SessionID"].ToString();
                        //RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        //RPH.Tuh_usr_id = Session["UserID"].ToString();
                        //RPH.Tuh_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                        //RPH.Tuh_doc_no = txtUserSeqNo.Text;
                        //RPH.Tuh_direct = false;
                        //// RPH.Tuh_doc_no = DocNo;
                        ////write entry to TEMP_PICK_HDR
                        //affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                    }
                    else
                    {
                        LoadItems(txtUserSeqNo.Text);
                        ViewState["userSeqNo"] = txtUserSeqNo.Text;
                    }
                    

                }
            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
        protected void lbtnAdvSeach_Click(object sender, EventArgs e)
        {
           
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
            DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, null, null);
            grdAdSearch.DataSource = _result;
            grdAdSearch.DataBind();
            Session["GRNbase"] = "true";
            _result.Columns.RemoveAt(0);
            _result.Columns.RemoveAt(1);
            _result.Columns.RemoveAt(1);
            _result.Columns.RemoveAt(1);
            _result.Columns.RemoveAt(2);
            _result.Columns.RemoveAt(2);
           
            
            _result.Columns.RemoveAt(3);
            _result.Columns.RemoveAt(2);
            _result.Columns.RemoveAt(3);
            _result.Columns.RemoveAt(0);
            _result.Columns.RemoveAt(0);
            BindUCtrlDDLData3(_result);
            lblvalue.Text = "Adv";
            Session["Advan"] = "true";
            UserAdPopup.Show();
        }

        protected void btnAdvanceAddItem_Click(object sender, EventArgs e)
        {
            if (txtOtherRef.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select supplier code...');", true);
                return;
            }
            bool _IsItemAvailable = false;
            // Add Item ...
            foreach (GridViewRow dgvr in grdAdSearch.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
                if (chk.Checked)
                {
                    string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
                    string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
                    string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
                    string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
                    string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
                   // txtOtherRef.Text = (dgvr.FindControl("Col_Supplier") as Label).Text;
                   // txtOtherRef.Enabled = false;
                    bool _balnce = CheckItem(_item);
                    if (_balnce == true)
                    {
                        DataTable _chkItem = CHNLSVC.Inventory.CheckItemTo_PRN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status, "GRN", txtOtherRef.Text);
                        if (_chkItem.Rows.Count  < 1)
                        {
                            AddItem(_item, _UnitCost, _status, _qty, txtUserSeqNo.Text, _serial);
                            _IsItemAvailable = true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You cannot return this item. Please check the item again...!');", true);

                            return;
                        }
                        
                    }
                    
                }
            }
            if (_IsItemAvailable == true)
            {
                // Add serial ...Save TEMP_PICK_SER
                foreach (GridViewRow dgvr in grdAdSearch.Rows)
                {
                    CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
                    if (chk.Checked)
                    {
                        string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
                        string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
                        string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
                        string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
                        string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
                        bool _balnce = CheckItem(_item);
                        if (_balnce == true)
                        {
                            if (Session["Advan"] == null)
                            {
                                _qty = "1";
                            }
                            AddSerials(_item, _serial, txtUserSeqNo.Text, _status, _qty);
                           // Session["Advan"] = "";
                        }

                    }
                }
            }
            UserAdPopup.Hide();
        }

        protected void lbtnser_Remove_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (grdSerial.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _item = (row.FindControl("ser_Item") as Label).Text;
                string _serialID = (row.FindControl("ser_SerialID") as Label).Text;
                string _status = (row.FindControl("ser_Status") as Label).Text;
                if (string.IsNullOrEmpty(_item)) return;
                OnRemoveFromSerialGrid(_item, Convert.ToInt32(_serialID), _status);
            }
        }

        protected void lbtnitm_Remove_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (grdItems.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                string _itemStatus = (row.FindControl("itm_Status") as Label).Text;
                int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                if (string.IsNullOrEmpty(_itemCode)) return;
                OnRemoveFromItemGrid(_itemCode, _itemStatus, _lineNo);
            }
        }

        protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                //if (!string.IsNullOrEmpty(ddlSeqNo.Text))
                // {
                txtUserSeqNo.Text = ddlSeqNo.SelectedItem.Text;
                LoadItems(txtUserSeqNo.Text);
                //}
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

        protected void ddlAdjSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // lblSubTypeDesc.Text = string.Empty;
                if (ddlAdjSubType.SelectedIndex == 0) return;
                if (IsValidAdjustmentSubType() == false)
                {
                    // MessageBox.Show("Invalid sub type.", "PRN Sub Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid sub type...!');", true);
                    return;
                }
            }
            catch (Exception err)
            {
                //Cursor.Current = Cursors.Default;
                //CHNLSVC.CloseChannel();
                ///MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnitm_AddSerial_Click(object sender, EventArgs e)
        {
            if (txtOtherRef.Text == "")
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
                string _itemStatus = (row.FindControl("itm_Status") as Label).Text;
                int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                Session["_lineNo"] = _lineNo;
                Session["_itemCode"] = _itemCode;
                Session["itemstatus"] = _itemStatus;
                if (string.IsNullOrEmpty(_itemCode)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerialWEB(SearchParams, "Item", _itemCode);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                btnAllItem.Visible = false;
                _result.Columns.RemoveAt(0);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(2);
                _result.Columns.RemoveAt(1);
                BindUCtrlDDLData3(_result);
                lblAvalue.Text = "Serial";
                UserAdPopup.Show();

            }


        }

        protected void ddlDeliver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDeliver.Text == "OWN")
            {
                lblVehicle.Text = "Vehicle No";
            }
            else if (ddlDeliver.Text == "COURIER")
            {
                lblVehicle.Text = "POD Slip No";
            }
            else if (ddlDeliver.Text == "CUSTOMER")
            {
                lblVehicle.Text = "Vehicle No";
            }
            else if (ddlDeliver.Text == "FLEET JOB")
            {
                lblVehicle.Text = "Fleet Job";
            }
        }

        protected void btnAClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordA.Text = "";
            UserAdPopup.Hide();
            UserDPopoup.Hide();
            UserPopoup.Hide();
            Session["Adv"] = "";
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            UserAdPopup.Hide();
            UserDPopoup.Hide();
            UserPopoup.Hide();
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserAdPopup.Hide();
            UserDPopoup.Hide();
            UserPopoup.Hide();
        }

        protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {
            if (chk_Temp.Checked == true)
            {
                lblvalue.Text = "";
                Session["Doc"] = "";
                Session["DocType"] = "Doc";
                GetTempDocData(txtDocumentNo.Text);
            
            }
            else
            {
                lblvalue.Text = "";
                Session["TempDoc"] = "";
                Session["DocType"] = "TempDoc";
                GetDocData(txtDocumentNo.Text);
            }
        }


        protected void btnAllItem_Click(object sender, EventArgs e)
        {
            if (txtOtherRef.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select supplier code...');", true);
                return;
            }
            bool _IsItemAvailable = false;
            // Add Item ...
            foreach (GridViewRow dgvr in grdAdSearch.Rows)
            {
               
                    string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
                    string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
                    string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
                    string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
                    string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
                    // txtOtherRef.Text = (dgvr.FindControl("Col_Supplier") as Label).Text;
                    // txtOtherRef.Enabled = false;
                    bool _balnce = CheckItem(_item);
                    if (_balnce == true)
                    {
                        DataTable _chkItem = CHNLSVC.Inventory.CheckItemTo_PRN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status, "GRN", txtOtherRef.Text);
                        if (_chkItem.Rows.Count < 1)
                        {
                            AddItem(_item, _UnitCost, _status, _qty, txtUserSeqNo.Text, _serial);
                            _IsItemAvailable = true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You cannot return this item. Please check the item again...!');", true);

                            return;
                        }

                    }

                
            }
            if (_IsItemAvailable == true)
            {
               
                // Add serial ...Save TEMP_PICK_SER
                foreach (GridViewRow dgvr in grdAdSearch.Rows)
                {
                   
                        string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
                        string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
                        string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
                        string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
                        string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
                        bool _balnce = CheckItem(_item);
                        if (_balnce == true)
                        {
                            if (Session["Advan"] == null)
                            {
                                _qty = "1";
                            }
                            AddSerials(_item, _serial, txtUserSeqNo.Text, _status, _qty);
                            // Session["Advan"] = "";
                        }

                    
                }
                Session["GRNbase"] = "";
            }
            //foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
            //{
            //    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtnitm_AddSerial");
            //    _delbutton.Enabled = false;
            //    _delbutton.CssClass = "buttoncolor";
            //    _delbutton.OnClientClick = "return Enable();";
           // }
            grdItems.Columns[0].Visible = false;
            //for (int i = 0; i < grdItems.HeaderRow.Cells.Count; i++)
            //{
            //    grdItems.Columns[i].Visible = false;
            //}
           // GridView gv = (sender as GridView);
           // gv.HeaderRow.Cells[0].Visible = false;
            UserAdPopup.Hide();

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


        protected void btnsend_Click(object sender, EventArgs e)
        {
            if (txtpdasend.Value == "Yes")
            {
                try
                {
                    SavePDA();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                    CHNLSVC.CloseChannel();
                }
               
            }
        }

        private void SavePDA()
        {
            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];
            ReptPickHeader _pick = new ReptPickHeader();
            _pick.Tuh_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
            _pick.Tuh_doc_no = txtdocname.Text;
            _pick.Tuh_wh_com = warehousecom;
            _pick.Tuh_wh_loc = warehouseloc;
            _pick.Tuh_load_bay = ddlloadingbay.SelectedValue;
            MasterAutoNumber _Auto= Session["_AutoNo"]  as MasterAutoNumber;
            Int32 No = CHNLSVC.Inventory.UpdatePicked_Hd_doc(_pick, _Auto);
            if (No > 0)
            {
                string _msg = "Successfully saved the PDA document number : " + txtdocname.Text;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _msg + "');", true);
                ClearPage();
            }
            else
            {
                // string Msg = _documentNo.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                string _msg = "Process uncompleted";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _msg + "');", true);
                //MessageBox.Show(documntNo, "Process Terminated : " + ddlAdjType.SelectedItem.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
       
            }
        }

        public void _print()
        {
            int value = (int)Session["print"];
            if (value == 1)
            {
                try
                {
                    if (Session["UserCompanyCode"].ToString() == "AEC")
                    {
                        Session["GlbReportType"] = "SCM1_PRN";
                        Session["GlbReportName"] = "Outward_Docs_Full__AEC.rpt";
                        Session["GlbReportName"] = "Outward_Docs_Full__AEC.rpt";
                        string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                        string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                        PrintPDF(targetFileName);
                        string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    }
                    else
                    {
                        Session["GlbReportType"] = "SCM1_PRN";
                        if (Session["UserCompanyCode"].ToString() == "ABE")
                        {
                            Session["GlbReportName"] = "Outward_Docs_Full_ABE_PRN.rpt";
                        }
                        else
                        {
                            Session["GlbReportName"] = "Outward_Docs_Full.rpt";
                            Session["GlbReportName"] = "Outward_Docs_Full.rpt";
                        }
                        string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                        string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                        PrintPDF(targetFileName);
                        string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    }
                }
                catch(Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Purchase Return Note Print", "PurchaseReturnNote", ex.Message, Session["UserID"].ToString());
                }
            }
            else if (value == 2)
            {
                try
                {
                    Session["GlbReportType"] = "";
                    Session["GlbReportName"] = "serial_items.rpt";
                    Session["GlbReportName"] = "serial_items.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    PrintPDFSer(targetFileName);
                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                catch(Exception ex)
                {
                    CHNLSVC.MsgPortal.SaveReportErrorLog("Purchase Return Note Print Serial", "PurchaseReturnNote", ex.Message, Session["UserID"].ToString());
                }
            }
        }

        public void PrintPDF(string targetFileName)
        {
            try
            {
                ReportDocument rptDoc = new ReportDocument();
                clsInventory obj = new clsInventory();
                obj.printOutwardDocs(Session["GlbReportName"].ToString(), Session["documntNo"].ToString());
                if (Session["UserCompanyCode"].ToString() == "AEC")
                {
                    rptDoc = (ReportDocument)obj._outdocfull_AEC;
                }
                else if (Session["UserCompanyCode"].ToString() == "ABE")
                {
                    rptDoc = (ReportDocument)obj._outdocfull_ABE_PRN;
                }
                else
                {
                    rptDoc = (ReportDocument)obj._outdocfull;
                }
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
        public void PrintPDFSer(string targetFileName)
        {
            try
            {
                clsInventory obj = new clsInventory();
                obj.get_Item_Serials(Session["documntNo"].ToString(), Session["UserID"].ToString(), Session["UserDefLoca"].ToString());
                ReportDocument rptDoc = (ReportDocument)obj._serialItems;
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportType = "SCM1_PRN";
            BaseCls.GlbReportDoc = Session["documntNo"].ToString(); ;
            BaseCls.GlbReportHeading = "OUTWARD DOC";
            Session["GlbReportName"] = "Outward_Docs_Full.rpt";
            Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
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
                        Session["print"] = 1;
                        Session["Suppler"] = txtOtherRef.Text;
                        _print();
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
                        Session["print"] = 2;
                        _print();
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
    }
}