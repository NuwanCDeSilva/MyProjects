using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.IO;
using FF.BusinessObjects;
using FF.BusinessObjects.ToursNew;

namespace FF.DataAccessLayer
{
    public class FMS_InventoryDAL : FMSDAL
    {

        //Serial Scan Common Control
        //Code By - Prabhath on 12/03/2012
        #region SerialScan

        public DataTable GetAllCompanyStatus(string _company)
        {

            //Query Data base.
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            ConnectionOpen();
            DataTable _dtItemStatus = QueryDataTable("tblCompanyItemStatus", "sp_scansearchstatus", CommandType.StoredProcedure, false, param);
            //ConnectionClose();

            return _dtItemStatus;
        }


        public DataTable GetAllLocationBin(string _company, string _location)
        {
            //Query Data base.
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            ConnectionOpen();
            DataTable _dtBin = QueryDataTable("tblLocationBin", "sp_scansearchbin", CommandType.StoredProcedure, false, param);
            //TODO: Put ths SP name
            //ConnectionClose();

            return _dtBin;
        }


        //Code By - Prabhath on 26/03/2012
        public Boolean IsItemSerialized_1(string _item)
        {
            //ConnectionOpen();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _isItemSerialized_1 = QueryDataTable("tblItem", "sp_checkserial1_isserialized", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            //ConnectionClose();
            return _isItemSerialized_1;
        }
        //Code By - Prabhath on 26/03/2012
        public Boolean IsItemSerialized_2(string _item)
        {
            //ConnectionOpen();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _isItemSerialized_2 = QueryDataTable("tblItem", "sp_checkserial2_isserialized", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            //ConnectionClose();
            return _isItemSerialized_2;
        }
        //Code By - Prabhath on 26/03/2012
        public Boolean IsItemSerialized_3(string _item)
        {
            //ConnectionOpen();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _isItemSerialized_3 = QueryDataTable("tblItem", "sp_checkserial3_isserialized", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            //ConnectionClose();
            return _isItemSerialized_3;
        }
        //Code By - Prabhath on 26/03/2012
        public Boolean IsItemHaveSubItem(string _item)
        {
            //ConnectionOpen();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _isItemHaveSubItem = QueryDataTable("tblItem", "SP_CHECKISSUBSERIALIZED", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            //ConnectionClose();
            return _isItemHaveSubItem;
        }
        //Code By - Prabhath on 27/03/2012
        public Boolean IsUOMDecimalAllow(string _item)
        {
            //ConnectionOpen();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            Boolean _isUOMDecimalAllow = QueryDataTable("tblUom", "sp_checkUOMdecimalallow", CommandType.StoredProcedure, false, _para).Rows.Count > 0 ? true : false;

            //ConnectionClose();
            return _isUOMDecimalAllow;
        }

        public List<InventorySerialRefN> GetItemDetailBySerial(string _company, string _location, string _serial)
        {
            List<InventorySerialRefN> _itemList = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _itemTable = QueryDataTable("tblserial", "sp_getItemBySerial", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _itemList = DataTableExtensions.ToGenericList<InventorySerialRefN>(_itemTable, InventorySerialRefN.Converter);
            }
            return _itemList;

        }

        #endregion

        //kapila
        public DataTable GetAvailableGvPrefixes(string _itm)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_search_gvprefix", CommandType.StoredProcedure, false, param);
       
            return _dtResults;
        }

