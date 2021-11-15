using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Warehouse
{
    public partial class BinSetup : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearPage();
                ClearMsg();
                grdLocationBay.DataSource = _LoadingBay;
                grdLocationBay.DataBind();
                grdDocLocationBay.DataSource = _DocLoadingBay;
                grdDocLocationBay.DataBind();
            }
            else
            {
                lblalert.Text = "";
                lblsuccess.Text = "";
                lblsuccess.Visible = false;
                lblalert.Visible = false;
                divUpcompleted.Visible = false;
            }
        }
        bool Isupdate = false;
        DataTable _result;
        DataTable _result1;
        /*
         ViewState["TableID"]
         ViewState["ZoneList"]
         ViewState["AisleList"]
         ViewState["RowList"]
         ViewState["LevelList"]
         ViewState["Bin"]
         ViewState["BinItem"]
         ViewState["Store"]
         ViewState["SEARCH"]
         Session["Zseq"]
         Session["AisleSeq"]
         Session["LevelSeq"]
         Session["RowSeq"]
         Session["ZoneSeq"]
         Session["BinSeq"]
         Session["FilePath"]*/

        private List<WarehouseZone> _ZoneList = new List<WarehouseZone>();
        private List<WarehouseAisle> _AisleList = new List<WarehouseAisle>();
        private List<WarehouseRow> _RowList = new List<WarehouseRow>();
        private List<WarehouseLevel> _LevelList = new List<WarehouseLevel>();
        private List<REF_ITM_CATE1> _REF_ITM_CATE1 = new List<REF_ITM_CATE1>();
        private List<warehouseStorageFacility> _warehouseStorageFacility = new List<warehouseStorageFacility>();
        private List<warehouseBinItems> _warehouseBinItems = new List<warehouseBinItems>();
        private List<WarehouseBin> _WarehouseBin = new List<WarehouseBin>();
        private List<LoadingBay> _LoadingBay = new List<LoadingBay>();
        private List<ReptPickHeader> _DocLoadingBay = new List<ReptPickHeader>();

        Tuple<List<WarehouseBin>, List<warehouseBinItems>, List<warehouseStorageFacility>> _BinDetas;
        bool Islayoutupdate = false;
        private void ClearMsg()
        {
            Warnninglayoutplan.Visible = false;
            Successlayoutplan.Visible = false;
            WarnningBin.Visible = false;
            SuccessBin.Visible = false;
        }
        private void ErrorMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            // Warnninglayoutplan.Visible = true;
            // lblWarnninglayoutplan.Text = _Msg;
        }
        private void SuccessMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
            // Successlayoutplan.Visible = true;
            // lblSuccesslayoutplan.Text = _Msg;
        }
        private void ErrorBinMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            // WarnningBin.Visible = true;
            //lblWarnninglBin.Text = _Msg;
        }
        private void SuccessBinMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
            // SuccessBin.Visible = true;
            // lblSuccessBin.Text = _Msg;
        }
        private void ClearPage()
        {
            treeViewLayout.Nodes.Clear();
            txtStorecode.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            txtPrefixBin.Text = string.Empty;
            txtZDes.Text = string.Empty;
            txtZID.Text = string.Empty;
            grdZone.DataSource = new int[] { };
            grdZone.DataBind();
            txtADes.Text = string.Empty;
            txtAId.Text = string.Empty;
            grdAisle.DataSource = new int[] { };
            grdAisle.DataBind();
            txtRDes.Text = string.Empty;
            txtRId.Text = string.Empty;
            grdRow.DataSource = new int[] { };
            grdRow.DataBind();
            txtlDes.Text = string.Empty;
            txtlId.Text = string.Empty;
            grdlevel.DataSource = new int[] { };
            grdlevel.DataBind();

            chk_AisleActive.Checked = false;
            chk_AisleIsDef.Checked = false;
            _ZoneList = new List<WarehouseZone>();
            _AisleList = new List<WarehouseAisle>();

            chk_BinIsDef.Checked = false;

            chk_BinActive.Checked = false;

            txtBinDes.Text = string.Empty;
            txtBinLocation.Text = string.Empty;
            txtBinLevel.Text = string.Empty;
            txtBinRow.Text = string.Empty;
            txtBinAisle.Text = string.Empty;
            txtBinZone.Text = string.Empty;
            txtBinStore.Text = string.Empty;
            txtBinwarehouse.Text = string.Empty;
            txtmaincat.Text = string.Empty;
            txtsubcat1.Text = string.Empty;
            txtsubcat2.Text = string.Empty;
            txtsubcat3.Text = string.Empty;
            txtsubcat4.Text = string.Empty;
            txtBinzoneS.Text = string.Empty;
            txtBinAisleS.Text = string.Empty;
            txtBinRowS.Text = string.Empty;
            txtBinLevelS.Text = string.Empty;
            txtwidth.Text = string.Empty;
            txtlength.Text = string.Empty;
            txtheight.Text = string.Empty;
            txtMaxweight.Text = string.Empty;
            txtTotspace.Text = string.Empty;
            txtutility.Text = string.Empty;
            txtfree.Text = string.Empty;

            txtTotspace.Text = "0";
            txtwidth.Text = "0";
            txtlength.Text = "0";
            txtheight.Text = "0";
            txtMaxweight.Text = "0";
            txtTotspace.Text = "0";
            txtutility.Text = "0";
            txtfree.Text = "0";
            UOM();
            GetStorageFacility();
            GetBinType();
            grdstoreFacility.DataSource = new int[] { };
            grdstoreFacility.DataBind();
            grdBinItem.DataSource = new int[] { };
            grdBinItem.DataBind();
            grdBinDeatils.DataSource = new int[] { };
            grdBinDeatils.DataBind();
            //grdLocationBay.DataSource = new int[] { };
            //grdLocationBay.DataBind();
            Session["Islayoutupdate"] = "";
            ViewState["AisleList"] = "";
            ViewState["RowList"] = "";
            ViewState["LevelList"] = "";
            Session["AisleSeq"] = "0";
            Session["LevelSeq"] = "0";
            Session["RowSeq"] = "0";
            Session["ZoneSeq"] = "0";
            Session["BinSeq"] = "";
            Session["Isupdate"] = "";

            txtManulBinDes.Text = string.Empty;
            txtManulBinLoc.Text = string.Empty;
            txtfrom.Text = string.Empty;
            txtTo.Text = string.Empty;
            divUpcompleted.Visible = false;
        }
        private void InforMsg(string _Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);
            // WarnningBin.Visible = true;
            //lblWarnninglBin.Text = _Msg;
        }
        private void UOM()
        {
            List<MasterUOM> _ListItems = null;
            _ListItems = CHNLSVC.General.GetItemUOM_active();
            ddluomwidth.DataSource = _ListItems;
            ddluomwidth.DataTextField = "msu_cd";
            ddluomwidth.DataValueField = "msu_cd";
            ddluomwidth.DataBind();
            ddluomlength.DataSource = _ListItems;
            ddluomlength.DataTextField = "msu_cd";
            ddluomlength.DataValueField = "msu_cd";
            ddluomlength.DataBind();
            ddluomheight.DataSource = _ListItems;
            ddluomheight.DataTextField = "msu_cd";
            ddluomheight.DataValueField = "msu_cd";
            ddluomheight.DataBind();
            ddluommaxweight.DataSource = _ListItems;
            ddluommaxweight.DataTextField = "msu_cd";
            ddluommaxweight.DataValueField = "msu_cd";
            ddluommaxweight.DataBind();
            ddluomtot.DataSource = _ListItems;
            ddluomtot.DataTextField = "msu_cd";
            ddluomtot.DataValueField = "msu_cd";
            ddluomtot.DataBind();
            ddluomutiliz.DataSource = _ListItems;
            ddluomutiliz.DataTextField = "msu_cd";
            ddluomutiliz.DataValueField = "msu_cd";
            ddluomutiliz.DataBind();
            ddluomfree.DataSource = _ListItems;
            ddluomfree.DataTextField = "msu_cd";
            ddluomfree.DataValueField = "msu_cd";
            ddluomfree.DataBind();
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
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append(txtmaincat.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtmaincat.Text + seperator + txtsubcat1.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtmaincat.Text + seperator + txtsubcat1.Text.Trim() + seperator + txtsubcat2.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(txtmaincat.Text + seperator + txtsubcat1.Text.Trim() + seperator + txtsubcat2.Text.Trim() + seperator + txtsubcat3.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub4:
                    {
                        paramsText.Append(txtmaincat.Text + seperator + txtsubcat1.Text.Trim() + seperator + txtsubcat2.Text.Trim() + seperator + txtsubcat3.Text.Trim() + seperator + txtsubcat4.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LoadingBayLocation:
                    {
                        paramsText.Append(Session["LoadingBayUserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void generatePrefixforBin(string level)
        {
            txtPrefixBin.Text = lblAisleValue.Text + lblRowvalue.Text + lblLevelvalue.Text + level;
        }
        private void generateBinLoc()
        {
            txtBinLocation.Text = txtBinZone.Text + txtBinAisle.Text + txtBinRow.Text + txtBinLevel.Text;
        }
        private void GetUpZone()
        {

        }
        private void TableID()
        {
            DataTable _tbl = new DataTable();
            DataRow dr = null;
            _tbl.TableName = "TableIDs";
            _tbl.Columns.Add(new DataColumn("AisleID", typeof(string)));
            _tbl.Columns.Add(new DataColumn("RowID", typeof(string)));
            _tbl.Columns.Add(new DataColumn("LevelID", typeof(string)));
            dr = _tbl.NewRow();
            dr["AisleID"] = lblAisleValue.Text;
            dr["RowID"] = lblRowvalue.Text;
            dr["LevelID"] = lblLevelvalue.Text;
            _tbl.Rows.Add(dr);
            ViewState["TableID"] = _tbl;
        }
        private void SaveExcel()
        {
            Int32 row_aff = 0;
            string _Error;
            _ZoneList = ViewState["ZoneList"] as List<WarehouseZone>;
            _AisleList = ViewState["AisleList"] as List<WarehouseAisle>;
            _RowList = ViewState["RowList"] as List<WarehouseRow>;
            _LevelList = ViewState["LevelList"] as List<WarehouseLevel>;
            _WarehouseBin = ViewState["Bin"] as List<WarehouseBin>;
            row_aff = (Int32)CHNLSVC.General.SaveWarehouseByExcel(_ZoneList, _AisleList, _RowList, _LevelList, _WarehouseBin, out _Error);
            if (row_aff == 1)
            {
                SuccessMsg("Successfully saved the uploaded excel sheet...!");
                // ClearPage();
                // BuildTree(_ZoneList);
                lblsuccess.Text = "Successfully saved the uploaded excel sheet data...!";
                lblsuccess.Visible = true;
                lblalert.Visible = false;
                divUpcompleted.Visible = false;
                excelUpload.Show();

            }
            else
            {
                ErrorMsg(_Error);
            }
        }
        private void SaveLayout()
        {
            if (CheckDefault())
            {
                Int32 row_aff = 0;
                _ZoneList = ViewState["ZoneList"] as List<WarehouseZone>;
                _AisleList = ViewState["AisleList"] as List<WarehouseAisle>;
                _RowList = ViewState["RowList"] as List<WarehouseRow>;
                _LevelList = ViewState["LevelList"] as List<WarehouseLevel>;

                TableID();
                DataTable _resultId = ViewState["TableID"] as DataTable;

                //if (Session["Islayoutupdate"].ToString() == "true")
                //{
                //   // GetUpZone();
                foreach (GridViewRow row in grdZone.Rows)
                {
                    WarehouseZone _UpWarehouseZone = new WarehouseZone();
                    Label Seq = (Label)row.FindControl("chk_zseq");
                    Label ID = (Label)row.FindControl("col_znId");
                    CheckBox IsDefault = (CheckBox)row.FindControl("chk_zoneIsDgrd");
                    CheckBox Active = (CheckBox)row.FindControl("chk_zoneactivegrd");

                    var obj = _ZoneList.FirstOrDefault(x => x.Iz_zn_id == ID.Text);
                    if (obj != null)
                    {
                        obj.Iz_is_def = (IsDefault.Checked == true) ? true : false; ;
                        obj.Iz_act = (Active.Checked == true) ? true : false;
                        obj.Iz_mod_by = Session["UserID"].ToString();
                        obj.Iz_mod_when = System.DateTime.Now;
                    }
                    else
                    {
                        //_ZoneList.Add(_UpWarehouseZone);
                    }

                }
                foreach (GridViewRow row in grdAisle.Rows)
                {
                    WarehouseAisle _UpWarehouseAisle = new WarehouseAisle();
                    Label Seq = (Label)row.FindControl("col_Aseq");
                    Label ID = (Label)row.FindControl("col_AisleId");
                    CheckBox IsDefault = (CheckBox)row.FindControl("chk_AisleIsDgrd");
                    CheckBox Active = (CheckBox)row.FindControl("chk_Aisleactivegrd");

                    var obj = _AisleList.FirstOrDefault(x => x.Ia_asl_id == ID.Text);
                    if (obj != null)
                    {
                        obj.Ia_is_def = (IsDefault.Checked == true) ? true : false; ;
                        obj.Ia_act = (Active.Checked == true) ? true : false;
                        obj.Ia_mod_by = Session["UserID"].ToString();
                        obj.Ia_mod_when = System.DateTime.Now;
                        //obj.Ia_zn_seq = Convert.ToInt32(Session["Zseq"].ToString());
                    }
                    else
                    {
                        //_AisleList.Add(_UpWarehouseAisle);
                    }

                }
                foreach (GridViewRow row in grdRow.Rows)
                {
                    WarehouseRow _UpWarehouseRow = new WarehouseRow();
                    Label Seq = (Label)row.FindControl("col_Rseq");
                    Label ID = (Label)row.FindControl("col_RowId");
                    CheckBox IsDefault = (CheckBox)row.FindControl("chk_RowIsDgrd");
                    CheckBox Active = (CheckBox)row.FindControl("chk_Rowactivegrd");
                    _UpWarehouseRow.Ir_zn_seq = Convert.ToInt32(Seq.Text);
                    _UpWarehouseRow.Ir_is_def = (IsDefault.Checked == true) ? true : false; ;
                    _UpWarehouseRow.Ir_act = (Active.Checked == true) ? true : false;
                    _UpWarehouseRow.Ir_mod_by = Session["UserID"].ToString();
                    _UpWarehouseRow.Ir_mod_when = System.DateTime.Now;

                    var obj = _RowList.FirstOrDefault(x => x.Ir_row_id == ID.Text);
                    if (obj != null)
                    {
                        obj.Ir_is_def = (IsDefault.Checked == true) ? true : false; ;
                        obj.Ir_act = (Active.Checked == true) ? true : false;
                        obj.Ir_mod_by = Session["UserID"].ToString();
                        obj.Ir_mod_when = System.DateTime.Now;
                        // obj.Ir_zn_seq = Convert.ToInt32(Session["Zseq"].ToString());
                        // obj.Ir_asl_seq = Convert.ToInt32(Session["AisleSeq"].ToString());
                    }
                    else
                    {
                        //_RowList.Add(_UpWarehouseRow);;
                    }

                }
                foreach (GridViewRow row in grdlevel.Rows)
                {
                    WarehouseLevel _UpWarehouseLevel = new WarehouseLevel();
                    Label Seq = (Label)row.FindControl("col_lseq");
                    Label ID = (Label)row.FindControl("col_LevelId");
                    CheckBox IsDefault = (CheckBox)row.FindControl("chk_LevelIsDgrd");
                    CheckBox Active = (CheckBox)row.FindControl("chk_Levelactivegrd");

                    var obj = _LevelList.FirstOrDefault(x => x.Il_lvl_id == ID.Text);
                    if (obj != null)
                    {
                        obj.Il_is_def = (IsDefault.Checked == true) ? true : false; ;
                        obj.Il_act = (Active.Checked == true) ? true : false;
                        obj.Il_mod_by = Session["UserID"].ToString();
                        obj.Il_mod_when = System.DateTime.Now;
                        // obj.Il_zn_seq = Convert.ToInt32(Session["Zseq"].ToString());
                        //obj.Il_asl_seq = Convert.ToInt32(Session["AisleSeq"].ToString());
                        //obj.Il_row_seq = Convert.ToInt32(Session["RowSeq"].ToString());
                    }
                    else
                    {
                        // _LevelList.Add(_UpWarehouseLevel);
                    }
                }
                //    row_aff = (Int32)CHNLSVC.General.UpdateWarehouseZone(_ZoneList, _AisleList, _RowList,_LevelList);
                //    if (row_aff == 1)
                //    {
                //        SuccessMsg("Successfully update Layout");                    
                //    }
                //    return;
                //}

                row_aff = (Int32)CHNLSVC.General.SaveWarehouseZone(_ZoneList, _AisleList, _RowList, _LevelList, _resultId);
                if (row_aff == 1)
                {
                    SuccessMsg("Successfully saved the layout details...!");
                    //ClearPage();
                    _ZoneList = ViewState["ZoneList"] as List<WarehouseZone>;
                    BuildTree(_ZoneList);

                    if (_ZoneList != null)
                    {
                        grdZone.DataSource = _ZoneList;
                        grdZone.DataBind();

                        _AisleList = new List<WarehouseAisle>();
                        _RowList = new List<WarehouseRow>();
                        _LevelList = new List<WarehouseLevel>();
                        ViewState["_AisleList"] = null;
                        ViewState["_RowList"] = null;
                        ViewState["_LevelList"] = null;
                        grdlevel.DataSource = _LevelList;
                        grdlevel.DataBind();
                        grdAisle.DataSource = _AisleList;
                        grdAisle.DataBind();

                        grdRow.DataSource = _RowList;
                        grdRow.DataBind();

                    }

                }
            }
        }
        private void SaveBin()
        {
            Int32 row_aff = 0;


            _WarehouseBin = ViewState["BinDetails"] as List<WarehouseBin>;
            if (_WarehouseBin == null)
            {
                _WarehouseBin = new List<WarehouseBin>();
            }
            if (_WarehouseBin.Count > 0)
            {

                var _filtedef2 = _WarehouseBin.Where(x => x.Ibn_is_def == true && x.Ibn_bin_cd == txtBinLocation.Text).ToList();
                if (_filtedef2.Count > 0)
                {

                }
                else
                {
                    var _filtedef = _WarehouseBin.Where(x => x.Ibn_is_def == true).ToList();
                    if (_filtedef.Count > 0)
                    {
                        if (chk_BinIsDef.Checked == true)
                        {
                            ErrorBinMsg("cant create default bins more than one");
                            return;
                        }
                    }
                }

            }
            if (string.IsNullOrEmpty(txtTotspace.Text))
            {
                ErrorBinMsg("please add values for total space");
                return;
            }
            WarehouseBin _Bin = new WarehouseBin();
            _Bin.Ibn_act = (chk_BinActive.Checked == true) ? true : false;
            _Bin.Ibn_asl_seq = Convert.ToInt32(Session["AisleSeq"].ToString());
            _Bin.Ibn_bin_cd = txtBinLocation.Text;
            _Bin.Ibn_bin_desc = txtBinDes.Text;

            _Bin.Ibn_capacity = Convert.ToDecimal(txtTotspace.Text);
            _Bin.Ibn_capacity_used = 0;
            _Bin.Ibn_com_cd = Session["UserCompanyCode"].ToString();
            _Bin.Ibn_cre_by = Session["UserID"].ToString();
            _Bin.Ibn_cre_when = System.DateTime.Now;
            _Bin.Ibn_height = Convert.ToDecimal(txtheight.Text);
            _Bin.Ibn_is_def = (chk_BinIsDef.Checked == true) ? true : false;
            _Bin.Ibn_length = Convert.ToDecimal(txtlength.Text);
            _Bin.Ibn_loc_cd = txtBinStore.Text;
            _Bin.Ibn_lvl_seq = Convert.ToInt32(Session["LevelSeq"].ToString());
            _Bin.Ibn_row_seq = Convert.ToInt32(Session["RowSeq"].ToString());
            _Bin.Ibn_session_id = Session["SessionID"].ToString();
            _Bin.Ibn_weight = Convert.ToDecimal(txtMaxweight.Text);
            _Bin.Ibn_width = Convert.ToDecimal(txtwidth.Text);
            _Bin.Ibn_zn_seq = Convert.ToInt32(Session["ZoneSeq"].ToString());
            _Bin.Ibn_tp = ddlbintype.SelectedValue;
            _Bin.Ibn_weight_uom = ddluommaxweight.SelectedValue;
            _Bin.Ibn_diam_uom = ddluomheight.SelectedValue;

            // _WarehouseBin = new List<WarehouseBin>();

            if (Session["Isupdate"].ToString() == "true")
            {
                _Bin.Ibn_bin_seq = Convert.ToInt32(Session["BinSeq"].ToString());
                _Bin.Ibn_mod_by = Session["UserID"].ToString();
                _Bin.Ibn_mod_when = System.DateTime.Now;
                _WarehouseBin.Add(_Bin);

                foreach (GridViewRow row in grdstoreFacility.Rows)
                {
                    warehouseStorageFacility _StorageFacility = new warehouseStorageFacility();
                    Label storecode = (Label)row.FindControl("col_storecode");
                    CheckBox Active = (CheckBox)row.FindControl("chk_Facility");
                    // _Bin.Ibn_zn_seq = Convert.ToInt32(Session["ZoneSeq"].ToString());
                    _StorageFacility.Ibns_stor_cd = storecode.Text;
                    _StorageFacility.Ibns_act = (Active.Checked == true) ? true : false;
                    _StorageFacility.Ibns_mod_by = Session["UserID"].ToString(); ;
                    _StorageFacility.Ibns_mod_when = System.DateTime.Now;
                    _StorageFacility.Ibns_bin_seq = Convert.ToInt32(Session["BinSeq"].ToString());
                    _StorageFacility.Ibns_cre_by = Session["UserID"].ToString();
                    _StorageFacility.Ibns_cre_when = System.DateTime.Now;
                    _StorageFacility.Ibns_session_id = Session["SessionID"].ToString();
                    _warehouseStorageFacility.Add(_StorageFacility);

                }
                foreach (GridViewRow row in grdBinItem.Rows)
                {
                    warehouseBinItems _BinItems = new warehouseBinItems();

                    Label Cd1 = (Label)row.FindControl("col_IBNI_CAT_CD1");
                    Label Cd2 = (Label)row.FindControl("col_IBNI_CAT_CD2");
                    Label Cd3 = (Label)row.FindControl("IBNI_CAT_CD3");
                    Label Cd4 = (Label)row.FindControl("col_IBNI_CAT_CD4");
                    Label Cd5 = (Label)row.FindControl("col_IBNI_CAT_CD5");
                    Label line = (Label)row.FindControl("col_itemline");

                    CheckBox Active = (CheckBox)row.FindControl("chk_BinItem");

                    _BinItems.Ibni_bin_seq = Convert.ToInt32(Session["BinSeq"].ToString());
                    _BinItems.Ibni_cat_cd1 = Cd1.Text;
                    _BinItems.Ibni_cat_cd2 = Cd2.Text;
                    _BinItems.Ibni_cat_cd3 = Cd3.Text;
                    _BinItems.Ibni_cat_cd4 = Cd4.Text;
                    _BinItems.Ibni_cat_cd5 = Cd5.Text;
                    _BinItems.Ibni_cat_line = Convert.ToInt32(line.Text);
                    _BinItems.Ibni_mod_by = Session["UserID"].ToString();
                    _BinItems.Ibni_mod_when = System.DateTime.Now;
                    _BinItems.Ibni_act = (Active.Checked == true) ? true : false;
                    _BinItems.Ibni_cre_by = Session["UserID"].ToString();
                    _BinItems.Ibni_cre_when = System.DateTime.Now;
                    _BinItems.Ibni_session_id = Session["SessionID"].ToString();
                    _warehouseBinItems.Add(_BinItems);

                }
                row_aff = (Int32)CHNLSVC.General.UpdateWarehouseBin(_WarehouseBin, _warehouseStorageFacility, _warehouseBinItems);


                if (row_aff == 1)
                {
                    SuccessBinMsg("Successfully updated the bin details...! ");
                    Session["Isupdate"] = "";
                }
                return;
            }
            else
            {
                List<WarehouseBin> _CheckWarehouseBin = new List<WarehouseBin>();
                _BinDetas = CHNLSVC.General.GetWarehouseBin(0, txtBinStore.Text, Session["UserCompanyCode"].ToString(), Convert.ToInt32(Session["ZoneSeq"].ToString()), Convert.ToInt32(Session["AisleSeq"].ToString()), Convert.ToInt32(Session["RowSeq"].ToString()), Convert.ToInt32(Session["LevelSeq"].ToString()));
                _CheckWarehouseBin = _BinDetas.Item1;
                if (_CheckWarehouseBin != null)
                {


                    var filterbincode = _CheckWarehouseBin.Where(x => x.Ibn_bin_cd == txtBinLocation.Text).ToList();
                    if (filterbincode.Count > 0)
                    {
                        ErrorBinMsg("Bin code is already available");
                        return;
                    }
                }
                _WarehouseBin.Add(_Bin);
                _warehouseStorageFacility = ViewState["Store"] as List<warehouseStorageFacility>;
                _warehouseBinItems = ViewState["BinItem"] as List<warehouseBinItems>;
                row_aff = (Int32)CHNLSVC.General.SaveWarehouseBin(_WarehouseBin, _warehouseStorageFacility, _warehouseBinItems);
                if (row_aff == 1)
                {
                    SuccessBinMsg("Successfully saved the bin details...!");
                    Session["Isupdate"] = "";
                    ClearBin();
                }
            }

        }
        //protected void checkGridzoneDef_Click(object sender, EventArgs e)
        //{
        //    bool _isdefalt = false;
        //    if (grdZone.Rows.Count == 0) return;         
        //    var lb = (CheckBox)sender;
        //    var row = (GridViewRow)lb.NamingContainer;
        //    if (row != null)
        //    {
        //        foreach (GridViewRow zrow in grdZone.Rows)
        //        {

        //            CheckBox IsDefault = (CheckBox)row.FindControl("chk_zoneIsDgrd");
        //            if (IsDefault.Checked == true)
        //            {
        //                _isdefalt = true;
        //            }

        //        }

        //        CheckBox _Zcheck = (row.FindControl("chk_zoneIsDgrd") as CheckBox);
        //        if (_Zcheck.Checked == true)
        //        {

        //            if (_isdefalt ==true)
        //            {
        //                ErrorMsg("More than one Default Zone already exist..!");
        //                _Zcheck.Checked = false;
        //            }
        //        }
        //    }
        //}
        private bool CheckDefault()
        {
            int Zcount = 0;
            int Acount = 0;
            int Rcount = 0;
            int Lcount = 0;
            foreach (GridViewRow row in grdZone.Rows)
            {

                CheckBox IsDefault = (CheckBox)row.FindControl("chk_zoneIsDgrd");
                if (IsDefault.Checked == true)
                {
                    Zcount++;
                }
                if (Zcount > 1)
                {
                    ErrorMsg("You cannot keep more than one default zone within one store");
                    IsDefault.Checked = false;
                    return false;
                }
            }
            foreach (GridViewRow row in grdAisle.Rows)
            {

                CheckBox IsDefault = (CheckBox)row.FindControl("chk_AisleIsDgrd");
                if (IsDefault.Checked == true)
                {
                    Acount++;
                }
                if (Acount > 1)
                {
                    ErrorMsg("You cannot keep more than one default aisle within one zone");
                    IsDefault.Checked = false;
                    return false;
                }
            }
            foreach (GridViewRow row in grdRow.Rows)
            {

                CheckBox IsDefault = (CheckBox)row.FindControl("chk_RowIsDgrd");
                if (IsDefault.Checked == true)
                {
                    Rcount++;
                }
                if (Rcount > 1)
                {
                    ErrorMsg("You cannot keep more than one default row within one aisle");
                    IsDefault.Checked = false;
                    return false;
                }
            }
            foreach (GridViewRow row in grdlevel.Rows)
            {

                CheckBox IsDefault = (CheckBox)row.FindControl("chk_LevelIsDgrd");
                if (IsDefault.Checked == true)
                {
                    Lcount++;
                }
                if (Lcount > 1)
                {
                    ErrorMsg("You cannot keep more than one default level within one row");
                    IsDefault.Checked = false;
                    return false;
                }
            }
            return true;
        }
        private void GetStorageFacility()
        {
            _result = CHNLSVC.General.GetWarehouseStorage();
            ddlStorageF.DataSource = _result;
            ddlStorageF.DataTextField = "rs_desc";
            ddlStorageF.DataValueField = "rs_cd";
            ddlStorageF.DataBind();
        }
        private void GetBinType()
        {
            DataTable _resultbintype = CHNLSVC.General.GetBinType();
            ddlbintype.DataSource = _resultbintype;
            ddlbintype.DataTextField = "bt_desc";
            ddlbintype.DataValueField = "bt_tp";
            ddlbintype.DataBind();
        }
        private void FilterData()
        {
            _result = (DataTable)ViewState["SEARCH"];
            DataView dv = new DataView(_result);
            string searchParameter = ddlSearchbykey.Text;
            dv.RowFilter = "" + ddlSearchbykey.Text + " LIKE '%" + txtSearchbyword.Text + "%'";
            // dv.RowFilter = "REFERENCESNO = '" + txtSearchbyword.Text + "' ";
            if (dv.Count > 0)
            {
                _result = dv.ToTable();
            }
            grdResult.DataSource = _result;
            grdResult.DataBind();
            UserPopoup.Show();
        }
        private void BuildTree(List<WarehouseZone> _TreeZoneList)
        {
            try
            {
                // DataTable dt_tree = CHNLSVC.Financial.GetCostCatergoryMaster("");
                treeViewLayout.Nodes.Clear();

                foreach (WarehouseZone _Zone in _TreeZoneList)
                {

                    TreeNode tnParent = new TreeNode();

                    tnParent.Text = _Zone.Iz_zn_id;

                    tnParent.Value = _Zone.Iz_zn_id;

                    tnParent.PopulateOnDemand = true;

                    tnParent.ToolTip = "Click to get Child";

                    tnParent.SelectAction = TreeNodeSelectAction.SelectExpand;

                    //tnParent.Expand();

                    tnParent.Selected = true;
                    string Zseq = _Zone.Iz_zn_seq.ToString();
                    treeViewLayout.Nodes.Add(tnParent);
                    FillAisle(tnParent, Zseq);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        public void FillAisle(TreeNode parent, string _Zseq)
        {

            parent.ChildNodes.Clear();

            _AisleList = CHNLSVC.General.GetWarehouseAisle(Session["UserCompanyCode"].ToString(), txtStorecode.Text, _Zseq);
            if (_AisleList != null)
            {
                foreach (WarehouseAisle _Aisle in _AisleList)
                {

                    TreeNode child = new TreeNode();

                    child.Text = _Aisle.Ia_asl_id;

                    child.Value = _Aisle.Ia_asl_id;

                    if (child.ChildNodes.Count == 0)
                    {

                        child.PopulateOnDemand = true;

                    }

                    child.ToolTip = "Click to get Child";

                    child.SelectAction = TreeNodeSelectAction.SelectExpand;

                    child.CollapseAll();

                    parent.ChildNodes.Add(child);
                    string Aseq = _Aisle.Ia_asl_seq.ToString();
                    FillRow(child, _Zseq, Aseq);
                }

            }

        }
        public void FillRow(TreeNode parent, string _Zseq, string _Aseq)
        {

            parent.ChildNodes.Clear();
            _RowList = CHNLSVC.General.GetWarehouseRow(Session["UserCompanyCode"].ToString(), txtStorecode.Text, _Zseq, _Aseq);

            if (_RowList != null)
            {
                foreach (WarehouseRow _Row in _RowList)
                {

                    TreeNode child = new TreeNode();

                    child.Text = _Row.Ir_row_id;

                    child.Value = _Row.Ir_row_id;

                    if (child.ChildNodes.Count == 0)
                    {

                        child.PopulateOnDemand = true;

                    }

                    child.ToolTip = "Click to get Child";

                    child.SelectAction = TreeNodeSelectAction.SelectExpand;

                    child.CollapseAll();

                    parent.ChildNodes.Add(child);
                    string Rseq = _Row.Ir_row_seq.ToString();
                    FillLevel(child, _Zseq, _Aseq, Rseq);
                }

            }

        }
        public void FillLevel(TreeNode parent, string _Zseq, string _Aseq, string _Rseq)
        {

            parent.ChildNodes.Clear();
            _LevelList = CHNLSVC.General.GetWarehouseLevel(Session["UserCompanyCode"].ToString(), txtStorecode.Text, _Zseq, _Aseq, _Rseq);

            if (_LevelList != null)
            {
                foreach (WarehouseLevel _RLevel in _LevelList)
                {

                    TreeNode child = new TreeNode();

                    child.Text = _RLevel.Il_lvl_id;

                    child.Value = _RLevel.Il_lvl_id;

                    if (child.ChildNodes.Count == 0)
                    {

                        child.PopulateOnDemand = true;

                    }

                    child.ToolTip = "Click to get Child";

                    child.SelectAction = TreeNodeSelectAction.SelectExpand;

                    child.CollapseAll();

                    parent.ChildNodes.Add(child);
                    // SUBFillChild(child, child.Value);
                }

            }

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
        public DataTable[] LoadData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Zonetbl = new DataTable();
            DataTable Aisletbl = new DataTable();
            DataTable Rowtbl = new DataTable();
            DataTable Leveltbl = new DataTable();
            DataTable Bintbl = new DataTable();

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
                try
                {
                    cmdExcel.CommandText = "SELECT F1,F2 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Zonetbl);

                    cmdExcel.CommandText = "SELECT F2,F3,F4 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Aisletbl);

                    cmdExcel.CommandText = "SELECT F2,F4,F5,F6 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Rowtbl);


                    cmdExcel.CommandText = "SELECT F2,F4,F6,F7,F8 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Leveltbl);


                    cmdExcel.CommandText = "SELECT F2,F4,F6,F8,F9,F10 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Bintbl);
                    cn.Close();
                }


                catch (Exception ex)
                {

                    lblalert.Visible = true;
                    lblalert.Text = "Invalid data found from the excel sheet. Please check data ...";
                    excelUpload.Show();
                    return new DataTable[] { Zonetbl, Aisletbl, Rowtbl, Leveltbl, Bintbl };

                }

                return new DataTable[] { Zonetbl, Aisletbl, Rowtbl, Leveltbl, Bintbl };

            }


        }

        public static Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }
        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            string Des = grdResult.SelectedRow.Cells[2].Text;
            if (lblvalue.Text == "5")
            {
                txtBinzoneS.Text = string.Empty;
                txtBinAisleS.Text = string.Empty;
                txtBinRowS.Text = string.Empty;
                txtBinLevelS.Text = string.Empty;
                lblAisleValue.Text = string.Empty;
                lblRowvalue.Text = string.Empty;
                lblLevelvalue.Text = string.Empty;
                txtStorecode.Text = ID;
                txtWarehouse.Text = Des;
                txtBinStore.Text = ID;
                txtBinwarehouse.Text = Des;

                _ZoneList = CHNLSVC.General.GetWarehouseZone(Session["UserCompanyCode"].ToString(), ID);
                if (_ZoneList != null)
                {
                    grdZone.DataSource = _ZoneList;
                    grdZone.DataBind();
                    ViewState["ZoneList"] = _ZoneList;
                    Islayoutupdate = true;
                    Session["Islayoutupdate"] = "true";
                    BuildTree(_ZoneList);
                    ClearBin();
                    return;
                }

                txtBinzoneS.Text = string.Empty;
                txtBinAisleS.Text = string.Empty;
                txtBinRowS.Text = string.Empty;
                txtBinLevelS.Text = string.Empty;
                grdZone.DataSource = new int[] { };
                grdZone.DataBind();
                grdAisle.DataSource = new int[] { };
                grdAisle.DataBind();
                grdRow.DataSource = new int[] { };
                grdRow.DataBind();
                grdlevel.DataSource = new int[] { };
                grdlevel.DataBind();
                grdBinDeatils.DataSource = new int[] { };
                grdBinDeatils.DataBind();
                lblvalue.Text = "";
                treeViewLayout.Nodes.Clear();

            }
            else if (lblvalue.Text == "zone")
            {

                txtBinZone.Text = ID;
                txtBinzoneS.Text = ID;
                Session["ZoneSeq"] = grdResult.SelectedRow.Cells[3].Text;
                generateBinLoc();
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "Aisle")
            {

                txtBinAisle.Text = ID;
                txtBinAisleS.Text = ID;
                Session["AisleSeq"] = grdResult.SelectedRow.Cells[3].Text;
                generateBinLoc();
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "Row")
            {

                txtBinRow.Text = ID;
                txtBinRowS.Text = ID;
                Session["RowSeq"] = grdResult.SelectedRow.Cells[3].Text;
                generateBinLoc();
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "level")
            {

                txtBinLevel.Text = ID;
                txtBinLevelS.Text = ID;
                Session["LevelSeq"] = grdResult.SelectedRow.Cells[3].Text;
                generateBinLoc();
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "MainCat")
            {

                txtmaincat.Text = ID;
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "Subcat1")
            {
                txtsubcat1.Text = ID;
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "Subcat2")
            {
                txtsubcat2.Text = ID;
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "Subcat3")
            {
                txtsubcat3.Text = ID;
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "Subcat4")
            {
                txtsubcat4.Text = ID;
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "userId")
            {
                txtUserId.Text = ID;
                lblUserNameView.Text = Des;
                lblvalue.Text = "";
                Session["LoadingBayUserID"] = txtUserId.Text;
            }
            else if (lblvalue.Text == "locationId")
            {
                txtLocationID.Text = ID;
                ViewState["LocId"] = ID;
                lblLocationView.Text = Des;
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "locationBayId")
            {
                txtLoadingBayID.Text = ID;
                ViewState["LocBayId"] = ID;
                lblLoadingBayView.Text = Des;
                //chkactive.Checked = grdResult.SelectedRow.Cells[3].Text == "1" ? true : false;
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "locationBayDocNo")
            {
                txtDocNo.Text = ID;
                ViewState["LocBayDoc"] = ID;
                lblDocNoView.Text = Des;
                //chkactive.Checked = grdResult.SelectedRow.Cells[3].Text == "1" ? true : false;
                lblvalue.Text = "";
            }
            else if (lblvalue.Text == "locationDocBayId")
            {
                txtDocLoadBay.Text = ID;
                ViewState["LocDocBay"] = ID;
                lblLoadingDocBayView.Text = Des;
                //chkactive.Checked = grdResult.SelectedRow.Cells[3].Text == "1" ? true : false;
                lblvalue.Text = "";
            }
            
            UserPopoup.Hide();
        }
        private void ClearBin()
        {
            grdAisle.DataSource = new int[] { };
            grdAisle.DataBind();
            grdRow.DataSource = new int[] { };
            grdRow.DataBind();
            grdlevel.DataSource = new int[] { };
            grdlevel.DataBind();
            grdBinDeatils.DataSource = new int[] { };
            grdBinDeatils.DataBind();
            UserPopoup.Hide();
            lblvalue.Text = "";
            txtBinLevelS.Text = string.Empty;
            txtBinRowS.Text = string.Empty;
            txtBinAisleS.Text = string.Empty;
            txtBinzoneS.Text = string.Empty;

            txtBinZone.Text = string.Empty;
            txtBinAisle.Text = string.Empty;
            txtBinRow.Text = string.Empty;
            txtBinLevel.Text = string.Empty;
            txtBinLocation.Text = string.Empty;
            txtBinDes.Text = string.Empty;
            chk_BinIsDef.Checked = false;
            chk_BinActive.Checked = false;
            txtwidth.Text = string.Empty;
            txtlength.Text = string.Empty;
            ddluomwidth.SelectedIndex = -1;
            ddluomlength.SelectedIndex = -1;
            txtheight.Text = string.Empty;
            ddluomheight.SelectedIndex = -1;
            txtMaxweight.Text = string.Empty;
            ddluommaxweight.SelectedIndex = -1;
            txtTotspace.Text = string.Empty;
            ddluomtot.SelectedIndex = -1;
            txtutility.Text = string.Empty;
            ddluomutiliz.SelectedIndex = -1;
            txtfree.Text = string.Empty;
            ddluomfree.SelectedIndex = -1;
            ddlStorageF.SelectedIndex = -1;
            grdstoreFacility.DataSource = new int[] { };
            grdstoreFacility.DataBind();
            txtsubcat1.Text = string.Empty;
            txtsubcat2.Text = string.Empty;
            txtsubcat3.Text = string.Empty;
            txtsubcat4.Text = string.Empty;
            grdBinItem.DataSource = new int[] { };
            grdBinItem.DataBind();
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "zone")
            {
                _ZoneList = CHNLSVC.General.GetWarehouseZone(Session["UserCompanyCode"].ToString(), txtBinStore.Text);
                DataRow dr = null;
                DataTable _tblzone = new DataTable();
                _tblzone.TableName = "Zone";
                _tblzone.Columns.Add(new DataColumn("Zone", typeof(string)));
                _tblzone.Columns.Add(new DataColumn("Description", typeof(string)));
                _tblzone.Columns.Add(new DataColumn("Number", typeof(int)));

                foreach (WarehouseZone zone in _ZoneList)
                {
                    dr = _tblzone.NewRow();
                    dr["Zone"] = zone.Iz_zn_id;
                    dr["Description"] = zone.Iz_zn_desc;
                    dr["Number"] = zone.Iz_zn_seq;
                    _tblzone.Rows.Add(dr);
                }

                grdResult.DataSource = _tblzone;
                grdResult.DataBind();
                ViewState["SEARCH"] = _tblzone;
                lblvalue.Text = "zone";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Aisle")
            {
                _AisleList = CHNLSVC.General.GetWarehouseAisle(Session["UserCompanyCode"].ToString(), txtStorecode.Text, Session["ZoneSeq"].ToString());
                DataRow dr = null;
                DataTable _tblAisle = new DataTable();
                _tblAisle.TableName = "Aisle";
                _tblAisle.Columns.Add(new DataColumn("Aisle", typeof(string)));
                _tblAisle.Columns.Add(new DataColumn("Description", typeof(string)));


                foreach (WarehouseAisle aisle in _AisleList)
                {
                    dr = _tblAisle.NewRow();
                    dr["Aisle"] = aisle.Ia_asl_id;
                    dr["Description"] = aisle.Ia_asl_desc;
                    _tblAisle.Rows.Add(dr);
                }
                grdResult.DataSource = _tblAisle;
                grdResult.DataBind();
                ViewState["SEARCH"] = _tblAisle;
                lblvalue.Text = "Aisle";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Row")
            {
                _RowList = CHNLSVC.General.GetWarehouseRow(Session["UserCompanyCode"].ToString(), txtStorecode.Text, Session["ZoneSeq"].ToString(), Session["AisleSeq"].ToString());

                DataRow dr = null;
                DataTable _tblRow = new DataTable();
                _tblRow.TableName = "Row";
                _tblRow.Columns.Add(new DataColumn("Row", typeof(string)));
                _tblRow.Columns.Add(new DataColumn("Description", typeof(string)));


                foreach (WarehouseRow Row in _RowList)
                {
                    dr = _tblRow.NewRow();
                    dr["Aisle"] = Row.Ir_row_id;
                    dr["Description"] = Row.Ir_row_desc;
                    _tblRow.Rows.Add(dr);
                }
                grdResult.DataSource = _tblRow;
                grdResult.DataBind();
                ViewState["SEARCH"] = _tblRow;
                lblvalue.Text = "Row";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "level")
            {
                _LevelList = CHNLSVC.General.GetWarehouseLevel(Session["UserCompanyCode"].ToString(), txtStorecode.Text, Session["ZoneSeq"].ToString(), Session["AisleSeq"].ToString(), Session["RowSeq"].ToString());
                DataRow dr = null;
                DataTable _tblLevel = new DataTable();
                _tblLevel.TableName = "Level";
                _tblLevel.Columns.Add(new DataColumn("Level", typeof(string)));
                _tblLevel.Columns.Add(new DataColumn("Description", typeof(string)));


                foreach (WarehouseLevel level in _LevelList)
                {
                    dr = _tblLevel.NewRow();
                    dr["Level"] = level.Il_lvl_id;
                    dr["Description"] = level.Il_lvl_desc;
                    _tblLevel.Rows.Add(dr);
                }
                grdResult.DataSource = _tblLevel;
                grdResult.DataBind();
                ViewState["SEARCH"] = _tblLevel;
                lblvalue.Text = "level";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "MainCat")
            {
                _result = CHNLSVC.General.GetItemCate1_active("", "", "");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "MainCat";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _result = CHNLSVC.General.GetItemSubCate2_active(SearchParams, "", "");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Subcat1";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _result = CHNLSVC.General.GetItemSubCate2_active(SearchParams, "", "");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Subcat2";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                _result = CHNLSVC.General.GetItemSubCate3_active(SearchParams, "", "");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Subcat3";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat4")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                _result = CHNLSVC.General.GetItemSubCate4_active(SearchParams, "", "");
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Subcat4";
                UserPopoup.Show();
            }

            else if (lblvalue.Text == "userId")
            {
                _result = CHNLSVC.CommonSearch.Get_UsersFrompda(Session["UserCompanyCode"].ToString(), ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                lblvalue.Text = "userId";
            }

            else if (lblvalue.Text == "locationId")
            {
                if (Session["LoadingBayUserID"] == null || Session["LoadingBayUserID"].ToString() == "")
                {
                    Session["LoadingBayUserID"] = txtUserId.Text;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoadingBayLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationFrompda(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                txtSearchbyword.Text = ""; txtSearchbyword.Focus();
                UserPopoup.Show();
                lblvalue.Text = "locationId";
                Session["LoadingBayUserID"] = string.Empty;
            }

            else if (lblvalue.Text == "locationBayId")
            {
                string SearchParams = "LB";
                DataTable _result = CHNLSVC.CommonSearch.Get_LoadingBays(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                lblvalue.Text = "locationBayId";
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                UserPopoup.Show();
            }

            else if (lblvalue.Text == "userId")
            {
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result = CHNLSVC.CommonSearch.Get_UsersFrompda(Session["UserCompanyCode"].ToString(), ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "userId";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "zone")
            {
                FilterData();
            }
            else if (lblvalue.Text == "Aisle")
            {
                FilterData();
            }
            else if (lblvalue.Text == "Row")
            {
                FilterData();
            }
            else if (lblvalue.Text == "level")
            {
                FilterData();
            }
            else if (lblvalue.Text == "MainCat")
            {
                _result = CHNLSVC.General.GetItemCate1_active("", ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "MainCat";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _result = CHNLSVC.General.GetItemSubCate2_active(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Subcat1";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _result = CHNLSVC.General.GetItemSubCate2_active(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Subcat2";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                _result = CHNLSVC.General.GetItemSubCate3_active(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Subcat3";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat4")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                _result = CHNLSVC.General.GetItemSubCate4_active(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Subcat4";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "locationId")
            {
                if (Session["LoadingBayUserID"] == null || Session["LoadingBayUserID"].ToString() == "")
                {
                    Session["LoadingBayUserID"] = txtUserId.Text;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoadingBayLocation);
                _result = CHNLSVC.CommonSearch.GetUserLocationFrompda(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "locationId";
                UserPopoup.Show();
                Session["LoadingBayUserID"] = string.Empty;
            }
            else if (lblvalue.Text == "locationBayId")
            {
                string SearchParams = "LB";
                _result = CHNLSVC.CommonSearch.Get_LoadingBays(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                lblvalue.Text = "locationBayId";
            }

        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            if (lblvalue.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "zone")
            {
                FilterData();
            }
            else if (lblvalue.Text == "Aisle")
            {
                FilterData();
            }
            else if (lblvalue.Text == "Row")
            {
                FilterData();
            }
            else if (lblvalue.Text == "level")
            {
                FilterData();
            }
            else if (lblvalue.Text == "MainCat")
            {
                _result = CHNLSVC.General.GetItemCate1_active("", ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "MainCat";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _result = CHNLSVC.General.GetItemSubCate2_active(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Subcat1";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "Subcat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _result = CHNLSVC.General.GetItemSubCate2_active(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Subcat2";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "locationBayDocNo")
            {
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                //_result = CHNLSVC.General.GetItemSubCate3_active(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                string SearchParams = Session["UserCompanyCode"].ToString();
                _result = CHNLSVC.CommonSearch.Get_LoadingBaysDoc(SearchParams, ddlSearchbykey.Text.Trim(), txtSearchbyword.Text.Trim());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "locationBayDocNo";
                UserPopoup.Show();
            }
            else if (lblvalue.Text == "locationDocBayId")
            {
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                //_result = CHNLSVC.General.GetItemSubCate4_active(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                string SearchParams = "LB";
                _result = CHNLSVC.CommonSearch.Get_LoadingBays(SearchParams, ddlSearchbykey.Text.Trim(), txtSearchbyword.Text.Trim());
            
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "locationDocBayId";
                UserPopoup.Show();
            }
        }
        #endregion

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ClearMsg();
            lblalert.Visible = false;
            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {
                    // divalert.Visible = true;
                    lblalert.Visible = true;
                    lblalert.Text = "Please select a valid excel (.xls) file";
                    excelUpload.Show();
                    return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //LoadData(FolderPath + FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload.SaveAs(FilePath);
                Session["FilePath"] = FilePath;
                divUpcompleted.Visible = true;
                excelUpload.Show();
                //Import_To_Grid(FilePath, Extension);
            }
            else
            {
                lblalert.Visible = true;
                //divalert.Visible = true;
                lblalert.Text = "Please select an excel file";
                excelUpload.Show();
            }
        }
        protected void lbtnlayoutplanClear_Click(object sender, EventArgs e)
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

        protected void lbtnlayoutplanSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStorecode.Text))
            {
                ErrorMsg("Please select the store code...!");
                return;
            }
            if (!(grdZone.Rows.Count > 0))
            {
                ErrorMsg("Can't empty zone list...!");
                return;
            }
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }

            SaveLayout();
        }

        protected void lbtnDelLoca_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "5";
            UserPopoup.Show();
        }

        protected void lbtnAddZone_Click(object sender, EventArgs e)
        {
            ViewState["AisleList"] = null;
            ViewState["RowList"] = null;
            ViewState["LevelList"] = null;
            grdlevel.DataSource = new int[] { };
            grdlevel.DataBind();
            grdRow.DataSource = new int[] { };
            grdRow.DataBind();
            grdAisle.DataSource = new int[] { };
            grdAisle.DataBind();
            ClearMsg();
            try
            {
                _ZoneList = ViewState["ZoneList"] as List<WarehouseZone>;
                if (ViewState["ZoneList"] == null)
                {
                    _ZoneList = new List<WarehouseZone>();
                }
                if (string.IsNullOrEmpty(txtStorecode.Text))
                {
                    ErrorMsg("Please select storecode...!");
                    return;
                }
                if (string.IsNullOrEmpty(txtZID.Text))
                {
                    ErrorMsg("Please add zone ID...!");
                    return;
                }
                if (string.IsNullOrEmpty(txtZDes.Text))
                {
                    ErrorMsg("Please add zone description...!");
                    return;
                }

                WarehouseZone _tmpZoneDetails = new WarehouseZone();

                _tmpZoneDetails.Iz_com_cd = Session["UserCompanyCode"].ToString();
                _tmpZoneDetails.Iz_loc_cd = txtStorecode.Text;
                _tmpZoneDetails.Iz_zn_desc = txtZDes.Text;
                _tmpZoneDetails.Iz_zn_id = txtZID.Text;
                _tmpZoneDetails.Iz_is_def = (chk_zoneIsDef.Checked == true) ? true : false;
                _tmpZoneDetails.Iz_act = (chk_zoneActive.Checked == true) ? true : false;
                _tmpZoneDetails.Iz_cre_by = Session["UserID"].ToString();
                _tmpZoneDetails.Iz_cre_when = System.DateTime.Now;
                _tmpZoneDetails.Iz_session_id = Session["SessionID"].ToString();

                var currrange = (from cur in _ZoneList
                                 where cur.Iz_zn_id == txtZID.Text.Trim()
                                 select cur).ToList();
                if (currrange.Count > 0)// ||currrange !=null)
                {
                    txtZID.Text = "";
                    ErrorMsg("Selected zone ID is already exist...!");
                    txtZID.Focus();
                    return;
                }
                var currrangeDes = (from cur in _ZoneList
                                    where cur.Iz_zn_desc == txtZDes.Text.Trim()
                                    select cur).ToList();
                if (currrangeDes.Count > 0)// ||currrange !=null)
                {
                    txtZDes.Text = "";
                    ErrorMsg("Selected zone description is already exist...!");
                    txtZDes.Focus();
                    return;
                }
                if (chk_zoneIsDef.Checked == true)
                {
                    if (_ZoneList.Exists(x => x.Iz_is_def == true))
                    {
                        ErrorMsg("Default zone is already exist...!");
                        chk_zoneIsDef.Checked = false;
                        return;
                    }
                }



                _ZoneList.Add(_tmpZoneDetails);

                //grdZone.AutoGenerateColumns = false;
                grdZone.DataSource = _ZoneList;
                grdZone.DataBind();
                ViewState["ZoneList"] = _ZoneList;

                txtZDes.Text = string.Empty;
                txtZID.Text = string.Empty;
                chk_zoneIsDef.Checked = false;
                chk_zoneActive.Checked = false;

            }
            catch (Exception ex)
            {
                ErrorMsg("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void lbtnAddAisle_Click(object sender, EventArgs e)
        {
            ViewState["RowList"] = null;
            ViewState["LevelList"] = null;
            grdlevel.DataSource = new int[] { };
            grdlevel.DataBind();
            grdRow.DataSource = new int[] { };
            grdRow.DataBind();
            ClearMsg();
            if (string.IsNullOrEmpty(lblAisleValue.Text))
            {
                ErrorMsg("Please select zone ID...!");
                return;
            }
            try
            {
                _AisleList = ViewState["AisleList"] as List<WarehouseAisle>;
                if (ViewState["AisleList"] == null)
                {
                    _AisleList = new List<WarehouseAisle>();
                }
                if (string.IsNullOrEmpty(txtStorecode.Text))
                {
                    ErrorMsg("Please select the store code...!");
                    return;
                }
                if (string.IsNullOrEmpty(txtAId.Text))
                {

                    ErrorMsg("Please add aisle ID...!");
                    return;
                }
                if (string.IsNullOrEmpty(txtADes.Text))
                {
                    ErrorMsg("Please add aisle description..!");
                    return;
                }

                WarehouseAisle _tmpAisleDetails = new WarehouseAisle();

                _tmpAisleDetails.Ia_com_cd = Session["UserCompanyCode"].ToString();
                _tmpAisleDetails.Ia_loc_cd = txtStorecode.Text;
                _tmpAisleDetails.Ia_asl_desc = txtADes.Text;
                _tmpAisleDetails.Ia_asl_id = txtAId.Text;
                _tmpAisleDetails.Ia_is_def = (chk_AisleIsDef.Checked == true) ? true : false;
                _tmpAisleDetails.Ia_act = (chk_AisleActive.Checked == true) ? true : false;
                _tmpAisleDetails.Ia_cre_by = Session["UserID"].ToString();
                _tmpAisleDetails.Ia_cre_when = System.DateTime.Now;
                _tmpAisleDetails.Ia_session_id = Session["SessionID"].ToString();
                if (_AisleList == null)
                {
                    _tmpAisleDetails.Ia_zn_seq = Convert.ToInt32(Session["Zseq"]);
                    _AisleList = new List<WarehouseAisle>();
                    _AisleList.Add(_tmpAisleDetails);
                }
                else
                {
                    var currrange = (from cur in _AisleList
                                     where cur.Ia_asl_id == txtAId.Text.Trim()
                                     select cur).ToList();
                    if (currrange.Count > 0)// ||currrange !=null)
                    {
                        txtAId.Text = "";
                        ErrorMsg("Selected aisle ID is already exist");
                        txtAId.Focus();
                        return;
                    }
                    var currrangeDes = (from cur in _AisleList
                                        where cur.Ia_asl_desc == txtADes.Text.Trim()
                                        select cur).ToList();
                    if (currrangeDes.Count > 0)// ||currrange !=null)
                    {
                        txtADes.Text = "";
                        ErrorMsg("Selected aisle description is already exist");
                        txtADes.Focus();
                        return;
                    }
                    var currrangeIsdefault = (from cur in _AisleList
                                              where cur.Ia_is_def == (chk_AisleIsDef.Checked == true)
                                              select cur).ToList();

                    if (chk_AisleIsDef.Checked == true)
                    {
                        if (_AisleList.Exists(x => x.Ia_is_def == true))
                        {
                            ErrorMsg("You cannot keep more than one default aisle within one zone.");
                            chk_AisleIsDef.Checked = false;
                            return;
                        }
                    }
                    _tmpAisleDetails.Ia_zn_seq = Convert.ToInt32(Session["Zseq"]);

                    _AisleList.Add(_tmpAisleDetails);
                }

                grdAisle.DataSource = _AisleList;
                grdAisle.DataBind();
                ViewState["AisleList"] = _AisleList;
                txtADes.Text = string.Empty;
                txtAId.Text = string.Empty;
                chk_AisleIsDef.Checked = false;
                chk_AisleActive.Checked = false;
            }
            catch (Exception ex)
            {
                ErrorMsg("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        protected void lbtnsZone_Click(object sender, EventArgs e)
        {
            ViewState["AisleList"] = null;
            ViewState["RowList"] = null;
            ViewState["LevelList"] = null;
            grdRow.DataSource = new int[] { };
            grdRow.DataBind();
            grdlevel.DataSource = new int[] { };
            grdlevel.DataBind();
            lblRowvalue.Text = string.Empty;
            lblLevelvalue.Text = string.Empty;
            if (string.IsNullOrEmpty(txtStorecode.Text))
            {
                ErrorMsg("Please select the store code...!");
                return;
            }
            if (grdZone.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _ZID = (row.FindControl("col_znId") as Label).Text;
                string _Zseq = (row.FindControl("chk_zseq") as Label).Text;
                lblAisleValue.Text = _ZID;
                _AisleList = CHNLSVC.General.GetWarehouseAisle(Session["UserCompanyCode"].ToString(), txtStorecode.Text, _Zseq);

                if (_AisleList != null)
                {
                    grdAisle.DataSource = _AisleList;
                    grdAisle.DataBind();
                    ViewState["AisleList"] = _AisleList;
                    Session["Zseq"] = _Zseq;
                    generatePrefixforBin("");
                    return;
                }
                grdAisle.DataSource = new int[] { };
                grdAisle.DataBind();
                grdRow.DataSource = new int[] { };
                grdRow.DataBind();
                grdlevel.DataSource = new int[] { };
                grdlevel.DataBind();
                Session["Zseq"] = _Zseq;
            }
        }
        protected void lbtnsArisle_Click(object sender, EventArgs e)
        {
            ViewState["RowList"] = null;
            ViewState["LevelList"] = null;
            lblLevelvalue.Text = string.Empty;
            if (string.IsNullOrEmpty(txtStorecode.Text))
            {
                ErrorMsg("Please select the store code...!");
                return;
            }
            if (grdAisle.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _ZID = (row.FindControl("col_AisleId") as Label).Text;
                string _Zseq = (row.FindControl("col_AZseq") as Label).Text;
                string _Aseq = (row.FindControl("col_Aseq") as Label).Text;
                lblRowvalue.Text = _ZID;
                _RowList = CHNLSVC.General.GetWarehouseRow(Session["UserCompanyCode"].ToString(), txtStorecode.Text, _Zseq, _Aseq);
                if (_RowList != null)
                {
                    grdRow.DataSource = _RowList;
                    grdRow.DataBind();
                    ViewState["RowList"] = _RowList;
                    generatePrefixforBin("");
                    foreach (WarehouseRow _Row in _RowList)
                    {
                        Session["AisleSeq"] = _Row.Ir_asl_seq;
                    }

                    return;
                }
                else
                {
                    Session["AisleSeq"] = "0";
                }
                grdRow.DataSource = new int[] { };
                grdRow.DataBind();
                ViewState["RowList"] = _RowList;
                Session["AisleSeq"] = _Aseq;

            }
        }
        protected void lbtnsRow_Click(object sender, EventArgs e)
        {
            grdlevel.DataSource = new int[] { };
            grdlevel.DataBind(); ViewState["LevelList"] = null;
            if (string.IsNullOrEmpty(txtStorecode.Text))
            {
                ErrorMsg("Please select the store code...!");
                return;
            }
            if (grdRow.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _ZID = (row.FindControl("col_RowId") as Label).Text;
                string _zseq = (row.FindControl("col_RZseq") as Label).Text;
                string _Aseq = (row.FindControl("col_Aseq") as Label).Text;
                string _Rseq = (row.FindControl("col_Rseq") as Label).Text;
                lblLevelvalue.Text = _ZID;
                _LevelList = CHNLSVC.General.GetWarehouseLevel(Session["UserCompanyCode"].ToString(), txtStorecode.Text, _zseq, _Aseq, _Rseq);
                if (_LevelList != null)
                {
                    grdlevel.DataSource = _LevelList;
                    grdlevel.DataBind();
                    ViewState["LevelList"] = _LevelList;
                    generatePrefixforBin("");
                    foreach (WarehouseLevel _Level in _LevelList)
                    {
                        Session["RowSeq"] = _Level.Il_row_seq;
                    }

                    return;
                }
                else
                {
                    Session["RowSeq"] = "0";
                }
                grdlevel.DataSource = new int[] { };
                grdlevel.DataBind();
                ViewState["LevelList"] = _LevelList;
            }
        }

        protected void lbtnAddRow_Click(object sender, EventArgs e)
        {
            ViewState["LevelList"] = null;
            grdlevel.DataSource = new int[] { };
            grdlevel.DataBind();
            ClearMsg();
            try
            {
                _RowList = ViewState["RowList"] as List<WarehouseRow>;
                if (string.IsNullOrEmpty(lblRowvalue.Text))
                {
                    ErrorMsg("Please select aisle...!");
                    return;
                }
                if (ViewState["RowList"] == null)
                {
                    _RowList = new List<WarehouseRow>();
                }
                if (string.IsNullOrEmpty(txtStorecode.Text))
                {
                    ErrorMsg("Please select the store code...!");
                    return;
                }
                if (string.IsNullOrEmpty(txtRId.Text))
                {
                    ErrorMsg("Please add row ID...!");
                    return;
                }
                if (string.IsNullOrEmpty(txtRDes.Text))
                {
                    ErrorMsg("Please add row description...!");
                    return;
                }

                WarehouseRow _tmpRowDetails = new WarehouseRow();

                _tmpRowDetails.Ir_com_cd = Session["UserCompanyCode"].ToString();
                _tmpRowDetails.Ir_loc_cd = txtStorecode.Text;
                _tmpRowDetails.Ir_row_desc = txtRDes.Text;
                _tmpRowDetails.Ir_row_id = txtRId.Text;
                _tmpRowDetails.Ir_is_def = (chk_RowIsDef.Checked == true) ? true : false;
                _tmpRowDetails.Ir_act = (chk_RowActive.Checked == true) ? true : false;
                _tmpRowDetails.Ir_cre_by = Session["UserID"].ToString();
                _tmpRowDetails.Ir_cre_when = System.DateTime.Now;
                _tmpRowDetails.Ir_session_id = Session["SessionID"].ToString();

                var currrange = (from cur in _RowList
                                 where cur.Ir_row_id == txtRId.Text.Trim()
                                 select cur).ToList();
                if (currrange.Count > 0)// ||currrange !=null)
                {
                    txtRId.Text = "";
                    ErrorMsg("Selected row ID is already exist");
                    txtRId.Focus();
                    return;
                }

                var currrangeDes = (from cur in _RowList
                                    where cur.Ir_row_desc == txtRDes.Text.Trim()
                                    select cur).ToList();
                if (currrangeDes.Count > 0)// ||currrange !=null)
                {
                    txtRDes.Text = "";
                    ErrorMsg("Selected row description is already exist");
                    txtRDes.Focus();
                    return;
                }
                if (chk_RowIsDef.Checked == true)
                {
                    if (_RowList.Exists(x => x.Ir_is_def == true))
                    {
                        ErrorMsg("You cannot keep more than one default row within one aisle.");
                        chk_RowIsDef.Checked = false;
                        return;
                    }
                }
                _tmpRowDetails.Ir_zn_seq = Convert.ToInt32(Session["Zseq"]);
                _tmpRowDetails.Ir_asl_seq = Convert.ToInt32(Session["AisleSeq"]);


                _RowList.Add(_tmpRowDetails);
                //grdZone.AutoGenerateColumns = false;
                grdRow.DataSource = _RowList;
                grdRow.DataBind();
                ViewState["RowList"] = _RowList;
                txtRDes.Text = string.Empty;
                txtRId.Text = string.Empty;
                chk_RowIsDef.Checked = false;
                chk_RowActive.Checked = false;
            }
            catch (Exception ex)
            {
                ErrorMsg("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void lbtnAddlevel_Click(object sender, EventArgs e)
        {
            ClearMsg();
            try
            {
                _LevelList = ViewState["LevelList"] as List<WarehouseLevel>;
                if (string.IsNullOrEmpty(lblLevelvalue.Text))
                {
                    ErrorMsg("Please select row...!");
                    return;
                }
                if (ViewState["LevelList"] == null)
                {
                    _LevelList = new List<WarehouseLevel>();
                }
                if (string.IsNullOrEmpty(txtStorecode.Text))
                {
                    ErrorMsg("Please select the store code...!");
                    return;
                }
                if (string.IsNullOrEmpty(txtlId.Text))
                {

                    ErrorMsg("Please insert level Id...!");
                    return;
                }
                if (string.IsNullOrEmpty(txtlDes.Text))
                {
                    ErrorMsg("Please add level description...!");
                    return;
                }

                WarehouseLevel _tmpLevelDetails = new WarehouseLevel();

                _tmpLevelDetails.Il_com_cd = Session["UserCompanyCode"].ToString();
                _tmpLevelDetails.Il_loc_cd = txtStorecode.Text;
                _tmpLevelDetails.Il_lvl_desc = txtlDes.Text;
                _tmpLevelDetails.Il_lvl_id = txtlId.Text;
                _tmpLevelDetails.Il_is_def = (chk_levelIsDef.Checked == true) ? true : false;
                _tmpLevelDetails.Il_act = (chk_levelActive.Checked == true) ? true : false;
                _tmpLevelDetails.Il_cre_by = Session["UserID"].ToString();
                _tmpLevelDetails.Il_cre_when = System.DateTime.Now;
                _tmpLevelDetails.Il_session_id = Session["SessionID"].ToString();

                var currrange = (from cur in _LevelList
                                 where cur.Il_lvl_id == txtlId.Text.Trim()
                                 select cur).ToList();
                if (currrange.Count > 0)// ||currrange !=null)
                {
                    txtlId.Text = "";
                    ErrorMsg("Selected row ID is already exist");
                    txtlId.Focus();
                    return;
                }

                var currrangeDes = (from cur in _LevelList
                                    where cur.Il_lvl_desc == txtlDes.Text.Trim()
                                    select cur).ToList();
                if (currrangeDes.Count > 0)// ||currrange !=null)
                {
                    txtlDes.Text = "";
                    ErrorMsg("Selected level description is already exist");
                    txtlDes.Focus();
                    return;
                }

                if (chk_levelIsDef.Checked == true)
                {
                    if (_LevelList.Exists(x => x.Il_is_def == true))
                    {
                        ErrorMsg("You cannot keep more than one default level within one row");
                        chk_levelIsDef.Checked = false;
                        return;
                    }
                }
                _tmpLevelDetails.Il_zn_seq = Convert.ToInt32(Session["Zseq"]);
                _tmpLevelDetails.Il_asl_seq = Convert.ToInt32(Session["AisleSeq"]);
                _tmpLevelDetails.Il_row_seq = Convert.ToInt32(Session["RowSeq"]);

                _LevelList.Add(_tmpLevelDetails);
                //grdZone.AutoGenerateColumns = false;
                grdlevel.DataSource = _LevelList;
                grdlevel.DataBind();
                ViewState["LevelList"] = _LevelList;
                txtlDes.Text = string.Empty;
                txtlId.Text = string.Empty;
                chk_levelIsDef.Checked = false;
                chk_levelActive.Checked = false;

            }
            catch (Exception ex)
            {
                ErrorMsg("Error Occurred while processing...");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnlayplaneclose_Click(object sender, EventArgs e)
        {
            ClearMsg();
        }
        protected void lbtnslevel_Click(object sender, EventArgs e)
        {
            if (grdAisle.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _level = (row.FindControl("col_LevelId") as Label).Text;
                generatePrefixforBin(_level);
                return;
            }

        }

        protected void lbtnBinzone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBinStore.Text))
            {
                ErrorBinMsg("Plase select store");
                return;
            }
            _ZoneList = CHNLSVC.General.GetWarehouseZone(Session["UserCompanyCode"].ToString(), txtBinStore.Text);
            if (_ZoneList != null)
            {
                DataRow dr = null;
                DataTable _tblzone = new DataTable();
                _tblzone.TableName = "Zone";
                _tblzone.Columns.Add(new DataColumn("Zone", typeof(string)));
                _tblzone.Columns.Add(new DataColumn("Description", typeof(string)));
                _tblzone.Columns.Add(new DataColumn("Number", typeof(int)));

                foreach (WarehouseZone zone in _ZoneList)
                {
                    dr = _tblzone.NewRow();
                    dr["Zone"] = zone.Iz_zn_id;
                    dr["Description"] = zone.Iz_zn_desc;
                    dr["Number"] = zone.Iz_zn_seq;
                    _tblzone.Rows.Add(dr);
                }

                grdResult.DataSource = _tblzone;
                grdResult.DataBind();
                BindUCtrlDDLData(_tblzone);
                ViewState["SEARCH"] = _tblzone;
                lblvalue.Text = "zone";
                UserPopoup.Show();
                return;
            }
            grdResult.DataSource = new int[] { };
            grdResult.DataBind();
            UserPopoup.Show();

        }

        protected void lbtnBinAisle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBinStore.Text))
            {
                ErrorBinMsg("Plase select store");
                return;
            }
            if (string.IsNullOrEmpty(txtBinZone.Text))
            {
                ErrorBinMsg("Plase select zone ID");
                return;
            }
            _AisleList = CHNLSVC.General.GetWarehouseAisle(Session["UserCompanyCode"].ToString(), txtStorecode.Text, Session["ZoneSeq"].ToString());
            if (_AisleList != null)
            {
                DataRow dr = null;
                DataTable _tblAisle = new DataTable();
                _tblAisle.TableName = "Aisle";
                _tblAisle.Columns.Add(new DataColumn("Aisle", typeof(string)));
                _tblAisle.Columns.Add(new DataColumn("Description", typeof(string)));
                _tblAisle.Columns.Add(new DataColumn("Number", typeof(int)));

                foreach (WarehouseAisle aisle in _AisleList)
                {
                    dr = _tblAisle.NewRow();
                    dr["Aisle"] = aisle.Ia_asl_id;
                    dr["Description"] = aisle.Ia_asl_desc;
                    dr["Number"] = aisle.Ia_asl_seq;
                    _tblAisle.Rows.Add(dr);
                }
                grdResult.DataSource = _tblAisle;
                grdResult.DataBind();
                BindUCtrlDDLData(_tblAisle);
                ViewState["SEARCH"] = _tblAisle;
                lblvalue.Text = "Aisle";
                UserPopoup.Show();
                return;
            }
            grdResult.DataSource = new int[] { };
            grdResult.DataBind();
            UserPopoup.Show();

        }


        protected void lbtnBinRow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBinStore.Text))
            {
                ErrorBinMsg("Plase select store");
                return;
            }
            if (string.IsNullOrEmpty(txtBinZone.Text))
            {
                ErrorBinMsg("Plase select zone ID");
                return;
            }
            if (string.IsNullOrEmpty(txtBinAisle.Text))
            {
                ErrorBinMsg("Plase select aisle ID");
                return;
            }
            _RowList = CHNLSVC.General.GetWarehouseRow(Session["UserCompanyCode"].ToString(), txtStorecode.Text, Session["ZoneSeq"].ToString(), Session["AisleSeq"].ToString());
            if (_RowList != null)
            {
                DataRow dr = null;
                DataTable _tblRow = new DataTable();
                _tblRow.TableName = "Row";
                _tblRow.Columns.Add(new DataColumn("Row", typeof(string)));
                _tblRow.Columns.Add(new DataColumn("Description", typeof(string)));
                _tblRow.Columns.Add(new DataColumn("Number", typeof(int)));

                foreach (WarehouseRow Row in _RowList)
                {
                    dr = _tblRow.NewRow();
                    dr["Row"] = Row.Ir_row_id;
                    dr["Description"] = Row.Ir_row_desc;
                    dr["Number"] = Row.Ir_row_seq;
                    _tblRow.Rows.Add(dr);
                }
                grdResult.DataSource = _tblRow;
                grdResult.DataBind();
                BindUCtrlDDLData(_tblRow);
                ViewState["SEARCH"] = _tblRow;
                lblvalue.Text = "Row";
                UserPopoup.Show();
                return;
            }
            grdResult.DataSource = new int[] { };
            grdResult.DataBind();
            UserPopoup.Show();

        }

        protected void lbtnBinLevel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBinStore.Text))
            {
                ErrorBinMsg("Plase select store");
                return;
            }
            if (string.IsNullOrEmpty(txtBinZone.Text))
            {
                ErrorBinMsg("Plase select zone ID");
                return;
            }
            if (string.IsNullOrEmpty(txtBinAisle.Text))
            {
                ErrorBinMsg("Plase select aisle ID");
                return;
            }
            if (string.IsNullOrEmpty(txtBinRow.Text))
            {
                ErrorBinMsg("Plase select row ID");
                return;
            }

            _LevelList = CHNLSVC.General.GetWarehouseLevel(Session["UserCompanyCode"].ToString(), txtStorecode.Text, Session["ZoneSeq"].ToString(), Session["AisleSeq"].ToString(), Session["RowSeq"].ToString());
            if (_LevelList != null)
            {
                DataRow dr = null;
                DataTable _tblLevel = new DataTable();
                _tblLevel.TableName = "Level";
                _tblLevel.Columns.Add(new DataColumn("Level", typeof(string)));
                _tblLevel.Columns.Add(new DataColumn("Description", typeof(string)));
                _tblLevel.Columns.Add(new DataColumn("Number", typeof(int)));

                foreach (WarehouseLevel level in _LevelList)
                {
                    dr = _tblLevel.NewRow();
                    dr["Level"] = level.Il_lvl_id;
                    dr["Description"] = level.Il_lvl_desc;
                    dr["Number"] = level.Il_lvl_seq;
                    _tblLevel.Rows.Add(dr);
                }
                grdResult.DataSource = _tblLevel;
                grdResult.DataBind();
                BindUCtrlDDLData(_tblLevel);
                ViewState["SEARCH"] = _tblLevel;
                lblvalue.Text = "level";
                UserPopoup.Show();
                return;
            }
            grdResult.DataSource = new int[] { };
            grdResult.DataBind();
            UserPopoup.Show();
        }

        protected void lbtnMacat_Click(object sender, EventArgs e)
        {
            _result = CHNLSVC.General.GetItemCate1_active("", "", "");
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            ViewState["SEARCH"] = _result;
            lblvalue.Text = "MainCat";
            UserPopoup.Show();
        }

        protected void lbtnsubcat1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmaincat.Text))
            {
                ErrorBinMsg("Please select the main catergory...!");
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            _result = CHNLSVC.General.GetItemSubCate2_active(SearchParams, "", "");
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            ViewState["SEARCH"] = _result;
            lblvalue.Text = "Subcat1";
            UserPopoup.Show();
        }

        protected void lbtnsubcat2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmaincat.Text))
            {
                ErrorBinMsg("Please select the main category...!");
                return;
            }
            if (string.IsNullOrEmpty(txtsubcat1.Text))
            {
                ErrorBinMsg("Please select the sub category 1 ...!");
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            _result = CHNLSVC.General.GetItemSubCate3_active(SearchParams, "", "");
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            ViewState["SEARCH"] = _result;
            lblvalue.Text = "Subcat2";
            UserPopoup.Show();
        }

        protected void lbtnsubcat3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmaincat.Text))
            {
                ErrorBinMsg("Please select the main category...!");
                return;
            }
            if (string.IsNullOrEmpty(txtsubcat1.Text))
            {
                ErrorBinMsg("Please select the sub category 1 ...!");
                return;
            }
            if (string.IsNullOrEmpty(txtsubcat2.Text))
            {
                ErrorBinMsg("Please select the sub category 2 ...!");
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            _result = CHNLSVC.General.GetItemSubCate4_active(SearchParams, "", "");
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            ViewState["SEARCH"] = _result;
            lblvalue.Text = "Subcat3";
            UserPopoup.Show();
        }

        protected void lbtnsubcat4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmaincat.Text))
            {
                ErrorBinMsg("Please select the main category...!");
                return;
            }
            if (string.IsNullOrEmpty(txtsubcat1.Text))
            {
                ErrorBinMsg("Please select the sub category 1 ...!");
                return;
            }
            if (string.IsNullOrEmpty(txtsubcat2.Text))
            {
                ErrorBinMsg("Please select the sub category 2 ...!");
                return;
            }
            if (string.IsNullOrEmpty(txtsubcat3.Text))
            {
                ErrorBinMsg("Please select the sub category 3 ...!");
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
            _result = CHNLSVC.General.GetItemSubCate5_active(SearchParams, "", "");
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            ViewState["SEARCH"] = _result;
            lblvalue.Text = "Subcat4";
            UserPopoup.Show();
        }

        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStorecode.Text))
            {
                ErrorMsg("Please select the store code");
                return;
            }

            InforMsg("Please unselect blank cell/cells which you have selected in excel sheet");
            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
            excelUpload.Show();
        }

        protected void btnprocess_Click(object sender, EventArgs e)
        {
            DataTable[] GetExecelTbl = LoadData(Session["FilePath"].ToString());
            DataTable tbl = GetExecelTbl[1].Copy();
            if (GetExecelTbl != null)
            {
                List<WarehouseZone> _ZoneList = null;
                List<WarehouseAisle> _AisleList = null;
                List<WarehouseRow> _RowList = null;
                List<WarehouseLevel> _LevelList = null;
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtStorecode.Text))
                    {
                        ErrorMsg("Please select the store code");
                        lblalert.Visible = true;
                        lblalert.Text = "Please select the store code";
                        excelUpload.Show();
                        return;
                    }

                    _ZoneList = new List<WarehouseZone>();


                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        try
                        {
                            WarehouseZone _Zone = new WarehouseZone();
                            _Zone.Iz_act = true;
                            _Zone.Iz_com_cd = Session["UserCompanyCode"].ToString();
                            _Zone.Iz_cre_by = Session["UserID"].ToString();
                            _Zone.Iz_cre_when = System.DateTime.Now;
                            _Zone.Iz_is_def = false;
                            _Zone.Iz_loc_cd = txtStorecode.Text;
                            _Zone.Iz_session_id = Session["SessionID"].ToString();
                            _Zone.Iz_zn_desc = GetExecelTbl[0].Rows[i][0].ToString();
                            _Zone.Iz_zn_id = GetExecelTbl[0].Rows[i][1].ToString();
                            _ZoneList.Add(_Zone);
                        }
                        catch (Exception ex)
                        {
                            ErrorMsg("Invalid excel zone data. Please check excel file and upload ...");
                            lblalert.Visible = true;
                            lblalert.Text = "Invalid excel zone data. Please check excel file and upload ...";
                            excelUpload.Show();
                        }

                    }
                    _AisleList = new List<WarehouseAisle>();

                    for (int i = 1; i < GetExecelTbl[1].Rows.Count; i++)
                    {
                        try
                        {
                            WarehouseAisle _Aisle = new WarehouseAisle();
                            _Aisle.Ia_act = true;
                            _Aisle.Ia_com_cd = Session["UserCompanyCode"].ToString();
                            _Aisle.Ia_cre_by = Session["UserID"].ToString();
                            _Aisle.Ia_cre_when = System.DateTime.Now;
                            _Aisle.Ia_is_def = false;
                            _Aisle.Ia_loc_cd = txtStorecode.Text;
                            _Aisle.Ia_session_id = Session["SessionID"].ToString();
                            _Aisle.Ia_asl_desc = GetExecelTbl[1].Rows[i][1].ToString();
                            _Aisle.Ia_asl_id = GetExecelTbl[1].Rows[i][2].ToString();
                            _Aisle.Ia_mod_by = GetExecelTbl[1].Rows[i][0].ToString(); //parameterset ZID
                            _AisleList.Add(_Aisle);
                        }
                        catch (Exception ex)
                        {
                            ErrorMsg("Invalid excel aisle data. Please check excel file and upload ...");
                            lblalert.Visible = true;
                            lblalert.Text = "Invalid excel aisle data. Please check excel file and upload ...";
                            excelUpload.Show();
                        }
                    }
                    _RowList = new List<WarehouseRow>();

                    for (int i = 1; i < GetExecelTbl[2].Rows.Count; i++)
                    {
                        try
                        {
                            WarehouseRow _Row = new WarehouseRow();
                            _Row.Ir_act = true;
                            _Row.Ir_com_cd = Session["UserCompanyCode"].ToString();
                            _Row.Ir_cre_by = Session["UserID"].ToString();
                            _Row.Ir_cre_when = System.DateTime.Now;
                            _Row.Ir_is_def = false;
                            _Row.Ir_loc_cd = txtStorecode.Text;
                            _Row.Ir_session_id = Session["SessionID"].ToString();
                            _Row.Ir_row_desc = GetExecelTbl[2].Rows[i][2].ToString();
                            _Row.Ir_row_id = GetExecelTbl[2].Rows[i][3].ToString();
                            _Row.Ir_tempZoonId = GetExecelTbl[2].Rows[i][0].ToString();
                            _Row.Ir_tempAisleId = GetExecelTbl[2].Rows[i][1].ToString();
                            _RowList.Add(_Row);
                        }
                        catch (Exception ex)
                        {
                            ErrorMsg("Invalid excel row data. Please check excel file and upload ...");
                            lblalert.Visible = true;
                            lblalert.Text = "Invalid excel row data. Please check excel file and upload ...";
                            excelUpload.Show();
                        }
                    }
                    _LevelList = new List<WarehouseLevel>();

                    for (int i = 1; i < GetExecelTbl[3].Rows.Count; i++)
                    {
                        try
                        {
                            WarehouseLevel _Level = new WarehouseLevel();
                            _Level.Il_act = true;
                            _Level.Il_com_cd = Session["UserCompanyCode"].ToString();
                            _Level.Il_cre_by = Session["UserID"].ToString();
                            _Level.Il_cre_when = System.DateTime.Now;
                            _Level.Il_is_def = false;
                            _Level.Il_loc_cd = txtStorecode.Text;
                            _Level.Il_session_id = Session["SessionID"].ToString();
                            _Level.Il_lvl_desc = GetExecelTbl[3].Rows[i][3].ToString();
                            _Level.Il_lvl_id = GetExecelTbl[3].Rows[i][4].ToString();
                            _Level.Ir_tempZoonId = GetExecelTbl[3].Rows[i][0].ToString();
                            _Level.Ir_tempRowlId = GetExecelTbl[3].Rows[i][2].ToString();
                            _Level.Ir_tempAisleId = GetExecelTbl[3].Rows[i][1].ToString();
                            _LevelList.Add(_Level);
                        }
                        catch (Exception ex)
                        {
                            ErrorMsg("Invalid excel level data. Please check excel file and upload ...");
                            lblalert.Visible = true;
                            lblalert.Text = "Invalid excel level data. Please check excel file and upload ...";
                            excelUpload.Show();
                        }
                    }
                    _WarehouseBin = new List<WarehouseBin>();

                    for (int i = 1; i < GetExecelTbl[4].Rows.Count; i++)
                    {
                        try
                        {
                            WarehouseBin _Bin = new WarehouseBin();
                            _Bin.Ibn_act = true;
                            //_Bin.Ibn_asl_seq = GetExecelTbl[4].Rows[i][1].ToString(); 
                            _Bin.Ibn_bin_cd = GetExecelTbl[4].Rows[i][5].ToString();
                            _Bin.Ibn_bin_desc = GetExecelTbl[4].Rows[i][4].ToString();
                            // _Bin.Ibn_capacity = Convert.ToDecimal(txtTotspace.Text);
                            //_Bin.Ibn_capacity_used = 0;
                            _Bin.Ibn_com_cd = Session["UserCompanyCode"].ToString();
                            _Bin.Ibn_cre_by = Session["UserID"].ToString();
                            _Bin.Ibn_cre_when = System.DateTime.Now;
                            //_Bin.Ibn_height = Convert.ToDecimal(txtheight.Text);
                            // _Bin.Ibn_is_def = (chk_BinIsDef.Checked == true) ? true : false;
                            //_Bin.Ibn_length = Convert.ToDecimal(txtlength.Text);
                            _Bin.Ibn_loc_cd = txtStorecode.Text;
                            //_Bin.Ibn_lvl_seq = Convert.ToInt32(Session["LevelSeq"].ToString());
                            //_Bin.Ibn_row_seq = Convert.ToInt32(Session["RowSeq"].ToString());
                            _Bin.Ibn_session_id = Session["SessionID"].ToString();
                            //_Bin.Ibn_weight = Convert.ToDecimal(txtMaxweight.Text);
                            // _Bin.Ibn_width = Convert.ToDecimal(txtwidth.Text);
                            //_Bin.Ibn_zn_seq = Convert.ToInt32(Session["ZoneSeq"].ToString());
                            _Bin.Ibn_tem_LevelID = GetExecelTbl[4].Rows[i][3].ToString();
                            _Bin.Ibn_tem_ZonelID = GetExecelTbl[0].Rows[i][1].ToString();
                            _WarehouseBin.Add(_Bin);
                        }
                        catch (Exception ex)
                        {
                            ErrorMsg("Invalid data found from the excel sheet. Please check data ...!");
                            lblalert.Visible = true;
                            lblalert.Text = "Invalid data found from the excel sheet. Please check data ...";
                            excelUpload.Show();
                        }
                    }

                    ViewState["ZoneList"] = _ZoneList;
                    ViewState["AisleList"] = _AisleList;
                    ViewState["RowList"] = _RowList;
                    ViewState["LevelList"] = _LevelList;
                    ViewState["Bin"] = _WarehouseBin;
                    SaveExcel();
                    return;
                }
                else
                {
                    ErrorMsg("Excel sheet zone data  null");
                    lblalert.Visible = true;
                    lblalert.Text = "Excel sheet zone data  null";
                    excelUpload.Show();
                }
            }
            ErrorMsg("Invalid data found from the excel sheet. Please check data ...!");
            lblalert.Visible = true;
            lblalert.Text = "Invalid data found from the excel sheet. Please check data ...";
            excelUpload.Show();
        }

        protected void txtwidth_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtheight.Text))
            {
                ErrorBinMsg("Please enter height");
                return;
            }
            if (string.IsNullOrEmpty(txtwidth.Text))
            {
                ErrorBinMsg("Please enter width");
                return;
            }
            if (string.IsNullOrEmpty(txtlength.Text))
            {
                ErrorBinMsg("Please enter lenght");
                return;
            }
            decimal MaxWeight = Convert.ToDecimal(txtwidth.Text) * Convert.ToDecimal(txtheight.Text) * Convert.ToDecimal(txtlength.Text);
            txtTotspace.Text = MaxWeight.ToString();
            if (txtutility.Text == "")
            {
                txtutility.Text = "0";
            }

            decimal _free = MaxWeight - Convert.ToDecimal(txtutility.Text);
            txtfree.Text = _free.ToString();
        }
        protected void txtheight_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtheight.Text))
            {
                ErrorBinMsg("Please enter height");
                return;
            }
            if (string.IsNullOrEmpty(txtwidth.Text))
            {
                ErrorBinMsg("Please enter width");
                return;
            }
            if (string.IsNullOrEmpty(txtlength.Text))
            {
                ErrorBinMsg("Please enter lenght");
                return;
            }
            decimal MaxWeight = Convert.ToDecimal(txtwidth.Text) * Convert.ToDecimal(txtheight.Text) * Convert.ToDecimal(txtlength.Text);
            txtTotspace.Text = MaxWeight.ToString();
            if (txtutility.Text == "")
            {
                txtutility.Text = "0";
            }

            decimal _free = MaxWeight - Convert.ToDecimal(txtutility.Text);
            txtfree.Text = _free.ToString();
        }
        protected void txtMaxweight_TextChanged(object sender, EventArgs e)
        {


        }

        protected void lbtnAddStorageF_Click(object sender, EventArgs e)
        {

            _warehouseStorageFacility = ViewState["Store"] as List<warehouseStorageFacility>;
            if (ViewState["Store"] == null)
            {
                _warehouseStorageFacility = new List<warehouseStorageFacility>();
            }
            warehouseStorageFacility _StorageFacility = new warehouseStorageFacility();
            _StorageFacility.Ibns_act = true;
            _StorageFacility.Ibns_cre_by = Session["UserID"].ToString();
            _StorageFacility.Ibns_cre_when = System.DateTime.Now;
            _StorageFacility.Ibns_session_id = Session["SessionID"].ToString();
            _StorageFacility.Ibns_stor_cd = ddlStorageF.SelectedValue;

            if (_warehouseStorageFacility.Count != 0)
            {
                var item = _warehouseStorageFacility.Max(i => i.Ibns_stor_line);
                if (item != null)
                {
                    _StorageFacility.Ibns_stor_line = Convert.ToInt32(item.ToString()) + 1;
                }
            }
            else
            {
                _StorageFacility.Ibns_stor_line = 0;
            }



            var currrangeDes = (from cur in _warehouseStorageFacility
                                where cur.Ibns_stor_cd == ddlStorageF.SelectedValue
                                select cur).ToList();
            if (currrangeDes.Count > 0)// ||currrange !=null)
            {
                ErrorBinMsg("Selected facility already exist...!");
                return;
            }
            _warehouseStorageFacility.Add(_StorageFacility);
            grdstoreFacility.DataSource = _warehouseStorageFacility;
            grdstoreFacility.DataBind();
            //_StorageFacility.Ibns_stor_line
            ViewState["Store"] = _warehouseStorageFacility;
        }

        protected void lbtnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmaincat.Text))
            {
                ErrorBinMsg("Please select main category...!");
                return;
            }
            _warehouseBinItems = ViewState["BinItem"] as List<warehouseBinItems>;
            if (ViewState["BinItem"] == null)
            {
                _warehouseBinItems = new List<warehouseBinItems>();
            }
            warehouseBinItems _BinItems = new warehouseBinItems();
            _BinItems.Ibni_act = true;
            _BinItems.Ibni_cre_by = Session["UserID"].ToString();
            _BinItems.Ibni_cre_when = System.DateTime.Now;
            _BinItems.Ibni_session_id = Session["SessionID"].ToString();
            _BinItems.Ibni_cat_cd1 = txtmaincat.Text.Trim();
            _BinItems.Ibni_cat_cd2 = txtsubcat1.Text.Trim();
            _BinItems.Ibni_cat_cd3 = txtsubcat2.Text.Trim();
            _BinItems.Ibni_cat_cd4 = txtsubcat3.Text.Trim();
            _BinItems.Ibni_cat_cd5 = txtsubcat4.Text.Trim();

            if (_warehouseBinItems.Count != 0)
            {
                var item = _warehouseBinItems.Max(i => i.Ibni_cat_line);
                if (item != null)
                {
                    _BinItems.Ibni_cat_line = Convert.ToInt32(item.ToString()) + 1;
                }
            }
            else
            {
                _BinItems.Ibni_cat_line = 0;
            }


            var currrangeDes = (from cur in _warehouseBinItems
                                where (cur.Ibni_cat_cd1 == txtmaincat.Text.Trim() && cur.Ibni_cat_cd2 == txtsubcat1.Text.Trim()
                                && cur.Ibni_cat_cd3 == txtsubcat2.Text.Trim() && cur.Ibni_cat_cd4 == txtsubcat3.Text.Trim()
                                && cur.Ibni_cat_cd5 == txtsubcat4.Text.Trim())
                                select cur).ToList();
            if (currrangeDes.Count > 0)// ||currrange !=null)
            {
                ErrorBinMsg("Selected category is already exist...!");
                return;
            }
            _warehouseBinItems.Add(_BinItems);
            grdBinItem.DataSource = _warehouseBinItems;
            grdBinItem.DataBind();
            //_StorageFacility.Ibns_stor_line
            ViewState["BinItem"] = _warehouseBinItems;

        }

        protected void lbtnBinSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBinZone.Text))
            {
                ErrorBinMsg("Please select zone code...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinAisle.Text))
            {
                ErrorBinMsg("Please select aisle code...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinRow.Text))
            {
                ErrorBinMsg("Please select row code...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinLevel.Text))
            {
                ErrorBinMsg("Please select level code...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinLocation.Text))
            {
                ErrorBinMsg("Please select location code...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinDes.Text))
            {
                ErrorBinMsg("Please select description code...!");
                return;
            }

            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }

            SaveBin();
        }

        protected void lbtnFilterBin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBinzoneS.Text))
            {
                ErrorBinMsg("Please select zone ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinAisle.Text))
            {
                ErrorBinMsg("Please select aisle ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinRow.Text))
            {
                ErrorBinMsg("Please select row ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinLevel.Text))
            {
                ErrorBinMsg("Please select level ID...!");
                return;
            }
            _BinDetas = CHNLSVC.General.GetWarehouseBin(0, txtBinStore.Text, Session["UserCompanyCode"].ToString(), Convert.ToInt32(Session["ZoneSeq"].ToString()), Convert.ToInt32(Session["AisleSeq"].ToString()), Convert.ToInt32(Session["RowSeq"].ToString()), Convert.ToInt32(Session["LevelSeq"].ToString()));
            _WarehouseBin = _BinDetas.Item1;
            grdBinDeatils.DataSource = _WarehouseBin;
            grdBinDeatils.DataBind();
            ViewState["BinDetails"] = _WarehouseBin;
        }
        protected void lbtnselectBin_Click(object sender, EventArgs e)
        {
            if (grdBinDeatils.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _Seq = (row.FindControl("col_BinSeq") as Label).Text;
                Session["BinSeq"] = _Seq;
                _BinDetas = CHNLSVC.General.GetWarehouseBin(Convert.ToInt32(_Seq), txtBinStore.Text, Session["UserCompanyCode"].ToString(), 0, 0, 0, 0);
                Session["Isupdate"] = "true";
                _WarehouseBin = _BinDetas.Item1;
                _warehouseBinItems = _BinDetas.Item2;
                _warehouseStorageFacility = _BinDetas.Item3;
                if (_WarehouseBin.Count > 0)
                {

                    foreach (WarehouseBin Bin in _WarehouseBin)
                    {
                        txtBinLocation.Text = Bin.Ibn_bin_cd;
                        txtBinDes.Text = Bin.Ibn_bin_desc;
                        chk_BinIsDef.Checked = Bin.Ibn_is_def;
                        chk_BinActive.Checked = Bin.Ibn_act;
                        txtwidth.Text = Bin.Ibn_width.ToString();
                        txtlength.Text = Bin.Ibn_length.ToString();
                        txtheight.Text = Bin.Ibn_height.ToString();
                        txtMaxweight.Text = Bin.Ibn_weight.ToString();
                        txtTotspace.Text = Bin.Ibn_capacity.ToString();
                        decimal Freevalue = Bin.Ibn_capacity - Bin.Ibn_capacity_used;
                        txtfree.Text = Freevalue.ToString();
                        if (Bin.Ibn_tp != "")
                        {
                            ddlbintype.SelectedValue = Bin.Ibn_tp;
                        }


                        if ((Bin.Ibn_diam_uom != ""))
                        {
                            ddluomwidth.SelectedValue = Bin.Ibn_diam_uom;
                            ddluomlength.SelectedValue = Bin.Ibn_diam_uom;
                            ddluomheight.SelectedValue = Bin.Ibn_diam_uom;
                            ddluomtot.SelectedValue = Bin.Ibn_diam_uom;
                            ddluomutiliz.SelectedValue = Bin.Ibn_diam_uom;
                            ddluomfree.SelectedValue = Bin.Ibn_diam_uom;
                        }
                        if ((Bin.Ibn_diam_uom != ""))
                        {

                            ddluommaxweight.SelectedValue = Bin.Ibn_weight_uom;
                        }

                    }

                    grdBinItem.DataSource = _warehouseBinItems;
                    grdBinItem.DataBind();
                    grdstoreFacility.DataSource = _warehouseStorageFacility;
                    grdstoreFacility.DataBind();
                    ViewState["BinItem"] = _warehouseBinItems;
                    ViewState["Store"] = _warehouseStorageFacility;

                    txtBinZone.Enabled = false;
                    txtBinAisle.Enabled = false;
                    txtBinRow.Enabled = false;
                    txtBinLevel.Enabled = false;
                    txtBinLocation.Enabled = false;

                }

            }


        }

        protected void txtStorecode_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "CODE", txtStorecode.Text.ToString());
            if (_result.Rows.Count > 0 && _result != null)
            {
                txtWarehouse.Text = _result.Rows[0][1].ToString();
                txtBinStore.Text = txtStorecode.Text;
                txtBinwarehouse.Text = txtWarehouse.Text;
                _ZoneList = CHNLSVC.General.GetWarehouseZone(Session["UserCompanyCode"].ToString(), txtStorecode.Text);
                if (_ZoneList != null)
                {
                    grdZone.DataSource = _ZoneList;
                    grdZone.DataBind();
                    ViewState["ZoneList"] = _ZoneList;
                    Islayoutupdate = true;
                    Session["Islayoutupdate"] = "true";
                    BuildTree(_ZoneList);
                    ClearBin();
                    return;
                }

                txtBinzoneS.Text = string.Empty;
                txtBinAisleS.Text = string.Empty;
                txtBinRowS.Text = string.Empty;
                txtBinLevelS.Text = string.Empty;
                grdZone.DataSource = new int[] { };
                grdZone.DataBind();
                grdAisle.DataSource = new int[] { };
                grdAisle.DataBind();
                grdRow.DataSource = new int[] { };
                grdRow.DataBind();
                grdlevel.DataSource = new int[] { };
                grdlevel.DataBind();
                grdBinDeatils.DataSource = new int[] { };
                grdBinDeatils.DataBind();
                lblvalue.Text = "";
                treeViewLayout.Nodes.Clear();
                return;
            }
            ErrorMsg("Incorrect store code");
            txtStorecode.Text = "";
            txtStorecode.Focus();
        }

        protected void txtBinStore_TextChanged(object sender, EventArgs e)
        {

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "CODE", txtBinStore.Text.ToString());
            if (_result.Rows.Count > 0 && _result != null)
            {
                txtBinwarehouse.Text = _result.Rows[0][1].ToString();
                txtStorecode.Text = txtBinStore.Text;
                txtWarehouse.Text = txtBinwarehouse.Text;
                _ZoneList = CHNLSVC.General.GetWarehouseZone(Session["UserCompanyCode"].ToString(), txtBinStore.Text);
                if (_ZoneList != null)
                {
                    grdZone.DataSource = _ZoneList;
                    grdZone.DataBind();
                    ViewState["ZoneList"] = _ZoneList;
                    Islayoutupdate = true;
                    Session["Islayoutupdate"] = "true";
                    BuildTree(_ZoneList);
                    ClearBin();
                    return;
                }

                txtBinzoneS.Text = string.Empty;
                txtBinAisleS.Text = string.Empty;
                txtBinRowS.Text = string.Empty;
                txtBinLevelS.Text = string.Empty;
                grdZone.DataSource = new int[] { };
                grdZone.DataBind();
                grdAisle.DataSource = new int[] { };
                grdAisle.DataBind();
                grdRow.DataSource = new int[] { };
                grdRow.DataBind();
                grdlevel.DataSource = new int[] { };
                grdlevel.DataBind();
                grdBinDeatils.DataSource = new int[] { };
                grdBinDeatils.DataBind();
                lblvalue.Text = "";
                treeViewLayout.Nodes.Clear();
                return;
            }
            ErrorBinMsg("Incorrect store code...!");
            txtBinStore.Text = "";
            txtBinStore.Focus();
        }

        protected void lbtManualybin_Click(object sender, EventArgs e)
        {
            MaxBin.Show();
        }

        protected void lbtnAddManulBin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBinStore.Text))
            {
                ErrorBinMsg("Please select store loction...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinZone.Text))
            {
                ErrorBinMsg("Please select zone ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinAisle.Text))
            {
                ErrorBinMsg("Please select aisle ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinRow.Text))
            {
                ErrorBinMsg("Please select row ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtBinLevel.Text))
            {
                ErrorBinMsg("Please select level ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtManulBinDes.Text))
            {
                ErrorBinMsg("Please type description...!");
                return;
            }
            if (string.IsNullOrEmpty(txtManulBinLoc.Text))
            {
                ErrorBinMsg("Please type loaction code...!");
                return;
            }
            if (string.IsNullOrEmpty(txtfrom.Text))
            {
                ErrorBinMsg("Please type value for FROM...!");
                return;
            }
            if (string.IsNullOrEmpty(txtTo.Text))
            {
                ErrorBinMsg("Please type value for TO...!");
                return;
            }
            int num;

            if (!(int.TryParse(txtfrom.Text, out num)))
            {
                ErrorBinMsg("Only allow  numeric value...!");
                return;

            }
            if (!(int.TryParse(txtTo.Text, out num)))
            {
                ErrorBinMsg("Only allow  numeric value...!");
                return;

            }
            string lengthCheck = txtfrom.Text + txtTo.Text;
            if (lengthCheck.Length > 20)
            {
                ErrorBinMsg("You can create maximum 20 bins ");
                return;
            }
            Int32 row_aff = 0;
            Int32 From = Convert.ToInt32(txtfrom.Text);
            Int32 To = Convert.ToInt32(txtTo.Text);
            for (int i = From; i <= To; i++)
            {
                WarehouseBin _Bin = new WarehouseBin();
                _Bin.Ibn_act = true;
                _Bin.Ibn_asl_seq = Convert.ToInt32(Session["AisleSeq"].ToString());
                _Bin.Ibn_bin_cd = txtManulBinLoc.Text + i;
                _Bin.Ibn_bin_desc = txtManulBinDes.Text;

                _Bin.Ibn_com_cd = Session["UserCompanyCode"].ToString();
                _Bin.Ibn_cre_by = Session["UserID"].ToString();
                _Bin.Ibn_cre_when = System.DateTime.Now;

                _Bin.Ibn_is_def = false;

                _Bin.Ibn_loc_cd = txtBinStore.Text;
                _Bin.Ibn_lvl_seq = Convert.ToInt32(Session["LevelSeq"].ToString());
                _Bin.Ibn_row_seq = Convert.ToInt32(Session["RowSeq"].ToString());
                _Bin.Ibn_session_id = Session["SessionID"].ToString();

                _Bin.Ibn_zn_seq = Convert.ToInt32(Session["ZoneSeq"].ToString());

                _WarehouseBin.Add(_Bin);
            }



            _warehouseStorageFacility = ViewState["Store"] as List<warehouseStorageFacility>;
            _warehouseBinItems = ViewState["BinItem"] as List<warehouseBinItems>;

            row_aff = (Int32)CHNLSVC.General.SaveWarehouseBin(_WarehouseBin, _warehouseStorageFacility, _warehouseBinItems);

            if (row_aff == 1)
            {
                SuccessBinMsg("Successfully saved the bin details...! ");
                ClearPage();
            }
        }

        protected void ddluomwidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddluomlength.SelectedValue = ddluomwidth.SelectedValue;
            ddluomheight.SelectedValue = ddluomwidth.SelectedValue;
            ddluommaxweight.SelectedValue = ddluomwidth.SelectedValue;
            ddluomtot.SelectedValue = ddluomwidth.SelectedValue;
            ddluomutiliz.SelectedValue = ddluomlength.SelectedValue;
            ddluomfree.SelectedValue = ddluomlength.SelectedValue;
        }

        protected void ddluomheight_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddluomlength.SelectedValue = ddluomwidth.SelectedValue;
            ddluomheight.SelectedValue = ddluomwidth.SelectedValue;
            ddluommaxweight.SelectedValue = ddluomwidth.SelectedValue;
            ddluomtot.SelectedValue = ddluomwidth.SelectedValue;
            ddluomutiliz.SelectedValue = ddluomlength.SelectedValue;
            ddluomfree.SelectedValue = ddluomlength.SelectedValue;
        }

        protected void ddluomlength_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddluomlength.SelectedValue = ddluomwidth.SelectedValue;
            ddluomheight.SelectedValue = ddluomwidth.SelectedValue;
            ddluommaxweight.SelectedValue = ddluomwidth.SelectedValue;
            ddluomtot.SelectedValue = ddluomwidth.SelectedValue;
            ddluomutiliz.SelectedValue = ddluomlength.SelectedValue;
            ddluomfree.SelectedValue = ddluomlength.SelectedValue;
        }

        protected void lbtnBinClear_Click(object sender, EventArgs e)
        {

        }

        protected void txtlength_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtheight.Text))
            {
                ErrorBinMsg("Please enter height");
                return;
            }
            if (string.IsNullOrEmpty(txtwidth.Text))
            {
                ErrorBinMsg("Please enter width");
                return;
            }
            if (string.IsNullOrEmpty(txtlength.Text))
            {
                ErrorBinMsg("Please enter lenght");
                return;
            }
            decimal MaxWeight = Convert.ToDecimal(txtwidth.Text) * Convert.ToDecimal(txtheight.Text) * Convert.ToDecimal(txtlength.Text);
            txtTotspace.Text = MaxWeight.ToString();
            if (txtutility.Text == "")
            {
                txtutility.Text = "0";
            }

            decimal _free = MaxWeight - Convert.ToDecimal(txtutility.Text);
            txtfree.Text = _free.ToString();
        }

        protected void DummyDataBind()
        {
            DataTable dummy = new DataTable();
            System.Data.DataColumn code = new System.Data.DataColumn("CODE", typeof(System.String));
            System.Data.DataColumn desc = new System.Data.DataColumn("DESCRIPTION", typeof(System.String));
            dummy.Columns.Add(code);
            dummy.Columns.Add(desc);
            BindUCtrlDDLData(dummy);
        }

        protected void lbtnUser_Click(object sender, EventArgs e)
        {

            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            //_result1 = CHNLSVC.CommonSearch.Get_All_SystemUsers(SearchParams, null, null);
            //_result1.Columns["USER ID"].ColumnName = "CODE";
            //_result1.Columns["NAME"].ColumnName = "DESCRIPTION";
            //BindUCtrlDDLData(_result1);
            DummyDataBind();


            _result = CHNLSVC.CommonSearch.Get_UsersFrompda(Session["UserCompanyCode"].ToString(), ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            lblvalue.Text = "userId";
            UserPopoup.Show();
            txtSearchbyword.Text = string.Empty;
        }

        protected void lbtnLocation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                ErrorBinMsg("Please Select A User");
                return;
            }
            else
            {
                if (Session["LoadingBayUserID"] == null || Session["LoadingBayUserID"].ToString() == "")
                {
                    Session["LoadingBayUserID"] = txtUserId.Text;
                }
                DummyDataBind();
                //grdResult.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoadingBayLocation);
                //DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationFrompda(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                lblvalue.Text = "locationId";
                txtSearchbyword.Text = string.Empty;
                Session["LoadingBayUserID"] = string.Empty;
            }
        }

        protected void lbtnLoadingBay_Click(object sender, EventArgs e)
        {
            DummyDataBind();
            string SearchParams = "LB";
            DataTable _result = CHNLSVC.CommonSearch.Get_LoadingBays(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            lblvalue.Text = "locationBayId";
            txtSearchbyword.Text = string.Empty;
        }
        protected void lbtnDocLoadingBay_Click(object sender, EventArgs e)
        {
            DummyDataBind();
            string SearchParams = "LB";
            DataTable _result = CHNLSVC.CommonSearch.Get_LoadingBays(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            lblvalue.Text = "locationDocBayId";
            txtSearchbyword.Text = string.Empty;
        }

        protected void lbtnDocno_Click(object sender, EventArgs e)
        {
            DummyDataBind();
            string SearchParams = Session["UserCompanyCode"].ToString();
            DataTable _result = CHNLSVC.CommonSearch.Get_LoadingBaysDoc(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            lblvalue.Text = "locationBayDocNo";
            txtSearchbyword.Text = string.Empty;
        }

        protected void lbtnadd_Click(object sender, EventArgs e)
        {
            Session["Isupdate"] = "";
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                ErrorBinMsg("Please select a User ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtLocationID.Text))
            {
                ErrorBinMsg("Please select a Location ID...!");
                return;
            }
            if (string.IsNullOrEmpty(txtLoadingBayID.Text))
            {
                ErrorBinMsg("Please select a Loading Bay ID...!");
                return;
            }

            LoadingBay _loadBay = new LoadingBay();
            _LoadingBay = ViewState["StoreLB"] as List<LoadingBay>;
            if (ViewState["StoreLB"] == null || ViewState["StoreLB"].ToString() == "")
            {
                _LoadingBay = new List<LoadingBay>();
            }

            string value = string.Empty;
            Int32 item = 0;
            if (ViewState["LBLine"] != null)
            {
                value = ViewState["LBLine"].ToString();
                ViewState["LBLine"] = string.Empty;
            }
            else
                value = null;

            if (value == null || value == "")
            {
                if (_LoadingBay.Count != 0)
                {
                    item = _LoadingBay.Max(i => i.LoadBayline);
                    if (item != 0)
                    {
                        _loadBay.LoadBayline = Convert.ToInt32(item.ToString()) + 1;
                    }
                }
                else
                {
                    _loadBay.LoadBayline = 1;
                }
                _loadBay.userId = txtUserId.Text;
                _loadBay.user1 = lblUserNameView.Text;
                _loadBay.locationID = txtLocationID.Text;
                _loadBay.location = lblLocationView.Text;
                _loadBay.loadingBayID = txtLoadingBayID.Text;
                _loadBay.loadingBay = lblLoadingBayView.Text;
                _loadBay.Default1 = chkdef.Checked ? 1 : 0;//== true ? "Yes" : "No";
                _loadBay.Active = chkactive.Checked ? 1 : 0;//== true ? "Yes" : "No";
                _LoadingBay.Add(_loadBay);
            }
            else
            {
                //var lb = _LoadingBay.Where(c => c.userId == txtUserId.Text).FirstOrDefault(); //c => c.userId == txtUserId.Text  c => c.LoadBay_line == item
                var check = _LoadingBay.Where(c => c.LoadBayline == Convert.ToInt32(value)).FirstOrDefault();
                check.userId = txtUserId.Text;
                check.user1 = lblUserNameView.Text;
                check.locationID = txtLocationID.Text;
                check.location = lblLocationView.Text;
                check.loadingBayID = txtLoadingBayID.Text;
                check.loadingBay = lblLoadingBayView.Text;
                check.Default1 = chkdef.Checked ? 1 : 0;//== true ? "Yes" : "No";
                check.Active = chkactive.Checked ? 1 : 0;//== true ? "Yes" : "No";
                check.LoadBayline = Convert.ToInt32(value);
            }

            grdLocationBay.DataSource = _LoadingBay;
            grdLocationBay.DataBind();
            ViewState["StoreLB"] = _LoadingBay;
            textFeildClean();
        }

        protected void lbtnDocAdd_Click(object sender, EventArgs e)
        {
            Session["Isupdate"] = "";
            if (string.IsNullOrEmpty(txtDocNo.Text))
            {
                ErrorBinMsg("Please select a Document Number...!");
                return;
            }
            if (string.IsNullOrEmpty(txtDocLoadBay.Text))
            {
                ErrorBinMsg("Please select a Loading Bay ID...!");
                return;
            }

            ReptPickHeader _DocLoadBay = new ReptPickHeader();
            _DocLoadingBay = ViewState["StoreLB1"] as List<ReptPickHeader>;
            if (ViewState["StoreLB1"] == null || ViewState["StoreLB1"].ToString() == "")
            {
                _DocLoadingBay = new List<ReptPickHeader>();
            }

            //string value = string.Empty;
            //Int32 item = 0;
            //if (ViewState["LBLine"] != null)
            //{
            //    value = ViewState["LBLine"].ToString();
            //    ViewState["LBLine"] = string.Empty;
            //}
            //else
            //    value = null;

            //if (value == null || value == "")
            //{
                //if (_LoadingBay.Count != 0)
                //{
                //    item = _LoadingBay.Max(i => i.LoadBayline);
                //    if (item != 0)
                //    {
                //        _DocLoadingBay.LoadBayline = Convert.ToInt32(item.ToString()) + 1;
                //    }
                //}
                //else
                //{
                //    _DocLoadingBay.LoadBayline = 1;
                //}
            _DocLoadBay.Tuh_doc_no = txtDocNo.Text;
            _DocLoadBay.Tuh_load_bay = txtDocLoadBay.Text;
            _DocLoadBay.Tuh_usr_com = Session["UserCompanyCode"].ToString();

            _DocLoadingBay.Add(_DocLoadBay);
            //}
            //else
            //{
            //    //var lb = _LoadingBay.Where(c => c.userId == txtUserId.Text).FirstOrDefault(); //c => c.userId == txtUserId.Text  c => c.LoadBay_line == item
            //    var check = _LoadingBay.Where(c => c.LoadBayline == Convert.ToInt32(value)).FirstOrDefault();
            //    check.userId = txtUserId.Text;
            //    check.user1 = lblUserNameView.Text;
            //    check.locationID = txtLocationID.Text;
            //    check.location = lblLocationView.Text;
            //    check.loadingBayID = txtLoadingBayID.Text;
            //    check.loadingBay = lblLoadingBayView.Text;
            //    check.Default1 = chkdef.Checked ? 1 : 0;//== true ? "Yes" : "No";
            //    check.Active = chkactive.Checked ? 1 : 0;//== true ? "Yes" : "No";
            //    check.LoadBayline = Convert.ToInt32(value);
            //}

            grdDocLocationBay.DataSource = _DocLoadingBay;
            grdDocLocationBay.DataBind();
            ViewState["StoreLB1"] = _DocLoadingBay;
            textFeildCleanDoc();
        }
        private void textFeildCleanDoc()
        {
            txtDocNo.Text = string.Empty;
            lblDocNoView.Text = "";
            txtDocLoadBay.Text = string.Empty;
            lblLoadingDocBayView.Text = "";

        }
        private void textFeildClean()
        {
            txtUserId.Text = string.Empty;
            lblUserNameView.Text = "";
            txtLocationID.Text = string.Empty;
            lblLocationView.Text = "";
            txtLoadingBayID.Text = string.Empty;
            lblLoadingBayView.Text = "";
            chkdef.Checked = false;
            chkactive.Checked = false;
        }

        private void LoadBayClean()
        {
            txtUserId.Text = string.Empty;
            lblUserNameView.Text = "";
            txtLocationID.Text = string.Empty;
            lblLocationView.Text = "";
            txtLoadingBayID.Text = string.Empty;
            lblLoadingBayView.Text = "";
            chkdef.Checked = false;
            chkactive.Checked = false;
            grdLocationBay.DataSource = null;//new int[] { };
            grdLocationBay.DataBind();
            lbtnUser.Enabled = true;
            Session["Isupdate"] = "";
            lbtnadd.Visible = true;
            ViewState["LBLine"] = string.Empty;
            ViewState["StoreLB"] = string.Empty;
            lbtnadd.Visible = true;
        }

        private void DocLoadBayClean()
        {
            txtUserId.Text = string.Empty;
            lblUserNameView.Text = "";
            txtLocationID.Text = string.Empty;
            lblLocationView.Text = "";
            txtLoadingBayID.Text = string.Empty;
            lblLoadingBayView.Text = "";
            chkdef.Checked = false;
            chkactive.Checked = false;
            grdDocLocationBay.DataSource = null;//new int[] { };
            grdDocLocationBay.DataBind();
            lbtnUser.Enabled = true;
            Session["Isupdate"] = "";
            lbtnadd.Visible = true;
            ViewState["LBLine"] = string.Empty;
            ViewState["StoreLB1"] = string.Empty;
            lbtnadd.Visible = true;
        }
        protected void grdLocationBay_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdLocationBay.SelectedRow;
            txtUserId.Text = (row.FindControl("col_userId") as Label).Text;
            ViewState["useridU"] = (row.FindControl("col_userId") as Label).Text;
            lblUserNameView.Text = (row.FindControl("col_user") as Label).Text;
            txtLocationID.Text = (row.FindControl("col_locationID") as Label).Text; //ViewState["LocId"].ToString();
            ViewState["locationidU"] = (row.FindControl("col_locationID") as Label).Text;
            lblLocationView.Text = (row.FindControl("col_location") as Label).Text;
            txtLoadingBayID.Text = (row.FindControl("col_loadingBayID") as Label).Text; //ViewState["LocBayId"].ToString();
            ViewState["loadingBayidU"] = (row.FindControl("col_loadingBayID") as Label).Text;
            lblLoadingBayView.Text = (row.FindControl("col_loadingBay") as Label).Text;
            chkactive.Checked = (row.FindControl("col_Active") as Label).Text == "Yes" ? true : false;
            chkdef.Checked = (row.FindControl("col_Default") as Label).Text == "Yes" ? true : false;
            ViewState["LBLine"] = (row.FindControl("col_Line") as Label).Text;//((HiddenField)grdLocationBay.SelectedRow.FindControl("hdnline")).Value;
            lbtnUser.Enabled = false;
            Session["Isupdate"] = "true";
        }

        protected void grdDocLocationBay_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdDocLocationBay.SelectedRow;
            txtDocNo.Text = (row.FindControl("col_docno") as Label).Text;
            ViewState["docnoU"] = (row.FindControl("col_docno") as Label).Text;
            lblDocNoView.Text = (row.FindControl("col_docno") as Label).Text;
            txtDocLoadBay.Text = (row.FindControl("col_loadingBayID") as Label).Text; //ViewState["LocBayId"].ToString();
            ViewState["loadingdocBayidU"] = (row.FindControl("col_loadingBayID") as Label).Text;
            lblLoadingDocBayView.Text = (row.FindControl("col_loadingBay") as Label).Text;
            //ViewState["LBLine"] = (row.FindControl("col_Line") as Label).Text;//((HiddenField)grdLocationBay.SelectedRow.FindControl("hdnline")).Value;
            lbtnUser.Enabled = false;
            Session["Isupdate"] = "true";
        }


        protected void lbtnBaySearch_Click(object sender, EventArgs e)
        {
            GetLoadBayDetails();
            lbtnadd.Visible = false;
        }

        protected void GetLoadBayDetails()
        {
            _LoadingBay = CHNLSVC.CommonSearch.Get_LoadingBayOtherDetails(txtUserId.Text, txtLocationID.Text, txtLoadingBayID.Text);
            grdLocationBay.DataSource = _LoadingBay;
            grdLocationBay.DataBind();
        }

        protected void SaveUpdateLoadingBay()
        {
            if (grdLocationBay.Rows.Count > 0)
            {
                Int32 result = 0;
                int row_aff = 0;
                DataTable dtVal = null;
                string wComv = string.Empty;
                string wLocv = string.Empty;
                DataTable tbl = null;
                if (txtUserId.Text != null && txtUserId.Text != "")
                {
                    dtVal = CHNLSVC.CommonSearch.getWarehouseData(txtUserId.Text, txtLocationID.Text, Session["UserCompanyCode"].ToString());
                    if (dtVal.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtVal.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtVal.Columns.Count; j++)
                            {
                                if (string.IsNullOrEmpty(dtVal.Rows[i][j].ToString()))
                                {
                                    ErrorBinMsg("selected User " + txtUserId.Text + " Have No Warehouse Location Or Company");
                                    LoadBayClean();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        ErrorBinMsg("selected User " + txtUserId.Text + " Have No Warehouse Location Or Company");
                        LoadBayClean();
                        return;
                    }
                }
                else
                {
                    foreach (GridViewRow row in grdLocationBay.Rows)
                    {
                        dtVal = CHNLSVC.CommonSearch.getWarehouseData((txtUserId.Text == null || txtUserId.Text == "") ? (row.FindControl("col_userId") as Label).Text : txtUserId.Text
                            , (txtLocationID.Text == null || txtLocationID.Text == "") ? (row.FindControl("col_locationID") as Label).Text : txtLocationID.Text, Session["UserCompanyCode"].ToString());

                        string usr = (row.FindControl("col_userId") as Label).Text;
                        if (dtVal.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtVal.Rows.Count; i++)
                            {
                                for (int j = 0; j < dtVal.Columns.Count; j++)
                                {
                                    if (string.IsNullOrEmpty(dtVal.Rows[i][j].ToString()))
                                    {
                                        ErrorBinMsg("selected User " + usr + " Have No Warehouse Location Or Company");
                                        LoadBayClean();
                                        return;
                                    }
                                }
                            }
                        }
                        else if (dtVal.Rows.Count <= 0)
                        {
                            ErrorBinMsg("selected User " + txtUserId.Text + " Have No Warehouse Location Or Company");
                            LoadBayClean();
                            return;
                        }
                        else
                        {
                            tbl = CHNLSVC.CommonSearch.Get_RecordExistInBay((txtUserId.Text == null || txtUserId.Text == "") ? (row.FindControl("col_userId") as Label).Text : txtUserId.Text
                                , (txtLocationID.Text == null || txtLocationID.Text == "") ? (row.FindControl("col_locationID") as Label).Text : txtLocationID.Text
                                , Session["UserCompanyCode"].ToString(), dtVal.Rows[0]["Company"].ToString(), dtVal.Rows[0]["Location"].ToString()
                                , (txtLoadingBayID.Text == null || txtLoadingBayID.Text == "") ? (row.FindControl("col_loadingBayID") as Label).Text : txtLoadingBayID.Text);
                            if (tbl.Rows.Count > 0)
                            {
                                ErrorBinMsg("Selected " + (row.FindControl("col_userId") as Label).Text + " Record Already Exist");
                                LoadBayClean();
                                return;
                            }
                        }
                    }
                }
                wComv = dtVal.Rows[0]["Company"].ToString();
                wLocv = dtVal.Rows[0]["Location"].ToString();
                tbl = CHNLSVC.CommonSearch.Get_RecordExistInBay(txtUserId.Text, txtLocationID.Text, Session["UserCompanyCode"].ToString(), wComv, wLocv, txtLoadingBayID.Text);
                if (tbl.Rows.Count > 0)
                {
                    ErrorBinMsg("Selected Record Already Exists");
                    LoadBayClean();
                    return;
                }
                else
                {
                    if (Session["Isupdate"].ToString() == "true")
                    {
                        try
                        {
                            result = CHNLSVC.CommonSearch.ExistsLoadingBaysUpdate(ViewState["useridU"].ToString(), ViewState["locationidU"].ToString(), ViewState["loadingBayidU"].ToString(), txtUserId.Text, txtLocationID.Text, txtLoadingBayID.Text, chkactive.Checked ? 1 : 0, Session["UserID"].ToString(), System.DateTime.Now, Session["SessionID"].ToString(), Session["UserCompanyCode"].ToString(), wComv, wLocv);
                        }
                        catch (Exception ex)
                        {
                            ErrorBinMsg(ex.Message);
                        }
                        if (result == 1)
                        {
                            SuccessMsg("Successfully Updated User ID: " + ViewState["useridU"].ToString().ToUpper());
                        }
                        ErrorBinMsg("Please select valid data before saving");
                        ViewState["useridU"] = string.Empty;
                        ViewState["locationidU"] = string.Empty;
                        LoadBayClean();
                    }
                    else
                    {
                        string wCom = string.Empty;
                        string wLoc = string.Empty;
                        if (txtUserId.Text == "" && txtLocationID.Text == "" && txtLoadingBayID.Text == "")
                        {
                            foreach (GridViewRow row in grdLocationBay.Rows)
                            {
                                DataTable dt = CHNLSVC.CommonSearch.getWarehouseData((row.FindControl("col_userId") as Label).Text, (row.FindControl("col_locationID") as Label).Text, Session["UserCompanyCode"].ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    wCom = dt.Rows[0]["Company"].ToString();
                                    wLoc = dt.Rows[0]["Location"].ToString();
                                }
                                else
                                {
                                    ErrorBinMsg("selected User " + txtUserId.Text + " Have No Warehouse Location Or Company");
                                    LoadBayClean();
                                    return;
                                }
                                LoadingBay _loadB = new LoadingBay();
                                _loadB.userId = (row.FindControl("col_userId") as Label).Text;
                                _loadB.companyCode = Session["UserCompanyCode"].ToString();
                                _loadB.locationID = (row.FindControl("col_locationID") as Label).Text;
                                _loadB.warehouseCom = wCom;
                                _loadB.warehouseLoc = wLoc;
                                _loadB.loadingBayID = (row.FindControl("col_loadingBayID") as Label).Text;
                                _loadB.Default1 = chkdef.Checked ? 1 : 0;
                                _loadB.sessionCreateBy = Session["UserID"].ToString();
                                _loadB.sessionCreateDate = DateTime.Now;
                                _loadB.createSession = Session["SessionID"].ToString();
                                _loadB.sessionModBy = string.Empty;
                                _loadB.sessionModDate = DateTime.MinValue;
                                _loadB.modSession = string.Empty;
                                _loadB.Active = chkactive.Checked ? 1 : 0;
                                _LoadingBay.Add(_loadB);
                            }
                        }
                        else
                            ErrorBinMsg("In This Situation You Only Can Update Selected Record...!!!");
                        try
                        {
                            row_aff = (Int32)CHNLSVC.General.saveLoadingBay(_LoadingBay);
                        }
                        catch (Exception ex)
                        {
                            ErrorBinMsg(ex.Message);
                        }
                        if (row_aff == 1)
                        {
                            SuccessBinMsg("Successfully saved the loadbay details...!");
                            LoadBayClean();
                        }
                    }
                    Session["Isupdate"] = "";
                }
            }
            else
                ErrorBinMsg("Please Add Records Before Saving");
            LoadBayClean();
            _LoadingBay.Clear();
            ViewState["StoreLB"] = string.Empty;
        }

        protected void lbtnLoadBaySave_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "Yes")
            {
                try
                {
                }
                catch (Exception ex)
                {
                    ErrorBinMsg(ex.Message);
                }
            }
            SaveUpdateLoadingBay();
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
        }

        protected void lbtnDocLoadBaySave_Click(object sender, EventArgs e)
        {
            _DocLoadingBay = ViewState["StoreLB1"] as List<ReptPickHeader>;
            if (txtSavelconformmessageValue.Value == "Yes")
            {
                Int32 row_aff = 0;
                try
                {
                    if (ViewState["StoreLB1"] == null || ViewState["StoreLB1"].ToString() == "")
                    {
                        ErrorBinMsg("No Record found");
                    }
                    else
                    {
                        row_aff = (Int32)CHNLSVC.General.saveDocLoadingBay(_DocLoadingBay);
                        if(row_aff==1)
                        {
                            SuccessBinMsg("Successfully saved the loadbay details...!");
                            DocLoadBayClean();
                            Session["Isupdate"] = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorBinMsg(ex.Message);
                }
            }
            SaveUpdateDocLoadingBay();
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
        }

        protected void SaveUpdateDocLoadingBay()
        {
            Int32 row_aff = 0;
            try
            {
                if (ViewState["StoreLB1"] == null || ViewState["StoreLB1"].ToString() == "")
                {
                    ErrorBinMsg("No Record found");
                }
                else
                {
                    row_aff = (Int32)CHNLSVC.General.saveDocLoadingBay(_DocLoadingBay);
                    if (row_aff == 1)
                    {
                        SuccessBinMsg("Successfully saved the loadingbay details...!");
                        DocLoadBayClean();
                        Session["Isupdate"] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorBinMsg(ex.Message);
            }
        }

        protected void lbtnLoadBayClean_Click(object sender, EventArgs e)
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
                LoadBayClean();
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        protected void grdLocationBay_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdLocationBay.Rows)
            {
                Label act = (Label)row.FindControl("col_Active");
                Label dft = (Label)row.FindControl("col_Default");
                string actVal = act.Text.Trim();
                string dftVal = dft.Text.Trim();
                (row.FindControl("col_Active") as Label).Text = actVal == "1" ? "Yes" : "No";
                (row.FindControl("col_Default") as Label).Text = dftVal == "1" ? "Yes" : "No";
            }
        }

        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {
            validateUserId();
        }

        protected void validateUserId()
        {
            if (txtUserId.Text != null && txtUserId.Text != "")
            {
                DummyDataBind();
                _result = CHNLSVC.CommonSearch.Get_UsersFrompda(Session["UserCompanyCode"].ToString(), ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                bool contains = _result.AsEnumerable().Any(row => txtUserId.Text == row.Field<String>("CODE"));
                if (!contains)
                {
                    ErrorBinMsg(txtUserId.Text + " is an Invalid user ID!");
                    txtUserId.Text = string.Empty;
                    return;
                }
                lblUserNameView.Text = (from DataRow dr in _result.Rows where (string)dr["CODE"] == txtUserId.Text select (string)dr["DESCRIPTION"]).FirstOrDefault();
                lblvalue.Text = "";
                Session["LoadingBayUserID"] = txtUserId.Text;
            }
        }

        protected void txtLocationID_TextChanged(object sender, EventArgs e)
        {
            validateLoadBayLocationId();
        }

        protected void validateLoadBayLocationId()
        {
            if (Session["LoadingBayUserID"] == null || Session["LoadingBayUserID"].ToString() == "")
            {
                Session["LoadingBayUserID"] = txtUserId.Text;
            }
            if (txtUserId.Text != null && txtUserId.Text != "")
            {
                DummyDataBind();
                grdResult.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.LoadingBayLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationFrompda(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                bool contains = _result.AsEnumerable().Any(row => txtLocationID.Text == row.Field<String>("CODE"));
                if (!contains)
                {
                    ErrorBinMsg(txtLocationID.Text + " is an Invalid Location Code!");
                    txtLocationID.Text = string.Empty;
                    return;
                }

                lblLocationView.Text = (from DataRow dr in _result.Rows where (string)dr["CODE"] == txtLocationID.Text select (string)dr["DESCRIPTION"]).FirstOrDefault();
                lblvalue.Text = "";
                Session["LoadingBayUserID"] = string.Empty;
            }
            else
                ErrorBinMsg("Please Select A User ID First");
        }

        protected void validateLoadingBayId()
        {
            DummyDataBind();
            string SearchParams = "LB";
            DataTable _result = CHNLSVC.CommonSearch.Get_LoadingBays(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            bool contains = _result.AsEnumerable().Any(row => txtLoadingBayID.Text == row.Field<String>("CODE"));
            if (!contains)
            {
                ErrorBinMsg(txtLoadingBayID.Text + " is an Invalid LoadingBay Code!");
                txtLoadingBayID.Text = string.Empty;
                return;
            }

            lblLoadingBayView.Text = (from DataRow dr in _result.Rows where (string)dr["CODE"] == txtLoadingBayID.Text select (string)dr["DESCRIPTION"]).FirstOrDefault();
            lblvalue.Text = "";
        }

        protected void txtLoadingBayID_TextChanged(object sender, EventArgs e)
        {
            validateLoadingBayId();
        }

        protected void validateDocLoadingBayId()
        {
            DummyDataBind();
            string SearchParams = "LB";
            DataTable _result = CHNLSVC.CommonSearch.Get_LoadingBays(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            bool contains = _result.AsEnumerable().Any(row => txtDocLoadBay.Text == row.Field<String>("CODE"));
            if (!contains)
            {
                ErrorBinMsg(txtDocLoadBay.Text + " is an Invalid LoadingBay Code!");
                txtDocLoadBay.Text = string.Empty;
                return;
            }

            lblLoadingDocBayView.Text = (from DataRow dr in _result.Rows where (string)dr["CODE"] == txtDocLoadBay.Text select (string)dr["DESCRIPTION"]).FirstOrDefault();
            lblvalue.Text = "";
        }

        protected void txtDocLoadBay_TextChanged(object sender, EventArgs e)
        {
            validateDocLoadingBayId();
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            validateDocNo();
        }

        protected void validateDocNo()
        {
            if (txtDocNo.Text != null && txtDocNo.Text != "")
            {
                DummyDataBind();
                string SearchParams = Session["UserCompanyCode"].ToString();
                //_result = CHNLSVC.CommonSearch.Get_UsersFrompda(Session["UserCompanyCode"].ToString(), ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                _result = CHNLSVC.CommonSearch.Get_LoadingBaysDoc(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
            
                bool contains = _result.AsEnumerable().Any(row => txtDocNo.Text == row.Field<String>("CODE"));
                if (!contains)
                {
                    ErrorBinMsg(txtDocNo.Text + " is an Invalid Company Code!");
                    txtDocNo.Text = string.Empty;
                    return;
                }
                lblDocNoView.Text = (from DataRow dr in _result.Rows where (string)dr["CODE"] == txtDocNo.Text select (string)dr["DESCRIPTION"]).FirstOrDefault();
                lblvalue.Text = "";
            }
        }
    }
}