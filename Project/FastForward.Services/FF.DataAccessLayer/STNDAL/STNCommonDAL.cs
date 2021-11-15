using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Oracle.DataAccess.Client;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects;
using System.Transactions;
using System.Globalization;


namespace FF.DataAccessLayer
{
    public class STNCommonDAL : STNBaseDAL
    {

        string itm_Desc = "";
        string itm_Model = "";
        string itm_Brand = "";
        string itm_Cate_1 = "";
        string itm_Cate_2 = "";
        string itm_Cate_3 = "";
        string itm_Uom = "";
        string itm_Serialize = "";
        string itm_Warranty = "";

        Int16 Move_I_Line_No = 0;
        Int16 Move_C_Line_No = 0;
        Int16 Move_Fifo_Line_No = 0;
        Int16 Move_S_Line_No = 0;

        string p_LoginUser = "EMS";
        string p_ComCostMethod = "FIFO";

        public DataTable GetAllFAMaterialRequestsList(string _com, string _loc, DateTime _from, DateTime _to, string _tp, string _reqno, string _reqby)
        {
            OracleParameter[] param = new OracleParameter[8];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value =_com ;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value =_loc ;
            (param[2] = new OracleParameter("p_fromDate", OracleDbType.Date, null, ParameterDirection.Input)).Value =_from ;
            (param[3] = new OracleParameter("p_toDate", OracleDbType.Date, null, ParameterDirection.Input)).Value =_to ;
            (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value =_tp ;
            (param[5] = new OracleParameter("p_reqNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value =_reqno ;
            (param[6] = new OracleParameter("p_reqby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value =_reqby ;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = new DataTable();

                _dtResults = QueryDataTable("tblReq", "pkg_trans_fa.sp_getallfamaterialrequests", CommandType.StoredProcedure, false, param);
                
            return _dtResults;
        }

        public DataTable GetFAItem( string _item)
        {

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = QueryDataTable("tblItem", "pkg_trans_fa.sp_get_allitemdetails", CommandType.StoredProcedure, false, param);

            return _itemTable;
        }

        public DataTable getSearchFAItemStatusData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            DataTable _dtResults = null;

            //Spliit for Search control type.
            if (_initialSearchParams.Contains(":"))
            {
                string[] arr = _initialSearchParams.Split(new string[] { ":" }, StringSplitOptions.None);
                _initialSearchParams = arr[1];
            }

            //Spliit for Search SP parameters.
            string[] seperator = new string[] { "|" };
            string[] searchParams = _initialSearchParams.Split(seperator, StringSplitOptions.None);

            string _code = null;
            string _desc = null;

            //Set relavant parameters according to the,search catergory.
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "Code":
                        _code = _searchText;
                        break;
                    case "Description":
                        _desc = _searchText;
                        break;

                    default:
                        break;
                }
            }

            //Modify parameter values for LIKE search.
            _code = (_code != null) ? (_code.ToUpper() + "%") : null;
            _desc = (_desc != null) ? (_desc.ToUpper() + "%") : null;

            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[1] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _desc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblCompany", "pkg_search_FA.sp_searchitemstusdata", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable GetAutoNumber(string _module, string _loc)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblAuto", "pkg_trans_fa.sp_getautonumber", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public Int16 UpdateAutoNumber(string _module,string _startchar,string _loc)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;
            (param[1] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _startchar;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateRecords("pkg_trans_fa.sp_updateautonumber", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        public List<FA_Inventory_Req_Itm> GetInventoryFARequestItemDataByReqNo(FA_Inventory_Req_Hdr _inputInventoryRequest)
        {
            List<FA_Inventory_Req_Itm> _inventoryRequestItemList = null;
            FA_Inventory_Req_Itm _inventoryRequestItem = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inputInventoryRequest.Disp_reqt_seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInventoryRequestItemData", "pkg_trans_fa.sp_getrequestitemdata", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _inventoryRequestItemList = new List<FA_Inventory_Req_Itm>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    //_inventoryRequestItem = new FA_Inventory_Req_Itm();
                    //_inventoryRequestItem.Itri_seq_no = (_dtResults.Rows[i]["Itri_seq_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Itri_seq_no"]);
                    //_inventoryRequestItem.Itri_line_no = (_dtResults.Rows[i]["Itri_line_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Itri_line_no"]);
                    //_inventoryRequestItem.Itri_itm_stus = (_dtResults.Rows[i]["Itri_itm_stus"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Itri_itm_stus"].ToString();
                    //_inventoryRequestItem.Itri_qty = (_dtResults.Rows[i]["Itri_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Itri_qty"]);
                    //_inventoryRequestItem.Itri_unit_price = (_dtResults.Rows[i]["Itri_unit_price"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Itri_unit_price"]);
                    //_inventoryRequestItem.Itri_app_qty = (_dtResults.Rows[i]["Itri_app_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Itri_app_qty"]);
                    //_inventoryRequestItem.Itri_res_no = (_dtResults.Rows[i]["Itri_res_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Itri_res_no"].ToString();
                    //_inventoryRequestItem.Itri_note = (_dtResults.Rows[i]["Itri_note"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Itri_note"].ToString();
                    //_inventoryRequestItem.Itri_mitm_cd = (_dtResults.Rows[i]["itri_mitm_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["itri_mitm_cd"].ToString();
                    //_inventoryRequestItem.Itri_mitm_stus = (_dtResults.Rows[i]["itri_mitm_stus"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["itri_mitm_stus"].ToString();
                    //_inventoryRequestItem.Itri_mqty = (_dtResults.Rows[i]["itri_mqty"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["itri_mqty"]);
                    //_inventoryRequestItem.Itri_bqty = (_dtResults.Rows[i]["itri_bqty"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["itri_bqty"]);

                    ////Added By Prabhath on 12/01/2012
                    //_inventoryRequestItem.Itri_itm_cd = (_dtResults.Rows[i]["itri_itm_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["itri_itm_cd"].ToString();
                    //_inventoryRequestItem.Mi_longdesc = (_dtResults.Rows[i]["mi_longdesc"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mi_longdesc"].ToString();
                    //_inventoryRequestItem.Mi_brand = (_dtResults.Rows[i]["mi_brand"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mi_brand"].ToString();
                    //_inventoryRequestItem.Mi_model = (_dtResults.Rows[i]["mi_model"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mi_model"].ToString();

                    //_inventoryRequestItem.Itri_mitm_cd = (_dtResults.Rows[i]["itri_itm_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["itri_itm_cd"].ToString();

                    _inventoryRequestItem.MasterItem = new MasterItem()
                    {
                        Mi_cd = (_dtResults.Rows[i]["itri_itm_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["itri_itm_cd"].ToString(),
                        Mi_longdesc = (_dtResults.Rows[i]["mi_longdesc"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mi_longdesc"].ToString(),
                        Mi_brand = (_dtResults.Rows[i]["mi_brand"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mi_brand"].ToString(),
                        Mi_model = (_dtResults.Rows[i]["mi_model"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["mi_model"].ToString()
                    };

                    _inventoryRequestItemList.Add(_inventoryRequestItem);
                }
            }

            return _inventoryRequestItemList;
        }

        //kapila
        public FA_Inventory_Req_Hdr GetInventoryFARequestDataByReqNo(FA_Inventory_Req_Hdr _inputInventoryRequest)
        {
            FA_Inventory_Req_Hdr _inventoryRequest = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inputInventoryRequest.Disp_reqt_seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInventoryRequestData", "pkg_trans_fa.sp_getrequestdata", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _inventoryRequest = DataTableExtensions.ToGenericList<FA_Inventory_Req_Hdr>(_dtResults, FA_Inventory_Req_Hdr.Converter)[0];
            }

            return _inventoryRequest;
        }

        public Int32 SaveFARequestHeader(FA_Inventory_Req_Hdr _reqHdr)
        {
            OracleParameter[] param = new OracleParameter[38];
            Int32 effects = 0;


            (param[0] = new OracleParameter("p_DISP_REQT_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_reqt_seq;
            (param[1] = new OracleParameter("p_DISP_REF_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_ref_no;
            (param[2] = new OracleParameter("p_DISP_LOCATION", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_location;
            (param[3] = new OracleParameter("p_DISP_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _reqHdr.Disp_date;
            (param[4] = new OracleParameter("p_DISP_AUTHORIZED_BY1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_authorized_by1;
            (param[5] = new OracleParameter("p_DISP_AUTHORIZED_BY2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_authorized_by2;
            (param[6] = new OracleParameter("p_DISP_AUTHORIZED_BY3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_authorized_by3;
            (param[7] = new OracleParameter("p_DISP_CREATED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_created_by;
            (param[8] = new OracleParameter("p_DISP_APPROVED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _reqHdr.Disp_approved_date;
            (param[9] = new OracleParameter("p_DISP_CREATED_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _reqHdr.Disp_created_date;
            (param[10] = new OracleParameter("p_DISP_COMMENTS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_comments;
            (param[11] = new OracleParameter("p_DISP_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_status;
            (param[12] = new OracleParameter("p_DISP_REQ_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_req_by;
            (param[13] = new OracleParameter("p_DISP_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_company;
            (param[14] = new OracleParameter("p_DISP_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_type;
            (param[15] = new OracleParameter("p_DISP_PREFER_LOCATION", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_prefer_location;
            (param[16] = new OracleParameter("p_DISP_REQUEST_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_request_type;
            (param[17] = new OracleParameter("p_DISP_LAST_MODIFY_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_last_modify_by;
            (param[18] = new OracleParameter("p_DISP_LAST_MODIFY_WHEN", OracleDbType.Date, null, ParameterDirection.Input)).Value = _reqHdr.Disp_last_modify_when;
            (param[19] = new OracleParameter("p_DISP_PRODUCTION_STAGE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_production_stage;
            (param[20] = new OracleParameter("p_DISP_PRODUCTION_JOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_production_job;
            (param[21] = new OracleParameter("p_DISP_SUB_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_sub_type;
            (param[22] = new OracleParameter("p_MAIN_MRN_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Main_mrn_no;
            (param[23] = new OracleParameter("p_PRO_MRN_SUB_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Pro_mrn_sub_type;
            (param[24] = new OracleParameter("p_DISP_REQUEST_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _reqHdr.Disp_request_date;
            (param[25] = new OracleParameter("p_DISP_AODNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_aodno;
            (param[26] = new OracleParameter("p_DISP_DEL_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_del_by;
            (param[27] = new OracleParameter("p_DIPS_TOWN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Dips_town;
            (param[28] = new OracleParameter("p_DISP_SKIP_APP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqHdr.Disp_skip_app;
            (param[29] = new OracleParameter("p_DISP_PDA_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqHdr.Disp_pda_status;
            (param[30] = new OracleParameter("p_DISP_COLLECTOR_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_collector_id;
            (param[31] = new OracleParameter("p_DISP_COLLECTOR_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_collector_name;
            (param[32] = new OracleParameter("p_DISP_CUST_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_cust_code;
            (param[33] = new OracleParameter("p_DISP_TEMP_LOCA", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_temp_loca;
            (param[34] = new OracleParameter("p_DISP_SBU", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_sbu;
            (param[35] = new OracleParameter("p_DISP_DOC_REF_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqHdr.Disp_doc_ref_no;
            (param[36] = new OracleParameter("p_DISP_SKD_REQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqHdr.Disp_skd_req;

            param[37] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("pkg_trans_fa.sp_save_fa_req_hdr", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveFARequestItems(FA_Inventory_Req_Itm _reqItm)
        {
            OracleParameter[] param = new OracleParameter[41];
            Int32 effects = 0;


            (param[0] = new OracleParameter("p_DISPD_REQT_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_reqt_seq;
            (param[1] = new OracleParameter("p_DISPD_LINE_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_line_no;
            (param[2] = new OracleParameter("p_DISPD_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _reqItm.Dispd_date;
            (param[3] = new OracleParameter("p_DISPD_ITEM_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_item_code;
            (param[4] = new OracleParameter("p_DISPD_REQUSTED_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_requsted_qty;
            (param[5] = new OracleParameter("p_DISPD_UOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_uom;
            (param[6] = new OracleParameter("p_DISPD_ITEM_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_item_status;
            (param[7] = new OracleParameter("p_DISPD_APPROVED_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_approved_qty;
            (param[8] = new OracleParameter("p_DISPD_APPROVED_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_approved_item;
            (param[9] = new OracleParameter("p_DISPD_ITEM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_item_desc;
            (param[10] = new OracleParameter("p_DISPD_ITEM_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_item_model;
            (param[11] = new OracleParameter("p_DISPD_REMARKS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_remarks;
            (param[12] = new OracleParameter("p_DISPD_SHOP_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_shop_qty;
            (param[13] = new OracleParameter("p_DISPD_FORWARD_SALES", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_forward_sales;
            (param[14] = new OracleParameter("p_DISPD_BUFFER_LIMIT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_buffer_limit;
            (param[15] = new OracleParameter("p_DISPD_SR_REQUEST_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_sr_request_qty;
            (param[16] = new OracleParameter("p_DISPD_ITEM_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_item_brand;
            (param[17] = new OracleParameter("p_DISPD_AOD_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_aod_qty;
            (param[18] = new OracleParameter("p_DISPD_RES_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_res_no;
            (param[19] = new OracleParameter("p_DISPD_RES_REQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_res_req_no;
            (param[20] = new OracleParameter("p_DISPD_RES_LINE_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_res_line_no;
            (param[21] = new OracleParameter("p_DISPD_RES_ITEM_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Dispd_res_item_code;
            (param[22] = new OracleParameter("p_DISPD_RES_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_res_qty;
            (param[23] = new OracleParameter("p_DISPD_RES_BALQTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_res_balqty;
            (param[24] = new OracleParameter("p_DISPD_CHK_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_chk_status;
            (param[25] = new OracleParameter("p_DISP_PRO_IN_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_pro_in_qty;
            (param[26] = new OracleParameter("p_DISP_MRN_BAL_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_mrn_bal_qty;
            (param[27] = new OracleParameter("p_DISP_COST_APPLICABLE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_cost_applicable;
            (param[28] = new OracleParameter("p_DISP_ALTERNATE_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Disp_alternate_item;
            (param[29] = new OracleParameter("p_DISP_PRO_OUT_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_pro_out_qty;
            (param[30] = new OracleParameter("p_DISP_KIT_ITEM_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItm.Disp_kit_item_code;
            (param[31] = new OracleParameter("p_DISPD_CAN_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_can_qty;
            (param[32] = new OracleParameter("p_DISPD_BAL_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispd_bal_qty;
            (param[33] = new OracleParameter("p_DISP_RTN_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_rtn_qty;
            (param[34] = new OracleParameter("p_DISP_ANY_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_any_status;
            (param[35] = new OracleParameter("p_DISP_SO_RES", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_so_res;
            (param[36] = new OracleParameter("p_DISP_BASE_KLINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_base_kline;
            (param[37] = new OracleParameter("p_DISP_BASE_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_base_line;
            (param[38] = new OracleParameter("p_DISP_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Disp_line;
            (param[39] = new OracleParameter("p_DISPF_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _reqItm.Dispf_line;

            param[40] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("pkg_trans_fa.sp_save_fa_req_Itm", CommandType.StoredProcedure, param);
            return effects;
        }

        //kapila
        public DataTable getSearchFAItemData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            DataTable _dtResults = null;

            //Spliit for Search control type.
            if (_initialSearchParams.Contains(":"))
            {
                string[] arr = _initialSearchParams.Split(new string[] { ":" }, StringSplitOptions.None);
                _initialSearchParams = arr[1];
            }

            //Spliit for Search SP parameters.
            string[] seperator = new string[] { "|" };
            string[] searchParams = _initialSearchParams.Split(seperator, StringSplitOptions.None);

            string _code = null;
            string _desc = null;

            //Set relavant parameters according to the,search catergory.
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "Code":
                        _code = _searchText;
                        break;
                    case "Description":
                        _desc = _searchText;
                        break;

                    default:
                        break;
                }
            }

            //Modify parameter values for LIKE search.
            _code = (_code != null) ? (_code.ToUpper() + "%") : null;
            _desc = (_desc != null) ? (_desc.ToUpper() + "%") : null;

            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[1] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _desc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblCompany", "pkg_search_FA.sp_searchitemdata", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public void upload_MovementsMinus_RCC(DataRow dr)
        {

           // Open_Connections(0);
            OracleDataAdapter _oDa;
                     OracleDataReader _oRd;

                     int _ref = 0;

            string _sql = "";
            bool AOD_EX = false;

            int I_He = 0;
            //int I_It = 0;
            int I_Se = 0;

            string Str_Doc_Type = "";
            string para_Com = "";
            string para_Loca = "";
            //string Str_Module_Chr = "";

            string para_Ems_SeqNo = "";
            string para_Doc_No = "";
            string para_Ems_DocNo = "";
            string para_OthetDoc_No = "";
            System.DateTime para_Mov_Date = default(System.DateTime);
            string para_Other_Loca = "";
            string para_Profit_Center = "";
            string para_Channel = "";
            string para_Sub_Type = "";
            string para_Entry_Type = "";

            string para_bin = "";
            string para_ItemCode = "";
            string para_ItemStatus = "";
            double para_ItemQty = 0;
            double para_ItemCost = 0;

            string para_ItemSerial = "";
            string para_ItemChassis = "";
            string para_ItemWarr = "";
            System.DateTime para_Com_Grn_Date = default(System.DateTime);
            string para_Com_Grn_No = "";
            string para_Main_MFC = "N/A";

            Int16 _ems_itm_line = 0;
            Int16 _ems_batch_line = 0;
            Int16 _ems_ser_line = 0;
            string _ems_ser_id = "0";

            string p_LocaType = "";

            _oDa = new OracleDataAdapter();
            DataSet _emsData = new DataSet();


            _sql = "SELECT COMPANY_CODE, LOCATION_CODE, INV_DATE, DOC_REF_NO, LINE_NO, ITEM_CODE_GRN, STATUS, UNIT_COST,ENTRY_NO FROM INV_LOCATION_INVETORY_DETAILS " +
                    "WHERE COMPANY_CODE ='ABL' AND LOCATION_CODE ='XXX' AND ITEM_CODE_GRN ='AMFNW624' AND ENTRY_NO ='" + dr["INR_NO"].ToString() + "'";
            OracleCommand _oCom = new OracleCommand();
            //_oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            //_oRd = _oCom.ExecuteReader();

            //if (_oRd.HasRows == true)
            //{
            //    while (_oRd.Read())
            //    {

            //    }
            //}
            //else {
            //    return;
            //}

            int tserial = GetSerialID();
            string _docNo = DateTime.Now.Year + "SEQ" + tserial;


            _emsData.Tables.Add("EMS_INT_HDR");

            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_SEQ_NO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_COM");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_LOC");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DOC_NO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DOC_YEAR");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DOC_TP");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_CATE_TP");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_OTH_DOCNO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DOC_DATE");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_OTH_LOC");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_ENTRY_NO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_SUB_TP");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_ENTRY_TP");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_BUS_ENTITY");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_REMARKS");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_VEHI_NO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DIRECT");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_CHANNEL");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_PC");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DEL_ADD1");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DEL_ADD2");

            DataRow _hdr = _emsData.Tables["EMS_INT_HDR"].NewRow();
            _hdr["ITH_SEQ_NO"] = _docNo;
            _hdr["ITH_COM"] = dr["INR_COM_CD"].ToString();
            _hdr["ITH_LOC"] = "XXX";
            _hdr["ITH_DOC_NO"] = dr["INR_NO"].ToString();
            _hdr["ITH_DOC_YEAR"] = DateTime.Now.Date.Year;
            _hdr["ITH_DOC_TP"] = "AOD-OUT-LOCAL";
            _hdr["ITH_CATE_TP"] = "";
            _hdr["ITH_OTH_DOCNO"] = "";
            _hdr["ITH_DOC_DATE"] = DateTime.Now.Date;
            _hdr["ITH_OTH_LOC"] = "";
            _hdr["ITH_ENTRY_NO"] = dr["INR_NO"].ToString();
            _hdr["ITH_BUS_ENTITY"] = "";
            _hdr["ITH_REMARKS"] = "RCC AGENT";
            _hdr["ITH_VEHI_NO"] = "";
            _hdr["ITH_DIRECT"] = "1";
            _hdr["ITH_CHANNEL"] = "0";
            _hdr["ITH_PC"] = "N/A";
            _hdr["ITH_REMARKS"] = "";
            _hdr["ITH_DEL_ADD1"] = "N/A";
            _hdr["ITH_DEL_ADD2"] = "N/A";

            _emsData.Tables["EMS_INT_HDR"].Rows.Add(_hdr);


            _sql = " SELECT GRN_DATE,COM_REF_NO FROM INV_ITEM_SERIAL_DETAILS WHERE COMPANY_CODE ='" + dr["INR_COM_CD"].ToString() + "' AND LOCATION_CODE ='XXX' AND DOC_REF_NO ='" + dr["INR_NO"].ToString() + "' AND " +
                  "ITEM_CODE ='" + dr["INR_ITM"].ToString() + "' AND ITEM_STATUS ='GOOD' AND AVAILABILITY ='YES'";
            _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oRd = _oCom.ExecuteReader();
            string item = "";
            string itemStatus = "";
            string unitCost = "";
            string serial = "";
            string chassis = "";
            string warranty = "";
            string grnDate = "";
            string grnNo = "";
            string serialId = "";
            string crossItem = "";
            string crossBatch = "";
            string serialLine = "";
            string docNo = "";
            if (_oRd.HasRows == true)
            {
                while (_oRd.Read())
                {
                    item = _oRd["item_code"].ToString();
                    itemStatus = _oRd["item_status"].ToString();
                    unitCost = _oRd["unit_cost"].ToString();
                    serial = _oRd["serial_no"].ToString();
                    chassis = _oRd["chassis_no"].ToString();
                    warranty = _oRd["warrenty_no"].ToString();
                    grnDate = _oRd["grn_date"].ToString();
                    grnNo = _oRd["com_ref_no"].ToString();
                    serialId = _oRd["grna_no"].ToString();
                    crossItem = _oRd["item_line_no"].ToString();
                    crossBatch = _oRd["item_line_no"].ToString();
                    serialLine = _oRd["item_line_no"].ToString();
                    docNo = _oRd["doc_ref_no"].ToString();
                }
            }
            else
            {
                return;
            }

            _emsData.Tables.Add("EMS_INT_ITM");

            _emsData.Tables["EMS_INT_ITM"].Columns.Add("DOCNO");
            _emsData.Tables["EMS_INT_ITM"].Columns.Add("ITEM");
            _emsData.Tables["EMS_INT_ITM"].Columns.Add("ITEMSTATUS");
            _emsData.Tables["EMS_INT_ITM"].Columns.Add("QTY");

            DataRow _itm = _emsData.Tables["EMS_INT_ITM"].NewRow();



            _itm["DOCNO"] = _docNo;
            _itm["ITEM"] = item;
            _itm["ITEMSTATUS"] = itemStatus;
            _itm["QTY"] = 1;

            _emsData.Tables["EMS_INT_ITM"].Rows.Add(_itm);

            //get default bin
            _sql = "SELECT BN_BIN_CODE FROM WH_R_BIN_LOCATION_DEFINITION WHERE BN_LOCATION ='XXX' AND BN_DEFAULT =1";
            _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oRd = _oCom.ExecuteReader();
            string bin = "";
            if (_oRd.HasRows == true)
            {
                while (_oRd.Read())
                {
                    bin = _oRd["bn_bin_code"].ToString();
                }
            }
            else
            {
                return;
            }

            DataRow _ser = _emsData.Tables["EMS_INT_SER"].NewRow();

            _ser["BIN"] = bin;
            _ser["DOCNO"] = _docNo;
            _ser["ITEM"] = item;
            _ser["ITEMSTATUS"] = itemStatus;
            _ser["UNITCOST"] = unitCost;
            _ser["QTY"] = 1;
            _ser["SER1"] = serial;
            _ser["SER2"] = chassis;
            _ser["SER3"] = "";
            _ser["WARR"] = warranty;
            _ser["GRNDATE"] = grnDate;
            _ser["GRNNO"] = grnNo;
            _ser["SERID"] = serialId;
            _ser["ITM_LINE"] = "1";
            _ser["BATCH_LINE"] = "1";
            _ser["SER_LINE"] = "1";
            _ser["IN_ITM_LINE"] = crossItem;
            _ser["IN_BATCH_LINE"] = crossBatch;
            _ser["IN_SER_LINE"] = serialLine;
            _ser["IN_DOC_NO "] = docNo;

            _emsData.Tables["EMS_INT_SER"].Rows.Add(_ser);

            //Close_Connections();
            for (I_He = 0; I_He <= _emsData.Tables["EMS_INT_HDR"].Rows.Count - 1; I_He++)
            {
              //  Open_Connections(0);

                _sql = "SELECT DOC_NO FROM INV_MOVEMENT_HEADER WHERE (MANUAL_REF_NO =:p_docno) AND (TRANSACTION_LOCATION =:p_loc) ";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
               // //_oCom.Transaction = oTrSCM;
                _oCom.Parameters.Add(":p_docno", OracleDbType.NVarchar2).Value = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString();
                _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_LOC"].ToString();
                _oRd = _oCom.ExecuteReader();
                if (_oRd.HasRows == true)
                {
                    while (_oRd.Read())
                    {
                        AOD_EX = true;
                        //goto N1;
                    }
                }
                else { AOD_EX = false; }
                _oRd.Close();

                if (AOD_EX == false)
                {
                    Move_I_Line_No = 1;
                    Move_C_Line_No = 1;
                    Move_S_Line_No = 1;

                    //Str_Module_Chr = "ADJ";

                    #region assign header values
                    if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DIRECT"].ToString() == "1")
                    {
                        if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_TP"].ToString() == "ADJ")
                        {
                            Str_Doc_Type = "ADJ+";
                        }
                        else if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_TP"].ToString() == "AOD")
                        {
                            Str_Doc_Type = "AOD-IN-LOCAL";
                        }
                        else if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_TP"].ToString() == "GRN" || _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_CATE_TP"].ToString() == "LOCAL")
                        {
                            Str_Doc_Type = "GRN_LOCAL";
                        }
                        else if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_TP"].ToString() == "SRN")
                        {
                            Str_Doc_Type = "SRN";
                        }
                        else
                        {
                            Str_Doc_Type = "";
                        }
                    }

                    para_Ems_SeqNo = "EMS" + _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SEQ_NO"].ToString();
                    para_Doc_No = "(U)" + _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString();
                    para_Ems_DocNo = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString();
                    para_OthetDoc_No = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_DOCNO"].ToString();
                    para_Com = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_COM"].ToString();
                    para_Loca = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_LOC"].ToString();
                    para_Mov_Date = Convert.ToDateTime(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_DATE"]).Date;

                    para_Entry_Type = "N/A";

                    if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_LOC"].ToString())) _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_LOC"] = "N/A";
                    para_Other_Loca = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_LOC"].ToString();

                    if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SUB_TP"].ToString())) _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SUB_TP"] = "NORMAL";
                    para_Sub_Type = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SUB_TP"].ToString();

                    if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_PC"].ToString())) _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_PC"] = "N/A";
                    para_Profit_Center = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_PC"].ToString();

                    if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_CHANNEL"].ToString())) _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_CHANNEL"] = "N/A";
                    para_Channel = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_CHANNEL"].ToString();


                    //Tr Date - Amalgamation Modification 4/4/2011
                    System.DateTime trDate = DateTime.MinValue;
                    //if (get_tr_date(para_Pos_Mov_No, "IN") == true) {
                    //    trDate = para_TR_DATE;
                    //}

                    #endregion

                  //  Close_Connections();

                 //   EmsBegin();
                  //  ScmBegin();

                    #region Check AOD Out is SCM2 Location
                    if (Str_Doc_Type == "AOD-IN-LOCAL")
                    {
                        _sql = "SELECT LOC_CODE FROM SCM_LOCATION_MASTER WHERE COM_CODE =:p_com AND LOC_CODE =:p_loc AND IS_ONLINE ='SCM2'";
                        _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                        ////_oCom.Transaction = oTrSCM;
                        _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = para_Com;
                        _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = para_Other_Loca;
                        _oRd = _oCom.ExecuteReader();
                        if (_oRd.HasRows == true)
                        {
                            while (_oRd.Read())
                            {
                                para_OthetDoc_No = "(U)" + para_OthetDoc_No;
                            }
                        }
                        _oRd.Close();
                    }
                    #endregion

                    Insert_Movement_Header(para_Ems_SeqNo, para_Loca, para_Doc_No, para_Mov_Date.Year, Str_Doc_Type, para_OthetDoc_No, para_Mov_Date.Date, para_Other_Loca, para_Doc_No, "APPROVED", "EMS_UPLOAD", "ADJ+", "N/A", p_LoginUser, para_Ems_DocNo, para_Sub_Type, para_Entry_Type, para_Com, "N/A", "LKR", 1, "N/A", "N/A", "IN", para_Channel, para_Profit_Center, "N/A", "N/A", trDate.Date);

                    for (I_Se = 0; I_Se <= _emsData.Tables["EMS_INT_SER"].Rows.Count - 1; I_Se++)
                    {
                        if (_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["DOCNO"].ToString() == _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString())
                        {
                            #region assign serial values
                            para_bin = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["BIN"].ToString();
                            para_ItemCode = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["ITEM"].ToString();
                            para_ItemStatus = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["ITEMSTATUS"].ToString();
                            para_ItemQty = 1;
                            para_ItemQty = Convert.ToDouble(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["QTY"].ToString());

                            para_ItemCost = Convert.ToDouble(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["UNITCOST"].ToString());

                            Get_Item_Code_Details(para_ItemCode);

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"] = "N/A";
                            para_ItemSerial = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"].ToString();

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER2"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER2"] = "N/A";
                            para_ItemChassis = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER2"].ToString();

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["WARR"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["WARR"] = "N/A";
                            para_ItemWarr = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["WARR"].ToString();

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNDATE"].ToString()) || Convert.ToDateTime(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNDATE"]).Date == Convert.ToDateTime("30/Dec/1899").Date) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNDATE"] = Convert.ToDateTime("01/Jan/1900").Date;
                            para_Com_Grn_Date = Convert.ToDateTime(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNDATE"]).Date;

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNNO"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNNO"] = "N/A";
                            para_Com_Grn_No = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNNO"].ToString();

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER3"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER3"] = "N/A";
                            para_Main_MFC = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER3"].ToString();

                            if (itm_Serialize == "1")
                            {
                                if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"].ToString()))
                                {
                                    _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"] = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SERID"].ToString();
                                }
                                else
                                {
                                    if (_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"].ToString() == "N/A")
                                    {
                                        _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"] = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SERID"].ToString();
                                    }
                                }
                            }

                            _ems_itm_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["ITM_LINE"].ToString());
                            _ems_batch_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["BATCH_LINE"].ToString());
                            _ems_ser_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER_LINE"].ToString());
                            _ems_ser_id = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SERID"].ToString();

                            #endregion

                            Insert_Movement_Cost(p_ComCostMethod, para_Ems_SeqNo, Move_C_Line_No, para_Loca, p_LocaType, para_Doc_No, para_ItemCode, para_ItemStatus, para_ItemCost, para_ItemQty, itm_Uom, para_Doc_No, para_Doc_No, p_LoginUser, para_Mov_Date.Date, _ems_itm_line, _ems_batch_line, _ems_ser_line, para_Com, para_ItemCode, 0, itm_Cate_1, itm_Cate_2, itm_Cate_3, itm_Brand, itm_Model, para_ItemSerial, para_ItemChassis, para_ItemWarr, para_Mov_Date.Date, para_Com_Grn_Date.Date, para_Com_Grn_No, true, para_ItemCode, para_Main_MFC, true, para_bin, _ems_ser_id, string.Empty, 0, 0, 0, Str_Doc_Type);
                            //Insert_FIFO(p_ComCostMethod, para_Com, para_Loca, p_LocaType, Str_Doc_Type, para_Doc_No, para_Mov_Date.Date, Move_Fifo_Line_No, para_ItemCode, para_ItemStatus, para_ItemCost, "N/A", "N/A", para_ItemQty, para_ItemQty, para_ItemQty, 0, 0, p_LoginUser, "N/A", itm_Uom, "N/A", "N/A", para_Ems_SeqNo, "LKR", 1, 1, trDate.Date, _ems_itm_line, _ems_batch_line);
                            //Insert_Company_Inventory(para_Com, para_ItemCode, para_ItemStatus, para_ItemQty, para_ItemQty, 0, p_LoginUser);
                            Insert_Location_Inventory(para_Com, para_Loca, para_ItemCode, para_ItemStatus, para_ItemQty, para_ItemQty, 0, p_LoginUser);
                            //Insert_Bin_Inventory_Details(COMPANY_CODE, LOC_CODE, DEF_BIN, para_ItemCode, para_ItemStatus, para_ItemQty, LOGIN_US, para_Pos_Mov_No, para_Mov_Date)

                        }
                    }


                    _sql = "UPDATE INT_HDR SET ITH_ANAL_12 =1, ITH_ANAL_9 =CURRENT_DATE WHERE ITH_DOC_NO =:p_emsdocno";
                    _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                 //   _oCom.Transaction = oTrEMS;
                    _oCom.Parameters.Add(":p_emsdocno", OracleDbType.NVarchar2).Value = para_Ems_DocNo;
                    _ref = _oCom.ExecuteNonQuery();


                //    EmsCommit();
                //    ScmCommit();

                }
                //N1:
            }


            _emsData.Tables["EMS_INT_HDR"].Clear();
            _emsData.Tables["EMS_INT_ITM"].Clear();
            _emsData.Tables["EMS_INT_SER"].Clear();

        }

        public void upload_MovementsPlus_RCC(DataRow dr)
        {
          //  Open_Connections(0);

            OracleDataAdapter _oDa;
            OracleDataReader _oRd;

            int _ref = 0;

            string _sql = "";
            bool AOD_EX = false;

            int I_He = 0;
            //int I_It = 0;
            int I_Se = 0;

            string Str_Doc_Type = "";
            string para_Com = "";
            string para_Loca = "";
            //string Str_Module_Chr = "";

            //string para_SCM_Seq_No = "";
            //string para_SCM_Mov_No = "";
            //string para_SCM_Com_No = "";
            string para_Ems_SeqNo = "";
            string para_Doc_No = "";
            string para_Ems_DocNo = "";
            string para_OthetDoc_No = "";
            System.DateTime para_Mov_Date = default(System.DateTime);
            string para_Other_Loca = "";
            string para_Profit_Center = "";
            string para_Channel = "";
            string para_Sub_Type = "";
            string para_Entry_Type = "";

            string para_bin = "";
            string para_ItemCode = "";
            string para_ItemStatus = "";
            double para_ItemQty = 0;
            double para_ItemCost = 0;

            string para_ItemSerial = "";
            string para_ItemChassis = "";
            string para_ItemWarr = "";
            System.DateTime para_Com_Grn_Date = default(System.DateTime);
            string para_Com_Grn_No = "";
            string para_Main_MFC = "N/A";

            Int16 _ems_itm_line = 0;
            Int16 _ems_batch_line = 0;
            Int16 _ems_ser_line = 0;

            string _ems_scm_in_doc_no = "";
            Int16 _ems_in_itm_line = 0;
            Int16 _ems_in_batch_line = 0;
            Int16 _ems_in_ser_line = 0;

            string _ems_ser_id = "0";


            string p_LocaType = "";

            _oDa = new OracleDataAdapter();
            DataSet _emsData = new DataSet();

            // Get Header
            _sql = "select * from inv_movement_header where entry_no='" + dr["INR_NO"].ToString() + "' and doc_type='AOD-OUT-LOCAL' and  transaction_location='" + dr["INR_LOC_CD"].ToString() + "' and STATUS ='APPROVED'";
            OracleCommand _oCom = new OracleCommand();
            _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
          //  //_oCom.Transaction = oTrSCM;
            _oRd = _oCom.ExecuteReader();

            string _inDoc = "";
            if (_oRd.HasRows == true)
            {
                while (_oRd.Read())
                {
                    _inDoc = _oRd["doc_no"].ToString();
                }
            }
            else
            {
                return;
            }

            int tserial = GetSerialID();
            string _docNo = DateTime.Now.Year + "SEQ" + tserial;

            _emsData.Tables.Add("EMS_INT_HDR");

            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_SEQ_NO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_COM");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_LOC");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DOC_NO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DOC_YEAR");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DOC_TP");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_CATE_TP");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_OTH_DOCNO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DOC_DATE");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_OTH_LOC");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_ENTRY_NO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_SUB_TP");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_ENTRY_TP");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_BUS_ENTITY");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_REMARKS");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_VEHI_NO");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DIRECT");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_CHANNEL");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_PC");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DEL_ADD1");
            _emsData.Tables["EMS_INT_HDR"].Columns.Add("ITH_DEL_ADD2");

            DataRow _hdr = _emsData.Tables["EMS_INT_HDR"].NewRow();
            _hdr["ITH_SEQ_NO"] = _docNo;
            _hdr["ITH_COM"] = dr["INR_COM_CD"].ToString();
            _hdr["ITH_LOC"] = dr["INR_LOC_CD"].ToString();
            _hdr["ITH_DOC_NO"] = _docNo;
            _hdr["ITH_DOC_YEAR"] = DateTime.Now.Date.Year;
            _hdr["ITH_DOC_TP"] = "AOD-IN";
            _hdr["ITH_CATE_TP"] = "";
            _hdr["ITH_OTH_DOCNO"] = dr["INR_NO"].ToString();
            _hdr["ITH_DOC_DATE"] = DateTime.Now.Date;
            _hdr["ITH_OTH_LOC"] = "";
            _hdr["ITH_ENTRY_NO"] = dr["INR_NO"].ToString();
            _hdr["ITH_BUS_ENTITY"] = "";
            _hdr["ITH_REMARKS"] = "RCC_AGENT";
            _hdr["ITH_VEHI_NO"] = "";
            _hdr["ITH_DIRECT"] = "1";
            _hdr["ITH_CHANNEL"] = "";
            _hdr["ITH_PC"] = "";
            _hdr["ITH_REMARKS"] = "N/A";
            _hdr["ITH_DEL_ADD1"] = "N/A";
            _hdr["ITH_DEL_ADD2"] = "N/A";

            _emsData.Tables["EMS_INT_HDR"].Rows.Add(_hdr);

            //get serials
            _sql = "select * from inv_movement_cost where doc_no='" + _inDoc + "'";

            _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            //_oCom.Transaction = oTrSCM;
            _oRd = _oCom.ExecuteReader();
            string item = "";
            string itemStatus = "";
            string unitCost = "";
            string serial = "";
            string chassis = "";
            string warranty = "";
            string grnDate = "";
            string grnNo = "";

            string docNo = "";
            if (_oRd.HasRows == true)
            {
                while (_oRd.Read())
                {
                    item = _oRd["item_code"].ToString();
                    itemStatus = _oRd["status"].ToString();
                    unitCost = _oRd["unit_cost"].ToString();
                    grnDate = _oRd["grn_date"].ToString();
                    grnNo = _oRd["grn_no"].ToString();
                }
            }
            else
            {
                return;
            }


            _emsData.Tables.Add("EMS_INT_ITM");

            _emsData.Tables["EMS_INT_ITM"].Columns.Add("DOCNO");
            _emsData.Tables["EMS_INT_ITM"].Columns.Add("ITEM");
            _emsData.Tables["EMS_INT_ITM"].Columns.Add("ITEMSTATUS");
            _emsData.Tables["EMS_INT_ITM"].Columns.Add("QTY");

            DataRow _itm = _emsData.Tables["EMS_INT_ITM"].NewRow();

            _itm["DOCNO"] = _docNo;
            _itm["ITEM"] = item;
            _itm["ITEMSTATUS"] = itemStatus;
            _itm["QTY"] = 1;

            _emsData.Tables["EMS_INT_ITM"].Rows.Add(_itm);

            //Get Serials
            _sql = "SELECT EMS.INT_SER.ITS_BIN AS BIN, EMS.INT_SER.ITS_DOC_NO AS DOCNO, EMS.INT_SER.ITS_ITM_CD AS ITEM, EMS.MST_ITM_STUS.MIS_OLD_CD AS ITEMSTATUS, EMS.INT_SER.ITS_UNIT_COST AS UNITCOST, '1' AS QTY, " +
                "EMS.INT_SER.ITS_SER_1 AS SER1, EMS.INT_SER.ITS_SER_2 AS SER2, EMS.INT_SER.ITS_SER_3 AS SER3, EMS.INT_SER.ITS_WARR_NO AS WARR, EMS.INT_SER.ITS_ORIG_GRNDT AS GRNDATE, EMS.INT_SER.ITS_ORIG_GRNNO AS GRNNO, " +
                "EMS.INT_SER.ITS_SER_ID AS SERID, EMS.INT_SER.ITS_ITM_LINE AS ITM_LINE, EMS.INT_SER.ITS_BATCH_LINE AS BATCH_LINE, EMS.INT_SER.ITS_SER_LINE AS SER_LINE " +
                "FROM EMS.INT_SER" +
                "WHERE EMS.INT_SER=" + Convert.ToInt32(_emsData.Tables["EMS_INT_HDR"].Rows[0]["ITH_SEQ_NO"].ToString()) + "";

            _emsData.Tables.Add("EMS_INT_SER");

            _emsData.Tables["EMS_INT_SER"].Columns.Add("BIN");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("DOCNO");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("ITEM");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("ITEMSTATUS");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("UNITCOST");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("QTY");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("SER1");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("SER2");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("SER3");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("WARR");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("GRNDATE");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("GRNNO");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("SERID");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("ITM_LINE");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("BATCH_LINE");
            _emsData.Tables["EMS_INT_SER"].Columns.Add("SER_LINE");


            DataRow _ser = _emsData.Tables["EMS_INT_SER"].NewRow();
            //get default bin
            _sql = "SELECT BN_BIN_CODE FROM WH_R_BIN_LOCATION_DEFINITION WHERE BN_LOCATION ='XXX' AND BN_DEFAULT =1";
            _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            //_oCom.Transaction = oTrSCM;
            _oRd = _oCom.ExecuteReader();
            string bin = "";
            if (_oRd.HasRows == true)
            {
                while (_oRd.Read())
                {
                    bin = _oRd["bn_bin_code"].ToString();
                }
            }
            else
            {
                return;
            }

            _ser["BIN"] = bin;
            _ser["DOCNO"] = _docNo;
            _ser["ITEM"] = item;
            _ser["ITEMSTATUS"] = itemStatus;
            _ser["UNITCOST"] = unitCost;
            _ser["QTY"] = 1;
            _ser["SER1"] = dr["INR_SER"].ToString();
            _ser["SER2"] = "";
            _ser["SER3"] = "";
            _ser["WARR"] = dr[""].ToString();
            _ser["GRNDATE"] = "";
            _ser["GRNNO"] = "";
            _ser["SERID"] = "";
            _ser["ITM_LINE"] = "";
            _ser["BATCH_LINE"] = "";
            _ser["SER_LINE"] = "";

            _emsData.Tables["EMS_INT_SER"].Rows.Add(_ser);

      //     Close_Connections();

            for (I_He = 0; I_He <= _emsData.Tables["EMS_INT_HDR"].Rows.Count - 1; I_He++)
            {
           //     Open_Connections(0);
                // ** [ Check the doc No allready uploded ] **
                //_sql = "SELECT DOC_NO FROM INV_MOVEMENT_HEADER WHERE (MANUAL_REF_NO = '" + _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"] + "') AND (TRANSACTION_LOCATION = '" + para_Loca + "') AND (STATUS <>'CANCEL') ";
                _sql = "SELECT DOC_NO FROM INV_MOVEMENT_HEADER WHERE (DOC_NO =:p_docno) AND (TRANSACTION_LOCATION =:p_loc) ";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                _oCom.Parameters.Add(":p_docno", OracleDbType.NVarchar2).Value = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString();
                _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_LOC"].ToString();
                _oRd = _oCom.ExecuteReader();
                if (_oRd.HasRows == true)
                {
                    while (_oRd.Read())
                    {
                        AOD_EX = true;
                        //goto N1;
                    }
                }
                else
                {
                    AOD_EX = false;
                }
                _oRd.Close();

                if (AOD_EX == false)
                {
                    Move_I_Line_No = 1;
                    Move_C_Line_No = 1;
                    Move_S_Line_No = 1;

                    //Str_Module_Chr = "ADJ";

                    #region assign header values
                    if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DIRECT"].ToString() == "0")
                    {
                        if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_TP"].ToString() == "ADJ")
                        {
                            Str_Doc_Type = "ADJ-";
                        }
                        else if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_TP"].ToString() == "DO")
                        {
                            Str_Doc_Type = "DO-DPS";
                        }
                        else if (_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_TP"].ToString() == "AOD")
                        {
                            Str_Doc_Type = "AOD-OUT-LOCAL";
                        }
                    }

                    para_Ems_SeqNo = "EMS" + _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SEQ_NO"].ToString();
                    para_Doc_No = "(U)" + _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString();
                    para_Ems_DocNo = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString();
                    para_OthetDoc_No = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_DOCNO"].ToString();
                    para_Com = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_COM"].ToString();
                    para_Loca = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_LOC"].ToString();
                    para_Mov_Date = Convert.ToDateTime(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_DATE"]).Date;

                    para_Entry_Type = "N/A";

                    if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_LOC"].ToString())) _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_LOC"] = "N/A";
                    para_Other_Loca = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_OTH_LOC"].ToString();

                    if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SUB_TP"].ToString())) _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SUB_TP"] = "NORMAL";
                    para_Sub_Type = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_SUB_TP"].ToString();

                    if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_PC"].ToString())) _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_PC"] = "N/A";
                    para_Profit_Center = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_PC"].ToString();

                    if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_CHANNEL"].ToString())) _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_CHANNEL"] = "N/A";
                    para_Channel = _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_CHANNEL"].ToString();


                    //Tr Date - Amalgamation Modification 4/4/2011
                    System.DateTime trDate = DateTime.MinValue;
                    //if (get_tr_date(para_Pos_Mov_No, "IN") == true) {
                    //    trDate = para_TR_DATE;
                    //}

                    #endregion

               //     Close_Connections();

                 //   EmsBegin();
                  //  ScmBegin();

                    Insert_Movement_Header(para_Ems_SeqNo, para_Loca, para_Doc_No, para_Mov_Date.Year, Str_Doc_Type, para_OthetDoc_No, para_Mov_Date.Date, para_Other_Loca, para_Doc_No, "APPROVED", "EMS_UPLOAD", ".", "N/A", p_LoginUser, para_Ems_DocNo, para_Sub_Type, para_Entry_Type, para_Com, "N/A", "LKR", 1, "N/A", "N/A", "OUT", para_Channel, para_Profit_Center, "N/A", "N/A", trDate.Date);

                    for (I_Se = 0; I_Se <= _emsData.Tables["EMS_INT_SER"].Rows.Count - 1; I_Se++)
                    {
                        if (_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["DOCNO"].ToString() == _emsData.Tables["EMS_INT_HDR"].Rows[I_He]["ITH_DOC_NO"].ToString())
                        {
                            #region assign serial values
                            para_bin = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["BIN"].ToString();
                            para_ItemCode = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["ITEM"].ToString();
                            para_ItemStatus = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["ITEMSTATUS"].ToString();
                            para_ItemQty = 1;
                            para_ItemQty = Convert.ToDouble(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["QTY"].ToString());

                            para_ItemCost = Convert.ToDouble(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["UNITCOST"].ToString());

                            Get_Item_Code_Details(para_ItemCode);

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"] = "N/A";
                            para_ItemSerial = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"].ToString();

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER2"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER2"] = "N/A";
                            para_ItemChassis = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER2"].ToString();

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["WARR"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["WARR"] = "N/A";
                            para_ItemWarr = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["WARR"].ToString();

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNDATE"].ToString()) || Convert.ToDateTime(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNDATE"]).Date == Convert.ToDateTime("30/Dec/1899").Date) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNDATE"] = Convert.ToDateTime("01/Jan/1900").Date;
                            para_Com_Grn_Date = Convert.ToDateTime(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNDATE"]).Date;

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNNO"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNNO"] = "N/A";
                            para_Com_Grn_No = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["GRNNO"].ToString();

                            if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER3"].ToString())) _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER3"] = "N/A";
                            para_Main_MFC = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER3"].ToString();

                            if (itm_Serialize == "1")
                            {
                                if (string.IsNullOrEmpty(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"].ToString()))
                                {
                                    _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"] = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SERID"].ToString();
                                }
                                else
                                {
                                    if (_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"].ToString() == "N/A")
                                    {
                                        _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER1"] = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SERID"].ToString();
                                    }
                                }
                            }

                            _ems_itm_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["ITM_LINE"].ToString());
                            _ems_batch_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["BATCH_LINE"].ToString());
                            _ems_ser_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SER_LINE"].ToString());
                            _ems_ser_id = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["SERID"].ToString();

                            _ems_in_itm_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["IN_ITM_LINE"].ToString());
                            _ems_in_batch_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["IN_BATCH_LINE"].ToString());
                            _ems_in_ser_line = Convert.ToInt16(_emsData.Tables["EMS_INT_SER"].Rows[I_Se]["IN_SER_LINE"].ToString());

                            _ems_scm_in_doc_no = _emsData.Tables["EMS_INT_SER"].Rows[I_Se]["IN_DOC_NO"].ToString();

                            #endregion

                            Insert_Movement_Cost(p_ComCostMethod, para_Ems_SeqNo, Move_C_Line_No, para_Loca, p_LocaType, para_Doc_No, para_ItemCode, para_ItemStatus, para_ItemCost, para_ItemQty, itm_Uom, para_Doc_No, para_Doc_No, p_LoginUser, para_Mov_Date.Date, _ems_itm_line, _ems_batch_line, _ems_ser_line, para_Com, para_ItemCode, 0, itm_Cate_1, itm_Cate_2, itm_Cate_3, itm_Brand, itm_Model, para_ItemSerial, para_ItemChassis, para_ItemWarr, para_Mov_Date.Date, para_Com_Grn_Date.Date, para_Com_Grn_No, false, para_ItemCode, para_Main_MFC, false, para_bin, _ems_ser_id, _ems_scm_in_doc_no, _ems_in_itm_line, _ems_in_batch_line, _ems_in_ser_line, Str_Doc_Type);
                            Update_Location_Inventory(para_Com, para_Loca, para_ItemCode, para_ItemStatus, para_ItemQty, para_ItemQty, 0, p_LoginUser);
                        }
                    }

                    _sql = "UPDATE INT_HDR SET ITH_ANAL_12 =1, ITH_ANAL_9 =CURRENT_DATE WHERE ITH_DOC_NO =:p_emsdocno";
                    _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                    //_oCom.Transaction = oTrEMS;
                    _oCom.Parameters.Add(":p_emsdocno", OracleDbType.NVarchar2).Value = para_Ems_DocNo;
                    _ref = _oCom.ExecuteNonQuery();

              //      EmsCommit();
                //    ScmCommit();

                }
            }


            _emsData.Tables["EMS_INT_HDR"].Clear();
            _emsData.Tables["EMS_INT_ITM"].Clear();
            _emsData.Tables["EMS_INT_SER"].Clear();

        }

        void Update_Location_Inventory(string Str_Company_Code, string Str_Location_Code, string Str_Item_Code, string Str_Item_Status, double Str_Qty_In_Hand, double Str_Free_Qty, double Str_Res_Qty, string Str_User)
        {
            string _sql = "";
            int _ref = 0;
            _sql = "UPDATE INV_LOCATION_INVENTORY_HEADER SET QTY_IN_HAND = QTY_IN_HAND - :p_qty, FREE_QTY = FREE_QTY - :p_fqty, ISSUED_QTY = ISSUED_QTY + :p_qty , LAST_MODIFY_BY = :p_user, LAST_MODIFY_DATE = CURRENT_DATE " +
            " WHERE COMPANY_CODE = :p_company AND LOCATION_CODE = :p_location AND ITEM_CODE = :p_itemcode AND STATUS = :p_itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            //_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty_In_Hand;
            _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = Str_Free_Qty;
            //_oCom.Parameters.Add(":p_rqty", OracleDbType.Double).Value = Str_Res_Qty;
            _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
            _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Location_Code;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _ref = _oCom.ExecuteNonQuery();
        }

        void Insert_Location_Inventory(string Str_Company_Code, string Str_Location_Code, string Str_Item_Code, string Str_Item_Status, double Str_Qty_In_Hand, double Str_Free_Qty, double Str_Res_Qty, string Str_User)
        {
            int _ref = 0;
            string _sql = "";

            _sql = "UPDATE INV_LOCATION_INVENTORY_HEADER SET QTY_IN_HAND = QTY_IN_HAND + :p_qty, FREE_QTY = FREE_QTY + :p_fqty, RESERVED_QTY = RESERVED_QTY + :p_rqty, LAST_MODIFY_BY = :p_user, LAST_MODIFY_DATE = CURRENT_DATE " + " WHERE COMPANY_CODE = :p_company AND LOCATION_CODE = :p_location AND ITEM_CODE = :p_itemcode AND STATUS = :p_itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty_In_Hand;
            _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = Str_Free_Qty;
            _oCom.Parameters.Add(":p_rqty", OracleDbType.Double).Value = Str_Res_Qty;
            _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
            _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Location_Code;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _ref = _oCom.ExecuteNonQuery();
            if (_ref <= 0)
            {
                _sql = "INSERT INTO INV_LOCATION_INVENTORY_HEADER(COMPANY_CODE,LOCATION_CODE,ITEM_CODE,STATUS,QTY_IN_HAND,FREE_QTY,RESERVED_QTY,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_DATE) VALUES " + " (:p_company, :p_location, :p_itemcode, :p_itemstatus, :p_qty, :p_fqty, :p_rqty, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE)";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                ////_oCom.Transaction = oTrSCM;
                _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company_Code;
                _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Location_Code;
                _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
                _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
                _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty_In_Hand;
                _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = Str_Free_Qty;
                _oCom.Parameters.Add(":p_rqty", OracleDbType.Double).Value = Str_Res_Qty;
                _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
                _ref = _oCom.ExecuteNonQuery();
            }
        }

        void Insert_Movement_Cost(string Str_Cost_Method, string Str_Year_Seq_No, Int16 Str_MCost_Line_No, string Str_Transaction_Loca, string Str_Location_Cate,
    string Str_Doc_No, string Str_Item_Code, string Str_Item_Status, Double Str_Unit_Cost, Double Str_Qty, string Str_Uom, string Str_SCM_Doc_IN_No, string Str_Entry_no, string Str_User,
    DateTime Str_Doc_IN_Date, Int16 Str_Doc_Item_Line_No, Int16 Str_Doc_Batch_Line_No, Int16 Str_Doc_Ser_Line_No, string Str_Company, string Str_Tobond_Item_Code, Double Str_Unit_Amt, string Str_Cate_1, string Str_Cate_2, string Str_Cate_3,
    string Str_Brand_Code, string Str_Model_No, string Str_Serial_no, string Str_Chassis_No, string Str_Warr_no, DateTime Str_Doc_Date, DateTime Str_Com_Grn_Date, string Str_Com_Grn_No,
    Boolean Str_Is_IN_Doc, string Str_Item_Code_Original, string Str_MFC_Code, Boolean Str_IsInwardDoc, string Str_Bin, string Str_Ser_ID, string Str_In_Doc_No, Int16 Str_In_Itm_Line, Int16 Str_In_Batch_Line, Int16 Str_In_Ser_Line, string Str_Doc_Type)
        {
            int _ref = 0;
            string _sql = "";

            SAVE_INVENTORY_ITEM_TO_BIN_DETAILS(Str_Company, Str_Transaction_Loca, Str_Bin, Str_Item_Code, Str_Item_Status, Str_Qty, Str_Qty, Str_Qty, 0, Str_User, Str_Doc_No, Str_Doc_Date.Date);

            Int16 SCM_In_Line = 0;

            if (Str_Is_IN_Doc == false)
            {
                //UPDATE_COST     --> IN ITEM LINE
                //UPDATE_EX_COST  --> IN BATCH LINE
                Str_SCM_Doc_IN_No = Str_In_Doc_No;
                Str_SCM_Doc_IN_No = "(U)" + Str_SCM_Doc_IN_No;
                SCM_In_Line = Get_SCM_IN_Cost_Line_No(Str_SCM_Doc_IN_No, Str_Transaction_Loca, Str_Item_Code, Str_Item_Status, Str_Unit_Cost, Str_In_Itm_Line, Str_In_Batch_Line);
            }


            _sql = "UPDATE INV_MOVEMENT_COST SET QTY = QTY + :p_qty, CURRENT_QTY = CURRENT_QTY + :p_qty, UNIT_AMOUNT = UNIT_AMOUNT + :p_unitamt " +
            "WHERE YEAR_SEQ_NO = :p_yearseqno AND TRANSACTION_LOCATION = :p_loca AND ITEM_CODE = :p_itemcode AND STATUS = :p_itemstatus AND UNIT_COST = :p_unitcost " +
            "AND GRN_NO = :p_scmindoc AND INV_LINE_NO = :p_scminline AND TO_BOND_LINE_NO = :p_docitemline AND C_COST =:p_docbatchline";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty;
            _oCom.Parameters.Add(":p_unitamt", OracleDbType.Double).Value = Str_Unit_Cost * Str_Qty;
            _oCom.Parameters.Add(":p_yearseqno", OracleDbType.NVarchar2).Value = Str_Year_Seq_No;
            _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
            _oCom.Parameters.Add(":p_scmindoc", OracleDbType.NVarchar2).Value = Str_SCM_Doc_IN_No;
            _oCom.Parameters.Add(":p_scminline", OracleDbType.Int16).Value = SCM_In_Line;
            _oCom.Parameters.Add(":p_docitemline", OracleDbType.Int16).Value = Str_Doc_Item_Line_No;
            _oCom.Parameters.Add(":p_docbatchline", OracleDbType.Int16).Value = Str_Doc_Batch_Line_No;
            _ref = _oCom.ExecuteNonQuery();

            #region Update FIFO
            if (Str_Is_IN_Doc == true)
            {
                //_sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET QTY_IN_HAND= QTY_IN_HAND + :p_qty, FREE_QTY = FREE_QTY + :p_qty, LAST_MODIFY_BY = :p_user, LAST_MODIFY_WHEN = CURRENT_DATE " +
                // " WHERE COMPANY_CODE = :p_company AND LOCATION_CODE = :p_location AND DOC_REF_NO = :p_docrefno AND  ITEM_CODE_GRN = :p_itemcode AND  STATUS = :p_itemstatus AND UNIT_COST = :p_unitcost " +
                // "AND GRN_LINE_NO = :p_itmline AND TOBOND_LINE_NO = :p_batchline";

                //Edit by chamal 07/11/2012
                _sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET QTY_IN_HAND= QTY_IN_HAND + :p_qty, FREE_QTY = FREE_QTY + :p_qty, LAST_MODIFY_BY = :p_user, LAST_MODIFY_WHEN = CURRENT_DATE " +
                 " WHERE COMPANY_CODE = :p_company AND LOCATION_CODE = :p_location AND DOC_REF_NO = :p_docrefno AND  ITEM_CODE_GRN = :p_itemcode AND  STATUS = :p_itemstatus AND " +
                 " GRN_LINE_NO = :p_itmline AND TOBOND_LINE_NO = :p_batchline";

                //GRN_LINE_NO       = ITEM LINE
                //TOBOND_LINE_NO    = BATCH LINE
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                ////_oCom.Transaction = oTrSCM;
                _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty;
                _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
                _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company;
                _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
                _oCom.Parameters.Add(":p_docrefno", OracleDbType.NVarchar2).Value = Str_Doc_No;
                _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
                _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
                //_oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
                _oCom.Parameters.Add(":p_itmline", OracleDbType.Int16).Value = Str_Doc_Item_Line_No;
                _oCom.Parameters.Add(":p_batchline", OracleDbType.Int16).Value = Str_Doc_Batch_Line_No;
                _ref = _oCom.ExecuteNonQuery();
            }
            #endregion

            if (_ref <= 0)
            {
                _sql = "INSERT INTO INV_MOVEMENT_COST(YEAR_SEQ_NO,ITEM_LINE_NO,TRANSACTION_LOCATION,DOC_NO,ITEM_CODE,STATUS,UNIT_COST,QTY,UOM,GRN_NO,ENTRY_NO,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,INV_DATE,INV_LINE_NO,TO_BOND_LINE_NO,COMPANY_CODE,ITEM_CODE_TOBOND,CURRENT_QTY,UNIT_AMOUNT,CATOGARY_1_CODE,CATOGARY_2_CODE,CATOGARY_3_CODE,BRAND_CODE,MODEL_NO,ITEM_CODE_ORIGINAL,UPDATE_COST,UPDATE_EX_COST,C_COST) VALUES " +
                "(:p_yearseqno, :p_lineno, :p_loca, :p_docno, :p_itemcode, :p_itemstatus, :p_unitcost, :p_qty, :p_uom, :p_scmdocinno, :p_entryno, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE , :p_docindate, :p_scminlineno, :p_docitemline, :p_company, :p_tobonditemcode, :p_qty, :p_unitamt, :p_cate1, :p_cate2, :p_cate3, :p_brand, :p_model, :p_itemcodeorig, :p_initmline, :p_inbatchline, :p_docbatchline)";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                ////_oCom.Transaction = oTrSCM;
                _oCom.Parameters.Add(":p_yearseqno", OracleDbType.NVarchar2).Value = Str_Year_Seq_No;
                _oCom.Parameters.Add(":p_lineno", OracleDbType.Int16).Value = Str_MCost_Line_No;
                _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
                _oCom.Parameters.Add(":p_docno", OracleDbType.NVarchar2).Value = Str_Doc_No;
                _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
                _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
                _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
                _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty;
                _oCom.Parameters.Add(":p_uom", OracleDbType.NVarchar2).Value = Str_Uom;
                _oCom.Parameters.Add(":p_scmdocinno", OracleDbType.NVarchar2).Value = Str_SCM_Doc_IN_No;
                _oCom.Parameters.Add(":p_scminlineno", OracleDbType.Int16).Value = SCM_In_Line;
                _oCom.Parameters.Add(":p_docitemline", OracleDbType.Int16).Value = Str_Doc_Item_Line_No;
                _oCom.Parameters.Add(":p_entryno", OracleDbType.NVarchar2).Value = Str_Entry_no;
                _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
                _oCom.Parameters.Add(":p_docindate", OracleDbType.Date).Value = Str_Doc_IN_Date.Date;
                _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company;
                _oCom.Parameters.Add(":p_tobonditemcode", OracleDbType.NVarchar2).Value = Str_Tobond_Item_Code;
                _oCom.Parameters.Add(":p_unitamt", OracleDbType.Double).Value = Str_Unit_Amt;
                _oCom.Parameters.Add(":p_cate1", OracleDbType.NVarchar2).Value = Str_Cate_1;
                _oCom.Parameters.Add(":p_cate2", OracleDbType.NVarchar2).Value = Str_Cate_2;
                _oCom.Parameters.Add(":p_cate3", OracleDbType.NVarchar2).Value = Str_Cate_3;
                _oCom.Parameters.Add(":p_brand", OracleDbType.NVarchar2).Value = Str_Brand_Code;
                _oCom.Parameters.Add(":p_model", OracleDbType.NVarchar2).Value = Str_Model_No;
                _oCom.Parameters.Add(":p_itemcodeorig", OracleDbType.NVarchar2).Value = Str_Item_Code_Original;
                _oCom.Parameters.Add(":p_initmline", OracleDbType.Int16).Value = Str_In_Itm_Line;
                _oCom.Parameters.Add(":p_inbatchline", OracleDbType.Int16).Value = Str_In_Batch_Line;
                _oCom.Parameters.Add(":p_docbatchline", OracleDbType.Int16).Value = Str_Doc_Batch_Line_No;
                _ref = _oCom.ExecuteNonQuery();

                #region Insert FIFO
                if (Str_Is_IN_Doc == true)
                {
                    _sql = "INSERT INTO INV_LOCATION_INVETORY_DETAILS(COMPANY_CODE,LOCATION_CODE,DOC_TYPE,DOC_REF_NO,INV_DATE,LINE_NO,ITEM_CODE_GRN,STATUS,GRN_LINE_NO,UNIT_COST,TOBOND_NO,ITEM_CODE_TOBOND,TOBOND_LINE_NO,QTY_TOBOND,QTY_IN_HAND,FREE_QTY,RESERVED_QTY,ISSUED_QTY,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,PI_NO,UOM,TO_BOND_UNIT_COST,SI_NO,LC_NO,ENTRY_NO,QTY_TOBOND_ACTUAL,CURRENCY_CODE,EXCHANGE_RATE,LATEST_COST_PICKING) VALUES " +
                    "(:p_company, :p_location, :p_doctype, :p_docrefno, :p_invdate, :p_movlineno, :p_itemcode, :p_itemstatus, :p_itmline, :p_unitcost, :p_tobondno, :p_tobitemcode, :p_batchline, :p_tobqty, :p_qty, :p_fqty, :p_rqty, :p_iqty, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE, :p_pino, :p_uom, :p_unitcost, :p_sino, :p_lcno, :p_entryno, :p_qty, :p_currencycode, :p_exchangerate, :p_latestcostpick)";
                    _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                    ////_oCom.Transaction = oTrSCM;
                    _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company;
                    _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
                    _oCom.Parameters.Add(":p_doctype", OracleDbType.NVarchar2).Value = Str_Doc_Type;
                    _oCom.Parameters.Add(":p_docrefno", OracleDbType.NVarchar2).Value = Str_Doc_No;
                    _oCom.Parameters.Add(":p_invdate", OracleDbType.Date).Value = Str_Doc_Date.Date;
                    _oCom.Parameters.Add(":p_movlineno", OracleDbType.Int16).Value = Str_MCost_Line_No;
                    _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
                    _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
                    _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
                    _oCom.Parameters.Add(":p_tobondno", OracleDbType.NVarchar2).Value = Str_Entry_no;
                    _oCom.Parameters.Add(":p_tobitemcode", OracleDbType.NVarchar2).Value = Str_Tobond_Item_Code;
                    _oCom.Parameters.Add(":p_tobqty", OracleDbType.Double).Value = Str_Qty;
                    _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty;
                    _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = Str_Qty;
                    _oCom.Parameters.Add(":p_rqty", OracleDbType.Double).Value = 0;
                    _oCom.Parameters.Add(":p_iqty", OracleDbType.Double).Value = 0;
                    _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
                    _oCom.Parameters.Add(":p_pino", OracleDbType.NVarchar2).Value = "N/A";
                    _oCom.Parameters.Add(":p_uom", OracleDbType.NVarchar2).Value = Str_Uom;
                    _oCom.Parameters.Add(":p_sino", OracleDbType.NVarchar2).Value = "N/A";
                    _oCom.Parameters.Add(":p_lcno", OracleDbType.NVarchar2).Value = "N/A";
                    _oCom.Parameters.Add(":p_entryno", OracleDbType.NVarchar2).Value = Str_Entry_no;
                    _oCom.Parameters.Add(":p_currencycode", OracleDbType.NVarchar2).Value = "LKR";
                    _oCom.Parameters.Add(":p_exchangerate", OracleDbType.Double).Value = "1";
                    _oCom.Parameters.Add(":p_latestcostpick", OracleDbType.Int16).Value = 0;
                    _oCom.Parameters.Add(":p_itmline", OracleDbType.Int16).Value = Str_Doc_Item_Line_No;
                    _oCom.Parameters.Add(":p_batchline", OracleDbType.Int16).Value = Str_Doc_Batch_Line_No;
                    _ref = _oCom.ExecuteNonQuery();
                }
                #endregion

                Insert_Movement_Item(Str_Year_Seq_No, Move_I_Line_No, Str_Item_Code, Str_Item_Status, Str_Qty, Str_Uom, p_LoginUser, Str_Transaction_Loca, Str_Doc_No, 0, Str_Cate_1, Str_Cate_2, Str_Cate_3, Str_Brand_Code, Str_Model_No);
                if (Str_Is_IN_Doc == true)
                {
                    Insert_Item_Serial_Details(Str_Company, Str_Transaction_Loca, Str_Bin, Str_Item_Code, Str_Item_Status, Str_Serial_no, Str_Warr_no, "YES", 1, Str_Doc_No, Move_C_Line_No, p_LoginUser, Str_Doc_Date.Date, Str_Unit_Cost, Str_Chassis_No, Str_Com_Grn_Date.Date, Str_Com_Grn_No, Move_S_Line_No, Str_MFC_Code, Str_Ser_ID);
                    Insert_Movement_Item_Serials(Str_Year_Seq_No, Move_C_Line_No, Str_Transaction_Loca, Str_Doc_No, Str_Doc_Date.Date, Str_Item_Code, Str_Item_Status, 1, Str_Bin, 0, Str_Serial_no, Str_Chassis_No, Str_Warr_no, "N/A", Str_Doc_Item_Line_No, Str_Com_Grn_Date.Date, Str_Com_Grn_No, Str_MFC_Code, Str_IsInwardDoc, Str_Ser_ID, Str_Doc_Batch_Line_No, Str_Doc_Ser_Line_No);
                }
                else
                {
                    Insert_Movement_Item_Serials(Str_Year_Seq_No, Move_C_Line_No, Str_Transaction_Loca, Str_Doc_No, Str_Doc_Date.Date, Str_Item_Code, Str_Item_Status, 1, Str_Bin, 0, Str_Serial_no, Str_Chassis_No, Str_Warr_no, "N/A", Str_Doc_Item_Line_No, Str_Com_Grn_Date.Date, Str_Com_Grn_No, Str_MFC_Code, Str_IsInwardDoc, Str_Ser_ID, Str_Doc_Batch_Line_No, Str_Doc_Ser_Line_No);
                    Update_FIFO_New(Str_Company, Str_Transaction_Loca, Str_Item_Code, Str_Item_Status, Str_Qty, Str_In_Doc_No, Str_In_Itm_Line, Str_In_Batch_Line, p_LoginUser);
                    Remove_Item_Serial_Details(Str_Company, Str_Transaction_Loca, Str_SCM_Doc_IN_No, Str_Bin, Str_Item_Code, Str_Item_Status, Str_Serial_no, Str_Ser_ID);
                }

                Move_Fifo_Line_No = Move_C_Line_No;
                Move_C_Line_No += 1;
            }
            else
            {
                Int16 arg_Move_Cost_Line = 0;

                //arg_Move_Cost_Line = Get_Mov_Cost_Line_No(Str_Year_Seq_No, Str_Transaction_Loca, Str_Item_Code, Str_Item_Status, Str_Unit_Cost, Str_SCM_Doc_IN_No, Str_Doc_Item_Line_No, Str_Doc_Batch_Line_No);
                //Move_Fifo_Line_No = arg_Move_Cost_Line;

                arg_Move_Cost_Line = Get_SCM_IN_Cost_Line_No(Str_SCM_Doc_IN_No, Str_Transaction_Loca, Str_Item_Code, Str_Item_Status, Str_Unit_Cost, Str_Doc_Item_Line_No, Str_Doc_Batch_Line_No);

                Insert_Movement_Item(Str_Year_Seq_No, Move_I_Line_No, Str_Item_Code, Str_Item_Status, Str_Qty, Str_Uom, p_LoginUser, Str_Transaction_Loca, Str_Doc_No, 0, Str_Cate_1, Str_Cate_2, Str_Cate_3, Str_Brand_Code, Str_Model_No);
                if (Str_Is_IN_Doc == true)
                {
                    Insert_Item_Serial_Details(Str_Company, Str_Transaction_Loca, Str_Bin, Str_Item_Code, Str_Item_Status, Str_Serial_no, Str_Warr_no, "YES", 1, Str_Doc_No, arg_Move_Cost_Line, p_LoginUser, Str_Doc_Date.Date, Str_Unit_Cost, Str_Chassis_No, Str_Com_Grn_Date.Date, Str_Com_Grn_No, Move_S_Line_No, Str_MFC_Code, Str_Ser_ID);
                    Insert_Movement_Item_Serials(Str_Year_Seq_No, arg_Move_Cost_Line, Str_Transaction_Loca, Str_Doc_No, Str_Doc_Date.Date, Str_Item_Code, Str_Item_Status, 1, Str_Bin, 0, Str_Serial_no, Str_Chassis_No, Str_Warr_no, "N/A", Str_Doc_Item_Line_No, Str_Com_Grn_Date.Date, Str_Com_Grn_No, Str_MFC_Code, Str_IsInwardDoc, Str_Ser_ID, Str_Doc_Batch_Line_No, Str_Doc_Ser_Line_No);
                }
                else
                {
                    Insert_Movement_Item_Serials(Str_Year_Seq_No, Move_C_Line_No, Str_Transaction_Loca, Str_Doc_No, Str_Doc_Date.Date, Str_Item_Code, Str_Item_Status, 1, Str_Bin, 0, Str_Serial_no, Str_Chassis_No, Str_Warr_no, "N/A", Str_Doc_Item_Line_No, Str_Com_Grn_Date.Date, Str_Com_Grn_No, Str_MFC_Code, Str_IsInwardDoc, Str_Ser_ID, Str_Doc_Batch_Line_No, Str_Doc_Ser_Line_No);
                    Update_FIFO_New(Str_Company, Str_Transaction_Loca, Str_Item_Code, Str_Item_Status, Str_Qty, Str_In_Doc_No, Str_In_Itm_Line, Str_In_Batch_Line, p_LoginUser);
                    Remove_Item_Serial_Details(Str_Company, Str_Transaction_Loca, Str_SCM_Doc_IN_No, Str_Bin, Str_Item_Code, Str_Item_Status, Str_Serial_no, Str_Ser_ID);
                }

            }
        }

        Int16 Get_SCM_IN_Cost_Line_No(string Str_DocNo, string Str_Transaction_Loca, string Str_Item_Code, string Str_Item_Status, Double Str_Unit_Cost, Int16 Str_IN_Item_Line, Int16 Str_IN_Batch_Line)
        {
            int _ref = 0;
            string _sql = "";

            OracleDataReader OrdMovCNo;
            Int16 Mov_Item_Line = 0;

            _sql = "SELECT ITEM_LINE_NO FROM INV_MOVEMENT_COST " +
              "WHERE TRANSACTION_LOCATION =:p_location AND DOC_NO =:p_doc AND ITEM_CODE =:p_itemcode AND STATUS =:p_itemstatus AND UNIT_COST =:p_unitcost AND " +
              "TO_BOND_LINE_NO = :p_initmline AND C_COST = :p_inbatchline";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
            _oCom.Parameters.Add(":p_doc", OracleDbType.NVarchar2).Value = Str_DocNo;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
            _oCom.Parameters.Add(":p_initmline", OracleDbType.Int16).Value = Str_IN_Item_Line;
            _oCom.Parameters.Add(":p_inbatchline", OracleDbType.Int16).Value = Str_IN_Batch_Line;
            OrdMovCNo = _oCom.ExecuteReader();
            if (OrdMovCNo.HasRows == true)
            {
                while (OrdMovCNo.Read())
                {
                    Mov_Item_Line = Convert.ToInt16(OrdMovCNo["ITEM_LINE_NO"].ToString());
                }
            }
            OrdMovCNo.Close();

            return Mov_Item_Line;
        }

        void Remove_Item_Serial_Details(string Str_Company_Code, string Str_Location_Code, string Str_Doc_No, string Str_Bin_Location, string Str_Item_Code, string Str_Item_Status, string Str_Serial_No, string Str_Ser_ID)
        {
            int _ref = 0;
            string _sql = "";
            _sql = "UPDATE INV_ITEM_SERIAL_DETAILS SET AVAILABILITY ='NO' " +
                " WHERE COMPANY_CODE =:p_comcode AND LOCATION_CODE =:p_loccode AND DOC_REF_NO =:p_docno AND ITEM_CODE = :p_itemcode AND ITEM_STATUS =:p_itemstatus AND SERIAL_NO =:p_serno AND GRNA_NO =:p_grna ";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_comcode", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":p_loccode", OracleDbType.NVarchar2).Value = Str_Location_Code;
            _oCom.Parameters.Add(":p_docno", OracleDbType.NVarchar2).Value = Str_Doc_No;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_serno", OracleDbType.NVarchar2).Value = Str_Serial_No;
            _oCom.Parameters.Add(":p_grna", OracleDbType.NVarchar2).Value = Str_Ser_ID;
            _ref = _oCom.ExecuteNonQuery();
        }

        void Insert_Item_Serial_Details(string Str_Company_Code, string Str_Location_Code, string Str_Bin_Location, string Str_Item_Code, string Str_Item_Status, string Str_Serial_No, string Str_Warrenty_No, string Str_Availability, Double Str_Qty, string Str_Doc_Ref_No, Int16 Str_Item_Line_No, string Str_User, DateTime Str_Inv_Date, Double Str_Unit_Cost, string Str_Chassis_No, DateTime Str_Com_Grn_Date, string Str_Com_Grn_No, Int16 Str_Updated_Line_No, string Str_MFC_Code, string Str_GRNA_NO)
        {
            int _ref = 0;
            string _sql = "";
            _sql = "INSERT INTO INV_ITEM_SERIAL_DETAILS " +
                " (COMPANY_CODE, LOCATION_CODE, BIN_LOCATION, ITEM_CODE, ITEM_STATUS, SERIAL_NO, WARRENTY_NO, AVAILABILITY, QTY, DOC_REF_NO, ITEM_LINE_NO, CREATE_BY, CREATE_WHEN, LAST_MODIFY_BY, LAST_MODIFY_WHEN, INV_DATE, UPDATE_LINE_NO, UNIT_COST, CHASSIS_NO, GRN_DATE, COM_REF_NO, MFC_CODE, GRNA_NO) " +
                " VALUES (:p_comcode, :p_loccode, :p_bincode, :p_itemcode, :p_itemstatus, :p_serno, :p_warrno, :p_availability, :p_qty, :p_docrefno, :p_itemlineno, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE, :p_invdate, :p_updatedlineno, :p_unitcost, :p_chassisno, :p_comgrndate, :p_comgrnno, :p_mfc, :p_grna)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_comcode", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":p_loccode", OracleDbType.NVarchar2).Value = Str_Location_Code;
            _oCom.Parameters.Add(":p_bincode", OracleDbType.NVarchar2).Value = Str_Bin_Location;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_serno", OracleDbType.NVarchar2).Value = Str_Serial_No;
            _oCom.Parameters.Add(":p_warrno", OracleDbType.NVarchar2).Value = Str_Warrenty_No.Replace("'", "`");
            _oCom.Parameters.Add(":p_availability", OracleDbType.NVarchar2).Value = Str_Availability;
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty;
            _oCom.Parameters.Add(":p_docrefno", OracleDbType.NVarchar2).Value = Str_Doc_Ref_No;
            _oCom.Parameters.Add(":p_itemlineno", OracleDbType.Int16).Value = Str_Item_Line_No;
            _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
            _oCom.Parameters.Add(":p_invdate", OracleDbType.Date).Value = Str_Inv_Date.Date;
            _oCom.Parameters.Add(":p_updatedlineno", OracleDbType.Int16).Value = Str_Updated_Line_No;
            _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
            _oCom.Parameters.Add(":p_chassisno", OracleDbType.NVarchar2).Value = Str_Chassis_No;
            _oCom.Parameters.Add(":p_comgrndate", OracleDbType.Date).Value = Str_Com_Grn_Date.Date;
            _oCom.Parameters.Add(":p_comgrnno", OracleDbType.NVarchar2).Value = Str_Com_Grn_No;
            _oCom.Parameters.Add(":p_mfc", OracleDbType.NVarchar2).Value = Str_MFC_Code;
            _oCom.Parameters.Add(":p_grna", OracleDbType.NVarchar2).Value = Str_GRNA_NO;
            _ref = _oCom.ExecuteNonQuery();
        }

        void Update_FIFO_New(string Str_Company_Code, string Str_Location_Code, string Str_Item_Code, string Str_Item_Status, double Str_Qty, string Str_In_Doc_No, Int16 Str_In_Itm_Line, Int16 Str_In_Batch_Line, string Str_User)
        {
            string _sql = "";
            int _ref = 0;
            _sql = "UPDATE INV_LOCATION_INVETORY_DETAILS SET QTY_IN_HAND = QTY_IN_HAND - :qty, FREE_QTY = FREE_QTY - :qty, ISSUED_QTY = ISSUED_QTY + :qty, LAST_MODIFY_BY =:loginuser, LAST_MODIFY_WHEN = CURRENT_DATE " +
             " WHERE COMPANY_CODE =:comcode AND LOCATION_CODE =:loccode AND DOC_REF_NO =:docrefno AND GRN_LINE_NO =:itmline AND TOBOND_LINE_NO =:batchline AND ITEM_CODE_GRN =:itemcode AND STATUS =:itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":qty", OracleDbType.Double).Value = Str_Qty;
            _oCom.Parameters.Add(":loginuser", OracleDbType.NVarchar2).Value = Str_User;
            _oCom.Parameters.Add(":comcode", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":loccode", OracleDbType.NVarchar2).Value = Str_Location_Code;
            _oCom.Parameters.Add(":docrefno", OracleDbType.NVarchar2).Value = "(U)" + Str_In_Doc_No;
            _oCom.Parameters.Add(":itmline", OracleDbType.Int16).Value = Str_In_Itm_Line;
            _oCom.Parameters.Add(":batchline", OracleDbType.Int16).Value = Str_In_Batch_Line;
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.CommandType = CommandType.Text;
            _ref = _oCom.ExecuteNonQuery();

            //Update Bin Inventory Details
            _sql = "UPDATE INV_BIN_INVENTORY_DETAILS " +
            "SET QTY_IN_HAND = QTY_IN_HAND - :qty, FREE_QTY = FREE_QTY - :qty, ISSUED_QTY = ISSUED_QTY - :qty, LAST_MODIFY_BY =:loginuser, LAST_MODIFY_WHEN = CURRENT_DATE " +
            "WHERE COMPANY_CODE =:comcode AND LOCATION_CODE =:loccode AND  ITEM_CODE =:itemcode AND ITEM_STATUS =:itemstatus AND DOC_REF_NO =:docrefno";
            _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":qty", OracleDbType.Double).Value = Str_Qty;
            _oCom.Parameters.Add(":loginuser", OracleDbType.NVarchar2).Value = Str_User;
            _oCom.Parameters.Add(":comcode", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":loccode", OracleDbType.NVarchar2).Value = Str_Location_Code;
            _oCom.Parameters.Add(":itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":docrefno", OracleDbType.NVarchar2).Value = "(U)" + Str_In_Doc_No;
            _oCom.CommandType = CommandType.Text;
            _ref = _oCom.ExecuteNonQuery();

        }

        void Insert_Movement_Item(string Str_Year_Seq_No, Int16 Str_MItem_Line_No, string Str_Item_Code, string Str_Item_Status, Double Str_Qty, string Str_UOM, string Str_User, string Str_Transaction_Loca, string Str_Doc_No, Double Str_Unit_Price, string Str_Cate_1, string Str_Cate_2, string Str_Cate_3, string Str_Brand_Code, string Str_Model_No)
        {
            int _ref = 0;
            string _sql = "";
            _sql = "UPDATE INV_MOVEMENT_ITEM SET QTY = QTY + :p_qty, CURRENT_QTY = CURRENT_QTY + :p_qty " +
                "WHERE YEAR_SEQ_NO = :p_yearseqno AND TRANSACTION_LOCATION = :p_loca AND ITEM_CODE = :p_itemcode AND STATUS = :p_itemstatus";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_yearseqno", OracleDbType.NVarchar2).Value = Str_Year_Seq_No;
            _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty;
            _ref = _oCom.ExecuteNonQuery();
            if (_ref <= 0)
            {
                _sql = "INSERT INTO INV_MOVEMENT_ITEM(YEAR_SEQ_NO,ITEM_LINE_NO,ITEM_CODE,STATUS,QTY,UOM,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,TRANSACTION_LOCATION,DOC_NO,UNIT_COST,CURRENT_QTY,CATOGARY_1_CODE,CATOGARY_2_CODE,CATOGARY_3_CODE,BRAND_CODE,MODEL_NO)" +
                " VALUES(:p_yearseqno, :p_lineno, :p_itemcode, :p_itemstatus, :p_qty, :p_uom, :p_user, CURRENT_DATE, :p_user, CURRENT_DATE, :p_loca, :p_docno, :p_unitprice, :p_qty, :p_cate1, :p_cate2, :p_cate3, :p_brand, :p_model)";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                ////_oCom.Transaction = oTrSCM;
                _oCom.Parameters.Add(":p_yearseqno", OracleDbType.NVarchar2).Value = Str_Year_Seq_No;
                _oCom.Parameters.Add(":p_lineno", OracleDbType.NVarchar2).Value = Move_I_Line_No;
                _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
                _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
                _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty;
                _oCom.Parameters.Add(":p_uom", OracleDbType.NVarchar2).Value = Str_UOM;
                _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
                _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
                _oCom.Parameters.Add(":p_docno", OracleDbType.NVarchar2).Value = Str_Doc_No;
                _oCom.Parameters.Add(":p_unitprice", OracleDbType.Double).Value = Str_Unit_Price;
                _oCom.Parameters.Add(":p_cate1", OracleDbType.NVarchar2).Value = Str_Cate_1;
                _oCom.Parameters.Add(":p_cate2", OracleDbType.NVarchar2).Value = Str_Cate_2;
                _oCom.Parameters.Add(":p_cate3", OracleDbType.NVarchar2).Value = Str_Cate_3;
                _oCom.Parameters.Add(":p_brand", OracleDbType.NVarchar2).Value = Str_Brand_Code;
                _oCom.Parameters.Add(":p_model", OracleDbType.NVarchar2).Value = Str_Model_No;
                _ref = _oCom.ExecuteNonQuery();
                Move_I_Line_No += 1;
            }

        }

        void Insert_Movement_Item_Serials(string Str_Year_Seq_No, Int16 Str_MCost_Line_No, string Str_Transaction_Loca, string Str_Doc_No, DateTime Str_Doc_Date, string Str_Item_Code, string Str_Item_Status, Double Str_Qty, string Str_Bin_Code, Double Str_Unit_Cost, string Str_Serial_No, string Str_Chassis_No, string Str_Warranty_No, string Str_Doc_Ref_No, Double Str_Item_Line_No, DateTime Str_Com_In_Date, string Str_Com_In_No, string Str_MFC_Code, Boolean Str_IsInwardDoc, string Str_GRNA_NO, Int16 Str_Batch_Line_No, Int16 Str_Ser_Line_No)
        {
            int _ref = 0;
            string _sql = "";
            _sql = "INSERT INTO INV_MOVEMENT_ITEM_SERIALS " +
                " (YEAR_SEQ_NO, TRANSACTION_LOCATION, DOC_NO, DOC_DATE, BIN_LOCA, ITEM_LINE_NO, ITEM_CODE, ITEM_STATUS, SERIAL_SEQ_NO, SERIAL_NO, CHASSIS_NO, WARRENTY_NO, UNIT_PRICE, QTY, DOC_REF_NO, UPDATE_LINE_NO, GRN_DATE, COM_REF_NO, MFC_CODE, GRNA_NO, DOC_LINE_NO) " +
                " VALUES " +
                " (:p_yearseqno, :p_loca, :p_docno, :p_docdate, :p_bin, :p_mcostlineno, :p_itemcode, :p_itemstatus, :p_serialseqno, :p_serno, :p_chassisno, :p_warrno, :p_unitcost, :p_qty, :p_docrefno, :p_updatelineno, :p_comindate, :p_cominno, :p_mfc, :p_grna, :p_doclineno)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_yearseqno", OracleDbType.NVarchar2).Value = Str_Year_Seq_No;
            _oCom.Parameters.Add(":p_loca", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
            _oCom.Parameters.Add(":p_docno", OracleDbType.NVarchar2).Value = Str_Doc_No;
            _oCom.Parameters.Add(":p_docdate", OracleDbType.Date).Value = Str_Doc_Date.Date;
            _oCom.Parameters.Add(":p_bin", OracleDbType.NVarchar2).Value = Str_Bin_Code;
            _oCom.Parameters.Add(":p_mcostlineno", OracleDbType.Int16).Value = Str_MCost_Line_No;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_Item_Code;
            _oCom.Parameters.Add(":p_itemstatus", OracleDbType.NVarchar2).Value = Str_Item_Status;
            _oCom.Parameters.Add(":p_serno", OracleDbType.NVarchar2).Value = Str_Serial_No;
            _oCom.Parameters.Add(":p_chassisno", OracleDbType.NVarchar2).Value = Str_Chassis_No;
            _oCom.Parameters.Add(":p_warrno", OracleDbType.NVarchar2).Value = Str_Warranty_No.Replace("'", "`");
            _oCom.Parameters.Add(":p_unitcost", OracleDbType.Double).Value = Str_Unit_Cost;
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = Str_Qty;
            _oCom.Parameters.Add(":p_docrefno", OracleDbType.NVarchar2).Value = Str_Doc_Ref_No;
            _oCom.Parameters.Add(":p_updatelineno", OracleDbType.Int16).Value = Str_Item_Line_No;
            _oCom.Parameters.Add(":p_comindate", OracleDbType.Date).Value = Str_Com_In_Date.Date;
            _oCom.Parameters.Add(":p_cominno", OracleDbType.NVarchar2).Value = Str_Com_In_No;
            _oCom.Parameters.Add(":p_mfc", OracleDbType.NVarchar2).Value = Str_MFC_Code;
            _oCom.Parameters.Add(":p_grna", OracleDbType.NVarchar2).Value = Str_GRNA_NO;
            _oCom.Parameters.Add(":p_doclineno", OracleDbType.Int16).Value = Str_Batch_Line_No;
            _oCom.Parameters.Add(":p_serialseqno", OracleDbType.Int16).Value = Str_Ser_Line_No;
            _ref = _oCom.ExecuteNonQuery();


            Move_S_Line_No += 1;
        }

        void SAVE_INVENTORY_ITEM_TO_BIN_DETAILS(string STR_COMPANY_CODE, string STR_LOCATION_CODE, string STR_BIN_LOCATION, string STR_ITEM_CODE, string STR_ITEM_STATUS, double STR_QTY_IN_HAND, double STR_FREE_QTY, double STR_RESERVED_QTY, double STR_ISSUED_QTY, string STR_USER, string STR_DOC_REF_NO, System.DateTime STR_DOC_DATE)
        {
            int _ref = 0;
            string _sql = "";

            _sql = "UPDATE INV_BIN_INVENTORY_DETAILS " +
                "SET QTY_IN_HAND = QTY_IN_HAND + :p_qty, FREE_QTY = FREE_QTY + :p_fqty, LAST_MODIFY_BY = :p_user, LAST_MODIFY_WHEN = CURRENT_DATE " +
                "WHERE COMPANY_CODE =:p_com AND LOCATION_CODE =:p_loc AND BIN_CODE =:p_bin AND ITEM_CODE =:p_item AND ITEM_STATUS =:p_itemstaus AND DOC_REF_NO =:p_docrefno";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = STR_QTY_IN_HAND;
            _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = STR_FREE_QTY;
            _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = STR_USER;
            _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = STR_COMPANY_CODE;
            _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = STR_LOCATION_CODE;
            _oCom.Parameters.Add(":p_bin", OracleDbType.NVarchar2).Value = STR_BIN_LOCATION;
            _oCom.Parameters.Add(":p_item", OracleDbType.NVarchar2).Value = STR_ITEM_CODE;
            _oCom.Parameters.Add(":p_itemstaus", OracleDbType.NVarchar2).Value = STR_ITEM_STATUS;
            _oCom.Parameters.Add(":p_docrefno", OracleDbType.NVarchar2).Value = STR_DOC_REF_NO;
            _ref = _oCom.ExecuteNonQuery();
            if (_ref <= 0)
            {
                _sql = "INSERT INTO INV_BIN_INVENTORY_DETAILS " +
                    " (COMPANY_CODE, LOCATION_CODE, BIN_CODE, ITEM_CODE, ITEM_STATUS, DOC_REF_NO, INV_DATE, QTY_IN_HAND, FREE_QTY, " +
                    " RESERVED_QTY, ISSUED_QTY, CREATE_BY, CREATE_WHEN, LAST_MODIFY_BY, LAST_MODIFY_WHEN)" +
                    " VALUES(:p_com, :p_loc, :p_bin, :p_item, :p_itemstaus, :p_docrefno, :p_docdate, :p_qty, :p_fqty, :p_rqty, 0, :p_user,CURRENT_DATE , :p_user,CURRENT_DATE )";
                _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
                ////_oCom.Transaction = oTrSCM;
                _oCom.Parameters.Add(":p_qty", OracleDbType.Double).Value = STR_QTY_IN_HAND;
                _oCom.Parameters.Add(":p_fqty", OracleDbType.Double).Value = STR_FREE_QTY;
                _oCom.Parameters.Add(":p_rqty", OracleDbType.Double).Value = STR_RESERVED_QTY;
                _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = STR_USER;
                _oCom.Parameters.Add(":p_com", OracleDbType.NVarchar2).Value = STR_COMPANY_CODE;
                _oCom.Parameters.Add(":p_loc", OracleDbType.NVarchar2).Value = STR_LOCATION_CODE;
                _oCom.Parameters.Add(":p_bin", OracleDbType.NVarchar2).Value = STR_BIN_LOCATION;
                _oCom.Parameters.Add(":p_item", OracleDbType.NVarchar2).Value = STR_ITEM_CODE;
                _oCom.Parameters.Add(":p_itemstaus", OracleDbType.NVarchar2).Value = STR_ITEM_STATUS;
                _oCom.Parameters.Add(":p_docrefno", OracleDbType.NVarchar2).Value = STR_DOC_REF_NO;
                _oCom.Parameters.Add(":p_docdate", OracleDbType.Date).Value = STR_DOC_DATE.Date;
                _ref = _oCom.ExecuteNonQuery();
            }
        }

        //kapila 24/8/2012
        public DataTable GetRCCSerialSearchData(string _com, string _loc, int _isSameLoc, int _isStockItem, string _searchText, string _searchCriteria)
        {
            DataTable _dtResults = null;
            string _warranty = null;
            string _item = null;
            string _serial = null;
            string _docNo = null;

            if (!string.IsNullOrEmpty(_searchCriteria))
            {
                switch (_searchCriteria)
                {
                    case "WARRANTY":
                        _warranty = _searchText;
                        break;

                    case "ITEM":
                        _item = _searchText;
                        break;
                    case "SERIAL":
                        _serial = _searchText;
                        break;
                    case "DOCUMENT":
                        _docNo = _searchText;
                        break;
                    default:
                        break;
                }
            }

            //Modify parameter values for LIKE search.
            _warranty = (_warranty != null) ? (_warranty + "%") : null;
            _item = (_item != null) ? (_item.ToUpper() + "%") : null;
            _serial = (_serial != null) ? (_serial + "%") : null;
            _docNo = (_docNo != null) ? (_docNo.ToUpper() + "%") : null;

            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty;
            (param[3] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            (param[4] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            (param[5] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[6] = new OracleParameter("p_is_same_loc", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _isSameLoc;
            (param[7] = new OracleParameter("p_is_stock_item", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _isStockItem;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tbl", "sp_rcc_srch_serial", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }

        public DataTable FixedAssetBalDetails(string _User)
        {
            // Sanjeewa 30-10-2013         
            OracleParameter[] param = new OracleParameter[2];
            param[0] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);            
            (param[1] = new OracleParameter("IN_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "sp_get_fixed_asst_bal", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable FixedAssetBalDetailsNew(string _User, string _loc)
        {
            // Sanjeewa 30-10-2013         
            OracleParameter[] param = new OracleParameter[3];
            param[0] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);            
            (param[1] = new OracleParameter("IN_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            (param[2] = new OracleParameter("in_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "sp_get_fixed_asst_bal_new", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        public bool Get_Item_Code_Details(string Str_ItemCode)
        {
            string _sql = "";
            bool functionReturnValue = false;
            OracleDataReader OrdModel = default(OracleDataReader);

            functionReturnValue = false;

            _sql = "SELECT DESCRIPTION, MODEL_NO, BRAND_CODE, CATOGARY_1_CODE, CATOGARY_2_CODE, CATOGARY_3_CODE, BASE_UOM, SERIALIZE, WARRANTY FROM SCM_ITEM_MASTER WHERE (ITEM_CODE = :p_itemcode)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_ItemCode;
            OrdModel = _oCom.ExecuteReader();
            if (OrdModel.HasRows == true)
            {
                while (OrdModel.Read())
                {
                    functionReturnValue = true;
                    itm_Desc = OrdModel["DESCRIPTION"].ToString();
                    itm_Model = OrdModel["MODEL_NO"].ToString();
                    itm_Brand = OrdModel["BRAND_CODE"].ToString();
                    itm_Cate_1 = OrdModel["CATOGARY_1_CODE"].ToString();
                    itm_Cate_2 = OrdModel["CATOGARY_2_CODE"].ToString();
                    itm_Cate_3 = OrdModel["CATOGARY_3_CODE"].ToString();
                    itm_Uom = OrdModel["BASE_UOM"].ToString();
                    itm_Serialize = OrdModel["SERIALIZE"].ToString();
                    itm_Warranty = OrdModel["WARRANTY"].ToString();
                }
            }
            OrdModel.Close();


            _sql = "SELECT MI_LONGDESC, MI_MODEL, MI_BRAND, MI_CATE_1, MI_CATE_2, MI_CATE_3, MI_ITM_UOM, MI_IS_SER1, MI_WARR FROM MST_ITM WHERE (MI_CD = :p_itemcode)";
            _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            //_oCom.Transaction = oTrEMS;
            _oCom.Parameters.Add(":p_itemcode", OracleDbType.NVarchar2).Value = Str_ItemCode;
            OrdModel = _oCom.ExecuteReader();
            if (OrdModel.HasRows == true)
            {
                while (OrdModel.Read())
                {
                    functionReturnValue = true;
                    itm_Desc = OrdModel["MI_LONGDESC"].ToString();
                    itm_Model = OrdModel["MI_MODEL"].ToString();
                    itm_Brand = OrdModel["MI_BRAND"].ToString();
                    itm_Cate_1 = OrdModel["MI_CATE_1"].ToString();
                    itm_Cate_2 = OrdModel["MI_CATE_2"].ToString();
                    itm_Cate_3 = OrdModel["MI_CATE_3"].ToString();
                    itm_Uom = OrdModel["MI_ITM_UOM"].ToString();
                    itm_Serialize = OrdModel["MI_IS_SER1"].ToString();
                    if (itm_Serialize == "1" || itm_Serialize == "0")
                        itm_Serialize = "1";
                    else
                        itm_Serialize = "0";

                    itm_Warranty = OrdModel["MI_WARR"].ToString();
                    if (itm_Warranty == "1")
                        itm_Serialize = "1";
                    else
                        itm_Serialize = "0";
                }
            }
            OrdModel.Close();

            return functionReturnValue;
        }

        void Insert_Movement_Header(string Str_Year_Seq_No, string Str_Transaction_Loca, string Str_Doc_No, int Str_Year, string Str_Doc_Type, string Str_Other_Doc_No, DateTime Str_Doc_Date, string Str_Other_Loca, string Str_Entry_No,
   string Str_Doc_Status, string Str_App_By_1, string Str_App_By_2, string Str_App_By_3, string Str_User, string Str_Manual_Ref_No, string Str_Doc_Sub_Type, string Str_Entry_Type, string Str_Company_Code, string Str_Supplier_Code,
   string Str_Currency_Code, double Str_Exchange_Rate, string Str_Remarks, string Str_Vehicle_NO, string Str_Inv_Direction, string Str_Channel_Code, string Str_Cost_Profit_Code, string Str_Del_Add1, string Str_Del_Add2, DateTime TR_DATE)
        {
            int _ref = 0;
            string _sql = "";

            if (Str_Other_Doc_No == "") Str_Other_Doc_No = "N/A";

            _sql = " INSERT INTO INV_MOVEMENT_HEADER(YEAR_SEQ_NO,TRANSACTION_LOCATION,DOC_NO,DOC_YEAR,DOC_TYPE,OTHER_DOC_NO,DOC_DATE,OTHER_LOCATION,ENTRY_NO,STATUS,APPROVED_BY_1,APPROVED_BY_2,APPROVED_BY_3,CREATE_BY,CREATE_WHEN,LAST_MODIFY_BY,LAST_MODIFY_WHEN,MANUAL_REF_NO,AOD_SUB_TYPE,ENTRY_TYPE,COMPANY_CODE,SUPPLIER_CODE,CURRENCY_CODE,EXCHANGE_RATE,REMARKS,VEHICLENO,INV_DIRECTION,CHANNEL_CODE,COST_PROFIT,DEL_ADD1,DEL_ADD2) " +
                   " VALUES(:p_yearseqno, :p_location, :p_docno, :p_year, :p_doctype, :p_otherdocno, :p_docdate, :p_otherloca, :p_entryno, :p_docstatus, :p_Appby1, :p_Appby2, :p_Appby3, :p_user, CURRENT_DATE , :p_user, CURRENT_DATE , :p_manualrefno, :p_docsubtype, :p_entrytype, :p_company, :p_suppcode, :p_currencycode, :p_exchangerate, :p_remarks, :p_vehicleno, :p_invdirection, :p_channelcode, :p_costprofitcode, :p_deladd1, :p_deladd2)";
            OracleCommand _oCom = new OracleCommand(_sql, oConnection) { CommandType = CommandType.Text, BindByName = true };
            ////_oCom.Transaction = oTrSCM;
            _oCom.Parameters.Add(":p_yearseqno", OracleDbType.NVarchar2).Value = Str_Year_Seq_No;
            _oCom.Parameters.Add(":p_location", OracleDbType.NVarchar2).Value = Str_Transaction_Loca;
            _oCom.Parameters.Add(":p_docno", OracleDbType.NVarchar2).Value = Str_Doc_No;
            _oCom.Parameters.Add(":p_year", OracleDbType.NVarchar2).Value = Str_Year;
            _oCom.Parameters.Add(":p_doctype", OracleDbType.NVarchar2).Value = Str_Doc_Type;
            _oCom.Parameters.Add(":p_otherdocno", OracleDbType.NVarchar2).Value = Str_Other_Doc_No;
            _oCom.Parameters.Add(":p_docdate", OracleDbType.Date).Value = Str_Doc_Date.Date;
            _oCom.Parameters.Add(":p_otherloca", OracleDbType.NVarchar2).Value = Str_Other_Loca;
            _oCom.Parameters.Add(":p_entryno", OracleDbType.NVarchar2).Value = Str_Entry_No;
            _oCom.Parameters.Add(":p_docstatus", OracleDbType.NVarchar2).Value = Str_Doc_Status;
            _oCom.Parameters.Add(":p_Appby1", OracleDbType.NVarchar2).Value = Str_App_By_1;
            _oCom.Parameters.Add(":p_Appby2", OracleDbType.NVarchar2).Value = Str_App_By_2;
            _oCom.Parameters.Add(":p_Appby3", OracleDbType.NVarchar2).Value = Str_App_By_3;
            _oCom.Parameters.Add(":p_user", OracleDbType.NVarchar2).Value = Str_User;
            _oCom.Parameters.Add(":p_manualrefno", OracleDbType.NVarchar2).Value = Str_Manual_Ref_No;
            _oCom.Parameters.Add(":p_docsubtype", OracleDbType.NVarchar2).Value = Str_Doc_Sub_Type;
            _oCom.Parameters.Add(":p_entrytype", OracleDbType.NVarchar2).Value = Str_Entry_Type;
            _oCom.Parameters.Add(":p_company", OracleDbType.NVarchar2).Value = Str_Company_Code;
            _oCom.Parameters.Add(":p_suppcode", OracleDbType.NVarchar2).Value = Str_Supplier_Code;
            _oCom.Parameters.Add(":p_currencycode", OracleDbType.NVarchar2).Value = Str_Currency_Code;
            _oCom.Parameters.Add(":p_exchangerate", OracleDbType.Double).Value = Str_Exchange_Rate;
            _oCom.Parameters.Add(":p_remarks", OracleDbType.NVarchar2).Value = Str_Remarks;
            _oCom.Parameters.Add(":p_vehicleno", OracleDbType.NVarchar2).Value = Str_Vehicle_NO;
            _oCom.Parameters.Add(":p_invdirection", OracleDbType.NVarchar2).Value = Str_Inv_Direction;
            _oCom.Parameters.Add(":p_channelcode", OracleDbType.NVarchar2).Value = Str_Channel_Code;
            _oCom.Parameters.Add(":p_costprofitcode", OracleDbType.NVarchar2).Value = Str_Cost_Profit_Code;
            _oCom.Parameters.Add(":p_deladd1", OracleDbType.NVarchar2).Value = Str_Del_Add1;
            _oCom.Parameters.Add(":p_deladd2", OracleDbType.NVarchar2).Value = Str_Del_Add2;

            _ref = _oCom.ExecuteNonQuery();
        }

        public Int32 GetSerialID()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;
            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_getserialid", CommandType.StoredProcedure, param);

            return effects;
        }

        public DataTable FixedAsset(string _com, string _pc, DateTime _from, DateTime _to,string _user)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _from;
            (param[3] = new OracleParameter("p_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _to;
            (param[4] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "pkg_scm2_fix_asset.sp_get_fix_asset_scm2", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

    }
}