        public DataTable GetManualDocBookNo(string _Comp, string _Loc, string _DocType, Int32 _NextNo, string _prefix)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param[2] = new OracleParameter("p_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NextNo;
            (param[3] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_MANUAL_BK_NO", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }
        public List<MasterItem> GetAllItems(string _company, string _item)
        {

            List<MasterItem> _itemList = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _itemTable = QueryDataTable("tblItem", "get_allitemdetails", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _itemList = DataTableExtensions.ToGenericList<MasterItem>(_itemTable, MasterItem.ConvertTotal);
            }
            return _itemList;

        }


        public MasterItem GetItem(string _company, string _item)
        {

            MasterItem _itemList = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _itemTable = QueryDataTable("tblItem", "get_allitemdetails", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _itemList = DataTableExtensions.ToGenericList<MasterItem>(_itemTable, MasterItem.ConvertTotal)[0];
            }
            return _itemList;

        }



        public List<MasterItemComponent> GetItemComponents(string _mainItemCode)
        {
            List<MasterItemComponent> _itemComponentList = null;
            MasterItemComponent _itemComponent = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_main_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mainItemCode;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _resultDT = QueryDataTable("tblItemComponent", "sp_get_mst_itm_components", CommandType.StoredProcedure, false, param);

            if (_resultDT.Rows.Count > 0)
            {
                _itemComponentList = new List<MasterItemComponent>();

                for (int i = 0; i < _resultDT.Rows.Count; i++)
                {
                    _itemComponent = new MasterItemComponent();
                    _itemComponent.ComponentItem = new MasterItem()
                    {
                        Mi_cd = ((_resultDT.Rows[i]["micp_comp_itm_cd"] == DBNull.Value) ? string.Empty : _resultDT.Rows[i]["micp_comp_itm_cd"].ToString()),
                        Mi_longdesc = ((_resultDT.Rows[i]["mi_longdesc"] == DBNull.Value) ? string.Empty : _resultDT.Rows[i]["mi_longdesc"].ToString()),
                        Mi_brand = ((_resultDT.Rows[i]["mi_brand"] == DBNull.Value) ? string.Empty : _resultDT.Rows[i]["mi_brand"].ToString()),
                        Mi_model = ((_resultDT.Rows[i]["mi_model"] == DBNull.Value) ? string.Empty : _resultDT.Rows[i]["mi_model"].ToString())
                    };

                    _itemComponent.Micp_itm_tp = ((_resultDT.Rows[i]["micp_itm_tp"] == DBNull.Value) ? string.Empty : _resultDT.Rows[i]["micp_itm_tp"].ToString());
                    _itemComponent.Micp_itm_cd = ((_resultDT.Rows[i]["micp_itm_cd"] == DBNull.Value) ? string.Empty : _resultDT.Rows[i]["micp_itm_cd"].ToString());
                    _itemComponent.Micp_cost_percentage = ((_resultDT.Rows[i]["micp_cost_percentage"] == DBNull.Value) ? 0 : Convert.ToDecimal(_resultDT.Rows[i]["micp_cost_percentage"]));
                    _itemComponent.Micp_qty = ((_resultDT.Rows[i]["micp_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(_resultDT.Rows[i]["micp_qty"]));

                    _itemComponent.Micp_isprice = ((_resultDT.Rows[i]["micp_isprice"] == DBNull.Value) ? false : Convert.ToBoolean(_resultDT.Rows[i]["micp_isprice"]));
                    _itemComponent.Micp_must_scan = ((_resultDT.Rows[i]["micp_must_scan"] == DBNull.Value) ? false : Convert.ToBoolean(_resultDT.Rows[i]["micp_must_scan"]));
                    _itemComponent.Micp_act = ((_resultDT.Rows[i]["micp_act"] == DBNull.Value) ? false : Convert.ToBoolean(_resultDT.Rows[i]["micp_act"]));

                    _itemComponentList.Add(_itemComponent);

                }

            }

            return _itemComponentList;
        }



        //Status change- Inventory
        //Code By - Shani on 23/4/2012
        #region
        public List<string> Get_all_Itemcodes()
        {

            //ConnectionOpen();
            DataTable dt = new DataTable("ItemCodes");
            ConnectionOpen();

            dt = QueryDataTable("tbl_ItemCodes", "sp_getall_itemcodes", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));
            //ConnectionClose();

            List<string> itemcodes = new List<string>();
            foreach (DataRow r in dt.Rows)
            {
                // Get the value of the wanted column and cast it to string
                string st = (string)r["MI_CD"];
                itemcodes.Add(st);

            }

            return itemcodes;
        }



        public List<string> GetAll_binCodes_for_loc(string company, string location)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl_bins", "sp_getall__bincodes_for_loc", CommandType.StoredProcedure, false, param);

            List<string> bin_list = new List<string>();
            bin_list.Add("--select--");
            foreach (DataRow r in _dtResults.Rows)
            {
                // Get the value of the wanted column and cast it to string
                string st = (string)r["INB_BIN"];
                bin_list.Add(st);

            }

            return bin_list;
        }


        //Code By - Shani on 24/4/2012
        public Dictionary<string, string> Get_all_ItemSatus()
        {

            //ConnectionOpen();
            DataTable dt = new DataTable("ItemCodes");
            ConnectionOpen();

            dt = QueryDataTable("tbl_ItemCodes", "sp_get_all_item_satus", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));
            //ConnectionClose();

            Dictionary<string, string> itemStatus_list = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                // Get the value of the wanted column and cast it to string
                string st_code = (string)r["MIS_CD"];
                string st_descript = (string)r["MIS_DESC"];
                itemStatus_list.Add(st_code, st_descript);

            }

            return itemStatus_list;
        }

        public DataTable getDetail_on_serial1(string _company, string _location, string _serial1)
        {
            //List<InventorySerial> _int_ser_List = new List<InventorySerial>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_ser1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial1;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = new DataTable();
            _itemTable = QueryDataTable("tblserial", "sp_get_detail_on_serial_no1", CommandType.StoredProcedure, false, param);

            return _itemTable;
        }


        //Code by- Shani on 25/4/2012

        public List<ReptPickSerials> Search_by_serial(string company, string location, string itemCode, string bin, string serial_1, string serial_2)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            (param[3] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bin;
            (param[4] = new OracleParameter("p_ser1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial_1;
            (param[5] = new OracleParameter("p_ser2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial_2;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            // (p_com in NVARCHAR2,p_loc in NVARCHAR2 ,p_item_code in NVARCHAR2,p_bin in NVARCHAR2,p_ser1 in NVARCHAR2,p_ser2 in NVARCHAR2,c_data OUT sys_refcursor)
            //query the INR_SER table
            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl_serial", "sp_search_by_serialNo", CommandType.StoredProcedure, false, param);

            List<ReptPickSerials> serial_list = new List<ReptPickSerials>();


            //if (dt.Rows.Count > 0)
            //{
            //    //Convert datatable to relavant generic List.
            //    serial_list = DataTableExtensions.ToGenericList<ReptPickSerials>(dt, ReptPickSerials.Converter);
            //}

            foreach (DataRow tr in dt.Rows)
            {
                ReptPickSerials _reptPickSerials = new ReptPickSerials();
                DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;

                _reptPickSerials.Tus_batch_line = Convert.ToInt32(tr["INS_BATCH_LINE"]);
                _reptPickSerials.Tus_bin = Convert.ToString(tr["INS_BIN"]);
                _reptPickSerials.Tus_com = Convert.ToString(tr["INS_COM"]);
                //    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();//not sure
                _reptPickSerials.Tus_cre_dt = _date;//not sure
                _reptPickSerials.Tus_doc_dt = Convert.ToDateTime(tr["INS_DOC_DT"]);
                _reptPickSerials.Tus_doc_no = Convert.ToString(tr["INS_DOC_NO"]);
                _reptPickSerials.Tus_exist_grncom = Convert.ToString(tr["INS_EXIST_GRNCOM"]);//not sure
                _reptPickSerials.Tus_exist_grndt = Convert.ToDateTime(tr["INS_EXIST_GRNDT"]);//not sure
                _reptPickSerials.Tus_exist_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_exist_supp = Convert.ToString(tr["INS_EXIST_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_itm_cd = Convert.ToString(tr["INS_ITM_CD"]);
                _reptPickSerials.Tus_itm_line = Convert.ToInt32(tr["INS_ITM_LINE"]);
                _reptPickSerials.Tus_itm_stus = Convert.ToString(tr["INS_ITM_STUS"]);//or ddl selected status?
                _reptPickSerials.Tus_loc = Convert.ToString(tr["INS_LOC"]);//not sure
                _reptPickSerials.Tus_orig_grncom = Convert.ToString(tr["INS_ORIG_GRNCOM"]);
                _reptPickSerials.Tus_orig_grndt = Convert.ToDateTime(tr["INS_ORIG_GRNDT"]);
                _reptPickSerials.Tus_orig_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_orig_supp = Convert.ToString(tr["INS_ORIG_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_qty = 1;// Convert.ToDecimal(txtQty.Text.Trim());//not sure
                _reptPickSerials.Tus_seq_no = Convert.ToInt32(tr["INS_SEQ_NO"]);
                _reptPickSerials.Tus_ser_1 = Convert.ToString(tr["INS_SER_1"]);
                _reptPickSerials.Tus_ser_2 = Convert.ToString(tr["INS_SER_2"]);
                _reptPickSerials.Tus_ser_3 = Convert.ToString(tr["INS_SER_3"]);
                _reptPickSerials.Tus_ser_4 = Convert.ToString(tr["INS_SER_4"]);
                _reptPickSerials.Tus_ser_id = Convert.ToInt32(tr["INS_SER_ID"]);
                _reptPickSerials.Tus_ser_line = Convert.ToInt32(tr["INS_SER_LINE"]);
                // _reptPickSerials.Tus_session_id = GlbUserSessionID;//not sure
                _reptPickSerials.Tus_unit_cost = Convert.ToDecimal(tr["INS_UNIT_COST"]);
                _reptPickSerials.Tus_unit_price = Convert.ToDecimal(tr["INS_UNIT_PRICE"]);
                //   _reptPickSerials.Tus_usrseq_no = (int)generated_seq;//not sure
                _reptPickSerials.Tus_warr_no = Convert.ToString(tr["INS_WARR_NO"]);
                _reptPickSerials.Tus_warr_period = Convert.ToInt32(tr["INS_WARR_PERIOD"]);

                serial_list.Add(_reptPickSerials);

            }
            return serial_list;
        }


        public List<ReptPickSerials> Get_all_serials_for_itemCD(string company, string location, string binCode, string itemCode)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = binCode;
            (param[3] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            //query INR_SER
            dt = QueryDataTable("tbl_serials", "sp_get_serials_for_itemCD", CommandType.StoredProcedure, false, param);
            List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
            foreach (DataRow tr in dt.Rows)
            {
                ReptPickSerials _reptPickSerials = new ReptPickSerials();
                DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;

                _reptPickSerials.Tus_batch_line = Convert.ToInt32(tr["INS_BATCH_LINE"]);
                _reptPickSerials.Tus_bin = Convert.ToString(tr["INS_BIN"]);
                _reptPickSerials.Tus_com = Convert.ToString(tr["INS_COM"]);
                //    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();//not sure
                _reptPickSerials.Tus_cre_dt = _date;//not sure
                _reptPickSerials.Tus_doc_dt = (DateTime)tr["INS_DOC_DT"];
                _reptPickSerials.Tus_doc_no = Convert.ToString(tr["INS_DOC_NO"]);
                _reptPickSerials.Tus_exist_grncom = Convert.ToString(tr["INS_EXIST_GRNCOM"]);//not sure
                _reptPickSerials.Tus_exist_grndt = (DateTime)tr["INS_EXIST_GRNDT"];//not sure
                _reptPickSerials.Tus_exist_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_exist_supp = Convert.ToString(tr["INS_EXIST_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_itm_cd = Convert.ToString(tr["INS_ITM_CD"]);
                _reptPickSerials.Tus_itm_line = Convert.ToInt32(tr["INS_ITM_LINE"]);
                _reptPickSerials.Tus_itm_stus = Convert.ToString(tr["INS_ITM_STUS"]);//or ddl selected status?
                _reptPickSerials.Tus_loc = Convert.ToString(tr["INS_LOC"]);//not sure
                _reptPickSerials.Tus_orig_grncom = Convert.ToString(tr["INS_ORIG_GRNCOM"]);
                _reptPickSerials.Tus_orig_grndt = (DateTime)tr["INS_ORIG_GRNDT"];
                _reptPickSerials.Tus_orig_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_orig_supp = Convert.ToString(tr["INS_ORIG_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_qty = 1;// Convert.ToDecimal(txtQty.Text.Trim());//not sure
                _reptPickSerials.Tus_seq_no = Convert.ToInt32(tr["INS_SEQ_NO"]);
                _reptPickSerials.Tus_ser_1 = Convert.ToString(tr["INS_SER_1"]);
                _reptPickSerials.Tus_ser_2 = Convert.ToString(tr["INS_SER_2"]);
                _reptPickSerials.Tus_ser_3 = Convert.ToString(tr["INS_SER_3"]);
                _reptPickSerials.Tus_ser_4 = Convert.ToString(tr["INS_SER_4"]);
                _reptPickSerials.Tus_ser_id = Convert.ToInt32(tr["INS_SER_ID"]);
                _reptPickSerials.Tus_ser_line = Convert.ToInt32(tr["INS_SER_LINE"]);
                // _reptPickSerials.Tus_session_id = GlbUserSessionID;//not sure
                _reptPickSerials.Tus_unit_cost = (Decimal)tr["INS_UNIT_COST"];
                _reptPickSerials.Tus_unit_price = (Decimal)tr["INS_UNIT_PRICE"];
                //  _reptPickSerials.Tus_usrseq_no = (int)generated_seq;//not sure
                _reptPickSerials.Tus_warr_no = Convert.ToString(tr["INS_WARR_NO"]);
                _reptPickSerials.Tus_warr_period = Convert.ToInt32(tr["INS_WARR_PERIOD"]);

                serial_list.Add(_reptPickSerials);

            }
            return serial_list;

        }
        //11-5-2012 Consignment
        // to search item by supplier and serial 1 or serial 2
        public DataTable getDetail_on_serial_Supplier(string _company, string _location, string supplierCD, string serial_1, string serial_2)
        {

            //List<InventorySerial> _int_ser_List = new List<InventorySerial>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_supplierCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supplierCD;
            (param[3] = new OracleParameter("p_ser1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial_1;
            (param[4] = new OracleParameter("p_ser2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial_2;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = new DataTable();
            // get from INR_SER
            _itemTable = QueryDataTable("tblserial", "sp_GETdetail_on_ser_Supplier", CommandType.StoredProcedure, false, param);

            return _itemTable;
        }

        // on 9-5-2012
        public Boolean Update_sat_itm_DO_qty(string invoiceNo, Int32 item_Line, int DO_qty)
        {
            Int32 effects = 0;

            //try
            //{
            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("p_invoiceNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoiceNo;
            (param[1] = new OracleParameter("p_itemline", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item_Line;

            (param[2] = new OracleParameter("p_do_qty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = DO_qty;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            //ConnectionOpen();
            effects = UpdateRecords("sp_update_sat_itm_do_qty", CommandType.StoredProcedure, param);

            return effects >= 1 ? true : false;
        }
        //on 8-5-2012
        public List<ReptPickSerials> Search_serials_for_itemCD(string company, string location, string itemCode)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;

            (param[2] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            //query INR_SER
            dt = QueryDataTable("tbl_serials", "sp_search_serials_for_itemCD", CommandType.StoredProcedure, false, param);
            List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
            foreach (DataRow tr in dt.Rows)
            {
                ReptPickSerials _reptPickSerials = new ReptPickSerials();
                DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;

                _reptPickSerials.Tus_batch_line = Convert.ToInt32(tr["INS_BATCH_LINE"]);
                _reptPickSerials.Tus_bin = Convert.ToString(tr["INS_BIN"]);
                _reptPickSerials.Tus_com = Convert.ToString(tr["INS_COM"]);
                //    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();//not sure
                _reptPickSerials.Tus_cre_dt = _date;//not sure
                _reptPickSerials.Tus_doc_dt = Convert.ToDateTime(tr["INS_DOC_DT"]);
                _reptPickSerials.Tus_doc_no = Convert.ToString(tr["INS_DOC_NO"]);
                _reptPickSerials.Tus_exist_grncom = Convert.ToString(tr["INS_EXIST_GRNCOM"]);//not sure
                _reptPickSerials.Tus_exist_grndt = Convert.ToDateTime(tr["INS_EXIST_GRNDT"]);//not sure
                _reptPickSerials.Tus_exist_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_exist_supp = Convert.ToString(tr["INS_EXIST_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_itm_cd = Convert.ToString(tr["INS_ITM_CD"]);
                _reptPickSerials.Tus_itm_line = Convert.ToInt32(tr["INS_ITM_LINE"]);
                _reptPickSerials.Tus_itm_stus = Convert.ToString(tr["INS_ITM_STUS"]);//or ddl selected status?
                _reptPickSerials.Tus_loc = Convert.ToString(tr["INS_LOC"]);//not sure
                _reptPickSerials.Tus_orig_grncom = Convert.ToString(tr["INS_ORIG_GRNCOM"]);//not sure
                _reptPickSerials.Tus_orig_grndt = Convert.ToDateTime(tr["INS_ORIG_GRNDT"]);//not sure
                _reptPickSerials.Tus_orig_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);//not sure
                _reptPickSerials.Tus_orig_supp = Convert.ToString(tr["INS_ORIG_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_qty = 1;// Convert.ToDecimal(txtQty.Text.Trim());//not sure
                _reptPickSerials.Tus_seq_no = Convert.ToInt32(tr["INS_SEQ_NO"]);
                _reptPickSerials.Tus_ser_1 = Convert.ToString(tr["INS_SER_1"]);
                _reptPickSerials.Tus_ser_2 = Convert.ToString(tr["INS_SER_2"]);
                _reptPickSerials.Tus_ser_3 = Convert.ToString(tr["INS_SER_3"]);
                _reptPickSerials.Tus_ser_4 = Convert.ToString(tr["INS_SER_4"]);
                _reptPickSerials.Tus_ser_id = Convert.ToInt32(tr["INS_SER_ID"]);
                _reptPickSerials.Tus_ser_line = Convert.ToInt32(tr["INS_SER_LINE"]);
                // _reptPickSerials.Tus_session_id = GlbUserSessionID;//not sure
                _reptPickSerials.Tus_unit_cost = Convert.ToDecimal(tr["INS_UNIT_COST"]);
                _reptPickSerials.Tus_unit_price = Convert.ToDecimal(tr["INS_UNIT_PRICE"]);
                //   _reptPickSerials.Tus_usrseq_no = (int)generated_seq;//not sure
                _reptPickSerials.Tus_warr_no = Convert.ToString(tr["INS_WARR_NO"]);
                _reptPickSerials.Tus_warr_period = Convert.ToInt32(tr["INS_WARR_PERIOD"]);

                serial_list.Add(_reptPickSerials);

            }
            return serial_list;

        }
        //10-5-2012 consigment return note
        public List<ReptPickSerials> GET_ser_for_ItmCD_Supplier(string company, string location, string binCode, string itemCode, string supplier)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = binCode;
            (param[3] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            (param[4] = new OracleParameter("p_supplierCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = supplier;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable dt = new DataTable();
            //query INR_SER
            dt = QueryDataTable("tbl_serials", "sp_GET_ser_for_ItmCD_Supplier", CommandType.StoredProcedure, false, param);
            List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
            foreach (DataRow tr in dt.Rows)
            {
                ReptPickSerials _reptPickSerials = new ReptPickSerials();
                DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;

                _reptPickSerials.Tus_batch_line = Convert.ToInt32(tr["INS_BATCH_LINE"]);
                _reptPickSerials.Tus_bin = Convert.ToString(tr["INS_BIN"]);
                _reptPickSerials.Tus_com = Convert.ToString(tr["INS_COM"]);
                //    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();//not sure
                _reptPickSerials.Tus_cre_dt = _date;//not sure
                _reptPickSerials.Tus_doc_dt = Convert.ToDateTime(tr["INS_DOC_DT"]);
                _reptPickSerials.Tus_doc_no = Convert.ToString(tr["INS_DOC_NO"]);
                _reptPickSerials.Tus_exist_grncom = Convert.ToString(tr["INS_EXIST_GRNCOM"]);//not sure
                _reptPickSerials.Tus_exist_grndt = Convert.ToDateTime(tr["INS_EXIST_GRNDT"]);//not sure
                _reptPickSerials.Tus_exist_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_exist_supp = Convert.ToString(tr["INS_EXIST_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_itm_cd = Convert.ToString(tr["INS_ITM_CD"]);
                _reptPickSerials.Tus_itm_line = Convert.ToInt32(tr["INS_ITM_LINE"]);
                _reptPickSerials.Tus_itm_stus = Convert.ToString(tr["INS_ITM_STUS"]);//or ddl selected status?
                _reptPickSerials.Tus_loc = Convert.ToString(tr["INS_LOC"]);//not sure
                _reptPickSerials.Tus_orig_grncom = Convert.ToString(tr["INS_ORIG_GRNCOM"]);
                _reptPickSerials.Tus_orig_grndt = Convert.ToDateTime(tr["INS_ORIG_GRNDT"]);
                _reptPickSerials.Tus_orig_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_orig_supp = Convert.ToString(tr["INS_ORIG_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_qty = 1;// Convert.ToDecimal(txtQty.Text.Trim());//not sure
                _reptPickSerials.Tus_seq_no = Convert.ToInt32(tr["INS_SEQ_NO"]);
                _reptPickSerials.Tus_ser_1 = Convert.ToString(tr["INS_SER_1"]);
                _reptPickSerials.Tus_ser_2 = Convert.ToString(tr["INS_SER_2"]);
                _reptPickSerials.Tus_ser_3 = Convert.ToString(tr["INS_SER_3"]);
                _reptPickSerials.Tus_ser_4 = Convert.ToString(tr["INS_SER_4"]);
                _reptPickSerials.Tus_ser_id = Convert.ToInt32(tr["INS_SER_ID"]);
                _reptPickSerials.Tus_ser_line = Convert.ToInt32(tr["INS_SER_LINE"]);
                // _reptPickSerials.Tus_session_id = GlbUserSessionID;//not sure
                _reptPickSerials.Tus_unit_cost = Convert.ToDecimal(tr["INS_UNIT_COST"]);
                _reptPickSerials.Tus_unit_price = Convert.ToDecimal(tr["INS_UNIT_PRICE"]);
                //   _reptPickSerials.Tus_usrseq_no = (int)generated_seq;//not sure
                _reptPickSerials.Tus_warr_no = Convert.ToString(tr["INS_WARR_NO"]);
                _reptPickSerials.Tus_warr_period = Convert.ToInt32(tr["INS_WARR_PERIOD"]);

                serial_list.Add(_reptPickSerials);

            }
            return serial_list;

        }

        //2-5-2012
        public ReptPickSerials Get_all_details_on_serialID(string company, string location, string bin, string itemCode, int serial_ID)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bin;
            (param[3] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            (param[4] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = serial_ID;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //query the INR_SER table
            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl_serial", "sp_get_detail_on_serial_ID", CommandType.StoredProcedure, false, param);

            ReptPickSerials _reptPickSerials = new ReptPickSerials();
            foreach (DataRow tr in dt.Rows)
            {
                DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;

                _reptPickSerials.Tus_batch_line = Convert.ToInt32(tr["INS_BATCH_LINE"]);
                _reptPickSerials.Tus_bin = Convert.ToString(tr["INS_BIN"]);
                _reptPickSerials.Tus_com = Convert.ToString(tr["INS_COM"]);
                //    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();//not sure
                _reptPickSerials.Tus_cre_dt = _date;//not sure
                _reptPickSerials.Tus_doc_dt = Convert.ToDateTime(tr["INS_DOC_DT"]);
                _reptPickSerials.Tus_doc_no = Convert.ToString(tr["INS_DOC_NO"]);
                _reptPickSerials.Tus_exist_grncom = Convert.ToString(tr["INS_EXIST_GRNCOM"]);//not sure
                //    _reptPickSerials.Tus_exist_grndt = Convert.ToDecimal(tr["INS_EXIST_GRNDT"]);//not sure
                _reptPickSerials.Tus_exist_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_exist_supp = Convert.ToString(tr["INS_EXIST_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_itm_cd = Convert.ToString(tr["INS_ITM_CD"]);
                _reptPickSerials.Tus_itm_line = Convert.ToInt32(tr["INS_ITM_LINE"]);
                _reptPickSerials.Tus_itm_stus = Convert.ToString(tr["INS_ITM_STUS"]);//or ddl selected status?
                _reptPickSerials.Tus_loc = Convert.ToString(tr["INS_LOC"]);//not sure
                _reptPickSerials.Tus_orig_grncom = Convert.ToString(tr["INS_ORIG_GRNCOM"]);
                _reptPickSerials.Tus_orig_grndt = Convert.ToDateTime(tr["INS_ORIG_GRNDT"]);
                _reptPickSerials.Tus_orig_grnno = Convert.ToString(tr["INS_EXIST_GRNNO"]);
                _reptPickSerials.Tus_orig_supp = Convert.ToString(tr["INS_ORIG_SUPP"]);//TODO : GET SUPPLIER
                _reptPickSerials.Tus_qty = 1;// Convert.ToDecimal(txtQty.Text.Trim());//not sure
                _reptPickSerials.Tus_seq_no = Convert.ToInt32(tr["INS_SEQ_NO"]);
                _reptPickSerials.Tus_ser_1 = Convert.ToString(tr["INS_SER_1"]);
                _reptPickSerials.Tus_ser_2 = Convert.ToString(tr["INS_SER_2"]);
                _reptPickSerials.Tus_ser_3 = Convert.ToString(tr["INS_SER_3"]);
                _reptPickSerials.Tus_ser_4 = Convert.ToString(tr["INS_SER_4"]);
                _reptPickSerials.Tus_ser_id = Convert.ToInt32(tr["INS_SER_ID"]);
                _reptPickSerials.Tus_ser_line = Convert.ToInt32(tr["INS_SER_LINE"]);
                // _reptPickSerials.Tus_session_id = GlbUserSessionID;//not sure
                _reptPickSerials.Tus_unit_cost = Convert.ToDecimal(tr["INS_UNIT_COST"]);
                _reptPickSerials.Tus_unit_price = Convert.ToDecimal(tr["INS_UNIT_PRICE"]);
                //   _reptPickSerials.Tus_usrseq_no = (int)generated_seq;//not sure
                _reptPickSerials.Tus_warr_no = Convert.ToString(tr["INS_WARR_NO"]);
                _reptPickSerials.Tus_warr_period = Convert.ToInt32(tr["INS_WARR_PERIOD"]);



            }
            return _reptPickSerials;
        }
        public ReptPickSerials Get_all_details_on_serial(string company, string location, string bin, string itemCode, string serial_1)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bin;
            (param[3] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            (param[4] = new OracleParameter("p_ser1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial_1;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //query the INR_SER table
            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl_serial", "sp_get_serial_detail", CommandType.StoredProcedure, false, param);

            ReptPickSerials _reptPickSerials = new ReptPickSerials();
            foreach (DataRow tr in dt.Rows)
            {


                //changed by prabhath
                _reptPickSerials = SetReptPickSerials(tr);

            }
            return _reptPickSerials;
        }
        //on 27/4/2012
        //sp_get_location_by_code   (p_com_code in string,p_loc_code in string,c_data OUT sys_refcursor)
        public DataTable Get_location_by_code(string com_code, string location)
        {

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com_code;
            (param[1] = new OracleParameter("p_loc_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl_location", "sp_get_location_by_code", CommandType.StoredProcedure, false, param);

            return dt;
        }


        // on 27/4/2012
        public string Get_item_description(string itemCode)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_item_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable dt = new DataTable();
            dt = QueryDataTable("tbl_itm_desc", "sp_MST_ITM_Descript", CommandType.StoredProcedure, false, param);
            string des = "";
            foreach (DataRow tr in dt.Rows)
            {

                des = (string)tr["MI_SHORTDESC"];
            }
            return des;
        }
        //on 2-5-2012
        //this method is for non-serial items
        public Boolean Update_serialID_INS_AVAILABLE(string compny, string location, string itemCD, Int32 ser_ID, int availability)
        {
            Int32 effects = 0;

            //try
            //{
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = compny;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCD;
            (param[3] = new OracleParameter("p_ser_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ser_ID;
            (param[4] = new OracleParameter("availability", OracleDbType.Int32, null, ParameterDirection.Input)).Value = availability;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            //ConnectionOpen();
            effects = UpdateRecords("sp_update_ser_id_INS_AVAILABLE", CommandType.StoredProcedure, param);

            return effects >= 1 ? true : false;
        }

        //on 26/4/2012

        public Boolean Update_inrser_INS_AVAILABLE(string compny, string location, string itemCD, string ser_1, int availability)
        {
            Int32 effects = 0;

            //try
            //{
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = compny;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCD;
            (param[3] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ser_1;
            (param[4] = new OracleParameter("availability", OracleDbType.Int32, null, ParameterDirection.Input)).Value = availability;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            //ConnectionOpen();
            effects = UpdateRecords("sp_update_inrser_INS_AVAILABLE", CommandType.StoredProcedure, param);

            return effects >= 1 ? true : false;
        }
        #endregion

        //Stock Adjustment- Inventory
        //Code By - Shani on 5/4/2012
        #region
        public List<string> GetAll_Adj_SubTypes()
        {

            //ConnectionOpen();
            DataTable dt = new DataTable("Adj_sub_items");
            //ConnectionOpen();

            dt = QueryDataTable("tblAdj_sub_items", "sp_get_adj_sub_items", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));
            //ConnectionClose();

            List<string> L = new List<string>();
            foreach (DataRow r in dt.Rows)
            {
                // Get the value of the wanted column and cast it to string
                string st = (string)r["MMCT_SCD"];
                // string st = (string)r["MMCT_SDESC"];
                L.Add(st);

            }

            return L;
        }
        public string Get_Adj_SubTypes_description(string subtypeCd)
        {

            DataTable dt = new DataTable("Adj_sub_items");

            //OracleParameter[] param = new OracleParameter[2];
            //(param[0] = new OracleParameter("subtype_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = subtypeCd;
            //param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            dt = QueryDataTable("tblAdj_sub_items", "sp_get_adj_sub_items", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));

            //  dt = QueryDataTable("tblAdj_sub_items", "sp_get_adj_sub_items", CommandType.StoredProcedure, false, param);
            //ConnectionClose();

            List<string> L = new List<string>();
            string desc = "";
            foreach (DataRow r in dt.Rows)
            {
                // Get the value of the wanted column and cast it to string
                string code = Convert.ToString(r["MMCT_SCD"]);
                desc = Convert.ToString(r["MMCT_SDESC"]);
                if (code == subtypeCd)
                {
                    return desc;
                }


                // L.Add(st);

            }

            // return L;
            return desc;
        }


        public int Generate_new_seq_num(string usrID, string doc_type, int direction_, string company_)
        {
            //ConnectionOpen();
            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("usrid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = usrID;
            (param[1] = new OracleParameter("doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc_type;
            (param[2] = new OracleParameter("direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction_;
            (param[3] = new OracleParameter("company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company_;
            //  param[4] = new OracleParameter("seq_number", OracleDbType.Int32, null, ParameterDirection.Output);
            OracleParameter outParameter = new OracleParameter("seq_number", OracleDbType.Int32, null, ParameterDirection.Output);
            //param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            // ReturnSP_SingleValue(String _storedProcedure, CommandType _commTypes,OracleParameter _outPara, params OracleParameter[] _parameters)
            int seq = ReturnSP_SingleValue("SP_SEQ_NUM_GENERATOR", CommandType.StoredProcedure, outParameter, param);
            //ConnectionClose();
            return seq;


        }
        //public Int32 SaveSeq_to_TempPickHDR()
        //{ 

        //}
        #endregion

        #region InventoryRequest(MRN)

        //Miginda - 31/03/2012
        public List<InventoryRequest> GetAllInventoryRequestData(InventoryRequest _inventoryRequest)
        {
            List<InventoryRequest> _inventoryRequestList = null;

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_loc;
            (param[2] = new OracleParameter("p_fromDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.FromDate;
            (param[3] = new OracleParameter("p_toDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.ToDate;
            (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_stus;
            (param[5] = new OracleParameter("p_createdBy", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_cre_by;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblAllInventoryRequestData", "sp_getallinventoryrequestdata", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _inventoryRequestList = DataTableExtensions.ToGenericList<InventoryRequest>(_dtResults, InventoryRequest.ConverterTotal);
            }

            return _inventoryRequestList;
        }

        //Miginda - 02/04/2012
        public InventoryRequest GetInventoryRequestDataByReqNo(InventoryRequest _inputInventoryRequest)
        {
            InventoryRequest _inventoryRequest = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inputInventoryRequest.Itr_req_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInventoryRequestData", "sp_getinventoryrequestdata", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _inventoryRequest = DataTableExtensions.ToGenericList<InventoryRequest>(_dtResults, InventoryRequest.Converter)[0];
            }

            return _inventoryRequest;
        }

        //Miginda - 02/04/2012
        public List<InventoryRequestItem> GetInventoryRequestItemDataByReqNo(InventoryRequest _inputInventoryRequest)
        {
            List<InventoryRequestItem> _inventoryRequestItemList = null;
            InventoryRequestItem _inventoryRequestItem = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inputInventoryRequest.Itr_req_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInventoryRequestItemData", "sp_getinventoryrequestitemdata", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _inventoryRequestItemList = new List<InventoryRequestItem>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _inventoryRequestItem = new InventoryRequestItem();
                    _inventoryRequestItem.Itri_seq_no = (_dtResults.Rows[i]["Itri_seq_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Itri_seq_no"]);
                    _inventoryRequestItem.Itri_line_no = (_dtResults.Rows[i]["Itri_line_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["Itri_line_no"]);
                    _inventoryRequestItem.Itri_itm_stus = (_dtResults.Rows[i]["Itri_itm_stus"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Itri_itm_stus"].ToString();
                    _inventoryRequestItem.Itri_qty = (_dtResults.Rows[i]["Itri_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Itri_qty"]);
                    _inventoryRequestItem.Itri_unit_price = (_dtResults.Rows[i]["Itri_unit_price"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Itri_unit_price"]);
                    _inventoryRequestItem.Itri_app_qty = (_dtResults.Rows[i]["Itri_app_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["Itri_app_qty"]);
                    _inventoryRequestItem.Itri_res_no = (_dtResults.Rows[i]["Itri_res_no"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Itri_res_no"].ToString();
                    _inventoryRequestItem.Itri_note = (_dtResults.Rows[i]["Itri_note"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["Itri_note"].ToString();
                    _inventoryRequestItem.Itri_mitm_cd = (_dtResults.Rows[i]["itri_mitm_cd"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["itri_mitm_cd"].ToString();
                    _inventoryRequestItem.Itri_mitm_stus = (_dtResults.Rows[i]["itri_mitm_stus"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["itri_mitm_stus"].ToString();
                    _inventoryRequestItem.Itri_mqty = (_dtResults.Rows[i]["itri_mqty"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["itri_mqty"]);

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

        //Miginda - 02/04/2012
        public int SaveInventoryRequestItem(InventoryRequestItem _inventoryRequestItem)
        {
            int rows_affected = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                (param[0] = new OracleParameter("p_itri_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_seq_no;
                (param[1] = new OracleParameter("p_itri_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_line_no;
                (param[2] = new OracleParameter("p_itri_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.MasterItem.Mi_cd;
                (param[3] = new OracleParameter("p_itri_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_itm_stus;
                (param[4] = new OracleParameter("p_itri_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_qty;
                (param[5] = new OracleParameter("p_itri_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_unit_price;
                (param[6] = new OracleParameter("p_itri_app_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_app_qty;
                (param[7] = new OracleParameter("p_itri_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_res_no;
                (param[8] = new OracleParameter("p_itri_note", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_note;
                (param[9] = new OracleParameter("p_itri_mitm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_mitm_cd;
                (param[10] = new OracleParameter("p_itri_mitm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_mitm_stus;
                (param[11] = new OracleParameter("p_itri_mqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequestItem.Itri_mqty;
                param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                //Open the connection and call the stored procedure.
                //ConnectionOpen();
                rows_affected = UpdateRecords("sp_updateinventoryrequestitem", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                //ConnectionClose();
            }
            return rows_affected;

        }

        //Miginda - 02/04/2012
        public int SaveInventoryRequest(InventoryRequest _inventoryRequest)
        {
            int seqno = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[25];
                (param[0] = new OracleParameter("p_itr_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_req_no;
                (param[1] = new OracleParameter("p_itr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_com;
                (param[2] = new OracleParameter("p_itr_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_tp;
                (param[3] = new OracleParameter("p_itr_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_sub_tp;
                (param[4] = new OracleParameter("p_itr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_loc;
                (param[5] = new OracleParameter("p_itr_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_ref;
                (param[6] = new OracleParameter("p_itr_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_dt;
                (param[7] = new OracleParameter("p_itr_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_exp_dt;
                (param[8] = new OracleParameter("p_itr_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_stus;
                (param[9] = new OracleParameter("p_itr_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_job_no;
                (param[10] = new OracleParameter("p_itr_bus_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_bus_code;
                (param[11] = new OracleParameter("p_itr_note", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_note;
                (param[12] = new OracleParameter("p_itr_issue_from", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_issue_from;
                (param[13] = new OracleParameter("p_itr_rec_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_rec_to;
                (param[14] = new OracleParameter("p_itr_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_direct;
                (param[15] = new OracleParameter("p_itr_country_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_country_cd;
                (param[16] = new OracleParameter("p_itr_town_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_town_cd;
                (param[17] = new OracleParameter("p_itr_cur_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_cur_code;
                (param[18] = new OracleParameter("p_itr_exg_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_exg_rate;
                (param[19] = new OracleParameter("p_itr_collector_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_collector_id;
                (param[20] = new OracleParameter("p_itr_collector_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_collector_name;
                (param[21] = new OracleParameter("p_itr_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_act;
                (param[22] = new OracleParameter("p_itr_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_cre_by;
                (param[23] = new OracleParameter("p_itr_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_session_id;
                param[24] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                //Open the connection and call the stored procedure.
                //ConnectionOpen();
                seqno = UpdateRecords("sp_updateinventoryrequest", CommandType.StoredProcedure, param);
                _inventoryRequest.Itr_seq_no = seqno;

            }
            catch (Exception)
            {
                seqno = -1;
            }
            finally
            {
                //ConnectionClose();
            }
            return seqno;

        }

        //Miginda - 18/05/2012
        public int UpdateInventoryRequestStatus(InventoryRequest _inventoryRequest)
        {
            int rows_effected = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_com;
                (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_loc;
                (param[2] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_req_no;
                (param[3] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_stus;
                (param[4] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_mod_by;
                (param[5] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_session_id;
                param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                rows_effected = UpdateRecords("sp_update_int_req_status", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_effected = -1;
            }
            finally
            {
                //ConnectionClose();
            }
            return rows_effected;
        }


        //Code By Miginda on 19/05/2012
        public int DeleteInventoryRequestItemData(InventoryRequest _inventoryRequest)
        {
            //ConnectionOpen();
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_loc;
            (param[2] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_req_no;
            OracleParameter outParameter = new OracleParameter("hdr_seq_no", OracleDbType.Int32, null, ParameterDirection.Output);
            int hdr_seq_no = ReturnSP_SingleValue("sp_delete_inv_req_data", CommandType.StoredProcedure, outParameter, param);
            //ConnectionClose();
            return hdr_seq_no;
        }


        #endregion

        //Written By P.Wijetunge on 9/4/2012
        public MasterAutoNumber GetAutoNumber(string _module, Int32? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year)
        {
            OracleParameter[] param = new OracleParameter[8];
            MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _startChar;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _catType;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _catCode;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _modifyDate;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _year;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAuto", "sp_autonumber", CommandType.StoredProcedure, false, param);
            //   p_module IN NVARCHAR2,p_direction IN NUMBER,p_startchar IN NVARCHAR2,p_cattype in NVARCHAR2,p_catcode in NVARCHAR2,p_modifydate in DATE,p_year in NUMBER ,c_data out SYS_REFCURSOR
            if (_dtResults.Rows.Count > 0)
            {
                _masterAutoNumber = DataTableExtensions.ToGenericList<MasterAutoNumber>(_dtResults, MasterAutoNumber.ConvertTotal)[0];
            }
            else if (_dtResults.Rows.Count == 0)
            {
                //_masterAutoNumber.Aut_number = 999;//since the auto number not generated, it is hard corded.

                _masterAutoNumber.Aut_cate_cd = _catCode;
                _masterAutoNumber.Aut_cate_tp = _catType;
                _masterAutoNumber.Aut_direction = _direction;
                _masterAutoNumber.Aut_modify_dt = _modifyDate;
                _masterAutoNumber.Aut_moduleid = _module;
                _masterAutoNumber.Aut_number = 1;
                _masterAutoNumber.Aut_start_char = _startChar;
                _masterAutoNumber.Aut_year = _year;
            }



            return _masterAutoNumber;
        }
        //Written By P.Wijetunge on 9/4/2012
        public Int16 UpdateAutoNumber(MasterAutoNumber _masterAutoNumber)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[8];

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_moduleid;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_start_char;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_tp;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_cd;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_modify_dt;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_year;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateRecords("sp_updateautonumber", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        //Written By Chamal De Silva on 15/05/2012
        public Int16 UpdateOtherDocuments(string _docNo, string _othDocNo)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            (param[1] = new OracleParameter("p_otherdocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _othDocNo;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateRecords("sp_updateotherdocno", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }


        //Written By P.Wijetunge on 10/4/2012
        public DataTable GetAllPurchaseOrder(string _company, string _adminTeam, string _status)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_ope", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _adminTeam;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblPo", "sp_getpurchaseorder", CommandType.StoredProcedure, false, param);

        }
        //Written By P.Wijetunge on 10/4/2012
        public DataTable GetAllPurchaseOrderDetail(string _company, string _adminTeam, string _poNo)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_ope", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _adminTeam;
            (param[2] = new OracleParameter("p_pono", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poNo;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tblPo", "sp_getpurchaseorderdetail", CommandType.StoredProcedure, false, param);
        }

        public List<PurchaseOrderDetail> GetAllPurchaseOrderDetailList(string _company, string _adminTeam, string _poNo)
        {
            OracleParameter[] param = new OracleParameter[4];
            List<PurchaseOrderDetail> _itemList = null;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_ope", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _adminTeam;
            (param[2] = new OracleParameter("p_pono", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _poNo;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tbl = QueryDataTable("tblPo", "sp_getpurchaseorderdetail", CommandType.StoredProcedure, false, param);
            if (_tbl.Rows.Count > 0)
            {
                _itemList = DataTableExtensions.ToGenericList<PurchaseOrderDetail>(_tbl, PurchaseOrderDetail.ConvertDetails);
            }
            return _itemList;


        }


        #region Inventory IN Process

        //Written By P.Wijetunge on 02/04/2012
        public Int32 SaveInvnetoryHeader(InventoryHeader _header)
        {
            OracleParameter[] param = new OracleParameter[58];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_acc_no;
            (param[1] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_anal_1;
            (param[2] = new OracleParameter("p_anal_10", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _header.Ith_anal_10 == true ? 1 : 0;
            (param[3] = new OracleParameter("p_anal_11", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _header.Ith_anal_11 == true ? 1 : 0;
            (param[4] = new OracleParameter("p_anal_12", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _header.Ith_anal_12 == true ? 1 : 0;
            (param[5] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_anal_2;
            (param[6] = new OracleParameter("p_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_anal_3;
            (param[7] = new OracleParameter("p_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_anal_4;
            (param[8] = new OracleParameter("p_anal_5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_anal_5;
            (param[9] = new OracleParameter("p_anal_6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _header.Ith_anal_6;//Decimal to Int32
            (param[10] = new OracleParameter("p_anal_7", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _header.Ith_anal_7;//Decimal to Int32
            (param[11] = new OracleParameter("p_anal_8", OracleDbType.Date, null, ParameterDirection.Input)).Value = _header.Ith_anal_8;
            (param[12] = new OracleParameter("p_anal_9", OracleDbType.Date, null, ParameterDirection.Input)).Value = _header.Ith_anal_9;
            (param[13] = new OracleParameter("p_bus_entity", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_bus_entity;
            (param[14] = new OracleParameter("p_cate_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_cate_tp;
            (param[15] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_channel;
            (param[16] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_com;
            (param[17] = new OracleParameter("p_com_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_com_docno;
            (param[18] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_cre_by;
            (param[19] = new OracleParameter("p_cre_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _header.Ith_cre_when;
            (param[20] = new OracleParameter("p_del_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_del_add1;
            (param[21] = new OracleParameter("p_del_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_del_add2;
            (param[22] = new OracleParameter("p_del_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_del_code;//"del code";
            (param[23] = new OracleParameter("p_del_party", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_del_party;
            (param[24] = new OracleParameter("p_del_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_del_town;
            (param[25] = new OracleParameter("p_direct", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _header.Ith_direct == true ? 1 : 0;
            (param[26] = new OracleParameter("p_doc_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _header.Ith_doc_date;
            (param[27] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_doc_no;
            (param[28] = new OracleParameter("p_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_doc_tp;
            (param[29] = new OracleParameter("p_doc_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _header.Ith_doc_year;
            (param[30] = new OracleParameter("p_entry_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_entry_no;
            (param[31] = new OracleParameter("p_entry_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_entry_tp;
            (param[32] = new OracleParameter("p_git_close", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _header.Ith_git_close == true ? 1 : 0;
            (param[33] = new OracleParameter("p_git_close_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _header.Ith_git_close_date;
            (param[34] = new OracleParameter("p_git_close_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_git_close_doc;
            (param[35] = new OracleParameter("p_is_manual", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _header.Ith_is_manual == true ? 1 : 0;
            (param[36] = new OracleParameter("p_isprinted", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _header.Ith_isprinted == true ? 1 : 0;
            (param[37] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_job_no;
            (param[38] = new OracleParameter("p_loading_point", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_loading_point;
            (param[39] = new OracleParameter("p_loading_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_loading_user;
            (param[40] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_loc;
            (param[41] = new OracleParameter("p_manual_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_manual_ref;
            (param[42] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_mod_by;
            (param[43] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _header.Ith_mod_when;
            (param[44] = new OracleParameter("p_noofcopies", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _header.Ith_noofcopies;
            (param[45] = new OracleParameter("p_oth_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_oth_loc;
            (param[46] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_pc; // add 18/05/2012 chamal
            (param[47] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_remarks;
            (param[48] = new OracleParameter("p_sbu", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_sbu;
            (param[49] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _header.Ith_seq_no;// GetSerialID();
            (param[50] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_session_id;
            (param[51] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_stus;
            (param[52] = new OracleParameter("p_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_sub_tp;
            (param[53] = new OracleParameter("p_vehi_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_vehi_no;
            (param[54] = new OracleParameter("p_sub_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_sub_docno;
            (param[55] = new OracleParameter("p_oth_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_oth_docno;
            (param[56] = new OracleParameter("p_oth_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _header.Ith_oth_com; //add 22/05/2012 chamal
            param[57] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            //p_del_cd
            effects = (Int16)UpdateRecords("sp_save_inthdr", CommandType.StoredProcedure, param);
            return effects;

        }

        //Written By P.Wijetunge on 02/04/2012
        public bool SaveInventoryItem(InventoryItem _item)
        {
            OracleParameter[] param = new OracleParameter[10];
            Int16 effects = 0;

            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _item.Iti_seq_no;
            (param[1] = new OracleParameter("p_item_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _item.Iti_item_line;
            (param[2] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_doc_no;
            (param[3] = new OracleParameter("p_bin_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_bin_code;
            (param[4] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_item_code;
            (param[5] = new OracleParameter("p_item_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_item_status;
            (param[6] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _item.Iti_qty;
            (param[7] = new OracleParameter("p_bal_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _item.Iti_bal_qty;
            (param[8] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _item.Iti_year;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_save_intitm", CommandType.StoredProcedure, param);
            return effects >= 1 ? true : false;
        }

        //Written By P.Wijetunge on 02/04/2012 
        //Edit by Chamal De Silva on 17/04/2012
        public Int32 SaveInventorySerial(InventorySerialN _serial, Int16 _invDirect)
        {
            OracleParameter[] param = new OracleParameter[35];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_seq_no;
            (param[1] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_itm_line;
            (param[2] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_batch_line;
            (param[3] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_ser_line;
            (param[4] = new OracleParameter("p_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_direct;
            (param[5] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_doc_no;
            (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serial.Ins_doc_dt;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_com;
            (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_loc;
            (param[9] = new OracleParameter("p_bin;", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_bin;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _serial.Ins_unit_cost;
            (param[13] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_ser_id;
            (param[14] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_ser_1;
            (param[15] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_ser_2;
            (param[16] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_ser_3;
            (param[17] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_ser_4;
            (param[18] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_warr_no;
            (param[19] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_warr_period;
            (param[20] = new OracleParameter("p_orig_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_orig_grncom;
            (param[21] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_orig_grnno;
            (param[22] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serial.Ins_orig_grndt;
            (param[23] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_orig_supp;
            (param[24] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_exist_grncom;
            (param[25] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_exist_grnno;
            (param[26] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serial.Ins_exist_grndt;
            (param[27] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_exist_supp;
            (param[28] = new OracleParameter("p_cross_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_cross_seqno;
            (param[29] = new OracleParameter("p_cross_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_cross_itmline;
            (param[30] = new OracleParameter("p_cross_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_cross_batchline;
            (param[31] = new OracleParameter("p_cross_serline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_cross_serline;
            (param[32] = new OracleParameter("p_issue_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serial.Ins_issue_dt;
            (param[33] = new OracleParameter("p_inv_direction", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invDirect;
            param[34] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_save_intser", CommandType.StoredProcedure, param);
            return effects;
        }

        //Written By P.Wijetunge on 02/04/2012
        //Edit by Chamal De Silva on 17/04/2012
        public bool SaveInventorySerialRef(InventorySerialRefN _serial, Int16 _invDirect)
        {
            OracleParameter[] param = new OracleParameter[37];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_loc;
            (param[2] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_bin;
            (param[3] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_doc_no;
            (param[4] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_seq_no;
            (param[5] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_itm_line;
            (param[6] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_batch_line;
            (param[7] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_ser_line;
            (param[8] = new OracleParameter("p_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_direct;
            (param[9] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serial.Ins_doc_dt;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _serial.Ins_unit_cost;
            (param[13] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _serial.Ins_unit_price;
            (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_ser_id;
            (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_ser_1;
            (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_ser_2;
            (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_ser_3;
            (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_ser_4;
            (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_warr_no;
            (param[20] = new OracleParameter("p_available", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _serial.Ins_available;
            (param[21] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_warr_period;
            (param[22] = new OracleParameter("p_orig_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_orig_grncom;
            (param[23] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_orig_grnno;
            (param[24] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serial.Ins_orig_grndt;
            (param[25] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_orig_supp;
            (param[26] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_exist_grncom;
            (param[27] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_exist_grnno;
            (param[28] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serial.Ins_exist_grndt;
            (param[29] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_exist_supp;
            (param[30] = new OracleParameter("p_cross_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_cross_seqno;
            (param[31] = new OracleParameter("p_cross_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_cross_itmline;
            (param[32] = new OracleParameter("p_cross_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_cross_batchline;
            (param[33] = new OracleParameter("p_cross_serline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_cross_serline;
            (param[34] = new OracleParameter("p_issue_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _serial.Ins_issue_dt;
            (param[35] = new OracleParameter("p_inv_direction", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invDirect;
            param[36] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_save_inrser", CommandType.StoredProcedure, param);
            return effects >= 1 ? true : false;
        }

        //Written By P.Wijetunge on 02/04/2012
        public bool SaveInventoryBatch(InventoryBatchN _batch)
        {
            OracleParameter[] param = new OracleParameter[34];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_seq_no;
            (param[1] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_itm_line;
            (param[2] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_batch_line;
            (param[3] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_batch_no;
            (param[4] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_doc_no;
            (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_com;
            (param[6] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_loc;
            (param[7] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_bin;
            (param[8] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_itm_cd;
            (param[9] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_itm_stus;
            (param[10] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_qty;
            (param[11] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_unit_cost;
            (param[12] = new OracleParameter("p_bal_qty1", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Itb_bal_qty1;
            (param[13] = new OracleParameter("p_bal_qty2", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Itb_bal_qty2;
            (param[14] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_unit_price;
            (param[15] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no;
            (param[16] = new OracleParameter("p_base_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_ref_no;
            (param[17] = new OracleParameter("p_base_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _batch.Itb_base_doc_dt;
            (param[18] = new OracleParameter("p_base_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmcd;
            (param[19] = new OracleParameter("p_base_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmline;
            (param[20] = new OracleParameter("p_base_itmstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmstus;
            (param[21] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_job_no;
            (param[22] = new OracleParameter("p_git_ignore", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _batch.Itb_git_ignore;
            (param[23] = new OracleParameter("p_git_ignore_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Itb_git_ignore_by;
            (param[24] = new OracleParameter("p_git_ignore_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _batch.Itb_git_ignore_dt;
            (param[25] = new OracleParameter("p_git_ignore_effdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _batch.Itb_git_ignore_effdt;
            (param[26] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_res_no;
            (param[27] = new OracleParameter("p_res_lineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_res_lineno;
            (param[28] = new OracleParameter("p_base_doc_no1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no1;
            (param[29] = new OracleParameter("p_base_doc_no2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no2;
            (param[30] = new OracleParameter("p_base_doc_no3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no3;
            (param[31] = new OracleParameter("p_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_cur_cd;
            (param[32] = new OracleParameter("p_grup_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_grup_cur_cd;
            param[33] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_save_intbatch", CommandType.StoredProcedure, param);
            return effects >= 1 ? true : false;

        }

        //Written By P.Wijetunge on 02/04/2012
        public bool SaveWarrantyDetail(InventoryWarrantyDetail _inventoryWarrantyDetail)
        {
            #region add by lakshan 28jun2017
            MasterItem _itm = GetItemMaster(_inventoryWarrantyDetail.Irsm_itm_cd);
            if (_itm != null)
            {
                _inventoryWarrantyDetail.Irsm_itm_brand = _itm.Mi_brand;
                _inventoryWarrantyDetail.Irsm_itm_desc = _itm.Mi_shortdesc;
                _inventoryWarrantyDetail.Irsm_itm_model = _itm.Mi_model;
            }
            #endregion
            OracleParameter[] param = new OracleParameter[60];
            Int16 effects = 0;

            (param[0] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_acc_no;
            (param[1] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_anal_1;
            (param[2] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_anal_2;
            (param[3] = new OracleParameter("p_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_anal_3;
            (param[4] = new OracleParameter("p_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_anal_4;
            (param[5] = new OracleParameter("p_anal_5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_anal_5;
            (param[6] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_channel;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_com;
            (param[8] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cre_by;
            (param[9] = new OracleParameter("p_cre_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cre_when;
            (param[10] = new OracleParameter("p_cust_addr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_addr;
            (param[11] = new OracleParameter("p_cust_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_cd;
            (param[12] = new OracleParameter("p_cust_del_addr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_del_addr;
            (param[13] = new OracleParameter("p_cust_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_email;
            (param[14] = new OracleParameter("p_cust_fax", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_fax;
            (param[15] = new OracleParameter("p_cust_mobile", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_mobile;
            (param[16] = new OracleParameter("p_cust_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_name;
            (param[17] = new OracleParameter("p_cust_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_prefix;
            (param[18] = new OracleParameter("p_cust_tel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_tel;
            (param[19] = new OracleParameter("p_cust_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_town;
            (param[20] = new OracleParameter("p_cust_vat_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_cust_vat_no;
            (param[21] = new OracleParameter("p_direct", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_direct;
            (param[22] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_doc_dt;
            (param[23] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_doc_no;
            (param[24] = new OracleParameter("p_doc_year", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_doc_year;
            (param[25] = new OracleParameter("p_exist_grn_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_exist_grn_com;
            (param[26] = new OracleParameter("p_exist_grn_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_exist_grn_dt;
            (param[27] = new OracleParameter("p_exist_grn_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_exist_grn_no;
            (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_exist_supp;
            (param[29] = new OracleParameter("p_invoice_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_invoice_dt;
            (param[30] = new OracleParameter("p_invoice_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_invoice_no;
            (param[31] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_itm_brand;
            (param[32] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_itm_cd;
            (param[33] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_itm_desc;
            (param[34] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_itm_model;
            (param[35] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_itm_stus;
            (param[36] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_loc;
            (param[37] = new OracleParameter("p_loc_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_loc_desc;
            (param[38] = new OracleParameter("p_mfc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_mfc;
            (param[39] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_mod_by;
            (param[40] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_mod_when;
            (param[41] = new OracleParameter("p_orig_grn_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_orig_grn_com;
            (param[42] = new OracleParameter("p_orig_grn_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_orig_grn_dt;
            (param[43] = new OracleParameter("p_orig_grn_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_orig_grn_no;
            (param[44] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_orig_supp;
            (param[45] = new OracleParameter("p_sbu", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_sbu;
            (param[46] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_ser_1;
            (param[47] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_ser_2;
            (param[48] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_ser_3;
            (param[49] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_ser_4;
            (param[50] = new OracleParameter("p_ser_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_ser_id;
            (param[51] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_session_id;
            (param[52] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_unit_cost;
            (param[53] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_unit_price;
            (param[54] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_warr_no;
            (param[55] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_warr_period;
            (param[56] = new OracleParameter("p_warr_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_warr_rem;
            (param[57] = new OracleParameter("p_warr_start_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_warr_start_dt;
            (param[58] = new OracleParameter("p_warr_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantyDetail.Irsm_warr_stus;
            param[59] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_update_inr_sermst", CommandType.StoredProcedure, param);
            return effects >= 1 ? true : false;
        }

        //Written By P.Wijetunge on 02/04/2012
        public bool SaveWarrantySubDetail(InventoryWarrantySubDetail _inventoryWarrantySubDetail)
        {
            OracleParameter[] param = new OracleParameter[11];
            Int16 effects = 0;

            (param[0] = new OracleParameter("Irsms_act", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_act;
            (param[1] = new OracleParameter("Irsms_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_itm_cd;
            (param[2] = new OracleParameter("Irsms_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_itm_stus;
            (param[3] = new OracleParameter("Irsms_mfc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_mfc;
            (param[4] = new OracleParameter("Irsms_ser_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_ser_id;
            (param[5] = new OracleParameter("Irsms_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_ser_line;
            (param[6] = new OracleParameter("Irsms_sub_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_sub_ser;
            (param[7] = new OracleParameter("Irsms_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_tp;
            (param[8] = new OracleParameter("Irsms_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_warr_no;
            (param[9] = new OracleParameter("Irsms_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_warr_period;
            (param[10] = new OracleParameter("Irsms_warr_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryWarrantySubDetail.Irsms_warr_rem;
            effects = (Int16)UpdateRecords("", CommandType.StoredProcedure, param);
            return effects >= 1 ? true : false;
        }


        // Written by P.Wijetunge on 05/04/2012
        /// <summary>
        /// Common Get Serial ID function return and increase the serial id no
        /// </summary>
        /// <returns>Unique Serial ID </returns>
        public Int32 GetSerialID()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int16 effects = 0;
            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_getserialid", CommandType.StoredProcedure, param);
            return effects;
        }

        #endregion

        #region Inventory In/Out Process

        //Written By Chamal De Silva on 10/04/2012
        public Int16 UpdateInventoryLocation(InventoryLocation _invLoca, Int16 _invDirect)
        {
            OracleParameter[] param = new OracleParameter[14];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invLoca.Inl_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invLoca.Inl_loc;
            (param[2] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invLoca.Inl_itm_cd;
            (param[3] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invLoca.Inl_itm_stus;
            (param[4] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invLoca.Inl_qty;
            (param[5] = new OracleParameter("p_free_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invLoca.Inl_free_qty;
            (param[6] = new OracleParameter("p_res_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invLoca.Inl_res_qty;
            (param[7] = new OracleParameter("p_isu_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invLoca.Inl_isu_qty;
            (param[8] = new OracleParameter("p_bl_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invLoca.Inl_bl_qty;
            (param[9] = new OracleParameter("p_eo_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invLoca.Inl_eo_qty;
            (param[10] = new OracleParameter("p_ro_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invLoca.Inl_ro_qty;
            (param[11] = new OracleParameter("p_usr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invLoca.Inl_cre_by;
            (param[12] = new OracleParameter("p_inv_direction", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invDirect;
            param[13] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_inrloc", CommandType.StoredProcedure, param);
            return effects;
        }

        //Written By Chamal De Silva on 10/04/2012
        public Int16 UpdateInventoryItem(InventoryItem _item)
        {
            OracleParameter[] param = new OracleParameter[10];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_bal_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _item.Iti_bal_qty;
            (param[1] = new OracleParameter("p_bin_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_bin_code;
            (param[2] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_doc_no;
            (param[3] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_item_code;
            (param[4] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _item.Iti_item_line;
            (param[5] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_item_status;
            (param[6] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _item.Iti_qty;
            (param[7] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _item.Iti_seq_no;
            (param[8] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _item.Iti_year;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_intitm", CommandType.StoredProcedure, param);
            return effects;
        }

        //Written By Chamal De Silva on 10/04/2012
        public Int16 GetInventoryItemLine(InventoryItem _item)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _item.Iti_seq_no;
            (param[1] = new OracleParameter("p_bin_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_bin_code;
            (param[2] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_item_code;
            (param[3] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item.Iti_item_status;
            OracleParameter _outParam = new OracleParameter("o_itm_line", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = Convert.ToInt16(ReturnSP_SingleValue("sp_getintitmline", CommandType.StoredProcedure, _outParam, param));
            return effects;
        }

        //Written By Chamal De Silva on 10/04/2012
        public Int16 GetInventoryBatchLine(InventoryBatchN _batch)
        {
            OracleParameter[] param = new OracleParameter[11];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_seq_no;
            (param[1] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_batch_no;
            (param[2] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_bin;
            (param[3] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_itm_cd;
            (param[4] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_itm_stus;
            (param[5] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_unit_cost;
            (param[6] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no;
            (param[7] = new OracleParameter("p_base_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmcd;
            (param[8] = new OracleParameter("p_base_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmline;
            (param[9] = new OracleParameter("p_base_itmstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmstus;
            (param[10] = new OracleParameter("p_base_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Itb_base_batchline;
            OracleParameter _outparam = new OracleParameter("o_batch_line", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = Convert.ToInt16(ReturnSP_SingleValue("sp_getintbatchline", CommandType.StoredProcedure, _outparam, param));
            return effects;
        }

        //Written By Chamal De Silva on 10/04/2012
        public Int16 UpdateInventoryBatch(InventoryBatchN _batch)
        {
            OracleParameter[] param = new OracleParameter[35];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_seq_no;
            (param[1] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_itm_line;
            (param[2] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_batch_line;
            (param[3] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_batch_no;
            (param[4] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_doc_no;
            (param[5] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_com;
            (param[6] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_loc;
            (param[7] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_bin;
            (param[8] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_itm_cd;
            (param[9] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_itm_stus;
            (param[10] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_qty;
            (param[11] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_unit_cost;
            (param[12] = new OracleParameter("p_bal_qty1", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Itb_bal_qty1;
            (param[13] = new OracleParameter("p_bal_qty2", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Itb_bal_qty2;
            (param[14] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_unit_price;
            (param[15] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no;
            (param[16] = new OracleParameter("p_base_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_ref_no;
            (param[17] = new OracleParameter("p_base_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _batch.Itb_base_doc_dt;
            (param[18] = new OracleParameter("p_base_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmcd;
            (param[19] = new OracleParameter("p_base_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmline;
            (param[20] = new OracleParameter("P_base_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Itb_base_batchline;
            (param[21] = new OracleParameter("p_base_itmstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmstus;
            (param[22] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_job_no;
            (param[23] = new OracleParameter("p_git_ignore", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _batch.Itb_git_ignore;
            (param[24] = new OracleParameter("p_git_ignore_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Itb_git_ignore_by;
            (param[25] = new OracleParameter("p_git_ignore_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _batch.Itb_git_ignore_dt;
            (param[26] = new OracleParameter("p_git_ignore_effdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _batch.Itb_git_ignore_effdt;
            (param[27] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_res_no;
            (param[28] = new OracleParameter("p_res_lineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_res_lineno;
            (param[29] = new OracleParameter("p_base_doc_no1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no1;
            (param[30] = new OracleParameter("p_base_doc_no2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no2;
            (param[31] = new OracleParameter("p_base_doc_no3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no3;
            (param[32] = new OracleParameter("p_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_cur_cd;
            (param[33] = new OracleParameter("p_grup_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_grup_cur_cd;
            param[34] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_intbatch", CommandType.StoredProcedure, param);
            return effects;
        }


        //Written By Chamal De Silva on 10/04/2012
        public Int16 UpdateInventoryBatchRef(InventoryBatchN _batch, Int16 _invDirect)
        {
            OracleParameter[] param = new OracleParameter[37];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no;
            (param[1] = new OracleParameter("p_base_doc_no1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no1;
            (param[2] = new OracleParameter("p_base_doc_no2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no2;
            (param[3] = new OracleParameter("p_base_doc_no3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_doc_no3;
            (param[4] = new OracleParameter("p_base_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmcd;
            (param[5] = new OracleParameter("p_base_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmline;
            (param[6] = new OracleParameter("p_base_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Itb_base_batchline;
            (param[7] = new OracleParameter("p_base_itmstus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_itmstus;
            (param[8] = new OracleParameter("p_base_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_base_qty;
            (param[9] = new OracleParameter("p_base_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_base_ref_no;
            (param[10] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_batch_line;
            (param[11] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_batch_no;
            (param[12] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_bin;
            (param[13] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_com;
            (param[14] = new OracleParameter("p_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_cur_cd;
            (param[15] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _batch.Inb_doc_dt;
            (param[16] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_doc_no;
            (param[17] = new OracleParameter("p_ex_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_ex_rate;
            (param[18] = new OracleParameter("p_excess_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_excess_qty;
            (param[19] = new OracleParameter("p_free_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_free_qty;
            (param[20] = new OracleParameter("p_grup_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_grup_cur_cd;
            (param[21] = new OracleParameter("p_grup_ex_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_grup_ex_rate;
            (param[22] = new OracleParameter("p_isu_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_isu_qty;
            (param[23] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_itm_cd;
            (param[24] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_itm_line;
            (param[25] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_itm_stus;
            (param[26] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_job_no;
            (param[27] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_loc;
            (param[28] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_qty;
            (param[29] = new OracleParameter("p_res_lineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_res_lineno;
            (param[30] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _batch.Inb_res_no;
            (param[31] = new OracleParameter("p_res_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_res_qty;
            (param[32] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _batch.Inb_seq_no;
            (param[33] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_unit_cost;
            (param[34] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _batch.Inb_unit_price;
            (param[35] = new OracleParameter("p_inv_direction", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invDirect;
            param[36] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_inrbatch", CommandType.StoredProcedure, param);
            return effects;
        }

        //public Int16 UpdateInventoryWarrantyMaster(InventoryWarrantyDetail _warranty)
        //       {
        //           OracleParameter[] param = new OracleParameter[60];
        //           Int16 effects = 0;
        //           (param[0] = new OracleParameter("Irsm_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_acc_no;
        //           (param[1] = new OracleParameter("Irsm_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_anal_1;
        //           (param[2] = new OracleParameter("Irsm_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_anal_2;
        //           (param[3] = new OracleParameter("Irsm_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_anal_3;
        //           (param[4] = new OracleParameter("Irsm_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_anal_4;
        //           (param[5] = new OracleParameter("Irsm_anal_5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_anal_5;
        //           (param[6] = new OracleParameter("Irsm_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_channel;
        //           (param[7] = new OracleParameter("Irsm_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_com;
        //           (param[8] = new OracleParameter("Irsm_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cre_by;
        //           (param[9] = new OracleParameter("Irsm_cre_when", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cre_when;
        //           (param[10] = new OracleParameter("Irsm_cust_addr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_addr;
        //           (param[11] = new OracleParameter("Irsm_cust_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_cd;
        //           (param[12] = new OracleParameter("Irsm_cust_del_addr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_del_addr;
        //           (param[13] = new OracleParameter("Irsm_cust_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_email;
        //           (param[14] = new OracleParameter("Irsm_cust_fax", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_fax;
        //           (param[15] = new OracleParameter("Irsm_cust_mobile", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_mobile;
        //           (param[16] = new OracleParameter("Irsm_cust_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_name;
        //           (param[17] = new OracleParameter("Irsm_cust_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_prefix;
        //           (param[18] = new OracleParameter("Irsm_cust_tel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_tel;
        //           (param[19] = new OracleParameter("Irsm_cust_town", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_town;
        //           (param[20] = new OracleParameter("Irsm_cust_vat_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_cust_vat_no;
        //           (param[21] = new OracleParameter("Irsm_direct", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_direct;
        //           (param[22] = new OracleParameter("Irsm_doc_dt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_doc_dt;
        //           (param[23] = new OracleParameter("Irsm_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_doc_no;
        //           (param[24] = new OracleParameter("Irsm_doc_year", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_doc_year;
        //           (param[25] = new OracleParameter("Irsm_exist_grn_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_exist_grn_com;
        //           (param[26] = new OracleParameter("Irsm_exist_grn_dt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_exist_grn_dt;
        //           (param[27] = new OracleParameter("Irsm_exist_grn_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_exist_grn_no;
        //           (param[28] = new OracleParameter("Irsm_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_exist_supp;
        //           (param[29] = new OracleParameter("Irsm_invoice_dt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_invoice_dt;
        //           (param[30] = new OracleParameter("Irsm_invoice_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_invoice_no;
        //           (param[31] = new OracleParameter("Irsm_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_itm_brand;
        //           (param[32] = new OracleParameter("Irsm_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_itm_cd;
        //           (param[33] = new OracleParameter("Irsm_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_itm_desc;
        //           (param[34] = new OracleParameter("Irsm_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_itm_model;
        //           (param[35] = new OracleParameter("Irsm_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_itm_stus;
        //           (param[36] = new OracleParameter("Irsm_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_loc;
        //           (param[37] = new OracleParameter("Irsm_loc_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_loc_desc;
        //           (param[38] = new OracleParameter("Irsm_mfc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_mfc;
        //           (param[39] = new OracleParameter("Irsm_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_mod_by;
        //           (param[40] = new OracleParameter("Irsm_mod_when", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_mod_when;
        //           (param[41] = new OracleParameter("Irsm_orig_grn_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_orig_grn_com;
        //           (param[42] = new OracleParameter("Irsm_orig_grn_dt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_orig_grn_dt;
        //           (param[43] = new OracleParameter("Irsm_orig_grn_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_orig_grn_no;
        //           (param[44] = new OracleParameter("Irsm_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_orig_supp;
        //           (param[45] = new OracleParameter("Irsm_sbu", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_sbu;
        //           (param[46] = new OracleParameter("Irsm_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_ser_1;
        //           (param[47] = new OracleParameter("Irsm_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_ser_2;
        //           (param[48] = new OracleParameter("Irsm_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_ser_3;
        //           (param[49] = new OracleParameter("Irsm_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_ser_4;
        //           (param[50] = new OracleParameter("Irsm_ser_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_ser_id;
        //           (param[51] = new OracleParameter("Irsm_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_session_id;
        //           (param[52] = new OracleParameter("Irsm_unit_cost", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_unit_cost;
        //           (param[53] = new OracleParameter("Irsm_unit_price", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_unit_price;
        //           (param[54] = new OracleParameter("Irsm_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_warr_no;
        //           (param[55] = new OracleParameter("Irsm_warr_period", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_warr_period;
        //           (param[56] = new OracleParameter("Irsm_warr_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_warr_rem;
        //           (param[57] = new OracleParameter("Irsm_warr_start_dt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_warr_start_dt;
        //           (param[58] = new OracleParameter("Irsm_warr_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty.Irsm_warr_stus;

        //           param[59] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
        //           effects = (Int16)UpdateRecords("sp_update_inr_sermst", CommandType.StoredProcedure, param);
        //           return effects;
        //       }



        //Written By Chamal De Silva on 10/04/2012
        public Int16 GetInventorySerLine(InventorySerialN _serial)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_seq_no;
            (param[1] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_itm_line;
            (param[2] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serial.Ins_batch_line;
            (param[3] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_bin;
            (param[4] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_itm_cd;
            (param[5] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial.Ins_itm_stus;
            (param[6] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _serial.Ins_unit_cost;
            OracleParameter _outParam = new OracleParameter("o_ser_no", OracleDbType.Int32, null, ParameterDirection.Output);
            //Int16 _effect = Convert.ToInt16(ReturnSP_SingleValue("sp_getintserline", CommandType.StoredProcedure, _outParam, param));
            effects = Convert.ToInt16(ReturnSP_SingleValue("sp_getintserline_duplicate", CommandType.StoredProcedure, _outParam, param));
            return effects;


        }

        //Written By Chamal De Silva on 15/04/2012
        public Int16 UpdateMovementDocNo(Int32 _seqno, string _docno)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqno;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docno;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_movseqno", CommandType.StoredProcedure, param);
            return effects;

        }

        public Int16 UpdateRCCDocNo(Int32 _seqno, string _docno)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqno;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docno;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_rcc_req_no", CommandType.StoredProcedure, param);
            return effects;

        }

        //Written By M.Geeganage on 19/04/2012
        public Int16 UpdateInventoryRequestDocNo(Int32 _seqno, string _docno)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqno;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docno;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_InvReqDocNo", CommandType.StoredProcedure, param);
            return effects;

        }

        #endregion

        //Written By Prabhath on 02/05/2012

        public DataTable GetItemComponentTable(string _mainItemCode)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_main_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mainItemCode;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _resultDT = QueryDataTable("tblItemComponent", "sp_get_mst_itm_components", CommandType.StoredProcedure, false, param);
            return _resultDT;
        }


        //Miginda - 03/05/2012
        public PurchaseOrder GetPurchaseOrderHeaderDetails(string _companyCode, string _docNo)
        {
            PurchaseOrder _purchaseOrder = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _companyCode;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblPOHeaderData", "sp_get_po_header_details", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _purchaseOrder = DataTableExtensions.ToGenericList<PurchaseOrder>(_dtResults, PurchaseOrder.ConvertTotal)[0];
            }

            return _purchaseOrder;
        }

        //Miginda - 03/05/2012
        public List<PurchaseOrderDelivery> GetConsignmentItemDetails(string _companyCode, string _docNo, string _locCode)
        {
            List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = null;
            PurchaseOrderDelivery _purchaseOrderDelivery = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _companyCode;
            (param[1] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _locCode;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblConsignmentItemDetails", "sp_get_consign_item_details", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _purchaseOrderDelivery = new PurchaseOrderDelivery();

                    _purchaseOrderDelivery.Podi_seq_no = (_dtResults.Rows[i]["podi_seq_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["podi_seq_no"]);
                    _purchaseOrderDelivery.Podi_line_no = (_dtResults.Rows[i]["podi_line_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["podi_line_no"]);
                    _purchaseOrderDelivery.Podi_del_line_no = (_dtResults.Rows[i]["podi_del_line_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["podi_del_line_no"]);

                    _purchaseOrderDelivery.Podi_itm_stus = (_dtResults.Rows[i]["PODI_ITM_STUS"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["PODI_ITM_STUS"].ToString();
                    _purchaseOrderDelivery.Podi_bal_qty = (_dtResults.Rows[i]["PODI_BAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["PODI_BAL_QTY"]);
                    _purchaseOrderDelivery.Actual_qty = 0;

                    _purchaseOrderDelivery.MasterItem = new MasterItem()
                    {
                        Mi_cd = (_dtResults.Rows[i]["PODI_ITM_CD"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["PODI_ITM_CD"].ToString(),
                        Mi_longdesc = (_dtResults.Rows[i]["MI_LONGDESC"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["MI_LONGDESC"].ToString(),
                        Mi_brand = (_dtResults.Rows[i]["MI_BRAND"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["MI_BRAND"].ToString(),
                        Mi_model = (_dtResults.Rows[i]["MI_MODEL"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["MI_MODEL"].ToString(),
                        Mi_is_ser1 = (_dtResults.Rows[i]["mi_is_ser1"] == DBNull.Value) ? -2 : Convert.ToInt32(_dtResults.Rows[i]["mi_is_ser1"]),
                        Mi_is_ser2 = (_dtResults.Rows[i]["mi_is_ser2"] == DBNull.Value) ? -2 : Convert.ToInt32(_dtResults.Rows[i]["mi_is_ser2"]),
                        Mi_is_ser3 = (_dtResults.Rows[i]["mi_is_ser3"] == DBNull.Value) ? false : Convert.ToBoolean(_dtResults.Rows[i]["mi_is_ser3"])
                    };

                    _purchaseOrderDelivery.PurchaseOrderDetail = new PurchaseOrderDetail()
                    {
                        Pod_unit_price = (_dtResults.Rows[i]["pod_unit_price"] == DBNull.Value) ? 0 : Convert.ToDecimal(_dtResults.Rows[i]["pod_unit_price"]),
                        Pod_line_no = (_dtResults.Rows[i]["pod_line_no"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["pod_line_no"])
                    };


                    _purchaseOrderDeliveryList.Add(_purchaseOrderDelivery);
                }

            }

            return _purchaseOrderDeliveryList;
        }


        //Code By Miginda on 10/05/2012
        public int UpdateConsignReceiptBalanceQty(int _seq_no, int _line_no, int _del_line_no, string _loc, string _item_cd, decimal _act_qty)
        {
            int rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq_no;
                (param[1] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _line_no;
                (param[2] = new OracleParameter("p_del_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _del_line_no;
                (param[3] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
                (param[4] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item_cd;
                (param[5] = new OracleParameter("p_act_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _act_qty;
                param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                //Open the connection and call the stored procedure.
                //ConnectionOpen(); Edit (Comment) by Chamal 15-05-2012
                rows_affected = UpdateRecords("sp_update_po_items_balqty", CommandType.StoredProcedure, param);

            }
            catch (Exception)
            {
                rows_affected = -1;
            }
            finally
            {
                //ConnectionClose();
            }

            return rows_affected;
        }

        //Miginda - 03/05/2012
        public List<PurchaseOrder> GetAllPendingConsignmentRequestData(PurchaseOrder _paramPurchaseOrder)
        {
            List<PurchaseOrder> _purchaseOrderList = null;
            PurchaseOrder _purchaseOrder = null;

            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramPurchaseOrder.Poh_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramPurchaseOrder.MasterLocation.Ml_loc_cd;
            (param[2] = new OracleParameter("p_orderStatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramPurchaseOrder.Poh_stus;
            (param[3] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramPurchaseOrder.MasterBusinessEntity.Mbe_tp;
            (param[4] = new OracleParameter("p_fromDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramPurchaseOrder.FromDate;
            (param[5] = new OracleParameter("p_toDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramPurchaseOrder.ToDate;
            (param[6] = new OracleParameter("p_suppCode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramPurchaseOrder.Poh_supp;
            (param[7] = new OracleParameter("p_reqNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramPurchaseOrder.Poh_doc_no;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblPendingConsRequests", "sp_get_pending_cons_requests", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _purchaseOrderList = new List<PurchaseOrder>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _purchaseOrder = new PurchaseOrder();

                    _purchaseOrder.Poh_seq_no = (_dtResults.Rows[i]["POH_SEQ_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(_dtResults.Rows[i]["POH_SEQ_NO"]);
                    _purchaseOrder.Poh_doc_no = (_dtResults.Rows[i]["POH_DOC_NO"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["POH_DOC_NO"].ToString();
                    _purchaseOrder.Poh_dt = (_dtResults.Rows[i]["POH_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_dtResults.Rows[i]["POH_DT"]);
                    _purchaseOrder.Poh_ref = (_dtResults.Rows[i]["POH_REF"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["POH_REF"].ToString();

                    _purchaseOrder.MasterBusinessEntity = new MasterBusinessEntity()
                    {
                        Mbe_cd = (_dtResults.Rows[i]["POH_SUPP"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["POH_SUPP"].ToString(),
                        Mbe_name = (_dtResults.Rows[i]["MBE_NAME"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["MBE_NAME"].ToString(),
                        Mbe_acc_cd = (_dtResults.Rows[i]["MBE_ACC_CD"] == DBNull.Value) ? string.Empty : _dtResults.Rows[i]["MBE_ACC_CD"].ToString(),
                    };

                    _purchaseOrderList.Add(_purchaseOrder);
                }

            }

            return _purchaseOrderList;
        }


        //Code By Miginda on 08/05/2012
        public Int32 IsExistInSerialMaster(string _companyCode, string _itemCode, string _serialNo1)
        {
            //ConnectionOpen();
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _companyCode;
            (param[1] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
            (param[2] = new OracleParameter("p_serialNo1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serialNo1;
            OracleParameter outParameter = new OracleParameter("serial_id", OracleDbType.Int32, null, ParameterDirection.Output);
            int _serial_id = ReturnSP_SingleValue("sp_isexist_in_serial_master", CommandType.StoredProcedure, outParameter, param);
            //ConnectionClose();
            return _serial_id;
        }

        public DataTable GetAvailableItemStatus(string _company, string _location, string _bin, string _item, string _serial)
        {
            //p_com in NVARCHAR2,p_loc in NVARCHAR2,p_bin in NVARCHAR2,p_item in NVARCHAR2,p_serial in NVARCHAR2
            //sp_availableitemstatus
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bin;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[4] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serial;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblAvlItmStatus = QueryDataTable("_tblAvlItmStatus", "sp_availableitemstatus", CommandType.StoredProcedure, false, param);
            return _tblAvlItmStatus;

        }

        //HW
        public List<InventoryLocation> GetItemInventoryBalance(string _company, string _location, string _item, string _status)
        {
            List<InventoryLocation> _list = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblBalance = QueryDataTable("_tblBalance", "sp_inventorybalance", CommandType.StoredProcedure, false, param);
            if (_tblBalance.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventoryLocation>(_tblBalance, InventoryLocation.ConvertTotal);
            }
            return _list;


        }

        //hw by Prabhath on 14 05 2012
        private ReptPickSerials SetReptPickSerials(DataRow _tr)
        {
            ReptPickSerials _pick = new ReptPickSerials();
            DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;

            //------ commented by shani and it is replaced as seen at the bottom----(Did the convertions properly.)---------------
            //_pick.Tus_batch_line = (int)_tr["INS_BATCH_LINE"];
            //_pick.Tus_bin = (string)_tr["INS_BIN"];
            //_pick.Tus_com = (string)_tr["INS_COM"];
            ////    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();//not sure
            //_pick.Tus_cre_dt = _date;//not sure
            //_pick.Tus_doc_dt = (DateTime)_tr["INS_DOC_DT"];
            //_pick.Tus_doc_no = (string)_tr["INS_DOC_NO"];
            //_pick.Tus_exist_grncom = (string)_tr["INS_EXIST_GRNCOM"];//not sure
            //_pick.Tus_exist_grndt = (DateTime)_tr["INS_EXIST_GRNDT"];//not sure
            //_pick.Tus_exist_grnno = (string)_tr["INS_EXIST_GRNNO"];
            //_pick.Tus_exist_supp = (string)_tr["INS_EXIST_SUPP"];//TODO : GET SUPPLIER
            //_pick.Tus_itm_cd = (string)_tr["INS_ITM_CD"];
            //_pick.Tus_itm_line = (int)_tr["INS_ITM_LINE"];
            //_pick.Tus_itm_stus = (string)_tr["INS_ITM_STUS"];//or ddl selected status?
            //_pick.Tus_loc = (string)_tr["INS_LOC"];//not sure
            //_pick.Tus_orig_grncom = (string)_tr["INS_ORIG_GRNCOM"];
            //_pick.Tus_orig_grndt = (DateTime)_tr["INS_ORIG_GRNDT"];
            //_pick.Tus_orig_grnno = (string)_tr["INS_EXIST_GRNNO"];
            //_pick.Tus_orig_supp = (string)_tr["INS_ORIG_SUPP"];//TODO : GET SUPPLIER
            //_pick.Tus_qty = 1;// Convert.ToDecimal(txtQty.Text.Trim());//not sure
            //_pick.Tus_seq_no = Convert.ToInt32(_tr["INS_SEQ_NO"]);
            //_pick.Tus_ser_1 = (string)_tr["INS_SER_1"];
            ////          _reptPickSerials.Tus_ser_2 = (string)tr["INS_SER_2"];
            ////          _reptPickSerials.Tus_ser_3 = (string)tr["INS_SER_3"];
            ////          _reptPickSerials.Tus_ser_4 = (string)tr["INS_SER_4"];
            //_pick.Tus_ser_id = Convert.ToInt32(_tr["INS_SER_ID"]);
            //_pick.Tus_ser_line = Convert.ToInt32(_tr["INS_SER_LINE"]);
            //// _reptPickSerials.Tus_session_id = GlbUserSessionID;//not sure
            //_pick.Tus_unit_cost = (Decimal)_tr["INS_UNIT_COST"];
            //_pick.Tus_unit_price = (Decimal)_tr["INS_UNIT_PRICE"];
            ////   _reptPickSerials.Tus_usrseq_no = (int)generated_seq;//not sure
            ////           _reptPickSerials.Tus_warr_no = (string)tr["INS_WARR_NO"];
            //_pick.Tus_warr_period = Convert.ToInt32(_tr["INS_WARR_PERIOD"]);

            //-------------------------------------------------------------------------------------------------
            _pick.Tus_batch_line = Convert.ToInt32(_tr["INS_BATCH_LINE"]);
            _pick.Tus_bin = Convert.ToString(_tr["INS_BIN"]);
            _pick.Tus_com = Convert.ToString(_tr["INS_COM"]);
            //    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();//not sure
            _pick.Tus_cre_dt = _date;//not sure
            _pick.Tus_doc_dt = Convert.ToDateTime(_tr["INS_DOC_DT"]);
            _pick.Tus_doc_no = Convert.ToString(_tr["INS_DOC_NO"]);
            _pick.Tus_exist_grncom = Convert.ToString(_tr["INS_EXIST_GRNCOM"]);//not sure
            _pick.Tus_exist_grndt = Convert.ToDateTime(_tr["INS_EXIST_GRNDT"]);//not sure
            _pick.Tus_exist_grnno = Convert.ToString(_tr["INS_EXIST_GRNNO"]);
            _pick.Tus_exist_supp = Convert.ToString(_tr["INS_EXIST_SUPP"]);//TODO : GET SUPPLIER
            _pick.Tus_itm_cd = Convert.ToString(_tr["INS_ITM_CD"]);
            _pick.Tus_itm_line = Convert.ToInt32(_tr["INS_ITM_LINE"]);
            _pick.Tus_itm_stus = Convert.ToString(_tr["INS_ITM_STUS"]);//or ddl selected status?
            _pick.Tus_loc = Convert.ToString(_tr["INS_LOC"]);//not sure
            _pick.Tus_orig_grncom = Convert.ToString(_tr["INS_ORIG_GRNCOM"]);
            _pick.Tus_orig_grndt = Convert.ToDateTime(_tr["INS_ORIG_GRNDT"]);
            _pick.Tus_orig_grnno = Convert.ToString(_tr["INS_EXIST_GRNNO"]);
            _pick.Tus_orig_supp = Convert.ToString(_tr["INS_ORIG_SUPP"]);//TODO : GET SUPPLIER
            _pick.Tus_qty = 1;// Convert.ToDecimal(txtQty.Text.Trim());//not sure
            _pick.Tus_seq_no = Convert.ToInt32(_tr["INS_SEQ_NO"]);
            _pick.Tus_ser_1 = Convert.ToString(_tr["INS_SER_1"]);
            _pick.Tus_ser_2 = Convert.ToString(_tr["INS_SER_2"]);
            _pick.Tus_ser_3 = Convert.ToString(_tr["INS_SER_3"]);
            _pick.Tus_ser_4 = Convert.ToString(_tr["INS_SER_4"]);
            _pick.Tus_ser_id = Convert.ToInt32(_tr["INS_SER_ID"]);
            _pick.Tus_ser_line = Convert.ToInt32(_tr["INS_SER_LINE"]);
            // _reptPickSerials.Tus_session_id = GlbUserSessionID;//not sure
            _pick.Tus_unit_cost = Convert.ToDecimal(_tr["INS_UNIT_COST"]);
            _pick.Tus_unit_price = Convert.ToDecimal(_tr["INS_UNIT_PRICE"]);
            //   _reptPickSerials.Tus_usrseq_no = (int)generated_seq;//not sure
            _pick.Tus_warr_no = Convert.ToString(_tr["INS_WARR_NO"]);
            _pick.Tus_warr_period = Convert.ToInt32(_tr["INS_WARR_PERIOD"]);
            return _pick;


        }

        //hw by Prabhath on 14 05 2012
        public List<ReptPickSerials> GetNonSerializedItemInTopOrder(string _company, string _location, string _bin, string _item, string _status, decimal _qty)
        {

            List<ReptPickSerials> _list = new List<ReptPickSerials>();
            ReptPickSerials _pick = new ReptPickSerials();


            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bin;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[5] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //sp_getnonserialitem_top_order
            //p_com in NVARCHAR2,p_loc in NVARCHAR2,p_bin in NVARCHAR2 ,p_item in NVARCHAR2,p_status in NVARCHAR2,p_qty IN NUMBER,c_data OUT sys_refcursor
            DataTable _tblSerial = new DataTable();
            _tblSerial = QueryDataTable("tblSerial", "sp_getnonserialitem_top_order", CommandType.StoredProcedure, false, param);

            foreach (DataRow tr in _tblSerial.Rows)
            {
                _pick = SetReptPickSerials(tr);
                _list.Add(_pick);
            }

            return _list;

        }


        public List<string> GetBinCodesforInventoryInward(string company, string location)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl_bins", "sp_getbincodes_for_inv_inward", CommandType.StoredProcedure, false, param);

            List<string> bin_list = new List<string>();
            bin_list.Add("--Select--");
            foreach (DataRow r in _dtResults.Rows)
            {
                // Get the value of the wanted column and cast it to string
                string st = (string)r["IBL_BIN_CD"];
                bin_list.Add(st);

            }

            return bin_list;
        }

        public List<InventoryRequest> GetAllMaterialRequestsList(InventoryRequest _inventoryRequest)
        {
            List<InventoryRequest> _inventoryRequestList = null;

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_loc;
            (param[2] = new OracleParameter("p_fromDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.FromDate;
            (param[3] = new OracleParameter("p_toDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.ToDate;
            (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_tp;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblAllInventoryRequestData", "sp_getallmaterialrequests", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                //Convert datatable to relavant generic List.
                _inventoryRequestList = DataTableExtensions.ToGenericList<InventoryRequest>(_dtResults, InventoryRequest.Converter);
            }

            return _inventoryRequestList;
        }

        public DataTable GetAllMaterialRequestsTable(InventoryRequest _inventoryRequest)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_loc;
            (param[2] = new OracleParameter("p_fromDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.FromDate;
            (param[3] = new OracleParameter("p_toDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.ToDate;
            (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Itr_tp;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblAllInventoryRequestData", "sp_getallmaterialrequests", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Prabhath
        public List<ReptPickSerials> GetMaterialRequestItem(string _reqNo)
        {

            List<ReptPickSerials> _list = new List<ReptPickSerials>();



            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInventoryRequestItemData", "sp_getinventoryrequestitemdata", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                foreach (DataRow tr in _dtResults.Rows)
                {

                    ReptPickSerials _pick = new ReptPickSerials();

                    _pick.Tus_itm_cd = (string)tr["itri_itm_cd"];
                    _pick.Tus_itm_desc = (string)tr["mi_longdesc"];
                    _pick.Tus_itm_model = (string)tr["mi_model"];
                    _pick.Tus_itm_stus = (string)tr["itri_itm_stus"];
                    _pick.Tus_qty = Convert.ToDecimal(tr["itri_app_qty"]);
                    _pick.Tus_base_doc_no = _reqNo;
                    _pick.Tus_base_itm_line = (Int32)tr["itri_line_no"];
                    _list.Add(_pick);

                }
            }

            return _list;
        }

        public InventorySerialMaster GetSerialMasterDetailBySerialID(Int32 _serialID)
        {
            InventorySerialMaster _list = new InventorySerialMaster();

            //p_id in NVARCHAR2
            //sp_getserialreceivecompany
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serialID;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAllInventoryRequestData", "sp_getserialreceivecompany", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<InventorySerialMaster>(_dtResults, InventorySerialMaster.ConvertTotal)[0];
            }
            return _list;

        }

        #region InventoryInwardEntry

        //Chamal 27/05/2012
        public String GetDefaultBinCode(String _com, String _loc)
        {
            String _defBin = "";
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbldefbin", "sp_getdefbin", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                foreach (DataRow tr in _dtResults.Rows)
                {
                    _defBin = (string)tr["ibl_bin_cd"];
                }
            }

            return _defBin;
        }

        //Chamal 24/05/2012
        public DataTable GetAllPendingInventoryOutwardsTable(InventoryHeader _inventoryRequest)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Ith_oth_com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Ith_oth_loc;
            (param[2] = new OracleParameter("p_fromDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.FromDate;
            (param[3] = new OracleParameter("p_toDate", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.ToDate;
            (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inventoryRequest.Ith_doc_tp;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.        
            DataTable _dtResults = QueryDataTable("tblAllInventoryRequestData", "sp_getallpendingoutwards", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public List<ReptPickSerials> GetOutwardItem(string _reqNo)
        {

            List<ReptPickSerials> _list = new List<ReptPickSerials>();



            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInventoryRequestItemData", "sp_getinventoryrequestitemdata", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                foreach (DataRow tr in _dtResults.Rows)
                {

                    ReptPickSerials _pick = new ReptPickSerials();

                    _pick.Tus_itm_cd = (string)tr["itri_itm_cd"];
                    _pick.Tus_itm_desc = (string)tr["mi_longdesc"];
                    _pick.Tus_itm_model = (string)tr["mi_model"];
                    _pick.Tus_itm_stus = (string)tr["itri_itm_stus"];
                    _pick.Tus_qty = Convert.ToDecimal(tr["itri_app_qty"]);
                    _pick.Tus_base_doc_no = _reqNo;
                    _pick.Tus_base_itm_line = (Int32)tr["itri_line_no"];
                    _list.Add(_pick);

                }
            }

            return _list;
        }

        public List<ReptPickSerials> GetOutwarditems1(string _reqNo)
        {

            List<ReptPickSerials> _list = new List<ReptPickSerials>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_req_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblIntSer", "sp_getintserbydocno", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                foreach (DataRow tr in _dtResults.Rows)
                {
                    ReptPickSerials _pick = new ReptPickSerials();
                    //ITS_SEQ_NO
                    //ITS_ITM_LINE//
                    //ITS_BATCH_LINE//
                    //ITS_SER_LINE
                    //ITS_DIRECT
                    //ITS_DOC_NO//
                    //ITS_DOC_DT
                    //ITS_COM
                    //ITS_LOC
                    //ITS_BIN//
                    //ITS_ITM_CD
                    //ITS_ITM_STUS
                    //ITS_UNIT_COST
                    //ITS_SER_ID
                    //ITS_SER_1
                    //ITS_SER_2
                    //ITS_SER_3
                    //ITS_SER_4
                    //ITS_WARR_NO
                    //ITS_WARR_PERIOD
                    //ITS_ORIG_GRNCOM
                    //ITS_ORIG_GRNNO
                    //ITS_ORIG_GRNDT
                    //ITS_ORIG_SUPP
                    //ITS_EXIST_GRNCOM
                    //ITS_EXIST_GRNNO
                    //ITS_EXIST_GRNDT
                    //ITS_EXIST_SUPP
                    //ITS_CROSS_SEQNO
                    //ITS_CROSS_ITMLINE
                    //ITS_CROSS_BATCHLINE
                    //ITS_CROSS_SERLINE
                    //ITS_ISSUE_DT
                    //ITS_PICK


                    _pick.Tus_base_doc_no = (string)tr["ITS_DOC_NO"];
                    _pick.Tus_base_itm_line = (Int16)tr["ITS_ITM_LINE"];
                    _pick.Tus_batch_line = (Int16)tr["ITS_BATCH_LINE"];
                    _pick.Tus_bin = (string)tr["ITS_BIN"];
                    //_pick.Tus_com =(string)



                    _pick.Tus_itm_cd = (string)tr["itri_itm_cd"];
                    _pick.Tus_itm_desc = (string)tr["mi_longdesc"];
                    _pick.Tus_itm_model = (string)tr["mi_model"];
                    _pick.Tus_itm_stus = (string)tr["itri_itm_stus"];
                    _pick.Tus_qty = Convert.ToDecimal(tr["itri_app_qty"]);
                    _pick.Tus_base_doc_no = _reqNo;
                    _pick.Tus_base_itm_line = (Int32)tr["itri_line_no"];
                    _list.Add(_pick);

                }
            }

            return _list;
        }

        //Chamal 28/05/2012
        public DataTable GetIntSerDetails(string _docNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblIntSer", "sp_getintserbydocno", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Chamal 28/05/2012
        public DataTable GetIntBatchDetailsDecimal(string _docNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblIntBatch", "sp_getintbatchdecimalitmbydocn", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        #endregion
        //hasith 27/1/2016
        public DataTable GetGVDetails(DateTime _fromdate, DateTime _todate, string _com, string _pc)
        {
            // hasith 25/01/2015         
            OracleParameter[] param = new OracleParameter[5];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("p_fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromdate.Date;
            (param[2] = new OracleParameter("p_tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate.Date;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "sp_getgvdetails", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        //kapila 30/5/2012
        #region Manual Document
        public DataTable GetManualDocs(string _LocCode, Int16 _IsRec)
        {

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _LocCode;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            if (_IsRec == 0)  //P books
            {
                DataTable _dtResults = QueryDataTable("tblManDocs", "sp_get_manual_docs", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            else if (_IsRec == 3) //  //T books   ADD BY SHANI
            {

                DataTable _dtResults = QueryDataTable("tblManDocs", "sp_get_transferd_man_docs", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            else if (_IsRec == 2) ////P AND T books    ADD BY SHANI
            {

                DataTable _dtResults = QueryDataTable("tblManDocs", "sp_get_manual_docs", CommandType.StoredProcedure, false, param);


                OracleParameter[] param2 = new OracleParameter[2];
                (param2[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _LocCode;
                param2[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

                DataTable _dtResults2 = QueryDataTable("tblManDocs2", "sp_get_transferd_man_docs", CommandType.StoredProcedure, false, param2);

                _dtResults.Merge(_dtResults2);
                return _dtResults;
            }
            else if (_IsRec == 1)   //F books
            {
                DataTable _dtResults = QueryDataTable("tblManDocs", "sp_get_manual_docs_rec", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            else
            {
                return null;
            }


        }

        public List<ManualDocDetail> GetManualDocDet(string _User)
        {
            List<ManualDocDetail> _ManDocList = null;
            ManualDocDetail _manualDocDet = null;

            OracleParameter[] param = new OracleParameter[2];
            // (param[0] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblManualDocDet", "SP_GET_MANUAL_DOC_DET", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _ManDocList = new List<ManualDocDetail>();

                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    _manualDocDet = new ManualDocDetail();

                    //_manualDocDet.MDD_LINE = Convert.ToInt32(_dtResults.Rows[i]["MDD_LINE"]);
                    //_manualDocDet.MDD_ITM_CD = _dtResults.Rows[i]["MDD_ITM_CD"].ToString();
                    //_manualDocDet.MDD_PREFIX = _dtResults.Rows[i]["MDD_PREFIX"].ToString();
                    //_manualDocDet.MDD_BK_NO = _dtResults.Rows[i]["MDD_BK_NO"].ToString();
                    //_manualDocDet.MDD_BK_TP = _dtResults.Rows[i]["MDD_BK_TP"].ToString();
                    //_manualDocDet.MDD_FIRST = Convert.ToInt32(_dtResults.Rows[i]["MDD_FIRST"]);
                    //_manualDocDet.MDD_LAST = Convert.ToInt32(_dtResults.Rows[i]["MDD_LAST"]);
                    //_manualDocDet.MDD_CNT = Convert.ToInt32(_dtResults.Rows[i]["MDD_CNT"]);
                    //_manualDocDet.MDD_ISSUE_BY = _dtResults.Rows[i]["MDD_ISSUE_BY"].ToString();
                    //_manualDocDet.MDD_USER = _dtResults.Rows[i]["MDD_USER"].ToString();

                    _manualDocDet.Mdd_line = Convert.ToInt32(_dtResults.Rows[i]["MDD_LINE"]);
                    _manualDocDet.Mdd_itm_cd = _dtResults.Rows[i]["MDD_ITM_CD"].ToString();
                    _manualDocDet.Mdd_prefix = _dtResults.Rows[i]["MDD_PREFIX"].ToString();
                    _manualDocDet.Mdd_bk_no = _dtResults.Rows[i]["MDD_BK_NO"].ToString();
                    _manualDocDet.Mdd_bk_tp = _dtResults.Rows[i]["MDD_BK_TP"].ToString();
                    _manualDocDet.Mdd_first = Convert.ToInt32(_dtResults.Rows[i]["MDD_FIRST"]);
                    _manualDocDet.Mdd_last = Convert.ToInt32(_dtResults.Rows[i]["MDD_LAST"]);
                    _manualDocDet.Mdd_cnt = Convert.ToInt32(_dtResults.Rows[i]["MDD_CNT"]);
                    _manualDocDet.Mdd_issue_by = _dtResults.Rows[i]["MDD_ISSUE_BY"].ToString();
                    _manualDocDet.Mdd_user = _dtResults.Rows[i]["MDD_USER"].ToString();

                    _ManDocList.Add(_manualDocDet);
                }
            }
            return _ManDocList;
        }

        public Int16 SavePickedManualDocDet(string _refNo, string _Loc, string _user, string _Status)
        {
            Int16 rows_affected = 0;

            OracleCommand CMD = new OracleCommand("sp_update_temp_man_doc", oConnection_FMS);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.Add("p_ref", OracleDbType.Varchar2).Value = _refNo;
            CMD.Parameters.Add("p_loc", OracleDbType.Varchar2).Value = _Loc;
            CMD.Parameters.Add("p_user", OracleDbType.Varchar2).Value = _user;
            CMD.Parameters.Add("p_status", OracleDbType.Varchar2).Value = _Status;
            CMD.ExecuteNonQuery();

            return rows_affected;

        }

        public Int16 UpdateManualDocs(string _RefNo, string _USer)
        {
            Int16 rows_affected = 0;

            return rows_affected;
        }

        public Int32 UpdateTransferStatus(string _RefNo, string _USer, string _TransLoc)
        {
            Int32 rows_affected = 0;

            return rows_affected;

        }

        public Int16 DeleteSelectedItem(Int32 _BkNo, string _prefix, string _USer)
        {
            Int16 rows_affected = 0;

            OracleCommand CMD = new OracleCommand("sp_del_temp_man_doc_det", oConnection_FMS);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.Add("p_bkno", OracleDbType.Int32).Value = _BkNo;
            CMD.Parameters.Add("p_prefix", OracleDbType.NVarchar2).Value = _prefix;
            CMD.Parameters.Add("p_user", OracleDbType.Varchar2).Value = _USer;

            CMD.ExecuteNonQuery();


            return rows_affected;
        }

        public Int16 DeleteTempPickSer(string _docno)
        {
            Int16 rows_affected = 0;

            OracleCommand CMD = new OracleCommand("sp_del_temp_pick_ser", oConnection_FMS);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.Add("p_docno", OracleDbType.NVarchar2).Value = _docno;

            CMD.ExecuteNonQuery();


            return rows_affected;
        }

        public int Get_Manual_Doc_Serial_List(string _RefNo, string _User, Int32 _UserSeqNo, string _defBin, string _Comp, string _Loc)
        {

            update_manual_serial(_RefNo, _User, _UserSeqNo, _defBin, _Comp, _Loc);

            return 1;
        }
        #endregion

        //kapila 18/6/2012
        public List<ReptPickSerials> Get_all_Serial_details(string _RefNo, string _USer, string _Comp, string _Loc)
        {
            string _sql = string.Empty;
            List<ReptPickSerials> _list = new List<ReptPickSerials>();

            _sql = "select * from temp_man_doc_det where mdd_ref=:refno and mdd_user=:username";
            OracleCommand oCom = new OracleCommand(_sql, oConnection_FMS);
            oCom.Parameters.Add("refno", _RefNo);
            oCom.Parameters.Add("username", _USer);

            OracleDataReader DR = oCom.ExecuteReader();
            while (DR.Read())
            {
                Int32 _count = 0;
                Int32 _nextNo = Convert.ToInt32(DR["MDD_FIRST"]);
                Int32 _CNT = Convert.ToInt32(DR["MDD_CNT"]);

                while (_count < _CNT)
                {
                    _sql = "select * from INR_SER where ins_com=:comp and ins_loc=:loc and ins_itm_cd=:item and ins_ser_1=:serial and ins_available=1";
                    OracleCommand oCom1 = new OracleCommand(_sql, oConnection_FMS);
                    oCom1.Parameters.Add("comp", _Comp);
                    oCom1.Parameters.Add("loc", _Loc);
                    oCom1.Parameters.Add("item", Convert.ToString(DR["mdd_itm_cd"]));
                    oCom1.Parameters.Add("serial", _nextNo);

                    OracleDataReader DR1 = oCom1.ExecuteReader();
                    while (DR1.Read())
                    {

                        ReptPickSerials _reptPickSerials = new ReptPickSerials();

                        DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;

                        _reptPickSerials.Tus_batch_line = Convert.ToInt32(DR1["INS_BATCH_LINE"]);
                        _reptPickSerials.Tus_bin = Convert.ToString(DR1["INS_BIN"]);
                        _reptPickSerials.Tus_com = Convert.ToString(DR1["INS_COM"]);
                        //    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();//not sure
                        _reptPickSerials.Tus_cre_dt = _date;
                        _reptPickSerials.Tus_doc_dt = Convert.ToDateTime(DR1["INS_DOC_DT"]);
                        _reptPickSerials.Tus_doc_no = Convert.ToString(DR1["INS_DOC_NO"]);
                        _reptPickSerials.Tus_exist_grncom = Convert.ToString(DR1["INS_EXIST_GRNCOM"]);//not sure
                        //    _reptPickSerials.Tus_exist_grndt = Convert.ToDecimal(DR1["INS_EXIST_GRNDT"]);//not sure
                        _reptPickSerials.Tus_exist_grnno = Convert.ToString(DR1["INS_EXIST_GRNNO"]);
                        _reptPickSerials.Tus_exist_supp = Convert.ToString(DR1["INS_EXIST_SUPP"]);//TODO : GET SUPPLIER
                        _reptPickSerials.Tus_itm_cd = Convert.ToString(DR1["INS_ITM_CD"]);
                        _reptPickSerials.Tus_itm_line = Convert.ToInt32(DR1["INS_ITM_LINE"]);
                        _reptPickSerials.Tus_itm_stus = Convert.ToString(DR1["INS_ITM_STUS"]);//or ddl selected status?
                        _reptPickSerials.Tus_loc = Convert.ToString(DR1["INS_LOC"]);//not sure
                        _reptPickSerials.Tus_orig_grncom = Convert.ToString(DR1["INS_ORIG_GRNCOM"]);
                        _reptPickSerials.Tus_orig_grndt = Convert.ToDateTime(DR1["INS_ORIG_GRNDT"]);
                        _reptPickSerials.Tus_orig_grnno = Convert.ToString(DR1["INS_EXIST_GRNNO"]);
                        _reptPickSerials.Tus_orig_supp = Convert.ToString(DR1["INS_ORIG_SUPP"]);//TODO : GET SUPPLIER
                        _reptPickSerials.Tus_qty = 1;// Convert.ToDecimal(txtQty.Text.Trim());//not sure
                        _reptPickSerials.Tus_seq_no = Convert.ToInt32(DR1["INS_SEQ_NO"]);
                        _reptPickSerials.Tus_ser_1 = Convert.ToString(DR1["INS_SER_1"]);
                        _reptPickSerials.Tus_ser_2 = Convert.ToString(DR1["INS_SER_2"]);
                        _reptPickSerials.Tus_ser_3 = Convert.ToString(DR1["INS_SER_3"]);
                        _reptPickSerials.Tus_ser_4 = Convert.ToString(DR1["INS_SER_4"]);
                        _reptPickSerials.Tus_ser_id = Convert.ToInt32(DR1["INS_SER_ID"]);
                        _reptPickSerials.Tus_ser_line = Convert.ToInt32(DR1["INS_SER_LINE"]);
                        // _reptPickSerials.Tus_session_id = GlbUserSessionID;//not sure
                        _reptPickSerials.Tus_unit_cost = Convert.ToDecimal(DR1["INS_UNIT_COST"]);
                        _reptPickSerials.Tus_unit_price = Convert.ToDecimal(DR1["INS_UNIT_PRICE"]);
                        //   _reptPickSerials.Tus_usrseq_no = (int)generated_seq;//not sure
                        _reptPickSerials.Tus_warr_no = Convert.ToString(DR1["INS_WARR_NO"]);
                        _reptPickSerials.Tus_warr_period = Convert.ToInt32(DR1["INS_WARR_PERIOD"]);

                        _list.Add(_reptPickSerials);
                    }
                    _nextNo = _nextNo + 1;
                    _count = _count + 1;
                    DR1.Close();
                }
            }
            return _list;
        }

        public Int16 SavePickedItemSerials(ReptPickSerials _scanserNew)
        {
            Int16 rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[39];
                (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
                (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_no;
                (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_seq_no;
                (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
                (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_line;
                (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_line;
                (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_dt;
                (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_com;
                (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_loc;
                (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin;
                (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
                (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
                (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
                (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
                (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_id;
                (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_1;
                (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_2;
                (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_3;
                (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_4;
                (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_no;
                (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_period;
                (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grncom;
                (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grnno;
                (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grndt;
                (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_supp;
                (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grncom;
                (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grnno;
                (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grndt;
                (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_supp;
                (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
                (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_session_id;
                (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;
                (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_status;
                (param[33] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_doc_no;
                (param[34] = new OracleParameter("p_base_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_itm_line;
                (param[35] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_desc;
                (param[36] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_model;
                (param[37] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_brand;


                param[38] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
                //Open the connection and call the stored procedure.

                rows_affected = (Int16)this.UpdateRecords("sp_pickser", CommandType.StoredProcedure, param);

            }
            catch (Exception e)
            {
                rows_affected = -1;
                throw new Exception(e.Message);
            }

            return rows_affected;
        }

        public Int16 DeleteTempPickObjs(Int32 _seqno)
        {
            Int16 rows_affected = 0;

            //Create parameters and assign values.
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _seqno;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);

            rows_affected = (Int16)this.UpdateRecords("sp_deletepickobjs", CommandType.StoredProcedure, param);



            return rows_affected;
        }


        #region HP Module
        //Written by Shani 19-06-2012
        public List<GntManualDocument> Get_valid_Man_ReceiptNo()
        {
            List<GntManualDocument> _userList = new List<GntManualDocument>();

            OracleParameter[] param = new OracleParameter[1];
            //(param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = UserID;
            //(param[1] = new OracleParameter("p_comcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Comp;
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserProf", "sp_get_ReceiptNo_valid", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<GntManualDocument>(_dtResults, GntManualDocument.Converter);
            }

            return _userList;
        }
        //created by Shani 22-06-2012
        public List<TempPickManualDocDet> Get_temp_collection_Man_Receipts(string user, string Loc, string prefix, Int32 receipt_seqno)
        {
            List<TempPickManualDocDet> _userList = new List<TempPickManualDocDet>();

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Loc;
            (param[2] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prefix;
            (param[3] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = receipt_seqno;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblUserProf", "sp_get_tempCollect_man_doc_det", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _userList = DataTableExtensions.ToGenericList<TempPickManualDocDet>(_dtResults, TempPickManualDocDet.Converter);
            }

            return _userList;
        }
        //created by Shani 22-06-2012
        public Int32 SaveTemp_coll_Man_doc_dt(TempPickManualDocDet tempManDocDt)
        {
            Int32 rows_affected = 0;
            try
            {
                //Create parameters and assign values.
                OracleParameter[] param = new OracleParameter[8];


                //   (param[0] = new OracleParameter("p_mdd_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_REF;
                //    (param[1] = new OracleParameter("p_mdd_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_LINE;
                //   (param[2] = new OracleParameter("p_mdd_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_ITM_CD;
                (param[0] = new OracleParameter("p_mdd_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_PREFIX;
                (param[1] = new OracleParameter("p_mdd_bk_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_BK_NO;
                //    (param[5] = new OracleParameter("p_mdd_bk_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_BK_TP;
                (param[2] = new OracleParameter("p_mdd_first", OracleDbType.Int32, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_FIRST;
                (param[3] = new OracleParameter("p_mdd_last", OracleDbType.Int32, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_LAST;
                (param[4] = new OracleParameter("p_mdd_cur", OracleDbType.Int32, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_CUR;
                //      (param[9] = new OracleParameter("p_mdd_issue_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_ISSUE_BY;
                (param[5] = new OracleParameter("p_mdd_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_USER;
                (param[6] = new OracleParameter("p_mdd_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tempManDocDt.MDD_LOC;

                param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                //Open the connection and call the stored procedure.

                rows_affected = (Int32)this.UpdateRecords("sp_save_TEMP_COL_MAN_DOC_DET", CommandType.StoredProcedure, param);
                return rows_affected;

            }
            catch (Exception e)
            {
                rows_affected = -1;
                throw new Exception(e.Message);
            }
        }

        #endregion HP Module

        //kapila 21/6/2012
        public Boolean IsValidManualDocument(string _Comp, string _Loc, string _DocType, Int32 _NextNo)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param[2] = new OracleParameter("p_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NextNo;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_is_valid_manual_no", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        // Nadeeka 12-09-2015
        public Boolean IsValidManualDocument_prefix(string _Comp, string _Loc, string _DocType, Int32 _NextNo, string _prefix )
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param[2] = new OracleParameter("p_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _NextNo;
            (param[3] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "sp_is_valid_manual_no_prefix", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        //kapila 21/6/2012
        public Int32 GetNextManualDocNo(string _Comp, string _Loc, string _DocType)
        {

            Int32 _NextNo = 0;

            OracleParameter[] param1 = new OracleParameter[4];

            (param1[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Comp;
            (param1[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param1[2] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;

            param1[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            _NextNo = (Int32)UpdateRecords("sp_getnextmanualdocno", CommandType.StoredProcedure, param1);

            return _NextNo;

        }

        //kapila 26/2/2013
        public Int32 Update_Manual_DocNo(string _Loc, string _DocType, Int32 _DocNo, string _TxnNo)
        {

            OracleParameter[] param1 = new OracleParameter[5];
            Int32 effects = 0;

            (param1[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param1[1] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param1[2] = new OracleParameter("p_docno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _DocNo;
            (param1[3] = new OracleParameter("p_txnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TxnNo;


            param1[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_update_manual_docno", CommandType.StoredProcedure, param1);


            //update document pages
            DateTime _cDate = DateTime.Now.Date;
            DataTable _result = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_txnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TxnNo;
            (param[1] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _cDate;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param[4] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocNo;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _result = QueryDataTable("tbl", "sp_update_man_doc_pages", CommandType.StoredProcedure, false, param);


            return 1;

        }

        public Int32 Update_Manual_DocNo_prefix(string _Loc, string _DocType, Int32 _DocNo, string _TxnNo ,string _prefix)
        {

            OracleParameter[] param1 = new OracleParameter[6];
            Int32 effects = 0;

            (param1[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param1[1] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param1[2] = new OracleParameter("p_docno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _DocNo;
            (param1[3] = new OracleParameter("p_txnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TxnNo;
            (param1[4] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;

            param1[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_update_manual_docno_prefix", CommandType.StoredProcedure, param1);


            //update document pages
            DateTime _cDate = DateTime.Now.Date;
            DataTable _result = null;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_txnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TxnNo;
            (param[1] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _cDate;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param[4] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocNo;
            (param[5] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _result = QueryDataTable("tbl", "sp_update_man_doc_pages_prefix", CommandType.StoredProcedure, false, param);


            return 1;

        }

        //kapila 22/6/2012
        public Int32 UpdateManualDocNo(string _Loc, string _DocType, Int32 _DocNo, string _TxnNo)
        {


            OracleParameter[] param1 = new OracleParameter[5];
            Int32 effects = 0;

            (param1[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param1[1] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param1[2] = new OracleParameter("p_docno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _DocNo;
            (param1[3] = new OracleParameter("p_txnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TxnNo;

            param1[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_update_manual_docno", CommandType.StoredProcedure, param1);


            //update document pages
            DateTime _cDate = DateTime.Now.Date;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_txnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TxnNo;
            (param[1] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _cDate;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocType;
            (param[4] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DocNo;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            Int32 _effect = (Int32)UpdateRecords("sp_update_man_doc_pages", CommandType.StoredProcedure, param);


            return 1;

        }

        public Boolean Check_Temp_coll_Man_doc_dt(string _User, string _Loc, string _item, string _Prefix, Int32 _Recno, string _module)
        {
            Boolean Save_Success = true;
            try
            {

                string _BkNo = get_Manual_bookno(_User, _Loc, _item, _Prefix, _Recno);

                Boolean _IsOk = check_current_receipt_book(_Loc, _item, _Prefix, _BkNo, _Recno);
                if (_IsOk == false)
                {
                    Boolean _isCurRecSuccess = check_temp_current_receipt_book(_Loc, _item, _Prefix, _BkNo, _Recno, _User, _module);
                    if (_isCurRecSuccess == false)
                    {
                        Save_Success = false;
                        return Save_Success;
                    }
                }

                if (Save_Success == true)
                {
                    //save_temp_current_receipt_det(_User, _Loc, _item, _Prefix, _BkNo, _Recno + 1,_module);
                }

            }

            catch (Exception e)
            {
                Save_Success = false;
                throw new Exception(e.Message);
            }

            return Save_Success;
        }

        public Int32 delete_temp_current_receipt_det(string _User, string _module)
        {

            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            (param[1] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;

            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_del_tmp_cur_rec_dt", CommandType.StoredProcedure, param);
            return effects;

        }

        public Int32 delete_temp_current_receipt(string _User, string _Prefix, Int32 _RecNo)
        {

            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            (param[1] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Prefix;
            (param[2] = new OracleParameter("p_recno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _RecNo + 1;

            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_del_tmp_col_man_doc_dt", CommandType.StoredProcedure, param);
            return effects;

        }

        private void save_temp_current_receipt_det(string _User, string _Loc, string _item, string _Prefix, string _bookno, Int32 _Recno, string _module)
        {
            OracleParameter[] param = new OracleParameter[8];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Prefix;
            (param[4] = new OracleParameter("p_bookno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bookno;
            (param[5] = new OracleParameter("p_recno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _Recno;
            (param[6] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;

            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_tmp_cur_rec_dt", CommandType.StoredProcedure, param);

        }

        public int save_temp_existing_receipt_det(string _User, string _Loc, string _item, string _Prefix, Int32 _Recno, string _module)
        {
            string _sql = "";
            string _BookNo = "";

            OracleParameter[] param = new OracleParameter[5];

            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Prefix;
            (param[3] = new OracleParameter("p_recno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _Recno;

            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _result = QueryDataTable("tbl", "sp_get_manual_bookno", CommandType.StoredProcedure, false, param);

            if (_result.Rows.Count > 0)
            {
                _BookNo = Convert.ToString(_result.Rows[0]["mdd_bk_no"]);
            }

            OracleParameter[] param1 = new OracleParameter[5];

            (param1[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param1[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param1[2] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Prefix;
            (param1[3] = new OracleParameter("p_bookno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _BookNo;

            param1[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _result1 = QueryDataTable("tbl", "sp_get_cur_rec_no", CommandType.StoredProcedure, false, param1);

            if (_result1.Rows.Count > 0)
            {
                Int32 v_cur = Convert.ToInt32(_result1.Rows[0]["mdd_current"]);
                string v_bookno = Convert.ToString(_result1.Rows[0]["mdd_bk_no"]);
                Int32 v_first = Convert.ToInt32(_result1.Rows[0]["mdd_first"]);
                Int32 v_last = Convert.ToInt32(_result1.Rows[0]["mdd_last"]);

                OracleParameter[] param2 = new OracleParameter[11];
                Int32 effects = 0;

                (param2[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
                (param2[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
                (param2[2] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Prefix;
                (param2[3] = new OracleParameter("p_bookno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _BookNo;
                (param2[4] = new OracleParameter("p_cur", OracleDbType.Int32, null, ParameterDirection.Input)).Value = v_cur;
                (param2[5] = new OracleParameter("v_bookno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = v_bookno;
                (param2[6] = new OracleParameter("v_first", OracleDbType.Int32, null, ParameterDirection.Input)).Value = v_first;
                (param2[7] = new OracleParameter("v_last", OracleDbType.Int32, null, ParameterDirection.Input)).Value = v_last;
                (param2[8] = new OracleParameter("v_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
                (param2[9] = new OracleParameter("v_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;

                param2[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("sp_tmp_collect_man_doc_dt", CommandType.StoredProcedure, param2);

                Boolean _isCurRecSuccess = check_temp_current_receipt_book(_Loc, _item, _Prefix, _BookNo, _Recno, _User, _module);
                if (_isCurRecSuccess == true)
                {
                    save_temp_current_receipt_det(_User, _Loc, _item, _Prefix, _BookNo, _Recno + 1, _module);
                }
            }

            return 1;

        }

        private string get_Manual_bookno(string _User, string _Loc, string _item, string _Prefix, Int32 _Recno)
        {
            string _BookNo = "";

            OracleParameter[] param = new OracleParameter[5];

            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Prefix;
            (param[3] = new OracleParameter("p_recno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _Recno;

            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _result = QueryDataTable("tbl", "sp_get_manual_bookno", CommandType.StoredProcedure, false, param);

            if (_result.Rows.Count > 0)
            {
                _BookNo = Convert.ToString(_result.Rows[0]["mdd_bk_no"]);
            }

            return _BookNo;

        }

        private Boolean check_current_receipt_book(string _Loc, string _item, string _Prefix, string _BookNo, Int32 _Recno)
        {
            //Check_Current_Receipt_Book
            Boolean _ok = false;

            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Prefix;
            (param[3] = new OracleParameter("p_bookno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _BookNo;
            (param[4] = new OracleParameter("p_recno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _Recno;

            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_chk_cur_rec_bk", CommandType.StoredProcedure, param);

            if (effects == 1)
            {
                _ok = true;
            }
            else
            {
                _ok = false;
            }

            return _ok;
        }

        private Boolean check_temp_current_receipt_book(string _Loc, string _item, string _Prefix, string _BookNo, Int32 _Recno, string _user, string _Module)
        {
            Boolean _ok = false;

            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;
            (param[1] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Prefix;
            (param[2] = new OracleParameter("p_bookno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _BookNo;
            (param[3] = new OracleParameter("p_recno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _Recno;
            (param[4] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[5] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Module;

            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_chk_tmp_cur_rec_bk", CommandType.StoredProcedure, param);

            if (effects == 1)
            {
                _ok = false;
            }
            else
            {
                _ok = true;
            }

            return _ok;
        }

        //kapila 16/7/2012
        public Int16 SaveManualDocPages(ManualDocPages _manualDocPages)
        {
            OracleParameter[] param = new OracleParameter[14];
            Int16 effects = 0;


            (param[0] = new OracleParameter("p_MDP_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_ref;
            (param[1] = new OracleParameter("p_MDP_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_line;
            (param[2] = new OracleParameter("p_MDP_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_loc;
            (param[3] = new OracleParameter("p_MDP_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_itm_cd;
            (param[4] = new OracleParameter("p_MDP_PREFIX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_prefix;
            (param[5] = new OracleParameter("p_MDP_PAGE_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_page_no;
            (param[6] = new OracleParameter("p_MDP_TXN_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_txn_tp;
            (param[7] = new OracleParameter("p_MDP_TXN_SB_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_txn_sb_tp;
            (param[8] = new OracleParameter("p_MDP_TXN_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_txn_no;
            (param[9] = new OracleParameter("p_MDP_TXN_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_txn_dt;
            (param[10] = new OracleParameter("p_MDP_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_stus;
            (param[11] = new OracleParameter("p_MDP_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_mod_by;
            (param[12] = new OracleParameter("p_MDP_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = _manualDocPages.Mdp_mod_dt;

            param[13] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_save_man_doc_pages", CommandType.StoredProcedure, param);
            return effects;
        }

        //sachith 2012/07/19
        public DataTable Get_all_Items()
        {

            //ConnectionOpen();
            DataTable dt = new DataTable("Items");
            ConnectionOpen();

            dt = QueryDataTable("tbl_ItemCodes", "sp_getall_items", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));
            //ConnectionClose();

            return dt;
        }

        //sachith 2012/07/19
        public DataTable Get_all_manual_docs_by_type(string _loc, string _code)
        {

            //ConnectionOpen();
            DataTable dt;
            ConnectionOpen();
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_loc", OracleDbType.Varchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[1] = new OracleParameter("p_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            dt = QueryDataTable("tbl_ManDocs", "sp_get_manual_docs_by_type", CommandType.StoredProcedure, false, param);
            //ConnectionClose();

            return dt;
        }

        //sachith 2012/07/19
        public int UpdateGntManDocDt(string _loc, string _code, string _type, int _current, string _prefix, string _bookNo)
        {
            int effected = 0;
            ConnectionOpen();
            OracleParameter[] param = new OracleParameter[7];

            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[1] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            (param[3] = new OracleParameter("p_current", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _current;
            (param[4] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            (param[5] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _bookNo;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateRecords("sp_update_gnt_man_doc_dt1", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        //sachith 2012/07/19
        public int UpdateGntManDocPages(string _prefix, string _loc, string _code, int _current, int _last, string _user, string _rmk)
        {
            int effected = 0;
            ConnectionOpen();
            OracleParameter[] param = new OracleParameter[8];

            (param[0] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            (param[1] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _code;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[3] = new OracleParameter("p_current", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _current;
            (param[4] = new OracleParameter("p_last", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _last;
            (param[5] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[6] = new OracleParameter("p_rmk", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _rmk;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateRecords("sp_update_gnt_man_doc_pages", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        public int Save_manual_doc_pages(Int32 _userseqno, string _refNo)
        {
            int rows_effected = 0;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_userseqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userseqno;
            (param[1] = new OracleParameter("p_refno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _refNo;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            return rows_effected = UpdateRecords("sp_save_manual_doc_pages", CommandType.StoredProcedure, param);

        }

        public DataTable Get_manual_docs_ByRef(string _ref)
        {
            //(p_ref in NVARCHAR2,c_data OUT sys_refcursor)
            OracleParameter[] param = new OracleParameter[2];
            DataTable _dtResults = null;
            (param[0] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;

            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            //Query Data base.         
            _dtResults = QueryDataTable("tblReturnCheque", "sp_get_manual_docs_ByRef", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public Int32 UpdateTransferStatus_NEW(string _RefNo, string _USer, string _TransLoc, string userLoc)
        {
            //new version of Kapila's old method - UpdateTransferStatus

            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _RefNo;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _USer;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _TransLoc;
            (param[3] = new OracleParameter("p_userloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userLoc;


            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_updatetransferstatus", CommandType.StoredProcedure, param);
            return effects;
        }

        public DataTable GetTempManualDocDet(string _User)
        {
            //(p_user in string,c_data OUT sys_refcursor)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _User;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblManualDocDet", "SP_GET_MANUAL_DOC_DET", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public Int16 SavePickedManualDocDet_TRNS(string _refNo, string _Loc, string _user, string _Status)
        {
            Int16 rows_affected = 0;

            OracleCommand CMD = new OracleCommand("sp_update_temp_man_doc_TRNS", oConnection_FMS);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.Add("p_ref", OracleDbType.Varchar2).Value = _refNo;
            CMD.Parameters.Add("p_loc", OracleDbType.Varchar2).Value = _Loc;
            CMD.Parameters.Add("p_user", OracleDbType.Varchar2).Value = _user;
            CMD.Parameters.Add("p_status", OracleDbType.Varchar2).Value = _Status;
            CMD.ExecuteNonQuery();

            return rows_affected;

        }

        public Int32 SaveManualDocDet(ManualDocDetail _mandoc)
        {
            OracleParameter[] param = new OracleParameter[22];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_ref;
            (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _mandoc.Mdd_line;
            (param[2] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_itm_cd;
            (param[3] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_prefix;
            (param[4] = new OracleParameter("p_bk_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_bk_no;
            (param[5] = new OracleParameter("p_bk_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_bk_tp;
            (param[6] = new OracleParameter("p_first", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _mandoc.Mdd_first;
            (param[7] = new OracleParameter("p_last", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _mandoc.Mdd_last;
            (param[8] = new OracleParameter("p_cnt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _mandoc.Mdd_cnt;
            (param[9] = new OracleParameter("p_issue_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_issue_by;
            (param[10] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_status;
            (param[11] = new OracleParameter("p_current", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _mandoc.Mdd_current;
            (param[12] = new OracleParameter("p_using", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _mandoc.Mdd_using;
            (param[13] = new OracleParameter("p_used", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _mandoc.Mdd_used;
            (param[14] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_loc;
            (param[15] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _mandoc.Mdd_dt;
            (param[16] = new OracleParameter("p_issue_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_issue_loc;
            (param[17] = new OracleParameter("p_rem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_rem;
            (param[18] = new OracleParameter("p_receive_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _mandoc.Mdd_receive_dt;
            (param[19] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_user;
            (param[20] = new OracleParameter("p_trans_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mandoc.Mdd_trans_loc;

            param[21] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_gnt_man_doc_dt", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<ManualDocDetail> Get_manual_doc(string p_ref, string p_itmCd, string p_prifix, string p_bookNo)
        {
            List<ManualDocDetail> _list = null;
            //(p_ref in NVARCHAR2,p_itmCd in NVARCHAR2,p_prifix in NVARCHAR2,p_bookNo in NUMBER,c_data OUT sys_refcursor)
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_ref;
            (param[1] = new OracleParameter("p_itmCd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_itmCd;
            (param[2] = new OracleParameter("p_prifix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_prifix;
            (param[3] = new OracleParameter("p_bookNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_bookNo;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblManDoc = QueryDataTable("_tblBalance", "sp_get_manual_doc", CommandType.StoredProcedure, false, param);
            if (_tblManDoc.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ManualDocDetail>(_tblManDoc, ManualDocDetail.Converter);
            }
            return _list;


        }
        public Int32 UpdateManualDocs_NEW(string _RefNo, string _USer, string location)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _RefNo;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _USer;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;


            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_updatemanualdocs_new", CommandType.StoredProcedure, param);
            return effects;

        }

        private ReptPickSerials GiftVoucher(DataRow _row)
        {
            ReptPickSerials _one = new ReptPickSerials();
            //gvp_ref,gvp_book,gvp_page,gvp_line,gvp_pc,gvp_gv_cd,gvp_gv_prefix,gvp_gv_tp,
            //gvp_com
            _one.Tus_com = _row.Field<string>("gvp_com");
            _one.Tus_itm_cd = _row.Field<string>("gvp_gv_cd");
            _one.Tus_itm_line = _row.Field<int>("gvp_line");
            _one.Tus_itm_stus = "GOD";
            _one.Tus_loc = _row.Field<string>("gvp_pc");
            _one.Tus_qty = 1;
            _one.Tus_ser_1 = Convert.ToString(_row.Field<Int64>("gvp_page"));
            _one.Tus_ser_2 = Convert.ToString(_row.Field<Int64>("gvp_book"));
            _one.Tus_ser_3 = _row.Field<string>("gvp_gv_prefix");
            _one.Tus_ser_4 = _row.Field<string>("gvp_gv_tp");
            _one.Tus_ser_id = Convert.ToInt32(_row.Field<Int64>("gvp_page"));
            if (_row["gvp_amt"] != DBNull.Value)
                _one.Tus_unit_price = _row.Field<decimal>("gvp_amt");
            else _one.Tus_unit_price = 0;
            if (_row["gvp_bal_amt"] != DBNull.Value)
                _one.Tus_unit_cost = _row.Field<decimal>("gvp_bal_amt");
            else
                _one.Tus_unit_cost = 0;
            return _one;
        }
        public List<ReptPickSerials> GetAvailableGiftVoucher(string _company, string _profitcenter, string _item)
        {
            //SP_GETAVAILABLEPAGE (p_com in NVARCHAR2,p_pc in NVARCHAR2,p_item in NVARCHAR2,c_data out sys_refcursor)
            List<ReptPickSerials> _one = new List<ReptPickSerials>();
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _table = QueryDataTable("_tblBalance", "sp_getavailablepage", CommandType.StoredProcedure, false, param);

            if (_table != null)
                if (_table.Rows.Count > 0)
                    foreach (DataRow _row in _table.Rows)
                        _one.Add(GiftVoucher(_row));

            return _one;
        }

        public DataTable GetAvailable_GV_books( Int32 _book, string _item,string _company)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _table = QueryDataTable("_tblBalance", "sp_getbook_by_book", CommandType.StoredProcedure, false, param);
            return _table;
        }

        public ReptPickSerials GetGiftVoucherDetail(string _company, string _profitcenter, string _item, Int32 _book, Int32 _page, string _prefix)
        {
            //sp_getpagedetail(p_com in NVARCHAR2,p_pc in NVARCHAR2,p_item in NVARCHAR2,p_book in NUMBER,p_page in NUMBER,c_data out sys_refcursor)
            ReptPickSerials _one = new ReptPickSerials();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[4] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[5] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _table = QueryDataTable("_tblBalance", "sp_getpagedetail", CommandType.StoredProcedure, false, param);

            if (_table != null)
                if (_table.Rows.Count > 0)
                    foreach (DataRow _row in _table.Rows)
                        _one = GiftVoucher(_row);

            return _one;
        }


        public DataTable SearchGiftVoucher(string _initialSearchParams, string _searchCatergory, string _searchText)
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

            string _refNo = null;
            string _page = null;
            string _status = null;
            string _book = null;

            //Set relavant parameters according to the,search catergory.
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "REFERENCE":
                        _refNo = _searchText;
                        break;
                    case "PAGE":
                        _page = _searchText;
                        break;
                    case "STATUS":
                        _status = _searchText;
                        break;
                    case "BOOK":
                        _book = _searchText;
                        break;
                    default:
                        break;
                }
            }

            //Modify parameter values for LIKE search.
            _refNo = (_refNo != null) ? (_refNo.ToUpper() + "%") : "%";
            _page = (_page != null) ? (_page.ToUpper() + "%") : "%";
            _status = (_status != null) ? (_status.ToUpper() + "%") : "%";
            _book = (_book != null) ? (_book.ToUpper() + "%") : "%";

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _refNo;
            (param[1] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _page;
            (param[2] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0].ToString();
            (param[4] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[5] = new OracleParameter("p_item", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(searchParams[1]);
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblInvoiceSearch", "sp_search_gv", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }


        public DataTable SearchGiftVoucherByPage(string _initialSearchParams, string _searchCatergory, string _searchText)
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


            string _page = null;
            string _book = null;

            //Set relavant parameters according to the,search catergory.
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {

                    case "PAGE":
                        _page = _searchText;
                        break;
                    case "BOOK":
                        _book = _searchText;
                        break;
                    default:
                        break;
                }
            }

            //Modify parameter values for LIKE search.
            _page = (_page != null) ? (_page.ToUpper() + "%") : "%";
            _book = (_book != null) ? (_book.ToUpper() + "%") : "%";

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0].ToString();
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[1].ToString();
            (param[2] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[2].ToString();
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[3].ToString();
            (param[4] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[5] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _page;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblInvoiceSearch", "sp_searchgvbypage", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public List<GiftVoucherPages> GetAllGvbyPages(string _com, string _pc, string _stus, string _itm, Int32 _page)
        {
            OracleParameter[] param = new OracleParameter[6];
            List<GiftVoucherPages> _gift = null;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[4] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_searchallgvbypage", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter);
            }
            return _gift;
        }

        public List<GiftVoucherPages> GetGiftVoucherPages(string _com, int _page)
        {
            OracleParameter[] param = new OracleParameter[3];
            List<GiftVoucherPages> _gift = null;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_get_gv", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter);
            }
            return _gift;
        }

        public DataTable GetLastNoSeqPageDetails(DateTime _asAtDate, string _Loc)
        {
            // Sanjeewa 06-05-2013         
            OracleParameter[] param = new OracleParameter[3];
            param[0] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("IN_AS_AT_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _asAtDate.Date;
            (param[2] = new OracleParameter("IN_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Loc;

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "sp_get_manual_doc_details1", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public DataTable GetGVPrintDetails(string _com, string _pc, int _book, int _fpage, int _tpage)
        {
            // Sanjeewa 23-11-2013         
            OracleParameter[] param = new OracleParameter[6];
            param[0] = new OracleParameter("n_Cursor", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[3] = new OracleParameter("in_book", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _book;
            (param[4] = new OracleParameter("in_fpage", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _fpage;
            (param[5] = new OracleParameter("in_tpage", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _tpage;

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "sp_get_gv_print", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        public int UpdateGiftVpucherPagesBalance(string _com, int _book, string _item, int _page, string _modBy, DateTime _modDt, decimal _amount)
        {

            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_amount", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _amount;
            (param[1] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[2] = new OracleParameter("p_item", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[5] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _modBy;
            (param[6] = new OracleParameter("p_mod_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _modDt;


            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_update_gv_balance", CommandType.StoredProcedure, param);
            return effects;

        }

        public DataTable GetDetailByGiftVoucher(string _company, string _profitcenter, int _page, string _type)
        {
            // sp_getdetailfrompage(p_com in NVARCHAR2,p_pc in NVARCHAR2,p_page in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[2] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[3] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblGv", "sp_getdetailfrompage", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }


        public DataTable GetDetailByPageNItem(string _company, string _profitcenter, int _page, string _item)
        {
            //sp_getpagebyitemandpage(p_com in NVARCHAR2,p_pc in NVARCHAR2,p_page in NVARCHAR2,p_item in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[2] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblGv", "sp_getpagebyitemandpage", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public List<GiftVoucherItems> GetGiftVoucherItems(string _com, int _page)
        {
            OracleParameter[] param = new OracleParameter[3];
            List<GiftVoucherItems> _gift = null;
            (param[0] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;

            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_get_gv_items", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherItems>(_dtResults, GiftVoucherItems.Converter);
            }
            return _gift;

        }

        public DataTable SearchAvailableGiftVoucher(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            DataTable _dtResults = null;


            if (_initialSearchParams.Contains(":"))
            {
                string[] arr = _initialSearchParams.Split(new string[] { ":" }, StringSplitOptions.None);
                _initialSearchParams = arr[1];
            }


            string[] seperator = new string[] { "|" };
            string[] searchParams = _initialSearchParams.Split(seperator, StringSplitOptions.None);

            string _page = null;
            string _book = null;
            string _item = null;
            string _description = null;
            string _model = null;


            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "PAGE":
                        _page = _searchText;
                        break;
                    case "BOOK":
                        _book = _searchText;
                        break;
                    case "ITEM":
                        _item = _searchText;
                        break;
                    case "DESCRIPTION":
                        _description = _searchText;
                        break;
                    case "MODEL":
                        _model = _searchText;
                        break;
                    default:
                        break;
                }
            }


            _page = (_page != null) ? (_page.ToUpper() + "%") : "%";
            _book = (_book != null) ? (_book.ToUpper() + "%") : "%";
            _item = (_item != null) ? (_item.ToUpper() + "%") : "%";
            _description = (_description != null) ? (_description.ToUpper() + "%") : "%";
            _model = (_model != null) ? (_model.ToUpper() + "%") : "%";


            //sp_searchavailablegv(p_com in NVARCHAR2,p_pc in NVARCHAR2,p_type in NVARCHAR2,p_page in NUMBER,p_book in NUMBER,p_item in NVARCHAR2,p_desc in NVARCHAR2,p_model in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0].ToString();
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[1].ToString();
            (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[2].ToString();

            (param[3] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _page;
            (param[4] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[5] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[6] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _description;
            (param[7] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _model;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            _dtResults = QueryDataTable("tblSearch", "sp_searchavailablegv", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Written By Prabhath on 09/05/2013 -(Not expose to client)
        public Int32 UpdateGiftVoucherByInvoice(GiftVoucherPages _voucher, int _month)
        {
            Int32 _effect = 0;
            //sp_updategvfrominvoice(            //p_com in NVARCHAR2,            //p_pc in NVARCHAR2,            //p_item in NVARCHAR2,            //p_book in NVARCHAR2,            //p_page in NVARCHAR2,            //p_customer in NVARCHAR2,            //p_name in NVARCHAR2,            //p_address1 in NVARCHAR2,            //p_address2 in NVARCHAR2,            //p_mobile in NVARCHAR2,            //p_validfrom in date,            //p_period in NUMBER,            //p_invoice in NVARCHAR2,            //p_moddate in date,            //p_issuedate in date,            //p_issueby in NVARCHAR2,            //p_modby in NVARCHAR2,            //o_effect out NUMBER)
            //(p_com IN NVARCHAR2,p_pc IN NVARCHAR2,p_item IN NVARCHAR2,p_book IN NUMBER,p_page IN NUMBER,p_customer IN NVARCHAR2, p_name IN NVARCHAR2,p_address1 IN NVARCHAR2,p_address2 IN NVARCHAR2,p_mobile IN NVARCHAR2,p_validto IN DATE,p_invoice IN NVARCHAR2,p_moddate IN DATE, p_issuedate   IN     DATE,                                  p_issueby     IN     NVARCHAR2,                                  p_modby       IN     NVARCHAR2,                                  o_effect         OUT NUMBER)
            OracleParameter[] param = new OracleParameter[17];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_pc;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_gv_cd;
            (param[3] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_book;
            (param[4] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_page;
            (param[5] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_cd;
            (param[6] = new OracleParameter("p_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_name;
            (param[7] = new OracleParameter("p_address1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_add1;
            (param[8] = new OracleParameter("p_address2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_add2;
            (param[9] = new OracleParameter("p_mobile", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_mob;
            (param[10] = new OracleParameter("p_validto", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(_voucher.Gvp_issue_dt).AddMonths(_month);
            (param[11] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_oth_ref;
            (param[12] = new OracleParameter("p_moddate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_mod_dt;
            (param[13] = new OracleParameter("p_issuedate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_issue_dt;
            (param[14] = new OracleParameter("p_issueby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_issue_by;
            (param[15] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_mod_by;
            param[16] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_updategvfrominvoice", CommandType.StoredProcedure, param);
            return _effect;
        }




        public int UpdateGiftVpucherPagesBalance(string _com, int _page, int _value, string _item)
        {

            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_value", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _value;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_update_gv_item_balance", CommandType.StoredProcedure, param);
            return effects;

        }

        public GiftVoucherPages GetGiftVoucherPage(string _com, string _pc, string _item, int _book, int _page, string _prefix)
        {

            GiftVoucherPages _gift = null;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[4] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[5] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _table = QueryDataTable("_tblBalance", "SP_GETAPPROVEDPAGEDETAIL", CommandType.StoredProcedure, false, param);
            if (_table.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_table, GiftVoucherPages.Converter)[0];
            }
            return _gift;

        }

        //darshana on 11-05-2013
        public DataTable GetAvailableGvBooks(string _com, string _pc, string _tp, string _status, string _itm,string _prefix)
        {
            OracleParameter[] param = new OracleParameter[7];
            //List<GiftVoucherPages> _gift = null;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tp;
            (param[3] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[4] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[5] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_search_gbbook_new", CommandType.StoredProcedure, false, param);
            //if (_dtResults.Rows.Count > 0)
            //{
            //  _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.ConverterBook);
            //}
            return _dtResults;
        }

        //darshana on 11-05-2013
        public List<GiftVoucherPages> GetAvailableGvPages(string _com, string _pc, string _tp, string _status, Int32 _book, string _gvCd)
        {
            OracleParameter[] param = new OracleParameter[7];
            List<GiftVoucherPages> _gift = null;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tp;
            (param[3] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[4] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[5] = new OracleParameter("p_gvCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _gvCd;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_search_gvpages", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter);
            }
            return _gift;
        }

        //darshana on 11-05-2013
        public List<GiftVoucherPages> GetAvailableGvPagesRange(string _com, string _pc, string _tp, string _status, Int32 _book, string _gvCd, Int32 _frmPg, Int32 _toPg)
        {
            OracleParameter[] param = new OracleParameter[9];
            List<GiftVoucherPages> _gift = null;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _tp;
            (param[3] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[4] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[5] = new OracleParameter("p_gvCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _gvCd;
            (param[6] = new OracleParameter("p_frm_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _frmPg;
            (param[7] = new OracleParameter("p_to_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _toPg;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_search_gvpages_range", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter);
            }
            return _gift;
        }

        //Written By darshana on 09/05/2013 
        public Int32 UpdateGiftVoucherByReceipt(GiftVoucherPages _voucher)
        {
            Int32 _effect = 0;

            //p_com in NVARCHAR2,
            //p_pc in NVARCHAR2,
            //p_item in NVARCHAR2,
            //p_book in NVARCHAR2,
            //p_page in NVARCHAR2,
            //p_customer in NVARCHAR2,
            //p_name in NVARCHAR2,
            //p_address1 in NVARCHAR2,
            //p_address2 in NVARCHAR2,
            //p_mobile in NVARCHAR2,
            //p_validfrom in date,
            //p_validto in date,
            //p_invoice in NVARCHAR2,
            //p_issueby in NVARCHAR2,
            //p_modby in NVARCHAR2,
            //p_amt in NUMBER,
            //p_staus in NVARCHAR2,
            //p_gvp_noof_itm in NUMBER,
            //p_from in NVARCHAR2, 
            //o_effect out NUMBER)

            OracleParameter[] param = new OracleParameter[21];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_pc;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_gv_cd;
            (param[3] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_book;
            (param[4] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_page;
            (param[5] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_cd;
            (param[6] = new OracleParameter("p_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_name;
            (param[7] = new OracleParameter("p_address1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_add1;
            (param[8] = new OracleParameter("p_address2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_add2;
            (param[9] = new OracleParameter("p_mobile", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_mob;
            (param[10] = new OracleParameter("p_validfrom", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_valid_from;
            (param[11] = new OracleParameter("p_validto", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_valid_to;
            (param[12] = new OracleParameter("p_invoice", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_oth_ref;
            (param[13] = new OracleParameter("p_issueby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_issue_by;
            (param[14] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_mod_by;
            (param[15] = new OracleParameter("p_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _voucher.Gvp_amt;
            (param[16] = new OracleParameter("p_staus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_stus;
            (param[17] = new OracleParameter("p_gvp_noof_itm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _voucher.Gvp_noof_itm;
            (param[18] = new OracleParameter("p_gvp_is_allow_promo", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _voucher.Gvp_is_allow_promo;
            (param[19] = new OracleParameter("p_from", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_from;
            param[20] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_updategvfromreceipt", CommandType.StoredProcedure, param);
            return _effect;
        }

        //written by darshana on 11-05-2013
        public List<GiftVoucherPages> GetGiftVoucherByOthRef(string _com, string _pc, string _refDoc)
        {
            OracleParameter[] param = new OracleParameter[4];
            List<GiftVoucherPages> _gift = null;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_oth_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _refDoc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_getgvbyothref", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter);
            }
            return _gift;
        }

        //darshana
        public Int32 UpdateGiftVoucherValidDate(DateTime p_validto, string p_modby, string p_book, string p_page, string p_pc, string p_item, string p_com, string p_status, Int32 p_amendNoofItems)
        {
            Int32 _effect = 0;

            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_validto", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_validto;
            (param[1] = new OracleParameter("p_modby", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_modby;
            (param[2] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_book;
            (param[3] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_page;
            (param[4] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            (param[5] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_item;
            (param[6] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[7] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_status;
            (param[8] = new OracleParameter("p_gvp_noof_itm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_amendNoofItems;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_updategvvaliddt", CommandType.StoredProcedure, param);
            return _effect;
        }

        //written by darshana on 11-05-2013
        public List<GiftVoucherItems> GetGiftVoucherAllItems(string _book, string _page, string _com, string _pc)
        {
            OracleParameter[] param = new OracleParameter[5];
            List<GiftVoucherItems> _gift = null;
            (param[0] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[1] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _page;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_getgvitmdet", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherItems>(_dtResults, GiftVoucherItems.Converter);
            }
            return _gift;
        }

        //darshana
        public Int32 UpdateGiftVoucherStatus(string p_com, string p_pc, string p_book, string p_page, string p_item, Int16 p_status)
        {
            Int32 _effect = 0;

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_pc;
            (param[2] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_book;
            (param[3] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_page;
            (param[4] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_item;
            (param[5] = new OracleParameter("p_act", OracleDbType.Int16, null, ParameterDirection.Input)).Value = p_status;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_updategvitemact", CommandType.StoredProcedure, param);
            return _effect;
        }


        //Written by Prabhath on 21/05/2013 - Not expose to client.
        public Int32 ReversVoucherBalance(string _amount, Int32 _book, string _item, Int32 _page, string _company, string _modby, DateTime _moddate)
        {
            //sp_revers_gv_balance(p_amount in NVARCHAR2,p_book in NUMBER,p_item in NVARCHAR2,p_page in Number,p_com in NVARCHAR2,p_mod_by in NVARCHAR2,p_mod_date in date,o_effect out number) IS
            Int32 _effect = 0;

            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_amount", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _amount;
            (param[1] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[5] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _modby;
            (param[6] = new OracleParameter("p_mod_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _moddate;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_revers_gv_balance", CommandType.StoredProcedure, param);
            return _effect;
        }

        //Written by Prabhath on 21/05/2013 - not expose to client
        public Int32 ReversVoucher(string _company, string _profitcenter, string _item, string _book, string _page, string _user)
        {
            //sp_reversvoucher(p_com in NVARCHAR2,p_pc in NVARCHAR2,p_item in NVARCHAR2,p_book in NVARCHAR2,p_page in NVARCHAR2,p_user in NVARCHAR2,o_effect out NUMBER)
            Int32 _effect = 0;

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[4] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _page;
            (param[5] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_reversvoucher", CommandType.StoredProcedure, param);
            return _effect;
        }

        public Int32 CreateIssueGvItems(GiftVoucherItems _GvItems)
        {
            OracleParameter[] param = new OracleParameter[16];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_gvi_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _GvItems.Gvi_com;
            (param[1] = new OracleParameter("p_gvi_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _GvItems.Gvi_pc;
            (param[2] = new OracleParameter("p_gvi_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _GvItems.Gvi_ref;
            (param[3] = new OracleParameter("p_gvi_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _GvItems.Gvi_book;
            (param[4] = new OracleParameter("p_gvi_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _GvItems.Gvi_page;
            (param[5] = new OracleParameter("p_gvi_page_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _GvItems.Gvi_page_line;
            (param[6] = new OracleParameter("p_gvi_line", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _GvItems.Gvi_line;
            (param[7] = new OracleParameter("p_gvi_pre_fix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _GvItems.Gvi_pre_fix;
            (param[8] = new OracleParameter("p_gvi_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _GvItems.Gvi_itm;
            (param[9] = new OracleParameter("p_gvi_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _GvItems.Gvi_qty;
            (param[10] = new OracleParameter("p_gvi_act", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _GvItems.Gvi_act;
            (param[11] = new OracleParameter("p_gvi_bal_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _GvItems.Gvi_bal_qty;
            (param[12] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _GvItems.Gvi_tp;
            (param[13] = new OracleParameter("p_val", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _GvItems.Gvi_val;
            (param[14] = new OracleParameter("p_verb", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _GvItems.Gvi_verb;
            param[15] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_savegvissueitem", CommandType.StoredProcedure, param);
            return effects;
        }

        //Written by Prabhath on 6/7/2013 (Not exposed to client)
        public Int32 UpdateRedeemVoucher(string _company, string _profitcenter, string _book, string _page, string _prefix, string _item, string _user, decimal _amount)
        {
            //sp_updateredeemvoucher(p_com in NVARCHAR2,p_pc in NVARCHAR2,p_book in NVARCHAR2,p_page in NVARCHAR2,p_prefix in NVARCHAR2,p_item in NVARCHAR2,p_user in NVARCHAR2,o_effect out NUMBER) is
            OracleParameter[] param = new OracleParameter[9];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[2] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[3] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _page;
            (param[4] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            (param[5] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[7] = new OracleParameter("p_amount", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _amount;
            param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_updateredeemvoucher", CommandType.StoredProcedure, param);
            return effects;

        }

        public DataTable GetBookFromMannualRef(string _pc, string _ref)
        {

            List<RecieptHeader> _list = null;
            //sp_getexchangerequest(p_com in NVARCHAR2,p_loc in NVARCHAR2, p_type in NVARCHAR2,p_status in NVARCHAR2,c_data out sys_refcursor)
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[1] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;

            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblData = QueryDataTable("tbData", "sp_get_book_from_no", CommandType.StoredProcedure, false, param);
            return _tblData;


        }

        //kapila 31/3/2014
        public Int32 UpdateVouSettlement(string _company, string _profitcenter, string _book, string _page, string _prefix, string _item, string _user, string _ref, decimal _amount, DateTime _setDate)
        {

            OracleParameter[] param = new OracleParameter[11];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[2] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[3] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _page;
            (param[4] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _prefix;
            (param[5] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[7] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;
            (param[8] = new OracleParameter("p_amount", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _amount;
            (param[9] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _setDate;
            param[10] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_updatevousettlement", CommandType.StoredProcedure, param);
            return effects;

        }
        //Darshana 18-06-2014
        public Int32 UpdateGiftVouUsedStatus(string _com, string _page, string _usr, string _stus)
        {
            Int32 _effect = 0;

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_page", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _page;
            (param[2] = new OracleParameter("p_usr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _usr;
            (param[3] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            _effect = (Int32)UpdateRecords("sp_updateusedvou", CommandType.StoredProcedure, param);
            return _effect;
        }


        //Written By Chamal on 27/06/2014 
        public Int32 SaveGiftVoucherPages(GiftVoucherPages _voucher)
        {
            OracleParameter[] param = new OracleParameter[35];
            (param[0] = new OracleParameter("p_gvp_is_allow_promo", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _voucher.Gvp_is_allow_promo;
            (param[1] = new OracleParameter("p_gvp_from", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_from;
            (param[2] = new OracleParameter("p_gvp_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_ref;
            (param[3] = new OracleParameter("p_gvp_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _voucher.Gvp_book;
            (param[4] = new OracleParameter("p_gvp_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _voucher.Gvp_page;
            (param[5] = new OracleParameter("p_gvp_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _voucher.Gvp_line;
            (param[6] = new OracleParameter("p_gvp_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_pc;
            (param[7] = new OracleParameter("p_gvp_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_stus;
            (param[8] = new OracleParameter("p_gvp_gv_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_gv_cd;
            (param[9] = new OracleParameter("p_gvp_gv_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_gv_prefix;
            (param[10] = new OracleParameter("p_gvp_gv_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_gv_tp;
            (param[11] = new OracleParameter("p_gvp_cus_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_cd;
            (param[12] = new OracleParameter("p_gvp_cus_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_name;
            (param[13] = new OracleParameter("p_gvp_cus_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_add1;
            (param[14] = new OracleParameter("p_gvp_cus_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_add2;
            (param[15] = new OracleParameter("p_gvp_cus_mob", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_mob;
            (param[16] = new OracleParameter("p_gvp_valid_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_valid_from;
            (param[17] = new OracleParameter("p_gvp_valid_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_valid_to;
            (param[18] = new OracleParameter("p_gvp_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _voucher.Gvp_amt;
            (param[19] = new OracleParameter("p_gvp_bal_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _voucher.Gvp_bal_amt;
            (param[20] = new OracleParameter("p_gvp_oth_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_oth_ref;
            (param[21] = new OracleParameter("p_gvp_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_cre_dt;
            (param[22] = new OracleParameter("p_gvp_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_mod_dt;
            (param[23] = new OracleParameter("p_gvp_can_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_can_dt;
            (param[24] = new OracleParameter("p_gvp_issue_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _voucher.Gvp_issue_dt;
            (param[25] = new OracleParameter("p_gvp_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cre_by;
            (param[26] = new OracleParameter("p_gvp_can_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_can_by;
            (param[27] = new OracleParameter("p_gvp_issue_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_issue_by;
            (param[28] = new OracleParameter("p_gvp_app_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_app_by;
            (param[29] = new OracleParameter("p_gvp_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_mod_by;
            (param[30] = new OracleParameter("p_gvp_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_com;
            (param[31] = new OracleParameter("p_gvp_noof_itm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _voucher.Gvp_noof_itm;
            (param[32] = new OracleParameter("p_gvp_issu_itm", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _voucher.Gvp_issu_itm;
            (param[33] = new OracleParameter("p_gvp_cus_nic", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _voucher.Gvp_cus_nic;
            param[34] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)UpdateRecords("SP_SAVE_GNTGVPAGES", CommandType.StoredProcedure, param);
        }

        // Nadeeka
        public int UpdateVouTransfer(string _company, string _pc, string _item, Int32 _book, Int32 _page, string _user)
        {
             int effect = 0;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[3] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[4] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[5] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effect = (Int16)UpdateRecords("sp_transfer_voucher", CommandType.StoredProcedure, param);

            return effect;
        }

        //shanuka 16/09/2014
        public Int32 SaveToIssueChqBank(List<Deposit_Bank_Pc_wise> lst_chqBank)
        {
            Int32 effects = 0;
            foreach (Deposit_Bank_Pc_wise _itemDetails in lst_chqBank)
            {
                OracleParameter[] param = new OracleParameter[13];

                (param[0] = new OracleParameter("p_MDD_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Ref_lnk;
                (param[1] = new OracleParameter("p_MDD_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Line_no;
                (param[2] = new OracleParameter("p_MDD_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Mid_no;
                (param[3] = new OracleParameter("p_MDD_PREFIX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Prifix;
                (param[4] = new OracleParameter("p_MDD_BK_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.SunAccNo;
                (param[5] = new OracleParameter("p_MDD_FIRST", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Start_no;
                (param[6] = new OracleParameter("p_MDD_LAST", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Ending_no;
                (param[7] = new OracleParameter("p_MDD_CNT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.No_of_pages;

                (param[8] = new OracleParameter("p_MDD_ISSUE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Create_by;
                (param[9] = new OracleParameter("p_MDD_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Stus;
                (param[10] = new OracleParameter("p_MDD_CURRENT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Curnt;
                (param[11] = new OracleParameter("p_MDD_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.BankCode;

                param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                effects = (Int16)UpdateRecords("SP_INSERT_ISSUE_CHQ_BANK", CommandType.StoredProcedure, param);

            }

            return effects;
        }


        //shanuka 16/09/2014
        public Int32 SaveToDocPages(List<Deposit_Bank_Pc_wise> lst_docpages)
        {
            Int32 effects = 0;
            foreach (Deposit_Bank_Pc_wise _itemDetails in lst_docpages)
            {
                OracleParameter[] param = new OracleParameter[9];

                (param[0] = new OracleParameter("p_MDP_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Company;
                (param[1] = new OracleParameter("p_MDP_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Pun_tp;
                (param[2] = new OracleParameter("p_MDP_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Profit_center;
                (param[3] = new OracleParameter("p_MDP_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Price_book;
                (param[4] = new OracleParameter("p_MDP_PREFIX", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Remark;
                (param[5] = new OracleParameter("p_MDP_PAGE_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _itemDetails.Page_num;
                (param[6] = new OracleParameter("p_MDP_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Promo_p_book;
                (param[7] = new OracleParameter("p_MDP_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemDetails.Createby;

                param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                effects = (Int16)UpdateRecords("SP_INSERT_DOC_PAGES", CommandType.StoredProcedure, param);

            }

            return effects;
        }
        //shanuka 16/09/2014
        public DataTable Load_ItemSearch_details(string _initialSearchParams, string _searchCatergory, string _searchText)
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

            string _item = null;
            string _description = null;
            string _model = null;
            string _brand = null;

            //Set relavant parameters according to the,search catergory.
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "ITEM":
                        _item = _searchText;
                        break;
                    case "DESCRIPTION":
                        _description = _searchText;
                        break;
                    case "MODEL":
                        _model = _searchText;
                        break;
                    case "BRAND":
                        _brand = _searchText;
                        break;
                    default:
                        break;
                }
            }

            //Modify parameter values for LIKE search.
            _item = (_item != null) ? (_item.ToUpper() + "%") : null;
            _description = (_description != null) ? (_description.ToUpper() + "%") : null;
            _model = (_model != null) ? (_model.ToUpper() + "%") : null;
            _brand = (_brand != null) ? (_brand.ToUpper() + "%") : null;

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0].ToString();

            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _description;
            (param[3] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _model;
            (param[4] = new OracleParameter("p_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _brand;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblItemSearch", "SP_SEARCH_ITEM_DETS", CommandType.StoredProcedure, false, param);

            return _dtResults;

        }


        //shanuka 25/09/2014
        public DataTable get_unused_doc_report(DateTime from, DateTime to, string profitcen)
        {

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_FromDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = from;
            (param[1] = new OracleParameter("p_ToDate", OracleDbType.Date, null, ParameterDirection.Input)).Value = to;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = profitcen;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable tblDet = QueryDataTable("tbl_det", "SP_UNUSED_DOC_DETAILS", CommandType.StoredProcedure, false, param);
            return tblDet;

        }

        //Tharaka 2014-11-04
        public DataTable Get_Currect_By_Book(string ITEMCODE, string BOOKNUM, string LOC)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_ITEMCODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ITEMCODE;
            (param[1] = new OracleParameter("P_BOOKNUM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = BOOKNUM;
            (param[2] = new OracleParameter("P_LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOC;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable tblDet = QueryDataTable("tbl_det", "SP_GET_CURRECT_BY_BOOK", CommandType.StoredProcedure, false, param);
            return tblDet;
        }
        //Darshana 2015-11-02
        public List<GiftVoucherPages> GetVoucherBySearch(Int32 _book, Int32 _page, string _ref)
        {
            OracleParameter[] param = new OracleParameter[4];
            List<GiftVoucherPages> _gift = null;
            (param[0] = new OracleParameter("p_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _page;
            (param[1] = new OracleParameter("p_book", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _book;
            (param[2] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblInvoiceSearch", "sp_getpage_by_search", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _gift = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter);
            }
            return _gift;
        }

        //Nuwan 2016/02/17
        public List<MST_GIFTVOUCHER_SEARCH_HEAD> getGiftVoucherSearch(string company,int item, string pgeNum, string pgeSize, string searchFld, string searchVal)
        {
            string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal.ToUpper() + "%" : "";
            switch (searchFld)
            {
                case "Reference":
                    searchFld = "GVP_REF";
                    break;
                case "Book":
                    searchFld = "GVP_BOOK";
                    break;
                case "Page":
                    searchFld = "GVP_PAGE";
                    break;
                case "Balance":
                    searchFld = "GVP_BAL_AMT";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<MST_GIFTVOUCHER_SEARCH_HEAD> giftVou = new List<MST_GIFTVOUCHER_SEARCH_HEAD>();
            OracleParameter[] param = new OracleParameter[10];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_item", OracleDbType.Int32, null, ParameterDirection.Input)).Value = item;
            string type = "ITEM";
            if (item == 1)
            {
                type = "ITEM";
            }
            else {
                type = "VALUE";
            }
            (param[6] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (param[7] = new OracleParameter("p_tempwaveoff", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = vaweOffVal(company);
            (param[8] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "A";
            param[9] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblGiftVou", "SP_GET_GIFTVOUCHERSEARCH", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                giftVou = DataTableExtensions.ToGenericList<MST_GIFTVOUCHER_SEARCH_HEAD>(_dtResults, MST_GIFTVOUCHER_SEARCH_HEAD.Converter);
            }
            return giftVou;
        }
        public int vaweOffVal(string company)
        {

            int vaweOff = 0;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_GET_WAVEOFFVAL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0 && _dtResults.Rows[0]["MC_WAVEOFF_VAL"].ToString() != "")
            {
                vaweOff = Convert.ToInt32(_dtResults.Rows[0]["MC_WAVEOFF_VAL"].ToString());
            }
            return vaweOff;
        }

        public GiftVoucherPages getGiftVoucherPage(string voucherNo, string voucherBook,string company)
        {
            OracleParameter[] param = new OracleParameter[4];
            GiftVoucherPages vouPge = new GiftVoucherPages();
            (param[0] = new OracleParameter("p_voucheNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = voucherNo;
            (param[1] = new OracleParameter("p_voucheBook", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = voucherBook;
            (param[2] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_GIFTVOUCHERBOOK", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                vouPge = DataTableExtensions.ToGenericList<GiftVoucherPages>(_dtResults, GiftVoucherPages.Converter)[0];
            }
            return vouPge;
        }
        //Add  by lakshan copy from general Dal
        public MasterItem GetItemMaster(string _item)
        {// Nadeeka
            MasterItem _itemList = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;

            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = QueryDataTable("tblItem", "get_itemMaster", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _itemList = DataTableExtensions.ToGenericList<MasterItem>(_itemTable, MasterItem.ConvertTotal)[0];
            }
            return _itemList;
        }
        //ADD BY THARANGA 2019/01/16
        public Int16 deletePickedManualDocDetail(string _refNo, string _Loc, string _user, string _Status)
        {
            Int16 rows_affected = 0;

            OracleCommand CMD = new OracleCommand("SP_DELETE_TEMP_MAN_DOC", oConnection_FMS);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.Add("p_ref", OracleDbType.Varchar2).Value = _refNo;
            CMD.Parameters.Add("p_loc", OracleDbType.Varchar2).Value = _Loc;
            CMD.Parameters.Add("p_user", OracleDbType.Varchar2).Value = _user;
            CMD.Parameters.Add("p_status", OracleDbType.Varchar2).Value = _Status;
            CMD.ExecuteNonQuery();

            return rows_affected;

        }
    }
}

